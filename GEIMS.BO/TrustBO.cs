namespace GEIMS.BO
{
	public class TrustBO
	{
        #region Trust Class Properties

        public const string TRUST_TABLE = "tbl_Trust_M";
        public const string TRUST_TRUSTMID = "TrustMID";
        public const string TRUST_TRUSTNAMEENG = "TrustNameEng";
        public const string TRUST_TRUSTNAMEGUJ = "TrustNameGuj";
        public const string TRUST_TRUSTABBREVIATION = "TrustAbbreviation";
        public const string TRUST_TRUSTLOGO = "TrustLogo";
        public const string TRUST_REGISTRATIONCODE = "RegistrationCode";
        public const string TRUST_ADDRESSENG = "AddressEng";
        public const string TRUST_ADDRESSGUJ = "AddressGuj";
        public const string TRUST_TOWNENG = "TownEng";
        public const string TRUST_TOWNGUJ = "TownGuj";
        public const string TRUST_DISTRICTENG = "DistrictEng";
        public const string TRUST_DISTRICTGUJ = "DistrictGuj";
        public const string TRUST_STATEENG = "StateEng";
        public const string TRUST_STATEGUJ = "StateGuj";
        public const string TRUST_COUNTRYENG = "CountryEng";
        public const string TRUST_COUNTRYGUJ = "CountryGuj";
        public const string TRUST_PINCODE = "Pincode";
        public const string TRUST_TELEPHONENO = "TelephoneNo";
        public const string TRUST_MOBILENO = "MobileNo";
        public const string TRUST_EMAILID = "EmailId";
        public const string TRUST_ALTERNATEEMAILID = "AlternateEmailId";
        public const string TRUST_FAXNO = "FaxNo";
        public const string TRUST_WEBSITE = "Website";
        public const string TRUST_APPROVALYEAR = "ApprovalYear";
        public const string TRUST_APPROVALDATE = "ApprovalDate";
        public const string TRUST_APPROVALNO = "ApprovalNo";
        public const string TRUST_ISDELETED = "IsDeleted";
        public const string TRUST_LASTMODIFIEDUSERID = "LastModifiedUserID";
        public const string TRUST_LASTMODIFIEDDATE = "LastModifiedDate";
        public const string TRUST_ACCOUNTSTARTDATE = "AccountStartDate";



        private int intTrustMID = 0;
        private string strTrustNameEng = string.Empty;
        private string strTrustNameGuj = string.Empty;
        private byte[] byteTrustLogo = { };
        private string strTrustAbbreviation = string.Empty;
        private string strRegistrationCode = string.Empty;
        private string strAddressEng = string.Empty;
        private string strAddressGuj = string.Empty;
        private string strTownEng = string.Empty;
        private string strTownGuj = string.Empty;
        private string strDistrictEng = string.Empty;
        private string strDistrictGuj = string.Empty;
        private string strStateEng = string.Empty;
        private string strStateGuj = string.Empty;
        private string strCountryEng = string.Empty;
        private string strCountryGuj = string.Empty;
        private string strPincode = string.Empty;
        private string strTelephoneNo = string.Empty;
        private string strMobileNo = string.Empty;
        private string strEmailId = string.Empty;
        private string strAlternateEmailId = string.Empty;
        private string strFaxNo = string.Empty;
        private string strWebsite = string.Empty;
        private string strApprovalYear = string.Empty;
        private string strApprovalDate = string.Empty;
        private string strApprovalNo = string.Empty;
        private int intIsDeleted = 0;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;
        private string strAccountStartDate = string.Empty;

        #endregion

        #region ---Properties---
        public int TrustMID
        {
            get { return intTrustMID; }
            set { intTrustMID = value; }
        }
        public string TrustNameEng
        {
            get { return strTrustNameEng; }
            set { strTrustNameEng = value; }
        }
        public string TrustNameGuj
        {
            get { return strTrustNameGuj; }
            set { strTrustNameGuj = value; }
        }

        public string TrustAbbreviation
        {
            get { return strTrustAbbreviation; }
            set { strTrustAbbreviation = value; }
        }
        public byte[] TrustLogo
        {
            get { return byteTrustLogo; }
            set { byteTrustLogo = value; }
        }
        public string RegistrationCode
        {
            get { return strRegistrationCode; }
            set { strRegistrationCode = value; }
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
        public string EmailId
        {
            get { return strEmailId; }
            set { strEmailId = value; }
        }
        public string AlternateEmailId
        {
            get { return strAlternateEmailId; }
            set { strAlternateEmailId = value; }
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
        public string ApprovalYear
        {
            get { return strApprovalYear; }
            set { strApprovalYear = value; }
        }
        public string ApprovalDate
        {
            get { return strApprovalDate; }
            set { strApprovalDate = value; }
        }
        public string ApprovalNo
        {
            get { return strApprovalNo; }
            set { strApprovalNo = value; }
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

        public string AccountStartDate
        {
            get { return strAccountStartDate; }
            set { strAccountStartDate = value; }
        }

        #endregion
    }
}


