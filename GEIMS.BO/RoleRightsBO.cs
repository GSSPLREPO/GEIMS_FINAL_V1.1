using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class RoleRightsBO
    {
        #region RoleRights Class Properties

        public const string ROLERIGHTS_TABLE = "tbl_RoleRights_T";
        public const string ROLERIGHTS_ROLERIGHTSTID = "RoleRightsTID";
        public const string ROLERIGHTS_ROLEID = "RoleID";
        public const string ROLERIGHTS_TRUSTMID = "TrustMID";
        public const string ROLERIGHTS_SCHOOLMID = "SchoolMID";
        public const string ROLERIGHTS_SCREENID = "ScreenID";

        private int intRoleRightsTID = 0;
        private int intRoleID = 0;
        private int intSchoolMID = 0;
        private int intTrustMID = 0;
        private int intScreenID = 0;

        #endregion

        #region ---Properties---
        public int RoleRightsTID
        {
            get { return intRoleRightsTID; }
            set { intRoleRightsTID = value; }
        }
        public int RoleID
        {
            get { return intRoleID; }
            set { intRoleID = value; }
        }
        public int TrustMID
        {
            get { return intTrustMID; }
            set { intTrustMID = value; }
        }
        public int SchoolMID
        {
            get { return intSchoolMID; }
            set { intSchoolMID = value; }
        }
        public int ScreenID
        {
            get { return intScreenID; }
            set { intScreenID = value; }
        }

        #endregion
    }
}
