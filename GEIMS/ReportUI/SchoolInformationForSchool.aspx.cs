using System;
using System.IO;
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
using System.Web.Script.Serialization;

namespace GEIMS.ReportUI
{
    public partial class SchoolInformationForSchool : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(SchoolInformationForSchool));
        #region PageLoad Event
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    btnPrintDetail.Visible = false;
                    btnBack1.Visible = false;
                    BindEmployeeDataList();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Go button Event
        protected void btnGo_Click(object sender, EventArgs e)
        {
            BindEmployeeDataList();
            //  btnPrintDetail.Visible = true;
        }
        #endregion

        #region Bind Employee DataList
        public void BindEmployeeDataList()
        {
            ApplicationResult objResult = new ApplicationResult();
            SchoolBL objSchoolBl = new SchoolBL();

            objResult = objSchoolBl.SchoolM_Select_InformationForReport(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
            dlSchool.DataSource = objResult.resultDT;
            dlSchool.DataBind();
            dlSchool1.DataSource = objResult.resultDT;
            dlSchool1.DataBind();
            if (objResult.resultDT.Rows.Count > 0)
            {
                divSchool.Visible = true;
                btnPrintDetail.Visible = true;
                btnBack1.Visible = true;
            }
            else
            {
                divSchool.Visible = false;
                btnPrintDetail.Visible = false;
                btnBack1.Visible = false;
                ClearAll();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Records Found.');", true);
            }
        }
        #endregion


        #region Back Button
        protected void btnBack_Click(object sender, EventArgs e)
        {
            divSchool.Visible = false;
            btnPrintDetail.Visible = false;
            ClearAll();
        }
        #endregion

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
        }
        #endregion

        protected void btnPrintDetail_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('divSchool1');", true);
        }

    }
}