using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GEIMS.BO;
using GEIMS.BL;
using GEIMS.Common;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace GEIMS.ReportUI
{
    public partial class ScheduleMeetingReport : System.Web.UI.Page
    {

        #region Page Load 
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
                    divMeetingName.Visible = false;
                    divToDate.Visible = false;
                    divFromDate.Visible = false;
                    btnGo.Visible = false;
                    pnlReport.Visible = false;
                    btnPrintDetail.Visible = false;
                   
                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Code for Generate Serial Number in Agenda GridView
        protected void gvAgenda_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                (e.Row.FindControl("lblSrNo") as Label).Text = (e.Row.RowIndex + 1).ToString();
            }
        }
        #endregion

        #region Bind Meeting Name Dropdown
        public void getMeetingName()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                MeetingsBL objMeetingsBL = new MeetingsBL();

                if (rdoAll.Checked == true)
                {
                    objResult = objMeetingsBL.MeetingsDetial_SelectAll_For_ScheduleMeeting_Report(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                }
                else
                {
                    objResult = objMeetingsBL.MeetingsDetial_SelectAll_For_ScheduleMeeting_Report(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), txtFromDate.Text, txtToDate.Text);
                }


                if (objResult.resultDT.Rows.Count > 0)
                {
                    objControls.BindDropDown_ListBox(objResult.resultDT, ddlMeeting, "Topic", "MeetingID");
                    ddlMeeting.Items.Insert(0, new ListItem("--Select--", ""));
                    divMeetingName.Visible = true;
                }
                else
                {
                    divMeetingName.Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Records Found.');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Radio Button All Cheked Change Event
        protected void rdoAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAll.Checked == true)
            {
                getMeetingName();
                divMeetingName.Visible = true;
                divFromDate.Visible = false;
                divToDate.Visible = false;
                btnGo.Visible = false;
            }
        }

        #endregion

        #region Radio Button Filter By Date Check Change Event
        protected void rdoDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDate.Checked == true)
            {

                divFromDate.Visible = true;
                divToDate.Visible = true;
                btnGo.Visible = true;
                divMeetingName.Visible = false;
            }
        }
        #endregion

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            pnlReport.Visible = false;
            pnlVandorMaterialInfo.Visible = true;
            //divButtons.Visible = false;
        }
        #endregion

        #region Back to Menu Button Click Event
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Client.UI/TrustReports.aspx?Mode=MeetingReports");
        }
        #endregion

        #region Cancle Button Click Event
        protected void btnBack_Click(object sender, EventArgs e)
        {
            ClearAll();
            btnPrintDetail.Visible = false;
        }
        #endregion

        #region Print Detail Button Click Event
        protected void btnPrintDetail_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('divReport');", true);
        }
        #endregion

        #region Go button Click Event
        protected void btnGo_Click(object sender, EventArgs e)
        {
            getMeetingName();
            
        }
        #endregion

        #region Button Export PDF Click Event
        [Obsolete]
        protected void btnExportPDF_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                string filename = "ScheduleMeetingReport" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                gvAgenda.Columns[0].ItemStyle.Width = Unit.Percentage(10);
                gvAgenda.Columns[1].ItemStyle.Width = Unit.Percentage(80);
                StringWriter sw = new StringWriter();
                StringWriter sw1 = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                HtmlTextWriter hw1 = new HtmlTextWriter(sw1);

               
                gvAgenda.AllowPaging = false;
                gvAgenda.GridLines = GridLines.Both;
                gvAgenda.RenderControl(hw);
   

                gvParticipant.AllowPaging = false;
                gvParticipant.GridLines = GridLines.Both;
               
                gvParticipant.RenderControl(hw1);

                string strPath = Server.MapPath("~/Images/FERTILIZER NAGAR SCHOOL LOGO COLOUR copy.jpg");
                string content = "<div align='Left' style='font-family:Bold;font-size:16px;margin-top:10px;'>" +
                    "<img height='70px' width='70px' src='" + strPath + "'/> </div> " +
                    "<div align='center' style='font-family:verdana;font-size:16px;margin-top:10px'> " +
                     "<div style='font-size:16px;font-weight:bold;color:Black; margin-top:10px'>" + "Report : Schedule Meeting" + "</div><br/>" +
                    "<div style='font-size:16px;font-weight:bold;color:Black; margin-top:10px'>" + lblMeetingName.Text + "</div><br/>" +
                    "<div>______________________________________________________________</div><br/>" +
                    "<div align='left' style='font-size:12px;color:Black; margin-top:10px'>" + "Date :" + lblDate.Text + "</div>" +
                    "<div align='right' style='font-size:12px;color:Black; margin-top:10px'>" + "Time From :" + lblFromTime.Text + "</div>" +
                    "<div align='left' style='font-size:12px;color:Black; margin-top:10px'>" + "Venue :" + lblPlace.Text + "</div>" +
                    "<div align='right' style='font-size:12px;color:Black; margin-top:10px'>" + "Time To :" + lblToTime.Text + "</div><br/>" +
                    "<div align='left' style='font-size:14px;color:Black;margin-top:30px;margin-bottom:10px;text-decoration:underline'>Agenda</div><br/>"

                                  + sw.ToString() + "<br/></div>";

                string content1 = "<div align='Left' style='font-family:Bold;font-size:16px;margin-top:10px;'>" +
                    "<div align='left' style='font-size:14px;color:Black;margin-top:30px;margin-bottom:10px;text-decoration:underline'>Participant</div><br/>"
                                 + sw1.ToString() + "<br/></div>";
                StringReader sr = new StringReader(content);
                StringReader sr1 = new StringReader(content1);
                Document pdfDoc = new Document(PageSize.A4, 20f, 20f, 10f, 20f);
                //pdfDoc.SetPageSize(iTextSharp.text.PageSize.A4);

                // pdfDoc.SetMargins first for Left, second for Right, third for Top and forth for Buttom

                //pdfDoc.SetMargins(20f, 20f, 50f, 50f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                htmlparser.Parse(sr1);
                pdfDoc.Close();
                Response.Write(pdfDoc);
                gvAgenda.GridLines = GridLines.None;
                gvParticipant.GridLines = GridLines.None;
                Response.End();
            }
            catch (Exception ex)
            {
                //log.Error("Button PDF", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);

            }

        }
        #endregion

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        #region Dropdown ddlMeeting Selected Index Change Event
        protected void ddlMeeting_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
                ApplicationResult objResult = new ApplicationResult();
                MeetingsBL objMeetingsBL = new MeetingsBL();

                objResult = objMeetingsBL.ScheduleMeeting_Report(Convert.ToInt32(ddlMeeting.SelectedValue));

                if (objResult.resultDT.Rows.Count > 0)
                {
                    btnPrintDetail.Visible = true;
                    pnlVandorMaterialInfo.Visible = false;

                    bindAgenda(Convert.ToInt32(ddlMeeting.SelectedValue));
                   
                    bindParticipant(Convert.ToInt32(ddlMeeting.SelectedValue));
                  
                    lblMeetingName.Text = objResult.resultDT.Rows[0]["Topic"].ToString();
                    lblDate.Text = objResult.resultDT.Rows[0]["MeetingDate"].ToString();
                    lblFromTime.Text = objResult.resultDT.Rows[0]["FromTime"].ToString();
                    lblToTime.Text = objResult.resultDT.Rows[0]["ToTime"].ToString();
                    lblPlace.Text = objResult.resultDT.Rows[0]["Venue"].ToString();
                    pnlReport.Visible = true;
                   
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Bind Agenda for Report
        public void bindAgenda(int intMeetingID)
        {
            ApplicationResult objResult = new ApplicationResult();
            MeetingsBL objMeetingsBL = new MeetingsBL();

            objResult = objMeetingsBL.ScheduleMeeting_FetchAgenda(intMeetingID);
            if (objResult.resultDT.Rows.Count > 0)
            {
                gvAgenda.DataSource = objResult.resultDT;
                gvAgenda.DataBind();
            }
        }
        #endregion

        #region Bind Participant for Report
        public void bindParticipant(int intMeetingID)
        {
            ApplicationResult objResult = new ApplicationResult();
            MeetingsBL objMeetingsBL = new MeetingsBL();

            objResult = objMeetingsBL.ScheduleMeeting_FetchParticipant(intMeetingID);
            if (objResult.resultDT.Rows.Count > 0)
            {
                gvParticipant.DataSource = objResult.resultDT;
                gvParticipant.DataBind();
            }
        }

        #endregion

        #region Code for Generate Serial Number in Participant GridView
        protected void gvParticipant_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                (e.Row.FindControl("lblSrNo1") as Label).Text = (e.Row.RowIndex + 1).ToString();
            }
        }
        #endregion
    }
}