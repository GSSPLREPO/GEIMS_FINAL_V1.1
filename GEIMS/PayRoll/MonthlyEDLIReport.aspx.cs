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
using System.Drawing;

namespace GEIMS.PayRoll
{
    public partial class MonthlyEDLIReport : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(MonthlyEDLIReport));
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
                //gvParameter.Columns[0].Visible = false;
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
                Response.AddHeader("content-disposition", "attachment;filename=EDLIReport" + "_" + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvReport.AllowPaging = false;
                //gvReport.DataBind();
                gvReport.RenderControl(hw);

                // string content = <div style='width:100% ;border-bottom:1px solid Black;'><div style='width:15%; float:left;padding-left:10px'><img src='"+ strPath +"' alt='logo' /></div><div style='width:85%; float:right;font-size:18px; padding-top:10px;'>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</div></div>
                //string content = "<div align='center' style='font-family:verdana;font-size:13px'></br><span style='font-size:16px:font-weight:bold;color:Maroon;'>E.D.L.I. Report</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><br/>" + sw.ToString() + "<br/><strong>";
                string content = "<div align='center' style='font-family:verdana;font-size:13px'>" +
                    "</br>" +
                    "<span style='font-size:16px:font-weight:bold;color:Maroon;'>FERTILIZER NAGAR SCHOOL</span>" +
                    "</br>" +
                    "<span style='font-size:13px:font-weight:bold'></span>" +
                    "<br/>" +
                    "<span align='center' style='font-family:verdana;font-size:11px'><strong>E.D.L.I</strong></span>" +
                    "<br/>" +
                    "<span align='center' style='font-family:verdana;font-size:11px'><strong> Month:</strong>" +ddlMonth.SelectedItem.ToString() + "-" +ddlYear.Text + "</span>" +
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

                int count = 0;
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                string filename = "EDLI Report" + "_" + Session[ApplicationSession.TRUSTNAME] + "_" + DateTime.Now.ToString("dd/MM/yyyy") + "_" + DateTime.Now.ToString("HH:mm:ss") + ".xls";

                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    StringWriter sw1 = new StringWriter();
                    HtmlTextWriter hw1 = new HtmlTextWriter(sw1);

                    //To Export all pages
                    gvReport.AllowPaging = false;

                    gvReport.HeaderRow.BackColor = Color.White;

                    foreach (TableCell cell in gvReport.HeaderRow.Cells)
                    {
                        cell.BackColor = gvReport.HeaderStyle.BackColor;
                        count++;
                    }
                    foreach (GridViewRow row in gvReport.Rows)
                    {
                        row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = gvReport.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = gvReport.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                            cell.HorizontalAlign = HorizontalAlign.Center;
                            List<Control> controls = new List<Control>();

                            //Add controls to be removed to Generic List
                            foreach (Control control in cell.Controls)
                            {
                                controls.Add(control);
                            }

                            //Loop through the controls to be removed and replace then with Literal
                            foreach (Control control in controls)
                            {
                                switch (control.GetType().Name)
                                {
                                    case "HyperLink":
                                        cell.Controls.Add(new Literal { Text = (control as HyperLink).Text });
                                        break;
                                    case "TextBox":
                                        cell.Controls.Add(new Literal { Text = (control as TextBox).Text });
                                        break;
                                    case "LinkButton":
                                        cell.Controls.Add(new Literal { Text = (control as LinkButton).Text });
                                        break;
                                    case "CheckBox":
                                        cell.Controls.Add(new Literal { Text = (control as CheckBox).Text });
                                        break;
                                    case "RadioButton":
                                        cell.Controls.Add(new Literal { Text = (control as RadioButton).Text });
                                        break;
                                }
                                cell.Controls.Remove(control);
                            }
                        }
                    }
                    int colh, cold;
                    colh = count - 4;
                    cold = count - 8;

                    gvReport.RenderControl(hw);


                    string strSubTitle = "Report : E.D.L.I Report ";
                    string strPath = Request.Url.GetLeftPart(UriPartial.Authority) + "/images/Logo1.jpg";

                    string content = "<div align='center' style='font-family:verdana;font-size:16px; width:800px;'>" +
                  "<table style='display: table; width: 800px; clear:both;'>" +
                  "<tr> </tr>" +
                  "<tr><th></th><th colspan='1' style='text-align:left'><img height='100' width='100' src='" + strPath + "'/></th>" +
                  "<th colspan='" + colh + "' style='width: 600px; float: left; font-weight:bold;font-size:16px;color:Maroon;'>" + strSubTitle + "</tr>" +
                     "<tr><th colspan='2'></th><th colspan='" + colh + "' style='font-size:13px;font-weight:bold;color:Black;'>Month :" + ddlMonth.SelectedItem.Text
                     + "&nbsp;&nbsp; Year :" + ddlYear.SelectedItem.Text + "</th></tr>" +
                     "<tr><th colspan='2'></th><th colspan='" + colh + "'></th></tr>" +
                     "<tr><th colspan='2'></th><th colspan='" + colh + "' style='font-size:13px;'><b>Trust Name :" + Session[ApplicationSession.TRUSTNAME] + "</b></th></tr>" +
                     "<tr></tr>" +
                "</table>" +

                      "<br/>" + sw.ToString() + "<br/></div>";

                    string style = @"<!--mce:2-->";
                    Response.Write(style);
                    Response.Output.Write(content);
                    gvReport.GridLines = GridLines.None;
                    Response.Flush();
                    Response.Clear();
                    Response.End();
                }
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
                Response.Clear();
                Response.Buffer = true;

                int count = 0;
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                string filename = "EDLI Report" + "_" + Session[ApplicationSession.TRUSTNAME] + "_" + DateTime.Now.ToString("dd/MM/yyyy") + "_" + DateTime.Now.ToString("HH:mm:ss") + ".doc";

                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    StringWriter sw1 = new StringWriter();
                    HtmlTextWriter hw1 = new HtmlTextWriter(sw1);

                    //To Export all pages
                    gvReport.AllowPaging = false;

                    gvReport.HeaderRow.BackColor = Color.White;

                    foreach (TableCell cell in gvReport.HeaderRow.Cells)
                    {
                        cell.BackColor = gvReport.HeaderStyle.BackColor;
                        count++;
                    }
                    foreach (GridViewRow row in gvReport.Rows)
                    {
                        row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = gvReport.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = gvReport.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                            cell.HorizontalAlign = HorizontalAlign.Center;
                            List<Control> controls = new List<Control>();

                            //Add controls to be removed to Generic List
                            foreach (Control control in cell.Controls)
                            {
                                controls.Add(control);
                            }

                            //Loop through the controls to be removed and replace then with Literal
                            foreach (Control control in controls)
                            {
                                switch (control.GetType().Name)
                                {
                                    case "HyperLink":
                                        cell.Controls.Add(new Literal { Text = (control as HyperLink).Text });
                                        break;
                                    case "TextBox":
                                        cell.Controls.Add(new Literal { Text = (control as TextBox).Text });
                                        break;
                                    case "LinkButton":
                                        cell.Controls.Add(new Literal { Text = (control as LinkButton).Text });
                                        break;
                                    case "CheckBox":
                                        cell.Controls.Add(new Literal { Text = (control as CheckBox).Text });
                                        break;
                                    case "RadioButton":
                                        cell.Controls.Add(new Literal { Text = (control as RadioButton).Text });
                                        break;
                                }
                                cell.Controls.Remove(control);
                            }
                        }
                    }
                    int colh, cold;
                    colh = count - 4;
                    cold = count - 8;

                    gvReport.RenderControl(hw);


                    string strSubTitle = "Report : E.D.L.I Report ";
                    string strPath = Request.Url.GetLeftPart(UriPartial.Authority) + "/images/Logo1.jpg";

                    string content = "<div align='center' style='font-family:verdana;font-size:16px; width:800px;'>" +
                  "<table style='display: table; width: 800px; clear:both;'>" +
                  "<tr> </tr>" +
                  "<tr><th></th><th colspan='1' style='text-align:left'><img height='100' width='100' src='" + strPath + "'/></th>" +
                  "<th colspan='" + colh + "' style='width: 600px; float: left; font-weight:bold;font-size:16px;color:Maroon;'>" + strSubTitle + "</tr>" +
                     "<tr><th colspan='2'></th><th colspan='" + colh + "' style='font-size:13px;font-weight:bold;color:Black;'>Month :" + ddlMonth.SelectedItem.Text
                     + "&nbsp;&nbsp; Year :" + ddlYear.SelectedItem.Text + "</th></tr>" +
                     "<tr><th colspan='2'></th><th colspan='" + colh + "'></th></tr>" +
                     "<tr><th colspan='2'></th><th colspan='" + colh + "' style='font-size:13px;'><b>Trust Name :" + Session[ApplicationSession.TRUSTNAME] + "</b></th></tr>" +
                     "<tr></tr>" +
                "</table>" +

                      "<br/>" + sw.ToString() + "<br/></div>";

                    string style = @"<!--mce:2-->";
                    Response.Write(style);
                    Response.Output.Write(content);
                    gvReport.GridLines = GridLines.None;
                    Response.Flush();
                    Response.Clear();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
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

            //string[] FieldstoDisplay;
            //FieldstoDisplay = ViewState["Feilds"].ToString().Split(',');






            ApplicationResult objResult = new ApplicationResult();
            EmployeeBL objEmployeeBl = new EmployeeBL();

            objResult = objEmployeeBl.EDLI_Report(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), 0);
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
        /// back to menu button click (Added comment on 07/10/2022 Bhandavi)
        ///  Redirect to School Payroll reports main page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReport_Click(object sender, EventArgs e)
        {
            //Response.Redirect("../Client.UI/TrustReports.aspx?Mode=GeneralReports");
            //Changed on 07/10/2022 Bhandavi
            //redirecting to trust reports page, need to redirect to school payroll reports page.
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
            // objResult = objEmployeeBl.Employee_PFRegister(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), 1, Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
            objResult = objEmployeeBl.EDLI_Report(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), 0);

            if (objResult != null)
            {
                gvReport.DataSource = null;
                lblTrustName.Text = "Fertilizer Nagar English and Gujarati Medium School.";
                lblYear.Text = ddlYear.SelectedItem.Text;
                lblMonth.Text = ddlMonth.SelectedItem.Text;

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