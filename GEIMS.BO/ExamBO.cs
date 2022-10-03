using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class ExamBo
    {
        #region Exam Class Properties

        public const string EXAM_TABLE = "tbl_Examination_M";
        public const string EXAM_EXAMINATIONID = "ExaminationID";
        public const string EXAM_NAME = "Name";
        public const string EXAM_DESCRIPTION = "Description";
        public const string EXAM_TRUSTMID = "TrustMID";
        public const string EXAM_SCHOOLMID = "SchoolMID";
        public const string EXAM_CREATEDUSERID = "CreatedUserID";
        public const string EXAM_CREATEDDATE = "CreatedDate";
        public const string EXAM_LASTMODIFIDEDATE = "LastModifideDate";
        public const string EXAM_LASTMODIFIDEUSERID = "LastModifideUserID";
        public const string EXAM_ISDELETED = "IsDeleted";



        private int intExaminationID = 0;
        private string strName = string.Empty;
        private string strDescription = string.Empty;
        private int intTrustMID = 0;
        private int intSchoolMID = 0;
        private int intCreatedUserID = 0;
        private string strCreatedDate = string.Empty;
        private string strLastModifideDate = string.Empty;
        private int intLastModifideUserID = 0;
        private int intIsDeleted = 0;

        #endregion

        #region ---Properties---
        public int ExaminationID
        {
            get { return intExaminationID; }
            set { intExaminationID = value; }
        }
        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }
        public string Description
        {
            get { return strDescription; }
            set { strDescription = value; }
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
        public string LastModifideDate
        {
            get { return strLastModifideDate; }
            set { strLastModifideDate = value; }
        }
        public int LastModifideUserID
        {
            get { return intLastModifideUserID; }
            set { intLastModifideUserID = value; }
        }
        public int IsDeleted
        {
            get { return intIsDeleted; }
            set { intIsDeleted = value; }
        }

        #endregion
    }
}


