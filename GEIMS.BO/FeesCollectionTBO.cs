namespace GEIMS.BO
{
	public class FeesCollectionTBO
	{
		#region FeesCollectionT Class Properties

		public const string FEESCOLLECTIONT_TABLE = "tbl_FeesCollection_T";
		public const string FEESCOLLECTIONT_FEESCOLLECTIONTID = "FeesCollectionTID";
		//public const string FEESCOLLECTIONT_BUDGETSUBHEADINGMID = "BudgetSubHeadingMID";
		public const string FEESCOLLECTIONT_FEESCOLLECTIONMID = "FeesCollectionMID";
		public const string FEESCOLLECTIONT_CLASSWISEFEESTEMPLATETID = "ClassWiseFeesTemplateTID";
		//public const string FEESCOLLECTIONT_STUDENTFEESTEMPLATETID = "StudentFeesTemplateTID";
		public const string FEESCOLLECTIONT_FEESAMOUNT = "FeesAmount";
		public const string FEESCOLLECTIONT_DISCOUNT = "Discount";
        public const string FEESCOLLECTIONT_REMAININGAMOUNT = "RemainingAmount";
		public const string FEESCOLLECTIONT_LASTMODIFIEDUSERID = "LastModifiedUserID";
		public const string FEESCOLLECTIONT_LASTMODIFIEDDATE = "LastModifiedDate";
		public const string FEESCOLLECTIONT_ISDELETED = "Isdeleted";

		private int intFeesCollectionTID = 0;
		private int intFeesCollectionMID = 0;
		//private int intBudgetSubHeadingMID = 0;
		private int intClassWiseFeesTemplateTID = 0;
		//private int intStudentFeesTemplateTID = 0;
		private double dbFeesAmount = 0.0;
		private double dbDiscount = 0.0;
        private double dbRemainingAmount = 0.0;
		private int intLastModifiedUserID = 0;
		private string strLastModifiedDate = string.Empty;
		private int intIsdeleted = 0;

		#endregion

		#region ---Properties---
		public int FeesCollectionTID
		{
			get { return intFeesCollectionTID; }
			set { intFeesCollectionTID = value; }
		}
		public int FeesCollectionMID
		{
			get { return intFeesCollectionMID; }
			set { intFeesCollectionMID = value; }
		}
		//public int BudgetSubHeadingMID
		//{
		//	get { return intBudgetSubHeadingMID; }
		//	set { intBudgetSubHeadingMID = value; }
		//}
		public int ClassWiseFeesTemplateTID
		{
			get { return intClassWiseFeesTemplateTID; }
			set { intClassWiseFeesTemplateTID = value; }
		}
        //public int StudentFeesTemplateTID
        //{
        //    get { return intStudentFeesTemplateTID; }
        //    set { intStudentFeesTemplateTID = value; }
        //}
		public double FeesAmount
		{
			get { return dbFeesAmount; }
			set { dbFeesAmount = value; }
		}
		public double Discount
		{
			get { return dbDiscount; }
			set { dbDiscount = value; }
		}
        public double RemainingAmount
        {
            get { return dbRemainingAmount; }
            set { dbRemainingAmount = value; }
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


