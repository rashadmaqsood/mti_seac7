using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedCode.Comm.DataContainer
{
    public enum TariffEnum : byte
    {
        Disable = 0,
        T1 = 1,
        T2 = 2,
        T3 = 3,
        T4 = 4
    }

    public enum EnergyMizerAlarmStatus : byte
    {
        Normal = 0,
        Acknowledged = 2,
        TeamsDispatched = 3,
        Processed = 4,
        Triggered,
        Transmitted,

    }
    
    public enum ModulationType : byte
    {
        FSK = 0,
        GFSK = 1,
        MSK = 2,
        GMSK = 3,
        OOK = 4
    }

    public enum PacketEncoding : byte
    {
        Manchester = 0,
        Whitening = 1
    }

    public enum PacketMode:byte
    {
        Packet = 0,
        Continuous = 1
    }

    public enum PacketFormat:byte
    {
        Variable_Length = 0,
        Fixed_Length = 1
    }

    public enum Disable_Enable : byte
    {
        Disable = 0,
        Enable  = 1
    }


    public enum USB_Parameter : byte
    {
        Disable = 0,
        GSM_Log = 1,
        SMT = 2
        //VLCD = 3
    }

    public enum Temperature_Settings : byte
    {
        Celsius = 0,
        Fahrenheit = 1
    }

    public enum WiFi_Mode : byte
    {
        Station = 0,
        Soft_AP = 1,
        Both = 3 //  Both_Station_And_SoftAP
    }

    public enum WiFi_Modem_Mode : byte
    {
        TCP_Client = 0,
        TCP_Server = 1,
        WEB_Server = 2
    }

    public enum DateTimeRequestType
    {
        SetTime,
        AdjustToMethods,
        ClockSynchMethod,
        ClockTimeShiftLimit,
        GetClockSynchMethod,
        GetClockTimeShiftLimit,
        GetPresetAdjustingTime
    }
    public enum ApplicationType : byte
    {
        MDC = 0,
        SEAC = 1,
        QC = 2,
    }
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

    public enum MeterType
    {
        NonKeepAlive = 0,
        KeepAlive = 1,
        Intruder = 2
    };

    public enum DeviceType : byte
    {
        Not_Assigned = 0,
        MeterDevice = 1,
        GateWay = 2,
        eGenious = 3,
    }

    public enum LLCProtocolType : byte
    {
        Not_Assigned = 0,
        TCP_Wrapper = 1,
        Direct_HDLC = 2,
        PG_BOTH = 3
    }

    public enum Scheduler_Type : byte
    {
        Not_Assigned = 0,
        /// <summary>
        /// Batch Processor;Meters With Large Data Items To Read/Write will be given Preference
        /// </summary>
        BatchProcessor = 1,
        /// <summary>
        /// Shortest Schedule Tasks Processor;Meters With Smallest Data Items To Read/Write will be given Preference
        /// </summary>
        ShortestSchTasksProcessor = 2,
        /// <summary>
        /// KasSchDueTime;General Keep Alive Scheduler Due Time For All Meters
        /// </summary>
        KasSchDueTime = 3,
        // More Scheduler Types
        /// <summary>
        /// Tasks Success Rate;Over All, Meter Previous Data Read/Write Success Ratio
        /// </summary>
        TasksSuccessRate = 4,

        /// <summary>
        /// Schedule Tasks Due Time;Meters with Older Due Time will be given Preference For PQ,Cum,Ev ETC
        /// </summary>
        SchTaskDueTime = 5,
        /// <summary>
        /// Last Task Execution Time;Meters with older Data Read Time Will be given Preference For PQ,Cum,Ev ETC
        /// </summary>
        LastTaskExecutionTime = 6,
        /// <summary>
        /// On Demand Reading;Only Meters with High Super Immediate Flags will be read
        /// </summary>
        OnDemandReading = 7,

    }

    public enum BillingMethods : byte
    {
        OneGetMethod = 1,
        Method2,
        Method3,
        Method4,
        GetByQuantity,
        CustomMethod
    }

    #region Enums for Make Request
    public enum Pooling_Type
    {
        keepAlive = 1,
        nonKeepAlive = 0
    }

    #endregion

    #region Other Enums
    
    public class Schedule
    {
        public ScheduleType SchType;
        public DateTime BaseDateTime = DateTime.Now;
        public TimeSpan Interval;
        public DateTime LastReadTime = DateTime.Now;
        public bool IsSuperImmediate = false;
        public bool Processed = false;

        #region Update Schedule Flags
        public bool UpdateLastReadTime = false;
        public bool UpdateScheduleType = false;
        public bool UpdateIsSuperImmediate = false;
        public bool UpdateBaseTime = false;
        public bool IsDisable = false;
        #endregion
    }

    public enum ScheduleType
    {
        Disabled = 0,
        Immediate = 1,
        EveryTime = 2,
        IntervalFixed = 3,
        IntervalRandom = 4,
        SpecifiedDateTime = 5
    };

    public enum Schedules : int
    {
        CumulativeBilling = 1,
        SignalStrength = 2,
        PowerQuantities = 3,
        Events = 4,
        MonthlyBilling = 5,
        LoadProfile = 6,
        PerameterizationWrite = 7,
        ParamterizationRead=8,
        RemoteGrid=9,
        LoadProfile2 = 10,
        DailyLoadProfile = 11,
        ReadContactorStatus = 12,
    }

    public enum AssociationState:byte
    {
        Login = 0,
        Logout = 1
    };

    #endregion

    //#region Limits Params Thresholds

    //[Serializable]
    //public enum ThreshouldItem_o : byte
    //{
    //    OverVolt = 0,
    //    UnderVolt,

    //    ImbalanceVolt,
    //    HighNeutralCurrent,

    //    ReverseEnergy,
    //    TamperEnergy,

    //    CTFail,
    //    PTFail_Amp,
    //    PTFail_Volt,

    //    OverCurrentByPhase_T1,
    //    OverCurrentByPhase_T2,
    //    OverCurrentByPhase_T3,
    //    OverCurrentByPhase_T4,

    //    OverLoadByPhase_T1,
    //    OverLoadByPhase_T2,
    //    OverLoadByPhase_T3,
    //    OverLoadByPhase_T4,

    //    OverLoadTotal_T1,
    //    OverLoadTotal_T2,
    //    OverLoadTotal_T3,
    //    OverLoadTotal_T4,

    //    MDIExceed_T1,
    //    MDIExceed_T2,
    //    MDIExceed_T3,
    //    MDIExceed_T4
    //}
    //#endregion

    //#region Monitering Time Params Threshold
    //[Serializable]
    //public enum MonitoringTimeItem : byte
    //{
    //    OverVolt = 0,
    //    UnderVolt,

    //    ImbalanceVolt,
    //    HighNeutralCurrent,

    //    ReverseEnergy,
    //    TamperEnergy,
    //    ReversePolarity,

    //    CTFail,
    //    PTFail,

    //    OverCurrent,
    //    OverLoad,

    //    PowerFail,
    //    PhaseFail,
    //    PhaseSequence,

    //    PowerUPDelay,
    //    PowerUpDelayEnergyRecording,
    //    PowerUpDelayEarth,

    //    Earth,
    //    HALLSensor
    //}
    //#endregion

    #region Error Codes

    public enum MDCErrors : int
    {
        #region Application Layer
        //Application Layer Error range is 1000-1999
        App_Private_Login = 1000,
        App_AlarmRegister_Read = 1001,
        App_AlarmRegister_Reset = 1002,
        App_AlarmRegister_Save = 1003,
        App_Association_Release = 1004,
        App_Cum_Billing_Read = 1005,
        App_Cum_Billing_Save = 1006,
        App_Signal_Strength_Read = 1007,
        App_Instantaneous_Data_Read = 1008,
        App_Instantaneous_Data_Save = 1009,
        App_Events_Data_Read = 1010,
        App_Events_Data_Save = 1011,
        App_Individual_Events_Data_Read = 1012,
        App_Individual_Events_Data_Save = 1013,
        App_Monthly_Billing_Read = 1014,
        App_Monthly_Billing_Save = 1015,
        App_Load_Profile_Read = 1016,
        App_Load_Profile_Save = 1017,
        App_Load_Profile_Channels_GroupID = 1018,
        App_RemoteGridStatus = 1019,
        App_Load_Profile2_Read = 1020,

        //TODO Parameterization


        #endregion

        #region DLMS Layer
        //DLMS Layer Error range is 2000-2999
        #endregion

        #region DataBase Layer
        //DataBase Layer Error range is 3000-3999
        DB_Duplicate_Entery = 3000,

        #endregion

        #region Physical Layer
        //Physical Layer Error range is 4000-4999
        Ph_Reset_Stream = 4000,
        #endregion
    }

    #endregion

    #region Paramters To Read
    public enum ParamList : byte
    {
        LM_OverVolt = 0,
        LM_UnderVolt=1,
        LM_ImbalanceVolt=2,
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
        Modem_Status_Information,
        dummy_param1,
        dummy_param2,
        dummy_param3,
        dummy_param4,
        dummy_param5,
        dummy_param6,
        dummy_param7,
        dummy_param8,

    }
    #endregion

    #region Parameters To Write
    public enum WriteParams :int
    {
        SetContactorParam=0,
        SetModemLimitsAndTime=1,
        TimeSync=2,
        SetIPProfile,
        SetKeepAlive,
        SetModemInit,
        SetTimeBaseEvents,
        SetDisableTBEsFlag,
        SetMajorAlarm,
        UnsetMajorAlarm,
        SetLoadProfileInterval,
        SetLoadProfileChannel,
        SetDisplayWindows,
        SetPassword,
        SetMDIResetDate,
        SetCustomerReferenceNo,
        SetWakeupProfiles,
        SetNumberProfile,
        SetDisplayPowerDown,
        SetLimits,
        SetMonitoringTime,
        SetCTPT,
        SetDecimalPoints,
        SetEnergyParam,
        RemoteGridConfig,
        SetLoadSchedule,
        SetConsumptionDataNow,
        SetConsumptionDataWeekly,
        SetConsumptionDataMonthly
    }
    #endregion
}
