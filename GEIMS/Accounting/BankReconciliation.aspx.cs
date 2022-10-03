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
using GEIMS.DataAccess;

namespace GEIMS.Accounting
{
	public partial class BankReconciliation : System.Web.UI.Page
	{

        #region declaration
        private static ILog logger = LogManager.GetLogger(typeof(Receipt));
        Controls objControl = new Controls();
        DataTable dtGridData = new DataTable();

       
        private string _reportTitle, _date;
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
                if (IsPostBack) return;
                txtFromDate.Text = Session[ApplicationSession.ACCOUNTFROMDATE].ToString();
                txtToDate.Text = Session[ApplicationSession.ACCOUNTTODATE].ToString();
                
                PanelVisibility(1);
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Attributes.Add("readonly", "readonly");
            }
            else
            {
                Response.Redirect(
                    Convert.ToInt32(Session[ApplicationSession.SCHOOLID]) == 0
                        ? "../Accounting/AccountLogin.aspx?mode=TU"
                        : "../Accounting/AccountLogin.aspx", false);
            }

            if(!IsPostBack)
            {
                bindBank();
            }

        }
        #endregion

        #region Panel Visibility Mode
        public void PanelVisibility(int intcode)
        {
            if (intcode == 1)
            {
                divGrid.Visible = false;
                tabs.Visible = true;
                btnBack.Visible = false;
                //btnExportExcel.Visible = false;
            }
            else if (intcode == 2)
            {
                divGrid.Visible = true;
                tabs.Visible = false;
                btnBack.Visible = true;
                //btnExportExcel.Visible = true;
            }
        }
        #endregion

        #region Bind Bank
        public void bindBank()
        {
            try
            {
                GeneralLedgerBL objGeneralLedgerBL = new GeneralLedgerBL();
                GeneralLedgerBO objGeneralLedgerBO = new GeneralLedgerBO();
                ApplicationResult objResultSelect = new ApplicationResult();
                DataTable dtSelect = new DataTable();

                objResultSelect = objGeneralLedgerBL.GeneralLedger_Select_Receipt_Payment(2, Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResultSelect != null)
                {
                    dtSelect = objResultSelect.resultDT;

                    objControl.BindDropDown_ListBox(dtSelect, ddlGeneralLedger, "AccountName", "LedgerID");
                    ddlGeneralLedger.Items.Insert(0, new ListItem("--Select--", ""));
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
     
        #region Search Record
        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                if(ddlGeneralLedger.SelectedIndex != 0)
                {
                    var objBankRecoDetailsBL = new BankRecoDetailsBL();
                    int intIsNarration;
                    //if (chkNarration.Checked)
                    //{
                    //    intIsNarration = 1;
                    //    //var bfield = new BoundField { HeaderText = "Narration", DataField = "Description" };
                    //    //bfield.HeaderStyle.Width = new Unit("30%");
                    //    //bfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                    //    //bfield.HeaderStyle.VerticalAlign = VerticalAlign.Top;
                    //    //bfield.ItemStyle.Width = new Unit("30%");
                    //    //bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                    //    //bfield.ItemStyle.VerticalAlign = VerticalAlign.Top;
                    //    //bfield.ItemStyle.Wrap = false;
                    //    //gvCashBankReport.Columns.Add(bfield);
                    //}
                    //else
                    intIsNarration = 0;

                    var objResult = objBankRecoDetailsBL.BankReconciliaiton_Select_BankBook(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(ddlGeneralLedger.SelectedValue), ddlType.SelectedValue, txtFromDate.Text, txtToDate.Text, intIsNarration);

                    if (objResult != null)
                    {
                     
                        gvBankReconciliation.DataSource = objResult.resultDT;
                        gvBankReconciliation.DataBind();
                        PanelVisibility(2);

                        //var strName = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]) == 0 ? Session[ApplicationSession.TRUSTNAME].ToString() : Session[ApplicationSession.SCHOOLNAME].ToString();
                        lblHeading.Text = "<b>Bank Name : " + ddlGeneralLedger.SelectedItem.Text + "</b><br/>" + txtFromDate.Text + " To " + txtToDate.Text;
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Select Dropdown!.');</script>");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                PanelVisibility(1);
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        #region Clear Operation and Reset page
        public void Clear()
        {
            if (Master != null) objControl.ClearForm(Master.FindControl("ContentPlaceHolder1"));
           
            gvBankReconciliation.DataSource = null;
            gvBankReconciliation.DataBind();
            txtFromDate.Text = Session[ApplicationSession.ACCOUNTFROMDATE].ToString();
            txtToDate.Text = Session[ApplicationSession.ACCOUNTTODATE].ToString();            
        }

        #endregion
        protected void gvBankReconciliation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //int count = gvBankReconciliation.Rows.Count;
            //if (count > 0)
            //{
            //    GridViewRow row = gvBankReconciliation.Rows[count - 1];
            //   TextBox lb = (TextBox)row.FindControl("txtdate1");
            //    if (lb != null)
            //        lb.Visible = true;
            //}

           
        }

        protected void gvBankReconciliation_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvBankReconciliation.Rows.Count > 0)
                {
                    gvBankReconciliation.UseAccessibleHeader = true;
                    gvBankReconciliation.HeaderRow.TableSection = TableRowSection.TableHeader;                
                }                
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

    }
}