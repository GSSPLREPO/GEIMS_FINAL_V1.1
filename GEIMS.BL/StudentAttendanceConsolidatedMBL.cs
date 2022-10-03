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
    public class StudentAttendanceConsolidatedMBL
    {
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter;


        public ApplicationResult Student_AttendacneConsolidated_Insert(StudentAttendanceConsolidatedMBO objStudAttendanceConsolidatedBO)
        {
            try {
                pSqlParameter = new SqlParameter[12];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objStudAttendanceConsolidatedBO.TrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objStudAttendanceConsolidatedBO.SchoolMID;

                pSqlParameter[2] = new SqlParameter("EmployeeMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objStudAttendanceConsolidatedBO.EmployeeMID;

                pSqlParameter[3] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objStudAttendanceConsolidatedBO.ClassMID;

                pSqlParameter[4] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objStudAttendanceConsolidatedBO.DivisionTID;

                pSqlParameter[5] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objStudAttendanceConsolidatedBO.AcademicYear;

                pSqlParameter[6] = new SqlParameter("@TotalStudentCount", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objStudAttendanceConsolidatedBO.TotalStudentCount;

                pSqlParameter[7] = new SqlParameter("@PresentStudentCount", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objStudAttendanceConsolidatedBO.PresentStudentCount;

                pSqlParameter[8] = new SqlParameter("@AbsentStudentCount", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objStudAttendanceConsolidatedBO.AbsentStudentCount;

                pSqlParameter[9] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objStudAttendanceConsolidatedBO.CreatedBy;

                pSqlParameter[10] = new SqlParameter("@CreatedDate", SqlDbType.Date);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objStudAttendanceConsolidatedBO.CreatedDate;

                pSqlParameter[11] = new SqlParameter("@AttendanceTakenDate", SqlDbType.Date);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objStudAttendanceConsolidatedBO.AttendanceTakenDate;

                sSql = "usp_tbl_StudentAttendanceConsolidated_M_Insert";
                DataTable dtEmployeeM = new DataTable();
                dtEmployeeM = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtEmployeeM);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objStudAttendanceConsolidatedBO = null;
            }
        }


        public ApplicationResult StudentAttendanceConsolidated_SelectAll(string dtAttendanceTakenDate, int intClassMID, int intDivisionTID)
        {
            pSqlParameter = new SqlParameter[3];

            pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
            pSqlParameter[0].Direction = ParameterDirection.Input;
            pSqlParameter[0].Value = intClassMID;

            pSqlParameter[1] = new SqlParameter("@DivisionTID", SqlDbType.Int);
            pSqlParameter[1].Direction = ParameterDirection.Input;
            pSqlParameter[1].Value = intDivisionTID;

            pSqlParameter[2] = new SqlParameter("AttendanceTakenDate", SqlDbType.Date);
            pSqlParameter[2].Direction = ParameterDirection.Input;
            pSqlParameter[2].Value = dtAttendanceTakenDate;

            sSql = "usp_tbl_StudentAttendanceConsolidated_M_SelectAll";
            DataTable dtEmployeeM = new DataTable();
            dtEmployeeM = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

            ApplicationResult objResults = new ApplicationResult(dtEmployeeM);
            objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
            return objResults;


        }


        public ApplicationResult StudentAttedanceConsolidated_Select_By_ID(int intStudentAttenConsolidatedMID)
        {
            try {

                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@StudentAttenConsolidatedMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentAttenConsolidatedMID;


                sSql = "usp_tbl_StudentAttendanceConsolidated_M_Select";
                DataTable dtEmployeeM = new DataTable();
                dtEmployeeM = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtEmployeeM);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ApplicationResult StudentAttendanceConsolidate_Update(StudentAttendanceConsolidatedMBO objStudAttendanceConsolidatedBO)
        {
            pSqlParameter = new SqlParameter[5];

            pSqlParameter[0] = new SqlParameter("@StudentAttenConsolidatedMID", SqlDbType.Int);
            pSqlParameter[0].Direction = ParameterDirection.Input;
            pSqlParameter[0].Value = objStudAttendanceConsolidatedBO.StudentAttendanceConsolidatedMID;

            pSqlParameter[1] = new SqlParameter("@PresentStudentCount", SqlDbType.Decimal);
            pSqlParameter[1].Direction = ParameterDirection.Input;
            pSqlParameter[1].Value = objStudAttendanceConsolidatedBO.PresentStudentCount; 

            pSqlParameter[2] = new SqlParameter("@AbsentStudentCount", SqlDbType.Decimal);
            pSqlParameter[2].Direction = ParameterDirection.Input;
            pSqlParameter[2].Value = objStudAttendanceConsolidatedBO.AbsentStudentCount;

            pSqlParameter[3] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
            pSqlParameter[3].Direction = ParameterDirection.Input;
            pSqlParameter[3].Value = objStudAttendanceConsolidatedBO.ModifiedBy;

            pSqlParameter[4] = new SqlParameter("@ModifiedDate", SqlDbType.DateTime);
            pSqlParameter[4].Direction = ParameterDirection.Input;
            pSqlParameter[4].Value = objStudAttendanceConsolidatedBO.ModifiedDate;

            sSql = "usp_tbl_StudentAttendanceConsolidated_M_Update";
            DataTable dtEmployeeM = new DataTable();
            dtEmployeeM = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

            ApplicationResult objResults = new ApplicationResult(dtEmployeeM);
            objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
            return objResults;
        }

        public ApplicationResult StudentAttendanceConsolidated_Delete(int intStudAttendanceConsolidatedMID, int intModifiedBy, string strModifiedDate)
        {
            pSqlParameter = new SqlParameter[3];

            pSqlParameter[0] = new SqlParameter("@StudentAttendanceConsolidatedMID", SqlDbType.Int);
            pSqlParameter[0].Direction = ParameterDirection.Input;
            pSqlParameter[0].Value = intStudAttendanceConsolidatedMID;        

            pSqlParameter[1] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
            pSqlParameter[1].Direction = ParameterDirection.Input;
            pSqlParameter[1].Value = intModifiedBy;

            pSqlParameter[2] = new SqlParameter("@ModifiedDate", SqlDbType.DateTime);
            pSqlParameter[2].Direction = ParameterDirection.Input;
            pSqlParameter[2].Value = strModifiedDate;

            sSql = "usp_tbl_StudentAttendanceConsolidated_M_Delete";
            DataTable dtEmployeeM = new DataTable();
            dtEmployeeM = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

            ApplicationResult objResults = new ApplicationResult(dtEmployeeM);
            objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
            return objResults;

        }



     
    }
}
