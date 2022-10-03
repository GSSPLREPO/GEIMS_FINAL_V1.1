using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.Bl;
using GEIMS.BL;
using GEIMS.Bo;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.Leave
{
    public partial class Leave : PageBase
    {
        #region Global Variable
        private static ILog logger = LogManager.GetLogger(typeof(Leave));
       // int intMonth = 0;
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindLeave();
                    DataTable dt1 = new DataTable();
                    dt1 = BindAcademicYear();
                    Controls objControl = new Controls();
                    objControl.BindDropDown_ListBox(dt1, ddlAcademicYear, "AcademicYear", "AcademicYear");
                    BindAcademicYear();
                    ViewState["Mode"] = "Save";
                    ViewState["Deduction"] = "0";
                    ddlAcademicYear.Enabled = false;
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
        }
        #endregion

        #region Bind Leave
        public void BindLeave()
        {
            ApplicationResult objResult = new ApplicationResult();
            LeaveBl objLeavebl = new LeaveBl();

            objResult = objLeavebl.Leave_SelectAll();
            if (objResult.resultDT.Rows.Count > 0)
            {
                gvLeave.DataSource = objResult.resultDT;
                gvLeave.DataBind();
                PanelVisibility(1);
            }
            else
            {
                PanelVisibility(2);
            }
        }
        #endregion

        #region BindAcademicYear
        public DataTable BindAcademicYear()
        {
            //try
            //{
            DataTable dt = new DataTable();

            #region Fetch Academic Month from School

            SchoolBL objSchoolBl = new SchoolBL();
            ApplicationResult objResults = new ApplicationResult();

            int intMonth = 4;  

            objResults = objSchoolBl.School_Select(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));

            if (objResults != null)
            {
                if (objResults.resultDT.Rows.Count > 0)
                {
                    intMonth = Convert.ToInt32(objResults.resultDT.Rows[0][SchoolBO.SCHOOL_ACADEMICMONTH].ToString());
                }
            }

            #endregion

            Controls objControls = new Controls();
            int month = System.DateTime.Now.Month;
            int Year = System.DateTime.Now.Year;
            int lastTwoDigit = Year % 100;
            string yr = string.Empty;
            if (month >= intMonth)
                yr = (lastTwoDigit.ToString() + (lastTwoDigit + 1).ToString()).ToString();
            else
                yr = ((lastTwoDigit - 1).ToString() + lastTwoDigit.ToString()).ToString();

            int f = (Convert.ToInt32(yr.Substring(0, 2)));
            int l = (Convert.ToInt32(yr.Substring(2, 2)));

            DataRow dr = null;
            dt.Columns.Add(new DataColumn("AcademicYear", typeof(string)));

            for (int i = 0; i < 5; i++)
            {
                dr = dt.NewRow();
                if (i == 0)
                {
                    dr["AcademicYear"] = Convert.ToString(f.ToString() + "-" + l.ToString());
                    dt.Rows.Add(dr);
                }
                else
                {
                    if ((f - 1).ToString().Length < 2)
                    {
                        if (f.ToString().Length == 2)
                        {
                            dr["AcademicYear"] = Convert.ToString("0" + (f - 1).ToString() + "-" + (f).ToString());
                        }
                        else
                        {
                            dr["AcademicYear"] =
                                Convert.ToString("0" + (f - 1).ToString() + "-" + "0" + (f).ToString());
                        }
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        dr["AcademicYear"] = Convert.ToString((f - 1).ToString() + "-" + (f).ToString());
                        dt.Rows.Add(dr);
                    }
                    f = f - 1;
                    l = f;
                }
            }
            return dt;

            //}
            //catch (Exception ex)
            //{
            //    logger.Error("Error", ex);
            //    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");

            //}
        }
        #endregion

        #region Save Button Click Event
        protected void btnSaveClass_OnClick(object sender, EventArgs e)
        {
            try
            {
                LeaveBo objLeaveBO = new LeaveBo();
                LeaveBl objLeaveBL = new LeaveBl();
                ApplicationResult objResult = new ApplicationResult();
                DataTable dtResult = new DataTable();
                int intLeaveID = 0;

                objLeaveBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                objLeaveBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                objLeaveBO.LeaveName = txtLeaveName.Text.Trim();
                objLeaveBO.LeaveCode = txtLeaveCode.Text.Trim();
                objLeaveBO.Year = ddlAcademicYear.Text;
                objLeaveBO.IsDeduction = Convert.ToInt32(cbDeduction.Checked);
                objLeaveBO.IsCarryForward = Convert.ToInt32(cbCarryForward.Checked);
                objLeaveBO.LeaveDescription = txtDescription.Text.Trim();
                objLeaveBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                objLeaveBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();

                //Code For Validate Leave Name
                if (ViewState["Mode"].ToString() == "Save")
                {
                    intLeaveID = -1;
                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    intLeaveID = Convert.ToInt32(ViewState["LeaveID"].ToString());
                }
                objResult = objLeaveBL.Leave_ValidateName(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), intLeaveID, objLeaveBO.LeaveName, ddlAcademicYear.Text);
                if (objResult != null)
                {
                    dtResult = objResult.resultDT;
                    if (dtResult.Rows.Count > 0)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Leave name already exist.');</script>");
                    }
                    else
                    {
                        if (ViewState["Deduction"].ToString() == "0")
                        {

                            if (ViewState["Mode"].ToString() == "Save")
                            {
                                objLeaveBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                objLeaveBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                                objResult = objLeaveBL.Leave_Insert(objLeaveBO);
                                if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                        "<script>alert('Record saved successfully.');</script>");
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                        "<script>alert('LeaveName already exist.');</script>");
                                }

                            }
                            else if (ViewState["Mode"].ToString() == "Edit")
                            {
                                objLeaveBO.LeaveID = Convert.ToInt32(ViewState["LeaveID"].ToString());
                                objResult = objLeaveBL.Leave_Update(objLeaveBO);
                                if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record updated successfully.');</script>");
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You can no delete this leave, Because its used in Leave Template.');</script>");
                                }
                            }
                            ClearAll();
                            BindLeave();
                            PanelVisibility(1);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                           "<script>alert('There is already assign deduction leave. Only one Leave can be deduct.');</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Add New Button Click Event
        protected void lnkAddNewClass_OnClick(object sender, EventArgs e)
        {
            try
            {

                ClearAll();
                cbCarryForward.Checked = false;
                PanelVisibility(2);
                ddlAcademicYear.Enabled = false;
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region View List Button Click event
        protected void lnkViewList_OnClick(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                PanelVisibility(1);
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Leave GridView Events [Row Command, Pre Render]
        protected void gvLeave_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                LeaveBl objLeaveBL = new LeaveBl();
                if (e.CommandName.ToString() == "Edit1")
                {
                    ViewState["Mode"] = "Edit";
                    ViewState["LeaveID"] = e.CommandArgument.ToString();
                    objResult = objLeaveBL.Leave_Select(Convert.ToInt32(ViewState["LeaveID"].ToString()));
                    if (objResult != null)
                    {
                        DataTable dtResult = objResult.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            txtLeaveName.Text = dtResult.Rows[0][LeaveBo.LEAVE_LEAVENAME].ToString();
                            txtLeaveCode.Text = dtResult.Rows[0][LeaveBo.LEAVE_LEAVECODE].ToString();
                            ddlAcademicYear.Text = dtResult.Rows[0][LeaveBo.LEAVE_YEAR].ToString();
                            ddlAcademicYear.Enabled = false;
                            if (dtResult.Rows[0][LeaveBo.LEAVE_IsDeduction].ToString() == "1")
                            {
                                cbDeduction.Checked = true;
                            }
                            else
                            {
                                cbDeduction.Checked = false;
                            }
                            if (dtResult.Rows[0][LeaveBo.LEAVE_ISCARRYFORWARD].ToString() == "1")
                            {
                                cbCarryForward.Checked = true;
                            }
                            else
                            {
                                cbCarryForward.Checked = false;
                            }
                            txtDescription.Text = dtResult.Rows[0][LeaveBo.LEAVE_LEAVEDESCRIPTION].ToString();
                            PanelVisibility(2);
                        }
                    }
                }
                else if (e.CommandName.ToString() == "Delete1")
                {
                    objResult = objLeaveBL.Leave_Delete(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session[ApplicationSession.USERID]),
                        DateTime.UtcNow.AddHours(5.5).ToString());
                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record deleted successfully.');</script>");
                        PanelVisibility(1);
                        BindLeave();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You cannot delete this record because it is in use.');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        protected void gvLeave_OnPreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvLeave.Rows.Count > 0)
                {
                    gvLeave.UseAccessibleHeader = true;
                    gvLeave.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region ComboBox deduction CheckedChanged event
        protected void cbDeduction_OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbDeduction.Checked)
                {
                    LeaveBl objLeaveBl = new LeaveBl();
                    var objResult = objLeaveBl.Leave_Select_ForDeduction(ddlAcademicYear.Text);
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                            "<script>alert('there is already assign deduction leave. Only one Leave can be deduct.');</script>");
                        ViewState["Deduction"] = "1";
                    }
                }
                else
                {
                    ViewState["Deduction"] = "0";
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Clear All Method
        public void ClearAll()
        {
            Controls objControl = new Controls();
            objControl.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["Mode"] = "Save";
            ViewState["Deduction"] = "0";
            cbDeduction.Checked = false;
            cbCarryForward.Checked = false;
        }
        #endregion

        #region Panel Visibility Mode
        public void PanelVisibility(int intcode)
        {
            if (intcode == 1)
            {
                divGrid.Visible = true;
                tabs.Visible = false;
                lnkViewList.Visible = false;
            }
            else if (intcode == 2)
            {
                divGrid.Visible = false;
                tabs.Visible = true;
                lnkViewList.Visible = true;
            }
        }
        #endregion

    }
}