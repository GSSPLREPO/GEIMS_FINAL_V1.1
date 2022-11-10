using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using GEIMS.BO;
using GEIMS.BL;
using GEIMS.Common;
using GEIMS.DataAccess;
using log4net;

namespace GEIMS.Client.UI
{
    public partial class FeesGroup : PageBase
    {
        #region Declaration
        private static ILog logger = LogManager.GetLogger(typeof(FeesGroup));
        int Catid = 0;
        #endregion

        #region Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }
            if(!IsPostBack)
            {
                //BindSection();
                BindBudgetCategory();
                BindBudgetHeading();
                //ddlBudgetSubHeading.Items.Insert(0, new ListItem("-Select-", "0"));
                //10/11/2022 Bhandavi
                //Changed code to get validations for Budget Sub Heading

                ddlBudgetSubHeading.Items.Insert(0, new ListItem("-Select-", ""));
            }
            if (!Page.IsPostBack)
            {
                try
                {
                    BindFeesGroup();
                    ViewState["Mode"] = "Save";
                    ViewState["FeesGroupID"] = 0;
                    BindGeneralLedger();
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
        }
        #endregion

        //#region Bind Section
        //public void BindSection()
        //{
        //    try
        //    {
        //        SectionBL objSectionBL = new SectionBL();
        //        ApplicationResult objResult = new ApplicationResult();
        //        Controls objControls = new Controls();

        //        objResult = objSectionBL.Section_SelectAll(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
        //        if (objResult != null)
        //        {
        //            if (objResult.resultDT.Rows.Count > 0)
        //            {
        //                objControls.BindDropDown_ListBox(objResult.resultDT, ddlSection, "SectionName", "SectionMID");
        //                ddlSection.Items.Insert(0, new ListItem("-Select-", ""));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Error", ex);
        //        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
        //    }
        //}
        //#endregion

        #region Bind Budget Category
        public void BindBudgetCategory()
        {
            try
            {
                BudgetCategoryMBL ObjBudgetHeadingSelectDropDownBL = new BudgetCategoryMBL();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                DataTable dt = new DataTable();

                objResult = ObjBudgetHeadingSelectDropDownBL.BudgetHeading_SelectDropDown();
                if (objResult != null)
                {
                    //if (objResult.resultDT.Rows.Count > 0)
                    //{
                    //    objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetCategory, "CategoryName", "BudgetCategoryMID");
                    //    ddlBudgetCategory.Items.Insert(0, new ListItem("-Select-", ""));
                    //}
                    DataTable dtResult = objResult.resultDT;
                    if (dtResult.Rows.Count > 0)
                    {
                        txtBudgetCategory.Text = dtResult.Rows[2][1].ToString();
                        hfBudgetCategoryName.Value = dtResult.Rows[2][1].ToString();
                        Catid = Convert.ToInt32(dtResult.Rows[2][0].ToString());
                        hfBudgetCategoryId.Value = Catid.ToString();
                        
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

        #region Category Wise  BindBudgetHeading
        public void BindBudgetHeading()
        {
            try
            {
                BudgetSubHeadingMBL ObjBudgetSubHeadingMBL = new BudgetSubHeadingMBL();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();

                objResult = ObjBudgetSubHeadingMBL.BudgetHeading_SelectDropDownByCapId(Convert.ToInt32(hfBudgetCategoryId.Value));

                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetHeading, "HeadingName", "BudgetHeadingMID");
                        //ddlBudgetHeading.Items.Insert(0, new ListItem("-Select-", "0"));
                        //10/11/2022 Bhandavi
                        //Changed code to get validations for Budget Heading

                        ddlBudgetHeading.Items.Insert(0, new ListItem("-Select-", ""));
                    }
                    else
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetHeading, "HeadingName", "BudgetHeadingMID");
                        //ddlBudgetHeading.Items.Insert(0, new ListItem("-Select-", "0"));
                        //10/11/2022 Bhandavi
                        //Changed code to get validations for Budget Heading

                        ddlBudgetHeading.Items.Insert(0, new ListItem("-Select-", ""));
                        ddlBudgetHeading.ClearSelection();
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

        #region Heading Wise SubHeading
        public void bindSubHeading()
        {
            try
            {
                BudgetCapitalCostBL ObjBudgetCapitalCostBL = new BudgetCapitalCostBL();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();

                int hid = Convert.ToInt32(ddlBudgetHeading.SelectedValue);

                if (hid > 0)
                {
                    objResult = ObjBudgetCapitalCostBL.BudgetSubHeading_SelectDropdown(hid);
                    if (objResult != null)
                    {
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetSubHeading, "SubHeadingName", "BudgetSubHeadingMID");
                            //ddlBudgetSubHeading.Items.Insert(0, new ListItem("-Select-", "0"));
                            //10/11/2022 Bhandavi
                            //Changed code to get validations for Budget Sub Heading

                            ddlBudgetSubHeading.Items.Insert(0, new ListItem("-Select-", ""));
                        }
                        else
                        {
                            objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetSubHeading, "SubHeadingName", "BudgetSubHeadingMID");
                            //ddlBudgetSubHeading.Items.Insert(0, new ListItem("-Select-", "0"));
                            //10/11/2022 Bhandavi
                            //Changed code to get validations for Budget Sub Heading

                            ddlBudgetSubHeading.Items.Insert(0, new ListItem("-Select-", ""));
                            ddlBudgetSubHeading.ClearSelection();
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

        #region Heading Wise  BindBudgetSubHeading
        protected void ddlBudgetHeading_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindSubHeading();
        }
        #endregion

        #region BindFeesGroup

        public void BindFeesGroup()
        {
            try
            {
                FeesGroupBl objFeesGroupBL = new FeesGroupBl();
                ApplicationResult objResult = new ApplicationResult();

                objResult = objFeesGroupBL.FeesGroup_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    gvFeesGroup.DataSource = objResult.resultDT;
                    gvFeesGroup.DataBind();
                    if (gvFeesGroup.Rows.Count > 0)
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

        #region BindGeneralLedger
        public void BindGeneralLedger()
        {
            try
            {
                FeesGroupBl ObjFeesGroupBL = new FeesGroupBl();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                objResult = ObjFeesGroupBL.GeneralLedger_SelectAll_FeesGroup_dropdown(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlLedger, "AccountName", "LedgerID");
                        ddlLedger.Items.Insert(0, new ListItem("-Select-", ""));
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

        #region Add new Button Click Event
        protected void lnkAddNewFeesGroup_Click(object sender, EventArgs e)
        {
            ClearAll();
            PanelGrid_VisibilityMode(2);
            //ddlSection.Enabled = true;
            ddlBudgetHeading.Enabled = true;
            ddlBudgetSubHeading.Enabled = true;
            ddlLedger.Enabled = true;
            BindBudgetCategory();
            BindBudgetHeading();
            //ddlBudgetSubHeading.Items.Insert(0, new ListItem("-Select-", "0"));
            //10/11/2022 Bhandavi
            //Changed code to get validations for Budget Sub Heading
            ddlBudgetSubHeading.Items.Insert(0, new ListItem("-Select-", ""));
        }
        #endregion

        #region Save Button Click Event
        protected void btnSaveFeesGroup_Click(object sender, EventArgs e)
        {
            try
            {
                //01 Connect to LedgerId Record [Dropdown]

                GeneralLedgerBO objGeneralLedgerBO = new GeneralLedgerBO();
                GeneralLedgerBL objGeneralLedgerBL = new GeneralLedgerBL();
                ApplicationResult objResults1 = new ApplicationResult();
                Controls objControls1 = new Controls();

                int intTrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                int intSchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                string strAccountName = txtFeesGroupName.Text.Trim();

                if (ViewState["Mode"].ToString() == "Save")
                {                  
                    FeesGroupBo objFeesGroupBO = new FeesGroupBo();
                    FeesGroupBl objFeesGroupBL = new FeesGroupBl();
                    ApplicationResult objResults = new ApplicationResult();
                    Controls objControls = new Controls();

                    objFeesGroupBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                    objFeesGroupBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                    //objFeesGroupBO.SectionMID = Convert.ToInt32(ddlSection.SelectedValue);
                    objFeesGroupBO.BudgetCategoryMID = Convert.ToInt32(hfBudgetCategoryId.Value);
                    objFeesGroupBO.BudgetHeadingMID = Convert.ToInt32(ddlBudgetHeading.SelectedValue);
                    objFeesGroupBO.BudgetSubHeadingMID = Convert.ToInt32(ddlBudgetSubHeading.SelectedValue);
                    objFeesGroupBO.LedgerID = Convert.ToInt32(ddlLedger.SelectedValue.ToString());
                    objFeesGroupBO.FeeGroupName = txtFeesGroupName.Text.Trim();
                    objFeesGroupBO.Description = txtDescription.Text.Trim();                   
                    
                    if (ViewState["Mode"].ToString() == "Save")
                    {
                        objFeesGroupBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                        objFeesGroupBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();

                        objResults = objFeesGroupBL.FeesGroup_Insert(objFeesGroupBO);

                        if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {
                            ClearAll();
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Fees Group Created Successfully.');</script>");
                            BindFeesGroup();
                            PanelGrid_VisibilityMode(1);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Fees Group Name already Exists.');</script>");
                        }
                    }
                }
                //Edit
                else
                {
                    FeesGroupBo objFeesGroupBO = new FeesGroupBo();
                    FeesGroupBl objFeesGroupBL = new FeesGroupBl();
                    ApplicationResult objResults = new ApplicationResult();
                    Controls objControls = new Controls();

                    objFeesGroupBO.FeesGroupID = Convert.ToInt32(ViewState["FeesGroupID"].ToString());
                    objFeesGroupBO.FeeGroupName = txtFeesGroupName.Text.Trim();
                    objFeesGroupBO.Description = txtDescription.Text.Trim();
                    objFeesGroupBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                    objFeesGroupBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();

                    objResults = objFeesGroupBL.FeesGroup_Update(objFeesGroupBO);
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {

                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Fees Group updated successfully.');</script>");

                        BindFeesGroup();
                        ClearAll();
                        ViewState["Mode"] = "Save";
                        //  btnSave.Text = "Save";
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Fees Group Name already Exists.');</script>");
                    }
                    ddlLedger.Enabled = true;
                }

                /* -- ---------------------------------------------------------- */
                ////02 Connect to LedgerId Record [Automatic]
                //
                //GeneralLedgerBO objGeneralLedgerBO = new GeneralLedgerBO();
                //GeneralLedgerBL objGeneralLedgerBL = new GeneralLedgerBL();
                //ApplicationResult objResults1 = new ApplicationResult();
                //Controls objControls1 = new Controls();

                //int intTrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                //int intSchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                //string strAccountName = txtFeesGroupName.Text.Trim();
                //int intAccountGroupID = 26;

                //if (ViewState["Mode"].ToString() == "Save")
                //{
                //    objResults1 = objGeneralLedgerBL.FeesGroup_M_Fetch_GeneralLedger(strAccountName, intTrustMID, intSchoolMID, intAccountGroupID);
                //    if (objResults1 != null)
                //    {
                //        DataTable dtResult = new DataTable();
                //        dtResult = objResults1.resultDT;
                //        if (dtResult.Rows.Count > 0)
                //        {
                //            FeesGroupBo objFeesGroupBO = new FeesGroupBo();
                //            FeesGroupBl objFeesGroupBL = new FeesGroupBl();
                //            ApplicationResult objResults = new ApplicationResult();
                //            Controls objControls = new Controls();

                //            objFeesGroupBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                //            objFeesGroupBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);                           
                //            objFeesGroupBO.FeeGroupName = txtFeesGroupName.Text.Trim();                          
                //            objFeesGroupBO.Description = txtDescription.Text.Trim();
                //            int intLedgerID = Convert.ToInt32(dtResult.Rows[0]["LedgerID"]);
                //            objFeesGroupBO.LedgerID = intLedgerID;

                //            if (ViewState["Mode"].ToString() == "Save")
                //            {
                //                objFeesGroupBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                //                objFeesGroupBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();

                //                objResults = objFeesGroupBL.FeesGroup_Insert(objFeesGroupBO);

                //                if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                //                {
                //                    ClearAll();
                //                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Fees Group Created Successfully.');</script>");
                //                    BindFeesGroup();
                //                    PanelGrid_VisibilityMode(1);
                //                }
                //                else
                //                {
                //                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Fees Group Name already Exists.');</script>");
                //                }
                //            }                           
                //        }
                //        else
                //        {
                //            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Please Create Ledger Name!!!!!.');</script>");
                //        }
                //    }
                //}
                //else
                //{
                //    FeesGroupBo objFeesGroupBO = new FeesGroupBo();
                //    FeesGroupBl objFeesGroupBL = new FeesGroupBl();
                //    ApplicationResult objResults = new ApplicationResult();
                //    Controls objControls = new Controls();

                //    objFeesGroupBO.FeesGroupID = Convert.ToInt32(ViewState["FeesGroupID"].ToString());
                //    objFeesGroupBO.FeeGroupName = txtFeesGroupName.Text.Trim();
                //    objFeesGroupBO.Description = txtDescription.Text.Trim();
                //    objFeesGroupBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                //    objFeesGroupBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();

                //    objResults = objFeesGroupBL.FeesGroup_Update(objFeesGroupBO);
                //    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                //    {

                //        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Fees Group updated successfully.');</script>");

                //        BindFeesGroup();
                //        ClearAll();
                //        ViewState["Mode"] = "Save";
                //        //  btnSave.Text = "Save";
                //    }
                //    else
                //    {
                //        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Fees Group Name already Exists.');</script>");
                //    }
                //}

                /* -- ---------------------------------------------------------- */
                ////01 Without Connect to LedgerId Record
                ///
                //FeesGroupBo objFeesGroupBO = new FeesGroupBo();
                //FeesGroupBl objFeesGroupBL = new FeesGroupBl();
                //ApplicationResult objResults = new ApplicationResult();
                //Controls objControls = new Controls();

                //objFeesGroupBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                //objFeesGroupBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                //objFeesGroupBO.FeeGroupName = txtFeesGroupName.Text.Trim();
                //objFeesGroupBO.Description = txtDescription.Text.Trim();

                //if (ViewState["Mode"].ToString() == "Save")
                //{
                //    objFeesGroupBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                //    objFeesGroupBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();

                //    objResults = objFeesGroupBL.FeesGroup_Insert(objFeesGroupBO);

                //    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                //    {
                //        ClearAll();
                //        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Fees Group Created Successfully.');</script>");
                //        BindFeesGroup();
                //        PanelGrid_VisibilityMode(1);
                //    }
                //    else
                //    {
                //        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Fees Group Name already Exists.');</script>");
                //    }
                //}
                //else
                //{
                //    //objFeesGroupBO.FeesGroupID = Convert.ToInt32(ViewState["FeesGroupID"].ToString());
                //    //objFeesGroupBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                //    //objFeesGroupBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();

                //    //objResults = objFeesGroupBL.FeesGroup_Update(objFeesGroupBO);
                //    //if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                //    //{

                //    //    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Fees Group updated successfully.');</script>");

                //    //    BindFeesGroup();
                //    //    ClearAll();
                //    //    ViewState["Mode"] = "Save";
                //    //    //  btnSave.Text = "Save";
                //    //}
                //    //else
                //    //{
                //    //    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Fees Group Name already Exists.');</script>");
                //    //}
                //}
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }

        }
        #endregion

        #region ViewList Click Event
        protected void lnkViewList_Click(object sender, EventArgs e)
        {
            ClearAll();
            PanelGrid_VisibilityMode(1);
            //ddlSection.Enabled = true;
            ddlBudgetHeading.Enabled = true;
            ddlBudgetSubHeading.Enabled = true;
            ddlLedger.Enabled = true;
            BindBudgetCategory();
            BindBudgetHeading();
            // ddlBudgetSubHeading.Items.Insert(0, new ListItem("-Select-", "0"));
            //10/11/2022 Bhandavi
            //Changed code to get validations for Budget Sub Heading

            ddlBudgetSubHeading.Items.Insert(0, new ListItem("-Select-", ""));
        }
        #endregion

        #region FeesGroup GridView Events [RowCommand,PreRender]
        protected void gvFeesGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                FeesGroupBl objFeesGroupBL = new FeesGroupBl();

                if (e.CommandName.ToString() == "Edit1")
                {
                    ViewState["Mode"] = "Edit";
                    ViewState["FeesGroupID"] = e.CommandArgument.ToString();
                   
                    //ddlSection.Enabled = false;
                    ddlBudgetHeading.Enabled = false;
                    ddlBudgetSubHeading.Enabled = false;
                    ddlLedger.Enabled = false;

                    //BindBudgetCategory();

                    objResult = objFeesGroupBL.FeesGroup_Select(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (objResult != null)
                    {
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            //ddlSection.SelectedValue = objResult.resultDT.Rows[0][FeesGroupBo.FEESGROUP_SECTIONMID].ToString();
                            BindBudgetCategory();
                    

                            //txtBudgetCategory.Text = objResult.resultDT.Rows[0][FeesGroupBo.FEESGROUP_BUDGETCATEGORYMID].ToString();
                            //txtBudgetCategory.Text = Convert.ToString(hfBudgetCategoryName.Value);
                            ddlBudgetHeading.SelectedValue = objResult.resultDT.Rows[0][FeesGroupBo.FEESGROUP_BUDGETHEADINGMID].ToString();
                            bindSubHeading();
                            ddlBudgetSubHeading.SelectedValue = objResult.resultDT.Rows[0][FeesGroupBo.FEESGROUP_BUDGETSUBHEADINGMID].ToString();
                            BindGeneralLedger();
                            ddlLedger.SelectedValue = objResult.resultDT.Rows[0][FeesGroupBo.FEESGROUP_LEDGERID].ToString();
                            txtFeesGroupName.Text = objResult.resultDT.Rows[0][FeesGroupBo.FEESGROUP_FEEGROUPNAME].ToString();
                            txtDescription.Text = objResult.resultDT.Rows[0][FeesGroupBo.FEESGROUP_DESCRIPTION].ToString();
                            PanelGrid_VisibilityMode(2);
                        }
                    }
                }
                else if (e.CommandName.ToString() == "Delete1")
                {
                    objResult = objFeesGroupBL.FeesGroup_Delete(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session[ApplicationSession.USERID]), DateTime.UtcNow.AddHours(5.5).ToString());
                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record Deleted Successfully');</script>");
                        PanelGrid_VisibilityMode(1);
                        BindFeesGroup();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        protected void gvFeesGroup_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvFeesGroup.Rows.Count > 0)
                {
                    gvFeesGroup.UseAccessibleHeader = true;
                    gvFeesGroup.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        private void PanelGrid_VisibilityMode(int intMode)
        {
            if (intMode == 1)
            {
                divGrid.Visible = true;
                tabs.Visible = false;
                lnkViewList.Visible = false;
            }
            else if (intMode == 2)
            {

                divGrid.Visible = false;
                tabs.Visible = true;
                lnkAddNewFeesGroup.Visible = true;
                lnkViewList.Visible = true;
                
            }
        }

        #endregion PanelGrid_VisibilityMode

        #region ClearAll Method
        private void ClearAll()
        {
            Controls objControl = new Controls();
            objControl.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["Mode"] = "Save";

        }
        #endregion

       
    }
}