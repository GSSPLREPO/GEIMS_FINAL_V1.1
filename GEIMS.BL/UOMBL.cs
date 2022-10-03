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
    /// Class Created By : Chintan, 10/9/2014
	/// Summary description for Organisation.
    /// </summary>
	public class UOMBL 
	{
		#region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion
		
		
		#region Select All UOM Details
        /// <summary>
        /// To Select All data from the tbl_UOM_M table
        /// Created By : Chintan, 10/9/2014
		/// Modified By :
        /// </summary>
		/// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult UOM_SelectAll()
        {
			try
            {
				sSql = "usp_tbl_UOM_M_SelectAll";
                DataTable dtUOM  = new DataTable();
                dtUOM = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtUOM);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select UOM Details for DropDown
        /// <summary>
        /// To Select All data from the tbl_UOM_M table
        /// Created By : Chintan, 10/9/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult UOM_Select_DropDown(int intMaterialID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@MaterialID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMaterialID;

                sSql = "usp_tbl_UOM_M_Select_ForDropDown";

                DataTable dtUOM = new DataTable();
                dtUOM = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtUOM);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
		
		#region Select UOM Details by UOMID
        /// <summary>
        /// Select all details of UOM for selected UOMID from tbl_UOM_M table
        /// Created By : Chintan, 10/9/2014
		/// Modified By :
        /// </summary>
        /// <param name="intUOMID"></param>
        /// <returns></returns>
		public ApplicationResult UOM_Select(int intUOMID)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@UOMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intUOMID;

				strStoredProcName = "usp_tbl_UOM_M_Select";
				
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
		
		#region Delete UOM Details by UOMID
        /// <summary>
        /// To Delete details of UOM for selected UOMID from tbl_UOM_M table
        /// Created By : Chintan, 10/9/2014
		/// Modified By :
        /// </summary>
        /// <param name="intUOMID"></param>
        /// <returns></returns>
		public ApplicationResult UOM_Delete(int intUOMID)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@UOMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intUOMID;

				strStoredProcName = "usp_tbl_UOM_M_Delete";
				
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
		
		#region Insert UOM Details
		/// <summary>
        /// To Insert details of UOM in tbl_UOM_M table
        /// Created By : Chintan, 10/9/2014
		/// Modified By :
        /// </summary>
        /// <param name="objUOMBO"></param>
        /// <returns></returns>
        public ApplicationResult UOM_Insert(UOMBO objUOMBO)
        {
            try
            {
				pSqlParameter = new SqlParameter[7];
                
				
          		pSqlParameter[0] = new SqlParameter("@UOMName",SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objUOMBO.UOMName;
 
				pSqlParameter[1] = new SqlParameter("@Description",SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objUOMBO.Description;
 
				pSqlParameter[2] = new SqlParameter("@IsDeleted",SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objUOMBO.IsDeleted;
 
				pSqlParameter[3] = new SqlParameter("@CreatedDate",SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objUOMBO.CreatedDate;
 
				pSqlParameter[4] = new SqlParameter("@CreatedUserID",SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objUOMBO.CreatedUserID;
 
				pSqlParameter[5] = new SqlParameter("@LastModifiedDate",SqlDbType.VarChar);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objUOMBO.LastModifiedDate;
 
				pSqlParameter[6] = new SqlParameter("@LastModifiedUserId",SqlDbType.Int);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objUOMBO.LastModifiedUserId;

		
				sSql = "usp_tbl_UOM_M_Insert";
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
                objUOMBO = null;
            }
        }
        #endregion
		
		#region Update UOM Details
		/// <summary>
        /// To Update details of UOM in tbl_UOM_M table
        /// Created By : Chintan, 10/9/2014
		/// Modified By :
        /// </summary>
        /// <param name="objUOMBO"></param>
        /// <returns></returns>
        public ApplicationResult UOM_Update(UOMBO objUOMBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[8];
                
				
          		pSqlParameter[0] = new SqlParameter("@UOMID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objUOMBO.UOMID;
 
				pSqlParameter[1] = new SqlParameter("@UOMName",SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objUOMBO.UOMName;
 
				pSqlParameter[2] = new SqlParameter("@Description",SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objUOMBO.Description;
 
				pSqlParameter[3] = new SqlParameter("@IsDeleted",SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objUOMBO.IsDeleted;
 
				pSqlParameter[4] = new SqlParameter("@CreatedDate",SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objUOMBO.CreatedDate;
 
				pSqlParameter[5] = new SqlParameter("@CreatedUserID",SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objUOMBO.CreatedUserID;
 
				pSqlParameter[6] = new SqlParameter("@LastModifiedDate",SqlDbType.VarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objUOMBO.LastModifiedDate;
 
				pSqlParameter[7] = new SqlParameter("@LastModifiedUserId",SqlDbType.Int);
				pSqlParameter[7].Direction = ParameterDirection.Input;
          		pSqlParameter[7].Value = objUOMBO.LastModifiedUserId;

		
				sSql = "usp_tbl_UOM_M_Update";
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
                objUOMBO = null;
            }
        }
        #endregion
	}
}



