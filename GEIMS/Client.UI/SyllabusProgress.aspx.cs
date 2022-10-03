using System;
using System.IO;
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
using GEIMS.DataAccess;
using log4net;

namespace GEIMS.Client.UI
{
    public partial class SyllabusProgress : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(SyllabusMaster));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEmployeeList();
                ViewState["Mode"] = "Save";
            }
        }
        #region Bind Employees
        public void BindEmployeeList()
        {
            try
            {
                Controls objControls = new Controls();
                EmployeeBL objEmployeeBl = new EmployeeBL();
                ApplicationResult objResult = new ApplicationResult();
                objResult = objEmployeeBl.Employee_SelectAll_ForReportingTo();
                if (objResult.resultDT.Rows.Count > 0)
                {
                    objControls.BindDropDown_ListBox(objResult.resultDT, ddlEmployeeList, "EmployeeName", "EmployeeMID");
                    objControls.BindDropDown_ListBox(objResult.resultDT, ddlTeacherList, "EmployeeName", "EmployeeMID");

                }
                ddlEmployeeList.Items.Insert(0, new ListItem("-Select-", ""));
                ddlTeacherList.Items.Insert(0, new ListItem("-Select-", ""));
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region BindYear
        public void BindYear()
        {
            try
            {

                int intMonth = 0;


                Controls objControls = new Controls();
                int month = System.DateTime.Now.Month;
                int Year = System.DateTime.Now.Year;
                int lastTwoDigit = Year % 100;
                string yr = string.Empty;
                if (month >= intMonth)
                    yr = (lastTwoDigit.ToString() + (lastTwoDigit + 1).ToString()).ToString();
                else
                    yr = ((lastTwoDigit - 1).ToString() + lastTwoDigit.ToString()).ToString();


                int f = (Convert.ToInt32(yr.Substring(0, 2)));
                int l = (Convert.ToInt32(yr.Substring(2, 2)));

                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("AcademicYear", typeof(string)));

                for (int i = 0; i < 5; i++)
                {
                    dr = dt.NewRow();
                    if (i == 0)
                    {
                        dr["AcademicYear"] = Convert.ToString(f.ToString() + "-" + l.ToString());
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        if ((f - 1).ToString().Length < 2)
                        {
                            if (f.ToString().Length == 2)
                            {
                                dr["AcademicYear"] = Convert.ToString("0" + (f - 1).ToString() + "-" + (f).ToString());
                            }
                            else
                            {
                                dr["AcademicYear"] = Convert.ToString("0" + (f - 1).ToString() + "-" + "0" + (f).ToString());
                            }
                            dt.Rows.Add(dr);
                        }
                        else
                        {
                            dr["AcademicYear"] = Convert.ToString((f - 1).ToString() + "-" + (f).ToString());
                            dt.Rows.Add(dr);
                        }
                        f = f - 1;
                        l = f;
                    }
                }

                objControls.BindDropDown_ListBox(dt, ddlYear, "AcademicYear", "AcademicYear");
                ddlYear.Items.Insert(0, new ListItem("--Select--", ""));

            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }




            //try
            //{
            //    int currentYear = DateTime.Now.Year;
            //    for (int i = 1990; i <= currentYear; i++)
            //    {
            //        ddlYear.Items.Add(Convert.ToString(i));

            //    }
            //    ddlYear.Items.Insert(0, "Select");
            //}
            //catch (Exception ex)
            //{
            //    logger.Error("Error", ex);
            //    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            //}

        }
        #endregion

        protected void ddlEmployeeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                SyllabusPlanningBL objSyllabusPLanningbl = new SyllabusPlanningBL();

                int TeacherMID = Convert.ToInt32(ddlEmployeeList.SelectedValue);

                objResult = objSyllabusPLanningbl.SyllabusPlanningListByTeacehrID(TeacherMID);
                if (objResult.resultDT.Rows.Count > 0)
                {
                    gvSyllabusPlanning.Visible = true;
                    NoDataFoundDiv.Visible = false;
                    gvSyllabusPlanning.DataSource = objResult.resultDT;
                    gvSyllabusPlanning.DataBind();
                    PanelVisibility(1);

                }
                else
                {
                    NoDataFoundDiv.Visible = true;
                    tabs.Visible = false;
                    gvSyllabusPlanning.Visible = false;
                    // PanelVisibility(2);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        protected void gvSyllabusPlanning_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                DataTable dt = new DataTable();
                SyllabusPlanningBL objSyllabusPlanningBL = new SyllabusPlanningBL();

                Controls objControls = new Controls();

                if (e.CommandName.ToString() == "Edit1")
                {

                    ddlSchoolName.Enabled = false;
                    ddlSection.Enabled = false;
                    ddlClassName.Enabled = false;
                    ddlDivisionName.Enabled = false;
                    ddlYear.Enabled = false;
                    ddlSubject.Enabled = false;
                    txtChapterNameAndNoENG.Enabled = false;
                    txtChapterNameAndNoGUJ.Enabled = false;
                    txtSyllabusDetailsENG.Enabled = false;
                    txtSyllabusDetailsGUJ.Enabled = false;
                    txtSyllabusRemark.Enabled = false;
                    ddlMonth.Enabled = false;
                    ddlEmployeeList.Enabled = false;
                    txtPlanStartDate.Enabled = false;
                    txtPlanEndDate.Enabled = false;
                    SyllabusPlanningDiv3.Visible = true;
                    SyllabusRecoveredDiv.Visible = true;
                    ddlTeacherList.Enabled = false;


                    ViewState["Mode"] = "Edit";
                    ViewState["SyllabusPlanTID"] = e.CommandArgument.ToString();
                    objResult = objSyllabusPlanningBL.SyllabusPlanning_SelectAll(Convert.ToInt32(ViewState["SyllabusPlanTID"].ToString()));
                    // objResult = objSyllabusBL.Syllabus_Select(Convert.ToInt32(ViewState["SyllabusMID"].ToString()));
                    if (objResult != null)
                    {

                        gvSyllabusPlanning.Visible = false;
                        tabs.Visible = true;

                        DataTable dtResult = objResult.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            GetSchoolName();
                            ddlSchoolName.SelectedValue = dtResult.Rows[0][SyllabusBO.SYLLABUS_SCHOOLMID].ToString();

                            dt = FetchDivision(Convert.ToInt32(dtResult.Rows[0][SyllabusBO.SYLLABUS_CLASSID]), Convert.ToInt32(dtResult.Rows[0][SyllabusBO.SYLLABUS_SCHOOLMID]));
                            if (dt.Rows.Count > 0)
                            {
                                objControls.BindDropDown_ListBox(dt, ddlDivisionName, "DivisionName", "DivisionTID");
                            }
                            ddlDivisionName.SelectedValue = dtResult.Rows[0][SyllabusBO.SYLLABUS_DIVISIONID].ToString();

                            BindYear();
                            ddlYear.SelectedItem.Text = dtResult.Rows[0][SyllabusBO.SYLLABUS_YEAR].ToString();

                            dt = FetchSection(Convert.ToInt32(dtResult.Rows[0][SyllabusBO.SYLLABUS_SCHOOLMID]));
                            if (dt.Rows.Count > 0)
                            {
                                objControls.BindDropDown_ListBox(dt, ddlSection, "SectionName", "SectionMID");
                            }
                            ddlSection.SelectedValue = dtResult.Rows[0][SyllabusBO.SYLLABUS_SECTIONID].ToString();

                            dt = FetchClass(Convert.ToInt32(dtResult.Rows[0][SyllabusBO.SYLLABUS_SECTIONID]), Convert.ToInt32(dtResult.Rows[0][SyllabusBO.SYLLABUS_SCHOOLMID]));
                            if (dt.Rows.Count > 0)
                            {
                                objControls.BindDropDown_ListBox(dt, ddlClassName, "ClassName", "ClassMID");
                            }
                           //*********************************************************************************************
                           
                            //*********************************************************************************************
                            ddlClassName.SelectedValue = dtResult.Rows[0][SyllabusBO.SYLLABUS_CLASSID].ToString();

                            FetchSubject(Convert.ToInt32(dtResult.Rows[0][SyllabusBO.SYLLABUS_SCHOOLMID]));
                            ddlSubject.SelectedValue = dtResult.Rows[0][SyllabusBO.SYLLABUS_SUBJECTID].ToString();

                            txtChapterNameAndNoENG.Text = dtResult.Rows[0][SyllabusBO.SYLLABUS_CHAPTERNAMEANDNOENG].ToString();
                            txtChapterNameAndNoGUJ.Text = dtResult.Rows[0][SyllabusBO.SYLLABUS_CHAPTERNAMEANDNOGUJ].ToString();
                            txtSyllabusDetailsENG.Text = dtResult.Rows[0][SyllabusBO.SYLLABUS_CHAPTERNAMEANDNOGUJ].ToString();
                            txtSyllabusDetailsGUJ.Text = dtResult.Rows[0][SyllabusBO.SYLLABUS_CHAPTERNAMEANDNOENG].ToString();
                            txtSyllabusRemark.Text = dtResult.Rows[0][SyllabusBO.SYLLABUS_SYLLABUSREMARKS].ToString();

                            ddlMonth.SelectedValue = dtResult.Rows[0][SyllabusBO.SYLLABUSPLANNING_MONTHNO].ToString();

                            BindEmployeeList();
                            //28/09/2022 Bhandavi
                            //when we edit a syllabus teachers name is not displaying in header drop down(ddlemployeelist)
                            //Selected value of ddlemployee is teacherid
                            ddlEmployeeList.SelectedValue = dtResult.Rows[0][SyllabusBO.SYLLABUSPLANNING_TEACHERMID].ToString();

                            ddlTeacherList.SelectedValue = dtResult.Rows[0][SyllabusBO.SYLLABUSPLANNING_TEACHERMID].ToString();

                            txtPlanStartDate.Text = dtResult.Rows[0][SyllabusBO.SYLLABUSPLANNING_PLANSTARTDATE].ToString();
                            txtPlanEndDate.Text = dtResult.Rows[0][SyllabusBO.SYLLABUSPLANNING_PLANENDDATE].ToString();
                            //*********************************************************************************************************
                            
                            txtActualStartDate.Text = dtResult.Rows[0][SyllabusBO.SYLLABUSPLANNING_ACTUALSTARTDATE].ToString();
                               txtActualEndDate.Text = dtResult.Rows[0][SyllabusBO.SYLLABUSPLANNING_ACTUALENDDATE].ToString();
                             txtSyllabusRecovered.Text = dtResult.Rows[0][SyllabusBO.SYLLABUSPLANNING_SYLLABUSCOVERED].ToString();
                            //*********************************************************************************************************

                          

                            //PanelVisibility(1);
                        }

                    }

                }
                else if (e.CommandName.ToString() == "Delete1")
                {
                    //objResult = objSyllabusPlanningBL.SyllabusPlanning_Delete(Convert.ToInt32(e.CommandArgument.ToString()));
                    //if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    //{
                    //    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record deleted successfully.');</script>");
                    //    PanelVisibility(1);
                    //    BindEmployeeList();
                    //}
                    //else
                    //{
                    //    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You cannot delete this record because it is in used.');</script>");
                    //}
                }

            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        #region School Selected Change Event
        protected void ddlSchoolName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                SectionBL objSectionBl = new SectionBL();

                objResult = objSectionBl.Section_SelectAll_ForDropDown(Convert.ToInt32(ddlSchoolName.SelectedValue));
                if (objResult != null)
                {

                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlSection, "SectionName", "SectionMID");

                    }
                    ddlSection.Items.Insert(0, new ListItem("--Select--", ""));

                }

                #region Fetch Subject Drop Down List
                FetchSubject(Convert.ToInt32(ddlSchoolName.SelectedValue));
                #endregion

                #region Fetch Academic Month from School
                SchoolBL objSchoolBl = new SchoolBL();
                ApplicationResult objResults = new ApplicationResult();
                int intMonth = 0;

                objResults = objSchoolBl.School_Select(Convert.ToInt32(ddlSchoolName.SelectedValue));

                if (objResults != null)
                {
                    if (objResults.resultDT.Rows.Count > 0)
                    {

                        intMonth = Convert.ToInt32(objResults.resultDT.Rows[0][SchoolBO.SCHOOL_ACADEMICMONTH].ToString());
                    }

                }
                #endregion


            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Fetch School
        public void GetSchoolName()
        {
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            SchoolBL objSchoolBl = new SchoolBL();

            objResult = objSchoolBl.School_SelectAll_ForDropDOwn(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
            if (objResult != null)
            {

                if (objResult.resultDT.Rows.Count > 0)
                {
                    objControls.BindDropDown_ListBox(objResult.resultDT, ddlSchoolName, "SchoolNameEng", "SchoolMID");

                }
                ddlSchoolName.Items.Insert(0, new ListItem("--Select--", ""));
                ddlClassName.Items.Insert(0, new ListItem("--Select--", ""));
                ddlSection.Items.Insert(0, new ListItem("--Select--", ""));
                ddlSubject.Items.Insert(0, new ListItem("--Select--", ""));
                ddlYear.Items.Insert(0, new ListItem("-Select-", "-1"));
                ddlDivisionName.Items.Insert(0, new ListItem("--Select--", ""));
            }
        }
        #endregion

        #region Class Selected Cahnge Event
        protected void ddlClassName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                DivisionTBL objDivisionTbl = new DivisionTBL();

                objResult = objDivisionTbl.Division_SelectAll_ClassWise_ForDropDown(Convert.ToInt32(ddlClassName.SelectedValue), Convert.ToInt32(ddlSchoolName.SelectedValue));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlDivisionName, "DivisionName", "DivisionTID");

                    }
                    ddlDivisionName.Items.Insert(0, new ListItem("--Select--", ""));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Section Seclected Change Event
        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                ClassBL objClassBl = new ClassBL();

                objResult = objClassBl.Class_SelectAll_SectionWise_ForDropDown(Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSchoolName.SelectedValue));
                if (objResult != null)
                {

                    if (objResult.resultDT.Rows.Count > 0)
                    {

                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlClassName, "ClassName", "ClassMID");
                    }
                    ddlClassName.Items.Insert(0, new ListItem("--Select--", ""));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Fetch Division data for DropDown
        private DataTable FetchDivision(int intClassMID, int intSchoolMID)
        {
            //DataTable dtDivision = new DataTable();
            DivisionTBL objDivisionBL = new DivisionTBL();
            DivisionTBO objDivisionBO = new DivisionTBO();
            ApplicationResult objResults = new ApplicationResult();

            objResults = objDivisionBL.Division_SelectAll_ClassWise_ForDropDown(intClassMID, intSchoolMID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {

            }
            return objResults.resultDT;
        }
        #endregion

        #region Fetch Section data For DropDown 
        private DataTable FetchSection(int intSchoolMID)
        {
            //DataTable dtDivision = new DataTable();

            SectionBL objSectionBL = new SectionBL();
            SectionBO objSectionBO = new SectionBO();
            ApplicationResult objResults = new ApplicationResult();

            objResults = objSectionBL.Section_SelectAll_ForDropDown(intSchoolMID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {

            }
            return objResults.resultDT;
        }
        #endregion

        #region Fetch Class Data For Drop Down 
        private DataTable FetchClass(int intSectionMID, int intSchoolMID)
        {
            //DataTable dtDivision = new DataTable();


            ClassBL objClassBL = new ClassBL();
            ApplicationResult objResults = new ApplicationResult();

            objResults = objClassBL.Class_SelectAll_SectionWise_ForDropDown(intSectionMID, intSchoolMID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {

            }
            return objResults.resultDT;
        }
        #endregion

        #region Fetch Subject Data For Drop Down
        private void FetchSubject(int intSchoolMID)
        {
            SubjectMBL objSubjectBL = new SubjectMBL();
            ApplicationResult objResults = new ApplicationResult();
            Controls objControls = new Controls();

            objResults = objSubjectBL.SubjectM_Select_By_School(intSchoolMID);

            if (objResults != null)
            {
                if (objResults.resultDT.Rows.Count > 0)
                {
                    objControls.BindDropDown_ListBox(objResults.resultDT, ddlSubject, "NameEng", "SubjectMID");
                }
                ddlSubject.Items.Insert(0, new ListItem("--Select--", ""));
            }
        }
        #endregion

        //*********************************************************************************************
        

        //*********************************************************************************************

        protected void gvSyllabusPlanning_PreRender(object sender, EventArgs e)
        {

        }



        #region Panel Visibility Mode
        public void PanelVisibility(int intcode)
        {
            if (intcode == 1)
            {
                divGrid.Visible = true;
                tabs.Visible = false;

            }
            else if (intcode == 2)
            {
                divGrid.Visible = false;
                tabs.Visible = true;

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

        protected void btnSaveClass_Click(object sender, EventArgs e)
        {
            try
            {

                SyllabusPlanningBO objSyllabusPlanningBO = new SyllabusPlanningBO();
                SyllabusPlanningBL objSyllabusPlanningBL = new SyllabusPlanningBL();



                ApplicationResult objResult = new ApplicationResult();
                DataTable dtResult = new DataTable();
                int intSyllabusPlanTID = 0;


                //Code For Validate Department Name
                if (ViewState["Mode"].ToString() == "Edit")
                {
                    intSyllabusPlanTID = Convert.ToInt32(ViewState["SyllabusPlanTID"].ToString());
                }




                if (ViewState["Mode"].ToString() == "Edit")
                {
                    if (txtActualStartDate.Text != "" && txtActualEndDate.Text != "")
                    {
                        objSyllabusPlanningBO.SyllabusPlanTID = intSyllabusPlanTID;
                        objSyllabusPlanningBO.SyllabusMID = Convert.ToInt32(ViewState["SyllabusPlanTID"].ToString());
                        objSyllabusPlanningBO.ActualStartDate = Convert.ToDateTime(txtActualStartDate.Text);
                        objSyllabusPlanningBO.ActualEndDate = Convert.ToDateTime(txtActualEndDate.Text);
                        objSyllabusPlanningBO.SyllabusCovered = Convert.ToString(txtSyllabusRecovered.Text);
                        objSyllabusPlanningBO.ModifiedByID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                        objSyllabusPlanningBO.ModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();

                        objResult = objSyllabusPlanningBL.SyllabusPlanning_Update(objSyllabusPlanningBO);
                        if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record updated successfully.');</script>");
                            ClearAll();
                            //     BindSyllabusMaster();
                            ddlEmployeeList.Enabled = true;
                            BindEmployeeList();
                            PanelVisibility(1);

                        }
                    }
                    else
                    {

                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Please Enter Actual Dates');</script>");

                    }

                }

             
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
    }
}