using Comm.DataContainers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dlmsConfigurationTool
{
    public enum QuantityName
    {
        Read_PQ = 0,
        Read_logbook = 1,
        Read_LP = 2,
        Read_CB = 3,
        Read_MB = 4,
        Read_AR = 5,
        Read_SS = 6,
        Read_CS = 7
    };

    public enum Process_Status
    {
        Default = 0,
        scheduleRead = 1,
        WakeupSent = 2,
        RequestFormatted = 3,
        Request_UnderProcess = 4,
        RequestProcessedSuccessfully = 5,
        RequestProccessedFailed = 6,
        Mute = 7
    };

    public enum Request_Status
    {
        Default = 0,
        ScheduleRead = 1,
        Assigned = 2,
        ConnectionNotAvailable = 3,
        Processed = 4
    };

    // public enum MeterType
    // {
    //     NonKeepAlive = 0,
    //     KeepAlive = 1,
    //     Intruder = 2
    // };

    #region Enums for Make Request
    public enum Pooling_Type
    {
        keepAlive = 1,
        nonKeepAlive = 0
    }

    #endregion

    #region Other Enums

    public enum ScheduleType
    {
        Disabled = 0,
        Immediate = 1,
        EveryTime = 2,
        IntervalFixed = 3,
        IntervalRandom = 4,
        SpecifiedDateTime = 5
    };
    public class Schedule
    {
        public ScheduleType SchType;
        public DateTime BaseDateTime=DateTime.Now;
        public TimeSpan Interval;
        public DateTime LastReadTime=DateTime.Now;
        public bool IsSuperImmediate = false;
        public bool Processed = false;
    }

    //public enum ScheduleType
    //{
    //    Disabled = 0,
    //    Immediate = 1,
    //    EveryTime = 2,
    //    IntervalFixed = 3,
    //    IntervalRandom = 4,
    //    SpecifiedDateTime = 5
    //};
    
    /// <summary>
    /// 0	Disable schedule if duplicate entry occurs
    /// 1	Skip the duplicate entries and insert new record
    /// 2	Override the duplicate entries and insert new record
    /// </summary>
    public enum InvalidUpdateType : byte
    {
        Disable_Schedule = 0,
        Skip_DuplicateEntry = 1,
        Override_DuplicateEntry = 2
    };

    public enum AssociationState
    {
        Login = 0,
        Logout = 1
    };

    #endregion

    #region Limits Params Thresholds

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
    #endregion

    #region Monitering Time Params Threshold
    
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
    #endregion

    public class LPChannels
    {
        public bool ChannelRequest = false;
        public bool ChangeIntervalRequest = false;
        public long Channel_1;
        public long Channel_2;
        public long Channel_3;
        public long Channel_4;
        public TimeSpan LoadProfilePeriod;

        /// <summary>
        /// Test
        /// </summary>
        // public LPChannels()
        // {
        //     Channel_1 = 845524459061503;
        //     Channel_2 = 845524475838719;
        //     Channel_3 = 1408474412220671;
        //     Channel_4 = 845525952168191;
        //     LoadProfilePeriod = TimeSpan.FromMinutes(30);
        // }
    }

    public class Counters
    {
        private long loadProfile;
        private long loadProfile_GroupID;

        private int monthlyBilling;

        private long max_load_Profile_enteries;
        private long max_events_enteries;
        private long events;

        public uint MaxLoadProfileDiffCheck = 0;
        public uint MinLoadProfileDiffCheck = 0;

        public uint MaxEventsDiffCheck = 0;
        public uint MinEventsDiffCheck = 0;

        #region Properties
        
        public long Max_events_enteries
        {
            get { return max_events_enteries; }
            set { max_events_enteries = value; }
        }

        public long Max_load_Profile_Enteries
        {
            get { return max_load_Profile_enteries; }
            set { max_load_Profile_enteries = value; }
        }
        public long LoadProfile_GroupID
        {
            get { return loadProfile_GroupID; }
            set { loadProfile_GroupID = value; }
        }
        public int MonthlyBilling_Count
        {
            get { return monthlyBilling; }
            set { monthlyBilling = value; }
        }
        public long LoadProfile_Count
        {
            get { return loadProfile; }
            set { loadProfile = value; }
        }
        public long Events_Count
        {
            get { return events; }
            set { events = value; }
        }

        #endregion

        public Counters()
        {
            loadProfile = -1;
            monthlyBilling = -1;
            max_load_Profile_enteries = 0;  // Limits.Max_LoadProfile_Count_Limit;
            events = -1;
        }

    }

    public enum UpdateType 
    {
        Schedule,
        Password,
        MDIAutoResetDate,
        Contactor,
        KeepAlive,
        ModemLimitsAndTime,
        ReferenceNo,
        Wakeup,
        None
    }

    public enum ConfigParamList : byte
    {
        LM_OverVolt = 0,
        LM_UnderVolt = 1,
        LM_ImbalanceVolt = 2,
        LM_HighNeutralCurrent,
        LM_ReverseEnergy,
        LM_TamperEnergy,
        LM_CTFailLimit,
        LM_PTFailLimit,
        LM_PTFailLimit_Volt,
        LM_OverCurrentByPhase,
        LM_MDIExceed,
        LM_Over_Load_Phase,
        LM_Over_Load_Total,
        MT_PowerFail,
        MT_PhaseFail,
        MT_OverVolt,
        MT_OverLoad,
        MT_HighNeutralCurrent,
        MT_ImbalanceVoltage,
        MT_OverCurrent,
        MT_ReversePolarity,
        MT_HallSensor,
        MT_UnderVoltage,
        MT_ReverseEnergy,
        MT_TamperEnergy,
        MT_CTFail,
        MT_PTFail,
        MT_PUDtoMonitor,
        MT_PUDforEnergyRecording,
        MT_PhaseSequence,
        ActivityCalaneder,
        WeekProfile,
        SeasonProfile,
        MDIParameters,
        MDIAutoResetDate,
        MDIIntervalTime,
        MDISlideCount,
        LoadProfile,
        CTPT_Ratio,
        DecimalPoints,
        EnergyParam,
        DisplayPowerDown,
        EnableSVSFlag,
        IPProfile,
        NumberProfile,
        KeepAlive,
        ModemLimitsAndTime,
        ModemInitialize,
        TimeBaseEvent1,
        TimeBaseEvent2,
        DisableOnPowerFailTBEs,
        Contactor,
        Debug_Error,
        Debug_Caution,
        Debug_Contactor_Status,
        Modem_Status_Information

    }

    public enum ConfigMeterEvent : ushort
    {
        ImbalanceVolt = 1,
        PhaseSequence = 2,
        ReversePolarity = 3,
        Phasefail = 4,
        UnderVolt = 5,
        OverVolt = 6,
        OverCurrent = 7,
        HighNeutralCurrent = 8,
        OverLoad = 9,
        ReverseEnergy = 10,
        TamperEnergy = 11,
        CTFail = 12,
        PTFail = 13,
        OpticalportLogin = 14,
        PowerFail = 15,
        PowerFailEnd = 16,
        OneWireTampering = 17,
        MeterOnLoad = 18,
        MeterOnLoadEnd = 19,
        UnderVoltEnd = 20,
        MDIExceed = 21,
        SystemReset = 22,
        SystemProblems = 23,
        MDIReset = 24,
        Parameters = 25,
        PasswordChange = 26,
        CustomerCode = 27,
        TimeChange = 28,
        WindowSequenseChange = 29,
        OverVoltEnd = 30,
        BillRegisterOverFlow = 31,
        ParamError = 32,
        PowerFactorChange = 33,
        BattreyLow = 34,
        DoorOpen = 35,
        ShortTimePowerFail = 36,
        RecordRecoverd = 37,
        TimeBaseEvent_1 = 38,
        TimeBaseEvent_2 = 39,
        ContactorStatusOn = 40,
        ContactorStatusOff = 41,
        ShortTimePowerFailEnd = 42,
        ReverseEnergyEnd = 43,
        TamperEnergyEnd = 44,
        OverLoadEnd = 45,
        MDIOccurance = 46,
        BillRegisterError = 47,
        PhasePhail_End = 48,
        MagneticFeild_End = 49,
        CTFail_End = 50,
        PTFail_End = 51,
        Software_Logout = 52,
        Reserved_07 = 53,
        Reserved_06 = 54,
        Reserved_05 = 55,
        Reserved_04 = 56,
        Reserved_03 = 57,
        Reserved_02 = 58,
        Reserved_01 = 59,
        Reserved_00 = 60
    }
}
