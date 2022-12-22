using DLMS.Comm;
using System;

namespace DLMS
{
    /// <summary>
    /// DLMS Specific Exception class 
    /// that has error description available from GetDescription method.
    /// </summary>
    public class DLMSErrCodesException : DLMSException
    {
        #region Properties

        /// <summary>
        /// Returns occurred error code.
        /// </summary>
        public int ErrorCode
        {
            get;
            internal set;
        }


        /// <summary>
        /// Returns occurred Association Result in AARE message.
        /// </summary>
        public AssociationResult Result
        {
            get;
            internal set;
        }

        /// <summary>
        /// Returns Diagnostic code in AARE message.
        /// </summary>
        public Result_SourceDiagnostic Diagnostic
        {
            get;
            internal set;
        }

        /// <summary>
        /// State error.
        /// </summary>
        public ExceptionStateError StateError
        {
            get;
            private set;
        }

        /// <summary>
        /// Service error.
        /// </summary>
        public ExceptionServiceError ExceptionServiceError
        {
            get;
            private set;
        }

        /// <summary>
        /// Confirmed service error.
        /// </summary>
        public ConfirmedServiceError ConfirmedServiceError
        {
            get;
            private set;
        }

        /// <summary>
        /// Service error.
        /// </summary>
        public ServiceError ServiceError
        {
            get;
            private set;
        }

        /// <summary>
        /// Service error value.
        /// </summary>
        public byte ServiceErrorValue
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public DLMSErrCodesException(int errCode)
            : base(GetDescription((Data_Access_Result)errCode))
        {
            ErrorCode = errCode;
        }

        public DLMSErrCodesException(int errCode, Exception InnerException)
            : base(GetDescription((Data_Access_Result)errCode), InnerException)
        {
            ErrorCode = errCode;
        }


        /// <summary>
        /// Constructor for Action Invoke Error
        /// </summary>
        internal DLMSErrCodesException(int errCode, int Action_Result)
            : this(errCode, Action_Result, null)
        {
        }

        /// <summary>
        /// Constructor for Action Invoke Error
        /// </summary>
        internal DLMSErrCodesException(int errCode, int Action_Result, Exception InnerException)
            : base("Action_Result:" + Get_ActionErrDescription((Action_Result)Action_Result) +
                   "Error_Code: " + GetDescription((Data_Access_Result)errCode), InnerException)
        {
        }


        public DLMSErrCodesException(string message)
            : base(message)
        {

        }


        /// <summary>
        /// Constructor for Confirmed ServiceError.
        /// </summary>
        internal DLMSErrCodesException(ConfirmedServiceError service, ServiceError type, byte value)
            : base("ServiceError " + GetConfirmedServiceError(service) + " exception. " + GetServiceError(type) + " " + GetServiceErrorValue(type, value))
        {
            ConfirmedServiceError = service;
            ServiceError = type;
            ServiceErrorValue = value;
        }

        /// <summary>
        /// Constructor for Confirmed ServiceError.
        /// </summary>
        internal DLMSErrCodesException(ConfirmedServiceError service, ServiceError type, byte value, Exception InnerException)
            : base("ServiceError " + GetConfirmedServiceError(service) + " exception. " + GetServiceError(type) + " " + GetServiceErrorValue(type, value),InnerException)
        {
            ConfirmedServiceError = service;
            ServiceError = type;
            ServiceErrorValue = value;
        }


        /// <summary>
        /// Constructor Confirmed ServiceError for AARE error.
        /// </summary>
        internal DLMSErrCodesException(ExceptionStateError stateError, ExceptionServiceError serviceError)
            : base("Meter returns " + GetStateError(stateError) + " exception. " + GetServiceError(serviceError))
        {
            StateError = stateError;
            ExceptionServiceError = serviceError;
        }

        /// <summary>
        /// Constructor for AARE error.
        /// </summary>
        internal DLMSErrCodesException(ExceptionStateError stateError, ExceptionServiceError serviceError, Exception InnerException)
            : base("Meter returns " + GetStateError(stateError) + " exception. " + GetServiceError(serviceError), InnerException)
        {
            StateError = stateError;
            ExceptionServiceError = serviceError;
        }


