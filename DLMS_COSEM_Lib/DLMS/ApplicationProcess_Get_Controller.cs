// #define Enable_DEBUG_ECHO
#define Enable_Error_Logging
#define Enable_IO_Logging

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using DLMS.Comm;
using Serenity.Crypto;

namespace DLMS
{
    /// <summary>
    /// The ApplicationProcess_GET_Controller is an Sub Interface/Controller class to manage 
    /// the flow and control for DLMS_COSEM GET Service
    /// The ApplicationProcess_GET_Controller provides the DLMS_COSEM Protocol service,GET_Service <see cref="GET_Helper(Base_Class)"/>
    /// </summary>
    public class ApplicationProcess_GET_Controller
    {
        #region Data_Members

        private ApplicationProcess_Controller internalAProcess_Controller = null;
        private Get_Process_Structure internalGETProcessStruct = null;

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
        public ApplicationProcess_GET_Controller()
        {
            internalGETProcessStruct = new Get_Process_Structure();
        }

        #region Member_Functions


        /// <summary>
        /// Read data from DLMS_COSEM device
        /// </summary>
        /// <remarks>
        /// This function is implemented using Synchronous IO Communication Model
        /// </remarks>
        ///	<exception cref="IOException">IOException Class</exception>
        ///	<exception cref="DLMSException">DLMSException Class</exception>
        ///	<exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        ///	<exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <param name="GetRequestObj">COSEM Object to be used for data read</param>
        /// <returns>Base_Class</returns>
        public Base_Class GET_Helper(Base_Class GetRequestObj, CancellationTokenSource GetAbortToken = null)
        {
            Get_Process_Structure Get_Command_Process_Status = internalGETProcessStruct;
            try
            {
                if (GetRequestObj == null)
                    throw new ArgumentNullException("GetRequestObj");

                 // Resend GET Normal Request
            Loop_WriteRead: ApplicationProcess_Controller.DLMSNotify = false;
                Get_Command_Process_Status.Object_Received = GetRequestObj;

                #region Debugging & Logging
#if  Enable_DEBUG_ECHO

                if (ApplicationProcess_Controller.ActivityLogger != null)
                    ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("Get_Helper Sending Normal Get Request {0} {1} {2}", GetRequestObj.INDEX,
                    GetRequestObj.OBISIndex, ApplicationProcess_Controller.GetCommunicationObject));

#endif
                #endregion
                // Send Get Service Request Normal
                SendRequest_GetHelper(Get_Command_Process_Status);

                #region Debugging & Logging
#if  Enable_DEBUG_ECHO

                if (ApplicationProcess_Controller.ActivityLogger != null)
                    ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("Get_Helper Receiving Get Request {0} {1} {2}", GetRequestObj.INDEX,
                    GetRequestObj.OBISIndex, ApplicationProcess_Controller.GetCommunicationObject));

#endif
                #endregion

            // Receive Get_Service_Response_Normal
            RepeatRead_Loop:
                try
                {
                    ReceiveResponse_GetHelper(Get_Command_Process_Status);
                    Get_Command_Process_Status.Retry = 0;
                }
                catch (Exception ex)
                {
                    if (ex.IsIODataTimeOut() &&
                        ApplicationProcess_Controller.DLMSNotify &&
                        Get_Command_Process_Status.Retry < ApplicationProcess_Controller.MAX_Request_Retry)
                    {
                        Get_Command_Process_Status.Retry++;
                        goto Loop_WriteRead;
                    }
                    else
                        // Raise Error
                        throw ex;
                }

                if (Get_Command_Process_Status.PacketDrop ||
                    Get_Command_Process_Status.InnerException != null)
                    throw Get_Command_Process_Status.InnerException;
                else if (ApplicationProcess_Controller.DLMSNotify)
                    goto RepeatRead_Loop;

                if (Get_Command_Process_Status.Response_Type == GET_Method.Normal)
                    return GetRequestObj;
                else if (Get_Command_Process_Status.Response_Type == GET_Method.With_Block)
                {
                    #region Debugging & Logging
#if  Enable_DEBUG_ECHO
                    if (ApplicationProcess_Controller.ActivityLogger != null)
                        ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("Get_Helper Receiving Get Request With Blocking {0} {1} {2}", GetRequestObj.INDEX,
                        GetRequestObj.OBISIndex, ApplicationProcess_Controller.GetCommunicationObject));
#endif
                    #endregion
                    GetReceive_WithBlock(Get_Command_Process_Status, GetAbortToken);
                }

                #region Debugging & Logging
#if  Enable_DEBUG_ECHO
                if (ApplicationProcess_Controller.ActivityLogger != null)
                    ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("Get_Helper completed successfully {0} {1} {2}", GetRequestObj.INDEX,
                    GetRequestObj.OBISIndex, ApplicationProcess_Controller.GetCommunicationObject));
