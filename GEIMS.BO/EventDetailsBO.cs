using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class EventDetailsBO
    {
        #region EventCategory Class Properties

        public const string EVENTDETAILSIMAGES_TABLE = "tbl_EventDetailsImages";
        public const string EVENTDETAILSIMAGES_EVENTDETAILSIMAGEID = "EventDetailsImageID";
        public const string EVENTDETAILSIMAGES_SCHEDULEDEVENTID = "ScheduledEventID";
        public const string EVENTDETAILSIMAGES_IMAGENAME = "ImageName";

        private int intEventDetailsImageID = 0;
        private int intScheduledEventID = 0;
        private string strImageName = string.Empty;



        #endregion


        #region ---Properties---

        public int EventDetailsImageID
        {
            get { return intEventDetailsImageID; }
            set { intEventDetailsImageID = value; }
        }
        public int ScheduledEventID
        {
            get { return intScheduledEventID; }
            set { intScheduledEventID = value; }
        }

        public string ImageName
        {
            get { return strImageName; }
            set { strImageName = value; }
        }

        #endregion
    }
}
