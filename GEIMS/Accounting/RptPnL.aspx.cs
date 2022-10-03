using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.BL;
using GEIMS.Common;

namespace GEIMS.Accounting
{
    public partial class RptPnL : PageBase
    {
        #region declaration
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Payment));
        readonly Controls _objControl = new Controls();
        private string _reportTitle, _date;
        #endregion

        #region Pre-Init Method
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.QueryString["mode"] != "TU")
            {
                MasterPageFile = "~/Master/SchoolMain.Master";
            }
        }
        #endregion

        #region Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Session[ApplicationSession.HASACCESSACCOUNTUSERID]) > 0)
            {
                if (IsPostBack) return;
                txtFromDate.Text = Session[ApplicationSession.ACCOUNTFROMDATE].ToString();
                txtToDate.Text = Session[ApplicationSession.ACCOUNTTODATE].ToString();
                //if (Request.QueryString["mode"] == "TU")
                //{
                //    lblDuration.Text = Session[ApplicationSession.TRUSTNAME] + ". Account Duration : " +
                //                       Session[ApplicationSession.ACCOUNTFROMDATE] + " To " +
                //                       Session[ApplicationSession.ACCOUNTTODATE];
                //}
                //else
                //{
                //    if (Convert.ToInt32(Session[ApplicationSession.SCHOOLID]) == 0)
                //    {
                //        lblDuration.Text = Session[ApplicationSession.TRUSTNAME] + ". Account Duration : " +
                //                           Session[ApplicationSession.ACCOUNTFROMDATE] + " To " +
                //                           Session[ApplicationSession.ACCOUNTTODATE];
                //    }
                //    else
                //        lblDuration.Text = Session[ApplicationSession.SCHOOLNAME] + ". Account Duration : " +
                //                           Session[ApplicationSession.ACCOUNTFROMDATE] + " To " +
                //                           Session[ApplicationSession.ACCOUNTTODATE];
                //}
                PanelVisibility(1);
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Attributes.Add("readonly", "readonly");
                BindAccountGroup(1);
                BindAccountGroup(2);
            }
            else
            {
                Response.Redirect(
                    Convert.ToInt32(Session[ApplicationSession.SCHOOLID]) == 0
                        ? "../Accounting/AccountLogin.aspx?mode=TU"
                        : "../Accounting/AccountLogin.aspx", false);
            }
        }
        #endregion

        #region BindGridView Methods

        public void BindAccountGroup(int intGroup)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                RptPnLBL objRptPnLbl = new RptPnLBL();

                objResult = objRptPnLbl.AccountGroup_Select_ByGroupNature(intGroup);
                if (intGroup == 1)
                {
                    gvIncome.DataSource = objResult.resultDT;
                    gvIncome.DataBind();
                }
                else if (intGroup == 2)
                {
                    gvExpense.DataSource = objResult.resultDT;
                    gvExpense.DataBind();
                }

            }
            catch (Exception ex)
            {
                Logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        #endregion

        #region VerifyRenderingInServerForm

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        #endregion VerifyRenderingInServerForm

        #region User Define Function

        #region Clear Operation and Reset page
        public void Clear()
        {
            if (Master != null) _objControl.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            txtFromDate.Text = Session[ApplicationSession.ACCOUNTFROMDATE].ToString();
            txtToDate.Text = Session[ApplicationSession.ACCOUNTTODATE].ToString();
            //chkNarration.Checked = false;
            //dlIncomeExpenditureAccount.DataSource = null;
            //dlIncomeExpenditureAccount.DataBind();

            //var colCount = gvGeneralLedgerReport.Columns.Count;
            ////Leave column 0 -- our select and view template column
            //while (colCount > 5)
            //{
            //    gvGeneralLedgerReport.Columns.RemoveAt(colCount - 1);
            //    --colCount;
            //}
        }

        #endregion

        #region Panel Visibility Mode
        public void PanelVisibility(int intcode)
        {
            if (intcode == 1)
            {
                divGrid.Visible = false;
                tabs.Visible = true;
                btnBack.Visible = false;
                btnExportExcel.Visible = false;
                dlIncomeExpenditureAccount.Visible = false;
                BindAccountGroup(1);
                BindAccountGroup(2);
            }
            else if (intcode == 2)
            {
                divGrid.Visible = true;
                tabs.Visible = false;
                btnBack.Visible = true;
                btnExportExcel.Visible = true;
                dlIncomeExpenditureAccount.Visible = true;
            }
        }
        #endregion



        #endregion

        #region dlIncomeExpenditureAccount ItemDatabound Event
        protected void dlIncomeExpenditureAccount_OnItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataList gvIncomeTran = (DataList)e.Item.FindControl("dlIncomeTransaction");
                DataList gvExpenseTran = (DataList)e.Item.FindControl("dlExpenseTransaction");

                gvIncomeTran.DataSource = (DataTable)ViewState["gvIncome"];
                gvIncomeTran.DataBind();
                gvExpenseTran.DataSource = (DataTable)ViewState["gvExpense"];
                gvExpenseTran.DataBind();
            }
        }
        #endregion

        #region Button Click Event
        #region Button btnGo OnClick Event
        protected void btnGo_OnClick(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                RptPnLBL objRptPnLbl = new RptPnLBL();

                DataTable dtResultExpense = new DataTable();
                dtResultExpense.Columns.Add("AccountGroupID", typeof(int));
                dtResultExpense.Columns.Add("AccountName", typeof(string));
                dtResultExpense.Columns.Add("Debit", typeof(string));
                dtResultExpense.Columns.Add("SubDebit", typeof(string));
                dtResultExpense.Columns.Add("Priority", typeof(int));

                int intPriority = 0;
                int intAccountGroupID = 0;

                for (int i = 0; i < gvExpense.Rows.Count; i++)
                {
                    TextBox txtSequence = (TextBox)gvExpense.Rows[i].FindControl("txtSequence1");
                    intPriority = Convert.ToInt32(txtSequence.Text);

                    intAccountGroupID = Convert.ToInt32(gvExpense.Rows[i].Cells[0].Text);
                    objResult = objRptPnLbl.ReportPnL_ExpenseGroup(txtToDate.Text,
                        Convert.ToInt32(Session[ApplicationSession.TRUSTID].ToString()),
                        Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString()), intAccountGroupID);
                    if (objResult != null)
                    {
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            for (int k = 0; k < objResult.resultDT.Rows.Count; k++)
                            {
                                dtResultExpense.Rows.Add(Convert.ToInt32(objResult.resultDT.Rows[k]["AccountGroupID"].ToString()), objResult.resultDT.Rows[k]["AccountName"].ToString(),
                                    objResult.resultDT.Rows[k]["Debit"].ToString(), objResult.resultDT.Rows[k]["SubDebit"].ToString(), intPriority);
                            }
                        }
                    }
                }

                DataTable dtMainExpense = new DataTable();
                dtMainExpense.Columns.Add("AccountGroupID", typeof(int));
                dtMainExpense.Columns.Add("AccountName", typeof(string));
                dtMainExpense.Columns.Add("Debit", typeof(string));
                dtMainExpense.Columns.Add("SubDebit", typeof(string));
                for (int r = 1; r <= gvExpense.Rows.Count; r++)
                {
                    DataRow[] rows = dtResultExpense.Select("Priority = " + r);
                    if (rows.Length > 0)
                    {
                        DataTable dt = rows.CopyToDataTable();
                        for (int s = 0; s < dt.Rows.Count; s++)
                        {
                            dtMainExpense.Rows.Add(Convert.ToInt32(dt.Rows[s]["AccountGroupID"].ToString()), dt.Rows[s]["AccountName"].ToString(), dt.Rows[s]["Debit"].ToString(), dt.Rows[s]["SubDebit"].ToString());
                        }
                    }
                }

                DataTable dtResultIncome = new DataTable();
                dtResultIncome.Columns.Add("AccountGroupID", typeof(int));
                dtResultIncome.Columns.Add("AccountName", typeof(string));
                dtResultIncome.Columns.Add("Debit", typeof(string));
                dtResultIncome.Columns.Add("SubDebit", typeof(string));
                dtResultIncome.Columns.Add("Priority", typeof(int));

                ApplicationResult objResult1 = new ApplicationResult();
                int intPriority1 = 0;
                int intAccountGroupID1 = 0;

                for (int i1 = 0; i1 < gvIncome.Rows.Count; i1++)
                {
                    TextBox txtSequence1 = (TextBox)gvIncome.Rows[i1].FindControl("txtSequence");
                    intPriority1 = Convert.ToInt32(txtSequence1.Text);

                    intAccountGroupID1 = Convert.ToInt32(gvIncome.Rows[i1].Cells[0].Text);
                    objResult1 = objRptPnLbl.ReportPnL_IncomeGroup(txtToDate.Text,
                        Convert.ToInt32(Session[ApplicationSession.TRUSTID].ToString()),
                        Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString()), intAccountGroupID1);
                    if (objResult1 != null)
                    {
                        if (objResult1.resultDT.Rows.Count > 0)
                        {
                            for (int k1 = 0; k1 < objResult1.resultDT.Rows.Count; k1++)
                            {
                                dtResultIncome.Rows.Add(Convert.ToInt32(objResult1.resultDT.Rows[k1]["AccountGroupID"].ToString()), objResult1.resultDT.Rows[k1]["AccountName"].ToString(),
                                    objResult1.resultDT.Rows[k1]["Debit"].ToString(), objResult1.resultDT.Rows[k1]["SubDebit"].ToString(), intPriority1);
                            }
                        }
                    }
                }
                DataTable dtMainIncome = new DataTable();
                dtMainIncome.Columns.Add("AccountGroupID", typeof(int));
                dtMainIncome.Columns.Add("AccountName", typeof(string));
                dtMainIncome.Columns.Add("Debit", typeof(string));
                dtMainIncome.Columns.Add("SubDebit", typeof(string));


                for (int t = 1; t <= gvIncome.Rows.Count; t++)
                {
                    DataRow[] rows1 = dtResultIncome.Select("Priority = " + t);
                    if (rows1.Length > 0)
                    {
                        DataTable dt1 = rows1.CopyToDataTable();
                        for (int u = 0; u < dt1.Rows.Count; u++)
                        {
                            dtMainIncome.Rows.Add(Convert.ToInt32(dt1.Rows[u]["AccountGroupID"].ToString()), dt1.Rows[u]["AccountName"].ToString(), dt1.Rows[u]["Debit"].ToString(), dt1.Rows[u]["SubDebit"].ToString());
                        }
                    }
                }

                double dblTotalIncome = 0.00, dblTotalExpense = 0.00, dblNetProfit = 0.00;
                ApplicationResult objNetResult = new ApplicationResult();
                objNetResult = objRptPnLbl.ReportPnL_NetResult(txtToDate.Text,
                    Convert.ToInt32(Session[ApplicationSession.TRUSTID].ToString()),
                    Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString()));

                if (objNetResult != null)
                {
                    if (objNetResult.resultDT.Rows.Count > 0)
                    {
                        if (objNetResult.resultDT.Rows[0]["Income"].ToString() != "")
                        {
                            dblTotalIncome = Convert.ToDouble(objNetResult.resultDT.Rows[0]["Income"].ToString());
                        }
                        if (objNetResult.resultDT.Rows[0]["Expense"].ToString() != "")
                        {
                            dblTotalExpense = Convert.ToDouble(objNetResult.resultDT.Rows[0]["Expense"].ToString());
                        }
                        if (objNetResult.resultDT.Rows[0]["NetProfit"].ToString() != "")
                        {
                            dblNetProfit = Convert.ToDouble(objNetResult.resultDT.Rows[0]["NetProfit"].ToString());
                        }


                        int intdtMainExpenseCount = dtMainExpense.Rows.Count;
                        int intdtMainIncomeCount = dtMainIncome.Rows.Count;

                        if (dblNetProfit > 0.00)
                        {
                            intdtMainExpenseCount += 2;
                            dtMainExpense.Rows.Add(1, "<b>Net Profit</b>", "<b style='float: right;'>" + objNetResult.resultDT.Rows[0]["NetProfit"].ToString() + "</b>");
                        }
                        else if (dblNetProfit < 0.00)
                        {
                            intdtMainIncomeCount += 2;
                            dtMainIncome.Rows.Add(1, "<b>Net Loss</b>", "<b style='float: right;'>" + objNetResult.resultDT.Rows[0]["NetProfit"].ToString() + "</b>");
                        }
                        int dtDiff = 0;
                        if (intdtMainExpenseCount > intdtMainIncomeCount)
                        {
                            dtDiff = intdtMainExpenseCount - intdtMainIncomeCount;
                            for (int intdtexp = 0; intdtexp < dtDiff; intdtexp++)
                            {
                                if (intdtexp == (dtDiff - 1))
                                {
                                    if (dblNetProfit > 0.00)
                                    {
                                        dtMainIncome.Rows.Add(0, "",
                                            "<b style='float: right;'><u>" + objNetResult.resultDT.Rows[0]["Income"].ToString() +
                                            "</u></b>");
                                        dtMainExpense.Rows.Add(0, "",
                                            "<b style='float: right;'><u>" + objNetResult.resultDT.Rows[0]["Income"].ToString() +
                                            "</u></b>");
                                    }
                                    else if (dblNetProfit < 0.00)
                                    {
                                        dtMainIncome.Rows.Add(0, "",
                                            "<b style='float: right;'><u>" + objNetResult.resultDT.Rows[0]["Expense"].ToString() +
                                            "</u></b>");
                                        dtMainExpense.Rows.Add(0, "",
                                            "<b style='float: right;'><u>" + objNetResult.resultDT.Rows[0]["Expense"].ToString() +
                                            "</u></b>");
                                    }
                                    else
                                    {
                                        dtMainIncome.Rows.Add(0, "",
                                            "<b style='float: right;'><u>" + objNetResult.resultDT.Rows[0]["Expense"].ToString() +
                                            "</u></b>");
                                        dtMainExpense.Rows.Add(0, "",
                                            "<b style='float: right;'><u>" + objNetResult.resultDT.Rows[0]["Expense"].ToString() +
                                            "</u></b>");
                                    }
                                }
                                else
                                {
                                    dtMainIncome.Rows.Add(0, "", "");
                                }
                            }
                        }
                        else if (intdtMainIncomeCount > intdtMainExpenseCount)
                        {
                            dtDiff = intdtMainIncomeCount - intdtMainExpenseCount;
                            for (int intdtinc = 0; intdtinc < dtDiff; intdtinc++)
                            {
                                if (intdtinc == (dtDiff - 1))
                                {
                                    //dtMainExpense.Rows.Add(0, "", "<b style='float: right;'><u>" + (Math.Round(dblTotalIncome, 2).ToString()) + "</u></b>");
                                    //dtMainIncome.Rows.Add(0, "", "<b style='float: right;'><u>" + (Math.Round(dblTotalIncome, 2).ToString()) + "</u></b>");
                                    if (dblNetProfit > 0.00)
                                    {
                                        dtMainIncome.Rows.Add(0, "",
                                            "<b style='float: right;'><u>" + objNetResult.resultDT.Rows[0]["Income"].ToString() +
                                            "</u></b>");
                                        dtMainExpense.Rows.Add(0, "",
                                            "<b style='float: right;'><u>" + objNetResult.resultDT.Rows[0]["Income"].ToString() +
                                            "</u></b>");
                                    }
                                    else if (dblNetProfit < 0.00)
                                    {
                                        dtMainIncome.Rows.Add(0, "",
                                            "<b style='float: right;'><u>" + objNetResult.resultDT.Rows[0]["Expense"].ToString() +
                                            "</u></b>");
                                        dtMainExpense.Rows.Add(0, "",
                                            "<b style='float: right;'><u>" + objNetResult.resultDT.Rows[0]["Expense"].ToString() +
                                            "</u></b>");
                                    }
                                    else
                                    {
                                        dtMainIncome.Rows.Add(0, "",
                                            "<b style='float: right;'><u>" + objNetResult.resultDT.Rows[0]["Expense"].ToString() +
                                            "</u></b>");
                                        dtMainExpense.Rows.Add(0, "",
                                            "<b style='float: right;'><u>" + objNetResult.resultDT.Rows[0]["Expense"].ToString() +
                                            "</u></b>");
                                    }
                                }
                                else
                                {
                                    dtMainExpense.Rows.Add(0, "", "");
                                }
                            }
                        }
                    }
                }

                ViewState["gvExpense"] = dtMainExpense;
                ViewState["gvIncome"] = dtMainIncome;

                ApplicationResult objIncExpResult = new ApplicationResult();
                objIncExpResult =
                    objRptPnLbl.ReportPnL_BasicInfo(Convert.ToInt32(Session[ApplicationSession.TRUSTID].ToString()),
                        Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString()), txtToDate.Text,
                        "Income & Expenditure Account for the year ended");

                dlIncomeExpenditureAccount.DataSource = objIncExpResult.resultDT;
                dlIncomeExpenditureAccount.DataBind();
                PanelVisibility(2);
            }
            catch (Exception ex)
            {
                Logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Back Button
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Clear();
            PanelVisibility(1);
        }
        #endregion

        #region Export to Excel
        protected void btnExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string strPath = Request.Url.GetLeftPart(UriPartial.Authority) + "/Images/NAVCHETAN LOGO COLOUR copy1.jpg";

                Response.Clear();
                Response.Buffer = true;

                Response.AddHeader("content-disposition", "attachment;filename=" + lblHeading.Text.Replace(" ", "_").Replace("<br/>", "") + "_" + DateTime.UtcNow.Year + "_" + DateTime.UtcNow.Month + "_" + DateTime.UtcNow.Day + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                var sw = new StringWriter();
                var hw = new HtmlTextWriter(sw);

                //dlIncomeExpenditureAccount.AllowPaging = false;

                dlIncomeExpenditureAccount.RenderControl(hw);
                _reportTitle = lblHeading.Text;
                _date = DateTime.UtcNow.AddHours(5.5).ToShortDateString();
                var content = "<div align='center' style='font-family:verdana;font-size:13px;color: #000000; padding: 5px;'><br/>" + "<img style='paddingg: 10px;' src='" + strPath + "' />" + sw;
                Response.Output.Write(content);
                const string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                Logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion
        #endregion

        protected void dlExpenseTransaction_OnItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                if (drv != null)
                {
                    HtmlControl tdAccountName = (HtmlControl)e.Item.FindControl("tdAccountName");
                    string id = drv.Row[0].ToString();
                    if (id == "0")
                    {
                        tdAccountName.Attributes.Add("style", "text-align:left;border:1px solid black;height:25px;");
                    }
                    else
                    {
                        tdAccountName.Attributes.Add("style", "text-align:left;border:1px solid black;height:25px;");
                    }

                }
            }
        }

        protected void dlIncomeTransaction_OnItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                if (drv != null)
                {
                    HtmlControl tdAccountName = (HtmlControl)e.Item.FindControl("tdiAccountName");
                    string id = drv.Row[0].ToString();
                    if (id == "0")
                    {
                        tdAccountName.Attributes.Add("style", "text-align:left;border:1px solid black;height:25px;");
                    }
                    else
                    {
                        tdAccountName.Attributes.Add("style", "text-align:left;border:1px solid black;height:25px;");
                    }

                }
            }
        }
    }
}