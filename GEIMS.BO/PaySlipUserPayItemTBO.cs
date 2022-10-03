using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class PaySlipUserPayItemTBo
    {
        #region PaySlipUserPayItemT Class Properties

        public const string PAYSLIPUSERPAYITEMT_TABLE = "tbl_PaySlipUserPayItem_T";
        public const string PAYSLIPUSERPAYITEMT_PAYSLIPUSERPAYITEMID = "PaySlipUserPayItemID";
        public const string PAYSLIPUSERPAYITEMT_PAYSLIPID = "PaySlipID";
        public const string PAYSLIPUSERPAYITEMT_USERPAYITEMID = "UserPayItemID";
        public const string PAYSLIPUSERPAYITEMT_PAYSLIPPAYITEMAMOUNT = "PaySlipPayItemAmount";
        public const string PAYSLIPUSERPAYITEMT_ISDELETED = "IsDeleted";
        public const string PAYSLIPUSERPAYITEMT_CREATEDUSERID = "CreatedUserID";
        public const string PAYSLIPUSERPAYITEMT_CREATEDDATE = "CreatedDate";
        public const string PAYSLIPUSERPAYITEMT_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string PAYSLIPUSERPAYITEMT_LASTMODIFIEDDATE = "LastModifiedDate";



        private int intPaySlipUserPayItemID = 0;
        private int intPaySlipID = 0;
        private int intUserPayItemID = 0;
        private double dbPaySlipPayItemAmount = 0.0;
        private int intIsDeleted = 0;
        private int intCreatedUserID = 0;
        private string strCreatedDate = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;

        #endregion

        #region ---Properties---
        public int PaySlipUserPayItemID
        {
            get { return intPaySlipUserPayItemID; }
            set { intPaySlipUserPayItemID = value; }
        }
        public int PaySlipID
        {
            get { return intPaySlipID; }
            set { intPaySlipID = value; }
        }
        public int UserPayItemID
        {
            get { return intUserPayItemID; }
            set { intUserPayItemID = value; }
        }
        public double PaySlipPayItemAmount
        {
            get { return dbPaySlipPayItemAmount; }
            set { dbPaySlipPayItemAmount = value; }
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


