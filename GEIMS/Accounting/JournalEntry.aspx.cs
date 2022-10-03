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
using GEIMS.DataAccess;

namespace GEIMS.Accounting
{
    public partial class JournalEntry : PageBase
    {
        #region declaration
        private static ILog logger = LogManager.GetLogger(typeof(JournalEntry));
        Controls objControl = new Controls();
        DataTable dtGridData = new DataTable();

        int TrustID = 0;
        int SchoolID = 0;
        int CategoryID =0;
        int HeadID = 0;
        int SubheadID = 0;
        int SectionMID = 0;
        double amt1 = 0;
        double amt2 = 0;
        double total = -1;
        string FAYEAR = "";
        double UnutilisedamtJ = 0;
        #endregion

        #region Pre-Init Method
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.QueryString["mode"] == "TU")
            {
                MasterPageFile = "~/Master/TrustMain.Master";
            }
        }
        #endregion

        #region pagae Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //ddlSection.Enabled = true;
            //ddlBudgetCategory.Enabled = true;
            //ddlBudgetHeading.Enabled = true;
            //ddlBudgetSubHeading.Enabled = true;
            if (!IsPostBack)
            {
                ddlBudgetCategory.Items.Insert(0, new ListItem("-Select-", ""));
                ddlBudgetHeading.Items.Insert(0, new ListItem("-Select-", ""));
                ddlBudgetSubHeading.Items.Insert(0, new ListItem("-Select-", ""));
            }
            if (Convert.ToInt32(Session[ApplicationSession.HASACCESSACCOUNTUSERID]) > 0)
            {
                
                if (!IsPostBack)
                {
                    if (Request.QueryString["mode"] == "TU")
                    {
                        lblDuration.Text = Session[ApplicationSession.TRUSTNAME].ToString() + ". Account Duration : " + Session[ApplicationSession.ACCOUNTFROMDATE].ToString() + " To " + Session[ApplicationSession.ACCOUNTTODATE].ToString();
                    }
                    else
                    {
                        if (Convert.ToInt32(Session[ApplicationSession.SCHOOLID]) == 0)
                        {
                            lblDuration.Text = Session[ApplicationSession.TRUSTNAME].ToString() + ". Account Duration : " + Session[ApplicationSession.ACCOUNTFROMDATE].ToString() + " To " + Session[ApplicationSession.ACCOUNTTODATE].ToString();
                        }
                        else
                            lblDuration.Text = Session[ApplicationSession.SCHOOLNAME].ToString() + ". Account Duration : " + Session[ApplicationSession.ACCOUNTFROMDATE].ToString() + " To " + Session[ApplicationSession.ACCOUNTTODATE].ToString();
                    }

                    GetMaxDate();
                    txtVoucherCode.Attributes.Add("readonly", "readonly");

                    DataTable dt = new DataTable();
                    dt.Rows.Clear();
                    dt.Columns.Add("NO");
                    for (int i = 1; i <= 10; i++)
                    {
                        dt.Rows.Add(this.ToString());
                    }
                    GvEntry.DataSource = dt;
                    GvEntry.DataBind();
                    BindGrid();
                    BindSection();
                    BindBudgetCategory();
                    BindAccountGroup();
                    PanelVisibility(1);
                    txtDate.Attributes.Add("readonly", "readonly");
                }

                if (ViewState["grid"] != null)
                {
                    dtGridData = (DataTable)ViewState["grid"];
                    dtGridData.Rows.Clear();

                    for (int i = 0; i < GvEntry.Rows.Count; i++)
                    {
                        DataRow dr = dtGridData.NewRow();

                        dr[0] = ((DropDownList)(GvEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).SelectedItem.Value;
                        dr[1] = ((TextBox)(GvEntry.Rows[i].Cells[1].FindControl("txtDebitAmount"))).Text;
                        dr[2] = ((TextBox)(GvEntry.Rows[i].Cells[2].FindControl("txtCreditAmount"))).Text;

                        dtGridData.Rows.Add(dr);
                    }
                    ViewState["grid"] = dtGridData;
                }
                else
                {
                    dtGridData.Rows.Clear();
                    dtGridData.Columns.Add("AccountID");
                    dtGridData.Columns.Add("DebitAmount");
                    dtGridData.Columns.Add("CreditAmount");

                    ViewState["grid"] = dtGridData;
                }
                setControlScript();
            }
            else
            {
                if (Convert.ToInt32(Session[ApplicationSession.SCHOOLID]) == 0)
                    Response.Redirect("../Accounting/AccountLogin.aspx?mode=TU", false);
                else
                    Response.Redirect("../Accounting/AccountLogin.aspx", false);
            }
        }
        #endregion

        #region Bind Section
        public void BindSection()
        {
            try
            {
                SectionBL objSectionBL = new SectionBL();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();

                objResult = objSectionBL.Section_SelectAll(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlSection, "SectionName", "SectionMID");
                        ddlSection.Items.Insert(0, new ListItem("-Select-", "0"));
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

        #region Bind Budget Category
        public void BindBudgetCategory()
        {
            try
            {
                BudgetCategoryMBL ObjBudgetHeadingSelectDropDownBL = new BudgetCategoryMBL();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();

                objResult = ObjBudgetHeadingSelectDropDownBL.BudgetHeading_SelectDropDown();
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetCategory, "CategoryName", "BudgetCategoryMID");
                        ddlBudgetCategory.Items.Insert(0, new ListItem("-Select-", "0"));
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

        #region Bind Category Wise Heading
        protected void ddlBudgetCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlBudgetCategory.SelectedIndex != 0)
                {
                    BudgetSubHeadingMBL ObjBudgetSubHeadingMBL = new BudgetSubHeadingMBL();
                    ApplicationResult objResult = new ApplicationResult();
                    Controls objControls = new Controls();

                    objResult = ObjBudgetSubHeadingMBL.BudgetHeading_SelectDropDownByCapId(Convert.ToInt32(ddlBudgetCategory.SelectedValue));
                    if (objResult != null)
                    {
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetHeading, "HeadingName", "BudgetHeadingMID");
                            ddlBudgetHeading.Items.Insert(0, new ListItem("-Select-", "0"));
                            ddlBudgetSubHeading.ClearSelection();
                            txtUnutilizedBudget.Text = "0";
                        }
                        else
                        {
                            objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetHeading, "HeadingName", "BudgetHeadingMID");
                            //ddlBudgetHeading.Items.Insert(0, new ListItem("-Select-", "0"));
                            //ddlBudgetSubHeading.Items.Insert(0, new ListItem("-Select-", "0"));
                           
                            ddlBudgetHeading.ClearSelection();
                            ddlBudgetSubHeading.ClearSelection();
                            txtUnutilizedBudget.Text = "0";
                        }
                    }
                }
                else if (ddlBudgetCategory.SelectedValue == "0")
                {     
                    ddlBudgetHeading.ClearSelection();
                    ddlBudgetSubHeading.ClearSelection();
                    txtUnutilizedBudget.Text = "0";
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Bind Heading wise Subheading
        protected void ddlBudgetHeading_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlBudgetHeading.SelectedIndex != 0)
                {
                    BudgetCapitalCostBL ObjBudgetCapitalCostBL = new BudgetCapitalCostBL();
                    ApplicationResult objResult = new ApplicationResult();
                    Controls objControls = new Controls();

                    int id = Convert.ToInt32(ddlBudgetHeading.SelectedValue);

                    if (id > 0)
                    {
                        objResult = ObjBudgetCapitalCostBL.BudgetSubHeading_SelectDropdown(id);
                        if (objResult != null)
                        {
                            if (objResult.resultDT.Rows.Count > 0)
                            {
                                objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetSubHeading, "SubHeadingName", "BudgetSubHeadingMID");
                                ddlBudgetSubHeading.Items.Insert(0, new ListItem("-Select-", "0"));
                                ddlBudgetSubHeading.ClearSelection();
                                txtUnutilizedBudget.Text = "0";
                            }
                            else
                            {
                                objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetSubHeading, "SubHeadingName", "BudgetSubHeadingMID");
                                ddlBudgetSubHeading.Items.Insert(0, new ListItem("-Select-", "0"));
                                ddlBudgetSubHeading.ClearSelection();
                                txtUnutilizedBudget.Text = "0";
                            }
                        }
                    }
                    else
                    {
                        ddlBudgetSubHeading.ClearSelection();
                        txtUnutilizedBudget.Text = "0";
                    }
                }
                else
                {
                    ddlBudgetSubHeading.ClearSelection();
                    txtUnutilizedBudget.Text = "0";
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Bind Grid Voucher
        public void BindGrid()
        {
            try
            {
                JournalVoucherMBL objJournalVoucherMBL = new JournalVoucherMBL();
                JournalVoucherMBO objJournalVoucherMBO = new JournalVoucherMBO();
                ApplicationResult objResultSelectAll = new ApplicationResult();

                objResultSelectAll = objJournalVoucherMBL.JournalVoucherM_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(Session[ApplicationSession.FINANCIALYEAR]), "Journal", Session[ApplicationSession.ACCOUNTFROMDATE].ToString(), Session[ApplicationSession.ACCOUNTTODATE].ToString());
                if (objResultSelectAll != null)
                {
                    DataTable dtSelectAll = new DataTable();
                    dtSelectAll = objResultSelectAll.resultDT;
                    gvJournalEntry.DataSource = dtSelectAll;
                    gvJournalEntry.DataBind();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Bind Account Group
        public void BindAccountGroup()
        {
            GeneralLedgerBL objGeneralLedgerBL = new GeneralLedgerBL();
            GeneralLedgerBO objGeneralLedgerBO = new GeneralLedgerBO();
            ApplicationResult objResultSelectAll = new ApplicationResult();
            DropDownList ddlAccountName = new DropDownList();
            objResultSelectAll = objGeneralLedgerBL.GeneralLedger_SelectAll_JournalEntry(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
            if (objResultSelectAll != null)
            {
                DataTable dtSelectAll = new DataTable();
                dtSelectAll = objResultSelectAll.resultDT;

                for (int i = 0; i < GvEntry.Rows.Count; i++)
                {
                    ddlAccountName = (DropDownList)GvEntry.Rows[i].Cells[0].FindControl("ddlAccountName");
                    if (ddlAccountName != null)
                    {
                        objControl.BindDropDown_ListBox(dtSelectAll, ddlAccountName, "AccountName", "LedgerID");
                        ddlAccountName.Items.Insert(0, new ListItem("", "-1"));
                    }
                }
            }
        }
        #endregion

        #region Add new Row Button
        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                if (ViewState["data"] != null)
                {
                    GvEntry.DataSource = (DataTable)ViewState["data"];
                    GvEntry.DataBind();
                    int cnt = GvEntry.Rows.Count;
                    cnt++;

                    dt = (DataTable)ViewState["data"];
                    dt.Rows.Add(cnt.ToString());

                    GvEntry.DataSource = dt;
                    GvEntry.DataBind();
                }
                else
                {
                    int cnt = GvEntry.Rows.Count;
                    cnt++;
                    dt.Columns.Add("NO");
                    for (int i = 0; i < cnt; i++)
                    {
                        dt.Rows.Add(i.ToString());
                    }
                    GvEntry.DataSource = dt;
                    GvEntry.DataBind();
                }

                BindAccountGroup();

                ViewState["data"] = dt;

                if (ViewState["grid"] != null)
                {
                    dtGridData = (DataTable)ViewState["grid"];

                    for (int i = 0; i < dtGridData.Rows.Count; i++)
                    {
                        ((DropDownList)(GvEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).SelectedValue = dtGridData.Rows[i][0].ToString();
                        ((TextBox)(GvEntry.Rows[i].Cells[1].FindControl("txtDebitAmount"))).Text = dtGridData.Rows[i][1].ToString();
                        ((TextBox)(GvEntry.Rows[i].Cells[2].FindControl("txtCreditAmount"))).Text = dtGridData.Rows[i][2].ToString();
                    }
                }
                setControlScript();
                enableDisableText();
                getDebitCreditSum();
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region SetJavascriptToControls
        public void setControlScript()
        {
            for (int i = 0; i < GvEntry.Rows.Count; i++)
            {
                ((DropDownList)(GvEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).Attributes.Add("onchange", "javascript:makeTextBoxFocus(" + i + ");");
                ((TextBox)(GvEntry.Rows[i].Cells[1].FindControl("txtDebitAmount"))).Attributes.Add("onkeydown", "javascript:enableDisableCreditText(" + i + ");");
                ((TextBox)(GvEntry.Rows[i].Cells[2].FindControl("txtCreditAmount"))).Attributes.Add("onkeydown", "javascript:enableDisableDebitText(" + i + ");");

                ((DropDownList)(GvEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).Attributes.Add("onfocus", "javascript:clearFocus()");
                ((TextBox)(GvEntry.Rows[i].Cells[1].FindControl("txtDebitAmount"))).Attributes.Add("onfocus", "javascript:focusDebitCreditText(" + (4 * i) + ");");
                ((TextBox)(GvEntry.Rows[i].Cells[2].FindControl("txtCreditAmount"))).Attributes.Add("onfocus", "javascript:focusDebitCreditText(" + ((4 * i) + 2) + ");");

                ((TextBox)(GvEntry.Rows[i].Cells[1].FindControl("txtDebitAmount"))).Attributes.Add("onchange", "javascript:makeDebitSum();");
                ((TextBox)(GvEntry.Rows[i].Cells[2].FindControl("txtCreditAmount"))).Attributes.Add("onchange", "javascript:makeCreditSum();");
            }
            txtDate.Attributes.Add("onfocus", "javascript:clearFocus();");
            txtNarration.Attributes.Add("onfocus", "javascript:clearFocus();");

            btnAddRow.Attributes.Add("onfocus", "javascript:clearFocus();");
            btnSave.Attributes.Add("onfocus", "javascript:clearFocus();");
            //btnCancel.Attributes.Add("onfocus", "javascript:clearFocus();");

            //lnkDisplayVouchers.Attributes.Add("onfocus", "javascript:clearFocus();");
            //lnkHideVouchers.Attributes.Add("onfocus", "javascript:clearFocus();");
        }
        #endregion

        #region DisableAlternateText
        public void enableDisableText()
        {
            for (int i = 0; i < GvEntry.Rows.Count; i++)
            {
                TextBox txtDebit = ((TextBox)(GvEntry.Rows[i].Cells[1].FindControl("txtDebitAmount")));
                TextBox txtCredit = ((TextBox)(GvEntry.Rows[i].Cells[2].FindControl("txtCreditAmount")));

                if (txtDebit.Text != "")
                {
                    txtCredit.Enabled = false;
                }
                else
                {
                    txtCredit.Enabled = true;
                }

                if (txtCredit.Text != "")
                {
                    txtDebit.Enabled = false;
                }
                else
                {
                    txtDebit.Enabled = true;
                }
            }
        }
        #endregion

        #region SumOfValuesInGridviewTextboxes
        public void getDebitCreditSum()
        {
            double dblDebitSum = 0.0, dblCreditSum = 0.0;
            TextBox txtDebitAmount = new TextBox();
            TextBox txtCreditAmount = new TextBox();
            for (int i = 0; i < GvEntry.Rows.Count; i++)
            {
                txtDebitAmount = (TextBox)(GvEntry.Rows[i].Cells[1].FindControl("txtDebitAmount"));
                if (txtDebitAmount.Text.Length > 0)
                {
                    dblDebitSum += Convert.ToDouble(txtDebitAmount.Text.ToString());
                }
                txtCreditAmount = (TextBox)(GvEntry.Rows[i].Cells[2].FindControl("txtCreditAmount"));
                if (txtCreditAmount.Text.Length > 0)
                {
                    dblCreditSum += Convert.ToDouble(txtCreditAmount.Text.ToString());
                }
            }

            ((System.Web.UI.HtmlControls.HtmlInputText)(GvEntry.FooterRow.Cells[1].FindControl("txtDebitSum"))).Value = dblDebitSum.ToString();
            ((System.Web.UI.HtmlControls.HtmlInputText)(GvEntry.FooterRow.Cells[2].FindControl("txtCreditSum"))).Value = dblCreditSum.ToString();
        }
        #endregion

        #region Save button
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                UnutolisedAmountBL objUnutolisedAmountBL = new UnutolisedAmountBL();
                BudgetEntryScreenTBO objBudgetEntryScreenTBO = new BudgetEntryScreenTBO();

                ApplicationResult objResult1 = new ApplicationResult();
                DataTable dtResult1 = new DataTable();

                ApplicationResult objResult2 = new ApplicationResult();
                DataTable dtResult2 = new DataTable();

                ApplicationResult objResultCheck = new ApplicationResult();

                string strVouchersDate = txtDate.Text;
                string strFromDate = Session[ApplicationSession.ACCOUNTFROMDATE].ToString();
                string strToDate = Session[ApplicationSession.ACCOUNTTODATE].ToString();
                string strYear;
                DateTime FromDate = Convert.ToDateTime(strFromDate);
                DateTime ToDate = Convert.ToDateTime(strToDate);
                DateTime VoucherDate = Convert.ToDateTime(strVouchersDate);

                strYear = Session[ApplicationSession.FINANCIALYEAR].ToString();

                if (VoucherDate >= FromDate && VoucherDate <= ToDate)
                {
                    //intOrgID = Convert.ToInt32(Session["Org_ID"].ToString());
                    getDebitCreditSum();
                    string strDebitSum, strCreditSum;
                    strDebitSum = ((System.Web.UI.HtmlControls.HtmlInputText)(GvEntry.FooterRow.Cells[1].FindControl("txtDebitSum"))).Value;
                    strCreditSum = ((System.Web.UI.HtmlControls.HtmlInputText)(GvEntry.FooterRow.Cells[2].FindControl("txtCreditSum"))).Value;

                    //  CHECK WHETHER ANY AMOUNT IS WITHOUT ACCOUNT SELECTION

                    int intvalidate = 0;
                    for (int i = 0; i < GvEntry.Rows.Count; i++)
                    {
                        string ddlVal = ((DropDownList)(GvEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).SelectedItem.Value;
                        string strDebitAmt = ((TextBox)(GvEntry.Rows[i].Cells[1].FindControl("txtDebitAmount"))).Text;
                        string strCreditAmt = ((TextBox)(GvEntry.Rows[i].Cells[2].FindControl("txtCreditAmount"))).Text;

                        if (strDebitAmt != "" || strCreditAmt != "")
                        {
                            if (ddlVal == "-1")
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Select Account or enter amount before save journal entry.');</script>");
                                return;
                            }
                        }
                        else
                        {
                            intvalidate = intvalidate + 1;
                        }
                        if (GvEntry.Rows.Count == intvalidate)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Select Account or enter amount before save journal entry.');</script>");
                            return;
                        }
                    }
                    if (strDebitSum == strCreditSum)    //  CHECKS WHETHER THE AMOUNTS MATCHES OR NOT
                    {
                        JournalVoucherMBL objJournalVoucherMBL = new JournalVoucherMBL();
                        JournalVoucherMBO objJournalVoucherMBO = new JournalVoucherMBO();
                        //JournalVoucherTBL objJournalVoucherTBL = new JournalVoucherTBL();
                        //JournalVoucherTBO objJournalVoucherTBO = new JournalVoucherTBO();

                        DatabaseTransaction.OpenConnectionTransation();
                        if (ViewState["Mode"].ToString() == "Save")
                        {
                            if (ddlSection.SelectedIndex != 0 && ddlBudgetCategory.SelectedIndex != 0 && ddlBudgetHeading.SelectedIndex != 0 && ddlBudgetSubHeading.SelectedIndex != 0)
                            {
                                ApplicationResult objResultInsert = new ApplicationResult();
                                objJournalVoucherMBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                                objJournalVoucherMBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                                objJournalVoucherMBO.SectionMID = Convert.ToInt32(ddlSection.SelectedValue);
                                objJournalVoucherMBO.BudgetCategoryMID = Convert.ToInt32(ddlBudgetCategory.SelectedValue);
                                objJournalVoucherMBO.BudgetHeadingMID = Convert.ToInt32(ddlBudgetHeading.SelectedValue);
                                objJournalVoucherMBO.BudgetSubHeadingMID = Convert.ToInt32(ddlBudgetSubHeading.SelectedValue);
                                objJournalVoucherMBO.VoucherDate = txtDate.Text;
                                objJournalVoucherMBO.OperationType = "Journal";
                                objJournalVoucherMBO.Description = txtNarration.Text;
                                objJournalVoucherMBO.CreatedBy = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                objJournalVoucherMBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString("dd/MM/yyyy");
                                objJournalVoucherMBO.IsDeleted = 0;
                                objJournalVoucherMBO.LastModifideDate = DateTime.UtcNow.AddHours(5.5).ToString("dd/MM/yyyy");
                                objJournalVoucherMBO.LastModifideUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                objJournalVoucherMBO.Year = Convert.ToInt32(strYear);

                                #region Check Unutilsed Amout Validation
                                int catid1 = Convert.ToInt32(ddlBudgetCategory.SelectedValue);
                                double UnutilisedAmt = Convert.ToDouble(txtUnutilizedBudget.Text);
                                double Ctotal = 0;

                                for (int i = 0; i < GvEntry.Rows.Count; i++)
                                {
                                    string strAccountID = ((DropDownList)(GvEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).SelectedItem.Value;
                                    string strDebit = ((TextBox)(GvEntry.Rows[i].Cells[1].FindControl("txtDebitAmount"))).Text;
                                    string strCredit = ((TextBox)(GvEntry.Rows[i].Cells[2].FindControl("txtCreditAmount"))).Text;

                                    if ((strAccountID == "-1" && strDebit == "") || (strAccountID == "-1" && strCredit == ""))
                                    { continue; }

                                    if (strAccountID != "-1")
                                    {
                                        int intLedgerID = Convert.ToInt32(strAccountID);

                                        objResult1 = objUnutolisedAmountBL.Unutilised_Amount_Validation(intLedgerID);
                                        if (objResult1 != null)
                                        {
                                            dtResult1 = objResult1.resultDT;
                                            string GroupName = Convert.ToString(objResult1.resultDT.Rows[0][10].ToString());
                                            if (catid1 == 1 || catid1 == 2 || catid1 == 5)
                                            {
                                                if ("Expense" == GroupName)
                                                {
                                                    if (strDebit == "")
                                                    {
                                                        objJournalVoucherMBO.TransactionType = "Credit";
                                                        objJournalVoucherMBO.Amount = Convert.ToDouble(strCredit);
                                                        amt1 = Convert.ToDouble(Ctotal == 0 ? UnutilisedAmt : Ctotal);
                                                        amt2 = Convert.ToDouble(strCredit);
                                                        Ctotal = amt1 + amt2;
                                                    }
                                                    else
                                                    {
                                                        objJournalVoucherMBO.TransactionType = "Debit";
                                                        objJournalVoucherMBO.Amount = Convert.ToDouble(strDebit);
                                                        amt1 = Convert.ToDouble(Ctotal == 0 ? UnutilisedAmt : Ctotal);
                                                        amt2 = Convert.ToDouble(strDebit);
                                                        Ctotal = amt1 - amt2;
                                                    }
                                                }
                                            }
                                            if (catid1 == 3 || catid1 == 4)
                                            {
                                                if ("Income" == GroupName)
                                                {
                                                    if (strDebit == "")
                                                    {
                                                        objJournalVoucherMBO.TransactionType = "Credit";
                                                        objJournalVoucherMBO.Amount = Convert.ToDouble(strCredit);
                                                        amt1 = Convert.ToDouble(Ctotal == 0 ? UnutilisedAmt : Ctotal);
                                                        amt2 = Convert.ToDouble(strCredit);
                                                        Ctotal = amt1 - amt2;
                                                    }
                                                    else
                                                    {
                                                        objJournalVoucherMBO.TransactionType = "Debit";
                                                        objJournalVoucherMBO.Amount = Convert.ToDouble(strDebit);
                                                        amt1 = Convert.ToDouble(Ctotal == 0 ? UnutilisedAmt : Ctotal);
                                                        amt2 = Convert.ToDouble(strDebit);
                                                        Ctotal = amt1 + amt2;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                double tranval = Ctotal;
                                #endregion

                                if (tranval >= 0)
                                {
                                    for (int i = 0; i < GvEntry.Rows.Count; i++)
                                    {
                                        string strAccountID = ((DropDownList)(GvEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).SelectedItem.Value;
                                        string strDebit = ((TextBox)(GvEntry.Rows[i].Cells[1].FindControl("txtDebitAmount"))).Text;
                                        string strCredit = ((TextBox)(GvEntry.Rows[i].Cells[2].FindControl("txtCreditAmount"))).Text;

                                        if ((strAccountID == "-1" && strDebit == "") || (strAccountID == "-1" && strCredit == ""))
                                        { continue; }

                                        if (strAccountID != "-1")
                                        {
                                            objJournalVoucherMBO.LedgerID = Convert.ToInt32(strAccountID);
                                            //int intLedgerID = Convert.ToInt32(strAccountID);

                                            if (strDebit == "")
                                            {
                                                objJournalVoucherMBO.TransactionType = "Credit";
                                                objJournalVoucherMBO.Amount = Convert.ToDouble(strCredit);
                                            }
                                            else
                                            {
                                                objJournalVoucherMBO.TransactionType = "Debit";
                                                objJournalVoucherMBO.Amount = Convert.ToDouble(strDebit);
                                            }
                                            objResultInsert = objJournalVoucherMBL.JournalVoucherM_Insert(objJournalVoucherMBO);
                                            if (i == 0)
                                            {
                                                if (objResultInsert != null)
                                                {
                                                    DataTable dt = new DataTable();
                                                    dt = objResultInsert.resultDT;
                                                    if (dt.Rows.Count > 0)
                                                    {
                                                        if (dt.Rows[0][0].ToString() == "0")
                                                        {
                                                            DatabaseTransaction.RollbackTransation();
                                                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Please Initialize Voucher Start No. For This Year.');</script>");
                                                            goto Exit;
                                                        }
                                                        else
                                                        {
                                                            ViewState["VoucherNo"] = Convert.ToInt32(dt.Rows[0][0]);
                                                            ViewState["VoucherCode"] = dt.Rows[0][1].ToString();
                                                            objJournalVoucherMBO.VoucherNo = Convert.ToInt32(ViewState["VoucherNo"]);
                                                            objJournalVoucherMBO.VoucherCode = ViewState["VoucherCode"].ToString();
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                objJournalVoucherMBO.VoucherNo = Convert.ToInt32(ViewState["VoucherNo"]);
                                                objJournalVoucherMBO.VoucherCode = ViewState["VoucherCode"].ToString();
                                            }
                                        }
                                    }
                                    insertLedgerTransaction(ViewState["VoucherCode"].ToString());
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Journal Saved Successfully. Voucher No is " + ViewState["VoucherCode"].ToString() + "');</script>");
                                    Response.Headers.Add("Refresh", "0");
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Unutilise Amount is Negative " + tranval + "');</script>");
                                }
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Journal Entry is not Connected to Budget!!!!!.');</script>");

                                ApplicationResult objResultInsert = new ApplicationResult();
                                objJournalVoucherMBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                                objJournalVoucherMBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                                objJournalVoucherMBO.SectionMID = 0;
                                objJournalVoucherMBO.BudgetCategoryMID = 0;
                                objJournalVoucherMBO.BudgetHeadingMID = 0;
                                objJournalVoucherMBO.BudgetSubHeadingMID = 0;
                                objJournalVoucherMBO.VoucherDate = txtDate.Text;
                                objJournalVoucherMBO.OperationType = "Journal";
                                objJournalVoucherMBO.Description = txtNarration.Text;
                                objJournalVoucherMBO.CreatedBy = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                objJournalVoucherMBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString("dd/MM/yyyy");
                                objJournalVoucherMBO.IsDeleted = 0;
                                objJournalVoucherMBO.LastModifideDate = DateTime.UtcNow.AddHours(5.5).ToString("dd/MM/yyyy");
                                objJournalVoucherMBO.LastModifideUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                objJournalVoucherMBO.Year = Convert.ToInt32(strYear);

                                for (int i = 0; i < GvEntry.Rows.Count; i++)
                                {
                                    string strAccountID = ((DropDownList)(GvEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).SelectedItem.Value;
                                    string strDebit = ((TextBox)(GvEntry.Rows[i].Cells[1].FindControl("txtDebitAmount"))).Text;
                                    string strCredit = ((TextBox)(GvEntry.Rows[i].Cells[2].FindControl("txtCreditAmount"))).Text;

                                    if ((strAccountID == "-1" && strDebit == "") || (strAccountID == "-1" && strCredit == ""))
                                    { continue; }

                                    if (strAccountID != "-1")
                                    {
                                        objJournalVoucherMBO.LedgerID = Convert.ToInt32(strAccountID);

                                        if (strDebit == "")
                                        {
                                            objJournalVoucherMBO.TransactionType = "Credit";
                                            objJournalVoucherMBO.Amount = Convert.ToDouble(strCredit);
                                        }
                                        else
                                        {
                                            objJournalVoucherMBO.TransactionType = "Debit";
                                            objJournalVoucherMBO.Amount = Convert.ToDouble(strDebit);
                                        }
                                        objResultInsert = objJournalVoucherMBL.JournalVoucherM_Insert(objJournalVoucherMBO);
                                        if (i == 0)
                                        {
                                            if (objResultInsert != null)
                                            {
                                                DataTable dt = new DataTable();
                                                dt = objResultInsert.resultDT;
                                                if (dt.Rows.Count > 0)
                                                {
                                                    if (dt.Rows[0][0].ToString() == "0")
                                                    {
                                                        DatabaseTransaction.RollbackTransation();
                                                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Please Initialize Voucher Start No. For This Year.');</script>");
                                                        goto Exit;
                                                    }
                                                    else
                                                    {
                                                        ViewState["VoucherNo"] = Convert.ToInt32(dt.Rows[0][0]);
                                                        ViewState["VoucherCode"] = dt.Rows[0][1].ToString();
                                                        objJournalVoucherMBO.VoucherNo = Convert.ToInt32(ViewState["VoucherNo"]);
                                                        objJournalVoucherMBO.VoucherCode = ViewState["VoucherCode"].ToString();
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            objJournalVoucherMBO.VoucherNo = Convert.ToInt32(ViewState["VoucherNo"]);
                                            objJournalVoucherMBO.VoucherCode = ViewState["VoucherCode"].ToString();
                                        }
                                    }
                                }
                                insertLedgerTransaction(ViewState["VoucherCode"].ToString());
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Journal Saved Successfully. Voucher No is " + ViewState["VoucherCode"].ToString() + "');</script>");
                                Response.AddHeader("refresh", "0");
                            }
                            //DatabaseTransaction.CommitTransation();
                            //clear();
                            //PanelVisibility(1);
                            //BindGrid();
                        }
                        else if (ViewState["Mode"].ToString() == "Edit")    //  UPDATE THE EXISTING JOURNAL
                        {
                            int secID = Convert.ToInt32(hfSectionID.Value);
                            int catID = Convert.ToInt32(hfCategoryID.Value);
                            int headID = Convert.ToInt32(hfHeadID.Value);
                            int subheadID = Convert.ToInt32(hfSubHeadID.Value);
                            if (secID > 0 && catID > 0 && headID > 0 && subheadID > 0)
                            ///if (ddlSection.SelectedIndex != 0 && ddlBudgetCategory.SelectedIndex != 0 && ddlBudgetHeading.SelectedIndex != 0 && ddlBudgetSubHeading.SelectedIndex != 0)
                            {
                                ApplicationResult objResulUpdate = new ApplicationResult();

                                objJournalVoucherMBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                                objJournalVoucherMBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                                objJournalVoucherMBO.SectionMID = Convert.ToInt32(ddlSection.SelectedValue);
                                objJournalVoucherMBO.BudgetCategoryMID = Convert.ToInt32(ddlBudgetCategory.SelectedValue);
                                objJournalVoucherMBO.BudgetHeadingMID = Convert.ToInt32(ddlBudgetHeading.SelectedValue);
                                objJournalVoucherMBO.BudgetSubHeadingMID = Convert.ToInt32(ddlBudgetSubHeading.SelectedValue);
                                objJournalVoucherMBO.VoucherDate = txtDate.Text;
                                objJournalVoucherMBO.OperationType = "Journal";
                                objJournalVoucherMBO.Description = txtNarration.Text;
                                objJournalVoucherMBO.CreatedBy = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                objJournalVoucherMBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString("dd/MM/yyyy");
                                objJournalVoucherMBO.IsDeleted = 0;
                                objJournalVoucherMBO.LastModifideDate = DateTime.UtcNow.AddHours(5.5).ToString("dd/MM/yyyy");
                                objJournalVoucherMBO.LastModifideUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                objJournalVoucherMBO.Year = Convert.ToInt32(strYear);
                                objJournalVoucherMBO.VoucherNo = Convert.ToInt32(txtVoucherCode.Text.ToString().Substring(6, txtVoucherCode.Text.Length - 6));
                                objJournalVoucherMBO.VoucherCode = txtVoucherCode.Text;

                                int intActRows = 0;
                                for (int i = 0; i < GvEntry.Rows.Count; i++)
                                {
                                    string strAccId = ((DropDownList)(GvEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).SelectedItem.Value;
                                    if (strAccId != "-1")
                                        intActRows++;
                                    else
                                        break;
                                }
                                if (intActRows == Convert.ToInt32(ViewState["VoucherRow"]))   //  CHECKS WHETHER THE JOURNAL RECORD IS LARGER OR SMALLER THEN EXIXTING RECORD
                                {

                                    #region Check Unutilsed Amout Validation
                                    int catid1 = Convert.ToInt32(ddlBudgetCategory.SelectedValue);
                                    double UnutilisedAmt = Convert.ToDouble(txtUnutilizedBudget.Text);
                                    double Ctotal = 0;

                                    for (int i = 0; i < GvEntry.Rows.Count; i++)
                                    {
                                        string strAccountID = ((DropDownList)(GvEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).SelectedItem.Value;
                                        string strDebit = ((TextBox)(GvEntry.Rows[i].Cells[1].FindControl("txtDebitAmount"))).Text;
                                        string strCredit = ((TextBox)(GvEntry.Rows[i].Cells[2].FindControl("txtCreditAmount"))).Text;

                                        if ((strAccountID == "-1" && strDebit == "") || (strAccountID == "-1" && strCredit == ""))
                                        { continue; }

                                        if (strAccountID != "-1")
                                        {
                                            int intLedgerID = Convert.ToInt32(strAccountID);

                                            objResult1 = objUnutolisedAmountBL.Unutilised_Amount_Validation(intLedgerID);
                                            if (objResult1 != null)
                                            {
                                                dtResult1 = objResult1.resultDT;
                                                string GroupName = Convert.ToString(objResult1.resultDT.Rows[0][10].ToString());
                                                if (catid1 == 1 || catid1 == 2 || catid1 == 5)
                                                {
                                                    if ("Expense" == GroupName)
                                                    {
                                                        if (strDebit == "")
                                                        {
                                                            objJournalVoucherMBO.TransactionType = "Credit";
                                                            objJournalVoucherMBO.Amount = Convert.ToDouble(strCredit);
                                                            amt1 = Convert.ToDouble(Ctotal == 0 ? UnutilisedAmt : Ctotal);
                                                            amt2 = Convert.ToDouble(strCredit);
                                                            Ctotal = amt1 + amt2;
                                                        }
                                                        else
                                                        {
                                                            objJournalVoucherMBO.TransactionType = "Debit";
                                                            objJournalVoucherMBO.Amount = Convert.ToDouble(strDebit);
                                                            amt1 = Convert.ToDouble(Ctotal == 0 ? UnutilisedAmt : Ctotal);
                                                            amt2 = Convert.ToDouble(strDebit);
                                                            Ctotal = amt1 - amt2;
                                                        }
                                                    }
                                                }
                                                if (catid1 == 3 || catid1 == 4)
                                                {
                                                    if ("Income" == GroupName)
                                                    {
                                                        if (strDebit == "")
                                                        {
                                                            objJournalVoucherMBO.TransactionType = "Credit";
                                                            objJournalVoucherMBO.Amount = Convert.ToDouble(strCredit);
                                                            amt1 = Convert.ToDouble(Ctotal == 0 ? UnutilisedAmt : Ctotal);
                                                            amt2 = Convert.ToDouble(strCredit);
                                                            Ctotal = amt1 - amt2;
                                                        }
                                                        else
                                                        {
                                                            objJournalVoucherMBO.TransactionType = "Debit";
                                                            objJournalVoucherMBO.Amount = Convert.ToDouble(strDebit);
                                                            amt1 = Convert.ToDouble(Ctotal == 0 ? UnutilisedAmt : Ctotal);
                                                            amt2 = Convert.ToDouble(strDebit);
                                                            Ctotal = amt1 + amt2;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    double tranval = Ctotal;
                                    #endregion

                                    if (tranval >= 0)
                                    {
                                        for (int i = 0; i < GvEntry.Rows.Count; i++)
                                        {
                                            GvEntry.Columns[3].Visible = true;

                                            if (GvEntry.Rows[i].Cells[3].Text.ToString() != "")
                                            {
                                                string strAccountID = ((DropDownList)(GvEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).SelectedItem.Value;
                                                string strDebit = ((TextBox)(GvEntry.Rows[i].Cells[1].FindControl("txtDebitAmount"))).Text;
                                                string strCredit = ((TextBox)(GvEntry.Rows[i].Cells[2].FindControl("txtCreditAmount"))).Text;

                                                if (strAccountID != "-1")
                                                {
                                                    objJournalVoucherMBO.LedgerID = Convert.ToInt32(strAccountID);

                                                    if (strDebit == "")
                                                    {
                                                        objJournalVoucherMBO.TransactionType = "Credit";
                                                        objJournalVoucherMBO.Amount = Convert.ToDouble(strCredit);
                                                    }
                                                    else
                                                    {
                                                        objJournalVoucherMBO.TransactionType = "Debit";
                                                        objJournalVoucherMBO.Amount = Convert.ToDouble(strDebit);
                                                    }
                                                }
                                                objJournalVoucherMBO.JournalID = Convert.ToInt32(GvEntry.Rows[i].Cells[3].Text.ToString());

                                                objResulUpdate = objJournalVoucherMBL.JournalVoucherM_Update(objJournalVoucherMBO, Session[ApplicationSession.ACCOUNTFROMDATE].ToString());
                                            }
                                            GvEntry.Columns[3].Visible = false;
                                        }
                                    }
                                    else
                                    {
                                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Unutilise Amount is Negative " + tranval + "');</script>");
                                    }
                                }
                                else
                                {
                                    ApplicationResult objResultInsert = new ApplicationResult();
                                    ApplicationResult objResultDelete = new ApplicationResult();
                                    objResultDelete = objJournalVoucherMBL.JournalVoucherM_Delete_Transaction(txtVoucherCode.Text, "Journal");
                                    if (objResultDelete.status == ApplicationResult.CommonStatusType.SUCCESS)
                                    { }

                                    for (int i = 0; i < GvEntry.Rows.Count; i++)
                                    {
                                        string strAccountID = ((DropDownList)(GvEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).SelectedItem.Value;
                                        string strDebit = ((TextBox)(GvEntry.Rows[i].Cells[1].FindControl("txtDebitAmount"))).Text;
                                        string strCredit = ((TextBox)(GvEntry.Rows[i].Cells[2].FindControl("txtCreditAmount"))).Text;

                                        if (strAccountID != "-1")
                                        {
                                            objJournalVoucherMBO.LedgerID = Convert.ToInt32(strAccountID);
                                            if (strDebit == "")
                                            {
                                                objJournalVoucherMBO.TransactionType = "Credit";
                                                objJournalVoucherMBO.Amount = Convert.ToDouble(strCredit);
                                            }
                                            else
                                            {
                                                objJournalVoucherMBO.TransactionType = "Debit";
                                                objJournalVoucherMBO.Amount = Convert.ToDouble(strDebit);
                                            }
                                            objResultInsert = objJournalVoucherMBL.JournalVoucherM_Insert(objJournalVoucherMBO);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    insertLedgerTransaction(txtVoucherCode.Text);
                                }
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Journal Updated Successfully...');</script>");
                                Response.Headers.Add("Refresh", "0");
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Journal Entry is not Connected to Budget!!!!!.');</script>");

                                ApplicationResult objResulUpdate = new ApplicationResult();

                                objJournalVoucherMBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                                objJournalVoucherMBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                                objJournalVoucherMBO.SectionMID = 0;
                                objJournalVoucherMBO.BudgetCategoryMID = 0;
                                objJournalVoucherMBO.BudgetHeadingMID = 0;
                                objJournalVoucherMBO.BudgetSubHeadingMID = 0;
                                objJournalVoucherMBO.VoucherDate = txtDate.Text;
                                objJournalVoucherMBO.OperationType = "Journal";
                                objJournalVoucherMBO.Description = txtNarration.Text;
                                objJournalVoucherMBO.CreatedBy = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                objJournalVoucherMBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString("dd/MM/yyyy");
                                objJournalVoucherMBO.IsDeleted = 0;
                                objJournalVoucherMBO.LastModifideDate = DateTime.UtcNow.AddHours(5.5).ToString("dd/MM/yyyy");
                                objJournalVoucherMBO.LastModifideUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                objJournalVoucherMBO.Year = Convert.ToInt32(strYear);
                                objJournalVoucherMBO.VoucherNo = Convert.ToInt32(txtVoucherCode.Text.ToString().Substring(6, txtVoucherCode.Text.Length - 6));
                                objJournalVoucherMBO.VoucherCode = txtVoucherCode.Text;

                                int intActRows = 0;
                                for (int i = 0; i < GvEntry.Rows.Count; i++)
                                {
                                    string strAccId = ((DropDownList)(GvEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).SelectedItem.Value;
                                    if (strAccId != "-1")
                                        intActRows++;
                                    else
                                        break;
                                }
                                if (intActRows == Convert.ToInt32(ViewState["VoucherRow"]))   //  CHECKS WHETHER THE JOURNAL RECORD IS LARGER OR SMALLER THEN EXIXTING RECORD
                                {
                                    for (int i = 0; i < GvEntry.Rows.Count; i++)
                                    {
                                        GvEntry.Columns[3].Visible = true;

                                        if (GvEntry.Rows[i].Cells[3].Text.ToString() != "")
                                        {
                                            string strAccountID = ((DropDownList)(GvEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).SelectedItem.Value;
                                            string strDebit = ((TextBox)(GvEntry.Rows[i].Cells[1].FindControl("txtDebitAmount"))).Text;
                                            string strCredit = ((TextBox)(GvEntry.Rows[i].Cells[2].FindControl("txtCreditAmount"))).Text;

                                            if (strAccountID != "-1")
                                            {
                                                objJournalVoucherMBO.LedgerID = Convert.ToInt32(strAccountID);

                                                if (strDebit == "")
                                                {
                                                    objJournalVoucherMBO.TransactionType = "Credit";
                                                    objJournalVoucherMBO.Amount = Convert.ToDouble(strCredit);
                                                }
                                                else
                                                {
                                                    objJournalVoucherMBO.TransactionType = "Debit";
                                                    objJournalVoucherMBO.Amount = Convert.ToDouble(strDebit);
                                                }
                                            }
                                            objJournalVoucherMBO.JournalID = Convert.ToInt32(GvEntry.Rows[i].Cells[3].Text.ToString());

                                            objResulUpdate = objJournalVoucherMBL.JournalVoucherM_Update(objJournalVoucherMBO, Session[ApplicationSession.ACCOUNTFROMDATE].ToString());
                                        }
                                        GvEntry.Columns[3].Visible = false;
                                    }
                                }
                                else
                                {
                                    ApplicationResult objResultInsert = new ApplicationResult();
                                    ApplicationResult objResultDelete = new ApplicationResult();
                                    objResultDelete = objJournalVoucherMBL.JournalVoucherM_Delete_Transaction(txtVoucherCode.Text, "Journal");
                                    if (objResultDelete.status == ApplicationResult.CommonStatusType.SUCCESS)
                                    { }

                                    for (int i = 0; i < GvEntry.Rows.Count; i++)
                                    {
                                        string strAccountID = ((DropDownList)(GvEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).SelectedItem.Value;
                                        string strDebit = ((TextBox)(GvEntry.Rows[i].Cells[1].FindControl("txtDebitAmount"))).Text;
                                        string strCredit = ((TextBox)(GvEntry.Rows[i].Cells[2].FindControl("txtCreditAmount"))).Text;

                                        if (strAccountID != "-1")
                                        {
                                            objJournalVoucherMBO.LedgerID = Convert.ToInt32(strAccountID);
                                            if (strDebit == "")
                                            {
                                                objJournalVoucherMBO.TransactionType = "Credit";
                                                objJournalVoucherMBO.Amount = Convert.ToDouble(strCredit);
                                            }
                                            else
                                            {
                                                objJournalVoucherMBO.TransactionType = "Debit";
                                                objJournalVoucherMBO.Amount = Convert.ToDouble(strDebit);
                                            }
                                            objResultInsert = objJournalVoucherMBL.JournalVoucherM_Insert(objJournalVoucherMBO);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    insertLedgerTransaction(txtVoucherCode.Text);
                                }
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Journal Updated Successfully...');</script>");
                                Response.Headers.Add("Refresh", "0");
                            }
                            //DatabaseTransaction.CommitTransation();
                            //clear();
                            //PanelVisibility(1);
                            //BindGrid();
                        }
                        DatabaseTransaction.CommitTransation();
                        clear();
                        PanelVisibility(1);
                        BindGrid();
                    }
                    else    //  WHEN THE AMOUNTS DOESNOT MATCH
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Total amounts of Debit and Credit donot match...');</script>");
                        return;
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Select Date according to your Accounting Period.');</script>");
                    return;
                }
                GvEntry.Visible = true;
            Exit:;
            }
            catch (Exception ex)
            {
                DatabaseTransaction.RollbackTransation();
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
            finally
            {
                DatabaseTransaction.connection.Close();
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

        #region Clear Operation and Reset page
        public void clear()
        {
            objControl.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["Mode"] = "Save";
            ViewState["VoucherNo"] = "";
            ViewState["VoucherCode"] = "";
            ViewState["VoucherRow"] = "0";
            GetMaxDate();

            GvEntry.DataSource = null;
            getNewRows();
            GvEntry.DataBind();

            BindAccountGroup();
            setControlScript();
            getDebitCreditSum();
        }
        #endregion

        #region Panel Visibility Mode
        public void PanelVisibility(int intcode)
        {
            if (intcode == 1)
            {
                divGrid.Visible = true;
                tabs.Visible = false;
                btnViewList.Visible = false;
            }
            else if (intcode == 2)
            {
                divGrid.Visible = false;
                tabs.Visible = true;
                btnViewList.Visible = true;
            }
        }
        #endregion

        #region AddNewRowInGridDynamically
        public void getNewRows()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Number");
            for (int i = 1; i <= 10; i++)
            {
                dt.Rows.Add(i.ToString());
            }
            GvEntry.DataSource = dt;
            GvEntry.DataBind();
        }
        #endregion

        #region AddNew
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            PanelVisibility(2);
            clear();
            BindAcademicYear();
            ddlBudgetCategory.ClearSelection();
            ddlBudgetHeading.ClearSelection();
            ddlBudgetSubHeading.ClearSelection();
            txtUnutilizedBudget.Text = "0";
        }
        #endregion

        #region Gridview Events
        protected void gvJournalEntry_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {            
                JournalVoucherMBL objJournalVoucherMBL = new JournalVoucherMBL();
                JournalVoucherMBO objJournalVoucherMBO = new JournalVoucherMBO();
                ViewState["VoucherCode"] = e.CommandArgument.ToString();

                if (e.CommandName.ToString() == "Edit1")
                {
                    //ddlSection.Enabled = false;
                    //ddlBudgetCategory.Enabled = false;
                    //ddlBudgetHeading.Enabled = false;
                    //ddlBudgetSubHeading.Enabled = false;

                    GvEntry.SelectedIndex = -1;
                    ApplicationResult objResultSelect = new ApplicationResult();
                    objResultSelect = objJournalVoucherMBL.JournalVoucherM_Select(ViewState["VoucherCode"].ToString(), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                    if (objResultSelect != null)
                    {
                        DataTable dtSelect = new DataTable();
                        dtSelect = objResultSelect.resultDT;
                        if (dtSelect.Rows.Count > 0)
                        {
                            ViewState["VoucherRow"] = dtSelect.Rows.Count;
                            DataTable dt = new DataTable();

                            dt.Columns.Add("Number");
                            for (int i = 1; i <= Convert.ToInt32(ViewState["VoucherRow"]); i++)
                            {
                                dt.Rows.Add(i.ToString());
                            }
                            GvEntry.DataSource = dt;
                            GvEntry.DataBind();

                            BindAccountGroup();

                            txtDate.Text = dtSelect.Rows[0]["VoucherDate"].ToString();

                            //Bind DropDown Box
                            ddlSection.SelectedValue = dtSelect.Rows[0]["SectionMID"].ToString();
                            ddlBudgetCategory.SelectedValue = dtSelect.Rows[0]["BudgetCategoryMID"].ToString();
                            if (ddlSection.SelectedValue == "0" && ddlBudgetCategory.SelectedValue == "0") 
                            {
                                ddlSection.ClearSelection();
                                ddlBudgetCategory.ClearSelection();
                                ddlBudgetHeading.ClearSelection();
                                ddlBudgetSubHeading.ClearSelection();
                            }
                            else
                            {
                                BindSection();
                                ddlSection.SelectedValue = dtSelect.Rows[0]["SectionMID"].ToString();
                                hfSectionID.Value = dtSelect.Rows[0]["SectionMID"].ToString();

                                BindBudgetCategory();
                                ddlBudgetCategory.SelectedValue = dtSelect.Rows[0]["BudgetCategoryMID"].ToString();
                                hfCategoryID.Value = dtSelect.Rows[0]["BudgetCategoryMID"].ToString();
                                //txtUnutilizedBudget.Text = 

                                //Find and Fetch Heading
                                Controls objControls = new Controls();
                                Controls objControls1 = new Controls();
                                DataTable dt1 = new DataTable();
                                //BindBudgetCategory();
                                dt = FetchHeading(Convert.ToInt32(dtSelect.Rows[0][BudgetSubHeadingMBO.BudgetHeading_BUDGETCATEGORYMID]), Convert.ToInt32(dtSelect.Rows[0][BudgetSubHeadingMBO.BudgetHeading_BUDGETHEADINGMID]));
                                int headid = Convert.ToInt32(dt.Rows[0][0]);
                                hfHeadID.Value = Convert.ToString(dt.Rows[0][0]);
                                if (dt.Rows.Count > 0)
                                {
                                    objControls.BindDropDown_ListBox(dt, ddlBudgetHeading, "HeadingName", "BudgetHeadingMID");
                                }
                                //Find and Fetch SubHeading
                                dt1 = FetchSubHeading(Convert.ToInt32(dtSelect.Rows[0][BudgetSubHeadingMBO.BudgetHeading_BUDGETCATEGORYMID]), Convert.ToInt32(dtSelect.Rows[0][BudgetSubHeadingMBO.BudgetHeading_BUDGETHEADINGMID]), Convert.ToInt32(dtSelect.Rows[0][BudgetSubHeadingMBO.BudgetSubHeading_BUDGETSUBHEADINGMID]));
                                int subheadid = Convert.ToInt32(dt1.Rows[0][0]);
                                hfSubHeadID.Value = Convert.ToString(dt1.Rows[0][0]);
                                if (dt1.Rows.Count > 0)
                                {
                                    objControls1.BindDropDown_ListBox(dt1, ddlBudgetSubHeading, "SubHeadingName", "BudgetSubHeadingMID");
                                }
                                //ddlBudgetHeading.Text = dtResult.Rows[0][BudgetSubHeadingMBO.BudgetHeading_BUDGETHEADINGMID].ToString();                          
                                //ddlBudgetHeading.SelectedValue = dtSelect.Rows[0]["BudgetHeadingMID"].ToString();
                                //ddlBudgetSubHeading.Text = dtSelect.Rows[0]["BudgetSubHeadingMID"].ToString();
                                
                                #region Find and Fetch Unutilised Amount
                                BudgetEntryScreenTBL ObjBudgetEntryScreenTBL = new BudgetEntryScreenTBL();
                                UnutolisedAmountBL ObjUnutolisedAmountBL = new UnutolisedAmountBL();
                                ApplicationResult objResult = new ApplicationResult();
                                ApplicationResult objResult1 = new ApplicationResult();
                                ApplicationResult objResult2 = new ApplicationResult();
                                ApplicationResult objResult3 = new ApplicationResult();

                                string strYear = Session[ApplicationSession.FINANCIALYEAR].ToString();
                                TrustID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                                SchoolID = Convert.ToInt32(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                                CategoryID = Convert.ToInt32(ddlBudgetCategory.SelectedValue);
                                HeadID = Convert.ToInt32(ddlBudgetHeading.SelectedValue);
                                SubheadID = Convert.ToInt32(ddlBudgetSubHeading.SelectedValue);
                                SectionMID = Convert.ToInt32(ddlSection.SelectedValue);
                                string fayear = Convert.ToString(hfDate.Value);
                                objResult2 = ObjUnutolisedAmountBL.Unutilised_Year_Validation(TrustID, SchoolID, CategoryID, HeadID, SubheadID, strYear);
                                if (objResult2 != null)
                                {
                                    DataTable dtResult2 = objResult2.resultDT;
                                    if (dtResult2.Rows.Count > 0)
                                    {
                                        int BudgetScreenID = Convert.ToInt32(dtResult2.Rows[0][0]);
                                        string CurrentYear = Convert.ToString(dtResult2.Rows[0][7]);

                                        string FAYear = strYear.ToString();
                                        //string FAYear = hfDate.Value.ToString(); //Convert.ToString(txtAcademicYear.Text);
                                        if (CurrentYear == FAYear)
                                        {
                                            objResult = ObjUnutolisedAmountBL.BudgetEntry_FindId(TrustID, SchoolID, CategoryID, HeadID, SubheadID, SectionMID, BudgetScreenID);

                                            if (objResult != null)
                                            {
                                                DataTable dtResult = objResult.resultDT;
                                                if (dtResult.Rows.Count > 0)
                                                {
                                                    double UnutilizedBudget = Convert.ToDouble(dtResult.Rows[0]["BudgetSectionAmount"]);

                                                    hfBudgetScreenTID.Value = dtResult.Rows[0]["BudgetScreenTID"].ToString();
                                                    hfBudgetScreenID.Value = dtResult.Rows[0]["BudgetScreenID"].ToString();
                                                    //txtUnutilizedBudget.Text = Convert.ToString(UnutilizedBudget);

                                                    // Check "Journal"
                                                    objResult1 = ObjUnutolisedAmountBL.Unutilised_Amount_2(TrustID, SchoolID, SectionMID, CategoryID, HeadID, SubheadID);

                                                    if (objResult1 != null)
                                                    {
                                                        DataTable dtResult1 = objResult1.resultDT;

                                                        for (int i = 0; i < dtResult1.Rows.Count; i++)
                                                        {
                                                            string Tamount = Convert.ToString(objResult1.resultDT.Rows[i][9].ToString());
                                                            string TransactionType = Convert.ToString(objResult1.resultDT.Rows[i][10].ToString());
                                                            string OperationType = Convert.ToString(objResult1.resultDT.Rows[i][11].ToString());
                                                            string GroupName = Convert.ToString(objResult1.resultDT.Rows[i][22].ToString());
                                                            int LedgerID = Convert.ToInt32(objResult1.resultDT.Rows[i][7].ToString());
                                                            //string AccountName = Convert.ToString(objResult1.resultDT.Rows[i][15].ToString());
                                                            int CategoryID = Convert.ToInt32(objResult1.resultDT.Rows[i][4].ToString());

                                                            if (CategoryID == 1 || CategoryID == 2 || CategoryID == 5)
                                                            {
                                                                if ("Journal" == OperationType && "Expense" == GroupName)
                                                                {
                                                                    if ("Debit" == TransactionType)
                                                                    {
                                                                        amt1 = Convert.ToDouble(total == -1 ? UnutilizedBudget : total);
                                                                        amt2 = Convert.ToDouble(Tamount);

                                                                        total = amt1 - amt2;
                                                                    }
                                                                    else if ("Credit" == TransactionType)
                                                                    {
                                                                        amt1 = Convert.ToDouble(total == -1 ? UnutilizedBudget : total);
                                                                        amt2 = Convert.ToDouble(Tamount);
                                                                        total = amt1 + amt2;
                                                                    }
                                                                }
                                                            }
                                                            if (CategoryID == 3 || CategoryID == 4)
                                                            {
                                                                if ("Journal" == OperationType && "Income" == GroupName)
                                                                {
                                                                    if ("Debit" == TransactionType)
                                                                    {
                                                                        amt1 = Convert.ToDouble(total == -1 ? UnutilizedBudget : total);
                                                                        amt2 = Convert.ToDouble(Tamount);
                                                                        total = amt1 + amt2;
                                                                    }
                                                                    else if ("Credit" == TransactionType)
                                                                    {
                                                                        amt1 = Convert.ToDouble(total == -1 ? UnutilizedBudget : total);
                                                                        amt2 = Convert.ToDouble(Tamount);
                                                                        total = amt1 - amt2;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        txtUnutilizedBudget.Text = Convert.ToString(total == -1 ? UnutilizedBudget : total);
                                                    }

                                                    //Check "Receipt" and "Payment"
                                                    objResult3 = ObjUnutolisedAmountBL.Unutilised_Amount_3(TrustID, SchoolID, SectionMID, CategoryID, HeadID, SubheadID, Convert.ToString(hfDate.Value));

                                                    if (objResult3 != null)
                                                    {
                                                        DataTable dtResultRP = objResult3.resultDT;

                                                        for (int i = 0; i < dtResultRP.Rows.Count; i++)
                                                        {
                                                            string Tamount = Convert.ToString(objResult3.resultDT.Rows[i][9].ToString());
                                                            string OperationType = Convert.ToString(objResult3.resultDT.Rows[i][10].ToString());
                                                            string CurrentYearRP = Convert.ToString(objResult3.resultDT.Rows[i][11].ToString());
                                                            string TransactionType = "Debit";
                                                            string GroupName = Convert.ToString(objResult3.resultDT.Rows[i][22].ToString());
                                                            int LedgerID = Convert.ToInt32(objResult3.resultDT.Rows[i][7].ToString());
                                                            //string AccountName = Convert.ToString(objResult1.resultDT.Rows[i][15].ToString());
                                                            int CategoryID = Convert.ToInt32(objResult3.resultDT.Rows[i][4].ToString());

                                                            if (CategoryID == 1 || CategoryID == 2 || CategoryID == 5)
                                                            {
                                                                if ("Payment" == OperationType && "Expense" == GroupName)
                                                                {
                                                                    if ("Debit" == TransactionType)//Receipt
                                                                    {
                                                                        amt1 = Convert.ToDouble(total == -1 ? UnutilizedBudget : total);
                                                                        amt2 = Convert.ToDouble(Tamount);

                                                                        total = amt1 - amt2;
                                                                    }
                                                                    //else if ("Credit" == TransactionType) //Payment
                                                                    //{
                                                                    //    amt1 = Convert.ToDouble(total == -1 ? UnutilizedBudget : total);
                                                                    //    amt2 = Convert.ToDouble(Tamount);
                                                                    //    total = amt1 + amt2;
                                                                    //}
                                                                }
                                                            }
                                                            if (CategoryID == 3 || CategoryID == 4)
                                                            {
                                                                if ("Payment" == OperationType && "Income" == GroupName)
                                                                {
                                                                    //if ("Debit" == TransactionType)
                                                                    //{
                                                                    //    amt1 = Convert.ToDouble(total == -1 ? UnutilizedBudget : total);
                                                                    //    amt2 = Convert.ToDouble(Tamount);
                                                                    //    total = amt1 + amt2;
                                                                    //}
                                                                    if ("Credit" == TransactionType)
                                                                    {
                                                                        amt1 = Convert.ToDouble(total == -1 ? UnutilizedBudget : total);
                                                                        amt2 = Convert.ToDouble(Tamount);
                                                                        total = amt1 - amt2;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        txtUnutilizedBudget.Text = Convert.ToString(total == -1 ? UnutilizedBudget : total);
                                                        //string UnutilisedamtJ = Convert.ToString(total == -1 ? UnutilizedBudget : total);
                                                    }
                                                }
                                                else
                                                {
                                                    txtUnutilizedBudget.Text = "0";
                                                    //ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Unutilised Amount is Not Available!!!!!.');</script>");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            txtUnutilizedBudget.Text = "0";
                                        }
                                    }
                                    else
                                    {
                                        txtUnutilizedBudget.Text = "0";
                                    }
                                }
                                #endregion

                            }
                            //End DropDown Box
                            for (int i = 0; i < dtSelect.Rows.Count; i++)
                            {
                                ((DropDownList)GvEntry.Rows[i].Cells[0].FindControl("ddlAccountName")).SelectedValue = dtSelect.Rows[i]["LedgerID"].ToString();

                                if (dtSelect.Rows[i]["TransactionType"].ToString() == "Debit")
                                {
                                    ((TextBox)GvEntry.Rows[i].Cells[1].FindControl("txtDebitAmount")).Text = dtSelect.Rows[i]["Amount"].ToString();
                                }
                                else
                                {
                                    ((TextBox)GvEntry.Rows[i].Cells[2].FindControl("txtCreditAmount")).Text = dtSelect.Rows[i]["Amount"].ToString();
                                }
                                GvEntry.Rows[i].Cells[3].Text = dtSelect.Rows[i][0].ToString();

                                txtNarration.Text = dtSelect.Rows[i]["Description"].ToString();
                                txtVoucherCode.Text = ViewState["VoucherCode"].ToString();
                            }
                            PanelVisibility(2);
                            setControlScript();
                            enableDisableText();
                            getDebitCreditSum();
                            ViewState["Mode"] = "Edit";
                        }
                        else
                        {
                            PanelVisibility(2);
                            setControlScript();
                            enableDisableText();
                            getDebitCreditSum();
                            ViewState["Mode"] = "Edit";
                        }
                    }
                    else
                    {
                        PanelVisibility(2);
                        setControlScript();
                        enableDisableText();
                        getDebitCreditSum();
                        ViewState["Mode"] = "Edit";
                    }
                }
                else if (e.CommandName.ToString() == "Delete1")
                {
                    ApplicationResult objResultDelete = new ApplicationResult();
                    objResultDelete = objJournalVoucherMBL.JournalVoucherM_Delete(e.CommandArgument.ToString(), "Journal");
                    if (objResultDelete.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        BindGrid();
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record Deleted Successfully.');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        protected void gvJournalEntry_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvJournalEntry_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvJournalEntry.Rows.Count > 0)
                {
                    gvJournalEntry.UseAccessibleHeader = true;
                    gvJournalEntry.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        protected void gvJournalEntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvJournalEntry.SelectedIndex = -1;
        }
        #endregion

        #region Button Viewlist
        protected void btnViewList_Click(object sender, EventArgs e)
        {
            clear();
            PanelVisibility(1);
            ddlBudgetCategory.ClearSelection();
            ddlBudgetHeading.ClearSelection();
            ddlBudgetSubHeading.ClearSelection();
            txtUnutilizedBudget.Text = "0";
        }
        #endregion

        #region DeleteDataFromTransactionTable
        public void deleteLedgerTransaction(string strVoucherCode)
        {
            try
            {
                JournalVoucherTBL objJournalVoucherTBL = new JournalVoucherTBL();
                ApplicationResult objResultDelete = new ApplicationResult();

                objResultDelete = objJournalVoucherTBL.JournalVoucherT_Delete(strVoucherCode);
                if (objResultDelete.status == ApplicationResult.CommonStatusType.SUCCESS)
                {

                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Get Max Date

        public void GetMaxDate()
        {
            JournalVoucherMBL objJournalVoucherMBL = new JournalVoucherMBL();
            ApplicationResult objresult = new ApplicationResult();
            objresult = objJournalVoucherMBL.GetMaxDate(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), "Journal", Convert.ToInt32(Session[ApplicationSession.FINANCIALYEAR]));
            if (objresult != null)
            {
                DataTable dt = objresult.resultDT;
                if (dt.Rows[0][0].ToString() != "")
                    txtDate.Text = dt.Rows[0][0].ToString();
                else
                    txtDate.Text = Session[ApplicationSession.ACCOUNTTODATE].ToString();
            }
        }

        #endregion      

        #region Fetch Heading data For DropDown 
        private DataTable FetchHeading(int intBudgetCategoryMID, int intBudgetHeadingMID)
        {
            //DataTable dtDivision = new DataTable();

            BudgetSubHeadingMBL objBudgetSubHeadingMBL = new BudgetSubHeadingMBL();
            BudgetSubHeadingMBO objBudgetSubHeadingMBO = new BudgetSubHeadingMBO();
            ApplicationResult objResults = new ApplicationResult();

            objResults = objBudgetSubHeadingMBL.BudgetHeading_SelectDropDownById(intBudgetCategoryMID, intBudgetHeadingMID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {

            }
            return objResults.resultDT;
        }
        #endregion

        #region Fetch SubHeading data For DropDown 
        private DataTable FetchSubHeading(int intBudgetCategoryMID, int intBudgetHeadingMID, int intBudgetSubHeadingMID)
        {
            //DataTable dtDivision = new DataTable();

            BudgetSubHeadingMBL objBudgetSubHeadingMBL = new BudgetSubHeadingMBL();
            BudgetSubHeadingMBO objBudgetSubHeadingMBO = new BudgetSubHeadingMBO();
            ApplicationResult objResults = new ApplicationResult();

            objResults = objBudgetSubHeadingMBL.BudgetSubHeading_SelectDropDownById(intBudgetCategoryMID, intBudgetHeadingMID, intBudgetSubHeadingMID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {

            }
            return objResults.resultDT;
        }
        #endregion

        #region SubHeading wise Unutilised Amount
        protected void ddlBudgetSubHeading_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlBudgetSubHeading.SelectedIndex != 0)
                {
                    //clear();                   
                    BudgetEntryScreenTBL ObjBudgetEntryScreenTBL = new BudgetEntryScreenTBL();
                    UnutolisedAmountBL ObjUnutolisedAmountBL = new UnutolisedAmountBL();
                    ApplicationResult objResult = new ApplicationResult();
                    ApplicationResult objResult1 = new ApplicationResult();
                    ApplicationResult objResult2 = new ApplicationResult();
                    ApplicationResult objResult3 = new ApplicationResult();

                    string strYear = Session[ApplicationSession.FINANCIALYEAR].ToString();
                    TrustID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                    SchoolID = Convert.ToInt32(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                    SectionMID = Convert.ToInt32(ddlSection.SelectedValue);
                    CategoryID = Convert.ToInt32(ddlBudgetCategory.SelectedValue);
                    HeadID = Convert.ToInt32(ddlBudgetHeading.SelectedValue);
                    SubheadID = Convert.ToInt32(ddlBudgetSubHeading.SelectedValue);
                    string fayear = Convert.ToString(hfDate.Value);

                    //objResult2 = ObjUnutolisedAmountBL.Unutilised_Year_Validation(TrustID, SchoolID, CategoryID, HeadID, SubheadID, Convert.ToString(hfDate.Value));
                    objResult2 = ObjUnutolisedAmountBL.Unutilised_Year_Validation(TrustID, SchoolID, CategoryID, HeadID, SubheadID, strYear);
                    if (objResult2 != null)
                    {
                        DataTable dtResult2 = objResult2.resultDT;
                        if (dtResult2.Rows.Count > 0)
                        {
                            int BudgetScreenID = Convert.ToInt32(dtResult2.Rows[0][0]);
                            string CurrentYear = Convert.ToString(dtResult2.Rows[0][7]);

                            string FAYear = hfDate.Value.ToString(); //Convert.ToString(txtAcademicYear.Text);
                            if (CurrentYear == FAYear)
                            {
                                objResult = ObjUnutolisedAmountBL.BudgetEntry_FindId(TrustID, SchoolID, CategoryID, HeadID, SubheadID, SectionMID, BudgetScreenID);

                                if (objResult != null)
                                {
                                    DataTable dtResult = objResult.resultDT;
                                    if (dtResult.Rows.Count > 0)
                                    {
                                        double UnutilizedBudget = Convert.ToDouble(dtResult.Rows[0]["BudgetSectionAmount"]);

                                        hfBudgetScreenTID.Value = dtResult.Rows[0]["BudgetScreenTID"].ToString();
                                        hfBudgetScreenID.Value = dtResult.Rows[0]["BudgetScreenID"].ToString();
                                        //txtUnutilizedBudget.Text = Convert.ToString(UnutilizedBudget);

                                        // Check "Journal"
                                        objResult1 = ObjUnutolisedAmountBL.Unutilised_Amount_2(TrustID, SchoolID, SectionMID, CategoryID, HeadID, SubheadID);

                                        if (objResult1 != null)
                                        {
                                            DataTable dtResult1 = objResult1.resultDT;

                                            for (int i = 0; i < dtResult1.Rows.Count; i++)
                                            {
                                                string Tamount = Convert.ToString(objResult1.resultDT.Rows[i][9].ToString());
                                                string TransactionType = Convert.ToString(objResult1.resultDT.Rows[i][10].ToString());
                                                string OperationType = Convert.ToString(objResult1.resultDT.Rows[i][11].ToString());
                                                string GroupName = Convert.ToString(objResult1.resultDT.Rows[i][22].ToString());
                                                int LedgerID = Convert.ToInt32(objResult1.resultDT.Rows[i][7].ToString());
                                                //string AccountName = Convert.ToString(objResult1.resultDT.Rows[i][15].ToString());
                                                int CategoryID = Convert.ToInt32(objResult1.resultDT.Rows[i][4].ToString());

                                                if (CategoryID == 1 || CategoryID == 2 || CategoryID == 5)
                                                {
                                                    if ("Journal" == OperationType && "Expense" == GroupName)
                                                    {
                                                        if ("Debit" == TransactionType)
                                                        {
                                                            amt1 = Convert.ToDouble(total == -1 ? UnutilizedBudget : total);
                                                            amt2 = Convert.ToDouble(Tamount);

                                                            total = amt1 - amt2;
                                                        }
                                                        else if ("Credit" == TransactionType)
                                                        {
                                                            amt1 = Convert.ToDouble(total == -1 ? UnutilizedBudget : total);
                                                            amt2 = Convert.ToDouble(Tamount);
                                                            total = amt1 + amt2;
                                                        }
                                                    }
                                                }
                                                if (CategoryID == 3 || CategoryID == 4)
                                                {
                                                    if ("Journal" == OperationType && "Income" == GroupName)
                                                    {
                                                        if ("Debit" == TransactionType)
                                                        {
                                                            amt1 = Convert.ToDouble(total == -1 ? UnutilizedBudget : total);
                                                            amt2 = Convert.ToDouble(Tamount);
                                                            total = amt1 + amt2;
                                                        }
                                                        else if ("Credit" == TransactionType)
                                                        {
                                                            amt1 = Convert.ToDouble(total == -1 ? UnutilizedBudget : total);
                                                            amt2 = Convert.ToDouble(Tamount);
                                                            total = amt1 - amt2;
                                                        }
                                                    }
                                                }
                                            }
                                            txtUnutilizedBudget.Text = Convert.ToString(total == -1 ? UnutilizedBudget : total);
                                        }

                                        //Check "Receipt" and "Payment"
                                        objResult3 = ObjUnutolisedAmountBL.Unutilised_Amount_3(TrustID, SchoolID, SectionMID, CategoryID, HeadID, SubheadID, Convert.ToString(hfDate.Value));

                                        //if (objResult3 != null)
                                        //{
                                        //    DataTable dtResultRP = objResult3.resultDT;

                                        //    for (int i = 0; i < dtResultRP.Rows.Count; i++)
                                        //    {
                                        //        string Tamount = Convert.ToString(objResult3.resultDT.Rows[i][9].ToString());
                                        //        string OperationType = Convert.ToString(objResult3.resultDT.Rows[i][10].ToString());
                                        //        string CurrentYearRP = Convert.ToString(objResult3.resultDT.Rows[i][11].ToString());
                                        //        string TransactionType = "Debit";
                                        //        string GroupName = Convert.ToString(objResult3.resultDT.Rows[i][22].ToString());
                                        //        int LedgerID = Convert.ToInt32(objResult3.resultDT.Rows[i][7].ToString());
                                        //        //string AccountName = Convert.ToString(objResult1.resultDT.Rows[i][15].ToString());
                                        //        int CategoryID = Convert.ToInt32(objResult3.resultDT.Rows[i][4].ToString());

                                        //        if (CategoryID == 1 || CategoryID == 2 || CategoryID == 5)
                                        //        {
                                        //            if ("Payment" == OperationType && "Expense" == GroupName)
                                        //            {
                                        //                if ("Debit" == TransactionType)//Receipt
                                        //                {
                                        //                    amt1 = Convert.ToDouble(total == -1 ? UnutilizedBudget : total);
                                        //                    amt2 = Convert.ToDouble(Tamount);

                                        //                    total = amt1 - amt2;
                                        //                }
                                        //                //else if ("Credit" == TransactionType) //Payment
                                        //                //{
                                        //                //    amt1 = Convert.ToDouble(total == -1 ? UnutilizedBudget : total);
                                        //                //    amt2 = Convert.ToDouble(Tamount);
                                        //                //    total = amt1 + amt2;
                                        //                //}
                                        //            }
                                        //        }
                                        //        if (CategoryID == 3 || CategoryID == 4)
                                        //        {
                                        //            if ("Payment" == OperationType && "Income" == GroupName)
                                        //            {
                                        //                //if ("Debit" == TransactionType)
                                        //                //{
                                        //                //    amt1 = Convert.ToDouble(total == -1 ? UnutilizedBudget : total);
                                        //                //    amt2 = Convert.ToDouble(Tamount);
                                        //                //    total = amt1 + amt2;
                                        //                //}
                                        //                if ("Credit" == TransactionType)
                                        //                {
                                        //                    amt1 = Convert.ToDouble(total == -1 ? UnutilizedBudget : total);
                                        //                    amt2 = Convert.ToDouble(Tamount);
                                        //                    total = amt1 - amt2;
                                        //                }
                                        //            }
                                        //        }
                                        //    }
                                        //    txtUnutilizedBudget.Text = Convert.ToString(total == -1 ? UnutilizedBudget : total);
                                        //    //string UnutilisedamtJ = Convert.ToString(total == -1 ? UnutilizedBudget : total);
                                        //}
                                        if (objResult3 != null)
                                        {
                                            DataTable dtResultRP = objResult3.resultDT;

                                            for (int i = 0; i < dtResultRP.Rows.Count; i++)
                                            {
                                                string Tamount = Convert.ToString(objResult3.resultDT.Rows[i][9].ToString());
                                                string OperationType = Convert.ToString(objResult3.resultDT.Rows[i][10].ToString());
                                                string CurrentYearRP = Convert.ToString(objResult3.resultDT.Rows[i][11].ToString());
                                                string TransactionType = "Debit";
                                                string TransactionType1 = "Credit";
                                                string GroupName = Convert.ToString(objResult3.resultDT.Rows[i][22].ToString());
                                                int LedgerID = Convert.ToInt32(objResult3.resultDT.Rows[i][7].ToString());
                                                //string AccountName = Convert.ToString(objResult1.resultDT.Rows[i][15].ToString());
                                                int CategoryID = Convert.ToInt32(objResult3.resultDT.Rows[i][4].ToString());

                                                if (CategoryID == 1 || CategoryID == 2 || CategoryID == 5)
                                                {
                                                    if ("Payment" == OperationType && "Expense" == GroupName)
                                                    {
                                                        if ("Debit" == TransactionType)//Receipt
                                                        {
                                                            amt1 = Convert.ToDouble(total == -1 ? UnutilizedBudget : total);
                                                            amt2 = Convert.ToDouble(Tamount);

                                                            total = amt1 - amt2;
                                                        }
                                                    }
                                                    if ("Receipt" == OperationType && "Expense" == GroupName)
                                                    {
                                                        if ("Credit" == TransactionType1) //Payment
                                                        {
                                                            amt1 = Convert.ToDouble(total == -1 ? UnutilizedBudget : total);
                                                            amt2 = Convert.ToDouble(Tamount);
                                                            total = amt1 + amt2;
                                                        }
                                                    }
                                                }
                                                if (CategoryID == 3 || CategoryID == 4)
                                                {
                                                    if ("Payment" == OperationType && "Income" == GroupName)
                                                    {
                                                        if ("Debit" == TransactionType)
                                                        {
                                                            amt1 = Convert.ToDouble(total == -1 ? UnutilizedBudget : total);
                                                            amt2 = Convert.ToDouble(Tamount);
                                                            total = amt1 + amt2;
                                                        }
                                                    }
                                                    if ("Receipt" == OperationType && "Income" == GroupName)
                                                    {
                                                        if ("Credit" == TransactionType1)
                                                        {
                                                            amt1 = Convert.ToDouble(total == -1 ? UnutilizedBudget : total);
                                                            amt2 = Convert.ToDouble(Tamount);
                                                            total = amt1 - amt2;
                                                        }
                                                    }
                                                }
                                            }
                                            txtUnutilizedBudget.Text = Convert.ToString(total == -1 ? UnutilizedBudget : total);
                                            //string UnutilisedamtJ = Convert.ToString(total == -1 ? UnutilizedBudget : total);
                                        }
                                    }
                                    else
                                    {
                                        txtUnutilizedBudget.Text = "0";
                                        //ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Unutilised Amount is Not Available!!!!!.');</script>");
                                    }
                                }
                            }
                            else
                            {
                                txtUnutilizedBudget.Text = "0";
                            }
                        }
                        else
                        {
                            txtUnutilizedBudget.Text = "0";
                        }
                    }
                    else
                    {
                        txtUnutilizedBudget.Text = "0";
                    }
                }
                else
                {
                    txtUnutilizedBudget.Text = "0";
                }
            }
            catch (Exception ex)
            {
                txtUnutilizedBudget.Text = "0";
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Section Changed
        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {           
            ddlBudgetCategory.ClearSelection();
            ddlBudgetHeading.ClearSelection();
            ddlBudgetSubHeading.ClearSelection();
            txtUnutilizedBudget.Text = "0";

            //Response.Headers.Add("Refresh", "0");
            //Response.Headers.Remove("Refresh");
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
                int intMonth = 4;
                //string strFromDate = Session[ApplicationSession.ACCOUNTFROMDATE].ToString();
                string strFromDate = txtDate.Text.ToString();

                //string strToDate = Session[ApplicationSession.ACCOUNTTODATE].ToString();
                //string strYear = Session[ApplicationSession.FINANCIALYEAR].ToString();
                DateTime FromDate = Convert.ToDateTime(strFromDate);
                //DateTime ToDate = Convert.ToDateTime(strToDate);

                //objResults = objSchoolBl.School_Select(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));

                //if (objResults != null)
                //{
                //    if (objResults.resultDT.Rows.Count > 0)
                //    {

                //        intMonth = Convert.ToInt32(objResults.resultDT.Rows[0][SchoolBO.SCHOOL_ACADEMICMONTH].ToString());
                //    }

                //}
                #endregion

                Controls objControls = new Controls();
                //int month = System.DateTime.Now.Month;
                int month = FromDate.Month;
                //int Year = System.DateTime.Now.Year;
                int Year = FromDate.Year;
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
                        //dr["AcademicYear"] = "20" + Convert.ToString(f.ToString() + "-" + l.ToString());
                        dr["AcademicYear"] = Convert.ToString(f.ToString() + l.ToString());
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        if ((f - 1).ToString().Length < 2)
                        {
                            if (f.ToString().Length == 2)
                            {
                                //dr["AcademicYear"] = "20" + Convert.ToString("0" + (f - 1).ToString() + "-" + (f).ToString());
                                dr["AcademicYear"] = Convert.ToString("0" + (f - 1).ToString() + (f).ToString());
                            }
                            else
                            {
                                //dr["AcademicYear"] = "20" + Convert.ToString("0" + (f - 1).ToString() + "-" + "0" + (f).ToString());
                                dr["AcademicYear"] = Convert.ToString("0" + (f - 1).ToString() + "0" + (f).ToString());
                            }
                            dt.Rows.Add(dr);
                        }
                        else
                        {
                            dr["AcademicYear"] = Convert.ToString((f - 1).ToString() + (f).ToString());
                            dt.Rows.Add(dr);
                        }
                        f = f - 1;
                        l = f;
                    }
                }
                //txtAcademicYear.Text = dt.Rows[0][0].ToString();
                FAYEAR = dt.Rows[0][0].ToString();
                hfDate.Value = FAYEAR.ToString();
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('First Login form Accounting Menu Side');</script>");
            }
        }
        #endregion

        #region Find Date wise FAYEAR
        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            BindAcademicYear();
            ddlSection.ClearSelection();
            ddlBudgetCategory.ClearSelection();
            ddlBudgetHeading.ClearSelection();
            ddlBudgetSubHeading.ClearSelection();
            txtUnutilizedBudget.Text = "0";
        }
        #endregion
    }
}