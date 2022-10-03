using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using log4net;
using GEIMS.DataAccess;
using GEIMS.Bl;
using GEIMS.Bo;
using System.Data;

namespace GEIMS.PayRoll
{
    public partial class EmployeeTDSCalculation : System.Web.UI.Page
    {
        #region user defined variables
        private static ILog logger = LogManager.GetLogger(typeof(EmployeeTDSCalculation));
        public double GTotal = 0;
        public double PTotal = 0;
        double ProfessionalValue = 0;
        double GrossValue = 0;
        double RentPaid = 0;
        double HRAExemption = 0;
        double TGSalary = 0;
        double StandardDeduction = 0;
        double FindHRA = 0;
        int FindCalculatorID = 0;
        int FindCalculatorDetailsID = 0;
        #endregion

        #region Declaration
        int month = 0;
        int year = 0;
        double days;
        static Double Gross = 0;
        TextBox txt = new TextBox();
        TextBox txt1 = new TextBox();
        #endregion

        #region Page Load
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

                    divForm.Visible = false;
                    bindYear();
                    BindAcademicYear();
                    //btnSaveForApprovalTDS.Enabled = false;
                }
                ViewState["Mode"] = "Save";
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
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

        #region BindAcademicYear
        public void BindAcademicYear()
        {
            try
            {
                #region Fetch Academic Month from School
                SchoolBL objSchoolBl = new SchoolBL();
                ApplicationResult objResults = new ApplicationResult();
                int intMonth = 0;

                objResults = objSchoolBl.School_Select(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));

                if (objResults != null)
                {
                    if (objResults.resultDT.Rows.Count > 0)
                    {

                        intMonth = Convert.ToInt32(objResults.resultDT.Rows[0][SchoolBO.SCHOOL_ACADEMICMONTH].ToString());
                    }

                }
                #endregion

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
                objControls.BindDropDown_ListBox(dt, ddlAcademicYear, "AcademicYear", "AcademicYear");
                ddlAcademicYear.Items.Insert(0, new ListItem("-Select-", "-1"));
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Back Button
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeTDSCalculation.aspx");
        }
        #endregion

        #region btnGo Event
        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
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

