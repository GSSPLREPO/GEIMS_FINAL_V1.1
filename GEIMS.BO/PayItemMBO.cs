using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
	public class PayItemMBO
	{
		#region PayItemM Class Properties
		
		public const string PAYITEMM_TABLE = "tbl_PayItem_M";
		public const string PAYITEMM_PAYITEMMID = "PayItemMID";
		public const string PAYITEMM_NAME = "Name";
		public const string PAYITEMM_DESCRIPTION = "Description";
		public const string PAYITEMM_TYPE = "Type";
		public const string PAYITEMM_ISDELETED = "IsDeleted";
		public const string PAYITEMM_CREATEDUSERID = "CreatedUserID";
		public const string PAYITEMM_CREATEDDATE = "CreatedDate";
		public const string PAYITEMM_LASTMODIFIEDUSERID = "LastModifiedUserID";
		public const string PAYITEMM_LASTMODIFIEDDATE = "LastModifiedDate";
				
		private int intPayItemMID = 0;
		private string strName = string.Empty;
		private string strDescription = string.Empty;
		private int intType = 0;
		private int intIsDeleted = 0;
		private int intCreatedUserID = 0;
		private string strCreatedDate = string.Empty;
		private int intLastModifiedUserID = 0;
		private string strLastModifiedDate = string.Empty;

		#endregion
		
		 #region ---Properties---
		public int PayItemMID
		{
			get { return intPayItemMID;}
			set { intPayItemMID = value;}
		}
		public string Name
		{
			get { return strName;}
			set { strName = value;}
		}
		public string Description
		{
			get { return strDescription;}
			set { strDescription = value;}
		}
		public int Type
		{
			get { return intType;}
			set { intType = value;}
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
