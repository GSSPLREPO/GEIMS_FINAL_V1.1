using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
    public class StudentAttendenceBO
    {
        #region StudentAttendence Class Properties

        public const string STUDENTATTENDENCE_TABLE = "tbl_StudentAttendence_M";
        public const string STUDENTATTENDENCE_STUDENTATTENDENCEMID = "StudentAttendenceMID";
        public const string STUDENTATTENDENCE_TRUSTMID = "TrustMID";
        public const string STUDENTATTENDENCE_SCHOOLMID = "SchoolMID";
        public const string STUDENTATTENDENCE_EMPLOYEEMID = "EmployeeMID";
        public const string STUDENTATTENDENCE_CLASSMID = "ClassMID";
        public const string STUDENTATTENDENCE_DIVISIONTID = "DivisionTID";
        public const string STUDENTATTENDENCE_ACADEMICYEAR = "AcademicYear";
        public const string STUDENTATTENDENCE_PRESENTSTUDENTIDS = "PresentStudentIDs";
        public const string STUDENTATTENDENCE_ABSENTSTUDENTIDS = "AbsentStudentIds";
        public const string STUDENTATTENDENCE_DATE = "Date";
        public const string STUDENTATTENDENCE_TIME = "Time";
        public const string STUDENTATTENDENCE_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string STUDENTATTENDENCE_LASTMODIFIEDDATE = "LastModifiedDate";
        public const string STUDENTATTENDENCE_ISDELETED = "IsDeleted";



        private int intStudentAttendenceMID = 0;
        private int intTrustMID = 0;
        private int intSchoolMID = 0;
        private int intEmployeeMID = 0;
        private int intClassMID = 0;
        private int intDivisionTID = 0;
        private string strAcademicYear = string.Empty;
        private string strPresentStudentIDs = string.Empty;
        private string strAbsentStudentIds = string.Empty;
        private string strDate = string.Empty;
        private string strTime = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;
        private int intIsDeleted = 0;

        #endregion

        #region ---Properties---
        public int StudentAttendenceMID
        {
            get { return intStudentAttendenceMID; }
            set { intStudentAttendenceMID = value; }
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
        public int ClassMID
        {
            get { return intClassMID; }
            set { intClassMID = value; }
        }
        public int DivisionTID
        {
            get { return intDivisionTID; }
            set { intDivisionTID = value; }
        }
        public string AcademicYear
        {
            get { return strAcademicYear; }
            set { strAcademicYear = value; }
        }
        public string PresentStudentIDs
        {
            get { return strPresentStudentIDs; }
            set { strPresentStudentIDs = value; }
        }
        public string AbsentStudentIds
        {
            get { return strAbsentStudentIds; }
            set { strAbsentStudentIds = value; }
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
        public int IsDeleted
        {
            get { return intIsDeleted; }
            set { intIsDeleted = value; }
        }

        #endregion
    }
}
