using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
    public class ConsumptionBO
    {
        #region Consumption Class Properties

        public const string CONSUMPTION_TABLE = "tbl_Consumption_M";
        public const string CONSUMPTION_CONSUMPTIONID = "ConsumptionID";
        public const string CONSUMPTION_TRUSTMID = "TrustMID";
        public const string CONSUMPTION_SCHOOLMID = "SchoolMID";
        public const string CONSUMPTION_MATERIALID = "MaterialID";
        public const string CONSUMPTION_QUANTITY = "Quantity";
        public const string CONSUMPTION_UOMID = "UOMID";
        public const string CONSUMPTION_CONSUMPTIONDATE = "ConsumptionDate";
        public const string CONSUMPTION_CONSUMPTIONYEAR = "ConsumptionYear";
        public const string CONSUMPTION_ISCONSUMPTION = "IsConsumption";
        public const string CONSUMPTION_ISDELETED = "IsDeleted";
        public const string CONSUMPTION_CREATEDDATE = "CreatedDate";
        public const string CONSUMPTION_CREATEDUSERID = "CreatedUserID";
        public const string CONSUMPTION_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string CONSUMPTION_LASTMODIFIEDDATE = "LastModifiedDate";

        private int intConsumptionID = 0;
        private int intTrustMID = 0;
        private int intSchoolMID = 0;
        private int intMaterialID = 0;
        private int intQuantity = 0;
        private int intUOMID = 0;
        private string strConsumptionDate = string.Empty;
        private int intConsumptionYear = 0;
        private int intIsConsumption = 0;
        private int intIsDeleted = 0;
        private string strCreatedDate = string.Empty;
        private int intCreatedUserID = 0;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;

        #endregion

        #region ---Properties---
        public int ConsumptionID
        {
            get { return intConsumptionID; }
            set { intConsumptionID = value; }
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
        public int UOMID
        {
            get { return intUOMID; }
            set { intUOMID = value; }
        }
        public string ConsumptionDate
        {
            get { return strConsumptionDate; }
            set { strConsumptionDate = value; }
        }
        public int ConsumptionYear
        {
            get { return intConsumptionYear; }
            set { intConsumptionYear = value; }
        }
        public int IsConsumption
        {
            get { return intIsConsumption; }
            set { intIsConsumption = value; }
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

        #endregion
    }
}