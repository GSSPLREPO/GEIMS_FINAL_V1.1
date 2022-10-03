using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace GEIMS.Client.UI
{
    public partial class AdmissionFormEng : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TxtFormNo.Text = Convert.ToString(Session["FormNo"]);
         
           

         

            // Response.Write("<script> window.print('" + "AdmissionFormEng.aspx" + "','_blank'); </script>");
        }

        protected void TxtFormNo_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            BtnPrint.Visible = false;

            Response.Write("<script> window.print('" + "AdmissionFormEng.aspx" + "','_blank'); </script>");
        }
    }
}