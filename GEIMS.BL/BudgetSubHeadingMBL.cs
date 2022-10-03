using System;
using System.Data;
using System.Data.SqlClient;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
    public class BudgetSubHeadingMBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        #region Budget Heading for DropDown Category Wise
        public ApplicationResult BudgetHeading_SelectDropDownById(int BudgetCategoryMID, int BudgetHeadingMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@BudgetCategoryMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = BudgetCategoryMID;

                pSqlParameter[1] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = BudgetHeadingMID;

                sSql = "usp_tbl_BudgetSubHeading_M_SelectDropDownById";

                DataTable dtDepartment = new DataTable();
                dtDepartment = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

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

        #region Budget Heading for DropDown for Capital
        public ApplicationResult BudgetHeading_SelectDropDownByCapId(int BudgetCategoryMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@BudgetCategoryMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = BudgetCategoryMID;
              
                sSql = "usp_tbl_BudgetSubHeading_M_SelectDropDownByCapId";

                DataTable dtDepartment = new DataTable();
                dtDepartment = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

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

        #region Budget SubHeading for DropDown Heading Wise
        public ApplicationResult BudgetSubHeading_SelectDropDownById(int BudgetCategoryMID, int BudgetHeadingMID, int BudgetSubHeadingMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@BudgetCategoryMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = BudgetCategoryMID;

                pSqlParameter[1] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = BudgetHeadingMID;

                pSqlParameter[2] = new SqlParameter("@BudgetSubHeadingMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = BudgetSubHeadingMID;

                sSql = "usp_tbl_BudgetSubHeading_M_SelectDropDownByHeadingMID";

                DataTable dtDepartment = new DataTable();
                dtDepartment = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

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

        #region Insert BudgetSubHeading Details
        public ApplicationResult BudgetSubHeading_Insert(BudgetSubHeadingMBO objBudgetSubHeadingMBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[7];

                pSqlParameter[0] = new SqlParameter("@BudgetSubHeadingMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objBudgetSubHeadingMBO.BudgetSubHeadingMID;

                pSqlParameter[1] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objBudgetSubHeadingMBO.BudgetHeadingMID;

                pSqlParameter[2] = new SqlParameter("@BudgetCategoryMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objBudgetSubHeadingMBO.BudgetCategoryMID;

                pSqlParameter[3] = new SqlParameter("@SubHeadingName", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objBudgetSubHeadingMBO.SubHeadingName;

                pSqlParameter[4] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objBudgetSubHeadingMBO.IsDeleted;

                pSqlParameter[5] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objBudgetSubHeadingMBO.CreatedBy;

                pSqlParameter[6] = new SqlParameter("@CreatedDate", SqlDbType.Date);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objBudgetSubHeadingMBO.CreatedDate;

                sSql = "usp_tbl_BudgetSubHeading_M_Insert";
                int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, sSql, pSqlParameter);

                ////DataTable dtResult = new DataTable();
                ////dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);
                ////ApplicationResult objResults = new ApplicationResult(dtResult);
                ////objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                ////return objResults;

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
                objBudgetSubHeadingMBO = null;
            }
        }
        #endregion

        #region BudgetSubHeading Select All    
        public ApplicationResult BudgetSubHeading_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_BudgetSubHeading_M_SelectAll";
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

        #region Delete BudgetSubHeading by BudgetSubHeadingMID
        public ApplicationResult BudgetSubHeading_Delete(int intBudgetSubHeadingMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@BudgetSubHeadingMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intBudgetSubHeadingMID;

                strStoredProcName = "usp_tbl_BudgetSubHeading_M_Delete";

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

        #region Select BudgetSubHeading Details by BudgetSubHeadingMID              
        public ApplicationResult BudgetSubHeading_SelectById(int BudgetSubHeadingMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@BudgetSubHeadingMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = BudgetSubHeadingMID;

                strStoredProcName = "usp_tbl_BudgetSubHeading_M_SelectById";

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

        #region Update BudgetSubHeading Details    
        public ApplicationResult BudgetSubHeading_Update(BudgetSubHeadingMBO objBudgetSubHeadingMBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

                pSqlParameter[0] = new SqlParameter("@BudgetSubHeadingMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objBudgetSubHeadingMBO.BudgetSubHeadingMID;

                pSqlParameter[1] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objBudgetSubHeadingMBO.BudgetHeadingMID;

                pSqlParameter[2] = new SqlParameter("@BudgetCategoryMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objBudgetSubHeadingMBO.BudgetCategoryMID;

                pSqlParameter[3] = new SqlParameter("@SubHeadingName", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objBudgetSubHeadingMBO.SubHeadingName;

                pSqlParameter[4] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objBudgetSubHeadingMBO.ModifiedBy;

                pSqlParameter[5] = new SqlParameter("@ModifiedDate", SqlDbType.Date);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objBudgetSubHeadingMBO.ModifiedDate;

                sSql = "usp_tbl_BudgetSubHeading_M_Update";
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
                objBudgetSubHeadingMBO = null;
            }
        }
        #endregion

        #region ValidateName for SubHeading
        public ApplicationResult SubHeadingM_ValidateName(int intBudgetCategoryMID, int intBudgetHeadingMID, int intBudgetSubHeadingMID, string strSubHeadingName)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@BudgetCategoryMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intBudgetCategoryMID;

                pSqlParameter[1] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intBudgetHeadingMID;

                pSqlParameter[2] = new SqlParameter("@BudgetSubHeadingMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intBudgetSubHeadingMID;

                pSqlParameter[3] = new SqlParameter("@SubHeadingName", SqlDbType.NVarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strSubHeadingName;

                strStoredProcName = "usp_tbl_BudgetSubHeading_M_ValidateName";

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

        #region Select BudgetSubHeading Cascade by BudgetSubHeadingMID
        /// <summary>
        /// Select all details of GeneralLedger for selected LedgerID from tbl_GeneralLedger_M table
        /// Created By : Viral, 10/7/2014
        /// Modified By :
        public ApplicationResult BudgetSubHeading_M_Cascade(int intBudgetSubHeadingMID, int intTrustMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@BudgetSubHeadingMID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = intBudgetSubHeadingMID
                };
                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = intTrustMID
                };              
                strStoredProcName = "usp_tbl_BudgetSubHeading_M_Cascade";

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

        //#region Budget SubHeading for DropDown in [gvFees] GridView     
        //public ApplicationResult BudgetSubHeading_Grid()
        //{
        //    try
        //    {
        //        sSql = "usp_tbl_BudgetSubHeading_Grid";
        //        DataTable dtDepartment = new DataTable();
        //        dtDepartment = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

        //        ApplicationResult objResults = new ApplicationResult(dtDepartment);
        //        objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
        //        return objResults;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //#endregion
    }
}
