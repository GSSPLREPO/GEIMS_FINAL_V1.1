using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.Common;
using GEIMS.BL;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using log4net;

namespace GEIMS.Accounting
{
    public partial class RptGeneralLedger : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(RptGeneralLedger));

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

        #region Bind General Ledger without Cash and Bank
        //public void getGLWithoutContra()
        //{
        //    ApplicationResult objResult = new ApplicationResult();
        //    Controls objControls = new Controls();
        //    GeneralLedgerBL objGLBL = new GeneralLedgerBL();

        //    objResult = objGLBL.GeneralLedger_SelectAll_JournalEntry(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
        //    if (objResult != null)
        //    {
        //        objControls.BindDropDown_ListBox(objResult.resultDT, ddlGeneralLedger, "AccountName", "LedgerID");
        //        if (objResult.resultDT.Rows.Count > 0)
        //        {
        //        }
        //        ddlGeneralLedger.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", ""));
        //    }
        //}
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
                //getGLWithoutContra();
                BindParameterGrid();
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

        #region BindParameterGrid Method
        public void BindParameterGrid()
        {
            ApplicationResult objrResult = new ApplicationResult();
            GeneralLedgerBL objGeneralLedgerBl = new GeneralLedgerBL();

            objrResult =
                objGeneralLedgerBl.Select_GeneralLedgers(
                    Convert.ToInt32(Session[ApplicationSession.TRUSTID].ToString()),
                    Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString()));
            if (objrResult != null)
            {
                gvParameter.DataSource = objrResult.resultDT;
                gvParameter.DataBind();
            }
        }
        #endregion

        #region Header checkbox_CheckedChanged Event
        protected void chkHeader_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkHeader = new CheckBox();
                chkHeader = (CheckBox)gvParameter.HeaderRow.FindControl("chkHeader");
                if (chkHeader.Checked == true)
                {
                    CheckBox chk = new CheckBox();
                    foreach (GridViewRow rowItem in gvParameter.Rows)
                    {
                        chk = (CheckBox)(rowItem.Cells[2].FindControl("chk"));
                        chk.Checked = true;

                    }
                }
                else
                {
                    CheckBox chk = new CheckBox();
                    foreach (GridViewRow rowItem in gvParameter.Rows)
                    {
                        chk = (CheckBox)(rowItem.Cells[2].FindControl("chk"));
                        chk.Checked = false;

                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion

        #region Button Click Events

        #region Button Get Report
        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                //var objGeneralLedgerBl = new GeneralLedgerBL();
                //int intIsNarration;
                //if (chkNarration.Checked)
                //{
                //    intIsNarration = 1;
                //    var bfield = new BoundField { HeaderText = "Narration", DataField = "Description" };
                //    bfield.HeaderStyle.Width = new Unit("30%");
                //    bfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                //    bfield.HeaderStyle.VerticalAlign = VerticalAlign.Top;
                //    bfield.ItemStyle.Width = new Unit("30%");
                //    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                //    bfield.ItemStyle.VerticalAlign = VerticalAlign.Top;
                //    bfield.ItemStyle.Wrap = false;
                //    gvGeneralLedgerReport.Columns.Add(bfield);
                //}
                //else
                //    intIsNarration = 0;

                //var objResult = objGeneralLedgerBl.GeneralLedgerReport(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(ddlGeneralLedger.SelectedValue), txtFromDate.Text, txtToDate.Text, intIsNarration);

                //if (objResult != null)
                //{
                //    var dsresult = objResult.resutlDS;

                //    foreach (DataRow dr in dsresult.Tables[1].Rows)
                //    {
                //        if (dsresult.Tables[0].Rows.Count > 0)
                //            dsresult.Tables[0].Rows.Add(dr.ItemArray);
                //    }

                //    double dbCrAmt = 0, dbDrAmt = 0;

                //    if (dsresult.Tables[0].Rows.Count > 0)
                //    {
                //        if (dsresult.Tables[0].Rows[0]["Debit"].ToString() == "" || dsresult.Tables[0].Rows[0]["Debit"].ToString() == "0" || dsresult.Tables[0].Rows[0]["Debit"] == null)
                //        {
                //            if (dsresult.Tables[0].Rows[0]["Credit"].ToString() == "" || dsresult.Tables[0].Rows[0]["Credit"] == null)
                //            {
                //            }
                //            else
                //            {
                //                dbCrAmt = Convert.ToInt32(dsresult.Tables[0].Rows[0]["Credit"]);
                //            }
                //            dbDrAmt = 0;
                //        }
                //        else
                //        {
                //            if (dsresult.Tables[0].Rows[0]["Debit"].ToString() == "" || dsresult.Tables[0].Rows[0]["Debit"] == null)
                //            {
                //            }
                //            else
                //            {
                //                dbDrAmt = Convert.ToDouble(dsresult.Tables[0].Rows[0]["Debit"]);
                //            }
                //            dbCrAmt = 0;
                //        }
                //    }

                //    if (dsresult.Tables[2].Rows.Count > 0)
                //    {
                //        if (dsresult.Tables[2].Rows[0][6].ToString() == "" || dsresult.Tables[2].Rows[0][6].ToString() == "0" || dsresult.Tables[2].Rows[0][6] == null)
                //            dbDrAmt += 0;
                //        else
                //            dbDrAmt += Convert.ToDouble(dsresult.Tables[2].Rows[0][6]);
                //        if (dsresult.Tables[2].Rows[0][7].ToString() == "" || dsresult.Tables[2].Rows[0][7].ToString() == "0" || dsresult.Tables[2].Rows[0][7] == null)
                //            dbCrAmt += 0;
                //        else
                //            dbCrAmt += Convert.ToDouble(dsresult.Tables[2].Rows[0][7]);
                //    }
                //    if (dsresult.Tables[1].Rows.Count > 0)
                //    {
                //        if (dbDrAmt > dbCrAmt)
                //        {
                //            //lblClosing.Text = "Cr. " + (dbDrAmt - dbCrAmt);
                //            // ReSharper disable once UnusedVariable
                //            foreach (DataRow row in dsresult.Tables[2].Rows)
                //            {
                //                dsresult.Tables[2].Rows[0][6] = 0;
                //                dsresult.Tables[2].Rows[0][7] = (dbDrAmt - dbCrAmt);
                //            }
                //        }
                //        else if (dbDrAmt < dbCrAmt)
                //        {
                //            //lblClosing.Text = "Dr. " + (dbCrAmt - dbDrAmt);
                //            // ReSharper disable once UnusedVariable
                //            foreach (DataRow row in dsresult.Tables[2].Rows)
                //            {
                //                dsresult.Tables[2].Rows[0][6] = (dbCrAmt - dbDrAmt);
                //                dsresult.Tables[2].Rows[0][7] = 0;
                //            }
                //        }
                //        else
                //        {
                //            //lblClosing.Text = "0";
                //            dsresult.Tables[2].Rows[0][6] = 0;
                //            dsresult.Tables[2].Rows[0][7] = 0;
                //        }
                //        btnExportExcel.Visible = true;
                //    }
                //    else
                //    {
                //        if (dsresult.Tables[0].Rows[0]["Debit"].ToString() == "" || dsresult.Tables[0].Rows[0]["Debit"].ToString() == "0" || dsresult.Tables[0].Rows[0]["Debit"] == null)
                //        {
                //            //lblClosing.Text = "Cr. " + dsresult.Tables[0].Rows[0]["Credit"];
                //            // ReSharper disable once UnusedVariable
                //            foreach (DataRow row in dsresult.Tables[2].Rows)
                //            {
                //                dsresult.Tables[2].Rows[0][6] = 0;
                //                dsresult.Tables[2].Rows[0][7] = dsresult.Tables[0].Rows[0]["Credit"];
                //            }
                //        }
                //        else
                //        {
                //            //lblClosing.Text = "Dr. " + dsresult.Tables[0].Rows[0]["Debit"];
                //            // ReSharper disable once UnusedVariable
                //            foreach (DataRow row in dsresult.Tables[2].Rows)
                //            {
                //                dsresult.Tables[2].Rows[0][6] = dsresult.Tables[0].Rows[0]["Debit"];
                //                dsresult.Tables[2].Rows[0][7] = 0;
                //            }
                //        }
                //        btnExportExcel.Visible = false;
                //    }

                //    foreach (DataRow dr in dsresult.Tables[2].Rows)
                //    {
                //        if (dsresult.Tables[0].Rows.Count > 0)
                //            dsresult.Tables[0].Rows.Add(dr.ItemArray);
                //    }

                ApplicationResult objResult = new ApplicationResult();
                GeneralLedgerBL objGeneralLedgerbl = new GeneralLedgerBL();
                string strChk = string.Empty;

                int intCheck = 0;
                if (chkNarration.Checked == true)
                    intCheck = 1;
                else
                    intCheck = 0;
                int intvalidate = 0;
                string strMainTable = string.Empty, strSubTable = string.Empty, strGeneralLedger = string.Empty;
                for (int i = 0; i < gvParameter.Rows.Count; i++)
                {
                   
                    CheckBox chkGeneralLedger = (CheckBox)gvParameter.Rows[i].FindControl("chk");
                    if (chkGeneralLedger.Checked == true)
                    {
                        objResult = objGeneralLedgerbl.GeneralLedgerReport(Convert.ToInt32(Session[ApplicationSession.TRUSTID].ToString()), Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString()), Convert.ToInt32(gvParameter.Rows[i].Cells[0].Text), txtFromDate.Text, txtToDate.Text, intCheck);
                        if (objResult != null)
                        {
                            strMainTable =
                                "<table style='vertical-align: top; width: 900px; font-family: Verdana; border: thin solid #000000; font-size: 11px; text-align: center;'>" +
                                "<tr>" +
                                "<td style='text-align: center; width: 100%;' colspan='6'>" +
                                "<b>Fertilizer Nagar School Trust<br/>" +
                                "General Ledger : " + gvParameter.Rows[i].Cells[2].Text + "<br/>" +
                                txtFromDate.Text + " to " + txtToDate.Text +
                                "</b>" +
                                "</td>" +
                                "</tr>" +
                                "<tr>" +
                                "<td style='text-align: center; width: 100%; height: 25px;' colspan='6'>" +
                                "</td>" +
                                "</tr>" +
                                "<tr>" +
                                "<td style='text-align: center; width: 10%; border: thin solid #000000;'>" +
                                "<b>Date</b>" +
                                "</td>" +
                                "<td style='text-align: center; width: 50%; border: thin solid #000000;'>" +
                                "<b>Particulars</b>" +
                                "</td>" +
                                "<td style='text-align: center; width: 10%; border: thin solid #000000;'>" +
                                "<b>Voucher Type</b>" +
                                "</td>" +
                                "<td style='text-align: center; width: 10%; border: thin solid #000000;'>" +
                                "<b>Voucher No</b>" +
                                "</td>" +
                                "<td style='text-align: center; width: 10%; border: thin solid #000000;'>" +
                                "<b>Debit</b>" +
                                "</td>" +
                                "<td style='text-align: center; width: 10%; border: thin solid #000000;'>" +
                                "<b>Credit</b>" +
                                "</td>" +
                                "</tr>";

                            for (int j = 0; j < objResult.resultDT.Rows.Count; j++)
                            {
                                string strDebit = objResult.resultDT.Rows[j]["Debit"].ToString();
                                if (strDebit == "0.00")
                                {
                                    strDebit = "";
                                }
                                string strCredit = objResult.resultDT.Rows[j]["Credit"].ToString();
                                if (strCredit == "0.00")
                                {
                                    strCredit = "";
                                }
                                strSubTable += "<tr>" +
                                               "<td style='text-align: left; width: 10%; border: thin solid #000000; height: 100%; vertical-align: top;'>" +
                                               objResult.resultDT.Rows[j]["OperationDate"].ToString() +
                                               "</td>" +
                                               "<td style='text-align: left; width: 50%; border: thin solid #000000; height: 100%; vertical-align: top;'>" +
                                               objResult.resultDT.Rows[j]["Particulars"].ToString() +
                                               "</td>" +
                                               "<td style='text-align: left; width: 10%; border: thin solid #000000; height: 100%; vertical-align: top;'>" +
                                               objResult.resultDT.Rows[j]["VoucherType"].ToString() +
                                               "</td>" +
                                               "<td style='text-align: left; width: 10%; border: thin solid #000000; height: 100%; vertical-align: top;'>" +
                                               objResult.resultDT.Rows[j]["VoucherNo"].ToString() +
                                               "</td>" +
                                               "<td style='text-align: right; width: 10%; border: thin solid #000000; height: 100%; vertical-align: top;'>" +
                                               strDebit +
                                               "</td>" +
                                               "<td style='text-align: right; width: 10%; border: thin solid #000000; height: 100%; vertical-align: top;'>" +
                                               strCredit +
                                               "</td>" +
                                               "</tr>";
                            }
                        }
                    }
                    else
                    {
                        intvalidate = intvalidate + 1;
                    }
                  
                    strGeneralLedger += strMainTable + strSubTable;

                    //break;
                    strMainTable = "";
                    strSubTable = "";
                }
                if (gvParameter.Rows.Count == intvalidate )
                {
                    ClientScript.RegisterStartupScript(typeof (Page), "MessagePopUp",
                        "<script>alert('Atleast select one Account Name.');</script>");

                }
                else
                {

                    Response.Clear();
                    Response.Buffer = true;

                    string Pattern = "[^a-zA-Z0-9 ]";
                    //Regex oRegex = new Regex(Pattern, RegexOptions.IgnoreCase);
                    //MatchCollection matches = oRegex.Matches(ddlProject.SelectedItem.ToString());
                    //string strFileName = oRegex.Replace(ddlProject.SelectedItem.ToString(), "");
                    //strFileName = System.Text.RegularExpressions.Regex.Replace(strFileName, @"\s+", " ");
                    //strFileName = strFileName.Replace(" ", "-");

                    Response.AddHeader("content-disposition",
                        "attachment;filename=General_Ledger_Report_" + "From_" + txtFromDate.Text + "_To_" +
                        txtToDate.Text + ".xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    string content = "<div align='center' style='font-family:verdana;font-size:13px'>" +
                                     strGeneralLedger + "</div>";
                    Response.Output.Write(content);
                    //style to format numbers to string
                    string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                    Response.Write(style);
                    Response.Flush();
                    Response.End();

                    //objResult = objGeneralLedgerbl.GeneralLedgerReport(Convert.ToInt32(Session[ApplicationSession.TRUSTID].ToString()), Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString()), strChk, txtFromDate.Text, txtToDate.Text, intCheck);
                    //gvGeneralLedgerReport.DataSource = objResult.resultDT;
                    //gvGeneralLedgerReport.DataBind();
                    PanelVisibility(2);

                    //var strName = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]) == 0 ? Session[ApplicationSession.TRUSTNAME].ToString() : Session[ApplicationSession.SCHOOLNAME].ToString();
                    //lblHeading.Text = "<b>General Ledgers: </b><br/>" + txtFromDate.Text + " To " + txtToDate.Text;
                    //}
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

                gvGeneralLedgerReport.AllowPaging = false;

                gvGeneralLedgerReport.RenderControl(hw);
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

        protected void gvGeneralLedgerReport_OnPreRender(object sender, EventArgs e)
        {
            if (gvGeneralLedgerReport.Rows.Count <= 0) return;
            gvGeneralLedgerReport.UseAccessibleHeader = true;
            gvGeneralLedgerReport.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void gvGeneralLedgerReport_OnRowDataBound(object sender, GridViewRowEventArgs e)
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

        #region User Define Function

        #region Clear Operation and Reset page
        public void Clear()
        {
            if (Master != null) _objControl.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            chkNarration.Checked = false;
            gvGeneralLedgerReport.DataSource = null;
            gvGeneralLedgerReport.DataBind();

            txtFromDate.Text = Session[ApplicationSession.ACCOUNTFROMDATE].ToString();
            txtToDate.Text = Session[ApplicationSession.ACCOUNTTODATE].ToString();

            ////var colCount = gvGeneralLedgerReport.Columns.Count;
            //////Leave column 0 -- our select and view template column
            ////while (colCount > 5)
            ////{
            ////    gvGeneralLedgerReport.Columns.RemoveAt(colCount - 1);
            ////    --colCount;
            ////}
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

        protected void gvGeneralLedgerReport_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        #endregion


    }
}