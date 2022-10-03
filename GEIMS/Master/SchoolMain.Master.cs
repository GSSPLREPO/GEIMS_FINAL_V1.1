using System;
using System.Data;
using System.Web.UI;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using System.Web.UI.HtmlControls;

namespace GEIMS.Master
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
		private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            // if (!IsPostBack)
            //{
           
                SessionSchoolName.InnerText = Session["SchoolName"].ToString();
                SessionTrustNameFooter.InnerText = Session["TrustName"].ToString();
                //SessionSchoolName.InnerText = Session["SchoolName"].ToString();

                if (Session["UserName"] != null)

                {
                    lblUserName.Text = Session["UserName"].ToString();
                    //       lblSchoolName.Text = Session[ApplicationSession.SCHOOLNAME].ToString();
                    string sPath = Page.Page.AppRelativeVirtualPath;
                    // string str= Request.Url.GetLeftPart(UriPartial.Authority);
                    string sRet = sPath.Substring(sPath.LastIndexOf('/') + 1);

                    if (Convert.ToInt32(Session[ApplicationSession.ISPANEL].ToString()) != 0)
                    {
                        divTransfer.Visible = false;
                    }
                    else
                    {
                        divTransfer.Visible = true;
                    }


                //if (Session[ApplicationSession.SCHOOLNAME].ToString() == "Fertilizer Nagar School")
                //// if (Convert.ToInt32(Session[ApplicationSession.ISPANEL].ToString()) == 0 && Convert.ToInt32(Session[ApplicationSession.ISPANEL].ToString()) == 3)
                //{
                //    Li2.Visible = true;
                //    Budget.Visible = true;
                //    Li1.Visible = true;
                //    BudgetLi2.Visible = true;
                //    BudgetScreenLi2.Visible = true;
                //    BudgetCapitalcostLi2.Visible = true;

                //}
                //else
                //{
                //    Li2.Visible = false;
                //    Budget.Visible = true;
                //    Li1.Visible = true;
                //    BudgetLi2.Visible = true;
                //    BudgetScreenLi2.Visible = true;
                //    BudgetCapitalcostLi2.Visible = true;
                //}

                RoleRightsBL objRoleRightsBL = new RoleRightsBL();
                    ApplicationResult objResults = new ApplicationResult();

                    DataTable dtRights = new DataTable();
                    int flagMaster = 0;
                    int flagTimeTable = 0;
                   
                    int flagPayroll = 0;
                    int flagFees = 0;
                    int flagBudget = 0;
                    int flagAccounting = 0;
                    int flagReport = 0;
                    int flagStudent = 0;
                    int flagAttendance = 0;
                    int flagResult = 0;
                    int flag = 0;
                    int flagVersion = 0;


                objResults = objRoleRightsBL.RoleRights_T_For_Authorisation(Convert.ToInt32(Session[ApplicationSession.ROLEID].ToString()), Convert.ToInt32(Session[ApplicationSession.TRUSTID].ToString()), Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString()));
                    if (objResults != null)
                    {
                        dtRights = objResults.resultDT;
                        for (int i = 0; i < dtRights.Rows.Count; i++)
                        {
                            #region Menu Hide
                            Control MyList = FindControl("cssmenu");
                            foreach (Control MyControl in MyList.Controls)
                            {
                                if (MyControl is HtmlGenericControl)
                                {
                                    HtmlGenericControl li = MyControl as HtmlGenericControl;

                                    if (li.ID == dtRights.Rows[i]["DisplayName"].ToString())
                                    {
                                        li.Visible = true;
                                        break;
                                    }
                                }
                            }

                        //For Masterli
                        if (dtRights.Rows[i]["DisplayName"].ToString() == "SchoolDepartment")
                            flagMaster = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "SchoolDesignation")
                            flagMaster = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "Section")
                            flagMaster = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "Class")
                            flagMaster = 1;
                        if (dtRights.Rows[i]["DisplayName"].ToString() == "SchoolEmployee")
                            flagMaster = 1;
                        //else if (dtRights.Rows[i]["DisplayName"].ToString() == "DisplayPriority")
                        //    flagMaster = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "ApplyLeave")
                            flagMaster = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "ApplyDutyLeave")
                            flagMaster = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "AdmissionForm")
                            flagMaster = 1;

                        //Attendance
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "StudentAttendanceConsolidated")
                            flagAttendance = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "StudentAttendence")
                            flagAttendance = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "ApproveLeave")
                            flagAttendance = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "ApproveDutyLeave")
                            flagAttendance = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "Workingdays")
                            flagAttendance = 1;

                        //For TimeTable
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "Subject")
                            flagTimeTable = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "Period")
                            flagTimeTable = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "SubjectAssociation")
                            flagTimeTable = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "TimeTable")
                            flagTimeTable = 1;

                        // For Student
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "Registration")
                            flagStudent = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "Upgradation")
                            flagStudent = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "DivisionTransfer")
                            flagStudent = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "PastEducationDetail")
                            flagStudent = 1;

                        //For Fees
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "FeesCategory")
                            flagFees = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "FeesGroup")
                            flagFees = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "ClassFeesTemplate")
                            flagFees = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "StudentFeesTemplate")
                            flagFees = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "FeesCollection")
                            flagFees = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "FeesCancellation")
                            flagFees = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "ClassWiseStudentTemplate")
                            flagFees = 1;

                        //For PayRoll
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "LeaveMaser")
                            flagPayroll = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "PayItem")
                            flagPayroll = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "TrustTemplate")
                            flagPayroll = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "TrustPayItem")
                            flagPayroll = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "EmployeePayItem")
                            flagPayroll = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "GeneratePayslip")
                            flagPayroll = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "ProcessPayroll")
                            flagPayroll = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "PayRollReport")
                            flagPayroll = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "EmployeeTDS")
                            flagPayroll = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "LeaveTemplate")
                            flagPayroll = 1;

                        //For Accounting
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "SchoolAccountLogin")
                            flagAccounting = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "SchoolAccountGroup")
                            flagAccounting = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "SchoolSerialNo")
                            flagAccounting = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "SchoolGeneralLedger")
                            flagAccounting = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "SchoolJournalEntry")
                            flagAccounting = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "SchoolContraEntry")
                            flagAccounting = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "SchoolReceipts")
                            flagAccounting = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "SchoolPayments")
                            flagAccounting = 1;

                        //For Reports
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "SchoolGeneralReports")
                            flagReport = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "SchoolPayrollReports")
                            flagReport = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "SchoolStudentReports")
                            flagReport = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "SchoolStudentReports")
                            flagReport = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "SchoolFeesReports")
                            flagReport = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "SchoolInventoryReports")
                            flagReport = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "SchoolAccountingReports")
                            flagReport = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "SchoolStatutoryReports")
                            flagReport = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "SchoolTimeTableReports")
                            flagReport = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "DEOReport")
                            flagReport = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "ExamResultReport")
                            flagReport = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "ResultReport")
                            flagResult = 1;


                        //For Result
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "ResultCreation")
                            flagResult = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "Exam")
                            flagResult = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "ClassExamAssociation")
                            flagResult = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "ClassSubjectAssociation")
                            flagResult = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "StudentSubjectAssociation")
                            flagResult = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "Grade")
                            flagResult = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "ExamConfiguration")
                            flagResult = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "FinalResult")
                            flagResult = 1;
                        
                        //Budget
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "BudgetHeading")
                            flagBudget = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "BudgetSubHeading")
                            flagBudget = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "BudgetScreenEntry")
                            flagBudget = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "BudgetCapitalCost")
                            flagBudget = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "BudgetAdjustment")
                            flagBudget = 1;


                        //For Importli
                        if (dtRights.Rows[i]["DisplayName"].ToString() == "EmployeeImport")
                            flagMaster = 1;
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "StudentsImport")
                            flagMaster = 1;
                       
                        // For Version Control
                        else if (dtRights.Rows[i]["DisplayName"].ToString() == "Versioncontrol")
                            flagVersion = 1;
                            #endregion

                        }
                        if (sRet != "NotAuthorized.aspx")
                        {
                            for (int j = 0; j < dtRights.Rows.Count; j++)
                            {
                                #region Not Authorized

                                if (sRet == "Home.aspx")
                                {
                                    flag = 0;
                                    break;
                                }
                                if (dtRights.Rows[j]["ScreenName"].ToString() == sRet)
                                {
                                    flag = 0;
                                    break;
                                }
                                //else
                                //{
                                //    flag = 1;
                                //}

                                #endregion
                            }
                        }
                        if (flagMaster == 1)
                        {
                            Master.Visible = true;
                        }
                        else
                        {
                            Master.Visible = false;
                        }

                        if (flagAttendance == 1)
                        {
                            SchoolAttendanceA.Visible = true;                      
                            ApproveDutyLeave.Visible = true;
                        }
                        else
                        {
                            SchoolAttendanceA.Visible = false;
                            ApproveDutyLeave.Visible = false;
                        }
                        if (flagPayroll == 1)
                        {
                            Payroll.Visible = true;
                        }
                        else
                        {
                            Payroll.Visible = false;
                        }
                        if (flagReport == 1)
                        {
                            Report.Visible = true;
                        }
                        else
                        {
                            Report.Visible = false;
                        }

                    if (flagTimeTable == 1)
                    {
                        TimeTablea.Visible = true;
                    }
                    else
                    {
                        TimeTablea.Visible = false;
                    }
                    if (flagStudent == 1)
                    {
                        Student.Visible = true;
                    }
                    else
                    {
                        Student.Visible = false;
                    }


                    //if (flagVersion == 1)
                    //{
                    //    Versioncontrol.Visible = true;
                    //}
                    //else
                    //{
                    //    Versioncontrol.Visible = false;
                    //}
                    if (flagFees == 1)
                    {
                        Fees.Visible = true;
                    }
                    else
                    {
                        Fees.Visible = false;
                    }
                    if (flagBudget == 1)
                        {
                            Budget.Visible = true;
                        }
                        else
                        {
                            Budget.Visible = false;
                        }
                        if (flagAccounting == 1)
                        {
                            Accounting.Visible = true;
                        }
                        else
                        {
                            Accounting.Visible = false;
                        }
                       
                        if (flagResult == 1)
                        {
                            Result.Visible = true;
                        }
                        else
                        {
                            Result.Visible = false;
                        }
                    }
                    if (!Page.IsPostBack)
                    {
                        FetchImage();
                    }
                }
                else
                {
                    Response.Redirect("../UserLogin.aspx");
                }
            }
#endregion

        #region Logout Button
        protected void imbbtnLogout_Click(object sender, ImageClickEventArgs e)
		{
			Session.Clear();
			Response.Redirect("../UserLogin.aspx", false);
		}
		#endregion

        #region Fetch Image
        public void FetchImage()
        {
            try
            {
                #region Declaretion
                SchoolBL objSchoolBl = new SchoolBL();
                DataTable dtSchool = new DataTable();
                #endregion
                ApplicationResult objResultsEdit = new ApplicationResult();
                objResultsEdit = objSchoolBl.School_Select(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));

                if (objResultsEdit != null)
                {
                    dtSchool = objResultsEdit.resultDT;
                    if (dtSchool.Rows.Count > 0)
                    {
                       
                            ViewState["Bytes"] = dtSchool.Rows[0][SchoolBO.SCHOOL_SCHOOLLOGO];
                            if (ViewState["Bytes"].ToString() != "")
                            {
                                imgphoto.ImageUrl = "../Client.UI/GetImage.ashx?SchoolMID=" + Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);

                            }
                            ViewState["Bytes"] = null;
                    }
                }
            }
            catch (Exception ex)
            {
                //DisplayErrorMsg("CommonError", ex);
            }
        }
        #endregion

    }
}