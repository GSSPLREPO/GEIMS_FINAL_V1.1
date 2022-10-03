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
    /// Class Created By : Nirmal, 04-01-2016
	/// Summary description for Organisation.
    /// </summary>
	public class HolidayBl 
	{
		#region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion
		
		
		
		#region Select All Holiday Details
        /// <summary>
        /// To Select All data from the tbl_Holiday_M table
        /// Created By : Nirmal, 04-01-2016
		/// Modified By :
        /// </summary>
		public ApplicationResult  Holiday_SelectAll()
        {
			try
            {
				sSql = "usp_tbl_Holiday_M_SelectAll";
                DataTable dtHoliday  = new DataTable();
                dtHoliday = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtHoliday);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
		
		
		
		#region Select Holiday Details by HolidayId
        /// <summary>
        /// Select all details of Holiday for selected HolidayId from tbl_Holiday_M table
        /// Created By : Nirmal, 04-01-2016
		/// Modified By :
        /// </summary>
        public ApplicationResult Holiday_Select(int intHolidayId)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@HolidayId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intHolidayId;

				strStoredProcName = "usp_tbl_Holiday_M_Select";
				
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
		
		
		
		#region Delete Holiday Details by HolidayId
        /// <summary>
        /// To Delete details of Holiday for selected HolidayId from tbl_Holiday_M table
        /// Created By : Nirmal, 04-01-2016
		/// Modified By :
        /// </summary>
        public ApplicationResult Holiday_Delete(int intHolidayId, int intLastModifiedBy, string strLastModifiedDate)
		{
            try
            {
                pSqlParameter = new SqlParameter[3];
				
				pSqlParameter[0] = new SqlParameter("@HolidayId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intHolidayId;
											
				pSqlParameter[1] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastModifiedBy;
				
				pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifiedDate;

				strStoredProcName = "usp_tbl_Holiday_M_Delete";
				
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
		
		
		
		#region Insert Holiday Details
		/// <summary>
        /// To Insert details of Holiday in tbl_Holiday_M table
        /// Created By : Nirmal, 04-01-2016
		/// Modified By :
        /// </summary>
        public ApplicationResult Holiday_Insert(HolidayBo objHolidayBo)
        {
            try
            {
				pSqlParameter = new SqlParameter[7];
                
				
          		pSqlParameter[0] = new SqlParameter("@Name",SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objHolidayBo.Name;
 
				pSqlParameter[1] = new SqlParameter("@AcademicYear",SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objHolidayBo.AcademicYear;
 
				pSqlParameter[2] = new SqlParameter("@StartDate",SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objHolidayBo.StartDate;

                pSqlParameter[3] = new SqlParameter("@EndDate", SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objHolidayBo.EndDate;
 
				pSqlParameter[4] = new SqlParameter("@Description",SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objHolidayBo.Description;
 
				pSqlParameter[5] = new SqlParameter("@CreatedBy",SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objHolidayBo.CreatedBy;
 
				pSqlParameter[6] = new SqlParameter("@CreatedDate",SqlDbType.VarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objHolidayBo.CreatedDate;
 
				sSql = "usp_tbl_Holiday_M_Insert";
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
                objHolidayBo = null;
            }
        }
        #endregion
		
		
		
		#region Update Holiday Details
		/// <summary>
        /// To Update details of Holiday in tbl_Holiday_M table
        /// Created By : Nirmal, 04-01-2016
		/// Modified By :
        /// </summary>
        public ApplicationResult Holiday_Update(HolidayBo objHolidayBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[8];
                
				
          		pSqlParameter[0] = new SqlParameter("@HolidayId",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objHolidayBo.HolidayId;
 
				pSqlParameter[1] = new SqlParameter("@Name",SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objHolidayBo.Name;
 
				pSqlParameter[2] = new SqlParameter("@AcademicYear",SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objHolidayBo.AcademicYear;
 
				pSqlParameter[3] = new SqlParameter("@StartDate",SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objHolidayBo.StartDate;

                pSqlParameter[4] = new SqlParameter("@EndDate", SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objHolidayBo.EndDate;
 
				pSqlParameter[5] = new SqlParameter("@Description",SqlDbType.VarChar);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objHolidayBo.Description;
 
				pSqlParameter[6] = new SqlParameter("@LastModifiedBy",SqlDbType.Int);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objHolidayBo.LastModifiedBy;
 
				pSqlParameter[7] = new SqlParameter("@LastModifiedDate",SqlDbType.VarChar);
				pSqlParameter[7].Direction = ParameterDirection.Input;
          		pSqlParameter[7].Value = objHolidayBo.LastModifiedDate;

		
				sSql = "usp_tbl_Holiday_M_Update";
                                
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
                objHolidayBo = null;
            }
        }
        #endregion
		
				
		
		#region ValidateName for Holiday 
        /// <summary>
        /// Function which validates whether the HolidayName already exits in tbl_Holiday_M table.
        /// Created By : Nirmal, 04-01-2016
		/// Modified By :
        /// </summary>
        public ApplicationResult Holiday_ValidateName(int intHolidayId, string strDate, string strAcademicYear)
		{
            try
            {
				pSqlParameter = new SqlParameter[3];
				
				pSqlParameter[0] = new SqlParameter("@HolidayId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intHolidayId;
				
				pSqlParameter[1] = new SqlParameter("@Date", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strDate;

                pSqlParameter[2] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strAcademicYear;

                strStoredProcName = "usp_tbl_Holiday_M_ValidateName";
				
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
