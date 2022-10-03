using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.Bl;
using GEIMS.BL;
using GEIMS.Bo;
using GEIMS.BO;
using GEIMS.Client.UI;
using GEIMS.Common;
using GEIMS.DataAccess;

namespace GEIMS.Leave
{
    public partial class LeaveTemplate : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(FeeCollection));

        #region Declaration
        double TotalDiscount = 0;
        double TotalPaidAmount = 0;
        int initialInsert = 0;
        #endregion

        #region Pageload Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }
            if (!Page.IsPostBack)
            {
                try
                {
                    ddlSearchBy.SelectedIndex = 1;
                    ViewState["Mode"] = "Save";
                    BindAcademicYear();
                    btnSave.Visible = false;
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
        }
        #endregion

        #region BindAcademicYear
        public void BindAcademicYear()
        {
            try
            {
                #region Fetch Academic Month from School
                SchoolBL objSchoolBl = new SchoolBL();
                ApplicationResult objResults = new ApplicationResult();
                int intMonth = 0;

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

                DataTable dt = new DataTable();
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
                                dr["AcademicYear"] = Convert.ToString("0" + (f - 1).ToString() + "-" + "0" + (f).ToString());
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

                objControls.BindDropDown_ListBox(dt, ddlYear, "AcademicYear", "AcademicYear");
                ddlYear.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-Select-", ""));


            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion

        #region Gridview Row COmmand Event
        protected void gvStudent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                btnSave.Enabled = true;
                btnSave.Attributes.Add("bgcolor", "#848484");
                Controls objControls = new Controls();
                StudentBL objStudentBL = new StudentBL();
                ApplicationResult objResults = new ApplicationResult();
                if (e.CommandName.ToString() == "Edit1")
                {
                    ViewState["StudentMID"] = Convert.ToInt32(e.CommandArgument.ToString());
                    objResults = objStudentBL.Student_Select(Convert.ToInt32(ViewState["StudentMID"].ToString()), 0);

                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {

                            ViewState["DivisionName"] = objResults.resultDT.Rows[0][StudentBO.STUDENT_CURRENTDIVISIONTID].ToString();
                            #region Find DivisionName
                            DivisionTBL objDivision = new DivisionTBL();
                            ApplicationResult objResultsDivision = new ApplicationResult();
                            objResultsDivision = objDivision.DivisionT_Select_By_DivisionTID(Convert.ToInt32(objResults.resultDT.Rows[0][StudentBO.STUDENT_CURRENTDIVISIONTID].ToString()));
                            if (objResultsDivision != null)
                            {
                                if (objResultsDivision.resultDT.Rows.Count > 0)
                                {
                                    ViewState["Division"] = objResultsDivision.resultDT.Rows[0][DivisionTBO.DIVISIONT_DIVISIONNAME].ToString();
                                }

                            }
                            #endregion

                            #region Find SectionName
                            SectionBL objSection = new SectionBL();
                            ApplicationResult objResultsSection = new ApplicationResult();
                            objResultsSection = objSection.Section_Select(Convert.ToInt32(objResults.resultDT.Rows[0][StudentBO.STUDENT_CURRENTSECTIONID].ToString()));
                            if (objResultsSection != null)
                            {
                                if (objResultsSection.resultDT.Rows.Count > 0)
                                {
                                    ViewState["SectionName"] = objResultsSection.resultDT.Rows[0][SectionBO.SECTION_SECTIONNAME].ToString();
                                }

                            }
                            #endregion

                            #region Find Class
                            ClassBL objClass = new ClassBL();
                            ApplicationResult objResultsClass = new ApplicationResult();
                            objResultsClass = objClass.Class_Select(Convert.ToInt32(objResults.resultDT.Rows[0][StudentBO.STUDENT_CURRENTCLASSID].ToString()));
                            if (objResultsClass != null)
                            {

                                if (objResultsClass.resultDT.Rows.Count > 0)
                                {
                                    ViewState["ClassMID"] = objResultsClass.resultDT.Rows[0][ClassBO.CLASS_CLASSMID].ToString();
                                    ViewState["ClassName"] = objResultsClass.resultDT.Rows[0][ClassBO.CLASS_CLASSNAME].ToString();
                                }

                            }
                            #endregion

                            ViewState["AcademicYear"] = objResults.resultDT.Rows[0][StudentBO.STUDENT_CURRENTYEAR].ToString();

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

        #region Row Data Bound of Past Fee Deatils Gridview
        protected void gvPastFees_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TotalPaidAmount = TotalPaidAmount + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "FeesAmount"));
                    TotalDiscount = TotalDiscount + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Discount"));
                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label lblPaidDiscount = (Label)e.Row.FindControl("lblTotalGivenDiscount");
                    lblPaidDiscount.Text = TotalDiscount.ToString();

                    Label lblTotal = (Label)e.Row.FindControl("lblTotalPaidFees");
                    lblTotal.Text = TotalPaidAmount.ToString();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Save Click Event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                LeaveBo objLeaveBo = new LeaveBo();
                LeaveBl objLeaveBl = new LeaveBl();
                ApplicationResult objResult1 = new ApplicationResult();

                LeaveTemplateBo objLeaveTemplateBo = new LeaveTemplateBo();
                LeaveTemplateBl objLeaveTemplateBl = new LeaveTemplateBl();
                ApplicationResult objResult = new ApplicationResult();
                int k = 0;
                int intCount = 0;
                DatabaseTransaction.OpenConnectionTransation();
                foreach (GridViewRow row in gvLeave.Rows)
                {
                    objLeaveTemplateBo.EmployeeMID = Convert.ToInt32(hfEmployeeID.Value);
                    objLeaveTemplateBo.LeaveID = Convert.ToInt32(row.Cells[0].Text);
                    objLeaveTemplateBo.Total = (((TextBox)row.FindControl("txtTotalLeaves")).Text);
                    objLeaveTemplateBo.AcademicYear = ddlYear.Text;
                    objLeaveTemplateBo.CreatedBy = Convert.ToInt32(Session[ApplicationSession.USERID]);
                    objLeaveTemplateBo.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    objLeaveTemplateBo.LastModifiedBy = Convert.ToInt32(Session[ApplicationSession.USERID]);
                    objLeaveTemplateBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();

                    if (((CheckBox)row.FindControl("chkChild")).Checked)
                    {
                        objResult1 = objLeaveBl.Leave_SelectName_ByID(objLeaveTemplateBo.LeaveID);
                        if (objResult1.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {
                            intCount += 1;
                            if ((objLeaveTemplateBo.Total != "") || (objLeaveTemplateBo.Total != " "))  
                            {
                                if ((objLeaveTemplateBo.Total == "0") || (objLeaveTemplateBo.Total == "00"))
                                {
                                    if ((objResult1.resultDT.Rows[0]["LeaveName"].ToString() != "Duty Leave") )
                                    {
                                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                        "<script language='javascript'>alert('Total Leave Cannot be 0 or 00,Please Fill Total Leaves of " + objResult1.resultDT.Rows[0]["LeaveName"].ToString() +
                                        ".');</script>");
                                        break;
                                    }
                                    else
                                    {
                                        objResult = objLeaveTemplateBl.LeaveTemplate_Insert(objLeaveTemplateBo);
                                        if (objResult != null)
                                        {
                                            k += 1;
                                        }
                                    }
                                }
                                else
                                {
                                    objResult = objLeaveTemplateBl.LeaveTemplate_Insert(objLeaveTemplateBo);
                                    if (objResult != null)
                                    {
                                        k += 1;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        objResult = objLeaveTemplateBl.LeaveTemplate_Delete(objLeaveTemplateBo);
                    }
                }
                if (k == intCount)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Leave Template Updated Successfully.');</script>");
                    DatabaseTransaction.CommitTransation();
                    ClearAll();
                    gvLeave.Visible = false;
                }
                else
                {
                    DatabaseTransaction.RollbackTransation();
                    //DatabaseTransaction.connection.Close();
                }
                ddlSearchBy.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                DatabaseTransaction.RollbackTransation();
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Go for Searching Employee
        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                LeaveBo objLeaveBo = new LeaveBo();
                LeaveBl objLeaveBl = new LeaveBl();
                ApplicationResult objResult1 = new ApplicationResult();

                LeaveTemplateBl objLeaveTemplateBl = new LeaveTemplateBl();
                ApplicationResult objResultProgram = new ApplicationResult();

                if (hfEmployeeID.Value != "")
                {
                    
                    objResultProgram = objLeaveTemplateBl.LeaveTemplate_SelectForTemplate(Convert.ToInt32(hfEmployeeID.Value), ddlYear.Text);
                    if (objResultProgram != null)
                    {

                        if (objResultProgram.resultDT.Rows.Count > 0)
                        {
                        
                            gvLeave.DataSource = null;
                            gvLeave.Visible = true;
                            gvLeave.DataSource = objResultProgram.resultDT;
                            gvLeave.DataBind();
                            btnSave.Visible = true;
                           // txtSearchName.Enabled = false;
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('No Record Found.');</script>");
                            gvLeave.Visible = false;

                        }
                        for (int i = 0; i < objResultProgram.resultDT.Rows.Count; i++)
                        {
                            var Id = objResultProgram.resultDT.Rows[i]["LeaveID"].ToString();
                            var Total = objResultProgram.resultDT.Rows[i]["Total"].ToString();

                            foreach (GridViewRow row in gvLeave.Rows)
                            {
                                if (row.Cells[0].Text == Id)
                                {
                                    objResult1 = objLeaveBl.Leave_SelectName_ByID(Convert.ToInt32(Id));
                                    if (objResult1.status == ApplicationResult.CommonStatusType.SUCCESS)
                                    {

                                        if ((Total != "0"))// || ((objResult1.resultDT.Rows[0]["LeaveName"].ToString()) == "Duty Leave"))
                                        {
                                            ((CheckBox)row.FindControl("chkChild")).Checked = true;
                                            Convert.ToDouble(((TextBox)row.FindControl("txtTotalLeaves")).Text = Total);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                   // logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Employee Not Found');</script>");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Get Employee Web Service Method
        [System.Web.Services.WebMethod]
        public static string[] GetAllEmployeeNameForReport(string prefixText, int TrustMID, int SchoolMID)
        {
            UserTemplateTBl objEmployeeMbl = new UserTemplateTBl();
            ApplicationResult objResult = new ApplicationResult();
            string strSearchText = prefixText + "%";
            List<string> result = new List<string>();

            objResult = objEmployeeMbl.Employee_Select_AutocomleteForPayroll(strSearchText, TrustMID, SchoolMID);
            if (objResult != null)
            {
                for (int i = 0; i < objResult.resultDT.Rows.Count; i++)
                {
                    string strEmployeeCodeName = objResult.resultDT.Rows[i]["EmployeeName"].ToString();
                    string strEmployeeMID = objResult.resultDT.Rows[i][EmployeeMBO.EMPLOYEEM_EMPLOYEEMID].ToString();
                    result.Add(string.Format("{0}~{1}", strEmployeeCodeName, strEmployeeMID));
                }
            }
            
            
            return result.ToArray();
        }

        #endregion

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
        }
        #endregion

        protected void txtSearchName_TextChanged(object sender, EventArgs e)
        {
            gvLeave.Visible = false;
            gvLeave.DataSource = null;
            btnSave.Visible = false;
        }

       

        protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvLeave.Visible = false;
            gvLeave.DataSource = null;
            btnSave.Visible = false;
            // gvLeave.Visible = true;
            //txtSearchName.Enabled = true;
        }

       
    }
}