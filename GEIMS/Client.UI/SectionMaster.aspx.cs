using System;
using System.Web.UI.WebControls;
using System.Data;
using log4net;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.Common;
using System.Web.UI;
using GEIMS.DataAccess;

namespace GEIMS.Client.UI
{
    public partial class SectionMaster : PageBase
    {

        private static ILog logger = LogManager.GetLogger(typeof(SectionMaster));
        #region Page_Load

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
                    ViewState["SectionMID"] = 0;
                    txtAbbreviation.Enabled = true;
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
                SectionBL objSectionBL = new SectionBL();

                objResult = objSectionBL.Section_SelectAll(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    gvSection.DataSource = objResult.resultDT;
                    gvSection.DataBind();
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

        #region ViewList Section Click

        protected void lnkViewList_Click(object sender, EventArgs e)
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["SectionMID"] = null;
            PanelGrid_VisibilityMode(1);
        }
        #endregion

        #region Add NEW
        protected void lnkAddNewSection_Click(object sender, EventArgs e)
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            PanelGrid_VisibilityMode(2);
            txtAbbreviation.Enabled = true;
        }
        #endregion

        #region Save Click
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResults = new ApplicationResult();
                SectionBO objSectionBO = new SectionBO();
                SectionBL objSectionBL = new SectionBL();
                SectionTBO objSectionTBO = new SectionTBO();

                objSectionBO.SectionName = txtSectionName.Text;
                objSectionBO.SectionAvbbreviation = txtAbbreviation.Text;
                objSectionBO.Description = txtSectionDesc.Text;
                objSectionBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                objSectionBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                if (ValidateName() == true)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Section Name or/and Section Abbrev " + txtSectionName.Text + "/" + txtAbbreviation.Text + " Already Exists.');</script>");
                    goto Exit;
                }
                #region RollBack Transaction Starts

                DatabaseTransaction.OpenConnectionTransation();
                if (ViewState["Mode"].ToString() == "Save")
                {
                    objResults = objSectionBL.Section_Insert(objSectionBO);
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            ViewState["SectionMID"] = Convert.ToInt32(objResults.resultDT.Rows[0][0].ToString());
                            objSectionTBO.SectionMID = Convert.ToInt32(ViewState["SectionMID"].ToString());
                            objSectionTBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                            objSectionTBO.MediumMID = 1;
                            objSectionTBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                            objSectionTBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                            objResults = objSectionBL.SectionT_Insert(objSectionTBO);
                            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {

                            }
                        }
                    }

                    ClearAll();
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Section Created Successfully.');</script>");
                    //GridDataBind();
                    PanelGrid_VisibilityMode(1);
                }
                else
                {
                    ApplicationResult objResultsUpdate = new ApplicationResult();
                    objSectionBO.SectionMID = Convert.ToInt32(ViewState["SectionMID"].ToString());
                    objResultsUpdate = objSectionBL.Section_Update(objSectionBO);
                    if (objResultsUpdate.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Section updated successfully.');</script>");

                        ClearAll();
                    }
                }
                DatabaseTransaction.CommitTransation();
                GridDataBind();
                #endregion
            Exit: ;

            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Section GridView RowCommand Click
        protected void gvSection_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Controls objControls = new Controls();
                ApplicationResult objResults = new ApplicationResult();
                TrustBL objTrustBl = new TrustBL();
                SectionBL objSectionBL = new SectionBL();

                ViewState["SectionMID"] = Convert.ToInt32(e.CommandArgument.ToString());
                objResults = objTrustBl.Abbreviation_Validation(0, 0, Convert.ToInt32(ViewState["SectionMID"].ToString()));

                if (objResults != null)
                {
                    if (objResults.resultDT.Rows.Count > 0)
                    {
                        txtAbbreviation.Enabled = false;
                    }
                    else
                    {
                        txtAbbreviation.Enabled = true;
                    }

                }
                if (e.CommandName.ToString() == "Edit1")
                {
                    objResults = objSectionBL.Section_Select(Convert.ToInt32(ViewState["SectionMID"].ToString()));

                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {

                            txtSectionName.Text = objResults.resultDT.Rows[0][SectionBO.SECTION_SECTIONNAME].ToString();
                            txtAbbreviation.Text = objResults.resultDT.Rows[0][SectionBO.SECTION_SECTIONAVBBREVIATION].ToString();
                            txtSectionDesc.Text = objResults.resultDT.Rows[0][SectionBO.SECTION_DESCRIPTION].ToString();

                            ViewState["Mode"] = "Edit1";
                            PanelGrid_VisibilityMode(2);
                        }
                    }
                }
                else if ((e.CommandName.ToString() == "Delete1"))
                {
                    objControls.EnableControls(Master.FindControl("ContentPlaceHolder1"));
                    objResults = objSectionBL.Validate_Section_Delete(Convert.ToInt32(ViewState["SectionMID"].ToString()), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(objResults.resultDT.Rows[0]["SectionMID"]) == Convert.ToInt32(ViewState["SectionMID"].ToString()))
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('There are Classes(s) associated with this Section. To delete this Section you need to delete Classes(s) first.');</script>");
                                goto Exit;
                            }
                        }
                    }

                    ApplicationResult objResultsDelete = new ApplicationResult();


                    objResultsDelete = objSectionBL.Section_Delete(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (objResultsDelete.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ApplicationResult objResultsDeleteT = new ApplicationResult();
                        objResultsDeleteT = objSectionBL.SectionT_Delete(Convert.ToInt32(e.CommandArgument.ToString()));
                        if (objResultsDeleteT.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {

                        }
                        ClearAll();
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Section deleted successfully.');</script>");
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

            ApplicationResult objResults = new ApplicationResult();
            SectionBL objSectionBL = new SectionBL();
            if (ViewState["Mode"].ToString() == "Save")
            {
                objResults = objSectionBL.Section_ValidateName(txtSectionName.Text, txtAbbreviation.Text, -1, Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
            }
            else
            {
                objResults = objSectionBL.Section_ValidateName(txtSectionName.Text, txtAbbreviation.Text, Convert.ToInt32(ViewState["SectionMID"].ToString()), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
            }
            if (objResults.resultDT.Rows.Count > 0)

                return true;
            return false;

        }
        #endregion

        #region PanelGrid_VisibilityMode

        //panel grid true & false according to condition
        private void PanelGrid_VisibilityMode(int intMode)
        {
            if (intMode == 1)
            {
                gvSection.Visible = true;
                tabs.Visible = false;
                lnkViewList.Visible = false;
                lnkAddNewSection.Visible = true;
            }
            else if (intMode == 2)
            {

                gvSection.Visible = false;
                tabs.Visible = true;
                lnkViewList.Visible = true;
                lnkAddNewSection.Visible = true;
            }
        }

        #endregion PanelGrid_VisibilityMode

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            ViewState["SectionMID"] = 0;
            ViewState["Mode"] = "Save";
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
        }
        #endregion


    }
}