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
    public class AdmissionFormBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        #region Select FormNo By ID
        public ApplicationResult AddmissionForm_Select(int intSchoolMID, int intSectionId)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                pSqlParameter[1] = new SqlParameter("@SectionID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSectionId;

                strStoredProcName = "usp_tbl_AdmissionForm_M_Select";

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

        #region Insert AdmissionForm Details

        public ApplicationResult AdmissionForm_Insert(AdmissionFormBO objAdmissionFormBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

                pSqlParameter[0] = new SqlParameter("@AdmissionFormNo", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objAdmissionFormBO.AdmissionFormNo;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objAdmissionFormBO.SchoolMID;

                pSqlParameter[2] = new SqlParameter("@SectionID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objAdmissionFormBO.SectionID;


                //pSqlParameter[3] = new SqlParameter("@Language", SqlDbType.Int);
                //pSqlParameter[3].Direction = ParameterDirection.Input;
                //pSqlParameter[3].Value = objAdmissionFormBO.Language;

                pSqlParameter[3] = new SqlParameter("@Year", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objAdmissionFormBO.Year;

                pSqlParameter[4] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objAdmissionFormBO.CreatedByID;

                pSqlParameter[5] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objAdmissionFormBO.CreatedDate;

          



                sSql = "usp_tbl_AdmissionForm_M_Insert";
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
                //  objDepartmentBO = null;
            }
        }
        #endregion
   
    }
}
