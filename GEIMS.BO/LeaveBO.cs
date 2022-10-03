using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.Bo
{
	public class LeaveBo
	{
		#region Leave Class Properties
		
		public const string LEAVE_TABLE = "tbl_Leave_M";
		public const string LEAVE_LEAVEID = "LeaveID";
		public const string LEAVE_TRUSTMID = "TrustMID";
		public const string LEAVE_SCHOOLMID = "SchoolMID";
		public const string LEAVE_LEAVECODE = "LeaveCode";
		public const string LEAVE_LEAVENAME = "LeaveName";
		public const string LEAVE_LEAVEDESCRIPTION = "LeaveDescription";
        public const string LEAVE_IsDeduction = "IsDeduction";
		public const string LEAVE_ISCARRYFORWARD = "IsCarryForward";
		public const string LEAVE_YEAR = "Year";
		public const string LEAVE_ISDELETED = "IsDeleted";
		public const string LEAVE_CREATEDUSERID = "CreatedUserID";
		public const string LEAVE_CREATEDDATE = "CreatedDate";
		public const string LEAVE_LASTMODIFIEDUSERID = "LastModifiedUserID";
		public const string LEAVE_LASTMODIFIEDDATE = "LastModifiedDate";
		
			
		
		private int intLeaveID = 0;
		private int intTrustMID = 0;
		private int intSchoolMID = 0;
		private string strLeaveCode = string.Empty;
		private string strLeaveName = string.Empty;
		private string strLeaveDescription = string.Empty;
        private int intIsDeduction = 0;
		private int intIsCarryForward = 0;
        private string strYear = string.Empty;
		private int intIsDeleted = 0;
		private int intCreatedUserID = 0;
		private string strCreatedDate = string.Empty;
		private int intLastModifiedUserID = 0;
		private string strLastModifiedDate = string.Empty;

		#endregion
		
		 #region ---Properties---
		public int LeaveID
		{
			get { return intLeaveID;}
			set { intLeaveID = value;}
		}
		public int TrustMID
		{
			get { return intTrustMID;}
			set { intTrustMID = value;}
		}
		public int SchoolMID
		{
			get { return intSchoolMID;}
			set { intSchoolMID = value;}
		}
		public string LeaveCode
		{
			get { return strLeaveCode;}
			set { strLeaveCode = value;}
		}
		public string LeaveName
		{
			get { return strLeaveName;}
			set { strLeaveName = value;}
		}
		public string LeaveDescription
		{
			get { return strLeaveDescription;}
			set { strLeaveDescription = value;}
		}
		public int IsDeduction
		{
            get { return intIsDeduction; }
            set { intIsDeduction = value; }
		}
		public int IsCarryForward
		{
			get { return intIsCarryForward;}
			set { intIsCarryForward = value;}
		}
		public string Year
		{
            get { return strYear; }
            set { strYear = value; }
		}
		public int IsDeleted
		{
			get { return intIsDeleted;}
			set { intIsDeleted = value;}
		}
		public int CreatedUserID
		{
			get { return intCreatedUserID;}
			set { intCreatedUserID = value;}
		}
		public string CreatedDate
		{
			get { return strCreatedDate;}
			set { strCreatedDate = value;}
		}
		public int LastModifiedUserID
		{
			get { return intLastModifiedUserID;}
			set { intLastModifiedUserID = value;}
		}
		public string LastModifiedDate
		{
			get { return strLastModifiedDate;}
			set { strLastModifiedDate = value;}
		}

		#endregion
	}
}
