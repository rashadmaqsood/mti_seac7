using System;

namespace SharedCode.Comm.HelperClasses
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
        MDIExceed_T4,

        GeneratorStart,
        //for Single Phase  //Azeem
        OverCurrent_Phase,
        Meter_ON_Load,
        PowerFactor_Change,
        CrestFactorLow,
        CrestFactorHigh,
        OverPower,
        ContactorFailure
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
        HALLSensor,
        Contactor_failure,
    }

    //[Serializable]
    //public enum EnergyMizerItem : byte
    //{

    //    #region -- RF_Configuration --

    //    RF_Channel = 0,
    //    ChannelFilterWB,
    //    TransmitCarrierFrequency,
    //    ReceiveCarrierFrequency,
    //    RFBaudRate,
    //    RFPower,
    //    PacketMode,
    //    ContinuesMode,
    //    FrequentDeviation,
    //    ReceiverBandwidth,
    //    CostParameter,

    //    Preamble,
    //    SyncWord,
    //    AddressFiltering,
    //    NodeAddress,
    //    BroadcastAddress,
    //    AESEncryption,
    //    RFCommandDelay,
    //    RFCommandTimeout,
    //    ModulationType,
    //    PacketEncoding,
    //    Temperature,

    //    #endregion //-- RF_Configuration --

    //    #region -- Display_Configuration 

    //    SerialNumber,
    //    LCDContrast,
    //    MeterToRead,
    //    MeterPassword,
    //    DataToRead,
    //    BuzzerSetting,
    //    Readhumidity,
    //    TemperatureSettings,
    //    USBParameters,

    //    #endregion // -- Display_Configuration --

    //    #region -- WiFi_Configuration --

    //    WifiWebServerConfigurationIP,
    //    WifiWebServerConfigurationPort,
    //    WifiServerIP,
    //    WifiServerPort,

    //    WifiBasicConfiguration,
    //    WifiClientIP,
    //    WifiClientPort

    //    #endregion // -- WiFi_Configuration --
        
    //}

}
