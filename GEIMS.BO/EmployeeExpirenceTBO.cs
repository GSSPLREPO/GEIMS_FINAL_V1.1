using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class EmployeeExpirenceTBO
    {
        #region EmployeeExpirenceT Class Properties

        public const string EMPLOYEEEXPIRENCET_TABLE = "tbl_EmployeeExperience_T";
        public const string EMPLOYEEEXPIRENCET_EMPLOYEEEXPERIENCETID = "EmployeeExperienceTID";
        public const string EMPLOYEEEXPIRENCET_EMPLOYEEMID = "EmployeeMID";
        public const string EMPLOYEEEXPIRENCET_ORGANISATIONNAMEENG = "OrganisationNameENG";
        public const string EMPLOYEEEXPIRENCET_ORGANISATIONNAMEGUJ = "OrganisationNameGUJ";
        public const string EMPLOYEEEXPIRENCET_ORGANISATIONADDRESSENG = "OrganisationAddressENG";
        public const string EMPLOYEEEXPIRENCET_ORGANISATIONADDRESSGUJ = "OrganisationAddressGUJ";
        public const string EMPLOYEEEXPIRENCET_DESIGNATIONENG = "DesignationENG";
        public const string EMPLOYEEEXPIRENCET_DESIGNATIONGUJ = "DesignationGUJ";
        public const string EMPLOYEEEXPIRENCET_DURATIONYEAR = "DurationYear";
        public const string EMPLOYEEEXPIRENCET_DURATIONMONTH = "DurationMonth";
        public const string EMPLOYEEEXPIRENCET_JOBRESPONSIBILITYENG = "JobResponsibilityENG";
        public const string EMPLOYEEEXPIRENCET_JOBRESPONSIBILITYGUJ = "JobResponsibilityGUJ";
        public const string EMPLOYEEEXPIRENCET_CTC = "CTC";
        public const string EMPLOYEEEXPIRENCET_REASONOFLEAVINGENG = "ReasonOfLeavingENG";
        public const string EMPLOYEEEXPIRENCET_REASONOFLEAVINGGUJ = "ReasonOfLeavingGUJ";
        public const string EMPLOYEEEXPIRENCET_ISDELETED = "IsDeleted";



        private int intEmployeeExperienceTID = 0;
        private int intEmployeeMID = 0;
        private string strOrganisationNameENG = string.Empty;
        private string strOrganisationNameGUJ = string.Empty;
        private string strOrganisationAddressENG = string.Empty;
        private string strOrganisationAddressGUJ = string.Empty;
        private string strDesignationENG = string.Empty;
        private string strDesignationGUJ = string.Empty;
        private string strDurationYear = string.Empty;
        private string strDurationMonth = string.Empty;
        private string strJobResponsibilityENG = string.Empty;
        private string strJobResponsibilityGUJ = string.Empty;
        private string strCTC = string.Empty;
        private string strReasonOfLeavingENG = string.Empty;
        private string strReasonOfLeavingGUJ = string.Empty;
        private int intIsDeleted = 0;

        #endregion

        #region ---Properties---
        public int EmployeeExperienceTID
        {
            get { return intEmployeeExperienceTID; }
            set { intEmployeeExperienceTID = value; }
        }
        public int EmployeeMID
        {
            get { return intEmployeeMID; }
            set { intEmployeeMID = value; }
        }
        public string OrganisationNameENG
        {
            get { return strOrganisationNameENG; }
            set { strOrganisationNameENG = value; }
        }
        public string OrganisationNameGUJ
        {
            get { return strOrganisationNameGUJ; }
            set { strOrganisationNameGUJ = value; }
        }
        public string OrganisationAddressENG
        {
            get { return strOrganisationAddressENG; }
            set { strOrganisationAddressENG = value; }
        }
        public string OrganisationAddressGUJ
        {
            get { return strOrganisationAddressGUJ; }
            set { strOrganisationAddressGUJ = value; }
        }
        public string DesignationENG
        {
            get { return strDesignationENG; }
            set { strDesignationENG = value; }
        }
        public string DesignationGUJ
        {
            get { return strDesignationGUJ; }
            set { strDesignationGUJ = value; }
        }
        public string DurationYear
        {
            get { return strDurationYear; }
            set { strDurationYear = value; }
        }
        public string DurationMonth
        {
            get { return strDurationMonth; }
            set { strDurationMonth = value; }
        }
        public string JobResponsibilityENG
        {
            get { return strJobResponsibilityENG; }
            set { strJobResponsibilityENG = value; }
        }
        public string JobResponsibilityGUJ
        {
            get { return strJobResponsibilityGUJ; }
            set { strJobResponsibilityGUJ = value; }
        }
        public string CTC
        {
            get { return strCTC; }
            set { strCTC = value; }
        }
        public string ReasonOfLeavingENG
        {
            get { return strReasonOfLeavingENG; }
            set { strReasonOfLeavingENG = value; }
        }
        public string ReasonOfLeavingGUJ
        {
            get { return strReasonOfLeavingGUJ; }
            set { strReasonOfLeavingGUJ = value; }
        }
        public int IsDeleted
        {
            get { return intIsDeleted; }
            set { intIsDeleted = value; }
        }

        #endregion
    }
}


