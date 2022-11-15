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
	public class LeaveApprovalBl 
	{
		#region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion
					
		#region Select All LeaveApproval Details
        /// <summary>
        /// To Select All data from the tbl_LeaveApproval table
        /// Created By : Nirmal, 11-02-2016
		/// Modified By :
        /// </summary>
		public ApplicationResult  LeaveApproval_SelectAll()
        {
			try
            {
				sSql = "usp_tbl_LeaveApproval_SelectAll";
                DataTable dtLeaveApproval  = new DataTable();
                dtLeaveApproval = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtLeaveApproval);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
					
		#region Select LeaveApproval Details by LeaveApprovalID
        /// <summary>
        /// Select all details of LeaveApproval for selected LeaveApprovalID from tbl_LeaveApproval table
        /// Created By : Nirmal, 11-02-2016
		/// Modified By :
        /// </summary>
        public ApplicationResult LeaveApproval_Select(int intLeaveApprovalID)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@LeaveApprovalID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intLeaveApprovalID;

				strStoredProcName = "usp_tbl_LeaveApproval_Select";
				
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

        #region Select LeaveApproval Details by LeaveApprovalID
        /// <summary>
        /// Select all details of LeaveApproval for selected LeaveApprovalID from tbl_LeaveApproval table
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult LeaveApproval_ForReport(int intEmployeeMID, string strYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                pSqlParameter[1] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strYear;

                strStoredProcName = "usp_Rpt_LeaveApprovalReport";

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
		
		#region Delete LeaveApproval Details by LeaveApprovalID
        /// <summary>
        /// To Delete details of LeaveApproval for selected LeaveApprovalID from tbl_LeaveApproval table
        /// Created By : Nirmal, 11-02-2016
		/// Modified By :
        /// </summary>
        public ApplicationResult LeaveApproval_Delete(int intLeaveApprovalID, int intLastModifiedBy, string strLastModifiedDate)
		{
            try
            {
                pSqlParameter = new SqlParameter[3];
				
				pSqlParameter[0] = new SqlParameter("@LeaveApprovalID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intLeaveApprovalID;
											
				pSqlParameter[1] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastModifiedBy;
				
				pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifiedDate;

				strStoredProcName = "usp_tbl_LeaveApproval_Delete";
				
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
					
		#region Insert LeaveApproval Details
		/// <summary>
        /// To Insert details of LeaveApproval in tbl_LeaveApproval table
        /// Created By : Nirmal, 11-02-2016
		/// Modified By :
        /// </summary>
        public ApplicationResult LeaveApproval_Insert(LeaveApprovalBo objLeaveApprovalBo)
        {
            try
            {
				pSqlParameter = new SqlParameter[8];
                
				
          		pSqlParameter[0] = new SqlParameter("@LeaveApplyID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objLeaveApprovalBo.LeaveApplyID;
 
				pSqlParameter[1] = new SqlParameter("@ApplyDate",SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objLeaveApprovalBo.ApplyDate;
 
				pSqlParameter[2] = new SqlParameter("@LeaveID",SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objLeaveApprovalBo.LeaveID;
 
				pSqlParameter[3] = new SqlParameter("@IsHalfDay",SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objLeaveApprovalBo.IsHalfDay;
 
				pSqlParameter[4] = new SqlParameter("@CreatedBy",SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objLeaveApprovalBo.CreatedBy;
 
				pSqlParameter[5] = new SqlParameter("@CreatedDate",SqlDbType.VarChar);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objLeaveApprovalBo.CreatedDate;
 
				pSqlParameter[6] = new SqlParameter("@LastModifiedBy",SqlDbType.Int);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objLeaveApprovalBo.LastModifiedBy;
 
				pSqlParameter[7] = new SqlParameter("@LastModifiedDate",SqlDbType.VarChar);
				pSqlParameter[7].Direction = ParameterDirection.Input;
          		pSqlParameter[7].Value = objLeaveApprovalBo.LastModifiedDate;
		
				sSql = "usp_tbl_LeaveApproval_Insert";

                int dtResult  = DatabaseTransaction.ExecuteNonQuery(CommandType.StoredProcedure, sSql, pSqlParameter);
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
                objLeaveApprovalBo = null;
            }
        }
        #endregion

        #region Insert DutyLeaveApproval Details
        /// <summary>
        /// To Insert details of LeaveApproval in tbl_LeaveApproval table
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult DutyLeaveApproval_Insert(LeaveApprovalBo objDutyLeaveApprovalBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[8];


                pSqlParameter[0] = new SqlParameter("@DutyLeaveApplyID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objDutyLeaveApprovalBo.DutyLeaveApplyID;

                pSqlParameter[1] = new SqlParameter("@ApplyDate", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objDutyLeaveApprovalBo.ApplyDate;

                pSqlParameter[2] = new SqlParameter("@LeaveID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objDutyLeaveApprovalBo.LeaveID;

                pSqlParameter[3] = new SqlParameter("@IsHalfDay", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objDutyLeaveApprovalBo.IsHalfDay;

                pSqlParameter[4] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objDutyLeaveApprovalBo.CreatedBy;

                pSqlParameter[5] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objDutyLeaveApprovalBo.CreatedDate;

                pSqlParameter[6] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objDutyLeaveApprovalBo.LastModifiedBy;

                pSqlParameter[7] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objDutyLeaveApprovalBo.LastModifiedDate;

                sSql = "usp_tbl_DutyLeaveApproval_Insert";

                int dtResult = DatabaseTransaction.ExecuteNonQuery(CommandType.StoredProcedure, sSql, pSqlParameter);
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
                objDutyLeaveApprovalBo = null;
            }
        }
        #endregion

        #region Update LeaveApproval Details
        /// <summary>
        /// To Update details of LeaveApproval in tbl_LeaveApproval table
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult LeaveApproval_Update(LeaveApprovalBo objLeaveApprovalBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[12];
                
				
          		pSqlParameter[0] = new SqlParameter("@LeaveApprovalID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objLeaveApprovalBo.LeaveApprovalID;
 
				pSqlParameter[1] = new SqlParameter("@LeaveApplyID",SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objLeaveApprovalBo.LeaveApplyID;
 
				pSqlParameter[2] = new SqlParameter("@IsApproved",SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objLeaveApprovalBo.IsApproved;
 
				pSqlParameter[3] = new SqlParameter("@ApplyDate",SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objLeaveApprovalBo.ApplyDate;
 
				pSqlParameter[4] = new SqlParameter("@LeaveID",SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objLeaveApprovalBo.LeaveID;
 
				pSqlParameter[5] = new SqlParameter("@IsHalfDay",SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objLeaveApprovalBo.IsHalfDay;
 
				pSqlParameter[6] = new SqlParameter("@NAReason",SqlDbType.VarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objLeaveApprovalBo.NAReason;
 
				pSqlParameter[7] = new SqlParameter("@IsDeleted",SqlDbType.Int);
				pSqlParameter[7].Direction = ParameterDirection.Input;
          		pSqlParameter[7].Value = objLeaveApprovalBo.IsDeleted;
 
				pSqlParameter[8] = new SqlParameter("@LastModifiedBy",SqlDbType.Int);
				pSqlParameter[8].Direction = ParameterDirection.Input;
          		pSqlParameter[8].Value = objLeaveApprovalBo.LastModifiedBy;
 
				pSqlParameter[9] = new SqlParameter("@LastModifiedDate",SqlDbType.VarChar);
				pSqlParameter[9].Direction = ParameterDirection.Input;
          		pSqlParameter[9].Value = objLeaveApprovalBo.LastModifiedDate;
 
		
		
				sSql = "usp_tbl_LeaveApproval_Update";
                                
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
                objLeaveApprovalBo = null;
            }
        }
        #endregion

        #region Update DutyLeaveApproval Details
        /// <summary>
        /// To Update details of LeaveApproval in tbl_LeaveApproval table
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult DutyLeaveApproval_Update(LeaveApprovalBo objLeaveApprovalBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[12];


                pSqlParameter[0] = new SqlParameter("@DutyLeaveApprovalID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objLeaveApprovalBo.LeaveApprovalID;

                pSqlParameter[1] = new SqlParameter("@DutyLeaveApplyID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objLeaveApprovalBo.DutyLeaveApplyID;

                pSqlParameter[2] = new SqlParameter("@IsApproved", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objLeaveApprovalBo.IsApproved;

                pSqlParameter[3] = new SqlParameter("@ApplyDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objLeaveApprovalBo.ApplyDate;

                pSqlParameter[4] = new SqlParameter("@LeaveID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objLeaveApprovalBo.LeaveID;

                pSqlParameter[5] = new SqlParameter("@IsHalfDay", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objLeaveApprovalBo.IsHalfDay;

                pSqlParameter[6] = new SqlParameter("@NAReason", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objLeaveApprovalBo.NAReason;

                pSqlParameter[7] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objLeaveApprovalBo.IsDeleted;

                pSqlParameter[8] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objLeaveApprovalBo.LastModifiedBy;

                pSqlParameter[9] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objLeaveApprovalBo.LastModifiedDate;



                sSql = "usp_tbl_DutyLeaveApproval_Update";

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
                objLeaveApprovalBo = null;
            }
        }
        #endregion

        #region Update LeaveApproval For Approval
        /// <summary>
        /// To Update details of LeaveApproval in tbl_LeaveApproval table
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult LeaveApproval_Update_ForApproval(LeaveApprovalBo objLeaveApprovalBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];


                pSqlParameter[0] = new SqlParameter("@LeaveApprovalID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objLeaveApprovalBo.LeaveApprovalID;

                pSqlParameter[1] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objLeaveApprovalBo.LastModifiedBy;

                pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.NVarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objLeaveApprovalBo.LastModifiedDate;

                pSqlParameter[3] = new SqlParameter("@NAReason", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objLeaveApprovalBo.NAReason;

                sSql = "usp_tbl_LeaveApproval_Update_ForApproval";

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
                objLeaveApprovalBo = null;
            }
        }
        #endregion

        #region Update DutyLeaveApproval For Approval
        /// <summary>
        /// To Update details of LeaveApproval in tbl_LeaveApproval table
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult DutyLeaveApproval_Update_ForApproval(LeaveApprovalBo objLeaveApprovalBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];


                pSqlParameter[0] = new SqlParameter("@DutyLeaveApprovalID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objLeaveApprovalBo.DutyLeaveApprovalID;

                pSqlParameter[1] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objLeaveApprovalBo.LastModifiedBy;

                pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objLeaveApprovalBo.LastModifiedDate;



                sSql = "usp_tbl_DutyLeaveApproval_Update_ForApproval";

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
                objLeaveApprovalBo = null;
            }
        }
        #endregion

        //#region Update DutyLeaveTotalApproval For Approval
        ///// <summary>
        ///// To Update details of LeaveApproval in tbl_LeaveApproval table
        ///// Created By : Nirmal, 11-02-2016
        ///// Modified By :
        ///// </summary>
        //public ApplicationResult DutyLeaveTotal_Update_ForApproval(LeaveApprovalBo objLeaveApprovalBo)
        //{
        //    try
        //    {
        //        pSqlParameter = new SqlParameter[4];


                

        //        pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
        //        pSqlParameter[0].Direction = ParameterDirection.Input;
        //        pSqlParameter[0].Value = objLeaveApprovalBo.CreatedBy;

        //        pSqlParameter[1] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
        //        pSqlParameter[1].Direction = ParameterDirection.Input;
        //        pSqlParameter[1].Value = objLeaveApprovalBo.LastModifiedBy;

        //        pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
        //        pSqlParameter[2].Direction = ParameterDirection.Input;
        //        pSqlParameter[2].Value = objLeaveApprovalBo.LastModifiedDate;

        //        pSqlParameter[3] = new SqlParameter("@IsDeleted", SqlDbType.VarChar);
        //        pSqlParameter[3].Direction = ParameterDirection.Input;
        //        pSqlParameter[3].Value = objLeaveApprovalBo.IsDeleted;


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
        //        objLeaveApprovalBo = null;
        //    }
        //}
        //#endregion

        #region Update LeaveApproval For Reject
        /// <summary>
        /// To Update details of LeaveApproval in tbl_LeaveApproval table
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult LeaveApproval_Update_ForReject(LeaveApprovalBo objLeaveApprovalBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];


                pSqlParameter[0] = new SqlParameter("@LeaveApprovalID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objLeaveApprovalBo.LeaveApprovalID;

                pSqlParameter[1] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objLeaveApprovalBo.LastModifiedBy;

                pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objLeaveApprovalBo.LastModifiedDate;

                pSqlParameter[3] = new SqlParameter("@NAReason", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objLeaveApprovalBo.NAReason;

                sSql = "usp_tbl_LeaveApproval_Update_ForReject";

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
                objLeaveApprovalBo = null;
            }
        }
        #endregion

        #region Update DutyLeaveApproval For Reject
        /// <summary>
        /// To Update details of LeaveApproval in tbl_LeaveApproval table
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult DutyLeaveApproval_Update_ForReject(LeaveApprovalBo objLeaveApprovalBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];


                pSqlParameter[0] = new SqlParameter("@DutyLeaveApprovalID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objLeaveApprovalBo.DutyLeaveApprovalID;

                pSqlParameter[1] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objLeaveApprovalBo.LastModifiedBy;

                pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objLeaveApprovalBo.LastModifiedDate;

                pSqlParameter[3] = new SqlParameter("@NAReason", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objLeaveApprovalBo.NAReason;

                sSql = "usp_tbl_DutyLeaveApproval_Update_ForReject";

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
                objLeaveApprovalBo = null;
            }
        }
        #endregion

        #region ValidateName for LeaveApproval 
        /// <summary>
        /// Function which validates whether the LeaveApprovalName already exits in tbl_LeaveApproval table.
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult LeaveApproval_ValidateName(int intID,int intBranchID,string strName)
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
				

				strStoredProcName = "usp_tbl_LeaveApproval_ValidateName";
				
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

        #region Select Employeeattendance for Employee Monthly Report
        /// <summary>
        /// Select all details of Employeeattendance for selected EmployeeAttandenceMID from tbl_EmployeeAttendance_M table
        /// Created By : Nafisa, 01-06-2015
        /// Modified By :
        /// </summary>
        /// <param name="intEmployeeAttandenceMID"></param>
        /// <returns></returns>
        public ApplicationResult Approved_Leave_Report(int intEmployeeMID, string strFromDate, string strToDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                pSqlParameter[1] = new SqlParameter("@FromDate", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strFromDate;

                pSqlParameter[2] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strToDate;

                strStoredProcName = "usp_rpt_Approved_Leave_Report";

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
