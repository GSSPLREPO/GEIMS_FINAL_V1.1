using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
	public class DesignationBO
	{
		#region Designation Class Properties
		
		public const string DESIGNATION_TABLE = "tbl_Designation_M";
		public const string DESIGNATION_DESIGNATIONID = "DesignationID";
		public const string DESIGNATION_DESIGNATIONNAMEENG = "DesignationNameENG";
		public const string DESIGNATION_DESIGNATIONNAMEGUJ = "DesignationNameGUJ";
		public const string DESIGNATION_DESCRIPTION = "Description";
		public const string DESIGNATION_FULLDAYWORKINGHOURS = "FullDayWorkingHours";
		public const string DESIGNATION_ISDELETED = "IsDeleted";
		public const string DESIGNATION_CREATEDUSERID = "CreatedUserID";
		public const string DESIGNATION_CREATEDDATE = "CreatedDate";
		public const string DESIGNATION_LASTMODIFIEDUSERID = "LastModifiedUserID";
		public const string DESIGNATION_LASTMODIFIEDDATE = "LastModifiedDate";

			
		
		private int intDesignationID = 0;
		private string strDesignationNameENG = string.Empty;
		private string strDesignationNameGUJ = string.Empty;
		private string strDescription = string.Empty;
		private string intFullDayWorkingHours = string.Empty;
		private int intIsDeleted = 0;
		private int intCreatedUserID = 0;
		private string strCreatedDate = string.Empty;
		private int intLastModifiedUserID = 0;
		private string strLastModifiedDate = string.Empty;

		#endregion
		
		 #region ---Properties---
		public int DesignationID
		{
			get { return intDesignationID;}
			set { intDesignationID = value;}
		}
		public string DesignationNameENG
		{
			get { return strDesignationNameENG;}
			set { strDesignationNameENG = value;}
		}
		public string DesignationNameGUJ
		{
			get { return strDesignationNameGUJ;}
			set { strDesignationNameGUJ = value;}
		}
		public string Description
		{
			get { return strDescription;}
			set { strDescription = value;}
		}
		public string FullDayWorkingHours
		{
			get { return intFullDayWorkingHours; }
			set { intFullDayWorkingHours = value; }
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
