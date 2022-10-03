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
    /// Class Created By : Darshan, 09/13/2014
	/// Summary description for Organisation.
    /// </summary>
	public class DepartmentBL 
	{
		#region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion
		
		

		#region Select All Department Details
        /// <summary>
        /// To Select All data from the tbl_Department_M table
        /// Created By : Darshan, 09/13/2014
		/// Modified By :
        /// </summary>
		/// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult  Department_SelectAll()
        {
			try
            {
				sSql = "usp_tbl_Department_M_SelectAll";
                DataTable dtDepartment  = new DataTable();
                dtDepartment = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtDepartment);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select All Department Details
        /// <summary>
        /// To Select All data from the tbl_Section_M table
        /// Created By : NafisaMulla, 4/7/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult Department_SelectAll_ForDropDown(int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                sSql = "usp_tbl_Department_M_SelectAll_ForDropDown";

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
        #endregion



        #region Select Department Details by DepartmentID
        /// <summary>
        /// Select all details of Department for selected DepartmentID from tbl_Department_M table
        /// Created By : Darshan, 09/13/2014
        /// Modified By :
        /// </summary>
        /// <param name="intDepartmentID"></param>
        /// <returns></returns>
        public ApplicationResult Department_Select(int intDepartmentID)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intDepartmentID;

				strStoredProcName = "usp_tbl_Department_M_Select";
				
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

        #region Select Department Details By School Trust
        public ApplicationResult Department_Select_By_Trust_School(int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_Department_M_Select_By_Trust_School";

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

        #region Select Department Details By School Trust
        public ApplicationResult Department_Select_By_Trust_School_ForDropDown(int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_Department_M_Select_By_Trust_SchoolForDropdown";

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



		#region Delete Department Details by DepartmentID
        /// <summary>
        /// To Delete details of Department for selected DepartmentID from tbl_Department_M table
        /// Created By : Darshan, 09/13/2014
		/// Modified By :
        /// </summary>
        /// <param name="intDepartmentID"></param>
        /// <returns></returns>
        public ApplicationResult Department_Delete(int intDepartmentID, int intLastModifiedUserID, string strLastModifiedDate)
		{
            try
            {
                pSqlParameter = new SqlParameter[3];
				
				pSqlParameter[0] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intDepartmentID;

                pSqlParameter[1] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastModifiedUserID;

                pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifiedDate;

				strStoredProcName = "usp_tbl_Department_M_Delete";
				
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
		


		#region Insert Department Details
		/// <summary>
        /// To Insert details of Department in tbl_Department_M table
        /// Created By : Darshan, 09/13/2014
		/// Modified By :
        /// </summary>
        /// <param name="objDepartmentBO"></param>
        /// <returns></returns>
        public ApplicationResult Department_Insert(DepartmentBO objDepartmentBO)
        {
            try
            {
				pSqlParameter = new SqlParameter[7];
                
				
          		pSqlParameter[0] = new SqlParameter("@DepartmentNameENG",SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objDepartmentBO.DepartmentNameENG;
 
				pSqlParameter[1] = new SqlParameter("@DepartmentNameGUJ",SqlDbType.NVarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objDepartmentBO.DepartmentNameGUJ;
 
				pSqlParameter[2] = new SqlParameter("@Description",SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objDepartmentBO.Description;
 
				pSqlParameter[3] = new SqlParameter("@SchoolMID",SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objDepartmentBO.SchoolMID;
 
				pSqlParameter[4] = new SqlParameter("@TrustMID",SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objDepartmentBO.TrustMID;
 
				pSqlParameter[5] = new SqlParameter("@CreatedUserID",SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objDepartmentBO.CreatedUserID;
 
				pSqlParameter[6] = new SqlParameter("@CreatedDate",SqlDbType.VarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objDepartmentBO.CreatedDate;
 
				
		
				sSql = "usp_tbl_Department_M_Insert";
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
                objDepartmentBO = null;
            }
        }
        #endregion
		


		#region Update Department Details
		/// <summary>
        /// To Update details of Department in tbl_Department_M table
        /// Created By : Darshan, 09/13/2014
		/// Modified By :
        /// </summary>
        /// <param name="objDepartmentBO"></param>
        /// <returns></returns>
        public ApplicationResult Department_Update(DepartmentBO objDepartmentBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[8];
                
				
          		pSqlParameter[0] = new SqlParameter("@DepartmentID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objDepartmentBO.DepartmentID;
 
				pSqlParameter[1] = new SqlParameter("@DepartmentNameENG",SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objDepartmentBO.DepartmentNameENG;
 
				pSqlParameter[2] = new SqlParameter("@DepartmentNameGUJ",SqlDbType.NVarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objDepartmentBO.DepartmentNameGUJ;
 
				pSqlParameter[3] = new SqlParameter("@Description",SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objDepartmentBO.Description;
 
				pSqlParameter[4] = new SqlParameter("@SchoolMID",SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objDepartmentBO.SchoolMID;
 
				pSqlParameter[5] = new SqlParameter("@TrustMID",SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objDepartmentBO.TrustMID;
 
				pSqlParameter[6] = new SqlParameter("@LastModifiedUserID",SqlDbType.Int);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objDepartmentBO.LastModifiedUserID;
 
				pSqlParameter[7] = new SqlParameter("@LastModifiedDate",SqlDbType.VarChar);
				pSqlParameter[7].Direction = ParameterDirection.Input;
          		pSqlParameter[7].Value = objDepartmentBO.LastModifiedDate;

		
				sSql = "usp_tbl_Department_M_Update";
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
                objDepartmentBO = null;
            }
        }
        #endregion



        #region ValidateName for Department
        public ApplicationResult Department_ValidateName(int intTrustMID, int intSchoolMID, int intDepartmentID, string strDepartmentNameENG)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                pSqlParameter[2] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intDepartmentID;

                pSqlParameter[3] = new SqlParameter("@DepartmentNameENG", SqlDbType.NVarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strDepartmentNameENG;

                strStoredProcName = "usp_tbl_Department_M_ValidateName";

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
