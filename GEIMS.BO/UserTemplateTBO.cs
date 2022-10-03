using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class UserTemplateTBo
    {
        #region UserTemplateT Class Properties

        public const string USERTEMPLATET_TABLE = "tbl_UserTemplate_M";
        public const string USERTEMPLATET_USERTEMPLATEID = "UserTemplateID";
        public const string USERTEMPLATET_USERID = "UserID";
        public const string USERTEMPLATET_TRUSTMID = "TrustMID";
        public const string USERTEMPLATET_SCHOOLMID = "SchoolMID";
        public const string USERTEMPLATET_TRUSTTEMPLATEID = "TrustTemplateID";
        public const string USERTEMPLATET_ANNUAL = "Annual";
        public const string USERTEMPLATET_MONTHLY = "Monthly";
        public const string USERTEMPLATET_GROSS = "Gross";
        public const string USERTEMPLATET_LASTPAYSLIPGENERATED = "LastPaySlipGenerated";
        public const string USERTEMPLATET_ISDELETED = "IsDeleted";
        public const string USERTEMPLATET_CREATEDUSERID = "CreatedUserID";
        public const string USERTEMPLATET_CREATEDDATE = "CreatedDate";
        public const string USERTEMPLATET_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string USERTEMPLATET_LASTMODIFIEDDATE = "LastModifiedDate";



        private int intUserTemplateID = 0;
        private int intUserID = 0;
        private int intTrustMID = 0;
        private int intSchoolMID = 0;
        private int intTrustTemplateID = 0;
        private double dbAnnual = 0.0;
        private double dbMonthly = 0.0;
        private double dbGross = 0.0;
        private int intLastPaySlipGenerated = 0;
        private int intIsDeleted = 0;
        private int intCreatedUserID = 0;
        private string strCreatedDate = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;

        #endregion

        #region ---Properties---
        public int UserTemplateID
        {
            get { return intUserTemplateID; }
            set { intUserTemplateID = value; }
        }
        public int UserID
        {
            get { return intUserID; }
            set { intUserID = value; }
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
        public int TrustTemplateID
        {
            get { return intTrustTemplateID; }
            set { intTrustTemplateID = value; }
        }
        public double Annual
        {
            get { return dbAnnual; }
            set { dbAnnual = value; }
        }
        public double Monthly
        {
            get { return dbMonthly; }
            set { dbMonthly = value; }
        }
        public double Gross
        {
            get { return dbGross; }
            set { dbGross = value; }
        }
        public int LastPaySlipGenerated
        {
            get { return intLastPaySlipGenerated; }
            set { intLastPaySlipGenerated = value; }
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


