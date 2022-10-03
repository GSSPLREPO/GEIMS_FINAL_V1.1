using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class StudentMarksBO
    {
        #region StudentMarks Class Properties

        public const string STUDENTMARKS_TABLE = "tbl_StudentMarks_M";
        public const string STUDENTMARKS_STUDENTMARKSID = "StudentMarksId";
        public const string STUDENTMARKS_EXAMCONFIGID = "ExamConfigId";
        public const string STUDENTMARKS_SUBJECTID = "SubjectId";
        public const string STUDENTMARKS_STUDENTID = "StudentId";
        public const string STUDENTMARKS_EXAM = "Exam";
        public const string STUDENTMARKS_TOTALMARKS = "TotalMarks";
        public const string STUDENTMARKS_PASSINGMARKS = "PassingMarks";
        public const string STUDENTMARKS_OBTAINEDMARKS = "ObtainedMarks";
        public const string STUDENTMARKS_CREATEDBYID = "CreatedById";
        public const string STUDENTMARKS_CREATEDDATE = "CreatedDate";
        public const string STUDENTMARKS_LASTMODIFIEDBYID = "LastModifiedById";
        public const string STUDENTMARKS_LASTMODIFIEDBYDATE = "LastModifiedByDate";
        public const string STUDENTMARKS_ISDELETED = "IsDeleted";



        private int intStudentMarksId = 0;
        private int intExamConfigId = 0;
        private int intSubjectId = 0;
        private int intStudentId = 0;
        private string strExam = string.Empty;
        private int intTotalMarks = 0;
        private int intPassingMarks = 0;
        private int intObtainedMarks = 0;
        private int intCreatedById = 0;
        private string strCreatedDate = string.Empty;
        private int intLastModifiedById = 0;
        private string strLastModifiedByDate = string.Empty;
        private int intIsDeleted = 0;

        #endregion

        #region ---Properties---
        public int StudentMarksId
        {
            get { return intStudentMarksId; }
            set { intStudentMarksId = value; }
        }
        public int ExamConfigId
        {
            get { return intExamConfigId; }
            set { intExamConfigId = value; }
        }
        public int SubjectId
        {
            get { return intSubjectId; }
            set { intSubjectId = value; }
        }
        public int StudentId
        {
            get { return intStudentId; }
            set { intStudentId = value; }
        }
        public string Exam
        {
            get { return strExam; }
            set { strExam = value; }
        }
        public int TotalMarks
        {
            get { return intTotalMarks; }
            set { intTotalMarks = value; }
        }
        public int PassingMarks
        {
            get { return intPassingMarks; }
            set { intPassingMarks = value; }
        }
        public int ObtainedMarks
        {
            get { return intObtainedMarks; }
            set { intObtainedMarks = value; }
        }
        public int CreatedById
        {
            get { return intCreatedById; }
            set { intCreatedById = value; }
        }
        public string CreatedDate
        {
            get { return strCreatedDate; }
            set { strCreatedDate = value; }
        }
        public int LastModifiedById
        {
            get { return intLastModifiedById; }
            set { intLastModifiedById = value; }
        }
        public string LastModifiedByDate
        {
            get { return strLastModifiedByDate; }
            set { strLastModifiedByDate = value; }
        }
        public int IsDeleted
        {
            get { return intIsDeleted; }
            set { intIsDeleted = value; }
        }

        #endregion
    }
}
