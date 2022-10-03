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
    public partial class SchoolEmpoyeeInformationReport : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(SchoolEmpoyeeInformationReport));
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
                    btnPrintDetail.Visible = false;
                    btnBack1.Visible = false;
                    divEmployee.Visible = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion


        #region Get Employee Web Service Method
        [System.Web.Services.WebMethod]
        public static string[] GetAllEmployeeNameForReport(string prefixText, int TrustMID, int SchoolMID)
        {
            EmployeeMBL objEmployeeMbl = new EmployeeMBL();
            ApplicationResult objResult = new ApplicationResult();
            string strSearchText = prefixText + "%";
            List<string> result = new List<string>();
            objResult = objEmployeeMbl.EmployeeM_Select_ForAutoComplete(strSearchText, TrustMID, SchoolMID);
            if (objResult != null)
            {
                for (int i = 0; i < objResult.resultDT.Rows.Count; i++)
                {
                    string strEmployeeCodeName = objResult.resultDT.Rows[i]["EmployeeName"].ToString();
                    string strEmployeeMID = objResult.resultDT.Rows[i][EmployeeMBO.EMPLOYEEM_EMPLOYEEMID].ToString();
                    result.Add(string.Format("{0}~{1}", strEmployeeCodeName, strEmployeeMID));
                }
            }
            return result.ToArray();
        }

        #endregion

        #region Go button Event
        protected void btnGo_Click(object sender, EventArgs e)
        {
            if (hfEmployeeMID.Value == "")
            {
                btnPrintDetail.Visible = false;
                pnlEmployeeInfo.Visible = true;
                btnBack1.Visible = false;
                ClearAll();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Records Found.');", true);
            }
            if (txtEmployeeName.Text != "")
            {
                if (txtEmployeeName.Text == hfEmployeeCodeName.Value)
                {
                    BindEmployeeDataList();
                    //  btnPrintDetail.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "alert('Enter Wrong Name.'),AutoComplete();", true);
                    //  ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "AutoComplete();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "alert('Enter Employee Name.'),AutoComplete();", true);
                //   ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "AutoComplete();", true);
            }
        }
        #endregion

        #region Bind Employee DataList
        public void BindEmployeeDataList()
        {
            ApplicationResult objResult = new ApplicationResult();
            EmployeeMBL objEmployeeBl = new EmployeeMBL();

            objResult = objEmployeeBl.EmployeeM_Select_InformationForReport(Convert.ToInt32(hfEmployeeMID.Value));
            dlEmployee.DataSource = objResult.resultDT;
            dlEmployee.DataBind();
            dlEmployee1.DataSource = objResult.resultDT;
            dlEmployee1.DataBind();
            if (objResult.resultDT.Rows.Count > 0)
            {
                divEmployee.Visible = true;
                //divEmployee1.Visible = true;
                pnlEmployeeInfo.Visible = false;
                btnPrintDetail.Visible = true;
                btnBack1.Visible = true;
            }
            else
            {
                divEmployee.Visible = false;
                btnPrintDetail.Visible = false;
                pnlEmployeeInfo.Visible = true;
                btnBack1.Visible = false;
                ClearAll();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Records Found.');", true);
            }
        }
        #endregion

        #region Datalist dlEmployee ItemBound
        protected void dlEmployee_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    GridView gvEducationalDetail = new GridView();
                    gvEducationalDetail = (GridView)e.Item.FindControl("gvEducationalDetail");
                    GridView gvExperience = new GridView();
                    gvExperience = (GridView)e.Item.FindControl("gvExperience");

                    ApplicationResult objResults = new ApplicationResult();
                    EmployeeTBL objEmployeeTBl = new EmployeeTBL();

                    objResults = objEmployeeTBl.EmpoyeeQualificationT_SelectForReport(Convert.ToInt32((hfEmployeeMID.Value)));

                    if (objResults != null)
                    {
                        gvEducationalDetail.DataSource = objResults.resultDT;
                        gvEducationalDetail.DataBind();
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            gvEducationalDetail.Visible = true;
                        }
                        else
                        {
                            gvEducationalDetail.Visible = false;
                        }
                    }

                    objResults = objEmployeeTBl.EmployeeExpirenceT_SelectForReport(Convert.ToInt32((hfEmployeeMID.Value)));

                    if (objResults != null)
                    {
                        gvExperience.DataSource = objResults.resultDT;
                        gvExperience.DataBind();

                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            gvExperience.Visible = true;
                        }
                        else
                        {
                            gvExperience.Visible = false;
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

        #region Datalist dlEmployee ItemBound
        protected void dlEmployee1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    GridView gvEducationalDetail1 = new GridView();
                    gvEducationalDetail1 = (GridView)e.Item.FindControl("gvEducationalDetail1");
                    GridView gvExperience1 = new GridView();
                    gvExperience1 = (GridView)e.Item.FindControl("gvExperience1");

                    ApplicationResult objResults = new ApplicationResult();
                    EmployeeTBL objEmployeeTBl = new EmployeeTBL();

                    objResults = objEmployeeTBl.EmpoyeeQualificationT_SelectForReport(Convert.ToInt32((hfEmployeeMID.Value)));

                    if (objResults != null)
                    {
                        gvEducationalDetail1.DataSource = objResults.resultDT;
                        gvEducationalDetail1.DataBind();
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            gvEducationalDetail1.Visible = true;
                        }
                        else
                        {
                            gvEducationalDetail1.Visible = false;
                        }
                    }

                    objResults = objEmployeeTBl.EmployeeExpirenceT_SelectForReport(Convert.ToInt32((hfEmployeeMID.Value)));

                    if (objResults != null)
                    {
                        gvExperience1.DataSource = objResults.resultDT;
                        gvExperience1.DataBind();

                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            gvExperience1.Visible = true;
                        }
                        else
                        {
                            gvExperience1.Visible = false;
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

        #region Back Button
        protected void btnBack_Click(object sender, EventArgs e)
        {
            // divEmployee.Visible = false;
            btnPrintDetail.Visible = false;
            pnlEmployeeInfo.Visible = true;
            ClearAll();
        }
        #endregion

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            divEmployee.Visible = false;
            btnBack1.Visible = false;
        }
        #endregion

        #region Print Button Click Event
        protected void btnPrintDetail_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('divEmployee1');", true);
        }
        #endregion

        #region Report Button Event
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Client.UI/SchoolReports.aspx?Mode=SchoolGeneralReports");
        }
        #endregion
    }
}