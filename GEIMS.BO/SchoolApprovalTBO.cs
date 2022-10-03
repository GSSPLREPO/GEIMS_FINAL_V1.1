namespace GEIMS.BO
{
	public class SchoolApprovalTBO
	{
		#region SchoolApprovalT Class Properties

		public const string SCHOOLAPPROVALT_TABLE = "SchoolApprovalT";
		public const string SCHOOLAPPROVALT_SCHOOLAPPROVALTID = "SchoolApprovalTID";
		public const string SCHOOLAPPROVALT_SCHOOLMID = "SchoolMID";
		public const string SCHOOLAPPROVALT_SECTIONAPPROVED = "SectionApproved";
		public const string SCHOOLAPPROVALT_CLASSAPPROVED = "ClassApproved";
		public const string SCHOOLAPPROVALT_DIVISIONAPPROVED = "DivisionApproved";
		public const string SCHOOLAPPROVALT_APPROVEDDATE = "ApprovedDate";
		public const string SCHOOLAPPROVALT_APPROVALNO = "ApprovalNo";
		public const string SCHOOLAPPROVALT_LASTMODIFIEDUSERID = "LastModifiedUserID";
		public const string SCHOOLAPPROVALT_LASTMODIFIEDDATE = "LastModifiedDate";
		public const string SCHOOLAPPROVALT_ISDELETED = "IsDeleted";



		private int intSchoolApprovalTID = 0;
		private int intSchoolMID = 0;
		private string strSectionApproved = string.Empty;
		private string strClassApproved = string.Empty;
		private string strDivisionApproved = string.Empty;
		private string strApprovedDate = string.Empty;
		private string strApprovalNo = string.Empty;
		private int intLastModifiedUserID = 0;
		private string strLastModifiedDate = string.Empty;
		private int intIsDeleted = 0;

		#endregion

		#region ---Properties---
		public int SchoolApprovalTID
		{
			get { return intSchoolApprovalTID; }
			set { intSchoolApprovalTID = value; }
		}
		public int SchoolMID
		{
			get { return intSchoolMID; }
			set { intSchoolMID = value; }
		}
		public string SectionApproved
		{
			get { return strSectionApproved; }
			set { strSectionApproved = value; }
		}
		public string ClassApproved
		{
			get { return strClassApproved; }
			set { strClassApproved = value; }
		}
		public string DivisionApproved
		{
			get { return strDivisionApproved; }
			set { strDivisionApproved = value; }
		}
		public string ApprovedDate
		{
			get { return strApprovedDate; }
			set { strApprovedDate = value; }
		}
		public string ApprovalNo
		{
			get { return strApprovalNo; }
			set { strApprovalNo = value; }
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


