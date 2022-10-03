using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
    public class BankRecoDetailsBO
    {
		#region GeneralLedger Class Properties

		public const string GENERALLEDGER_TABLE = "tbl_GeneralLedger_M";
		public const string GENERALLEDGER_LEDGERID = "LedgerID";
		public const string GENERALLEDGER_TRUSTMID = "TrustMID";
		public const string GENERALLEDGER_SCHOOLMID = "SchoolMID";
		public const string GENERALLEDGER_ACCOUNTNAME = "AccountName";
		public const string GENERALLEDGER_ACCOUNTGROUPID = "AccountGroupID";
		public const string GENERALLEDGER_OPENINGBALANCE = "OpeningBalance";
		public const string GENERALLEDGER_BALANCETYPE = "BalanceType";
		public const string GENERALLEDGER_DESCRIPTION = "Description";
		public const string GENERALLEDGER_CREATEDDATE = "CreatedDate";
		public const string GENERALLEDGER_CREATEDUSERID = "CreatedUserID";
		public const string GENERALLEDGER_ISDELETED = "IsDeleted";
		public const string GENERALLEDGER_LASTMODIFIDEUSERID = "LastModifideUserID";
		public const string GENERALLEDGER_LASTMODIFIDEDATE = "LastModifideDate";



		private int intLedgerID = 0;
		private int intTrustMID = 0;
		private int intSchoolMID = 0;
		private string strAccountName = string.Empty;
		private int intAccountGroupID = 0;
		private double dbOpeningBalance = 0.0;
		private string strBalanceType = string.Empty;
		private string strDescription = string.Empty;
		private string strCreatedDate = string.Empty;
		private int intCreatedUserID = 0;
		private int intIsDeleted = 0;
		private int intLastModifideUserID = 0;
		private string strLastModifideDate = string.Empty;

		#endregion

		#region ---Properties---
		public int LedgerID
		{
			get { return intLedgerID; }
			set { intLedgerID = value; }
		}
		public int TrustMID
		{
			get { return intTrustMID; }
			set { intTrustMID = value; }
		}
		public int SchoolMID
		{
			get { return intSchoolMID; }
			set { intSchoolMID = value; }
		}
		public string AccountName
		{
			get { return strAccountName; }
			set { strAccountName = value; }
		}
		public int AccountGroupID
		{
			get { return intAccountGroupID; }
			set { intAccountGroupID = value; }
		}
		public double OpeningBalance
		{
			get { return dbOpeningBalance; }
			set { dbOpeningBalance = value; }
		}
		public string BalanceType
		{
			get { return strBalanceType; }
			set { strBalanceType = value; }
		}
		public string Description
		{
			get { return strDescription; }
			set { strDescription = value; }
		}
		public string CreatedDate
		{
			get { return strCreatedDate; }
			set { strCreatedDate = value; }
		}
		public int CreatedUserID
		{
			get { return intCreatedUserID; }
			set { intCreatedUserID = value; }
		}
		public int IsDeleted
		{
			get { return intIsDeleted; }
			set { intIsDeleted = value; }
		}
		public int LastModifideUserID
		{
			get { return intLastModifideUserID; }
			set { intLastModifideUserID = value; }
		}
		public string LastModifideDate
		{
			get { return strLastModifideDate; }
			set { strLastModifideDate = value; }
		}

		#endregion
	}
}
