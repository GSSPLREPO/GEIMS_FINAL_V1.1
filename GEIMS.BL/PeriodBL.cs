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
    /// Class Created By : Darshan, 09/04/2014
	/// Summary description for Organisation.
    /// </summary>
	public class PeriodBL 
	{
		#region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion
		
		

		#region Select All Period Details
        /// <summary>
        /// To Select All data from the tbl_Period_M table
        /// Created By : Darshan, 09/04/2014
		/// Modified By :
        /// </summary>
		/// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult  Period_SelectAll()
        {
			try
            {
				sSql = "usp_tbl_Period_M_SelectAll";
                DataTable dtPeriod  = new DataTable();
                dtPeriod = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtPeriod);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select All Days by Class 
        /// <summary>
        /// Select all details of Period for selected PeriodID from tbl_Period_M table
        /// Created By : Darshan, 09/04/2014
        /// Modified By :
        /// </summary>
        /// <param name="intPeriodID"></param>
        /// <returns></returns>
        public ApplicationResult Period_SelectAll_Days(int intClassMID, int intMode)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intClassMID;

                pSqlParameter[1] = new SqlParameter("@Mode", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intMode;

                strStoredProcName = "usp_tbl_Period_M_SelectAll_Days";

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



        #region Select Period Details by PeriodID
        /// <summary>
        /// Select all details of Period for selected PeriodID from tbl_Period_M table
        /// Created By : Darshan, 09/04/2014
		/// Modified By :
        /// </summary>
        /// <param name="intPeriodID"></param>
        /// <returns></returns>
		public ApplicationResult Period_Select(int intPeriodID)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@PeriodID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intPeriodID;

				strStoredProcName = "usp_tbl_Period_M_Select";
				
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

        #region Select Period Details By School and Class
        public ApplicationResult Period_Select_By_School_Class(int intSchoolMID, int intClassMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                pSqlParameter[1] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intClassMID;

                strStoredProcName = "usp_tbl_Period_M_Select_By_School_Class";

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

        #region Select Period Details By Class and Day
        public ApplicationResult Period_Select_By_Class(int intClassMID, int intDayNo)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intClassMID;

                pSqlParameter[1] = new SqlParameter("@DayNo", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intDayNo;

                strStoredProcName = "usp_tbl_Period_M_Select_By_Class";

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



        #region Delete Period Details by PeriodID
        /// <summary>
        /// To Delete details of Period for selected PeriodID from tbl_Period_M table
        /// Created By : Darshan, 09/04/2014
		/// Modified By :
        /// </summary>
        /// <param name="intPeriodID"></param>
        /// <returns></returns>
		public ApplicationResult Period_Delete_By_Class_Day(int intClassMID, int intDayNo, int intLastModifiedUserID, string strLastModifiedDate)
		{
            try
            {
                pSqlParameter = new SqlParameter[4];
				
				pSqlParameter[0] = new SqlParameter("@ClassMId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intClassMID;

                pSqlParameter[1] = new SqlParameter("@DayNo", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intDayNo;

                pSqlParameter[2] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intLastModifiedUserID;

                pSqlParameter[3] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strLastModifiedDate;

				strStoredProcName = "usp_tbl_Period_M_Delete_By_Class_Day";
				
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
		


		#region Insert Period Details
		/// <summary>
        /// To Insert details of Period in tbl_Period_M table
        /// Created By : Darshan, 09/04/2014
		/// Modified By :
        /// </summary>
        /// <param name="objPeriodBO"></param>
        /// <returns></returns>
        public ApplicationResult Period_Insert(PeriodBO objPeriodBO)
        {
            try
            {
				pSqlParameter = new SqlParameter[9];
                
				
          		pSqlParameter[0] = new SqlParameter("@DayName",SqlDbType.NVarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objPeriodBO.DayName;
 
				pSqlParameter[1] = new SqlParameter("@DayNo",SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objPeriodBO.DayNo;
 
				pSqlParameter[2] = new SqlParameter("@PeriodNo",SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objPeriodBO.PeriodNo;
 
				pSqlParameter[3] = new SqlParameter("@FromTime",SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objPeriodBO.FromTime;
 
				pSqlParameter[4] = new SqlParameter("@ToTime",SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objPeriodBO.ToTime;
 
				pSqlParameter[5] = new SqlParameter("@ClassMID",SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objPeriodBO.ClassMID;
 
				pSqlParameter[6] = new SqlParameter("@SchoolMID",SqlDbType.Int);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objPeriodBO.SchoolMID;
 
				pSqlParameter[7] = new SqlParameter("@CreatedUserID",SqlDbType.Int);
				pSqlParameter[7].Direction = ParameterDirection.Input;
          		pSqlParameter[7].Value = objPeriodBO.CreatedUserID;
 
				pSqlParameter[8] = new SqlParameter("@CreatedDate",SqlDbType.VarChar);
				pSqlParameter[8].Direction = ParameterDirection.Input;
          		pSqlParameter[8].Value = objPeriodBO.CreatedDate;
 
				
				sSql = "usp_tbl_Period_M_Insert";
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
                objPeriodBO = null;
            }
        }
        #endregion
		


		#region Update Period Details
		/// <summary>
        /// To Update details of Period in tbl_Period_M table
        /// Created By : Darshan, 09/04/2014
		/// Modified By :
        /// </summary>
        /// <param name="objPeriodBO"></param>
        /// <returns></returns>
        public ApplicationResult Period_Update(PeriodBO objPeriodBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[7];
                
				
          		pSqlParameter[0] = new SqlParameter("@DayNo",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objPeriodBO.DayNo;

                pSqlParameter[1] = new SqlParameter("@PeriodNo", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objPeriodBO.PeriodNo;
 
				pSqlParameter[2] = new SqlParameter("@FromTime",SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objPeriodBO.FromTime;
 
				pSqlParameter[3] = new SqlParameter("@ToTime",SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objPeriodBO.ToTime;

                pSqlParameter[4] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objPeriodBO.ClassMID;
 
				pSqlParameter[5] = new SqlParameter("@LastModifiedUserID",SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objPeriodBO.LastModifiedUserID;
 
				pSqlParameter[6] = new SqlParameter("@LastModifiedDate",SqlDbType.VarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objPeriodBO.LastModifiedDate;

		
				sSql = "usp_tbl_Period_M_Update";
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
                objPeriodBO = null;
            }
        }
        #endregion
	}
}
