using System;
using System.Data;
using System.Data.SqlClient;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
	/// <summary>
	/// Class Created By : NafisaMulla, 2/7/2014
	/// Summary description for Organisation.
	/// </summary>
	public class FeesCategoryBL
	{
		#region user defined variables
		public string sSql;
		public string strStoredProcName;
		public SqlParameter[] pSqlParameter = null;
		#endregion


		#region Select All FeesCategory Details
		/// <summary>
		/// To Select All data from the tbl_FeesCategory_M table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public ApplicationResult FeesCategory_SelectAll(int intSchoolMID)
		{
			try
			{
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

				sSql = "usp_tbl_FeesCategory_M_SelectAll";
				DataTable dtFeesCategory = new DataTable();
                dtFeesCategory = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

				ApplicationResult objResults = new ApplicationResult(dtFeesCategory);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion


        #region Select All FeesCategory Details for Class template
        /// <summary>
        /// To Select All data from the tbl_FeesCategory_M table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult FeesCategory_Select_For_ClassTemplate(int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                sSql = "usp_tbl_FeesCategory_M_SelectAll_For_ClassTemplate";
                DataTable dtFeesCategory = new DataTable();
                dtFeesCategory = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtFeesCategory);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



		#region Select FeesCategory Details by FeesCategoryMID
		/// <summary>
		/// Select all details of FeesCategory for selected FeesCategoryMID from tbl_FeesCategory_M table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="intFeesCategoryMID"></param>
		/// <returns></returns>
        public ApplicationResult FeesCategory_Select(int intFeesCategoryMID, int intSchoolMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[2];

				pSqlParameter[0] = new SqlParameter("@FeesCategoryMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intFeesCategoryMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

				strStoredProcName = "usp_tbl_FeesCategory_M_Select";

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

		#region Delete FeesCategory Details by FeesCategoryMID
		/// <summary>
		/// To Delete details of FeesCategory for selected FeesCategoryMID from tbl_FeesCategory_M table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="intFeesCategoryMID"></param>
		/// <returns></returns>
		public ApplicationResult FeesCategory_Delete(int intFeesCategoryMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@FeesCategoryMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intFeesCategoryMID;

				strStoredProcName = "usp_tbl_FeesCategory_M_Delete";

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

		#region Insert FeesCategory Details
		/// <summary>
		/// To Insert details of FeesCategory in tbl_FeesCategory_M table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="objFeesCategoryBO"></param>
		/// <returns></returns>
		public ApplicationResult FeesCategory_Insert(FeesCategoryBO objFeesCategoryBO)
		{
			try
			{
				pSqlParameter = new SqlParameter[10];


				pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = objFeesCategoryBO.TrustMID;

				pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = objFeesCategoryBO.SchoolMID;

				pSqlParameter[2] = new SqlParameter("@FeesName", SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = objFeesCategoryBO.FeesName;

				pSqlParameter[3] = new SqlParameter("@FeesType", SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = objFeesCategoryBO.FeesType;

                pSqlParameter[4] = new SqlParameter("@OutstandingMonth", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objFeesCategoryBO.OutstandingMonth;

				pSqlParameter[5] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
				pSqlParameter[5].Value = objFeesCategoryBO.LastModifiedUserID;

				pSqlParameter[6] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
				pSqlParameter[6].Value = objFeesCategoryBO.LastModifiedDate;

				pSqlParameter[7] = new SqlParameter("@Isdeleted", SqlDbType.Int);
				pSqlParameter[7].Direction = ParameterDirection.Input;
				pSqlParameter[7].Value = objFeesCategoryBO.Isdeleted;

                pSqlParameter[8] = new SqlParameter("@FeeAbbreviation", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objFeesCategoryBO.FeeAbbreviation;

                pSqlParameter[9] = new SqlParameter("@FeeGroupID", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objFeesCategoryBO.FeeGroupID;


				sSql = "usp_tbl_FeesCategory_M_Insert";
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
				objFeesCategoryBO = null;
			}
		}
		#endregion

		#region Update FeesCategory Details
		/// <summary>
		/// To Update details of FeesCategory in tbl_FeesCategory_M table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="objFeesCategoryBO"></param>
		/// <returns></returns>
		public ApplicationResult FeesCategory_Update(FeesCategoryBO objFeesCategoryBO)
		{
			try
			{
				pSqlParameter = new SqlParameter[11];


				pSqlParameter[0] = new SqlParameter("@FeesCategoryMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = objFeesCategoryBO.FeesCategoryMID;

				pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = objFeesCategoryBO.TrustMID;

				pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = objFeesCategoryBO.SchoolMID;

				pSqlParameter[3] = new SqlParameter("@FeesName", SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = objFeesCategoryBO.FeesName;

				pSqlParameter[4] = new SqlParameter("@FeesType", SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = objFeesCategoryBO.FeesType;

                pSqlParameter[5] = new SqlParameter("@OutstandingMonth", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objFeesCategoryBO.OutstandingMonth;

				pSqlParameter[6] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
				pSqlParameter[6].Direction = ParameterDirection.Input;
				pSqlParameter[6].Value = objFeesCategoryBO.LastModifiedUserID;

				pSqlParameter[7] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
				pSqlParameter[7].Direction = ParameterDirection.Input;
				pSqlParameter[7].Value = objFeesCategoryBO.LastModifiedDate;

				pSqlParameter[8] = new SqlParameter("@Isdeleted", SqlDbType.Int);
				pSqlParameter[8].Direction = ParameterDirection.Input;
				pSqlParameter[8].Value = objFeesCategoryBO.Isdeleted;

                pSqlParameter[9] = new SqlParameter("@FeeAbbreviation", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objFeesCategoryBO.FeeAbbreviation;

                pSqlParameter[10] = new SqlParameter("@FeeGroupID", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objFeesCategoryBO.FeeGroupID;


				sSql = "usp_tbl_FeesCategory_M_Update";
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
				objFeesCategoryBO = null;
			}
		}
		#endregion


        #region Update FeesCategory Details For Priority
        /// <summary>
        /// To Update details of FeesCategory in tbl_FeesCategory_M table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="objFeesCategoryBO"></param>
        /// <returns></returns>
        public ApplicationResult FeesCategory_Update_For_Priority(FeesCategoryBO objFeesCategoryBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[12];


                pSqlParameter[0] = new SqlParameter("@FeesCategoryMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objFeesCategoryBO.FeesCategoryMID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objFeesCategoryBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objFeesCategoryBO.SchoolMID;

                pSqlParameter[3] = new SqlParameter("@FeesName", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objFeesCategoryBO.FeesName;

                pSqlParameter[4] = new SqlParameter("@FeesType", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objFeesCategoryBO.FeesType;

                pSqlParameter[5] = new SqlParameter("@OutstandingMonth", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objFeesCategoryBO.OutstandingMonth;

                pSqlParameter[6] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objFeesCategoryBO.LastModifiedUserID;

                pSqlParameter[7] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objFeesCategoryBO.LastModifiedDate;

                pSqlParameter[8] = new SqlParameter("@Isdeleted", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objFeesCategoryBO.Isdeleted;

                pSqlParameter[9] = new SqlParameter("@FeeAbbreviation", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objFeesCategoryBO.FeeAbbreviation;

                pSqlParameter[10] = new SqlParameter("@FeeGroupID", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objFeesCategoryBO.FeeGroupID;

                pSqlParameter[11] = new SqlParameter("@Priority", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objFeesCategoryBO.Priority;


                sSql = "usp_tbl_FeesCategory_M_Update_For_Priority";
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
                objFeesCategoryBO = null;
            }
        }
        #endregion




		#region Select FeesCategory Details by FeesCategoryName
		/// <summary>
		/// Select all details of FeesCategory for selected FeesCategoryName from tbl_FeesCategory_M table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="FeesCategoryName"></param>
		/// <returns></returns>
		public ApplicationResult FeesCategory_Select_byFeesCategoryName(string strFeesCategoryName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@FeesCategoryName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strFeesCategoryName;

				strStoredProcName = "usp_tbl_FeesCategory_M_Select_ByFeesCategory";

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


		#region ValidateName for FeesCategory
		/// <summary>
		/// Function which validates whether the FeesCategoryName already exits in tbl_FeesCategory_M table.
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="strFeesCategoryName"></param>
		/// <returns></returns>
		public ApplicationResult FeesCategory_ValidateName(string strFeesCategoryName, int intCategotyMID , int intSchoolMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@FeesName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strFeesCategoryName;

                pSqlParameter[1] = new SqlParameter("@FeesCategoryMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intCategotyMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_FeesCategory_M_ValidationName";

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
        public ApplicationResult Validate_FeesCategory_Delete(int intFeesCategoryMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@FeesCategoryMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intFeesCategoryMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_FeesCategory_M_Validate_For_Delete";

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


