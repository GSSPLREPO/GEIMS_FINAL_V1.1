using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
    public class LockBo
    {
        #region Lock Class Properties

        public const string LOCK_TABLE = "tbl_Lock_M";
        public const string LOCK_LOCKID = "LockID";
        public const string LOCK_TRUSTMID = "TrustMID";
        public const string LOCK_SCHOOLMID = "SchoolMID";
        public const string LOCK_FROMDATE = "FromDate";
        public const string LOCK_TODATE = "ToDate";
        public const string LOCK_LOCKYEAR = "LockYear";

        private int intLockID = 0;
        private int intTrustMID = 0;
        private int intSchoolMID = 0;
        private string strFromDate = string.Empty;
        private string strToDate = string.Empty;
        private int intLockYear = 0;

        #endregion

        #region ---Properties---
        public int LockID
        {
            get { return intLockID; }
            set { intLockID = value; }
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
        public string FromDate
        {
            get { return strFromDate; }
            set { strFromDate = value; }
        }
        public string ToDate
        {
            get { return strToDate; }
            set { strToDate = value; }
        }
        public int LockYear
        {
            get { return intLockYear; }
            set { intLockYear = value; }
        }

        #endregion
    }
}


