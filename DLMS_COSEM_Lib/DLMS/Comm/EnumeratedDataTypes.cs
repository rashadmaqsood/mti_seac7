using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLMS.Comm
{
    /// <summary>
    /// Specify the unit associated with object data
    /// </summary>
    public enum units : byte
    {
        _B01_year = 0x01,
        _B02_month = 0x02,
        _B03_week = 0x03,
        _B04_day = 0x04,
        _B05_hour = 0x05,
        _B06_minute = 0x06,
        _B07_second = 0x07,
        _B08_phase_angle = 0x08,
        _B09_temperature = 0x09,
        _B1B_active_power_WATT = 0x1B,
        _B1C_apparent_power_VA = 0x1C,
        _B1D_Reactive_power_VAR = 0x1D,
        _B1E_active_energy_watt_hour = 0x1E,
        _B1F_apparent_energy = 0x1F,
        _B20_reative_energy = 0x20,
        _B21_current = 0x21,
        _B23_voltage = 0x23,
        _B2C_frequncy = 0x2C,
        _BFF_count_unitless = 0xFF,
    }

    public enum SortMethod : byte
    {
        FIFO = 0x01,
        LIFO = 0x02,
        LARGEST = 0x03,
        SMALLEST = 0x04,
        NEAREST_TO_ZERO = 0x05,
        FAREST_FROM_ZERO = 0x06,
    }

    /// <summary>
    /// Specify the types of data/values being use with DLMS/COSEM class object
    /// </summary>
    public enum DataTypes : byte
    {
        _A00_Null = 0x00,
        _A01_array = 0x01,
        _A02_structure = 0x02,
        _A03_boolean = 0x03,
        _A04_bit_string = 0x04,
        _A05_double_long = 0x05,
        _A06_double_long_unsigned = 0x06,
        _A07_floating_point = 0x07,
        _A09_octet_string = 0x09,
        _A0A_visible_string = 0x0A,
        _A0C_utf8_string = 0x0C,

        _A0D_bcd = 0x0D,
        _A0F_integer = 0x0F,
        _A10_long = 0x10,
        _A11_unsigned = 0x11,
        _A12_long_unsigned = 0x12,
        _A13_compact_arry = 0x13,
        _A14_long_64 = 0x14,
        _A15_long_64_unsigned = 0x15,
        _A16_enum = 0x16,

        _A19_datetime = 0x19,
        _A1A_date = 0x1A,
        _A1B_time = 0x1B,

        _A23_Float32 = 0x23,
        _A24_Float64 = 0x24,
        _AC9_Arry_of_structures = 0xC9,
        _AFA_dont_care = 0xFA,
    }

    public enum Sort_Method
    {
        /// <summary>
        /// FIFO(first in first out)
        /// </summary>
        FIFO,
        /// <summary>
        /// LIFO (last in first out),
        /// </summary>
        LIFO,
        /// <summary>
        /// LARGEST (Largest In Sequence Comes First)
        /// </summary>
        LARGEST,
        /// <summary>
        /// SMALLEST (Smallest In Sequence Comes First)
        /// </summary>
        SMALLEST,
        /// <summary>
        ///  Nearest_To_Zero (Nearest_To_Zero In Sequence Comes First)
        /// </summary>
        Nearest_To_Zero,
        /// <summary>
        ///  Farest_From_Zero (Farest_From_Zero In Sequence Comes First)
        /// </summary>
        Farest_From_Zero
    }

    public enum AssociationStatus : byte
    {
        Non_associated = 0,
        Association_pending = 1,
        Associated = 2
    }

    public enum AssociationResult : byte
    {
        Accepted = 0,
        RejectedPermanent = 1,
        RejectedTransient = 2
    }

    public enum HLS_Mechanism : byte
    {
        LowestSec = 0,
        LowSec = 1,
        HLS_Manufac = 2,
        HLS_MD5 = 3,
        HLS_SHA1 = 4,
        HLS_GMAC = 5,
        HLS_SHA2 = 6,
        HLS_ECDSA = 7
    }

    public enum Application_ContextName : byte
    {
        LN_Referencing_No_Ciphering = 1,
        SN_Referencing_No_Ciphering = 2,
        LN_Referencing_With_Ciphering = 3,
        SN_Referencing_With_Ciphering = 4
    }

    public enum Result_SourceDiagnostic : byte
    {
        /// <summary>
        /// OK
        /// </summary>
        None = 0,
        
        /// <summary>
        /// No reason is given
        /// </summary>
        NoReasonGiven = 1,
        
        /// <summary>
        /// The application context name is not supported 
        /// </summary>
        ApplicationContextNameNotSupported = 2,

        Calling_AP_TitleNotRecognized = 3,
        Calling_AP_InvocationIdentifierNotRecognized = 4,
        Calling_AE_QualifierNotRecognized = 5,
        Calling_AE_InvocationIdentifierNotRecognized = 6,
        
        Called_AP_TitleNotRecognized = 7,
        Called_AP_Invocation_IdentifierNotRecognized = 8,
        Called_AE_QualifierNotRecognized = 9,
        Called_AE_Invocation_IdentifierNotRecognized = 10,

        /// <summary>
        /// The authentication mechanism name is not recognized
        /// </summary>
        AuthenticationMechanismNameNotRecognised = 0x0B,
        
        /// <summary>
        /// Authentication Mechanism Name is Required
        /// </summary>
        AuthenticationMechanismNameReguired = 0x0C,
        
        /// <summary>
        /// Authentication Failure
        /// </summary>
        AuthenticationFailure = 0x0D,
        
        /// <summary>
        /// Authentication is Required,HLS_Success = 0x0E
        /// </summary>
        AuthenticationRequired = 0x0E
    }

    public enum SecurityControl : byte
    {
        None = 0x00,
        AuthenticationOnly = 0x10,
        EncryptionOnly = 0x20,
        AuthenticationAndEncryption = 0x30,
        InvalidValue = 0x40
    }

    public enum KEY_ID : byte
    {
        GLOBAL_Unicast_EncryptionKey = 0x00,
        GLOBAL_Broadcast_EncryptionKey = 0x01,
        AuthenticationKey = 0x02,
        MasterKey = 0x03,

        HLS_Secret = 0xFF
    }

    /// <summary>
    /// Control state 
    /// </summary>
    public enum ControlState : byte
    {
        Disconnected = 0,
        Connected = 1,
        ReadyForReconnection = 2
    }

    public enum Security_Policy : byte
    {
        Nothing = 0x00,
        Authenticated = 0x01,
        Encrypted = 0x02,
        AuthenticatedEncrypted = 0x03
    }

    public enum Security_Suite : byte
    {
        Val_0 = 0x00,
        Val_1 = 0x01,
        Val_2 = 0x02,
        Val_3 = 0x03
    }

    /// <summary>
    /// Defines the mode controlling the auto dial functionality concerning the timing.
    /// </summary>
    public enum AutoConnectMode : byte
    {
        /// <summary>
        /// No auto dialling,
        /// </summary>
        NoAutoDialling = 0,
        /// <summary>
        /// Auto dialling allowed anytime,
        /// </summary>
        AutoDiallingAllowedAnytime = 1,
        /// <summary>
        /// Auto dialling allowed within the validity time of the calling window.
        /// </summary>
        AutoDiallingAllowedCallingWindow = 2,
        /// <summary>
        /// “regular” auto dialling allowed within the validity time
        /// of the calling window; “alarm” initiated auto dialling allowed anytime,
        /// </summary>        
        RegularAutoDiallingAllowedCallingWindow = 3,
        /// <summary>
        /// SMS sending via Public Land Mobile Network (PLMN),
        /// </summary>
        SmsSendingPlmn = 4,
        /// <summary>
        /// SMS sending via PSTN.
        /// </summary>
        SmsSendingPstn = 5,
        /// <summary>
        /// Email sending.
        /// </summary>
        EmailSending = 6,

        //... Version 2

        /// <summary>
        /// Permanently Connected to the Communication network i.e GPRS
        /// </summary>
        PermanentConnectionAlways = 101,

        /// <summary>
        /// Permanently connected to the communication network i.e GPRS within the validity time of the calling window. 
        /// The device is disconnected outside the calling window. No connection possible outside the calling window,
        /// </summary>
        PermanentConnectionAllowedCallingWindow = 102,

        /// <summary>
        /// Permanently connected to the communication network i.e GPRS within the validity time of the calling window. 
        /// The device is disconnected outside the calling window. No connection possible outside the calling window,
        /// but it connects to the communication network as soon as the connect method is invoked,
        /// </summary>
        ManualInvokeConnectAndPermanentConnectionAllowedCallingWindow = 103,

        /// <summary>
        /// Usually disconnected,It connects to the communication network i.e GPRS as soon as the Connect method is invoked,
        /// </summary>
        ManualInvokeConnect = 104

        // (200..255) manufacturer specific modes
    }

    /// <summary>
    /// Define the mode controlling the auto dial functionality concerning the timing.
    /// </summary>
    public enum AutoAnswerMode
    {
        /// <summary>
        /// line dedicated to the device,With Unlimited Wakeup Calls Are Allowed
        /// </summary>
        UnlimitedAutoDial = 0,
        /// <summary>
        /// Shared line management with a limited number of calls allowed. 
        /// Once the number of calls is reached, whatever the result of the call
        /// The window status becomes inactive until the next start date,
        /// </summary>
        LimitedWakeupCallsAllowed,
        /// <summary>
        /// Shared line management with a limited number of successful calls allowed. 
        /// Once the number of successful communications is reached, 
        /// the window status becomes inactive until the next start date,
        /// </summary>
        LimitedSuccessfulWakeupsAllowed,
        /// <summary>
        /// currently no modem connected,
        /// </summary>
        NoModemInstall,

        // (200..255) manufacturer specific modes
    }

    public enum AutoAnswerStatus
    {
        /// <summary>
        /// The device will manage no new incoming call. 
        /// This status is automatically reset to Active when the next listening window starts,
        /// </summary>
        Inactive = 0,
        /// <summary>
        /// The device can answer to the next incoming call.
        /// </summary>
        Active = 1,
        /// <summary>
        /// This value can be set automatically by the device or by a specific client when this client has
        /// completed its reading session and wants to give the line back to the customer before the end of the
        /// window duration. This status is automatically reset to Active when the next listening window starts.
        /// </summary>
        Locked = 2
    }

    public enum AutoAnswerCallerType
    {
        /// <summary>
        /// Normal Wakeup calls Only; the modem only connects if the calling number matches an entry in the list. 
        /// This is tested in addition to all other attributes, e.g. number_of_rings, listening_windows, etc
        /// </summary>
        WakeupCallsOnly = 0,
        /// <summary>
        /// Wake-up request; calls or messages from this calling number are handled as wake-up requests.
        /// </summary>
        WakeupSmsAndCalls = 1,
    }

    public enum SingleActionScheduleType
    {
        /// <summary>
        /// Size of execution_time = 1. Wildcard in date allowed.
        /// </summary>
        SingleActionScheduleType1,
        /// <summary>
        /// Size of execution_time = n. 
        /// All time values are the same, wildcards in date not allowed.
        /// </summary>
        SingleActionScheduleType2,
        /// <summary>
        /// Size of execution_time = n. 
        /// All time values are the same, wildcards in date are allowed,
        /// </summary>
        SingleActionScheduleType3,
        /// <summary>
        /// Size of execution_time = n.
        /// Time values may be different, wildcards in date not allowed,
        /// </summary>
        SingleActionScheduleType4,
        /// <summary>
        /// Size of execution_time = n.
        /// Time values may be different, wildcards in date are allowed
        /// </summary>
        SingleActionScheduleType5
    }

    public enum ScriptActionType
    {
        /// <summary>
        /// Nothing is going to execute.
        /// </summary>
        None = 0,
        /// <summary>
        /// Write attribute.
        /// </summary>
        Write = 1,
        /// <summary>
        /// Execute specific method
        /// </summary>
        Execute = 2
    }

    public enum CommSpeed : byte
    {
        _300 = 0,
        _600 = 1,
        _1200 = 2,
        _2400 = 3,
        _4800 = 4,
        _9600 = 5,
        _19200 = 6,
        _38400 = 7,
        _57600 = 8,
        _115200 = 9
    }

    public enum Protocol_Gate : byte
    {
        PG_TCP = 1,
        PG_HDLC,
        PG_BOTH
    };

    public enum DLMSErrors : int
    {
        Invalid_DecodingAttribute = 2000,
        Invalid_EncodingAttribute = 2001,
        Invalid_MethodInvokeId = 2002,
        Invalid_OBISCode = 2003,
        Invalid_ObjectReference = 2004,

        Invalid_Type_MisMatch = 2010,
        Invalid_TypeNotIncluded = 2011,
        Invalid_DataLength = 2012,
        Invalid_DataValue = 2013,
        Invalid_DecoderNotIncluded = 2014,
        Invalid_EncoderNotIncluded = 2015,

        ErrorDecoding_Type = 2020,
        ErrorEncoding_Type = 2021,

        ServiceFailure_GET = 2022,
        ServiceFailure_SET = 2023,
        ServiceFailure_Action = 2024,
        ServiceFailure_AARQ = 2025,

        Password_Error = 2026,

        Invalid_BlockNumber = 2027,
        Invalid_LoginStatus = 2028,
        Insufficient_Priviledge = 2029,

        Invalid_AuthenticationTAG = 2050,
        Invalid_HLS_LoginStatus = 2051,
        Invalid_SecurityData_EK = 2052,
        Invalid_SecurityData_AK = 2053,
        Invalid_SecurityData_SystemTitle = 2054,

        Invalid_KeyWrap = 2055,
        Invalid_KeyUnWrap = 2056,
        Invalid_KeyLengthSuport = 2057,
        Invalid_SecurityHeader = 2058,
        Invalid_SecurityControl = 2059,
        Invalid_FrameCounter = 2060,


        Invalid_InvokeIDPriority,
        IOChannel_Disconnected
    }

    #region Clock Synchronization Methods

    public enum Clock_Synchronization_Method : byte
    {
        /// <summary>
        /// no synchronization
        /// </summary>
        NO_SYNCHRONIZATION = 0,
        /// <summary>
        /// adjust to quarter
        /// </summary>
        ADJUST_TO_QUARTER,
        /// <summary>
        /// adjust to measuring period
        /// </summary>
        ADJUST_TO_MEASURING_PERIOD,
        /// <summary>
        /// adjust to minute
        /// </summary>
        ADJUST_TO_MINUTE,
        /// <summary>
        ///  reserved
        /// </summary>
        RESERVED,
        /// <summary>
        ///  adjust to preset time
        /// </summary>
        ADJUST_TO_PRESET_TIME,
        /// <summary>
        ///  shift time
        /// </summary>
        SHIFT_TIME
    }

    #endregion // Clock Synchronization Methods

}
