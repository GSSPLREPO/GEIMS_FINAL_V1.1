using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
    public class MaterialTransferBO
    {
        #region MaterialTransfer Class Properties

        public const string MATERIALTRANSFER_TABLE = "tbl_MaterialTransfer_M";
        public const string MATERIALTRANSFER_MTRANSFERID = "MTransferID";
        public const string MATERIALTRANSFER_TRUSTMID = "TrustMID";
        public const string MATERIALTRANSFER_SCHOOLMID = "SchoolMID";
        public const string MATERIALTRANSFER_MATERIALTID = "MaterialTID";
        public const string MATERIALTRANSFER_QUANTITY = "Quantity";
        public const string MATERIALTRANSFER_UOMID = "UOMID";
        public const string MATERIALTRANSFER_TRANSFERTO = "TransferTo";
        public const string MATERIALTRANSFER_TRANSFERTOID = "TransferToID";
        public const string MATERIALTRANSFER_YEAR = "Year";
        public const string MATERIALTRANSFER_ISDELETED = "IsDeleted";
        public const string MATERIALTRANSFER_CREATEDDATE = "CreatedDate";
        public const string MATERIALTRANSFER_CREATEDUSERID = "CreatedUserID";
        public const string MATERIALTRANSFER_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string MATERIALTRANSFER_LASTMODIFIEDDATE = "LastModifiedDate";

        private int intMTransferID = 0;
        private int intTrustMID = 0;
        private int intSchoolMID = 0;
        private int intMaterialTID = 0;
        private int intQuantity = 0;
        private int intUOMID = 0;
        private string strTransferTo = string.Empty;
        private int intTransferToID = 0;
        private string strYear = string.Empty;
        private int intIsDeleted = 0;
        private string strCreatedDate = string.Empty;
        private int intCreatedUserID = 0;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;

        #endregion

        #region ---Properties---
        public int MTransferID
        {
            get { return intMTransferID; }
            set { intMTransferID = value; }
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
        public int MaterialTID
        {
            get { return intMaterialTID; }
            set { intMaterialTID = value; }
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
        public string TransferTo
        {
            get { return strTransferTo; }
            set { strTransferTo = value; }
        }
        public int TransferToID
        {
            get { return intTransferToID; }
            set { intTransferToID = value; }
        }
        public string Year
        {
            get { return strYear; }
            set { strYear = value; }
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