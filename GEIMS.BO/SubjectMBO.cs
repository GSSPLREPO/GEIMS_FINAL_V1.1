using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
	public class SubjectMBO
	{
		#region SubjectM Class Properties
		
		public const string SUBJECTM_TABLE = "tbl_Subject_M";
		public const string SUBJECTM_SUBJECTMID = "SubjectMID";
public const string SUBJECTM_NAMEENG = "NameEng";
public const string SUBJECTM_NAMEGUJ = "NameGuj";
public const string SUBJECTM_DESCRIPTION = "Description";
public const string SUBJECTM_SCHOOLMID = "SchoolMID";
public const string SUBJECTM_ISDELETED = "IsDeleted";
public const string SUBJECTM_CREATEDUSERID = "CreatedUserID";
public const string SUBJECTM_CREATEDDATE = "CreatedDate";
public const string SUBJECTM_LASTMODIFIEDUSERID = "LastModifiedUserID";
public const string SUBJECTM_LASTMODIFIEDDATE = "LastModifiedDate";

			
		
		private int intSubjectMID = 0;
		private string strNameEng = string.Empty;
		private string strNameGuj = string.Empty;
		private string strDescription = string.Empty;
		private int intSchoolMID = 0;
		private int intIsDeleted = 0;
		private int intCreatedUserID = 0;
		private string strCreatedDate = string.Empty;
		private int intLastModifiedUserID = 0;
		private string strLastModifiedDate = string.Empty;

		#endregion
		
		 #region ---Properties---
		public int SubjectMID
		{
			get { return intSubjectMID;}
			set { intSubjectMID = value;}
		}
		public string NameEng
		{
			get { return strNameEng;}
			set { strNameEng = value;}
		}
		public string NameGuj
		{
			get { return strNameGuj;}
			set { strNameGuj = value;}
		}
		public string Description
		{
			get { return strDescription;}
			set { strDescription = value;}
		}
		public int SchoolMID
		{
			get { return intSchoolMID;}
			set { intSchoolMID = value;}
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
