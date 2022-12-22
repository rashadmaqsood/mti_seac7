// #define Enable_DEBUG_ECHO
#define Enable_IO_Logging

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DLMS.Comm;
using Serenity.Crypto;

namespace DLMS
{
    /// <summary>
    /// The ApplicationProcess_SET_Controller is Sub_Interface/Controller class to manage the flow and control for DLMS_COSEM SET Service
    /// The ApplicationProcess_SET_Controller provides the DLMS_COSEM Protocol service,SET_Service 
    /// <see cref="SET_Helper(Base_Class)"/>,<see cref="SET_Helper(Base_Class,SetResult)"/>
    /// <see cref="SET_HelperAsync(Base_Class,CancellationTokenSource)"/>
    /// </summary>
    public class ApplicationProcess_SET_Controller
    {
        #region Data_Members

        private ApplicationProcess_Controller internalAProcess_Controller = null;
        internal SET_Process_Structure SET_Command_Process_Status = null;

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
        public ApplicationProcess_SET_Controller()
        {
            SET_Command_Process_Status = new SET_Process_Structure();
        }

        #region Member_Functions

        /// <summary>
        /// Write data to DLMS_COSEM device using DLMS_SET Service
        /// </summary>
        /// <remarks>
        /// This function is implemented using Sync Communication model
        /// </remarks>
        ///<exception cref="IOException">IOException Class</exception>
        ///<exception cref="DLMSException">DLMSException Class</exception>
        ///<exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        ///<exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <param name="ObjectToSet">COSEM Object to be used to write data</param>
        /// <returns><see cref="Data_Access_Result"/> the result/OutCome of DLMS_SET Service</returns>
        public Data_Access_Result SET_Helper(Base_Class SetRequestObj, CancellationTokenSource SETAbortToken = null)
        {
            try
            {
                if (SetRequestObj == null)
                    throw new ArgumentNullException("GetRequestObj");

                // Resend Set Normal Request
            Loop_WriteRead: ApplicationProcess_Controller.DLMSNotify = false;
                SET_Command_Process_Status.Object_Received = SetRequestObj;
                SET_Command_Process_Status.Request_Type = SET_req_Type.NORMAL;
                SET_Command_Process_Status.Response_Type = SET_Response_Type.NORMAL;

                #region Debugging & Logging

#if  Enable_DEBUG_ECHO

                if (ApplicationProcess_Controller.ActivityLogger != null)
                    ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("SET_Helper Sending Normal Set Request {0} {1} {2}", SetRequestObj.INDEX,
                                                                            SetRequestObj.OBISIndex, ApplicationProcess_Controller.GetCommunicationObject));

#endif
                #endregion
                //Send SET Service Request
                SendRequest_SETHelper(SET_Command_Process_Status);

                #region Debugging & Logging

#if  Enable_DEBUG_ECHO

                if (ApplicationProcess_Controller.ActivityLogger != null)
                    ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("SET_Helper Receiving Set Request {0} {1} {2}",
                                            SetRequestObj.INDEX, SetRequestObj.OBISIndex, ApplicationProcess_Controller.GetCommunicationObject));
#endif

                #endregion

            RepeatRead_Loop:
                try
                {
                    // Receive Get_Service_Response_Normal
                    ReceiveResponse_SETHelper(SET_Command_Process_Status);
                    // Reset Retry Counter
                    SET_Command_Process_Status.Retry = 0;
                }
                catch (Exception ex)
                {
                    if (ex.IsIODataTimeOut() &&
                        ApplicationProcess_Controller.DLMSNotify &&
                        SET_Command_Process_Status.Retry < ApplicationProcess_Controller.MAX_Request_Retry)
                    {
                        SET_Command_Process_Status.Retry++;
                        goto Loop_WriteRead;
                    }
                    else
                        // Raise Error
                        throw ex;
                }

                // Packet Drop Raise Error
                if (SET_Command_Process_Status.PacketDrop ||
                    SET_Command_Process_Status.InnerException != null)
                    throw SET_Command_Process_Status.InnerException;
                // Event Notification Receive
                else if (ApplicationProcess_Controller.DLMSNotify)
                    goto RepeatRead_Loop;

                if (SET_Command_Process_Status.Response_Type == SET_Response_Type.NORMAL)
                    return SET_Command_Process_Status.Result;
                else if (SET_Command_Process_Status.Response_Type == SET_Response_Type.Non_Last_Data_BLOCK ||
                    SET_Command_Process_Status.Response_Type == SET_Response_Type.Last_Data_Block)
                {
                    #region Debugging & Logging

#if  Enable_DEBUG_ECHO
                    if (ApplicationProcess_Controller.ActivityLogger != null)
                        ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("SET_Helper Sending SET Request With Blocking {0} {1} {2}", SetRequestObj.INDEX,
                        SetRequestObj.OBISIndex, ApplicationProcess_Controller.GetCommunicationObject));
#endif

