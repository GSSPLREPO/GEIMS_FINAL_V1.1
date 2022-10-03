using System;
using System.Data;
using System.Web.UI.WebControls;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using log4net;
using System.Web.UI;

namespace GEIMS.Client.UI
{
    public partial class FeesCategoryMaster : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(FeesCategoryMaster));

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
                    Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
                    GridDataBind();
                    BindFeeGroup();
                    ViewState["Mode"] = "Save";
                    ViewState["FeesCategoryMID"] = 0;
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
        }
        #endregion

        protected override void OnPreRender(EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }

        #region Bind Fees Group
        private void BindFeeGroup()
        {
            try
            {

                FeesGroupBl objFeesBL = new FeesGroupBl();
                ApplicationResult objResults = new ApplicationResult();
                Controls objControls = new Controls();
                objResults = objFeesBL.FeesGroup_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResults != null)
                {
                    objControls.BindDropDown_ListBox(objResults.resultDT, ddlFeeGroup, "FeeGroupName", "FeesGroupID");
                    if (objResults.resultDT.Rows.Count > 0)
                    {

                    }
                    ddlFeeGroup.Items.Insert(0, new ListItem("-Select-", ""));
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
                FeesCategoryBL objFeesBl = new FeesCategoryBL();

                objResult = objFeesBl.FeesCategory_SelectAll(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvFeesCategory.DataSource = objResult.resultDT;
                        gvFeesCategory.DataBind();
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
                {
                    Session["CheckRefresh"] =
                    Server.UrlDecode(System.DateTime.Now.ToString());

                    FeesCategoryBO objFeesBo = new FeesCategoryBO();
                    ApplicationResult objResults = new ApplicationResult();
                    FeesCategoryBL objFeesBl = new FeesCategoryBL();
                    Controls objControls = new Controls();

                    objFeesBo.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                    objFeesBo.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                    objFeesBo.FeesName = txtFeesCategoryName.Text;
                    objFeesBo.FeesType = rblFeeType.SelectedItem.Text;
                    objFeesBo.OutstandingMonth = Convert.ToInt32(ddlOutstandingMonth.SelectedValue);
                    objFeesBo.FeeAbbreviation = txtAbbreviation.Text;
                    objFeesBo.FeeGroupID = Convert.ToInt32(ddlFeeGroup.SelectedValue);

                    objFeesBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    objFeesBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                    if (ValidateName() == true)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Fees Category Name " + txtFeesCategoryName.Text + " Already Exists');</script>");
                        goto Exit;
                    }
                    if (ViewState["Mode"].ToString() == "Save")
                    {

                        objResults = objFeesBl.FeesCategory_Insert(objFeesBo);

                        if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {
                            ClearAll();
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Fees Category Created Successfully.');</script>");
                            GridDataBind();
                            PanelGrid_VisibilityMode(1);
                        }

                    }
                    else
                    {
                        objFeesBo.FeesCategoryMID = Convert.ToInt32(ViewState["FeesCategoryMID"].ToString());

                        objResults = objFeesBl.FeesCategory_Update(objFeesBo);
                        if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {

                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Fee Category updated successfully.');</script>");

                            GridDataBind();
                            ClearAll();
                            //objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
                            ViewState["Mode"] = "Save";
                            btnSave.Text = "Save";
                        }
                    }
                Exit: ;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region ViewList Event
        protected void lnkViewList_Click(object sender, EventArgs e)
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["FeesCategoryMID"] = null;
            PanelGrid_VisibilityMode(1);
        }
        #endregion

        #region Add New Event
        protected void lnkAddNewFeeCategory_Click(object sender, EventArgs e)
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            PanelGrid_VisibilityMode(2);
        }
        #endregion

        #region FeesCategory RowCommand
        protected void gvFeesCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            FeesCategoryBL objFeesBl = new FeesCategoryBL();
            try
            {
                ViewState["FeesCategoryMID"] = Convert.ToInt32(e.CommandArgument.ToString());
                if (e.CommandName.ToString() == "Edit1")
                {
                    ApplicationResult objResultsEdit = new ApplicationResult();
                    objResultsEdit = objFeesBl.FeesCategory_Select(Convert.ToInt32(ViewState["FeesCategoryMID"].ToString()), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));

                    if (objResultsEdit != null)
                    {
                        if (objResultsEdit.resultDT.Rows.Count > 0)
                        {
                            txtFeesCategoryName.Text = objResultsEdit.resultDT.Rows[0][FeesCategoryBO.FEESCATEGORY_FEESNAME].ToString();
                            rblFeeType.SelectedValue = objResultsEdit.resultDT.Rows[0][FeesCategoryBO.FEESCATEGORY_FEESTYPE].ToString();
                            ddlOutstandingMonth.SelectedValue = objResultsEdit.resultDT.Rows[0][FeesCategoryBO.FEESCATEGORY_OutstandingMonth].ToString();
                            txtAbbreviation.Text = objResultsEdit.resultDT.Rows[0][FeesCategoryBO.FEESCATEGORY_FEEABBREVIATION].ToString();
                            ddlFeeGroup.Text = objResultsEdit.resultDT.Rows[0][FeesCategoryBO.FEESCATEGORY_FEEGROUPID].ToString();
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

                    objDelete = objFeesBl.Validate_FeesCategory_Delete(Convert.ToInt32(ViewState["FeesCategoryMID"].ToString()), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                    if (objDelete != null)
                    {
                        if (objDelete.resultDT.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(objDelete.resultDT.Rows[0]["FeesCategoryMID"]) == Convert.ToInt32(ViewState["FeesCategoryMID"].ToString()))
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('There are ClasseTemplate(s) associated with this FeeCategory. To delete this FeeCategory you need to delete ClasseTemplate(s) first');</script>");
                                goto Exit;
                            }
                        }
                    }
                    ApplicationResult objResultsDelete = new ApplicationResult();

                    objResultsDelete = objFeesBl.FeesCategory_Delete(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (objResultsDelete.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClearAll();
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Fee Category deleted successfully.');</script>");
                        GridDataBind();
                        PanelGrid_VisibilityMode(1);
                    }
                }
            Exit: ;
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region ValidateName
        public bool ValidateName()
        {
            FeesCategoryBL objFeesBl = new FeesCategoryBL();
            ApplicationResult objResults = new ApplicationResult();
            if (ViewState["Mode"].ToString() == "Save")
            {
                objResults = objFeesBl.FeesCategory_ValidateName(txtFeesCategoryName.Text, -1, Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
            }
            else
            {
                objResults = objFeesBl.FeesCategory_ValidateName(txtFeesCategoryName.Text, Convert.ToInt32(ViewState["FeesCategoryMID"].ToString()), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
            }
            if (objResults.resultDT.Rows.Count > 0)

                return true;

            return false;

        }
        #endregion

        #region Clear all
        private void ClearAll()
        {
            ViewState["FeesCategoryMID"] = 0;
            ViewState["Mode"] = "Save";
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            rblFeeType.SelectedIndex = -1;

        }
        #endregion

        #region PanelGrid_VisibilityMode

        private void PanelGrid_VisibilityMode(int intMode)
        {
            if (intMode == 1)
            {
                gvFeesCategory.Visible = true;
                tabs.Visible = false;
                lnkAddNewFeeCategory.Visible = true;
                lnkViewList.Visible = false;
            }
            else if (intMode == 2)
            {

                gvFeesCategory.Visible = false;
                tabs.Visible = true;
                lnkAddNewFeeCategory.Visible = true;
                lnkViewList.Visible = true;
            }
        }

        #endregion PanelGrid_VisibilityMode

    }
}