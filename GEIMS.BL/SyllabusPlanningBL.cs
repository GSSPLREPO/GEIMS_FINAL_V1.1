using System;
using System.Data;
using System.Data.SqlClient;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
  public  class SyllabusPlanningBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        #region Insert Syllabus Planning Details
        public ApplicationResult SyllabusPlanning_Insert(SyllabusPlanningBO objSyllabusPlanningBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[7];


                pSqlParameter[0] = new SqlParameter("@SyllabusMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objSyllabusPlanningBO.SyllabusMID;

                pSqlParameter[1] = new SqlParameter("@MonthNo", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objSyllabusPlanningBO.MonthNo;

                pSqlParameter[2] = new SqlParameter("@TeacherMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objSyllabusPlanningBO.TeacherMID;

                pSqlParameter[3] = new SqlParameter("@PlannedStartDate", SqlDbType.Date);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objSyllabusPlanningBO.PlannedStartDate;

                pSqlParameter[4] = new SqlParameter("@PlannedEndDate", SqlDbType.Date);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objSyllabusPlanningBO.PlannedEndDate;     

                pSqlParameter[5] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objSyllabusPlanningBO.CreatedByID;

                pSqlParameter[6] = new SqlParameter("@CreatedDate", SqlDbType.DateTime);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objSyllabusPlanningBO.CreatedDate;             



                sSql = "usp_tbl_SyllabusPlanning_T_Insert";
                DataTable dtResult = new DataTable();
                dtResult = DatabaseTransaction.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objSyllabusPlanningBO = null;
            }
        }
        #endregion


        public ApplicationResult SyllabusPlanningListByTeacehrID(int TeacehrMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];


                pSqlParameter[0] = new SqlParameter("@TeacherMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = TeacehrMID;


                sSql = "usp_tbl_SyllabusPlanningList_By_TeacherID";
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

        public ApplicationResult SyllabusPlanning_Update(SyllabusPlanningBO objSyllabusPlanningBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];


                pSqlParameter[0] = new SqlParameter("@SyllabusPlanTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objSyllabusPlanningBO.SyllabusPlanTID;

                pSqlParameter[1] = new SqlParameter("@ActualStartDate", SqlDbType.DateTime);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objSyllabusPlanningBO.ActualStartDate;

                pSqlParameter[2] = new SqlParameter("@ActualEndDate", SqlDbType.DateTime);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objSyllabusPlanningBO.ActualEndDate;

                pSqlParameter[3] = new SqlParameter("@SyllabusCovered", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objSyllabusPlanningBO.SyllabusCovered;

                pSqlParameter[4] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objSyllabusPlanningBO.ModifiedByID;

                pSqlParameter[5] = new SqlParameter("@ModifiedDate", SqlDbType.DateTime);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objSyllabusPlanningBO.ModifiedDate;



                sSql = "usp_tbl_SyllabusPlanning_T_Update";
                DataTable dtResult = new DataTable();
                dtResult = DatabaseTransaction.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objSyllabusPlanningBO = null;
            }
        }


        public ApplicationResult SyllabusPlanning_SelectAll(int SyllabusPlanningTID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];


                pSqlParameter[0] = new SqlParameter("@SyllabusPlanningTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = SyllabusPlanningTID;


                sSql = "usp_tbl_SyllabusPlanningList_ByID";
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

    }
}
