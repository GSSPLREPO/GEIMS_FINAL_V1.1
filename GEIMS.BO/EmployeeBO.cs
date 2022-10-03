namespace GEIMS.BO
{
	public class EmployeeBO
	{
		#region Employee Class Properties

		public const string EMPLOYEE_TABLE = "tbl_Employee_M";
		public const string EMPLOYEE_EMPLOYEEID = "EmployeeID";
		public const string EMPLOYEE_EMPLOYEENAME = "EmployeeName";
		public const string EMPLOYEE_EMPLOYEECODE = "EmployeeCode";
		public const string EMPLOYEE_TRUSTMID = "TrustMID";
		public const string EMPLOYEE_SCHOOLMID = "SchoolMID";
		public const string EMPLOYEE_EMPLOYEEROLEID = "EmployeeRoleID";
		public const string EMPLOYEE_DESIGNATIONID = "DesignationID";
		public const string EMPLOYEE_ADDRESS = "Address";
		public const string EMPLOYEE_CONTACTNO = "ContactNo";
		public const string EMPLOYEE_MOBILENO = "MobileNo";
		public const string EMPLOYEE_EMAIL = "Email";
		public const string EMPLOYEE_USERNAME = "UserName";
		public const string EMPLOYEE_PASSWORD = "Password";
		public const string EMPLOYEE_JOINDATE = "JoinDate";
		public const string EMPLOYEE_BIRTHDATE = "BirthDate";
		public const string EMPLOYEE_MARRIAGEDATE = "MarriageDate";
		public const string EMPLOYEE_RESIGNEDDATE = "ResignedDate";
		public const string EMPLOYEE_RESIGNREASON = "ResignReason";
		public const string EMPLOYEE_ISRESIGNED = "IsResigned";
		public const string EMPLOYEE_ISDELETED = "IsDeleted";
		public const string EMPLOYEE_LASTMODIFIEDUSERID = "LastModifiedUserID";
		public const string EMPLOYEE_LASTMODIFIEDDATE = "LastModifiedDate";



		private int intEmployeeID = 0;
		private string strEmployeeName = string.Empty;
		private string strEmployeeCode = string.Empty;
		private int intTrustMID = 0;
		private int intSchoolMID = 0;
		private int intEmployeeRoleID = 0;
		private int intDesignationID = 0;
		private string strAddress = string.Empty;
		private string strContactNo = string.Empty;
		private string strMobileNo = string.Empty;
		private string strEmail = string.Empty;
		private string strUserName = string.Empty;
		private string strPassword = string.Empty;
		private string strJoinDate = string.Empty;
		private string strBirthDate = string.Empty;
		private string strMarriageDate = string.Empty;
		private string strResignedDate = string.Empty;
		private string strResignReason = string.Empty;
		private int intIsResigned = 0;
		private int intIsDeleted = 0;
		private int intLastModifiedUserID = 0;
		private string strLastModifiedDate = string.Empty;

		#endregion

		#region ---Properties---
		public int EmployeeID
		{
			get { return intEmployeeID; }
			set { intEmployeeID = value; }
		}
		public string EmployeeName
		{
			get { return strEmployeeName; }
			set { strEmployeeName = value; }
		}
		public string EmployeeCode
		{
			get { return strEmployeeCode; }
			set { strEmployeeCode = value; }
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
		public int EmployeeRoleID
		{
			get { return intEmployeeRoleID; }
			set { intEmployeeRoleID = value; }
		}
		public int DesignationID
		{
			get { return intDesignationID; }
			set { intDesignationID = value; }
		}

		
		public string Address
		{
			get { return strAddress; }
			set { strAddress = value; }
		}
		public string ContactNo
		{
			get { return strContactNo; }
			set { strContactNo = value; }
		}
		public string MobileNo
		{
			get { return strMobileNo; }
			set { strMobileNo = value; }
		}
		public string Email
		{
			get { return strEmail; }
			set { strEmail = value; }
		}
		public string UserName
		{
			get { return strUserName; }
			set { strUserName = value; }
		}
		public string Password
		{
			get { return strPassword; }
			set { strPassword = value; }
		}
		public string JoinDate
		{
			get { return strJoinDate; }
			set { strJoinDate = value; }
		}
		public string BirthDate
		{
			get { return strBirthDate; }
			set { strBirthDate = value; }
		}
		public string MarriageDate
		{
			get { return strMarriageDate; }
			set { strMarriageDate = value; }
		}
		public string ResignedDate
		{
			get { return strResignedDate; }
			set { strResignedDate = value; }
		}
		public string ResignReason
		{
			get { return strResignReason; }
			set { strResignReason = value; }
		}
		public int IsResigned
		{
			get { return intIsResigned; }
			set { intIsResigned = value; }
		}
		public int IsDeleted
		{
			get { return intIsDeleted; }
			set { intIsDeleted = value; }
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

		#endregion
	}
}


