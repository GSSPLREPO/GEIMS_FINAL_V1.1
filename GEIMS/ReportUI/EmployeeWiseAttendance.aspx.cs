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
using System.Drawing;

namespace GEIMS.ReportUI
{
    public partial class EmployeeWiseAttendance : System.Web.UI.Page
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
                ddlSearchBy.SelectedIndex = 1;
                ddlabsentHalfday.SelectedIndex = -1;
                divReport.Visible = false;
                divLabels.Visible = false;
                btnPrintDetail.Visible = false;
                btnBack.Visible = false;
               // divButtons.Visible = false;
            }
        }
        #endregion

        #region Bind Employee Attendance Grid
        public void BindEmployeeAttendance()
        {
            ApplicationResult objResult = new ApplicationResult();
            EmployeeattendanceBl objEmployeeattendanceBl = new EmployeeattendanceBl();

            objResult = objEmployeeattendanceBl.Employeeattendance_Select_ForEmployeeWiseAttendance(Convert.ToInt32(hfSearchName.Value),txtFromDate.Text, txtToDate.Text,Convert.ToInt32(ddlabsentHalfday.SelectedValue));
            if (objResult.resultDT.Rows.Count > 0)
            {
                gvReport.DataSource = objResult.resultDT;
                gvReport.DataBind();
                gvAttendance.DataSource = objResult.resultDT;
                gvAttendance.DataBind();

                divReport.Visible = true;
                divLabels.Visible = true;
                //divButtons.Visible = true;
                btnBack.Visible = true;
                btnPrintDetail.Visible = true;
                pnlStudentAttendanceInfo.Visible = false;
                lblSchoolName.Text = Session[ApplicationSession.SCHOOLNAME].ToString();
                lblEmployeeName.Text = txtSearchName.Text;
                lblFromDate.Text = txtFromDate.Text;
                lblToDate.Text = txtToDate.Text;
                lblSchool1.Text = Session[ApplicationSession.SCHOOLNAME].ToString();
            }
            else
            {
                divReport.Visible = false;
                divLabels.Visible = false;
                btnPrintDetail.Visible = false;
                pnlStudentAttendanceInfo.Visible = true;
                btnBack.Visible = false;
               // divButtons.Visible = true;
                ClearAll();
                ddlSearchBy.SelectedIndex = 1;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Records Found.');", true);
            }
        }
        #endregion

        //public void bindGridFooterTotal()
        //{ 
        //    int sumP = 0;
        //    int sumA = 0;
        //    int sumHD = 0;
        //    int sumH = 0;
        //    int sumW = 0;


        //    {
        //    for (int i = 0; i < (gvReport.Rows.Count - 1); i++)
        //    {
        //        if (gvReport.Rows[i].Cells["Status"].Value.ToString() == "P")
        //        {
        //            sumP += 1;
        //        }
        //         if (gvReport.Rows[i].Cells["Status"].Value.ToString() == "P")
        //        {
        //            sumP += 1;
        //        }
        //    }
        //    lblPresent.Text = sumP.ToString();
        //}
        //}

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

        #region btnBack Click Event
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
                int count = 0;
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                string filename = "Employee Payroll Report (Monthly)" + DateTime.Now.ToString("dd/MM/yyyy") + "_" + DateTime.Now.ToString("HH:mm:ss") + ".xls";


                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    StringWriter sw1 = new StringWriter();
                    HtmlTextWriter hw1 = new HtmlTextWriter(sw1);

                    //To Export all pages
                    gvReport.AllowPaging = false;

                    gvReport.HeaderRow.BackColor = Color.White;

                    foreach (TableCell cell in gvReport.HeaderRow.Cells)
                    {
                        cell.BackColor = gvReport.HeaderStyle.BackColor;
                        count++;
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
                            cell.HorizontalAlign = HorizontalAlign.Center;
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
                    int colh, cold;
                    colh = count - 4;
                    cold = count - 8;

                    gvReport.RenderControl(hw);


                    string strSubTitle = "Report Name :Employee Wise Attendance Report  ";
                    string strPath = Request.Url.GetLeftPart(UriPartial.Authority) + "/images/Logo1.jpg";

                    //    string content = "<div align='center' style='font-family:verdana;font-size:16px; width:800px;'>" +
                    //  "<table style='display: table; width: 800px; clear:both;'>" +
                    //  "<tr> </tr>" +
                    //  "<tr><th></th><th><img height='100' width='100' src='" + strPath + "'/></th>" +
                    //  "<th colspan='" + colh + "' style='width: 600px; float: left; font-weight:bold;font-size:16px;color:Maroon;'>" + strSubTitle + "</tr>" +
                    //     "<tr><th colspan='2'></th><th colspan='" + colh + "' style='font-size:13px;font-weight:bold;color:Black;'>From Date :" + lblFromDate.Text
                    //     + "&nbsp;&nbsp; To Date :" + lblToDate.Text + "</th></tr>" +
                    //     "<tr><th colspan='2'></th><th colspan='" + colh + "'></th></tr>" +
                    //     "<tr><th colspan='2'></th><th colspan='" + colh + "' style='font-size:13px;'><b>Trust Name :" + Session[ApplicationSession.TRUSTNAME] + "</b></th></tr>" +
                    //     "<tr></tr>" +
                    //"</table>" +

                    string content = "<div align='center' style='font-family:verdana;font-size:13px'>" +
                        "<span style='font-size:16px:font-weight:bold;color:Maroon;'>" +
                        "Employee Wise Attendance Report</span><br/>" +
                        "<span style='font-size:13px:font-weight:bold'></span><br/>" +
                        "<span align='center' style='font-family:verdana;font-size:11px'>" +
                        "<strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + Session[ApplicationSession.SCHOOLNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Employee Name:</strong>" + txtSearchName.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>From Date:</strong>" + txtFromDate.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>To Date:</strong>" + txtToDate.Text + "</span><br/></span><br/><div align='left' style='font-family:verdana;font-size:11px'> " + "Note *:  'A' Considered as <strong>Absent</strong><br/>Note *:  'P' Considered as <strong>Present</strong><br/>Note *:  '+' Considered as <strong>Half Day</strong><br/>Note *:  '*' Considered as <strong>Holiday</strong><br/>Note *:  '-' Considered as <strong>Week End</strong><br/>Note *:  'CL/ML/DL..etc.' Considered as <strong>Approved Leave</strong><br/>Note *: '+(CL),+(ML),+(DL)..etc.' Considered as <strong>Approved Half day Leave</strong> " +
                        "</div><br/>" + sw.ToString() + "<br/>";

                    //"<br/>" + sw.ToString() + "<br/></div>";

                    string style = @"<!--mce:2-->";
                    Response.Write(style);
                    Response.Output.Write(content);
                    gvReport.GridLines = GridLines.None;
                    Response.Flush();
                    Response.Clear();
                    Response.End();
                }
            }

            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        
        //try
        //{
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("content-disposition", "attachment;filename=Employee Wise Attendance" + Session[ApplicationSession.SCHOOLNAME].ToString().Replace(" ", "_").Replace("<br/>", "") + "_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".pdf");
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter hw = new HtmlTextWriter(sw);
        //    gvReport.AllowPaging = false;
        //    //gvReport.DataBind();
        //    gvReport.RenderControl(hw);

        //    // string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:13px;'>" + ReportTitle + "</span><br/><br/><span style='font-size:8px:font-weight:bold'>" + sw.ToString() + "</span></div>";
        //    string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Employee Wise Attendance Report</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + Session[ApplicationSession.SCHOOLNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Employee Name:</strong>" + txtSearchName.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>From Date:</strong>" + txtFromDate.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>To Date:</strong>" + txtToDate.Text + "</span><br/></span><br/><div align='left' style='font-family:verdana;font-size:11px'> " + "Note *:  'A' Considered as <strong>Absent</strong><br/>Note *:  'P' Considered as <strong>Present</strong><br/>Note *:  '+' Considered as <strong>Half Day</strong><br/>Note *:  '*' Considered as <strong>Holiday</strong><br/>Note *:  '-' Considered as <strong>Week End</strong><br/>Note *:  'CL/ML/DL..etc.' Considered as <strong>Approved Leave</strong><br/>Note *: '+(CL),+(ML),+(DL)..etc.' Considered as <strong>Approved Half day Leave</strong> " + "</div><br/>" + sw.ToString() + "<br/>";
        //    // Response.Output.Write(content);

        //    StringReader sr = new StringReader(content);
        //    Document pdfDoc = new Document(PageSize.A3, 10f, 10f, 10f, 0f);
        //    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //    pdfDoc.Open();
        //    htmlparser.Parse(sr);
        //    pdfDoc.Close();
        //    Response.Write(pdfDoc);
        //    //	HttpContext.Current.ApplicationInstance.CompleteRequest();
        //    Response.End();


        //}
        //catch (System.Threading.ThreadAbortException lException)
        //{
        //    // logger.Error("Error", ex);
        //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
        //}
    }
        #endregion

        #region ExportExcel button Click Event
        protected void btnExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;

                Response.AddHeader("content-disposition", "attachment;filename=Employee Wise Attendance" + Session[ApplicationSession.SCHOOLNAME].ToString().Replace(" ", "_").Replace("<br/>", "") + "_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
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
                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Employee Attendance Report</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + Session[ApplicationSession.SCHOOLNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Employee Name:</strong>" + txtSearchName.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>From Date:</strong>" + txtFromDate.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>To Date:</strong>" + txtToDate.Text + "</span><br/></span><br/><div align='left' style='font-family:verdana;font-size:11px'> " + "Note *:  'A' Considered as <strong>Absent</strong><br/>Note *:  'P' Considered as <strong>Present</strong><br/>Note *:  '+' Considered as <strong>Half Day</strong><br/>Note *:  '*' Considered as <strong>Holiday</strong><br/>Note *:  '-' Considered as <strong>Week End</strong><br/>Note *:  'CL/ML/DL..etc.' Considered as <strong>Approved Leave</strong><br/>Note *: '+(CL),+(ML),+(DL)..etc.' Considered as <strong>Approved Half day Leave</strong><br/>" + "</div>" + sw.ToString() + "<br/>";
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

                Response.AddHeader("content-disposition", "attachment;filename=Employee Wise Attendance" + Session[ApplicationSession.SCHOOLNAME].ToString().Replace(" ", "_").Replace("<br/>", "") + "_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".doc");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-word ";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                gvReport.AllowPaging = false;
                // gvReport.DataBind();
                gvReport.RenderControl(hw);


                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Employee Attendance Report</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + Session[ApplicationSession.SCHOOLNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Employee Name:</strong>" + txtSearchName.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>From Date:</strong>" + txtFromDate.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>To Date:</strong>" + txtToDate.Text + "</span><br/></span><br/><div align='left' style='font-family:verdana;font-size:11px'> " + "Note *:  'A' Considered as <strong>Absent</strong><br/>Note *:  'P' Considered as <strong>Present</strong><br/>Note *:  '+' Considered as <strong>Half Day</strong><br/>Note *:  '*' Considered as <strong>Holiday</strong><br/>Note *:  '-' Considered as <strong>Week End</strong><br/>Note *:  'CL/ML/DL..etc.' Considered as <strong>Approved Leave</strong><br/>Note *: '+(CL),+(ML),+(DL)..etc.' Considered as <strong>Approved Half day Leave</strong></div><br/>" + sw.ToString() + "<br/>";
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
            Response.Redirect("../Client.UI/SchoolReports.aspx?Mode=SchoolGeneralReports");
        }
        #endregion

        protected void gvReport_PreRender(object sender, EventArgs e)
        {
            if (gvReport.Rows.Count > 0)
            {

                GridViewRow LastRow = gvReport.Rows[gvReport.Rows.Count - 1];

                LastRow.BackColor = System.Drawing.Color.White;
                LastRow.Font.Bold = true;
                LastRow.Font.Italic = true;
            }
        }
    }
}