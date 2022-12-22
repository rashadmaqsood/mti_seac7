using SharedCode.Comm.DataContainer;
using SharedCode.Comm.Param;
using System;
using System.Collections.Generic;

namespace comm
{
    public class Param_ModemLimitsAndTime_Comparer : EqualityComparer<Param_ModemLimitsAndTime>
    {
        public override bool Equals(Param_ModemLimitsAndTime Obj_A, Param_ModemLimitsAndTime Obj_B)
        {
            bool isMatch = false;
            try
            {
                if (Obj_A == null || Obj_B == null)
                    throw new ArgumentNullException("Param_Keep_Alive_IP Param_ObjA");

                //Compare RSSI_LEVEL_TCP_UDP_Connection
                isMatch = Obj_A.RSSI_LEVEL_TCP_UDP_Connection == Obj_B.RSSI_LEVEL_TCP_UDP_Connection;
                if (!isMatch)
                    return isMatch;
                //Compare RSSI_LEVEL_SMS
                isMatch = Obj_A.RSSI_LEVEL_SMS == Obj_B.RSSI_LEVEL_SMS;
                if (!isMatch)
                    return isMatch;
                //Compare RSSI_LEVEL_Data_Call
                isMatch = Obj_A.RSSI_LEVEL_Data_Call == Obj_B.RSSI_LEVEL_Data_Call;
                if (!isMatch)
                    return isMatch;
                //Compare Time_between_Retries_SMS
                isMatch = Obj_A.Time_between_Retries_SMS == Obj_B.Time_between_Retries_SMS;
                if (!isMatch)
                    return isMatch;
                //Compare Time_between_Retries_IP_connection
                isMatch = Obj_A.Time_between_Retries_IP_connection == Obj_B.Time_between_Retries_IP_connection;
                if (!isMatch)
                    return isMatch;
                //Compare Time_between_Retries_Data_Call
                isMatch = Obj_A.Time_between_Retries_Data_Call == Obj_B.Time_between_Retries_Data_Call;
                if (!isMatch)
                    return isMatch;

                //Compare Retry_SMS
                isMatch = Obj_A.Retry_SMS == Obj_B.Retry_SMS;
                if (!isMatch)
                    return isMatch;
                //Compare Retry
                isMatch = Obj_A.Retry == Obj_B.Retry;
                if (!isMatch)
                    return isMatch;
                //Compare Retry_IP_connection
                isMatch = Obj_A.Retry_IP_connection == Obj_B.Retry_IP_connection;
                if (!isMatch)
                    return isMatch;
                //Compare Retry_TCP
                isMatch = Obj_A.Retry_TCP == Obj_B.Retry_TCP;
                if (!isMatch)
                    return isMatch;
                //Compare Retry_UDP
                isMatch = Obj_A.Retry_UDP == Obj_B.Retry_UDP;
                if (!isMatch)
                    return isMatch;

                //Compare TimeRetriesAlwaysOnCycle
                isMatch = Obj_A.TimeRetriesAlwaysOnCycle == Obj_B.TimeRetriesAlwaysOnCycle;
                if (!isMatch)
                    return isMatch;
                //Compare TCP_Inactivity
                isMatch = Obj_A.TCP_Inactivity == Obj_B.TCP_Inactivity;
                if (!isMatch)
                    return isMatch;
                //Compare TimeOut_CipSend
                isMatch = Obj_A.TimeOut_CipSend == Obj_B.TimeOut_CipSend;
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

        public override int GetHashCode(Param_ModemLimitsAndTime obj)
        {
            return obj.GetHashCode();
        }
    }
}
