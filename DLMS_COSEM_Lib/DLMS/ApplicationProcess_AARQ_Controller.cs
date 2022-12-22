// #define Enable_DEBUG_ECHO
#define Enable_Error_Logging
#define Enable_IO_Logging

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLMS.Comm;
using System.IO;
using Serenity.Crypto;

namespace DLMS
{
    /// <summary>
    /// The ApplicationProcess_AARQ_Controller is an Sub Interface/Controller class to manage 
    /// the flow and control for DLMS_COSEM AARQ Service
    /// The ApplicationProcess_AARQ_Controller provides the DLMS_COSEM Protocol service,AARQ_Service <see cref="AARQ_Helper(SAP_Object,SAP_Object,String)"/>
    /// </summary>
    public class ApplicationProcess_AARQ_Controller
    {
        #region Data_Members

        internal ApplicationProcess_Controller internalAProcess_Controller = null;
        internal AARQ_Process_Structure LocalCommStruct = null;

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
        /// 
        /// </summary>
        public ApplicationProcess_AARQ_Controller()
        {
            LocalCommStruct = new AARQ_Process_Structure();
        }

        #region Member_Functions

        /// <summary>
        /// This helper method Connect to DLMS_COSEM device using DLMS_COSEM AARQ Service
        /// </summary>
        /// <remarks>
        /// This function is implemented using Sync Communication model
        /// </remarks>
        ///<exception cref="IOException">IOException Class</exception>
        ///<exception cref="DLMSException">DLMSException Class</exception>
        ///<exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        ///<exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <param name="MeterSAP">Meter DLMS_COSEM Service Access Point<see cref="SAP_Object"/></param>
        /// <param name="ClientSAP">Client DLMS_COSEM Service Access Point<see cref="SAP_Object"/></param>
        /// <param name="Password">Valid Password String for <see cref="SAP_Object"/></param>
        /// <returns>Either Is Connected or Not</returns>
        public bool AARQ_Helper(SAP_Object MeterSAP, SAP_Object ClientSAP, String Password)
        {
            try
            {
                byte[] ChallengeStr = null;
                byte[] ChallengeOutStr = null;
                HLS_Mechanism Authentication_Mechanism = HLS_Mechanism.HLS_GMAC;

                /// Public
                if (ClientSAP._SAP_Address == 0x0010)
                {
                    Authentication_Mechanism = HLS_Mechanism.LowestSec;
                }
                // Authenticated
                else /// if (ClientSAP._SAP_Address == 0x0001)
                {
                    Authentication_Mechanism = HLS_Mechanism.LowSec;
                }

                ChallengeStr = DLMS_Common.PrintableStringToByteArray(Password);

                return AARQ_Helper(MeterSAP, ClientSAP, ChallengeStr, ref ChallengeOutStr, Authentication_Mechanism);
            }
            catch (Exception ex)
            {
                ApplicationProcess_Controller.SetResetIOStream(true);

                if (ex is IOException)
                    throw ex;
                else if (ex is DLMSException)
                    throw ex;
                else if (ex is CryptoException)
                    throw ex;
                else
                    throw new DLMSException("Error occurred while creating application association", ex);
            }
            finally
            {
                LocalCommStruct.InitStruct();
            }
        }


