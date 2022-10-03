using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class StudentAttemptBO
    {
        #region StudentAttempt Class Properties
		
		public const string STUDENTATTEMPT_TABLE = "tbl_StudentAttempt";
		public const string STUDENTATTEMPT_STUDENTATTEMPTID = "StudentAttemptID";
        public const string STUDENTATTEMPT_STUDENTMID = "StudentMID";
        public const string STUDENTATTEMPT_ATTEMPT = "Attempt";
        public const string STUDENTATTEMPT_PERCENT = "Percent";
        public const string STUDENTATTEMPT_YEAR = "Year";
        public const string STUDENTATTEMPT_MONTH = "Month";
        public const string STUDENTATTEMPT_SEATNO = "SeatNo";
        public const string STUDENTATTEMPT_ISATTEMPT = "IsAttempt";

			
		
		private int intStudentAttemptID = 0;
		private int intStudentMID = 0;
		private string strAttempt = string.Empty;
		private string strPercent = string.Empty;
		private string strYear = string.Empty;
		private string strMonth = string.Empty;
        private string strSeatNo = string.Empty;
		private int intIsAttempt = 0;

		#endregion
		
		#region ---Properties---
		public int StudentAttemptID
		{
			get { return intStudentAttemptID;}
			set { intStudentAttemptID = value;}
		}
		public int StudentMID
		{
			get { return intStudentMID;}
			set { intStudentMID = value;}
		}
		public string Attempt
		{
			get { return strAttempt;}
			set { strAttempt = value;}
		}
		public string Percent
		{
			get { return strPercent;}
			set { strPercent = value;}
		}
		public string Year
		{
			get { return strYear;}
			set { strYear = value;}
		}
		public string Month
		{
			get { return strMonth;}
			set { strMonth = value;}
		}

        public string SeatNo
        {
            get{ return strSeatNo; }
            set { strSeatNo = value; }
        }

		public int IsAttempt
		{
			get { return intIsAttempt;}
			set { intIsAttempt = value;}
		}

		#endregion
	
    }
}
