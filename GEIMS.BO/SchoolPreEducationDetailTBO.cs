using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
   public class SchoolPreEducationDetailTBO
   {
    #region SchoolPreEducationDetail Class Properties

        public const string SCHOOLPREEDUCATIONDETAIL_TABLE = "tbl_StudentPreEducationDetail_T";
        public const string SCHOOLPREEDUCATIONDETAIL_STUDENTEDUCATIONDETAILTID = "StudentEducationDetailTID";
        public const string SCHOOLPREEDUCATIONDETAIL_STUDENTMID = "StudentMID";
        public const string SCHOOLPREEDUCATIONDETAIL_SCHOOLMID = "SchoolMID";
        public const string SCHOOLPREEDUCATIONDETAIL_SCHOOLNAME = "SchoolName";
        public const string SCHOOLPREEDUCATIONDETAIL_ADDRESS = "Address";
        public const string SCHOOLPREEDUCATIONDETAIL_MEDIUMNAME = "MediumName";
        public const string SCHOOLPREEDUCATIONDETAIL_PASSEDEXAM = "PassedExam";
        public const string SCHOOLPREEDUCATIONDETAIL_BOARDNAME = "BoardName";

        public const string SCHOOLPREEDUCATIONDETAIL_PASSINGYEAR = "PassingYear";
        public const string SCHOOLPREEDUCATIONDETAIL_ISDELETED = "IsDeleted";
        public const string SCHOOLPREEDUCATIONDETAIL_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string SCHOOLPREEDUCATIONDETAIL_LASTMODIFIEDDATE = "LastModifiedDate";
       
        private int intStudentEducationDetailTID = 0;
        private int intStudentMID = 0;
        private int intSchoolMID = 0;
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
        public int SchoolMID
        {
            get { return intSchoolMID; }
            set { intSchoolMID = value; }
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





