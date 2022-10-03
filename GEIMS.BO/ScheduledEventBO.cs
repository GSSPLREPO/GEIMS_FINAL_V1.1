using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class ScheduledEventBO
    {
        #region Scheduled Event Class Properties
        public const string SCHEDULEDEVENT_TABLE = "tbl_ScheduledEvent";
        public const string SCHEDULEDEVENT_SCHEDULEDEVENTID = "ScheduledEventID";
        public const string SCHEDULEDEVENT_TRUSTMID = "TrustMID";
        public const string SCHEDULEDEVENT_SCHOOLMID = "SchoolMID";
        public const string SCHEDULEDEVENT_SECTIONMID = "SectionMID";
        public const string SCHEDULEDEVENT_EVENTNAME = "EventName";
        public const string SCHEDULEDEVENT_EVENTCATEGORYID = "EventCategoryID";
        public const string SCHEDULEDEVENT_EVENTFROMDATE = "EventFromDate";
        public const string SCHEDULEDEVENT_EVENTFROMDATEFROMTIME = "EventFromDateFromTime";
        public const string SCHEDULEDEVENT_EVENTFROMDATETOTIME = "EventFromDateToTime";
        public const string SCHEDULEDEVENT_EVENTTODATE = "EventToDate";
        public const string SCHEDULEDEVENT_EVENTTODATEFROMTIME = "EventToDateFromTime";
        public const string SCHEDULEDEVENT_EVENTTODATETOTIME = "EventToDateToTime";
        public const string SCHEDULEDEVENT_EVENTPLATFORM = "EventPlatform";
        public const string SCHEDULEDEVENT_EVENTLOCATION = "EventLocation";
        public const string SCHEDULEDEVENT_EVENTDESCRIPTION = "EventDescription";
        public const string SCHEDULEDEVENT_EVENTDETAILSDESCRIPTION = "EventDetailsDescription";
        public const string SCHEDULEDEVENT_EVENTORGENISERNAME = "EventOrgeniserName";
        public const string SCHEDULEDEVENT_EVENTORGENISERSECTION = "EventOrgeniserSection";
        public const string SCHEDULEDEVENT_EVENTNOTE = "EventNote";
        public const string SCHEDULEDEVENT_EVENTMOBILENO = "EventMobileNo";
        public const string SCHEDULEDEVENT_EVENTEMAIL = "EventEmail";
        public const string SCHEDULEDEVENT_ISDELETED = "IsDelete";
        public const string SCHEDULEDEVENT_CREATEDUSERID = "CreatedUserID";
        public const string SCHEDULEDEVENT_CREATEDDATE = "CreatedDate";
        public const string SCHEDULEDEVENT_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string SCHEDULEDEVENT_LASTMODIFIEDDATE = "LastModifiedDate";


        private int intScheduledEventID = 0;
        private int intTrustMID = 0;
        private int intSchoolMID = 0;
        private int intSectionMID = 0;
        private string strEventName = string.Empty;
        private int intEventCategoryID = 0;
        private string strEventFromDate = string.Empty;
        private string strEventFromDateFromTime = string.Empty;
        private string strEventFromDateToTime = string.Empty;
        private string strEventToDate = string.Empty;
        private string strEventToDateFromTime = string.Empty;
        private string strEventToDateToTime = string.Empty;
        private string strEventPlatform = string.Empty;
        private string strEventLocation = string.Empty;
        private string strEventDescription = string.Empty;
        private string strEventDetailsDescription = string.Empty;
        private string strEventOrgeniserName = string.Empty;
        private string strEventOrgeniserSection = string.Empty;
        private string strEventNote = string.Empty;
        private string strEventMobileNo = string.Empty;
        private string strEventEmail = string.Empty;
        private int intIsDelete = 0;
        private int intCreatedUserID = 0;
        private string strCreatedDate = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;
        #endregion


        #region ---Properties---
        public int ScheduledEventID
        {
            get { return intScheduledEventID; }
            set { intScheduledEventID = value; }
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

        public int SectionMID
        {
            get { return intSectionMID; }
            set { intSectionMID = value; }
        }

        public string EventName
        {
            get { return strEventName; }
            set { strEventName = value; }
        }

        public int EventCategoryID
        {
            get { return intEventCategoryID; }
            set { intEventCategoryID = value; }
        }

        public string EventFromDate
        {
            get { return strEventFromDate; }
            set { strEventFromDate = value; }
        }

        public string EventFromDateFromTime
        {
            get { return strEventFromDateFromTime; }
            set { strEventFromDateFromTime = value; }
        }

        public string EventFromDateToTime
        {
            get { return strEventFromDateToTime; }
            set { strEventFromDateToTime = value; }
        }

        public string EventToDate
        {
            get { return strEventToDate; }
            set { strEventToDate = value; }
        }

        public string EventToDateFromTime
        {
            get { return strEventToDateFromTime; }
            set { strEventToDateFromTime=value; }
        }

        public string EventToDateToTime
        {
            get { return strEventToDateToTime; }
            set { strEventToDateToTime=value; }
        }

        public string EventPlatform
        {
            get { return strEventPlatform; }
            set { strEventPlatform = value; }
        }

        public string EventLocation
        {
            get { return strEventLocation; }
            set { strEventLocation = value; }
        }

        public string EventDescription
        {
            get { return strEventDescription; }
            set { strEventDescription = value; }
        }

        public string EventDetailsDescription
        {
            get { return strEventDetailsDescription; }
            set { strEventDetailsDescription = value; }
        }
        public string EventOrgeniserName
        {
            get { return strEventOrgeniserName; }
            set { strEventOrgeniserName = value; }
        }

        public string EventOrgeniserSection
        {
            get { return strEventOrgeniserSection; }
            set { strEventOrgeniserSection = value; }
        }

        public string EventNote
        {
            get { return strEventNote; }
            set { strEventNote = value; }
        }

        public string EventMobileNo
        {
            get { return strEventMobileNo; }
            set { strEventMobileNo = value; }
        }

        public string EventEmail
        {
            get { return strEventEmail; }
            set { strEventEmail = value; }
        }

        public int IsDelete
        {
            get { return intIsDelete; }
            set { intIsDelete = value; }
        }

        public int CreatedUserID
        {
            get { return intCreatedUserID; }
            set { intCreatedUserID = value; }
        }

        public string CreatedDate
        {
            get {return strCreatedDate; }
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
