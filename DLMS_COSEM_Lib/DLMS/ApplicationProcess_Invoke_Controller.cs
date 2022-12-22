// #define Enable_DEBUG_ECHO
#define Enable_Error_Logging
#define Enable_IO_Logging

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DLMS.Comm;
using System.IO;
using Serenity.Crypto;

namespace DLMS
{
    /// <summary>
    /// The ApplicationProcess_Invoke_Controller is an Sub Interface/Controller class to manage 
    /// the flow and control for DLMS_COSEM Invoke Service
    /// The ApplicationProcess_Invoke_Controller provides the DLMS_COSEM Protocol service,Invoke_Service <see cref="Method_InvokeHelper(Base_Class)"/>,
    /// <see cref="Method_InvokeHelperAsync(Base_Class)"/>
    /// </summary>
    public class ApplicationProcess_Invoke_Controller
    {
        #region Data_Members

        private ApplicationProcess_Controller internalAProcess_Controller = null;
        internal Invoke_Process_Structure Struct_Invoke = null;

        #endregion

        #region Properties

        /// <summary>
        /// GET/SET <see cref="ApplicationProcess_Controller"/>
        /// </summary>
        public ApplicationProcess_Controller ApplicationProcess_Controller
        {
            get { return internalAProcess_Controller; }
            set { internalAProcess_Controller = value; }
        }

        #endregion

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ApplicationProcess_Invoke_Controller()
        {
            Struct_Invoke = new Invoke_Process_Structure();
        }

        #region Member Functions

        /// <summary>
        /// This helper method invokes particular action by DLMS_COSEM Action Service
        /// </summary>
        /// <remarks>
        /// This function is implemented using Sync Communication Model
        /// </remarks>
        /// <exception cref="IOException">IOException Class</exception>
        /// <exception cref="DLMSException">DLMSException Class</exception>
        /// <exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        /// <exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <param name="ObjectToInvoke">COSEM Object used to Invoke Action</param>
        /// <returns>Action_Result</returns>
        public Action_Result Method_InvokeHelper(Base_Class ObjectToInvoke)
        {
            Action_Result _ActionResult = Action_Result.other_reason;
            try
            {

            Loop_WriteRead:
                ApplicationProcess_Controller.DLMSNotify = false;
                Struct_Invoke.Object_Received = ObjectToInvoke;
                Struct_Invoke.Request_Type = ACTION_req_Type.Normal;

                // Send Invoke Request Normal
                SendRequest_InvokeHelper(Struct_Invoke);
            RepeatRead_Loop:
                try
                {
                    ReceiveResponse_InvokeHelper(Struct_Invoke);
                    // Reset Retry Counter
                    Struct_Invoke.Retry = 0;
                }
                catch (Exception ex)
                {
                    if (ex.IsIODataTimeOut() &&
                        ApplicationProcess_Controller.DLMSNotify &&
                        Struct_Invoke.Retry < ApplicationProcess_Controller.MAX_Request_Retry)
                    {
                        Struct_Invoke.Retry++;
                        goto Loop_WriteRead;
                    }
                    else
                        // Raise Error
                        throw ex;
                }

                if (Struct_Invoke.InnerException != null ||
                    Struct_Invoke.PacketDrop)
                    throw Struct_Invoke.InnerException;
                else if (ApplicationProcess_Controller.DLMSNotify)
                    goto RepeatRead_Loop;

                if (Struct_Invoke.Response_Type != Action_res_Type.Normal)
                    throw new DLMSException("Response Type Not implemented yet");
                _ActionResult = Struct_Invoke.Result;
                return (Action_Result)_ActionResult;
            }
            catch (Exception ex)
            {
                ApplicationProcess_Controller.SetResetIOStream(true);

                if (ex is IOException)
                    throw ex;
                else if (ex is DLMSErrCodesException)
                {
                    var statErr = (ex as DLMSErrCodesException).StateError;
                    var expServErr = (ex as DLMSErrCodesException).ExceptionServiceError;

                    #region // Special Validation Rule

                    // Meter Reply With Disconnection
                    if (expServErr == ExceptionServiceError.MD_GetRequest_Received ||
                        expServErr == ExceptionServiceError.MD_SetRequest_Received ||
                        expServErr == ExceptionServiceError.MD_ActRequest_Received)
                    {
                        if (ApplicationProcess_Controller != null &&
                            ApplicationProcess_Controller.ApplicationProcess != null)
                            ApplicationProcess_Controller.ApplicationProcess.Is_Association_Developed = false;
                    } 

                    #endregion

                    throw ex;
                }
                else if (ex is DLMSException)
                    throw ex;
                else if (ex is CryptoException)
                    throw ex;
                else
                    throw new DLMSException(String.Format("Error occurred in Method_Invoke Service {0}", ObjectToInvoke.INDEX), ex);
            }
            finally
            {
                Struct_Invoke.InitStruct();
            }
        }

