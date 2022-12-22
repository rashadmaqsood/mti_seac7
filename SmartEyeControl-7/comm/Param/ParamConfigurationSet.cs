using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using comm;
using SmartEyeControl_7.comm;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace AccurateOptocomSoftware.comm.Param
{
    [Serializable]
    public class ParamConfigurationSet:ICloneable
    {
        private ConcurrentDictionary<Params, List<IParam>> configurationSet = null;

        #region Properties

        public ConcurrentDictionary<Params, List<IParam>> ConfigurationSet
        {
            get { return configurationSet; }
            private set { configurationSet = value; }
        }

        #region Param_Properties


        public Param_Monitoring_time ParamMonitoringTime
        {
            get
            {
                Param_Monitoring_time Param_Montr_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamMonitoringTime);
                Param_Montr_Obj = (Param_Monitoring_time)t_Param[0];
                return Param_Montr_Obj;
            }
            set
            {
                UpdateByParam(Params.ParamMonitoringTime, value);
            }
        }

        public Param_Limits ParamLimits
        {
            get
            {
                Param_Limits Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamLimits);
                Param_Obj = (Param_Limits)t_Param[0];
                return Param_Obj;
            }
            set
            {
                UpdateByParam(Params.ParamLimits, value);
            }
        }

        public Param_ActivityCalendar ParamTariffication
        {
            get
            {
                Param_ActivityCalendar Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamTariffication);
                Param_Obj = (Param_ActivityCalendar)t_Param[0];
                return Param_Obj;
            }
            set
            {
                UpdateByParam(Params.ParamTariffication, value);
            }
        }

        public Param_MDI_parameters ParamMDI
        {
            get
            {
                Param_MDI_parameters Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamMDI);
                Param_Obj = (Param_MDI_parameters)t_Param[0];
                return Param_Obj;
            }
            set
            {
                UpdateByParam(Params.ParamMDI, value);
            }
        }

        public List<LoadProfileChannelInfo> ParamLoadProfileChannelInfo
        {
            get
            {
                List<LoadProfileChannelInfo> Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamLoadProfileChannelInfo);
                Param_Obj = t_Param.Cast<LoadProfileChannelInfo>().ToList();
                return Param_Obj;
            }
            set
            {
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamLoadProfileChannelInfo);
                if (value != null && value.Count > 0)
                {
                    t_Param.Clear();
                    t_Param.AddRange(value);
                }
                //List<IParam> t_Param = null;
                //t_Param = value.Cast<IParam>().ToList<IParam>();
                UpdateByParam(Params.ParamLoadProfileChannelInfo, t_Param);
            }
        }

        public List<LoadProfileChannelInfo> ParamLoadProfileChannelInfo_2
        {
            get
            {
                List<LoadProfileChannelInfo> Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamLoadProfileChannelInfo_2);
                Param_Obj = t_Param.Cast<LoadProfileChannelInfo>().ToList();
                return Param_Obj;
            }
            set
            {
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamLoadProfileChannelInfo_2);
                if (value != null && value.Count > 0)
                {
                    t_Param.Clear();
                    t_Param.AddRange(value);
                }
                //List<IParam> t_Param = null;
                //t_Param = value.Cast<IParam>().ToList<IParam>();
                UpdateByParam(Params.ParamLoadProfileChannelInfo_2, t_Param);
            }
        }

        public DisplayWindows ParamDisplayWindowsNormal
        {
            get
            {
                DisplayWindows Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamDisplayWindowsNormal);
                Param_Obj = (DisplayWindows)t_Param[0];
                return Param_Obj;
            }
            set
            {
                UpdateByParam(Params.ParamDisplayWindowsNormal, value);
            }
        }

        public DisplayWindows ParamDisplayWindowsAlternate
        {
            get
            {
                DisplayWindows Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamDisplayWindowsAlternate);
                Param_Obj = (DisplayWindows)t_Param[0];
                return Param_Obj;
            }
            set
            {
                UpdateByParam(Params.ParamDisplayWindowsAlternate, value);
            }
        }

        public DisplayWindows ParamDisplayWindowsTestMode
        {
            get
            {
                DisplayWindows Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamDisplayWindowsTestMode);
                Param_Obj = (DisplayWindows)t_Param[0];
                return Param_Obj;
            }
            set
            {
                UpdateByParam(Params.ParamDisplayWindowsTestMode, value);
            }
        }

        public Param_CTPT_ratio ParamCTPTRatio
        {
            get
            {
                Param_CTPT_ratio Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamCTPTRatio);
                Param_Obj = (Param_CTPT_ratio)t_Param[0];
                return Param_Obj;
            }
            set
            {
                UpdateByParam(Params.ParamCTPTRatio, value);
            }
        }

        public Param_decimal_point ParamDecimalPoint
        {
            get
            {
                Param_decimal_point Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamDecimalPoint);
                Param_Obj = (Param_decimal_point)t_Param[0];
                return Param_Obj;
            }
            set
            {
                UpdateByParam(Params.ParamDecimalPoint, value);
            }
        }
        public Param_StatusWordMap ParamStatusWordMap
        {
            get
            {
                Param_StatusWordMap Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamStatausWordMap);
                Param_Obj = (Param_StatusWordMap)t_Param[0];
                return Param_Obj;
            }
            set
            {
                UpdateByParam(Params.ParamStatausWordMap, value);
            }
        }

        public Param_Customer_Code ParamCustomerReferenceCode
        {
            get
            {
                Param_Customer_Code Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamCustomerReferenceCode);
                Param_Obj = (Param_Customer_Code)t_Param[0];
                return Param_Obj;
            }
            set
            {
                UpdateByParam(Params.ParamCustomerReferenceCode, value);
            }
        }

        public Param_password ParamPassword
        {
            get
            {
                Param_password Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamPassword);
                Param_Obj = (Param_password)t_Param[0];
                return Param_Obj;
            }
            set
            {
                UpdateByParam(Params.ParamPassword, value);
            }
        }

        public Param_Energy_Parameter ParamEnergy
        {
            get
            {
                Param_Energy_Parameter Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamEnergy);
                Param_Obj = (Param_Energy_Parameter)t_Param[0];
                return Param_Obj;
            }
            set
            {
                UpdateByParam(Params.ParamEnergy, value);
            }
        }

        public Param_clock_caliberation ParamClockCalib
        {
            get
            {
                Param_clock_caliberation Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamClockCalib);
                Param_Obj = (Param_clock_caliberation)t_Param[0];
                return Param_Obj;
            }
            set
            {
                UpdateByParam(Params.ParamClockCalib, value);
            }
        }

        public Param_Contactor ParamContactor
        {
            get
            {
                Param_Contactor Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamContactor);
                Param_Obj = (Param_Contactor)t_Param[0];
                return Param_Obj;
            }
            set
            {
                UpdateByParam(Params.ParamContactor, value);
            }
        }

        public Param_TimeBaseEvents ParamTimeBaseEvent_1
        {
            get
            {
                Param_TimeBaseEvents Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamTimeBaseEvent);
                Param_Obj = (Param_TimeBaseEvents)t_Param[0];
                return Param_Obj;
            }
            set
            {
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamTimeBaseEvent);
                if (value != null)
                {
                    if (t_Param.Count <= 0)
                        t_Param.Add(null);
                    t_Param[0] = value;
                }
                //List<IParam> t_Param = null;
                //t_Param = value.Cast<IParam>().ToList<IParam>();
                UpdateByParam(Params.ParamTimeBaseEvent, t_Param);
            }
        }

        public Param_TimeBaseEvents ParamTimeBaseEvent_2
        {
            get
            {
                Param_TimeBaseEvents Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamTimeBaseEvent);
                Param_Obj = (Param_TimeBaseEvents)t_Param[1];
                return Param_Obj;
            }
            set
            {
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamTimeBaseEvent);
                if (value != null)
                {
                    if (t_Param.Count <= 0)
                        t_Param.Add(null);
                    if (t_Param.Count <= 1)
                        t_Param.Add(null);
                    t_Param[1] = value;
                }
                //List<IParam> t_Param = null;
                //t_Param = value.Cast<IParam>().ToList<IParam>();
                UpdateByParam(Params.ParamTimeBaseEvent, t_Param);
            }
        }

        public TBE_PowerFail ParamTBPowerFail
        {
            get
            {
                TBE_PowerFail Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamTBPowerFail);
                Param_Obj = (TBE_PowerFail)t_Param[0];
                return Param_Obj;
            }
            set
            {
                UpdateByParam(Params.ParamTBPowerFail, value);
            }
        }

        public Param_MajorAlarmProfile ParamMajorAlarmProfile
        {
            get
            {
                Param_MajorAlarmProfile Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamMajorAlarmProfile);
                Param_Obj = (Param_MajorAlarmProfile)t_Param[0];
                return Param_Obj;
            }
            set
            {
                UpdateByParam(Params.ParamMajorAlarmProfile, value);
            }
        }

        public List<Param_EventsCaution> ParamEventsCaution
        {
            get
            {
                List<Param_EventsCaution> Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamEventsCaution);
                Param_Obj = t_Param.Cast<Param_EventsCaution>().ToList();
                return Param_Obj;
            }
            set
            {
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamEventsCaution);
                if (value != null && value.Count > 0)
                {
                    t_Param.Clear();
                    t_Param.AddRange(value);
                }
                //List<IParam> t_Param = null;
                //t_Param = value.Cast<IParam>().ToList<IParam>();
                UpdateByParam(Params.ParamEventsCaution, t_Param);
            }
        }

        public Param_IP_Profiles[] ParamIPProfiles
        {
            get
            {
                Param_IP_Profiles[] Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamIPProfiles);
                Param_Obj = t_Param.Cast<Param_IP_Profiles>().ToArray();
                return Param_Obj;
            }
            set
            {
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamIPProfiles);
                if (value != null && value.Length > 0)
                {
                    t_Param.Clear();
                    t_Param.AddRange(value);
                }
                //List<IParam> t_Param = null;
                //t_Param = value.Cast<IParam>().ToList<IParam>();
                UpdateByParam(Params.ParamIPProfiles, t_Param);
            }
        }

        public Param_WakeUp_Profile[] ParamWakeUpProfile
        {
            get
            {
                Param_WakeUp_Profile[] Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamWakeUpProfile);
                Param_Obj = t_Param.Cast<Param_WakeUp_Profile>().ToArray();
                return Param_Obj;
            }
            set
            {
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamWakeUpProfile);
                if (value != null && value.Length > 0)
                {
                    t_Param.Clear();
                    t_Param.AddRange(value);
                }
                //List<IParam> t_Param = null;
                //t_Param = value.Cast<IParam>().ToList<IParam>();
                UpdateByParam(Params.ParamWakeUpProfile, t_Param);
            }
        }

        public Param_Number_Profile[] ParamNumberProfile
        {
            get
            {
                Param_Number_Profile[] Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamNumberProfile);
                Param_Obj = t_Param.Cast<Param_Number_Profile>().ToArray();
                return Param_Obj;
            }
            set
            {
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamNumberProfile);
                if (value != null && value.Length > 0)
                {
                    t_Param.Clear();
                    t_Param.AddRange(value);
                }
                //List<IParam> t_Param = null;
                //t_Param = value.Cast<IParam>().ToList<IParam>();
                UpdateByParam(Params.ParamNumberProfile, t_Param);
            }
        }

        public Param_Communication_Profile ParamCommunicationProfile
        {
            get
            {
                Param_Communication_Profile Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamCommunicationProfile);
                Param_Obj = (Param_Communication_Profile)t_Param[0];
                return Param_Obj;
            }
            set
            {
                UpdateByParam(Params.ParamCommunicationProfile, value);
            }
        }

        public Param_Keep_Alive_IP ParamKeepAliveIP
        {
            get
            {
                Param_Keep_Alive_IP Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamKeepAliveIP);
                Param_Obj = (Param_Keep_Alive_IP)t_Param[0];
                return Param_Obj;
            }
            set
            {
                UpdateByParam(Params.ParamKeepAliveIP, value);
            }
        }

        public Param_ModemLimitsAndTime ParamModemLimitsAndTime
        {
            get
            {
                Param_ModemLimitsAndTime Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamModemLimitsAndTime);
                Param_Obj = (Param_ModemLimitsAndTime)t_Param[0];
                return Param_Obj;
            }
            set
            {
                UpdateByParam(Params.ParamModemLimitsAndTime, value);
            }
        }

        public Param_Modem_Initialize ParamModemInitialize
        {
            get
            {
                Param_Modem_Initialize Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamModemInitialize);
                Param_Obj = (Param_Modem_Initialize)t_Param[0];
                return Param_Obj;
            }
            set
            {
                UpdateByParam(Params.ParamModemInitialize, value);
            }
        }

        public Param_ModemBasics_NEW ParamModemBasicsNEW
        {
            get
            {
                Param_ModemBasics_NEW Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamModemBasicsNEW);
                Param_Obj = (Param_ModemBasics_NEW)t_Param[0];
                return Param_Obj;
            }
            set
            {
                UpdateByParam(Params.ParamModemBasicsNEW, value);
            }
        }

        public Param_TCP_UDP ParamTCPUDP
        {
            get
            {
                Param_TCP_UDP Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamTCPUDP);
                Param_Obj = (Param_TCP_UDP)t_Param[0];
                return Param_Obj;
            }
            set
            {
                UpdateByParam(Params.ParamTCPUDP, value);
            }
        }

        public Param_Display_PowerDown ParamDisplayPowerDown
        {
            get
            {
                Param_Display_PowerDown Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamDisplayWindowPowerDown);
                Param_Obj = (Param_Display_PowerDown)t_Param[0];
                return Param_Obj;
            }
            set
            {
                UpdateByParam(Params.ParamDisplayWindowPowerDown, value);
            }
        }

        public Param_Generel_Process ParamGeneralProcess
        {
            get
            {
                Param_Generel_Process Param_Obj = null;
                List<IParam> t_Param = null;
                t_Param = SelectByParam(Params.ParamGeneralProcess);
                Param_Obj = (Param_Generel_Process)t_Param[0];
                return Param_Obj;
            }
            set
            {
                UpdateByParam(Params.ParamGeneralProcess, value);
            }
        }

        #endregion

        #endregion

        public ParamConfigurationSet()
        {
            configurationSet = new ConcurrentDictionary<Params, List<IParam>>(5, 50);
            ///Initialize ConfigurationSet
            ParamConfigurationSet.Init_ConfigurationSet(this);
            ParamConfigurationSet.Init_DefaultConfigurationSet(this);
        }

        #region Init_ConfigurationSet

        internal static void Init_ConfigurationSet(ParamConfigurationSet configurationSet)
        {
            try
            {
                var Params = Enum.GetValues(typeof(Params));
                configurationSet.configurationSet.Clear();
                foreach (Params paramItem in Params)
                {
                    ///Add Initialize ConfigurationSet
                    configurationSet.configurationSet.TryAdd(paramItem, new List<IParam>(01));
                }
            }
            catch
            {
                throw;
            }
        }

        internal static void Init_DefaultConfigurationSet(ParamConfigurationSet configurationSet)
        {
            try
            {
                #region Meter_Parameters

                ///Init With Default Values/Variables
                configurationSet.ParamMonitoringTime = new Param_Monitoring_time();
                configurationSet.ParamLimits = new Param_Limits();
                configurationSet.ParamTariffication = new Param_ActivityCalendar();
                configurationSet.ParamMDI = new Param_MDI_parameters();

                configurationSet.ParamDisplayWindowsNormal = new DisplayWindows();
                configurationSet.ParamDisplayWindowsAlternate = new DisplayWindows();
                configurationSet.ParamDisplayWindowsTestMode = new DisplayWindows();

                configurationSet.ParamCTPTRatio = new Param_CTPT_ratio();
                configurationSet.ParamDecimalPoint = new Param_decimal_point();
                configurationSet.ParamPassword = new Param_password();
                configurationSet.ParamCustomerReferenceCode = new Param_Customer_Code();
                configurationSet.ParamEnergy = new Param_Energy_Parameter();
                configurationSet.ParamClockCalib = new Param_clock_caliberation();
                configurationSet.ParamContactor = new Param_ContactorExt();

                configurationSet.ParamTimeBaseEvent_1 = new Param_TimeBaseEvents();
                configurationSet.ParamTimeBaseEvent_2 = new Param_TimeBaseEvents();
                configurationSet.ParamTBPowerFail = new TBE_PowerFail();

                configurationSet.ParamMajorAlarmProfile = new Param_MajorAlarmProfile();
                configurationSet.ParamEventsCaution = Param_EventCautionHelper.Init_ParamEventCaution();

                configurationSet.ParamDisplayPowerDown = new Param_Display_PowerDown();
                configurationSet.ParamGeneralProcess = new Param_Generel_Process();

                #endregion
                #region Init_ModemParameters

                ///Param_IP_Profiles_object 
                Param_IP_Profiles[] Param_IP_Profiles_object =
                    Param_IP_ProfilesHelper.Param_IP_Profiles_initailze(Param_IP_ProfilesHelper.Max_IP_Profile);
                ///Param_Wakeup_Profile_object
                Param_WakeUp_Profile[] Param_Wakeup_Profile_object =
                    Param_WakeUp_ProfileHelper.Param_Wakeup_Profile_object_initialze(Param_WakeUp_ProfileHelper.Max_WakeUp_Profile);
                ///Param_Number_Profile_object
                Param_Number_Profile[] Param_Number_Profile_object =
                    Param_Number_ProfileHelper.Param_Number_Profile_object_initialze(Param_Number_ProfileHelper.Max_Number_Profile + 1);

                #endregion
                #region Modem_Parameters

                configurationSet.ParamIPProfiles = Param_IP_Profiles_object;
                configurationSet.ParamWakeUpProfile = Param_Wakeup_Profile_object;
                configurationSet.ParamNumberProfile = Param_Number_Profile_object;
                configurationSet.ParamKeepAliveIP = new Param_Keep_Alive_IP();
                configurationSet.ParamCommunicationProfile = new Param_Communication_Profile();
                configurationSet.ParamModemLimitsAndTime = new Param_ModemLimitsAndTime();
                configurationSet.ParamModemInitialize = new Param_Modem_Initialize();
                configurationSet.ParamModemBasicsNEW = new Param_ModemBasics_NEW();
                ///configurationSet.ParamIPV4 = new Param_IPV4();
                configurationSet.ParamTCPUDP = new Param_TCP_UDP();
                configurationSet.ParamStatusWordMap = new Param_StatusWordMap();

                #endregion
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region SelectByParam

        public List<IParam> SelectByParam(Params Parameters)
        {
            List<IParam> loadedParam = null;
            try
            {
                loadedParam = SelectByParam(ConfigurationSet, Parameters);
                if (loadedParam == null || loadedParam.Count < 0)
                    throw new ArgumentNullException("" + Parameters, "Invalid Parameter Configuration");
                return loadedParam;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occured while Get Parameter {0}", Parameters), ex);
            }
        }

        public static List<IParam> SelectByParam(ConcurrentDictionary<Params, List<IParam>> configurationSet, Params Parameters)
        {
            List<IParam> loadedParam = null;
            int retry_Count = 0;
            bool configSet = false;
            try
            {
                while (retry_Count < 05)
                {
                    configSet = configurationSet.TryGetValue(Parameters, out loadedParam);
                    if (!configSet)
                        continue;
                    else
                        break;
                }
                if (!configSet)
                    throw new ArgumentNullException("" + Parameters, "Invalid Parameter Configuration");
                return loadedParam;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occured while Get Parameter {0}", Parameters), ex);
            }
        }

        public static List<List<IParam>> SelectByParam(ConcurrentDictionary<Params, List<IParam>> configurationSet, List<Params> Parameters)
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
                        Param = SelectByParam(configurationSet, parameter);
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

        #endregion

        #region UpdateByParam

        public static List<IParam> UpdateFactoryMethod(Params Parameters, List<IParam> paramListNew)
        {
            ///Validate List<IParam>
            if (paramListNew == null || paramListNew.Count > 0)
            {
                return paramListNew;
            }
            else
                throw new ArgumentNullException("" + Parameters, "Invalid Parameter Configuration");
        }

        public List<IParam> UpdateByParam(Params Parameters, IParam paramObj)
        {
            try
            {
                if (paramObj == null)
                    throw new ArgumentNullException("" + Parameters, "Invalid Parameter IParam");
                List<IParam> TParamList = null;
                TParamList = ParamConfigurationSet.SelectByParam(ConfigurationSet, Parameters);
                if (TParamList == null)
                    TParamList = new List<IParam>();
                else
                    TParamList.Clear();
                TParamList.Add(paramObj);
                return ParamConfigurationSet.UpdateByParam(ConfigurationSet, Parameters, TParamList);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred while Update/Add {0} Parameter", Parameters), ex);
            }
        }

        public List<IParam> UpdateByParam(Params Parameters, List<IParam> paramList)
        {
            try
            {
                return ParamConfigurationSet.UpdateByParam(ConfigurationSet, Parameters, paramList);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occured while Update/Add {0} Parameter", Parameters), ex);
            }
        }

        public static List<IParam> UpdateByParam(ConcurrentDictionary<Params, List<IParam>> configurationSet, Params Parameters, List<IParam> paramList)
        {
            List<IParam> loadedParam = null;
            try
            {
                Type[] typeOff = ConfigFileHelper.GetParamsTypes(Parameters);
                IParam t = null;
                bool validated = false;
                ///validate Match With Type
                foreach (Type type in typeOff)
                {
                    t = paramList.Find((x) => x != null && x.GetType() == type);
                    if (t != null)
                    {
                        validated = true;
                    }
                    else
                    {
                        validated = false;
                        break;
                    }
                }
                if (!validated || paramList == null || paramList.Count <= 0)
                    throw new ArgumentNullException("" + Parameters, "Invalid Parameter List<IParam> Configuration");
                ///Validate Argument paramList
                ///Func<Params, List<IParam>, List<IParam>> delegateUpdateFactory =   new Func<Params, List<IParam>, List<IParam>>(UpdateFactoryMethod);
                Func<Params, List<IParam>, List<IParam>> delegateUpdateFactory =
                #region delegateUpdateFactory
 (Params ParametersArg, List<IParam> paramListNewArg) =>
 {
     ///Validate List<IParam>
     if (paramListNewArg == null)
     {
         paramListNewArg = new List<IParam>();
         paramListNewArg.AddRange(paramList);
     }
     else if (paramListNewArg.Count > 0 && paramListNewArg != paramList)
     {
         paramListNewArg.Clear();
         paramListNewArg.AddRange(paramList);
     }
     return paramListNewArg;
 };
                #endregion
                loadedParam = configurationSet.AddOrUpdate(Parameters, paramList, delegateUpdateFactory);
                return loadedParam;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occured while Update/Add {0} Parameter", Parameters), ex);
            }
        }

        #endregion

        // Convert the Smart meter parameters into the array of bytes
        public static byte[] GetBinaryOfSmartMeterParams(ParamConfigurationSet obj)
        {
            if (obj == null)
                return null;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream mStream = new MemoryStream();
                formatter.Serialize(mStream, obj);
                return mStream.ToArray();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        // convert the bytes of smart meter params into the SmartMeterParams into an object
        public static ParamConfigurationSet GetSmartMeterParamsObjFromBinary(byte[] buffer)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream mStream = new MemoryStream();
                mStream.Read(buffer, 0, buffer.Length);
                mStream.Seek(0, SeekOrigin.Begin);
                ParamConfigurationSet _meterParams = (ParamConfigurationSet)formatter.Deserialize(mStream);
                return _meterParams;
            }
            catch (Exception)
            {

                throw;
            }
        }

        object ICloneable.Clone()
        {
            try
            {

                ParamConfigurationSet temp = new ParamConfigurationSet();
                Init_DefaultConfigurationSet(temp);
                var allnotnullElement = from m in ConfigurationSet
                                        where m.Value != null && m.Value.Count > 0
                                        select m;
                List<IParam> parms;
                foreach (var item in allnotnullElement)
                {
                    parms = new List<IParam>();
                    foreach (var it in item.Value)
                    {
                        if (it != null && it is ICloneable && it is IParam)
                        {
                            var new_it = (IParam)(((ICloneable)it).Clone());

                            parms.Add(new_it);
                        }
                    }
                    temp.ConfigurationSet.AddOrUpdate(item.Key, parms, ((x, y) => y = parms));
                }
                return temp;

            }
            catch (Exception)
            {
                
                throw new Exception("Error Prepareing Clone of ParamConfiguratinSet");
            }
                                    

 
              
        }

        public ParamConfigurationSet Clone() 
        {
           return (ParamConfigurationSet)((ICloneable)this).Clone();
        }
    }
}
