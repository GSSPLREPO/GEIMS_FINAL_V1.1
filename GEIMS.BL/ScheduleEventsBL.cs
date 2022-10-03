using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEIMS.Common;
using System.Data;
using System.Data.SqlClient;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.DataAccess;

namespace GEIMS.BL
{
    public class ScheduleEventsBL
    {
        #region User Defined variable
        public string strStrordProcedureName;
        public SqlParameter[] pSqlParameter=null;
        #endregion

        #region Select EventCategoryName For Dropdown Event Category
        /// <summary>
        /// To Select EventCategoryName from the tbl_EventCategories table
        /// Created By : Chirag, 29/09/2021
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult Select_EventCategory_ForDropdown(int intTrustMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;


                strStrordProcedureName = "usp_tbl_EventCategoriesName_ForDropdown";

                DataTable dtEventCategory = new DataTable();
                dtEventCategory = Database.ExecuteDataTable(CommandType.StoredProcedure, strStrordProcedureName, pSqlParameter);

                ApplicationResult objResult = new ApplicationResult(dtEventCategory);
                objResult.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResult;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Select SchoolName for Dropdown School Name
        public ApplicationResult Select_SchoolName_ForDropdown(int intTrustMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                strStrordProcedureName = "usp_tbl_SchoolName_ForDropdown";

                DataTable dtSchoolName = new DataTable();
                dtSchoolName = Database.ExecuteDataTable(CommandType.StoredProcedure, strStrordProcedureName, pSqlParameter);

                ApplicationResult objResult = new ApplicationResult(dtSchoolName);
                objResult.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResult;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Validate Event Name
        public ApplicationResult EventName_Validate(int intTrustMID, int intSchoolMID, int intScheduledEventID, string strEventName)
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

                pSqlParameter[2] = new SqlParameter("@ScheduledEventID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intScheduledEventID;

                pSqlParameter[3] = new SqlParameter("@EventName", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strEventName;

                strStrordProcedureName = "usp_tbl_EventName_Validate";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStrordProcedureName, pSqlParameter);
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

        #region Insert Method For ScheduledEvent
        public ApplicationResult ScheduledEvent_Insert(ScheduledEventBO objScheduledEventBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[21];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objScheduledEventBO.TrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objScheduledEventBO.SchoolMID;

                pSqlParameter[2] = new SqlParameter("@SectionMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objScheduledEventBO.SectionMID;

                pSqlParameter[3] = new SqlParameter("@EventName", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objScheduledEventBO.EventName;

                pSqlParameter[4] = new SqlParameter("@EventCategoryID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objScheduledEventBO.EventCategoryID;

                pSqlParameter[5] = new SqlParameter("@EventFromDate", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objScheduledEventBO.EventFromDate;

                pSqlParameter[6] = new SqlParameter("@EventFromDateFromTime", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objScheduledEventBO.EventFromDateFromTime;

                pSqlParameter[7] = new SqlParameter("@EventFromDateToTime", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objScheduledEventBO.EventFromDateToTime;

                pSqlParameter[8] = new SqlParameter("@EventToDate", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objScheduledEventBO.EventToDate;

                pSqlParameter[9] = new SqlParameter("@EventToDateFromTime", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objScheduledEventBO.EventToDateFromTime;

                pSqlParameter[10] = new SqlParameter("@EventToDateToTime", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objScheduledEventBO.EventToDateToTime;

                pSqlParameter[11] = new SqlParameter("@EventPlatform", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objScheduledEventBO.EventPlatform;

                pSqlParameter[12] = new SqlParameter("@EventLocation", SqlDbType.VarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objScheduledEventBO.EventLocation;

                pSqlParameter[13] = new SqlParameter("@EventDescription", SqlDbType.VarChar);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objScheduledEventBO.EventDescription;

                pSqlParameter[14] = new SqlParameter("@EventOrgeniserName", SqlDbType.VarChar);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objScheduledEventBO.EventOrgeniserName;

                pSqlParameter[15] = new SqlParameter("@EventOrgeniserSection", SqlDbType.VarChar);
                pSqlParameter[15].Direction = ParameterDirection.Input;
                pSqlParameter[15].Value = objScheduledEventBO.EventOrgeniserSection;

                pSqlParameter[16] = new SqlParameter("@EventNote", SqlDbType.VarChar);
                pSqlParameter[16].Direction = ParameterDirection.Input;
                pSqlParameter[16].Value = objScheduledEventBO.EventNote;

                pSqlParameter[17] = new SqlParameter("@EventMobileNo", SqlDbType.VarChar);
                pSqlParameter[17].Direction = ParameterDirection.Input;
                pSqlParameter[17].Value = objScheduledEventBO.EventMobileNo;

                pSqlParameter[18] = new SqlParameter("@EventEmail", SqlDbType.VarChar);
                pSqlParameter[18].Direction = ParameterDirection.Input;
                pSqlParameter[18].Value = objScheduledEventBO.EventEmail;

                pSqlParameter[19] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[19].Direction = ParameterDirection.Input;
                pSqlParameter[19].Value = objScheduledEventBO.CreatedUserID;

                pSqlParameter[20] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[20].Direction = ParameterDirection.Input;
                pSqlParameter[20].Value = objScheduledEventBO.CreatedDate;

                strStrordProcedureName = "usp_tbl_ScheduledEvent_Insert";

                int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, strStrordProcedureName, pSqlParameter);
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
            catch(Exception ex)
            {
                throw ex;
            }


        }
        #endregion

        #region Update Method For ScheduledEvent
        public ApplicationResult ScheduledEvent_Update(ScheduledEventBO objScheduledEventBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[22];

                pSqlParameter[0] = new SqlParameter("@ScheduledEventID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objScheduledEventBO.ScheduledEventID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objScheduledEventBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objScheduledEventBO.SchoolMID;

                pSqlParameter[3] = new SqlParameter("@SectionMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objScheduledEventBO.SectionMID;

                pSqlParameter[4] = new SqlParameter("@EventName", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objScheduledEventBO.EventName;

                pSqlParameter[5] = new SqlParameter("@EventCategoryID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objScheduledEventBO.EventCategoryID;

                pSqlParameter[6] = new SqlParameter("@EventFromDate", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objScheduledEventBO.EventFromDate;

                pSqlParameter[7] = new SqlParameter("@EventFromDateFromTime", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objScheduledEventBO.EventFromDateFromTime;

                pSqlParameter[8] = new SqlParameter("@EventFromDateToTime", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objScheduledEventBO.EventFromDateToTime;

                pSqlParameter[9] = new SqlParameter("@EventToDate", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objScheduledEventBO.EventToDate;

                pSqlParameter[10] = new SqlParameter("@EventToDateFromTime", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objScheduledEventBO.EventToDateFromTime;

                pSqlParameter[11] = new SqlParameter("@EventToDateToTime", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objScheduledEventBO.EventToDateToTime;

                pSqlParameter[12] = new SqlParameter("@EventPlatform", SqlDbType.VarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objScheduledEventBO.EventPlatform;

                pSqlParameter[13] = new SqlParameter("@EventLocation", SqlDbType.VarChar);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objScheduledEventBO.EventLocation;

                pSqlParameter[14] = new SqlParameter("@EventDescription", SqlDbType.VarChar);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objScheduledEventBO.EventDescription;

                pSqlParameter[15] = new SqlParameter("@EventOrgeniserName", SqlDbType.VarChar);
                pSqlParameter[15].Direction = ParameterDirection.Input;
                pSqlParameter[15].Value = objScheduledEventBO.EventOrgeniserName;
                
                pSqlParameter[16] = new SqlParameter("@EventOrgeniserSection", SqlDbType.VarChar);
                pSqlParameter[16].Direction = ParameterDirection.Input;
                pSqlParameter[16].Value = objScheduledEventBO.EventOrgeniserSection;

                pSqlParameter[17] = new SqlParameter("@EventNote", SqlDbType.VarChar);
                pSqlParameter[17].Direction = ParameterDirection.Input;
                pSqlParameter[17].Value = objScheduledEventBO.EventNote;

                pSqlParameter[18] = new SqlParameter("@EventMobileNo", SqlDbType.VarChar);
                pSqlParameter[18].Direction = ParameterDirection.Input;
                pSqlParameter[18].Value = objScheduledEventBO.EventMobileNo;

                pSqlParameter[19] = new SqlParameter("@EventEmail", SqlDbType.VarChar);
                pSqlParameter[19].Direction = ParameterDirection.Input;
                pSqlParameter[19].Value = objScheduledEventBO.EventEmail;

                pSqlParameter[20] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[20].Direction = ParameterDirection.Input;
                pSqlParameter[20].Value = objScheduledEventBO.LastModifiedUserID;

                pSqlParameter[21] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[21].Direction = ParameterDirection.Input;
                pSqlParameter[21].Value = objScheduledEventBO.LastModifiedDate;

                strStrordProcedureName = "usp_tbl_ScheduledEvent_Update";

                int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, strStrordProcedureName, pSqlParameter);
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
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select ScheduledEvent Details For Edit Data
        public ApplicationResult ScheduledEvent_Select(int intScheduledEventID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@ScheduledEventID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intScheduledEventID;

                strStrordProcedureName = "usp_tbl_ScheduledEventID_Select";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStrordProcedureName, pSqlParameter);
                ApplicationResult objResult = new ApplicationResult(dtResult);
                objResult.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResult;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ScheduledEvent Details For Delete

        public ApplicationResult ScheduledEvent_Delete(int intScheduledEventID, int intLastModifiedUserID, string strLastModifiedDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@ScheduledEventID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intScheduledEventID;

                pSqlParameter[1] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastModifiedUserID;

                pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifiedDate;

                strStrordProcedureName = "usp_tbl_ScheduledEvent_Delete";

                int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, strStrordProcedureName, pSqlParameter);

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
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ScheduledEvent Details Select For Gridview by Trust School

        public ApplicationResult ScheduledEvent_Select_By_Trust_School(int intTrustMID, int intMonth, int intYear)
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

                strStrordProcedureName = "usp_tbl_ScheduledEvent_Select_By_Trust_School";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStrordProcedureName, pSqlParameter);
                ApplicationResult objResult = new ApplicationResult(dtResult);
                objResult.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResult;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Select Department name for Dropdown edit data
        public ApplicationResult Department_SelectAll_ForDropDown(int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                strStrordProcedureName = "usp_tbl_Select_SectionName_ForDropDown";

                DataTable dtSection = new DataTable();
                dtSection = Database.ExecuteDataTable(CommandType.StoredProcedure, strStrordProcedureName, pSqlParameter);

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

        #region Select EventName for Dropdown edit data
        public ApplicationResult EventName_Select_ForDropDown(int intScheduledEventID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@ScheduledEventID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intScheduledEventID;

                strStrordProcedureName = "usp_tbl_Select_EventName_ForDropDown";

                DataTable dtSection = new DataTable();
                dtSection = Database.ExecuteDataTable(CommandType.StoredProcedure, strStrordProcedureName, pSqlParameter);

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

        #region Select Scheduled Event Details for ListofEvent Report
        public ApplicationResult ScheduledEventDetails_Select_ForListofEventDetailsRepot(int intSchoolMID, int intSectionMID, string strFromDate, string strTodate)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                pSqlParameter[1] = new SqlParameter("@SectionMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSectionMID;

                pSqlParameter[2] = new SqlParameter("@EventFromDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strFromDate;

                pSqlParameter[3] = new SqlParameter("@EventToDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strTodate;

                strStrordProcedureName = "usp_tbl_Select_ScheduleEventDetails_For_ListofEventDetailsReport";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStrordProcedureName, pSqlParameter);
                ApplicationResult objResult = new ApplicationResult(dtResult);
                objResult.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ApplicationResult ScheduledEventDetails_Select_ForListofEventDetailsRepot(string strFromDate, string strTodate)
        {
            try
            {
                int intSchoolMID = 0;
                int intSectionMID = 0;
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                pSqlParameter[1] = new SqlParameter("@SectionMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSectionMID;

                pSqlParameter[2] = new SqlParameter("@EventFromDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strFromDate;

                pSqlParameter[3] = new SqlParameter("@EventToDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strTodate;

                strStrordProcedureName = "usp_tbl_Select_ScheduleEventDetails_For_ListofEventDetailsReport";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStrordProcedureName, pSqlParameter);
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

        #region Update EventDetailsDescription
        public ApplicationResult ScheduledEvetn_UpdateForEventDestailsDescription(ScheduledEventBO objScheduledEventBO)
        {
            pSqlParameter = new SqlParameter[2];

            pSqlParameter[0] = new SqlParameter("@ScheduledEventID", SqlDbType.Int);
            pSqlParameter[0].Direction = ParameterDirection.Input;
            pSqlParameter[0].Value = objScheduledEventBO.ScheduledEventID;

            pSqlParameter[1] = new SqlParameter("@EventDetailsDescription", SqlDbType.VarChar);
            pSqlParameter[1].Direction = ParameterDirection.Input;
            pSqlParameter[1].Value = objScheduledEventBO.EventDetailsDescription;

            strStrordProcedureName = "usp_tbl_EventDetailsDescription_Update";

            int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, strStrordProcedureName, pSqlParameter);
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

        #region Select Data using ScheduledEventID for Detail Report
        public ApplicationResult ScheduleEventID_Select_ForDetailReport(int intScheduledEventID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@ScheduledEventID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intScheduledEventID;

                strStrordProcedureName = "usp_tbl_ScheduleEventID_Select_ForDetailReport";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStrordProcedureName, pSqlParameter);
                ApplicationResult objResult = new ApplicationResult(dtResult);
                objResult.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResult;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
