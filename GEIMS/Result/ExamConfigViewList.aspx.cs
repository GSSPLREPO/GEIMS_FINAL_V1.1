using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.Client.UI;
using GEIMS.Common;

namespace GEIMS.Result
{
    public partial class ExamConfigViewList : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(StudentDetailMaster));
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnExam_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExamConfiguration.aspx");
        }


        protected void gvExamConfig_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
        }
    }
}