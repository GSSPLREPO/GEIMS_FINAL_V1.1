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
    public partial class DivisionTransfer : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(DivisionTransfer));
        #region Page Load
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
                    BindAcademicYear();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "BindSection();", true);
                    btnSave.Enabled = false;

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
                ApplicationResult objResult = new ApplicationResult();
                StudentBL objStudentBL = new StudentBL();
                ViewState["ClassMID"] = Convert.ToInt32(Request.Form[ddlClass.UniqueID]);
                ViewState["DivisionName"] = Convert.ToInt32(Request.Form[ddlDivision.UniqueID]);
                ViewState["AcademicYear"] = ddlAcademicYear.SelectedItem.ToString();
                objResult = objStudentBL.Student_SelectFor_Upgrade(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(ViewState["ClassMID"].ToString()), Convert.ToInt32(ViewState["DivisionName"].ToString()), ddlAcademicYear.SelectedItem.ToString(), 1);
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvStudent.Visible = true;
                        gvStudent.DataSource = objResult.resultDT;
                        gvStudent.DataBind();
                    }
                    else
                    {
                        gvStudent.Visible = false;
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('No record Found');</script>");
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

        #region ViewGrid Button
        protected void btnViewGrid_Click(object sender, EventArgs e)
        {
            try
            {
                Controls objControls = new Controls();
                ViewState["SectionID"] = Convert.ToInt32(Request.Form[ddlSection.UniqueID]);
                ViewState["ClassMID"] = Convert.ToInt32(Request.Form[ddlClass.UniqueID]);
                ViewState["DivisionName"] = Convert.ToInt32(Request.Form[ddlDivision.UniqueID]);
                ViewState["AcademicYear"] = ddlAcademicYear.SelectedItem.ToString();
                hfCLassMID.Value = ViewState["ClassMID"].ToString();
                hfDivisionTID.Value = ViewState["DivisionName"].ToString();
                hfSectionID.Value = ViewState["SectionID"].ToString();

                BindStudentGrid();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "BindDorpdownOnButtonClick();", true);
                btnSave.Enabled = true;
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
        public static string LoadSection(int intSchoolID)
        {
            try
            {
                UpgradeStudent objStudentDetailMaster = new UpgradeStudent();
                DataTable dtStudent = new DataTable();
                dtStudent = objStudentDetailMaster.FetchSection(intSchoolID);
                string res = "";
                res = DataSetToJSON(dtStudent);
                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [System.Web.Services.WebMethod]
        public static string LoadClass(int intSectionID, int intSchoolMID)
        {
            try
            {
                #region Bind Class
                ClassBL objClassBL = new ClassBL();
                ClassBO objClassBO = new ClassBO();

                ApplicationResult objResultSection = new ApplicationResult();
                objResultSection = objClassBL.Class_SelectAll_SectionWise_ForDropDown(intSectionID, intSchoolMID);
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

        #region Btn Save Click
        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            try
            {
                DatabaseTransaction.OpenConnectionTransation();
                int intCount = 0;
                int intgvRowCount = gvStudent.Rows.Count;
                foreach (GridViewRow row in gvStudent.Rows)
                {
                    ViewState["ClassMID"] = Convert.ToInt32(Request.Form[ddlClass.UniqueID]);
                    ViewState["DivisionName"] = Convert.ToInt32(Request.Form[ddlDivision.UniqueID]);
                    ViewState["AcademicYear"] = ddlAcademicYear.SelectedItem.ToString();
                    ViewState["StudentMID"] = Convert.ToInt32(row.Cells[0].Text);
                    ViewState["StudentTID"] = Convert.ToInt32(row.Cells[1].Text);

                    StudentBL objStudentBL = new StudentBL();
                    StudentTBO objStudentTBO = new StudentTBO();
                    ApplicationResult objResults = new ApplicationResult();
           
                    DropDownList ddlgridDivision = (DropDownList)row.Cells[6].FindControl("ddlGridDivision");


                    if (((CheckBox)row.FindControl("chkChild")).Checked)
                    {

                        if (ddlgridDivision.SelectedValue != "")
                        {
                            if (ViewState["DivisionName"].ToString() == ddlgridDivision.SelectedValue)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Student are not upgrade into the Same Class.');</script>");
                            }
                            else
                            {

                                objResults = objStudentBL.StudentM_Update_AtTimeOfDivisionTransfer(Convert.ToInt32(ViewState["StudentMID"].ToString()), Convert.ToInt32(ddlgridDivision.SelectedValue), Convert.ToInt32(Session[ApplicationSession.USERID]), DateTime.UtcNow.AddHours(5.5).ToString());
                                if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                                {

                                }

                                objStudentTBO.StudentTID = Convert.ToInt32(ViewState["StudentTID"].ToString());
                                objResults = objStudentBL.StudentT_Update_AtTimeOfDivisionTransfer(Convert.ToInt32(ViewState["StudentTID"].ToString()), Convert.ToInt32(ddlgridDivision.SelectedValue), Convert.ToInt32(Session[ApplicationSession.USERID]), DateTime.UtcNow.AddHours(5.5).ToString());
                                if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Students Division Transfered Successfully.');</script>");
                                }
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Division(s) are not Selected.');</script>");
                        }


                    }
                    else
                    {
                        intCount++;
                    }

                }
                if (intgvRowCount == intCount)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Please Select Atleast One Student.');$('#divLoading').hide();</script>");
                }
                else
                {
                    gvStudent.Visible = false;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "BindSection();", true);
                }
                DatabaseTransaction.CommitTransation();
                
            }
            catch (Exception ex)
            {
                DatabaseTransaction.RollbackTransation();
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        
        #endregion

        #region Division Header Dropdown Change SelectedIndexChange
        protected void ddlDivisionHeader_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gvStudent.Rows)
                {
                    DropDownList ddlgridDivision = (DropDownList)row.FindControl("ddlGridDivision");
                    DropDownList ddlDivisionHeader = (DropDownList)gvStudent.HeaderRow.FindControl("ddlDivisionHeader");

                    ddlgridDivision.SelectedValue = ddlDivisionHeader.SelectedValue;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }

        }
        #endregion

        #region Row Bound Event
        protected void gvStudent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                DivisionTBL objDivisionBL = new DivisionTBL();
                ApplicationResult objResults = new ApplicationResult();
                Controls objControls = new Controls();
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                  
                    DropDownList ddlgridDivision = (DropDownList)e.Row.FindControl("ddlGridDivision");
                    objResults = objDivisionBL.Division_SelectAll_ClassWise(Convert.ToInt32(ViewState["ClassMID"].ToString()), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            objControls.BindDropDown_ListBox(objResults.resultDT, ddlgridDivision, "DivisionName", "DivisionTID");
                        }
                        ddlgridDivision.Items.Insert(0, new ListItem("-Select-", ""));
                    }
                }
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    DropDownList ddlDivisionHeader = (DropDownList)e.Row.FindControl("ddlDivisionHeader");

                    objResults = objDivisionBL.Division_SelectAll_ClassWise(Convert.ToInt32(ViewState["ClassMID"].ToString()), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            objControls.BindDropDown_ListBox(objResults.resultDT, ddlDivisionHeader, "DivisionName", "DivisionTID");
                        }
                        ddlDivisionHeader.Items.Insert(0, new ListItem("-Select-", ""));
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

        #region FetchSection
        private DataTable FetchSection(int intSchoolID)
        {
            // DataTable dtStudent = new DataTable();
            SectionBL objSectionBL = new SectionBL();
            SectionBO objSectionBO = new SectionBO();
            ApplicationResult objResults = new ApplicationResult();
            objResults = objSectionBL.Section_SelectAll_ForDropDown(intSchoolID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {

            }
            return objResults.resultDT;
        }
        #endregion

        #region FetchClass
        private DataTable FetchClass(int intSchoolMID)
        {
            // DataTable dtClass = new DataTable();
            ClassBL objClassBL = new ClassBL();
            ClassBO objClassBO = new ClassBO();
            ApplicationResult objResults = new ApplicationResult();

            objResults = objClassBL.Class_SelectAll_ForDropDownNotSectionWise(intSchoolMID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {

            }
            return objResults.resultDT;
        }
        #endregion

        #region Fetch Division
        private DataTable FetchDivision(int intClassMID, int intSchoolMID)
        {
            //DataTable dtDivision = new DataTable();
            DivisionTBL objDivisionBL = new DivisionTBL();
            DivisionTBO objDivisionBO = new DivisionTBO();
            ApplicationResult objResults = new ApplicationResult();

            objResults = objDivisionBL.Division_SelectAll_ClassWise_ForDropDown(intClassMID, intSchoolMID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {
            }
            return objResults.resultDT;
        }
        #endregion
    }
}