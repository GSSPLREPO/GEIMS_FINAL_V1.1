using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class ExamConfigurationBO
    {
        #region ExamConfigurationBO Class Properties

        public const string EXAMCONFIGURATIONBO_TABLE = "ExamConfiguration";
        public const string EXAMCONFIGURATIONBO_EXAMCONFIGID = "ExamConfigId";
        public const string EXAMCONFIGURATIONBO_CLASSID = "ClassId";
        public const string EXAMCONFIGURATIONBO_DIVISIONID = "DivisionId";
        public const string EXAMCONFIGURATIONBO_ACADEMICYEAR = "AcademicYear";
        public const string EXAMCONFIGURATIONBO_EXAM = "Exam";
        public const string EXAMCONFIGURATIONBO_SUBJECTID = "SubjectId";
        public const string EXAMCONFIGURATIONBO_CREATEDBYUSERID = "CreatedByUserId";
        public const string EXAMCONFIGURATIONBO_CREATEDDATE = "CreatedDate";
        public const string EXAMCONFIGURATIONBO_LASTMODIFIEDBY = "LastModifiedBy";
        public const string EXAMCONFIGURATIONBO_LASTMODIFIEDDATE = "LastModifiedDate";



        private int intExamConfigId = 0;
        private int intSchoolMID = 0;
        private int intClassId = 0;
        private int intDivisionId = 0;
        private string strAcademicYear = string.Empty;
        private string strExam = string.Empty;
        private string strSubjectId = string.Empty;
        private int intCreatedByUserId = 0;
        private string strCreatedDate = string.Empty;
        private int intLastModifiedBy = 0;
        private string strLastModifiedDate = string.Empty;
        private int intIsDeleted = 0;

        #endregion

        #region ---Properties---
        public int ExamConfigId
        {
            get { return intExamConfigId; }
            set { intExamConfigId = value; }
        }
        public int SchoolMID
        {
            get { return intSchoolMID; }
            set { intSchoolMID = value; }
        }
        public int ClassId
        {
            get { return intClassId; }
            set { intClassId = value; }
        }
        public int DivisionId
        {
            get { return intDivisionId; }
            set { intDivisionId = value; }
        }
        public string AcademicYear
        {
            get { return strAcademicYear; }
            set { strAcademicYear = value; }
        }
        public string Exam
        {
            get { return strExam; }
            set { strExam = value; }
        }
        public string SubjectId
        {
            get { return strSubjectId; }
            set { strSubjectId = value; }
        }
        public int CreatedByUserId
        {
            get { return intCreatedByUserId; }
            set { intCreatedByUserId = value; }
        }
        public string CreatedDate
        {
            get { return strCreatedDate; }
            set { strCreatedDate = value; }
        }
        public int LastModifiedBy
        {
            get { return intLastModifiedBy; }
            set { intLastModifiedBy = value; }
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
