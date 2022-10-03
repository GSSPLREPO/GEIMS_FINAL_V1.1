using System;
using System.Data;
using System.IO;
using System.Web.UI;
using log4net;
using GEIMS.Common;
using GEIMS.BL;
using System.Web.UI.WebControls;

namespace GEIMS.Accounting
{
    public partial class RptCashbook : PageBase
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

        #region Page Load
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

        #region Button Click Events

        #region Button Get Report
        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                var objGeneralLedgerBl = new GeneralLedgerBL();
                int intIsNarration;
                if (chkNarration.Checked)
                {
                    intIsNarration = 1;
                    //var bfield = new BoundField { HeaderText = "Narration", DataField = "Description" };
                    //bfield.HeaderStyle.Width = new Unit("30%");
                    //bfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                    //bfield.HeaderStyle.VerticalAlign = VerticalAlign.Top;
                    //bfield.ItemStyle.Width = new Unit("30%");
                    //bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                    //bfield.ItemStyle.VerticalAlign = VerticalAlign.Top;
                    //bfield.ItemStyle.Wrap = false;
                    //gvCashBankReport.Columns.Add(bfield);
                }
                else
                intIsNarration = 0;

                var objResult = objGeneralLedgerBl.GeneralLedger_Select_Rpt_CashBankBook(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(ddlGeneralLedger.SelectedValue), ddlType.SelectedValue, txtFromDate.Text, txtToDate.Text, intIsNarration);

