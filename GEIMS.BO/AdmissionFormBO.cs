using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;


namespace GEIMS.BO
{
    public class AdmissionFormBO
    {
        #region AdmissionForm Properties
        public const string ADDMISSIONFORM_TABLE = "tbl_AdmissionForm_M";
        public const string ADMISSIONFORM_ADMISSIONFORMMID = "AdmissionFormMID";
        public const string ADMISSIONFORM_ADMISSIONFORMNO = "AdmissionFormNo";
        public const string ADMISSIONFORM_SCHOOLMID = "SchoolMID";
        public const string ADMISSIONFORM_SECTIONID = "SectionID";
       // public const string ADMISSIONFORM_LANGUAGE = "Language";
        public const string SYLLABUS_YEAR = "Year";
        public const string SYLLABUS_CREATEDBYID = "CreatedByID";
        public const string SYLLABUS_CREATEDDATE = "CreatedDate";


        private int intAdmissionFormMID = 0;
        private string strAdmissionFormNo = string.Empty;
        private int intSchoolMID = 0;
        private int intSectionID = 0;
        //private string strLanguage = string.Empty;
        private string strYear = string.Empty;
        private int intCreatedByID = 0;
        private string strCreatedDate = string.Empty;
      
        #endregion

        #region ---Properties---
        public int AdmissionFormMID
        {
            get { return intAdmissionFormMID; }
            set { intAdmissionFormMID = value; }
        }
        public string AdmissionFormNo
        {
            get { return strAdmissionFormNo; }
            set { strAdmissionFormNo = value; }
        }
        public int SchoolMID
        {
            get { return intSchoolMID; }
            set { intSchoolMID = value; }
        }
        
        public int SectionID
        {
            get { return intSectionID; }
            set { intSectionID = value; }
        }

        //public string Language
        //{
        //    get { return strLanguage; }
        //    set { strLanguage = value; }
        //}

        public string Year
        {
            get { return strYear; }
            set { strYear = value; }
        }
     
        public string CreatedDate
        {
            get { return strCreatedDate; }
            set { strCreatedDate = value; }
        }
        
        public int CreatedByID
        {
            get { return intCreatedByID; }
            set { intCreatedByID = value; }
        }
       
     

      
       

        #endregion

    }
}
