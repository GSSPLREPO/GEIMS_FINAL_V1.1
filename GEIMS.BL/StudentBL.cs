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
	public class StudentBL
	{
		#region user defined variables
		public string sSql;
		public string strStoredProcName;
		public SqlParameter[] pSqlParameter = null;
		#endregion

		#region Select All Student Details
		/// <summary>
		/// To Select All data from the tbl_Student_M table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public ApplicationResult Student_SelectAll(int intSchoolMID)
		{
			try
			{
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

				sSql = "usp_tbl_Student_M_SelectAll";
				DataTable dtStudent = new DataTable();
                dtStudent = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

				ApplicationResult objResults = new ApplicationResult(dtStudent);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
        #endregion

        #region Select All Excell Student Details
        /// <summary>
        /// To Select All data from the tbl_Student_M table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult Excel_Student_SelectAll(int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                sSql = "usp_tbl_Excel_Student_M_SelectAll";
                DataTable dtStudent = new DataTable();
                dtStudent = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtStudent);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select Student Details by StudentMID
        /// <summary>
        /// Select all details of Student for selected StudentMID from tbl_Student_M table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="intStudentMID"></param>
        /// <returns></returns>
        public ApplicationResult Student_Select(int intStudentMID, int intType)
		{
			try
			{
				pSqlParameter = new SqlParameter[2];

				pSqlParameter[0] = new SqlParameter("@StudentMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intStudentMID;

                pSqlParameter[1] = new SqlParameter("@intType", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intType;

				strStoredProcName = "usp_tbl_Student_M_Select";

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

        #region Select for Student Class and Division Wise
        public ApplicationResult Student_Select_ClassDivisionWise(int intClassMID, int intDivisionTID, string strAcademicYear, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];


                pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intClassMID;

                pSqlParameter[1] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intDivisionTID;

                pSqlParameter[2] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strAcademicYear;

                pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_Student_M_Select_By_ClassDivisionWise";

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

        #region Select for Student Class and Division Wise For Exam
        public ApplicationResult Student_Select_ClassDivisionWise_ForExam(int intClassMID, int intDivisionTID, string strAcademicYear, string strExam, int intSubjectId , int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];


                pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intClassMID;

                pSqlParameter[1] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intDivisionTID;

                pSqlParameter[2] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strAcademicYear;

                pSqlParameter[3] = new SqlParameter("@Exam", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strExam;

                pSqlParameter[4] = new SqlParameter("@SubjectId", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intSubjectId;

                pSqlParameter[5] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = intSchoolMID;


                strStoredProcName = "usp_tbl_Student_M_Select_By_ClassDivisionWise_For_Exam";

                DataSet dsResult = new DataSet();
                dsResult = Database.ExecuteDataSet(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dsResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
              
        #region Delete Student Details by StudentMID
		/// <summary>
		/// To Delete details of Student for selected StudentMID from tbl_Student_M table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="intStudentMID"></param>
		/// <returns></returns>
		public ApplicationResult Student_Delete(int intStudentMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@StudentMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intStudentMID;

				strStoredProcName = "usp_tbl_Student_M_Delete";

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

		#region Insert Student Details
		/// <summary>
		/// To Insert details of Student in tbl_Student_M table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="objStudentBO"></param>
		/// <returns></returns>
		public ApplicationResult Student_Insert(StudentBO objStudentBO)
		{
			try
			{
                pSqlParameter = new SqlParameter[116];


                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objStudentBO.SchoolMID;

                pSqlParameter[1] = new SqlParameter("@StudentFirstNameEng", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objStudentBO.StudentFirstNameEng;

                pSqlParameter[2] = new SqlParameter("@StudentMiddleNameEng", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objStudentBO.StudentMiddleNameEng;

                pSqlParameter[3] = new SqlParameter("@StudentLastNameEng", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objStudentBO.StudentLastNameEng;

                pSqlParameter[4] = new SqlParameter("@StudentFirstNameGuj", SqlDbType.NVarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objStudentBO.StudentFirstNameGuj;

                pSqlParameter[5] = new SqlParameter("@StudentMiddleNameGuj", SqlDbType.NVarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objStudentBO.StudentMiddleNameGuj;

                pSqlParameter[6] = new SqlParameter("@StudentLastNameGuj", SqlDbType.NVarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objStudentBO.StudentLastNameGuj;

                pSqlParameter[7] = new SqlParameter("@FatherFirstNameEng", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objStudentBO.FatherFirstNameEng;

                pSqlParameter[8] = new SqlParameter("@FatherFirstNameGuj", SqlDbType.NVarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objStudentBO.FatherFirstNameGuj;

                pSqlParameter[9] = new SqlParameter("@FatherMiddleNameEng", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objStudentBO.FatherMiddleNameEng;

                pSqlParameter[10] = new SqlParameter("@FatherMiddleNameGuj", SqlDbType.NVarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objStudentBO.FatherMiddleNameGuj;

                pSqlParameter[11] = new SqlParameter("@FatherLastNameEng", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objStudentBO.FatherLastNameEng;

                pSqlParameter[12] = new SqlParameter("@FatherLastNameGuj", SqlDbType.NVarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objStudentBO.FatherLastNameGuj;

                pSqlParameter[13] = new SqlParameter("@MotherFirstNameEng", SqlDbType.VarChar);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objStudentBO.MotherFirstNameEng;

                pSqlParameter[14] = new SqlParameter("@MotherFirstNameGuj", SqlDbType.NVarChar);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objStudentBO.MotherFirstNameGuj;

                pSqlParameter[15] = new SqlParameter("@MotherMiddleNameEng", SqlDbType.VarChar);
                pSqlParameter[15].Direction = ParameterDirection.Input;
                pSqlParameter[15].Value = objStudentBO.MotherMiddleNameEng;

                pSqlParameter[16] = new SqlParameter("@MotherMiddleNameGuj", SqlDbType.NVarChar);
                pSqlParameter[16].Direction = ParameterDirection.Input;
                pSqlParameter[16].Value = objStudentBO.MotherMiddleNameGuj;

                pSqlParameter[17] = new SqlParameter("@MotherLastNameEng", SqlDbType.VarChar);
                pSqlParameter[17].Direction = ParameterDirection.Input;
                pSqlParameter[17].Value = objStudentBO.MotherLastNameEng;

                pSqlParameter[18] = new SqlParameter("@MotherLastNameGuj", SqlDbType.NVarChar);
                pSqlParameter[18].Direction = ParameterDirection.Input;
                pSqlParameter[18].Value = objStudentBO.MotherLastNameGuj;

                pSqlParameter[19] = new SqlParameter("@AdmissionNo", SqlDbType.VarChar);
                pSqlParameter[19].Direction = ParameterDirection.Input;
                pSqlParameter[19].Value = objStudentBO.AdmissionNo;

                pSqlParameter[20] = new SqlParameter("@CurrentDate", SqlDbType.VarChar);
                pSqlParameter[20].Direction = ParameterDirection.Input;
                pSqlParameter[20].Value = objStudentBO.CurrentDate;

                pSqlParameter[21] = new SqlParameter("@JoiningDate", SqlDbType.VarChar);
                pSqlParameter[21].Direction = ParameterDirection.Input;
                pSqlParameter[21].Value = objStudentBO.JoiningDate;

                pSqlParameter[22] = new SqlParameter("@CurrentYear", SqlDbType.VarChar);
                pSqlParameter[22].Direction = ParameterDirection.Input;
                pSqlParameter[22].Value = objStudentBO.CurrentYear;

                pSqlParameter[23] = new SqlParameter("@CurrentSectionID", SqlDbType.Int);
                pSqlParameter[23].Direction = ParameterDirection.Input;
                pSqlParameter[23].Value = objStudentBO.CurrentSectionID;

                pSqlParameter[24] = new SqlParameter("@CurrentClassID", SqlDbType.Int);
                pSqlParameter[24].Direction = ParameterDirection.Input;
                pSqlParameter[24].Value = objStudentBO.CurrentClassID;

                pSqlParameter[25] = new SqlParameter("@CurrentDivisionTID", SqlDbType.Int);
                pSqlParameter[25].Direction = ParameterDirection.Input;
                pSqlParameter[25].Value = objStudentBO.CurrentDivisionTID;

                pSqlParameter[26] = new SqlParameter("@CurrentGrNo", SqlDbType.VarChar);
                pSqlParameter[26].Direction = ParameterDirection.Input;
                pSqlParameter[26].Value = objStudentBO.CurrentGrNo;

                pSqlParameter[27] = new SqlParameter("@AdmittedGrNo", SqlDbType.VarChar);
                pSqlParameter[27].Direction = ParameterDirection.Input;
                pSqlParameter[27].Value = objStudentBO.AdmittedGrNo;

                pSqlParameter[28] = new SqlParameter("@AdmittedClassID", SqlDbType.Int);
                pSqlParameter[28].Direction = ParameterDirection.Input;
                pSqlParameter[28].Value = objStudentBO.AdmittedClassID;

                pSqlParameter[29] = new SqlParameter("@AdmittedDivisionTID", SqlDbType.Int);
                pSqlParameter[29].Direction = ParameterDirection.Input;
                pSqlParameter[29].Value = objStudentBO.AdmittedDivisionTID;

                pSqlParameter[30] = new SqlParameter("@AdmittedYear", SqlDbType.VarChar);
                pSqlParameter[30].Direction = ParameterDirection.Input;
                pSqlParameter[30].Value = objStudentBO.AdmittedYear;

                pSqlParameter[31] = new SqlParameter("@StudentPhoto", SqlDbType.Image);
                pSqlParameter[31].Direction = ParameterDirection.Input;
                pSqlParameter[31].Value = objStudentBO.StudentPhoto;

                pSqlParameter[32] = new SqlParameter("@GenderGuj", SqlDbType.NVarChar);
                pSqlParameter[32].Direction = ParameterDirection.Input;
                pSqlParameter[32].Value = objStudentBO.GenderGuj;

                pSqlParameter[33] = new SqlParameter("@GenderEng", SqlDbType.VarChar);
                pSqlParameter[33].Direction = ParameterDirection.Input;
                pSqlParameter[33].Value = objStudentBO.GenderEng;

                pSqlParameter[34] = new SqlParameter("@DateOfBirth", SqlDbType.VarChar);
                pSqlParameter[34].Direction = ParameterDirection.Input;
                pSqlParameter[34].Value = objStudentBO.DateOfBirth;

                pSqlParameter[35] = new SqlParameter("@BirthDistrictEng", SqlDbType.VarChar);
                pSqlParameter[35].Direction = ParameterDirection.Input;
                pSqlParameter[35].Value = objStudentBO.BirthDistrictEng;

                pSqlParameter[36] = new SqlParameter("@BirthDistrictGuj", SqlDbType.NVarChar);
                pSqlParameter[36].Direction = ParameterDirection.Input;
                pSqlParameter[36].Value = objStudentBO.BirthDistrictGuj;

                pSqlParameter[37] = new SqlParameter("@NationalityEng", SqlDbType.VarChar);
                pSqlParameter[37].Direction = ParameterDirection.Input;
                pSqlParameter[37].Value = objStudentBO.NationalityEng;

                pSqlParameter[38] = new SqlParameter("@NationalityGuj", SqlDbType.NVarChar);
                pSqlParameter[38].Direction = ParameterDirection.Input;
                pSqlParameter[38].Value = objStudentBO.NationalityGuj;

                pSqlParameter[39] = new SqlParameter("@ReligionEng", SqlDbType.VarChar);
                pSqlParameter[39].Direction = ParameterDirection.Input;
                pSqlParameter[39].Value = objStudentBO.ReligionEng;

                pSqlParameter[40] = new SqlParameter("@CasteEng", SqlDbType.VarChar);
                pSqlParameter[40].Direction = ParameterDirection.Input;
                pSqlParameter[40].Value = objStudentBO.CasteEng;

                pSqlParameter[41] = new SqlParameter("@CasteGuj", SqlDbType.NVarChar);
                pSqlParameter[41].Direction = ParameterDirection.Input;
                pSqlParameter[41].Value = objStudentBO.CasteGuj;

                pSqlParameter[42] = new SqlParameter("@SubCasteEng", SqlDbType.VarChar);
                pSqlParameter[42].Direction = ParameterDirection.Input;
                pSqlParameter[42].Value = objStudentBO.SubCasteEng;

                pSqlParameter[43] = new SqlParameter("@SubCasteGuj", SqlDbType.NVarChar);
                pSqlParameter[43].Direction = ParameterDirection.Input;
                pSqlParameter[43].Value = objStudentBO.SubCasteGuj;

                pSqlParameter[44] = new SqlParameter("@CategoryEng", SqlDbType.VarChar);
                pSqlParameter[44].Direction = ParameterDirection.Input;
                pSqlParameter[44].Value = objStudentBO.CategoryEng;

                pSqlParameter[45] = new SqlParameter("@CategoryGuj", SqlDbType.NVarChar);
                pSqlParameter[45].Direction = ParameterDirection.Input;
                pSqlParameter[45].Value = objStudentBO.CategoryGuj;

                pSqlParameter[46] = new SqlParameter("@SubCategory", SqlDbType.VarChar);
                pSqlParameter[46].Direction = ParameterDirection.Input;
                pSqlParameter[46].Value = objStudentBO.SubCategory;

                pSqlParameter[47] = new SqlParameter("@HandicapPrecent", SqlDbType.VarChar);
                pSqlParameter[47].Direction = ParameterDirection.Input;
                pSqlParameter[47].Value = objStudentBO.HandicapPrecent;

                pSqlParameter[48] = new SqlParameter("@OtherDefect", SqlDbType.VarChar);
                pSqlParameter[48].Direction = ParameterDirection.Input;
                pSqlParameter[48].Value = objStudentBO.OtherDefect;

                pSqlParameter[49] = new SqlParameter("@PresentAddressEng", SqlDbType.VarChar);
                pSqlParameter[49].Direction = ParameterDirection.Input;
                pSqlParameter[49].Value = objStudentBO.PresentAddressEng;

                pSqlParameter[50] = new SqlParameter("@PresentAddressGuj", SqlDbType.NVarChar);
                pSqlParameter[50].Direction = ParameterDirection.Input;
                pSqlParameter[50].Value = objStudentBO.PresentAddressGuj;

                pSqlParameter[51] = new SqlParameter("@PresentCityEng", SqlDbType.VarChar);
                pSqlParameter[51].Direction = ParameterDirection.Input;
                pSqlParameter[51].Value = objStudentBO.PresentCityEng;

                pSqlParameter[52] = new SqlParameter("@PresentCityGuj", SqlDbType.NVarChar);
                pSqlParameter[52].Direction = ParameterDirection.Input;
                pSqlParameter[52].Value = objStudentBO.PresentCityGuj;

                pSqlParameter[53] = new SqlParameter("@PresentStateEng", SqlDbType.VarChar);
                pSqlParameter[53].Direction = ParameterDirection.Input;
                pSqlParameter[53].Value = objStudentBO.PresentStateEng;

                pSqlParameter[54] = new SqlParameter("@PresentStateGuj", SqlDbType.NVarChar);
                pSqlParameter[54].Direction = ParameterDirection.Input;
                pSqlParameter[54].Value = objStudentBO.PresentStateGuj;

                pSqlParameter[55] = new SqlParameter("@PresentPinCode", SqlDbType.VarChar);
                pSqlParameter[55].Direction = ParameterDirection.Input;
                pSqlParameter[55].Value = objStudentBO.PresentPinCode;

                pSqlParameter[56] = new SqlParameter("@PresentContactNo", SqlDbType.VarChar);
                pSqlParameter[56].Direction = ParameterDirection.Input;
                pSqlParameter[56].Value = objStudentBO.PresentContactNo;

                pSqlParameter[57] = new SqlParameter("@PresentEmailId", SqlDbType.VarChar);
                pSqlParameter[57].Direction = ParameterDirection.Input;
                pSqlParameter[57].Value = objStudentBO.PresentEmailId;

                pSqlParameter[58] = new SqlParameter("@PermanentAddressEng", SqlDbType.VarChar);
                pSqlParameter[58].Direction = ParameterDirection.Input;
                pSqlParameter[58].Value = objStudentBO.PermanentAddressEng;

                pSqlParameter[59] = new SqlParameter("@PermanentAddressGuj", SqlDbType.NVarChar);
                pSqlParameter[59].Direction = ParameterDirection.Input;
                pSqlParameter[59].Value = objStudentBO.PermanentAddressGuj;

                pSqlParameter[60] = new SqlParameter("@PermanentCityEng", SqlDbType.VarChar);
                pSqlParameter[60].Direction = ParameterDirection.Input;
                pSqlParameter[60].Value = objStudentBO.PermanentCityEng;

                pSqlParameter[61] = new SqlParameter("@PermanentCityGuj", SqlDbType.NVarChar);
                pSqlParameter[61].Direction = ParameterDirection.Input;
                pSqlParameter[61].Value = objStudentBO.PermanentCityGuj;

                pSqlParameter[62] = new SqlParameter("@PermanentStateEng", SqlDbType.VarChar);
                pSqlParameter[62].Direction = ParameterDirection.Input;
                pSqlParameter[62].Value = objStudentBO.PermanentStateEng;

                pSqlParameter[63] = new SqlParameter("@PermanentStateGuj", SqlDbType.NVarChar);
                pSqlParameter[63].Direction = ParameterDirection.Input;
                pSqlParameter[63].Value = objStudentBO.PermanentStateGuj;

                pSqlParameter[64] = new SqlParameter("@PermanentPinCode", SqlDbType.VarChar);
                pSqlParameter[64].Direction = ParameterDirection.Input;
                pSqlParameter[64].Value = objStudentBO.PermanentPinCode;

                pSqlParameter[65] = new SqlParameter("@PermanentContactNo", SqlDbType.VarChar);
                pSqlParameter[65].Direction = ParameterDirection.Input;
                pSqlParameter[65].Value = objStudentBO.PermanentContactNo;

                pSqlParameter[66] = new SqlParameter("@PermanentEmailId", SqlDbType.VarChar);
                pSqlParameter[66].Direction = ParameterDirection.Input;
                pSqlParameter[66].Value = objStudentBO.PermanentEmailId;

                pSqlParameter[67] = new SqlParameter("@FatherOccupation", SqlDbType.VarChar);
                pSqlParameter[67].Direction = ParameterDirection.Input;
                pSqlParameter[67].Value = objStudentBO.FatherOccupation;

                pSqlParameter[68] = new SqlParameter("@MotherOccupation", SqlDbType.VarChar);
                pSqlParameter[68].Direction = ParameterDirection.Input;
                pSqlParameter[68].Value = objStudentBO.MotherOccupation;

                pSqlParameter[69] = new SqlParameter("@GardianOccupation", SqlDbType.VarChar);
                pSqlParameter[69].Direction = ParameterDirection.Input;
                pSqlParameter[69].Value = objStudentBO.GardianOccupation;

                pSqlParameter[70] = new SqlParameter("@FatherQualification", SqlDbType.VarChar);
                pSqlParameter[70].Direction = ParameterDirection.Input;
                pSqlParameter[70].Value = objStudentBO.FatherQualification;

                pSqlParameter[71] = new SqlParameter("@MotherQualification", SqlDbType.VarChar);
                pSqlParameter[71].Direction = ParameterDirection.Input;
                pSqlParameter[71].Value = objStudentBO.MotherQualification;

                pSqlParameter[72] = new SqlParameter("@GardianQualification", SqlDbType.VarChar);
                pSqlParameter[72].Direction = ParameterDirection.Input;
                pSqlParameter[72].Value = objStudentBO.GardianQualification;

                pSqlParameter[73] = new SqlParameter("@FatherMobileNo", SqlDbType.VarChar);
                pSqlParameter[73].Direction = ParameterDirection.Input;
                pSqlParameter[73].Value = objStudentBO.FatherMobileNo;

                pSqlParameter[74] = new SqlParameter("@MotherMobileNo", SqlDbType.VarChar);
                pSqlParameter[74].Direction = ParameterDirection.Input;
                pSqlParameter[74].Value = objStudentBO.MotherMobileNo;

                pSqlParameter[75] = new SqlParameter("@GardianMobileNo", SqlDbType.VarChar);
                pSqlParameter[75].Direction = ParameterDirection.Input;
                pSqlParameter[75].Value = objStudentBO.GardianMobileNo;

                pSqlParameter[76] = new SqlParameter("@FatherEmailID", SqlDbType.VarChar);
                pSqlParameter[76].Direction = ParameterDirection.Input;
                pSqlParameter[76].Value = objStudentBO.FatherEmailID;

                pSqlParameter[77] = new SqlParameter("@MotherEmailID", SqlDbType.VarChar);
                pSqlParameter[77].Direction = ParameterDirection.Input;
                pSqlParameter[77].Value = objStudentBO.MotherEmailID;

                pSqlParameter[78] = new SqlParameter("@GardianEmailID", SqlDbType.VarChar);
                pSqlParameter[78].Direction = ParameterDirection.Input;
                pSqlParameter[78].Value = objStudentBO.GardianEmailID;

                pSqlParameter[79] = new SqlParameter("@Height", SqlDbType.VarChar);
                pSqlParameter[79].Direction = ParameterDirection.Input;
                pSqlParameter[79].Value = objStudentBO.Height;

                pSqlParameter[80] = new SqlParameter("@Wight", SqlDbType.VarChar);
                pSqlParameter[80].Direction = ParameterDirection.Input;
                pSqlParameter[80].Value = objStudentBO.Weight;

                pSqlParameter[81] = new SqlParameter("@Hobbies", SqlDbType.VarChar);
                pSqlParameter[81].Direction = ParameterDirection.Input;
                pSqlParameter[81].Value = objStudentBO.Hobbies;

                pSqlParameter[82] = new SqlParameter("@StatusMasterID", SqlDbType.Int);
                pSqlParameter[82].Direction = ParameterDirection.Input;
                pSqlParameter[82].Value = objStudentBO.StatusMasterID;

                pSqlParameter[83] = new SqlParameter("@LeftDate", SqlDbType.VarChar);
                pSqlParameter[83].Direction = ParameterDirection.Input;
                pSqlParameter[83].Value = objStudentBO.LeftDate;

                pSqlParameter[84] = new SqlParameter("@LeftReason", SqlDbType.VarChar);
                pSqlParameter[84].Direction = ParameterDirection.Input;
                pSqlParameter[84].Value = objStudentBO.LeftReason;

                pSqlParameter[85] = new SqlParameter("@LeftYear", SqlDbType.VarChar);
                pSqlParameter[85].Direction = ParameterDirection.Input;
                pSqlParameter[85].Value = objStudentBO.LeftYear;

                pSqlParameter[86] = new SqlParameter("@LeftStd", SqlDbType.VarChar);
                pSqlParameter[86].Direction = ParameterDirection.Input;
                pSqlParameter[86].Value = objStudentBO.LeftStd;

                pSqlParameter[87] = new SqlParameter("@LcNo", SqlDbType.VarChar);
                pSqlParameter[87].Direction = ParameterDirection.Input;
                pSqlParameter[87].Value = objStudentBO.LcNo;

                pSqlParameter[88] = new SqlParameter("@LcDate", SqlDbType.VarChar);
                pSqlParameter[88].Direction = ParameterDirection.Input;
                pSqlParameter[88].Value = objStudentBO.LcDate;

                pSqlParameter[89] = new SqlParameter("@LcRemarks", SqlDbType.VarChar);
                pSqlParameter[89].Direction = ParameterDirection.Input;
                pSqlParameter[89].Value = objStudentBO.LcRemarks;

                pSqlParameter[90] = new SqlParameter("@LcCopy", SqlDbType.VarChar);
                pSqlParameter[90].Direction = ParameterDirection.Input;
                pSqlParameter[90].Value = objStudentBO.LcCopy;

                pSqlParameter[91] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[91].Direction = ParameterDirection.Input;
                pSqlParameter[91].Value = objStudentBO.LastModifiedUserID;

                pSqlParameter[92] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[92].Direction = ParameterDirection.Input;
                pSqlParameter[92].Value = objStudentBO.LastModifiedDate;

                pSqlParameter[93] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[93].Direction = ParameterDirection.Input;
                pSqlParameter[93].Value = objStudentBO.IsDeleted;

                pSqlParameter[94] = new SqlParameter("@RegisteredYear", SqlDbType.VarChar);
                pSqlParameter[94].Direction = ParameterDirection.Input;
                pSqlParameter[94].Value = objStudentBO.RegisteredYear;

                pSqlParameter[95] = new SqlParameter("@AdmissionDate", SqlDbType.VarChar);
                pSqlParameter[95].Direction = ParameterDirection.Input;
                pSqlParameter[95].Value = objStudentBO.AdmissionDate;

                pSqlParameter[96] = new SqlParameter("@GVUniqueID", SqlDbType.VarChar);
                pSqlParameter[96].Direction = ParameterDirection.Input;
                pSqlParameter[96].Value = objStudentBO.GVUniqueID;

                pSqlParameter[97] = new SqlParameter("@BankAccount", SqlDbType.VarChar);
                pSqlParameter[97].Direction = ParameterDirection.Input;
                pSqlParameter[97].Value = objStudentBO.BankAccount;

                pSqlParameter[98] = new SqlParameter("@IsLeavingCerti", SqlDbType.Int);
                pSqlParameter[98].Direction = ParameterDirection.Input;
                pSqlParameter[98].Value = objStudentBO.IsLeavingCerti;

                pSqlParameter[99] = new SqlParameter("@IsLeavingGujaratiCerti", SqlDbType.Int);
                pSqlParameter[99].Direction = ParameterDirection.Input;
                pSqlParameter[99].Value = objStudentBO.IsLeavingGujaratiCerti;


                pSqlParameter[100] = new SqlParameter("@MotherTongue", SqlDbType.VarChar);
                pSqlParameter[100].Direction = ParameterDirection.Input;
                pSqlParameter[100].Value = objStudentBO.MotherTongue;

                pSqlParameter[101] = new SqlParameter("@PreviousSchoolDetails", SqlDbType.VarChar);
                pSqlParameter[101].Direction = ParameterDirection.Input;
                pSqlParameter[101].Value = objStudentBO.PreviousSchoolDetails;

                pSqlParameter[102] = new SqlParameter("@PhysicalIdentification", SqlDbType.VarChar);
                pSqlParameter[102].Direction = ParameterDirection.Input;
                pSqlParameter[102].Value = objStudentBO.PhysicalIdentification;

                pSqlParameter[103] = new SqlParameter("@FatherOrganisationName", SqlDbType.VarChar);
                pSqlParameter[103].Direction = ParameterDirection.Input;
                pSqlParameter[103].Value = objStudentBO.FatherOrganisationName;

                pSqlParameter[104] = new SqlParameter("@FatherOrganisationContactNumber", SqlDbType.VarChar);
                pSqlParameter[104].Direction = ParameterDirection.Input;
                pSqlParameter[104].Value = objStudentBO.FatherOrganisationContactNumber;

                pSqlParameter[105] = new SqlParameter("@BloodGroup", SqlDbType.VarChar);
                pSqlParameter[105].Direction = ParameterDirection.Input;
                pSqlParameter[105].Value = objStudentBO.BloodGroup;

                pSqlParameter[106] = new SqlParameter("@IFSCCode", SqlDbType.VarChar);
                pSqlParameter[106].Direction = ParameterDirection.Input;
                pSqlParameter[106].Value = objStudentBO.IFSCCode;

                pSqlParameter[107] = new SqlParameter("@BranchName", SqlDbType.VarChar);
                pSqlParameter[107].Direction = ParameterDirection.Input;
                pSqlParameter[107].Value = objStudentBO.BranchName;


                pSqlParameter[108] = new SqlParameter("@AccountNumber", SqlDbType.VarChar);
                pSqlParameter[108].Direction = ParameterDirection.Input;
                pSqlParameter[108].Value = objStudentBO.AccountNumber;


                pSqlParameter[109] = new SqlParameter("@TypeOfVehicle", SqlDbType.VarChar);
                pSqlParameter[109].Direction = ParameterDirection.Input;
                pSqlParameter[109].Value = objStudentBO.TypeOfVehicle;

                pSqlParameter[110] = new SqlParameter("@VehicleNo", SqlDbType.VarChar);
                pSqlParameter[110].Direction = ParameterDirection.Input;
                pSqlParameter[110].Value = objStudentBO.VehicleNo;

                pSqlParameter[111] = new SqlParameter("@DriverName", SqlDbType.VarChar);
                pSqlParameter[111].Direction = ParameterDirection.Input;
                pSqlParameter[111].Value = objStudentBO.DriverName;


                pSqlParameter[112] = new SqlParameter("@DriverContactNo", SqlDbType.VarChar);
                pSqlParameter[112].Direction = ParameterDirection.Input;
                pSqlParameter[112].Value = objStudentBO.DriverContactNo;

                pSqlParameter[113] = new SqlParameter("@AadharCardNo", SqlDbType.VarChar);
                pSqlParameter[113].Direction = ParameterDirection.Input;
                pSqlParameter[113].Value = objStudentBO.AadharCardNo;

                pSqlParameter[114] = new SqlParameter("@RollNumber", SqlDbType.VarChar);
                pSqlParameter[114].Direction = ParameterDirection.Input;
                pSqlParameter[114].Value = objStudentBO.RollNumber;

                pSqlParameter[115] = new SqlParameter("@GvUniqueNo", SqlDbType.VarChar);
                pSqlParameter[115].Direction = ParameterDirection.Input;
                pSqlParameter[115].Value = objStudentBO.GvUniqueNo;


                sSql = "usp_tbl_Student_M_Insert";
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
				objStudentBO = null;
			}
		}
		#endregion

		#region Update Student Details
		/// <summary>
		/// To Update details of Student in tbl_Student_M table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="objStudentBO"></param>
		/// <returns></returns>
		public ApplicationResult Student_Update(StudentBO objStudentBO, StudentTBO objStudentTBO)
		{
			try
			{
                pSqlParameter = new SqlParameter[116];


                pSqlParameter[0] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objStudentBO.StudentMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objStudentBO.SchoolMID;

                pSqlParameter[2] = new SqlParameter("@StudentFirstNameEng", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objStudentBO.StudentFirstNameEng;

                pSqlParameter[3] = new SqlParameter("@StudentMiddleNameEng", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objStudentBO.StudentMiddleNameEng;

                pSqlParameter[4] = new SqlParameter("@StudentLastNameEng", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objStudentBO.StudentLastNameEng;

                pSqlParameter[5] = new SqlParameter("@StudentFirstNameGuj", SqlDbType.NVarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objStudentBO.StudentFirstNameGuj;

                pSqlParameter[6] = new SqlParameter("@StudentMiddleNameGuj", SqlDbType.NVarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objStudentBO.StudentMiddleNameGuj;

                pSqlParameter[7] = new SqlParameter("@StudentLastNameGuj", SqlDbType.NVarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objStudentBO.StudentLastNameGuj;

                pSqlParameter[8] = new SqlParameter("@FatherFirstNameEng", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objStudentBO.FatherFirstNameEng;

                pSqlParameter[9] = new SqlParameter("@FatherFirstNameGuj", SqlDbType.NVarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objStudentBO.FatherFirstNameGuj;

                pSqlParameter[10] = new SqlParameter("@FatherMiddleNameEng", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objStudentBO.FatherMiddleNameEng;

                pSqlParameter[11] = new SqlParameter("@FatherMiddleNameGuj", SqlDbType.NVarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objStudentBO.FatherMiddleNameGuj;

                pSqlParameter[12] = new SqlParameter("@FatherLastNameEng", SqlDbType.VarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objStudentBO.FatherLastNameEng;

                pSqlParameter[13] = new SqlParameter("@FatherLastNameGuj", SqlDbType.NVarChar);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objStudentBO.FatherLastNameGuj;

                pSqlParameter[14] = new SqlParameter("@MotherFirstNameEng", SqlDbType.VarChar);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objStudentBO.MotherFirstNameEng;

                pSqlParameter[15] = new SqlParameter("@MotherFirstNameGuj", SqlDbType.NVarChar);
                pSqlParameter[15].Direction = ParameterDirection.Input;
                pSqlParameter[15].Value = objStudentBO.MotherFirstNameGuj;

                pSqlParameter[16] = new SqlParameter("@MotherMiddleNameEng", SqlDbType.VarChar);
                pSqlParameter[16].Direction = ParameterDirection.Input;
                pSqlParameter[16].Value = objStudentBO.MotherMiddleNameEng;

                pSqlParameter[17] = new SqlParameter("@MotherMiddleNameGuj", SqlDbType.NVarChar);
                pSqlParameter[17].Direction = ParameterDirection.Input;
                pSqlParameter[17].Value = objStudentBO.MotherMiddleNameGuj;

                pSqlParameter[18] = new SqlParameter("@MotherLastNameEng", SqlDbType.VarChar);
                pSqlParameter[18].Direction = ParameterDirection.Input;
                pSqlParameter[18].Value = objStudentBO.MotherLastNameEng;

                pSqlParameter[19] = new SqlParameter("@MotherLastNameGuj", SqlDbType.NVarChar);
                pSqlParameter[19].Direction = ParameterDirection.Input;
                pSqlParameter[19].Value = objStudentBO.MotherLastNameGuj;

                pSqlParameter[20] = new SqlParameter("@AdmissionNo", SqlDbType.VarChar);
                pSqlParameter[20].Direction = ParameterDirection.Input;
                pSqlParameter[20].Value = objStudentBO.AdmissionNo;

                pSqlParameter[21] = new SqlParameter("@CurrentDate", SqlDbType.VarChar);
                pSqlParameter[21].Direction = ParameterDirection.Input;
                pSqlParameter[21].Value = objStudentBO.CurrentDate;

                pSqlParameter[22] = new SqlParameter("@JoiningDate", SqlDbType.VarChar);
                pSqlParameter[22].Direction = ParameterDirection.Input;
                pSqlParameter[22].Value = objStudentBO.JoiningDate;

                pSqlParameter[23] = new SqlParameter("@CurrentYear", SqlDbType.VarChar);
                pSqlParameter[23].Direction = ParameterDirection.Input;
                pSqlParameter[23].Value = objStudentBO.CurrentYear;

                pSqlParameter[24] = new SqlParameter("@CurrentSectionID", SqlDbType.Int);
                pSqlParameter[24].Direction = ParameterDirection.Input;
                pSqlParameter[24].Value = objStudentBO.CurrentSectionID;

                pSqlParameter[25] = new SqlParameter("@CurrentClassID", SqlDbType.Int);
                pSqlParameter[25].Direction = ParameterDirection.Input;
                pSqlParameter[25].Value = objStudentBO.CurrentClassID;

                pSqlParameter[26] = new SqlParameter("@CurrentDivisionTID", SqlDbType.Int);
                pSqlParameter[26].Direction = ParameterDirection.Input;
                pSqlParameter[26].Value = objStudentBO.CurrentDivisionTID;

                pSqlParameter[27] = new SqlParameter("@CurrentGrNo", SqlDbType.VarChar);
                pSqlParameter[27].Direction = ParameterDirection.Input;
                pSqlParameter[27].Value = objStudentBO.CurrentGrNo;

                pSqlParameter[28] = new SqlParameter("@AdmittedGrNo", SqlDbType.VarChar);
                pSqlParameter[28].Direction = ParameterDirection.Input;
                pSqlParameter[28].Value = objStudentBO.AdmittedGrNo;

                pSqlParameter[29] = new SqlParameter("@AdmittedClassID", SqlDbType.Int);
                pSqlParameter[29].Direction = ParameterDirection.Input;
                pSqlParameter[29].Value = objStudentBO.AdmittedClassID;

                pSqlParameter[30] = new SqlParameter("@AdmittedDivisionTID", SqlDbType.Int);
                pSqlParameter[30].Direction = ParameterDirection.Input;
                pSqlParameter[30].Value = objStudentBO.AdmittedDivisionTID;

                pSqlParameter[31] = new SqlParameter("@AdmittedYear", SqlDbType.VarChar);
                pSqlParameter[31].Direction = ParameterDirection.Input;
                pSqlParameter[31].Value = objStudentBO.AdmittedYear;

                pSqlParameter[32] = new SqlParameter("@StudentPhoto", SqlDbType.Image);
                pSqlParameter[32].Direction = ParameterDirection.Input;
                pSqlParameter[32].Value = objStudentBO.StudentPhoto;

                pSqlParameter[33] = new SqlParameter("@GenderGuj", SqlDbType.NVarChar);
                pSqlParameter[33].Direction = ParameterDirection.Input;
                pSqlParameter[33].Value = objStudentBO.GenderGuj;

                pSqlParameter[34] = new SqlParameter("@GenderEng", SqlDbType.VarChar);
                pSqlParameter[34].Direction = ParameterDirection.Input;
                pSqlParameter[34].Value = objStudentBO.GenderEng;

                pSqlParameter[35] = new SqlParameter("@DateOfBirth", SqlDbType.VarChar);
                pSqlParameter[35].Direction = ParameterDirection.Input;
                pSqlParameter[35].Value = objStudentBO.DateOfBirth;

                pSqlParameter[36] = new SqlParameter("@BirthDistrictEng", SqlDbType.VarChar);
                pSqlParameter[36].Direction = ParameterDirection.Input;
                pSqlParameter[36].Value = objStudentBO.BirthDistrictEng;

                pSqlParameter[37] = new SqlParameter("@BirthDistrictGuj", SqlDbType.NVarChar);
                pSqlParameter[37].Direction = ParameterDirection.Input;
                pSqlParameter[37].Value = objStudentBO.BirthDistrictGuj;

                pSqlParameter[38] = new SqlParameter("@NationalityEng", SqlDbType.VarChar);
                pSqlParameter[38].Direction = ParameterDirection.Input;
                pSqlParameter[38].Value = objStudentBO.NationalityEng;

                pSqlParameter[39] = new SqlParameter("@NationalityGuj", SqlDbType.NVarChar);
                pSqlParameter[39].Direction = ParameterDirection.Input;
                pSqlParameter[39].Value = objStudentBO.NationalityGuj;

                pSqlParameter[40] = new SqlParameter("@ReligionEng", SqlDbType.VarChar);
                pSqlParameter[40].Direction = ParameterDirection.Input;
                pSqlParameter[40].Value = objStudentBO.ReligionEng;

                pSqlParameter[41] = new SqlParameter("@CasteEng", SqlDbType.VarChar);
                pSqlParameter[41].Direction = ParameterDirection.Input;
                pSqlParameter[41].Value = objStudentBO.CasteEng;

                pSqlParameter[42] = new SqlParameter("@CasteGuj", SqlDbType.NVarChar);
                pSqlParameter[42].Direction = ParameterDirection.Input;
                pSqlParameter[42].Value = objStudentBO.CasteGuj;

                pSqlParameter[43] = new SqlParameter("@SubCasteEng", SqlDbType.VarChar);
                pSqlParameter[43].Direction = ParameterDirection.Input;
                pSqlParameter[43].Value = objStudentBO.SubCasteEng;

                pSqlParameter[44] = new SqlParameter("@SubCasteGuj", SqlDbType.NVarChar);
                pSqlParameter[44].Direction = ParameterDirection.Input;
                pSqlParameter[44].Value = objStudentBO.SubCasteGuj;

                pSqlParameter[45] = new SqlParameter("@CategoryEng", SqlDbType.VarChar);
                pSqlParameter[45].Direction = ParameterDirection.Input;
                pSqlParameter[45].Value = objStudentBO.CategoryEng;

                pSqlParameter[46] = new SqlParameter("@CategoryGuj", SqlDbType.NVarChar);
                pSqlParameter[46].Direction = ParameterDirection.Input;
                pSqlParameter[46].Value = objStudentBO.CategoryGuj;

                pSqlParameter[47] = new SqlParameter("@SubCategory", SqlDbType.VarChar);
                pSqlParameter[47].Direction = ParameterDirection.Input;
                pSqlParameter[47].Value = objStudentBO.SubCategory;

                pSqlParameter[48] = new SqlParameter("@HandicapPrecent", SqlDbType.VarChar);
                pSqlParameter[48].Direction = ParameterDirection.Input;
                pSqlParameter[48].Value = objStudentBO.HandicapPrecent;

                pSqlParameter[49] = new SqlParameter("@OtherDefect", SqlDbType.VarChar);
                pSqlParameter[49].Direction = ParameterDirection.Input;
                pSqlParameter[49].Value = objStudentBO.OtherDefect;

                pSqlParameter[50] = new SqlParameter("@PresentAddressEng", SqlDbType.VarChar);
                pSqlParameter[50].Direction = ParameterDirection.Input;
                pSqlParameter[50].Value = objStudentBO.PresentAddressEng;

                pSqlParameter[51] = new SqlParameter("@PresentAddressGuj", SqlDbType.NVarChar);
                pSqlParameter[51].Direction = ParameterDirection.Input;
                pSqlParameter[51].Value = objStudentBO.PresentAddressGuj;

                pSqlParameter[52] = new SqlParameter("@PresentCityEng", SqlDbType.VarChar);
                pSqlParameter[52].Direction = ParameterDirection.Input;
                pSqlParameter[52].Value = objStudentBO.PresentCityEng;

                pSqlParameter[53] = new SqlParameter("@PresentCityGuj", SqlDbType.NVarChar);
                pSqlParameter[53].Direction = ParameterDirection.Input;
                pSqlParameter[53].Value = objStudentBO.PresentCityGuj;

                pSqlParameter[54] = new SqlParameter("@PresentStateEng", SqlDbType.VarChar);
                pSqlParameter[54].Direction = ParameterDirection.Input;
                pSqlParameter[54].Value = objStudentBO.PresentStateEng;

                pSqlParameter[55] = new SqlParameter("@PresentStateGuj", SqlDbType.NVarChar);
                pSqlParameter[55].Direction = ParameterDirection.Input;
                pSqlParameter[55].Value = objStudentBO.PresentStateGuj;

                pSqlParameter[56] = new SqlParameter("@PresentPinCode", SqlDbType.VarChar);
                pSqlParameter[56].Direction = ParameterDirection.Input;
                pSqlParameter[56].Value = objStudentBO.PresentPinCode;

                pSqlParameter[57] = new SqlParameter("@PresentContactNo", SqlDbType.VarChar);
                pSqlParameter[57].Direction = ParameterDirection.Input;
                pSqlParameter[57].Value = objStudentBO.PresentContactNo;

                pSqlParameter[58] = new SqlParameter("@PresentEmailId", SqlDbType.VarChar);
                pSqlParameter[58].Direction = ParameterDirection.Input;
                pSqlParameter[58].Value = objStudentBO.PresentEmailId;

                pSqlParameter[59] = new SqlParameter("@PermanentAddressEng", SqlDbType.VarChar);
                pSqlParameter[59].Direction = ParameterDirection.Input;
                pSqlParameter[59].Value = objStudentBO.PermanentAddressEng;

                pSqlParameter[60] = new SqlParameter("@PermanentAddressGuj", SqlDbType.NVarChar);
                pSqlParameter[60].Direction = ParameterDirection.Input;
                pSqlParameter[60].Value = objStudentBO.PermanentAddressGuj;

                pSqlParameter[61] = new SqlParameter("@PermanentCityEng", SqlDbType.VarChar);
                pSqlParameter[61].Direction = ParameterDirection.Input;
                pSqlParameter[61].Value = objStudentBO.PermanentCityEng;

                pSqlParameter[62] = new SqlParameter("@PermanentCityGuj", SqlDbType.NVarChar);
                pSqlParameter[62].Direction = ParameterDirection.Input;
                pSqlParameter[62].Value = objStudentBO.PermanentCityGuj;

                pSqlParameter[63] = new SqlParameter("@PermanentStateEng", SqlDbType.VarChar);
                pSqlParameter[63].Direction = ParameterDirection.Input;
                pSqlParameter[63].Value = objStudentBO.PermanentStateEng;

                pSqlParameter[64] = new SqlParameter("@PermanentStateGuj", SqlDbType.NVarChar);
                pSqlParameter[64].Direction = ParameterDirection.Input;
                pSqlParameter[64].Value = objStudentBO.PermanentStateGuj;

                pSqlParameter[65] = new SqlParameter("@PermanentPinCode", SqlDbType.VarChar);
                pSqlParameter[65].Direction = ParameterDirection.Input;
                pSqlParameter[65].Value = objStudentBO.PermanentPinCode;

                pSqlParameter[66] = new SqlParameter("@PermanentContactNo", SqlDbType.VarChar);
                pSqlParameter[66].Direction = ParameterDirection.Input;
                pSqlParameter[66].Value = objStudentBO.PermanentContactNo;

                pSqlParameter[67] = new SqlParameter("@PermanentEmailId", SqlDbType.VarChar);
                pSqlParameter[67].Direction = ParameterDirection.Input;
                pSqlParameter[67].Value = objStudentBO.PermanentEmailId;

                pSqlParameter[68] = new SqlParameter("@FatherOccupation", SqlDbType.VarChar);
                pSqlParameter[68].Direction = ParameterDirection.Input;
                pSqlParameter[68].Value = objStudentBO.FatherOccupation;

                pSqlParameter[69] = new SqlParameter("@MotherOccupation", SqlDbType.VarChar);
                pSqlParameter[69].Direction = ParameterDirection.Input;
                pSqlParameter[69].Value = objStudentBO.MotherOccupation;

                pSqlParameter[70] = new SqlParameter("@GardianOccupation", SqlDbType.VarChar);
                pSqlParameter[70].Direction = ParameterDirection.Input;
                pSqlParameter[70].Value = objStudentBO.GardianOccupation;

                pSqlParameter[71] = new SqlParameter("@FatherQualification", SqlDbType.VarChar);
                pSqlParameter[71].Direction = ParameterDirection.Input;
                pSqlParameter[71].Value = objStudentBO.FatherQualification;

                pSqlParameter[72] = new SqlParameter("@MotherQualification", SqlDbType.VarChar);
                pSqlParameter[72].Direction = ParameterDirection.Input;
                pSqlParameter[72].Value = objStudentBO.MotherQualification;

                pSqlParameter[73] = new SqlParameter("@GardianQualification", SqlDbType.VarChar);
                pSqlParameter[73].Direction = ParameterDirection.Input;
                pSqlParameter[73].Value = objStudentBO.GardianQualification;

                pSqlParameter[74] = new SqlParameter("@FatherMobileNo", SqlDbType.VarChar);
                pSqlParameter[74].Direction = ParameterDirection.Input;
                pSqlParameter[74].Value = objStudentBO.FatherMobileNo;

                pSqlParameter[75] = new SqlParameter("@MotherMobileNo", SqlDbType.VarChar);
                pSqlParameter[75].Direction = ParameterDirection.Input;
                pSqlParameter[75].Value = objStudentBO.MotherMobileNo;

                pSqlParameter[76] = new SqlParameter("@GardianMobileNo", SqlDbType.VarChar);
                pSqlParameter[76].Direction = ParameterDirection.Input;
                pSqlParameter[76].Value = objStudentBO.GardianMobileNo;

                pSqlParameter[77] = new SqlParameter("@FatherEmailID", SqlDbType.VarChar);
                pSqlParameter[77].Direction = ParameterDirection.Input;
                pSqlParameter[77].Value = objStudentBO.FatherEmailID;

                pSqlParameter[78] = new SqlParameter("@MotherEmailID", SqlDbType.VarChar);
                pSqlParameter[78].Direction = ParameterDirection.Input;
                pSqlParameter[78].Value = objStudentBO.MotherEmailID;

                pSqlParameter[79] = new SqlParameter("@GardianEmailID", SqlDbType.VarChar);
                pSqlParameter[79].Direction = ParameterDirection.Input;
                pSqlParameter[79].Value = objStudentBO.GardianEmailID;

                pSqlParameter[80] = new SqlParameter("@Height", SqlDbType.VarChar);
                pSqlParameter[80].Direction = ParameterDirection.Input;
                pSqlParameter[80].Value = objStudentBO.Height;

                pSqlParameter[81] = new SqlParameter("@Wight", SqlDbType.VarChar);
                pSqlParameter[81].Direction = ParameterDirection.Input;
                pSqlParameter[81].Value = objStudentBO.Weight;

                pSqlParameter[82] = new SqlParameter("@Hobbies", SqlDbType.VarChar);
                pSqlParameter[82].Direction = ParameterDirection.Input;
                pSqlParameter[82].Value = objStudentBO.Hobbies;

                pSqlParameter[83] = new SqlParameter("@StatusMasterID", SqlDbType.Int);
                pSqlParameter[83].Direction = ParameterDirection.Input;
                pSqlParameter[83].Value = objStudentBO.StatusMasterID;

                pSqlParameter[84] = new SqlParameter("@LeftDate", SqlDbType.VarChar);
                pSqlParameter[84].Direction = ParameterDirection.Input;
                pSqlParameter[84].Value = objStudentBO.LeftDate;

                pSqlParameter[85] = new SqlParameter("@LeftReason", SqlDbType.VarChar);
                pSqlParameter[85].Direction = ParameterDirection.Input;
                pSqlParameter[85].Value = objStudentBO.LeftReason;

                pSqlParameter[86] = new SqlParameter("@LeftYear", SqlDbType.VarChar);
                pSqlParameter[86].Direction = ParameterDirection.Input;
                pSqlParameter[86].Value = objStudentBO.LeftYear;

                pSqlParameter[87] = new SqlParameter("@LeftStd", SqlDbType.VarChar);
                pSqlParameter[87].Direction = ParameterDirection.Input;
                pSqlParameter[87].Value = objStudentBO.LeftStd;

                pSqlParameter[88] = new SqlParameter("@LcNo", SqlDbType.VarChar);
                pSqlParameter[88].Direction = ParameterDirection.Input;
                pSqlParameter[88].Value = objStudentBO.LcNo;

                pSqlParameter[89] = new SqlParameter("@LcDate", SqlDbType.VarChar);
                pSqlParameter[89].Direction = ParameterDirection.Input;
                pSqlParameter[89].Value = objStudentBO.LcDate;

                pSqlParameter[90] = new SqlParameter("@LcRemarks", SqlDbType.VarChar);
                pSqlParameter[90].Direction = ParameterDirection.Input;
                pSqlParameter[90].Value = objStudentBO.LcRemarks;

                pSqlParameter[91] = new SqlParameter("@LcCopy", SqlDbType.VarChar);
                pSqlParameter[91].Direction = ParameterDirection.Input;
                pSqlParameter[91].Value = objStudentBO.LcCopy;

                pSqlParameter[92] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[92].Direction = ParameterDirection.Input;
                pSqlParameter[92].Value = objStudentBO.LastModifiedUserID;

                pSqlParameter[93] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[93].Direction = ParameterDirection.Input;
                pSqlParameter[93].Value = objStudentBO.LastModifiedDate;

                pSqlParameter[94] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[94].Direction = ParameterDirection.Input;
                pSqlParameter[94].Value = objStudentBO.IsDeleted;

                pSqlParameter[95] = new SqlParameter("@RegisteredYear", SqlDbType.VarChar);
                pSqlParameter[95].Direction = ParameterDirection.Input;
                pSqlParameter[95].Value = objStudentBO.RegisteredYear;

                pSqlParameter[96] = new SqlParameter("@AdmissionDate", SqlDbType.VarChar);
                pSqlParameter[96].Direction = ParameterDirection.Input;
                pSqlParameter[96].Value = objStudentBO.AdmissionDate;

                pSqlParameter[97] = new SqlParameter("@GVUniqueID", SqlDbType.VarChar);
                pSqlParameter[97].Direction = ParameterDirection.Input;
                pSqlParameter[97].Value = objStudentBO.GVUniqueID;

                pSqlParameter[98] = new SqlParameter("@BankAccount", SqlDbType.VarChar);
                pSqlParameter[98].Direction = ParameterDirection.Input;
                pSqlParameter[98].Value = objStudentBO.BankAccount;

                pSqlParameter[99] = new SqlParameter("@IsLate", SqlDbType.Int);
                pSqlParameter[99].Direction = ParameterDirection.Input;
                pSqlParameter[99].Value = objStudentTBO.IsLate;


                pSqlParameter[100] = new SqlParameter("@MotherTongue", SqlDbType.VarChar);
                pSqlParameter[100].Direction = ParameterDirection.Input;
                pSqlParameter[100].Value = objStudentBO.MotherTongue;

                pSqlParameter[101] = new SqlParameter("@PreviousSchoolDetails", SqlDbType.VarChar);
                pSqlParameter[101].Direction = ParameterDirection.Input;
                pSqlParameter[101].Value = objStudentBO.PreviousSchoolDetails;

                pSqlParameter[102] = new SqlParameter("@PhysicalIdentification", SqlDbType.VarChar);
                pSqlParameter[102].Direction = ParameterDirection.Input;
                pSqlParameter[102].Value = objStudentBO.PhysicalIdentification;

                pSqlParameter[103] = new SqlParameter("@FatherOrganisationName", SqlDbType.VarChar);
                pSqlParameter[103].Direction = ParameterDirection.Input;
                pSqlParameter[103].Value = objStudentBO.FatherOrganisationName;

                pSqlParameter[104] = new SqlParameter("@FatherOrganisationContactNumber", SqlDbType.VarChar);
                pSqlParameter[104].Direction = ParameterDirection.Input;
                pSqlParameter[104].Value = objStudentBO.FatherOrganisationContactNumber;

                pSqlParameter[105] = new SqlParameter("@BloodGroup", SqlDbType.VarChar);
                pSqlParameter[105].Direction = ParameterDirection.Input;
                pSqlParameter[105].Value = objStudentBO.BloodGroup;

                pSqlParameter[106] = new SqlParameter("@IFSCCode", SqlDbType.VarChar);
                pSqlParameter[106].Direction = ParameterDirection.Input;
                pSqlParameter[106].Value = objStudentBO.IFSCCode;

                pSqlParameter[107] = new SqlParameter("@BranchName", SqlDbType.VarChar);
                pSqlParameter[107].Direction = ParameterDirection.Input;
                pSqlParameter[107].Value = objStudentBO.BranchName;


                pSqlParameter[108] = new SqlParameter("@AccountNumber", SqlDbType.VarChar);
                pSqlParameter[108].Direction = ParameterDirection.Input;
                pSqlParameter[108].Value = objStudentBO.AccountNumber;


                pSqlParameter[109] = new SqlParameter("@TypeOfVehicle", SqlDbType.VarChar);
                pSqlParameter[109].Direction = ParameterDirection.Input;
                pSqlParameter[109].Value = objStudentBO.TypeOfVehicle;

                pSqlParameter[110] = new SqlParameter("@VehicleNo", SqlDbType.VarChar);
                pSqlParameter[110].Direction = ParameterDirection.Input;
                pSqlParameter[110].Value = objStudentBO.VehicleNo;

                pSqlParameter[111] = new SqlParameter("@DriverName", SqlDbType.VarChar);
                pSqlParameter[111].Direction = ParameterDirection.Input;
                pSqlParameter[111].Value = objStudentBO.DriverName;


                pSqlParameter[112] = new SqlParameter("@DriverContactNo", SqlDbType.VarChar);
                pSqlParameter[112].Direction = ParameterDirection.Input;
                pSqlParameter[112].Value = objStudentBO.DriverContactNo;

                pSqlParameter[113] = new SqlParameter("@AadharCardNo", SqlDbType.VarChar);
                pSqlParameter[113].Direction = ParameterDirection.Input;
                pSqlParameter[113].Value = objStudentBO.AadharCardNo;

                pSqlParameter[114] = new SqlParameter("@RollNumber", SqlDbType.VarChar);
                pSqlParameter[114].Direction = ParameterDirection.Input;
                pSqlParameter[114].Value = objStudentBO.RollNumber;

                pSqlParameter[115] = new SqlParameter("@GvUniqueNo", SqlDbType.VarChar);
                pSqlParameter[115].Direction = ParameterDirection.Input;
                pSqlParameter[115].Value = objStudentBO.GvUniqueNo;

                sSql = "usp_tbl_Student_M_Update";
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
				objStudentBO = null;
			}
		}
		#endregion
      
		#region Select Student Details by StudentName
		/// <summary>
		/// Select all details of Student for selected StudentName from tbl_Student_M table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="StudentName"></param>
		/// <returns></returns>
		public ApplicationResult Student_Select_byStudentName(string strStudentName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@StudentName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strStudentName;

				strStoredProcName = "usp_tbl_Student_M_Select_ByStudent";

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

        #region Select for Student Select For Upgrade
        public ApplicationResult Student_SelectFor_Upgrade(int intSchoolMID,int intClassMID, int intDivisionTID, string strAcademicYear, int intStatusMasterID)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                pSqlParameter[1] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intClassMID;

                pSqlParameter[2] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intDivisionTID;

                pSqlParameter[3] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strAcademicYear;

                pSqlParameter[4] = new SqlParameter("@StatusMasterID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intStatusMasterID;


                strStoredProcName = "usp_tbl__Select_Student_For_Upgrade";

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

		#region ValidateName for Student
		/// <summary>
		/// Function which validates whether the StudentName already exits in tbl_Student_M table.
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="strStudentName"></param>
		/// <returns></returns>
		public ApplicationResult Student_ValidateName(string strCurrentGRNo, int intSectionID, int intStudentMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@CurrentGrNo", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strCurrentGRNo;

                pSqlParameter[1] = new SqlParameter("@CurrentSectionID ", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSectionID;

                pSqlParameter[2] = new SqlParameter("@StudentMID", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intStudentMID;

                strStoredProcName = "usp_tbl_Student_M_ValidationName";

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

        #region Search Student By Different Way
        /// <summary>
        /// Function which validates whether the StudentTName already exits in tbl_Student_T table.
        /// Created By : Amruta, 5/3/2013
        /// Modified By :
        /// </summary>
        /// <param name="strStudentTName"></param>
        /// <returns></returns>
        public ApplicationResult Student_Search_StudentName(string strStudentName, int strSearchID, int intEmployeeRoleID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@SearchText", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strStudentName;

                pSqlParameter[1] = new SqlParameter("@SearchID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strSearchID;

                pSqlParameter[2] = new SqlParameter("@EmployeeRoleID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intEmployeeRoleID;

                pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_Student_M_Search_Student";

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

        #region Select All StudentT Details
        /// <summary>
        /// To Select All data from the tbl_Student_T table
        /// Created By : Darshan, 18-09-2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult StudentT_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_Student_T_SelectAll";
                DataTable dtStudentT = new DataTable();
                dtStudentT = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtStudentT);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

		#region Select StudentT Details by StudentTID
		/// <summary>
		/// Select all details of StudentT for selected StudentTID from StudentT table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="intStudentTID"></param>
		/// <returns></returns>
		public ApplicationResult StudentT_Select(int intStudentTID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@StudentTID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intStudentTID;

				strStoredProcName = "usp_tbl_Student_T_Select";

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

		#region Delete StudentT Details by StudentTID
		/// <summary>
		/// To Delete details of StudentT for selected StudentTID from StudentT table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="intStudentTID"></param>
		/// <returns></returns>
		public ApplicationResult StudentT_Delete(int intStudentTID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@StudentTID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intStudentTID;

                strStoredProcName = "usp_tbl_Student_T_Delete";

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

        #region Insert StudentT Details
        /// <summary>
        /// To Insert details of StudentT in tbl_Student_T table
        /// Created By : Darshan, 18-09-2014
        /// Modified By :
        /// </summary>
        /// <param name="objStudentTBO"></param>
        /// <returns></returns>
        public ApplicationResult StudentT_Insert(StudentTBO objStudentTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[11];


                pSqlParameter[0] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objStudentTBO.StudentMID;

                pSqlParameter[1] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objStudentTBO.ClassMID;

                pSqlParameter[2] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objStudentTBO.DivisionTID;

                pSqlParameter[3] = new SqlParameter("@StatusMasterID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objStudentTBO.StatusMasterID;

                pSqlParameter[4] = new SqlParameter("@Year", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objStudentTBO.Year;

                pSqlParameter[5] = new SqlParameter("@StatusName", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objStudentTBO.StatusName;

                pSqlParameter[6] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objStudentTBO.LastModifiedUserID;

                pSqlParameter[7] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objStudentTBO.LastModifiedDate;

                pSqlParameter[8] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objStudentTBO.IsDeleted;

                pSqlParameter[9] = new SqlParameter("@Grno", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objStudentTBO.GrNo;

                pSqlParameter[10] = new SqlParameter("@IsLate", SqlDbType.Int);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objStudentTBO.IsLate;

                sSql = "usp_tbl_Student_T_Insert";
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
                objStudentTBO = null;
            }
        }
        #endregion

        #region Update StudentT Details
        /// <summary>
        /// To Update details of StudentT in tbl_Student_T table
        /// Created By : Darshan, 18-09-2014
        /// Modified By :
        /// </summary>
        /// <param name="objStudentTBO"></param>
        /// <returns></returns>
        public ApplicationResult StudentT_Update(StudentTBO objStudentTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[11];


                pSqlParameter[0] = new SqlParameter("@StudentTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objStudentTBO.StudentTID;

                pSqlParameter[1] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objStudentTBO.StudentMID;

                pSqlParameter[2] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objStudentTBO.ClassMID;

                pSqlParameter[3] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objStudentTBO.DivisionTID;

                pSqlParameter[4] = new SqlParameter("@StatusMasterID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objStudentTBO.StatusMasterID;

                pSqlParameter[5] = new SqlParameter("@Year", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objStudentTBO.Year;

                pSqlParameter[6] = new SqlParameter("@StatusName", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objStudentTBO.StatusName;

                pSqlParameter[7] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objStudentTBO.LastModifiedUserID;

                pSqlParameter[8] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objStudentTBO.LastModifiedDate;

                pSqlParameter[9] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objStudentTBO.IsDeleted;

                pSqlParameter[10] = new SqlParameter("@Grno", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objStudentTBO.GrNo;


                sSql = "usp_tbl_Student_T_Update";
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
                objStudentTBO = null;
            }
        }
        #endregion

        #region Update StudentT Details At Upgrade Time 
        /// <summary>
        /// To Update details of StudentT in tbl_Student_T table
        /// Created By : Darshan, 18-09-2014
        /// Modified By :
        /// </summary>
        /// <param name="objStudentTBO"></param>
        /// <returns></returns>
        public ApplicationResult StudentT_Update_AtTimeOfUpgrade(int intStudentTID,int intStatusMID, string strStatusName, int intLastModifiedUserID, string strLastModifiedDate, string strGrNo)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];


                pSqlParameter[0] = new SqlParameter("@StudentTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentTID;

                pSqlParameter[1] = new SqlParameter("@StatusMasterID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intStatusMID;

                pSqlParameter[2] = new SqlParameter("@StatusName", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strStatusName;

                pSqlParameter[3] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intLastModifiedUserID;

                pSqlParameter[4] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = strLastModifiedDate;

                pSqlParameter[5] = new SqlParameter("@GrNo", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = strGrNo;

                sSql = "usp_tbl_Student_T_Update_AtTimeOfUpgrade";
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
          
        }
        #endregion

        #region Update StudentT Details At Division Transfer
        /// <summary>
        /// To Update details of StudentT in tbl_Student_T table
        /// Created By : Darshan, 18-09-2014
        /// Modified By :
        /// </summary>
        /// <param name="objStudentTBO"></param>
        /// <returns></returns>
        public ApplicationResult StudentT_Update_AtTimeOfDivisionTransfer(int intStudentTID, int intDivisionTID, int intLastModifiedUserID, string strLastModifiedDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];


                pSqlParameter[0] = new SqlParameter("@StudentTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentTID;

                pSqlParameter[1] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intDivisionTID;

                pSqlParameter[2] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intLastModifiedUserID;

                pSqlParameter[3] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strLastModifiedDate;


                sSql = "usp_tbl_Student_T_Update_AtTimeOfDivisionTransfer";
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

        }
        #endregion

        #region Update StudentM Details At Upgrade Time
        /// <summary>
        /// To Update details of StudentT in tbl_Student_T table
        /// Created By : Darshan, 18-09-2014
        /// Modified By :
        /// </summary>
        /// <param name="objStudentTBO"></param>
        /// <returns></returns>
        public ApplicationResult StudentM_Update_AtTimeOfUpgrade(int intStudentMID, int intSectionMID, int intClassMID,int intDivisionTID,string strAcademicYear,int intStatusMID, int intLastModifiedUserID, string strLastModifiedDate, string strGrNO)
        {
            try
            {
                pSqlParameter = new SqlParameter[9];


                pSqlParameter[0] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentMID;

                pSqlParameter[1] = new SqlParameter("@CurrentSectionID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSectionMID;

                pSqlParameter[2] = new SqlParameter("@CurrentClassID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intClassMID;

                pSqlParameter[3] = new SqlParameter("@CurrentDivisionTID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intDivisionTID;

                pSqlParameter[4] = new SqlParameter("@CurrentYear", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = strAcademicYear;

                pSqlParameter[5] = new SqlParameter("@StatusMasterID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = intStatusMID;

                pSqlParameter[6] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = intLastModifiedUserID;

                pSqlParameter[7] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = strLastModifiedDate;

                pSqlParameter[8] = new SqlParameter("@CurrentGrNo", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = strGrNO;



                sSql = "usp_tbl_Student_M_Update_AtTimeOfUpgrade";
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

        }
        #endregion

        #region Update StudentM Details At Division Transfer Times
        /// <summary>
        /// To Update details of StudentT in tbl_Student_T table
        /// Created By : Darshan, 18-09-2014
        /// Modified By :
        /// </summary>
        /// <param name="objStudentTBO"></param>
        /// <returns></returns>
        public ApplicationResult StudentM_Update_AtTimeOfDivisionTransfer(int intStudentMID, int intDivisionTID,  int intLastModifiedUserID, string strLastModifiedDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];


                pSqlParameter[0] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentMID;

                pSqlParameter[1] = new SqlParameter("@CurrentDivisionTID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intDivisionTID;

                pSqlParameter[2] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intLastModifiedUserID;

                pSqlParameter[3] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strLastModifiedDate;

                sSql = "usp_tbl_Student_M_Update_AtTimeOfDivisionTransfer";
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

        }
        #endregion

        #region Select Students For Class Wise Student FeesReport
        public ApplicationResult FeesCollction_ClassWiseStudentFees(int intTrusMID, int intSchoolMID, int intClassMID, int intDivisionID, string strAcademicYear, string strFromDate, string strToDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[7];


                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrusMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                pSqlParameter[2] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intClassMID;

                pSqlParameter[3] = new SqlParameter("@DivisionID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intDivisionID;

                pSqlParameter[4] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = strAcademicYear;

                pSqlParameter[5] = new SqlParameter("@FromDate", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = strFromDate;

                pSqlParameter[6] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = strToDate;


                strStoredProcName = "usp_Rpt_ClassWiseStudentFees";

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

        #region Update StudentM Details At Upgrade Time Of Status
        /// <summary>
        /// To Update details of StudentT in tbl_Student_T table
        /// Created By : Darshan, 18-09-2014
        /// Modified By :
        /// </summary>
        /// <param name="objStudentTBO"></param>
        /// <returns></returns>
        public ApplicationResult StudentM_Update_AtTimeOfUpgradeOfStatus(int intStudentMID, int intStatusMID, int intLastModifiedUserID, string strLastModifiedDate, string strGrNo, string strAcademicYear, string strLeftDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[7];


                pSqlParameter[0] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentMID;
              
                pSqlParameter[1] = new SqlParameter("@StatusMasterID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intStatusMID;

                pSqlParameter[2] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intLastModifiedUserID;

                pSqlParameter[3] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strLastModifiedDate;

                pSqlParameter[4] = new SqlParameter("@CurrentGrNo", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = strGrNo;

                pSqlParameter[5] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = strAcademicYear;

                pSqlParameter[6] = new SqlParameter("@LeftDate", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = strLeftDate;

                sSql = "usp_tbl_Student_M_Update_AtTimeOfUpgradeOfStatus";
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

        }
        #endregion

		#region Select StudentT Details by StudentTName
		/// <summary>
		/// Select all details of StudentT for selected StudentTName from StudentT table
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="StudentTName"></param>
		/// <returns></returns>
		public ApplicationResult StudentT_Select_byStudentTName(string strStudentTName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@StudentTName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strStudentTName;

				strStoredProcName = "usp_StudentT_Select_ByStudentT";

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

		#region ValidateName for StudentT
		/// <summary>
		/// Function which validates whether the StudentTName already exits in StudentT table.
		/// Created By : NafisaMulla, 12/6/2014
		/// Modified By :
		/// </summary>
		/// <param name="strStudentTName"></param>
		/// <returns></returns>
		public ApplicationResult StudentT_ValidateName(string strStudentTName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@StudentTName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strStudentTName;

				strStoredProcName = "usp_StudentT_Validate_StudentTName";

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

        #region Select for Student Select For Report
        public ApplicationResult StudentList_ForReport(int intSchoolMID, int intSectionMID, int intClassMID, int intDivisionTID, string strAcademicYear, int intStatusMasterID)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                pSqlParameter[1] = new SqlParameter("@SectionMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSectionMID;

                pSqlParameter[2] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intClassMID;

                pSqlParameter[3] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intDivisionTID;

                pSqlParameter[4] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = strAcademicYear;

                pSqlParameter[5] = new SqlParameter("@StatusMasterID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = intStatusMasterID;


                strStoredProcName = "usp_tbl_Student_M_ListForReport";

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

        #region Select for Student Select For Report
        public ApplicationResult StudentList_ForReportExport(int intSchoolMID, int intSectionMID, int intClassMID, int intDivisionTID, string strAcademicYear, int intStatusMasterID)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                pSqlParameter[1] = new SqlParameter("@SectionMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSectionMID;

                pSqlParameter[2] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intClassMID;

                pSqlParameter[3] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intDivisionTID;

                pSqlParameter[4] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = strAcademicYear;

                pSqlParameter[5] = new SqlParameter("@StatusMasterID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = intStatusMasterID;


                strStoredProcName = "usp_tbl_Student_M_ListForReport_Export";

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

        #region Update StudentT Details At Upgrade to Left
        /// <summary>
        /// To Update details of StudentT in tbl_Student_T table
        /// Created By : Darshan, 18-09-2014
        /// Modified By :
        /// </summary>
        /// <param name="objStudentTBO"></param>
        /// <returns></returns>
        public ApplicationResult StudentM_UpgradeToLeft(int intStudentMID, int intStatusMID, int intLastModifiedUserID, string strLastModifiedDate, string strLeftDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];


                pSqlParameter[0] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentMID;

                pSqlParameter[1] = new SqlParameter("@StatusMasterID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intStatusMID;

                pSqlParameter[2] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intLastModifiedUserID;

                pSqlParameter[3] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strLastModifiedDate;

                pSqlParameter[4] = new SqlParameter("@LeftDate", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = strLeftDate;

                sSql = "usp_tbl_Student_T_Update_AtTimeOfUpgrade";
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

        }
        #endregion

        #region Select Student Details for autocomplete
        /// <summary>
        /// Select all details of Student for selected StudentMID from tbl_Student_M table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="intStudentMID"></param>
        /// <returns></returns>
        public ApplicationResult Student_ForAutocomplete(string strSearch, int intSearchType, int intSchoolMID, int intSectionType, int intClassType, int intDivisionType)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

                pSqlParameter[0] = new SqlParameter("@Search", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strSearch;

                pSqlParameter[1] = new SqlParameter("@SearchType", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSearchType;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                pSqlParameter[3] = new SqlParameter("@SectionMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intSectionType;

                pSqlParameter[4] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intClassType;

                pSqlParameter[5] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = intDivisionType;

                strStoredProcName = "usp_tbl_Student_M_ForAutoComplete";

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

        #region Select Student Details for autocomplete
        /// <summary>
        /// Select all details of Student for selected StudentMID from tbl_Student_M table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="intStudentMID"></param>
        /// <returns></returns>
        public ApplicationResult Student_ForAutocomplete_ForClasswiseStudentTemplate(int intClassMID, int intDivisionTID, int intSchoolMID, string strAcademicYear, string strSearch)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                pSqlParameter[1] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intClassMID;

                pSqlParameter[2] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intDivisionTID;

                pSqlParameter[3] = new SqlParameter("@AcademicYear", SqlDbType.NVarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strAcademicYear;

                pSqlParameter[4] = new SqlParameter("@Search", SqlDbType.NVarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = strSearch;

                strStoredProcName = "usp_tbl_Student_M_ForAutoComplete_ForClasswise_StudentTemplate";

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

        #region Select for Student For DEO reports
        public ApplicationResult Select_Student_ForDeoReports(int intTrustMID, int IntSchoolMID,int intStudentMID, int intType, int intIsEnglish)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = IntSchoolMID;

                pSqlParameter[2] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intStudentMID;

                pSqlParameter[3] = new SqlParameter("@intType", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intType;

                pSqlParameter[4] = new SqlParameter("@IsEnglish", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intIsEnglish;

                strStoredProcName = "usp_rpt_StudentDetail_ForDeoReport";

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

        #region Select Student Details for autocomplete for report
        /// <summary>
        /// Select all details of Student for selected StudentMID from tbl_Student_M table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="intStudentMID"></param>
        /// <returns></returns>
        public ApplicationResult Student_Autocomplete_ForReport(string strSearch, int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@Search", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strSearch;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_Student_M_ForAutoComplete_ForReport";

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

        #region Update StudentT Details At Upgrade to Left
        /// <summary>
        /// To Update details of StudentT in tbl_Student_T table
        /// Created By : Darshan, 18-09-2014
        /// Modified By :
        /// </summary>
        /// <param name="objStudentTBO"></param>
        /// <returns></returns>
        public ApplicationResult StudentM_UpdateForLeavingCerti(int intStudentMID, int intIsLeavingCerti,string strLeftDate, int intIsLeavingGujaratiCerti, string strLastModifiedDate, int intLastModifiedUserID, int intIsEnglish)
        {
            try
            {
                pSqlParameter = new SqlParameter[7];


                pSqlParameter[0] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentMID;

                pSqlParameter[1] = new SqlParameter("@IsLeavingCerti", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intIsLeavingCerti;

                pSqlParameter[2] = new SqlParameter("@LeftDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLeftDate;

                pSqlParameter[3] = new SqlParameter("@IsLeavingGujaratiCerti", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intIsLeavingGujaratiCerti;

                pSqlParameter[4] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = strLastModifiedDate;

                pSqlParameter[5] = new SqlParameter("@LastModifiedUserID", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = intLastModifiedUserID;

                pSqlParameter[6] = new SqlParameter("@IsEnglish", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = intIsEnglish;

                sSql = "usp_tbl_Student_M_Update_ForLeavingCerti";
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

        }
        #endregion

        #region Select StudentT For Report
        /// <summary>
        /// Select all details of StudentT for selected StudentTID from StudentT table
        /// Created By : NafisaMulla, 12/6/2014
        /// Modified By :
        /// </summary>
        /// <param name="intStudentTID"></param>
        /// <returns></returns>
        public ApplicationResult StudentDetail_ForCategoryWiseReport(int intTrustMID, int intSchoolMID, string strAcademicYear, int intStatusID)
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

                pSqlParameter[2] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strAcademicYear;

                pSqlParameter[3] = new SqlParameter("@StatusMasterID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intStatusID;

                strStoredProcName = "usp_rpt_StudentDetail_CategoryWise";

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

        #region Student Get Status
        /// <summary>
        /// Select all details of StudentT for selected StudentTID from StudentT table
        /// Created By : NafisaMulla, 12/6/2014
        /// Modified By :
        /// </summary>
        /// <param name="intStudentTID"></param>
        /// <returns></returns>
        public ApplicationResult Student_GetStatus(int intStudentMID, string strAcademicYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentMID;

                pSqlParameter[1] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strAcademicYear;

                strStoredProcName = "usp_tbl_Student_GetStatus";

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

        #region Student Get Status
        /// <summary>
        /// Select all details of StudentT for selected StudentTID from StudentT table
        /// Created By : NafisaMulla, 12/6/2014
        /// Modified By :
        /// </summary>
        /// <param name="intStudentTID"></param>
        /// <returns></returns>
        public ApplicationResult Student_UpdateStatus(int intStudentMID, string strAcademicYear, int intLastModifiedUserId, string strLastModifiedDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentMID;

                pSqlParameter[1] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strAcademicYear;

                pSqlParameter[2] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intLastModifiedUserId;

                pSqlParameter[3] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strLastModifiedDate;

                strStoredProcName = "usp_tbl_Student_upgrade_Delete_LeftFailStudent";

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

        #region Select Students For Date Wise Student FeesReport
        public ApplicationResult FeesCollction_DatesWiseStudentFees(int intTrusMID, int intSchoolMID, string strFromDate, string strToDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];


                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrusMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                pSqlParameter[2] = new SqlParameter("@FromDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strFromDate;

                pSqlParameter[3] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strToDate;


                strStoredProcName = "usp_rpt_DateWise_FeesCollection";

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

        public ApplicationResult GetTotalStudentCount_For_AttendanceConsolidated(int SchoolMID,int ClassMID,int intDivisionTID,string strAcademicYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = SchoolMID;

                pSqlParameter[1] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = ClassMID;

                pSqlParameter[2] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intDivisionTID;

                pSqlParameter[3] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strAcademicYear;

                strStoredProcName = "usp_tbl_Get_TotalStudentCount_For_StudentAttendance_Consolidated";

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
    }
}


