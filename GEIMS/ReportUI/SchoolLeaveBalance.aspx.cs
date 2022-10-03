using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.Bl;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.Client.UI;
using GEIMS.Common;

namespace GEIMS.ReportUI
{
    public partial class SchoolLeaveBalance : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(FeeCollection));

        #region Pageload Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }
            if (!Page.IsPostBack)
            {
                try
                {
                    ddlSearchBy.SelectedIndex = 1;
                    ViewState["Mode"] = "Save";
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
        }
        #endregion

        #region Get Employee Web Service Method
        [System.Web.Services.WebMethod]
        public static string[] GetAllEmployeeNameForReport(string prefixText, int TrustMID, int SchoolMID)
        {
            UserTemplateTBl objEmployeeMbl = new UserTemplateTBl();
            ApplicationResult objResult = new ApplicationResult();
            string strSearchText = prefixText + "%";
            List<string> result = new List<string>();
            objResult = objEmployeeMbl.Employee_Select_AutocomleteForPayroll(strSearchText, TrustMID, SchoolMID);
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

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
        }
        #endregion

        #region Go button click event
        protected void btnGo_OnClick(object sender, EventArgs e)
        {
            LeaveBl objLeaveBl = new LeaveBl();
            var objResult =
                objLeaveBl.Leave_Select_ForBalance(Convert.ToInt32(hfEmployeeID.Value));
            if (objResult != null)
            {
                if (objResult.resultDT.Rows.Count > 0)
                {
                    gvLeaveBalance.DataSource = objResult.resultDT;
                    gvLeaveBalance.DataBind();
                    divgrid.Visible = true;
                }
                else
                {
                    ddlSearchBy.SelectedIndex = 1;
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Leave Template has not created.Please Create Leave Template first.');</script>");
                    divgrid.Visible = false;
                }
            }
        }
        #endregion
    }
}