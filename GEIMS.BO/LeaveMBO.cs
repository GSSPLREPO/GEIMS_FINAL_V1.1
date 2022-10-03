using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class LeaveMBo
    {
        #region LeaveM Class Properties

        public const string LEAVEM_TABLE = "tbl_Leave_M";
        public const string LEAVEM_LEAVEID = "LeaveID";
        public const string LEAVEM_TRUSTMID = "TrustMID";
        public const string LEAVEM_LEAVECODE = "LeaveCode";
        public const string LEAVEM_LEAVENAME = "LeaveName";
        public const string LEAVEM_LEAVEDESCRIPTION = "LeaveDescription";
        public const string LEAVEM_LEAVEOPENING = "LeaveOpening";
        public const string LEAVEM_LEAVECARRYFORWARDLIMIT = "LeaveCarryForwardLimit";
        public const string LEAVEM_YEAR = "Year";
        public const string LEAVEM_ISDELETED = "IsDeleted";
        public const string LEAVEM_CREATEDUSERID = "CreatedUserID";
        public const string LEAVEM_CREATEDDATE = "CreatedDate";
        public const string LEAVEM_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string LEAVEM_LASTMODIFIEDDATE = "LastModifiedDate";



        private int intLeaveID = 0;
        private int intTrustMID = 0;
        private string strLeaveCode = string.Empty;
        private string strLeaveName = string.Empty;
        private string strLeaveDescription = string.Empty;
        private int intLeaveOpening = 0;
        private int intLeaveCarryForwardLimit = 0;
        private int intYear = 0;
        private int intIsDeleted = 0;
        private int intCreatedUserID = 0;
        private string strCreatedDate = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;

        #endregion

        #region ---Properties---()
        public int LeaveID
        {
            get { return intLeaveID; }
            set { intLeaveID = value; }
        }
        public int TrustMID
        {
            get { return intTrustMID; }
            set { intTrustMID = value; }
        }
        public string LeaveCode
        {
            get { return strLeaveCode; }
            set { strLeaveCode = value; }
        }
        public string LeaveName
        {
            get { return strLeaveName; }
            set { strLeaveName = value; }
        }
        public string LeaveDescription
        {
            get { return strLeaveDescription; }
            set { strLeaveDescription = value; }
        }
        public int LeaveOpening
        {
            get { return intLeaveOpening; }
            set { intLeaveOpening = value; }
        }
        public int LeaveCarryForwardLimit
        {
            get { return intLeaveCarryForwardLimit; }
            set { intLeaveCarryForwardLimit = value; }
        }
        public int Year
        {
            get { return intYear; }
            set { intYear = value; }
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


