using SharedCode.Comm.DataContainer;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.Param;
using System;
using System.Collections.Generic;

namespace comm
{
    public class Param_Monitoring_time_EqualityComparer : EqualityComparer<Param_Monitoring_Time>
    {
        public override bool Equals(Param_Monitoring_Time Param_ObjA, Param_Monitoring_Time Param_ObjB)
        {
            bool isEqual = false;
            try
            {
                if (Param_ObjA.Values == null || Param_ObjB.Values == null)
                    throw new ArgumentNullException("Param_Monitoring_Time Obj_a");
                else if (Param_ObjA == Param_ObjB)
                {
                    isEqual = true;
                    return isEqual;
                }
                var Param_Montr_Vals = Enum.GetValues(typeof(MonitoringTimeItem));
                //Compare Every MonitorTime Value
                foreach (byte item in Param_Montr_Vals)
                {
                    var Mntr_a = Param_ObjA.Values[item];
                    var Mntr_b = Param_ObjB.Values[item];
                    //Compare MonitorTime Value
                    isEqual = Mntr_a.Equals(Mntr_b);
                    if (isEqual)
                        continue;
                    else
                        break;
                }
                if (isEqual)
                {
                    isEqual = Param_ObjA.MonitoringTime_FLAG == Param_ObjB.MonitoringTime_FLAG;
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

        public override int GetHashCode(Param_Monitoring_Time b1)
        {
            //TODO:Change Later
            return b1.GetHashCode();
        }
    }
}