        #region CalculateTDS function
        public void CalculateTDS()
        {
            try
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

                //txtTotal.Text = "0";

                btnSaveForApprovalTDS.Enabled = true;

                //Last Payslip Check
                objResult = objUserTemplateBl.UserTemplateT_SelectLeaveID_ByEmployeeMID(Convert.ToInt32(ViewState["EmployeeMID"].ToString()));


                //User PayItem Earned Days
                objResultPayslip = objUserPayItemTemplateBl.EmployeeTemplate_SelectEarnDays(Convert.ToInt32(ViewState["EmployeeMID"].ToString()));
                if (objResultPayslip.resultDT.Rows.Count > 0)
                {
                    //ddlMonth.SelectedIndex = Convert.ToInt32(objResultPayslip.resultDT.Rows[0]["Month"].ToString());
                    ddlYear.SelectedValue = objResultPayslip.resultDT.Rows[0]["Year"].ToString();
                }
                else
                {
                    ddlMonth.SelectedIndex = 0;
                    ddlYear.SelectedValue = Convert.ToString(DateTime.Now.Year);
                }
                //txtTotal.Enabled = true;

                //objEmployee = objEmployeeBl.SelectEmployee_For_ProcessPayRoll(Convert.ToInt32(ViewState["EmployeeMID"].ToString()));

                //if (objEmployee.resultDT.Rows.Count > 0)
                //{

                //    txtPayEmpCode.Text = objEmployee.resultDT.Rows[0]["EmployeeCode"].ToString();
                //    txtEmpName.Text = objEmployee.resultDT.Rows[0]["EmployeeLNameEng"].ToString() + " " + objEmployee.resultDT.Rows[0]["EmployeeFNameEng"].ToString() + " " + objEmployee.resultDT.Rows[0]["EmployeeMNameEng"].ToString();
                //    txtPayDesignation.Text = objEmployee.resultDT.Rows[0]["DesignationName"].ToString();
                //    txtPayDepartment.Text = objEmployee.resultDT.Rows[0]["DepartmentName"].ToString();
                //}
                objTemplateZero = objUserTemplateBl.EmployeeTemplate_SelectForZero(Convert.ToInt32(ViewState["EmployeeMID"].ToString()), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                
               //binding Grid 1
                gvEarnings.DataSource = objTemplateZero.resultDT;
                gvEarnings.DataBind();


                int NoMonths = Convert.ToInt32(ddlMonth.SelectedValue.ToString());
                //Amount
                int a = 0;
                foreach (GridViewRow rowItem in gvEarnings.Rows)
                {
                    //txt = (TextBox)rowItem.Cells[2].FindControl("textAmount");
                    //txt1 = (TextBox)rowItem.Cells[2].FindControl("textAmountOld");

                    //txt.Text = Convert.ToString(Math.Round(Convert.ToDouble(objTemplateZero.resultDT.Rows[a]["Amount"].ToString()), 2));
                    //txt1.Text = Convert.ToString(Math.Round(Convert.ToDouble(objTemplateZero.resultDT.Rows[a]["Amount"].ToString()), 2));

                    txt = (TextBox)rowItem.Cells[2].FindControl("textAmount");
                    txt1 = (TextBox)rowItem.Cells[2].FindControl("textAmountOld");

                    double bamt = (Math.Round(Convert.ToDouble(objTemplateZero.resultDT.Rows[a]["Amount"].ToString()), 2)) * NoMonths;
                    txt.Text = Convert.ToString(bamt.ToString());
                    txt1.Text = Convert.ToString(Math.Round(Convert.ToDouble(objTemplateZero.resultDT.Rows[a]["Amount"].ToString()), 2));

                    a = a + 1; 
                }

                //Amount Total
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
                //txtBasicTotal.Text = objControl.ConvertToCurrancy(TotalSalary.ToString());
                SA = Gross - TotalEarnings;

                //Gross annual salary              
                txtPayTotalEarnings.Text = objControl.ConvertToCurrancy(TotalEarnings.ToString());
                GTotal = Convert.ToDouble(objControl.ConvertToCurrancy(TotalEarnings.ToString()));

                //Extra PayItem
                PTotal = Convert.ToDouble(txtPayItem3.Text == "" ? "0" : txtPayItem3.Text) + Convert.ToDouble(txtPayItem4.Text == "" ? "0" : txtPayItem4.Text) + Convert.ToDouble(txtPayItem5.Text == "" ? "0" : txtPayItem5.Text) + Convert.ToDouble(txtPayItem6.Text == "" ? "0" : txtPayItem6.Text) + Convert.ToDouble(txtPayItem7.Text == "" ? "0" : txtPayItem7.Text) + GTotal;
                txtPayTotalEarnings.Text = Convert.ToString(PTotal);

                //Find Professional Tax Salb Wise
                int i = Convert.ToInt32(ddlMonth.SelectedValue);
                GrossValue = Convert.ToDouble(txtPayTotalEarnings.Text) / i;

                if (GrossValue <= 6000)
                {
                    ProfessionalValue = (0 * i);
                    txtProfessionalTax2.Text = Convert.ToString(ProfessionalValue);
                }
                else if (GrossValue > 6000 && GrossValue <= 9000)
                {
                    ProfessionalValue = (80 * i);
                    txtProfessionalTax2.Text = Convert.ToString(ProfessionalValue);
                }
                else if (GrossValue > 9000 && GrossValue <= 12000)
                {
                    ProfessionalValue = (150 * i);
                    txtProfessionalTax2.Text = Convert.ToString(ProfessionalValue);
                }
                else if (GrossValue > 12000)
                {
                    ProfessionalValue = (200 * i);
                    txtProfessionalTax2.Text = Convert.ToString(ProfessionalValue);
                }

                //Find Standard Deduction
                StandardDeduction = Convert.ToDouble(txtPayTotalEarnings.Text == "" ? "0" : txtPayTotalEarnings.Text) - Convert.ToDouble(txtProfessionalTax2.Text == "" ? "0" : txtProfessionalTax2.Text) - Convert.ToDouble(txtHRAExemption2.Text == "" ? "0" : txtHRAExemption2.Text);
                TtxtStandardDeduction2.Text = Convert.ToString(StandardDeduction);
                if (50000 >= StandardDeduction)
                {
                    TtxtStandardDeduction2.Text = Convert.ToString(StandardDeduction);
                }
                else
                {
                    TtxtStandardDeduction2.Text = "50000";
                }


                //ApplicationResult ObjUserTempleteOne = new ApplicationResult();
                //ObjUserTempleteOne = objUserPayItemTemplateBl.EmployeeTemplate_SelectForEarning(Convert.ToInt32(ViewState["EmployeeMID"].ToString()));
                //gvDeduction.DataSource = ObjUserTempleteOne.resultDT;
                //gvDeduction.DataBind();
                //int l = 0;
                //foreach (GridViewRow rowItem in gvDeduction.Rows)
                //{
                //    txt = (TextBox)rowItem.Cells[2].FindControl("textAmount");

                //    txt.Text = Convert.ToString(Math.Round(Convert.ToDouble(ObjUserTempleteOne.resultDT.Rows[l]["Amount"].ToString()), 2));
                //    l = l + 1;
                //}
                //j = 0;
                //Double TotalDedction = 0;
                //Double pf = 0;
                //Double Pension = 0;
                //foreach (GridViewRow rowItem in gvDeduction.Rows)
                //{
                //    txt = (TextBox)rowItem.Cells[2].FindControl("textAmount");
                //    //  string strAmount = Convert.ToString(Math.Round(Convert.ToDouble(ObjUserTempleteOne.resultDT.Rows[l]["Amount"].ToString(),0), 0));
                //    TotalDedction = TotalDedction + Convert.ToDouble(txt.Text.ToString());
                //    j = j + 1;
                //    if (Convert.ToInt32(rowItem.Cells[0].Text) == 8)
                //    {
                //        Pension = Math.Round(TotalSalary * Convert.ToDouble(8.3333) / 100, 2);
                //        if (Pension >= 542)
                //        {
                //            txtPension.Text = objControl.ConvertToCurrancy("542");
                //            pf = Math.Round(Convert.ToDouble(Convert.ToDouble(txt.Text.ToString()) - Convert.ToInt32(542)), 2);
                //            txtGpf.Text = objControl.ConvertToCurrancy(pf.ToString());
                //        }
                //        else
                //        {
                //            txtPension.Text = objControl.ConvertToCurrancy(Pension.ToString());
                //            pf = Math.Round(Convert.ToDouble(Convert.ToInt32(txt.Text.ToString()) - Convert.ToInt32(Pension)), 2);
                //            txtGpf.Text = Convert.ToString((pf));
                //        }
                //    }
                //}
                //txtPayTotalDeduction.Text = objControl.ConvertToCurrancy(TotalDedction.ToString());
                //txtPayNetSalary.Text = objControl.ConvertToCurrancy((TotalEarnings - TotalDedction).ToString());
                //txtPayNetSalaryRoundOf.Text = Convert.ToString(Math.Round(Convert.ToDouble(objControl.ConvertToCurrancy((TotalEarnings - TotalDedction).ToString())), 0));
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion
      
        #region gvEmployee RowCommand Event
        protected void gvEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                ViewState["EmployeeMID"] = commandArgs[0];
                //ViewState["SchoolMID"] = commandArgs[1];
                if (e.CommandName.ToString() == "Edit1")
                {
                    hfEmployeeMID.Value = Convert.ToString(e.CommandArgument.ToString());
                    CalculateTDS();                 
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion

        #region No. of months of work
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(ddlMonth.SelectedIndex >= 0)
                {
                    UserTemplateTBl objUserTemplateBl = new UserTemplateTBl();
                    ApplicationResult objTemplateZero = new ApplicationResult();
                    Controls objControl = new Controls();
                    int NoMonths = Convert.ToInt32(ddlMonth.SelectedValue.ToString());

                    objTemplateZero = objUserTemplateBl.EmployeeTemplate_SelectForZero(Convert.ToInt32(ViewState["EmployeeMID"].ToString()), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                    gvEarnings.DataSource = objTemplateZero.resultDT;
                    gvEarnings.DataBind();
                    int a = 0;
                    foreach (GridViewRow rowItem in gvEarnings.Rows)
                    {
                        txt = (TextBox)rowItem.Cells[2].FindControl("textAmount");
                        txt1 = (TextBox)rowItem.Cells[2].FindControl("textAmountOld");

                        double bamt = (Math.Round(Convert.ToDouble(objTemplateZero.resultDT.Rows[a]["Amount"].ToString()), 2)) * NoMonths;
                        txt.Text = Convert.ToString(bamt.ToString()) ;
                        txt1.Text = Convert.ToString(Math.Round(Convert.ToDouble(objTemplateZero.resultDT.Rows[a]["Amount"].ToString()), 2));
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
                    //txtBasicTotal.Text = objControl.ConvertToCurrancy(TotalSalary.ToString());
                    SA = Gross - TotalEarnings;

                    //Gross annual salary
                    txtPayTotalEarnings.Text = objControl.ConvertToCurrancy(TotalEarnings.ToString());

                    GTotal = Convert.ToDouble(objControl.ConvertToCurrancy(TotalEarnings.ToString()));

                    //Extra PayItem
                    PTotal = Convert.ToDouble(txtPayItem3.Text == "" ? "0" : txtPayItem3.Text) + Convert.ToDouble(txtPayItem4.Text == "" ? "0" : txtPayItem4.Text) + Convert.ToDouble(txtPayItem5.Text == "" ? "0" : txtPayItem5.Text) + Convert.ToDouble(txtPayItem6.Text == "" ? "0" : txtPayItem6.Text) + Convert.ToDouble(txtPayItem7.Text == "" ? "0" : txtPayItem7.Text) + GTotal;
                    txtPayTotalEarnings.Text = Convert.ToString(PTotal);

                    //Find Professional Tax Salb Wise
                    int i = Convert.ToInt32(ddlMonth.SelectedValue);
                    if (i >= 1 && i <= 12)
                    {
                        GrossValue = Convert.ToDouble(txtPayTotalEarnings.Text) / i;

                        if (GrossValue <= 6000)
                        {
                            ProfessionalValue = (0 * i);
                            txtProfessionalTax2.Text = Convert.ToString(ProfessionalValue);
                        }
                        else if (GrossValue > 6000 && GrossValue <= 9000)
                        {
                            ProfessionalValue = (80 * i);
                            txtProfessionalTax2.Text = Convert.ToString(ProfessionalValue);
                        }
                        else if (GrossValue > 9000 && GrossValue <= 12000)
                        {
                            ProfessionalValue = (150 * i);
                            txtProfessionalTax2.Text = Convert.ToString(ProfessionalValue);
                        }
                        else if (GrossValue > 12000)
                        {
                            ProfessionalValue = (200 * i);
                            txtProfessionalTax2.Text = Convert.ToString(ProfessionalValue);
                        }
                    }

                    //Find Standard Deduction
                    StandardDeduction = Convert.ToDouble(txtPayTotalEarnings.Text == "" ? "0" : txtPayTotalEarnings.Text) - Convert.ToDouble(txtProfessionalTax2.Text == "" ? "0" : txtProfessionalTax2.Text) - Convert.ToDouble(txtHRAExemption2.Text == "" ? "0" : txtHRAExemption2.Text);
                    TtxtStandardDeduction2.Text = Convert.ToString(StandardDeduction);
                    if (50000 >= StandardDeduction)
                    {
                        TtxtStandardDeduction2.Text = Convert.ToString(StandardDeduction);
                    }
                    else
                    {
                        TtxtStandardDeduction2.Text = "50000";
                    }


                    //else if (i == 1)
                    //{
                    //    GrossValue = Convert.ToDouble(txtPayTotalEarnings.Text) / i;

                    //    if (GrossValue <= 6000)
                    //    {
                    //        ProfessionalValue = (0 * i);
                    //        txtProfessionalTax2.Text = Convert.ToString(ProfessionalValue);
                    //    }
                    //    else if (GrossValue > 6000 && GrossValue <= 9000)
                    //    {
                    //        ProfessionalValue = (80 * i);
                    //        txtProfessionalTax2.Text = Convert.ToString(ProfessionalValue);
                    //    }
                    //    else if (GrossValue > 9000 && GrossValue <= 12000)
                    //    {
                    //        ProfessionalValue = (150 * i);
                    //        txtProfessionalTax2.Text = Convert.ToString(ProfessionalValue);
                    //    }
                    //    else if (GrossValue > 12000)
                    //    {
                    //        ProfessionalValue = (200 * i);
                    //        txtProfessionalTax2.Text = Convert.ToString(ProfessionalValue);
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion

        #region Clear
        protected void btnClear_Click(object sender, EventArgs e)
        {
           
        }
        #endregion

        #region Redo
        protected void btnRedo_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Save
        protected void btnSaveForApprovalTDS_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlAcademicYear.SelectedIndex != 0)
                {
                    TDSCalculatorsBO objTDSCalculatorsBO = new TDSCalculatorsBO();
                    TDSCalculatorsDetailsTBO objTDSCalculatorsDetailsTBO = new TDSCalculatorsDetailsTBO();
                    TDSCalculatorPayItemTBO objTDSCalculatorPayItemTBO = new TDSCalculatorPayItemTBO();
                    TDSCalculatorMediclaimTBO objTDSCalculatorMediclaimTBO = new TDSCalculatorMediclaimTBO();
                    TDSCalculatorsBL objTDSCalculatorsBL = new TDSCalculatorsBL();

                    ApplicationResult objResult1 = new ApplicationResult();
                    ApplicationResult objResult2 = new ApplicationResult();
                    ApplicationResult objResult3 = new ApplicationResult();
                    ApplicationResult objResult4 = new ApplicationResult();
                    ApplicationResult objResultTDSCalculationsID = new ApplicationResult();
                    ApplicationResult objResultTDSCalculationsDetailsID = new ApplicationResult();

                    DataTable dtResult1 = new DataTable();
                    DataTable dtResult2 = new DataTable();
                    DataTable dtResult3 = new DataTable();
                    DataTable dtResult4 = new DataTable();

                    int intTDSCalculatorsID = 0;

                    //TDSCalculators
                    objTDSCalculatorsBO.EmployeeMID = Convert.ToInt32(hfEmployeeMID.Value); 
                    objTDSCalculatorsBO.NoOfMonths = Convert.ToInt32(ddlMonth.SelectedValue);
                    objTDSCalculatorsBO.FinancialYear = Convert.ToString(ddlAcademicYear.SelectedValue.ToString());
                    objTDSCalculatorsBO.OptforSec115BAC = 0; //Pending 
                    objTDSCalculatorsBO.AdditionalEarningsLabel1 = txtPayItemName3.Text.ToString().Trim();
                    objTDSCalculatorsBO.AdditionalEarningsValue1 = Convert.ToDouble(txtPayItem3.Text == "" ? "0" : txtPayItem3.Text);
                    objTDSCalculatorsBO.AdditionalEarningsLabel2 = txtPayItemName4.Text.ToString().Trim();
                    objTDSCalculatorsBO.AdditionalEarningsValue2 = Convert.ToDouble(txtPayItem4.Text == "" ? "0" : txtPayItem4.Text);
                    objTDSCalculatorsBO.AdditionalEarningsLabel3 = txtPayItemName5.Text.ToString().Trim();
                    objTDSCalculatorsBO.AdditionalEarningsValue3 = Convert.ToDouble(txtPayItem5.Text == "" ? "0" : txtPayItem5.Text);
                    objTDSCalculatorsBO.AdditionalEarningsLabel4 = txtPayItemName6.Text.ToString().Trim();
                    objTDSCalculatorsBO.AdditionalEarningsValue4 = Convert.ToDouble(txtPayItem6.Text == "" ? "0" : txtPayItem6.Text);
                    objTDSCalculatorsBO.AdditionalEarningsLabel5 = txtPayItemName7.Text.ToString().Trim();
                    objTDSCalculatorsBO.AdditionalEarningsValue5 = Convert.ToDouble(txtPayItem7.Text == "" ? "0" : txtPayItem7.Text);
                    objTDSCalculatorsBO.BasicSalary = Convert.ToDouble(txtBasicSalary.Text == "" ? "0" : txtBasicSalary.Text);
                    objTDSCalculatorsBO.DearnessAllowance = Convert.ToDouble(txtDA.Text == "" ? "0" : txtDA.Text);
                    objTDSCalculatorsBO.Commision = Convert.ToDouble(txtCommission.Text == "" ? "0" : txtCommission.Text);
                    objTDSCalculatorsBO.HRAReceived = Convert.ToDouble(txthraReceived.Text == "" ? "0" : txthraReceived.Text);
                    objTDSCalculatorsBO.RentPaid = Convert.ToDouble(txtRentPaidHRa.Text == "" ? "0" : txtRentPaidHRa.Text);
                    if (chkCheck.Checked)
                    {
                        objTDSCalculatorsBO.ResidingMetroCity = 1;
                    }
                    else
                    {
                        objTDSCalculatorsBO.ResidingMetroCity = 0;
                    }
                    objTDSCalculatorsBO.OtherIncomeLabel1 = txtIncome1.Text.ToString().Trim();
                    objTDSCalculatorsBO.OtherIncomeValue1 = Convert.ToDouble(txtIncomeAmt1.Text == "" ? "0" : txtIncomeAmt1.Text);
                    objTDSCalculatorsBO.OtherIncomeLabel2 = txtIncome2.Text.ToString().Trim();
                    objTDSCalculatorsBO.OtherIncomeValue2 = Convert.ToDouble(txtIncomeAmt2.Text == "" ? "0" : txtIncomeAmt2.Text);
                    objTDSCalculatorsBO.OtherIncomeLabel3 = txtIncome3.Text.ToString().Trim();
                    objTDSCalculatorsBO.OtherIncomeValue3 = Convert.ToDouble(txtIncomeAmt3.Text == "" ? "0" : txtIncomeAmt3.Text);
                    objTDSCalculatorsBO.OtherIncomeLabel4 = txtIncome4.Text.ToString().Trim();
                    objTDSCalculatorsBO.OtherIncomeValue4 = Convert.ToDouble(txtIncomeAmt4.Text == "" ? "0" : txtIncomeAmt4.Text);
                    objTDSCalculatorsBO.OtherIncomeLabel5 = txtIncome5.Text.ToString().Trim();
                    objTDSCalculatorsBO.OtherIncomeValue5 = Convert.ToDouble(txtIncomeAmt5.Text == "" ? "0" : txtIncomeAmt5.Text);
                    objTDSCalculatorsBO.InterestOfHousingLoan = Convert.ToDouble(txtInterest1.Text == "" ? "0" : txtInterest1.Text);
                    objTDSCalculatorsBO.HomeLoanRepayment = Convert.ToDouble(txtDeduction1.Text == "" ? "0" : txtDeduction1.Text);
                    objTDSCalculatorsBO.LICPremium = Convert.ToDouble(txtDeduction2.Text == "" ? "0" : txtDeduction2.Text);
                    objTDSCalculatorsBO.ELSSMutualFund = Convert.ToDouble(txtDeduction3.Text == "" ? "0" : txtDeduction3.Text);
                    objTDSCalculatorsBO.SchoolTutionFee = Convert.ToDouble(txtDeduction4.Text == "" ? "0" : txtDeduction4.Text);
                    objTDSCalculatorsBO.PPF = Convert.ToDouble(txtDeduction5.Text == "" ? "0" : txtDeduction5.Text);
                    objTDSCalculatorsBO.InvestmentInNPS = Convert.ToDouble(txtNPS.Text == "" ? "0" : txtNPS.Text);
                    objTDSCalculatorsBO.AdvanceTaxPaidbyEmployee = Convert.ToDouble(txtAdvanceTaxPaid.Text == "" ? "0" : txtAdvanceTaxPaid.Text);
                    objTDSCalculatorsBO.TDSDeductedbyOtherEmployerSource = Convert.ToDouble(txtDeductEmp.Text == "" ? "0" : txtDeductEmp.Text);
                    objTDSCalculatorsBO.MonthlyTDSDeducted = Convert.ToDouble(txtMonthlyDeduct.Text == "" ? "0" : txtMonthlyDeduct.Text);

                    //Code For Validate SubHeadingM Name
                    if (ViewState["Mode"].ToString() == "Save")
                    {
                        intTDSCalculatorsID = -1;
                    }
                    else if (ViewState["Mode"].ToString() == "Edit")
                    {
                        intTDSCalculatorsID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                    if (ViewState["Mode"].ToString() == "Save")
                    {
                        //TDSCalculators

                        objTDSCalculatorsBO.IsDeleted = 0;
                        objTDSCalculatorsBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                        objTDSCalculatorsBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        objResult1 = objTDSCalculatorsBL.TDSCalculators_M_Insert(objTDSCalculatorsBO);
                        if (objResult1.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {
                            objResultTDSCalculationsID = objTDSCalculatorsBL.TDSFindID_Last();
                            DataRow dr1 = objResultTDSCalculationsID.resultDT.NewRow();
                            if (objResultTDSCalculationsID.resultDT.Rows.Count > 0)
                            {
                                //Find TDSCalculatorsID
                                int TDSCalculatorsID = Convert.ToInt32(objResultTDSCalculationsID.resultDT.Rows[0][0].ToString()); //Find TDSCalculation_LastId

                                //02 TDSCalculatorsDetails
                                objTDSCalculatorsDetailsTBO.TDSCalculatorsID = TDSCalculatorsID;
                                objTDSCalculatorsDetailsTBO.EmployeeMID = Convert.ToInt32(hfEmployeeMID.Value);
                                objTDSCalculatorsDetailsTBO.GrossTotalSalary = Convert.ToDouble(txtPayTotalEarnings.Text == "" ? "0" : txtPayTotalEarnings.Text);
                                objTDSCalculatorsDetailsTBO.UserTemplateID = 0;
                                objTDSCalculatorsDetailsTBO.ProfessionalTax = Convert.ToDouble(txtProfessionalTax2.Text == "" ? "0" : txtProfessionalTax2.Text);
                                objTDSCalculatorsDetailsTBO.HRAExcemption = Convert.ToDouble(txtHRAExemption2.Text == "" ? "0" : txtHRAExemption2.Text);
                                objTDSCalculatorsDetailsTBO.StandardDeduction = Convert.ToDouble(TtxtStandardDeduction2.Text == "" ? "0" : TtxtStandardDeduction2.Text);
                                objTDSCalculatorsDetailsTBO.TaxableSalary = Convert.ToDouble(txtTaxableSalary2.Text == "" ? "0" : txtTaxableSalary2.Text);
                                objTDSCalculatorsDetailsTBO.OtherIncomeTotal = Convert.ToDouble(txtTotalOtherIncome.Text == "" ? "0" : txtTotalOtherIncome.Text);
                                objTDSCalculatorsDetailsTBO.EligibleAmount1 = Convert.ToDouble(txtEligibleAmt1.Text == "" ? "0" : txtEligibleAmt1.Text);
                                objTDSCalculatorsDetailsTBO.EligibleAmount2 = Convert.ToDouble(txtEligibleAmt2.Text == "" ? "0" : txtEligibleAmt2.Text);
                                //Missing Field = Convert.ToDouble(txtTotalDeduction.Text);
                                objTDSCalculatorsDetailsTBO.GrosTotalIncome = Convert.ToDouble(txtGT.Text == "" ? "0" : txtGT.Text);
                                objTDSCalculatorsDetailsTBO.EligibleDeduction80C = Convert.ToDouble(txtEligibleDeduction1.Text == "" ? "0" : txtEligibleDeduction1.Text);
                                objTDSCalculatorsDetailsTBO.EligibleDeduction80CCD1B = Convert.ToDouble(txtEligibleDeduction2.Text == "" ? "0" : txtEligibleDeduction2.Text);
                                objTDSCalculatorsDetailsTBO.SalaryHRA = Convert.ToDouble(txtHRATotal.Text == "" ? "0" : txtHRATotal.Text);
                                objTDSCalculatorsDetailsTBO.ActualHRAReceived = Convert.ToDouble(lblHRARec.Text == "" ? "0" : lblHRARec.Text);
                                objTDSCalculatorsDetailsTBO.SalaryLess5040 = Convert.ToDouble(lblDeductHRA.Text == "" ? "0" : lblDeductHRA.Text);
                                //Missing Field = Convert.ToDouble(lblRentPaid.Text);
                                objTDSCalculatorsDetailsTBO.ExemptedHRA = Convert.ToDouble(lblExtendedHRA.Text == "" ? "0" : lblExtendedHRA.Text);
                                objTDSCalculatorsDetailsTBO.TaxableHRA = Convert.ToDouble(lblTaxableHRA.Text == "" ? "0" : lblTaxableHRA.Text);
                                objTDSCalculatorsDetailsTBO.EligibleDeduction80D = Convert.ToDouble(txtEligibleDeduction3.Text == "" ? "0" : txtEligibleDeduction3.Text);
                                objTDSCalculatorsDetailsTBO.AvailableDeductions80 = Convert.ToDouble(txtAvailableDeduction.Text == "" ? "0" : txtAvailableDeduction.Text);
                                objTDSCalculatorsDetailsTBO.EligibleDeduction80 = Convert.ToDouble(txtEligibleDeduction4.Text == "" ? "0" : txtEligibleDeduction4.Text);
                                objTDSCalculatorsDetailsTBO.TaxableIncome = Convert.ToDouble(txtTaxableIncome.Text == "" ? "0" : txtTaxableIncome.Text);
                                objTDSCalculatorsDetailsTBO.TaxPayable = Convert.ToDouble(txtTaxPayable.Text == "" ? "0" : txtTaxPayable.Text);
                                objTDSCalculatorsDetailsTBO.Rebate87A = Convert.ToDouble(txtRebate1.Text == "" ? "0" : txtRebate1.Text);
                                objTDSCalculatorsDetailsTBO.TaxPayableRebate87A = Convert.ToDouble(txtRebate2.Text == "" ? "0" : txtRebate2.Text);
                                objTDSCalculatorsDetailsTBO.HealthandEducationCessHEC = Convert.ToDouble(txtHealthEducation.Text == "" ? "0" : txtHealthEducation.Text);
                                objTDSCalculatorsDetailsTBO.TotalTaxPayable = Convert.ToDouble(txtTotalTaxPayable.Text == "" ? "0" : txtTotalTaxPayable.Text);
                                objTDSCalculatorsDetailsTBO.TaxPayablefortheYear = Convert.ToDouble(txtTaxPayableYear.Text == "" ? "0" : txtTaxPayableYear.Text);
                                objTDSCalculatorsDetailsTBO.IsDeleted = 0;
                                objTDSCalculatorsDetailsTBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                                objTDSCalculatorsDetailsTBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);

                                objResult2 = objTDSCalculatorsBL.TDSCalculatorsDetails_T_Insert(objTDSCalculatorsDetailsTBO);

                                if (objResult2.status == ApplicationResult.CommonStatusType.SUCCESS)
                                {
                                    //03 TDSCalculatorMediclaim
                                    objTDSCalculatorMediclaimTBO.TDSCalculatorsID = TDSCalculatorsID;
                                    objTDSCalculatorMediclaimTBO.EmployeeMID = Convert.ToInt32(hfEmployeeMID.Value);
                                    objTDSCalculatorMediclaimTBO.MediclaimPremiumAmtAssesseeCGHS = Convert.ToDouble(txtMed1.Text == "" ? "0" : txtMed1.Text);
                                    objTDSCalculatorMediclaimTBO.MediclaimEligibleAmtAssesseeCGHS = Convert.ToDouble(txtMed2.Text == "" ? "0" : txtMed2.Text);
                                    objTDSCalculatorMediclaimTBO.MediclaimPremiumAmtAssesseeSrCitizen = Convert.ToDouble(txtMed5.Text == "" ? "0" : txtMed5.Text);
                                    objTDSCalculatorMediclaimTBO.MediclaimEligibleAmtAssesseeSrCitizen = Convert.ToDouble(txtMed6.Text == "" ? "0" : txtMed6.Text);
                                    objTDSCalculatorMediclaimTBO.MediclaimPremiumAmtAssesseeHealthCheckUp = Convert.ToDouble(txtMed9.Text == "" ? "0" : txtMed9.Text);
                                    objTDSCalculatorMediclaimTBO.MediclaimEligibleAmtAssesseeHealthCheckUp = Convert.ToDouble(txtMed10.Text == "" ? "0" : txtMed10.Text);
                                    objTDSCalculatorMediclaimTBO.MediclaimPremiumAmtAssesseeMedicalExpenditure = Convert.ToDouble(txtMed13.Text == "" ? "0" : txtMed13.Text);
                                    objTDSCalculatorMediclaimTBO.MediclaimEligibleAmtAssesseeMedicalExpenditure = Convert.ToDouble(txtMed14.Text == "" ? "0" : txtMed14.Text);
                                    objTDSCalculatorMediclaimTBO.MediclaimPremiumAmtAssesseeParentsCGHS = Convert.ToDouble(txtMed3.Text == "" ? "0" : txtMed3.Text);
                                    objTDSCalculatorMediclaimTBO.MediclaimEligibleAmtAssesseeParentsCGHS = Convert.ToDouble(txtMed4.Text == "" ? "0" : txtMed4.Text);
                                    objTDSCalculatorMediclaimTBO.MediclaimPremiumAmtAssesseeParentsSrCitizen = Convert.ToDouble(txtMed7.Text == "" ? "0" : txtMed7.Text);
                                    objTDSCalculatorMediclaimTBO.MediclaimEligibleAmtAssesseeParentsSrCitizen = Convert.ToDouble(txtMed8.Text == "" ? "0" : txtMed8.Text);
                                    objTDSCalculatorMediclaimTBO.MediclaimPremiumAmtAssesseeParentsHealthCheckUp = Convert.ToDouble(txtMed11.Text == "" ? "0" : txtMed11.Text);
                                    objTDSCalculatorMediclaimTBO.MediclaimEligibleAmtAssesseeParentsHealthCheckUp = Convert.ToDouble(txtMed12.Text == "" ? "0" : txtMed12.Text);
                                    objTDSCalculatorMediclaimTBO.MediclaimPremiumAmtAssesseeParentsMedicalExpenditure = Convert.ToDouble(txtMed15.Text == "" ? "0" : txtMed15.Text);
                                    objTDSCalculatorMediclaimTBO.MediclaimEligibleAmtAssesseeParentsMedicalExpenditure = Convert.ToDouble(txtMed16.Text == "" ? "0" : txtMed16.Text);
                                    objTDSCalculatorMediclaimTBO.IsDeleted = 0;
                                    objTDSCalculatorMediclaimTBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                    objTDSCalculatorMediclaimTBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();

                                    objResult3 = objTDSCalculatorsBL.TDSCalculatorMediclaim_T_Insert(objTDSCalculatorMediclaimTBO);

                                    //04 TDSCalculatorPayItem
                                    objResultTDSCalculationsDetailsID = objTDSCalculatorsBL.TDSDetailsFindID_Last();
                                    DataRow dr2 = objResultTDSCalculationsDetailsID.resultDT.NewRow();
                                    int TDSCalculatorsDetailsID = Convert.ToInt32(objResultTDSCalculationsDetailsID.resultDT.Rows[0][0].ToString()); //Find TDSCalculationDetails_LastId

                                    UserTemplateTBl objUserTemplateBl1 = new UserTemplateTBl();
                                    ApplicationResult objTemplateZero1 = new ApplicationResult();
                                    objTemplateZero1 = objUserTemplateBl1.EmployeeTemplate_SelectForZero(Convert.ToInt32(ViewState["EmployeeMID"].ToString()), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                                    Controls objControl1 = new Controls();
                                    if (objTemplateZero1 != null)
                                    {
                                        int a = 0;
                                        foreach (GridViewRow rowItem in gvEarnings.Rows)
                                        {
                                            objTDSCalculatorPayItemTBO.TDSCalculatorsDetailsID = TDSCalculatorsDetailsID;
                                            objTDSCalculatorPayItemTBO.TDSCalculatorsID = TDSCalculatorsID;
                                            objTDSCalculatorPayItemTBO.EmployeeMID = Convert.ToInt32(hfEmployeeMID.Value);
                                            objTDSCalculatorPayItemTBO.UserTemplateID = Convert.ToInt32(objTemplateZero1.resultDT.Rows[a]["UserTemplateID"].ToString());
                                            objTDSCalculatorPayItemTBO.UserPayItemTemplateID = Convert.ToInt32(objTemplateZero1.resultDT.Rows[a]["UserPayItemTemplateID"].ToString());
                                            objTDSCalculatorPayItemTBO.PayItemMID = Convert.ToInt32(objTemplateZero1.resultDT.Rows[a]["PayItemMID"].ToString());
                                            objTDSCalculatorPayItemTBO.Name = Convert.ToString(objTemplateZero1.resultDT.Rows[a]["Name"].ToString());
                                            objTDSCalculatorPayItemTBO.ActualAmount = Math.Round(Convert.ToDouble(objTemplateZero1.resultDT.Rows[a]["Amount"].ToString()), 2);
                                            objTDSCalculatorPayItemTBO.NoOfMonthsMultipliedAmount = Math.Round(Convert.ToDouble(objTemplateZero1.resultDT.Rows[a]["Amount"].ToString()), 2);
                                            objTDSCalculatorPayItemTBO.IsDeleted = 0;
                                            objTDSCalculatorPayItemTBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                            objTDSCalculatorPayItemTBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();

                                            objResult4 = objTDSCalculatorsBL.TDSCalculatorPayItem_T_Insert(objTDSCalculatorPayItemTBO);
                                            a = a + 1;
                                        }
                                    }
                                    if ((objResult4.status == ApplicationResult.CommonStatusType.SUCCESS) && (objResult3.status == ApplicationResult.CommonStatusType.SUCCESS))
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Record Save Successfully.');", true);
                                        Response.Redirect("EmployeeTDSCalculation.aspx");
                                    }
                                    else
                                    {
                                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record Not Save.');</script>");
                                    }
                                }
                                //else
                                //{
                                //    //
                                //}
                            }
                            //else
                            //{
                            //    //
                            //}
                        }
                        //else
                        //{
                        //    //
                        //}
                    }
                    else if (ViewState["Mode"].ToString() == "Edit")
                    {

                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Select Financial Year DropDown.');</script>");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #region textAmout_Change Event in Gridview
        protected void textAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBox = new TextBox();
                
            }
            catch(Exception ex)
            {

            }
        }
        #endregion

        #region TDS Calculation
        //Extra PayItem
        protected void txtPayItem3_TextChanged(object sender, EventArgs e)
        {
            CalculateTDS();
        }
        protected void txtPayItem4_TextChanged(object sender, EventArgs e)
        {
            CalculateTDS();
        }
        protected void txtPayItem5_TextChanged(object sender, EventArgs e)
        {
            CalculateTDS();
        }
        protected void txtPayItem6_TextChanged(object sender, EventArgs e)
        {
            CalculateTDS();
        }
        protected void txtPayItem7_TextChanged(object sender, EventArgs e)
        {
            CalculateTDS();
        }
        //Find Professional Tax Salb Wise
        protected void txtPayTotalEarnings_TextChanged(object sender, EventArgs e)
        {
            CalculateTDS();
        }

        //Find HRA Exemption
        protected void txtBasicSalary_TextChanged(object sender, EventArgs e)
        {
            CalculateHRA();
        }
        protected void txtDA_TextChanged(object sender, EventArgs e)
        {
            CalculateHRA();
        }
        protected void txtCommission_TextChanged(object sender, EventArgs e)
        {
            CalculateHRA();
        }
        protected void txthraReceived_TextChanged(object sender, EventArgs e)
        {
            lblHRARec.Text = Convert.ToString(txthraReceived.Text==""?"0" :txthraReceived.Text);

            //Taxable HRA
            double hrarec = Convert.ToDouble(txthraReceived.Text == "" ? "0" : txthraReceived.Text) - Convert.ToDouble(lblExtendedHRA.Text == "" ? "0" : lblExtendedHRA.Text);

            if (hrarec >= 0)
            {
                lblTaxableHRA.Text = hrarec.ToString();
            }
            else
            {
                lblTaxableHRA.Text = "0";
            }
        }
        protected void chkCheck_CheckedChanged(object sender, EventArgs e)
        {
            //Find HRAExcemption as Without Metro City Wise 40% and Metro City 50%
            
            if (chkCheck.Checked)
            {
                FindHRA = ((Convert.ToDouble(txtHRATotal.Text==""?"0" : txtHRATotal.Text) * 50) / 100);
                lblDeductHRA.Text = FindHRA.ToString();
            }
            else
            { 
                FindHRA = ((Convert.ToDouble(txtHRATotal.Text == "" ? "0" : txtHRATotal.Text) * 40) / 100);
                lblDeductHRA.Text = FindHRA.ToString();
            }

            //Find minimum HRA Excemption
            double a, b, c;
            a = Convert.ToDouble(lblHRARec.Text == "" ? "0" : lblHRARec.Text);
            b = Convert.ToDouble(lblDeductHRA.Text == "" ? "0" : lblDeductHRA.Text);
            c = Convert.ToDouble(lblRentPaid.Text == "" ? "0" : lblRentPaid.Text);
            txtHRAExemption2.Text = lblExtendedHRA.Text = Convert.ToString(Math.Min(a, Math.Min(b, c))); 
        }
        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            divHRAExcemption.Visible = true;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            divHRAExcemption.Visible = false;
        }
        protected void txtRentPaidHRa_TextChanged(object sender, EventArgs e)
        {
            //Rent paid less 10% of Basic salary + DA
            double rp = (Convert.ToDouble(txtRentPaidHRa.Text == "" ? "0" : txtRentPaidHRa.Text) - ((Convert.ToDouble(txtBasicSalary.Text == "" ? "0" : txtBasicSalary.Text) + Convert.ToDouble(txtDA.Text == "" ? "0" : txtDA.Text)) * 10) / 100);

            if (rp >= 0)
            {
                lblRentPaid.Text = rp.ToString();
            }
            else
            {
                lblRentPaid.Text = "0";
            }

            //Find minimum Exempted HRA
            double a, b, c;
            a = Convert.ToDouble(lblHRARec.Text == "" ? "0" : lblHRARec.Text);
            b = Convert.ToDouble(lblDeductHRA.Text == "" ? "0" : lblDeductHRA.Text);
            c = Convert.ToDouble(lblRentPaid.Text == "" ? "0" : lblRentPaid.Text);
            txtHRAExemption2.Text = lblExtendedHRA.Text = Convert.ToString(Math.Min(a, Math.Min(b,c)));

            //Taxable HRA
            double hrarec = Convert.ToDouble(txthraReceived.Text == "" ? "0" : txthraReceived.Text) - Convert.ToDouble(lblExtendedHRA.Text == "" ? "0" : lblExtendedHRA.Text);
            lblTaxableHRA.Text = hrarec.ToString();
            //if (hrarec >= 0)
            //{
            //    lblTaxableHRA.Text = hrarec.ToString();
            //}
            //else
            //{
            //    lblTaxableHRA.Text = "0";
            //}

            //Find Standard Deduction
            StandardDeduction = Convert.ToDouble(txtPayTotalEarnings.Text == "" ? "0" : txtPayTotalEarnings.Text) - Convert.ToDouble(txtProfessionalTax2.Text == "" ? "0" : txtProfessionalTax2.Text) - Convert.ToDouble(txtHRAExemption2.Text == "" ? "0" : txtHRAExemption2.Text);
            TtxtStandardDeduction2.Text = Convert.ToString(StandardDeduction);
            if (50000 >= StandardDeduction)
            {
                TtxtStandardDeduction2.Text = Convert.ToString(StandardDeduction);
            }
            else
            {
                TtxtStandardDeduction2.Text = "50000";
            }
        }
        protected void btnTSalary_Click(object sender, EventArgs e)
        {
            //double taxablesalary = Convert.ToDouble(txtPayTotalEarnings.Text == "" ? "0" : txtPayTotalEarnings.Text) - Convert.ToDouble(txtProfessionalTax2.Text == "" ? "0" : txtProfessionalTax2.Text) - Convert.ToDouble(txtHRAExemption2.Text == "" ? "0" : txtHRAExemption2.Text) - Convert.ToDouble(TtxtStandardDeduction2.Text == "" ? "0" : TtxtStandardDeduction2.Text);
            //txtTaxableSalary2.Text = taxablesalary.ToString();
            if (Convert.ToDecimal(txtTaxableSalary2.Text == "" ? "0" : txtTaxableSalary2.Text) >= 0)
            {
                double taxablesalary = Convert.ToDouble(txtPayTotalEarnings.Text == "" ? "0" : txtPayTotalEarnings.Text) - Convert.ToDouble(txtProfessionalTax2.Text == "" ? "0" : txtProfessionalTax2.Text) - Convert.ToDouble(txtHRAExemption2.Text == "" ? "0" : txtHRAExemption2.Text) - Convert.ToDouble(TtxtStandardDeduction2.Text == "" ? "0" : TtxtStandardDeduction2.Text);
                txtTaxableSalary2.Text = taxablesalary.ToString();
            }
            else
            {
                txtTaxableSalary2.Text = "0";
            }
        }
        #endregion

        #region HRA Calculation
        public void CalculateHRA()
        {
            try
            {
                //Find Salary for the purpose of HRA
                TGSalary = Convert.ToDouble(txtBasicSalary.Text == "" ? "0" : txtBasicSalary.Text) + Convert.ToDouble(txtDA.Text == "" ? "0" : txtDA.Text) + Convert.ToDouble(txtCommission.Text == "" ? "0" : txtCommission.Text);
                txtHRATotal.Text = TGSalary.ToString();

                //Find HRAExcemption as Without Metro City Wise 40%
                FindHRA = ((Convert.ToDouble(txtHRATotal.Text == "" ? "0" : txtHRATotal.Text) * 40) / 100);
                lblDeductHRA.Text = FindHRA.ToString(); 
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }


        #endregion

        #region Add: any other income reported by employee
        protected void txtIncomeAmt1_TextChanged(object sender, EventArgs e)
        {
            OtherIncome();
        }
        protected void txtIncomeAmt2_TextChanged(object sender, EventArgs e)
        {
            OtherIncome();
        }

        protected void txtIncomeAmt3_TextChanged(object sender, EventArgs e)
        {
            OtherIncome();
        }

        protected void txtIncomeAmt4_TextChanged(object sender, EventArgs e)
        {
            OtherIncome();
        }

        protected void txtIncomeAmt5_TextChanged(object sender, EventArgs e)
        {
            OtherIncome();
        }

        public void OtherIncome()
        {
            try
            {
                //Add: any other income reported by employee
                double a, b, c, d, e,OIncome;
                a = Convert.ToDouble(txtIncomeAmt1.Text == "" ? "0" : txtIncomeAmt1.Text);
                b = Convert.ToDouble(txtIncomeAmt2.Text == "" ? "0" : txtIncomeAmt2.Text);
                c = Convert.ToDouble(txtIncomeAmt3.Text == "" ? "0" : txtIncomeAmt3.Text);
                d = Convert.ToDouble(txtIncomeAmt4.Text == "" ? "0" : txtIncomeAmt4.Text);
                e = Convert.ToDouble(txtIncomeAmt5.Text == "" ? "0" : txtIncomeAmt5.Text);

                OIncome = (a + b + c + d + e);
                txtTotalOtherIncome.Text = OIncome.ToString();
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion

        #region Eligible Amount
        protected void txtInterest1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Eligible Amount Left
                double Interest1 = Convert.ToDouble(txtInterest1.Text == "" ? "0" : txtInterest1.Text);
                txtEligibleAmt1.Text = Convert.ToString(Math.Max(Interest1,(-200000)));

                //Eligible Amount Right
                double em1, em2, em3;
                em1 = Convert.ToDouble(txtEligibleAmt1.Text == "" ? "0" : txtEligibleAmt1.Text);
                em2 = Convert.ToDouble(txtTaxableSalary2.Text == "" ? "0" : txtTaxableSalary2.Text);
                em3 = Convert.ToDouble(txtTotalOtherIncome.Text == "" ? "0" : txtTotalOtherIncome.Text);
                txtEligibleAmt2.Text = Convert.ToString(Math.Min(em1,((em2 + em3))));
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion

        #region Find Gross Total
        protected void btnGT_Click(object sender, EventArgs e)
        {
            try
            {
                double gt1, gt2, gt3;
                gt1 = Convert.ToDouble(txtTaxableSalary2.Text == "" ? "0" : txtTaxableSalary2.Text);
                gt2 = Convert.ToDouble(txtTotalOtherIncome.Text == "" ? "0" : txtTotalOtherIncome.Text);
                gt3 = Convert.ToDouble(txtEligibleAmt2.Text == "" ? "0" : txtEligibleAmt2.Text);
                txtGT.Text = Convert.ToString(gt1 + gt2 + gt3);
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion

        #region Deduction & Eligible deduction u/s. 80C
        protected void txtDeduction1_TextChanged(object sender, EventArgs e)
        {
            TotalDeduction();
        }
        protected void txtDeduction2_TextChanged(object sender, EventArgs e)
        {
            TotalDeduction();
        }
        protected void txtDeduction3_TextChanged(object sender, EventArgs e)
        {
            TotalDeduction();
        }
        protected void txtDeduction4_TextChanged(object sender, EventArgs e)
        {
            TotalDeduction();
        }
        protected void txtDeduction5_TextChanged(object sender, EventArgs e)
        {
            TotalDeduction();
        }
        public void TotalDeduction()
        {
            try
            {
                //Add: any other income reported by employee
                double td1, td2, td3, td4, td5, TDeduction;
                td1 = Convert.ToDouble(txtDeduction1.Text == "" ? "0" : txtDeduction1.Text);
                td2 = Convert.ToDouble(txtDeduction2.Text == "" ? "0" : txtDeduction2.Text);
                td3 = Convert.ToDouble(txtDeduction3.Text == "" ? "0" : txtDeduction3.Text);
                td4 = Convert.ToDouble(txtDeduction4.Text == "" ? "0" : txtDeduction4.Text);
                td5 = Convert.ToDouble(txtDeduction5.Text == "" ? "0" : txtDeduction5.Text);

                TDeduction = (td1 + td2 + td3 + td4 + td5);
                
                txtTotalDeduction.Text = TDeduction.ToString();

                //Eligible deduction u/s. 80C               
                txtEligibleDeduction1.Text = Convert.ToString(Math.Min(Convert.ToDouble(txtTotalDeduction.Text == "" ? "0" : txtTotalDeduction.Text), 150000));

            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion

        #region NPS & Eligible deduction u/s. 80CCD(1B)
        protected void txtNPS_TextChanged(object sender, EventArgs e)
        {
            //Eligible deduction u/s. 80CCD(1B)
            txtEligibleDeduction2.Text = Convert.ToString(Math.Min(Convert.ToDouble(txtNPS.Text == "" ? "0" : txtNPS.Text), 50000));
        }

        #endregion

        #region Mediclaim
        //Assessee, spouse, dependent children
        protected void txtMed1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Assessee, spouse, dependent children
                txtMed2.Text = Convert.ToString(Math.Min(Convert.ToDouble(txtMed1.Text == "" ? "0" : txtMed1.Text), 25000));
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        protected void txtMed5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Assessee, spouse, dependent children
                txtMed6.Text = Convert.ToString(Math.Min(Convert.ToDouble(txtMed5.Text == "" ? "0" : txtMed5.Text), 50000));
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        protected void txtMed9_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Assessee, spouse, dependent children
                txtMed10.Text = Convert.ToString(Math.Min(Convert.ToDouble(txtMed9.Text == "" ? "0" : txtMed9.Text), 5000));
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        protected void txtMed13_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Assessee, spouse, dependent children
                txtMed14.Text = Convert.ToString(Math.Min(Convert.ToDouble(txtMed13.Text == "" ? "0" : txtMed13.Text), 50000));
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }      
        //Assessee's parents
        protected void txtMed3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Assessee's parents
                txtMed4.Text = Convert.ToString(Math.Min(Convert.ToDouble(txtMed3.Text == "" ? "0" : txtMed3.Text), 25000));
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        protected void txtMed7_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Assessee's parents
                txtMed8.Text = Convert.ToString(Math.Min(Convert.ToDouble(txtMed7.Text == "" ? "0" : txtMed7.Text), 50000));
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        protected void txtMed11_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Assessee's parents
                txtMed12.Text = Convert.ToString(Math.Min(Convert.ToDouble(txtMed11.Text == "" ? "0" : txtMed11.Text), 5000));
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        protected void txtMed15_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Assessee's parents
                txtMed16.Text = Convert.ToString(Math.Min(Convert.ToDouble(txtMed15.Text == "" ? "0" : txtMed15.Text), 50000)); 
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion

        #region Calculation Final
        protected void btnFinal_Click(object sender, EventArgs e)
        {
            try
            {
                //Eligible deduction u/s.80D 
                double m1, m2, m3, m4, m5, m6, m7, m8;
                m1 = Convert.ToDouble(txtMed2.Text == "" ? "0" : txtMed2.Text);
                m2 = Convert.ToDouble(txtMed6.Text == "" ? "0" : txtMed6.Text);
                m3 = Convert.ToDouble(txtMed10.Text == "" ? "0" : txtMed10.Text);
                m4 = Convert.ToDouble(txtMed14.Text == "" ? "0" : txtMed14.Text);
                m5 = Convert.ToDouble(txtMed4.Text == "" ? "0" : txtMed4.Text);
                m6 = Convert.ToDouble(txtMed8.Text == "" ? "0" : txtMed8.Text);
                m7 = Convert.ToDouble(txtMed12.Text == "" ? "0" : txtMed12.Text);
                m8 = Convert.ToDouble(txtMed16.Text == "" ? "0" : txtMed16.Text);

                txtEligibleDeduction3.Text = Convert.ToString(Math.Min((m1 + m2 + m3 + m4 + m5 + m6 + m7 + m8), 100000));

                //Available deductions u/s.80
                double td1, td2, td3;
                td1 = Convert.ToDouble(txtEligibleDeduction1.Text == "" ? "0" : txtEligibleDeduction1.Text);
                td2 = Convert.ToDouble(txtEligibleDeduction2.Text == "" ? "0" : txtEligibleDeduction2.Text);
                td3 = Convert.ToDouble(txtEligibleDeduction3.Text == "" ? "0" : txtEligibleDeduction3.Text);
                txtAvailableDeduction.Text = Convert.ToString(td1 + td2 + td3);

                //Eligible deduction u/s.80
                txtEligibleDeduction4.Text = Convert.ToString(Math.Min(Convert.ToDouble(txtGT.Text == "" ? "0" : txtGT.Text),Convert.ToDouble(txtAvailableDeduction.Text == "" ? "0" : txtAvailableDeduction.Text)));

                //Taxable income
                txtTaxableIncome.Text = Convert.ToString((+Convert.ToDouble(txtGT.Text == "" ? "0" : txtGT.Text)) - (Convert.ToDouble(txtEligibleDeduction4.Text == "" ? "0" : txtEligibleDeduction4.Text)));

                //Tax Payable
                double pt = 0;
                pt = Convert.ToDouble(txtTaxableIncome.Text == "" ? "0" : txtTaxableIncome.Text);

                if(pt <= 250000)
                {
                    txtTaxPayable.Text = "0";
                }
                else if(pt > 250000 && pt < 500000)
                {
                    txtTaxPayable.Text = Convert.ToString((Convert.ToDouble(pt) - 250000) * 5 / 100);
                }
                else if (pt > 500000 && pt < 1000000)
                {
                    txtTaxPayable.Text = Convert.ToString((Convert.ToDouble(pt) - 250000) * 20 / 100);
                }
                else if (pt > 1000000)
                {
                    txtTaxPayable.Text = Convert.ToString((Convert.ToDouble(pt) - 250000) * 30 / 100);
                }

                //Less: Rebate u/s 87A if Total Income upto Rs.5 L
                double reb = 0;
                reb = Convert.ToDouble(txtTaxableIncome.Text);
                if (reb > 500000)
                {
                    txtRebate1.Text = "0";
                    //txtRebate1.Text = Convert.ToString(Math.Min(Convert.ToDouble(txtTaxPayable.Text == "" ? "0" : txtTaxPayable.Text), 12500));
                }
                else
                {
                    //txtRebate1.Text = "0";
                    txtRebate1.Text = Convert.ToString(Math.Min(Convert.ToDouble(txtTaxPayable.Text == "" ? "0" : txtTaxPayable.Text), 12500));
                }

                //Tax payable after Rebate u/s 87A
                txtRebate2.Text = Convert.ToString(Convert.ToDouble(txtTaxPayable.Text == "" ? "0" : txtTaxPayable.Text) - Convert.ToDouble(txtRebate1.Text == "" ? "0" : txtRebate1.Text));

                //Add: Health and Education Cess (HEC) @ 4%
                txtHealthEducation.Text = Convert.ToString(Math.Round(Convert.ToDouble(txtRebate2.Text == "" ? "0" : txtRebate2.Text) * 4 / 100, 0));

                //Total Tax payable
                txtTotalTaxPayable.Text = Convert.ToString(Convert.ToDouble(txtRebate2.Text == "" ? "0" : txtRebate2.Text) + Convert.ToDouble(txtHealthEducation.Text == "" ? "0" : txtHealthEducation.Text));

              
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion

        #region Find Monthly TDS to be deducted
        protected void MonthlyTDSDeducted_Click(object sender, EventArgs e)
        {
            try
            {
                //Tax payable for the year
                double tpy1 = 0, tpy2 = 0, tpy3 = 0, TAXPAYABLE = 0;
                tpy1 = Convert.ToDouble(txtTotalTaxPayable.Text == "" ? "0" : txtTotalTaxPayable.Text);
                tpy2 = Convert.ToDouble(txtAdvanceTaxPaid.Text == "" ? "0" : txtAdvanceTaxPaid.Text);
                tpy3 = Convert.ToDouble(txtDeductEmp.Text == "" ? "0" : txtDeductEmp.Text);
                TAXPAYABLE = tpy1 - tpy2 - tpy3;
                    //txtTaxPayableYear.Text = Convert.ToString(tpy1 - tpy2 - tpy3);
                if (TAXPAYABLE >= 0)
                {
                    txtTaxPayableYear.Text = Convert.ToString(TAXPAYABLE);
                }
                else
                {
                    txtTaxPayableYear.Text = "0";
                }

                //Monthly TDS to be deducted
                double tdstpy1 = 0, tdstpy2 = 0, MTD = 0;
                tdstpy1 = Convert.ToDouble(txtTaxPayableYear.Text == "" ? "0" : txtTaxPayableYear.Text);
                tdstpy2 = Convert.ToDouble(ddlMonth.SelectedValue);
                MTD = (Math.Round(tdstpy1 / tdstpy2, 0));
                    //txtMonthlyDeduct.Text = Convert.ToString(Math.Round(tdstpy1 / tdstpy2, 0));
                if (MTD >= 0)
                {
                    txtMonthlyDeduct.Text = Convert.ToString(MTD);
                }
                else
                {
                    txtMonthlyDeduct.Text = "0";
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);
            }
        }
        #endregion

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["ID"] = null;
            ViewState["Mode"] = "Save";

        }



        #endregion

        #region Find Financial Year wise TDS Find
        protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int intEmployeeMID = Convert.ToInt32(hfEmployeeMID.Value);
                string strYear = Convert.ToString(ddlAcademicYear.SelectedValue.ToString()); 

                if(intEmployeeMID > 0 && strYear != "" && ddlAcademicYear.SelectedIndex != 0)
                {
                    //Find TDS Calculators Count ID Employee Wise
                    TDSCalculatorsBO objTDSCalculatorsBO = new TDSCalculatorsBO();
                    TDSCalculatorsDetailsTBO objTDSCalculatorsDetailsTBO = new TDSCalculatorsDetailsTBO();
                    TDSCalculatorPayItemTBO objTDSCalculatorPayItemTBO = new TDSCalculatorPayItemTBO();
                    TDSCalculatorMediclaimTBO objTDSCalculatorMediclaimTBO = new TDSCalculatorMediclaimTBO();
                    TDSCalculatorsBL objTDSCalculatorsBL = new TDSCalculatorsBL();

                    ApplicationResult objResultCascade = new ApplicationResult();
                    ApplicationResult objResultFetch1= new ApplicationResult();
                    ApplicationResult objResultFetch2 = new ApplicationResult();
                    ApplicationResult objResultFetch3 = new ApplicationResult();
                    ApplicationResult objResultFetch4 = new ApplicationResult();

                    DataTable dtResultCascade = new DataTable();
                    Controls objControlCascade = new Controls();

                    objResultCascade = objTDSCalculatorsBL.TDSCalculator_Count(intEmployeeMID, strYear);
                    DataRow dr2 = objResultCascade.resultDT.NewRow();
                    int TDSCountID = Convert.ToInt32(objResultCascade.resultDT.Rows[0][0].ToString()); //Find TDSCalculators Count ID
                    if(TDSCountID > 0)
                    {
                        //Fetch TDS Calculators [Employee Wise]
                        btnSaveForApprovalTDS.Enabled = true;
                        ddlAcademicYear.Enabled = false;
                        btnSaveForApprovalTDS.Text = "Record Update";

                        objResultFetch1 = objTDSCalculatorsBL.TDSCalculator_Fetch(intEmployeeMID, strYear);

                        if (objResultFetch1 != null)
                        {
                            DataTable dtResult1 = objResultFetch1.resultDT;
                            if (dtResult1.Rows.Count > 0)
                            {
                                //TDSCalculators
                                FindCalculatorID = Convert.ToInt32(dtResult1.Rows[0]["ID"]);
                                hfEmployeeMID.Value = Convert.ToString(dtResult1.Rows[0]["EmployeeMID"]);

                                //No. of months of work
                                ddlMonth.SelectedValue = Convert.ToString(dtResult1.Rows[0]["NoOfMonths"]);
                                ddlAcademicYear.SelectedValue= Convert.ToString(dtResult1.Rows[0]["FinancialYear"]);
                                //Fetch Whether opting for taxation under Section 115BAC? = objTDSCalculatorsBO.OptforSec115BAC 
                                txtPayItemName3.Text = Convert.ToString(dtResult1.Rows[0]["AdditionalEarningsLabel1"]);
                                //txtPayItem3.Text = Convert.ToString(dtResult.Rows[0][objTDSCalculatorsBO.AdditionalEarningsValue1]); Error
                                txtPayItem3.Text = Convert.ToString(dtResult1.Rows[0]["AdditionalEarningsValue1"]);
                                txtPayItemName4.Text = Convert.ToString(dtResult1.Rows[0]["AdditionalEarningsLabel2"]);
                                txtPayItem4.Text = Convert.ToString(dtResult1.Rows[0]["AdditionalEarningsValue2"]);
                                txtPayItemName5.Text = Convert.ToString(dtResult1.Rows[0]["AdditionalEarningsLabel3"]);
                                txtPayItem5.Text = Convert.ToString(dtResult1.Rows[0]["AdditionalEarningsValue3"]);
                                txtPayItemName6.Text = Convert.ToString(dtResult1.Rows[0]["AdditionalEarningsLabel4"]);
                                txtPayItem6.Text = Convert.ToString(dtResult1.Rows[0]["AdditionalEarningsValue4"]); 
                                txtPayItemName7.Text = Convert.ToString(dtResult1.Rows[0]["AdditionalEarningsLabel5"]);
                                txtPayItem7.Text = Convert.ToString(dtResult1.Rows[0]["AdditionalEarningsValue5"]); 
                                txtBasicSalary.Text = Convert.ToString(dtResult1.Rows[0]["BasicSalary"]);
                                txtDA.Text = Convert.ToString(dtResult1.Rows[0]["DearnessAllowance"]);
                                txtCommission.Text = Convert.ToString(dtResult1.Rows[0]["Commission"]);
                                txthraReceived.Text = Convert.ToString(dtResult1.Rows[0]["HRAReceived"]);
                                txtRentPaidHRa.Text = Convert.ToString(dtResult1.Rows[0]["RentPaid"]);
                                int resiid = Convert.ToInt32(dtResult1.Rows[0]["ResidingMetroCity"]);
                                if (resiid == 1)
                                {
                                    chkCheck.Checked = true;
                                }
                                else
                                {
                                    chkCheck.Checked = false;
                                }
                                txtIncome1.Text = Convert.ToString(dtResult1.Rows[0]["OtherIncomeLabel1"]);
                                txtIncomeAmt1.Text = Convert.ToString(dtResult1.Rows[0]["OtherIncomeValue1"]);
                                txtIncome2.Text = Convert.ToString(dtResult1.Rows[0]["OtherIncomeLabel2"]);
                                txtIncomeAmt2.Text = Convert.ToString(dtResult1.Rows[0]["OtherIncomeValue2"]);
                                txtIncome3.Text = Convert.ToString(dtResult1.Rows[0]["OtherIncomeLabel3"]);
                                txtIncomeAmt3.Text = Convert.ToString(dtResult1.Rows[0]["OtherIncomeValue3"]);
                                txtIncome4.Text = Convert.ToString(dtResult1.Rows[0]["OtherIncomeLabel4"]);
                                txtIncomeAmt4.Text = Convert.ToString(dtResult1.Rows[0]["OtherIncomeValue4"]);
                                txtIncome5.Text = Convert.ToString(dtResult1.Rows[0]["OtherIncomeLabel5"]);
                                txtIncomeAmt5.Text = Convert.ToString(dtResult1.Rows[0]["OtherIncomeValue5"]);
                                txtInterest1.Text = Convert.ToString(dtResult1.Rows[0]["InterestOfHousingLoan"]);
                                txtDeduction1.Text = Convert.ToString(dtResult1.Rows[0]["HomeLoanRepayment"]);
                                txtDeduction2.Text = Convert.ToString(dtResult1.Rows[0]["LICPremium"]);
                                txtDeduction3.Text = Convert.ToString(dtResult1.Rows[0]["ELSSMutualFund"]);
                                txtDeduction4.Text = Convert.ToString(dtResult1.Rows[0]["SchoolTutionFee"]);
                                txtDeduction5.Text = Convert.ToString(dtResult1.Rows[0]["PPF"]);
                                txtNPS.Text = Convert.ToString(dtResult1.Rows[0]["InvestmentInNPS"]);
                                txtAdvanceTaxPaid.Text = Convert.ToString(dtResult1.Rows[0]["AdvanceTaxPaidbyEmployee"]);
                                txtDeductEmp.Text = Convert.ToString(dtResult1.Rows[0]["TDSDeductedbyOtherEmployerSource"]);
                                txtMonthlyDeduct.Text = Convert.ToString(dtResult1.Rows[0]["MonthlyTDSDeducted"]);

                                //02 TDSCalculatorsDetails
                                objResultFetch2 = objTDSCalculatorsBL.TDSCalculatorDetails_Fetch(FindCalculatorID, intEmployeeMID);
                                if (objResultFetch2 != null)
                                {
                                    DataTable dtResult2 = objResultFetch2.resultDT;
                                    if (dtResult2.Rows.Count > 0)
                                    {
                                        FindCalculatorDetailsID = Convert.ToInt32(dtResult2.Rows[0]["ID"]); //Find CalculatorsDetailsID
                                        //FindCalculatorID = Convert.ToInt32(dtResult2.Rows[0]["TDSCalculatorsID"]);
                                        //FindEmployeeMID = Convert.ToInt32(dtResult2.Rows[0]["EmployeeMID"]);
                                        //FindUserTEmplateID = Convert.ToInt32(dtResult2.Rows[0]["UserTemplateID"]);

                                        txtPayTotalEarnings.Text = Convert.ToString(dtResult2.Rows[0]["GrossTotalSalary"]);
                                        txtProfessionalTax2.Text = Convert.ToString(dtResult2.Rows[0]["ProfessionalTax"]);
                                        txtHRAExemption2.Text = Convert.ToString(dtResult2.Rows[0]["HRAExcemption"]);
                                        TtxtStandardDeduction2.Text = Convert.ToString(dtResult2.Rows[0]["StandardDeduction"]);
                                        txtTaxableSalary2.Text = Convert.ToString(dtResult2.Rows[0]["TaxableSalary"]);
                                        txtTotalOtherIncome.Text = Convert.ToString(dtResult2.Rows[0]["OtherIncomeTotal"]);
                                        txtEligibleAmt1.Text = Convert.ToString(dtResult2.Rows[0]["EligibleAmount1"]);
                                        txtEligibleAmt2.Text = Convert.ToString(dtResult2.Rows[0]["EligibleAmount2"]);
                                        //txtTotalDeduction.Text =  Convert.ToString(dtResult2.Rows[0]["TotalDeduction"]); Missing
                                        txtGT.Text = Convert.ToString(dtResult2.Rows[0]["GrosTotalIncome"]);
                                        txtEligibleDeduction1.Text = Convert.ToString(dtResult2.Rows[0]["EligibleDeduction80C"]);
                                        txtEligibleDeduction2.Text = Convert.ToString(dtResult2.Rows[0]["EligibleDeduction80CCD1B"]);
                                        txtHRATotal.Text = Convert.ToString(dtResult2.Rows[0]["SalaryHRA"]);
                                        lblHRARec.Text = Convert.ToString(dtResult2.Rows[0]["ActualHRAReceived"]);
                                        lblDeductHRA.Text = Convert.ToString(dtResult2.Rows[0]["SalaryLess5040"]);
                                        //lblRentPaid.Text =  Convert.ToString(dtResult2.Rows[0]["TRentPaid"]); Missing
                                        lblExtendedHRA.Text = Convert.ToString(dtResult2.Rows[0]["ExemptedHRA"]);
                                        lblTaxableHRA.Text = Convert.ToString(dtResult2.Rows[0]["TaxableHRA"]);
                                        txtEligibleDeduction3.Text = Convert.ToString(dtResult2.Rows[0]["EligibleDeduction80D"]);
                                        txtAvailableDeduction.Text = Convert.ToString(dtResult2.Rows[0]["AvailableDeductions80"]);
                                        txtEligibleDeduction4.Text = Convert.ToString(dtResult2.Rows[0]["EligibleDeduction80"]);
                                        txtTaxableIncome.Text = Convert.ToString(dtResult2.Rows[0]["TaxableIncome"]);
                                        txtTaxPayable.Text = Convert.ToString(dtResult2.Rows[0]["TaxPayable"]);
                                        txtRebate1.Text = Convert.ToString(dtResult2.Rows[0]["Rebate87A"]);
                                        txtRebate2.Text = Convert.ToString(dtResult2.Rows[0]["TaxPayableRebate87A"]);
                                        txtHealthEducation.Text = Convert.ToString(dtResult2.Rows[0]["HealthandEducationCessHEC"]);
                                        txtTotalTaxPayable.Text = Convert.ToString(dtResult2.Rows[0]["TotalTaxPayable"]);
                                        txtTaxPayableYear.Text = Convert.ToString(dtResult2.Rows[0]["TaxPayablefortheYear"]);
                                    }

                                    //03 TDSCalculatorMediclaim
                                    objResultFetch3 = objTDSCalculatorsBL.TDSCalculatorMediclaim_Fetch(FindCalculatorID, intEmployeeMID);
                                    if (objResultFetch3 != null)
                                    {
                                        DataTable dtResult3 = objResultFetch3.resultDT;
                                        if (dtResult3.Rows.Count > 0)
                                        {
                                            //FindCalculatorMediclaimsID = Convert.ToInt32(dtResult3.Rows[0]["ID"]); //Find CalculatorMediclaimID
                                            //FindCalculatorsID = Convert.ToInt32(dtResult3.Rows[0]["TDSCalculatorsID"]); //Find TDSCalculatorsID
                                            //FindEmployeeMID = Convert.ToInt32(dtResult3.Rows[0]["EmployeeMID"]); //Find EmployeeMID
                                            txtMed1.Text = Convert.ToString(dtResult3.Rows[0]["MediclaimPremiumAmtAssesseeCGHS"]);
                                            txtMed2.Text = Convert.ToString(dtResult3.Rows[0]["MediclaimEligibleAmtAssesseeCGHS"]); 
                                            txtMed5.Text = Convert.ToString(dtResult3.Rows[0]["MediclaimPremiumAmtAssesseeSrCitizen"]);
                                            txtMed6.Text = Convert.ToString(dtResult3.Rows[0]["MediclaimEligibleAmtAssesseeSrCitizen"]);
                                            txtMed9.Text = Convert.ToString(dtResult3.Rows[0]["MediclaimPremiumAmtAssesseeHealthCheckUp"]);
                                            txtMed10.Text = Convert.ToString(dtResult3.Rows[0]["MediclaimEligibleAmtAssesseeHealthCheckUp"]);
                                            txtMed13.Text = Convert.ToString(dtResult3.Rows[0]["MediclaimPremiumAmtAssesseeMedicalExpenditure"]);
                                            txtMed14.Text = Convert.ToString(dtResult3.Rows[0]["MediclaimEligibleAmtAssesseeMedicalExpenditure"]);
                                            txtMed3.Text = Convert.ToString(dtResult3.Rows[0]["MediclaimPremiumAmtAssesseeParentsCGHS"]);
                                            txtMed4.Text = Convert.ToString(dtResult3.Rows[0]["MediclaimEligibleAmtAssesseeParentsCGHS"]);
                                            txtMed7.Text = Convert.ToString(dtResult3.Rows[0]["MediclaimPremiumAmtAssesseeParentsSrCitizen"]);
                                            txtMed8.Text = Convert.ToString(dtResult3.Rows[0]["MediclaimEligibleAmtAssesseeParentsSrCitizen"]);
                                            txtMed11.Text = Convert.ToString(dtResult3.Rows[0]["MediclaimPremiumAmtAssesseeParentsHealthCheckUp"]);
                                            txtMed12.Text = Convert.ToString(dtResult3.Rows[0]["MediclaimEligibleAmtAssesseeParentsHealthCheckUp"]);
                                            txtMed15.Text = Convert.ToString(dtResult3.Rows[0]["MediclaimPremiumAmtAssesseeParentsMedicalExpenditure"]);
                                            txtMed16.Text = Convert.ToString(dtResult3.Rows[0]["MediclaimEligibleAmtAssesseeParentsMedicalExpenditure"]);
                                        }
                                    }

                                    //04 TDSCalculatorPayItem
                                    objResultFetch4 = objTDSCalculatorsBL.TDSCalculatorPayItem_Fetch(FindCalculatorDetailsID, FindCalculatorID, intEmployeeMID);
                                    if (objResultFetch4 != null)
                                    {
                                        //Bind Grid as per Employee wise TDSCalculators assign PayItem
                                        gvEarnings.DataSource = objResultFetch4.resultDT;
                                        ddlMonth_SelectedIndexChanged(sender, e);
                                        //gvEarnings.DataBind();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        btnSaveForApprovalTDS.Enabled = true;
                        ddlAcademicYear.Enabled = true;
                        btnSaveForApprovalTDS.Text = "Record Save";
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
    }
}