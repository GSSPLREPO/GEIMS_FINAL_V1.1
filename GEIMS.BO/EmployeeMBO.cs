using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
    public class EmployeeMBO
    {
        #region EmployeeM Class Properties

        public const string EMPLOYEEM_TABLE = "tbl_Employee_M";
        public const string EMPLOYEEM_EMPLOYEEMID = "EmployeeMID";
        public const string EMPLOYEEM_TRUSTMID = "TrustMID";
        public const string EMPLOYEEM_SCHOOLMID = "SchoolMID";
        public const string EMPLOYEEM_DEPARTMENTID = "DepartmentID";
        public const string EMPLOYEEM_DESIGNATIONID = "DesignationID";
        public const string EMPLOYEEM_SECTIONID = "SectionID";
        public const string EMPLOYEEM_EMPLOYEECODE = "EmployeeCode";
        public const string EMPLOYEEM_EMPLOYEEFNAMEENG = "EmployeeFNameENG";
        public const string EMPLOYEEM_EMPLOYEEFNAMEGUJ = "EmployeeFNameGUJ";
        public const string EMPLOYEEM_EMPLOYEEMNAMEENG = "EmployeeMNameENG";
        public const string EMPLOYEEM_EMPLOYEEMNAMEGUJ = "EmployeeMNameGUJ";
        public const string EMPLOYEEM_EMPLOYEELNAMEENG = "EmployeeLNameENG";
        public const string EMPLOYEEM_EMPLOYEELNAMEGUJ = "EmployeeLNameGUJ";
        public const string EMPLOYEEM_PHOTO = "Photo";
        public const string EMPLOYEEM_GENDER = "Gender";
        public const string EMPLOYEEM_GENDERGUJ = "GenderGuj";
        public const string EMPLOYEEM_DATEOFBIRTH = "DateOfBirth";
        public const string EMPLOYEEM_BIRTHDISTRICTENG = "BirthDistrictENG";
        public const string EMPLOYEEM_BIRTHDISTRICTGUJ = "BirthDistrictGUJ";
        public const string EMPLOYEEM_BIRTHTALUKAENG = "BirthTalukaENG";
        public const string EMPLOYEEM_BIRTHTALUKAGUJ = "BirthTalukaGUJ";
        public const string EMPLOYEEM_BIRTHCITYVILLAGEENG = "BirthCityVillageENG";
        public const string EMPLOYEEM_BIRTHCITYVILLAGEGUJ = "BirthCityVillageGUJ";
        public const string EMPLOYEEM_NATIONALITYENG = "NationalityENG";
        public const string EMPLOYEEM_NATIONALITYGUJ = "NationalityGUJ";
        public const string EMPLOYEEM_RELIGIONENG = "ReligionENG";
        public const string EMPLOYEEM_RELIGIONGUJ = "ReligionGUJ";
        public const string EMPLOYEEM_CASTE = "Caste";
        public const string EMPLOYEEM_MARITALSTATUS = "MaritalStatus";
        public const string EMPLOYEEM_BLOODGROUP = "BloodGroup";
        public const string EMPLOYEEM_MOTHERLANGUAGE = "MotherLanguage";
        public const string EMPLOYEEM_CURRENTADDRESSENG = "CurrentAddressENG";
        public const string EMPLOYEEM_CURRENTADDRESSGUJ = "CurrentAddressGUJ";
        public const string EMPLOYEEM_CURRENTLANDMARKENG = "CurrentLandmarkENG";
        public const string EMPLOYEEM_CURRENTLANDMARKGUJ = "CurrentLandmarkGUJ";
        public const string EMPLOYEEM_CURRENTCITYENG = "CurrentCityENG";
        public const string EMPLOYEEM_CURRENTCITYGUJ = "CurrentCityGUJ";
        public const string EMPLOYEEM_CURRENTSTATEENG = "CurrentStateENG";
        public const string EMPLOYEEM_CURRENTSTATEGUJ = "CurrentStateGUJ";
        public const string EMPLOYEEM_CURRENTPINCODE = "CurrentPinCode";
        public const string EMPLOYEEM_PERMENANTADDRESSENG = "PermenantAddressEng";
        public const string EMPLOYEEM_PERMENANTADDRESSGUJ = "PermenantAddressGuj";
        public const string EMPLOYEEM_PERMENANTLANDMARKENG = "PermenantLandmarkEng";
        public const string EMPLOYEEM_PERMENANTLANDMARKGUJ = "PermenantLandmarkGuj";
        public const string EMPLOYEEM_PERMENANTCITYENG = "PermenantCityEng";
        public const string EMPLOYEEM_PERMENANTCITYGUJ = "PermenantCityGuj";
        public const string EMPLOYEEM_PERMENANTSTATEENG = "PermenantStateEng";
        public const string EMPLOYEEM_PERMENANTSTATEGUJ = "PermenantStateGuj";
        public const string EMPLOYEEM_PERMENANTPINCODE = "PermenantPincode";
        public const string EMPLOYEEM_TELEPHONENO = "TelephoneNo";
        public const string EMPLOYEEM_MOBILENO = "MobileNo";
        public const string EMPLOYEEM_EMAILID = "EmailId";
        public const string EMPLOYEEM_HOBBIES = "Hobbies";
        public const string EMPLOYEEM_RIGHTVISION = "RightVision";
        public const string EMPLOYEEM_LEFTVISION = "LeftVision";
        public const string EMPLOYEEM_RECTIFICATIONDEVICE = "RectificationDevice";
        public const string EMPLOYEEM_HEIGHT = "Height";
        public const string EMPLOYEEM_WEIGHT = "Weight";
        public const string EMPLOYEEM_PHYSICALIDENTIFICATIONENG = "PhysicalIdentificationENG";
        public const string EMPLOYEEM_PHYSICALIDENTIFICATIONGUJ = "PhysicalIdentificationGUJ";
        public const string EMPLOYEEM_MOTHERMAIDENFNAMEENG = "MotherMaidenFNameENG";
        public const string EMPLOYEEM_MOTHERMAIDENFNAMEGUJ = "MotherMaidenFNameGUJ";
        public const string EMPLOYEEM_MOTHERMAIDENMNAMEENG = "MotherMaidenMNameENG";
        public const string EMPLOYEEM_MOTHERMAIDENMNAMEGUJ = "MotherMaidenMNameGUJ";
        public const string EMPLOYEEM_MOTHERMAIDENLNAMEENG = "MotherMaidenLNameENG";
        public const string EMPLOYEEM_MOTHERMAIDENLNAMEGUJ = "MotherMaidenLNameGUJ";
        public const string EMPLOYEEM_BANKNAME = "BankName";
        public const string EMPLOYEEM_BRANCHNAME = "BranchName";
        public const string EMPLOYEEM_BRANCHCODE = "BranchCode";
        public const string EMPLOYEEM_ACCOUNTNO = "AccountNo";
        public const string EMPLOYEEM_PFNO = "PFNo";
        public const string EMPLOYEEM_PANNO = "PANNo";
        public const string EMPLOYEEM_ESICNO = "ESICNo";
        public const string EMPLOYEEM_IFSCCODE = "IFSCCode";
        public const string EMPLOYEEM_GPFACCOUNTNO = "GPFAccountNo";
        public const string EMPLOYEEM_CPFACCOUNTNO = "CPFAccountNo";
        public const string EMPLOYEEM_DEPARTMENTJOININGDATE = "DepartmentJoiningDate";
        public const string EMPLOYEEM_ORGANISATIONJOININGDATE = "OrganisationJoiningDate";
        public const string EMPLOYEEM_TYPEOFAPPOINTMENT = "TypeOfAppointment";
        public const string EMPLOYEEM_REPLACEMENTSCHOOLINFOENG = "ReplacementSchoolInfoENG";
        public const string EMPLOYEEM_REPLACEMENTSCHOOLINFOGUJ = "ReplacementSchoolInfoGUJ";
        public const string EMPLOYEEM_RETIREMENTDATE = "RetirementDate";
        public const string EMPLOYEEM_TERMENDRETIREMENTDATE = "TermEndRetirementDate";
        public const string EMPLOYEEM_ISRESIGNED = "IsResigned";
        public const string EMPLOYEEM_RESIGNEDDATE = "ResignedDate";
        public const string EMPLOYEEM_BREAKINFOENG = "BreakInfoENG";
        public const string EMPLOYEEM_BREAKINFOGUJ = "BreakInfoGUJ";
        public const string EMPLOYEEM_RESIGNREASONENG = "ResignReasonEng";
        public const string EMPLOYEEM_RESIGNREASONGUJ = "ResignReasonGuj";
        public const string EMPLOYEEM_OTHERACHIVEMENTDETAILSENG = "OtherAchivementDetailsENG";
        public const string EMPLOYEEM_OTHERACHIVEMENTDETAILSGUJ = "OtherAchivementDetailsGUJ";
        public const string EMPLOYEEM_ISUSER = "IsUser";
        public const string EMPLOYEEM_USERNAME = "UserName";
        public const string EMPLOYEEM_PASSWORD = "Password";
        public const string EMPLOYEEM_ROLEID = "RoleID";
        public const string EMPLOYEEM_ISTEACHER = "IsTeacher";
        public const string EMPLOYEEM_ISPRINCIPAL = "IsPrincipal";
        public const string EMPLOYEEM_ISDELETED = "IsDeleted";
        public const string EMPLOYEEM_CREATEDUSERID = "CreatedUserID";
        public const string EMPLOYEEM_CREATEDDATE = "CreatedDate";
        public const string EMPLOYEEM_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string EMPLOYEEM_LASTMODIFIEDDATE = "LastModifiedDate";
        public const string EMPLOYEEM_ALLOWACCOUNTACCESS = "AllowAccountAccess";
        public const string EMPLOYEEM_CATEGORYENG = "CategoryEng";
        public const string EMPLOYEEM_CATEGORYGUJ = "CategoryGuj";
        public const string EMPLOYEEM_ReportingTo = "ReportingTo";
        public const string EMPLOYEEM_TypeOfAppointmentCode = "TypeOfAppointmentCode";
        public const string EMPLOYEEM_AaharCardNo = "AaharCardNo";
        public const string EMPLOYEEM_ElectionCardNo = "ElectionCardNo";
        public const string EMPLOYEEM_VehicleNo = "VehicleNo";
        public const string EMPLOYEEM_PortalID = "PortalID";
        public const string EMPLOYEEM_PRANNo = "PRANNo";
        public const string EMPLOYEEM_TANNO = "TANNO";
        public const string EMPLOYEEM_LASTWORKINGDATE = "LASTWORKINGDATE";





        private int intEmployeeMID = 0;
        private int intTrustMID = 0;
        private int intSchoolMID = 0;
        private int intDepartmentID = 0;
        private int intDesignationID = 0;
        private int intSectionID = 0;
        private int intReportingTo = 0;
        private string strEmployeeCode = string.Empty;
        private string strEmployeeFNameENG = string.Empty;
        private string strEmployeeFNameGUJ = string.Empty;
        private string strEmployeeMNameENG = string.Empty;
        private string strEmployeeMNameGUJ = string.Empty;
        private string strEmployeeLNameENG = string.Empty;
        private string strEmployeeLNameGUJ = string.Empty;
        private byte[] bytePhoto = { };
        private string strGender = string.Empty;
        private string strGenderGuj = string.Empty;
        private string strDateOfBirth = string.Empty;
        private string strBirthDistrictENG = string.Empty;
        private string strBirthDistrictGUJ = string.Empty;
        private string strBirthTalukaENG = string.Empty;
        private string strBirthTalukaGUJ = string.Empty;
        private string strBirthCityVillageENG = string.Empty;
        private string strBirthCityVillageGUJ = string.Empty;
        private string strNationalityENG = string.Empty;
        private string strNationalityGUJ = string.Empty;
        private string strReligionENG = string.Empty;
        private string strReligionGUJ = string.Empty;
        private string strCaste = string.Empty;
        private string strMaritalStatus = string.Empty;
        private string strBloodGroup = string.Empty;
        private string strMotherLanguage = string.Empty;
        private string strCurrentAddressENG = string.Empty;
        private string strCurrentAddressGUJ = string.Empty;
        private string strCurrentLandmarkENG = string.Empty;
        private string strCurrentLandmarkGUJ = string.Empty;
        private string strCurrentCityENG = string.Empty;
        private string strCurrentCityGUJ = string.Empty;
        private string strCurrentStateENG = string.Empty;
        private string strCurrentStateGUJ = string.Empty;
        private string strCurrentPinCode = string.Empty;
        private string strPermenantAddressEng = string.Empty;
        private string strPermenantAddressGuj = string.Empty;
        private string strPermenantLandmarkEng = string.Empty;
        private string strPermenantLandmarkGuj = string.Empty;
        private string strPermenantCityEng = string.Empty;
        private string strPermenantCityGuj = string.Empty;
        private string strPermenantStateEng = string.Empty;
        private string strPermenantStateGuj = string.Empty;
        private string strPermenantPincode = string.Empty;
        private string strTelephoneNo = string.Empty;
        private string strMobileNo = string.Empty;
        private string strEmailId = string.Empty;
        private string strHobbies = string.Empty;
        private string strRightVision = string.Empty;
        private string strLeftVision = string.Empty;
        private string strRectificationDevice = string.Empty;
        private string strHeight = string.Empty;
        private string strWeight = string.Empty;
        private string strPhysicalIdentificationENG = string.Empty;
        private string strPhysicalIdentificationGUJ = string.Empty;
        private string strMotherMaidenFNameENG = string.Empty;
        private string strMotherMaidenFNameGUJ = string.Empty;
        private string strMotherMaidenMNameENG = string.Empty;
        private string strMotherMaidenMNameGUJ = string.Empty;
        private string strMotherMaidenLNameENG = string.Empty;
        private string strMotherMaidenLNameGUJ = string.Empty;
        private string strBankName = string.Empty;
        private string strBranchName = string.Empty;
        private string strBranchCode = string.Empty;
        private string strAccountNo = string.Empty;
        private string strPFNo = string.Empty;
        private string strPANNo = string.Empty;
        private string strESICNo = string.Empty;
        private string strIFSCCode = string.Empty;
        private string strGPFAccountNo = string.Empty;
        private string strCPFAccountNo = string.Empty;
        private string strDepartmentJoiningDate = string.Empty;
        private string strOrganisationJoiningDate = string.Empty;
        private string strTypeOfAppointment = string.Empty;
        private string strReplacementSchoolInfoENG = string.Empty;
        private string strReplacementSchoolInfoGUJ = string.Empty;
        private string strRetirementDate = string.Empty;
        private string strTermEndRetirementDate = string.Empty;
        private int intIsResigned = 0;
        private string strResignedDate = string.Empty;
        private string strBreakInfoENG = string.Empty;
        private string strBreakInfoGUJ = string.Empty;
        private string strResignReasonEng = string.Empty;
        private string strResignReasonGuj = string.Empty;
        private string strOtherAchivementDetailsENG = string.Empty;
        private string strOtherAchivementDetailsGUJ = string.Empty;
        private int intIsUser = 0;
        private string strUserName = string.Empty;
        private string strPassword = string.Empty;
        private int intRoleID = 0;
        private int intIsTeacher = 0;
        private int intIsPrincipal = 0;
        private int intIsDeleted = 0;
        private int intCreatedUserID = 0;
        private string strCreatedDate = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;
        private int intAllowAccountAccess = 0;
        private string strCategoryEng = string.Empty;
        private string strCategoryGuj = string.Empty;

        private string strTypeOfAppointmentCode = string.Empty;
        private string strAaharCardNo = string.Empty;
        private string strElectionCardNo = string.Empty;
        private string strVehicleNo = string.Empty;
        private string strPortalID = string.Empty;
        private string strPRANNo = string.Empty;
        private string strTANNO = string.Empty;
        

        private string strLASTWORKINGDATE = string.Empty;


        #endregion

        #region ---Properties---
        public int EmployeeMID
        {
            get { return intEmployeeMID; }
            set { intEmployeeMID = value; }
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
        public int DepartmentID
        {
            get { return intDepartmentID; }
            set { intDepartmentID = value; }
        }
        public int DesignationID
        {
            get { return intDesignationID; }
            set { intDesignationID = value; }
        }
        public int SectionID
        {
            get { return intSectionID; }
            set { intSectionID = value; }
        }
        public int ReportingTo
        {
            get { return intReportingTo; }
            set { intReportingTo = value; }
        }
        public string EmployeeCode
        {
            get { return strEmployeeCode; }
            set { strEmployeeCode = value; }
        }
        public string EmployeeFNameENG
        {
            get { return strEmployeeFNameENG; }
            set { strEmployeeFNameENG = value; }
        }
        public string EmployeeFNameGUJ
        {
            get { return strEmployeeFNameGUJ; }
            set { strEmployeeFNameGUJ = value; }
        }
        public string EmployeeMNameENG
        {
            get { return strEmployeeMNameENG; }
            set { strEmployeeMNameENG = value; }
        }
        public string EmployeeMNameGUJ
        {
            get { return strEmployeeMNameGUJ; }
            set { strEmployeeMNameGUJ = value; }
        }
        public string EmployeeLNameENG
        {
            get { return strEmployeeLNameENG; }
            set { strEmployeeLNameENG = value; }
        }
        public string EmployeeLNameGUJ
        {
            get { return strEmployeeLNameGUJ; }
            set { strEmployeeLNameGUJ = value; }
        }
        public byte[] Photo
        {
            get { return bytePhoto; }
            set { bytePhoto = value; }
        }
        public string Gender
        {
            get { return strGender; }
            set { strGender = value; }
        }
        public string GenderGuj
        {
            get { return strGenderGuj; }
            set { strGenderGuj = value; }
        }
        public string DateOfBirth
        {
            get { return strDateOfBirth; }
            set { strDateOfBirth = value; }
        }
        public string BirthDistrictENG
        {
            get { return strBirthDistrictENG; }
            set { strBirthDistrictENG = value; }
        }
        public string BirthDistrictGUJ
        {
            get { return strBirthDistrictGUJ; }
            set { strBirthDistrictGUJ = value; }
        }
        public string BirthTalukaENG
        {
            get { return strBirthTalukaENG; }
            set { strBirthTalukaENG = value; }
        }
        public string BirthTalukaGUJ
        {
            get { return strBirthTalukaGUJ; }
            set { strBirthTalukaGUJ = value; }
        }
        public string BirthCityVillageENG
        {
            get { return strBirthCityVillageENG; }
            set { strBirthCityVillageENG = value; }
        }
        public string BirthCityVillageGUJ
        {
            get { return strBirthCityVillageGUJ; }
            set { strBirthCityVillageGUJ = value; }
        }
        public string NationalityENG
        {
            get { return strNationalityENG; }
            set { strNationalityENG = value; }
        }
        public string NationalityGUJ
        {
            get { return strNationalityGUJ; }
            set { strNationalityGUJ = value; }
        }
        public string ReligionENG
        {
            get { return strReligionENG; }
            set { strReligionENG = value; }
        }
        public string ReligionGUJ
        {
            get { return strReligionGUJ; }
            set { strReligionGUJ = value; }
        }
        public string Caste
        {
            get { return strCaste; }
            set { strCaste = value; }
        }
        public string MaritalStatus
        {
            get { return strMaritalStatus; }
            set { strMaritalStatus = value; }
        }
        public string BloodGroup
        {
            get { return strBloodGroup; }
            set { strBloodGroup = value; }
        }
        public string MotherLanguage
        {
            get { return strMotherLanguage; }
            set { strMotherLanguage = value; }
        }
        public string CurrentAddressENG
        {
            get { return strCurrentAddressENG; }
            set { strCurrentAddressENG = value; }
        }
        public string CurrentAddressGUJ
        {
            get { return strCurrentAddressGUJ; }
            set { strCurrentAddressGUJ = value; }
        }
        public string CurrentLandmarkENG
        {
            get { return strCurrentLandmarkENG; }
            set { strCurrentLandmarkENG = value; }
        }
        public string CurrentLandmarkGUJ
        {
            get { return strCurrentLandmarkGUJ; }
            set { strCurrentLandmarkGUJ = value; }
        }
        public string CurrentCityENG
        {
            get { return strCurrentCityENG; }
            set { strCurrentCityENG = value; }
        }
        public string CurrentCityGUJ
        {
            get { return strCurrentCityGUJ; }
            set { strCurrentCityGUJ = value; }
        }
        public string CurrentStateENG
        {
            get { return strCurrentStateENG; }
            set { strCurrentStateENG = value; }
        }
        public string CurrentStateGUJ
        {
            get { return strCurrentStateGUJ; }
            set { strCurrentStateGUJ = value; }
        }
        public string CurrentPinCode
        {
            get { return strCurrentPinCode; }
            set { strCurrentPinCode = value; }
        }
        public string PermenantAddressEng
        {
            get { return strPermenantAddressEng; }
            set { strPermenantAddressEng = value; }
        }
        public string PermenantAddressGuj
        {
            get { return strPermenantAddressGuj; }
            set { strPermenantAddressGuj = value; }
        }
        public string PermenantLandmarkEng
        {
            get { return strPermenantLandmarkEng; }
            set { strPermenantLandmarkEng = value; }
        }
        public string PermenantLandmarkGuj
        {
            get { return strPermenantLandmarkGuj; }
            set { strPermenantLandmarkGuj = value; }
        }
        public string PermenantCityEng
        {
            get { return strPermenantCityEng; }
            set { strPermenantCityEng = value; }
        }
        public string PermenantCityGuj
        {
            get { return strPermenantCityGuj; }
            set { strPermenantCityGuj = value; }
        }
        public string PermenantStateEng
        {
            get { return strPermenantStateEng; }
            set { strPermenantStateEng = value; }
        }
        public string PermenantStateGuj
        {
            get { return strPermenantStateGuj; }
            set { strPermenantStateGuj = value; }
        }
        public string PermenantPincode
        {
            get { return strPermenantPincode; }
            set { strPermenantPincode = value; }
        }
        public string TelephoneNo
        {
            get { return strTelephoneNo; }
            set { strTelephoneNo = value; }
        }
        public string MobileNo
        {
            get { return strMobileNo; }
            set { strMobileNo = value; }
        }
        public string EmailId
        {
            get { return strEmailId; }
            set { strEmailId = value; }
        }
        public string Hobbies
        {
            get { return strHobbies; }
            set { strHobbies = value; }
        }
        public string RightVision
        {
            get { return strRightVision; }
            set { strRightVision = value; }
        }
        public string LeftVision
        {
            get { return strLeftVision; }
            set { strLeftVision = value; }
        }
        public string RectificationDevice
        {
            get { return strRectificationDevice; }
            set { strRectificationDevice = value; }
        }
        public string Height
        {
            get { return strHeight; }
            set { strHeight = value; }
        }
        public string Weight
        {
            get { return strWeight; }
            set { strWeight = value; }
        }
        public string PhysicalIdentificationENG
        {
            get { return strPhysicalIdentificationENG; }
            set { strPhysicalIdentificationENG = value; }
        }
        public string PhysicalIdentificationGUJ
        {
            get { return strPhysicalIdentificationGUJ; }
            set { strPhysicalIdentificationGUJ = value; }
        }
        public string MotherMaidenFNameENG
        {
            get { return strMotherMaidenFNameENG; }
            set { strMotherMaidenFNameENG = value; }
        }
        public string MotherMaidenFNameGUJ
        {
            get { return strMotherMaidenFNameGUJ; }
            set { strMotherMaidenFNameGUJ = value; }
        }
        public string MotherMaidenMNameENG
        {
            get { return strMotherMaidenMNameENG; }
            set { strMotherMaidenMNameENG = value; }
        }
        public string MotherMaidenMNameGUJ
        {
            get { return strMotherMaidenMNameGUJ; }
            set { strMotherMaidenMNameGUJ = value; }
        }
        public string MotherMaidenLNameENG
        {
            get { return strMotherMaidenLNameENG; }
            set { strMotherMaidenLNameENG = value; }
        }
        public string MotherMaidenLNameGUJ
        {
            get { return strMotherMaidenLNameGUJ; }
            set { strMotherMaidenLNameGUJ = value; }
        }
        public string BankName
        {
            get { return strBankName; }
            set { strBankName = value; }
        }
        public string BranchName
        {
            get { return strBranchName; }
            set { strBranchName = value; }
        }
        public string BranchCode
        {
            get { return strBranchCode; }
            set { strBranchCode = value; }
        }
        public string AccountNo
        {
            get { return strAccountNo; }
            set { strAccountNo = value; }
        }
        public string PFNo
        {
            get { return strPFNo; }
            set { strPFNo = value; }
        }
        public string PANNo
        {
            get { return strPANNo; }
            set { strPANNo = value; }
        }
        public string ESICNo
        {
            get { return strESICNo; }
            set { strESICNo = value; }
        }
        public string IFSCCode
        {
            get { return strIFSCCode; }
            set { strIFSCCode = value; }
        }
        public string GPFAccountNo
        {
            get { return strGPFAccountNo; }
            set { strGPFAccountNo = value; }
        }
        public string CPFAccountNo
        {
            get { return strCPFAccountNo; }
            set { strCPFAccountNo = value; }
        }
        public string DepartmentJoiningDate
        {
            get { return strDepartmentJoiningDate; }
            set { strDepartmentJoiningDate = value; }
        }
        public string OrganisationJoiningDate
        {
            get { return strOrganisationJoiningDate; }
            set { strOrganisationJoiningDate = value; }
        }
        public string TypeOfAppointment
        {
            get { return strTypeOfAppointment; }
            set { strTypeOfAppointment = value; }
        }
        public string ReplacementSchoolInfoENG
        {
            get { return strReplacementSchoolInfoENG; }
            set { strReplacementSchoolInfoENG = value; }
        }
        public string ReplacementSchoolInfoGUJ
        {
            get { return strReplacementSchoolInfoGUJ; }
            set { strReplacementSchoolInfoGUJ = value; }
        }
        public string RetirementDate
        {
            get { return strRetirementDate; }
            set { strRetirementDate = value; }
        }
        public string TermEndRetirementDate
        {
            get { return strTermEndRetirementDate; }
            set { strTermEndRetirementDate = value; }
        }
        public int IsResigned
        {
            get { return intIsResigned; }
            set { intIsResigned = value; }
        }
        public string ResignedDate
        {
            get { return strResignedDate; }
            set { strResignedDate = value; }
        }
        public string BreakInfoENG
        {
            get { return strBreakInfoENG; }
            set { strBreakInfoENG = value; }
        }
        public string BreakInfoGUJ
        {
            get { return strBreakInfoGUJ; }
            set { strBreakInfoGUJ = value; }
        }
        public string ResignReasonEng
        {
            get { return strResignReasonEng; }
            set { strResignReasonEng = value; }
        }
        public string ResignReasonGuj
        {
            get { return strResignReasonGuj; }
            set { strResignReasonGuj = value; }
        }
        public string OtherAchivementDetailsENG
        {
            get { return strOtherAchivementDetailsENG; }
            set { strOtherAchivementDetailsENG = value; }
        }
        public string OtherAchivementDetailsGUJ
        {
            get { return strOtherAchivementDetailsGUJ; }
            set { strOtherAchivementDetailsGUJ = value; }
        }
        public int IsUser
        {
            get { return intIsUser; }
            set { intIsUser = value; }
        }
        public string UserName
        {
            get { return strUserName; }
            set { strUserName = value; }
        }
        public string Password
        {
            get { return strPassword; }
            set { strPassword = value; }
        }
        public int RoleID
        {
            get { return intRoleID; }
            set { intRoleID = value; }
        }
        public int IsTeacher
        {
            get { return intIsTeacher; }
            set { intIsTeacher = value; }
        }
        public int IsPrincipal
        {
            get { return intIsPrincipal; }
            set { intIsPrincipal = value; }
        }
        public int IsDeleted
        {
            get { return intIsDeleted; }
            set { intIsDeleted = value; }
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
        public int AllowAccountAccess
        {
            get { return intAllowAccountAccess; }
            set { intAllowAccountAccess = value; }
        }
        public string CategoryEng
        {
            get { return strCategoryEng; }
            set { strCategoryEng = value; }
        }
        public string CategoryGuj
        {
            get { return strCategoryGuj; }
            set { strCategoryGuj = value; }
        }

        public string TypeOfAppointmentCode
        {
            get { return strTypeOfAppointmentCode; }
            set { strTypeOfAppointmentCode = value; }
        }

        public string AaharCardNo
        {
            get { return strAaharCardNo; }
            set { strAaharCardNo = value; }
        }
        public string ElectionCardNo
        {
            get { return strElectionCardNo; }
            set { strElectionCardNo = value; }
        }

        public string VehicleNo
        {
            get { return strVehicleNo; }
            set { strVehicleNo = value; }
        }

        public string PortalID
        {
            get { return strPortalID; }
            set { strPortalID = value; }
        }
        public string PRANNo
        {
            get { return strPRANNo; }
            set { strPRANNo = value; }
        }
        public string TANNO
        {
            get { return strTANNO; }
            set { strTANNO = value; }
        }
        public string LASTWORKINGDATE
        {
            get { return strLASTWORKINGDATE; }
            set { strLASTWORKINGDATE = value; }
        }
        #endregion

    }
}


