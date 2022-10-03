using System;
using System.Web.UI;
using GEIMS.Common;

namespace GEIMS.Accounting
{
    public partial class Home : PageBase
    {
        #region Pre-Init Method
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.QueryString["mode"] == "TU")
            {
                MasterPageFile = "~/Master/TrustMain.Master";
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            lblDuration.Text = Request.QueryString["mode"] == "TU"
                ? Session[ApplicationSession.TRUSTNAME] + ". Account Duration : " +
                  Session[ApplicationSession.ACCOUNTFROMDATE] + " To " + Session[ApplicationSession.ACCOUNTTODATE]
                : (Convert.ToInt32(Session[ApplicationSession.SCHOOLID]) == 0
                    ? Session[ApplicationSession.TRUSTNAME] + ". Account Duration : " +
                      Session[ApplicationSession.ACCOUNTFROMDATE] + " To " + Session[ApplicationSession.ACCOUNTTODATE]
                    : Session[ApplicationSession.SCHOOLNAME] + ". Account Duration : " +
                      Session[ApplicationSession.ACCOUNTFROMDATE] + " To " + Session[ApplicationSession.ACCOUNTTODATE]);
        }
    }
}