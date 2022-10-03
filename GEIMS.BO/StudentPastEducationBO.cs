using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class StudentPastEducationBO
    {
        #region StudentPastEducation Class Properties

        public const string STUDENTPASTEDUCATION_TABLE = "tbl_StudentPreEducationDetail_T";
        public const string STUDENTPASTEDUCATION_STUDENTEDUCATIONDETAILTID = "StudentEducationDetailTID";
        public const string STUDENTPASTEDUCATION_STUDENTMID = "StudentMID";
        public const string STUDENTPASTEDUCATION_SCHOOLNAME = "SchoolName";
        public const string STUDENTPASTEDUCATION_ADDRESS = "Address";
        public const string STUDENTPASTEDUCATION_MEDIUMNAME = "MediumName";
        public const string STUDENTPASTEDUCATION_PASSEDEXAM = "PassedExam";
        public const string STUDENTPASTEDUCATION_BOARDNAME = "BoardName";
        public const string STUDENTPASTEDUCATION_PASSINGYEAR = "PassingYear";
        public const string STUDENTPASTEDUCATION_ISDELETED = "IsDeleted";
        public const string STUDENTPASTEDUCATION_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string STUDENTPASTEDUCATION_LASTMODIFIEDDATE = "LastModifiedDate";



        private int intStudentEducationDetailTID = 0;
        private int intStudentMID = 0;
        private string strSchoolName = string.Empty;
        private string strAddress = string.Empty;
        private string strMediumName = string.Empty;
        private string strPassedExam = string.Empty;
        private string strBoardName = string.Empty;
        private string strPassingYear = string.Empty;
        private int intIsDeleted = 0;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;

        #endregion

        #region ---Properties---
        public int StudentEducationDetailTID
        {
            get { return intStudentEducationDetailTID; }
            set { intStudentEducationDetailTID = value; }
        }
        public int StudentMID
        {
            get { return intStudentMID; }
            set { intStudentMID = value; }
        }
        public string SchoolName
        {
            get { return strSchoolName; }
            set { strSchoolName = value; }
        }
        public string Address
        {
            get { return strAddress; }
            set { strAddress = value; }
        }
        public string MediumName
        {
            get { return strMediumName; }
            set { strMediumName = value; }
        }
        public string PassedExam
        {
            get { return strPassedExam; }
            set { strPassedExam = value; }
        }
        public string BoardName
        {
            get { return strBoardName; }
            set { strBoardName = value; }
        }
        public string PassingYear
        {
            get { return strPassingYear; }
            set { strPassingYear = value; }
        }
        public int IsDeleted
        {
            get { return intIsDeleted; }
            set { intIsDeleted = value; }
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



