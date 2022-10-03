using System;
using System.IO;
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;


namespace GEIMS.ReportUI
{
    public partial class MaterialReturn : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(MaterialReturn));

        #region PageLoad Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }
            if (!IsPostBack)
            {
                divReport.Visible = false;
            }
        }
        #endregion

        #region VerifyRenderingInServerForm
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        #endregion

        #region Go button Click Event
        protected void btnGo_Click(object sender, EventArgs e)
        {
            BindConsumptionsGrid(txtConsumptionFromDate.Text, txtConsumptionTodate.Text, 1);
        }
        #endregion

        #region Exportpdf button Click Event
        protected void btnExportPDF_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename= MaterialReturnList"+ "_" + DateTime.Now.Date.ToString("yyyy-mm-dd") +".pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvReport.AllowPaging = false;
                //gvReport.DataBind();
                gvReport.RenderControl(hw);

                // string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:13px;'>" + ReportTitle + "</span><br/><br/><span style='font-size:8px:font-weight:bold'>" + sw.ToString() + "</span></div>";
                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Report : Material Return List</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToString("dd/MM/yyyy") + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + txtConsumptionFromDate.Text + " To " + txtConsumptionTodate.Text + "</span><br/><br/>" + sw.ToString() + "<br/>";
                // Response.Output.Write(content);

                StringReader sr = new StringReader(content);
                Document pdfDoc = new Document(PageSize.A3, 10f, 10f, 10f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                Response.Write(pdfDoc);
                //	HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.End();


            }
            catch (System.Threading.ThreadAbortException lException)
            {
                // logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion

        #region ExportExcel button Click Event
        protected void btnExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;

                Response.AddHeader("content-disposition", "attachment;filename=MaterialReturnList" + "_" + DateTime.Now.Date.ToString("yyyy-mm-dd") +".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                gvReport.AllowPaging = false;
                // gvReport.DataBind();

                //Change the Header Row back to white color
                //gvReport.HeaderRow.Style.Add("background-color", "#67A3D1");
                gvReport.HeaderRow.Style.Add("ForeColor", "#000000");


                gvReport.RenderControl(hw);

                // string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:13px:font-weight:bold'>" + ReportTitle + "</span><br/><br/>" + sw.ToString() + "</div>";
                //string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>" + lblTitle.Text + "</span><br/><br/><span style='font-size:13px:font-weight:bold'>" + ReportTitle + "</span><br/><br/><div align='right' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + Date + "</div><br/>" + sw.ToString() + "<br/><br/><br/><div align='left'><span style='font-size:11px:font-weight:bold:padding-left:2px'>" + lblText.Text + "</span></div>";
                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Report : Material Return List</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToString("dd/MM/yyyy") + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + txtConsumptionFromDate.Text + " To " + txtConsumptionTodate.Text + "</span><br/><br/>" + sw.ToString() + "<br/>";
                Response.Output.Write(content);
                //style to format numbers to string
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                // Response.Output.Write(sw.ToString());
                Response.Flush();
                //HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.End();
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion

        #region ExportWord button Click Event
        protected void btnExportWord_Click(object sender, ImageClickEventArgs e)
        {

            try
            {

                Response.AddHeader("content-disposition", "attachment;filenameMaterialReturnList" + "_" + DateTime.Now.Date.ToString("yyyy-mm-dd") +".doc");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-word ";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                gvReport.AllowPaging = false;
                // gvReport.DataBind();
                gvReport.RenderControl(hw);


                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Report : Material Return List</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToString("dd/MM/yyyy") + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + txtConsumptionFromDate.Text + " To " + txtConsumptionTodate.Text + "</span><br/><br/>" + sw.ToString() + "<br/>";
                Response.Output.Write(content);

                Response.Flush();
                //	HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.End();
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion

        #region Back Button Click
        protected void btnBack_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        #endregion

        #region Bind Consumption DataList
        public void BindConsumptionsGrid(string strFromDate, string strToDate, int intTypeID)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                ConsumptionBL objConsumptionBl = new ConsumptionBL();

                objResult = objConsumptionBl.Consumption_SelectForReport(strFromDate, strToDate, Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), intTypeID);
                if (objResult != null)
                {
                    gvReport.DataSource = objResult.resultDT;
                    gvReport.DataBind();

                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        divReport.Visible = true;
                        pnlConsumptionInfo.Visible = false;
                    }
                    else
                    {
                        divReport.Visible = false;
                        pnlConsumptionInfo.Visible = true;
                        ClearAll();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Records Found.');", true);
                    }
                }
                lblFromDate.Text = txtConsumptionFromDate.Text;
                lblToDate.Text = txtConsumptionTodate.Text;

                lblTrust.Text = Session[ApplicationSession.TRUSTNAME].ToString();
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            divReport.Visible = false;
            pnlConsumptionInfo.Visible = true;
        }
        #endregion

        #region Report Button Event
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Client.UI/TrustReports.aspx?Mode=InventoryReports");
        }
        #endregion
    }
}