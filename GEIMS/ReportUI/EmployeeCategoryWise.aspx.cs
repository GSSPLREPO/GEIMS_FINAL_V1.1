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
    public partial class EmployeeCategoryWise : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(SchoolEmployeeCategoryWise));

        #region Page_Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }
            if (!IsPostBack)
            {
                divReport.Visible = false;
                BindStudentList();
            }
        }
        #endregion

        #region Back Button Event
        protected void btnBack_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        #endregion

        //#region Go button Event
        //protected void btnGo_Click(object sender, EventArgs e)
        //{
        //    BindStudentList();
        //}
        //#endregion

        #region Bind Student Gridview
        public void BindStudentList()
        {


            ApplicationResult objResult = new ApplicationResult();
            EmployeeMBL objEmployeeBl = new EmployeeMBL();

            objResult = objEmployeeBl.EmployeeMDetail_ForCategoryWiseReport(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
            if (objResult.resultDT.Rows.Count > 0)
            {
                gvReport.DataSource = objResult.resultDT;
                gvReport.DataBind();
                gvReport1.DataSource = objResult.resultDT;
                gvReport1.DataBind();

                divReport.Visible = true;
                //btnPrintDetail.Visible = true;
                //pnlStudentInfo.Visible = false;
                lblSchoolName.Text = "નવચેતન અંગ્રેજી અને ગુજરાતી માધ્યમિક શાળા, ખરચ";
                lblSchoolName1.Text = "નવચેતન અંગ્રેજી અને ગુજરાતી માધ્યમિક શાળા, ખરચ";
            }
            else
            {
                divReport.Visible = false;
                // btnPrintDetail.Visible = false;
                // pnlStudentInfo.Visible = true;
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
            //pnlStudentInfo.Visible = true;
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

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Client.UI/TrustReports.aspx?Mode=StatutoryReports");
        }
    }
}