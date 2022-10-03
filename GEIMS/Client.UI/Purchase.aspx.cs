using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using log4net;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.Common;
using GEIMS.DataAccess;

namespace GEIMS.Client.UI
{
    public partial class Purchase : PageBase
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
                    divPurchaseUpdate.Visible = false;
                    divPurchaseSave.Visible = true;
                    GetVendor();
                    ReadOnlyTextBoxes();
                    hfMode.Value = "Save";
                    //GetFinancialYear();
                    GetMaterialGroupName();
                    TempData();
                    txtAvailableQuantity.Attributes.Add("readonly", "readonly");
                    txtTransferFromDate.Attributes.Add("readonly", "readonly");
                    txtTransferTodate.Attributes.Add("readonly", "readonly");

                    //txtDate.Text =  DateTime.UtcNow.AddHours(5.5).ToString("dd/mm/yyyy");
                    hfTab.Value = "0";

                    ViewState["Mode"] = hfMode.Value;
                    if (ViewState["Mode"].ToString() == "Save")
                    {
                        gvMaterial.Columns[5].Visible = false;
                    }
                    else if (ViewState["Mode"].ToString() == "Edit")
                    {

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

        #region Vendor Fetch Method

        public void GetVendor()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                VendorBL objVendorBl = new VendorBL();

                objResult = objVendorBl.Vendor_SelectAll();
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlVendorName, "VendorName", "VendorID");
                    }
                    ddlVendorName.Items.Insert(0, new ListItem("--Select--", ""));
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        #endregion

        #region Make Read Only TextBox Method

        public void ReadOnlyTextBoxes()
        {
            txtBillDate.Attributes.Add("readonly", "readonly");
            txtPoDate.Attributes.Add("readonly", "readonly");
            txtVoucharDate.Attributes.Add("readonly", "readonly");
            txtMaterialTotalAmount.Attributes.Add("readonly", "readonly");
            txtTotalAmount.Attributes.Add("readonly", "readonly");
        }

        #endregion

        #region Fetch Financial Year

        //public void GetFinancialYear()
        //{
        //    CommonFunctions objFunctions = new CommonFunctions();
        //    Controls objControls = new Controls();

        //    objControls.BindDropDown_ListBox(objFunctions.CreateFinancialYearDt(), ddlYear, "FinancialYear", "Year");
        //    ddlYear.Items.Insert(0, new ListItem("--Select--", ""));
        //}

        #endregion

        #region Fetch Material Group Name

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
                    objControls.BindDropDown_ListBox(objResult.resultDT, ddlTMaterialGroup, "MaterialGroupName", "MaterialGroupID");
                    objControls.BindDropDown_ListBox(objResult.resultDT, ddlMaterialGroupConsume, "MaterialGroupName", "MaterialGroupID");
                }
                ddlMaterialGroupName.Items.Insert(0, new ListItem("--Select--", ""));
                ddlTMaterialGroup.Items.Insert(0, new ListItem("--Select--", ""));
                lbMaterial.Items.Insert(0, new ListItem("--Select--", ""));
                lbMaterialConsume.Items.Insert(0, new ListItem("--Select--", ""));
                ddlMaterialGroupConsume.Items.Insert(0, new ListItem("--Select--", ""));
            }
        }

        #endregion



        #region Dynamic Data Table

        public void TempData()
        {
            DataTable dtTemp = new DataTable();

            dtTemp.Columns.Add("PurchaseTID", typeof(int));
            dtTemp.Columns.Add("PurchaseID", typeof(int));
            dtTemp.Columns.Add("UOMID", typeof(int));
            dtTemp.Columns.Add("UOMName", typeof(string));
            dtTemp.Columns.Add("MaterialGroupID", typeof(int));
            dtTemp.Columns.Add("MaterialGroupName", typeof(string));
            dtTemp.Columns.Add("MaterialID", typeof(int));
            dtTemp.Columns.Add("MaterialName", typeof(string));
            dtTemp.Columns.Add("Quantity", typeof(int));
            dtTemp.Columns.Add("Rate", typeof(float));
            dtTemp.Columns.Add("TotalAmount", typeof(float));

            ViewState["dtMaterial"] = dtTemp;
            gvMaterial.DataSource = dtTemp;
            gvMaterial.DataBind();
        }

        #endregion

        #region Web Service Get Material
        [System.Web.Services.WebMethod]
        public static string GetMaterial(int MaterialGroupID, int TrustID, int SchoolID)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                MaterialBL objMaterialBl = new MaterialBL();

                objResult = objMaterialBl.Material_SelectAll_ForDropDown(MaterialGroupID, TrustID, SchoolID);
                string res = "";
                res = DataSetToJSON(objResult.resultDT);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Web Service Get UOM
        [System.Web.Services.WebMethod]
        public static string GetUOM(int MaterialID)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                UOMBL objUombl = new UOMBL();

                objResult = objUombl.UOM_Select_DropDown(MaterialID);
                string res = "";
                res = DataSetToJSON(objResult.resultDT);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



        #region Method for Converting DataSet to Json
        public static string DataSetToJSON(DataTable dt)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            JavaScriptSerializer json = new JavaScriptSerializer();

            return json.Serialize(rows);
        }
        #endregion

        #region FetchMaterialUom
        private DataTable FetchUom(int MaterialID)
        {
            // DataTable dtClass = new DataTable();
            UOMBL objUombl = new UOMBL();
            ApplicationResult objResults = new ApplicationResult();

            objResults = objUombl.UOM_Select_DropDown(MaterialID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {

            }
            return objResults.resultDT;
        }
        #endregion

        #region FetchMaterialName
        private DataTable FetchMaterialName(int MaterialGroupID, int TrustID, int SchoolID)
        {
            // DataTable dtClass = new DataTable();
            MaterialBL objMaterialBl = new MaterialBL();
            ApplicationResult objResults = new ApplicationResult();

            objResults = objMaterialBl.Material_SelectAll_ForDropDown(MaterialGroupID, TrustID, SchoolID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {

            }
            return objResults.resultDT;
        }
        #endregion

        #region Button Save Click Event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                PurchaseBO objPurchaseBo = new PurchaseBO();
                PurchaseTBO objPurchaseTBo = new PurchaseTBO();
                PurchaseBL objPurchaseBl = new PurchaseBL();
                ApplicationResult objResult = new ApplicationResult();
                MaterialBL objMaterialBl = new MaterialBL();

                objResult = objMaterialBl.Material_Select_ForStockUpdate();
                if (objResult.resultDT.Rows[0][0].ToString() == "0")
                {

                    objPurchaseBo.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                    objPurchaseBo.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                    objPurchaseBo.VendorID = Convert.ToInt32(ddlVendorName.SelectedValue);
                    objPurchaseBo.PONO = txtPONo.Text;
                    objPurchaseBo.PODate = txtPoDate.Text.Trim();
                    objPurchaseBo.BillDate = txtBillDate.Text.Trim();
                    objPurchaseBo.BillNO = txtBillNo.Text;
                    objPurchaseBo.VoucharNO = txtVoucharNo.Text;
                    objPurchaseBo.VoucharDate = txtVoucharDate.Text.Trim();
                    objPurchaseBo.PaymentType = ddlPaymentType.SelectedItem.Text;
                    objPurchaseBo.PaymentMode = ddlPaymentMode.SelectedItem.Text;
                    objPurchaseBo.VAT = !string.IsNullOrEmpty(txtVAT.Text) ? Convert.ToDouble(txtVAT.Text) : 0.0;
                    objPurchaseBo.AddVAT = !string.IsNullOrEmpty(txtCST.Text) ? Convert.ToDouble(txtCST.Text) : 0.0;
                    objPurchaseBo.CST = !string.IsNullOrEmpty(txtCST.Text) ? Convert.ToDouble(txtCST.Text) : 0.0;
                    objPurchaseBo.Discount = !string.IsNullOrEmpty(txtDiscount.Text)
                        ? Convert.ToDouble(txtDiscount.Text)
                        : 0.0;
                    objPurchaseBo.TotalAmount = !string.IsNullOrEmpty(txtTotalAmount.Text)
                        ? Convert.ToDouble(txtTotalAmount.Text)
                        : 0.0;
                    objPurchaseBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    objPurchaseBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                    ViewState["Mode"] = hfMode.Value;
                    if (ViewState["Mode"].ToString() == "Save")
                    {
                        try
                        {
                            objPurchaseBo.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                            objPurchaseBo.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);

                            if (gvMaterial.Rows.Count > 0)
                            {
                                bool reConnect = true;
                                int j = 1;
                                while (reConnect && j <= 40) // Added For Connection Problem
                                {
                                    if (DatabaseTransaction.connection.State == ConnectionState.Closed)
                                    {
                                        reConnect = false;
                                        DatabaseTransaction.OpenConnectionTransation();
                                        objResult = objPurchaseBl.Purchase_Insert(objPurchaseBo);
                                        if (objResult != null)
                                        {
                                            if (objResult.resultDT.Rows.Count > 0)
                                            {
                                                ViewState["PurchaseID"] =
                                                    objResult.resultDT.Rows[0]["PurchaseID"].ToString();
                                                DataTable dtPurchaseT = (DataTable)ViewState["dtMaterial"];

                                                #region Purchase Transaction Objects

                                                int intSuccessCount = 0;
                                                for (int i = 0; i < dtPurchaseT.Rows.Count; i++)
                                                {
                                                    objPurchaseTBo.PurchaseID =
                                                        Convert.ToInt32(ViewState["PurchaseID"].ToString());
                                                    objPurchaseTBo.MaterialID =
                                                        Convert.ToInt32(dtPurchaseT.Rows[i]["MaterialID"].ToString());
                                                    objPurchaseTBo.UOMID =
                                                        Convert.ToInt32(dtPurchaseT.Rows[i]["UOMID"].ToString());
                                                    objPurchaseTBo.Quantity =
                                                        Convert.ToInt32(dtPurchaseT.Rows[i]["Quantity"].ToString());
                                                    objPurchaseTBo.Rate =
                                                        Convert.ToDouble(dtPurchaseT.Rows[i]["Rate"].ToString());
                                                    objPurchaseTBo.TotalAmount =
                                                        Convert.ToDouble(dtPurchaseT.Rows[i]["TotalAmount"].ToString());
                                                    objPurchaseTBo.LastModifiedDate =
                                                        DateTime.UtcNow.AddHours(5.5).ToString();
                                                    objPurchaseTBo.LastModifiedUserID =
                                                        Convert.ToInt32(Session[ApplicationSession.USERID]);
                                                    objPurchaseTBo.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                                                    objPurchaseTBo.CreatedUserID =
                                                        Convert.ToInt32(Session[ApplicationSession.USERID]);

                                                    objResult = objPurchaseBl.Purchase_T_Insert(objPurchaseTBo,
                                                        Convert.ToInt32(Session[ApplicationSession.TRUSTID].ToString()),
                                                        Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString()));
                                                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                                                    {
                                                        intSuccessCount = intSuccessCount + 1;
                                                    }
                                                    else
                                                    {
                                                        DatabaseTransaction.RollbackTransation();
                                                        break;
                                                    }
                                                }
                                                if (intSuccessCount == dtPurchaseT.Rows.Count)
                                                {
                                                    DatabaseTransaction.CommitTransation();
                                                    BindPurchaseGrid("", "", 0,
                                                        Convert.ToInt32(ViewState["PurchaseID"].ToString()));
                                                    ClearAll();
                                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                                        "<script>alert('Purchase Detail Saved Successfully.');</script>");
                                                }

                                                #endregion
                                            }
                                            else
                                            {
                                                DatabaseTransaction.RollbackTransation();
                                            }
                                        }
                                        else
                                        {
                                            DatabaseTransaction.RollbackTransation();
                                        }
                                    }
                                    else
                                    {
                                        Thread.Sleep(500);
                                        reConnect = true;
                                        j++;
                                        if (j > 40)
                                        {
                                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                                "<script>alert('Try again later.');</script>");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                    "<script>alert('You have to add a Material.');</script>");
                            }
                        }
                        catch (Exception ex)
                        {
                            DatabaseTransaction.RollbackTransation();
                            logger.Error("Error", ex);
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                        }
                    }
                    else if (ViewState["Mode"].ToString() == "Edit")
                    {
                        objPurchaseBo.PurchaseID = Convert.ToInt32(ViewState["PurchaseID"].ToString());
                        objResult = objPurchaseBl.Purchase_Update(objPurchaseBo);
                        if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {
                            hfTab.Value = "6";
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                "<script>alert('Basic Details Updated Successfully.');</script>");

                        }
                    }

                    hfMode.Value = "Save";
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Financial Year has been change. First Update the Stock from Stock Updation');</script>");
                }
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
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["PurchaseID"] = null;
            ViewState["Mode"] = "Save";
            DataTable dtTask = (DataTable)ViewState["dtMaterial"];
            dtTask.Rows.Clear();
            gvMaterial.DataSource = dtTask;
            gvMaterial.DataBind();
            ViewState["dtMaterial"] = dtTask;
            divGrid.Style.Add("display", "none");
            divPurchaseUpdate.Visible = false;
            divPurchaseSave.Visible = true;
        }
        #endregion

        #region Clear All Material Panel

        public void MaterialClearAll()
        {
            ddlMaterialGroupName.SelectedValue = "";
            ddlMaterialName.Items.Clear();
            ddlUOM.Items.Clear();
            txtQuantity.Text = "";
            txtPerUnitPrice.Text = "";
            txtMaterialTotalAmount.Text = "";
            //txtDeliveryNote.Text = "";
        }

        #endregion

        #region Bind Purchase Gridview

        public void BindPurchaseGrid(string strFromDate, string strToDate, int intFlag, int intPurchaseID)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                PurchaseBL objPurchaseBl = new PurchaseBL();

                objResult = objPurchaseBl.Purchase_Select(strFromDate, strToDate, intFlag, intPurchaseID, Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    gvPurchase.Visible = true;
                    gvPurchase.DataSource = objResult.resultDT;
                    gvPurchase.DataBind();

                    if (objResult.resultDT.Rows.Count > 0)
                    {

                    }
                    else
                    {

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

        #region Button btnGo Click Event
        protected void btnGo_OnClick(object sender, EventArgs e)
        {
            try
            {
                BindPurchaseGrid(txtFromDate.Text, txtToDate.Text, 1, 0);
                hfTab.Value = "6";
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                throw;
            }
        }
        #endregion

        #region Button Add Click
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                if (ViewState["Mode"].ToString() == "Save")
                {
                    gvMaterial.Columns[5].Visible = false;

                    DataTable dtMaterial = new DataTable();
                    dtMaterial = (DataTable)ViewState["dtMaterial"];

                    int i = 0;
                    if (dtMaterial.Rows.Count > 0)
                    {
                        i = dtMaterial.Rows.Count;
                    }
                    i = i + 1;

                    string strMaterial = hfMaterial.Value;
                    string strUOM = hfUOM.Value;
                    dtMaterial.Rows.Add(i, 0, Convert.ToInt32(ddlUOM.SelectedValue), ddlUOM.SelectedItem.Text,
                        Convert.ToInt32(ddlMaterialGroupName.SelectedValue), ddlMaterialGroupName.SelectedItem.Text,
                        Convert.ToInt32(ddlMaterialName.SelectedValue), ddlMaterialName.SelectedItem.Text,
                        Convert.ToInt32(txtQuantity.Text), Convert.ToDouble(txtPerUnitPrice.Text),
                        Convert.ToDouble(txtMaterialTotalAmount.Text));
                    ViewState["dtMaterial"] = dtMaterial;
                    gvMaterial.DataSource = dtMaterial;
                    gvMaterial.DataBind();
                    CalcTotalAmt();
                    ClearMaterialPanel();
                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    gvMaterial.Columns[5].Visible = true;
                    ApplicationResult objResult = new ApplicationResult();
                    PurchaseBL objPurchaseBl = new PurchaseBL();
                    PurchaseTBO objPurchaseTbo = new PurchaseTBO();

                    objPurchaseTbo.PurchaseID = Convert.ToInt32(ViewState["PurchaseID"].ToString());
                    objPurchaseTbo.UOMID = Convert.ToInt32(Request.Form[ddlUOM.UniqueID]);
                    objPurchaseTbo.MaterialID = Convert.ToInt32(Request.Form[ddlMaterialName.UniqueID]);
                    objPurchaseTbo.Quantity = Convert.ToInt32(txtQuantity.Text);
                    objPurchaseTbo.Rate = Convert.ToDouble(txtPerUnitPrice.Text);
                    objPurchaseTbo.TotalAmount = Convert.ToDouble(txtMaterialTotalAmount.Text);
                    objPurchaseTbo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    objPurchaseTbo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                    objPurchaseTbo.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    objPurchaseTbo.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);

                    objResult = objPurchaseBl.Purchase_T_Insert_withoutTransaction(objPurchaseTbo,
                        Convert.ToInt32(Session[ApplicationSession.TRUSTID]),
                        Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        BindMaterialGrid();
                        CalcTotalAmt();
                        ClearMaterialPanel();

                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Material Saved Successfully.');</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                    }
                }
                ddlMaterialGroupName.Enabled = true;
                ddlMaterialName.Enabled = true;

            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region clearMaterialPanel Method

        public void ClearMaterialPanel()
        {
            ddlMaterialGroupName.SelectedValue = "";
            ddlMaterialName.Items.Clear();
            ddlUOM.Items.Clear();
            txtQuantity.Text = "";
            txtPerUnitPrice.Text = "";
            txtMaterialTotalAmount.Text = "";
            ViewState["PurchaseTID"] = null;
            //UpdatePanel2.Update();
        }

        #endregion

        #region Bind Material Grid

        public void BindMaterialGrid()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                PurchaseBL objPurchaseBl = new PurchaseBL();

                objResult = objPurchaseBl.Purchase_T_Select_PurchaseID(Convert.ToInt32(ViewState["PurchaseID"].ToString()));
                if (objResult != null)
                {
                    divGrid.Style.Remove("display");
                    gvMaterial.DataSource = objResult.resultDT;
                    gvMaterial.DataBind();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        #endregion

        #region Method for Total amount Calculation

        public void CalcTotalAmt()
        {
            try
            {
                if (gvMaterial.Rows.Count >= 0)
                {
                    double dblTotalAmt = 0.0;
                    for (int j = 0; j < gvMaterial.Rows.Count; j++)
                    {
                        dblTotalAmt += Convert.ToDouble(gvMaterial.Rows[j].Cells[4].Text);
                    }
                    txtTotalAmount.Text = dblTotalAmt.ToString();
                    hfTotalamt.Value = dblTotalAmt.ToString();
                    divGrid.Style.Remove("display");
                    gvMaterial.Visible = true;
                }
                else
                {
                    gvMaterial.Visible = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        #endregion

        #region GridView gvMaterial Row Command Event
        protected void gvMaterial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ClearMaterialPanel();
                ApplicationResult objResults = new ApplicationResult();
                PurchaseBL objPurchaseBl = new PurchaseBL();

                ViewState["PurchaseTID"] = e.CommandArgument.ToString();

                if (e.CommandName.ToString() == "EditMaterial")
                {
                    if (ViewState["Mode"].ToString() == "Edit")
                    {
                        objResults = objPurchaseBl.Purchase_T_Select(Convert.ToInt32(ViewState["PurchaseTID"].ToString()));

                        if (objResults != null)
                        {
                            if (objResults.resultDT.Rows.Count > 0)
                            {
                                ddlMaterialGroupName.SelectedValue =
                                    objResults.resultDT.Rows[0]["MaterialGroupID"].ToString();
                                ddlMaterialGroupName_OnSelectedIndexChanged(sender, e);
                                ddlMaterialName.SelectedValue = objResults.resultDT.Rows[0]["MaterialID"].ToString();
                                ddlMaterialName_SelectedIndexChanged(sender, e);
                                ddlUOM.SelectedValue = objResults.resultDT.Rows[0]["UOMID"].ToString();

                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MaterialGroupChange();", true);
                                //ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "Sys.WebForms.PageRequestManager.getInstance().add_endRequest(MaterialGroupChange);", true);
                                //hfMaterialID.Value =
                                //objResults.resultDT.Rows[0]["MaterialID"].ToString();
                                //ScriptManager.RegisterStartupScript(this, GetType(), "myFunction1", "Sys.WebForms.PageRequestManager.getInstance().add_endRequest(getValue);", true);
                                //ScriptManager.RegisterStartupScript(this, GetType(), "myFunction2", "Sys.WebForms.PageRequestManager.getInstance().add_endRequest(MaterialChange);", true);
                                txtQuantity.Text = objResults.resultDT.Rows[0]["Quantity"].ToString();
                                txtPerUnitPrice.Text = objResults.resultDT.Rows[0]["Rate"].ToString();
                                txtMaterialTotalAmount.Text = objResults.resultDT.Rows[0]["TotalAmount"].ToString();
                                //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(getValue);

                                //ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>Sys.WebForms.PageRequestManager.getInstance().add_endRequest(MaterialGroupChange);</script>");
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "getValue();", true);
                                ddlMaterialGroupName.Enabled = false;
                                ddlMaterialName.Enabled = false;
                                btnUpdateMaterial.Visible = true;
                                btnAdd.Visible = false;
                                //UpdatePanel2.Update();
                            }
                        }
                    }
                    //if (ViewState["Mode"].ToString() == "Save")
                    //{
                    //    // GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    //    DataTable dtTaskTemp = (DataTable)ViewState["Division"];
                    //    //txtDivisionName.Text= dtTaskTemp.Rows[0][1].ToString();
                    //    //txtDivisionName.Text = Convert.ToString(row.RowIndex);
                    //    ViewState["DivisionDatatableTID"] = e.CommandArgument.ToString();
                    //    string strFilter = "DivisionTID = '" + e.CommandArgument.ToString() + "'";
                    //    DataRow[] results = dtTaskTemp.Select(strFilter);
                    //    if (results.CopyToDataTable().Rows.Count > 0)
                    //    {
                    //        txtDivisionName.Text = results.CopyToDataTable().Rows[0][1].ToString();
                    //    }
                    //    ViewState["DivisionMode"] = "Edit";
                    //}
                    //else
                    //{
                    //    objResults = objDivisionTBL.DivisionT_Select_By_DivisionTID(Convert.ToInt32(ViewState["DivisionTID"].ToString()));
                    //    if (objResults != null)
                    //    {
                    //        if (objResults.resultDT.Rows.Count > 0)
                    //        {
                    //            txtDivisionName.Text = objResults.resultDT.Rows[0][DivisionTBO.DIVISIONT_DIVISIONNAME].ToString();
                    //            ViewState["DivisionMode"] = "Edit";
                    //        }
                    //    }
                    //}
                }
                else if (e.CommandName == "DeleteMaterial")
                {
                    if (ViewState["Mode"].ToString() == "Save")
                    {
                        GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                        DataTable dtTaskTemp = (DataTable)ViewState["dtMaterial"];
                        dtTaskTemp.Rows.RemoveAt(row.RowIndex);
                        dtTaskTemp.AcceptChanges();
                        ViewState["dtMaterial"] = dtTaskTemp;
                        gvMaterial.DataSource = (DataTable)ViewState["dtMaterial"];
                        gvMaterial.DataBind();
                        CalcTotalAmt();
                        ClearMaterialPanel();
                    }
                    else
                    {
                        if (gvMaterial.Rows.Count > 1)
                        {
                            ViewState["PurchaseTID"] = Convert.ToInt32(e.CommandArgument.ToString());

                            objResults = objPurchaseBl.Purchase_T_Delete(Convert.ToInt32(e.CommandArgument.ToString()));
                            if (objResults != null)
                            {
                                //if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                                //{

                                //    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Material Deleted Successfully.');</script>");
                                //}
                                int intStatus = Convert.ToInt32(objResults.resultDT.Rows[0]["Status"].ToString());
                                if (intStatus == 1)
                                {
                                    BindMaterialGrid();
                                    CalcTotalAmt();
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Record deleted Successfully.');", true);
                                }
                                else if (intStatus == 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
                                }
                                else if (intStatus == 2)
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Your Quntity of deletion exceed to Main sock.');", true);
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You can't delete this material, because the purchase must contain atleast one material.');", true);
                            //ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('You have atleast one Material Purchase so, you can not delete this Material.');</script>");
                        }
                        //    if (objResults != null)
                        //    {
                        //        if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                        //        {
                        //            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Division Deleted Successfully.');</script>");
                        //        }
                        //        else
                        //        {
                        //            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('There are Division(s) associated with this Student. To delete this Divisions you need to delete Student(s) first.');</script>");
                        //        }

                        //    }
                        //    objResults = objDivisionTBL.DivisionT_Select_DivisionName_By_Class(Convert.ToInt32(ViewState["ClassMID"].ToString()));
                        //    if (objResults != null)
                        //    {
                        //        //  ViewState["Division"] = objResults.resultDT;
                        //        // gvDivision.DataSource = (DataTable)ViewState["Division"];
                        //        gvDivision.DataSource = objResults.resultDT;
                        //        gvDivision.DataBind();
                        //    }
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

        #region Bind Material Grid
        //public void BindMaterialGrid()
        //{
        //    try
        //    {
        //        ApplicationResult objResult = new ApplicationResult();
        //        PurchaseBL objPurchaseBl = new PurchaseBL();

        //        objResult = objPurchaseBl.Purchase_T_Select(Convert.ToInt32(ViewState["PurchaseTID"].ToString()));
        //        if (objResult != null)
        //        {
        //            gvMaterial.DataSource = objResult.resultDT;
        //            gvMaterial.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Error", ex);
        //        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
        //    }
        //}
        #endregion

        #region Developed By Nafisa Mulla
        #region Bind Transfer Gridview

        public void BindTransferGrid(string strFromDate, string strToDate)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                MaterialTransferBL objMTransferBl = new MaterialTransferBL();

                objResult = objMTransferBl.MaterialTransfer_Select(strFromDate, strToDate, Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    gvTransfer.Visible = true;
                    gvTransfer.DataSource = objResult.resultDT;
                    gvTransfer.DataBind();

                    if (objResult.resultDT.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Records Found.');", true);
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


        #region Web Service Get Material Quantity
        [System.Web.Services.WebMethod]
        public static string GetMaterialQuantity(int MaterialID, int TrustID, int SchoolID)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                MaterialBL objMaterialBl = new MaterialBL();

                objResult = objMaterialBl.Material_Select_ForPurchase(MaterialID, TrustID, SchoolID);
                string res = "";
                res = DataSetToJSON(objResult.resultDT);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region View School Grid in View Button Click
        protected void btnViewSchools_Click(object sender, EventArgs e)
        {
            try
            {
                gvSchoolForMaterial.Visible = false;
                if (hfValidate.Value == "1")
                {

                    hfMaterialGroup.Value = ddlTMaterialGroup.SelectedValue;
                    ViewState["MaterialNameID"] = Convert.ToInt32(Request.Form[ddlTMaterialName.UniqueID]);
                    ViewState["UomTID"] = Convert.ToInt32(Request.Form[ddlTUOM.UniqueID]);
                    hfMaterialName.Value = ViewState["MaterialNameID"].ToString();
                    hfUOM.Value = ViewState["UomTID"].ToString();
                    hfAvailableQuantity.Value = txtAvailableQuantity.Text;
                    // ViewState["DivisionName"] = Convert.ToInt32(Request.Form[ddlTMaterialName.UniqueID]);
                    ApplicationResult objResult = new ApplicationResult();
                    SchoolBL objMaterialGroupBL = new SchoolBL();

                    objResult =
                        objMaterialGroupBL.School_SelectAll_ForDropDOwn(
                            Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                    if (objResult != null)
                    {
                        gvSchoolForMaterial.DataSource = objResult.resultDT;
                        gvSchoolForMaterial.DataBind();

                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            gvSchoolForMaterial.Visible = true;
                        }
                        else
                        {
                            gvSchoolForMaterial.Visible = false;

                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Records Found.');", true);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion

        #region Button btnTransfer  Click Event
        protected void btnTrnasfer_Click(object sender, EventArgs e)
        {
            try
            {
                if (hfValidate.Value == "1")
                {
                    MaterialTransferBO objMaterialTransferBO = new MaterialTransferBO();
                    MaterialTransferBL objMaterialTransferBL = new MaterialTransferBL();
                    ApplicationResult objResults = new ApplicationResult();
                    MaterialBL objMaterialBl = new MaterialBL();

                    objResults = objMaterialBl.Material_Select_ForStockUpdate();
                    if (objResults.resultDT.Rows[0][0].ToString() == "0")
                    {


                        objMaterialTransferBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                        objMaterialTransferBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                        if (rblSchoolOrTrust.SelectedValue == "0")
                        {
                            objMaterialTransferBO.TransferTo = "S";
                        }
                        else
                        {
                            objMaterialTransferBO.TransferTo = "T";
                        }
                        objMaterialTransferBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        objMaterialTransferBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                        objMaterialTransferBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        objMaterialTransferBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                        if (rblMaterialSelection.SelectedValue == "0")
                        {
                            foreach (GridViewRow row in gvSchoolForMaterial.Rows)
                            {

                                if (((CheckBox) row.FindControl("chkChild")).Checked)
                                {
                                    objMaterialTransferBO.MaterialTID =
                                        Convert.ToInt32(ViewState["MaterialNameID"].ToString());
                                    objMaterialTransferBO.Quantity =
                                        Convert.ToInt32(((TextBox) row.FindControl("txtQuantity")).Text);
                                    objMaterialTransferBO.UOMID = Convert.ToInt32(ViewState["UomTID"].ToString());
                                    objMaterialTransferBO.TransferToID = Convert.ToInt32(row.Cells[0].Text);

                                    objResults = objMaterialTransferBL.MaterialTransfer_Insert(objMaterialTransferBO);
                                }
                            }
                        }
                        else
                        {
                            foreach (GridViewRow row in gvMultipleMaterial.Rows)
                            {
                                DropDownList ddlgridSchool = (DropDownList) row.Cells[7].FindControl("ddlGridSchool");

                                objMaterialTransferBO.MaterialTID = Convert.ToInt32(row.Cells[0].Text);
                                objMaterialTransferBO.Quantity =
                                    Convert.ToInt32(((TextBox) row.FindControl("txtQuantity")).Text);
                                objMaterialTransferBO.UOMID = Convert.ToInt32(row.Cells[2].Text);
                                objMaterialTransferBO.TransferToID = Convert.ToInt32(ddlgridSchool.SelectedValue);

                                objResults = objMaterialTransferBL.MaterialTransfer_Insert(objMaterialTransferBO);
                            }
                        }
                        if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {
                            string strMessage = objResults.resultDT.Rows[0]["Message"].ToString();
                            if (strMessage != "" && strMessage != null)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                                    "alert('" + strMessage + "');",
                                    true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                                    "alert('Material(s) transfered Successfully');", true);
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction",
                                    "ClearOnTransfer();", true);
                                ViewState["MaterialNameID"] = null;
                                ViewState["UomTID"] = null;
                                ViewState["MaterialIDs"] = null;
                                hfTab.Value = "1";

                            }
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Financial Year has been change. First Update the Stock from Stock Updation');</script>");
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

        #region Transfer BtnGoTransfer Click Event
        protected void BtnGoTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                BindTransferGrid(txtTransferFromDate.Text, txtTransferTodate.Text);
                hfTab.Value = "2";
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion


        #region Add Multiple Material In gridview
        protected void btnAddMaterial_Click(object sender, EventArgs e)
        {
            string listValues = "";
            gvSchoolForMaterial.Visible = false;
            try
            {
                if (hfValidate.Value == "1")
                {
                    hfMaterialGroup.Value = ddlTMaterialGroup.SelectedValue;
                    string leftSelectedItems = Request.Form[lbMaterial.UniqueID];
                    lbMaterial.Items.Clear();
                    ViewState["MaterialIDs"] = leftSelectedItems;
                    //if (!string.IsNullOrEmpty(leftSelectedItems))
                    //{
                    //    // foreach (string item in leftSelectedItems.Split(','))
                    //    //  {
                    //    //  lbMaterial.Items.Add(item);
                    //    //  }
                    //    //  ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('"+leftSelectedItems+"');</script>");
                    //}
                    ApplicationResult objResult = new ApplicationResult();
                    MaterialBL objMaterialBL = new MaterialBL();

                    objResult = objMaterialBL.Material_M_Select_ForMultipleMaterialTransfer(leftSelectedItems,
                        Session[ApplicationSession.TRUSTID].ToString(), Session[ApplicationSession.SCHOOLID].ToString());
                    if (objResult != null)
                    {
                        gvMultipleMaterial.DataSource = objResult.resultDT;
                        gvMultipleMaterial.DataBind();

                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            gvMultipleMaterial.Visible = true;
                        }
                        else
                        {
                            gvMultipleMaterial.Visible = false;

                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Records Found.');",
                                true);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }

        #endregion

        #region Row Bound Event of Multiple Material Gridview
        protected void gvMultipleMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                SchoolBL objSchoolBl = new SchoolBL();
                ApplicationResult objResults = new ApplicationResult();
                Controls objControls = new Controls();


                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlSchool = (DropDownList)e.Row.FindControl("ddlGridSchool");

                    objResults = objSchoolBl.School_SelectAll_ForDropDOwn(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            objControls.BindDropDown_ListBox(objResults.resultDT, ddlSchool, "SchoolNameEng", "SchoolMID");
                        }

                        ddlSchool.Items.Insert(0, new ListItem("-Select-", ""));
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

        //For Consumption and Return


        #region Go Click Event For Consumption Search
        protected void btnGoConsume_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFromConsume.Text != "" && txtToConsume.Text != "")
                {
                    BindConsumptionsGrid(txtFromConsume.Text, txtToConsume.Text, 1);
                    hfTab.Value = "3";
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Plz Select All Fields For Search.');</script>");
                    hfTab.Value = "3";
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        public void BindConsumptionsGrid(string strFromDate, string strToDate, int intTypeID)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                ConsumptionBL objConsumptionBl = new ConsumptionBL();

                objResult = objConsumptionBl.Consumption_SelectByType(strFromDate, strToDate, Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    gvConsumption.DataSource = objResult.resultDT;
                    gvConsumption.DataBind();

                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvConsumption.Visible = true;
                    }
                    else
                    {
                        gvConsumption.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Records Found.');", true);
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

        #region Gridview gvPurchase Row Command Event
        protected void gvPurchase_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                PurchaseBL objpPurchaseBl = new PurchaseBL();
                ViewState["PurchaseID"] = e.CommandArgument.ToString();

                if (e.CommandName.ToString() == "EditPurchase")
                {
                    objResult = objpPurchaseBl.Purchase_Select("", "", 0,
                        Convert.ToInt32(ViewState["PurchaseID"].ToString()), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                    if (objResult != null)
                    {
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            ddlVendorName.SelectedValue =
                                objResult.resultDT.Rows[0]["VendorID"].ToString();
                            txtPONo.Text = objResult.resultDT.Rows[0]["PONO"].ToString();
                            txtPoDate.Text = objResult.resultDT.Rows[0]["PODate"].ToString();
                            txtBillDate.Text = objResult.resultDT.Rows[0]["BillDate"].ToString();
                            txtBillNo.Text = objResult.resultDT.Rows[0]["BillNO"].ToString();
                            txtVoucharNo.Text = objResult.resultDT.Rows[0]["VoucharNo"].ToString();
                            txtVoucharDate.Text = objResult.resultDT.Rows[0]["VoucharDate"].ToString();
                            ddlPaymentType.SelectedItem.Text = objResult.resultDT.Rows[0]["PaymentType"].ToString();
                            ddlPaymentMode.SelectedItem.Text = objResult.resultDT.Rows[0]["PaymentMode"].ToString();
                            txtVAT.Text = objResult.resultDT.Rows[0]["VAT"].ToString();
                            txtAddVAT.Text = objResult.resultDT.Rows[0]["AddVAT"].ToString();
                            txtCST.Text = objResult.resultDT.Rows[0]["CST"].ToString();
                            txtDiscount.Text = objResult.resultDT.Rows[0]["Discount"].ToString();
                            txtTotalAmount.Text = objResult.resultDT.Rows[0]["TotalAmount"].ToString();

                            objResult =
                                objpPurchaseBl.Purchase_T_Select_PurchaseID(Convert.ToInt32(ViewState["PurchaseID"].ToString()));
                            if (objResult != null)
                            {
                                divGrid.Style.Remove("display");
                                gvMaterial.Visible = true;
                                gvMaterial.DataSource = objResult.resultDT;
                                gvMaterial.DataBind();
                            }
                            divPurchaseUpdate.Visible = true;
                            divPurchaseSave.Visible = false;
                            hfMode.Value = "Edit";
                            ViewState["Mode"] = hfMode.Value;
                            hfTab.Value = "8";
                        }
                    }
                }
                else if (e.CommandName.ToString() == "DeletePurchase")
                {
                    objResult = objpPurchaseBl.Purchase_Delete(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (objResult != null)
                    {
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            int intStatus = Convert.ToInt32(objResult.resultDT.Rows[0]["Status"].ToString());
                            if (intStatus > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Record deleted Successfully.');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
                            }
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

        #region Calculation of Total Amount

        private double calcTotalAmt()
        {
            double dblTotalAmt = 0.0;

            double dblQuantity = !string.IsNullOrEmpty(txtQuantity.Text) ? Convert.ToDouble(txtQuantity.Text) : 0.0;
            double dblRate = !string.IsNullOrEmpty(txtPerUnitPrice.Text) ? Convert.ToDouble(txtPerUnitPrice.Text) : 0.0;
            dblTotalAmt = dblQuantity * dblRate;
            return dblTotalAmt;
        }

        #endregion

        #region Textbox txtQuantity Text Changed Event
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtMaterialTotalAmount.Text = calcTotalAmt().ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "myFunction1", "Validation();", true);
                //UpdatePanel2.Update();
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Textbox txtPerUnitPrice Text Changed Event
        protected void txtPerUnitPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtMaterialTotalAmount.Text = calcTotalAmt().ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "myFunction1", "Validation();", true);
                //UpdatePanel2.Update();
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region DropDownList ddlMaterialGroupName Selected Index changed Event
        protected void ddlMaterialGroupName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlMaterialName.Items.Clear();
                ApplicationResult objResult = new ApplicationResult();
                MaterialBL objMaterialBl = new MaterialBL();
                Controls objControls = new Controls();

                objResult = objMaterialBl.Material_SelectAll_ForDropDown(Convert.ToInt32(ddlMaterialGroupName.SelectedValue), Convert.ToInt32(Session[ApplicationSession.TRUSTID].ToString()), Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString()));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlMaterialName, "MaterialName", "MaterialID");
                    }
                    ddlMaterialName.Items.Insert(0, new ListItem("--Select--", ""));
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "myFunction1", "Validation();", true);
                //UpdatePanel2.Update();
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region DropDownList ddlMaterialName SelectedIndexChanged Event
        protected void ddlMaterialName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlUOM.Items.Clear();
                ApplicationResult objResult = new ApplicationResult();
                UOMBL objUombl = new UOMBL();
                Controls objControls = new Controls();

                objResult = objUombl.UOM_Select_DropDown(Convert.ToInt32(ddlMaterialName.SelectedValue));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlUOM, "UOMName", "UOMID");
                    }
                    //ddlUOM.Items.Insert(0, new ListItem("--Select--", ""));
                }
                //UpdatePanel2.Update();
                ScriptManager.RegisterStartupScript(this, GetType(), "myFunction1", "Validation();", true);
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

                ApplicationResult objResult = new ApplicationResult();
                PurchaseBL objPurchaseBl = new PurchaseBL();
                PurchaseTBO objPurchaseTbo = new PurchaseTBO();

                objPurchaseTbo.PurchaseTID = Convert.ToInt32(ViewState["PurchaseTID"].ToString());
                objPurchaseTbo.PurchaseID = Convert.ToInt32(ViewState["PurchaseID"].ToString());
                objPurchaseTbo.Quantity = Convert.ToInt32(txtQuantity.Text);
                objPurchaseTbo.Rate = Convert.ToDouble(txtPerUnitPrice.Text);
                objPurchaseTbo.TotalAmount = Convert.ToDouble(txtMaterialTotalAmount.Text);
                objPurchaseTbo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                objPurchaseTbo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);

                objResult = objPurchaseBl.Purchase_T_Update(objPurchaseTbo);
                if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                {
                    int intMessage = !string.IsNullOrEmpty(objResult.resultDT.Rows[0]["Message"].ToString())
                        ? Convert.ToInt32(objResult.resultDT.Rows[0]["Message"].ToString())
                        : 0;
                    if (intMessage == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction1",
                            "<script>alert('You can not update this material because entered quantity is greater than stock.');</script>",
                            true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction1",
                            "<script>alert('Material updated Successfully.');</script>", true);
                        ClearMaterialPanel();
                        ddlMaterialGroupName.Enabled = true;
                        ddlMaterialName.Enabled = true;
                        btnUpdateMaterial.Visible = false;
                        btnAdd.Visible = true;
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

        #region gvConsumption RowCommand Event
        protected void gvConsumption_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResults = new ApplicationResult();
                Controls objControls = new Controls();
                ConsumptionBL objConsumptionBl = new ConsumptionBL();
                MaterialBL objMaterialBl = new MaterialBL();

                //  ViewState["ConsumptionID"] = Convert.ToInt32(e.CommandArgument.ToString());
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                ViewState["ConsumptionID"] = commandArgs[0];
                ViewState["MaterialID"] = commandArgs[1];

                if (e.CommandName.ToString() == "DeleteConsume")
                {
                    objResults = objConsumptionBl.Consumption_Delete(Convert.ToInt32(ViewState["ConsumptionID"].ToString()), Convert.ToInt32(ViewState["MaterialID"].ToString()), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record Successfully Deleted.');</script>");
                    }
                    BindConsumptionsGrid(txtFromConsume.Text, txtToConsume.Text, 1);
                    hfTab.Value = "3";
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region btnSaveConsume Click Event
        protected void btnSaveConsume_Click(object sender, EventArgs e)
        {
            try
            {
                if (hfValidate.Value == "1")
                {
                    ConsumptionBO objConsumptionBo = new ConsumptionBO();
                    ConsumptionBL objConsumptionBl = new ConsumptionBL();
                    MaterialTransferBL objMaterialTransferBL = new MaterialTransferBL();
                    ApplicationResult objResults = new ApplicationResult();

                    objConsumptionBo.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                    objConsumptionBo.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                    objConsumptionBo.IsConsumption = 1;

                    objConsumptionBo.ConsumptionDate = txtDate.Text;
                    objConsumptionBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    objConsumptionBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                    DatabaseTransaction.OpenConnectionTransation();
                    foreach (GridViewRow row in gvMaterialConsume.Rows)
                    {

                        objConsumptionBo.MaterialID = Convert.ToInt32(row.Cells[0].Text);
                        objConsumptionBo.Quantity =
                            Convert.ToInt32(((TextBox)row.FindControl("txtQuantityConsume")).Text);
                        objConsumptionBo.UOMID = Convert.ToInt32(row.Cells[2].Text);
                        hfMode.Value = ViewState["Mode"].ToString();
                        if (ViewState["Mode"].ToString() == "Save")
                        {
                            objConsumptionBo.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                            objConsumptionBo.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);

                            objResults = objConsumptionBl.Consumption_Insert(objConsumptionBo);
                            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                // ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record Successfully Saved.');</script>");

                            }
                        }
                    }
                    DatabaseTransaction.CommitTransation();
                    ViewState["MaterialNameID"] = null;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Record Successfully Saved.'); ClearConsumption();", true);
                    // hfTab.Value = "4"
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Stock is never be zero.');", true);
                }
            }
            catch (Exception ex)
            {
                DatabaseTransaction.RollbackTransation();
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        //#region BtnAddNewConsume Click Event
        //protected void btnAddNewConsume_Click(object sender, EventArgs e)
        //{
        //    ViewState["Mode"] = "Save";
        //    hfTab.Value = "5";
        //   // ScriptManager.RegisterStartupScript(this, GetType(), "myFunction1", "btnAddNew();", true);
        //}
        //#endregion

        //#region btnViewListConsume Click Event
        //protected void btnViewListConsume_Click(object sender, EventArgs e)
        //{
        //    hfTab.Value = "3";
        //}
        //#endregion

        #region Add Material Click Event For Consumption
        protected void btnAddMaterialConsume_Click(object sender, EventArgs e)
        {
            string listValues = "";

            try
            {
                if (hfValidate.Value == "1")
                {
                    hfMaterialGroupConsume.Value = ddlMaterialGroupConsume.SelectedValue;
                    string leftSelectedItems = Request.Form[lbMaterialConsume.UniqueID];
                    lbMaterialConsume.Items.Clear();
                    ViewState["MaterialIDsConsume"] = leftSelectedItems;

                    ApplicationResult objResult = new ApplicationResult();
                    MaterialBL objMaterialBL = new MaterialBL();

                    objResult = objMaterialBL.Material_M_Select_ForMultipleMaterialTransfer(leftSelectedItems,
                        Session[ApplicationSession.TRUSTID].ToString(), Session[ApplicationSession.SCHOOLID].ToString());
                    if (objResult != null)
                    {
                        gvMaterialConsume.DataSource = objResult.resultDT;
                        gvMaterialConsume.DataBind();

                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            gvMaterialConsume.Visible = true;
                            btnSaveConsume.Visible = true;
                        }
                        else
                        {
                            gvMaterialConsume.Visible = false;
                            btnSaveConsume.Visible = false;
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Records Found.');",
                                true);
                        }
                    }
                }
                else
                {
                    gvMaterialConsume.Visible = false;
                    btnSaveConsume.Visible = false;

                }

            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion

        #region btnAddNewMain_Click
        protected void btnAddNewMain_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}