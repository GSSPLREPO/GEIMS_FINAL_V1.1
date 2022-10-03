using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class PaySlipLeaveTBo
    {
        #region PaySlipLeaveT Class Properties

        public const string PAYSLIPLEAVET_TABLE = "tbl_PayslipLeave_T";
        public const string PAYSLIPLEAVET_PAYSLIPLEAVEID = "PayslipLeaveID";
        public const string PAYSLIPLEAVET_PAYSLIPID = "PaySlipID";
        public const string PAYSLIPLEAVET_LEAVEID = "LeaveID";
        public const string PAYSLIPLEAVET_LEAVEOPENING = "LeaveOpening";
        public const string PAYSLIPLEAVET_LEAVEAVAILED = "LeaveAvailed";
        public const string PAYSLIPLEAVET_LEAVEBALANCE = "LeaveBalance";
        public const string PAYSLIPLEAVET_ISDELETED = "IsDeleted";
        public const string PAYSLIPLEAVET_CREATEDUSERID = "CreatedUserID";
        public const string PAYSLIPLEAVET_CREATEDDATE = "CreatedDate";
        public const string PAYSLIPLEAVET_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string PAYSLIPLEAVET_LASTMODIFIEDDATE = "LastModifiedDate";



        private int intPayslipLeaveID = 0;
        private int intPaySlipID = 0;
        private int intLeaveID = 0;
        private double dbLeaveOpening = 0.0;
        private double dbLeaveAvailed = 0.0;
        private double dbLeaveBalance = 0.0;
        private int intIsDeleted = 0;
        private int intCreatedUserID = 0;
        private string strCreatedDate = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;

        #endregion

        #region ---Properties---
        public int PayslipLeaveID
        {
            get { return intPayslipLeaveID; }
            set { intPayslipLeaveID = value; }
        }
        public int PaySlipID
        {
            get { return intPaySlipID; }
            set { intPaySlipID = value; }
        }
        public int LeaveID
        {
            get { return intLeaveID; }
            set { intLeaveID = value; }
        }
        public double LeaveOpening
        {
            get { return dbLeaveOpening; }
            set { dbLeaveOpening = value; }
        }
        public double LeaveAvailed
        {
            get { return dbLeaveAvailed; }
            set { dbLeaveAvailed = value; }
        }
        public double LeaveBalance
        {
            get { return dbLeaveBalance; }
            set { dbLeaveBalance = value; }
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


