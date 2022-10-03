using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.Bo
{
	public class LeaveTemplateBo
	{
		#region LeaveTemplate Class Properties
		
		public const string LEAVETEMPLATE_TABLE = "tbl_LeaveTemplate";
		public const string LEAVETEMPLATE_LEAVETEMPLATEID = "LeaveTemplateID";
public const string LEAVETEMPLATE_EMPLOYEEMID = "EmployeeMID";
public const string LEAVETEMPLATE_LEAVEID = "LeaveID";
public const string LEAVETEMPLATE_TOTAL = "Total";
public const string LEAVETEMPLATE_ACADEMICYEAR = "AcademicYear";
public const string LEAVETEMPLATE_ISDELETED = "IsDeleted";
public const string LEAVETEMPLATE_CREATEDBY = "CreatedBy";
public const string LEAVETEMPLATE_CREATEDDATE = "CreatedDate";
public const string LEAVETEMPLATE_LASTMODIFIEDBY = "LastModifiedBy";
public const string LEAVETEMPLATE_LASTMODIFIEDDATE = "LastModifiedDate";

			
		
		private int intLeaveTemplateID = 0;
		private int intEmployeeMID = 0;
		private int intLeaveID = 0;
		private string strTotal = string.Empty;
	    private string strAcademicYear = string.Empty;
		private int intIsDeleted = 0;
		private int intCreatedBy = 0;
		private string strCreatedDate = string.Empty;
		private int intLastModifiedBy = 0;
		private string strLastModifiedDate = string.Empty;

		#endregion
		
		 #region ---Properties---
		public int LeaveTemplateID
		{
			get { return intLeaveTemplateID;}
			set { intLeaveTemplateID = value;}
		}
		public int EmployeeMID
		{
			get { return intEmployeeMID;}
			set { intEmployeeMID = value;}
		}
		public int LeaveID
		{
			get { return intLeaveID;}
			set { intLeaveID = value;}
		}
		public string Total
		{
			get { return strTotal;}
			set { strTotal = value;}
		}
        public string AcademicYear
        {
            get { return strAcademicYear; }
            set { strAcademicYear = value; }
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
