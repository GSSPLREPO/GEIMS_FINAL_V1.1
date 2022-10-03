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
    public class MaterialStockTBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion


        #region Select All MaterialStockT Details
        /// <summary>
        /// To Select All data from the tbl_MaterialStock_T table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult MaterialStockT_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_MaterialStock_T_SelectAll";
                DataTable dtMaterialStockT = new DataTable();
                dtMaterialStockT = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtMaterialStockT);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select MaterialStockT Details by StockTID
        /// <summary>
        /// Select all details of MaterialStockT for selected StockTID from tbl_MaterialStock_T table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="intStockTID"></param>
        /// <returns></returns>
        public ApplicationResult MaterialStockT_Select(int intStockTID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@StockTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStockTID;

                strStoredProcName = "usp_tbl_MaterialStock_T_Select";

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

        #region Delete MaterialStockT Details by StockTID
        /// <summary>
        /// To Delete details of MaterialStockT for selected StockTID from tbl_MaterialStock_T table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="intStockTID"></param>
        /// <returns></returns>
        public ApplicationResult MaterialStockT_Delete(int intStockTID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@StockTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStockTID;

                strStoredProcName = "usp_tbl_MaterialStock_T_Delete";

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

        #region Insert MaterialStockT Details
        /// <summary>
        /// To Insert details of MaterialStockT in tbl_MaterialStock_T table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="objMaterialStockTBO"></param>
        /// <returns></returns>
        public ApplicationResult MaterialStockT_Insert(MaterialStockTBO objMaterialStockTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[11];


                pSqlParameter[0] = new SqlParameter("@MaterialID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objMaterialStockTBO.MaterialID;

                pSqlParameter[1] = new SqlParameter("@Quantity", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objMaterialStockTBO.Quantity;

                pSqlParameter[2] = new SqlParameter("@IsOpeningStock", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objMaterialStockTBO.IsOpeningStock;

                pSqlParameter[3] = new SqlParameter("@StockYear", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objMaterialStockTBO.StockYear;

                pSqlParameter[4] = new SqlParameter("@MaterialInDate", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objMaterialStockTBO.MaterialInDate;

                pSqlParameter[5] = new SqlParameter("@Price", SqlDbType.Float);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objMaterialStockTBO.Price;

                pSqlParameter[6] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objMaterialStockTBO.IsDeleted;

                pSqlParameter[7] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objMaterialStockTBO.CreatedDate;

                pSqlParameter[8] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objMaterialStockTBO.CreatedUserID;

                pSqlParameter[9] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objMaterialStockTBO.LastModifiedDate;

                pSqlParameter[10] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objMaterialStockTBO.LastModifiedUserID;


                sSql = "usp_tbl_MaterialStock_T_Insert";
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
                objMaterialStockTBO = null;
            }
        }
        #endregion

        #region Update MaterialStockT Details
        /// <summary>
        /// To Update details of MaterialStockT in tbl_MaterialStock_T table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="objMaterialStockTBO"></param>
        /// <returns></returns>
        public ApplicationResult MaterialStockT_Update(MaterialStockTBO objMaterialStockTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[12];


                pSqlParameter[0] = new SqlParameter("@StockTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objMaterialStockTBO.StockTID;

                pSqlParameter[1] = new SqlParameter("@MaterialID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objMaterialStockTBO.MaterialID;

                pSqlParameter[2] = new SqlParameter("@Quantity", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objMaterialStockTBO.Quantity;

                pSqlParameter[3] = new SqlParameter("@IsOpeningStock", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objMaterialStockTBO.IsOpeningStock;

                pSqlParameter[4] = new SqlParameter("@StockYear", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objMaterialStockTBO.StockYear;

                pSqlParameter[5] = new SqlParameter("@MaterialInDate", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objMaterialStockTBO.MaterialInDate;

                pSqlParameter[6] = new SqlParameter("@Price", SqlDbType.Float);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objMaterialStockTBO.Price;

                pSqlParameter[7] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objMaterialStockTBO.IsDeleted;

                pSqlParameter[8] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objMaterialStockTBO.CreatedDate;

                pSqlParameter[9] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objMaterialStockTBO.CreatedUserID;

                pSqlParameter[10] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objMaterialStockTBO.LastModifiedDate;

                pSqlParameter[11] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objMaterialStockTBO.LastModifiedUserID;


                sSql = "usp_tbl_MaterialStock_T_Update";
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
                objMaterialStockTBO = null;
            }
        }
        #endregion

        #region Insert MaterialStockT for StockUpdation
        /// <summary>
        /// To Insert details of MaterialStockT in tbl_MaterialStock_T table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="objMaterialStockTBO"></param>
        /// <returns></returns>
        public ApplicationResult MaterialStockT_Insert_ForStockUpdate(MaterialStockTBO objMaterialStockTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];


                pSqlParameter[0] = new SqlParameter("@MaterialTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objMaterialStockTBO.MaterialID;

                pSqlParameter[1] = new SqlParameter("@Quantity", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objMaterialStockTBO.Quantity;

                pSqlParameter[2] = new SqlParameter("@Year", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objMaterialStockTBO.StockYear;

                pSqlParameter[3] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objMaterialStockTBO.CreatedDate;

                pSqlParameter[4] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objMaterialStockTBO.CreatedUserID;


                sSql = "usp_tbl_Material_M_Insert_ForStockUpdate";
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
                objMaterialStockTBO = null;
            }
        }
        #endregion




        #region Select MaterialStockT Details by MaterialStockTName
        /// <summary>
        /// Select all details of MaterialStockT for selected MaterialStockTName from tbl_MaterialStock_T table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="MaterialStockTName"></param>
        /// <returns></returns>
        public ApplicationResult MaterialStockT_Select_byMaterialStockTName(string strMaterialStockTName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@MaterialStockTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strMaterialStockTName;

                strStoredProcName = "usp_tbl_MaterialStock_T_Select_ByMaterialStockT";

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


        #region ValidateName for MaterialStockT
        /// <summary>
        /// Function which validates whether the MaterialStockTName already exits in tbl_MaterialStock_T table.
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="strMaterialStockTName"></param>
        /// <returns></returns>
        public ApplicationResult MaterialStockT_ValidateName(string strMaterialStockTName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@MaterialStockTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strMaterialStockTName;

                strStoredProcName = "usp_tbl_MaterialStock_T_Validate_MaterialStockTName";

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

        #region Select Material Stock
        /// <summary>
        /// Select all details of Purchase for selected PurchaseID from tbl_Purchase_M table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="intPurchaseID"></param>
        /// <returns></returns>
        public ApplicationResult Material_StockReport(int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_Material_M_StockReport";

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

        #region Select Material VandorWise
        /// <summary>
        /// Select all details of Purchase for selected PurchaseID from tbl_Purchase_M table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="intPurchaseID"></param>
        /// <returns></returns>
        public ApplicationResult Material_VandorWiseReport(int intVandorID,string strFromDate,string strToDate, int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];

                pSqlParameter[0] = new SqlParameter("@VendorID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intVandorID;

                pSqlParameter[1] = new SqlParameter("@FromDate", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strFromDate;

                pSqlParameter[2] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strToDate;

                pSqlParameter[3] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intTrustMID;

                pSqlParameter[4] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_VendorwiseMaterial_Select_ForReport";

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