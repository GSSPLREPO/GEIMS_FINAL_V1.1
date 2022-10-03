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
    /// Class Created By : Chintan, 10/13/2014
    /// Summary description for Organisation.
    /// </summary>
    public class MaterialBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion


        #region Select All Material Details
        /// <summary>
        /// To Select All data from the tbl_Material_M table
        /// Created By : Chintan, 10/13/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult Material_SelectAll(int intMaterialGroupID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@MaterialGroupID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMaterialGroupID;

                sSql = "usp_tbl_Material_M_SelectAll";

                DataTable dtMaterial = new DataTable();
                dtMaterial = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtMaterial);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select Material Details by MaterialID
        /// <summary>
        /// Select all details of Material for selected MaterialID from tbl_Material_M table
        /// Created By : Chintan, 10/13/2014
        /// Modified By :
        /// </summary>
        /// <param name="intMaterialID"></param>
        /// <returns></returns>
        public ApplicationResult Material_Select(int intMaterialID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@MaterialID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMaterialID;

                strStoredProcName = "usp_tbl_Material_M_Select";

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

        #region Select Material Details by MaterialID for Purchase
        /// <summary>
        /// Select all details of Material for selected MaterialID from tbl_Material_M table
        /// Created By : Chintan, 10/13/2014
        /// Modified By :
        /// </summary>
        /// <param name="intMaterialID"></param>
        /// <returns></returns>
        public ApplicationResult Material_Select_ForPurchase(int intMaterialID, int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@MaterialID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMaterialID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_Material_M_SelectForPurchase";

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

        #region Delete Material Details by MaterialID
        /// <summary>
        /// To Delete details of Material for selected MaterialID from tbl_Material_M table
        /// Created By : Chintan, 10/13/2014
        /// Modified By :
        /// </summary>
        /// <param name="intMaterialID"></param>
        /// <returns></returns>
        public ApplicationResult Material_Delete(int intMaterialID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@MaterialID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMaterialID;

                strStoredProcName = "usp_tbl_Material_M_Delete";

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

        #region Insert Material Details
        /// <summary>
        /// To Insert details of Material in tbl_Material_M table
        /// Created By : Chintan, 10/13/2014
        /// Modified By :
        /// </summary>
        /// <param name="objMaterialBO"></param>
        /// <returns></returns>
        public ApplicationResult Material_Insert(MaterialBO objMaterialBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[13];


                pSqlParameter[0] = new SqlParameter("@MaterialGroupID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objMaterialBO.MaterialGroupID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objMaterialBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objMaterialBO.SchoolMID;

                pSqlParameter[3] = new SqlParameter("@UOMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objMaterialBO.UOMID;

                pSqlParameter[4] = new SqlParameter("@MaterialCode", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objMaterialBO.MaterialCode;

                pSqlParameter[5] = new SqlParameter("@MaterialName", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objMaterialBO.MaterialName;

                pSqlParameter[6] = new SqlParameter("@Description", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objMaterialBO.Description;

                pSqlParameter[7] = new SqlParameter("@ModelNo", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objMaterialBO.ModelNo;

                pSqlParameter[8] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objMaterialBO.IsDeleted;

                pSqlParameter[9] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objMaterialBO.CreatedDate;

                pSqlParameter[10] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objMaterialBO.CreatedUserID;

                pSqlParameter[11] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objMaterialBO.LastModifiedDate;

                pSqlParameter[12] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objMaterialBO.LastModifiedUserID;


                sSql = "usp_tbl_Material_M_Insert";
                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objMaterialBO = null;
            }
        }
        #endregion

        #region Update Material Details
        /// <summary>
        /// To Update details of Material in tbl_Material_M table
        /// Created By : Chintan, 10/13/2014
        /// Modified By :
        /// </summary>
        /// <param name="objMaterialBO"></param>
        /// <returns></returns>
        public ApplicationResult Material_Update(MaterialBO objMaterialBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[14];


                pSqlParameter[0] = new SqlParameter("@MaterialID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objMaterialBO.MaterialID;

                pSqlParameter[1] = new SqlParameter("@MaterialGroupID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objMaterialBO.MaterialGroupID;

                pSqlParameter[2] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objMaterialBO.TrustMID;

                pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objMaterialBO.SchoolMID;

                pSqlParameter[4] = new SqlParameter("@UOMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objMaterialBO.UOMID;

                pSqlParameter[5] = new SqlParameter("@MaterialCode", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objMaterialBO.MaterialCode;

                pSqlParameter[6] = new SqlParameter("@MaterialName", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objMaterialBO.MaterialName;

                pSqlParameter[7] = new SqlParameter("@Description", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objMaterialBO.Description;

                pSqlParameter[8] = new SqlParameter("@ModelNo", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objMaterialBO.ModelNo;

                pSqlParameter[9] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objMaterialBO.IsDeleted;

                pSqlParameter[10] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objMaterialBO.CreatedDate;

                pSqlParameter[11] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objMaterialBO.CreatedUserID;

                pSqlParameter[12] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objMaterialBO.LastModifiedDate;

                pSqlParameter[13] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objMaterialBO.LastModifiedUserID;


                sSql = "usp_tbl_Material_M_Update";
                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objMaterialBO = null;
            }
        }
        #endregion

        #region Select All Material Details for DropDown
        /// <summary>
        /// To Select All data from the tbl_Material_M table
        /// Created By : Chintan, 10/13/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult Material_SelectAll_ForDropDown(int intMaterialGroupID, int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@MaterialGroupID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMaterialGroupID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                sSql = "usp_tbl_Material_M_SelectAll_DropDown";

                DataTable dtMaterial = new DataTable();
                dtMaterial = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtMaterial);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select Material Main Quantity by MaterialID, SchoolID, TrustID
        /// <summary>
        /// Select all details of Material for selected MaterialID from tbl_Material_M table
        /// Created By : Chintan, 10/13/2014
        /// Modified By :
        /// </summary>
        /// <param name="intMaterialID"></param>
        /// <returns></returns>
        public ApplicationResult MaterialT_UpdateMainQuantity(int intMaterialID, int intSchoolMID, int intTrustMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@MaterialID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMaterialID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                pSqlParameter[2] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intTrustMID;

                sSql = "usp_tbl_Material_T_UpdateMainQuanity";

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
        }
        #endregion

        #region Select Material Main Quantity by MaterialID, SchoolID, TrustID
        /// <summary>
        /// Select all details of Material for selected MaterialID from tbl_Material_M table
        /// Created By : Chintan, 10/13/2014
        /// Modified By :
        /// </summary>
        /// <param name="intMaterialID"></param>
        /// <returns></returns>
        public ApplicationResult Material_M_Select_ForMultipleMaterialTransfer(string strMaterialID, string strTrustMID, string strSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@MaterialID", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strMaterialID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strTrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strSchoolMID;

                strStoredProcName = "usp_tbl_Material_M_Select_ForMultipleMaterialTransfer";
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

        #region Select Material Main Quantity by MaterialID, SchoolID, TrustID
        /// <summary>
        /// Select all details of Material for selected MaterialID from tbl_Material_M table
        /// Created By : Chintan, 10/13/2014
        /// Modified By :
        /// </summary>
        /// <param name="intMaterialID"></param>
        /// <returns></returns>
        public ApplicationResult Material_T_Select(int intMaterialID, int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@MaterialID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMaterialID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_MaterialT_Select";
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

        #region Check weather its new year or not and stock Update
        /// <summary>
        /// To Select All data from the tbl_Material_M table
        /// Created By : Chintan, 10/13/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult Material_Select_ForStockUpdate()
        {
            try
            {

                sSql = "usp_tbl_Material_M_Select_ForStockUpdate";

                DataTable dtMaterial = new DataTable();
                dtMaterial = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtMaterial);
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


