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
    public partial class FeesCancellation : PageBase
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
                    ViewState["Mode"] = "Save";
                    txtTotalPaidAmount.Attributes.Add("readonly", "readonly");
                    txtTotalScholerShip.Attributes.Add("readonly", "readonly");
                    txtAmountPaid.Attributes.Add("readonly", "readonly");
                    txtRemainingPaidFees.Attributes.Add("readonly", "readonly");
                    divFeePanel.Visible = false;
                    BindAcademicYear();
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
        }
        #endregion

        #region Bind Fees Gridview
        protected void BindFeesGrid()
        {
            try
            {
                FeesCollectionBL objFeeCollectionBL = new FeesCollectionBL();
                ApplicationResult objResults = new ApplicationResult();

                objResults = objFeeCollectionBL.Fee_Collection_PastDetail(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(ViewState["ClassMID"].ToString()), Convert.ToInt32(ViewState["DivisionName"].ToString()), ddlYear.SelectedItem.ToString(), Convert.ToInt32(ViewState["StudentMID"].ToString()));
                if (objResults != null)
                {
                    gvPastFees.Visible = true;
                    gvPastFees.DataSource = objResults.resultDT;
                    gvPastFees.DataBind();
                    if (objResults.resultDT.Rows.Count > 0)
                    {
                        aa.Visible = true;
                        divPastFees.Visible = true;
                    }
                    else
                    {
                        aa.Visible = false;
                        divPastFees.Visible = false;
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('No Record Found.');</script>");
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
                    divFeePanel.Visible = true;
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
                                    lblClassDivision.Text = objResultsClass.resultDT.Rows[0][ClassBO.CLASS_CLASSNAME].ToString() + "-" + ViewState["Division"].ToString();
                                    ViewState["ClassName"] = objResultsClass.resultDT.Rows[0][ClassBO.CLASS_CLASSNAME].ToString();
                                }

                            }
                            #endregion

                            lblAdmissionNo.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_ADMISSIONNO].ToString();
                            lblCurrentGrNo.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_CURRENTGRNO].ToString();
                            lblStudentNameEng.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_STUDENTLASTNAMEENG].ToString() + " " + objResults.resultDT.Rows[0][StudentBO.STUDENT_STUDENTFIRSTNAMEENG].ToString() + " " + objResults.resultDT.Rows[0][StudentBO.STUDENT_STUDENTMIDDLENAMEENG].ToString();

                            lblCurrentSection.Text = ViewState["SectionName"].ToString();
                            lblAcademicYear.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_CURRENTYEAR].ToString();
                            ViewState["AcademicYear"] = objResults.resultDT.Rows[0][StudentBO.STUDENT_CURRENTYEAR].ToString();

                        }
                    }
                    divFeePanel.Visible = true;
                    BindFeesGrid();
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
                FeesCollectionBL ObjFeesCancellationBL = new FeesCollectionBL();
                ApplicationResult objResults = new ApplicationResult();
                int Count = 0;
                string FeesTID = string.Empty;
                string FeesMID = string.Empty;
                int FeesMIDCount = 0;
                foreach (GridViewRow row in gvPastFees.Rows)
                {
                    ViewState["FeesCollectionTID"] = Convert.ToInt32(row.Cells[1].Text);

                    if (((CheckBox)row.FindControl("chkChild")).Checked)
                    {
                        FeesTID += ViewState["FeesCollectionTID"].ToString() + ",";
                        if (FeesMIDCount == 0)
                        {
                            FeesMIDCount = Convert.ToInt32(row.Cells[0].Text);
                            FeesMID += FeesMIDCount + ",";
                            // ViewState["FeesCollectionMID"] = Convert.ToInt32(row.Cells[0].Text);
                        }
                        else
                        {
                            if (FeesMIDCount != Convert.ToInt32(row.Cells[0].Text))
                            {
                                FeesMIDCount = Convert.ToInt32(row.Cells[0].Text);
                                FeesMID += FeesMIDCount + ",";
                                // ViewState["FeesCollectionMID"] = Convert.ToInt32(row.Cells[0].Text);
                            }
                        }
                        Count = Count + 1;
                    }
                }
                objResults = ObjFeesCancellationBL.Fee_Collection_ForCancellation(Convert.ToInt32(ViewState["StudentMID"].ToString()), FeesTID.TrimEnd(','), Convert.ToInt32(Session[ApplicationSession.USERID]), DateTime.UtcNow.AddHours(5.5).ToString(), txtCancellationReason.Text, FeesMID.TrimEnd(','));
                if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                {
                    ClearAll();
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('FeesCancellation Done SuccessFully.');</script>");
                    divFeePanel.Visible = false;

                }
            }
            catch (Exception ex)
            {
                DatabaseTransaction.RollbackTransation();
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Go for Searching Student
        protected void btnGo_Click(object sender, EventArgs e)
        {
            gvPastFees.Visible = false;
            try
            {
                StudentBL objStudentBL = new StudentBL();
                StudentBO objStudentBO = new StudentBO();

                ApplicationResult objResultProgram = new ApplicationResult();
                objResultProgram = objStudentBL.Student_Search_StudentName(txtSearchName.Text, Convert.ToInt32(ddlSearchBy.SelectedValue), Convert.ToInt32(Session[ApplicationSession.ROLEID].ToString()), Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString()));
                if (objResultProgram != null)
                {

                    if (objResultProgram.resultDT.Rows.Count > 0)
                    {
                        gvStudent.Visible = true;
                        gvStudent.DataSource = objResultProgram.resultDT;
                        gvStudent.DataBind();
                        divFeePanel.Visible = false;
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('No Record Found.');</script>");
                        gvStudent.Visible = false;
                        divFeePanel.Visible = false;
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

        #region Get Employee Web Service Method
        [System.Web.Services.WebMethod]
        public static string[] GetAllStudentNameForReport(string prefixText, int SearchType, int SchoolMID, int SectionType, int ClassType, int DivisionType)
        {
            StudentBL objEmployeeMbl = new StudentBL();
            ApplicationResult objResult = new ApplicationResult();
            string strSearchText = prefixText + "%";
            List<string> result = new List<string>();
            objResult = objEmployeeMbl.Student_ForAutocomplete(strSearchText, SearchType, SchoolMID, SectionType, ClassType, DivisionType);
            if (objResult != null)
            {
                for (int i = 0; i < objResult.resultDT.Rows.Count; i++)
                {
                    if (SearchType == 1)
                    {
                        string strStudentName = objResult.resultDT.Rows[i]["StudentName"].ToString();
                        string strStudentMID = objResult.resultDT.Rows[i][StudentBO.STUDENT_STUDENTMID].ToString();
                        result.Add(string.Format("{0}~{1}", strStudentName, strStudentMID));
                    }
                    else if (SearchType == 2)
                    {
                        string strStudentGRNo = objResult.resultDT.Rows[i]["CurrentGrNo"].ToString();
                        string strEmployeeMID = objResult.resultDT.Rows[i][StudentBO.STUDENT_STUDENTMID].ToString();
                        result.Add(string.Format("{0}~{1}", strStudentGRNo, strEmployeeMID));
                    }
                    else if (SearchType == 3)
                    {
                        string strAdmission = objResult.resultDT.Rows[i]["AdmissionNo"].ToString();
                        string strEmployeeMID = objResult.resultDT.Rows[i][StudentBO.STUDENT_STUDENTMID].ToString();
                        result.Add(string.Format("{0}~{1}", strAdmission, strEmployeeMID));
                    }
                    else if (SearchType == 4)
                    {
                        string strUniqueID = objResult.resultDT.Rows[i]["UniqueID"].ToString();
                        string strEmployeeMID = objResult.resultDT.Rows[i][StudentBO.STUDENT_STUDENTMID].ToString();
                        result.Add(string.Format("{0}~{1}", strUniqueID, strEmployeeMID));
                    }
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
            ViewState["FeesCollectionTID"] = null;
            ViewState["FeesCollectionMID"] = null;
            ViewState["StudentMID"] = null;
            ViewState["Mode"] = "Save";
        }
        #endregion
    }
}