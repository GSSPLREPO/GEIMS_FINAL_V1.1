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
    public class StudentAttemptBL
    {
       /// <summary>
    /// Class Created By : Vishal, 10/16/2015
	/// Summary description for Organisation.
    /// </summary>
		#region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion
		
		
		
		#region Select All StudentAttempt Details
        /// <summary>
        /// To Select All data from the tbl_StudentAttempt table
        /// Created By : Vishal, 10/16/2015
		/// Modified By :
        /// </summary>
		public ApplicationResult  StudentAttempt_SelectAll()
        {
			try
            {
				sSql = "usp_tbl_tbl_StudentAttempt_SelectAll";
                DataTable dtStudentAttempt  = new DataTable();
                dtStudentAttempt = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtStudentAttempt);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
		
		
		
		#region Select StudentAttempt Details by StudentAttemptID
        /// <summary>
        /// Select all details of StudentAttempt for selected StudentAttemptID from tbl_StudentAttempt table
        /// Created By : Vishal, 10/16/2015
		/// Modified By :
        /// </summary>
        public ApplicationResult StudentAttempt_Select(int intStudentMID)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentMID;

				strStoredProcName = "usp_tbl_StudentAttempt_Select";
				
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
		
		
		
		#region Delete StudentAttempt Details by StudentAttemptID
        /// <summary>
        /// To Delete details of StudentAttempt for selected StudentAttemptID from tbl_StudentAttempt table
        /// Created By : Vishal, 10/16/2015
		/// Modified By :
        /// </summary>
        public ApplicationResult StudentAttempt_Delete(int intStudentAttemptID, int intLastModifiedBy, string strLastModifiedDate)
		{
            try
            {
                pSqlParameter = new SqlParameter[3];
				
				pSqlParameter[0] = new SqlParameter("@StudentAttemptID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentAttemptID;
											
				pSqlParameter[1] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastModifiedBy;
				
				pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifiedDate;

				strStoredProcName = "usp_tbl_tbl_StudentAttempt_Delete";
				
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
		
		
		
		#region Insert StudentAttempt Details
		/// <summary>
        /// To Insert details of StudentAttempt in tbl_StudentAttempt table
        /// Created By : Vishal, 10/16/2015
		/// Modified By :
        /// </summary>
        public ApplicationResult StudentAttempt_Insert(StudentAttemptBO objStudentAttemptBo)
        {
            try
            {
				pSqlParameter = new SqlParameter[7];
                
				
          		pSqlParameter[0] = new SqlParameter("@StudentMID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objStudentAttemptBo.StudentMID;
 
				pSqlParameter[1] = new SqlParameter("@Attempt",SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objStudentAttemptBo.Attempt;
 
				pSqlParameter[2] = new SqlParameter("@Percent",SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objStudentAttemptBo.Percent;
 
				pSqlParameter[3] = new SqlParameter("@Year",SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objStudentAttemptBo.Year;
 
				pSqlParameter[4] = new SqlParameter("@Month",SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objStudentAttemptBo.Month;

                pSqlParameter[5] = new SqlParameter("@SeatNo", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objStudentAttemptBo.SeatNo;

				pSqlParameter[6] = new SqlParameter("@IsAttempt",SqlDbType.Int);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objStudentAttemptBo.IsAttempt;

		
				sSql = "usp_tbl_tbl_StudentAttempt_Insert";
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
                objStudentAttemptBo = null;
            }
        }
        #endregion
		
		
		
		#region Update StudentAttempt Details
		/// <summary>
        /// To Update details of StudentAttempt in tbl_StudentAttempt table
        /// Created By : Vishal, 10/16/2015
		/// Modified By :
        /// </summary>
        public ApplicationResult StudentAttempt_Update(StudentAttemptBO objStudentAttemptBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[7];
                
				
          		pSqlParameter[0] = new SqlParameter("@StudentAttemptID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objStudentAttemptBo.StudentAttemptID;
 
				pSqlParameter[1] = new SqlParameter("@StudentMID",SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objStudentAttemptBo.StudentMID;
 
				pSqlParameter[2] = new SqlParameter("@Attempt",SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objStudentAttemptBo.Attempt;
 
				pSqlParameter[3] = new SqlParameter("@Percent",SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objStudentAttemptBo.Percent;
 
				pSqlParameter[4] = new SqlParameter("@Year",SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objStudentAttemptBo.Year;
 
				pSqlParameter[5] = new SqlParameter("@Month",SqlDbType.VarChar);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objStudentAttemptBo.Month;
 
				pSqlParameter[6] = new SqlParameter("@IsAttempt",SqlDbType.Int);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objStudentAttemptBo.IsAttempt;

		
				sSql = "usp_tbl_tbl_StudentAttempt_Update";
                                
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
                objStudentAttemptBo = null;
            }
        }
        #endregion
		
				
		
		#region ValidateName for StudentAttempt 
        /// <summary>
        /// Function which validates whether the StudentAttemptName already exits in tbl_StudentAttempt table.
        /// Created By : Vishal, 10/16/2015
		/// Modified By :
        /// </summary>
        public ApplicationResult StudentAttempt_ValidateName(int intStudentAttemptId,string strName)
		{
            try
            {
				pSqlParameter = new SqlParameter[2];
				
				pSqlParameter[0] = new SqlParameter("@StudentAttemptId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentAttemptId;
				
				pSqlParameter[1] = new SqlParameter("@Name", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strName;

				strStoredProcName = "usp_tbl_tbl_StudentAttempt_ValidateName";
				
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
