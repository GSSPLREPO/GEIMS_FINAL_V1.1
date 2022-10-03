using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.Bo
{
	public class LeaveApplyBo
	{
		#region LeaveApply Class Properties
		
		//Leave
		public const string LEAVEAPPLY_TABLE = "tbl_LeaveApply";
		public const string LEAVEAPPLY_LEAVEAPPLYLID = "LeaveApplylID";
		public const string LEAVEAPPLY_EMPLOYEEMID = "EmployeeMID";
		public const string LEAVEAPPLY_FROMDATE = "FromDate";
		public const string LEAVEAPPLY_TODATE = "ToDate";
		public const string LEAVEAPPLY_REASON = "Reason";
		public const string LEAVEAPPLY_APPROVEDBY = "ApprovedBy";
		public const string LEAVEAPPLY_APPROVEDDATE = "ApprovedDate";
		public const string LEAVEAPPLY_TOTALDAYS = "TotalDays";
		public const string LEAVEAPPLY_REPORTINGTO = "ReportingTo"; //Arpit Shah [27-11-2021]
		public const string LEAVEAPPLY_ISDELETED = "IsDeleted";
		public const string LEAVEAPPLY_CREATEDBY = "CreatedBy";
		public const string LEAVEAPPLY_CREATEDDATE = "CreatedDate";
		public const string LEAVEAPPLY_LASTMODIFIEDBY = "LastModifiedBy";
		public const string LEAVEAPPLY_LASTMODIFIEDDATE = "LastModifiedDate";

		//Duty Leave
		public const string DUTYLEAVEAPPLY_TABLE = "tbl_DutyLeaveApply";
		public const string DUTYLEAVEAPPLY_LEAVEAPPLYLID = "DutyLeaveApplylID";
		public const string DUTYLEAVEAPPLY_EMPLOYEEMID = "EmployeeMID";
		public const string DUTYLEAVEAPPLY_FROMDATE = "FromDate";
		public const string DUTYLEAVEAPPLY_TODATE = "ToDate";
		public const string DUTYLEAVEAPPLY_REASON = "Reason";
		public const string DUTYLEAVEAPPLY_APPROVEDBY = "ApprovedBy";
		public const string DUTYLEAVEAPPLY_APPROVEDDATE = "ApprovedDate";
		public const string DUTYLEAVEAPPLY_TOTALDAYS = "TotalDays";
		public const string DUTYLEAVEAPPLY_ISDELETED = "IsDeleted";
		public const string DUTYLEAVEAPPLY_CREATEDBY = "CreatedBy";
		public const string DUTYLEAVEAPPLY_CREATEDDATE = "CreatedDate";
		public const string DUTYLEAVEAPPLY_LASTMODIFIEDBY = "LastModifiedBy";
		public const string DUTYLEAVEAPPLY_LASTMODIFIEDDATE = "LastModifiedDate";


		//Leave
		private int intLeaveApplylID = 0;
		private int intEmployeeMID = 0;
		private string strFromDate = string.Empty;
		private string strToDate = string.Empty;
		private string strReason = string.Empty;
		private int intApprovedBy = 0;
		private string strApprovedDate = string.Empty;
		private double dbTotalDays = 0.0;
		private int intReportingTo = 0; //Arpit Shah [27-11-2021]
		private int intIsDeleted = 0;
		private int intCreatedBy = 0;
		private string strCreatedDate = string.Empty;
		private int intLastModifiedBy = 0;
		private string strLastModifiedDate = string.Empty;

		//DutyLeave
		private int intDutyLeaveApplylID = 0;
		//private int intDutyEmployeeMID = 0;
		//private string strDutyFromDate = string.Empty;
		//private string strDutyToDate = string.Empty;
		//private string strDutyReason = string.Empty;
		//private int intDutyApprovedBy = 0;
		//private string strDutyApprovedDate = string.Empty;
		//private double dbDutyTotalDays = 0.0;
		//private int intDutyIsDeleted = 0;
		//private int intDutyCreatedBy = 0;
		//private string strDutyCreatedDate = string.Empty;
		//private int intDutyLastModifiedBy = 0;
		//private string strDutyLastModifiedDate = string.Empty;

		#endregion

		#region ---Properties---
		//Leave
		public int LeaveApplylID
		{
			get { return intLeaveApplylID;}
			set { intLeaveApplylID = value;}
		}
		public int EmployeeMID
		{
			get { return intEmployeeMID;}
			set { intEmployeeMID = value;}
		}
		public string FromDate
		{
			get { return strFromDate;}
			set { strFromDate = value;}
		}
		public string ToDate
		{
			get { return strToDate;}
			set { strToDate = value;}
		}
		public string Reason
		{
			get { return strReason;}
			set { strReason = value;}
		}
		public int ApprovedBy
		{
			get { return intApprovedBy;}
			set { intApprovedBy = value;}
		}
		public string ApprovedDate
		{
			get { return strApprovedDate;}
			set { strApprovedDate = value;}
		}
		public double TotalDays
		{
			get { return dbTotalDays;}
			set { dbTotalDays = value;}
		}
		public int ReportingTo
		{
			get { return intReportingTo; }
			set { intReportingTo = value; }
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

		//Duty Leave
		public int DutyLeaveApplylID
		{
			get { return intDutyLeaveApplylID; }
			set { intDutyLeaveApplylID = value; }
		}
		//public int DutyEmployeeMID
		//{
		//	get { return intDutyEmployeeMID; }
		//	set { intDutyEmployeeMID = value; }
		//}
		//public string DutyFromDate
		//{
		//	get { return strDutyFromDate; }
		//	set { strDutyFromDate = value; }
		//}
		//public string DutyToDate
		//{
		//	get { return strDutyToDate; }
		//	set { strDutyToDate = value; }
		//}
		//public string DutyReason
		//{
		//	get { return strDutyReason; }
		//	set { strDutyReason = value; }
		//}
		//public int DutyApprovedBy
		//{
		//	get { return intDutyApprovedBy; }
		//	set { intDutyApprovedBy = value; }
		//}
		//public string DutyApprovedDate
		//{
		//	get { return strDutyApprovedDate; }
		//	set { strDutyApprovedDate = value; }
		//}
		//public double DutyTotalDays
		//{
		//	get { return dbDutyTotalDays; }
		//	set { dbDutyTotalDays = value; }
		//}
		//public int DutyIsDeleted
		//{
		//	get { return intDutyIsDeleted; }
		//	set { intDutyIsDeleted = value; }
		//}
		//public int DutyCreatedBy
		//{
		//	get { return intDutyCreatedBy; }
		//	set { intDutyCreatedBy = value; }
		//}
		//public string DutyCreatedDate
		//{
		//	get { return strDutyCreatedDate; }
		//	set { strDutyCreatedDate = value; }
		//}
		//public int DutyLastModifiedBy
		//{
		//	get { return intDutyLastModifiedBy; }
		//	set { intDutyLastModifiedBy = value; }
		//}
		//public string DutyLastModifiedDate
		//{
		//	get { return strDutyLastModifiedDate; }
		//	set { strDutyLastModifiedDate = value; }
		//}

		#endregion
	}
}
