using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.Client.UI
{
    public partial class SubjectMaster : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(SubjectMaster));

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindSubject();
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

        #region Bind Subject
        public void BindSubject()
        {
            ApplicationResult objResult = new ApplicationResult();
            SubjectMBL objSubjectMbl=new SubjectMBL();

            objResult = objSubjectMbl.SubjectM_Select_By_School(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
            if (objResult.resultDT.Rows.Count > 0)
            {
                gvSubject.DataSource = objResult.resultDT;
                gvSubject.DataBind();
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
                SubjectMBO objSubjectMBO = new SubjectMBO();
                SubjectMBL objSubjectMBL = new SubjectMBL();
                ApplicationResult objResult = new ApplicationResult();
                DataTable dtResult = new DataTable();
                int intSubjectID = 0;

                objSubjectMBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                objSubjectMBO.NameEng = txtSubjectNameENG.Text.Trim();
                objSubjectMBO.NameGuj = txtSubjectNameGUJ.Text.Trim();
                objSubjectMBO.Description = txtDescription.Text.Trim();
                objSubjectMBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                objSubjectMBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();

                //Code For Validate SubjectM Name
                if (ViewState["Mode"].ToString() == "Save")
                {
                    intSubjectID = -1;
                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    intSubjectID = Convert.ToInt32(ViewState["SubjectMID"].ToString());
                }
                objResult = objSubjectMBL.SubjectM_ValidateName(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), intSubjectID, objSubjectMBO.NameEng);
                if (objResult != null)
                {
                    dtResult = objResult.resultDT;
                    if (dtResult.Rows.Count > 0)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Subject name already exist.');</script>");
                    }
                    else
                    {
                        if (ViewState["Mode"].ToString() == "Save")
                        {
                            objSubjectMBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                            objSubjectMBO.CreatedDate = System.DateTime.UtcNow.AddHours(5.5).ToString();
                            objResult = objSubjectMBL.SubjectM_Insert(objSubjectMBO);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record saved successfully.');</script>");
                            }
                        }
                        else if (ViewState["Mode"].ToString() == "Edit")
                        {
                            objSubjectMBO.SubjectMID = Convert.ToInt32(ViewState["SubjectMID"].ToString());
                            objResult = objSubjectMBL.SubjectM_Update(objSubjectMBO);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record updated successfully.');</script>");
                            }
                        }
                        ClearAll();
                        BindSubject();
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

        #region Subject GridView Events [Row Command, Pre Render]
        protected void gvSubject_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                SubjectMBL objSubjectMBL = new SubjectMBL();
                if (e.CommandName.ToString() == "Edit1")
                {
                    ViewState["Mode"] = "Edit";
                    ViewState["SubjectMID"] = e.CommandArgument.ToString();
                    objResult = objSubjectMBL.SubjectM_Select(Convert.ToInt32(ViewState["SubjectMID"].ToString()));
                    if (objResult != null)
                    {
                        DataTable dtResult = objResult.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            txtSubjectNameENG.Text = dtResult.Rows[0][SubjectMBO.SUBJECTM_NAMEENG].ToString();
                            txtSubjectNameGUJ.Text = dtResult.Rows[0][SubjectMBO.SUBJECTM_NAMEGUJ].ToString();
                            txtDescription.Text = dtResult.Rows[0][SubjectMBO.SUBJECTM_DESCRIPTION].ToString();
                            PanelVisibility(2);
                        }
                    }
                }
                else if (e.CommandName.ToString() == "Delete1")
                {
                    objResult = objSubjectMBL.SubjectM_Delete(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session[ApplicationSession.USERID]),
                        DateTime.UtcNow.AddHours(5.5).ToString());
                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record deleted successfully.');</script>");
                        PanelVisibility(1);
                        BindSubject();
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

        protected void gvSubject_OnPreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvSubject.Rows.Count > 0)
                {
                    gvSubject.UseAccessibleHeader = true;
                    gvSubject.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            else if(intcode == 2)
            {
                divGrid.Visible = false;
                tabs.Visible = true;
                lnkViewList.Visible = true;
            }
        }
        #endregion
    }
}