using SharedCode.Comm.DataContainer;
using SharedCode.Comm.Param;
using System;
using System.Collections.Generic;

namespace comm
{
    public class Param_Contactor_Comparer : EqualityComparer<Param_Contactor>
    {
        public override bool Equals(Param_Contactor x, Param_Contactor y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || y == null)
                    throw new ArgumentNullException("Param_ContactorExt Param_ObjA");

                //Compare Contactor_ON_Pulse_Time
                isMatch = x.Contactor_ON_Pulse_Time == y.Contactor_ON_Pulse_Time;
                if (!isMatch)
                    return isMatch;
                //Compare Contactor_OFF_Pulse_Time
                isMatch = x.Contactor_OFF_Pulse_Time == y.Contactor_OFF_Pulse_Time;
                if (!isMatch)
                    return isMatch;
                //Compare Minimum_Interval_Bw_Contactor_State_Change
                isMatch = x.Minimum_Interval_Bw_Contactor_State_Change == y.Minimum_Interval_Bw_Contactor_State_Change;
                if (!isMatch)
                    return isMatch;
                //Compare Interval_Between_Retries
                isMatch = x.Interval_Between_Retries == y.Interval_Between_Retries;
                if (!isMatch)
                    return isMatch;
                //Compare RetryCount
                isMatch = x.RetryCount == y.RetryCount;
                if (!isMatch)
                    return isMatch;
                //Compare Control_Mode
                isMatch = x.Control_Mode == y.Control_Mode;
                if (!isMatch)
                    return isMatch;
                if (typeof(Param_ContactorExt) == x.GetType() && typeof(Param_ContactorExt) == y.GetType())
                {
                    //Compare Interval_Contactor_Failure_Status
                    isMatch = ((Param_ContactorExt)x).Interval_Contactor_Failure_Status == ((Param_ContactorExt)y).Interval_Contactor_Failure_Status;
                }
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

        public override int GetHashCode(Param_Contactor obj)
        {
            return obj.GetHashCode();
        }
    }
}

