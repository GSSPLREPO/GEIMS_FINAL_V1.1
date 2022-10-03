using System;
using System.Data;
using System.Data.SqlClient;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
	/// <summary>
	/// Class Created By : NafisaMulla, 2/7/2014
	/// Summary description for Organisation.
	/// </summary>
	public class ClassBL
	{
		#region user defined variables
		public string sSql;
		public string strStoredProcName;
		public SqlParameter[] pSqlParameter = null;
		#endregion

		#region Select All Class Details with division
		/// <summary>
		/// To Select All data from the tbl_Section_M table
		/// Created By : NafisaMulla, 4/7/2014
		/// Modified By :
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public ApplicationResult Class_SelectAll_WithDivision(int intSchoolMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

				sSql = "usp_tbl_Class_M_SelectAll_WithDivision";

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

        #region Select All Class Details Section Wise
        /// <summary>
        /// To Select All data from the tbl_Section_M table
        /// Created By : NafisaMulla, 4/7/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult Class_SelectAll_SectionWise(int intSectionMID , int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@SectionTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSectionMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                sSql = "usp_tbl_Class_M_SectionWise";

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

        #region Select All Class Details Section Wise
        /// <summary>
        /// To Select All data from the tbl_Section_M table
        /// Created By : NafisaMulla, 4/7/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult Class_SelectAll_SectionWise_ForDropDown(int intSectionMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@SectionTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSectionMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                sSql = "usp_tbl_Class_M_SelectAll_ForDropDown";

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

        #region Select All Class Details Employee Wise(Teacher Wise)
        /// <summary>
        /// To Select All data from the tbl_Section_M table
        /// Created By : NafisaMulla, 4/7/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult Class_SelectAll_EmployeeWise(int intEmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

               
                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                sSql = "usp_tbl_Class_M_EmployeeWise";

                DataTable dtClass = new DataTable();
                dtClass = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtClass);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

		#region Select All Class Details
		/// <summary>
		/// To Select All data from the tbl_Class_M table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public ApplicationResult Class_SelectAll(int intSchoolMID)
		{
			try
			{
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

				sSql = "usp_tbl_Class_M_SelectAll";
				DataTable dtClass = new DataTable();
                dtClass = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

				ApplicationResult objResults = new ApplicationResult(dtClass);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

        #region Select All Class Details
        /// <summary>
        /// To Select All data from the tbl_Class_M table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult Class_SelectAll_ForDropDownNotSectionWise(int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                sSql = "usp_tbl_Class_M_SelectAllDropdownNotSectionwise";
                DataTable dtClass = new DataTable();
                dtClass = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtClass);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

		#region Select Class Details by ClassMID
		/// <summary>
		/// Select all details of Class for selected ClassMID from tbl_Class_M table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="intClassMID"></param>
		/// <returns></returns>
		public ApplicationResult Class_Select(int intClassMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intClassMID;

				strStoredProcName = "usp_tbl_Class_M_Select";

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

		#region Delete Class Details by ClassMID
		/// <summary>
		/// To Delete details of Class for selected ClassMID from tbl_Class_M table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="intClassMID"></param>
		/// <returns></returns>
		public ApplicationResult Class_Delete(int intClassMID, int intSchoolMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[2];

				pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intClassMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

				strStoredProcName = "usp_tbl_Class_M_Delete";

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

		#region Insert Class Details
		/// <summary>
		/// To Insert details of Class in tbl_Class_M table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="objClassBO"></param>
		/// <returns></returns>
		public ApplicationResult Class_Insert(ClassBO objClassBO)
		{
			try
			{
				pSqlParameter = new SqlParameter[9];


				pSqlParameter[0] = new SqlParameter("@SectionTID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = objClassBO.SectionTID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objClassBO.SchoolMID;

				pSqlParameter[2] = new SqlParameter("@ClassName", SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = objClassBO.ClassName;

				pSqlParameter[3] = new SqlParameter("@ApprovalDate", SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = objClassBO.ApprovalDate;

				pSqlParameter[4] = new SqlParameter("@ApprovalNo", SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = objClassBO.ApprovalNo;

				pSqlParameter[5] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
				pSqlParameter[5].Value = objClassBO.LastModifiedUserID;

				pSqlParameter[6] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
				pSqlParameter[6].Value = objClassBO.LastModifiedDate;

				pSqlParameter[7] = new SqlParameter("@IsDeleted", SqlDbType.Int);
				pSqlParameter[7].Direction = ParameterDirection.Input;
				pSqlParameter[7].Value = objClassBO.IsDeleted;

                pSqlParameter[8] = new SqlParameter("@NoOfPeriod", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objClassBO.NoOfPeriod;


				sSql = "usp_tbl_Class_M_Insert";
				DataTable dtResult = new DataTable();
				dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);
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
				objClassBO = null;
			}
		}
		#endregion

		#region Update Class Details
		/// <summary>
		/// To Update details of Class in tbl_Class_M table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="objClassBO"></param>
		/// <returns></returns>
		public ApplicationResult Class_Update(ClassBO objClassBO)
		{
			try
			{
				pSqlParameter = new SqlParameter[10];


				pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = objClassBO.ClassMID;

				pSqlParameter[1] = new SqlParameter("@SectionTID", SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = objClassBO.SectionTID;

				pSqlParameter[2] = new SqlParameter("@ClassName", SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = objClassBO.ClassName;

				pSqlParameter[3] = new SqlParameter("@ApprovalDate", SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = objClassBO.ApprovalDate;

				pSqlParameter[4] = new SqlParameter("@ApprovalNo", SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = objClassBO.ApprovalNo;

				pSqlParameter[5] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
				pSqlParameter[5].Value = objClassBO.LastModifiedUserID;

				pSqlParameter[6] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
				pSqlParameter[6].Value = objClassBO.LastModifiedDate;

				pSqlParameter[7] = new SqlParameter("@IsDeleted", SqlDbType.Int);
				pSqlParameter[7].Direction = ParameterDirection.Input;
				pSqlParameter[7].Value = objClassBO.IsDeleted;

                pSqlParameter[8] = new SqlParameter("@NoOfPeriod", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objClassBO.NoOfPeriod;

                pSqlParameter[9] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objClassBO.SchoolMID;


				sSql = "usp_tbl_Class_M_Update";
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
				objClassBO = null;
			}
		}
		#endregion

        #region Update Class Details For Priority
        /// <summary>
        /// To Update details of Class in tbl_Class_M table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="objClassBO"></param>
        /// <returns></returns>
        public ApplicationResult Class_Update_For_Priority(ClassBO objClassBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[7];

                pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objClassBO.ClassMID;

                pSqlParameter[1] = new SqlParameter("@ClassName", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objClassBO.ClassName;

                pSqlParameter[2] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objClassBO.LastModifiedUserID;

                pSqlParameter[3] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objClassBO.LastModifiedDate;

                pSqlParameter[4] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objClassBO.IsDeleted;

                pSqlParameter[5] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objClassBO.SchoolMID;

                pSqlParameter[6] = new SqlParameter("@Priority", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objClassBO.Priority;

                sSql = "usp_tbl_Class_M_Update_For_Priority";
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
                objClassBO = null;
            }
        }
        #endregion

		#region Select Class Details by ClassName
		/// <summary>
		/// Select all details of Class for selected ClassName from tbl_Class_M table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="ClassName"></param>
		/// <returns></returns>
		public ApplicationResult Class_Select_byClassName(string strClassName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@ClassName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strClassName;

				strStoredProcName = "usp_tbl_Class_M_Select_ByClass";

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

		#region ValidateName for Class
		/// <summary>
		/// Function which validates whether the ClassName already exits in tbl_Class_M table.
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="strClassName"></param>
		/// <returns></returns>
		public ApplicationResult Class_ValidateName(string strClassName, int intSchoolMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[2];

				pSqlParameter[0] = new SqlParameter("@ClassName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strClassName;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

				strStoredProcName = "usp_tbl_Class_M_Validate_ClassName";

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

        #region ValidateName for ClassDivision
        /// <summary>
        /// Function which validates whether the ClassName already exits in tbl_Class_M table.
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="strClassName"></param>
        /// <returns></returns>
        public ApplicationResult Class_ValidateName_ClassDivision(string strClassName, int intSchoolMID ,int intClassMID, string strDivisionName)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@ClassName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strClassName;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                pSqlParameter[2] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intClassMID;

                pSqlParameter[3] = new SqlParameter("@DivisionName", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strDivisionName;

                strStoredProcName = "usp_tbl_Class_M_Validate_ClassDivision";

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

        #region Validate For Delete
        public ApplicationResult Validate_Class_Delete(int intClassID, int intSchoolMID, string strDivisionName)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intClassID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                pSqlParameter[2] = new SqlParameter("@DivisionName", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strDivisionName;

                strStoredProcName = "usp_tbl_Class_M_Validate_For_Delete";

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

        public ApplicationResult Class_SelectAll_SchoolWise_ForDropDown(int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;
              
                strStoredProcName = "usp_tbl_Class_M_SelectAll_SchoolWise_ForDropDown";

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

		//Using Fees Collection Form
		#region Select All Class Details Section Wise
		public ApplicationResult Find_Class_SectionWise(int intSchoolMID, int intSectionMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[2];

				pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intSchoolMID;

				pSqlParameter[1] = new SqlParameter("@SectionMID", SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = intSectionMID;

				sSql = "usp_tbl_Find_Class_SectionWise";

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

		#region Select All Division Details Class Wise
		public ApplicationResult Find_Division_ClassWise(int intClassMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intClassMID;

				sSql = "usp_tbl_Find_Division_ClassWise";

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
	}
}


