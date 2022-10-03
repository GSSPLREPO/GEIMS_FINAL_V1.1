using System.Data;
using System.Data.SqlClient;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
	/// <summary>
    /// Class Created By : Viral, 10/29/2014
	/// Summary description for Organisation.
    /// </summary>
	public class SerialNoBl 
	{
		#region user defined variables
        public string SSql;
        public string StrStoredProcName;
        public SqlParameter[] PSqlParameter = null;
        #endregion
				
		#region Select All SerialNo Details

	    /// <summary>
	    /// To Select All data from the tbl_SerialNoInit_M table
	    /// Created By : Viral, 10/29/2014
	    /// Modified By :
	    /// </summary>
	    /// <param name="intTrustId"></param>
	    /// <param name="intSchoolId"></param>
	    /// <returns></returns>
	    public ApplicationResult  SerialNo_SelectAll(int intTrustId, int intSchoolId)
        {
            PSqlParameter = new SqlParameter[2];

            PSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = intTrustId
            };

	        PSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int)
	        {
	            Direction = ParameterDirection.Input,
	            Value = intSchoolId
	        };

	        SSql = "usp_tbl_SerialNoInit_M_SelectAll";
	        DataTable dtSerialNo = Database.ExecuteDataTable(CommandType.StoredProcedure, SSql, PSqlParameter);

	        var objResults = new ApplicationResult(dtSerialNo) {status = ApplicationResult.CommonStatusType.SUCCESS};
	        return objResults;
        }
        #endregion
		
		#region Select SerialNo Details by Id
        /// <summary>
        /// Select all details of SerialNo for selected Id from tbl_SerialNoInit_M table
        /// Created By : Viral, 10/29/2014
		/// Modified By :
        /// </summary>
        /// <param name="intId"></param>
        /// <returns></returns>
		public ApplicationResult SerialNo_Select(int intId)
		{
            PSqlParameter = new SqlParameter[1];
				
            PSqlParameter[0] = new SqlParameter("@Id", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = intId
            };

            StrStoredProcName = "usp_tbl_SerialNoInit_M_Select";

            DataTable dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, StrStoredProcName, PSqlParameter);
            var objResults = new ApplicationResult(dtResult) {status = ApplicationResult.CommonStatusType.SUCCESS};
            return objResults;
		}
        #endregion
		
		#region Delete SerialNo Details by Id
        /// <summary>
        /// To Delete details of SerialNo for selected Id from tbl_SerialNoInit_M table
        /// Created By : Viral, 10/29/2014
		/// Modified By :
        /// </summary>
        /// <param name="intId"></param>
        /// <returns></returns>
		public ApplicationResult SerialNo_Delete(int intId)
		{
            PSqlParameter = new SqlParameter[1];
				
            PSqlParameter[0] = new SqlParameter("@Id", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = intId
            };

            StrStoredProcName = "usp_tbl_SerialNoInit_M_Delete";
				
            int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, StrStoredProcName, PSqlParameter);
            if (iResult > 0)
            {
                var objResults = new ApplicationResult {status = ApplicationResult.CommonStatusType.SUCCESS};
                return objResults;
            }
            else
            {
                var objResults = new ApplicationResult {status = ApplicationResult.CommonStatusType.FAILURE};
                return objResults;
            }
		}
        #endregion
		
		#region Insert SerialNo Details
		/// <summary>
        /// To Insert details of SerialNo in tbl_SerialNoInit_M table
        /// Created By : Viral, 10/29/2014
		/// Modified By :
        /// </summary>
        /// <param name="objSerialNoBo"></param>
        /// <returns></returns>
        public ApplicationResult SerialNo_Insert(SerialNoBo objSerialNoBo)
        {
		    PSqlParameter = new SqlParameter[10];
                
				
		    PSqlParameter[0] = new SqlParameter("@TrustMID",SqlDbType.Int)
		    {
		        Direction = ParameterDirection.Input,
		        Value = objSerialNoBo.TrustMID
		    };

		    PSqlParameter[1] = new SqlParameter("@SchoolMID",SqlDbType.Int)
		    {
		        Direction = ParameterDirection.Input,
		        Value = objSerialNoBo.SchoolMID
		    };

		    PSqlParameter[2] = new SqlParameter("@StartNo",SqlDbType.Int)
		    {
		        Direction = ParameterDirection.Input,
		        Value = objSerialNoBo.StartNo
		    };

		    PSqlParameter[3] = new SqlParameter("@Year",SqlDbType.Int)
		    {
		        Direction = ParameterDirection.Input,
		        Value = objSerialNoBo.Year
		    };

		    PSqlParameter[4] = new SqlParameter("@EntryType",SqlDbType.NVarChar)
		    {
		        Direction = ParameterDirection.Input,
		        Value = objSerialNoBo.EntryType
		    };

		    PSqlParameter[5] = new SqlParameter("@CreatedBy",SqlDbType.Int)
		    {
		        Direction = ParameterDirection.Input,
		        Value = objSerialNoBo.CreatedBy
		    };

		    PSqlParameter[6] = new SqlParameter("@CreatedDate",SqlDbType.NVarChar)
		    {
		        Direction = ParameterDirection.Input,
		        Value = objSerialNoBo.CreatedDate
		    };

		    PSqlParameter[7] = new SqlParameter("@IsDeleted",SqlDbType.Int)
		    {
		        Direction = ParameterDirection.Input,
		        Value = objSerialNoBo.IsDeleted
		    };

		    PSqlParameter[8] = new SqlParameter("@LastModifideDate",SqlDbType.NVarChar)
		    {
		        Direction = ParameterDirection.Input,
		        Value = objSerialNoBo.LastModifideDate
		    };

		    PSqlParameter[9] = new SqlParameter("@LastModifideUserID",SqlDbType.Int)
		    {
		        Direction = ParameterDirection.Input,
		        Value = objSerialNoBo.LastModifideUserID
		    };


		    SSql = "usp_tbl_SerialNoInit_M_Insert";
		    int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, SSql, PSqlParameter);
				
		    if (iResult > 0)
		    {
		        var objResults = new ApplicationResult {status = ApplicationResult.CommonStatusType.SUCCESS};
		        return objResults;
		    }
		    else
		    {
		        var objResults = new ApplicationResult {status = ApplicationResult.CommonStatusType.FAILURE};
		        return objResults;
		    }
        }
        #endregion
		
		#region Update SerialNo Details
		/// <summary>
        /// To Update details of SerialNo in tbl_SerialNoInit_M table
        /// Created By : Viral, 10/29/2014
		/// Modified By :
        /// </summary>
        /// <param name="objSerialNoBo"></param>
        /// <returns></returns>
        public ApplicationResult SerialNo_Update(SerialNoBo objSerialNoBo)
        {
		    PSqlParameter = new SqlParameter[11];
                
				
		    PSqlParameter[0] = new SqlParameter("@Id",SqlDbType.Int)
		    {
		        Direction = ParameterDirection.Input,
		        Value = objSerialNoBo.Id
		    };

		    PSqlParameter[1] = new SqlParameter("@TrustMID",SqlDbType.Int)
		    {
		        Direction = ParameterDirection.Input,
		        Value = objSerialNoBo.TrustMID
		    };

		    PSqlParameter[2] = new SqlParameter("@SchoolMID",SqlDbType.Int)
		    {
		        Direction = ParameterDirection.Input,
		        Value = objSerialNoBo.SchoolMID
		    };

		    PSqlParameter[3] = new SqlParameter("@StartNo",SqlDbType.Int)
		    {
		        Direction = ParameterDirection.Input,
		        Value = objSerialNoBo.StartNo
		    };

		    PSqlParameter[4] = new SqlParameter("@Year",SqlDbType.Int)
		    {
		        Direction = ParameterDirection.Input,
		        Value = objSerialNoBo.Year
		    };

		    PSqlParameter[5] = new SqlParameter("@EntryType",SqlDbType.NVarChar)
		    {
		        Direction = ParameterDirection.Input,
		        Value = objSerialNoBo.EntryType
		    };

		    PSqlParameter[6] = new SqlParameter("@CreatedBy",SqlDbType.Int)
		    {
		        Direction = ParameterDirection.Input,
		        Value = objSerialNoBo.CreatedBy
		    };

		    PSqlParameter[7] = new SqlParameter("@CreatedDate",SqlDbType.NVarChar)
		    {
		        Direction = ParameterDirection.Input,
		        Value = objSerialNoBo.CreatedDate
		    };

		    PSqlParameter[8] = new SqlParameter("@IsDeleted",SqlDbType.Int)
		    {
		        Direction = ParameterDirection.Input,
		        Value = objSerialNoBo.IsDeleted
		    };

		    PSqlParameter[9] = new SqlParameter("@LastModifideDate",SqlDbType.NVarChar)
		    {
		        Direction = ParameterDirection.Input,
		        Value = objSerialNoBo.LastModifideDate
		    };

		    PSqlParameter[10] = new SqlParameter("@LastModifideUserID",SqlDbType.Int)
		    {
		        Direction = ParameterDirection.Input,
		        Value = objSerialNoBo.LastModifideUserID
		    };


		    SSql = "usp_tbl_SerialNoInit_M_Update";
		    var iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, SSql, PSqlParameter);
				
		    if (iResult > 0)
		    {
		        var objResults = new ApplicationResult {status = ApplicationResult.CommonStatusType.SUCCESS};
		        return objResults;
		    }
		    else
		    {
		        var objResults = new ApplicationResult {status = ApplicationResult.CommonStatusType.FAILURE};
		        return objResults;
		    }
        }
        #endregion
					
		#region Select SerialNo Details by SerialNoName

	    /// <summary>
	    /// Select all details of SerialNo for selected SerialNoName from tbl_SerialNoInit_M table
	    /// Created By : Viral, 10/29/2014
	    /// Modified By :
	    /// </summary>
	    /// <returns></returns>
	    public ApplicationResult SerialNo_Select_bySerialNoName(string strSerialNoName)
		{
	        PSqlParameter = new SqlParameter[1];
				
	        PSqlParameter[0] = new SqlParameter("@SerialNoName", SqlDbType.VarChar)
	        {
	            Direction = ParameterDirection.Input,
	            Value = strSerialNoName
	        };

	        StrStoredProcName = "usp_tbl_SerialNoInit_M_Select_BySerialNo";

	        DataTable dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, StrStoredProcName, PSqlParameter);
	        var objResults = new ApplicationResult(dtResult) {status = ApplicationResult.CommonStatusType.SUCCESS};
	        return objResults;
		}
        #endregion
				
		#region ValidateName for SerialNo 

	    /// <summary>
	    /// Function which validates whether the SerialNoName already exits in tbl_SerialNoInit_M table.
	    /// Created By : Viral, 10/29/2014
	    /// Modified By :
	    /// </summary>
	    /// <returns></returns>
	    public ApplicationResult SerialNo_ValidateName(int intId, int intTrustId, int intSchoolId, int intYear, string strEntryType)
		{
            PSqlParameter = new SqlParameter[5];
				
            PSqlParameter[0] = new SqlParameter("@Id", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = intId
            };

            PSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = intTrustId
            };

            PSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = intSchoolId
            };

            PSqlParameter[3] = new SqlParameter("@Year", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = intYear
            };

            PSqlParameter[4] = new SqlParameter("@EntryType", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = strEntryType
            };

            StrStoredProcName = "usp_tbl_SerialNoInit_M_ValidateName";

            var dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, StrStoredProcName, PSqlParameter);
            var objResults = new ApplicationResult(dtResult) {status = ApplicationResult.CommonStatusType.SUCCESS};
            return objResults;
		}
        #endregion
				
	}
}