        /// <summary>
        /// This helper method Connect to DLMS_COSEM device using DLMS_COSEM AARQ Service
        /// </summary>
        /// <remarks>
        /// This function is implemented using Sync Communication model
        /// </remarks>
        ///<exception cref="IOException">IOException Class</exception>
        ///<exception cref="DLMSException">DLMSException Class</exception>
        ///<exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        ///<exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <param name="MeterSAP">Meter DLMS_COSEM Service Access Point<see cref="SAP_Object"/></param>
        /// <param name="ClientSAP">Client DLMS_COSEM Service Access Point<see cref="SAP_Object"/></param>
        /// <param name="ChallengeStr">Valid Challenge String for <see cref="SAP_Object"/></param>
        /// <param name="ChallengeOutStr">Returned Valid Challenge String from Meter <see cref="SAP_Object"/></param>  
        /// <param name="Authentication_Mechanism">HLS Authentication Mechanism</param>
        /// <returns>Either Is Connected or Not</returns>
        public bool AARQ_Helper(SAP_Object MeterSAP, SAP_Object ClientSAP, byte[] ChallengeStr, ref byte[] ChallengeOutStr,
                                HLS_Mechanism Authentication_Mechanism = HLS_Mechanism.HLS_GMAC)
        {
            try
            {
                // AARQ_Process_Structure LocalCommStruct = new AARQ_Process_Structure();
                LocalCommStruct.MeterSAP = MeterSAP;
                LocalCommStruct.ClientSAP = ClientSAP;
                LocalCommStruct.Authentication_Mechanism = Authentication_Mechanism;
                LocalCommStruct.Password = DLMS_Common.ArrayToPrintableString(ChallengeStr);

                byte[] T_LcocalBuffer = null;
                int offset = 0, count = 0;
                T_LcocalBuffer = ApplicationProcess_Controller.LocalBuffer;
                count = T_LcocalBuffer.Length;

                LocalCommStruct.AARQ_HelperEncode(ApplicationProcess_Controller, ref T_LcocalBuffer, ref offset, ref count);

                // Sending AARQ Request
                ApplicationProcess_Controller.SendRequestFromPhysicalLayer(T_LcocalBuffer, offset, count);
            RepeatRead_Loop: ApplicationProcess_Controller.DLMSNotify = false;
                int responseLength = ApplicationProcess_Controller.ReceiveResponseFromPhysicalLayer(T_LcocalBuffer, offset, T_LcocalBuffer.Length);

                LocalCommStruct.AARQ_HelperDecode(ApplicationProcess_Controller, T_LcocalBuffer, offset, responseLength);

                // throw packet Drop Exception
                if (LocalCommStruct.InnerException != null)
                    throw LocalCommStruct.InnerException;

                if (ApplicationProcess_Controller.DLMSNotify)
                    goto RepeatRead_Loop;

                if (String.IsNullOrEmpty(LocalCommStruct.Password) || String.IsNullOrEmpty(LocalCommStruct.Password_Received))
                    if (Authentication_Mechanism >= HLS_Mechanism.LowSec)
                        throw new DLMSException(String.Format("Invalid Password provided (Error Code:{0})",
                                               (int)DLMSErrors.Password_Error));

                if (!string.IsNullOrEmpty(LocalCommStruct.Password_Received))
                    ChallengeOutStr = DLMS_Common.PrintableStringToByteArray(LocalCommStruct.Password_Received);
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
                else if (ex is CryptoException)
                    throw ex;
                else
                    throw new DLMSException(String.Format("Error occurred while creating application association(Error Code:{0})",
                                            (int)DLMSErrors.ServiceFailure_AARQ), ex);
            }
            finally
            {
                LocalCommStruct.InitStruct();
            }
        }

