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
    public partial class UpgradeStudent : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(UpgradeStudent));

        #region Declare Global Variation
        int intMonth = 0;

        #endregion

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
                    DataTable dt1 = new DataTable();
                    dt1 = BindAcademicYear();
                    Controls objControl = new Controls();
                    bindGridStatus();
                    objControl.BindDropDown_ListBox(dt1, ddlAcademicYear, "AcademicYear", "AcademicYear");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "BindSection();", true);
                    foreach (GridViewRow row in gvStudent.Rows)
                    {
                        DropDownList txtDate = (DropDownList)row.FindControl("txtDate");
                        txtDate.Enabled = true;
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

        #region BindAcademicYear
        public DataTable BindAcademicYear()
        {
            //try
            //{
            DataTable dt = new DataTable();

            #region Fetch Academic Month from School

            SchoolBL objSchoolBl = new SchoolBL();
            ApplicationResult objResults = new ApplicationResult();


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

        #region Bind Status Master

        private void bindGridStatus()
        {
            try
            {
                StatusBL objStatusBL = new StatusBL();
                ApplicationResult objResults = new ApplicationResult();
                Controls objControls = new Controls();
                objResults = objStatusBL.Status_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                if (objResults != null)
                {
                    if (objResults.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResults.resultDT, ddlStatus, "StatusName", "StatusMasterID");
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
                objResult = objStudentBL.Student_SelectFor_Upgrade(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(ViewState["ClassMID"].ToString()), Convert.ToInt32(ViewState["DivisionName"].ToString()), ddlAcademicYear.SelectedItem.ToString(), Convert.ToInt32((ddlStatus.SelectedValue)));
                if (objResult != null)
                {
                    gvStudent.DataSource = objResult.resultDT;
                    gvStudent.DataBind();
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvStudent.Visible = true;

                        if (rblAcademicOrStatus.SelectedValue == "1" && Convert.ToInt32(ddlStatus.SelectedValue) == 4)
                        {

                            gvStudent.Visible = true;
                            gvStudent.Columns[7].Visible = false;
                            gvStudent.Columns[8].Visible = false;
                            gvStudent.Columns[9].Visible = false;
                            gvStudent.Columns[10].Visible = false;
                            gvStudent.Columns[11].Visible = false;
                            DropDownList ddlStatusHeader = (DropDownList)gvStudent.HeaderRow.FindControl("ddlStatusHeader");
                            foreach (GridViewRow row in gvStudent.Rows)
                            {
                                DropDownList ddlGridStatus = (DropDownList)row.FindControl("ddlStatus");
                                //ddlGridStatus.SelectedValue = "1";
                                ddlGridStatus.Enabled = true;
                            }

                            //ddlStatusHeader.SelectedValue = "1";

                            ddlStatusHeader.Enabled = true;

                        }
                        else if (rblAcademicOrStatus.SelectedValue == "1" && Convert.ToInt32(ddlStatus.SelectedValue) == 1)
                        {
                            gvStudent.Columns[7].Visible = false;
                            gvStudent.Columns[8].Visible = true;
                            gvStudent.Columns[9].Visible = true;
                            gvStudent.Columns[10].Visible = true;
                            gvStudent.Columns[11].Visible = false;
                            //   gvStudent.DataSource = objResult.resultDT;
                            //  gvStudent.DataBind();
                            gvStudent.Visible = true;
                            DropDownList ddlStatusHeader = (DropDownList)gvStudent.HeaderRow.FindControl("ddlStatusHeader");
                            foreach (GridViewRow row in gvStudent.Rows)
                            {
                                DropDownList ddlGridStatus = (DropDownList)row.FindControl("ddlStatus");

                                ddlGridStatus.Enabled = true;
                            }
                            ddlStatusHeader.Enabled = true;

                        }
                        else if (rblAcademicOrStatus.SelectedValue == "2" && Convert.ToInt32(ddlStatus.SelectedValue) == 1)
                        {
                            gvStudent.Columns[7].Visible = true;
                            gvStudent.Columns[8].Visible = false;
                            gvStudent.Columns[9].Visible = false;
                            gvStudent.Columns[10].Visible = false;
                            gvStudent.Columns[11].Visible = true;
                            //   gvStudent.DataSource = objResult.resultDT;
                            //  gvStudent.DataBind();
                            gvStudent.Visible = true;
                            DropDownList ddlStatusHeader = (DropDownList)gvStudent.HeaderRow.FindControl("ddlStatusHeader");
                            foreach (GridViewRow row in gvStudent.Rows)
                            {
                                DropDownList ddlGridStatus = (DropDownList)row.FindControl("ddlStatus");

                                ddlGridStatus.Enabled = true;
                            }
                            ddlStatusHeader.Enabled = true;

                        }
                        else if (rblAcademicOrStatus.SelectedValue == "2" && Convert.ToInt32(ddlStatus.SelectedValue) == 4)
                        {
                            gvStudent.Columns[7].Visible = false;
                            gvStudent.Columns[8].Visible = false;
                            gvStudent.Columns[9].Visible = false;
                            gvStudent.Columns[10].Visible = false;
                            gvStudent.Columns[11].Visible = false;
                            //gvStudent.DataSource = objResult.resultDT;
                            // gvStudent.DataBind();
                            gvStudent.Visible = true;
                            DropDownList ddlStatusHeader = (DropDownList)gvStudent.HeaderRow.FindControl("ddlStatusHeader");
                            foreach (GridViewRow row in gvStudent.Rows)
                            {
                                DropDownList ddlGridStatus = (DropDownList)row.FindControl("ddlStatus");
                                //ddlGridStatus.SelectedValue = "1";
                                ddlGridStatus.Enabled = true;
                            }

                            // ddlStatusHeader.SelectedValue = "1";

                            ddlStatusHeader.Enabled = true;
                        }
                        else if (rblAcademicOrStatus.SelectedValue == "2" && Convert.ToInt32(ddlStatus.SelectedValue) == 6)
                        {
                            gvStudent.Columns[7].Visible = false;
                            gvStudent.Columns[8].Visible = false;
                            gvStudent.Columns[9].Visible = false;
                            gvStudent.Columns[10].Visible = false;
                            gvStudent.Columns[11].Visible = false;
                            //  gvStudent.DataSource = objResult.resultDT;
                            //  gvStudent.DataBind();
                            gvStudent.Visible = true;
                            DropDownList ddlStatusHeader = (DropDownList)gvStudent.HeaderRow.FindControl("ddlStatusHeader");
                            foreach (GridViewRow row in gvStudent.Rows)
                            {
                                DropDownList ddlGridStatus = (DropDownList)row.FindControl("ddlStatus");

                                ddlGridStatus.Enabled = true;
                            }
                            ddlStatusHeader.Enabled = true;
                        }
                        else if (rblAcademicOrStatus.SelectedValue == "1" && Convert.ToInt32(ddlStatus.SelectedValue) == 6)
                        {
                            gvStudent.Columns[7].Visible = false;
                            gvStudent.Columns[8].Visible = false;
                            gvStudent.Columns[9].Visible = false;
                            gvStudent.Columns[10].Visible = false;
                            gvStudent.Columns[11].Visible = false;
                            //  gvStudent.DataSource = objResult.resultDT;
                            //  gvStudent.DataBind();
                            gvStudent.Visible = true;
                            DropDownList ddlStatusHeader = (DropDownList)gvStudent.HeaderRow.FindControl("ddlStatusHeader");
                            foreach (GridViewRow row in gvStudent.Rows)
                            {
                                DropDownList ddlGridStatus = (DropDownList)row.FindControl("ddlStatus");

                                ddlGridStatus.Enabled = true;
                            }
                            ddlStatusHeader.Enabled = true;

                        }
                        else if (rblAcademicOrStatus.SelectedValue == "2" && Convert.ToInt32(ddlStatus.SelectedValue) == 3)
                        {
                            gvStudent.Columns[7].Visible = false;
                            gvStudent.Columns[8].Visible = false;
                            gvStudent.Columns[9].Visible = false;
                            gvStudent.Columns[10].Visible = false;
                            gvStudent.Columns[11].Visible = false;
                            //  gvStudent.DataSource = objResult.resultDT;
                            //  gvStudent.DataBind();
                            gvStudent.Visible = true;
                        }
                        else if (rblAcademicOrStatus.SelectedValue == "2" && Convert.ToInt32(ddlStatus.SelectedValue) == 2)
                        {
                            gvStudent.Columns[7].Visible = false;
                            gvStudent.Columns[8].Visible = false;
                            gvStudent.Columns[9].Visible = false;
                            gvStudent.Columns[10].Visible = false;
                            gvStudent.Columns[11].Visible = false;
                            //  gvStudent.DataSource = objResult.resultDT;
                            //  gvStudent.DataBind();
                            gvStudent.Visible = true;
                        }
                        else if (rblAcademicOrStatus.SelectedValue == "1" && Convert.ToInt32(ddlStatus.SelectedValue) == 2)
                        {
                            gvStudent.Columns[7].Visible = false;
                            gvStudent.Columns[8].Visible = false;
                            gvStudent.Columns[9].Visible = false;
                            gvStudent.Columns[10].Visible = false;
                            gvStudent.Columns[11].Visible = false;
                            //  gvStudent.DataSource = objResult.resultDT;
                            //  gvStudent.DataBind();
                            gvStudent.Visible = true;
                        }
                        else if (rblAcademicOrStatus.SelectedValue == "1" && Convert.ToInt32(ddlStatus.SelectedValue) == 5)
                        {
                            gvStudent.Columns[7].Visible = false;
                            gvStudent.Columns[8].Visible = false;
                            gvStudent.Columns[9].Visible = false;
                            gvStudent.Columns[10].Visible = false;
                            gvStudent.Columns[11].Visible = false;
                            //  gvStudent.DataSource = objResult.resultDT;
                            //  gvStudent.DataBind();
                            gvStudent.Visible = true;
                        }
                        else if (rblAcademicOrStatus.SelectedValue == "2" && Convert.ToInt32(ddlStatus.SelectedValue) == 5)
                        {
                            gvStudent.Columns[7].Visible = false;
                            gvStudent.Columns[8].Visible = false;
                            gvStudent.Columns[9].Visible = false;
                            gvStudent.Columns[10].Visible = false;
                            gvStudent.Columns[11].Visible = false;
                            //  gvStudent.DataSource = objResult.resultDT;
                            //  gvStudent.DataBind();
                            gvStudent.Visible = true;
                        }
                        else if (rblAcademicOrStatus.SelectedValue == "1" && Convert.ToInt32(ddlStatus.SelectedValue) == 3)
                        {
                            gvStudent.Columns[7].Visible = false;
                            gvStudent.Columns[8].Visible = false;
                            gvStudent.Columns[9].Visible = false;
                            gvStudent.Columns[10].Visible = false;
                            gvStudent.Columns[11].Visible = false;
                            //   gvStudent.DataSource = objResult.resultDT;
                            //  gvStudent.DataBind();
                            gvStudent.Visible = true;

                        }
                        else
                        {
                            gvStudent.Columns[11].Visible = true;
                            gvStudent.Columns[7].Visible = true;
                            gvStudent.Columns[7].Visible = false;
                            gvStudent.Columns[9].Visible = false;
                            gvStudent.Columns[10].Visible = false;
                            // gvStudent.DataSource = objResult.resultDT;
                            //  gvStudent.DataBind();
                            gvStudent.Visible = true;
                            DropDownList ddlStatusHeader = (DropDownList)gvStudent.HeaderRow.FindControl("ddlStatusHeader");
                            foreach (GridViewRow row in gvStudent.Rows)
                            {
                                DropDownList ddlGridStatus = (DropDownList)row.FindControl("ddlStatus");

                                ddlGridStatus.Enabled = true;
                            }
                            ddlStatusHeader.Enabled = true;
                        }
                    }
                    else
                    {
                        gvStudent.Visible = false;
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('No record Found.');</script>");
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

        #region ViewGrid Button
        protected void btnViewGrid_Click(object sender, EventArgs e)
        {
            try
            {
                // btnSave.Enabled = false;
                // btnSave.Attributes.Add("bgcolor", "#3b5998");

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
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Save Event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string StatusName = "";

            try
            {
                DatabaseTransaction.OpenConnectionTransation();
                foreach (GridViewRow row in gvStudent.Rows)
                {
                    ViewState["ClassMID"] = Convert.ToInt32(Request.Form[ddlClass.UniqueID]);
                    ViewState["DivisionName"] = Convert.ToInt32(Request.Form[ddlDivision.UniqueID]);
                    ViewState["AcademicYear"] = ddlAcademicYear.SelectedItem.ToString();
                    ViewState["StudentMID"] = Convert.ToInt32(row.Cells[1].Text);
                    ViewState["StudentTID"] = Convert.ToInt32(row.Cells[2].Text);
                    StudentBL objStudentBL = new StudentBL();
                    StudentTBO objStudentTBO = new StudentTBO();
                    ApplicationResult objResults = new ApplicationResult();
                    DropDownList ddlgridclass = (DropDownList)row.Cells[7].FindControl("ddlGridviewClass");
                    DropDownList ddlgridStatus = (DropDownList)row.Cells[6].FindControl("ddlStatus");
                    DropDownList ddlgridDivision = (DropDownList)row.Cells[8].FindControl("ddlGridDivision");
                    DropDownList ddlgridYear = (DropDownList)row.Cells[9].FindControl("ddlGridYear");
                    TextBox txtDate = (TextBox)row.Cells[10].FindControl("txtDate");

                    //StatusBL objStatusBL = new StatusBL();
                    //objStudentTBO.StudentTID = Convert.ToInt32(ViewState["StudentTID"].ToString());
                    //objResults = objStatusBL.Status_Select(Convert.ToInt32(ddlgridStatus.SelectedValue), Convert.ToInt32(Session[ApplicationSession.TRUSTID]));

                    //if (objResults != null)
                    //{
                    //    if (objResults.resultDT.Rows.Count > 0)
                    //    {
                    //        StatusName = objResults.resultDT.Rows[0][StatusBO.STATUS_STATUSNAME].ToString();
                    //    }

                    //}
                    if (((CheckBox)row.FindControl("chkChild")).Checked)
                    {
                        // 4 for drop
                        TextBox date = row.FindControl("txtDate") as TextBox;
                        var FindDate = date.Text;
                        TextBox GrNo = row.FindControl("txtDate") as TextBox;
                        var FindGrNo = GrNo.Text;
                        TextBox RollNo = row.FindControl("txtDate") as TextBox;
                        var FindRollNo = RollNo.Text;
                        if (rblAcademicOrStatus.SelectedValue == "1" && rblAcademicOrStatus.SelectedValue == "3")
                        {
                            if (FindDate == "" && FindGrNo == "")
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                           "<script language='javascript'>alert('Enter GrNo / RollNo / Left Date of selected students.');</script>");
                                break;
                            }
                        }
                        else if (rblAcademicOrStatus.SelectedValue == "2")
                        {
                            if (FindDate == "" && FindGrNo == "" && FindRollNo == "")
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                           "<script language='javascript'>alert('Enter GrNo / RollNo / Left Date of selected students.');</script>");
                                break;
                            }
                        }
                        if (Convert.ToInt32(ddlStatus.SelectedValue) == 4)
                        {
                            StatusBL objStatusBL = new StatusBL();
                            objStudentTBO.StudentTID = Convert.ToInt32(ViewState["StudentTID"].ToString());
                            objResults = objStatusBL.Status_Select(Convert.ToInt32(ddlgridStatus.SelectedValue), Convert.ToInt32(Session[ApplicationSession.TRUSTID]));

                            if (objResults != null)
                            {
                                if (objResults.resultDT.Rows.Count > 0)
                                {
                                    StatusName = objResults.resultDT.Rows[0][StatusBO.STATUS_STATUSNAME].ToString();
                                }

                            }
                            if (ddlgridclass.SelectedValue != "-1" && ddlgridDivision.SelectedValue != "-1" &&
                                ddlgridStatus.SelectedValue != "-1" && ddlgridYear.SelectedValue != "-1")
                            {

                                //Status Change in Master inYear
                                objResults =
                                    objStudentBL.StudentM_Update_AtTimeOfUpgradeOfStatus(
                                        Convert.ToInt32(ViewState["StudentMID"].ToString()),
                                        Convert.ToInt32(ddlgridStatus.SelectedValue),
                                        Convert.ToInt32(Session[ApplicationSession.USERID]),
                                        DateTime.UtcNow.AddHours(5.5).ToString(), ((TextBox)row.FindControl("txtGridGrNo")).Text, "0","");
                                if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                                {

                                }
                                //Fetch SectionTid
                                ClassBL objClassBL = new ClassBL();
                                objResults = objClassBL.Class_Select(Convert.ToInt32(ddlgridclass.SelectedValue));

                                if (objResults != null)
                                {
                                    if (objResults.resultDT.Rows.Count > 0)
                                    {
                                        ViewState["SectionTID"] =
                                            objResults.resultDT.Rows[0][ClassBO.CLASS_SECTIONTID].ToString();
                                    }

                                }
                                //change in master in Class Section Division

                                objResults =
                                    objStudentBL.StudentM_Update_AtTimeOfUpgrade(
                                        Convert.ToInt32(ViewState["StudentMID"].ToString()),
                                        Convert.ToInt32(ViewState["SectionTID"].ToString()), Convert.ToInt32(ddlgridclass.SelectedValue),
                                        Convert.ToInt32(ddlgridDivision.SelectedValue), ddlgridYear.SelectedItem.ToString(), Convert.ToInt32(ddlgridStatus.SelectedValue),
                                        Convert.ToInt32(Session[ApplicationSession.USERID]),
                                        DateTime.UtcNow.AddHours(5.5).ToString(), ((TextBox)row.FindControl("txtGridGrNo")).Text);
                                if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                                {

                                }
                                //Status Change in Transaction
                                //objResults =
                                //    objStudentBL.StudentT_Update_AtTimeOfUpgrade(
                                //        Convert.ToInt32(ViewState["StudentTID"].ToString()),
                                //        Convert.ToInt32(ddlgridStatus.SelectedValue), StatusName,
                                //        Convert.ToInt32(Session[ApplicationSession.USERID]),
                                //        DateTime.UtcNow.AddHours(5.5).ToString(), ((TextBox)row.FindControl("txtGridGrNo")).Text);
                                //if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                                //{
                                //    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                //        "<script language='javascript'>alert('Students Status Changed Successfully.');</script>");
                                //}
                                //New ENtry In 
                                objStudentTBO.StudentMID = Convert.ToInt32(ViewState["StudentMID"].ToString());
                                objStudentTBO.ClassMID = Convert.ToInt32(ddlgridclass.SelectedValue);
                                objStudentTBO.Year = ddlgridYear.SelectedItem.ToString();
                                objStudentTBO.DivisionTID = Convert.ToInt32(ddlgridDivision.SelectedValue);
                                objStudentTBO.StatusMasterID = Convert.ToInt32(ddlgridStatus.SelectedValue);
                                objStudentTBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                                objStudentTBO.StatusName = StatusName;
                                objStudentTBO.GrNo = ((TextBox)row.FindControl("txtGridGrNo")).Text;
                                objStudentTBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);

                                objResults = objStudentBL.StudentT_Insert(objStudentTBO);

                                if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                        "<script language='javascript'>alert('Students Status Changed Successfully.');</script>");
                                }

                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Please Select Class,Division,Status and Academic Year of Selected Student.');</script>");
                            }
                        }
                        else if (Convert.ToInt32(ddlStatus.SelectedValue) == 3)
                        {
                            // change only left Date of Student id Search Status is Left
                            objResults =
                                          objStudentBL.StudentM_UpgradeToLeft(
                                              Convert.ToInt32(ViewState["StudentMID"].ToString()),
                                              0,
                                              Convert.ToInt32(Session[ApplicationSession.USERID]),
                                              DateTime.UtcNow.AddHours(5.5).ToString(), txtDate.Text);
                            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {

                            }
                        }


                        else
                        {
                            if (ddlgridclass.SelectedValue != "-1")
                            {
                                if (ddlgridclass.SelectedValue != "-1" && ddlgridDivision.SelectedValue != "-1")
                                {
                                    if (ViewState["ClassMID"].ToString() == ddlgridclass.SelectedValue)
                                    {
                                        ApplicationResult objStatusResult = objStudentBL.Student_GetStatus(Convert.ToInt32(ViewState["StudentMID"].ToString()), ddlAcademicYear.SelectedValue);
                                        if(objStatusResult != null)
                                        {
                                            if(objStatusResult.resultDT.Rows.Count > 0)
                                            {
                                                if(Convert.ToInt32(objStatusResult.resultDT.Rows[0]["StatusMasterID"].ToString()) == 6)
                                                {

                                                    ApplicationResult objUpdateStatusResult = objStudentBL.Student_UpdateStatus(Convert.ToInt32(ViewState["StudentMID"].ToString()), ddlAcademicYear.SelectedValue, Convert.ToInt32(Session[ApplicationSession.USERID]), DateTime.UtcNow.AddHours(5.5).ToString());
                                                    if (objUpdateStatusResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                                                    {
                                                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                                            "<script language='javascript'>alert('Students Class Transfered Successfully.');</script>");
                                                    }
                                                }
                                                else
                                                {
                                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                                        "<script language='javascript'>alert('Student are not upgrade into the Same Class.');</script>");
                                                }
                                            }
                                        }
                                        
                                    }
                                    else
                                    {
                                        //string str3 = ddlAcademicYear.SelectedItem.ToString().Substring(3);
                                        //string Year = (str3) + "-" + (Convert.ToInt32(str3) + 1);
                                        objStudentTBO.StudentMID = Convert.ToInt32(ViewState["StudentMID"].ToString());
                                        objStudentTBO.ClassMID = Convert.ToInt32(ddlgridclass.SelectedValue);
                                        objStudentTBO.Year = ddlgridYear.SelectedItem.ToString();
                                        objStudentTBO.DivisionTID = Convert.ToInt32(ddlgridDivision.SelectedValue);
                                        objStudentTBO.StatusMasterID = 1;
                                        objStudentTBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                                        objStudentTBO.StatusName = "Running";
                                        objStudentTBO.GrNo = ((TextBox)row.FindControl("txtGridGrNo")).Text;
                                        objStudentTBO.LastModifiedUserID =
                                            Convert.ToInt32(Session[ApplicationSession.USERID]);

                                        objResults = objStudentBL.StudentT_Insert(objStudentTBO);

                                        if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                                        {

                                            ClassBL objClassBL = new ClassBL();
                                            objResults = objClassBL.Class_Select(objStudentTBO.ClassMID);

                                            if (objResults != null)
                                            {
                                                if (objResults.resultDT.Rows.Count > 0)
                                                {
                                                    ViewState["SectionTID"] =
                                                        objResults.resultDT.Rows[0][ClassBO.CLASS_SECTIONTID].ToString();
                                                }

                                            }
                                            objResults =
                                                objStudentBL.StudentM_Update_AtTimeOfUpgrade(
                                                    Convert.ToInt32(ViewState["StudentMID"].ToString()),
                                                    Convert.ToInt32(ViewState["SectionTID"].ToString()),
                                                    objStudentTBO.ClassMID, objStudentTBO.DivisionTID,
                                                    ddlgridYear.SelectedItem.ToString(), 1,
                                                    Convert.ToInt32(Session[ApplicationSession.USERID]),
                                                    DateTime.UtcNow.AddHours(5.5).ToString(), objStudentTBO.GrNo);
                                            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                                            {

                                            }

                                            objStudentTBO.StudentTID =
                                                Convert.ToInt32(ViewState["StudentTID"].ToString());
                                            objResults =
                                                objStudentBL.StudentT_Update_AtTimeOfUpgrade(
                                                    Convert.ToInt32(ViewState["StudentTID"].ToString()), 2, "Completed",
                                                    Convert.ToInt32(Session[ApplicationSession.USERID]),
                                                    DateTime.UtcNow.AddHours(5.5).ToString(), objStudentTBO.GrNo);
                                            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                                            {
                                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                                    "<script language='javascript'>alert('Students Class Transfered Successfully.');</script>");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                                   "<script language='javascript'>alert('Please Select Class,Division and Academic Year of Selected Student..');</script>");
                                }
                            }
                            else
                            {
                                StatusBL objStatusBL = new StatusBL();
                                objStudentTBO.StudentTID = Convert.ToInt32(ViewState["StudentTID"].ToString());
                                objResults = objStatusBL.Status_Select(Convert.ToInt32(ddlgridStatus.SelectedValue), Convert.ToInt32(Session[ApplicationSession.TRUSTID]));

                                if (objResults != null)
                                {
                                    if (objResults.resultDT.Rows.Count > 0)
                                    {
                                        StatusName = objResults.resultDT.Rows[0][StatusBO.STATUS_STATUSNAME].ToString();
                                    }

                                }

                                // 6 for fail , 3 for Left, 4 for Drop, 5 for cancelled
                                //
                                if (ddlgridStatus.SelectedValue != "-1")
                                {
                                    if (Convert.ToInt32(ddlgridStatus.SelectedValue) == 6)
                                    {
                                        //update  Year in Student Master and Status in Old Entry of Student Transaction and New Entry in Student Transaction for Status
                                        string str3 = ddlAcademicYear.SelectedItem.ToString().Substring(3);
                                        string Year = (str3) + "-" + (Convert.ToInt32(str3) + 1);
                                        //Update Year in Master
                                        objResults =
                                            objStudentBL.StudentM_Update_AtTimeOfUpgradeOfStatus(
                                                Convert.ToInt32(ViewState["StudentMID"].ToString()),
                                                Convert.ToInt32(ddlStatus.SelectedValue),
                                                Convert.ToInt32(Session[ApplicationSession.USERID]),
                                                DateTime.UtcNow.AddHours(5.5).ToString(), ((TextBox)row.FindControl("txtGridGrNo")).Text,
                                               Year,"");
                                        if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                                        {

                                        }
                                        // Update Status in Old Entry
                                        objResults =
                                            objStudentBL.StudentT_Update_AtTimeOfUpgrade(
                                                Convert.ToInt32(ViewState["StudentTID"].ToString()),
                                                Convert.ToInt32(ddlgridStatus.SelectedValue), StatusName,
                                                Convert.ToInt32(Session[ApplicationSession.USERID]),
                                                DateTime.UtcNow.AddHours(5.5).ToString(), ((TextBox)row.FindControl("txtGridGrNo")).Text);
                                        if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                                        {
                                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                                "<script language='javascript'>alert('Students Status Changed Successfully.');</script>");
                                        }
                                        //Insert in Student T
                                        objStudentTBO.StudentMID = Convert.ToInt32(ViewState["StudentMID"].ToString());
                                        objStudentTBO.ClassMID = Convert.ToInt32(Request.Form[ddlClass.UniqueID]);
                                        objStudentTBO.Year = Year;
                                        objStudentTBO.DivisionTID = Convert.ToInt32(Request.Form[ddlDivision.UniqueID]);
                                        objStudentTBO.StatusMasterID = 1;
                                        objStudentTBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                                        objStudentTBO.StatusName = "Running";
                                        objStudentTBO.LastModifiedUserID =
                                            Convert.ToInt32(Session[ApplicationSession.USERID]);

                                        objResults = objStudentBL.StudentT_Insert(objStudentTBO);
                                        if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                                        {

                                        }
                                    }
                                    else
                                    {
                                        //Status Change in Master
                                        objResults =
                                            objStudentBL.StudentM_Update_AtTimeOfUpgradeOfStatus(
                                                Convert.ToInt32(ViewState["StudentMID"].ToString()),
                                                Convert.ToInt32(ddlgridStatus.SelectedValue),
                                                Convert.ToInt32(Session[ApplicationSession.USERID]),
                                                DateTime.UtcNow.AddHours(5.5).ToString(), objStudentTBO.GrNo, "0", ((TextBox)row.FindControl("txtDate")).Text);
                                        if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                                        {

                                        }
                                        //Status Change in Transaction
                                        objResults =
                                            objStudentBL.StudentT_Update_AtTimeOfUpgrade(
                                                Convert.ToInt32(ViewState["StudentTID"].ToString()),
                                                Convert.ToInt32(ddlgridStatus.SelectedValue), StatusName,
                                                Convert.ToInt32(Session[ApplicationSession.USERID]),
                                                DateTime.UtcNow.AddHours(5.5).ToString(), objStudentTBO.GrNo);
                                        if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                                        {
                                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                                "<script language='javascript'>alert('Students Status Changed Successfully.');</script>");
                                        }
                                    }
                                }
                                else if (ddlgridStatus.SelectedValue != "-1" && ddlgridYear.SelectedValue != "-1" && txtDate.Text != "")
                                {
                                    if (Convert.ToInt32(ddlgridStatus.SelectedValue) == 3)
                                    {
                                        //Status Change in Master
                                        objResults =
                                            objStudentBL.StudentM_UpgradeToLeft(
                                                Convert.ToInt32(ViewState["StudentMID"].ToString()),
                                                Convert.ToInt32(ddlgridStatus.SelectedValue),
                                                Convert.ToInt32(Session[ApplicationSession.USERID]),
                                                DateTime.UtcNow.AddHours(5.5).ToString(), txtDate.Text);
                                        if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                                        {

                                        }
                                        //Status Change in Transaction
                                        objResults =
                                            objStudentBL.StudentT_Update_AtTimeOfUpgrade(
                                                Convert.ToInt32(ViewState["StudentTID"].ToString()),
                                                Convert.ToInt32(ddlgridStatus.SelectedValue), StatusName,
                                                Convert.ToInt32(Session[ApplicationSession.USERID]),
                                                DateTime.UtcNow.AddHours(5.5).ToString(), objStudentTBO.GrNo);
                                        if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                                        {
                                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                                "<script language='javascript'>alert('Students Status Changed Successfully.');</script>");
                                        }
                                    }
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Please Select Status and Academic Year of Selected Student.');</script>");
                                }
                                //
                            }
                        }
                    }

                }
                DatabaseTransaction.CommitTransation();
                BindStudentGrid();
                gvStudent.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "BindSection();", true);

            }
            catch (Exception ex)
            {
                DatabaseTransaction.RollbackTransation();
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region FetchSection
        public DataTable FetchSection(int intSchoolID)
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

        #region Row Bound Event
        protected void gvStudent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                StatusBL objStatusBL = new StatusBL();
                ClassBL objClassBL = new ClassBL();
                ApplicationResult objResults = new ApplicationResult();
                Controls objControls = new Controls();
                DataTable dt1 = new DataTable();
                dt1 = BindAcademicYear();

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                    DropDownList ddlClass = (DropDownList)e.Row.FindControl("ddlGridviewClass");
                    DropDownList ddlgridDivision = (DropDownList)e.Row.FindControl("ddlGridDivision");
                    DropDownList ddlgridYear = (DropDownList)e.Row.FindControl("ddlGridYear");


                    objResults = objStatusBL.Status_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            objControls.BindDropDown_ListBox(objResults.resultDT, ddlStatus, "StatusName", "StatusMasterID");
                        }
                        ddlStatus.Items.Insert(0, new ListItem("-Select-", "-1"));
                        ddlgridDivision.Items.Insert(0, new ListItem("-Select-", "-1"));
                    }
                    objResults = objClassBL.Class_SelectAll(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            objControls.BindDropDown_ListBox(objResults.resultDT, ddlClass, "ClassName", "ClassMID");
                        }
                        ddlClass.Items.Insert(0, new ListItem("-Select-", "-1"));
                    }
                    objControls.BindDropDown_ListBox(dt1, ddlgridYear, "AcademicYear", "AcademicYear");
                    ddlgridYear.Items.Insert(0, new ListItem("-Select-", "-1"));
                }
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    DropDownList ddlStatusHeader = (DropDownList)e.Row.FindControl("ddlStatusHeader");
                    DropDownList ddlClassHeader = (DropDownList)e.Row.FindControl("ddlClassHeader");
                    DropDownList ddlDivisionHeader = (DropDownList)e.Row.FindControl("ddlDivisionHeader");
                    DropDownList ddlYearHeader = (DropDownList)e.Row.FindControl("ddlYearHeader");

                    objResults = objStatusBL.Status_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            objControls.BindDropDown_ListBox(objResults.resultDT, ddlStatusHeader, "StatusName", "StatusMasterID");
                        }
                        ddlStatusHeader.Items.Insert(0, new ListItem("-Status-", ""));
                    }
                    objResults = objClassBL.Class_SelectAll(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            objControls.BindDropDown_ListBox(objResults.resultDT, ddlClassHeader, "ClassName", "ClassMID");
                        }
                        ddlClassHeader.Items.Insert(0, new ListItem("-Class-", ""));
                        ddlDivisionHeader.Items.Insert(0, new ListItem("-Division-", ""));
                    }
                    objControls.BindDropDown_ListBox(dt1, ddlYearHeader, "AcademicYear", "AcademicYear");
                    ddlYearHeader.Items.Insert(0, new ListItem("-Year-", ""));

                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region class Header Dropdown Change SelectedIndexChange
        protected void ddlClassHeader_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DivisionTBL objDivisionBL = new DivisionTBL();
                DivisionTBO objDivisionBO = new DivisionTBO();
                ApplicationResult objResults = new ApplicationResult();
                Controls objControls = new Controls();


                for (int i = 0; i < gvStudent.Rows.Count; i++)
                {
                    DropDownList ddlgridclass = (DropDownList)gvStudent.Rows[i].Cells[7].FindControl("ddlGridviewClass");
                    DropDownList ddlClassHeader = (DropDownList)gvStudent.HeaderRow.FindControl("ddlClassHeader");
                    DropDownList ddlgridDivision = (DropDownList)gvStudent.Rows[i].Cells[8].FindControl("ddlGridDivision");
                    DropDownList ddlDivisionHeader = (DropDownList)gvStudent.HeaderRow.FindControl("ddlDivisionHeader");

                    //ddlgridclass.SelectedItem.Text = ddlClassHeader.SelectedItem.Text;
                    ddlgridclass.SelectedValue = ddlClassHeader.SelectedValue;


                    objResults = objDivisionBL.Division_SelectAll_ClassWise(Convert.ToInt32(ddlClassHeader.SelectedValue), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            objControls.BindDropDown_ListBox(objResults.resultDT, ddlgridDivision, "DivisionName", "DivisionTID");
                            objControls.BindDropDown_ListBox(objResults.resultDT, ddlDivisionHeader, "DivisionName", "DivisionTID");
                        }
                        ddlgridDivision.Items.Insert(0, new ListItem("-Select-", ""));
                        ddlDivisionHeader.Items.Insert(0, new ListItem("-Select-", ""));

                        // objControls.BindDropDown_ListBox(dt, ddlAcademicYear, "AcademicYear", "AcademicYear");
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

        #region Year Header Dropdown Change SelectedIndexChange

        protected void ddlYearHeader_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gvStudent.Rows)
                {
                    DropDownList ddlgridYear = (DropDownList)row.FindControl("ddlGridYear");
                    DropDownList ddlYearHeader = (DropDownList)gvStudent.HeaderRow.FindControl("ddlYearHeader");

                    ddlgridYear.SelectedValue = ddlYearHeader.SelectedValue;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Status Header Dropdown Change SelectedIndexChange
        protected void ddlStatusHeader_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gvStudent.Rows)
                {
                    DropDownList ddlgridStatus = (DropDownList)row.FindControl("ddlStatus");
                    DropDownList ddlStatusHeader = (DropDownList)gvStudent.HeaderRow.FindControl("ddlStatusHeader");

                    ddlgridStatus.SelectedValue = ddlStatusHeader.SelectedValue;
                    gvStudent.Columns[11].Visible = true;
                    gvStudent.Columns[7].Visible = true;
                    gvStudent.Columns[8].Visible = false;
                    gvStudent.Columns[9].Visible = false;
                    gvStudent.Columns[10].Visible = false;
                    //if (ddlStatusHeader.SelectedValue == "3")
                    //{
                    //    gvStudent.Columns[10].Visible = true;
                    //    gvStudent.Columns[6].Visible = true;
                    //    gvStudent.Columns[7].Visible = false;
                    //    gvStudent.Columns[8].Visible = false;
                    //    gvStudent.Columns[9].Visible = true;
                    //}
                    //else
                    //{
                    //    gvStudent.Columns[10].Visible = false;
                    //    gvStudent.Columns[6].Visible = true;
                    //    gvStudent.Columns[7].Visible = false;
                    //    gvStudent.Columns[8].Visible = false;
                    //    gvStudent.Columns[9].Visible = false;
                    //}
                }
            }
            catch (Exception ex)
            {
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

        #region Class Dropdown Change SelectedIndexChange
        protected void ddlGridviewClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DivisionTBL objDivisionBL = new DivisionTBL();
                DivisionTBO objDivisionBO = new DivisionTBO();
                ApplicationResult objResults = new ApplicationResult();
                Controls objControls = new Controls();


                GridViewRow gvr = (GridViewRow)((DropDownList)sender).Parent.Parent;
                DropDownList ddl = (DropDownList)sender;

                DropDownList ddlGridDivision = gvr.FindControl("ddlGridDivision") as DropDownList;

                objResults = objDivisionBL.Division_SelectAll_ClassWise(Convert.ToInt32(ddl.SelectedValue), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResults != null)
                {
                    if (objResults.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResults.resultDT, ddlGridDivision, "DivisionName", "DivisionTID");

                    }
                    ddlGridDivision.Items.Insert(0, new ListItem("-Select-", ""));

                }
                // So in this you can get second drodpdown and bind your data
                //dsCust = Convert.ToInt32(ddl.SelectedValue);
                //ddlsecond.DataSource = dsCust.Tables[0];
                //ddlsecond.DataTextField = "CustName";
                //ddlsecond.DataValueField = "ID";
                //ddlsecond.DataBind();



            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }

        }
        #endregion

        #region UnUsed Code
        //Check The Grid Dropdown Value
        //string lklk =ddlgridclass.SelectedItem.ToString();


        //string classid = (gvStudent.Rows[i].FindControl("ddlGridviewClass") as DropDownList).SelectedItem.Value;
        //string classid1 = (Request.Form[ddlgridclass.UniqueID]).ToString();
        //string classid2 = (Request.Form[hfGridClassID.Value]);
        //string classid3 = hfGridClassID.Value;


        //var currRow = gvStudent.Rows[i];


        //var selectedIndex = gvStudent.Rows[i].Cells[5].Text;
        //string classname = currRow.Cells[5].Options[selectedIndex].text;

        // End Dropdown Check
        #endregion

        #region Status Dropdown Change SelectedIndexChange
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gvStudent.Rows)
                {
                    DropDownList ddlgridStatus = (DropDownList)row.FindControl("ddlStatus");
                    gvStudent.Columns[11].Visible = true;
                    gvStudent.Columns[7].Visible = true;
                    gvStudent.Columns[8].Visible = false;
                    gvStudent.Columns[9].Visible = false;
                    gvStudent.Columns[10].Visible = false;
                    //if (ddlgridStatus.SelectedValue == "3")
                    //{
                    //    gvStudent.Columns[10].Visible = true;
                    //    gvStudent.Columns[6].Visible = true;
                    //    gvStudent.Columns[7].Visible = false;
                    //    gvStudent.Columns[8].Visible = false;
                    //    gvStudent.Columns[9].Visible = true;
                    //}
                    //else
                    //{
                    //    gvStudent.Columns[10].Visible = false;
                    //    gvStudent.Columns[6].Visible = true;
                    //    gvStudent.Columns[7].Visible = false;
                    //    gvStudent.Columns[8].Visible = false;
                    //    gvStudent.Columns[8].Visible = false;
                    //}
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }

        }
        #endregion

        #region Method for Alert
        public void MyItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    string strName;
                    try
                    {
                        foreach (GridViewRow row in gvStudent.Rows)
                        {
                            strName = Convert.ToInt32(row.Cells[5].Text) + ",";

                        }
                        //Button myButton = (Button)e.Item.FindControl("MyButton");
                        //myButton.Attributes.Add("onclick",
                        //   "return confirm('Are you sure you want to delete "
                        //    + e.Item.DataItem.ToString() + "?');");
                    }
                    catch { }
                    break;
            }
        }

        #endregion

      
    }
}