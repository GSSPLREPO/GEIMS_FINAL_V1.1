using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
	public class PeriodBO
	{
		#region Period Class Properties
		
		public const string PERIOD_TABLE = "tbl_Period_M";
		public const string PERIOD_PERIODID = "PeriodID";
		public const string PERIOD_DAYNAME = "DayName";
		public const string PERIOD_DAYNO = "DayNo";
		public const string PERIOD_PERIODNO = "PeriodNo";
		public const string PERIOD_FROMTIME = "FromTime";
		public const string PERIOD_TOTIME = "ToTime";
		public const string PERIOD_CLASSMID = "ClassMID";
		public const string PERIOD_SCHOOLMID = "SchoolMID";
		public const string PERIOD_ISDELETED = "IsDeleted";
		public const string PERIOD_CREATEDUSERID = "CreatedUserID";
		public const string PERIOD_CREATEDDATE = "CreatedDate";
		public const string PERIOD_LASTMODIFIEDUSERID = "LastModifiedUserID";
		public const string PERIOD_LASTMODIFIEDDATE = "LastModifiedDate";
		
			
		
		private int intPeriodID = 0;
		private int intDayNo = 0;
		private string strDayName = string.Empty;
		private int intPeriodNo = 0;
		private string strFromTime = string.Empty;
		private string strToTime = string.Empty;
		private int intClassMID = 0;
		private int intSchoolMID = 0;
		private int intIsDeleted = 0;
		private int intCreatedUserID = 0;
		private string strCreatedDate = string.Empty;
		private int intLastModifiedUserID = 0;
		private string strLastModifiedDate = string.Empty;

		#endregion
		
		 #region ---Properties---
		public int PeriodID
		{
			get { return intPeriodID;}
			set { intPeriodID = value;}
		}
		public int DayNo
		{
			get { return intDayNo;}
			set { intDayNo = value;}
		}
		public string DayName
		{
			get { return strDayName;}
			set { strDayName = value;}
		}
		public int PeriodNo
		{
			get { return intPeriodNo;}
			set { intPeriodNo = value;}
		}
		public string FromTime
		{
			get { return strFromTime;}
			set { strFromTime = value;}
		}
		public string ToTime
		{
			get { return strToTime;}
			set { strToTime = value;}
		}
		public int ClassMID
		{
			get { return intClassMID;}
			set { intClassMID = value;}
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
