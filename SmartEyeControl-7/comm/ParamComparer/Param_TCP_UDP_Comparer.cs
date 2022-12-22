using System;
using System.Collections.Generic;

namespace comm
{
    public class Param_TCP_UDP_Comparer : EqualityComparer<Param_TCP_UDP>
    {
        public override bool Equals(Param_TCP_UDP Obj_A, Param_TCP_UDP Obj_B)
        {
            bool isMatch = false;
            try
            {
                if (Obj_A == null || Obj_B == null)
                    throw new ArgumentNullException("Param_TCP_UDP Param_ObjA");

                //Compare IP_Port
                isMatch = String.Equals(Obj_A.IP_Port, Obj_B.IP_Port);
                if (!isMatch)
                    return isMatch;
                //Compare Max_Segmentation_Size
                isMatch = String.Equals(Obj_A.Max_Segmentation_Size, Obj_B.Max_Segmentation_Size);
                if (!isMatch)
                    return isMatch;

                //Compare Inactivity_Time_Out
                isMatch = String.Equals(Obj_A.Inactivity_Time_Out, Obj_B.Inactivity_Time_Out);
                if (!isMatch)
                    return isMatch;

                //Compare Max_no_of_simulaneous_connections
                isMatch = Obj_A.Max_no_of_simulaneous_connections == Obj_B.Max_no_of_simulaneous_connections;
                if (!isMatch)
                    return isMatch;

                //Compare IP_reference
                isMatch = Obj_A.IP_reference == Obj_B.IP_reference;
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

        public override int GetHashCode(Param_TCP_UDP obj)
        {
            return obj.GetHashCode();
        }
    }
}
