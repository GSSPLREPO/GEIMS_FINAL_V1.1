using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
    public class DepartmentBO
    {
        #region Department Class Properties

        public const string DEPARTMENT_TABLE = "tbl_Department_M";
        public const string DEPARTMENT_DEPARTMENTID = "DepartmentID";
        public const string DEPARTMENT_DEPARTMENTNAMEENG = "DepartmentNameENG";
        public const string DEPARTMENT_DEPARTMENTNAMEGUJ = "DepartmentNameGUJ";
        public const string DEPARTMENT_DESCRIPTION = "Description";
        public const string DEPARTMENT_SCHOOLMID = "SchoolMID";
        public const string DEPARTMENT_TRUSTMID = "TrustMID";
        public const string DEPARTMENT_ISDELETED = "IsDeleted";
        public const string DEPARTMENT_CREATEDUSERID = "CreatedUserID";
        public const string DEPARTMENT_CREATEDDATE = "CreatedDate";
        public const string DEPARTMENT_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string DEPARTMENT_LASTMODIFIEDDATE = "LastModifiedDate";



        private int intDepartmentID = 0;
        private string strDepartmentNameENG = string.Empty;
        private string strDepartmentNameGUJ = string.Empty;
        private string strDescription = string.Empty;
        private int intSchoolMID = 0;
        private int intTrustMID = 0;
        private int intIsDeleted = 0;
        private int intCreatedUserID = 0;
        private string strCreatedDate = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;

        #endregion

        #region ---Properties---
        public int DepartmentID
        {
            get { return intDepartmentID; }
            set { intDepartmentID = value; }
        }
        public string DepartmentNameENG
        {
            get { return strDepartmentNameENG; }
            set { strDepartmentNameENG = value; }
        }
        public string DepartmentNameGUJ
        {
            get { return strDepartmentNameGUJ; }
            set { strDepartmentNameGUJ = value; }
        }
        public string Description
        {
            get { return strDescription; }
            set { strDescription = value; }
        }
        public int SchoolMID
        {
            get { return intSchoolMID; }
            set { intSchoolMID = value; }
        }
        public int TrustMID
        {
            get { return intTrustMID; }
            set { intTrustMID = value; }
        }
        public int IsDeleted
        {
            get { return intIsDeleted; }
            set { intIsDeleted = value; }
        }
        public int CreatedUserID
        {
            get { return intCreatedUserID; }
            set { intCreatedUserID = value; }
        }
        public string CreatedDate
        {
            get { return strCreatedDate; }
            set { strCreatedDate = value; }
        }
        public int LastModifiedUserID
        {
            get { return intLastModifiedUserID; }
            set { intLastModifiedUserID = value; }
        }
        public string LastModifiedDate
        {
            get { return strLastModifiedDate; }
            set { strLastModifiedDate = value; }
        }

        #endregion
    }
}
