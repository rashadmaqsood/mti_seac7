using SharedCode.Comm.DataContainer;
using SharedCode.Comm.Param;
using System;
using System.Collections.Generic;

namespace comm
{
    public class Param_Keep_Alive_IP_Comparer : EqualityComparer<Param_Keep_Alive_IP>
    {
        public override bool Equals(Param_Keep_Alive_IP Obj_A, Param_Keep_Alive_IP Obj_B)
        {
            bool isMatch = false;
            try
            {
                if (Obj_A == null || Obj_B == null)
                    throw new ArgumentNullException("Param_Keep_Alive_IP Param_ObjA");

                //Compare Enabled
                isMatch = Obj_A.Enabled == Obj_B.Enabled;
                if (!isMatch)
                    return isMatch;
                //Compare IP_Profile_ID
                isMatch = Obj_A.IP_Profile_ID == Obj_B.IP_Profile_ID;
                if (!isMatch)
                    return isMatch;
                //Compare Ping_time
                isMatch = Obj_A.Ping_time == Obj_B.Ping_time;
                if (!isMatch)
                    return isMatch;
                return isMatch;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override int GetHashCode(Param_Keep_Alive_IP obj)
        {
            return obj.GetHashCode();
        }
    }
}
