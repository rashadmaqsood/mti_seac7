using SharedCode.Comm.DataContainer;
using SharedCode.Comm.Param;
using System;
using System.Collections.Generic;

namespace comm
{
    public class Param_WakeUp_Profile_Comparer : EqualityComparer<Param_WakeUp_Profile[]>
    {
        public override bool Equals(Param_WakeUp_Profile[] x, Param_WakeUp_Profile[] y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || x.Length < Param_WakeUp_ProfileHelper.Max_WakeUp_Profile ||
                    y == null || y.Length < Param_IP_ProfilesHelper.Max_IP_Profile)
                    throw new ArgumentNullException("Param_WakeUp_Profile Param_ObjA");

                Param_WakeUp_Profile Obj_A = null, Obj_B = null;
                for (int Index = 0; Index < Param_WakeUp_ProfileHelper.Max_WakeUp_Profile; Index++)
                {
                    Obj_A = x[Index];
                    Obj_B = y[Index];

                    if (Obj_A == null || Obj_B == null)
                        throw new ArgumentNullException("Param_WakeUp_Profile Param_ObjA");

                    //Compare Wake_Up_Profile_ID
                    isMatch = Obj_A.Wake_Up_Profile_ID == Obj_B.Wake_Up_Profile_ID;
                    if (!isMatch)
                        break;
                    //Compare IP_Profile_ID_1
                    isMatch = Obj_A.IP_Profile_ID_1 == Obj_B.IP_Profile_ID_1;
                    if (!isMatch)
                        break;
                    //Compare IP_Profile_ID_2
                    isMatch = Obj_A.IP_Profile_ID_2 == Obj_B.IP_Profile_ID_2;
                    if (!isMatch)
                        break;
                    //Compare IP_Profile_ID_3
                    isMatch = Obj_A.IP_Profile_ID_3 == Obj_B.IP_Profile_ID_3;
                    if (!isMatch)
                        break;
                    //Compare IP_Profile_ID_4
                    isMatch = Obj_A.IP_Profile_ID_4 == Obj_B.IP_Profile_ID_4;
                    if (!isMatch)
                        break;
                    //Compare FLAG_WAKEUP_PROFILE_1
                    isMatch = Obj_A.FLAG_WAKEUP_PROFILE_1 == Obj_B.FLAG_WAKEUP_PROFILE_1;
                    if (!isMatch)
                        break;
                    //Compare FLAG_WAKEUP_PROFILE_2
                    isMatch = Obj_A.FLAG_WAKEUP_PROFILE_2 == Obj_B.FLAG_WAKEUP_PROFILE_2;
                    if (!isMatch)
                        break;
                    //Compare FLAG_WAKEUP_PROFILE_3
                    isMatch = Obj_A.FLAG_WAKEUP_PROFILE_3 == Obj_B.FLAG_WAKEUP_PROFILE_3;
                    if (!isMatch)
                        break;
                    //Compare FLAG_WAKEUP_PROFILE_4
                    isMatch = Obj_A.FLAG_WAKEUP_PROFILE_4 == Obj_B.FLAG_WAKEUP_PROFILE_4;
                    if (!isMatch)
                        break;
                    if (!isMatch)
                        break;
                }
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

        public override int GetHashCode(Param_WakeUp_Profile[] obj)
        {
            return obj.GetHashCode();
        }
    }
}
