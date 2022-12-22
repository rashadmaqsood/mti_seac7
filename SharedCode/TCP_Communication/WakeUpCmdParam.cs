using System;

namespace SharedCode.TCP_Communication
{
    public class WakeUpCmdParam
    {
        private SMS_Params wakeUpSMSParam;
        private String mobNumber;
        private WakeUpCommandType _WakeUpCmdType;
        private String modemPort;
        private int modembaudRate;

        
        
        public WakeUpCommandType WakeUpCmdType
        {
            get { return _WakeUpCmdType; }
            set { _WakeUpCmdType = value; }
        }
        
        public String ModemInitPort
        {
            get { return modemPort; }
            set { modemPort = value; }
        }

        public SMS_Params WakeUpSMSParam
        {
            get { return wakeUpSMSParam; }
            set { wakeUpSMSParam = value; }
        }

        public String RecvMobileNumber
        {
            get { return mobNumber; }
            set { mobNumber = value; }
        }
        
        public int ModembaudRate
        {
            get { return modembaudRate; }
            set { modembaudRate = value; }
        }

    }

    public enum WakeUpCommandType : byte 
    { 
        SMSParamWakeUp,
        VoiceCallWakeUp
    }
}
