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
using System.Web.Script.Serialization;

namespace GEIMS.ReportUI
{
    public partial class TeacherWiseTimeTable : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(TimeTableTeacherWise));
        #region  pageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("../UserLogin.aspx");
                }
                if (!IsPostBack)
                {
                    divtimeTable.Visible = true;
                    divReport.Visible = false;
                    btnPrintDetail.Visible = false;
                    btnBack1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Bind Time Table
        public void BindTimeTable()
        {
            ApplicationResult objResult = new ApplicationResult();
            TimeTableBL objSchoolBL = new TimeTableBL();

            objResult = objSchoolBL.TeacherWise_TimeTable(Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString()), Convert.ToInt32(hfEmployeeMID.Value));
            if (objResult.resultDT.Rows.Count > 0)
            {
                gvReport.DataSource = objResult.resultDT;
                gvReport.DataBind();
                gvReport1.DataSource = objResult.resultDT;
                gvReport1.DataBind();

                divReport.Visible = true;
                divtimeTable.Visible = false;
                btnPrintDetail.Visible = true;
                btnBack1.Visible = true;
                //btnPrintDetail.Visible = true;
                lblTrust.Text = Session[ApplicationSession.TRUSTNAME].ToString();
                lblSchool.Text = Session[ApplicationSession.SCHOOLNAME].ToString();
                lblName.Text = txtEmployeeName.Text;
            }
            else
            {
                divReport.Visible = false;
                divtimeTable.Visible = true;
                btnPrintDetail.Visible = false;
                btnBack1.Visible = false;
                ClearAll();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Records Found.');", true);
            }
        }
        #endregion

        #region btnPrintDetail
        protected void btnPrintDetail_Click(object sender, EventArgs e)
        {
            divReport.Visible = true;
            divtimeTable.Visible = false;
            BindTimeTable();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('divReport1');", true);
        }
        #endregion

        #region Get Employee Web Service Method
        [System.Web.Services.WebMethod]
        public static string[] GetAllEmployeeNameForReport(string prefixText, int TrustMID, int SchoolMID)
        {
            EmployeeMBL objEmployeeMbl = new EmployeeMBL();
            ApplicationResult objResult = new ApplicationResult();
            string strSearchText = prefixText + "%";
            List<string> result = new List<string>();
            objResult = objEmployeeMbl.EmployeeM_Select_ForAutoComplete(strSearchText, TrustMID, SchoolMID);
            if (objResult != null)
            {
                for (int i = 0; i < objResult.resultDT.Rows.Count; i++)
                {
                    string strEmployeeCodeName = objResult.resultDT.Rows[i]["EmployeeName"].ToString();
                    string strEmployeeMID = objResult.resultDT.Rows[i][EmployeeMBO.EMPLOYEEM_EMPLOYEEMID].ToString();
                    result.Add(string.Format("{0}~{1}", strEmployeeCodeName, strEmployeeMID));
                }
            }
            return result.ToArray();
        }

        #endregion

        #region btnGo
        protected void btnGo_Click(object sender, EventArgs e)
        {
            if (hfEmployeeMID.Value != "")
            {
                divReport.Visible = true;
                divtimeTable.Visible = false;
                BindTimeTable();
            }
            else
            {
                divReport.Visible = false;
                divtimeTable.Visible = true;
                btnPrintDetail.Visible = false;
                btnBack1.Visible = false;
                ClearAll();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Records Found.');", true);
            }
        }
        #endregion

        #region btnBack Click
        protected void btnBack_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
        }
        #endregion


        #region Report Button Event
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Client.UI/SchoolReports.aspx?Mode=SchoolTimeTableReports");
        }
        #endregion
    }
}