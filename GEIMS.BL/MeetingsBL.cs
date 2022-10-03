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
    public class MeetingsBL
    {
        // Code for Meeting Detail

        #region User Defined Variables
        public SqlParameter[] pSqlParameter = null;
        public string strStoredProcName;
        #endregion

        #region Insert MeetingCategory Details
        ///<summary>
        ///To insert Details of Meetings in tbl_Meetings Table
        ///Created By Chirag, 20/10/2021
        ///Modifird BY :
        ///</summary>
        ///<param name="objMeetingsBO"></param>
        ///<returns></returns>

        public ApplicationResult Meetings_Insert(MeetingsBO objMeetingsBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[13];

                pSqlParameter[0] = new SqlParameter("@CategoryID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objMeetingsBO.CategoryID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objMeetingsBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@Topic", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objMeetingsBO.Topic;

                pSqlParameter[3] = new SqlParameter("@MeetingDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objMeetingsBO.MeetingDate;

                pSqlParameter[4] = new SqlParameter("@FromTime", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objMeetingsBO.FromTime;

                pSqlParameter[5] = new SqlParameter("@ToTime", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objMeetingsBO.Totime;

                pSqlParameter[6] = new SqlParameter("@Venue", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objMeetingsBO.Venue;

                pSqlParameter[7] = new SqlParameter("@Mode", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objMeetingsBO.Mode;

                pSqlParameter[8] = new SqlParameter("@OrganizeBy", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objMeetingsBO.OrganizeBy;

                pSqlParameter[9] = new SqlParameter("@Status", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objMeetingsBO.Status;

                pSqlParameter[10] = new SqlParameter("@MinutesBy", SqlDbType.Int);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objMeetingsBO.MinutesBy;

                pSqlParameter[11] = new SqlParameter("@CreatedByID", SqlDbType.Int);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objMeetingsBO.CreatedByID;

                pSqlParameter[12] = new SqlParameter("@CreatedByDate", SqlDbType.VarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objMeetingsBO.CreatedByDate;

                strStoredProcName = "usp_tbl_Meetings_Insert";

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

        #region Validate Topic of Meeting on Same Date
        public ApplicationResult Meetings_Validate(int intTrustMID,int intCategoryID, string strTopic, string strMeetingDate, string strFromTime, string strToTime, string strVenue, string strMode, int intMinitusBy)
        {
            try
            {
                pSqlParameter = new SqlParameter[9];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@CategoryID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intCategoryID;

                pSqlParameter[2] = new SqlParameter("@Topic", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strTopic;

                pSqlParameter[3] = new SqlParameter("@MeetingDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strMeetingDate;

                pSqlParameter[4] = new SqlParameter("@FromTime", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = strFromTime;

                pSqlParameter[5] = new SqlParameter("@ToTime", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = strToTime;

                pSqlParameter[6] = new SqlParameter("@Venue", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = strVenue;

                pSqlParameter[7] = new SqlParameter("@Mode", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = strMode;

                pSqlParameter[8] = new SqlParameter("@MinutesBy", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = intMinitusBy;

                strStoredProcName = "usp_tbl_MeetingsTopicOnSameDateTime_Validate";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResult = new ApplicationResult(dtResult);
                if (dtResult.Rows.Count > 0)
                {
                    objResult.status = ApplicationResult.CommonStatusType.SUCCESS;
                    return objResult;
                }
                else
                {
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

        #region Fetch MeetingCategory Name for bind Meeting Cateogry Dropdown
        ///<summary>
        ///To Fetch MeetingCategory Name for bind Dropdown
        ///Created By Chirag, 21/10/2021
        ///Modifird BY :
        ///</summary>
        ///<param name=""></param>
        ///<returns></returns>

        public ApplicationResult MeetingCategory_SelectAll(int intTrustMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                strStoredProcName = "usp_tbl_MeetingCategory_SelectAll";
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

        #region Fetch Employee Name for bind MinutesBy Dropdown
        ///<summary>
        ///To Fetch Employee Name for bind Dropdown
        ///Created By Chirag, 21/10/2021
        ///Modifird BY :
        ///</summary>
        ///<param name=""></param>
        ///<returns></returns>

        public ApplicationResult MinutesBy_SelectAll(int intTrustMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                strStoredProcName = "usp_tbl_MinutesBy_SelectAll";

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

        #region Fetch Meetings Data for Edit by MeetingID
        public ApplicationResult MeetingID_Select_For_Edit(int intMeetingID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@MeetingID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMeetingID;

                strStoredProcName = "usp_tbl_MeetingID_Select_For_Edit";

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

        #region Delete Meetings by MeetingID
        public ApplicationResult Meetings_Delete(int intMeetingID, int intLastModifiedByID, string strLastModifiedByDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@MeetingID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMeetingID;

                pSqlParameter[1] = new SqlParameter("@LastModifiedByID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastModifiedByID;

                pSqlParameter[2] = new SqlParameter("@LastModifiedByDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifiedByDate;

                strStoredProcName = "usp_tbl_Meetings_Delete";

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

        #region Bind GridView Meeting Details

       /* public ApplicationResult GridviewMeeting_SelectAll(int intTrustMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                strStoredProcName = "usp_tbl_GridviewMeeting_SelectAll";

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
        }*/


        public ApplicationResult GridviewMeeting_SelectAll(int intTrustMID, int intMonth, int intYear)
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

                strStoredProcName = "usp_tbl_GridviewMeeting_SelectAll";

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

        #region Update Meetings Details
        public ApplicationResult Meetings_Update(MeetingsBO objMeetingsBO)
        {
            pSqlParameter = new SqlParameter[13];

            pSqlParameter[0] = new SqlParameter("@MeetingID", SqlDbType.Int);
            pSqlParameter[0].Direction = ParameterDirection.Input;
            pSqlParameter[0].Value = objMeetingsBO.MeetingID;

            pSqlParameter[1] = new SqlParameter("@CategoryID", SqlDbType.Int);
            pSqlParameter[1].Direction = ParameterDirection.Input;
            pSqlParameter[1].Value = objMeetingsBO.CategoryID;

            pSqlParameter[2] = new SqlParameter("@Topic", SqlDbType.VarChar);
            pSqlParameter[2].Direction = ParameterDirection.Input;
            pSqlParameter[2].Value = objMeetingsBO.Topic;

            pSqlParameter[3] = new SqlParameter("@MeetingDate", SqlDbType.VarChar);
            pSqlParameter[3].Direction = ParameterDirection.Input;
            pSqlParameter[3].Value = objMeetingsBO.MeetingDate;

            pSqlParameter[4] = new SqlParameter("@FromTime", SqlDbType.VarChar);
            pSqlParameter[4].Direction = ParameterDirection.Input;
            pSqlParameter[4].Value = objMeetingsBO.FromTime;

            pSqlParameter[5] = new SqlParameter("@ToTime", SqlDbType.VarChar);
            pSqlParameter[5].Direction = ParameterDirection.Input;
            pSqlParameter[5].Value = objMeetingsBO.Totime;

            pSqlParameter[6] = new SqlParameter("@Venue", SqlDbType.VarChar);
            pSqlParameter[6].Direction = ParameterDirection.Input;
            pSqlParameter[6].Value = objMeetingsBO.Venue;

            pSqlParameter[7] = new SqlParameter("@Mode", SqlDbType.VarChar);
            pSqlParameter[7].Direction = ParameterDirection.Input;
            pSqlParameter[7].Value = objMeetingsBO.Mode;

            pSqlParameter[8] = new SqlParameter("@OrganizeBy", SqlDbType.Int);
            pSqlParameter[8].Direction = ParameterDirection.Input;
            pSqlParameter[8].Value = objMeetingsBO.OrganizeBy;

            pSqlParameter[9] = new SqlParameter("@Status", SqlDbType.VarChar);
            pSqlParameter[9].Direction = ParameterDirection.Input;
            pSqlParameter[9].Value = objMeetingsBO.Status;

            pSqlParameter[10] = new SqlParameter("@MinutesBy", SqlDbType.Int);
            pSqlParameter[10].Direction = ParameterDirection.Input;
            pSqlParameter[10].Value = objMeetingsBO.MinutesBy;

            pSqlParameter[11] = new SqlParameter("@LastModifiedByID", SqlDbType.Int);
            pSqlParameter[11].Direction = ParameterDirection.Input;
            pSqlParameter[11].Value = objMeetingsBO.LastModifiedByID;

            pSqlParameter[12] = new SqlParameter("@LastModifiedByDate", SqlDbType.VarChar);
            pSqlParameter[12].Direction = ParameterDirection.Input;
            pSqlParameter[12].Value = objMeetingsBO.LastModifiedByDate;

            

            strStoredProcName = "usp_tbl_Meetings_Update";

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
        #endregion

        #region Fetch MeetingsID for Recent Record Added
        public ApplicationResult fetchRecentMeetingID(int intTrustMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                strStoredProcName = "usp_tbl_fetchRecentMeetingID";

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

        // Code for Meeting Agenda

        #region Insert MeetingAgenda Details
        public ApplicationResult MeetingAgenda_Insert(MeetingsBO objMeetingsBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@MeetingID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objMeetingsBO.MeetingID;

                pSqlParameter[1] = new SqlParameter("@AgendaPoint", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objMeetingsBO.AgendaPoint;

                pSqlParameter[2] = new SqlParameter("@CreatedByID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objMeetingsBO.CreatedByID;

                pSqlParameter[3] = new SqlParameter("@CreatedByDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objMeetingsBO.CreatedByDate;

                strStoredProcName = "usp_tbl_MeetingAgenda_Insert";

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

        #region Bind GridView Agenda Details

        public ApplicationResult GridviewAgenda_SelectAll(int intMeetingID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@MeetingID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMeetingID;

                strStoredProcName = "usp_tbl_GridviewAgenda_SelectAll";

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

        #region Delete MeetingAgenda by AgendaID
        public ApplicationResult MeetingAgenda_Delete(int intAgendaID, int intLastModifiedByID, string strLastModifiedByDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@AgendaID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intAgendaID;

                pSqlParameter[1] = new SqlParameter("@LastModifiedByID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastModifiedByID;

                pSqlParameter[2] = new SqlParameter("@LastModifiedByDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifiedByDate;

                strStoredProcName = "usp_tbl_MeetingAgenda_Delete";

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

        #region Validate Agenda for Duplication
        public ApplicationResult agendaPoint_Validate(int intMeetingID, string strAgenda)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@MeetingID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMeetingID;

                pSqlParameter[1] = new SqlParameter("@AgendaPoint", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strAgenda;


                strStoredProcName = "usp_tbl_agendaPoint_Validate";

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

        // Code for Meeteing Participant

        #region Validate Employee Name for Same Meeting
        public ApplicationResult EmployeeName_Validate(int intTrustMID,int intEmployeeID, string strMeetingTopic)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intEmployeeID;

                pSqlParameter[2] = new SqlParameter("@Topic", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strMeetingTopic;


                strStoredProcName = "usp_tbl_EmployeeName_Validate";

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

        #region Insert MeetingParticipant Details
        public ApplicationResult MeetingParticipantEmployee_Insert(MeetingsBO objMeetingsBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@MeetingID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objMeetingsBO.MeetingID;

                pSqlParameter[1] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objMeetingsBO.EmployeeID;

                pSqlParameter[2] = new SqlParameter("@CreatedByID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objMeetingsBO.CreatedByID;

                pSqlParameter[3] = new SqlParameter("@CreatedByDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objMeetingsBO.CreatedByDate;

                strStoredProcName = "usp_tbl_MeetingParticipantEmployee_Insert";

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

        #region Insert MeetingParticipant Details
        public ApplicationResult MeetingParticipantExternal_Insert(MeetingsBO objMeetingsBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

                pSqlParameter[0] = new SqlParameter("@MeetingID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objMeetingsBO.MeetingID;

                pSqlParameter[1] = new SqlParameter("@Name", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objMeetingsBO.Name;

                pSqlParameter[2] = new SqlParameter("@OrgName", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objMeetingsBO.OrgName;

                pSqlParameter[3] = new SqlParameter("@Email", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objMeetingsBO.Email;

                pSqlParameter[4] = new SqlParameter("@CreatedByID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objMeetingsBO.CreatedByID;

                pSqlParameter[5] = new SqlParameter("@CreatedByDate", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objMeetingsBO.CreatedByDate;

                strStoredProcName = "usp_tbl_MeetingParticipantEmployee_Insert";

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

        #region Bind GridView Agenda Details

        public ApplicationResult GridviewParticipant_SelectAll(int intMeetingID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@MeetingID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMeetingID;

                strStoredProcName = "usp_tbl_GridviewParticipant_SelectAll";

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

        #region Delete MeetingParticipant by ParticipantID
        public ApplicationResult MeetingParticipant_Delete(int intParticipantID, int intLastModifiedByID, string strLastModifiedByDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@ParticipantID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intParticipantID;

                pSqlParameter[1] = new SqlParameter("@LastModifiedByID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastModifiedByID;

                pSqlParameter[2] = new SqlParameter("@LastModifiedByDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifiedByDate;

                strStoredProcName = "usp_tbl_MeetingParticipant_Delete";

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

        #region Meeting Participant Sent Invitaion button click
        public ApplicationResult MeetingParticipant_UpdateRow(string strEmail, int intMarkStatus, int intStatusID, int intMeetingID)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@Email", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strEmail;

                pSqlParameter[1] = new SqlParameter("@SendInvite", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intMarkStatus;

                pSqlParameter[2] = new SqlParameter("@StatusID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intStatusID;

                pSqlParameter[3] = new SqlParameter("@MeetingID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intMeetingID;
                strStoredProcName = "usp_tbl_MeetingParticipant_UpdateRow";

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

        // code for Schedule Meeting Report

        #region Fetch Meeting Name for bind Meeting Name Dropdown
      
        public ApplicationResult MeetingsDetial_SelectAll_For_ScheduleMeeting_Report(int intTrustMID)
        {
            try
            {
                string strFromDate = "";
                string strToDate = "";
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@FromDate", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strFromDate;

                pSqlParameter[2] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strToDate;

                strStoredProcName = "usp_tbl_MeetingsDetial_SelectAll_For_ScheduleMeeting_Report";

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

        // Method Overloading
        public ApplicationResult MeetingsDetial_SelectAll_For_ScheduleMeeting_Report(int intTrustMID, string strFromDate,string strToDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@FromDate", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strFromDate;

                pSqlParameter[2] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strToDate;

                strStoredProcName = "usp_tbl_MeetingsDetial_SelectAll_For_ScheduleMeeting_Report";

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

        #region Schedule Meeting Report
        public ApplicationResult ScheduleMeeting_Report(int intMeetingID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@MeetingID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMeetingID;

                strStoredProcName = "usp_tbl_ScheduleMeeting_Report";

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

        #region Fetch Agenda  for Report Grid
        public ApplicationResult ScheduleMeeting_FetchAgenda(int intMeetingID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@MeetingID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMeetingID;

                strStoredProcName = "usp_tbl_ScheduleMeeting_FetchAgenda";

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

        #region Fetch Participant  for Report Grid
        public ApplicationResult ScheduleMeeting_FetchParticipant(int intMeetingID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@MeetingID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMeetingID;

                strStoredProcName = "usp_tbl_ScheduleMeeting_FetchParticipant";

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

        #region Fetch Meeting Point  for Report Grid
        public ApplicationResult SendMOM_bindMeetingPoint(int intMeetingID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@MeetingID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intMeetingID;

                strStoredProcName = "usp_tbl_SendMOM_bindMeetingPoint";

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
