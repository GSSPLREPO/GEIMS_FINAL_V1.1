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
    public class UserPayItemTemplateTBl
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion


        #region Select All UserPayItemTemplateT Details
        /// <summary>
        /// To Select All data from the tbl_UserPayItemTemplate_T table
        /// Created By : NafisaMulla, 14-04-2015
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult UserPayItemTemplateT_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_UserPayItemTemplate_T_SelectAll";
                DataTable dtUserPayItemTemplateT = new DataTable();
                dtUserPayItemTemplateT = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtUserPayItemTemplateT);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select UserPayItemTemplateT Details by UserPayItemTemplateID
        /// <summary>
        /// Select all details of UserPayItemTemplateT for selected UserPayItemTemplateID from tbl_UserPayItemTemplate_T table
        /// Created By : NafisaMulla, 14-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intUserPayItemTemplateID"></param>
        /// <returns></returns>
        public ApplicationResult UserPayItemTemplateT_Select(int intUserPayItemTemplateID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@UserPayItemTemplateID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intUserPayItemTemplateID;

                strStoredProcName = "usp_tbl_UserPayItemTemplate_T_Select";

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

        #region Delete UserPayItemTemplateT Details by UserPayItemTemplateID
        /// <summary>
        /// To Delete details of UserPayItemTemplateT for selected UserPayItemTemplateID from tbl_UserPayItemTemplate_T table
        /// Created By : NafisaMulla, 14-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intUserPayItemTemplateID"></param>
        /// <returns></returns>
        public ApplicationResult UserPayItemTemplateT_Delete(int intUserPayItemTemplateID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@UserPayItemTemplateID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intUserPayItemTemplateID;

                strStoredProcName = "usp_tbl_UserPayItemTemplate_T_Delete";

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

        #region Insert UserPayItemTemplateT Details
        /// <summary>
        /// To Insert details of UserPayItemTemplateT in tbl_UserPayItemTemplate_T table
        /// Created By : NafisaMulla, 14-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="objUserPayItemTemplateTBO"></param>
        /// <returns></returns>
        public ApplicationResult UserPayItemTemplateT_Insert(UserPayItemTemplateTBo objUserPayItemTemplateTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[13];


                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objUserPayItemTemplateTBO.TrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objUserPayItemTemplateTBO.SchoolMID;

                pSqlParameter[2] = new SqlParameter("@UserTemplateID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objUserPayItemTemplateTBO.UserTemplateID;

                pSqlParameter[3] = new SqlParameter("@PayItem", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objUserPayItemTemplateTBO.PayItem;

                pSqlParameter[4] = new SqlParameter("@DependsOn", SqlDbType.NVarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objUserPayItemTemplateTBO.DependsOn;

                pSqlParameter[5] = new SqlParameter("@Type", SqlDbType.NVarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objUserPayItemTemplateTBO.Type;

                pSqlParameter[6] = new SqlParameter("@Percentage", SqlDbType.Float);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objUserPayItemTemplateTBO.Percentage;

                pSqlParameter[7] = new SqlParameter("@Amount", SqlDbType.Float);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objUserPayItemTemplateTBO.Amount;

                pSqlParameter[8] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objUserPayItemTemplateTBO.IsDeleted;

                pSqlParameter[9] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objUserPayItemTemplateTBO.CreatedUserID;

                pSqlParameter[10] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objUserPayItemTemplateTBO.CreatedDate;

                pSqlParameter[11] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objUserPayItemTemplateTBO.LastModifiedUserID;

                pSqlParameter[12] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objUserPayItemTemplateTBO.LastModifiedDate;


                sSql = "usp_tbl_UserPayItemTemplate_T_Insert";
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
                objUserPayItemTemplateTBO = null;
            }
        }
        #endregion

        #region Update UserPayItemTemplateT Details
        /// <summary>
        /// To Update details of UserPayItemTemplateT in tbl_UserPayItemTemplate_T table
        /// Created By : NafisaMulla, 14-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="objUserPayItemTemplateTBO"></param>
        /// <returns></returns>
        public ApplicationResult UserPayItemTemplateT_Update(UserPayItemTemplateTBo objUserPayItemTemplateTBO,double dbGrosssalary)
        {
            try
            {
                pSqlParameter = new SqlParameter[10];


                pSqlParameter[0] = new SqlParameter("@UserPayItemTemplateID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objUserPayItemTemplateTBO.UserPayItemTemplateID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objUserPayItemTemplateTBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objUserPayItemTemplateTBO.SchoolMID;

                pSqlParameter[3] = new SqlParameter("@UserTemplateID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objUserPayItemTemplateTBO.UserTemplateID;

                pSqlParameter[4] = new SqlParameter("@PayItem", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objUserPayItemTemplateTBO.PayItem;
             
                pSqlParameter[5] = new SqlParameter("@Percentage", SqlDbType.Float);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objUserPayItemTemplateTBO.Percentage;

                pSqlParameter[6] = new SqlParameter("@Amount", SqlDbType.Float);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objUserPayItemTemplateTBO.Amount;

                pSqlParameter[7] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objUserPayItemTemplateTBO.LastModifiedUserID;

                pSqlParameter[8] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objUserPayItemTemplateTBO.LastModifiedDate;

                pSqlParameter[9] = new SqlParameter("@Grosssalary", SqlDbType.Float);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = dbGrosssalary;


                sSql = "usp_tbl_UserPayItemTemplate_T_Update";
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
                objUserPayItemTemplateTBO = null;
            }
        }
        #endregion




        #region Select UserPayItemTemplateT Details by UserPayItemTemplateTName
        /// <summary>
        /// Select all details of UserPayItemTemplateT for selected UserPayItemTemplateTName from tbl_UserPayItemTemplate_T table
        /// Created By : NafisaMulla, 14-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="UserPayItemTemplateTName"></param>
        /// <returns></returns>
        public ApplicationResult UserPayItemTemplateT_Select_byUserPayItemTemplateTName(string strUserPayItemTemplateTName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@UserPayItemTemplateTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strUserPayItemTemplateTName;

                strStoredProcName = "usp_tbl_UserPayItemTemplate_T_Select_ByUserPayItemTemplateT";

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


        #region ValidateName for UserPayItemTemplateT
        /// <summary>
        /// Function which validates whether the UserPayItemTemplateTName already exits in tbl_UserPayItemTemplate_T table.
        /// Created By : NafisaMulla, 14-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="strUserPayItemTemplateTName"></param>
        /// <returns></returns>
        public ApplicationResult UserPayItemTemplateT_ValidateName(string strUserPayItemTemplateTName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@UserPayItemTemplateTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strUserPayItemTemplateTName;

                strStoredProcName = "usp_tbl_UserPayItemTemplate_T_Validate_UserPayItemTemplateTName";

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

        #region Select Employee  For PayItem
        /// <summary>
        /// Select all details of UserTemplateT for selected UserTemplateID from tbl_UserTemplate_M table
        /// Created By : NafisaMulla, 14-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intUserTemplateID"></param>
        /// <returns></returns>
        public ApplicationResult Employee_Select_ByEmployeeID(int intEmployeeMID, int intTrustMID, int intSchoolMID)
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

                strStoredProcName = "usp_tbl_UserTemplate_M_UserID_Select";

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

        #region Select Employee Template For PayItem
        /// <summary>
        /// Select all details of UserTemplateT for selected UserTemplateID from tbl_UserTemplate_M table
        /// Created By : NafisaMulla, 14-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intUserTemplateID"></param>
        /// <returns></returns>
        public ApplicationResult EmployeeTemplate_Select_ByEmployeeID(int intEmployeeMID, int intTrustMID, int intSchoolMID)
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

                strStoredProcName = "usp_tbl_UserTemplate_M_UserID_TemplateSelect";

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

        #region EmployeeTemplate_Select_ByEmployeeID
        /// <summary>
        /// Select all details of UserTemplateT for selected UserTemplateID from tbl_UserTemplate_M table
        /// Created By : NafisaMulla, 14-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intUserTemplateID"></param>
        /// <returns></returns>
        public ApplicationResult EmployeeTemplate_Select_ByEmployeeID(int intEmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;


                strStoredProcName = "usp_tbl_UserPayItem_M_SelectForEarning";

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

        #region EmployeeTemplate_SelectForEarning
        /// <summary>
        /// Select all details of UserTemplateT for selected UserTemplateID from tbl_UserTemplate_M table
        /// Created By : NafisaMulla, 14-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intUserTemplateID"></param>
        /// <returns></returns>
        public ApplicationResult EmployeeTemplate_SelectForEarning(int intEmployeeMID,int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;


                strStoredProcName = "usp_tbl_UserPayItem_M_SelectForEarning";

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

        #region usp_tbl_UserEarnedDays_Select
        /// <summary>
        /// Select all details of UserTemplateT for selected UserTemplateID from tbl_UserTemplate_M table
        /// Created By : NafisaMulla, 14-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intUserTemplateID"></param>
        /// <returns></returns>
        public ApplicationResult EmployeeTemplate_SelectEarnDays(int intEmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;


                strStoredProcName = "usp_tbl_UserEarnedDays_Select";

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


