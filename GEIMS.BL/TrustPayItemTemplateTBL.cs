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
    /// Class Created By : NafisaMulla, 10-04-2015
	/// Summary description for Organisation.
    /// </summary>
	public class TrustPayItemTemplateTBl 
	{
		#region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion
				
		#region Select All TrustPayItemTemplateT Details
        /// <summary>
        /// To Select All data from the tbl_TrustPayItemTemplate_T table
        /// Created By : NafisaMulla, 10-04-2015
		/// Modified By :
        /// </summary>
		/// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult  TrustPayItemTemplateT_SelectAll()
        {
			try
            {
				sSql = "usp_tbl_TrustPayItemTemplate_T_SelectAll";
                DataTable dtTrustPayItemTemplateT  = new DataTable();
                dtTrustPayItemTemplateT = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtTrustPayItemTemplateT);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
		
		#region Select TrustPayItemTemplateT Details by 
        /// <summary>
        /// Select all details of TrustPayItemTemplateT for selected  from tbl_TrustPayItemTemplate_T table
        /// Created By : NafisaMulla, 10-04-2015
		/// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
		public ApplicationResult TrustPayItemTemplateT_Select(int intTrustTemplateID)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@TrustTemplateID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value =intTrustTemplateID ;

				strStoredProcName = "usp_tbl_TrustPayItemTemplate_T_Select";
				
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

        #region Select TrustTemplate Details by TrustTemplateID
        /// <summary>
        /// Select all details of TrustTemplate for selected TrustTemplateID from tbl_TrustTemplate_M table
        /// Created By : NafisaMulla, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intTrustTemplateID"></param>
        /// <returns></returns>
        public ApplicationResult TrustPayItemTemplate_Select(int intTrustTemplateID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TrustTemplateID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustTemplateID;

                strStoredProcName = "usp_tbl_TrustPayItemTemplate_T_Select";

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
		
		#region Insert TrustPayItemTemplateT Details
		/// <summary>
        /// To Insert details of TrustPayItemTemplateT in tbl_TrustPayItemTemplate_T table
        /// Created By : NafisaMulla, 10-04-2015
		/// Modified By :
        /// </summary>
        /// <param name="objTrustPayItemTemplateTBO"></param>
        /// <returns></returns>
        public ApplicationResult TrustPayItemTemplateT_Insert(TrustPayItemTemplateTBo objTrustPayItemTemplateTBO)
        {
            try
            {
				pSqlParameter = new SqlParameter[11];
                
				
          		pSqlParameter[0] = new SqlParameter("@TemplateID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objTrustPayItemTemplateTBO.TemplateID;
 
				pSqlParameter[1] = new SqlParameter("@PayItemID",SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objTrustPayItemTemplateTBO.PayItemID;
 
				pSqlParameter[2] = new SqlParameter("@PayItemType",SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objTrustPayItemTemplateTBO.PayItemType;
 
				pSqlParameter[3] = new SqlParameter("@PayItemDependsOn",SqlDbType.NVarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objTrustPayItemTemplateTBO.PayItemDependsOn;
 
				pSqlParameter[4] = new SqlParameter("@Percentage",SqlDbType.Float);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objTrustPayItemTemplateTBO.Percentage;
 
				pSqlParameter[5] = new SqlParameter("@Amount",SqlDbType.Float);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objTrustPayItemTemplateTBO.Amount;
 
				pSqlParameter[6] = new SqlParameter("@IsDeleted",SqlDbType.Int);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objTrustPayItemTemplateTBO.IsDeleted;
 
				pSqlParameter[7] = new SqlParameter("@CreatedUserID",SqlDbType.Int);
				pSqlParameter[7].Direction = ParameterDirection.Input;
          		pSqlParameter[7].Value = objTrustPayItemTemplateTBO.CreatedUserID;
 
				pSqlParameter[8] = new SqlParameter("@CreatedDate",SqlDbType.VarChar);
				pSqlParameter[8].Direction = ParameterDirection.Input;
          		pSqlParameter[8].Value = objTrustPayItemTemplateTBO.CreatedDate;
 
				pSqlParameter[9] = new SqlParameter("@LastModifiedUserID",SqlDbType.Int);
				pSqlParameter[9].Direction = ParameterDirection.Input;
          		pSqlParameter[9].Value = objTrustPayItemTemplateTBO.LastModifiedUserID;
 
				pSqlParameter[10] = new SqlParameter("@LastModifiedDate",SqlDbType.VarChar);
				pSqlParameter[10].Direction = ParameterDirection.Input;
          		pSqlParameter[10].Value = objTrustPayItemTemplateTBO.LastModifiedDate;

		
				sSql = "usp_tbl_TrustPayItemTemplate_T_Insert";
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
                objTrustPayItemTemplateTBO = null;
            }
        }
        #endregion
		
		#region Update TrustPayItemTemplateT Details
		/// <summary>
        /// To Update details of TrustPayItemTemplateT in tbl_TrustPayItemTemplate_T table
        /// Created By : NafisaMulla, 10-04-2015
		/// Modified By :
        /// </summary>
        /// <param name="objTrustPayItemTemplateTBO"></param>
        /// <returns></returns>
        public ApplicationResult TrustPayItemTemplateT_Update(TrustPayItemTemplateTBo objTrustPayItemTemplateTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[9];
                
				
          		pSqlParameter[0] = new SqlParameter("@TrustTemplateID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objTrustPayItemTemplateTBO.TrustTemplateID;
 
				pSqlParameter[1] = new SqlParameter("@TemplateID",SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objTrustPayItemTemplateTBO.TemplateID;
 
				pSqlParameter[2] = new SqlParameter("@PayItemID",SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objTrustPayItemTemplateTBO.PayItemID;
 
				pSqlParameter[3] = new SqlParameter("@PayItemType",SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objTrustPayItemTemplateTBO.PayItemType;
 
				pSqlParameter[4] = new SqlParameter("@PayItemDependsOn",SqlDbType.NVarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objTrustPayItemTemplateTBO.PayItemDependsOn;
 
				pSqlParameter[5] = new SqlParameter("@Percentage",SqlDbType.Float);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objTrustPayItemTemplateTBO.Percentage;
 
				pSqlParameter[6] = new SqlParameter("@Amount",SqlDbType.Float);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objTrustPayItemTemplateTBO.Amount;
 
				pSqlParameter[7] = new SqlParameter("@LastModifiedUserID",SqlDbType.Int);
				pSqlParameter[7].Direction = ParameterDirection.Input;
          		pSqlParameter[7].Value = objTrustPayItemTemplateTBO.LastModifiedUserID;
 
				pSqlParameter[8] = new SqlParameter("@LastModifiedDate",SqlDbType.VarChar);
				pSqlParameter[8].Direction = ParameterDirection.Input;
          		pSqlParameter[8].Value = objTrustPayItemTemplateTBO.LastModifiedDate;

		
				sSql = "usp_tbl_TrustPayItemTemplate_T_Update";
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
                objTrustPayItemTemplateTBO = null;
            }
        }
        #endregion

        #region Delete TrustTemplate Details by TrustTemplateID with Cascade
        /// <summary>
        /// To Delete details of TrustTemplate for selected TrustTemplateID from tbl_TrustTemplate_M table 
        /// If Not Delete deails of TrustTemplate for selected TrustTEmplateID from tbl_TrustTemplate_M table when use tbl_UserPayItemTemplate_T
        /// Created By : Arpit Shah, 06-12-2021
        /// Modified By : Arpit Shah, 06-12-2021 [Merge Cascade Query]
        /// </summary>
        /// <param name="intTrustTemplateID"></param>
        /// <returns></returns>
        //public ApplicationResult TrustPayItemTemplate_Delete(int intTrustTemplateID, int intLastUserID, string strLastDate, int intPayItemID, int intTrustMID, int intSchoolMID)
        //public ApplicationResult TrustPayItemTemplate_Delete(int intTrustTemplateID, int intLastUserID, string strLastDate)
        public ApplicationResult TrustPayItemTemplate_Delete(int intTrustTemplateID, int intLastUserID, string strLastDate, int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];

                pSqlParameter[0] = new SqlParameter("@TrustTemplateID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustTemplateID;

                pSqlParameter[1] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastUserID;

                pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastDate;

                //pSqlParameter[3] = new SqlParameter("@PayItemID", SqlDbType.Int);
                //pSqlParameter[3].Direction = ParameterDirection.Input;
                //pSqlParameter[3].Value = intPayItemID;

                pSqlParameter[3] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intTrustMID;

                pSqlParameter[4] = new SqlParameter("@SchoolMID", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_TrustPayItemTemplate_T_Delete";

                int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                //if (iResult > 0)
                //if (iResult > 1)
                if (iResult == 2)
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
				
		#region Select TrustPayItemTemplateT Details by TrustPayItemTemplateTName
        /// <summary>
        /// Select all details of TrustPayItemTemplateT for selected TrustPayItemTemplateTName from tbl_TrustPayItemTemplate_T table
        /// Created By : NafisaMulla, 10-04-2015
		/// Modified By :
        /// </summary>
        /// <param name="TrustPayItemTemplateTName"></param>
        /// <returns></returns>
		public ApplicationResult TrustPayItemTemplateT_Select_byTrustPayItemTemplateTName(string strTrustPayItemTemplateTName)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@TrustPayItemTemplateTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strTrustPayItemTemplateTName;

				strStoredProcName = "usp_tbl_TrustPayItemTemplate_T_Select_ByTrustPayItemTemplateT";
				
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
		
		#region ValidateName for TrustPayItemTemplateT 
        /// <summary>
        /// Function which validates whether the TrustPayItemTemplateTName already exits in tbl_TrustPayItemTemplate_T table.
        /// Created By : NafisaMulla, 10-04-2015
		/// Modified By :
        /// </summary>
        /// <param name="strTrustPayItemTemplateTName"></param>
        /// <returns></returns>
		public ApplicationResult TrustPayItemTemplateT_ValidateName(string strTrustPayItemTemplateTName)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@TrustPayItemTemplateTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strTrustPayItemTemplateTName;

				strStoredProcName = "usp_tbl_TrustPayItemTemplate_T_Validate_TrustPayItemTemplateTName";
				
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

        #region Select TrustTemplate Details by TrustTemplateID
        /// <summary>
        /// Select all details of TrustTemplate for selected TrustTemplateID from tbl_TrustTemplate_M table
        /// Created By : NafisaMulla, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intTrustTemplateID"></param>
        /// <returns></returns>
        public ApplicationResult TrustPayItemTemplate_Select_PayItemWise(int intTemplateID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TemplateID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTemplateID;

                strStoredProcName = "usp_tbl_TrustPayItemTemplate_PayItemID";

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


