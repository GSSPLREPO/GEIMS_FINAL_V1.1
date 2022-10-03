using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class TrustTemplateBo
    {
        #region TrustTemplate Class Properties

        public const string TRUSTTEMPLATE_TABLE = "tbl_TrustTemplate_M";
        public const string TRUSTTEMPLATE_TRUSTTEMPLATEID = "TrustTemplateID";
        public const string TRUSTTEMPLATE_TRUSTTEMPLATENAME = "TrustTemplateName";
        public const string TRUSTTEMPLATE_SCHOOLMID = "SchoolMID";
        public const string TRUSTTEMPLATE_TRUSTMID = "TrustMID";
        public const string TRUSTTEMPLATE_ISDELETED = "IsDeleted";
        public const string TRUSTTEMPLATE_CREATEDUSERID = "CreatedUserID";
        public const string TRUSTTEMPLATE_CREATEDDATE = "CreatedDate";
        public const string TRUSTTEMPLATE_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string TRUSTTEMPLATE_LASTMODIFIEDDATE = "LastModifiedDate";

        private int intTrustTemplateID = 0;
        private string strTrustTemplateName = string.Empty;
        private int intSchoolMID = 0;
        private int intTrustMID = 0;
        private int intIsDeleted = 0;
        private int intCreatedUserID = 0;
        private string strCreatedDate = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;

        #endregion

        #region ---Properties---
        public int TrustTemplateID
        {
            get { return intTrustTemplateID; }
            set { intTrustTemplateID = value; }
        }
        public string TrustTemplateName
        {
            get { return strTrustTemplateName; }
            set { strTrustTemplateName = value; }
        }
        public int SchoolMID
        {
            get { return intSchoolMID; }
            set { intSchoolMID = value; }
        }
        public int TrustMID
        {
            get { return intTrustMID; }
            set { intTrustMID = value; }
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



