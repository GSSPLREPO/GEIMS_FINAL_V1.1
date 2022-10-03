using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GEIMS.BL;
using GEIMS.Common;
using System.Data;
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
//using iTextSharp.tool.xml;
using log4net;
using iTextSharp.text.html.simpleparser;

namespace GEIMS.ReportUI
{
    public partial class DetailsEventReport : System.Web.UI.Page
    {

        private static ILog logger = LogManager.GetLogger(typeof(StudentAttendenceReport));

        #region Page load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("../UserLogin.aspx");
                }
                if (!IsPostBack)
                {
                    PanelGridview.Visible = false;
                    btnPrintDetail.Visible = false;
                    getSchoolName();
                    divReportView.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Bind Event list 
        public void BindEventList()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                ScheduleEventsBL objScheduledEventBL = new ScheduleEventsBL();

                if (ddlSchoolName.SelectedValue == "" || ddlSectionName.SelectedValue == "")
                {
                    objResult = objScheduledEventBL.ScheduledEventDetails_Select_ForListofEventDetailsRepot(txtFromDate.Text, txtToDate.Text);
                }
                else
                {
                    objResult = objScheduledEventBL.ScheduledEventDetails_Select_ForListofEventDetailsRepot(Convert.ToInt32(ddlSchoolName.SelectedValue), Convert.ToInt32(ddlSectionName.SelectedValue), txtFromDate.Text, txtToDate.Text);
                }
                if (objResult.resultDT.Rows.Count > 0)
                {
                    pnlVandorMaterialInfo.Visible = true;
                    PanelGridview.Visible = true;
                    gvReport.DataSource = objResult.resultDT;
                    gvReport.DataBind();

                }
                else
                {
                    pnlVandorMaterialInfo.Visible = true;
                    PanelGridview.Visible = false;
                    ClearAll();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Records Found.');", true);
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Bind SchoolName
        public void getSchoolName()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                SchoolBL objSchoolBl = new SchoolBL();

                objResult = objSchoolBl.School_SelectAll_ForDropDOwn(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                if (objResult != null)
                {
                    ddlSectionName.Enabled = true;
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlSchoolName, "SchoolNameEng", "SchoolMID");

                    }
                    ddlSchoolName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", ""));
                    ddlSectionName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", ""));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Fetch SectionName for Dropdown Section Name
        protected void ddlSchoolName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                DepartmentBL objDepartmentBl = new DepartmentBL();

                if (ddlSchoolName.SelectedValue != "")
                {
                    ddlSectionName.Enabled = true;
                    objResult = objDepartmentBl.Department_SelectAll_ForDropDown(Convert.ToInt32(ddlSchoolName.SelectedValue));
                    if (objResult != null)
                    {

                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            objControls.BindDropDown_ListBox(objResult.resultDT, ddlSectionName, "DepartmentNameENG", "DepartmentID");
                        }
                    }
                    ddlSectionName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", ""));
                }
                else
                {
                    ddlSectionName.SelectedValue = "";
                    ddlSectionName.Enabled = false;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region Go button Click Event
        protected void btnGo_Click(object sender, EventArgs e)
        {
            BindEventList();
        }
        #endregion

        #region Back to menu butto click Event
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Client.UI/TrustReports.aspx?Mode=EventReports");
        }
        #endregion

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            pnlVandorMaterialInfo.Visible = true;
            btnPrintDetail.Visible = false;
            divReportView.Visible = false;
            //divButtons.Visible = false;
        }
        #endregion

        #region Cancle Button
        protected void btnBack_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        #endregion

        #region Print Details Button
        protected void btnPrintDetail_Click(object sender, EventArgs e)
        {
            //btnExportPDF.Visible = false;
            pnlVandorMaterialInfo.Visible = false;
            divReportView.Visible = true;


            //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('divReportView');", true);
        }
        #endregion

        protected void gvReport_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #region Gridview Row command
        protected void gvReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                ScheduleEventsBL objScheduledEventBL = new ScheduleEventsBL();

