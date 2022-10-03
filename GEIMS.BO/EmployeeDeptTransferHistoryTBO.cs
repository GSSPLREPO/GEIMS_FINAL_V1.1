using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
   public class EmployeeDeptTransferHistoryTBO
    {

        #region Employee Department Transfer History Class Properties
        public const string EMPLOYEEDEPT_TRANSFER_HISTORY_ID = "EmployeeDeptTransferHistoryID";
        public const string EMPLOYEEDEPT_TRANSFER_HISTORY_EMPLOYEEMID = "EmployeeMID";
        public const string EMPLOYEEDEPT_TRANSFER_HISTORY_EMPLOYEECODE = "EmployeeCode";
        public const string EMPLOYEEDEPT_TRANSFER_HISTORY_DEPARTMENTID = "DepartmentID";
        public const string EMPLOYEEDEPT_TRANSFER_HISTORY_DESIGNATIONID = "DesignationID";
        public const string EMPLOYEEDEPT_TRANSFER_HISTORY_DEPARTMENTJOININGDATE = "DepartmentJoiningDate";     
        public const string EMPLOYEEDEPT_TRANSFER_HISTORY_ReportingTo = "ReportingTo";     
        public const string EMPLOYEEDEPT_TRANSFER_HISTORY_IsACTIVE = "IsActive";
        public const string EMPLOYEEDEPT_TRANSFER_HISTORY_CREATEDByID = "CreatedByID";
        public const string EMPLOYEEDEPT_TRANSFER_HISTORY_CREATEDDATE = "CreatedDate";
        public const string EMPLOYEEDEPT_TRANSFER_HISTORY_MODIFIEDByID = "ModifiedByID";
        public const string EMPLOYEEDEPT_TRANSFER_HISTORY_MODIFIEDDATE = "ModifiedDate";


        private int intEmployeeDeptTransferHistory = 0;
        private int intEmployeeMID = 0;
        private string strEmployeeCode = string.Empty;
        private int intDepartmentID = 0;
        private int intDesignationID = 0;     
        private string strDepartmentJoiningDate = string.Empty;      
        private int intReportingTo = 0;
        private Boolean intIsActive = false;
        private int intCreatedByID = 0;
        private string strCreatedDate = string.Empty;
        private int intModifiedByID = 0;
        private string strModifiedDate = string.Empty;


        #endregion

        #region ---Properties---
        public int EmployeeMID
        {
            get { return intEmployeeMID; }
            set { intEmployeeMID = value; }
        }
        public int DepartmentID
        {
            get { return intDepartmentID; }
            set { intDepartmentID = value; }
        }
        public int DesignationID
        {
            get { return intDesignationID; }
            set { intDesignationID = value; }
        }
        public int ReportingTo
        {
            get { return intReportingTo; }
            set { intReportingTo = value; }
        }
        public string EmployeeCode
        {
            get { return strEmployeeCode; }
            set { strEmployeeCode = value; }
        }
        public string DepartmentJoiningDate
        {
            get { return strDepartmentJoiningDate; }
            set { strDepartmentJoiningDate = value; }
        }
      
        public Boolean IsActive {
            get { return intIsActive; }
            set { intIsActive = value; }
        }
        public int CreatedByID
        {
            get { return intCreatedByID; }
            set { intCreatedByID = value; }
        }
        public string CreatedDate
        {
            get { return strCreatedDate; }
            set { strCreatedDate = value; }
        }
        public int ModifiedByID
        {
            get { return intModifiedByID; }
            set { intModifiedByID = value; }
        }
        public string ModifiedDate
        {
            get { return strModifiedDate; }
            set { strModifiedDate = value; }
        }
        #endregion
    }
}
