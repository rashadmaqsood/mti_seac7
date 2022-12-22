using System;
using System.Collections.Generic;
using AccurateOptocomSoftware.comm;

namespace comm
{
    public class Param_Limits_EqualityComparer : EqualityComparer<Param_Limits>
    {
        public override bool Equals(Param_Limits Param_ObjA, Param_Limits Param_ObjB)
        {
            bool isEqual = false;
            try
            {
                if (Param_ObjA.Values == null || Param_ObjB.Values == null)
                    throw new ArgumentNullException("Param_Limits Obj_a");
                else if (Param_ObjA == Param_ObjB)
                {
                    isEqual = true;
                    return isEqual;
                }
                var Param_Limit_Vals = Enum.GetValues(typeof(ThreshouldItem));
                //Compare Every MonitorTime Value
                foreach (byte item in Param_Limit_Vals)
                {
                    var Mntr_a = Param_ObjA.Values[(int)item];
                    var Mntr_b = Param_ObjB.Values[(int)item];
                    //Compare MonitorTime Value
                    isEqual = Mntr_a.Equals(Mntr_b);
                    if (isEqual)
                        continue;
                    else
                        break;
                }
                return isEqual;
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

        public override int GetHashCode(Param_Limits b1)
        {
            //TODO:Change Later
            return b1.GetHashCode();
        }
    }
}
