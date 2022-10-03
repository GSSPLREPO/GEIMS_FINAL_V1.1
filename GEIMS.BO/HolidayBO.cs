using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
	public class HolidayBo
	{
		#region Holiday Class Properties
		
		public const string HOLIDAY_TABLE = "tbl_Holiday_M";
		public const string HOLIDAY_HOLIDAYID = "HolidayId";
		public const string HOLIDAY_NAME = "Name";
		public const string HOLIDAY_ACADEMICYEAR = "AcademicYear";
		public const string HOLIDAY_STARTDATE = "StartDate";
		public const string HOLIDAY_ENDDATE = "EndDate";
		public const string HOLIDAY_DESCRIPTION = "Description";
		public const string HOLIDAY_ISDELETED = "IsDeleted";
		public const string HOLIDAY_CREATEDBY = "CreatedBy";
		public const string HOLIDAY_CREATEDDATE = "CreatedDate";
		public const string HOLIDAY_LASTMODIFIEDBY = "LastModifiedBy";
		public const string HOLIDAY_LASTMODIFIEDDATE = "LastModifiedDate";
		
			
		
		private int intHolidayId = 0;
		private string strName = string.Empty;
		private string strAcademicYear = string.Empty;
		private string strStartDate = string.Empty;
        private string strEndDate = string.Empty;
		private string strDescription = string.Empty;
		private int intIsDeleted = 0;
		private int intCreatedBy = 0;
		private string strCreatedDate = string.Empty;
		private int intLastModifiedBy = 0;
		private string strLastModifiedDate = string.Empty;

		#endregion
		
		 #region ---Properties---
		public int HolidayId
		{
			get { return intHolidayId;}
			set { intHolidayId = value;}
		}
		public string Name
		{
			get { return strName;}
			set { strName = value;}
		}
		public string AcademicYear
		{
			get { return strAcademicYear;}
			set { strAcademicYear = value;}
		}
		public string StartDate
		{
			get { return strStartDate;}
			set { strStartDate = value;}
		}
        public string EndDate
        {
            get { return strEndDate; }
            set { strEndDate = value; }
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
		public int CreatedBy
		{
			get { return intCreatedBy;}
			set { intCreatedBy = value;}
		}
		public string CreatedDate
		{
			get { return strCreatedDate;}
			set { strCreatedDate = value;}
		}
		public int LastModifiedBy
		{
			get { return intLastModifiedBy;}
			set { intLastModifiedBy = value;}
		}
		public string LastModifiedDate
		{
			get { return strLastModifiedDate;}
			set { strLastModifiedDate = value;}
		}

		#endregion
	}
}
