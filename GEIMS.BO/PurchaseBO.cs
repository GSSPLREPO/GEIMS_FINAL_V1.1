using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
	public class PurchaseBO
	{
		#region Purchase Class Properties
		
		public const string PURCHASE_TABLE = "tbl_Purchase_M";
		public const string PURCHASE_PURCHASEID = "PurchaseID";
		public const string PURCHASE_TRUSTMID = "TrustMID";
		public const string PURCHASE_SCHOOLMID = "SchoolMID";
		public const string PURCHASE_VENDORID = "VendorID";
		public const string PURCHASE_PONO = "PONO";
		public const string PURCHASE_PODATE = "PODate";
		public const string PURCHASE_BILLNO = "BillNO";
        public const string PURCHASE_BILLDATE = "BillDate";
        public const string PURCHASET_VOUCHARNO = "VoucharNO";
        public const string PURCHASET_VOUCHARDATE = "VoucharDate";
		public const string PURCHASE_PAYMENTTYPE = "PaymentType";
		public const string PURCHASE_PAYMENTMODE = "PaymentMode";
        public const string PURCHASE_YEAR = "Year";
		public const string PURCHASE_VAT = "VAT";
		public const string PURCHASE_ADDVAT = "AddVAT";
		public const string PURCHASE_CST = "CST";
		public const string PURCHASE_TOTALAMOUNT = "TotalAmount";
		public const string PURCHASE_DISCOUNT = "Discount";
		public const string PURCHASE_ISDELETED = "IsDeleted";
		public const string PURCHASE_CREATEDDATE = "CreatedDate";
		public const string PURCHASE_CREATEDUSERID = "CreatedUserID";
		public const string PURCHASE_LASTMODIFIEDDATE = "LastModifiedDate";
		public const string PURCHASE_LASTMODIFIEDUSERID = "LastModifiedUserID";
		
		private int intPurchaseID = 0;
		private int intTrustMID = 0;
		private int intSchoolMID = 0;
		private int intVendorID = 0;
		private string strPONO = string.Empty;
		private string strPODate = string.Empty;
		private string strBillNO = string.Empty;
        private string strBillDate = string.Empty;
        private string strVoucharNO = string.Empty;
        private string strVoucharDate = string.Empty;
		private string strPaymentType = string.Empty;
		private string strPaymentMode = string.Empty;
        private int intYear = 0;
		private double dbVAT = 0.0;
		private double dbAddVAT = 0.0;
		private double dbCST = 0.0;
		private double dbTotalAmount = 0.0;
		private double dbDiscount = 0.0;
		private int intIsDeleted = 0;
		private string strCreatedDate = string.Empty;
		private int intCreatedUserID = 0;
		private string strLastModifiedDate = string.Empty;
		private int intLastModifiedUserID = 0;

		#endregion
		
		 #region ---Properties---
		public int PurchaseID
		{
			get { return intPurchaseID;}
			set { intPurchaseID = value;}
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
		public int VendorID
		{
			get { return intVendorID;}
			set { intVendorID = value;}
		}
		public string PONO
		{
			get { return strPONO;}
			set { strPONO = value;}
		}
		public string PODate
		{
			get { return strPODate;}
			set { strPODate = value;}
		}
		public string BillNO
		{
			get { return strBillNO;}
			set { strBillNO = value;}
		}
		public string BillDate
		{
			get { return strBillDate;}
			set { strBillDate = value;}
        }
        public string VoucharNO
        {
            get { return strVoucharNO; }
            set { strVoucharNO = value; }
        }
        public string VoucharDate
        {
            get { return strVoucharDate; }
            set { strVoucharDate = value; }
        }
		public string PaymentType
		{
			get { return strPaymentType;}
			set { strPaymentType = value;}
		}
		public string PaymentMode
		{
			get { return strPaymentMode;}
			set { strPaymentMode = value;}
		}
        public int Year
        {
            get { return intYear; }
            set { intYear = value; }
        }
		public double VAT
		{
			get { return dbVAT;}
			set { dbVAT = value;}
		}
		public double AddVAT
		{
			get { return dbAddVAT;}
			set { dbAddVAT = value;}
		}
		public double CST
		{
			get { return dbCST;}
			set { dbCST = value;}
		}
		public double TotalAmount
		{
			get { return dbTotalAmount;}
			set { dbTotalAmount = value;}
		}
		public double Discount
		{
			get { return dbDiscount;}
			set { dbDiscount = value;}
		}
		public int IsDeleted
		{
			get { return intIsDeleted;}
			set { intIsDeleted = value;}
		}
		public string CreatedDate
		{
			get { return strCreatedDate;}
			set { strCreatedDate = value;}
		}
		public int CreatedUserID
		{
			get { return intCreatedUserID;}
			set { intCreatedUserID = value;}
		}
		public string LastModifiedDate
		{
			get { return strLastModifiedDate;}
			set { strLastModifiedDate = value;}
		}
		public int LastModifiedUserID
		{
			get { return intLastModifiedUserID;}
			set { intLastModifiedUserID = value;}
		}

		#endregion
	}
}
		