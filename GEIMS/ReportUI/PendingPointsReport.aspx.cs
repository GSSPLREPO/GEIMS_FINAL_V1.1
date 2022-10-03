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
    public partial class PendingPointsReport : System.Web.UI.Page
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

        #region Back to Menu button click Event
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Client.UI/TrustReports.aspx?Mode=MeetingReports");
        }
        #endregion

        #region Cancle Button click Event
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

        #region Go button click Event
        protected void btnGo_Click(object sender, EventArgs e)
        {
            getMeetingName();
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

        #region Code of Generate Serial Number for Pending Point GridView
        protected void gvPendingPoint_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                (e.Row.FindControl("lblSrNo") as Label).Text = (e.Row.RowIndex + 1).ToString();
            }
        }
        #endregion

        #region Dropdown ddlMeeting Selected Index Change Event
        protected void ddlMeeting_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                ApplicationResult objResult = new ApplicationResult();
                MeetingsBL objMeetingsBL = new MeetingsBL();

               
                if (ddlMeeting.SelectedIndex != 0)
                {
                    objResult = objMeetingsBL.ScheduleMeeting_Report(Convert.ToInt32(ddlMeeting.SelectedValue));
                    if (objResult.resultDT.Rows.Count > 0)
                    {

                        pnlVandorMaterialInfo.Visible = false;
                        lblMeetingName.Text = ddlMeeting.SelectedItem.ToString();
                        bindPendingPoint(Convert.ToInt32(ddlMeeting.SelectedValue));

                    }
                    else
                    {
                        pnlVandorMaterialInfo.Visible = true;

                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Records Found.');", true);
                    }
                }
                else
                {

                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Bind Pending Point for Report
        public void bindPendingPoint(int intMeetingID)
        {
            ApplicationResult objResult = new ApplicationResult();
            MeetingPointsBL objMeetingPointsBL = new MeetingPointsBL();

            objResult = objMeetingPointsBL.FetchPendingPointForReport(intMeetingID);
            if (objResult.resultDT.Rows.Count > 0)
            {
                btnPrintDetail.Visible = true;
                pnlReport.Visible = true;
                gvPendingPoint.DataSource = objResult.resultDT;
                gvPendingPoint.DataBind();
            }
            else
            {
                pnlReport.Visible = false;
                pnlVandorMaterialInfo.Visible = true;
                btnPrintDetail.Visible = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Pending Point Not Found for these Meeting.');", true);
               
            }
        }
        #endregion

        #region Export PDF
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
                string filename = "Pending Point Report" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvPendingPoint.AllowPaging = false;
                gvPendingPoint.GridLines = GridLines.Both;
                gvPendingPoint.RenderControl(hw);
                // string Date = DateTime.UtcNow.AddHours(5.5).ToString();
                //string strSubTitle = "Asset Report</br> as   "; *+Date; *
                Label2.Text = "Pending Point";
                
                string strPath = Server.MapPath("~/Images/FERTILIZER NAGAR SCHOOL LOGO COLOUR copy.jpg");
                string content = "<div align='Left' style='font-family:Bold;font-size:16px;margin-top:10px;'>" +
                    "<img height='70px' width='70px' src='" + strPath + "'/> </div> " +
                    "<div align='center' style='font-family:verdana;font-size:16px;margin-top:10px'> " +
                     "<div style='font-size:14px;font-weight:bold;color:Black; margin-top:10px'>" + "Report : Pending Point" + "</div><br/>" +
                    "<div style='font-size:16px;font-weight:bold;color:Black; margin-top:10px'>" + lblMeetingName.Text + "</div><br/>" +
                    "<div>____________________________________________________________________________________________</div><br/>"+
                    "<div align='left' style='text-decoration:underline;font-size:14px;color:Black; margin-top:10px;'>" + Label2.Text + "</div><br/>" 
                                  + sw.ToString() + "<br/></div>";
                StringReader sr = new StringReader(content);
                Document pdfDoc = new Document(iTextSharp.text.PageSize.A3, 10f, 10f, 10f, 0f);
                pdfDoc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                Response.Write(pdfDoc);
                gvPendingPoint.GridLines = GridLines.None;
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
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }
    }
}