using GEIMS.BL;
using GEIMS.BO;
using GEIMS.Common;
using GEIMS.DataAccess;
using log4net;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GEIMS.Client.UI
{
    public partial class EmployeeWorkTiming : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(EmployeeWorkTiming));
        Controls objControl = new Controls();

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("../UserLogin.aspx");
                }
                if (!Page.IsPostBack)
                {
                    DayOfWeek();
                    GetSchoolName();
                    btnSave.Enabled = false;
                    if(btnSave.Enabled == false)
                    {
                        btnSave.BackColor = System.Drawing.Color.Gray;
                    }
                    //txtdate.Attributes.Add("readonly", "readonly");
                    ViewState["Mode"] = "Save";
                    divNote.Visible = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        //#region Bind ShiftName 
        //public void BindShiftName()
        //{
        //    ShiftMasterBL objShiftMasterBL = new ShiftMasterBL();
        //    ShiftMasterBO objShiftMasterrBO = new ShiftMasterBO();
        //    ApplicationResult objResultSelectAll = new ApplicationResult();
        //    DropDownList ddlShiftName = new DropDownList();
        //    objResultSelectAll = objShiftMasterBL.Shift_SelectAll_For_Employee(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
        //    if (objResultSelectAll != null)
        //    {
        //        DataTable dtSelectAll = new DataTable();
        //        dtSelectAll = objResultSelectAll.resultDT;

        //        for (int i = 0; i < gvEmployee.Rows.Count; i++)
        //        {
        //            ddlShiftName = (DropDownList)gvEmployee.Rows[i].Cells[0].FindControl("ddlShiftName");
        //            if (ddlShiftName != null)
        //            {
        //                objControl.BindDropDown_ListBox(dtSelectAll, ddlShiftName, "ShiftName", "ShiftMID");
        //                ddlShiftName.Items.Insert(0, new ListItem("Select", "-1"));
        //            }
        //        }
        //    }
        //}
        //#endregion

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
                ddlDepartment.Items.Insert(0, new ListItem("--Select--", ""));

            }
        }

        #endregion

        #region Fetch Department data For DropDown 
        private DataTable FetchSection(int intSchoolMID)
        {
            DataTable dtDivision = new DataTable();

            DepartmentBL objDepartmentBL = new DepartmentBL();
            DepartmentBO objDepartmentBO = new DepartmentBO();
            ApplicationResult objResults = new ApplicationResult();

            objResults = objDepartmentBL.Department_SelectAll_ForDropDown(intSchoolMID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {

            }
            return objResults.resultDT;
        }

        #endregion

        #region BindGrid of Empolyee
        protected void BindEmpolyeeGrid()
        {
            try
            {
                EmployeeWorkTimeBL objEmployeeWorkTimeBL = new EmployeeWorkTimeBL();
                ApplicationResult objResult = new ApplicationResult();
                //EmployeeMBL objEmployeeBL = new EmployeeMBL();

                EmployeeattendanceBl objEmployeeAttendenceBL = new EmployeeattendanceBl();
                ApplicationResult objResult1 = new ApplicationResult();
                EmployeeMBL objEmployeeBL = new EmployeeMBL();

                objResult1 = objEmployeeBL.Select_Employee_ForAttandance(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult1 != null)
                {
                    gvEmployee.DataSource = objResult1.resultDT;
                    gvEmployee.DataBind();
                    if (objResult1.resultDT.Rows.Count > 0)
                    {
                        divGrid.Visible = true;
                        btnSave.Enabled = true;
                        gvEmployee.Visible = true;
                        objResult = objEmployeeWorkTimeBL.Select_Employee_ForWorkTimeDayWise(Convert.ToInt32(ddlDaysofweek.SelectedValue));
                        if (objResult != null)
                        {
                            if (objResult.resultDT.Rows.Count > 0)
                            {
                                ViewState["Mode"] = "Edit";
                                ViewState["WorkTimingMID"] = objResult.resultDT.Rows[0][EmployeeWorkTimeBO.EMPLOYEEWORKTIME_WORKTIMEINGMID].ToString();

                                foreach (GridViewRow row in gvEmployee.Rows)
                                {
                                    for (int i = 0; i < objResult.resultDT.Rows.Count; i++)
                                    {
                                        if (row.Cells[0].Text ==
                                            objResult.resultDT.Rows[i][
                                                EmployeeWorkTimeBO.EMPLOYEEWORKTIME_EMPLOYEEMID].ToString())
                                        {
                                           ((CheckBox)row.FindControl("chkChild")).Checked = true;

                                            (((TextBox)row.FindControl("txtIntime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeWorkTimeBO.EMPLOYEEWORKTIME_STARTTIME].ToString();

                                            (((TextBox)row.FindControl("txtOuttime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeWorkTimeBO.EMPLOYEEWORKTIME_ENDTIME].ToString();

                                            (((Label)row.FindControl("lblTime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeWorkTimeBO.EMPLOYEEWORKTIME_TOTALTIME].ToString();
                                            (((Label)row.FindControl("lblRecessStartTime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeWorkTimeBO.EMPLOYEEWORKTIME_RECESSSTARTTIME].ToString();

                                            (((Label)row.FindControl("lblRecessEndTime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeWorkTimeBO.EMPLOYEEWORKTIME_RECESSENDTIME].ToString();

                                            (((Label)row.FindControl("lblFirstHalfTime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeWorkTimeBO.EMPLOYEEWORKTIME_FIRSTHALFTIME].ToString();

                                            (((Label)row.FindControl("lblSecondHalfTime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeWorkTimeBO.EMPLOYEEWORKTIME_SECONDHALFTIME].ToString();

                                            (((DropDownList)row.FindControl("ddlShiftName")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeWorkTimeBO.EMPLOYEEWORKTIME_SHIFTNAME].ToString();

                                        }
                                    }
                                }
                            }
                            else
                            {
                                ViewState["Mode"] = "Save";
                              //  Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CheckAll();", true);
                            }
                        }
                    }
                    else
                    {
                        btnSave.Enabled = false;
                        divGrid.Visible = false;
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Insert Employee For Attendence.');</script>");
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        #region Employee Grid Bind
        protected void BindEmpolyeeGrid_DepartmentWise()
        {
            try
            {
                EmployeeWorkTimeBL objEmployeeWorkTimeBL = new EmployeeWorkTimeBL();
                ApplicationResult objResult = new ApplicationResult(); 

                EmployeeattendanceBl objEmployeeAttendenceBL = new EmployeeattendanceBl();
                ApplicationResult objResult1 = new ApplicationResult();
                EmployeeMBL objEmployeeBL = new EmployeeMBL();

                objResult1 = objEmployeeBL.Select_Employee_ByDepartment_ForAttandance(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(ddlSchoolName.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue)); ;
                if (objResult1 != null)
                {
                    gvEmployee.DataSource = objResult1.resultDT;
                    gvEmployee.DataBind();
                    if (objResult1.resultDT.Rows.Count > 0)
                    {
                        divGrid.Visible = true;
                        btnSave.Enabled = true;
                        gvEmployee.Visible = true;
                        //BindShiftName();
                        objResult = objEmployeeWorkTimeBL.Select_Employee_ForWorkTimeDayWise(Convert.ToInt32(ddlDaysofweek.SelectedValue));
                        if (objResult != null)
                        {
                            if (objResult.resultDT.Rows.Count > 0)
                            {
                                ViewState["Mode"] = "Edit";
                                ViewState["WorkTimingMID"] = objResult.resultDT.Rows[0][EmployeeWorkTimeBO.EMPLOYEEWORKTIME_WORKTIMEINGMID].ToString();
                              
                                foreach (GridViewRow row in gvEmployee.Rows)
                                {
                                    for (int i = 0; i < objResult.resultDT.Rows.Count; i++)
                                    {
                                        if (row.Cells[0].Text ==
                                            objResult.resultDT.Rows[i][
                                                EmployeeWorkTimeBO.EMPLOYEEWORKTIME_EMPLOYEEMID].ToString())
                                        {
                                            ((CheckBox)row.FindControl("chkChild")).Checked = true;

                                            (((TextBox)row.FindControl("txtIntime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeWorkTimeBO.EMPLOYEEWORKTIME_STARTTIME].ToString();

                                            (((TextBox)row.FindControl("txtOuttime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeWorkTimeBO.EMPLOYEEWORKTIME_ENDTIME].ToString();

                                            (((Label)row.FindControl("lblTime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeWorkTimeBO.EMPLOYEEWORKTIME_TOTALTIME].ToString();

                                            (((Label)row.FindControl("lblRecessStartTime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeWorkTimeBO.EMPLOYEEWORKTIME_RECESSSTARTTIME].ToString();

                                            (((Label)row.FindControl("lblRecessEndTime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeWorkTimeBO.EMPLOYEEWORKTIME_RECESSENDTIME].ToString();

                                            (((Label)row.FindControl("lblFirstHalfTime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeWorkTimeBO.EMPLOYEEWORKTIME_FIRSTHALFTIME].ToString();

                                            (((Label)row.FindControl("lblSecondHalfTime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeWorkTimeBO.EMPLOYEEWORKTIME_SECONDHALFTIME].ToString();

                                            (((DropDownList)row.FindControl("ddlShiftName")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeWorkTimeBO.EMPLOYEEWORKTIME_SHIFTNAME].ToString();
                                        }

                                    }
                                }
                            }
                            else
                            {
                                ViewState["Mode"] = "Save";
                                //  Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CheckAll();", true);
                            }
                        }
                    }
                    else
                    {
                        btnSave.Enabled = false;
                        divGrid.Visible = false;
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Insert Employee For Attendence.');</script>");
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

        protected void BindEmpolyeeGrid_SchoolWise()
        {
            try
            {
                EmployeeWorkTimeBL objEmployeeWorkTimeBL = new EmployeeWorkTimeBL();
                ApplicationResult objResult = new ApplicationResult();

                EmployeeattendanceBl objEmployeeAttendenceBL = new EmployeeattendanceBl();
                ApplicationResult objResult1 = new ApplicationResult();
                EmployeeMBL objEmployeeBL = new EmployeeMBL();



                objResult1 = objEmployeeBL.Select_Employee_BySchool_ForAttandance(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(ddlSchoolName.SelectedValue)); ;
                if (objResult1 != null)
                {
                    gvEmployee.DataSource = objResult1.resultDT;
                    gvEmployee.DataBind();
                    if (objResult1.resultDT.Rows.Count > 0)
                    {
                        divGrid.Visible = true;
                        btnSave.Enabled = true;
                        gvEmployee.Visible = true;
                        objResult = objEmployeeWorkTimeBL.Select_Employee_ForWorkTimeDayWise(Convert.ToInt32(ddlDaysofweek.SelectedValue));
                        if (objResult != null)
                        {
                            if (objResult.resultDT.Rows.Count > 0)
                            {
                                ViewState["Mode"] = "Edit";
                                ViewState["WorkTimingMID"] = objResult.resultDT.Rows[0][EmployeeWorkTimeBO.EMPLOYEEWORKTIME_WORKTIMEINGMID].ToString();

                                foreach (GridViewRow row in gvEmployee.Rows)
                                {
                                    for (int i = 0; i < objResult.resultDT.Rows.Count; i++)
                                    {
                                        if (row.Cells[0].Text ==
                                            objResult.resultDT.Rows[i][
                                                EmployeeWorkTimeBO.EMPLOYEEWORKTIME_EMPLOYEEMID].ToString())
                                        {
                                            ((CheckBox)row.FindControl("chkChild")).Checked = true;

                                            (((TextBox)row.FindControl("txtIntime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeWorkTimeBO.EMPLOYEEWORKTIME_STARTTIME].ToString();

                                            (((TextBox)row.FindControl("txtOuttime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeWorkTimeBO.EMPLOYEEWORKTIME_ENDTIME].ToString();

                                            (((Label)row.FindControl("lblTime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeWorkTimeBO.EMPLOYEEWORKTIME_TOTALTIME].ToString();

                                            (((Label)row.FindControl("lblRecessStartTime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeWorkTimeBO.EMPLOYEEWORKTIME_RECESSSTARTTIME].ToString();

                                            (((Label)row.FindControl("lblRecessEndTime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeWorkTimeBO.EMPLOYEEWORKTIME_RECESSENDTIME].ToString();

                                            (((Label)row.FindControl("lblFirstHalfTime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeWorkTimeBO.EMPLOYEEWORKTIME_FIRSTHALFTIME].ToString();

                                            (((Label)row.FindControl("lblSecondHalfTime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeWorkTimeBO.EMPLOYEEWORKTIME_SECONDHALFTIME].ToString();

                                            (((DropDownList)row.FindControl("ddlShiftName")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeWorkTimeBO.EMPLOYEEWORKTIME_SHIFTNAME].ToString();

                                        }

                                    }
                                }
                            }
                            else
                            {
                                ViewState["Mode"] = "Save";
                                //  Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CheckAll();", true);
                            }
                        }
                    }
                    else
                    {
                        btnSave.Enabled = false;
                        divGrid.Visible = false;
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Insert Employee For Attendence.');</script>");
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

        #region View Button Click Event
        protected void btnViewGrid_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlSchoolName.SelectedIndex != 0 && ddlDepartment.SelectedIndex != 0 && ddlDaysofweek.SelectedIndex != 0)
                {
                    btnSave.Enabled = true;
                    if(btnSave.Enabled == true)
                    {
                        /*Name : Arpit Shah
                         Note : This method are using direct CSS Class,  or CSS Attirbutes aspx.cs file
                        */

                        //btnSave.BackColor = System.Drawing.Color.CadetBlue;
                        
                        btnSave.Attributes.Add("style", "background:#3B5998; border: 1px solid #3B5998; border-radius: 5px;");  //Apply .CSS Attributes
                        //btnSave.Style.Add("background", "#3B5998");  //Apply Attributes
                        //btnSave.Attributes.Add("class", "btn-blue-medium"); //Apply .CSS Class 
                    }
                    // GetSchoolName();
                    // ddlSchoolName.Visible = true;
                    // ddlSection.Visible = true;
                    if ((ddlSchoolName.SelectedIndex > 0) && (ddlDepartment.SelectedIndex > 0))
                    {
                        //BindShiftName();
                        BindEmpolyeeGrid_DepartmentWise();
                    }
                    else if ((ddlSchoolName.SelectedIndex > 0) && (ddlDepartment.SelectedIndex == 0))
                    {
                        // ddlDepartment.SelectedIndex=;
                        // BindShiftName();
                        BindEmpolyeeGrid_SchoolWise();
                    }
                    else
                    {
                        //BindShiftName();
                        BindEmpolyeeGrid();
                    }
                    divNote.Visible = true;
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                            "<script language='javascript'>alert('Please Select All DropDown List!!!!');</script>");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Save Button Event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            EmployeeWorkTimeBL objEmployeeWorkTimeBL = new EmployeeWorkTimeBL();
            EmployeeWorkTimeBO objEmployeeWorkTimeBO = new EmployeeWorkTimeBO();
            ApplicationResult objResult = new ApplicationResult();

            EmployeeattendanceBl objEmployeeAttendenceBL = new EmployeeattendanceBl();
            EmployeeattendanceBo objEmployeeAttendenceBO = new EmployeeattendanceBo();
            ApplicationResult objResults = new ApplicationResult();
            try
            {
                int intCount = 0;
                int k = 0;
                DatabaseTransaction.OpenConnectionTransation();

                foreach (GridViewRow row in gvEmployee.Rows)
                {
                    if (((CheckBox)row.FindControl("chkChild")).Checked)
                    {

                        ViewState["EmployeeMID"] = Convert.ToInt32(row.Cells[0].Text);
                    objEmployeeWorkTimeBO.EmployeeMID = Convert.ToInt32(row.Cells[0].Text);
                    objEmployeeWorkTimeBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                    objEmployeeWorkTimeBO.SchoolMID = Convert.ToInt32(ddlSchoolName.SelectedValue);
                    objEmployeeWorkTimeBO.DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
                    objEmployeeWorkTimeBO.Daysofweek = Convert.ToInt32(ddlDaysofweek.SelectedValue);
                    objEmployeeWorkTimeBO.StartTime =
                       (((TextBox)row.FindControl("txtIntime")).Text);
               
                    objEmployeeWorkTimeBO.EndTime =
                      (((TextBox)row.FindControl("txtOuttime")).Text);
                        objEmployeeWorkTimeBO.RecessStartTime =
                        (((Label)row.FindControl("lblRecessStartTime")).Text);
                        objEmployeeWorkTimeBO.RecessEndTime =
                      (((Label)row.FindControl("lblRecessEndTime")).Text);
                        objEmployeeWorkTimeBO.FirstHalfTime =
                    (((Label)row.FindControl("lblFirstHalfTime")).Text);
                        objEmployeeWorkTimeBO.SecondHalfTime =
                  (((Label)row.FindControl("lblSecondHalfTime")).Text);
                        objEmployeeWorkTimeBO.ShiftName =
                (((DropDownList)row.FindControl("ddlShiftName")).Text);

                        if (objEmployeeWorkTimeBO.StartTime == "0" || objEmployeeWorkTimeBO.EndTime == "0" || objEmployeeWorkTimeBO.StartTime == "0" || objEmployeeWorkTimeBO.EndTime == "0")
                        {
                            objEmployeeWorkTimeBO.StartTime = "23:58";
                            objEmployeeWorkTimeBO.EndTime = "23:59";
                            //ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                            //    "<script language='javascript'>alert('Please Fill Time " + row.Cells[0].Text +
                            //   ".');</script>");
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
    "<script language='javascript'>alert('Please Fill The Timings For All Selected Employees');</script>");

                            if ((ddlSchoolName.SelectedIndex > 0) && (ddlDepartment.SelectedIndex > 0))
                            {
                                BindEmpolyeeGrid_DepartmentWise();
                            }
                            else if ((ddlSchoolName.SelectedIndex > 0) && (ddlDepartment.SelectedIndex == 0))
                            {
                                BindEmpolyeeGrid_SchoolWise();
                            }
                            else
                            {
                                BindEmpolyeeGrid();
                            }
                            divNote.Visible = true;


                            goto select;
                            //break;
                        }
                        else
                        {
                            //10/11/2022 Bhandavi getting oops error if starttime is 0
                            DateTime time1 = Convert.ToDateTime(objEmployeeWorkTimeBO.StartTime);
                            DateTime time2 = Convert.ToDateTime(objEmployeeWorkTimeBO.EndTime);
                            // TimeSpan TimeSpan = time2.Subtract(time1).ToString("HH\\:mm");
                            objEmployeeWorkTimeBO.TotalTime = time2.Subtract(time1).ToString(@"hh\:mm");
                            // (((Label)row.FindControl("lblTime")).Text);
                            //if (((CheckBox)row.FindControl("chkChild")).Checked)
                            //{

                            intCount += 1;
                            //10/11/2022 Bhandavi getting oops error if starttime is 0 

                            // if (objEmployeeWorkTimeBO.StartTime == "0" || objEmployeeWorkTimeBO.EndTime == "0" || objEmployeeWorkTimeBO.StartTime == "0" || objEmployeeWorkTimeBO.EndTime == "0")
                            //{
                            //objEmployeeWorkTimeBO.StartTime = "23:58";
                            //objEmployeeWorkTimeBO.EndTime = "23:59";
                            //ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                            //    "<script language='javascript'>alert('Please Fill Time " + row.Cells[0].Text +
                            //   ".');</script>");
                            //break;
                            //}
                            DateTime dtIntime = Convert.ToDateTime((((TextBox)row.FindControl("txtIntime")).Text));
                            DateTime dtOutTime = Convert.ToDateTime((((TextBox)row.FindControl("txtOuttime")).Text));

                            // DateTime dtRecOutTime = Convert.ToDateTime((((TextBox)row.FindControl("txtRecOuttime")).Text));
                            //DateTime dtRecIntime = Convert.ToDateTime((((TextBox)row.FindControl("txtRecIntime")).Text));

                            if ((dtIntime >= dtOutTime))
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                    "<script language='javascript'>alert('Time Format Should be 24Hours Or OutTime Must be greater then InTime.');</script>");
                                break;
                            }
                            else
                            {
                                ApplicationResult objResultsInsert = new ApplicationResult();
                                if (ViewState["Mode"].ToString() == "Save")
                                {
                                    objEmployeeWorkTimeBO.CreatedModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                                    objEmployeeWorkTimeBO.CreateModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                    objResultsInsert =
                                        objEmployeeWorkTimeBL.EmployeeWorkTime_Insert(objEmployeeWorkTimeBO);
                                    divGrid.Visible = false;
                                    btnSave.Enabled = false;
                                    k += 1;
                                }
                                else
                                {
                                    objEmployeeWorkTimeBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                                    objEmployeeWorkTimeBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                    objEmployeeWorkTimeBO.EmployeeMID = Convert.ToInt32(ViewState["EmployeeMID"].ToString());
                                    objResult = objEmployeeWorkTimeBL.EmployeeWorkTime_Update(objEmployeeWorkTimeBO);
                                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                                    {
                                        k += 1;
                                        divGrid.Visible = false;
                                        btnSave.Enabled = false;
                                    }
                                }
                            }
                        }
                    }
                }
                if (k == intCount)
                {
                    DatabaseTransaction.CommitTransation();
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Employee Working Time saved Successfully.');</script>");
                    divNote.Visible = false;
                }
                else
                {
                    DatabaseTransaction.RollbackTransation();
                }
                select: ;
            }
            catch (Exception ex)
            {
                DatabaseTransaction.RollbackTransation();
                DatabaseTransaction.connection.Close();
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Scholl Name Selected Index Change
        protected void ddlSchoolName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                DepartmentBL objDepartmentBl = new DepartmentBL();

                if (ddlSchoolName.SelectedIndex != 0)
                {
                    objResult = objDepartmentBl.Department_SelectAll_ForDropDown(Convert.ToInt32(ddlSchoolName.SelectedValue));
                    if (objResult != null)
                    {
                        ddlDepartment.Enabled = true;
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            objControls.BindDropDown_ListBox(objResult.resultDT, ddlDepartment, "DepartmentNameENG", "DepartmentID");

                        }
                        ddlDepartment.Items.Insert(0, new ListItem("--Select--", ""));

                    }
                }
                else
                {
                    ddlDepartment.Enabled = false;
                }

            }
            catch (Exception)
            {

                throw;
            }
            divGrid.Visible = false;
            btnSave.Enabled = false;
        }
        #endregion

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            divGrid.Visible = false;
            btnSave.Enabled = false;
        }

       

        protected void gvEmployee_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void ddlDaysofweek_SelectedIndexChanged(object sender, EventArgs e)
        {
            // btnViewGrid_Click(sender, e);
            divGrid.Visible = false;
            btnSave.Enabled = false;
        }

        #region BindDayOfWeek [Static Value]
        public void DayOfWeek()
        {
            try
            {
                ddlDaysofweek.Items.Insert(0, new ListItem("--Select--", "-1"));
                ddlDaysofweek.Items.Insert(1, new ListItem("Mon","2"));
                ddlDaysofweek.Items.Insert(2, new ListItem("Tue", "3"));
                ddlDaysofweek.Items.Insert(3, new ListItem("Wed", "4"));
                ddlDaysofweek.Items.Insert(4, new ListItem("Thu", "5"));
                ddlDaysofweek.Items.Insert(5, new ListItem("Fri", "6"));
                ddlDaysofweek.Items.Insert(6, new ListItem("Sat", "7"));
                //ddlDaysofweek.Items.Insert(0, new ListItem("--Select--", ""));
            }
            catch(Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region ShiftName Dropdown Change SelectedIndexChange
        protected void ddlShiftName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ShiftMasterBL objShiftMasterBL = new ShiftMasterBL();
                ShiftMasterBO objShiftMasterBO = new ShiftMasterBO();
                ApplicationResult objResults = new ApplicationResult();
                Controls objControls = new Controls();

                //Name : Arpit Shah
                //GridView's Dropdown value find when select index change
                GridViewRow gvr = (GridViewRow)((DropDownList)sender).Parent.Parent;
                DropDownList ddl = (DropDownList)sender;

                TextBox txtIntime = gvr.FindControl("txtIntime") as TextBox;
                TextBox txtOuttime = gvr.FindControl("txtOuttime") as TextBox;
                Label lblTime = gvr.FindControl("lblTime") as Label;
                Label lblRecessStartTime = gvr.FindControl("lblRecessStartTime") as Label;
                Label lblRecessEndTime = gvr.FindControl("lblRecessEndTime") as Label;
                Label lblFirstHalfTime = gvr.FindControl("lblFirstHalfTime") as Label;
                Label lblSecondHalfTime = gvr.FindControl("lblSecondHalfTime") as Label;

                objResults = objShiftMasterBL.InTime_Select_ShiftWise(Convert.ToInt32(ddl.SelectedValue), Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                if (objResults != null)
                {
                    if (objResults.resultDT.Rows.Count > 0)
                    {
                        txtIntime.Text = objResults.resultDT.Rows[0][ShiftMasterBO.SHIFTMASTER_STARTTIME].ToString();
                        txtOuttime.Text = objResults.resultDT.Rows[0][ShiftMasterBO.SHIFTMASTER_ENDTIME].ToString();
                        lblTime.Text = objResults.resultDT.Rows[0][ShiftMasterBO.SHIFTMASTER_TOTALWORKINGHOURS].ToString();

                        lblRecessStartTime.Text = objResults.resultDT.Rows[0][ShiftMasterBO.SHIFTMASTER_RECESSSTARTTIME].ToString();
                        lblRecessEndTime.Text = objResults.resultDT.Rows[0][ShiftMasterBO.SHIFTMASTER_RECESSENDTIME].ToString();
                        lblFirstHalfTime.Text = objResults.resultDT.Rows[0][ShiftMasterBO.SHIFTMASTER_TOTALFIRSTHALFTIME].ToString();
                        lblSecondHalfTime.Text = objResults.resultDT.Rows[0][ShiftMasterBO.SHIFTMASTER_TOTALSECONDHALFTIME].ToString();
                        //txtIntime.Text = objResults.resultDT.Rows[0][ShiftMasterBO.SHIFTMASTER_STARTTIME].ToString();
                        // txtIntime.Text = objResults.resultDT.Rows[0][ShiftMasterBO.SHIFTMASTER_STARTTIME].ToString();
                        //objControls.BindDropDown_ListBox(objResults.resultDT, txtIntime, "StartTime", "ShiftMID");
                    }
                    else
                    {
                        txtIntime.Text = "0";
                        txtOuttime.Text = "0";
                        lblTime.Text = "0:0";
                        lblRecessStartTime.Text = "0:0";
                        lblRecessEndTime.Text = "0:0"; ;
                        lblFirstHalfTime.Text = "0:0";
                        lblSecondHalfTime.Text = "0:0";
                    }
                }
                // So in this you can get second drodpdown and bind your data
                //dsCust = Convert.ToInt32(ddl.SelectedValue);
                //ddlsecond.DataSource = dsCust.Tables[0];
                //ddlsecond.DataTextField = "CustName";
                //ddlsecond.DataValueField = "ID";
                //ddlsecond.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Row Bound Event
        protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                ShiftMasterBL objShiftMasterBL = new ShiftMasterBL();
                ShiftMasterBO objShiftMasterrBO = new ShiftMasterBO();
                ApplicationResult objResultSelectAll = new ApplicationResult();
                Controls objControls = new Controls();
                DataTable dt1 = new DataTable();
                //DropDownList ddlShiftName = new DropDownList();

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlShiftName = (DropDownList)e.Row.FindControl("ddlShiftName");

                    objResultSelectAll = objShiftMasterBL.Shift_SelectAll_For_Employee(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                    if (objResultSelectAll != null)
                    {
                        if (objResultSelectAll.resultDT.Rows.Count > 0)
                        {
                            objControls.BindDropDown_ListBox(objResultSelectAll.resultDT, ddlShiftName, "ShiftName", "ShiftMID");
                        }
                        ddlShiftName.Items.Insert(0, new ListItem("-Select-", "-1"));
                    }
                }
                //if (e.Row.RowType == DataControlRowType.Header)
                //{
                //    DropDownList ddlShiftNameMaster = (DropDownList)e.Row.FindControl("ddlShiftNameMaster");

                //    objResultSelectAll = objShiftMasterBL.Shift_SelectAll_For_Employee(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                //    objResultSelectAll = objShiftMasterBL.Shift_SelectAll_For_Employee(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                //    if (objResultSelectAll != null)
                //    {
                //        if (objResultSelectAll.resultDT.Rows.Count > 0)
                //        {
                //            objControls.BindDropDown_ListBox(objResultSelectAll.resultDT, ddlShiftNameMaster, "ShiftName", "ShiftMID");
                //        }
                //        ddlShiftNameMaster.Items.Insert(0, new ListItem("-Select-", "-1"));
                //    } 
                //}
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Status Header Dropdown Change SelectedIndexChange
        protected void ddlShiftNameMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gvEmployee.Rows)
                {
                    //Bind all Dropdown
                    DropDownList ddlShiftName = (DropDownList)row.FindControl("ddlShiftName");
                    DropDownList ddlShiftNameMaster = (DropDownList)gvEmployee.HeaderRow.FindControl("ddlShiftNameMaster");
                    //int id = Convert.ToInt32(ddlShiftNameMaster);

                    ddlShiftName.SelectedValue = ddlShiftNameMaster.SelectedValue;
                    //ddlShiftName_SelectedIndexChanged(sender, e);
                    //gvEmployee.Columns[06].Visible = true;

                    //Bind all another field
                    ShiftMasterBL objShiftMasterBL = new ShiftMasterBL();
                    ShiftMasterBO objShiftMasterBO = new ShiftMasterBO();
                    ApplicationResult objResultsMaster = new ApplicationResult();
                    Controls objControls = new Controls();

                    GridViewRow gvr = (GridViewRow)((DropDownList)sender).Parent.Parent;
                    //DropDownList ddl = (DropDownList)sender;

                    TextBox txtIntime = gvr.FindControl("txtIntime") as TextBox;
                    TextBox txtOuttime = gvr.FindControl("txtOuttime") as TextBox;
                    Label lblTime = gvr.FindControl("lblTime") as Label;
                    Label lblRecessStartTime = gvr.FindControl("lblRecessStartTime") as Label;
                    Label lblRecessEndTime = gvr.FindControl("lblRecessEndTime") as Label;
                    Label lblFirstHalfTime = gvr.FindControl("lblFirstHalfTime") as Label;
                    Label lblSecondHalfTime = gvr.FindControl("lblSecondHalfTime") as Label;

                    objResultsMaster = objShiftMasterBL.InTime_Select_ShiftWise(Convert.ToInt32(ddlShiftName.SelectedValue), Convert.ToInt32(Session[ApplicationSession.TRUSTID]));

                    //foreach (GridViewRow rowEmployee in gvEmployee.Rows)
                    //{
                    //    for (int i = 0; i < objResultsMaster.resultDT.Rows.Count; i++)
                    //    {
                    //        //if (row.Cells[0].Text == objResultsMaster.resultDT.Rows[i][ShiftMasterBO.SHIFTMASTER_STARTTIME].ToString()) ; 
                    //        txtIntime.Text = objResultsMaster.resultDT.Rows[i][ShiftMasterBO.SHIFTMASTER_STARTTIME].ToString();
                    //    }
                    //}
                    //if (objResultsMaster != null)
                    //{
                    //    if (objResultsMaster.resultDT.Rows.Count > 0)
                    //    {
                    //        int i = 0;
                    //        for(i = 0; i<=gvEmployee.Rows.Count;i++)
                    //        {
                    //            row.Cells[6].Text = objResultsMaster.resultDT.Rows[0][ShiftMasterBO.SHIFTMASTER_STARTTIME].ToString();
                    //            row.Cells[7].Text = objResultsMaster.resultDT.Rows[0][ShiftMasterBO.SHIFTMASTER_ENDTIME].ToString();
                    //            row.Cells[8].Text = objResultsMaster.resultDT.Rows[0][ShiftMasterBO.SHIFTMASTER_TOTALWORKINGHOURS].ToString();
                    //            row.Cells[9].Text = objResultsMaster.resultDT.Rows[0][ShiftMasterBO.SHIFTMASTER_RECESSSTARTTIME].ToString();
                    //            row.Cells[10].Text = objResultsMaster.resultDT.Rows[0][ShiftMasterBO.SHIFTMASTER_RECESSENDTIME].ToString();
                    //            row.Cells[11].Text = objResultsMaster.resultDT.Rows[0][ShiftMasterBO.SHIFTMASTER_TOTALFIRSTHALFTIME].ToString();
                    //            row.Cells[12].Text = objResultsMaster.resultDT.Rows[0][ShiftMasterBO.SHIFTMASTER_TOTALSECONDHALFTIME].ToString();
                                
                    //            //txtIntime.Text = objResultsMaster.resultDT.Rows[i][ShiftMasterBO.SHIFTMASTER_STARTTIME].ToString();
                    //            //txtOuttime.Text = objResultsMaster.resultDT.Rows[i][ShiftMasterBO.SHIFTMASTER_ENDTIME].ToString();
                    //            //lblTime.Text = objResultsMaster.resultDT.Rows[i][ShiftMasterBO.SHIFTMASTER_TOTALWORKINGHOURS].ToString();

                    //            //lblRecessStartTime.Text = objResultsMaster.resultDT.Rows[i][ShiftMasterBO.SHIFTMASTER_RECESSSTARTTIME].ToString();
                    //            //lblRecessEndTime.Text = objResultsMaster.resultDT.Rows[i][ShiftMasterBO.SHIFTMASTER_RECESSENDTIME].ToString();
                    //            //lblFirstHalfTime.Text = objResultsMaster.resultDT.Rows[i][ShiftMasterBO.SHIFTMASTER_TOTALFIRSTHALFTIME].ToString();
                    //            //lblSecondHalfTime.Text = objResultsMaster.resultDT.Rows[i][ShiftMasterBO.SHIFTMASTER_TOTALSECONDHALFTIME].ToString();
                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion


 
    }
}