namespace GEIMS.BO
{
	public class FeesSeperationTBO
	{
		#region FeesSeperationT Class Properties

		public const string FEESSEPERATIONT_TABLE = "tbl_FeesSeperation_T";
		public const string FEESSEPERATIONT_FEESSEPERATIONTID = "FeesSeperationTID";
		public const string FEESSEPERATIONT_FEESCATEGORYMID = "FeesCategoryMID";
		public const string FEESSEPERATIONT_FEESNAME = "FeesName";
		public const string FEESSEPERATIONT_FEESAMOUNT = "FeesAmount";
		public const string FEESSEPERATIONT_DISPLAYNAME = "DisplayName";
		public const string FEESSEPERATIONT_LASTMODIFIEDUSERID = "LastModifiedUserID";
		public const string FEESSEPERATIONT_LASTMODIFIEDDATE = "LastModifiedDate";
		public const string FEESSEPERATIONT_ISDELETED = "Isdeleted";



		private int intFeesSeperationTID = 0;
		private int intFeesCategoryMID = 0;
		private string strFeesName = string.Empty;
		private double dbFeesAmount = 0.0;
		private string strDisplayName = string.Empty;
		private int intLastModifiedUserID = 0;
		private string strLastModifiedDate = string.Empty;
		private int intIsdeleted = 0;

		#endregion

		#region ---Properties---
		public int FeesSeperationTID
		{
			get { return intFeesSeperationTID; }
			set { intFeesSeperationTID = value; }
		}
		public int FeesCategoryMID
		{
			get { return intFeesCategoryMID; }
			set { intFeesCategoryMID = value; }
		}
		public string FeesName
		{
			get { return strFeesName; }
			set { strFeesName = value; }
		}
		public double FeesAmount
		{
			get { return dbFeesAmount; }
			set { dbFeesAmount = value; }
		}
		public string DisplayName
		{
			get { return strDisplayName; }
			set { strDisplayName = value; }
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


