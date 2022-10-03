using System;
using System.Data;
using System.Web.UI.WebControls;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI;
using log4net;
using GEIMS.DataAccess;


namespace GEIMS.PayRoll
{
    public partial class PayItemTemplate : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(PayItemTemplate));

        #region Page_Load Event
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
                    //btnSave.Enabled = false;
                    //gvPayTemplate.Visible = false;
                    //ViewState["Mode"] = "Save";
                    //BindTemplateName();
                    //BindPayItem();
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divHide();", true);
                    //lbDependsOn.Enabled = false;

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divShow();", true);
                    gvPayTemplate.Visible = false;
                    GridTemplateBind();
                    ViewState["Mode"] = "Save";
                    //BindTemplateName();
                    //BindPayItem();
                    ddlSearchBy.Enabled = false;
                    ddlSearchBy.SelectedIndex = 1;
                    btnAddTemplate.Text = "Add Template";
                    //btnSave.Visible = true;
                    btnSave.Enabled = false;
                    lbDependsOn.Enabled = false;               
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
        }
        #endregion

        #region ViewList Event
        protected void lnkViewList_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divHide();", true);
            gvPayTemplate.Visible = false;
        }
        #endregion

        #region Add New
        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                TrustTemplateBl ObjTrustTemplateBl = new TrustTemplateBl();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                objResult = ObjTrustTemplateBl.TrustTemplate_Select_SchoolWise(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        ViewState["Mode"] = "Save";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divHide();", true);

                        GridNameConvertion();
                        BindTemplateName();
                        BindPayItem();

                        gvPayTemplate.Visible = false;
                        ddlSelectTemplateName.SelectedIndex = 0;
                        ddlPayItemType.SelectedIndex = 0;
                        ddlPayItemName.SelectedIndex = 0;
                        lbDependsOn.Items.Clear();

                        ddlSelectTemplateName.Enabled = true;
                        ddlPayItemName.Enabled = true; ;
                        ddlPayItemType.Enabled = true;

                        txtPercentage.Text = "";
                        txtAmount.Text = "";
                        txtTemplateName.Text = "";

                        btnSave.Text = "Save";
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Page is not Opened, Please Create Template Name!!!!! .');</script>");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divShow();", true);
                        btnSave.Visible = true;
                        btnSave.Enabled = false;
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

        //#region Add Template Name
        //protected void lnkAddNewTemplate_Click(object sender, EventArgs e)
        //{
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divShow();", true);
        //    gvPayTemplate.Visible = false;
        //    GridTemplateBind();
        //    ViewState["Mode"] = "Save";         
        //    //btnSave.Visible = true;
        //    btnSave.Enabled = false;
        //}
        //#endregion

        #region Save Template
        protected void btnAddTemplate_OnClick(object sender, EventArgs e)
        {
            try
            {
                TrustTemplateBo objTrustTemplateBo = new TrustTemplateBo();
                ApplicationResult objResult = new ApplicationResult();
                TrustTemplateBl ObjTrustTemplatemBl = new TrustTemplateBl();
                if (txtTemplateName.Text != "")
                {
                    objTrustTemplateBo.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                    objTrustTemplateBo.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                    objTrustTemplateBo.TrustTemplateName = txtTemplateName.Text.Trim();
                    objTrustTemplateBo.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    objTrustTemplateBo.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                    if (ViewState["Mode"].ToString() == "Save")
                    {
                        objTrustTemplateBo.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        objTrustTemplateBo.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);

                        objResult = ObjTrustTemplatemBl.TrustTemplate_Insert(objTrustTemplateBo);

                        if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divHide();", true);
                            //txtTemplateName.Text = "";
                            //BindTemplateName();
                            //ddlPayItemType.SelectedIndex = 0;
                            //GridTemplateBind();
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divShow();", true);
                            gvPayTemplate.Visible = false;
                            GridTemplateBind();
                            ViewState["Mode"] = "Save";
                            //btnSave.Visible = true;
                            btnSave.Enabled = false;
                            txtTemplateName.Text = "";
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Template Name Saved Successfully.');</script>");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divShow();", true);
                            gvPayTemplate.Visible = false;
                            GridTemplateBind();
                            ViewState["Mode"] = "Save";        
                            //btnSave.Visible = true;
                            btnSave.Enabled = false;
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Template Name already Exists.');</script>");
                        }
                    }
                    else
                    {
                        //Edit
                        objTrustTemplateBo.TrustTemplateID = Convert.ToInt32(ViewState["TrustTemplateID"].ToString());
                        objTrustTemplateBo.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                        objTrustTemplateBo.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                        objTrustTemplateBo.TrustTemplateName = txtTemplateName.Text.Trim();
                        objTrustTemplateBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        objTrustTemplateBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);

                        objResult = ObjTrustTemplatemBl.TrustTemplate_Update(objTrustTemplateBo);

                        if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divHide();", true);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divShow();", true);
                            txtTemplateName.Text = "";
                            BindTemplateName();
                            ddlPayItemType.SelectedIndex = 0;
                            GridTemplateBind();
                            btnAddTemplate.Text = "Add Template";
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Template Name Updated Successfully.');</script>");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Template Name already Exists.');</script>");
                        }
                    }
                    ViewState["Mode"] = "Save";
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Write template Name.');</script>");
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                TrustPayItemTemplateTBl objTrustPayItemTemplateBl = new TrustPayItemTemplateTBl();
                ApplicationResult objResults = new ApplicationResult();
                TrustPayItemTemplateTBo objTrustPayItemTemplateBo = new TrustPayItemTemplateTBo();

                objTrustPayItemTemplateBo.TemplateID = Convert.ToInt32(ddlSelectTemplateName.SelectedValue.ToString());

                if (lbDependsOn.Enabled == true)
                {
                    lblDependent.Text = "";
                    string dependsOn = "";
                    for (int i = 0; i < lbDependsOn.Items.Count; i++)
                    {
                        if (lbDependsOn.Items[i].Selected)
                        {
                            dependsOn = dependsOn + "," + lbDependsOn.Items[i].Value.ToString();
                        }
                    }
                    if (dependsOn == "")
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Please select the PayItem on which this PayItem depends.');</script>");
                        dependsOn = "";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divHide();", true);
                        goto Break;
                    }
                    objTrustPayItemTemplateBo.PayItemDependsOn = dependsOn;
                }
                else
                {
                    objTrustPayItemTemplateBo.PayItemDependsOn = "0";
                }
                if (txtAmount.Enabled = true && ddlPayItemType.SelectedValue == "0")
                {
                    if (txtAmount.Text != "")
                    {
                        objTrustPayItemTemplateBo.Amount = Convert.ToDouble(txtAmount.Text.Trim());
                    }
                    else
                    {
                        objTrustPayItemTemplateBo.Amount = Convert.ToDouble(0.00);

                    }
                }
                else
                {
                    objTrustPayItemTemplateBo.Amount = 0;
                    txtAmount.Text = "";
                }
                objTrustPayItemTemplateBo.PayItemType = Convert.ToInt32(ddlPayItemType.SelectedValue);
                if (txtPercentage.Enabled == true)
                {
                    if (txtPercentage.Text != "")
                    {
                        objTrustPayItemTemplateBo.Percentage = Convert.ToDouble(txtPercentage.Text.Trim());
                    }
                    else
                    {
                        objTrustPayItemTemplateBo.Percentage = 0;
                    }
                }
                else
                {
                    objTrustPayItemTemplateBo.Percentage = 0;
                }
                objTrustPayItemTemplateBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                objTrustPayItemTemplateBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);

                if (ViewState["Mode"].ToString() == "Save")
                {
                    for (int i = 0; i < gvPayTemplate.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(ddlPayItemName.SelectedValue) == Convert.ToInt32(gvPayTemplate.Rows[i].Cells[0].Text))
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('This PayItem is already inserted.');</script>");
                            txtAmount.Text = "";
                            ddlPayItemType.SelectedValue = "-1";
                            goto Break;
                        }
                    }
                    objTrustPayItemTemplateBo.PayItemID = Convert.ToInt32(ddlPayItemName.SelectedValue.ToString());
                    objTrustPayItemTemplateBo.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    objTrustPayItemTemplateBo.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                    ddlSelectTemplateName.SelectedValue = Convert.ToString(objTrustPayItemTemplateBo.TemplateID);
                    if (objTrustPayItemTemplateBo.PayItemID != -1 && objTrustPayItemTemplateBo.PayItemType != -1)
                    {
                        objResults = objTrustPayItemTemplateBl.TrustPayItemTemplateT_Insert(objTrustPayItemTemplateBo);

                        if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {
                            txtAmount.Text = "";
                            lbDependsOn.Items.Clear();
                            GridNameConvertion();
                            BindTemplateName();
                            BindPayItem();

                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Record Successfully Inserted.');</script>");
                            GridDataBind();
                            gvPayTemplate.Visible = false;
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Please Select Any PayItem..');</script>");
                        ddlPayItemType.SelectedIndex = -1;
                    }
                }
                else
                {
                    //Edit
                    objTrustPayItemTemplateBo.TrustTemplateID = Convert.ToInt32(ViewState["TrustTemplateID"].ToString());
                    objTrustPayItemTemplateBo.PayItemID = Convert.ToInt32(ddlPayItemName.SelectedValue.ToString());
                    objTrustPayItemTemplateBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    objTrustPayItemTemplateBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                    objTrustPayItemTemplateBo.IsDeleted = 0;
                    if (objTrustPayItemTemplateBo.PayItemID != -1 && objTrustPayItemTemplateBo.PayItemType != -1)
                    {
                        //for (int i = 0; i < gvPayTemplate.Rows.Count; i++)
                        //{
                        //    if (Convert.ToInt32(ddlPayItemName.SelectedValue) == Convert.ToInt32(gvPayTemplate.Rows[i].Cells[0].Text))
                        //    {
                        //        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('This PayItem is already inserted.');</script>");
                        //        txtAmount.Text = "";
                        //        ddlPayItemType.SelectedValue = "-1";
                        //        goto Break;
                        //    }
                        //}
                        objResults = objTrustPayItemTemplateBl.TrustPayItemTemplateT_Update(objTrustPayItemTemplateBo);
                        if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {
                            ddlSelectTemplateName.SelectedValue = ViewState["TemplateNameID"].ToString();
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Record Successfully Updated');</script>");
                            GridNameConvertion();
                            BindTemplateName();
                            BindPayItem();
                            //  int i = Convert.ToInt32(ddlSelectTemplateName.SelectedValue);
                            GridDataBind();
                            ViewState["Mode"] = "Save";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divHide();", true);
                            gvPayTemplate.Visible = false;

                            btnSave.Text = "Save";
                        }
                    }
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divHide();", true);             
                ViewState["Mode"] = "Save";

                gvPayTemplate.Visible = false;
                ddlSelectTemplateName.SelectedIndex = 0;
                ddlPayItemType.SelectedIndex = 0;
                ddlPayItemName.SelectedIndex = 0;
                lbDependsOn.Items.Clear();

                ddlSelectTemplateName.Enabled = true;
                ddlPayItemName.Enabled = true; ;
                ddlPayItemType.Enabled = true;

                txtPercentage.Text = "";
                txtAmount.Text = "";
                txtTemplateName.Text = "";

                btnSave.Text = "Save";
                btnSave.Enabled = true;
            //objControls.DisableControls(Master.FindControl("ContentPlaceHolder1"));
            Break: ;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divHide();", true);
                Exit:
                ;
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Selected Changed Event od Select Template Name dropdown
        protected void ddlSelectTemplateName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSelectTemplateName.SelectedValue != "")
            {
                GridDataBind();
                ddlPayItemType.SelectedIndex = 0;
                ddlPayItemName.SelectedIndex = 0;
                txtPercentage.Text = "";
                txtAmount.Text = "";
                lbDependsOn.Items.Clear();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divHide();", true);
            }
            else
            {
                ViewState["Mode"] = "Save";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divHide();", true);
                gvPayTemplate.Visible = false;
            }
        }
        #endregion

        #region ddlPayItemType dropdown's SelectedIndexChanged
        protected void ddlPayItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPayItemType.SelectedItem.Text == "Independent")
            {
                lbDependsOn.Enabled = false;
                txtPercentage.Enabled = false;
                txtAmount.Enabled = true;
                lbDependsOn.Items.Clear();
                txtAmount.Text = "0";
                txtPercentage.Text = "0";
            }
            else if (ddlPayItemType.SelectedItem.Text == "Dependent")
            {
                lbDependsOn.Enabled = true;
                txtPercentage.Enabled = true;
                txtAmount.Enabled = false;
                txtAmount.Text = "0";
                txtPercentage.Text = "0";
            }
            else if (ddlPayItemType.SelectedItem.Text == "Depends On Gross")
            {
                lbDependsOn.Enabled = false;
                txtPercentage.Enabled = false;
                txtAmount.Enabled = false;
                lbDependsOn.Items.Clear();
                txtAmount.Text = "0";
                txtPercentage.Text = "0";
            }
            else
            {
                lbDependsOn.Enabled = false;
                txtPercentage.Enabled = false;
                txtAmount.Enabled = false;
                lbDependsOn.Items.Clear();
                txtAmount.Text = "0";
                txtPercentage.Text = "0";
            }
            if (Convert.ToInt32(ddlPayItemType.SelectedValue.ToString()) == 1)
            {
                BindListbox();
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divHide();", true);
        }
        #endregion

        #region ddlPayItemName dropdown's SelectedIndexChanged
        protected void ddlPayItemName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region DropDOwnList For Template Name
        public void BindTemplateName()
        {
            try
            {
                TrustTemplateBl ObjTrustTemplateBl = new TrustTemplateBl();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                objResult = ObjTrustTemplateBl.TrustTemplate_Select_SchoolWise(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlSelectTemplateName, "TrustTemplateName", "TrustTemplateID");
                        ddlSelectTemplateName.Items.Insert(0, new ListItem("-Select-", ""));
                        btnSave.Visible = true;
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        ddlSelectTemplateName.Items.Insert(0, new ListItem("-Select-", ""));
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divShow();", true);
                        btnSave.Visible = true;
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

        #region DropDOwnList For PayItem
        public void BindPayItem()
        {
            try
            {
                PayItemBl ObjPayItemBl = new PayItemBl();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                objResult = ObjPayItemBl.PayItem_Select_PayItemName(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlPayItemName, "Name", "PayItemMID");
                        ddlPayItemName.Items.Insert(0, new ListItem("-Select-", ""));
                    }
                    else
                    {
                        ddlPayItemName.Items.Insert(0, new ListItem("-Select-", ""));
                        //  Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divShow();", true);
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Please Insert School Pay Item Template.');</script>");
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

        #region GridNameConvertion Method
        public void GridNameConvertion()
        {
            int i;
            for (i = 0; i < gvPayTemplate.Rows.Count; ++i)
            {
                if (gvPayTemplate.Rows[i].Cells[2].Text.ToString() == "0")
                {
                    gvPayTemplate.Rows[i].Cells[2].Text = "Independent";
                }
                else
                    if (gvPayTemplate.Rows[i].Cells[2].Text.ToString() == "1")
                {
                    gvPayTemplate.Rows[i].Cells[2].Text = "Dependent";
                }
                else
                        if (gvPayTemplate.Rows[i].Cells[2].Text.ToString() == "2")
                {
                    gvPayTemplate.Rows[i].Cells[2].Text = "Depends On Gross";
                }
                else
                            if (gvPayTemplate.Rows[i].Cells[2].Text.ToString() == "&nbsp;")
                {
                    gvPayTemplate.Rows[i].Cells[2].Text = "";
                }
            }
        }
        #endregion

        #region Bind grid of Pay Template
        private void GridDataBind()
        {
            try
            {

                TrustPayItemBl objTrustPayItemBl = new TrustPayItemBl();
                ApplicationResult objResult = new ApplicationResult();

                objResult = objTrustPayItemBl.PayItemTemplate_SelectAll_TemplateID(Convert.ToInt32(ddlSelectTemplateName.SelectedValue));
                if (objResult != null)
                {
                    gvPayTemplate.DataSource = objResult.resultDT;
                    gvPayTemplate.DataBind();
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvPayTemplate.Visible = true;
                    }
                    else
                    {
                        gvPayTemplate.Visible = false;
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
        private void GridTemplateBind()
        {
            try
            {

                TrustTemplateBl objTrustItemBl = new TrustTemplateBl();
                ApplicationResult objResult = new ApplicationResult();
                //int SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                //int TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);

                objResult = objTrustItemBl.TrustTemplate_Select_SchoolWise(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    gvTemplates.DataSource = objResult.resultDT;
                    gvTemplates.DataBind();
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvPayTemplate.Visible = true;
                    }
                    else
                    {
                        gvPayTemplate.Visible = false;
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

        #region BindListBox
        private void BindListbox()
        {
            try
            {
                TrustPayItemTemplateTBl objTrustPayItemTemplateBl = new TrustPayItemTemplateTBl();
                ApplicationResult objResults = new ApplicationResult();
                Controls objControls = new Controls();
                if(ddlSelectTemplateName.SelectedIndex !=0)
                {
                    objResults = objTrustPayItemTemplateBl.TrustPayItemTemplate_Select_PayItemWise(Convert.ToInt32(ddlSelectTemplateName.SelectedValue));

                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            objControls.BindDropDown_ListBox(objResults.resultDT, lbDependsOn, "Name", "PayItemMID");
                            //lbDependsOn.Items.Insert(0, new ListItem("-Select-", "-1"));
                        }
                        else
                        {
                            //When updatepanel used so you can put "ScriptManager" alert message
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please create independent item first.');", true);
                            //ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Please create independent item first.');</script>");
                            ddlPayItemType.SelectedIndex = 0;
                        }
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Please Select Template first.');</script>");
                    ddlPayItemType.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region rowcommand Event of Gridview
        protected void gvPayTemplate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            TrustPayItemTemplateTBl objTrustPayItemTemplateBl = new TrustPayItemTemplateTBl();
            ApplicationResult objResultsEdit = new ApplicationResult();
            try
            {
                ViewState["TrustTemplateID"] = Convert.ToInt32(e.CommandArgument.ToString());
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;
                if (e.CommandName.ToString() == "Edit1")
                {
                    btnSave.Text = "Edit";
                    ddlSelectTemplateName.Enabled = false;
                    ddlPayItemName.Enabled = false;
                    objResultsEdit = objTrustPayItemTemplateBl.TrustPayItemTemplateT_Select(Convert.ToInt32(ViewState["TrustTemplateID"].ToString()));
                    String[] str;
                    if (objResultsEdit != null)
                    {
                        if (objResultsEdit.resultDT.Rows.Count > 0)
                        {
                            ddlSelectTemplateName.SelectedValue = objResultsEdit.resultDT.Rows[0][TrustPayItemTemplateTBo.TRUSTPAYITEMTEMPLATET_TEMPLATEID].ToString();
                            ViewState["TemplateNameID"] = objResultsEdit.resultDT.Rows[0][TrustPayItemTemplateTBo.TRUSTPAYITEMTEMPLATET_TEMPLATEID].ToString();
                            ddlPayItemType.SelectedValue = objResultsEdit.resultDT.Rows[0][TrustPayItemTemplateTBo.TRUSTPAYITEMTEMPLATET_PAYITEMTYPE].ToString();
                            ddlPayItemName.SelectedValue = objResultsEdit.resultDT.Rows[0][TrustPayItemTemplateTBo.TRUSTPAYITEMTEMPLATET_PAYITEMID].ToString();
                            txtAmount.Text = objResultsEdit.resultDT.Rows[0][TrustPayItemTemplateTBo.TRUSTPAYITEMTEMPLATET_AMOUNT].ToString();
                            txtPercentage.Text = objResultsEdit.resultDT.Rows[0][TrustPayItemTemplateTBo.TRUSTPAYITEMTEMPLATET_PERCENTAGE].ToString();

                            str = gvPayTemplate.Rows[rowIndex].Cells[4].Text.ToString().Split(',');
                            int i = 0;
                            int len = str.Length;
                            for (i = 0; i < len; i++)
                            {
                                if (i >= len)
                                    break;
                                else
                                {
                                    if (str[i] != "NULL" && str[i] != "&nbsp;")
                                    {
                                        //cbDependsOn.SelectedValue = str[i];             
                                        lbDependsOn.Items.Add(str[i]);
                                    }
                                    else
                                    {
                                        lbDependsOn.SelectedValue = null;
                                    }
                                }
                            }
                            if (ddlPayItemType.SelectedItem.Text == "Depends On Gross")
                            {
                                txtPercentage.Enabled = true;
                                txtPercentage.BackColor = System.Drawing.Color.White;
                                txtAmount.Enabled = false;
                                lbDependsOn.Enabled = false;
                                lbDependsOn.Items.Clear();
                            }
                            else
                                if (ddlPayItemType.SelectedItem.Text == "Dependent")
                                {
                                    txtPercentage.Enabled = true;
                                    txtPercentage.BackColor = System.Drawing.Color.White;
                                    txtAmount.Enabled = false;
                                    lbDependsOn.Enabled = true;
                                    BindListbox();
                                }
                                else
                                    if (ddlPayItemType.SelectedItem.Text == "Independent")
                                    {
                                        txtAmount.Enabled = true;
                                        txtAmount.BackColor = System.Drawing.Color.White;
                                        txtPercentage.Enabled = false;
                                        lbDependsOn.Enabled = false;
                                        lbDependsOn.Items.Clear();
                                    }
                            ViewState["Mode"] = "Edit";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divHide();", true);
                        }
                    }
                }
                if (e.CommandName.ToString() == "Delete1")
                {
                    //Name : Arpit Shah
                    //New Code for Not Delete when Pay Item is assign to Employee ohter wise delete this record.

                    int intTrustTemplateID = Convert.ToInt32(e.CommandArgument.ToString());
                    int intLastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                    string strLastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    int intTrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID].ToString());
                    int intSchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString());

                    objResultsEdit = objTrustPayItemTemplateBl.TrustPayItemTemplate_Delete(intTrustTemplateID, intLastModifiedUserID, strLastModifiedDate, intTrustMID, intSchoolMID);
                    if (objResultsEdit != null)
                    {
                        if (objResultsEdit.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {
                            //When updatepanel used so you can put "ScriptManager" alert message
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('PayTemplate Deleted Successfully');", true);
                            //When updatepanel is not used so you can put "ClientScript" alert message
                            //ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('PayTemplate Deleted Successfully.');</script>");

                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divHide();", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('PayTemplate names is assigned to Employee Pay-Item Template So you Can not Delete.');", true);
                            //ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('PayTemplate(S) names are assigned to Employee Pay-Item Template So you Can not Delete.');</script>",true);
                            
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divHide();", true);
                        }
                        GridDataBind();
                        //ddlSelectTemplateName.SelectedIndex = 0;
                        ddlPayItemType.SelectedIndex = 0;
                        ddlPayItemName.SelectedIndex = 0;

                        txtPercentage.Text = "";
                        txtAmount.Text = "";
                        lbDependsOn.Items.Clear();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divHide();", true);
                    }
                   

                    ////Old Code
                    //objResultsEdit = objTrustPayItemTemplateBl.TrustPayItemTemplate_Delete(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session[ApplicationSession.USERID]), DateTime.UtcNow.AddHours(5.5).ToString());
                    //if (objResultsEdit != null)
                    //{
                    //    if (objResultsEdit.status == ApplicationResult.CommonStatusType.SUCCESS)
                    //    {
                    //        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('PayTemplate Deleted Successfully.');</script>");
                    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divHide();", true);
                    //    }
                    //    else
                    //    {
                    //        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('There are PayTemplate(s) associated with Payroll.');</script>");
                    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divHide();", true);
                    //    }
                    //}
                    //GridDataBind();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Gvtemplates Row Command [Template Name]
        protected void gvTemplates_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                TrustTemplateBl objTrustItemBl = new TrustTemplateBl();
                ApplicationResult objResult = new ApplicationResult();
                ViewState["TrustTemplateID"] = Convert.ToInt32(e.CommandArgument.ToString());
                if (e.CommandName.ToString() == "Edit1")
                {
                    objResult = objTrustItemBl.TrustTemplate_Select(Convert.ToInt32(ViewState["TrustTemplateID"].ToString()));

                    if (objResult != null)
                    {
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            txtTemplateName.Text = objResult.resultDT.Rows[0][TrustTemplateBo.TRUSTTEMPLATE_TRUSTTEMPLATENAME].ToString();
                            btnAddTemplate.Text = "Update Template";

                            ViewState["Mode"] = "Edit";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divShow();", true);
                        }
                    }
                }
                if (e.CommandName.ToString() == "Delete1")
                {
                    Controls objControls = new Controls();
                    //objControls.EnableControls(Master.FindControl("ContentPlaceHolder1"));
                    ApplicationResult objDelete = new ApplicationResult();

                    objDelete = objTrustItemBl.TrustTemplate_Delete(Convert.ToInt32(ViewState["TrustTemplateID"].ToString()), Convert.ToInt32(Session[ApplicationSession.USERID]), DateTime.UtcNow.AddHours(5.5).ToString());
                    if (objDelete.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Template name deleted successfully.');</script>");
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divHide();", true);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divShow();", true);
                        txtTemplateName.Text = "";
                        BindTemplateName();
                        GridTemplateBind();

                        GridNameConvertion();
                       
                        BindPayItem();
                       
                        GridDataBind();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Template names are assigned to Add Pay Item So you Can not Delete.');</script>");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divShow();", true);
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

        #region Get Employee Web Service Method
        [System.Web.Services.WebMethod]
        public static string[] GetAllEmployeeNameForReport(string prefixText, int TrustMID, int SchoolMID)
        {
            UserTemplateTBl objEmployeeMbl = new UserTemplateTBl();
            ApplicationResult objResult = new ApplicationResult();
            string strSearchText = prefixText + "%";
            List<string> result = new List<string>();
            objResult = objEmployeeMbl.Employee_Select_AutocomleteForPayroll(strSearchText, TrustMID, SchoolMID);
            if (objResult != null)
            {
                for (int i = 0; i < objResult.resultDT.Rows.Count; i++)
                {
                    string strEmployeeCodeName = objResult.resultDT.Rows[i]["EmployeeName"].ToString();
                    string strEmployeeMID = objResult.resultDT.Rows[i][EmployeeMBO.EMPLOYEEM_EMPLOYEEMID].ToString();
                    result.Add(string.Format("{0}~{1}", strEmployeeCodeName, strEmployeeMID));
                }
            }
            return result.ToArray();
        }

        #endregion

    }
}