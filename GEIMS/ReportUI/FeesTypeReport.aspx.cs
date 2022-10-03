using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using log4net;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using GEIMS.DataAccess;

namespace GEIMS.ReportUI
{
    public partial class FeesTypeReport : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(FeesTypeReport));

        #region Page Load
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
                BindStatus();
                divReport.Visible = false;
                divButtons.Visible = false;
            }
        }
        #endregion


        #region Bind Class
        protected void BindClass()
        {
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            ClassBL objClassBl = new ClassBL();

            objResult = objClassBl.Class_SelectAll_ForDropDownNotSectionWise(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
            if (objResult != null)
            {
                gvClass.DataSource = objResult.resultDT;
                gvClass.DataBind();
            }
        }
        #endregion

        #region Bind Status Name
        public void BindStatus()
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

        #region Back Button Event
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
            //pnlStudentInfo.Visible = true;
            tabs1.Visible = true;
            divButtons.Visible = false;
        }
        #endregion

        #region Class Dropdown Selected Changed event
        //protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ApplicationResult objResult = new ApplicationResult();
        //    Controls objControls = new Controls();
        //    DivisionTBL objDivisionBl = new DivisionTBL();

        //    objResult = objDivisionBl.Division_SelectAll_ClassWise_ForDropDown(Convert.ToInt32(ddlclass.SelectedValue), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
        //    if (objResult != null)
        //    {
        //        objControls.BindDropDown_ListBox(objResult.resultDT, ddlDivision, "DivisionName", "DivisionTID");
        //        if (objResult.resultDT.Rows.Count > 0)
        //        {

        //        }

        //        ddlDivision.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", ""));

        //    }
        //}
        #endregion

        #region Go Button Event
        protected void btnGo_Click(object sender, EventArgs e)
        {
            FeesCollectionBL objFeesCollectionBl = new FeesCollectionBL();
            ApplicationResult objResult = new ApplicationResult();
            string strDvisionTIDs = string.Empty;

            for (int i = 0; i < gvClass.Rows.Count; i++)
            {
                GridView gvChildGrid = (GridView)gvClass.Rows[i].FindControl("gvChild");
                for (int j = 0; j < gvChildGrid.Rows.Count; j++)
                {
                    CheckBox chk = (CheckBox)gvChildGrid.Rows[j].FindControl("chkSelect");
                    if (chk.Checked == true)
                    {
                        strDvisionTIDs += gvChildGrid.Rows[j].Cells[0].Text + ",";
                    }

                }
            }
            // objResult = objFeesCollectionBl.Test(strDvisionTIDs.TrimEnd());
            objResult = objFeesCollectionBl.OutstandingFees_Report(
               Convert.ToInt32(Session[ApplicationSession.SCHOOLID]),
               strDvisionTIDs.TrimEnd(), ddlYear.SelectedItem.Text,
               Convert.ToInt32(ddlStatus.SelectedValue), txtFromDate.Text, txtToDate.Text);
            if (objResult != null)
            {
                gvReport.DataSource = null;

                if (objResult.resultDT.Rows.Count > 0)
                {

                    divReport.Visible = true;
                    gvReport.Visible = true;
                    divButtons.Visible = true;
                    //lblClassName.Text = ddlclass.SelectedItem.Text;
                    lblAYear.Text = ddlYear.SelectedItem.Text;
                    //lblDivision.Text = ddlDivision.SelectedItem.Text;
                    lblStatus.Text = ddlStatus.SelectedItem.Text;
                    //DataRow newrow1 = objResult.resultDT.NewRow();
                    //newrow1[1] = "Total";
                    //objResult.resultDT.Rows.Add(newrow1);
                    //int i = 0;
                    //foreach (DataColumn col in objResult.resultDT.Columns)
                    //{

                    //    if (i != 0 && i != 1 && i != 2 && i != 3 && i != 4)
                    //    {
                    //        object sumObject;
                    //        sumObject = objResult.resultDT.Compute("Sum([" + col.ColumnName + "])", "");
                    //        objResult.resultDT.Rows[objResult.resultDT.Rows.Count - 1][i] = sumObject;

                    //    }
                    //    i++;
                    //}

                    //pnlStudentInfo.Visible = false;
                    tabs1.Visible = false;

                }
                else
                {
                    ClearAll();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Records Found.');", true);
                }
            }

            gvReport.DataSource = objResult.resultDT;
            gvReport.DataBind();
        }
        #endregion



        #region Export PDF Button Click Event
        protected void btnExportPDF_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=OutStandingFeesReport.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvReport.AllowPaging = false;
                gvReport.RenderControl(hw);
                //string strTitle = "Office of The Commissioner of Police, Vadodara City";
                //string strSubTitle = lblTitle.Text;
                string Date = DateTime.UtcNow.AddHours(5.5).ToString();
                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Report : Outstanding Fees List</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + Session[ApplicationSession.SCHOOLNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Status :</strong>" + lblStatus.Text + "</span><br/><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>AcademicYear:</strong>" + lblAYear.Text + "</div><br/><br/>" + sw.ToString() + "<br/>";
                StringReader sr = new StringReader(content);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                Response.Write(pdfDoc);
                Response.End();
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical Problem. Contact to your Administrator.');</script>");
            }

        }
        #endregion

        #region Export Excel Button Click Event
        protected void btnExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.AddHeader("content-disposition", "attachment;filename=OutStandingFeesReport.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvReport.AllowPaging = false;
                gvReport.RenderControl(hw);
                //string strTitle = "Office of The Commissioner of Police, Vadodara City";
                //string strSubTitle = lblTitle.Text;
                string Date = DateTime.UtcNow.AddHours(5.5).ToString();
                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Report : Outstanding Fees List</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + Session[ApplicationSession.SCHOOLNAME].ToString() + "</span><br/><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Status :</strong>" + lblStatus.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>AcademicYear:</strong>" + lblAYear.Text + "</div><br/><br/>" + sw.ToString() + "<br/>";
                Response.Output.Write(content);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical Problem. Contact to your Administrator.');</script>");
            }
        }
        #endregion

        #region Export Word Button Click Event
        protected void btnExportWord_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.AddHeader("content-disposition", "attachment;filename=OutStandingFeesReport.doc");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-word ";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvReport.AllowPaging = false;
                gvReport.RenderControl(hw);
                //string strTitle = "Office of The Commissioner of Police, Vadodara City";
                //string strSubTitle = lblTitle.Text;
                string Date = DateTime.UtcNow.AddHours(5.5).ToString();
                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Report : Outstanding Fees List</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + Session[ApplicationSession.SCHOOLNAME].ToString() + "</span></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Status :</strong>" + lblStatus.Text + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>AcademicYear:</strong>" + lblAYear.Text + "</div><br/><br/>" + sw.ToString() + "<br/>";
                Response.Output.Write(content);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical Problem. Contact to your Administrator.');</script>");
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
            Response.Redirect("../Client.UI/SchoolReports.aspx?Mode=SchoolFeesReports");
        }
        #endregion

        #region gvClass OnRowDataBound Event
        protected void gvClass_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int intClassMID = Convert.ToInt32(e.Row.Cells[0].Text);
                GridView gvChild = (GridView)e.Row.FindControl("gvChild");

                DivisionTBL objDivisionTbl = new DivisionTBL();
                ApplicationResult objResult = new ApplicationResult();

                objResult = objDivisionTbl.Division_SelectAll_ClassWise(intClassMID,
                    Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    gvChild.DataSource = objResult.resultDT;
                    gvChild.DataBind();
                }
            }
        }
        #endregion

        protected void gvReport_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //e.Row.Cells[0].Visible = false;
        }
    }
}