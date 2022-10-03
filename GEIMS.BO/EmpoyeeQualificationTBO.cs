using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class EmpoyeeQualificationTBO
    {
        #region EmpoyeeQualificationT Class Properties

        public const string EMPOYEEQUALIFICATIONT_TABLE = "tbl_EmployeeQualification_T";
        public const string EMPOYEEQUALIFICATIONT_QUALIFICATIONTID = "QualificationTID";
        public const string EMPOYEEQUALIFICATIONT_QUALIFICATIONNAMEENG = "QualificationNameENG";
        public const string EMPOYEEQUALIFICATIONT_QUALIFICATIONNAMEGUJ = "QualificationNameGUJ";
        public const string EMPOYEEQUALIFICATIONT_YEAR = "Year";
        public const string EMPOYEEQUALIFICATIONT_PERCENTAGE = "Percentage";
        public const string EMPOYEEQUALIFICATIONT_UNIVERSITYENG = "UniversityENG";
        public const string EMPOYEEQUALIFICATIONT_UNIVERSITYGUJ = "UniversityGUJ";
        public const string EMPOYEEQUALIFICATIONT_EMPLOYEEMID = "EmployeeMID";
        public const string EMPOYEEQUALIFICATIONT_ISDELETED = "IsDeleted";



        private int intQualificationTID = 0;
        private string strQualificationNameENG = string.Empty;
        private string strQualificationNameGUJ = string.Empty;
        private string strYear = string.Empty;
        private string strPercentage = string.Empty;
        private string strUniversityENG = string.Empty;
        private string strUniversityGUJ = string.Empty;
        private int intEmployeeMID = 0;
        private int intIsDeleted = 0;

        #endregion

        #region ---Properties---
        public int QualificationTID
        {
            get { return intQualificationTID; }
            set { intQualificationTID = value; }
        }
        public string QualificationNameENG
        {
            get { return strQualificationNameENG; }
            set { strQualificationNameENG = value; }
        }
        public string QualificationNameGUJ
        {
            get { return strQualificationNameGUJ; }
            set { strQualificationNameGUJ = value; }
        }
        public string Year
        {
            get { return strYear; }
            set { strYear = value; }
        }
        public string Percentage
        {
            get { return strPercentage; }
            set { strPercentage = value; }
        }
        public string UniversityENG
        {
            get { return strUniversityENG; }
            set { strUniversityENG = value; }
        }
        public string UniversityGUJ
        {
            get { return strUniversityGUJ; }
            set { strUniversityGUJ = value; }
        }
        public int EmployeeMID
        {
            get { return intEmployeeMID; }
            set { intEmployeeMID = value; }
        }
        public int IsDeleted
        {
            get { return intIsDeleted; }
            set { intIsDeleted = value; }
        }

        #endregion
    }
}


