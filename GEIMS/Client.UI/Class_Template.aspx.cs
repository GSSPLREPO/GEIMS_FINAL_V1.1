using System;
using System.Data;
using System.Drawing;
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
    public partial class Class_Template : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(Class_Template));

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
                    ViewState["Mode"] = "Save";
                    ViewState["ClassTemplateMID"] = 0;
                    BindAcademicYear();
                    // btnCancel.Visible = false;
                    btnSave.Visible = false;
                    hfTab.Value = "0";

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

        #region BindFeesGrid
        public void BindGridView()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                FeesCategoryBL objGridFeesBL = new FeesCategoryBL();

                objResult = objGridFeesBL.FeesCategory_Select_For_ClassTemplate(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {

                        gvFees.Visible = true;
                        gvFees.DataSource = objResult.resultDT;
                        gvFees.DataBind();
                        ((CheckBox)gvFees.HeaderRow.FindControl("chkHeader")).Enabled = true;
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


                        if (Dt.Rows.Count > 0)
                        {
                            ViewState["ClassTemplateTID"] = Convert.ToInt32(Dt.Rows[0][ClassWiseFeesTemplateTBO.CLASSWISEFEESTEMPLATET_CLASSWISEFEESTEMPLATETID].ToString());
                            ((CheckBox)row.FindControl("chkChild")).Checked = true;
                            ((CheckBox)gvFees.HeaderRow.FindControl("chkHeader")).Enabled = false;
                            btnSave.Enabled = true;
                            // btnSave.BackColor = Color.#3b5998;
                            btnSave.Attributes.Add("bgcolor", "#3b5998");
                            //((TextBox)row.FindControl("txtFeesAmount")).Text = Dt.Rows[0][ClassWiseFeesTemplateTBO.CLASSWISEFEESTEMPLATET_FEESAMOUNT].ToString();
                            ((TextBox)row.FindControl("txtFeesAmountForMale")).Text = Dt.Rows[0][ClassWiseFeesTemplateTBO.CLASSWISEFEESTEMPLATET_FEESAMOUNTFORMALE].ToString();
                            ((TextBox)row.FindControl("txtFeesAmountForFemale")).Text = Dt.Rows[0][ClassWiseFeesTemplateTBO.CLASSWISEFEESTEMPLATET_FEESAMOUNTFORFEMALE].ToString();

                            DataTable dtFeeCollection = ValidateFeesbyFeesCollection(Convert.ToInt32(ViewState["ClassTemplateTID"].ToString()), Convert.ToInt32(ViewState["ClassMID"].ToString()), Convert.ToInt32(ViewState["DivisionName"].ToString()), ddlAcademicYear.SelectedItem.ToString(), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), 0);
                            if (dtFeeCollection.Rows.Count > 0)
                            {
                                //ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='This Fee Category is Not Update/Delete.');</script>");
                                //foreach (GridViewRow rowData in gvFees.Rows)
                                //{
                                ((CheckBox)row.FindControl("chkChild")).Enabled = false;
                                //((TextBox)row.FindControl("txtFeesAmount")).Enabled = false;
                                ((TextBox)row.FindControl("txtFeesAmountForMale")).Enabled = false;
                                ((TextBox)row.FindControl("txtFeesAmountForFemale")).Enabled = false;
                                //}
                                btnSave.Enabled = true;
                                btnSave.Attributes.Add("bgcolor", "#3b5998");
                                // ((CheckBox)row.FindControl("chkHeader")).Visible = false;
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

        #region Save Event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // divLoading.Visible = true;
                ClassWiseFeesTemplateTBL objClassTemplateBL = new ClassWiseFeesTemplateTBL();
                ClassWiseFeesTemplateTBO objClassTemplateBO = new ClassWiseFeesTemplateTBO();
                #region RollBack Transaction Starts

                DatabaseTransaction.OpenConnectionTransation();

                foreach (GridViewRow row in gvFees.Rows)
                {
                    ViewState["FeesCategoryMID"] = Convert.ToInt32(row.Cells[0].Text);
                    if (((CheckBox)row.FindControl("chkChild")).Checked)
                    {

                        objClassTemplateBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                        objClassTemplateBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                        objClassTemplateBO.ClassMID = Convert.ToInt32(ViewState["ClassMID"].ToString());
                        objClassTemplateBO.DivisionTID = Convert.ToInt32(ViewState["DivisionName"].ToString());
                        //objClassTemplateBO.FeesAmount = Convert.ToDouble(((TextBox)row.FindControl("txtFeesAmount")).Text);
                        objClassTemplateBO.FeesAmountForMale = Convert.ToDouble(((TextBox)row.FindControl("txtFeesAmountForMale")).Text);
                        objClassTemplateBO.FeesAmountForFemale = Convert.ToDouble(((TextBox)row.FindControl("txtFeesAmountForFemale")).Text);

                        objClassTemplateBO.FeesCategoryMID = Convert.ToInt32(row.Cells[0].Text);
                        objClassTemplateBO.AcademicYear = ddlAcademicYear.SelectedItem.Text;
                        objClassTemplateBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        objClassTemplateBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                        if (objClassTemplateBO.FeesAmountForMale == 0.0 || objClassTemplateBO.FeesAmountForFemale   == 0.0)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Please Fill Fees Amount " + row.Cells[0].Text + ".');</script>");
                        }
                        else
                        {
                            Select_ClassTemlate_FeeCategory();

                            DataTable Dt = Select_ClassTemlate_FeeCategory();

                            if (((CheckBox)row.FindControl("chkChild")).Checked)
                            {
                                if (Dt.Rows.Count > 0)
                                {
                                    ViewState["ClassTemplateTID"] = Convert.ToInt32(Dt.Rows[0][ClassWiseFeesTemplateTBO.CLASSWISEFEESTEMPLATET_CLASSWISEFEESTEMPLATETID].ToString());
                                    ApplicationResult objResultsInsert = new ApplicationResult();
                                    objClassTemplateBO.ClassWiseFeesTemplateTID = Convert.ToInt32(ViewState["ClassTemplateTID"].ToString());
                                    objResultsInsert = objClassTemplateBL.ClassWiseFeesTemplateT_Update(objClassTemplateBO);
                                    if (objResultsInsert != null)
                                    {
                                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Fees Amount Successfully Saved.');</script>");
                                    }
                                }
                                else
                                {
                                    ApplicationResult objResultsInsert = new ApplicationResult();

                                    objResultsInsert = objClassTemplateBL.ClassWiseFeesTemplateT_Insert(objClassTemplateBO);
                                    if (objResultsInsert != null)
                                    {
                                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Fees Amount Successfully Saved.');</script>");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        DataTable Dt = Select_ClassTemlate_FeeCategory();
                        if (Dt.Rows.Count > 0)
                        {
                            ViewState["ClassTemplateTID"] = Convert.ToInt32(Dt.Rows[0][ClassWiseFeesTemplateTBO.CLASSWISEFEESTEMPLATET_CLASSWISEFEESTEMPLATETID].ToString());

                            DataTable dtFeeCollection = ValidateFeesbyFeesCollection(Convert.ToInt32(ViewState["ClassTemplateTID"].ToString()), objClassTemplateBO.ClassMID, Convert.ToInt32(objClassTemplateBO.DivisionTID), objClassTemplateBO.AcademicYear, Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), 0);
                            if (dtFeeCollection.Rows.Count > 0)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='This Fee Category is Not Update/Delete.');</script>");
                            }
                            else
                            {
                                ApplicationResult objResultsDelete = new ApplicationResult();
                                objResultsDelete = objClassTemplateBL.ClassWiseFeesTemplateT_Delete(Convert.ToInt32(Dt.Rows[0][ClassWiseFeesTemplateTBO.CLASSWISEFEESTEMPLATET_CLASSWISEFEESTEMPLATETID].ToString()));
                                if (objResultsDelete.status == ApplicationResult.CommonStatusType.SUCCESS)
                                {
                                    StudentFeesTemplateTBL objStudentTemplate = new StudentFeesTemplateTBL();
                                    objResultsDelete = objStudentTemplate.StudentFeesTemplateT_Delete_By_ClassWiseTemplateTID(Convert.ToInt32(Dt.Rows[0][ClassWiseFeesTemplateTBO.CLASSWISEFEESTEMPLATET_CLASSWISEFEESTEMPLATETID].ToString()), 0);
                                    if (objResultsDelete.status == ApplicationResult.CommonStatusType.SUCCESS)
                                    {

                                    }
                                }
                            }
                        }

                    }

                }
                DatabaseTransaction.CommitTransation();
                #endregion
                ViewState["ClassMID"] = 0;
                ViewState["DivisionName"] = 0;
                ViewState["AcademicYear"] = "";
                hfCLassMID.Value = "0";
                hfDivisionTID.Value = "0";
               // ClearAll();
                //  Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "BindClass();", true);
                // divLoading.Visible = false;
                //  Response.Redirect("Class_Template.aspx");

            }
            catch (Exception ex)
            {
                DatabaseTransaction.RollbackTransation();
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Add New Fees
        protected void lnkAddNewFee_Click(object sender, EventArgs e)
        {
            //Controls objControls = new Controls();
            //objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            //gvFees.Visible = false;
            Response.Redirect("Class_Template.aspx");

        }
        #endregion

        #region View GridView
        protected void btnViewGrid_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                btnSave.Attributes.Add("bgcolor", "#848484");
                Controls objControls = new Controls();
                ViewState["ClassMID"] = Convert.ToInt32(Request.Form[ddlClass.UniqueID]);
                ViewState["DivisionName"] = Convert.ToInt32(Request.Form[ddlDivision.UniqueID]);
                ViewState["AcademicYear"] = ddlAcademicYear.SelectedItem.ToString();
                hfCLassMID.Value = ViewState["ClassMID"].ToString();
                hfDivisionTID.Value = ViewState["DivisionName"].ToString();
                //DataTable dtClass = new DataTable();
                //dtClass = FetchClass(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                //if (dtClass.Rows.Count > 0)
                //{
                //    objControls.BindDropDown_ListBox(dtClass, ddlClass, "ClassName", "ClassMID");
                //}
                //DataTable dtDivision = new DataTable();
                //ddlClass.SelectedValue = ViewState["ClassMID"].ToString();
                //dtDivision = FetchDivision(Convert.ToInt32(ViewState["ClassMID"].ToString()), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                //if (dtDivision.Rows.Count > 0)
                //{
                //    objControls.BindDropDown_ListBox(dtDivision, ddlDivision, "DivisionName", "DivisionTID");
                //}
                //DivisionTBO objDivisionBO = new DivisionTBO();
                //ddlDivision.SelectedValue = ViewState["DivisionName"].ToString();
                //ddlAcademicYear.SelectedItem.Text = ViewState["AcademicYear"].ToString();
                BindGridView();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "BindDorpdownOnButtonClick();", true);
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Cancel Click
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAll();
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

        #region Fetching Datatable ClassTemlate_FeeCategory_M
        public DataTable Select_ClassTemlate_FeeCategory()
        {
            ClassWiseFeesTemplateTBL objClassTemplateBL = new ClassWiseFeesTemplateTBL();


            ApplicationResult objResultsSelectT = new ApplicationResult();
            objResultsSelectT = objClassTemplateBL.ClassWiseFeesTemplateT_Select_FeesCategory_M(Convert.ToInt32(ViewState["FeesCategoryMID"].ToString()), Convert.ToInt32(ViewState["ClassMID"].ToString()), Convert.ToInt32(ViewState["DivisionName"].ToString()), ddlAcademicYear.SelectedItem.ToString(), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), 0);
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
            gvFees.Visible = false;
            btnSave.Visible = false;
            // btnCancel.Visible = false;
            hfTab.Value = "0";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Clear();", true);
        }
        #endregion

        #region Unused Code
        //  $(document.getElementById('<%= btnViewGrid.ClientID %>')).click(function () {
        //    $(document.getElementById('<%= ddlClass.ClientID %>')).empty();
        //    var optionhtml1 = '<option value="' + -1 + '">' + "-Select-" + '</option>';
        //    $(document.getElementById('<%= ddlClass.ClientID %>')).append(optionhtml1);

        //    //var optionhtml1 = '<option value="' + -1 + '">' + "-Select-" + '</option>';
        //     $(document.getElementById('<%= ddlDivision.ClientID %>')).append(optionhtml1);
        //    var valid = $("#aspnetForm").validationEngine('validate');
        //    var vars = $("#aspnetForm").serialize();
        //    $.ajax({
        //        type: "POST",
        //        contentType: "application/json; charset=utf-8",
        //        url: "Class_Template.aspx/LoadClass",
        //        data: "{'intSchoolMID':'" + <%=Session["SchoolID"] %> + "'}",
        //        dataType: "json",
        //        success: function (data) {
        //            var temp = $.parseJSON(data.d);



        //            $.each(temp, function (i) {
        //                var optionhtml = '<option value="' +
        //                 temp[i].ClassMID + '">' + temp[i].ClassName + '</option>';
        //                $(document.getElementById('<%= ddlClass.ClientID %>')).append(optionhtml);

        //            });
        //        },
        //        error: function (error) {
        //            // alert("Error" + error);
        //        }

        //    });
        //});
        #endregion
    }
}