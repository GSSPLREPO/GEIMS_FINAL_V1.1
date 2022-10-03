using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
    public class MaterialGroupBO
    {
        #region MaterialBO Class Properties

        public const string MATERIALGROUP_TABLE = "tbl_MaterialGroup_M";
        public const string MATERIALGROUP_MATERIALGROUPID = "MaterialGroupID";
        public const string MATERIALGROUP_MATERIALGROUPNAME = "MaterialGroupName";
        public const string MATERIALGROUP_DESCRIPTION = "Description";
        public const string MATERIALGROUP_ISDELETED = "IsDeleted";
        public const string MATERIALGROUP_CREATEDUSERID = "CreatedUserID";
        public const string MATERIALGROUP_CREATEDDATE = "CreatedDate";
        public const string MATERIALGROUP_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string MATERIALGROUP_LASTMODIFIEDDATE = "LastModifiedDate";

        private int intMaterialGroupID = 0;
        private string strMaterialGroupName = string.Empty;
        private string strDescription = string.Empty;
        private int intIsDeleted = 0;
        private int intCreatedUserID = 0;
        private string strCreatedDate = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;
        #endregion

        #region ---Properties---
        public int MaterialGroupID
        {
            get { return intMaterialGroupID; }
            set { intMaterialGroupID = value; }
        }
        public string MaterialGroupName
        {
            get { return strMaterialGroupName; }
            set { strMaterialGroupName = value; }
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
        public int CreatedUserID
        {
            get { return intCreatedUserID; }
            set { intCreatedUserID = value; }
        }
        public string CreatedDate
        {
            get { return strCreatedDate; }
            set { strCreatedDate = value; }
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


