using System;
using System.Data;
using System.Data.SqlClient;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
    public class BankReconciliationForFeesBL
    {
        //Using BO File : FeesCollectionBO.cs

        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        #region Select All FeesCollection Details bind DropDown
        public ApplicationResult BankReconcilition_FeesCollection()
        {
            try
            {
                sSql = "usp_tbl_BankReconciliation_FeesCollection_M";
                DataTable dtFeesCollection = new DataTable();
                dtFeesCollection = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtFeesCollection);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select All FeesCollection Details bind Gridview
        public ApplicationResult BankReconcilition_FeesCollection_SearchingWise(int SchoolMID, int ClassMID, int DivisionTID, string YearName)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = SchoolMID;

                pSqlParameter[1] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = ClassMID;

                pSqlParameter[2] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = DivisionTID;

                pSqlParameter[3] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = YearName;

                sSql = "usp_tbl_BankReconciliation_FeesCollection";


                DataTable dtFeesCollection = new DataTable();
                dtFeesCollection = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtFeesCollection);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Update Bank Reconciliation for tbl_FeesCollection_M
        public ApplicationResult BankReconciliation_Update(FeesCollectionBO objFeesCollectionBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

                pSqlParameter[0] = new SqlParameter("@FeesCollectionMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objFeesCollectionBO.FeesCollectionMID;

                pSqlParameter[1] = new SqlParameter("@DateOfDeposit", SqlDbType.Date);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objFeesCollectionBO.DateOfDeposit;

                pSqlParameter[2] = new SqlParameter("@RChequeNo", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objFeesCollectionBO.RChequeNo;

                pSqlParameter[3] = new SqlParameter("@RBankName", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objFeesCollectionBO.RBankName;

                pSqlParameter[4] = new SqlParameter("@RBranchName", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objFeesCollectionBO.RBranchName;

                pSqlParameter[5] = new SqlParameter("@DateInBankStatement", SqlDbType.Date);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objFeesCollectionBO.DateInBankStatement;

                sSql = "usp_tbl_BankReconciliation_Update";
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
                objFeesCollectionBO = null;
            }
        }
        #endregion

    }
}
