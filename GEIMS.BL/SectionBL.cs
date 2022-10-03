using System;
using System.Data;
using System.Data.SqlClient;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
	/// <summary>
	/// Class Created By : NafisaMulla, 12/6/2014
	/// Summary description for Organisation.
	/// </summary>
	public class SectionBL
	{
		#region user defined variables
		public string sSql;
		public string strStoredProcName;
		public SqlParameter[] pSqlParameter = null;
		#endregion

		#region Select All Section Details
		/// <summary>
		/// To Select All data from the tbl_Section_M table
		/// Created By : NafisaMulla, 4/7/2014
		/// Modified By :
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public ApplicationResult Section_SelectAll(int intSchoolMID)
		{
			try
			{
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

				sSql = "usp_tbl_Section_M_SelectAll";

				DataTable dtSection = new DataTable();
				dtSection = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

				ApplicationResult objResults = new ApplicationResult(dtSection);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

        #region Select All Section Details
        /// <summary>
        /// To Select All data from the tbl_Section_M table
        /// Created By : NafisaMulla, 4/7/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult Section_SelectAll_ForDropDown(int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                sSql = "usp_tbl_Section_M_SelectAll_ForDropDown";

                DataTable dtSection = new DataTable();
                dtSection = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtSection);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

		#region Select All Section Details By SectionMID
		/// <summary>
		/// To Select All data from the tbl_Section_M table
		/// Created By : NafisaMulla, 4/7/2014
		/// Modified By :
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public ApplicationResult Section_SelectAll_SectionMID(int intSchoolMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

				sSql = "usp_tbl_Section_M_SelectAll_SectionMID";

				DataTable dtSection = new DataTable();
				dtSection = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

				ApplicationResult objResults = new ApplicationResult(dtSection);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region Select Section Details by SectionMID
		/// <summary>
		/// Select all details of Section for selected SectionMID from tbl_Section_M table
		/// Created By : NafisaMulla, 4/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="intSectionMID"></param>
		/// <returns></returns>
		public ApplicationResult Section_Select(int intSectionMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@SectionMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intSectionMID;

				strStoredProcName = "usp_tbl_Section_M_Select";

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

		#region Delete Section Details by SectionMID
		/// <summary>
		/// To Delete details of Section for selected SectionMID from tbl_Section_M table
		/// Created By : NafisaMulla, 4/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="intSectionMID"></param>
		/// <returns></returns>
		public ApplicationResult Section_Delete(int intSectionMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@SectionMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intSectionMID;

				strStoredProcName = "usp_tbl_Section_M_Delete";

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

		#region Insert Section Details
		/// <summary>
		/// To Insert details of Section in tbl_Section_M table
		/// Created By : NafisaMulla, 4/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="objSectionBO"></param>
		/// <returns></returns>
		public ApplicationResult Section_Insert(SectionBO objSectionBO)
		{
			try
			{
                pSqlParameter = new SqlParameter[6];


                pSqlParameter[0] = new SqlParameter("@SectionName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objSectionBO.SectionName;

                pSqlParameter[1] = new SqlParameter("@SectionAvbbreviation", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objSectionBO.SectionAvbbreviation;

                pSqlParameter[2] = new SqlParameter("@Description", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objSectionBO.Description;

                pSqlParameter[3] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objSectionBO.LastModifiedUserID;

                pSqlParameter[4] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objSectionBO.LastModifiedDate;

                pSqlParameter[5] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objSectionBO.IsDeleted;

				sSql = "usp_tbl_Section_M_Insert";
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
				objSectionBO = null;
			}
		}
		#endregion

		#region Update Section Details
		/// <summary>
		/// To Update details of Section in tbl_Section_M table
		/// Created By : NafisaMulla, 4/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="objSectionBO"></param>
		/// <returns></returns>
		public ApplicationResult Section_Update(SectionBO objSectionBO)
		{
			try
			{
                pSqlParameter = new SqlParameter[7];


                pSqlParameter[0] = new SqlParameter("@SectionMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objSectionBO.SectionMID;

                pSqlParameter[1] = new SqlParameter("@SectionName", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objSectionBO.SectionName;

                pSqlParameter[2] = new SqlParameter("@SectionAvbbreviation", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objSectionBO.SectionAvbbreviation;

                pSqlParameter[3] = new SqlParameter("@Description", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objSectionBO.Description;

                pSqlParameter[4] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objSectionBO.LastModifiedUserID;

                pSqlParameter[5] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objSectionBO.LastModifiedDate;

                pSqlParameter[6] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objSectionBO.IsDeleted;


				sSql = "usp_tbl_Section_M_Update";
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
				objSectionBO = null;
			}
		}
		#endregion

        #region Validate For Delete
        public ApplicationResult Validate_Section_Delete(int intSectionID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@SectionMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSectionID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_Section_M_Validate_For_Delete";

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

		#region Select Section Details by SectionName
		/// <summary>
		/// Select all details of Section for selected SectionName from tbl_Section_M table
		/// Created By : NafisaMulla, 4/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="SectionName"></param>
		/// <returns></returns>
		public ApplicationResult Section_Select_bySectionName(string strSectionName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@SectionName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strSectionName;

				strStoredProcName = "usp_tbl_Section_M_Select_BySection";

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

		#region ValidateName for Section
		/// <summary>
		/// Function which validates whether the SectionName already exits in tbl_Section_M table.
		/// Created By : NafisaMulla, 4/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="strSectionName"></param>
		/// <returns></returns>
		public ApplicationResult Section_ValidateName(string strSectionName,string strSectionAbbrev, int intSectionMID, int intSchoolMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[4];

				pSqlParameter[0] = new SqlParameter("@SectionName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strSectionName;

                pSqlParameter[1] = new SqlParameter("@SectionAvbbreviation", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strSectionAbbrev;

                pSqlParameter[2] = new SqlParameter("@SectionMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSectionMID;

                pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_Section_M_ValidationName";

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
		
		#region Select All SectionT Details
		/// <summary>
		/// To Select All data from the SectionT table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public ApplicationResult SectionT_SelectAll()
		{
			try
			{
				sSql = "usp_tbl__Section_T_SelectAll";
				DataTable dtSectionT = new DataTable();
				dtSectionT = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

				ApplicationResult objResults = new ApplicationResult(dtSectionT);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region Select SectionT Details by SectionTID
		/// <summary>
		/// Select all details of SectionT for selected SectionTID from SectionT table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="intSectionTID"></param>
		/// <returns></returns>
		public ApplicationResult SectionT_Select(int intSectionTID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@SectionTID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intSectionTID;

				strStoredProcName = "usp_tbl_Section_T_Select";

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

		#region Delete SectionT Details by SectionTID
		/// <summary>
		/// To Delete details of SectionT for selected SectionTID from SectionT table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="intSectionTID"></param>
		/// <returns></returns>
		public ApplicationResult SectionT_Delete(int intSectionTID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@SectionTID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intSectionTID;

				strStoredProcName = "usp_tbl_Section_T_Delete";

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

		#region Insert SectionT Details
		/// <summary>
		/// To Insert details of SectionT in SectionT table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="objSectionTBO"></param>
		/// <returns></returns>
		public ApplicationResult SectionT_Insert(SectionTBO objSectionTBO)
		{
			try
			{
				pSqlParameter = new SqlParameter[6];


				pSqlParameter[0] = new SqlParameter("@SectionMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = objSectionTBO.SectionMID;

				pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = objSectionTBO.SchoolMID;

				pSqlParameter[2] = new SqlParameter("@MediumMID", SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = objSectionTBO.MediumMID;

				pSqlParameter[3] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = objSectionTBO.LastModifiedUserID;

				pSqlParameter[4] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = objSectionTBO.LastModifiedDate;

				pSqlParameter[5] = new SqlParameter("@IsDeleted", SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
				pSqlParameter[5].Value = objSectionTBO.IsDeleted;


				sSql = "usp_tbl_Section_T_Insert";
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
				objSectionTBO = null;
			}
		}
		#endregion

		#region Update SectionT Details
		/// <summary>
		/// To Update details of SectionT in SectionT table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="objSectionTBO"></param>
		/// <returns></returns>
		public ApplicationResult SectionT_Update(SectionTBO objSectionTBO)
		{
			try
			{
				pSqlParameter = new SqlParameter[7];


				pSqlParameter[0] = new SqlParameter("@SectionTID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = objSectionTBO.SectionTID;

				pSqlParameter[1] = new SqlParameter("@SectionMID", SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = objSectionTBO.SectionMID;

				pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = objSectionTBO.SchoolMID;

				pSqlParameter[3] = new SqlParameter("@MediumMID", SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = objSectionTBO.MediumMID;

				pSqlParameter[4] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = objSectionTBO.LastModifiedUserID;

				pSqlParameter[5] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
				pSqlParameter[5].Direction = ParameterDirection.Input;
				pSqlParameter[5].Value = objSectionTBO.LastModifiedDate;

				pSqlParameter[6] = new SqlParameter("@IsDeleted", SqlDbType.Int);
				pSqlParameter[6].Direction = ParameterDirection.Input;
				pSqlParameter[6].Value = objSectionTBO.IsDeleted;


				sSql = "usp_tbl_Section_T_Update";
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
				objSectionTBO = null;
			}
		}
		#endregion

		#region Select SectionT Details by SectionTName
		/// <summary>
		/// Select all details of SectionT for selected SectionTName from SectionT table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="SectionTName"></param>
		/// <returns></returns>
		public ApplicationResult SectionT_Select_bySectionTName(string strSectionTName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@SectionTName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strSectionTName;

				strStoredProcName = "usp_tbl_Section_T_Select_BySectionT";

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

		#region ValidateName for SectionT
		/// <summary>
		/// Function which validates whether the SectionTName already exits in SectionT table.
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="strSectionTName"></param>
		/// <returns></returns>
		public ApplicationResult SectionT_ValidateName(string strSectionTName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@SectionTName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strSectionTName;

				strStoredProcName = "usp_tbl_Section_T_Validate_SectionTName";

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

		#region Select SectionID After Insertion
		/// <summary>
		/// Select all details of SectionT for selected SectionTID from SectionT table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="intSectionTID"></param>
		/// <returns></returns>
		public ApplicationResult Section_Select_SectionID_ByName(String strName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@SectionName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strName;

				strStoredProcName = "usp_tbl_Section_M_Name";

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


