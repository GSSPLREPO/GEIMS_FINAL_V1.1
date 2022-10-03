using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class ShiftMasterBO
    {
        #region ShiftMaster Properties

        public const string SHIFTMASTER_TABLE = "tbl_Shift";
        public const string SHIFTMASTER_SHIFTMID = "ShiftMID";
        public const string SHIFTMASTER_TRUSTID   = "TRUSTID";
        public const string SHIFTMASTER_SHIFTNAME = "ShiftName";
        public const string SHIFTMASTER_STARTTIME = "StartTime";
        public const string SHIFTMASTER_RECESSSTARTTIME = "RecessStartTime";
        public const string SHIFTMASTER_RECESSENDTIME = "RecessEndTime";
        public const string SHIFTMASTER_ENDTIME = "EndTime";
        public const string SHIFTMASTER_TOTALWORKINGHOURS = "TotalWH";
        public const string SHIFTMASTER_TOTALFIRSTHALFTIME= "TotalFirstHalfWH";
        public const string SHIFTMASTER_TOTALSECONDHALFTIME = "TotalSecondHalfWH";
        public const string SHIFTMASTER_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string SHIFTMASTER_LASTMODIFIEDDATE = "LastModifiedDate";
        public const string SHIFTMASTER_CREATEMODIFIEDUSERID = "CreateModifiedUserID";
        public const string SHIFTMASTER_CREATEDMODIFIEDDATE = "CreatedModifiedDate";
        public const string SHIFTMASTER_ISDELETED = "IsDeleted";



        private int intShiftMasterMID = 0;
        private int intTrustMID = 0;
        private string strShiftName = string.Empty;
        private string strstartTime = string.Empty;
        private string strRecessstartTime = string.Empty;
        private string strRecessEndTime = string.Empty;
        private string strEndTime = string.Empty;
        private string strTotalWH = string.Empty;
        private string strFirstHalfTime = string.Empty;
        private string strSecondHalfTime = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;
        private int intCreateModifiedUserID = 0;
        private string strCreatedModifiedDate = string.Empty;
        private int intIsDeleted = 0;


        #endregion

        #region ---Properties---
        public int ShiftMID
        {
            get { return intShiftMasterMID; }
            set { intShiftMasterMID = value; }
        }
        public int TrustMID
        {
            get { return intTrustMID; }
            set { intTrustMID = value; }
        }
        public string ShiftName
        {
            get { return strShiftName; }
            set { strShiftName = value; }
        }

        public string StartTime
        {
            get { return strstartTime; }
            set { strstartTime = value; }
        }
        public string RecessStartTime
        {
            get { return strRecessstartTime; }
            set { strRecessstartTime = value; }
        }
        public string RecessEndTime
        {
            get { return strRecessEndTime; }
            set { strRecessEndTime = value; }
        }

        public string EndTime
        {
            get { return strEndTime; }
            set { strEndTime = value; }
        }
        public string TotalWH
        {
            get { return strTotalWH; }
            set { strTotalWH = value; }
        }
        public string FirstHalfWH
        {
            get { return strFirstHalfTime; }
            set { strFirstHalfTime = value; }
        }

        public string SecondHalfWH
        {
            get { return strSecondHalfTime; }
            set { strSecondHalfTime = value; }
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
        public int CreateModifiedUserID
        {
            get { return intCreateModifiedUserID; }
            set { intCreateModifiedUserID = value; }
        }
        public string CreatedModifiedDate
        {
            get { return strCreatedModifiedDate; }
            set { strCreatedModifiedDate = value; }
        }
        public int IsDeleted
        {
            get { return intIsDeleted; }
            set { intIsDeleted = value; }
        }

        #endregion
    }




}
