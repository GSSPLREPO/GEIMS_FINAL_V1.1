namespace GEIMS.BO
{
    public class StudentFeesTemplateTBO
    {
        #region StudentFeesTemplateT Class Properties

        public const string STUDENTFEESTEMPLATET_TABLE = "tbl_StudentFeesTemplate_T";
        public const string STUDENTFEESTEMPLATET_STUDENTFEESTEMPLATETID = "StudentFeesTemplateTID";
        public const string STUDENTFEESTEMPLATET_FEESCATEGORYMID = "FeesCategoryMID";
        public const string STUDENTFEESTEMPLATET_TRUSTMID = "TrustMID";
        public const string STUDENTFEESTEMPLATET_SCHOOLMID = "SchoolMID";
        public const string STUDENTFEESTEMPLATET_CLASSMID = "ClassMID";
        public const string STUDENTFEESTEMPLATET_DIVISIONTID = "DivisionTID";
        public const string STUDENTFEESTEMPLATET_STUDENTMID = "StudentMID";
        public const string STUDENTFEESTEMPLATET_CLASSWISEFEESTEMPLATETID = "ClassWiseFeesTemplateTID";
        public const string STUDENTFEESTEMPLATET_FEESAMOUNT = "FeesAmount";
        public const string STUDENTFEESTEMPLATET_ACADEMICYEAR = "AcademicYear";
        public const string STUDENTFEESTEMPLATET_ISLATE = "IsLate";
        public const string STUDENTFEESTEMPLATET_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string STUDENTFEESTEMPLATET_LASTMODIFIEDDATE = "LastModifiedDate";
        public const string STUDENTFEESTEMPLATET_ISDELETED = "Isdeleted";

        private int intStudentFeesTemplateTID = 0;
        private int intFeesCategoryMID = 0;
        private int intTrustMID = 0;
        private int intSchoolMID = 0;
        private int intClassMID = 0;
        private int intDivisionTID = 0;
        private int intStudentMID = 0;
        private int intClassWiseFeesTemplateTID = 0;
        private double dbFeesAmount = 0.0;
        private string strAcademicYear = string.Empty;
        private int intIsLate = 0;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;
        private int intIsdeleted = 0;

        #endregion

        #region ---Properties---
        public int StudentFeesTemplateTID
        {
            get { return intStudentFeesTemplateTID; }
            set { intStudentFeesTemplateTID = value; }
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
        public int IsLate
        {
            get { return intIsLate; }
            set { intIsLate = value; }
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
        public int Isdeleted
        {
            get { return intIsdeleted; }
            set { intIsdeleted = value; }
        }

        #endregion
    }
}


