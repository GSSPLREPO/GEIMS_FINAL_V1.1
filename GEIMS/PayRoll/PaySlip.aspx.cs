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

namespace GEIMS.PayRoll
{
    public partial class PaySlip : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(PaySlip));

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
                    ClearAll();
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
            PaySlipBl objPaySlipBl = new PaySlipBl();
            ApplicationResult objResult = new ApplicationResult();

            objResult = objPaySlipBl.Select_EmployeeDetail_ForPaySlipPrint(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(hfEmployeeMID.Value),  Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue),0);
            if (objResult != null)
            {
                dlPaySlip.DataSource = null;
                dlPaySlip1.DataSource = null;
                dlPaySlipOffice.DataSource = null;

                dlPaySlip.DataSource = objResult.resultDT;
                dlPaySlip1.DataSource = objResult.resultDT;
                dlPaySlipOffice.DataSource = objResult.resultDT;
                dlPaySlip.DataBind();
                dlPaySlip1.DataBind();
                dlPaySlipOffice.DataBind();
                if (objResult.resultDT.Rows.Count > 0)
                {
                    dlPaySlip.Visible = true;
                    dlPaySlip1.Visible = true;
                    dlPaySlipOffice.DataSource = null;
                    divReport.Visible = true;
                    pnlEmployeePayrollInfo.Visible = false;
                    btnPrintDetail.Visible = true;
                }
                else
                {
                    ClearAll();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Record Found.');", true);
                }
            }

        }
        #endregion

        #region Print button Event
        protected void btnPrintDetail_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('divReport1');", true);
        }
        #endregion

        #region button Go Event
        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmployeeName.Text != "" && ddlMonth.SelectedValue != "" && ddlYear.SelectedValue != "-1")
                {
                    BindgvReport();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Select All Parameters.');", true);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
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

        #region Back button Event
        protected void btnBack_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        #endregion

        #region DataList ItemData Bound Event
        protected void dlPaySlip_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    PaySlipBl objPaySlipBl = new PaySlipBl();
                    ApplicationResult objResult = new ApplicationResult();

                    GridView gvEarning = new GridView();
                    gvEarning = (GridView)e.Item.FindControl("gvEarning");
                    GridView gvDeduction = new GridView();
                    gvDeduction = (GridView)e.Item.FindControl("gvDeduction");
                    GridView gvLeave = new GridView();
                    gvLeave = (GridView)e.Item.FindControl("gvLeave");
                    Label lblMonth = (Label)e.Item.FindControl("lblMonth");
                    lblMonth.Text = ddlMonth.SelectedItem.ToString() + " " + ddlYear.SelectedItem.ToString();
                    Label lblAmountInWords = (Label)e.Item.FindControl("lblAmountInWords");
                    CommonFunctions objFuction = new CommonFunctions();
                    string strAmount  = objFuction.ConvertInWords(Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "NetSalary").ToString()));
                    lblAmountInWords.Text = Convert.ToString(strAmount);
                     objResult = objPaySlipBl.Select_EmployeeDetail_ForPaySlipPrint(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(hfEmployeeMID.Value),  Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue),1);
                     if (objResult != null)
                     {
                         gvEarning.DataSource = null;
                         //gvReport.DataSource = null;
                         gvEarning.DataSource = objResult.resultDT;
                         gvEarning.DataBind();
                         if (objResult.resultDT.Rows.Count > 0)
                         {
                             gvEarning.Visible = true;
                         }
                     }
                     objResult = objPaySlipBl.Select_EmployeeDetail_ForPaySlipPrint(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(hfEmployeeMID.Value), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), 2);
                     if (objResult != null)
                     {
                         gvDeduction.DataSource = null;
                         //gvReport.DataSource = null;
                         gvDeduction.DataSource = objResult.resultDT;
                         gvDeduction.DataBind();
                         if (objResult.resultDT.Rows.Count > 0)
                         {
                             gvDeduction.Visible = true;
                         }
                     }
                     objResult = objPaySlipBl.Select_EmployeeDetail_ForPaySlipPrint(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(hfEmployeeMID.Value), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), 3);
                     if (objResult != null)
                     {
                         gvLeave.DataSource = null;
                         //gvReport.DataSource = null;
                         gvLeave.DataSource = objResult.resultDT;
                         gvLeave.DataBind();
                         if (objResult.resultDT.Rows.Count > 0)
                         {
                             gvLeave.Visible = true;
                         }
                     }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region DataList ItemData Bound Event
        protected void dlPaySlip1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    PaySlipBl objPaySlipBl = new PaySlipBl();
                    ApplicationResult objResult = new ApplicationResult();

                    GridView gvEarning = new GridView();
                    gvEarning = (GridView)e.Item.FindControl("gvEarning1");
                    GridView gvDeduction = new GridView();
                    gvDeduction = (GridView)e.Item.FindControl("gvDeduction1");
                    GridView gvLeave = new GridView();
                    gvLeave = (GridView)e.Item.FindControl("gvLeave1");
                    Label lblMonth = (Label)e.Item.FindControl("lblMonth1");
                    lblMonth.Text = ddlMonth.SelectedItem.ToString() + " " + ddlYear.SelectedItem.ToString();
                    Label lblAmountInWords = (Label)e.Item.FindControl("lblAmountInWords1");
                    CommonFunctions objFuction = new CommonFunctions();
                    string strAmount = objFuction.ConvertInWords(Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "NetSalary").ToString()));
                    lblAmountInWords.Text = Convert.ToString(strAmount);
                    objResult = objPaySlipBl.Select_EmployeeDetail_ForPaySlipPrint(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(hfEmployeeMID.Value), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), 1);
                    if (objResult != null)
                    {
                        gvEarning.DataSource = null;
                        //gvReport.DataSource = null;
                        gvEarning.DataSource = objResult.resultDT;
                        gvEarning.DataBind();
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            gvEarning.Visible = true;
                        }
                    }
                    objResult = objPaySlipBl.Select_EmployeeDetail_ForPaySlipPrint(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(hfEmployeeMID.Value), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), 2);
                    if (objResult != null)
                    {
                        gvDeduction.DataSource = null;
                        //gvReport.DataSource = null;
                        gvDeduction.DataSource = objResult.resultDT;
                        gvDeduction.DataBind();
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            gvDeduction.Visible = true;
                        }
                    }
                    objResult = objPaySlipBl.Select_EmployeeDetail_ForPaySlipPrint(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(hfEmployeeMID.Value), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), 3);
                    if (objResult != null)
                    {
                        gvLeave.DataSource = null;
                        //gvReport.DataSource = null;
                        gvLeave.DataSource = objResult.resultDT;
                        gvLeave.DataBind();
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            gvLeave.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region DataList ItemData Bound Event
        protected void dlPaySlipOffice_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    PaySlipBl objPaySlipBl = new PaySlipBl();
                    ApplicationResult objResult = new ApplicationResult();

                    GridView gvEarning = new GridView();
                    gvEarning = (GridView)e.Item.FindControl("gvEarning12");
                    GridView gvDeduction = new GridView();
                    gvDeduction = (GridView)e.Item.FindControl("gvDeduction12");
                    GridView gvLeave = new GridView();
                    gvLeave = (GridView)e.Item.FindControl("gvLeave12");
                    Label lblMonth = (Label)e.Item.FindControl("lblMonth12");
                    lblMonth.Text = ddlMonth.SelectedItem.ToString() + " " + ddlYear.SelectedItem.ToString();
                    Label lblAmountInWords = (Label)e.Item.FindControl("lblAmountInWords12");
                    CommonFunctions objFuction = new CommonFunctions();
                    string strAmount = objFuction.ConvertInWords(Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "NetSalary").ToString()));
                    lblAmountInWords.Text = Convert.ToString(strAmount);
                    objResult = objPaySlipBl.Select_EmployeeDetail_ForPaySlipPrint(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(hfEmployeeMID.Value), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), 1);
                    if (objResult != null)
                    {
                        gvEarning.DataSource = null;
                        //gvReport.DataSource = null;
                        gvEarning.DataSource = objResult.resultDT;
                        gvEarning.DataBind();
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            gvEarning.Visible = true;
                        }
                    }
                    objResult = objPaySlipBl.Select_EmployeeDetail_ForPaySlipPrint(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(hfEmployeeMID.Value), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), 2);
                    if (objResult != null)
                    {
                        gvDeduction.DataSource = null;
                        //gvReport.DataSource = null;
                        gvDeduction.DataSource = objResult.resultDT;
                        gvDeduction.DataBind();
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            gvDeduction.Visible = true;
                        }
                    }
                    objResult = objPaySlipBl.Select_EmployeeDetail_ForPaySlipPrint(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(hfEmployeeMID.Value), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), 3);
                    if (objResult != null)
                    {
                        gvLeave.DataSource = null;
                        //gvReport.DataSource = null;
                        gvLeave.DataSource = objResult.resultDT;
                        gvLeave.DataBind();
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            gvLeave.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region BtnBackToreport Event
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Client.UI/TrustReports.aspx?Mode=PayRollReports");
        }
        #endregion

        

        protected void dlPaySlip_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}