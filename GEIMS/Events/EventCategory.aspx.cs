using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GEIMS.BO;
using GEIMS.BL;
using GEIMS.Common;
using System.Data;
using log4net;

namespace GEIMS.Events
{
    public partial class EventCategory : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(EventCategory));
        
        #region Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            gvEventCategory.Attributes.Add("style", "word-break:break-all; word-wrap:break-word");
            try
            {
                if (!IsPostBack)
                {
                    ViewState["Mode"] = "Save";
                    BindEventCategory();

                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
            
        }
        #endregion

        #region View List Button Click Event
        protected void lnkViewList_OnClick(object sender, EventArgs e)
        {
            try 
            {
                ClearAll();
                BindEventCategory();
                
            }   
            catch(Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Add new Button Click Event
        protected void lnkAddNewClass_OnClick(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                PanelVisibility(2);
            }
            catch(Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        protected void gvEventCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #region EventCategory Gridview Event [Row Command, Pre rendar]
        protected void gvEventCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                EventCategoryBL objEventCategoryBL = new EventCategoryBL();
                if (e.CommandName.ToString() == "Edit1")
                {
                    ViewState["Mode"] = "Edit";
                    ViewState["EventCategoryID"] = e.CommandArgument.ToString();

                    objResult = objEventCategoryBL.EventCategory_Select(Convert.ToInt32(ViewState["EventCategoryID"].ToString()));
                    if (objResult != null)
                    {
                        DataTable dtResult = objResult.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            txtEventCategory.Text = dtResult.Rows[0][EventCategoryBO.EVENTCATEGORY_EVENTCATEGORYNAME].ToString();
                            txtDescription.Text = dtResult.Rows[0][EventCategoryBO.EVENTCATEGORY_EVENTDESCRIPTION].ToString();
                            PanelVisibility(2);
                        }
                    }
                }
                else if (e.CommandName.ToString() == "Delete1")
                {
                    int check=CheckRecord(Convert.ToInt32( e.CommandArgument));
                    if (check == 0)
                    {
                        objResult = objEventCategoryBL.EventCategory_Delete(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session[ApplicationSession.USERID]), DateTime.UtcNow.AddHours(5.5).ToString());

                        if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record deleted successfully.');</script>");


                            PanelVisibility(1);
                            BindEventCategory();
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You cannot delete this record because it is in used.');</script>");
                    }

                }
            }
            catch(Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        #endregion

        #region Save Button Click Event
        protected void btnSaveClass_Click(object sender, EventArgs e)
        {
            try
            {
                EventCategoryBO objEventCategoryBO = new EventCategoryBO();
                EventCategoryBL objEventCategoryBL = new EventCategoryBL();
                ApplicationResult objResult = new ApplicationResult();
                DataTable dtResult = new DataTable();
                int intEventCategoryID = 0;

                objEventCategoryBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                objEventCategoryBO.EventCategoryName = txtEventCategory.Text.Trim();
                objEventCategoryBO.EventDescription = txtDescription.Text.Trim();
                objEventCategoryBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                objEventCategoryBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();

                if (ViewState["Mode"].ToString() == "Save")
                {
                    intEventCategoryID = -1;

                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    intEventCategoryID = Convert.ToInt32(ViewState["EventCategoryID"].ToString());
                }

                objResult = objEventCategoryBL.EventCategory_ValidateName(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), intEventCategoryID, objEventCategoryBO.EventCategoryName);
                if (objResult != null)
                {
                    dtResult = objResult.resultDT;
                    if (dtResult.Rows.Count > 0)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Event Category Name Already Exists.');</script>");
                    }
                    else
                    {
                        if (ViewState["Mode"].ToString() == "Save")
                        {
                            objEventCategoryBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                            objEventCategoryBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                            objResult = objEventCategoryBL.EventCategory_Insert(objEventCategoryBO);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record saved successfully.');</script>");
                            }
                        }
                        else if (ViewState["Mode"].ToString() == "Edit")
                        {
                            objEventCategoryBO.EventCategoryID = Convert.ToInt32(ViewState["EventCategoryID"].ToString());
                            objResult = objEventCategoryBL.EventCategory_Update(objEventCategoryBO);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record updated successfully.');</script>");
                            }
                        }
                          
                        ClearAll();
                        BindEventCategory();
                        PanelVisibility(1);
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
            
        }
        #endregion

        #region Bind EventCategory For GridView
        public void BindEventCategory()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                EventCategoryBL objEventCategoryBL = new EventCategoryBL();

                objResult = objEventCategoryBL.EventCategory_Select_By_Trust(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));

                if (objResult.resultDT.Rows.Count > 0)
                {
                    gvEventCategory.DataSource = objResult.resultDT;
                    gvEventCategory.DataBind();
                    PanelVisibility(1);
                }
                else
                {
                    PanelVisibility(2);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Clear ALL Method
        public void ClearAll()
        {
            Controls objControl = new Controls();
            objControl.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["EventCategoryID"] = null;
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
            else if (intcode == 2 && ViewState["Mode"].ToString() == "Edit")
            {
               
                btnSaveClass.Text = "Update";
                divGrid.Visible = false;
                tabs.Visible = true;
                lnkViewList.Visible = true;

            }
            else if (intcode == 2 && ViewState["Mode"].ToString() == "Save")
            {
                btnSaveClass.Text = "Save";
                divGrid.Visible = false;
                tabs.Visible = true;
                lnkViewList.Visible = true;
            }

            
        }

        #endregion

        #region Check Record For Delete
        public int CheckRecord(int intID)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                EventCategoryBL objEventCategoryBL = new EventCategoryBL();

                objResult = objEventCategoryBL.CheckRecordForDelete(intID);

                return objResult.resultDT.Rows.Count;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}