        /// <summary>
        /// Constructor for AARE Error.
        /// </summary>
        internal DLMSErrCodesException(AssociationResult result, Result_SourceDiagnostic diagnostic)
            : base("Connection is " + GetResult(result) + ". " + GetDiagnostic(diagnostic))
        {
            Result = result;
            Diagnostic = diagnostic;
        }

        /// <summary>
        /// Constructor OverLoad for AARE Error.
        /// </summary>
        internal DLMSErrCodesException(AssociationResult result, Result_SourceDiagnostic diagnostic, Exception InnerException)
            : base("Connection is " + GetResult(result) + ". " + GetDiagnostic(diagnostic), InnerException)
        {
            Result = result;
            Diagnostic = diagnostic;
        }

        #endregion

        #region Support Methods

        public static string GetStateError(ExceptionStateError stateError)
        {
            switch (stateError)
            {
                case ExceptionStateError.NoValue:
                    return string.Empty;
                case ExceptionStateError.ServiceNotAllowed:
                    return "Service not allowed";
                case ExceptionStateError.ServiceUnknown:
                    return "Service unknown";
            }
            return string.Empty;
        }

        public static string GetConfirmedServiceError(ConfirmedServiceError stateError)
        {
            switch (stateError)
            {
                case ConfirmedServiceError.InitiateError:
                    return "Initiate Error";
                case ConfirmedServiceError.Read:
                    return "Read";
                case ConfirmedServiceError.Write:
                    return "Write";
                default:
                    break;
            }
            return string.Empty;
        }

        public static string GetServiceError(ServiceError error)
        {
            switch (error)
            {
                case ServiceError.ApplicationReference:
                    return "ApplicationReference";
                case ServiceError.HardwareResource:
                    return "HardwareResource";
                case ServiceError.VdeStateError:
                    return "VdeStateError";
                case ServiceError.Service:
                    return "Service";
                case ServiceError.Definition:
                    return "Definition";
                case ServiceError.Access:
                    return "Access";
                case ServiceError.Initiate:
                    return "Initiate";
                case ServiceError.LoadDataSet:
                    return "Load data set";
                case ServiceError.Task:
                    return "Task";
                case ServiceError.OtherError:
                    return "Other Error";
                default:
                    break;
            }
            return string.Empty;
        }

        public static string GetServiceErrorValue(ServiceError error, byte value)
        {
            switch (error)
            {
                case ServiceError.ApplicationReference:
                    return ((ApplicationReference)value).ToString();
                case ServiceError.HardwareResource:
                    return ((HardwareResource)value).ToString();
                case ServiceError.VdeStateError:
                    return ((VdeStateError)value).ToString();
                case ServiceError.Service:
                    return ((Service)value).ToString();
                case ServiceError.Definition:
                    return ((Definition)value).ToString();
                case ServiceError.Access:
                    return ((Access)value).ToString();
                case ServiceError.Initiate:
                    return ((Initiate)value).ToString();
                case ServiceError.LoadDataSet:
                    return ((LoadDataSet)value).ToString();
                case ServiceError.Task:
                    return ((TaskErrs)value).ToString();
                case ServiceError.OtherError:
                    return value.ToString("X2");
                default:
                    break;
            }
            return string.Empty;
        }

