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
    public partial class StudentTemplate : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(StudentTemplate));
        #region pageload Event
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
                    btnSave.Enabled = true;
                    btnSave.Attributes.Add("bgcolor", "#3b5998");
                    ViewState["Mode"] = "Save";
                    ViewState["StudentFeesTemplateTID"] = 0;
                    // lblMsg.Visible = false;
                    BindAcademicYear();
                  //  btnCancel.Visible = false;
                    btnSave.Visible = false;
                    hfTab.Value = "0";
                    //BindColumnToGridview();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Clear();", true);

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

                objControls.BindDropDown_ListBox(dt, ddlAcademicYear, "AcademicYear", "AcademicYear");
                ddlAcademicYear.Items.Insert(0, new ListItem("-Select-", ""));
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
            try
            {
                StudentFeesTemplateTBL objStudentFeesTemplateBL = new StudentFeesTemplateTBL();
                StudentFeesTemplateTBO objStudentFeesTemplateBO = new StudentFeesTemplateTBO();
                ApplicationResult objResults = new ApplicationResult();

                DatabaseTransaction.OpenConnectionTransation();
                foreach (GridViewRow row in gvStudentFees.Rows)
                {

                    if (((CheckBox)row.FindControl("chkChild")).Checked)
                    {
                        string FeesNameWithAmount =ddlFeesCategoryName.SelectedItem.Text;
                        char[] delimiterChars = { '/' };
                        string[] words = FeesNameWithAmount.Split(delimiterChars);

                        objStudentFeesTemplateBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                        objStudentFeesTemplateBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                        objStudentFeesTemplateBO.ClassMID = Convert.ToInt32(ViewState["ClassMID"].ToString());
                        objStudentFeesTemplateBO.DivisionTID = Convert.ToInt32(ViewState["DivisionName"].ToString());
                        objStudentFeesTemplateBO.StudentMID = Convert.ToInt32(row.Cells[0].Text);
                        objStudentFeesTemplateBO.AcademicYear = ddlAcademicYear.SelectedItem.Text;
                        objStudentFeesTemplateBO.FeesCategoryMID = Convert.ToInt32(ddlFeesCategoryName.SelectedValue);

                        objStudentFeesTemplateBO.FeesAmount = Convert.ToDouble(words[1]);
                        objStudentFeesTemplateBO.IsLate = 0;
                        objStudentFeesTemplateBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        objStudentFeesTemplateBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);

                        DataTable Dt = Select_ClassTemlate_FeeCategory(Convert.ToInt32(ViewState["FeesCategoryMID"].ToString()), Convert.ToInt32(ViewState["ClassMID"].ToString()), Convert.ToInt32(ViewState["DivisionName"].ToString()), ddlAcademicYear.SelectedItem.ToString(), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(row.Cells[0].Text));
                        #region RollBack Transaction Starts

                       
                        if (((CheckBox)row.FindControl("chkChild")).Checked)
                        {
                            if (Dt.Rows.Count > 0)
                            {
                            }
                            else
                            {
                                DataTable dt = Select_ClassTemlate_FeeCategory(Convert.ToInt32(ViewState["FeesCategoryMID"].ToString()), Convert.ToInt32(ViewState["ClassMID"].ToString()),Convert.ToInt32(ViewState["DivisionName"].ToString()), ddlAcademicYear.SelectedItem.ToString(), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), 0);
                                if (dt.Rows.Count > 0)
                                {
                                    ViewState["ClassTemplateTID"] = Convert.ToInt32(dt.Rows[0][ClassWiseFeesTemplateTBO.CLASSWISEFEESTEMPLATET_CLASSWISEFEESTEMPLATETID].ToString());
                                }
                                objStudentFeesTemplateBO.ClassWiseFeesTemplateTID = Convert.ToInt32(ViewState["ClassTemplateTID"].ToString());
                              
                                objResults = objStudentFeesTemplateBL.StudentFeesTemplateT_Insert(objStudentFeesTemplateBO);
                                if (objResults != null)
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Fees Amount Successfully Saved.');</script>");
                                }
                               
                            }
                        }
                    }
                    else
                    {
                        DataTable Dt = Select_ClassTemlate_FeeCategory(Convert.ToInt32(ViewState["FeesCategoryMID"].ToString()), Convert.ToInt32(ViewState["ClassMID"].ToString()), Convert.ToInt32(ViewState["DivisionName"].ToString()), ddlAcademicYear.SelectedItem.ToString(), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(row.Cells[0].Text));
                        if (Dt.Rows.Count > 0)
                        {
                            ViewState["ClassTemplateTID"] = Convert.ToInt32(Dt.Rows[0][ClassWiseFeesTemplateTBO.CLASSWISEFEESTEMPLATET_CLASSWISEFEESTEMPLATETID].ToString());

                            DataTable dtFeeCollection = ValidateFeesbyFeesCollection(Convert.ToInt32(ViewState["ClassTemplateTID"].ToString()), Convert.ToInt32(ViewState["ClassMID"].ToString()),Convert.ToInt32( ViewState["DivisionName"].ToString()), ddlAcademicYear.SelectedItem.ToString(), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(row.Cells[0].Text));
                            if (dtFeeCollection.Rows.Count > 0)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('This Fee Category is Not Update/Delete.');</script>");
                            }
                            else
                            {
                                objResults = objStudentFeesTemplateBL.StudentFeesTemplateT_Delete(Convert.ToInt32(Convert.ToInt32(Dt.Rows[0][StudentFeesTemplateTBO.STUDENTFEESTEMPLATET_STUDENTFEESTEMPLATETID].ToString())), 0);
                                if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('This Fee Category is Update/Delete Successfully.');</script>");
                                }

                            }
                        }

                    }

                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Clear();", true);
                DatabaseTransaction.CommitTransation();
                #endregion
                ViewState["ClassMID"] = 0;
                ViewState["DivisionName"] = 0;
                ViewState["AcademicYear"] = "";
                ViewState["FeesCategoryMID"] = "";
                hfAcademicYear.Value = "0";
                hfCLassMID.Value = "0";
                hfDivisionTID.Value = "0";
                gvStudentFees.Visible = false;
                //ClearAll();
                //Response.Redirect("StudentTemplate.aspx",false);
                
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Cancel Event
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        #endregion

        #region View GridView
        protected void btnViewGrid_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = true;
                btnSave.Attributes.Add("bgcolor", "#848484");
                hfTab.Value = "1";
                Set_ButtonDropDown();
                gvAssignFees.Visible = false;
                gvStudentFees.Visible = true;
                ApplicationResult objResult = new ApplicationResult();
                StudentBL objStudentBL = new StudentBL();
                ClassWiseFeesTemplateTBL objClassTemplateBL = new ClassWiseFeesTemplateTBL();

                objResult = objStudentBL.Student_Select_ClassDivisionWise(Convert.ToInt32(ViewState["ClassMID"].ToString()), Convert.ToInt32(ViewState["DivisionName"].ToString()), ViewState["AcademicYear"].ToString(), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvStudentFees.DataSource = objResult.resultDT;
                        gvStudentFees.DataBind();
                    }
                    else
                    {
                        gvStudentFees.DataSource = null;
                        gvStudentFees.DataBind();
                        ClientScript.RegisterStartupScript(typeof(Page),"MessagePopUp","<script language='javascript'>alert('No Records.');</script>");
                        
                    }

                }

                foreach (GridViewRow row in gvStudentFees.Rows)
                {
                    objResult = objClassTemplateBL.StudentFeesTemplate_ForValidation(Convert.ToInt32(ViewState["ClassMID"].ToString()), Convert.ToInt32(ViewState["DivisionName"].ToString()), ddlAcademicYear.SelectedItem.ToString(), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(row.Cells[0].Text));
                    if (objResult != null)
                    {
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            ((CheckBox)row.FindControl("chkChild")).Enabled = false;
                            ((CheckBox)gvStudentFees.HeaderRow.FindControl("chkHeader")).Enabled = false;
                        }
                        DataTable Dt = Select_ClassTemlate_FeeCategory(Convert.ToInt32(ViewState["FeesCategoryMID"].ToString()), Convert.ToInt32(ViewState["ClassMID"].ToString()), Convert.ToInt32(ViewState["DivisionName"].ToString()), ddlAcademicYear.SelectedItem.ToString(), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(row.Cells[0].Text));


                        if (Dt.Rows.Count > 0)
                        {

                            ViewState["ClassTemplateTID"] = Convert.ToInt32(Dt.Rows[0][ClassWiseFeesTemplateTBO.CLASSWISEFEESTEMPLATET_CLASSWISEFEESTEMPLATETID].ToString());
                            ((CheckBox)row.FindControl("chkChild")).Checked = true;

                            DataTable dtFeeCollection = ValidateFeesbyFeesCollection(Convert.ToInt32(ViewState["ClassTemplateTID"].ToString()), Convert.ToInt32(ViewState["ClassMID"].ToString()), Convert.ToInt32(ViewState["DivisionName"].ToString()), ddlAcademicYear.SelectedItem.ToString(), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(row.Cells[0].Text));
                            if (dtFeeCollection.Rows.Count > 0)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='This Fee Category is Not Update/Delete.');</script>");

                                ((CheckBox)row.FindControl("chkChild")).Enabled = false;
                                ((CheckBox)gvStudentFees.HeaderRow.FindControl("chkHeader")).Enabled = false;
                            }
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

        #region Webservice

        [System.Web.Services.WebMethod]
        public static string LoadClass(int intSchoolMID)
        {
            try
            {
                #region Bind Class
                ClassBL objClassBL = new ClassBL();
                ClassBO objClassBO = new ClassBO();

                ApplicationResult objResultSection = new ApplicationResult();
                objResultSection = objClassBL.Class_SelectAll_ForDropDownNotSectionWise(intSchoolMID);
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
                DivisionTBL objDivisionBL = new DivisionTBL();
                DivisionTBO objDivisionBO = new DivisionTBO();

                ApplicationResult objResultSection = new ApplicationResult();
                objResultSection = objDivisionBL.Division_SelectAll_ClassWise_ForDropDown(intClassMID, intSchoolMID);
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
        public static string LoadFeesWithAmount(int intClassMID, int intDivisionTID, string strAcademicYear, int intSchoolMID)
        {
            try
            {
                #region Bind LoadFeesWithAmount
                StudentFeesTemplateTBL objStudentFeesTemplateBL = new StudentFeesTemplateTBL();
                ApplicationResult objResults = new ApplicationResult();

                objResults = objStudentFeesTemplateBL.StudentFeeTemplate_Select_FeesName_With_Amount(intClassMID, intDivisionTID, strAcademicYear, intSchoolMID);
                if (objResults != null)
                {
                    if (objResults.resultDT.Rows.Count > 0)
                    {

                    }

                }
                string res = "";
                res = DataSetToJSON(objResults.resultDT);
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

        #region Fetching Datatable ClassTemlate_FeeCategory_M
        public DataTable Select_ClassTemlate_FeeCategory(int intFeesCategoryMID, int intClassMID, int intDivisionTID, string strAcademicYear, int intSchoolMID, int intStudentMID)
        {
            ClassWiseFeesTemplateTBL objClassTemplateBL = new ClassWiseFeesTemplateTBL();


            ApplicationResult objResultsSelectT = new ApplicationResult();
            objResultsSelectT = objClassTemplateBL.ClassWiseFeesTemplateT_Select_FeesCategory_M(intFeesCategoryMID, intClassMID, intDivisionTID, strAcademicYear, intSchoolMID, intStudentMID);
            if (objResultsSelectT != null)
            {
            }
            return objResultsSelectT.resultDT;
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
            ApplicationResult objResults = new ApplicationResult();

            objResults = objDivisionBL.Division_SelectAll_ClassWise_ForDropDown(intClassMID, intSchoolMID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {
            }
            return objResults.resultDT;
        }
        #endregion

        #region Fetch FeesName With Amount
        private DataTable FetchFeesNameWithAmount(int intClassMID, int intDivisionTID, string strAcademicYear, int intSchoolMID)
        {
            StudentFeesTemplateTBL objStudentTemplateBl = new StudentFeesTemplateTBL();
            ApplicationResult objResults = new ApplicationResult();

            objResults = objStudentTemplateBl.StudentFeeTemplate_Select_FeesName_With_Amount(intClassMID, intDivisionTID, strAcademicYear, intSchoolMID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {
            }
            return objResults.resultDT;
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

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            ViewState["Mode"] = "Save";
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            gvStudentFees.Visible = false;
            //btnCancel.Visible = false;
            btnSave.Visible = false;
            hfTab.Value = "0";
           // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Clear();", true);
        }
        #endregion

        #region Set Button Click
        private void Set_ButtonDropDown()
        {
            try
            {
               // btnCancel.Visible = true;
                btnSave.Visible = true;
                Controls objControls = new Controls();
                ViewState["ClassMID"] = Convert.ToInt32(Request.Form[ddlClass.UniqueID]);
                ViewState["DivisionName"] = Convert.ToInt32(Request.Form[ddlDivision.UniqueID]);
                ViewState["AcademicYear"] = ddlAcademicYear.SelectedItem.ToString();
                ViewState["FeesCategoryMID"] = Request.Form[ddlFeesCategoryName.UniqueID].ToString();
                hfCLassMID.Value = ViewState["ClassMID"].ToString();
                hfDivisionTID.Value = ViewState["DivisionName"].ToString();
                hfFees.Value = Request.Form[ddlFeesCategoryName.UniqueID] ;
                hfAcademicYear.Value = ddlAcademicYear.SelectedItem.ToString();

                DataTable dtFeesNameWithAmount = new DataTable();
                //ddlFeesCategoryName.SelectedValue = ViewState["FeesName"].ToString();
                dtFeesNameWithAmount = FetchFeesNameWithAmount(Convert.ToInt32(ViewState["ClassMID"].ToString()), Convert.ToInt32(ViewState["DivisionName"].ToString()), ViewState["AcademicYear"].ToString(), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (dtFeesNameWithAmount.Rows.Count > 0)
                {
                    objControls.BindDropDown_ListBox(dtFeesNameWithAmount, ddlFeesCategoryName, "FeesNameWithAmount", "FeesCategoryMID");
                    ViewState["FeesName"] = dtFeesNameWithAmount.Rows[0]["FeesNameWithAmount"].ToString();
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "BindDorpdownOnButtonClick();", true);


                //DataTable dtClass = new DataTable();
                //dtClass = FetchClass(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                //if (dtClass.Rows.Count > 0)
                //{
                //    objControls.BindDropDown_ListBox(dtClass, ddlClass, "ClassName", "SectionTID");
                //}

                //DataTable dtDivision = new DataTable();
                //ddlClass.SelectedValue = ViewState["ClassMID"].ToString();
                //dtDivision = FetchDivision(Convert.ToInt32(ViewState["ClassMID"].ToString()), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                //if (dtDivision.Rows.Count > 0)
                //{
                //    objControls.BindDropDown_ListBox(dtDivision, ddlDivision, "DivisionName", "DivisionTID");
                //}

                
                //ddlDivision.SelectedValue = ViewState["DivisionName"].ToString();
                //ddlAcademicYear.SelectedItem.Text = ViewState["AcademicYear"].ToString();
                //ddlFeesCategoryName.Text = ViewState["FeesCategoryMID"].ToString();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region View Fees Which assign to Student
        protected void btnViewAssignFee_Click(object sender, EventArgs e)
        {
            try
            {
                hfTab.Value = "1";
                Set_ButtonDropDown();
                gvStudentFees.Visible = false;
                gvAssignFees.Visible = true;
                gvAssignFees.AutoGenerateColumns = false;
                ApplicationResult objResults = new ApplicationResult();
                StudentFeesTemplateTBL objStudentTemplateBL = new StudentFeesTemplateTBL();
                objResults = objStudentTemplateBL.StudentFeeTemplate_FeesNameWise(Session[ApplicationSession.SCHOOLID].ToString(),ViewState["ClassMID"].ToString(),ViewState["DivisionName"].ToString(), ddlAcademicYear.SelectedItem.ToString(), 0);

                if (objResults != null)
                {
                    if (objResults.resultDT.Rows.Count > 0)
                    {
                        for (int i = 0; i < objResults.resultDT.Columns.Count; i++)
                        {
                            BoundField boundfield = new BoundField();
                            boundfield.DataField = objResults.resultDT.Columns[i].ColumnName.ToString();
                            boundfield.HeaderText = objResults.resultDT.Columns[i].ColumnName.ToString();
                           // boundfield.SortExpression = dtStudentList.Columns[i].ColumnName.ToString();
                            gvAssignFees.Columns.Add(boundfield);
                        }
                        gvAssignFees.Visible = true;
                        gvAssignFees.DataSource = objResults.resultDT;
                        gvAssignFees.DataBind();
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

        #region Unused Code
        ////foreach (GridViewRow row in gvStudentFees.Rows)
        //for (int row = 0; row < this.gvStudentFees.Rows.Count; row++)
        //{
        //    StudentFeesTemplateTBL objStudentTemplateBl = new StudentFeesTemplateTBL();
        //    StudentFeesTemplateTBO objStudentTemplateBo = new StudentFeesTemplateTBO();
        //    //CheckBox chk = this.gvStudentFees.Rows[row].FindControl("chk5") as CheckBox;
        //    //string str1 = this.gvStudentFees.Rows[row]Text;
        //    //string str2 = this.gvStudentFees.Rows[row].Cells[0].Text;
        //    //string str3 = this.gvStudentFees.Rows[row].Cells[1].Text;

        //    //foreach (var control in this.gvStudentFees.Rows[row].Controls)
        //    //{
        //    //    var st = control;
        //    //}

        //    CheckBox rs = (CheckBox)gvStudentFees.Rows[row].Cells[2].NamingContainer.FindControl("chkChintan");

        //    //var sc = rs.FindControl("_chkChintan") as CheckBox;


        //    string header = "";
        //    //for (int i = 0; i < gvStudentFees.Rows.Count; i++)
        //    //{
        //    int intColumnCount = gvStudentFees.Columns.Count;
        //    //for (int j = 2; j < intColumnCount; j++)
        //    //{
        //    //    string chk1 = "chk";

        //    //    string str1 = this.gvStudentFees.Rows[row].Cells[j].Text;
        //    //    string str2 = this.gvStudentFees.Rows[row].Cells[0].Text;
        //    //    string str3 = this.gvStudentFees.Rows[row].Cells[1].Text;
        //    //    CheckBox chk = this.gvStudentFees.Rows[row].Cells[j].FindControl(chk1) as CheckBox;
        //    //    if (chk.Checked == true)
        //    //    {
        //    //        // header = gvStudentFees.HeaderRow.Cells[i].Text;

        //    //        char[] delimiterChars = { '/' };
        //    //        string[] words = header.Split(delimiterChars);
        //    //        //objStudentTemplateBo.StudentMID = Convert.ToInt32(row.Cells[0].Text);
        //    //        //objStudentTemplateBo.ClassWiseFeesTemplateTID = 1;
        //    //        objStudentTemplateBo.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
        //    //        objStudentTemplateBo.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
        //    //        objStudentTemplateBo.ClassMID = Convert.ToInt32(ViewState["ClassMID"].ToString());
        //    //        objStudentTemplateBo.DivisionName = ViewState["DivisionName"].ToString();
        //    //        objStudentTemplateBo.AcademicYear = ddlAcademicYear.SelectedItem.Text;
        //    //        objStudentTemplateBo.FeesCategoryMID = Convert.ToInt32(words[0]);
        //    //        objStudentTemplateBo.FeesAmount = Convert.ToDouble(words[1]);
        //    //        ClassWiseFeesTemplateTBL objClassTemplateBL = new ClassWiseFeesTemplateTBL();


        //    //        ApplicationResult objResultsSelectT = new ApplicationResult();
        //    //        objResultsSelectT = objClassTemplateBL.ClassWiseFeesTemplateT_Select_FeesCategory_M(objStudentTemplateBo.FeesCategoryMID, objStudentTemplateBo.ClassMID, objStudentTemplateBo.DivisionName, objStudentTemplateBo.AcademicYear, objStudentTemplateBo.SchoolMID);
        //    //        if (objResultsSelectT != null)
        //    //        {
        //    //            objStudentTemplateBo.ClassWiseFeesTemplateTID = Convert.ToInt32(objResultsSelectT.resultDT.Rows[0][ClassWiseFeesTemplateTBO.CLASSWISEFEESTEMPLATET_CLASSWISEFEESTEMPLATETID].ToString());
        //    //        }


        //    //        objStudentTemplateBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
        //    //        objStudentTemplateBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
        //    //    }
        //    //}

        //    // }

        //}
        //protected void ViewDetails(object sender, EventArgs e)
        //{
        //    CheckBox chk = (sender as CheckBox);

        //    GridViewRow row = (chk.NamingContainer as GridViewRow);
        //    //string id = lnkView.CommandArgument;
        //    string name = row.Cells[0].Text;
        //    CheckBox chkb = (row.FindControl("chkChintan") as CheckBox);
        //    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Id: " + id + " Name: " + name + " Country: " + country + "')", true);
        //}
        //#region Row DataBound of Student Gridview
        //protected void gvStudentFees_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            ApplicationResult objResult = new ApplicationResult();
        //            StudentFeesTemplateTBL objStudentFeeTemplate = new StudentFeesTemplateTBL();
        //            objResult = objStudentFeeTemplate.StudentFeeTemplate_FeesNameWise("1", "1", "A", "14-15");
        //            int intcCount = objResult.resultDT.Columns.Count;
        //            for (int j = 2; j < intcCount; j++)
        //            {
        //                CheckBox chk = new CheckBox();
        //                chk.ID = "chkChintan";
        //                // chk.AutoPostBack = true;
        //                chk.CheckedChanged += ViewDetails;
        //                //chk.Text = (e.Row.DataItem as DataRowView).Row["chk"].ToString();
        //                e.Row.Cells[j].Controls.Add(chk);

        //            }
        //        }
        //        if (e.Row.RowType == DataControlRowType.Header)
        //        {
        //            ApplicationResult objResult = new ApplicationResult();
        //            StudentFeesTemplateTBL objStudentFeeTemplate = new StudentFeesTemplateTBL();
        //            objResult = objStudentFeeTemplate.StudentFeeTemplate_FeesNameWise("1", "1", "A", "14-15");
        //            int intcCount = objResult.resultDT.Columns.Count;
        //            for (int j = 2; j < intcCount; j++)
        //            {
        //                TemplateField tfield = new TemplateField();
        //                CheckBox chk = new CheckBox();
        //                chk.ID = "chkHeader_" + j;
        //                chk.Text = objResult.resultDT.Columns[j].ColumnName.ToString();
        //                e.Row.Cells[j].Controls.Add(chk);
        //                tfield = new TemplateField();
        //                // tfield.HeaderText = objResult.resultDT.Columns[j].ColumnName.ToString();
        //                gvStudentFees.Columns.Add(tfield);

        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Error", ex);
        //        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
        //    }
        //}
        //#endregion
        #endregion

      
    }
}