using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.DataAccess;

namespace GEIMS.PayRoll
{
    public partial class LeaveMaster : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(LeaveMaster));

        #region Page Load Event
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
                    PanelGrid_VisibilityMode(1);
                    BindLeave();
                    bindYear();
                    ViewState["Mode"] = "Save";
                    ViewState["LeaveID"] = 0;
                  
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
        
      
        }
        #endregion

        #region BindgvLeave

        public void BindLeave()
        {
            try
            {
                LeaveMBo objLeaveMBO = new LeaveMBo();


                LeaveMBl objLeaveMBL = new LeaveMBl();
                ApplicationResult objResult = new ApplicationResult();

                objResult = objLeaveMBL.LeaveM_SelectAll();
                if (objResult != null)
                {
                    gvLeaveMaster.DataSource = objResult.resultDT;
                    gvLeaveMaster.DataBind();
                    if (gvLeaveMaster.Rows.Count > 0)
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

        #region Bind Year
        public void bindYear()
        {
            string[] strYear;
            int intacYear = 0;
            #region Get Accounting Start Date
            ApplicationResult objResults = new ApplicationResult();
            TrustBL objTrustBl = new TrustBL();

            objResults = objTrustBl.Trust_Select(Convert.ToInt32(Session[ApplicationSession.TRUSTID].ToString()));

            if (objResults != null)
            {
                if (objResults.resultDT.Rows.Count > 0)
                {
                    string strACStartDate = objResults.resultDT.Rows[0][TrustBO.TRUST_ACCOUNTSTARTDATE].ToString();
                    strYear = strACStartDate.ToString().Split(new char[] { '/' });
                    intacYear = Convert.ToInt32(strYear[2]);
                }

            }
            #endregion


            for (int i = intacYear; i <= DateTime.Now.Year; i++)
            {
                ddlYear.Items.Add(i.ToString());
            }
            ddlYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;
        }
        #endregion

        #region Add New Leave
        protected void lnkAddNewLeaveMaster_OnClick(object sender, EventArgs e)
        {
            ClearAll();
            PanelGrid_VisibilityMode(2);
        }


        #endregion

        #region ViewList Click Event
        protected void lnkViewList_OnClick(object sender, EventArgs e)
        {
            ClearAll();
            PanelGrid_VisibilityMode(1);
        }
        #endregion

        #region FeesGroup GridView Events [RowCommand,PreRender]
        protected void gvLeaveMaster_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                LeaveMBl objLeaveMBL = new LeaveMBl();

                

                if (e.CommandName.ToString() == "Edit1")
                {
                    ViewState["Mode"] = "Edit";
                    ViewState["LeaveID"] = e.CommandArgument.ToString();

                    objResult = objLeaveMBL.LeaveM_Select(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (objResult != null)
                    {
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            txtLeaveCode.Text = objResult.resultDT.Rows[0][LeaveMBo.LEAVEM_LEAVECODE].ToString();
                            txtLeaveName.Text = objResult.resultDT.Rows[0][LeaveMBo.LEAVEM_LEAVENAME].ToString();
                            txtLeaveDescription.Text = objResult.resultDT.Rows[0][LeaveMBo.LEAVEM_LEAVEDESCRIPTION].ToString();
                            txtLOpening.Text = objResult.resultDT.Rows[0][LeaveMBo.LEAVEM_LEAVEOPENING].ToString();
                            txtCarryForward.Text = objResult.resultDT.Rows[0][LeaveMBo.LEAVEM_LEAVECARRYFORWARDLIMIT].ToString();
                            ddlYear.Text = objResult.resultDT.Rows[0][LeaveMBo.LEAVEM_YEAR].ToString();

                           // txtFeesGroupName.Text = objResult.resultDT.Rows[0][FeesGroupBo.FEESGROUP_FEEGROUPNAME].ToString();
                           // ddlLedger.SelectedValue = objResult.resultDT.Rows[0][FeesGroupBo.FEESGROUP_LedgerID].ToString();
                            PanelGrid_VisibilityMode(2);
                        }
                    }
                }
                else if (e.CommandName.ToString() == "Delete1")
                {
                    objResult = objLeaveMBL.LeaveM_Delete(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record Deleted Successfully');</script>");
                        PanelGrid_VisibilityMode(1);
                       // bindYear();
                        BindLeave();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region OnpreRender
        protected void gvLeaveMaster_OnPreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvLeaveMaster.Rows.Count > 0)
                {
                    gvLeaveMaster.UseAccessibleHeader = true;
                    gvLeaveMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion
        
        #region ClearAll Method
        private void ClearAll()
        {
            Controls objControl = new Controls();
            objControl.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["Mode"] = "Save";

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
                lnkAddNewLeaveMaster.Visible = true;
                lnkViewList.Visible = true;

            }
        }

        #endregion

        #region Save Leave

        protected void btnSaveFeesGroup_Click(object sender, EventArgs e)
        {
            ;
            LeaveMBo objLeaveMBO = new LeaveMBo();

            
            LeaveMBl objLeaveMBL = new LeaveMBl();
            ApplicationResult objResults = new ApplicationResult();
            Controls objControls = new Controls();

            objLeaveMBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
            objLeaveMBO.LeaveCode = txtLeaveCode.Text.Trim();
            objLeaveMBO.LeaveName = txtLeaveName.Text.Trim();
            objLeaveMBO.LeaveDescription = txtLeaveDescription.Text;
            objLeaveMBO.LeaveOpening =Convert .ToInt32 ( txtLOpening.Text);
            objLeaveMBO.LeaveCarryForwardLimit = Convert.ToInt32(txtCarryForward.Text);
            objLeaveMBO.Year =Convert .ToInt32 ( ddlYear.SelectedItem.Text);
            objLeaveMBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
            objLeaveMBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();

            //objLeaveMBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
            //objLeaveMBO.FeeGroupName = txtFeesGroupName.Text.Trim();
            //objLeaveMBO.LedgerID = Convert.ToInt32(ddlLedger.SelectedValue);

            if (ViewState["Mode"].ToString() == "Save")
            {
                objLeaveMBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                objLeaveMBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();

                objResults = objLeaveMBL.LeaveM_Insert(objLeaveMBO);

                if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                {
                    ClearAll();
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Leave Saved Successfully.');</script>");
                    BindLeave();
                    //bindYear();
                    PanelGrid_VisibilityMode(1);
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Leave Name already Exists.');</script>");
                }

            }
            else
            {
                objLeaveMBO.LeaveID = Convert.ToInt32(ViewState["LeaveID"].ToString());
                objLeaveMBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                objLeaveMBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                objLeaveMBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                objLeaveMBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();

                objResults = objLeaveMBL.LeaveM_Update(objLeaveMBO);
                if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                {

                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Leave updated successfully.');</script>");

                    //bindYear();
                    BindLeave();
                    ClearAll();
                    ViewState["Mode"] = "Save";
                    //  btnSave.Text = "Save";
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Fees Group Name already Exists.');</script>");
                }
            }
        }
       
        #endregion
        

    }
}