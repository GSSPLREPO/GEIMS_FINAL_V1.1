namespace GEIMS.BO
{
	public class SectionTBO
	{
		#region SectionT Class Properties

		public const string SECTIONT_TABLE = "tbl_Section_T";
		public const string SECTIONT_SECTIONTID = "SectionTID";
		public const string SECTIONT_SECTIONMID = "SectionMID";
		public const string SECTIONT_SCHOOLMID = "SchoolMID";
		public const string SECTIONT_MEDIUMMID = "MediumMID";
		public const string SECTIONT_LASTMODIFIEDUSERID = "LastModifiedUserID";
		public const string SECTIONT_LASTMODIFIEDDATE = "LastModifiedDate";
		public const string SECTIONT_ISDELETED = "IsDeleted";



		private int intSectionTID = 0;
		private int intSectionMID = 0;
		private int intSchoolMID = 0;
		private int intMediumMID = 0;
		private int intLastModifiedUserID = 0;
		private string strLastModifiedDate = string.Empty;
		private int intIsDeleted = 0;

		#endregion

		#region ---Properties---
		public int SectionTID
		{
			get { return intSectionTID; }
			set { intSectionTID = value; }
		}
		public int SectionMID
		{
			get { return intSectionMID; }
			set { intSectionMID = value; }
		}
		public int SchoolMID
		{
			get { return intSchoolMID; }
			set { intSchoolMID = value; }
		}
		public int MediumMID
		{
			get { return intMediumMID; }
			set { intMediumMID = value; }
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


