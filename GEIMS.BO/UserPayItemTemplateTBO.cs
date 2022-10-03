using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class UserPayItemTemplateTBo
    {
        #region UserPayItemTemplateT Class Properties

        public const string USERPAYITEMTEMPLATET_TABLE = "tbl_UserPayItemTemplate_T";
        public const string USERPAYITEMTEMPLATET_USERPAYITEMTEMPLATEID = "UserPayItemTemplateID";
        public const string USERPAYITEMTEMPLATET_TRUSTMID = "TrustMID";
        public const string USERPAYITEMTEMPLATET_SCHOOLMID = "SchoolMID";
        public const string USERPAYITEMTEMPLATET_USERTEMPLATEID = "UserTemplateID";
        public const string USERPAYITEMTEMPLATET_PAYITEM = "PayItem";
        public const string USERPAYITEMTEMPLATET_DEPENDSON = "DependsOn";
        public const string USERPAYITEMTEMPLATET_TYPE = "Type";
        public const string USERPAYITEMTEMPLATET_PERCENTAGE = "Percentage";
        public const string USERPAYITEMTEMPLATET_AMOUNT = "Amount";
        public const string USERPAYITEMTEMPLATET_ISDELETED = "IsDeleted";
        public const string USERPAYITEMTEMPLATET_CREATEDUSERID = "CreatedUserID";
        public const string USERPAYITEMTEMPLATET_CREATEDDATE = "CreatedDate";
        public const string USERPAYITEMTEMPLATET_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string USERPAYITEMTEMPLATET_LASTMODIFIEDDATE = "LastModifiedDate";



        private int intUserPayItemTemplateID = 0;
        private int intTrustMID = 0;
        private int intSchoolMID = 0;
        private int intUserTemplateID = 0;
        private int intPayItem = 0;
        private string strDependsOn = string.Empty;
        private string strType = string.Empty;
        private double dbPercentage = 0.0;
        private double dbAmount = 0.0;
        private int intIsDeleted = 0;
        private int intCreatedUserID = 0;
        private string strCreatedDate = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;

        #endregion

        #region ---Properties---
        public int UserPayItemTemplateID
        {
            get { return intUserPayItemTemplateID; }
            set { intUserPayItemTemplateID = value; }
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
        public int UserTemplateID
        {
            get { return intUserTemplateID; }
            set { intUserTemplateID = value; }
        }
        public int PayItem
        {
            get { return intPayItem; }
            set { intPayItem = value; }
        }
        public string DependsOn
        {
            get { return strDependsOn; }
            set { strDependsOn = value; }
        }
        public string Type
        {
            get { return strType; }
            set { strType = value; }
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


