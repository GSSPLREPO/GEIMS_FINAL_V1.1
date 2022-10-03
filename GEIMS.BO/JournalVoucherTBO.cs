using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
	public class JournalVoucherTBO
	{
		#region JournalVoucherT Class Properties
		
		public const string JOURNALVOUCHERT_TABLE = "tbl_JournalVoucher_T";
		public const string JOURNALVOUCHERT_JOURNALTID = "JournalTID";
        public const string JOURNALVOUCHERT_JOURNALID = "JournalID";
        public const string JOURNALVOUCHERT_OPPOSITEJOURNALID = "OppositeJournalID";
        public const string JOURNALVOUCHERT_AMOUNT = "Amount";
        public const string JOURNALVOUCHERT_CREATEDDATE = "CreatedDate";
        public const string JOURNALVOUCHERT_CREATEDUSERID = "CreatedUserID";
        public const string JOURNALVOUCHERT_ISDELETED = "IsDeleted";
        public const string JOURNALVOUCHERT_LASTMODIFIDEUSERID = "LastModifideUserID";
        public const string JOURNALVOUCHERT_LASTMODIFIDEDATE = "LastModifideDate";

			
		
		private int intJournalTID = 0;
		private int intJournalID = 0;
		private int intOppositeJournalID = 0;
		private double dbAmount = 0.0;
		private string strCreatedDate = string.Empty;
		private int intCreatedUserID = 0;
		private int intIsDeleted = 0;
		private int intLastModifideUserID = 0;
		private string strLastModifideDate = string.Empty;

		#endregion
		
		 #region ---Properties---
		public int JournalTID
		{
			get { return intJournalTID;}
			set { intJournalTID = value;}
		}
		public int JournalID
		{
			get { return intJournalID;}
			set { intJournalID = value;}
		}
		public int OppositeJournalID
		{
			get { return intOppositeJournalID;}
			set { intOppositeJournalID = value;}
		}
		public double Amount
		{
			get { return dbAmount;}
			set { dbAmount = value;}
		}
		public string CreatedDate
		{
			get { return strCreatedDate;}
			set { strCreatedDate = value;}
		}
		public int CreatedUserID
		{
			get { return intCreatedUserID;}
			set { intCreatedUserID = value;}
		}
		public int IsDeleted
		{
			get { return intIsDeleted;}
			set { intIsDeleted = value;}
		}
		public int LastModifideUserID
		{
			get { return intLastModifideUserID;}
			set { intLastModifideUserID = value;}
		}
		public string LastModifideDate
		{
			get { return strLastModifideDate;}
			set { strLastModifideDate = value;}
		}

		#endregion
	}
}
