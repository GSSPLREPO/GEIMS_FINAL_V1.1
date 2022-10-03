namespace GEIMS.BO
{
	public class ClassWiseFeesTemplateTBO
	{
		#region ClassWiseFeesTemplateT Class Properties

		public const string CLASSWISEFEESTEMPLATET_TABLE = "tbl_ClassWiseFeesTemplate_T";
		public const string CLASSWISEFEESTEMPLATET_CLASSWISEFEESTEMPLATETID = "ClassWiseFeesTemplateTID";
		public const string CLASSWISEFEESTEMPLATET_TRUSTMID = "TrustMID";
		public const string CLASSWISEFEESTEMPLATET_CLASSMID = "ClassMID";
		public const string CLASSWISEFEESTEMPLATET_SCHOOLMID = "SchoolMID";
		public const string CLASSWISEFEESTEMPLATET_FEESCATEGORYMID = "FeesCategoryMID";
		public const string CLASSWISEFEESTEMPLATET_DIVISIONTID = "DivisionTID";
		public const string CLASSWISEFEESTEMPLATET_FEESAMOUNT = "FeesAmount";
		public const string CLASSWISEFEESTEMPLATET_ACADEMICYEAR = "AcademicYear";
		public const string CLASSWISEFEESTEMPLATET_LASTMODIFIEDUSERID = "LastModifiedUserID";
		public const string CLASSWISEFEESTEMPLATET_LASTMODIFIEDDATE = "LastModifiedDate";
		public const string CLASSWISEFEESTEMPLATET_ISDELETED = "Isdeleted";
        public const string CLASSWISEFEESTEMPLATET_FEESAMOUNTFORMALE = "FeesAmountForMale";
        public const string CLASSWISEFEESTEMPLATET_FEESAMOUNTFORFEMALE = "FeesAmountForFemale";




        private int intClassWiseFeesTemplateTID = 0;
		private int intTrustMID = 0;
		private int intClassMID = 0;
		private int intSchoolMID = 0;
		private int intFeesCategoryMID = 0;
        private int intDivisionTID = 0;
		private double dbFeesAmount = 0.0;
		private string strAcademicYear = string.Empty;
		private int intLastModifiedUserID = 0;
		private string strLastModifiedDate = string.Empty;
		private int intIsdeleted = 0;
        private double dbFeesAmountForMale = 0.0;
        private double dbFeesAmountForFemale = 0.0;

        #endregion

        #region ---Properties---
        public int ClassWiseFeesTemplateTID
		{
			get { return intClassWiseFeesTemplateTID; }
			set { intClassWiseFeesTemplateTID = value; }
		}
		public int TrustMID
		{
			get { return intTrustMID; }
			set { intTrustMID = value; }
		}
		public int ClassMID
		{
			get { return intClassMID; }
			set { intClassMID = value; }
		}
		public int SchoolMID
		{
			get { return intSchoolMID; }
			set { intSchoolMID = value; }
		}
		public int FeesCategoryMID
		{
			get { return intFeesCategoryMID; }
			set { intFeesCategoryMID = value; }
		}
		public int DivisionTID
		{
            get { return intDivisionTID; }
            set { intDivisionTID = value; }
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
        public double FeesAmountForMale
        {
            get { return dbFeesAmountForMale; }
            set { dbFeesAmountForMale = value; }
        }
        public double FeesAmountForFemale
        {
            get { return dbFeesAmountForFemale; }
            set { dbFeesAmountForFemale = value; }
        }

        #endregion
    }
}


