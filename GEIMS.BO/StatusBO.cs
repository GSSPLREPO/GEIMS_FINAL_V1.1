using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class StatusBO
    {
        #region Status Class Properties

        public const string STATUS_TABLE = "tbl_Status_M";
        public const string STATUS_STATUSMASTERID = "StatusMasterID";
        public const string STATUS_TRUSTMID = "TrustMID";
        public const string STATUS_STATUSNAME = "StatusName";
        public const string STATUS_DISCRIPTION = "Discription";
        public const string STATUS_ISDELETED = "IsDeleted";
        public const string STATUS_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string STATUS_LASTMODIFICATIONDATE = "LastModificationDate";



        private int intStatusMasterID = 0;
        private int intTrustID = 0;
        private string strStatusName = string.Empty;
        private string strDiscription = string.Empty;
        private int intIsDeleted = 0;
        private int intLastModifiedUserID = 0;
        private string strLastModificationDate = string.Empty;

        #endregion

        #region ---Properties---
        public int StatusMasterID
        {
            get { return intStatusMasterID; }
            set { intStatusMasterID = value; }
        }
        public int TrustMID
        {
            get { return intTrustID; }
            set { intTrustID = value; }
        }
        public string StatusName
        {
            get { return strStatusName; }
            set { strStatusName = value; }
        }
        public string Discription
        {
            get { return strDiscription; }
            set { strDiscription = value; }
        }
        public int IsDeleted
        {
            get { return intIsDeleted; }
            set { intIsDeleted = value; }
        }
        public int LastModifiedUserID
        {
            get { return intLastModifiedUserID; }
            set { intLastModifiedUserID = value; }
        }
        public string LastModificationDate
        {
            get { return strLastModificationDate; }
            set { strLastModificationDate = value; }
        }

        #endregion
    }

}
