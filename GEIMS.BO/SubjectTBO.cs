using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
	public class SubjectTBO
	{
		#region SubjectT Class Properties
		
		public const string SUBJECTT_TABLE = "tbl_Subject_T";
		public const string SUBJECTT_SUBJECTTID = "SubjectTID";
public const string SUBJECTT_SCHOOLMID = "SchoolMID";
public const string SUBJECTT_CLASSMID = "ClassMID";
public const string SUBJECTT_DIVISIONTID = "DivisionTID";
public const string SUBJECTT_SUBJECTMID = "SubjectMID";
public const string SUBJECTT_EMPLOYEEMID = "EmployeeMID";

			
		
		private int intSubjectTID = 0;
		private int intSchoolMID = 0;
		private int intClassMID = 0;
		private int intDivisionTID = 0;
		private int intSubjectMID = 0;
		private int intEmployeeMID = 0;

		#endregion
		
		 #region ---Properties---
		public int SubjectTID
		{
			get { return intSubjectTID;}
			set { intSubjectTID = value;}
		}
		public int SchoolMID
		{
			get { return intSchoolMID;}
			set { intSchoolMID = value;}
		}
		public int ClassMID
		{
			get { return intClassMID;}
			set { intClassMID = value;}
		}
		public int DivisionTID
		{
			get { return intDivisionTID;}
			set { intDivisionTID = value;}
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
