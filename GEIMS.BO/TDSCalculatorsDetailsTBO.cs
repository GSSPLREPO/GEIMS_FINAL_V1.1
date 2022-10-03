using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class TDSCalculatorsDetailsTBO
    {
        #region EmployeeM Class Properties

        public const string TDSCALCULATORSDETAILS_TABLE = "TDSCalculatorsDetails";
        public const string TDSCALCULATORSDETAILS_ID = "ID";
        public const string TDSCALCULATORSDETAILS_TDSCalculatorsID = "TDSCalculatorsID";
        public const string TDSCALCULATORSDETAILS_EMPLOYEEMID = "EmployeeMID";
        public const string TDSCALCULATORSDETAILS_UserTemplateID = "UserTemplateID";
        public const string TDSCALCULATORSDETAILS_GrossTotalSalary = "GrossTotalSalary";
        public const string TDSCALCULATORSDETAILS_ProfessionalTax = "ProfessionalTax";
        public const string TDSCALCULATORSDETAILS_HRAExcemption = "HRAExcemption";
        public const string TDSCALCULATORSDETAILS_StandardDeduction = "StandardDeduction";
        public const string TDSCALCULATORSDETAILS_TaxableSalary = "TaxableSalary";
        public const string TDSCALCULATORSDETAILS_OtherIncomeTotal = "OtherIncomeTotal";
        public const string TDSCALCULATORSDETAILS_EligibleAmount1 = "EligibleAmount1";
        public const string TDSCALCULATORSDETAILS_EligibleAmount2 = "EligibleAmount2";
        public const string TDSCALCULATORSDETAILS_GrosTotalIncome = "GrosTotalIncome";
        public const string TDSCALCULATORSDETAILS_EligibleDeduction80C = "EligibleDeduction80C";
        public const string TDSCALCULATORSDETAILS_EligibleDeduction80CCD1B = "EligibleDeduction80CCD1B";
        public const string TDSCALCULATORSDETAILS_SalaryHRA = "SalaryHRA";
        public const string TDSCALCULATORSDETAILS_ActualHRAReceived = "ActualHRAReceived";
        public const string TDSCALCULATORSDETAILS_salaryLess5040 = "SalaryLess5040";        
        public const string TDSCALCULATORSDETAILS_ExemptedHRA = "ExemptedHRA";
        public const string TDSCALCULATORSDETAILS_TaxableHRA = "TaxableHRA";
        public const string TDSCALCULATORSDETAILS_EligibleDeduction80D = "EligibleDeduction80D";
        public const string TDSCALCULATORSDETAILS_AvailableDeductions80 = "AvailableDeductions80";
        public const string TDSCALCULATORSDETAILS_EligibleDeduction80 = "EligibleDeduction80";
        public const string TDSCALCULATORSDETAILS_TaxableIncome = "TaxableIncome";
        public const string TDSCALCULATORSDETAILS_TaxPayable = "TaxPayable";
        public const string TDSCALCULATORSDETAILS_Rebate87A = "Rebate87A";
        public const string TDSCALCULATORSDETAILS_TaxPayableRebate87A = "TaxPayableRebate87A";
        public const string TDSCALCULATORSDETAILS_HealthandEducationCessHEC = "HealthandEducationCessHEC";
        public const string TDSCALCULATORSDETAILS_TotalTaxPayable = "TotalTaxPayable";
        public const string TDSCALCULATORSDETAILS_TaxPayablefortheYear = "TaxPayablefortheYear";     
        public const string TDSCALCULATOR_IsDeleted = "IsDeleted";
        public const string TDSCALCULATOR_CreatedUserID = "CreatedUserID";
        public const string TDSCALCULATOR_CreatedDate = "CreatedDate";
        public const string TDSCALCULATOR_LastModifiedUserID = "LastModifiedUserID";
        public const string TDSCALCULATOR_LastModifiedDate = "LastModifiedDate";

        private int intID = 0;
        private int intTDSCalculatorsID = 0;
        private int intEmployeeMID = 0;
        private int intUserTemplateID = 0;
        private double dbGrossTotalSalary = 0.00;
        private double dbProfessionalTax = 0.00;      
        private double dbHRAExcemption = 0.00;
        private double dbStandardDeduction = 0.00;
        private double dbTaxableSalary = 0.00;
        private double dbOtherIncomeTotal = 0.00;
        private double dbEligibleAmount1 = 0.00;
        private double dbEligibleAmount2 = 0.00;       
        private double dbGrosTotalIncome = 0.00;
        private double dbEligibleDeduction80C = 0.00;
        private double dbEligibleDeduction80CCD1B = 0.00;
        private double dbSalaryHRA = 0.00;
        private double dbActualHRAReceived = 0.00;
        private double dbSalaryLess5040 = 0.00;
        private double dbExemptedHRA = 0.00;
        private double dbTaxableHRA = 0.00;
        private double dbEligibleDeduction80D = 0.00;
        private double dbAvailableDeductions80 = 0.00;
        private double dbEligibleDeduction80 = 0.00;
        private double dbTaxableIncome = 0.00;
        private double dbTaxPayable = 0.00;
        private double dbRebate87A = 0.00;
        private double dbTaxPayableRebate87A = 0.00;
        private double dbHealthandEducationCessHEC = 0.00;
        private double dbTotalTaxPayable = 0.00;
        private double dbTaxPayablefortheYear = 0.00;
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
        public int TDSCalculatorsID
        {
            get { return intTDSCalculatorsID; }
            set { intTDSCalculatorsID = value; }
        }
        public int EmployeeMID
        {
            get { return intEmployeeMID; }
            set { intEmployeeMID = value; }
        }
        public int UserTemplateID
        {
            get { return intUserTemplateID; }
            set { intUserTemplateID = value; }
        }
        public double GrossTotalSalary
        {
            get { return dbGrossTotalSalary; }
            set { dbGrossTotalSalary = value; }
        }
        public double ProfessionalTax
        {
            get { return dbProfessionalTax; }
            set { dbProfessionalTax = value; }
        }
        public double HRAExcemption
        {
            get { return dbHRAExcemption; }
            set { dbHRAExcemption = value; }
        }  
        public double StandardDeduction
        {
            get { return dbStandardDeduction; }
            set { dbStandardDeduction = value; }
        }
        public double TaxableSalary
        {
            get { return dbTaxableSalary; }
            set { dbTaxableSalary = value; }
        }
        public double OtherIncomeTotal
        {
            get { return dbOtherIncomeTotal; }
            set { dbOtherIncomeTotal = value; }
        }
        public double EligibleAmount1
        {
            get { return dbEligibleAmount1; }
            set { dbEligibleAmount1 = value; }
        }
        public double EligibleAmount2
        {
            get { return dbEligibleAmount2; }
            set { dbEligibleAmount2 = value; }
        }
        public double GrosTotalIncome
        {
            get { return dbGrosTotalIncome; }
            set { dbGrosTotalIncome = value; }
        }
        public double EligibleDeduction80C
        {
            get { return dbEligibleDeduction80C; }
            set { dbEligibleDeduction80C = value; }
        }
        public double EligibleDeduction80CCD1B
        {
            get { return dbEligibleDeduction80CCD1B; }
            set { dbEligibleDeduction80CCD1B = value; }
        }
        public double SalaryHRA
        {
            get { return dbSalaryHRA; }
            set { dbSalaryHRA = value; }
        }
        public double ActualHRAReceived
        {
            get { return dbActualHRAReceived; }
            set { dbActualHRAReceived = value; }
        }
        public double SalaryLess5040
        {
            get { return dbSalaryLess5040; }
            set { dbSalaryLess5040 = value; }
        }
        public double ExemptedHRA
        {
            get { return dbExemptedHRA; }
            set { dbExemptedHRA = value; }
        }
        public double TaxableHRA
        {
            get { return dbTaxableHRA; }
            set { dbTaxableHRA = value; }
        }
        public double EligibleDeduction80D
        {
            get { return dbEligibleDeduction80D; }
            set { dbEligibleDeduction80D = value; }
        }
        public double AvailableDeductions80
        {
            get { return dbAvailableDeductions80; }
            set { dbAvailableDeductions80 = value; }
        }
        public double EligibleDeduction80
        {
            get { return dbEligibleDeduction80; }
            set { dbEligibleDeduction80 = value; }
        }
        public double TaxableIncome
        {
            get { return dbTaxableIncome; }
            set { dbTaxableIncome = value; }
        }
        public double TaxPayable
        {
            get { return dbTaxPayable; }
            set { dbTaxPayable = value; }
        }
        public double Rebate87A
        {
            get { return dbRebate87A; }
            set { dbRebate87A = value; }
        }
        public double TaxPayableRebate87A
        {
            get { return dbTaxPayableRebate87A; }
            set { dbTaxPayableRebate87A = value; }
        }
        public double HealthandEducationCessHEC
        {
            get { return dbHealthandEducationCessHEC; }
            set { dbHealthandEducationCessHEC = value; }
        }
        public double TotalTaxPayable
        {
            get { return dbTotalTaxPayable; }
            set { dbTotalTaxPayable = value; }
        }
        public double TaxPayablefortheYear
        {
            get { return dbTaxPayablefortheYear; }
            set { dbTaxPayablefortheYear = value; }
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
