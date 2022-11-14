using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI; 
using System.Web.UI.WebControls;
using log4net;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.Common;
using System.Web.Script.Serialization;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using System.Drawing;

namespace GEIMS.ReportUI
{
    public partial class EmployeePayrollReport : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(EmployeePayrollReport));
        #region Pageload Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }
            if (!IsPostBack)
            {
                bindYear();
                divReport.Visible = false;
                divEmployee.Visible = true;
                btnPrintDetail.Visible = false;
                //hfTrustMID.Value = Session[ApplicationSession.TRUSTID].ToString();
                //hfSchoolMID.Value = Session[ApplicationSession.SCHOOLID].ToString();
            }

        }
        #endregion

        #region Bind Year
        public void bindYear()
        {
            string[] strYear;
            int intacYear = 0;
            #region Get Accounting Start Date
            ApplicationResult objResults = new ApplicationResult();
            TrustBL objTrustBl = new TrustBL();
           
            objResults = objTrustBl.Trust_Select(Convert.ToInt32(Session[ApplicationSession.TRUSTID].ToString()));

            if (objResults != null)
            {
                if (objResults.resultDT.Rows.Count > 0)
                {
                    string strACStartDate = objResults.resultDT.Rows[0][TrustBO.TRUST_ACCOUNTSTARTDATE].ToString();
                    strYear = strACStartDate.ToString().Split(new char[] { '/' });
                    intacYear = Convert.ToInt32(strYear[2]);
                }

            }
            #endregion
           

            for (int i = intacYear; i <= DateTime.Now.Year; i++)
            {
                ddlYear.Items.Add(i.ToString());
            }
            ddlYear.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-Select-", "-1"));
        }
        #endregion

        #region btnPrintDetail Click Event
        protected void btnPrintDetail_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('divSchool1');", true);
            lblTrust.Text = "Fertilizer Nagar English and Gujarati Medium School.";
            lblYear1.Text = ddlYear.SelectedItem.Text;
            lblMonth1.Text = ddlMonth.SelectedItem.Text;
            
        }
        #endregion

        #region VerifyRenderingInServerForm
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        #endregion

        #region BtnGo Event
        protected void btnGo_Click(object sender, EventArgs e)
        {
            if (ddlMonth.SelectedValue != "" && ddlYear.SelectedValue != "-1")
            {
                BindgvReport();
                //divReport.Visible = true;
                //divEmployee.Visible = false;
            }
            else
            {
                divReport.Visible = false;
                divEmployee.Visible = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Select Dropdowns.');", true);
            }

        }
    #endregion

        #region Bind GridView
        /// <summary>
        /// to show employee payroll report for selected month and year
        /// </summary>
        public void BindgvReport()
        {
            PaySlipBl objPaySlipBl = new PaySlipBl();
            ApplicationResult objResult = new ApplicationResult();

            //objResult = objPaySlipBl.Select_Employee_ForPaySlip_Report((Session[ApplicationSession.TRUSTID]).ToString(),
            //    ddlMonth.SelectedValue, ddlYear.SelectedValue, 1);
            objResult = objPaySlipBl.Select_Employee_ForPaySlip_Report((Session[ApplicationSession.TRUSTID]).ToString(), ddlMonth.SelectedValue, ddlYear.SelectedValue, 1, (Session[ApplicationSession.SCHOOLID]).ToString());
            if (objResult != null)
            {
                gvReport1.DataSource = null;
                gvReport.DataSource = null;
               
                if (objResult.resultDT.Rows.Count > 0)
                {
                    gvReport1.Visible = true;
                    gvReport.Visible = true;
                    divReport.Visible = true;
                    lblTrustName.Text = "Fertilizer Nagar English and Gujarati Medium School.";
                    lblYear.Text = ddlYear.SelectedItem.Text;
                    lblMonth.Text = ddlMonth.SelectedItem.Text;
                    DataRow newrow1 = objResult.resultDT.NewRow();
                    newrow1[1] = "Total";
                    objResult.resultDT.Rows.Add(newrow1);
                    int i = 0;
                    foreach (DataColumn col in objResult.resultDT.Columns)
                    {

                        if (i != 0 && i != 1 && i != 2 && i != 3)
                        {
                            object sumObject;
                            sumObject = objResult.resultDT.Compute("Sum([" + col.ColumnName + "])", "");
                            objResult.resultDT.Rows[objResult.resultDT.Rows.Count - 1][i] = sumObject;
                        }
                        i++;
                    }

                    gvReport1.DataSource = objResult.resultDT;
                    gvReport.DataSource = objResult.resultDT;
                    gvReport.DataBind();
                    gvReport1.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No record Found.');", true);
                   //Response.Redirect("../PayRoll/EmployeePayRollReport.aspx");
                    ClearAll();
                }
            }

      


        }
        #endregion

        #region btnBack Click Event
        protected void btnBack_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        #endregion

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            divReport.Visible = false;
             divEmployee .Visible = true;
             btnPrintDetail.Visible = false;
        }
        #endregion

        #region gvReport row bound Event
        protected void gvReport1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[3].Visible = false;
            
        }

      
        protected void gvReport_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[3].Visible = false;
        }
        #endregion

        #region BtnBackToreport Event
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Client.UI/SchoolReports.aspx?Mode=SchoolPayrollReports");
            //Response.Redirect("GeneratePaySlip.aspx");
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
                Response.AddHeader("content-disposition", "attachment;filename=EmployeePayroll" + Session[ApplicationSession.SCHOOLNAME].ToString().Replace(" ", "_").Replace("<br/>", "") + "_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvReport.AllowPaging = false;
                //gvReport.DataBind();
                gvReport.RenderControl(hw);

                // string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:13px;'>" + ReportTitle + "</span><br/><br/><span style='font-size:8px:font-weight:bold'>" + sw.ToString() + "</span></div>";
                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>" +
                    "Employee Payroll Report</span><br/>" +
                    "<span style='font-size:13px:font-weight:bold'></span><br/>" +
                    "<span align='center' style='font-family:verdana;font-size:11px'>" +
                   "<br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + 
                   "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Month:</strong>" + ddlMonth.SelectedItem.ToString() + "</span><br/>" +
                   "<span align='center' style='font-family:verdana;font-size:11px'><strong>Year:</strong>" + ddlYear.SelectedItem.ToString() + "</span><br/>" +
                   "</span>" + sw.ToString() + "<br/>";
                // Response.Output.Write(content);

                StringReader sr = new StringReader(content);
                Document pdfDoc = new Document(PageSize.A2.Rotate(), 10f, 10f, 10f, 0f);
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
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                string filename = "RPT_Tanker_Movement_Report_" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "_" + DateTime.Now.Date.ToString("HH:mm:ss") + ".xls";
                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    StringWriter sw1 = new StringWriter();
                    HtmlTextWriter hw1 = new HtmlTextWriter(sw1);

                    //To Export all pages
                    gvReport.AllowPaging = false;
                    //gvTotalQty.AllowPaging = false;
                    //   this.MilkStorageGrid();

                    gvReport.HeaderRow.BackColor = Color.White;
                    //gvTotalQty.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in gvReport.HeaderRow.Cells)
                    {
                        cell.BackColor = gvReport.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in gvReport.Rows)
                    {
                        row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = gvReport.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = gvReport.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                            List<Control> controls = new List<Control>();

                            //Add controls to be removed to Generic List
                            foreach (Control control in cell.Controls)
                            {
                                controls.Add(control);
                            }

                            //Loop through the controls to be removed and replace then with Literal
                            foreach (Control control in controls)
                            {
                                switch (control.GetType().Name)
                                {
                                    case "HyperLink":
                                        cell.Controls.Add(new Literal { Text = (control as HyperLink).Text });
                                        break;
                                    case "TextBox":
                                        cell.Controls.Add(new Literal { Text = (control as TextBox).Text });
                                        break;
                                    case "LinkButton":
                                        cell.Controls.Add(new Literal { Text = (control as LinkButton).Text });
                                        break;
                                    case "CheckBox":
                                        cell.Controls.Add(new Literal { Text = (control as CheckBox).Text });
                                        break;
                                    case "RadioButton":
                                        cell.Controls.Add(new Literal { Text = (control as RadioButton).Text });
                                        break;
                                }
                                cell.Controls.Remove(control);
                            }
                        }
                    }

                    gvReport.RenderControl(hw);

                    string strSubTitle = "Employee Monthly Report";
                    string strPath = Request.Url.GetLeftPart(UriPartial.Authority) + "/Images/Logo1.jpg";
                    string content = "<div align='center' style='font-family:verdana;font-size:16px'><img src='" + strPath + "'width='100' height='100'/>" +
                        "<span style='font-size:16px;font-weight:bold;color:Black;'>" + Session[ApplicationSession.TRUSTNAME] +
                        "</span><br/>" +
                           "<span align='center' style='font-family:verdana;font-size:13px'><strong>" + strSubTitle + "</strong></span><br/>" +
                           "<div align='center' style='font-family:verdana;font-size:12px'><strong>From Date :</strong>" + ddlMonth.SelectedValue.ToString() +
                           "&nbsp;&nbsp;&nbsp;&nbsp;<strong> To Date :</strong>" + ddlMonth.SelectedValue.ToString() +
                        //DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture) +
                        // "&nbsp;&nbsp;&nbsp;&nbsp;<strong> To Date :</strong>" +
                        // DateTime.ParseExact(txtToDate.Text, "d/M/yyyy", CultureInfo.InvariantCulture) +
                        "</div><br/> "
                        + sw.ToString() + "<br/> <br/> <br/>" + sw1.ToString() + "<br/></div>";
                    string style = @"<!--mce:2-->";
                    Response.Write(style);
                    Response.Output.Write(content);
                    gvReport.GridLines = GridLines.None;
                    //gvTotalQty.GridLines = GridLines.None;
                    Response.Flush();
                    Response.Clear();
                    Response.End();
                }
            }
            catch (Exception)
            {
            }
        }
        protected void btnExportExcel_Click1(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;

                Response.AddHeader("content-disposition", "attachment;filename=EmployeeMonthlyAttendance" + Session[ApplicationSession.SCHOOLNAME].ToString().Replace(" ", "_").Replace("<br/>", "") + "_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                gvReport.AllowPaging = false;
                // gvReport.DataBind();

                //Change the Header Row back to white color
                //gvReport.HeaderRow.Style.Add("background-color", "#67A3D1");
                gvReport.HeaderRow.Style.Add("ForeColor", "#000000");

                string imgPath1 = Request.Url.GetLeftPart(UriPartial.Authority) + "/Images/Logo1.jpg";
                gvReport.RenderControl(hw);
              
             
              

                string content = "<div class='row'><div class='col-md-2'>" +
                    "<img src='" + imgPath1 + "' width='100' height='100'/></div>" +
                    "<div class='col-md-10' align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>" +
                     "Employee Payroll Report</span><br/>" +
                     "<span style='font-size:13px:font-weight:bold'></span>" +
                     "<span align='center' style='font-family:verdana;font-size:11px'>" +
                    "<span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() +
                    "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Month:</strong>" + ddlMonth.SelectedItem.ToString() + "</span><br/>" +
                    "<span align='center' style='font-family:verdana;font-size:11px'><strong>Year:</strong>" + ddlYear.SelectedItem.ToString() + "</span><br/>" +
                    "</span></div></div>" + sw.ToString() + "<br/>";
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

                Response.AddHeader("content-disposition", "attachment;filename=EmployeeMonthlyAttendance" + Session[ApplicationSession.SCHOOLNAME].ToString().Replace(" ", "_").Replace("<br/>", "") + "_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".doc");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-word ";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                gvReport.AllowPaging = false;
                // gvReport.DataBind();
                gvReport.RenderControl(hw);


                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Employee Attendance Report</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + Session[ApplicationSession.SCHOOLNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Month:</strong>" + ddlMonth.SelectedItem.ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Year:</strong>" + ddlYear.SelectedItem.ToString() + "</span><br/></span><br/><div align='left' style='font-family:verdana;font-size:11px'> " + "Note *:  'A' Considered as <strong>Absent</strong><br/>Note *:  'P' Considered as <strong>Present</strong><br/>Note *:  '+' Considered as <strong>Half Day</strong><br/>Note *:  '*' Considered as <strong>Holiday</strong><br/>Note *:  '-' Considered as <strong>Week End</strong><br/>Note *:  'CL/ML/DL..etc.' Considered as <strong>Approved Leave</strong><br/>Note *:  '+(CL),+(ML),+(DL)..etc.' Considered as <strong>Approved Halfday Leave</strong>" + "</div><br/>" + sw.ToString() + "<br/>";
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
    }
}