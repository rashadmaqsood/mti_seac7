using SharedCode.Comm.DataContainer;
using SharedCode.Comm.Param;
using System;
using System.Collections.Generic;

namespace comm
{
    public class Param_IP_Profiles_Comparer : EqualityComparer<Param_IP_Profiles[]>
    {
        public override bool Equals(Param_IP_Profiles[] x, Param_IP_Profiles[] y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || x.Length != Param_IP_ProfilesHelper.Max_IP_Profile ||
                    y == null || y.Length != Param_IP_ProfilesHelper.Max_IP_Profile)
                    throw new ArgumentNullException("Param_IP_Profiles Param_ObjA");

                Param_IP_Profiles Obj_A = null, Obj_B = null;
                for (int Index = 0; Index < Param_IP_ProfilesHelper.Max_IP_Profile; Index++)
                {
                    Obj_A = x[Index];
                    Obj_B = y[Index];

                    if (Obj_A == null || Obj_B == null)
                        throw new ArgumentNullException("Param_IP_Profiles Param_ObjA");

                    //Compare Unique_ID
                    isMatch = Obj_A.Unique_ID == Obj_B.Unique_ID;
                    if (!isMatch)
                        break;
                    //Compare IP
                    isMatch = Obj_A.IP == Obj_B.IP;
                    if (!isMatch)
                        break;
                    //Compare Wrapper_Over_TCP_port
                    isMatch = Obj_A.Wrapper_Over_TCP_port == Obj_B.Wrapper_Over_TCP_port;
                    if (!isMatch)
                        break;
                    //Compare Wrapper_Over_UDP_port
                    isMatch = Obj_A.Wrapper_Over_UDP_port == Obj_B.Wrapper_Over_UDP_port;
                    if (!isMatch)
                        break;
                    //Compare HDLC_Over_TCP_Port
                    isMatch = Obj_A.HDLC_Over_TCP_Port == Obj_B.HDLC_Over_TCP_Port;
                    if (!isMatch)
                        break;
                    //Compare HDLC_Over_UDP_Port
                    isMatch = Obj_A.HDLC_Over_UDP_Port == Obj_B.HDLC_Over_UDP_Port;
                    if (!isMatch)
                        break;
                    //Compare RawFlags
                    //isMatch = Obj_A.RawFlags == Obj_B.RawFlags;
                    //if (!isMatch)
                    //    break;

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

        public override int GetHashCode(Param_IP_Profiles[] obj)
        {
            return obj.GetHashCode();
        }
    }
}
