using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class TDSCalculatorMediclaimTBO
    {
        #region EmployeeM Class Properties

        public const string TDSCALCULATORMEDICLAIM_TABLE = "TDSCalculatorMediclaim";
        public const string TDSCALCULATORMEDICLAIM_ID = "ID";
        public const string TDSCALCULATORMEDICLAIM_TDSCalculatorsID = "TDSCalculatorsID";
        public const string TDSCALCULATORMEDICLAIM_EMPLOYEEMID = "EmployeeMID";
        public const string TDSCALCULATORMEDICLAIM_MediclaimPremiumAmtAssesseeCGHS = "MediclaimPremiumAmtAssesseeCGHS";
        public const string TDSCALCULATORMEDICLAIM_MediclaimEligibleAmtAssesseeCGHS = "MediclaimEligibleAmtAssesseeCGHS";
        public const string TDSCALCULATORMEDICLAIM_MediclaimPremiumAmtAssesseeSrCitizen = "MediclaimPremiumAmtAssesseeSrCitizen";
        public const string TDSCALCULATORMEDICLAIM_MediclaimEligibleAmtAssesseeSrCitizen = "MediclaimEligibleAmtAssesseeSrCitizen";
        public const string TDSCALCULATORMEDICLAIM_MediclaimPremiumAmtAssesseeHealthCheckUp = "MediclaimPremiumAmtAssesseeHealthCheckUp";
        public const string TDSCALCULATORMEDICLAIM_MediclaimEligibleAmtAssesseeHealthCheckUp = "MediclaimEligibleAmtAssesseeHealthCheckUp";
        public const string TDSCALCULATORMEDICLAIM_MediclaimPremiumAmtAssesseeMedicalExpenditure = "MediclaimPremiumAmtAssesseeMedicalExpenditure";
        public const string TDSCALCULATORMEDICLAIM_MediclaimEligibleAmtAssesseeMedicalExpenditure = "MediclaimEligibleAmtAssesseeMedicalExpenditure";
        public const string TDSCALCULATORMEDICLAIM_MediclaimPremiumAmtAssesseeParentsCGHS = "MediclaimPremiumAmtAssesseeParentsCGHS";
        public const string TDSCALCULATORMEDICLAIM_MediclaimEligibleAmtAssesseeParentsCGHS = "MediclaimEligibleAmtAssesseeParentsCGHS";
        public const string TDSCALCULATORMEDICLAIM_MediclaimPremiumAmtAssesseeParentsSrCitizen = "MediclaimPremiumAmtAssesseeParentsSrCitizen";
        public const string TDSCALCULATORMEDICLAIM_MediclaimEligibleAmtAssesseeParentsSrCitizen = "MediclaimEligibleAmtAssesseeParentsSrCitizen";
        public const string TDSCALCULATORMEDICLAIM_MediclaimPremiumAmtAssesseeParentsHealthCheckUp = "MediclaimPremiumAmtAssesseeParentsHealthCheckUp";
        public const string TDSCALCULATORMEDICLAIM_MediclaimEligibleAmtAssesseeParentsHealthCheckUp = "MediclaimEligibleAmtAssesseeParentsHealthCheckUp";
        public const string TDSCALCULATORMEDICLAIM_MediclaimPremiumAmtAssesseeParentsMedicalExpenditure = "MediclaimPremiumAmtAssesseeParentsMedicalExpenditure";
        public const string TDSCALCULATORMEDICLAIM_MediclaimEligibleAmtAssesseeParentsMedicalExpenditure = "MediclaimEligibleAmtAssesseeParentsMedicalExpenditure";
        public const string TDSCALCULATOR_IsDeleted = "IsDeleted";
        public const string TDSCALCULATOR_CreatedUserID = "CreatedUserID";
        public const string TDSCALCULATOR_CreatedDate = "CreatedDate";
        public const string TDSCALCULATOR_LastModifiedUserID = "LastModifiedUserID";
        public const string TDSCALCULATOR_LastModifiedDate = "LastModifiedDate";

        private int intID = 0;
        private int intTDSCalculatorsID = 0;
        private int intEmployeeMID = 0;
        private double dbMediclaimPremiumAmtAssesseeCGHS = 0;
        private double dbMediclaimEligibleAmtAssesseeCGHS = 0.00;
        private double dbMediclaimPremiumAmtAssesseeSrCitizen = 0.00;
        private double dbMediclaimEligibleAmtAssesseeSrCitizen = 0.00;
        private double dbMediclaimPremiumAmtAssesseeHealthCheckUp = 0.00;
        private double dbMediclaimEligibleAmtAssesseeHealthCheckUp = 0.00;
        private double dbMediclaimPremiumAmtAssesseeMedicalExpenditure = 0.00;
        private double dbMediclaimEligibleAmtAssesseeMedicalExpenditure = 0.00;

        private double dbMediclaimPremiumAmtAssesseeParentsCGHS = 0.00;
        private double dbMediclaimEligibleAmtAssesseeParentsCGHS = 0.00;
        private double dbMediclaimPremiumAmtAssesseeParentsSrCitizen = 0.00;
        private double dbMediclaimEligibleAmtAssesseeParentsSrCitizen = 0.00;
        private double dbMediclaimPremiumAmtAssesseeParentsHealthCheckUp = 0.00;
        private double dbMediclaimEligibleAmtAssesseeParentsHealthCheckUp = 0.00;
        private double dbMediclaimPremiumAmtAssesseeParentsMedicalExpenditure = 0.00;
        private double dbMediclaimEligibleAmtAssesseeParentsMedicalExpenditure = 0.00;
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
        public double MediclaimPremiumAmtAssesseeCGHS
        {
            get { return dbMediclaimPremiumAmtAssesseeCGHS; }
            set { dbMediclaimPremiumAmtAssesseeCGHS = value; }
        }
        public double MediclaimEligibleAmtAssesseeCGHS
        {
            get { return dbMediclaimEligibleAmtAssesseeCGHS; }
            set { dbMediclaimEligibleAmtAssesseeCGHS = value; }
        }
        public double MediclaimPremiumAmtAssesseeSrCitizen
        {
            get { return dbMediclaimPremiumAmtAssesseeSrCitizen; }
            set { dbMediclaimPremiumAmtAssesseeSrCitizen = value; }
        }
        public double MediclaimEligibleAmtAssesseeSrCitizen
        {
            get { return dbMediclaimEligibleAmtAssesseeSrCitizen; }
            set { dbMediclaimEligibleAmtAssesseeSrCitizen = value; }
        }
        public double MediclaimPremiumAmtAssesseeHealthCheckUp
        {
            get { return dbMediclaimPremiumAmtAssesseeHealthCheckUp; }
            set { dbMediclaimPremiumAmtAssesseeHealthCheckUp = value; }
        }
        public double MediclaimEligibleAmtAssesseeHealthCheckUp
        {
            get { return dbMediclaimEligibleAmtAssesseeHealthCheckUp; }
            set { dbMediclaimEligibleAmtAssesseeHealthCheckUp = value; }
        }
        public double MediclaimPremiumAmtAssesseeMedicalExpenditure
        {
            get { return dbMediclaimPremiumAmtAssesseeMedicalExpenditure; }
            set { dbMediclaimPremiumAmtAssesseeMedicalExpenditure = value; }
        }
        public double MediclaimEligibleAmtAssesseeMedicalExpenditure
        {
            get { return dbMediclaimEligibleAmtAssesseeMedicalExpenditure; }
            set { dbMediclaimEligibleAmtAssesseeMedicalExpenditure = value; }
        }
        public double MediclaimPremiumAmtAssesseeParentsCGHS
        {
            get { return dbMediclaimPremiumAmtAssesseeParentsCGHS; }
            set { dbMediclaimPremiumAmtAssesseeParentsCGHS = value; }
        }
        public double MediclaimEligibleAmtAssesseeParentsCGHS
        {
            get { return dbMediclaimEligibleAmtAssesseeParentsCGHS; }
            set { dbMediclaimEligibleAmtAssesseeParentsCGHS = value; }
        }
        public double MediclaimPremiumAmtAssesseeParentsSrCitizen
        {
            get { return dbMediclaimPremiumAmtAssesseeParentsSrCitizen; }
            set { dbMediclaimPremiumAmtAssesseeParentsSrCitizen = value; }
        }
        public double MediclaimEligibleAmtAssesseeParentsSrCitizen
        {
            get { return dbMediclaimEligibleAmtAssesseeParentsSrCitizen; }
            set { dbMediclaimEligibleAmtAssesseeParentsSrCitizen = value; }
        }
        public double MediclaimPremiumAmtAssesseeParentsHealthCheckUp
        {
            get { return dbMediclaimPremiumAmtAssesseeParentsHealthCheckUp; }
            set { dbMediclaimPremiumAmtAssesseeParentsHealthCheckUp = value; }
        }
        public double MediclaimEligibleAmtAssesseeParentsHealthCheckUp
        {
            get { return dbMediclaimEligibleAmtAssesseeParentsHealthCheckUp; }
            set { dbMediclaimEligibleAmtAssesseeParentsHealthCheckUp = value; }
        }
        public double MediclaimPremiumAmtAssesseeParentsMedicalExpenditure
        {
            get { return dbMediclaimPremiumAmtAssesseeParentsMedicalExpenditure; }
            set { dbMediclaimPremiumAmtAssesseeParentsMedicalExpenditure = value; }
        }
        public double MediclaimEligibleAmtAssesseeParentsMedicalExpenditure
        {
            get { return dbMediclaimEligibleAmtAssesseeParentsMedicalExpenditure; }
            set { dbMediclaimEligibleAmtAssesseeParentsMedicalExpenditure = value; }
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
