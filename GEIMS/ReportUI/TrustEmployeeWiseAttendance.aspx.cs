using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using log4net;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.Common;
namespace GEIMS.ReportUI
{
    public partial class TrustEmployeeWiseAttendance : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(StudentAttendenceReport));

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }
            if (!IsPostBack)
            {
                divReport.Visible = false;
                btnPrintDetail.Visible = false;
                btnBack.Visible = false;
                //divButtons.Visible = false;
                divLabels.Visible = false;
                ddlSearchBy.SelectedIndex = 1;
                ddlabsentHalfday.SelectedIndex = -1;
            }
        }
        #endregion

        #region Bind Employee Attendance Grid
        public void BindEmployeeAttendance()
        {
            ApplicationResult objResult = new ApplicationResult();
            EmployeeattendanceBl objEmployeeattendanceBl = new EmployeeattendanceBl();

            objResult = objEmployeeattendanceBl.Employeeattendance_Select_ForEmployeeWiseAttendance_Trust(Convert.ToInt32(hfSearchName.Value), txtFromDate.Text, txtToDate.Text, Convert.ToInt32(ddlabsentHalfday.SelectedValue));
            if (objResult.resultDT.Rows.Count > 0)
            {
                gvReport.DataSource = objResult.resultDT;
                gvReport.DataBind();
                gvAttendance.DataSource = objResult.resultDT;
                gvAttendance.DataBind();

                divReport.Visible = true;
                //divButtons.Visible = true;
                divLabels.Visible = true;
                btnBack.Visible = true;
                btnPrintDetail.Visible = true;
                pnlStudentAttendanceInfo.Visible = false;
                lblEmployeeName.Text = txtSearchName.Text;
                lblFromDate.Text = txtFromDate.Text;
                lblToDate.Text = txtToDate.Text;
            }
            else
            {
                divReport.Visible = false;
                btnPrintDetail.Visible = false;
                pnlStudentAttendanceInfo.Visible = true;
                btnBack.Visible = false;
                //divButtons.Visible = true;
                divLabels.Visible = false;
                ClearAll();
                ddlSearchBy.SelectedIndex = 1;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Records Found.');", true);
            }
        }
        #endregion

        #region Get Employee Web Service Method
        [System.Web.Services.WebMethod]
        public static string[] GetAllEmployeeNameForReport(string prefixText, int TrustMID, int SchoolMID)
        {
            EmployeeMBL objEmployeeMbl = new EmployeeMBL();
            ApplicationResult objResult = new ApplicationResult();
            string strSearchText = prefixText + "%";
            List<string> result = new List<string>();
            objResult = objEmployeeMbl.EmployeeM_Select_ForAutoComplete(strSearchText, TrustMID, SchoolMID);
            if (objResult != null)
            {
                for (int i = 0; i < objResult.resultDT.Rows.Count; i++)
                {
                    string strEmployeeCodeName = objResult.resultDT.Rows[i]["EmployeeName"].ToString();
                    string strEmployeeMID = objResult.resultDT.Rows[i][EmployeeMBO.EMPLOYEEM_EMPLOYEEMID].ToString();
                    result.Add(string.Format("{0}~{1}", strEmployeeCodeName, strEmployeeMID));
                }
            }
            return result.ToArray();
        }

        #endregion

        #region btnBack Clicl Event
        protected void btnBack_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        #endregion

        #region btnGo Click Event
        protected void btnGo_Click(object sender, EventArgs e)
        {
            DateTime fromdate = Convert.ToDateTime(txtFromDate.Text.Trim());
            DateTime todate1 = Convert.ToDateTime(txtToDate.Text.Trim());
            if (fromdate <= todate1)
            {
                BindEmployeeAttendance();
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! Wrong Date selection : To Date Must be greter then From Date .');</script>");
                txtFromDate.Text = string.Empty;
                txtToDate.Text = string.Empty;
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
                Response.AddHeader("content-disposition", "attachment;filename=Employee Wise Attendance" + Session[ApplicationSession.TRUSTNAME].ToString().Replace(" ", "_").Replace("<br/>", "") + "_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvReport.AllowPaging = false;
                //gvReport.DataBind();
                gvReport.RenderControl(hw);

                // string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:13px;'>" + ReportTitle + "</span><br/><br/><span style='font-size:8px:font-weight:bold'>" + sw.ToString() + "</span></div>";
                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Employee Wise Attendance Report</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Employee Name:</strong>" + txtSearchName.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>From Date:</strong>" + txtFromDate.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>To Date:</strong>" + txtToDate.Text + "</span><br/></span><br/><div align='left' style='font-family:verdana;font-size:11px'> " + "Note *:  'A' Considered as <strong>Absent</strong><br/>Note *:  'P' Considered as <strong>Present</strong><br/>Note *:  '+' Considered as <strong>Half Day</strong><br/>Note *:  '*' Considered as <strong>Holiday</strong><br/>Note *:  '-' Considered as <strong>Week End</strong><br/>Note *:  'CL/ML/DL..etc.' Considered as <strong>Approved Leave</strong><br/>Note *: '+(CL),+(ML),+(DL)..etc.' Considered as <strong>Approved Half day Leave</strong> " + "</div><br/>" + " </div><br/>" + sw.ToString() + "<br/>";
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

                Response.AddHeader("content-disposition", "attachment;filename=EmployeeMonthlyAttendance" + Session[ApplicationSession.TRUSTNAME].ToString().Replace(" ", "_").Replace("<br/>", "") + "_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
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
                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Employee Attendance Report</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Employee Name:</strong>" + txtSearchName.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>From Date:</strong>" + txtFromDate.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>To Date:</strong>" + txtToDate.Text + "</span><br/></span><br/><div align='left' style='font-family:verdana;font-size:11px'> " + "Note *:  'A' Considered as <strong>Absent</strong><br/>Note *:  'P' Considered as <strong>Present</strong><br/>Note *:  '+' Considered as <strong>Half Day</strong><br/>Note *:  '*' Considered as <strong>Holiday</strong><br/>Note *:  '-' Considered as <strong>Week End</strong><br/>Note *:  'CL/ML/DL..etc.' Considered as <strong>Approved Leave</strong><br/>Note *: '+(CL),+(ML),+(DL)..etc.' Considered as <strong>Approved Half day Leave</strong> " + "</div><br/>" + " </div>" + sw.ToString() + "<br/>";
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

                Response.AddHeader("content-disposition", "attachment;filename=EmployeeMonthlyAttendance" + Session[ApplicationSession.TRUSTNAME].ToString().Replace(" ", "_").Replace("<br/>", "") + "_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".doc");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-word ";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                gvReport.AllowPaging = false;
                // gvReport.DataBind();
                gvReport.RenderControl(hw);


                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Employee Attendance Report</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Employee Name:</strong>" + txtSearchName.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>From Date:</strong>" + txtFromDate.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>To Date:</strong>" + txtToDate.Text + "</span><br/></span><br/><div align='left' style='font-family:verdana;font-size:11px'> " + "Note *:  'A' Considered as <strong>Absent</strong><br/>Note *:  'P' Considered as <strong>Present</strong><br/>Note *:  '+' Considered as <strong>Half Day</strong><br/>Note *:  '*' Considered as <strong>Holiday</strong><br/>Note *:  '-' Considered as <strong>Week End</strong><br/>Note *:  'CL/ML/DL..etc.' Considered as <strong>Approved Leave</strong><br/>Note *: '+(CL),+(ML),+(DL)..etc.' Considered as <strong>Approved Half day Leave</strong> " + "</div><br/>" + " </div><br/>" + sw.ToString() + "<br/>";
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

        #region btnPrintDetail Click Event
        protected void btnPrintDetail_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('divSchool1');", true);
        }
        #endregion

        #region VerifyRenderingInServerForm
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        #endregion

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            divReport.Visible = false;
            pnlStudentAttendanceInfo.Visible = true;
            //divButtons.Visible = false;
            divLabels.Visible = false;
        }
        #endregion

        #region Report Button Event
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Client.UI/TrustReports.aspx?Mode=GeneralReports");
        }
        #endregion
    }
}