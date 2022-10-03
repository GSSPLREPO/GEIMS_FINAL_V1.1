using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.BO;
using GEIMS.BL;
using GEIMS.Common;
using System.Data;

namespace GEIMS.Client.UI
{
    public partial class SubjectAssociation : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(SubjectAssociation));
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ViewState["Mode"] = "Save";
                    TempTeacher();
                    BindClass();
                    PanelVisibility(1);
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                        "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
        }
        #endregion



        #region Create Temp Teacher
        public void TempTeacher()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add("EmployeeMID", typeof(int));
                dtTemp.Columns.Add("EmployeeCodeName", typeof(string));

                ViewState["Teacher"] = dtTemp;
                gvTeacher.DataSource = dtTemp;
                gvTeacher.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion Create Temp Task



        #region Bind Class
        public void BindClass()
        {
            ApplicationResult objResult = new ApplicationResult();
            ClassBL objClassBl = new ClassBL();
            Controls objControl = new Controls();
            objResult = objClassBl.Class_SelectAll(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
            if (objResult != null)
            {
                objControl.BindDropDown_ListBox(objResult.resultDT, ddlClass, "ClassName", "ClassMID");
            }
            ddlClass.Items.Insert(0, new ListItem("--Select--", ""));
            ddlDivision.Items.Insert(0, new ListItem("--Select--", ""));
        }
        #endregion

        #region Bind Division
        public void BindDivision(int intClassMID)
        {
            ApplicationResult objResult = new ApplicationResult();
            DivisionTBL objDivisionTbl = new DivisionTBL();
            Controls objControl = new Controls();
            objResult = objDivisionTbl.DivisionT_Select_By_Class(intClassMID);
            if (objResult != null)
            {
                objControl.BindDropDown_ListBox(objResult.resultDT, ddlDivision, "DivisionName", "DivisionTID");
            }
            ddlDivision.Items.Insert(0, new ListItem("--Select--", ""));
        }
        #endregion

        #region Bind Subject Association
        public void BindSubjectAssociation()
        {
            ApplicationResult objResult = new ApplicationResult();
            SubjectTBL objSubjectTbl = new SubjectTBL();

            objResult = objSubjectTbl.SubjectT_SelectAll_By_Class_Division(Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlDivision.SelectedValue));
            if (objResult.resultDT.Rows.Count > 0)
            {
                gvSubjectAssociation.DataSource = objResult.resultDT;
                gvSubjectAssociation.DataBind();
                PanelVisibility(1);
            }
            else
            {
                BindSubject(2);
                PanelVisibility(2);
            }
        }
        #endregion

        #region Bind Subject
        public void BindSubject(int intMode)
        {
            ApplicationResult objResult = new ApplicationResult();
            SubjectTBL objSubjectTbl = new SubjectTBL();
            Controls objControl = new Controls();
            objResult = objSubjectTbl.Subject_Select_By_Class_Division(Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlDivision.SelectedValue), intMode, Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString()));
            if (objResult != null)
            {
                objControl.BindDropDown_ListBox(objResult.resultDT, ddlSubject, "NameEng", "SubjectMID");
            }
            ddlSubject.Items.Insert(0, new ListItem("--Select--", ""));
        }
        #endregion



        #region Class DropDown Selected Index Change Event
        protected void ddlClass_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlClass.SelectedValue != "")
                {
                    BindDivision(Convert.ToInt32(ddlClass.SelectedValue));
                    DataTable dtTeacher = (DataTable)ViewState["Teacher"];
                    dtTeacher.Rows.Clear();
                    gvTeacher.DataSource = dtTeacher;
                    gvTeacher.DataBind();
                    ViewState["Teacher"] = dtTeacher;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Division DropDown  Selected Index Change Event
        protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlDivision.SelectedValue != "")
                {

                    if (ViewState["DropDownMode"].ToString() == "Load")
                    {
                        BindSubjectAssociation();
                    }
                    else if (ViewState["DropDownMode"].ToString() == "New")
                    {
                        BindSubject(2);
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



        #region Add Button Click Event
        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (txtTeacher.Text.Trim() != "")
                {
                    if (txtTeacher.Text.Trim() == hfEmployeeCodeName.Value)
                    {
                        int intflag = 0;
                        DataTable dtTeacher = new DataTable();
                        dtTeacher = (DataTable)ViewState["Teacher"];
                        if (dtTeacher.Rows.Count > 0)
                        {
                            string strFilter = "EmployeeMID = '" + Convert.ToInt32(hfEmployeeMID.Value) + "'";
                            DataRow[] results = dtTeacher.Select(strFilter);
                            if (results.Length > 0)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Teacher name already exist.');</script>");
                                intflag = 1;
                            }
                        }
                        if (intflag == 0)
                        {
                            dtTeacher.Rows.Add(Convert.ToInt32(hfEmployeeMID.Value), txtTeacher.Text);
                            ViewState["Teacher"] = dtTeacher;
                            gvTeacher.DataSource = dtTeacher;
                            gvTeacher.DataBind();
                            txtTeacher.Text = "";
                            ViewState["Count"] = dtTeacher.Rows.Count;
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Enter valid teacher name.');</script>");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Enter teacher name.');</script>");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }

        }
        #endregion

        #region Save Button Click Event
        protected void btnSaveClass_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Count"] != null)
                {
                    if (Convert.ToInt32(ViewState["Count"].ToString()) > 0)
                    {
                        ApplicationResult objResult = new ApplicationResult();
                        SubjectTBO objSubjectTbo = new SubjectTBO();
                        SubjectTBL objSubjectTbl = new SubjectTBL();
                        string strEmployeeMIDs = null;
                        objSubjectTbo.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString());
                        objSubjectTbo.ClassMID = Convert.ToInt32(ddlClass.SelectedValue);
                        objSubjectTbo.DivisionTID = Convert.ToInt32(ddlDivision.SelectedValue);
                        objSubjectTbo.SubjectMID = Convert.ToInt32(ddlSubject.SelectedValue);
                        DataTable dtTeacher = (DataTable)ViewState["Teacher"];
                        for (int i = 0; i < dtTeacher.Rows.Count; i++)
                        {
                            if (strEmployeeMIDs != null)
                            {
                                strEmployeeMIDs += "~";
                            }
                            strEmployeeMIDs += dtTeacher.Rows[i]["EmployeeMID"].ToString();
                        }
                        if (ViewState["Mode"].ToString() == "Save")
                        {
                            objResult = objSubjectTbl.SubjectT_Insert(objSubjectTbo, strEmployeeMIDs);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record saved successfully.');</script>");
                        }
                        else if (ViewState["Mode"].ToString() == "Edit")
                        {
                            objResult = objSubjectTbl.SubjectT_Update(objSubjectTbo, strEmployeeMIDs);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record updated successfully.');</script>");
                        }
                        ClearAll();
                        BindSubjectAssociation();
                        PanelVisibility(1);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Please Add teachers First.');</script>");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Please Add teachers First.');</script>");
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
                BindSubject(2);
                PanelVisibility(2);
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
                BindSubjectAssociation();
                PanelVisibility(1);
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion



        #region Subject Association Grid View Events [Row Command]
        protected void gvSubjectAssociation_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                SubjectTBL objSubjectTbl = new SubjectTBL();
                SubjectTBO objSubjectTbo = new SubjectTBO();
                objSubjectTbo.ClassMID = Convert.ToInt32(ddlClass.SelectedValue);
                objSubjectTbo.DivisionTID = Convert.ToInt32(ddlDivision.SelectedValue);
                objSubjectTbo.SubjectMID = Convert.ToInt32(e.CommandArgument.ToString());
                if (e.CommandName == "Edit1")
                {
                    BindSubject(1);
                    ViewState["Mode"] = "Edit";
                    objResult = objSubjectTbl.SubjectT_Select(objSubjectTbo);
                    ddlSubject.SelectedValue =
                        objResult.resutlDS.Tables[0].Rows[0][SubjectTBO.SUBJECTT_SUBJECTMID].ToString();
                    ViewState["Teacher"] = objResult.resutlDS.Tables[1];
                    gvTeacher.DataSource = (DataTable)ViewState["Teacher"];
                    gvTeacher.DataBind();
                    PanelVisibility(2);
                }
                else if (e.CommandName == "Delete1")
                {
                    objResult = objSubjectTbl.SubjectT_Delete(objSubjectTbo);
                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                            "<script>alert('Record deleted successfully.');</script>");
                        BindSubjectAssociation();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                            "<script>alert('You cannot delete this record because it is in used.');</script>");
                    }
                    ClearAll();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                    "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Teacher Grid View Event [Row Command]
        protected void gvTeacher_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DeleteTeacher")
                {
                    if (ViewState["Mode"].ToString() == "Save")
                    {
                        GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                        DataTable dtTeacher = (DataTable)ViewState["Teacher"];
                        dtTeacher.Rows.RemoveAt(row.RowIndex);
                        dtTeacher.AcceptChanges();
                        ViewState["Teacher"] = dtTeacher;
                        gvTeacher.DataSource = (DataTable)ViewState["Teacher"];
                        gvTeacher.DataBind();
                    }
                    else if (ViewState["Mode"].ToString() == "Edit")
                    {
                        ApplicationResult objResult = new ApplicationResult();
                        SubjectTBL objSubjectTbl = new SubjectTBL();
                        SubjectTBO objSubjectTbo = new SubjectTBO();
                        objSubjectTbo.ClassMID = Convert.ToInt32(ddlClass.SelectedValue);
                        objSubjectTbo.DivisionTID = Convert.ToInt32(ddlDivision.SelectedValue);
                        objSubjectTbo.SubjectMID = Convert.ToInt32(ddlSubject.SelectedValue);
                        objSubjectTbo.EmployeeMID = Convert.ToInt32(e.CommandArgument.ToString());
                        objResult = objSubjectTbl.SubjectT_Delete_Association(objSubjectTbo);
                        if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {
                            objResult = objSubjectTbl.SubjectT_Select(objSubjectTbo);
                            ViewState["Teacher"] = objResult.resutlDS.Tables[1];
                            gvTeacher.DataSource = (DataTable)ViewState["Teacher"];
                            gvTeacher.DataBind();
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                "<script>alert('Record deleted successfully.');</script>");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                "<script>alert('You cannot delete this record because it is in used.');</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                    "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion



        #region Clear All Method
        public void ClearAll()
        {
            Controls objControl = new Controls();
            objControl.ClearForm(Master.FindControl("ContentPlaceHolder1").FindControl("tabs"));
            ViewState["Mode"] = "Save";

            DataTable dtTeacher = (DataTable)ViewState["Teacher"];
            dtTeacher.Rows.Clear();
            gvTeacher.DataSource = dtTeacher;
            gvTeacher.DataBind();
            ViewState["Teacher"] = dtTeacher;
        }
        #endregion

        #region Panel Visibility Mode
        public void PanelVisibility(int intcode)
        {
            if (intcode == 1)
            {
                gvSubjectAssociation.Visible = true;
                tabs.Visible = false;
                ViewState["DropDownMode"] = "Load";
                btnViewList.Visible = false;
            }
            else if (intcode == 2)
            {
                gvSubjectAssociation.Visible = false;
                tabs.Visible = true;
                ViewState["DropDownMode"] = "New";
                btnViewList.Visible = true;
            }
            if (ViewState["Mode"].ToString() == "Edit")
            {
                ddlClass.Enabled = false;
                ddlDivision.Enabled = false;
                ddlSubject.Enabled = false;
            }
            else if (ViewState["Mode"].ToString() == "Save")
            {
                ddlClass.Enabled = true;
                ddlDivision.Enabled = true;
                ddlSubject.Enabled = true;
            }
        }
        #endregion



        #region Get Employee Web Service Method
        [System.Web.Services.WebMethod]
        public static string[] GetAllEmployeeName(string prefixText,string schoolMID)
        {
            EmployeeMBL objEmployeeMbl = new EmployeeMBL();
            ApplicationResult objResult = new ApplicationResult();
            string strSearchText = prefixText + "%";
            List<string> result = new List<string>();
            objResult = objEmployeeMbl.EmployeeM_Select_EmployeeName(strSearchText, schoolMID);
            if (objResult != null)
            {
                for (int i = 0; i < objResult.resultDT.Rows.Count; i++)
                {
                    string strEmployeeCodeName = objResult.resultDT.Rows[i]["EmployeeCodeName"].ToString();
                    string strEmployeeMID = objResult.resultDT.Rows[i][EmployeeMBO.EMPLOYEEM_EMPLOYEEMID].ToString();
                    result.Add(string.Format("{0}~{1}", strEmployeeCodeName, strEmployeeMID));
                }
            }
            return result.ToArray();
        }
        #endregion
    }
}