using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
 
    
    public class MeetingCategoryBO
    {
        #region MeetingCategory Class Properties

        public const string MEETINGCATEGORY_TABLE = "tbl_MeetingCategories";
        public const string MEETINGCATEGORY_CATEGORYID = "CategoryID";
        public const string MEETINGCATEGORY_TRUSTMID = "TrustMID";
        public const string MEETINGCATEGORY_CATEGORYNAME = "CategoryName";
        public const string MEETINGCATEGORY_CATEGORYDESCRIPTION = "CategoryDescription";
        public const string MEETINGCATEGORY_ISDELETED = "IsDelete";
        public const string MEETINGCATEGORY_CREATEDBYID = "CreatedByID";
        public const string MEETINGCATEGORY_CREATEDBYDATE = "CreatedByDate";
        public const string MEETINGCATEGORY_LASTMODIFIEDBYID = "LastModifiedByID";
        public const string MEETINGCATEGORY_LASTMODIFIEDBYDATE = "LastModifiedByDate";

        private int intCategoryID = 0;
        private int intTrustMID = 0;
        private string strCategoryName = string.Empty;
        private string strCategoryDescription = string.Empty;
        private int intIsDeleted = 0;
        private int intCreatedByID = 0;
        private string strCreatedByDate = string.Empty;
        private int intLastModifiedByID = 0;
        private string strLastModifiedByDate = string.Empty;

        #endregion


        #region ---Properties---

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
        public string CategoryName
        {
            get { return strCategoryName; }
            set { strCategoryName = value; }
        }

        public string CategoryDescription
        {
            get { return strCategoryDescription; }
            set { strCategoryDescription = value; }
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
