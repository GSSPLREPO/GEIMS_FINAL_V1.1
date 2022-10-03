using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class EmployeeattendanceBo
    {
        #region Employeeattendance Class Properties

        public const string EMPLOYEEATTENDANCE_TABLE = "tbl_EmployeeAttendance_M";
        public const string EMPLOYEEATTENDANCE_EMPLOYEEATTANDANCEMID = "EmployeeAttandenceMID";
        public const string EMPLOYEEATTENDANCE_TRUSTMID = "TrustMID";
        public const string EMPLOYEEATTENDANCE_SCHOOLMID = "SchoolMID";
        public const string EMPLOYEEATTENDANCE_EMPLOYEEMID = "EmployeeMID";
        public const string EMPLOYEEATTENDANCE_ACADEMICYEAR = "AcademicYear";
        public const string EMPLOYEEATTENDANCE_INTIME = "InTime";
        public const string EMPLOYEEATTENDANCE_RECOUTTIME = "RecessOutTime";
        public const string EMPLOYEEATTENDANCE_RECINTIME = "RecessInTime";
        public const string EMPLOYEEATTENDANCE_OUTTIME = "OutTime";
        public const string EMPLOYEEATTENDANCE_DATE = "Date";
        public const string EMPLOYEEATTENDANCE_TIME = "Time";
        public const string EMPLOYEEATTENDANCE_ISMANUAL = "IsManual";
        public const string EMPLOYEEATTENDANCE_TYPE = "Type";
        public const string EMPLOYEEATTENDANCE_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string EMPLOYEEATTENDANCE_LASTMODIFIEDDATE = "LastModifiedDate";
        public const string EMPLOYEEATTENDANCE_CREATEMODIFIEDUSERID = "CreateModifiedUserID";
        public const string EMPLOYEEATTENDANCE_CREATEDMODIFIEDDATE = "CreatedModifiedDate";
        public const string EMPLOYEEATTENDANCE_ISDELETED = "IsDeleted";
        public const string EMPLOYEEATTENDANCE_TotalTime = "TotalTime";

        private int intEmployeeAttandanceMID = 0;
        private int intTrustMID = 0;
        private int intSchoolMID = 0;
        private int intEmployeeMID = 0;
        private string strAcademicYear = string.Empty;
        private string strInTime = string.Empty;
        private string strRecOutTime = string.Empty;
        private string strRecInTime = string.Empty;
        private string strOutTime = string.Empty;
        private string strDate = string.Empty;
        private string strTime = string.Empty;
        private int intIsManual = 0;
        private int intType = 0;
        private string strHolidayType = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;
        private int intCreateModifiedUserID = 0;
        private string strCreatedModifiedDate = string.Empty;
        private int intIsDeleted = 0;
        private string strTotalTime = string.Empty;

        #endregion

        #region ---Properties---
        public int EmployeeAttandanceMID
        {
            get { return intEmployeeAttandanceMID; }
            set { intEmployeeAttandanceMID = value; }
        }
        public int TrustMID
        {
            get { return intTrustMID; }
            set { intTrustMID = value; }
        }
        public int SchoolMID
        {
            get { return intSchoolMID; }
            set { intSchoolMID = value; }
        }
        public int EmployeeMID
        {
            get { return intEmployeeMID; }
            set { intEmployeeMID = value; }
        }
        public string AcademicYear
        {
            get { return strAcademicYear; }
            set { strAcademicYear = value; }
        }
        public string TotalTime
        {
            get { return strTotalTime; }
            set { strTotalTime = value; }
        }
        public string InTime
        {
            get { return strInTime; }
            set { strInTime = value; }
        }
        public string RecOutTime
        {
            get { return strRecOutTime; }
            set { strRecOutTime = value; }
        }
        public string RecInTime
        {
            get { return strRecInTime; }
            set { strRecInTime = value; }
        }
       
        public string OutTime
        {
            get { return strOutTime; }
            set { strOutTime = value; }
        }
        public string Date
        {
            get { return strDate; }
            set { strDate = value; }
        }
        public string Time
        {
            get { return strTime; }
            set { strTime = value; }
        }
        public int IsManual
        {
            get { return intIsManual; }
            set { intIsManual = value; }
        }
        public int Type
        {
            get { return intType; }
            set { intType = value; }
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
        public int CreateModifiedUserID
        {
            get { return intCreateModifiedUserID; }
            set { intCreateModifiedUserID = value; }
        }
        public string CreatedModifiedDate
        {
            get { return strCreatedModifiedDate; }
            set { strCreatedModifiedDate = value; }
        }
        public int IsDeleted
        {
            get { return intIsDeleted; }
            set { intIsDeleted = value; }
        }

        #endregion
    }
}



