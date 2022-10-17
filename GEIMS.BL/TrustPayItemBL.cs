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
    /// Class Created By : Nafisa, 09-04-2015
    /// Summary description for Organisation.
    /// </summary>
    public class TrustPayItemBl
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        #region Select All TrustPayItem Details
        /// <summary>
        /// To Select All data from the tbl_TrustPayItem_M table
        /// Created By : Nafisa, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult TrustPayItem_SelectAll(int intTrustMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                sSql = "usp_tbl_TrustPayItem_M_SelectAll";
                DataTable dtTrustPayItem = new DataTable();
                dtTrustPayItem = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtTrustPayItem);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select All TrustPayItem Details
        /// <summary>
        /// To Select All data from the tbl_TrustPayItem_M table
        /// Created By : Nafisa, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult TrustPayItem_SelectAll_AscOrder(int intTrustMID,  int intSchoolMID)
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

                sSql = "usp_tbl_TrustPayItem_M_SelectAll_AscOrder";
                DataTable dtTrustPayItem = new DataTable();
                dtTrustPayItem = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtTrustPayItem);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select TrustPayItem Details by TrustPayItemID
        /// <summary>
        /// Select all details of TrustPayItem for selected TrustPayItemID from tbl_TrustPayItem_M table
        /// Created By : Nafisa, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intTrustPayItemID"></param>
        /// <returns></returns>
        public ApplicationResult TrustPayItem_Select(int intTrustPayItemID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TrustPayItemID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustPayItemID;

                strStoredProcName = "usp_tbl_TrustPayItem_M_Select";

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

        #region Delete TrustPayItemTemplate Cascade Valdiation 
        public ApplicationResult TrustPayItemTemplate_Delete_Cascade(int intTrustPayItemID, int intPayItemID/*,int intIsDeleted*/)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@TrustPayItemID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustPayItemID;

                pSqlParameter[1] = new SqlParameter("@PayItemID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intPayItemID;

                strStoredProcName = "usp_PayItemTemplate_Delete_Cascade";

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

        #region Delete TrustPayItemTemplate Details by TrustPayItemID
        /// <summary>
        /// To Delete details of TrustPayItem for selected TrustPayItemID from tbl_TrustPayItem_M table
        /// Created By : Nafisa, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intTrustPayItemID"></param>
        /// <returns></returns>
        public ApplicationResult TrustPayItem_Delete(int intTrustPayItemID, int intLastModifiedID, string strLastModifiedDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@TrustPayItemID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustPayItemID;

                pSqlParameter[1] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastModifiedID;

                pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifiedDate;

                strStoredProcName = "usp_tbl_TrustPayItem_M_Delete";

                int iResult = DatabaseTransaction.ExecuteNonQuery(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
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

        #region Insert TrustPayItem Details
        /// <summary>
        /// To Insert details of TrustPayItem in tbl_TrustPayItem_M table
        /// Created By : Nafisa, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="objTrustPayItemBO"></param>
        /// <returns></returns>
        public ApplicationResult TrustPayItem_Insert(TrustPayItemBo objTrustPayItemBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[8];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objTrustPayItemBO.TrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objTrustPayItemBO.SchoolMID;

                pSqlParameter[2] = new SqlParameter("@PayItemID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objTrustPayItemBO.PayItemID;

                pSqlParameter[3] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objTrustPayItemBO.IsDeleted;

                pSqlParameter[4] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objTrustPayItemBO.CreatedUserID;

                pSqlParameter[5] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objTrustPayItemBO.CreatedDate;

                pSqlParameter[6] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objTrustPayItemBO.LastModifiedUserID;

                pSqlParameter[7] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objTrustPayItemBO.LastModifiedDate;

                sSql = "usp_tbl_TrustPayItem_M_Insert";
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
                objTrustPayItemBO = null;
            }
        }
        #endregion

        #region Update TrustPayItem Details
        /// <summary>
        /// To Update details of TrustPayItem in tbl_TrustPayItem_M table
        /// Created By : Nafisa, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="objTrustPayItemBO"></param>
        /// <returns></returns>
        public ApplicationResult TrustPayItem_Update(TrustPayItemBo objTrustPayItemBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];


                pSqlParameter[0] = new SqlParameter("@TrustPayItemID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objTrustPayItemBO.TrustPayItemID;

                pSqlParameter[1] = new SqlParameter("@PayItemID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objTrustPayItemBO.PayItemID;

                pSqlParameter[2] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objTrustPayItemBO.LastModifiedUserID;

                pSqlParameter[3] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objTrustPayItemBO.LastModifiedDate;

                sSql = "usp_tbl_TrustPayItem_M_Update";
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
                objTrustPayItemBO = null;
            }
        }
        #endregion

        #region Select TrustPayItem Details by TrustPayItemName
        /// <summary>
        /// Select all details of TrustPayItem for selected TrustPayItemName from tbl_TrustPayItem_M table
        /// Created By : Nafisa, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="TrustPayItemName"></param>
        /// <returns></returns>
        public ApplicationResult TrustPayItem_Select_byTrustPayItemName(string strTrustPayItemName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TrustPayItemName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strTrustPayItemName;

                strStoredProcName = "usp_tbl_TrustPayItem_M_Select_ByTrustPayItem";

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

        #region ValidateName for TrustPayItem
        /// <summary>
        /// Function which validates whether the TrustPayItemName already exits in tbl_TrustPayItem_M table.
        /// Created By : Nafisa, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="strTrustPayItemName"></param>
        /// <returns></returns>
        public ApplicationResult TrustPayItem_ValidateName(string strTrustPayItemName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TrustPayItemName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strTrustPayItemName;

                strStoredProcName = "usp_tbl_TrustPayItem_M_Validate_TrustPayItemName";

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
        public ApplicationResult PayItemTemplate_SelectAll_TemplateID(int intTemplateID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TemplateID ", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTemplateID;

                sSql = "usp_tbl_PayItemTemplate_M_SelectAll_TemplateID";
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

        #region Select All PayItem Details
        /// <summary>
        /// To Select All data from the tbl_PayItem_M table
        /// Created By : NafisaMulla, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult PayItemTemplate_SelectAll_BothID(int intTemplateID, int intPayItem)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@UserTemplateID ", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTemplateID;

                pSqlParameter[1] = new SqlParameter("@PayItem ", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intPayItem;

                sSql = "usp_tbl_PayItemTemplate_M_SelectAll_BothID";
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

        /// <summary>
        /// 17/10/2022 Bhandavi 
        /// To check selected parameters are already exists in [tbl_TrustPayItem_M] table
        /// </summary>
        /// <param name=></param>
        /// <returns></returns>
        public int CheckTrustPayItem(int TrustMID,int SchoolMID,int PayItemID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = TrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = SchoolMID;

                pSqlParameter[2] = new SqlParameter("@PayItemID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = PayItemID;

                //pSqlParameter[3] = new SqlParameter("@Count", SqlDbType.Int);
                //pSqlParameter[3].Direction = ParameterDirection.Output;
                //pSqlParameter[2].Value = objTrustPayItemBO.PayItemID;

                sSql = "usp_tbl_TrustPayItem_M_Check";
                DataTable dt= DatabaseTransaction.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);
                if (dt.Rows.Count > 0)
                    return Convert.ToInt32(dt.Rows[0][0]);
                else
                    return 0;
                //if (dt.Rows.Count > 0)
                //{
                //    ApplicationResult objResults = new ApplicationResult();
                //    objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                //    return dt.;
                //}
                //else
                //{
                //    ApplicationResult objResults = new ApplicationResult();
                //    objResults.status = ApplicationResult.CommonStatusType.FAILURE;
                //    return objResults;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //objResults.sta = null;
            }
        }

    }
}


