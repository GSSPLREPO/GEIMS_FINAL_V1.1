using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using log4net;
using GEIMS.DataAccess;

namespace GEIMS.Client.UI
{
    public partial class StudentPastEducationDetail : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(StudentPastEducationDetail));

        #region Pageload Event
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
                    ViewState["Mode"] = "Save";
                    BindYear();
                    BindSection();

                    divStudentPanel.Visible = false;
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
        }
        #endregion

        #region BindYear
        public void BindYear()
        {
            try
            {
                Controls objControls = new Controls();

                int currentYear = DateTime.Now.Year;
                CommonFunctions CF = new CommonFunctions();
                DataTable dtYear = CF.CreateDTYear();

                if (dtYear.Rows.Count > 0)
                {
                    objControls.BindDropDown_ListBox(dtYear, ddlPassingYear, "AcademicYear", "AcademicYear");
                }
                ddlPassingYear.Items.Insert(0, new ListItem("-Select-", ""));
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }

        }
        #endregion

        #region Bind grid
        private void GridDataBind()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                StudentPreEducationDetailTBL ObjPreEducationBL = new StudentPreEducationDetailTBL();

                objResult = ObjPreEducationBL.StudentPreEducationDetailT_SelectAll(Convert.ToInt32(ViewState["StudentMID"].ToString()));
                if (objResult != null)
                {
                    gvEducation.DataSource = objResult.resultDT;
                    gvEducation.DataBind();
                    if (objResult.resultDT.Rows.Count > 0)
                    {

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

        #region Gridview Row COmmand Event
        protected void gvStudent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Controls objControls = new Controls();
                StudentBL objStudentBL = new StudentBL();
                ApplicationResult objResults = new ApplicationResult();
                if (e.CommandName.ToString() == "Edit1")
                {
                    ViewState["StudentMID"] = Convert.ToInt32(e.CommandArgument.ToString());
                    divStudentPanel.Visible = true;
                    objResults = objStudentBL.Student_Select(Convert.ToInt32(ViewState["StudentMID"].ToString()),0);
                    divStudentPanel.Visible = true; 
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {

                            ViewState["DivisionName"] = objResults.resultDT.Rows[0][StudentBO.STUDENT_CURRENTDIVISIONTID].ToString();
                            #region Find DivisionName
                            DivisionTBL objDivision = new DivisionTBL();
                            ApplicationResult objResultsDivision = new ApplicationResult();
                            objResultsDivision = objDivision.DivisionT_Select_By_DivisionTID(Convert.ToInt32(objResults.resultDT.Rows[0][StudentBO.STUDENT_CURRENTDIVISIONTID].ToString()));
                            if (objResultsDivision != null)
                            {
                                if (objResultsDivision.resultDT.Rows.Count > 0)
                                {
                                    ViewState["Division"] = objResultsDivision.resultDT.Rows[0][DivisionTBO.DIVISIONT_DIVISIONNAME].ToString();
                                }

                            }
                            #endregion

                            #region Find SectionName
                            SectionBL objSection = new SectionBL();
                            ApplicationResult objResultsSection = new ApplicationResult();
                            objResultsSection = objSection.Section_Select(Convert.ToInt32(objResults.resultDT.Rows[0][StudentBO.STUDENT_CURRENTSECTIONID].ToString()));
                            if (objResultsSection != null)
                            {
                                if (objResultsSection.resultDT.Rows.Count > 0)
                                {
                                    ViewState["SectionName"] = objResultsSection.resultDT.Rows[0][SectionBO.SECTION_SECTIONNAME].ToString();
                                }

                            }
                            #endregion

                            #region Find Class
                            ClassBL objClass = new ClassBL();
                            ApplicationResult objResultsClass = new ApplicationResult();
                            objResultsClass = objClass.Class_Select(Convert.ToInt32(objResults.resultDT.Rows[0][StudentBO.STUDENT_CURRENTCLASSID].ToString()));
                            if (objResultsClass != null)
                            {

                                if (objResultsClass.resultDT.Rows.Count > 0)
                                {
                                    ViewState["ClassMID"] = objResultsClass.resultDT.Rows[0][ClassBO.CLASS_CLASSNAME].ToString();
                                }

                            }
                            #endregion

                            lblAdmissionNo.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_ADMISSIONNO].ToString();
                            lblCurrentGrNo.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_CURRENTGRNO].ToString();
                            lblStudentNameEng.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_STUDENTLASTNAMEENG].ToString() + " " + objResults.resultDT.Rows[0][StudentBO.STUDENT_STUDENTFIRSTNAMEENG].ToString() + " " + objResults.resultDT.Rows[0][StudentBO.STUDENT_STUDENTMIDDLENAMEENG].ToString();
                            lblClassDivision.Text = ViewState["ClassMID"].ToString() + "-" + ViewState["Division"].ToString();
                            lblCurrentSection.Text = ViewState["SectionName"].ToString();
                            lblAcademicYear.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_CURRENTYEAR].ToString();
                            ViewState["AcademicYear"] = objResults.resultDT.Rows[0][StudentBO.STUDENT_CURRENTYEAR].ToString();

                        }
                    }
                   
                    GridDataBind();

                    ClearPastEducationDetails();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        /// <summary>
        /// 27/10/2022 Bhandavi
        /// to clear all fields of divDetails when adding new education details
        /// </summary>
        private void ClearPastEducationDetails()
        {
            //clearDetails
            divStudentPanel.Visible = true;
            txtSchoolName.Text = "";
            txtSchoolAddress.Text = "";
            txtMediumName.Text = "";
            txtPassedExam.Text = "";
            txtBoardName.Text = "";
            ddlPassingYear.SelectedIndex = 0;
            txtState.Text = "";
            txtDistrict.Text = "";
            txtTaluka.Text = "";
            txtTown.Text = "";
            ViewState["Mode"] = "Save";
        }
        #endregion

        #region Education Gridview Row Command

        /// <summary>
        /// Commnet added by Bhandavi 27/10/2022
        /// To get details of selected students pre education details(Previous school,exam, year)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvEducation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Controls objControls = new Controls();
                StudentPreEducationDetailTBL objPreEducationBL = new StudentPreEducationDetailTBL();
                ApplicationResult objResults = new ApplicationResult();
                if (e.CommandName.ToString() == "Edit1")
                {
                    ViewState["StudentEducationDetailTID"] = Convert.ToInt32(e.CommandArgument.ToString());
                    //Convert.ToInt32(ViewState["StudentMID"].ToString())
                    objResults = objPreEducationBL.StudentPreEducationDetailT_Select(Convert.ToInt32(ViewState["StudentEducationDetailTID"].ToString()));

                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            divStudentPanel.Visible = true;
                            txtSchoolName.Text = objResults.resultDT.Rows[0][StudentPreEducationDetailTBO.STUDENTPASTEDUCATIONDETAILT_SCHOOLNAME].ToString();
                            txtSchoolAddress.Text = objResults.resultDT.Rows[0][StudentPreEducationDetailTBO.STUDENTPASTEDUCATIONDETAILT_ADDRESS].ToString();
                            txtMediumName.Text = objResults.resultDT.Rows[0][StudentPreEducationDetailTBO.STUDENTPASTEDUCATIONDETAILT_MEDIUMNAME].ToString();
                            txtPassedExam.Text = objResults.resultDT.Rows[0][StudentPreEducationDetailTBO.STUDENTPASTEDUCATIONDETAILT_PASSEDEXAM].ToString();
                            txtBoardName.Text = objResults.resultDT.Rows[0][StudentPreEducationDetailTBO.STUDENTPASTEDUCATIONDETAILT_BOARDNAME].ToString();
                            ddlPassingYear.SelectedValue = objResults.resultDT.Rows[0][StudentPreEducationDetailTBO.STUDENTPASTEDUCATIONDETAILT_PASSINGYEAR].ToString();
                            txtState.Text = objResults.resultDT.Rows[0][StudentPreEducationDetailTBO.STUDENTPASTEDUCATIONDETAILT_STATE].ToString();
                            txtDistrict.Text = objResults.resultDT.Rows[0][StudentPreEducationDetailTBO.STUDENTPASTEDUCATIONDETAILT_DISTRICT].ToString();
                            txtTaluka.Text = objResults.resultDT.Rows[0][StudentPreEducationDetailTBO.STUDENTPASTEDUCATIONDETAILT_TALUKA].ToString();
                            txtTown.Text = objResults.resultDT.Rows[0][StudentPreEducationDetailTBO.STUDENTPASTEDUCATIONDETAILT_TOWN].ToString();
                            ViewState["Mode"] = "Edit";
                            
                        }
                        
                        GridDataBind();
                        divDetails.Visible = true;
                    }

                }
                else if (e.CommandName.ToString() == "Delete1")
                {
                    ApplicationResult objResultsDelete = new ApplicationResult();


                    objResultsDelete = objPreEducationBL.StudentPreEducationDetailT_Delete(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (objResultsDelete.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClearAll();
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Student Education Details deleted successfully.');</script>");
                        GridDataBind();
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

        #region Save Click Event
        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                ApplicationResult objResults = new ApplicationResult();
                StudentPreEducationDetailTBL objPreEducationBL = new StudentPreEducationDetailTBL();
                StudentPreEducationDetailTBO objPreEducationBO = new StudentPreEducationDetailTBO();

                objPreEducationBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString());
                objPreEducationBO.StudentMID = Convert.ToInt32(ViewState["StudentMID"].ToString());
                objPreEducationBO.SchoolName = txtSchoolName.Text;
                objPreEducationBO.Address = txtSchoolAddress.Text;
                objPreEducationBO.MediumName = txtMediumName.Text;
                objPreEducationBO.PassedExam = txtPassedExam.Text;
                objPreEducationBO.BoardName = txtBoardName.Text;
                objPreEducationBO.PassingYear = ddlPassingYear.SelectedItem.ToString();
                objPreEducationBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                objPreEducationBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                objPreEducationBO.Town = txtTown.Text;
                objPreEducationBO.Taluka = txtTaluka.Text;
                objPreEducationBO.District = txtDistrict.Text;
                objPreEducationBO.State = txtState.Text;
              

                #region RollBack Transaction Starts

               // DatabaseTransaction.OpenConnectionTransation();
                if (ViewState["Mode"].ToString() == "Save")
                {
                    objResults = objPreEducationBL.StudentPreEducationDetailT_Insert(objPreEducationBO);
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        divDetails.Visible = false;
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Student Education Details Saved Successfully.');</script>");
                        //added code here because after pasing exam/passing year already exists message comes also clear all fields of divDetails
                        ClearPastEducationDetails();

                        ClearAll();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Passing Exam/ Passing Year is already exists .');</script>");
                    }

                }
                else
                {

                    objPreEducationBO.StudentEducationDetailTID = Convert.ToInt32(ViewState["StudentEducationDetailTID"].ToString());
                    objResults = objPreEducationBL.StudentPreEducationDetailT_Update(objPreEducationBO);
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Student Education Details Update Successfully.');</script>");
                        //added code here because after pasing exam/passing year already exists message comes also clear all fields of divDetails
                        ClearAll();
                        ViewState["Mode"] = "Edit";
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Passing Exam/ Passing Year is already exists .');</script>");
                    }
                }
                //DatabaseTransaction.CommitTransation();
                //ClearAll(); commented on 27/10/2022
                GridDataBind();
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Go for Searching Student
        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                StudentBL objStudentBL = new StudentBL();
                StudentBO objStudentBO = new StudentBO();

                ApplicationResult objResultProgram = new ApplicationResult();
                objResultProgram = objStudentBL.Student_Search_StudentName(txtSearchName.Text.Trim(), Convert.ToInt32(ddlSearchBy.SelectedValue), Convert.ToInt32(Session[ApplicationSession.ROLEID].ToString()), Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString()));
                if (objResultProgram != null)
                {

                    if (objResultProgram.resultDT.Rows.Count > 0)
                    {
                        gvStudent.Visible = true;
                        gvStudent.DataSource = objResultProgram.resultDT;
                        gvStudent.DataBind();
                        divStudentPanel.Visible = false;
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('No Record Found.');</script>");
                        gvStudent.Visible = false;
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


        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            ViewState["StudentEducationDetailTID"] = 0;
            ViewState["Mode"] = "Save";
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
        }
        #endregion


        #region Get Student Web Service Method
        [System.Web.Services.WebMethod]
        public static string[] GetAllStudentNameForReport(string prefixText, int SearchType, int SchoolMID, int SectionType, int ClassType, int DivisionType)
        {
            StudentBL objEmployeeMbl = new StudentBL();
            ApplicationResult objResult = new ApplicationResult();
            string strSearchText = prefixText + "%";
            List<string> result = new List<string>();
            objResult = objEmployeeMbl.Student_ForAutocomplete(strSearchText, SearchType, SchoolMID, SectionType, ClassType, DivisionType);
            if (objResult != null)
            {
                for (int i = 0; i < objResult.resultDT.Rows.Count; i++)
                {
                    if (SearchType == 1)
                    {
                        string strStudentName = objResult.resultDT.Rows[i]["StudentName"].ToString();
                        string strStudentMID = objResult.resultDT.Rows[i][StudentBO.STUDENT_STUDENTMID].ToString();
                        result.Add(string.Format("{0}~{1}", strStudentName, strStudentMID));
                    }
                    else if (SearchType == 2)
                    {
                        string strStudentGRNo = objResult.resultDT.Rows[i]["CurrentGrNo"].ToString();
                        string strEmployeeMID = objResult.resultDT.Rows[i][StudentBO.STUDENT_STUDENTMID].ToString();
                        result.Add(string.Format("{0}~{1}", strStudentGRNo, strEmployeeMID));
                    }
                    else if (SearchType == 3)
                    {
                        string strAdmission = objResult.resultDT.Rows[i]["AdmissionNo"].ToString();
                        string strEmployeeMID = objResult.resultDT.Rows[i][StudentBO.STUDENT_STUDENTMID].ToString();
                        result.Add(string.Format("{0}~{1}", strAdmission, strEmployeeMID));
                    }
                    else if (SearchType == 4)
                    {
                        string strUniqueID = objResult.resultDT.Rows[i]["UniqueID"].ToString();
                        string strEmployeeMID = objResult.resultDT.Rows[i][StudentBO.STUDENT_STUDENTMID].ToString();
                        result.Add(string.Format("{0}~{1}", strUniqueID, strEmployeeMID));
                    }
                }
            }
            return result.ToArray();
        }

        #endregion

        #region Bind Section
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
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlSection1, "SectionName", "SectionMID");
                        ddlSection1.Items.Insert(0, new ListItem("-Select-", ""));
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

        #region Bind Class
        public void BindClass()
        {
            try
            {
                ClassBL objClassBL = new ClassBL();
                ClassBO objClassBO = new ClassBO();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();

                int SchoolID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                int SectionID = Convert.ToInt32(ddlSection1.SelectedValue.ToString());

                objResult = objClassBL.Find_Class_SectionWise(SchoolID, SectionID);

                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlClassName1, "ClassName", "ClassMID");
                        ddlClassName1.Items.Insert(0, new ListItem("-Select-", "0"));
                    }
                    else
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlClassName1, "ClassName", "ClassMID");
                        ddlDivisionName1.Items.Insert(0, new ListItem("-Select-", "0"));
                        ddlDivisionName1.ClearSelection();
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

        #region Bind Division
        public void BindDivision()
        {
            try
            {
                ClassBL objClassBL = new ClassBL();
                ClassBO objClassBO = new ClassBO();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();

                int ClassID = Convert.ToInt32(ddlClassName1.SelectedValue.ToString());

                objResult = objClassBL.Find_Division_ClassWise(ClassID);

                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlDivisionName1, "DivisionName", "DivisionTID");
                        ddlDivisionName1.Items.Insert(0, new ListItem("-Select-", "0"));
                    }
                    else
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlDivisionName1, "DivisionName", "DivisionTID");
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

        protected void ddlSection1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindClass();
        }

        protected void ddlClassName1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDivision();
        }

        /// <summary>
        /// 28/10/2022 Bhandavi
        /// To clear all fields in the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["Mode"] = "Save";
                ddlClassName1.Items.Clear();
                ddlClassName1.Items.Insert(0, new ListItem("-Select-", "0"));
                ddlDivisionName1.Items.Clear();
                ddlDivisionName1.Items.Insert(0, new ListItem("-Select-", "0"));
                ddlSearchBy.SelectedIndex = 0;
                ddlSection1.SelectedIndex = 0;
                txtSearchName.Text = "";
                divStudentPanel.Visible = false;
                gvStudent.Visible = false;
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
    }
}