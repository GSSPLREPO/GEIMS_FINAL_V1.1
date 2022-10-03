using System;
using System.Web.UI.WebControls;
using System.Data;
using log4net;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.Common;
using System.Web.UI;
using GEIMS.DataAccess;

namespace GEIMS.Client.UI
{
    public partial class MaterialGroup : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(MaterialGroup));

        #region Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    GridDataBind();
                    ViewState["Mode"] = "Save";
                    ViewState["MaterialGroupID"] = 0;
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
        }
        #endregion

        #region Bind grid
        private void GridDataBind()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                MaterialGroupBL objMaterialGroupBL = new MaterialGroupBL();

                objResult = objMaterialGroupBL.MaterialGroup_SelectAll();
                if (objResult != null)
                {
                    gvMaterialGroup.DataSource = objResult.resultDT;
                    gvMaterialGroup.DataBind();

                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        PanelGrid_VisibilityMode(1);
                    }
                    else
                    {
                        PanelGrid_VisibilityMode(2);
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
        
        #region ViewList Material Group Click
        protected void lnkViewList_Click(object sender, EventArgs e)
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["MaterialGroupID"] = null;
            PanelGrid_VisibilityMode(1);
        }
        #endregion

        #region Add NEW
        protected void lnkAddNewSection_Click(object sender, EventArgs e)
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            PanelGrid_VisibilityMode(2);
        }
        #endregion

        #region Save Click
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResults = new ApplicationResult();
                MaterialGroupBO objMaterialGroupBO = new MaterialGroupBO();
                MaterialGroupBL objMaterialGroupBL = new MaterialGroupBL();

                objMaterialGroupBO.MaterialGroupName = txtMGroupName.Text;
                objMaterialGroupBO.Description = txtDescription.Text;
                objMaterialGroupBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                objMaterialGroupBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                int intClear = 1;

                if (ViewState["Mode"].ToString() == "Save")
                {
                    objMaterialGroupBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    objMaterialGroupBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                    objResults = objMaterialGroupBL.MaterialGroup_Insert(objMaterialGroupBO);
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        intClear = 1;
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Material Group Created Successfully.');</script>");
                    }
                    else
                    {
                        intClear = 0;
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Material Group is already exist.');</script>");
                    }
                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    objMaterialGroupBO.MaterialGroupID = Convert.ToInt32(ViewState["MaterialGroupID"].ToString());
                    objResults = objMaterialGroupBL.MaterialGroup_Update(objMaterialGroupBO);
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        intClear = 1;
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Material Group updated successfully.');</script>");
                    }
                    else
                    {
                        intClear = 0;
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Material Group is already exist.');</script>");
                    }
                }
                if (intClear == 1)
                {
                    ClearAll();
                    GridDataBind();
                    PanelGrid_VisibilityMode(1);
                }
                else
                {
                    txtMGroupName.Text = "";
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion


        #region Gridview gvMaerialGroup Events
        protected void gvMaterialGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Controls objControls = new Controls();
                ApplicationResult objResults = new ApplicationResult();
                MaterialGroupBL objMaterialGroupBL = new MaterialGroupBL();

                ViewState["MaterialGroupID"] = Convert.ToInt32(e.CommandArgument.ToString());
                if (e.CommandName.ToString() == "Edit1")
                {
                    objResults = objMaterialGroupBL.MaterialGroup_Select(Convert.ToInt32(ViewState["MaterialGroupID"].ToString()));

                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            txtMGroupName.Text = objResults.resultDT.Rows[0][MaterialGroupBO.MATERIALGROUP_MATERIALGROUPNAME].ToString();
                            txtDescription.Text = objResults.resultDT.Rows[0][MaterialGroupBO.MATERIALGROUP_DESCRIPTION].ToString();

                            ViewState["Mode"] = "Edit";
                            PanelGrid_VisibilityMode(2);
                        }
                    }
                }
                else if ((e.CommandName.ToString() == "Delete1"))
                {
                    objResults = objMaterialGroupBL.MaterialGroup_Delete(Convert.ToInt32(ViewState["MaterialGroupID"].ToString()));
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClearAll();
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Material Group deleted successfully.');</script>");
                        GridDataBind();
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

        protected void gvMaterialGroup_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvMaterialGroup.Rows.Count > 0)
                {
                    gvMaterialGroup.UseAccessibleHeader = true;
                    gvMaterialGroup.HeaderRow.TableSection = TableRowSection.TableHeader;
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
                lnkViewList.Visible = false;
                lnkAddNewMaterialGroup.Visible = true;
            }
            else if (intMode == 2)
            {

                divGrid.Visible = false;
                tabs.Visible = true;
                lnkViewList.Visible = true;
                lnkAddNewMaterialGroup.Visible = true;
            }
        }

        #endregion PanelGrid_VisibilityMode

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            ViewState["MaterialGroupID"] = null;
            ViewState["Mode"] = "Save";
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
        }
        #endregion
    }
}