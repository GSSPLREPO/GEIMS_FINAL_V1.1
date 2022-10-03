namespace GEIMS.BO
{
	public class SectionBO
	{
        #region Section Class Properties

        public const string SECTION_TABLE = "tbl_Section_M";
        public const string SECTION_SECTIONMID = "SectionMID";
        public const string SECTION_SECTIONNAME = "SectionName";
        public const string SECTION_SECTIONAVBBREVIATION = "SectionAvbbreviation";
        public const string SECTION_DESCRIPTION = "Description";
        public const string SECTION_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string SECTION_LASTMODIFIEDDATE = "LastModifiedDate";
        public const string SECTION_ISDELETED = "IsDeleted";



        private int intSectionMID = 0;
        private string strSectionName = string.Empty;
        private string strSectionAvbbreviation = string.Empty;
        private string strDescription = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;
        private int intIsDeleted = 0;

        #endregion

        #region ---Properties---
        public int SectionMID
        {
            get { return intSectionMID; }
            set { intSectionMID = value; }
        }
        public string SectionName
        {
            get { return strSectionName; }
            set { strSectionName = value; }
        }
        public string SectionAvbbreviation
        {
            get { return strSectionAvbbreviation; }
            set { strSectionAvbbreviation = value; }
        }
        public string Description
        {
            get { return strDescription; }
            set { strDescription = value; }
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
        public int IsDeleted
        {
            get { return intIsDeleted; }
            set { intIsDeleted = value; }
        }

        #endregion
    }
}


