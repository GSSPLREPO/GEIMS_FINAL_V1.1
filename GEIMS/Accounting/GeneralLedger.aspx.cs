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
    public partial class GeneralLedger : PageBase
    {
        #region declaration
        private static ILog logger = LogManager.GetLogger(typeof(GeneralLedger));
        Controls objControl = new Controls();

        #endregion

        #region Pre-Init Method
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
                        btnGoBack.Visible = false;
                        ViewState["Mode"] = "Save";
                        PanelVisibility(1);
                        BindGrid();                       

                        if (Request.QueryString["mode"] == "TU")
                        {
                            lblDuration.Text = Session[ApplicationSession.TRUSTNAME].ToString() + ". Account Duration : " + Session[ApplicationSession.ACCOUNTFROMDATE].ToString() + " To " + Session[ApplicationSession.ACCOUNTTODATE].ToString();
                        }
                        else
                        {
                            if (Convert.ToInt32(Session[ApplicationSession.SCHOOLID]) == 0)
                            {
                                lblDuration.Text = Session[ApplicationSession.TRUSTNAME].ToString() + ". Account Duration : " + Session[ApplicationSession.ACCOUNTFROMDATE].ToString() + " To " + Session[ApplicationSession.ACCOUNTTODATE].ToString();
                            }
                            else
                                lblDuration.Text = Session[ApplicationSession.SCHOOLNAME].ToString() + ". Account Duration : " + Session[ApplicationSession.ACCOUNTFROMDATE].ToString() + " To " + Session[ApplicationSession.ACCOUNTTODATE].ToString();
                        }
                        if (Request.QueryString["modetype"] == "new" && Request.QueryString["page"] == "receipt")
                        {
                            btnGoBack.Visible = true;
                            btnGoBack.Text = "Go Back to Receipt";
                            PanelVisibility(2);
                            BindAccountGroup();
                        }
                        if (Request.QueryString["modetype"] == "new" && Request.QueryString["page"] == "payment")
                        {
                            btnGoBack.Visible = true;
                            btnGoBack.Text = "Go Back to Payment";
                            PanelVisibility(2);
                            BindAccountGroup();
                        }

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
        public void BindGrid()
        {
            try
            {
                GeneralLedgerBL objGeneralLedgerBL = new GeneralLedgerBL();
                GeneralLedgerBO objGeneralLedgerBO = new GeneralLedgerBO();
                ApplicationResult objResultSelectAll = new ApplicationResult();

                objResultSelectAll = objGeneralLedgerBL.GeneralLedger_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResultSelectAll != null)
                {
                    DataTable dtSelectAll = new DataTable();
                    dtSelectAll = objResultSelectAll.resultDT;
                    if (dtSelectAll.Rows.Count > 0)
                    {
                        gvGeneralLedger.DataSource = dtSelectAll;
                        gvGeneralLedger.DataBind();
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

        #region Bind Account Group
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
                    objControl.BindDropDown_ListBox(dtSelectAll, ddlAccountGroup, "AccountGroupName", "AccountGroupID");
                    ddlAccountGroup.Items.Insert(0, new ListItem("--Select--", ""));
                }
            }
        }
        #endregion

        #region Add New Button
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            ClearAll();
            BindAccountGroup();
            PanelVisibility(2);
        }
        #endregion

        #region View List Button
        protected void btnViewList_Click(object sender, EventArgs e)
        {
            ClearAll();
            BindGrid();
            PanelVisibility(1);
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
        }
        #endregion

        #region Save Button
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                GeneralLedgerBL objGeneralLedgerBL = new GeneralLedgerBL();
                GeneralLedgerBO objGeneralLedgerBO = new GeneralLedgerBO();
                ApplicationResult objResultValidate = new ApplicationResult();
                ApplicationResult objResultSelect = new ApplicationResult();
                int intLedgerID = 0;

                objGeneralLedgerBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                objGeneralLedgerBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                objGeneralLedgerBO.AccountName = txtAccountName.Text;
                objGeneralLedgerBO.AccountGroupID = Convert.ToInt32(ddlAccountGroup.SelectedValue);
                if (txtOpeningBalance.Text.Length > 0)
                    objGeneralLedgerBO.OpeningBalance = Convert.ToDouble(txtOpeningBalance.Text);
                if (ddlBalanceType.SelectedValue != "")
                    objGeneralLedgerBO.BalanceType = ddlBalanceType.SelectedValue;
                objGeneralLedgerBO.Description = txtDescription.Text;
                objGeneralLedgerBO.CreatedDate = System.DateTime.UtcNow.AddHours(5.5).ToString();
                objGeneralLedgerBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                objGeneralLedgerBO.IsDeleted = 0;
                objGeneralLedgerBO.LastModifideDate = System.DateTime.UtcNow.AddHours(5.5).ToString();
                objGeneralLedgerBO.LastModifideUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);

                //Code For Validate Department Name
                if (ViewState["Mode"].ToString() == "Save")
                {
                    intLedgerID = -1;
                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    intLedgerID = Convert.ToInt32(ViewState["LedgerID"].ToString());
                    objGeneralLedgerBO.LedgerID = Convert.ToInt32(ViewState["LedgerID"].ToString());
                }
                objResultValidate = objGeneralLedgerBL.GeneralLedger_ValidateName(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), txtAccountName.Text, intLedgerID);

                if (objResultValidate != null)
                {
                    DataTable dtValidate = new DataTable();
                    dtValidate = objResultValidate.resultDT;
                    if (dtValidate.Rows.Count > 0)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Ledger name already exist.');</script>");
                    }
                    else
                    {
                        if (ViewState["Mode"].ToString() == "Save")
                        {
                            ApplicationResult objResultSave = new ApplicationResult();
                            objResultSave = objGeneralLedgerBL.GeneralLedger_Insert(objGeneralLedgerBO, Convert.ToInt32(Session[ApplicationSession.FINANCIALYEAR]));
                            if (objResultSave.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record Saved successfully.');</script>");
                            }
                        }
                        else if (ViewState["Mode"].ToString() == "Edit")
                        {
                            //IF LedgerID is pass out another table Recrod can not delete or Edit
                            objResultSelect = objGeneralLedgerBL.GeneralLedger_M_Cascade(Convert.ToInt32(ViewState["LedgerID"].ToString()), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                            if (objResultSelect != null)
                            {
                                DataTable dtResult1 = objResultSelect.resultDT;
                                int col = Convert.ToInt32(dtResult1.Rows[0][0]);
                                //if (dtResult1.Rows.Count > 0)
                                if (col == 0)
                                {
                                    ApplicationResult objResultUpdate = new ApplicationResult();
                                    objResultUpdate = objGeneralLedgerBL.GeneralLedger_Update(objGeneralLedgerBO, Session[ApplicationSession.ACCOUNTFROMDATE].ToString());
                                    if (objResultUpdate.status == ApplicationResult.CommonStatusType.SUCCESS)
                                    {
                                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record updated successfully.');</script>");
                                    }
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You can not changed the record. It is already in use.');</script>");
                                }
                            }
                            else
                            {                              
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! LedgerID is Null!.');</script>");
                            }                          
                        }
                        ClearAll();
                        BindGrid();
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

        #region Gridview Event
        protected void gvGeneralLedger_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GeneralLedgerBL objGeneralLedgerBL = new GeneralLedgerBL();
                GeneralLedgerBO objGeneralLedgerBO = new GeneralLedgerBO();
                ApplicationResult objResultSelect = new ApplicationResult();
                ApplicationResult objResultDelete = new ApplicationResult();
                ViewState["LedgerID"] = e.CommandArgument.ToString();

                if (e.CommandName.ToString() == "Edit1")
                {
                    objResultSelect = objGeneralLedgerBL.GeneralLedger_Select(Convert.ToInt32(ViewState["LedgerID"].ToString()), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                    if (objResultSelect != null)
                    {
                        DataTable dtResult = objResultSelect.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            BindAccountGroup();
                            txtAccountName.Text = dtResult.Rows[0][GeneralLedgerBO.GENERALLEDGER_ACCOUNTNAME].ToString();
                            ddlAccountGroup.SelectedValue = dtResult.Rows[0][GeneralLedgerBO.GENERALLEDGER_ACCOUNTGROUPID].ToString();
                            txtOpeningBalance.Text = dtResult.Rows[0][GeneralLedgerBO.GENERALLEDGER_OPENINGBALANCE].ToString();
                            if (dtResult.Rows[0][GeneralLedgerBO.GENERALLEDGER_BALANCETYPE].ToString() != "")
                                ddlBalanceType.SelectedValue = dtResult.Rows[0][GeneralLedgerBO.GENERALLEDGER_BALANCETYPE].ToString();
                            txtDescription.Text = dtResult.Rows[0][GeneralLedgerBO.GENERALLEDGER_DESCRIPTION].ToString();
                            PanelVisibility(2);
                            ViewState["Mode"] = "Edit";
                        }
                    }
                }
                else if (e.CommandName.ToString() == "Delete1")
                {
                    //IF LedgerID is pass out another table Recrod can not delete or Edit
                    objResultSelect = objGeneralLedgerBL.GeneralLedger_M_Cascade(Convert.ToInt32(ViewState["LedgerID"].ToString()), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                    if (objResultSelect != null)
                    {
                        DataTable dtResult1 = objResultSelect.resultDT;
                        int col = Convert.ToInt32(dtResult1.Rows[0][0]);
                        //if (dtResult1.Rows.Count > 0)
                        if (col == 0)
                        {
                            objResultDelete = objGeneralLedgerBL.GeneralLedger_Delete(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session[ApplicationSession.USERID]), DateTime.UtcNow.AddHours(5.5).ToString(), "Ledger");
                            if (objResultDelete.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record deleted successfully.');</script>");
                                PanelVisibility(1);
                                BindGrid();
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record deleted not successfully.');</script>");
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You can not delete this record. It is already in used.');</script>");
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! LedgerID is Null!.');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        protected void gvGeneralLedger_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvGeneralLedger_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvGeneralLedger.Rows.Count > 0)
                {
                    gvGeneralLedger.UseAccessibleHeader = true;
                    gvGeneralLedger.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Button Go Back
        protected void btnGoBack_OnClick(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Session[ApplicationSession.SCHOOLID]) == 0)
                Response.Redirect(btnGoBack.Text == "Go Back to Receipt" ? "Receipt.aspx?mode=TU&modetype=new" : "Payment.aspx?mode=TU&modetype=new", false);
            else
                Response.Redirect(btnGoBack.Text == "Go Back to Receipt" ? "Receipt.aspx?mode=new" : "Payment.aspx?mode=new", false);

        }

        #endregion
       
    }
}