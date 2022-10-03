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
    /// Class Created By : Nirmal, 09-02-2016
	/// Summary description for Organisation.
    /// </summary>
	public class LeaveTemplateBl 
	{
		#region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion
		
		
		
		#region Select All LeaveTemplate Details
        /// <summary>
        /// To Select All data from the tbl_LeaveTemplate table
        /// Created By : Nirmal, 09-02-2016
		/// Modified By :
        /// </summary>
		public ApplicationResult  LeaveTemplate_SelectAll()
        {
			try
            {
				sSql = "usp_tbl_LeaveTemplate_SelectAll";
                DataTable dtLeaveTemplate  = new DataTable();
                dtLeaveTemplate = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtLeaveTemplate);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Update DutyLeaveTotalApproval For Approval
        /// <summary>
        /// To Update details of LeaveApproval in tbl_LeaveApproval table
        /// Created By : Nirmal, 11-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult DutyLeaveTotal_Update_ForApproval(LeaveTemplateBo objLeaveTemplateBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];


                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objLeaveTemplateBo.CreatedBy;

                pSqlParameter[1] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objLeaveTemplateBo.LastModifiedBy;

                pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objLeaveTemplateBo.LastModifiedDate;

                pSqlParameter[3] = new SqlParameter("@IsDeleted", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objLeaveTemplateBo.IsDeleted;


                sSql = "usp_tbl_DutyLeaveTemplate_TotalUpdate";

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
                objLeaveTemplateBo = null;
            }
        }
        #endregion

        #region Select LeaveTemplate Details by LeaveTemplateID
        /// <summary>
        /// Select all details of LeaveTemplate for selected LeaveTemplateID from tbl_LeaveTemplate table
        /// Created By : Nirmal, 09-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult LeaveTemplate_Select(int intLeaveTemplateID)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@LeaveTemplateID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intLeaveTemplateID;

				strStoredProcName = "usp_tbl_LeaveTemplate_Select";
				
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

        #region Select LeaveTemplate For Employee Template
        /// <summary>
        /// Select all details of LeaveTemplate for selected LeaveTemplateID from tbl_LeaveTemplate table
        /// Created By : Nirmal, 09-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult LeaveTemplate_SelectForTemplate(int intEmployeeMID,string strAcademicYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                pSqlParameter[1] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strAcademicYear;

                strStoredProcName = "usp_tbl_LeaveTemplate_SelectForTemplate";

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
		
		
		
		#region Delete LeaveTemplate Details by LeaveTemplateID
        /// <summary>
        /// To Delete details of LeaveTemplate for selected LeaveTemplateID from tbl_LeaveTemplate table
        /// Created By : Nirmal, 09-02-2016
		/// Modified By :
        /// </summary>
        public ApplicationResult LeaveTemplate_Delete(LeaveTemplateBo objLeaveTemplateBo)
		{
            try
            {
                pSqlParameter = new SqlParameter[3];
				
				pSqlParameter[0] = new SqlParameter("@LeaveID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objLeaveTemplateBo.LeaveID;

                pSqlParameter[1] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objLeaveTemplateBo.EmployeeMID;

                pSqlParameter[2] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objLeaveTemplateBo.AcademicYear;

				strStoredProcName = "usp_tbl_LeaveTemplate_Delete";
				
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
		
		
		
		#region Insert LeaveTemplate Details
		/// <summary>
        /// To Insert details of LeaveTemplate in tbl_LeaveTemplate table
        /// Created By : Nirmal, 09-02-2016
		/// Modified By :
        /// </summary>
        public ApplicationResult LeaveTemplate_Insert(LeaveTemplateBo objLeaveTemplateBo)
        {
            try
                {
				pSqlParameter = new SqlParameter[9];
                
				
          		pSqlParameter[0] = new SqlParameter("@EmployeeMID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objLeaveTemplateBo.EmployeeMID;
 
				pSqlParameter[1] = new SqlParameter("@LeaveID",SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objLeaveTemplateBo.LeaveID;
 
				pSqlParameter[2] = new SqlParameter("@Total",SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objLeaveTemplateBo.Total;

                pSqlParameter[3] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objLeaveTemplateBo.AcademicYear;
 
				pSqlParameter[4] = new SqlParameter("@IsDeleted",SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objLeaveTemplateBo.IsDeleted;
 
				pSqlParameter[5] = new SqlParameter("@CreatedBy",SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objLeaveTemplateBo.CreatedBy;
 
				pSqlParameter[6] = new SqlParameter("@CreatedDate",SqlDbType.VarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objLeaveTemplateBo.CreatedDate;
 
				pSqlParameter[7] = new SqlParameter("@LastModifiedBy",SqlDbType.Int);
				pSqlParameter[7].Direction = ParameterDirection.Input;
          		pSqlParameter[7].Value = objLeaveTemplateBo.LastModifiedBy;
 
				pSqlParameter[8] = new SqlParameter("@LastModifiedDate",SqlDbType.VarChar);
				pSqlParameter[8].Direction = ParameterDirection.Input;
          		pSqlParameter[8].Value = objLeaveTemplateBo.LastModifiedDate;
 
		
				sSql = "usp_tbl_LeaveTemplate_Insert";
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
                objLeaveTemplateBo = null;
            }
        }
        #endregion
		
		
		
		#region Update LeaveTemplate Details
		/// <summary>
        /// To Update details of LeaveTemplate in tbl_LeaveTemplate table
        /// Created By : Nirmal, 09-02-2016
		/// Modified By :
        /// </summary>
        public ApplicationResult LeaveTemplate_Update(LeaveTemplateBo objLeaveTemplateBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[9];
                
				
          		pSqlParameter[0] = new SqlParameter("@LeaveTemplateID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objLeaveTemplateBo.LeaveTemplateID;
 
				pSqlParameter[1] = new SqlParameter("@EmployeeMID",SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objLeaveTemplateBo.EmployeeMID;
 
				pSqlParameter[2] = new SqlParameter("@LeaveID",SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objLeaveTemplateBo.LeaveID;
 
				pSqlParameter[3] = new SqlParameter("@Total",SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objLeaveTemplateBo.Total;
 
				pSqlParameter[4] = new SqlParameter("@IsDeleted",SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objLeaveTemplateBo.IsDeleted;
 
				pSqlParameter[5] = new SqlParameter("@LastModifiedBy",SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objLeaveTemplateBo.LastModifiedBy;
 
				pSqlParameter[6] = new SqlParameter("@LastModifiedDate",SqlDbType.VarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objLeaveTemplateBo.LastModifiedDate;
 
		
		
				sSql = "usp_tbl_LeaveTemplate_Update";
                                
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
                objLeaveTemplateBo = null;
            }
        }
        #endregion
		
				
		
		#region ValidateName for LeaveTemplate 
        /// <summary>
        /// Function which validates whether the LeaveTemplateName already exits in tbl_LeaveTemplate table.
        /// Created By : Nirmal, 09-02-2016
		/// Modified By :
        /// </summary>
        public ApplicationResult LeaveTemplate_ValidateName(int intID,int intBranchID,string strName)
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
				

				strStoredProcName = "usp_tbl_LeaveTemplate_ValidateName";
				
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
