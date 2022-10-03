using System;
using System.Data;
using System.Web.UI.WebControls;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using log4net;
using System.Web.UI;
using GEIMS.DataAccess;

namespace GEIMS.Client.UI
{
    public partial class StudentAttendence : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(StudentAttendence));
        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("../UserLogin.aspx");
                }
                if (!Page.IsPostBack)
                {
                    btnSave.Enabled = false;
                    btnSave.Attributes.Add("bgcolor", "#848484");
                    btnSMS.Visible = false;
                    BindAcademicYear();
                    txtdate.Attributes.Add("readonly", "readonly");
                    ViewState["Mode"] = "Save";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "BindClass();", true);
                   
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
               
                //ddlAcademicYear.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Webservice

        [System.Web.Services.WebMethod]
        public static string LoadClass(int intEmployeeMID)
        {
            try
            {
                #region Bind Class
                ClassBL objClassBL = new ClassBL();
                ClassBO objClassBO = new ClassBO();

                ApplicationResult objResultSection = new ApplicationResult();
                objResultSection = objClassBL.Class_SelectAll_EmployeeWise(intEmployeeMID);
                if (objResultSection != null)
                {
                    if (objResultSection.resultDT.Rows.Count > 0)
                    {

                    }

                }
                string res = "";
                res = DataSetToJSON(objResultSection.resultDT);
                return res;
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [System.Web.Services.WebMethod]
        public static string LoadDivision(int intClassMID, int intSchoolMID)
        {
            try
            {
                #region Bind Division
                DataTable dtDivision = new DataTable();
                DivisionTBL objDivisionBL = new DivisionTBL();
                DivisionTBO objDivisionBO = new DivisionTBO();

                ApplicationResult objResultSection = new ApplicationResult();
                objResultSection = objDivisionBL.Division_SelectAll_ClassWise_ForDropDown(intClassMID, intSchoolMID);
                if (objResultSection != null)
                {
                    dtDivision = objResultSection.resultDT;
                    if (dtDivision.Rows.Count > 0)
                    {

                    }

                }
                string res = "";
                res = DataSetToJSON(dtDivision);
                return res;
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static string DataSetToJSON(DataTable dt)
        {

            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            JavaScriptSerializer json = new JavaScriptSerializer();

            return json.Serialize(rows);
        }

        #endregion

        #region View Grid Button Event
        protected void btnViewGrid_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = true;
                btnSave.Attributes.Add("bgcolor", "#848484");
                Controls objControls = new Controls();
                ViewState["ClassMID"] = Convert.ToInt32(Request.Form[ddlClass.UniqueID]);
                ViewState["DivisionName"] = Convert.ToInt32(Request.Form[ddlDivision.UniqueID]);
                ViewState["AcademicYear"] = ddlAcademicYear.SelectedItem.ToString();
                hfCLassMID.Value = ViewState["ClassMID"].ToString();
                hfDivisionTID.Value = ViewState["DivisionName"].ToString();
                BindStudentGrid();
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "BindDorpdownOnButtonClick();", true);
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Save Button Event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ViewState["ClassMID"] = Convert.ToInt32(Request.Form[ddlClass.UniqueID]);
            ViewState["DivisionName"] = Convert.ToInt32(Request.Form[ddlDivision.UniqueID]);
            ViewState["AcademicYear"] = ddlAcademicYear.SelectedItem.ToString();

            StudentAttendenceBL objStudentAttendenceBL = new StudentAttendenceBL();
            StudentAttendenceBO objStudentAttendenceBO = new StudentAttendenceBO();
            ApplicationResult objResults = new ApplicationResult();
            try
            {
                string PresentIds = string.Empty;
                string AbsentIds = string.Empty;
                int Count = 0;
                foreach (GridViewRow row in gvStudent.Rows)
                {
                    ViewState["StudentMID"] = Convert.ToInt32(row.Cells[0].Text);

                    if (((CheckBox)row.FindControl("chkChild")).Checked)
                    {
                        PresentIds += ViewState["StudentMID"].ToString() + ",";
                        Count = Count + 1;
                    }
                    else
                    {
                        AbsentIds += ViewState["StudentMID"].ToString() + ",";
                    }

                }
                objStudentAttendenceBO.PresentStudentIDs = PresentIds.TrimEnd(',');
                objStudentAttendenceBO.AbsentStudentIds = AbsentIds.TrimEnd(',');
                objStudentAttendenceBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                objStudentAttendenceBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                objStudentAttendenceBO.EmployeeMID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                objStudentAttendenceBO.ClassMID = Convert.ToInt32(ViewState["ClassMID"].ToString());
                objStudentAttendenceBO.DivisionTID = Convert.ToInt32(ViewState["DivisionName"].ToString());
                objStudentAttendenceBO.AcademicYear = ViewState["AcademicYear"].ToString();
                objStudentAttendenceBO.Date = txtdate.Text;
                objStudentAttendenceBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                objStudentAttendenceBO.Time = System.DateTime.Now.ToShortTimeString();
                objStudentAttendenceBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                if (Count > 0)
                {
                    if (ViewState["Mode"].ToString() == "Save")
                    {
                        objResults = objStudentAttendenceBL.StudentAttendence_Insert(objStudentAttendenceBO);
                        if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Student Attendence Done Successfully.');</script>");
                        }
                    }
                    else
                    {
                        objStudentAttendenceBO.StudentAttendenceMID = Convert.ToInt32(ViewState["StudentAttendenceMID"].ToString());
                        objResults = objStudentAttendenceBL.StudentAttendence_Update(objStudentAttendenceBO);
                        if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Student Attendence Updated Successfully.');</script>");
                            ViewState["Mode"] = "Save";
                        }
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Select Any One Checkbox For Attendence.');</script>");
                    ViewState["ClassMID"] = Convert.ToInt32(Request.Form[ddlClass.UniqueID]);
                    ViewState["DivisionName"] = Convert.ToInt32(Request.Form[ddlDivision.UniqueID]);
                    ViewState["AcademicYear"] = ddlAcademicYear.SelectedItem.ToString();
                    hfCLassMID.Value = ViewState["ClassMID"].ToString();
                    hfDivisionTID.Value = ViewState["DivisionName"].ToString();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "BindDorpdownOnButtonClick();", true);
                }
                BindStudentGrid();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "BindSection();", true);
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region BindGrid of Student
        protected void BindStudentGrid()
        {
            try
            {
                StudentAttendenceBL objStudentAttendenceBL = new StudentAttendenceBL();
                ApplicationResult objResult = new ApplicationResult();
                StudentBL objStudentBL = new StudentBL();
                ViewState["ClassMID"] = Convert.ToInt32(Request.Form[ddlClass.UniqueID]);
                ViewState["DivisionName"] = Convert.ToInt32(Request.Form[ddlDivision.UniqueID]);
                ViewState["AcademicYear"] = ddlAcademicYear.SelectedItem.ToString();
                objResult = objStudentBL.Student_SelectFor_Upgrade(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(ViewState["ClassMID"].ToString()), Convert.ToInt32(ViewState["DivisionName"].ToString()), ddlAcademicYear.SelectedItem.ToString(), 1);
                if (objResult != null)
                {
                    gvStudent.DataSource = objResult.resultDT;
                    gvStudent.DataBind();
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        btnSMS.Visible = true;
                        objResult = objStudentAttendenceBL.StudentAttendence_Select_Datewise(txtdate.Text, Convert.ToInt32(ViewState["ClassMID"].ToString()), Convert.ToInt32(ViewState["DivisionName"].ToString()), ddlAcademicYear.SelectedItem.ToString());
                        if (objResult != null)
                        {
                            if (objResult.resultDT.Rows.Count > 0)
                            {
                                ViewState["Mode"] = "Edit";
                                ViewState["StudentAttendenceMID"] = objResult.resultDT.Rows[0][StudentAttendenceBO.STUDENTATTENDENCE_STUDENTATTENDENCEMID].ToString();
                                string PresentStudentIDs = objResult.resultDT.Rows[0][StudentAttendenceBO.STUDENTATTENDENCE_PRESENTSTUDENTIDS].ToString();
                                string AbsentStudentIDs = objResult.resultDT.Rows[0][StudentAttendenceBO.STUDENTATTENDENCE_ABSENTSTUDENTIDS].ToString();
                                string[] Presentids = PresentStudentIDs.Split(',');
                                string[] Absentids = AbsentStudentIDs.Split(',');

                                foreach (GridViewRow row in gvStudent.Rows)
                                {
                                    foreach (string pid in Presentids)
                                    {
                                        if (row.Cells[0].Text == pid)
                                        {
                                            ((CheckBox)row.FindControl("chkChild")).Checked = true;
                                            ((CheckBox)row.FindControl("chkSms")).Checked = false;
                                            ((CheckBox)row.FindControl("chkSms")).Enabled = false;
                                            // ((CheckBox)gvStudent.HeaderRow.FindControl("chkHeader")).Enabled = false;
                                        }

                                    }
                                    foreach (string aid in Absentids)
                                    {
                                        if (row.Cells[0].Text == aid)
                                        {
                                            ((CheckBox)row.FindControl("chkChild")).Checked = false;
                                            ((CheckBox)row.FindControl("chkSms")).Checked = true;
                                        }

                                    }
                                }
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "BindDorpdownOnButtonClick();", true);
                            }
                            else
                            {
                                ViewState["Mode"] = "Save";
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CheckAll();BindDorpdownOnButtonClick();", true);
                            }
                        }

                    }
                    else
                    {
                        btnSMS.Visible = false;
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

        #region Sms button Event
        protected void btnSMS_Click(object sender, EventArgs e)
        {

        }
        #endregion


    }
}