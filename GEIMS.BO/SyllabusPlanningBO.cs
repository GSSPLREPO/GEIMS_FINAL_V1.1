using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;


namespace GEIMS.BO
{
   public class SyllabusPlanningBO
    {
        #region Syllabus Class Properties

        public const string SYLLABUSPLANNING_TABLE = "tbl_SyllabusPlanning_T";
        public const string SYLLABUSPLANNING_SYLLABUSPLANTID = "SyllabusPlanTID";
        public const string SYLLABUSPLANNING_SYLLABUSMIDD = "SyllabusMID";
        public const string SYLLABUSPLANNING_MONTHNO = "MonthNo";
        public const string SYLLABUSPLANNING_TEACHERMID = "TeacherMID";
        public const string SYLLABUSPLANNING_PLANNEDSTARTDATE = "PlannedStartDate";
        public const string SYLLABUSPLANNING_PLANNEDENDDATE = "PlannedEndDate";
        public const string SYLLABUSPLANNING_ISACTIVE = "IsActive";
        public const string SYLLABUSPLANNING_ACTUALSTARTDATE = "ActualStartDate";
        public const string SYLLABUSPLANNING_ACTUALENDDATE = "ActualEndDate";
        public const string SYLLABUSPLANNING_SYLLABUSCOVERED = "SyllabusCovered";  
        public const string SYLLABUSPLANNING_CREATEDBYID = "CreatedByID";
        public const string SYLLABUSPLANNING_CREATEDDATE = "CreatedDate";
        public const string SYLLABUSPLANNING_MODIFIEDBYID = "ModifiedByID";
        public const string SYLLABUSPLANNING_MODIFIEDDATE = "ModifiedDate";




        private int intSyllabusPlanningTID = 0;
        private int intSyllabusMID = 0;
        private int intMonthNo = 0;
        private int intTeacherMID = 0;
        private DateTime dtPlannedStartDate = DateTime.UtcNow;
        private DateTime dtPlannedEndDate = DateTime.UtcNow;
        private bool boolIsActive = false;
        private DateTime dtActualStartDate = DateTime.UtcNow;
        private DateTime dtActualEndDate = DateTime.UtcNow;
        private string strSyllabusCovered = string.Empty;      
        private int intCreatedByID = 0;
        private string strCreatedDate = string.Empty;
        private int intModifiedByID = 0;
        private string strModifiedDate = string.Empty;


        #endregion

        #region ---Properties---
        public int SyllabusPlanTID
        {
            get { return intSyllabusPlanningTID; }
            set { intSyllabusPlanningTID = value; }
        }
        public int SyllabusMID
        {
            get { return intSyllabusMID; }
            set { intSyllabusMID = value; }
        }
        public int MonthNo {
            get { return intMonthNo; }
            set { intMonthNo = value; }
        }
        public int TeacherMID {
            get { return intTeacherMID; }
            set { intTeacherMID = value; }
        }
        public DateTime PlannedStartDate {
            get { return dtPlannedStartDate; }
            set { dtPlannedStartDate = value; }
        }
        public DateTime PlannedEndDate
        {
            get { return dtPlannedEndDate; }
            set { dtPlannedEndDate = value; }
        }
        public bool IsActive
        {
            get { return boolIsActive; }
            set { boolIsActive = value; }
        }
        public DateTime ActualStartDate
        {
            get { return dtActualStartDate; }
            set { dtActualStartDate = value; }
        }
        public DateTime ActualEndDate
        {
            get { return dtActualEndDate; }
            set { dtActualEndDate = value; }
        }
        public string SyllabusCovered {
            get { return strSyllabusCovered; }
            set { strSyllabusCovered = value; }
        }
        public string CreatedDate
        {
            get { return strCreatedDate; }
            set { strCreatedDate = value; }
        }
        public string ModifiedDate
        {
            get { return strModifiedDate; }
            set { strModifiedDate = value; }
        }
        public int CreatedByID
        {
            get { return intCreatedByID; }
            set { intCreatedByID = value; }
        }
        public int ModifiedByID
        {
            get { return intModifiedByID; }
            set { intModifiedByID = value; }
        }
        #endregion
    }
}
