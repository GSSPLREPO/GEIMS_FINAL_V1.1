using System;
using System.Data;
using System.Data.SqlClient;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
    public class BankRecoDetailsBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        #region bank book report
        public ApplicationResult BankReconciliaiton_Select_BankBook(int intTrustMId, int intSchookMId, int intLedgerId, string strTransactionType, string strFromDate, string strToDate, int intIsNarration)
        {
            try
            {
                pSqlParameter = new SqlParameter[7];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMId;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchookMId;

                pSqlParameter[2] = new SqlParameter("@LedgerID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intLedgerId;

                pSqlParameter[3] = new SqlParameter("@TransactionType", SqlDbType.NVarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strTransactionType;

                pSqlParameter[4] = new SqlParameter("@FromDate", SqlDbType.NVarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = strFromDate;

                pSqlParameter[5] = new SqlParameter("@ToDate", SqlDbType.NVarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = strToDate;

                pSqlParameter[6] = new SqlParameter("@IsNarration", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = intIsNarration;

                strStoredProcName = "usp_BankReconciliation_Details";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                //DataSet dsResult = new DataSet();
                //dsResult = Database.ExecuteDataSet(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
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
