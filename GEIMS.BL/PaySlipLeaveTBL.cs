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
    /// Class Created By : GEIMS, 17-04-2015
    /// Summary description for Organisation.
    /// </summary>
    public class PaySlipLeaveTBl
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion


        #region Select All PaySlipLeaveT Details
        /// <summary>
        /// To Select All data from the tbl_PayslipLeave_T table
        /// Created By : GEIMS, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult PaySlipLeaveT_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_PayslipLeave_T_SelectAll";
                DataTable dtPaySlipLeaveT = new DataTable();
                dtPaySlipLeaveT = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtPaySlipLeaveT);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select PaySlipLeaveT Details by PayslipLeaveID
        /// <summary>
        /// Select all details of PaySlipLeaveT for selected PayslipLeaveID from tbl_PayslipLeave_T table
        /// Created By : GEIMS, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intPayslipLeaveID"></param>
        /// <returns></returns>
        public ApplicationResult PaySlipLeaveT_Select(int intPayslipLeaveID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@PayslipLeaveID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intPayslipLeaveID;

                strStoredProcName = "usp_tbl_PayslipLeave_T_Select";

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

        #region Delete PaySlipLeaveT Details by PayslipLeaveID
        /// <summary>
        /// To Delete details of PaySlipLeaveT for selected PayslipLeaveID from tbl_PayslipLeave_T table
        /// Created By : GEIMS, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intPayslipLeaveID"></param>
        /// <returns></returns>
        public ApplicationResult PaySlipLeaveT_Delete(int intPayslipLeaveID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@PayslipLeaveID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intPayslipLeaveID;

                strStoredProcName = "usp_tbl_PayslipLeave_T_Delete";

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

        #region Insert PaySlipLeaveT Details
        /// <summary>
        /// To Insert details of PaySlipLeaveT in tbl_PayslipLeave_T table
        /// Created By : GEIMS, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="objPaySlipLeaveTBO"></param>
        /// <returns></returns>
        public ApplicationResult PaySlipLeaveT_Insert(PaySlipLeaveTBo objPaySlipLeaveTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[10];


                pSqlParameter[0] = new SqlParameter("@PaySlipID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objPaySlipLeaveTBO.PaySlipID;

                pSqlParameter[1] = new SqlParameter("@LeaveID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objPaySlipLeaveTBO.LeaveID;

                pSqlParameter[2] = new SqlParameter("@LeaveOpening", SqlDbType.Float);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objPaySlipLeaveTBO.LeaveOpening;

                pSqlParameter[3] = new SqlParameter("@LeaveAvailed", SqlDbType.Float);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objPaySlipLeaveTBO.LeaveAvailed;

                pSqlParameter[4] = new SqlParameter("@LeaveBalance", SqlDbType.Float);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objPaySlipLeaveTBO.LeaveBalance;

                pSqlParameter[5] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objPaySlipLeaveTBO.IsDeleted;

                pSqlParameter[6] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objPaySlipLeaveTBO.CreatedUserID;

                pSqlParameter[7] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objPaySlipLeaveTBO.CreatedDate;

                pSqlParameter[8] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objPaySlipLeaveTBO.LastModifiedUserID;

                pSqlParameter[9] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objPaySlipLeaveTBO.LastModifiedDate;


                sSql = "usp_tbl_PayslipLeave_T_Insert";
                int iResult = DatabaseTransaction.ExecuteNonQuery(CommandType.StoredProcedure, sSql, pSqlParameter);

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
                objPaySlipLeaveTBO = null;
            }
        }
        #endregion

        #region Update PaySlipLeaveT Details
        /// <summary>
        /// To Update details of PaySlipLeaveT in tbl_PayslipLeave_T table
        /// Created By : GEIMS, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="objPaySlipLeaveTBO"></param>
        /// <returns></returns>
        public ApplicationResult PaySlipLeaveT_Update(PaySlipLeaveTBo objPaySlipLeaveTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[11];


                pSqlParameter[0] = new SqlParameter("@PayslipLeaveID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objPaySlipLeaveTBO.PayslipLeaveID;

                pSqlParameter[1] = new SqlParameter("@PaySlipID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objPaySlipLeaveTBO.PaySlipID;

                pSqlParameter[2] = new SqlParameter("@LeaveID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objPaySlipLeaveTBO.LeaveID;

                pSqlParameter[3] = new SqlParameter("@LeaveOpening", SqlDbType.Float);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objPaySlipLeaveTBO.LeaveOpening;

                pSqlParameter[4] = new SqlParameter("@LeaveAvailed", SqlDbType.Float);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objPaySlipLeaveTBO.LeaveAvailed;

                pSqlParameter[5] = new SqlParameter("@LeaveBalance", SqlDbType.Float);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objPaySlipLeaveTBO.LeaveBalance;

                pSqlParameter[6] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objPaySlipLeaveTBO.IsDeleted;

                pSqlParameter[7] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objPaySlipLeaveTBO.CreatedUserID;

                pSqlParameter[8] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objPaySlipLeaveTBO.CreatedDate;

                pSqlParameter[9] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objPaySlipLeaveTBO.LastModifiedUserID;

                pSqlParameter[10] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objPaySlipLeaveTBO.LastModifiedDate;


                sSql = "usp_tbl_PayslipLeave_T_Update";
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
                objPaySlipLeaveTBO = null;
            }
        }
        #endregion




        #region Select PaySlipLeaveT Details by PaySlipLeaveTName
        /// <summary>
        /// Select all details of PaySlipLeaveT for selected PaySlipLeaveTName from tbl_PayslipLeave_T table
        /// Created By : GEIMS, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="PaySlipLeaveTName"></param>
        /// <returns></returns>
        public ApplicationResult PaySlipLeaveT_Select_byPaySlipLeaveTName(string strPaySlipLeaveTName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@PaySlipLeaveTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strPaySlipLeaveTName;

                strStoredProcName = "usp_tbl_PayslipLeave_T_Select_ByPaySlipLeaveT";

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


        #region ValidateName for PaySlipLeaveT
        /// <summary>
        /// Function which validates whether the PaySlipLeaveTName already exits in tbl_PayslipLeave_T table.
        /// Created By : GEIMS, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="strPaySlipLeaveTName"></param>
        /// <returns></returns>
        public ApplicationResult PaySlipLeaveT_ValidateName(string strPaySlipLeaveTName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@PaySlipLeaveTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strPaySlipLeaveTName;

                strStoredProcName = "usp_tbl_PayslipLeave_T_Validate_PaySlipLeaveTName";

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


