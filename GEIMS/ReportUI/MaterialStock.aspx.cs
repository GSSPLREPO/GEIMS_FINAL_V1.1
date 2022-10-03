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

    public partial class MaterialStock : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(MaterialStock));
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
                divSchool.Visible = false;
                divTrust.Visible = false;
                divGO.Visible = false;
                
            }
        }
        #endregion

        #region Bind School Name
        public void getSchoolName()
        {
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            SchoolBL objSchoolBL = new SchoolBL();

            objResult = objSchoolBL.School_SelectAll_ForDropDOwn(Convert.ToInt32(ddlTrust.SelectedValue));
            if (objResult != null)
            {
                objControls.BindDropDown_ListBox(objResult.resultDT, ddlSchool, "SchoolNameEng", "SchoolMID");
                if (objResult.resultDT.Rows.Count > 0)
                {
                }
                ddlSchool.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "-1"));
            }
        }
        #endregion

        #region Bind Trust Name
        public void getTrustName()
        {
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            TrustBL objTrustBL = new TrustBL();
           
            objResult = objTrustBL.Trust_SelectAll_ForDropDown();
            if (objResult != null)
            {
                objControls.BindDropDown_ListBox(objResult.resultDT, ddlTrust, "TrustNameEng", "TrustMID");
                if (objResult.resultDT.Rows.Count > 0)
                {
                }
                ddlTrust.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "-1"));
                ddlSchool.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "-1"));
            }
        }
        #endregion


        #region rblSelect Index Change
        protected void rblSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            getTrustName();
            getSchoolName();
           
            if (rblSelect.SelectedValue == "0")
            {
                divTrust.Visible = true;
                divSchool.Visible = false;
                divDisplaySchool.Visible = false;
            }
            else
            {
                divTrust.Visible = true;
                divSchool.Visible = true;
                divDisplaySchool.Visible = true;
            }
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
                Response.AddHeader("content-disposition", "attachment;filename= MaterialStockList" + "_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvReport.AllowPaging = false;
                //gvReport.DataBind();
                gvReport.RenderControl(hw);
                string content;
                // string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:13px;'>" + ReportTitle + "</span><br/><br/><span style='font-size:8px:font-weight:bold'>" + sw.ToString() + "</span></div>";
                if (rblSelect.SelectedValue == "0")
                {
                     content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Report : Material Stock List</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToString("dd/MM/yyyy") + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><br/>" + sw.ToString() + "<br/>";
                }
                else
                {
                     content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Report : Material Stock List</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToString("dd/MM/yyyy") + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + ddlSchool.SelectedItem.Text + "</span><br/><br/>" + sw.ToString() + "<br/>";
                }
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

                Response.AddHeader("content-disposition", "attachment;filename= MaterialStockList" + "_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
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
                string content;
                // string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:13px;'>" + ReportTitle + "</span><br/><br/><span style='font-size:8px:font-weight:bold'>" + sw.ToString() + "</span></div>";
                if (rblSelect.SelectedValue == "0")
                {
                    content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Report : Material Stock List</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToString("dd/MM/yyyy") + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><br/>" + sw.ToString() + "<br/>";
                }
                else
                {
                    content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Report : Material Stock List</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToString("dd/MM/yyyy") + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + ddlSchool.SelectedItem.Text + "</span><br/><br/>" + sw.ToString() + "<br/>";
                }
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

                Response.AddHeader("content-disposition", "attachment;filename= MaterialStockList" + "_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".doc");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-word ";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                gvReport.AllowPaging = false;
                // gvReport.DataBind();
                gvReport.RenderControl(hw);


                string content;
                // string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:13px;'>" + ReportTitle + "</span><br/><br/><span style='font-size:8px:font-weight:bold'>" + sw.ToString() + "</span></div>";
                if (rblSelect.SelectedValue == "0")
                {
                    content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Report : Material Stock List</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToString("dd/MM/yyyy") + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><br/>" + sw.ToString() + "<br/>";
                }
                else
                {
                    content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Report : Material Stock List</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToString("dd/MM/yyyy") + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + ddlSchool.SelectedItem.Text + "</span><br/><br/>" + sw.ToString() + "<br/>";
                }
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

        #region Bind Employee DataList
        public void BindStockGrid()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                MaterialStockTBL objStockBl = new MaterialStockTBL();

                objResult = objStockBl.Material_StockReport(Convert.ToInt32(ddlTrust.SelectedValue), Convert.ToInt32(ddlSchool.SelectedValue));
                if (objResult != null)
                {
                    gvReport.Visible = true;
                    gvReport.DataSource = objResult.resultDT;
                    gvReport.DataBind();

                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        pnlStockInfo.Visible = false;
                        divReport.Visible = true;
                    }
                    else
                    {
                        pnlStockInfo.Visible = true;
                        divReport.Visible = false;
                        ClearAll();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Records Found.');", true);
                    }
                }
               

                lblTrust.Text = Session[ApplicationSession.TRUSTNAME].ToString();
                lblSchool.Text = ddlSchool.SelectedItem.Text;
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
            pnlStockInfo.Visible = true;
        }
        #endregion

        #region btnGo CLick Event
        protected void btnGo_Click(object sender, EventArgs e)
        {
            if (ddlTrust.SelectedValue != "-1")
            {
                BindStockGrid();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "alert('Please select All Parameters.');", true);
            }
        }
        #endregion

        #region Trust Dropdown Selected CHange Event
        protected void ddlTrust_SelectedIndexChanged(object sender, EventArgs e)
        {

            divGO.Visible = true;
            getSchoolName();
        }
        #endregion

        #region VerifyRenderingInServerForm
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        #endregion


        #region Report Button Event
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Client.UI/TrustReports.aspx?Mode=InventoryReports");
        }
        #endregion

        #region Material Stock GridView RowDataBound event
        protected void gvReport_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
        }
        #endregion
    }
}