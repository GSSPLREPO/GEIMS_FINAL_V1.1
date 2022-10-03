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
    public partial class UOM : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(UOM));

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

        #region gridview Events [RowCommand, PreRender]
        protected void gvUOM_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvUOM.Rows.Count > 0)
                {
                    gvUOM.UseAccessibleHeader = true;
                    gvUOM.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        protected void gvUOM_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Controls objControls = new Controls();
                ApplicationResult objResults = new ApplicationResult();
                UOMBL objUOMBL = new UOMBL();

                ViewState["UOMID"] = Convert.ToInt32(e.CommandArgument.ToString());
                if (e.CommandName.ToString() == "Edit1")
                {
                    objResults = objUOMBL.UOM_Select(Convert.ToInt32(ViewState["UOMID"].ToString()));

                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            txtUnitName.Text = objResults.resultDT.Rows[0][UOMBO.UOM_UOMNAME].ToString();
                            txtDescription.Text = objResults.resultDT.Rows[0][UOMBO.UOM_DESCRIPTION].ToString();

                            ViewState["Mode"] = "Edit";
                            PanelGrid_VisibilityMode(2);
                        }
                    }
                }
                else if ((e.CommandName.ToString() == "Delete1"))
                {
                    objResults = objUOMBL.UOM_Delete(Convert.ToInt32(ViewState["UOMID"].ToString()));
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClearAll();
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('UOM deleted successfully.');</script>");
                        GridDataBind();
                        PanelGrid_VisibilityMode(1);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('UOM is not deleted because it is in use.');</script>");
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


        #region Bind grid
        private void GridDataBind()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                UOMBL objUOMBL = new UOMBL();

                objResult = objUOMBL.UOM_SelectAll();
                if (objResult != null)
                {
                    gvUOM.DataSource = objResult.resultDT;
                    gvUOM.DataBind();

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
                UOMBO objUOMBO = new UOMBO();
                UOMBL objUOMBL = new UOMBL();

                objUOMBO.UOMName = txtUnitName.Text;
                objUOMBO.Description = txtDescription.Text;
                objUOMBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                objUOMBO.LastModifiedUserId = Convert.ToInt32(Session[ApplicationSession.USERID]);
                int intClear = 1;

                if (ViewState["Mode"].ToString() == "Save")
                {
                    objUOMBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    objUOMBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                    objResults = objUOMBL.UOM_Insert(objUOMBO);
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        intClear = 1;
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('UOM Created Successfully.');</script>");
                    }
                    else
                    {
                        intClear = 0;
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Unit Name is already exist.');</script>");
                    }
                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    objUOMBO.UOMID = Convert.ToInt32(ViewState["UOMID"].ToString());
                    objResults = objUOMBL.UOM_Update(objUOMBO);
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        intClear = 1;
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('UOM updated successfully.');</script>");
                    }
                    else
                    {
                        intClear = 0;
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Unit Name is already exist.');</script>");
                    }
                }
                if (intClear == 1)
                {
                    ClearAll();
                    GridDataBind();
                }
                else
                {
                    txtUnitName.Text = "";
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
            ViewState["UOMID"] = null;
            ViewState["Mode"] = "Save";
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
        }
        #endregion
    }
}