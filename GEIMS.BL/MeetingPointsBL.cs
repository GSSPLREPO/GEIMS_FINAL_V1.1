using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEIMS.BO;
using GEIMS.Common;
using GEIMS.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace GEIMS.BL
{
    public class MeetingPointsBL
    {
        #region User Defined Variables
        public SqlParameter[] pSqlParameter = null;
        public string strStoredProcName;
        #endregion

        #region Fetch Meeting Topic for bind Meeting Dropdown
        public ApplicationResult MeetingTopic_SelectAll(int intTrustMID, int intMonth, int intYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@intMonth", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intMonth;

                pSqlParameter[2] = new SqlParameter("@intYear", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intYear;

                strStoredProcName = "usp_tbl_MeetingTopic_SelectAll";
                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResult = new ApplicationResult(dtResult);
                objResult.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select Paticipant on Meeting Selected Index Change for Dropdown

        public ApplicationResult Participant_SelectAll_ForDropDown(int intMeetingID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@MeetingID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMeetingID;

                strStoredProcName = "usp_tbl_Participant_SelectAll_ForDropDown";

                DataTable dtDepartment = new DataTable();
                dtDepartment = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);


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

        #region Select Employee Name on Meeting Selected Index Change for Dropdown

        public ApplicationResult AssignTo_SelectAll_ForDropDown(int intMeetingID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@MeetingID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMeetingID;

                strStoredProcName = "usp_tbl_AssignTo_SelectAll_ForDropDown";

                DataTable dtDepartment = new DataTable();
                dtDepartment = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);

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

        #region Validate Meeting Point for Meeting Agenda
        public ApplicationResult MeetingPoint_Validate(int intTrustMID, int intMeetingID, string strPoint)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@MeetingID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intMeetingID;

                pSqlParameter[2] = new SqlParameter("@Point", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strPoint;

                strStoredProcName = "usp_tbl_MeetingPoint_Validate";

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

        #region Insert Meeting Point Agenda Wise Details
        public ApplicationResult MeetingPoint_Insert(MeetingPointsBO objMeetingPointsBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[10];


                pSqlParameter[0] = new SqlParameter("@MeetingID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objMeetingPointsBO.MeetingID;

                pSqlParameter[1] = new SqlParameter("@ParticipantID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objMeetingPointsBO.ParticipantID;

                pSqlParameter[2] = new SqlParameter("@Point", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objMeetingPointsBO.Point;

                pSqlParameter[3] = new SqlParameter("@AssignTo", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objMeetingPointsBO.AssignTo;

                pSqlParameter[4] = new SqlParameter("@Action", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objMeetingPointsBO.Action;

                pSqlParameter[5] = new SqlParameter("@TargetDate", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objMeetingPointsBO.TargetDate;

                pSqlParameter[6] = new SqlParameter("@Status", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objMeetingPointsBO.Status;

                pSqlParameter[7] = new SqlParameter("@Remarks", SqlDbType.NVarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objMeetingPointsBO.Remarks;

                pSqlParameter[8] = new SqlParameter("@CreatedByID", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objMeetingPointsBO.CreatedByID;

                pSqlParameter[9] = new SqlParameter("@CreatedByDate", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objMeetingPointsBO.CreatedByDate;

                strStoredProcName = "usp_tbl_MeetingPoint_Insert";
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
            finally
            {
                objMeetingPointsBO = null;
            }
        }
        #endregion

        #region Update Method For ScheduledEvent
        public ApplicationResult MeetingPoints_Update(MeetingPointsBO objMeetingPointsBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[11];

                pSqlParameter[0] = new SqlParameter("@PointID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objMeetingPointsBO.PointID;

                pSqlParameter[1] = new SqlParameter("@MeetingID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objMeetingPointsBO.MeetingID;

                pSqlParameter[2] = new SqlParameter("@ParticipantID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objMeetingPointsBO.ParticipantID;

                pSqlParameter[3] = new SqlParameter("@Point", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objMeetingPointsBO.Point;

                pSqlParameter[4] = new SqlParameter("@AssignTo", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objMeetingPointsBO.AssignTo;

                pSqlParameter[5] = new SqlParameter("@Action", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objMeetingPointsBO.Action;

                pSqlParameter[6] = new SqlParameter("@TargetDate", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objMeetingPointsBO.TargetDate;

                pSqlParameter[7] = new SqlParameter("@Status", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objMeetingPointsBO.Status;

                pSqlParameter[8] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objMeetingPointsBO.Remarks;

                pSqlParameter[9] = new SqlParameter("@LastModifiedByID", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objMeetingPointsBO.LastModifiedByID;

                pSqlParameter[10] = new SqlParameter("@LastModifiedByDate", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objMeetingPointsBO.LastModifiedByDate;

                strStoredProcName = "usp_tbl_MeetingPoints_Update";

                int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                if (iResult > 0)
                {
                    ApplicationResult objResult = new ApplicationResult();
                    objResult.status = ApplicationResult.CommonStatusType.SUCCESS;
                    return objResult;
                }
                else
                {
                    ApplicationResult objResult = new ApplicationResult();
                    objResult.status = ApplicationResult.CommonStatusType.FAILURE;
                    return objResult;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ScheduledEvent Details For Delete

        public ApplicationResult MeetingPoint_Delete(int intPointID, int intLastModifiedByID, string strLastModifiedByDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@PointID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intPointID;

                pSqlParameter[1] = new SqlParameter("@LastModifiedByID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastModifiedByID;

                pSqlParameter[2] = new SqlParameter("@LastModifiedByDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifiedByDate;

                strStoredProcName = "usp_tbl_MeetingPoint_Delete";

                int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);

                if (iResult > 0)
                {
                    ApplicationResult objResult = new ApplicationResult();
                    objResult.status = ApplicationResult.CommonStatusType.SUCCESS;
                    return objResult;
                }
                else
                {
                    ApplicationResult objResult = new ApplicationResult();
                    objResult.status = ApplicationResult.CommonStatusType.FAILURE;
                    return objResult;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Bind GridView Meeting Points Details

        public ApplicationResult GridviewMeetingPoints_SelectAll(int intTrustMID, int intMeetingID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@MeetingID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intMeetingID;

                strStoredProcName = "usp_tbl_GridviewMeetingPoints_SelectAll";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResult = new ApplicationResult(dtResult);
                objResult.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Fetch Meeting Points Data for Edit
        public ApplicationResult PointID_Select_For_Edit(int intPointID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@PointID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intPointID;

                strStoredProcName = "usp_tbl_PointID_Select_For_Edit";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResult = new ApplicationResult(dtResult);
                objResult.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Fetch Pending Point for Bind Gridview of Report
        public ApplicationResult FetchPendingPointForReport(int intMeetingID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@MeetingID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMeetingID;

                strStoredProcName = "usp_tbl_FetchPendingPointForReport";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResult = new ApplicationResult(dtResult);
                objResult.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
