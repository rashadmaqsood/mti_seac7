using System;
using System.Collections.Generic;

//namespace SmartEyeControl_7.comm
namespace Rights
{
    public class User
    {
        public  UserTypeID userTypeID;
        public int userID;
        public bool isActive = false;
        public string userName;
        public string userPassword;
        public string fatherName;
        public string address;
        public string phone_1;
        public string phone_2;
        public string mobile_no;
        public string fax_no;
        public string nid_no;
        public string employee_code;
        public DateTime creation_date;

        private bool isCurrentAccessRightsChanged = false;
        public bool IsCurrentAccessRightsChanged
        {
            get { return isCurrentAccessRightsChanged; }
        }

        #region Rights
        
        /// <summary>
        /// RightsID_T421 Value For DB_Related Fields
        /// </summary>
        //public int RightsID_T421;
        //public int RightsID_R421;
        //public int RightsID_R411;
        //public int RightsID_ACT34G;
        //public int RightsID_R283;

        //v4.8.15
        /// <summary>
        /// Dictionary<MeterTypeInfoId, AccessRrightsId>        where MeterInfoId = Meter Model Id
        /// </summary>
        public Dictionary<int, int> RightsID_Model = new Dictionary<int, int>();

        #endregion

        #region ApplicationRight
        //v4.8.15
        /// <summary>
        /// List<ApplicationRight>     All Model rights in List
        /// </summary>
        public List<ApplicationRight> AccessRights_Model = new List<ApplicationRight>();

        /*
        public ApplicationRight AccessRights_T421
        {
            get
            {
                return rights.GetApplicationRightById(RightsID_T421);
            }
            set
            {
                //Apply Validation Rules here
                rights.SetApplicationRightById(RightsID_T421, value);
            }
        }
        public ApplicationRight AccessRights_R421
        {
            get
            {
                return rights.GetApplicationRightById(RightsID_R421);
            }
            set
            {
                //Apply Validation Rules here
                rights.SetApplicationRightById(RightsID_R421, value);
            }
        }
        public ApplicationRight AccessRights_R411
        {
            get
            {
                return rights.GetApplicationRightById(RightsID_R411);
            }
            set
            {
                //Apply Validation Rules here
                rights.SetApplicationRightById(RightsID_R411, value);
            }
        }
        public ApplicationRight AccessRights_Act34G
        {
            get
            {
                return rights.GetApplicationRightById(RightsID_ACT34G);
            }
            set
            {
                //Apply Validation Rules here
                rights.SetApplicationRightById(RightsID_ACT34G, value);
            }
        }
        public ApplicationRight AccessRights_R283
        {
            get
            {
                return rights.GetApplicationRightById(RightsID_R283);
            }
            set
            {
                //Apply Validation Rules here
                rights.SetApplicationRightById(RightsID_R283, value);
            }
        }
       */
        
        public ApplicationRight CurrentAccessRights { get; set; }
        
        #endregion

        public void SetCurrentAccessRights(string MeterModel, int meterInfoId)
        {
            try
            {
            int rightsId = RightsID_Model[meterInfoId];

            foreach (var currentModelRights in AccessRights_Model)
            {
                if (currentModelRights.RightsId == rightsId)
                {
                    CurrentAccessRights = currentModelRights;
                    isCurrentAccessRightsChanged = true;
                    break;
                }
            }

            /*  //commented in v4.8.15

            ApplicationRight Prev_Obj = CurrentAccessRights;

                if (MeterModel != null && MeterModel.Contains("34G"))
                {
                    CurrentAccessRights = AccessRights_Act34G;
                }
                else if (MeterModel != null && MeterModel.StartsWith("r326", StringComparison.OrdinalIgnoreCase))
                {
                    CurrentAccessRights = AccessRights_Act34G;
                }
                else if (MeterModel != null && MeterModel.StartsWith("t421", StringComparison.OrdinalIgnoreCase))
                {
                    CurrentAccessRights = AccessRights_T421;
                }
                else if (MeterModel != null && MeterModel.StartsWith("r421", StringComparison.OrdinalIgnoreCase))
                {
                    CurrentAccessRights = AccessRights_R421;
                }
                else if (MeterModel != null && MeterModel.StartsWith("r411", StringComparison.OrdinalIgnoreCase))
                {
                    CurrentAccessRights = AccessRights_R411;
                }
                else if (MeterModel != null && MeterModel.StartsWith("r283", StringComparison.OrdinalIgnoreCase))
                {
                    CurrentAccessRights = AccessRights_R283;
                }
                */
            }
            catch { }
            finally 
            {
                //Commented by Azeem Inayat
                //if (Prev_Obj != CurrentAccessRights)
                    isCurrentAccessRightsChanged = true;
            }
        }

        public UserRights rights;

        public User()
        {
            this.rights = new UserRights();
        }

        public static User GetDefaultUser()
        {
            User Default_User = new User();

            Default_User.userTypeID = UserTypeID.Admin;
            Default_User.userID = int.MaxValue;
            Default_User.isActive = false;
            Default_User.userName = "Admin";
            Default_User.userPassword = "default";
            Default_User.fatherName = String.Empty;
            Default_User.address = "Lahore, Pakistan";
            Default_User.phone_1 = "+92-42-00000000";
            Default_User.phone_2 = "+92-42-00000000";
            Default_User.mobile_no = "";
            Default_User.fax_no = "";
            Default_User.nid_no = "00000-0000000-0";
            Default_User.employee_code = String.Empty;
            Default_User.creation_date = DateTime.Now;

            //v4.8.15
            Default_User.AccessRights_Model = new List<ApplicationRight>();
            Default_User.RightsID_Model = new Dictionary<int, int>();

            //Default_User.RightsID_T421 = 0;
            //Default_User.RightsID_R421 = 0;
            //Default_User.RightsID_R411 = 0;
            //Default_User.RightsID_ACT34G = 0;
            //Default_User.RightsID_R283 = 0;

            return Default_User;
        }
    }

    public enum UserTypeID
    {
        SuperAdmin=1,
        Admin,
        Inspector,
        Reader,
        Custom
    }
}
