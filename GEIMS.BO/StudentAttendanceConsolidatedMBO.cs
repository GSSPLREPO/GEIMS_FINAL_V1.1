using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;


namespace GEIMS.BO
{
   public class StudentAttendanceConsolidatedMBO
    {
        #region Student Attendance Consolidated Class Properties

        public const string STUDENTATTENDANCECONSOLIDATED_TABLE = "tbl_StudentAttendanceConsolidated_M";
        public const string STUDENTATTENDANCECONSOLIDATED_STUDENTATTENDANCECONSOLIDATEDMID = "StudentAttendanceConsolidatedMID";
        public const string STUDENTATTENDANCECONSOLIDATED_TRUSTMID = "TrustMID";
        public const string STUDENTATTENDANCECONSOLIDATED_SCHOOLMID = "SchoolMID";
        public const string STUDENTATTENDANCECONSOLIDATED_EMPLOYEEMID = "EmployeeMID";
        public const string STUDENTATTENDANCECONSOLIDATED_CLASSMID = "ClassMID";
        public const string STUDENTATTENDANCECONSOLIDATED_DIVISIONTID = "DivisionTID";
        public const string STUDENTATTENDANCECONSOLIDATED_ACADEMICYEAR = "AcademicYear";
        public const string STUDENTATTENDANCECONSOLIDATED_ATTENDANCETAKENDATE = "AttendanceTakenDate";
        public const string STUDENTATTENDANCECONSOLIDATED_TOTALSTUDENTCOUNT = "TotalStudentCount";
        public const string STUDENTATTENDANCECONSOLIDATED_PRESENTSTUDENTCOUNT = "PresentStudentCount";
        public const string STUDENTATTENDANCECONSOLIDATED_ABSENTSTUDENTCOUNT = "AbsentStudentCount";
        public const string STUDENTATTENDANCECONSOLIDATED_ISDELETED = "IsDeleted";
        public const string STUDENTATTENDANCECONSOLIDATED_CREATEDBY = "CreatedBy";
        public const string STUDENTATTENDANCECONSOLIDATED_CREATEDDATE = "CreatedDate";
        public const string STUDENTATTENDANCECONSOLIDATED_MODIFIEDBY = "ModifiedBy";
        public const string STUDENTATTENDANCECONSOLIDATED_MODIFIEDDATE = "ModifiedDate";

        private int intStudentAttendanceConsolidatedMID = 0;
        private int intTrustMID = 0;
        private int intSchoolMID = 0;
        private int intEmployeeMID = 0;
        private int intClassMID = 0;
        private int intDivisionTID = 0;
        private string strAcademicYear = string.Empty;
        private string strTotalStudentCount = string.Empty;
        private string strPresentStudentCount = string.Empty;
        private string strAbsentStudentCount = string.Empty;
        private int intIsDeleted = 0;
        private int intCreatedBy = 0;
        private DateTime dtCreatedDate = DateTime.UtcNow;
        private int intModifiedBy = 0;
        private DateTime dtModifiedDate = DateTime.UtcNow;
        private DateTime dtAttendanceTakenDate = DateTime.UtcNow;



        #endregion


        public int TrustMID
        {
            get { return intTrustMID; }
            set { intTrustMID = value; }
        }
        public int StudentAttendanceConsolidatedMID
        {
            get { return intStudentAttendanceConsolidatedMID; }
            set { intStudentAttendanceConsolidatedMID = value; }
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
        public string TotalStudentCount
        {
            get { return strTotalStudentCount; }
            set { strTotalStudentCount = value; }
        }
        public string PresentStudentCount
        {
            get { return strPresentStudentCount; }
            set { strPresentStudentCount = value; }
        }
        public string AbsentStudentCount
        {
            get { return strAbsentStudentCount; }
            set { strAbsentStudentCount = value; }
        }
        public int IsDeleted
        {
            get { return intIsDeleted; }
            set { intIsDeleted = value; }
        }
        public int CreatedBy
        {
            get { return intCreatedBy; }
            set { intCreatedBy = value; }
        }
        public DateTime CreatedDate
        {
            get { return dtCreatedDate; }
            set { dtCreatedDate = value; }
        }
        public int ModifiedBy
        {
            get { return intModifiedBy; }
            set { intModifiedBy = value; }
        }
        public DateTime ModifiedDate
        {
            get { return dtModifiedDate; }
            set { dtModifiedDate = value; }
        }
        public DateTime AttendanceTakenDate
        {
            get { return dtAttendanceTakenDate; }
            set { dtAttendanceTakenDate = value; }
        }

    }
}
