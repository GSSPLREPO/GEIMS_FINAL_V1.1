namespace GEIMS.BO
{
	public class SchoolBO
	{
		#region School Class Properties

        public const string SCHOOL_TABLE = "tbl_School_M";
        public const string SCHOOL_SCHOOLMID = "SchoolMID";
        public const string SCHOOL_TRUSTMID = "TrustMID";
        public const string SCHOOL_SCHOOLNAMEENG = "SchoolNameEng";
        public const string SCHOOL_SCHOOLNAMEGUJ = "SchoolNameGuj";
        public const string SCHOOL_SCHOOLCODE = "SchoolCode";
        public const string SCHOOL_SCHOOLTIMING = "SchoolTiming";
        public const string SCHOOL_ACADEMICMONTH = "AcademicMonth";
        public const string SCHOOL_SCHOOLLOGO = "SchoolLogo";
        public const string SCHOOL_SCHOOLABBREVIATION = "SchoolAbbreviation";
        public const string SCHOOL_ADDRESSENG = "AddressEng";
        public const string SCHOOL_ADDRESSGUJ = "AddressGuj";
        public const string SCHOOL_AREATYPE = "AreaType";
        public const string SCHOOL_AREASUBTYPE = "AreaSubType";
        public const string SCHOOL_TOWNENG = "TownEng";
        public const string SCHOOL_TOWNGUJ = "TownGuj";
        public const string SCHOOL_ATPONOENG = "AtPoNoEng";
        public const string SCHOOL_ATPONOGUJ = "AtPoNoGuj";
        public const string SCHOOL_TALUKAENG = "TalukaEng";
        public const string SCHOOL_TALUKAGUJ = "TalukaGuj";
        public const string SCHOOL_DISTRICTENG = "DistrictEng";
        public const string SCHOOL_DISTRICTGUJ = "DistrictGuj";
        public const string SCHOOL_STATEENG = "StateEng";
        public const string SCHOOL_STATEGUJ = "StateGuj";
        public const string SCHOOL_COUNTRYENG = "CountryEng";
        public const string SCHOOL_COUNTRYGUJ = "CountryGuj";
        public const string SCHOOL_PINCODE = "Pincode";
        public const string SCHOOL_TELEPHONENO = "TelephoneNo";
        public const string SCHOOL_MOBILENO = "MobileNo";
        public const string SCHOOL_EMAILID = "EmailID";
        public const string SCHOOL_ALTERNATEEMAILID = "AlternateEmailID";
        public const string SCHOOL_FAXNO = "FaxNo";
        public const string SCHOOL_WEBSITE = "Website";
        public const string SCHOOL_APPROVALNO = "ApprovalNo";
        public const string SCHOOL_APPROVALDATE = "ApprovalDate";
        public const string SCHOOL_APPROVALYEAR = "ApprovalYear";
        public const string SCHOOL_SSCINDEXNO = "SSCindexNo";
        public const string SCHOOL_HSCSCIENCEINDEXNO = "HSCScienceIndexNo";
        public const string SCHOOL_HSCCOMMERCEINDEXNO = "HSCCommerceIndexNo";
        public const string SCHOOL_HSCARTSINDEXNO = "HSCArtsIndexNo";
        public const string SCHOOL_REGISTRATIONCODE = "RegistrationCode";
        public const string SCHOOL_REGISTEREDNAMEENG = "RegisteredNameEng";
        public const string SCHOOL_REGISTREREDNAMEGUJ = "RegistreredNameGuj";
        public const string SCHOOL_REGISTEREDADDRESSGUJ = "RegisteredAddressGuj";
        public const string SCHOOL_SCHOOLMOTTOENG = "SchoolMottoEng";
        public const string SCHOOL_SCHOOLMOTTOGUJ = "SchoolMottoGuj";
        public const string SCHOOL_SCHOOLVISIONENG = "SchoolVisionEng";
        public const string SCHOOL_SCHOOLVISIONGUJ = "SchoolVisionGuj";
        public const string SCHOOL_ISONRENT = "IsOnRent";
        public const string SCHOOL_OWNERNAMEENG = "OwnerNameEng";
        public const string SCHOOL_OWNERNAMEGUJ = "OwnerNameGuj";
        public const string SCHOOL_OWNERADDRESSENG = "OwnerAddressEng";
        public const string SCHOOL_OWNERADDRESSGUJ = "OwnerAddressGuj";
        public const string SCHOOL_WORDNO = "WordNo";
        public const string SCHOOL_WORDNAMEENG = "WordNameEng";
        public const string SCHOOL_WORDNAMEGUJ = "WordNameGuj";
        public const string SCHOOL_PLOTNO = "PlotNo";
        public const string SCHOOL_PLOTAREA = "PlotArea";
        public const string SCHOOL_CONSTRUNCTIONYEAR = "ConstrunctionYear";
        public const string SCHOOL_NOOFFLOORS = "NoOfFloors";
        public const string SCHOOL_AUDITLIST = "AuditList";
        public const string SCHOOL_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string SCHOOL_LASTMODIFIEDDATE = "LastModifiedDate";
        public const string SCHOOL_ISDELETED = "IsDeleted";
        public const string SCHOOL_ACCOUNTSTARTDATE = "AccountStartDate";
        public const string SCHOOL_AREATYPEGUJ = "AreaTypeGuj";
        public const string SCHOOL_AREASUBTYPEGUJ = "AreaSubTypeGuj";



		private int intSchoolMID = 0;
		private int intTrustMID = 0;
		private string strSchoolNameEng = string.Empty;
		private string strSchoolNameGuj = string.Empty;
		private string strSchoolTiming = string.Empty;
        private int intAcademicMonth = 0;
		private string strSchoolCode = string.Empty;
		private byte[] byteSchoolLogo = { };
        private string strSchoolAbbreviation = string.Empty;
        private string strAddressEng = string.Empty;
        private string strAddressGuj = string.Empty;
        private string strAreaType = string.Empty;
        private string strAreaSubType = string.Empty;
        private string strTownEng = string.Empty;
        private string strTownGuj = string.Empty;
        private string strAtPoNoEng = string.Empty;
        private string strAtPoNoGuj = string.Empty;
        private string strTalukaEng = string.Empty;
        private string strTalukaGuj = string.Empty;
        private string strDistrictEng = string.Empty;
        private string strDistrictGuj = string.Empty;
        private string strStateEng = string.Empty;
        private string strStateGuj = string.Empty;
        private string strCountryEng = string.Empty;
        private string strCountryGuj = string.Empty;
        private string strPincode = string.Empty;
        private string strTelephoneNo = string.Empty;
        private string strMobileNo = string.Empty;
        private string strEmailID = string.Empty;
        private string strAlternateEmailID = string.Empty;
        private string strFaxNo = string.Empty;
        private string strWebsite = string.Empty;
        private string strApprovalNo = string.Empty;
        private string strApprovalDate = string.Empty;
        private string strApprovalYear = string.Empty;
        private string strSSCindexNo = string.Empty;
        private string strHSCScienceIndexNo = string.Empty;
        private string strHSCCommerceIndexNo = string.Empty;
        private string strHSCArtsIndexNo = string.Empty;
        private string strRegistrationCode = string.Empty;
        private string strRegisteredNameEng = string.Empty;
        private string strRegistreredNameGuj = string.Empty;
        private string strRegisteredAddressGuj = string.Empty;
        private string strSchoolMottoEng = string.Empty;
        private string strSchoolMottoGuj = string.Empty;
        private string strSchoolVisionEng = string.Empty;
        private string strSchoolVisionGuj = string.Empty;
        private int intIsOnRent = 0;
        private string strOwnerNameEng = string.Empty;
        private string strOwnerNameGuj = string.Empty;
        private string strOwnerAddressEng = string.Empty;
        private string strOwnerAddressGuj = string.Empty;
        private string strWordNo = string.Empty;
        private string strWordNameEng = string.Empty;
        private string strWordNameGuj = string.Empty;
        private string strPlotNo = string.Empty;
        private string strPlotArea = string.Empty;
        private string strConstrunctionYear = string.Empty;
        private string strNoOfFloors = string.Empty;
        private string strAuditList = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;
        private int intIsDeleted = 0;
        private string strAccountStartDate = string.Empty;
        private string strAreaTypeGuj = string.Empty;
        private string strAreaSubTypeGuj = string.Empty;

		#endregion

		#region ---Properties---
		public int SchoolMID
		{
			get { return intSchoolMID; }
			set { intSchoolMID = value; }
		}
		public int TrustMID
		{
			get { return intTrustMID; }
			set { intTrustMID = value; }
		}
		public string SchoolNameEng
		{
			get { return strSchoolNameEng; }
			set { strSchoolNameEng = value; }
		}
		public string SchoolNameGuj
		{
			get { return strSchoolNameGuj; }
			set { strSchoolNameGuj = value; }
		}
		public string SchoolTiming
		{
			get { return strSchoolTiming; }
			set { strSchoolTiming = value; }
		}
        public int AcademicMonth
        {
            get { return intAcademicMonth; }
            set { intAcademicMonth = value; }
        }
		public string SchoolCode
		{
			get { return strSchoolCode; }
			set { strSchoolCode = value; }
		}
		public byte[] SchoolLogo
		{
			get { return byteSchoolLogo; }
			set { byteSchoolLogo = value; }
		}
        public string SchoolAbbreviation
        {
            get { return strSchoolAbbreviation; }
            set { strSchoolAbbreviation = value; }
        }
        public string AddressEng
        {
            get { return strAddressEng; }
            set { strAddressEng = value; }
        }
        public string AddressGuj
        {
            get { return strAddressGuj; }
            set { strAddressGuj = value; }
        }
        public string AreaType
        {
            get { return strAreaType; }
            set { strAreaType = value; }
        }
        public string AreaSubType
        {
            get { return strAreaSubType; }
            set { strAreaSubType = value; }
        }
        public string TownEng
        {
            get { return strTownEng; }
            set { strTownEng = value; }
        }
        public string TownGuj
        {
            get { return strTownGuj; }
            set { strTownGuj = value; }
        }
        public string AtPoNoEng
        {
            get { return strAtPoNoEng; }
            set { strAtPoNoEng = value; }
        }
        public string AtPoNoGuj
        {
            get { return strAtPoNoGuj; }
            set { strAtPoNoGuj = value; }
        }
        public string TalukaEng
        {
            get { return strTalukaEng; }
            set { strTalukaEng = value; }
        }
        public string TalukaGuj
        {
            get { return strTalukaGuj; }
            set { strTalukaGuj = value; }
        }
        public string DistrictEng
        {
            get { return strDistrictEng; }
            set { strDistrictEng = value; }
        }
        public string DistrictGuj
        {
            get { return strDistrictGuj; }
            set { strDistrictGuj = value; }
        }
        public string StateEng
        {
            get { return strStateEng; }
            set { strStateEng = value; }
        }
        public string StateGuj
        {
            get { return strStateGuj; }
            set { strStateGuj = value; }
        }
        public string CountryEng
        {
            get { return strCountryEng; }
            set { strCountryEng = value; }
        }
        public string CountryGuj
        {
            get { return strCountryGuj; }
            set { strCountryGuj = value; }
        }
        public string Pincode
        {
            get { return strPincode; }
            set { strPincode = value; }
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
        public string EmailID
        {
            get { return strEmailID; }
            set { strEmailID = value; }
        }
        public string AlternateEmailID
        {
            get { return strAlternateEmailID; }
            set { strAlternateEmailID = value; }
        }
        public string FaxNo
        {
            get { return strFaxNo; }
            set { strFaxNo = value; }
        }
        public string Website
        {
            get { return strWebsite; }
            set { strWebsite = value; }
        }
        public string ApprovalNo
        {
            get { return strApprovalNo; }
            set { strApprovalNo = value; }
        }
        public string ApprovalDate
        {
            get { return strApprovalDate; }
            set { strApprovalDate = value; }
        }
        public string ApprovalYear
        {
            get { return strApprovalYear; }
            set { strApprovalYear = value; }
        }
        public string SSCindexNo
        {
            get { return strSSCindexNo; }
            set { strSSCindexNo = value; }
        }
        public string HSCScienceIndexNo
        {
            get { return strHSCScienceIndexNo; }
            set { strHSCScienceIndexNo = value; }
        }
        public string HSCCommerceIndexNo
        {
            get { return strHSCCommerceIndexNo; }
            set { strHSCCommerceIndexNo = value; }
        }
        public string HSCArtsIndexNo
        {
            get { return strHSCArtsIndexNo; }
            set { strHSCArtsIndexNo = value; }
        }
        public string RegistrationCode
        {
            get { return strRegistrationCode; }
            set { strRegistrationCode = value; }
        }
        public string RegisteredNameEng
        {
            get { return strRegisteredNameEng; }
            set { strRegisteredNameEng = value; }
        }
        public string RegistreredNameGuj
        {
            get { return strRegistreredNameGuj; }
            set { strRegistreredNameGuj = value; }
        }
        public string RegisteredAddressGuj
        {
            get { return strRegisteredAddressGuj; }
            set { strRegisteredAddressGuj = value; }
        }
        public string SchoolMottoEng
        {
            get { return strSchoolMottoEng; }
            set { strSchoolMottoEng = value; }
        }
        public string SchoolMottoGuj
        {
            get { return strSchoolMottoGuj; }
            set { strSchoolMottoGuj = value; }
        }
        public string SchoolVisionEng
        {
            get { return strSchoolVisionEng; }
            set { strSchoolVisionEng = value; }
        }
        public string SchoolVisionGuj
        {
            get { return strSchoolVisionGuj; }
            set { strSchoolVisionGuj = value; }
        }
        public int IsOnRent
        {
            get { return intIsOnRent; }
            set { intIsOnRent = value; }
        }
        public string OwnerNameEng
        {
            get { return strOwnerNameEng; }
            set { strOwnerNameEng = value; }
        }
        public string OwnerNameGuj
        {
            get { return strOwnerNameGuj; }
            set { strOwnerNameGuj = value; }
        }
        public string OwnerAddressEng
        {
            get { return strOwnerAddressEng; }
            set { strOwnerAddressEng = value; }
        }
        public string OwnerAddressGuj
        {
            get { return strOwnerAddressGuj; }
            set { strOwnerAddressGuj = value; }
        }
        public string WordNo
        {
            get { return strWordNo; }
            set { strWordNo = value; }
        }
        public string WordNameEng
        {
            get { return strWordNameEng; }
            set { strWordNameEng = value; }
        }
        public string WordNameGuj
        {
            get { return strWordNameGuj; }
            set { strWordNameGuj = value; }
        }
        public string PlotNo
        {
            get { return strPlotNo; }
            set { strPlotNo = value; }
        }
        public string PlotArea
        {
            get { return strPlotArea; }
            set { strPlotArea = value; }
        }
        public string ConstrunctionYear
        {
            get { return strConstrunctionYear; }
            set { strConstrunctionYear = value; }
        }
        public string NoOfFloors
        {
            get { return strNoOfFloors; }
            set { strNoOfFloors = value; }
        }
        public string AuditList
        {
            get { return strAuditList; }
            set { strAuditList = value; }
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

        public string AccountStartDate
        {
            get { return strAccountStartDate; }
            set { strAccountStartDate = value; }
        }

        public string AreaTypeGuj
        {
            get { return strAreaTypeGuj; }
            set { strAreaTypeGuj = value; }
        }

        public string AreaSubTypeGuj
        {
            get { return strAreaSubTypeGuj; }
            set { strAreaSubTypeGuj = value; }
        }

		#endregion
	}
}


