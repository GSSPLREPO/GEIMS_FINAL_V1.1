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
    public class BudgetEntryScreenMBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        #region Insert BudgetSubHeading Details
        public ApplicationResult BudgetEntryScreenM_Insert(BudgetEntryScreenMBO objBudgetEntryScreenMBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[12];

                pSqlParameter[0] = new SqlParameter("@BudgetScreenId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objBudgetEntryScreenMBO.BudgetScreenId;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objBudgetEntryScreenMBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objBudgetEntryScreenMBO.SchoolMID;
            
                pSqlParameter[3] = new SqlParameter("@CurrentYear", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objBudgetEntryScreenMBO.CurrentYear;

                pSqlParameter[4] = new SqlParameter("@BudgetCategoryMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objBudgetEntryScreenMBO.BudgetCategoryMID;

                pSqlParameter[5] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objBudgetEntryScreenMBO.BudgetHeadingMID;

                pSqlParameter[6] = new SqlParameter("@BudgetSubHeadingMID", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objBudgetEntryScreenMBO.BudgetSubHeadingMID;

                pSqlParameter[7] = new SqlParameter("@AdminGeneralMGTRole", SqlDbType.Decimal);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objBudgetEntryScreenMBO.AdminGeneralMGTRole;

                pSqlParameter[8] = new SqlParameter("@TotalAmount", SqlDbType.Decimal);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objBudgetEntryScreenMBO.TotalAmount;

                pSqlParameter[9] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objBudgetEntryScreenMBO.IsDeleted;

                pSqlParameter[10] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objBudgetEntryScreenMBO.CreatedBy;

                pSqlParameter[11] = new SqlParameter("@CreatedDate", SqlDbType.Date);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objBudgetEntryScreenMBO.CreatedDate;

                sSql = "usp_tbl_BudgetEntryScreen_M_Insert";
                int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, sSql, pSqlParameter);

                ////DataTable dtResult = new DataTable();
                ////dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);
                ////ApplicationResult objResults = new ApplicationResult(dtResult);
                ////objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                ////return objResults;

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
                objBudgetEntryScreenMBO = null;
            }
        }
        #endregion

        #region Get BudgetScreenId
        public ApplicationResult BudgetScreenId_Last()
        {
            try
            {
                sSql = "usp_tbl_BudgetScreenId";
                DataTable dtDepartment = new DataTable();
                dtDepartment = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtDepartment);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Budget Entry Screen Select All with Pivot Function Static
        public ApplicationResult BudgetEntryScreen_SelectAll(int intSchoolMID, int intTrustMID, string strCurrentYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@CurrentYear", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strCurrentYear;

                //sSql = "usp_tbl_BudgetEntryScreen_SelectAll";

                sSql = "usp_tbl_BudgetEntryScreen_SelectAll_Temp";
                DataTable dtDepartment = new DataTable();
                dtDepartment = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtDepartment);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Budget Entry Screen Select All with Pivot Function Dynamic
        public ApplicationResult BudgetEntryScreen_SelectAll_Dynamic(int intSchoolMID, int intTrustMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                sSql = "usp_tbl_BudgetEntryScreen_SelectAll_Temp";
                DataTable dtDepartment = new DataTable();
                dtDepartment = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtDepartment);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Delete BudgetHeading by BudgetHeadingMID

        //tbl_BudgetEntry_M
        public ApplicationResult BudgetEntry_M_Delete(int intBudgetScreenId)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@BudgetScreenId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intBudgetScreenId;

                strStoredProcName = "usp_tbl_BudgetEntry_M_Delete";

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

        //tbl_BudgetEntry_T
        public ApplicationResult BudgetEntry_T_Delete(int intBudgetScreenId)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@BudgetScreenId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intBudgetScreenId;

                strStoredProcName = "usp_tbl_BudgetEntry_T_Delete";

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

        #region Select BudgetScreen Details by BudgetScreenId              
        public ApplicationResult BudgetScreen_M_SelectById(int BudgetScreenId)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@BudgetScreenId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = BudgetScreenId;

                strStoredProcName = "usp_tbl_BudgetScreen_M_SelectById";

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

        #region ValidateName for SubHeading
        public ApplicationResult BudgetEntrySubHeadingM_ValidateName(int intBudgetScreenId, int intTrustMID, int intSchoolMID, int intcatMID, int intheadMID, int intsubheadMID, string strCurrentYear)
        {
            try
            {
                //pSqlParameter = new SqlParameter[5];

                //pSqlParameter[0] = new SqlParameter("@BudgetScreenId", SqlDbType.Int);
                //pSqlParameter[0].Direction = ParameterDirection.Input;
                //pSqlParameter[0].Value = intBudgetScreenId;

                //pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                //pSqlParameter[1].Direction = ParameterDirection.Input;
                //pSqlParameter[1].Value = intTrustMID;

                //pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                //pSqlParameter[2].Direction = ParameterDirection.Input;
                //pSqlParameter[2].Value = intSchoolMID;

                //pSqlParameter[3] = new SqlParameter("@CurrentYear", SqlDbType.VarChar);
                //pSqlParameter[3].Direction = ParameterDirection.Input;
                //pSqlParameter[3].Value = stringCurrentYear;

                //pSqlParameter[4] = new SqlParameter("@BudgetSubHeadingMID", SqlDbType.NVarChar);
                //pSqlParameter[4].Direction = ParameterDirection.Input;
                //pSqlParameter[4].Value = intBudgetSubHeadingMID;

                pSqlParameter = new SqlParameter[7];

                pSqlParameter[0] = new SqlParameter("@BudgetScreenId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intBudgetScreenId;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                pSqlParameter[3] = new SqlParameter("@BudgetCategoryMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intcatMID;

                pSqlParameter[4] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intheadMID;

                pSqlParameter[5] = new SqlParameter("@BudgetSubHeadingMID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = intsubheadMID;

                pSqlParameter[6] = new SqlParameter("@CurrentYear", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = strCurrentYear;

                strStoredProcName = "usp_tbl_BudgetEntryScreen_M_ValidateName";

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

        #region Budget Category not display in"Capital Cost" for DropDown      
        public ApplicationResult BudgetEntry_Heading_SelectDropDown()
        {
            try
            {
                sSql = "usp_tbl_BudgetEntryScreen_M_SelectDropDown";
                DataTable dtDepartment = new DataTable();
                dtDepartment = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtDepartment);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Update 
        public ApplicationResult BudgetEntryScreenM_Update(BudgetEntryScreenMBO objBudgetEntryScreenMBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@BudgetScreenId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objBudgetEntryScreenMBO.BudgetScreenId;

                pSqlParameter[1] = new SqlParameter("@AdminGeneralMGTRole", SqlDbType.Decimal);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objBudgetEntryScreenMBO.AdminGeneralMGTRole;

                pSqlParameter[2] = new SqlParameter("@TotalAmount", SqlDbType.Decimal);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objBudgetEntryScreenMBO.TotalAmount;

                sSql = "usp_tbl_BudgetEntryScreenM_Update";
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
                objBudgetEntryScreenMBO = null;
            }
        }
        #endregion
    }
}
