using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.DataAccess;
using System.Web.Script.Serialization;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using System.Data;

namespace GEIMS.Client.UI
{
    public partial class ClasswiseStudentTemplate : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(ClasswiseStudentTemplate));

        #region Page_Load
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
                    TextBox1.Text = "";
                    ViewState["Mode"] = "Save";
                    ViewState["ClassTemplateMID"] = 0;
                    BindAcademicYear();
                    // btnCancel.Visible = false;
                    btnSave.Visible = false;
                    hfTab.Value = "0";
                    divStName.Visible = false;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "BindClass();", true);
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
        }
        #endregion

        #region View Button Click Event
        protected void btnViewGrid_Click(object sender, EventArgs e)
        {
            try
            {
                divStName.Visible = true;
                //  btnSave.Enabled = false;
                //  btnSave.Attributes.Add("bgcolor", "#848484");
                Controls objControls = new Controls();
                ViewState["ClassMID"] = Convert.ToInt32(Request.Form[ddlClass.UniqueID]);
                ViewState["DivisionName"] = Convert.ToInt32(Request.Form[ddlDivision.UniqueID]);
                ViewState["AcademicYear"] = ddlAcademicYear.SelectedItem.ToString();
                hfCLassMID.Value = ViewState["ClassMID"].ToString();
                hfDivisionTID.Value = ViewState["DivisionName"].ToString();
                //BindGridView();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "BindDorpdownOnButtonClick();",
                    true);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Save Event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // divLoading.Visible = true;
                StudentFeesTemplateTBL objStudentFeesTemplateTBL = new StudentFeesTemplateTBL();
                StudentFeesTemplateTBO objStudentFeesTemplateTBO = new StudentFeesTemplateTBO();
                #region RollBack Transaction Starts

                DatabaseTransaction.OpenConnectionTransation();
                int k = 0;
                int intCount = 0;

                var objResult =
                    objStudentFeesTemplateTBL.StudentFeesTemplateT_Delete_ForInsert(Convert.ToInt32(hfSearchID.Value),
                        ddlAcademicYear.SelectedValue);
                if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                {
                }


                foreach (GridViewRow row in gvFees.Rows)
                {

                    ViewState["FeesCategoryMID"] = Convert.ToInt32(row.Cells[0].Text);
                    objStudentFeesTemplateTBO.ClassWiseFeesTemplateTID = Convert.ToInt32(row.Cells[1].Text);
                    objStudentFeesTemplateTBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                    objStudentFeesTemplateTBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                    objStudentFeesTemplateTBO.ClassMID = Convert.ToInt32(ViewState["ClassMID"].ToString());
                    objStudentFeesTemplateTBO.DivisionTID = Convert.ToInt32(ViewState["DivisionName"].ToString());
                    objStudentFeesTemplateTBO.FeesAmount =
                        Convert.ToDouble(((TextBox)row.FindControl("txtFeesAmount")).Text);
                    objStudentFeesTemplateTBO.FeesCategoryMID = Convert.ToInt32(row.Cells[0].Text);
                    objStudentFeesTemplateTBO.StudentMID = Convert.ToInt32(hfSearchID.Value);
                    objStudentFeesTemplateTBO.AcademicYear = ddlAcademicYear.SelectedItem.Text;
                    objStudentFeesTemplateTBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    objStudentFeesTemplateTBO.LastModifiedUserID =
                        Convert.ToInt32(Session[ApplicationSession.USERID]);
                    objStudentFeesTemplateTBO.IsLate = 1;


                    //DataTable Dt = Select_ClassTemlate_FeeCategory();

                    if (((CheckBox)row.FindControl("chkChild")).Checked)
                    {
                        intCount += 1;
                        if (objStudentFeesTemplateTBO.FeesAmount == 0.0)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                "<script language='javascript'>alert('Please Fill Fees Amount " + row.Cells[0].Text +
                                ".');</script>");
                            break;
                        }
                        else
                        {
                            ApplicationResult objResultsInsert = new ApplicationResult();

                            objResultsInsert =
                                objStudentFeesTemplateTBL.StudentFeesTemplateT_Insert(objStudentFeesTemplateTBO);
                            if (objResultsInsert != null)
                            {
                                k += 1;
                            }
                        }
                    }
                }

                if (k == intCount)
                {
                    DatabaseTransaction.CommitTransation();
                }
                else
                {
                    DatabaseTransaction.RollbackTransation();
                    //DatabaseTransaction.connection.Close();
                }

                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Fees Amount Successfully Saved.');</script>");
                #endregion
                ViewState["ClassMID"] = 0;
                ViewState["DivisionName"] = 0;
                ViewState["AcademicYear"] = "";
                hfCLassMID.Value = "0";
                hfDivisionTID.Value = "0";
                gvFees.Visible = false;
                ClearAll();

                //  Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "BindClass();", true);
                // divLoading.Visible = false;
                //  Response.Redirect("Class_Template.aspx");

            }
            catch (Exception ex)
            {
                DatabaseTransaction.RollbackTransation();
                DatabaseTransaction.connection.Close();
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region BindFeesGrid
        public void BindGridView()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                StudentFeesTemplateTBL objGridFeesBL = new StudentFeesTemplateTBL();
                var a = ddlDivision.SelectedItem;
                var b = ddlDivision.SelectedValue;



                objResult = objGridFeesBL.StudentFeesTemplateT_SelectAll_For_ClassTemplate(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(Request.Form[ddlClass.UniqueID]), Convert.ToInt32(Request.Form[ddlDivision.UniqueID]), ddlAcademicYear.SelectedValue);
                if (objResult != null)
                {
                    gvFees.Visible = true;
                    gvFees.DataSource = objResult.resultDT;
                    gvFees.DataBind();

                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        //((CheckBox)gvFees.HeaderRow.FindControl("chkHeader")).Enabled = true;
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('No Record Found.');</script>");
                        gvFees.Visible = false;
                    }

                    foreach (GridViewRow row in gvFees.Rows)
                    {

                        ViewState["FeesCategoryMID"] = Convert.ToInt32(row.Cells[0].Text);
                        DataTable Dt = Select_ClassTemlate_FeeCategory();


                        //if (Dt.Rows.Count > 0)
                        //{
                        //    ViewState["ClassTemplateTID"] = Convert.ToInt32(Dt.Rows[0][ClassWiseFeesTemplateTBO.CLASSWISEFEESTEMPLATET_CLASSWISEFEESTEMPLATETID].ToString());
                        //    //((CheckBox)row.FindControl("chkChild")).Checked = true;
                        //    //((CheckBox)gvFees.HeaderRow.FindControl("chkHeader")).Enabled = false;
                        //    btnSave.Enabled = true;
                        //    // btnSave.BackColor = Color.#3b5998;
                        //    btnSave.Attributes.Add("bgcolor", "#3b5998");
                        //    ((TextBox)row.FindControl("txtFeesAmount")).Text = Dt.Rows[0][ClassWiseFeesTemplateTBO.CLASSWISEFEESTEMPLATET_FEESAMOUNT].ToString();

                        //    DataTable dtFeeCollection = ValidateFeesbyFeesCollection(Convert.ToInt32(ViewState["ClassTemplateTID"].ToString()), Convert.ToInt32(ViewState["ClassMID"].ToString()), Convert.ToInt32(ViewState["DivisionName"].ToString()), ddlAcademicYear.SelectedItem.ToString(), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), 0);
                        //    if (dtFeeCollection.Rows.Count > 0)
                        //    {
                        //        //ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='This Fee Category is Not Update/Delete.');</script>");
                        //        //foreach (GridViewRow rowData in gvFees.Rows)
                        //        //{
                        //        //((CheckBox)row.FindControl("chkChild")).Enabled = false;
                        //        ((TextBox)row.FindControl("txtFeesAmount")).Enabled = false;
                        //        //}
                        //        btnSave.Enabled = true;
                        //        btnSave.Attributes.Add("bgcolor", "#3b5998");
                        //        // ((CheckBox)row.FindControl("chkHeader")).Visible = false;
                        //    }

                        //}

                    }
                }
                objResult = objGridFeesBL.StudentFeesTemplateT_Select(Convert.ToInt32(hfSearchID.Value), ddlAcademicYear.SelectedValue);
                if (objResult != null)
                {
                    if (objResult.resutlDS.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < objResult.resutlDS.Tables[0].Rows.Count; i++)
                        {
                            var Id = objResult.resutlDS.Tables[0].Rows[i]["FeesCategoryMID"].ToString();
                            var Amount = objResult.resutlDS.Tables[0].Rows[i]["FeesAmount"].ToString();
                            foreach (GridViewRow row in gvFees.Rows)
                            {
                                if (row.Cells[0].Text == Id)
                                {
                                    ((CheckBox)row.FindControl("chkChild")).Checked = true;
                                    Convert.ToDouble(((TextBox)row.FindControl("txtFeesAmount")).Text = Amount);
                                }
                            }
                        }
                        for (int i = 0; i < objResult.resutlDS.Tables[1].Rows.Count; i++)
                        {
                            var Id = objResult.resutlDS.Tables[1].Rows[i]["FeesCategoryMID"].ToString();
                            foreach (GridViewRow row in gvFees.Rows)
                            {
                                if (row.Cells[0].Text == Id)
                                {
                                    ((CheckBox)row.FindControl("chkChild")).Enabled = false;
                                }
                            }
                        }
                    }
                }
                btnSave.Visible = true;
                //btnCancel.Visible = true;

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
                ddlAcademicYear.Items.Insert(0, new ListItem("-Select-", "-1"));
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");

            }
        }
        #endregion

        #region Authorise If in Fee Collection
        public DataTable ValidateFeesbyFeesCollection(int intClassTemplateTID, int intClassMId, int intDivisionTID, string strAcademicYear, int intSchoolMID, int intStudentMID)
        {
            ApplicationResult objResults = new ApplicationResult();
            ClassWiseFeesTemplateTBL objClassTemplateBL = new ClassWiseFeesTemplateTBL();

            objResults = objClassTemplateBL.ClassWiseFeesTemplateT_Fee_Collection_M(intClassTemplateTID, intClassMId, intDivisionTID, strAcademicYear, intSchoolMID, intStudentMID);

            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {
            }
            return objResults.resultDT;

        }
        #endregion

        #region Webservice

        [System.Web.Services.WebMethod]
        public static string LoadClass(int intSchoolMID)
        {
            try
            {
                #region Bind Class
                DataTable dtClass = new DataTable();
                ClassBL objClassBL = new ClassBL();
                ClassBO objClassBO = new ClassBO();

                ApplicationResult objResultSection = new ApplicationResult();
                objResultSection = objClassBL.Class_SelectAll_ForDropDownNotSectionWise(intSchoolMID);
                if (objResultSection != null)
                {
                    dtClass = objResultSection.resultDT;
                    if (dtClass.Rows.Count > 0)
                    {

                    }

                }
                string res = "";
                res = DataSetToJSON(dtClass);
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

        #region FetchClass
        private DataTable FetchClass(int intSchoolMID)
        {
            // DataTable dtClass = new DataTable();
            ClassBL objClassBL = new ClassBL();
            ClassBO objClassBO = new ClassBO();
            ApplicationResult objResults = new ApplicationResult();

            objResults = objClassBL.Class_SelectAll(intSchoolMID);
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

            objResults = objDivisionBL.Division_SelectAll_ClassWise(intClassMID, intSchoolMID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {
            }
            return objResults.resultDT;
        }
        #endregion

        #region Fetching Datatable ClassTemlate_FeeCategory_M
        public DataTable Select_ClassTemlate_FeeCategory()
        {
            StudentFeesTemplateTBL objStudentFeesTemplateTBL = new StudentFeesTemplateTBL();
            //ClassWiseFeesTemplateTBL objClassTemplateBL = new ClassWiseFeesTemplateTBL();


            ApplicationResult objResultsSelectT = new ApplicationResult();
            objResultsSelectT = objStudentFeesTemplateTBL.StudentFeeTemplate_Select_ClassWiseStudentTemplate(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(hfCLassMID.Value), Convert.ToInt32(hfDivisionTID.Value), ddlAcademicYear.SelectedValue, 1, Convert.ToInt32(hfSearchID.Value));
            if (objResultsSelectT != null)
            {
            }
            return objResultsSelectT.resultDT;
        }
        #endregion

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            ViewState["Mode"] = "Save";
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            gvFees.Visible = false;
            btnSave.Visible = false;
            // btnCancel.Visible = false;
            hfTab.Value = "0";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Clear();", true);
        }
        #endregion

        #region Get Student Web Service Method
        [System.Web.Services.WebMethod]
        public static string[] GetAllStudentNameForReport(string prefixText, int SchoolMID, int ClassMID, int DivisionTID, string AcademicYear)
        {
            StudentBL objEmployeeMbl = new StudentBL();
            ApplicationResult objResult = new ApplicationResult();
            string strSearchText = prefixText + "%";
            List<string> result = new List<string>();
            objResult = objEmployeeMbl.Student_ForAutocomplete_ForClasswiseStudentTemplate(ClassMID, DivisionTID, SchoolMID, AcademicYear, prefixText);
            if (objResult != null)
            {
                if (objResult.resultDT.Rows.Count > 0)
                {
                    for (int i = 0; i < objResult.resultDT.Rows.Count; i++)
                    {
                        string strStudentName = objResult.resultDT.Rows[i]["StudentName"].ToString();
                        string strStudentMID = objResult.resultDT.Rows[i][StudentBO.STUDENT_STUDENTMID].ToString();
                        result.Add(string.Format("{0}~{1}", strStudentName, strStudentMID));
                    }
                }
            }
            return result.ToArray();
        }

        #endregion

        #region Add New Fees
        protected void lnkAddNewFee_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClasswiseStudentTemplate.aspx");
        }
        #endregion

        #region Show Button Click Event
        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                // btnSave.Enabled = false;
                //  btnSave.Attributes.Add("bgcolor", "#848484");
                Controls objControls = new Controls();
                ViewState["ClassMID"] = Convert.ToInt32(Request.Form[ddlClass.UniqueID]);
                ViewState["DivisionName"] = Convert.ToInt32(Request.Form[ddlDivision.UniqueID]);
                ViewState["AcademicYear"] = ddlAcademicYear.SelectedItem.ToString();
                hfCLassMID.Value = ViewState["ClassMID"].ToString();
                hfDivisionTID.Value = ViewState["DivisionName"].ToString();
                BindGridView();
                TextBox1.Text = "";
                divStName.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "BindDorpdownOnButtonClick();", true);
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");

            }
        }
        #endregion
    }
}