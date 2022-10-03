using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
    public class UOMBO
    {
        #region UOM Class Properties

        public const string UOM_TABLE = "tbl_UOM_M";
        public const string UOM_UOMID = "UOMID";
        public const string UOM_UOMNAME = "UOMName";
        public const string UOM_DESCRIPTION = "Description";
        public const string UOM_ISDELETED = "IsDeleted";
        public const string UOM_CREATEDDATE = "CreatedDate";
        public const string UOM_CREATEDUSERID = "CreatedUserID";
        public const string UOM_LASTMODIFIEDDATE = "LastModifiedDate";
        public const string UOM_LASTMODIFIEDUSERID = "LastModifiedUserId";

        private int intUOMID = 0;
        private string strUOMName = string.Empty;
        private string strDescription = string.Empty;
        private int intIsDeleted = 0;
        private string strCreatedDate = string.Empty;
        private int intCreatedUserID = 0;
        private string strLastModifiedDate = string.Empty;
        private int intLastModifiedUserId = 0;

        #endregion

        #region ---Properties---
        public int UOMID
        {
            get { return intUOMID; }
            set { intUOMID = value; }
        }
        public string UOMName
        {
            get { return strUOMName; }
            set { strUOMName = value; }
        }
        public string Description
        {
            get { return strDescription; }
            set { strDescription = value; }
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
        public int LastModifiedUserId
        {
            get { return intLastModifiedUserId; }
            set { intLastModifiedUserId = value; }
        }

        #endregion
    }
}



