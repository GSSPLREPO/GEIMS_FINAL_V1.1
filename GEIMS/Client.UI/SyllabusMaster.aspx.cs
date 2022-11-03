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
    public partial class SyllabusMaster : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(SyllabusMaster));

        #region Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    GetSchoolName();
                    BindSyllabusMaster();
                   BindYear();
                    BindEmployeeList();
                    ViewState["Mode"] = "Save";


                }
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Data Saved Successfully.');</script>");
            }
        }
        #endregion

        #region School Selected Change Event
        protected void ddlSchoolName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                SectionBL objSectionBl = new SectionBL();

                if (ddlSchoolName.SelectedIndex != 0)
                {
                    objResult = objSectionBl.Section_SelectAll_ForDropDown(Convert.ToInt32(ddlSchoolName.SelectedValue));
                    if (objResult != null)
                    {
                        ddlSection.Enabled = true;
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            objControls.BindDropDown_ListBox(objResult.resultDT, ddlSection, "SectionName", "SectionMID");

                        }
                        ddlSection.Items.Insert(0, new ListItem("--Select--", ""));

                    }
                }
                else
                {
                    ddlSection.Enabled = false;
                }

                #region Fetch Subject Drop Down List
                if (ddlSchoolName.SelectedIndex != 0)
                {
                    FetchSubject(Convert.ToInt32(ddlSchoolName.SelectedValue));
                }
                #endregion

                #region Fetch Academic Month from School
                SchoolBL objSchoolBl = new SchoolBL();
                ApplicationResult objResults = new ApplicationResult();
                int intMonth = 0;
                if (ddlSchoolName.SelectedIndex != 0)
                {


                    objResults = objSchoolBl.School_Select(Convert.ToInt32(ddlSchoolName.SelectedValue));

                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {

                            intMonth = Convert.ToInt32(objResults.resultDT.Rows[0][SchoolBO.SCHOOL_ACADEMICMONTH].ToString());
                        }

                    }
                }
                #endregion

                //27/09/2022 Bhandavi
                //Need to clear class and division drop down when we change school drop down value(coming previous values only)
                ddlClassName.Items.Clear();
                ddlClassName.Items.Insert(0, new ListItem("--Select--", ""));
                ddlDivisionName.Items.Clear();
                ddlDivisionName.Items.Insert(0, new ListItem("--Select--", ""));
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region BindYear
        public void BindYear()
        {
            try {

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
                ddlYear.Items.Insert(0, new ListItem("-Select-", ""));

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

        #region Section Seclected Change Event
        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                ClassBL objClassBl = new ClassBL();
                if (ddlSection.SelectedIndex!= 0)
                {
                    objResult = objClassBl.Class_SelectAll_SectionWise_ForDropDown(Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSchoolName.SelectedValue));
                    if (objResult != null)
                    {
                        ddlClassName.Enabled = true;

                        if (objResult.resultDT.Rows.Count > 0)
                        {

                            objControls.BindDropDown_ListBox(objResult.resultDT, ddlClassName, "ClassName", "ClassMID");
                        }
                        //Added on 27/09/2022 Bhandavi
                        //when a Section does not having classes, then showing previous classes along with select.
                        //If Section doesnot have classes then clear class dropdown
                        else
                        {
                            ddlClassName.Items.Clear();
                        }
                        ddlClassName.Items.Insert(0, new ListItem("--Select--", ""));
                    }
                }
                else
                {
                    ddlClassName.Enabled = false;
                }

                //27/09/2022 Bhandavi
                //Need to clear division drop down when we change school drop down value(coming previuos values only)
        
                ddlDivisionName.Items.Clear();
                ddlDivisionName.Items.Insert(0, new ListItem("--Select--", ""));
            }
            catch (Exception)
            {

                throw;
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
                if (ddlClassName.SelectedIndex != 0)
                {
                    objResult = objDivisionTbl.Division_SelectAll_ClassWise_ForDropDown(Convert.ToInt32(ddlClassName.SelectedValue), Convert.ToInt32(ddlSchoolName.SelectedValue));
                    if (objResult != null)
                    {

                        ddlDivisionName.Enabled = true;
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            objControls.BindDropDown_ListBox(objResult.resultDT, ddlDivisionName, "DivisionName", "DivisionTID");

                        }
                        ddlDivisionName.Items.Insert(0, new ListItem("--Select--", ""));
                    }
                }
                else
                {
                    ddlDivisionName.Enabled = false;
                        }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Fetch School
        /// <summary>
        /// bind school names
        /// </summary>
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
                ddlYear.Items.Insert(0, new ListItem("-Select-", "-1"));
                ddlSubject.Items.Insert(0, new ListItem("--Select--", "-1"));
                ddlDivisionName.Items.Insert(0, new ListItem("--Select--", ""));
            }
        }

        #endregion

        #region Bind Syllabus Master
        /// <summary>
        /// Commented on 01/11/2022 Bhandavi
        /// To get all syllabus from Tbl_Syllabus_M With isDeleted flag 0
        /// and bind to GridView gvSyllabus
        /// </summary>
        public void BindSyllabusMaster()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                SyllabusBL objSyllabusbl = new SyllabusBL();

                objResult = objSyllabusbl.Syllabus_SelectAll();
                if (objResult.resultDT.Rows.Count > 0)
                {
                    gvSyllabus.DataSource = objResult.resultDT;
                    gvSyllabus.DataBind();
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

        #region Clear All Method
        public void ClearAll()
        {
            Controls objControl = new Controls();
            objControl.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ddlSection.Items.Clear();
            ddlClassName.Items.Clear();
            ddlDivisionName.Items.Clear();
            ddlSubject.Items.Clear();
            ddlYear.SelectedIndex = 0;
            ViewState["Mode"] = "Save";
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

        #region Add New Button Click Event
        protected void lnkAddNewClass_OnClick(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                PanelVisibility(2);
                ddlSubject.Enabled = true;
                ddlSchoolName.Enabled = true;
                ddlSection.Enabled = true;
                ddlClassName.Enabled = true;
                ddlDivisionName.Enabled = true;
                ddlYear.Enabled = true;             

            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
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
            else if (intcode == 2)
            {
                divGrid.Visible = false;
                tabs.Visible = true;
                lnkViewList.Visible = true;
              
            }
        }
        #endregion

        #region gvSyllabus_RowCommand
        
        protected void gvSyllabus_RowCommand(object sender, GridViewCommandEventArgs e)
         {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                ApplicationResult objResult1 = new ApplicationResult();
                DataTable dt = new DataTable();
                SyllabusBL objSyllabusBL = new SyllabusBL();

                Controls objControls = new Controls();
              
                if (e.CommandName.ToString() == "Edit1")
                {
                    ddlSchoolName.Enabled = true;
                    ddlSection.Enabled = true;
                    ddlClassName.Enabled = true;
                    ddlDivisionName.Enabled = true;
                    ddlYear.Enabled = true;
                    ddlSubject.Enabled = true;
                    
                   // ddlSubject.Enabled = false;
                    txtChapterNameAndNoENG.Enabled = true;
                    txtChapterNameAndNoGUJ.Enabled = true;
                    txtSyllabusDetailsENG.Enabled = true;
                    txtSyllabusDetailsGUJ.Enabled = true;
                    txtSyllabusRemark.Enabled = true;
                    SyllabusPlanningDiv1.Visible = false;
                    SyllabusPlanningDiv2.Visible = false;

                    ViewState["Mode"] = "Edit";
                    ViewState["SyllabusMID"] = e.CommandArgument.ToString();

                    //31/11/2022 Bhandavi
                    //check if planning is there if  yes then disable school,section,classname,division name,year and subject drop downlists
                    objResult1 = objSyllabusBL.SyyllabusProgress_Count(Convert.ToInt32(ViewState["SyllabusMID"].ToString()));
                    if (objResult1 != null)
                    {
                        DataTable dtResult = objResult1.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            ddlSchoolName.Enabled = false;
                            ddlSection.Enabled = false;
                            ddlClassName.Enabled = false;
                            ddlDivisionName.Enabled = false;
                            ddlYear.Enabled = false;
                            ddlSubject.Enabled = false;
                        }
                    }



                    objResult = objSyllabusBL.Syllabus_Select(Convert.ToInt32(ViewState["SyllabusMID"].ToString()));
                    if (objResult != null)
                    {
                        DataTable dtResult = objResult.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            ddlSchoolName.SelectedValue = dtResult.Rows[0][SyllabusBO.SYLLABUS_SCHOOLMID].ToString();

                            dt = FetchDivision(Convert.ToInt32(dtResult.Rows[0][SyllabusBO.SYLLABUS_CLASSID]), Convert.ToInt32(dtResult.Rows[0][SyllabusBO.SYLLABUS_SCHOOLMID]));
                            if (dt.Rows.Count > 0)
                            {
                                objControls.BindDropDown_ListBox(dt, ddlDivisionName, "DivisionName", "DivisionTID");
                            }
                            ddlDivisionName.SelectedValue = dtResult.Rows[0][SyllabusBO.SYLLABUS_DIVISIONID].ToString();
                            
                            //Changed on 27/09/2022 by Bhandavi
                            //Coming this field is required when updating a record
                            //value from database is showing in dropdown by replacing --select-- with year value
                            //ddlYear.SelectedItem.Text = dtResult.Rows[0][SyllabusBO.SYLLABUS_YEAR].ToString();
                            ddlYear.SelectedValue = dtResult.Rows[0][SyllabusBO.SYLLABUS_YEAR].ToString();

                            dt = FetchSection(Convert.ToInt32(dtResult.Rows[0][SyllabusBO.SYLLABUS_SCHOOLMID]));
                            if (dt.Rows.Count > 0)
                            {
                                objControls.BindDropDown_ListBox(dt, ddlSection, "SectionName", "SectionMID");
                            }
                            ddlSection.SelectedValue = dtResult.Rows[0][SyllabusBO.SYLLABUS_SECTIONID].ToString();

                           
                            FetchSubject(Convert.ToInt32(dtResult.Rows[0][SyllabusBO.SYLLABUS_SCHOOLMID]));
                            ddlSubject.SelectedValue = dtResult.Rows[0][SyllabusBO.SYLLABUS_SUBJECTID].ToString();
                            
                            dt = FetchClass(Convert.ToInt32(dtResult.Rows[0][SyllabusBO.SYLLABUS_SECTIONID]), Convert.ToInt32(dtResult.Rows[0][SyllabusBO.SYLLABUS_SCHOOLMID]));
                            if (dt.Rows.Count > 0)
                            {
                                objControls.BindDropDown_ListBox(dt, ddlClassName, "ClassName", "ClassMID");
                            }
                            ddlClassName.SelectedValue = dtResult.Rows[0][SyllabusBO.SYLLABUS_CLASSID].ToString();


                            txtChapterNameAndNoENG.Text = dtResult.Rows[0][SyllabusBO.SYLLABUS_CHAPTERNAMEANDNOENG].ToString();
                            txtChapterNameAndNoGUJ.Text = dtResult.Rows[0][SyllabusBO.SYLLABUS_CHAPTERNAMEANDNOGUJ].ToString();
                            txtSyllabusDetailsENG.Text = dtResult.Rows[0][SyllabusBO.SYLLABUS_CHAPTERNAMEANDNOGUJ].ToString();
                            txtSyllabusDetailsGUJ.Text = dtResult.Rows[0][SyllabusBO.SYLLABUS_CHAPTERNAMEANDNOENG].ToString();
                            txtSyllabusRemark.Text = dtResult.Rows[0][SyllabusBO.SYLLABUS_SYLLABUSREMARKS].ToString();

                            PanelVisibility(2);
                        }
                    }
                    //27/09/2022 Bhandavi
                    //In edit mode select is not added for class, section and division drop down lists. So when we select
                    //first value of class section is disabled(code written like if selected index =0 then disable
                    //and when we select first value of section then division is disabled.
                    //so added select to all 3 drop down lists
                    ddlClassName.Items.Insert(0, new ListItem("--Select--", ""));
                    ddlSection.Items.Insert(0, new ListItem("--Select--", ""));
                    ddlDivisionName.Items.Insert(0, new ListItem("--Select--", ""));
                }
                else if (e.CommandName.ToString() == "Delete1")
                {
                    //31/10/2022 Bhandavi
                    //check if planning is there if  yes then unable to delete a syllabus
                    objResult1 = objSyllabusBL.SyyllabusProgress_Count(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (objResult1 != null)
                    {
                        DataTable dtResult = objResult1.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You cannot delete this record because Planning has been created.');</script>");
                        }
                        else
                        {
                            objResult = objSyllabusBL.Syllabus_Delete(Convert.ToInt32(e.CommandArgument.ToString()));
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record deleted successfully.');</script>");
                                PanelVisibility(1);
                                BindSyllabusMaster();
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You cannot delete this record because it is in use.');</script>");
                            }
                        }
                    }

                   
                }
                else if (e.CommandName.ToString() == "Planning1")
                {
                    ViewState["Mode"] = "Planning1";
                    ddlSchoolName.Enabled = false;
                    ddlSection.Enabled = false;
                    ddlClassName.Enabled = false;
                    ddlDivisionName.Enabled = false;
                    ddlYear.Enabled = false;
                    ddlSubject.Enabled = false;

                    /*txtChapterNameAndNoENG.Attributes.Add("readonly", "readonly");
                    txtChapterNameAndNoGUJ.Attributes.Add("readonly", "readonly");
                    txtSyllabusDetailsENG.Attributes.Add("readonly", "readonly");
                    txtSyllabusDetailsGUJ.Attributes.Add("readonly", "readonly");
                    txtSyllabusRemark.Attributes.Add("readonly", "readonly");*/
                    //txtChapterNameAndNoENG.Enabled = false;
                    //txtChapterNameAndNoGUJ.Enabled = false;
                    //txtSyllabusDetailsENG.Enabled = false;
                    //txtSyllabusDetailsGUJ.Enabled = false;
                    //txtSyllabusRemark.Enabled = false;

                    SyllabusPlanningDiv1.Visible = true;
                    SyllabusPlanningDiv2.Visible = true;

                    ViewState["SyllabusMID"] = e.CommandArgument.ToString();
                    objResult = objSyllabusBL.Syllabus_Select(Convert.ToInt32(ViewState["SyllabusMID"].ToString()));
                    if (objResult != null)
                    {
                        DataTable dtResult = objResult.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            ddlSchoolName.SelectedValue = dtResult.Rows[0][SyllabusBO.SYLLABUS_SCHOOLMID].ToString();

                            dt = FetchDivision(Convert.ToInt32(dtResult.Rows[0][SyllabusBO.SYLLABUS_CLASSID]), Convert.ToInt32(dtResult.Rows[0][SyllabusBO.SYLLABUS_SCHOOLMID]));
                            if (dt.Rows.Count > 0)
                            {
                                objControls.BindDropDown_ListBox(dt, ddlDivisionName, "DivisionName", "DivisionTID");
                            }
                            ddlDivisionName.SelectedValue = dtResult.Rows[0][SyllabusBO.SYLLABUS_DIVISIONID].ToString();
                           
                            //ddlYear.SelectedItem.Text = dtResult.Rows[0][SyllabusBO.SYLLABUS_YEAR].ToString();
                            ddlYear.SelectedValue = dtResult.Rows[0][SyllabusBO.SYLLABUS_YEAR].ToString();

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
                            ddlClassName.SelectedValue = dtResult.Rows[0][SyllabusBO.SYLLABUS_CLASSID].ToString();


                            FetchSubject(Convert.ToInt32(dtResult.Rows[0][SyllabusBO.SYLLABUS_SCHOOLMID]));
                            ddlSubject.SelectedValue= dtResult.Rows[0][SyllabusBO.SYLLABUS_SUBJECTID].ToString();

                            txtChapterNameAndNoENG.Text = dtResult.Rows[0][SyllabusBO.SYLLABUS_CHAPTERNAMEANDNOENG].ToString();
                            txtChapterNameAndNoGUJ.Text = dtResult.Rows[0][SyllabusBO.SYLLABUS_CHAPTERNAMEANDNOGUJ].ToString();
                            txtSyllabusDetailsENG.Text = dtResult.Rows[0][SyllabusBO.SYLLABUS_CHAPTERNAMEANDNOGUJ].ToString();
                            txtSyllabusDetailsGUJ.Text = dtResult.Rows[0][SyllabusBO.SYLLABUS_CHAPTERNAMEANDNOENG].ToString();
                            txtSyllabusRemark.Text = dtResult.Rows[0][SyllabusBO.SYLLABUS_SYLLABUSREMARKS].ToString();

                            PanelVisibility(2);
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
                }
                ddlEmployeeList.Items.Insert(0, new ListItem("-Select-", ""));
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Fetch Class Data For Drop Down 
        private DataTable FetchClass(int intSectionMID,int intSchoolMID)
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
        /// <summary>
        /// Bind subjects to dropdown
        /// </summary>
        /// <param name="intSchoolMID"></param>
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
                //Added on 27/09/2022 Bhandavi
                //when a school is not having subjects, then showing previos subjects along with select. So if 
                //School doesnot have subjects then clear subject dropdown
                else
                { 
                     ddlSubject.Items.Clear();
                }
                ddlSubject.Items.Insert(0, new ListItem("--Select--", ""));
                
            }

       
        }
        #endregion

        protected void gvSyllabus_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvSyllabus.Rows.Count > 0)
                {
                    gvSyllabus.UseAccessibleHeader = true;
                    gvSyllabus.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        #region Save Button Click Event
        protected void btnSaveClass_Click(object sender, EventArgs e)
        {
            try
            {
                if(ddlSchoolName.SelectedIndex != 0  && ddlSubject.SelectedIndex != 0  )
                {
                    SyllabusBO objSyllabusBO = new SyllabusBO();
                    SyllabusBL objSyllabusBL = new SyllabusBL();

                    SyllabusPlanningBO objSyllabusPlanningBO = new SyllabusPlanningBO();
                    SyllabusPlanningBL objSyllabusPlanningBL = new SyllabusPlanningBL();

                    ApplicationResult objResult = new ApplicationResult();
                    DataTable dtResult = new DataTable();
                    int intDepartmentID = 0;

                    objSyllabusBO.SchoolMID = Convert.ToInt32(ddlSchoolName.SelectedValue);
                    objSyllabusBO.SectionID = Convert.ToInt32(ddlSection.SelectedValue);
                    objSyllabusBO.ClassID = Convert.ToInt32(ddlClassName.SelectedValue);
                    objSyllabusBO.DivisionID = Convert.ToInt32(ddlDivisionName.SelectedValue);
                    objSyllabusBO.Year = Convert.ToString(ddlYear.SelectedItem);
                    objSyllabusBO.ChapterNameAndNoENG = txtChapterNameAndNoENG.Text.Trim();
                    objSyllabusBO.ChapterNameAndNoGUJ = txtChapterNameAndNoGUJ.Text.Trim();
                    objSyllabusBO.SyllabusDetailsENG = txtSyllabusDetailsENG.Text.Trim();
                    objSyllabusBO.SyllabusDetailsGUJ = txtSyllabusDetailsGUJ.Text.Trim();
                    objSyllabusBO.SyllabusRemarks = txtSyllabusRemark.Text.Trim();
                    objSyllabusBO.SubjectID = Convert.ToInt32(ddlSubject.SelectedValue);


                    //Code For Validate Department Name
                    if (ViewState["Mode"].ToString() == "Save")
                    {
                        intDepartmentID = -1;
                    }
                    else if (ViewState["Mode"].ToString() == "Edit")
                    {
                        intDepartmentID = Convert.ToInt32(ViewState["SyllabusMID"].ToString());
                    }
                    else if (ViewState["Mode"].ToString() == "Planning1")
                    {
                        intDepartmentID = Convert.ToInt32(ViewState["SyllabusMID"].ToString());
                    }


                    if (ViewState["Mode"].ToString() == "Save")
                    {
                        objSyllabusBO.CreatedByID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                        objSyllabusBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        objResult = objSyllabusBL.Syllabus_Insert(objSyllabusBO);
                        if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record saved successfully.');</script>");
                        }
                    }
                    else if (ViewState["Mode"].ToString() == "Edit")
                    {
                        objSyllabusBO.SyllabusMID = Convert.ToInt32(ViewState["SyllabusMID"].ToString());
                        objResult = objSyllabusBL.Syllabus_Update(objSyllabusBO);
                        if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record updated successfully.');</script>");
                        }
                      
                    }
                    else if (ViewState["Mode"].ToString() == "Planning1")
                    {
                        //31/10/2022 Bhandavi
                        //Changed code to get validation messages when clicking on save button in planning without selecting mandatory fields
                        if (ddlMonth.SelectedIndex <=0 || ddlEmployeeList.SelectedIndex <=0 || txtPlanStartDate.Text == "" || txtPlanEndDate.Text == "")
                        {
                            
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Please Select All Mandatory fields.');</script>");
                            goto msg;
                        }
                        else
                        {
                            objSyllabusPlanningBO.SyllabusMID = intDepartmentID;
                            objSyllabusPlanningBO.CreatedByID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                            objSyllabusPlanningBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                            objSyllabusPlanningBO.MonthNo = Convert.ToInt32(ddlMonth.SelectedValue);
                            objSyllabusPlanningBO.TeacherMID = Convert.ToInt32(ddlEmployeeList.SelectedValue);
                            objSyllabusPlanningBO.PlannedStartDate = Convert.ToDateTime(txtPlanStartDate.Text);
                            objSyllabusPlanningBO.PlannedEndDate = Convert.ToDateTime(txtPlanEndDate.Text);
                            objResult = objSyllabusPlanningBL.SyllabusPlanning_Insert(objSyllabusPlanningBO);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record saved successfully.');</script>");
                            } 
                        }
                    }
                    ClearAll();
                    BindSyllabusMaster();
                    PanelVisibility(1);
                }    
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Please Select All DropDown Lists.');</script>");
                }
            msg:;
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }


        #endregion

        protected void ddlDivisionName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDivisionName.SelectedIndex == 0)
            {
                ddlSubject.Enabled = false;
            }
            else
            {
                ddlSubject.Enabled = true;
            }
        }

        protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
       
    }
}