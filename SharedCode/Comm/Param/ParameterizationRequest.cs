using System;

namespace SharedCode.Comm.Param
{
    public class ParameterizationRequest
    {
        public long ParamRequestID = 0;
        public Int64 SET_Time = 0;
        public Int64 SET_IPProfiles = 0;
        public Int64 SET_ModemInitializeBasic = 0;
        public Int64 SET_ModemInitializeExtended = 0;
        public Int64 SET_KeepAlive = 0;

        public bool IsEmpty
        {
            get
            {
                if (ParamRequestID == 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
