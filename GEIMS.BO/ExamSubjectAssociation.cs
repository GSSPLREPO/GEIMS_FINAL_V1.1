using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class ExamSubjectAssociationBo
    {
        #region ExamSubjectAssociation Class Properties

        public const string EXAMSUBJECTASSOCIATION_TABLE = "tbl_ExamSubjectAssociation_T";
        public const string EXAMSUBJECTASSOCIATION_EXAMSUBJECTASSOCIATIONID = "ExamSubjectAssociationID";
        public const string EXAMSUBJECTASSOCIATION_EXAMCLASSDIVISIONID = "ExamClassDivisionID";
        public const string EXAMSUBJECTASSOCIATION_SUBJECTMID = "SubjectMID";
        public const string EXAMSUBJECTASSOCIATION_EXAMDATE = "ExamDate";
        public const string EXAMSUBJECTASSOCIATION_EXAMTIME = "ExamTime";
        public const string EXAMSUBJECTASSOCIATION_TOTALMARKS = "TotalMarks";
        public const string EXAMSUBJECTASSOCIATION_PASSINGMARKS = "PassingMarks";
        public const string EXAMSUBJECTASSOCIATION_CREATEDUSERID = "CreatedUserID";
        public const string EXAMSUBJECTASSOCIATION_LASTMODIFIDEUSERID = "LastModifideUserID";
        public const string EXAMSUBJECTASSOCIATION_CREATEDDATE = "CreatedDate";
        public const string EXAMSUBJECTASSOCIATION_LASTMODIFIDEDATE = "LastModifideDate";
        public const string EXAMSUBJECTASSOCIATION_ISDELETED = "IsDeleted";



        private int intExamSubjectAssociationID = 0;
        private int intExamClassDivisionID = 0;
        private int intSubjectMID = 0;
        private string strExamDate = string.Empty;
        private string strExamTime = string.Empty;
        private int intTotalMarks = 0;
        private int intPassingMarks = 0;
        private int intCreatedUserID = 0;
        private int intLastModifideUserID = 0;
        private string strCreatedDate = string.Empty;
        private string strLastModifideDate = string.Empty;
        private int intIsDeleted = 0;

        #endregion

        #region ---Properties---
        public int ExamSubjectAssociationID
        {
            get { return intExamSubjectAssociationID; }
            set { intExamSubjectAssociationID = value; }
        }
        public int ExamClassDivisionID
        {
            get { return intExamClassDivisionID; }
            set { intExamClassDivisionID = value; }
        }
        public int SubjectMID
        {
            get { return intSubjectMID; }
            set { intSubjectMID = value; }
        }
        public string ExamDate
        {
            get { return strExamDate; }
            set { strExamDate = value; }
        }
        public string ExamTime
        {
            get { return strExamTime; }
            set { strExamTime = value; }
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
        public int CreatedUserID
        {
            get { return intCreatedUserID; }
            set { intCreatedUserID = value; }
        }
        public int LastModifideUserID
        {
            get { return intLastModifideUserID; }
            set { intLastModifideUserID = value; }
        }
        public string CreatedDate
        {
            get { return strCreatedDate; }
            set { strCreatedDate = value; }
        }
        public string LastModifideDate
        {
            get { return strLastModifideDate; }
            set { strLastModifideDate = value; }
        }
        public int IsDeleted
        {
            get { return intIsDeleted; }
            set { intIsDeleted = value; }
        }

        #endregion
    }
}


