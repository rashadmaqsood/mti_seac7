using System.Collections.Generic;

namespace Rights
{
    public class UserRights
    {
        public uint rightsID;

        //public bool tcp_access;
        //public bool hdlc_access;
        //public bool billingData;
        //public bool instantaneousData;
        //public bool loadProfileData;
        //public bool eventsData;
        //public bool parameters;

        public UserRights()
        {
            ApplicationRights = new List<ApplicationRight>();
        }

        internal List<ApplicationRight> ApplicationRights { get; set; }

        public ApplicationRight GetApplicationRightById(int RightId)
        {
            ApplicationRight App_Right = null;
            try
            {
                App_Right = ApplicationRights.Find((x) => (x != null && x.RightsId == RightId));
            }
            catch { }
            return App_Right;
        }

        public void SetApplicationRightById(int RightId, ApplicationRight Right)
        {
            ApplicationRight App_Right = null;
            try
            {
                App_Right = ApplicationRights.Find((x) => (x != null && x.RightsId == RightId));
                if (App_Right != null)
                    ApplicationRights.Remove(App_Right);
                ApplicationRights.Add(Right);
            }
            catch { }
        }

    }
}
