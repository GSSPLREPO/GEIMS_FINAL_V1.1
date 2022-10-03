using System;
using System.Data;
using System.Data.SqlClient;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
    public class ShiftMasterBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
		#endregion

		#region Select All Shift Details
		/// <summary>
		/// To Select All data from the tbl_Class_M table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public ApplicationResult Shift_SelectAll(int intTrustMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intTrustMID;

				sSql = "usp_tbl_Shift_SelectAll";
				DataTable dtClass = new DataTable();
				dtClass = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

				ApplicationResult objResults = new ApplicationResult(dtClass);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region Select All Shift Details for Journal Entry
		/// <summary>
		/// To Select All data from the tbl_GeneralLedger_M table
		/// Created By : Viral, 10/7/2014
		/// Modified By :
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public ApplicationResult Shift_SelectAll_For_Employee(int intTrustID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intTrustID;

				

				sSql = "usp_tbl_Shift_SelectAll_ForEmployee";
				DataTable dtShiftName = new DataTable();
				dtShiftName = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

				ApplicationResult objResults = new ApplicationResult(dtShiftName);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region Delete Shift Details by ShiftMID
		/// <summary>
		/// To Delete details of Class for selected ClassMID from tbl_Class_M table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="intShiftMID"></param>
		/// <returns></returns>
		public ApplicationResult Shift_Delete(int intShiftMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@ShiftMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intShiftMID;

				

				strStoredProcName = "usp_tbl_ShiftMaster_Delete";

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

		#region ValidateName for ShiftName
		/// <summary>
		/// Function which validates whether the ClassName already exits in tbl_Class_M table.
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="strClassName"></param>
		/// <returns></returns>
		public ApplicationResult Shift_ValidateName(string strShiftName , int intTrustMID   )
		{
			try
			{
				pSqlParameter = new SqlParameter[2];

				pSqlParameter[0] = new SqlParameter("@ShiftName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strShiftName;

				//pSqlParameter[1] = new SqlParameter("@ShiftMID", SqlDbType.Int);
				//pSqlParameter[1].Direction = ParameterDirection.Input;
				//pSqlParameter[1].Value = intShiftMID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;



                strStoredProcName = "usp_tbl_Shift_Validate_ShiftName";

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

		#region Insert Shift Details
		/// <summary>
		/// To Insert details of Class in tbl_Class_M table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="objShiftMasterBO"></param>
		/// <returns></returns>
		public ApplicationResult Shift_Insert(ShiftMasterBO objShiftMasterBO)
		{
			try
			{
				pSqlParameter = new SqlParameter[12];


				pSqlParameter[0] = new SqlParameter("@TrustID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = objShiftMasterBO.TrustMID;

				pSqlParameter[1] = new SqlParameter("@ShiftName", SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = objShiftMasterBO.ShiftName;

				pSqlParameter[2] = new SqlParameter("@StartTime", SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = objShiftMasterBO.StartTime;

				pSqlParameter[3] = new SqlParameter("@RecessStartTime", SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = objShiftMasterBO.RecessStartTime;

				pSqlParameter[4] = new SqlParameter("@RecessEndTime", SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = objShiftMasterBO.RecessEndTime;

				pSqlParameter[5] = new SqlParameter("@EndTime", SqlDbType.VarChar);
				pSqlParameter[5].Direction = ParameterDirection.Input;
				pSqlParameter[5].Value = objShiftMasterBO.EndTime;


				pSqlParameter[6] = new SqlParameter("@TotalWH", SqlDbType.VarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
				pSqlParameter[6].Value = objShiftMasterBO.TotalWH;

				pSqlParameter[7] = new SqlParameter("@FirstHalfTime", SqlDbType.VarChar);
				pSqlParameter[7].Direction = ParameterDirection.Input;
				pSqlParameter[7].Value = objShiftMasterBO.FirstHalfWH;

				pSqlParameter[8] = new SqlParameter("@SecondHalfTime", SqlDbType.VarChar);
				pSqlParameter[8].Direction = ParameterDirection.Input;
				pSqlParameter[8].Value = objShiftMasterBO.SecondHalfWH;


				pSqlParameter[9] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
				pSqlParameter[9].Direction = ParameterDirection.Input;
				pSqlParameter[9].Value = objShiftMasterBO.LastModifiedUserID;

				pSqlParameter[10] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
				pSqlParameter[10].Direction = ParameterDirection.Input;
				pSqlParameter[10].Value = objShiftMasterBO.LastModifiedDate;

				pSqlParameter[11] = new SqlParameter("@IsDeleted", SqlDbType.Int);
				pSqlParameter[11].Direction = ParameterDirection.Input;
				pSqlParameter[11].Value = objShiftMasterBO.IsDeleted;

			


				sSql = "usp_tbl_ShiftMaster_Insert";
				DataTable dtResult = new DataTable();
				dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);
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
				objShiftMasterBO = null;
			}
		}
		#endregion

		#region Select  Intime Details ShiftName Wise
		/// <summary>
		/// To Select All data from the tbl_Section_M table
		/// Created By : NafisaMulla, 4/7/2014
		/// Modified By :
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public ApplicationResult InTime_Select_ShiftWise(int intShiftMID, int intTrustID)
		{
			try
			{
				pSqlParameter = new SqlParameter[2];

				pSqlParameter[0] = new SqlParameter("@ShiftMID", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intShiftMID;

				pSqlParameter[1] = new SqlParameter("@TrustID", SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = intTrustID;

				sSql = "usp_tbl_ShiftDate_ShiftNameWise";

				DataTable dtSection = new DataTable();
				dtSection = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

				ApplicationResult objResults = new ApplicationResult(dtSection);
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
