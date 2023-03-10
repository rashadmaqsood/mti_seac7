using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using comm;
using SmartEyeControl_7.comm;

namespace AccurateOptocomSoftware.comm.Param
{
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
        ParamStatausWordMap = 54
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
        ParamModem = 22
    }

    public static class ConfigFileHelper
    {
        #region ParamTypeDefinition

        ////Params Enumeration Mapping On Type Object for Params
        internal readonly static Type[] ParamMonitoringTimeInfo = new Type[] { typeof(Param_Monitoring_time) };
        internal readonly static Type[] ParamLimitsInfo = new Type[] { typeof(Param_Limits) };///, typeof(Param_Limit_Demand_OverLoad) };
        internal readonly static Type[] ParamTarifficationInfo = new Type[] { typeof(Param_ActivityCalendar) };
        internal readonly static Type[] ParamMDIInfo = new Type[] { typeof(Param_MDI_parameters) };
        internal readonly static Type[] ParamLoadProfileChannelInfo = new Type[] { typeof(LoadProfileChannelInfo) };
        internal readonly static Type[] ParamLoadProfilePeriod = new Type[] { typeof(TimeSpan) };
        internal readonly static Type[] ParamLoadProfileChannelInfo_2 = new Type[] { typeof(LoadProfileChannelInfo) };
        internal readonly static Type[] ParamLoadProfilePeriod_2 = new Type[] { typeof(TimeSpan) };
        internal readonly static Type[] ParamDisplayWindowsInfo = new Type[] { typeof(DisplayWindows) };
        internal readonly static Type[] ParamCTPTRatioInfo = new Type[] { typeof(Param_CTPT_ratio) };
        internal readonly static Type[] ParamDecimalPointInfo = new Type[] { typeof(Param_decimal_point) };
        internal readonly static Type[] ParamCustomerReferenceCodeInfo = new Type[] { typeof(Param_Customer_Code) };
        internal readonly static Type[] ParamPasswordInfo = new Type[] { typeof(Param_password) };
        internal readonly static Type[] ParamEnergyInfo = new Type[] { typeof(Param_Energy_Parameter) };
        internal readonly static Type[] ParamClockCalibInfo = new Type[] { typeof(Param_clock_caliberation) };
        internal readonly static Type[] ParamContactorInfo = new Type[] { typeof(Param_ContactorExt) };
        internal readonly static Type[] ParamTimeBaseEventInfo = new Type[] { typeof(Param_TimeBaseEvents) };
        internal readonly static Type[] ParamTBPowerFailInfo = new Type[] { typeof(TBE_PowerFail) };
        internal readonly static Type[] ParamDisplayPowerDownInfo = new Type[] { typeof(Param_Display_PowerDown) };
        internal readonly static Type[] ParamGeneralProcessInfo = new Type[] { typeof(Param_Generel_Process)};

        /// <summary>
        /// Modem Parameters
        /// </summary>
        internal readonly static Type[] ParamIPProfilesInfo = new Type[] { typeof(Param_IP_Profiles) };
        internal readonly static Type[] ParamTCPUDPInfo = new Type[] { typeof(Param_TCP_UDP) };
        internal readonly static Type[] ParamNumberProfileInfo = new Type[] { typeof(Param_Number_Profile) };
        internal readonly static Type[] ParamWakeUpProfileInfo = new Type[] { typeof(Param_WakeUp_Profile) };
        internal readonly static Type[] ParamCommunicationProfileInfo = new Type[] { typeof(Param_Communication_Profile) };
        internal readonly static Type[] ParamKeepAliveIPInfo = new Type[] { typeof(Param_Keep_Alive_IP) };
        internal readonly static Type[] ParamModemLimitsAndTimeInfo = new Type[] { typeof(Param_ModemLimitsAndTime) };
        internal readonly static Type[] ParamModemInitializeInfo = new Type[] { typeof(Param_Modem_Initialize) };
        internal readonly static Type[] ParamModemBasicsNEWInfo = new Type[] { typeof(Param_ModemBasics_NEW) };
        internal readonly static Type[] ParamStatusWordMap = new Type[] { typeof(Param_StatusWordMap) };
        /// <summary>
        /// Major Alarm Configurations for event
        /// </summary>
        internal readonly static Type[] ParamMajorAlarmProfileInfo = new Type[] { typeof(Param_MajorAlarmProfile) };
        internal readonly static Type[] ParamEventsCautionInfo = new Type[] { typeof(Param_EventsCaution) };
        internal readonly static int maxParamVal = 55;
        internal readonly static Type[][] ParamTypeMap = new Type[maxParamVal][];

        #endregion

        #region ParamCategory_Definition

        internal const int maxCategoryVal = 54;
        internal readonly static Params[][] ParamCategoryMap = new Params[maxCategoryVal][];

        #endregion

        static ConfigFileHelper()
        {
            ///Init ConfigFileHelper Class
            #region MeterParameters

            ParamTypeMap[(int)Params.ParamMonitoringTime] = ParamMonitoringTimeInfo;
            ParamTypeMap[(int)Params.ParamLimits] = ParamLimitsInfo;
            ParamTypeMap[(int)Params.ParamTariffication] = ParamTarifficationInfo;
            ParamTypeMap[(int)Params.ParamMDI] = ParamMDIInfo;
            ParamTypeMap[(int)Params.ParamLoadProfileChannelInfo] = ParamLoadProfileChannelInfo;
            ParamTypeMap[(int)Params.ParamLoadProfilePeriod] = ParamLoadProfilePeriod;
            ParamTypeMap[(int)Params.ParamDisplayWindowsNormal] = ParamDisplayWindowsInfo;
            ParamTypeMap[(int)Params.ParamDisplayWindowsAlternate] = ParamDisplayWindowsInfo;
            ParamTypeMap[(int)Params.ParamDisplayWindowsTestMode] = ParamDisplayWindowsInfo;
            ParamTypeMap[(int)Params.ParamCTPTRatio] = ParamCTPTRatioInfo;
            ParamTypeMap[(int)Params.ParamDecimalPoint] = ParamDecimalPointInfo;
            ParamTypeMap[(int)Params.ParamCustomerReferenceCode] = ParamCustomerReferenceCodeInfo;

            ParamTypeMap[(int)Params.ParamPassword] = ParamPasswordInfo;
            ParamTypeMap[(int)Params.ParamEnergy] = ParamEnergyInfo;

            ParamTypeMap[(int)Params.ParamClockCalib] = ParamClockCalibInfo;
            ParamTypeMap[(int)Params.ParamClock] = new Type[] { typeof(DateTime) };
            ParamTypeMap[(int)Params.ParamContactor] = ParamContactorInfo;
            ParamTypeMap[(int)Params.ParamTimeBaseEvent] = ParamTimeBaseEventInfo;
            ParamTypeMap[(int)Params.ParamTBPowerFail] = ParamTBPowerFailInfo;
            ParamTypeMap[(int)Params.ParamDisplayWindowPowerDown] = ParamDisplayPowerDownInfo;
            ParamTypeMap[(int)Params.ParamGeneralProcess] = ParamGeneralProcessInfo;
            ParamTypeMap[(int)Params.ParamLoadProfileChannelInfo_2] = ParamLoadProfileChannelInfo_2;
            ParamTypeMap[(int)Params.ParamLoadProfilePeriod_2] = ParamLoadProfilePeriod_2;
            ParamTypeMap[(int)Params.ParamStatausWordMap] = ParamStatusWordMap;

            #endregion
            #region ModemParameters

            ParamTypeMap[(int)Params.ParamIPProfiles] = ParamIPProfilesInfo;
            ParamTypeMap[(int)Params.ParamTCPUDP] = ParamTCPUDPInfo;
            ParamTypeMap[(int)Params.ParamNumberProfile] = ParamNumberProfileInfo;
            ParamTypeMap[(int)Params.ParamWakeUpProfile] = ParamWakeUpProfileInfo;

            ParamTypeMap[(int)Params.ParamCommunicationProfile] = ParamCommunicationProfileInfo;
            ParamTypeMap[(int)Params.ParamKeepAliveIP] = ParamKeepAliveIPInfo;
            ParamTypeMap[(int)Params.ParamModemLimitsAndTime] = ParamModemLimitsAndTimeInfo;
            ParamTypeMap[(int)Params.ParamModemInitialize] = ParamModemInitializeInfo;
            ParamTypeMap[(int)Params.ParamModemBasicsNEW] = ParamModemBasicsNEWInfo;

            #endregion
            #region Param_Major_Alarm_Config

            ParamTypeMap[(int)Params.ParamMajorAlarmProfile] = ParamMajorAlarmProfileInfo;
            ParamTypeMap[(int)Params.ParamEventsCaution] = ParamEventsCautionInfo;

            #endregion
            #region ParamCategory_Init

            #region Param_Meter

            ParamCategoryMap[(int)ParamsCategory.ParamMeter] = new Params[]
            { 
              Params.ParamMonitoringTime,Params.ParamLimits,Params.ParamTariffication,Params.ParamMDI,                    
              Params.ParamLoadProfileChannelInfo,Params.ParamLoadProfilePeriod,Params.ParamDisplayWindowsNormal,
              Params.ParamDisplayWindowsAlternate,Params.ParamDisplayWindowsTestMode,Params.ParamCTPTRatio,
              Params.ParamCustomerReferenceCode,Params.ParamPassword,Params.ParamEnergy,
              Params.ParamClockCalib,Params.ParamClock,Params.ParamContactor,Params.ParamTimeBaseEvent,Params.ParamTBPowerFail,
              Params.ParamDecimalPoint,Params.ParamDisplayWindowPowerDown,Params.ParamGeneralProcess
            };

            ParamCategoryMap[(int)ParamsCategory.ParamThresholdMonitoring] = new Params[] { Params.ParamMonitoringTime, Params.ParamLimits };
            ParamCategoryMap[(int)ParamsCategory.ParamRTC] = new Params[] { Params.ParamClockCalib, Params.ParamClock };
            ParamCategoryMap[(int)ParamsCategory.ParamMajorAlarm] = new Params[] { Params.ParamMajorAlarmProfile, Params.ParamEventsCaution };

            ParamCategoryMap[(int)ParamsCategory.ParamSecurity] = new Params[] { Params.ParamPassword, Params.ParamCustomerReferenceCode };
            ParamCategoryMap[(int)ParamsCategory.ParamEnergyCalc] = new Params[] { Params.ParamTariffication, Params.ParamMDI, Params.ParamEnergy };
            ParamCategoryMap[(int)ParamsCategory.ParamDisplayConfig] = new Params[] {Params.ParamDisplayWindowsNormal, Params.ParamDisplayWindowsAlternate,
                                                                                     Params.ParamDisplayWindowsTestMode,Params.ParamDecimalPoint};

            ParamCategoryMap[(int)ParamsCategory.ParamLoadProfile] = new Params[] { Params.ParamLoadProfileChannelInfo,Params.ParamLoadProfileChannelInfo_2,
                                        Params.ParamLoadProfilePeriod, Params.ParamLoadProfilePeriod_2, Params.ParamPQLoadProfilePeriod };
            ParamCategoryMap[(int)ParamsCategory.ParamContactor] = new Params[] { Params.ParamContactor };

            ParamCategoryMap[(int)ParamsCategory.ParamMeterScheduling] = new Params[] { Params.ParamTimeBaseEvent, Params.ParamTBPowerFail };

            #endregion

            ParamCategoryMap[(int)ParamsCategory.ParamMisc] = new Params[]
            { 
              Params.ParamCTPTRatio
            };

            ParamCategoryMap[(int)ParamsCategory.ParamModem] = new Params[]
            { 
              Params.ParamIPProfiles,Params.ParamTCPUDP,Params.ParamNumberProfile,Params.ParamWakeUpProfile,        
              Params.ParamCommunicationProfile,Params.ParamKeepAliveIP,Params.ParamModemLimitsAndTime,   
              Params.ParamModemInitialize,Params.ParamModemBasicsNEW
            };

            #endregion
        }

        #region SelectByCategory

        public static List<List<IParam>> SelectByCategory(List<IParam> LoadedParameters, ParamsCategory Category)
        {
            List<List<IParam>> loadedParams = new List<List<IParam>>();
            List<IParam> loadedParam = null;
            try
            {
                Params[] tArray = GetParamsByCategory(Category);
                if (tArray == null || tArray.Length <= 0)
                    throw new Exception(String.Format("Invalid Parameter Category {0}", Category));
                ///foreach param in Category
                foreach (var param in tArray)
                {
                    loadedParam = SelectByParam(LoadedParameters, param);
                    if (loadedParam == null)
                        continue;
                    else
                        loadedParams.Add(loadedParam);
                }
                return loadedParams;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occured while selecting Parameter Category {0}", Category), ex);
            }
        }

        public static Params[] GetParamsByCategory(ParamsCategory Category)
        {
            Params[] Params = ParamCategoryMap[(int)Category];
            return Params;
        }

        #endregion

        #region SelectByParam

        public static List<IParam> SelectByParam(List<IParam> LoadedParameters, Params Parameters)
        {
            List<IParam> loadedParam = null;
            try
            {
                loadedParam = LoadedParameters.FindAll((IParam x) =>
                {
                    ///Select Type[] based on Parameters
                    Type[] ParamTypes = ParamTypeMap[(int)Parameters];
                    if (ParamTypes == null || ParamTypes.Length <= 0)
                        throw new ArgumentNullException("" + Parameters, String.Format("Invalid Params {0}", Parameters));
                    ///Parameter Type
                    foreach (Type IparamType in ParamTypes)
                    {
                        if (x != null && x.GetType() == IparamType)
                        {
                            return true;
                        }
                    }
                    return false;
                });
                return loadedParam;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occured while selecting Parameter {0}", Parameters), ex);
            }
        }

        public static List<List<IParam>> SelectByParam(List<IParam> LoadedParameters, List<Params> Parameters)
        {
            List<List<IParam>> Params = new List<List<IParam>>();
            try
            {
                Parameters.Sort();
                List<IParam> Param = null;
                foreach (Params parameter in Parameters)
                {
                    try
                    {
                        Param = SelectByParam(LoadedParameters, parameter);
                        Params.Add(Param);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(String.Format("Error occured while selecting Parameters,{0}",
                            ex.Message, Parameters), ex);
                    }
                }
                return Params;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occured while selecting Parameters,{0}", ex.Message, Parameters), ex);
            }
        }

        public static Type[] GetParamsTypes(Params Parameters)
        {
            Type[] Params = ParamTypeMap[(int)Parameters];
            return Params;
        }

        #endregion

        #region Define_HeaderInfo_ParamList

        public static HeaderInfo Init_HeaderInfo_ParamList(List<IParam> LoadedParameters, HeaderInfo HInfo)
        {
            try
            {
                List<Params> paramList = new List<Params>();
                if (LoadedParameters == null || LoadedParameters.Count <= 0)
                    throw new ArgumentNullException("LoadedParameters", "Invalid Argument supplied,LoadParameters");
                if (HInfo == null)
                {
                    HInfo = new HeaderInfo();
                    HInfo.ConfigName = "Default Configuration";
                    HInfo.Description = "Defines Set of parameter as default configuration set";
                }
                HInfo.ParamList = paramList;
                ushort[] ParamVals = (ushort[])Enum.GetValues(typeof(Params));
                ///Select For Each ParamValues
                foreach (var _ParamVal in ParamVals)
                {
                    Type[] ParamTypes = GetParamsTypes((Params)_ParamVal);
                    if (ParamTypes == null || ParamTypes.Length <= 0)
                        throw new Exception(String.Format("Invalid Params {0}", (Params)_ParamVal));
                    ///Parameter Type
                    List<IParam> loadedParam = SelectByParam(LoadedParameters, (Params)_ParamVal);
                    bool paramVal = false;
                    foreach (Type IparamType in ParamTypes)
                    {
                        paramVal = loadedParam.Exists((x) => { return x.GetType() == IparamType; });
                        if (!paramVal)
                            break;
                    }
                    ///All Types Matched 
                    if (paramVal)
                        paramList.Add((Params)_ParamVal);
                }
                return HInfo;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occured while Init_HeaderInfo_ParamList,{0}", ex.Message), ex);
            }
        }

        public static void Init_HeaderInfo_ParamCategoryList(HeaderInfo HInfo)
        {
            List<ParamsCategory> CategoryList = new List<ParamsCategory>();
            try
            {
                if (HInfo == null || HInfo.ParamList.Count <= 0)
                    throw new ArgumentNullException("HeaderInfo", "Invalid Argument supplied,ParamList cannot be empty");
                HInfo.ParamCategory = CategoryList;
                byte[] ParamCatVals = (byte[])Enum.GetValues(typeof(ParamsCategory));
                ///Select For Each ParamValues
                foreach (var _ParamVal in ParamCatVals)
                {
                    Params[] Params = GetParamsByCategory((ParamsCategory)_ParamVal);
                    if (Params == null || Params.Length <= 0)
                        throw new Exception(String.Format("Invalid Params {0}", (ParamsCategory)_ParamVal));
                    bool paramVal = false;
                    foreach (var IparamType in Params)
                    {
                        ///Partial Match For Param Category List
                        paramVal = HInfo.ParamList.Exists((x) => { return x == IparamType; });
                        if (paramVal)
                            break;
                        else
                            continue;
                    }
                    ///All Types Matched 
                    if (paramVal)
                        CategoryList.Add((ParamsCategory)_ParamVal);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occured while Init_HeaderInfo_ParamList,{0}", ex.Message), ex);
            }
        }

        #endregion

        #region IsCompleteCategory_Defined

        public static bool IsCompleteCategoryDefined(HeaderInfo HInfo, ParamsCategory Category)
        {
            try
            {
                ///Null Argument Exception
                if (HInfo == null || HInfo.ParamList.Count <= 0 || HInfo.ParamCategory.Count <= 0)
                    throw new ArgumentNullException("HeaderInfo", "Invalid Argument supplied,ParamList cannot be empty");
                if (Category == null)
                    throw new ArgumentNullException("ParamCategory", "Invalid Argument supplied,ParamsCategory cannot be null");
                Params[] Params = ParamCategoryMap[(int)Category];
                if (Params == null || Params.Length <= 0)
                    throw new Exception(String.Format("Invalid Params {0}", (ParamsCategory)Category));
                bool paramVal = false;
                foreach (var IparamType in Params)
                {
                    ///Partial Match For Param Category List
                    paramVal = HInfo.ParamList.Exists((x) => { return x == IparamType; });
                    if (paramVal)
                        continue;
                    else
                        break;
                }
                ///All Types Matched 
                return paramVal;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occured while IsCompleteCategoryDefined,{0} {1}", Category, ex.Message), ex);
            }
        }

        #region IsCompleteCategoryDefined

        public static bool IsCompleteModemParamDefined(HeaderInfo HInfo)
        {
            return IsCompleteCategoryDefined(HInfo, ParamsCategory.ParamModem);
        }

        public static bool IsCompleteMeterScheduling(HeaderInfo HInfo)
        {
            return IsCompleteCategoryDefined(HInfo, ParamsCategory.ParamMeterScheduling);
        }

        public static bool IsCompleteMeterParam(HeaderInfo HInfo)
        {
            return IsCompleteCategoryDefined(HInfo, ParamsCategory.ParamMeter);
        }

        public static bool IsCompleteThresholdMonitoringParam(HeaderInfo HInfo)
        {
            return IsCompleteCategoryDefined(HInfo, ParamsCategory.ParamThresholdMonitoring);
        }

        public static bool IsCompleteMajorAlarmParam(HeaderInfo HInfo)
        {
            return IsCompleteCategoryDefined(HInfo, ParamsCategory.ParamMajorAlarm);
        }

        public static bool IsCompleteEnergyCalculationParam(HeaderInfo HInfo)
        {
            return IsCompleteCategoryDefined(HInfo, ParamsCategory.ParamEnergyCalc);
        }

        public static bool IsCompleteDisplayConfigurationParam(HeaderInfo HInfo)
        {
            return IsCompleteCategoryDefined(HInfo, ParamsCategory.ParamDisplayConfig);
        }

        #endregion

        #endregion
    }
}
