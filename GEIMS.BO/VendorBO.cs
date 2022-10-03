using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
    public class VendorBO
    {
        #region Vendor Class Properties

        public const string VENDOR_TABLE = "tbl_Vendor_M";
        public const string VENDOR_VENDORID = "VendorID";
        public const string VENDOR_TRUSTMID = "TrustMID";
        public const string VENDOR_SCHOOLMID = "SchoolMID";
        public const string VENDOR_VENDORNAME = "VendorName";
        public const string VENDOR_ADDRESS = "Address";
        public const string VENDOR_TELEPHONENO = "TelephoneNo";
        public const string VENDOR_MOBILENO = "MobileNo";
        public const string VENDOR_FAX = "Fax";
        public const string VENDOR_EMAILID = "EmailID";
        public const string VENDOR_TINGST = "TINGST";
        public const string VENDOR_TINCST = "TINCST";
        public const string VENDOR_BANKNAME = "BankName";
        public const string VENDOR_ACCOUNTNO = "AccountNo";
        public const string VENDOR_ACCOUNTNAME = "AccountName";
        public const string VENDOR_IFSCCODE = "IFSCCode";
        public const string VENDOR_PANNO = "PANNO";
        public const string VENDOR_TAXREGNO = "TaxRegNo";
        public const string VENDOR_ISDELETED = "IsDeleted";
        public const string VENDOR_CREATEDDATE = "CreatedDate";
        public const string VENDOR_CREATEDUSERID = "CreatedUserID";
        public const string VENDOR_LASTMODIFIEDDATE = "LastModifiedDate";
        public const string VENDOR_LASTMODIFIEDUSERID = "LastModifiedUserID";



        private int intVendorID = 0;
        private int intTrustMID = 0;
        private int intSchoolMID = 0;
        private string strVendorName = string.Empty;
        private string strAddress = string.Empty;
        private string strTelephoneNo = string.Empty;
        private string strMobileNo = string.Empty;
        private string strFax = string.Empty;
        private string strEmailID = string.Empty;
        private string strTINGST = string.Empty;
        private string strTINCST = string.Empty;
        private string strBankName = string.Empty;
        private string strAccountNo = string.Empty;
        private string strAccountName = string.Empty;
        private string strIFSCCode = string.Empty;
        private string strPANNO = string.Empty;
        private string strTaxRegNo = string.Empty;
        private int intIsDeleted = 0;
        private string strCreatedDate = string.Empty;
        private int intCreatedUserID = 0;
        private string strLastModifiedDate = string.Empty;
        private int intLastModifiedUserID = 0;

        #endregion

        #region ---Properties---
        public int VendorID
        {
            get { return intVendorID; }
            set { intVendorID = value; }
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
        public string VendorName
        {
            get { return strVendorName; }
            set { strVendorName = value; }
        }
        public string Address
        {
            get { return strAddress; }
            set { strAddress = value; }
        }
        public string TelephoneNo
        {
            get { return strTelephoneNo; }
            set { strTelephoneNo = value; }
        }
        public string MobileNo
        {
            get { return strMobileNo; }
            set { strMobileNo = value; }
        }
        public string Fax
        {
            get { return strFax; }
            set { strFax = value; }
        }
        public string EmailID
        {
            get { return strEmailID; }
            set { strEmailID = value; }
        }
        public string TINGST
        {
            get { return strTINGST; }
            set { strTINGST = value; }
        }
        public string TINCST
        {
            get { return strTINCST; }
            set { strTINCST = value; }
        }
        public string BankName
        {
            get { return strBankName; }
            set { strBankName = value; }
        }
        public string AccountNo
        {
            get { return strAccountNo; }
            set { strAccountNo = value; }
        }
        public string AccountName
        {
            get { return strAccountName; }
            set { strAccountName = value; }
        }
        public string IFSCCode
        {
            get { return strIFSCCode; }
            set { strIFSCCode = value; }
        }
        public string PANNO
        {
            get { return strPANNO; }
            set { strPANNO = value; }
        }
        public string TaxRegNo
        {
            get { return strTaxRegNo; }
            set { strTaxRegNo = value; }
        }
        public int IsDeleted
        {
            get { return intIsDeleted; }
            set { intIsDeleted = value; }
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
        public string LastModifiedDate
        {
            get { return strLastModifiedDate; }
            set { strLastModifiedDate = value; }
        }
        public int LastModifiedUserID
        {
            get { return intLastModifiedUserID; }
            set { intLastModifiedUserID = value; }
        }

        #endregion
    }
}



