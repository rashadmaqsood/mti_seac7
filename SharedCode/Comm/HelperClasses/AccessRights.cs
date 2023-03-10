using SharedCode.Comm.DataContainer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace SharedCode.Comm.HelperClasses
{
    [Serializable]
    public class ApplicationRight : ICloneable
    {
        #region Properties

        //_____________________________________________________________
        public List<AccessRights> MeterRights { get; set; }
        //_____________________________________________________________
        public List<AccessRights> LimitsRights { get; set; }
        //_____________________________________________________________
        public List<AccessRights> MonitoringTimeRights { get; set; }
        //_____________________________________________________________
        public List<AccessRights> ActivityCalenderRights { get; set; }
        //_____________________________________________________________
        public List<AccessRights> MDIParametersRights { get; set; }
        //_____________________________________________________________
        public List<AccessRights> LoadProfileRights { get; set; }
        //_____________________________________________________________
        public List<AccessRights> DisplayWindowsRights { get; set; }
        //_____________________________________________________________
        public List<AccessRights> TestingRights { get; set; }
        //_____________________________________________________________
        public List<AccessRights> MiscRights { get; set; }
        //_____________________________________________________________
        public List<AccessRights> ContactorParamRights { get; set; }
        //_____________________________________________________________
        public List<AccessRights> SinglePhaseRights { get; set; }
        //_____________________________________________________________
        public List<AccessRights> ClockRights { get; set; }
        //_____________________________________________________________
        public List<AccessRights> ModemRights { get; set; }
        //_____________________________________________________________
        public List<AccessRights> StandardModemRights { get; set; }
        //_____________________________________________________________
        public List<AccessRights> HDLCSetupRights { get; set; }
        //_____________________________________________________________
        public List<AccessRights> MeterSchedulingRights { get; set; }
        //_____________________________________________________________
        public List<AccessRights> BillingRights { get; set; }
        //_____________________________________________________________
        public List<AccessRights> InstantenousDataRights { get; set; }
        //_____________________________________________________________   
        public List<AccessRights> EventsRights { get; set; }
        //_____________________________________________________________   
        public List<AccessRights> MeterEventsRights { get; set; }
        //_____________________________________________________________
        public List<AccessRights> SettingsRights { get; set; }
        //_____________________________________________________________
        public List<AccessRights> DebugRights { get; set; }
        //_____________________________________________________________ 
        public List<AccessRights> LoadProfileParams { get; set; }
        //_____________________________________________________________
        public List<AccessRights> DisplayPowerDown { get; set; }
        //_____________________________________________________________
        //public List<AccessRights> TimeBasedEvent { get; set; }
        //_____________________________________________________________
        public List<AccessRights> IPProfiles { get; set; }
        //_____________________________________________________________
        public List<AccessRights> NumberProfiles { get; set; }
        //_____________________________________________________________
        public List<AccessRights> WakeupProfiles { get; set; }
        //_____________________________________________________________
        public List<AccessRights> CommunicationProfiles { get; set; }
        //_____________________________________________________________
        public List<AccessRights> KeepAlive { get; set; }
        //_____________________________________________________________
        public List<AccessRights> ModemLimits { get; set; }
        //_____________________________________________________________
        public List<AccessRights> ModemInitialize { get; set; }
        //_____________________________________________________________
        public List<AccessRights> GeneralRights { get; set; }
        //_____________________________________________________________
        public List<sAccessRights> MsnRange { get; set; }
        //_____________________________________________________________
        public List<AccessRights> OtherRights { get; set; }
        //_____________________________________________________________
        public List<AccessRights> StatusWordMapRights { get; set; }
        //_____________________________________________________________
        public List<AccessRights> StatusWordParamRights { get; set; }
        //_____________________________________________________________
        public List<AccessRights> EnergyMizerRights { get; set; }
        //_____________________________________________________________
        public int RightsId { get; set; }
        //_____________________________________________________________
        public int MeterModelId { get; set; }
        //_____________________________________________________________
        public string MeterModel { get; set; }



        #endregion

        #region Methods

        //_____________________________________________________________
        public static List<AccessRights> GetDefaultAccessRightByEnum(Type abc)
        {
            try
            {
                bool default_Write = true, default_Read = true;
                Type type = abc;
                if (!type.IsEnum) throw new ArgumentException("Argument Must be Type Of an Enumeration");
                var defaultAccesslist = new List<AccessRights>();
                var allEnumItems = Enum.GetNames(type);
                //v4.8.23
                if (type == typeof(OtherRights))
                {
                    default_Write = default_Read = false;
                }
                else if (type == typeof(GeneralRights))
                {
                    default_Write = false; default_Read = true;
                }

                foreach (var item in allEnumItems)
                {
                    var userRight = new AccessRights();
                    userRight.QuantityName = item;
                    userRight.Write = default_Write;
                    userRight.Read = default_Read;
                    userRight.QuantityType = type;
                    defaultAccesslist.Add(userRight);
                }
                return defaultAccesslist;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //_________________________ v4.8.15 ___________________________
        public static List<sAccessRights> sGetDefaultAccessRightByEnum(Type abc)
        {
            try
            {
                Type type = abc;
                if (!type.IsEnum) throw new ArgumentException("Argument Must be Type Of an Enumeration");
                var defaultAccesslist = new List<sAccessRights>();
                var allEnumItems = Enum.GetNames(type);
                byte b = 1;
                foreach (var item in allEnumItems)
                {
                    var userRight = new sAccessRights();
                    userRight.QuantityName = item;
                    if (type == typeof(MsnRange))
                    {
                        userRight.Start = userRight.Start = "00";
                        if (b == 1) userRight.End = userRight.End = "41";
                        else if (b == 2) userRight.End = userRight.End = "99";
                        else if (b == 3) userRight.End = userRight.End = "999999";
                        b++;
                    }
                    else
                    {
                        userRight.Start = userRight.Start = "Please Enter";
                        userRight.End = userRight.End = "Please Enter";
                    }
                    userRight.QuantityType = type;
                    defaultAccesslist.Add(userRight);
                }
                return defaultAccesslist;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //_____________________________________________________________ 
        public static byte[] GetBinaryObject(ApplicationRight obj_Application)
        {
            using (MemoryStream binStram = new MemoryStream())
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(binStram, obj_Application);
                    return binStram.ToArray();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        //_____________________________________________________________ 
        public static ApplicationRight GetObjectFromBytes(byte[] obj)
        {
            using (MemoryStream strm = new MemoryStream())
            {
                try
                {
                    strm.Write(obj, 0, obj.Length);
                    strm.Seek(0, SeekOrigin.Begin);
                    var Formatter = new BinaryFormatter();
                    var obj_apRights = (ApplicationRight)Formatter.Deserialize(strm);
                    return obj_apRights;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        //_____________________________________________________________ 
        public override string ToString()
        {
            using (MemoryStream binStram = new MemoryStream())
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(binStram, this);
                    return BitConverter.ToString(binStram.ToArray()).Replace("-", "");
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        //_____________________________________________________________ 

        public static ApplicationRight GetDefaultRights()
        {
            try
            {
                var rights = new ApplicationRight();
                rights.MeterRights              = ApplicationRight.GetDefaultAccessRightByEnum(typeof(MeterRights));
                rights.ActivityCalenderRights   = ApplicationRight.GetDefaultAccessRightByEnum(typeof(ActivityCalender));
                rights.BillingRights            = ApplicationRight.GetDefaultAccessRightByEnum(typeof(Billing));
                rights.ClockRights              = ApplicationRight.GetDefaultAccessRightByEnum(typeof(Clock));
                rights.ContactorParamRights     = ApplicationRight.GetDefaultAccessRightByEnum(typeof(ContactorParam));
                rights.DebugRights              = ApplicationRight.GetDefaultAccessRightByEnum(typeof(Debug));
                rights.DisplayWindowsRights     = ApplicationRight.GetDefaultAccessRightByEnum(typeof(DisplayWindowsParams));
                rights.DisplayPowerDown         = ApplicationRight.GetDefaultAccessRightByEnum(typeof(DisplayPowerDownMode));
                rights.EventsRights             = ApplicationRight.GetDefaultAccessRightByEnum(typeof(Events));
                rights.MeterEventsRights        = ApplicationRight.GetDefaultAccessRightByEnum(typeof(MeterEvent));
                rights.InstantenousDataRights   = ApplicationRight.GetDefaultAccessRightByEnum(typeof(eInstantaneousData));
                rights.LimitsRights             = ApplicationRight.GetDefaultAccessRightByEnum(typeof(Limits));
                rights.LoadProfileRights        = ApplicationRight.GetDefaultAccessRightByEnum(typeof(LoadProfileDataRights));
                rights.MDIParametersRights      = ApplicationRight.GetDefaultAccessRightByEnum(typeof(MDIParameters));
                rights.MeterSchedulingRights    = ApplicationRight.GetDefaultAccessRightByEnum(typeof(MeterScheduling));
                rights.MiscRights               = ApplicationRight.GetDefaultAccessRightByEnum(typeof(Misc));
                rights.ModemRights              = ApplicationRight.GetDefaultAccessRightByEnum(typeof(Modem));
                rights.StandardModemRights      = ApplicationRight.GetDefaultAccessRightByEnum(typeof(StandardModem));
                rights.HDLCSetupRights          = ApplicationRight.GetDefaultAccessRightByEnum(typeof(Modem_HDLCSetup));
                rights.MonitoringTimeRights     = ApplicationRight.GetDefaultAccessRightByEnum(typeof(MonitoringTime));
                rights.SettingsRights           = ApplicationRight.GetDefaultAccessRightByEnum(typeof(Setting));
                rights.SinglePhaseRights        = ApplicationRight.GetDefaultAccessRightByEnum(typeof(SinglePhase));
                rights.TestingRights            = ApplicationRight.GetDefaultAccessRightByEnum(typeof(Testing));
                rights.LoadProfileParams        = ApplicationRight.GetDefaultAccessRightByEnum(typeof(LoadProfileParams));
                //Modem Parameters
                rights.IPProfiles               = ApplicationRight.GetDefaultAccessRightByEnum(typeof(IPProfiles));
                rights.NumberProfiles           = ApplicationRight.GetDefaultAccessRightByEnum(typeof(NumberProfiles));
                rights.CommunicationProfiles    = ApplicationRight.GetDefaultAccessRightByEnum(typeof(CommunicationProfile));
                rights.WakeupProfiles           = ApplicationRight.GetDefaultAccessRightByEnum(typeof(WakeUpProfiles));
                rights.KeepAlive                = ApplicationRight.GetDefaultAccessRightByEnum(typeof(KeepAlive));
                rights.ModemLimits              = ApplicationRight.GetDefaultAccessRightByEnum(typeof(ModemLimit));
                rights.ModemInitialize          = ApplicationRight.GetDefaultAccessRightByEnum(typeof(ModemInitialize));
                rights.GeneralRights            = ApplicationRight.GetDefaultAccessRightByEnum(typeof(GeneralRights)); //v4.10.13
                rights.MsnRange                 = ApplicationRight.sGetDefaultAccessRightByEnum(typeof(MsnRange)); //v4.10.13
                rights.OtherRights              = ApplicationRight.GetDefaultAccessRightByEnum(typeof(OtherRights)); //v4.8.23
                rights.StatusWordMapRights      = ApplicationRight.GetDefaultAccessRightByEnum(typeof(eStatusWordWindow)); //v4.8.41
                rights.StatusWordParamRights    = ApplicationRight.GetDefaultAccessRightByEnum(typeof(StatusWordParam));
                rights.EnergyMizerRights        = ApplicationRight.GetDefaultAccessRightByEnum(typeof(EnergyMizer)); //v5.3.12

                return rights;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<PropertyInfo> Get_PropertyInfo()
        {
            List<PropertyInfo> Ret_Val = new List<PropertyInfo>();
            PropertyInfo[] pinfo;
            PropertyInfo PropInfoRet = null;
            try
            {
                pinfo = typeof(ApplicationRight).GetProperties();
                Ret_Val.AddRange(pinfo);
                //Sort On Basis Of Property Name Alphabetic Order
                Ret_Val.Sort((x,y)=> x.Name.CompareTo(y.Name));
            }
            catch { }
            return Ret_Val;
        }

        public static List<PropertyInfo> GetMeteringParameters_PropertyInfo()
        {
            List<PropertyInfo> Ret_Val = new List<PropertyInfo>();
            PropertyInfo[] pinfo;
            PropertyInfo PropInfoRet = null;
            try
            {
                pinfo = typeof(ApplicationRight).GetProperties();
                //Limits
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("LimitsRights");
                });
                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);
                //Monitoring Times
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("MonitoringTimeRights");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                //ActivityCalenderRights
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("ActivityCalenderRights");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                //MDIParametersRights
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("MDIParametersRights");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                //LoadProfileParams
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("LoadProfileParams");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                //DisplayWindowsRights
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("DisplayWindowsRights");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                //DisplayPowerDown
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("DisplayPowerDown");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                //SinglePhaseRights
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("SinglePhaseRights");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                //ContactorParamRights
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("ContactorParamRights");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                //MiscRights
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("MiscRights");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                //ClockRights
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("ClockRights");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                //TestingRights
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("TestingRights");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                //MeterSchedulingRights
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("MeterSchedulingRights");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                //StatusWordMapRights //v4.8.41
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("StatusWordMapRights");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("StatusWordParamRights");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("Schedule_Entry");
                });
                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                // TODO: 02   ** Access Writes **  Get Property
                //Schedule Entries
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("GeneratorStart");
                });
                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);
                // ~~~~
                

            }
            catch (Exception)
            {
            }
            return Ret_Val;
        }

        public static List<PropertyInfo> GetModemParameters_PropertyInfo()
        {
            List<PropertyInfo> Ret_Val = new List<PropertyInfo>();
            PropertyInfo[] pinfo;
            PropertyInfo PropInfoRet = null;
            try
            {
                pinfo = typeof(ApplicationRight).GetProperties();
                //ModemRights
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("ModemRights");
                });
                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);
                
                //IPProfiles
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("IPProfiles");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                //NumberProfiles
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("NumberProfiles");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                //WakeupProfiles
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("WakeupProfiles");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                //CommunicationProfiles
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("CommunicationProfiles");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                //KeepAlive
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("KeepAlive");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                //ModemLimits
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("ModemLimits");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                //ModemInitialize
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("ModemInitialize");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

            }
            catch (Exception)
            {
            }
            return Ret_Val;
        }

        public static List<PropertyInfo> GetStandardModemParameters_PropertyInfo()
        {
            List<PropertyInfo> Ret_Val = new List<PropertyInfo>();
            PropertyInfo[] pinfo;
            PropertyInfo PropInfoRet = null;
            try
            {
                pinfo = typeof(ApplicationRight).GetProperties();
                //ModemRights
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("StandardModemRights");
                });
                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                //IPProfiles
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("StandardIPProfiles");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                //NumberProfiles
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("StandardNumberProfiles");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                //KeepAlive
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("StandardKeepAlive");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

            }
            catch (Exception)
            {
            }
            return Ret_Val;
        }

        public static List<PropertyInfo> GetHDLCSetupParameters_PropertyInfo()
        {
            List<PropertyInfo> Ret_Val = new List<PropertyInfo>();
            PropertyInfo[] pinfo;
            PropertyInfo PropInfoRet = null;
            try
            {
                pinfo = typeof(ApplicationRight).GetProperties();
                //ModemRights
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("HDLCSetupRights");
                });
                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                //IPProfiles
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("Get_All");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                //NumberProfiles
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("Set_HDLCAddress");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

                //KeepAlive
                PropInfoRet = pinfo.First<PropertyInfo>((PropertyInfo PropInfo) =>
                {
                    return PropInfo != null &&
                        PropInfo.Name.Contains("Set_Inactivity_Timeout");
                });

                if (PropInfoRet != null)
                    Ret_Val.Add(PropInfoRet);

            }
            catch (Exception)
            {
            }
            return Ret_Val;
        }

        #endregion

        #region Support_Function

        public static bool IsControlWTEnable(Type QuantityType, String QuantityName, List<AccessRights> AccessRights)
        {
            bool isEnable = false;
            try
            {
                AccessRights right = AccessRights.Find((x) => x.QuantityType == QuantityType && x.QuantityName.Equals(QuantityName));
                if (right != null)
                    isEnable = right.Write;
            }
            catch { }
            return isEnable;
        }
        public static string sControlStartVal(Type QuantityType, String QuantityName, List<sAccessRights> AccessRights)
        {
            string start = uint.MaxValue.ToString();
            try
            {
                sAccessRights right = AccessRights.Find((x) => x.QuantityType == QuantityType && x.QuantityName.Equals(QuantityName));
                if (right != null)
                    start = right.Start;
            }
            catch { }
            return start;
        }

        public static bool IsControlRDEnable(Type QuantityType, String QuantityName, List<AccessRights> AccessRights)
        {
            bool isEnable = false;
            try
            {
                AccessRights right = AccessRights.Find((x) =>
                {
                    return x.QuantityType == QuantityType && x.QuantityName.Equals(QuantityName);
                });
                if (right != null)
                    isEnable = right.Read;
            }
            catch { }
            return isEnable;
        }

        public static string sControlEndVal(Type QuantityType, String QuantityName, List<sAccessRights> AccessRights)
        {
            string end = uint.MinValue.ToString().PadRight(10,'0');
            try
            {
                sAccessRights right = AccessRights.Find((x) =>
                {
                    return x.QuantityType == QuantityType && x.QuantityName.Equals(QuantityName);
                });
                if (right != null)
                    end = right.End;
            }
            catch { }
            return end;
        }

        #endregion

        public object Clone()
        {
            var bin = GetBinaryObject(this);
            return GetObjectFromBytes(bin);
        }
    }

    public class ApplicationRight_LT : IEquatable<ApplicationRight_LT>
    {
        public int ID { get; set; }
        public string Identifier { get; set; }
        public UserTypeID Role { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(ApplicationRight_LT other)
        {
            return ID == other.ID;
        }

        public override string ToString()
        {
            return Identifier;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }

    [Serializable]
    public class AccessRights
    {
        #region Properties

        public string QuantityName { get; set; }
        [Browsable(false)]
        public Type QuantityType { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }

        #endregion
    }

    [Serializable]
    public class sAccessRights
    {
        #region Properties

        public string QuantityName { get; set; }
        [Browsable(false)]
        public Type QuantityType { get; set; }
        public string Start { get; set; }
        public string End { get; set; }

        #endregion
    }

    #region Enumerations of All Quantities

    #region Meter

    public enum MeterRights
    {
        Limits = 1,
        MonitoringTime,
        ActivityCalender,
        MDI,
        LoadProfile,
        DisplayWindows,
        DisplayPowerDown,
        Contactor,
        SinglePhase,
        Misc,
        Clock,
        Testing,
        MeterScheduling,
        // TODO: 01   ** Access Writes ** Add Property
        Schedule_Entry,
        GeneratorStart
    };
    public enum Limits
    {
        OverVolt = 1,
        UnderVolt,
        ImbalanceVolt,
        HighNeturalCurrent,
        ReverseEnergykWh,

        TemperEnergykWh,
        CTFailLimitAmp,
        PTFailLimitAmp,
        PTFailLimitV,
        OverCurrentByPhaseT1,
        OverCurrentByPhaseT2,
        OverCurrentByPhaseT3,
        OverCurrentByPhaseT4,
        OverLoadByPhaseT1,
        OverLoadByPhaseT2,
        OverLoadByPhaseT3,
        OverLoadByPhaseT4,
        OverLoadTotalT1,
        OverLoadTotalT2,
        OverLoadTotalT3,
        OverLoadTotalT4,
        MDIExceedT1,
        MDIExceedT2,
        MDIExceedT3,
        MDIExceedT4,
        Contactor_Failure_Pwr,
        //for Single Phase  //Azeem
        OverCurrent_Phase, 
        Meter_ON_Load,
        PowerFactor_Change,
        CrestFactorLow,
        CrestFactorHigh,
        OverPower,
    };

    public enum MonitoringTime
    {
        PowerFail,
        PhaseFail,
        OverVolt,
        HighNeturalCurrent,
        ImbalanceVolt,
        OverCurrent,
        OverLoad,
        ReversePolarity,
        HallSensor,
        UnderVolt,
        ReverseEnergy,
        TemperEnergy,
        CTFail,
        PTFail,
        PUDToMoniter,
        PUDForEnergyRecording,
        PhaseSequence,
        StartGenerator,
        ContactorFailure,
    };

    public enum ActivityCalender
    {
        ActivateCalander,
        DayProfile,
        WeekProfile,
        SeasonProfile,
        SpecialDays,
        CalenderName,
        CalenderActivationNow,
        CalenderActivationOnSpecificDate
    };

    public enum MDIParameters
    {
        MinimumTimeIntervalBetweenManualReset,
        MDIAutoResetDataTime,
        MDInterval,
        MDISlidesCount,
        ManualResetByButtonflag,
        ManualResetByRemoteCommandflag,
        ManualResetinPowerDownModeflag,
        DisableAutoResetinPowerDownModeflag,
        AutoResetEnableflag

    };

    public enum LoadProfileParams
    {
        LoadProfileChannels,
        LoadProfileInterval,
        LoadProfileChannels2,
        LoadProfileInterval2,
        PQLoadProfile
    };

    public enum DisplayWindowsParams
    {
        DisplayWindowsItems,
        DisplayWindowsAlternate,
        DisplayWindowsNormal,
        DisplayWindowsTest,
        NumberFormat,
        ScrollTime
    };

    public enum Testing
    {
        TestingPanel,
        RTCUpdate
    };

    public enum Misc
    {
        CustomerReference,
        //CTPTPatio,
        CT_Ratio,
        AssociationPassword,
        GernalProcessParameter,
        EnergyParam,
        //DecimalPoint,
        DecimalPoint_BillEnergy,
        Clock,
        DecimalPoint_BillMdi,
        DecimalPoint_InstVolt,
        DecimalPoint_InstPower,
        DecimalPoint_InstCurrent,
        DecimalPoint_InstMdi,
        PT_Ratio,
        SecData_AuthenticationKey,
        SecData_EncrptiontionKey,
        SecData_Control_NoSecurity,
        SecData_Control_Authentication,
        SecData_Control_Encrptiontion,
        SecData_Control_Auth_Encrp,
        TariffOnStartGenerator
    };

    public enum ContactorParam
    {
        OnPulseTime,
        OffPulseTime,
        MinIntervalStateChange,
        PUDelayStateChange,
        IntervalFailureStatus,

        ReadStatus,
        ConnectDisconnectRelay,
        ConnectContactorThroughSwitch,
        ContactorParam,

        OverloadMonitoringTime,
        OverLoadLimit,
        TurnOffOnOverLoadflag,

        RetryAutoInterval,
        RetryCount,
        RetryAutoflag,
        RetrySwitchflag,

        RetryExpireAutoInterval,
        RetryExpireAutoflag,
        RetryExpireSwitchflag,


        ReconnectTariffChangeflag,
        OpticallyDisconnectflag,
        OpticallyConnectflag
    };

    public enum SinglePhase
    {
        MTPhaseFail,
        MTEarth,
        MTReverseEnergy,
        LimitOverLoadTotal,
        MDIAutoResetDateTime,
        ErrorFlags
    };

    public enum Clock
    {
        RTC,
        ClockSyncMethods
    };

    public enum DisplayPowerDownMode
    {
        OffDelay,
        OnTime,
        OffTime,
        PowerDownDisplayFlags
    };

    #endregion

    #region General Rights Enums
    public enum MeterScheduling
    {
        PowerFailTBE1 = 1,
        PowerFailTBE2,
        TimeBaseEvent1 = 3,
        TimeBaseEvent2,
        Disable,
        DateTime,
        TimeInterval,
        TimeInterval_Sink,
        TimeInterval_Fixed
    };

    public enum Billing
    {
        CummulativeBilling,
        MonthlyBilling,
        DailyEnergy_Profile,
        Detail_Cumulative_Billing,
        Itm_kWh_Cumm_Absolute,
        Itm_kWh_Cumm_Import,
        Itm_kWh_Cumm_Export,
        Itm_kVarh_Cumm_Absolute,
        Itm_kVarh_Cumm_Import,
        Itm_kVarh_Cumm_Export,
        Itm_kVarh_Cumm_Q1,
        Itm_kVarh_Cumm_Q2,
        Itm_kVarh_Cumm_Q3,
        Itm_kVarh_Cumm_Q4,
        Itm_kW_Cumm_Absolute,
        Itm_kW_Cumm_Import,
        Itm_kW_Cumm_Export,
        Itm_kW_CurMonth_Absolute,
        Itm_kW_CurMonth_Import,
        Itm_kW_CurMonth_Export,
        Itm_kW_OccurDT_CurMonth_Absolute,
        Itm_kW_OccurDT_CurMonth_Export,
        Itm_kW_OccurDT_CurMonth_Import,
        Itm_kVar_Cumm_Absolute,
        Itm_kVar_Cumm_Import,
        Itm_kVar_Cumm_Export,
        Itm_kVar_CurMonth_Absolute,
        Itm_kVar_CurMonth_Import,
        Itm_kVar_CurMonth_Export,
        Itm_kVar_OccurDT_CurMonth_Absolute,
        Itm_kVar_OccurDT_CurMonth_Export,
        Itm_kVar_OccurDT_CurMonth_Import,
        Itm_Cumm_kvah,
        Itm_Cumm_Tamper_kW,
        Itm_Cumm_PowerFactor,

        //Inst_Itm_kW_Cumm_Absolute,
        //Inst_Itm_kW_Cumm_Import,
        //Inst_Itm_kW_Cumm_Export,
        //Inst_Itm_Current_Max_kW_Absolute,
        //Inst_Itm_Current_Max_kW_Import,
        //Inst_Itm_Current_Max_kW_Export,
        Inst_Itm_Current_Max_kW_DateTime_Absolute,
        Inst_Itm_Current_Max_kW_DateTime_Export,
        Inst_Itm_Current_Max_kW_DateTime_Import,
        //Inst_Itm_kVar_Cumm_Absolute,
        //Inst_Itm_kVar_Cumm_Import,
        //Inst_Itm_kVar_Cumm_Export,
        //Inst_Itm_kVar_CurMonth_Absolute,
        //Inst_Itm_kVar_CurMonth_Import,
        //Inst_Itm_kVar_CurMonth_Export,
        //Inst_Itm_kVar_OccurDT_CurMonth_Absolute,
        //Inst_Itm_kVar_OccurDT_CurMonth_Export,
        //Inst_Itm_kVar_OccurDT_CurMonth_Import,


        //Cumulative_TariffTL_KwhAbsolute
        //Cumulative_TariffTL_KvarhAbsolute
        //CUMULATIVE_ACTIVE_MDI_ABSOLUTE_TOTAL
        //Cumulative_TariffTL_CurrentMonthMdiKw
        //Cumulative_TariffTL_KwhImport
        //Reactive_Energy_Total_Pos
        //CUMULATIVE_ACTIVE_MDI_IMPORT_TOTAL
        //Cumulative_Reactive_MDI_Import_TOTAL
        //MONTHLY_ACTIVE_MDI_IMPORT_TOTAL
        //Cumulative_TariffTL_CurrentMonthMdiKvar
        //Cumulative_TariffTL_KwhExport
        //Reactive_Energy_Total_Neg
        //CUMULATIVE_ACTIVE_MDI_EXPORT_TOTAL
        //CUMULATIVE_REACTIVE_MDI_EXPORT_TOTAL
        //MONTHLY_ACTIVE_MDI_EXPORT_TOTAL
        //MONTHLY_REACTIVE_MDI_EXPORT_TOTAL

    };

    public enum eInstantaneousData
    {
        Voltage,
        Current,
        PowerFactor,
        ActivePower,
        ReactivePower,
        ApparentPower,
        Misc,
        TabInstantaneous,
        MDI,
        Debug_MDI,
        Instantaneous_MDI,
        MDI_Interval,
        NewInstantaneousData,
        InstantaneousRecord,
        CurrentDisplayWindows,
        ActivePower_Abs,
        ActivePower_Pos,
        ActivePower_Neg,
        ReactivePower_Abs,
        ReactivePower_Pos,
        ReactivePower_Neg,
        PowerQuadrant,
        Misc_ActiveSeason,
        Misc_ActiveTariff,
        Misc_ActiveDayProfile,
        Misc_ActiveFirmwareId,
        Misc_BatteryVolts,
        Misc_MdiResetCounter,
        Misc_CtRatio,
        Misc_EventCounter,
        Misc_LoadProfileCounter,
        Misc_PtRatio,
        Misc_PowerFailCount,
        Misc_Frequency,
        Misc_ContactorRelayStatus,
        Misc_TamperPower,
        Misc_RssiSignalStrength,
        Contactor,
        NewInst_Voltage,
        NewInst_Current,
        NewInst_Power_kW,
        NewInst_Power_kvar,
        NewInst_PowerkW_Import,
        NewInst_PowerkW_Export,
        NewInst_Powerkvar_Import,
        NewInst_Powerkvar_Export,
        NewInst_Frequency

    };

    public enum LoadProfileDataRights
    {
        ReadLoadProfileData_EntryWise,
        ReadLoadProfileData_DateTimeWise,
        ReadLoadProfileData_Scheme1,
        ReadLoadProfileData_Scheme2,
        ReadLoadProfileData_PQ
    };

    public enum Events
    {
        LogBook,
        MeterEventCounters,
        IndividualEvents,
        MajorAlarm,
        EventCautions,
        SecurityData
    };

    public enum Setting
    {
        ConfigurationSetings
    };

    public enum Debug
    {
        DebugPanel
    };

    // v4.8.15
    //Used for Display Tool Icons and AdvanceView Print report
    public enum GeneralRights
    {
        TCP,
        HDLC,
        Direct_HDLC,
        IP_Over_Serial,
        Parameter,
        Billing,
        Instataneous,
        LoadProfile,
        Events,
        Menufacturer,
        Device,
        Authentication,
        Password,
        SecurityControl_NoSecurity,
        SecurityControl_AutherizeKey,
        SecurityControl_EncryptKey,
        SecurityControl_AuthEncrpKey,
        SecurityAuthKey,
        SecurityEncrpKey,
        SecurityInvocationCounter,
        IgnoreReports

    };

    public enum MsnRange
    {
        CompanyCode,
        MeterType,
        MeterSerial
    };

    public enum OtherRights
    {
        IsWebFormat,
        IsMonitoringTimeCompensationInEvents,
    };

    public enum ReportFormat
    {
        WAPDA_DDS,
        WEB_GALAXY,
        ADVANCE_MTI
    };

    public enum eStatusWordWindow
    {
        AvailableStatusWordObjects,
        SelectedStatusWordObjects,
        GetStatusWordButton,
        ComboOptionStatusWordMap1,
        ComboOptionStatusWordMap2,
    };

    public enum StatusWordParam
    {
        StausWord1,
        StatusWord2
    };
    #endregion

    public enum EnergyMizer
    {
        RFConfiguration,
        WifiConfiguration,
        DisplayConfiguration,

    }

    #region Modem_Parameter

    public enum Modem
    {
        IPProfile,
        WakeUpProfile,
        NumberProfile,
        CommunicationProfile,
        KeepAlive,
        ModemInitiallization,
        ModemLimitsAndTime
    };

    public enum IPProfiles
    {
        TotalIPProfiles,
        ID,
        IP,
        WrapperOverTCPPort,
        WrapperOverUDPPort,
        HDLCOverTCPPort,
        HDLCOverUDPPort
    };

    public enum NumberProfiles
    {
        TotalNumberProfiles,
        ID,
        WakeUpOnVoiceCall,
        WakeUpOnSMS,
        WakeUpType,
        Number,
        DataCallNumber,
        VerifyPasswordflag,
        WakeUpOnSMSflag,
        WakeUpOnVoiceCallflag,
        AcceptParametersinSMsflag,
        Allow2waySMSCommunicationflag,
        Rejectwithattendflag,
        RejectCallflag,
        AcceptDataflag,
        Regular_Anonymous, //Added by Azeem Inayat
    };

    public enum WakeUpProfiles
    {
        TotalWakeUPProfile,
        ID,
        Priority1IPProfileID,
        Priority2IPProfileID,
        Priority3IPProfileID,
        Priority4IPProfileID
    };

    public enum CommunicationProfile
    {
        SMS_Mode,
        TCPLink_Mode,
        NoEventNotification_Mode,
        NumberProfileSelection,
        WakeUpID,
        Transport,
        Protocol
    };

    public enum KeepAlive
    {
        Enable,
        WakeUpProfileID,
        PingTime
    };

    public enum ModemLimit
    {
        RSSILevelConnection, RSSILevelSMS, RSSILevelDataCall,
        RetriesIPConnection,
        RetriesSMS,
        RetriesTCPdata,
        RetriesUDPdata,
        RetriesDataCall,
        TimebetweenRetriesSMS,
        TimebetweenIPConnection,
        TimebetweenRetriesUDPdata,
        TimebetweenRetriesTCPdata,
        TimebetweenRetriesDataCall,
        TimebetweenRetryAlwaysOnCycle,
        TCPInactivity,
        TimeOutCipSend
    };

    public enum ModemInitialize
    {
        APN,
        UserName,
        Password,
        WakeUpPassword,
        PINCode,
        ReleaseAssociationTCPDisconnectflag,
        DecrementEventCounterflag,
        FastDisconnectflag
    };

    public enum Modem_HDLCSetup
    {
        Get_All,
        Set_Inactivity_Timeout,
        Set_Device_Address
    };


    #endregion

    #region Standard Modem_Parameter

    public enum StandardModem
    {
        StandardIPProfile,
        StandardNumberProfile,
        StandardKeepAlive
    };

    #endregion

    #endregion
}