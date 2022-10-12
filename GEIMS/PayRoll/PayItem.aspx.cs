using System;
using System.Data;
using System.Web.UI.WebControls;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using log4net;
using System.Web.UI;

namespace GEIMS.PayRoll
{
    public partial class PayItem : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(PayItem));

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }
            if (!Page.IsPostBack)
            {
                try
                {
                    GridDataBind();
                    ViewState["Mode"] = "Save";
                    ViewState["PayItemMID"] = 0;
                    bindDeductionDropDown();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divHide();", true);
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
        /// <summary>
        /// Comment added on 12/10/2022 Bhandavi        
        /// Bind gridview gvPayItem with values from [tbl_PayItem_M] table with trust id 1 (for all schools taking trust id as parameter)
        /// </summary>
        private void GridDataBind()
        {
            try
            {

                ApplicationResult objResult = new ApplicationResult();
                PayItemBl objPayItemBl = new PayItemBl();

                int intTrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                //int intSchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                //objResult = objPayItemBl.PayItem_SelectAll(intTrustMID, intSchoolMID);
                objResult = objPayItemBl.PayItem_SelectAll(intTrustMID);
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvPayItem.DataSource = objResult.resultDT;
                        gvPayItem.DataBind();
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

        #region Save Event
        /// <summary>
        /// Comment added on 12/10/2022 Bhandavi    
        /// to save new pay item into table  [tbl_PayItem_M]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            PayItemBo objPayItemBo = new PayItemBo();
            ApplicationResult objResults = new ApplicationResult();
            ApplicationResult objResultSelect = new ApplicationResult();

            PayItemBl objPayItemBl = new PayItemBl();
            Controls objControls = new Controls();

            objPayItemBo.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
            objPayItemBo.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
            objPayItemBo.Name = txtPayItemName.Text;
            objPayItemBo.Description = txtPayItemDescription.Text;
            objPayItemBo.Type = Convert.ToInt32(rdEraningDeductionList.SelectedValue);

            //Deduction
            if (rdEraningDeductionList.SelectedValue != "0")
            {
                if (ddlDeduction.SelectedIndex != 0)
                {
                    //objPayItemBo.Deduction = Convert.ToInt32(rbtnDeduction.SelectedValue);
                    objPayItemBo.Deduction = Convert.ToInt32(ddlDeduction.SelectedValue);

                    if (ViewState["Mode"].ToString() == "Save")
                    {
                        //Add
                        objPayItemBo.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        objPayItemBo.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                        objResults = objPayItemBl.PayItem_Insert(objPayItemBo);

                        if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {
                            ClearAll();
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Pay Item Created Successfully.');</script>");
                            GridDataBind();
                            PanelGrid_VisibilityMode(1);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Pay Item already Exists.');</script>");
                            ClearAll();
                            GridDataBind();
                            PanelGrid_VisibilityMode(1);
                        }
                    }
                    else
                    {
                        //Edit
                        int intPayItemMID = Convert.ToInt32(ViewState["PayItemMID"].ToString());
                        objResultSelect = objPayItemBl.PayItem_M_Cascade(Convert.ToInt32(intPayItemMID), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                        if (objResultSelect != null)
                        {
                            DataTable dtResult1 = objResultSelect.resultDT;
                            int col = Convert.ToInt32(dtResult1.Rows[0][0]);

                            if (col == 0)
                            {
                                objPayItemBo.PayItemMID = Convert.ToInt32(ViewState["PayItemMID"].ToString());
                                objPayItemBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                                objPayItemBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                objResults = objPayItemBl.PayItem_Update(objPayItemBo);

                                if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Pay Item updated successfully.');</script>");

                                    GridDataBind();
                                    ClearAll();
                                    //objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
                                    ViewState["Mode"] = "Save";
                                    btnSave.Text = "Save";
                                    //rdEraningDeductionList.Enabled = true;
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Pay Item already Exists.');</script>");
                                }
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You cannot Update this record because it is in used.');</script>");
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You cannot Update this record because it is in used.');</script>");
                        }
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Pay Item updated successfully.');</script>");

                        GridDataBind();
                        ClearAll();
                        //objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
                        ViewState["Mode"] = "Save";
                        btnSave.Text = "Save";
                        //rdEraningDeductionList.Enabled = true;
                    }
                }
                //else
                //{
                //    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Select Deduction DropDown.');</script>");
                //}
            }
            //Earnings
            else
            {
                if (ViewState["Mode"].ToString() == "Save")
                {
                    //Add
                    objPayItemBo.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    objPayItemBo.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                    objResults = objPayItemBl.PayItem_Insert(objPayItemBo);

                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClearAll();
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Pay Item Created Successfully.');</script>");
                        GridDataBind();
                        PanelGrid_VisibilityMode(1);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Pay Item already Exists.');</script>");
                        ClearAll();
                        GridDataBind();
                        PanelGrid_VisibilityMode(1);
                    }
                }
                else
                {
                    //Edit
                    int intPayItemMID = Convert.ToInt32(ViewState["PayItemMID"].ToString());
                    objResultSelect = objPayItemBl.PayItem_M_Cascade(Convert.ToInt32(intPayItemMID), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                    if (objResultSelect != null)
                    {
                        DataTable dtResult1 = objResultSelect.resultDT;
                        int col = Convert.ToInt32(dtResult1.Rows[0][0]);

                        if(col==0)
                        {
                            objPayItemBo.PayItemMID = Convert.ToInt32(ViewState["PayItemMID"].ToString());
                            objPayItemBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                            objPayItemBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                            objResults = objPayItemBl.PayItem_Update(objPayItemBo);

                            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Pay Item updated successfully.');</script>");

                                GridDataBind();
                                ClearAll();
                                //objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
                                ViewState["Mode"] = "Save";
                                btnSave.Text = "Save";
                                //rdEraningDeductionList.Enabled = true;
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Pay Item already Exists.');</script>");
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You cannot Update this record because it is in used.');</script>");
                        }
                    }                  
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Pay Item already Exists.');</script>");
                    }
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Pay Item updated successfully.');</script>");
                    GridDataBind();
                    ClearAll();
                    //objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
                    ViewState["Mode"] = "Save";
                    btnSave.Text = "Save";
                    rdEraningDeductionList.Enabled = true;
                }
            }
        }
        #endregion

        #region ViewList Event
        protected void lnkViewList_Click(object sender, EventArgs e)
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["PayItemMID"] = null;
            PanelGrid_VisibilityMode(1);
        }
        #endregion

        #region Add New Event
        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            PanelGrid_VisibilityMode(2);
            rdEraningDeductionList.SelectedIndex = 0;
            //rdEraningDeductionList.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divHide();", true);
        }
        #endregion

        #region PayItem RowCommand
        protected void gvPayItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            PayItemBl objPayItemBl = new PayItemBl();
            try
            {
                ViewState["PayItemMID"] = Convert.ToInt32(e.CommandArgument.ToString());
                if (e.CommandName.ToString() == "Edit1")
                {
                    //rdEraningDeductionList.Enabled = false;
                    ApplicationResult objResultsEdit = new ApplicationResult();
                    objResultsEdit = objPayItemBl.PayItem_Select(Convert.ToInt32(ViewState["PayItemMID"].ToString()));

                    if (objResultsEdit != null)
                    {
                        if (objResultsEdit.resultDT.Rows.Count > 0)
                        { 
                            txtPayItemName.Text = objResultsEdit.resultDT.Rows[0][PayItemBo.PAYITEM_NAME].ToString();
                            txtPayItemDescription.Text = objResultsEdit.resultDT.Rows[0][PayItemBo.PAYITEM_DESCRIPTION].ToString();
                            rdEraningDeductionList.SelectedValue = objResultsEdit.resultDT.Rows[0][PayItemBo.PAYITEM_TYPE].ToString();
                            if (objResultsEdit.resultDT.Rows[0][PayItemBo.PAYITEM_TYPE].ToString() == "1")
                            {
                                //rbtnDeduction.SelectedValue = objResultsEdit.resultDT.Rows[0][PayItemBo.PAYITEM_DEDUCTION].ToString();
                                ddlDeduction.Text = objResultsEdit.resultDT.Rows[0][PayItemBo.PAYITEM_DEDUCTION].ToString();
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divShow();", true);
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divHide();", true);
                            }
                           
                            ViewState["Mode"] = "Edit";
                            PanelGrid_VisibilityMode(2);
                        }
                    }
                }
                if (e.CommandName.ToString() == "Delete1")
                {
                    Controls objControls = new Controls();
                    //objControls.EnableControls(Master.FindControl("ContentPlaceHolder1"));
                    ApplicationResult objDelete = new ApplicationResult();
                    ApplicationResult objResultSelect1 = new ApplicationResult();

                    int intPayItemMID = Convert.ToInt32(ViewState["PayItemMID"].ToString());
                    objResultSelect1 = objPayItemBl.PayItem_M_Cascade(Convert.ToInt32(intPayItemMID), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                    if (objResultSelect1 != null)
                    {
                        DataTable dtResult1 = objResultSelect1.resultDT;
                        int col = Convert.ToInt32(dtResult1.Rows[0][0]);
                        if (col == 0)
                        {
                            objDelete = objPayItemBl.PayItem_Delete(Convert.ToInt32(e.CommandArgument.ToString()));
                            if (objDelete.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClearAll();
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Pay Items deleted successfully.');</script>");
                                GridDataBind();
                                PanelGrid_VisibilityMode(1);
                            }
                            else if (objDelete.status == ApplicationResult.CommonStatusType.FAILURE)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You cannot Delete this record because it is in used to School PI Template.');</script>");
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record Not Deleted.');</script>");
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You cannot Delete this record because it is in used to School PI Template.');</script>");
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

        #region Clear all
        public void ClearAll()
        {
            ViewState["PayItemMID"] = 0;
            ViewState["Mode"] = "Save";
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            rdEraningDeductionList.SelectedIndex = 0;
            //rbtnDeduction.SelectedIndex = 0;
            ddlDeduction.SelectedIndex = 0;

        }
        #endregion

        #region PanelGrid_VisibilityMode

        private void PanelGrid_VisibilityMode(int intMode)
        {
            if (intMode == 1)
            {
                gvPayItem.Visible = true;
                tabs.Visible = false;
                lnkAddNew.Visible = true;
                lnkViewList.Visible = false;
                divGrid.Visible = true;
            }
            else if (intMode == 2)
            {

                gvPayItem.Visible = false;
                tabs.Visible = true;
                lnkAddNew.Visible = true;
                lnkViewList.Visible = true;
                divGrid.Visible = false;
            }
        }

        #endregion PanelGrid_VisibilityMode

        #region Deduction Dropwon
        public void bindDeductionDropDown()
        {
            try
            {
                ddlDeduction.Items.Insert(0, new ListItem("--Select--", ""));
                ddlDeduction.Items.Insert(1, new ListItem("80C Deduction","1"));
                ddlDeduction.Items.Insert(2, new ListItem("80CCC Deduction","2"));
                ddlDeduction.Items.Insert(3, new ListItem("80CCD Deduction","3"));
                ddlDeduction.Items.Insert(4, new ListItem("Professional Tax","4"));
                ddlDeduction.Items.Insert(5, new ListItem("Tax Deducted at Source (TDS)","5"));
                ddlDeduction.Items.Insert(6, new ListItem("ESIC","6"));
                ddlDeduction.Items.Insert(7, new ListItem("Other", "7"));
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