                    #endregion
                    SET_WithBlock(SET_Command_Process_Status, SETAbortToken);
                }
                #region Debugging & Logging
#if  Enable_DEBUG_ECHO
                if (ApplicationProcess_Controller.ActivityLogger != null)
                    ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("SET_HelperAsync completed successfully {0} {1} {2}", SetRequestObj.INDEX,
                    SetRequestObj.OBISIndex, ApplicationProcess_Controller.GetCommunicationObject));
#endif
                #endregion
                return SET_Command_Process_Status.Result;
            }
            catch (Exception ex)
            {
                #region Exception_Handle

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
                    throw new DLMSException(String.Format("Error occurred in SET_HelperAsync {0},{1}", SetRequestObj.INDEX,
                        SetRequestObj.OBISIndex), ex);
                }

                #endregion
            }
            finally
            {
                SET_Command_Process_Status.InitStruct();
            }
        }

        /// <summary>
        /// This method will Write data to DLMS_COSEM device using DLMS_SET Service
        /// </summary>
        /// <remarks>
        /// This function is implemented using Task based ASync Communication model
        /// </remarks>
        /// <exception cref="IOException">IOException Class</exception>
        /// <exception cref="DLMSException">DLMSException Class</exception>
        /// <exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        /// <exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <param name="ObjectToSet">COSEM Object to be used to write data</param>
        /// <returns>Task&ls;Data_Access_Result&gt;represent pending IO to Complete</returns>
        public async Task<Data_Access_Result> SET_HelperAsync(Base_Class SetRequestObj, CancellationTokenSource SETAbortToken = null)
        {
            try
            {
                if (SetRequestObj == null)
                    throw new ArgumentNullException("GetRequestObj");
            // Resend Set Normal Request
            Loop_WriteRead: SET_Command_Process_Status.Object_Received = SetRequestObj;
                ApplicationProcess_Controller.DLMSNotify = false;
                SET_Command_Process_Status.Request_Type = SET_req_Type.NORMAL;
                SET_Command_Process_Status.Response_Type = SET_Response_Type.NORMAL;
                #region Debugging & Logging
#if  Enable_DEBUG_ECHO

                if (ApplicationProcess_Controller.ActivityLogger != null)
                    ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("SET_HelperAsync Sending Normal Set Request {0} {1} {2}", SetRequestObj.INDEX,
                    SetRequestObj.OBISIndex, ApplicationProcess_Controller.GetCommunicationObject));

#endif
                #endregion
                // Send SET Service Request
                await SendRequest_SETHelperASync(SET_Command_Process_Status);
            // T.Wait();
                #region Debugging & Logging

#if  Enable_DEBUG_ECHO

                if (ApplicationProcess_Controller.ActivityLogger != null)
                    ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("SET_HelperAsync Receiving Set Request {0} {1} {2}", SetRequestObj.INDEX,
                    SetRequestObj.OBISIndex, ApplicationProcess_Controller.GetCommunicationObject));

#endif

                #endregion

            RepeatRead_Loop:
                try
                {
                    // Receive Get_Service_Response_Normal
                    await ReceiveResponse_SETHelperASync(SET_Command_Process_Status);
                    // Reset Retry Counter
                    SET_Command_Process_Status.Retry = 0;
                }
                catch (Exception ex)
                {
                    if (ex.IsIODataTimeOut() &&
                        ApplicationProcess_Controller.DLMSNotify &&
                        SET_Command_Process_Status.Retry < ApplicationProcess_Controller.MAX_Request_Retry)
                    {
                        SET_Command_Process_Status.Retry++;
                        goto Loop_WriteRead;
                    }
                    else
                        // Raise Error
                        throw ex;
                }

                if (SET_Command_Process_Status.PacketDrop ||
                    SET_Command_Process_Status.InnerException != null)
                    throw SET_Command_Process_Status.InnerException;
                else if (ApplicationProcess_Controller.DLMSNotify)
                    goto RepeatRead_Loop;

                if (SET_Command_Process_Status.Response_Type == SET_Response_Type.NORMAL)
                    return SET_Command_Process_Status.Result;
                else if (SET_Command_Process_Status.Response_Type == SET_Response_Type.Non_Last_Data_BLOCK ||
                    SET_Command_Process_Status.Response_Type == SET_Response_Type.Last_Data_Block)
                {
                    #region Debugging & Logging

#if  Enable_DEBUG_ECHO

                    if (ApplicationProcess_Controller.ActivityLogger != null)
                        ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("SET_HelperAsync Sending SET Request With Blocking {0} {1} {2}", SetRequestObj.INDEX,
                        SetRequestObj.OBISIndex, ApplicationProcess_Controller.GetCommunicationObject));

