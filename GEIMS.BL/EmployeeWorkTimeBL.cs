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
    public class EmployeeWorkTimeBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        #region Get Employee Details by Day Wise

        public ApplicationResult Select_Employee_ForWorkTimeDayWise(int intDaysofweek)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@DayofWeek", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intDaysofweek;

                strStoredProcName = "usp_tbl_EmployeeWorkTime_M_Select_DayWise";

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


        #region Insert EmployeeWorkTime Details
        /// <summary>
        /// To Insert details of Employeeattendance in tbl_EmployeeAttendance_M table
        /// Created By : Nafisa, 01-06-2015
        /// Modified By :
        /// </summary>
        /// <param name="objEmployeeWorkTimeBO"></param>
        /// <returns></returns>
        public ApplicationResult EmployeeWorkTime_Insert(EmployeeWorkTimeBO objEmployeeWorkTimeBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[15];


                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objEmployeeWorkTimeBO.TrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objEmployeeWorkTimeBO.SchoolMID;

                pSqlParameter[2] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objEmployeeWorkTimeBO.EmployeeMID;

                pSqlParameter[3] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objEmployeeWorkTimeBO.DepartmentID;

                pSqlParameter[4] = new SqlParameter("@Daysofweek", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objEmployeeWorkTimeBO.Daysofweek;

                pSqlParameter[5] = new SqlParameter("@StartTime", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objEmployeeWorkTimeBO.StartTime;

                pSqlParameter[6] = new SqlParameter("@EndTime", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objEmployeeWorkTimeBO.EndTime;

                pSqlParameter[7] = new SqlParameter("@TotalTime", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objEmployeeWorkTimeBO.TotalTime;

                pSqlParameter[8] = new SqlParameter("@RecessStartTime", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objEmployeeWorkTimeBO.RecessStartTime;

                pSqlParameter[9] = new SqlParameter("@RecessEndTime", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objEmployeeWorkTimeBO.RecessEndTime;

                pSqlParameter[10] = new SqlParameter("@FirstHalfTime", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objEmployeeWorkTimeBO.FirstHalfTime;

                pSqlParameter[11] = new SqlParameter("@SecondHalfTime", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objEmployeeWorkTimeBO.SecondHalfTime;

                pSqlParameter[12] = new SqlParameter("@ShiftName", SqlDbType.Int);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objEmployeeWorkTimeBO.ShiftName;

                //pSqlParameter[9] = new SqlParameter("@LastdModifiedDate", SqlDbType.VarChar);
                //pSqlParameter[9].Direction = ParameterDirection.Input;
                //pSqlParameter[9].Value = objEmployeeWorkTimeBO.CreatedModifiedDate;

                pSqlParameter[13] = new SqlParameter("@CreateModifiedUserID", SqlDbType.Int);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objEmployeeWorkTimeBO.CreateModifiedUserID;

                pSqlParameter[14] = new SqlParameter("@CreatedModifiedDate", SqlDbType.VarChar);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objEmployeeWorkTimeBO.CreatedModifiedDate;





                sSql = "usp_tbl_EmployeeWorkTime_Insert";
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
                objEmployeeWorkTimeBO = null;
            }
        }
        #endregion

        #region Update EmployeeWork Details
        /// <summary>
        /// To Update details of Employeeattendance in tbl_EmployeeAttendance_M table
        /// Created By : Nafisa, 01-06-2015
        /// Modified By :
        /// </summary>
        /// <param name="objEmployeeWorkTimeBO"></param>
        /// <returns></returns>
        public ApplicationResult EmployeeWorkTime_Update(EmployeeWorkTimeBO objEmployeeWorkTimeBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[15];


                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objEmployeeWorkTimeBO.EmployeeMID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objEmployeeWorkTimeBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objEmployeeWorkTimeBO.SchoolMID;

                pSqlParameter[3] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objEmployeeWorkTimeBO.DepartmentID;

                pSqlParameter[4] = new SqlParameter("@Daysofweek", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objEmployeeWorkTimeBO.Daysofweek;

                pSqlParameter[5] = new SqlParameter("@StartTime", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objEmployeeWorkTimeBO.StartTime;

                pSqlParameter[6] = new SqlParameter("@EndTime", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objEmployeeWorkTimeBO.EndTime;

                pSqlParameter[7] = new SqlParameter("@TotalTime", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objEmployeeWorkTimeBO.TotalTime;

                pSqlParameter[8] = new SqlParameter("@RecessStartTime", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objEmployeeWorkTimeBO.RecessStartTime;

                pSqlParameter[9] = new SqlParameter("@RecessEndTime", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objEmployeeWorkTimeBO.RecessEndTime;

                pSqlParameter[10] = new SqlParameter("@FirstHalfTime", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objEmployeeWorkTimeBO.FirstHalfTime;

                pSqlParameter[11] = new SqlParameter("@SecondHalfTime", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objEmployeeWorkTimeBO.SecondHalfTime;

                pSqlParameter[12] = new SqlParameter("@ShiftName", SqlDbType.VarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objEmployeeWorkTimeBO.ShiftName;

                pSqlParameter[13] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objEmployeeWorkTimeBO.LastModifiedUserID;

                pSqlParameter[14] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objEmployeeWorkTimeBO.LastModifiedDate;

         

         




                sSql = "usp_tbl_EmployeeWorkTime_Update";
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
                objEmployeeWorkTimeBO = null;
            }
        }
        #endregion




    }
}
