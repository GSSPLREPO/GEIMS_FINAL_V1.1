using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GEIMS.Common;

namespace GEIMS.Result
{
    public partial class FinalResult : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            tblStudent.Visible = false;
            divSave.Visible = false;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            tblStudent.Visible = true;
            divSave.Visible = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            tblStudent.Visible = false;
            divSave.Visible = false;
        }
    }
}