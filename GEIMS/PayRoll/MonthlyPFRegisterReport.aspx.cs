using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.DataAccess;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

namespace GEIMS.PayRoll
{
    public partial class MonthlyPFRegisterReport : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(MonthlyPFRegisterReport));
        #region PageLoad Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }
            if (!IsPostBack)
            {
                //getDesignationName();
                // getDepartmentName();
                //getEmployeeRoleName();
                bindYear();
                divReport.Visible = false;
               // BindParameterGrid();
            }
        }
        #endregion

        #region Bind Year
        public void bindYear()
        {
            string[] strYear;
            int intacYear = 0;
            #region Get Accounting Start Date
            ApplicationResult objResults = new ApplicationResult();
            TrustBL objTrustBl = new TrustBL();

            objResults = objTrustBl.Trust_Select(Convert.ToInt32(Session[ApplicationSession.TRUSTID].ToString()));

            if (objResults != null)
            {
                if (objResults.resultDT.Rows.Count > 0)
                {
                    string strACStartDate = objResults.resultDT.Rows[0][TrustBO.TRUST_ACCOUNTSTARTDATE].ToString();
                    strYear = strACStartDate.ToString().Split(new char[] { '/' });
                    intacYear = Convert.ToInt32(strYear[2]);
                }

            }
            #endregion


            for (int i = intacYear; i <= DateTime.Now.Year; i++)
            {
                ddlYear.Items.Add(i.ToString());
            }
            ddlYear.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-Select-", "-1"));
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
        public void BindParameterGrid()
        {
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            EmployeeBL objEmployeeBL = new EmployeeBL();

            objResult = objEmployeeBL.Employee_Select_ForSalarySummary(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
            if (objResult != null)
            {
                gvParameter.Visible = true;
                divSelectEmp.Visible = true;
                gvParameter.DataSource = objResult.resultDT;
                gvParameter.DataBind();
                if (objResult.resultDT.Rows.Count > 0)
                {


                    foreach (GridViewRow row in gvParameter.Rows)
                    {
                        for (int i = 0; i < objResult.resultDT.Rows.Count; i++)
                        {
                            if (row.Cells[0].Text ==
                                objResult.resultDT.Rows[i]["EmployeeMID"].ToString())
                            {
                                //((CheckBox)row.FindControl("chkChild")).Checked = true;

                            }
                        }
                    }
                }
              //  gvParameter.Columns[0].Visible = false;
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
                Response.AddHeader("content-disposition", "attachment;filename=EmployeePFRegister" + "_" + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvReport.AllowPaging = false;
                //gvReport.DataBind();
                gvReport.RenderControl(hw);

                // string content = <div style='width:100% ;border-bottom:1px solid Black;'><div style='width:15%; float:left;padding-left:10px'><img src='"+ strPath +"' alt='logo' /></div><div style='width:85%; float:right;font-size:18px; padding-top:10px;'>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</div></div>
                //string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Report : Employee Monthly PF Register</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><br/><br/>" + sw.ToString() + "<br/><strong>";
                string content = "<div align='center' style='font-family:verdana;font-size:13px'>" +
                   "</br>" +
                   "<span style='font-size:16px:font-weight:bold;color:Maroon;'>FERTILIZER NAGAR SCHOOL</span>" +
                   "</br>" +
                   "<span style='font-size:13px:font-weight:bold'></span>" +
                   "<br/>" +
                   "<span align='center' style='font-family:verdana;font-size:11px'><strong>P.F. REGISTER</strong></span>" +
                   "<br/>" +
                   "<span align='center' style='font-family:verdana;font-size:11px'><strong> Month:</strong>" + ddlMonth.SelectedItem.ToString() + "-" + ddlYear.Text + "</span>" +
                   "<br/>" +
                   "<span align='center' style='font-family:verdana;font-size:11px'><br/>" + sw.ToString() + "<br/><strong>";
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

                Response.AddHeader("content-disposition", "attachment;filename=MonthlyPFRegister" + "_" + Session[ApplicationSession.TRUSTNAME] + "_" + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".xls");
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
                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Report : Monthly PF Register</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><br/>" + sw.ToString() + "<br/><strong>";
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

                Response.AddHeader("content-disposition", "attachment;filename=EmployeeList" + "_" + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".doc");
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
                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Report : Employee List</span><br/><br/><span style='font-size:13px:font-weight:bold'></span><br/><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</div><br/><div align='center' style='font-family:verdana;font-size:11px'></div><br/><br/>" + sw.ToString() + "<br/><strong></span></div>";
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
        //on clear button click
        protected void btnBack_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        #endregion

        #region Bind Employee DataList
        public void BindEmployeeList()
        {

            //string[] FieldstoDisplay;
            //FieldstoDisplay = ViewState["Feilds"].ToString().Split(',');






            ApplicationResult objResult = new ApplicationResult();
            EmployeeBL objEmployeeBl = new EmployeeBL();

            objResult = objEmployeeBl.Employee_SalarySummary(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), 0);
            if (objResult.resultDT.Rows.Count > 0)
            {
                //for (int i = 87; i > 0; i--)
                //{
                //    if (FieldstoDisplay.Contains(i.ToString()) == false)
                //        objResult.resultDT.Columns.Remove(objResult.resultDT.Columns[i - 1]);
                //}

                //dlReport.DataSource = null;
                //dlReport.DataSource = objResult.resultDT;
                //dlReport.DataBind();
                gvReport.DataSource = objResult.resultDT;
                gvReport.DataBind();

                divReport.Visible = true;
                //btnPrintDetail.Visible = true;
                pnlEmployeeInfo.Visible = false;
                //lblTrust.Text = Session[ApplicationSession.TRUSTNAME].ToString();

              

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

        /// <summary>
        /// On back to menu button click
        /// Back to payroll reports in school portal
        /// (Added comment on 07 10 2022 Bhandavi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReport_Click(object sender, EventArgs e)
        {
            //Response.Redirect("../Client.UI/TrustReports.aspx?Mode=GeneralReports");
            //Previously redirecting to trust reports page (Need to redirect to school reports page)
            //Changed code to redirect on 07 10 2022 Bhandavi
            Response.Redirect("../Client.UI/SchoolReports.aspx?Mode=SchoolPayrollReports");
           
        }
        #endregion

        #region Bind GridView
        public void BindgvReport()
        {
            //PaySlipBl objPaySlipBl = new PaySlipBl();
            //ApplicationResult objResult = new ApplicationResult();

            //objResult = objPaySlipBl.Select_EmployeeDetail_ForPaySlipPrint(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), 65, Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), 0);
            ApplicationResult objResult = new ApplicationResult();
            EmployeeBL objEmployeeBl = new EmployeeBL();
            objResult = objEmployeeBl.Employee_PFRegister(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), 1,Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));

            if (objResult != null)
            {
                gvReport.DataSource = null;
              

                gvReport.DataSource = objResult.resultDT;
               
                gvReport.DataBind();
                
                if (objResult.resultDT.Rows.Count > 0)
                {
                    divReport.Visible = true;
                    gvReport.Visible = true;
                    

                    pnlEmployeeInfo.Visible = false;
                    //btnPrintDetail.Visible = true;
                }
                else
                {
                    ClearAll();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Record Found.');", true);
                }
            }

        }
        #endregion

        protected void BtnFind_Click(object sender, EventArgs e)
        {
            BindParameterGrid();
            gvParameter.Columns[0].Visible = false;
            BtnFind.Visible = false;
        }

        #region Go button Click Event
        protected void btnGo_Click1(object sender, EventArgs e)
        {
            try
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

                string[] splitStrings = strFields.Split(","[0]);

                var StringCount = splitStrings.Length.ToString();



                string[] FieldstoDisplay;
                FieldstoDisplay = ViewState["Feilds"].ToString().Split(',');

                for (int i = 0; i < Convert.ToInt32(StringCount); ++i)
                {

                    ApplicationResult objResult = new ApplicationResult();
                    EmployeeBL objEmployeeBl = new EmployeeBL();

                    objResult = objEmployeeBl.Insert_Employee_TempTable(Convert.ToInt32(FieldstoDisplay[i]));

                }


                //if (txtEmployeeName.Text != "" && ddlMonth.SelectedValue != "" && ddlYear.SelectedValue != "-1")
                //{
                BindgvReport();
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Select All Parameters.');", true);
                //}
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnFind.Visible = true;
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnFind.Visible = true;
        }
    }
}