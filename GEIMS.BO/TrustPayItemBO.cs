using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class TrustPayItemBo
    {
        #region TrustPayItem Class Properties

        public const string TRUSTPAYITEM_TABLE = "tbl_TrustPayItem_M";
        public const string TRUSTPAYITEM_TRUSTPAYITEMID = "TrustPayItemID";
        public const string TRUSTPAYITEM_TRUSTMID = "TrustMID";
        public const string TRUSTPAYITEM_SCHOOLMID = "SchoolMID";
        public const string TRUSTPAYITEM_PAYITEMID = "PayItemID";
        public const string TRUSTPAYITEM_ISDELETED = "IsDeleted";
        public const string TRUSTPAYITEM_CREATEDUSERID = "CreatedUserID";
        public const string TRUSTPAYITEM_CREATEDDATE = "CreatedDate";
        public const string TRUSTPAYITEM_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string TRUSTPAYITEM_LASTMODIFIEDDATE = "LastModifiedDate";

        private int intTrustPayItemID = 0;
        private int intTrustMID = 0;
        private int intSchoolMID = 0;
        private int intPayItemID = 0;
        private int intIsDeleted = 0;
        private int intCreatedUserID = 0;
        private string strCreatedDate = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;

        #endregion

        #region ---Properties---
        public int TrustPayItemID
        {
            get { return intTrustPayItemID; }
            set { intTrustPayItemID = value; }
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
        public int PayItemID
        {
            get { return intPayItemID; }
            set { intPayItemID = value; }
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



