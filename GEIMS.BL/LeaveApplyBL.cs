using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using GEIMS.DataAccess;
using GEIMS.Bo;
using GEIMS.Common;

namespace GEIMS.Bl
{
    /// <summary>
    /// Class Created By : Nirmal, 11-02-2016
    /// Summary description for Organisation.
    /// </summary>
    public class LeaveApplyBl
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        #region Select All LeaveApply Details
        /// <summary>
        /// To Select All data from the tbl_LeaveApply table
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult LeaveApply_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_LeaveApply_SelectAll";
                DataTable dtLeaveApply = new DataTable();
                dtLeaveApply = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtLeaveApply);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select All DutyLeaveApply Details
        /// <summary>
        /// To Select All data from the tbl_LeaveApply table
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult DutyLeaveApply_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_DutyLeaveApply_SelectAll";
                DataTable dtLeaveApply = new DataTable();
                dtLeaveApply = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtLeaveApply);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        //#region Update DutyLeaveTotalApproval For Approval
        ///// <summary>
        ///// To Update details of LeaveApproval in tbl_LeaveApproval table
        ///// Created By : Nirmal, 11-02-2016
        ///// Modified By :
        ///// </summary>
        //public ApplicationResult DutyLeaveTotal_Update_ForApproval(LeaveApprovalBo objLeaveTemplateBo)
        //{
        //    try
        //    {
        //        pSqlParameter = new SqlParameter[4];




        //        pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
        //        pSqlParameter[0].Direction = ParameterDirection.Input;
        //        pSqlParameter[0].Value = objLeaveTemplateBo.CreatedBy;

        //        pSqlParameter[1] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
        //        pSqlParameter[1].Direction = ParameterDirection.Input;
        //        pSqlParameter[1].Value = objLeaveTemplateBo.LastModifiedBy;

        //        pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
        //        pSqlParameter[2].Direction = ParameterDirection.Input;
        //        pSqlParameter[2].Value = objLeaveTemplateBo.LastModifiedDate;

        //        pSqlParameter[3] = new SqlParameter("@IsDeleted", SqlDbType.VarChar);
        //        pSqlParameter[3].Direction = ParameterDirection.Input;
        //        pSqlParameter[3].Value = objLeaveTemplateBo.IsDeleted;


        //        sSql = "usp_tbl_DutyLeaveTemplate_TotalUpdate";

        //        int iResult = DatabaseTransaction.ExecuteNonQuery(CommandType.StoredProcedure, sSql, pSqlParameter);

        //        if (iResult > 0)
        //        {

        //            ApplicationResult objResults = new ApplicationResult();
        //            objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
        //            return objResults;
        //        }
        //        else
        //        {
        //            ApplicationResult objResults = new ApplicationResult();
        //            objResults.status = ApplicationResult.CommonStatusType.FAILURE;
        //            return objResults;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        objLeaveTemplateBo = null;
        //    }
        //}
        //#endregion

        #region Select LeaveApply Details by LeaveApplylID
        /// <summary>
        /// Select all details of LeaveApply for selected LeaveApplylID from tbl_LeaveApply table
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult LeaveApply_Select(int intStatus,int EmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@Status", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStatus;

                pSqlParameter[1] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = EmployeeMID;

                strStoredProcName = "usp_tbl_LeaveApply_Select";

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

        #region Select DutyLeaveApply Details by LeaveApplylID
        /// <summary>
        /// Select all details of LeaveApply for selected LeaveApplylID from tbl_LeaveApply table
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult DutyLeaveApply_Select(int intStatus, int EmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@Status", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStatus;

                pSqlParameter[1] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = EmployeeMID;

                strStoredProcName = "usp_tbl_DutyLeaveApply_Select";

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

        #region Select LeaveApply Details by LeaveApplylID 

        /// <summary>
        /// Select all details of LeaveApply for selected LeaveApplylID from tbl_LeaveApply table
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult LeaveApply_Select_ForPrincipal(int intStatus, int EmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@Status", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStatus;

                pSqlParameter[1] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = EmployeeMID;

                strStoredProcName = "usp_tbl_LeaveApply_Select_ForPrincipal";

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

        #region Select Duty LeaveApply Details by LeaveApplylID

        /// <summary>
        /// Select all details of LeaveApply for selected LeaveApplylID from tbl_LeaveApply table
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult DutyLeaveApply_Select_ForPrincipal(int intStatus, int EmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@Status", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStatus;

                pSqlParameter[1] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = EmployeeMID;

                strStoredProcName = "usp_tbl_DutyLeaveApply_Select_ForPrincipal";

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

        #region Select LeaveApply Details For Approve
        /// <summary>
        /// Select all details of LeaveApply for selected LeaveApplylID from tbl_LeaveApply table
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult LeaveApply_Select_ForApprove(int intLeaveApplyID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@LeaveApplyID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intLeaveApplyID;

                strStoredProcName = "usp_tbl_LeaveApply_Select_ForApprove";

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

        #region Select LeaveApply Details For Approve
        /// <summary>
        /// Select all details of LeaveApply for selected LeaveApplylID from tbl_LeaveApply table
        /// Created By : Nirmal, 22-11-2021
        /// Modified By : Arpit Shah
        /// </summary>
        public ApplicationResult LeaveApply_Select_ForApprove_ApprovedDate(int intLeaveApplyID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@LeaveApplyID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intLeaveApplyID;

                strStoredProcName = "usp_tbl_LeaveApply_Select_ForApprove_ApproveDate";

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

        #region Select DutyLeaveApply Details For Approve
        /// <summary>
        /// Select all details of LeaveApply for selected LeaveApplylID from tbl_LeaveApply table
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult DutyLeaveApply_Select_ForApprove(int intLeaveApplyID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@DutyLeaveApplyID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intLeaveApplyID;

                strStoredProcName = "usp_tbl_DutyLeaveApply_Select_ForApprove";

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

        #region Delete LeaveApply Details by LeaveApplylID
        /// <summary>
        /// To Delete details of LeaveApply for selected LeaveApplylID from tbl_LeaveApply table
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult LeaveApply_Delete(int intLeaveApplylID, int intLastModifiedBy, string strLastModifiedDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@LeaveApplylID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intLeaveApplylID;

                pSqlParameter[1] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastModifiedBy;

                pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifiedDate;

                strStoredProcName = "usp_tbl_LeaveApply_Delete";

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

        #region Delete DutyLeaveApply Details by LeaveApplylID
        /// <summary>
        /// To Delete details of LeaveApply for selected LeaveApplylID from tbl_LeaveApply table
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult DutyLeaveApply_Delete(int intLeaveApplylID, int intLastModifiedBy, string strLastModifiedDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@DutyLeaveApplylID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intLeaveApplylID;

                pSqlParameter[1] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastModifiedBy;

                pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifiedDate;

                strStoredProcName = "usp_tbl_DutyLeaveApply_Delete";

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

        #region Insert LeaveApply Details
        /// <summary>
        /// To Insert details of LeaveApply in tbl_LeaveApply table
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult LeaveApply_Insert(LeaveApplyBo objLeaveApplyBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[10];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objLeaveApplyBo.EmployeeMID;

                pSqlParameter[1] = new SqlParameter("@FromDate", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objLeaveApplyBo.FromDate;

                pSqlParameter[2] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objLeaveApplyBo.ToDate;

                pSqlParameter[3] = new SqlParameter("@Reason", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objLeaveApplyBo.Reason;

                pSqlParameter[4] = new SqlParameter("@TotalDays", SqlDbType.Float);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objLeaveApplyBo.TotalDays;

                pSqlParameter[5] = new SqlParameter("@ReportingTo", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objLeaveApplyBo.ReportingTo;

                pSqlParameter[6] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objLeaveApplyBo.CreatedBy;

                pSqlParameter[7] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objLeaveApplyBo.CreatedDate;

                pSqlParameter[8] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objLeaveApplyBo.LastModifiedBy;

                pSqlParameter[9] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objLeaveApplyBo.LastModifiedDate;

                sSql = "usp_tbl_LeaveApply_Insert";

                DataTable dtResult = new DataTable();
                dtResult = DatabaseTransaction.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
                //int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, sSql, pSqlParameter);

                //if (iResult > 0)
                //{
                //    ApplicationResult objResults = new ApplicationResult();
                //    objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                //    return objResults;
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
                objLeaveApplyBo = null;
            }
        }
        #endregion

        #region Insert DutyLeaveApply Details
        /// <summary>
        /// To Insert details of LeaveApply in tbl_LeaveApply table
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult DutyLeaveApply_Insert(LeaveApplyBo objLeaveApplyBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[10];


                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objLeaveApplyBo.EmployeeMID;

                pSqlParameter[1] = new SqlParameter("@FromDate", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objLeaveApplyBo.FromDate;

                pSqlParameter[2] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objLeaveApplyBo.ToDate;

                pSqlParameter[3] = new SqlParameter("@Reason", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objLeaveApplyBo.Reason;

                pSqlParameter[4] = new SqlParameter("@TotalDays", SqlDbType.Float);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objLeaveApplyBo.TotalDays;

                pSqlParameter[5] = new SqlParameter("@ReportingTo", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objLeaveApplyBo.ReportingTo;

                pSqlParameter[6] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objLeaveApplyBo.CreatedBy;

                pSqlParameter[7] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objLeaveApplyBo.CreatedDate;

                pSqlParameter[8] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objLeaveApplyBo.LastModifiedBy;

                pSqlParameter[9] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objLeaveApplyBo.LastModifiedDate;

                sSql = "usp_tbl_DutyLeaveApply_Insert";

                DataTable dtResult = new DataTable();
                dtResult = DatabaseTransaction.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
                //int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, sSql, pSqlParameter);

                //if (iResult > 0)
                //{
                //    ApplicationResult objResults = new ApplicationResult();
                //    objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                //    return objResults;
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
                objLeaveApplyBo = null;
            }
        }
        #endregion

        #region Update LeaveApply Details
        /// <summary>
        /// To Update details of LeaveApply in tbl_LeaveApply table
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult LeaveApply_Update(LeaveApplyBo objLeaveApplyBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[13];


                pSqlParameter[0] = new SqlParameter("@LeaveApplylID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objLeaveApplyBo.LeaveApplylID;

                pSqlParameter[1] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objLeaveApplyBo.EmployeeMID;

                pSqlParameter[2] = new SqlParameter("@FromDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objLeaveApplyBo.FromDate;

                pSqlParameter[3] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objLeaveApplyBo.ToDate;

                pSqlParameter[4] = new SqlParameter("@Reason", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objLeaveApplyBo.Reason;

                pSqlParameter[5] = new SqlParameter("@ApprovedBy", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objLeaveApplyBo.ApprovedBy;

                pSqlParameter[6] = new SqlParameter("@ApprovedDate", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objLeaveApplyBo.ApprovedDate;

                pSqlParameter[7] = new SqlParameter("@TotalDays", SqlDbType.Float);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objLeaveApplyBo.TotalDays;

                pSqlParameter[8] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objLeaveApplyBo.IsDeleted;

                pSqlParameter[9] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objLeaveApplyBo.LastModifiedBy;

                pSqlParameter[10] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objLeaveApplyBo.LastModifiedDate;



                sSql = "usp_tbl_LeaveApply_Update";

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
                objLeaveApplyBo = null;
            }
        }
        #endregion

        #region Update DutyLeaveApply Details
        /// <summary>
        /// To Update details of LeaveApply in tbl_LeaveApply table
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult DutyLeaveApply_Update(LeaveApplyBo objLeaveApplyBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[13];


                pSqlParameter[0] = new SqlParameter("@DutyLeaveApplylID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objLeaveApplyBo.LeaveApplylID;

                pSqlParameter[1] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objLeaveApplyBo.EmployeeMID;

                pSqlParameter[2] = new SqlParameter("@FromDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objLeaveApplyBo.FromDate;

                pSqlParameter[3] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objLeaveApplyBo.ToDate;

                pSqlParameter[4] = new SqlParameter("@Reason", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objLeaveApplyBo.Reason;

                pSqlParameter[5] = new SqlParameter("@ApprovedBy", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objLeaveApplyBo.ApprovedBy;

                pSqlParameter[6] = new SqlParameter("@ApprovedDate", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objLeaveApplyBo.ApprovedDate;

                pSqlParameter[7] = new SqlParameter("@TotalDays", SqlDbType.Float);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objLeaveApplyBo.TotalDays;

                pSqlParameter[8] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objLeaveApplyBo.IsDeleted;

                pSqlParameter[9] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objLeaveApplyBo.LastModifiedBy;

                pSqlParameter[10] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objLeaveApplyBo.LastModifiedDate;



                sSql = "usp_tbl_DutyLeaveApply_Update";

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
                objLeaveApplyBo = null;
            }
        }
        #endregion

        #region Update LeaveApply For Approval
        /// <summary>
        /// To Update details of LeaveApply in tbl_LeaveApply table
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult LeaveApply_Update_ForApproval(LeaveApplyBo objLeaveApplyBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];


                pSqlParameter[0] = new SqlParameter("@LeaveApplylID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objLeaveApplyBo.LeaveApplylID;

                pSqlParameter[1] = new SqlParameter("@ApprovedDate", SqlDbType.NVarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objLeaveApplyBo.ApprovedDate;

                pSqlParameter[2] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objLeaveApplyBo.LastModifiedBy;

                pSqlParameter[3] = new SqlParameter("@LastModifiedDate", SqlDbType.NVarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objLeaveApplyBo.LastModifiedDate;


                sSql = "usp_tbl_LeaveApply_Update_ForApproval";

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
                objLeaveApplyBo = null;
            }
        }
        #endregion

        #region Update DutyLeaveApply For Approval
        /// <summary>
        /// To Update details of LeaveApply in tbl_LeaveApply table
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult DutyLeaveApply_Update_ForApproval(LeaveApplyBo objLeaveApplyBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];


                pSqlParameter[0] = new SqlParameter("@DutyLeaveApplylID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objLeaveApplyBo.DutyLeaveApplylID;

                pSqlParameter[1] = new SqlParameter("@ApprovedDate", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objLeaveApplyBo.ApprovedDate;

                pSqlParameter[2] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objLeaveApplyBo.LastModifiedBy;

                pSqlParameter[3] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objLeaveApplyBo.LastModifiedDate;


                sSql = "usp_tbl_DutyLeaveApply_Update_ForApproval";

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
                objLeaveApplyBo = null;
            }
        }
        #endregion

        #region ValidateName for LeaveApply
        /// <summary>
        /// Function which validates whether the LeaveApplyName already exits in tbl_LeaveApply table.
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult LeaveApply_ValidateName(int intID, int intBranchID, string strName)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@ID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intID;


                pSqlParameter[1] = new SqlParameter("@BranchID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intBranchID;

                pSqlParameter[2] = new SqlParameter("@Name", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strName;


                strStoredProcName = "usp_tbl_LeaveApply_ValidateName";

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

        #region ValidateName for LeaveApply
        /// <summary>
        /// Function which validates whether the LeaveApplyName already exits in tbl_LeaveApply table.
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult LeaveApply_Validation(int inttrustID, int intschoolMID, int intemployeeID, string strfdate)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@TrustID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = inttrustID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intschoolMID;

                pSqlParameter[2] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intemployeeID;

                pSqlParameter[3] = new SqlParameter("@Date", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strfdate;

                strStoredProcName = "usp_tbl_LeaveApply_Validation";

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

        #region ValidateName for DutyLeaveApply
        /// <summary>
        /// Function which validates whether the LeaveApplyName already exits in tbl_LeaveApply table.
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult DutyLeaveApply_ValidateName(int intID, int intBranchID, string strName)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@ID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intID;


                pSqlParameter[1] = new SqlParameter("@BranchID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intBranchID;

                pSqlParameter[2] = new SqlParameter("@Name", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strName;


                strStoredProcName = "usp_tbl_DutyLeaveApply_ValidateName";

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
