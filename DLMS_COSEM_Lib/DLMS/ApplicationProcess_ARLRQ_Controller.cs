///#define Enable_DEBUG_ECHO
#define Enable_Error_Logging
#define Enable_IO_Logging

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLMS.Comm;
using System.IO;

namespace DLMS
{
    /// <summary>
    /// The ApplicationProcess_ARLRQ_Controller is an Sub Interface/Controller class to manage 
    /// the flow and the control for DLMS_COSEM ARLRQ(Association Release Request) Service
    /// The ApplicationProcess_ARLRQ_Controller provides the DLMS_COSEM Protocol service to release current association,ARLRQ_Service <see cref="ARLRQ_Helper()"/>
    /// </summary>
    public class ApplicationProcess_ARLRQ_Controller
    {
        #region Data_Members

        internal ApplicationProcess_Controller internalAProcess_Controller = null;
        internal ARLRQ_Process_Structure LocalCommStruct = null;

        #endregion

        #region Properties

        /// <summary>
        /// GET/SET ApplicationProcess_Controller<see cref="ApplicationProcess_Controller"/>
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
        public ApplicationProcess_ARLRQ_Controller()
        {
            LocalCommStruct = new ARLRQ_Process_Structure();
        }

        #region Member_Functions

        /// <summary>
        /// This method helps to release association with DLMS_COSEM device using DLMS_COSEM ARLRQ Service or Logout Manually
        /// </summary>
        /// <remarks>
        /// This function is implemented using Sync Communication model
        /// </remarks>
        ///<exception cref="IOException">IOException Class</exception>
        ///<exception cref="DLMSException">DLMSException Class</exception>
        ///<exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        ///<exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <returns>Either Is Disconnected or Not</returns>
        public bool ARLRQ_Helper(SAP_Object MeterSAP, SAP_Object ClientSAP)
        {
            ApplicationProcess_Controller.ApplicationProcess.CurrentClientSAP = ClientSAP;
            ApplicationProcess_Controller.ApplicationProcess.CurrentMeterSAP = MeterSAP;
            return ARLRQ_Helper();
        }

        /// <summary>
        /// This method helps to release association with DLMS_COSEM device using DLMS_COSEM ARLRQ Service
        /// </summary>
        /// <remarks>
        /// This function is implemented using Sync Communication model
        /// </remarks>
        ///<exception cref="IOException">IOException Class</exception>
        ///<exception cref="DLMSException">DLMSException Class</exception>
        ///<exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        ///<exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <returns>Either Is Disconnected or Not</returns>
        public bool ARLRQ_Helper()
        {
            try
            {
            Loop_ReWriteRead:
                ApplicationProcess_Controller.DLMSNotify = false;

                byte[] T_LcocalBuffer = null;
                int offset = 0, count = 0;
                T_LcocalBuffer = ApplicationProcess_Controller.LocalBuffer;
                count = T_LcocalBuffer.Length;

                LocalCommStruct.ARLRQ_HelperEncode(ApplicationProcess_Controller, ref T_LcocalBuffer, ref offset, ref count);
                // Sending ARLRQ Request
                #region Debugging & Logging
#if  Enable_DEBUG_ECHO

                if (ApplicationProcess_Controller.ActivityLogger != null)
                    ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("ARLRQ_HelperAsync Sending ARLRQ Request"));

#endif
                #endregion
                ApplicationProcess_Controller.SendRequestFromPhysicalLayer(T_LcocalBuffer, offset, count);
                #region Debugging & Logging
#if  Enable_DEBUG_ECHO
                
                if (ApplicationProcess_Controller.ActivityLogger != null)
                    ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("ARLRQ_HelperAsync Receive ARLRQ Response"));
                
#endif
                #endregion