#endif
                #endregion
                return GetRequestObj;
            }
            catch (Exception ex)
            {
                #region Exception Handle

                if (ex is DLMSDecodingException ||
                    ex is DLMSEncodingException)
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
                else if (ex is IOException)
                    throw ex;
                else if (ex is ThreadAbortException)
                    throw ex;

                else
                {
                    throw new DLMSException(String.Format("Error occurred in Get_HelperAsync {0},{1}", GetRequestObj.INDEX,
                        GetRequestObj.OBISIndex), ex);
                }

                #endregion
            }
            finally
            {
                #region Trying Decode Partial GET_Response

                try
                {
                    if (Get_Command_Process_Status != null && !Get_Command_Process_Status.isSuccess)
                    {
                        //try Decode Received Partial Data
                        if (Get_Command_Process_Status.Response_Type != GET_Method.Normal)
                        {
                            if (Get_Command_Process_Status.ReceivedBuffer != null && Get_Command_Process_Status.ReceivedBuffer.Length >= 1)
                            {
                                #region Debugging & Logging
#if  Enable_DEBUG_ECHO
                                if (ApplicationProcess_Controller.ActivityLogger != null)
                                    ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("Trying Decode GET With Blocking Partial Response", GetRequestObj.INDEX,
                                    GetRequestObj.OBISIndex, ApplicationProcess_Controller.GetCommunicationObject));
#endif
                                #endregion
                                int array_traverse = 0;
                                byte[] t_Buffer = Get_Command_Process_Status.ReceivedBuffer;
                                GetRequestObj.Decode_Data(ref t_Buffer, ref array_traverse, t_Buffer.Length);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    #region Debugging & Logging

#if  Enable_DEBUG_ECHO
                    if (ApplicationProcess_Controller.ActivityLogger != null)
                        ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("Error occurred while Processing {0}", ex, GetRequestObj.INDEX,
                        GetRequestObj.OBISIndex, ApplicationProcess_Controller.GetCommunicationObject));
#endif

                    #endregion
                }

                #endregion
                internalGETProcessStruct.InitStruct();
            }

        }

        /// <summary>
        /// Read data from DLMS_COSEM device Asynchronously
        /// </summary>
        /// <remarks>
        /// This function is implemented using Task based Async communication model,see also <see cref="GET_HelperAsync"/>
        /// </remarks>
        ///<exception cref="IOException">IOException Class</exception>
        ///<exception cref="ThreadAbortException">ThreadAbortException Class</exception>
        ///<exception cref="DLMSException">DLMSException Class</exception>
        ///<exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        ///<exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <param name="GetRequestObj">COSEM Object to be used to read data</param>
        /// <param name="GetAbortToken">Data Read Cancellation Token during GET_WITH_BLOCKING blocking</param>
        /// <returns>Task&lt;Base_Class&gt;</returns>
        public async Task GET_HelperAsync(Base_Class GetRequestObj, CancellationTokenSource GetAbortToken = null)
        {
            Get_Process_Structure Get_Command_Process_Status = internalGETProcessStruct;
            try
            {
                if (GetRequestObj == null)
                    throw new ArgumentNullException("GetRequestObj");

            Loop_WriteRead:
                ApplicationProcess_Controller.DLMSNotify = false;
                Get_Command_Process_Status.Object_Received = GetRequestObj;

                #region Debugging & Logging
#if  Enable_DEBUG_ECHO
                if (ApplicationProcess_Controller.ActivityLogger != null)
                    ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("Get_HelperAsync Sending Normal Get Request {0} {1} {2}", GetRequestObj.INDEX,
                    GetRequestObj.OBISIndex, ApplicationProcess_Controller.GetCommunicationObject));
#endif
                #endregion
                // Send Get Service Request Normal
                await SendRequest_GetHelperASync(Get_Command_Process_Status);
                #region Debugging & Logging
#if  Enable_DEBUG_ECHO
                if (ApplicationProcess_Controller.ActivityLogger != null)
                    ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("Get_HelperAsync Receiving Get Request {0} {1} {2}", GetRequestObj.INDEX,
                    GetRequestObj.OBISIndex, ApplicationProcess_Controller.GetCommunicationObject));
#endif
                #endregion

            RepeatRead_Loop:
                try
                {
                    // Receive Get_Service_Response_Normal
                    await ReceiveResponse_GetHelperASync(Get_Command_Process_Status);
                    Get_Command_Process_Status.Retry = 0;
                }
                catch (Exception ex)
                {
                    if (ex.IsIODataTimeOut() &&
                        ApplicationProcess_Controller.DLMSNotify &&
                        Get_Command_Process_Status.Retry < ApplicationProcess_Controller.MAX_Request_Retry)
                    {
                        Get_Command_Process_Status.Retry++;
                        goto Loop_WriteRead;
                    }
                    else
                        // Raise Error
                        throw ex;
                }

                if (Get_Command_Process_Status.PacketDrop ||
                    Get_Command_Process_Status.InnerException != null)
                    throw Get_Command_Process_Status.InnerException;
                else if (ApplicationProcess_Controller.DLMSNotify)
                    goto RepeatRead_Loop;

                if (Get_Command_Process_Status.Response_Type == GET_Method.Normal)
                    return;
                else if (Get_Command_Process_Status.Response_Type == GET_Method.With_Block)
                {
                    #region Debugging & Logging
#if  Enable_DEBUG_ECHO
                    if (ApplicationProcess_Controller.ActivityLogger != null)
                        ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("Get_HelperAsync Receiving Get Request With Blocking {0} {1} {2}", GetRequestObj.INDEX,
                        GetRequestObj.OBISIndex, ApplicationProcess_Controller.GetCommunicationObject));
#endif
                    #endregion
                    await GetReceive_WithBlockASync(Get_Command_Process_Status, GetAbortToken);
                }
                #region Debugging & Logging
#if  Enable_DEBUG_ECHO
                if (ApplicationProcess_Controller.ActivityLogger != null)
                    ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("Get_HelperAsync completed successfully {0} {1} {2}", GetRequestObj.INDEX,
                    GetRequestObj.OBISIndex, ApplicationProcess_Controller.GetCommunicationObject));
