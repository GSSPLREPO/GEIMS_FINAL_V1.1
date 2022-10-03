﻿using System;
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
    public partial class SarasariReport : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(SarasariReport));
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
                divButtons.Visible = false;
                getSchoolName();
            }
        }
        #region Bind SchoolName
        public void getSchoolName()
        {
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            SchoolBL objSchoolBl = new SchoolBL();

            objResult = objSchoolBl.School_SelectAll_ForDropDOwn(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
            if (objResult != null)
            {
                if (objResult.resultDT.Rows.Count > 0)
                {
                    objControls.BindDropDown_ListBox(objResult.resultDT, ddlSchool, "SchoolNameEng", "SchoolMID");

                }
                ddlSchool.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", ""));
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

                objResults = objSchoolBl.School_Select( Convert.ToInt32(ddlSchool.SelectedValue));

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

        #region Bind Student Attendance Grid
        public void BindStudentAttendance()
        {

            ApplicationResult objResult = new ApplicationResult();
            StudentAttendenceBL objStudentAttendanceBl = new StudentAttendenceBL();

            objResult = objStudentAttendanceBl.Sarasari_Report(Convert.ToInt32(Session[ApplicationSession.TRUSTID]),Convert.ToInt32(ddlSchool.SelectedValue), ddlYear.SelectedItem.ToString(), Convert.ToInt32(ddlMonth.SelectedValue));
            if (objResult.resultDT.Rows.Count > 0)
            {

                gvReport.DataSource = objResult.resultDT;
                gvReport.DataBind();
                gvAttendance.DataSource = objResult.resultDT;
                gvAttendance.DataBind();

                divReport.Visible = true;
                divButtons.Visible = true;
                btnBack.Visible = true;
                btnPrintDetail.Visible = true;
                pnlStudentAttendanceInfo.Visible = false;
                lblSchoolName.Text = ddlSchool.SelectedItem.ToString();
                lblYear.Text = ddlYear.SelectedItem.ToString();
                lblMonth.Text = ddlMonth.SelectedItem.ToString();
                lblSchool1.Text = ddlSchool.SelectedItem.ToString();
                lblYear1.Text = ddlYear.SelectedItem.ToString();
                lblMonth1.Text = ddlMonth.SelectedItem.ToString();

            }
            else
            {
                divReport.Visible = false;
                btnPrintDetail.Visible = false;
                pnlStudentAttendanceInfo.Visible = true;
                btnBack.Visible = false;
                divButtons.Visible = true;
                ClearAll();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Records Found.');", true);
            }
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
            BindStudentAttendance();
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
                Response.AddHeader("content-disposition", "attachment;filename=SarasariReport" + ddlSchool.SelectedItem.Text.Replace(" ", "_").Replace("<br/>", "") + "_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvReport.AllowPaging = false;
                //gvReport.DataBind();
                gvReport.RenderControl(hw);

                // string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:13px;'>" + ReportTitle + "</span><br/><br/><span style='font-size:8px:font-weight:bold'>" + sw.ToString() + "</span></div>";
                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Sarasari Report</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + ddlSchool.SelectedItem.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Year:</strong>" + ddlYear.SelectedItem.ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Month:</strong>" + ddlMonth.SelectedItem.ToString() + "</span><br/><br/><br/>" + sw.ToString() + "<br/>";
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

                Response.AddHeader("content-disposition", "attachment;filename=SarasariReport" + ddlSchool.SelectedItem.Text.Replace(" ", "_").Replace("<br/>", "") + "_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
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
                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Sarasari Report</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + ddlSchool.SelectedItem.ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Year:</strong>" + ddlYear.SelectedItem.ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Month:</strong>" + ddlMonth.SelectedItem.ToString() + "</span><br/><br/><br/>" + sw.ToString() + "<br/>";
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

                Response.AddHeader("content-disposition", "attachment;filename=SarasariReport" + ddlSchool.SelectedItem.Text.Replace(" ", "_").Replace("<br/>", "") + "_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".doc");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-word ";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                gvReport.AllowPaging = false;
                // gvReport.DataBind();
                gvReport.RenderControl(hw);


                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Sarasari Report</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + ddlSchool.SelectedItem.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Year:</strong>" + ddlYear.SelectedItem.ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Month:</strong>" + ddlMonth.SelectedItem.ToString() + "</span><br/><br/><br/>" + sw.ToString() + "<br/>";
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
            divButtons.Visible = false;

        }
        #endregion

        protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAcademicYear();
        }

        #region Report Button Event
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Client.UI/TrustReports.aspx?Mode=StatutoryReports");
        }
        #endregion
    }
}