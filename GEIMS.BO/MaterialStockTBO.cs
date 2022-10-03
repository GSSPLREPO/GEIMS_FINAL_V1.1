using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
    public class MaterialStockTBO
    {
        #region StockT Class Properties

        public const string STOCKT_TABLE = "tbl_MaterialStock_T";
        public const string STOCKT_STOCKTID = "StockTID";
        public const string STOCKT_MATERIALID = "MaterialID";
        public const string STOCKT_QUANTITY = "Quantity";
        public const string STOCKT_ISOPENINGSTOCK = "IsOpeningStock";
        public const string STOCKT_STOCKYEAR = "StockYear";
        public const string STOCKT_MATERIALINDATE = "MaterialInDate";
        public const string STOCKT_PRICE = "Price";
        public const string STOCKT_ISDELETED = "IsDeleted";
        public const string STOCKT_CREATEDDATE = "CreatedDate";
        public const string STOCKT_CREATEDUSERID = "CreatedUserID";
        public const string STOCKT_LASTMODIFIEDDATE = "LastModifiedDate";
        public const string STOCKT_LASTMODIFIEDUSERID = "LastModifiedUserID";

        private int intStockTID = 0;
        private int intMaterialID = 0;
        private int intQuantity = 0;
        private int intIsOpeningStock = 0;
        private int intStockYear = 0;
        private string strMaterialInDate = string.Empty;
        private double dbPrice = 0.0;
        private int intIsDeleted = 0;
        private string strCreatedDate = string.Empty;
        private int intCreatedUserID = 0;
        private string strLastModifiedDate = string.Empty;
        private int intLastModifiedUserID = 0;

        #endregion

        #region ---Properties---
        public int StockTID
        {
            get { return intStockTID; }
            set { intStockTID = value; }
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
        public int IsOpeningStock
        {
            get { return intIsOpeningStock; }
            set { intIsOpeningStock = value; }
        }
        public int StockYear
        {
            get { return intStockYear; }
            set { intStockYear = value; }
        }
        public string MaterialInDate
        {
            get { return strMaterialInDate; }
            set { strMaterialInDate = value; }
        }
        public double Price
        {
            get { return dbPrice; }
            set { dbPrice = value; }
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