using System;
using System.Data;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using log4net;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace GEIMS.Client.UI
{
    public partial class SchoolHome : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(SchoolHome));
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
                
            try
            {  
                #region Declaretion
                SchoolBL objSchoolBl = new SchoolBL();
                DataTable dtSchool = new DataTable();
                #endregion
                ApplicationResult objResultsEdit = new ApplicationResult();
                objResultsEdit = objSchoolBl.School_Select(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));

                if (objResultsEdit != null)
                {
                    dtSchool = objResultsEdit.resultDT;
                    if (dtSchool.Rows.Count > 0)
                    {
                        lblSchoolName.Text = dtSchool.Rows[0][SchoolBO.SCHOOL_SCHOOLNAMEENG].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
        }
    }
}