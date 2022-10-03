using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
    public partial class ClassWiseStudentFees : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(StudentList));

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }
            if (!IsPostBack)
            {
                getSectionName();
                BindAcademicYear();
                divReport.Visible = false;
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Attributes.Add("readonly", "readonly");
            }
        }
        #endregion



        #region Bind Class Name
        public void getClassName()
        {
            ddlclass.Items.Clear();
            ddlDivision.Items.Clear();
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            ClassBL objClassBl = new ClassBL();
            // string str = ddlsection.se;
            objResult = objClassBl.Class_SelectAll_SectionWise_ForDropDown(Convert.ToInt32(ddlsection.SelectedValue), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
            if (objResult != null)
            {
                objControls.BindDropDown_ListBox(objResult.resultDT, ddlclass, "ClassName", "ClassMID");
                if (objResult.resultDT.Rows.Count > 0)
                {

                }
                ddlclass.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", ""));
                ddlDivision.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", ""));

            }
        }
        #endregion

        #region Bind Section Name
        public void getSectionName()
        {
            ddlsection.Items.Clear();
            ddlclass.Items.Clear();
            ddlDivision.Items.Clear();
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            SectionBL objSectionBl = new SectionBL();

            objResult = objSectionBl.Section_SelectAll_ForDropDown(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
            if (objResult != null)
            {
                objControls.BindDropDown_ListBox(objResult.resultDT, ddlsection, "SectionName", "SectionMID");
                if (objResult.resultDT.Rows.Count > 0)
                {


                }
                ddlsection.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", ""));
                ddlclass.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", ""));
                ddlDivision.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", ""));

            }
        }
        #endregion

        #region Bind Division Name
        public void getDivisionName()
        {
            ddlDivision.Items.Clear();
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            DivisionTBL objDivisionBl = new DivisionTBL();

            objResult = objDivisionBl.Division_SelectAll_ClassWise_ForDropDown(Convert.ToInt32(ddlclass.SelectedValue), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
            if (objResult != null)
            {
                objControls.BindDropDown_ListBox(objResult.resultDT, ddlDivision, "DivisionName", "DivisionTID");
                if (objResult.resultDT.Rows.Count > 0)
                {

                }

                ddlDivision.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", ""));

            }
        }
        #endregion

        #region BindAcademicYear
        public void BindAcademicYear()
        {
            try
            {
                #region Fetch Academic Month from School
                SchoolBL objSchoolBl = new SchoolBL();
                ApplicationResult objResults = new ApplicationResult();
                int intMonth = 0;

                objResults = objSchoolBl.School_Select(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));

                if (objResults != null)
                {
                    if (objResults.resultDT.Rows.Count > 0)
                    {

                        intMonth = Convert.ToInt32(objResults.resultDT.Rows[0][SchoolBO.SCHOOL_ACADEMICMONTH].ToString());
                    }

                }
                #endregion

                Controls objControls = new Controls();
                int month = System.DateTime.Now.Month;
                int Year = System.DateTime.Now.Year;
                int lastTwoDigit = Year % 100;
                string yr = string.Empty;
                if (month >= intMonth)
                    yr = (lastTwoDigit.ToString() + (lastTwoDigit + 1).ToString()).ToString();
                else
                    yr = ((lastTwoDigit - 1).ToString() + lastTwoDigit.ToString()).ToString();

                int f = (Convert.ToInt32(yr.Substring(0, 2)));
                int l = (Convert.ToInt32(yr.Substring(2, 2)));

                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("AcademicYear", typeof(string)));

                for (int i = 0; i < 5; i++)
                {
                    dr = dt.NewRow();
                    if (i == 0)
                    {
                        dr["AcademicYear"] = Convert.ToString(f.ToString() + "-" + l.ToString());
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        if ((f - 1).ToString().Length < 2)
                        {
                            if (f.ToString().Length == 2)
                            {
                                dr["AcademicYear"] = Convert.ToString("0" + (f - 1).ToString() + "-" + (f).ToString());
                            }
                            else
                            {
                                dr["AcademicYear"] = Convert.ToString("0" + (f - 1).ToString() + "-" + "0" + (f).ToString());
                            }
                            dt.Rows.Add(dr);
                        }
                        else
                        {
                            dr["AcademicYear"] = Convert.ToString((f - 1).ToString() + "-" + (f).ToString());
                            dt.Rows.Add(dr);
                        }
                        f = f - 1;
                        l = f;
                    }
                }

                objControls.BindDropDown_ListBox(dt, ddlYear, "AcademicYear", "AcademicYear");
                ddlYear.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-Select-", ""));


            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion

        #region Bind Student DataList
        public void BindStudentList()
        {

            ApplicationResult objResult = new ApplicationResult();
            StudentBL objStudentBl = new StudentBL();

            objResult = objStudentBl.FeesCollction_ClassWiseStudentFees(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(ddlclass.SelectedValue), Convert.ToInt32(ddlDivision.SelectedValue), ddlYear.SelectedItem.ToString(), txtFromDate.Text.Trim(), txtToDate.Text.Trim());
            if (objResult.resultDT.Rows.Count > 0)
            {

                gvReport.DataSource = objResult.resultDT;
                gvReport.DataBind();

                divReport.Visible = true;
                pnlFeesCollectionInfo.Visible = false;
                lblTrust.Text = Session[ApplicationSession.TRUSTNAME].ToString();
                lblSchool.Text = Session[ApplicationSession.SCHOOLNAME].ToString();
                lblClass.Text = ddlclass.SelectedItem.ToString() + "-" + ddlDivision.SelectedItem.ToString();
                lblYear.Text = ddlYear.SelectedItem.ToString();
            }
            else
            {
                divReport.Visible = false;
                pnlFeesCollectionInfo.Visible = true;
                ClearAll();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Records Found.');", true);
            }
        }
        #endregion



        #region Report Button Event
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Client.UI/SchoolReports.aspx?Mode=SchoolFeesReports");
        }
        #endregion

        #region Cancel Button click Event
        protected void btnBack_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        #endregion

        #region Go Button click Event
        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                BindStudentList();
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
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
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                Response.AddHeader("content-disposition", "attachment;filename=" + Session[ApplicationSession.SCHOOLNAME].ToString().Replace(" ", "_").Replace("<br/>", "") + "_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvReport.AllowPaging = false;
                //gvReport.DataBind();
                gvReport.RenderControl(hw);

                // string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:13px;'>" + ReportTitle + "</span><br/><br/><span style='font-size:8px:font-weight:bold'>" + sw.ToString() + "</span></div>";
                string content = "<div align='center' style='font-family:verdana;font-size:13px'>" +
                                 "<span style='font-size:16px:font-weight:bold;color:Maroon;'>Student List Report</span><br/><span style='font-size:13px:font-weight:bold'></span>" +
                                 "<br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/>" +
                                 "<span align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + Session[ApplicationSession.SCHOOLNAME].ToString() + "</span><br/>" +
                                 "<div align='center' style='font-family:verdana;font-size:11px'><strong>Section :</strong>" + ddlsection.SelectedItem.ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'>" +
                                 "<strong>Division:</strong>" + ddlclass.SelectedItem.ToString() + "-" + ddlDivision.SelectedItem.ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Year:</strong>" + ddlYear.SelectedItem.ToString() + "</span><br/>" +
                                 "<span align='center' style='font-family:verdana;font-size:11px'><strong></strong></span><br/><br/><br/>" + "Note * : Blank Space Considered as Fees Not applicable" + "<br/>" + "Note * : '$' Considered as Outstandingfees" + "<br/>" + "" + sw.ToString() +
                                 "<br/>";
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

                Response.AddHeader("content-disposition", "attachment;filename=" + Session[ApplicationSession.SCHOOLNAME].ToString().Replace(" ", "_").Replace("<br/>", "") + "_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
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
                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Student List Report</span><br/><br/><span style='font-size:13px:font-weight:bold'></span><br/><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + Session[ApplicationSession.SCHOOLNAME].ToString() + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Section :</strong>" + ddlsection.SelectedItem.ToString() + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Division:</strong>" + ddlclass.SelectedItem.ToString() + "-" + ddlDivision.SelectedItem.ToString() + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Year:</strong>" + ddlYear.SelectedItem.ToString() + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'><strong></strong></div>" +
                     "<br/><div align='left'>" + "<span style='font-size:11px:font-weight:bold:padding-left:2px'>Note * : Blank Space Considered as Fees Not applicable<br/>Note * : '$' Considered as Outstandingfees</span></div>"
                   + "<br/>" + sw.ToString() + "<br/>";
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

                Response.AddHeader("content-disposition", "attachment;filename=" + Session[ApplicationSession.SCHOOLNAME].ToString().Replace(" ", "_").Replace("<br/>", "") + "_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".doc");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-word ";
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                gvReport.AllowPaging = false;
                // gvReport.DataBind();
                gvReport.RenderControl(hw);


                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Student List Report</span><br/><br/><span style='font-size:13px:font-weight:bold'></span><br/><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + Session[ApplicationSession.SCHOOLNAME].ToString() + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Section :</strong>" + ddlsection.SelectedItem.ToString() + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Division:</strong>" + ddlclass.SelectedItem.ToString() + "-" + ddlDivision.SelectedItem.ToString() + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Year:</strong>" + ddlYear.SelectedItem.ToString() + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'><strong></strong></div>" +
                     "<br/><div align='left'>" + "<span style='font-size:11px:font-weight:bold:padding-left:2px'>Note * : Blank Space Considered as Fees Not applicable<br/>Note * : '$' Considered as Outstandingfees</span></div>"
                   + "<br/>" + sw.ToString() + "<br/>";
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

        #region VerifyRenderingInServerForm
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        #endregion



        #region ddlsection SelectedIndexChanged Event
        protected void ddlsection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlsection.SelectedValue != "")
                {
                    getClassName();
                }
                else
                {
                    getSectionName();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion

        #region ddlClass SelectedIndexChange Event
        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlclass.SelectedValue != "")
                {
                    getDivisionName();
                }
                else
                {
                    getClassName();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion


        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            divReport.Visible = false;
        }
        #endregion

        protected void gvReport_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TableCellCollection cells = e.Row.Cells;

                foreach (TableCell cell in cells)
                {
                    cell.Text = Server.HtmlDecode(cell.Text);
                    cell.Attributes.Add("style", "white-space: nowrap;");
                }

            }
            else
            {
                TableCellCollection cells = e.Row.Cells;

                foreach (TableCell cell in cells)
                {
                    cell.Attributes.Add("style", "white-space: nowrap;");
                }
            }
        }
    }
}