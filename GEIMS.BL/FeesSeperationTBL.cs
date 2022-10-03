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
	public class FeesSeperationTBL
	{
		#region user defined variables
		public string sSql;
		public string strStoredProcName;
		public SqlParameter[] pSqlParameter = null;
		#endregion


		#region Select All FeesSeperationT Details
		/// <summary>
		/// To Select All data from the tbl_FeesSeperation_T table
		/// Created By : NafisaMulla, 1/4/2015
		/// Modified By :
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public ApplicationResult FeesSeperationT_SelectAll()
		{
			try
			{
				sSql = "usp_tbl_FeesSeperation_T_SelectAll";
				DataTable dtFeesSeperationT = new DataTable();
				dtFeesSeperationT = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

				ApplicationResult objResults = new ApplicationResult(dtFeesSeperationT);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region Select FeesSeperationT Details by FeesSeperationTID
		/// <summary>
		/// Select all details of FeesSeperationT for selected FeesSeperationTID from tbl_FeesSeperation_T table
		/// Created By : NafisaMulla, 1/4/2015
		/// Modified By :
		/// </summary>
		/// <param name="intFeesSeperationTID"></param>
		/// <returns></returns>
		public ApplicationResult FeesSeperationT_Select(int intFeesSeperationTID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@FeesSeperationTID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intFeesSeperationTID;

				strStoredProcName = "usp_tbl_FeesSeperation_T_Select";

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

		#region Delete FeesSeperationT Details by FeesSeperationTID
		/// <summary>
		/// To Delete details of FeesSeperationT for selected FeesSeperationTID from tbl_FeesSeperation_T table
		/// Created By : NafisaMulla, 1/4/2015
		/// Modified By :
		/// </summary>
		/// <param name="intFeesSeperationTID"></param>
		/// <returns></returns>
		public ApplicationResult FeesSeperationT_Delete(int intFeesSeperationTID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@FeesSeperationTID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intFeesSeperationTID;

				strStoredProcName = "usp_tbl_FeesSeperation_T_Delete";

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

		#region Insert FeesSeperationT Details
		/// <summary>
		/// To Insert details of FeesSeperationT in tbl_FeesSeperation_T table
		/// Created By : NafisaMulla, 1/4/2015
		/// Modified By :
		/// </summary>
		/// <param name="objFeesSeperationTBO"></param>
		/// <returns></returns>
		public ApplicationResult FeesSeperationT_Insert(FeesSeperationTBO objFeesSeperationTBO)
		{
			try
			{
				pSqlParameter = new SqlParameter[7];


				pSqlParameter[0] = new SqlParameter("@FeesCategoryMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = objFeesSeperationTBO.FeesCategoryMID;

				pSqlParameter[1] = new SqlParameter("@FeesName", SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = objFeesSeperationTBO.FeesName;

				pSqlParameter[2] = new SqlParameter("@FeesAmount", SqlDbType.Float);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = objFeesSeperationTBO.FeesAmount;

				pSqlParameter[3] = new SqlParameter("@DisplayName", SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = objFeesSeperationTBO.DisplayName;

				pSqlParameter[4] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = objFeesSeperationTBO.LastModifiedUserID;

				pSqlParameter[5] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
				pSqlParameter[5].Direction = ParameterDirection.Input;
				pSqlParameter[5].Value = objFeesSeperationTBO.LastModifiedDate;

				pSqlParameter[6] = new SqlParameter("@Isdeleted", SqlDbType.Int);
				pSqlParameter[6].Direction = ParameterDirection.Input;
				pSqlParameter[6].Value = objFeesSeperationTBO.Isdeleted;


				sSql = "usp_tbl_FeesSeperation_T_Insert";
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
				objFeesSeperationTBO = null;
			}
		}
		#endregion

		#region Update FeesSeperationT Details
		/// <summary>
		/// To Update details of FeesSeperationT in tbl_FeesSeperation_T table
		/// Created By : NafisaMulla, 1/4/2015
		/// Modified By :
		/// </summary>
		/// <param name="objFeesSeperationTBO"></param>
		/// <returns></returns>
		public ApplicationResult FeesSeperationT_Update(FeesSeperationTBO objFeesSeperationTBO)
		{
			try
			{
				pSqlParameter = new SqlParameter[8];


				pSqlParameter[0] = new SqlParameter("@FeesSeperationTID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = objFeesSeperationTBO.FeesSeperationTID;

				pSqlParameter[1] = new SqlParameter("@FeesCategoryMID", SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = objFeesSeperationTBO.FeesCategoryMID;

				pSqlParameter[2] = new SqlParameter("@FeesName", SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = objFeesSeperationTBO.FeesName;

				pSqlParameter[3] = new SqlParameter("@FeesAmount", SqlDbType.Float);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = objFeesSeperationTBO.FeesAmount;

				pSqlParameter[4] = new SqlParameter("@DisplayName", SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = objFeesSeperationTBO.DisplayName;

				pSqlParameter[5] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
				pSqlParameter[5].Value = objFeesSeperationTBO.LastModifiedUserID;

				pSqlParameter[6] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
				pSqlParameter[6].Value = objFeesSeperationTBO.LastModifiedDate;

				pSqlParameter[7] = new SqlParameter("@Isdeleted", SqlDbType.Int);
				pSqlParameter[7].Direction = ParameterDirection.Input;
				pSqlParameter[7].Value = objFeesSeperationTBO.Isdeleted;


				sSql = "usp_tbl_FeesSeperation_T_Update";
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
				objFeesSeperationTBO = null;
			}
		}
		#endregion




		#region Select FeesSeperationT Details by FeesSeperationTName
		/// <summary>
		/// Select all details of FeesSeperationT for selected FeesSeperationTName from tbl_FeesSeperation_T table
		/// Created By : NafisaMulla, 1/4/2015
		/// Modified By :
		/// </summary>
		/// <param name="FeesSeperationTName"></param>
		/// <returns></returns>
		public ApplicationResult FeesSeperationT_Select_byFeesSeperationTName(string strFeesSeperationTName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@FeesSeperationTName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strFeesSeperationTName;

				strStoredProcName = "usp_tbl_FeesSeperation_T_Select_ByFeesSeperationT";

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


		#region ValidateName for FeesSeperationT
		/// <summary>
		/// Function which validates whether the FeesSeperationTName already exits in tbl_FeesSeperation_T table.
		/// Created By : NafisaMulla, 1/4/2015
		/// Modified By :
		/// </summary>
		/// <param name="strFeesSeperationTName"></param>
		/// <returns></returns>
		public ApplicationResult FeesSeperationT_ValidateName(string strFeesSeperationTName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@FeesSeperationTName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strFeesSeperationTName;

				strStoredProcName = "usp_tbl_FeesSeperation_T_Validate_FeesSeperationTName";

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


