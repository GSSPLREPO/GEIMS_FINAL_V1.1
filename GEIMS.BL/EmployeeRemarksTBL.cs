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
    public class EmployeeRemarksTBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        #region Insert Data
        public ApplicationResult EmployeeRemarksT_Insert(EmployeeRemarksTBO objEmployeeRemakrsTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];


                pSqlParameter[0] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objEmployeeRemakrsTBO.Remarks;

                pSqlParameter[1] = new SqlParameter("@RemarkDate", SqlDbType.Date);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objEmployeeRemakrsTBO.RemarksDate;

                pSqlParameter[2] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objEmployeeRemakrsTBO.EmployeeMID;
               

                sSql = "usp_tbl_EmployeeRemakrs_T_Insert";
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
                objEmployeeRemakrsTBO = null;
            }
        }
        #endregion

        #region Employee Remaks Select All
        public ApplicationResult EmployeeRemarksT_SelectALL(int intEmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                sSql = "usp_tbl_EmployeeRemarks_T_SelectAll";
                DataTable dtEmpoyeeQualificationT = new DataTable();
                dtEmpoyeeQualificationT = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtEmpoyeeQualificationT);
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
