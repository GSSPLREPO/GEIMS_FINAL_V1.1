using AjaxControlToolkit;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.Common;
using log4net;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace GEIMS.Client.UI
{
    public partial class StudentAttendanceConsolidated : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(StudentAttendanceConsolidated));
       

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack)
                    return;
                if (Session["UserName"] == null)
                    Response.Redirect("../UserLogin.aspx");
                BindAcademicYear();
                BindClass();
            }
            catch (Exception ex)
            {
                StudentAttendanceConsolidated.logger.Error((object)"Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }


        public void BindAcademicYear()
        {
            try
            {
                SchoolBL schoolBl = new SchoolBL();
                ApplicationResult applicationResult1 = new ApplicationResult();
                int num1 = 0;
                int int32_1 = Convert.ToInt32(this.Session["SchoolID"]);
                ApplicationResult applicationResult2 = schoolBl.School_Select(int32_1);
                if (applicationResult2 != null && applicationResult2.resultDT.Rows.Count > 0)
                    num1 = Convert.ToInt32(applicationResult2.resultDT.Rows[0]["AcademicMonth"].ToString());
                Controls controls = new Controls();
                DateTime now = DateTime.Now;
                int month = now.Month;
                now = DateTime.Now;
                int num2 = now.Year % 100;
                string empty = string.Empty;
                int num3 = num1;
                string str1;
                int num4;
                if (month >= num3)
                {
                    str1 = (num2.ToString() + (num2 + 1).ToString()).ToString();
                }
                else
                {
                    num4 = num2 - 1;
                    str1 = (num4.ToString() + num2.ToString()).ToString();
                }
                int int32_2 = Convert.ToInt32(str1.Substring(0, 2));
                int num5 = Convert.ToInt32(str1.Substring(2, 2));
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("AcademicYear", typeof(string)));
                for (int index = 0; index < 5; ++index)
                {
                    DataRow row = dt.NewRow();
                    if (index == 0)
                    {
                        row["AcademicYear"] = (object)Convert.ToString(int32_2.ToString() + "-" + num5.ToString());
                        dt.Rows.Add(row);
                    }
                    else
                    {
                        num4 = int32_2 - 1;
                        if (num4.ToString().Length < 2)
                        {
                            if (int32_2.ToString().Length == 2)
                            {
                                DataRow dataRow = row;
                                num4 = int32_2 - 1;
                                string str2 = Convert.ToString("0" + num4.ToString() + "-" + int32_2.ToString());
                                dataRow["AcademicYear"] = (object)str2;
                            }
                            else
                            {
                                DataRow dataRow = row;
                                num4 = int32_2 - 1;
                                string str2 = Convert.ToString("0" + num4.ToString() + "-0" + int32_2.ToString());
                                dataRow["AcademicYear"] = (object)str2;
                            }
                            dt.Rows.Add(row);
                        }
                        else
                        {
                            DataRow dataRow = row;
                            num4 = int32_2 - 1;
                            string str2 = Convert.ToString(num4.ToString() + "-" + int32_2.ToString());
                            dataRow["AcademicYear"] = (object)str2;
                            dt.Rows.Add(row);
                        }
                        --int32_2;
                        num5 = int32_2;
                    }
                }
                controls.BindDropDown_ListBox(dt, (Control)this.ddlAcademicYear, "AcademicYear", "AcademicYear");
            }
            catch (Exception ex)
            {
                StudentAttendanceConsolidated.logger.Error((object)"Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        /// <summary>
        /// to bind classes in selected school
        /// </summary>
        public void BindClass()
        {
            try
            {
                ApplicationResult applicationResult1 = new ApplicationResult();
                Controls controls = new Controls();
                ApplicationResult applicationResult2 = new ClassBL().Class_SelectAll_SchoolWise_ForDropDown(Convert.ToInt32(this.Session["SchoolID"]));
                if (applicationResult2 == null)
                    return;
                if (applicationResult2.resultDT.Rows.Count > 0)
                {
                    controls.BindDropDown_ListBox(applicationResult2.resultDT, (Control)this.ddlClass, "ClassName", "ClassMID");
                    controls.BindDropDown_ListBox(applicationResult2.resultDT, (Control)this.ddlClass1, "ClassName", "ClassMID");
                }
                ddlClass.Items.Insert(0, new ListItem("--Select--", ""));
                ddlClass1.Items.Insert(0, new ListItem("--Select--", ""));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult applicationResult1 = new ApplicationResult();
                StudentAttendanceConsolidatedMBO objStudAttendanceConsolidatedBO = new StudentAttendanceConsolidatedMBO();
                StudentAttendanceConsolidatedMBL attendanceConsolidatedMbl = new StudentAttendanceConsolidatedMBL();
                if (!(ViewState["Mode"].ToString() == "Save") && this.ViewState["Mode"].ToString() == "Edit")
                    Convert.ToInt32(this.ViewState["StudentAttendanceConsolidatedMID"].ToString());
                int int32_1 = Convert.ToInt32(this.txtPresentStudentCount.Text);
                int int32_2 = Convert.ToInt32(this.txtAbsentStudentCount.Text);
                if (ViewState["Mode"].ToString() == "Save")
                {
                    if (Convert.ToInt32(this.txtTotalStudentCount.Text) > 0)
                    {
                        if (Convert.ToInt32(int32_1 + int32_2) != Convert.ToInt32(this.txtTotalStudentCount.Text))
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Kindly check the calculation.');</script>");
                            PanelVisibility(1);
                        }
                        else
                        {
                            objStudAttendanceConsolidatedBO.TrustMID = Convert.ToInt32(this.Session["TrustID"]);
                            objStudAttendanceConsolidatedBO.SchoolMID = Convert.ToInt32(this.Session["SchoolID"]);
                            objStudAttendanceConsolidatedBO.EmployeeMID = Convert.ToInt32(this.Session["UserID"]);
                            objStudAttendanceConsolidatedBO.ClassMID = Convert.ToInt32(this.ddlClass.SelectedValue);
                            objStudAttendanceConsolidatedBO.ClassMID = Convert.ToInt32(this.ddlClass.SelectedValue);
                            objStudAttendanceConsolidatedBO.DivisionTID = Convert.ToInt32(this.ddlDivision.SelectedValue);
                            objStudAttendanceConsolidatedBO.AcademicYear = Convert.ToString((object)this.ddlAcademicYear.SelectedItem);
                            objStudAttendanceConsolidatedBO.AttendanceTakenDate = Convert.ToDateTime(this.txtdate.Text);
                            objStudAttendanceConsolidatedBO.TotalStudentCount = Convert.ToString(this.txtTotalStudentCount.Text);
                            objStudAttendanceConsolidatedBO.PresentStudentCount =Convert.ToString(this.txtPresentStudentCount.Text);
                            objStudAttendanceConsolidatedBO.AbsentStudentCount = Convert.ToString(this.txtAbsentStudentCount.Text);
                            objStudAttendanceConsolidatedBO.CreatedBy = Convert.ToInt32(this.Session["UserID"]);
                            objStudAttendanceConsolidatedBO.CreatedDate = DateTime.UtcNow.AddHours(5.5);
                            ApplicationResult applicationResult2 = attendanceConsolidatedMbl.Student_AttendacneConsolidated_Insert(objStudAttendanceConsolidatedBO);
                            if (applicationResult2.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClearSection();
                                PanelVisibility(1);
                             
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Student Attendence Done Successfully.');</script>");
                            }
                            else if (applicationResult2.status == ApplicationResult.CommonStatusType.RECORD_EXISTS)
                            {
                                ClearSection();
                                PanelVisibility(1);
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Student Attendence Record Allready Exists.');</script>");
                            }
                        }
                    }
                    else
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is no Student Count for this selection.');</script>");
                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    if (Convert.ToInt32(int32_1 + int32_2) != Convert.ToInt32(this.txtTotalStudentCount.Text))
                    {
                        PanelVisibility(1);
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Kindly check the calculation.');</script>");
                    }
                    else
                    {
                        objStudAttendanceConsolidatedBO.StudentAttendanceConsolidatedMID = Convert.ToInt32(this.ViewState["StudentAttendanceConsolidatedMID"].ToString());
                        objStudAttendanceConsolidatedBO.PresentStudentCount = Convert.ToString(this.txtPresentStudentCount.Text);
                        objStudAttendanceConsolidatedBO.AbsentStudentCount = Convert.ToString(this.txtAbsentStudentCount.Text);
                        objStudAttendanceConsolidatedBO.ModifiedBy = Convert.ToInt32(this.Session["UserID"]);
                        objStudAttendanceConsolidatedBO.ModifiedDate = DateTime.UtcNow.AddHours(5.5);
                        if (attendanceConsolidatedMbl.StudentAttendanceConsolidate_Update(objStudAttendanceConsolidatedBO).status == ApplicationResult.CommonStatusType.SUCCESS)
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record updated successfully.');</script>");
                    }
                }
                ClearAll();
                divGrid.Visible = true;
                gvStudentAttendance.Visible = false;
                tabs.Visible = false;
                lnkViewList.Visible = true;
            }
            catch (Exception ex)
            {
                StudentAttendanceConsolidated.logger.Error((object)"Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult applicationResult1 = new ApplicationResult();
                Controls controls = new Controls();
                ApplicationResult applicationResult2 = new DivisionTBL().DivisionT_Select_DivisionName_By_Class(Convert.ToInt32(this.ddlClass.SelectedValue));
                if (applicationResult2 == null)
                    return;
                if (applicationResult2.resultDT.Rows.Count > 0)
                    controls.BindDropDown_ListBox(applicationResult2.resultDT, (Control)this.ddlDivision, "DivisionName", "DivisionTID");
                ddlDivision.Items.Insert(0, new ListItem("--Select--", ""));
            }
            catch (Exception ex)
            {
                StudentAttendanceConsolidated.logger.Error((object)"Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        public void BindDivisionByClassMID(int intClassMID)
        {
            try
            {
                ApplicationResult applicationResult1 = new ApplicationResult();
                Controls controls = new Controls();
                ApplicationResult applicationResult2 = new DivisionTBL().DivisionT_Select_DivisionName_By_Class(Convert.ToInt32(intClassMID));
                if (applicationResult2 == null)
                    return;
                if (applicationResult2.resultDT.Rows.Count > 0)
                    controls.BindDropDown_ListBox(applicationResult2.resultDT, (Control)this.ddlDivision, "DivisionName", "DivisionTID");
                ddlDivision.Items.Insert(0, new ListItem("--Select--", ""));
            }
            catch (Exception ex)
            {
                StudentAttendanceConsolidated.logger.Error((object)"Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult applicationResult = new ApplicationResult();
                ApplicationResult attendanceConsolidated = new StudentBL().GetTotalStudentCount_For_AttendanceConsolidated(Convert.ToInt32(this.Session["SchoolID"]), Convert.ToInt32(this.ddlClass.SelectedValue), Convert.ToInt32(this.ddlDivision.SelectedValue), Convert.ToString((object)ddlAcademicYear.SelectedItem));
                if (attendanceConsolidated == null || attendanceConsolidated.resultDT.Rows.Count <= 0)
                    return;
                txtTotalStudentCount.Text = Convert.ToString(attendanceConsolidated.resultDT.Rows[0]["TotalStudentCount"]);
            }
            catch (Exception ex)
            {
                StudentAttendanceConsolidated.logger.Error((object)"Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }


        public void ClearSection()
        {
            txtdate.Text = "";
            txtTotalStudentCount.Text = "";
            txtPresentStudentCount.Text = "";
            txtAbsentStudentCount.Text = "";
            ddlClass.Text = "";
            ddlDivision.Text = "";
        }


        protected void lnkAddNewClass_Click(object sender, EventArgs e)
        {
            try
            {
                ddlClass.Enabled = true;
                ddlDivision.Enabled = true;
                ddlAcademicYear.Enabled = true;
                txtdate.Enabled = true;
                NoDataFound.Visible = false;
                ClearAll();
                PanelVisibility(2);
            }
            catch (Exception ex)
            {
                StudentAttendanceConsolidated.logger.Error((object)"Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }


        protected void lnkViewList_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                PanelVisibility(1);

                //Added on 28/10/2022 Bhandavi
                //When we click on View list button hide grid view
                gvStudentAttendance.Visible = false;
            }
            catch (Exception ex)
            {
                StudentAttendanceConsolidated.logger.Error((object)"Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }


        /// <summary>
        /// On edit/delete of student attendance grid(To edit and delte consolidated details)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvStudentAttendance_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult applicationResult1 = new ApplicationResult();
                StudentAttendanceConsolidatedMBL attendanceConsolidatedMbl = new StudentAttendanceConsolidatedMBL();
                if (e.CommandName.ToString() == "Edit1")
                {
                    ddlClass.Enabled = false;
                    ddlDivision.Enabled = false;
                    ddlAcademicYear.Enabled = false;
                    CalendarExtender1.Enabled = false;
                    txtdate.ReadOnly = true;
                    ViewState["Mode"] = (object)"Edit";
                    ViewState["StudentAttendanceConsolidatedMID"] = (object)e.CommandArgument.ToString();
                    ApplicationResult applicationResult2 = attendanceConsolidatedMbl.StudentAttedanceConsolidated_Select_By_ID(Convert.ToInt32(this.ViewState["StudentAttendanceConsolidatedMID"].ToString()));
                    if (applicationResult2 == null)
                        return;
                    DataTable resultDt = applicationResult2.resultDT;
                    if (resultDt.Rows.Count <= 0)
                        return;
                    txtdate.Text = resultDt.Rows[0]["AttendanceTakenDate"].ToString();
                    txtTotalStudentCount.Text = resultDt.Rows[0]["TotalStudentCount"].ToString();
                    txtPresentStudentCount.Text = resultDt.Rows[0]["PresentStudentCount"].ToString();
                    txtAbsentStudentCount.Text = resultDt.Rows[0]["AbsentStudentCount"].ToString();
                    ddlClass.SelectedValue = resultDt.Rows[0]["ClassMID"].ToString();
                    BindDivisionByClassMID(Convert.ToInt32(resultDt.Rows[0]["ClassMID"]));
                    ddlDivision.SelectedValue = resultDt.Rows[0]["DivisionTID"].ToString();
                    ddlAcademicYear.SelectedValue = resultDt.Rows[0]["AcademicYear"].ToString();
                    PanelVisibility(2);
                }
                else
                {
                    if (!(e.CommandName.ToString() == "Delete1"))
                        return;
                    if (attendanceConsolidatedMbl.StudentAttendanceConsolidated_Delete(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(this.Session["UserID"]), DateTime.UtcNow.AddHours(5.5).ToString()).status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record deleted successfully.');</script>");
                        ClearAll();
                        PanelVisibility(3);
                    }
                    else
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You cannot delete this record because it is in used.');</script>");
                }
            }
            catch (Exception ex)
            {
                StudentAttendanceConsolidated.logger.Error((object)"Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }



        public void ClearAll()
        {
            new Controls().ClearForm(this.Master.FindControl("ContentPlaceHolder1"));
            ViewState["Mode"] = (object)"Save";
        }


        public void PanelVisibility(int intcode)
        {
            switch (intcode)
            {
                case 1:
                    divGrid.Visible = true;
                    tabs.Visible = false;
                    lnkViewList.Visible = false;
                    break;
                case 2:
                    divGrid.Visible = false;
                    tabs.Visible = true;
                    lnkViewList.Visible = true;
                    break;
                case 3:
                    divGrid.Visible = true;
                    gvStudentAttendance.Visible = false;
                    tabs.Visible = false;
                    lnkViewList.Visible = false;
                    break;
            }
        }



        public void BindgvStudentAttendance(string DtAttendanceTakenDate,int intClassMID,int intDivisionTID)
        {
            ApplicationResult applicationResult1 = new ApplicationResult();
            ApplicationResult applicationResult2 = new StudentAttendanceConsolidatedMBL().StudentAttendanceConsolidated_SelectAll(DtAttendanceTakenDate, intClassMID, intDivisionTID);
            if (applicationResult2.resultDT.Rows.Count > 0)
            {
                gvStudentAttendance.DataSource = (object)applicationResult2.resultDT;
                gvStudentAttendance.DataBind();
                gvStudentAttendance.Visible = true;
                NoDataFound.Visible = false;
                PanelVisibility(1);
            }
            else
            {
                gvStudentAttendance.Visible = false;
                NoDataFound.Visible = true;
            }
        }



        protected void ddlClass1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult applicationResult1 = new ApplicationResult();
                Controls controls = new Controls();
                ApplicationResult applicationResult2 = new DivisionTBL().DivisionT_Select_DivisionName_By_Class(Convert.ToInt32(this.ddlClass1.SelectedValue));
                if (applicationResult2 == null)
                    return;
                if (applicationResult2.resultDT.Rows.Count > 0)
                    controls.BindDropDown_ListBox(applicationResult2.resultDT, (Control)this.ddlDivision1, "DivisionName", "DivisionTID");
                ddlDivision1.Items.Insert(0, new ListItem("--Select--", ""));
            }
            catch (Exception ex)
            {
                StudentAttendanceConsolidated.logger.Error((object)"Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }




        protected void btnGo_Click(object sender, EventArgs e) => this.BindgvStudentAttendance(Convert.ToString(this.txtAttendanceTakenDate.Text), Convert.ToInt32(this.ddlClass1.SelectedValue), Convert.ToInt32(this.ddlDivision1.SelectedValue));
    }
}
