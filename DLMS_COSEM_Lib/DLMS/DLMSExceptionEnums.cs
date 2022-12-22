using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DLMS
{
    /// <summary>
    /// ServiceError Enumerates Service Errors
    /// </summary>
    public enum ServiceError : byte
    {
        /// <summary>
        /// Application error.
        /// </summary>
        ApplicationReference = 0,
        /// <summary>
        /// Hardware error.
        /// </summary>
        HardwareResource,
        /// <summary>
        /// Vde state error.
        /// </summary>
        VdeStateError,
        /// <summary>
        /// Service error.
        /// </summary>
        Service,
        /// <summary>
        /// Definition error.
        /// </summary>
        Definition,
        /// <summary>
        /// Access error.
        /// </summary>
        Access,
        /// <summary>
        /// Initiate error.
        /// </summary>
        Initiate = 6,
        /// <summary>
        /// LoadDataSet error.
        /// </summary>
        LoadDataSet,
        /// <summary>
        /// Task error.
        /// </summary>
        Task,
        /// <summary>
        /// Other error describes manufacturer specific error code.
        /// </summary>
        OtherError
    }

    /// <summary>
    /// Confirmed service error tells when error has occurred.
    /// </summary>
    public enum ConfirmedServiceError : byte
    {
        /// <summary>
        /// No Value Applicable
        /// </summary>
        NoValue = 0,
        /// <summary>
        /// Error has occurred on initialize.
        /// </summary>
        InitiateError = 1,         // ServiceError,

        getStatus = 2,             // ServiceError,
        getNameList = 3,           // ServiceError,
        getVariableAttribute = 4,  // ServiceError,

        /// <summary>
        /// Error has occurred on read.
        /// </summary>
        Read = 5,                  // ServiceError,
        /// <summary>
        /// Error has occurred on write.
        /// </summary>
        Write = 6,                 // ServiceError,

        getDataSetAttribute = 7,   // ServiceError,
        getTIAttribute = 8,        // ServiceError,
        changeScope = 9,           // ServiceError,
        start = 10,                // ServiceError,
        stop = 11,                 // ServiceError,
        resume = 12,               // ServiceError,
        makeUsable = 13,           // ServiceError,
        initiateLoad = 14,         // ServiceError,
        loadSegment = 15,          // ServiceError,
        terminateLoad = 16,        // ServiceError,
        initiateUpLoad = 17,       // ServiceError,
        upLoadSegment = 18,         // ServiceError,
        terminateUpLoad = 19        // ServiceError
    }

    /// <summary>
    /// ServiceError Enumerates Exception Service Errors.
    /// </summary>
    public enum ExceptionServiceError : byte
    {
        /// <summary>
        /// No Value Applicable
        /// </summary>
        NoValue = 0,
        /// <summary>
        /// Operation not possible.
        /// </summary>
        OperationNotPossible = 1,
        /// <summary>
        /// Service not supported.
        /// </summary>
        ServiceNotSupported = 2,
        /// <summary>
        /// Other reason.
        /// </summary>
        OtherReason = 3,

        // Non Standard MTL Codes Used Here

        /// <summary>
        /// Security Control Byte Mismatch
        /// </summary>
        /// <remarks>MTL Non Standard Err Codes</remarks>
        SC_Mismatch = 4,
        /// <summary>
        /// Frame Counter Mismatch
        /// </summary>
        /// <remarks>MTL Non Standard Err Codes</remarks>
        FC_Mismatch = 5,
        /// <summary>
        /// Authentication Tag_Mismatch
        /// </summary>
        /// <remarks>MTL Non Standard Err Codes</remarks>
        AuthTag_Mismatch = 6,
        /// <summary>
        /// AARQ_Received When Meter is Connected
        /// </summary>
        /// <remarks>MTL Non Standard Err Codes</remarks>
        MC_AARQRequest_Received = 7,
        /// <summary>
        /// Get Request Received When Meter is Disconnected
        /// </summary>
        /// <remarks>MTL Non Standard Err Codes</remarks>
        MD_GetRequest_Received = 8,
        /// <summary>
        /// Set Request Received When Meter is Disconnected
        /// </summary>
        /// <remarks>MTL Non Standard Err Codes</remarks>
        MD_SetRequest_Received = 9,
        /// <summary>
        /// Act Request Received When Meter is Disconnected
        /// </summary>
        /// <remarks>MTL Non Standard Err Codes</remarks>
        MD_ActRequest_Received = 10,
        /// <summary>
        /// Meter SAP Value Received Mismatch When Meter is Connected
        /// </summary>
        /// <remarks>MTL Non Standard Err Codes</remarks>
        MC_SAPValue_Mismatch = 11,
        /// <summary>
        /// Secure APDU With General Protection Cyphering Mode is Expected
        /// </summary>
        /// <remarks>MTL Non Standard Err Codes</remarks>
        GeneralProtection_Expected = 12,
        /// <summary>
        /// Secure APDU With Service Specific Cyphering Mode is Expected
        /// </summary>
        /// <remarks>MTL Non Standard Err Codes</remarks>
        ServiceSpecific_Expected = 13,
        /// <summary>
        /// Invalid System Title Length Received in Secure APDU
        /// </summary>
        /// <remarks>MTL Non Standard Err Codes</remarks>
        Invalid_SystemTitle_Length = 14,
        /// <summary>
        /// System Title Received is Mismatched in Secure APDU
        /// </summary>
        /// <remarks>MTL Non Standard Err Codes</remarks>
        SystemTitle_Mismatch = 15,
        /// <summary>
        /// Reply To HLS (3rd Invoke Message Pass 2 Way Hand-Shake)  in HLS Authentication Required
        /// </summary>
        /// <remarks>MTL Non Standard Err Codes</remarks>
        ReplyToHLS_Required = 16
    }

    /// <summary>
    /// StateError enumerates state errors.
    /// </summary>
    public enum ExceptionStateError : byte
    {
        /// <summary>
        /// No Value Applicable
        /// </summary>
        [XmlEnum("0")]
        NoValue = 0,
        [XmlEnum("1")]
        ServiceNotAllowed = 1,
        [XmlEnum("2")]
        ServiceUnknown = 2
    }



    /// <summary>
    /// Application reference describes application errors.
    /// </summary>
    public enum ApplicationReference : byte
    {
        /// <summary>
        /// Other error is occurred.
        /// </summary>
        [XmlEnum("0")]
        Other = 0,
        /// <summary>
        /// Time elapsed.
        /// </summary>
        [XmlEnum("1")]
        TimeElapsed,
        /// <summary>
        /// Application unreachable.
        /// </summary>
        [XmlEnum("2")]
        ApplicationUnreachable,
        /// <summary>
        /// Application reference is invalid.
        /// </summary>
        [XmlEnum("3")]
        ApplicationReferenceInvalid,
        /// <summary>
        /// Application context unsupported.
        /// </summary>
        [XmlEnum("4")]
        ApplicationContextUnsupported,
        /// <summary>
        /// Provider communication error.
        /// </summary>
        [XmlEnum("5")]
        ProviderCommunicationError,
        /// <summary>
        /// Deciphering error.
        /// </summary>
        [XmlEnum("6")]
        DecipheringError,

        // Non Standard Application Service Error

        /// <summary>
        /// glo-initiateRequest Tag Mismatch
        /// </summary>
        [XmlEnum("7")]
        glo_initiateRequestTagMismatch = 7,


        /// <summary>
        /// Security Control Byte Mismatch
        /// </summary>
        [XmlEnum("8")]
        SCMismatch = 8,

        /// <summary>
        /// Frame Counter Byte Mismatch
        /// </summary>
        [XmlEnum("9")]
        FCMismatch = 9,

        /// <summary>
        /// AuthenticationTAG Mismatch
        /// </summary>
        [XmlEnum("10")]
        AuthenticationTagMismatch = 10,

        /// <summary>
        /// AuthenticationTAG Mismatch
        /// </summary>
        [XmlEnum("11")]
        InitiateRequestTagMismatch = 11,
    }

    /// <summary>
    /// Hardware resource describes hardware errors.
    /// </summary>
    public enum HardwareResource : byte
    {
        /// <summary>
        /// Other hardware resource error.
        /// </summary>
        [XmlEnum("0")]
        Other = 0,
        /// <summary>
        /// Memory is unavailable.
        /// </summary>
        [XmlEnum("1")]
        MemoryUnavailable,
        /// <summary>
        /// Processor resource is unavailable.
        /// </summary>
        [XmlEnum("2")]
        ProcessorResourceUnavailable,
        /// <summary>
        /// Mass storage is unavailable.
        /// </summary>
        [XmlEnum("3")]
        MassStorageUnavailable,
        /// <summary>
        /// Other resource is unavailable.
        /// </summary>
        [XmlEnum("4")]
        OtherResourceUnavailable
    }

    /// <summary>
    /// VdeState error describes Vde state errors.
    /// </summary>
    public enum VdeStateError : byte
    {
        /// <summary>
        /// Other error.
        /// </summary>
        [XmlEnum("0")]
        Other = 0,
        /// <summary>
        /// No DLMS context.
        /// </summary>
        [XmlEnum("1")]
        NoDlmsContext,
        /// <summary>
        /// Loading dataset error.
        /// </summary>
        [XmlEnum("2")]
        LoadingDataSet,
        /// <summary>
        /// Status No change.
        /// </summary>
        [XmlEnum("3")]
        StatusNochange,
        /// <summary>
        /// Status Inoperable.
        /// </summary>
        [XmlEnum("4")]
        StatusInoperable
    }

    /// <summary>
    /// Service describes service errors.
    /// </summary>
    public enum Service : byte
    {
        /// <summary>
        /// Other error.
        /// </summary>
        [XmlEnum("0")]
        Other = 0,
        /// <summary>
        /// PDU size is wrong.
        /// </summary>
        [XmlEnum("1")]
        PduSize,
        /// <summary>
        /// Service is unsupported.
        /// </summary>
        [XmlEnum("2")]
        ServiceUnsupported
    }

    /// <summary>
    /// Definition describes definition errors.
    /// </summary>
    public enum Definition : byte
    {
        /// <summary>
        /// Other error.
        /// </summary>
        [XmlEnum("0")]
        Other = 0,
        /// <summary>
        /// Object is Undefined.
        /// </summary>
        [XmlEnum("1")]
        ObjectUndefined,
        /// <summary>
        /// Object class inconsistent.
        /// </summary>
        [XmlEnum("2")]
        ObjectClassInconsistent,
        /// <summary>
        /// Object attribute inconsistent.
        /// </summary>
        [XmlEnum("3")]
        ObjectAttributeInconsistent
    }

    /// <summary>
    /// Access describes access errors.
    /// </summary>
    public enum Access : byte
    {
        /// <summary>
        /// Other error.
        /// </summary>
        [XmlEnum("0")]
        Other = 0,
        /// <summary>
        /// Scope of access violated.
        /// </summary>
        [XmlEnum("1")]
        ScopeOfAccessViolated,
        /// <summary>
        /// Object access is invalid.
        /// </summary>
        [XmlEnum("2")]
        ObjectAccessInvalid,
        /// <summary>
        /// Hardware fault.
        /// </summary>
        [XmlEnum("3")]
        HardwareFault,
        /// <summary>
        /// Object is unavailable.
        /// </summary>
        [XmlEnum("4")]
        ObjectUnavailable
    }

    /// <summary>
    /// Initiate describes on initiate errors.
    /// </summary>
    public enum Initiate : byte
    {
        /// <summary>
        /// Other error.
        /// </summary>
        [XmlEnum("0")]
        Other = 0,
        /// <summary>
        /// Dlms version is too low.
        /// </summary>
        [XmlEnum("1")]
        DlmsVersionTooLow,
        /// <summary>
        /// Incompatible conformance.
        /// </summary>
        [XmlEnum("2")]
        IncompatibleConformance,
        /// <summary>
        /// Pdu size is too short.
        /// </summary>
        [XmlEnum("3")]
        PduSizeTooShort,
        /// <summary>
        /// Refused by the VDE handler.
        /// </summary>
        [XmlEnum("4")]
        RefusedByTheVDEHandler

    }

    /// <summary>
    /// LoadDataSet describes load dataset errors.
    /// </summary>
    public enum LoadDataSet
    {
        /// <summary>
        /// Other error.
        /// </summary>
        [XmlEnum("0")]
        Other = 0,
        /// <summary>
        /// Primitive out of sequence.
        /// </summary>
        [XmlEnum("1")]
        PrimitiveOutOfSequence,
        /// <summary>
        /// Not loadable.
        /// </summary>
        [XmlEnum("2")]
        NotLoadable,
        /// <summary>
        /// Dataset size is too large.
        /// </summary>
        [XmlEnum("3")]
        DatasetSizeTooLarge,
        /// <summary>
        /// Not awaited segment.
        /// </summary>
        [XmlEnum("4")]
        NotAwaitedSegment,
        /// <summary>
        /// Interpretation failure.
        /// </summary>
        [XmlEnum("5")]
        InterpretationFailure,
        /// <summary>
        /// Storage failure.
        /// </summary>
        [XmlEnum("6")]
        StorageFailure,
        /// <summary>
        /// Dataset not ready.
        /// </summary>
        [XmlEnum("7")]
        DatasetNotReady
    }

    /// <summary>
    /// Task describes load task errors.
    /// </summary>
    public enum TaskErrs : byte
    {
        /// <summary>
        /// Other error.
        /// </summary>
        [XmlEnum("0")]
        Other = 0,
        /// <summary>
        /// No remote control.
        /// </summary>
        [XmlEnum("1")]
        NoRemoteControl,
        /// <summary>
        /// Ti is stopped.
        /// </summary>
        [XmlEnum("2")]
        TiStopped,
        /// <summary>
        /// Ti is running.
        /// </summary>
        [XmlEnum("3")]
        TiRunning,
        /// <summary>
        /// Ti is unusable.
        /// </summary>
        [XmlEnum("4")]
        TiUnusable
    }
}
