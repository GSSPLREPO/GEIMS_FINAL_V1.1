using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
	public class ReceiptPaymentBO
	{
		#region ReceiptPayment Class Properties
		
		public const string RECEIPTPAYMENT_TABLE = "tbl_ReceiptPaymentDetail_M";
		public const string RECEIPTPAYMENT_RECEIPTPAYMENTID = "ReceiptPaymentID";
		public const string RECEIPTPAYMENT_TRUSTMID = "TrustMID";
		public const string RECEIPTPAYMENT_SCHOOLMID = "SchoolMID";
		public const string RECEIPTPAYMENT_SECTIONMID = "SectionMID";
		public const string RECEIPTPAYMENT_BUDGETCATEGORYMID = "BudgetCategoryMID";
		public const string RECEIPTPAYMENT_BUDGETHEADINGMID = "BudgetHeadingMID";
		public const string RECEIPTPAYMENT_BUDGETSUBHEADINGMID = "BudgetSubHeadingMID";
		public const string RECEIPTPAYMENT_RECEIPTPAYMENTNO = "ReceiptPaymentNo";
		public const string RECEIPTPAYMENT_RECEIPTPAYMENTCODE = "ReceiptPaymentCode";
		public const string RECEIPTPAYMENT_RECEIPTPAYMENTDATE = "ReceiptPaymentDate";
		public const string RECEIPTPAYMENT_YEAR = "Year";
		public const string RECEIPTPAYMENT_TRANSACTIONMODE = "TransactionMode";
		public const string RECEIPTPAYMENT_TRANSACTIONTYPE = "TransactionType";
		public const string RECEIPTPAYMENT_GENERALLEDGER = "GeneralLedger";
		public const string RECEIPTPAYMENT_LEDGERID = "LedgerID";
		public const string RECEIPTPAYMENT_AMOUNT = "Amount";
		public const string RECEIPTPAYMENT_BANKNAME = "BankName";
		public const string RECEIPTPAYMENT_BRANCHNAME = "BranchName";
		public const string RECEIPTPAYMENT_CHEQUENO = "ChequeNo";
		public const string RECEIPTPAYMENT_NARRATION = "Narration";
		public const string RECEIPTPAYMENT_CREATEDDATE = "CreatedDate";
		public const string RECEIPTPAYMENT_CREATEDBY = "CreatedBy";
		public const string RECEIPTPAYMENT_ISDELETED = "IsDeleted";
		public const string RECEIPTPAYMENT_LASTMODIFIDEUSERID = "LastModifideUserID";
		public const string RECEIPTPAYMENT_LASTMODIFIDEDATE = "LastModifideDate";
					
		private int intReceiptPaymentID = 0;
		private int intTrustMID = 0;
		private int intSchoolMID = 0;
		private int intSectionMID = 0;
		private int intBudgetCategoryMID = 0;
		private int intBudgetHeadingMID = 0;
		private int intBudgetSubHeadingMID = 0;
		private int intReceiptPaymentNo = 0;
		private string strReceiptPaymentCode = string.Empty;
		private string strReceiptPaymentDate = string.Empty;
		private int intYear = 0;
		private string strTransactionMode = string.Empty;
		private string strTransactionType = string.Empty;
		private int intGeneralLedger = 0;
		private int intLedgerID = 0;
		private double dbAmount = 0.0;
		private string strBankName = string.Empty;
		private string strBranchName = string.Empty;
		private int intChequeNo = 0;
	    private string strNarration = string.Empty;
		private string strCreatedDate = string.Empty;
		private int intCreatedBy = 0;
		private int intIsDeleted = 0;
		private int intLastModifideUserID = 0;
		private string strLastModifideDate = string.Empty;

		#endregion
		
		 #region ---Properties---
		public int ReceiptPaymentID
		{
			get { return intReceiptPaymentID;}
			set { intReceiptPaymentID = value;}
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
		public int ReceiptPaymentNo
		{
			get { return intReceiptPaymentNo;}
			set { intReceiptPaymentNo = value;}
		}
		public string ReceiptPaymentCode
		{
			get { return strReceiptPaymentCode;}
			set { strReceiptPaymentCode = value;}
		}
		public string ReceiptPaymentDate
		{
			get { return strReceiptPaymentDate;}
			set { strReceiptPaymentDate = value;}
		}
		public int Year
		{
			get { return intYear;}
			set { intYear = value;}
		}
		public string TransactionMode
		{
			get { return strTransactionMode;}
			set { strTransactionMode = value;}
		}
		public string TransactionType
		{
			get { return strTransactionType;}
			set { strTransactionType = value;}
		}
		public int GeneralLedger
		{
			get { return intGeneralLedger;}
			set { intGeneralLedger = value;}
		}
		public int LedgerID
		{
			get { return intLedgerID;}
			set { intLedgerID = value;}
		}
		public double Amount
		{
			get { return dbAmount;}
			set { dbAmount = value;}
		}
		public string BankName
		{
			get { return strBankName;}
			set { strBankName = value;}
		}
		public string BranchName
		{
			get { return strBranchName;}
			set { strBranchName = value;}
		}
		public int ChequeNo
		{
			get { return intChequeNo;}
			set { intChequeNo = value;}
		}
        public string Narration
        {
            get { return strNarration; }
            set { strNarration = value; }
        }
		public string CreatedDate
		{
			get { return strCreatedDate;}
			set { strCreatedDate = value;}
		}
		public int CreatedBy
		{
			get { return intCreatedBy;}
			set { intCreatedBy = value;}
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
		#endregion
	}
}
