using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class ClasswiseStudentTemplateBO
    {
        #region ClasswiseStudentTemplateBO Class Properties

        public const string CLASSWISESTUDENTTEMPLATEBO_TABLE = "tbl_ClassWiseStudentTemplate_T";
        public const string CLASSWISESTUDENTTEMPLATEBO_CLASSWISESTUDENTTEMPLATETID = "ClassWiseStudentTemplateTID";
        public const string CLASSWISESTUDENTTEMPLATEBO_FEESCATEGORYMID = "FeesCategoryMID";
        public const string CLASSWISESTUDENTTEMPLATEBO_TRUSTMID = "TrustMID";
        public const string CLASSWISESTUDENTTEMPLATEBO_SCHOOLMID = "SchoolMID";
        public const string CLASSWISESTUDENTTEMPLATEBO_CLASSMID = "ClassMID";
        public const string CLASSWISESTUDENTTEMPLATEBO_DIVISIONTID = "DivisionTID";
        public const string CLASSWISESTUDENTTEMPLATEBO_STUDENTMID = "StudentMID";
        public const string CLASSWISESTUDENTTEMPLATEBO_CLASSWISEFEESTEMPLATETID = "ClassWiseFeesTemplateTID";
        public const string CLASSWISESTUDENTTEMPLATEBO_FEESAMOUNT = "FeesAmount";
        public const string CLASSWISESTUDENTTEMPLATEBO_ACADEMICYEAR = "AcademicYear";
        public const string CLASSWISESTUDENTTEMPLATEBO_LASTMODIFIEDDATE = "LastModifiedDate";
        public const string CLASSWISESTUDENTTEMPLATEBO_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string CLASSWISESTUDENTTEMPLATEBO_ISDELETED = "IsDeleted";



        private int intClassWiseStudentTemplateTID = 0;
        private int intFeesCategoryMID = 0;
        private int intTrustMID = 0;
        private int intSchoolMID = 0;
        private int intClassMID = 0;
        private int intDivisionTID = 0;
        private int intStudentMID = 0;
        private int intClassWiseFeesTemplateTID = 0;
        private double dbFeesAmount = 0.0;
        private string strAcademicYear = string.Empty;
        private string strLastModifiedDate = string.Empty;
        private int intLastModifiedUserID = 0;
        private int intIsDeleted = 0;

        #endregion

        #region ---Properties---
        public int ClassWiseStudentTemplateTID
        {
            get { return intClassWiseStudentTemplateTID; }
            set { intClassWiseStudentTemplateTID = value; }
        }
        public int FeesCategoryMID
        {
            get { return intFeesCategoryMID; }
            set { intFeesCategoryMID = value; }
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
        public int StudentMID
        {
            get { return intStudentMID; }
            set { intStudentMID = value; }
        }
        public int ClassWiseFeesTemplateTID
        {
            get { return intClassWiseFeesTemplateTID; }
            set { intClassWiseFeesTemplateTID = value; }
        }
        public double FeesAmount
        {
            get { return dbFeesAmount; }
            set { dbFeesAmount = value; }
        }
        public string AcademicYear
        {
            get { return strAcademicYear; }
            set { strAcademicYear = value; }
        }
        public string LastModifiedDate
        {
            get { return strLastModifiedDate; }
            set { strLastModifiedDate = value; }
        }
        public int LastModifiedUserID
        {
            get { return intLastModifiedUserID; }
            set { intLastModifiedUserID = value; }
        }
        public int IsDeleted
        {
            get { return intIsDeleted; }
            set { intIsDeleted = value; }
        }

        #endregion
    }
}
