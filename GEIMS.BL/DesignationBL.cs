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
    /// Class Created By : Darshan, 09/15/2014
	/// Summary description for Organisation.
    /// </summary>
	public class DesignationBL 
	{
		#region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion
		
		

		#region Select All Designation Details
        /// <summary>
        /// To Select All data from the tbl_Designation_M table
        /// Created By : Darshan, 09/15/2014
		/// Modified By :
        /// </summary>
		/// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult  Designation_SelectAll()
        {
			try
            {
				sSql = "usp_tbl_Designation_M_SelectAll";
                DataTable dtDesignation  = new DataTable();
                dtDesignation = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtDesignation);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select All Designation Details
        /// <summary>
        /// To Select All data from the tbl_Designation_M table
        /// Created By : Darshan, 09/15/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult Designation_SelectAll_ForDropDown()
        {
            try
            {
                sSql = "usp_tbl_Designation_M_SelectAll_ForDropDown";
                DataTable dtDesignation = new DataTable();
                dtDesignation = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtDesignation);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

		#region Select Designation Details by DesignationID
        /// <summary>
        /// Select all details of Designation for selected DesignationID from tbl_Designation_M table
        /// Created By : Darshan, 09/15/2014
		/// Modified By :
        /// </summary>
        /// <param name="intDesignationID"></param>
        /// <returns></returns>
		public ApplicationResult Designation_Select(int intDesignationID)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@DesignationID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intDesignationID;

				strStoredProcName = "usp_tbl_Designation_M_Select";
				
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
		


		#region Delete Designation Details by DesignationID
        /// <summary>
        /// To Delete details of Designation for selected DesignationID from tbl_Designation_M table
        /// Created By : Darshan, 09/15/2014
		/// Modified By :
        /// </summary>
        /// <param name="intDesignationID"></param>
        /// <returns></returns>
        public ApplicationResult Designation_Delete(int intDesignationID, int intLastModifiedUserID, string strLastModifiedDate)
		{
            try
            {
                pSqlParameter = new SqlParameter[3];
				
				pSqlParameter[0] = new SqlParameter("@DesignationID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intDesignationID;

                pSqlParameter[1] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastModifiedUserID;

                pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifiedDate;

				strStoredProcName = "usp_tbl_Designation_M_Delete";
				
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
		


		#region Insert Designation Details
		/// <summary>
        /// To Insert details of Designation in tbl_Designation_M table
        /// Created By : Darshan, 09/15/2014
		/// Modified By :
        /// </summary>
        /// <param name="objDesignationBO"></param>
        /// <returns></returns>
        public ApplicationResult Designation_Insert(DesignationBO objDesignationBO)
        {
            try
            {
				pSqlParameter = new SqlParameter[6];
                
				
          		pSqlParameter[0] = new SqlParameter("@DesignationNameENG",SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objDesignationBO.DesignationNameENG;
 
				pSqlParameter[1] = new SqlParameter("@DesignationNameGUJ",SqlDbType.NVarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objDesignationBO.DesignationNameGUJ;
 
				pSqlParameter[2] = new SqlParameter("@Description",SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objDesignationBO.Description;

                pSqlParameter[3] = new SqlParameter("@FullDayWH", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objDesignationBO.FullDayWorkingHours;

                pSqlParameter[4] = new SqlParameter("@CreatedUserID",SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objDesignationBO.CreatedUserID;
 
				pSqlParameter[5] = new SqlParameter("@CreatedDate",SqlDbType.VarChar);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objDesignationBO.CreatedDate;
 
				
				sSql = "usp_tbl_Designation_M_Insert";
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
                objDesignationBO = null;
            }
        }
        #endregion
	
	

		#region Update Designation Details
		/// <summary>
        /// To Update details of Designation in tbl_Designation_M table
        /// Created By : Darshan, 09/15/2014
		/// Modified By :
        /// </summary>
        /// <param name="objDesignationBO"></param>
        /// <returns></returns>
        public ApplicationResult Designation_Update(DesignationBO objDesignationBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[7];
                
				
          		pSqlParameter[0] = new SqlParameter("@DesignationID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objDesignationBO.DesignationID;
 
				pSqlParameter[1] = new SqlParameter("@DesignationNameENG",SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objDesignationBO.DesignationNameENG;
 
				pSqlParameter[2] = new SqlParameter("@DesignationNameGUJ",SqlDbType.NVarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objDesignationBO.DesignationNameGUJ;
 
				pSqlParameter[3] = new SqlParameter("@Description",SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objDesignationBO.Description;

                pSqlParameter[4] = new SqlParameter("@FullDayWH", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objDesignationBO.FullDayWorkingHours;

                pSqlParameter[5] = new SqlParameter("@LastModifiedUserID",SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objDesignationBO.LastModifiedUserID;
 
				pSqlParameter[6] = new SqlParameter("@LastModifiedDate",SqlDbType.VarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objDesignationBO.LastModifiedDate;

		
				sSql = "usp_tbl_Designation_M_Update";
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
                objDesignationBO = null;
            }
        }
        #endregion



        #region ValidateName for Designation
        public ApplicationResult Designation_ValidateName(int intDesignationID, string strDesignationNameENG)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@DesignationID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intDesignationID;

                pSqlParameter[1] = new SqlParameter("@DesignationNameENG", SqlDbType.NVarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strDesignationNameENG;

                strStoredProcName = "usp_tbl_Designation_M_ValidateName";

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