                if (e.CommandName.ToString() == "Print1")
                {
                    btnPrintDetail.Visible = true;
                    pnlVandorMaterialInfo.Visible = false;
                    PanelGridview.Visible = false;
                    divReportView.Visible = true;

                    ViewState["ScheduledEventID"] = e.CommandArgument.ToString();

                    objResult = objScheduledEventBL.ScheduleEventID_Select_ForDetailReport(Convert.ToInt32(ViewState["ScheduledEventID"].ToString()));
                    if (objResult != null)
                    {
                        DataTable dtResult = objResult.resultDT;

                        if (dtResult.Rows.Count > 0)
                        {
                            if (objResult.resultDT.Rows[0]["SchoolNameEng"].ToString() == null)
                            {
                                lblSchoolName.Visible = false;
                                lblAddress.Visible = false;
                                lblPhoneNo.Visible = false;
                                lblSectionName.Visible = false;
                                //lblSchoolNameFooter.Visible = false;
                            }
                            else
                            {
                                lblSchoolName.Text = objResult.resultDT.Rows[0]["SchoolNameEng"].ToString();
                                lblAddress.Text = objResult.resultDT.Rows[0]["AddressEng"].ToString();
                                lblPhoneNo.Text = "Phono No :" + objResult.resultDT.Rows[0]["TelephoneNo"].ToString();
                                lblSectionName.Text = "Section Name :" + objResult.resultDT.Rows[0]["DepartmentNameENG"].ToString();
                               //lblSchoolNameFooter.Text = objResult.resultDT.Rows[0]["SchoolNameEng"].ToString();
                            }
                            lblEventName.Text = "Event Name :" + objResult.resultDT.Rows[0]["EventName"].ToString();
                            lblFromDate.Text = objResult.resultDT.Rows[0]["EventFromDate"].ToString();
                            lblToDate.Text = objResult.resultDT.Rows[0]["EventToDate"].ToString();
                            lblFromTime.Text = objResult.resultDT.Rows[0]["EventFromDateFromTime"].ToString();
                            lblToTime.Text = objResult.resultDT.Rows[0]["EventFromDateToTime"].ToString();
                            lblPlatform.Text = "Event Platform :" + objResult.resultDT.Rows[0]["EventPlatform"].ToString();
                            lblEventLocation.Text = "Event Location :" + objResult.resultDT.Rows[0]["EventLocation"].ToString();
                            lblAboutEvent.Text = "About the Event :" + objResult.resultDT.Rows[0]["EventDescription"].ToString();
                            lblOrgeniserName.Text = "Orgeniser Name :" + objResult.resultDT.Rows[0]["EventOrgeniserName"].ToString();
                            lblMobileNo.Text = "Mobile No :" + objResult.resultDT.Rows[0]["EventMobileNo"].ToString();
                            lblEmail.Text = "Email :" + objResult.resultDT.Rows[0]["EventEmail"].ToString();

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        protected void gvReport_PreRender(object sender, EventArgs e)
        {

        }

        #region Button Export PDF click Event
        protected void btnExportPDF_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                string filename = "EventList Report" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
               

                string strPath = Server.MapPath("~/Images/FERTILIZER NAGAR SCHOOL LOGO COLOUR copy.jpg");
                string content =  
                    "<div align='center' style='font-family:verdana;font-size:16px;margin-top:10px'> " +
                    "<div align='Left' style='font-family:Bold;font-size:16px;margin-top:10px;'>" +
                    "<img height='70px' width='70px' src='" + strPath + "'/> </div> " +
                     "<div style='font-size:14px;font-weight:bold;color:Black; margin-top:5px'>" + "Report : List of Events" + "</div><br/>" +
                    "<div style='font-size:16px;font-weight:bold;color:Black; margin-top:5px'>" + lblSchoolName.Text + "</div>" +
                     "<div style='font-size:11px;color:Black; margin-top:5px'>" + lblAddress.Text + "</div>"+
                     "<div style='font-size:11px;color:Black; margin-top:5px'>" + lblPhoneNo.Text + "</div><br/>"+
                     "<div style='font-size:11px;color:Black; margin-top:10px'>" + lblSectionName.Text + "</div><br/>"+
                     "<div style='font-size:11px;color:Black; margin-top:5px'>" + "________________________________________________________________________________________" + "</div><br/>" +
                     "<div style='font-size:14px;color:Black; margin-top:10px'>" + lblEventName.Text + "</div>"+
                     "<div style='font-size:11px;color:Black; margin-top:5px'>" + "Date : From &nbsp; "+lblFromDate.Text + "&nbsp; To&nbsp; "+lblToDate.Text+"</div>"+
                     "<div style='font-size:11px;color:Black; margin-top:5px'>" +"Time : From" +lblFromTime.Text+" To "+lblToTime.Text + "</div><br/>"+
                     "<div align='left' style='font-size:11px;color:Black; margin-top:5px; padding-left:30px'>" + lblEventLocation.Text + "</div>" +
                     "<div align='left' style='font-size:11px;color:Black; margin-top:5px; padding-left:30px'>" + lblAboutEvent.Text + "</div><br/><br/>" +
                     "<div style='font-size:11px;color:Black; margin-top:5px'>" + "________________________________________________________________________________________" + "</div><br/>" +
                     "<div align='left' style='font-size:11px;color:Black; margin-top:5px; padding-left:30px'>" + lblOrgeniserName.Text + "</div>" +
                     //"<div align='right' style='font-size:11px;color:Black; margin-top:5px; padding-right:50px'>" + lblSchoolNameFooter.Text + "</div>" +
                     "<div align='left' style='font-size:11px;color:Black; margin-top:5px; padding-left:30px'>" + lblMobileNo.Text + "</div>" +
                     "<div align='left' style='font-size:11px;color:Black; margin-top:5px; padding-left:30px'>" + lblEmail.Text + "</div>" +
                     "<div align='right' style='font-size:11px;color:Black; margin-top:5px; padding-right:50px'>" + "Administrator / Principal" + "</div>" +
                                   sw.ToString() + "<br/></div>";
                StringReader sr = new StringReader(content);
                // pdfDoc.SetMargins first for Left, second for Right, third for Top and forth for Buttom
                Document pdfDoc = new Document(iTextSharp.text.PageSize.A4, 20f, 20f, 10f, 20f);
                //pdfDoc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                Response.Write(pdfDoc);
                //gvReport.GridLines = GridLines.None;
                Response.End();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
       
        /*
        protected void btnExportWord_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
               
                string filename = "EventList Report" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".doc";
                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-word ";

                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);


                string strPath = Server.MapPath("~/Images/FERTILIZER NAGAR SCHOOL LOGO COLOUR copy.jpg");
                string content =
                    "<div align='center' style='font-family:verdana;font-size:16px;margin-top:10px'> " +
                    "<div align='Left' style='font-family:Bold;font-size:16px;margin-top:10px;'>" +
                    "<img height='100px' width='100px' src='" + strPath + "'/> </div> " +
                     "<div style='font-size:18px;font-weight:bold;color:Black; margin-top:5px'>" + "Report : List of Events" + "</div><br/>" +
                    "<div style='font-size:20px;font-weight:bold;color:Black; margin-top:5px'>" + lblSchoolName.Text + "</div>" +
                     "<div style='font-size:14px;color:Black; margin-top:5px'>" + lblAddress.Text + "</div>" +
                     "<div style='font-size:14px;color:Black; margin-top:5px'>" + lblPhoneNo.Text + "</div>" +
                     "<div style='font-size:16px;color:Black; margin-top:10px'>" + lblSectionName.Text + "</div>" +
                     "<div style='font-size:16px;color:Black; margin-top:5px'>" + "_________________________________________________________________" + "</div><br/>" +
                     "<div style='font-size:18px;color:Black; margin-top:10px'>" + lblEventName.Text + "</div>" +
                     "<div style='font-size:14px;color:Black; margin-top:5px'>" + "Date : From &nbsp; " + lblFromDate.Text + "&nbsp; To&nbsp; " + lblToDate.Text + "</div>" +
                     "<div style='font-size:14px;color:Black; margin-top:5px'>" + "Time : From" + lblFromTime.Text + " To " + lblToTime.Text + "</div><br/>" +
                     "<div align='left' style='font-size:14px;color:Black; margin-top:5px; padding-left:30px'>" + lblEventLocation.Text + "</div>" +
                     "<div align='left' style='font-size:14px;color:Black; margin-top:5px; padding-left:30px'>" + lblAboutEvent.Text + "</div><br/>" +
                     "<div style='font-size:16px;color:Black; margin-top:5px'>" + "_________________________________________________________________" + "</div><br/>" +
                     "<div align='left' style='font-size:14px;color:Black; margin-top:5px; padding-left:30px'>" + lblOrgeniserName.Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                     + lblSchoolNameFooter.Text + "</div>" +
                     "<div align='left' style='font-size:14px;color:Black; margin-top:5px; padding-left:30px'>" + lblMobileNo.Text + "</div>" +
                     "<div align='left' style='font-size:14px;color:Black; margin-top:5px; padding-left:30px'>" + lblEmail.Text +                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Administrator / Principal" + "</div>" +
                                   sw.ToString() + "<br/></div>";
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
        }*/
    }


}