using System;
using System.Data;
using System.Data.SqlClient;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
    public class TDSCalculatorsBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        #region Insert TDSCalculators 
        public ApplicationResult TDSCalculators_M_Insert(TDSCalculatorsBO objTDSCalculatorsBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[44];

                pSqlParameter[0] = new SqlParameter("@ID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objTDSCalculatorsBO.ID;

                pSqlParameter[1] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objTDSCalculatorsBO.EmployeeMID;

                pSqlParameter[2] = new SqlParameter("@NoOfMonths", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objTDSCalculatorsBO.NoOfMonths;

                pSqlParameter[3] = new SqlParameter("@FinancialYear", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objTDSCalculatorsBO.FinancialYear;

                pSqlParameter[4] = new SqlParameter("@OptforSec115BAC", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objTDSCalculatorsBO.OptforSec115BAC;

                pSqlParameter[5] = new SqlParameter("@AdditionalEarningsLabel1", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objTDSCalculatorsBO.AdditionalEarningsLabel1;

                pSqlParameter[6] = new SqlParameter("@AdditionalEarningsValue1", SqlDbType.Float);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objTDSCalculatorsBO.AdditionalEarningsValue1;

                pSqlParameter[7] = new SqlParameter("@AdditionalEarningsLabel2", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objTDSCalculatorsBO.AdditionalEarningsLabel2;

                pSqlParameter[8] = new SqlParameter("@AdditionalEarningsValue2", SqlDbType.Float);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objTDSCalculatorsBO.AdditionalEarningsValue2;

                pSqlParameter[9] = new SqlParameter("@AdditionalEarningsLabel3", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objTDSCalculatorsBO.AdditionalEarningsLabel3;

                pSqlParameter[10] = new SqlParameter("@AdditionalEarningsValue3", SqlDbType.Float);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objTDSCalculatorsBO.AdditionalEarningsValue3;

                pSqlParameter[11] = new SqlParameter("@AdditionalEarningsLabel4", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objTDSCalculatorsBO.AdditionalEarningsLabel4;

                pSqlParameter[12] = new SqlParameter("@AdditionalEarningsValue4", SqlDbType.Float);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objTDSCalculatorsBO.AdditionalEarningsValue4;

                pSqlParameter[13] = new SqlParameter("@AdditionalEarningsLabel5", SqlDbType.VarChar);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objTDSCalculatorsBO.AdditionalEarningsLabel5;

                pSqlParameter[14] = new SqlParameter("@AdditionalEarningsValue5", SqlDbType.Float);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objTDSCalculatorsBO.AdditionalEarningsValue5;

                pSqlParameter[15] = new SqlParameter("@BasicSalary", SqlDbType.Float);
                pSqlParameter[15].Direction = ParameterDirection.Input;
                pSqlParameter[15].Value = objTDSCalculatorsBO.BasicSalary;

                pSqlParameter[16] = new SqlParameter("@DearnessAllowance", SqlDbType.Float);
                pSqlParameter[16].Direction = ParameterDirection.Input;
                pSqlParameter[16].Value = objTDSCalculatorsBO.DearnessAllowance;

                pSqlParameter[17] = new SqlParameter("@Commission", SqlDbType.Float);
                pSqlParameter[17].Direction = ParameterDirection.Input;
                pSqlParameter[17].Value = objTDSCalculatorsBO.Commision;

                pSqlParameter[18] = new SqlParameter("@HRAReceived", SqlDbType.Float);
                pSqlParameter[18].Direction = ParameterDirection.Input;
                pSqlParameter[18].Value = objTDSCalculatorsBO.HRAReceived;

                pSqlParameter[19] = new SqlParameter("@RentPaid", SqlDbType.Float);
                pSqlParameter[19].Direction = ParameterDirection.Input;
                pSqlParameter[19].Value = objTDSCalculatorsBO.RentPaid;

                pSqlParameter[20] = new SqlParameter("@ResidingMetroCity", SqlDbType.Float);
                pSqlParameter[20].Direction = ParameterDirection.Input;
                pSqlParameter[20].Value = objTDSCalculatorsBO.ResidingMetroCity;

                pSqlParameter[21] = new SqlParameter("@OtherIncomeLabel1", SqlDbType.VarChar);
                pSqlParameter[21].Direction = ParameterDirection.Input;
                pSqlParameter[21].Value = objTDSCalculatorsBO.OtherIncomeLabel1;

                pSqlParameter[22] = new SqlParameter("@OtherIncomeValue1", SqlDbType.Float);
                pSqlParameter[22].Direction = ParameterDirection.Input;
                pSqlParameter[22].Value = objTDSCalculatorsBO.OtherIncomeValue1;

                pSqlParameter[23] = new SqlParameter("@OtherIncomeLabel2", SqlDbType.VarChar);
                pSqlParameter[23].Direction = ParameterDirection.Input;
                pSqlParameter[23].Value = objTDSCalculatorsBO.OtherIncomeLabel2;

                pSqlParameter[24] = new SqlParameter("@OtherIncomeValue2", SqlDbType.Float);
                pSqlParameter[24].Direction = ParameterDirection.Input;
                pSqlParameter[24].Value = objTDSCalculatorsBO.OtherIncomeValue2;

                pSqlParameter[25] = new SqlParameter("@OtherIncomeLabel3", SqlDbType.VarChar);
                pSqlParameter[25].Direction = ParameterDirection.Input;
                pSqlParameter[25].Value = objTDSCalculatorsBO.OtherIncomeLabel3;

                pSqlParameter[26] = new SqlParameter("@OtherIncomeValue3", SqlDbType.Float);
                pSqlParameter[26].Direction = ParameterDirection.Input;
                pSqlParameter[26].Value = objTDSCalculatorsBO.OtherIncomeValue3;

                pSqlParameter[27] = new SqlParameter("@OtherIncomeLabel4", SqlDbType.VarChar);
                pSqlParameter[27].Direction = ParameterDirection.Input;
                pSqlParameter[27].Value = objTDSCalculatorsBO.OtherIncomeLabel4;

                pSqlParameter[28] = new SqlParameter("@OtherIncomeValue4", SqlDbType.Float);
                pSqlParameter[28].Direction = ParameterDirection.Input;
                pSqlParameter[28].Value = objTDSCalculatorsBO.OtherIncomeValue4;

                pSqlParameter[29] = new SqlParameter("@OtherIncomeLabel5", SqlDbType.VarChar);
                pSqlParameter[29].Direction = ParameterDirection.Input;
                pSqlParameter[29].Value = objTDSCalculatorsBO.OtherIncomeLabel5;

                pSqlParameter[30] = new SqlParameter("@OtherIncomeValue5", SqlDbType.Float);
                pSqlParameter[30].Direction = ParameterDirection.Input;
                pSqlParameter[30].Value = objTDSCalculatorsBO.OtherIncomeValue5;

                pSqlParameter[31] = new SqlParameter("@InterestOfHousingLoan", SqlDbType.Float);
                pSqlParameter[31].Direction = ParameterDirection.Input;
                pSqlParameter[31].Value = objTDSCalculatorsBO.InterestOfHousingLoan;

                pSqlParameter[32] = new SqlParameter("@HomeLoanRepayment", SqlDbType.Float);
                pSqlParameter[32].Direction = ParameterDirection.Input;
                pSqlParameter[32].Value = objTDSCalculatorsBO.HomeLoanRepayment;

                pSqlParameter[33] = new SqlParameter("@LICPremium", SqlDbType.Float);
                pSqlParameter[33].Direction = ParameterDirection.Input;
                pSqlParameter[33].Value = objTDSCalculatorsBO.LICPremium;

                pSqlParameter[34] = new SqlParameter("@ELSSMutualFund", SqlDbType.Float);
                pSqlParameter[34].Direction = ParameterDirection.Input;
                pSqlParameter[34].Value = objTDSCalculatorsBO.ELSSMutualFund;

                pSqlParameter[35] = new SqlParameter("@SchoolTutionFee", SqlDbType.Float);
                pSqlParameter[35].Direction = ParameterDirection.Input;
                pSqlParameter[35].Value = objTDSCalculatorsBO.SchoolTutionFee;

                pSqlParameter[36] = new SqlParameter("@PPF", SqlDbType.Float);
                pSqlParameter[36].Direction = ParameterDirection.Input;
                pSqlParameter[36].Value = objTDSCalculatorsBO.PPF;

                pSqlParameter[37] = new SqlParameter("@InvestmentInNPS", SqlDbType.Float);
                pSqlParameter[37].Direction = ParameterDirection.Input;
                pSqlParameter[37].Value = objTDSCalculatorsBO.InvestmentInNPS;

                pSqlParameter[38] = new SqlParameter("@AdvanceTaxPaidbyEmployee", SqlDbType.Float);
                pSqlParameter[38].Direction = ParameterDirection.Input;
                pSqlParameter[38].Value = objTDSCalculatorsBO.AdvanceTaxPaidbyEmployee;

                pSqlParameter[39] = new SqlParameter("@TDSDeductedbyOtherEmployerSource", SqlDbType.Float);
                pSqlParameter[39].Direction = ParameterDirection.Input;
                pSqlParameter[39].Value = objTDSCalculatorsBO.TDSDeductedbyOtherEmployerSource;

                pSqlParameter[40] = new SqlParameter("@MonthlyTDSDeducted", SqlDbType.Float);
                pSqlParameter[40].Direction = ParameterDirection.Input;
                pSqlParameter[40].Value = objTDSCalculatorsBO.MonthlyTDSDeducted;

                pSqlParameter[41] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[41].Direction = ParameterDirection.Input;
                pSqlParameter[41].Value = objTDSCalculatorsBO.IsDeleted;

                pSqlParameter[42] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[42].Direction = ParameterDirection.Input;
                pSqlParameter[42].Value = objTDSCalculatorsBO.CreatedUserID;

                pSqlParameter[43] = new SqlParameter("@CreatedDate", SqlDbType.Date);
                pSqlParameter[43].Direction = ParameterDirection.Input;
                pSqlParameter[43].Value = objTDSCalculatorsBO.CreatedDate;

                sSql = "usp_tbl_TDSCalculators_M_Insert";
                int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, sSql, pSqlParameter);

                ////DataTable dtResult = new DataTable();
                ////dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);
                ////ApplicationResult objResults = new ApplicationResult(dtResult);
                ////objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                ////return objResults;

                if (iResult > 0)
                {
                    ApplicationResult objResults = new ApplicationResult();
                    objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                    return objResults;
                }
                else
                {
                    ApplicationResult objResults = new ApplicationResult();
                    objResults.status = ApplicationResult.CommonStatusType.FAILURE;
                    return objResults;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objTDSCalculatorsBO = null;
            }
        }
        #endregion

        #region Insert TDSCalculatorsDetails 
        public ApplicationResult TDSCalculatorsDetails_T_Insert(TDSCalculatorsDetailsTBO objTDSCalculatorsDetailsTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[33];

                pSqlParameter[0] = new SqlParameter("@ID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objTDSCalculatorsDetailsTBO.ID;

                pSqlParameter[1] = new SqlParameter("@TDSCalculatorsID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objTDSCalculatorsDetailsTBO.TDSCalculatorsID;

                pSqlParameter[2] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objTDSCalculatorsDetailsTBO.EmployeeMID;

                pSqlParameter[3] = new SqlParameter("@UserTemplateID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objTDSCalculatorsDetailsTBO.UserTemplateID;

                pSqlParameter[4] = new SqlParameter("@GrossTotalSalary", SqlDbType.Float);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objTDSCalculatorsDetailsTBO.GrossTotalSalary;

                pSqlParameter[5] = new SqlParameter("@ProfessionalTax", SqlDbType.Float);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objTDSCalculatorsDetailsTBO.ProfessionalTax;

                pSqlParameter[6] = new SqlParameter("@HRAExcemption", SqlDbType.Float);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objTDSCalculatorsDetailsTBO.HRAExcemption;

                pSqlParameter[7] = new SqlParameter("@StandardDeduction", SqlDbType.Float);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objTDSCalculatorsDetailsTBO.StandardDeduction;

                pSqlParameter[8] = new SqlParameter("@TaxableSalary", SqlDbType.Float);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objTDSCalculatorsDetailsTBO.TaxableSalary;

                pSqlParameter[9] = new SqlParameter("@OtherIncomeTotal", SqlDbType.Float);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objTDSCalculatorsDetailsTBO.OtherIncomeTotal;

                pSqlParameter[10] = new SqlParameter("@EligibleAmount1", SqlDbType.Float);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objTDSCalculatorsDetailsTBO.EligibleAmount1;

                pSqlParameter[11] = new SqlParameter("@EligibleAmount2", SqlDbType.Float);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objTDSCalculatorsDetailsTBO.EligibleAmount2;

                pSqlParameter[12] = new SqlParameter("@GrosTotalIncome", SqlDbType.Float);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objTDSCalculatorsDetailsTBO.GrosTotalIncome;

                pSqlParameter[13] = new SqlParameter("@EligibleDeduction80C", SqlDbType.Float);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objTDSCalculatorsDetailsTBO.EligibleDeduction80C;

                pSqlParameter[14] = new SqlParameter("@EligibleDeduction80CCD1B", SqlDbType.Float);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objTDSCalculatorsDetailsTBO.EligibleDeduction80CCD1B;

                pSqlParameter[15] = new SqlParameter("@SalaryHRA", SqlDbType.Float);
                pSqlParameter[15].Direction = ParameterDirection.Input;
                pSqlParameter[15].Value = objTDSCalculatorsDetailsTBO.SalaryHRA;

                pSqlParameter[16] = new SqlParameter("@ActualHRAReceived", SqlDbType.Float);
                pSqlParameter[16].Direction = ParameterDirection.Input;
                pSqlParameter[16].Value = objTDSCalculatorsDetailsTBO.ActualHRAReceived;

                pSqlParameter[17] = new SqlParameter("@SalaryLess5040", SqlDbType.Float);
                pSqlParameter[17].Direction = ParameterDirection.Input;
                pSqlParameter[17].Value = objTDSCalculatorsDetailsTBO.SalaryLess5040;

                pSqlParameter[18] = new SqlParameter("@ExemptedHRA", SqlDbType.Float);
                pSqlParameter[18].Direction = ParameterDirection.Input;
                pSqlParameter[18].Value = objTDSCalculatorsDetailsTBO.ExemptedHRA;

                pSqlParameter[19] = new SqlParameter("@TaxableHRA", SqlDbType.Float);
                pSqlParameter[19].Direction = ParameterDirection.Input;
                pSqlParameter[19].Value = objTDSCalculatorsDetailsTBO.TaxableHRA;
              
                pSqlParameter[20] = new SqlParameter("@EligibleDeduction80D", SqlDbType.Float);
                pSqlParameter[20].Direction = ParameterDirection.Input;
                pSqlParameter[20].Value = objTDSCalculatorsDetailsTBO.EligibleDeduction80D;

                pSqlParameter[21] = new SqlParameter("@AvailableDeductions80", SqlDbType.Float);
                pSqlParameter[21].Direction = ParameterDirection.Input;
                pSqlParameter[21].Value = objTDSCalculatorsDetailsTBO.AvailableDeductions80;

                pSqlParameter[22] = new SqlParameter("@EligibleDeduction80", SqlDbType.Float);
                pSqlParameter[22].Direction = ParameterDirection.Input;
                pSqlParameter[22].Value = objTDSCalculatorsDetailsTBO.EligibleDeduction80;

                pSqlParameter[23] = new SqlParameter("@TaxableIncome", SqlDbType.Float);
                pSqlParameter[23].Direction = ParameterDirection.Input;
                pSqlParameter[23].Value = objTDSCalculatorsDetailsTBO.TaxableIncome;

                pSqlParameter[24] = new SqlParameter("@TaxPayable", SqlDbType.Float);
                pSqlParameter[24].Direction = ParameterDirection.Input;
                pSqlParameter[24].Value = objTDSCalculatorsDetailsTBO.TaxPayable;

                pSqlParameter[25] = new SqlParameter("@Rebate87A", SqlDbType.Float);
                pSqlParameter[25].Direction = ParameterDirection.Input;
                pSqlParameter[25].Value = objTDSCalculatorsDetailsTBO.Rebate87A;

                pSqlParameter[26] = new SqlParameter("@TaxPayableRebate87A", SqlDbType.Float);
                pSqlParameter[26].Direction = ParameterDirection.Input;
                pSqlParameter[26].Value = objTDSCalculatorsDetailsTBO.TaxPayableRebate87A;

                pSqlParameter[27] = new SqlParameter("@HealthandEducationCessHEC", SqlDbType.Float);
                pSqlParameter[27].Direction = ParameterDirection.Input;
                pSqlParameter[27].Value = objTDSCalculatorsDetailsTBO.HealthandEducationCessHEC;

                pSqlParameter[28] = new SqlParameter("@TotalTaxPayable", SqlDbType.Float);
                pSqlParameter[28].Direction = ParameterDirection.Input;
                pSqlParameter[28].Value = objTDSCalculatorsDetailsTBO.TotalTaxPayable;

                pSqlParameter[29] = new SqlParameter("@TaxPayablefortheYear", SqlDbType.Float);
                pSqlParameter[29].Direction = ParameterDirection.Input;
                pSqlParameter[29].Value = objTDSCalculatorsDetailsTBO.TaxPayablefortheYear;

                pSqlParameter[30] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[30].Direction = ParameterDirection.Input;
                pSqlParameter[30].Value = objTDSCalculatorsDetailsTBO.IsDeleted;

                pSqlParameter[31] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[31].Direction = ParameterDirection.Input;
                pSqlParameter[31].Value = objTDSCalculatorsDetailsTBO.CreatedUserID;

                pSqlParameter[32] = new SqlParameter("@CreatedDate", SqlDbType.Date);
                pSqlParameter[32].Direction = ParameterDirection.Input;
                pSqlParameter[32].Value = objTDSCalculatorsDetailsTBO.CreatedDate;

                sSql = "usp_tbl_TDSCalculatorsDetails_T_Insert";
                int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, sSql, pSqlParameter);

                ////DataTable dtResult = new DataTable();
                ////dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);
                ////ApplicationResult objResults = new ApplicationResult(dtResult);
                ////objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                ////return objResults;

                if (iResult > 0)
                {
                    ApplicationResult objResults = new ApplicationResult();
                    objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                    return objResults;
                }
                else
                {
                    ApplicationResult objResults = new ApplicationResult();
                    objResults.status = ApplicationResult.CommonStatusType.FAILURE;
                    return objResults;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objTDSCalculatorsDetailsTBO = null;
            }
        }
        #endregion

        #region Insert TDSCalculatorMediclaim 
        public ApplicationResult TDSCalculatorMediclaim_T_Insert(TDSCalculatorMediclaimTBO objTDSCalculatorMediclaimTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[22];

                pSqlParameter[0] = new SqlParameter("@ID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objTDSCalculatorMediclaimTBO.ID;

                pSqlParameter[1] = new SqlParameter("@TDSCalculatorsID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objTDSCalculatorMediclaimTBO.TDSCalculatorsID;

                pSqlParameter[2] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objTDSCalculatorMediclaimTBO.EmployeeMID;

                pSqlParameter[3] = new SqlParameter("@MediclaimPremiumAmtAssesseeCGHS", SqlDbType.Float);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objTDSCalculatorMediclaimTBO.MediclaimPremiumAmtAssesseeCGHS;

                pSqlParameter[4] = new SqlParameter("@MediclaimEligibleAmtAssesseeCGHS", SqlDbType.Float);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objTDSCalculatorMediclaimTBO.MediclaimEligibleAmtAssesseeCGHS;

                pSqlParameter[5] = new SqlParameter("@MediclaimPremiumAmtAssesseeSrCitizen", SqlDbType.Float);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objTDSCalculatorMediclaimTBO.MediclaimPremiumAmtAssesseeSrCitizen;

                pSqlParameter[6] = new SqlParameter("@MediclaimEligibleAmtAssesseeSrCitizen", SqlDbType.Float);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objTDSCalculatorMediclaimTBO.MediclaimEligibleAmtAssesseeSrCitizen;

                pSqlParameter[7] = new SqlParameter("@MediclaimPremiumAmtAssesseeHealthCheckUp", SqlDbType.Float);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objTDSCalculatorMediclaimTBO.MediclaimPremiumAmtAssesseeHealthCheckUp;

                pSqlParameter[8] = new SqlParameter("@MediclaimEligibleAmtAssesseeHealthCheckUp", SqlDbType.Float);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objTDSCalculatorMediclaimTBO.MediclaimEligibleAmtAssesseeHealthCheckUp;

                pSqlParameter[9] = new SqlParameter("@MediclaimPremiumAmtAssesseeMedicalExpenditure", SqlDbType.Float);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objTDSCalculatorMediclaimTBO.MediclaimPremiumAmtAssesseeMedicalExpenditure;
         
                pSqlParameter[10] = new SqlParameter("@MediclaimEligibleAmtAssesseeMedicalExpenditure", SqlDbType.Float);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objTDSCalculatorMediclaimTBO.MediclaimEligibleAmtAssesseeMedicalExpenditure;

                pSqlParameter[11] = new SqlParameter("@MediclaimPremiumAmtAssesseeParentsCGHS", SqlDbType.Float);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objTDSCalculatorMediclaimTBO.MediclaimPremiumAmtAssesseeParentsCGHS;

                pSqlParameter[12] = new SqlParameter("@MediclaimEligibleAmtAssesseeParentsCGHS", SqlDbType.Float);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objTDSCalculatorMediclaimTBO.MediclaimEligibleAmtAssesseeParentsCGHS;

                pSqlParameter[13] = new SqlParameter("@MediclaimPremiumAmtAssesseeParentsSrCitizen", SqlDbType.Float);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objTDSCalculatorMediclaimTBO.MediclaimPremiumAmtAssesseeParentsSrCitizen;

                pSqlParameter[14] = new SqlParameter("@MediclaimEligibleAmtAssesseeParentsSrCitizen", SqlDbType.Float);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objTDSCalculatorMediclaimTBO.MediclaimEligibleAmtAssesseeParentsSrCitizen;

                pSqlParameter[15] = new SqlParameter("@MediclaimPremiumAmtAssesseeParentsHealthCheckUp", SqlDbType.Float);
                pSqlParameter[15].Direction = ParameterDirection.Input;
                pSqlParameter[15].Value = objTDSCalculatorMediclaimTBO.MediclaimPremiumAmtAssesseeParentsHealthCheckUp;

                pSqlParameter[16] = new SqlParameter("@MediclaimEligibleAmtAssesseeParentsHealthCheckUp", SqlDbType.Float);
                pSqlParameter[16].Direction = ParameterDirection.Input;
                pSqlParameter[16].Value = objTDSCalculatorMediclaimTBO.MediclaimEligibleAmtAssesseeParentsHealthCheckUp;

                pSqlParameter[17] = new SqlParameter("@MediclaimPremiumAmtAssesseeParentsMedicalExpenditure", SqlDbType.Float);
                pSqlParameter[17].Direction = ParameterDirection.Input;
                pSqlParameter[17].Value = objTDSCalculatorMediclaimTBO.MediclaimPremiumAmtAssesseeParentsMedicalExpenditure;

                pSqlParameter[18] = new SqlParameter("@MediclaimEligibleAmtAssesseeParentsMedicalExpenditure", SqlDbType.Float);
                pSqlParameter[18].Direction = ParameterDirection.Input;
                pSqlParameter[18].Value = objTDSCalculatorMediclaimTBO.MediclaimEligibleAmtAssesseeParentsMedicalExpenditure;

                pSqlParameter[19] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[19].Direction = ParameterDirection.Input;
                pSqlParameter[19].Value = objTDSCalculatorMediclaimTBO.IsDeleted;

                pSqlParameter[20] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[20].Direction = ParameterDirection.Input;
                pSqlParameter[20].Value = objTDSCalculatorMediclaimTBO.CreatedUserID;

                pSqlParameter[21] = new SqlParameter("@CreatedDate", SqlDbType.Date);
                pSqlParameter[21].Direction = ParameterDirection.Input;
                pSqlParameter[21].Value = objTDSCalculatorMediclaimTBO.CreatedDate;

                sSql = "usp_tbl_TDSCalculatorMediclaim_T_Insert";
                int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, sSql, pSqlParameter);

                ////DataTable dtResult = new DataTable();
                ////dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);
                ////ApplicationResult objResults = new ApplicationResult(dtResult);
                ////objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                ////return objResults;

                if (iResult > 0)
                {
                    ApplicationResult objResults = new ApplicationResult();
                    objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                    return objResults;
                }
                else
                {
                    ApplicationResult objResults = new ApplicationResult();
                    objResults.status = ApplicationResult.CommonStatusType.FAILURE;
                    return objResults;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objTDSCalculatorMediclaimTBO = null;
            }
        }
        #endregion

        #region Insert TDSCalculatorPayItem
        public ApplicationResult TDSCalculatorPayItem_T_Insert(TDSCalculatorPayItemTBO objTDSCalculatorPayItemTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[13];

                pSqlParameter[0] = new SqlParameter("@ID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objTDSCalculatorPayItemTBO.ID;

                pSqlParameter[1] = new SqlParameter("@TDSCalculatorsDetailsID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objTDSCalculatorPayItemTBO.TDSCalculatorsDetailsID;

                pSqlParameter[2] = new SqlParameter("@TDSCalculatorsID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objTDSCalculatorPayItemTBO.TDSCalculatorsID;

                pSqlParameter[3] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objTDSCalculatorPayItemTBO.EmployeeMID;

                pSqlParameter[4] = new SqlParameter("@UserTemplateID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objTDSCalculatorPayItemTBO.UserTemplateID;

                pSqlParameter[5] = new SqlParameter("@UserPayItemTemplateID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objTDSCalculatorPayItemTBO.UserPayItemTemplateID;

                pSqlParameter[6] = new SqlParameter("@PayItemMID", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objTDSCalculatorPayItemTBO.PayItemMID;

                pSqlParameter[7] = new SqlParameter("@Name", SqlDbType.VarChar); //PayITemName
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objTDSCalculatorPayItemTBO.Name;

                pSqlParameter[8] = new SqlParameter("@ActualAmount", SqlDbType.Float);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objTDSCalculatorPayItemTBO.ActualAmount;

                pSqlParameter[9] = new SqlParameter("@NoOfMonthsMultipliedAmount", SqlDbType.Float);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objTDSCalculatorPayItemTBO.NoOfMonthsMultipliedAmount;

                pSqlParameter[10] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objTDSCalculatorPayItemTBO.IsDeleted;

                pSqlParameter[11] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objTDSCalculatorPayItemTBO.CreatedUserID;

                pSqlParameter[12] = new SqlParameter("@CreatedDate", SqlDbType.Date);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objTDSCalculatorPayItemTBO.CreatedDate;

                sSql = "usp_tbl_TDSCalculatorPayItem_T_Insert";
                int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, sSql, pSqlParameter);

                ////DataTable dtResult = new DataTable();
                ////dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);
                ////ApplicationResult objResults = new ApplicationResult(dtResult);
                ////objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                ////return objResults;

                if (iResult > 0)
                {
                    ApplicationResult objResults = new ApplicationResult();
                    objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                    return objResults;
                }
                else
                {
                    ApplicationResult objResults = new ApplicationResult();
                    objResults.status = ApplicationResult.CommonStatusType.FAILURE;
                    return objResults;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objTDSCalculatorPayItemTBO = null;
            }
        }
        #endregion

        #region Get TDSFindID_Last
        public ApplicationResult TDSFindID_Last()
        {
            try
            {
                sSql = "usp_tbl_TDSFindID";
                DataTable dtDepartment = new DataTable();
                dtDepartment = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtDepartment);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Get TDSDetailsFindID_Last
        public ApplicationResult TDSDetailsFindID_Last()
        {
            try
            {
                sSql = "usp_tbl_TDSDetailsFindID";
                DataTable dtDepartment = new DataTable();
                dtDepartment = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtDepartment);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        //Fetching

        #region TDSCalculator_Count
        public ApplicationResult TDSCalculator_Count(int intEmployeeMID, string strFinancialYear)
        {
            try
            {               
                pSqlParameter = new SqlParameter[2];
                
                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                pSqlParameter[1] = new SqlParameter("@FinancialYear", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strFinancialYear;

                strStoredProcName = "usp_tbl_TDSCalculator_M_Cascade";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region TDSCalculator_Fetch
        public ApplicationResult TDSCalculator_Fetch(int intEmployeeMID, string strFinancialYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                pSqlParameter[1] = new SqlParameter("@FinancialYear", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strFinancialYear;

                strStoredProcName = "usp_tbl_TDSCalculator_M_Fetch";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region TDSCalculatorDetails_Fetch
        public ApplicationResult TDSCalculatorDetails_Fetch(int intTDSCalculatorsID, int intEmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@TDSCalculatorsID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTDSCalculatorsID;

                pSqlParameter[1] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intEmployeeMID;

                strStoredProcName = "usp_tbl_TDSCalculatorsDetails_M_Fetch";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region TDSCalculatorMediclaim_Fetch
        public ApplicationResult TDSCalculatorMediclaim_Fetch(int intTDSCalculatorsID, int intEmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@TDSCalculatorsID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTDSCalculatorsID;

                pSqlParameter[1] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intEmployeeMID;

                strStoredProcName = "usp_tbl_TDSCalculatoMediclaim_M_Fetch";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region TDSCalculatorPayItem_Fetch
        public ApplicationResult TDSCalculatorPayItem_Fetch(int intTDSCalculatorsDetailsID, int intTDSCalculatorsID, int intEmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@TDSCalculatorsDetailsID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTDSCalculatorsDetailsID;

                pSqlParameter[1] = new SqlParameter("@TDSCalculatorsID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTDSCalculatorsID;

                pSqlParameter[2] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intEmployeeMID;

                strStoredProcName = "usp_tbl_TDSCalculatorPayItem_M_Fetch";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
