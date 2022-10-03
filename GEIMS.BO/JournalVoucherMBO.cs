using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
	public class JournalVoucherMBO
	{
		#region JournalVoucherM Class Properties
		
		public const string JOURNALVOUCHERM_TABLE = "tbl_JournalVoucher_M";
		public const string JOURNALVOUCHERM_JOURNALID = "JournalID";
        public const string JOURNALVOUCHERM_TRUSTMID = "TrustMID";
        public const string JOURNALVOUCHERM_SCHOOLMID = "SchoolMID";
		public const string JOURNALVOUCHERM_SECTIONMID = "SectionMID";
		public const string JOURNALVOUCHERM_BUDGETCATEGORYMID = "BudgetCategoryMID";
		public const string JOURNALVOUCHERM_BUDGETHEADINGMID = "BudgetHeadingMID";
		public const string JOURNALVOUCHERM_BUDGETSUBHEADINGMID = "BudgetSubHeadingMID";
		public const string JOURNALVOUCHERM_LEDGERID = "LedgerID";
        public const string JOURNALVOUCHERM_VOUCHERNO = "VoucherNo";
        public const string JOURNALVOUCHERM_VOUCHERCODE = "VoucherCode";
        public const string JOURNALVOUCHERM_VOUCHERDATE = "VoucherDate";
        public const string JOURNALVOUCHERM_AMOUNT = "Amount";
        public const string JOURNALVOUCHERM_TRANSACTIONTYPE = "TransactionType";
        public const string JOURNALVOUCHERM_OPERATIONTYPE = "OperationType";
        public const string JOURNALVOUCHERM_CHEQUENUMBER = "ChequeNumber";
        public const string JOURNALVOUCHERM_DESCRIPTION = "Description";
        public const string JOURNALVOUCHERM_CREATEDDATE = "CreatedDate";
        public const string JOURNALVOUCHERM_CREATEDBY = "CreatedBy";
        public const string JOURNALVOUCHERM_ISDELETED = "IsDeleted";
        public const string JOURNALVOUCHERM_LASTMODIFIDEUSERID = "LastModifideUserID";
        public const string JOURNALVOUCHERM_LASTMODIFIDEDATE = "LastModifideDate";
        public const string JOURNALVOUCHERM_YEAR = "Year";
		
		private int intJournalID = 0;
		private int intTrustMID = 0;
		private int intSchoolMID = 0;
		private int intSectionMID = 0;
		private int intBudgetCategoryMID = 0;
		private int intBudgetHeadingMID = 0;
		private int intBudgetSubHeadingMID = 0;
		private int intLedgerID = 0;
		private int intVoucherNo = 0;
		private string strVoucherCode = string.Empty;
		private string strVoucherDate = string.Empty;
		private double dbAmount = 0.0;
		private string strTransactionType = string.Empty;
		private string strOperationType = string.Empty;
		private int intChequeNumber = 0;
		private string strDescription = string.Empty;
		private string strCreatedDate = string.Empty;
		private int intCreatedBy = 0;
		private int intIsDeleted = 0;
		private int intLastModifideUserID = 0;
		private string strLastModifideDate = string.Empty;
        private int intYear = 0;

		#endregion
		
		 #region ---Properties---
		public int JournalID
		{
			get { return intJournalID;}
			set { intJournalID = value;}
		}
		public int TrustMID
		{
			get { return intTrustMID;}
			set { intTrustMID = value;}
		}
		public int SchoolMID
		{
			get { return intSchoolMID;}
			set { intSchoolMID = value;}
		}
		public int SectionMID
		{
			get { return intSectionMID; }
			set { intSectionMID = value; }
		}
		public int BudgetCategoryMID
		{
			get { return intBudgetCategoryMID; }
			set { intBudgetCategoryMID = value; }
		}
		public int BudgetHeadingMID
		{
			get { return intBudgetHeadingMID; }
			set { intBudgetHeadingMID = value; }
		}
		public int BudgetSubHeadingMID
		{
			get { return intBudgetSubHeadingMID; }
			set { intBudgetSubHeadingMID = value; }
		}
		public int LedgerID
		{
			get { return intLedgerID;}
			set { intLedgerID = value;}
		}
		public int VoucherNo
		{
			get { return intVoucherNo;}
			set { intVoucherNo = value;}
		}
		public string VoucherCode
		{
            get { return strVoucherCode; }
            set { strVoucherCode = value; }
		}
		public string VoucherDate
		{
			get { return strVoucherDate;}
			set { strVoucherDate = value;}
		}
		public double Amount
		{
			get { return dbAmount;}
			set { dbAmount = value;}
		}
		public string TransactionType
		{
			get { return strTransactionType;}
			set { strTransactionType = value;}
		}
		public string OperationType
		{
			get { return strOperationType;}
			set { strOperationType = value;}
		}
		public int ChequeNumber
		{
			get { return intChequeNumber;}
			set { intChequeNumber = value;}
		}
		public string Description
		{
			get { return strDescription;}
			set { strDescription = value;}
		}
		public string CreatedDate
		{
			get { return strCreatedDate;}
			set { strCreatedDate = value;}
		}
		public int CreatedBy
		{
			get { return intCreatedBy;}
            set { intCreatedBy = value; }
		}
		public int IsDeleted
		{
			get { return intIsDeleted;}
			set { intIsDeleted = value;}
		}
		public int LastModifideUserID
		{
			get { return intLastModifideUserID;}
			set { intLastModifideUserID = value;}
		}
		public string LastModifideDate
		{
			get { return strLastModifideDate;}
			set { strLastModifideDate = value;}
		}
        public int Year
        {
            get { return intYear; }
            set { intYear = value; }
        }
		#endregion
	}
}
