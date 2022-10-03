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
	public class MediumBL
	{
		#region user defined variables
		public string sSql;
		public string strStoredProcName;
		public SqlParameter[] pSqlParameter = null;
		#endregion


		#region Select All Medium Details
		/// <summary>
		/// To Select All data from the MediumM table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public ApplicationResult Medium_SelectAll()
		{
			try
			{
				sSql = "usp_MediumM_SelectAll";
				DataTable dtMedium = new DataTable();
				dtMedium = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

				ApplicationResult objResults = new ApplicationResult(dtMedium);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region Select Medium Details by MediumMID
		/// <summary>
		/// Select all details of Medium for selected MediumMID from MediumM table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="intMediumMID"></param>
		/// <returns></returns>
		public ApplicationResult Medium_Select(int intMediumMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@MediumMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intMediumMID;

				strStoredProcName = "usp_MediumM_Select";

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

		#region Delete Medium Details by MediumMID
		/// <summary>
		/// To Delete details of Medium for selected MediumMID from MediumM table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="intMediumMID"></param>
		/// <returns></returns>
		public ApplicationResult Medium_Delete(int intMediumMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@MediumMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intMediumMID;

				strStoredProcName = "usp_MediumM_Delete";

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

		#region Insert Medium Details
		/// <summary>
		/// To Insert details of Medium in MediumM table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="objMediumBO"></param>
		/// <returns></returns>
		public ApplicationResult Medium_Insert(MediumBO objMediumBO)
		{
			try
			{
				pSqlParameter = new SqlParameter[5];


				pSqlParameter[0] = new SqlParameter("@MediumName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = objMediumBO.MediumName;

				pSqlParameter[1] = new SqlParameter("@Description", SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = objMediumBO.Description;

				pSqlParameter[2] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = objMediumBO.LastModifiedUserID;

				pSqlParameter[3] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = objMediumBO.LastModifiedDate;

				pSqlParameter[4] = new SqlParameter("@IsDeleted", SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = objMediumBO.IsDeleted;


				sSql = "usp_MediumM_Insert";
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
				objMediumBO = null;
			}
		}
		#endregion

		#region Update Medium Details
		/// <summary>
		/// To Update details of Medium in MediumM table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="objMediumBO"></param>
		/// <returns></returns>
		public ApplicationResult Medium_Update(MediumBO objMediumBO)
		{
			try
			{
				pSqlParameter = new SqlParameter[6];


				pSqlParameter[0] = new SqlParameter("@MediumMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = objMediumBO.MediumMID;

				pSqlParameter[1] = new SqlParameter("@MediumName", SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = objMediumBO.MediumName;

				pSqlParameter[2] = new SqlParameter("@Description", SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = objMediumBO.Description;

				pSqlParameter[3] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = objMediumBO.LastModifiedUserID;

				pSqlParameter[4] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = objMediumBO.LastModifiedDate;

				pSqlParameter[5] = new SqlParameter("@IsDeleted", SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
				pSqlParameter[5].Value = objMediumBO.IsDeleted;


				sSql = "usp_MediumM_Update";
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
				objMediumBO = null;
			}
		}
		#endregion




		#region Select Medium Details by MediumName
		/// <summary>
		/// Select all details of Medium for selected MediumName from MediumM table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="MediumName"></param>
		/// <returns></returns>
		public ApplicationResult Medium_Select_byMediumName(string strMediumName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@MediumName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strMediumName;

				strStoredProcName = "usp_MediumM_Select_ByMedium";

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


		#region ValidateName for Medium
		/// <summary>
		/// Function which validates whether the MediumName already exits in MediumM table.
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="strMediumName"></param>
		/// <returns></returns>
		public ApplicationResult Medium_ValidateName(string strMediumName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@MediumName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strMediumName;

				strStoredProcName = "usp_MediumM_Validate_MediumName";

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


