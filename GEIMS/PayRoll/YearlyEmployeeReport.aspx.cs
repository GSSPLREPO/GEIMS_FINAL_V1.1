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
using System.Data;

namespace GEIMS.PayRoll
{
    public partial class YearlyEmployeeReport : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(YearlyEmployeeReport));

        #region PageLoad Event
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("../UserLogin.aspx");
                }
                if (!Page.IsPostBack)
                {
                    bindYear();
                    divReport.Visible = false;
                    hfTrustMID.Value = Session[ApplicationSession.TRUSTID].ToString();
                    hfSchoolMID.Value = Session[ApplicationSession.SCHOOLID].ToString();
                    btnPrintDetail.Visible = false;
                    pnlEmployeePayrollInfo.Visible = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
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
            ddlYear.Items.Insert(0, new ListItem("-Select-", "-1"));
        }
        #endregion

        #region Bind GridView
        public void BindgvReport()
        {
            int intIsApproved = 0;
            if(chkApproved.Checked == true)
            {
                intIsApproved = 1;
            }
            else{
                 intIsApproved = 0;
            }
            PaySlipBl objPaySlipBl = new PaySlipBl();
            ApplicationResult objResult = new ApplicationResult();

            //string SchoolMId = (Session[ApplicationSession.SCHOOLID]).ToString();

            objResult = objPaySlipBl.Select_EmployeePayroll_Yearly((Session[ApplicationSession.TRUSTID]).ToString(), (Session[ApplicationSession.SCHOOLID]).ToString(),
                hfEmployeeMID.Value, ddlYear.SelectedValue, intIsApproved);
            if (objResult != null)
            {
                gvReport1.DataSource = null;
                gvReport.DataSource = null;

                if (objResult.resultDT.Rows.Count > 0)
                {
                    gvReport1.Visible = true;
                    gvReport.Visible = true;
                    btnPrintDetail.Visible = false;
                    lblTrustName.Text = "Fertilizer Nagar English and Gujarati Medium School.";
                    lblYear.Text = ddlYear.SelectedItem.Text;
                    lblName.Text = txtEmployeeName.Text;
                    DataRow newrow1 = objResult.resultDT.NewRow();
                    newrow1[1] = "Total";
                    objResult.resultDT.Rows.Add(newrow1);
                    int i = 0;
                    foreach (DataColumn col in objResult.resultDT.Columns)
                    {

                        if (i != 0 && i != 1 && i != 2 && i != 3)
                        {
                            object sumObject;
                            sumObject = objResult.resultDT.Compute("Sum([" + col.ColumnName + "])", "");
                            objResult.resultDT.Rows[objResult.resultDT.Rows.Count - 1][i] = sumObject;

                        }
                        i++;
                    }


                }
                else
                {
                   
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Record Found.');", true);
                    ClearAll();
                }
            }

            gvReport1.DataSource = objResult.resultDT;
            gvReport.DataSource = objResult.resultDT;
            gvReport.DataBind();
            gvReport1.DataBind();
            gvReport1.Visible = true;
            gvReport.Visible = true;
            divReport.Visible = true;

        }
        #endregion

        #region Print Button Event
        protected void btnPrintDetail_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('divReport1');", true);
            lblTrust.Text = "Fertilizer Nagar English and Gujarati Medium School.";
            lblYear1.Text = ddlYear.SelectedItem.Text;
            lblName1.Text = txtEmployeeName.Text;
        }
        #endregion

        #region Go button Click Event
        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmployeeName.Text != "" && ddlYear.SelectedValue != "-1")
                {
                    BindgvReport();
                    
                    //pnlEmployeePayrollInfo.Visible = false;
                    //lblErrMsg.Visible = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Select All Parameters.');", true);
                 
                }
                dvReport.Visible = true;
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region back Button Event
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
            pnlEmployeePayrollInfo.Visible = true;
            btnPrintDetail.Visible = false;
        }
        #endregion

        #region report gridview row data bound Event
        protected void gvReport_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
        }
        #endregion

        #region report gridview row data bound Event
        protected void gvReport1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
        }
        #endregion

        #region BtnBackToreport Event
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Client.UI/TrustReports.aspx?Mode=PayRollReports");
        }
        #endregion
    }
}