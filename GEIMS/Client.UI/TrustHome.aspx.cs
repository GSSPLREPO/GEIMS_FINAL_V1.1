using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using System.IO;
using log4net;

namespace GEIMS.Client.UI
{
    public partial class TrustHome : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(TrustHome));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session[ApplicationSession.SCHOOLID] = 0;
                FetchImage();
                FetchLabel();
            }
        }

        #region Bind Label
        private void FetchLabel()
        {
            try
            {
                #region Declaretion
                TrustBL objTrustBl = new TrustBL();
                DataTable dtTrust = new DataTable();
                #endregion
                ApplicationResult objResultsEdit = new ApplicationResult();
                objResultsEdit = objTrustBl.Trust_Select(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));

                if (objResultsEdit != null)
                {
                    dtTrust = objResultsEdit.resultDT;
                    if (dtTrust.Rows.Count > 0)
                    {
                        lblTrustName.Text = dtTrust.Rows[0][TrustBO.TRUST_TRUSTNAMEENG].ToString();
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

        #region Fetch Image
        public void FetchImage()
        {
            try
            {
                #region Declaretion
                TrustBL objTrustBl = new TrustBL();
                ApplicationResult objResultsEdit = new ApplicationResult();
                #endregion
               
                objResultsEdit = objTrustBl.Trust_Select(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));

                if (objResultsEdit != null)
                {
                    if (objResultsEdit.resultDT.Rows.Count > 0)
                    {

                        ViewState["Bytes"] = objResultsEdit.resultDT.Rows[0][TrustBO.TRUST_TRUSTLOGO];
                        if (ViewState["Bytes"].ToString() != "")
                        {
                            imgphoto.ImageUrl = "../Client.UI/GetImage.ashx?TrustMID=" + Convert.ToInt32(Session[ApplicationSession.TRUSTID]);

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
    }
}