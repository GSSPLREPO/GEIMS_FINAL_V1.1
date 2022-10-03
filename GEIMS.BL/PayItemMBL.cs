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
    /// Class Created By : Darshan, 09/04/2014
	/// Summary description for Organisation.
    /// </summary>
	public class PayItemMBL 
	{
		#region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion
		
		

		#region Select All PayItemM Details
        /// <summary>
        /// To Select All data from the tbl_PayItem_M table
        /// Created By : Darshan, 09/04/2014
		/// Modified By :
        /// </summary>
		/// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult  PayItemM_SelectAll()
        {
			try
            {
				sSql = "usp_tbl_PayItem_M_SelectAll";
                DataTable dtPayItemM  = new DataTable();
                dtPayItemM = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtPayItemM);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
		


		#region Select PayItemM Details by PayItemMID
        /// <summary>
        /// Select all details of PayItemM for selected PayItemMID from tbl_PayItem_M table
        /// Created By : Darshan, 09/04/2014
		/// Modified By :
        /// </summary>
        /// <param name="intPayItemMID"></param>
        /// <returns></returns>
		public ApplicationResult PayItemM_Select(int intPayItemMID)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@PayItemMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intPayItemMID;

				strStoredProcName = "usp_tbl_PayItem_M_Select";
				
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
		


		#region Delete PayItemM Details by PayItemMID
        /// <summary>
        /// To Delete details of PayItemM for selected PayItemMID from tbl_PayItem_M table
        /// Created By : Darshan, 09/04/2014
		/// Modified By :
        /// </summary>
        /// <param name="intPayItemMID"></param>
        /// <returns></returns>
        public ApplicationResult PayItemM_Delete(int intPayItemMID, int intLastModifiedUserID, string strLastModifiedDate)
		{
            try
            {
                pSqlParameter = new SqlParameter[3];
				
				pSqlParameter[0] = new SqlParameter("@PayItemMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intPayItemMID;

                pSqlParameter[1] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastModifiedUserID;

                pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifiedDate;

				strStoredProcName = "usp_tbl_PayItem_M_Delete";
				
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
		


		#region Insert PayItemM Details
		/// <summary>
        /// To Insert details of PayItemM in tbl_PayItem_M table
        /// Created By : Darshan, 09/04/2014
		/// Modified By :
        /// </summary>
        /// <param name="objPayItemMBO"></param>
        /// <returns></returns>
        public ApplicationResult PayItemM_Insert(PayItemMBO objPayItemMBO)
        {
            try
            {
				pSqlParameter = new SqlParameter[5];
                
				
          		pSqlParameter[0] = new SqlParameter("@Name",SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objPayItemMBO.Name;
 
				pSqlParameter[1] = new SqlParameter("@Description",SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objPayItemMBO.Description;
 
				pSqlParameter[2] = new SqlParameter("@Type",SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objPayItemMBO.Type;
 
				pSqlParameter[3] = new SqlParameter("@CreatedUserID",SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objPayItemMBO.CreatedUserID;
 
				pSqlParameter[4] = new SqlParameter("@CreatedDate",SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objPayItemMBO.CreatedDate;
 
				
				sSql = "usp_tbl_PayItem_M_Insert";
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
                objPayItemMBO = null;
            }
        }
        #endregion
		


		#region Update PayItemM Details
		/// <summary>
        /// To Update details of PayItemM in tbl_PayItem_M table
        /// Created By : Darshan, 09/04/2014
		/// Modified By :
        /// </summary>
        /// <param name="objPayItemMBO"></param>
        /// <returns></returns>
        public ApplicationResult PayItemM_Update(PayItemMBO objPayItemMBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];
                
				
          		pSqlParameter[0] = new SqlParameter("@PayItemMID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objPayItemMBO.PayItemMID;
 
				pSqlParameter[1] = new SqlParameter("@Name",SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objPayItemMBO.Name;
 
				pSqlParameter[2] = new SqlParameter("@Description",SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objPayItemMBO.Description;
 
				pSqlParameter[3] = new SqlParameter("@Type",SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objPayItemMBO.Type;
 
				pSqlParameter[4] = new SqlParameter("@LastModifiedUserID",SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objPayItemMBO.LastModifiedUserID;
 
				pSqlParameter[5] = new SqlParameter("@LastModifiedDate",SqlDbType.VarChar);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objPayItemMBO.LastModifiedDate;

		
				sSql = "usp_tbl_PayItem_M_Update";
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
                objPayItemMBO = null;
            }
        }
        #endregion



        #region ValidateName for Pay Item
        public ApplicationResult PayItemM_ValidateName(int intPayItemMID, string strName)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@PayItemMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intPayItemMID;

                pSqlParameter[1] = new SqlParameter("@Name", SqlDbType.NVarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strName;

                strStoredProcName = "usp_tbl_PayItem_M_ValidateName";

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
