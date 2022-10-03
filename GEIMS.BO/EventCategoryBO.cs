using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class EventCategoryBO
    {
        #region EventCategory Class Properties
        public const string EVENTCATEGORY_TABLE = "tbl_EventCategories";
        public const string EVENTCATEGORY_EVENTCATEGORYID = "EventCategoryID";
        public const string EVENTCATEGORY_EVENTCATEGORYNAME = "EventCategoryName";
        public const string EVENTCATEGORY_EVENTDESCRIPTION = "EventDescription";
        public const string EVENTCATEGORY_TRUSTMID = "TrustMID";
        public const string EVENTCATEGORY_ISDELETED = "IsDelete";
        public const string EVENTCATEGORY_CREATEDUSERID = "CreatedUserID";
        public const string EVENTCATEGORY_CREATEDDATE = "CreatedDate";
        public const string EVENTCATEGORY_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string EVENTCATEGORY_LASTMODIFIEDDATE = "LastModifiedDate";

        private int intEventCategoryID = 0;
        private string strEventCategoryName = string.Empty;
        private string strEventDescription = string.Empty;
        private int intTrustMID = 0;
        private int intISDeleted = 0;
        private int intCreatedUserID = 0;
        private string strCreatedDate = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;

        #endregion


        #region ---Properties---

        public int EventCategoryID
        {
            get { return intEventCategoryID; }
            set { intEventCategoryID = value; }
        }

        public string EventCategoryName
        {
            get { return strEventCategoryName; }
            set { strEventCategoryName = value; }
        }

        public string EventDescription
        {
            get { return strEventDescription; }
            set { strEventDescription = value; }
        }

        public int TrustMID
        {
            get { return intTrustMID; }
            set { intTrustMID = value; }
        }

        public int IsDeleted
        {
            get { return intISDeleted; }
            set { intISDeleted = value; }
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
