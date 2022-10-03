using System;
using System.Data;
using System.Web.UI.WebControls;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using log4net;
using System.Web.UI;
using GEIMS.DataAccess;

namespace GEIMS.Client.UI
{
    public partial class Status : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(Status));
        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    GridDataBind();
                    ViewState["Mode"] = "Save";
                    ViewState["StatusMasterID"] = 0;
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
                StatusBL objStatusBl = new StatusBL();

                objResult = objStatusBl.Status_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvStatus.DataSource = objResult.resultDT;
                        gvStatus.DataBind();
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
                StatusBO objStatusBo = new StatusBO();
                ApplicationResult objResults = new ApplicationResult();
                StatusBL objStatusBl = new StatusBL();
                Controls objControls = new Controls();

                objStatusBo.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                objStatusBo.StatusName = txtStatusName.Text;
                objStatusBo.Discription = txtStatusDesc.Text;
                objStatusBo.LastModificationDate = DateTime.UtcNow.AddHours(5.5).ToString();
                objStatusBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);

                if (ViewState["Mode"].ToString() == "Save")
                {
                    objResults = objStatusBl.Status_Insert(objStatusBo);

                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClearAll();
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Status Created Successfully.');</script>");
                        GridDataBind();
                        PanelGrid_VisibilityMode(1);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Status Name Already Exists');</script>");
                    }
                }
                else
                {
                    objStatusBo.StatusMasterID = Convert.ToInt32(ViewState["StatusMasterID"].ToString());

                    objResults = objStatusBl.Status_Update(objStatusBo);
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {

                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Status updated successfully.');</script>");

                        GridDataBind();
                        ClearAll();
                        //objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
                        ViewState["Mode"] = "Save";
                        btnSave.Text = "Save";
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Status  Name Already Exists');</script>");
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

        #region ViewList Event
        protected void lnkViewList_Click(object sender, EventArgs e)
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["StatusMasterID"] = null;
            PanelGrid_VisibilityMode(1);
        }
        #endregion

        #region Add New Event
        protected void lnkAddNewStatus_Click(object sender, EventArgs e)
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            PanelGrid_VisibilityMode(2);
        }
        #endregion

        #region Status RowCommand
        protected void gvStatus_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            StatusBL objStatusBl = new StatusBL();
            try
            {
                ViewState["StatusMasterID"] = Convert.ToInt32(e.CommandArgument.ToString());
                if (e.CommandName.ToString() == "Edit1")
                {
                    ApplicationResult objResultsEdit = new ApplicationResult();
                    objResultsEdit = objStatusBl.Status_Select(Convert.ToInt32(ViewState["StatusMasterID"].ToString()), Convert.ToInt32(Session[ApplicationSession.TRUSTID]));

                    if (objResultsEdit != null)
                    {
                        if (objResultsEdit.resultDT.Rows.Count > 0)
                        {
                            txtStatusName.Text = objResultsEdit.resultDT.Rows[0][StatusBO.STATUS_STATUSNAME].ToString();
                            txtStatusDesc.Text = objResultsEdit.resultDT.Rows[0][StatusBO.STATUS_DISCRIPTION].ToString();

                            ViewState["Mode"] = "Edit1";
                            PanelGrid_VisibilityMode(2);
                        }
                    }
                }

                if (e.CommandName.ToString() == "Delete1")
                {
                    Controls objControls = new Controls();
                    objControls.EnableControls(Master.FindControl("ContentPlaceHolder1"));
                    ApplicationResult objDelete = new ApplicationResult();

                    objDelete = objStatusBl.Validate_Status_Delete(Convert.ToInt32(ViewState["StatusMasterID"].ToString()), Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                    if (objDelete != null)
                    {
                        ApplicationResult objResultsDelete = new ApplicationResult();

                        objResultsDelete = objStatusBl.Status_Delete(Convert.ToInt32(e.CommandArgument.ToString()));
                        if (objResultsDelete.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {
                            ClearAll();
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Status deleted successfully.');</script>");
                            GridDataBind();
                            PanelGrid_VisibilityMode(1);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('There are Student(s) associated with this Status. To delete this Status you need to delete Student(s) first');</script>");
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

        #region ValidateName
        public bool ValidateName()
        {
            StatusBL objStatusBl = new StatusBL();
            ApplicationResult objResults = new ApplicationResult();
            if (ViewState["Mode"].ToString() == "Save")
            {
                objResults = objStatusBl.Status_ValidateName(txtStatusName.Text, -1, Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
            }
            else
            {
                objResults = objStatusBl.Status_ValidateName(txtStatusName.Text, Convert.ToInt32(ViewState["StatusMasterID"].ToString()), Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
            }
            if (objResults.resultDT.Rows.Count > 0)

                return true;

            return false;

        }
        #endregion

        #region Clear all
        private void ClearAll()
        {
            ViewState["StatusMasterID"] = 0;
            ViewState["Mode"] = "Save";
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));

        }
        #endregion

        #region PanelGrid_VisibilityMode

        private void PanelGrid_VisibilityMode(int intMode)
        {
            if (intMode == 1)
            {
                gvStatus.Visible = true;
                tabs.Visible = false;
                lnkAddNewStatus.Visible = true;
                lnkViewList.Visible = false;
            }
            else if (intMode == 2)
            {

                gvStatus.Visible = false;
                tabs.Visible = true;
                lnkAddNewStatus.Visible = true;
                lnkViewList.Visible = true;
            }
        }

        #endregion PanelGrid_VisibilityMode
    }
}