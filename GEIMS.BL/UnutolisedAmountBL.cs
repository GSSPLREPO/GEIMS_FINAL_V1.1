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
    public class UnutolisedAmountBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        #region Select Unutilised_Amount LedgerID wise    
        public ApplicationResult Unutilised_Amount_Validation(int intLedgerID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@LedgerID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intLedgerID;
                
                sSql = "usp_tbl_Unutilised_Amount_Validation";
                DataTable dtJournalVoucherM = new DataTable();
                dtJournalVoucherM = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtJournalVoucherM);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select All Unutilised_Amount Budget Wise Journal    
        public ApplicationResult Unutilised_Amount_2(int intTrustMID, int intSchoolMID, int intSectionMID, int intBudgetCategoryMID, int intBudgetHeadingMID, int intBudgetSubHeadingMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                pSqlParameter[2] = new SqlParameter("@SectionMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSectionMID;

                pSqlParameter[3] = new SqlParameter("@BudgetCategoryMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intBudgetCategoryMID;

                pSqlParameter[4] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intBudgetHeadingMID;

                pSqlParameter[5] = new SqlParameter("@BudgetSubHeadingMID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = intBudgetSubHeadingMID;

                sSql = "usp_tbl_Unutilised_Amount_2";
                DataTable dtJournalVoucherM = new DataTable();
                dtJournalVoucherM = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtJournalVoucherM);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select All Unutilised_Amount Budget Wise Receipt/Payment   
        public ApplicationResult Unutilised_Amount_3(int intTrustMID, int intSchoolMID, int intSectionMID, int intBudgetCategoryMID, int intBudgetHeadingMID, int intBudgetSubHeadingMID, string strCurrentYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[7];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                pSqlParameter[2] = new SqlParameter("@SectionMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSectionMID;

                pSqlParameter[3] = new SqlParameter("@BudgetCategoryMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intBudgetCategoryMID;

                pSqlParameter[4] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intBudgetHeadingMID;

                pSqlParameter[5] = new SqlParameter("@BudgetSubHeadingMID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = intBudgetSubHeadingMID;

                pSqlParameter[6] = new SqlParameter("@CurrentYear", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = strCurrentYear;

                sSql = "usp_tbl_Unutilised_Amount_3";
                DataTable dtJournalVoucherM = new DataTable();
                dtJournalVoucherM = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtJournalVoucherM);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select All Unutilised_Budget Year Validation  
        public ApplicationResult Unutilised_Year_Validation(int intTrustMID, int intSchoolMID, int intBudgetCategoryMID, int intBudgetHeadingMID, int intBudgetSubHeadingMID, string strCurrentYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                pSqlParameter[2] = new SqlParameter("@BudgetCategoryMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intBudgetCategoryMID;

                pSqlParameter[3] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intBudgetHeadingMID;

                pSqlParameter[4] = new SqlParameter("@BudgetSubHeadingMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intBudgetSubHeadingMID;

                pSqlParameter[5] = new SqlParameter("@CurrentYear", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = strCurrentYear;

                sSql = "usp_tbl_Unutilised_Year_Validation";
                DataTable dtJournalVoucherM = new DataTable();
                dtJournalVoucherM = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtJournalVoucherM);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Update BudgetEntry From Amount   
        public ApplicationResult Unutilised_Amount_1_Update(BudgetEntryScreenTBO objBudgetEntryScreenTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@BudgetScreenTId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objBudgetEntryScreenTBO.BudgetScreenTId;

                pSqlParameter[1] = new SqlParameter("@BudgetScreenId", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objBudgetEntryScreenTBO.BudgetScreenId;

                pSqlParameter[2] = new SqlParameter("@BudgetSectionAmount", SqlDbType.Decimal);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objBudgetEntryScreenTBO.BudgetSectionAmount;

                sSql = "usp_tbl_Unutilised_Amount_1_Update";
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

        #region Find Section Wise Amount    
        public ApplicationResult BudgetEntry_FindId(int BudgetTrustMID, int BudgetSchoolMID, int BudgetCategoryMID, int BudgetHeadingMID, int BudgetSubHeadingMID, int SectionMID, int BudgetScreenID)
        {
            try
            {
                pSqlParameter = new SqlParameter[7];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = BudgetTrustMID;

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

                pSqlParameter[6] = new SqlParameter("@BudgetScreenId", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = BudgetScreenID;

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
    }
}
