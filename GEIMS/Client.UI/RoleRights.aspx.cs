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
    public partial class RoleRights : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(RoleRights));

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ViewState["Mode"] = "Save";
                    ViewState["RoleScreenID"] = 0;
                    ViewState["SchoolMID"] = 0;
                    //bindTrust();
                    divRoleRight.Visible = false;
                    divSchool.Visible = false;
                    // divTrust.Visible = false;
                    divRole.Visible = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical Problem. Contact to your Administrator.');</script>");
            }
        }
        #endregion

        #region Bind Role
        public void BindRole()
        {
            Controls objControls = new Controls();
            RoleBL objRoleBL = new RoleBL();
            ApplicationResult objResult = new ApplicationResult();
            objResult = objRoleBL.Role_SelectAll();

            if (objResult != null)
            {
                DataTable dtRole = new DataTable();
                dtRole = objResult.resultDT;
                if (dtRole.Rows.Count > 0)
                {
                    dtRole = objResult.resultDT;
                    objControls.BindDropDown_ListBox(dtRole, ddlRole, "RoleName", "RoleID");
                    ddlRole.Items.Insert(0, new ListItem("-Select-", "-1"));
                }
            }
        }
        #endregion

        #region Bind Role only Admin and SuperAdmin
        public void BindRoleAdminSuperAdmin()
        {
            Controls objControls = new Controls();
            RoleBL objRoleBL = new RoleBL();
            ApplicationResult objResult = new ApplicationResult();
            objResult = objRoleBL.Role_SelectAllAdminSuperAdmin();

            if (objResult != null)
            {
                DataTable dtRole = new DataTable();
                dtRole = objResult.resultDT;
                if (dtRole.Rows.Count > 0)
                {
                    dtRole = objResult.resultDT;
                    objControls.BindDropDown_ListBox(dtRole, ddlRole, "RoleName", "RoleID");
                    ddlRole.Items.Insert(0, new ListItem("-Select-", "-1"));
                }
            }
        }
        #endregion

        #region Bind Screen
        private void BindScreen()
        {
            if (ddlRole.SelectedValue != "-1")
            {
                RoleRightsBL objRoleRightsBL = new RoleRightsBL();
                ApplicationResult objResult = new ApplicationResult();
                objResult = objRoleRightsBL.RoleRightsScreenName_SelectAll(Convert.ToInt32(rblSelect.SelectedValue));
                if (objResult != null)
                {
                    DataTable dtResult = new DataTable();
                    dtResult = objResult.resultDT;
                    if (dtResult.Rows.Count > 0)
                    {
                        gvSelectRights.Visible = true;
                        gvSelectRights.DataSource = dtResult;
                        gvSelectRights.DataBind();
                        divRoleRight.Visible = true;
                    }
                    else
                    {
                        divRoleRight.Visible = false;
                        gvSelectRights.Visible = false;
                    }
                }
            }
        }
        #endregion

        #region Role Drop Down Selected Index Change
        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RoleRightsBL objRoleRightsBL = new RoleRightsBL();
                DataTable dtRoleRight = new DataTable();
                ApplicationResult objApplicationResult = new ApplicationResult();
                DataTable dt = new DataTable();
                dt.Rows.Clear();
                gvSelectRights.DataSource = dt;
                gvSelectRights.DataBind();
                BindScreen();
                
                objApplicationResult = objRoleRightsBL.RoleRights_T_Select(Convert.ToInt32(ddlRole.SelectedValue), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(ViewState["SchoolMID"].ToString()), 0);
                dtRoleRight = objApplicationResult.resultDT;
                if (dtRoleRight.Rows.Count > 0)
                {
                    int j = 0;
                    foreach (GridViewRow rowItem in gvSelectRights.Rows)
                    {
                        for (int i = 0; i < dtRoleRight.Rows.Count; i++)
                        {
                            if (gvSelectRights.Rows[j].Cells[0].Text.ToString() == dtRoleRight.Rows[i]["ScreenID"].ToString())
                            {
                                CheckBox chk = (CheckBox)gvSelectRights.Rows[j].FindControl("chkRights");
                                chk.Checked = true;
                                if (ddlRole.SelectedValue == "1")
                                {
                                    chk.Enabled = true;
                                }
                                else
                                {
                                    chk.Enabled = true;
                                }
                            }
                        }
                        j++;
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical Problem. Contact to your Administrator.');</script>");
            }
        }
        #endregion

        #region Save Button Click Event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                RoleRightsBO objRoleRightsBO = new RoleRightsBO();
                RoleRightsBL objRoleRightsBL = new RoleRightsBL();
                ApplicationResult objResultsInsert = new ApplicationResult();
                ApplicationResult objResultsSelect = new ApplicationResult();
                ApplicationResult objResultsDelete = new ApplicationResult();
                DataTable dtRoleRights = new DataTable();
                int j = 0;
                CheckBox chk;
                for (int i = 0; i < gvSelectRights.Rows.Count; i++)
                {
                    objResultsSelect = objRoleRightsBL.RoleRights_T_Select(Convert.ToInt32(ddlRole.SelectedValue), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), 0, Convert.ToInt32(gvSelectRights.Rows[i].Cells[0].Text));
                    chk = (CheckBox)gvSelectRights.Rows[i].Cells[2].FindControl("chkRights");
                    if (chk.Checked == true)
                    {
                        if (rblSelect.SelectedValue == "0")
                        {
                            if (objResultsSelect != null)
                            {
                                if (objResultsSelect.resultDT.Rows.Count > 0)
                                {

                                }
                                else
                                {
                                    objRoleRightsBO.RoleID = Convert.ToInt32(ddlRole.SelectedValue);
                                    objRoleRightsBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                                    objRoleRightsBO.SchoolMID = 0;
                                    objRoleRightsBO.ScreenID = Convert.ToInt32(gvSelectRights.Rows[i].Cells[0].Text);
                                    objResultsInsert = objRoleRightsBL.RoleRights_Insert(objRoleRightsBO);
                                    if (Convert.ToInt32(gvSelectRights.Rows[i].Cells[1].Text) == 1)
                                    {
                                        ApplicationResult objResults = new ApplicationResult();
                                        SchoolBL objSchool = new SchoolBL();
                                        objResults = objSchool.School_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                                        if (objResults != null)
                                        {
                                            if (objResults.resultDT.Rows.Count > 0)
                                            {
                                                foreach (DataRow dt in objResults.resultDT.Rows)
                                                {
                                                    objRoleRightsBO.RoleID = Convert.ToInt32(ddlRole.SelectedValue);
                                                    objRoleRightsBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                                                    objRoleRightsBO.SchoolMID = Convert.ToInt32(dt[0].ToString());
                                                    objRoleRightsBO.ScreenID = Convert.ToInt32(Convert.ToInt32(gvSelectRights.Rows[i].Cells[0].Text));
                                                    objResultsInsert = objRoleRightsBL.RoleRights_Insert(objRoleRightsBO);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            objRoleRightsBO.RoleID = Convert.ToInt32(ddlRole.SelectedValue);
                            objRoleRightsBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                            objRoleRightsBO.SchoolMID = Convert.ToInt32(ViewState["SchoolMID"].ToString());
                            objRoleRightsBO.ScreenID = Convert.ToInt32(Convert.ToInt32(gvSelectRights.Rows[i].Cells[0].Text));
                            objResultsInsert = objRoleRightsBL.RoleRights_Insert(objRoleRightsBO);
                        }
                    }
                    else
                    {
                        if (rblSelect.SelectedValue == "0")
                        {
                            dtRoleRights = objResultsSelect.resultDT;
                            if (objResultsSelect.resultDT.Rows.Count > 0)
                            {
                                objResultsDelete = objRoleRightsBL.RoleRights_Delete(Convert.ToInt32(ddlRole.SelectedValue), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), 0, Convert.ToInt32(gvSelectRights.Rows[i].Cells[0].Text));
                            }
                        }
                        else
                        {
                            objResultsDelete = objRoleRightsBL.RoleRights_Delete(Convert.ToInt32(ddlRole.SelectedValue), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(ViewState["SchoolMID"].ToString()), Convert.ToInt32(gvSelectRights.Rows[i].Cells[0].Text));
                        }
                    }
                }
                if (objResultsInsert.status == ApplicationResult.CommonStatusType.SUCCESS)
                {
                    ClearAll();
                    BindScreen();
                    lblMsg.Visible = true;
                    lblMsg.Text = "Roles Applied Successfully";
                }

                //objResultsSelect = objRoleRightsBL.RoleRights_Select(Convert.ToInt32(ddlRole.SelectedValue), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(ViewState["SchoolMID"].ToString()));
                //if (objResultsSelect != null)
                //{
                //    dtRoleRights = objResultsSelect.resultDT;
                //    if (dtRoleRights.Rows.Count > 0)
                //    {
                //        // objResultsDelete = objRoleRightsBL.RoleRights_Delete(Convert.ToInt32(ddlRole.SelectedValue), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(ViewState["SchoolMID"].ToString()));
                //    }
                //}

                //for (int i = 0; i < gvSelectRights.Rows.Count; i++)
                //{
                //    chk = (CheckBox)gvSelectRights.Rows[i].Cells[2].FindControl("chkRights");
                //    if (chk.Checked == true)
                //    {
                //        objRoleRightsBO.RoleID = Convert.ToInt32(ddlRole.SelectedValue);
                //        objRoleRightsBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                //        objRoleRightsBO.SchoolMID = Convert.ToInt32(ViewState["SchoolMID"].ToString());
                //        objRoleRightsBO.ScreenID = Convert.ToInt32(gvSelectRights.Rows[i].Cells[1].Text);
                //        objResultsInsert = objRoleRightsBL.RoleRights_Insert(objRoleRightsBO);
                //    }
                //}

                //
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical Problem. Contact to your Administrator.');</script>");
            }
        }
        #endregion

        #region ClearAll
        private void ClearAll()
        {
            Controls objControls = new Controls();
            ViewState["RoleScreenID"] = 0;
            ViewState["SchoolMID"] = 0;
            ViewState["Mode"] = "Save";
            divRoleRight.Visible = false;
            divSchool.Visible = false;
            // divTrust.Visible = false;
            divRole.Visible = false;
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            gvSelectRights.DataSource = dt;
            gvSelectRights.DataBind();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            rblSelect.SelectedIndex = -1;
        }
        #endregion

        #region RadioButton Select
        /// <summary>
        /// trust or school radio button selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblSelect.SelectedValue == "0")
            {
                //Added on 12/10/2022 Bhandavi
                //When we select trust after selecting some school then not showing role rights correctly
                ViewState["SchoolMID"] = 0;
                divSchool.Visible = false;
                //  divTrust.Visible = true;
                divRole.Visible = true;
                BindRoleAdminSuperAdmin();
            }
            else
            {
                bindSchool();
                divSchool.Visible = true;
                //  divTrust.Visible = true;
                divRole.Visible = true;
                BindRole();
            }
            //BindRole();
            divRoleRight.Visible = false;
        }
        #endregion

        #region bindSchool
        public void bindSchool()
        {
            try
            {
                #region Bind School
                SchoolBL objSchoolBL = new SchoolBL();
                SchoolBO objSchoolBO = new SchoolBO();
                DocumentBL objDocumentBl = new DocumentBL();
                Controls objControls = new Controls();
                ApplicationResult objResults = new ApplicationResult();

                objResults = objSchoolBL.School_SelectAll_ForDropDOwn(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                if (objResults != null)
                {
                    if (objResults.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResults.resultDT, ddlSchool, "SchoolNameEng", "SchoolMID");

                    }
                    ddlSchool.Items.Insert(0, new ListItem("-Select-", "-1"));

                }
                BindScreen();
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        //#region bindTrust
        //public void bindTrust()
        //{
        //    try
        //    {
        //        #region Bind Trust

        //        TrustBL objTrustBL = new TrustBL();
        //        TrustBO objTrustBO = new TrustBO();
        //        Controls objControls = new Controls();
        //        ApplicationResult objResultTrust = new ApplicationResult();
        //        objResultTrust = objTrustBL.Trust_SelectAll();
        //        if (objResultTrust != null)
        //        {
        //            if (objResultTrust.resultDT.Rows.Count > 0)
        //            {
        //                objControls.BindDropDown_ListBox(objResultTrust.resultDT, ddlTrust, "TrustNameEng", "TrustMID");

        //            }
        //            ddlTrust.Items.Insert(0, new ListItem("-Select-", "-1"));
        //            ddlSchool.Items.Insert(0, new ListItem("-Select-", "-1"));

        //        }

        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Error", ex);
        //        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical Problem. Contact to your Administrator.');</script>");
        //    }
        //}
        //#endregion


        //protected void ddlTrust_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    bindSchool();
        //    BindScreen();
        //    BindRole();
        //}

        /// <summary>
        /// To Get roles in drop down list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["SchoolMID"] = ddlSchool.SelectedValue;           
            BindScreen();
            
            //Changed on 27/09/2022 by Bhandavi when we change school drop down with out saving role rights empty rolerights div is showing
            divRoleRight.Visible = false;
            gvSelectRights.Visible = false;
            ViewState["RoleScreenID"] = 0;
            BindRole();
        }

        protected void ChkRightsAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)gvSelectRights.HeaderRow.FindControl("ChkRightsAll");
            foreach (GridViewRow row in gvSelectRights.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkRights");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
        }
    }
}