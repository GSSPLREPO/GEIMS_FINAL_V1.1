using System;
using System.Data;
using System.Data.SqlClient;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
    public class StatusBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        #region Select All Status Details
        /// <summary>
        /// To Select All data from the tbl_Status_M table
        /// Created By : NafisaMulla, 25-09-2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult Status_SelectAll(int intTrustMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];


                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                sSql = "usp_tbl_Status_M_SelectAll";

                DataTable dtStatus = new DataTable();
                dtStatus = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtStatus);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select Status Details by StatusMasterID
        /// <summary>
        /// Select all details of Status for selected StatusMasterID from tbl_Status_M table
        /// Created By : NafisaMulla, 25-09-2014
        /// Modified By :
        /// </summary>
        /// <param name="intStatusMasterID"></param>
        /// <returns></returns>
        public ApplicationResult Status_Select(int intStatusMasterID, int intTrustMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@StatusMasterID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStatusMasterID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                strStoredProcName = "usp_tbl_Status_M_Select";

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

        #region Delete Status Details by StatusMasterID
        /// <summary>
        /// To Delete details of Status for selected StatusMasterID from tbl_Status_M table
        /// Created By : NafisaMulla, 25-09-2014
        /// Modified By :
        /// </summary>
        /// <param name="intStatusMasterID"></param>
        /// <returns></returns>
        public ApplicationResult Status_Delete(int intStatusMasterID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@StatusMasterID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStatusMasterID;

                strStoredProcName = "usp_tbl_Status_M_Delete";

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

        #region Validate For Delete
        public ApplicationResult Validate_Status_Delete(int intStatusID, int intTrustMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@StatusMasterID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStatusID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                strStoredProcName = "usp_tbl_Status_M_Validate_For_Delete";

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

        #region Insert Status Details
        /// <summary>
        /// To Insert details of Status in tbl_Status_M table
        /// Created By : NafisaMulla, 25-09-2014
        /// Modified By :
        /// </summary>
        /// <param name="objStatusBO"></param>
        /// <returns></returns>
        public ApplicationResult Status_Insert(StatusBO objStatusBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];


                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objStatusBO.TrustMID;

                pSqlParameter[1] = new SqlParameter("@StatusName", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objStatusBO.StatusName;

                pSqlParameter[2] = new SqlParameter("@Discription", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objStatusBO.Discription;

                pSqlParameter[3] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objStatusBO.IsDeleted;

                pSqlParameter[4] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objStatusBO.LastModifiedUserID;

                pSqlParameter[5] = new SqlParameter("@LastModificationDate", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objStatusBO.LastModificationDate;


                sSql = "usp_tbl_Status_M_Insert";
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
                objStatusBO = null;
            }
        }
        #endregion

        #region Update Status Details
        /// <summary>
        /// To Update details of Status in tbl_Status_M table
        /// Created By : NafisaMulla, 25-09-2014
        /// Modified By :
        /// </summary>
        /// <param name="objStatusBO"></param>
        /// <returns></returns>
        public ApplicationResult Status_Update(StatusBO objStatusBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[7];


                pSqlParameter[0] = new SqlParameter("@StatusMasterID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objStatusBO.StatusMasterID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objStatusBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@StatusName", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objStatusBO.StatusName;

                pSqlParameter[3] = new SqlParameter("@Discription", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objStatusBO.Discription;

                pSqlParameter[4] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objStatusBO.IsDeleted;

                pSqlParameter[5] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objStatusBO.LastModifiedUserID;

                pSqlParameter[6] = new SqlParameter("@LastModificationDate", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objStatusBO.LastModificationDate;


                sSql = "usp_tbl_Status_M_Update";
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
                objStatusBO = null;
            }
        }
        #endregion

        #region Select Status Details by StatusName
        /// <summary>
        /// Select all details of Status for selected StatusName from tbl_Status_M table
        /// Created By : NafisaMulla, 25-09-2014
        /// Modified By :
        /// </summary>
        /// <param name="StatusName"></param>
        /// <returns></returns>
        public ApplicationResult Status_Select_byStatusName(string strStatusName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@StatusName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strStatusName;

                strStoredProcName = "usp_tbl_Status_M_Select_ByStatus";

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

        #region ValidateName for Status
        /// <summary>
        /// Function which validates whether the StatusName already exits in tbl_Status_M table.
        /// Created By : NafisaMulla, 25-09-2014
        /// Modified By :
        /// </summary>
        /// <param name="strStatusName"></param>
        /// <returns></returns>
        public ApplicationResult Status_ValidateName(string strStatusName, int intStatusMID, int intTrustMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@StatusName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strStatusName;

                pSqlParameter[1] = new SqlParameter("@StatusMasterID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intStatusMID;

                pSqlParameter[2] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intTrustMID;

                strStoredProcName = "usp_tbl_Status_M_ValidationName";

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


