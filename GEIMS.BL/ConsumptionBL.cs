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
    public class ConsumptionBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion


        #region Select All Consumption Details
        /// <summary>
        /// To Select All data from the tbl_Consumption_M table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult Consumption_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_Consumption_M_SelectAll";
                DataTable dtConsumption = new DataTable();
                dtConsumption = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtConsumption);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select Consumption Details by ConsumptionID
        /// <summary>
        /// Select all details of Consumption for selected ConsumptionID from tbl_Consumption_M table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="intConsumptionID"></param>
        /// <returns></returns>
        public ApplicationResult Consumption_Select(int intConsumptionID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@ConsumptionID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intConsumptionID;

                strStoredProcName = "usp_tbl_Consumption_M_Select";

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

        #region Delete Consumption Details by ConsumptionID
        /// <summary>
        /// To Delete details of Consumption for selected ConsumptionID from tbl_Consumption_M table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="intConsumptionID"></param>
        /// <returns></returns>
        public ApplicationResult Consumption_Delete(int intConsumptionID , int intMaterialID, int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@ConsumptionID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intConsumptionID;

                pSqlParameter[1] = new SqlParameter("@MaterialID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intMaterialID;

                pSqlParameter[2] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intTrustMID;

                pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_Consumption_M_Delete";

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

        #region Insert Consumption Details
        /// <summary>
        /// To Insert details of Consumption in tbl_Consumption_M table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="objConsumptionBO"></param>
        /// <returns></returns>
        public ApplicationResult Consumption_Insert(ConsumptionBO objConsumptionBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[12];


                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objConsumptionBO.TrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objConsumptionBO.SchoolMID;

                pSqlParameter[2] = new SqlParameter("@MaterialID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objConsumptionBO.MaterialID;

                pSqlParameter[3] = new SqlParameter("@Quantity", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objConsumptionBO.Quantity;

                pSqlParameter[4] = new SqlParameter("@UOMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objConsumptionBO.UOMID;

                pSqlParameter[5] = new SqlParameter("@IsConsumption", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objConsumptionBO.IsConsumption;

                pSqlParameter[6] = new SqlParameter("@ConsumptionDate", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objConsumptionBO.ConsumptionDate;

                pSqlParameter[7] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objConsumptionBO.IsDeleted;

                pSqlParameter[8] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objConsumptionBO.CreatedDate;

                pSqlParameter[9] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objConsumptionBO.CreatedUserID;

                pSqlParameter[10] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objConsumptionBO.LastModifiedUserID;

                pSqlParameter[11] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objConsumptionBO.LastModifiedDate;


                sSql = "usp_tbl_Consumption_M_Insert";
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
                objConsumptionBO = null;
            }
        }
        #endregion

        #region Update Consumption Details
        /// <summary>
        /// To Update details of Consumption in tbl_Consumption_M table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="objConsumptionBO"></param>
        /// <returns></returns>
        public ApplicationResult Consumption_Update(ConsumptionBO objConsumptionBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[10];


                pSqlParameter[0] = new SqlParameter("@ConsumptionID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objConsumptionBO.ConsumptionID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objConsumptionBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objConsumptionBO.SchoolMID;

                pSqlParameter[3] = new SqlParameter("@MaterialID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objConsumptionBO.MaterialID;

                pSqlParameter[4] = new SqlParameter("@Quantity", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objConsumptionBO.Quantity;

                pSqlParameter[5] = new SqlParameter("@UOMID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objConsumptionBO.UOMID;

                pSqlParameter[6] = new SqlParameter("@ConsumptionDate", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objConsumptionBO.ConsumptionDate;

                pSqlParameter[7] = new SqlParameter("@IsConsumption", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objConsumptionBO.IsConsumption;

                pSqlParameter[8] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objConsumptionBO.LastModifiedUserID;

                pSqlParameter[9] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objConsumptionBO.LastModifiedDate;


                sSql = "usp_tbl_Consumption_M_Update";
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
                objConsumptionBO = null;
            }
        }
        #endregion

        #region Select Consumption Details by Type
        /// <summary>
        /// Select all details of Consumption for selected ConsumptionID from tbl_Consumption_M table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="intConsumptionID"></param>
        /// <returns></returns>
        public ApplicationResult Consumption_SelectByType(string strFromDate, string strTodate, int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@FromDate", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strFromDate;

                pSqlParameter[1] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strTodate;

                pSqlParameter[2] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intTrustMID;

                pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intSchoolMID;

                //pSqlParameter[4] = new SqlParameter("@Type", SqlDbType.Int);
                //pSqlParameter[4].Direction = ParameterDirection.Input;
                //pSqlParameter[4].Value = intTypeID;

                strStoredProcName = "usp_tbl_Consumption_M_SelectByType";

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

        #region Select Consumption Details by Type for report
        /// <summary>
        /// Select all details of Consumption for selected ConsumptionID from tbl_Consumption_M table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="intConsumptionID"></param>
        /// <returns></returns>
        public ApplicationResult Consumption_SelectForReport(string strFromDate, string strTodate, int intTrustMID, int intSchoolMID, int intTypeID)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];

                pSqlParameter[0] = new SqlParameter("@FromDate", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strFromDate;

                pSqlParameter[1] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strTodate;

                pSqlParameter[2] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intTrustMID;

                pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intSchoolMID;

                pSqlParameter[4] = new SqlParameter("@Type", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intTypeID;

                strStoredProcName = "usp_tbl_Consumption_M_SelectForReport";

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