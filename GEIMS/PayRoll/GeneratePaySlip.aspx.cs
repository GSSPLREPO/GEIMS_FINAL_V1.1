using System;
using System.Data;
using System.Web.UI.WebControls;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using System.Web.UI;
using log4net;
using GEIMS.DataAccess;
using System.Collections.Generic;
using GEIMS.Bl;
using GEIMS.Bo;
using System.Globalization;

namespace GEIMS.PayRoll
{

    public partial class GeneratePaySlip : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(GeneratePaySlip));

        #region Declaration
        int month = 0;
        int year = 0;
        double days;
        static Double Gross = 0;
        TextBox txt = new TextBox();
        Label lbl = new Label();  //date: 22-12-2021  
        string payname = "";
        int LEAVEID = 0;
        #endregion

        #region Page_Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }
            if (!Page.IsPostBack)
            {
                ddlSearchBy.SelectedIndex = 1;
                divForm.Visible = false;
                bindYear();
            }
        }
        #endregion

        #region btnGo Event
        /// <summary>
        /// to display employee grid based on selected name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGo_OnClick(object sender, EventArgs e)
        {
            try
            {
                //Added below code 12/11/2022 Bhandavi
                //When we change name of employee and click on Go button then form of payslip should invisible
                divForm.Visible = false;

                EmployeeMBL objEmployeeBL = new EmployeeMBL();
                EmployeeMBO objEmployeeBO = new EmployeeMBO();

                ApplicationResult objResultProgram = new ApplicationResult();
                objResultProgram = objEmployeeBL.Employee_Search_By_NameAndCode(txtSearchName.Text, Convert.ToInt32(ddlSearchBy.SelectedValue), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResultProgram != null)
                {
                    if (objResultProgram.resultDT.Rows.Count > 0)
                    {
                        gvEmployee.Visible = true;
                        gvEmployee.DataSource = objResultProgram.resultDT;
                        gvEmployee.DataBind();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Record Found.');", true);
                        gvEmployee.Visible = false;
                    }
                   
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion

        #region CalculatePaySlip function
        public void CalculatePayslip()
        {

            UserTemplateTBl objUserTemplateBl = new UserTemplateTBl();
            UserPayItemTemplateTBl objUserPayItemTemplateBl = new UserPayItemTemplateTBl();
            EmployeeMBL objEmployeeBl = new EmployeeMBL();
            LeaveMBl ObjLeaveMBl = new LeaveMBl();
            ApplicationResult objResult = new ApplicationResult();
            ApplicationResult objResultPayslip = new ApplicationResult();
            ApplicationResult objLeaveRequest = new ApplicationResult();
            ApplicationResult objLeaveGenerate = new ApplicationResult();
            ApplicationResult objLeaveGenerateEmployeeMID = new ApplicationResult();
            ApplicationResult objLeaveFinal = new ApplicationResult();
            ApplicationResult objEmployee = new ApplicationResult();
            ApplicationResult objTemplateZero = new ApplicationResult();
            Controls objControl = new Controls();

            divForm.Visible = true;
            Controls objControls = new Controls();
            objControls.EnableControls(Master.FindControl("ContentPlaceHolder1"));

            txtPayAbsenceDay.Text = "0";
            txtTotal.Text = "0";
            
            GDBalance.Enabled = true;
            btnSendForApproval.Enabled = true;

            //Last Payslip Check
            objResult = objUserTemplateBl.UserTemplateT_SelectLeaveID_ByEmployeeMID(Convert.ToInt32(ViewState["EmployeeMID"].ToString()));

            objResultPayslip = objUserPayItemTemplateBl.EmployeeTemplate_SelectEarnDays(Convert.ToInt32(ViewState["EmployeeMID"].ToString()));
            if (objResultPayslip.resultDT.Rows.Count > 0)
            {
                ddlMonth.SelectedIndex = Convert.ToInt32(objResultPayslip.resultDT.Rows[0]["Month"].ToString());
                ddlYear.SelectedValue = objResultPayslip.resultDT.Rows[0]["Year"].ToString();
                txtPayTotalDays.Text = objResultPayslip.resultDT.Rows[0]["TotalDaysofMonth"].ToString();
                txtPayEarnedDays.Text = objResultPayslip.resultDT.Rows[0]["EarnedDaysofMonth"].ToString();

                objLeaveRequest = ObjLeaveMBl.PayRollLeaveRequestM_SelectbyEmployeeMID(Convert.ToInt32(ViewState["EmployeeMID"].ToString()));
                if (objLeaveRequest.resultDT.Rows.Count > 0)
                {
                    BindLeaveBalance(Convert.ToInt32(ViewState["EmployeeMID"].ToString()), ddlMonth.SelectedValue,Convert.ToInt32(ddlYear.SelectedValue));
                    //ABC
                    //GDBalance.DataSource = objLeaveRequest.resultDT;
                    //GDBalance.DataBind();

                   // objLeaveGenerate = ObjLeaveMBl.LeaveGenerate_Select(Convert.ToInt32(ViewState["EmployeeMID"].ToString()));

                    //for (int k = 0; k < objLeaveGenerate.resultDT.Rows.Count; k++)
                    //{

                    //    GDBalance.Rows[k].Cells[3].Text = objLeaveGenerate.resultDT.Rows[k]["LeaveBalance"].ToString();
                    //    GDBalance.Rows[k].Cells[5].Text = GDBalance.Rows[k].Cells[2].Text.ToString();

                    //}
                }
                else
                {
                    month = ddlMonth.SelectedIndex;
                    year = Convert.ToInt32(ddlYear.SelectedValue.ToString());

                    objLeaveFinal = ObjLeaveMBl.UserLeave_M_Select_Final(month, year, Convert.ToInt32(ViewState["EmployeeMID"].ToString()));
                    BindLeaveBalance(Convert.ToInt32(ViewState["EmployeeMID"].ToString()), ddlMonth.SelectedValue,Convert.ToInt32(ddlYear.SelectedValue));
                    //ABC
                    //GDBalance.DataSource = objLeaveFinal.resultDT;
                    //GDBalance.DataBind();

                    //int k = 0;
                    //foreach (GridViewRow rowItem in GDBalance.Rows)
                    //{
                    //    //txt = (TextBox)rowItem.Cells[4].FindControl("textDays");
                    //    //txt.Text = "0";
                    //    // GDBalance.Rows[k].Cells[5].Text = GDBalance.Rows[k].Cells[3].Text.ToString();

                    //    k = k + 1;
                    //}
                }
                if (objResult.resultDT.Rows.Count > 0 && objResult.resultDT.Rows[0]["LastPaySlipGenerated"].ToString() != "0")
                {
                    ViewState["PaySlipID"] = Convert.ToInt32(objResult.resultDT.Rows[0]["LastPaySlipGenerated"].ToString());
                    objResult.resultDT = null;
                    objResult = ObjLeaveMBl.LeaveRequestM_SelectLeaveID(Convert.ToInt32(ViewState["PaySlipID"].ToString()));
                    //ABC
                    //if (objResult.resultDT.Rows.Count > 0)
                    //{
                    //    foreach (GridViewRow rowItem in GDBalance.Rows)
                    //    {
                    //        txt = (TextBox)rowItem.FindControl("textDays");

                    //        // txt.Text = "0";
                    //    }
                    //}
                }
            }
            else
            {
                ddlMonth.SelectedIndex = 0;
                ddlYear.SelectedValue = Convert.ToString(DateTime.Now.Year);
                txtPayTotalDays.Text = "0";
                txtPayEarnedDays.Text = "0";
                objLeaveRequest = ObjLeaveMBl.UserLeaveM_Select_ByTrustMID(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));

                if (objLeaveRequest.resultDT.Rows.Count > 0)
                {
                    BindLeaveBalance(Convert.ToInt32(ViewState["EmployeeMID"].ToString()), ddlMonth.SelectedValue, Convert.ToInt32(ddlYear.SelectedValue));
                    //ABC
                    //GDBalance.DataSource = objLeaveRequest.resultDT;
                    //GDBalance.DataBind();

                    //int k = 0;
                    //foreach (GridViewRow rowItem in GDBalance.Rows)
                    //{
                    //    //txt = (TextBox)rowItem.FindControl("textDays");
                    //    ////txt.Text = "0";
                    //    //  GDBalance.Rows[k].Cells[5].Text = GDBalance.Rows[k].Cells[3].Text.ToString();
                    //    k = k + 1;
                    //}
                }
                else
                {
                    month = ddlMonth.SelectedIndex;
                    year = Convert.ToInt32(ddlYear.SelectedValue.ToString());

                    objLeaveFinal = ObjLeaveMBl.UserLeave_M_Select_Final(month, year, Convert.ToInt32(ViewState["EmployeeMID"].ToString()));
                    BindLeaveBalance(Convert.ToInt32(ViewState["EmployeeMID"].ToString()), ddlMonth.SelectedValue,Convert.ToInt32(ddlYear.SelectedValue));
                    //ABC
                    //GDBalance.DataSource = objLeaveFinal.resultDT;
                    //GDBalance.DataBind();

                    //int k = 0;
                    //foreach (GridViewRow rowItem in GDBalance.Rows)
                    //{
                    //    txt = (TextBox)rowItem.FindControl("textDays");
                    //    // txt.Text = "0";
                    //    //  GDBalance.Rows[k].Cells[5].Text = GDBalance.Rows[k].Cells[3].Text.ToString();

                    //    k = k + 1;
                    //}

                }
                if (objResult.resultDT.Rows.Count > 0 && objResult.resultDT.Rows[0]["LastPaySlipGenerated"].ToString() != "0")
                {
                    ViewState["PaySlipID"] = Convert.ToInt32(objResult.resultDT.Rows[0]["LastPaySlipGenerated"].ToString());
                    objResult.resultDT = null;
                    objResult = ObjLeaveMBl.LeaveRequestM_SelectLeaveID(Convert.ToInt32(ViewState["PaySlipID"].ToString()));
                    //ABC
                    // if (objResult.resultDT.Rows.Count > 0)
                    //{
                    //    foreach (GridViewRow rowItem in GDBalance.Rows)
                    //    {
                    //        txt = (TextBox)rowItem.FindControl("textDays");

                    //        // txt.Text = "0";
                    //    }
                    //}
                }
            }

            //ABC
            //foreach (GridViewRow rowItem in GDBalance.Rows)
            //{
            //    TextBox txt = (TextBox)rowItem.FindControl("textDays");
            //    //txt.Enabled = true;
            //}

            //txtTotal.Enabled = true;

            objEmployee = objEmployeeBl.SelectEmployee_For_ProcessPayRoll(Convert.ToInt32(ViewState["EmployeeMID"].ToString()));

            if (objEmployee.resultDT.Rows.Count > 0)
            {
                txtPayEmpCode.Text = objEmployee.resultDT.Rows[0]["EmployeeCode"].ToString();
                txtEmpName.Text = objEmployee.resultDT.Rows[0]["EmployeeLNameEng"].ToString() + " " + objEmployee.resultDT.Rows[0]["EmployeeFNameEng"].ToString() + " " + objEmployee.resultDT.Rows[0]["EmployeeMNameEng"].ToString();
                txtPayDesignation.Text = objEmployee.resultDT.Rows[0]["DesignationName"].ToString();
                txtPayDepartment.Text = objEmployee.resultDT.Rows[0]["DepartmentName"].ToString();
                txtGoossRS.Text = objEmployee.resultDT.Rows[0]["Gross"].ToString();
            }

            objTemplateZero = objUserTemplateBl.EmployeeTemplate_SelectForZero(Convert.ToInt32(ViewState["EmployeeMID"].ToString()), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
            gvEarnings.DataSource = objTemplateZero.resultDT;
            gvEarnings.DataBind();
            int a = 0;
            foreach (GridViewRow rowItem in gvEarnings.Rows)
            {
                //Note :  Change for TemplateField replace to BoundField get value Name : Arpit Shah, Date : 22-12-2021
                //For PayItemName
                lbl = (Label)rowItem.Cells[1].FindControl("lblName");
                lbl.Text = Convert.ToString(objTemplateZero.resultDT.Rows[a]["Name"].ToString());
                //For amount
                txt = (TextBox)rowItem.Cells[2].FindControl("textAmount");
                txt.Text = Convert.ToString(Math.Round(Convert.ToDouble(objTemplateZero.resultDT.Rows[a]["Amount"].ToString()), 2));
                a = a + 1;
            }
            Double TotalEarnings = 0;
            Double TotalSalary = 0;
            int j = 0;
            foreach (GridViewRow rowItem in gvEarnings.Rows)
            {
                txt = (TextBox)rowItem.Cells[2].FindControl("textAmount");
                TotalEarnings = TotalEarnings + Convert.ToDouble(txt.Text.ToString());
                j = j + 1;
                if (Convert.ToInt32(rowItem.Cells[0].Text) == 1 || Convert.ToInt32(rowItem.Cells[0].Text) == 2)
                {
                    txt = (TextBox)rowItem.Cells[2].FindControl("textAmount");
                    TotalSalary = TotalSalary + Convert.ToDouble(txt.Text.ToString());
                    j = j + 1;
                }
            }
            Double SA;
            txtBasicTotal.Text = objControl.ConvertToCurrancy(TotalSalary.ToString());
            SA = Gross - TotalEarnings;

            txtPayTotalEarnings.Text = objControl.ConvertToCurrancy(TotalEarnings.ToString());

            ApplicationResult ObjUserTempleteOne = new ApplicationResult();
            ObjUserTempleteOne = objUserPayItemTemplateBl.EmployeeTemplate_SelectForEarning(Convert.ToInt32(ViewState["EmployeeMID"].ToString()), (Convert.ToInt32(Session[ApplicationSession.SCHOOLID])));
            gvDeduction.DataSource = ObjUserTempleteOne.resultDT;
            gvDeduction.DataBind();
            int l = 0;
            foreach (GridViewRow rowItem in gvDeduction.Rows)
            {
                //Note :  Change for TemplateField replace to BoundField get value Name : Arpit Shah, Date : 22-12-2021
                //For PayItemName
                lbl = (Label)rowItem.Cells[1].FindControl("lblName");
                lbl.Text = Convert.ToString(ObjUserTempleteOne.resultDT.Rows[l]["Name"].ToString());
                //For amount
                txt = (TextBox)rowItem.Cells[2].FindControl("textAmount");
                txt.Text = Convert.ToString(Math.Round(Convert.ToDouble(ObjUserTempleteOne.resultDT.Rows[l]["Amount"].ToString()), 2));
                l = l + 1;
            }
            j = 0;
            Double TotalDedction = 0;

            //Pension and GPF Calcualtion
            //Double pf = 0;
            //Double Pension = 0;
            foreach (GridViewRow rowItem in gvDeduction.Rows)
            {
                txt = (TextBox)rowItem.Cells[2].FindControl("textAmount");
                //string strAmount = Convert.ToString(Math.Round(Convert.ToDouble(ObjUserTempleteOne.resultDT.Rows[l]["Amount"].ToString(),0), 0));
                TotalDedction = TotalDedction + Convert.ToDouble(txt.Text.ToString());
                //j = j + 1;
                //if (Convert.ToInt32(rowItem.Cells[0].Text) == 8)
                //{
                //    Pension = Math.Round(TotalSalary * Convert.ToDouble(8.3333) / 100, 2);
                //    if (Pension >= 542)
                //    {
                //        txtPension.Text = objControl.ConvertToCurrancy("542");
                //        pf = Math.Round(Convert.ToDouble(Convert.ToDouble(txt.Text.ToString()) - Convert.ToInt32(542)), 2);
                //        txtGpf.Text = objControl.ConvertToCurrancy(pf.ToString());
                //    }
                //    else
                //    {
                //        txtPension.Text = objControl.ConvertToCurrancy(Pension.ToString());
                //        pf = Math.Round(Convert.ToDouble(Convert.ToInt32(txt.Text.ToString()) - Convert.ToInt32(Pension)), 2);
                //        txtGpf.Text = Convert.ToString((pf));
                //    }
                //}
            }
            txtPayTotalDeduction.Text = objControl.ConvertToCurrancy(TotalDedction.ToString());
            txtPayNetSalary.Text = objControl.ConvertToCurrancy((TotalEarnings - TotalDedction).ToString());
            txtPayNetSalaryRoundOf.Text = Convert.ToString(Math.Round(Convert.ToDouble(objControl.ConvertToCurrancy((TotalEarnings - TotalDedction).ToString())), 0));
        }
        #endregion

        #region gvEmployee RowCommand Event
        protected void gvEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            ViewState["EmployeeMID"] = commandArgs[0];
            ViewState["SchoolMID"] = commandArgs[1];
            if (e.CommandName.ToString() == "Edit1")
            {
                CalculatePayslip();
            }
        }
        #endregion

        #region Get Employee Web Service Method
        [System.Web.Services.WebMethod]
        public static string[] GetAllEmployeeNameForReport(string prefixText, int TrustMID, int SchoolMID)
        {
            UserTemplateTBl objEmployeeMbl = new UserTemplateTBl();
            ApplicationResult objResult = new ApplicationResult();
            string strSearchText = prefixText + "%";
            List<string> result = new List<string>();
            objResult = objEmployeeMbl.Employee_Select_AutocomleteForPayroll(strSearchText, TrustMID, SchoolMID);
            if (objResult != null)
            {
                for (int i = 0; i < objResult.resultDT.Rows.Count; i++)
                {
                    string strEmployeeCodeName = objResult.resultDT.Rows[i]["EmployeeName"].ToString();
                    string strEmployeeMID = objResult.resultDT.Rows[i][EmployeeMBO.EMPLOYEEM_EMPLOYEEMID].ToString();
                    result.Add(string.Format("{0}~{1}", strEmployeeCodeName, strEmployeeMID));
                }
            }
            return result.ToArray();
        }

        #endregion

        #region Bind Year
        public void bindYear()
        {
            string[] strYear;
            int intacYear = 0;
            #region Get Accounting Start Date
            ApplicationResult objResults = new ApplicationResult();
            TrustBL objTrustBl = new TrustBL();

            objResults = objTrustBl.Trust_Select(Convert.ToInt32(Session[ApplicationSession.TRUSTID].ToString()));

            if (objResults != null)
            {
                if (objResults.resultDT.Rows.Count > 0)
                {
                    string strACStartDate = objResults.resultDT.Rows[0][TrustBO.TRUST_ACCOUNTSTARTDATE].ToString();
                    strYear = strACStartDate.ToString().Split(new char[] { '/' });
                    intacYear = Convert.ToInt32(strYear[2]);
                }

            }
            #endregion


            for (int i = intacYear; i <= DateTime.Now.Year; i++)
            {
                ddlYear.Items.Add(i.ToString());
            }
            ddlYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;
        }
        #endregion

        #region ddlMonth SelectedChangedIndex Event
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Find Month wise Leave Balance and Absent Days, Total Days and Earned Days
            try
            {
                if (ddlMonth.SelectedIndex != 0)
                {
                    //ddlYear.Items.Insert(0, new ListItem("-Select-", "-1"));
                    month = Convert.ToInt32(ddlMonth.SelectedValue);
                    year = Convert.ToInt32(ddlYear.SelectedValue.ToString());
                    txtPayTotalDays.Text = Convert.ToString(DateTime.DaysInMonth(year, month));
                    txtPayEarnedDays.Text = txtPayTotalDays.Text;
                    
                    //Name : Arpit Shah
                    //Date: 22-12-2021
                    //Description : For Manual Days Calculation.
                    hftxtPayTotalDays.Value = Convert.ToString(DateTime.DaysInMonth(year, month));
                    //clear total textbox when month changed 10/11/2022 Bhandavi
                    txtTotal.Text = "0";
                    BindLeaveBalance(Convert.ToInt32(ViewState["EmployeeMID"].ToString()), ddlMonth.SelectedValue, Convert.ToInt32(ddlYear.SelectedValue));                   

                }
                else
                {
                    txtPayTotalDays.Text = "0";
                    txtPayEarnedDays.Text = "0";
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion

        #region rblNewEmp selectedIndexChanged
        protected void rblnNewEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlMonth.SelectedIndex != 0)
                {
                    if (rblnNewEmp.SelectedValue == "Yes")
                    {
                        txtPayTotalDays.Enabled = true;
                        btnChange.Visible = true;
                    }
                    else
                    {
                        txtPayTotalDays.Enabled = false;
                        btnChange.Visible = false;
                        month = ddlMonth.SelectedIndex;
                        year = Convert.ToInt32(ddlYear.SelectedValue.ToString());
                        txtPayTotalDays.Text = Convert.ToString(DateTime.DaysInMonth(year, month));
                        txtPayEarnedDays.Text = txtPayTotalDays.Text;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Select Month!.');", true);
                }
            }

            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion

        #region txtPayAbsenceDay TextChanged Event
        public void txtPayAbsenceDay_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txtPayAbsenceDay.Text == "" ? "0" : txtPayAbsenceDay.Text) > 0)
            {
                btnSendForApproval.Enabled = false;
            }
            else if (Convert.ToDouble(txtPayAbsenceDay.Text == "" ? "0" : txtPayAbsenceDay.Text) == 0)
            {
                btnSendForApproval.Enabled = true;
            }
        }
        #endregion

        #region txtrentPaid textchanged Event
        protected void txtRentPaid_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region btnClear Click Event
        protected void btnClear_Click(object sender, EventArgs e)
        {
            lblerror.Visible = false;
            GDBalance.Enabled = true;
            btnClear.Visible = false;
            txtTotal.Text = "0";
            int j = 0;
            foreach (GridViewRow rowItem in GDBalance.Rows)
            {
                if (GDBalance.Rows[j].Cells[1].Text == "1")
                {
                    txt = (TextBox)rowItem.Cells[3].FindControl("textDays");
                    txt.Text = "0";
                    try
                    {
                        txt.BackColor = System.Drawing.Color.White;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                j = j + 1;
            }
        }
        #endregion

        #region btnCalculate Click Event
        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            Controls objControl = new Controls();
            btnSendForApproval.Enabled = true;
            btnCalculate.Enabled = false;
            btnRedo.Enabled = true;

            lblerror.Visible = false;
            double i = 0;
            int j = 0;
            double LWP = 0;

            foreach (GridViewRow rowItem in GDBalance.Rows)
            {              
                txt = (TextBox)rowItem.Cells[4].FindControl("textDays");
                //string abc = txt.Text.ToString();
                try
                {
                    //if (j == 0)
                    //{
                    //    LWP = Convert.ToDouble(txt.Text);
                    //}
                    if (GDBalance.Rows[j].Cells[1].Text == "1")
                    {
                        LWP = Convert.ToDouble(txt.Text);
                    }
                    if (txt.Text != "")
                    {
                        if (GDBalance.Rows[j].Cells[1].Text == "1")
                        {
                            double AvailDays = Convert.ToDouble(GDBalance.Rows[j].Cells[3].Text.ToString());
                            if (AvailDays < Convert.ToDouble(txt.Text))
                            {
                                txt.BackColor = System.Drawing.Color.Red;
                                GDBalance.Enabled = false;
                                btnClear.Visible = true;
                                lblerror.Visible = true;
                                lblerror.Text = "You have entered more days then Available";
                            }
                            else
                            {
                                i = i + Convert.ToDouble(txt.Text);
                                if (i == Convert.ToDouble(txtPayAbsenceDay.Text))
                                {
                                    GDBalance.Enabled = false;
                                }
                                if (i > Convert.ToDouble(txtPayAbsenceDay.Text))
                                {
                                    GDBalance.Enabled = false;
                                    btnClear.Visible = true;
                                    lblerror.Text = "More then requested Absence Days";
                                    txtTotal.Text = i.ToString();
                                    lblerror.Visible = true;
                                }
                                else
                                {
                                    txtTotal.Text = i.ToString();
                                    GDBalance.Rows[j].Cells[5].Text = (AvailDays - Convert.ToDouble(txt.Text)).ToString();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                j = j + 1;
            }
           // double TD = Convert.ToDouble(txtPayTotalDays.Text.ToString());
            double TD = Convert.ToDouble(hftxtPayTotalDays.Value);
            //double TD = Convert.ToDouble(30);

            txtPayEarnedDays.Text = (TD - LWP).ToString();
            int a = 0;
            int c = 0;
            double GrossValue = 0;
            foreach (GridViewRow rowItem in gvEarnings.Rows)
            {
                CheckBox ckb = new CheckBox();
                ckb = (CheckBox)rowItem.Cells[4].FindControl("ckbAmount");
                if (ckb.Checked == true)
                {
                    Double amt = 0;
                    txt = (TextBox)rowItem.Cells[2].FindControl("textAmount");
                    amt = Math.Round(Convert.ToDouble(txt.Text.ToString()), 0);
                    Double iTotal = Convert.ToDouble(txtPayEarnedDays.Text) / Convert.ToDouble(TD);
                    amt = (amt * Convert.ToDouble(txtPayEarnedDays.Text)) / Convert.ToDouble(TD);
                    txt.Text = Convert.ToString(Math.Round(Convert.ToDouble(amt.ToString()), 0));

                    GrossValue = GrossValue + Convert.ToDouble(txt.Text);
                    c = c + 1;
                }
            }
            //Find Basic Total
            //txtBasicTotal.Text =
            //    Convert.ToString(
            //        Math.Round(Convert.ToDouble(txtPayNetSalaryRoundOf.Text) -
            //                   (Convert.ToDouble(txtPayNetSalaryRoundOf.Text) / 30) * Convert.ToDouble(LWP)));

            //Find 
            txtPayTotalEarnings.Text = Convert.ToString(GrossValue);
            c = 0;

            foreach (GridViewRow rowItem in gvDeduction.Rows)
            {
                CheckBox ckb = new CheckBox();
                ckb = (CheckBox)rowItem.Cells[3].FindControl("ckbAmount");
                if (ckb.Checked == true)
                {
                    //Note : Find PayItem Name because this logic is used to calculate Professional Tax
                    //Name : Arpit Shah
                    //Date : 22-12-2021

                    //Note : Changes for new  Professional Tax slab as per kaushik sir mail on 03 June 2022 10:51
                    //Name : Yogesh Patel
                    //Date : 07-06-2022

                    Double amt = 0;

                    //For PayItemName
                    lbl = (Label)rowItem.Cells[1].FindControl("lblName");
                    payname = Convert.ToString(lbl.Text.ToString());

                    //For Amount
                    txt = (TextBox)rowItem.Cells[2].FindControl("textAmount");
                    amt = Math.Round(Convert.ToDouble(txt.Text.ToString()), 0);
                    
                    if (payname == "P.TAX")
                    {
                        //Apply Professional Tax Slab
                        //if(txtPayTotalEarnings.Text <= 6000)
                        if (GrossValue < 12000)
                        {
                            txt.Text = Convert.ToString(Math.Round(Convert.ToDouble(0)));
                            c = c + 1;
                        }
                        else if (GrossValue >= 12000)
                        {
                            txt.Text = Convert.ToString(Math.Round(Convert.ToDouble(200)));
                            c = c + 1;
                        }
                        
                        //if (GrossValue <= 6000)
                        //{
                        //    txt.Text = Convert.ToString(Math.Round(Convert.ToDouble(0)));
                        //    c = c + 1;
                        //}
                        //else if (GrossValue > 6000 && GrossValue <= 9000)
                        //{
                        //    txt.Text = Convert.ToString(Math.Round(Convert.ToDouble(80)));
                        //    c = c + 1;
                        //}
                        //else if (GrossValue > 9000 && GrossValue <= 12000)
                        //{
                        //    txt.Text = Convert.ToString(Math.Round(Convert.ToDouble(150)));
                        //    c = c + 1;
                        //}
                        //else if (GrossValue > 12000)
                        //{
                        //    txt.Text = Convert.ToString(Math.Round(Convert.ToDouble(200)));
                        //    c = c + 1;
                        //}
                    }
                    //Note :Added ITotal To subtract itotal value from PF amount
                    //Name :Yogesh Patel    
                    //Date : 23-06-2022
                    else if (payname != "P.TAX")
                    {
                        
                        Double iTotal = Convert.ToDouble(txtPayEarnedDays.Text) / Convert.ToDouble(TD);
                        amt = (amt * Convert.ToDouble(txtPayEarnedDays.Text)) / Convert.ToDouble(TD)- Convert.ToDouble(iTotal);// Added ITotal To substact itotal valcue from PF amount
                        txt.Text = Convert.ToString(Math.Round(Convert.ToDouble(amt.ToString()), 0));
                        c = c + 1;
                    }

                    //Old Code
                    //Double iTotal = Convert.ToDouble(txtPayEarnedDays.Text) / Convert.ToDouble(TD);
                    //amt = (amt * Convert.ToDouble(txtPayEarnedDays.Text)) / Convert.ToDouble(TD);
                    //txt.Text = Convert.ToString(Math.Round(Convert.ToDouble(amt.ToString()), 0));
                    //c = c + 1;
                }
            }

            //Apply LWP after Calculation Earning and Deduction
            Double TotalEarnings = 0;
            Double TotalSalary = 0;
            j = 0;
            foreach (GridViewRow rowItem in gvEarnings.Rows)
            {
                txt = (TextBox)rowItem.Cells[2].FindControl("textAmount");
                TotalEarnings = TotalEarnings + Math.Round(Convert.ToDouble(txt.Text.ToString()), 0);
                j = j + 1;
                if (Convert.ToInt32(rowItem.Cells[0].Text) == 1 || Convert.ToInt32(rowItem.Cells[0].Text) == 2)
                {
                    txt = (TextBox)rowItem.Cells[2].FindControl("textAmount");
                    TotalSalary = TotalSalary + Math.Round(Convert.ToDouble(txt.Text.ToString()), 0);
                    j = j + 1;
                }
            }
            txtPayTotalEarnings.Text = objControl.ConvertToCurrancy(TotalEarnings.ToString());
            txtBasicTotal.Text = objControl.ConvertToCurrancy(TotalSalary.ToString());
            j = 0;
            Double TotalDedction = 0;

            //Pension and GPF Calcualtion 
            Double pf = 0;
            //Double Pension = 0;
            foreach (GridViewRow rowItem in gvDeduction.Rows)
            {
                txt = (TextBox)rowItem.Cells[2].FindControl("textAmount");
                TotalDedction = TotalDedction + Convert.ToDouble(txt.Text.ToString());
                j = j + 1;

                //-----------------------------------------------------------------------------------------------------------------
                //if (Convert.ToInt32(rowItem.Cells[0].Text) == 8)
                //{
                //    Pension = Math.Round(TotalSalary * Convert.ToDouble(8.3333) / 100, 0);
                //    if (Pension >= 542)
                //    {
                //        txtPension.Text = objControl.ConvertToCurrancy("542");
                //        pf = Math.Round((Convert.ToDouble(txt.Text.ToString())) - Convert.ToDouble(542));
                //        txtGpf.Text = objControl.ConvertToCurrancy(pf.ToString());
                //    }
                //    else
                //    {
                //        txtPension.Text = objControl.ConvertToCurrancy(Pension.ToString());
                //        pf = Math.Round(Convert.ToDouble(txt.Text.ToString()), 0) - Math.Round(Convert.ToDouble(Pension), 0);
                //        txtGpf.Text = Convert.ToString(pf);
                //    }
                //}
                //-----------------------------------------------------------------------------------------------------------------
            }
            txtPayTotalDeduction.Text = objControl.ConvertToCurrancy(TotalDedction.ToString());
            txtPayNetSalary.Text = objControl.ConvertToCurrancy((TotalEarnings - TotalDedction).ToString());
            txtPayNetSalaryRoundOf.Text = Convert.ToString(Math.Round(Convert.ToDouble(objControl.ConvertToCurrancy((TotalEarnings - TotalDedction).ToString())), 2));
        }
        #endregion

        #region btnSendForApproval Click Event
        protected void btnSendForApproval_Click(object sender, EventArgs e)
        {
            try
            {
                //Check New Employe or Not
                if (rblnNewEmp.SelectedValue == "Yes" || rblnNewEmp.SelectedValue == "No" && (ddlMonth.SelectedValue != "-1" && ddlYear.SelectedValue != "-1"))
                {
                    //Check Earning or Deduction
                    if(gvEarnings.Rows.Count !=0)
                    {
                        //Check Leave Balance
                        if (GDBalance.Rows.Count != 0)
                        {
                            //Check PayAbsenceDay and Total are match or not
                            if (txtPayAbsenceDay.Text == txtTotal.Text)
                            {
                                ApplicationResult objResults = new ApplicationResult();
                                ApplicationResult objResultsPaySlipLeave = new ApplicationResult();
                                ApplicationResult objResultsPaySlipUserPayItem = new ApplicationResult();
                                Controls objControl = new Controls();
                                PaySlipBo ObjPaySlipBo = new PaySlipBo();
                                PaySlipBl ObjPaySlipBl = new PaySlipBl();
                                PaySlipLeaveTBo ObjPaySlipLeaveTBo = new PaySlipLeaveTBo();
                                PaySlipLeaveTBl ObjPaySlipLeaveTBl = new PaySlipLeaveTBl();
                                PaySlipUserPayItemTBl ObjPaySlipUserPayItemTBl = new PaySlipUserPayItemTBl();
                                PaySlipUserPayItemTBo ObjPaySlipUserPayItemTBo = new PaySlipUserPayItemTBo();
                                UserPayItemTemplateTBl objUserPayItemTemplateBl = new UserPayItemTemplateTBl();

                                if (Page.IsValid == true)
                                {
                                    if (txtTotal.Text != txtPayAbsenceDay.Text)
                                    {
                                        lblerror.Text = "Please enter Leaves Availed for Absent days.";
                                        lblerror.Visible = true;
                                    }
                                    else
                                    {
                                        objResults = objUserPayItemTemplateBl.EmployeeTemplate_SelectEarnDays(Convert.ToInt32(ViewState["EmployeeMID"].ToString()));

                                        if (objResults.resultDT.Rows.Count > 0)
                                        {
                                            if (ddlMonth.SelectedIndex ==
                                                Convert.ToInt32(objResults.resultDT.Rows[0]["Month"].ToString()) &&
                                                ddlYear.SelectedValue == objResults.resultDT.Rows[0]["Year"].ToString() && (Convert.ToInt32(objResults.resultDT.Rows[0]["EmployeeMID"].ToString()) == (Convert.ToInt32(ViewState["EmployeeMID"].ToString()))))
                                            {
                                                ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                                                    "alert('Payslip of this User of this month has already been generated.');",
                                                    true);
                                                goto PayslipGenerated;
                                            }
                                        }
                                        DatabaseTransaction.OpenConnectionTransation();
                                        ObjPaySlipBo.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID].ToString());
                                        ObjPaySlipBo.UserID =
                                            Convert.ToInt32(Convert.ToInt32(ViewState["EmployeeMID"].ToString()));
                                        ObjPaySlipBo.Month = ddlMonth.SelectedIndex;
                                        ObjPaySlipBo.Year = Convert.ToInt32(ddlYear.SelectedValue);
                                        ObjPaySlipBo.Excemption = 0;
                                        //ObjPaySlipBo.TotalDaysofMonth = Convert.ToInt32(txtPayTotalDays.Text);
                                        ObjPaySlipBo.TotalDaysofMonth = Convert.ToDouble(txtPayTotalDays.Text);
                                        ObjPaySlipBo.EarnedDaysofMonth = Convert.ToDouble(txtPayEarnedDays.Text);
                                        ObjPaySlipBo.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                                        ObjPaySlipBo.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                        ObjPaySlipBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                                        ObjPaySlipBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                        DataTable dtPayslip = new DataTable();
                                        objResults = ObjPaySlipBl.PaySlip_Insert(ObjPaySlipBo);
                                        int pid = 0;
                                        if (objResults.resultDT.Rows.Count > 0)
                                        {
                                            pid = Convert.ToInt32(objResults.resultDT.Rows[0]["LastPaySlipGenerated"].ToString());
                                            int j = 0;
                                            //int count = 0;
                                            foreach (GridViewRow rowItem in GDBalance.Rows)
                                            {
                                                txt = (TextBox)rowItem.Cells[4].FindControl("textDays");
                                                try
                                                {
                                                    ObjPaySlipLeaveTBo.PaySlipID =
                                                        Convert.ToInt32(
                                                            objResults.resultDT.Rows[0]["LastPaySlipGenerated"].ToString());
                                                    ObjPaySlipLeaveTBo.LeaveID =
                                                        Convert.ToInt32(GDBalance.Rows[j].Cells[0].Text.ToString());
                                                    ObjPaySlipLeaveTBo.LeaveOpening =
                                                        Convert.ToDouble(GDBalance.Rows[j].Cells[3].Text.ToString());
                                                    ObjPaySlipLeaveTBo.LeaveBalance =
                                                        Convert.ToDouble(GDBalance.Rows[j].Cells[5].Text.ToString());
                                                    ObjPaySlipLeaveTBo.LeaveAvailed = Convert.ToDouble(txt.Text.ToString());
                                                    ObjPaySlipLeaveTBo.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                                                    ObjPaySlipLeaveTBo.CreatedUserID =
                                                        Convert.ToInt32(Session[ApplicationSession.USERID]);
                                                    ObjPaySlipLeaveTBo.LastModifiedDate =
                                                        DateTime.UtcNow.AddHours(5.5).ToString();
                                                    ObjPaySlipLeaveTBo.LastModifiedUserID =
                                                        Convert.ToInt32(Session[ApplicationSession.USERID]);
                                                    objResultsPaySlipLeave =
                                                        ObjPaySlipLeaveTBl.PaySlipLeaveT_Insert(ObjPaySlipLeaveTBo);
                                                    j = j + 1;
                                                }
                                                catch (Exception ex)

                                                {
                                                    throw ex;
                                                }
                                            }
                                        }
                                        int c = 0;
                                        foreach (GridViewRow rowItem in gvEarnings.Rows)
                                        {
                                            int id = Convert.ToInt32(gvEarnings.Rows[c].Cells[0].Text.ToString());
                                            Double amt = 0.0;
                                            txt = (TextBox)rowItem.Cells[2].FindControl("textAmount");
                                            amt = Convert.ToDouble(txt.Text.ToString());
                                            ObjPaySlipUserPayItemTBo.PaySlipID = pid;
                                            ObjPaySlipUserPayItemTBo.UserPayItemID = id;
                                            ObjPaySlipUserPayItemTBo.PaySlipPayItemAmount = amt;
                                            ObjPaySlipUserPayItemTBo.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                                            ObjPaySlipUserPayItemTBo.CreatedUserID =
                                                Convert.ToInt32(Session[ApplicationSession.USERID]);
                                            ObjPaySlipUserPayItemTBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                                            ObjPaySlipUserPayItemTBo.LastModifiedUserID =
                                                Convert.ToInt32(Session[ApplicationSession.USERID]);

                                            objResultsPaySlipUserPayItem =
                                                ObjPaySlipUserPayItemTBl.PaySlipUserPayItemT_Insert(ObjPaySlipUserPayItemTBo);
                                            c = c + 1;
                                        }

                                        c = 0;
                                        foreach (GridViewRow rowItem in gvDeduction.Rows)
                                        {
                                            int id = Convert.ToInt32(gvDeduction.Rows[c].Cells[0].Text.ToString());
                                            Double amt = 0.0;
                                            txt = (TextBox)rowItem.Cells[2].FindControl("textAmount");
                                            amt = Convert.ToDouble(txt.Text.ToString());
                                            ObjPaySlipUserPayItemTBo.PaySlipID = pid;
                                            ObjPaySlipUserPayItemTBo.UserPayItemID = id;
                                            ObjPaySlipUserPayItemTBo.PaySlipPayItemAmount = amt;
                                            ObjPaySlipUserPayItemTBo.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                                            ObjPaySlipUserPayItemTBo.CreatedUserID =
                                                Convert.ToInt32(Session[ApplicationSession.USERID]);
                                            ObjPaySlipUserPayItemTBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                                            ObjPaySlipUserPayItemTBo.LastModifiedUserID =
                                                Convert.ToInt32(Session[ApplicationSession.USERID]);
                                            objResultsPaySlipUserPayItem =
                                                ObjPaySlipUserPayItemTBl.PaySlipUserPayItemT_Insert(ObjPaySlipUserPayItemTBo);
                                            c = c + 1;
                                        }
                                        ObjPaySlipBo.UserID = Convert.ToInt32(ViewState["EmployeeMID"].ToString());
                                        ObjPaySlipBo.PayslipApproved = 0;
                                        ObjPaySlipBo.PaySlipSendforApproval = 1;
                                        ObjPaySlipBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                                        ObjPaySlipBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                        objResults = ObjPaySlipBl.PaySlip_Update_SelectedPart(ObjPaySlipBo);
                                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Payslip is generated Successfully.');", true);
                                        DatabaseTransaction.CommitTransation();
                                    PayslipGenerated:
                                        ;
                                    }
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Absent day and Total does not match! Calculate the Leaves.');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Leave Template has not created.Please Create Leave Template first.');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Applied (Earning / Deduction) Designation PI Template First.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Select Employee New or not.');", true);
                }
                //Response.Redirect("GeneratePaySlip.aspx");
            }
            catch (Exception ex)
            {
                DatabaseTransaction.RollbackTransation();
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion

        #region btnRedo Click Event
        protected void btnRedo_Click(object sender, EventArgs e)
        {
            CalculatePayslip();
            btnRedo.Enabled = false;
            btnSendForApproval.Enabled = true;
            btnCalculate.Enabled = false;
        }
        #endregion

        #region ChangePF textbox event of Gridview
        protected void ChangePF(object sender, System.EventArgs e)
        {
            int i = 0;
            Double arr = 0;
            Double Per = 0;
            Double PF = 0;
            Double ESIC = 0;
            foreach (GridViewRow rowItem in gvEarnings.Rows)
            {
                //if (gvEarnings.Rows[i].Cells[0].Text.ToString() == "6")
                //{
                //    TextBox txt = (TextBox)rowItem.Cells[2].FindControl("textAmount");
                //    if (txt.Text != "0" || txt.Text != "0")
                //    {
                //        arr = Convert.ToDouble(txt.Text.ToString());
                //        Per = Convert.ToDouble(gvEarnings.Rows[0].Cells[3].Text.ToString());
                //        PF = (((arr * Per) / 100) * 12) / 100;
                //        TextBox txtAmount = (TextBox)gvEarnings.Rows[0].Cells[2].FindControl("textAmount");
                //        ESIC = Convert.ToDouble(txtAmount.Text.ToString()) * 100 / Convert.ToDouble(gvEarnings.Rows[0].Cells[3].Text.ToString()); ;
                //        ESIC = ESIC * 1.75 / 100;
                //    }
                //}
                //i = i + 1;
            }
            i = 0;
            foreach (GridViewRow rowItem in gvDeduction.Rows)
            {
                if (gvDeduction.Rows[i].Cells[0].Text.ToString() == "4")
                {
                    TextBox txt = (TextBox)rowItem.Cells[2].FindControl("textAmount");
                    Double TotalPF = Convert.ToDouble(txt.Text.ToString()) + PF;
                    txt.Text = TotalPF.ToString();
                }
                i = i + 1;
            }
            i = 0;
            foreach (GridViewRow rowItem in gvDeduction.Rows)
            {
                if (gvDeduction.Rows[i].Cells[0].Text.ToString() == "3")
                {
                    TextBox txt = (TextBox)rowItem.Cells[2].FindControl("textAmount");
                    txt.Text = ESIC.ToString();
                }
                i = i + 1;
            }
        }
        #endregion

        #region ChangeDays textbox event of Gridview
        protected void ChangeDays(object sender, System.EventArgs e)
        {
            lblerror.Visible = false;
            double i = 0;
            int j = 0;
            double LWP = 0;

            foreach (GridViewRow rowItem in GDBalance.Rows)
            {
                txt = (TextBox)rowItem.Cells[4].FindControl("textDays");
                try
                {
                    //if (j == 0)
                    //{

                    //    LWP = Convert.ToDouble(txt.Text);
                    //}
                    if (GDBalance.Rows[j].Cells[1].Text == "1")
                    {
                        LWP = Convert.ToDouble(txt.Text);
                    }
                    if (txt.Text != "")
                    {
                        if (GDBalance.Rows[j].Cells[1].Text == "1")
                        {
                            double AvailDays = Convert.ToDouble(GDBalance.Rows[j].Cells[3].Text.ToString());
                            if (AvailDays < Convert.ToDouble(txt.Text))
                            {
                                txt.BackColor = System.Drawing.Color.Red;
                                GDBalance.Enabled = false;
                                btnClear.Visible = true;
                                lblerror.Visible = true;
                                lblerror.Text = "You have entered more days then Available";
                            }
                            else
                            {

                                i = i + Convert.ToDouble(txt.Text);

                                if (i == Convert.ToDouble(txtPayAbsenceDay.Text))
                                {
                                    GDBalance.Enabled = false;
                                }
                                if (i > Convert.ToDouble(txtPayAbsenceDay.Text))
                                {
                                    GDBalance.Enabled = false;
                                    btnClear.Visible = true;
                                    lblerror.Text = "More than requested Absence Days";
                                    txtTotal.Text = i.ToString();
                                    lblerror.Visible = true;
                                }
                                else
                                {
                                    txtTotal.Text = i.ToString();
                                    if (Convert.ToDouble(txtTotal.Text) == Convert.ToDouble(txtPayAbsenceDay.Text))
                                    {
                                        btnCalculate.Enabled = true;
                                    }
                                    GDBalance.Rows[j].Cells[5].Text = (AvailDays - Convert.ToDouble(txt.Text)).ToString();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                j = j + 1;
            }
        }
        #endregion

        #region btnChange Click Event
        protected void btnChange_Click(object sender, EventArgs e)
        {
            //New Employee or Change Basic Amount for Employee
            txtPayTotalDays.Enabled = false;
            month = ddlMonth.SelectedIndex;
            year = Convert.ToInt32(ddlYear.SelectedValue.ToString());
            days = Convert.ToDouble(DateTime.DaysInMonth(year, month));

            //Amount to Earning
            int d = 0;
            foreach (GridViewRow rowItem in gvEarnings.Rows)
            {
                Double amt = 0;
                txt = (TextBox)rowItem.Cells[2].FindControl("textAmount");
                amt = Math.Round(Convert.ToDouble(txt.Text.ToString()), 0);
                //amt = Math.Round(amt, 0) * Math.Round((Convert.ToDouble(txtPayTotalDays.Text)) / days, 0);
                d = d + 1;
                txt.Text = Convert.ToString(Math.Round(Convert.ToDouble(amt.ToString()), 0));

            }
            //Amount to Deduction
            d = 0;
            foreach (GridViewRow rowItem in gvDeduction.Rows)
            {
                Double amt = 0;
                txt = (TextBox)rowItem.Cells[2].FindControl("textAmount");
                amt = Math.Round(Convert.ToDouble(txt.Text.ToString()), 0);
                //amt = Math.Round(amt, 0) * Math.Round((Convert.ToDouble(txtPayTotalDays.Text)) / days, 0);
                d = d + 1;
                txt.Text = Convert.ToString(Math.Round(Convert.ToDouble(amt.ToString()), 0));
            }

            txtPayEarnedDays.Text = txtPayTotalDays.Text;

            double AbsenceDay = days - Convert.ToDouble(txtPayEarnedDays.Text);
            txtPayAbsenceDay.Text = Convert.ToString(AbsenceDay);
            btnChange.Visible = false;
        }
        #endregion

        #region ddlYear SelectedIndexChanged event
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            month = ddlMonth.SelectedIndex;
            year = Convert.ToInt32(ddlYear.SelectedValue.ToString());
            txtPayTotalDays.Text = Convert.ToString(DateTime.DaysInMonth(year, month));
        }
        #endregion

        //#region Bind GridView Leave Balance Old
        //private void BindLeaveBalance(int intEmployeeMID, string strMonth)
        //{
        //    try
        //    {
        //        LeaveBl objLeaveBl = new LeaveBl();
        //        ApplicationResult objResult = new ApplicationResult();

        //        if (Convert.ToInt32(strMonth) > 0 && Convert.ToInt32(intEmployeeMID) > 0)
        //        {
        //            //Bind Leave List
        //            objResult = objLeaveBl.Leave_Select_ForPayrollBalance(intEmployeeMID, strMonth);
        //            if (objResult != null)
        //            {
        //                if (objResult.resultDT.Rows.Count > 0)
        //                {
        //                    GDBalance.DataSource = objResult.resultDT;
        //                    GDBalance.DataBind();
        //                }
        //                else
        //                {
        //                    //ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Leave Template has not created.Please Create Leave Template first.');</script>");
        //                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Leave Template has not created.Please Create Leave Template first.');", true);
        //                }
        //            }
        //            //Find Total Leave, Full and Half Days 
        //            var objResultLeave = objLeaveBl.Leave_Select_ForPayrollLeave(intEmployeeMID, strMonth);
        //            for (int i = 0; i < GDBalance.Rows.Count; i++)
        //            {
        //                ((TextBox)(GDBalance.Rows[i].Cells[4].FindControl("textDays"))).Enabled = false;
        //                //if(GDBalance.Rows[i].Cells[0].Text == objResultLeave.resutlDS.Tables[0].Rows)
        //                for (int k = 0; k < objResultLeave.resultDT.Rows.Count; k++)
        //                {
        //                    int abc = Convert.ToInt32(objResultLeave.resultDT.Rows[k]["LeaveID"].ToString());
        //                    if (GDBalance.Rows[i].Cells[0].Text == (objResultLeave.resultDT.Rows[k]["LeaveID"].ToString()))
        //                    {
        //                        double dbFullDay = 0.0;
        //                        dbFullDay = Convert.ToInt32(objResultLeave.resultDT.Rows[k]["FullDay"].ToString());
        //                        double dbHalfDay = 0.0;
        //                        double dbTotal = 0.0;
        //                        double dbBalance = 0.0;
        //                        double dbDiff = 0.0;

        //                        dbBalance = Convert.ToDouble(GDBalance.Rows[i].Cells[3].Text.ToString()); //Find Leave Balance 
        //                        if ((objResultLeave.resultDT.Rows[k]["HalfDay"].ToString()) == "1")
        //                        {
        //                            dbHalfDay = 0.5;
        //                        }
        //                        dbTotal = dbFullDay + dbHalfDay; //Find Total = FullDyas - HalfDays
        //                        dbDiff = dbBalance - dbTotal; //Difference = Leave Balance - Total
        //                        ((TextBox)(GDBalance.Rows[i].Cells[4].FindControl("textDays"))).Text = dbTotal.ToString();
        //                        GDBalance.Rows[i].Cells[5].Text = dbDiff.ToString();
        //                    }
        //                    else
        //                    {
        //                        GDBalance.Rows[i].Cells[5].Text = (GDBalance.Rows[i].Cells[3].Text.ToString());
        //                    }
        //                }
        //                if (objResultLeave.resultDT.Rows.Count == 0)
        //                {
        //                    GDBalance.Rows[i].Cells[5].Text = (GDBalance.Rows[i].Cells[3].Text.ToString());
        //                }
        //                if (GDBalance.Rows[i].Cells[1].Text == "1")
        //                {
        //                    ((TextBox)(GDBalance.Rows[i].Cells[4].FindControl("textDays"))).Enabled = true;
        //                }
        //            }
        //            var objResultAbsent = objLeaveBl.Leave_Select_ForAbsentDays(intEmployeeMID, strMonth);
        //            if (objResultAbsent != null)
        //            {
        //                if (objResultAbsent.resultDT.Rows.Count > 0)
        //                {
        //                    txtPayAbsenceDay.Text = objResultAbsent.resultDT.Rows[0]["AbsentDays"].ToString();
        //                    btnCalculate.Enabled = true;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            txtPayTotalDays.Text = "0";
        //            txtPayEarnedDays.Text = "0";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Error", ex);
        //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
        //    }
        //}
        //#endregion

        /// <summary>
        /// to bind leave balance, availed leaves to gridview
        /// and to calculate late hours (For a hour late half day leave to be applied)
        /// </summary>
        /// <param name="intEmployeeMID"></param>
        /// <param name="strMonth"></param>
        /// <param name="intYear"></param>
        #region Bind GridView Leave Balance
        private void BindLeaveBalance(int intEmployeeMID, string strMonth,int intYear)
        {
            try
            {
                LeaveBl objLeaveBl = new LeaveBl();
                 ApplicationResult objResult = new ApplicationResult();

                if (Convert.ToInt32(strMonth) > 0 && Convert.ToInt32(intEmployeeMID) > 0 && Convert.ToInt32(intYear) > 0)
                {
                    //Bind Leave as per Leave Template Assign [LeaveID, LeaveName, Total, IsDeduction]
                    objResult = objLeaveBl.Leave_Select_ForPayrollBalance(intEmployeeMID, strMonth, intYear);
                    if (objResult != null)
                    {
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            GDBalance.DataSource = objResult.resultDT;
                            GDBalance.DataBind();
                        }
                        else
                        {
                            //ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Leave Template has not created.Please Create Leave Template first.');</script>");
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Leave Template has not created.Please Create Leave Template first.');", true);
                        }
                    }
                    //Display "Applied Leave Employee and Month wise] [LeaveID, LeaveName, Total, FullDay, HalfDay]
                    //Find Total Leave, Full and Half Days 
                    var objResultLeave = objLeaveBl.Leave_Select_ForPayrollLeave(intEmployeeMID, strMonth,intYear);
                    for (int i = 0; i < GDBalance.Rows.Count; i++)
                    {
                        ((TextBox)(GDBalance.Rows[i].Cells[4].FindControl("textDays"))).Enabled = false;
                        //if(GDBalance.Rows[i].Cells[0].Text == objResultLeave.resutlDS.Tables[0].Rows)
                        for (int k = 0; k < objResultLeave.resultDT.Rows.Count; k++)
                        {
                            LEAVEID = Convert.ToInt32(objResultLeave.resultDT.Rows[k]["LeaveID"].ToString());
                            //if (GDBalance.Rows[i].Cells[0].Text == (objResultLeave.resultDT.Rows[k]["LeaveID"].ToString()))
                            if (GDBalance.Rows[i].Cells[0].Text == Convert.ToString(LEAVEID)) //(objResultLeave.resultDT.Rows[k]["LeaveID"].ToString()))
                            {
                                double dbFullDay = 0.0;
                                dbFullDay = Convert.ToInt32(objResultLeave.resultDT.Rows[k]["FullDay"].ToString());
                                double dbHalfDay = 0.0;
                                double dbTotal = 0.0;
                                double dbBalance = 0.0;
                                double dbDiff = 0.0;

                                //Opening
                                dbBalance = Convert.ToDouble(GDBalance.Rows[i].Cells[3].Text.ToString()); //Find Leave Balance 
                                //if ((objResultLeave.resultDT.Rows[k]["HalfDay"].ToString()) == "1")
                                if (Convert.ToDouble((objResultLeave.resultDT.Rows[k]["HalfDay"].ToString())) > 0)
                                {
                                    dbHalfDay = Convert.ToDouble((objResultLeave.resultDT.Rows[k]["HalfDay"].ToString())) * 0.5;
                                    //dbHalfDay = 0.5;
                                }
                                //Availed   [Print Availed Value in GridView]                 
                                dbTotal = dbFullDay + dbHalfDay; //Find Total = FullDyas - HalfDays
                                ((TextBox)(GDBalance.Rows[i].Cells[4].FindControl("textDays"))).Text = dbTotal.ToString();

                                //Balance [Print Balance Value in GridView]
                                //dbDiff = dbBalance - dbTotal; //Difference = Leave Balance - Total
                                //GDBalance.Rows[i].Cells[5].Text = dbDiff.ToString();
                            }
                            else
                            {
                                //GDBalance.Rows[i].Cells[5].Text = (GDBalance.Rows[i].Cells[3].Text.ToString());
                            }
                        }
                        if (objResultLeave.resultDT.Rows.Count == 0)
                        {
                            GDBalance.Rows[i].Cells[5].Text = (GDBalance.Rows[i].Cells[3].Text.ToString());
                        }
                        if (GDBalance.Rows[i].Cells[1].Text == "1")
                        {
                            //Whose Leave using IsDeduction so textbox can be enable mode regarding user input manuualy leavae 
                            GDBalance.Rows[i].Cells[5].Text = (GDBalance.Rows[i].Cells[3].Text.ToString());
                            ((TextBox)(GDBalance.Rows[i].Cells[4].FindControl("textDays"))).Enabled = true;
                        }
                    }

                    //Find Balance [Arpit Shah] For new coding regarding Balance = Total - Availed

                    for (int i = 0; i < GDBalance.Rows.Count; i++)
                    {
                        ((TextBox)(GDBalance.Rows[i].Cells[4].FindControl("textDays"))).Enabled = false;
                        for (int k = 0; k < objResultLeave.resultDT.Rows.Count; k++)
                        {
                            double dbFullDay = 0.0;
                            dbFullDay = Convert.ToInt32(objResultLeave.resultDT.Rows[k]["FullDay"].ToString());

                            double dbBalance = 0.0;
                            double dbAvailed = 0.0;
                            double dbDiff = 0.0;

                            //Opening
                            dbBalance = Convert.ToDouble(GDBalance.Rows[i].Cells[3].Text.ToString()); //Find Leave Balance 
                            dbAvailed = Convert.ToDouble(((TextBox)(GDBalance.Rows[i].Cells[4].FindControl("textDays"))).Text);                                                                         //if ((objResultLeave.resultDT.Rows[k]["HalfDay"].ToString()) == "1")

                            //Balance [Print Balance Value in GridView]
                            dbDiff = dbBalance - dbAvailed; //Difference = Leave Balance - Total
                            GDBalance.Rows[i].Cells[5].Text = dbDiff.ToString();
                        }
                        if (objResultLeave.resultDT.Rows.Count == 0)
                        {
                            GDBalance.Rows[i].Cells[5].Text = (GDBalance.Rows[i].Cells[3].Text.ToString());
                        }
                        if (GDBalance.Rows[i].Cells[1].Text == "1")
                        {
                            //Whose Leave using IsDeduction so textbox can be enable mode regarding user input manuualy leavae 
                            GDBalance.Rows[i].Cells[5].Text = (GDBalance.Rows[i].Cells[3].Text.ToString());
                            ((TextBox)(GDBalance.Rows[i].Cells[4].FindControl("textDays"))).Enabled = true;
                        }
                    }
                    //to display absent days in selected month
                    var objResultAbsent = objLeaveBl.Leave_Select_ForAbsentDays(intEmployeeMID, strMonth);
                    if (objResultAbsent != null)
                    {
                        //Old Code
                        //if (objResultAbsent.resultDT.Rows.Count > 0)
                        //{
                        //    txtPayAbsenceDay.Text = objResultAbsent.resultDT.Rows[0]["AbsentDays"].ToString();
                        //    btnCalculate.Enabled = true;
                        //}

                        //Chnage Code : Arpit Shah [21-12-2021] 
                        //Note : When AbesnceDay show (-) minus value, so Total Textbox can change edit mode.
                        
                        if (objResultAbsent.resultDT.Rows.Count > 0)
                        {
                            //txtPayAbsenceDay.Text = objResultAbsent.resultDT.Rows[0]["AbsentDays"].ToString();
                            //Getting error when objResultAbsent.resultDT.Rows[0]["AbsentDays"].ToString() value is empty string
                            //so changed following code 11/10/2022  Bhandvai
                            //Getting minus(-) values when there is record in employee attendance table and leave applied on taht day
                            txtPayAbsenceDay.Text = objResultAbsent.resultDT.Rows[0]["AbsentDays"].ToString() == "" ? "0" : objResultAbsent.resultDT.Rows[0]["AbsentDays"].ToString();
                            double PayAbsenceDay = Convert.ToDouble(txtPayAbsenceDay.Text);

                            if(PayAbsenceDay < 0)
                            {
                                txtPayAbsenceDay.Text="0";
                                txtTotal.Enabled = true;
                                btnCalculate.Enabled = false;
                            }
                            else
                            {
                                txtTotal.Enabled = false;
                                btnCalculate.Enabled = true;
                            }
                        }
                    }
                    //07/11/2022 Bhandavi
                    //Check whether an employee is late for 1 hour with in a month
                    //if late by 1 hour then need to apply leave of halfday
                    var objResultLate = objLeaveBl.Leave_Select_Late(intEmployeeMID, strMonth, intYear);
                    if (objResultLate != null)
                    {
                        if (objResultLate.resultDT.Rows.Count > 0)
                        {
                            DataTable dtLate = objResultLate.resultDT;
                            decimal timeDiff = 0;
                            for (int i=0;i< dtLate.Rows.Count;i++)
                            {
                                timeDiff +=Convert.ToDecimal(dtLate.Rows[i]["timeDiff"].ToString());                                
                            }
                            decimal lateHours = 0;
                            //change minutes into hours
                            if (timeDiff >= 60)
                            {                              
                                timeDiff = timeDiff / 60;                              
                                //decimal d = 467.75M;
                                int lateHour = (int)timeDiff;    
                                 lateHours = (decimal)lateHour / 2;

                                txtPayAbsenceDay.Text = (Convert.ToDecimal(txtPayAbsenceDay.Text) + lateHours).ToString();
                                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please apply a leave');", true);
                            }

                            //Check number of leaves applied and absent days
                            //check attendance table if present and applied for leave then consider for late hours (half day leave for 30 minutes late)
                            //for every 
                            if (lateHours > 0)
                            {
                                var objResultAbsentLeave = objLeaveBl.Leave_Select_AbsentDays(intEmployeeMID, strMonth, intYear);
                                if (objResultAbsentLeave != null)
                                {
                                    if (objResultAbsentLeave.resultDT.Rows.Count > 0)
                                    {
                                        double countL = 0;
                                        countL = Convert.ToDouble(objResultAbsentLeave.resultDT.Rows[0]["CountA"]);
                                        txtPayAbsenceDay.Text = (Convert.ToDouble(txtPayAbsenceDay.Text) - countL).ToString();
                                    }
                                }
                            }
                        }                           
                    }

                  
                   
                }
                else
                {
                    txtPayTotalDays.Text = "0";
                    txtPayEarnedDays.Text = "0";
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion

        #region Back Button
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("GeneratePaySlip.aspx");
        }
        #endregion
    }
}