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

namespace GEIMS.ReportUI
{
    public partial class StudentCategoryWise : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(StudentCategoryWise));

        #region Page_Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }
            if (!IsPostBack)
            {
                getStatusName();
                BindSchool();
                divReport.Visible = false;
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
            BindStudentList();
        }
        #endregion

        #region Method to bind School
        private void BindSchool()
        {
            SchoolBL objSchoolBL = new SchoolBL();
            Controls objControls = new Controls();
            ApplicationResult objResults = new ApplicationResult();

            objResults = objSchoolBL.School_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
            if (objResults != null)
            {
                if (objResults.resultDT.Rows.Count > 0)
                {
                    objControls.BindDropDown_ListBox(objResults.resultDT, ddlSchool, "SchoolNameEng", "SchoolMID");

                }
                ddlSchool.Items.Insert(0, new ListItem("-Select-", ""));

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

        #region Bind Section Name
        public void getSectionName()
        {
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            SectionBL objStatusBl = new SectionBL();

            objResult = objStatusBl.Section_SelectAll(Convert.ToInt32(ddlSchool.SelectedValue));
            if (objResult != null)
            {
                objControls.BindDropDown_ListBox(objResult.resultDT, ddlSection, "SectionName", "SectionMID");
                if (objResult.resultDT.Rows.Count > 0)
                {

                }

                ddlSection.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", ""));

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

                objResults = objSchoolBl.School_Select(Convert.ToInt32(ddlSchool.SelectedValue));

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

        #region Bind Student Gridview
        public void BindStudentList()
        {
           
                ApplicationResult objResult = new ApplicationResult();
                StudentBL objStudentBl = new StudentBL();

                objResult = objStudentBl.StudentDetail_ForCategoryWiseReport(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(ddlSchool.SelectedValue), ddlYear.SelectedItem.ToString(), Convert.ToInt32(ddlStatus.SelectedValue));
                if (objResult.resultDT.Rows.Count > 0)
                {
                    gvReport.DataSource = objResult.resultDT;
                    gvReport.DataBind();
                    gvReport1.DataSource = objResult.resultDT;
                    gvReport1.DataBind();

                    divReport.Visible = true;
                    //btnPrintDetail.Visible = true;
                    pnlStudentInfo.Visible = false;
                    lblSchool.Text = "નવચેતન અંગ્રેજી અને ગુજરાતી માધ્યમિક શાળા, ખરચ";
                    lblSchool1.Text = "નવચેતન અંગ્રેજી અને ગુજરાતી માધ્યમિક શાળા, ખરચ";
                    lblSection.Text = ddlSection.SelectedItem.ToString();
                    lblYear.Text = ddlYear.SelectedItem.ToString();
                    lblStatus.Text = ddlStatus.SelectedItem.ToString();
                    lblSection1.Text = ddlSection.SelectedItem.ToString();
                    lblYear1.Text = ddlYear.SelectedItem.ToString();
                    lblStatus1.Text = ddlStatus.SelectedItem.ToString();
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
        }
        #endregion

        #region SchoolChangeIndex Event
        protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAcademicYear();
            getSectionName();
        }
        #endregion

        protected void btnPrintDetail_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('divPrint');", true);
        }

        #region gridview row Created Event
        protected void gvReport_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell Cell_Header = new TableCell();

                Cell_Header = new TableCell();
                Cell_Header.Text = "ધોરણ અને વર્ગ સંખ્યા";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.ColumnSpan = 2;
                HeaderRow.Cells.Add(Cell_Header);

                Cell_Header = new TableCell();
                Cell_Header.Text = "અનુસૂચિત જાતિ";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.ColumnSpan = 2;
                HeaderRow.Cells.Add(Cell_Header);

                Cell_Header = new TableCell();
                Cell_Header.Text = "અનુસૂચિત જન જાતિ";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.ColumnSpan = 2;
                HeaderRow.Cells.Add(Cell_Header);

                Cell_Header = new TableCell();
                Cell_Header.Text = "બક્ષિપંચ";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.ColumnSpan = 2;
                HeaderRow.Cells.Add(Cell_Header);

                Cell_Header = new TableCell();
                Cell_Header.Text = "અન્ય";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.ColumnSpan = 2;
                HeaderRow.Cells.Add(Cell_Header);

                Cell_Header = new TableCell();
                Cell_Header.Text = "કુલ";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.ColumnSpan = 2;
                HeaderRow.Cells.Add(Cell_Header);

                gvReport.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }
        protected void gvReport1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell Cell_Header = new TableCell();

                Cell_Header = new TableCell();
                Cell_Header.Text = "ધોરણ અને વર્ગ સંખ્યા";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.ColumnSpan = 2;
                HeaderRow.Cells.Add(Cell_Header);

                Cell_Header = new TableCell();
                Cell_Header.Text = "અનુસૂચિત જાતિ";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.ColumnSpan = 2;
                HeaderRow.Cells.Add(Cell_Header);

                Cell_Header = new TableCell();
                Cell_Header.Text = "અનુસૂચિત જન જાતિ";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.ColumnSpan = 2;
                HeaderRow.Cells.Add(Cell_Header);

                Cell_Header = new TableCell();
                Cell_Header.Text = "બક્ષિપંચ";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.ColumnSpan = 2;
                HeaderRow.Cells.Add(Cell_Header);

                Cell_Header = new TableCell();
                Cell_Header.Text = "અન્ય";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.ColumnSpan = 2;
                HeaderRow.Cells.Add(Cell_Header);

                Cell_Header = new TableCell();
                Cell_Header.Text = "કુલ";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.ColumnSpan = 2;
                HeaderRow.Cells.Add(Cell_Header);

                gvReport1.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }
        #endregion

        #region Report Button Event
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Client.UI/TrustReports.aspx?Mode=StatutoryReports");
        }
        #endregion

    }
}