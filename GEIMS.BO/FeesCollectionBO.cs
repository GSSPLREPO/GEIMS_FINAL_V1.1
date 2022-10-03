using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;


namespace GEIMS.BO
{
    public class FeesCollectionBO
    {
        #region FeesCollection Class Properties

        public const string FEESCOLLECTION_TABLE = "tbl_FeesCollection_M";
        public const string FEESCOLLECTION_FEESCOLLECTIONMID = "FeesCollectionMID";
        public const string FEESCOLLECTION_RECEIPTNO = "ReceiptNo";
        public const string FEESCOLLECTION_SCHOOLMID = "SchoolMID";
        public const string FEESCOLLECTION_TRUSTMID = "TrustMID";
        public const string FEESCOLLECTION_STUDENTMID = "StudentMID";
        public const string FEESCOLLECTION_CLASSMID = "ClassMID";
        public const string FEESCOLLECTION_DIVISIONTID = "DivisionTID";
        public const string FEESCOLLECTION_FEESTOBEPAID = "FeesToBePaid";
        public const string FEESCOLLECTION_AMOUNTPAID = "AmountPaid";
        public const string FEESCOLLECTION_DATE = "Date";
        public const string FEESCOLLECTION_ACADEMICYEAR = "AcademicYear";
        public const string FEESCOLLECTION_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string FEESCOLLECTION_LASTMODIFIEDDATE = "LastModifiedDate";
        public const string FEESCOLLECTION_ISDELETED = "Isdeleted";
        public const string FEESCOLLECTION_CANCELLATIONREASON = "CancellationReason";
        public const string FEESCOLLECTION_CLASSWISEFEETEMPLATETIDS = "ClassWiseTemplateIDs";
        public const string FEESCOLLECTION_MODEOFPAYMENT = "ModeOfPayment";
        public const string FEESCOLLECTION_ACCOUNTNUMBER = "AccountNumber";
        public const string FEESCOLLECTION_CHEQUENO = "ChequeNo";
        public const string FEESCOLLECTION_BANKNAME = "BankName";
        public const string FEESCOLLECTION_BRANCHNAME = "BranchName";
        public const string FEESCOLLECTION_IFSCODE = "IFSCODE";
        public const string DATEOF_DEPOSIT = "DateOfDeposit";
        public const string RFEESCOLLECTION_CHEQUENO = "RChequeNo";
        public const string RFEESCOLLECTION_BANKNAME = "RBankName";
        public const string RFEESCOLLECTION_BRANCHNAME = "RBranchName";
        public const string DATEIN_BANKSTATEMENT = "DateInBankStatement";

        private int intFeesCollectionMID = 0;
        private string strReceiptNo = string.Empty;
        private int intSchoolMID = 0;
        private int intTrustMID = 0;
        private int intStudentMID = 0;
        private int intClassMID = 0;
        private int intDivisionTID = 0;
        private double dbFeesToBePaid = 0.0;
        private double dbAmountPaid = 0.0;
        private string strDate = string.Empty;
        private string strAcademicYear = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;
        private int intIsdeleted = 0;
        private string strCancellationReason = string.Empty;
        private string strClassWiseTemplateIDs = string.Empty;
        private int intModeOfPayment = 0;
        private string strAccountNumber = string.Empty;
        private string strChequeNo = string.Empty;
        private string strBankName = string.Empty;
        private string strBranchName = string.Empty;
        private string strIFSCODE = string.Empty;
        private DateTime strDateOfDeposit = DateTime.UtcNow;
        private string strRChequeNo = string.Empty;
        private string strRBankName = string.Empty;
        private string strRBranchName = string.Empty;
        private DateTime strDateInBankStatement = DateTime.UtcNow;
        #endregion

        #region ---Properties---
        public int FeesCollectionMID
        {
            get { return intFeesCollectionMID; }
            set { intFeesCollectionMID = value; }
        }
        public string ReceiptNo
        {
            get { return strReceiptNo; }
            set { strReceiptNo = value; }
        }
        public int SchoolMID
        {
            get { return intSchoolMID; }
            set { intSchoolMID = value; }
        }
        public int TrustMID
        {
            get { return intTrustMID; }
            set { intTrustMID = value; }
        }
        public int StudentMID
        {
            get { return intStudentMID; }
            set { intStudentMID = value; }
        }
        public int ClassMID
        {
            get { return intClassMID; }
            set { intClassMID = value; }
        }
        public int DivisionTID
        {
            get { return intDivisionTID; }
            set { intDivisionTID = value; }
        }
        public double FeesToBePaid
        {
            get { return dbFeesToBePaid; }
            set { dbFeesToBePaid = value; }
        }
        public double AmountPaid
        {
            get { return dbAmountPaid; }
            set { dbAmountPaid = value; }
        }
        public string Date
        {
            get { return strDate; }
            set { strDate = value; }
        }
        public string AcademicYear
        {
            get { return strAcademicYear; }
            set { strAcademicYear = value; }
        }
        public int LastModifiedUserID
        {
            get { return intLastModifiedUserID; }
            set { intLastModifiedUserID = value; }
        }
        public string LastModifiedDate
        {
            get { return strLastModifiedDate; }
            set { strLastModifiedDate = value; }
        }
        public int Isdeleted
        {
            get { return intIsdeleted; }
            set { intIsdeleted = value; }
        }
        public string CancellationReason
        {
            get { return strCancellationReason; }
            set { strCancellationReason = value; }
        }
        public string ClassWiseTemplateIDs
        {
            get { return strClassWiseTemplateIDs; }
            set { strClassWiseTemplateIDs = value; }
        }

        public int ModeOfPayment
        {
            get { return intModeOfPayment; }
            set { intModeOfPayment = value; }
        }

        public string AccountNumber
        {
            get { return strAccountNumber; }
            set { strAccountNumber = value; }
        }
        public string ChequeNo
        {
            get { return strChequeNo; }
            set { strChequeNo = value; }
        }
        public string BankName
        {
            get { return strBankName; }
            set { strBankName = value; }
        }
        public string BranchName
        {
            get { return strBranchName; }
            set { strBranchName = value; }
        }
        public string IFSCODE
        {
            get { return strIFSCODE; }
            set { strIFSCODE = value; }
        }
        public DateTime DateOfDeposit
        {
            get { return strDateOfDeposit; }
            set { strDateOfDeposit = value; }
        }
        public string RChequeNo
        {
            get { return strRChequeNo; }
            set { strRChequeNo = value; }
        }
        public string RBankName
        {
            get { return strRBankName; }
            set { strRBankName = value; }
        }
        public string RBranchName
        {
            get { return strRBranchName; }
            set { strRBranchName = value; }
        }
        public DateTime DateInBankStatement
        {
            get { return strDateInBankStatement; }
            set { strDateInBankStatement = value; }
        }
        #endregion
    }
}


