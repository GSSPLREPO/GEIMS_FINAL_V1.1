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
    /// Class Created By : NafisaMulla, 09-04-2015
    /// Summary description for Organisation.
    /// </summary>
    public class PayItemBl
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        #region Select All PayItem Details
        //public ApplicationResult PayItem_SelectAll(int intTrustMID, int intSchoolMID*)
        public ApplicationResult PayItem_SelectAll(int intTrustMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                //pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                //pSqlParameter[1].Direction = ParameterDirection.Input;
                //pSqlParameter[1].Value = intSchoolMID;

                sSql = "usp_tbl_PayItem_M_SelectAll";
                DataTable dtPayItem = new DataTable();
                dtPayItem = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtPayItem);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select PayItem Details by PayItemMID
        /// <summary>
        /// Select all details of PayItem for selected PayItemMID from tbl_PayItem_M table
        /// Created By : NafisaMulla, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intPayItemMID"></param>
        /// <returns></returns>
        public ApplicationResult PayItem_Select(int intPayItemMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@PayItemMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intPayItemMID;

                strStoredProcName = "usp_tbl_PayItem_M_Select";

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

        #region Delete PayItem Details by PayItemMID
        /// <summary>
        /// To Delete details of PayItem for selected PayItemMID from tbl_PayItem_M table
        /// Created By : NafisaMulla, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intPayItemMID"></param>
        /// <returns></returns>
        public ApplicationResult PayItem_Delete(int intPayItemMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@PayItemMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intPayItemMID;

                strStoredProcName = "usp_tbl_PayItem_M_Delete";

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

        #region Insert PayItem Details
        /// <summary>
        /// To Insert details of PayItem in tbl_PayItem_M table
        /// Created By : NafisaMulla, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="objPayItemBO"></param>
        /// <returns></returns>
        public ApplicationResult PayItem_Insert(PayItemBo objPayItemBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[11];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objPayItemBO.TrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objPayItemBO.SchoolMID; ;

                pSqlParameter[2] = new SqlParameter("@Name", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objPayItemBO.Name;

                pSqlParameter[3] = new SqlParameter("@Description", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objPayItemBO.Description;

                pSqlParameter[4] = new SqlParameter("@Type", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objPayItemBO.Type;

                pSqlParameter[5] = new SqlParameter("@Deduction", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objPayItemBO.Deduction;

                pSqlParameter[6] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objPayItemBO.IsDeleted;

                pSqlParameter[7] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objPayItemBO.CreatedUserID;

                pSqlParameter[8] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objPayItemBO.CreatedDate;

                pSqlParameter[9] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objPayItemBO.LastModifiedUserID;

                pSqlParameter[10] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objPayItemBO.LastModifiedDate;

                sSql = "usp_tbl_PayItem_M_Insert";
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
                objPayItemBO = null;
            }
        }
        #endregion

        #region Update PayItem Details
        /// <summary>
        /// To Update details of PayItem in tbl_PayItem_M table
        /// Created By : NafisaMulla, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="objPayItemBO"></param>
        /// <returns></returns>
        public ApplicationResult PayItem_Update(PayItemBo objPayItemBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[12];

                pSqlParameter[0] = new SqlParameter("@PayItemMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objPayItemBO.PayItemMID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objPayItemBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objPayItemBO.SchoolMID; ;

                pSqlParameter[3] = new SqlParameter("@Name", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objPayItemBO.Name;

                pSqlParameter[4] = new SqlParameter("@Description", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objPayItemBO.Description;

                pSqlParameter[5] = new SqlParameter("@Type", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objPayItemBO.Type;

                pSqlParameter[6] = new SqlParameter("@Deduction", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objPayItemBO.Deduction;

                pSqlParameter[7] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objPayItemBO.IsDeleted;

                pSqlParameter[8] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objPayItemBO.CreatedUserID;

                pSqlParameter[9] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objPayItemBO.CreatedDate;

                pSqlParameter[10] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objPayItemBO.LastModifiedUserID;

                pSqlParameter[11] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objPayItemBO.LastModifiedDate;

                sSql = "usp_tbl_PayItem_M_Update";
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
                objPayItemBO = null;
            }
        }
        #endregion

        #region Select PayItem Details by PayItemName
        /// <summary>
        /// Select all details of PayItem for selected PayItemName from tbl_PayItem_M table
        /// Created By : NafisaMulla, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="PayItemName"></param>
        /// <returns></returns>
        public ApplicationResult PayItem_Select_byPayItemName(string strPayItemName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@PayItemName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strPayItemName;

                strStoredProcName = "usp_tbl_PayItem_M_Select_ByPayItem";

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

        #region ValidateName for PayItem
        /// <summary>
        /// Function which validates whether the PayItemName already exits in tbl_PayItem_M table.
        /// Created By : NafisaMulla, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="strPayItemName"></param>
        /// <returns></returns>
        public ApplicationResult PayItem_ValidateName(string strPayItemName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@PayItemName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strPayItemName;

                strStoredProcName = "usp_tbl_PayItem_M_Validate_PayItemName";

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

        #region Select All PayItem Details
        /// <summary>
        /// To Select All data from the tbl_PayItem_M table
        /// Created By : NafisaMulla, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult PayItem_Select_PayItemName(int intTrustMID, int intSchoolMID)
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

                sSql = "usp_tbl_PayItem_M_Select_PayItemName";
                DataTable dtPayItem = new DataTable();
                dtPayItem = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtPayItem);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select PayItem Cascade by PayItemMID

        public ApplicationResult PayItem_M_Cascade(int intPayItemID, int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@PayItemMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intPayItemID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_PayItem_M_Cascade";
                
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



