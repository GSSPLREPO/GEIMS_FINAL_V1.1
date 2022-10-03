using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
	public class TimeTableBO
	{
		#region TimeTable Class Properties
		
		public const string TIMETABLE_TABLE = "tbl_TimeTable_T";
		public const string TIMETABLE_TIMETABLETID = "TimeTableTID";
public const string TIMETABLE_TIMETABLEMID = "TimeTableMID";
public const string TIMETABLE_PERIODID = "PeriodID";
public const string TIMETABLE_SUBJECTMID = "SubjectMID";
public const string TIMETABLE_EMPLOYEEMID = "EmployeeMID";

			
		
		private int intTimeTableTID = 0;
		private int intTimeTableMID = 0;
		private int intPeriodID = 0;
		private int intSubjectMID = 0;
		private int intEmployeeMID = 0;

		#endregion
		
		 #region ---Properties---
		public int TimeTableTID
		{
			get { return intTimeTableTID;}
			set { intTimeTableTID = value;}
		}
		public int TimeTableMID
		{
			get { return intTimeTableMID;}
			set { intTimeTableMID = value;}
		}
		public int PeriodID
		{
			get { return intPeriodID;}
			set { intPeriodID = value;}
		}
		public int SubjectMID
		{
			get { return intSubjectMID;}
			set { intSubjectMID = value;}
		}
		public int EmployeeMID
		{
			get { return intEmployeeMID;}
			set { intEmployeeMID = value;}
		}

		#endregion
	}
}
