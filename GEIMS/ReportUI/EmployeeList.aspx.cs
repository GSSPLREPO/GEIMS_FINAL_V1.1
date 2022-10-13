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
    public partial class EmployeeList : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(EmployeeList));

        #region PageLoad Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }
            if (!IsPostBack)
            {
                getDesignationName();
                getDepartmentName();
                getEmployeeRoleName();
                divReport.Visible = false;
                BindParameterGrid();
            }
        }
        #endregion

        #region Bind Designation Name
        public void getDesignationName()
        {
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            DesignationBL objDesignationBL = new DesignationBL();

            objResult = objDesignationBL.Designation_SelectAll_ForDropDown();
            if (objResult != null)
            {
                objControls.BindDropDown_ListBox(objResult.resultDT, ddlDesignation, "DesignationNameENG", "DesignationID");
                if (objResult.resultDT.Rows.Count > 0)
                {
                }
                ddlDesignation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select All--", "-1"));
            }
        }
        #endregion

        #region Bind Department Name
        public void getDepartmentName()
        {
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            DepartmentBL objDepartmentBL = new DepartmentBL();

            objResult = objDepartmentBL.Department_Select_By_Trust_School_ForDropDown(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
            if (objResult != null)
            {
                objControls.BindDropDown_ListBox(objResult.resultDT, ddlDepartment, "DepartmentNameENG", "DepartmentID");
                if (objResult.resultDT.Rows.Count > 0)
                {
                }
                ddlDepartment.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select All--", "-1"));
            }
        }
        #endregion

        #region Bind Employee Role
        public void getEmployeeRoleName()
        {
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            RoleBL objRoleBL = new RoleBL();

            objResult = objRoleBL.Role_SelectAll_ForDropDown();
            if (objResult != null)
            {
                objControls.BindDropDown_ListBox(objResult.resultDT, ddlEmployeeRole, "RoleName", "RoleID");
                if (objResult.resultDT.Rows.Count > 0)
                {
                }
                ddlEmployeeRole.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select All--", "-1"));
            }
        }
        #endregion

        //public void FetchImage()
        //{
        //    try
        //    {
        //        #region Declaretion
        //        TrustBL objTrustBl = new TrustBL();
        //        DataTable dtTrust = new DataTable();
        //        #endregion
        //        ApplicationResult objResultsEdit = new ApplicationResult();
        //        objResultsEdit = objTrustBl.Trust_Select(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));

        //        if (objResultsEdit != null)
        //        {
        //            dtTrust = objResultsEdit.resultDT;
        //            if (dtTrust.Rows.Count > 0)
        //            {

        //                ViewState["Bytes"] = dtTrust.Rows[0][TrustBO.TRUST_TRUSTLOGO];
        //                if (ViewState["Bytes"].ToString() != "")
        //                {
        //                    ViewState["Image"] = "../Client.UI/GetImage.ashx?TrustMID=" + Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
        //                }
        //                ViewState["Bytes"] = null;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //DisplayErrorMsg("CommonError", ex);
        //    }
        //}

        #region Bind Parameter Grid
        //Following parameeters will be shown in grid view to select.
        public void BindParameterGrid()
        {
            DataTable dtParameters = new DataTable();
            dtParameters.Columns.Add("SrNo");
            dtParameters.Columns.Add("Fields");
            dtParameters.Rows.Add("1", "Sr No.");
            dtParameters.Rows.Add("2", "Employee Code");
            dtParameters.Rows.Add("3", "Department Name");
            dtParameters.Rows.Add("4", "Designation Name");
            dtParameters.Rows.Add("5", "Employee Role Name");
            dtParameters.Rows.Add("6", "Employee Name");
            dtParameters.Rows.Add("7", "Employee Name (Gujarati)");
            dtParameters.Rows.Add("8", "Gender");
            dtParameters.Rows.Add("9", "Gender (Gujarati)");
            dtParameters.Rows.Add("10", "Date Of Birth");
            dtParameters.Rows.Add("11", "Birth District");
            dtParameters.Rows.Add("12", "Birth District (Gujarati)");
            dtParameters.Rows.Add("13", "Birth Taluka");
            dtParameters.Rows.Add("14", "Birth Taluka(Gujarati)");
            dtParameters.Rows.Add("15", "Birth CityVillage");
            dtParameters.Rows.Add("16", "Birth CityVillage (Gujarati)");
            dtParameters.Rows.Add("17", "Nationality");
            dtParameters.Rows.Add("18", "Nationality (Gujarati)");
            dtParameters.Rows.Add("19", "Religion");
            dtParameters.Rows.Add("20", "Religion (Gujarati)");
            dtParameters.Rows.Add("21", "Caste");
            dtParameters.Rows.Add("22", "Marital Status");
            dtParameters.Rows.Add("23", "Blood Group");
            dtParameters.Rows.Add("24", "Mother Language");
            dtParameters.Rows.Add("25", "Current Address");
            dtParameters.Rows.Add("26", "Current Address (Gujarati)");
            dtParameters.Rows.Add("27", "Current LandMark");
            dtParameters.Rows.Add("28", "Current LandMark (Gujarati)");
            dtParameters.Rows.Add("29", "Current City");
            dtParameters.Rows.Add("30", "Current City (Gujarati)");
            dtParameters.Rows.Add("31", "Current State");
            dtParameters.Rows.Add("32", "Current State (Gujarati)");
            dtParameters.Rows.Add("33", "Current PinCode");
            dtParameters.Rows.Add("34", "Permanent Address");
            dtParameters.Rows.Add("35", "Permanent Address (Gujarati)");
            dtParameters.Rows.Add("36", "Permanent LandMark");
            dtParameters.Rows.Add("37", "Permanent LandMark (Gujarati)");
            dtParameters.Rows.Add("38", "Permanent City");
            dtParameters.Rows.Add("39", "Permanent City (Gujarati)");
            dtParameters.Rows.Add("40", "Permanent State");
            dtParameters.Rows.Add("41", "Permanent State (Gujarati)");
            dtParameters.Rows.Add("42", "Permanent PinCode");
            dtParameters.Rows.Add("43", "TelephoneNo");
            dtParameters.Rows.Add("44", "MobileNo");
            dtParameters.Rows.Add("45", "EmailId");
            dtParameters.Rows.Add("46", "Hobbies");
            dtParameters.Rows.Add("47", "RightVision");
            dtParameters.Rows.Add("48", "LeftVision");
            dtParameters.Rows.Add("49", "Rectification Device");
            dtParameters.Rows.Add("50", "Height");
            dtParameters.Rows.Add("51", "Weight");
            dtParameters.Rows.Add("52", "Physical Identification");
            dtParameters.Rows.Add("53", "Physical Identification (Gujarati)");
            dtParameters.Rows.Add("54", "Mother Maiden Name");
            dtParameters.Rows.Add("55", "Mother Maiden Name (Gujarati)");
            dtParameters.Rows.Add("56", "Bank Name");
            dtParameters.Rows.Add("57", "Branch Name");
            dtParameters.Rows.Add("58", "Branch Code");
            dtParameters.Rows.Add("59", "AccountNo");
            dtParameters.Rows.Add("60", "PFNo");
            dtParameters.Rows.Add("61", "PANNo");
            dtParameters.Rows.Add("62", "ESICNo");
            dtParameters.Rows.Add("63", "IFSCCode");
            dtParameters.Rows.Add("64", "GPFAccountNo");
            dtParameters.Rows.Add("65", "CPFAccountNo");
            dtParameters.Rows.Add("66", "Department Joining Date");
            dtParameters.Rows.Add("67", "Organisation Joining Date");
            dtParameters.Rows.Add("68", "Type Of Appointment");
            dtParameters.Rows.Add("69", "School Replacement Info");
            dtParameters.Rows.Add("70", "School Replacement Info (Gujarati)");
            dtParameters.Rows.Add("71", "Retirement Date");
            dtParameters.Rows.Add("72", "TermEnd Retirement Date");
            dtParameters.Rows.Add("73", "Resigned Date");
            dtParameters.Rows.Add("74", "Break Info");
            dtParameters.Rows.Add("75", "Break Info (Gujarati)");
            dtParameters.Rows.Add("76", "Resign Reason");
            dtParameters.Rows.Add("77", "Resign Reason (Gujarati)");
            dtParameters.Rows.Add("78", "Achievement Detail");
            dtParameters.Rows.Add("79", "Achievement Detail (Gujarati)");
            dtParameters.Rows.Add("80", "UserType");
            dtParameters.Rows.Add("81", "UserName");
            dtParameters.Rows.Add("82", "password");
            dtParameters.Rows.Add("83", "Teacher");
            dtParameters.Rows.Add("84", "Principal");
            dtParameters.Rows.Add("85", "AllowAccountAccess");
            dtParameters.Rows.Add("86", "Category");
            dtParameters.Rows.Add("87", "Category (Gujarati)");
            dtParameters.Rows.Add("88", "Aadhar Card No.");
           

            gvParameter.DataSource = dtParameters;
            gvParameter.DataBind();
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

        #region Go button Click Event
        protected void btnGo_Click(object sender, EventArgs e)
        {
            //if (hfTab.Value == "0")
            //{
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
            BindEmployeeList();
            //FetchImage();
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Select Parameter.');", true);
            //}

        }
        #endregion

        #region Exportpdf button Click Event
        protected void btnExportPDF_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
               // string strPath = "../Client.UI/GetImage.ashx?TrustMID=" + Convert.ToInt32(Session[ApplicationSession.TRUSTID]);

                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                Response.AddHeader("content-disposition", "attachment;filename=EmployeeList" + "_" + DateTime.Now.Date.ToString("yyyy-MM-dd") +".pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvReport.AllowPaging = false;
                //gvReport.DataBind();
                gvReport.RenderControl(hw);

                // string content = <div style='width:100% ;border-bottom:1px solid Black;'><div style='width:15%; float:left;padding-left:10px'><img src='"+ strPath +"' alt='logo' /></div><div style='width:85%; float:right;font-size:18px; padding-top:10px;'>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</div></div>
                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Report : Employee List</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Department :</strong>" + lblDepartmenmt.Text + "</span><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Designation:</strong>" + lblDesignation.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Role:</strong>" + lblRole.Text + "</div><br/><br/>" + sw.ToString() + "<br/>";
                // Response.Output.Write(content);

                StringReader sr = new StringReader(content);
                Document pdfDoc = new Document(PageSize.A3, 10f, 10f, 10f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                Response.Write(pdfDoc);
                	HttpContext.Current.ApplicationInstance.CompleteRequest();
               // Response.End();


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

                Response.AddHeader("content-disposition", "attachment;filename= EmployeeList" + "_" + Session[ApplicationSession.TRUSTNAME] + "_" + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".xlsx");
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
                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Report : Employee List</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Department :</strong>" + lblDepartmenmt.Text + "</span><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Designation:</strong>" + lblDesignation.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Role:</strong>" + lblRole.Text + "</div><br/><br/>" + sw.ToString() + "<br/>";
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

                Response.AddHeader("content-disposition", "attachment;filename=EmployeeList" + "_" + DateTime.Now.Date.ToString("yyyy-MM-dd") +".doc");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-word ";
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                gvReport.AllowPaging = false;
                // gvReport.DataBind();
                gvReport.RenderControl(hw);

                //   string content1 = "";
                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Report : Employee List</span><br/><br/><span style='font-size:13px:font-weight:bold'></span><br/><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Department :</strong>" + lblDepartmenmt.Text + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Designation:</strong>" + lblDesignation.Text + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Role:</strong>" + lblRole.Text + "</div><br/>" + sw.ToString() + "<br/><br/><br/><div align='left'><span style='font-size:11px:font-weight:bold:padding-left:2px'></span></div>";
                Response.Output.Write(content);

                Response.Flush();
                	//HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.End();
            }
           catch (System.Threading.ThreadAbortException lException)
            {
                // logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion

        #region Back Button Click
        protected void btnBack_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        #endregion

        #region Bind Employee DataList
        public void BindEmployeeList()
        {

            string[] FieldstoDisplay;
            FieldstoDisplay = ViewState["Feilds"].ToString().Split(',');

            ApplicationResult objResult = new ApplicationResult();
            EmployeeMBL objEmployeeBl = new EmployeeMBL();

            objResult = objEmployeeBl.EmployeeM_Select_ForList(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlDesignation.SelectedValue), Convert.ToInt32(ddlEmployeeRole.SelectedValue));
            if (objResult.resultDT.Rows.Count > 0)
            {
                for (int i = 88; i > 0; i--)
                {
                    if (FieldstoDisplay.Contains(i.ToString()) == false)
                        objResult.resultDT.Columns.Remove(objResult.resultDT.Columns[i - 1]);
                }
                gvReport.DataSource = objResult.resultDT;
                gvReport.DataBind();

                divReport.Visible = true;
                //btnPrintDetail.Visible = true;
                pnlEmployeeInfo.Visible = false;
                lblTrust.Text = Session[ApplicationSession.TRUSTNAME].ToString();

                if (ddlDepartment.SelectedValue == "-1")
                {
                    lblDepartmenmt.Text = "All";
                }
                else
                {
                    lblDepartmenmt.Text = ddlDepartment.SelectedItem.ToString();
                }
                if (ddlDesignation.SelectedValue == "-1")
                {
                    lblDesignation.Text = "All";
                }
                else
                {
                    lblDesignation.Text = ddlDesignation.SelectedItem.ToString();
                }
                if (ddlEmployeeRole.SelectedValue == "-1")
                {
                    lblRole.Text = "All";
                }
                else
                {
                    lblRole.Text = ddlEmployeeRole.SelectedItem.ToString();
                }

            }
            else
            {
                divReport.Visible = false;
                // btnPrintDetail.Visible = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Records Found.');", true);
                pnlEmployeeInfo.Visible = true;
                ClearAll();
            }
        }
        #endregion

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            divReport.Visible = false;
            pnlEmployeeInfo.Visible = true;
            BindParameterGrid();
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
            Response.Redirect("../Client.UI/TrustReports.aspx?Mode=GeneralReports");
        }
        #endregion
    }
}