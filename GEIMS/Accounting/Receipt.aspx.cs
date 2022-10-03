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
    public partial class Receipt : PageBase
    {
        #region declaration
        private static ILog logger = LogManager.GetLogger(typeof(Receipt));
        Controls objControl = new Controls();
        DataTable dtGridData = new DataTable();

        int TrustID = 0;
        int SchoolID = 0;
        int CategoryID = 0;
        int HeadID = 0;
        int SubheadID = 0;
        int SectionMID = 0;
        double amt1 = 0;
        double amt2 = 0;
        double total = -1;
        string FAYEAR = "";
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
                    divlblBalance.Visible = false;
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
                    getNewRows();

                    BindGrid();
                    BindSection();
                    BindBudgetCategory();
                    BindAccountGroup();
                    PanelVisibility(1);

                    ViewState["Mode"] = "Save";

                    if (Request.QueryString["modetype"] == "new")
                        btnAddNew_Click(sender, e);

                }

                if (ViewState["grid"] != null)
                {
                    dtGridData = (DataTable)ViewState["grid"];
                    dtGridData.Rows.Clear();

                    for (int i = 0; i < gvReceiptsEntry.Rows.Count; i++)
                    {
                        DataRow dr = dtGridData.NewRow();

                        dr[0] = "0";
                        dr[1] = ((DropDownList)(gvReceiptsEntry.Rows[i].Cells[1].FindControl("ddlAccountName"))).SelectedItem.Value;
                        dr[2] = ((TextBox)(gvReceiptsEntry.Rows[i].Cells[2].FindControl("txtCreditAmount"))).Text;

                        dtGridData.Rows.Add(dr);
                    }
                    ViewState["grid"] = dtGridData;
                }
                else
                {
                    dtGridData.Rows.Clear();
                    dtGridData.Columns.Add("ReceiptPaymentID");
                    dtGridData.Columns.Add("AccountID");
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
                        ddlBudgetCategory.Items.Insert(0, new ListItem("-Select-", ""));
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
                            //ddlBudgetHeading.ClearSelection();
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

        #region Button Click Events

        #region Add New Button
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            BindAcademicYear();
            PanelVisibility(2);
            clear();
            ddlBudgetCategory.ClearSelection();
            ddlBudgetHeading.ClearSelection();
            ddlBudgetSubHeading.ClearSelection();
            txtUnutilizedBudget.Text = "0";
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

        #region Add New Row
        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (ViewState["ADDRow"] != null)
            {
                gvReceiptsEntry.DataSource = (DataTable)ViewState["ADDRow"];
                gvReceiptsEntry.DataBind();
                int cnt = gvReceiptsEntry.Rows.Count;
                cnt++;

                dt = (DataTable)ViewState["ADDRow"];
                dt.Rows.Add(cnt.ToString());

                gvReceiptsEntry.DataSource = dt;
                gvReceiptsEntry.DataBind();
            }
            else
            {
                int cnt = gvReceiptsEntry.Rows.Count;
                cnt++;
                dt.Columns.Add("Number");
                dt.Columns.Add("ReceiptPaymentID");
                for (int i = 0; i < cnt; i++)
                {
                    dt.Rows.Add(i.ToString(), "0");
                }
                gvReceiptsEntry.DataSource = dt;
            }
            BindAccountGroup();
            ViewState["ADDRow"] = dt;
            if (ViewState["grid"] != null)
            {
                dtGridData = (DataTable)ViewState["grid"];

                for (int i = 0; i < dtGridData.Rows.Count; i++)
                {
                    int value = 0;
                    gvReceiptsEntry.Rows[i].Cells[0].Text = Convert.ToString(value);
                    ((DropDownList)(gvReceiptsEntry.Rows[i].Cells[1].FindControl("ddlAccountName"))).SelectedValue = dtGridData.Rows[i][1].ToString();
                    ((TextBox)(gvReceiptsEntry.Rows[i].Cells[2].FindControl("txtCreditAmount"))).Text = dtGridData.Rows[i][2].ToString();
                }
            }
            setControlScript();
            getDebitCreditSum();
        }
        #endregion

        #region Save Button
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ReceiptPaymentBL objReceiptPaymentBL = new ReceiptPaymentBL();
                ReceiptPaymentBO objReceiptPaymentBO = new ReceiptPaymentBO();
                ApplicationResult objResultInsert = new ApplicationResult();
                ApplicationResult objResultUpdate = new ApplicationResult();

                DataTable dtResult = new DataTable();
                double dbSum = 0.0;
                DateTime dtFromDate = Convert.ToDateTime(Session[ApplicationSession.ACCOUNTFROMDATE].ToString());
                DateTime dtToDate = Convert.ToDateTime(Session[ApplicationSession.ACCOUNTTODATE].ToString());
                DateTime dtCurrentDate = Convert.ToDateTime(txtDate.Text);

                if (ViewState["Mode"].ToString() == "Save")
                {
                    int intvalidate = 0;
                    for (int i = 0; i < gvReceiptsEntry.Rows.Count; i++)
                    {
                        string ddlVal = ((DropDownList)(gvReceiptsEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).SelectedItem.Value;
                        string strDebitAmt = ((TextBox)(gvReceiptsEntry.Rows[i].Cells[1].FindControl("txtCreditAmount"))).Text;

                        if (strDebitAmt != "")
                        {
                            if (ddlVal == "-1")
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Select General Ledger Account or enter amount before save Receipt entry.');</script>");
                                return;
                            }
                        }
                        else
                        {
                            intvalidate = intvalidate + 1;
                        }
                        if (gvReceiptsEntry.Rows.Count == intvalidate)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Select General Ledger Account or enter amount before save Receipt entry.');</script>");
                            return;
                        }
                    }
                    if (txtDate.Text == "" || txtDate.Text == "&nbsp;")
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                            "<script>alert('Select Date First.');</script>");
                    }
                    else if (ddlReceiptMode.SelectedValue == "-1" || ddlGeneralLedger.SelectedValue == "-1")
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                            "<script>alert('Please select mode of Payment and General Ledger.');</script>");
                    }
                    else
                    {
                        objReceiptPaymentBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                        objReceiptPaymentBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                        objReceiptPaymentBO.SectionMID = Convert.ToInt32(ddlSection.SelectedValue.ToString());
                        objReceiptPaymentBO.BudgetCategoryMID = Convert.ToInt32(ddlBudgetCategory.SelectedValue);
                        objReceiptPaymentBO.BudgetHeadingMID = Convert.ToInt32(ddlBudgetHeading.SelectedValue);
                        objReceiptPaymentBO.BudgetSubHeadingMID = Convert.ToInt32(ddlBudgetSubHeading.SelectedValue);
                        objReceiptPaymentBO.ReceiptPaymentDate = dtCurrentDate.ToShortDateString();
                        objReceiptPaymentBO.Year = Convert.ToInt32(Session[ApplicationSession.FINANCIALYEAR]);
                        objReceiptPaymentBO.TransactionType = "Receipt";
                        objReceiptPaymentBO.GeneralLedger = Convert.ToInt32(ddlGeneralLedger.SelectedValue);
                        objReceiptPaymentBO.Narration = txtNarration.Text;
                        objReceiptPaymentBO.CreatedBy = Convert.ToInt32(Session[ApplicationSession.USERID]);
                        objReceiptPaymentBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString("dd/MM/yyyy");
                        objReceiptPaymentBO.IsDeleted = 0;
                        objReceiptPaymentBO.LastModifideDate = DateTime.UtcNow.AddHours(5.5).ToString("dd/MM/yyyy");
                        objReceiptPaymentBO.LastModifideUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);

                        if (dtFromDate <= dtCurrentDate && dtCurrentDate <= dtToDate)
                        {
                            if (ddlReceiptMode.SelectedValue == "Cash")
                            {
                                objReceiptPaymentBO.TransactionMode = "Cash";
                            }
                            else if (ddlReceiptMode.SelectedValue == "Bank" || ddlReceiptMode.SelectedValue == "ODCC")
                            {
                                objReceiptPaymentBO.TransactionMode = ddlReceiptMode.SelectedValue;
                                objReceiptPaymentBO.BankName = txtBankName.Text;
                                objReceiptPaymentBO.BranchName = txtBranchName.Text;
                                if (txtChequeNo.Text.Length > 0)
                                    objReceiptPaymentBO.ChequeNo = Convert.ToInt32(txtChequeNo.Text);
                            }

                            for (int j = 0; j < gvReceiptsEntry.Rows.Count; j++)
                            {
                                GridViewRow row = gvReceiptsEntry.Rows[j];
                                TextBox txtCrAmount = (TextBox)row.FindControl("txtCreditAmount");
                                DropDownList ddlAccName = (DropDownList)row.FindControl("ddlAccountName");
                                if (txtCrAmount.Text == "" && ddlAccName.SelectedValue == "-1")
                                {
                                    continue;
                                }
                                else if (txtCrAmount.Text == "" || ddlAccName.SelectedValue == "-1")
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                        "<script>alert('Select Account Name or Amount');</script>");
                                    break;
                                }
                                else
                                {
                                    objReceiptPaymentBO.LedgerID = Convert.ToInt32(ddlAccName.SelectedValue);
                                    dbSum = Convert.ToDouble(txtCrAmount.Text);
                                    objReceiptPaymentBO.Amount = dbSum;
                                }
                                objResultInsert = objReceiptPaymentBL.ReceiptPayment_Insert(objReceiptPaymentBO);

                                if (j == 0)
                                {
                                    if (objResultInsert != null)
                                    {
                                        dtResult = objResultInsert.resultDT;
                                        if (dtResult.Rows.Count > 0)
                                        {
                                            if (dtResult.Rows[0][0].ToString() == "0")
                                            {
                                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                                    "<script>alert('Please Initialize Receipt Start No. For This Year.');</script>");
                                                goto Exit;
                                            }
                                            else
                                            {
                                                ViewState["ReceiptNo"] = Convert.ToInt32(dtResult.Rows[0][0]);
                                                ViewState["ReceiptCode"] = dtResult.Rows[0][1].ToString();
                                                objReceiptPaymentBO.ReceiptPaymentNo =
                                                    Convert.ToInt32(ViewState["ReceiptNo"]);
                                                objReceiptPaymentBO.ReceiptPaymentCode =
                                                    ViewState["ReceiptCode"].ToString();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    objReceiptPaymentBO.ReceiptPaymentNo = Convert.ToInt32(ViewState["ReceiptNo"]);
                                    objReceiptPaymentBO.ReceiptPaymentCode = ViewState["ReceiptCode"].ToString();
                                }
                            }
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                "<script>alert('Receipt Saved Successfully. Receipt No is " +
                                ViewState["ReceiptCode"].ToString() + "');</script>");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                "<script>alert('Select Date according to your Accounting Period.');</script>");
                        }
                    }
                    BindGrid();
                    clear();
                    PanelVisibility(1);
                }
                else
                {
                    int intvalidate = 0;
                    for (int i = 0; i < gvReceiptsEntry.Rows.Count; i++)
                    {
                        string ddlVal = ((DropDownList)(gvReceiptsEntry.Rows[i].Cells[0].FindControl("ddlAccountName"))).SelectedItem.Value;
                        string strDebitAmt = ((TextBox)(gvReceiptsEntry.Rows[i].Cells[1].FindControl("txtCreditAmount"))).Text;

                        if (strDebitAmt != "")
                        {
                            if (ddlVal == "-1")
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Select General Ledger Account or enter amount before save Receipt entry.');</script>");
                                return;
                            }
                        }
                        else
                        {
                            intvalidate = intvalidate + 1;
                        }
                        if (gvReceiptsEntry.Rows.Count == intvalidate)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Select General Ledger Account or enter amount before save Receipt entry.');</script>");
                            return;
                        }
                    }
                    ApplicationResult objResultDelete = new ApplicationResult();

                    if (txtDate.Text == "" || txtDate.Text == "&nbsp;")
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                            "<script>alert('Select Date First.');</script>");
                    }
                    else if (ddlReceiptMode.SelectedValue == "-1" || ddlGeneralLedger.SelectedValue == "-1")
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                            "<script>alert('Please select mode of Payment and General Ledger.');</script>");
                    }
                    else
                    {
                        objReceiptPaymentBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                        objReceiptPaymentBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);

                        objReceiptPaymentBO.SectionMID = Convert.ToInt32(ddlSection.SelectedValue.ToString());
                        objReceiptPaymentBO.BudgetCategoryMID = Convert.ToInt32(ddlSection.SelectedValue.ToString());
                        objReceiptPaymentBO.BudgetHeadingMID = Convert.ToInt32(ddlSection.SelectedValue.ToString());
                        objReceiptPaymentBO.BudgetSubHeadingMID = Convert.ToInt32(ddlSection.SelectedValue.ToString());
                        
                        objReceiptPaymentBO.ReceiptPaymentDate = dtCurrentDate.ToShortDateString();
                        objReceiptPaymentBO.Year = Convert.ToInt32(Session[ApplicationSession.FINANCIALYEAR]);
                        objReceiptPaymentBO.TransactionType = "Receipt";
                        objReceiptPaymentBO.GeneralLedger = Convert.ToInt32(ddlGeneralLedger.SelectedValue);
                        objReceiptPaymentBO.Narration = txtNarration.Text;
                        objReceiptPaymentBO.CreatedBy = Convert.ToInt32(Session[ApplicationSession.USERID]);
                        objReceiptPaymentBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString("dd/MM/yyyy");
                        objReceiptPaymentBO.IsDeleted = 0;
                        objReceiptPaymentBO.LastModifideDate = DateTime.UtcNow.AddHours(5.5).ToString("dd/MM/yyyy");
                        objReceiptPaymentBO.LastModifideUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                        objReceiptPaymentBO.ReceiptPaymentNo = Convert.ToInt32(txtVoucherCode.Text.Substring(6, txtVoucherCode.Text.Length - 6));
                        objReceiptPaymentBO.ReceiptPaymentCode = txtVoucherCode.Text;

                        if (dtFromDate <= dtCurrentDate && dtCurrentDate <= dtToDate)
                        {
                            if (ddlReceiptMode.SelectedValue == "Cash")
                            {
                                objReceiptPaymentBO.TransactionMode = "Cash";
                            }
                            else if (ddlReceiptMode.SelectedValue == "Bank" || ddlReceiptMode.SelectedValue == "ODCC")
                            {
                                objReceiptPaymentBO.TransactionMode = ddlReceiptMode.SelectedValue;
                                objReceiptPaymentBO.BankName = txtBankName.Text;
                                objReceiptPaymentBO.BranchName = txtBranchName.Text;
                                objReceiptPaymentBO.ChequeNo = Convert.ToInt32(txtChequeNo.Text);
                            }

                            DatabaseTransaction.OpenConnectionTransation();

                            objResultDelete = objReceiptPaymentBL.ReceiptPayment_Delete_Transaction(txtVoucherCode.Text, "Receipt");

                            for (int j = 0; j < gvReceiptsEntry.Rows.Count; j++)
                            {
                                GridViewRow row = gvReceiptsEntry.Rows[j];
                                TextBox txtCrAmount = (TextBox)row.FindControl("txtCreditAmount");
                                DropDownList ddlAccName = (DropDownList)row.FindControl("ddlAccountName");
                                if (txtCrAmount.Text == "" && ddlAccName.SelectedValue == "-1")
                                {
                                    continue;
                                }
                                else if (txtCrAmount.Text == "" || ddlAccName.SelectedValue == "-1")
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                        "<script>alert('Select Account Name or Amount');</script>");
                                    break;
                                }
                                else
                                {
                                    objReceiptPaymentBO.LedgerID = Convert.ToInt32(ddlAccName.SelectedValue);
                                    dbSum = Convert.ToDouble(txtCrAmount.Text);
                                    objReceiptPaymentBO.Amount = dbSum;
                                }
                                objResultUpdate = objReceiptPaymentBL.ReceiptPayment_Update(objReceiptPaymentBO);
                            }
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Receipt Updated Successfully. Receipt No is " + ViewState["ReceiptCode"].ToString() + "');</script>");
                            DatabaseTransaction.CommitTransation();
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                "<script>alert('Select Date according to your Accounting Period.');</script>");
                        }
                    }
                    BindGrid();
                    clear();
                    PanelVisibility(1);
                }
            Exit: ;
            }
            catch (Exception ex)
            {
                if (ViewState["Mode"].ToString() == "Edit")
                    DatabaseTransaction.RollbackTransation();
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                    "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
            finally
            {
                DatabaseTransaction.connection.Close();
            }
        }
        #endregion

        #region Link Button Createnew
        protected void lbtnCreateNew_OnClick(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Session[ApplicationSession.SCHOOLID]) == 0)
                Response.Redirect("GeneralLedger.aspx?mode=TU&modetype=new&page=receipt", false);
            else
                Response.Redirect("GeneralLedger.aspx", false);
        }
        #endregion
        #endregion

        #region Drop Down Index Changed Events
        #region Receipt Mode Selected Index Changed Event
        protected void ddlReceiptMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlReceiptMode.SelectedValue != "")
                {
                    GeneralLedgerBL objGeneralLedgerBL = new GeneralLedgerBL();
                    GeneralLedgerBO objGeneralLedgerBO = new GeneralLedgerBO();
                    ApplicationResult objResultSelect = new ApplicationResult();
                    DataTable dtSelect = new DataTable();
                    divlblBalance.Visible = false;
                    if (ddlReceiptMode.SelectedValue == "Cash")
                    {
                        txtChequeNo.Text = "";
                        txtBankName.Text = "";
                        txtBranchName.Text = "";
                        txtChequeNo.Enabled = false;
                        txtBankName.Enabled = false;
                        txtBranchName.Enabled = false;
                        ddlGeneralLedger.ClearSelection();

                        objResultSelect = objGeneralLedgerBL.GeneralLedger_Select_Receipt_Payment(1, Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                        if (objResultSelect != null)
                        {
                            dtSelect = objResultSelect.resultDT;

                            objControl.BindDropDown_ListBox(dtSelect, ddlGeneralLedger, "AccountName", "LedgerID");
                            ddlGeneralLedger.Items.Insert(0, new ListItem("--Select--", ""));
                        }
                    }
                    else if (ddlReceiptMode.SelectedValue == "Bank")
                    {
                        txtChequeNo.Text = "";
                        txtBankName.Text = "";
                        txtBranchName.Text = "";
                        txtChequeNo.Enabled = true;
                        txtBankName.Enabled = true;
                        txtBranchName.Enabled = true;
                        ddlGeneralLedger.ClearSelection();

                        objResultSelect = objGeneralLedgerBL.GeneralLedger_Select_Receipt_Payment(2, Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                        if (objResultSelect != null)
                        {
                            dtSelect = objResultSelect.resultDT;

                            objControl.BindDropDown_ListBox(dtSelect, ddlGeneralLedger, "AccountName", "LedgerID");
                            ddlGeneralLedger.Items.Insert(0, new ListItem("--Select--", ""));
                        }
                    }
                    else
                    {
                        txtChequeNo.Text = "";
                        txtBankName.Text = "";
                        txtBranchName.Text = "";
                        txtChequeNo.Enabled = true;
                        txtBankName.Enabled = true;
                        txtBranchName.Enabled = true;
                        ddlGeneralLedger.ClearSelection();

                        objResultSelect = objGeneralLedgerBL.GeneralLedger_Select_Receipt_Payment(3, Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                        if (objResultSelect != null)
                        {
                            dtSelect = objResultSelect.resultDT;

                            objControl.BindDropDown_ListBox(dtSelect, ddlGeneralLedger, "AccountName", "LedgerID");
                            ddlGeneralLedger.Items.Insert(0, new ListItem("--Select--", ""));
                        }
                    }
                }
                else
                {
                    divlblBalance.Visible = false;
                    ddlGeneralLedger.Items.Clear();
                    ddlGeneralLedger.Items.Insert(0, new ListItem("--Select--", ""));
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
        #endregion

        #region Gridview Events
        protected void gvReceipts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ReceiptPaymentBL objReceiptPaymentBL = new ReceiptPaymentBL();
                ReceiptPaymentBO objReceiptPaymentBO = new ReceiptPaymentBO();
                ApplicationResult objResultSelect = new ApplicationResult();
                ViewState["ReceiptCode"] = e.CommandArgument.ToString();

                if (e.CommandName.ToString() == "Edit1")
                {
                    gvReceipts.SelectedIndex = -1;
                    objResultSelect = objReceiptPaymentBL.ReceiptPayment_Select(ViewState["ReceiptCode"].ToString(), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                    if (objResultSelect != null)
                    {
                        DataTable dtSelect = new DataTable();
                        dtSelect = objResultSelect.resultDT;
                        if (dtSelect.Rows.Count > 0)
                        {
                            ViewState["ReceiptRow"] = dtSelect.Rows.Count;

                            DataTable dt = new DataTable();

                            dt.Columns.Add("Number");
                            dt.Columns.Add("ReceiptPaymentID");
                            for (int i = 1; i <= Convert.ToInt32(ViewState["ReceiptRow"]); i++)
                            {
                                dt.Rows.Add(i.ToString(), "0");
                            }
                            ViewState["ADDRow"] = dt;
                            gvReceiptsEntry.DataSource = dt;
                            gvReceiptsEntry.DataBind();

                            BindAccountGroup();

                            txtDate.Text = dtSelect.Rows[0]["ReceiptPaymentDate"].ToString();

                            ddlReceiptMode.SelectedValue = dtSelect.Rows[0]["TransactionMode"].ToString();
                            ddlReceiptMode_SelectedIndexChanged(sender, e);
                            ddlGeneralLedger.SelectedValue = dtSelect.Rows[0]["GeneralLedger"].ToString();
                            txtVoucherCode.Text = ViewState["ReceiptCode"].ToString();
                            txtChequeNo.Text = dtSelect.Rows[0]["ChequeNo"].ToString();
                            txtBankName.Text = dtSelect.Rows[0]["BankName"].ToString();
                            txtBranchName.Text = dtSelect.Rows[0]["BranchName"].ToString();
                            ddlSection.SelectedValue = dtSelect.Rows[0]["SectionMID"].ToString();
                            ddlBudgetCategory.SelectedValue = dtSelect.Rows[0]["BudgetCategoryMID"].ToString();

                            ////Find and Fetch Heading
                            //Controls objControls = new Controls();
                            //Controls objControls1 = new Controls();
                            //DataTable dt1 = new DataTable();
                            //BindBudgetCategory();
                            //dt = FetchHeading(Convert.ToInt32(dtSelect.Rows[0][BudgetSubHeadingMBO.BudgetHeading_BUDGETCATEGORYMID]), Convert.ToInt32(dtSelect.Rows[0][BudgetSubHeadingMBO.BudgetHeading_BUDGETHEADINGMID]));
                            //if (dt.Rows.Count > 0)
                            //{
                            //    objControls.BindDropDown_ListBox(dt, ddlBudgetHeading, "HeadingName", "BudgetHeadingMID");
                            //}
                            ////ddlBudgetHeading.SelectedValue = dtSelect.Rows[0]["BudgetHeadingMID"].ToString();
                            ///

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
                            txtNarration.Text = dtSelect.Rows[0]["Narration"].ToString();

                            for (int i = 0; i < dtSelect.Rows.Count; i++)
                            {
                                gvReceiptsEntry.Rows[i].Cells[0].Text = dtSelect.Rows[i][0].ToString();
                                ((DropDownList)gvReceiptsEntry.Rows[i].Cells[1].FindControl("ddlAccountName")).SelectedValue = dtSelect.Rows[i]["LedgerID"].ToString();
                                ((TextBox)gvReceiptsEntry.Rows[i].Cells[2].FindControl("txtCreditAmount")).Text = dtSelect.Rows[i]["Amount"].ToString();
                            }
                            PanelVisibility(2);
                            setControlScript();
                            getDebitCreditSum();
                            fnOpeningBalance(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(ddlGeneralLedger.SelectedValue), txtDate.Text);
                            ViewState["Mode"] = "Edit";
                        }
                    }
                }
                else if (e.CommandName.ToString() == "Delete1")
                {
                    ApplicationResult objResultDelete = new ApplicationResult();
                    objResultDelete = objReceiptPaymentBL.ReceiptPayment_Delete(e.CommandArgument.ToString(), "Receipt");
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
        protected void gvReceipts_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        protected void gvReceipts_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvReceipts.Rows.Count > 0)
                {
                    gvReceipts.UseAccessibleHeader = true;
                    gvReceipts.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        protected void gvReceipts_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvReceipts.SelectedIndex = -1;
        }
        #endregion

        #region User Define Functions

        #region Bind Grid
        public void BindGrid()
        {
            try
            {
                ReceiptPaymentBL objReceiptPaymentBL = new ReceiptPaymentBL();
                ReceiptPaymentBO objReceiptPaymentBO = new ReceiptPaymentBO();
                ApplicationResult objResultSelectAll = new ApplicationResult();

                objResultSelectAll = objReceiptPaymentBL.ReceiptPayment_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(Session[ApplicationSession.FINANCIALYEAR]), Session[ApplicationSession.ACCOUNTFROMDATE].ToString(), Session[ApplicationSession.ACCOUNTTODATE].ToString(), "Receipt");
                if (objResultSelectAll != null)
                {
                    DataTable dtSelectAll = new DataTable();
                    dtSelectAll = objResultSelectAll.resultDT;
                    gvReceipts.DataSource = dtSelectAll;
                    gvReceipts.DataBind();
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

                for (int i = 0; i < gvReceiptsEntry.Rows.Count; i++)
                {
                    ddlAccountName = (DropDownList)gvReceiptsEntry.Rows[i].Cells[0].FindControl("ddlAccountName");
                    if (ddlAccountName != null)
                    {
                        objControl.BindDropDown_ListBox(dtSelectAll, ddlAccountName, "AccountName", "LedgerID");
                        ddlAccountName.Items.Insert(0, new ListItem("", "-1"));
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
            ViewState["ReceitNo"] = "";
            ViewState["ReceiptCode"] = "";
            ViewState["ReceiptRow"] = "0";
            GetMaxDate();
            divlblBalance.Visible = false;

            gvReceiptsEntry.DataSource = null;
            getNewRows();
            gvReceiptsEntry.DataBind();

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

        #region SetJavascriptToControls
        public void setControlScript()
        {
            for (int i = 0; i < gvReceiptsEntry.Rows.Count; i++)
            {
                ((DropDownList)(gvReceiptsEntry.Rows[i].Cells[1].FindControl("ddlAccountName"))).Attributes.Add("onchange", "javascript:makeTextBoxFocus(" + i + ");");
                ((TextBox)(gvReceiptsEntry.Rows[i].Cells[2].FindControl("txtCreditAmount"))).Attributes.Add("onfocus", "javascript:colorfocusTextBox(" + (2 * i) + ");");
            }
            txtDate.Attributes.Add("onfocus", "javascript:clearFocus();");
            txtNarration.Attributes.Add("onfocus", "javascript:clearFocus();");

            btnAddRow.Attributes.Add("onfocus", "javascript:clearFocus();");
            btnSave.Attributes.Add("onfocus", "javascript:clearFocus();");
        }
        #endregion

        #region DisableAlternateText
        public void enableDisableText()
        {
            for (int i = 0; i < gvReceiptsEntry.Rows.Count; i++)
            {
                TextBox txtDebit = ((TextBox)(gvReceiptsEntry.Rows[i].Cells[1].FindControl("txtDebitAmount")));
                TextBox txtCredit = ((TextBox)(gvReceiptsEntry.Rows[i].Cells[2].FindControl("txtCreditAmount")));

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
            for (int i = 0; i < gvReceiptsEntry.Rows.Count; i++)
            {
                //txtDebitAmount = (TextBox)(gvReceiptsEntry.Rows[i].Cells[1].FindControl("txtDebitAmount"));
                //if (txtDebitAmount.Text.Length > 0)
                //{
                //    dblDebitSum += Convert.ToDouble(txtDebitAmount.Text.ToString());
                //}
                txtCreditAmount = (TextBox)(gvReceiptsEntry.Rows[i].Cells[2].FindControl("txtCreditAmount"));
                if (txtCreditAmount.Text.Length > 0)
                {
                    dblCreditSum += Convert.ToDouble(txtCreditAmount.Text.ToString());
                }
            }
            //((System.Web.UI.HtmlControls.HtmlInputText)(gvReceiptsEntry.FooterRow.Cells[1].FindControl("txtDebitSum"))).Value = dblDebitSum.ToString();
            ((System.Web.UI.HtmlControls.HtmlInputText)(gvReceiptsEntry.FooterRow.Cells[2].FindControl("txtCreditSum"))).Value = dblCreditSum.ToString();
        }
        #endregion

        #region AddNewRowInGridDynamically
        public void getNewRows()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Number");
            dt.Columns.Add("ReceiptPaymentID");
            for (int i = 1; i <= 10; i++)
            {
                dt.Rows.Add(i.ToString(), "0");
            }
            ViewState["ADDRow"] = dt;
            gvReceiptsEntry.DataSource = dt;
            gvReceiptsEntry.DataBind();
        }
        #endregion

        #region Get Max Date

        public void GetMaxDate()
        {
            JournalVoucherMBL objJournalVoucherMBL = new JournalVoucherMBL();
            ApplicationResult objresult = new ApplicationResult();
            objresult = objJournalVoucherMBL.GetMaxDate(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), "Receipt", Convert.ToInt32(Session[ApplicationSession.FINANCIALYEAR]));
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



        #endregion

        #region FindOpeningBalance
        private void fnOpeningBalance(int intTrustMID, int intSchoolMID, int intLedgerID, string strToDate)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                GeneralLedgerBL objGLedgerBL = new GeneralLedgerBL();
                if (Convert.ToString(intLedgerID) != "")
                {
                    divlblBalance.Visible = true;
                    objResult = objGLedgerBL.Select_OpeningBalanceForAccounting(intTrustMID, intSchoolMID, intLedgerID, strToDate);
                    if (objResult != null)
                    {

                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            lblCurrentBalance.Text = objResult.resultDT.Rows[0][0].ToString();
                        }
                        else
                        {
                            lblCurrentBalance.Text = "0";
                        }
                    }
                }
                else
                {
                    divlblBalance.Visible = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region SelectedIndexChanged og GeneralLedger Dropdown
        protected void ddlGeneralLedger_SelectedIndexChanged(object sender, EventArgs e)
        {

            fnOpeningBalance(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(ddlGeneralLedger.SelectedValue), txtDate.Text);
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

                                        //hfBudgetScreenTID.Value = dtResult.Rows[0]["BudgetScreenTID"].ToString();
                                        //hfBudgetScreenID.Value = dtResult.Rows[0]["BudgetScreenID"].ToString();
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