namespace GEIMS.BO
{
    public class DocumentBO
    {

        #region Document Class Properties

        public const string DOCUMENT_TABLE = "tbl_Document_M";
        public const string DOCUMENT_DOCUMENTMID = "DocumentMID";
        public const string DOCUMENT_TRUSTMID = "TrustMID";
        public const string DOCUMENT_SCHOOLMID = "SchoolMID";
        public const string DOCUMENT_STUDENTMID = "StudentMID";
        public const string DOCUMENT_EMPLOYEEMID = "EmployeeMID";
        public const string DOCUMENT_DOCUMENTNAME = "DocumentName";
        public const string DOCUMENT_DOCUMENTPATH = "DocumentPath";
        public const string DOCUMENT_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string DOCUMENT_LASTMODIFIEDDATE = "LastModifiedDate";
        public const string DOCUMENT_ISDELETED = "IsDeleted";



        private int intDocumentMID = 0;
        private int intTrustMID = 0;
        private int intSchoolMID = 0;
        private int intStudentMID = 0;
        private int intEmployeeMID = 0;
        private string strDocumentName = string.Empty;
        private string strDocumentPath = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;
        private int intIsDeleted = 0;

        #endregion

        #region ---Properties---
        public int DocumentMID
        {
            get { return intDocumentMID; }
            set { intDocumentMID = value; }
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
        public int StudentMID
        {
            get { return intStudentMID; }
            set { intStudentMID = value; }
        }
        public int EmployeeMID
        {
            get { return intEmployeeMID; }
            set { intEmployeeMID = value; }
        }
        public string DocumentName
        {
            get { return strDocumentName; }
            set { strDocumentName = value; }
        }
        public string DocumentPath
        {
            get { return strDocumentPath; }
            set { strDocumentPath = value; }
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