#endif
                #endregion
            }
            catch (Exception ex)
            {
                #region Exception_Handle

                if (ex is DLMSDecodingException || ex is DLMSEncodingException)
                    throw ex;
                else if (ex is DLMSException)
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
                else if (ex is CryptoException)
                    throw ex;
                else if (ex is IOException)
                    throw ex;
                else if (ex is ThreadAbortException)
                    throw ex;
                else
                {
                    throw new DLMSException(String.Format("Error occurred in Get_HelperAsync {0},{1}", GetRequestObj.INDEX,
                        GetRequestObj.OBISIndex), ex);
                }

                #endregion
            }
            finally
            {
                #region Trying Decode Partial GET_Response

                try
                {
                    if (Get_Command_Process_Status != null && !Get_Command_Process_Status.isSuccess)
                    {
                        // try Decode Received Partial Data
                        if (Get_Command_Process_Status.Response_Type != GET_Method.Normal)
                        {
                            if (Get_Command_Process_Status.ReceivedBuffer != null &&
                                Get_Command_Process_Status.ReceivedBuffer.Length >= 1)
                            {
                                #region Debugging & Logging
#if  Enable_DEBUG_ECHO
                                if (ApplicationProcess_Controller.ActivityLogger != null)
                                    ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("Trying Decode GET With Blocking Partial Response", GetRequestObj.INDEX,
                                    GetRequestObj.OBISIndex, ApplicationProcess_Controller.GetCommunicationObject));

#endif
                                #endregion
                                int array_traverse = 0;
                                byte[] t_Buffer = Get_Command_Process_Status.ReceivedBuffer;
                                GetRequestObj.Decode_Data(ref t_Buffer, ref array_traverse, t_Buffer.Length);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    #region Debugging & Logging
#if  Enable_DEBUG_ECHO

                    if (ApplicationProcess_Controller.ActivityLogger != null)
                        ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("Error occurred while Processing {0}", ex, GetRequestObj.INDEX,
                        GetRequestObj.OBISIndex, ApplicationProcess_Controller.GetCommunicationObject));

#endif
                    #endregion
                }

                #endregion
                internalGETProcessStruct.InitStruct();
            }
        }


        /// <summary>
        /// This method transmit and receive GET_WITH_BLOCK APDUs(packets)
        /// </summary>
        /// <remarks>
        /// GetReceive_WithBlock function transmits Next Get Block Service Request,receives Next Get Block Service Response
        /// and validate the block # expected and received
        /// </remarks>
        /// <param name="Get_Process_Structure_Local"><see cref="Get_Process_Structure"/> the last GetResponse_Decoded struct</param>
        /// <param name="GetAbortToken">The GET_Received With Blocking Process Abort Thread_Token</param>
        internal async Task GetReceive_WithBlockASync(Get_Process_Structure Get_Process_Structure_Local = null, CancellationTokenSource GetAbortToken = null)
        {
            stGET GET_from_AL = null;
            try
            {
                if (Get_Process_Structure_Local.Object_Received == null)
                {
                    new ArgumentNullException("Get_Process_Structure_Local.Object_Received");
                }
                if (Get_Process_Structure_Local.Response_Type != GET_Method.With_Block ||
                    Get_Process_Structure_Local.Block_NumberReceived != 1)
                    new ArgumentNullException("Get_Process_Structure_Local.Block_NumberReceived");

                ApplicationProcess_Controller.DLMSNotify = false;
                Get_Process_Structure_Local.Request_Type = GET_req_Packet_Type.NEXT;
                Get_Process_Structure_Local.Last_Block = false;

                // Proceed Till Last Block Received
                while (!Get_Process_Structure_Local.Last_Block &&
                        Get_Process_Structure_Local.Block_NumberReceived < ApplicationProcess_Controller.Max_GET_BLOCK_Count)
                {
                    // Loop_WriteRead:
                    if (GetAbortToken != null &&
                        GetAbortToken.IsCancellationRequested)
                    {
                        #region // Initial Cancel Current GET Next Request

                        Get_Process_Structure_Local.Block_NumberTransfer = UInt32.MaxValue;
                        Get_Process_Structure_Local.Block_NumberReceived = UInt32.MaxValue;
                        ApplicationProcess_Controller.ApplicationProcess.GET_to_AL.Block_Number = (uint)ApplicationProcess_Controller.Max_GET_BLOCK_Count;
                        ApplicationProcess_Controller.ApplicationProcess.Get_Next.Block_Number = (uint)ApplicationProcess_Controller.Max_GET_BLOCK_Count;

                        #endregion
                        #region Debugging & Logging
#if  Enable_DEBUG_ECHO
                       if (ApplicationProcess_Controller.ActivityLogger != null)
                            ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("GetReceive_WithBlock Reset GET_Next_Abort {0} {1} {2}", Get_Process_Structure_Local.Object_Received,
                                                                                    Get_Process_Structure_Local.Object_Received, ApplicationProcess_Controller.GetCommunicationObject));
#endif
                        #endregion
                        // Sending Data Read Request
                        await SendRequest_GetHelperASync(Get_Process_Structure_Local);
                        // T.Wait();
                        // Receive Data Read Response
                        await ReceiveResponse_GetHelperASync(Get_Process_Structure_Local);

                        // Request Get Next Abort
                        GetAbortToken.Token.ThrowIfCancellationRequested();
                    }

                    #region Debugging & Logging
#if  Enable_DEBUG_ECHO

                    Base_Class OBj_T = Get_Process_Structure_Local.Object_Received;

                    if (ApplicationProcess_Controller.ActivityLogger != null)
                        ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("Get_HelperAsync Sending Get Request {0} {1} With Block {2} {3}", OBj_T.INDEX,
                       OBj_T.OBISIndex, Get_Process_Structure_Local.Block_NumberReceived, ApplicationProcess_Controller.GetCommunicationObject));

#endif
                    #endregion
                    ApplicationProcess_Controller.DLMSNotify = false;
                    // Sending Data Read Request
                    await SendRequest_GetHelperASync(Get_Process_Structure_Local);
                    // T_lit.Wait();

                    // Receive Data Read Response
                RepeatRead_Loop:
                    try
                    {
                        await ReceiveResponse_GetHelperASync(Get_Process_Structure_Local);
                        Get_Process_Structure_Local.Retry = 0;
                    }
                    catch (Exception ex)
                    {
                        if (ex.IsIODataTimeOut() &&
                            ApplicationProcess_Controller.DLMSNotify &&
                            Get_Process_Structure_Local.Retry < ApplicationProcess_Controller.MAX_Request_Retry)
                        {
                            Get_Process_Structure_Local.Retry++;
                            continue; // goto Loop_WriteRead;
                        }
                        else
                            // Raise Error
                            throw ex;
                    }

                    #region Populate Get_Process_Structure_Local internal variables here

                    // Copy Internal Get Decoder Structure Here
                    GET_from_AL = ApplicationProcess_Controller.ApplicationProcess.GET_from_AL;
                    // Populate Internal Variable
                    Get_Process_Structure_Local.Response_Type = (GET_Method)GET_from_AL.Response_Type;
                    // Either Get Response Or Last Block Package
                    Get_Process_Structure_Local.Block_NumberReceived = GET_from_AL.Block_Number;
                    Get_Process_Structure_Local.Last_Block = Convert.ToBoolean(GET_from_AL.DataBlock_G.Last_Block);
                    Get_Process_Structure_Local.Block_NumberTransfer = GET_from_AL.Block_Number;
                    // Validate Block # here

                    #endregion

                    if (Get_Process_Structure_Local.PacketDrop ||
                        Get_Process_Structure_Local.InnerException != null ||
                        Get_Process_Structure_Local.Response_Type != GET_Method.With_Block)
                        throw Get_Process_Structure_Local.InnerException;
                    else if (ApplicationProcess_Controller.DLMSNotify)
                        goto RepeatRead_Loop;

                    if (Get_Process_Structure_Local.Block_NumberReceived !=
                        Get_Process_Structure_Local.Block_NumberTransfer)
                        throw new DLMSException(String.Format("Invalid block number received in GetReceive_WithBlock {0} (Error Code:{1})",
                            Get_Process_Structure_Local.Block_NumberReceived, (int)DLMSErrors.Invalid_BlockNumber));
                    #region Debugging & Logging
#if  Enable_DEBUG_ECHO

                    OBj_T = Get_Process_Structure_Local.Object_Received;

                    if (ApplicationProcess_Controller.ActivityLogger != null)
                        ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("Get_HelperAsync Get Response {0} {1} With Block {2} {3}", OBj_T.INDEX,
                        OBj_T.OBISIndex, Get_Process_Structure_Local.Block_NumberReceived,
                         ApplicationProcess_Controller.GetCommunicationObject));

