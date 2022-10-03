using System;
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
    public partial class ExportStudentList : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(ExportStudentList));
        #region Page_Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }
            if (!IsPostBack)
            {
                getSectionName();
                getStatusName();
                BindAcademicYear();
                divReport.Visible = false;
                
                BindParameterGrid();
            }
        }
        #endregion

        #region Back Button Event
        protected void btnBack_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        #endregion

        #region Go button Event
        protected void btnGo_Click(object sender, EventArgs e)
        {
            CheckBox chk = new CheckBox();
            string strFields = "";
            foreach (GridViewRow rowItem in gvParameter.Rows)
            {
                chk = (CheckBox)(rowItem.Cells[2].FindControl("chk"));
                if (chk.Checked == true)
                {
                    if (strFields == "")
                        strFields = rowItem.Cells[0].Text;
                    else
                        strFields = strFields + "," + rowItem.Cells[0].Text;
                }
            }
            ViewState["Feilds"] = strFields;
            BindStudentList();
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

        #region Bind Parameter Grid
        public void BindParameterGrid()
        {
            DataTable dtParameters = new DataTable();
            dtParameters.Columns.Add("SrNo");
            dtParameters.Columns.Add("Fields");
            dtParameters.Rows.Add("1", "Sr.");
            dtParameters.Rows.Add("2", "Student Name");
            dtParameters.Rows.Add("3", "Student Name (Gujarati)");
            dtParameters.Rows.Add("4", "Father Name");
            dtParameters.Rows.Add("5", "Father Name (Gujarati)");
            dtParameters.Rows.Add("6", "Mother Name");
            dtParameters.Rows.Add("7", "Mother Name (Gujarati)");
            dtParameters.Rows.Add("8", "Admission No");
            dtParameters.Rows.Add("9", "Gr No");
            dtParameters.Rows.Add("10", "Admitted Class Name");
            dtParameters.Rows.Add("11", "Admitted Division Name");
            dtParameters.Rows.Add("12", "Admitted Year");
            dtParameters.Rows.Add("13", "Registered Year");
            dtParameters.Rows.Add("14", "Admitted Gr No");
            dtParameters.Rows.Add("15", "Gender Guj");
            dtParameters.Rows.Add("16", "Gender Eng");
            dtParameters.Rows.Add("17", "Date Of Birth");
            dtParameters.Rows.Add("18", "Birth District");
            dtParameters.Rows.Add("19", "Birth District(Gujarati)");
            dtParameters.Rows.Add("20", "Nationality");
            dtParameters.Rows.Add("21", "Nationality(Gujarati)");
            dtParameters.Rows.Add("22", "Religion");
            dtParameters.Rows.Add("23", "Caste");
            dtParameters.Rows.Add("24", "Caste (Gujarati)");
            dtParameters.Rows.Add("25", "Sub Caste");
            dtParameters.Rows.Add("26", "Sub Caste (Gujarati)");
            dtParameters.Rows.Add("27", "Category");
            dtParameters.Rows.Add("28", "Category (Gujarati)");
            dtParameters.Rows.Add("29", "Sub Category");
            dtParameters.Rows.Add("30", "Handicap Precent");
            dtParameters.Rows.Add("31", "Other Defect");
            dtParameters.Rows.Add("32", "Present Address");
            dtParameters.Rows.Add("33", "Present Address (Gujarati)");
            dtParameters.Rows.Add("34", "Present City");
            dtParameters.Rows.Add("35", "Present City (Gujarati)");
            dtParameters.Rows.Add("36", "Present State");
            dtParameters.Rows.Add("37", "Present State (Gujarati)");
            dtParameters.Rows.Add("38", "Present PinCode");
            dtParameters.Rows.Add("39", "Present Contact No");
            dtParameters.Rows.Add("40", "Present EmailId");
            dtParameters.Rows.Add("41", "Permanent Address");
            dtParameters.Rows.Add("42", "Permanent Address (Gujarati)");
            dtParameters.Rows.Add("43", "Permanent City");
            dtParameters.Rows.Add("44", "Permanent City (Gujarati)");
            dtParameters.Rows.Add("45", "Permanent State");
            dtParameters.Rows.Add("46", "Permanent State (Gujarati)");
            dtParameters.Rows.Add("47", "Permanent PinCode");
            dtParameters.Rows.Add("48", "Permanent Contact No");
            dtParameters.Rows.Add("49", "Permanent EmailId");
            dtParameters.Rows.Add("50", "Father Occupation");
            dtParameters.Rows.Add("51", "Mother Occupation");
            dtParameters.Rows.Add("52", "Gardian Occupation");
            dtParameters.Rows.Add("53", "Father Qualification");
            dtParameters.Rows.Add("54", "Mother Qualification");
            dtParameters.Rows.Add("55", "Gardian Qualification");
            dtParameters.Rows.Add("56", "Father MobileNo");
            dtParameters.Rows.Add("57", "Mother MobileNo");
            dtParameters.Rows.Add("58", "Gardian MobileNo");
            dtParameters.Rows.Add("59", "Father EmailID");
            dtParameters.Rows.Add("60", "Mother EmailID");
            dtParameters.Rows.Add("61", "Gardian EmailID");
            dtParameters.Rows.Add("62", "Height");
            dtParameters.Rows.Add("63", "Wight");
            dtParameters.Rows.Add("64", "Hobbies");
            dtParameters.Rows.Add("65", "LeftDate");
            dtParameters.Rows.Add("66", "LeftYear");
            dtParameters.Rows.Add("67", "LeftReason");
            dtParameters.Rows.Add("68", "LeftStd");
            dtParameters.Rows.Add("69", "LcNo");
            dtParameters.Rows.Add("70", "LcDate");
            dtParameters.Rows.Add("71", "LcRemarks");
            dtParameters.Rows.Add("72", "LcCopy");
            gvParameter.DataSource = dtParameters;
            gvParameter.DataBind();
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

        #region Bind Status Name
        public void getStatusName()
        {
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            StatusBL objStatusBl = new StatusBL();

            objResult = objStatusBl.Status_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
            if (objResult != null)
            {
                objControls.BindDropDown_ListBox(objResult.resultDT, ddlStatus, "StatusName", "StatusMasterID");
                if (objResult.resultDT.Rows.Count > 0)
                {

                }

                ddlStatus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", ""));

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

            string[] FieldstoDisplay;
            FieldstoDisplay = ViewState["Feilds"].ToString().Split(',');

            ApplicationResult objResult = new ApplicationResult();
            ApplicationResult objResult1 = new ApplicationResult();
            StudentBL objStudentBl = new StudentBL();

            objResult = objStudentBl.StudentList_ForReport(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(ddlsection.SelectedValue), Convert.ToInt32(ddlclass.SelectedValue), Convert.ToInt32(ddlDivision.SelectedValue), ddlYear.SelectedItem.ToString(), Convert.ToInt32(ddlStatus.SelectedValue));
            objResult1 = objStudentBl.StudentList_ForReportExport(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(ddlsection.SelectedValue), Convert.ToInt32(ddlclass.SelectedValue), Convert.ToInt32(ddlDivision.SelectedValue), ddlYear.SelectedItem.ToString(), Convert.ToInt32(ddlStatus.SelectedValue));
            if (objResult.resultDT.Rows.Count > 0)
            {
                //GvExport
                GvExport.Visible = true;
                GvExport.DataSource = objResult1.resultDT;
                GvExport.DataBind();
                for (int i = 72; i > 0; i--)
                {
                    if (FieldstoDisplay.Contains(i.ToString()) == false)
                        objResult.resultDT.Columns.Remove(objResult.resultDT.Columns[i - 1]);
                }
                gvReport.DataSource = objResult.resultDT;
                gvReport.DataBind();

                divReport.Visible = true;
                //btnPrintDetail.Visible = true;
                pnlStudentInfo.Visible = false;
                lblSchoolName.Text = Session[ApplicationSession.SCHOOLNAME].ToString();
                lblSectionName.Text = ddlsection.SelectedItem.ToString();
                lblClassName.Text = ddlclass.SelectedItem.ToString() + "-" + ddlDivision.SelectedItem.ToString();
                lblYear.Text = ddlYear.SelectedItem.ToString();
                lblStatusName.Text = ddlStatus.SelectedItem.ToString();
            }
            else
            {
                divReport.Visible = false;
                // btnPrintDetail.Visible = false;
                pnlStudentInfo.Visible = true;
                ClearAll();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Records Found.');", true);
            }
        }
        #endregion

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            divReport.Visible = false;
            pnlStudentInfo.Visible = true;
            BindParameterGrid();
           
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
                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Student List Report</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + Session[ApplicationSession.SCHOOLNAME].ToString() + "</span><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Section :</strong>" + ddlsection.SelectedItem.ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Division:</strong>" + ddlclass.SelectedItem.ToString() + "-" + ddlDivision.SelectedItem.ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Year:</strong>" + ddlYear.SelectedItem.ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Status:</strong>" + ddlStatus.SelectedItem.ToString() + "</span><br/><br/><br/>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        " + sw.ToString() + "<br/>";
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
                gvReport.Visible = false;
                GvExport.Visible = true;


                Response.AddHeader("content-disposition", "attachment;filename=" + Session[ApplicationSession.SCHOOLNAME].ToString().Replace(" ", "_").Replace("<br/>", "") + "_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                Response.Charset = "";
                //Response.ContentType = "application/vnd.ms-excel"; 
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                gvReport.Visible = false;
                GvExport.Visible = true;
                GvExport.AllowPaging = false;
                
                // gvReport.DataBind();

                //Change the Header Row back to white color
                //gvReport.HeaderRow.Style.Add("background-color", "#67A3D1");

                GvExport.HeaderRow.Style.Add("ForeColor", "#000000");
                

                GvExport.RenderControl(hw);

                // string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:13px:font-weight:bold'>" + ReportTitle + "</span><br/><br/>" + sw.ToString() + "</div>";
                //string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>" + lblTitle.Text + "</span><br/><br/><span style='font-size:13px:font-weight:bold'>" + ReportTitle + "</span><br/><br/><div align='right' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + Date + "</div><br/>" + sw.ToString() + "<br/><br/><br/><div align='left'><span style='font-size:11px:font-weight:bold:padding-left:2px'>" + lblText.Text + "</span></div>";
                //string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Student List Report</span><br/><br/><span style='font-size:13px:font-weight:bold'></span><br/><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + Session[ApplicationSession.SCHOOLNAME].ToString() + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Section :</strong>" + ddlsection.SelectedItem.ToString() + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Division:</strong>" + ddlclass.SelectedItem.ToString() + "-" + ddlDivision.SelectedItem.ToString() + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Year:</strong>" + ddlYear.SelectedItem.ToString() + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Status:</strong>" + ddlStatus.SelectedItem.ToString() + "</div><br/>" + sw.ToString() + "<br/><br/><br/><div align='left'><span style='font-size:11px:font-weight:bold:padding-left:2px'></span></div>";
                string content = "<div align='left' style='font-family:verdana;font-size:13px' >" +
                    "<div align='left' style='font-family:verdana;font-size:11px'><strong>Current</strong></div>" +
                    "<div align='left' style='font-family:verdana;font-size:11px'><strong>Completed</strong></div>" +
                    "<div align='left style='font-family:verdana;font-size:11px'><strong>Left</strong></div>" +
                    "<div align='left' style='font-family:verdana;font-size:11px'><strong>Drop:</strong></div>" +
                    "<div align='left' style='font-family:verdana;font-size:11px'><strong>Cancelled:</strong></div>" +
                      "<div align='left' style='font-family:verdana;font-size:11px'><strong>Fail:</strong></div>" +
                    "<span style='font-size:16px:font-weight:bold;color:Maroon;'>Student List Report</span><br/>" +
                    "<br/><span style='font-size:13px:font-weight:bold'></span>" +
                    //"<div align='left' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</div><br/>" +
                    "<div align='left' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + Session[ApplicationSession.SCHOOLNAME].ToString() + "</div><br/>" +
                    "<div align='left' style='font-family:verdana;font-size:11px'><strong>Section :</strong>" + ddlsection.SelectedItem.ToString() + "</div><br/>" +
                    "<div align='left style='font-family:verdana;font-size:11px'><strong>Division:</strong>" + ddlclass.SelectedItem.ToString() + "-" + ddlDivision.SelectedItem.ToString() + "</div><br/>" +
                    "<div align='left' style='font-family:verdana;font-size:11px'><strong>Year:</strong>" + ddlYear.SelectedItem.ToString() + "</div><br/>" +
                    "<div align='left' style='font-family:verdana;font-size:11px'><strong>Status:</strong>" + ddlStatus.SelectedItem.ToString() + "</div><br/>" + sw.ToString() + "<br/><br/><br/>" +

                    

                    "<div align='left'><span style='font-size:11px:font-weight:bold:padding-left:2px'></span></div>";
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


                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Student List Report</span><br/><br/><span style='font-size:13px:font-weight:bold'></span><br/><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + Session[ApplicationSession.SCHOOLNAME].ToString() + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Section :</strong>" + ddlsection.SelectedItem.ToString() + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Division:</strong>" + ddlclass.SelectedItem.ToString() + "-" + ddlDivision.SelectedItem.ToString() + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Year:</strong>" + ddlYear.SelectedItem.ToString() + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Status:</strong>" + ddlStatus.SelectedItem.ToString() + "</div><br/>" + sw.ToString() + "<br/><br/><br/><div align='left'><span style='font-size:11px:font-weight:bold:padding-left:2px'></span></div>";
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

        #region Header checkbox_CheckedChanged Event
        protected void chkHeader_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkHeader = new CheckBox();
                chkHeader = (CheckBox)gvParameter.HeaderRow.FindControl("chkHeader");
                if (chkHeader.Checked == true)
                {
                    CheckBox chk = new CheckBox();
                    foreach (GridViewRow rowItem in gvParameter.Rows)
                    {
                        chk = (CheckBox)(rowItem.Cells[2].FindControl("chk"));
                        chk.Checked = true;

                    }
                }
                else
                {
                    CheckBox chk = new CheckBox();
                    foreach (GridViewRow rowItem in gvParameter.Rows)
                    {
                        chk = (CheckBox)(rowItem.Cells[2].FindControl("chk"));
                        chk.Checked = false;

                    }
                }
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


        #region Report Button Event
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Client.UI/SchoolReports.aspx?Mode=SchoolGeneralReports");
        }
        #endregion
    }
}