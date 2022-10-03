namespace GEIMS.BO
{
	public class SerialNoBo
	{
		#region SerialNo Class Properties
		
		public const string SerialnoTable = "tbl_SerialNoInit_M";
		public const string SerialnoId = "Id";
		public const string SerialnoTrustmid = "TrustMID";
		public const string SerialnoSchoolmid = "SchoolMID";
		public const string SerialnoStartno = "StartNo";
		public const string SerialnoYear = "Year";
		public const string SerialnoEntrytype = "EntryType";
		public const string SerialnoCreatedby = "CreatedBy";
		public const string SerialnoCreateddate = "CreatedDate";
		public const string SerialnoIsdeleted = "IsDeleted";
		public const string SerialnoLastmodifidedate = "LastModifideDate";
		public const string SerialnoLastmodifideuserid = "LastModifideUserID";

	    private string _strEntryType = string.Empty;
	    private string _strCreatedDate = string.Empty;
	    private string _strLastModifideDate = string.Empty;
		private int _intLastModifideUserId;

		#endregion
		
		 #region ---Properties---

	    public int Id { get; set; }

	    public int TrustMID { get; set; }

	    public int SchoolMID { get; set; }

	    public int StartNo { get; set; }

	    public int Year { get; set; }

	    public string EntryType
		{
			get { return _strEntryType;}
			set { _strEntryType = value;}
		}

	    public int CreatedBy { get; set; }

	    public string CreatedDate
		{
			get { return _strCreatedDate;}
			set { _strCreatedDate = value;}
		}

	    public int IsDeleted { get; set; }

	    public string LastModifideDate
		{
			get { return _strLastModifideDate;}
			set { _strLastModifideDate = value;}
		}
		public int LastModifideUserID
		{
			get { return _intLastModifideUserId;}
			set { _intLastModifideUserId = value;}
		}

		#endregion
	}
}
