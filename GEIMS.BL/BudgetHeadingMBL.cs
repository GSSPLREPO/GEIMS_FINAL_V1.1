using System;
using System.Data;
using System.Data.SqlClient;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
    public class BudgetHeadingMBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        #region Insert BudgetHeading Details
        public ApplicationResult BudgetHeading_Insert(BudgetHeadingMBO objBudgetHeadingMBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

                pSqlParameter[0] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objBudgetHeadingMBO.BudgetHeadingMID;

                pSqlParameter[1] = new SqlParameter("@BudgetCategoryMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objBudgetHeadingMBO.BudgetCategoryMID;       

                pSqlParameter[2] = new SqlParameter("@HeadingName", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objBudgetHeadingMBO.HeadingName;

                pSqlParameter[3] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objBudgetHeadingMBO.IsDeleted;

                pSqlParameter[4] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objBudgetHeadingMBO.CreatedBy;

                pSqlParameter[5] = new SqlParameter("@CreatedDate", SqlDbType.Date);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objBudgetHeadingMBO.CreatedDate;

                sSql = "usp_tbl_BudgetHeading_M_Insert";
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
                objBudgetHeadingMBO = null;
            }
        }
        #endregion

        #region BudgetHeading Select All    
        public ApplicationResult BudgetHeading_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_BudgetHeading_M_SelecetAll";
                DataTable dtDepartment = new DataTable();
                dtDepartment = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtDepartment);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Delete BudgetHeading by BudgetHeadingMID
        public ApplicationResult BudgetHeading_Delete(int intBudgetHeadingMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intBudgetHeadingMID;
              
                strStoredProcName = "usp_tbl_BudgetHeading_M_Delete";

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

        #region Select BudgetHeading Details by BudgetHeadingMID              
        public ApplicationResult BudgetHeading_SelectById(int BudgetHeadingMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = BudgetHeadingMID;
           
                strStoredProcName = "usp_tbl_BudgetHeading_M_SelectById";

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

        #region Update BudgetHeading Details    
        public ApplicationResult BudgetHeading_Update(BudgetHeadingMBO objBudgetHeadingMBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];

                pSqlParameter[0] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objBudgetHeadingMBO.BudgetHeadingMID;

                pSqlParameter[1] = new SqlParameter("@BudgetCategoryMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objBudgetHeadingMBO.BudgetCategoryMID;

                pSqlParameter[2] = new SqlParameter("@HeadingName", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objBudgetHeadingMBO.HeadingName;

                pSqlParameter[3] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objBudgetHeadingMBO.ModifiedBy;

                pSqlParameter[4] = new SqlParameter("@ModifiedDate", SqlDbType.Date);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objBudgetHeadingMBO.ModifiedDate;

                sSql = "usp_tbl_BudgetHeading_M_Update";
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
                objBudgetHeadingMBO = null;
            }
        }
        #endregion

        #region ValidateName for Heading
        public ApplicationResult HeadingM_ValidateName(int intBudgetHeadingMID, string strHeadingName)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intBudgetHeadingMID;

                pSqlParameter[1] = new SqlParameter("@HeadingName", SqlDbType.NVarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strHeadingName;

                strStoredProcName = "usp_tbl_BudgetHeading_M_ValidateName";

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

        #region Select BudgetHeading Cascade by BudgetHeadingMID
        /// <summary>
        /// Select all details of GeneralLedger for selected LedgerID from tbl_GeneralLedger_M table
        /// Created By : Viral, 10/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="intLedgerID"></param>
        /// <returns></returns>
        public ApplicationResult BudgetHeading_M_Cascade(int intHeadingMID, int intTrustMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = intHeadingMID
                };
                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = intTrustMID
                };
               
                strStoredProcName = "usp_tbl_BudgetHeading_M_Cascade";

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

        #region Select BudgetHeading Cascade_Delete by BudgetHeadingMID
        /// <summary>
        /// Select all details of GeneralLedger for selected LedgerID from tbl_GeneralLedger_M table
        /// Created By : Viral, 10/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="intLedgerID"></param>
        /// <returns></returns>
        public ApplicationResult BudgetHeading_M_Cascade_Delete(int intHeadingMID, int intTrustMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = intHeadingMID
                };
                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = intTrustMID
                };

                strStoredProcName = "usp_tbl_BudgetHeading_M_Cascade_Delete";

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
