using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    
    public class MeetingsBO
    {
        #region Meetings Class Properties of Meetings Table

        public const string MEETINGS_TABLE = "tbl_Meetings";
        public const string MEETINGS_MEETINGID = "MeetingID";
        public const string MEETINGS_CATEGORYID = "CategoryID";
        public const string MEETINGS_TRUSTMID = "TrustMID";
        public const string MEETINGS_TOPIC = "Topic";
        public const string MEETINGS_MEETINGDATE = "MeetingDate";
        public const string MEETINGS_FROMTIME = "FromTime";
        public const string MEETINGS_TOTIME = "ToTime";
        public const string MEETINGS_VENUE = "Venue";
        public const string MEETINGS_MODE = "Mode";
        public const string MEETINGS_ORGANIZEBY = "OrganizeBy";
        public const string MEETINGS_STATUS = "Status";
        public const string MEETINGS_MINUTESBY = "MinutesBy";

        public const string MEETINGS_ISDELETED = "IsDelete";
        public const string MEETINGS_CREATEDBYID = "CreatedByID";
        public const string MEETINGS_CREATEDBYDATE = "CreatedByDate";
        public const string MEETINGS_LASTMODIFIEDBYID = "LastModifiedByID";
        public const string MEETINGS_LASTMODIFIEDBYDATE = "LastModifiedByDate";

        private int intMeetingID = 0;
        private int intCategoryID = 0;
        private int intTrustMID = 0;
        private string strTopic = string.Empty;
        private string strMeetingDate = string.Empty;
        private string strFromTime = string.Empty;
        private string strToTime = string.Empty;
        private string strVenue = string.Empty;
        private string strMode = string.Empty;
        private int intOrganizeBy = 0;
        private string strStatus = string.Empty;
        private int intMinutesBy = 0;

        private int intIsDeleted = 0;
        private int intCreatedByID = 0;
        private string strCreatedByDate = string.Empty;
        private int intLastModifiedByID = 0;
        private string strLastModifiedByDate = string.Empty;

        #endregion

        #region Meetings Class Properties of MeetingAgenda Table

        public const string MEETINGAGENDAS_TABLE = "tbl_MeetingAgendas";
        public const string MEETINGAGENDAS_AGENDAID = "AgendaID";
        public const string MEETINGAGENDAS_MEETINGID = "MeetingID";
        public const string MEETINGAGENDAS_AGENDAPOINT = "AgendaPoint";
        
        public const string MEETINGAGENDAS_ISDELETED = "IsDelete";
        public const string MEETINGAGENDAS_CREATEDBYID = "CreatedByID";
        public const string MEETINGAGENDAS_CREATEDBYDATE = "CreatedByDate";
        public const string MEETINGAGENDAS_LASTMODIFIEDBYID = "LastModifiedByID";
        public const string MEETINGAGENDAS_LASTMODIFIEDBYDATE = "LastModifiedByDate";

        private int intAgendaID = 0;        
        private string strAgendaPoint = string.Empty;
        #endregion

        #region Meetings Class Properties of MeetingParticipants Table

        public const string MEETINGPARTICIPANTS_TABLE = "tbl_MeetingParticipants";
        public const string MEETINGPARTICIPANTS_PARTICIPANTID = "ParticipantID";
        public const string MEETINGPARTICIPANTS_MEETINGID = "MeetingID";
        public const string MEETINGPARTICIPANTS_EMPLOYEEID = "EmployeeID";
        public const string MEETINGPARTICIPANTS_NAME = "Name";
        public const string MEETINGPARTICIPANTS_ORGID = "OrgID";
        public const string MEETINGPARTICIPANTS_ORGNAME = "OrgName";
        public const string MEETINGPARTICIPANTS_EMAIL = "Email";
        public const string MEETINGPARTICIPANTS_SENDINVITE = "SendInvite";
        public const string MEETINGPARTICIPANTS_SENDMOM = "SendMOM";
        public const string MEETINGPARTICIPANTS_ABSENT = "Absent";


        public const string MEETINGPARTICIPANTS_ISDELETED = "IsDelete";
        public const string MEETINGPARTICIPANTS_CREATEDBYID = "CreatedByID";
        public const string MEETINGPARTICIPANTS_CREATEDBYDATE = "CreatedByDate";
        public const string MEETINGPARTICIPANTS_LASTMODIFIEDBYID = "LastModifiedByID";
        public const string MEETINGPARTICIPANTS_LASTMODIFIEDBYDATE = "LastModifiedByDate";

        private int intParticipantID = 0;
        private int intEmployeeID = 0;
        private string strName = string.Empty;
        private int intOrgID = 0;
        private string strOrgName = string.Empty;
        private string strEmail = string.Empty;
        private int intSendInvite = 0;
        private int intSendMOM = 0;
        private int intAbsent = 0;

        #endregion

        #region ---Properties of Meetings Table---

        public int MeetingID
        {
            get { return intMeetingID; }
            set { intMeetingID = value; }
        }

        public int CategoryID
        {
            get { return intCategoryID; }
            set { intCategoryID = value; }
        }

        public int TrustMID
        {
            get { return intTrustMID; }
            set { intTrustMID = value; }
        }

        public string Topic
        {
            get { return strTopic; }
            set { strTopic = value; }
        }

        public string MeetingDate
        {
            get { return strMeetingDate; }
            set { strMeetingDate= value; }
        }

        public string FromTime
        {
            get { return strFromTime; }
            set { strFromTime = value; }
        }

        public string Totime
        {
            get { return strToTime; }
            set { strToTime = value; }
        }

        public string Venue
        {
            get { return strVenue; }
            set { strVenue = value; }
        }

        public string Mode
        {
            get { return strMode; }
            set { strMode = value; }
        }

        public int OrganizeBy
        {
            get { return intOrganizeBy; }
            set { intOrganizeBy = value; }
        }

        public string Status
        {
            get { return strStatus; }
            set { strStatus = value; }
        }

        public int MinutesBy
        {
            get { return intMinutesBy; }
            set { intMinutesBy = value; }
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

        #region ---Properties of MeetingAgenda Table---
        public int AgendaID
        {
            get { return intAgendaID; }
            set { intAgendaID = value; }
        }

        public string AgendaPoint
        {
            get { return strAgendaPoint; }
            set { strAgendaPoint = value; }
        }
        #endregion

        #region ---Properties of MeetingParticipants Table---
        public int ParticipantID
        {
            get { return intParticipantID; }
            set { intParticipantID = value; }
        }

        public int EmployeeID
        {
            get { return intEmployeeID; }
            set { intEmployeeID = value; }
        }

        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }

        public int OrgID
        {
            get { return intOrgID; }
            set { intOrgID = value; }
        }

        public string OrgName
        {
            get { return strOrgName; }
            set { strOrgName = value; }
        }

        public string Email
        {
            get { return strEmail; }
            set { strEmail = value; }
        }

        public int SendInvite
        {
            get { return intSendInvite; }
            set { intSendInvite = value; }
        }

        public int SendMOM
        {
            get { return intSendMOM; }
            set { intSendMOM = value; }
        }

        public int Absent
        {
            get { return intAbsent; }
            set { intAbsent = value; }
        }
        #endregion
    }

}
