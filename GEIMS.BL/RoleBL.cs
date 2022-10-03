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
    /// Class Created By : Darshan, 09/15/2014
	/// Summary description for Organisation.
    /// </summary>
	public class RoleBL 
	{
		#region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion
		
		

		#region Select All Role Details
        /// <summary>
        /// To Select All data from the tbl_Role_M table
        /// Created By : Darshan, 09/15/2014
		/// Modified By :
        /// </summary>
		/// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult  Role_SelectAll()
        {
			try
            {
				sSql = "usp_tbl_Role_M_SelectAll";
                DataTable dtRole  = new DataTable();
                dtRole = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtRole);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select All Role Details
        /// <summary>
        /// To Select Admin and SuperAdmin data from the tbl_Role_M table
        /// Created By : Arpit, 26/11/2021
		/// Modified By :
        /// </summary>
		/// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult Role_SelectAllAdminSuperAdmin()
        {
            try
            {
                sSql = "usp_tbl_Role_M_SelectAdminSuperAdmin";
                DataTable dtRole = new DataTable();
                dtRole = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtRole);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select All Role Details
        /// <summary>
        /// To Select All data from the tbl_Role_M table
        /// Created By : Darshan, 09/15/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult Role_SelectAll_ForDropDown()
        {
            try
            {
                sSql = "usp_tbl_Role_M_SelectAll_ForDropDown";
                DataTable dtRole = new DataTable();
                dtRole = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtRole);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

		#region Select Role Details by RoleID
        /// <summary>
        /// Select all details of Role for selected RoleID from tbl_Role_M table
        /// Created By : Darshan, 09/15/2014
		/// Modified By :
        /// </summary>
        /// <param name="intRoleID"></param>
        /// <returns></returns>
		public ApplicationResult Role_Select(int intRoleID)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@RoleID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intRoleID;

				strStoredProcName = "usp_tbl_Role_M_Select";
				
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
		


		#region Delete Role Details by RoleID
        /// <summary>
        /// To Delete details of Role for selected RoleID from tbl_Role_M table
        /// Created By : Darshan, 09/15/2014
		/// Modified By :
        /// </summary>
        /// <param name="intRoleID"></param>
        /// <returns></returns>
        public ApplicationResult Role_Delete(int intRoleID, int intLastModifiedUserID, string strLastModifiedDate)
		{
            try
            {
                pSqlParameter = new SqlParameter[3];
				
				pSqlParameter[0] = new SqlParameter("@RoleID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intRoleID;

                pSqlParameter[1] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastModifiedUserID;

                pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifiedDate;

				strStoredProcName = "usp_tbl_Role_M_Delete";
				
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
		


		#region Insert Role Details
		/// <summary>
        /// To Insert details of Role in tbl_Role_M table
        /// Created By : Darshan, 09/15/2014
		/// Modified By :
        /// </summary>
        /// <param name="objRoleBO"></param>
        /// <returns></returns>
        public ApplicationResult Role_Insert(RoleBO objRoleBO)
        {
            try
            {
				pSqlParameter = new SqlParameter[4];
                
				
          		pSqlParameter[0] = new SqlParameter("@RoleName",SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objRoleBO.RoleName;
 
				pSqlParameter[1] = new SqlParameter("@Description",SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objRoleBO.Description;
 
				pSqlParameter[2] = new SqlParameter("@CreatedUserID",SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objRoleBO.CreatedUserID;
 
				pSqlParameter[3] = new SqlParameter("@CreatedDate",SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objRoleBO.CreatedDate;
 
				
				sSql = "usp_tbl_Role_M_Insert";
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
                objRoleBO = null;
            }
        }
        #endregion
		


		#region Update Role Details
		/// <summary>
        /// To Update details of Role in tbl_Role_M table
        /// Created By : Darshan, 09/15/2014
		/// Modified By :
        /// </summary>
        /// <param name="objRoleBO"></param>
        /// <returns></returns>
        public ApplicationResult Role_Update(RoleBO objRoleBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];
                
				
          		pSqlParameter[0] = new SqlParameter("@RoleID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objRoleBO.RoleID;
 
				pSqlParameter[1] = new SqlParameter("@RoleName",SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objRoleBO.RoleName;
 
				pSqlParameter[2] = new SqlParameter("@Description",SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objRoleBO.Description;
 
				pSqlParameter[3] = new SqlParameter("@LastModifiedUserID",SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objRoleBO.LastModifiedUserID;
 
				pSqlParameter[4] = new SqlParameter("@LastModifiedDate",SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objRoleBO.LastModifiedDate;

		
				sSql = "usp_tbl_Role_M_Update";
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
                objRoleBO = null;
            }
        }
        #endregion



        #region ValidateName for Role
        public ApplicationResult Role_ValidateName(int intRoleID, string strRoleName)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@RoleID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intRoleID;

                pSqlParameter[1] = new SqlParameter("@RoleName", SqlDbType.NVarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strRoleName;

                strStoredProcName = "usp_tbl_Role_M_ValidateName";

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
