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
    public partial class Vendor : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(Vendor));

        #region Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    GridDataBind();
                    ViewState["Mode"] = "Save";
                    ViewState["VendorID"] = 0;
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
                VendorBL objUOMBL = new VendorBL();

                objResult = objUOMBL.Vendor_SelectAll();
                if (objResult != null)
                {
                    gvVendor.DataSource = objResult.resultDT;
                    gvVendor.DataBind();

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

        #region PanelGrid_VisibilityMode

        //panel grid true & false according to condition
        private void PanelGrid_VisibilityMode(int intMode)
        {
            if (intMode == 1)
            {
                divGrid.Visible = true;
                tabs.Visible = false;
                lnkViewList.Visible = false;
                lnkAddNewVendor.Visible = true;
            }
            else if (intMode == 2)
            {
                divGrid.Visible = false;
                tabs.Visible = true;
                lnkViewList.Visible = true;
                lnkAddNewVendor.Visible = true;
            }
        }

        #endregion PanelGrid_VisibilityMode

        #region Link Add new Vendeor Click Event
        protected void lnkAddNewVendor_Click(object sender, EventArgs e)
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            PanelGrid_VisibilityMode(2);
        }
        #endregion

        #region  GridView Events [RowCommand, PreRender]
        protected void gvVendor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Controls objControls = new Controls();
                ApplicationResult objResults = new ApplicationResult();
                VendorBL objVendorBL = new VendorBL();

                ViewState["VendorID"] = Convert.ToInt32(e.CommandArgument.ToString());
                if (e.CommandName.ToString() == "Edit1")
                {
                    objResults = objVendorBL.Vendor_Select(Convert.ToInt32(ViewState["VendorID"].ToString()));

                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            txtVendorName.Text = objResults.resultDT.Rows[0][VendorBO.VENDOR_VENDORNAME].ToString();
                            txtAddress.Text = objResults.resultDT.Rows[0][VendorBO.VENDOR_ADDRESS].ToString();
                            txtTelephoneNo.Text = objResults.resultDT.Rows[0][VendorBO.VENDOR_TELEPHONENO].ToString();
                            txtMobileNo.Text = objResults.resultDT.Rows[0][VendorBO.VENDOR_MOBILENO].ToString();
                            txtFax.Text = objResults.resultDT.Rows[0][VendorBO.VENDOR_FAX].ToString();
                            txtEmail.Text = objResults.resultDT.Rows[0][VendorBO.VENDOR_EMAILID].ToString();
                            txtTinGst.Text = objResults.resultDT.Rows[0][VendorBO.VENDOR_TINGST].ToString();
                            txtTinCst.Text = objResults.resultDT.Rows[0][VendorBO.VENDOR_TINCST].ToString();
                            txtBankName.Text = objResults.resultDT.Rows[0][VendorBO.VENDOR_BANKNAME].ToString();
                            txtAccountNo.Text = objResults.resultDT.Rows[0][VendorBO.VENDOR_ACCOUNTNO].ToString();
                            txtAccountName.Text = objResults.resultDT.Rows[0][VendorBO.VENDOR_ACCOUNTNAME].ToString();
                            txtIFSCCode.Text = objResults.resultDT.Rows[0][VendorBO.VENDOR_IFSCCODE].ToString();
                            txtPanNo.Text = objResults.resultDT.Rows[0][VendorBO.VENDOR_PANNO].ToString();
                            txtTaxRegNo.Text = objResults.resultDT.Rows[0][VendorBO.VENDOR_TAXREGNO].ToString();
                            ViewState["Mode"] = "Edit";
                            PanelGrid_VisibilityMode(2);
                        }
                    }
                }
                else if ((e.CommandName.ToString() == "Delete1"))
                {
                    objResults = objVendorBL.Vendor_Delete(Convert.ToInt32(ViewState["VendorID"].ToString()));
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClearAll();
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Vendor deleted successfully.');</script>");
                        GridDataBind();
                        PanelGrid_VisibilityMode(1);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Vendor is not deleted because it is in use.');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        protected void gvVendor_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvVendor.Rows.Count > 0)
                {
                    gvVendor.UseAccessibleHeader = true;
                    gvVendor.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Button Save Click Event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResults = new ApplicationResult();
                VendorBO objVendorBO = new VendorBO();
                VendorBL objVendorBL = new VendorBL();

                objVendorBO.VendorName = txtVendorName.Text;
                objVendorBO.Address = txtAddress.Text;
                objVendorBO.TelephoneNo = txtTelephoneNo.Text;
                objVendorBO.MobileNo = txtMobileNo.Text;
                objVendorBO.Fax = txtFax.Text;
                objVendorBO.EmailID = txtEmail.Text;
                objVendorBO.TINGST = txtTinGst.Text;
                objVendorBO.TINCST = txtTinCst.Text;
                objVendorBO.BankName = txtBankName.Text;
                objVendorBO.AccountNo = txtAccountNo.Text;
                objVendorBO.AccountName = txtAccountName.Text;
                objVendorBO.IFSCCode = txtIFSCCode.Text;
                objVendorBO.PANNO = txtPanNo.Text;
                objVendorBO.TaxRegNo = txtTaxRegNo.Text;
                objVendorBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                objVendorBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                int intClear = 1;


                //Entry in General Ledger 
                GeneralLedgerBL objGeneralLedgerBL = new GeneralLedgerBL();
                GeneralLedgerBO objGeneralLedgerBO = new GeneralLedgerBO();

                objGeneralLedgerBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                objGeneralLedgerBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                objGeneralLedgerBO.AccountName = txtAccountName.Text;
                objGeneralLedgerBO.AccountGroupID = 17;    //Sundry Creditors Account  [Account Group ID]
                objGeneralLedgerBO.OpeningBalance = Convert.ToDouble(0);
                objGeneralLedgerBO.BalanceType = "Credit";
                objGeneralLedgerBO.Description = "";



                if (ViewState["Mode"].ToString() == "Save")
                {
                    objVendorBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    objVendorBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                    objResults = objVendorBL.Vendor_Insert(objVendorBO);
                    ApplicationResult objResultSave = new ApplicationResult();
                    objResultSave = objGeneralLedgerBL.GeneralLedger_Insert(objGeneralLedgerBO, Convert.ToInt32(Session[ApplicationSession.FINANCIALYEAR]));

                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        intClear = 1;
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Vendor & their General Ledger Created Successfully.');</script>");
                    }
                    else
                    {
                        intClear = 0;
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Vendor is already exist.');</script>");
                    }
                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    objVendorBO.VendorID = Convert.ToInt32(ViewState["VendorID"].ToString());
                    objResults = objVendorBL.Vendor_Update(objVendorBO);
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        intClear = 1;
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Vendor updated successfully.');</script>");
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
                    txtVendorName.Text = "";
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Link ViewList Click Event
        protected void lnkViewList_Click(object sender, EventArgs e)
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["VendorID"] = null;
            PanelGrid_VisibilityMode(1);
        }
        #endregion

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            ViewState["VendorID"] = null;
            ViewState["Mode"] = "Save";
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
        }
        #endregion
    }
}