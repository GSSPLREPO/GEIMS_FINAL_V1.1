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
    /// Class Created By : Darshan, 09/16/2014
	/// Summary description for Organisation.
    /// </summary>
	public class TimeTableBL 
	{
		#region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion
		


		#region Select All TimeTable Details
        /// <summary>
        /// To Select All data from the tbl_TimeTable_T table
        /// Created By : Darshan, 09/16/2014
		/// Modified By :
        /// </summary>
		/// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult  TimeTable_SelectAll()
        {
			try
            {
				sSql = "usp_tbl_TimeTable_T_SelectAll";
                DataTable dtTimeTable  = new DataTable();
                dtTimeTable = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtTimeTable);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
		


		#region Select TimeTable Details by Class Division
        /// <summary>
        /// Select all details of TimeTable for selected TimeTableTID from tbl_TimeTable_T table
        /// Created By : Darshan, 09/16/2014
		/// Modified By :
        /// </summary>
		public ApplicationResult TimeTable_Select_By_Class_Division(int intClassMID, int intDivisionTID)
		{
            try
            {
                pSqlParameter = new SqlParameter[2];
				
				pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intClassMID;

                pSqlParameter[1] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intDivisionTID;

				strStoredProcName = "usp_tbl_TimeTable_T_Select_By_Class_Division";
				
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

        #region Select TimeTable Details by Class Division Day
        /// <summary>
        /// Select all details of TimeTable for selected TimeTableTID from tbl_TimeTable_T table
        /// Created By : Darshan, 09/16/2014
        /// Modified By :
        /// </summary>
        public ApplicationResult TimeTable_Select_By_Class_Division_Day(int intClassMID, int intDivisionTID, int intDayNo)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intClassMID;

                pSqlParameter[1] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intDivisionTID;

                pSqlParameter[2] = new SqlParameter("@DayNo", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intDayNo;

                strStoredProcName = "usp_tbl_TimeTable_T_Select_By_Class_Division_Day";

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
		


		#region Delete TimeTable Details by TimeTableTID
        /// <summary>
        /// To Delete details of TimeTable for selected TimeTableTID from tbl_TimeTable_T table
        /// Created By : Darshan, 09/16/2014
		/// Modified By :
        /// </summary>
        /// <param name="intTimeTableTID"></param>
        /// <returns></returns>
		public ApplicationResult TimeTable_Delete(int intTimeTableTID)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@TimeTableTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTimeTableTID;

				strStoredProcName = "usp_tbl_TimeTable_T_Delete";
				
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
		


		#region Insert TimeTable Details
		/// <summary>
        /// To Insert details of TimeTable in tbl_TimeTable_T table
        /// Created By : Darshan, 09/16/2014
		/// Modified By :
        /// </summary>
        /// <param name="objTimeTableBO"></param>
        /// <returns></returns>
        public ApplicationResult TimeTable_Insert(TimeTableBO objTimeTableBO, int intClassMID, int intDivisionTID, int intLastModifiedUserID, string strLastModifiedDate)
        {
            try
            {
				pSqlParameter = new SqlParameter[7];
                
				
				pSqlParameter[0] = new SqlParameter("@PeriodID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objTimeTableBO.PeriodID;
 
				pSqlParameter[1] = new SqlParameter("@SubjectMID",SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objTimeTableBO.SubjectMID;
 
				pSqlParameter[2] = new SqlParameter("@EmployeeMID",SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objTimeTableBO.EmployeeMID;

                pSqlParameter[3] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intClassMID;

                pSqlParameter[4] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intDivisionTID;

                pSqlParameter[5] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = intLastModifiedUserID;

                pSqlParameter[6] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = strLastModifiedDate;
		
				sSql = "usp_tbl_TimeTable_T_Insert";
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
                objTimeTableBO = null;
            }
        }
        #endregion
	
	

		#region Update TimeTable Details
		/// <summary>
        /// To Update details of TimeTable in tbl_TimeTable_T table
        /// Created By : Darshan, 09/16/2014
		/// Modified By :
        /// </summary>
        /// <param name="objTimeTableBO"></param>
        /// <returns></returns>
        public ApplicationResult TimeTable_Update(TimeTableBO objTimeTableBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];
                
				
          		pSqlParameter[0] = new SqlParameter("@TimeTableTID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objTimeTableBO.TimeTableTID;
 
				pSqlParameter[1] = new SqlParameter("@TimeTableMID",SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objTimeTableBO.TimeTableMID;
 
				pSqlParameter[2] = new SqlParameter("@PeriodID",SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objTimeTableBO.PeriodID;
 
				pSqlParameter[3] = new SqlParameter("@SubjectMID",SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objTimeTableBO.SubjectMID;
 
				pSqlParameter[4] = new SqlParameter("@EmployeeMID",SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objTimeTableBO.EmployeeMID;

		
				sSql = "usp_tbl_TimeTable_T_Update";
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
                objTimeTableBO = null;
            }
        }
        #endregion



        #region Validate TimeTable with Period
        /// <summary>
        /// Select all details of TimeTable for selected TimeTableTID from tbl_TimeTable_T table
        /// Created By : Darshan, 09/16/2014
        /// Modified By :
        /// </summary>
        public ApplicationResult TimeTable_Validate_Period(int intClassMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intClassMID;

                strStoredProcName = "usp_tbl_TimeTable_T_Validate_Period";

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


        #region Select TimeTable Details by Class Division
        /// <summary>
        /// Select all details of TimeTable for selected TimeTableTID from tbl_TimeTable_T table
        /// Created By : Darshan, 09/16/2014
        /// Modified By :
        /// </summary>
        public ApplicationResult TimeTable_Select_TeacherWise(int intSchoolMID, int intEmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                pSqlParameter[1] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intEmployeeMID;

                strStoredProcName = "usp_tbl_TimeTable_T_Select_By_TeacherWise";

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

        #region Select TimeTable Details by Class Division
        /// <summary>
        /// Select all details of TimeTable for selected TimeTableTID from tbl_TimeTable_T table
        /// Created By : Darshan, 09/16/2014
        /// Modified By :
        /// </summary>
        public ApplicationResult TeacherWise_TimeTable(int intSchoolMID, int intEmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                pSqlParameter[1] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intEmployeeMID;

                strStoredProcName = "usp_tbl_TimeTable_T_Select_By_TeacherWise";

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
