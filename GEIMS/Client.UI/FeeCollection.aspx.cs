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
    public partial class FeeCollection : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(FeeCollection));

        #region Declaration
        double totalAmount = 0;
        double totalAmount2 = 0;
        double FinalTotal = 0;
        double totalPaidAmount = 0;
        double FinalpaidTotal = 0;
        double totalPendingAmount = 0;
        double FinalPendingTotal = 0;
        double TotalDiscount = 0;
        double TotalPaidAmount = 0;
        double totalAmount1 = 0;
        double FinalTotal1 = 0;
        double totalPaidAmount1 = 0;
        double FinalpaidTotal1 = 0;
        double totalPendingAmount1 = 0;
        double FinalPendingTotal1 = 0;
        double FinalCurrentTotal1 = 0;
        double FinalCurrentTotal = 0;
        int initialInsert = 0;
        double Discount = 0;
        double Discount1 = 0;
        int id = 0;
        int id1 = 0;
        Controls objControl = new Controls();
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
                    //txtdate.Attributes.Add("readonly", "readonly");
                    txtAmountPaid.Attributes.Add("readonly", "readonly");
                    divFeePanel.Visible = false;
                    //BindModeOfPayment();
                    //BindBankList();
                    BindSection();
                    foreach (GridViewRow row in gvFees.Rows)
                    {
                        TextBox txtTotalAmount = (TextBox)row.FindControl("txtTotalAmount");
                        TextBox txtFeesAmount = (TextBox)row.FindControl("txtFeeAmount");
                        txtTotalAmount.Attributes.Add("readonly", "readonly");
                        txtFeesAmount.Attributes.Add("readonly", "readonly");
                    }
                    Session["FYear"] = GetCurrentFinancialYear();
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
        }
        #endregion

        #region Bind Section
        public void BindSection()
        {
            try
            {
                SectionBL ObjSectionBl = new SectionBL();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();

                objResult = ObjSectionBl.Section_SelectAll_SectionMID(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlSection, "SectionName", "SectionMID");
                        ddlSection.Items.Insert(0, new ListItem("-Select-", ""));
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

        #region Bind Class
        public void BindClass()
        {
            try
            {
                ClassBL objClassBL = new ClassBL();
                ClassBO objClassBO = new ClassBO();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();

                int SchoolID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                int SectionID = Convert.ToInt32(ddlSection.SelectedValue.ToString());

                objResult = objClassBL.Find_Class_SectionWise(SchoolID, SectionID);

                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlClassName, "ClassName", "ClassMID");
                        ddlClassName.Items.Insert(0, new ListItem("-Select-", "0"));
                    }
                    else
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlClassName, "ClassName", "ClassMID");
                        ddlDivisionName.Items.Insert(0, new ListItem("-Select-", "0"));
                        ddlDivisionName.ClearSelection();
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

        #region Bind Division
        public void BindDivision()
        {
            try
            {
                ClassBL objClassBL = new ClassBL();
                ClassBO objClassBO = new ClassBO();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();

                int ClassID = Convert.ToInt32(ddlClassName.SelectedValue.ToString());

                objResult = objClassBL.Find_Division_ClassWise(ClassID);

                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlDivisionName, "DivisionName", "DivisionTID");
                        ddlDivisionName.Items.Insert(0, new ListItem("-Select-", "0"));
                    }
                    else
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlDivisionName, "DivisionName", "DivisionTID");
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

        #region Bind Fees Gridview
        protected void BindFeesGrid()
        {
            try
            {
                FeesCollectionBL objFeeCollectionBL = new FeesCollectionBL();
                ApplicationResult objResults = new ApplicationResult();
                objResults = objFeeCollectionBL.Fee_Collection_WithOptionalAndCompulsoryFees(Convert.ToInt32(ViewState["StudentMID"].ToString()));
                if (objResults != null)
                {
                    gvFees.Visible = true;
                    gvFees.DataSource = objResults.resultDT;
                    gvFees.DataBind();
                    if (objResults.resultDT.Rows.Count > 0)
                    {
                        divFeeVisibility.Visible = true;
                        lblFee.Visible = false;
                        pnlFees.Visible = true;
                    }
                    else
                    {
                        // ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Class Fee Template is not defined for " + Convert.ToInt32(ViewState["ClassName"].ToString()) + "-" + ViewState["Division"].ToString() + "( " + ViewState["AcademicYear"].ToString() + " ).');</script>");
                        divFeePanel.Visible = true;
                        divFeeVisibility.Visible = false;
                        pnlFees.Visible = false;
                        lblFee.Visible = true;
                        lblFee.Text = "No fee Details";
                    }
                }
                objResults = objFeeCollectionBL.Fee_Collection_PastDetail(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(ViewState["ClassMID"].ToString()), Convert.ToInt32(ViewState["DivisionName"].ToString()), ViewState["AcademicYear"].ToString(), Convert.ToInt32(ViewState["StudentMID"].ToString()));
                if (objResults != null)
                {
                    if (objResults.resultDT.Rows.Count > 0)
                    {
                        lblmsg.Visible = false;
                        gvPastFees.Visible = true;
                        gvPastFees.DataSource = objResults.resultDT;
                        gvPastFees.DataBind();
                        divFeeVisibility.Visible = true;
                    }
                    else
                    {
                        //divPastFees.Visible = false;
                        lblmsg.Visible = true;
                        lblmsg.Text = "No Past Record.";
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

        #region Bind Report Gridview
        protected void BindFeesReport()
        {
            try
            {
                FeesCollectionBL objFeeCollectionBL = new FeesCollectionBL();
                ApplicationResult objResults = new ApplicationResult();
                objResults = objFeeCollectionBL.FeesCollection_Report(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(ViewState["FeesCollectionMID"]));
                if (objResults != null)
                {
                    if (objResults.resultDT.Rows.Count > 0)
                    {
                       // gvReport.Visible = true;
                        gvReport.DataSource = objResults.resultDT;
                        gvReport.DataBind();

                        //gvReport1.Visible = true;
                        gvReport1.DataSource = objResults.resultDT;
                        gvReport1.DataBind();


                        dlBindFeesGrid.DataSource = objResults.resultDT;
                        dlBindFeesGrid.DataBind();

                        dlBindFeesGridForSchool.DataSource= objResults.resultDT;
                        dlBindFeesGridForSchool.DataBind();

                        //dlBindFeesGridForClassTeacher.DataSource= objResults.resultDT;
                        //dlBindFeesGridForClassTeacher.DataBind();

                        dlBindFeesGridForBank.DataSource= objResults.resultDT;
                        dlBindFeesGridForBank.DataBind();

                        int CurrentPayVal1 = 0;
                        foreach (DataRow dr in objResults.resultDT.Rows)
                        {
                            CurrentPayVal1 += Convert.ToInt32(dr["CurrentlyPaid"]);
                        }
                        ViewState["TotalPaidAmount"] = CurrentPayVal1;
                        ViewState["AMTinWords"] = ConvertInWords(Convert.ToInt32(CurrentPayVal1));


                        //ViewState["AMTinWords"] = ConvertInWords(Convert.ToInt32(ViewState["CurrentPay"]));

                    }
                    else
                    {
                        gvReport.Visible = false;
                        gvReport1.Visible = false;
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

        #region RowDataBound Event of Fees Report
        protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    totalAmount2 = totalAmount2 + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "FeesAmount"));
                    totalPaidAmount = totalPaidAmount + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PaidFees"));
                    Discount = Discount + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ScholarShip"));
                    totalPendingAmount = totalPendingAmount + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PendingFees"));
                    FinalCurrentTotal = FinalCurrentTotal + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CurrentlyPaid"));
                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label lblTotalAmount = (Label)e.Row.FindControl("lblTotalAmount");
                    lblTotalAmount.Text = totalAmount2.ToString();

                    Label lblPaidAmount = (Label)e.Row.FindControl("lblPaidAmount");
                    lblPaidAmount.Text = totalPaidAmount.ToString();

                    Label lblDiscount = (Label)e.Row.FindControl("lblDiscount");
                    lblDiscount.Text = Discount.ToString();

                    Label lblCurrentlyPaid = (Label)e.Row.FindControl("lblCurrentAmount");
                    lblCurrentlyPaid.Text = FinalCurrentTotal.ToString();

                    Label lblPendingAmount = (Label)e.Row.FindControl("lblPendingAmount");
                    lblPendingAmount.Text = totalPendingAmount.ToString();

                }
                CommonFunctions objFuction = new CommonFunctions();
                lblcurAmount.Text = objFuction.ConvertInWords(Convert.ToInt32(FinalCurrentTotal));
                lblCurAmountInt.Text = Convert.ToString(FinalCurrentTotal);
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region RowDataBound Event of Fees Report
        protected void gvReport1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    totalAmount1 = totalAmount1 + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "FeesAmount"));
                    totalPaidAmount1 = totalPaidAmount1 + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PaidFees"));
                    Discount1 = Discount1 + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ScholarShip"));
                    totalPendingAmount1 = totalPendingAmount1 + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PendingFees"));
                    FinalCurrentTotal1 = FinalCurrentTotal1 + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CurrentlyPaid"));

                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label lblTotalAmount1 = (Label)e.Row.FindControl("lblTotalAmount1");
                    lblTotalAmount1.Text = totalAmount1.ToString();

                    Label lblCurrentlyPaid1 = (Label)e.Row.FindControl("lblCurrentAmount1");
                    lblCurrentlyPaid1.Text = FinalCurrentTotal1.ToString();

                    Label lblDiscount1 = (Label)e.Row.FindControl("lblDiscount1");
                    lblDiscount1.Text = Discount1.ToString();

                    Label lblPaidAmount1 = (Label)e.Row.FindControl("lblPaidAmount1");
                    lblPaidAmount1.Text = totalPaidAmount1.ToString();

                    Label lblPendingAmount1 = (Label)e.Row.FindControl("lblPendingAmount1");
                    lblPendingAmount1.Text = totalPendingAmount1.ToString();

                }
                CommonFunctions objFuction = new CommonFunctions();
                lblcurAmount1.Text = objFuction.ConvertInWords(Convert.ToInt32(FinalCurrentTotal));
                lblCurAmountInt1.Text = Convert.ToString(FinalCurrentTotal1);
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Gridview Row Command Event
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
                            ViewState["StudentContactNumber"]= objResults.resultDT.Rows[0][StudentBO.STUDENT_PERMANENTCONTACTNO].ToString();
                            BindBudgetCategory();                  
                        }
                    }
                    divFeePanel.Visible = true;
                    BindFeesGrid();
                    //BindBudgetCategory();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region RowDataBound Event of Fees Gridview
        protected void gvFees_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {           
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    totalAmount = totalAmount + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "FeesAmount"));
                    FinalTotal = FinalTotal + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "FeesAmount"));
                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label lblamount = (Label)e.Row.FindControl("lblFeeAmount");
                    lblamount.Text = totalAmount.ToString();

                    Label lblTotal = (Label)e.Row.FindControl("lblTotalAmount");
                    lblTotal.Text = FinalTotal.ToString();
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
            FeesCollectionBL objFeeCollectionBL = new FeesCollectionBL();
            FeesCollectionBO objFeeCollectionBO = new FeesCollectionBO();
            FeesCollectionTBO objFeeCollectionTBO = new FeesCollectionTBO();
            ApplicationResult objResults = new ApplicationResult();

            ApplicationResult objResultInsert = new ApplicationResult();
            ReceiptPaymentBL objReceiptPaymentBL = new ReceiptPaymentBL();
            ReceiptPaymentBO objReceiptPaymentBO = new ReceiptPaymentBO();
            try
            {
                float Total = 0;

                Label lblTotalAmount = (Label)gvFees.FooterRow.FindControl("lblTotalAmount");
                objFeeCollectionBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                objFeeCollectionBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                objFeeCollectionBO.StudentMID = Convert.ToInt32(ViewState["StudentMID"].ToString());
                objFeeCollectionBO.FeesToBePaid = Convert.ToDouble(txtFullAmount.Text);
                objFeeCollectionBO.AmountPaid = Convert.ToDouble(txtAmountPaid.Text);
                objFeeCollectionBO.Date = txtdate.Text;
                objFeeCollectionBO.CancellationReason = "";
                objFeeCollectionBO.AcademicYear = ViewState["AcademicYear"].ToString();
                objFeeCollectionBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                objFeeCollectionBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                string FeesCollectionTIDs = string.Empty;
                int Count = 0;

                ViewState["TrustName"] = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);

                foreach (GridViewRow row in gvFees.Rows)
                {
                    ViewState["ClassTemplateIDs"] = Convert.ToInt32(row.Cells[0].Text);

                    if (((CheckBox)row.FindControl("chkChild")).Checked)
                    {
                        TextBox txt = (TextBox)row.FindControl("lblAcademicYear");
                        string LID = Convert.ToString(row.Cells[10].Text);
                        ViewState["LedgerID"] = Convert.ToInt32(row.Cells[10].Text);
                        //int DIV = Convert.ToInt32(row.Cells[11].Text);
                        ViewState["Class"] = Convert.ToInt32(row.Cells[11].Text);
                        ViewState["Division"] = Convert.ToInt32(row.Cells[12].Text);
                        ViewState["Year"] = txt.Text;
                        lblFinancialYear.Text = GetCurrentFinancialYear();
                        lblFYear.Text = GetCurrentFinancialYear();
                        FeesCollectionTIDs += ViewState["ClassTemplateIDs"].ToString() + ",";
                        Count = Count + 1;
                    }
                }
                objFeeCollectionBO.ClassMID = Convert.ToInt32(ViewState["Class"].ToString());
                objFeeCollectionBO.DivisionTID = Convert.ToInt32(ViewState["Division"].ToString());
                objFeeCollectionBO.AcademicYear = ViewState["Year"].ToString();
                objFeeCollectionBO.ClassWiseTemplateIDs = FeesCollectionTIDs.TrimEnd(',');
                objFeeCollectionBO.ModeOfPayment = Convert.ToInt32(ddlModeOfPayment.SelectedValue);
                objFeeCollectionBO.AccountNumber = Convert.ToString(ddlBankList.SelectedItem.Text);
                objFeeCollectionBO.ChequeNo = Convert.ToString(txtChequeNo.Text);
                objFeeCollectionBO.BankName = Convert.ToString(txtBankName.Text);
                objFeeCollectionBO.BranchName = Convert.ToString(txtBranchName.Text);
                objFeeCollectionBO.IFSCODE = Convert.ToString(txtIFSCode.Text);

                ViewState["ModeOfPayment"] = Convert.ToString(ddlModeOfPayment.SelectedItem.Text);
                ViewState["AccountNumber"] = Convert.ToString(ddlBankList.SelectedItem.Text);
                //objFeeCollectionTBO
                // jornal M
                objReceiptPaymentBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                objReceiptPaymentBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                //objReceiptPaymentBO.SchoolMID = 0;
                objReceiptPaymentBO.ReceiptPaymentDate = txtdate.Text;
                //objReceiptPaymentBO.Year = Convert.ToInt32(Session[ApplicationSession.FINANCIALYEAR]);
                objReceiptPaymentBO.TransactionType = "Receipt";
                objReceiptPaymentBO.GeneralLedger = Convert.ToInt32(ddlBankList.SelectedValue);
                objReceiptPaymentBO.Narration = "Fee recieved from student: " + lblStudentNameEng.Text + " in Class " + lblClassDivision.Text + "/" + ViewState["AcademicYear"].ToString();
                objReceiptPaymentBO.CreatedBy = Convert.ToInt32(Session[ApplicationSession.USERID]);
                objReceiptPaymentBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString("dd/MM/yyyy");
                objReceiptPaymentBO.IsDeleted = 0;
                objReceiptPaymentBO.LastModifideDate = DateTime.UtcNow.AddHours(5.5).ToString("dd/MM/yyyy");
                objReceiptPaymentBO.LastModifideUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);

                string[] words = ViewState["AcademicYear"].ToString().Split('-');
                string Year = words[0] + words[1];
                objReceiptPaymentBO.Year = Convert.ToInt32(Year);

                //if (initialInsert == 0)
                //{
                //objJournalVoucherMBO.LedgerID = 2;
                //objJournalVoucherMBO.TransactionType = "Debit";
                //objJournalVoucherMBO.Amount = Convert.ToDouble(txtAmountPaid.Text);

                DatabaseTransaction.OpenConnectionTransation();

                //objResultsJM = objJournalVoucherMBL.JournalVoucherM_Insert(objJournalVoucherMBO);
                //initialInsert++;
                //if (objResultsJM != null)
                //{
                //    DataTable dt = new DataTable();
                //    dt = objResultsJM.resultDT;
                //    if (initialInsert == 1)
                //    {
                //        if (dt.Rows.Count > 0)
                //        {
                //            if (dt.Rows[0][0].ToString() == "Fail")
                //            {
                //                DatabaseTransaction.RollbackTransation();
                //                //ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Please Initialize Voucher Start No. For This Year.');</script>");
                //                initialInsert = 100;// not allow to go in for loop and any transactin and master entry insert
                //            }
                //            else
                //            {
                //                ViewState["VoucherNoJM"] = Convert.ToInt32(dt.Rows[0][0]);
                //                ViewState["VoucherCode"] = dt.Rows[0][1].ToString();
                //                objJournalVoucherMBO.VoucherNo = Convert.ToInt32(ViewState["VoucherNoJM"]);
                //                objJournalVoucherMBO.VoucherCode = ViewState["VoucherCode"].ToString();
                //            }
                //        }
                //    }

                //}
                //}
                //Total = Total + txtFeesAmount;
                objResults = objFeeCollectionBL.FeesCollection_Insert(objFeeCollectionBO);
                if (objResults != null)
                {
                    ViewState["FeesCollectionMID"] = Convert.ToInt32(objResults.resultDT.Rows[0][0].ToString());
                    ViewState["VoucherNo"] = Convert.ToInt32(objResults.resultDT.Rows[0][1].ToString());
                    if (initialInsert != 100)
                    {
                        for (int i = 0; i < gvFees.Rows.Count; i++)
                        {
                            if (((CheckBox)gvFees.Rows[i].FindControl("chkChild")).Checked)
                            {
                                ViewState["ClassWiseFeesTemplateTID"] = Convert.ToInt32(gvFees.Rows[i].Cells[0].Text);
                                //int LID = Convert.ToInt32(gvFees.Rows[i].Cells[10].Text);
                                ViewState["LedgerID"] = Convert.ToInt32(gvFees.Rows[i].Cells[10].Text);
                                objFeeCollectionTBO.FeesCollectionMID = Convert.ToInt32(ViewState["FeesCollectionMID"].ToString());
                                TextBox txtDiscount = (TextBox)gvFees.Rows[i].Cells[4].FindControl("txtDiscountAmount");
                                TextBox txtFeesAmount = (TextBox)gvFees.Rows[i].Cells[6].FindControl("txtTotalAmount");
                                TextBox txtRemainingAmount = (TextBox)gvFees.Rows[i].Cells[7].FindControl("txtRemainingAmount");
                                objFeeCollectionTBO.Discount = Convert.ToInt32(txtDiscount.Text);
                                objFeeCollectionTBO.FeesAmount = Convert.ToInt32(txtFeesAmount.Text);
                                objFeeCollectionTBO.RemainingAmount = Convert.ToDouble(txtRemainingAmount.Text);
                                objFeeCollectionTBO.ClassWiseFeesTemplateTID = Convert.ToInt32(ViewState["ClassWiseFeesTemplateTID"]);
                                objFeeCollectionTBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                                objFeeCollectionTBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                objResults = objFeeCollectionBL.FeesCollectionT_Insert(objFeeCollectionTBO);
                                if (objResults != null)
                                {
                                    //ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Fees are Collected.');</script>");
                                }
                                // insertion of Journal Voucher M

                                if (initialInsert == 0)
                                {
                                    //objJournalVoucherMBO.LedgerID = Convert.ToInt32(ViewState["LedgerID"].ToString());
                                    //objJournalVoucherMBO.TransactionType = "Credit";
                                    //objJournalVoucherMBO.Amount = Convert.ToInt32(txtFeesAmount.Text);
                                    //// initialInsert++;
                                    //objResultsJM = objJournalVoucherMBL.JournalVoucherM_Insert(objJournalVoucherMBO);
                                    if (ddlModeOfPayment.SelectedValue == "1")
                                    {
                                        objReceiptPaymentBO.TransactionMode = "Cash";
                                    }
                                    else if (ddlModeOfPayment.SelectedValue == "2" || ddlModeOfPayment.SelectedValue == "3")
                                    {
                                        objReceiptPaymentBO.TransactionMode = "Bank";
                                        objReceiptPaymentBO.BankName = txtBankName.Text;
                                        objReceiptPaymentBO.BranchName = txtBranchName.Text;
                                        if (txtChequeNo.Text.Length > 0)
                                            objReceiptPaymentBO.ChequeNo = Convert.ToInt32(txtChequeNo.Text);
                                    }
                                    //objJournalVoucherMBO.LedgerID = Convert.ToInt32(ViewState["LedgerID"].ToString());
                                    objReceiptPaymentBO.LedgerID = Convert.ToInt32(ViewState["LedgerID"].ToString());
                                    //objJournalVoucherMBO.TransactionType = "Credit";
                                    //objJournalVoucherMBO.Amount = Convert.ToInt32(txtFeesAmount.Text);
                                    objReceiptPaymentBO.Amount = Convert.ToInt32(txtFeesAmount.Text);
                                    objReceiptPaymentBO.SectionMID = 0;
                                    objReceiptPaymentBO.BudgetCategoryMID = 0;
                                    objReceiptPaymentBO.BudgetHeadingMID = 0;
                                    objReceiptPaymentBO.BudgetSubHeadingMID = 0;
                                    //// initialInsert++;
                                    //objResultsJM = objJournalVoucherMBL.JournalVoucherM_Insert(objJournalVoucherMBO);
                                    objResultInsert = objReceiptPaymentBL.ReceiptPayment_Insert(objReceiptPaymentBO);
                                    if (objResultInsert != null)
                                    {
                                        DataTable dt = new DataTable();
                                        dt = objResultInsert.resultDT;
                                        if (initialInsert == 0)
                                        {
                                            if (dt.Rows.Count > 0)
                                            {
                                                if (dt.Rows[0][0].ToString() == "Fail")
                                                {
                                                    DatabaseTransaction.RollbackTransation();
                                                    //ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Please Initialize Voucher Start No. For This Year.');</script>");
                                                    initialInsert = 100;// not allow to go in for loop and any transactin and master entry insert
                                                }
                                                else
                                                {
                                                    ViewState["VoucherNoJM"] = Convert.ToInt32(dt.Rows[0][0]);
                                                    ViewState["VoucherCode"] = dt.Rows[0][1].ToString();
                                                    objReceiptPaymentBO.ReceiptPaymentNo = Convert.ToInt32(ViewState["VoucherNoJM"]);
                                                    objReceiptPaymentBO.ReceiptPaymentCode = ViewState["VoucherCode"].ToString();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //insertLedgerTransaction(ViewState["VoucherCode"].ToString());
                    }
                    DatabaseTransaction.CommitTransation();
                    BindFeesGrid();

                    string strToDate = DateTime.UtcNow.AddHours(5.5).ToShortDateString();
                    string LastTwoDigit, strYear;
                    int intNo;
                    LastTwoDigit = strToDate.Substring(strToDate.Length - 2);
                    intNo = Convert.ToInt32(LastTwoDigit) - 1;
                    strYear = intNo.ToString() + '-' + LastTwoDigit;

                    lblStudentName.Text = lblStudentNameEng.Text;
                    lblVoucherNo.Text = GetCurrentFinancialYear() + "/" + ViewState["VoucherNo"].ToString();
                    Session["FYear"] = GetCurrentFinancialYear();
                    lblStd.Text = lblClassDivision.Text;
                    lblDate.Text = txtdate.Text;
                    lblStudentName1.Text = lblStudentNameEng.Text;
                    lblVoucherNo1.Text = GetCurrentFinancialYear() + "/" + ViewState["VoucherNo"].ToString();
                    lblStd1.Text = lblClassDivision.Text;
                    lblDate1.Text = txtdate.Text;
                    BindFeesReport();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('divFeeCollectionPrint');", true);
                    txtdate.Text = "";
                    ViewState["AcademicYear"] = "";
                    divFeePanel.Visible = false;
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Please Initialize Voucher Start No. For This Year.');</script>");
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

        #region Financial Year
        public static string GetCurrentFinancialYear()
        {
            int CurrentYear = DateTime.UtcNow.AddHours(5.5).Year;
            int PreviousYear = DateTime.UtcNow.AddHours(5.5).Year - 1;
            int NextYear = DateTime.UtcNow.AddHours(5.5).Year + 1;
            string PreYear = PreviousYear.ToString();
            string NexYear = NextYear.ToString();
            string CurYear = CurrentYear.ToString();
            string FinYear = null;

            if (DateTime.UtcNow.AddHours(5.5).Month > 3)
                FinYear = CurYear.Substring(2, 2) + "-" + NexYear.Substring(2, 2);
            else
                FinYear = PreYear.Substring(2, 2) + "-" + CurYear.Substring(2, 2);
            return FinYear.Trim();
        }
        #endregion

        #region Go for Searching Student
        protected void btnGo_Click(object sender, EventArgs e)
        {
            gvFees.Visible = false;
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

        #region InsertDataIntoTransactionTable
        public void insertLedgerTransaction(string strVoucherCode)
        {
            JournalVoucherMBL objJournalVoucherMBL = new JournalVoucherMBL();
            JournalVoucherTBL objJournalVoucherTBL = new JournalVoucherTBL();
            JournalVoucherTBO objJournalVoucherTBO = new JournalVoucherTBO();
            ApplicationResult objResultSelect = new ApplicationResult();
            ApplicationResult objResultInsert = new ApplicationResult();
            DataTable dt = new DataTable();

            objResultSelect = objJournalVoucherMBL.JournalVoucherM_Select(strVoucherCode, Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
            if (objResultSelect != null)
            {
                dt = objResultSelect.resultDT;
                if (dt.Rows.Count > 0)
                {
                    int intJournalID = Convert.ToInt32(dt.Rows[0][0].ToString());
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        int intOppJpurnaaID = Convert.ToInt32(dt.Rows[i][0].ToString());

                        objJournalVoucherTBO.JournalID = intJournalID;
                        objJournalVoucherTBO.OppositeJournalID = intOppJpurnaaID;
                        objJournalVoucherTBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString("dd/MM/yyyy");
                        objJournalVoucherTBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                        objJournalVoucherTBO.IsDeleted = 0;
                        objJournalVoucherTBO.LastModifideDate = DateTime.UtcNow.AddHours(5.5).ToString("dd/MM/yyyy");
                        objJournalVoucherTBO.LastModifideUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                        objResultInsert = objJournalVoucherTBL.JournalVoucherT_Insert(objJournalVoucherTBO);
                    }
                }
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        #region GridView PastFees RowCommand
        protected void gvPastFees_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToString() == "Print1")
                {
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                    int ReceiptNo = Convert.ToInt32(e.CommandArgument.ToString());
                    int rowIndex = gvr.RowIndex;
                    Label lblRecNo = (Label)gvPastFees.Rows[rowIndex].Cells[0].FindControl("lblRcNo");
                    string strDate = gvPastFees.Rows[rowIndex].Cells[2].Text;
                    string strName = lblStudentNameEng.Text;
                    string strClass = lblClassDivision.Text;
                    lblFinancialYear.Text = lblRecNo.Text.Split('/')[0];
                    lblVoucherNo.Text = lblRecNo.Text;
                    lblDate.Text = strDate;
                    lblStudentName.Text = strName;
                    lblStd.Text = strClass.Split(' ')[0];
                    lblFYear.Text = lblRecNo.Text.Split('/')[0];
                    lblVoucherNo1.Text = lblRecNo.Text;
                    lblDate1.Text = strDate;
                    lblStudentName1.Text = strName;
                    lblStd1.Text = strClass.Split(' ')[0];

                    FeesCollectionBL objFeeCollectionBL = new FeesCollectionBL();
                    ApplicationResult objResults = new ApplicationResult();

                    objResults = objFeeCollectionBL.FeesCollection_RePrint(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), ReceiptNo, Convert.ToInt32((ViewState["StudentMID"])));
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                           // gvReport.Visible = true;
                            gvReport.DataSource = objResults.resultDT;
                            gvReport.DataBind();

                            //gvReport1.Visible = true;
                            gvReport1.DataSource = objResults.resultDT;
                            gvReport1.DataBind();



                            dlBindFeesGrid.DataSource = objResults.resultDT;
                            dlBindFeesGrid.DataBind();

                            dlBindFeesGridForSchool.DataSource = objResults.resultDT;
                            dlBindFeesGridForSchool.DataBind();

                            //dlBindFeesGridForClassTeacher.DataSource = objResults.resultDT;
                            //dlBindFeesGridForClassTeacher.DataBind();

                            dlBindFeesGridForBank.DataSource = objResults.resultDT;
                            dlBindFeesGridForBank.DataBind();

                            int CurrentPayVal=0;
                            foreach(DataRow dr in objResults.resultDT.Rows)
                            {
                                CurrentPayVal += Convert.ToInt32(dr["CurrentlyPaid"]);
                            }
                            ViewState["TotalPaidAmount"] = CurrentPayVal;
                            ViewState["AMTinWords"] = ConvertInWords(Convert.ToInt32(CurrentPayVal));


                            //DataList dlBindFeesGrid = new DataList();
                            //dlBindFeesGrid = (DataList)e.Item.FindControl("dlBindFeesGrid");
                            //dlBindFeesGrid.DataSource = objResults.resultDT;
                            //dlBindFeesGrid.DataBind();

                        }
                        else
                        {
                            gvReport.Visible = false;
                            gvReport1.Visible = false;
                        }
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('divFeeCollectionPrint');", true);
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

        #region Bind Mode of Payment Drop Down
        public void BindModeOfPayment()
        {
            ModeOfPaymentBO objModeOfPaymentBO = new ModeOfPaymentBO();
            ModeOfPaymentBL objModeOfPaymentBL = new ModeOfPaymentBL();
            ApplicationResult objResultSelectAll = new ApplicationResult();

            objResultSelectAll = objModeOfPaymentBL.ModeofPayment_SelectAll();
            if (objResultSelectAll != null)
            {
                DataTable dtSelectAll = new DataTable();
                dtSelectAll = objResultSelectAll.resultDT;
                if (dtSelectAll.Rows.Count > 0)
                {
                    objControl.BindDropDown_ListBox(dtSelectAll, ddlModeOfPayment, "ModeOfPaymentName", "ID");
                    ddlModeOfPayment.Items.Insert(0, new ListItem("--Select--", ""));
                }
            }
        }


        #endregion

        #region Bind Bank List
        public void BindBankList()
        {
            BankAssociationBO objBankAssociationBO = new BankAssociationBO();
            BankAssociationBL objBankAssociationBL = new BankAssociationBL();

            ApplicationResult objResultSelectAll = new ApplicationResult();

            objResultSelectAll = objBankAssociationBL.BankAssociation_SelectAll();
            if (objResultSelectAll != null)
            {
                DataTable dtSelectAll = new DataTable();
                dtSelectAll = objResultSelectAll.resultDT;
                if (dtSelectAll.Rows.Count > 0)
                {
                    objControl.BindDropDown_ListBox(dtSelectAll, ddlBankList, "AccountNo", "BankAssociationMID");
                    ddlBankList.Items.Insert(0, new ListItem("--Select--", ""));
                }
            }
        }
        protected void ddlModeOfPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlModeOfPayment.SelectedValue != "")
                {
                    GeneralLedgerBL objGeneralLedgerBL = new GeneralLedgerBL();
                    GeneralLedgerBO objGeneralLedgerBO = new GeneralLedgerBO();
                    ApplicationResult objResultSelect = new ApplicationResult();
                    DataTable dtSelect = new DataTable();
                    //int intSchoolID = 0;

                    if (ddlModeOfPayment.SelectedValue == "1" /*"Cash"*/)
                    {
                        txtChequeNo.Text = "";
                        txtBankName.Text = "";
                        txtBranchName.Text = "";
                        txtIFSCode.Text = "";
                        txtChequeNo.Enabled = false;
                        txtBankName.Enabled = false;
                        txtBranchName.Enabled = false;
                        txtIFSCode.Enabled = false;
                        ddlBankList.ClearSelection();

                        objResultSelect = objGeneralLedgerBL.GeneralLedger_Select_Receipt_Payment(1, Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                        //objResultSelect = objGeneralLedgerBL.GeneralLedger_Select_Receipt_Payment(2, Convert.ToInt32(Session[ApplicationSession.TRUSTID]), /*Convert.ToInt32(Session[ApplicationSession.SCHOOLID])*/ intSchoolID);
                        if (objResultSelect != null)
                        {
                            dtSelect = objResultSelect.resultDT;

                            objControl.BindDropDown_ListBox(dtSelect, ddlBankList, "AccountName", "LedgerID");
                            ddlBankList.Items.Insert(0, new ListItem("--Select--", ""));
                        }
                    }
                    else if (ddlModeOfPayment.SelectedValue == "2" /*"Cheque"*/)
                    {
                        txtChequeNo.Text = "";
                        txtBankName.Text = "";
                        txtBranchName.Text = "";
                        txtIFSCode.Text = "";
                        txtChequeNo.Enabled = true;
                        txtBankName.Enabled = true;
                        txtBranchName.Enabled = true;
                        txtIFSCode.Enabled = true;
                        ddlBankList.ClearSelection();

                        objResultSelect = objGeneralLedgerBL.GeneralLedger_Select_Receipt_Payment(2, Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                        //objResultSelect = objGeneralLedgerBL.GeneralLedger_Select_Receipt_Payment(2, Convert.ToInt32(Session[ApplicationSession.TRUSTID]), /*Convert.ToInt32(Session[ApplicationSession.SCHOOLID])*/ intSchoolID);
                        if (objResultSelect != null)
                        {
                            dtSelect = objResultSelect.resultDT;

                            objControl.BindDropDown_ListBox(dtSelect, ddlBankList, "AccountName", "LedgerID");
                            ddlBankList.Items.Insert(0, new ListItem("--Select--", ""));
                        }
                    }
                    else if (ddlModeOfPayment.SelectedValue == "3" /*"Deposit In Bank"*/)
                    {
                        txtChequeNo.Text = "";
                        txtBankName.Text = "";
                        txtBranchName.Text = "";
                        txtIFSCode.Text = "";
                        txtChequeNo.Enabled = true;
                        txtBankName.Enabled = true;
                        txtBranchName.Enabled = true;
                        txtIFSCode.Enabled = true;
                        ddlBankList.ClearSelection();

                        objResultSelect = objGeneralLedgerBL.GeneralLedger_Select_Receipt_Payment(2, Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                        //objResultSelect = objGeneralLedgerBL.GeneralLedger_Select_Receipt_Payment(2, Convert.ToInt32(Session[ApplicationSession.TRUSTID]), /*Convert.ToInt32(Session[ApplicationSession.SCHOOLID])*/ intSchoolID);
                        if (objResultSelect != null)
                        {
                            dtSelect = objResultSelect.resultDT;

                            objControl.BindDropDown_ListBox(dtSelect, ddlBankList, "AccountName", "LedgerID");
                            ddlBankList.Items.Insert(0, new ListItem("--Select--", ""));
                        }
                    }
                    else
                    {
                        txtChequeNo.Text = "";
                        txtBankName.Text = "";
                        txtBranchName.Text = "";
                        txtIFSCode.Text = "";
                        txtChequeNo.Enabled = true;
                        txtBankName.Enabled = true;
                        txtBranchName.Enabled = true;
                        txtIFSCode.Enabled = true;
                        ddlBankList.ClearSelection();

                        objResultSelect = objGeneralLedgerBL.GeneralLedger_Select_Receipt_Payment(3, Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                        if (objResultSelect != null)
                        {
                            dtSelect = objResultSelect.resultDT;

                            objControl.BindDropDown_ListBox(dtSelect, ddlBankList, "AccountName", "LedgerID");
                            ddlBankList.Items.Insert(0, new ListItem("--Select--", ""));
                        }
                    }
                }
                else
                {
                    ddlBankList.Items.Clear();
                    ddlBankList.Items.Insert(0, new ListItem("--Select--", ""));
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        protected void dlBindFeesGrid_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            //try
            //{
            //    FeesCollectionBL objFeeCollectionBL = new FeesCollectionBL();
            //    ApplicationResult objResults = new ApplicationResult();
            //    objResults = objFeeCollectionBL.FeesCollection_Report(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(ViewState["FeesCollectionMID"]));
            //    if (objResults != null)
            //    {
            //        if (objResults.resultDT.Rows.Count > 0)
            //        {
            //            DataList dlBindFeesGrid = new DataList();
            //            dlBindFeesGrid = (DataList)e.Item.FindControl("dlBindFeesGrid");
            //            dlBindFeesGrid.DataSource = objResults.resultDT;
            //            dlBindFeesGrid.DataBind();


            //        }
            //        else
            //        {

            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    logger.Error("Error", ex);
            //    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            //}
        }

        #region ConvertAmountInWords
        public string ConvertInWords(int Digits)
        {
            int amount, num, digiCount = 0, tenCrores = 0, unitCrores = 0, tenLacs = 0, unitLacs = 0, tenThousands = 0,
           unitThousands = 0, hundreds = 0, tens = 0, units = 0;
            string words, strTenCrores = "", strUnitCrores = "", strCrores = "", strTenLacs = "", strUnitLacs = "", strLacs = "",
                strTenThousands = "", strUnitThousands = "", strThousands = "", strHundreds = "", strTens = "", strUnits = "",
                strTensnUnits = "";

            amount = Digits;

            num = amount;

            #region Digicount
            while (num > 0)
            {
                num /= 10;
                digiCount++;
            }
            #endregion
            num = amount;
            #region assign digits to place value

            //Assign Ten Crores & Crores digits

            #region assignCrores
            if (digiCount > 7)
            {
                tenCrores = num / 10000000;
                unitCrores = tenCrores % 10;
                tenCrores /= 10;
                num = num - ((tenCrores * 10 + unitCrores) * 10000000);
                digiCount -= 2;
            }
            #endregion

            //Assign Ten Lacs and Lacs digits

            #region assignLacs

            if (digiCount > 5)
            {
                tenLacs = num / 100000;
                unitLacs = tenLacs % 10;
                tenLacs /= 10;
                num = num - ((tenLacs * 10 + unitLacs) * 100000);
                digiCount -= 2;
            }

            #endregion

            //Assign Ten Thousands and Thousands digits

            #region assignThousands

            if (digiCount > 3)
            {
                tenThousands = num / 1000;
                unitThousands = tenThousands % 10;
                tenThousands /= 10;
                num = num - ((tenThousands * 10 + unitThousands) * 1000);
                digiCount -= 2;
            }

            #endregion

            //Assign Hundreds, Tens and Units

            #region assignHundredsTensUnits

            if (digiCount >= 2)
            {
                hundreds = num / 100;
                num = num - (hundreds * 100);
                digiCount--;
            }

            if (digiCount >= 1)
            {
                tens = num / 10;
                num = num - (tens * 10);
                digiCount--;
            }

            if (digiCount >= 0)
            {
                units = num;
            }

            #endregion

            //Create string for Crores

            #endregion
            #region allocate strings for digits

            #region string for Crores

            if (tenCrores != 1) //If the digit for Ten Crores place is NOT zero, follow this condition
            {
                switch (tenCrores)
                {
                    case 0:
                        {
                            strTenCrores = "";
                            break;
                        }
                    case 2:
                        {
                            strTenCrores = "Twenty";
                            break;
                        }
                    case 3:
                        {
                            strTenCrores = "Thirty";
                            break;
                        }
                    case 4:
                        {
                            strTenCrores = "Forty";
                            break;
                        }
                    case 5:
                        {
                            strTenCrores = "Fifty";
                            break;
                        }
                    case 6:
                        {
                            strTenCrores = "Sixty";
                            break;
                        }
                    case 7:
                        {
                            strTenCrores = "Seventy";
                            break;
                        }
                    case 8:
                        {
                            strTenCrores = "Eighty";
                            break;
                        }
                    case 9:
                        {
                            strTenCrores = "Ninety";
                            break;
                        }
                }

                switch (unitCrores)
                {
                    case 0:
                        {
                            strUnitCrores = "";
                            break;
                        }
                    case 1:
                        {
                            strUnitCrores = "One";
                            break;
                        }
                    case 2:
                        {
                            strUnitCrores = "Two";
                            break;
                        }
                    case 3:
                        {
                            strUnitCrores = "Three";
                            break;
                        }
                    case 4:
                        {
                            strUnitCrores = "Four";
                            break;
                        }
                    case 5:
                        {
                            strUnitCrores = "Five";
                            break;
                        }
                    case 6:
                        {
                            strUnitCrores = "Six";
                            break;
                        }
                    case 7:
                        {
                            strUnitCrores = "Seven";
                            break;
                        }
                    case 8:
                        {
                            strUnitCrores = "Eight";
                            break;
                        }
                    case 9:
                        {
                            strUnitCrores = "Nine";
                            break;
                        }
                }

                if ((tenCrores == 0) & (unitCrores == 0))
                {
                    strCrores = "";
                }
                else
                {
                    strCrores = strTenCrores + " " + strUnitCrores + " " + "Crores"; //string defined for Crores
                }
            }
            else  //If Ten Crores digit's place value is 1, execute this
            {
                switch (unitCrores)
                {
                    case 0:
                        {
                            strCrores = "Ten Crores";
                            break;
                        }
                    case 1:
                        {
                            strCrores = "Eleven Crores";
                            break;
                        }
                    case 2:
                        {
                            strCrores = "Twelve Crores";
                            break;
                        }
                    case 3:
                        {
                            strCrores = "Thirteen Crores";
                            break;
                        }
                    case 4:
                        {
                            strCrores = "Fourteen Crores";
                            break;
                        }
                    case 5:
                        {
                            strCrores = "Fifteen Crores";
                            break;
                        }
                    case 6:
                        {
                            strCrores = "Sixteen Crores";
                            break;
                        }
                    case 7:
                        {
                            strCrores = "Seventeen Crores";
                            break;
                        }
                    case 8:
                        {
                            strCrores = "Eighteen Crores";
                            break;
                        }
                    case 9:
                        {
                            strCrores = "Nineteen Crores";
                            break;
                        }
                }
            }
            #endregion

            //Create string for Lacs

            #region string for Lacs

            if (tenLacs != 1)
            {
                switch (tenLacs)
                {
                    case 0:
                        {
                            strTenLacs = "";
                            break;
                        }
                    case 2:
                        {
                            strTenLacs = "Twenty";
                            break;
                        }
                    case 3:
                        {
                            strTenLacs = "Thirty";
                            break;
                        }
                    case 4:
                        {
                            strTenLacs = "Forty";
                            break;
                        }
                    case 5:
                        {
                            strTenLacs = "Fifty";
                            break;
                        }
                    case 6:
                        {
                            strTenLacs = "Sixty";
                            break;
                        }
                    case 7:
                        {
                            strTenLacs = "Seventy";
                            break;
                        }
                    case 8:
                        {
                            strTenLacs = "Eighty";
                            break;
                        }
                    case 9:
                        {
                            strTenLacs = "Ninety";
                            break;
                        }
                }

                switch (unitLacs)
                {
                    case 1:
                        {
                            strUnitLacs = "One";
                            break;
                        }
                    case 2:
                        {
                            strUnitLacs = "Two";
                            break;
                        }
                    case 3:
                        {
                            strUnitLacs = "Three";
                            break;
                        }
                    case 4:
                        {
                            strUnitLacs = "Four";
                            break;
                        }
                    case 5:
                        {
                            strUnitLacs = "Five";
                            break;
                        }
                    case 6:
                        {
                            strUnitLacs = "Six";
                            break;
                        }
                    case 7:
                        {
                            strUnitLacs = "Seven";
                            break;
                        }
                    case 8:
                        {
                            strUnitLacs = "Eight";
                            break;
                        }
                    case 9:
                        {
                            strUnitLacs = "Nine";
                            break;
                        }
                }

                if ((tenLacs == 0) & (unitLacs == 0))
                {
                    strLacs = "";
                }
                else
                {
                    strLacs = strTenLacs + " " + strUnitLacs + " " + "Lacs";
                }
            }
            else
            {
                switch (unitLacs)
                {
                    case 0:
                        {
                            strLacs = "Ten Lacs";
                            break;
                        }
                    case 1:
                        {
                            strLacs = "Eleven Lacs";
                            break;
                        }
                    case 2:
                        {
                            strLacs = "Twelve Lacs";
                            break;
                        }
                    case 3:
                        {
                            strLacs = "Thirteen Lacs";
                            break;
                        }
                    case 4:
                        {
                            strLacs = "Fourteen Lacs";
                            break;
                        }
                    case 5:
                        {
                            strLacs = "Fifteen Lacs";
                            break;
                        }
                    case 6:
                        {
                            strLacs = "Sixteen Lacs";
                            break;
                        }
                    case 7:
                        {
                            strLacs = "Seventeen Lacs";
                            break;
                        }
                    case 8:
                        {
                            strLacs = "Eighteen Lacs";
                            break;
                        }
                    case 9:
                        {
                            strLacs = "Nineteen Lacs";
                            break;
                        }
                }
            }

            #endregion

            //Create string for Thousands

            #region string for Thousands

            if (tenThousands != 1)
            {
                switch (tenThousands)
                {
                    case 0:
                        {
                            strTenThousands = "";
                            break;
                        }
                    case 2:
                        {
                            strTenThousands = "Twenty";
                            break;
                        }
                    case 3:
                        {
                            strTenThousands = "Thirty";
                            break;
                        }
                    case 4:
                        {
                            strTenThousands = "Forty";
                            break;
                        }
                    case 5:
                        {
                            strTenThousands = "Fifty";
                            break;
                        }
                    case 6:
                        {
                            strTenThousands = "Sixty";
                            break;
                        }
                    case 7:
                        {
                            strTenThousands = "Seventy";
                            break;
                        }
                    case 8:
                        {
                            strTenThousands = "Eighty";
                            break;
                        }
                    case 9:
                        {
                            strTenThousands = "Ninety";
                            break;
                        }
                }

                switch (unitThousands)
                {
                    case 1:
                        {
                            strUnitThousands = "One";
                            break;
                        }
                    case 2:
                        {
                            strUnitThousands = "Two";
                            break;
                        }
                    case 3:
                        {
                            strUnitThousands = "Three";
                            break;
                        }
                    case 4:
                        {
                            strUnitThousands = "Four";
                            break;
                        }
                    case 5:
                        {
                            strUnitThousands = "Five";
                            break;
                        }
                    case 6:
                        {
                            strUnitThousands = "Six";
                            break;
                        }
                    case 7:
                        {
                            strUnitThousands = "Seven";
                            break;
                        }
                    case 8:
                        {
                            strUnitThousands = "Eight";
                            break;
                        }
                    case 9:
                        {
                            strUnitThousands = "Nine";
                            break;
                        }
                }
                if ((tenThousands == 0) & (unitThousands == 0))
                {
                    strThousands = "";
                }
                else
                {
                    strThousands = strTenThousands + " " + strUnitThousands + " " + "Thousand";
                }
            }
            else
            {
                switch (unitThousands)
                {
                    case 0:
                        {
                            strThousands = "Ten Thousand";
                            break;
                        }
                    case 1:
                        {
                            strThousands = "Eleven Thousand";
                            break;
                        }
                    case 2:
                        {
                            strThousands = "Twelve Thousand";
                            break;
                        }
                    case 3:
                        {
                            strThousands = "Thirteen Thousand";
                            break;
                        }
                    case 4:
                        {
                            strThousands = "Fourteen Thousand";
                            break;
                        }
                    case 5:
                        {
                            strThousands = "Fifteen Thousand";
                            break;
                        }
                    case 6:
                        {
                            strThousands = "Sixteen Thousand";
                            break;
                        }
                    case 7:
                        {
                            strThousands = "Seventeen Thousand";
                            break;
                        }
                    case 8:
                        {
                            strThousands = "Eighteen Thousand";
                            break;
                        }
                    case 9:
                        {
                            strThousands = "Nineteen Thousand";
                            break;
                        }
                }
            }
            #endregion

            //Create string for Hundreds

            #region string for Hundreds

            switch (hundreds)
            {
                case 0:
                    {
                        strHundreds = "";
                        break;
                    }
                case 1:
                    {
                        strHundreds = "One Hundred";
                        break;
                    }
                case 2:
                    {
                        strHundreds = "Two Hundred";
                        break;
                    }
                case 3:
                    {
                        strHundreds = "Three Hundred";
                        break;
                    }
                case 4:
                    {
                        strHundreds = "Four Hundred";
                        break;
                    }
                case 5:
                    {
                        strHundreds = "Five Hundred";
                        break;
                    }
                case 6:
                    {
                        strHundreds = "Six Hundred";
                        break;
                    }
                case 7:
                    {
                        strHundreds = "Seven Hundred";
                        break;
                    }
                case 8:
                    {
                        strHundreds = "Eight Hundred";
                        break;
                    }
                case 9:
                    {
                        strHundreds = "Nine Hundred";
                        break;
                    }
            }
            #endregion

            //Create string for Tens and Units

            #region string for Tens and Units

            if (tens != 1)
            {
                switch (tens)
                {
                    case 0:
                        {
                            strTens = "";
                            break;
                        }
                    case 2:
                        {
                            strTens = "Twenty";
                            break;
                        }
                    case 3:
                        {
                            strTens = "Thirty";
                            break;
                        }
                    case 4:
                        {
                            strTens = "Forty";
                            break;
                        }
                    case 5:
                        {
                            strTens = "Fifty";
                            break;
                        }
                    case 6:
                        {
                            strTens = "Sixty";
                            break;
                        }
                    case 7:
                        {
                            strTens = "Seventy";
                            break;
                        }
                    case 8:
                        {
                            strTens = "Eighty";
                            break;
                        }
                    case 9:
                        {
                            strTens = "Ninety";
                            break;
                        }
                }

                switch (units)
                {
                    case 1:
                        {
                            strUnits = "One";
                            break;
                        }
                    case 2:
                        {
                            strUnits = "Two";
                            break;
                        }
                    case 3:
                        {
                            strUnits = "Three";
                            break;
                        }
                    case 4:
                        {
                            strUnits = "Four";
                            break;
                        }
                    case 5:
                        {
                            strUnits = "Five";
                            break;
                        }
                    case 6:
                        {
                            strUnits = "Six";
                            break;
                        }
                    case 7:
                        {
                            strUnits = "Seven";
                            break;
                        }
                    case 8:
                        {
                            strUnits = "Eight";
                            break;
                        }
                    case 9:
                        {
                            strUnits = "Nine";
                            break;
                        }
                }
                if (amount == 0)
                {
                    strTensnUnits = "Zero";
                }
                else
                {
                    strTensnUnits = strTens + " " + strUnits;
                }
            }
            else
            {
                switch (units)
                {
                    case 0:
                        {
                            strTensnUnits = "Ten";
                            break;
                        }
                    case 1:
                        {
                            strTensnUnits = "Eleven";
                            break;
                        }
                    case 2:
                        {
                            strTensnUnits = "Twelve";
                            break;
                        }
                    case 3:
                        {
                            strTensnUnits = "Thirteen";
                            break;
                        }
                    case 4:
                        {
                            strTensnUnits = "Fourteen";
                            break;
                        }
                    case 5:
                        {
                            strTensnUnits = "Fifteen";
                            break;
                        }
                    case 6:
                        {
                            strTensnUnits = "Sixteen";
                            break;
                        }
                    case 7:
                        {
                            strTensnUnits = "Seventeen";
                            break;
                        }
                    case 8:
                        {
                            strTensnUnits = "Eighteen";
                            break;
                        }
                    case 9:
                        {
                            strTensnUnits = "Nineteen";
                            break;
                        }
                }
            }
            #endregion

            #endregion

            words = strCrores + " " + strLacs + " " + strThousands + " " + strHundreds + " " + strTensnUnits + " Rupees Only";

            return words;
        }

        #endregion

        //#region Bind Budget Category
        //public void BindBudgetCategory()
        //{
        //    try
        //    {
        //        BudgetCategoryMBL ObjBudgetHeadingSelectDropDownBL = new BudgetCategoryMBL();
        //        ApplicationResult objResult = new ApplicationResult();
        //        Controls objControls = new Controls();

        //        objResult = ObjBudgetHeadingSelectDropDownBL.BudgetHeading_SelectDropDown();
        //        if (objResult != null)
        //        {
        //            if (objResult.resultDT.Rows.Count > 0)
        //            {
        //                objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetCategory, "CategoryName", "BudgetCategoryMID");
        //                ddlBudgetCategory.Items.Insert(0, new ListItem("-Select-", ""));
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

        #region Bind Budget Category
        public void BindBudgetCategory()
        {
            try
            {
                BudgetCategoryMBL ObjBudgetHeadingSelectDropDownBL = new BudgetCategoryMBL();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                DataTable dt = new DataTable();

                objResult = ObjBudgetHeadingSelectDropDownBL.BudgetHeading_SelectDropDown();
                if (objResult != null)
                {
                    //if (objResult.resultDT.Rows.Count > 0)
                    //{
                    //    objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetCategory, "CategoryName", "BudgetCategoryMID");
                    //    ddlBudgetCategory.Items.Insert(0, new ListItem("-Select-", ""));
                    //}
                    DataTable dtResult = objResult.resultDT;
                    if (dtResult.Rows.Count > 0)
                    {
                        lblBudgetCategory.Text = dtResult.Rows[2][1].ToString();
                        id = Convert.ToInt32(dtResult.Rows[2][0].ToString());
                        hfBudgetCategory.Value = id.ToString();
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

        #region Section Wise Class
        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindClass();
        }
        #endregion
        
        #region Division Details Class Wise
        protected void ddlClassName_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDivision();
        }
        #endregion

        //protected void ddlBudgetHeading_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        BudgetCapitalCostBL ObjBudgetCapitalCostBL = new BudgetCapitalCostBL();
        //        ApplicationResult objResultSH = new ApplicationResult();
        //        Controls objControlsSH = new Controls();

        //        DropDownList ddlBudgetSubHeading = new DropDownList();
        //        DropDownList ddlBudgetHeading = (DropDownList)sender;
        //        GridViewRow gr = (GridViewRow)ddlBudgetSubHeading.NamingContainer;

        //        int hid = Convert.ToInt32(ddlBudgetHeading.SelectedValue);
        //        if (hid > 0)
        //        {
        //            objResultSH = ObjBudgetCapitalCostBL.BudgetSubHeading_SelectDropdown(hid);
        //            if (objResultSH != null)
        //            {
        //                DataTable dtSelectAllSH = new DataTable();
        //                dtSelectAllSH = objResultSH.resultDT;

        //                if (dtSelectAllSH.Rows.Count > 0) 
        //                {
        //                    //GridViewRow row = gvFees.SelectedRow;
        //                      Control ddl  = gr.Cells[5].FindControl("ddlBudgetSubHeading") as DropDownList;
        //                    //ddlBudgetSubHeading = (DropDownList)Rows[0]FindControl("ddlBudgetSubHeading");


        //                    for (int i = 0; i < dtSelectAllSH.Rows.Count; i++)
        //                    {
        //                        objControlsSH.BindDropDown_ListBox(dtSelectAllSH, ddlBudgetSubHeading, "SubHeadingName", "BudgetSubHeadingMID");
        //                        id1 = Convert.ToInt32(dtSelectAllSH.Rows[i][0].ToString());
        //                    }

        //                    //ddlBudgetHeading.Items.Insert(0, new ListItem("", "-1"));
        //                    //ddlBudgetSubHeading.Items.Insert(0, "--Select--");
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Error", ex);
        //        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
        //    }
        //}
    }
}