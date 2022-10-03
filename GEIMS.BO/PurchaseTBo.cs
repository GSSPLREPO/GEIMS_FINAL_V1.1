using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
    public class PurchaseTBO
    {
        #region PurchaseT Class Properties

        public const string PURCHASET_TABLE = "tbl_Purchase_T";
        public const string PURCHASET_PURCHASETID = "PurchaseTID";
        public const string PURCHASET_PURCHASEID = "PurchaseID";
        public const string PURCHASET_UOMID = "UOMID";
        public const string PURCHASET_MATERIALID = "MaterialID";
        public const string PURCHASET_QUANTITY = "Quantity";
        public const string PURCHASET_RATE = "Rate";
        public const string PURCHASET_TOTALAMOUNT = "TotalAmount";
        public const string PURCHASET_DELIVERYNOTE = "DeliveryNote";
        public const string PURCHASET_ISDELETED = "IsDeleted";
        public const string PURCHASET_CREATEDDATE = "CreatedDate";
        public const string PURCHASET_CREATEDUSERID = "CreatedUserID";
        public const string PURCHASET_LASTMODIFIEDDATE = "LastModifiedDate";
        public const string PURCHASET_LASTMODIFIEDUSERID = "LastModifiedUserID";

        private int intPurchaseTID = 0;
        private int intPurchaseID = 0;
        private int intUOMID = 0;
        private int intMaterialID = 0;
        private int intQuantity = 0;
        private double dbRate = 0.0;
        private double dbTotalAmount = 0.0;
        private string strDeliveryNote = string.Empty;
        private int intIsDeleted = 0;
        private string strCreatedDate = string.Empty;
        private int intCreatedUserID = 0;
        private string strLastModifiedDate = string.Empty;
        private int intLastModifiedUserID = 0;

        #endregion

        #region ---Properties---
        public int PurchaseTID
        {
            get { return intPurchaseTID; }
            set { intPurchaseTID = value; }
        }
        public int PurchaseID
        {
            get { return intPurchaseID; }
            set { intPurchaseID = value; }
        }
        public int UOMID
        {
            get { return intUOMID; }
            set { intUOMID = value; }
        }
        public int MaterialID
        {
            get { return intMaterialID; }
            set { intMaterialID = value; }
        }
        public int Quantity
        {
            get { return intQuantity; }
            set { intQuantity = value; }
        }
        public double Rate
        {
            get { return dbRate; }
            set { dbRate = value; }
        }
        public double TotalAmount
        {
            get { return dbTotalAmount; }
            set { dbTotalAmount = value; }
        }
        public string DeliveryNote
        {
            get { return strDeliveryNote; }
            set { strDeliveryNote = value; }
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