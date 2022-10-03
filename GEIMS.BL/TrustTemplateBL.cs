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
    public class TrustTemplateBl
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        //Not Used 
        //#region Select All TrustTemplate Details
        ///// <summary>
        ///// To Select All data from the tbl_TrustTemplate_M table
        ///// Created By : NafisaMulla, 09-04-2015
        ///// Modified By :
        ///// </summary>
        ///// <param name=""></param>
        ///// <returns></returns>
        //public ApplicationResult TrustTemplate_SelectAll(int intTrustMID)
        //{
        //    try
        //    {
        //        pSqlParameter = new SqlParameter[1];

        //        pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
        //        pSqlParameter[0].Direction = ParameterDirection.Input;
        //        pSqlParameter[0].Value = intTrustMID;

        //        sSql = "usp_tbl_TrustTemplate_M_SelectAll";
        //        DataTable dtTrustTemplate = new DataTable();
        //        dtTrustTemplate = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

        //        ApplicationResult objResults = new ApplicationResult(dtTrustTemplate);
        //        objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
        //        return objResults;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //#endregion

        #region Select All TrustTemplate Details TrustMID and SchoolMID Wise
        /// <summary>
        /// To Select All data from the tbl_TrustTemplate_M table
        /// Created By : NafisaMulla, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult TrustTemplate_Select_SchoolWise(int intTrustMID, int intSchoolMID)
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

                sSql = "usp_tbl_TrustTemplate_M_Select_SchoolWise";
                DataTable dtTrustTemplate = new DataTable();
                dtTrustTemplate = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtTrustTemplate);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select All TrustTemplate Details
        /// <summary>
        /// To Select All data from the tbl_TrustTemplate_M table
        /// Created By : NafisaMulla, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult TrustTemplate_SelectAll_PayItemWithAsc(int intTrustMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                sSql = "usp_tbl_PayItem_M_SelectAll_PayItemID";
                DataTable dtTrustTemplate = new DataTable();
                dtTrustTemplate = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtTrustTemplate);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select TrustTemplate Details by TrustTemplateID
        /// <summary>
        /// Select all details of TrustTemplate for selected TrustTemplateID from tbl_TrustTemplate_M table
        /// Created By : NafisaMulla, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intTrustTemplateID"></param>
        /// <returns></returns>
        public ApplicationResult TrustTemplate_Select(int intTrustTemplateID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TrustTemplateID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustTemplateID;

                strStoredProcName = "usp_tbl_TrustTemplate_M_Select";

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

        #region Delete TrustTemplate Details by TrustTemplateID
        /// <summary>
        /// To Delete details of TrustTemplate for selected TrustTemplateID from tbl_TrustTemplate_M table
        /// Created By : NafisaMulla, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intTrustTemplateID"></param>
        /// <returns></returns>
        public ApplicationResult TrustTemplate_Delete(int intTrustTemplateID, int intLastModifiedUserID, string strLastModifiedDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@TrustTemplateID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustTemplateID;

                pSqlParameter[1] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastModifiedUserID;

                pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifiedDate;

                strStoredProcName = "usp_tbl_TrustTemplate_M_Delete";

                int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                //if (iResult == 0)
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

        #region Insert TrustTemplate Details
        /// <summary>
        /// To Insert details of TrustTemplate in tbl_TrustTemplate_M table
        /// Created By : NafisaMulla, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="objTrustTemplateBO"></param>
        /// <returns></returns>
        public ApplicationResult TrustTemplate_Insert(TrustTemplateBo objTrustTemplateBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[8];

                pSqlParameter[0] = new SqlParameter("@TrustTemplateName", SqlDbType.NVarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objTrustTemplateBO.TrustTemplateName;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objTrustTemplateBO.SchoolMID;

                pSqlParameter[2] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objTrustTemplateBO.TrustMID;

                pSqlParameter[3] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objTrustTemplateBO.IsDeleted;

                pSqlParameter[4] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objTrustTemplateBO.CreatedUserID;

                pSqlParameter[5] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objTrustTemplateBO.CreatedDate;

                pSqlParameter[6] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objTrustTemplateBO.LastModifiedUserID;

                pSqlParameter[7] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objTrustTemplateBO.LastModifiedDate;


                sSql = "usp_tbl_TrustTemplate_M_Insert";
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
                objTrustTemplateBO = null;
            }
        }
        #endregion

        #region Update TrustTemplate Details
        /// <summary>
        /// To Update details of TrustTemplate in tbl_TrustTemplate_M table
        /// Created By : NafisaMulla, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="objTrustTemplateBO"></param>
        /// <returns></returns>
        public ApplicationResult TrustTemplate_Update(TrustTemplateBo objTrustTemplateBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

                pSqlParameter[0] = new SqlParameter("@TrustTemplateID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objTrustTemplateBO.TrustTemplateID;

                pSqlParameter[1] = new SqlParameter("@TrustTemplateName", SqlDbType.NVarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objTrustTemplateBO.TrustTemplateName;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objTrustTemplateBO.SchoolMID;

                pSqlParameter[3] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objTrustTemplateBO.TrustMID;

                pSqlParameter[4] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objTrustTemplateBO.LastModifiedUserID;

                pSqlParameter[5] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objTrustTemplateBO.LastModifiedDate;

                sSql = "usp_tbl_TrustTemplate_M_Update";
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
                objTrustTemplateBO = null;
            }
        }
        #endregion

        #region Select TrustTemplate Details by TrustTemplateName
        /// <summary>
        /// Select all details of TrustTemplate for selected TrustTemplateName from tbl_TrustTemplate_M table
        /// Created By : NafisaMulla, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="TrustTemplateName"></param>
        /// <returns></returns>
        public ApplicationResult TrustTemplate_Select_byTrustTemplateName(string strTrustTemplateName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TrustTemplateName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strTrustTemplateName;

                strStoredProcName = "usp_tbl_TrustTemplate_M_Select_ByTrustTemplate";

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

        #region ValidateName for TrustTemplate
        /// <summary>
        /// Function which validates whether the TrustTemplateName already exits in tbl_TrustTemplate_M table.
        /// Created By : NafisaMulla, 09-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="strTrustTemplateName"></param>
        /// <returns></returns>
        public ApplicationResult TrustTemplate_ValidateName(string strTrustTemplateName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TrustTemplateName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strTrustTemplateName;

                strStoredProcName = "usp_tbl_TrustTemplate_M_Validate_TrustTemplateName";

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



