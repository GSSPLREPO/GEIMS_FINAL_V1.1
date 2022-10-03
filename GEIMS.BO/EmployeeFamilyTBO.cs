using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class EmployeeFamilyTBO
    {
        #region EmployeeFamilyT Class Properties

        public const string EMPLOYEEFAMILYT_TABLE = "tbl_EmployeeFamilyPerson_T";
        public const string EMPLOYEEFAMILYT_FAMILYPERSONTID = "FamilyPersonTID";
        public const string EMPLOYEEFAMILYT_FAMILYPERSONNAME = "FamilyPersonName";
        public const string EMPLOYEEFAMILYT_OCCUPATION = "Occupation";
        public const string EMPLOYEEFAMILYT_ORGANISATION = "Organisation";
        public const string EMPLOYEEFAMILYT_QUALIFICATION = "Qualification";
        public const string EMPLOYEEFAMILYT_CONTACTNO = "ContactNo";
        public const string EMPLOYEEFAMILYT_MOBILENO = "MobileNo";
        public const string EMPLOYEEFAMILYT_EMAILID = "EmailID";
        public const string EMPLOYEEFAMILYT_EMPLOYEEMID = "EmployeeMID";
        public const string EMPLOYEEFAMILYT_ISDELETED = "IsDeleted";



        private int intFamilyPersonTID = 0;
        private string strFamilyPersonName = string.Empty;
        private string strOccupation = string.Empty;
        private string strOrganisation = string.Empty;
        private string strQualification = string.Empty;
        private string strContactNo = string.Empty;
        private string strMobileNo = string.Empty;
        private string strEmailID = string.Empty;
        private int intEmployeeMID = 0;
        private int intIsDeleted = 0;

        #endregion

        #region ---Properties---
        public int FamilyPersonTID
        {
            get { return intFamilyPersonTID; }
            set { intFamilyPersonTID = value; }
        }
        public string FamilyPersonName
        {
            get { return strFamilyPersonName; }
            set { strFamilyPersonName = value; }
        }
        public string Occupation
        {
            get { return strOccupation; }
            set { strOccupation = value; }
        }
        public string Organisation
        {
            get { return strOrganisation; }
            set { strOrganisation = value; }
        }
        public string Qualification
        {
            get { return strQualification; }
            set { strQualification = value; }
        }
        public string ContactNo
        {
            get { return strContactNo; }
            set { strContactNo = value; }
        }
        public string MobileNo
        {
            get { return strMobileNo; }
            set { strMobileNo = value; }
        }
        public string EmailID
        {
            get { return strEmailID; }
            set { strEmailID = value; }
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


