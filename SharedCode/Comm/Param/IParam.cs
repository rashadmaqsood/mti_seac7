using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedCode.Comm.Param
{
    public interface IParam
    { }

    [Serializable]
    public enum Params : ushort
    {
        /// <summary>
        /// Meter Parameters
        /// </summary>
        ParamMonitoringTime = 10,
        ParamLimits = 11,
        ParamTariffication = 12,
        ParamMDI = 13,
        ParamLoadProfileChannelInfo = 14,

        ParamLoadProfilePeriod = 15,

        ParamDisplayWindowsNormal = 16,
        ParamDisplayWindowsAlternate = 17,
        ParamDisplayWindowsTestMode = 18,
        ParamCTPTRatio = 19,
        ParamDecimalPoint = 20,
        ParamCustomerReferenceCode = 21,
        ParamPassword = 22,
        ParamEnergy = 23,
        ParamClockCalib = 24,
        ParamClock = 25,
        ParamContactor = 26,
        /// <summary>
        /// ParamTimeBaseEvents,define set of All Parameters for TimeBase Events
        /// Includes ParamTimeBaseEvent1,ParamTimeBaseEvent2,
        /// </summary>
        ParamTimeBaseEvent = 27,
        ParamTBPowerFail = 28,
        /// <summary>
        /// ParamMajorAlarmProfile defines Set Of Alarms Configured as Major Alarm
        /// </summary>
        ParamMajorAlarmProfile = 29,
        /// <summary>
        /// ParamEventsCaution defines cautions values for Set Of Alarms
        /// </summary>
        ParamEventsCaution = 30,
        /// <summary>
        /// ParamModem,define set of all Parameters for Modem Configuration
        /// Includes ParamModem,ParamIPProfiles,ParamWakeUpProfile,ParamNumberProfile,ParamCommunicationProfile
        /// ParamKeepAliveIP,ParamModemLimitsAndTime,ParamModemInitialize,ParamModemBasicsNEW,ParamTCPUDP
        /// </summary>
        ParamIPProfiles = 40,
        ParamWakeUpProfile = 41,
        ParamNumberProfile = 42,
        ParamCommunicationProfile = 43,
        ParamKeepAliveIP = 44,
        ParamModemLimitsAndTime = 45,
        ParamModemInitialize = 46,
        ParamModemBasicsNEW = 47,
        ParamTCPUDP = 48,
        ParamDisplayWindowPowerDown = 49,
        ParamGeneralProcess = 50,
        ParamLoadProfileChannelInfo_2 = 51,
        ParamLoadProfilePeriod_2 = 52,
        ParamPQLoadProfilePeriod = 53,
        ParamStatausWordMap = 54,
        ParamStandardIPProfile = 55,
        ParamStandardNumberProfile = 56,
        //ParamStandardKeepAlive = 57
        ParamLoadShedding = 57,
        ParamEnergyMizer = 58,
        //TODO: SaveToFile 00 Add in Enum
        ParamGeneratorStart = 59,
        
    }

    [Serializable]
    public enum ParamsCategory : byte
    {
        /// <summary>
        /// Meter Parameters,define set of all Parameters for Meter Configurations excluding ParamModem
        /// Includes ParamMonitoringTime,ParamLimits,ParamTariffication,ParamMDI,ParamLoadProfileChannelInfo,ParamLoadProfilePeriod,
        ///ParamDisplayWindowsNormal,ParamDisplayWindowsAlternate,ParamDisplayWindowsTestMode,ParamCTPTRatio,ParamDecimalPoint,ParamCustomerReferenceCode,
        ///ParamPassword,ParamEnergy,ParamClockCalib,ParamClock,ParamContactor
        /// </summary>
        ParamMeter = 10,
        ParamThresholdMonitoring = 11,
        ParamRTC = 12,
        /// <summary>
        /// ParamMeterScheduling Category,defines set of All Parameters for TimeBase Events
        /// Includes ParamTimeBaseEvent1,ParamTimeBaseEvent2,
        /// </summary>
        ParamMeterScheduling = 13,
        ParamMajorAlarm = 14,
        ParamSecurity = 15,
        ParamEnergyCalc = 16,
        ParamDisplayConfig = 17,
        ParamLoadProfile = 18,
        ParamContactor = 19,
        ParamMisc = 20,
        /// <summary>
        /// ParamModem,define set of all Parameters for Modem Configuration
        /// Includes ParamModem,ParamIPProfiles,ParamWakeUpProfile,ParamNumberProfile,ParamCommunicationProfile
        /// ParamKeepAliveIP,ParamModemLimitsAndTime,ParamModemInitialize,ParamModemBasicsNEW,ParamTCPUDP
        /// </summary>
        ParamModem = 22,
        ParamStandardModem = 23,
        
        //TODO: SaveToFile 00 create category (if needed)
        ParamEnergyMizer = 24
    }
}
