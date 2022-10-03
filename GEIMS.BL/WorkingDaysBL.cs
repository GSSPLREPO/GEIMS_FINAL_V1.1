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
    /// <summary>
    /// Class Created By : NafisaMulla, 25-02-2015
    /// Summary description for Organisation.
    /// </summary>
    public class WorkingDaysBl
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion


        #region Select All WorkingDays Details
        /// <summary>
        /// To Select All data from the tbl_TotalWorkDays_M table
        /// Created By : NafisaMulla, 25-02-2015
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult WorkingDays_SelectAll(string strAcademicYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strAcademicYear;

                sSql = "usp_tbl_TotalWorkDays_M_SelectAll";

                DataTable dtWorkingDays = new DataTable();
                dtWorkingDays = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtWorkingDays);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select WorkingDays Details by WorkingDayID
        /// <summary>
        /// Select all details of WorkingDays for selected WorkingDayID from tbl_TotalWorkDays_M table
        /// Created By : NafisaMulla, 25-02-2015
        /// Modified By :
        /// </summary>
        /// <param name="intWorkingDayID"></param>
        /// <returns></returns>
        public ApplicationResult WorkingDays_Select(int intWorkingDayID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@WorkingDayID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intWorkingDayID;

                strStoredProcName = "usp_tbl_TotalWorkDays_M_Select";

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

        #region Delete WorkingDays Details by WorkingDayID
        /// <summary>
        /// To Delete details of WorkingDays for selected WorkingDayID from tbl_TotalWorkDays_M table
        /// Created By : NafisaMulla, 25-02-2015
        /// Modified By :
        /// </summary>
        /// <param name="intWorkingDayID"></param>
        /// <returns></returns>
        public ApplicationResult WorkingDays_Delete(int intWorkingDayID, int intLastModifiedUserID, string strLastModifiedDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@WorkingDayID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intWorkingDayID;

                pSqlParameter[1] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastModifiedUserID;

                pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifiedDate;

                strStoredProcName = "usp_tbl_TotalWorkDays_M_Delete";

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

        #region Insert WorkingDays Details
        /// <summary>
        /// To Insert details of WorkingDays in tbl_TotalWorkDays_M table
        /// Created By : NafisaMulla, 25-02-2015
        /// Modified By :
        /// </summary>
        /// <param name="objWorkingDaysBO"></param>
        /// <returns></returns>
        public ApplicationResult WorkingDays_Insert(WorkingDaysBo objWorkingDaysBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[11];


                pSqlParameter[0] = new SqlParameter("@WorkingDayID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objWorkingDaysBO.WorkingDayID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objWorkingDaysBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objWorkingDaysBO.SchoolMID;

                pSqlParameter[3] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objWorkingDaysBO.AcademicYear;

                pSqlParameter[4] = new SqlParameter("@MonthID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objWorkingDaysBO.MonthID;

                pSqlParameter[5] = new SqlParameter("@TotalWorkingDays", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objWorkingDaysBO.TotalWorkingDays;

                pSqlParameter[6] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objWorkingDaysBO.IsDeleted;

                pSqlParameter[7] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objWorkingDaysBO.CreatedUserID;

                pSqlParameter[8] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objWorkingDaysBO.CreatedDate;

                pSqlParameter[9] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objWorkingDaysBO.LastModifiedUserID;

                pSqlParameter[10] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objWorkingDaysBO.LastModifiedDate;


                sSql = "usp_tbl_TotalWorkDays_M_Insert";
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
                objWorkingDaysBO = null;
            }
        }
        #endregion

        #region Update WorkingDays Details
        /// <summary>
        /// To Update details of WorkingDays in tbl_TotalWorkDays_M table
        /// Created By : NafisaMulla, 25-02-2015
        /// Modified By :
        /// </summary>
        /// <param name="objWorkingDaysBO"></param>
        /// <returns></returns>
        public ApplicationResult WorkingDays_Update(WorkingDaysBo objWorkingDaysBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[8];


                pSqlParameter[0] = new SqlParameter("@WorkingDayID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objWorkingDaysBO.WorkingDayID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objWorkingDaysBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objWorkingDaysBO.SchoolMID;

                pSqlParameter[3] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objWorkingDaysBO.AcademicYear;

                pSqlParameter[4] = new SqlParameter("@MonthID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objWorkingDaysBO.MonthID;

                pSqlParameter[5] = new SqlParameter("@TotalWorkingDays", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objWorkingDaysBO.TotalWorkingDays;

                pSqlParameter[6] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objWorkingDaysBO.LastModifiedUserID;

                pSqlParameter[7] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objWorkingDaysBO.LastModifiedDate;


                sSql = "usp_tbl_TotalWorkDays_M_Update";
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
                objWorkingDaysBO = null;
            }
        }
        #endregion




        #region Select WorkingDays Details by WorkingDaysName
        /// <summary>
        /// Select all details of WorkingDays for selected WorkingDaysName from tbl_TotalWorkDays_M table
        /// Created By : NafisaMulla, 25-02-2015
        /// Modified By :
        /// </summary>
        /// <param name="WorkingDaysName"></param>
        /// <returns></returns>
        public ApplicationResult WorkingDays_Select_byWorkingDaysName(string strWorkingDaysName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@WorkingDaysName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strWorkingDaysName;

                strStoredProcName = "usp_tbl_TotalWorkDays_M_Select_ByWorkingDays";

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


        #region ValidateName for WorkingDays
        /// <summary>
        /// Function which validates whether the WorkingDaysName already exits in tbl_TotalWorkDays_M table.
        /// Created By : NafisaMulla, 25-02-2015
        /// Modified By :
        /// </summary>
        /// <param name="strWorkingDaysName"></param>
        /// <returns></returns>
        public ApplicationResult WorkingDays_ValidateName(string strWorkingDaysName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@WorkingDaysName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strWorkingDaysName;

                strStoredProcName = "usp_tbl_TotalWorkDays_M_Validate_WorkingDaysName";

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

        #region Select All WorkingDays All Months Details
        /// <summary>
        /// To Select All data from the tbl_TotalWorkDays_M table
        /// Created By : NafisaMulla, 25-02-2015
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult WorkingDays_SelectAllMonths()
        {
            try
            {
                sSql = "usp_tbl_WorkingDays_M_SelectAll_Months";
                DataTable dtWorkingDays = new DataTable();
                dtWorkingDays = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtWorkingDays);
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


