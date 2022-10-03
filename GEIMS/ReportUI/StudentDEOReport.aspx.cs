using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.DataAccess;
using System.Data;
using System.Globalization;

namespace GEIMS.ReportUI
{
    public partial class StudentDEOReport : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(StudentDEOReport));

        #region PageLoad Event
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
                    divReport.Visible = false;
                    hfTrustMID.Value = Session[ApplicationSession.TRUSTID].ToString();
                    hfSchoolMID.Value = Session[ApplicationSession.SCHOOLID].ToString();
                    divLeft.Visible = false;
                    divTransfer.Visible = false;
                    divAttempt.Visible = false;
                    BindMonth();
                    BindYear();
                    divAttemptEnglish.Visible = false;
                    divAttemptGujarati.Visible = false;
                    btnGo.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Back Button Event
        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Print Button Event
        protected void btnPrintDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(ddlType.SelectedValue) == 1 && chkEngish.Checked == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('divBonafideEnglishPrint');", true);
                }
                else if (Convert.ToInt32(ddlType.SelectedValue) == 1 && chkEngish.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('divBonafideGujaratiPrint');", true);
                }
                else if (Convert.ToInt32(ddlType.SelectedValue) == 2 && chkEngish.Checked == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('divLeavingEngPrint');", true);
                }
                else if (Convert.ToInt32(ddlType.SelectedValue) == 2 && chkEngish.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('divLeavingGujPrint');", true);
                }
                else if (Convert.ToInt32(ddlType.SelectedValue) == 4 && chkEngish.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('divTransferGujaratiPrint');", true);
                }
                else if (Convert.ToInt32(ddlType.SelectedValue) == 3 && chkEngish.Checked == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('divAttemptEngPrint');", true);
                }
                else if (Convert.ToInt32(ddlType.SelectedValue) == 3 && chkEngish.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('divAttemptGujPrint');", true);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Bind Month
        private void BindMonth()
        {
            DateTimeFormatInfo DtInfo = DateTimeFormatInfo.GetInstance(null);
            for (int i = 1; i < 13; i++)
            {
                ddlMonth.Items.Add(new ListItem(DtInfo.GetMonthName(i), i.ToString()));
            }
        }
        #endregion

        #region Bind Year
        private void BindYear()
        {
            int currentYear = DateTime.Now.Year;
            for (int i = 2000; i <= currentYear; i++)
            {
                ddlYear.Items.Add(i.ToString());
            }

        }
        #endregion

        #region Bind GridView
        public void BindgvReport()
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }
            if (hfStudentMID.Value != "")
            {
                int intIsEnglish = 0;
                if (chkEngish.Checked == true)
                {
                    intIsEnglish = 1;

                    StudentBL objStudentBl = new StudentBL();
                    ApplicationResult objResult = new ApplicationResult();
                    if (Convert.ToInt32(ddlType.SelectedValue) == 1)
                    {
                        objResult = objStudentBl.Select_Student_ForDeoReports(Convert.ToInt32((Session[ApplicationSession.TRUSTID]).ToString()), Convert.ToInt32((Session[ApplicationSession.SCHOOLID]).ToString()),
                            Convert.ToInt32(hfStudentMID.Value), Convert.ToInt32(ddlType.SelectedValue), intIsEnglish);
                        if (objResult != null)
                        {
                            dlBonafideEnglish.DataSource = null;
                            dlBonafideEnglish1.DataSource = null;

                            if (objResult.resultDT.Rows.Count > 0)
                            {
                                divReport.Visible = true;
                                divBonafideEnglish.Visible = true;
                                divBonafideGujarati.Visible = false;
                                dlBonafideEnglish.Visible = true;
                                dlBonafideEnglish1.Visible = true;
                                divLeavingEnglish.Visible = false;
                                divLeavingGujarati.Visible = false;
                                divTransferGujarati.Visible = false;
                            }
                            else
                            {
                                divReport.Visible = false;
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No record found.');", true);
                                dlBonafideEnglish.Visible = false;
                                dlBonafideEnglish1.Visible = false;
                                divBonafideEnglish.Visible = false;
                                divBonafideGujarati.Visible = false;
                                divLeavingEnglish.Visible = false;
                                divLeavingGujarati.Visible = false;
                                divTransferGujarati.Visible = false;
                                ClearAll();
                            }
                        }

                        dlBonafideEnglish.DataSource = objResult.resultDT;
                        dlBonafideEnglish1.DataSource = objResult.resultDT;
                        dlBonafideEnglish.DataBind();
                        dlBonafideEnglish1.DataBind();
                    }
                    else if (Convert.ToInt32(ddlType.SelectedValue) == 2)
                    {
                        objResult = objStudentBl.Select_Student_ForDeoReports(Convert.ToInt32((Session[ApplicationSession.TRUSTID]).ToString()), Convert.ToInt32(hfSchoolMID.Value),
                            Convert.ToInt32(hfStudentMID.Value), Convert.ToInt32(ddlType.SelectedValue), intIsEnglish);
                        if (objResult != null)
                        {
                            dlLeavingEnglish1.DataSource = null;
                            dlLeavingEnglish.DataSource = null;

                            if (objResult.resultDT.Rows.Count > 0)
                            {
                                divReport.Visible = true;
                                dlLeavingEnglish.Visible = true;
                                dlLeavingEnglish1.Visible = true;
                                divBonafideEnglish.Visible = false;
                                divBonafideGujarati.Visible = false;
                                divLeavingEnglish.Visible = true;
                                divLeavingGujarati.Visible = false;
                                divTransferGujarati.Visible = false;
                            }
                            else
                            {
                                divReport.Visible = false;
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No record found.');", true);
                                dlLeavingEnglish.Visible = false;
                                dlLeavingEnglish1.Visible = false;
                                divBonafideEnglish.Visible = false;
                                divBonafideGujarati.Visible = false;
                                divLeavingEnglish.Visible = false;
                                divLeavingGujarati.Visible = false;
                                divTransferGujarati.Visible = false;
                                ClearAll();
                            }
                        }

                        dlLeavingEnglish.DataSource = objResult.resultDT;
                        dlLeavingEnglish1.DataSource = objResult.resultDT;
                        dlLeavingEnglish.DataBind();
                        dlLeavingEnglish1.DataBind();
                    }
                    else if (Convert.ToInt32(ddlType.SelectedValue) == 3)
                    {

                        StudentAttemptBL objStudentAttemptBL = new StudentAttemptBL();
                        StudentAttemptBO objStudentAttemptBO = new StudentAttemptBO();
                        ApplicationResult objreResultInsertAttempt = new ApplicationResult();
                        ApplicationResult objreResultStudentMID = new ApplicationResult();



                        objStudentAttemptBO.Attempt = txtAttempt.Text;
                        objStudentAttemptBO.IsAttempt = 1;
                        objStudentAttemptBO.Month = ddlMonth.SelectedItem.Text;
                        objStudentAttemptBO.Year = ddlYear.SelectedItem.Text;
                        objStudentAttemptBO.Percent = txtPercent.Text;
                        objStudentAttemptBO.StudentMID = Convert.ToInt32(hfStudentMID.Value);
                        objStudentAttemptBO.SeatNo = txtSeatNo.Text;

                        objreResultStudentMID = objStudentAttemptBL.StudentAttempt_Select(objStudentAttemptBO.StudentMID);
                        if (objreResultStudentMID.resultDT.Rows.Count > 0)
                        {
                            goto FetchDataBase;
                        }
                        objreResultInsertAttempt = objStudentAttemptBL.StudentAttempt_Insert(objStudentAttemptBO);
                        if (objreResultInsertAttempt != null)
                        {

                        }
                    FetchDataBase:
                        objResult = objStudentBl.Select_Student_ForDeoReports(Convert.ToInt32((Session[ApplicationSession.TRUSTID]).ToString()), Convert.ToInt32((Session[ApplicationSession.SCHOOLID]).ToString()),
                            Convert.ToInt32(hfStudentMID.Value), Convert.ToInt32(ddlType.SelectedValue), intIsEnglish);
                        if (objResult != null)
                        {
                            dlAttemptEnglish.DataSource = null;
                            dlAttemptEnglish1.DataSource = null;

                            if (objResult.resultDT.Rows.Count > 0)
                            {
                                divReport.Visible = true;
                                dlAttemptEnglish.Visible = true;
                                dlAttemptEnglish1.Visible = true;
                                divBonafideEnglish.Visible = false;
                                divBonafideGujarati.Visible = false;
                                divAttemptEnglish.Visible = true;
                                divLeavingGujarati.Visible = false;
                                divTransferGujarati.Visible = false;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No record found.');", true);
                                dlLeavingEnglish.Visible = false;
                                dlAttemptEnglish.Visible = false;
                                dlAttemptEnglish1.Visible = false;
                                dlLeavingEnglish1.Visible = false;
                                divAttemptEnglish.Visible = false;
                                divBonafideEnglish.Visible = false;
                                divBonafideGujarati.Visible = false;
                                divLeavingEnglish.Visible = false;
                                divLeavingGujarati.Visible = false;
                                divTransferGujarati.Visible = false;
                                ClearAll();
                            }
                        }

                        dlAttemptEnglish.DataSource = objResult.resultDT;
                        dlAttemptEnglish1.DataSource = objResult.resultDT;
                        dlAttemptEnglish.DataBind();
                        dlAttemptEnglish1.DataBind();
                        divAttempt.Visible = false;
                        btnGo.Enabled = false;
                    }
                }
                else
                {
                    intIsEnglish = 0;

                    StudentBL objStudentBl = new StudentBL();
                    ApplicationResult objResult = new ApplicationResult();
                    if (Convert.ToInt32(ddlType.SelectedValue) == 1)
                    {
                        objResult = objStudentBl.Select_Student_ForDeoReports(Convert.ToInt32((Session[ApplicationSession.TRUSTID]).ToString()), Convert.ToInt32((Session[ApplicationSession.SCHOOLID]).ToString()),
                            Convert.ToInt32(hfStudentMID.Value), Convert.ToInt32(ddlType.SelectedValue), intIsEnglish);
                        if (objResult != null)
                        {
                            dlBonafideGujarati.DataSource = null;
                            dlBonafideGujarati1.DataSource = null;

                            if (objResult.resultDT.Rows.Count > 0)
                            {
                                divReport.Visible = true;
                                dlBonafideGujarati.Visible = true;
                                dlBonafideGujarati1.Visible = true;
                                divBonafideEnglish.Visible = false;
                                divBonafideGujarati.Visible = true;
                                divLeavingEnglish.Visible = false;
                                divLeavingGujarati.Visible = false;
                                divTransferGujarati.Visible = false;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No record found.');", true);
                                dlBonafideGujarati.Visible = false;
                                dlBonafideGujarati1.Visible = false;
                                divBonafideEnglish.Visible = false;
                                divBonafideGujarati.Visible = false;
                                divLeavingEnglish.Visible = false;
                                divLeavingGujarati.Visible = false;
                                divTransferGujarati.Visible = false;
                                ClearAll();
                            }
                        }

                        dlBonafideGujarati.DataSource = objResult.resultDT;
                        dlBonafideGujarati1.DataSource = objResult.resultDT;
                        dlBonafideGujarati.DataBind();
                        dlBonafideGujarati1.DataBind();
                    }
                    else if (Convert.ToInt32(ddlType.SelectedValue) == 2)
                    {
                        objResult = objStudentBl.Select_Student_ForDeoReports(Convert.ToInt32((Session[ApplicationSession.TRUSTID]).ToString()), Convert.ToInt32((Session[ApplicationSession.SCHOOLID]).ToString()),
                            Convert.ToInt32(hfStudentMID.Value), Convert.ToInt32(ddlType.SelectedValue), intIsEnglish);
                        if (objResult != null)
                        {
                            dlLeavingGujarati.DataSource = null;
                            dlLeavingGujarati1.DataSource = null;

                            if (objResult.resultDT.Rows.Count > 0)
                            {
                                divReport.Visible = true;
                                dlLeavingGujarati.Visible = true;
                                dlLeavingGujarati1.Visible = true;
                                divBonafideEnglish.Visible = false;
                                divBonafideGujarati.Visible = false;
                                divLeavingEnglish.Visible = false;
                                divLeavingGujarati.Visible = true;
                                divTransferGujarati.Visible = false;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No record found.');", true);
                                dlLeavingGujarati.Visible = false;
                                dlLeavingGujarati1.Visible = false;
                                divBonafideEnglish.Visible = false;
                                divBonafideGujarati.Visible = false;
                                divLeavingEnglish.Visible = false;
                                divLeavingGujarati.Visible = false;
                                divTransferGujarati.Visible = false;
                                ClearAll();
                            }
                        }

                        dlLeavingGujarati.DataSource = objResult.resultDT;
                        dlLeavingGujarati1.DataSource = objResult.resultDT;
                        dlLeavingGujarati.DataBind();
                        dlLeavingGujarati1.DataBind();
                    }
                    else if (Convert.ToInt32(ddlType.SelectedValue) == 3)
                    {
                        StudentAttemptBL objStudentAttemptBL = new StudentAttemptBL();
                        StudentAttemptBO objStudentAttemptBO = new StudentAttemptBO();
                        ApplicationResult objreResultInsertAttempt = new ApplicationResult();
                        ApplicationResult objreResultStudentMID = new ApplicationResult();

                        objStudentAttemptBO.Attempt = txtAttempt.Text;
                        objStudentAttemptBO.IsAttempt = 1;
                        objStudentAttemptBO.Month = ddlMonth.SelectedItem.Text;
                        objStudentAttemptBO.Year = ddlYear.SelectedItem.Text;
                        objStudentAttemptBO.Percent = txtPercent.Text;
                        objStudentAttemptBO.StudentMID = Convert.ToInt32(hfStudentMID.Value);
                        objStudentAttemptBO.SeatNo = txtSeatNo.Text;

                        objreResultStudentMID = objStudentAttemptBL.StudentAttempt_Select(objStudentAttemptBO.StudentMID);
                        if (objreResultStudentMID.resultDT.Rows.Count > 0)
                        {
                            goto FetchDataBase1;
                        }

                        objreResultInsertAttempt = objStudentAttemptBL.StudentAttempt_Insert(objStudentAttemptBO);
                        if (objreResultInsertAttempt != null)
                        {

                        }

                    FetchDataBase1:

                        objResult = objStudentBl.Select_Student_ForDeoReports(Convert.ToInt32((Session[ApplicationSession.TRUSTID]).ToString()), Convert.ToInt32((Session[ApplicationSession.SCHOOLID]).ToString()),
                            Convert.ToInt32(hfStudentMID.Value), Convert.ToInt32(ddlType.SelectedValue), intIsEnglish);
                        if (objResult != null)
                        {
                            dlAttemptGujarati.DataSource = null;
                            dlAttemptGujarati1.DataSource = null;

                            if (objResult.resultDT.Rows.Count > 0)
                            {
                                divReport.Visible = true;
                                dlAttemptGujarati.Visible = true;
                                dlAttemptGujarati1.Visible = true;
                                divAttemptGujarati.Visible = true;
                                divBonafideEnglish.Visible = false;
                                divBonafideGujarati.Visible = false;
                                divLeavingEnglish.Visible = false;
                                divAttemptGujPrint.Visible = true;
                                divTransferGujarati.Visible = false;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No record found.');", true);
                                dlAttemptGujarati.Visible = false;
                                divAttemptGujPrint.Visible = false;
                                dlAttemptGujarati1.Visible = false;
                                dlLeavingGujarati.Visible = false;
                                dlLeavingGujarati1.Visible = false;
                                divBonafideEnglish.Visible = false;
                                divBonafideGujarati.Visible = false;
                                divLeavingEnglish.Visible = false;
                                divLeavingGujarati.Visible = false;
                                divTransferGujarati.Visible = false;
                                ClearAll();
                            }
                        }

                        dlAttemptGujarati.DataSource = objResult.resultDT;
                        dlAttemptGujarati1.DataSource = objResult.resultDT;
                        dlAttemptGujarati.DataBind();
                        dlAttemptGujarati1.DataBind();
                        divAttempt.Visible = false;
                        btnGo.Enabled = false;
                    }
                    else if (Convert.ToInt32(ddlType.SelectedValue) == 4)
                    {
                        objResult = objStudentBl.Select_Student_ForDeoReports(Convert.ToInt32((Session[ApplicationSession.TRUSTID]).ToString()), Convert.ToInt32((Session[ApplicationSession.SCHOOLID]).ToString()),
                            Convert.ToInt32(hfStudentMID.Value), 2, intIsEnglish);
                        if (objResult != null)
                        {
                            dlTransferGujarati.DataSource = null;
                            dlTransferGujarati1.DataSource = null;

                            if (objResult.resultDT.Rows.Count > 0)
                            {
                                divReport.Visible = true;
                                dlTransferGujarati.Visible = true;
                                dlTransferGujarati1.Visible = true;
                                divBonafideEnglish.Visible = false;
                                divBonafideGujarati.Visible = false;
                                divLeavingEnglish.Visible = false;
                                divLeavingGujarati.Visible = false;
                                divTransferGujarati.Visible = true;
                            }
                            else
                            {
                                dlTransferGujarati.Visible = false;
                                dlTransferGujarati1.Visible = false;
                                divBonafideEnglish.Visible = false;
                                divBonafideGujarati.Visible = false;
                                divLeavingEnglish.Visible = false;
                                divLeavingGujarati.Visible = false;
                                divTransferGujarati.Visible = false;
                                ClearAll();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No record found.');", true);
                                //Response.Redirect("StudentDEOReport.aspx");
                            }
                        }

                        dlTransferGujarati.DataSource = objResult.resultDT;
                        dlTransferGujarati1.DataSource = objResult.resultDT;
                        dlTransferGujarati.DataBind();
                        dlTransferGujarati1.DataBind();
                    }
                }
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No record found.');", true);
                dlLeavingEnglish.Visible = false;
                dlLeavingEnglish1.Visible = false;
                ClearAll();
            }
        }
        #endregion

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            divReport.Visible = false;
            pnlStudentInfo.Visible = true;
            divLeft.Visible = false;
            divTransfer.Visible = false;
        }
        #endregion

        #region Go button Event
        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                //fetch Isleaving Detail

                //
                if (hfStudentMID.Value == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No record found.');", true);
                    dlLeavingEnglish.Visible = false;
                    dlLeavingEnglish1.Visible = false;
                    ClearAll();
                }
                else if (txtStudentName.Text != "" && ddlType.SelectedValue != "-1")
                {
                    BindgvReport();

                    if (ddlType.SelectedValue == "2")
                    {
                        StudentBL objStudent = new StudentBL();
                        ApplicationResult objResults = new ApplicationResult();
                        if (chkEngish.Checked == true)
                        {
                            int intEnglish = 1;
                            int intSubmitted = 1;
                            if (chkSubmitted.Checked == true)
                            {

                                objResults =
                                  objStudent.StudentM_UpdateForLeavingCerti(
                                      Convert.ToInt32(hfStudentMID.Value),
                                      1, txtDate.Text, 0, DateTime.UtcNow.AddHours(5.5).ToString(),
                                      Convert.ToInt32(Session[ApplicationSession.USERID]), intEnglish);
                                if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                                {

                                }
                            }
                            else
                            {
                                objResults =
                                  objStudent.StudentM_UpdateForLeavingCerti(
                                      Convert.ToInt32(hfStudentMID.Value),
                                      0, txtDate.Text, 0, DateTime.UtcNow.AddHours(5.5).ToString(),
                                      Convert.ToInt32(Session[ApplicationSession.USERID]), intEnglish);
                                if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                                {

                                }
                            }

                        }
                        else
                        {
                            int intEnglish = 0;
                            if (chkSubmitted.Checked == true)
                            {
                                objResults =
                                  objStudent.StudentM_UpdateForLeavingCerti(
                                      Convert.ToInt32(hfStudentMID.Value),
                                      0, txtDate.Text, 1, DateTime.UtcNow.AddHours(5.5).ToString(),
                                      Convert.ToInt32(Session[ApplicationSession.USERID]), intEnglish);
                                if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                                {

                                }
                            }
                            else
                            {
                                objResults =
                                  objStudent.StudentM_UpdateForLeavingCerti(
                                      Convert.ToInt32(hfStudentMID.Value),
                                      0, txtDate.Text, 0, DateTime.UtcNow.AddHours(5.5).ToString(),
                                      Convert.ToInt32(Session[ApplicationSession.USERID]), intEnglish);
                                if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                                {

                                }
                            }
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Select Proper Name or type.');", true);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Get Student Web Service Method
        [System.Web.Services.WebMethod]
        public static string[] GetAllStudentNameForReport(string prefixText, int TrustMID, int SchoolMID)
        {
            StudentBL objStudentbl = new StudentBL();
            ApplicationResult objResult = new ApplicationResult();
            string strSearchText = prefixText + "%";
            List<string> result = new List<string>();
            objResult = objStudentbl.Student_Autocomplete_ForReport(strSearchText, TrustMID, SchoolMID);
            if (objResult != null)
            {
                for (int i = 0; i < objResult.resultDT.Rows.Count; i++)
                {
                    string strStudentCodeName = objResult.resultDT.Rows[i]["StudentName"].ToString();
                    string strStudentMID = objResult.resultDT.Rows[i][StudentBO.STUDENT_STUDENTMID].ToString();
                    string strSchoolMID = objResult.resultDT.Rows[i]["SchoolMID"].ToString();
                    result.Add(string.Format("{0}~{1}~{2}", strStudentCodeName, strStudentMID, strSchoolMID));
                }
            }
            return result.ToArray();
        }

        #endregion

        #region Datalist Item Bound Event

        protected void dlBonafideEnglish_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lblBirthDateInWords = (Label)e.Item.FindControl("lblBirthDateInWords");
                    if (DataBinder.Eval(e.Item.DataItem, "BirthDate").ToString() != "")
                    {
                        CommonFunctions objFuction = new CommonFunctions();
                        lblBirthDateInWords.Text = objFuction.DateToText(Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "BirthDate").ToString()), false, true);
                    }
                    else
                    {
                        lblBirthDateInWords.Text = "-";
                    }
                    Label lblDate = (Label)e.Item.FindControl("lblDate");
                    lblDate.Text = DateTime.UtcNow.AddHours(5.5).ToShortDateString();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        protected void dlBonafideEnglish1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lblBirthDateInWords = (Label)e.Item.FindControl("lblBirthDateInWords1");
                    if (DataBinder.Eval(e.Item.DataItem, "BirthDate").ToString() != "")
                    {
                        CommonFunctions objFuction = new CommonFunctions();
                        lblBirthDateInWords.Text = objFuction.DateToText(Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "BirthDate").ToString()), false, true);
                    }
                    else
                    {
                        lblBirthDateInWords.Text = "-";
                    }
                    Label lblDate = (Label)e.Item.FindControl("lblDate1");
                    lblDate.Text = DateTime.UtcNow.AddHours(5.5).ToShortDateString();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        protected void dlBonafideGujarati_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lblDate = (Label)e.Item.FindControl("lblDateGuj");
                    lblDate.Text = DateTime.UtcNow.AddHours(5.5).ToShortDateString();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        protected void dlBonafideGujarati1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lblDate = (Label)e.Item.FindControl("lblDateGuj1");
                    lblDate.Text = DateTime.UtcNow.AddHours(5.5).ToShortDateString();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }


        protected void dlLeavingEnglish_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                Label lblFeesStatus = (Label)e.Item.FindControl("lblFeesStatus");
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lblBirthDateInWords = (Label)e.Item.FindControl("lblDateInWords");
                    if (DataBinder.Eval(e.Item.DataItem, "BirthDate").ToString() != "")
                    {
                        CommonFunctions objFuction = new CommonFunctions();
                        lblBirthDateInWords.Text = objFuction.DateToText(Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "BirthDate").ToString()), false, true);
                    }
                    else
                    {
                        lblBirthDateInWords.Text = "";
                    }
                    Label lblDate = (Label)e.Item.FindControl("lblDateForLeaving");
                    lblDate.Text = DateTime.UtcNow.AddHours(5.5).ToShortDateString();
                    FeesCollectionBL objFeeCollectionBL = new FeesCollectionBL();
                    ApplicationResult objResults = new ApplicationResult();
                    objResults = objFeeCollectionBL.Fee_Collection_WithOptionalAndCompulsoryFees(Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "StudentMID").ToString()));
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {

                            lblFeesStatus.Text = "Pending";
                        }
                        else
                        {
                            ClassWiseFeesTemplateTBL objClassWiseFeesTemplate = new ClassWiseFeesTemplateTBL();
                            ApplicationResult objResultsClass = new ApplicationResult();
                            objResultsClass = objClassWiseFeesTemplate.ClassWiseFeesTemplateT_Select_ForReport(Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "SchoolMID").ToString()), Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "ClassMID").ToString()), Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "DivisionTID").ToString()), DataBinder.Eval(e.Item.DataItem, "Year").ToString());
                            if (objResults != null)
                            {
                                if (objResultsClass.resultDT.Rows.Count > 0)
                                {
                                    lblFeesStatus.Text = "Not Applicable";
                                }
                                else
                                {
                                    lblFeesStatus.Text = "Paid";
                                }
                            }

                        }
                    }
                    Label lblDuplicate = (Label)e.Item.FindControl("lblDuplicateEng");
                    if (Convert.ToInt32(ViewState["LeavingEng"].ToString()) == 0)
                    {
                        lblDuplicate.Text = "";
                    }
                    else
                    {
                        lblDuplicate.Text = "(Duplicate)";
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        protected void dlLeavingEnglish1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                Label lblFeesStatus = (Label)e.Item.FindControl("lblFeesStatus1");
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lblBirthDateInWords = (Label)e.Item.FindControl("lblDateInWord1");
                    if (DataBinder.Eval(e.Item.DataItem, "BirthDate").ToString() != "")
                    {
                        CommonFunctions objFuction = new CommonFunctions();
                        lblBirthDateInWords.Text = objFuction.DateToText(Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "BirthDate").ToString()), false, true);
                    }
                    else
                    {
                        lblBirthDateInWords.Text = "";
                    }
                    Label lblDate = (Label)e.Item.FindControl("lblDateForLeaving1");
                    lblDate.Text = DateTime.UtcNow.AddHours(5.5).ToShortDateString();
                    FeesCollectionBL objFeeCollectionBL = new FeesCollectionBL();
                    ApplicationResult objResults = new ApplicationResult();
                    objResults = objFeeCollectionBL.Fee_Collection_WithOptionalAndCompulsoryFees(Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "StudentMID").ToString()));
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {

                            lblFeesStatus.Text = "Pending";
                        }
                        else
                        {
                            ClassWiseFeesTemplateTBL objClassWiseFeesTemplate = new ClassWiseFeesTemplateTBL();
                            ApplicationResult objResultsClass = new ApplicationResult();
                            objResultsClass = objClassWiseFeesTemplate.ClassWiseFeesTemplateT_Select_ForReport(Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "SchoolMID").ToString()), Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "ClassMID").ToString()), Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "DivisionTID").ToString()), DataBinder.Eval(e.Item.DataItem, "Year").ToString());
                            if (objResults != null)
                            {
                                if (objResultsClass.resultDT.Rows.Count > 0)
                                {
                                    lblFeesStatus.Text = "Not Applicable";
                                }
                                else
                                {
                                    lblFeesStatus.Text = "Paid";
                                }
                            }

                        }
                    }
                    Label lblDuplicate = (Label)e.Item.FindControl("lblDuplicateEng1");
                    if (Convert.ToInt32(ViewState["LeavingEng"].ToString()) == 0)
                    {
                        lblDuplicate.Text = "";
                    }
                    else
                    {
                        lblDuplicate.Text = "(Duplicate)";
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        protected void dlTransferGujarati_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                Label lblDate = (Label)e.Item.FindControl("lblDateGujTransfer");
                lblDate.Text = DateTime.UtcNow.AddHours(5.5).ToShortDateString();
                Label lblPrincipalName = (Label)e.Item.FindControl("lblPrincipal");
                lblPrincipalName.Text = txtPrincipalName.Text;
                Label lblSchoolDiesCode = (Label)e.Item.FindControl("lblSchoolDiesCode");
                lblSchoolDiesCode.Text = txtDiesCode.Text;
                Label lblKalstar = (Label)e.Item.FindControl("lblKalstar");
                lblKalstar.Text = txtkalstarName.Text;
            }
        }

        protected void dlTransferGujarati1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                Label lblDate = (Label)e.Item.FindControl("lblDateGujTransfer1");
                lblDate.Text = DateTime.UtcNow.AddHours(5.5).ToShortDateString();
                Label lblPrincipalName = (Label)e.Item.FindControl("lblPrincipal1");
                lblPrincipalName.Text = txtPrincipalName.Text;
                Label lblSchoolDiesCode = (Label)e.Item.FindControl("lblSchoolDiesCode1");
                lblSchoolDiesCode.Text = txtDiesCode.Text;
                Label lblKalstar = (Label)e.Item.FindControl("lblKalstar1");
                lblKalstar.Text = txtkalstarName.Text;
            }
        }


        protected void dlLeavingGujarati_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                Label lblFeesStatus = (Label)e.Item.FindControl("lblFeesStatusGuj");
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    //Label lblBirthDateInWords = (Label)e.Item.FindControl("lblDateInWord1");
                    //if (DataBinder.Eval(e.Item.DataItem, "BirthDate").ToString() != "")
                    //{
                    //    CommonFunctions objFuction = new CommonFunctions();
                    //    lblBirthDateInWords.Text = objFuction.DateToText(Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "BirthDate").ToString()), false, true);
                    //}
                    //else
                    //{
                    //    lblBirthDateInWords.Text = "";
                    //}
                    Label lblDate = (Label)e.Item.FindControl("lblDateForLeavingGuj");
                    lblDate.Text = DateTime.UtcNow.AddHours(5.5).ToShortDateString();
                    FeesCollectionBL objFeeCollectionBL = new FeesCollectionBL();
                    ApplicationResult objResults = new ApplicationResult();
                    objResults = objFeeCollectionBL.Fee_Collection_WithOptionalAndCompulsoryFees(Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "StudentMID").ToString()));
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {

                            lblFeesStatus.Text = "બાકી.";
                        }
                        else
                        {
                            ClassWiseFeesTemplateTBL objClassWiseFeesTemplate = new ClassWiseFeesTemplateTBL();
                            ApplicationResult objResultsClass = new ApplicationResult();
                            objResultsClass = objClassWiseFeesTemplate.ClassWiseFeesTemplateT_Select_ForReport(Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "SchoolMID").ToString()), Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "ClassMID").ToString()), Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "DivisionTID").ToString()), DataBinder.Eval(e.Item.DataItem, "Year").ToString());
                            if (objResults != null)
                            {
                                if (objResultsClass.resultDT.Rows.Count > 0)
                                {
                                    lblFeesStatus.Text = "લાગુ નથી.";
                                }
                                else
                                {
                                    lblFeesStatus.Text = "ચૂકવેલ છે.";
                                }
                            }

                        }
                    }
                    Label lblDuplicate = (Label)e.Item.FindControl("lblDuplicateGuj");
                    if (Convert.ToInt32(ViewState["LeavingGuj"].ToString()) == 0)
                    {
                        lblDuplicate.Text = "";
                    }
                    else
                    {
                        lblDuplicate.Text = "(પ્રમાણપત્રની નકલ)";
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        protected void dlLeavingGujarati1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                Label lblFeesStatus = (Label)e.Item.FindControl("lblFeesStatusGuj1");
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    //Label lblBirthDateInWords = (Label)e.Item.FindControl("lblDateInWord1");
                    //if (DataBinder.Eval(e.Item.DataItem, "BirthDate").ToString() != "")
                    //{
                    //    CommonFunctions objFuction = new CommonFunctions();
                    //    lblBirthDateInWords.Text = objFuction.DateToText(Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "BirthDate").ToString()), false, true);
                    //}
                    //else
                    //{
                    //    lblBirthDateInWords.Text = "";
                    //}
                    Label lblDate = (Label)e.Item.FindControl("lblDateForLeavingGuj1");
                    lblDate.Text = DateTime.UtcNow.AddHours(5.5).ToShortDateString();
                    FeesCollectionBL objFeeCollectionBL = new FeesCollectionBL();
                    ApplicationResult objResults = new ApplicationResult();
                    objResults = objFeeCollectionBL.Fee_Collection_WithOptionalAndCompulsoryFees(Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "StudentMID").ToString()));
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {

                            lblFeesStatus.Text = "બાકી.";
                        }
                        else
                        {
                            ClassWiseFeesTemplateTBL objClassWiseFeesTemplate = new ClassWiseFeesTemplateTBL();
                            ApplicationResult objResultsClass = new ApplicationResult();
                            objResultsClass = objClassWiseFeesTemplate.ClassWiseFeesTemplateT_Select_ForReport(Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "SchoolMID").ToString()), Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "ClassMID").ToString()), Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "DivisionTID").ToString()), DataBinder.Eval(e.Item.DataItem, "Year").ToString());
                            if (objResults != null)
                            {
                                if (objResultsClass.resultDT.Rows.Count > 0)
                                {
                                    lblFeesStatus.Text = "લાગુ નથી.";
                                }
                                else
                                {
                                    lblFeesStatus.Text = "ચૂકવેલ છે.";
                                }
                            }

                        }
                    }
                    Label lblDuplicate = (Label)e.Item.FindControl("lblDuplicateGuj1");
                    if (Convert.ToInt32(ViewState["LeavingGuj"].ToString()) == 0)
                    {
                        lblDuplicate.Text = "";
                    }
                    else
                    {
                        lblDuplicate.Text = "(પ્રમાણપત્રની નકલ)";
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

        #region Changed Index Events
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "2")
            {
                divLeft.Visible = true;
            }
            else
            {
                divLeft.Visible = false;
            }
            if (ddlType.SelectedValue == "4")
            {
                divTransfer.Visible = true;
                chkEngish.Checked = false;
                chkEngish.Enabled = false;
                lblStudentName.Visible = false;
                lblStudentNameGujarati.Visible = true;
            }
            else
            {
                divTransfer.Visible = false;
                chkEngish.Checked = true;
                chkEngish.Enabled = true;
                lblStudentName.Visible = true;
                lblStudentNameGujarati.Visible = false;
            }
            if (ddlType.SelectedValue == "3")
            {
                divAttempt.Visible = true;
                //Panel1.Visible = true;
            }
            else
            {
                //Panel1.Visible = false;
            }
        }


        protected void txtStudentName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                StudentBL objStudentBL = new StudentBL();
                ApplicationResult objResultsStudent = new ApplicationResult();
                if (hfSchoolMID.Value != null && hfSchoolMID.Value != " ")
                {
                    objResultsStudent = objStudentBL.Student_Select(Convert.ToInt32(hfStudentMID.Value), 1);

                    if (objResultsStudent != null)
                    {
                        if (objResultsStudent.resultDT.Rows.Count > 0)
                        {
                            ViewState["LeavingEng"] =
                                objResultsStudent.resultDT.Rows[0][StudentBO.STUDENT_ISLEAVINGCERTI].ToString();
                            ViewState["LeavingGuj"] =
                                objResultsStudent.resultDT.Rows[0][StudentBO.STUDENT_ISLEAVINGGUJARATICERTI].ToString();
                            txtDate.Text = objResultsStudent.resultDT.Rows[0][StudentBO.STUDENT_LCDATE].ToString();
                        }
                    }

                    if (chkEngish.Checked == true)
                    {
                        if (ddlType.SelectedValue == "2")
                        {
                            if (Convert.ToInt32(ViewState["LeavingEng"].ToString()) == 0)
                            {
                                chkSubmitted.Checked = false;
                            }
                            else
                            {
                                chkSubmitted.Checked = true;
                            }
                        }
                    }
                    else
                    {
                        if (ddlType.SelectedValue == "2")
                        {
                            if (Convert.ToInt32(ViewState["LeavingGuj"].ToString()) == 0)
                            {
                                chkSubmitted.Checked = false;
                            }
                            else
                            {
                                chkSubmitted.Checked = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Opps! There is some Technical Error.Please Contact Your Administrator.');</script>");
            }

        }

        protected void chkEngish_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEngish.Checked == true)
            {
                if (ddlType.SelectedValue == "2")
                {
                    if (Convert.ToInt32(ViewState["LeavingEng"].ToString()) == 0)
                    {
                        chkSubmitted.Checked = false;
                    }
                    else
                    {
                        chkSubmitted.Checked = true;
                    }
                }
            }
            else
            {
                if (ddlType.SelectedValue == "2")
                {
                    if ((ViewState["LeavingGuj"].ToString()) != "")
                    {
                        if (Convert.ToInt32(ViewState["LeavingGuj"].ToString()) == 0)
                        {
                            chkSubmitted.Checked = false;
                        }
                        else
                        {
                            chkSubmitted.Checked = true;
                        }
                    }

                }
            }
        }
        #endregion

        #region Report Button Event
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Client.UI/SchoolReports.aspx?Mode=SchoolStudentReports");
        }
        #endregion
    }
}