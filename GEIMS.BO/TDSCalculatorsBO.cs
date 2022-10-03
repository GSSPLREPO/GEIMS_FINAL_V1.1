using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class TDSCalculatorsBO
    {
        #region EmployeeM Class Properties

        public const string TDSCALCULATORS_TABLE = "TDSCalculators";
        public const string TDSCALCULATOR_ID = "ID";
        public const string TDSCALCULATORS_EMPLOYEEMID = "EmployeeMID";
        public const string TDSCALCULATORS_NoOfMonths = "NoOfMonths";
        public const string TDSCALCULATORS_FinancialYear = "FinancialYear";
        public const string TDSCALCULATOR_OptforSec115BAC = "OptforSec115BAC";
        public const string TDSCALCULATOR_AdditionalEarningsLabel1 = "AdditionalEarningsLabel1";
        public const string TDSCALCUALTOR_AdditionalEarningsValue1 = "AdditionalEarningsValue1";
        public const string TDSCALCULATOR_AdditionalEarningsLabel2 = "AdditionalEarningsLabel2";
        public const string TDSCALCUALTOR_AdditionalEarningsValue2 = "AdditionalEarningsValue2";
        public const string TDSCALCULATOR_AdditionalEarningsLabel3 = "AdditionalEarningsLabel3";
        public const string TDSCALCUALTOR_AdditionalEarningsValue3 = "AdditionalEarningsValue3";
        public const string TDSCALCULATOR_AdditionalEarningsLabel4 = "AdditionalEarningsLabel4";
        public const string TDSCALCUALTOR_AdditionalEarningsValue4 = "AdditionalEarningsValue4";
        public const string TDSCALCULATOR_AdditionalEarningsLabel5 = "AdditionalEarningsLabel5";
        public const string TDSCALCUALTOR_AdditionalEarningsValue5 = "AdditionalEarningsValue5";
        public const string TDSCALCUALTOR_BasicSalary = "BasicSalary";
        public const string TDSCALCULATOR_DearnessAllowance = "DearnessAllowance";
        public const string TDSCALCULATOR_Commission = "Commission";
        public const string TDSCALCULATOR_HRAReceived = "HRAReceived";
        public const string TDSCALCULATOR_RentPaid = "RentPaid";
        public const string TDSCALCULATOR_ResidingMetroCity = "ResidingMetroCity";
        public const string TDSCALCULATOR_OtherIncomeLabel1 = "OtherIncomeLabel1";
        public const string TDSCALCULATOR_OtherIncomeValue1 = "OtherIncomeValue1";
        public const string TDSCALCULATOR_OtherIncomeLabel2 = "OtherIncomeLabel2";
        public const string TDSCALCULATOR_OtherIncomeValue2 = "OtherIncomeValue2";
        public const string TDSCALCULATOR_OtherIncomeLabel3 = "OtherIncomeLabel3";
        public const string TDSCALCULATOR_OtherIncomeValue3 = "OtherIncomeValue3";
        public const string TDSCALCULATOR_OtherIncomeLabel4 = "OtherIncomeLabel4";
        public const string TDSCALCULATOR_OtherIncomeValue4 = "OtherIncomeValue4";
        public const string TDSCALCULATOR_OtherIncomeLabel5 = "OtherIncomeLabel5";
        public const string TDSCALCULATOR_OtherIncomeValue5 = "OtherIncomeValue5";
        public const string TDSCALCULATOR_InterestOfHousingLoan = "InterestOfHousingLoan";
        public const string TDSCALCULATOR_HomeLoanRepayment = "HomeLoanRepayment";
        public const string TDSCALCULATOR_LICPremium = "LICPremium";
        public const string TDSCALCULATOR_ELSSMutualFund = "ELSSMutualFund";
        public const string TDSCALCULATOR_SchoolTutionFee = "SchoolTutionFee";
        public const string TDSCALCULATOR_PPF = "PPF";
        public const string TDSCALCULATOR_InvestmentInNPS = "InvestmentInNPS";
        public const string TDSCALCULATOR_AdvanceTaxPaidbyEmployee = "AdvanceTaxPaidbyEmployee";
        public const string TDSCALCULATOR_TDSDeductedbyOtherEmployerSource = "TDSDeductedbyOtherEmployerSource";
        public const string TDSCALCULATOR_MonthlyTDSDeducted = "MonthlyTDSDeducted";
        public const string TDSCALCULATOR_IsDeleted = "IsDeleted";
        public const string TDSCALCULATOR_CreatedUserID = "CreatedUserID";
        public const string TDSCALCULATOR_CreatedDate = "CreatedDate";
        public const string TDSCALCULATOR_LastModifiedUserID = "LastModifiedUserID";
        public const string TDSCALCULATOR_LastModifiedDate = "LastModifiedDate";

        private int intID = 0;
        private int intEmployeeMID = 0;
        private int  intNoOfMonths = 0;
        private string strFinancialYear = string.Empty;
        private int intOptforSec115BAC = 0;
        private string strAdditionalEarningsLabel1 = string.Empty;
        private double dbAdditionalEarningsValue1 = 0.00;
        private string strAdditionalEarningsLabel2 = string.Empty;
        private double dbAdditionalEarningsValue2 = 0.00;
        private string strAdditionalEarningsLabel3 = string.Empty;
        private double dbAdditionalEarningsValue3 = 0.00;
        private string strAdditionalEarningsLabel4 = string.Empty;
        private double dbAdditionalEarningsValue4 = 0.00;
        private string strAdditionalEarningsLabel5 = string.Empty;
        private double dbAdditionalEarningsValue5 = 0.00;
        private double dbBasicSalary = 0.00;
        private double dbDearnessAllowance = 0.00;
        private double dbCommission = 0.00;
        private double dbHRAReceived = 0.00;
        private double dbRentPaid = 0.00;
        private int intResidingMetroCity = 0;
        private string strOtherIncomeLabel1 = string.Empty;
        private double dbOtherIncomeValue1 = 0.00;
        private string strOtherIncomeLabel2 = string.Empty;
        private double dbOtherIncomeValue2 = 0.00;
        private string strOtherIncomeLabel3 = string.Empty;
        private double dbOtherIncomeValue3 = 0.00;
        private string strOtherIncomeLabel4 = string.Empty;
        private double dbOtherIncomeValue4 = 0.00;
        private string strOtherIncomeLabel5 = string.Empty;
        private double dbOtherIncomeValue5 = 0.00;
        private double dbInterestOfHousingLoan = 0.00;
        private double dbHomeLoanRepayment = 0.00;
        private double dbLICPremium = 0.00;
        private double dbELSSMutualFund = 0.00;
        private double dbSchoolTutionFee = 0.00;
        private double dbPPF = 0.00;
        private double dbInvestmentInNPS = 0.00;
        private double dbAdvanceTaxPaidbyEmployee = 0.00;
        private double dbTDSDeductedbyOtherEmployerSource = 0.00;
        private double dbMonthlyTDSDeducted = 0.00;
        private int intIsDeleted = 0;
        private int intCreatedUserID = 0;
        private string strCreatedDate = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;

        #endregion

        #region ---Properties---
        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }
        public int EmployeeMID
        {
            get { return intEmployeeMID; }
            set { intEmployeeMID = value; }
        }
        public int NoOfMonths
        {
            get { return intNoOfMonths; }
            set { intNoOfMonths = value; }
        }
        public string FinancialYear
        {
            get { return strFinancialYear; }
            set { strFinancialYear = value; }
        }
        public int OptforSec115BAC
        {
            get { return intOptforSec115BAC; }
            set { intOptforSec115BAC = value; }
        }
        public string AdditionalEarningsLabel1
        {
            get { return strAdditionalEarningsLabel1; }
            set { strAdditionalEarningsLabel1 = value; }
        }
        public double AdditionalEarningsValue1
        {
            get { return dbAdditionalEarningsValue1; }
            set { dbAdditionalEarningsValue1 = value; }
        }
        public string AdditionalEarningsLabel2
        {
            get { return strAdditionalEarningsLabel2; }
            set { strAdditionalEarningsLabel2 = value; }
        }
        public double AdditionalEarningsValue2
        {
            get { return dbAdditionalEarningsValue2; }
            set { dbAdditionalEarningsValue2 = value; }
        }
        public string AdditionalEarningsLabel3
        {
            get { return strAdditionalEarningsLabel3; }
            set { strAdditionalEarningsLabel3 = value; }
        }
        public double AdditionalEarningsValue3
        {
            get { return dbAdditionalEarningsValue3; }
            set { dbAdditionalEarningsValue3 = value; }
        }
        public string AdditionalEarningsLabel4
        {
            get { return strAdditionalEarningsLabel4; }
            set { strAdditionalEarningsLabel4 = value; }
        }
        public double AdditionalEarningsValue4
        {
            get { return dbAdditionalEarningsValue4; }
            set { dbAdditionalEarningsValue4 = value; }
        }
        public string AdditionalEarningsLabel5
        {
            get { return strAdditionalEarningsLabel5; }
            set { strAdditionalEarningsLabel5 = value; }
        }
        public double AdditionalEarningsValue5
        {
            get { return dbAdditionalEarningsValue5; }
            set { dbAdditionalEarningsValue5 = value; }
        }
        public double BasicSalary
        {
            get { return dbBasicSalary; }
            set { dbBasicSalary = value; }
        }
        public double DearnessAllowance
        {
            get { return dbDearnessAllowance; }
            set { dbDearnessAllowance = value; }
        }
        public double Commision
        {
            get { return dbCommission; }
            set { dbCommission = value; }
        }
        public double HRAReceived
        {
            get { return dbHRAReceived; }
            set { dbHRAReceived = value; }
        }
        public double RentPaid
        {
            get { return dbRentPaid; }
            set { dbRentPaid = value; }
        }
        public int  ResidingMetroCity
        {
            get { return intResidingMetroCity; }
            set { intResidingMetroCity = value; }
        }
        public string OtherIncomeLabel1 
        {
            get { return strOtherIncomeLabel1; }
            set { strOtherIncomeLabel1 = value; }
        }
        public double OtherIncomeValue1
        {
            get { return dbOtherIncomeValue1; }
            set { dbOtherIncomeValue1 = value; }
        }
        public string OtherIncomeLabel2
        {
            get { return strOtherIncomeLabel2; }
            set { strOtherIncomeLabel2 = value; }
        }
        public double OtherIncomeValue2
        {
            get { return dbOtherIncomeValue2; }
            set { dbOtherIncomeValue2 = value; }
        }
        public string OtherIncomeLabel3
        {
            get { return strOtherIncomeLabel3; }
            set { strOtherIncomeLabel3 = value; }
        }
        public double OtherIncomeValue3
        {
            get { return dbOtherIncomeValue3; }
            set { dbOtherIncomeValue3 = value; }
        }  
        public string OtherIncomeLabel4
        {
            get { return strOtherIncomeLabel4; }
            set { strOtherIncomeLabel4 = value; }
        }
        public double OtherIncomeValue4
        {
            get { return dbOtherIncomeValue4; }
            set { dbOtherIncomeValue4 = value; }
        }
        public string OtherIncomeLabel5
        {
            get { return strOtherIncomeLabel5; }
            set { strOtherIncomeLabel5 = value; }
        }
        public double OtherIncomeValue5
        {
            get { return dbOtherIncomeValue5; }
            set { dbOtherIncomeValue5 = value; }
        }
        public double InterestOfHousingLoan
        {
            get { return dbInterestOfHousingLoan; }
            set { dbInterestOfHousingLoan = value; }
        }
        public double HomeLoanRepayment
        {
            get { return dbHomeLoanRepayment;}
            set { dbHomeLoanRepayment = value; }
        }
        public double LICPremium
        {
            get { return dbLICPremium;}
            set { dbLICPremium = value; }
        }
        public double ELSSMutualFund
        {
            get { return dbELSSMutualFund; }
            set { dbELSSMutualFund = value; }
        }
        public double SchoolTutionFee
        {
            get { return dbSchoolTutionFee; }
            set { dbSchoolTutionFee = value; }
        }
        public double PPF
        {
            get { return dbPPF;}
            set { dbPPF = value; }
        }
        public double InvestmentInNPS
        {
            get { return dbInvestmentInNPS;}
            set { dbInvestmentInNPS = value; }
        }
        public double AdvanceTaxPaidbyEmployee
        {
            get { return dbAdvanceTaxPaidbyEmployee;}
            set { dbAdvanceTaxPaidbyEmployee = value; }
        }
        public double TDSDeductedbyOtherEmployerSource
        {
            get { return dbTDSDeductedbyOtherEmployerSource;}
            set { dbTDSDeductedbyOtherEmployerSource = value; }
        }
        public double MonthlyTDSDeducted
        {
            get { return dbMonthlyTDSDeducted;}
            set { dbMonthlyTDSDeducted = value; }
        }
        public string CreatedDate
        {
            get { return strCreatedDate; }
            set { strCreatedDate = value; }
        }
        public int CreatedUserID
        {
            get { return intCreatedUserID; }
            set { intCreatedUserID = value; }
        }
        public int IsDeleted
        {
            get { return intIsDeleted; }
            set { intIsDeleted = value; }
        }
        public int LastModifideUserID
        {
            get { return intLastModifiedUserID; }
            set { intLastModifiedUserID = value; }
        }
        public string LastModifideDate
        {
            get { return strLastModifiedDate; }
            set { strLastModifiedDate = value; }
        }
        #endregion
    }
}
