using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEIMS.BO;
using GEIMS.Common;
using GEIMS.DataAccess;

namespace GEIMS.BL
{
   public class StudentMarksBL
    {
       #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion
		
		
		
		#region Select All StudentMarks Details
        /// <summary>
        /// To Select All data from the tbl_StudentMarks_M table
        /// Created By : Vishal, 1/12/2016
		/// Modified By :
        /// </summary>
		public ApplicationResult  StudentMarks_SelectAll()
        {
			try
            {
				sSql = "usp_tbl_tbl_StudentMarks_M_SelectAll";
                DataTable dtStudentMarks  = new DataTable();
                dtStudentMarks = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtStudentMarks);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
		
		
		
		#region Select StudentMarks Details by StudentMarksId
        /// <summary>
        /// Select all details of StudentMarks for selected StudentMarksId from tbl_StudentMarks_M table
        /// Created By : Vishal, 1/12/2016
		/// Modified By :
        /// </summary>
        public ApplicationResult StudentMarks_Select(int intStudentMarksId)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@StudentMarksId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentMarksId;

				strStoredProcName = "usp_tbl_tbl_StudentMarks_M_Select";
				
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
		
		
		
		#region Delete StudentMarks Details by StudentMarksId
        /// <summary>
        /// To Delete details of StudentMarks for selected StudentMarksId from tbl_StudentMarks_M table
        /// Created By : Vishal, 1/12/2016
		/// Modified By :
        /// </summary>
        public ApplicationResult StudentMarks_Delete(int intStudentMarksId, int intLastModifiedBy, string strLastModifiedDate)
		{
            try
            {
                pSqlParameter = new SqlParameter[3];
				
				pSqlParameter[0] = new SqlParameter("@StudentMarksId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentMarksId;
											
				pSqlParameter[1] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastModifiedBy;
				
				pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifiedDate;

				strStoredProcName = "usp_tbl_tbl_StudentMarks_M_Delete";
				
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
		
		
		
		#region Insert StudentMarks Details
		/// <summary>
        /// To Insert details of StudentMarks in tbl_StudentMarks_M table
        /// Created By : Vishal, 1/12/2016
		/// Modified By :
        /// </summary>
        public ApplicationResult StudentMarks_Insert(StudentMarksBO objStudentMarksBo)
        {
            try
            {
				pSqlParameter = new SqlParameter[12];
                
				
          		pSqlParameter[0] = new SqlParameter("@ExamConfigId",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objStudentMarksBo.ExamConfigId;
 
				pSqlParameter[1] = new SqlParameter("@SubjectId",SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objStudentMarksBo.SubjectId;
 
				pSqlParameter[2] = new SqlParameter("@StudentId",SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objStudentMarksBo.StudentId;
 
				pSqlParameter[3] = new SqlParameter("@Exam",SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objStudentMarksBo.Exam;
 
				pSqlParameter[4] = new SqlParameter("@TotalMarks",SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objStudentMarksBo.TotalMarks;
 
				pSqlParameter[5] = new SqlParameter("@PassingMarks",SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objStudentMarksBo.PassingMarks;
 
				pSqlParameter[6] = new SqlParameter("@ObtainedMarks",SqlDbType.Int);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objStudentMarksBo.ObtainedMarks;
 
				pSqlParameter[7] = new SqlParameter("@CreatedById",SqlDbType.Int);
				pSqlParameter[7].Direction = ParameterDirection.Input;
          		pSqlParameter[7].Value = objStudentMarksBo.CreatedById;
 
				pSqlParameter[8] = new SqlParameter("@CreatedDate",SqlDbType.VarChar);
				pSqlParameter[8].Direction = ParameterDirection.Input;
          		pSqlParameter[8].Value = objStudentMarksBo.CreatedDate;
 
				pSqlParameter[9] = new SqlParameter("@LastModifiedById",SqlDbType.Int);
				pSqlParameter[9].Direction = ParameterDirection.Input;
          		pSqlParameter[9].Value = objStudentMarksBo.LastModifiedById;
 
				pSqlParameter[10] = new SqlParameter("@LastModifiedByDate",SqlDbType.VarChar);
				pSqlParameter[10].Direction = ParameterDirection.Input;
          		pSqlParameter[10].Value = objStudentMarksBo.LastModifiedByDate;
 
				pSqlParameter[11] = new SqlParameter("@IsDeleted",SqlDbType.Int);
				pSqlParameter[11].Direction = ParameterDirection.Input;
          		pSqlParameter[11].Value = objStudentMarksBo.IsDeleted;

		
				sSql = "usp_tbl_StudentMarks_M_Insert";
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
                objStudentMarksBo = null;
            }
        }
        #endregion
		
		
		
		#region Update StudentMarks Details
		/// <summary>
        /// To Update details of StudentMarks in tbl_StudentMarks_M table
        /// Created By : Vishal, 1/12/2016
		/// Modified By :
        /// </summary>
        public ApplicationResult StudentMarks_Update(StudentMarksBO objStudentMarksBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[10];
                
				
                //pSqlParameter[0] = new SqlParameter("@StudentMarksId",SqlDbType.Int);
                //pSqlParameter[0].Direction = ParameterDirection.Input;
                //pSqlParameter[0].Value = objStudentMarksBo.StudentMarksId;
 
				pSqlParameter[0] = new SqlParameter("@ExamConfigId",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objStudentMarksBo.ExamConfigId;
 
                //pSqlParameter[1] = new SqlParameter("@SubjectId",SqlDbType.Int);
                //pSqlParameter[1].Direction = ParameterDirection.Input;
                //pSqlParameter[1].Value = objStudentMarksBo.SubjectId;
 
				pSqlParameter[1] = new SqlParameter("@StudentId",SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objStudentMarksBo.StudentId;
 
				pSqlParameter[2] = new SqlParameter("@Exam",SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objStudentMarksBo.Exam;
 
				pSqlParameter[3] = new SqlParameter("@TotalMarks",SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objStudentMarksBo.TotalMarks;
 
				pSqlParameter[4] = new SqlParameter("@PassingMarks",SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objStudentMarksBo.PassingMarks;
 
				pSqlParameter[5] = new SqlParameter("@ObtainedMarks",SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objStudentMarksBo.ObtainedMarks;
 
                //pSqlParameter[6] = new SqlParameter("@CreatedById",SqlDbType.Int);
                //pSqlParameter[8].Direction = ParameterDirection.Input;
                //pSqlParameter[8].Value = objStudentMarksBo.CreatedById;
 
                //pSqlParameter[9] = new SqlParameter("@CreatedDate",SqlDbType.VarChar);
                //pSqlParameter[9].Direction = ParameterDirection.Input;
                //pSqlParameter[9].Value = objStudentMarksBo.CreatedDate;
 
				pSqlParameter[6] = new SqlParameter("@LastModifiedById",SqlDbType.Int);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objStudentMarksBo.LastModifiedById;
 
				pSqlParameter[7] = new SqlParameter("@LastModifiedByDate",SqlDbType.VarChar);
				pSqlParameter[7].Direction = ParameterDirection.Input;
          		pSqlParameter[7].Value = objStudentMarksBo.LastModifiedByDate;
 
				pSqlParameter[8] = new SqlParameter("@IsDeleted",SqlDbType.Int);
				pSqlParameter[8].Direction = ParameterDirection.Input;
          		pSqlParameter[8].Value = objStudentMarksBo.IsDeleted;

                pSqlParameter[9] = new SqlParameter("@SubjectId", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objStudentMarksBo.SubjectId;
 

		
				sSql = "usp_tbl_StudentMarks_M_Update";
                                
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
                objStudentMarksBo = null;
            }
        }
        #endregion
		
				
		
		#region ValidateName for StudentMarks 
        /// <summary>
        /// Function which validates whether the StudentMarksName already exits in tbl_StudentMarks_M table.
        /// Created By : Vishal, 1/12/2016
		/// Modified By :
        /// </summary>
        public ApplicationResult StudentMarks_ValidateName(int intStudentMarksId,string strName)
		{
            try
            {
				pSqlParameter = new SqlParameter[2];
				
				pSqlParameter[0] = new SqlParameter("@StudentMarksId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentMarksId;
				
				pSqlParameter[1] = new SqlParameter("@Name", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strName;

				strStoredProcName = "usp_tbl_tbl_StudentMarks_M_ValidateName";
				
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
