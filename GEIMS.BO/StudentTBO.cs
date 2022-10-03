namespace GEIMS.BO
{
	public class StudentTBO
	{
        #region StudentT Class Properties

        public const string STUDENTT_TABLE = "tbl_Student_T";
        public const string STUDENTT_STUDENTTID = "StudentTID";
        public const string STUDENTT_STUDENTMID = "StudentMID";
        public const string STUDENTT_CLASSMID = "ClassMID";
        public const string STUDENTT_DIVISIONTID = "DivisionTID";
        public const string STUDENTT_STATUSMASTERID = "StatusMasterID";
        public const string STUDENTT_YEAR = "Year";
        public const string STUDENTT_STATUSNAME = "StatusName";
        public const string STUDENTT_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string STUDENTT_LASTMODIFIEDDATE = "LastModifiedDate";
        public const string STUDENTT_ISDELETED = "IsDeleted";
        public const string STUDENTT_GRNO = "GrNo";
        public const string STUDENTT_ISLATE = "IsLate";


        private int intStudentTID = 0;
        private int intStudentMID = 0;
        private int intClassMID = 0;
        private int intDivisionTID = 0;
        private int intStatusMasterID = 0;
        private string strYear = string.Empty;
        private string strStatusName = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;
        private int intIsDeleted = 0;
        private string strGrNo = string.Empty;
        private int intIsLate = 0;

        #endregion

        #region ---Properties---
        public int StudentTID
        {
            get { return intStudentTID; }
            set { intStudentTID = value; }
        }
        public int StudentMID
        {
            get { return intStudentMID; }
            set { intStudentMID = value; }
        }
        public int ClassMID
        {
            get { return intClassMID; }
            set { intClassMID = value; }
        }
        public int DivisionTID
        {
            get { return intDivisionTID; }
            set { intDivisionTID = value; }
        }
        public int StatusMasterID
        {
            get { return intStatusMasterID; }
            set { intStatusMasterID = value; }
        }
        public string Year
        {
            get { return strYear; }
            set { strYear = value; }
        }
        public string StatusName
        {
            get { return strStatusName; }
            set { strStatusName = value; }
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
        public string GrNo
        {
            get { return strGrNo; }
            set { strGrNo = value; }
        }
        public int IsLate
        {
            get { return intIsLate; }
            set { intIsLate = value; }
        }
        #endregion
	}
}


