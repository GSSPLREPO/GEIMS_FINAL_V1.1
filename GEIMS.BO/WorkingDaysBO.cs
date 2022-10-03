using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class WorkingDaysBo
    {
        #region WorkingDays Class Properties

        public const string WORKINGDAYS_TABLE = "tbl_TotalWorkDays_M";
        public const string WORKINGDAYS_WORKINGDAYID = "WorkingDayID";
        public const string WORKINGDAYS_TRUSTMID = "TrustMID";
        public const string WORKINGDAYS_SCHOOLMID = "SchoolMID";
        public const string WORKINGDAYS_ACADEMICYEAR = "AcademicYear";
        public const string WORKINGDAYS_MONTHID = "MonthID";
        public const string WORKINGDAYS_TOTALWORKINGDAYS = "TotalWorkingDays";
        public const string WORKINGDAYS_ISDELETED = "IsDeleted";
        public const string WORKINGDAYS_CREATEDUSERID = "CreatedUserID";
        public const string WORKINGDAYS_CREATEDDATE = "CreatedDate";
        public const string WORKINGDAYS_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string WORKINGDAYS_LASTMODIFIEDDATE = "LastModifiedDate";



        private int intWorkingDayID = 0;
        private int intTrustMID = 0;
        private int intSchoolMID = 0;
        private string strAcademicYear = string.Empty;
        private int intMonthID = 0;
        private int intTotalWorkingDays = 0;
        private int intIsDeleted = 0;
        private int intCreatedUserID = 0;
        private string strCreatedDate = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;

        #endregion

        #region ---Properties---
        public int WorkingDayID
        {
            get { return intWorkingDayID; }
            set { intWorkingDayID = value; }
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
        public string AcademicYear
        {
            get { return strAcademicYear; }
            set { strAcademicYear = value; }
        }
        public int MonthID
        {
            get { return intMonthID; }
            set { intMonthID = value; }
        }
        public int TotalWorkingDays
        {
            get { return intTotalWorkingDays; }
            set { intTotalWorkingDays = value; }
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


