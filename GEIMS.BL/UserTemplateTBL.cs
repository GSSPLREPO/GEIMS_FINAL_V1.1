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
    /// Class Created By : NafisaMulla, 14-04-2015
    /// Summary description for Organisation.
    /// </summary>
    public class UserTemplateTBl
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        #region Select All UserTemplateT Details
        /// <summary>
        /// To Select All data from the tbl_UserTemplate_M table
        /// Created By : NafisaMulla, 14-04-2015
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult UserTemplateT_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_UserTemplate_M_SelectAll";
                DataTable dtUserTemplateT = new DataTable();
                dtUserTemplateT = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtUserTemplateT);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select UserTemplateT Details by UserTemplateID
        /// <summary>
        /// Select all details of UserTemplateT for selected UserTemplateID from tbl_UserTemplate_M table
        /// Created By : NafisaMulla, 14-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intUserTemplateID"></param>
        /// <returns></returns>
        public ApplicationResult UserTemplateT_Select(int intUserTemplateID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@UserTemplateID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intUserTemplateID;

                strStoredProcName = "usp_tbl_UserTemplate_M_Select";

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

        #region Delete UserTemplateT Details by UserTemplateID
        /// <summary>
        /// To Delete details of UserTemplateT for selected UserTemplateID from tbl_UserTemplate_M table
        /// Created By : NafisaMulla, 14-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intUserTemplateID"></param>
        /// <returns></returns>
        public ApplicationResult UserTemplateT_Delete(int intUserTemplateID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@UserTemplateID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intUserTemplateID;

                strStoredProcName = "usp_tbl_UserTemplate_M_Delete";

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

        #region Insert UserTemplateT Details
        /// <summary>
        /// To Insert details of UserTemplateT in tbl_UserTemplate_M table
        /// Created By : NafisaMulla, 14-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="objUserTemplateTBO"></param>
        /// <returns></returns>
        public ApplicationResult UserTemplateT_Insert(UserTemplateTBo objUserTemplateTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[13];


                pSqlParameter[0] = new SqlParameter("@UserID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objUserTemplateTBO.UserID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objUserTemplateTBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objUserTemplateTBO.SchoolMID;

                pSqlParameter[3] = new SqlParameter("@TrustTemplateID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objUserTemplateTBO.TrustTemplateID;

                pSqlParameter[4] = new SqlParameter("@Annual", SqlDbType.Float);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objUserTemplateTBO.Annual;

                pSqlParameter[5] = new SqlParameter("@Monthly", SqlDbType.Float);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objUserTemplateTBO.Monthly;

                pSqlParameter[6] = new SqlParameter("@Gross", SqlDbType.Float);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objUserTemplateTBO.Gross;

                pSqlParameter[7] = new SqlParameter("@LastPaySlipGenerated", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objUserTemplateTBO.LastPaySlipGenerated;

                pSqlParameter[8] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objUserTemplateTBO.IsDeleted;

                pSqlParameter[9] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objUserTemplateTBO.CreatedUserID;

                pSqlParameter[10] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objUserTemplateTBO.CreatedDate;

                pSqlParameter[11] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objUserTemplateTBO.LastModifiedUserID;

                pSqlParameter[12] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objUserTemplateTBO.LastModifiedDate;

                sSql = "usp_tbl_UserTemplate_M_Insert";
                DataTable dtResult = new DataTable();
                dtResult = DatabaseTransaction.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);
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
                objUserTemplateTBO = null;
            }
        }
        #endregion

        #region Update UserTemplateT Details
        /// <summary>
        /// To Update details of UserTemplateT in tbl_UserTemplate_M table
        /// Created By : NafisaMulla, 14-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="objUserTemplateTBO"></param>
        /// <returns></returns>
        public ApplicationResult UserTemplateT_Update(UserTemplateTBo objUserTemplateTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[14];


                pSqlParameter[0] = new SqlParameter("@UserTemplateID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objUserTemplateTBO.UserTemplateID;

                pSqlParameter[1] = new SqlParameter("@UserID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objUserTemplateTBO.UserID;

                pSqlParameter[2] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objUserTemplateTBO.TrustMID;

                pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objUserTemplateTBO.SchoolMID;

                pSqlParameter[4] = new SqlParameter("@TrustTemplateID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objUserTemplateTBO.TrustTemplateID;

                pSqlParameter[5] = new SqlParameter("@Annual", SqlDbType.Float);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objUserTemplateTBO.Annual;

                pSqlParameter[6] = new SqlParameter("@Monthly", SqlDbType.Float);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objUserTemplateTBO.Monthly;

                pSqlParameter[7] = new SqlParameter("@Gross", SqlDbType.Float);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objUserTemplateTBO.Gross;

                pSqlParameter[8] = new SqlParameter("@LastPaySlipGenerated", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objUserTemplateTBO.LastPaySlipGenerated;

                pSqlParameter[9] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objUserTemplateTBO.IsDeleted;

                pSqlParameter[10] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objUserTemplateTBO.CreatedUserID;

                pSqlParameter[11] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objUserTemplateTBO.CreatedDate;

                pSqlParameter[12] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objUserTemplateTBO.LastModifiedUserID;

                pSqlParameter[13] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objUserTemplateTBO.LastModifiedDate;


                sSql = "usp_tbl_UserTemplate_M_Update";
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
                objUserTemplateTBO = null;
            }
        }
        #endregion

        #region Select UserTemplateT Details by UserTemplateTName
        /// <summary>
        /// Select all details of UserTemplateT for selected UserTemplateTName from tbl_UserTemplate_M table
        /// Created By : NafisaMulla, 14-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="UserTemplateTName"></param>
        /// <returns></returns>
        public ApplicationResult UserTemplateT_Select_byUserTemplateTName(string strUserTemplateTName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@UserTemplateTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strUserTemplateTName;

                strStoredProcName = "usp_tbl_UserTemplate_M_Select_ByUserTemplateT";

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

        #region ValidateName for UserTemplateT
        /// <summary>
        /// Function which validates whether the UserTemplateTName already exits in tbl_UserTemplate_M table.
        /// Created By : NafisaMulla, 14-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="strUserTemplateTName"></param>
        /// <returns></returns>
        public ApplicationResult UserTemplateT_ValidateName(string strUserTemplateTName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@UserTemplateTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strUserTemplateTName;

                strStoredProcName = "usp_tbl_UserTemplate_M_Validate_UserTemplateTName";

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

        #region Select EMployee Autocomplete
        /// <summary>
        /// Select all details of UserTemplateT for selected UserTemplateID from tbl_UserTemplate_M table
        /// Created By : NafisaMulla, 14-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intUserTemplateID"></param>
        /// <returns></returns>
        public ApplicationResult Employee_Select_AutocomleteForPayroll(string strSearch,int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@Search", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strSearch;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_Employee_M_ForAutoComplete_For_Payroll";

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

        #region Select Employee Template
        /// <summary>
        /// Select all details of UserTemplateT for selected UserTemplateID from tbl_UserTemplate_M table
        /// Created By : NafisaMulla, 14-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intUserTemplateID"></param>
        /// <returns></returns>
        public ApplicationResult Employee_Select_ForPayItemTemplate(int intEmployeeMID,int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_UserTemplate_M_EMployeeID";

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

        #region Select Employee Template Select For Zero
        /// <summary>
        /// Select all details of UserTemplateT for selected UserTemplateID from tbl_UserTemplate_M table
        /// Created By : NafisaMulla, 14-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intUserTemplateID"></param>
        /// <returns></returns>
        public ApplicationResult EmployeeTemplate_SelectForZero(int intEmployeeMID, int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_UserPayItemZero_M_Select";

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

        #region Select Employee Template Select For Zero
        /// <summary>
        /// Select all details of UserTemplateT for selected UserTemplateID from tbl_UserTemplate_M table
        /// Created By : NafisaMulla, 14-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intUserTemplateID"></param>
        /// <returns></returns>
        public ApplicationResult UserTemplateT_SelectLeaveID_ByEmployeeMID(int intEmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;


                strStoredProcName = "usp_tbl_UserTemplete_M_SelectLeaveID";

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


