using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AccurateOptocomSoftware.comm
{
    [Serializable]
    public enum ThreshouldItem : byte
    {
        OverVolt = 0,
        UnderVolt,

        ImbalanceVolt,
        HighNeutralCurrent,

        ReverseEnergy,
        TamperEnergy,

        CTFail,
        PTFail_Amp,
        PTFail_Volt,

        OverCurrentByPhase_T1,
        OverCurrentByPhase_T2,
        OverCurrentByPhase_T3,
        OverCurrentByPhase_T4,

        OverLoadByPhase_T1,
        OverLoadByPhase_T2,
        OverLoadByPhase_T3,
        OverLoadByPhase_T4,

        OverLoadTotal_T1,
        OverLoadTotal_T2,
        OverLoadTotal_T3,
        OverLoadTotal_T4,

        MDIExceed_T1,
        MDIExceed_T2,
        MDIExceed_T3,
        MDIExceed_T4
    }

    [Serializable]
    public enum MonitoringTimeItem : byte
    {
        OverVolt = 0,
        UnderVolt,

        ImbalanceVolt,
        HighNeutralCurrent,

        ReverseEnergy,
        TamperEnergy,
        ReversePolarity,

        CTFail,
        PTFail,

        OverCurrent,
        OverLoad,

        PowerFail,
        PhaseFail,
        PhaseSequence,
      
        PowerUPDelay,
        PowerUpDelayEnergyRecording,
        PowerUpDelayEarth,

        Earth,
        HALLSensor
    }
}
