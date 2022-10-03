using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
    public class MaterialBO
    {
        #region Material Class Properties

        public const string MATERIAL_TABLE = "tbl_Material_M";
        public const string MATERIAL_MATERIALID = "MaterialID";
        public const string MATERIAL_MATERIALGROUPID = "MaterialGroupID";
        public const string MATERIAL_TRUSTMID = "TrustMID";
        public const string MATERIAL_SCHOOLMID = "SchoolMID";
        public const string MATERIAL_UOMID = "UOMID";
        public const string MATERIAL_MATERIALCODE = "MaterialCode";
        public const string MATERIAL_MATERIALNAME = "MaterialName";
        public const string MATERIAL_DESCRIPTION = "Description";
        public const string MATERIAL_MODELNO = "ModelNo";
        public const string MATERIAL_ISDELETED = "IsDeleted";
        public const string MATERIAL_CREATEDDATE = "CreatedDate";
        public const string MATERIAL_CREATEDUSERID = "CreatedUserID";
        public const string MATERIAL_LASTMODIFIEDDATE = "LastModifiedDate";
        public const string MATERIAL_LASTMODIFIEDUSERID = "LastModifiedUserID";

        private int intMaterialID = 0;
        private int intMaterialGroupID = 0;
        private int intTrustMID = 0;
        private int intSchoolMID = 0;
        private int intUOMID = 0;
        private string strMaterialCode = string.Empty;
        private string strMaterialName = string.Empty;
        private string strDescription = string.Empty;
        private string strModelNo = string.Empty;
        private int intIsDeleted = 0;
        private string strCreatedDate = string.Empty;
        private int intCreatedUserID = 0;
        private string strLastModifiedDate = string.Empty;
        private int intLastModifiedUserID = 0;

        #endregion

        #region ---Properties---
        public int MaterialID
        {
            get { return intMaterialID; }
            set { intMaterialID = value; }
        }
        public int MaterialGroupID
        {
            get { return intMaterialGroupID; }
            set { intMaterialGroupID = value; }
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
        public int UOMID
        {
            get { return intUOMID; }
            set { intUOMID = value; }
        }
        public string MaterialCode
        {
            get { return strMaterialCode; }
            set { strMaterialCode = value; }
        }
        public string MaterialName
        {
            get { return strMaterialName; }
            set { strMaterialName = value; }
        }
        public string Description
        {
            get { return strDescription; }
            set { strDescription = value; }
        }
        public string ModelNo
        {
            get { return strModelNo; }
            set { strModelNo = value; }
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