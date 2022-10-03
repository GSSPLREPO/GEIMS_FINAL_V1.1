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
    /// Class Created By : Nafisal, 16-04-2015
    /// Summary description for Organisation.
    /// </summary>
    public class LeaveMBl
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion


        #region Select All LeaveM Details
        /// <summary>
        /// To Select All data from the tbl_Leave_M table
        /// Created By : Nafisal, 16-04-2015
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult LeaveM_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_Leave_M_SelectAll";
                DataTable dtLeaveM = new DataTable();
                dtLeaveM = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtLeaveM);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select LeaveM Details by LeaveID
        /// <summary>
        /// Select all details of LeaveM for selected LeaveID from tbl_Leave_M table
        /// Created By : Nafisal, 16-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intLeaveID"></param>
        /// <returns></returns>
        public ApplicationResult LeaveM_Select(int intLeaveID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@LeaveID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intLeaveID;

                strStoredProcName = "usp_tbl_Leave_M_Select";

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

        #region Delete LeaveM Details by LeaveID
        /// <summary>
        /// To Delete details of LeaveM for selected LeaveID from tbl_Leave_M table
        /// Created By : Nafisal, 16-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intLeaveID"></param>
        /// <returns></returns>
        public ApplicationResult LeaveM_Delete(int intLeaveID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@LeaveID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intLeaveID;

                strStoredProcName = "usp_tbl_Leave_M_Delete";

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

        #region Insert LeaveM Details
        /// <summary>
        /// To Insert details of LeaveM in tbl_Leave_M table
        /// Created By : Nafisal, 16-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="objLeaveMBO"></param>
        /// <returns></returns>
        public ApplicationResult LeaveM_Insert(LeaveMBo objLeaveMBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[12];


                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objLeaveMBO.TrustMID;

                pSqlParameter[1] = new SqlParameter("@LeaveCode", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objLeaveMBO.LeaveCode;

                pSqlParameter[2] = new SqlParameter("@LeaveName", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objLeaveMBO.LeaveName;

                pSqlParameter[3] = new SqlParameter("@LeaveDescription", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objLeaveMBO.LeaveDescription;

                pSqlParameter[4] = new SqlParameter("@LeaveOpening", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objLeaveMBO.LeaveOpening;

                pSqlParameter[5] = new SqlParameter("@LeaveCarryForwardLimit", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objLeaveMBO.LeaveCarryForwardLimit;

                pSqlParameter[6] = new SqlParameter("@Year", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objLeaveMBO.Year;

                pSqlParameter[7] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objLeaveMBO.IsDeleted;

                pSqlParameter[8] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objLeaveMBO.CreatedUserID;

                pSqlParameter[9] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objLeaveMBO.CreatedDate;

                pSqlParameter[10] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objLeaveMBO.LastModifiedUserID;

                pSqlParameter[11] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objLeaveMBO.LastModifiedDate;


                sSql = "usp_tbl_Leave_M_Insert";
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
                objLeaveMBO = null;
            }
        }
        #endregion

        #region Update LeaveM Details
        /// <summary>
        /// To Update details of LeaveM in tbl_Leave_M table
        /// Created By : Nafisal, 16-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="objLeaveMBO"></param>
        /// <returns></returns>
        public ApplicationResult LeaveM_Update(LeaveMBo objLeaveMBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[10];


                pSqlParameter[0] = new SqlParameter("@LeaveID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objLeaveMBO.LeaveID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objLeaveMBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@LeaveCode", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objLeaveMBO.LeaveCode;

                pSqlParameter[3] = new SqlParameter("@LeaveName", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objLeaveMBO.LeaveName;

                pSqlParameter[4] = new SqlParameter("@LeaveDescription", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objLeaveMBO.LeaveDescription;

                pSqlParameter[5] = new SqlParameter("@LeaveOpening", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objLeaveMBO.LeaveOpening;

                pSqlParameter[6] = new SqlParameter("@LeaveCarryForwardLimit", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objLeaveMBO.LeaveCarryForwardLimit;

                pSqlParameter[7] = new SqlParameter("@Year", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objLeaveMBO.Year;

               
                pSqlParameter[8] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objLeaveMBO.LastModifiedUserID;

                pSqlParameter[9] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objLeaveMBO.LastModifiedDate;


                sSql = "usp_tbl_Leave_M_Update";
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
                objLeaveMBO = null;
            }
        }
        #endregion




        #region Select LeaveM Details by LeaveMName
        /// <summary>
        /// Select all details of LeaveM for selected LeaveMName from tbl_Leave_M table
        /// Created By : Nafisal, 16-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="LeaveMName"></param>
        /// <returns></returns>
        public ApplicationResult LeaveM_Select_byLeaveMName(string strLeaveMName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@LeaveMName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strLeaveMName;

                strStoredProcName = "usp_tbl_Leave_M_Select_ByLeaveM";

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


        #region ValidateName for LeaveM
        /// <summary>
        /// Function which validates whether the LeaveMName already exits in tbl_Leave_M table.
        /// Created By : Nafisal, 16-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="strLeaveMName"></param>
        /// <returns></returns>
        public ApplicationResult LeaveM_ValidateName(string strLeaveMName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@LeaveMName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strLeaveMName;

                strStoredProcName = "usp_tbl_Leave_M_Validate_LeaveMName";

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

        #region Select LeaveM For LeaveID
        /// <summary>
        /// Select all details of LeaveM for selected LeaveID from tbl_Leave_M table
        /// Created By : Nafisal, 16-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intLeaveID"></param>
        /// <returns></returns>
        public ApplicationResult LeaveRequestM_SelectLeaveID(int intPaySlipID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@PaySlipID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intPaySlipID;

                strStoredProcName = "usp_tbl_LeaveRequest_M_Select_LeaveID";

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

        #region Select LeaveRequestM by Employee
        /// <summary>
        /// Select all details of LeaveM for selected LeaveID from tbl_Leave_M table
        /// Created By : Nafisal, 16-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intLeaveID"></param>
        /// <returns></returns>
        public ApplicationResult PayRollLeaveRequestM_SelectbyEmployeeMID(int intEmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                strStoredProcName = "usp_tbl_PayRoll_LeaveRequest_M_SelectbyEmployeeMID";

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

        #region Select LeaveGenerate Select by EmployeeMID 
        /// <summary>
        /// Select all details of LeaveM for selected LeaveID from tbl_Leave_M table
        /// Created By : Nafisal, 16-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intLeaveID"></param>
        /// <returns></returns>
        public ApplicationResult LeaveGenerate_SelectbyEmployeeMID(int intEmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                strStoredProcName = "usp_tbl_LeaveGenerate_SelectByEmployeeID";

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

        #region Select LeaveGenerate Select 
        /// <summary>
        /// Select all details of LeaveM for selected LeaveID from tbl_Leave_M table
        /// Created By : Nafisal, 16-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intLeaveID"></param>
        /// <returns></returns>
        public ApplicationResult LeaveGenerate_Select(int intEmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                strStoredProcName = "usp_tbl_LeaveGenerate_Select";

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

        #region UserLeave Select Final
        /// <summary>
        /// Select all details of LeaveM for selected LeaveID from tbl_Leave_M table
        /// Created By : Nafisal, 16-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intLeaveID"></param>
        /// <returns></returns>
        public ApplicationResult UserLeave_M_Select_Final(int intMonth, int intYear, int intEmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@Month", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMonth;

                pSqlParameter[1] = new SqlParameter("@Year", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intYear;

                pSqlParameter[2] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intEmployeeMID;

                strStoredProcName = "usp_tbl_UserLeave_M_SelectFinal";

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

        #region UserLeave Select By TrustMID
        /// <summary>
        /// Select all details of LeaveM for selected LeaveID from tbl_Leave_M table
        /// Created By : Nafisal, 16-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intLeaveID"></param>
        /// <returns></returns>
        public ApplicationResult UserLeaveM_Select_ByTrustMID(int intTrustMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                strStoredProcName = "usp_tbl_UserLeave_M_SelectByTrustMID";

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


