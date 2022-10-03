using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using log4net;
using GEIMS.DataAccess;

namespace GEIMS.Client.UI
{
    public partial class WorkingDays : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(WorkingDays));
        #region Page_Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["WorkingDayID"] = 0;
                BindAcademicYear();
                divSave.Visible = false;
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

                objControls.BindDropDown_ListBox(dt, ddlAcademicYear, "AcademicYear", "AcademicYear");
                ddlAcademicYear.Items.Insert(0, new ListItem("-Select-", "-1"));
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");

            }
        }
        #endregion

        #region btnGridView
        protected void btnViewGrid_Click(object sender, EventArgs e)
        {
            BindGridView();
        }
        #endregion

        #region BindGridView
        public void BindGridView()
        {
            ApplicationResult objResult = new ApplicationResult();
            WorkingDaysBl objWorkDaysBL = new WorkingDaysBl();
            WorkingDaysBo objWorkingDaysBO = new WorkingDaysBo();

            objResult = objWorkDaysBL.WorkingDays_SelectAllMonths();
            if (objResult != null)
            {
                gvWorkingDays.DataSource = objResult.resultDT;
                gvWorkingDays.DataBind();
                if (objResult.resultDT.Rows.Count > 0)
                {
                    gvWorkingDays.Visible = true;
                    divSave.Visible = true;
                }
                else
                {
                    gvWorkingDays.Visible = false;
                    divSave.Visible = false;
                }

            }

            //Fetch the data in Gridview if Data in Database table
            DataTable Dt = Select_WorkingDays_ByAcademicYear();
            if (Dt.Rows.Count > 0)
            {
                #region Updation of WOrk Days
                for (int i = 0; i < gvWorkingDays.Rows.Count; i++)
                {
                    TextBox txtDays = (TextBox)gvWorkingDays.Rows[i].Cells[2].FindControl("txtTotalDays");
                    txtDays.Text = Dt.Rows[i][WorkingDaysBo.WORKINGDAYS_TOTALWORKINGDAYS].ToString();
                }
                #endregion
            }

        }
        #endregion

        #region btnSave
        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region Fetch The Data if Present
            WorkingDaysBl objWOrkingDaysBL = new WorkingDaysBl();
            WorkingDaysBo objWorkingDaysBO = new WorkingDaysBo();


            ApplicationResult objResults = new ApplicationResult();

            objWorkingDaysBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
            objWorkingDaysBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
            objWorkingDaysBO.AcademicYear = ddlAcademicYear.SelectedItem.ToString();
            objWorkingDaysBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
            objWorkingDaysBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);

             DataTable Dt = Select_WorkingDays_ByAcademicYear();
             if (Dt.Rows.Count > 0)
             {
                 // if Data is Already in table than Update
                 #region Update Data
                 for (int i = 0; i < gvWorkingDays.Rows.Count; i++)
                 {
                     objWorkingDaysBO.WorkingDayID = Convert.ToInt32(Dt.Rows[i][WorkingDaysBo.WORKINGDAYS_WORKINGDAYID].ToString());

                     objWorkingDaysBO.MonthID = Convert.ToInt32(gvWorkingDays.Rows[i].Cells[0].Text);
                     TextBox txtDays = (TextBox)gvWorkingDays.Rows[i].Cells[2].FindControl("txtTotalDays");
                     objWorkingDaysBO.TotalWorkingDays = Convert.ToInt32(txtDays.Text);

                     objResults = objWOrkingDaysBL.WorkingDays_Update(objWorkingDaysBO);
                     if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                     {

                     }

                 }
                 #endregion
             }
             else
             {
                 // if No row is found than new entry or insertion entry in table
                 #region Insert Data
                 for (int i = 0; i < gvWorkingDays.Rows.Count; i++)
                 {
                     objWorkingDaysBO.MonthID = Convert.ToInt32(gvWorkingDays.Rows[i].Cells[0].Text);
                     TextBox txtDays = (TextBox)gvWorkingDays.Rows[i].Cells[2].FindControl("txtTotalDays");
                     objWorkingDaysBO.TotalWorkingDays = Convert.ToInt32(txtDays.Text);
                     objWorkingDaysBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                     objWorkingDaysBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                     objResults = objWOrkingDaysBL.WorkingDays_Insert(objWorkingDaysBO);
                     if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                     {

                     }

                 }
                 #endregion
             }
            
             ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Working Days Successfully Added/Updated.');</script>");
            divSave.Visible = false;
            gvWorkingDays.Visible = false;
            BindAcademicYear();
            #endregion
        }
        #endregion

        #region Fetching Datatable to fetch The WOrk Days Data
        public DataTable Select_WorkingDays_ByAcademicYear()
        {
            ApplicationResult objResult = new ApplicationResult();
            WorkingDaysBl objWorkDaysBL = new WorkingDaysBl();

            objResult = objWorkDaysBL.WorkingDays_SelectAll(ddlAcademicYear.SelectedItem.Text);
            if (objResult != null)
            {
            }
            return objResult.resultDT;
        }
        #endregion
    }
}