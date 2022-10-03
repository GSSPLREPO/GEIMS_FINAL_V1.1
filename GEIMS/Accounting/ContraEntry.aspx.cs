using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.Common;
using GEIMS.DataAccess;

namespace GEIMS.Accounting
{
    public partial class ContraEntry : PageBase
    {
        #region declaration
        private static ILog logger = LogManager.GetLogger(typeof(ContraEntry));
        Controls objControl = new Controls();
        DataTable dtGridData = new DataTable();
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

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {          
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
                    txtDate.Attributes.Add("readonly", "readonly");

                    DataTable dt = new DataTable();
                    dt.Rows.Clear();
                    dt.Columns.Add("NO");
                    for (var i = 1; i <= 10; i++)
                    {
                        dt.Rows.Add(this.ToString());
                    }
                    GvContraEntry.DataSource = dt;
                    GvContraEntry.DataBind();

                    BindGrid();
                    BindSection();
                    BindAccountGroup();
                    PanelVisibility(1);
                }

                if (ViewState["grid"] != null)
                {
                    dtGridData = (DataTable)ViewState["grid"];
                    dtGridData.Rows.Clear();

                    for (int i = 0; i < GvContraEntry.Rows.Count; i++)
                    {
                        DataRow dr = dtGridData.NewRow();

                        dr[0] = ((DropDownList)(GvContraEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).SelectedItem.Value;
                        dr[1] = ((TextBox)(GvContraEntry.Rows[i].Cells[2].FindControl("txtDebitAmount"))).Text;
                        dr[2] = ((TextBox)(GvContraEntry.Rows[i].Cells[3].FindControl("txtCreditAmount"))).Text;

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

        #region Bind Grid Voucher
        public void BindGrid()
        {
            try
            {
                JournalVoucherMBL objJournalVoucherMBL = new JournalVoucherMBL();
                JournalVoucherMBO objJournalVoucherMBO = new JournalVoucherMBO();
                ApplicationResult objResultSelectAll = new ApplicationResult();

                objResultSelectAll = objJournalVoucherMBL.JournalVoucherM_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(Session[ApplicationSession.FINANCIALYEAR]), "Contra", Session[ApplicationSession.ACCOUNTFROMDATE].ToString(), Session[ApplicationSession.ACCOUNTTODATE].ToString());
                if (objResultSelectAll != null)
                {
                    DataTable dtSelectAll = new DataTable();
                    dtSelectAll = objResultSelectAll.resultDT;
                    gvJournalContraEntry.DataSource = dtSelectAll;
                    gvJournalContraEntry.DataBind();
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
            objResultSelectAll = objGeneralLedgerBL.GeneralLedger_SelectAll_ContraEntry(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
            if (objResultSelectAll != null)
            {
                DataTable dtSelectAll = new DataTable();
                dtSelectAll = objResultSelectAll.resultDT;

                for (int i = 0; i < GvContraEntry.Rows.Count; i++)
                {
                    ddlAccountName = (DropDownList)GvContraEntry.Rows[i].Cells[0].FindControl("ddlAccountName");
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
                    GvContraEntry.DataSource = (DataTable)ViewState["data"];
                    GvContraEntry.DataBind();
                    int cnt = GvContraEntry.Rows.Count;
                    cnt++;

                    dt = (DataTable)ViewState["data"];
                    dt.Rows.Add(cnt.ToString());

                    GvContraEntry.DataSource = dt;
                    GvContraEntry.DataBind();
                }
                else
                {
                    int cnt = GvContraEntry.Rows.Count;
                    cnt++;
                    dt.Columns.Add("NO");
                    for (int i = 0; i < cnt; i++)
                    {
                        dt.Rows.Add(i.ToString());
                    }
                    GvContraEntry.DataSource = dt;
                    GvContraEntry.DataBind();
                }

                BindAccountGroup();

                ViewState["data"] = dt;

                if (ViewState["grid"] != null)
                {
                    dtGridData = (DataTable)ViewState["grid"];

                    for (int i = 0; i < dtGridData.Rows.Count; i++)
                    {
                        ((DropDownList)(GvContraEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).SelectedValue = dtGridData.Rows[i][0].ToString();
                        ((TextBox)(GvContraEntry.Rows[i].Cells[2].FindControl("txtDebitAmount"))).Text = dtGridData.Rows[i][1].ToString();
                        ((TextBox)(GvContraEntry.Rows[i].Cells[3].FindControl("txtCreditAmount"))).Text = dtGridData.Rows[i][2].ToString();
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
            for (int i = 0; i < GvContraEntry.Rows.Count; i++)
            {
                ((DropDownList)(GvContraEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).Attributes.Add("onchange", "javascript:makeTextBoxFocus(" + i + ");");
                ((TextBox)(GvContraEntry.Rows[i].Cells[2].FindControl("txtDebitAmount"))).Attributes.Add("onkeydown", "javascript:enableDisableCreditText(" + i + ");");
                ((TextBox)(GvContraEntry.Rows[i].Cells[3].FindControl("txtCreditAmount"))).Attributes.Add("onkeydown", "javascript:enableDisableDebitText(" + i + ");");

                ((DropDownList)(GvContraEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).Attributes.Add("onfocus", "javascript:clearFocus()");
                ((TextBox)(GvContraEntry.Rows[i].Cells[2].FindControl("txtDebitAmount"))).Attributes.Add("onfocus", "javascript:focusDebitCreditText(" + (4 * i) + ");");
                ((TextBox)(GvContraEntry.Rows[i].Cells[3].FindControl("txtCreditAmount"))).Attributes.Add("onfocus", "javascript:focusDebitCreditText(" + ((4 * i) + 2) + ");");

                ((TextBox)(GvContraEntry.Rows[i].Cells[2].FindControl("txtDebitAmount"))).Attributes.Add("onchange", "javascript:makeDebitSum();");
                ((TextBox)(GvContraEntry.Rows[i].Cells[3].FindControl("txtCreditAmount"))).Attributes.Add("onchange", "javascript:makeCreditSum();");
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
            for (int i = 0; i < GvContraEntry.Rows.Count; i++)
            {
                TextBox txtDebit = ((TextBox)(GvContraEntry.Rows[i].Cells[2].FindControl("txtDebitAmount")));
                TextBox txtCredit = ((TextBox)(GvContraEntry.Rows[i].Cells[3].FindControl("txtCreditAmount")));

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
            for (int i = 0; i < GvContraEntry.Rows.Count; i++)
            {
                txtDebitAmount = (TextBox)(GvContraEntry.Rows[i].Cells[2].FindControl("txtDebitAmount"));
                if (txtDebitAmount.Text.Length > 0)
                {
                    dblDebitSum += Convert.ToDouble(txtDebitAmount.Text.ToString());
                }
                txtCreditAmount = (TextBox)(GvContraEntry.Rows[i].Cells[3].FindControl("txtCreditAmount"));
                if (txtCreditAmount.Text.Length > 0)
                {
                    dblCreditSum += Convert.ToDouble(txtCreditAmount.Text.ToString());
                }
            }

            ((System.Web.UI.HtmlControls.HtmlInputText)(GvContraEntry.FooterRow.Cells[2].FindControl("txtDebitSum"))).Value = dblDebitSum.ToString();
            ((System.Web.UI.HtmlControls.HtmlInputText)(GvContraEntry.FooterRow.Cells[3].FindControl("txtCreditSum"))).Value = dblCreditSum.ToString();
        }
        #endregion

        #region Save button
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
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
                    strDebitSum = ((System.Web.UI.HtmlControls.HtmlInputText)(GvContraEntry.FooterRow.Cells[2].FindControl("txtDebitSum"))).Value;
                    strCreditSum = ((System.Web.UI.HtmlControls.HtmlInputText)(GvContraEntry.FooterRow.Cells[3].FindControl("txtCreditSum"))).Value;

                    //  CHECK WHETHER ANY AMOUNT IS WITHOUT ACCOUNT SELECTION
                    int intvalidate = 0;
                    for (int i = 0; i < GvContraEntry.Rows.Count; i++)
                    {
                        string ddlVal = ((DropDownList)(GvContraEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).SelectedItem.Value;
                        string strDebitAmt = ((TextBox)(GvContraEntry.Rows[i].Cells[2].FindControl("txtDebitAmount"))).Text;
                        string strCreditAmt = ((TextBox)(GvContraEntry.Rows[i].Cells[3].FindControl("txtCreditAmount"))).Text;

                        if (strDebitAmt != "" || strCreditAmt != "")
                        {
                            if (ddlVal == "-1")
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Select Account or enter amount before save Contra entry.');</script>");
                                return;
                            }
                        }
                        else
                        {
                            intvalidate = intvalidate + 1;
                        }
                        if (GvContraEntry.Rows.Count == intvalidate)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Select Account or enter amount before save Contra entry.');</script>");
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
                            ApplicationResult objResultInsert = new ApplicationResult();
                            objJournalVoucherMBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                            objJournalVoucherMBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                            objJournalVoucherMBO.VoucherDate = txtDate.Text;

                            objJournalVoucherMBO.SectionMID = Convert.ToInt32(ddlSection.SelectedValue.ToString());
                            objJournalVoucherMBO.BudgetCategoryMID = 0;
                            objJournalVoucherMBO.BudgetHeadingMID = 0;
                            objJournalVoucherMBO.BudgetSubHeadingMID = 0;

                            if (txtChequeNo.Text != "")
                                objJournalVoucherMBO.ChequeNumber = Convert.ToInt32(txtChequeNo.Text);
                            objJournalVoucherMBO.OperationType = "Contra";
                            objJournalVoucherMBO.Description = txtNarration.Text;
                            objJournalVoucherMBO.CreatedBy = Convert.ToInt32(Session[ApplicationSession.USERID]);
                            objJournalVoucherMBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString("dd/MM/yyyy");
                            objJournalVoucherMBO.IsDeleted = 0;
                            objJournalVoucherMBO.LastModifideDate = DateTime.UtcNow.AddHours(5.5).ToString("dd/MM/yyyy");
                            objJournalVoucherMBO.LastModifideUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                            objJournalVoucherMBO.Year = Convert.ToInt32(strYear);

                            for (int i = 0; i < GvContraEntry.Rows.Count; i++)
                            {
                                string strAccountID = ((DropDownList)(GvContraEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).SelectedItem.Value;
                                string strDebit = ((TextBox)(GvContraEntry.Rows[i].Cells[2].FindControl("txtDebitAmount"))).Text;
                                string strCredit = ((TextBox)(GvContraEntry.Rows[i].Cells[3].FindControl("txtCreditAmount"))).Text;

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
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Contra Saved Successfully. Voucher No is " + ViewState["VoucherCode"].ToString() + "');</script>");
                        }
                        else if (ViewState["Mode"].ToString() == "Edit")    //  UPDATE THE EXISTING JOURNAL
                        {
                            ApplicationResult objResulUpdate = new ApplicationResult();

                            objJournalVoucherMBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                            objJournalVoucherMBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                            objJournalVoucherMBO.VoucherDate = txtDate.Text;

                            objJournalVoucherMBO.SectionMID = Convert.ToInt32(ddlSection.SelectedValue.ToString());
                            objJournalVoucherMBO.BudgetCategoryMID = 0;
                            objJournalVoucherMBO.BudgetHeadingMID = 0;
                            objJournalVoucherMBO.BudgetSubHeadingMID = 0;

                            if (txtChequeNo.Text != "")
                                objJournalVoucherMBO.ChequeNumber = Convert.ToInt32(txtChequeNo.Text);
                            objJournalVoucherMBO.OperationType = "Contra";
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
                            for (int i = 0; i < GvContraEntry.Rows.Count; i++)
                            {
                                string strAccId = ((DropDownList)(GvContraEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).SelectedItem.Value;
                                if (strAccId != "-1")
                                    intActRows++;
                                else
                                    break;
                            }

                            if (intActRows == Convert.ToInt32(ViewState["VoucherRow"]))   //  CHECKS WHETHER THE JOURNAL RECORD IS LARGER OR SMALLER THEN EXIXTING RECORD
                            {
                                for (int i = 0; i < GvContraEntry.Rows.Count; i++)
                                {
                                    GvContraEntry.Columns[4].Visible = true;

                                    if (GvContraEntry.Rows[i].Cells[4].Text.ToString() != "")
                                    {
                                        string strAccountID = ((DropDownList)(GvContraEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).SelectedItem.Value;
                                        string strDebit = ((TextBox)(GvContraEntry.Rows[i].Cells[2].FindControl("txtDebitAmount"))).Text;
                                        string strCredit = ((TextBox)(GvContraEntry.Rows[i].Cells[3].FindControl("txtCreditAmount"))).Text;

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
                                        objJournalVoucherMBO.JournalID = Convert.ToInt32(GvContraEntry.Rows[i].Cells[4].Text.ToString());

                                        objResulUpdate = objJournalVoucherMBL.JournalVoucherM_Update(objJournalVoucherMBO, Session[ApplicationSession.ACCOUNTFROMDATE].ToString());
                                    }
                                    GvContraEntry.Columns[4].Visible = false;
                                }
                            }
                            else
                            {
                                ApplicationResult objResultInsert = new ApplicationResult();
                                ApplicationResult objResultDelete = new ApplicationResult();
                                objResultDelete = objJournalVoucherMBL.JournalVoucherM_Delete_Transaction(txtVoucherCode.Text,"Journal");
                                if (objResultDelete.status == ApplicationResult.CommonStatusType.SUCCESS)
                                { }

                                for (int i = 0; i < GvContraEntry.Rows.Count; i++)
                                {
                                    string strAccountID = ((DropDownList)(GvContraEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).SelectedItem.Value;
                                    string strDebit = ((TextBox)(GvContraEntry.Rows[i].Cells[2].FindControl("txtDebitAmount"))).Text;
                                    string strCredit = ((TextBox)(GvContraEntry.Rows[i].Cells[3].FindControl("txtCreditAmount"))).Text;

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
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Contra Updated Successfully...');</script>");
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
                GvContraEntry.Visible = true;
            Exit: ;
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

            GvContraEntry.DataSource = null;
            getNewRows();
            GvContraEntry.DataBind();

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
            GvContraEntry.DataSource = dt;
            GvContraEntry.DataBind();
        }
        #endregion

        #region Add New Button
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            PanelVisibility(2);
            clear();
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
                    GvContraEntry.SelectedIndex = -1;
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
                            GvContraEntry.DataSource = dt;
                            GvContraEntry.DataBind();

                            BindAccountGroup();
                            ddlSection.Text = dtSelect.Rows[0]["SectionMID"].ToString();
                           
                            for (int i = 0; i < dtSelect.Rows.Count; i++)
                            {
                                ((DropDownList)GvContraEntry.Rows[i].Cells[0].FindControl("ddlAccountName")).SelectedValue = dtSelect.Rows[i]["LedgerID"].ToString();

                                if (dtSelect.Rows[i]["TransactionType"].ToString() == "Debit")
                                {
                                    ((TextBox)GvContraEntry.Rows[i].Cells[2].FindControl("txtDebitAmount")).Text = dtSelect.Rows[i]["Amount"].ToString();
                                }
                                else
                                {
                                    ((TextBox)GvContraEntry.Rows[i].Cells[3].FindControl("txtCreditAmount")).Text = dtSelect.Rows[i]["Amount"].ToString();
                                }
                                GvContraEntry.Rows[i].Cells[4].Text = dtSelect.Rows[i][0].ToString();

                                txtNarration.Text = dtSelect.Rows[i]["Description"].ToString();
                                if (Convert.ToInt32(dtSelect.Rows[i]["ChequeNumber"]) != 0)
                                    txtChequeNo.Text = dtSelect.Rows[i]["ChequeNumber"].ToString();
                                txtVoucherCode.Text = ViewState["VoucherCode"].ToString();
                                txtDate.Text = dtSelect.Rows[i]["VoucherDate"].ToString();
                            }
                            PanelVisibility(2);
                            setControlScript();
                            enableDisableText();
                            getDebitCreditSum();
                            ViewState["Mode"] = "Edit";
                        }
                    }
                }
                else if (e.CommandName.ToString() == "Delete1")
                {
                    ApplicationResult objResultDelete = new ApplicationResult();
                    objResultDelete = objJournalVoucherMBL.JournalVoucherM_Delete(e.CommandArgument.ToString(), "Contra");
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
                if (gvJournalContraEntry.Rows.Count > 0)
                {
                    gvJournalContraEntry.UseAccessibleHeader = true;
                    gvJournalContraEntry.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            gvJournalContraEntry.SelectedIndex = -1;
        }
        #endregion

        #region Button Viewlist
        protected void btnViewList_Click(object sender, EventArgs e)
        {
            clear();
            PanelVisibility(1);
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
            objresult = objJournalVoucherMBL.GetMaxDate(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), "Contra", Convert.ToInt32(Session[ApplicationSession.FINANCIALYEAR]));
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

        #region webservise for fetch balance
        [System.Web.Services.WebMethod]
        public static string LoadBalance(int intTrustMID, int intSchoolID, int intLedgerID, string strToDate)
        {
            try
            {
                #region Bind Balance
                ApplicationResult objResult = new ApplicationResult();
                GeneralLedgerBL objGLedgerBL = new GeneralLedgerBL();

                objResult = objGLedgerBL.Select_OpeningBalanceForAccounting(intTrustMID, intSchoolID, intLedgerID, strToDate);
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        for (int i = 0; i < objResult.resultDT.Rows.Count; i++)
                        {

                        }
                    }

                }
                string res = "";
                res = DataSetToJSON(objResult.resultDT);
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
    }
}