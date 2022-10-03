namespace GEIMS.BO
{
	public class ClassBO
	{
		#region Class Class Properties

		public const string CLASS_TABLE = "tbl_Class_M";
		public const string CLASS_CLASSMID = "ClassMID";
        public const string CLASS_SCHOOLMID = "SchoolMID";
		public const string CLASS_SECTIONTID = "SectionTID";
		public const string CLASS_CLASSNAME = "ClassName";
        public const string CLASS_NOOFPERIOD = "NoOfPeriod";
		public const string CLASS_APPROVALDATE = "ApprovalDate";
	    public const string CALSS_PRIORITY = "Priority";
		public const string CLASS_APPROVALNO = "ApprovalNo";
		public const string CLASS_LASTMODIFIEDUSERID = "LastModifiedUserID";
		public const string CLASS_LASTMODIFIEDDATE = "LastModifiedDate";
		public const string CLASS_ISDELETED = "IsDeleted";




		private int intClassMID = 0;
        private int intSchoolMID = 0;
		private int intSectionTID = 0;
		private string strClassName = string.Empty;
        private int intNoOfPeriod = 0;
		private string strApprovalDate = string.Empty;
		private string strApprovalNo = string.Empty;
	    private int intPriority = 0;
		private int intLastModifiedUserID = 0;
		private string strLastModifiedDate = string.Empty;
		private int intIsDeleted = 0;

		#endregion

		#region ---Properties---
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
		public int SectionTID
		{
			get { return intSectionTID; }
			set { intSectionTID = value; }
		}
        public string ClassName
		{
			get { return strClassName; }
			set { strClassName = value; }
		}
        public int NoOfPeriod
        {
            get { return intNoOfPeriod; }
            set { intNoOfPeriod = value; }
        }
		public string ApprovalDate
		{
			get { return strApprovalDate; }
			set { strApprovalDate = value; }
		}
		public string ApprovalNo
		{
			get { return strApprovalNo; }
			set { strApprovalNo = value; }
		}

	    public int Priority
	    {
	        get { return intPriority; }
            set { intPriority = value; }
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


