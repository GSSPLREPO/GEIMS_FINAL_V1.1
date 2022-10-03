using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.Common;
using GEIMS.DataAccess;

namespace GEIMS.Client.UI
{
    public partial class SchoolMaterial : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(SchoolMaterial));

        #region Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    BindMaterialGroup();
                    BindUOM();
                    ViewState["Mode"] = "Save";
                    ViewState["MaterialID"] = 0;
                    PanelGrid_VisibilityMode(1);
                    divGrid.Visible = false;
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
        }
        #endregion

        #region Bind Material Group
        public void BindMaterialGroup()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                MaterialGroupBL objMaterialGroupBL = new MaterialGroupBL();
                Controls objControl = new Controls();
                objResult = objMaterialGroupBL.MaterialGroup_SelectAll();
                if (objResult != null)
                {
                    objControl.BindDropDown_ListBox(objResult.resultDT, ddlMaterialGroup, "MaterialGroupName", "MaterialGroupID");
                }
                ddlMaterialGroup.Items.Insert(0, new ListItem("--Select--", ""));
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Bind UOM
        public void BindUOM()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                UOMBL objUOMBL = new UOMBL();
                Controls objControl = new Controls();
                objResult = objUOMBL.UOM_SelectAll();
                if (objResult != null)
                {
                    objControl.BindDropDown_ListBox(objResult.resultDT, ddlUOM, "UOMName", "UOMID");
                }
                ddlUOM.Items.Insert(0, new ListItem("--Select--", ""));
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Bind grid
        private void GridDataBind(int intMaterialGroupID)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                MaterialBL objMaterialGroupBL = new MaterialBL();

                objResult = objMaterialGroupBL.Material_SelectAll(intMaterialGroupID);
                if (objResult != null)
                {
                    gvMaterial.DataSource = objResult.resultDT;
                    gvMaterial.DataBind();

                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        PanelGrid_VisibilityMode(1);
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

        #region Save Click
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResults = new ApplicationResult();
                MaterialBO objMaterialBO = new MaterialBO();
                MaterialBL objMaterialBL = new MaterialBL();

                objMaterialBO.MaterialGroupID = Convert.ToInt32(ddlMaterialGroup.SelectedValue);
                objMaterialBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                objMaterialBO.SchoolMID = 0;
                objMaterialBO.UOMID = Convert.ToInt32(ddlUOM.SelectedValue);
                objMaterialBO.MaterialCode = txtMaterialCode.Text;
                objMaterialBO.MaterialName = txtMaterialName.Text;
                objMaterialBO.Description = txtDescription.Text;
                objMaterialBO.ModelNo = txtModelNo.Text;
                objMaterialBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                objMaterialBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                int intValidate = 0;

                if (ViewState["Mode"].ToString() == "Save")
                {
                    objMaterialBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    objMaterialBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                    objResults = objMaterialBL.Material_Insert(objMaterialBO);
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            intValidate = Convert.ToInt32(objResults.resultDT.Rows[0]["MaterialValidate"]);
                        }
                    }
                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    objMaterialBO.MaterialID = Convert.ToInt32(ViewState["MaterialID"].ToString());
                    objResults = objMaterialBL.Material_Update(objMaterialBO);
                    if (objResults.resultDT.Rows.Count > 0)
                    {
                        intValidate = Convert.ToInt32(objResults.resultDT.Rows[0]["MaterialValidate"]);
                    }
                }
                if (intValidate == 1)
                {
                    txtMaterialName.Text = "";
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Material Name is already exist.');</script>");
                }
                else if (intValidate == 2)
                {
                    txtMaterialCode.Text = "";
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Material Code is already exist.');</script>");
                }
                else if (intValidate == 3)
                {
                    if (ViewState["Mode"].ToString() == "Save")
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Material Created Successfully.');</script>");
                    }
                    else if (ViewState["Mode"].ToString() == "Edit")
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Material updated successfully.');</script>");
                    }
                    setVisibility();
                }
                else if (intValidate == 4)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Set Visibility
        public void setVisibility()
        {
            ClearAll();
            GridDataBind(Convert.ToInt32(ddlMaterialGroup.SelectedValue));
            PanelGrid_VisibilityMode(1);
        }
        #endregion

        #region Gridview gvMaterial Events [RowCommand, PreRender]
        protected void gvMaterial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Controls objControls = new Controls();
                ApplicationResult objResults = new ApplicationResult();
                MaterialBL objMaterialBL = new MaterialBL();

                ViewState["MaterialID"] = Convert.ToInt32(e.CommandArgument.ToString());
                if (e.CommandName.ToString() == "Edit1")
                {
                    objResults = objMaterialBL.Material_Select(Convert.ToInt32(ViewState["MaterialID"].ToString()));
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            ddlMaterialGroup.SelectedValue = objResults.resultDT.Rows[0][MaterialBO.MATERIAL_MATERIALGROUPID].ToString();
                            ddlUOM.SelectedValue = objResults.resultDT.Rows[0][MaterialBO.MATERIAL_UOMID].ToString();
                            txtMaterialCode.Text = objResults.resultDT.Rows[0][MaterialBO.MATERIAL_MATERIALCODE].ToString();
                            txtMaterialName.Text = objResults.resultDT.Rows[0][MaterialBO.MATERIAL_MATERIALNAME].ToString();
                            txtModelNo.Text = objResults.resultDT.Rows[0][MaterialBO.MATERIAL_MODELNO].ToString();
                            txtDescription.Text = objResults.resultDT.Rows[0][MaterialBO.MATERIAL_DESCRIPTION].ToString();
                            ViewState["Mode"] = "Edit";
                            PanelGrid_VisibilityMode(2);
                        }
                    }
                }
                else if ((e.CommandName.ToString() == "Delete1"))
                {
                    objResults = objMaterialBL.Material_Delete(Convert.ToInt32(ViewState["MaterialID"].ToString()));
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClearAll();
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Material Group deleted successfully.');</script>");
                        GridDataBind(Convert.ToInt32(ddlMaterialGroup.SelectedValue));
                        PanelGrid_VisibilityMode(1);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Material Group is not deleted because it is in use.');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        protected void gvMaterial_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvMaterial.Rows.Count > 0)
                {
                    gvMaterial.UseAccessibleHeader = true;
                    gvMaterial.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region PanelGrid_VisibilityMode

        //panel grid true & false according to condition
        private void PanelGrid_VisibilityMode(int intMode)
        {
            if (intMode == 1)
            {
                divGrid.Visible = true;
                tabs.Visible = false;
                ViewState["DropDownMode"] = "Load";
                btnViewList.Visible = false;
                btnAddNew.Visible = true;
            }
            else if (intMode == 2)
            {
                divGrid.Visible = false;
                tabs.Visible = true;
                ViewState["DropDownMode"] = "New";
                btnViewList.Visible = true;
                btnAddNew.Visible = true;
            }
            if (ViewState["Mode"].ToString() == "Edit")
            {
                ddlMaterialGroup.Enabled = false;
            }
            else if (ViewState["Mode"].ToString() == "Save")
            {
                ddlMaterialGroup.Enabled = true;
            }
        }

        #endregion PanelGrid_VisibilityMode

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            ViewState["MaterialID"] = null;
            ViewState["Mode"] = "Save";
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
        }
        #endregion

        #region DropDown ddlMaterialGroup Selected IndexChanged Event
        protected void ddlMaterialGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlMaterialGroup.SelectedValue != "")
                {
                    if (ViewState["DropDownMode"].ToString() == "Load")
                    {
                        GridDataBind(Convert.ToInt32(ddlMaterialGroup.SelectedValue));
                    }
                }
                else
                {
                    divGrid.Visible = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Button AddNew Click Event
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            PanelGrid_VisibilityMode(2);
        }
        #endregion

        #region Button ViewList Click Event
        protected void btnViewList_Click(object sender, EventArgs e)
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["MaterialID"] = null;
            PanelGrid_VisibilityMode(1);
        }
        #endregion
    }
}