        public static string GetServiceError(ExceptionServiceError serviceError)
        {
            string ErrMsg = string.Empty;

            switch (serviceError)
            {
                case ExceptionServiceError.OperationNotPossible:
                    ErrMsg = "Operation not possible";
                    break;
                case ExceptionServiceError.OtherReason:
                    ErrMsg = "Other reason";
                    break;
                case ExceptionServiceError.ServiceNotSupported:
                    ErrMsg = "Service not supported";
                    break;

                // Non Standard MTL Code Used Here
                case ExceptionServiceError.SC_Mismatch:
                    ErrMsg = "Security Control Byte Mismatch";
                    break;
                case ExceptionServiceError.FC_Mismatch:
                    ErrMsg = "Frame Counter Mismatch";
                    break;
                case ExceptionServiceError.AuthTag_Mismatch:
                    ErrMsg = "Authentication TAG Mismatch";
                    break;
                case ExceptionServiceError.MC_AARQRequest_Received:
                    ErrMsg = "AARQ_Received When Meter is already Connected";
                    break;
                case ExceptionServiceError.MD_GetRequest_Received:
                    ErrMsg = "Get Request Received When Meter is Disconnected";
                    break;
                case ExceptionServiceError.MD_SetRequest_Received:
                    ErrMsg = "Set Request Received When Meter is Disconnected";
                    break;
                case ExceptionServiceError.MD_ActRequest_Received:
                    ErrMsg = "Act Request Received When Meter is Disconnected";
                    break;
                case ExceptionServiceError.MC_SAPValue_Mismatch:
                    ErrMsg = "Meter SAP Value Received Mismatch When Meter is Connected";
                    break;
                case ExceptionServiceError.GeneralProtection_Expected:
                    ErrMsg = "Secure APDU With General Protection Cyphering Mode is Expected";
                    break;
                case ExceptionServiceError.ServiceSpecific_Expected:
                    ErrMsg = "Secure APDU With Service Specific Cyphering Mode is Expected";
                    break;
                case ExceptionServiceError.Invalid_SystemTitle_Length:
                    ErrMsg = "Invalid System Title Length Received in Secure APDU";
                    break;
                case ExceptionServiceError.ReplyToHLS_Required:
                    ErrMsg = "Reply To HLS (3rd Invoke Message Pass 2 Way Hand-Shake) in HLS Authentication Required";
                    break;
                default:
                    ErrMsg = string.Format("Unknown Service (Error Code={0:X2})", serviceError);
                    break;
            }

            return ErrMsg;
        }


        /// <summary>
        /// Get result as a string.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string GetResult(AssociationResult result)
        {
            if (result == AssociationResult.Accepted)
            {
                return "Success";
            }
            if (result == AssociationResult.RejectedPermanent)
            {
                return "permanently rejected";
            }
            if (result == AssociationResult.RejectedTransient)
            {
                return "transient rejected";
            }

            throw new InvalidOperationException();
        }

        /// <summary>
        /// Get Diagnostic as String
        /// </summary>
        /// <param name="diagnostic"></param>
        /// <returns></returns>
        public static string GetDiagnostic(Result_SourceDiagnostic diagnostic)
        {
            // String For
            string RetValue = diagnostic.ToString();

            if (diagnostic == Result_SourceDiagnostic.NoReasonGiven)
            {
                RetValue = "No reason is given.";
            }
            else if (diagnostic == Result_SourceDiagnostic.ApplicationContextNameNotSupported)
            {
                RetValue = "The application context name is not supported.";
            }
            else if (diagnostic == Result_SourceDiagnostic.AuthenticationMechanismNameNotRecognised)
            {
                RetValue = "The authentication mechanism name is not recognized.";
            }
            else if (diagnostic == Result_SourceDiagnostic.AuthenticationMechanismNameReguired)
            {
                RetValue = "Authentication mechanism name is required.";
            }
            else if (diagnostic == Result_SourceDiagnostic.AuthenticationFailure)
            {
                RetValue = "Authentication failure.";
            }
            else if (diagnostic == Result_SourceDiagnostic.AuthenticationRequired)
            {
                RetValue = "Authentication is required.";
            }

            return RetValue;
            // throw new InvalidOperationException();
        }

