namespace GEIMS.BO
{
	public class MediumBO
	{
		#region Medium Class Properties

		public const string MEDIUM_TABLE = "tbl_Medium_M";
		public const string MEDIUM_MEDIUMMID = "MediumMID";
		public const string MEDIUM_MEDIUMNAME = "MediumName";
		public const string MEDIUM_DESCRIPTION = "Description";
		public const string MEDIUM_LASTMODIFIEDUSERID = "LastModifiedUserID";
		public const string MEDIUM_LASTMODIFIEDDATE = "LastModifiedDate";
		public const string MEDIUM_ISDELETED = "IsDeleted";



		private int intMediumMID = 0;
		private string strMediumName = string.Empty;
		private string strDescription = string.Empty;
		private int intLastModifiedUserID = 0;
		private string strLastModifiedDate = string.Empty;
		private int intIsDeleted = 0;

		#endregion

		#region ---Properties---
		public int MediumMID
		{
			get { return intMediumMID; }
			set { intMediumMID = value; }
		}
		public string MediumName
		{
			get { return strMediumName; }
			set { strMediumName = value; }
		}
		public string Description
		{
			get { return strDescription; }
			set { strDescription = value; }
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


