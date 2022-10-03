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
using GEIMS.Client.UI;
using GEIMS.Common;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace GEIMS.Result
{
    public partial class GradeWiseResultReport : System.Web.UI.Page
    {
        #region Declaration
        private static ILog logger = LogManager.GetLogger(typeof(StudentDetailMaster));
        private TableCell tcStudentName = null;
        private TableCell tcPercentage = null;
        private string strTempStudentName = string.Empty;
        private string strTempPercentage = string.Empty;
        #endregion

        #region Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }
            if (!IsPostBack)
            {
                BindClass();
                BindAcademicYear();
                divReport.Visible = false;
            }
        }
        #endregion


        #region Bind Class
        public void BindClass()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                ClassBL objClassBl = new ClassBL();
                Controls objControl = new Controls();
                objResult = objClassBl.Class_SelectAll(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    objControl.BindDropDown_ListBox(objResult.resultDT, ddlClass, "ClassName", "ClassMID");
                    //PanelVisibility(1);
                    ddlClass.Items.Insert(0, new ListItem("--Select--", ""));
                }
                else
                {
                    //PanelVisibility(2);
                }

            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }

        }

        #endregion

        #region Bind Division
        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                DivisionTBL objDivisionTbl = new DivisionTBL();

                objResult = objDivisionTbl.DivisionT_SelectAll_For_Exam(Convert.ToInt32(ddlClass.SelectedValue));
                if (objResult != null)
                {
                    objControls.BindDropDown_ListBox(objResult.resultDT, ddlDivision, "DivisionName", "DivisionTID");
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        //DivisionTID = Convert.ToInt32(objResult.resultDT.Rows[0]["DivisionTID"].ToString());
                        //ViewState["DivisionTID"] = DivisionTID;

                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Bind Academic Year
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

                objControls.BindDropDown_ListBox(dt, ddlAcademicYear, "AcademicYear", "AcademicYear");

            }
            catch (Exception ex)
            {
                //logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");

            }
        }
        #endregion


        #region Button Report Click Event
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Result/ExamResultReport.aspx");

        }
        #endregion

        #region Button Go Click Event
        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                ExamConfigurationBL objExamConfigurationBL = new ExamConfigurationBL();
                objResult = objExamConfigurationBL.ExamConfiguration_Select_Result_ClassWise_GradeReport(Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlDivision.SelectedValue),
                    ddlAcademicYear.SelectedItem.ToString(), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult.resultDT.Rows.Count > 0)
                {
                    gvReport.DataSource = objResult.resultDT;
                    gvReport.DataBind();
                    //TemplateField tf = new TemplateField();
                    //tf.HeaderText = "TEMPLATE FIELD";
                    //tf.Visible = true;

                    //this.gvReport.Columns.Insert(0, tf);
                    divReport.Visible = true;
                    //if (gvReport.Rows.Count > 1)
                    //{
                    //    for (int i = 0; i < gvReport.Rows.Count - 1; i++)
                    //    {
                    //        gvReport.Rows[i].Cells[0].Text = (i + 1).ToString();
                    //    }
                    //}
                    pnlEmployeeInfo.Visible = false;
                    lblTrust.Text = Session[ApplicationSession.TRUSTNAME].ToString();
                    lblSchool.Text = Session[ApplicationSession.SCHOOLNAME].ToString();
                    lblClass.Text = ddlClass.SelectedItem.ToString();
                    lblDivision.Text = ddlDivision.SelectedItem.ToString();
                    lblAcademicYear.Text = ddlAcademicYear.SelectedItem.ToString();
                    //lblExam.Text = ddlExam.SelectedItem.ToString();
                }
                else
                {
                    divReport.Visible = false;
                    // btnPrintDetail.Visible = false;
                    pnlEmployeeInfo.Visible = true;
                    ClearAll();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Records Found.');", true);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
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
                Response.AddHeader("content-disposition", "attachment;filename=GradeResultReport" + Session[ApplicationSession.SCHOOLNAME].ToString().Replace(" ", "_").Replace("<br/>", "") + "_" + DateTime.Now.Date.ToString("yyyy-mm-dd") + lblClass.Text + ".pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvReport.AllowPaging = false;
                //gvReport.DataBind();
                gvReport.RenderControl(hw);

                // string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:13px;'>" + ReportTitle + "</span><br/><br/><span style='font-size:8px:font-weight:bold'>" + sw.ToString() + "</span></div>";
                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Report : ClassWise Grade Result</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + Session[ApplicationSession.SCHOOLNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Class :</strong>" + lblClass.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Academic Year:</strong>" + lblAcademicYear.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'>" + "</span><br/><br/>" + sw.ToString() + "<br/>";
                // string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Student List Report</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Department :</strong>" + lblDepartmenmt.Text + "</span><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Designation:</strong>" + lblDesignation.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Role:</strong>" + lblRole.Text + "</div><br/><br/>" + sw.ToString() + "<br/>";
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

                Response.AddHeader("content-disposition", "attachment;filename= ClassWiseGradeResultReport" + "_" + Session[ApplicationSession.SCHOOLNAME] + "_" + DateTime.Now.Date.ToString("yyyy-MM-dd") + lblClass.Text + ".xlsx");
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
                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Report : ClassWise Grade Result</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><strong>School Name :</strong>" + Session[ApplicationSession.SCHOOLNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Class :</strong>" + lblClass.Text + "</span><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Academic Year:</strong>" + lblAcademicYear.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'>" + "</div><br/><br/>" + sw.ToString() + "<br/>";
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

                Response.AddHeader("content-disposition", "attachment;filename=ClassWiseGradeResultReport" + Session[ApplicationSession.SCHOOLNAME].ToString().Replace(" ", "_").Replace("<br/>", "") + "_" + DateTime.Now.Date.ToString("yyyy-mm-dd") + lblClass.Text +  ".doc");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-word ";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                gvReport.AllowPaging = false;
                // gvReport.DataBind();
                gvReport.RenderControl(hw);


                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Report : ClassWise Grade Result</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + Session[ApplicationSession.SCHOOLNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Class :</strong>" + lblClass.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Academic Year:</strong>" + lblAcademicYear.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'>" +  "</span><br/><br/>" + sw.ToString() + "<br/>";
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

        #region Button Back Click Event
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
            pnlEmployeeInfo.Visible = true;
        }
        #endregion

        #region VerifyRenderingInServerForm
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        #endregion

        protected void gvReport_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Style.Add("text-align", "left");
                string strStudentName = (string)DataBinder.GetPropertyValue(e.Row.DataItem, "StudentName");
                if (strStudentName == strTempStudentName)
                {

                    e.Row.Cells[0].Text = "";
                    e.Row.Cells[0].Style.Add("border-width", "0px");
                    //e.Row.Cells[1].Text = "";
                    //e.Row.Cells[1].Style.Add("border-width", "0px");
                }
                else
                {

                    this.tcStudentName = e.Row.Cells[0];
                    this.tcStudentName.RowSpan = 1;
                    this.strTempStudentName = strStudentName;
                    //e.Row.Cells[0].Style.Add(" border-style", "solid none none none");
                    e.Row.Cells[0].Style.Add(" border-style", "solid none none none");
                   // e.Row.Cells[1].Style.Add(" border-style", "solid none none solid");
                }


                //int colCount = e.Row.Cells.Count;
                //e.Row.Cells[colCount-1].Style.Add("text-align", "left");
                //string strPercentage =  (string)DataBinder.GetPropertyValue(e.Row.DataItem, "Percentage");
                //if (strPercentage == strTempPercentage)
                //{

                //    e.Row.Cells[colCount-1].Text = "";
                //    e.Row.Cells[colCount-1].Style.Add("border-width", "0px");
                //    //e.Row.Cells[1].Text = "";
                //    //e.Row.Cells[1].Style.Add("border-width", "0px");
                //}
                //else
                //{

                //    this.tcPercentage = e.Row.Cells[colCount-1];
                //    this.tcPercentage.RowSpan = 1;
                //    this.strTempPercentage = strPercentage;
                //    //e.Row.Cells[0].Style.Add(" border-style", "solid none none none");
                //    e.Row.Cells[colCount-1].Style.Add(" border-style", "solid none none none");
                //    // e.Row.Cells[1].Style.Add(" border-style", "solid none none solid");
                //}
            }
        }

    }
}