using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class StudentPreEducationDetailTBO
    {
        #region StudentPastEducationDetailT Class Properties
        //Student Pre Education
        public const string STUDENTPASTEDUCATIONDETAILT_TABLE = "tbl_StudentPreEducationDetail_T";
        public const string STUDENTPASTEDUCATIONDETAILT_STUDENTEDUCATIONDETAILTID = "StudentEducationDetailTID";
        public const string STUDENTPASTEDUCATIONDETAILT_STUDENTMID = "StudentMID";
        public const string STUDENTPASTEDUCATIONDETAILT_SCHOOLMID = "SchoolMID";
        public const string STUDENTPASTEDUCATIONDETAILT_SCHOOLNAME = "SchoolName";
        public const string STUDENTPASTEDUCATIONDETAILT_ADDRESS = "Address";
        public const string STUDENTPASTEDUCATIONDETAILT_MEDIUMNAME = "MediumName";
        public const string STUDENTPASTEDUCATIONDETAILT_PASSEDEXAM = "PassedExam";
        public const string STUDENTPASTEDUCATIONDETAILT_BOARDNAME = "BoardName";
        public const string STUDENTPASTEDUCATIONDETAILT_PASSINGYEAR = "PassingYear";
        public const string STUDENTPASTEDUCATIONDETAILT_ISDELETED = "IsDeleted";
        public const string STUDENTPASTEDUCATIONDETAILT_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string STUDENTPASTEDUCATIONDETAILT_LASTMODIFIEDDATE = "LastModifiedDate";
        public const string STUDENTPASTEDUCATIONDETAILT_TOWN = "Town";
        public const string STUDENTPASTEDUCATIONDETAILT_TALUKA = "Taluka";
        public const string STUDENTPASTEDUCATIONDETAILT_DISTRICT = "District";
        public const string STUDENTPASTEDUCATIONDETAILT_STATE = "State";


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
        private string strTown = string.Empty;
        private string strTaluka = string.Empty;
        private string strDistrict = string.Empty;
        private string strState = string.Empty;

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

        public string Town
        {
            get { return strTown; }
            set { strTown = value; }
        }

        public string Taluka
        {
            get { return strTaluka; }
            set { strTaluka = value; }
        }

        public string District
        {
            get { return strDistrict; }
            set { strDistrict = value; }
        }

        public string State
        {
            get { return strState; }
            set { strState = value; }
        }
        #endregion
    }
}



