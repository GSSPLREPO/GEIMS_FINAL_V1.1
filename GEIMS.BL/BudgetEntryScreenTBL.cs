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
    public class BudgetEntryScreenTBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        #region Insert BudgetSubHeading Details
        public ApplicationResult BudgetEntryScreenT_Insert(BudgetEntryScreenTBO objBudgetEntryScreenTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];

                pSqlParameter[0] = new SqlParameter("@BudgetScreenTId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objBudgetEntryScreenTBO.BudgetScreenTId;

                pSqlParameter[1] = new SqlParameter("@BudgetScreenId", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objBudgetEntryScreenTBO.BudgetScreenId;              

                pSqlParameter[2] = new SqlParameter("@SectionMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objBudgetEntryScreenTBO.SectionMID;

                pSqlParameter[3] = new SqlParameter("@BudgetSectionAmount", SqlDbType.Decimal);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objBudgetEntryScreenTBO.BudgetSectionAmount;

                pSqlParameter[4] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objBudgetEntryScreenTBO.IsDeleted;



                sSql = "usp_tbl_BudgetEntryScreen_T_Insert";
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
                objBudgetEntryScreenTBO = null;
            }
        }
        #endregion

        #region Find Section Wise Amount    
        public ApplicationResult BudgetEntry_FindId(int TrustMID, int BudgetSchoolMID, int BudgetCategoryMID, int BudgetHeadingMID, int BudgetSubHeadingMID, int SectionMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = TrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = BudgetSchoolMID;

                pSqlParameter[2] = new SqlParameter("@BudgetCategoryMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = BudgetCategoryMID;

                pSqlParameter[3] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = BudgetHeadingMID;
                              
                pSqlParameter[4] = new SqlParameter("@BudgetSubHeadingMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = BudgetSubHeadingMID;

                pSqlParameter[5] = new SqlParameter("@SectionMID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = SectionMID;

                sSql = "usp_tbl_BudgetEntryScreen_Amount";

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

        #region Update BudgetEntry To Amount   
        public ApplicationResult BudgetEntryScreenToT_Update(BudgetEntryScreenTBO objBudgetEntryScreenTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@BudgetScreenTId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objBudgetEntryScreenTBO.BudgetScreenTId;

                pSqlParameter[1] = new SqlParameter("@BudgetSectionAmount", SqlDbType.Decimal);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objBudgetEntryScreenTBO.BudgetSectionAmount;
              
                sSql = "usp_tbl_BudgetEntryScreenTo_T_Update";
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
                objBudgetEntryScreenTBO = null;
            }
        }
        #endregion

        #region Update BudgetEntry From Amount   
        public ApplicationResult BudgetEntryScreenFromT_Update(BudgetEntryScreenTBO objBudgetEntryScreenTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@BudgetScreenTId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objBudgetEntryScreenTBO.BudgetScreenTId;

                pSqlParameter[1] = new SqlParameter("@BudgetSectionAmount", SqlDbType.Decimal);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objBudgetEntryScreenTBO.BudgetSectionAmount;

                sSql = "usp_tbl_BudgetEntryScreenFrom_T_Update";
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
                objBudgetEntryScreenTBO = null;
            }
        }
        #endregion

        #region Select BudgetScreen Details by BudgetScreenId              
        public ApplicationResult BudgetScreen_T_SelectById(int BudgetScreenId)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@BudgetScreenId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = BudgetScreenId;

                strStoredProcName = "usp_tbl_BudgetScreen_T_SelectById";

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

        #region Select BudgetScreen Details by BudgetScreenId and SectionMID             
        public ApplicationResult BudgetScreen_T_Fetch(int BudgetScreenId, int SectionMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@BudgetScreenId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = BudgetScreenId;

                pSqlParameter[1] = new SqlParameter("@SectionMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = SectionMID;

                strStoredProcName = "usp_tbl_BudgetScreen_T_Fetch";

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

        #region Update 
        public ApplicationResult BudgetEntryScreenT_Update(BudgetEntryScreenTBO objBudgetEntryScreenTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@BudgetScreenId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objBudgetEntryScreenTBO.BudgetScreenId;

                pSqlParameter[1] = new SqlParameter("@SectionMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objBudgetEntryScreenTBO.SectionMID;

                pSqlParameter[2] = new SqlParameter("@BudgetSectionAmount", SqlDbType.Decimal);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objBudgetEntryScreenTBO.BudgetSectionAmount;

                sSql = "usp_tbl_BudgetEntryScreenT_Update";
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
                objBudgetEntryScreenTBO = null;
            }
        }
        #endregion
    }
}