#endif
                    #endregion

                }

                // Validate Process Completed Here
                if (!Get_Process_Structure_Local.Last_Block ||
                     Get_Process_Structure_Local.PacketDrop)
                    throw new DLMSException("Error completing GetReceive_WithBlock");
            }
            catch (Exception ex)
            {
                #region Handle_Case

                if (ex is DLMSDecodingException ||
                    ex is DLMSEncodingException ||
                    ex is DLMSException)
                    throw ex;
                else if (ex is CryptoException)
                    throw ex;
                else if (ex is IOException)
                    throw ex;
                else if (ex is ThreadAbortException)
                    throw ex;
                else
                {
                    throw new DLMSException(String.Format("Error occurred in GET_Receive_With_Blocking {0}", 
                              Get_Process_Structure_Local.Object_Received), ex);
                }

                #endregion
            }
            return;
        }



        /// <summary>
        /// This method transmit and receive GET_WITH_BLOCK APDUs(packets)
        /// </summary>
        /// <remarks>
        /// GetReceive_WithBlock function transmits Next Get Block Service Request,receives Next Get Block Service Response
        /// and validate the block # expected and received
        /// </remarks>
        /// <param name="Get_Process_Structure_Local"><see cref="Get_Process_Structure"/> the last GetResponse_Decoded Struct</param>
        /// <param name="GetAbortToken">The GET_Received With Blocking Process Abort Thread_Token</param>
        internal void GetReceive_WithBlock(Get_Process_Structure Get_Process_Structure_Local = null, CancellationTokenSource GetAbortToken = null)
        {
            stGET GET_from_AL = null;
            try
            {
                if (Get_Process_Structure_Local.Object_Received == null)
                {
                    new ArgumentNullException("Get_Process_Structure_Local.Object_Received");
                }
                if (Get_Process_Structure_Local.Response_Type != GET_Method.With_Block ||
                    Get_Process_Structure_Local.Block_NumberReceived != 1)
                    new ArgumentNullException("Get_Process_Structure_Local.Block_NumberReceived");

                ApplicationProcess_Controller.DLMSNotify = false;
                Get_Process_Structure_Local.Request_Type = GET_req_Packet_Type.NEXT;
                Get_Process_Structure_Local.Last_Block = false;

                // Proceed Till Last Block Received
                while (!Get_Process_Structure_Local.Last_Block &&
                        Get_Process_Structure_Local.Block_NumberReceived < ApplicationProcess_Controller.Max_GET_BLOCK_Count)
                {
                    // Loop_WriteRead:
                    if (GetAbortToken != null &&
                        GetAbortToken.IsCancellationRequested)
                    {
                        #region // Initial Cancel Current GET Next Request

                        Get_Process_Structure_Local.Block_NumberTransfer = UInt32.MaxValue;
                        Get_Process_Structure_Local.Block_NumberReceived = UInt32.MaxValue;
                        ApplicationProcess_Controller.ApplicationProcess.GET_to_AL.Block_Number = (uint)ApplicationProcess_Controller.Max_GET_BLOCK_Count;
                        ApplicationProcess_Controller.ApplicationProcess.Get_Next.Block_Number = (uint)ApplicationProcess_Controller.Max_GET_BLOCK_Count;

                        #endregion
                        #region Debugging & Logging
#if  Enable_DEBUG_ECHO
                       if (ApplicationProcess_Controller.ActivityLogger != null)
                            ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("GetReceive_WithBlock Reset GET_Next_Abort {0} {1} {2}", Get_Process_Structure_Local.Object_Received,
                                                                                    Get_Process_Structure_Local.Object_Received, ApplicationProcess_Controller.GetCommunicationObject));
#endif
                        #endregion
                        // Sending Data Read Request
                        SendRequest_GetHelper(Get_Process_Structure_Local);
                        // Receive Data Read Response
                        ReceiveResponse_GetHelper(Get_Process_Structure_Local);
                        // Request Get Next Abort
                        GetAbortToken.Token.ThrowIfCancellationRequested();
                    }

                    #region Debugging & Logging
#if  Enable_DEBUG_ECHO
                    Base_Class OBj_T = Get_Process_Structure_Local.Object_Received;

                    if (ApplicationProcess_Controller.ActivityLogger != null)
                        ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("Get_HelperAsync Sending Get Request {0} {1} With Block {2} {3}", OBj_T.INDEX,
                       OBj_T.OBISIndex, Get_Process_Structure_Local.Block_NumberReceived, ApplicationProcess_Controller.GetCommunicationObject));
#endif
                    #endregion

                    ApplicationProcess_Controller.DLMSNotify = false;
                    // Sending Data Read Request
                    SendRequest_GetHelper(Get_Process_Structure_Local);

                RepeatRead_Loop:
                    try
                    {
                        // Receive Data Read Response
                        ReceiveResponse_GetHelper(Get_Process_Structure_Local);
                        Get_Process_Structure_Local.Retry = 0;
                    }
                    catch (Exception ex)
                    {
                        if (ex.IsIODataTimeOut() &&
                            ApplicationProcess_Controller.DLMSNotify &&
                            Get_Process_Structure_Local.Retry < ApplicationProcess_Controller.MAX_Request_Retry)
                        {
                            Get_Process_Structure_Local.Retry++;
                            continue;  // goto Loop_WriteRead;
                        }
                        else
                            // Raise Error
                            throw ex;
                    }

                    #region Populate Get_Process_Structure_Local internal variables here

                    // Copy Internal Get Decoder Structure Here
                    GET_from_AL = ApplicationProcess_Controller.ApplicationProcess.GET_from_AL;
                    // Populate Internal Variable
                    Get_Process_Structure_Local.Response_Type = (GET_Method)GET_from_AL.Response_Type;
                    // Either Get Response Or Last Block Package
                    Get_Process_Structure_Local.Block_NumberReceived = GET_from_AL.Block_Number;
                    Get_Process_Structure_Local.Last_Block = Convert.ToBoolean(GET_from_AL.DataBlock_G.Last_Block);
                    Get_Process_Structure_Local.Block_NumberTransfer = GET_from_AL.Block_Number;
                    // Validate Block # here

                    #endregion

                    if (Get_Process_Structure_Local.PacketDrop ||
                        Get_Process_Structure_Local.Response_Type != GET_Method.With_Block ||
                        Get_Process_Structure_Local.InnerException != null)
                        throw Get_Process_Structure_Local.InnerException;
                    else if (ApplicationProcess_Controller.DLMSNotify)
                        goto RepeatRead_Loop;

                    if (Get_Process_Structure_Local.Block_NumberReceived != Get_Process_Structure_Local.Block_NumberTransfer)
                        throw new DLMSException(String.Format("Invalid block number received in GetReceive_WithBlock {0} (Error Code:{1})",
                                Get_Process_Structure_Local.Block_NumberReceived, (int)DLMSErrors.Invalid_BlockNumber));
                    #region Debugging & Logging
#if  Enable_DEBUG_ECHO
                    OBj_T = Get_Process_Structure_Local.Object_Received;

                    if (ApplicationProcess_Controller.ActivityLogger != null)
                        ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("Get_HelperAsync Get Response {0} {1} With Block {2} {3}", OBj_T.INDEX,
                        OBj_T.OBISIndex, Get_Process_Structure_Local.Block_NumberReceived,
                         ApplicationProcess_Controller.GetCommunicationObject));

