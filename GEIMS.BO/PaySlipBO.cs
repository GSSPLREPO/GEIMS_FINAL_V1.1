using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class PaySlipBo
    {
        #region PaySlip Class Properties

        public const string PAYSLIP_TABLE = "tbl_Payslip_M";
        public const string PAYSLIP_PAYSLIPID = "PayslipID";
        public const string PAYSLIP_TRUSTMID = "TrustMID";
        public const string PAYSLIP_USERID = "UserID";
        public const string PAYSLIP_MONTH = "Month";
        public const string PAYSLIP_YEAR = "Year";
        public const string PAYSLIP_TOTALDAYSOFMONTH = "TotalDaysofMonth";
        public const string PAYSLIP_EARNEDDAYSOFMONTH = "EarnedDaysofMonth";
        public const string PAYSLIP_PAYSLIPSENDFORAPPROVAL = "PaySlipSendforApproval";
        public const string PAYSLIP_PAYSLIPAPPROVED = "PayslipApproved";
        public const string PAYSLIP_EXCEMPTION = "Excemption";
        public const string PAYSLIP_ISDELETED = "IsDeleted";
        public const string PAYSLIP_CREATEDUSERID = "CreatedUserID";
        public const string PAYSLIP_CREATEDDATE = "CreatedDate";
        public const string PAYSLIP_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string PAYSLIP_LASTMODIFIEDDATE = "LastModifiedDate";



        private int intPayslipID = 0;
        private int intTrustMID = 0;
        private int intUserID = 0;
        private int intMonth = 0;
        private int intYear = 0;
        //private int intTotalDaysofMonth = 0;
        private double intTotalDaysofMonth = 0.0;
        private double dbEarnedDaysofMonth = 0.0;
        private int intPaySlipSendforApproval = 0;
        private int intPayslipApproved = 0;
        private int intExcemption = 0;
        private int intIsDeleted = 0;
        private int intCreatedUserID = 0;
        private string strCreatedDate = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;

        #endregion

        #region ---Properties---
        public int PayslipID
        {
            get { return intPayslipID; }
            set { intPayslipID = value; }
        }
        public int TrustMID
        {
            get { return intTrustMID; }
            set { intTrustMID = value; }
        }
        public int UserID
        {
            get { return intUserID; }
            set { intUserID = value; }
        }
        public int Month
        {
            get { return intMonth; }
            set { intMonth = value; }
        }
        public int Year
        {
            get { return intYear; }
            set { intYear = value; }
        }
        //public int TotalDaysofMonth
        //{
        //    get { return intTotalDaysofMonth; }
        //    set { intTotalDaysofMonth = value; }
        //}
        public Double TotalDaysofMonth
        {
            get { return intTotalDaysofMonth; }
            set { intTotalDaysofMonth = value; }
        }
        public double EarnedDaysofMonth
        {
            get { return dbEarnedDaysofMonth; }
            set { dbEarnedDaysofMonth = value; }
        }
        public int PaySlipSendforApproval
        {
            get { return intPaySlipSendforApproval; }
            set { intPaySlipSendforApproval = value; }
        }
        public int PayslipApproved
        {
            get { return intPayslipApproved; }
            set { intPayslipApproved = value; }
        }
        public int Excemption
        {
            get { return intExcemption; }
            set { intExcemption = value; }
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


