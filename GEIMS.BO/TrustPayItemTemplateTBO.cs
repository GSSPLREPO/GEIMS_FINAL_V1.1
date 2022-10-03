using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class TrustPayItemTemplateTBo
    {
        #region TrustPayItemTemplateT Class Properties

        public const string TRUSTPAYITEMTEMPLATET_TABLE = "tbl_TrustPayItemTemplate_T";
        public const string TRUSTPAYITEMTEMPLATET_TRUSTTEMPLATEID = "TrustTemplateID";
        public const string TRUSTPAYITEMTEMPLATET_TEMPLATEID = "TemplateID";
        public const string TRUSTPAYITEMTEMPLATET_PAYITEMID = "PayItemID";
        public const string TRUSTPAYITEMTEMPLATET_PAYITEMTYPE = "PayItemType";
        public const string TRUSTPAYITEMTEMPLATET_PAYITEMDEPENDSON = "PayItemDependsOn";
        public const string TRUSTPAYITEMTEMPLATET_PERCENTAGE = "Percentage";
        public const string TRUSTPAYITEMTEMPLATET_AMOUNT = "Amount";
        public const string TRUSTPAYITEMTEMPLATET_ISDELETED = "IsDeleted";
        public const string TRUSTPAYITEMTEMPLATET_CREATEDUSERID = "CreatedUserID";
        public const string TRUSTPAYITEMTEMPLATET_CREATEDDATE = "CreatedDate";
        public const string TRUSTPAYITEMTEMPLATET_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string TRUSTPAYITEMTEMPLATET_LASTMODIFIEDDATE = "LastModifiedDate";



        private int intTrustTemplateID = 0;
        private int intTemplateID = 0;
        private int intPayItemID = 0;
        private int intPayItemType = 0;
        private string strPayItemDependsOn = string.Empty;
        private double dbPercentage = 0.0;
        private double dbAmount = 0.0;
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
        public int TemplateID
        {
            get { return intTemplateID; }
            set { intTemplateID = value; }
        }
        public int PayItemID
        {
            get { return intPayItemID; }
            set { intPayItemID = value; }
        }
        public int PayItemType
        {
            get { return intPayItemType; }
            set { intPayItemType = value; }
        }
        public string PayItemDependsOn
        {
            get { return strPayItemDependsOn; }
            set { strPayItemDependsOn = value; }
        }
        public double Percentage
        {
            get { return dbPercentage; }
            set { dbPercentage = value; }
        }
        public double Amount
        {
            get { return dbAmount; }
            set { dbAmount = value; }
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


