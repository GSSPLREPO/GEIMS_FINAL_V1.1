namespace GEIMS.BO
{
	public class DivisionTBO
	{
		#region DivisionT Class Properties

		public const string DIVISIONT_TABLE = "tbl_Division_T";
		public const string DIVISIONT_DIVISIONTID = "DivisionTID";
		public const string DIVISIONT_CLASSMID = "ClassMID";
		public const string DIVISIONT_DIVISIONNAME = "DivisionName";
		public const string DIVISIONT_LASTMODIFIEDUSERID = "LastModifiedUserID";
		public const string DIVISIONT_LASTMODIFIEDDATE = "LastModifiedDate";
		public const string DIVISIONT_ISDELETED = "IsDeleted";



		private int intDivisionTID = 0;
		private int intClassMID = 0;
		private string strDivisionName = string.Empty;
		private int intLastModifiedUserID = 0;
		private string strLastModifiedDate = string.Empty;
		private int intIsDeleted = 0;

		#endregion

		#region ---Properties---
		public int DivisionTID
		{
			get { return intDivisionTID; }
			set { intDivisionTID = value; }
		}
		public int ClassMID
		{
			get { return intClassMID; }
			set { intClassMID = value; }
		}
		public string DivisionName
		{
			get { return strDivisionName; }
			set { strDivisionName = value; }
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
		public int IsDeleted
		{
			get { return intIsDeleted; }
			set { intIsDeleted = value; }
		}

		#endregion
	}
}


