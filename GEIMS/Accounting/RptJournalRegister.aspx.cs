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

namespace GEIMS.Accounting
{
    public partial class RptJournalRegister : PageBase
    {
        #region declaration
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Payment));
        readonly Controls _objControl = new Controls();
        private string _reportTitle, _date;
        int intNarration = 0;
        decimal TotalDebit, TotalCredit;
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

        #region Bind Report
        public void BindReport()
        {
            CheckNarration();
            //ApplicationResult objResult = new ApplicationResult();

            var objJournalRegisterBl = new JournalVoucherMBL();
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
                //gvJournalRegisterReport.Columns.Add(bfield);
            }
            else
                intIsNarration = 0;

            var objResult = objJournalRegisterBl.JournalRegisterReport(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), txtFromDate.Text, txtToDate.Text, intIsNarration);

            if (objResult.resultDT.Rows.Count > 0)
            {
                gvJournalRegisterReport.DataSource = objResult.resultDT;
                gvJournalRegisterReport.DataBind();
                lblHeading.Text = "<b>Journal Register </b><br/>" + txtFromDate.Text + " To " + txtToDate.Text;

            }

        }
        #endregion

        #region  Check Narration
        public void CheckNarration()
        {
            if (chkNarration.Checked == true)
            {
                intNarration = 1;
            }
            else
            {
                intNarration = 0;
            }
        }
        #endregion

        #region Button Click Events

        #region Button Get Report
        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                BindReport();
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

                Response.AddHeader("content-disposition", "attachment;filename=" + lblHeading.Text.Replace(" ", "_").Replace("<br/>", "").Replace("<b>", "").Replace("</b>", "") + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                var sw = new StringWriter();
                var hw = new HtmlTextWriter(sw);

                gvJournalRegisterReport.AllowPaging = false;

                gvJournalRegisterReport.RenderControl(hw);
                _reportTitle = lblHeading.Text;
                _date = DateTime.UtcNow.AddHours(5.5).ToShortDateString();
                var content = "<div align='center' style='font-family:verdana;font-size:13px;text-align:center;color: #000000;'><span style='font-size:16px;text-align:center;font-weight:bold;color:Maroon'>Fertilizer Nagar School Trust</span><br/><span style='font-size:13px:font-weight:bold;text-align:center;'>" + _reportTitle + "</span><br/><div align='right' style='font-family:verdana;font-size:11px;text-align:right;'><strong>Date : </strong>" + _date + "</div><br/>" + sw;
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

        protected void gvJournalRegisterReport_OnPreRender(object sender, EventArgs e)
        {
            if (gvJournalRegisterReport.Rows.Count <= 0) return;
            gvJournalRegisterReport.UseAccessibleHeader = true;
            gvJournalRegisterReport.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        #endregion

        #region User Define Function

        #region Clear Operation and Reset page
        public void Clear()
        {
            if (Master != null) _objControl.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            chkNarration.Checked = false;
            gvJournalRegisterReport.DataSource = null;
            gvJournalRegisterReport.DataBind();
            txtFromDate.Text = Session[ApplicationSession.ACCOUNTFROMDATE].ToString();
            txtToDate.Text = Session[ApplicationSession.ACCOUNTTODATE].ToString();

            //var colCount = gvJournalRegisterReport.Columns.Count;
            ////Leave column 0 -- our select and view template column
            //while (colCount > 5)
            //{
            //    gvJournalRegisterReport.Columns.RemoveAt(colCount - 1);
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

        #region gridview rowdata bound event
        protected void gvJournalRegisterReport_RowDataBound(object sender, GridViewRowEventArgs e)
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


        #endregion
    }
}