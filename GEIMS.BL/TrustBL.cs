using System;
using System.Data;
using System.Data.SqlClient;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
	/// <summary>
	/// Class Created By : NafisaMulla, 1/4/2015
	/// Summary description for Organisation.
	/// </summary>
	public class TrustBL
	{
		#region user defined variables
		public string sSql;
		public string strStoredProcName;
		public SqlParameter[] pSqlParameter = null;
		#endregion

		#region Select All Trust Details
		/// <summary>
		/// To Select All data from the tbl_Trust_M table
		/// Created By : NafisaMulla, 27/6/2014
		/// Modified By :
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public ApplicationResult Trust_SelectAll()
		{
			try
			{
				sSql = "usp_tbl_Trust_M_SelectAll";
				DataTable dtTrust = new DataTable();
				dtTrust = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

				ApplicationResult objResults = new ApplicationResult(dtTrust);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

        #region Select All Trust Details
        /// <summary>
        /// To Select All data from the tbl_Trust_M table
        /// Created By : NafisaMulla, 27/6/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult Trust_SelectAll_ForDropDown()
        {
            try
            {
                sSql = "usp_tbl_Trust_M_SelectAll_ForDropDown";
                DataTable dtTrust = new DataTable();
                dtTrust = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtTrust);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

		#region Select Trust Details by TrustMID
		/// <summary>
		/// Select all details of Trust for selected TrustMID from tbl_Trust_M table
		/// Created By : NafisaMulla, 27/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="intTrustMID"></param>
		/// <returns></returns>
		public ApplicationResult Trust_Select(int intTrustMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intTrustMID;

				strStoredProcName = "usp_tbl_Trust_M_Select";

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

		#region Delete Trust Details by TrustMID
		/// <summary>
		/// To Delete details of Trust for selected TrustMID from tbl_Trust_M table
		/// Created By : NafisaMulla, 27/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="intTrustMID"></param>
		/// <returns></returns>
		public ApplicationResult Trust_Delete(int intTrustMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intTrustMID;

				strStoredProcName = "usp_tbl_Trust_M_Delete";

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

        #region Validate For Delete
        public ApplicationResult Validate_Trust_Delete(int intTrustID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustID;

                strStoredProcName = "usp_tbl_Trust_M_Validate_For_Delete";

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

		#region Insert Trust Details
		/// <summary>
		/// To Insert details of Trust in tbl_Trust_M table
		/// Created By : NafisaMulla, 27/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="objTrustBO"></param>
		/// <returns></returns>
		public ApplicationResult Trust_Insert(TrustBO objTrustBO)
		{
			try
			{
                pSqlParameter = new SqlParameter[29];


                pSqlParameter[0] = new SqlParameter("@TrustNameEng", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objTrustBO.TrustNameEng;

                pSqlParameter[1] = new SqlParameter("@TrustNameGuj", SqlDbType.NVarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objTrustBO.TrustNameGuj;

                pSqlParameter[2] = new SqlParameter("@TrustAbbreviation", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objTrustBO.TrustAbbreviation;

                pSqlParameter[3] = new SqlParameter("@TrustLogo", SqlDbType.Image);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objTrustBO.TrustLogo;

                pSqlParameter[4] = new SqlParameter("@RegistrationCode", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objTrustBO.RegistrationCode;

                pSqlParameter[5] = new SqlParameter("@AddressEng", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objTrustBO.AddressEng;

                pSqlParameter[6] = new SqlParameter("@AddressGuj", SqlDbType.NVarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objTrustBO.AddressGuj;

                pSqlParameter[7] = new SqlParameter("@TownEng", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objTrustBO.TownEng;

                pSqlParameter[8] = new SqlParameter("@TownGuj", SqlDbType.NVarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objTrustBO.TownGuj;

                pSqlParameter[9] = new SqlParameter("@DistrictEng", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objTrustBO.DistrictEng;

                pSqlParameter[10] = new SqlParameter("@DistrictGuj", SqlDbType.NVarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objTrustBO.DistrictGuj;

                pSqlParameter[11] = new SqlParameter("@StateEng", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objTrustBO.StateEng;

                pSqlParameter[12] = new SqlParameter("@StateGuj", SqlDbType.NVarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objTrustBO.StateGuj;

                pSqlParameter[13] = new SqlParameter("@CountryEng", SqlDbType.VarChar);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objTrustBO.CountryEng;

                pSqlParameter[14] = new SqlParameter("@CountryGuj", SqlDbType.NVarChar);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objTrustBO.CountryGuj;

                pSqlParameter[15] = new SqlParameter("@Pincode", SqlDbType.VarChar);
                pSqlParameter[15].Direction = ParameterDirection.Input;
                pSqlParameter[15].Value = objTrustBO.Pincode;

                pSqlParameter[16] = new SqlParameter("@TelephoneNo", SqlDbType.VarChar);
                pSqlParameter[16].Direction = ParameterDirection.Input;
                pSqlParameter[16].Value = objTrustBO.TelephoneNo;

                pSqlParameter[17] = new SqlParameter("@MobileNo", SqlDbType.VarChar);
                pSqlParameter[17].Direction = ParameterDirection.Input;
                pSqlParameter[17].Value = objTrustBO.MobileNo;

                pSqlParameter[18] = new SqlParameter("@EmailId", SqlDbType.VarChar);
                pSqlParameter[18].Direction = ParameterDirection.Input;
                pSqlParameter[18].Value = objTrustBO.EmailId;

                pSqlParameter[19] = new SqlParameter("@AlternateEmailId", SqlDbType.VarChar);
                pSqlParameter[19].Direction = ParameterDirection.Input;
                pSqlParameter[19].Value = objTrustBO.AlternateEmailId;

                pSqlParameter[20] = new SqlParameter("@FaxNo", SqlDbType.VarChar);
                pSqlParameter[20].Direction = ParameterDirection.Input;
                pSqlParameter[20].Value = objTrustBO.FaxNo;

                pSqlParameter[21] = new SqlParameter("@Website", SqlDbType.VarChar);
                pSqlParameter[21].Direction = ParameterDirection.Input;
                pSqlParameter[21].Value = objTrustBO.Website;

                pSqlParameter[22] = new SqlParameter("@ApprovalYear", SqlDbType.VarChar);
                pSqlParameter[22].Direction = ParameterDirection.Input;
                pSqlParameter[22].Value = objTrustBO.ApprovalYear;

                pSqlParameter[23] = new SqlParameter("@ApprovalDate", SqlDbType.VarChar);
                pSqlParameter[23].Direction = ParameterDirection.Input;
                pSqlParameter[23].Value = objTrustBO.ApprovalDate;

                pSqlParameter[24] = new SqlParameter("@ApprovalNo", SqlDbType.VarChar);
                pSqlParameter[24].Direction = ParameterDirection.Input;
                pSqlParameter[24].Value = objTrustBO.ApprovalNo;

                pSqlParameter[25] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[25].Direction = ParameterDirection.Input;
                pSqlParameter[25].Value = objTrustBO.IsDeleted;

                pSqlParameter[26] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[26].Direction = ParameterDirection.Input;
                pSqlParameter[26].Value = objTrustBO.LastModifiedUserID;

                pSqlParameter[27] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[27].Direction = ParameterDirection.Input;
                pSqlParameter[27].Value = objTrustBO.LastModifiedDate;

                pSqlParameter[28] = new SqlParameter("@AccountStartDate", SqlDbType.VarChar);
                pSqlParameter[28].Direction = ParameterDirection.Input;
                pSqlParameter[28].Value = objTrustBO.AccountStartDate;


                sSql = "usp_tbl_Trust_M_Insert";
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
				objTrustBO = null;
			}
		}
		#endregion

		#region Update Trust Details
		/// <summary>
		/// To Update details of Trust in tbl_Trust_M table
		/// Created By : NafisaMulla, 27/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="objTrustBO"></param>
		/// <returns></returns>
		public ApplicationResult Trust_Update(TrustBO objTrustBO)
		{
			try
			{
                pSqlParameter = new SqlParameter[30];


                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objTrustBO.TrustMID;

                pSqlParameter[1] = new SqlParameter("@TrustNameEng", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objTrustBO.TrustNameEng;

                pSqlParameter[2] = new SqlParameter("@TrustNameGuj", SqlDbType.NVarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objTrustBO.TrustNameGuj;

                pSqlParameter[3] = new SqlParameter("@TrustAbbreviation", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objTrustBO.TrustAbbreviation;

                pSqlParameter[4] = new SqlParameter("@TrustLogo", SqlDbType.Image);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objTrustBO.TrustLogo;

                pSqlParameter[5] = new SqlParameter("@RegistrationCode", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objTrustBO.RegistrationCode;

                pSqlParameter[6] = new SqlParameter("@AddressEng", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objTrustBO.AddressEng;

                pSqlParameter[7] = new SqlParameter("@AddressGuj", SqlDbType.NVarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objTrustBO.AddressGuj;

                pSqlParameter[8] = new SqlParameter("@TownEng", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objTrustBO.TownEng;

                pSqlParameter[9] = new SqlParameter("@TownGuj", SqlDbType.NVarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objTrustBO.TownGuj;

                pSqlParameter[10] = new SqlParameter("@DistrictEng", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objTrustBO.DistrictEng;

                pSqlParameter[11] = new SqlParameter("@DistrictGuj", SqlDbType.NVarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objTrustBO.DistrictGuj;

                pSqlParameter[12] = new SqlParameter("@StateEng", SqlDbType.VarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objTrustBO.StateEng;

                pSqlParameter[13] = new SqlParameter("@StateGuj", SqlDbType.NVarChar);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objTrustBO.StateGuj;

                pSqlParameter[14] = new SqlParameter("@CountryEng", SqlDbType.VarChar);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objTrustBO.CountryEng;

                pSqlParameter[15] = new SqlParameter("@CountryGuj", SqlDbType.NVarChar);
                pSqlParameter[15].Direction = ParameterDirection.Input;
                pSqlParameter[15].Value = objTrustBO.CountryGuj;

                pSqlParameter[16] = new SqlParameter("@Pincode", SqlDbType.VarChar);
                pSqlParameter[16].Direction = ParameterDirection.Input;
                pSqlParameter[16].Value = objTrustBO.Pincode;

                pSqlParameter[17] = new SqlParameter("@TelephoneNo", SqlDbType.VarChar);
                pSqlParameter[17].Direction = ParameterDirection.Input;
                pSqlParameter[17].Value = objTrustBO.TelephoneNo;

                pSqlParameter[18] = new SqlParameter("@MobileNo", SqlDbType.VarChar);
                pSqlParameter[18].Direction = ParameterDirection.Input;
                pSqlParameter[18].Value = objTrustBO.MobileNo;

                pSqlParameter[19] = new SqlParameter("@EmailId", SqlDbType.VarChar);
                pSqlParameter[19].Direction = ParameterDirection.Input;
                pSqlParameter[19].Value = objTrustBO.EmailId;

                pSqlParameter[20] = new SqlParameter("@AlternateEmailId", SqlDbType.VarChar);
                pSqlParameter[20].Direction = ParameterDirection.Input;
                pSqlParameter[20].Value = objTrustBO.AlternateEmailId;

                pSqlParameter[21] = new SqlParameter("@FaxNo", SqlDbType.VarChar);
                pSqlParameter[21].Direction = ParameterDirection.Input;
                pSqlParameter[21].Value = objTrustBO.FaxNo;

                pSqlParameter[22] = new SqlParameter("@Website", SqlDbType.VarChar);
                pSqlParameter[22].Direction = ParameterDirection.Input;
                pSqlParameter[22].Value = objTrustBO.Website;

                pSqlParameter[23] = new SqlParameter("@ApprovalYear", SqlDbType.VarChar);
                pSqlParameter[23].Direction = ParameterDirection.Input;
                pSqlParameter[23].Value = objTrustBO.ApprovalYear;

                pSqlParameter[24] = new SqlParameter("@ApprovalDate", SqlDbType.VarChar);
                pSqlParameter[24].Direction = ParameterDirection.Input;
                pSqlParameter[24].Value = objTrustBO.ApprovalDate;

                pSqlParameter[25] = new SqlParameter("@ApprovalNo", SqlDbType.VarChar);
                pSqlParameter[25].Direction = ParameterDirection.Input;
                pSqlParameter[25].Value = objTrustBO.ApprovalNo;

                pSqlParameter[26] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[26].Direction = ParameterDirection.Input;
                pSqlParameter[26].Value = objTrustBO.IsDeleted;

                pSqlParameter[27] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[27].Direction = ParameterDirection.Input;
                pSqlParameter[27].Value = objTrustBO.LastModifiedUserID;

                pSqlParameter[28] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[28].Direction = ParameterDirection.Input;
                pSqlParameter[28].Value = objTrustBO.LastModifiedDate;

                pSqlParameter[29] = new SqlParameter("@AccountStartDate", SqlDbType.VarChar);
                pSqlParameter[29].Direction = ParameterDirection.Input;
                pSqlParameter[29].Value = objTrustBO.AccountStartDate;


                sSql = "usp_tbl_Trust_M_Update";
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
				objTrustBO = null;
			}
		}
		#endregion

        #region Validate Abbreviation of Trust, School, Section
        /// <summary>
        /// Select all details of Trust for selected TrustMID from tbl_Trust_M table
        /// Created By : NafisaMulla, 27/6/2014
        /// Modified By :
        /// </summary>
        /// <param name="intTrustMID"></param>
        /// <returns></returns>
        public ApplicationResult Abbreviation_Validation(int intTrustMID, int intSchoolMID, int intSectionTID)
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

                pSqlParameter[2] = new SqlParameter("@SectionTID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSectionTID;

                strStoredProcName = "usp_tbl_Abbreviation_Validation";

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

		#region Select Trust Details by TrustName
		/// <summary>
		/// Select all details of Trust for selected TrustName from tbl_Trust_M table
		/// Created By : NafisaMulla, 27/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="TrustName"></param>
		/// <returns></returns>
		public ApplicationResult Trust_Select_byTrustName(string strTrustName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@TrustName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strTrustName;

				strStoredProcName = "usp_tbl_Trust_M_Select_ByTrust";

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

		#region ValidateName for Trust
		/// <summary>
		/// Function which validates whether the TrustName already exits in tbl_Trust_M table.
		/// Created By : NafisaMulla, 27/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="strTrustName"></param>
		/// <returns></returns>
        public ApplicationResult Trust_ValidateName(string strTrustName, string strTrustAbbrev, int intTrustMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[3];

				pSqlParameter[0] = new SqlParameter("@TrustNameEng", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strTrustName;

                pSqlParameter[1] = new SqlParameter("@TrustAbbreviation", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strTrustAbbrev;

                pSqlParameter[2] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intTrustMID;

                strStoredProcName = "usp_tbl_Trust_M_ValidationName";

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

		#region Select TrustMID Details by RegistrationCode
		/// <summary>
		/// Select all details of School for selected SchoolName from tbl_School_M table
		/// Created By : NafisaMulla, 23/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="SchoolName"></param>
		/// <returns></returns>
		public ApplicationResult Trust_Select_TrustMID(string strRegistrationCode)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@RegistrationCode", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strRegistrationCode;

				strStoredProcName = "usp_tbl_Trust_M_TrustMID";

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

        #region Download Database Backup

        public ApplicationResult Download_DatabaseBackup()
        {
            try
            {
                sSql = "usp_Database_BackUp";
                DataTable dtOrganisation = new DataTable();
                dtOrganisation = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtOrganisation);
                //objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
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


