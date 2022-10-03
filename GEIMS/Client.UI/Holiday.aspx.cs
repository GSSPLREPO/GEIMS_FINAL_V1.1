using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.Client.UI
{
    public partial class Holiday : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(Holiday));
        int intMonth = 0;

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    DataTable dt1 = new DataTable();
                    dt1 = BindAcademicYear();
                    Controls objControl = new Controls();
                    objControl.BindDropDown_ListBox(dt1, ddlAcademicYear, "AcademicYear", "AcademicYear");
                    BindHoliday();
                    BindAcademicYear();
                    ViewState["Mode"] = "Save";
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
        }
        #endregion

        #region Bind Holiday
        public void BindHoliday()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                HolidayBl objHolidayBl = new HolidayBl();

                objResult = objHolidayBl.Holiday_SelectAll();
                if (objResult.resultDT.Rows.Count > 0)
                {
                    gvHoliday.DataSource = objResult.resultDT;
                    gvHoliday.DataBind();
                    PanelVisibility(1);
                }
                else
                {
                    PanelVisibility(2);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
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
            int intMonth = 6;

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
            DateTime fromdate = Convert.ToDateTime(txtFromDate.Text.Trim());
            DateTime todate1 = Convert.ToDateTime(txtToDate.Text.Trim());
          
                try
            {
                if (fromdate <= todate1)
                { 
                    HolidayBo objHolidayBO = new HolidayBo();
                    HolidayBl objHolidayBL = new HolidayBl();
                    ApplicationResult objResult = new ApplicationResult();
                    DataTable dtResult = new DataTable();
                    int intHolidayID = 0;

                    objHolidayBO.Name = txtHolidayName.Text.Trim();
                    objHolidayBO.AcademicYear = ddlAcademicYear.Text;
                    objHolidayBO.StartDate = txtFromDate.Text.Trim();
                    objHolidayBO.EndDate = txtToDate.Text.Trim();
                    objHolidayBO.Description = txtDescription.Text.Trim();

                        //Code For Validate Holiday Name
                        if (ViewState["Mode"].ToString() == "Save")
                        {
                            intHolidayID = -1;
                        }
                        else if (ViewState["Mode"].ToString() == "Edit")
                        {
                            intHolidayID = Convert.ToInt32(ViewState["HolidayID"].ToString());
                        }
                     objResult = objHolidayBL.Holiday_ValidateName(intHolidayID, objHolidayBO.StartDate, objHolidayBO.AcademicYear);

                    if (objResult != null)
                    {
                        dtResult = objResult.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Holiday already exist.');</script>");
                        }
                        else
                        {
                            if (ViewState["Mode"].ToString() == "Save")
                            {
                                objHolidayBO.CreatedBy = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                objHolidayBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                                objResult = objHolidayBL.Holiday_Insert(objHolidayBO);
                                if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record saved successfully.');</script>");
                                }
                            }
                            else if (ViewState["Mode"].ToString() == "Edit")
                            {
                                objHolidayBO.HolidayId = Convert.ToInt32(ViewState["HolidayID"].ToString());
                                objHolidayBO.LastModifiedBy = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                objHolidayBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                                objResult = objHolidayBL.Holiday_Update(objHolidayBO);
                                if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record updated successfully.');</script>");
                                }
                            }
                            ClearAll();
                            BindHoliday();
                            PanelVisibility(1);
                        }
                    }
                   
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! Wrong Date selection : To Date Must be greter then From Date .');</script>");
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
                PanelVisibility(2);
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

        #region Holiday GridView Events [Row Command, Pre Render]
        protected void gvHoliday_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                HolidayBl objHolidayBL = new HolidayBl();
                if (e.CommandName.ToString() == "Edit1")
                {
                    ViewState["Mode"] = "Edit";
                    ViewState["HolidayID"] = e.CommandArgument.ToString();
                    objResult = objHolidayBL.Holiday_Select(Convert.ToInt32(ViewState["HolidayID"].ToString()));
                    if (objResult != null)
                    {
                        DataTable dtResult = objResult.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            txtHolidayName.Text = dtResult.Rows[0][HolidayBo.HOLIDAY_NAME].ToString();
                            ddlAcademicYear.Text = dtResult.Rows[0][HolidayBo.HOLIDAY_ACADEMICYEAR].ToString();
                            txtFromDate.Text = dtResult.Rows[0][HolidayBo.HOLIDAY_STARTDATE].ToString();
                            txtToDate.Text = dtResult.Rows[0][HolidayBo.HOLIDAY_ENDDATE].ToString();
                            txtDescription.Text = dtResult.Rows[0][HolidayBo.HOLIDAY_DESCRIPTION].ToString();
                            PanelVisibility(2);
                        }
                    }
                }
                else if (e.CommandName.ToString() == "Delete1")
                {
                    objResult = objHolidayBL.Holiday_Delete(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session[ApplicationSession.USERID]),
                        DateTime.UtcNow.AddHours(5.5).ToString());
                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record deleted successfully.');</script>");
                        PanelVisibility(1);
                        BindHoliday();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You cannot delete this record because it is in used.');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        protected void gvHoliday_OnPreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvHoliday.Rows.Count > 0)
                {
                    gvHoliday.UseAccessibleHeader = true;
                    gvHoliday.HeaderRow.TableSection = TableRowSection.TableHeader;
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