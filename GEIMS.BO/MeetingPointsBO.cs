using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class MeetingPointsBO
    {
        #region MeetingPoints Class Properties

        public const string MEETINGPOINTS_TABLE = "tbl_MeetingPoints";
        public const string MEETINGPOINTS_POINTID = "PointID";
        public const string MEETINGPOINTS_MEETINGID = "MeetingID";
        public const string MEETINGPOINTS_ParticipantID = "ParticipantID";
        public const string MEETINGPOINTS_POINT = "Point";
        public const string MEETINGPOINTS_ACTION = "Action";
        public const string MEETINGPOINTS_ASSIGNTO = "AssignTo";
        public const string MEETINGPOINTS_TARGETDATE = "TargetDate";
        public const string MEETINGPOINTS_STATUS = "Status";
        public const string MEETINGPOINTS_REMARKS = "Remarks";
       
        public const string MEETINGPOINTS_ISDELETED = "IsDelete";
        public const string MEETINGPOINTS_CREATEDBYID = "CreatedByID";
        public const string MEETINGPOINTS_CREATEDBYDATE = "CreatedByDate";
        public const string MEETINGPOINTS_LASTMODIFIEDBYID = "LastModifiedByID";
        public const string MEETINGPOINTS_LASTMODIFIEDBYDATE = "LastModifiedByDate";

        private int intPointID = 0;
        private int intMeetingID = 0;
        private int intParticipantID = 0;
        private string strPoint = string.Empty;
        private string strAction = string.Empty;
        private string strAssignTo = string.Empty;
        private string strTargetDate = string.Empty;
        private string strStatus = string.Empty;
        private string strRemarks = string.Empty;
        
        private int intIsDeleted = 0;
        private int intCreatedByID = 0;
        private string strCreatedByDate = string.Empty;
        private int intLastModifiedByID = 0;
        private string strLastModifiedByDate = string.Empty;

        #endregion

        #region ---Properties---

        public int PointID
        {
            get { return intPointID; }
            set { intPointID = value; }
        }

        public int MeetingID
        {
            get { return intMeetingID; }
            set { intMeetingID = value; }
        }

        public int ParticipantID
        {
            get { return intParticipantID; }
            set { intParticipantID = value; }
        }

        public string Point
        {
            get { return strPoint; }
            set { strPoint = value; }
        }

        public string Action
        {
            get { return strAction; }
            set { strAction = value; }
        }

        public string AssignTo
        {
            get { return strAssignTo; }
            set { strAssignTo = value; }
        }

        public string TargetDate
        {
            get { return strTargetDate; }
            set { strTargetDate = value; }
        }

        public string Status
        {
            get { return strStatus; }
            set { strStatus = value; }
        }

        public string Remarks
        {
            get { return strRemarks; }
            set { strRemarks = value; }
        }

        public int IsDeleted
        {
            get { return intIsDeleted; }
            set { intIsDeleted = value; }
        }

        public int CreatedByID
        {
            get { return intCreatedByID; }
            set { intCreatedByID = value; }
        }

        public string CreatedByDate
        {
            get { return strCreatedByDate; }
            set { strCreatedByDate = value; }
        }

        public int LastModifiedByID
        {
            get { return intLastModifiedByID; }
            set { intLastModifiedByID = value; }
        }

        public string LastModifiedByDate
        {
            get { return strLastModifiedByDate; }
            set { strLastModifiedByDate = value; }
        }
        #endregion
    }
}
