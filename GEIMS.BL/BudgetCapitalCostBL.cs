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
    public class BudgetCapitalCostBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        //Note Use this Method for CapitalCostReport.aspx and CapitalCost.aspx

        #region Budget Heading for DropDown      
        public ApplicationResult BudgetHeading_SelectDropDown()
        {
            try
            {
                sSql = "usp_tbl_BudgetCapitalCost";
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

        #region Select BudgetSubHeading Details by BudgetHeadingID         
        public ApplicationResult BudgetSubHeading_SelectDropdown(int BudgetSubHeadingMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = BudgetSubHeadingMID;

                strStoredProcName = "BudgetCapitalCost_SelectDropDownById";

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

        #region UOM
        public ApplicationResult BudgetUOM_DropDown()
        {
            try
            {
                sSql = "BudgetCapitalCost_UOM";
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

        //Note Use this Method for CapitalCostReport.aspx

        #region Insert BudgetSubHeading Details
        public ApplicationResult BudgetCapitalCost_Insert(BudgetCapitalCostBO objBudgetCapitalCostBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[15];

                pSqlParameter[0] = new SqlParameter("@CapitalCostId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objBudgetCapitalCostBO.CapitalCostId;

                pSqlParameter[1] = new SqlParameter("@SectionMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objBudgetCapitalCostBO.SectionMID;

                pSqlParameter[2] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objBudgetCapitalCostBO.TrustMID;

                pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objBudgetCapitalCostBO.SchoolMID;

                pSqlParameter[4] = new SqlParameter("@BudgetCategoryMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objBudgetCapitalCostBO.BudgetCategoryMID;

                pSqlParameter[5] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objBudgetCapitalCostBO.BudgetHeadingMID;

                pSqlParameter[6] = new SqlParameter("@BudgetSubHeadingMID", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objBudgetCapitalCostBO.BudgetSubHeadingMID;

                pSqlParameter[7] = new SqlParameter("@Quantity", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objBudgetCapitalCostBO.Quantity;

                pSqlParameter[8] = new SqlParameter("@UOMID", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objBudgetCapitalCostBO.UOMID;

                pSqlParameter[9] = new SqlParameter("@RatePerUnit", SqlDbType.Decimal);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objBudgetCapitalCostBO.RatePerUnit;

                pSqlParameter[10] = new SqlParameter("@TotalAmount", SqlDbType.Decimal);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objBudgetCapitalCostBO.TotalAmount;

                pSqlParameter[11] = new SqlParameter("@CurrentYear", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objBudgetCapitalCostBO.CurrentYear;

                pSqlParameter[12] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objBudgetCapitalCostBO.IsDeleted;

                pSqlParameter[13] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objBudgetCapitalCostBO.CreatedBy;

                pSqlParameter[14] = new SqlParameter("@CreatedDate", SqlDbType.Date);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objBudgetCapitalCostBO.CreatedDate;

                sSql = "usp_tbl_BudgetCapitalCost_M_Insert";
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
                objBudgetCapitalCostBO = null;
            }
        }
        #endregion

        #region Delete BudgetSubHeading by BudgetSubHeadingMID
        public ApplicationResult BudgetCapitalCost_Delete(int intCapitalCostId)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@CapitalCostId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intCapitalCostId;

                strStoredProcName = "usp_tbl_BudgetCapitalCost_M_Delete";

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

        #region CapitalCost Select All    
        public ApplicationResult BudgetCapital_SelectAll(int intSchoolMID, int intTrustMID, string strCurrentYear)
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

                sSql = "usp_tbl_BudgetCapitalCost_M_SelectAll";               

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
        }
        #endregion

        #region Select BudgetCapitalCost Details by BudgetCapitalCost_M_SelectById              
        public ApplicationResult BudgetCapitalCost_M_SelectById(int CapitalCostId)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@CapitalCostId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = CapitalCostId;

                strStoredProcName = "usp_tbl_BudgetCapitalCost_M_SelectById";

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

        #region Update BudgetCapitalCost Details    
        public ApplicationResult BudgetCapitalCost_Update(BudgetCapitalCostBO objBudgetCapitalCostBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[10];

                pSqlParameter[0] = new SqlParameter("@CapitalCostId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objBudgetCapitalCostBO.CapitalCostId;

                pSqlParameter[1] = new SqlParameter("@SectionMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objBudgetCapitalCostBO.SectionMID;

                pSqlParameter[2] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objBudgetCapitalCostBO.TrustMID;

                pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objBudgetCapitalCostBO.SchoolMID;

                //pSqlParameter[3] = new SqlParameter("@BudgetCategoryMID", SqlDbType.Int);
                //pSqlParameter[3].Direction = ParameterDirection.Input;
                //pSqlParameter[3].Value = objBudgetCapitalCostBO.BudgetCategoryMID;

                //pSqlParameter[4] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int);
                //pSqlParameter[4].Direction = ParameterDirection.Input;
                //pSqlParameter[4].Value = objBudgetCapitalCostBO.BudgetHeadingMID;

                //pSqlParameter[5] = new SqlParameter("@BudgetSubHeadingMID", SqlDbType.Int);
                //pSqlParameter[5].Direction = ParameterDirection.Input;
                //pSqlParameter[5].Value = objBudgetCapitalCostBO.BudgetSubHeadingMID;

                pSqlParameter[4] = new SqlParameter("@Quantity", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objBudgetCapitalCostBO.Quantity;

                pSqlParameter[5] = new SqlParameter("@UOMID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objBudgetCapitalCostBO.UOMID;

                pSqlParameter[6] = new SqlParameter("@RatePerUnit", SqlDbType.Decimal);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objBudgetCapitalCostBO.RatePerUnit;

                pSqlParameter[7] = new SqlParameter("@TotalAmount", SqlDbType.Decimal);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objBudgetCapitalCostBO.TotalAmount;

                pSqlParameter[8] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objBudgetCapitalCostBO.ModifiedBy;

                pSqlParameter[9] = new SqlParameter("@ModifiedDate", SqlDbType.Date);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objBudgetCapitalCostBO.ModifiedDate;

                sSql = "usp_tbl_BudgetCapitalCost_M_Update";
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
                objBudgetCapitalCostBO = null;
            }
        }
        #endregion

        #region ValidateName for SubHeading
        public ApplicationResult BudgetCapitalCostSubHeadingM_ValidateName(int intCapitalCostId, int intTrustMID,int intsectionMID, int intSchoolMID,int intcatMID, int intheadMID, int intsubheadMID, string strCurrentYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[8];

                pSqlParameter[0] = new SqlParameter("@CapitalCostId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intCapitalCostId;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                pSqlParameter[3] = new SqlParameter("@SectionMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intsectionMID;

                pSqlParameter[4] = new SqlParameter("@BudgetCategoryMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intcatMID;

                pSqlParameter[5] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = intheadMID;

                pSqlParameter[6] = new SqlParameter("@BudgetSubHeadingMID", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = intsubheadMID;
            
                pSqlParameter[7] = new SqlParameter("@CurrentYear", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = strCurrentYear;

                strStoredProcName = "usp_tbl_BudgetCapitalCostSubHeadingM_ValidateName";

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

        //#region ValidateName for SubHeading
        //public ApplicationResult SubHeadingM_ValidateName(int intBudgetSubHeadingMID, string strSubHeadingName)
        //{
        //    try
        //    {
        //        pSqlParameter = new SqlParameter[2];

        //        pSqlParameter[0] = new SqlParameter("@BudgetSubHeadingMID", SqlDbType.Int);
        //        pSqlParameter[0].Direction = ParameterDirection.Input;
        //        pSqlParameter[0].Value = intBudgetSubHeadingMID;

        //        pSqlParameter[1] = new SqlParameter("@SubHeadingName", SqlDbType.NVarChar);
        //        pSqlParameter[1].Direction = ParameterDirection.Input;
        //        pSqlParameter[1].Value = strSubHeadingName;

        //        strStoredProcName = "usp_tbl_BudgetSubHeading_M_ValidateName";

        //        DataTable dtResult = new DataTable();
        //        dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
        //        ApplicationResult objResults = new ApplicationResult(dtResult);
        //        objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
        //        return objResults;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //#endregion
    }
}
