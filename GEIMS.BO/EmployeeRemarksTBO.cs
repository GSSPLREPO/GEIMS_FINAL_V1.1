using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GEIMS.BO
{
   public class EmployeeRemarksTBO
    {

        #region EmployeeRemarksT Class Properties
        public const string EMPLOYEEREMARKST_TABLE = "tbl_EmployeeRemarks_T";
        public const string EMPLOYEEREMARKST_REMARKSTID = "RemarksTID";
        public const string EMPLOYEEREMARKST_REMARKS = "Remarks";
        public const string EMPLOYEEREMARKST_REMARKSDATE = "RemarksDate";     
        public const string EMPLOYEEREMARKST_EMPLOYEEMID = "EmployeeMID";
        public const string EMPLOYEEREMARKST_ISDELETED = "IsDeleted";


        private int intRemarksTID = 0;
        private string strRemarks = string.Empty;
        private DateTime dtRemarksDate = DateTime.UtcNow;   
        private int intEmployeeMID = 0;
        private int intIsDeleted = 0;

        #endregion

        #region ---Properties---

        public int RemarksTID
        {
            get { return intRemarksTID; }
            set { intRemarksTID = value; }
        }
        public string Remarks {
            get { return strRemarks; }
            set { strRemarks = value; }
        }
        public DateTime RemarksDate {
            get { return dtRemarksDate; }
            set { dtRemarksDate = value; }
        }
        public int EmployeeMID
        {
            get { return intEmployeeMID; }
            set { intEmployeeMID = value; }
        }
        public int IsDeleted
        {
            get { return intIsDeleted; }
            set { intIsDeleted = value; }
        }
        #endregion
    }
}