#endif

                    #endregion
                    await SET_WithBlockASync(SET_Command_Process_Status, SETAbortToken);
                }
                #region Debugging & Logging
#if  Enable_DEBUG_ECHO

                if (ApplicationProcess_Controller.ActivityLogger != null)
                    ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("SET_HelperAsync completed successfully {0} {1} {2}", SetRequestObj.INDEX,
                    SetRequestObj.OBISIndex, ApplicationProcess_Controller.GetCommunicationObject));

#endif
                #endregion
                return SET_Command_Process_Status.Result;
            }
            catch (Exception ex)
            {
                #region Exception_Handle

                if (ex is DLMSDecodingException ||
                    ex is DLMSEncodingException)
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
                    throw new DLMSException(String.Format("Error occurred in SET_HelperAsync {0},{1}", SetRequestObj.INDEX,
                                            SetRequestObj.OBISIndex), ex);
                }

                #endregion
            }
            finally
            {
                SET_Command_Process_Status.InitStruct();
            }
        }

        #endregion

        #region Support_Functions

        /// <summary>
        /// This helper method send Next SET_WithBlockASync Service Request and Response
        /// </summary>
        /// <remarks>
        /// This helper method transmits Next SET_WithBlock Service Request,receives Next SET_WITH_BLOCK Service Response
        /// and validate the block # expected and received.This helper method is implemented using Task based A Sync Communication Model.
        /// </remarks>
        /// <param name="SET_Process_Structure_Local">Structure the last SET Response Decoded</param>
        /// <param name="SetAbortToken">The Cancellation token to Abort SET_WITH_BLOCK Service</param>
        /// <returns>Task that represent the pending IO to Complete</returns>
        internal async Task SET_WithBlockASync(SET_Process_Structure SET_Process_Structure_Local, CancellationTokenSource SetAbortToken = null)
        {
            try
            {
                if (SET_Process_Structure_Local.Object_Received == null)
                {
                    new ArgumentNullException("SET_Process_Structure_Local.Object_Received");
                }
                if (SET_Process_Structure_Local.Response_Type == SET_Response_Type.NORMAL ||
                    SET_Process_Structure_Local.Block_NumberReceived != 01 ||
                    SET_Process_Structure_Local.Block_NumberTransfer != 02)
                    new ArgumentNullException("SET_Process_Structure_Local invalid send/received block number");

                SET_Process_Structure_Local.Request_Type = SET_req_Type.Non_First_Data_Block;
                SET_Process_Structure_Local.Last_Block = false;
                ApplicationProcess_Controller.DLMSNotify = false;

                // Proceed Till Last Block Received
                while (SET_Process_Structure_Local.Block_NumberTransfer <= SET_Process_Structure_Local.MaxSETBlocks &&
                       SET_Process_Structure_Local.Block_NumberTransfer < ApplicationProcess_Controller.Max_SET_BLOCK_Count &&
                       !SET_Process_Structure_Local.Last_Block)
                {
                    // Request SET Next Abort
                    // Loop_WriteRead:
                    ApplicationProcess_Controller.DLMSNotify = false;
                    if (SetAbortToken != null)
                        SetAbortToken.Token.ThrowIfCancellationRequested();

                    #region Debugging & Logging
#if  Enable_DEBUG_ECHO

                    Base_Class OBj_T = SET_Process_Structure_Local.Object_Received;
                    if (ApplicationProcess_Controller.ActivityLogger != null)
                        ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("SET_HelperAsync Sending SET Request {0} {1} With Block {2} {3}", OBj_T.INDEX,
                        OBj_T.OBISIndex, SET_Process_Structure_Local.Block_NumberReceived, ApplicationProcess_Controller.GetCommunicationObject));

#endif
                    #endregion
                    // Send Set Request With Blocking
                    await SendRequest_SETHelperASync(SET_Process_Structure_Local);

                    // Modification For On-Going ASYNC Test
                    // Wait On-Going Write Operation
                    // if (SetAbortToken != null)
                    // {
                    //     T.Wait(SetAbortToken.Token);
                    // }
                    // else
                    // {
                    //     T.Wait();
                    // }

                RepeatRead_Loop:
                    try
                    {
                        // Read Response For Last Data Write Command
                        await ReceiveResponse_SETHelperASync(SET_Process_Structure_Local);
                        // Reset Retry Counter
                        SET_Command_Process_Status.Retry = 0;
                    }
                    catch (Exception ex)
                    {
                        if (ex.IsIODataTimeOut() &&
                            ApplicationProcess_Controller.DLMSNotify &&
                            SET_Command_Process_Status.Retry < ApplicationProcess_Controller.MAX_Request_Retry)
                        {
                            SET_Command_Process_Status.Retry++;
                            continue; // goto Loop_WriteRead;
                        }
                        else
                            // Raise Error
                            throw ex;
                    }

                    if (SET_Process_Structure_Local.PacketDrop ||
                        SET_Process_Structure_Local.InnerException != null)
                        throw SET_Process_Structure_Local.InnerException;
                    else if (ApplicationProcess_Controller.DLMSNotify)
                        goto RepeatRead_Loop;

                    if (SET_Process_Structure_Local.Block_NumberReceived + 1 !=
                        SET_Process_Structure_Local.Block_NumberTransfer)
                        throw new DLMSException(String.Format("Invalid block number received in SETReceive_WithBlock {0} (Error Code:{1})",
                            SET_Process_Structure_Local.Block_NumberReceived, (int)DLMSErrors.Invalid_BlockNumber));
                     #region Debugging & Logging
#if  Enable_DEBUG_ECHO

                    OBj_T = SET_Process_Structure_Local.Object_Received;
                    if (ApplicationProcess_Controller.ActivityLogger != null)
                        ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("SET_HelperAsync SET Response {0} {1} With Block {2} {3}", OBj_T.INDEX,
                        OBj_T.OBISIndex, SET_Process_Structure_Local.Block_NumberReceived, ApplicationProcess_Controller.GetCommunicationObject));

#endif
                    #endregion
                }

                // Validate Process Completed Here
                if (!SET_Process_Structure_Local.Last_Block ||
                    SET_Process_Structure_Local.PacketDrop)
                    throw new DLMSException(String.Format("Error Completing SETReceive_WithBlock (Error Code:{0})",
                                            (int)DLMSErrors.ServiceFailure_SET));
            }
            catch (Exception ex)
            {
                #region Handle_Case

                if (ex is DLMSDecodingException ||
                    ex is DLMSEncodingException)
                    throw ex;
                else if (ex is CryptoException)
                    throw ex;
                else if (ex is DLMSException)
                    throw ex;
                else if (ex is IOException)
                    throw ex;
                else if (ex is ThreadAbortException)
                    throw ex;
                else
                {
                    throw new DLMSException(String.Format("Error occurred in SET_Receive_With_Blocking {0} (Error Code:{1})",
                         SET_Process_Structure_Local.Object_Received, (int)DLMSErrors.ServiceFailure_SET), ex);
                }

                #endregion
            }
            return;
        }

        /// <summary>
        /// This helper method send Next SET_WithBlock Service Request and Response
        /// </summary>
        /// <remarks>
        /// This helper method transmits Next SET_WithBlock Service Request,receives Next SET_WITH_BLOCK Service Response
        /// and validate the block# expected and received.
        /// </remarks>
        /// <param name="SET_Process_Structure_Local">Structure the last SET Response Decoded</param>
        /// <param name="SetAbortToken">The Cancellation token to Abort SET_WITH_BLOCK Service</param>
        /// <returns>Task that represent the pending IO to Complete</returns>
        internal void SET_WithBlock(SET_Process_Structure SET_Process_Structure_Local, CancellationTokenSource SetAbortToken = null)
        {
            try
            {
                if (SET_Process_Structure_Local.Object_Received == null)
                {
                    new ArgumentNullException("SET_Process_Structure_Local.Object_Received");
                }

                if (SET_Process_Structure_Local.Response_Type == SET_Response_Type.NORMAL ||
                    SET_Process_Structure_Local.Block_NumberReceived != 01 || SET_Process_Structure_Local.Block_NumberTransfer != 02)
                    new ArgumentNullException("SET_Process_Structure_Local invalid send/received block number");

                ApplicationProcess_Controller.DLMSNotify = false;
                SET_Process_Structure_Local.Request_Type = SET_req_Type.Non_First_Data_Block;
                SET_Process_Structure_Local.Last_Block = false;

                // Proceed Till Last Block Received
                while (SET_Process_Structure_Local.Block_NumberTransfer <= SET_Process_Structure_Local.MaxSETBlocks &&
                       SET_Process_Structure_Local.Block_NumberTransfer < ApplicationProcess_Controller.Max_SET_BLOCK_Count &&
                       !SET_Process_Structure_Local.Last_Block)
                {
                    // Request SET Next Abort
                    // Loop_WriteRead:
                    ApplicationProcess_Controller.DLMSNotify = false;
                    if (SetAbortToken != null)
                        SetAbortToken.Token.ThrowIfCancellationRequested();
                    #region Debugging & Logging
#if  Enable_DEBUG_ECHO

                    Base_Class OBj_T = SET_Process_Structure_Local.Object_Received;
                    if (ApplicationProcess_Controller.ActivityLogger != null)
                        ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("SET_HelperAsync Sending SET Request {0} {1} With Block {2} {3}", OBj_T.INDEX,
                        OBj_T.OBISIndex, SET_Process_Structure_Local.Block_NumberReceived, ApplicationProcess_Controller.GetCommunicationObject));

#endif
                    #endregion
                    // Send Set Request With Blocking
                    SendRequest_SETHelper(SET_Process_Structure_Local);

                    // Read Response For Last Data Write Command
                RepeatRead_Loop:
                    try
                    {
                        ReceiveResponse_SETHelper(SET_Process_Structure_Local);
                        // Reset Retry Counter
                        SET_Command_Process_Status.Retry = 0;
                    }
                    catch (Exception ex)
                    {
                        if (ex.IsIODataTimeOut() &&
                            ApplicationProcess_Controller.DLMSNotify &&
                            SET_Command_Process_Status.Retry < ApplicationProcess_Controller.MAX_Request_Retry)
                        {
                            SET_Command_Process_Status.Retry++;
                            continue; // goto Loop_WriteRead;
                        }
                        else
                            // Raise Error
                            throw ex;
                    }

                    if (SET_Process_Structure_Local.PacketDrop ||
                        SET_Process_Structure_Local.InnerException != null)
                        throw SET_Process_Structure_Local.InnerException;
                    else if (ApplicationProcess_Controller.DLMSNotify)
                        goto RepeatRead_Loop;

                    if (SET_Process_Structure_Local.Block_NumberReceived + 1 !=
                        SET_Process_Structure_Local.Block_NumberTransfer)
                        throw new DLMSException(String.Format("Invalid block number received in SETReceive_WithBlock {0} (Error Code:{1})",
                                                SET_Process_Structure_Local.Block_NumberReceived, (int)DLMSErrors.Invalid_BlockNumber));

                    #region Debugging & Logging
#if  Enable_DEBUG_ECHO

                    OBj_T = SET_Process_Structure_Local.Object_Received;
                    if (ApplicationProcess_Controller.ActivityLogger != null)
                        ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("SET_HelperAsync SET Response {0} {1} With Block {2} {3}", OBj_T.INDEX,
                        OBj_T.OBISIndex, SET_Process_Structure_Local.Block_NumberReceived, ApplicationProcess_Controller.GetCommunicationObject));

#endif
                    #endregion
                }
                // validate Process Completed Here
                if (!SET_Process_Structure_Local.Last_Block ||
                    SET_Process_Structure_Local.PacketDrop)
                    throw new DLMSException(String.Format("Error completing SETReceive_WithBlock (Error Code:{0})",
                        (int)DLMSErrors.ServiceFailure_SET));
            }
            catch (Exception ex)
            {
                #region Handle_Case

                if (ex is DLMSDecodingException ||
                    ex is DLMSEncodingException)
                    throw ex;
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
                    throw new DLMSException(String.Format("Error occurred in SET_Receive_With_Blocking {0} (Error Code:{1})",
                        SET_Process_Structure_Local.Object_Received, (int)DLMSErrors.ServiceFailure_SET), ex);
                }

                #endregion
            }
            return;
        }


        /// <summary>
        ///  This helper method receive SET Service Response then decode Response
        /// </summary>
        /// <remarks>
        /// This function is implemented Task based Async Communication Model
        /// </remarks>
        /// <param name="Set_Process_Structure_Local">Structure the last SET Response Decoded</param>
        /// <returns>Task that represent the pending IO Structure Complete</returns>
        internal async Task ReceiveResponse_SETHelperASync(SET_Process_Structure Set_Process_Structure_Local)
        {
            try
            {
                byte[] T_LcocalBuffer = null;
                int offset = 0, count = 0;
                T_LcocalBuffer = ApplicationProcess_Controller.LocalBuffer;
                count = T_LcocalBuffer.Length;
                // Receive Get Response Async
                var res = await ApplicationProcess_Controller.ReceiveResponseFromPhysicalLayerAsync(T_LcocalBuffer, offset, count);
                Set_Process_Structure_Local.SET_HelperDecode(ApplicationProcess_Controller, T_LcocalBuffer, offset, res);
                return;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///  This helper method receive SET Service Response then decode Response
        /// </summary>
        /// <param name="Set_Process_Structure_Local">Structure the last SET Response Decoded</param>
        /// <returns>Task that represent the pending IO to Complete</returns>
        internal int ReceiveResponse_SETHelper(SET_Process_Structure Set_Process_Structure_Local)
        {
            try
            {
                byte[] T_LcocalBuffer = null;
                int offset = 0, count = 0;
                T_LcocalBuffer = ApplicationProcess_Controller.LocalBuffer;
                count = T_LcocalBuffer.Length;
                // Receive SET Response
                var res = ApplicationProcess_Controller.ReceiveResponseFromPhysicalLayer(T_LcocalBuffer, offset, count);
                Set_Process_Structure_Local.SET_HelperDecode(ApplicationProcess_Controller, T_LcocalBuffer, offset, res);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Encodes SET_Service request and transmits
        /// </summary>
        /// <remarks>
        /// This function is implemented on Task based Async Communication Model
        /// </remarks>
        /// <param name="SET_Process_Structure_Local">Structure the last SET Request Encoded</param>
        /// <returns>Task that represent the pending IO to Complete</returns>
        internal async Task<int> SendRequest_SETHelperASync(SET_Process_Structure SET_Process_Structure_Local)
        {
            try
            {
                byte[] T_LcocalBuffer = null;
                int offset = 0, count = 0;
                T_LcocalBuffer = ApplicationProcess_Controller.LocalBuffer;
                count = T_LcocalBuffer.Length;
                // Encode Get Request Packet
                SET_Process_Structure_Local.SET_HelperEncode(ApplicationProcess_Controller, ref T_LcocalBuffer, ref offset, ref count);
                var res = await ApplicationProcess_Controller.SendRequestFromPhysicalLayerAsync(T_LcocalBuffer, offset, count);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Encodes SET_Service request and transmits
        /// </summary>
        /// <param name="SET_Process_Structure_Local">Structure the last SET Request Encoded</param>
        /// <returns>int:byte count transmitted</returns>
        internal int SendRequest_SETHelper(SET_Process_Structure SET_Process_Structure_Local)
        {
            try
            {
                byte[] T_LcocalBuffer = null;
                int offset = 0, count = 0;
                T_LcocalBuffer = ApplicationProcess_Controller.LocalBuffer;
                count = T_LcocalBuffer.Length;
                // Encode Get Request Packet
                SET_Process_Structure_Local.SET_HelperEncode(ApplicationProcess_Controller, ref T_LcocalBuffer, ref offset, ref count);
                ApplicationProcess_Controller.SendRequestFromPhysicalLayer(T_LcocalBuffer, offset, count);
                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }

    #region SET_Process_Structure

    /// <summary>
    /// This class is used as storage structure to be used during the SET_Service internal member method execution
    /// </summary>
    /// <remarks>
    /// The class structure is initialized and processed during <see cref="ApplicationProcess_SET_Controller.
    /// SET_WithBlock(SET_Process_Structure,CancellationTokenSource)"/>,
    /// <see cref="ApplicationProcess_SET_Controller.ReceiveResponse_SETHelperASync(SET_Process_Structure)"/> and
    /// <see cref="ApplicationProcess_SET_Controller.SendRequest_SETHelperASync(SET_Process_Structure)"/> methods executions
    ///</remarks>
    class SET_Process_Structure
    {
        #region Data_Members

        #region Event_Handlers

        /// <summary>
        /// This event notify SET_Service WITH_BLOCK decoding Complete
        /// <see cref="SET_Next_Sender"/>
        /// </summary>
        internal event SET_Next_Sender Application_Process_Set_Response_With_Block = null;
        /// <summary>
        /// This event notify SET_Service decoding Process Complete
        /// <see cref="SetResult"/>
        /// </summary>
        internal event SetResult Application_Process_SET_Response = null;
        /// <summary>
        /// This event Notify COSEM Transport Layer Packet dropped
        /// </summary>
        internal event Action<PacketType, String> ApplicationLayer_PacketDrop = null;
        /// <summary>
        /// This event notify Transport Layer Packet received
        /// </summary>
        internal event Action<object> Packet_Received = null;

        #endregion

        /// <summary>
        /// The instance used to Write Data to DLMS_COSEM Compliant Device
        /// </summary>
        internal Base_Class Object_Received = null;
        /// <summary>
        /// The SET_Service SET_Response_Type<see cref="SET_Response_Type"/>
        /// </summary>
        internal SET_Response_Type Response_Type;
        /// <summary>
        /// The SET_Service SET_Request_Type<see cref="SET_req_Type"/>
        /// </summary>
        internal SET_req_Type Request_Type;
        /// <summary>
        /// The SET_Service returned OutCome<see cref="Data_Access_Result"/>
        /// </summary>
        internal Data_Access_Result Result;
        /// <summary>
        /// The Encoded_Buffer to be used for SET_Service Encoding or Decoding process
        /// </summary>
        internal byte[] Encoded_Buffer = null;
        /// <summary>
        /// The block Number Received In SET_Service WITH_BLOCK Response
        /// </summary>
        internal UInt32 Block_NumberReceived;
        /// <summary>
        /// The block Number encoded In SET_Service WITH_BLOCK Request
        /// </summary>
        internal UInt32 Block_NumberTransfer;
        /// <summary>
        /// The Max Number Of Requests can make In SET_Service WITH_BLOCK
        /// </summary>
        internal int MaxSETBlocks;
        /// <summary>
        /// This flag notify the last SET Request Sent In SET_Service WITH_BLOCK
        /// </summary>
        internal bool Last_Block;
        internal bool PacketDrop;

        /// <summary>
        /// The Counter to remember Message Request Resent
        /// </summary>
        internal int Retry = 0;

        /// <summary>
        /// This exception store error that occurred
        /// </summary>
        /// <remarks>
        /// The error occurred during last SET_Service Encoding or Decoding process is stored
        /// </remarks>
        internal Exception InnerException = null;
        internal bool isPorcessComplete = false;

        internal bool HLS_Applied = false;
        internal bool isTagMatch = false;

        #endregion

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SET_Process_Structure()
        {
            InitStruct();
        }

        /// <summary>
        /// This helper method initialize the Current Structure
        /// </summary>
        internal void InitStruct()
        {
            Object_Received = null;
            Response_Type = SET_Response_Type.NORMAL;
            Request_Type = SET_req_Type.NORMAL;
            Result = Data_Access_Result.Success;

            Encoded_Buffer = null;
            MaxSETBlocks = -1;
            Block_NumberReceived = 0;
            Block_NumberTransfer = 0;
            Last_Block = false;
            PacketDrop = false;
            Retry = 0;
            InnerException = null;
        }

        /// <summary>
        /// This helper function use to decode SET_Service packet received
        /// </summary>
        /// <remarks>
        /// This helper function use to decode SET_Service packet received and verify either valid response received.
        /// After Decoding the received SET Response it updates current instance SET_Process_Structure
        /// to reflect the Decoding Status
        /// </remarks>
        /// <param name="ApplicationProcess_Controller">Provides high level services</param>
        /// <param name="localBuffer">IO buffer to be used for decoding</param>
        /// <param name="offset">int:OffSet to the encoded buffered</param>
        /// <param name="count">int:Number of bytes encoded</param>
        /// <returns>Task that represent the pending IO to Complete</returns>
        internal void SET_HelperDecode(ApplicationProcess_Controller ApplicationProcess_Controller, byte[] localBuffer, int offset, int count)
        {
            try
            {
                /// Initialize Local Variables
                /// ApplicationProcess_Controller.Dettach_Handlers();
                isPorcessComplete = false;

                String error_Message = String.Empty;
                InnerException = null;
                PacketDrop = false;
                stSET SET_from_AL = null;

                #region // Event Handler Code To Receive Decoding Errors & Exception

                if (ApplicationLayer_PacketDrop == null)
                    ApplicationLayer_PacketDrop =
                            (PacketType packetType, string errorMessage) =>
                            {
                                ApplicationProcess_Controller.DLMSNotify = false;
                                InnerException = new DLMSException(String.Format("{0} Packet drop,error details {1}", packetType, error_Message));
                                PacketDrop = true;
                                isPorcessComplete = true;
                            };

                #endregion

                #region Event Handler To Receive SET_Service_Response

                if (Application_Process_SET_Response == null)
                    Application_Process_SET_Response = (Data_Access_Result Arg) =>
                    {
                        ApplicationProcess_Controller.DLMSNotify = false;
                        Result = Arg;
                        SET_from_AL = ApplicationProcess_Controller.ApplicationProcess.SET_from_AL;
                        //Populate Internal Variables
                        Response_Type = (SET_Response_Type)SET_from_AL.Response_Type;
                        //Either SET Response Or Last Block Package
                        Last_Block = true;
                        // Either Block_NumberReceived
                        Block_NumberReceived = SET_from_AL.Block_number;
                        isPorcessComplete = true;
                    };

                #endregion
                #region Event_Handler To Decode SET Received With Block

                if (Application_Process_Set_Response_With_Block == null)
                    Application_Process_Set_Response_With_Block = (uint blockNumber) =>
                    {
                        ApplicationProcess_Controller.DLMSNotify = false;
                        Block_NumberReceived = blockNumber;
                        // Copy Internal Get Decoder Structure Here
                        SET_from_AL = ApplicationProcess_Controller.ApplicationProcess.SET_from_AL;
                        //Populate Internal Variables
                        Response_Type = (SET_Response_Type)SET_from_AL.Response_Type;
                        //Either Get Response Or Last Block Package
                        Last_Block = Convert.ToBoolean(SET_from_AL.DataBlock_SA.Last_Block);
                        isPorcessComplete = true;
                    };

                #endregion

                #region Delegate_Packet_Received

                // if (Packet_Received == null)
                //     Packet_Received = new Action<object>((object localBufferArg) =>
                //     {
                //         byte[] TBuf = new byte[((ArraySegment<byte>)localBufferArg).Count];
                //         Array.Copy(((ArraySegment<byte>)localBufferArg).Array, ((ArraySegment<byte>)localBufferArg).Offset, TBuf, 0, TBuf.Length);
                //         ApplicationProcess_Controller.ApplicationProcess.ApplicationLayer.packetReceived(TBuf);
                //     });

                #endregion

                #region Attach_Local_Function_Handlers

                ApplicationProcess_Controller.ApplicationProcess.ApplicationLayer.PacketDrop += ApplicationLayer_PacketDrop;
                ApplicationProcess_Controller.ApplicationProcess.SET_Response_With_Block += Application_Process_Set_Response_With_Block;
                ApplicationProcess_Controller.ApplicationProcess.SET_Response += Application_Process_SET_Response;

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
                if (!isPorcessComplete
                    && !ApplicationProcess_Controller.DLMSNotify)
                    throw new DLMSException("Error occurred while Process Set_Decode_Packet,unknown packet type");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                #region Deattach_Local_Function_Handlers

                ApplicationProcess_Controller.ApplicationProcess.ApplicationLayer.PacketDrop -= ApplicationLayer_PacketDrop;
                ApplicationProcess_Controller.ApplicationProcess.SET_Response_With_Block -= Application_Process_Set_Response_With_Block;
                ApplicationProcess_Controller.ApplicationProcess.SET_Response -= Application_Process_SET_Response;

                #endregion
            }
        }

        /// <summary>
        /// Encodes Request (SET_Request_Normal,SET_Request_Next_Block) 
        /// </summary>
        /// <param name="ApplicationProcess_Controller">Provides High Level Services</param>
        /// <param name="localBuffer">IO buffer to be used for encoding</param>
        /// <param name="offset">int:OffSet to the encoded buffered</param>
        /// <param name="count">int:Number of bytes encoded</param>
        internal void SET_HelperEncode(ApplicationProcess_Controller ApplicationProcess_Controller, ref byte[] localBuffer, ref int offset, ref int count)
        {
            ArraySegment<byte> Encoded_Packet;
            byte[] Packet = null;
            try
            {
                if (Object_Received == null)
                    throw new ArgumentNullException("Null Argument Exception SET_Process_Structure_Lcoal");

                stSET SET_APStruct = null;

                // Encode SET Service Normal Request
                if (Request_Type == SET_req_Type.NORMAL || Block_NumberReceived == 0)
                {
                    // Encode NExt Buffer To be Sent
                    Packet = ApplicationProcess_Controller.ApplicationProcess.
                                         Encode_SET_Object(Object_Received, Object_Received.EncodingAttribute);
                    SET_APStruct = ApplicationProcess_Controller.ApplicationProcess.SET_to_AL;
                    // Copy Encoded SET Buffer Here
                    Encoded_Buffer = ApplicationProcess_Controller.ApplicationProcess.SetBuf;
                    Request_Type = (SET_req_Type)SET_APStruct.Request_Type;

                    Block_NumberTransfer = ApplicationProcess_Controller.ApplicationProcess.internalBlockNum;
                    Block_NumberReceived = 0x01;
                    MaxSETBlocks = ApplicationProcess_Controller.ApplicationProcess.MaxSETBlock;
                    Last_Block = Convert.ToBoolean(SET_APStruct.DataBlock_SA.Last_Block);

                }
                else if (Request_Type == SET_req_Type.First_Data_BLOCK ||
                         Request_Type == SET_req_Type.Non_First_Data_Block)
                {
                    // Encode NExt Buffer To be Sent
                    Packet = ApplicationProcess_Controller.ApplicationProcess.
                                         Encode_SET_Next(Block_NumberReceived);

                    SET_APStruct = ApplicationProcess_Controller.ApplicationProcess.SET_to_AL;
                    Request_Type = (SET_req_Type)SET_APStruct.Request_Type;
                    Block_NumberTransfer = ApplicationProcess_Controller.ApplicationProcess.internalBlockNum;
                    Last_Block = Convert.ToBoolean(SET_APStruct.DataBlock_SA.Last_Block);
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
                        throw new CryptoException("Security Data not initialized properly to proceed for HLS Communication");
                    }
                    // Authenticated_Encryption
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
                        // Update Glo_Message Command_TAG
                        if (ApplicationProcess_Controller.Security_Data != null)
                            ApplicationProcess_Controller.Security_Data.GloMessageTAG = (DLMSCommand)DLMS_Common.GetGloDLMSCommmandTAG(DLMSCommand.SetRequest);

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
                throw new Exception("Error occurred while Encoding Object", ex);
            }
        }
    }

    #endregion
}
