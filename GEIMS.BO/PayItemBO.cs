using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class PayItemBo
    {
        #region PayItem Class Properties

        public const string PAYITEM_TABLE = "tbl_PayItem_M";
        public const string PAYITEM_PAYITEMMID = "PayItemMID";
        public const string PAYITEM_TRUSTMID = "TrustMID";
        public const string PAYITEM_SCHOOLMID = "SchoolMID";
        public const string PAYITEM_NAME = "Name";
        public const string PAYITEM_DESCRIPTION = "Description";
        public const string PAYITEM_TYPE = "Type";
        public const string PAYITEM_DEDUCTION = "Deduction";
        public const string PAYITEM_ISDELETED = "IsDeleted";
        public const string PAYITEM_CREATEDUSERID = "CreatedUserID";
        public const string PAYITEM_CREATEDDATE = "CreatedDate";
        public const string PAYITEM_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string PAYITEM_LASTMODIFIEDDATE = "LastModifiedDate";

        private int intPayItemMID = 0;
        private int intTrustMID = 0;
        private int intSchoolMID = 0;
        private string strName = string.Empty;
        private string strDescription = string.Empty;
        private int intType = 0;
        private int intDeduction = 0;
        private int intIsDeleted = 0;
        private int intCreatedUserID = 0;
        private string strCreatedDate = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;

        #endregion

        #region ---Properties---
        public int PayItemMID
        {
            get { return intPayItemMID; }
            set { intPayItemMID = value; }
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
        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }
        public string Description
        {
            get { return strDescription; }
            set { strDescription = value; }
        }
        public int Type
        {
            get { return intType; }
            set { intType = value; }
        }
        public int Deduction
        {
            get { return intDeduction; }
            set { intDeduction = value; }
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


