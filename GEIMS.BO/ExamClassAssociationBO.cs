using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class ExamClassAssociationBo
    {
        #region ExamClassAssociation Class Properties

        public const string EXAMCLASSASSOCIATION_TABLE = "tbl_ExaminationClassDivisionAssociation_T";
        public const string EXAMCLASSASSOCIATION_EXAMCLASSDIVISIONID = "ExamClassDivisionID";
        public const string EXAMCLASSASSOCIATION_EXAMINATIONID = "ExaminationID";
        public const string EXAMCLASSASSOCIATION_TRUSTMID = "TrustMID";
        public const string EXAMCLASSASSOCIATION_SCHOOLMID = "SchoolMID";
        public const string EXAMCLASSASSOCIATION_CLASSMID = "ClassMID";
        public const string EXAMCLASSASSOCIATION_DIVISIONTID = "DivisionTID";
        public const string EXAMCLASSASSOCIATION_ACADEMICYEAR = "AcademicYear";
        public const string EXAMCLASSASSOCIATION_FROMDATE = "FromDate";
        public const string EXAMCLASSASSOCIATION_TODATE = "ToDate";
        public const string EXAMCLASSASSOCIATION_CREATEDUSERID = "CreatedUserID";
        public const string EXAMCLASSASSOCIATION_CREATEDDATE = "CreatedDate";
        public const string EXAMCLASSASSOCIATION_LASTMODIFIDEDATE = "LastModifideDate";
        public const string EXAMCLASSASSOCIATION_LASTMODIFIDEUSERID = "LastModifideUserID";
        public const string EXAMCLASSASSOCIATION_ISDELETED = "IsDeleted";



        private int intExamClassDivisionID = 0;
        private int intExaminationID = 0;
        private int intTrustMID = 0;
        private int intSchoolMID = 0;
        private int intClassMID = 0;
        private int intDivisionTID = 0;
        private string strAcademicYear = string.Empty;
        private string strFromDate = string.Empty;
        private string strToDate = string.Empty;
        private int intCreatedUserID = 0;
        private string strCreatedDate = string.Empty;
        private string strLastModifideDate = string.Empty;
        private int intLastModifideUserID = 0;
        private int intIsDeleted = 0;

        #endregion

        #region ---Properties---
        public int ExamClassDivisionID
        {
            get { return intExamClassDivisionID; }
            set { intExamClassDivisionID = value; }
        }
        public int ExaminationID
        {
            get { return intExaminationID; }
            set { intExaminationID = value; }
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
        public string FromDate
        {
            get { return strFromDate; }
            set { strFromDate = value; }
        }
        public string ToDate
        {
            get { return strToDate; }
            set { strToDate = value; }
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


