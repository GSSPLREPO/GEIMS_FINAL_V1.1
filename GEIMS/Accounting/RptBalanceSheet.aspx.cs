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
    public partial class RptBalanceSheet : PageBase
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
                //lblDuration.Text = Session[ApplicationSession.TRUSTNAME] + ". Account Duration : " +
                //                   Session[ApplicationSession.ACCOUNTFROMDATE] + " To " +
                //                   Session[ApplicationSession.ACCOUNTTODATE];
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
                BindAccountGroup(3);
                BindAccountGroup(4);
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
                rptBalanceSheetBL objRptBalanceSheetbl = new rptBalanceSheetBL();

                objResult = objRptBalanceSheetbl.AccountGroup_Select_ByGroupNature(intGroup);
                if (intGroup == 3)
                {
                    gvLiability.DataSource = objResult.resultDT;
                    gvLiability.DataBind();
                }
                else if (intGroup == 4)
                {
                    gvAsset.DataSource = objResult.resultDT;
                    gvAsset.DataBind();
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
            //chkNarration.Checked = false;
            //gvDayBookReport.DataSource = null;
            //gvDayBookReport.DataBind();

            //var colCount = gvGeneralLedgerReport.Columns.Count;
            ////Leave column 0 -- our select and view template column
            //while (colCount > 5)
            //{
            //    gvGeneralLedgerReport.Columns.RemoveAt(colCount - 1);
            //    --colCount;
            //}
            txtFromDate.Text = Session[ApplicationSession.ACCOUNTFROMDATE].ToString();
            txtToDate.Text = Session[ApplicationSession.ACCOUNTTODATE].ToString();
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
                dlBalanceSheet.Visible = false;
                BindAccountGroup(3);
                BindAccountGroup(4);
            }
            else if (intcode == 2)
            {
                divGrid.Visible = true;
                tabs.Visible = false;
                btnBack.Visible = true;
                btnExportExcel.Visible = true;
                dlBalanceSheet.Visible = true;
            }
        }
        #endregion



        #endregion

        #region dlBalanceSheet ItemDatabound Event
        protected void dlBalanceSheet_OnItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataList gvLiabilityTran = (DataList)e.Item.FindControl("dlLiabilityTransaction");
                DataList gvAssetTran = (DataList)e.Item.FindControl("dlAssetTransaction");

                gvLiabilityTran.DataSource = (DataTable)ViewState["gvLiability"];
                gvLiabilityTran.DataBind();
                gvAssetTran.DataSource = (DataTable)ViewState["gvAsset"];
                gvAssetTran.DataBind();
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
                rptBalanceSheetBL objRptBalanceSheetbl = new rptBalanceSheetBL();

                DataTable dtResultExpense = new DataTable();
                dtResultExpense.Columns.Add("AccountGroupID", typeof(int));
                dtResultExpense.Columns.Add("AccountName", typeof(string));
                dtResultExpense.Columns.Add("Debit", typeof(string));
                dtResultExpense.Columns.Add("SubDebit", typeof(string));
                dtResultExpense.Columns.Add("Priority", typeof(int));

                int intPriority = 0;
                int intAccountGroupID = 0;

                for (int i = 0; i < gvLiability.Rows.Count; i++)
                {
                    TextBox txtSequence = (TextBox)gvLiability.Rows[i].FindControl("txtSequence1");
                    intPriority = Convert.ToInt32(txtSequence.Text);

                    intAccountGroupID = Convert.ToInt32(gvLiability.Rows[i].Cells[0].Text);
                    objResult = objRptBalanceSheetbl.ReportBalanceSheet_LiabilityGroup(txtToDate.Text,
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
                for (int r = 1; r <= gvLiability.Rows.Count; r++)
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

                for (int i1 = 0; i1 < gvAsset.Rows.Count; i1++)
                {
                    TextBox txtSequence1 = (TextBox)gvAsset.Rows[i1].FindControl("txtSequence");
                    intPriority1 = Convert.ToInt32(txtSequence1.Text);

                    intAccountGroupID1 = Convert.ToInt32(gvAsset.Rows[i1].Cells[0].Text);
                    objResult1 = objRptBalanceSheetbl.ReportBalanceSheet_AssetGroup(txtToDate.Text,
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

                for (int t = 1; t <= gvAsset.Rows.Count; t++)
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

                double dblDiff = 0.00, dblTotal = 0.00, dblNetProfit = 0.00;
                ApplicationResult objNetResult = new ApplicationResult();
                objNetResult = objRptBalanceSheetbl.ReportBalanceSheet_Result(txtToDate.Text,
                    Convert.ToInt32(Session[ApplicationSession.TRUSTID].ToString()),
                    Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString()));

                if (objNetResult != null)
                {
                    if (objNetResult.resultDT.Rows.Count > 0)
                    {
                        if (objNetResult.resultDT.Rows[0]["Diff"].ToString() != "")
                        {
                            dblDiff = Convert.ToDouble(objNetResult.resultDT.Rows[0]["Diff"].ToString());
                        }
                        else
                        {
                            dblDiff = 0.0;
                        }
                        if (objNetResult.resultDT.Rows[0]["Total"].ToString() != "")
                        {
                            dblTotal = Convert.ToDouble(objNetResult.resultDT.Rows[0]["Total"].ToString());
                        }
                        //---------------------------------------------------------------------------------
                        if (objNetResult.resultDT.Rows[0]["NetProfit"].ToString() != "")
                        {
                            dblNetProfit = Convert.ToDouble(objNetResult.resultDT.Rows[0]["NetProfit"].ToString());
                        }
                        else
                        {
                            dblNetProfit = 0.0;
                        }
                        if (dblNetProfit > 0.00)
                        {
                            dtMainExpense.Rows.Add(1, "<b>Excess of Income over expenditure</ b>", "<b style='float: right;'>" + (Math.Round(dblNetProfit, 2).ToString()) + "</b>");
                        }
                        else if (dblNetProfit < 0.00)
                        {
                            dtMainIncome.Rows.Add(1, "<b>Excess of expenditure over income</ b>", "<b style='float: right;'>" + (Math.Abs(Math.Round(dblNetProfit, 2))).ToString() + "</b>");
                        }
                        //---------------------------------------------------------------------------------
                        if (dblDiff > 0.00)
                        {
                            dtMainIncome.Rows.Add(1, "<b>Difference</b>", "<b style='float: right;'>" + (objNetResult.resultDT.Rows[0]["ODiff"].ToString()).ToString() + "</b>");
                        }
                        else if (dblDiff < 0.00)
                        {
                            dblDiff = Math.Abs(dblDiff);
                            dtMainExpense.Rows.Add(1, "<b>Difference</b>", "<b style='float: right;'>" + (objNetResult.resultDT.Rows[0]["ODiff"].ToString()).ToString() + "</b>");
                        }

                        int intdtMainExpenseCount = dtMainExpense.Rows.Count;
                        int intdtMainIncomeCount = dtMainIncome.Rows.Count;

                        int dtDiff = 0;
                        if (intdtMainExpenseCount > intdtMainIncomeCount)
                        {
                            dtDiff = intdtMainExpenseCount - intdtMainIncomeCount;
                            for (int intdtexp = 0; intdtexp < dtDiff + 1; intdtexp++)
                            {
                                if (intdtexp == (dtDiff))
                                {
                                    dtMainIncome.Rows.Add(0, "", "<b style='float: right;'><u>" + (objNetResult.resultDT.Rows[0]["Total"].ToString()).ToString() + "</u></b>");
                                    dtMainExpense.Rows.Add(0, "", "<b style='float: right;'><u>" + (objNetResult.resultDT.Rows[0]["Total"].ToString()).ToString() + "</u></b>");
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
                            for (int intdtinc = 0; intdtinc < dtDiff + 1; intdtinc++)
                            {
                                if (intdtinc == (dtDiff))
                                {
                                    dtMainExpense.Rows.Add(0, "", "<b style='float: right;'><u>" + (objNetResult.resultDT.Rows[0]["Total"].ToString()) + "</u></b>");
                                    dtMainIncome.Rows.Add(0, "", "<b style='float: right;'><u>" + (objNetResult.resultDT.Rows[0]["Total"].ToString()) + "</u></b>");
                                }
                                else
                                {
                                    dtMainExpense.Rows.Add(0, "", "");
                                }
                            }
                        }
                    }
                }

                ViewState["gvLiability"] = dtMainExpense;
                ViewState["gvAsset"] = dtMainIncome;

                ApplicationResult objIncExpResult = new ApplicationResult();
                objIncExpResult =
                    objRptBalanceSheetbl.ReportPnL_BasicInfo(Convert.ToInt32(Session[ApplicationSession.TRUSTID].ToString()),
                        Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString()), txtToDate.Text,
                        "Balance Sheet as on");

                dlBalanceSheet.DataSource = objIncExpResult.resultDT;
                dlBalanceSheet.DataBind();
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


                Response.Clear();
                Response.Buffer = true;

                Response.AddHeader("content-disposition", "attachment;filename=" + lblHeading.Text.Replace(" ", "_").Replace("<br/>", "") + "_" + DateTime.UtcNow.Year + "_" + DateTime.UtcNow.Month + "_" + DateTime.UtcNow.Day + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                var sw = new StringWriter();
                var hw = new HtmlTextWriter(sw);

                //dlIncomeExpenditureAccount.AllowPaging = false;

                dlBalanceSheet.RenderControl(hw);
                _reportTitle = lblHeading.Text;
                _date = DateTime.UtcNow.AddHours(5.5).ToShortDateString();
                var content = "<div align='center' style='border: 1px solid black; font-family:verdana;font-size:13px;color: #000000;'><br/>" + sw;
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
                    tdAccountName.Attributes.Add("style",
                        id == "0"
                            ? "text-align:right;border:1px solid black;height:25px;"
                            : "text-align:left;border:1px solid black;height:25px;");
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
                    tdAccountName.Attributes.Add("style",
                        id == "0"
                            ? "text-align:right;border:1px solid black;height:25px;"
                            : "text-align:left;border:1px solid black;height:25px;");
                }
            }
        }
    }
}