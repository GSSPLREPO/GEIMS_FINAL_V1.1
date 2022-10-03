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
    /// Class Created By : Nafisa, 17-04-2015
    /// Summary description for Organisation.
    /// </summary>
    public class PaySlipUserPayItemTBl
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion


        #region Select All PaySlipUserPayItemT Details
        /// <summary>
        /// To Select All data from the tbl_PaySlipUserPayItem_T table
        /// Created By : Nafisa, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult PaySlipUserPayItemT_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_PaySlipUserPayItem_T_SelectAll";
                DataTable dtPaySlipUserPayItemT = new DataTable();
                dtPaySlipUserPayItemT = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtPaySlipUserPayItemT);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select PaySlipUserPayItemT Details by PaySlipUserPayItemID
        /// <summary>
        /// Select all details of PaySlipUserPayItemT for selected PaySlipUserPayItemID from tbl_PaySlipUserPayItem_T table
        /// Created By : Nafisa, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intPaySlipUserPayItemID"></param>
        /// <returns></returns>
        public ApplicationResult PaySlipUserPayItemT_Select(int intPaySlipUserPayItemID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@PaySlipUserPayItemID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intPaySlipUserPayItemID;

                strStoredProcName = "usp_tbl_PaySlipUserPayItem_T_Select";

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

        #region Delete PaySlipUserPayItemT Details by PaySlipUserPayItemID
        /// <summary>
        /// To Delete details of PaySlipUserPayItemT for selected PaySlipUserPayItemID from tbl_PaySlipUserPayItem_T table
        /// Created By : Nafisa, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intPaySlipUserPayItemID"></param>
        /// <returns></returns>
        public ApplicationResult PaySlipUserPayItemT_Delete(int intPaySlipUserPayItemID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@PaySlipUserPayItemID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intPaySlipUserPayItemID;

                strStoredProcName = "usp_tbl_PaySlipUserPayItem_T_Delete";

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

        #region Insert PaySlipUserPayItemT Details
        /// <summary>
        /// To Insert details of PaySlipUserPayItemT in tbl_PaySlipUserPayItem_T table
        /// Created By : Nafisa, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="objPaySlipUserPayItemTBO"></param>
        /// <returns></returns>
        public ApplicationResult PaySlipUserPayItemT_Insert(PaySlipUserPayItemTBo objPaySlipUserPayItemTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[8];


                pSqlParameter[0] = new SqlParameter("@PaySlipID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objPaySlipUserPayItemTBO.PaySlipID;

                pSqlParameter[1] = new SqlParameter("@UserPayItemID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objPaySlipUserPayItemTBO.UserPayItemID;

                pSqlParameter[2] = new SqlParameter("@PaySlipPayItemAmount", SqlDbType.Float);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objPaySlipUserPayItemTBO.PaySlipPayItemAmount;

                pSqlParameter[3] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objPaySlipUserPayItemTBO.IsDeleted;

                pSqlParameter[4] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objPaySlipUserPayItemTBO.CreatedUserID;

                pSqlParameter[5] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objPaySlipUserPayItemTBO.CreatedDate;

                pSqlParameter[6] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objPaySlipUserPayItemTBO.LastModifiedUserID;

                pSqlParameter[7] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objPaySlipUserPayItemTBO.LastModifiedDate;


                sSql = "usp_tbl_PaySlipUserPayItem_T_Insert";
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
                objPaySlipUserPayItemTBO = null;
            }
        }
        #endregion

        #region Update PaySlipUserPayItemT Details
        /// <summary>
        /// To Update details of PaySlipUserPayItemT in tbl_PaySlipUserPayItem_T table
        /// Created By : Nafisa, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="objPaySlipUserPayItemTBO"></param>
        /// <returns></returns>
        public ApplicationResult PaySlipUserPayItemT_Update(PaySlipUserPayItemTBo objPaySlipUserPayItemTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[9];


                pSqlParameter[0] = new SqlParameter("@PaySlipUserPayItemID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objPaySlipUserPayItemTBO.PaySlipUserPayItemID;

                pSqlParameter[1] = new SqlParameter("@PaySlipID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objPaySlipUserPayItemTBO.PaySlipID;

                pSqlParameter[2] = new SqlParameter("@UserPayItemID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objPaySlipUserPayItemTBO.UserPayItemID;

                pSqlParameter[3] = new SqlParameter("@PaySlipPayItemAmount", SqlDbType.Float);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objPaySlipUserPayItemTBO.PaySlipPayItemAmount;

                pSqlParameter[4] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objPaySlipUserPayItemTBO.IsDeleted;

                pSqlParameter[5] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objPaySlipUserPayItemTBO.CreatedUserID;

                pSqlParameter[6] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objPaySlipUserPayItemTBO.CreatedDate;

                pSqlParameter[7] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objPaySlipUserPayItemTBO.LastModifiedUserID;

                pSqlParameter[8] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objPaySlipUserPayItemTBO.LastModifiedDate;


                sSql = "usp_tbl_PaySlipUserPayItem_T_Update";
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
                objPaySlipUserPayItemTBO = null;
            }
        }
        #endregion




        #region Select PaySlipUserPayItemT Details by PaySlipUserPayItemTName
        /// <summary>
        /// Select all details of PaySlipUserPayItemT for selected PaySlipUserPayItemTName from tbl_PaySlipUserPayItem_T table
        /// Created By : Nafisa, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="PaySlipUserPayItemTName"></param>
        /// <returns></returns>
        public ApplicationResult PaySlipUserPayItemT_Select_byPaySlipUserPayItemTName(string strPaySlipUserPayItemTName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@PaySlipUserPayItemTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strPaySlipUserPayItemTName;

                strStoredProcName = "usp_tbl_PaySlipUserPayItem_T_Select_ByPaySlipUserPayItemT";

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


        #region ValidateName for PaySlipUserPayItemT
        /// <summary>
        /// Function which validates whether the PaySlipUserPayItemTName already exits in tbl_PaySlipUserPayItem_T table.
        /// Created By : Nafisa, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="strPaySlipUserPayItemTName"></param>
        /// <returns></returns>
        public ApplicationResult PaySlipUserPayItemT_ValidateName(string strPaySlipUserPayItemTName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@PaySlipUserPayItemTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strPaySlipUserPayItemTName;

                strStoredProcName = "usp_tbl_PaySlipUserPayItem_T_Validate_PaySlipUserPayItemTName";

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