#endif
                    #endregion
                }

                // Validate Process Completed Here
                if (!Get_Process_Structure_Local.Last_Block || Get_Process_Structure_Local.PacketDrop)
                    throw new DLMSException("Error completing GetReceive_WithBlock");
            }
            catch (Exception ex)
            {
                #region Handle_Case

                if (ex is DLMSDecodingException ||
                    ex is DLMSEncodingException ||
                    ex is DLMSException)
                    throw ex;
                else if (ex is CryptoException)
                    throw ex;
                else if (ex is IOException)
                    throw ex;
                else if (ex is ThreadAbortException)
                    throw ex;
                else
                {
                    throw new DLMSException(String.Format("Error occurred in GET_Receive_With_Blocking {0}", Get_Process_Structure_Local.Object_Received), ex);
                }

                #endregion
            }
            return;
        }

        #endregion

        #region Support_Functions

        #region Support_Functions_ASync



        /// <summary>
        /// This method Decode GET_Service response after receiving the decoded data from channel Asynchronously
        /// </summary>
        /// <param name="Get_Process_Structure_Local"><see cref="Get_Process_Structure"/> the last GetResponse_Decoded struct</param>
        internal async Task ReceiveResponse_GetHelperASync(Get_Process_Structure Get_Process_Structure_Local)
        {
            try
            {
                // Receive Get Response Async
                var res = await ApplicationProcess_Controller.
                    ReceiveResponseFromPhysicalLayerAsync(ApplicationProcess_Controller.LocalBuffer, 0,
                                                          ApplicationProcess_Controller.LocalBuffer.Length);
                Get_Process_Structure_Local.Get_HelperDecode(ApplicationProcess_Controller, ApplicationProcess_Controller.LocalBuffer, 0, res);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method Encodes GET_Service request and transmit the encoded APDU over channel Asynchronously
        /// </summary>
        /// <param name="Get_Process_Structure_Local"><see cref="Get_Process_Structure"/> the last GetResponse_Encoded Struct</param>
        internal async Task SendRequest_GetHelperASync(Get_Process_Structure Get_Process_Structure_Local)
        {
            try
            {
                byte[] T_LcocalBuffer = null;
                int offset = 0, count = 0;
                T_LcocalBuffer = ApplicationProcess_Controller.LocalBuffer;
                count = T_LcocalBuffer.Length;
                /// Encode Get Request Packet
                Get_Process_Structure_Local.Get_HelperEncode(ApplicationProcess_Controller, ref T_LcocalBuffer, ref offset, ref count);
                var res = await ApplicationProcess_Controller.SendRequestFromPhysicalLayerAsync(T_LcocalBuffer, offset, count);
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Support_Functions_Sync



        /// <summary>
        /// This method Decode GET_Service response after receiving the decoded data from channel
        /// </summary>
        /// <param name="Get_Process_Structure_Local"><see cref="Get_Process_Structure"/> the last GetResponse_Decoded struct</param>
        internal void ReceiveResponse_GetHelper(Get_Process_Structure Get_Process_Structure_Local)
        {
            try
            {
                // Receive Get Response
                var res = ApplicationProcess_Controller.
                    ReceiveResponseFromPhysicalLayer(ApplicationProcess_Controller.LocalBuffer, 0, ApplicationProcess_Controller.LocalBuffer.Length);
                Get_Process_Structure_Local.Get_HelperDecode(ApplicationProcess_Controller, ApplicationProcess_Controller.LocalBuffer, 0, res);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// This method Encodes GET_Service request and transmit the encoded APDU over channel
        /// </summary>
        /// <param name="Get_Process_Structure_Local"><see cref="Get_Process_Structure"/> the last GetResponse_Encoded struct</param>
        internal void SendRequest_GetHelper(Get_Process_Structure Get_Process_Structure_Local)
        {
            try
            {
                byte[] T_LcocalBuffer = null;
                int offset = 0, count = 0;
                T_LcocalBuffer = ApplicationProcess_Controller.LocalBuffer;
                count = T_LcocalBuffer.Length;
                // Encode Get Request Packet
                Get_Process_Structure_Local.Get_HelperEncode(ApplicationProcess_Controller, ref T_LcocalBuffer, ref offset, ref count);
                ApplicationProcess_Controller.SendRequestFromPhysicalLayer(T_LcocalBuffer, offset, count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #endregion
    }

    #region Get_Process_Structure

    /// <summary>
    /// Helper class provides structure used by GET_Service
    /// </summary>
    /// <remarks>
    /// The class structure is initialized and process during <see cref="ApplicationProcess_GET_Controller.GetReceive_WithBlockASync(Get_Process_Structure,CancellationTokenSource)"/>,
    /// <see cref="ApplicationProcess_GET_Controller.ReceiveResponse_GetHelperASync(Get_Process_Structure)"/> and <see cref="ApplicationProcess_GET_Controller.SendRequest_GetHelperASync(Get_Process_Structure)"/>
    /// methods executions
    ///</remarks>
    public class Get_Process_Structure
    {
        #region Data_Members

        /// <summary>
        /// This event notify GET_Service WITH_BLOCK decoding Complete
        /// <see cref="Get_next_sender"/>
        /// </summary>
        internal event Get_next_sender Application_Process_Get_Recieved_With_Block = null;
        /// <summary>
        /// This event notify GET_Service decoding Complete
        /// <see cref="DecodingCompleted"/>
        /// </summary>
        internal event DecodingCompleted Application_Process_GET_Response = null;
        /// <summary>
        /// This event Notify COSEM Transport Layer Packet dropped
        /// </summary>
        internal event Action<PacketType, String> ApplicationLayer_PacketDrop = null;
        /// <summary>
        /// This event notify Transport Layer Packet received
        /// </summary>
        internal event Action<object> Packet_Received = null;

        /// <summary>
        /// The DLMS_COSEM instance used in GET_Service
        /// </summary>
        public Base_Class Object_Received;
        /// <summary>
        /// The GET_Service Response_Type<see cref="GET_Method"/>,GET_NORMAL,GET_WITH_BLOCK
        /// </summary>
        public GET_Method Response_Type;
        /// <summary>
        /// The GET_Service Request_Type<see cref="GET_req_Packet_Type"/>,Normal,Next
        /// </summary>
        public GET_req_Packet_Type Request_Type;
        /// <summary>
        /// The block Number Received In GET_Service WITH_BLOCK Response
        /// </summary>
        public UInt32 Block_NumberReceived;
        /// <summary>
        /// The block Number encoded In GET_Service WITH_BLOCK Request
        /// </summary>
        public UInt32 Block_NumberTransfer;
        /// <summary>
        /// The flag notify either GET_Service WITH_BLOCK response received
        /// </summary>
        public bool Last_Block;
        /// <summary>
        /// The flag notify either IO Packet dropped on Transport Layer
        /// </summary>
        public bool PacketDrop;

        /// <summary>
        /// The Counter to remember Message Request Resent
        /// </summary>
        internal int Retry = 0;

        /// <summary>
        /// This exception store error that occurred
        /// </summary>
        /// <remarks>
        /// The error occurred during last GET_Service Encoding or Decoding process is stored
        /// </remarks>
        public Exception InnerException;
        public bool isSuccess = false;
        bool isPorcessComplete = false;
        /// <summary>
        /// The IO_Buffer to be used for GET_Service Encoding or Decoding process
        /// </summary>
        internal byte[] ReceivedBuffer = null;

        internal bool HLS_Applied = false;
        internal bool isTagMatch = false;

        // internal int TestData_Handle = 20;

        #endregion

        /// <summary>
        /// Default Constructs
        /// </summary>
        public Get_Process_Structure()
        {
            InitStruct();
        }

        internal void InitStruct()
        {
            Object_Received = null;
            Response_Type = GET_Method.Normal;
            Request_Type = GET_req_Packet_Type.NORMAL;

            Block_NumberReceived = 1;
            Block_NumberTransfer = 1;
            Last_Block = false;
            PacketDrop = false;
            Retry = 0;
            InnerException = null;
            ReceivedBuffer = null;
        }

        /// <summary>
        /// Get_HelperDecode function helps to decode packet received
        /// </summary>
        /// <remarks>
        /// This function helps to decode packet received and verify either valid response received.
        /// After Decoding the received GET Response it updates Decoding Process Structure Get_Process_Structure
        /// to reflect the Decoding Status
        /// </remarks>
        /// <param name="ApplicationProcess_Controller">provides high level services </param>
        /// <param name="localBuffer">IO buffer to be used for decoding</param>
        /// <param name="offset">int:OffSet to the encoded buffered</param>
        /// <param name="count">int:Number of bytes encoded</param>
        /// <returns>Task that represent the pending IO to Complete</returns>
        internal void Get_HelperDecode(ApplicationProcess_Controller ApplicationProcess_Controller, byte[] localBuffer, int offset, int count)
        {
            try
            {
                // ApplicationProcess_Controller.Dettach_Handlers();

                String error_Message = String.Empty;
                stGET GET_from_AL = null;
                isPorcessComplete = false;

                #region // Event Handler Code Receive Decoding Errors & Exceptions

                if (ApplicationLayer_PacketDrop == null)
                    ApplicationLayer_PacketDrop =
                    (PacketType packetType, string errorMessage) =>
                    {
                        InnerException = new DLMSException(String.Format("{0} Packet drop,error details {1}", packetType, error_Message));
                        PacketDrop = true;
                        ApplicationProcess_Controller.DLMSNotify = false;
                        isSuccess = false;
                        #region Copy Internal Get Decoder Structure Here

                        // Copy Internal Get Decoder Structure Here
                        GET_from_AL = ApplicationProcess_Controller.ApplicationProcess.GET_from_AL;
                        // Update Get Response Type
                        Response_Type = (GET_Method)GET_from_AL.Response_Type;
                        // Copy Received Buffer
                        if (GET_from_AL.Response_Type == (byte)GET_Method.Normal)
                        {
                            ReceivedBuffer = GET_from_AL.Data;
                        }
                        else if (GET_from_AL.Response_Type == (byte)GET_Method.With_Block ||
                            GET_from_AL.Response_Type == (byte)GET_Method.With_List)
                        {
                            if (GET_from_AL.DataBlock_G != null &&
                                GET_from_AL.DataBlock_G.Raw_Data != null)
                                ReceivedBuffer = GET_from_AL.DataBlock_G.Raw_Data.ToArray();
                            else
                                ReceivedBuffer = null;
                        }

                        #endregion
                        isPorcessComplete = true;
                    };

                #endregion

                #region Event Handler Receive Get_Service_Response

                if (Application_Process_GET_Response == null)
                    Application_Process_GET_Response =
                    (Base_Class Arg) =>
                    {

                        Object_Received = Arg;
                        isSuccess = true;
                        ApplicationProcess_Controller.DLMSNotify = false;
                        //Copy Internal Get Decoder Structure Here
                        GET_from_AL = ApplicationProcess_Controller.ApplicationProcess.GET_from_AL;
                        //Populate Internal Variables
                        Response_Type = (GET_Method)GET_from_AL.Response_Type;
                        ReceivedBuffer = GET_from_AL.Data;
                        //Either Get Response Or Last Block Package
                        Last_Block = true;
                        isPorcessComplete = true;
                    };

                #endregion
                #region Event_Handler Decode Get Received

                if (Application_Process_Get_Recieved_With_Block == null)
                    Application_Process_Get_Recieved_With_Block =
                    () =>
                    {
                        ApplicationProcess_Controller.DLMSNotify = false;
                        #region Copy_Internal_Get_Decoder_Structure_Here

                        // Copy Internal Get Decoder Structure Here
                        GET_from_AL = ApplicationProcess_Controller.ApplicationProcess.GET_from_AL;
                        // Update Get Response Type
                        Response_Type = (GET_Method)GET_from_AL.Response_Type;
                        // Either Get Response Or Last Block Package
                        Block_NumberReceived = GET_from_AL.Block_Number;
                        Last_Block = Convert.ToBoolean(GET_from_AL.DataBlock_G.Last_Block);

                        #region Copy Received Buffer

                        // Copy Received Buffer
                        if (GET_from_AL.Response_Type == (byte)GET_Method.Normal)
                        {
                            ReceivedBuffer = GET_from_AL.Data;
                        }
                        else if (GET_from_AL.Response_Type == (byte)GET_Method.With_Block ||
                            GET_from_AL.Response_Type == (byte)GET_Method.With_List)
                        {
                            if (GET_from_AL.DataBlock_G != null && GET_from_AL.DataBlock_G.Raw_Data != null && Last_Block)
                                ReceivedBuffer = GET_from_AL.DataBlock_G.Raw_Data.ToArray();
                            else
                                ReceivedBuffer = null;
                        }

                        #endregion

                        #endregion

                        isSuccess = false;
                        isPorcessComplete = true;

                    };

                #endregion

                #region Delegate_Packet_Received

                // if (Packet_Received == null)
                //    Packet_Received = new Action<object>((object localBufferArg) =>
                //    {
                //        byte[] TBuf = new byte[((ArraySegment<byte>)localBufferArg).Count];
                //        Array.Copy(((ArraySegment<byte>)localBufferArg).Array, ((ArraySegment<byte>)localBufferArg).Offset, TBuf, 0, TBuf.Length);
                //        ApplicationProcess_Controller.ApplicationProcess.ApplicationLayer.packetReceived(TBuf);
                //    });

                #endregion

                #region Attach_Local_Function_Handlers

                ApplicationProcess_Controller.ApplicationProcess.ApplicationLayer.PacketDrop += ApplicationLayer_PacketDrop;
                ApplicationProcess_Controller.ApplicationProcess.GET_Recieved_With_Block += Application_Process_Get_Recieved_With_Block;
                ApplicationProcess_Controller.ApplicationProcess.GET_Response += Application_Process_GET_Response;

                #endregion

                #region //  Apply Packet_Decryption Then Send Packet For IO Decoding

                if (localBuffer == null || localBuffer.Length == 0 || count <= 0)
                    throw new IOException("Invalid data buffer to be decoded received_DLMS_Controller_GETIOData");

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
                    throw new DLMSException("Error occurred while Process GET_Decode_Packet,unknown packet type Get_HelperDecode_Get_Process_Structure");
            }
            #region Catch Handlers

            catch (DLMSDecodingException ex)
            {
                throw ex;
            }
            catch (CryptoException ex)
            {
                throw ex;
            }
            catch (DLMSException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DLMSException("Error occurred while decoding Get_HelperDecode_Get_Process_Structure", ex);
                // throw ex;
            }

            #endregion
            finally
            {
                #region Deattach_Local_Function_Handlers

                ApplicationProcess_Controller.ApplicationProcess.ApplicationLayer.PacketDrop -= ApplicationLayer_PacketDrop;
                ApplicationProcess_Controller.ApplicationProcess.GET_Recieved_With_Block -= Application_Process_Get_Recieved_With_Block;
                ApplicationProcess_Controller.ApplicationProcess.GET_Response -= Application_Process_GET_Response;

                #endregion
            }
        }

        /// <summary>
        /// Encodes the Packet (Get_Request_Normal,Get_Request_Next_Block )
        /// </summary>
        /// <param name="ApplicationProcess_Controller"><see cref="ApplicationProcess_Controller"/> </param>
        /// <param name="localBuffer">IO buffer to be used for encoding</param>
        /// <param name="offset">int:OffSet to the encoded buffered</param>
        /// <param name="count">int:Number of bytes encoded</param>
        internal void Get_HelperEncode(ApplicationProcess_Controller ApplicationProcess_Controller, ref byte[] localBuffer, ref int offset, ref int count)
        {
            ArraySegment<byte> Encoded_Packet;
            byte[] Packet = null;
            try
            {
                if (Object_Received == null)
                    throw new ArgumentNullException("Null Argument Exception Get_Process_Structure_Lcoal");

                // Encode Get Service Normal Request
                if (Request_Type == GET_req_Packet_Type.NORMAL)
                {
                    Packet = ApplicationProcess_Controller.ApplicationProcess.Encode_GET_Object(Object_Received);
                }
                else
                {
                    Packet = ApplicationProcess_Controller.ApplicationProcess.Encode_Get_Next_Block();
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

                // Only For HLS Authentication Type And SecurityControl is not 'None'
                if (ApplicationProcess_Controller.ApplicationProcess.Send_APPLICATION_ASSOCIATION.Authentication_Mechanism >= HLS_Mechanism.HLS_Manufac &&
                    ApplicationProcess_Controller.Security_Data != null &&
                    ApplicationProcess_Controller.Security_Data.SecurityControl > SecurityControl.None)
                {
                    if (ApplicationProcess_Controller.Crypto == null ||
                        ApplicationProcess_Controller.Security_Data == null ||
                       !ApplicationProcess_Controller.Security_Data.IsInitialized)
                    {
                        throw new CryptoException(string.Format("Security Data not initialized properly to proceed for HLS Communication (Error Code:{0})",
                            (int)DLMSErrors.Invalid_SecurityData_EK));

                    }

                    ArraySegment<byte> AuthenticatedEncrypted_Packet;

                    if (ApplicationProcess_Controller.ApplicationProcess.Negotiated_Conformance.GeneralProtection)
                    {
                        // Authenticated_Encryption
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
                            ApplicationProcess_Controller.Security_Data.GloMessageTAG = (DLMSCommand)DLMS_Common.GetGloDLMSCommmandTAG(DLMSCommand.GetRequest);

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

                #region // Copy Encoded_Buffer

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
                throw new Exception("Error occurred while Encoding Object ", ex);
            }
        }
    }

    #endregion
}