        /// <summary>
        /// This method Connect to DLMS_COSEM device using DLMS_COSEM AARQ Service
        /// </summary>
        /// <remarks>
        /// This function is implemented using Task Based ASync Communication model
        /// </remarks>
        /// <exception cref="IOException">IOException Class</exception>
        /// <exception cref="DLMSException">DLMSException Class</exception>
        /// <exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        /// <exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <param name="MeterSAP">Meter DLMS_COSEM Service Access Point<see cref="SAP_Object"/></param>
        /// <param name="ClientSAP">Client DLMS_COSEM Service Access Point<see cref="SAP_Object"/></param>
        /// <param name="Password">Valid Password String for <see cref="SAP_Object"/></param>
        /// <returns>Task&ls;bool&gt;;Is Connected or not;Task that represent the pending IO to Complete</returns>
        public async Task<bool> AARQ_HelperAsync(SAP_Object MeterSAP, SAP_Object ClientSAP, String Password = null)
        {
            try
            {
                List<byte> ChallengeStr = new List<byte>();
                List<byte> ChallengeOutStr = new List<byte>();
                HLS_Mechanism Authentication_Mechanism = HLS_Mechanism.HLS_GMAC;

                // Public
                if (ClientSAP._SAP_Address == 0x0010)
                {
                    Authentication_Mechanism = HLS_Mechanism.LowestSec;
                }
                // Authenticated
                else ///if (ClientSAP._SAP_Address == 0x0001)
                {
                    Authentication_Mechanism = HLS_Mechanism.LowSec;
                }

                ChallengeStr.AddRange(DLMS_Common.PrintableStringToByteArray(Password));

                return await AARQ_HelperAsync(MeterSAP, ClientSAP, ChallengeStr, ChallengeOutStr, Authentication_Mechanism);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// This helper method Connect to DLMS_COSEM device using DLMS_COSEM AARQ Service
        /// </summary>
        /// <remarks>
        /// This function is implemented using ASync Communication model
        /// </remarks>
        ///<exception cref="IOException">IOException Class</exception>
        ///<exception cref="DLMSException">DLMSException Class</exception>
        ///<exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        ///<exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <param name="MeterSAP">Meter DLMS_COSEM Service Access Point<see cref="SAP_Object"/></param>
        /// <param name="ClientSAP">Client DLMS_COSEM Service Access Point<see cref="SAP_Object"/></param>
        /// <param name="ChallengeStr">Valid Challenge String for <see cref="SAP_Object"/></param>
        /// <param name="ChallengeOutStr">Returned Valid Challenge String from Meter <see cref="SAP_Object"/></param>  
        /// <param name="Authentication_Mechanism">HLS Authentication Mechanism</param>
        /// <returns>Either Is Connected or Not</returns>
        public async Task<bool> AARQ_HelperAsync(SAP_Object MeterSAP, SAP_Object ClientSAP, List<byte> ChallengeStr, List<byte> ChallengeOutStr,
                                HLS_Mechanism Authentication_Mechanism = HLS_Mechanism.HLS_GMAC)
        {
            try
            {
                // AARQ_Process_Structure LocalCommStruct = new AARQ_Process_Structure();
                LocalCommStruct.MeterSAP = MeterSAP;
                LocalCommStruct.ClientSAP = ClientSAP;
                LocalCommStruct.Authentication_Mechanism = Authentication_Mechanism;
                LocalCommStruct.Password = DLMS_Common.ArrayToPrintableString(ChallengeStr);


                byte[] T_LcocalBuffer = null;
                int offset = 0, count = 0;
                T_LcocalBuffer = ApplicationProcess_Controller.LocalBuffer;
                count = T_LcocalBuffer.Length;

                LocalCommStruct.AARQ_HelperEncode(ApplicationProcess_Controller, ref T_LcocalBuffer, ref offset, ref count);

                // Sending AARQ Request
                var T = await ApplicationProcess_Controller.SendRequestFromPhysicalLayerAsync(T_LcocalBuffer, offset, count);
            RepeatRead_Loop: ApplicationProcess_Controller.DLMSNotify = false;
                int responseLength = await ApplicationProcess_Controller.ReceiveResponseFromPhysicalLayerAsync(T_LcocalBuffer, offset,
                                                                                                               T_LcocalBuffer.Length);

                LocalCommStruct.AARQ_HelperDecode(ApplicationProcess_Controller, T_LcocalBuffer, offset, responseLength);
                
                // throw packet Drop Exception
                if (LocalCommStruct.InnerException != null)
                    throw LocalCommStruct.InnerException;

                if (ApplicationProcess_Controller.DLMSNotify)
                    goto RepeatRead_Loop;

                if (String.IsNullOrEmpty(LocalCommStruct.Password) || String.IsNullOrEmpty(LocalCommStruct.Password_Received))
                    throw new DLMSException(String.Format("Invalid Password provided (Error Code:{0})",
                                           (int)DLMSErrors.Password_Error));


                var Ret_Challenge_STR = DLMS_Common.PrintableStringToByteArray(LocalCommStruct.Password_Received);
                ChallengeOutStr.AddRange(Ret_Challenge_STR);

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
                else if (ex is CryptoException)
                    throw ex;
                else
                    throw new DLMSException(String.Format("Error occurred while creating application association(Error Code:{0})",
                                            (int)DLMSErrors.ServiceFailure_AARQ), ex);
            }
            finally
            {
                LocalCommStruct.InitStruct();
            }
        }

        #endregion
    }

    /// <summary>
    /// Helper class provides structure used by AARQ_Service 
    /// </summary>
    /// <remarks>
    /// The class structure is initialized and processed during <see cref="ApplicationProcess_AARQ_Controller.AARQ_HelperAsync(SAP_Object, SAP_Object,String)"/>,
    /// <see cref="ApplicationProcess_AARQ_Controller.AARQ_Helper(SAP_Object, SAP_Object,String)"/>
    /// methods executions
    ///</remarks>
    class AARQ_Process_Structure
    {
        #region Data_Members

        /// <summary>
        /// Meter DLMS_COSEM Service Access Point<see cref="SAP_Object"/>
        /// </summary>
        public SAP_Object MeterSAP = null;

        /// <summary>
        /// Client DLMS_COSEM Service Access Point<see cref="SAP_Object"/>
        /// </summary>
        public SAP_Object ClientSAP = null;

        /// <summary>
        /// The Password is used by AARQ Service to authenticate
        /// </summary>
        public String Password = null;

        /// <summary>
        /// The Password provided by DLMS/COSEM device to AARE Response Service to authenticate
        /// </summary>
        public string Password_Received = null;
        public bool IsSuccess;
        public bool PacketDrop;

        public HLS_Mechanism Authentication_Mechanism;

        /// <summary>
        /// This exception store error that occurred
        /// </summary>
        /// <remarks>
        /// The error occurred during last AARQ_Service Encoding or AARE Response Decoding process
        /// </remarks>
        public Exception InnerException = null;

        /// <summary>
        /// This event will notify AARQ_Service when Response decoding Process Complete
        /// </summary>
        internal flagStatus Application_Association = null;

        /// <summary>
        /// This event will notify COSEM Transport Layer when IO Packet drop
        /// </summary>
        internal Action<PacketType, String> ApplicationLayer_PacketDrop = null;

        /// <summary>
        /// This event will notify Transport Layer when Packet receive
        /// </summary>
        internal Action<object> Packet_Received = null;
        bool isPorcessComplete = false;

        #endregion

        /// <summary>
        /// Default Constructs
        /// </summary>
        public AARQ_Process_Structure()
        {
            MeterSAP = new SAP_Object();
            ClientSAP = new SAP_Object();
            Password = string.Empty;
            Password_Received = string.Empty;
            IsSuccess = false;
            PacketDrop = false;
            InnerException = null;
        }


        /// <summary>
        /// Initialize <see cref="AARQ_Process_Structure"/> with default values
        /// </summary>
        internal void InitStruct()
        {
            Password = string.Empty;
            Password_Received = string.Empty;
            IsSuccess = false;
            PacketDrop = false;
            MeterSAP = ClientSAP = null;
            InnerException = null;
        }

        #region Member_Functions

        /// <summary>
        /// Helps to decode packet received from DLMS/COSEM device
        /// </summary>
        /// <remarks>
        /// AARQ_HelperDecode function helps to decode packet received and verify either valid response received or not
        /// </remarks>
        /// <param name="ApplicationProcess_Controller"></param>
        /// <param name="localBuffer">IO buffer to be used for decoding</param>
        /// <param name="offset">int:OffSet index to buffer</param>
        /// <param name="count">int:Number of bytes</param>
        /// <returns>void</returns>
        internal void AARQ_HelperDecode(ApplicationProcess_Controller ApplicationProcess_Controller, byte[] localBuffer, int offset, int count)
        {
            try
            {
                // Initialize Work
                // ApplicationProcess_Controller.Dettach_Handlers();

                isPorcessComplete = false;
                String error_Message = String.Empty;

                PacketDrop = false;
                stAPPLICATION_ASSOCIATION AARQ_from_AL = null;

                #region // Event Handler Code To Receive Decoding Errors & Exception

                if (ApplicationLayer_PacketDrop == null)
                    ApplicationLayer_PacketDrop =
                            (PacketType packetType, string errorMessage) =>
                            {

                                InnerException = new DLMSException(String.Format("{0} Packet drop,error details {1}",
                                    packetType, error_Message));
                                PacketDrop = true;
                                isPorcessComplete = true;

                            };

                #endregion

                #region Event Handler To Receive Application Association

                if (Application_Association == null)
                    Application_Association = (bool Arg) =>
                    {
                        // Copy Internal AARQ Decoder Structure Here
                        AARQ_from_AL = ApplicationProcess_Controller.ApplicationProcess.APPLICATION_ASSOCIATION_from_AL;
                        // Populate Internal Variable
                        IsSuccess = Arg;

                        Password_Received = string.Empty;
                        byte[] PassWordReceive = AARQ_from_AL.Calling_Authentication_Value;
                        // Decode Received Bytes To String
                        if (PassWordReceive != null && PassWordReceive.Length >= 1)
                            Password_Received = DLMS_Common.ArrayToPrintableString(PassWordReceive);
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
                ApplicationProcess_Controller.ApplicationProcess.SAPAssociation += Application_Association;

                #endregion

                #region // Send Packet For IO Decoding

                byte[] _TBuf = new byte[count];
                Array.Copy(localBuffer, offset, _TBuf, 0, _TBuf.Length);

                #region Debugging & Logging
#if Enable_IO_Logging

                if (ApplicationProcess_Controller.ApplicationProcess.Logger != null)
                    // Log Transmitted Data
                    ApplicationProcess_Controller.ApplicationProcess.
                        Logger.LogALTraffic(_TBuf,
                                            0,
                                            _TBuf.Length, DataStatus.Read);

#endif
                #endregion

                ApplicationProcess_Controller.ApplicationProcess.ApplicationLayer.packetReceived(_TBuf);

                #endregion

                // Check If AARQ Response Received
                if (!isPorcessComplete &&
                    !ApplicationProcess_Controller.DLMSNotify)
                    throw new DLMSException("Error occurred while Process AARQ_Decode_Packet,unknown packet type");
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
        ///  encodes the Request Packet (Get_Request_Normal,Get_Request_Next_Block) of AARQ_Service
        /// </summary>
        /// <param name="ApplicationProcess_Controller">provide high level services</param>
        /// <param name="localBuffer">IO buffer to be used for encoding</param>
        /// <param name="offset">int:OffSet to the encoded buffered</param>
        /// <param name="count">int:Number of bytes encoded</param>
        internal void AARQ_HelperEncode(ApplicationProcess_Controller ApplicationProcess_Controller,
                                        ref byte[] localBuffer, ref int offset, ref int count)
        {
            try
            {
                if (MeterSAP == null ||
                    ClientSAP == null ||
                    Password == null)
                    throw new ArgumentNullException("Null Argument Exception AARQ_Process_Structure");

                ApplicationProcess_Controller.ApplicationProcess.Password = Password;
                ApplicationProcess_Controller.ApplicationProcess.CurrentClientSAP = ClientSAP;
                ApplicationProcess_Controller.ApplicationProcess.CurrentMeterSAP = MeterSAP;

                byte[] Encoded_Packet = ApplicationProcess_Controller.ApplicationProcess.Encode_AARQ(this.Authentication_Mechanism);

                #region // Copy Encoded Buffer

                if (localBuffer == null)
                {
                    localBuffer = Encoded_Packet;

                    offset = 0;
                    count = localBuffer.Length;
                }
                else
                {
                    Buffer.BlockCopy(Encoded_Packet, 0, localBuffer, offset, Encoded_Packet.Length);
                    count = Encoded_Packet.Length;
                }

                #endregion

                #region Debugging & Logging
#if Enable_IO_Logging

                if (ApplicationProcess_Controller.ApplicationProcess.Logger != null)
                    // Log Transmitted Data
                    ApplicationProcess_Controller.ApplicationProcess.
                        Logger.LogALTraffic(localBuffer,
                                            offset,
                                            count, DataStatus.Write);

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
