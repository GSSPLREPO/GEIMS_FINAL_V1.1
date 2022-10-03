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
    public partial class BudgetSubHeadingMaster : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(ClassMaster));

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    bindgrid();
                    BindBudgetCategory();
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

        #region Save
        protected void btnSaveClass_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlBudgetCategory.SelectedIndex != 0 && ddlBudgetHeading.SelectedIndex != 0)
                {
                    BudgetSubHeadingMBO objBudgetSubHeadingMBO = new BudgetSubHeadingMBO();
                    BudgetSubHeadingMBL objBudgetSubHeadingMBL = new BudgetSubHeadingMBL();
                    ApplicationResult objResult = new ApplicationResult();
                    ApplicationResult objResultSelect = new ApplicationResult();
                    DataTable dtResult = new DataTable();
                    int intBudgetSubHeadingMID = 0;

                    objBudgetSubHeadingMBO.BudgetCategoryMID = Convert.ToInt32(ddlBudgetCategory.SelectedValue);
                    objBudgetSubHeadingMBO.BudgetHeadingMID = Convert.ToInt32(ddlBudgetHeading.SelectedValue);
                    objBudgetSubHeadingMBO.SubHeadingName = txtSubHeading.Text.ToString().Trim();
                    objBudgetSubHeadingMBO.IsDeleted = 0;


                    //Code For Validate SubHeadingM Name
                    if (ViewState["Mode"].ToString() == "Save")
                    {
                        intBudgetSubHeadingMID = -1;
                    }
                    else if (ViewState["Mode"].ToString() == "Edit")
                    {
                        intBudgetSubHeadingMID = Convert.ToInt32(ViewState["BudgetSubHeadingMID"].ToString());
                    }
                    objResult = objBudgetSubHeadingMBL.SubHeadingM_ValidateName(objBudgetSubHeadingMBO.BudgetCategoryMID, objBudgetSubHeadingMBO.BudgetHeadingMID,intBudgetSubHeadingMID, objBudgetSubHeadingMBO.SubHeadingName);
                    
                    if (objResult != null)
                    {
                        dtResult = objResult.resultDT;
                        int colValidation = Convert.ToInt32(dtResult.Rows[0][0]);
                        //if (dtResult.Rows.Count > 0)
                        if (colValidation > 0)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('SubHeading Name Already exist.');</script>");
                        }
                        else
                        {
                            if (ViewState["Mode"].ToString() == "Save")
                            {
                                objBudgetSubHeadingMBO.CreatedBy = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                objBudgetSubHeadingMBO.CreatedDate = DateTime.UtcNow.AddHours(5.5);
                                objResult = objBudgetSubHeadingMBL.BudgetSubHeading_Insert(objBudgetSubHeadingMBO);
                                if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record saved successfully.');</script>");
                                }
                            }
                            else if (ViewState["Mode"].ToString() == "Edit")
                            {
                                objBudgetSubHeadingMBO.BudgetSubHeadingMID = Convert.ToInt32(intBudgetSubHeadingMID);

                                //IF BgetHeadingMID is pass out another table Recrod can not delete or Edit
                                objResultSelect = objBudgetSubHeadingMBL.BudgetSubHeading_M_Cascade(Convert.ToInt32(intBudgetSubHeadingMID), Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                                if(objResultSelect != null)
                                {
                                    DataTable dtResult1 = objResultSelect.resultDT;
                                    int col = Convert.ToInt32(dtResult1.Rows[0][0]);
                                    if (col == 0)
                                    {
                                        objBudgetSubHeadingMBO.ModifiedBy = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                        objBudgetSubHeadingMBO.ModifiedDate = DateTime.UtcNow.AddHours(5.5);
                                        objResult = objBudgetSubHeadingMBL.BudgetSubHeading_Update(objBudgetSubHeadingMBO);
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
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! BudgetSubHeadingMID is Null!.');</script>");
                                }                            
                            }
                            ClearAll();
                            bindgrid();
                            PanelGrid_VisibilityMode(1);
                        }
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! Select DropDown!.');</script>");
                    ClearAll();
                    bindgrid();
                    PanelGrid_VisibilityMode(1);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region PreRender
        protected void gvSubject_OnPreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvBudgetSubHeading.Rows.Count > 0)
                {
                    gvBudgetSubHeading.UseAccessibleHeader = true;
                    gvBudgetSubHeading.HeaderRow.TableSection = TableRowSection.TableHeader;
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
        protected void gvSubject_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                ApplicationResult objResult1 = new ApplicationResult();
                ApplicationResult objResultSelect = new ApplicationResult();
                DataTable dt = new DataTable();
                BudgetSubHeadingMBL objBudgetSubHeadingMBL = new BudgetSubHeadingMBL();

                Controls objControls = new Controls();

                if (e.CommandName.ToString() == "Edit1")
                {
                    ViewState["Mode"] = "Edit";
                    ViewState["BudgetSubHeadingMID"] = e.CommandArgument.ToString();
                   
                    objResult = objBudgetSubHeadingMBL.BudgetSubHeading_SelectById(Convert.ToInt32(ViewState["BudgetSubHeadingMID"].ToString()));

                    if (objResult != null)
                    {
                        DataTable dtResult = objResult.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            ddlBudgetCategory.Text = dtResult.Rows[0][BudgetSubHeadingMBO.BudgetHeading_BUDGETCATEGORYMID].ToString();

                            int catid = Convert.ToInt32(dtResult.Rows[0][2]);
                            int Headid = Convert.ToInt32(dtResult.Rows[0][1]);

                            dt = FetchHeading(Convert.ToInt32(dtResult.Rows[0][BudgetSubHeadingMBO.BudgetHeading_BUDGETCATEGORYMID]), Headid);
                            if (dt.Rows.Count > 0)
                            {
                                objControls.BindDropDown_ListBox(dt, ddlBudgetHeading, "HeadingName", "BudgetHeadingMID");
                            }
                            //ddlBudgetHeading.Text = dtResult.Rows[0]["BudgetHeadingMID"].ToString();
                            txtSubHeading.Text = dtResult.Rows[0][BudgetSubHeadingMBO.BudgetHeading_SUBHEADINGNAME].ToString();

                            PanelGrid_VisibilityMode(2);
                        }
                    }
                }
                else if (e.CommandName.ToString() == "Delete1")
                {
                    int id = Convert.ToInt32(e.CommandArgument.ToString());

                    //IF BgetSubHeadingMID is pass out another table Recrod can not delete or Edit
                    objResultSelect = objBudgetSubHeadingMBL.BudgetSubHeading_M_Cascade(Convert.ToInt32(id), Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                    if (objResultSelect != null)
                    {
                        DataTable dtResult1 = objResultSelect.resultDT;
                        int col = Convert.ToInt32(dtResult1.Rows[0][0]);
                        if (col == 0)
                        {
                            objResult = objBudgetSubHeadingMBL.BudgetSubHeading_Delete(id);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record deleted successfully.');</script>");
                                PanelGrid_VisibilityMode(1);
                                bindgrid();
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You cannot delete this record because it is in used.');</script>");
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You can not delete this record. It is already in used.');</script>");
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! BudgetSubHeadingMID is Null!.');</script>");
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
                PanelGrid_VisibilityMode(2);
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region View List Click Event
        protected void lnkViewList_OnClick(object sender, EventArgs e)
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

        #region BindGrid
        public void bindgrid()
        {
            try
            {
                BudgetSubHeadingMBL ObjBudgetSubHeadingMBL = new BudgetSubHeadingMBL();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();

                objResult = ObjBudgetSubHeadingMBL.BudgetSubHeading_SelectAll();
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvBudgetSubHeading.DataSource = objResult.resultDT;
                        gvBudgetSubHeading.DataBind();
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
                        ddlBudgetHeading.Items.Insert(0, new ListItem("-Select-", ""));
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

        #region Change Event for BudgetCategory Wise BudgetHeading
        protected void ddlBudgetCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlBudgetHeading.ClearSelection();
                BudgetSubHeadingMBL ObjBudgetSubHeadingMBL = new BudgetSubHeadingMBL();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();

                objResult = ObjBudgetSubHeadingMBL.BudgetHeading_SelectDropDownByCapId(Convert.ToInt32(ddlBudgetCategory.SelectedValue));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetHeading, "HeadingName", "BudgetHeadingMID");
                        ddlBudgetHeading.Items.Insert(0, new ListItem("-Select-", "0"));
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

        #region Fetch Heading data For DropDown 
        private DataTable FetchHeading(int intBudgetCategoryMID, int intBudgetHeadingMID)
        {
            //DataTable dtDivision = new DataTable();

            BudgetSubHeadingMBL objBudgetSubHeadingMBL = new BudgetSubHeadingMBL();
            BudgetSubHeadingMBO objBudgetSubHeadingMBO = new BudgetSubHeadingMBO();
            ApplicationResult objResults = new ApplicationResult();

            objResults = objBudgetSubHeadingMBL.BudgetHeading_SelectDropDownById(intBudgetCategoryMID, intBudgetHeadingMID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {

            }
            return objResults.resultDT;
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
            ViewState["BudgetSubHeadingMID"] = null;
            ViewState["Mode"] = "Save";
            ddlBudgetHeading.Items.Insert(0, new ListItem("-Select-", ""));

        }
        #endregion

    }
}