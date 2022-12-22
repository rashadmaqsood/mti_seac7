using DLMS;
using SharedCode.Comm.Param;
using SharedCode.TCP_Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedCode.Others
{
    interface IParamHelper
    {
        InitHDLCParam GetDefaultHDLCParams();

        InitTCPParams GetDefaultTCPParams();
        InitCommuincationParams GetDefaultCommunicationParams();
        SMS_Params GetDefaultWakeUpSmsParams();

        void SaveWakeupSmsParams(SMS_Params WakeUpSmsParams);

        SMS_Params LoadWakeupSmsParams();
        void SaveTCPParams(InitTCPParams InitTCPParams);

        InitTCPParams LoadTCPParams();

        void SaveCommuincationParams(InitCommuincationParams InitCommParams);

        InitCommuincationParams LoadCommunicationParams();

        void SaveHDLCParams(InitHDLCParam hdlcParams);
        InitHDLCParam LoadHDLCParams();

        bool SaveSAPAccessRights(SAP_Object meterLogicalDevice, SAP_Object clientSAP, List<OBISCodeRights> accessRights);

        List<OBISCodeRights> LoadSAPAccessRights(SAP_Object meterLogicalDevice, SAP_Object clientSAP);
    }
}
