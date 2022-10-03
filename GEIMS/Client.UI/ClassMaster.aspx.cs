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
    public partial class ClassMaster : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(ClassMaster));

        #region PageLoad Event
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
                    ViewState["DivisionMode"] = "Save";
                    ViewState["ClassMID"] = 0;
                    BindSection();
                    TempDivision();
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
        }
        #endregion



        #region Create Temp Division
        public void TempDivision()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add("DivisionTID", typeof(int));
                dtTemp.Columns.Add("DivisionName", typeof(string));

                ViewState["Division"] = dtTemp;
                gvDivision.DataSource = dtTemp;
                gvDivision.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion Create Temp Task



        #region DropDOwnList For Section
        public void BindSection()
        {
            try
            {
                SectionBL ObjSectionBl = new SectionBL();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();

                objResult = ObjSectionBl.Section_SelectAll_SectionMID(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlSection, "SectionName", "SectionMID");
                        ddlSection.Items.Insert(0, new ListItem("-Select-", ""));
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

        #region Bind grid for Class
        /// <summary>
        /// To bind Class gridview with all class details
        /// </summary>
        private void GridDataBind()
        {
            try
            {
                ClassBL objClasstBl = new ClassBL();
                ApplicationResult objResult = new ApplicationResult();

                objResult = objClasstBl.Class_SelectAll_WithDivision(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvClass.DataSource = objResult.resultDT;
                        gvClass.DataBind();
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



        #region View List Click Event
        protected void lnkViewList_Click(object sender, EventArgs e)
        {
            //Controls objControls = new Controls();
            //objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            //ViewState["ClassMID"] = null;
            ClearAll();
            PanelGrid_VisibilityMode(1);
        }
        #endregion

        #region Add New Click Event
        protected void lnkAddNewClass_Click(object sender, EventArgs e)
        {
            //Controls objControls = new Controls();
            //objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ClearAll();
            PanelGrid_VisibilityMode(2);
        }
        #endregion

        #region Save Class Transaction
        public void SaveClass_T()
        {
            ApplicationResult objResults = new ApplicationResult();
            DivisionTBL objDivisionBl = new DivisionTBL();
            DivisionTBO objDivisionBo = new DivisionTBO();

            objDivisionBo.ClassMID = Convert.ToInt32(ViewState["ClassMID"].ToString());

            objDivisionBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
            objDivisionBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);

            DataTable dtTask = (DataTable)ViewState["Division"];
            for (int i = 0; i < dtTask.Rows.Count; i++)
            {
                objDivisionBo.DivisionName = dtTask.Rows[i]["DivisionName"].ToString();
                objResults = objDivisionBl.DivisionT_Insert(objDivisionBo);
            }
            //if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
            //{
            //ClearAll();
            //ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Class updated successfully.');</script>");
            //GridDataBind();
            //PanelGrid_VisibilityMode(1);
            //}
        }
        #endregion

        #region Save Click Event
        /// <summary>
        /// to save entered details of class and division for new and 
        /// to modify selected class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveClass_Click(object sender, EventArgs e)
        {
            try
            {
                ClassBL objClasstBl = new ClassBL();
                ClassBO objClassBo = new ClassBO();
                DivisionTBO objDivisionBo = new DivisionTBO();
                DivisionTBL objDivisionBl = new DivisionTBL();
                Controls objControls = new Controls();
                ApplicationResult objResults = new ApplicationResult();

                if (ValidateName() == true)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Class Name " + txtClassName.Text + " And Divsion " + txtDivisionName.Text + " already Exists.');</script>");
                    goto Exit;
                }
                objClassBo.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString());
                objClassBo.SectionTID = Convert.ToInt32(ddlSection.SelectedValue);
                objClassBo.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                objClassBo.ClassName = txtClassName.Text;
                objClassBo.NoOfPeriod = Convert.ToInt32(txtNoOfPeriod.Text);
                objClassBo.ApprovalNo = txtApprovalNo.Text;
                objClassBo.ApprovalDate = txtApprovalDate.Text;
                objClassBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                objClassBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                objDivisionBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                objDivisionBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);


                #region RollBack Transaction Starts

                DatabaseTransaction.OpenConnectionTransation();
                if (ViewState["Mode"].ToString() == "Save")
                {
                    //objResults = objClasstBl.Class_ValidateName(txtClassName.Text, Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                    //if (objResults != null)
                    //{
                    //    if (objResults.resultDT.Rows.Count > 0)
                    //    {
                    //        ViewState["ClassMID"] = Convert.ToInt32(objResults.resultDT.Rows[0][0].ToString());
                    //        SaveClass_T();
                    //    }
                    //    else
                    //    {
                    objResults = objClasstBl.Class_Insert(objClassBo);
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            ViewState["ClassMID"] = Convert.ToInt32(objResults.resultDT.Rows[0][0].ToString());
                            SaveClass_T();
                            ClearAll();
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Record saved successfully.');</script>");
                            GridDataBind();
                            PanelGrid_VisibilityMode(1);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Class Name " + txtClassName.Text + " Already Exists.');</script>");
                        }

                    }

                    //    }
                    //}

                }
                else
                {
                    objClassBo.ClassMID = Convert.ToInt32(ViewState["ClassMID"].ToString());
                    objDivisionBo.ClassMID = Convert.ToInt32(ViewState["ClassMID"].ToString());
                    objResults = objClasstBl.Class_Update(objClassBo);
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {

                        //ApplicationResult objResultsDivisionUpdate = new ApplicationResult();
                        //objResultsDivisionUpdate =
                        //    objDivisionBl.DivisionT_Delete_By_Class(Convert.ToInt32(ViewState["ClassMID"].ToString()));
                        //DataTable dtTask = (DataTable)ViewState["Division"];
                        //for (int i = 0; i < dtTask.Rows.Count; i++)
                        //{
                        //    objDivisionBo.DivisionName = dtTask.Rows[i]["DivisionName"].ToString();
                        //    objResultsDivisionUpdate = objDivisionBl.DivisionT_Insert(objDivisionBo);
                        //}

                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Record updated successfully.');</script>");
                        ClearAll();
                        GridDataBind();

                        objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
                        ViewState["Mode"] = "Save";
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Class Name " + txtClassName.Text + " Already Exists.');</script>");
                    }
                }
                DatabaseTransaction.CommitTransation();
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

        #region Add Button Click Event
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            try
            {
                int intflagsave = 0;
                int intflagedit = 0;
                DataTable dtTask = new DataTable();
                dtTask = (DataTable)ViewState["Division"];
                if (ViewState["DivisionMode"].ToString() == "Save")
                {

                    if (dtTask.Rows.Count > 0)
                    {
                        string strFilter = "DivisionName = '" + txtDivisionName.Text.Trim() + "'";
                        DataRow[] results = dtTask.Select(strFilter);
                        if (results.Length > 0)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Division name already exist.');</script>");
                            intflagsave = 1;
                        }
                    }
                    if (intflagsave == 0)
                    {
                        if (ViewState["Mode"].ToString() == "Save")
                        {
                            int i = 0;
                            if (dtTask.Rows.Count > 0)
                            {
                                i = dtTask.Rows.Count;
                            }
                            i = i + 1;
                            dtTask.Rows.Add(i, txtDivisionName.Text);
                            ViewState["Division"] = dtTask;
                            gvDivision.DataSource = dtTask;
                            gvDivision.DataBind();
                            txtDivisionName.Text = "";
                        }
                        else if (ViewState["Mode"].ToString() == "Edit")
                        {
                            ApplicationResult objResults = new ApplicationResult();
                            DivisionTBL objDivisionBl = new DivisionTBL();
                            DivisionTBO objDivisionBo = new DivisionTBO();

                            objDivisionBo.ClassMID = Convert.ToInt32(ViewState["ClassMID"].ToString());

                            objDivisionBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                            objDivisionBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                            objDivisionBo.DivisionName = txtDivisionName.Text;
                            objResults = objDivisionBl.DivisionT_Insert(objDivisionBo);
                            objResults = objDivisionBl.DivisionT_Select_DivisionName_By_Class(Convert.ToInt32(ViewState["ClassMID"].ToString()));
                            if (objResults != null)
                            {
                                //  ViewState["Division"] = objResults.resultDT;
                                // gvDivision.DataSource = (DataTable)ViewState["Division"];
                                gvDivision.DataSource = objResults.resultDT;
                                gvDivision.DataBind();
                            }
                        }
                    }
                }
                if (ViewState["DivisionMode"].ToString() == "Edit")
                {
                    if (dtTask.Rows.Count > 0)
                    {
                        string strFilter = "DivisionName = '" + txtDivisionName.Text.Trim() + "'";
                        DataRow[] results = dtTask.Select(strFilter);
                        if (results.Length > 1)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Division name already exist.');</script>");
                            intflagedit = 1;
                        }
                    }
                    if (intflagedit == 0)
                    {
                        if (ViewState["Mode"].ToString() == "Save")
                        {
                            DataTable dtTaskTemp = (DataTable)ViewState["Division"];
                            foreach (DataRow dr in dtTaskTemp.Rows) // search whole table
                            {
                                if (Convert.ToInt32(dr["DivisionTID"].ToString()) ==Convert.ToInt32( ViewState["DivisionDatatableTID"].ToString())) // if id==2
                                {
                                    dr["DivisionName"] =txtDivisionName.Text; //change the name
                                    //break; break or not depending on you
                                }
                                ViewState["Division"] = dtTask;
                                gvDivision.DataSource = dtTask;
                                gvDivision.DataBind();

                            }
                        }
                        else if (ViewState["Mode"].ToString() == "Edit")
                        {
                            ApplicationResult objResults = new ApplicationResult();
                            DivisionTBL objDivisionBl = new DivisionTBL();
                            DivisionTBO objDivisionBo = new DivisionTBO();

                            objDivisionBo.ClassMID = Convert.ToInt32(ViewState["ClassMID"].ToString());
                            objDivisionBo.DivisionTID = Convert.ToInt32(ViewState["DivisionTID"].ToString());
                            objDivisionBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                            objDivisionBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                            objDivisionBo.DivisionName = txtDivisionName.Text;
                            objResults = objDivisionBl.DivisionT_Update(objDivisionBo);
                            ViewState["Mode"] = "Edit";
                            objResults = objDivisionBl.DivisionT_Select_DivisionName_By_Class(Convert.ToInt32(ViewState["ClassMID"].ToString()));
                            if (objResults != null)
                            {
                                //  ViewState["Division"] = objResults.resultDT;
                                // gvDivision.DataSource = (DataTable)ViewState["Division"];
                                gvDivision.DataSource = objResults.resultDT;
                                gvDivision.DataBind();
                            }
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



        #region Gridview Class RowCommand Event
        protected void gvClass_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ViewState["ClassMID"] = e.CommandArgument.ToString();
                Controls objControls = new Controls();
                ClassBL objClasstBl = new ClassBL();
                ApplicationResult objResults = new ApplicationResult();
                DivisionTBL objDivisionBl = new DivisionTBL();
                if (e.CommandName.ToString() == "Edit1")
                {
                    //for edit mode of class gridview
                    objResults = objClasstBl.Class_Select(Convert.ToInt32(ViewState["ClassMID"].ToString()));
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            ddlSection.SelectedValue = objResults.resultDT.Rows[0][ClassBO.CLASS_SECTIONTID].ToString();
                            txtClassName.Text = objResults.resultDT.Rows[0][ClassBO.CLASS_CLASSNAME].ToString();
                            txtApprovalNo.Text = objResults.resultDT.Rows[0][ClassBO.CLASS_APPROVALNO].ToString();
                            txtApprovalDate.Text = objResults.resultDT.Rows[0][ClassBO.CLASS_APPROVALDATE].ToString();
                            txtNoOfPeriod.Text = objResults.resultDT.Rows[0][ClassBO.CLASS_NOOFPERIOD].ToString();
                            objResults = objDivisionBl.DivisionT_Select_DivisionName_By_Class(Convert.ToInt32(ViewState["ClassMID"].ToString()));
                            if (objResults != null)
                            {
                                //  ViewState["Division"] = objResults.resultDT;
                                // gvDivision.DataSource = (DataTable)ViewState["Division"];
                                gvDivision.DataSource = objResults.resultDT;
                                gvDivision.DataBind();
                            }
                            ViewState["Mode"] = "Edit";
                            PanelGrid_VisibilityMode(2);
                        }
                    }
                }
                if (e.CommandName.ToString() == "Delete1")
                {
                    //objControls.EnableControls(Master.FindControl("ContentPlaceHolder1"));
                    //ApplicationResult objDelete = new ApplicationResult();

                    //objDelete = objClasstBl.Validate_Class_Delete(Convert.ToInt32(ViewState["ClassMID"].ToString()), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), ViewState["DivisionName"].ToString());
                    //if (objDelete != null)
                    //{
                    //    if (objDelete.resultDT.Rows.Count > 0)
                    //    {
                    //        if (Convert.ToInt32(objDelete.resultDT.Rows[0]["CurrentClassID"]) == Convert.ToInt32(ViewState["ClassMID"].ToString()))
                    //        {
                    //            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('There are Student(s) associated with this Class. To delete this Section you need to delete Student(s) first.');</script>");
                    //        }
                    //    }
                    //    else
                    //    {
                    //        objResults = objClasstBl.Class_Delete(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), ViewState["DivisionName"].ToString());
                    //        if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    //        {
                    //            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Class deleted successfully.');</script>");
                    //            ClearAll();
                    //            GridDataBind();
                    //            PanelGrid_VisibilityMode(1);
                    //        }
                    //    }
                    //}
                    ApplicationResult objResult = new ApplicationResult();
                    ClassBL objClassBl = new ClassBL();
                    objResult = objClassBl.Class_Delete(Convert.ToInt32(ViewState["ClassMID"].ToString()),
                        Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Class deleted successfully.');</script>");
                        GridDataBind();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('There are Student(s) associated with this Class. To delete this Section you need to delete Student(s) first.');</script>");
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

        #region Division GridView Events [Row Command, Pre Render]
        protected void gvDivision_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            ApplicationResult objResults = new ApplicationResult();
            DivisionTBL objDivisionTBL = new DivisionTBL();
            ViewState["DivisionTID"] = e.CommandArgument.ToString();
            Controls objControls = new Controls();
            try
            {

                if (e.CommandName.ToString() == "EditDivision")
                {
                    if (ViewState["Mode"].ToString() == "Save")
                    {
                        // GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                        DataTable dtTaskTemp = (DataTable)ViewState["Division"];
                        //txtDivisionName.Text= dtTaskTemp.Rows[0][1].ToString();
                        //txtDivisionName.Text = Convert.ToString(row.RowIndex);
                        ViewState["DivisionDatatableTID"] = e.CommandArgument.ToString();
                        string strFilter = "DivisionTID = '" + e.CommandArgument.ToString() + "'";
                        DataRow[] results = dtTaskTemp.Select(strFilter);
                        if (results.CopyToDataTable().Rows.Count > 0)
                        {
                            txtDivisionName.Text = results.CopyToDataTable().Rows[0][1].ToString();
                        }
                        ViewState["DivisionMode"] = "Edit";
                    }
                    else
                    {
                        objResults = objDivisionTBL.DivisionT_Select_By_DivisionTID(Convert.ToInt32(ViewState["DivisionTID"].ToString()));
                        if (objResults != null)
                        {
                            if (objResults.resultDT.Rows.Count > 0)
                            {
                                txtDivisionName.Text = objResults.resultDT.Rows[0][DivisionTBO.DIVISIONT_DIVISIONNAME].ToString();
                                ViewState["DivisionMode"] = "Edit";
                            }
                        }
                    }

                }
                else if (e.CommandName == "DeleteDivision")
                {
                    if (ViewState["Mode"].ToString() == "Save")
                    {
                        GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                        DataTable dtTaskTemp = (DataTable)ViewState["Division"];
                        dtTaskTemp.Rows.RemoveAt(row.RowIndex);
                        dtTaskTemp.AcceptChanges();
                        ViewState["Division"] = dtTaskTemp;
                        gvDivision.DataSource = (DataTable)ViewState["Division"];
                        gvDivision.DataBind();
                    }
                    else
                    {
                        objResults = objDivisionTBL.DivisionT_Delete(Convert.ToInt32(e.CommandArgument.ToString()));
                        if (objResults != null)
                        {
                            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Division Deleted Successfully.');</script>");
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('There are Division(s) associated with this Student. To delete this Divisions you need to delete Student(s) first.');</script>");
                            }

                        }
                        objResults = objDivisionTBL.DivisionT_Select_DivisionName_By_Class(Convert.ToInt32(ViewState["ClassMID"].ToString()));
                        if (objResults != null)
                        {
                            //  ViewState["Division"] = objResults.resultDT;
                            // gvDivision.DataSource = (DataTable)ViewState["Division"];
                            gvDivision.DataSource = objResults.resultDT;
                            gvDivision.DataBind();
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

        protected void gvClass_OnPreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvClass.Rows.Count > 0)
                {
                    gvClass.UseAccessibleHeader = true;
                    gvClass.HeaderRow.TableSection = TableRowSection.TableHeader;
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
        /// <summary>
        /// 
        /// displaying gridviews based on condition(to show class gridview or to show division gridview
        /// </summary>
        /// <param name="intMode"></param>
        private void PanelGrid_VisibilityMode(int intMode)
        {
            if (intMode == 1)
            {
                //show class gridview 
                divGrid.Visible = true;
                tabs.Visible = false;
                lnkViewList.Visible = false;
            }
            else if (intMode == 2)
            {
                //to show fields to save new class
                //to show division gridview
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
            ViewState["ClassMID"] = null;
            ViewState["Mode"] = "Save";
            DataTable dtTask = (DataTable)ViewState["Division"];
            dtTask.Rows.Clear();
            gvDivision.DataSource = dtTask;
            gvDivision.DataBind();
            ViewState["Division"] = dtTask;
        }
        #endregion



        #region ValidateName
        public bool ValidateName()
        {
            try
            {
                ClassBL objClasstBl = new ClassBL();
                ApplicationResult objResults = new ApplicationResult();
                DataTable dtClass = new DataTable();

                if (ViewState["Mode"].ToString() == "Save")
                {
                    objResults = objClasstBl.Class_ValidateName_ClassDivision(txtClassName.Text, Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), -1, txtDivisionName.Text);
                }
                else
                {
                    objResults = objClasstBl.Class_ValidateName_ClassDivision(txtClassName.Text, Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(ViewState["ClassMID"].ToString()), txtDivisionName.Text);
                }
                if (objResults.resultDT.Rows.Count > 0)

                    return true;
                return false;
            }
            catch (Exception ex)
            {
                //throw ex;
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                return false;
            }
        }
        #endregion


    }

}