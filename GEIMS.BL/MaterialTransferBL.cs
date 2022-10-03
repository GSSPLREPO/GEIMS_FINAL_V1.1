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
    /// Class Created By : Chintan, 10/31/2014
    /// Summary description for Organisation.
    /// </summary>
    public class MaterialTransferBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion


        #region Select All MaterialTransfer Details
        /// <summary>
        /// To Select All data from the tbl_MaterialTransfer_M table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult MaterialTransfer_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_MaterialTransfer_M_SelectAll";
                DataTable dtMaterialTransfer = new DataTable();
                dtMaterialTransfer = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtMaterialTransfer);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select MaterialTransfer Details by MTransferID
        /// <summary>
        /// Select all details of MaterialTransfer for selected MTransferID from tbl_MaterialTransfer_M table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="intMTransferID"></param>
        /// <returns></returns>
        public ApplicationResult MaterialTransfer_Select(int intMTransferID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@MTransferID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMTransferID;

                strStoredProcName = "usp_tbl_MaterialTransfer_M_Select";

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

        #region Delete MaterialTransfer Details by MTransferID
        /// <summary>
        /// To Delete details of MaterialTransfer for selected MTransferID from tbl_MaterialTransfer_M table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="intMTransferID"></param>
        /// <returns></returns>
        public ApplicationResult MaterialTransfer_Delete(int intMTransferID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@MTransferID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMTransferID;

                strStoredProcName = "usp_tbl_MaterialTransfer_M_Delete";

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

        #region Insert MaterialTransfer Details
        /// <summary>
        /// To Insert details of MaterialTransfer in tbl_MaterialTransfer_M table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="objMaterialTransferBO"></param>
        /// <returns></returns>
        public ApplicationResult MaterialTransfer_Insert(MaterialTransferBO objMaterialTransferBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[12];


                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objMaterialTransferBO.TrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objMaterialTransferBO.SchoolMID;

                pSqlParameter[2] = new SqlParameter("@MaterialID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objMaterialTransferBO.MaterialTID;

                pSqlParameter[3] = new SqlParameter("@Quantity", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objMaterialTransferBO.Quantity;

                pSqlParameter[4] = new SqlParameter("@UOMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objMaterialTransferBO.UOMID;

                pSqlParameter[5] = new SqlParameter("@TransferTo", SqlDbType.Char);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objMaterialTransferBO.TransferTo;

                pSqlParameter[6] = new SqlParameter("@TransferToID", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objMaterialTransferBO.TransferToID;

                //pSqlParameter[7] = new SqlParameter("@Year", SqlDbType.VarChar);
                //pSqlParameter[7].Direction = ParameterDirection.Input;
                //pSqlParameter[7].Value = objMaterialTransferBO.Year;

                pSqlParameter[7] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objMaterialTransferBO.IsDeleted;

                pSqlParameter[8] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objMaterialTransferBO.CreatedDate;

                pSqlParameter[9] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objMaterialTransferBO.CreatedUserID;

                pSqlParameter[10] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objMaterialTransferBO.LastModifiedUserID;

                pSqlParameter[11] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objMaterialTransferBO.LastModifiedDate;


                strStoredProcName = "usp_tbl_MaterialTransfer_M_Insert";
                //int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, sSql, pSqlParameter);

                //if (iResult > 0)
                //{
                //    ApplicationResult objResults = new ApplicationResult();
                //    objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                //    return objResults;
                //}
                //else
                //{
                //    ApplicationResult objResults = new ApplicationResult();
                //    objResults.status = ApplicationResult.CommonStatusType.FAILURE;
                //    return objResults;
                //}

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                if (dtResult.Rows.Count > 0)
                {
                    int intStatus = Convert.ToInt32(dtResult.Rows[0]["Status"].ToString());
                    if (intStatus == 1)
                    {
                        objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                    }
                    else if (intStatus == 0)
                    {
                        objResults.status = ApplicationResult.CommonStatusType.FAILURE;
                    }
                }
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objMaterialTransferBO = null;
            }
        }
        #endregion

        #region Update MaterialTransfer Details
        /// <summary>
        /// To Update details of MaterialTransfer in tbl_MaterialTransfer_M table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="objMaterialTransferBO"></param>
        /// <returns></returns>
        public ApplicationResult MaterialTransfer_Update(MaterialTransferBO objMaterialTransferBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[14];


                pSqlParameter[0] = new SqlParameter("@MTransferID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objMaterialTransferBO.MTransferID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objMaterialTransferBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objMaterialTransferBO.SchoolMID;

                pSqlParameter[3] = new SqlParameter("@MaterialID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objMaterialTransferBO.MaterialTID;

                pSqlParameter[4] = new SqlParameter("@Quantity", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objMaterialTransferBO.Quantity;

                pSqlParameter[5] = new SqlParameter("@UOMID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objMaterialTransferBO.UOMID;

                pSqlParameter[6] = new SqlParameter("@TransferTo", SqlDbType.Char);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objMaterialTransferBO.TransferTo;

                pSqlParameter[7] = new SqlParameter("@TransferToID", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objMaterialTransferBO.TransferToID;

                pSqlParameter[8] = new SqlParameter("@Year", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objMaterialTransferBO.Year;

                pSqlParameter[9] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objMaterialTransferBO.IsDeleted;

                pSqlParameter[10] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objMaterialTransferBO.CreatedDate;

                pSqlParameter[11] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objMaterialTransferBO.CreatedUserID;

                pSqlParameter[12] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objMaterialTransferBO.LastModifiedUserID;

                pSqlParameter[13] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objMaterialTransferBO.LastModifiedDate;


                sSql = "usp_tbl_MaterialTransfer_M_Update";
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
                objMaterialTransferBO = null;
            }
        }
        #endregion

        #region Select Purchase Details by PurchaseID
        /// <summary>
        /// Select all details of Purchase for selected PurchaseID from tbl_Purchase_M table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="intPurchaseID"></param>
        /// <returns></returns>
        public ApplicationResult MaterialTransfer_Select(string strFromDate, string strToDate, int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@FromDate", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strFromDate;

                pSqlParameter[1] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strToDate;

                pSqlParameter[2] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intTrustMID;

                pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_MaterialTransfer_M_Select";

                DataTable dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
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

        #region Select Transfer report
        /// <summary>
        /// Select all details of Purchase for selected PurchaseID from tbl_Purchase_M table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="intPurchaseID"></param>
        /// <returns></returns>
        public ApplicationResult MaterialTransfer_SelectForReport(string strFromDate, string strToDate, int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@FromDate", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strFromDate;

                pSqlParameter[1] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strToDate;

                pSqlParameter[2] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intTrustMID;

                pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_MaterialTransfer_M_SelectForReport";

                DataTable dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
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


