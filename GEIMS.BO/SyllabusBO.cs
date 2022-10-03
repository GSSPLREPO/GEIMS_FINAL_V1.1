using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;


namespace GEIMS.BO
{
    public class SyllabusBO
    {
        #region Syllabus Class Properties

        public const string SYLLABUS_TABLE = "tbl_Syllabus_M";
        public const string SYLLABUS_SYLLABUSMIDD = "SyllabusMID";
        public const string SYLLABUS_SCHOOLMID = "SchoolMID";
        public const string SYLLABUS_SECTIONID = "SectionID";
        public const string SYLLABUS_CLASSID = "ClassID";
        public const string SYLLABUS_DIVISIONID = "DivisionID";
        public const string SYLLABUS_YEAR = "Year";
        public const string SYLLABUS_CHAPTERNAMEANDNOENG = "ChapterNameAndNoENG";
        public const string SYLLABUS_CHAPTERNAMEANDNOGUJ = "ChapterNameAndNoGUJ";
        public const string SYLLABUS_SYLLABUSDETAILSENG = "SyllabusDetailsENG";
        public const string SYLLABUS_SYLLABUSDETAILSGUJ = "SyllabusDetailsGUJ";
        public const string SYLLABUS_SYLLABUSREMARKS = "SyllabusRemarks";
        public const string SYLLABUS_CREATEDBYID = "CreatedByID";
        public const string SYLLABUS_CREATEDDATE = "CreatedDate";
        public const string SYLLABUS_MODIFIEDBYID = "ModifiedByID";
        public const string SYLLABUS_MODIFIEDDATE = "ModifiedDate";
        public const string SYLLABUS_SUBJECTID = "SubjectID";

        public const string SYLLABUSPLANNING_MONTHNO = "MonthNo";
        public const string SYLLABUSPLANNING_TEACHERMID = "TeacherMID";
        public const string SYLLABUSPLANNING_PLANSTARTDATE = "PlannedStartDate";
        public const string SYLLABUSPLANNING_PLANENDDATE = "PlannedEndDate";
        public const string SYLLABUSPLANNING_ACTUALSTARTDATE = "ActualStartDate";
        public const string SYLLABUSPLANNING_ACTUALENDDATE = "ActualEndDate";
        public const string SYLLABUSPLANNING_SYLLABUSCOVERED = "SyllabusCovered";



        private int intSyllabusMID = 0;
        private int intSchoolMID = 0;
        private int intClassID = 0;
        private int intDivisionID = 0;
        private int intSectionID = 0;
        private string strYear = string.Empty;
        private string strChapterNameAndNoENG = string.Empty;
        private string strChapterNameAndNoGUJ = string.Empty;
        private string strSyllabusDetailsENG = string.Empty;
        private string strSyllabusDetailsGUJ = string.Empty;
        private string strSyllabusRemarks = string.Empty;
        private int intCreatedByID = 0;
        private string strCreatedDate = string.Empty;
        private int intModifiedByID = 0;
        private string strModifiedDate = string.Empty;
        private int intSubjectID = 0;

        private int intMonthNo = 0;
        private int intTeacherMID = 0;
        private string strPlanStartDate = string.Empty;
        private string strPlanEndDate = string.Empty;
        private DateTime dtActualStartDate = DateTime.UtcNow;
        private DateTime dtActualEndDate = DateTime.UtcNow;
        private string strSyllabusCovered = string.Empty;


        #endregion

        #region ---Properties---
        public int SyllabusMID
        {
            get { return intSyllabusMID; }
            set { intSyllabusMID = value; }
        }
        public int SchoolMID
        {
            get { return intSchoolMID; }
            set { intSchoolMID = value; }
        }
        public int ClassID
        {
            get { return intClassID; }
            set { intClassID = value; }
        }
        public int SectionID
        {
            get { return intSectionID; }
            set { intSectionID = value; }
        }
        public int DivisionID
        {
            get { return intDivisionID; }
            set { intDivisionID = value; }
        }
        public string Year
        {
            get { return strYear; }
            set { strYear = value; }
        }
        public string ChapterNameAndNoENG
        {
            get { return strChapterNameAndNoENG; }
            set { strChapterNameAndNoENG = value; }
        }
        public string ChapterNameAndNoGUJ
        {
            get { return strChapterNameAndNoGUJ; }
            set { strChapterNameAndNoGUJ = value; }
        }
        public string SyllabusDetailsENG
        {
            get { return strSyllabusDetailsENG; }
            set { strSyllabusDetailsENG = value; }
        }
        public string SyllabusDetailsGUJ
        {
            get { return strSyllabusDetailsGUJ; }
            set { strSyllabusDetailsGUJ = value; }
        }
        public string SyllabusRemarks
        {
            get { return strSyllabusRemarks; }
            set { strSyllabusRemarks = value; }
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
        public int SubjectID
        {
            get { return intSubjectID; }
            set { intSubjectID = value; }
        }
        public int MonthNo
        {
            get { return intMonthNo; }
            set { intMonthNo = value; }
        }

        public int TeacherMID
        {
            get { return intTeacherMID; }
            set { intTeacherMID = value; }
        }
        public string PlannedStartDate
        {
            get { return strPlanStartDate; }
            set { strPlanStartDate = value; }
        }
        public string PlannedEndDate
        {
            get { return strPlanEndDate; }
            set { strPlanEndDate = value; }
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
        public string SyllabusCovered
        {
            get { return strSyllabusCovered; }
            set { strSyllabusCovered = value; }
        }


        #endregion
    }
}