        /// <summary>
        /// Invokes particular action/method of a particular object in DLMS_COSEM device
        /// </summary>
        /// <remarks>
        /// This function is implemented using TPL (Task parallel library) communication model
        /// </remarks>
        /// <exception cref="IOException">IOException Class</exception>
        /// <exception cref="DLMSException">DLMSException Class</exception>
        /// <exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        /// <exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <param name="ObjectToInvoke">COSEM Object used to Invoke Action</param>
        /// <returns>Task&ls;Action_Result&gt;</returns>
        public async Task<Action_Result> Method_InvokeHelperAsync(Base_Class ObjectToInvoke)
        {
            Action_Result _ActionResult = Action_Result.other_reason;
            try
            {
            Loop_WriteRead:
                ApplicationProcess_Controller.DLMSNotify = false;
                Struct_Invoke.Object_Received = ObjectToInvoke;
                Struct_Invoke.Request_Type = ACTION_req_Type.Normal;
                // Send Invoke Request Normal
                await SendRequest_InvokeHelperASync(Struct_Invoke);

            RepeatRead_Loop:
                try
                {
                    // T.Wait();
                    await ReceiveResponse_InvokeHelperASync(Struct_Invoke);
                    // Reset Retry Counter
                    Struct_Invoke.Retry = 0;
                }
                catch (Exception ex)
                {
                    if (ex.IsIODataTimeOut() &&
                        ApplicationProcess_Controller.DLMSNotify &&
                        Struct_Invoke.Retry < ApplicationProcess_Controller.MAX_Request_Retry)
                    {
                        Struct_Invoke.Retry++;
                        goto Loop_WriteRead;
                    }
                    else
                        // Raise Error
                        throw ex;
                }

                if (Struct_Invoke.InnerException != null ||
                    Struct_Invoke.PacketDrop)
                    throw Struct_Invoke.InnerException;
                else if (ApplicationProcess_Controller.DLMSNotify)
                    goto RepeatRead_Loop;

                if (Struct_Invoke.Response_Type != Action_res_Type.Normal)
                    throw new DLMSException("Response Type Not implemented yet");
                _ActionResult = Struct_Invoke.Result;
                return (Action_Result)_ActionResult;
            }
            catch (Exception ex)
            {
                ApplicationProcess_Controller.SetResetIOStream(true);

                if (ex is IOException)
                    throw ex;
                else if (ex is DLMSErrCodesException)
                {
                    var statErr = (ex as DLMSErrCodesException).StateError;
                    var expServErr = (ex as DLMSErrCodesException).ExceptionServiceError;

                    // Special Validation Rules
                    // Meter Reply With Disconnection
                    if (expServErr == ExceptionServiceError.MD_GetRequest_Received ||
                        expServErr == ExceptionServiceError.MD_SetRequest_Received ||
                        expServErr == ExceptionServiceError.MD_ActRequest_Received)
                    {
                        if (ApplicationProcess_Controller != null &&
                            ApplicationProcess_Controller.ApplicationProcess != null)
                            ApplicationProcess_Controller.ApplicationProcess.Is_Association_Developed = false;
                    }

                    throw ex;
                }
                else if (ex is DLMSException)
                    throw ex;
                else if (ex is CryptoException)
                    throw ex;
                else
                    throw new DLMSException(String.Format("Error occurred in Method_Invoke Service {0}",
                                            ObjectToInvoke.INDEX), ex);
            }
            finally
            {
                Struct_Invoke.InitStruct();
            }
        }

        #endregion

        #region Support Function