                if (objResult != null)
                {
                    //var dsresult = objResult.resutlDS;

                    //foreach (DataRow dr in dsresult.Tables[1].Rows)
                    //{
                    //    if (dsresult.Tables[0].Rows.Count>0)
                    //        dsresult.Tables[0].Rows.Add(dr.ItemArray);
                    //}

                    //double dbCrAmt = 0, dbDrAmt = 0;

                    //if (dsresult.Tables[0].Rows.Count > 0)
                    //{
                    //    if (dsresult.Tables[0].Rows[0]["Debit"].ToString() == "" || dsresult.Tables[0].Rows[0]["Debit"].ToString() == "0" || dsresult.Tables[0].Rows[0]["Debit"] == null)
                    //    {
                    //        if (dsresult.Tables[0].Rows[0]["Credit"].ToString() == "" || dsresult.Tables[0].Rows[0]["Credit"] == null)
                    //        {
                    //        }
                    //        else
                    //        {
                    //            dbCrAmt = Convert.ToInt32(dsresult.Tables[0].Rows[0]["Credit"]);
                    //        }
                    //        dbDrAmt = 0;
                    //    }
                    //    else
                    //    {
                    //        if (dsresult.Tables[0].Rows[0]["Debit"].ToString() == "" || dsresult.Tables[0].Rows[0]["Debit"] == null)
                    //        {
                    //        }
                    //        else
                    //        {
                    //            dbDrAmt = Convert.ToDouble(dsresult.Tables[0].Rows[0]["Debit"]);
                    //        }
                    //        dbCrAmt = 0;
                    //    }
                    //}

                    //if (dsresult.Tables[2].Rows.Count > 0)
                    //{
                    //    if (dsresult.Tables[2].Rows[0][6].ToString() == "" || dsresult.Tables[2].Rows[0][6].ToString() == "0" || dsresult.Tables[2].Rows[0][6] == null)
                    //        dbDrAmt += 0;
                    //    else
                    //        dbDrAmt += Convert.ToDouble(dsresult.Tables[2].Rows[0][6]);
                    //    if (dsresult.Tables[2].Rows[0][7].ToString() == "" || dsresult.Tables[2].Rows[0][7].ToString() == "0" || dsresult.Tables[2].Rows[0][7] == null)
                    //        dbCrAmt += 0;
                    //    else
                    //        dbCrAmt += Convert.ToDouble(dsresult.Tables[2].Rows[0][7]);
                    //}
                    //if (dsresult.Tables[1].Rows.Count > 0)
                    //{
                    //    if (dbDrAmt > dbCrAmt)
                    //    {
                    //        //lblClosing.Text = "Cr. " + (dbDrAmt - dbCrAmt);
                    //        // ReSharper disable once UnusedVariable
                    //        foreach (DataRow row in dsresult.Tables[2].Rows)
                    //        {
                    //            dsresult.Tables[2].Rows[0][6] = 0;
                    //            dsresult.Tables[2].Rows[0][7] = (dbDrAmt - dbCrAmt);
                    //        }
                    //    }
                    //    else if (dbDrAmt < dbCrAmt)
                    //    {
                    //        //lblClosing.Text = "Dr. " + (dbCrAmt - dbDrAmt);
                    //        // ReSharper disable once UnusedVariable
                    //        foreach (DataRow row in dsresult.Tables[2].Rows)
                    //        {
                    //            dsresult.Tables[2].Rows[0][6] = (dbCrAmt - dbDrAmt);
                    //            dsresult.Tables[2].Rows[0][7] = 0;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        //lblClosing.Text = "0";
                    //        dsresult.Tables[2].Rows[0][6] = 0;
                    //        dsresult.Tables[2].Rows[0][7] = 0;
                    //    }
                    //    btnExportExcel.Visible = true;
                    //}
                    //else
                    //{
                    //    if (dsresult.Tables[0].Rows[0]["Debit"].ToString() == "" || dsresult.Tables[0].Rows[0]["Debit"].ToString() == "0" || dsresult.Tables[0].Rows[0]["Debit"] == null)
                    //    {
                    //        //lblClosing.Text = "Cr. " + dsresult.Tables[0].Rows[0]["Credit"];
                    //        // ReSharper disable once UnusedVariable
                    //        foreach (DataRow row in dsresult.Tables[2].Rows)
                    //        {
                    //            dsresult.Tables[2].Rows[0][6] = 0;
                    //            dsresult.Tables[2].Rows[0][7] = dsresult.Tables[0].Rows[0]["Credit"];
                    //        }
                    //    }
                    //    else
                    //    {
                    //        //lblClosing.Text = "Dr. " + dsresult.Tables[0].Rows[0]["Debit"];
                    //        // ReSharper disable once UnusedVariable
                    //        foreach (DataRow row in dsresult.Tables[2].Rows)
                    //        {
                    //            dsresult.Tables[2].Rows[0][6] = dsresult.Tables[0].Rows[0]["Debit"];
                    //            dsresult.Tables[2].Rows[0][7] = 0;
                    //        }
                    //    }
                    //    btnExportExcel.Visible = false;
                    //}

                    //foreach (DataRow dr in dsresult.Tables[2].Rows)
                    //{
                    //    if (dsresult.Tables[0].Rows.Count > 0)
                    //        dsresult.Tables[0].Rows.Add(dr.ItemArray);
                    //}

                    gvCashBankReport.DataSource = objResult.resultDT;
                    gvCashBankReport.DataBind();
                    PanelVisibility(2);

                    //var strName = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]) == 0 ? Session[ApplicationSession.TRUSTNAME].ToString() : Session[ApplicationSession.SCHOOLNAME].ToString();
                    lblHeading.Text = "<b>Cash/Bank Book of " + ddlGeneralLedger.SelectedItem.Text + "</b><br/>" + txtFromDate.Text + " To " + txtToDate.Text;
                }
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