        /// <summary>
        /// Get error description.
        /// </summary>
        /// <param name="error">Error number.</param>
        /// <returns>Error as plain text.</returns>
        public static string GetDescription(Data_Access_Result error)
        {
            string str = null;
            switch (error)
            {
                case Data_Access_Result.Success:
                    str = "";
                    break;
                case Data_Access_Result.Rejected:
                    str = "Connection Rejected";
                    break;
                case Data_Access_Result.Hardware_Fault: //Access Error : Device reports a hardware fault
                    str = "Access Error : Device reports a hardware fault.";
                    break;
                case Data_Access_Result.Temporary_Failure: //Access Error : Device reports a temporary failure
                    str = "Access Error : Device reports a temporary failure.";
                    break;
                case Data_Access_Result.Read_Write_Denied: // Access Error : Device reports Read-Write denied
                    str = "Access Error : Device reports Read-Write denied.";
                    break;
                case Data_Access_Result.Object_Undefined: // Access Error : Device reports a undefined object
                    str = "Access Error : Device reports a undefined object.";
                    break;
                case Data_Access_Result.Object_Class_Inconsistent: // Access Error : Device reports a inconsistent Class or object
                    str = "Access Error : Device reports a inconsistent Class or object.";
                    break;
                case Data_Access_Result.Object_Unavailable: // Access Error : Device reports a unavailable object
                    str = "Access Error : Device reports a unavailable object.";
                    break;
                case Data_Access_Result.Type_Mismatched: // Access Error : Device reports a unmatched type
                    str = "Access Error : Device reports a unmatched type.";
                    break;
                case Data_Access_Result.Scope_of_Access_Violated: // Access Error : Device reports scope of access violated
                    str = "Access Error : Device reports scope of access violated.";
                    break;
                case Data_Access_Result.Data_Block_Unavailable: // Access Error : Data Block Unavailable. 
                    str = "Access Error : Data Block Unavailable.";
                    break;
                case Data_Access_Result.Long_Get_Aborted: // Access Error : Long Get Or Read Aborted.
                    str = "Access Error : Long Get Or Read Aborted.";
                    break;
                case Data_Access_Result.no_Long_Get_in_Progress: // Access Error : No Long Get Or Read In Progress.
                    str = "Access Error : No Long Get Or Read In Progress.";
                    break;
                case Data_Access_Result.Long_Set_Aborted: // Access Error : Long Set Or Write Aborted.
                    str = "Access Error : Long Set Or Write Aborted.";
                    break;
                case Data_Access_Result.no_Long_Set_in_Progress: // Access Error : No Long Set Or Write In Progress.
                    str = "Access Error : No Long Set Or Write In Progress.";
                    break;
                case Data_Access_Result.Data_Block_NumberInvalid: // Access Error : Data Block Number Invalid.
                    str = "Access Error : Data Block Number Invalid.";
                    break;
                case Data_Access_Result.Other_Reason: // Access Error : Other Reason.
                    str = "Access Error : Other Reason.";
                    break;
                default:
                    str = "Unknown Error.";
                    break;
            }
            return str;
        }


        /// <summary>
        /// Get error description.
        /// </summary>
        /// <param name="error">Error number.</param>
        /// <returns>Error as plain text.</returns>
        public static string Get_ActionErrDescription(Action_Result error)
        {
            string str = null;
            switch (error)
            {
                case Action_Result.Success:
                    str = string.Empty;
                    break;
                case Action_Result.Rejected:
                    str = "Connection Rejected";
                    break;
                case Action_Result.Hardware_fault: //Access Error : Device reports a hardware fault
                    str = "Access Error : Device reports a hardware fault.";
                    break;
                case Action_Result.Temporary_failure: //Access Error : Device reports a temporary failure
                    str = "Access Error : Device reports a temporary failure.";
                    break;
                case Action_Result.Read_write_denied: // Access Error : Device reports Read-Write denied
                    str = "Access Error : Device reports Read-Write denied.";
                    break;
                case Action_Result.Object_undefined: // Access Error : Device reports a undefined object
                    str = "Access Error : Device reports a undefined object.";
                    break;
                case Action_Result.Object_class_inconsistent: // Access Error : Device reports a inconsistent Class or object
                    str = "Access Error : Device reports a inconsistent Class or object.";
                    break;
                case Action_Result.Object_unavailable: // Access Error : Device reports a unavailable object
                    str = "Access Error : Device reports a unavailable object.";
                    break;
                case Action_Result.Type_unmatched: // Access Error : Device reports a unmatched type
                    str = "Access Error : Device reports a unmatched type.";
                    break;
                case Action_Result.Scope_of_access_violated: // Access Error : Device reports scope of access violated
                    str = "Access Error : Device reports scope of access violated.";
                    break;
                case Action_Result.Data_block_unavailable: // Access Error : Data Block Unavailable. 
                    str = "Access Error : Data Block Unavailable.";
                    break;
                case Action_Result.Long_action_aborted: // Access Error : Long Action Or Read Aborted.
                    str = "Access Error : Long Action Or Read Aborted.";
                    break;
                case Action_Result.No_long_action_in_progress: // Access Error : No Long Action Or Read In Progress.
                    str = "Access Error : No Long Action Or Read In Progress.";
                    break;
                case Action_Result.other_reason: // Access Error : Other Reason.
                    str = "Access Error : Other Reason.";
                    break;
                default:
                    str = "Unknown Error.";
                    break;
            }
            return str;
        }

        #endregion
    }
}
