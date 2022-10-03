using System;

namespace GEIMS.Client.UI
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("../UserLogin.aspx");
        }
    }
}