            RepeatRead_Loop:
                try
                {
                    int responseLength = ApplicationProcess_Controller.ReceiveResponseFromPhysicalLayer(T_LcocalBuffer, offset, T_LcocalBuffer.Length);
                    // Reset Retry Counter
                    LocalCommStruct.Retry = 0;
                    LocalCommStruct.ARLRQ_HelperDecode(ApplicationProcess_Controller, T_LcocalBuffer, offset, responseLength);
                }
                catch (Exception ex)
                {
                    if (ex.IsIODataTimeOut() &&
                        ApplicationProcess_Controller.DLMSNotify &&
                        LocalCommStruct.Retry < ApplicationProcess_Controller.MAX_Request_Retry)
                    {
                        LocalCommStruct.Retry++;
                        goto Loop_ReWriteRead;
                    }
                    else
                        // Raise Error
                        throw ex;
                }

                // throw packet Drop Exception
                if (LocalCommStruct.InnerException != null)
                    throw LocalCommStruct.InnerException;
                else if (ApplicationProcess_Controller.DLMSNotify)
                    goto RepeatRead_Loop;

                // return login results
                return LocalCommStruct.IsSuccess;
            }
            catch (Exception ex)
            {
                ApplicationProcess_Controller.SetResetIOStream(true);
                if (ex is IOException)
                    throw ex;
                else if (ex is DLMSException)
                    throw ex;
                else
                    throw new DLMSException("Error occurred while releasing application association", ex);
            }
            finally
            {
                LocalCommStruct.InitStruct();
            }
        }

        /// <summary>
        /// This method helps to release association with DLMS_COSEM device using DLMS_COSEM ARLRQ Service Asynchronously
        /// </summary>
        /// <remarks>
        /// this methods implement TPL (Task Parallel Libray) for Asynchronous  Communication model
        /// </remarks>
        ///<exception cref="IOException">IOException Class</exception>
        ///<exception cref="DLMSException">DLMSException Class</exception>
        ///<exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        ///<exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <returns>true if association release successfully ,false if not</returns>
        public async Task<bool> ARLRQ_HelperAsync()
        {
            try
            {
            Loop_ReWriteRead:
                ApplicationProcess_Controller.DLMSNotify = false;
                // ApplicationProcess_Controller.Dettach_Handlers();
                byte[] T_LcocalBuffer = null;
                int offset = 0, count = 0;
                T_LcocalBuffer = ApplicationProcess_Controller.LocalBuffer;
                count = T_LcocalBuffer.Length;
                LocalCommStruct.ARLRQ_HelperEncode(ApplicationProcess_Controller, ref T_LcocalBuffer, ref offset, ref count);
                // Sending AARQ Request
                #region Debugging & Logging
#if  Enable_DEBUG_ECHO
                
                if (ApplicationProcess_Controller.ActivityLogger != null)
                    ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("ARLRQ_HelperAsync Sending ARLRQ Request "));

#endif
                #endregion
                Task T = ApplicationProcess_Controller.SendRequestFromPhysicalLayerAsync(T_LcocalBuffer, offset, count);
                T.Wait();
                #region Debugging & Logging
#if  Enable_DEBUG_ECHO
                
                if (ApplicationProcess_Controller.ActivityLogger != null)
                    ApplicationProcess_Controller.ActivityLogger.LogMessage(String.Format("ARLRQ_HelperAsync Receive ARLRQ Response "));

#endif
                #endregion

            RepeatRead_Loop:
                try
                {
                    int responseLength = await ApplicationProcess_Controller.ReceiveResponseFromPhysicalLayerAsync(T_LcocalBuffer, offset, T_LcocalBuffer.Length);
                    // Reset Retry Counter
                    LocalCommStruct.Retry = 0;
                    LocalCommStruct.ARLRQ_HelperDecode(ApplicationProcess_Controller, T_LcocalBuffer, offset, responseLength);
                }
                catch (Exception ex)
                {
                    if (ex.IsIODataTimeOut() &&
                        ApplicationProcess_Controller.DLMSNotify &&
                        LocalCommStruct.Retry < ApplicationProcess_Controller.MAX_Request_Retry)
                    {
                        LocalCommStruct.Retry++;
                        goto Loop_ReWriteRead;
                    }
                    else
                        // Raise Error
                        throw ex;
                }

                // throw packet Drop Exception
                if (LocalCommStruct.InnerException != null)
                    throw LocalCommStruct.InnerException;
                else if (ApplicationProcess_Controller.DLMSNotify)
                    goto RepeatRead_Loop;

                // return login results
                return LocalCommStruct.IsSuccess;
            }
            catch (Exception ex)
            {
                ApplicationProcess_Controller.SetResetIOStream(true);
                if (ex is IOException)
                    throw ex;
                else if (ex is DLMSException)
                    throw ex;
                else
                    throw new DLMSException("Error occurred while releasing application association", ex);
            }
            finally
            {
                LocalCommStruct.InitStruct();
            }
        }

        #endregion
    }

    /// <summary>
    /// Helper class provides structure used by ARLRQ_Service
    /// </summary>
    /// <remarks>
    /// The class structure is initialized and processed during <see cref="ApplicationProcess_ARLRQ_Controller.ARLRQ_HelperAsync"/>,
    /// this will store response status
    ///</remarks>
    class ARLRQ_Process_Structure
    {
        #region data_members

        /// <summary>
        /// This event will notify when ARLRQ_Service Response decoding Process Completed
        /// </summary>
        internal flagStatus Application_Association = null;
        /// <summary>
        /// This event will notify COSEM Transport Layer when IO Packet dropped
        /// </summary>
        internal Action<PacketType, String> ApplicationLayer_PacketDrop = null;
        /// <summary>
        /// This event will notify Transport Layer when new Packet received
        /// </summary>
        internal Action<object> Packet_Received = null;

        internal bool IsSuccess;
        internal bool PacketDrop;
        /// <summary>
        /// The Counter to Remember Message Request Resent
        /// </summary>
        internal int Retry = 0;

        /// <summary>
        /// This exception store error that occurred
        /// </summary>
        /// <remarks>
        /// The error occurred during last ARLRQ_Service Encoding or ARLRQ Response Decoding process
        /// </remarks>
        internal Exception InnerException = null;
        internal bool isPorcessComplete = false;

        #endregion

        /// <summary>
        /// Initialize <see cref="ARLRQ_Process_Structure"/> with Default values
        /// </summary>
        internal void InitStruct()
        {
            IsSuccess = false;
            PacketDrop = false;
            InnerException = null;
            Retry = 0;
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ARLRQ_Process_Structure()
        {
            IsSuccess = false;
            PacketDrop = false;
            InnerException = null;
        }

        #region Member_Functions

        /// <summary>
        /// helps to decode packet received From DLMS/COSEM device
        /// </summary>
        /// <remarks>
        /// AARQ_HelperDecode decode and verify either response is valid or not
        /// </remarks>
        /// <param name="ApplicationProcess_Controller">AP Controller for receiving or sending packets</param>
        /// <param name="localBuffer">IO buffer to be used for decoding</param>
        /// <param name="offset">int:OffSet index to buffer</param>
        /// <param name="count">int:Number of bytes</param>
        internal void ARLRQ_HelperDecode(ApplicationProcess_Controller ApplicationProcess_Controller,
            byte[] localBuffer, int offset, int count)
        {
            try
            {
                // Initial Work
                // ApplicationProcess_Controller.Dettach_Handlers();

                isPorcessComplete = false;
                String error_Message = String.Empty;
                PacketDrop = false;

                #region // Event Handler Code Receive Decoding Errors & Exception

                if (ApplicationLayer_PacketDrop == null)
                    ApplicationLayer_PacketDrop =
                            (PacketType packetType, string errorMessage) =>
                            {
                                ApplicationProcess_Controller.DLMSNotify = false;
                                InnerException = new DLMSException(String.Format("{0} Packet drop,error details {1}",
                                    packetType, error_Message));
                                PacketDrop = true;
                                isPorcessComplete = true;
                            };

                #endregion

                #region Event Handler To Receive ARLRQ_Response

                if (Application_Association == null)
                    Application_Association = (bool Arg) =>
                    {
                        ApplicationProcess_Controller.DLMSNotify = false;
                        // Populate Internal Variable
                        IsSuccess = Arg;
                        isPorcessComplete = true;
                    };

                #endregion

                #region Delegate_Packet_Received

                if (Packet_Received == null)
                    Packet_Received = new Action<object>((object localBufferArg) =>
                    {
                        byte[] TBuf = new byte[((ArraySegment<byte>)localBufferArg).Count];
                        Array.Copy(((ArraySegment<byte>)localBufferArg).Array, ((ArraySegment<byte>)localBufferArg).Offset, TBuf, 0, TBuf.Length);
                        ApplicationProcess_Controller.ApplicationProcess.ApplicationLayer.packetReceived(TBuf);
                    });

                #endregion

                #region Attach_Local_Function_Handlers

                ApplicationProcess_Controller.ApplicationProcess.ApplicationLayer.PacketDrop += ApplicationLayer_PacketDrop;
                ApplicationProcess_Controller.ApplicationProcess.SAPAssociation += Application_Association;

                #endregion

                #region Debugging & Logging
#if Enable_IO_Logging

                if (ApplicationProcess_Controller.ApplicationProcess.Logger != null)
                    // Log Transmitted Data
                    ApplicationProcess_Controller.ApplicationProcess.Logger.LogALTraffic(localBuffer, offset, count, DataStatus.Read);

#endif
                #endregion


                #region // Send Packet For IO Decoding

                byte[] _TBuf = new byte[count];
                Array.Copy(localBuffer, offset, _TBuf, 0, _TBuf.Length);
                ApplicationProcess_Controller.ApplicationProcess.ApplicationLayer.packetReceived(_TBuf);

                #endregion
                // Check If GET Response Received
                if (!isPorcessComplete &&
                    !ApplicationProcess_Controller.DLMSNotify)
                    throw new DLMSException("Error occurred while Process ARLRQ_Decode_Packet,unknown packet type");

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                #region Deattach_Local_Function_Handlers

                ApplicationProcess_Controller.ApplicationProcess.ApplicationLayer.PacketDrop -= ApplicationLayer_PacketDrop;
                ApplicationProcess_Controller.ApplicationProcess.SAPAssociation -= Application_Association;

                #endregion
            }
        }

        /// <summary>
        /// encodes the ARLRQ (Association Release Request) Request Packet
        /// </summary>
        /// <param name="ApplicationProcess_Controller">AP Controller for receiving or sending packets</param>
        /// <param name="localBuffer">IO buffer to be used for encoding</param>
        /// <param name="offset">int:OffSet to the encoded buffered</param>
        /// <param name="count">int:Number of bytes encoded</param>
        internal void ARLRQ_HelperEncode(ApplicationProcess_Controller ApplicationProcess_Controller, ref byte[] localBuffer, ref int offset, ref int count)
        {
            try
            {

                byte[] Encode_Buffer = null;
                Encode_Buffer = ApplicationProcess_Controller.ApplicationProcess.Encode_Association_Release();

                #region /// Copy_Encoded_Buffer

                if (localBuffer == null)
                {
                    localBuffer = Encode_Buffer;
                    offset = 0;
                    count = localBuffer.Length;
                }
                else
                {
                    Buffer.BlockCopy(Encode_Buffer, 0, localBuffer, offset, Encode_Buffer.Length);
                    count = Encode_Buffer.Length;
                }

                #endregion

                #region Debugging & Logging
#if Enable_IO_Logging

                if (ApplicationProcess_Controller.ApplicationProcess.Logger != null)
                    // Log Transmitted Data
                    ApplicationProcess_Controller.ApplicationProcess.
                        Logger.LogALTraffic(Encode_Buffer,
                                            0,
                                            Encode_Buffer.Length, DataStatus.Write);

#endif
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Encoding Object", ex);
            }
        }

        #endregion

    }

}

