using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;
using System.Data;
using System.Data.SqlClient;

namespace GEIMS.BL
{
    public class rptBalanceSheetBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        #region Select AccountGroup Details by GroupNature
        /// <summary>
        /// Select all details of Department for selected DepartmentID from tbl_Department_M table
        /// Created By : Darshan, 09/13/2014
        /// Modified By :
        /// </summary>
        /// <param name="intDepartmentID"></param>
        /// <returns></returns>
        public ApplicationResult AccountGroup_Select_ByGroupNature(int intGroup)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@Group", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intGroup;

                strStoredProcName = "usp_tbl_AccountGroup_Select_IncomeGroup";

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

        #region Asset Group
        /// <summary>
        /// Select all details of Department for selected DepartmentID from tbl_Department_M table
        /// Created By : Darshan, 09/13/2014
        /// Modified By :
        /// </summary>
        /// <param name="intDepartmentID"></param>
        /// <returns></returns>
        public ApplicationResult ReportBalanceSheet_AssetGroup(string strToDate, int intTrustMID, int intSchoolMID, int intAccountGroupID)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strToDate;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                pSqlParameter[3] = new SqlParameter("@AccountGroupID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intAccountGroupID;

                strStoredProcName = "usp_rpt_BalSheet_Asset";
                //strStoredProcName = "usp_rpt_BalSheet_Asset_1";

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

        #region Liability Group
        /// <summary>
        /// Select all details of Department for selected DepartmentID from tbl_Department_M table
        /// Created By : Darshan, 09/13/2014
        /// Modified By :
        /// </summary>
        /// <param name="intDepartmentID"></param>
        /// <returns></returns>
        public ApplicationResult ReportBalanceSheet_LiabilityGroup(string strToDate, int intTrustMID, int intSchoolMID, int intAccountGroupID)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strToDate;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                pSqlParameter[3] = new SqlParameter("@AccountGroupID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intAccountGroupID;

                strStoredProcName = "usp_rpt_BalSheet_Liability";
                //strStoredProcName = "usp_rpt_BalSheet_Liability_1";

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

        #region Net Result
        /// <summary>
        /// Select all details of Department for selected DepartmentID from tbl_Department_M table
        /// Created By : Darshan, 09/13/2014
        /// Modified By :
        /// </summary>
        /// <param name="intDepartmentID"></param>
        /// <returns></returns>
        public ApplicationResult ReportBalanceSheet_Result(string strToDate, int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strToDate;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                //strStoredProcName = "usp_rpt_BalanceSheetResult";
                strStoredProcName = "usp_rpt_BalanceSheetResult_1";

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

        #region Basic Info
        /// <summary>
        /// Select all details of Department for selected DepartmentID from tbl_Department_M table
        /// Created By : Darshan, 09/13/2014
        /// Modified By :
        /// </summary>
        /// <param name="intDepartmentID"></param>
        /// <returns></returns>
        public ApplicationResult ReportPnL_BasicInfo(int intTrustMID, int intSchoolMID, string strToDate, string strReport)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                pSqlParameter[2] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strToDate;

                pSqlParameter[3] = new SqlParameter("@Report", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strReport;

                strStoredProcName = "usp_rpt_BasicInfo";

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
