using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class EmployeeWorkTimeBO
    {
        #region Employee WorkTime Class Properties

        public const string EMPLOYEEWORKTIME_TABLE = "tbl_EmployeeWorkTiming";
        public const string EMPLOYEEWORKTIME_WORKTIMEINGMID = "WorkTimingMID";
        public const string EMPLOYEEWORKTIME_TRUSTMID = "TrustMID";
        public const string EMPLOYEEWORKTIME_SCHOOLMID = "SchoolMID";
        public const string EMPLOYEEWORKTIME_EMPLOYEEMID = "EmployeeMID";
        public const string EMPLOYEEWORKTIME_DEPARTMENTID = "DepartmentID";
        public const string EMPLOYEEWORKTIME_DAYSOFWEEK = "Daysofweek";
        public const string EMPLOYEEWORKTIME_STARTTIME = "StartTime";
    
        public const string EMPLOYEEWORKTIME_ENDTIME = "EndTime";
        public const string EMPLOYEEWORKTIME_TOTALTIME = "TotalTime";
        public const string EMPLOYEEWORKTIME_RECESSSTARTTIME = "RecessStartTime";
        public const string EMPLOYEEWORKTIME_RECESSENDTIME = "RecessEndTime";
        public const string EMPLOYEEWORKTIME_FIRSTHALFTIME = "FirstHalfTime";
        public const string EMPLOYEEWORKTIME_SECONDHALFTIME = "SecondHalfTime";
        public const string EMPLOYEEWORKTIME_SHIFTNAME = "ShiftName";
        public const string EMPLOYEEWORKTIME_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string EMPLOYEEWORKTIME_LASTMODIFIEDDATE = "LastModifiedDate";
        public const string EMPLOYEEWORKTIME_CREATEMODIFIEDUSERID = "CreateModifiedUserID";
        public const string EMPLOYEEWORKTIME_CREATEDMODIFIEDDATE = "CreatedModifiedDate";
        public const string EMPLOYEEWORKTIME_ISDELETED = "IsDeleted";



        private int intWorkTimingID = 0;
        private int intTrustMID = 0;
        private int intSchoolMID = 0;
        private int intEmployeeMID = 0;
        private int intDepartmentID = 0;
        private int intDaysofweek = 0;
        private string strstartTime = string.Empty;
        private string strEndTime = string.Empty;
        private string strTotalTime = string.Empty;
        private string strRecessStart = string.Empty;
        private string strRecessEnd = string.Empty;
        private string strFirstHalfTime = string.Empty;
        private string strSecondHalfTime = string.Empty;
        private string strShiftName = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;
        private int intCreateModifiedUserID = 0;
        private string strCreatedModifiedDate = string.Empty;
        private int intIsDeleted = 0;


        #endregion

        #region ---Properties---
        public int WorkTimeMID
        {
            get { return intWorkTimingID; }
            set { intWorkTimingID = value; }
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
        public int DepartmentID
        {
            get { return intDepartmentID; }
            set { intDepartmentID = value; }
        }

        public int Daysofweek
        {
            get { return intDaysofweek; }
            set { intDaysofweek = value; }
        }
        public string StartTime
        {
            get { return strstartTime; }
            set { strstartTime = value; }
        }

        public string EndTime
        {
            get { return strEndTime; }
            set { strEndTime = value; }
        }
        public string TotalTime
        {
            get { return strTotalTime; }
            set { strTotalTime = value; }
        }

        public string RecessStartTime
        {
            get { return strRecessStart; }
            set { strRecessStart = value; }
        }

        public string RecessEndTime
        {
            get { return strRecessEnd; }
            set { strRecessEnd = value; }
        }

        public string FirstHalfTime
        {
            get { return strFirstHalfTime; }
            set { strFirstHalfTime = value; }
        }

        public string SecondHalfTime
        {
            get { return strSecondHalfTime; }
            set { strSecondHalfTime = value; }
        }

        public string ShiftName
        {
            get { return strShiftName; }
            set { strShiftName = value; }
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
