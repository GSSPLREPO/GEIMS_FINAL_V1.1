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
    public partial class RoleMaster : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(RoleMaster));

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindRole();
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



        #region Bind Role
        public void BindRole()
        {
            ApplicationResult objResult = new ApplicationResult();
            RoleBL objRolebl = new RoleBL();

            objResult = objRolebl.Role_SelectAll();
            if (objResult.resultDT.Rows.Count > 0)
            {
                gvRole.DataSource = objResult.resultDT;
                gvRole.DataBind();
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
                RoleBO objRoleBO = new RoleBO();
                RoleBL objRoleBL = new RoleBL();
                ApplicationResult objResult = new ApplicationResult();
                DataTable dtResult = new DataTable();
                int intRoleID = 0;

                objRoleBO.RoleName = txtRoleName.Text.Trim();
                objRoleBO.Description = txtDescription.Text.Trim();
                objRoleBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                objRoleBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();

                //Code For Validate Role Name
                if (ViewState["Mode"].ToString() == "Save")
                {
                    intRoleID = -1;
                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    intRoleID = Convert.ToInt32(ViewState["RoleID"].ToString());
                }
                objResult = objRoleBL.Role_ValidateName(intRoleID, objRoleBO.RoleName);
                if (objResult != null)
                {
                    dtResult = objResult.resultDT;
                    if (dtResult.Rows.Count > 0)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Role name already exist.');</script>");
                    }
                    else
                    {
                        if (ViewState["Mode"].ToString() == "Save")
                        {
                            objRoleBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                            objRoleBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                            objResult = objRoleBL.Role_Insert(objRoleBO);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record saved successfully.');</script>");
                            }
                        }
                        else if (ViewState["Mode"].ToString() == "Edit")
                        {
                            objRoleBO.RoleID = Convert.ToInt32(ViewState["RoleID"].ToString());
                            objResult = objRoleBL.Role_Update(objRoleBO);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record updated successfully.');</script>");
                            }
                        }
                        ClearAll();
                        BindRole();
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



        #region Role GridView Events [Row Command, Pre Render]
        protected void gvRole_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                RoleBL objRoleBL = new RoleBL();
                if (e.CommandName.ToString() == "Edit1")
                {
                    ViewState["Mode"] = "Edit";
                    ViewState["RoleID"] = e.CommandArgument.ToString();
                    objResult = objRoleBL.Role_Select(Convert.ToInt32(ViewState["RoleID"].ToString()));
                    if (objResult != null)
                    {
                        DataTable dtResult = objResult.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            txtRoleName.Text = dtResult.Rows[0][RoleBO.ROLE_ROLENAME].ToString();
                            txtDescription.Text = dtResult.Rows[0][RoleBO.ROLE_DESCRIPTION].ToString();
                            PanelVisibility(2);
                        }
                    }
                }
                else if (e.CommandName.ToString() == "Delete1")
                {
                    objResult = objRoleBL.Role_Delete(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session[ApplicationSession.USERID]),
                        DateTime.UtcNow.AddHours(5.5).ToString());
                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record deleted successfully.');</script>");
                        PanelVisibility(1);
                        BindRole();
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
        protected void gvRole_OnPreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvRole.Rows.Count > 0)
                {
                    gvRole.UseAccessibleHeader = true;
                    gvRole.HeaderRow.TableSection = TableRowSection.TableHeader;
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