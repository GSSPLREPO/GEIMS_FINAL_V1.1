using System;
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

namespace GEIMS.Accounting
{
    public partial class AccountLogin : PageBase
    {
        #region declaration
        private static ILog logger = LogManager.GetLogger(typeof(AccountLogin));
        Controls objControl = new Controls();
        DataTable dtGridData = new DataTable();
        int intEndingYear, intCurrentYear;
        #endregion

        #region Pre-Init Method
        protected void Page_PreInit(object sender, EventArgs e)
        {
            //if (Request.QueryString["mode"] == "TU")
            //{
            //    MasterPageFile = "~/Master/TrustMain.Master";
            //}
        }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Request.QueryString["mode"] == "TU")
                //    Session[ApplicationSession.SCHOOLID] = 0;
                txtTo.Enabled = true;
                txtFrom.Attributes.Add("readonly", "readonly");
            }
        }
        #endregion

        #region Login Button
        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            try
            {
                string strFromDate = txtFrom.Text;
                string strToDate = txtTo.Text;
                string LastTwoDigit, strYear;
                int intNo;
                LastTwoDigit = strToDate.Substring(strToDate.Length - 2);
                intNo = Convert.ToInt32(LastTwoDigit) - 1;
                strYear = intNo.ToString() + LastTwoDigit;

                LockBL objLockBl = new LockBL();
                LockBo objLockBo = new LockBo();
                ApplicationResult objResult = new ApplicationResult();

                if (chkLock.Checked == true)
                {
                    objLockBo.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                    objLockBo.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                    objLockBo.FromDate = strFromDate;
                    objLockBo.ToDate = strToDate;
                    objLockBo.LockYear = Convert.ToInt32(strYear);

                    objResult = objLockBl.Lock_Insert(objLockBo);
                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {

                    }
                }
                else
                {
                    objResult = objLockBl.Lock_SelectbyYear(Convert.ToInt32(strYear));
                    if (objResult != null)
                    {
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('The Data of financial year " + strYear + " has been locked.');</script>");
                        }
                        else
                        {
                            EmployeeMBL objEmployeeMBL = new EmployeeMBL();
                            EmployeeMBO objEmployeeMBO = new EmployeeMBO();
                            ApplicationResult objResultValidate = new ApplicationResult();
                            int temp = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                            objResultValidate = objEmployeeMBL.Employee_CheckForLoginAccount(txtUserName.Text, txtPassword.Text,
                                Convert.ToInt32(Session[ApplicationSession.TRUSTID]), 0);
                               // Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                            if (objResultValidate != null)
                            {
                                DataTable dtResult = new DataTable();
                                dtResult = objResultValidate.resultDT;
                                if (dtResult.Rows.Count > 0)
                                {
                                    if (Convert.ToInt32(Session[ApplicationSession.USERID]) ==
                                        Convert.ToInt32(dtResult.Rows[0]["EmployeeMID"]))
                                    {
                                        Session[ApplicationSession.HASACCESSACCOUNTUSERID] =
                                            Convert.ToInt32(dtResult.Rows[0]["EmployeeMID"]);
                                        Session[ApplicationSession.ACCOUNTFROMDATE] = txtFrom.Text;
                                        Session[ApplicationSession.ACCOUNTTODATE] = txtTo.Text;
                                        Session[ApplicationSession.FINANCIALYEAR] = strYear;

                                        Response.Redirect(
                                            Convert.ToInt32(Session[ApplicationSession.SCHOOLID]) == 0
                                                ? "../Accounting/Home.aspx?mode=TU"
                                                : "../Accounting/Home.aspx", false);
                                        //Response.Redirect("../Client.UI/BudgetScreenEntry.aspx?Parameter=" + strFromDate);
                                        //Response.Redirect("../Client.UI/BudgetScreenEntry.aspx?Parameter=" + Server.UrlEncode(strFromDate));
                                    }
                                    else
                                    {
                                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                            "<script>alert('Invalid username & password.');</script>");
                                    }
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                        "<script>alert('You do not have permission to access this module.');</script>");
                                }
                            }
                            else
                            {

                            }
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

        #region TextChange Event
        protected void txtFrom_TextChanged(object sender, EventArgs e)
        {
            if (txtFrom.Text == "" || txtFrom.Text == "&nbsp;")
            {
                lblMessage.Text = "Select date First";
            }
            else
            {
                int mon = Convert.ToInt32(txtFrom.Text.Substring(3, 2));
                int Year = Convert.ToInt32(txtFrom.Text.Substring(6));

                if (mon <= 3)
                {
                    intEndingYear = Year;
                    txtTo.Text = System.DateTime.Today.ToString("31/03/" + intEndingYear.ToString());
                    ViewState["TODate31"] = txtTo.Text;
                }
                else
                {
                    intCurrentYear = Year + 1;
                    txtTo.Text = System.DateTime.Today.ToString("31/03/" + intCurrentYear.ToString());
                    ViewState["TODate31"] = txtTo.Text;
                }
                txtUserName.Focus();
            }
        }
        #endregion
    }
}