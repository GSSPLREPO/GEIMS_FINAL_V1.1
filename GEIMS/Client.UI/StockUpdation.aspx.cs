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

namespace GEIMS.Client.UI
{
    public partial class StockUpdation : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(Material));

        #region Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ViewState["Mode"] = "Save";
                    hfMode.Value = "Save";
                    hfTab.Value = "0";
                    GetMaterialGroupName();
                    BindYear();
                    ViewState["Mode"] = hfMode.Value;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion



        #region Bind Material Group 
        public void GetMaterialGroupName()
        {
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            MaterialGroupBL objMaterialGroupBl = new MaterialGroupBL();

            objResult = objMaterialGroupBl.MaterialGroup_SelectAll();
            if (objResult != null)
            {
                if (objResult.resultDT.Rows.Count > 0)
                {
                    objControls.BindDropDown_ListBox(objResult.resultDT, ddlMaterialGroupName, "MaterialGroupName", "MaterialGroupID");
                }
                ddlMaterialGroupName.Items.Insert(0, new ListItem("--Select--", ""));
            }
        }
        #endregion

        #region Bind Year
        public void BindYear()
        {
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            MaterialGroupBL objMaterialGroupBl = new MaterialGroupBL();

            objResult = objMaterialGroupBl.GetYear();
            if (objResult != null)
            {
                if (objResult.resultDT.Rows.Count > 0)
                {
                    objControls.BindDropDown_ListBox(objResult.resultDT, ddlYear, "Year", "Year");
                    ddlYear.SelectedIndex = 1;
                }
            }
        }
        #endregion

        #region Bind gvMaterial
        private void BindgvMaterial()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                MaterialGroupBL objMaterialGroupBl = new MaterialGroupBL();

                objResult = objMaterialGroupBl.MaterialGroup_Select_ForMaterial(Convert.ToInt32(ddlMaterialGroupName.SelectedValue),Convert.ToInt32(Session[ApplicationSession.TRUSTID]),Convert.ToInt32(Session[ApplicationSession.SCHOOLID]),ddlYear.Text);
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvMaterial.DataSource = objResult.resultDT;
                        gvMaterial.DataBind();
                        //(TextBox)row.FindControl("txtStock");
                        for (int i = 0; i < objResult.resultDT.Rows.Count; i++)
                        {
                            var Id = objResult.resultDT.Rows[i]["MaterialTID"].ToString();
                            var Stock = objResult.resultDT.Rows[i]["Stock"].ToString();
                            foreach (GridViewRow row in gvMaterial.Rows)
                            {
                                if (row.Cells[0].Text == Id)
                                {
                                  //  ((CheckBox)row.FindControl("chkChild")).Checked = true;
                                    Convert.ToDouble(((TextBox)row.FindControl("txtStock")).Text = Stock);
                                }
                            }
                        }
                        divGrid.Visible = true;
                        btnSave.Visible = true;
                    }
                    else
                    {
                        gvMaterial.DataSource = null;
                        gvMaterial.DataBind();
                        divGrid.Visible = false;
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Stock is already Updated of this year or Financial year has not changed.');</script>");
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



        #region Button btnUpdateMaterial click Event
        protected void btnUpdateMaterial_OnClick(object sender, EventArgs e)
        {
            try
            {
                
                BindgvMaterial();
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Save Button Click Event
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                MaterialStockTBL objMaterialStockTbl = new MaterialStockTBL();
                MaterialStockTBO objMaterialStockTbo = new MaterialStockTBO();

                foreach (GridViewRow row in gvMaterial.Rows)
                {
                    objMaterialStockTbo.MaterialID = Convert.ToInt32(row.Cells[0].Text);
                    objMaterialStockTbo.Quantity = Convert.ToInt32(((TextBox)row.FindControl("txtStock")).Text);
                    objMaterialStockTbo.StockYear = Convert.ToInt32(ddlYear.Text);
                    objMaterialStockTbo.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    objMaterialStockTbo.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);

                    objResult = objMaterialStockTbl.MaterialStockT_Insert_ForStockUpdate(objMaterialStockTbo);
                }
                ClearAll();
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion



        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            ViewState["Mode"] = "Save";
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            gvMaterial.Visible = false;
            ddlYear.SelectedIndex = 1;
        }
        #endregion

    }
}