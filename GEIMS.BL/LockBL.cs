using System;
using System.Data;
using System.Data.SqlClient;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
    public class LockBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        #region Select Lock by Year
        /// <summary>
        /// Select all details of Student for selected StudentMID from tbl_Student_M table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="intStudentMID"></param>
        /// <returns></returns>
        public ApplicationResult Lock_SelectbyYear(int intLockYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@LockYear", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intLockYear;

                strStoredProcName = "tbl_Lock_M_SelectByYear";

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

        #region Insert Lock Details
        /// <summary>
        /// To Insert details of Student in tbl_Student_M table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="objStudentBO"></param>
        /// <returns></returns>
        public ApplicationResult Lock_Insert(LockBo objLockBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objLockBo.TrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objLockBo.SchoolMID;

                pSqlParameter[2] = new SqlParameter("@FromDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objLockBo.FromDate;

                pSqlParameter[3] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objLockBo.ToDate;

                pSqlParameter[4] = new SqlParameter("@LockYear", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objLockBo.LockYear;

                sSql = "tbl_Lock_M_Insert";

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
                objLockBo = null;
            }
        }
        #endregion
    }
}
