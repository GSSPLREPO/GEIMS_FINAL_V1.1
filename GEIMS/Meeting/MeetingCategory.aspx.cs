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

namespace GEIMS.Meeting
{
    public partial class MeetingCategory : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(MeetingCategory));

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            gvMeetingCategory.Attributes.Add("style", "word-break:break-all; word-wrap:break-word");
            try
            {
                if (!IsPostBack)
                {
                    ViewState["Mode"] = "Save";
                    BindMeetingCategory();

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
                BindMeetingCategory();

            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Button Save Click Event
        protected void btnSaveClass_Click(object sender, EventArgs e)
        {

            try
            {
                MeetingCategoryBO objMeetingCategoryBO = new MeetingCategoryBO();
                MeetingCategoryBL objMeetingCategoryBL = new MeetingCategoryBL();
                ApplicationResult objResult = new ApplicationResult();
                DataTable dtResult = new DataTable();
                int intCategoryID = 0;

                objMeetingCategoryBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                objMeetingCategoryBO.CategoryName = txtCategoryName.Text.Trim();
                objMeetingCategoryBO.CategoryDescription = txtDescription.Text.Trim();
                objMeetingCategoryBO.LastModifiedByID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                objMeetingCategoryBO.LastModifiedByDate = DateTime.UtcNow.AddHours(5.5).ToString();

                if (ViewState["Mode"].ToString() == "Save")
                {
                    intCategoryID = -1;

                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    intCategoryID = Convert.ToInt32(ViewState["CategoryID"].ToString());
                }

                objResult = objMeetingCategoryBL.MeetingCategory_ValidateName(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), intCategoryID, objMeetingCategoryBO.CategoryName);
                if (objResult != null)
                {
                    dtResult = objResult.resultDT;
                    if (dtResult.Rows.Count > 0)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Meeting Categoryname Already Exists.');</script>");
                    }
                    else
                    {
                        if (ViewState["Mode"].ToString() == "Save")
                        {
                            objMeetingCategoryBO.CreatedByID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                            objMeetingCategoryBO.CreatedByDate = DateTime.UtcNow.AddHours(5.5).ToString();
                            objResult = objMeetingCategoryBL.MeetingCategory_Insert(objMeetingCategoryBO);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record saved successfully.');</script>");
                            }
                        }
                        else if (ViewState["Mode"].ToString() == "Edit")
                        {
                            objMeetingCategoryBO.CategoryID = Convert.ToInt32(ViewState["CategoryID"].ToString());
                            objResult = objMeetingCategoryBL.MeetingCategory_Update(objMeetingCategoryBO);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record updated successfully.');</script>");
                            }
                        }

                        ClearAll();
                        BindMeetingCategory();
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

        #region BindMeetingCategory Method 
        public void BindMeetingCategory()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                MeetingCategoryBL objMeetingCategoryBL = new MeetingCategoryBL();

                objResult = objMeetingCategoryBL.MeetingCategory_Select_By_Trust(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));

                if (objResult.resultDT.Rows.Count > 0)
                {
                    gvMeetingCategory.DataSource = objResult.resultDT;
                    gvMeetingCategory.DataBind();
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
            try
            {
                Controls objControl = new Controls();
                objControl.ClearForm(Master.FindControl("ContentPlaceHolder1"));
                ViewState["EventCategoryID"] = null;
                ViewState["Mode"] = "Save";
            }
            catch(Exception ex)
            {
                throw ex;
            }
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

        #region GridView Row Command for Edit and Delete Data
        protected void gvMeetingCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                MeetingCategoryBL objMeetingCategoryBL = new MeetingCategoryBL();
                if (e.CommandName.ToString() == "Edit1")
                {
                    ViewState["Mode"] = "Edit";
                    ViewState["CategoryID"] = e.CommandArgument.ToString();

                    objResult = objMeetingCategoryBL.MeetingCategory_Select_For_Edit(Convert.ToInt32(ViewState["CategoryID"].ToString()));
                    if (objResult != null)
                    {
                        DataTable dtResult = objResult.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            txtCategoryName.Text = dtResult.Rows[0][MeetingCategoryBO.MEETINGCATEGORY_CATEGORYNAME].ToString();
                            txtDescription.Text = dtResult.Rows[0][MeetingCategoryBO.MEETINGCATEGORY_CATEGORYDESCRIPTION].ToString();
                            PanelVisibility(2);
                        }
                    }
                }
                else if (e.CommandName.ToString() == "Delete1")
                {
                        objResult = objMeetingCategoryBL.MeetingCategory_Select_For_Delete(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session[ApplicationSession.USERID]), DateTime.UtcNow.AddHours(5.5).ToString());
                        
                        if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record deleted successfully.');</script>");

                            PanelVisibility(1);
                            BindMeetingCategory();
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

        //protected void gvMeetingCategory_PreRender(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (gvMeetingCategory.Rows.Count > 0)
        //        {
        //            gvMeetingCategory.UseAccessibleHeader = true;
        //            gvMeetingCategory.HeaderRow.TableSection = TableRowSection.TableHeader;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Error", ex);
        //        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
        //    }

        //}

        #endregion
    }
}