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

namespace GEIMS.Accounting
{
    public partial class AccountGroup : PageBase
    {
        #region declaration
        private static ILog logger = LogManager.GetLogger(typeof(AccountGroup));
        Controls objControl = new Controls();
        string[] strDefaultNature = { "Debit", "Credit", "Credit", "Debit", "Credit" };
        #endregion

        #region Pre Init Method
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.QueryString["mode"] == "TU")
            {
                MasterPageFile = "~/Master/TrustMain.Master";
            }
        }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Session[ApplicationSession.HASACCESSACCOUNTUSERID]) > 0)
            {
                if (!IsPostBack)
                {
                    try
                    {
                        if (Request.QueryString["mode"] == "TU")
                        {
                            lblDuration.Text = Session[ApplicationSession.TRUSTNAME].ToString()+". Account Duration : " + Session[ApplicationSession.ACCOUNTFROMDATE].ToString() + " To " + Session[ApplicationSession.ACCOUNTTODATE].ToString();
                        }
                        else
                        {
                            if (Convert.ToInt32(Session[ApplicationSession.SCHOOLID])==0)
                            {
                                lblDuration.Text = Session[ApplicationSession.TRUSTNAME].ToString() + ". Account Duration : " + Session[ApplicationSession.ACCOUNTFROMDATE].ToString() + " To " + Session[ApplicationSession.ACCOUNTTODATE].ToString();
                            }
                            else
                                lblDuration.Text = Session[ApplicationSession.SCHOOLNAME].ToString() + ". Account Duration : " + Session[ApplicationSession.ACCOUNTFROMDATE].ToString() + " To " + Session[ApplicationSession.ACCOUNTTODATE].ToString();
                        }

                        ViewState["Mode"] = "Save";
                        PanelVisibility(1);
                        BindAccountGroup();
                        
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Error", ex);
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                    }
                }
            }
            else
            {
                if (Convert.ToInt32(Session[ApplicationSession.SCHOOLID]) == 0)
                    Response.Redirect("../Accounting/AccountLogin.aspx?mode=TU", false);
                else
                    Response.Redirect("../Accounting/AccountLogin.aspx", false);
            }
        }
        #endregion

        #region Bind Grid
        public void BindAccountGroup()
        {
            AccountGroupBL objAccountGroupBL = new AccountGroupBL();
            AccountGroupBO objAccountGroupBO = new AccountGroupBO();
            ApplicationResult objResultSelectAll = new ApplicationResult();

            objResultSelectAll = objAccountGroupBL.AccountGroup_SelectAll();
            if (objResultSelectAll != null)
            {
                DataTable dtSelectAll = new DataTable();
                dtSelectAll = objResultSelectAll.resultDT;
                if (dtSelectAll.Rows.Count > 0)
                {
                    gvAccountGroup.DataSource = dtSelectAll;
                    gvAccountGroup.DataBind();
                }
            }
        }
        #endregion

        #region Bind Sub Group
        public void BindAccountSubGroup()
        {
            AccountGroupBL objAccountGroupBL = new AccountGroupBL();
            AccountGroupBO objAccountGroupBO = new AccountGroupBO();
            ApplicationResult objResultSelectAll = new ApplicationResult();

            objResultSelectAll = objAccountGroupBL.AccountGroup_SelectAll();
            if (objResultSelectAll != null)
            {
                DataTable dtSelectAll = new DataTable();
                dtSelectAll = objResultSelectAll.resultDT;
                if (dtSelectAll.Rows.Count > 0)
                {
                    objControl.BindDropDown_ListBox(dtSelectAll, ddlSubGroup, "AccountGroupName", "AccountGroupID");
                    ddlSubGroup.Items.Insert(0,new ListItem("--Select--",""));
                }
            }
        }
        #endregion

        #region Save Button
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                AccountGroupBL objAccountGroupBL = new AccountGroupBL();
                AccountGroupBO objAccountGroupBO = new AccountGroupBO();
                ApplicationResult objResultValidate = new ApplicationResult();
                int intAccountGroupID = 0;
                
                objAccountGroupBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                objAccountGroupBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                objAccountGroupBO.AccountGroupName = txtAccountGroupName.Text;
                int sel = ddlAccountNature.SelectedIndex;
                objAccountGroupBO.AccountGroupDefaultNature = strDefaultNature[sel - 1]; ;
                objAccountGroupBO.GroupNature = ddlAccountNature.SelectedValue;
                if (ddlSubGroup.SelectedValue != "")
                {
                    objAccountGroupBO.SubGroupID = Convert.ToInt32(ddlSubGroup.SelectedValue);
                    objAccountGroupBO.SubGroupOf = ddlSubGroup.SelectedItem.ToString();
                }
                objAccountGroupBO.Description = txtDescription.Text.Trim();
                objAccountGroupBO.IsDeleted = 0;
                objAccountGroupBO.CreatedDate = System.DateTime.UtcNow.AddHours(5.5).ToString();
                objAccountGroupBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                objAccountGroupBO.LastModifideDate = System.DateTime.UtcNow.AddHours(5.5).ToString();;
                objAccountGroupBO.LastModifideUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);

                //Code For Validate Department Name
                if (ViewState["Mode"].ToString() == "Save")
                {
                    intAccountGroupID = -1;
                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    intAccountGroupID = Convert.ToInt32(ViewState["AccountGroupID"].ToString());
                    objAccountGroupBO.AccountGroupID = Convert.ToInt32(ViewState["AccountGroupID"].ToString());
                }
                //objResultValidate = objAccountGroupBL.AccountGroup_ValidateName(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), txtAccountGroupName.Text, intAccountGroupID);
                objResultValidate = objAccountGroupBL.AccountGroup_ValidateName(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), txtAccountGroupName.Text, intAccountGroupID);

                if (objResultValidate != null)
                {
                    DataTable dtValidate = new DataTable();
                    dtValidate = objResultValidate.resultDT;
                    if (dtValidate.Rows.Count > 0)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Account group name already exist.');</script>");
                    }
                    else
                    {
                        if (ViewState["Mode"].ToString() == "Save")
                        {
                            ApplicationResult objResultSave = new ApplicationResult();
                            objResultSave = objAccountGroupBL.AccountGroup_Insert(objAccountGroupBO);
                            if (objResultSave.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record Saved successfully.');</script>");
                            }
                        }
                        else if (ViewState["Mode"].ToString() == "Edit")
                        {
                            ApplicationResult objResultUpdate = new ApplicationResult();
                            objResultUpdate = objAccountGroupBL.AccountGroup_Update(objAccountGroupBO);
                            if (objResultUpdate.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record updated successfully.');</script>");
                            }
                        }
                        ClearAll();
                        BindAccountGroup();
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

        #region Panel Visibility Mode
        public void PanelVisibility(int intcode)
        {
            if (intcode == 1)
            {
                divGrid.Visible = true;
                tabs.Visible = false;
                btnViewList.Visible = false;
            }
            else if (intcode == 2)
            {
                divGrid.Visible = false;
                tabs.Visible = true;
                btnViewList.Visible = true;
            }
        }
        #endregion

        #region Clear All
        public void ClearAll()
        {            
            objControl.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["Mode"] = "Save";
            Panel1.Enabled = true;
        }
        #endregion

        #region Add New
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            ClearAll();
            PanelVisibility(2);
            BindAccountSubGroup();
        }
        #endregion

        #region View List
        protected void btnViewList_Click(object sender, EventArgs e)
        {
            ClearAll();
            BindAccountGroup();
            PanelVisibility(1);
        }
        #endregion

        #region Gridview Event
        protected void gvAccountGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                AccountGroupBL objAccountGroupBL = new AccountGroupBL();
                AccountGroupBO objAccountGroupBO = new AccountGroupBO();
                ApplicationResult objResultSelect = new ApplicationResult();
                ApplicationResult objResultDelete = new ApplicationResult();
                ViewState["AccountGroupID"] = e.CommandArgument.ToString();

                if (e.CommandName.ToString() == "Edit1")
                {
                    objResultSelect = objAccountGroupBL.AccountGroup_Select(Convert.ToInt32(ViewState["AccountGroupID"].ToString()));
                    if (objResultSelect != null )
                    {
                        DataTable dtResult = objResultSelect.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            BindAccountSubGroup();
                            txtAccountGroupName.Text = dtResult.Rows[0][AccountGroupBO.ACCOUNTGROUP_ACCOUNTGROUPNAME].ToString();
                            ddlAccountNature.SelectedValue = dtResult.Rows[0][AccountGroupBO.ACCOUNTGROUP_GROUPNATURE].ToString();
                            if (dtResult.Rows[0][AccountGroupBO.ACCOUNTGROUP_SUBGROUPID].ToString() != "0")
                                ddlSubGroup.SelectedValue = dtResult.Rows[0][AccountGroupBO.ACCOUNTGROUP_SUBGROUPID].ToString();
                            txtDescription.Text = dtResult.Rows[0][AccountGroupBO.ACCOUNTGROUP_DESCRIPTION].ToString();
                            PanelVisibility(2);
                            if (ddlSubGroup.SelectedValue == "")
                            {
                                Panel1.Enabled = false;
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('This is a default Group Name, you can't change it.');</script>");
                            }
                            else
                            {
                                Panel1.Enabled = true;
                                ViewState["Mode"] = "Edit";
                            }
                                
                            
                        }
                    }
                }
                else if (e.CommandName.ToString() == "Delete1")
                {
                    objResultSelect = objAccountGroupBL.AccountGroup_Cascade(Convert.ToInt32(ViewState["AccountGroupID"].ToString()), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                    if (objResultSelect != null)
                    {
                        DataTable dtResult1 = objResultSelect.resultDT;
                        int col = Convert.ToInt32(dtResult1.Rows[0][0]);
                        //if (dtResult1.Rows.Count > 0)
                        if (col == 0)
                        {
                            objResultDelete = objAccountGroupBL.AccountGroup_Delete(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session[ApplicationSession.USERID]), DateTime.UtcNow.AddHours(5.5).ToString());
                            if (objResultDelete.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record deleted successfully.');</script>");
                                PanelVisibility(1);
                                BindAccountGroup();
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record not deleted successfully.');</script>");
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You cannot delete this record because it is in used.');</script>");
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! AccountGroupID is Null!.');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        protected void gvAccountGroup_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvAccountGroup_OnPreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvAccountGroup.Rows.Count > 0)
                {
                    gvAccountGroup.UseAccessibleHeader = true;
                    gvAccountGroup.HeaderRow.TableSection = TableRowSection.TableHeader;
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