namespace GEIMS.BO
{
	public class BankAssociationBO
	{
		#region BankAssociation Class Properties

		public const string BANKASSOCIATION_TABLE = "tbl_BankAssociation_M";
		public const string BANKASSOCIATION_BANKASSOCIATIONMID = "BankAssociationMID";
		public const string BANKASSOCIATION_TRUSTMID = "TrustMID";
		public const string BANKASSOCIATION_SCHOOLMID = "SchoolMID";
		public const string BANKASSOCIATION_BANKNAME = "BankName";
		public const string BANKASSOCIATION_BRANCHNAME = "BranchName";
		public const string BANKASSOCIATION_ACCOUNTNAMEENG = "AccountNameEng";
		public const string BANKASSOCIATION_ACCOUNTNAMEGUJ = "AccountNameGuj";
		public const string BANKASSOCIATION_ACCOUNTNO = "AccountNo";
		public const string BANKASSOCIATION_ACCOUNTTYPE = "AccountType";
		public const string BANKASSOCIATION_IFSCCODE = "IfscCode";
		public const string BANKASSOCIATION_PANNO = "PanNO";
		public const string BANKASSOCIATION_LASTMODIFIEDUSERID = "LastModifiedUserID";
		public const string BANKASSOCIATION_LASTMODIFIEDDATE = "LastModifiedDate";
		public const string BANKASSOCIATION_ISDELETED = "IsDeleted";



		private int intBankAssociationMID = 0;
		private int intTrustMID = 0;
		private int intSchoolMID = 0;
		private string strBankName = string.Empty;
		private string strBranchName = string.Empty;
		private string strAccountNameEng = string.Empty;
		private string strAccountNameGuj = string.Empty;
		private string strAccountNo = string.Empty;
		private string strAccountType = string.Empty;
		private string strIfscCode = string.Empty;
		private string strPanNO = string.Empty;
		private int intLastModifiedUserID = 0;
		private string strLastModifiedDate = string.Empty;
		private int intIsDeleted = 0;

		#endregion

		#region ---Properties---
		public int BankAssociationMID
		{
			get { return intBankAssociationMID; }
			set { intBankAssociationMID = value; }
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
		public string BankName
		{
			get { return strBankName; }
			set { strBankName = value; }
		}
		public string BranchName
		{
			get { return strBranchName; }
			set { strBranchName = value; }
		}
		public string AccountNameEng
		{
			get { return strAccountNameEng; }
			set { strAccountNameEng = value; }
		}
		public string AccountNameGuj
		{
			get { return strAccountNameGuj; }
			set { strAccountNameGuj = value; }
		}
		public string AccountNo
		{
			get { return strAccountNo; }
			set { strAccountNo = value; }
		}
		public string AccountType
		{
			get { return strAccountType; }
			set { strAccountType = value; }
		}
		public string IfscCode
		{
			get { return strIfscCode; }
			set { strIfscCode = value; }
		}
		public string PanNO
		{
			get { return strPanNO; }
			set { strPanNO = value; }
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


