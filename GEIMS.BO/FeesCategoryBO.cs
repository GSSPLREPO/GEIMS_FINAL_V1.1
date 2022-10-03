namespace GEIMS.BO
{
	public class FeesCategoryBO
	{
		#region FeesCategory Class Properties

		public const string FEESCATEGORY_TABLE = "tbl_FeesCategory_M";
		public const string FEESCATEGORY_FEESCATEGORYMID = "FeesCategoryMID";
		public const string FEESCATEGORY_TRUSTMID = "TrustMID";
		public const string FEESCATEGORY_SCHOOLMID = "SchoolMID";
		public const string FEESCATEGORY_FEESNAME = "FeesName";
		public const string FEESCATEGORY_FEESTYPE = "FeesType";
        public const string FEESCATEGORY_OutstandingMonth = "OutstandingMonth";
		public const string FEESCATEGORY_LASTMODIFIEDUSERID = "LastModifiedUserID";
		public const string FEESCATEGORY_LASTMODIFIEDDATE = "LastModifiedDate";
		public const string FEESCATEGORY_ISDELETED = "Isdeleted";
        public const string FEESCATEGORY_FEEABBREVIATION = "FeeAbbreviation";
        public const string FEESCATEGORY_FEEGROUPID = "FeeGroupID";
        public const string FEESCATEGORY_PRIORITY = "Priority";



		private int intFeesCategoryMID = 0;
		private int intTrustMID = 0;
		private int intSchoolMID = 0;
		private string strFeesName = string.Empty;
		private string strFeesType = string.Empty;
        private int intOutstandingMonth = 0;
        private string strFeesAbbreviation = string.Empty;
		private int intLastModifiedUserID = 0;
		private string strLastModifiedDate = string.Empty;
		private int intIsdeleted = 0;
        private int intFeeGroupID = 0;
        private int intPriority = 0;

		#endregion

		#region ---Properties---
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
		public string FeesName
		{
			get { return strFeesName; }
			set { strFeesName = value; }
		}
		public string FeesType
		{
			get { return strFeesType; }
			set { strFeesType = value; }
		}
        public int OutstandingMonth
        {
            get { return intOutstandingMonth; }
            set { intOutstandingMonth = value; }
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
        public string FeeAbbreviation
        {
            get { return strFeesAbbreviation; }
            set { strFeesAbbreviation = value; }
        }
        public int FeeGroupID
        {
            get { return intFeeGroupID; }
            set { intFeeGroupID = value; }
        }
        public int Priority
        {
            get { return intPriority; }
            set { intPriority = value; }
        }
		#endregion
	}
}


