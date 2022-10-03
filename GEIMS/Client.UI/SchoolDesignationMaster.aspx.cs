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
namespace GEIMS.Client.UI
{
    public partial class SchoolDesignationMaster : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(SchoolDesignationMaster));

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindDesignation();
                    ViewState["Mode"] = "Save";
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
        }
        #endregion



        #region Bind Designation
        public void BindDesignation()
        {
            ApplicationResult objResult = new ApplicationResult();
            DesignationBL objDesignationbl = new DesignationBL();

            objResult = objDesignationbl.Designation_SelectAll();
            if (objResult.resultDT.Rows.Count > 0)
            {
                gvDesignation.DataSource = objResult.resultDT;
                gvDesignation.DataBind();
                PanelVisibility(1);
            }
            else
            {
                PanelVisibility(2);
            }
        }
        #endregion



        #region Save Button Click Event
        protected void btnSaveClass_OnClick(object sender, EventArgs e)
        {
            try
            {
                DesignationBO objDesignationBO = new DesignationBO();
                DesignationBL objDesignationBL = new DesignationBL();
                ApplicationResult objResult = new ApplicationResult();
                DataTable dtResult = new DataTable();
                int intDesignationID = 0;

                objDesignationBO.DesignationNameENG = txtDesignationNameENG.Text.Trim();
                objDesignationBO.DesignationNameGUJ = txtDesignationNameGUJ.Text.Trim();
                objDesignationBO.Description = txtDescription.Text.Trim();
                objDesignationBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                objDesignationBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();

                //Code For Validate Designation Name
                if (ViewState["Mode"].ToString() == "Save")
                {
                    intDesignationID = -1;
                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    intDesignationID = Convert.ToInt32(ViewState["DesignationID"].ToString());
                }
                objResult = objDesignationBL.Designation_ValidateName(intDesignationID, objDesignationBO.DesignationNameENG);
                if (objResult != null)
                {
                    dtResult = objResult.resultDT;
                    if (dtResult.Rows.Count > 0)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Designation name already exist.');</script>");
                    }
                    else
                    {
                        if (ViewState["Mode"].ToString() == "Save")
                        {
                            objDesignationBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                            objDesignationBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                            objResult = objDesignationBL.Designation_Insert(objDesignationBO);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record saved successfully.');</script>");
                            }
                        }
                        else if (ViewState["Mode"].ToString() == "Edit")
                        {
                            objDesignationBO.DesignationID = Convert.ToInt32(ViewState["DesignationID"].ToString());
                            objResult = objDesignationBL.Designation_Update(objDesignationBO);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record updated successfully.');</script>");
                            }
                        }
                        ClearAll();
                        BindDesignation();
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

        #region Add New Button Click Event
        protected void lnkAddNewClass_OnClick(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                PanelVisibility(2);
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region View List Button Click event
        protected void lnkViewList_OnClick(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                PanelVisibility(1);
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion



        #region Designation GridView Events [Row Command, Pre Render]
        protected void gvDesignation_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                DesignationBL objDesignationBL = new DesignationBL();
                if (e.CommandName.ToString() == "Edit1")
                {
                    ViewState["Mode"] = "Edit";
                    ViewState["DesignationID"] = e.CommandArgument.ToString();
                    objResult = objDesignationBL.Designation_Select(Convert.ToInt32(ViewState["DesignationID"].ToString()));
                    if (objResult != null)
                    {
                        DataTable dtResult = objResult.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            txtDesignationNameENG.Text = dtResult.Rows[0][DesignationBO.DESIGNATION_DESIGNATIONNAMEENG].ToString();
                            txtDesignationNameGUJ.Text = dtResult.Rows[0][DesignationBO.DESIGNATION_DESIGNATIONNAMEGUJ].ToString();
                            txtDescription.Text = dtResult.Rows[0][DesignationBO.DESIGNATION_DESCRIPTION].ToString();
                            PanelVisibility(2);
                        }
                    }
                }
                else if (e.CommandName.ToString() == "Delete1")
                {
                    objResult = objDesignationBL.Designation_Delete(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session[ApplicationSession.USERID]),
                        DateTime.UtcNow.AddHours(5.5).ToString());
                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record deleted successfully.');</script>");
                        PanelVisibility(1);
                        BindDesignation();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You cannot delete this record because it is in used.');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        protected void gvDesignation_OnPreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvDesignation.Rows.Count > 0)
                {
                    gvDesignation.UseAccessibleHeader = true;
                    gvDesignation.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion



        #region Clear All Method
        public void ClearAll()
        {
            Controls objControl = new Controls();
            objControl.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["Mode"] = "Save";
        }
        #endregion

        #region Panel Visibility Mode
        public void PanelVisibility(int intcode)
        {
            if (intcode == 1)
            {
                divGrid.Visible = true;
                tabs.Visible = false;
                lnkViewList.Visible = false;
            }
            else if (intcode == 2)
            {
                divGrid.Visible = false;
                tabs.Visible = true;
                lnkViewList.Visible = true;
            }
        }
        #endregion
    }
}