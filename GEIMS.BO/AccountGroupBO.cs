using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
	public class AccountGroupBO
	{
		#region AccountGroup Class Properties
		
		public const string ACCOUNTGROUP_TABLE = "tbl_AccountGroup_M";
		public const string ACCOUNTGROUP_ACCOUNTGROUPID = "AccountGroupID";
        public const string ACCOUNTGROUP_TRUSTMID = "TrustMID";
        public const string ACCOUNTGROUP_SCHOOLMID = "SchoolMID";
        public const string ACCOUNTGROUP_ACCOUNTGROUPNAME = "AccountGroupName";
        public const string ACCOUNTGROUP_ACCOUNTGROUPDEFAULTNATURE = "AccountGroupDefaultNature";
        public const string ACCOUNTGROUP_GROUPNATURE = "GroupNature";
        public const string ACCOUNTGROUP_SUBGROUPID = "SubGroupID";
        public const string ACCOUNTGROUP_SUBGROUPOF = "SubGroupOf";
        public const string ACCOUNTGROUP_DESCRIPTION = "Description";
        public const string ACCOUNTGROUP_ISDELETED = "IsDeleted";
        public const string ACCOUNTGROUP_CREATEDDATE = "CreatedDate";
        public const string ACCOUNTGROUP_CREATEDUSERID = "CreatedUserID";
        public const string ACCOUNTGROUP_LASTMODIFIDEDATE = "LastModifideDate";
        public const string ACCOUNTGROUP_LASTMODIFIDEUSERID = "LastModifideUserID";

			
		
		private int intAccountGroupID = 0;
		private int intTrustMID = 0;
		private int intSchoolMID = 0;
		private string strAccountGroupName = string.Empty;
		private string strAccountGroupDefaultNature = string.Empty;
		private string strGroupNature = string.Empty;
		private int intSubGroupID = 0;
		private string strSubGroupOf = string.Empty;
		private string strDescription = string.Empty;
		private int intIsDeleted = 0;
        private string strCreatedDate = string.Empty;
        private int intCreatedUserID = 0;
		private string strLastModifideDate = string.Empty;
		private int intLastModifideUserID = 0;

		#endregion
		
		#region ---Properties---
		public int AccountGroupID
		{
			get { return intAccountGroupID;}
			set { intAccountGroupID = value;}
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
		public string AccountGroupName
		{
			get { return strAccountGroupName;}
			set { strAccountGroupName = value;}
		}
		public string AccountGroupDefaultNature
		{
			get { return strAccountGroupDefaultNature;}
			set { strAccountGroupDefaultNature = value;}
		}
		public string GroupNature
		{
			get { return strGroupNature;}
			set { strGroupNature = value;}
		}
		public int SubGroupID
		{
			get { return intSubGroupID;}
			set { intSubGroupID = value;}
		}
		public string SubGroupOf
		{
			get { return strSubGroupOf;}
			set { strSubGroupOf = value;}
		}
		public string Description
		{
			get { return strDescription;}
			set { strDescription = value;}
		}
		public int IsDeleted
		{
			get { return intIsDeleted;}
			set { intIsDeleted = value;}
		}
        public string CreatedDate
        {
            get { return strCreatedDate; }
            set { strCreatedDate = value; }
        }
        public int CreatedUserID
        {
            get { return intCreatedUserID; }
            set { intCreatedUserID = value; }
        }
		public string LastModifideDate
		{
			get { return strLastModifideDate;}
			set { strLastModifideDate = value;}
		}
		public int LastModifideUserID
		{
			get { return intLastModifideUserID;}
			set { intLastModifideUserID = value;}
		}
		#endregion
	}
}
