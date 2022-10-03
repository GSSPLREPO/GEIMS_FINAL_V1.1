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
    /// Class Created By : Viral, 10/13/2014
	/// Summary description for Organisation.
    /// </summary>
	public class JournalVoucherTBL 
	{
		#region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion
		
		
		#region Select All JournalVoucherT Details
        /// <summary>
        /// To Select All data from the tbl_JournalVoucher_T table
        /// Created By : Viral, 10/13/2014
		/// Modified By :
        /// </summary>
		/// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult  JournalVoucherT_SelectAll()
        {
			try
            {
				sSql = "usp_tbl_JournalVoucher_T_SelectAll";
                DataTable dtJournalVoucherT  = new DataTable();
                dtJournalVoucherT = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtJournalVoucherT);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
		
		#region Select JournalVoucherT Details by JournalTID
        /// <summary>
        /// Select all details of JournalVoucherT for selected JournalTID from tbl_JournalVoucher_T table
        /// Created By : Viral, 10/13/2014
		/// Modified By :
        /// </summary>
        /// <param name="intJournalTID"></param>
        /// <returns></returns>
		public ApplicationResult JournalVoucherT_Select(int intJournalTID)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@JournalTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intJournalTID;

				strStoredProcName = "usp_tbl_JournalVoucher_T_Select";
				
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
		
		#region Delete JournalVoucherT Details by JournalTID
        /// <summary>
        /// To Delete details of JournalVoucherT for selected JournalTID from tbl_JournalVoucher_T table
        /// Created By : Viral, 10/13/2014
		/// Modified By :
        /// </summary>
        /// <param name="intJournalTID"></param>
        /// <returns></returns>
		public ApplicationResult JournalVoucherT_Delete(string strVoucherCode)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@VoucherCode", SqlDbType.NVarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strVoucherCode;

				strStoredProcName = "usp_tbl_JournalVoucher_T_Delete";
				
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
		
		#region Insert JournalVoucherT Details
		/// <summary>
        /// To Insert details of JournalVoucherT in tbl_JournalVoucher_T table
        /// Created By : Viral, 10/13/2014
		/// Modified By :
        /// </summary>
        /// <param name="objJournalVoucherTBO"></param>
        /// <returns></returns>
        public ApplicationResult JournalVoucherT_Insert(JournalVoucherTBO objJournalVoucherTBO)
        {
            try
            {
				pSqlParameter = new SqlParameter[8];
                
				
          		pSqlParameter[0] = new SqlParameter("@JournalID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objJournalVoucherTBO.JournalID;
 
				pSqlParameter[1] = new SqlParameter("@OppositeJournalID",SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objJournalVoucherTBO.OppositeJournalID;
 
				pSqlParameter[2] = new SqlParameter("@Amount",SqlDbType.Float);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objJournalVoucherTBO.Amount;
 
				pSqlParameter[3] = new SqlParameter("@CreatedDate",SqlDbType.NVarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objJournalVoucherTBO.CreatedDate;
 
				pSqlParameter[4] = new SqlParameter("@CreatedUserID",SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objJournalVoucherTBO.CreatedUserID;
 
				pSqlParameter[5] = new SqlParameter("@IsDeleted",SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objJournalVoucherTBO.IsDeleted;
 
				pSqlParameter[6] = new SqlParameter("@LastModifideUserID",SqlDbType.Int);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objJournalVoucherTBO.LastModifideUserID;
 
				pSqlParameter[7] = new SqlParameter("@LastModifideDate",SqlDbType.NVarChar);
				pSqlParameter[7].Direction = ParameterDirection.Input;
          		pSqlParameter[7].Value = objJournalVoucherTBO.LastModifideDate;
		
				sSql = "usp_tbl_JournalVoucher_T_Insert";
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
                objJournalVoucherTBO = null;
            }
        }
        #endregion
		
		#region Update JournalVoucherT Details
		/// <summary>
        /// To Update details of JournalVoucherT in tbl_JournalVoucher_T table
        /// Created By : Viral, 10/13/2014
		/// Modified By :
        /// </summary>
        /// <param name="objJournalVoucherTBO"></param>
        /// <returns></returns>
        public ApplicationResult JournalVoucherT_Update(JournalVoucherTBO objJournalVoucherTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[9];
                
				
          		pSqlParameter[0] = new SqlParameter("@JournalTID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objJournalVoucherTBO.JournalTID;
 
				pSqlParameter[1] = new SqlParameter("@JournalID",SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objJournalVoucherTBO.JournalID;
 
				pSqlParameter[2] = new SqlParameter("@OppositeJournalID",SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objJournalVoucherTBO.OppositeJournalID;
 
				pSqlParameter[3] = new SqlParameter("@Amount",SqlDbType.Float);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objJournalVoucherTBO.Amount;
 
				pSqlParameter[4] = new SqlParameter("@CreatedDate",SqlDbType.NVarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objJournalVoucherTBO.CreatedDate;
 
				pSqlParameter[5] = new SqlParameter("@CreatedUserID",SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objJournalVoucherTBO.CreatedUserID;
 
				pSqlParameter[6] = new SqlParameter("@IsDeleted",SqlDbType.Int);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objJournalVoucherTBO.IsDeleted;
 
				pSqlParameter[7] = new SqlParameter("@LastModifideUserID",SqlDbType.Int);
				pSqlParameter[7].Direction = ParameterDirection.Input;
          		pSqlParameter[7].Value = objJournalVoucherTBO.LastModifideUserID;
 
				pSqlParameter[8] = new SqlParameter("@LastModifideDate",SqlDbType.NVarChar);
				pSqlParameter[8].Direction = ParameterDirection.Input;
          		pSqlParameter[8].Value = objJournalVoucherTBO.LastModifideDate;

		
				sSql = "usp_tbl_JournalVoucher_T_Update";
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
                objJournalVoucherTBO = null;
            }
        }
        #endregion
		
		
		
		
		#region Select JournalVoucherT Details by JournalVoucherTName
        /// <summary>
        /// Select all details of JournalVoucherT for selected JournalVoucherTName from tbl_JournalVoucher_T table
        /// Created By : Viral, 10/13/2014
		/// Modified By :
        /// </summary>
        /// <param name="JournalVoucherTName"></param>
        /// <returns></returns>
		public ApplicationResult JournalVoucherT_Select_byJournalVoucherTName(string strJournalVoucherTName)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@JournalVoucherTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strJournalVoucherTName;

				strStoredProcName = "usp_tbl_JournalVoucher_T_Select_ByJournalVoucherT";
				
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
		
		
		#region ValidateName for JournalVoucherT 
        /// <summary>
        /// Function which validates whether the JournalVoucherTName already exits in tbl_JournalVoucher_T table.
        /// Created By : Viral, 10/13/2014
		/// Modified By :
        /// </summary>
        /// <param name="strJournalVoucherTName"></param>
        /// <returns></returns>
		public ApplicationResult JournalVoucherT_ValidateName(string strJournalVoucherTName)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@JournalVoucherTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strJournalVoucherTName;

				strStoredProcName = "usp_tbl_JournalVoucher_T_Validate_JournalVoucherTName";
				
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
