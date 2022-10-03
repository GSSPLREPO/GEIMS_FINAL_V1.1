using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
	public class RoleBO
	{
		#region Role Class Properties
		
		public const string ROLE_TABLE = "tbl_Role_M";
		public const string ROLE_ROLEID = "RoleID";
		public const string ROLE_ROLENAME = "RoleName";
		public const string ROLE_DESCRIPTION = "Description";
		public const string ROLE_ISDELETED = "IsDeleted";
		public const string ROLE_CREATEDUSERID = "CreatedUserID";
		public const string ROLE_CREATEDDATE = "CreatedDate";
		public const string ROLE_LASTMODIFIEDUSERID = "LastModifiedUserID";
		public const string ROLE_LASTMODIFIEDDATE = "LastModifiedDate";
		
			
		
		private int intRoleID = 0;
		private string strRoleName = string.Empty;
		private string strDescription = string.Empty;
		private int intIsDeleted = 0;
		private int intCreatedUserID = 0;
		private string strCreatedDate = string.Empty;
		private int intLastModifiedUserID = 0;
		private string strLastModifiedDate = string.Empty;

		#endregion
		
		 #region ---Properties---
		public int RoleID
		{
			get { return intRoleID;}
			set { intRoleID = value;}
		}
		public string RoleName
		{
			get { return strRoleName;}
			set { strRoleName = value;}
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
