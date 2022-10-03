using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.ReportUI
{
    public partial class CompactFirstReport : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(FeesTypeReport));
        public override void VerifyRenderingInServerForm(Control control) { return; }


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

            //objResult = objFeesCollectionBl.Compact_FirstReport(
            //    Convert.ToInt32(Session[ApplicationSession.SCHOOLID]),
            //    strDvisionTIDs.TrimEnd(), txtFromDate.Text, txtToDate.Text ,ddlYear.SelectedItem.Text, Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
            objResult = objFeesCollectionBl.Compact_Report(strDvisionTIDs.TrimEnd(), txtToDate.Text, ddlYear.SelectedItem.Text, Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(Session[ApplicationSession.TRUSTID]),txtFromDate.Text);

            
            if (objResult != null)
            {
                gvReport.DataSource = null;

                if (objResult.resultDT.Rows.Count > 0)
                {

                    divReport.Visible = true;
                    gvReport.Visible = true;
                    divButtons.Visible = true;
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
            lblFromDate.Text = txtFromDate.Text;
            lblToDate.Text = txtToDate.Text;

        }
        #endregion

        protected void gvReport_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

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

        #region Report Button Event
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Client.UI/SchoolReports.aspx?Mode=SchoolFeesReports");
        }
        #endregion


        #region Export Excel Button Click Event
        protected void btnExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.AddHeader("content-disposition", "attachment;filename=CompactReport.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvReport.AllowPaging = false;
                gvReport.RenderControl(hw);
                //string strTitle = "Office of The Commissioner of Police, Vadodara City";
                //string strSubTitle = lblTitle.Text;
                string Date = DateTime.UtcNow.AddHours(5.5).ToString();
                // string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Report : Compact Report</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + Session[ApplicationSession.SCHOOLNAME].ToString() + "</span><br/><br/><span align='center' style='font-family:verdana;font-size:11px'></span><br/><span align='center' style='font-family:verdana;font-size:11px'></div><br/><br/>" + sw.ToString() + "<br/>";
                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Report : Outstanding Fees List</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + Session[ApplicationSession.SCHOOLNAME].ToString() + "</span><br/><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>AcademicYear: "+ddlYear.SelectedItem+"</strong></div><br/><br/>" + sw.ToString() + "<br/>";
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
    }
}