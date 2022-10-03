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

namespace GEIMS.PayRoll
{
    public partial class PayItemMaster : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(PayItemMaster));

        #region Page PreInit Event
        protected void Page_PreInit()
        {
            if (Request.QueryString["Source"] != null)
            {
                if (Request.QueryString["Source"] == "trust")
                {
                    Page.MasterPageFile = "../master/TrustMain.Master";
                }
                else if (Request.QueryString["Source"] == "school")
                {
                    Page.MasterPageFile = "../master/SchoolMain.Master";
                }
                else
                {
                    Response.Redirect("../Client.UI/LogOut.aspx", false);
                }
            }
            else
            {
                Response.Redirect("../Client.UI/LogOut.aspx", false);
            }
        }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindPayItem();
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



        #region Bind Subject
        public void BindPayItem()
        {
            ApplicationResult objResult = new ApplicationResult();
            PayItemMBL objPayItemMbl = new PayItemMBL();

            objResult = objPayItemMbl.PayItemM_SelectAll();
            if (objResult.resultDT.Rows.Count > 0)
            {
                gvPayItem.DataSource = objResult.resultDT;
                gvPayItem.DataBind();
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
                PayItemMBO objPayItemMBO = new PayItemMBO();
                PayItemMBL objPayItemMBL = new PayItemMBL();
                ApplicationResult objResult = new ApplicationResult();
                DataTable dtResult = new DataTable();
                int intPayItemID = 0;

                objPayItemMBO.Name = txtPayItemName.Text.Trim();
                objPayItemMBO.Type = Convert.ToInt32(ddlType.SelectedValue);
                objPayItemMBO.Description = txtDescription.Text.Trim();
                objPayItemMBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                objPayItemMBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();

                //Code For Validate PayItemM Name
                if (ViewState["Mode"].ToString() == "Save")
                {
                    intPayItemID = -1;
                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    intPayItemID = Convert.ToInt32(ViewState["PayItemMID"].ToString());
                }
                objResult = objPayItemMBL.PayItemM_ValidateName(intPayItemID, objPayItemMBO.Name);
                if (objResult != null)
                {
                    dtResult = objResult.resultDT;
                    if (dtResult.Rows.Count > 0)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Pay item name already exist.');</script>");
                    }
                    else
                    {
                        if (ViewState["Mode"].ToString() == "Save")
                        {
                            objPayItemMBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                            objPayItemMBO.CreatedDate = System.DateTime.UtcNow.AddHours(5.5).ToString();
                            objResult = objPayItemMBL.PayItemM_Insert(objPayItemMBO);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record saved successfully.');</script>");
                            }
                        }
                        else if (ViewState["Mode"].ToString() == "Edit")
                        {
                            objPayItemMBO.PayItemMID = Convert.ToInt32(ViewState["PayItemMID"].ToString());
                            objResult = objPayItemMBL.PayItemM_Update(objPayItemMBO);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record updated successfully.');</script>");
                            }
                        }
                        ClearAll();
                        BindPayItem();
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



        #region Subject GridView Events [Row Command, Pre Render]
        protected void gvPayItem_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                PayItemMBL objPayItemMBL = new PayItemMBL();
                if (e.CommandName.ToString() == "Edit1")
                {
                    ViewState["Mode"] = "Edit";
                    ViewState["PayItemMID"] = e.CommandArgument.ToString();
                    objResult = objPayItemMBL.PayItemM_Select(Convert.ToInt32(ViewState["PayItemMID"].ToString()));
                    if (objResult != null)
                    {
                        DataTable dtResult = objResult.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            txtPayItemName.Text = dtResult.Rows[0][PayItemMBO.PAYITEMM_NAME].ToString();
                            txtDescription.Text = dtResult.Rows[0][PayItemMBO.PAYITEMM_DESCRIPTION].ToString();
                            ddlType.SelectedValue = dtResult.Rows[0][PayItemMBO.PAYITEMM_TYPE].ToString();
                            PanelVisibility(2);
                        }
                    }
                }
                else if (e.CommandName.ToString() == "Delete1")
                {
                    objResult = objPayItemMBL.PayItemM_Delete(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session[ApplicationSession.USERID]),
                        DateTime.UtcNow.AddHours(5.5).ToString());
                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record deleted successfully.');</script>");
                        PanelVisibility(1);
                        BindPayItem();
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

        protected void gvPayItem_OnPreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvPayItem.Rows.Count > 0)
                {
                    gvPayItem.UseAccessibleHeader = true;
                    gvPayItem.HeaderRow.TableSection = TableRowSection.TableHeader;
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