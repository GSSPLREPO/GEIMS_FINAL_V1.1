namespace GEIMS.BO
{
	public class StudentBO
	{
		#region Student Class Properties

        public const string STUDENT_TABLE = "tbl_Student_M";
        public const string STUDENT_STUDENTMID = "StudentMID";
        public const string STUDENT_SCHOOLMID = "SchoolMID";
        public const string STUDENT_STUDENTFIRSTNAMEENG = "StudentFirstNameEng";
        public const string STUDENT_STUDENTMIDDLENAMEENG = "StudentMiddleNameEng";
        public const string STUDENT_STUDENTLASTNAMEENG = "StudentLastNameEng";
        public const string STUDENT_STUDENTFIRSTNAMEGUJ = "StudentFirstNameGuj";
        public const string STUDENT_STUDENTMIDDLENAMEGUJ = "StudentMiddleNameGuj";
        public const string STUDENT_STUDENTLASTNAMEGUJ = "StudentLastNameGuj";
        public const string STUDENT_FATHERFIRSTNAMEENG = "FatherFirstNameEng";
        public const string STUDENT_FATHERFIRSTNAMEGUJ = "FatherFirstNameGuj";
        public const string STUDENT_FATHERMIDDLENAMEENG = "FatherMiddleNameEng";
        public const string STUDENT_FATHERMIDDLENAMEGUJ = "FatherMiddleNameGuj";
        public const string STUDENT_FATHERLASTNAMEENG = "FatherLastNameEng";
        public const string STUDENT_FATHERLASTNAMEGUJ = "FatherLastNameGuj";
        public const string STUDENT_MOTHERFIRSTNAMEENG = "MotherFirstNameEng";
        public const string STUDENT_MOTHERFIRSTNAMEGUJ = "MotherFirstNameGuj";
        public const string STUDENT_MOTHERMIDDLENAMEENG = "MotherMiddleNameEng";
        public const string STUDENT_MOTHERMIDDLENAMEGUJ = "MotherMiddleNameGuj";
        public const string STUDENT_MOTHERLASTNAMEENG = "MotherLastNameEng";
        public const string STUDENT_MOTHERLASTNAMEGUJ = "MotherLastNameGuj";
        public const string STUDENT_ADMISSIONNO = "AdmissionNo";
        public const string STUDENT_CURRENTDATE = "CurrentDate";
        public const string STUDENT_JOININGDATE = "JoiningDate";
        public const string STUDENT_CURRENTYEAR = "CurrentYear";
        public const string STUDENT_CURRENTSECTIONID = "CurrentSectionID";
        public const string STUDENT_CURRENTCLASSID = "CurrentClassID";
        public const string STUDENT_CURRENTDIVISIONTID = "CurrentDivisionTID";
        public const string STUDENT_CURRENTGRNO = "CurrentGrNo";
        public const string STUDENT_ADMITTEDGRNO = "AdmittedGrNo";
       // public const string STUDENT_ADMITTEDSECTION = "AdmittedSection";
        public const string STUDENT_ADMITTEDCLASSID = "AdmittedClassID";
        public const string STUDENT_ADMITTEDDIVISIONTID = "AdmittedDivisionTID";
        public const string STUDENT_ADMITTEDYEAR = "AdmittedYear";
        public const string STUDENT_STUDENTPHOTO = "StudentPhoto";
        public const string STUDENT_GENDERGUJ = "GenderGuj";
        public const string STUDENT_GENDERENG = "GenderEng";
        public const string STUDENT_DATEOFBIRTH = "DateOfBirth";
        public const string STUDENT_BIRTHDISTRICTENG = "BirthDistrictEng";
        public const string STUDENT_BIRTHDISTRICTGUJ = "BirthDistrictGuj";
        public const string STUDENT_NATIONALITYENG = "NationalityEng";
        public const string STUDENT_NATIONALITYGUJ = "NationalityGuj";
        public const string STUDENT_RELIGIONENG = "ReligionEng";
       // public const string STUDENT_RELIGIONGUJ = "ReligionGuj";
        public const string STUDENT_CASTEENG = "CasteEng";
        public const string STUDENT_CASTEGUJ = "CasteGuj";
        public const string STUDENT_SUBCASTEENG = "SubCasteEng";
        public const string STUDENT_SUBCASTEGUJ = "SubCasteGuj";
        public const string STUDENT_CATEGORYENG = "CategoryEng";
        public const string STUDENT_CATEGORYGUJ = "CategoryGuj";
        public const string STUDENT_SUBCATEGORY = "SubCategory";
        public const string STUDENT_HANDICAPPRECENT = "HandicapPrecent";
        public const string STUDENT_OTHERDEFECT = "OtherDefect";
        public const string STUDENT_PRESENTADDRESSENG = "PresentAddressEng";
        public const string STUDENT_PRESENTADDRESSGUJ = "PresentAddressGuj";
        public const string STUDENT_PRESENTCITYENG = "PresentCityEng";
        public const string STUDENT_PRESENTCITYGUJ = "PresentCityGuj";
        public const string STUDENT_PRESENTSTATEENG = "PresentStateEng";
        public const string STUDENT_PRESENTSTATEGUJ = "PresentStateGuj";
        public const string STUDENT_PRESENTPINCODE = "PresentPinCode";
        public const string STUDENT_PRESENTCONTACTNO = "PresentContactNo";
        public const string STUDENT_PRESENTEMAILID = "PresentEmailId";
        public const string STUDENT_PERMANENTADDRESSENG = "PermanentAddressEng";
        public const string STUDENT_PERMANENTADDRESSGUJ = "PermanentAddressGuj";
        public const string STUDENT_PERMANENTCITYENG = "PermanentCityEng";
        public const string STUDENT_PERMANENTCITYGUJ = "PermanentCityGuj";
        public const string STUDENT_PERMANENTSTATEENG = "PermanentStateEng";
        public const string STUDENT_PERMANENTSTATEGUJ = "PermanentStateGuj";
        public const string STUDENT_PERMANENTPINCODE = "PermanentPinCode";
        public const string STUDENT_PERMANENTCONTACTNO = "PermanentContactNo";
        public const string STUDENT_PERMANENTEMAILID = "PermanentEmailId";
        public const string STUDENT_FATHEROCCUPATION = "FatherOccupation";
        public const string STUDENT_MOTHEROCCUPATION = "MotherOccupation";
        public const string STUDENT_GARDIANOCCUPATION = "GardianOccupation";
        public const string STUDENT_FATHERQUALIFICATION = "FatherQualification";
        public const string STUDENT_MOTHERQUALIFICATION = "MotherQualification";
        public const string STUDENT_GARDIANQUALIFICATION = "GardianQualification";
        public const string STUDENT_FATHERMOBILENO = "FatherMobileNo";
        public const string STUDENT_MOTHERMOBILENO = "MotherMobileNo";
        public const string STUDENT_GARDIANMOBILENO = "GardianMobileNo";
        public const string STUDENT_FATHEREMAILID = "FatherEmailID";
        public const string STUDENT_MOTHEREMAILID = "MotherEmailID";
        public const string STUDENT_GARDIANEMAILID = "GardianEmailID";
        public const string STUDENT_HEIGHT = "Height";
        public const string STUDENT_WEIGHT = "Wight";
        public const string STUDENT_HOBBIES = "Hobbies";
        public const string STUDENT_STATUSMASTERID = "StatusMasterID";
        public const string STUDENT_LEFTDATE = "LeftDate";
        public const string STUDENT_LEFTREASON = "LeftReason";
        public const string STUDENT_LEFTYEAR = "LeftYear";
        public const string STUDENT_LEFTSTD = "LeftStd";
        public const string STUDENT_LCNO = "LcNo";
        public const string STUDENT_LCDATE = "LcDate";
        public const string STUDENT_LCREMARKS = "LcRemarks";
        public const string STUDENT_LCCOPY = "LcCopy";
        public const string STUDENT_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string STUDENT_LASTMODIFIEDDATE = "LastModifiedDate";
        public const string STUDENT_ISDELETED = "IsDeleted";
        public const string STUDENT_REGISTERDYEAR = "RegisteredYear";
        public const string STUDENT_ADMISSIONDATE = "AdmissionDate";
        public const string STUDENT_GVUNIQUEID="GVUniqueID";
        public const string STUDENT_BANKACCOUNT="BankAccount";
        public const string STUDENT_ISLEAVINGCERTI= "IsLeavingCerti";
        public const string STUDENT_ISLEAVINGGUJARATICERTI = "IsLeavingGujaratiCerti";

        public const string STUDENT_MOTHERTONGUE = "MotherTongue";
        public const string STUDENT_PREVIOUSSCHOOLDETAILS = "PreviousSchoolDetails";
        public const string STUDENT_PHYSICALIDENTIFICATION = "PhysicalIdentification";
        public const string STUDENT_FATHERORGANISATIONNAME = "FatherOrganisationName";
        public const string STUDENT_FATHERORGANISATIONCONTACTNUMBER = "FatherOrganisationContactNumber";
        public const string STUDENT_BloodGroup = "BloodGroup";
        public const string STUDENT_IFSCCODE = "IFSCCode";
        public const string STUDENT_BRANCHName = "BranchName";
        public const string STUDENT_ACCOUNTNUMBER = "AccountNumber";
        public const string STUDENT_TYPEOFVEHICLE= "TypeOfVehicle";
        public const string STUDENT_VEHICLENo = "VehicleNo";
        public const string STUDENT_DRIVERNAME = "DriverName";
        public const string STUDENT_DRIVERCONTACTNO = "DriverContactNo";
        public const string STUDENT_AADHARCARDNO = "AadharCardNo";
        public const string STUDENT_ROLLNUMBER = "RollNumber";
        public const string STUDENT_UNIQUENO = "UNIQUENO";
        public const string STUDENT_UNIQUEID = "UNIQUEId";
        public const string STUDENT_GVUNIQUENO = "GVUniqueNo";


        private int intStudentMID = 0;
        private int intSchoolMID = 0;
        private string strStudentFirstNameEng = string.Empty;
        private string strStudentMiddleNameEng = string.Empty;
        private string strStudentLastNameEng = string.Empty;
        private string strStudentFirstNameGuj = string.Empty;
        private string strStudentMiddleNameGuj = string.Empty;
        private string strStudentLastNameGuj = string.Empty;
        private string strFatherFirstNameEng = string.Empty;
        private string strFatherMiddleNameEng = string.Empty;
        private string strFatherLastNameEng = string.Empty;
        private string strMotherFirstNameEng = string.Empty;
        private string strMotherMiddleNameEng = string.Empty;
        private string strMotherLastNameEng = string.Empty;

        private string strFatherFirstNameGuj = string.Empty;
        private string strFatherMiddleNameGuj = string.Empty;
        private string strFatherLastNameGuj = string.Empty;
        private string strMotherFirstNameGuj = string.Empty;
        private string strMotherMiddleNameGuj = string.Empty;
        private string strMotherLastNameGuj = string.Empty;

        private string strAdmissionNo = string.Empty;
        private string strCurrentDate = string.Empty;
        private string strJoiningDate = string.Empty;
        private string strCurrentYear = string.Empty;
        private int intCurrentSectionID = 0;
        private int intCurrentClassID = 0;
        private int intCurrentDivisionTID = 0;
        private string strCurrentGrNo = string.Empty;
        private string strAdmittedGrNo = string.Empty;
       // private int intAdmittedSection = 0;
        private int intAdmittedClassID = 0;
        private int intAdmittedDivisionTID = 0;
        private string strAdmittedYear = string.Empty;
        private byte[] byteStudentPhoto = { };
        private string strGenderEng = string.Empty;
        private string strGenderGuj = string.Empty;
        private string strDateOfBirth = string.Empty;
        private string strBirthDistrictEng = string.Empty;
        private string strNationalityEng = string.Empty;
        private string strReligionEng = string.Empty;
        private string strCasteEng = string.Empty;
        private string strSubCasteEng = string.Empty;
        private string strCategoryEng = string.Empty;

        private string strBirthDistrictGuj = string.Empty;
        private string strNationalityGuj = string.Empty;
       // private string strReligionGuj = string.Empty;
        private string strCasteGuj = string.Empty;
        private string strSubCasteGuj = string.Empty;
        private string strCategoryGuj = string.Empty;

        private string strSubCategory = string.Empty;
        private string strHandicapPrecent = string.Empty;
        private string strOtherDefect = string.Empty;
        private string strPresentAddressEng = string.Empty;
        private string strPresentAddressGuj = string.Empty;
        private string strPresentCityEng = string.Empty;
        private string strPresentStateEng = string.Empty;

        private string strPresentCityGuj = string.Empty;
        private string strPresentStateGuj = string.Empty;

        private string strPresentPinCode = string.Empty;
        private string strPresentContactNo = string.Empty;
        private string strPresentEmailId = string.Empty;
        private string strPermanentAddressEng = string.Empty;
        private string strPermanentCityEng = string.Empty;
        private string strPermanentStateEng = string.Empty;

        private string strPermanentAddressGuj = string.Empty;
        private string strPermanentCityGuj = string.Empty;
        private string strPermanentStateGuj = string.Empty;

        private string strPermanentPinCode = string.Empty;
        private string strPermanentContactNo = string.Empty;
        private string strPermanentEmailId = string.Empty;
        private string strFatherOccupation = string.Empty;
        private string strMotherOccupation = string.Empty;
        private string strGardianOccupation = string.Empty;
        private string strFatherQualification = string.Empty;
        private string strMotherQualification = string.Empty;
        private string strGardianQualification = string.Empty;
        private string strFatherMobileNo = string.Empty;
        private string strMotherMobileNo = string.Empty;
        private string strGardianMobileNo = string.Empty;
        private string strFatherEmailID = string.Empty;
        private string strMotherEmailID = string.Empty;
        private string strGardianEmailID = string.Empty;
        private string strHeight = string.Empty;
        private string strWeight = string.Empty;
        private string strHobbies = string.Empty;
        private int intStatusMasterID = 0;
        private string strLeftDate = string.Empty;
        private string strLeftReason = string.Empty;
        private string strLeftYear = string.Empty;
        private string strLeftStd = string.Empty;
        private string strLcNo = string.Empty;
        private string strLcDate = string.Empty;
        private string strLcRemarks = string.Empty;
        private string strLcCopy = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;
        private int intIsDeleted = 0;
        private string strRegisteredYear = string.Empty;
        private string strAdmissionDate = string.Empty;
        private string strGVUniqueID=string.Empty ;
        private string strBankAccount=string.Empty;
        private int intLeavingCerti = 0;
        private int intIsLeavingGujaratiCerti = 0;

        private string strMotherTongue = string.Empty;
        private string strPreviousSchoolDetails = string.Empty;
        private string strPhysicalIdentification = string.Empty;
        private string strFatherOrganisationName = string.Empty;
        private string strFatherOrganisationContactNumber = string.Empty;
        private string strBloodGroup = string.Empty;
        private string strIFSCCode = string.Empty;
        private string strBranchName = string.Empty;
        private string strAccountNumber = string.Empty;
        private string strTypeOfVehicle = string.Empty;
        private string strVehicleNo = string.Empty;
        private string strDriverName = string.Empty;
        private string strDriverContactNo = string.Empty;
        private string strAadharCardNo = string.Empty;
        private string strRollNumber = string.Empty;
        private string strUNIQUENO = string.Empty;
        private string strUNIQUEId = string.Empty;
        private string strGVUniqueNo = string.Empty;

        #endregion

        #region ---Properties---()
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
        public string StudentFirstNameEng
        {
            get { return strStudentFirstNameEng; }
            set { strStudentFirstNameEng = value; }
        }
        public string StudentMiddleNameEng
        {
            get { return strStudentMiddleNameEng; }
            set { strStudentMiddleNameEng = value; }
        }
        public string StudentLastNameEng
        {
            get { return strStudentLastNameEng; }
            set { strStudentLastNameEng = value; }
        }
        public string StudentFirstNameGuj
        {
            get { return strStudentFirstNameGuj; }
            set { strStudentFirstNameGuj = value; }
        }
        public string StudentMiddleNameGuj
        {
            get { return strStudentMiddleNameGuj; }
            set { strStudentMiddleNameGuj = value; }
        }
        public string StudentLastNameGuj
        {
            get { return strStudentLastNameGuj; }
            set { strStudentLastNameGuj = value; }
        }
        public string FatherFirstNameEng
        {
            get { return strFatherFirstNameEng; }
            set { strFatherFirstNameEng = value; }
        }
        public string FatherFirstNameGuj
        {
            get { return strFatherFirstNameGuj; }
            set { strFatherFirstNameGuj = value; }
        }
        public string FatherMiddleNameEng
        {
            get { return strFatherMiddleNameEng; }
            set { strFatherMiddleNameEng = value; }
        }
        public string FatherMiddleNameGuj
        {
            get { return strFatherMiddleNameGuj; }
            set { strFatherMiddleNameGuj = value; }
        }
        public string FatherLastNameEng
        {
            get { return strFatherLastNameEng; }
            set { strFatherLastNameEng = value; }
        }
        public string FatherLastNameGuj
        {
            get { return strFatherLastNameGuj; }
            set { strFatherLastNameGuj = value; }
        }
        public string MotherFirstNameEng
        {
            get { return strMotherFirstNameEng; }
            set { strMotherFirstNameEng = value; }
        }
        public string MotherFirstNameGuj
        {
            get { return strMotherFirstNameGuj; }
            set { strMotherFirstNameGuj = value; }
        }
        public string MotherMiddleNameEng
        {
            get { return strMotherMiddleNameEng; }
            set { strMotherMiddleNameEng = value; }
        }
        public string MotherMiddleNameGuj
        {
            get { return strMotherMiddleNameGuj; }
            set { strMotherMiddleNameGuj = value; }
        }
        public string MotherLastNameEng
        {
            get { return strMotherLastNameEng; }
            set { strMotherLastNameEng = value; }
        }

        public string MotherLastNameGuj
        {
            get { return strMotherLastNameGuj; }
            set { strMotherLastNameGuj = value; }
        }
        public string AdmissionNo
        {
            get { return strAdmissionNo; }
            set { strAdmissionNo = value; }
        }
        public string CurrentDate
        {
            get { return strCurrentDate; }
            set { strCurrentDate = value; }
        }
        public string JoiningDate
        {
            get { return strJoiningDate; }
            set { strJoiningDate = value; }
        }
        public string CurrentYear
        {
            get { return strCurrentYear; }
            set { strCurrentYear = value; }
        }
        public int CurrentSectionID
        {
            get { return intCurrentSectionID; }
            set { intCurrentSectionID = value; }
        }
        public int CurrentClassID
        {
            get { return intCurrentClassID; }
            set { intCurrentClassID = value; }
        }
        public int CurrentDivisionTID
        {
            get { return intCurrentDivisionTID; }
            set { intCurrentDivisionTID = value; }
        }
        public string CurrentGrNo
        {
            get { return strCurrentGrNo; }
            set { strCurrentGrNo = value; }
        }
        public string AdmittedGrNo
        {
            get { return strAdmittedGrNo; }
            set { strAdmittedGrNo = value; }
        }
        public int AdmittedClassID
        {
            get { return intAdmittedClassID; }
            set { intAdmittedClassID = value; }
        }
        public int AdmittedDivisionTID
        {
            get { return intAdmittedDivisionTID; }
            set { intAdmittedDivisionTID = value; }
        }
        public string AdmittedYear
        {
            get { return strAdmittedYear; }
            set { strAdmittedYear = value; }
        }
        public byte[] StudentPhoto
        {
            get { return byteStudentPhoto; }
            set { byteStudentPhoto = value; }
        }
        public string GenderGuj
        {
            get { return strGenderGuj; }
            set { strGenderGuj = value; }
        }
        public string GenderEng
        {
            get { return strGenderEng; }
            set { strGenderEng = value; }
        }
        public string DateOfBirth
        {
            get { return strDateOfBirth; }
            set { strDateOfBirth = value; }
        }
        public string BirthDistrictEng
        {
            get { return strBirthDistrictEng; }
            set { strBirthDistrictEng = value; }
        }
        public string BirthDistrictGuj
        {
            get { return strBirthDistrictGuj; }
            set { strBirthDistrictGuj = value; }
        }
        public string NationalityEng
        {
            get { return strNationalityEng; }
            set { strNationalityEng = value; }
        }
        public string NationalityGuj
        {
            get { return strNationalityGuj; }
            set { strNationalityGuj = value; }
        }
        public string ReligionEng
        {
            get { return strReligionEng; }
            set { strReligionEng = value; }
        }
        public string CasteEng
        {
            get { return strCasteEng; }
            set { strCasteEng = value; }
        }
        public string CasteGuj
        {
            get { return strCasteGuj; }
            set { strCasteGuj = value; }
        }
        public string SubCasteEng
        {
            get { return strSubCasteEng; }
            set { strSubCasteEng = value; }
        }
        public string SubCasteGuj
        {
            get { return strSubCasteGuj; }
            set { strSubCasteGuj = value; }
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
        public string SubCategory
        {
            get { return strSubCategory; }
            set { strSubCategory = value; }
        }
        public string HandicapPrecent
        {
            get { return strHandicapPrecent; }
            set { strHandicapPrecent = value; }
        }
        public string OtherDefect
        {
            get { return strOtherDefect; }
            set { strOtherDefect = value; }
        }
        public string PresentAddressEng
        {
            get { return strPresentAddressEng; }
            set { strPresentAddressEng = value; }
        }
        public string PresentAddressGuj
        {
            get { return strPresentAddressGuj; }
            set { strPresentAddressGuj = value; }
        }
        public string PresentCityEng
        {
            get { return strPresentCityEng; }
            set { strPresentCityEng = value; }
        }
        public string PresentCityGuj
        {
            get { return strPresentCityGuj; }
            set { strPresentCityGuj = value; }
        }
        public string PresentStateEng
        {
            get { return strPresentStateEng; }
            set { strPresentStateEng = value; }
        }
        public string PresentStateGuj
        {
            get { return strPresentStateGuj; }
            set { strPresentStateGuj = value; }
        }
        public string PresentPinCode
        {
            get { return strPresentPinCode; }
            set { strPresentPinCode = value; }
        }
        public string PresentContactNo
        {
            get { return strPresentContactNo; }
            set { strPresentContactNo = value; }
        }
        public string PresentEmailId
        {
            get { return strPresentEmailId; }
            set { strPresentEmailId = value; }
        }
        public string PermanentAddressEng
        {
            get { return strPermanentAddressEng; }
            set { strPermanentAddressEng = value; }
        }
        public string PermanentAddressGuj
        {
            get { return strPermanentAddressGuj; }
            set { strPermanentAddressGuj = value; }
        }
        public string PermanentCityEng
        {
            get { return strPermanentCityEng; }
            set { strPermanentCityEng = value; }
        }
        public string PermanentCityGuj
        {
            get { return strPermanentCityGuj; }
            set { strPermanentCityGuj = value; }
        }
        public string PermanentStateEng
        {
            get { return strPermanentStateEng; }
            set { strPermanentStateEng = value; }
        }
        public string PermanentStateGuj
        {
            get { return strPermanentStateGuj; }
            set { strPermanentStateGuj = value; }
        }
        public string PermanentPinCode
        {
            get { return strPermanentPinCode; }
            set { strPermanentPinCode = value; }
        }
        public string PermanentContactNo
        {
            get { return strPermanentContactNo; }
            set { strPermanentContactNo = value; }
        }
        public string PermanentEmailId
        {
            get { return strPermanentEmailId; }
            set { strPermanentEmailId = value; }
        }
        public string FatherOccupation
        {
            get { return strFatherOccupation; }
            set { strFatherOccupation = value; }
        }
        public string MotherOccupation
        {
            get { return strMotherOccupation; }
            set { strMotherOccupation = value; }
        }
        public string GardianOccupation
        {
            get { return strGardianOccupation; }
            set { strGardianOccupation = value; }
        }
        public string FatherQualification
        {
            get { return strFatherQualification; }
            set { strFatherQualification = value; }
        }
        public string MotherQualification
        {
            get { return strMotherQualification; }
            set { strMotherQualification = value; }
        }
        public string GardianQualification
        {
            get { return strGardianQualification; }
            set { strGardianQualification = value; }
        }
        public string FatherMobileNo
        {
            get { return strFatherMobileNo; }
            set { strFatherMobileNo = value; }
        }
        public string MotherMobileNo
        {
            get { return strMotherMobileNo; }
            set { strMotherMobileNo = value; }
        }
        public string GardianMobileNo
        {
            get { return strGardianMobileNo; }
            set { strGardianMobileNo = value; }
        }
        public string FatherEmailID
        {
            get { return strFatherEmailID; }
            set { strFatherEmailID = value; }
        }
        public string MotherEmailID
        {
            get { return strMotherEmailID; }
            set { strMotherEmailID = value; }
        }
        public string GardianEmailID
        {
            get { return strGardianEmailID; }
            set { strGardianEmailID = value; }
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
        public string Hobbies
        {
            get { return strHobbies; }
            set { strHobbies = value; }
        }
        public int StatusMasterID
        {
            get { return intStatusMasterID; }
            set { intStatusMasterID = value; }
        }
        public string LeftDate
        {
            get { return strLeftDate; }
            set { strLeftDate = value; }
        }
        public string LeftReason
        {
            get { return strLeftReason; }
            set { strLeftReason = value; }
        }
        public string LeftYear
        {
            get { return strLeftYear; }
            set { strLeftYear = value; }
        }
        public string LeftStd
        {
            get { return strLeftStd; }
            set { strLeftStd = value; }
        }
        public string LcNo
        {
            get { return strLcNo; }
            set { strLcNo = value; }
        }
        public string LcDate
        {
            get { return strLcDate; }
            set { strLcDate = value; }
        }
        public string LcRemarks
        {
            get { return strLcRemarks; }
            set { strLcRemarks = value; }
        }
        public string LcCopy
        {
            get { return strLcCopy; }
            set { strLcCopy = value; }
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
        public string RegisteredYear
        {
            get { return strRegisteredYear; }
            set { strRegisteredYear = value; }
        }
        public string AdmissionDate
        {
            get { return strAdmissionDate; }
            set { strAdmissionDate = value; }
        }



        public string GVUniqueID
        {
            get { return strGVUniqueID; }
            set { strGVUniqueID = value; }
        }
        public string BankAccount
        {
            get { return strBankAccount; }
            set { strBankAccount = value; }
        }

        public int IsLeavingCerti
        {
            get { return intLeavingCerti; }
            set { intLeavingCerti = value; }
        }

        public int IsLeavingGujaratiCerti
        {
            get { return intIsLeavingGujaratiCerti; }
            set { intIsLeavingGujaratiCerti = value; }
        }
        public string MotherTongue
        {
            get { return strMotherTongue; }
            set { strMotherTongue = value; }
        }
        public string PreviousSchoolDetails
        {
            get { return strPreviousSchoolDetails; }
            set { strPreviousSchoolDetails = value; }
        }
      
        public string PhysicalIdentification
        {
            get { return strPhysicalIdentification; }
            set { strPhysicalIdentification = value; }
        }
        public string FatherOrganisationName
        {
            get { return strFatherOrganisationName; }
            set { strFatherOrganisationName = value; }
        }
        public string FatherOrganisationContactNumber
        {
            get { return strFatherOrganisationContactNumber; }
            set { strFatherOrganisationContactNumber = value; }
        }
        public string BloodGroup
        {
            get { return strBloodGroup; }
            set { strBloodGroup = value; }
        }
        public string IFSCCode
        {
            get { return strIFSCCode; }
            set { strIFSCCode = value; }
        }
        public string BranchName
        {
            get { return strBranchName; }
            set { strBranchName = value; }
        }
        public string AccountNumber
        {
            get { return strAccountNumber; }
            set { strAccountNumber = value; }
        }
        public string TypeOfVehicle
        {
            get { return strTypeOfVehicle; }
            set { strTypeOfVehicle = value; }
        }
        public string VehicleNo
        {
            get { return strVehicleNo; }
            set { strVehicleNo = value; }
        }
        public string DriverName
        {
            get { return strDriverName; }
            set { strDriverName = value; }
        }
        public string DriverContactNo
        {
            get { return strDriverContactNo; }
            set { strDriverContactNo = value; }
        }
        public string AadharCardNo
        {
            get { return strAadharCardNo; }
            set { strAadharCardNo = value; }
        }
        public string RollNumber
        {
            get { return strRollNumber; }
            set { strRollNumber = value; }
        }
        public string UNIQUENO
        {
            get { return strUNIQUENO; }
            set { strUNIQUENO = value; }
        }
        public string UNIQUEId
        {
            get { return strUNIQUEId; }
            set { strUNIQUEId = value; }
        }
        public string GvUniqueNo
        {
            get { return strGVUniqueNo; }
            set { strGVUniqueNo = value; }

        }
        #endregion
    }
}


