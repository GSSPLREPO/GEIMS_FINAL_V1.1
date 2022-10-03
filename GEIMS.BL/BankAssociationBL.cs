using System;
using System.Data;
using System.Data.SqlClient;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
	/// <summary>
	/// Class Created By : NafisaMulla, 26/6/2014
	/// Summary description for Organisation.
	/// </summary>
	public class BankAssociationBL
	{
		#region user defined variables
		public string sSql;
		public string strStoredProcName;
		public SqlParameter[] pSqlParameter = null;
		#endregion


		#region Select All BankAssociation Details
		/// <summary>
		/// To Select All data from the tbl_BankAssociation_M table
		/// Created By : NafisaMulla, 26/6/2014
		/// Modified By :
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public ApplicationResult BankAssociation_SelectAll()
		{
			try
			{
				sSql = "usp_tbl_BankAssociation_M_SelectAll";
				DataTable dtBankAssociation = new DataTable();
				dtBankAssociation = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

				ApplicationResult objResults = new ApplicationResult(dtBankAssociation);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region Select BankAssociation Details by BankAssociationMID
		/// <summary>
		/// Select all details of BankAssociation for selected BankAssociationMID from tbl_BankAssociation_M table
		/// Created By : NafisaMulla, 26/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="intBankAssociationMID"></param>
		/// <returns></returns>
		public ApplicationResult BankAssociation_Select(int intBankAssociationMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@BankAssociationMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intBankAssociationMID;

				strStoredProcName = "usp_tbl_BankAssociation_M_Select";

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

		#region Select BankAssociation Details by TrustMID
		/// <summary>
		/// Select all details of BankAssociation for selected BankAssociationMID from tbl_BankAssociation_M table
		/// Created By : NafisaMulla, 26/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="intBankAssociationMID"></param>
		/// <returns></returns>
		public ApplicationResult BankAssociation_Select_TrustMID(int intTrustMID, int intSchoolMID)
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

				strStoredProcName = "usp_tbl_BankAssociation_M_Select_ByTrustMID";

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

		#region Delete BankAssociation Details by BankAssociationMID
		/// <summary>
		/// To Delete details of BankAssociation for selected BankAssociationMID from tbl_BankAssociation_M table
		/// Created By : NafisaMulla, 26/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="intBankAssociationMID"></param>
		/// <returns></returns>
		public ApplicationResult BankAssociation_Delete(int intBankAssociationMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@BankAssociationMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intBankAssociationMID;

				strStoredProcName = "usp_tbl_BankAssociation_M_Delete";

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

		#region Delete BankAssociation Details by TrustMID
		/// <summary>
		/// To Delete details of BankAssociation for selected BankAssociationMID from tbl_BankAssociation_M table
		/// Created By : NafisaMulla, 26/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="intBankAssociationMID"></param>
		/// <returns></returns>
		public ApplicationResult BankAssociation_Delete_TrustMID(int intTrustMID, int intSchoolMID)
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

				strStoredProcName = "usp_tbl_BankAssociation_M_Delete_TrustMID";

				int iResult = DatabaseTransaction.ExecuteNonQuery(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
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

		#region Insert BankAssociation Details
		/// <summary>
		/// To Insert details of BankAssociation in tbl_BankAssociation_M table
		/// Created By : NafisaMulla, 26/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="objBankAssociationBO"></param>
		/// <returns></returns>
		public ApplicationResult BankAssociation_Insert(BankAssociationBO objBankAssociationBO)
		{
			try
			{
				pSqlParameter = new SqlParameter[13];


				pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = objBankAssociationBO.TrustMID;

				pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = objBankAssociationBO.SchoolMID;

				pSqlParameter[2] = new SqlParameter("@BankName", SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = objBankAssociationBO.BankName;

				pSqlParameter[3] = new SqlParameter("@BranchName", SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = objBankAssociationBO.BranchName;

				pSqlParameter[4] = new SqlParameter("@AccountNameEng", SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = objBankAssociationBO.AccountNameEng;

				pSqlParameter[5] = new SqlParameter("@AccountNameGuj", SqlDbType.NVarChar);
				pSqlParameter[5].Direction = ParameterDirection.Input;
				pSqlParameter[5].Value = objBankAssociationBO.AccountNameGuj;

				pSqlParameter[6] = new SqlParameter("@AccountNo", SqlDbType.VarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
				pSqlParameter[6].Value = objBankAssociationBO.AccountNo;

				pSqlParameter[7] = new SqlParameter("@AccountType", SqlDbType.VarChar);
				pSqlParameter[7].Direction = ParameterDirection.Input;
				pSqlParameter[7].Value = objBankAssociationBO.AccountType;

				pSqlParameter[8] = new SqlParameter("@IfscCode", SqlDbType.VarChar);
				pSqlParameter[8].Direction = ParameterDirection.Input;
				pSqlParameter[8].Value = objBankAssociationBO.IfscCode;

				pSqlParameter[9] = new SqlParameter("@PanNO", SqlDbType.VarChar);
				pSqlParameter[9].Direction = ParameterDirection.Input;
				pSqlParameter[9].Value = objBankAssociationBO.PanNO;

				pSqlParameter[10] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
				pSqlParameter[10].Direction = ParameterDirection.Input;
				pSqlParameter[10].Value = objBankAssociationBO.LastModifiedUserID;

				pSqlParameter[11] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
				pSqlParameter[11].Direction = ParameterDirection.Input;
				pSqlParameter[11].Value = objBankAssociationBO.LastModifiedDate;

				pSqlParameter[12] = new SqlParameter("@IsDeleted", SqlDbType.Int);
				pSqlParameter[12].Direction = ParameterDirection.Input;
				pSqlParameter[12].Value = objBankAssociationBO.IsDeleted;


				sSql = "usp_tbl_BankAssociation_M_Insert";
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
				objBankAssociationBO = null;
			}
		}
		#endregion

		#region Update BankAssociation Details
		/// <summary>
		/// To Update details of BankAssociation in tbl_BankAssociation_M table
		/// Created By : NafisaMulla, 26/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="objBankAssociationBO"></param>
		/// <returns></returns>
		public ApplicationResult BankAssociation_Update(BankAssociationBO objBankAssociationBO)
		{
			try
			{
				pSqlParameter = new SqlParameter[14];


				pSqlParameter[0] = new SqlParameter("@BankAssociationMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = objBankAssociationBO.BankAssociationMID;

				pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = objBankAssociationBO.TrustMID;

				pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = objBankAssociationBO.SchoolMID;

				pSqlParameter[3] = new SqlParameter("@BankName", SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = objBankAssociationBO.BankName;

				pSqlParameter[4] = new SqlParameter("@BranchName", SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = objBankAssociationBO.BranchName;

				pSqlParameter[5] = new SqlParameter("@AccountNameEng", SqlDbType.VarChar);
				pSqlParameter[5].Direction = ParameterDirection.Input;
				pSqlParameter[5].Value = objBankAssociationBO.AccountNameEng;

				pSqlParameter[6] = new SqlParameter("@AccountNameGuj", SqlDbType.NVarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
				pSqlParameter[6].Value = objBankAssociationBO.AccountNameGuj;

				pSqlParameter[7] = new SqlParameter("@AccountNo", SqlDbType.VarChar);
				pSqlParameter[7].Direction = ParameterDirection.Input;
				pSqlParameter[7].Value = objBankAssociationBO.AccountNo;

				pSqlParameter[8] = new SqlParameter("@AccountType", SqlDbType.VarChar);
				pSqlParameter[8].Direction = ParameterDirection.Input;
				pSqlParameter[8].Value = objBankAssociationBO.AccountType;

				pSqlParameter[9] = new SqlParameter("@IfscCode", SqlDbType.VarChar);
				pSqlParameter[9].Direction = ParameterDirection.Input;
				pSqlParameter[9].Value = objBankAssociationBO.IfscCode;

				pSqlParameter[10] = new SqlParameter("@PanNO", SqlDbType.VarChar);
				pSqlParameter[10].Direction = ParameterDirection.Input;
				pSqlParameter[10].Value = objBankAssociationBO.PanNO;

				pSqlParameter[11] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
				pSqlParameter[11].Direction = ParameterDirection.Input;
				pSqlParameter[11].Value = objBankAssociationBO.LastModifiedUserID;

				pSqlParameter[12] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
				pSqlParameter[12].Direction = ParameterDirection.Input;
				pSqlParameter[12].Value = objBankAssociationBO.LastModifiedDate;

				pSqlParameter[13] = new SqlParameter("@IsDeleted", SqlDbType.Int);
				pSqlParameter[13].Direction = ParameterDirection.Input;
				pSqlParameter[13].Value = objBankAssociationBO.IsDeleted;


				sSql = "usp_tbl_BankAssociation_M_Update";
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
				objBankAssociationBO = null;
			}
		}
		#endregion




		#region Select BankAssociation Details by BankAssociationName
		/// <summary>
		/// Select all details of BankAssociation for selected BankAssociationName from tbl_BankAssociation_M table
		/// Created By : NafisaMulla, 26/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="BankAssociationName"></param>
		/// <returns></returns>
		public ApplicationResult BankAssociation_Select_byBankAssociationName(string strBankAssociationName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@BankAssociationName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strBankAssociationName;

				strStoredProcName = "usp_tbl_BankAssociation_M_Select_ByBankAssociation";

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


		#region ValidateName for BankAssociation
		/// <summary>
		/// Function which validates whether the BankAssociationName already exits in tbl_BankAssociation_M table.
		/// Created By : NafisaMulla, 26/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="strBankAssociationName"></param>
		/// <returns></returns>
		public ApplicationResult BankAssociation_ValidateName(string strBankAssociationName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@BankAssociationName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strBankAssociationName;

				strStoredProcName = "usp_tbl_BankAssociation_M_Validate_BankAssociationName";

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


