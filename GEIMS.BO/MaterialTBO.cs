using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class MaterialTBo
    {
        #region MaterialT Class Properties

        public const string MATERIALT_TABLE = "tbl_Material_T";
        public const string MATERIALT_MATERIALTID = "MaterialTID";
        public const string MATERIALT_MATERIALID = "MaterialID";
        public const string MATERIALT_TRUSTMID = "TrustMID";
        public const string MATERIALT_SCHOOLMID = "SchoolMID";
        public const string MATERIALT_MAINQUANTITY = "MainQuantity";
        public const string MATERIALT_ISDELETED = "IsDeleted";
        public const string MATERIALT_CREATEDDATE = "CreatedDate";
        public const string MATERIALT_CREATEDUSERID = "CreatedUserID";
        public const string MATERIALT_LASTMODIFIEDDATE = "LastModifiedDate";
        public const string MATERIALT_LASTMODIFIEDUSERID = "LastModifiedUserID";



        private int intMaterialTID = 0;
        private int intMaterialID = 0;
        private int intTrustMID = 0;
        private int intSchoolMID = 0;
        private int intMainQuantity = 0;
        private int intIsDeleted = 0;
        private string strCreatedDate = string.Empty;
        private int intCreatedUserID = 0;
        private string strLastModifiedDate = string.Empty;
        private int intLastModifiedUserID = 0;

        #endregion

        #region ---Properties---
        public int MaterialTID
        {
            get { return intMaterialTID; }
            set { intMaterialTID = value; }
        }
        public int MaterialID
        {
            get { return intMaterialID; }
            set { intMaterialID = value; }
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
        public int MainQuantity
        {
            get { return intMainQuantity; }
            set { intMainQuantity = value; }
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


