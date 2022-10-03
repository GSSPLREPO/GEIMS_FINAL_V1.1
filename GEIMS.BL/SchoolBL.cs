using System;
using System.Data;
using System.Data.SqlClient;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
	/// <summary>
	/// Class Created By : NafisaMulla, 12/6/2014
	/// Summary description for Organisation.
	/// </summary>
	public class SchoolBL
	{
		#region user defined variables
		public string sSql;
		public string strStoredProcName;
		public SqlParameter[] pSqlParameter = null;
		#endregion

        #region Select All School Details
        /// <summary>
        /// To Select All data from the tbl_School_M table
        /// Created By : NafisaMulla, 23/6/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult School_SelectAll(int intTrustID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustID;

                sSql = "usp_tbl_School_M_SelectAll";

                DataTable dtSchool = new DataTable();
                dtSchool = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtSchool);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		#endregion

		#region Select All School_SchoolMID Wise
		public ApplicationResult School_Select_SchoolMID(int intTrustID, int intSchoolID)
		{
			try
			{
				pSqlParameter = new SqlParameter[2];

				pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intTrustID;

				pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = intSchoolID;

				sSql = "usp_tbl_School_M_Select_SchoolMID";

				DataTable dtSchool = new DataTable();
				dtSchool = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

				ApplicationResult objResults = new ApplicationResult(dtSchool);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region Select All School Details
		/// <summary>
		/// To Select All data from the tbl_School_M table
		/// Created By : NafisaMulla, 23/6/2014
		/// Modified By :
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public ApplicationResult School_SelectAll_ForDropDOwn(int intTrustID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustID;

                sSql = "usp_tbl_School_M_SelectAll_ForDropdown";

                DataTable dtSchool = new DataTable();
                dtSchool = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtSchool);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select All School Details for all without trust
        /// <summary>
        /// To Select All data from the tbl_School_M table
        /// Created By : NafisaMulla, 23/6/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult School_SelectAll_All()
        {
            try
            {
                sSql = "usp_tbl_SChool_M_SelectAll_All";

                DataTable dtSchool = new DataTable();
                dtSchool = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtSchool);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

		#region Select School Details by SchooMID
		/// <summary>
		/// Select all details of School for selected SchooMID from tbl_School_M table
		/// Created By : NafisaMulla, 23/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="intSchooMID"></param>
		/// <returns></returns>
		public ApplicationResult School_Select(int intSchoolMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intSchoolMID;

				strStoredProcName = "usp_tbl_School_M_Select";

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

		#region Delete School Details by SchoolMID
		/// <summary>
		/// To Delete details of School for selected SchooMID from tbl_School_M table
		/// Created By : NafisaMulla, 23/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="intSchooMID"></param>
		/// <returns></returns>
		public ApplicationResult School_Delete(int intSchoolMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intSchoolMID;

				strStoredProcName = "usp_tbl_School_M_Delete";

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

		#region Insert School Details
		/// <summary>
		/// To Insert details of School in tbl_School_M table
		/// Created By : NafisaMulla, 17/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="objSchoolBO"></param>
		/// <returns></returns>
		public ApplicationResult School_Insert(SchoolBO objSchoolBO)
		{
			try
			{
                pSqlParameter = new SqlParameter[65];


                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objSchoolBO.TrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolNameEng", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objSchoolBO.SchoolNameEng;

                pSqlParameter[2] = new SqlParameter("@SchoolNameGuj", SqlDbType.NVarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objSchoolBO.SchoolNameGuj;

                pSqlParameter[3] = new SqlParameter("@SchoolCode", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objSchoolBO.SchoolCode;

                pSqlParameter[4] = new SqlParameter("@SchoolTiming", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objSchoolBO.SchoolTiming;

                pSqlParameter[5] = new SqlParameter("@AcademicMonth", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objSchoolBO.AcademicMonth;

                pSqlParameter[6] = new SqlParameter("@SchoolLogo", SqlDbType.Image);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objSchoolBO.SchoolLogo;

                pSqlParameter[7] = new SqlParameter("@SchoolAbbreviation", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objSchoolBO.SchoolAbbreviation;

                pSqlParameter[8] = new SqlParameter("@AddressEng", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objSchoolBO.AddressEng;

                pSqlParameter[9] = new SqlParameter("@AddressGuj", SqlDbType.NVarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objSchoolBO.AddressGuj;

                pSqlParameter[10] = new SqlParameter("@AreaType", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objSchoolBO.AreaType;

                pSqlParameter[11] = new SqlParameter("@AreaSubType", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objSchoolBO.AreaSubType;

                pSqlParameter[12] = new SqlParameter("@TownEng", SqlDbType.VarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objSchoolBO.TownEng;

                pSqlParameter[13] = new SqlParameter("@TownGuj", SqlDbType.NVarChar);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objSchoolBO.TownGuj;

                pSqlParameter[14] = new SqlParameter("@AtPoNoEng", SqlDbType.VarChar);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objSchoolBO.AtPoNoEng;

                pSqlParameter[15] = new SqlParameter("@AtPoNoGuj", SqlDbType.NVarChar);
                pSqlParameter[15].Direction = ParameterDirection.Input;
                pSqlParameter[15].Value = objSchoolBO.AtPoNoGuj;

                pSqlParameter[16] = new SqlParameter("@TalukaEng", SqlDbType.VarChar);
                pSqlParameter[16].Direction = ParameterDirection.Input;
                pSqlParameter[16].Value = objSchoolBO.TalukaEng;

                pSqlParameter[17] = new SqlParameter("@TalukaGuj", SqlDbType.NVarChar);
                pSqlParameter[17].Direction = ParameterDirection.Input;
                pSqlParameter[17].Value = objSchoolBO.TalukaGuj;

                pSqlParameter[18] = new SqlParameter("@DistrictEng", SqlDbType.VarChar);
                pSqlParameter[18].Direction = ParameterDirection.Input;
                pSqlParameter[18].Value = objSchoolBO.DistrictEng;

                pSqlParameter[19] = new SqlParameter("@DistrictGuj", SqlDbType.NVarChar);
                pSqlParameter[19].Direction = ParameterDirection.Input;
                pSqlParameter[19].Value = objSchoolBO.DistrictGuj;

                pSqlParameter[20] = new SqlParameter("@StateEng", SqlDbType.VarChar);
                pSqlParameter[20].Direction = ParameterDirection.Input;
                pSqlParameter[20].Value = objSchoolBO.StateEng;

                pSqlParameter[21] = new SqlParameter("@StateGuj", SqlDbType.NVarChar);
                pSqlParameter[21].Direction = ParameterDirection.Input;
                pSqlParameter[21].Value = objSchoolBO.StateGuj;

                pSqlParameter[22] = new SqlParameter("@CountryEng", SqlDbType.VarChar);
                pSqlParameter[22].Direction = ParameterDirection.Input;
                pSqlParameter[22].Value = objSchoolBO.CountryEng;

                pSqlParameter[23] = new SqlParameter("@CountryGuj", SqlDbType.NVarChar);
                pSqlParameter[23].Direction = ParameterDirection.Input;
                pSqlParameter[23].Value = objSchoolBO.CountryGuj;

                pSqlParameter[24] = new SqlParameter("@Pincode", SqlDbType.VarChar);
                pSqlParameter[24].Direction = ParameterDirection.Input;
                pSqlParameter[24].Value = objSchoolBO.Pincode;

                pSqlParameter[25] = new SqlParameter("@TelephoneNo", SqlDbType.VarChar);
                pSqlParameter[25].Direction = ParameterDirection.Input;
                pSqlParameter[25].Value = objSchoolBO.TelephoneNo;

                pSqlParameter[26] = new SqlParameter("@MobileNo", SqlDbType.VarChar);
                pSqlParameter[26].Direction = ParameterDirection.Input;
                pSqlParameter[26].Value = objSchoolBO.MobileNo;

                pSqlParameter[27] = new SqlParameter("@EmailID", SqlDbType.VarChar);
                pSqlParameter[27].Direction = ParameterDirection.Input;
                pSqlParameter[27].Value = objSchoolBO.EmailID;

                pSqlParameter[28] = new SqlParameter("@AlternateEmailID", SqlDbType.VarChar);
                pSqlParameter[28].Direction = ParameterDirection.Input;
                pSqlParameter[28].Value = objSchoolBO.AlternateEmailID;

                pSqlParameter[29] = new SqlParameter("@FaxNo", SqlDbType.VarChar);
                pSqlParameter[29].Direction = ParameterDirection.Input;
                pSqlParameter[29].Value = objSchoolBO.FaxNo;

                pSqlParameter[30] = new SqlParameter("@Website", SqlDbType.VarChar);
                pSqlParameter[30].Direction = ParameterDirection.Input;
                pSqlParameter[30].Value = objSchoolBO.Website;

                pSqlParameter[31] = new SqlParameter("@ApprovalNo", SqlDbType.VarChar);
                pSqlParameter[31].Direction = ParameterDirection.Input;
                pSqlParameter[31].Value = objSchoolBO.ApprovalNo;

                pSqlParameter[32] = new SqlParameter("@ApprovalDate", SqlDbType.VarChar);
                pSqlParameter[32].Direction = ParameterDirection.Input;
                pSqlParameter[32].Value = objSchoolBO.ApprovalDate;

                pSqlParameter[33] = new SqlParameter("@ApprovalYear", SqlDbType.VarChar);
                pSqlParameter[33].Direction = ParameterDirection.Input;
                pSqlParameter[33].Value = objSchoolBO.ApprovalYear;

                pSqlParameter[34] = new SqlParameter("@SSCindexNo", SqlDbType.VarChar);
                pSqlParameter[34].Direction = ParameterDirection.Input;
                pSqlParameter[34].Value = objSchoolBO.SSCindexNo;

                pSqlParameter[35] = new SqlParameter("@HSCScienceIndexNo", SqlDbType.VarChar);
                pSqlParameter[35].Direction = ParameterDirection.Input;
                pSqlParameter[35].Value = objSchoolBO.HSCScienceIndexNo;

                pSqlParameter[36] = new SqlParameter("@HSCCommerceIndexNo", SqlDbType.VarChar);
                pSqlParameter[36].Direction = ParameterDirection.Input;
                pSqlParameter[36].Value = objSchoolBO.HSCCommerceIndexNo;

                pSqlParameter[37] = new SqlParameter("@HSCArtsIndexNo", SqlDbType.VarChar);
                pSqlParameter[37].Direction = ParameterDirection.Input;
                pSqlParameter[37].Value = objSchoolBO.HSCArtsIndexNo;

                pSqlParameter[38] = new SqlParameter("@RegistrationCode", SqlDbType.VarChar);
                pSqlParameter[38].Direction = ParameterDirection.Input;
                pSqlParameter[38].Value = objSchoolBO.RegistrationCode;

                pSqlParameter[39] = new SqlParameter("@RegisteredNameEng", SqlDbType.VarChar);
                pSqlParameter[39].Direction = ParameterDirection.Input;
                pSqlParameter[39].Value = objSchoolBO.RegisteredNameEng;

                pSqlParameter[40] = new SqlParameter("@RegistreredNameGuj", SqlDbType.NVarChar);
                pSqlParameter[40].Direction = ParameterDirection.Input;
                pSqlParameter[40].Value = objSchoolBO.RegistreredNameGuj;

                pSqlParameter[41] = new SqlParameter("@RegisteredAddressGuj", SqlDbType.NVarChar);
                pSqlParameter[41].Direction = ParameterDirection.Input;
                pSqlParameter[41].Value = objSchoolBO.RegisteredAddressGuj;

                pSqlParameter[42] = new SqlParameter("@SchoolMottoEng", SqlDbType.VarChar);
                pSqlParameter[42].Direction = ParameterDirection.Input;
                pSqlParameter[42].Value = objSchoolBO.SchoolMottoEng;

                pSqlParameter[43] = new SqlParameter("@SchoolMottoGuj", SqlDbType.NVarChar);
                pSqlParameter[43].Direction = ParameterDirection.Input;
                pSqlParameter[43].Value = objSchoolBO.SchoolMottoGuj;

                pSqlParameter[44] = new SqlParameter("@SchoolVisionEng", SqlDbType.VarChar);
                pSqlParameter[44].Direction = ParameterDirection.Input;
                pSqlParameter[44].Value = objSchoolBO.SchoolVisionEng;

                pSqlParameter[45] = new SqlParameter("@SchoolVisionGuj", SqlDbType.NVarChar);
                pSqlParameter[45].Direction = ParameterDirection.Input;
                pSqlParameter[45].Value = objSchoolBO.SchoolVisionGuj;

                pSqlParameter[46] = new SqlParameter("@IsOnRent", SqlDbType.Int);
                pSqlParameter[46].Direction = ParameterDirection.Input;
                pSqlParameter[46].Value = objSchoolBO.IsOnRent;

                pSqlParameter[47] = new SqlParameter("@OwnerNameEng", SqlDbType.VarChar);
                pSqlParameter[47].Direction = ParameterDirection.Input;
                pSqlParameter[47].Value = objSchoolBO.OwnerNameEng;

                pSqlParameter[48] = new SqlParameter("@OwnerNameGuj", SqlDbType.NVarChar);
                pSqlParameter[48].Direction = ParameterDirection.Input;
                pSqlParameter[48].Value = objSchoolBO.OwnerNameGuj;

                pSqlParameter[49] = new SqlParameter("@OwnerAddressEng", SqlDbType.VarChar);
                pSqlParameter[49].Direction = ParameterDirection.Input;
                pSqlParameter[49].Value = objSchoolBO.OwnerAddressEng;

                pSqlParameter[50] = new SqlParameter("@OwnerAddressGuj", SqlDbType.NVarChar);
                pSqlParameter[50].Direction = ParameterDirection.Input;
                pSqlParameter[50].Value = objSchoolBO.OwnerAddressGuj;

                pSqlParameter[51] = new SqlParameter("@WordNo", SqlDbType.VarChar);
                pSqlParameter[51].Direction = ParameterDirection.Input;
                pSqlParameter[51].Value = objSchoolBO.WordNo;

                pSqlParameter[52] = new SqlParameter("@WordNameEng", SqlDbType.VarChar);
                pSqlParameter[52].Direction = ParameterDirection.Input;
                pSqlParameter[52].Value = objSchoolBO.WordNameEng;

                pSqlParameter[53] = new SqlParameter("@WordNameGuj", SqlDbType.VarChar);
                pSqlParameter[53].Direction = ParameterDirection.Input;
                pSqlParameter[53].Value = objSchoolBO.WordNameGuj;

                pSqlParameter[54] = new SqlParameter("@PlotNo", SqlDbType.VarChar);
                pSqlParameter[54].Direction = ParameterDirection.Input;
                pSqlParameter[54].Value = objSchoolBO.PlotNo;

                pSqlParameter[55] = new SqlParameter("@PlotArea", SqlDbType.VarChar);
                pSqlParameter[55].Direction = ParameterDirection.Input;
                pSqlParameter[55].Value = objSchoolBO.PlotArea;

                pSqlParameter[56] = new SqlParameter("@ConstrunctionYear", SqlDbType.VarChar);
                pSqlParameter[56].Direction = ParameterDirection.Input;
                pSqlParameter[56].Value = objSchoolBO.ConstrunctionYear;

                pSqlParameter[57] = new SqlParameter("@NoOfFloors", SqlDbType.VarChar);
                pSqlParameter[57].Direction = ParameterDirection.Input;
                pSqlParameter[57].Value = objSchoolBO.NoOfFloors;

                pSqlParameter[58] = new SqlParameter("@AuditList", SqlDbType.VarChar);
                pSqlParameter[58].Direction = ParameterDirection.Input;
                pSqlParameter[58].Value = objSchoolBO.AuditList;

                pSqlParameter[59] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[59].Direction = ParameterDirection.Input;
                pSqlParameter[59].Value = objSchoolBO.LastModifiedUserID;

                pSqlParameter[60] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[60].Direction = ParameterDirection.Input;
                pSqlParameter[60].Value = objSchoolBO.LastModifiedDate;

                pSqlParameter[61] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[61].Direction = ParameterDirection.Input;
                pSqlParameter[61].Value = objSchoolBO.IsDeleted;

                pSqlParameter[62] = new SqlParameter("@AccountStartDate", SqlDbType.VarChar);
                pSqlParameter[62].Direction = ParameterDirection.Input;
                pSqlParameter[62].Value = objSchoolBO.AccountStartDate;

                pSqlParameter[63] = new SqlParameter("@AreaTypeGuj", SqlDbType.NVarChar);
                pSqlParameter[63].Direction = ParameterDirection.Input;
                pSqlParameter[63].Value = objSchoolBO.AreaTypeGuj;

                pSqlParameter[64] = new SqlParameter("@AreaSubTypeGuj", SqlDbType.NVarChar);
                pSqlParameter[64].Direction = ParameterDirection.Input;
                pSqlParameter[64].Value = objSchoolBO.AreaSubTypeGuj;

				sSql = "usp_tbl_School_M_Insert";
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
				objSchoolBO = null;
			}
		}
		#endregion

		#region Update School Details
		/// <summary>
		/// To Update details of School in tbl_School_M table
		/// Created By : NafisaMulla, 17/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="objSchoolBO"></param>
		/// <returns></returns>
		public ApplicationResult School_Update(SchoolBO objSchoolBO)
		{
			try
			{
                pSqlParameter = new SqlParameter[66];


                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objSchoolBO.SchoolMID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objSchoolBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolNameEng", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objSchoolBO.SchoolNameEng;

                pSqlParameter[3] = new SqlParameter("@SchoolNameGuj", SqlDbType.NVarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objSchoolBO.SchoolNameGuj;

                pSqlParameter[4] = new SqlParameter("@SchoolCode", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objSchoolBO.SchoolCode;

                pSqlParameter[5] = new SqlParameter("@SchoolTiming", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objSchoolBO.SchoolTiming;

                pSqlParameter[6] = new SqlParameter("@AcademicMonth", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objSchoolBO.AcademicMonth;

                pSqlParameter[7] = new SqlParameter("@SchoolLogo", SqlDbType.Image);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objSchoolBO.SchoolLogo;

                pSqlParameter[8] = new SqlParameter("@SchoolAbbreviation", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objSchoolBO.SchoolAbbreviation;

                pSqlParameter[9] = new SqlParameter("@AddressEng", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objSchoolBO.AddressEng;

                pSqlParameter[10] = new SqlParameter("@AddressGuj", SqlDbType.NVarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objSchoolBO.AddressGuj;

                pSqlParameter[11] = new SqlParameter("@AreaType", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objSchoolBO.AreaType;

                pSqlParameter[12] = new SqlParameter("@AreaSubType", SqlDbType.VarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objSchoolBO.AreaSubType;

                pSqlParameter[13] = new SqlParameter("@TownEng", SqlDbType.VarChar);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objSchoolBO.TownEng;

                pSqlParameter[14] = new SqlParameter("@TownGuj", SqlDbType.NVarChar);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objSchoolBO.TownGuj;

                pSqlParameter[15] = new SqlParameter("@AtPoNoEng", SqlDbType.VarChar);
                pSqlParameter[15].Direction = ParameterDirection.Input;
                pSqlParameter[15].Value = objSchoolBO.AtPoNoEng;

                pSqlParameter[16] = new SqlParameter("@AtPoNoGuj", SqlDbType.NVarChar);
                pSqlParameter[16].Direction = ParameterDirection.Input;
                pSqlParameter[16].Value = objSchoolBO.AtPoNoGuj;

                pSqlParameter[17] = new SqlParameter("@TalukaEng", SqlDbType.VarChar);
                pSqlParameter[17].Direction = ParameterDirection.Input;
                pSqlParameter[17].Value = objSchoolBO.TalukaEng;

                pSqlParameter[18] = new SqlParameter("@TalukaGuj", SqlDbType.NVarChar);
                pSqlParameter[18].Direction = ParameterDirection.Input;
                pSqlParameter[18].Value = objSchoolBO.TalukaGuj;

                pSqlParameter[19] = new SqlParameter("@DistrictEng", SqlDbType.VarChar);
                pSqlParameter[19].Direction = ParameterDirection.Input;
                pSqlParameter[19].Value = objSchoolBO.DistrictEng;

                pSqlParameter[20] = new SqlParameter("@DistrictGuj", SqlDbType.NVarChar);
                pSqlParameter[20].Direction = ParameterDirection.Input;
                pSqlParameter[20].Value = objSchoolBO.DistrictGuj;

                pSqlParameter[21] = new SqlParameter("@StateEng", SqlDbType.VarChar);
                pSqlParameter[21].Direction = ParameterDirection.Input;
                pSqlParameter[21].Value = objSchoolBO.StateEng;

                pSqlParameter[22] = new SqlParameter("@StateGuj", SqlDbType.NVarChar);
                pSqlParameter[22].Direction = ParameterDirection.Input;
                pSqlParameter[22].Value = objSchoolBO.StateGuj;

                pSqlParameter[23] = new SqlParameter("@CountryEng", SqlDbType.VarChar);
                pSqlParameter[23].Direction = ParameterDirection.Input;
                pSqlParameter[23].Value = objSchoolBO.CountryEng;

                pSqlParameter[24] = new SqlParameter("@CountryGuj", SqlDbType.NVarChar);
                pSqlParameter[24].Direction = ParameterDirection.Input;
                pSqlParameter[24].Value = objSchoolBO.CountryGuj;

                pSqlParameter[25] = new SqlParameter("@Pincode", SqlDbType.VarChar);
                pSqlParameter[25].Direction = ParameterDirection.Input;
                pSqlParameter[25].Value = objSchoolBO.Pincode;

                pSqlParameter[26] = new SqlParameter("@TelephoneNo", SqlDbType.VarChar);
                pSqlParameter[26].Direction = ParameterDirection.Input;
                pSqlParameter[26].Value = objSchoolBO.TelephoneNo;

                pSqlParameter[27] = new SqlParameter("@MobileNo", SqlDbType.VarChar);
                pSqlParameter[27].Direction = ParameterDirection.Input;
                pSqlParameter[27].Value = objSchoolBO.MobileNo;

                pSqlParameter[28] = new SqlParameter("@EmailID", SqlDbType.VarChar);
                pSqlParameter[28].Direction = ParameterDirection.Input;
                pSqlParameter[28].Value = objSchoolBO.EmailID;

                pSqlParameter[29] = new SqlParameter("@AlternateEmailID", SqlDbType.VarChar);
                pSqlParameter[29].Direction = ParameterDirection.Input;
                pSqlParameter[29].Value = objSchoolBO.AlternateEmailID;

                pSqlParameter[30] = new SqlParameter("@FaxNo", SqlDbType.VarChar);
                pSqlParameter[30].Direction = ParameterDirection.Input;
                pSqlParameter[30].Value = objSchoolBO.FaxNo;

                pSqlParameter[31] = new SqlParameter("@Website", SqlDbType.VarChar);
                pSqlParameter[31].Direction = ParameterDirection.Input;
                pSqlParameter[31].Value = objSchoolBO.Website;

                pSqlParameter[32] = new SqlParameter("@ApprovalNo", SqlDbType.VarChar);
                pSqlParameter[32].Direction = ParameterDirection.Input;
                pSqlParameter[32].Value = objSchoolBO.ApprovalNo;

                pSqlParameter[33] = new SqlParameter("@ApprovalDate", SqlDbType.VarChar);
                pSqlParameter[33].Direction = ParameterDirection.Input;
                pSqlParameter[33].Value = objSchoolBO.ApprovalDate;

                pSqlParameter[34] = new SqlParameter("@ApprovalYear", SqlDbType.VarChar);
                pSqlParameter[34].Direction = ParameterDirection.Input;
                pSqlParameter[34].Value = objSchoolBO.ApprovalYear;

                pSqlParameter[35] = new SqlParameter("@SSCindexNo", SqlDbType.VarChar);
                pSqlParameter[35].Direction = ParameterDirection.Input;
                pSqlParameter[35].Value = objSchoolBO.SSCindexNo;

                pSqlParameter[36] = new SqlParameter("@HSCScienceIndexNo", SqlDbType.VarChar);
                pSqlParameter[36].Direction = ParameterDirection.Input;
                pSqlParameter[36].Value = objSchoolBO.HSCScienceIndexNo;

                pSqlParameter[37] = new SqlParameter("@HSCCommerceIndexNo", SqlDbType.VarChar);
                pSqlParameter[37].Direction = ParameterDirection.Input;
                pSqlParameter[37].Value = objSchoolBO.HSCCommerceIndexNo;

                pSqlParameter[38] = new SqlParameter("@HSCArtsIndexNo", SqlDbType.VarChar);
                pSqlParameter[38].Direction = ParameterDirection.Input;
                pSqlParameter[38].Value = objSchoolBO.HSCArtsIndexNo;

                pSqlParameter[39] = new SqlParameter("@RegistrationCode", SqlDbType.VarChar);
                pSqlParameter[39].Direction = ParameterDirection.Input;
                pSqlParameter[39].Value = objSchoolBO.RegistrationCode;

                pSqlParameter[40] = new SqlParameter("@RegisteredNameEng", SqlDbType.VarChar);
                pSqlParameter[40].Direction = ParameterDirection.Input;
                pSqlParameter[40].Value = objSchoolBO.RegisteredNameEng;

                pSqlParameter[41] = new SqlParameter("@RegistreredNameGuj", SqlDbType.NVarChar);
                pSqlParameter[41].Direction = ParameterDirection.Input;
                pSqlParameter[41].Value = objSchoolBO.RegistreredNameGuj;

                pSqlParameter[42] = new SqlParameter("@RegisteredAddressGuj", SqlDbType.NVarChar);
                pSqlParameter[42].Direction = ParameterDirection.Input;
                pSqlParameter[42].Value = objSchoolBO.RegisteredAddressGuj;

                pSqlParameter[43] = new SqlParameter("@SchoolMottoEng", SqlDbType.VarChar);
                pSqlParameter[43].Direction = ParameterDirection.Input;
                pSqlParameter[43].Value = objSchoolBO.SchoolMottoEng;

                pSqlParameter[44] = new SqlParameter("@SchoolMottoGuj", SqlDbType.NVarChar);
                pSqlParameter[44].Direction = ParameterDirection.Input;
                pSqlParameter[44].Value = objSchoolBO.SchoolMottoGuj;

                pSqlParameter[45] = new SqlParameter("@SchoolVisionEng", SqlDbType.VarChar);
                pSqlParameter[45].Direction = ParameterDirection.Input;
                pSqlParameter[45].Value = objSchoolBO.SchoolVisionEng;

                pSqlParameter[46] = new SqlParameter("@SchoolVisionGuj", SqlDbType.NVarChar);
                pSqlParameter[46].Direction = ParameterDirection.Input;
                pSqlParameter[46].Value = objSchoolBO.SchoolVisionGuj;

                pSqlParameter[47] = new SqlParameter("@IsOnRent", SqlDbType.Int);
                pSqlParameter[47].Direction = ParameterDirection.Input;
                pSqlParameter[47].Value = objSchoolBO.IsOnRent;

                pSqlParameter[48] = new SqlParameter("@OwnerNameEng", SqlDbType.VarChar);
                pSqlParameter[48].Direction = ParameterDirection.Input;
                pSqlParameter[48].Value = objSchoolBO.OwnerNameEng;

                pSqlParameter[49] = new SqlParameter("@OwnerNameGuj", SqlDbType.NVarChar);
                pSqlParameter[49].Direction = ParameterDirection.Input;
                pSqlParameter[49].Value = objSchoolBO.OwnerNameGuj;

                pSqlParameter[50] = new SqlParameter("@OwnerAddressEng", SqlDbType.VarChar);
                pSqlParameter[50].Direction = ParameterDirection.Input;
                pSqlParameter[50].Value = objSchoolBO.OwnerAddressEng;

                pSqlParameter[51] = new SqlParameter("@OwnerAddressGuj", SqlDbType.NVarChar);
                pSqlParameter[51].Direction = ParameterDirection.Input;
                pSqlParameter[51].Value = objSchoolBO.OwnerAddressGuj;

                pSqlParameter[52] = new SqlParameter("@WordNo", SqlDbType.VarChar);
                pSqlParameter[52].Direction = ParameterDirection.Input;
                pSqlParameter[52].Value = objSchoolBO.WordNo;

                pSqlParameter[53] = new SqlParameter("@WordNameEng", SqlDbType.VarChar);
                pSqlParameter[53].Direction = ParameterDirection.Input;
                pSqlParameter[53].Value = objSchoolBO.WordNameEng;

                pSqlParameter[54] = new SqlParameter("@WordNameGuj", SqlDbType.VarChar);
                pSqlParameter[54].Direction = ParameterDirection.Input;
                pSqlParameter[54].Value = objSchoolBO.WordNameGuj;

                pSqlParameter[55] = new SqlParameter("@PlotNo", SqlDbType.VarChar);
                pSqlParameter[55].Direction = ParameterDirection.Input;
                pSqlParameter[55].Value = objSchoolBO.PlotNo;

                pSqlParameter[56] = new SqlParameter("@PlotArea", SqlDbType.VarChar);
                pSqlParameter[56].Direction = ParameterDirection.Input;
                pSqlParameter[56].Value = objSchoolBO.PlotArea;

                pSqlParameter[57] = new SqlParameter("@ConstrunctionYear", SqlDbType.VarChar);
                pSqlParameter[57].Direction = ParameterDirection.Input;
                pSqlParameter[57].Value = objSchoolBO.ConstrunctionYear;

                pSqlParameter[58] = new SqlParameter("@NoOfFloors", SqlDbType.VarChar);
                pSqlParameter[58].Direction = ParameterDirection.Input;
                pSqlParameter[58].Value = objSchoolBO.NoOfFloors;

                pSqlParameter[59] = new SqlParameter("@AuditList", SqlDbType.VarChar);
                pSqlParameter[59].Direction = ParameterDirection.Input;
                pSqlParameter[59].Value = objSchoolBO.AuditList;

                pSqlParameter[60] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[60].Direction = ParameterDirection.Input;
                pSqlParameter[60].Value = objSchoolBO.LastModifiedUserID;

                pSqlParameter[61] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[61].Direction = ParameterDirection.Input;
                pSqlParameter[61].Value = objSchoolBO.LastModifiedDate;

                pSqlParameter[62] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[62].Direction = ParameterDirection.Input;
                pSqlParameter[62].Value = objSchoolBO.IsDeleted;

                pSqlParameter[63] = new SqlParameter("@AccountStartDate", SqlDbType.VarChar);
                pSqlParameter[63].Direction = ParameterDirection.Input;
                pSqlParameter[63].Value = objSchoolBO.AccountStartDate;

                pSqlParameter[64] = new SqlParameter("@AreaTypeGuj", SqlDbType.NVarChar);
                pSqlParameter[64].Direction = ParameterDirection.Input;
                pSqlParameter[64].Value = objSchoolBO.AreaTypeGuj;

                pSqlParameter[65] = new SqlParameter("@AreaSubTypeGuj", SqlDbType.NVarChar);
                pSqlParameter[65].Direction = ParameterDirection.Input;
                pSqlParameter[65].Value = objSchoolBO.AreaSubTypeGuj;


				sSql = "usp_tbl_School_M_Update";
				int iResult = DatabaseTransaction.ExecuteNonQuery(CommandType.StoredProcedure, sSql, pSqlParameter);

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
				objSchoolBO = null;
			}
		}
		#endregion

		#region Select School Details by SchoolName
		/// <summary>
		/// Select all details of School for selected SchoolName from tbl_School_M table
		/// Created By : NafisaMulla, 23/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="SchoolName"></param>
		/// <returns></returns>
		public ApplicationResult School_Select_bySchoolName(string strSchoolName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@SchoolName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strSchoolName;

				strStoredProcName = "usp_tbl_School_M_Select_BySchool";

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

		#region ValidateName for School
		/// <summary>
		/// Function which validates whether the SchoolName already exits in tbl_School_M table.
		/// Created By : NafisaMulla, 23/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="strSchoolName"></param>
		/// <returns></returns>
        public ApplicationResult School_ValidateName(string strSchoolName, string strSchoolCode, string strSSCindexNo, string strHSCScienceIndexNo, string strHSCCommerceIndexNo, string strHSCArtsIndexNo, int intSchoolMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[7];

				pSqlParameter[0] = new SqlParameter("@SchoolNameEng", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strSchoolName;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                pSqlParameter[2] = new SqlParameter("@SchoolCode", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strSchoolCode;

                pSqlParameter[3] = new SqlParameter("@SSCindexNo", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strSSCindexNo;

                pSqlParameter[4] = new SqlParameter("@HSCScienceIndexNo", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = strHSCScienceIndexNo;

                pSqlParameter[5] = new SqlParameter("@HSCCommerceIndexNo", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = strHSCCommerceIndexNo;

                pSqlParameter[6] = new SqlParameter("@HSCArtsIndexNo", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = strHSCArtsIndexNo;

                strStoredProcName = "usp_tbl_School_M_ValidationName";

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

        #region Validate For Delete
        public ApplicationResult Validate_School_Delete(int intTrustID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustID;

                strStoredProcName = "usp_tbl_School_M_Validate_For_Delete";

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

        #region Select All SchoolApprovalT Details
        /// <summary>
		/// To Select All data from the SchoolApprovalT table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public ApplicationResult SchoolApprovalT_SelectAll()
		{
			try
			{
				sSql = "usp_SchoolApprovalT_SelectAll";
				DataTable dtSchoolApprovalT = new DataTable();
				dtSchoolApprovalT = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

				ApplicationResult objResults = new ApplicationResult(dtSchoolApprovalT);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region Select SchoolApprovalT Details by SchoolApprovalTID
		/// <summary>
		/// Select all details of SchoolApprovalT for selected SchoolApprovalTID from SchoolApprovalT table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="intSchoolApprovalTID"></param>
		/// <returns></returns>
		public ApplicationResult SchoolApprovalT_Select(int intSchoolApprovalTID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@SchoolApprovalTID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intSchoolApprovalTID;

				strStoredProcName = "usp_SchoolApprovalT_Select";

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

		#region Delete SchoolApprovalT Details by SchoolApprovalTID
		/// <summary>
		/// To Delete details of SchoolApprovalT for selected SchoolApprovalTID from SchoolApprovalT table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="intSchoolApprovalTID"></param>
		/// <returns></returns>
		public ApplicationResult SchoolApprovalT_Delete(int intSchoolApprovalTID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@SchoolApprovalTID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intSchoolApprovalTID;

				strStoredProcName = "usp_SchoolApprovalT_Delete";

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

		#region Insert SchoolApprovalT Details
		/// <summary>
		/// To Insert details of SchoolApprovalT in SchoolApprovalT table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="objSchoolApprovalTBO"></param>
		/// <returns></returns>
		public ApplicationResult SchoolApprovalT_Insert(SchoolApprovalTBO objSchoolApprovalTBO)
		{
			try
			{
				pSqlParameter = new SqlParameter[9];


				pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = objSchoolApprovalTBO.SchoolMID;

				pSqlParameter[1] = new SqlParameter("@SectionApproved", SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = objSchoolApprovalTBO.SectionApproved;

				pSqlParameter[2] = new SqlParameter("@ClassApproved", SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = objSchoolApprovalTBO.ClassApproved;

				pSqlParameter[3] = new SqlParameter("@DivisionApproved", SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = objSchoolApprovalTBO.DivisionApproved;

				pSqlParameter[4] = new SqlParameter("@ApprovedDate", SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = objSchoolApprovalTBO.ApprovedDate;

				pSqlParameter[5] = new SqlParameter("@ApprovalNo", SqlDbType.VarChar);
				pSqlParameter[5].Direction = ParameterDirection.Input;
				pSqlParameter[5].Value = objSchoolApprovalTBO.ApprovalNo;

				pSqlParameter[6] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
				pSqlParameter[6].Direction = ParameterDirection.Input;
				pSqlParameter[6].Value = objSchoolApprovalTBO.LastModifiedUserID;

				pSqlParameter[7] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
				pSqlParameter[7].Direction = ParameterDirection.Input;
				pSqlParameter[7].Value = objSchoolApprovalTBO.LastModifiedDate;

				pSqlParameter[8] = new SqlParameter("@IsDeleted", SqlDbType.Int);
				pSqlParameter[8].Direction = ParameterDirection.Input;
				pSqlParameter[8].Value = objSchoolApprovalTBO.IsDeleted;


				sSql = "usp_SchoolApprovalT_Insert";
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
				objSchoolApprovalTBO = null;
			}
		}
		#endregion

		#region Update SchoolApprovalT Details
		/// <summary>
		/// To Update details of SchoolApprovalT in SchoolApprovalT table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="objSchoolApprovalTBO"></param>
		/// <returns></returns>
		public ApplicationResult SchoolApprovalT_Update(SchoolApprovalTBO objSchoolApprovalTBO)
		{
			try
			{
				pSqlParameter = new SqlParameter[10];


				pSqlParameter[0] = new SqlParameter("@SchoolApprovalTID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = objSchoolApprovalTBO.SchoolApprovalTID;

				pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = objSchoolApprovalTBO.SchoolMID;

				pSqlParameter[2] = new SqlParameter("@SectionApproved", SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = objSchoolApprovalTBO.SectionApproved;

				pSqlParameter[3] = new SqlParameter("@ClassApproved", SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = objSchoolApprovalTBO.ClassApproved;

				pSqlParameter[4] = new SqlParameter("@DivisionApproved", SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = objSchoolApprovalTBO.DivisionApproved;

				pSqlParameter[5] = new SqlParameter("@ApprovedDate", SqlDbType.VarChar);
				pSqlParameter[5].Direction = ParameterDirection.Input;
				pSqlParameter[5].Value = objSchoolApprovalTBO.ApprovedDate;

				pSqlParameter[6] = new SqlParameter("@ApprovalNo", SqlDbType.VarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
				pSqlParameter[6].Value = objSchoolApprovalTBO.ApprovalNo;

				pSqlParameter[7] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
				pSqlParameter[7].Direction = ParameterDirection.Input;
				pSqlParameter[7].Value = objSchoolApprovalTBO.LastModifiedUserID;

				pSqlParameter[8] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
				pSqlParameter[8].Direction = ParameterDirection.Input;
				pSqlParameter[8].Value = objSchoolApprovalTBO.LastModifiedDate;

				pSqlParameter[9] = new SqlParameter("@IsDeleted", SqlDbType.Int);
				pSqlParameter[9].Direction = ParameterDirection.Input;
				pSqlParameter[9].Value = objSchoolApprovalTBO.IsDeleted;


				sSql = "usp_SchoolApprovalT_Update";
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
				objSchoolApprovalTBO = null;
			}
		}
		#endregion

        #region ValidateName for School Name
        /// <summary>
        /// Function which validates whether the SectionName already exits in tbl_Section_M table.
        /// Created By : NafisaMulla, 4/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="strSectionName"></param>
        /// <returns></returns>
        public ApplicationResult School_ValidateName_SchoolName(string strSchoolName, string strSchoolAbbrev, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@SchoolNameEng", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strSchoolName;

                pSqlParameter[1] = new SqlParameter("@SchoolAbbreviation", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strSchoolAbbrev;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_School_M_ValidationName_School";

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

		#region Select SchoolApprovalT Details by SchoolApprovalTName
		/// <summary>
		/// Select all details of SchoolApprovalT for selected SchoolApprovalTName from SchoolApprovalT table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="SchoolApprovalTName"></param>
		/// <returns></returns>
		public ApplicationResult SchoolApprovalT_Select_bySchoolApprovalTName(string strSchoolApprovalTName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@SchoolApprovalTName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strSchoolApprovalTName;

				strStoredProcName = "usp_SchoolApprovalT_Select_BySchoolApprovalT";

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

		#region ValidateName for SchoolApprovalT
		/// <summary>
		/// Function which validates whether the SchoolApprovalTName already exits in SchoolApprovalT table.
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="strSchoolApprovalTName"></param>
		/// <returns></returns>
		public ApplicationResult SchoolApprovalT_ValidateName(string strSchoolApprovalTName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@SchoolApprovalTName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strSchoolApprovalTName;

				strStoredProcName = "usp_SchoolApprovalT_Validate_SchoolApprovalTName";

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

		#region Select All SchoolEducationDetailT Details
		/// <summary>
		/// To Select All data from the SchoolEducationDetailT table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public ApplicationResult SchoolEducationDetailT_SelectAll()
		{
			try
			{
				sSql = "usp_SchoolEducationDetailT_SelectAll";
				DataTable dtSchoolEducationDetailT = new DataTable();
				dtSchoolEducationDetailT = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

				ApplicationResult objResults = new ApplicationResult(dtSchoolEducationDetailT);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region Select SchoolEducationDetailT Details by StudentEducationDetailTID
		/// <summary>
		/// Select all details of SchoolEducationDetailT for selected StudentEducationDetailTID from SchoolEducationDetailT table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="intStudentEducationDetailTID"></param>
		/// <returns></returns>
		public ApplicationResult SchoolEducationDetailT_Select(int intStudentEducationDetailTID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@StudentEducationDetailTID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intStudentEducationDetailTID;

				strStoredProcName = "usp_SchoolEducationDetailT_Select";

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

		#region Delete SchoolEducationDetailT Details by StudentEducationDetailTID
		/// <summary>
		/// To Delete details of SchoolEducationDetailT for selected StudentEducationDetailTID from SchoolEducationDetailT table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="intStudentEducationDetailTID"></param>
		/// <returns></returns>
		public ApplicationResult SchoolEducationDetailT_Delete(int intStudentEducationDetailTID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@StudentEducationDetailTID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intStudentEducationDetailTID;

				strStoredProcName = "usp_SchoolEducationDetailT_Delete";

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

		#region Insert SchoolEducationDetailT Details
		/// <summary>
		/// To Insert details of SchoolEducationDetailT in SchoolEducationDetailT table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="objSchoolEducationDetailTBO"></param>
		/// <returns></returns>
		public ApplicationResult SchoolEducationDetailT_Insert(SchoolPreEducationDetailTBO objSchoolEducationDetailTBO)
		{
			try
			{
				pSqlParameter = new SqlParameter[9];


				pSqlParameter[0] = new SqlParameter("@StudentMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = objSchoolEducationDetailTBO.StudentMID;

				pSqlParameter[1] = new SqlParameter("@SchoolName", SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = objSchoolEducationDetailTBO.SchoolName;

				pSqlParameter[2] = new SqlParameter("@Address", SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = objSchoolEducationDetailTBO.Address;

				pSqlParameter[3] = new SqlParameter("@MediumName", SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = objSchoolEducationDetailTBO.MediumName;

				pSqlParameter[4] = new SqlParameter("@PassedExam", SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = objSchoolEducationDetailTBO.PassedExam;

				pSqlParameter[5] = new SqlParameter("@BoardName", SqlDbType.VarChar);
				pSqlParameter[5].Direction = ParameterDirection.Input;
				pSqlParameter[5].Value = objSchoolEducationDetailTBO.BoardName;

				pSqlParameter[6] = new SqlParameter("@PassingYear", SqlDbType.VarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
				pSqlParameter[6].Value = objSchoolEducationDetailTBO.PassingYear;

				pSqlParameter[7] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
				pSqlParameter[7].Direction = ParameterDirection.Input;
				pSqlParameter[7].Value = objSchoolEducationDetailTBO.LastModifiedUserID;

				pSqlParameter[8] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
				pSqlParameter[8].Direction = ParameterDirection.Input;
				pSqlParameter[8].Value = objSchoolEducationDetailTBO.LastModifiedDate;


				sSql = "usp_SchoolEducationDetailT_Insert";
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
				objSchoolEducationDetailTBO = null;
			}
		}
		#endregion

		#region Update SchoolEducationDetailT Details
		/// <summary>
		/// To Update details of SchoolEducationDetailT in SchoolEducationDetailT table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="objSchoolEducationDetailTBO"></param>
		/// <returns></returns>
		public ApplicationResult SchoolEducationDetailT_Update(SchoolPreEducationDetailTBO objSchoolEducationDetailTBO)
		{
			try
			{
				pSqlParameter = new SqlParameter[10];


				pSqlParameter[0] = new SqlParameter("@StudentEducationDetailTID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = objSchoolEducationDetailTBO.StudentEducationDetailTID;

				pSqlParameter[1] = new SqlParameter("@StudentMID", SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = objSchoolEducationDetailTBO.StudentMID;

				pSqlParameter[2] = new SqlParameter("@SchoolName", SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = objSchoolEducationDetailTBO.SchoolName;

				pSqlParameter[3] = new SqlParameter("@Address", SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = objSchoolEducationDetailTBO.Address;

				pSqlParameter[4] = new SqlParameter("@MediumName", SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = objSchoolEducationDetailTBO.MediumName;

				pSqlParameter[5] = new SqlParameter("@PassedExam", SqlDbType.VarChar);
				pSqlParameter[5].Direction = ParameterDirection.Input;
				pSqlParameter[5].Value = objSchoolEducationDetailTBO.PassedExam;

				pSqlParameter[6] = new SqlParameter("@BoardName", SqlDbType.VarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
				pSqlParameter[6].Value = objSchoolEducationDetailTBO.BoardName;

				pSqlParameter[7] = new SqlParameter("@PassingYear", SqlDbType.VarChar);
				pSqlParameter[7].Direction = ParameterDirection.Input;
				pSqlParameter[7].Value = objSchoolEducationDetailTBO.PassingYear;

				pSqlParameter[8] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
				pSqlParameter[8].Direction = ParameterDirection.Input;
				pSqlParameter[8].Value = objSchoolEducationDetailTBO.LastModifiedUserID;

				pSqlParameter[9] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
				pSqlParameter[9].Direction = ParameterDirection.Input;
				pSqlParameter[9].Value = objSchoolEducationDetailTBO.LastModifiedDate;


				sSql = "usp_SchoolEducationDetailT_Update";
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
				objSchoolEducationDetailTBO = null;
			}
		}
		#endregion

		#region Select SchoolEducationDetailT Details by SchoolEducationDetailTName
		/// <summary>
		/// Select all details of SchoolEducationDetailT for selected SchoolEducationDetailTName from SchoolEducationDetailT table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="SchoolEducationDetailTName"></param>
		/// <returns></returns>
		public ApplicationResult SchoolEducationDetailT_Select_bySchoolEducationDetailTName(string strSchoolEducationDetailTName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@SchoolEducationDetailTName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strSchoolEducationDetailTName;

				strStoredProcName = "usp_SchoolEducationDetailT_Select_BySchoolEducationDetailT";

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

		#region ValidateName for SchoolEducationDetailT
		/// <summary>
		/// Function which validates whether the SchoolEducationDetailTName already exits in SchoolEducationDetailT table.
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="strSchoolEducationDetailTName"></param>
		/// <returns></returns>
		public ApplicationResult SchoolEducationDetailT_ValidateName(string strSchoolEducationDetailTName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@SchoolEducationDetailTName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strSchoolEducationDetailTName;

				strStoredProcName = "usp_SchoolEducationDetailT_Validate_SchoolEducationDetailTName";

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

		#region Select SchoolMID Details by RegistrationCode
		/// <summary>
		/// Select all details of School for selected SchoolName from tbl_School_M table
		/// Created By : NafisaMulla, 23/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="SchoolName"></param>
		/// <returns></returns>
		public ApplicationResult School_Select_SchoolMID(string strRegistrationCode)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@RegistrationCode", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strRegistrationCode;

				strStoredProcName = "usp_tbl_School_M_SchoolMID";

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

        #region Select School Information For Report
        /// Select all details of EmployeeM for selected EmployeeMID from tbl_Employee_M table
        /// Created By : Darshan, 09/12/2014
        /// Modified By :
        public ApplicationResult SchoolM_Select_InformationForReport(int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_School_M_InformationForReport";

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

        #region Select All School Details
        /// <summary>
        /// To Select All data from the tbl_School_M table
        /// Created By : NafisaMulla, 23/6/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult School_M_ForListReport(int intTrustID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustID;

                sSql = "usp_tbl_School_M_ListReport";

                DataTable dtSchool = new DataTable();
                dtSchool = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtSchool);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select All School Details
        /// <summary>
        /// To Select All data from the tbl_School_M table
        /// Created By : NafisaMulla, 23/6/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult School_AccountingInfoReport(int intTrustID, int intSchoolID, int intType)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolID;

                pSqlParameter[2] = new SqlParameter("@intType", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intType;

                sSql = "usp_rpt_SchoolAccountingInformation";

                DataTable dtSchool = new DataTable();
                dtSchool = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtSchool);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select All DEO REport Patrak-3 Details
        /// <summary>
        /// To Select All data from the tbl_School_M table
        /// Created By : Vishal, 13/10/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult SchoolWiseDEOReport_Patrak3( int intSchoolID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];
               
                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolID;

                sSql = "usp_rpt_DEO";

                DataTable dtSchool = new DataTable();
                dtSchool = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtSchool);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select All DEO REport Patrak-2 Details
        /// <summary>
        /// To Select All data from the tbl_School_M table
        /// Created By : Vishal, 13/10/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult SchoolWiseDEOReport_Patrak2(int intSchoolID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolID;

                sSql = "usp_rpt_DEO_Patrak_2";

                DataTable dtSchool = new DataTable();
                dtSchool = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtSchool);
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


