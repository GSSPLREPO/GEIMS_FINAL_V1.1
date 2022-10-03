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
    public partial class ListofEventReport : System.Web.UI.Page
    {
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
                    btnPrintDetail.Visible = false;
                    getSchoolName();
                    divReport.Visible = false;
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
                    pnlVandorMaterialInfo.Visible = false;
                    divReport.Visible = true;
                    gvReport.DataSource = objResult.resultDT;
                    gvReport.DataBind();
                    divReport.Visible = true;
                    if (ddlSectionName.SelectedValue == "")
                    {
                       
                        lblSchoolName.Visible = false;
                        lblPhoneNo.Visible = false;
                    }
                    else
                    {
                        gvReport.Columns[3].Visible = false;
                        lblSchoolName.Text = ddlSchoolName.SelectedItem.Text;
                        lblPhoneNo.Text = "Phone No :" + objResult.resultDT.Rows[0]["TelephoneNo"].ToString();
                    }


                    lblFromDate.Text = txtFromDate.Text;
                    lblToDate.Text = txtToDate.Text;
                    btnPrintDetail.Visible = true;
                }
                else
                {
                    pnlVandorMaterialInfo.Visible = true;
                    divReport.Visible = false;
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
                    ddlSchoolName.Items.Insert(0, new ListItem("--Select--", ""));
                    ddlSectionName.Items.Insert(0, new ListItem("--Select--", ""));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SchoolName Change Event Fetch SectionName for Dropdown Section Name
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
                    ddlSectionName.Items.Insert(0, new ListItem("--Select--", ""));
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
            divReport.Visible = false;
            pnlVandorMaterialInfo.Visible = true;
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
            // ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('divReport');", true);
        }
        #endregion

        #region Export PDF
        
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
                gvReport.AllowPaging = false;
                gvReport.GridLines = GridLines.Both;
                gvReport.RenderControl(hw);
                // string Date = DateTime.UtcNow.AddHours(5.5).ToString();
                //string strSubTitle = "Asset Report</br> as   "; *+Date; *

                string strPath = Server.MapPath("~/Images/FERTILIZER NAGAR SCHOOL LOGO COLOUR copy.jpg");
                string content = "<div align='Left' style='font-family:Bold;font-size:16px;margin-top:10px;'>" +
                    "<img height='70px' width='70px' src='" + strPath + "'/> </div> " +
                    "<div align='center' style='font-family:verdana;font-size:16px;margin-top:10px'> " +
                     "<div style='font-size:16px;font-weight:bold;color:Black; margin-top:10px'>" + "Report : List of Events" + "</div><br/>" +
                    "<div style='font-size:16px;font-weight:bold;color:Black; margin-top:10px'>" +lblSchoolName.Text+"</div><br/>"
                                  + sw.ToString()+ "<br/></div>"; 
                StringReader sr = new StringReader(content);
                Document pdfDoc = new Document(iTextSharp.text.PageSize.A3, 10f, 10f, 10f, 0f);
                pdfDoc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                Response.Write(pdfDoc);
                gvReport.GridLines = GridLines.None;
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