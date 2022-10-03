using System;
using System.Data;
using System.Web.UI.WebControls;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using System.Web.UI;
using log4net;
using GEIMS.DataAccess;

namespace GEIMS.Client.UI
{
    public partial class BudgetHeadingMaster : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(ClassMaster));
  
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    bindgrid();
                    BindBudgetCategory();
                    ViewState["Mode"] = "Save";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region PreRender
        protected void gvBudgetHeading_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvBudgetHeading.Rows.Count > 0)
                {
                    gvBudgetHeading.UseAccessibleHeader = true;
                    gvBudgetHeading.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator11.');</script>");
            }
        }
        #endregion

        #region Edit / Delete
        protected void gvBudgetHeading_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                ApplicationResult objResultSelect = new ApplicationResult();
                BudgetHeadingMBL objBudgetHeadingMBL = new BudgetHeadingMBL();
                if (e.CommandName.ToString() == "Edit1")
                {
                    ViewState["Mode"] = "Edit";
                    ViewState["BudgetHeadingMID"] = e.CommandArgument.ToString();
                    objResult = objBudgetHeadingMBL.BudgetHeading_SelectById(Convert.ToInt32(ViewState["BudgetHeadingMID"].ToString()));
                    //int id = Convert.ToInt32(e.CommandArgument.ToString());                 
                    //objResult = objBudgetHeadingMBL.BudgetHeading_SelectById(id);

                    if (objResult != null)
                    {
                        DataTable dtResult = objResult.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            ddlBudgetCategory.Text = dtResult.Rows[0][BudgetHeadingMBO.BudgetHeading_BUDGETCATEGORYMID].ToString();
                            txtDepartmentNameGUJ.Text = dtResult.Rows[0][BudgetHeadingMBO.BudgetHeading_HEADINGNAME].ToString();

                            PanelGrid_VisibilityMode(2);
                        }
                    }
                }
                else if (e.CommandName.ToString() == "Delete1")
                {
                    int id = Convert.ToInt32(e.CommandArgument.ToString());

                    //IF BgetHeadingMID is pass out another table Recrod can not delete or Edit
                    objResultSelect = objBudgetHeadingMBL.BudgetHeading_M_Cascade_Delete(Convert.ToInt32(id), Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                    if (objResultSelect != null)
                    {
                        DataTable dtResult1 = objResultSelect.resultDT;
                        int col = Convert.ToInt32(dtResult1.Rows[0][0]);
                        if (col == 0)
                        {
                            //int id = Convert.ToInt32(e.CommandArgument.ToString());
                            objResult = objBudgetHeadingMBL.BudgetHeading_Delete(id);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record deleted successfully.');</script>");
                                PanelGrid_VisibilityMode(1);
                                bindgrid();
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record not deleted.');</script>");
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You can not delete this record. It is already in used.');</script>");
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! BudgetHeadingMID is Null!.');</script>");
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
                lnkViewList.Visible = true;
            }
        }
        #endregion PanelGrid_VisibilityMode

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["BudgetHeadingMID"] = null;
            ViewState["Mode"] = "Save";

        }
        #endregion

        #region Save
        protected void btnSaveClass_Click(object sender, EventArgs e)
        {
            //Save
            try
            {
                BudgetHeadingMBO objBudgetHeadingMBO = new BudgetHeadingMBO();
                BudgetHeadingMBL objBudgetHeadingMBL = new BudgetHeadingMBL();
                ApplicationResult objResult = new ApplicationResult();
                ApplicationResult objResultSelect = new ApplicationResult();
                DataTable dtResult = new DataTable();
                int intBudgetHeadingMID = 0;

                objBudgetHeadingMBO.BudgetCategoryMID = Convert.ToInt32(ddlBudgetCategory.SelectedValue);
                objBudgetHeadingMBO.HeadingName = txtDepartmentNameGUJ.Text.ToString().Trim();
                objBudgetHeadingMBO.IsDeleted = 0;

                //Code For Validate SubjectM Name
                if (ViewState["Mode"].ToString() == "Save")
                {
                    intBudgetHeadingMID = -1;
                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    intBudgetHeadingMID = Convert.ToInt32(ViewState["BudgetHeadingMID"].ToString());
                }
                objResult = objBudgetHeadingMBL.HeadingM_ValidateName(intBudgetHeadingMID, objBudgetHeadingMBO.HeadingName);
                if (objResult != null)
                {
                    dtResult = objResult.resultDT;
                    if (dtResult.Rows.Count > 0)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Heading Name Already exist.');</script>");
                    }
                    else
                    {
                        if (ViewState["Mode"].ToString() == "Save")
                        {
                            if (ddlBudgetCategory.SelectedIndex != 0)
                            {
                                objBudgetHeadingMBO.CreatedBy = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                objBudgetHeadingMBO.CreatedDate = DateTime.UtcNow.AddHours(5.5);
                                objResult = objBudgetHeadingMBL.BudgetHeading_Insert(objBudgetHeadingMBO);
                                if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record Save successfully.');</script>");
                                    bindgrid();
                                    PanelGrid_VisibilityMode(1);
                                }
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Select Budget Category!.');</script>");
                            }
                        }
                        else if (ViewState["Mode"].ToString() == "Edit")
                        {
                            if (ddlBudgetCategory.SelectedIndex != 0)
                            {
                                objBudgetHeadingMBO.BudgetHeadingMID = Convert.ToInt32(intBudgetHeadingMID);

                                //IF BgetHeadingMID is pass out another table Recrod can not delete or Edit
                                objResultSelect = objBudgetHeadingMBL.BudgetHeading_M_Cascade(Convert.ToInt32(intBudgetHeadingMID), Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                                if (objResultSelect != null)
                                {
                                    DataTable dtResult1 = objResultSelect.resultDT;
                                    int col = Convert.ToInt32(dtResult1.Rows[0][0]);
                                    if (col == 0)
                                    {
                                        objBudgetHeadingMBO.ModifiedBy = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                        objBudgetHeadingMBO.ModifiedDate = DateTime.UtcNow.AddHours(5.5);
                                        objResult = objBudgetHeadingMBL.BudgetHeading_Update(objBudgetHeadingMBO);
                                        if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                                        {
                                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record updated successfully.');</script>");
                                        }
                                        else
                                        {
                                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record not updated.');</script>");
                                        }
                                    }
                                    else
                                    {
                                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You cannot Update this record because it is in used.');</script>");
                                    }
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! BudgetHeadingMID is Null!.');</script>");
                                }
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Select Budget Category!.');</script>");
                            }
                        }
                        ClearAll();
                        bindgrid();
                        PanelGrid_VisibilityMode(1);
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

        #region BindGrid
        public void bindgrid()
        {
            try
            {
                BudgetHeadingMBL ObjBudgetHeadingMBL = new BudgetHeadingMBL();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();

                objResult = ObjBudgetHeadingMBL.BudgetHeading_SelectAll();
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvBudgetHeading.DataSource = objResult.resultDT;
                        gvBudgetHeading.DataBind();
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

        #region Bind Budget Category
        public void BindBudgetCategory()
        {
            try
            {
                BudgetCategoryMBL ObjBudgetHeadingSelectDropDownBL = new BudgetCategoryMBL();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();

                objResult = ObjBudgetHeadingSelectDropDownBL.BudgetHeading_SelectDropDown();
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetCategory, "CategoryName", "BudgetCategoryMID");
                        ddlBudgetCategory.Items.Insert(0, new ListItem("-Select-", ""));
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

        #region View List Click Event
        protected void lnkViewList_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                PanelGrid_VisibilityMode(1);
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Add New Button Click Event
        protected void lnkAddNewClass_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                PanelGrid_VisibilityMode(2);
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