        /// <summary>
        /// Receive and decode the response of an Invoke_Service 
        /// </summary>
        /// <remarks>
        /// This helper function is Implemented using Sync Communication Model
        /// </remarks>
        /// <param name="Invoke_Process_Structure_Local">The last InvokeResponse_Decoded</param>
        /// <returns>Task that represent the pending IO Invoke Response</returns>
        internal void ReceiveResponse_InvokeHelper(Invoke_Process_Structure Invoke_Process_Structure_Local)
        {
            try
            {
                byte[] T_LcocalBuffer = null;
                int offset = 0, count = 0;
                T_LcocalBuffer = ApplicationProcess_Controller.LocalBuffer;
                count = T_LcocalBuffer.Length;
                ApplicationProcess_Controller.DLMSNotify = false;
                // Receive Get Response Syn
                var res = ApplicationProcess_Controller.ReceiveResponseFromPhysicalLayer(T_LcocalBuffer, offset, count);
                Invoke_Process_Structure_Local.Invoke_HelperDecode(ApplicationProcess_Controller, T_LcocalBuffer, offset, res);

                return;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Encode and transmit Invoke_Service Request
        /// </summary>
        /// <remarks>
        /// This helper function is Implemented using Sync Communication Model
        /// </remarks>
        /// <param name="Invoke_Process_Structure_Local">The last InvokeRequest Encoded</param>
        /// <returns>The Task that represent last pending IO request</returns>
        internal void SendRequest_InvokeHelper(Invoke_Process_Structure Invoke_Process_Structure_Local)
        {
            try
            {
                byte[] T_LcocalBuffer = null;
                int offset = 0, count = 0;
                T_LcocalBuffer = ApplicationProcess_Controller.LocalBuffer;
                count = T_LcocalBuffer.Length;
                // Encode Get Request Packet
                Invoke_Process_Structure_Local.Invoke_HelperEncode(ApplicationProcess_Controller, ref T_LcocalBuffer, ref offset, ref count);
                ApplicationProcess_Controller.SendRequestFromPhysicalLayer(T_LcocalBuffer, offset, count);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Receive and decode the response of an Invoke_Service 
        /// </summary>
        /// <remarks>
        /// This helper function is Implemented using Task Based Async Communication Model
        /// </remarks>
        /// <param name="Invoke_Process_Structure_Local">The last InvokeResponse_Decoded</param>
        /// <returns>Task that represent the pending IO Invoke Response</returns>
        internal async Task ReceiveResponse_InvokeHelperASync(Invoke_Process_Structure Invoke_Process_Structure_Local)
        {
            try
            {
                byte[] T_LcocalBuffer = null;
                int offset = 0, count = 0;
                T_LcocalBuffer = ApplicationProcess_Controller.LocalBuffer;
                count = T_LcocalBuffer.Length;
                // Receive Get Response ASync
                var res = await ApplicationProcess_Controller.ReceiveResponseFromPhysicalLayerAsync(T_LcocalBuffer, offset, count);
                Invoke_Process_Structure_Local.Invoke_HelperDecode(ApplicationProcess_Controller, T_LcocalBuffer, offset, res);
                return;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Encode and transmit Invoke_Service Request
        /// </summary>
        /// <remarks>
        /// This helper function is Implemented using Task Based Async Communication Model
        /// </remarks>
        /// <param name="Invoke_Process_Structure_Local">The last InvokeRequest Encoded</param>
        /// <returns>The Task that represent last pending IO request</returns>
        internal async Task SendRequest_InvokeHelperASync(Invoke_Process_Structure Invoke_Process_Structure_Local)
        {
            try
            {
                byte[] T_LcocalBuffer = null;
                int offset = 0, count = 0;
                T_LcocalBuffer = ApplicationProcess_Controller.LocalBuffer;
                count = T_LcocalBuffer.Length;
                // Encode Get Request Packet
                Invoke_Process_Structure_Local.Invoke_HelperEncode(ApplicationProcess_Controller, ref T_LcocalBuffer, ref offset, ref count);
                var res = await ApplicationProcess_Controller.SendRequestFromPhysicalLayerAsync(T_LcocalBuffer, offset, count);
                return;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }

    /// <summary>
    /// Helper class provides structure used by Invoke_Service
    /// </summary>
    /// <remarks>
    /// The class structure is initialize and process during <see cref="DLMS.ApplicationProcess_Invoke_Controller.
    /// ReceiveResponse_InvokeHelper(Invoke_Process_Structure)"/>,<see cref="DLMS.ApplicationProcess_Invoke_Controller.
    /// SendRequest_InvokeHelper(Invoke_Process_Structure)"/> methods execution
    ///</remarks>
    class Invoke_Process_Structure
    {
        #region Data_Members

        /// <summary>
        /// This event will Notify Method_Invoke_Response when Decoding Process Complete 
        /// </summary>
        event Action<Action_Result> Application_Process_Action_Response = null;
        /// <summary>
        /// This event will notify COSEM Transport Layer when Packet drop
        /// </summary>
        event Action<PacketType, String> ApplicationLayer_PacketDrop = null;

        /// <summary>
        /// The COSEM Instance/Object which Action would be Invoke 
        /// </summary>
        internal Base_Class Object_Received = null;
        /// <summary>
        /// Get Action Invoke_Service Response <see cref="Action_res_Type"/>
        /// </summary>
        internal Action_res_Type Response_Type;
        /// <summary>
        /// Get/Set Action Invoke_Service Request Type to transmit <see cref="ACTION_req_Type"/>
        /// </summary>
        internal ACTION_req_Type Request_Type;
        /// <summary>
        /// The Action Invoke_Service OutCome/Result <see cref="Action_Result"/>
        /// </summary>
        internal Action_Result Result;
        /// <summary>
        /// Param_Response Result <see cref="Data_Access_Result"/>
        /// </summary>
        internal Data_Access_Result Param_ResponseResult;
        /// <summary>
        /// The IO_Buffer for an Action Encoding or Decoding process
        /// </summary>
        internal byte[] Encoded_Buffer = null;
        /// <summary>
        /// No of Block Number Received in Invoke_Service_With_BLOCK process
        /// </summary>
        internal UInt32 Block_NumberReceived;
        /// <summary>
        /// No of Block to Transmit
        /// </summary>
        internal UInt32 Block_NumberTransfer;
        internal int MaxSETBlocks;
        internal bool Last_Block;
        internal bool PacketDrop;

        /// <summary>
        /// The Counter to Remember Message Request Resent
        /// </summary>
        internal int Retry = 0;

        /// <summary>
        /// This exception store error that occurred
        /// </summary>
        /// <remarks>
        /// The error occurred during last Invoke_Service Encoding or Decoding process is stored
        /// </remarks>
        internal Exception InnerException = null;
        internal bool isPorcessComplete = false;

        internal bool HLS_Applied = false;
        internal bool isTagMatch = false;

        #endregion

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Invoke_Process_Structure()
        {
            InitStruct();
        }

        /// <summary>
        /// Helper method to initialize the Structure
        /// </summary>
        internal void InitStruct()
        {
            Object_Received = null;
            Response_Type = Action_res_Type.Normal;
            Request_Type = ACTION_req_Type.Normal;
            Result = Action_Result.Success;
            Param_ResponseResult = Data_Access_Result.Success;

            Encoded_Buffer = null;
            MaxSETBlocks = -1;
            Block_NumberReceived = 0;
            Block_NumberTransfer = 0;
            Last_Block = false;
            PacketDrop = false;
            Retry = 0;
            InnerException = null;
            isPorcessComplete = false;
        }

        /// <summary>
        /// Used to decode Action Invoke_Service Response packet receive
        /// </summary>
        /// <remarks>
        /// This helper function used to decode Action Invoke_Service Response packet received 
        /// and verify either valid response received.After Decoding the received Invoke Response 
        /// it updates Invoke_Process_Structure to reflect the Decoding Status
        /// </remarks>
        /// <param name="ApplicationProcess_Controller">Provides high level services</param>
        /// <param name="localBuffer">IO buffer to be used for decoding</param>
        /// <param name="offset">int:OffSet Index to the encoded buffered</param>
        /// <param name="count">int:Number of bytes encoded</param>
        internal void Invoke_HelperDecode(ApplicationProcess_Controller ApplicationProcess_Controller,
            byte[] localBuffer, int offset, int count)
        {
            try
            {
                // ApplicationProcess_Controller.Dettach_Handlers();

                isPorcessComplete = false;
                String error_Message = String.Empty;
                PacketDrop = false;
                stACTION StAction_from_AL = null;

                #region // Event Handler Code To Receive Decoding Errors & Exception

                if (ApplicationLayer_PacketDrop == null)
                    ApplicationLayer_PacketDrop =
                            (PacketType packetType, string errorMessage) =>
                            {
                                InnerException = new DLMSException(String.Format("{0} Packet drop,error details {1}", packetType, error_Message));
                                PacketDrop = true;
                                ApplicationProcess_Controller.DLMSNotify = false;
                                isPorcessComplete = true;
                            };

                #endregion

                #region Event Handler To Receive Get_Service_Response

                if (Application_Process_Action_Response == null)
                    Application_Process_Action_Response =
                    (Action_Result ActionResult) =>
                    {
                        ApplicationProcess_Controller.DLMSNotify = false;
                        Result = ActionResult;
                        // Copy Internal Get Decoder Structure Here
                        StAction_from_AL = ApplicationProcess_Controller.ApplicationProcess.ACTION_from_AL;
                        // Populate Internal Variables
                        Response_Type = (Action_res_Type)StAction_from_AL.Response_Type;
                        // Either Get Response Or Last Block Package
                        Last_Block = true;
                        isPorcessComplete = true;
                    };

                #endregion

                #region Attach_Local_Function_Handlers

                ApplicationProcess_Controller.ApplicationProcess.ApplicationLayer.PacketDrop += ApplicationLayer_PacketDrop;
                ApplicationProcess_Controller.ApplicationProcess.Action_Response += Application_Process_Action_Response;

                #endregion

                #region //  Apply Packet_Decryption Then Send Packet For IO Decoding

                if (localBuffer == null || localBuffer.Length == 0 || count <= 0)
                    throw new IOException("Invalid data buffer to be decoded received_DLMS_Controller_InvokeIOData");

                ArraySegment<byte> Resultant_Data = new ArraySegment<byte>(localBuffer, offset, count);

                if (Security_Context.IsSecurityApplicable(Resultant_Data.Array[offset]))
                {
                    Resultant_Data = Security_Context.Process_HLS_APDU(ApplicationProcess_Controller.Security_Data,
                                                                       ApplicationProcess_Controller.Crypto, Resultant_Data, out isTagMatch);
                }

                #region Debugging & Logging
#if Enable_IO_Logging

                if (ApplicationProcess_Controller.ApplicationProcess.Logger != null)
                    // Log Transmitted Data
                    ApplicationProcess_Controller.ApplicationProcess.Logger.LogALTraffic(Resultant_Data.Array, Resultant_Data.Offset, Resultant_Data.Count, DataStatus.Read);

#endif
                #endregion

                ApplicationProcess_Controller.ApplicationProcess.ApplicationLayer.packetReceived(Resultant_Data);

                #endregion

                // Check If GET Response Received
                if (!isPorcessComplete &&
                    !ApplicationProcess_Controller.DLMSNotify)
                    throw new DLMSException("Error occurred while Process Invoke_HelperDecode,unknown packet type");

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                #region Deattach_Local_Function_Handlers

                ApplicationProcess_Controller.ApplicationProcess.ApplicationLayer.PacketDrop -= ApplicationLayer_PacketDrop;
                ApplicationProcess_Controller.ApplicationProcess.Action_Response -= Application_Process_Action_Response;

                #endregion
            }
        }

        /// <summary>
        /// Encodes the Invoke_Request_Normal,Invoke_Request_Next_Block Request Packet
        /// </summary>
        /// <param name="ApplicationProcess_Controller">NIL</param>
        /// <param name="localBuffer">IO buffer to be used for encoding</param>
        /// <param name="offset">int:OffSet Index to the encoded buffered</param>
        /// <param name="count">int:Number of bytes encoded</param>
        internal void Invoke_HelperEncode(ApplicationProcess_Controller ApplicationProcess_Controller,
            ref byte[] localBuffer, ref int offset, ref int count)
        {
            ArraySegment<byte> Encoded_Packet;
            byte[] Packet = null;
            try
            {
                if (Object_Received == null)
                    throw new ArgumentNullException("Null Argument Exception Get_Process_Structure_Local");

                // Build later Encode Invoke With Parameters
                if (Request_Type == ACTION_req_Type.Normal)
                {
                    // Check Method Access Rights
                    byte methodId = Object_Received.MethodInvokeId;
                    Method_Access_Modes MethodAccess = Object_Received.GetMethodRight(methodId);

                    if (!Object_Received.IsMethodInvokable(methodId))
                        throw new DLMSException(String.Format("{0} not valid access rights to invoke method {1},{2} (Error Code:{3})",
                                MethodAccess, methodId, Object_Received.INDEX, (int)DLMSErrors.Insufficient_Priviledge));

                    byte[] ParameterData = Object_Received.Encode_Parameters();
                    Encoded_Buffer = ParameterData;
                    // Encode Action Base Class Object
                    Packet = ApplicationProcess_Controller.ApplicationProcess.Encode_Action_Object(Object_Received, methodId, ParameterData);
                }
                else
                {
                    throw new DLMSException("Invoke other Normal Request not implemented yet");
                }
                Encoded_Packet = Packet.GetSegment<byte>();

                #region Debugging & Logging
#if Enable_IO_Logging

                if (ApplicationProcess_Controller.ApplicationProcess.Logger != null)
                    // Log Transmitted Data
                    ApplicationProcess_Controller.ApplicationProcess.
                        Logger.LogALTraffic(Packet,
                                            0,
                                            Packet.Length, DataStatus.Write);

#endif
                #endregion

                #region Apply_HighLEVEL_Security

                // Only For HLS Authentication Type & SecurityControl is not 'None'
                if (ApplicationProcess_Controller.ApplicationProcess.Send_APPLICATION_ASSOCIATION.Authentication_Mechanism >= HLS_Mechanism.HLS_Manufac &&
                    ApplicationProcess_Controller.Security_Data != null &&
                    ApplicationProcess_Controller.Security_Data.SecurityControl > SecurityControl.None)
                {
                    if (ApplicationProcess_Controller.Crypto == null ||
                        ApplicationProcess_Controller.Security_Data == null ||
                       !ApplicationProcess_Controller.Security_Data.IsInitialized)
                    {
                        throw new CryptoException("Security Data not initialized properly to proceed for HLS Communication");
                    }

                    // Authenticated_Encryption
                    ArraySegment<byte> AuthenticatedEncrypted_Packet;

                    if (ApplicationProcess_Controller.ApplicationProcess.Negotiated_Conformance.GeneralProtection)
                    {
                        // Authenticated_Encryption
                        // Update Glo_Message Command TAG

                        AuthenticatedEncrypted_Packet =
                                             Security_Context.Apply_GlobalGeneral_AuthenticatedEncryption(ApplicationProcess_Controller.Security_Data,
                                                                                                          ApplicationProcess_Controller.Crypto,
                                                                                                          Encoded_Packet);
                    }
                    else
                    {
                        // Authenticated_Encryption
                        // Update Glo_Message Command TAG
                        if (ApplicationProcess_Controller.Security_Data != null)
                            ApplicationProcess_Controller.Security_Data.GloMessageTAG = (DLMSCommand)DLMS_Common.GetGloDLMSCommmandTAG(DLMSCommand.MethodRequest);

                        AuthenticatedEncrypted_Packet =
                                             Security_Context.Apply_GlobalServiceSpecific_AuthenticatedEncryption(ApplicationProcess_Controller.Security_Data,
                                                                                                          ApplicationProcess_Controller.Crypto,
                                                                                                          Encoded_Packet);

                    }

                    Encoded_Packet = AuthenticatedEncrypted_Packet;
                    HLS_Applied = true;
                }
                else
                    HLS_Applied = false;

                #endregion

                #region /// Copy Encoded_Buffer

                if (localBuffer == null)
                {
                    localBuffer = Encoded_Packet.Array;
                    offset = Encoded_Packet.Offset;
                    count = Encoded_Packet.Count;
                }
                else
                {
                    Buffer.BlockCopy(Encoded_Packet.Array, Encoded_Packet.Offset, localBuffer, offset, Encoded_Packet.Count);
                    count = Encoded_Packet.Count;
                }

                #endregion
            }
            catch (CryptoException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Encoding Object", ex);
            }
        }
    }

}
