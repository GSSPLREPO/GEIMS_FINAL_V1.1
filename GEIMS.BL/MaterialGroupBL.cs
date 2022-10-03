using System;
using System.Threading.Tasks;
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
    /// Class Created By : Chintan, 10/7/2014
	/// Summary description for Organisation.
    /// </summary>
	public class MaterialGroupBL 
	{
		#region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion
		
		
		#region Select All MaterialGroup Details
        /// <summary>
        /// To Select All data from the tbl_MaterialGroup_M table
        /// Created By : Chintan, 10/7/2014
		/// Modified By :
        /// </summary>
		/// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult  MaterialGroup_SelectAll()
        {
			try
            {
				sSql = "usp_tbl_MaterialGroup_M_SelectAll";
                DataTable dtMaterialGroup  = new DataTable();
                dtMaterialGroup = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtMaterialGroup);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select All Years
        /// <summary>
        /// To Select All data from the tbl_MaterialGroup_M table
        /// Created By : Chintan, 10/7/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult GetYear()
        {
            try
            {
                sSql = "usp_tbl_GetYear_SelectAll";
                DataTable dtMaterialGroup = new DataTable();
                dtMaterialGroup = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtMaterialGroup);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
		
		#region Select MaterialGroup Details by MaterialGroupID
        /// <summary>
        /// Select all details of MaterialGroup for selected MaterialGroupID from tbl_MaterialGroup_M table
        /// Created By : Chintan, 10/7/2014
		/// Modified By :
        /// </summary>
        /// <param name="intMaterialGroupID"></param>
        /// <returns></returns>
		public ApplicationResult MaterialGroup_Select(int intMaterialGroupID)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@MaterialGroupID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMaterialGroupID;

				strStoredProcName = "usp_tbl_MaterialGroup_M_Select";
				
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
		
		#region Delete MaterialGroup Details by MaterialGroupID
        /// <summary>
        /// To Delete details of MaterialGroup for selected MaterialGroupID from tbl_MaterialGroup_M table
        /// Created By : Chintan, 10/7/2014
		/// Modified By :
        /// </summary>
        /// <param name="intMaterialGroupID"></param>
        /// <returns></returns>
		public ApplicationResult MaterialGroup_Delete(int intMaterialGroupID)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@MaterialGroupID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMaterialGroupID;

				strStoredProcName = "usp_tbl_MaterialGroup_M_Delete";
				
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
		
		#region Insert MaterialGroup Details
		/// <summary>
        /// To Insert details of MaterialGroup in tbl_MaterialGroup_M table
        /// Created By : Chintan, 10/7/2014
		/// Modified By :
        /// </summary>
        /// <param name="objMaterialGroupBO"></param>
        /// <returns></returns>
        public ApplicationResult MaterialGroup_Insert(MaterialGroupBO objMaterialGroupBO)
        {
            try
            {
				pSqlParameter = new SqlParameter[7];
                
				
          		pSqlParameter[0] = new SqlParameter("@MaterialGroupName",SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objMaterialGroupBO.MaterialGroupName;
 
				pSqlParameter[1] = new SqlParameter("@Description",SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objMaterialGroupBO.Description;
 
				pSqlParameter[2] = new SqlParameter("@IsDeleted",SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objMaterialGroupBO.IsDeleted;
 
				pSqlParameter[3] = new SqlParameter("@CreatedUserID",SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objMaterialGroupBO.CreatedUserID;
 
				pSqlParameter[4] = new SqlParameter("@CreatedDate",SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objMaterialGroupBO.CreatedDate;
 
				pSqlParameter[5] = new SqlParameter("@LastModifiedUserID",SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objMaterialGroupBO.LastModifiedUserID;
 
				pSqlParameter[6] = new SqlParameter("@LastModifiedDate",SqlDbType.VarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objMaterialGroupBO.LastModifiedDate;

		
				sSql = "usp_tbl_MaterialGroup_M_Insert";
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
                objMaterialGroupBO = null;
            }
        }
        #endregion
		
		#region Update MaterialGroup Details
		/// <summary>
        /// To Update details of MaterialGroup in tbl_MaterialGroup_M table
        /// Created By : Chintan, 10/7/2014
		/// Modified By :
        /// </summary>
        /// <param name="objMaterialGroupBO"></param>
        /// <returns></returns>
        public ApplicationResult MaterialGroup_Update(MaterialGroupBO objMaterialGroupBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[8];
                
				
          		pSqlParameter[0] = new SqlParameter("@MaterialGroupID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objMaterialGroupBO.MaterialGroupID;
 
				pSqlParameter[1] = new SqlParameter("@MaterialGroupName",SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objMaterialGroupBO.MaterialGroupName;
 
				pSqlParameter[2] = new SqlParameter("@Description",SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objMaterialGroupBO.Description;
 
				pSqlParameter[3] = new SqlParameter("@IsDeleted",SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objMaterialGroupBO.IsDeleted;
 
				pSqlParameter[4] = new SqlParameter("@CreatedUserID",SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objMaterialGroupBO.CreatedUserID;
 
				pSqlParameter[5] = new SqlParameter("@CreatedDate",SqlDbType.VarChar);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objMaterialGroupBO.CreatedDate;
 
				pSqlParameter[6] = new SqlParameter("@LastModifiedUserID",SqlDbType.Int);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objMaterialGroupBO.LastModifiedUserID;
 
				pSqlParameter[7] = new SqlParameter("@LastModifiedDate",SqlDbType.VarChar);
				pSqlParameter[7].Direction = ParameterDirection.Input;
          		pSqlParameter[7].Value = objMaterialGroupBO.LastModifiedDate;

		
				sSql = "usp_tbl_MaterialGroup_M_Update";
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
                objMaterialGroupBO = null;
            }
        }
        #endregion

        #region Select MaterialGroup 
        /// <summary>
        /// Select all details of MaterialGroup for selected MaterialGroupID from tbl_MaterialGroup_M table
        /// Created By : Chintan, 10/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="intMaterialGroupID"></param>
        /// <returns></returns>
        public ApplicationResult MaterialGroup_Select_ForMaterial(int intMaterialGroupID, int intTrustMID, int intSchoolMID, string strYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@MaterialGroupID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMaterialGroupID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                pSqlParameter[3] = new SqlParameter("@Year", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strYear;

                strStoredProcName = "usp_tbl_Material_M_Select_ByGroupId";

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



