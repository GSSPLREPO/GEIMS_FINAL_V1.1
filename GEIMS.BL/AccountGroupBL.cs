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
    /// Class Created By : Viral, 9/30/2014
	/// Summary description for Organisation.
    /// </summary>
	public class AccountGroupBL 
	{
		#region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion		
		
		#region Select All AccountGroup Details
        /// <summary>
        /// To Select All data from the tbl_AccountGroup_M table
        /// Created By : Viral, 9/30/2014
		/// Modified By :
        /// </summary>
		/// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult  AccountGroup_SelectAll()
        {
			try
            {
				sSql = "usp_tbl_AccountGroup_M_SelectAll";
                DataTable dtAccountGroup  = new DataTable();
                dtAccountGroup = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtAccountGroup);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion        
		
		#region Select AccountGroup Details by AccountGroupID
        /// <summary>
        /// Select all details of AccountGroup for selected AccountGroupID from tbl_AccountGroup_M table
        /// Created By : Viral, 9/30/2014
		/// Modified By :
        /// </summary>
        /// <param name="intAccountGroupID"></param>
        /// <returns></returns>
		public ApplicationResult AccountGroup_Select(int intAccountGroupID)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@AccountGroupID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intAccountGroupID;

				strStoredProcName = "usp_tbl_AccountGroup_M_Select";
				
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
		
		#region Delete AccountGroup Details by AccountGroupID
        /// <summary>
        /// To Delete details of AccountGroup for selected AccountGroupID from tbl_AccountGroup_M table
        /// Created By : Viral, 9/30/2014
		/// Modified By :
        /// </summary>
        /// <param name="intAccountGroupID"></param>
        /// <returns></returns>
		public ApplicationResult AccountGroup_Delete(int intAccountGroupID,int intUserID,string strLastModifideDate)
		{
            try
            {
                pSqlParameter = new SqlParameter[3];
				
				pSqlParameter[0] = new SqlParameter("@AccountGroupID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intAccountGroupID;

                pSqlParameter[1] = new SqlParameter("@LastModifideUserID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intUserID;

                pSqlParameter[2] = new SqlParameter("@LastModifideDate", SqlDbType.NVarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifideDate;

				strStoredProcName = "usp_tbl_AccountGroup_M_Delete";
				
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
		
		#region Insert AccountGroup Details
		/// <summary>
        /// To Insert details of AccountGroup in tbl_AccountGroup_M table
        /// Created By : Viral, 9/30/2014
		/// Modified By :
        /// </summary>
        /// <param name="objAccountGroupBO"></param>
        /// <returns></returns>
        public ApplicationResult AccountGroup_Insert(AccountGroupBO objAccountGroupBO)
        {
            try
            {
				pSqlParameter = new SqlParameter[13];
                
				
          		pSqlParameter[0] = new SqlParameter("@TrustMID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objAccountGroupBO.TrustMID;
 
				pSqlParameter[1] = new SqlParameter("@SchoolMID",SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objAccountGroupBO.SchoolMID;
 
				pSqlParameter[2] = new SqlParameter("@AccountGroupName",SqlDbType.NVarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objAccountGroupBO.AccountGroupName;
 
				pSqlParameter[3] = new SqlParameter("@AccountGroupDefaultNature",SqlDbType.NVarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objAccountGroupBO.AccountGroupDefaultNature;
 
				pSqlParameter[4] = new SqlParameter("@GroupNature",SqlDbType.NVarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objAccountGroupBO.GroupNature;
 
				pSqlParameter[5] = new SqlParameter("@SubGroupID",SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objAccountGroupBO.SubGroupID;
 
				pSqlParameter[6] = new SqlParameter("@SubGroupOf",SqlDbType.NVarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objAccountGroupBO.SubGroupOf;
 
				pSqlParameter[7] = new SqlParameter("@Description",SqlDbType.NVarChar);
				pSqlParameter[7].Direction = ParameterDirection.Input;
          		pSqlParameter[7].Value = objAccountGroupBO.Description;
 
				pSqlParameter[8] = new SqlParameter("@IsDeleted",SqlDbType.Int);
				pSqlParameter[8].Direction = ParameterDirection.Input;
          		pSqlParameter[8].Value = objAccountGroupBO.IsDeleted;

                pSqlParameter[9] = new SqlParameter("@CreatedDate", SqlDbType.NVarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objAccountGroupBO.CreatedDate;

                pSqlParameter[10] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objAccountGroupBO.CreatedUserID;

				pSqlParameter[11] = new SqlParameter("@LastModifideDate",SqlDbType.NVarChar);
				pSqlParameter[11].Direction = ParameterDirection.Input;
          		pSqlParameter[11].Value = objAccountGroupBO.LastModifideDate;
 
				pSqlParameter[12] = new SqlParameter("@LastModifideUserID",SqlDbType.Int);
				pSqlParameter[12].Direction = ParameterDirection.Input;
          		pSqlParameter[12].Value = objAccountGroupBO.LastModifideUserID;

		
				sSql = "usp_tbl_AccountGroup_M_Insert";
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
                objAccountGroupBO = null;
            }
        }
        #endregion
		
		#region Update AccountGroup Details
		/// <summary>
        /// To Update details of AccountGroup in tbl_AccountGroup_M table
        /// Created By : Viral, 9/30/2014
		/// Modified By :
        /// </summary>
        /// <param name="objAccountGroupBO"></param>
        /// <returns></returns>
        public ApplicationResult AccountGroup_Update(AccountGroupBO objAccountGroupBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[14];
                
				
          		pSqlParameter[0] = new SqlParameter("@AccountGroupID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objAccountGroupBO.AccountGroupID;
 
				pSqlParameter[1] = new SqlParameter("@TrustMID",SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objAccountGroupBO.TrustMID;
 
				pSqlParameter[2] = new SqlParameter("@SchoolMID",SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objAccountGroupBO.SchoolMID;
 
				pSqlParameter[3] = new SqlParameter("@AccountGroupName",SqlDbType.NVarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objAccountGroupBO.AccountGroupName;
 
				pSqlParameter[4] = new SqlParameter("@AccountGroupDefaultNature",SqlDbType.NVarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objAccountGroupBO.AccountGroupDefaultNature;
 
				pSqlParameter[5] = new SqlParameter("@GroupNature",SqlDbType.NVarChar);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objAccountGroupBO.GroupNature;
 
				pSqlParameter[6] = new SqlParameter("@SubGroupID",SqlDbType.Int);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objAccountGroupBO.SubGroupID;
 
				pSqlParameter[7] = new SqlParameter("@SubGroupOf",SqlDbType.NVarChar);
				pSqlParameter[7].Direction = ParameterDirection.Input;
          		pSqlParameter[7].Value = objAccountGroupBO.SubGroupOf;
 
				pSqlParameter[8] = new SqlParameter("@Description",SqlDbType.NVarChar);
				pSqlParameter[8].Direction = ParameterDirection.Input;
          		pSqlParameter[8].Value = objAccountGroupBO.Description;
 
				pSqlParameter[9] = new SqlParameter("@IsDeleted",SqlDbType.Int);
				pSqlParameter[9].Direction = ParameterDirection.Input;
          		pSqlParameter[9].Value = objAccountGroupBO.IsDeleted;

                pSqlParameter[10] = new SqlParameter("@CreatedDate", SqlDbType.NVarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objAccountGroupBO.CreatedDate;

                pSqlParameter[11] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objAccountGroupBO.CreatedUserID;
 
				pSqlParameter[12] = new SqlParameter("@LastModifideDate",SqlDbType.NVarChar);
				pSqlParameter[12].Direction = ParameterDirection.Input;
          		pSqlParameter[12].Value = objAccountGroupBO.LastModifideDate;
 
				pSqlParameter[13] = new SqlParameter("@LastModifideUserID",SqlDbType.Int);
				pSqlParameter[13].Direction = ParameterDirection.Input;
          		pSqlParameter[13].Value = objAccountGroupBO.LastModifideUserID;

		
				sSql = "usp_tbl_AccountGroup_M_Update";
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
                objAccountGroupBO = null;
            }
        }
        #endregion		
		
		#region Select AccountGroup Details by AccountGroupName
        /// <summary>
        /// Select all details of AccountGroup for selected AccountGroupName from tbl_AccountGroup_M table
        /// Created By : Viral, 9/30/2014
		/// Modified By :
        /// </summary>
        /// <param name="AccountGroupName"></param>
        /// <returns></returns>
		public ApplicationResult AccountGroup_Select_byAccountGroupName(string strAccountGroupName)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@AccountGroupName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strAccountGroupName;

				strStoredProcName = "usp_tbl_AccountGroup_M_Select_ByAccountGroup";
				
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
		
		#region ValidateName for AccountGroup 
        /// <summary>
        /// Function which validates whether the AccountGroupName already exits in tbl_AccountGroup_M table.
        /// Created By : Viral, 9/30/2014
		/// Modified By :
        /// </summary>
        /// <param name="strAccountGroupName"></param>
        /// <returns></returns>
		public ApplicationResult AccountGroup_ValidateName(int intTrustID,string strAccountGroupName,int intAccountGroupID)
		{
            try
            {
                pSqlParameter = new SqlParameter[3];
				
				pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustID;

                //pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                //pSqlParameter[1].Direction = ParameterDirection.Input;
                //pSqlParameter[1].Value = intSchoolID;

                pSqlParameter[1] = new SqlParameter("@AccountGroupName", SqlDbType.NVarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strAccountGroupName;

                pSqlParameter[2] = new SqlParameter("@AccountGroupID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intAccountGroupID;

                strStoredProcName = "usp_tbl_AccountGroup_M_ValidateName";
				
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

        #region Select Account Group Cascade by AccountGroupID
        /// <summary>
        /// Select all details of GeneralLedger for selected LedgerID from tbl_GeneralLedger_M table
        /// Created By : Viral, 10/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="intLedgerID"></param>
        /// <returns></returns>
        public ApplicationResult AccountGroup_Cascade(int intAccountGroupID, int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@AccountGroupID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intAccountGroupID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_AccountGroup_M_Cascade";

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
