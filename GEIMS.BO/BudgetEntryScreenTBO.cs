using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class BudgetEntryScreenTBO
    {
        #region Budget Entry  Screen Transaction Properties

        public const string BudgetEntryScreen_TABLE = "tbl_BudgetEntryScreen_T";
        public const string BudgetScreenTId_BUDGETSCREENTID = "BudgetScreenTId";
        public const string BudgetScreenId_BUDGETSCREENID = "BudgetScreenId";
        public const string BudgetBudgetSectionMID_SECTIONMID = "SectionMID";
        public const string BudgetBudgetSectionAmount_BUDGETSECTIONAMOUNT = "BudgetSectionAmount";
        public const string BudgetHeading_ISDELETED = "IsDeleted";


        private int intBudgetScreenTId = 0;
        private int intBudgetScreenId = 0;
        private int intSectionMID = 0;
        private decimal decBudgetSectionAmount = 0;
        private int intIsDeleted = 0;


        #endregion

        #region  ---Properties---
        public int BudgetScreenTId
        {
            get { return intBudgetScreenTId; }
            set { intBudgetScreenTId = value; }
        }
        public int BudgetScreenId
        {
            get { return intBudgetScreenId; }
            set { intBudgetScreenId = value; }
        }
        public int SectionMID
        {
            get { return intSectionMID; }
            set { intSectionMID = value; }
        }
        public decimal BudgetSectionAmount
        {
            get { return decBudgetSectionAmount; }
            set { decBudgetSectionAmount = value; }
        }
        public int IsDeleted
        {
            get { return intIsDeleted; }
            set { intIsDeleted = value; }
        }

        #endregion
    }
}
