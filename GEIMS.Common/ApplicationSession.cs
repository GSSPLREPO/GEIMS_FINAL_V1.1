using System.Web.SessionState;

namespace GEIMS.Common
{
	public class ApplicationSession
	{
		private static HttpSessionState mvarSession;

		public static void Init(HttpSessionState Session)
		{
			mvarSession = Session;
		}

		#region Constant declaration of the session variable

		public const string USERNAME = "UserName";
		public const string USERID = "UserID";
		public const string TRUSTID = "TrustID";
	    public const string TRUSTNAME = "TrustName";
		public const string SCHOOLID = "SchoolID";
		public const string SCHOOLNAME = "SchoolName";
		public const string ROLEID = "RoleID";
		public const string EMPCODE = "EmployeeCode"; //Arpit Shah
		public const string REPORTINGTO = "ReporitngTo";  //28-11-2021 Arpit Shah
        public const string ACCOUNTFROMDATE = "AccountFromDate";
        public const string ACCOUNTTODATE = "AccountToDate";
        public const string HASACCESSACCOUNTUSERID = "HasAccessAccountUserID";
        public const string FINANCIALYEAR = "FinancialYear";
        public const string ISPANEL = "IsPanel";

		public static void ClearAllSessions()
		{
			mvarSession.Remove(USERNAME);
		}
		#endregion
	}
}
