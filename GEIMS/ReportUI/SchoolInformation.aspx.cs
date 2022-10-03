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

namespace GEIMS.Reports
{
    public partial class SchoolInformation : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(SchoolInformation));
        #region PageLoad Event
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("../UserLogin.aspx");
                }
                if (!IsPostBack)
                {
                    getSchoolName();
                    btnPrintDetail.Visible = false;
                    btnBack1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Bind SchoolName
        public void getSchoolName()
        {
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            SchoolBL objSchoolBl = new SchoolBL();

            objResult = objSchoolBl.School_SelectAll_ForDropDOwn(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
            if (objResult != null)
            {
                if (objResult.resultDT.Rows.Count > 0)
                {
                    objControls.BindDropDown_ListBox(objResult.resultDT, ddlSchoolName, "SchoolNameEng", "SchoolMID");

                }
                ddlSchoolName.Items.Insert(0, new ListItem("--Select--", ""));
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

            objResult = objSchoolBl.SchoolM_Select_InformationForReport(Convert.ToInt32(ddlSchoolName.SelectedValue));
            dlSchool.DataSource = objResult.resultDT;
            dlSchool.DataBind();
            dlSchool1.DataSource = objResult.resultDT;
            dlSchool1.DataBind();
            if (objResult.resultDT.Rows.Count > 0)
            {
                divSchool.Visible = true;
                pnlSchoolInfo.Visible = false;
                btnPrintDetail.Visible = true;
                btnBack1.Visible = true;
            }
            else
            {
                divSchool.Visible = false;
                btnPrintDetail.Visible = false;
                pnlSchoolInfo.Visible = true;
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
            pnlSchoolInfo.Visible = true;
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

        #region Print Button Event
        protected void btnPrintDetail_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('divSchool1');", true);
        }
        #endregion


        #region Report Button Event
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Client.UI/TrustReports.aspx?Mode=GeneralReports");
        }
        #endregion

    }
}