                Response.AddHeader("content-disposition", "attachment;filename=" + lblHeading.Text.Replace(" ", "_").Replace("<br/>", "").Replace("<b>", "").Replace("</b>", "") + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                var sw = new StringWriter();
                var hw = new HtmlTextWriter(sw);

                gvCashBankReport.AllowPaging = false;
                gvCashBankReport.HeaderRow.Style.Add("ForeColor", "#000000");

                gvCashBankReport.RenderControl(hw);
                _reportTitle = lblHeading.Text;
                _date = DateTime.UtcNow.AddHours(5.5).ToShortDateString();
                var content = "<div align='center' style='font-family:verdana;font-size:13px;text-align:center;color: #000000;'><span style='font-size:16px;text-align:center;font-weight:bold;color:Maroon'>Fertilizer Nagar School Trust</span><br/><span style='font-size:13px:font-weight:bold;text-align:center;'>" + _reportTitle + "</span><br/><div align='right' style='font-family:verdana;font-size:11px;text-align:right;'><strong>Date :</strong>" + _date + "</div><br/>" + sw;
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

        #region VerifyRenderingInServerForm
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        #endregion VerifyRenderingInServerForm

        #region gridview Events

        protected void gvCashBankReport_OnPreRender(object sender, EventArgs e)
        {
            if (gvCashBankReport.Rows.Count <= 0) return;
            gvCashBankReport.UseAccessibleHeader = true;
            gvCashBankReport.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        #endregion

        #region User Define Function

        #region Clear Operation and Reset page
        public void Clear()
        {
            if (Master != null) _objControl.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            chkNarration.Checked = false;
            gvCashBankReport.DataSource = null;
            gvCashBankReport.DataBind();
            txtFromDate.Text = Session[ApplicationSession.ACCOUNTFROMDATE].ToString();
            txtToDate.Text = Session[ApplicationSession.ACCOUNTTODATE].ToString();

            //var colCount = gvCashBankReport.Columns.Count;
            ////Leave column 0 -- our select and view template column
            //while (colCount > 5)
            //{
            //    gvCashBankReport.Columns.RemoveAt(colCount - 1);
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
            }
            else if (intcode == 2)
            {
                divGrid.Visible = true;
                tabs.Visible = false;
                btnBack.Visible = true;
                btnExportExcel.Visible = true;
            }
        }
        #endregion

        #region Drop Down Index Changed Events
        #region Receipt Mode Selected Index Changed Event
        protected void ddlReceiptMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlReceiptMode.SelectedValue != "")
                {
                    var objGeneralLedgerBl = new GeneralLedgerBL();
                    ApplicationResult objResultSelect;
                    DataTable dtSelect;

                    switch (ddlReceiptMode.SelectedValue)
                    {
                        case "Cash":
                            ddlGeneralLedger.ClearSelection();
                            objResultSelect = objGeneralLedgerBl.GeneralLedger_Select_Receipt_Payment(1, Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                            if (objResultSelect == null) return;
                            dtSelect = objResultSelect.resultDT;
                            _objControl.BindDropDown_ListBox(dtSelect, ddlGeneralLedger, "AccountName", "LedgerID");
                            ddlGeneralLedger.Items.Insert(0, new ListItem("--Select--", ""));
                            break;
                        case "Bank":
                            ddlGeneralLedger.ClearSelection();
                            objResultSelect = objGeneralLedgerBl.GeneralLedger_Select_Receipt_Payment(2, Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                            if (objResultSelect == null) return;
                            dtSelect = objResultSelect.resultDT;
                            _objControl.BindDropDown_ListBox(dtSelect, ddlGeneralLedger, "AccountName", "LedgerID");
                            ddlGeneralLedger.Items.Insert(0, new ListItem("--Select--", ""));
                            break;
                        default:
                            ddlGeneralLedger.ClearSelection();
                            objResultSelect = objGeneralLedgerBl.GeneralLedger_Select_Receipt_Payment(3, Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                            if (objResultSelect == null) return;
                            dtSelect = objResultSelect.resultDT;
                            _objControl.BindDropDown_ListBox(dtSelect, ddlGeneralLedger, "AccountName", "LedgerID");
                            ddlGeneralLedger.Items.Insert(0, new ListItem("--Select--", ""));
                            break;
                    }
                }
                else
                {
                    ddlGeneralLedger.Items.Clear();
                    ddlGeneralLedger.Items.Insert(0, new ListItem("--Select--", ""));
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion
        #endregion



        #endregion

        #region gvCashBankReort Row Data Bound Event
        protected void gvCashBankReport_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var Debit = e.Row.Cells[4].Text;
                var Credit = e.Row.Cells[5].Text;

                if (Debit == "0.00")
                {
                    e.Row.Cells[4].Text = "";
                }
                if (Credit == "0.00")
                {
                    e.Row.Cells[5].Text = "";
                }
            }
        }
        #endregion
    }
}