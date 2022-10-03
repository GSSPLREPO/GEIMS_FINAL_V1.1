using System;
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

namespace GEIMS.Client.UI
{
    public partial class SchoolDepartment : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(SchoolDepartment));


        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindDepartment();
                    ViewState["Mode"] = "Save";
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
        }
        #endregion



        #region Bind Department
        public void BindDepartment()
        {
            ApplicationResult objResult = new ApplicationResult();
            DepartmentBL objDepartmentbl = new DepartmentBL();

            objResult = objDepartmentbl.Department_Select_By_Trust_School(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
            if (objResult.resultDT.Rows.Count > 0)
            {
                gvDepartment.DataSource = objResult.resultDT;
                gvDepartment.DataBind();
                PanelVisibility(1);
            }
            else
            {
                PanelVisibility(2);
            }
        }
        #endregion



        #region Save Button Click Event
        protected void btnSaveClass_OnClick(object sender, EventArgs e)
        {
            try
            {
                DepartmentBO objDepartmentBO = new DepartmentBO();
                DepartmentBL objDepartmentBL = new DepartmentBL();
                ApplicationResult objResult = new ApplicationResult();
                DataTable dtResult = new DataTable();
                int intDepartmentID = 0;

                objDepartmentBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                objDepartmentBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                objDepartmentBO.DepartmentNameENG = txtDepartmentNameENG.Text.Trim();
                objDepartmentBO.DepartmentNameGUJ = txtDepartmentNameGUJ.Text.Trim();
                objDepartmentBO.Description = txtDescription.Text.Trim();
                objDepartmentBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                objDepartmentBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();

                //Code For Validate Department Name
                if (ViewState["Mode"].ToString() == "Save")
                {
                    intDepartmentID = -1;
                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    intDepartmentID = Convert.ToInt32(ViewState["DepartmentID"].ToString());
                }
                objResult = objDepartmentBL.Department_ValidateName(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), intDepartmentID, objDepartmentBO.DepartmentNameENG);
                if (objResult != null)
                {
                    dtResult = objResult.resultDT;
                    if (dtResult.Rows.Count > 0)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Department name already exist.');</script>");
                    }
                    else
                    {
                        if (ViewState["Mode"].ToString() == "Save")
                        {
                            objDepartmentBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                            objDepartmentBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                            objResult = objDepartmentBL.Department_Insert(objDepartmentBO);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record saved successfully.');</script>");
                            }
                        }
                        else if (ViewState["Mode"].ToString() == "Edit")
                        {
                            objDepartmentBO.DepartmentID = Convert.ToInt32(ViewState["DepartmentID"].ToString());
                            objResult = objDepartmentBL.Department_Update(objDepartmentBO);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record updated successfully.');</script>");
                            }
                        }
                        ClearAll();
                        BindDepartment();
                        PanelVisibility(1);
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

        #region Add New Button Click Event
        protected void lnkAddNewClass_OnClick(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                PanelVisibility(2);
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region View List Button Click event
        protected void lnkViewList_OnClick(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                PanelVisibility(1);
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion



        #region Department GridView Events [Row Command, Pre Render]
        protected void gvDepartment_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                DepartmentBL objDepartmentBL = new DepartmentBL();
                if (e.CommandName.ToString() == "Edit1")
                {
                    ViewState["Mode"] = "Edit";
                    ViewState["DepartmentID"] = e.CommandArgument.ToString();
                    objResult = objDepartmentBL.Department_Select(Convert.ToInt32(ViewState["DepartmentID"].ToString()));
                    if (objResult != null)
                    {
                        DataTable dtResult = objResult.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            txtDepartmentNameENG.Text = dtResult.Rows[0][DepartmentBO.DEPARTMENT_DEPARTMENTNAMEENG].ToString();
                            txtDepartmentNameGUJ.Text = dtResult.Rows[0][DepartmentBO.DEPARTMENT_DEPARTMENTNAMEGUJ].ToString();
                            txtDescription.Text = dtResult.Rows[0][DepartmentBO.DEPARTMENT_DESCRIPTION].ToString();
                            PanelVisibility(2);
                        }
                    }
                }
                else if (e.CommandName.ToString() == "Delete1")
                {
                    objResult = objDepartmentBL.Department_Delete(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session[ApplicationSession.USERID]),
                        DateTime.UtcNow.AddHours(5.5).ToString());
                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record deleted successfully.');</script>");
                        PanelVisibility(1);
                        BindDepartment();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You cannot delete this record because it is in used.');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        protected void gvDepartment_OnPreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvDepartment.Rows.Count > 0)
                {
                    gvDepartment.UseAccessibleHeader = true;
                    gvDepartment.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion



        #region Clear All Method
        public void ClearAll()
        {
            Controls objControl = new Controls();
            objControl.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["Mode"] = "Save";
        }
        #endregion

        #region Panel Visibility Mode
        public void PanelVisibility(int intcode)
        {
            if (intcode == 1)
            {
                divGrid.Visible = true;
                tabs.Visible = false;
                lnkViewList.Visible = false;
            }
            else if (intcode == 2)
            {
                divGrid.Visible = false;
                tabs.Visible = true;
                lnkViewList.Visible = true;
            }
        }
        #endregion
    }
}