using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
	/// <summary>
    /// Class Created By : Darshan, 09/12/2014
	/// Summary description for Organisation.
    /// </summary>
	public class EmployeeMBL 
	{
		#region user defined variables
        public string sSql;
        public string strStoredProcName;

        public SqlParameter[] pSqlParameter = null;
        #endregion
		
		#region Select All EmployeeM Details
        /// <summary>
        /// To Select All data from the tbl_Employee_M table
        /// Created By : Darshan, 09/12/2014
		/// Modified By :
        /// </summary>
		/// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult  EmployeeM_SelectAll()
        {
			try
            {
				sSql = "usp_tbl_Employee_M_SelectAll";
                DataTable dtEmployeeM  = new DataTable();
                dtEmployeeM = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtEmployeeM);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select All EmployeeM Details
        /// <summary>
        /// To Select All data from the tbl_Employee_M table
        /// Created By : Darshan, 09/12/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult EmployeeM_SelectAll_ForDropDown(int intEmployeeMID)
        {
            try
            {

                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;
                sSql = "usp_tbl_Employee_M_SelectAll_ForDropdown";
                DataTable dtEmployeeM = new DataTable();
                dtEmployeeM = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtEmployeeM);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
		
		#region Select EmployeeM Details by EmployeeMID
        /// <summary>
        /// Select all details of EmployeeM for selected EmployeeMID from tbl_Employee_M table
        /// Created By : Darshan, 09/12/2014
		/// Modified By :
        /// </summary>
        /// <param name="intEmployeeMID"></param>
        /// <returns></returns>
		public ApplicationResult EmployeeM_Select(int intEmployeeMID)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

				strStoredProcName = "usp_tbl_Employee_M_Select";
				
                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
			}
			catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select EmployeeName by SearchText
        /// Select all details of EmployeeM for selected EmployeeMID from tbl_Employee_M table
        /// Created By : Darshan, 09/12/2014
        /// Modified By :
        public ApplicationResult EmployeeM_Select_EmployeeName(string strSearch,string strSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@Search", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strSearch;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = Convert.ToInt32(strSchoolMID);

                strStoredProcName = "usp_tbl_Employee_M_SelectAll_Teacher";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Search Student By Different Way
        /// <summary>
        /// Function which validates whether the StudentTName already exits in tbl_Student_T table.
        /// Created By : Amruta, 5/3/2013
        /// Modified By :
        /// </summary>
        /// <param name="strStudentTName"></param>
        /// <returns></returns>
        public ApplicationResult Employee_Search_By_NameAndCode(string strSearchText, int strSearchID, int TrustMID,int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@SearchText", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strSearchText;

                pSqlParameter[1] = new SqlParameter("@SearchID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strSearchID;

                pSqlParameter[2] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = TrustMID;

                pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_Employee_M_SearchByCodeandName";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

		#region Delete EmployeeM Details by EmployeeMID
        /// <summary>
        /// To Delete details of EmployeeM for selected EmployeeMID from tbl_Employee_M table
        /// Created By : Darshan, 09/12/2014
		/// Modified By :
        /// </summary>
        /// <param name="intEmployeeMID"></param>
        /// <returns></returns>
		public ApplicationResult EmployeeM_Delete(int intEmployeeMID)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

				strStoredProcName = "usp_tbl_Employee_M_Delete";
				
                int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                if (iResult > 0)
                {
                    ApplicationResult objResults = new ApplicationResult();
                    objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                    return objResults;
                }
                else
                {
                    ApplicationResult objResults = new ApplicationResult();
                    objResults.status = ApplicationResult.CommonStatusType.FAILURE;
                    return objResults;
                }
			}
			catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
		
		#region Insert EmployeeM Details
		/// <summary>
        /// To Insert details of EmployeeM in tbl_Employee_M table
        /// Created By : Darshan, 09/12/2014
		/// Modified By :
        /// </summary>
        /// <param name="objEmployeeMBO"></param>
        /// <returns></returns>
        public ApplicationResult EmployeeM_Insert(EmployeeMBO objEmployeeMBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[112];


                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objEmployeeMBO.TrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objEmployeeMBO.SchoolMID;

                pSqlParameter[2] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objEmployeeMBO.DepartmentID;

                pSqlParameter[3] = new SqlParameter("@DesignationID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objEmployeeMBO.DesignationID;

                pSqlParameter[4] = new SqlParameter("@EmployeeCode", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objEmployeeMBO.EmployeeCode;

                pSqlParameter[5] = new SqlParameter("@EmployeeFNameENG", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objEmployeeMBO.EmployeeFNameENG;

                pSqlParameter[6] = new SqlParameter("@EmployeeFNameGUJ", SqlDbType.NVarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objEmployeeMBO.EmployeeFNameGUJ;

                pSqlParameter[7] = new SqlParameter("@EmployeeMNameENG", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objEmployeeMBO.EmployeeMNameENG;

                pSqlParameter[8] = new SqlParameter("@EmployeeMNameGUJ", SqlDbType.NVarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objEmployeeMBO.EmployeeMNameGUJ;

                pSqlParameter[9] = new SqlParameter("@EmployeeLNameENG", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objEmployeeMBO.EmployeeLNameENG;

                pSqlParameter[10] = new SqlParameter("@EmployeeLNameGUJ", SqlDbType.NVarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objEmployeeMBO.EmployeeLNameGUJ;

                pSqlParameter[11] = new SqlParameter("@Photo", SqlDbType.Image);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objEmployeeMBO.Photo;

                pSqlParameter[12] = new SqlParameter("@Gender", SqlDbType.VarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objEmployeeMBO.Gender;

                pSqlParameter[13] = new SqlParameter("@GenderGuj", SqlDbType.NVarChar);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objEmployeeMBO.GenderGuj;

                pSqlParameter[14] = new SqlParameter("@DateOfBirth", SqlDbType.VarChar);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objEmployeeMBO.DateOfBirth;

                pSqlParameter[15] = new SqlParameter("@BirthDistrictENG", SqlDbType.VarChar);
                pSqlParameter[15].Direction = ParameterDirection.Input;
                pSqlParameter[15].Value = objEmployeeMBO.BirthDistrictENG;

                pSqlParameter[16] = new SqlParameter("@BirthDistrictGUJ", SqlDbType.NVarChar);
                pSqlParameter[16].Direction = ParameterDirection.Input;
                pSqlParameter[16].Value = objEmployeeMBO.BirthDistrictGUJ;

                pSqlParameter[17] = new SqlParameter("@BirthTalukaENG", SqlDbType.VarChar);
                pSqlParameter[17].Direction = ParameterDirection.Input;
                pSqlParameter[17].Value = objEmployeeMBO.BirthTalukaENG;

                pSqlParameter[18] = new SqlParameter("@BirthTalukaGUJ", SqlDbType.NVarChar);
                pSqlParameter[18].Direction = ParameterDirection.Input;
                pSqlParameter[18].Value = objEmployeeMBO.BirthTalukaGUJ;

                pSqlParameter[19] = new SqlParameter("@BirthCityVillageENG", SqlDbType.VarChar);
                pSqlParameter[19].Direction = ParameterDirection.Input;
                pSqlParameter[19].Value = objEmployeeMBO.BirthCityVillageENG;

                pSqlParameter[20] = new SqlParameter("@BirthCityVillageGUJ", SqlDbType.NVarChar);
                pSqlParameter[20].Direction = ParameterDirection.Input;
                pSqlParameter[20].Value = objEmployeeMBO.BirthCityVillageGUJ;

                pSqlParameter[21] = new SqlParameter("@NationalityENG", SqlDbType.VarChar);
                pSqlParameter[21].Direction = ParameterDirection.Input;
                pSqlParameter[21].Value = objEmployeeMBO.NationalityENG;

                pSqlParameter[22] = new SqlParameter("@NationalityGUJ", SqlDbType.NVarChar);
                pSqlParameter[22].Direction = ParameterDirection.Input;
                pSqlParameter[22].Value = objEmployeeMBO.NationalityGUJ;

                pSqlParameter[23] = new SqlParameter("@ReligionENG", SqlDbType.VarChar);
                pSqlParameter[23].Direction = ParameterDirection.Input;
                pSqlParameter[23].Value = objEmployeeMBO.ReligionENG;

                pSqlParameter[24] = new SqlParameter("@ReligionGUJ", SqlDbType.NVarChar);
                pSqlParameter[24].Direction = ParameterDirection.Input;
                pSqlParameter[24].Value = objEmployeeMBO.ReligionGUJ;

                pSqlParameter[25] = new SqlParameter("@Caste", SqlDbType.VarChar);
                pSqlParameter[25].Direction = ParameterDirection.Input;
                pSqlParameter[25].Value = objEmployeeMBO.Caste;

                pSqlParameter[26] = new SqlParameter("@MaritalStatus", SqlDbType.VarChar);
                pSqlParameter[26].Direction = ParameterDirection.Input;
                pSqlParameter[26].Value = objEmployeeMBO.MaritalStatus;

                pSqlParameter[27] = new SqlParameter("@BloodGroup", SqlDbType.VarChar);
                pSqlParameter[27].Direction = ParameterDirection.Input;
                pSqlParameter[27].Value = objEmployeeMBO.BloodGroup;

                pSqlParameter[28] = new SqlParameter("@MotherLanguage", SqlDbType.VarChar);
                pSqlParameter[28].Direction = ParameterDirection.Input;
                pSqlParameter[28].Value = objEmployeeMBO.MotherLanguage;

                pSqlParameter[29] = new SqlParameter("@CurrentAddressENG", SqlDbType.VarChar);
                pSqlParameter[29].Direction = ParameterDirection.Input;
                pSqlParameter[29].Value = objEmployeeMBO.CurrentAddressENG;

                pSqlParameter[30] = new SqlParameter("@CurrentAddressGUJ", SqlDbType.NVarChar);
                pSqlParameter[30].Direction = ParameterDirection.Input;
                pSqlParameter[30].Value = objEmployeeMBO.CurrentAddressGUJ;

                pSqlParameter[31] = new SqlParameter("@CurrentLandmarkENG", SqlDbType.VarChar);
                pSqlParameter[31].Direction = ParameterDirection.Input;
                pSqlParameter[31].Value = objEmployeeMBO.CurrentLandmarkENG;

                pSqlParameter[32] = new SqlParameter("@CurrentLandmarkGUJ", SqlDbType.NVarChar);
                pSqlParameter[32].Direction = ParameterDirection.Input;
                pSqlParameter[32].Value = objEmployeeMBO.CurrentLandmarkGUJ;

                pSqlParameter[33] = new SqlParameter("@CurrentCityENG", SqlDbType.VarChar);
                pSqlParameter[33].Direction = ParameterDirection.Input;
                pSqlParameter[33].Value = objEmployeeMBO.CurrentCityENG;

                pSqlParameter[34] = new SqlParameter("@CurrentCityGUJ", SqlDbType.NVarChar);
                pSqlParameter[34].Direction = ParameterDirection.Input;
                pSqlParameter[34].Value = objEmployeeMBO.CurrentCityGUJ;

                pSqlParameter[35] = new SqlParameter("@CurrentStateENG", SqlDbType.VarChar);
                pSqlParameter[35].Direction = ParameterDirection.Input;
                pSqlParameter[35].Value = objEmployeeMBO.CurrentStateENG;

                pSqlParameter[36] = new SqlParameter("@CurrentStateGUJ", SqlDbType.NVarChar);
                pSqlParameter[36].Direction = ParameterDirection.Input;
                pSqlParameter[36].Value = objEmployeeMBO.CurrentStateGUJ;

                pSqlParameter[37] = new SqlParameter("@CurrentPinCode", SqlDbType.VarChar);
                pSqlParameter[37].Direction = ParameterDirection.Input;
                pSqlParameter[37].Value = objEmployeeMBO.CurrentPinCode;

                pSqlParameter[38] = new SqlParameter("@PermenantAddressEng", SqlDbType.VarChar);
                pSqlParameter[38].Direction = ParameterDirection.Input;
                pSqlParameter[38].Value = objEmployeeMBO.PermenantAddressEng;

                pSqlParameter[39] = new SqlParameter("@PermenantAddressGuj", SqlDbType.NVarChar);
                pSqlParameter[39].Direction = ParameterDirection.Input;
                pSqlParameter[39].Value = objEmployeeMBO.PermenantAddressGuj;

                pSqlParameter[40] = new SqlParameter("@PermenantLandmarkEng", SqlDbType.VarChar);
                pSqlParameter[40].Direction = ParameterDirection.Input;
                pSqlParameter[40].Value = objEmployeeMBO.PermenantLandmarkEng;

                pSqlParameter[41] = new SqlParameter("@PermenantLandmarkGuj", SqlDbType.NVarChar);
                pSqlParameter[41].Direction = ParameterDirection.Input;
                pSqlParameter[41].Value = objEmployeeMBO.PermenantLandmarkGuj;

                pSqlParameter[42] = new SqlParameter("@PermenantCityEng", SqlDbType.VarChar);
                pSqlParameter[42].Direction = ParameterDirection.Input;
                pSqlParameter[42].Value = objEmployeeMBO.PermenantCityEng;

                pSqlParameter[43] = new SqlParameter("@PermenantCityGuj", SqlDbType.NVarChar);
                pSqlParameter[43].Direction = ParameterDirection.Input;
                pSqlParameter[43].Value = objEmployeeMBO.PermenantCityGuj;

                pSqlParameter[44] = new SqlParameter("@PermenantStateEng", SqlDbType.VarChar);
                pSqlParameter[44].Direction = ParameterDirection.Input;
                pSqlParameter[44].Value = objEmployeeMBO.PermenantStateEng;

                pSqlParameter[45] = new SqlParameter("@PermenantStateGuj", SqlDbType.NVarChar);
                pSqlParameter[45].Direction = ParameterDirection.Input;
                pSqlParameter[45].Value = objEmployeeMBO.PermenantStateGuj;

                pSqlParameter[46] = new SqlParameter("@PermenantPincode", SqlDbType.VarChar);
                pSqlParameter[46].Direction = ParameterDirection.Input;
                pSqlParameter[46].Value = objEmployeeMBO.PermenantPincode;

                pSqlParameter[47] = new SqlParameter("@TelephoneNo", SqlDbType.VarChar);
                pSqlParameter[47].Direction = ParameterDirection.Input;
                pSqlParameter[47].Value = objEmployeeMBO.TelephoneNo;

                pSqlParameter[48] = new SqlParameter("@MobileNo", SqlDbType.VarChar);
                pSqlParameter[48].Direction = ParameterDirection.Input;
                pSqlParameter[48].Value = objEmployeeMBO.MobileNo;

                pSqlParameter[49] = new SqlParameter("@EmailId", SqlDbType.VarChar);
                pSqlParameter[49].Direction = ParameterDirection.Input;
                pSqlParameter[49].Value = objEmployeeMBO.EmailId;

                pSqlParameter[50] = new SqlParameter("@Hobbies", SqlDbType.VarChar);
                pSqlParameter[50].Direction = ParameterDirection.Input;
                pSqlParameter[50].Value = objEmployeeMBO.Hobbies;

                pSqlParameter[51] = new SqlParameter("@RightVision", SqlDbType.VarChar);
                pSqlParameter[51].Direction = ParameterDirection.Input;
                pSqlParameter[51].Value = objEmployeeMBO.RightVision;

                pSqlParameter[52] = new SqlParameter("@LeftVision", SqlDbType.VarChar);
                pSqlParameter[52].Direction = ParameterDirection.Input;
                pSqlParameter[52].Value = objEmployeeMBO.LeftVision;

                pSqlParameter[53] = new SqlParameter("@RectificationDevice", SqlDbType.VarChar);
                pSqlParameter[53].Direction = ParameterDirection.Input;
                pSqlParameter[53].Value = objEmployeeMBO.RectificationDevice;

                pSqlParameter[54] = new SqlParameter("@Height", SqlDbType.VarChar);
                pSqlParameter[54].Direction = ParameterDirection.Input;
                pSqlParameter[54].Value = objEmployeeMBO.Height;

                pSqlParameter[55] = new SqlParameter("@Weight", SqlDbType.VarChar);
                pSqlParameter[55].Direction = ParameterDirection.Input;
                pSqlParameter[55].Value = objEmployeeMBO.Weight;

                pSqlParameter[56] = new SqlParameter("@PhysicalIdentificationENG", SqlDbType.VarChar);
                pSqlParameter[56].Direction = ParameterDirection.Input;
                pSqlParameter[56].Value = objEmployeeMBO.PhysicalIdentificationENG;

                pSqlParameter[57] = new SqlParameter("@PhysicalIdentificationGUJ", SqlDbType.NVarChar);
                pSqlParameter[57].Direction = ParameterDirection.Input;
                pSqlParameter[57].Value = objEmployeeMBO.PhysicalIdentificationGUJ;

                pSqlParameter[58] = new SqlParameter("@MotherMaidenFNameENG", SqlDbType.VarChar);
                pSqlParameter[58].Direction = ParameterDirection.Input;
                pSqlParameter[58].Value = objEmployeeMBO.MotherMaidenFNameENG;

                pSqlParameter[59] = new SqlParameter("@MotherMaidenFNameGUJ", SqlDbType.NVarChar);
                pSqlParameter[59].Direction = ParameterDirection.Input;
                pSqlParameter[59].Value = objEmployeeMBO.MotherMaidenFNameGUJ;

                pSqlParameter[60] = new SqlParameter("@MotherMaidenMNameENG", SqlDbType.VarChar);
                pSqlParameter[60].Direction = ParameterDirection.Input;
                pSqlParameter[60].Value = objEmployeeMBO.MotherMaidenMNameENG;

                pSqlParameter[61] = new SqlParameter("@MotherMaidenMNameGUJ", SqlDbType.NVarChar);
                pSqlParameter[61].Direction = ParameterDirection.Input;
                pSqlParameter[61].Value = objEmployeeMBO.MotherMaidenMNameGUJ;

                pSqlParameter[62] = new SqlParameter("@MotherMaidenLNameENG", SqlDbType.VarChar);
                pSqlParameter[62].Direction = ParameterDirection.Input;
                pSqlParameter[62].Value = objEmployeeMBO.MotherMaidenLNameENG;

                pSqlParameter[63] = new SqlParameter("@MotherMaidenLNameGUJ", SqlDbType.NVarChar);
                pSqlParameter[63].Direction = ParameterDirection.Input;
                pSqlParameter[63].Value = objEmployeeMBO.MotherMaidenLNameGUJ;

                pSqlParameter[64] = new SqlParameter("@BankName", SqlDbType.VarChar);
                pSqlParameter[64].Direction = ParameterDirection.Input;
                pSqlParameter[64].Value = objEmployeeMBO.BankName;

                pSqlParameter[65] = new SqlParameter("@BranchName", SqlDbType.VarChar);
                pSqlParameter[65].Direction = ParameterDirection.Input;
                pSqlParameter[65].Value = objEmployeeMBO.BranchName;

                pSqlParameter[66] = new SqlParameter("@BranchCode", SqlDbType.VarChar);
                pSqlParameter[66].Direction = ParameterDirection.Input;
                pSqlParameter[66].Value = objEmployeeMBO.BranchCode;

                pSqlParameter[67] = new SqlParameter("@AccountNo", SqlDbType.VarChar);
                pSqlParameter[67].Direction = ParameterDirection.Input;
                pSqlParameter[67].Value = objEmployeeMBO.AccountNo;

                pSqlParameter[68] = new SqlParameter("@PFNo", SqlDbType.VarChar);
                pSqlParameter[68].Direction = ParameterDirection.Input;
                pSqlParameter[68].Value = objEmployeeMBO.PFNo;

                pSqlParameter[69] = new SqlParameter("@PANNo", SqlDbType.VarChar);
                pSqlParameter[69].Direction = ParameterDirection.Input;
                pSqlParameter[69].Value = objEmployeeMBO.PANNo;

                pSqlParameter[70] = new SqlParameter("@ESICNo", SqlDbType.VarChar);
                pSqlParameter[70].Direction = ParameterDirection.Input;
                pSqlParameter[70].Value = objEmployeeMBO.ESICNo;

                pSqlParameter[71] = new SqlParameter("@IFSCCode", SqlDbType.VarChar);
                pSqlParameter[71].Direction = ParameterDirection.Input;
                pSqlParameter[71].Value = objEmployeeMBO.IFSCCode;

                pSqlParameter[72] = new SqlParameter("@GPFAccountNo", SqlDbType.VarChar);
                pSqlParameter[72].Direction = ParameterDirection.Input;
                pSqlParameter[72].Value = objEmployeeMBO.GPFAccountNo;

                pSqlParameter[73] = new SqlParameter("@CPFAccountNo", SqlDbType.VarChar);
                pSqlParameter[73].Direction = ParameterDirection.Input;
                pSqlParameter[73].Value = objEmployeeMBO.CPFAccountNo;

                pSqlParameter[74] = new SqlParameter("@DepartmentJoiningDate", SqlDbType.VarChar);
                pSqlParameter[74].Direction = ParameterDirection.Input;
                pSqlParameter[74].Value = objEmployeeMBO.DepartmentJoiningDate;

                pSqlParameter[75] = new SqlParameter("@OrganisationJoiningDate", SqlDbType.VarChar);
                pSqlParameter[75].Direction = ParameterDirection.Input;
                pSqlParameter[75].Value = objEmployeeMBO.OrganisationJoiningDate;

                pSqlParameter[76] = new SqlParameter("@TypeOfAppointment", SqlDbType.VarChar);
                pSqlParameter[76].Direction = ParameterDirection.Input;
                pSqlParameter[76].Value = objEmployeeMBO.TypeOfAppointment;

                pSqlParameter[77] = new SqlParameter("@ReplacementSchoolInfoENG", SqlDbType.VarChar);
                pSqlParameter[77].Direction = ParameterDirection.Input;
                pSqlParameter[77].Value = objEmployeeMBO.ReplacementSchoolInfoENG;

                pSqlParameter[78] = new SqlParameter("@ReplacementSchoolInfoGUJ", SqlDbType.NVarChar);
                pSqlParameter[78].Direction = ParameterDirection.Input;
                pSqlParameter[78].Value = objEmployeeMBO.ReplacementSchoolInfoGUJ;

                pSqlParameter[79] = new SqlParameter("@RetirementDate", SqlDbType.VarChar);
                pSqlParameter[79].Direction = ParameterDirection.Input;
                pSqlParameter[79].Value = objEmployeeMBO.RetirementDate;

                pSqlParameter[80] = new SqlParameter("@TermEndRetirementDate", SqlDbType.VarChar);
                pSqlParameter[80].Direction = ParameterDirection.Input;
                pSqlParameter[80].Value = objEmployeeMBO.TermEndRetirementDate;

                pSqlParameter[81] = new SqlParameter("@IsResigned", SqlDbType.Int);
                pSqlParameter[81].Direction = ParameterDirection.Input;
                pSqlParameter[81].Value = objEmployeeMBO.IsResigned;

                pSqlParameter[82] = new SqlParameter("@ResignedDate", SqlDbType.VarChar);
                pSqlParameter[82].Direction = ParameterDirection.Input;
                pSqlParameter[82].Value = objEmployeeMBO.ResignedDate;

                pSqlParameter[83] = new SqlParameter("@BreakInfoENG", SqlDbType.VarChar);
                pSqlParameter[83].Direction = ParameterDirection.Input;
                pSqlParameter[83].Value = objEmployeeMBO.BreakInfoENG;

                pSqlParameter[84] = new SqlParameter("@BreakInfoGUJ", SqlDbType.NVarChar);
                pSqlParameter[84].Direction = ParameterDirection.Input;
                pSqlParameter[84].Value = objEmployeeMBO.BreakInfoGUJ;

                pSqlParameter[85] = new SqlParameter("@ResignReasonEng", SqlDbType.VarChar);
                pSqlParameter[85].Direction = ParameterDirection.Input;
                pSqlParameter[85].Value = objEmployeeMBO.ResignReasonEng;

                pSqlParameter[86] = new SqlParameter("@ResignReasonGuj", SqlDbType.NVarChar);
                pSqlParameter[86].Direction = ParameterDirection.Input;
                pSqlParameter[86].Value = objEmployeeMBO.ResignReasonGuj;

                pSqlParameter[87] = new SqlParameter("@OtherAchivementDetailsENG", SqlDbType.VarChar);
                pSqlParameter[87].Direction = ParameterDirection.Input;
                pSqlParameter[87].Value = objEmployeeMBO.OtherAchivementDetailsENG;

                pSqlParameter[88] = new SqlParameter("@OtherAchivementDetailsGUJ", SqlDbType.NVarChar);
                pSqlParameter[88].Direction = ParameterDirection.Input;
                pSqlParameter[88].Value = objEmployeeMBO.OtherAchivementDetailsGUJ;

                pSqlParameter[89] = new SqlParameter("@IsUser", SqlDbType.Int);
                pSqlParameter[89].Direction = ParameterDirection.Input;
                pSqlParameter[89].Value = objEmployeeMBO.IsUser;

                pSqlParameter[90] = new SqlParameter("@UserName", SqlDbType.VarChar);
                pSqlParameter[90].Direction = ParameterDirection.Input;
                pSqlParameter[90].Value = objEmployeeMBO.UserName;

                pSqlParameter[91] = new SqlParameter("@Password", SqlDbType.VarChar);
                pSqlParameter[91].Direction = ParameterDirection.Input;
                pSqlParameter[91].Value = objEmployeeMBO.Password;

                pSqlParameter[92] = new SqlParameter("@RoleID", SqlDbType.Int);
                pSqlParameter[92].Direction = ParameterDirection.Input;
                pSqlParameter[92].Value = objEmployeeMBO.RoleID;

                pSqlParameter[93] = new SqlParameter("@IsTeacher", SqlDbType.Int);
                pSqlParameter[93].Direction = ParameterDirection.Input;
                pSqlParameter[93].Value = objEmployeeMBO.IsTeacher;

                pSqlParameter[94] = new SqlParameter("@IsPrincipal", SqlDbType.Int);
                pSqlParameter[94].Direction = ParameterDirection.Input;
                pSqlParameter[94].Value = objEmployeeMBO.IsPrincipal;

                pSqlParameter[95] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[95].Direction = ParameterDirection.Input;
                pSqlParameter[95].Value = objEmployeeMBO.IsDeleted;

                pSqlParameter[96] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[96].Direction = ParameterDirection.Input;
                pSqlParameter[96].Value = objEmployeeMBO.CreatedUserID;

                pSqlParameter[97] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[97].Direction = ParameterDirection.Input;
                pSqlParameter[97].Value = objEmployeeMBO.CreatedDate;

                pSqlParameter[98] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[98].Direction = ParameterDirection.Input;
                pSqlParameter[98].Value = objEmployeeMBO.LastModifiedUserID;

                pSqlParameter[99] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[99].Direction = ParameterDirection.Input;
                pSqlParameter[99].Value = objEmployeeMBO.LastModifiedDate;

                pSqlParameter[100] = new SqlParameter("@AllowAccountAccess", SqlDbType.Int);
                pSqlParameter[100].Direction = ParameterDirection.Input;
                pSqlParameter[100].Value = objEmployeeMBO.AllowAccountAccess;

                pSqlParameter[101] = new SqlParameter("@CategoryEng", SqlDbType.VarChar);
                pSqlParameter[101].Direction = ParameterDirection.Input;
                pSqlParameter[101].Value = objEmployeeMBO.CategoryEng;

                pSqlParameter[102] = new SqlParameter("@CategoryGuj", SqlDbType.NVarChar);
                pSqlParameter[102].Direction = ParameterDirection.Input;
                pSqlParameter[102].Value = objEmployeeMBO.CategoryGuj;

                pSqlParameter[103] = new SqlParameter("@ReportingTo", SqlDbType.Int);
                pSqlParameter[103].Direction = ParameterDirection.Input;
                pSqlParameter[103].Value = objEmployeeMBO.ReportingTo;

                pSqlParameter[104] = new SqlParameter("@TypeOfAppointmentCode", SqlDbType.VarChar);
                pSqlParameter[104].Direction = ParameterDirection.Input;
                pSqlParameter[104].Value = objEmployeeMBO.TypeOfAppointmentCode;

                pSqlParameter[105] = new SqlParameter("@AaharCardNo", SqlDbType.VarChar);
                pSqlParameter[105].Direction = ParameterDirection.Input;
                pSqlParameter[105].Value = objEmployeeMBO.AaharCardNo;

                pSqlParameter[106] = new SqlParameter("@ElectionCardNo", SqlDbType.VarChar);
                pSqlParameter[106].Direction = ParameterDirection.Input;
                pSqlParameter[106].Value = objEmployeeMBO.ElectionCardNo;

                pSqlParameter[107] = new SqlParameter("@VehicleNo", SqlDbType.VarChar);
                pSqlParameter[107].Direction = ParameterDirection.Input;
                pSqlParameter[107].Value = objEmployeeMBO.VehicleNo;

                pSqlParameter[108] = new SqlParameter("@PortalID", SqlDbType.VarChar);
                pSqlParameter[108].Direction = ParameterDirection.Input;
                pSqlParameter[108].Value = objEmployeeMBO.PortalID;

                pSqlParameter[109] = new SqlParameter("@PRANNo", SqlDbType.VarChar);
                pSqlParameter[109].Direction = ParameterDirection.Input;
                pSqlParameter[109].Value = objEmployeeMBO.PRANNo;

                pSqlParameter[110] = new SqlParameter("@TANNO", SqlDbType.VarChar);
                pSqlParameter[110].Direction = ParameterDirection.Input;
                pSqlParameter[110].Value = objEmployeeMBO.TANNO;

                pSqlParameter[111] = new SqlParameter("@LastWorkingDate", SqlDbType.VarChar);
                pSqlParameter[111].Direction = ParameterDirection.Input;
                pSqlParameter[111].Value = objEmployeeMBO.LASTWORKINGDATE;

                sSql = "usp_tbl_Employee_M_Insert";
                DataTable dtResult = new DataTable();
                dtResult = DatabaseTransaction.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objEmployeeMBO = null;
            }
        }
        #endregion
		
		#region Update EmployeeM Details
		/// <summary>
        /// To Update details of EmployeeM in tbl_Employee_M table
        /// Created By : Darshan, 09/12/2014
		/// Modified By :
        /// </summary>
        /// <param name="objEmployeeMBO"></param>
        /// <returns></returns>
        public ApplicationResult EmployeeM_Update(EmployeeMBO objEmployeeMBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[111];


                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objEmployeeMBO.EmployeeMID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objEmployeeMBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objEmployeeMBO.SchoolMID;

                pSqlParameter[3] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objEmployeeMBO.DepartmentID;

                pSqlParameter[4] = new SqlParameter("@DesignationID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objEmployeeMBO.DesignationID;

                pSqlParameter[5] = new SqlParameter("@EmployeeCode", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objEmployeeMBO.EmployeeCode;

                pSqlParameter[6] = new SqlParameter("@EmployeeFNameENG", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objEmployeeMBO.EmployeeFNameENG;

                pSqlParameter[7] = new SqlParameter("@EmployeeFNameGUJ", SqlDbType.NVarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objEmployeeMBO.EmployeeFNameGUJ;

                pSqlParameter[8] = new SqlParameter("@EmployeeMNameENG", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objEmployeeMBO.EmployeeMNameENG;

                pSqlParameter[9] = new SqlParameter("@EmployeeMNameGUJ", SqlDbType.NVarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objEmployeeMBO.EmployeeMNameGUJ;

                pSqlParameter[10] = new SqlParameter("@EmployeeLNameENG", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objEmployeeMBO.EmployeeLNameENG;

                pSqlParameter[11] = new SqlParameter("@EmployeeLNameGUJ", SqlDbType.NVarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objEmployeeMBO.EmployeeLNameGUJ;

                pSqlParameter[12] = new SqlParameter("@Photo", SqlDbType.Image);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objEmployeeMBO.Photo;

                pSqlParameter[13] = new SqlParameter("@Gender", SqlDbType.VarChar);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objEmployeeMBO.Gender;

                pSqlParameter[14] = new SqlParameter("@GenderGuj", SqlDbType.NVarChar);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objEmployeeMBO.GenderGuj;

                pSqlParameter[15] = new SqlParameter("@DateOfBirth", SqlDbType.VarChar);
                pSqlParameter[15].Direction = ParameterDirection.Input;
                pSqlParameter[15].Value = objEmployeeMBO.DateOfBirth;

                pSqlParameter[16] = new SqlParameter("@BirthDistrictENG", SqlDbType.VarChar);
                pSqlParameter[16].Direction = ParameterDirection.Input;
                pSqlParameter[16].Value = objEmployeeMBO.BirthDistrictENG;

                pSqlParameter[17] = new SqlParameter("@BirthDistrictGUJ", SqlDbType.NVarChar);
                pSqlParameter[17].Direction = ParameterDirection.Input;
                pSqlParameter[17].Value = objEmployeeMBO.BirthDistrictGUJ;

                pSqlParameter[18] = new SqlParameter("@BirthTalukaENG", SqlDbType.VarChar);
                pSqlParameter[18].Direction = ParameterDirection.Input;
                pSqlParameter[18].Value = objEmployeeMBO.BirthTalukaENG;

                pSqlParameter[19] = new SqlParameter("@BirthTalukaGUJ", SqlDbType.NVarChar);
                pSqlParameter[19].Direction = ParameterDirection.Input;
                pSqlParameter[19].Value = objEmployeeMBO.BirthTalukaGUJ;

                pSqlParameter[20] = new SqlParameter("@BirthCityVillageENG", SqlDbType.VarChar);
                pSqlParameter[20].Direction = ParameterDirection.Input;
                pSqlParameter[20].Value = objEmployeeMBO.BirthCityVillageENG;

                pSqlParameter[21] = new SqlParameter("@BirthCityVillageGUJ", SqlDbType.NVarChar);
                pSqlParameter[21].Direction = ParameterDirection.Input;
                pSqlParameter[21].Value = objEmployeeMBO.BirthCityVillageGUJ;

                pSqlParameter[22] = new SqlParameter("@NationalityENG", SqlDbType.VarChar);
                pSqlParameter[22].Direction = ParameterDirection.Input;
                pSqlParameter[22].Value = objEmployeeMBO.NationalityENG;

                pSqlParameter[23] = new SqlParameter("@NationalityGUJ", SqlDbType.NVarChar);
                pSqlParameter[23].Direction = ParameterDirection.Input;
                pSqlParameter[23].Value = objEmployeeMBO.NationalityGUJ;

                pSqlParameter[24] = new SqlParameter("@ReligionENG", SqlDbType.VarChar);
                pSqlParameter[24].Direction = ParameterDirection.Input;
                pSqlParameter[24].Value = objEmployeeMBO.ReligionENG;

                pSqlParameter[25] = new SqlParameter("@ReligionGUJ", SqlDbType.NVarChar);
                pSqlParameter[25].Direction = ParameterDirection.Input;
                pSqlParameter[25].Value = objEmployeeMBO.ReligionGUJ;

                pSqlParameter[26] = new SqlParameter("@Caste", SqlDbType.VarChar);
                pSqlParameter[26].Direction = ParameterDirection.Input;
                pSqlParameter[26].Value = objEmployeeMBO.Caste;

                pSqlParameter[27] = new SqlParameter("@MaritalStatus", SqlDbType.VarChar);
                pSqlParameter[27].Direction = ParameterDirection.Input;
                pSqlParameter[27].Value = objEmployeeMBO.MaritalStatus;

                pSqlParameter[28] = new SqlParameter("@BloodGroup", SqlDbType.VarChar);
                pSqlParameter[28].Direction = ParameterDirection.Input;
                pSqlParameter[28].Value = objEmployeeMBO.BloodGroup;

                pSqlParameter[29] = new SqlParameter("@MotherLanguage", SqlDbType.VarChar);
                pSqlParameter[29].Direction = ParameterDirection.Input;
                pSqlParameter[29].Value = objEmployeeMBO.MotherLanguage;

                pSqlParameter[30] = new SqlParameter("@CurrentAddressENG", SqlDbType.VarChar);
                pSqlParameter[30].Direction = ParameterDirection.Input;
                pSqlParameter[30].Value = objEmployeeMBO.CurrentAddressENG;

                pSqlParameter[31] = new SqlParameter("@CurrentAddressGUJ", SqlDbType.NVarChar);
                pSqlParameter[31].Direction = ParameterDirection.Input;
                pSqlParameter[31].Value = objEmployeeMBO.CurrentAddressGUJ;

                pSqlParameter[32] = new SqlParameter("@CurrentLandmarkENG", SqlDbType.VarChar);
                pSqlParameter[32].Direction = ParameterDirection.Input;
                pSqlParameter[32].Value = objEmployeeMBO.CurrentLandmarkENG;

                pSqlParameter[33] = new SqlParameter("@CurrentLandmarkGUJ", SqlDbType.NVarChar);
                pSqlParameter[33].Direction = ParameterDirection.Input;
                pSqlParameter[33].Value = objEmployeeMBO.CurrentLandmarkGUJ;

                pSqlParameter[34] = new SqlParameter("@CurrentCityENG", SqlDbType.VarChar);
                pSqlParameter[34].Direction = ParameterDirection.Input;
                pSqlParameter[34].Value = objEmployeeMBO.CurrentCityENG;

                pSqlParameter[35] = new SqlParameter("@CurrentCityGUJ", SqlDbType.NVarChar);
                pSqlParameter[35].Direction = ParameterDirection.Input;
                pSqlParameter[35].Value = objEmployeeMBO.CurrentCityGUJ;

                pSqlParameter[36] = new SqlParameter("@CurrentStateENG", SqlDbType.VarChar);
                pSqlParameter[36].Direction = ParameterDirection.Input;
                pSqlParameter[36].Value = objEmployeeMBO.CurrentStateENG;

                pSqlParameter[37] = new SqlParameter("@CurrentStateGUJ", SqlDbType.NVarChar);
                pSqlParameter[37].Direction = ParameterDirection.Input;
                pSqlParameter[37].Value = objEmployeeMBO.CurrentStateGUJ;

                pSqlParameter[38] = new SqlParameter("@CurrentPinCode", SqlDbType.VarChar);
                pSqlParameter[38].Direction = ParameterDirection.Input;
                pSqlParameter[38].Value = objEmployeeMBO.CurrentPinCode;

                pSqlParameter[39] = new SqlParameter("@PermenantAddressEng", SqlDbType.VarChar);
                pSqlParameter[39].Direction = ParameterDirection.Input;
                pSqlParameter[39].Value = objEmployeeMBO.PermenantAddressEng;

                pSqlParameter[40] = new SqlParameter("@PermenantAddressGuj", SqlDbType.NVarChar);
                pSqlParameter[40].Direction = ParameterDirection.Input;
                pSqlParameter[40].Value = objEmployeeMBO.PermenantAddressGuj;

                pSqlParameter[41] = new SqlParameter("@PermenantLandmarkEng", SqlDbType.VarChar);
                pSqlParameter[41].Direction = ParameterDirection.Input;
                pSqlParameter[41].Value = objEmployeeMBO.PermenantLandmarkEng;

                pSqlParameter[42] = new SqlParameter("@PermenantLandmarkGuj", SqlDbType.NVarChar);
                pSqlParameter[42].Direction = ParameterDirection.Input;
                pSqlParameter[42].Value = objEmployeeMBO.PermenantLandmarkGuj;

                pSqlParameter[43] = new SqlParameter("@PermenantCityEng", SqlDbType.VarChar);
                pSqlParameter[43].Direction = ParameterDirection.Input;
                pSqlParameter[43].Value = objEmployeeMBO.PermenantCityEng;

                pSqlParameter[44] = new SqlParameter("@PermenantCityGuj", SqlDbType.NVarChar);
                pSqlParameter[44].Direction = ParameterDirection.Input;
                pSqlParameter[44].Value = objEmployeeMBO.PermenantCityGuj;

                pSqlParameter[45] = new SqlParameter("@PermenantStateEng", SqlDbType.VarChar);
                pSqlParameter[45].Direction = ParameterDirection.Input;
                pSqlParameter[45].Value = objEmployeeMBO.PermenantStateEng;

                pSqlParameter[46] = new SqlParameter("@PermenantStateGuj", SqlDbType.NVarChar);
                pSqlParameter[46].Direction = ParameterDirection.Input;
                pSqlParameter[46].Value = objEmployeeMBO.PermenantStateGuj;

                pSqlParameter[47] = new SqlParameter("@PermenantPincode", SqlDbType.VarChar);
                pSqlParameter[47].Direction = ParameterDirection.Input;
                pSqlParameter[47].Value = objEmployeeMBO.PermenantPincode;

                pSqlParameter[48] = new SqlParameter("@TelephoneNo", SqlDbType.VarChar);
                pSqlParameter[48].Direction = ParameterDirection.Input;
                pSqlParameter[48].Value = objEmployeeMBO.TelephoneNo;

                pSqlParameter[49] = new SqlParameter("@MobileNo", SqlDbType.VarChar);
                pSqlParameter[49].Direction = ParameterDirection.Input;
                pSqlParameter[49].Value = objEmployeeMBO.MobileNo;

                pSqlParameter[50] = new SqlParameter("@EmailId", SqlDbType.VarChar);
                pSqlParameter[50].Direction = ParameterDirection.Input;
                pSqlParameter[50].Value = objEmployeeMBO.EmailId;

                pSqlParameter[51] = new SqlParameter("@Hobbies", SqlDbType.VarChar);
                pSqlParameter[51].Direction = ParameterDirection.Input;
                pSqlParameter[51].Value = objEmployeeMBO.Hobbies;

                pSqlParameter[52] = new SqlParameter("@RightVision", SqlDbType.VarChar);
                pSqlParameter[52].Direction = ParameterDirection.Input;
                pSqlParameter[52].Value = objEmployeeMBO.RightVision;

                pSqlParameter[53] = new SqlParameter("@LeftVision", SqlDbType.VarChar);
                pSqlParameter[53].Direction = ParameterDirection.Input;
                pSqlParameter[53].Value = objEmployeeMBO.LeftVision;

                pSqlParameter[54] = new SqlParameter("@RectificationDevice", SqlDbType.VarChar);
                pSqlParameter[54].Direction = ParameterDirection.Input;
                pSqlParameter[54].Value = objEmployeeMBO.RectificationDevice;

                pSqlParameter[55] = new SqlParameter("@Height", SqlDbType.VarChar);
                pSqlParameter[55].Direction = ParameterDirection.Input;
                pSqlParameter[55].Value = objEmployeeMBO.Height;

                pSqlParameter[56] = new SqlParameter("@Weight", SqlDbType.VarChar);
                pSqlParameter[56].Direction = ParameterDirection.Input;
                pSqlParameter[56].Value = objEmployeeMBO.Weight;

                pSqlParameter[57] = new SqlParameter("@PhysicalIdentificationENG", SqlDbType.VarChar);
                pSqlParameter[57].Direction = ParameterDirection.Input;
                pSqlParameter[57].Value = objEmployeeMBO.PhysicalIdentificationENG;

                pSqlParameter[58] = new SqlParameter("@PhysicalIdentificationGUJ", SqlDbType.NVarChar);
                pSqlParameter[58].Direction = ParameterDirection.Input;
                pSqlParameter[58].Value = objEmployeeMBO.PhysicalIdentificationGUJ;

                pSqlParameter[59] = new SqlParameter("@MotherMaidenFNameENG", SqlDbType.VarChar);
                pSqlParameter[59].Direction = ParameterDirection.Input;
                pSqlParameter[59].Value = objEmployeeMBO.MotherMaidenFNameENG;

                pSqlParameter[60] = new SqlParameter("@MotherMaidenFNameGUJ", SqlDbType.NVarChar);
                pSqlParameter[60].Direction = ParameterDirection.Input;
                pSqlParameter[60].Value = objEmployeeMBO.MotherMaidenFNameGUJ;

                pSqlParameter[61] = new SqlParameter("@MotherMaidenMNameENG", SqlDbType.VarChar);
                pSqlParameter[61].Direction = ParameterDirection.Input;
                pSqlParameter[61].Value = objEmployeeMBO.MotherMaidenMNameENG;

                pSqlParameter[62] = new SqlParameter("@MotherMaidenMNameGUJ", SqlDbType.NVarChar);
                pSqlParameter[62].Direction = ParameterDirection.Input;
                pSqlParameter[62].Value = objEmployeeMBO.MotherMaidenMNameGUJ;

                pSqlParameter[63] = new SqlParameter("@MotherMaidenLNameENG", SqlDbType.VarChar);
                pSqlParameter[63].Direction = ParameterDirection.Input;
                pSqlParameter[63].Value = objEmployeeMBO.MotherMaidenLNameENG;

                pSqlParameter[64] = new SqlParameter("@MotherMaidenLNameGUJ", SqlDbType.NVarChar);
                pSqlParameter[64].Direction = ParameterDirection.Input;
                pSqlParameter[64].Value = objEmployeeMBO.MotherMaidenLNameGUJ;

                pSqlParameter[65] = new SqlParameter("@BankName", SqlDbType.VarChar);
                pSqlParameter[65].Direction = ParameterDirection.Input;
                pSqlParameter[65].Value = objEmployeeMBO.BankName;

                pSqlParameter[66] = new SqlParameter("@BranchName", SqlDbType.VarChar);
                pSqlParameter[66].Direction = ParameterDirection.Input;
                pSqlParameter[66].Value = objEmployeeMBO.BranchName;

                pSqlParameter[67] = new SqlParameter("@BranchCode", SqlDbType.VarChar);
                pSqlParameter[67].Direction = ParameterDirection.Input;
                pSqlParameter[67].Value = objEmployeeMBO.BranchCode;

                pSqlParameter[68] = new SqlParameter("@AccountNo", SqlDbType.VarChar);
                pSqlParameter[68].Direction = ParameterDirection.Input;
                pSqlParameter[68].Value = objEmployeeMBO.AccountNo;

                pSqlParameter[69] = new SqlParameter("@PFNo", SqlDbType.VarChar);
                pSqlParameter[69].Direction = ParameterDirection.Input;
                pSqlParameter[69].Value = objEmployeeMBO.PFNo;

                pSqlParameter[70] = new SqlParameter("@PANNo", SqlDbType.VarChar);
                pSqlParameter[70].Direction = ParameterDirection.Input;
                pSqlParameter[70].Value = objEmployeeMBO.PANNo;

                pSqlParameter[71] = new SqlParameter("@ESICNo", SqlDbType.VarChar);
                pSqlParameter[71].Direction = ParameterDirection.Input;
                pSqlParameter[71].Value = objEmployeeMBO.ESICNo;

                pSqlParameter[72] = new SqlParameter("@IFSCCode", SqlDbType.VarChar);
                pSqlParameter[72].Direction = ParameterDirection.Input;
                pSqlParameter[72].Value = objEmployeeMBO.IFSCCode;

                pSqlParameter[73] = new SqlParameter("@GPFAccountNo", SqlDbType.VarChar);
                pSqlParameter[73].Direction = ParameterDirection.Input;
                pSqlParameter[73].Value = objEmployeeMBO.GPFAccountNo;

                pSqlParameter[74] = new SqlParameter("@CPFAccountNo", SqlDbType.VarChar);
                pSqlParameter[74].Direction = ParameterDirection.Input;
                pSqlParameter[74].Value = objEmployeeMBO.CPFAccountNo;

                pSqlParameter[75] = new SqlParameter("@DepartmentJoiningDate", SqlDbType.VarChar);
                pSqlParameter[75].Direction = ParameterDirection.Input;
                pSqlParameter[75].Value = objEmployeeMBO.DepartmentJoiningDate;

                pSqlParameter[76] = new SqlParameter("@OrganisationJoiningDate", SqlDbType.VarChar);
                pSqlParameter[76].Direction = ParameterDirection.Input;
                pSqlParameter[76].Value = objEmployeeMBO.OrganisationJoiningDate;

                pSqlParameter[77] = new SqlParameter("@TypeOfAppointment", SqlDbType.VarChar);
                pSqlParameter[77].Direction = ParameterDirection.Input;
                pSqlParameter[77].Value = objEmployeeMBO.TypeOfAppointment;

                pSqlParameter[78] = new SqlParameter("@ReplacementSchoolInfoENG", SqlDbType.VarChar);
                pSqlParameter[78].Direction = ParameterDirection.Input;
                pSqlParameter[78].Value = objEmployeeMBO.ReplacementSchoolInfoENG;

                pSqlParameter[79] = new SqlParameter("@ReplacementSchoolInfoGUJ", SqlDbType.NVarChar);
                pSqlParameter[79].Direction = ParameterDirection.Input;
                pSqlParameter[79].Value = objEmployeeMBO.ReplacementSchoolInfoGUJ;

                pSqlParameter[80] = new SqlParameter("@RetirementDate", SqlDbType.VarChar);
                pSqlParameter[80].Direction = ParameterDirection.Input;
                pSqlParameter[80].Value = objEmployeeMBO.RetirementDate;

                pSqlParameter[81] = new SqlParameter("@TermEndRetirementDate", SqlDbType.VarChar);
                pSqlParameter[81].Direction = ParameterDirection.Input;
                pSqlParameter[81].Value = objEmployeeMBO.TermEndRetirementDate;

                pSqlParameter[82] = new SqlParameter("@IsResigned", SqlDbType.Int);
                pSqlParameter[82].Direction = ParameterDirection.Input;
                pSqlParameter[82].Value = objEmployeeMBO.IsResigned;

                pSqlParameter[83] = new SqlParameter("@ResignedDate", SqlDbType.VarChar);
                pSqlParameter[83].Direction = ParameterDirection.Input;
                pSqlParameter[83].Value = objEmployeeMBO.ResignedDate;

                pSqlParameter[84] = new SqlParameter("@BreakInfoENG", SqlDbType.VarChar);
                pSqlParameter[84].Direction = ParameterDirection.Input;
                pSqlParameter[84].Value = objEmployeeMBO.BreakInfoENG;

                pSqlParameter[85] = new SqlParameter("@BreakInfoGUJ", SqlDbType.NVarChar);
                pSqlParameter[85].Direction = ParameterDirection.Input;
                pSqlParameter[85].Value = objEmployeeMBO.BreakInfoGUJ;

                pSqlParameter[86] = new SqlParameter("@ResignReasonEng", SqlDbType.VarChar);
                pSqlParameter[86].Direction = ParameterDirection.Input;
                pSqlParameter[86].Value = objEmployeeMBO.ResignReasonEng;

                pSqlParameter[87] = new SqlParameter("@ResignReasonGuj", SqlDbType.NVarChar);
                pSqlParameter[87].Direction = ParameterDirection.Input;
                pSqlParameter[87].Value = objEmployeeMBO.ResignReasonGuj;

                pSqlParameter[88] = new SqlParameter("@OtherAchivementDetailsENG", SqlDbType.VarChar);
                pSqlParameter[88].Direction = ParameterDirection.Input;
                pSqlParameter[88].Value = objEmployeeMBO.OtherAchivementDetailsENG;

                pSqlParameter[89] = new SqlParameter("@OtherAchivementDetailsGUJ", SqlDbType.NVarChar);
                pSqlParameter[89].Direction = ParameterDirection.Input;
                pSqlParameter[89].Value = objEmployeeMBO.OtherAchivementDetailsGUJ;

                pSqlParameter[90] = new SqlParameter("@IsUser", SqlDbType.Int);
                pSqlParameter[90].Direction = ParameterDirection.Input;
                pSqlParameter[90].Value = objEmployeeMBO.IsUser;

                pSqlParameter[91] = new SqlParameter("@UserName", SqlDbType.VarChar);
                pSqlParameter[91].Direction = ParameterDirection.Input;
                pSqlParameter[91].Value = objEmployeeMBO.UserName;

                pSqlParameter[92] = new SqlParameter("@Password", SqlDbType.VarChar);
                pSqlParameter[92].Direction = ParameterDirection.Input;
                pSqlParameter[92].Value = objEmployeeMBO.Password;

                pSqlParameter[93] = new SqlParameter("@RoleID", SqlDbType.Int);
                pSqlParameter[93].Direction = ParameterDirection.Input;
                pSqlParameter[93].Value = objEmployeeMBO.RoleID;

                pSqlParameter[94] = new SqlParameter("@IsTeacher", SqlDbType.Int);
                pSqlParameter[94].Direction = ParameterDirection.Input;
                pSqlParameter[94].Value = objEmployeeMBO.IsTeacher;

                pSqlParameter[95] = new SqlParameter("@IsPrincipal", SqlDbType.Int);
                pSqlParameter[95].Direction = ParameterDirection.Input;
                pSqlParameter[95].Value = objEmployeeMBO.IsPrincipal;

                pSqlParameter[96] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[96].Direction = ParameterDirection.Input;
                pSqlParameter[96].Value = objEmployeeMBO.IsDeleted;

                pSqlParameter[97] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[97].Direction = ParameterDirection.Input;
                pSqlParameter[97].Value = objEmployeeMBO.LastModifiedUserID;

                pSqlParameter[98] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[98].Direction = ParameterDirection.Input;
                pSqlParameter[98].Value = objEmployeeMBO.LastModifiedDate;

                pSqlParameter[99] = new SqlParameter("@AllowAccountAccess", SqlDbType.Int);
                pSqlParameter[99].Direction = ParameterDirection.Input;
                pSqlParameter[99].Value = objEmployeeMBO.AllowAccountAccess;

                pSqlParameter[100] = new SqlParameter("@CategoryEng", SqlDbType.VarChar);
                pSqlParameter[100].Direction = ParameterDirection.Input;
                pSqlParameter[100].Value = objEmployeeMBO.CategoryEng;

                pSqlParameter[101] = new SqlParameter("@CategoryGuj", SqlDbType.NVarChar);
                pSqlParameter[101].Direction = ParameterDirection.Input;
                pSqlParameter[101].Value = objEmployeeMBO.CategoryGuj;

                pSqlParameter[102] = new SqlParameter("@ReportingTo", SqlDbType.Int);
                pSqlParameter[102].Direction = ParameterDirection.Input;
                pSqlParameter[102].Value = objEmployeeMBO.ReportingTo;

                pSqlParameter[103] = new SqlParameter("@TypeOfAppointmentCode", SqlDbType.VarChar);
                pSqlParameter[103].Direction = ParameterDirection.Input;
                pSqlParameter[103].Value = objEmployeeMBO.TypeOfAppointmentCode;

                pSqlParameter[104] = new SqlParameter("@AadharCardNo", SqlDbType.VarChar);
                pSqlParameter[104].Direction = ParameterDirection.Input;
                pSqlParameter[104].Value = objEmployeeMBO.AaharCardNo;

                pSqlParameter[105] = new SqlParameter("@ElectionCardNo", SqlDbType.VarChar);
                pSqlParameter[105].Direction = ParameterDirection.Input;
                pSqlParameter[105].Value = objEmployeeMBO.ElectionCardNo;

                pSqlParameter[106] = new SqlParameter("@VehicleNo", SqlDbType.VarChar);
                pSqlParameter[106].Direction = ParameterDirection.Input;
                pSqlParameter[106].Value = objEmployeeMBO.VehicleNo;

                pSqlParameter[107] = new SqlParameter("@PortalId", SqlDbType.VarChar);
                pSqlParameter[107].Direction = ParameterDirection.Input;
                pSqlParameter[107].Value = objEmployeeMBO.PortalID;

                pSqlParameter[108] = new SqlParameter("@PRANNo", SqlDbType.VarChar);
                pSqlParameter[108].Direction = ParameterDirection.Input;
                pSqlParameter[108].Value = objEmployeeMBO.PRANNo;

                pSqlParameter[109] = new SqlParameter("@TANNo", SqlDbType.VarChar);
                pSqlParameter[109].Direction = ParameterDirection.Input;
                pSqlParameter[109].Value = objEmployeeMBO.TANNO;


                pSqlParameter[110] = new SqlParameter("@LastWorkingDate", SqlDbType.VarChar);
                pSqlParameter[110].Direction = ParameterDirection.Input;
                pSqlParameter[110].Value = objEmployeeMBO.LASTWORKINGDATE;


                sSql = "usp_tbl_Employee_M_Update";
                int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, sSql, pSqlParameter);

                if (iResult > 0)
                {
                    ApplicationResult objResults = new ApplicationResult();
                    objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                    return objResults;
                }
                else
                {
                    ApplicationResult objResults = new ApplicationResult();
                    objResults.status = ApplicationResult.CommonStatusType.FAILURE;
                    return objResults;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objEmployeeMBO = null;
            }
        }
        #endregion

        #region Insert Employee Deprt Transfer History
        public ApplicationResult EmployeeDeptTransferHistoryT_Insert(EmployeeDeptTransferHistoryTBO objEmployeeDeptTransferHistoryBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[10];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objEmployeeDeptTransferHistoryBO.EmployeeMID;

                pSqlParameter[1] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objEmployeeDeptTransferHistoryBO.DepartmentID;

                pSqlParameter[2] = new SqlParameter("@DesignationID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objEmployeeDeptTransferHistoryBO.DesignationID;

                pSqlParameter[3] = new SqlParameter("@EmployeeCode", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objEmployeeDeptTransferHistoryBO.EmployeeCode;

                pSqlParameter[4] = new SqlParameter("@DepartmentJoiningDate", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objEmployeeDeptTransferHistoryBO.DepartmentJoiningDate;

                pSqlParameter[5] = new SqlParameter("@ReportingTo", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objEmployeeDeptTransferHistoryBO.ReportingTo;

                pSqlParameter[6] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objEmployeeDeptTransferHistoryBO.CreatedByID;

                pSqlParameter[7] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objEmployeeDeptTransferHistoryBO.CreatedDate;

                pSqlParameter[8] = new SqlParameter("@ModifiedByID", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objEmployeeDeptTransferHistoryBO.ModifiedByID;

                pSqlParameter[9] = new SqlParameter("@ModifiedDate", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objEmployeeDeptTransferHistoryBO.ModifiedDate;

                sSql = "usp_tbl_EmployeeDeptTrasnferHistory_T_Insert";
                int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, sSql, pSqlParameter);

                if (iResult > 0)
                {
                    ApplicationResult objResults = new ApplicationResult();
                    objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                    return objResults;
                }
                else
                {
                    ApplicationResult objResults = new ApplicationResult();
                    objResults.status = ApplicationResult.CommonStatusType.FAILURE;
                    return objResults;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objEmployeeDeptTransferHistoryBO = null;
            }
           


        }
        #endregion

        #region Check Login for Employee
        /// Function which validates whether the EmployeeName already exits in tbl_Employee_M table.
        /// Created By : Chintan, 27-06-2014
        /// Modified By :
        public ApplicationResult Employee_CheckForLogin(string strUserName, string strPassword)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@UserName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strUserName;

                pSqlParameter[1] = new SqlParameter("@Password", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strPassword;

                strStoredProcName = "usp_tbl_Employee_CheckForLogin";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Check Login for Employee
        /// Function which validates whether the EmployeeName already exits in tbl_Employee_M table.
        /// Created By : Chintan, 27-06-2014
        /// Modified By :
        public ApplicationResult Employee_CheckForLoginAccount(string strUserName, string strPassword, int intTrustID, int intSchoolID)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@UserName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strUserName;

                pSqlParameter[1] = new SqlParameter("@Password", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strPassword;

                pSqlParameter[2] = new SqlParameter("@TrustMID", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intTrustID;

                pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intSchoolID;

                strStoredProcName = "usp_tbl_Employee_M_CheckAccountLogin";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select EmployeeName by SearchText
        /// Select all details of EmployeeM for selected EmployeeMID from tbl_Employee_M table
        /// Created By : Darshan, 09/12/2014
        /// Modified By :
        public ApplicationResult EmployeeM_Select_ForAutoComplete(string strSearch, int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@Search", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strSearch;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_Employee_M_ForAutoComplete";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select EmployeeInformation For Report
        /// Select all details of EmployeeM for selected EmployeeMID from tbl_Employee_M table
        /// Created By : Darshan, 09/12/2014
        /// Modified By :
        public ApplicationResult EmployeeM_Select_InformationForReport(int intEmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                strStoredProcName = "usp_tbl_Employee_M_InformationForReport";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select EmployeeInformation For Report
        /// Select all details of EmployeeM for selected EmployeeMID from tbl_Employee_M table
        /// Created By : Darshan, 09/12/2014
        /// Modified By :
        public ApplicationResult EmployeeM_Select_ForList(int intTrustMID, int intSchoolMID, int intDepartmentID, int intDesignationID, int intRoleID)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                pSqlParameter[2] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intDepartmentID;

                pSqlParameter[3] = new SqlParameter("@DesignationID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intDesignationID;

                pSqlParameter[4] = new SqlParameter("@RoleID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intRoleID;

                strStoredProcName = "usp_tbl_Employee_M_ListReport";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select Process Pay ROll
        /// Select all details of EmployeeM for selected EmployeeMID from tbl_Employee_M table
        /// Created By : Darshan, 09/12/2014
        /// Modified By :
        public ApplicationResult SelectEmployee_For_ProcessPayRoll(int intEmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                strStoredProcName = "usp_tbl_ProcessPayroll_M_SelectForEmployee";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select EmployeeM Details by EmployeeMID
        /// <summary>
        /// Select all details of EmployeeM for selected EmployeeMID from tbl_Employee_M table
        /// Created By : Darshan, 09/12/2014
        /// Modified By :
        /// </summary>
        /// <param name="intEmployeeMID"></param>
        /// <returns></returns>
        public ApplicationResult EmployeeMDetail_ForCategoryWiseReport(int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                strStoredProcName = "usp_rpt_EmployeeDetail_CategoryWise";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select EmployeeM Details 
        /// <summary>
        /// To Delete details of EmployeeM for selected EmployeeMID from tbl_Employee_M table
        /// Created By : Darshan, 09/12/2014
        /// Modified By :
        /// </summary>
        /// <param name="intEmployeeMID"></param>
        /// <returns></returns>
        public ApplicationResult Select_Employee_ForAttandance(int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_Employee_M_Select_ForAttadence";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select EmployeeM Details 
        /// <summary>
        /// To Delete details of EmployeeM for selected EmployeeMID from tbl_Employee_M table
        /// Created By : Darshan, 09/12/2014
        /// Modified By :
        /// </summary>
        /// <param name="intEmployeeMID"></param>
        /// <returns></returns>
        public ApplicationResult Select_Employee_ByDepartment_ForAttandance(int intTrustMID, int intSchoolMID,int intDepartment)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                pSqlParameter[2] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intDepartment;

                strStoredProcName = "usp_tbl_Employee_M_Select_ByDepartment_ForAttadence";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select EmployeeM Details 
        /// <summary>
        /// To Delete details of EmployeeM for selected EmployeeMID from tbl_Employee_M table
        /// Created By : Darshan, 09/12/2014
        /// Modified By :
        /// </summary>
        /// <param name="intEmployeeMID"></param>
        /// <returns></returns>
        public ApplicationResult Select_Employee_BySchool_ForAttandance(int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

               

                strStoredProcName = "usp_tbl_Employee_M_Select_BySchool_ForAttadence";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select Employee For Report
        /// <summary>
        /// Select all details of StudentT for selected StudentTID from StudentT table
        /// Created By : NafisaMulla, 12/6/2014
        /// Modified By :
        /// </summary>
        /// <param name="intStudentTID"></param>
        /// <returns></returns>
        public ApplicationResult EmployeeDetail_ForCategoryWiseReport(int intTrustMID, int intSchoolMID, string strAcademicYear, int intStatusID)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                pSqlParameter[2] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strAcademicYear;

                pSqlParameter[3] = new SqlParameter("@DesignationID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intStatusID;

                pSqlParameter[4] = new SqlParameter("@Type", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intStatusID;

                strStoredProcName = "usp_Rpt_CountEmployeeGendereWise";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
     
	}
}
