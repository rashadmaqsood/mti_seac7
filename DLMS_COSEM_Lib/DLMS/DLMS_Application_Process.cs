// #define Enable_Log_Message
#define Enable_Log_Error

// Code Revised & Modified
// Revision # 1.1 Tuesday 09 October 2012
// <Comments>
// Code Revision And Modification For Code Optimization
// </Comments>

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using DLMS.Comm;
using Serenity.Crypto;

namespace DLMS
{
    #region delegate

    /// <summary>
    /// This delegate will notify when GET_Service Decoding Process Completes;
    /// This delegate will invoke when COSEM Interface Instance Decoding Complete
    /// </summary>
    /// <param name="arg">COSEM Interface Class Instance whose data request in GET_Service function</param>
    public delegate void DecodingCompleted(Base_Class arg);

    /// <summary>
    /// This delegate will notify when AARQ_Service response AARE Decoding Process Complete
    /// </summary>
    /// <param name="flag">Either Last Process Successful or failed</param>
    public delegate void flagStatus(bool flag);

    /// <summary>
    /// This delegate will notify when SET_Service Decoding Process Completes
    /// </summary>
    /// <param name="result">The OutCome for Last SET Service Process <see cref="Data_Access_Result"/></param>
    public delegate void SetResult(Data_Access_Result result);

    /// <summary>
    /// This delegate Encodes/transmit the DLMS GET_NEXT request in GET_WITH_BLOCKING Service
    /// </summary>
    public delegate void Get_next_sender();
    /// <summary>
    /// This delegate Encodes/transmit the DLMS SET_NEXT request in SET_WITH_BLOCKING Service
    /// </summary>
    /// <param name="blockNum">Sender Block Number For SET_Service</param>
    public delegate void SET_Next_Sender(uint blockNum);

    #endregion

    /// <summary>
    /// The DLMS_Application_Process(AP) is Client Class that use services being provided by (AL) Application_Layer(REF) 
    /// in DLMS_COSEM protocol stack and architecture. This class contains storage structures to reflect the DLMS_Protocol state
    /// of Service being provided by AL. This class expose the standard interface through the Public events and helper function
    /// to be used by the Client of this Class.
    /// </summary>
    public partial class DLMS_Application_Process : IDisposable
    {

        #region Delegates and External Events

        private event flagStatus _SAPAssociation = null; // delegate { };
        private event Get_next_sender _GET_Recieved_With_Block = null;
        private event SET_Next_Sender _SET_Response_With_Block = null;
        private event DecodingCompleted _DecodingComplete = null;
        private event DecodingCompleted _GET_Response = null;
        private event SetResult _SET_Response = null;
        private event Action<Action_Result> _Action_Response = null;

        private bool isHandlerAttached = false;

        /// <summary>
        /// This event will notify when AARQ_Service response AARE Decoding Process Complete
        /// </summary>
        public event flagStatus SAPAssociation
        {
            add
            {
                if (_SAPAssociation != value)
                    _SAPAssociation += value;
                else ; // Skip Already Event Handler Attached
            }
            remove
            {
                if (_SAPAssociation != null)
                    _SAPAssociation -= value;
            }
        }

        /// <summary>
        /// This event notify DLMS GET_NEXT request in GET_WITH_BLOCKING Service Decoding Process Complete
        /// </summary>
        public event Get_next_sender GET_Recieved_With_Block
        {
            add
            {
                if (_GET_Recieved_With_Block != value)
                    _GET_Recieved_With_Block += value;
                else ; // Skip Already Event Handler Attached
            }
            remove
            {
                if (_GET_Recieved_With_Block != null)
                    _GET_Recieved_With_Block -= value;
            }
        }

        /// <summary>
        /// This event notify DLMS SET_NEXT response in SET_WITH_BLOCKING Service Decoding Process Complete
        /// </summary>
        public event SET_Next_Sender SET_Response_With_Block
        {
            add
            {
                if (_SET_Response_With_Block != value)
                    _SET_Response_With_Block += value;
                else ; // Skip Already Event Handler Attached
            }
            remove
            {
                if (_SET_Response_With_Block != null)
                    _SET_Response_With_Block -= value;
            }
        }

        internal event DecodingCompleted DecodingComplete
        {
            add
            {
                if (_DecodingComplete != value)
                    _DecodingComplete += value;
                else ; // Skip Already Event Handler Attached
            }
            remove
            {
                if (_DecodingComplete != null)
                    _DecodingComplete -= value;
            }
        }

        /// <summary>
        /// This event will notify when GET_Service Decoding Process Completes;
        /// </summary>
        public event DecodingCompleted GET_Response
        {
            add
            {
                if (_GET_Response != value)
                    _GET_Response += value;
                else ; // Skip Already Event Handler Attached
            }
            remove
            {
                if (_GET_Response != null)
                    _GET_Response -= value;
            }
        }

        /// <summary>
        /// This event will notify when SET_Service Decoding Process Completes
        /// </summary>
        public event SetResult SET_Response
        {
            add
            {
                if (_SET_Response != value)
                    _SET_Response += value;
                else ; // Skip Already Event Handler Attached
            }
            remove
            {
                if (_SET_Response != null)
                    _SET_Response -= value;
            }
        }

        /// <summary>
        /// This event will notify when Method_Invoke Decoding Process Completes
        /// </summary>
        public event Action<Action_Result> Action_Response
        {
            add
            {
                if (_Action_Response != value)
                    _Action_Response += value;
                else ; // Skip Already Event Handler Attached
            }
            remove
            {
                if (_Action_Response != null)
                    _Action_Response -= value;
            }
        }

        #endregion

        #region DLMS_Application_Process Objects

        /// <summary>
        /// Flag Indicate Either Client AP(Application Process) IsConnected With Server AP(Application Process).
        /// The flag flg_Association_Developed is updated after AARQ response decoded by AP.
        /// </summary>
        internal bool flg_Association_Developed;

        /// <summary>
        /// Flag Indicate Either Server Device SAP(REF) list is received
        /// </summary>
        internal bool flg_SAP_List_Recieved;

        /// <summary>
        /// Flag Indicate Either Struts and member variables are initialized in Current AP Instance by
        /// invoke function <see cref="Init_ApplicationProcess"/> 
        /// </summary>
        internal bool flg_Application_Process_Init;

        /// <summary>
        /// The temporary Array Buffer used to store Encoded data being Sent by SET_Service
        /// </summary>
        internal byte[] SetBuf;

        /// <summary>
        /// Variable store block count in SET_Service With Blocking
        /// </summary>
        internal uint internalBlockNum;

        /// <summary>
        /// Compute count how many SET block needs to Send in SET_Service
        /// </summary>
        internal int MaxSETBlock
        {
            get
            {
                int maxBlocks = -1;
                try
                {
                    int MaxpackSize = Server_PDU_Size - MaxSETHeader;
                    int SetBufferSize = SetBuf.Length;
                    maxBlocks = SetBufferSize / MaxpackSize + ((SetBufferSize % MaxpackSize == 0) ? 0 : 1);
                    return maxBlocks;
                }
                catch (Exception ex)
                {
                    maxBlocks = -1;
                }
                return maxBlocks;
            }
        }

        /// <summary>
        /// Compute count how many number of SET_Service block left to transmit
        /// </summary>
        internal uint SETBlockLeft
        {
            get
            {
                int maxBlocks = -1;
                uint blocksLeft = 00;
                try
                {
                    maxBlocks = MaxSETBlock;
                    if (maxBlocks != -1)
                        blocksLeft = (uint)(maxBlocks - ((int)internalBlockNum));
                }
                catch (Exception ex)
                {
                    blocksLeft = 0;
                }
                return blocksLeft;
            }
        }

        #region /// Instantiate Structures

        /// <summary>
        /// The Strut <see cref="stAPPLICATION_ASSOCIATION"/> holds state information and data  
        /// pass to AL(Application_Layer) class during AARQ_Service Encoding Process
        /// </summary>
        internal stAPPLICATION_ASSOCIATION Send_APPLICATION_ASSOCIATION;
        /// <summary>
        /// The Strut <see cref="stAPPLICATION_ASSOCIATION"/> holds state information and data  
        /// sent from AL(Application_Layer) class during AARQ_Service response Decoding Process
        /// </summary>
        internal stAPPLICATION_ASSOCIATION APPLICATION_ASSOCIATION_from_AL;
        /// <summary>
        /// The Strut <see cref="stGET"/> holds Protocol state information and data pass to AL(Application_Layer) 
        /// class during GET_Service Encoding Process
        /// </summary>
        internal stGET GET_to_AL;
        /// <summary>
        /// The Strut <see cref="stGET"/> holds Protocol state information and data Sent from AL(Application_Layer) 
        /// class during GET_Service Decoding Process
        /// </summary>
        internal stGET GET_from_AL;
        /// <summary>
        /// The Strut <see cref="stGET"/> holds Protocol state information and data Sent from AL(Application_Layer) class during GET_WITH_BLOCK_Service
        /// </summary>
        internal stGET Get_Next;
        /// <summary>
        /// The Strut <see cref="stSET"/> holds Protocol state information and data pass to AL(Application_Layer) 
        /// class during SET_Service Encoding Process
        /// </summary>
        internal stSET SET_to_AL;
        /// <summary>
        /// The Strut <see cref="stSET"/> holds Protocol state information and data Sent from AL(Application_Layer) 
        /// class during SET_Service Decoding Process
        /// </summary>
        internal stSET SET_from_AL;
        /// <summary>
        /// The Strut <see cref="stACTION"/> holds Protocol state information and data pass to AL(Application_Layer) 
        /// class during ACTION_Service Encoding Process
        /// </summary>
        internal stACTION ACTION_to_AL;
        /// <summary>
        /// The Strut <see cref="stACTION"/> holds Protocol state information and data Sent from AL(Application_Layer) 
        /// class during ACTION_Service Decoding Process
        /// </summary>
        internal stACTION ACTION_from_AL;
        /// <summary>
        /// The Strut <see cref="stASSOCIATION_RELEASE"/> holds Protocol state information and data pass to AL(Application_Layer) 
        /// class during ARLRQ_Service Encoding Process
        /// </summary>
        internal stASSOCIATION_RELEASE ASSOCIATION_RELEASE_to_AL;
        /// <summary>
        /// The Strut <see cref="stASSOCIATION_RELEASE"/> holds Protocol state information and data Sent from AL(Application_Layer) 
        /// class during ARLRQ_Service Decoding Process
        /// </summary>
        internal stASSOCIATION_RELEASE ASSOCIATION_RELEASE_from_AL;

        #endregion

        // General Parameters
        /// <summary>
        /// Service Access Point(REF) for Client AP
        /// </summary>
        private SAP_Object current_Client_SAP;
        /// <summary>
        /// Service Access Point(REF) for Server
        /// </summary>
        private SAP_Object current_Meter_SAP;

        public List<SAP_Object> Client_Logical_Devices;

        /// <summary>
        /// Set of All Service Access Point(REF) for Server
        /// </summary>
        public List<SAP_Object> Server_Logical_Devices;

        // private UInt32[] Total_Associations;
        private const ushort MIN_Server_PDU_Size = 50;
        private const ushort Max_Server_PDU_Size = 1500;

        /// <summary>
        /// The Size of PDU(Protocol Data Unit) Supported by Server AP;
        /// This parameter is updated during AARQ response decoding
        /// </summary>
        private UInt16 _Server_PDU_Size = MIN_Server_PDU_Size;
        /// <summary>
        /// The Conformance BLock(REF) send in AARQ request
        /// </summary>
        /// <remarks>
        /// A parameter of the COSEM_OPEN AARQ_Service, listing the xDLMS services and features
        /// </remarks>
        private byte[] Proposed_Conformance;
        /// <summary>
        /// The Conformance BLOCK(REF) received by Server AP;
        /// </summary>
        /// <remarks>
        /// A parameter of the COSEM_OPEN AARQ_Service, listing the xDLMS services and features
        /// </remarks>
        private ConformanceBlock _Negotiated_Conformance;


        /// <summary>
        /// AL (Application_Layer) Class instance <see cref="DLMS_Application_Layer"/> that reveals set of 
        /// helper function to implement Service Supported by DLMS_COSEM
        /// </summary>
        private DLMS_Application_Layer Application_Layer;
        /// <summary>
        /// COSEM IC Object Related to DLMS Read/Write GET_Service
        /// </summary>
        internal Base_Class Previous_Requested_Object;
        /// <summary>
        /// COSEM IC Object Related to DLMS method Invoke_Service
        /// </summary>
        internal Base_Class Previous_Invoke_Object;
        /// <summary>
        /// The request type <see cref="GET_req_Packet_Type"/> of GET_Service 
        /// </summary>
        internal GET_req_Packet_Type Previous_Get_Request_Type;
        /// <summary>
        /// The request type <see cref="SET_req_Type"/> of SET_Service
        /// </summary>
        internal SET_req_Type Previous_SET_Request_Type;
        /// <summary>
        /// The request type <see cref="ACTION_req_Type"/> of ACTION_Service
        /// </summary>
        internal ACTION_req_Type Previous_Action_Request_Type;
        /// <summary>
        /// The response type <see cref="Action_res_Type"/> of ACTION_Service
        /// </summary>
        internal Action_res_Type Previous_Action_Response_Type;
        /// <summary>
        /// The Invoke_Id_Priority Id Sent from AP in last IO Operation
        /// </summary>
        internal byte Previous_Invoke_Id_Priority;
        /// <summary>
        /// The attribute Id for COSEM Object Specification in last GET_Service
        /// </summary>
        internal byte Previous_Attribute_Requested;
        /// <summary>
        /// The method Id for COSEM Object Specification in last Method_Invoke_Service
        /// </summary>
        internal byte Previous_Method_Invoke;

        internal byte IsCompatibilityMode = 0;

        #endregion
        internal string Password
        {
            set
            {
                Send_APPLICATION_ASSOCIATION.Calling_Authentication_Value = new byte[value.Length];
                Send_APPLICATION_ASSOCIATION.Calling_Authentication_Value = DLMS_Common.PrintableStringToByteArray(value);
            }
        }

        /// <summary>
        /// The Conformance BLOCK(REF) received by Server AP;
        /// </summary>
        /// <remarks>
        /// A parameter of the COSEM_OPEN AARQ_Service, listing the xDLMS services and features
        /// </remarks>
        public ConformanceBlock Negotiated_Conformance
        {
            get { return _Negotiated_Conformance; }
            internal set { _Negotiated_Conformance = value; }
        }

        public ICrypto Crypto
        {
            set
            {
                Application_Layer.Crypto = value;
            }
        }

        public Security_Data Security_Data
        {
            set
            {
                Application_Layer.Security_Data = value;
            }
            get
            {
                return Application_Layer.Security_Data;
            }
        }

        //--------------------------------------------------------------------------------------
        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public DLMS_Application_Process()
        {
            #region Instantiate Member variables

            // Flag
            flg_Association_Developed = false;
            // Conformance
            Proposed_Conformance = new byte[]  { 0x00, 0x7E, 0x1F };//{ 0xFF, 0xFF, 0xFF }; 
            _Negotiated_Conformance = new ConformanceBlock(new byte[] { 0x00, 0x00, 0x00 });
            // Construct Structures
            Send_APPLICATION_ASSOCIATION = new stAPPLICATION_ASSOCIATION();         // APPLICATION_ASSOCIATION for AL
            APPLICATION_ASSOCIATION_from_AL = new stAPPLICATION_ASSOCIATION();      // APPLICATION_ASSOCIATION from AL
            GET_to_AL = new stGET();                                                // GET for AL
            GET_from_AL = new stGET();                                              // GET from AL
            SET_to_AL = new stSET();
            SET_from_AL = new stSET();
            ACTION_to_AL = new stACTION();
            ACTION_from_AL = new stACTION();
            ASSOCIATION_RELEASE_to_AL = new stASSOCIATION_RELEASE();
            ASSOCIATION_RELEASE_from_AL = new stASSOCIATION_RELEASE();

            #endregion
            #region Instantiate DLMS_Application_Layer

            Application_Layer = new DLMS_Application_Layer();
            logger = new DLMSLogger();
            ApplicationLayer.DLMSLogger = logger;

            #endregion
        }

        #endregion

        /// <summary>
        /// The Size of PDU(Protocol Data Unit) Supported by Server AP;
        /// This parameter is updated during AARQ response decoding
        /// </summary>
        public UInt16 Server_PDU_Size
        {
            get { return _Server_PDU_Size; }
            set
            {
                if (_Server_PDU_Size >= MIN_Server_PDU_Size &&
                    _Server_PDU_Size <= Max_Server_PDU_Size)
                    _Server_PDU_Size = value;
                else
                    throw new Exception("Error occurred while initializing Max_Server_PDU_Size");
            }
        }

        /// <summary>
        /// This helper function to initialize the Current AP(Application_Process),AL(Application_Layer)
        /// class instances
        /// </summary>
        internal void Init_ApplicationProcess()
        {
            try
            {
                SetBuf = null;
                InitializeVariables();
                Application_Layer.InitializeVariables();
                AttachEventHandlers();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Initializing DLMS_Application_Process", ex);
            }
        }

        /// <summary>
        /// This helper function to DE-initialize the Current AP(Application_Process),AL(Application_Layer)
        /// class instances
        /// </summary>
        internal void DeInit_ApplicationPorcess()
        {
            try
            {
                GetSAPTableEntryDelegate = null;
                Is_Association_Developed = false;
                DetachEventHandlers();
                SetBuf = null;
                Previous_Requested_Object = null;
                Previous_Invoke_Object = null;
                Client_Logical_Devices = new List<SAP_Object>();
                Server_Logical_Devices = new List<SAP_Object>();
                InitializeVariables();
                Application_Layer.InitializeVariables();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while DeIntializing DLMS_Application_Process", ex);
            }
        }

        //--------------------------------------------------------------------------------------
        #region Encode AARQ,ARLRQ

        /// <summary>
        /// The helper function Encode_Association_Release, encodes the ARLRQ_Service request packet
        /// </summary>
        /// <returns>Buffer Array</returns>
        public byte[] Encode_Association_Release()
        {
            return Application_Layer.ASSOCIATION_RELEASE(ASSOCIATION_RELEASE_to_AL);
        }

        /// <summary>
        /// The helper function Encode_AARQ, Encodes the AARQ_Service request packet
        /// </summary>
        public byte[] Encode_AARQ(HLS_Mechanism Sec_Mechanism_Name = HLS_Mechanism.LowSec)
        {
            // Re-Initialize Application Association Structure
            Init_stApplication_Association();

            Send_APPLICATION_ASSOCIATION.Flg_CompatibilityMode = IsCompatibilityMode;
            Send_APPLICATION_ASSOCIATION.Authentication_Mechanism = Sec_Mechanism_Name;
            // Default value
            Send_APPLICATION_ASSOCIATION.Application_Context_ID = Application_ContextName.LN_Referencing_No_Ciphering;

            // Update Called Application Title
            if (User_Software_ID > 0)
            {
                Send_APPLICATION_ASSOCIATION.Called_AP_Title.AP_Title = new byte[04];
                var Encoded_SUID = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A06_double_long_unsigned, User_Software_ID);
                Buffer.BlockCopy(Encoded_SUID, 1, Send_APPLICATION_ASSOCIATION.Called_AP_Title.AP_Title, 0, 4);

                Send_APPLICATION_ASSOCIATION.Called_AP_Title.UserId = User_Software_ID;
            }
            else
                Send_APPLICATION_ASSOCIATION.Called_AP_Title.AP_Title = null;

            if (Send_APPLICATION_ASSOCIATION.Authentication_Mechanism > HLS_Mechanism.LowSec)
            {
                // Either Packet Encryption Will be Applied
                if (Security_Data != null)
                    Send_APPLICATION_ASSOCIATION.Application_Context_ID = Application_ContextName.LN_Referencing_With_Ciphering;
            }

            // Pass structure to AL Encode AARQ Packet
            return Application_Layer.A_ASSOCIATE_req(Send_APPLICATION_ASSOCIATION);
        }

        #endregion

        #region DLMS Service Handlers

        /// <summary>
        /// The helper function is an event handler that consume AL (Application_Layer) event 
        /// <see cref="DLMS.DLMS_Application_Layer.AARE_Packet_Decoded"/>
        /// </summary>
        /// <param name="Recieved_Structure"></param>
        private void AARE_is_Recieved(stAPPLICATION_ASSOCIATION Recieved_Structure)
        {
            try
            {
                String ErrorMessage = null;
                APPLICATION_ASSOCIATION_from_AL = (stAPPLICATION_ASSOCIATION)Recieved_Structure.Clone();

                // If Already Associated
                // if (Is_Association_Developed)
                // {
                //     throw new DLMSException("AARE Received when already associated");
                // }
                // else

                {
                    // no error is received
                    if (APPLICATION_ASSOCIATION_from_AL.Result == (byte)AssociationResult.Accepted)
                    {
                        // Check If XDLMS_Initiate_Error Error
                        if (APPLICATION_ASSOCIATION_from_AL.XDLMS_Initiate_Error != null)
                            throw APPLICATION_ASSOCIATION_from_AL.XDLMS_Initiate_Error;

                        // Store Max APDU Size to class level variable
                        Server_PDU_Size = APPLICATION_ASSOCIATION_from_AL.Negotiated_xDLMS_Context.Server_Max_Receive_PDU_Size;

                        // Store Negotiated Conformance
                        byte[] _Negotiated_ConformanceVAL = null;

                        #region  // Store Negotiated Conformance

                        DLMS_Common.Byte_Array_Copier(APPLICATION_ASSOCIATION_from_AL.Negotiated_xDLMS_Context.Negotiated_DLMS_Conformance,
                                                      ref _Negotiated_ConformanceVAL, 0);

                        if (Negotiated_Conformance == null)
                            Negotiated_Conformance = new ConformanceBlock();
                        Negotiated_Conformance.SetConformanceBlock(_Negotiated_ConformanceVAL);

                        #endregion

                        if (Server_PDU_Size == 0 || Server_PDU_Size >= MIN_Server_PDU_Size)
                        {
                            Association_is_Developed(true);
                            return;
                        }
                        else
                            // Server PDU Size is between 0 and MIN_Server_PDU_Size:50
                            ErrorMessage = String.Format("APDU size is only {0:X2}", Server_PDU_Size);

                        // HLS Security Mechanism
                        if (APPLICATION_ASSOCIATION_from_AL.Authentication_Mechanism >= HLS_Mechanism.HLS_Manufac)
                        {
                            if (APPLICATION_ASSOCIATION_from_AL.Failure_Type != Result_SourceDiagnostic.AuthenticationRequired)
                            {
                                ErrorMessage = String.Format("HLS Authentication Required Error Local_Remote_Error:{0:X2},Failure Type:{1} (Error Code:{2})"
                                    , APPLICATION_ASSOCIATION_from_AL.Local_Or_Remote
                                    , APPLICATION_ASSOCIATION_from_AL.Failure_Type,
                                    (int)DLMSErrors.ServiceFailure_AARQ);

                                Association_is_Developed(false);
                            }
                        }

                    } // End if no error is received
                    else
                    {
                        Association_is_Developed(false);

                        // Check If XDLMS_Initiate_Error Error
                        if (APPLICATION_ASSOCIATION_from_AL.XDLMS_Initiate_Error != null)
                            throw APPLICATION_ASSOCIATION_from_AL.XDLMS_Initiate_Error;

                        // Error was received
                        ErrorMessage = String.Format("Local_Remote_Error:{0:X2},Failure Type:{1} (Error Code:{2})"
                            , APPLICATION_ASSOCIATION_from_AL.Local_Or_Remote
                            , APPLICATION_ASSOCIATION_from_AL.Failure_Type
                            , (int)DLMSErrors.ServiceFailure_AARQ);
                    }

                    if (!Is_Association_Developed || ErrorMessage != null)
                    {
                        var InnerException = new DLMSException(ErrorMessage);
                        // DLMSErrCodesException
                        throw new DLMSErrCodesException((AssociationResult)APPLICATION_ASSOCIATION_from_AL.Result,
                                                        (Result_SourceDiagnostic)APPLICATION_ASSOCIATION_from_AL.Failure_Type,
                                                        InnerException);
                    }
                }
            }
            catch (DLMSException ex)
            {
                #region Debugging_&_Logging

#if Enable_Log_Error

                int count = -1;
                byte[] TArray = Application_Layer.Recieved_Packet;
                if (TArray != null)
                    count = TArray.Length;
                Logger.LogAPError(ex, TArray, 0, count, Get_Index.Dummy, PacketType.ARE);

#endif
                #endregion
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// The helper function Association_is_Developed
        /// </summary>
        /// <param name="IsAssociated"></param>
        private void Association_is_Developed(bool IsAssociated)
        {
            flg_Association_Developed = IsAssociated;
            if (Is_Association_Developed)
            {
                // UpdateCurrentAssociation();
                // Update Current Association Index In SAP Table
                SetCurrentAssociationIndex(Current_Association_Index);
                // Logger.LogAPMessage("Application Association Developed Successfully", PacketType.ARE);
            }
            if (_SAPAssociation != null)
                _SAPAssociation.Invoke(Is_Association_Developed);
            // MsgDisplay.AppendMsg("Association Developed Successfully");
        }

        /// <summary>
        /// The helper function is an event handler that consume AL (Application_Layer) event 
        /// <see cref="DLMS.DLMS_Application_Layer.Association_Release_Packet_Decoded"/>
        /// </summary>
        /// <param name="Recieved_Structure"></param>
        private void Association_Release_is_Recieved(stASSOCIATION_RELEASE Recieved_Structure)
        {
            try
            {
                ASSOCIATION_RELEASE_from_AL = (stASSOCIATION_RELEASE)Recieved_Structure.Clone();
                // association is not developed already
                //if (!Is_Association_Developed)
                //{
                //    throw new DLMSException("ARLRE is received before Association is developed");
                //}
                flg_Association_Developed = false;
                //Update Current Association Index In SAP Table (Not Associated 0xFF)
                SetCurrentAssociationIndex(0xFF);
                #region Debugging And Logging
#if Enable_Log_Message

                Logger.LogAPMessage("Application Association Release Successfully", PacketType.RLRQ);

#endif
                #endregion
                if (_SAPAssociation != null)
                    _SAPAssociation.Invoke(Is_Association_Developed);
            }
            catch (DLMSException ex)
            {
                #region Debugging & Logging
#if Enable_Log_Error

                int count = -1;
                byte[] TArray = Application_Layer.Recieved_Packet;
                if (TArray != null)
                    count = TArray.Length;
                Logger.LogAPError(ex, TArray, 0, count, Get_Index.Dummy, PacketType.RLRQ);

#endif
                #endregion
                throw ex;
            }
            finally
            {
            }
        }

        private void SetCurrentAssociationIndex(byte AssociationIndex)
        {
            //if (SAP_Table != null)
            //for (int index = 0; index < SAP_Table.Length; index++)
            //if (SAP_Table[index] != null) SAP_Table[index].Rights.CurrentAssociationIndex = AssociationIndex;
        }
        //--------------------------------------------------------------------------------------

        /// <summary>
        /// The helper function is an event handler that Consume AL (Application_Layer) events 
        /// <see cref="DLMS.DLMS_Application_Layer.GET_Packet_Decoded"/>
        /// </summary>
        /// <param name="Recieved_Structure"></param>
        private void GET_Decoded_Handler(stGET Recieved_Structure)
        {
            byte[] IODump = null;
            int count = -1;
            try
            {
                GET_from_AL = (stGET)Recieved_Structure.Clone();
                String StrError = null;
                if (GET_from_AL.Invoke_Id_Priority == Previous_Invoke_Id_Priority /* &&
                GET_from_AL.Response_Type == (byte)Previous_Request_Type*/)
                {
                    if (GET_from_AL.Get_Data_Result != (byte)Data_Access_Result.Success)
                    {
                        StrError = String.Format("Get Data Result:{0:X2},Data Access Result:{1} (Error Code:{2})", GET_from_AL.Get_Data_Result,
                            (Data_Access_Result)GET_from_AL.Data_Access_Result, (int)DLMSErrors.ServiceFailure_GET);

                        // throw new DLMSErrCodesException(GET_from_AL.Data_Access_Result, null);
                    }
                    else
                    {
                        if (GET_from_AL.Response_Type == (byte)GET_Method.Normal)
                        {
                            #region Decoding Goes Here

                            try
                            {
                                int array_traverse = 0;
                                // Decode and save data
                                Previous_Requested_Object.DecodingAttribute = Previous_Attribute_Requested;
                                // Optional Data Encode Decode Procedure
                                if (Previous_Requested_Object.EnableCOSEM_EncodeDecode_OPT)
                                    Previous_Requested_Object.Decode_Data(ref GET_from_AL.Data, ref array_traverse, GET_from_AL.Data.Length);
                                else
                                {
                                    ArraySegment<byte> TArraySeg = new ArraySegment<byte>(GET_from_AL.Data, array_traverse, GET_from_AL.Data.Length);

                                    // Copy Data
                                    Previous_Requested_Object.EncodedRaw = new List<byte>(TArraySeg.Count);
                                    Previous_Requested_Object.EncodedRaw.AddRange(TArraySeg.AsEnumerable<byte>());
                                    array_traverse += TArraySeg.Count;
                                }
                            }
                            finally
                            {
                                IODump = Recieved_Structure.Data;
                                if (IODump != null)
                                    count = IODump.Length;
                            }

                            #endregion
                            #region Debugging & Logging
#if Enable_Log_Message

                            // Decoding Completed Here....

                            Logger.LogAPMessage(String.Format("{0} Received and decoded",
                                Previous_Requested_Object.OBISIndex), PacketType.GET);
                            Logger.LogAP_OBISCodeDecoded(Previous_Requested_Object);
                            // DecodingComplete(Previous_Requested_Object);

#endif
                            #endregion
                            if (_GET_Response != null)
                                _GET_Response.Invoke(Previous_Requested_Object);

                        } // Check if last block of blocking
                        else if (GET_from_AL.Response_Type == (byte)GET_Method.With_Block &&
                            GET_from_AL.DataBlock_G.Last_Block == 0x01)
                        {

                            #region // Decoding Comes Here,Copy Raw data first
                            try
                            {
                                int array_traverse = 0;
                                // DLMS_Common.Byte_Array_Copier(GET_from_AL.DataBlock_G.Raw_Data, ref GET_from_AL.Data, 0);
                                GET_from_AL.Data = GET_from_AL.DataBlock_G.Raw_Data.ToArray();
                                // Decode and save data
                                Previous_Requested_Object.DecodingAttribute = Previous_Attribute_Requested;
                                if (Previous_Requested_Object.EnableCOSEM_EncodeDecode_OPT)
                                    Previous_Requested_Object.Decode_Data(ref GET_from_AL.Data, ref array_traverse, GET_from_AL.Data.Length);
                                else
                                {
                                    ArraySegment<byte> TArraySeg = new ArraySegment<byte>(GET_from_AL.Data, array_traverse, GET_from_AL.Data.Length);
                                    // Copy_Data
                                    Previous_Requested_Object.EncodedRaw = new List<byte>(TArraySeg.Count);
                                    Previous_Requested_Object.EncodedRaw.AddRange(TArraySeg.AsEnumerable<byte>());
                                    array_traverse += TArraySeg.Count;
                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                            finally
                            {
                                if (Recieved_Structure != null &&
                                    Recieved_Structure.DataBlock_G.Raw_Data != null &&
                                    Recieved_Structure.DataBlock_G.Raw_Data.Count > 0)
                                {
                                    // DLMS_Common.Byte_Array_Copier(Recieved_Structure.DataBlock_G.Raw_Data, ref Recieved_Structure.Data, 0);
                                    IODump = Recieved_Structure.Data = Recieved_Structure.DataBlock_G.Raw_Data.ToArray();
                                    if (IODump != null)
                                        count = IODump.Length;
                                }
                            }

                            #endregion
                            #region Debugging & Logging
#if Enable_Log_Message
                            // Decoding Completed Here....
                            // Logger & Debugger
                            Logger.LogAPMessage("Received and decoded", PacketType.GET);
                            Logger.LogAP_OBISCodeDecoded(Previous_Requested_Object);
#endif
                            #endregion
                            // DecodingComplete(Previous_Requested_Object);
                            if (_GET_Response != null)
                                _GET_Response.Invoke(Previous_Requested_Object);
                        }
                        else //GET Next With Block
                        {
                            // copy received structure to global for future use
                            Get_Next = (stGET)Recieved_Structure.Clone();
                            Get_Next.Request_Type = Recieved_Structure.Response_Type;
                            // Fire Event
                            if (_GET_Recieved_With_Block != null)
                                _GET_Recieved_With_Block();
                        }
                    }
                }
                else
                {
                    // Error Message For Logger/Debugger
                    StrError = String.Format("Wrong Invoke Id/Priority and/or Response Type Received:{0:X2},Response type:{1:X2} (Error Code:{2})",
                        GET_from_AL.Invoke_Id_Priority
                        , GET_from_AL.Response_Type,
                        (int)DLMSErrors.Invalid_InvokeIDPriority);
                }
                //AL Errors For Debugger/Logger
                if (StrError != null)
                {
                    var InnerException = new DLMSException(StrError);
                    // Throw DLMSErrCodesException
                    if (GET_from_AL.Get_Data_Result != (byte)Data_Access_Result.Success)
                        throw new DLMSErrCodesException(GET_from_AL.Data_Access_Result, InnerException);
                    else
                        throw InnerException;
                }
            }
            catch (DLMSException ex)
            {
                #region Debugging & Logging
#if Enable_Log_Error

                Logger.LogAPError(ex, IODump, 0, count, Previous_Requested_Object.OBISIndex, PacketType.GET);

#endif
                #endregion
                throw ex;
            }
            catch (DLMSDecodingException ex)
            {
                #region Debugging & Logging
#if Enable_Log_Error

                Logger.LogAPDecodingError(ex, IODump, 0, count, Previous_Requested_Object.INDEX);

#endif
                #endregion
                throw ex;
            }
            finally
            {

            }
        }
        /// <summary>
        /// The helper function is an event handler that Consume AL (Application_Layer) events 
        /// <see cref="DLMS.DLMS_Application_Layer.SET_Packet_Decoded"/>
        /// </summary>
        /// <param name="Recieved_Structure"></param>
        private void SET_Decoded_Handler(stSET Recieved_Structure)
        {
            try
            {
                SET_from_AL = (stSET)Recieved_Structure.Clone();
                String StrError = null;
                if (SET_from_AL.Invoke_Id_Priority == Previous_Invoke_Id_Priority
                    /* && GET_from_AL.Response_Type == (byte)Previous_Request_Type */)
                {
                    if (SET_from_AL.Response_Type == (byte)SET_Response_Type.NORMAL ||
                        SET_from_AL.Response_Type == (byte)SET_Response_Type.Last_Data_Block)  // SET Response Normal/Last Data_Block
                    {
                        #region Debugging & Logging
#if Enable_Log_Message
                        Logger.LogAPMessage(String.Format("_{0} response received,Result:{1}", Previous_Requested_Object.INDEX,
                            (Data_Access_Result)SET_from_AL.Result), PacketType.SET);
#endif
                        #endregion
                        if (_SET_Response != null)
                            _SET_Response((Data_Access_Result)SET_from_AL.Result);
                        SetBuf = null;
                        internalBlockNum = 1;
                    }
                    // SET_Response_With_Block
                    else if (SET_from_AL.Response_Type == (byte)SET_Response_Type.Non_Last_Data_BLOCK)
                    {
                        if (_SET_Response_With_Block != null)
                            _SET_Response_With_Block.Invoke(SET_from_AL.Block_number);
                    }
                    else
                    {
                        StrError = String.Format("Invalid Response Type Received {0:X2} (Error Code:{1})",
                                   SET_from_AL.Response_Type, (int)DLMSErrors.ServiceFailure_SET);
                    }
                }
                else
                {
                    // Error Message For Logger / Debugger
                    StrError = String.Format("Wrong Invoke Id/Priority and/or Response Type Received:{0:X2},Response type:{1:X2} (Error Code:{2})",
                        GET_from_AL.Invoke_Id_Priority
                        , GET_from_AL.Response_Type
                        , (int)DLMSErrors.Invalid_MethodInvokeId);

                }
                // AL Errors For Debugger / Logger
                if (StrError != null)
                {
                    var InnerException = new DLMSException(StrError);
                    // Throw DLMSErrCodesException
                    throw new DLMSErrCodesException(SET_from_AL.Result, InnerException);
                }
            }
            catch (DLMSException ex)
            {
                #region Debugging & Logging
#if Enable_Log_Error

                int count = -1;
                byte[] IOBuffer = null;
                if (SetBuf != null && SetBuf.Length > 0)
                    IOBuffer = SetBuf;
                else
                    IOBuffer = Application_Layer.Recieved_Packet;
                if (IOBuffer != null)
                    count = IOBuffer.Length;

                if (Previous_Requested_Object == null)
                {

                    Logger.LogAPError(ex, IOBuffer, 0, count, Get_Index.Dummy, PacketType.GET);
                }
                else
                {
                    Logger.LogAPError(ex, IOBuffer, 0, count, Previous_Requested_Object.INDEX, PacketType.GET);
                }

#endif
                #endregion
                throw ex;
            }
            finally
            {

            }
        }

        /// <summary>
        /// The helper function is an event handler that consume AL (Application_Layer) events 
        /// <see cref="DLMS.DLMS_Application_Layer.Action_Packet_Decoded"/>
        /// </summary>
        /// <param name="Received_Structure"></param>
        private void Action_Decoded_Handler(stACTION Received_Structure)
        {
            Action_Result Action_Result = DLMS.Action_Result.Rejected;
            Data_Access_Result Result = Data_Access_Result.Rejected;

            try
            {
                ACTION_from_AL = (stACTION)Received_Structure.Clone();
                String StrError = null;
                if (ACTION_from_AL.Invoke_Id_Priority == Previous_Invoke_Id_Priority)
                {
                    Previous_Action_Response_Type = (Action_res_Type)ACTION_from_AL.Response_Type;
                    if (ACTION_from_AL.Response_Type == (byte)Action_res_Type.Normal)
                    // SET Response Normal/Last Data_Block
                    {
                        //Store Action Result
                        Action_Result = (Action_Result)ACTION_from_AL.Action_Response.Result;
                        bool ResponseParameterFlag = (bool)(ACTION_from_AL.Action_Response.Flg_Response_Parameters == 0) ? false : true;

                        #region Debugging & Logging
#if Enable_Log_Message

                        Logger.LogAPMessage(String.Format("Action Invoke _{0} response received,Result:{1}",
                        Previous_Invoke_Object.INDEX, (Action_Result)Action_Result), PacketType.Action);

#endif
                        #endregion

                        Result = Data_Access_Result.Success;
                        Previous_Invoke_Object.Method_ParametersFlag = ResponseParameterFlag;
                        byte[] responseData = null;
                        // Try Response Flag Received
                        if (ResponseParameterFlag)
                        {
                            Result = (Data_Access_Result)ACTION_from_AL.Action_Response.Response_Parameters.Data_Access_Result;
                            Previous_Invoke_Object.Method_Data_Result = Result;
                            responseData = ACTION_from_AL.Action_Response.Response_Parameters.Data;
                            if (Result == Data_Access_Result.Success)
                                Previous_Invoke_Object.Decode_Parameters(ref responseData);
                        }
                        if (_Action_Response != null)
                            _Action_Response.Invoke(Action_Result);
                        SetBuf = null;
                        internalBlockNum = 1;
                    }
                    // SET_Response_With_Block
                    else
                    {
                        StrError = String.Format("Invalid Response Type Received {0:X2} (Error Code:{1})",
                            ACTION_from_AL.Response_Type, (int)DLMSErrors.ServiceFailure_Action);
                    }
                }
                else
                {
                    // Error Message For Logger/Debugger
                    StrError = String.Format("Wrong Invoke Id/Priority and/or Response Type Received:{0:X2},Response type:{1:X2} (Error Code:{2})",
                        ACTION_from_AL.Invoke_Id_Priority, ACTION_from_AL.Response_Type, (int)DLMSErrors.Invalid_MethodInvokeId);
                }

                //AL Errors For Debugger/Logger
                if (!string.IsNullOrEmpty(StrError))
                {
                    var InnerException = new DLMSException(StrError);
                    // Throw DLMSErrCodesException
                    if (Result != Data_Access_Result.Success ||
                        Action_Result != DLMS.Action_Result.Success)
                        throw new DLMSErrCodesException((byte)Result, (byte)Action_Result, InnerException);
                    else
                        throw InnerException;
                }
            }
            catch (DLMSException ex)
            {
                #region Debugging & Logging

#if Enable_Log_Error

                byte[] IOBuffer = null;
                if (SetBuf != null && SetBuf.Length > 0)
                    IOBuffer = SetBuf;
                else
                    IOBuffer = Application_Layer.Recieved_Packet;
                int count = -1;
                if (IOBuffer != null)
                    count = IOBuffer.Length;
                if (Previous_Invoke_Object == null)
                    Logger.LogAPError(ex, IOBuffer, 0, count, Get_Index.Dummy, PacketType.Action);
                else
                    Logger.LogAPError(ex, IOBuffer, 0, count, Previous_Invoke_Object.INDEX, PacketType.Action);

#endif

                #endregion
                throw ex;
            }
            finally
            {

            }
        }

        #endregion
        //--------------------------------------------------------------------------------------
        #region AL encoding of packets

        /// <summary>
        /// The helper function encodes the GET_WITH_Block request APDU
        /// </summary>
        /// <returns></returns>
        public byte[] Encode_Get_Next_Block()
        {
            // Encode Packet
            return Application_Layer.GET_req(Get_Next);
        }

        #region Get_Encoding

        /// <summary>
        /// Returns encoded packet of Get for the requested object
        /// </summary>
        /// <param name="Reqd_Object"></param>
        /// <param name="Attribute"></param>
        /// <returns></returns>
        public byte[] Encode_GET_Object(Base_Class Reqd_Object, byte Attribute)
        {
            try
            {
                if (!Is_Association_Developed)
                {
                    throw new DLMSException(String.Format("Create Application Association First (Error Code:{0})",
                                            (int)DLMSErrors.Invalid_LoginStatus));
                }
                else
                {
                    Init_stGet_Request();           //Init GET Request
                    byte[] Obis_Code = (byte[])Reqd_Object.OBIS_CODE.Clone();
                    UInt16 Class_Id = Reqd_Object.Class_ID;
                    Reqd_Object.DecodingAttribute = Attribute;
                    byte Attrib_No = Attribute;
                    // Store for verification in GET response
                    Previous_Requested_Object = Reqd_Object;
                    Previous_Attribute_Requested = Attrib_No;
                    // Make structure Ready
                    Build_Get_Structure(Class_Id, Obis_Code, Attrib_No);
                    // Store for verification
                    Previous_Get_Request_Type = (GET_req_Packet_Type)GET_to_AL.Request_Type;
                    Previous_Invoke_Id_Priority = GET_to_AL.Invoke_Id_Priority;
                    //Encode Access Selection Here
                    //Hard Code Access Selector With Attribute Code Here
                    if (Reqd_Object.AccessSelector != null && (Attrib_No == 0x2 ||
                        Reqd_Object.IsAccessSelecterApplied(Attrib_No) != SelectiveAccessType.Not_Applied))
                    {

                        GET_to_AL.COSEM_Attribute_Descriptor.Access_Selection_Parameters = (byte)Reqd_Object.AccessSelector.GetSelectorType();
                        GET_to_AL.COSEM_Attribute_Descriptor.Access_Parameters = Reqd_Object.AccessSelector.Encode();
                    }
                    // Encode Packet
                    return Application_Layer.GET_req(GET_to_AL);
                }
            }
            catch (DLMSException ex)
            {
                #region Debugging & Logging
#if Enable_Log_Error

                Logger.LogAPError(ex, null, 0, -1, Previous_Requested_Object.OBISIndex, PacketType.GET);

#endif
                #endregion
                throw ex;
            }
            finally
            {

            }
        }

        /// <summary>
        /// Returns encoded packet of Get for the requested object
        /// </summary>
        /// <param name="Reqd_Object"></param>
        /// <returns></returns>
        public byte[] Encode_GET_Object(Base_Class Reqd_Object)
        {
            return Encode_GET_Object(Reqd_Object, Reqd_Object.DecodingAttribute);
        }

        /// <summary>
        /// Prepares GET structure to be passed to AL according to parameters
        /// </summary>
        /// <param name="Class_ID"></param>
        /// <param name="OBIS_Code"></param>
        /// <param name="Attribute_No"></param>
        private void Build_Get_Structure(UInt16 Class_ID, byte[] OBIS_Code, byte Attribute_No)
        {
            // Assign obis code
            GET_to_AL.COSEM_Attribute_Descriptor.COSEM_Object_Instance_Id = OBIS_Code;

            // Assign Class ID
            GET_to_AL.COSEM_Attribute_Descriptor.COSEM_Class_Id = Class_ID;

            // Assign Attribute ID
            GET_to_AL.COSEM_Attribute_Descriptor.COSEM_Object_Attribute_Id = Attribute_No;
        }

        #endregion

        #region SET_Encoding

        /// <summary>
        /// This helper function encodes the SET_Service request Packet APDU
        /// </summary>
        /// <param name="Reqd_Object"><see cref="Base_Class"/>COSEM IC Object Specification</param>
        /// <param name="Attribute">COSEM IC Object Attribute Specification</param>
        /// <returns></returns>
        public byte[] Encode_SET_Object(Base_Class Reqd_Object, byte Attribute)
        {
            byte[] IOBuffer = null;
            try
            {
                if (!Is_Association_Developed)
                {
                    throw new DLMSException(String.Format("Create Application Association First (Error Code:{0})",
                                           (int)DLMSErrors.Invalid_LoginStatus));
                }
                else
                {
                    byte[] Data_To_Set = null;
                    try
                    {
                        Data_To_Set = Reqd_Object.Encode_Data();
                        return Encode_SET_Object(Reqd_Object, Attribute, Data_To_Set);
                    }
                    finally
                    {
                        IOBuffer = Data_To_Set;
                    }
                }
            }
            catch (DLMSException ex)
            {
                #region Debugging & Logging

#if Enable_Log_Error

                Logger.LogAPError(ex, IOBuffer, 0, (IOBuffer != null) ? IOBuffer.Length : -1, Reqd_Object.INDEX, PacketType.SET);

#endif

                #endregion
                throw ex;
            }
            catch (DLMSEncodingException ex)
            {
                #region Debugging & Logging

#if Enable_Log_Error

                int count = -1;
                byte[] TArray = Application_Layer.Recieved_Packet;
                if (TArray != null)
                    count = TArray.Length;
                Logger.LogAPEncodingError(ex, IOBuffer, 0, (IOBuffer != null) ? IOBuffer.Length : -1, Reqd_Object.INDEX);

#endif

                #endregion
                throw ex;
            }
            finally
            {

            }
        }

        /// <summary>
        /// This helper function encodes the SET_Service request Packet APDU
        /// </summary>
        /// <param name="Reqd_Object"><see cref="Base_Class"/>COSEM IC Object Specification</param>
        /// <param name="Attribute">COSEM IC Object Attribute Specification</param>
        /// <param name="Attrib_Value">Encoded Reqd_Object Parameters Data</param>
        /// <returns></returns>
        public byte[] Encode_SET_Object(Base_Class Reqd_Object, byte Attribute, byte[] Attrib_Value)
        {
            try
            {
                if (!Is_Association_Developed)
                {
                    throw new DLMSException(String.Format("Create Application Association First (Error Code:{0})",
                                           (int)DLMSErrors.Invalid_LoginStatus));
                }
                else
                {
                    Init_stSet_Request(); ///Init_SET Command
                    byte Attrib_No = Attribute;
                    UInt16 Class_Id = Reqd_Object.Class_ID;
                    // Store for verification in SET response
                    Previous_Requested_Object = Reqd_Object;
                    Previous_Attribute_Requested = Attrib_No;
                    byte[] Obis_Code = (byte[])Reqd_Object.OBIS_CODE.Clone();

                    SetBuf = new byte[Attrib_Value.Length];
                    internalBlockNum = 1;
                    Attrib_Value.CopyTo(SetBuf, 0);
                    // Make structure Ready
                    Build_Set_Structure(Class_Id, Obis_Code, Attrib_No);
                    Encode_SET_APDU(SET_to_AL, 0);
                    // Store for verification
                    Previous_SET_Request_Type = (SET_req_Type)SET_to_AL.Request_Type;
                    Previous_Invoke_Id_Priority = SET_to_AL.Invoke_Id_Priority;
                    Previous_Requested_Object = Reqd_Object;
                    // Encode Packet
                    return Application_Layer.SET_req(SET_to_AL);
                }
            }
            catch (DLMSException ex)
            {
                throw ex;
            }
            catch (DLMSEncodingException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                DLMSEncodingException _ex = new DLMSEncodingException("Error Occurred While Encoding SET_Next Packet ",
                                                                      "Application_Process_Encode_SET_Next");
                throw _ex;
            }
        }

        /// <summary>
        /// This helper function encodes the SET_WITH_BLOCK_Service request Packet APDU
        /// </summary>
        /// <param name="blockNum">current Set request block number</param>
        /// <returns></returns>
        public byte[] Encode_SET_Next(uint blockNum)
        {
            try
            {
                if (!Is_Association_Developed)
                {
                    throw new DLMSException("Create Application Association First");
                }
                else
                {
                    Encode_SET_APDU(SET_to_AL, blockNum);
                    // Encode Packet
                    return Application_Layer.SET_req(SET_to_AL);
                }
            }
            catch (DLMSException ex)
            {
                #region Debugging & Logging
#if Enable_Log_Error

                Logger.LogAPError(ex, SetBuf, 0, (SetBuf != null) ? SetBuf.Length : -1, Previous_Requested_Object.INDEX, PacketType.SET);

#endif
                #endregion
                throw ex;
            }
            catch (DLMSEncodingException ex)
            {
                #region Debugging & Logging
#if Enable_Log_Error

                Logger.LogAPEncodingError(ex, SetBuf, 0, (SetBuf != null) ? SetBuf.Length : -1, Previous_Requested_Object.INDEX);

#endif
                #endregion
                throw ex;
            }
            catch (Exception ex)
            {
                DLMSEncodingException _ex = new DLMSEncodingException("Error Occurred While Encoding SET_Next Packet ",
                                                                      "Application_Process_Encode_SET_Next");
                #region Debugging & Logging
#if Enable_Log_Error

                Logger.LogAPEncodingError(_ex, SetBuf, 0, (SetBuf != null) ? SetBuf.Length : -1, Previous_Requested_Object.INDEX);

#endif
                #endregion
                throw _ex;
            }
        }

        /// <summary>
        /// This helper function initialize the <see cref="stSET"/> strut for particular block number request
        /// </summary>
        /// <param name="SET_to_AL"></param>
        /// <param name="blockNum"></param>
        internal void Encode_SET_APDU(stSET SET_to_AL, uint blockNum)
        {
            try
            {
                if (SetBuf.Length + MaxSETHeader > Server_PDU_Size && MIN_Server_PDU_Size != 0)    /// SET WITH BLOCK 
                {
                    //Non_First Block#
                    if (blockNum > 0)
                        SET_to_AL.Request_Type = (byte)SET_req_Type.Non_First_Data_Block;
                    else
                        SET_to_AL.Request_Type = (byte)SET_req_Type.First_Data_BLOCK;
                    int MaxpackSize = Server_PDU_Size - MaxSETHeader;
                    int offSet = (int)blockNum * MaxpackSize;

                    if (offSet >= SetBuf.Length || internalBlockNum != blockNum + 1)              /// Not As Expected Block #
                        throw new DLMSEncodingException(string.Format("{0} SET Block Number received is Invalid (Error Code:{1})",
                                                    blockNum, (int)DLMSErrors.Invalid_BlockNumber), "Application_Process_Encode_SET_APDU");

                    int packLength = ((offSet + MaxpackSize) > SetBuf.Length) ? SetBuf.Length - offSet : MaxpackSize; /// Compute Set Packet Length
                    SET_to_AL.DataBlock_SA.Block_Number = internalBlockNum;
                    SET_to_AL.DataBlock_SA.Raw_Data = new byte[packLength];
                    Array.Copy(SetBuf, offSet, SET_to_AL.DataBlock_SA.Raw_Data, 0, packLength);

                    if ((offSet + packLength) >= SetBuf.Length)
                        SET_to_AL.DataBlock_SA.Last_Block = 0x01;               /// Last Block Packet Set
                    else
                        SET_to_AL.DataBlock_SA.Last_Block = 0x00;

                    internalBlockNum++;
                }
                else                                                            /// SET NORMAL 
                {
                    SET_to_AL.Request_Type = (byte)SET_req_Type.NORMAL;
                    SET_to_AL.Data = new byte[SetBuf.Length];
                    Array.Copy(SetBuf, SET_to_AL.Data, SET_to_AL.Data.Length);
                }
            }
            catch (DLMSEncodingException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DLMSEncodingException("Error Occurred While Encoding SET APDU", "Application_Process_Encode_SET_APDU");
            }
        }

        /// <summary>
        /// This helper function initialize <see cref="stCOSEM_Attribute_Descriptor"/> strut for COSEM Object and
        /// attribute Specification as passed parameters
        /// </summary>
        /// <param name="Class_ID"></param>
        /// <param name="OBIS_Code"></param>
        /// <param name="Attribute_No"></param>
        private void Build_Set_Structure(UInt16 Class_ID, byte[] OBIS_Code, byte Attribute_No)
        {
            // Assign Data
            //SET_to_AL.Data = Attribute_Value;

            // Assign obis code
            SET_to_AL.COSEM_Attribute_Descriptor.COSEM_Object_Instance_Id = OBIS_Code;

            // Assign Class ID
            SET_to_AL.COSEM_Attribute_Descriptor.COSEM_Class_Id = Class_ID;

            // Assign Attribute ID
            SET_to_AL.COSEM_Attribute_Descriptor.COSEM_Object_Attribute_Id = Attribute_No;

            SET_to_AL.COSEM_Attribute_Descriptor.Access_Selection_Parameters = (byte)DataTypes._A00_Null;
        }

        #endregion

        #region Action_Encoding

        /// <summary>
        /// This helper function initialize the <see cref="stACTION"/> that encode request APDU Action_Service
        /// </summary>
        internal byte[] Encode_Action_Object(Base_Class Invoke_Object)
        {
            byte[] IOBuffer = null;
            try
            {
                byte[] ParameterData = Invoke_Object.Encode_Parameters();
                IOBuffer = ParameterData;
                byte method_Invoke_Id = Invoke_Object.MethodInvokeId;
                byte[] T = Encode_Action_Object(Invoke_Object, method_Invoke_Id, ParameterData);
                IOBuffer = T;
                return T;
            }
            catch (DLMSException ex)
            {
                #region Debugging & Logging
#if Enable_Log_Error

                Logger.LogAPError(ex, IOBuffer, 0, (IOBuffer != null) ? IOBuffer.Length : -1, Previous_Invoke_Object.INDEX, PacketType.Action);

#endif
                #endregion

                throw ex;
            }
            catch (DLMSEncodingException ex)
            {
                #region Debugging & Logging
#if Enable_Log_Error

                Logger.LogAPEncodingError(ex, IOBuffer, 0, (IOBuffer != null) ? IOBuffer.Length : -1, Previous_Invoke_Object.INDEX);

#endif
                #endregion

                throw ex;
            }
            catch (Exception ex)
            {
                DLMSEncodingException _ex = new DLMSEncodingException("Error Occurred While Encoding Action Packet ", "Application_Process_Encode_Action");
                #region Debugging & Logging
#if Enable_Log_Error

                Logger.LogAPEncodingError(_ex, IOBuffer, 0, (IOBuffer != null) ? IOBuffer.Length : -1, Previous_Invoke_Object.INDEX);

#endif
                #endregion
                throw _ex;
            }
        }

        /// <summary>
        /// This helper function initialize the <see cref="stACTION"/> that encode request APDU Action_WTIH_BLOCK_Service
        /// </summary>
        internal byte[] Encode_Action_Object(Base_Class Invoke_Object, byte method_Id, byte[] parameterData)
        {
            try
            {
                Init_stAction_Request();                       /// Init Action StCommand
                UInt16 Class_Id = Invoke_Object.Class_ID;
                /// Store for verification in Action response
                this.Previous_Invoke_Object = Invoke_Object;
                Previous_Method_Invoke = method_Id;
                byte[] Obis_Code = (byte[])Invoke_Object.OBIS_CODE.Clone();

                if (parameterData == null)
                {
                    SetBuf = null;
                }
                else
                {
                    SetBuf = new byte[parameterData.Length];
                    Array.Copy(parameterData, SetBuf, parameterData.Length);
                }
                internalBlockNum = 1;

                /// Make structure Ready
                Build_Action_Structure(Class_Id, Obis_Code, method_Id);
                Encode_Action_APDU(ACTION_to_AL, 1);

                /// Store for verification
                Previous_Invoke_Id_Priority = ACTION_to_AL.Invoke_Id_Priority;
                Previous_Action_Request_Type = (ACTION_req_Type)ACTION_to_AL.Request_Type;
                /// Encode Packet
                return Application_Layer.ACTION_req(ACTION_to_AL);

            }
            catch (DLMSException ex)
            {

                throw ex;
            }
            catch (DLMSEncodingException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                DLMSEncodingException _ex = new DLMSEncodingException("Error Occurred While Encoding Action Packet ",
                                                                      "Application_Process_Encode_Action", ex);
                throw _ex;
            }
            finally
            {

            }
        }

        /// <summary>
        /// This helper function initialize the <see cref="stACTION"/> that encode request APDU Action_Service
        /// </summary>
        internal void Encode_Action_APDU(stACTION ACTION_to_AL, uint blockNum)
        {
            try
            {
                //Encode For Only Action Request Normal Packet
                ACTION_to_AL.Request_Type = (byte)ACTION_req_Type.Normal;
                int MaxpackSize = Server_PDU_Size - MaxSETHeader;
                if (SetBuf != null)
                {
                    if (this.SetBuf.Length > MaxpackSize)                        //Not As Expected Block #
                        throw new DLMSEncodingException("Unable to encode Action Normal Packet,length increases Max Device PDU Size",
                            "Application_Process_Encode_Action_APDU");
                    //Parameters Data Present
                    ACTION_to_AL.Flg_Method_Invocation_Parameters = 0x01;
                    ACTION_to_AL.Method_Invocation_Parameters = new byte[SetBuf.Length];
                    //Copy Action Parameters Data
                    Array.Copy(SetBuf, ACTION_to_AL.Method_Invocation_Parameters, SetBuf.Length);
                }
                else
                {
                    //Parameters Data Not Present
                    ACTION_to_AL.Flg_Method_Invocation_Parameters = 0x00;
                }

            }
            catch (DLMSEncodingException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DLMSEncodingException("Error Occurred While Encoding Action APDU", "Application_Process_Encode_Action_APDU");
            }
        }

        /// <summary>
        /// This helper function <see cref="stCOSEM_Method_Descriptor"/> strut for COSEM Object Specification(REF) and Method Specification(REF)
        /// </summary>
        /// <param name="Class_ID"></param>
        /// <param name="OBIS_Code"></param>
        /// <param name="Method_ID"></param>
        private void Build_Action_Structure(UInt16 Class_ID, byte[] OBIS_Code, byte Method_ID)
        {
            // Assign Data
            //SET_to_AL.Data = Attribute_Value;

            // Assign obis code
            ACTION_to_AL.COSEM_Method_Descriptor.COSEM_Object_Instance_Id = OBIS_Code;

            // Assign Class ID
            ACTION_to_AL.COSEM_Method_Descriptor.COSEM_Class_Id = Class_ID;

            // Assign Attribute ID
            ACTION_to_AL.COSEM_Method_Descriptor.COSEM_Object_Method_Id = Method_ID;

            //No Method Parameters Flags
            ACTION_to_AL.Flg_Method_Invocation_Parameters = 0x00;
        }

        #endregion

        #endregion

        /// <summary>
        /// DLMS_Application_Process_DecodingCompeleted,Do All Work After Decoding Complete To Update App Status
        /// </summary>
        /// <param name="arg">DLMS Base Class Reference</param>
        private void DLMS_Application_Process_DecodingComplete(Base_Class arg)
        {
            Type classType = arg.GetType();
            if (classType == typeof(Class_17))      //SAP List Received
            {
                // Instantiate Objects
                if (!this.flg_Application_Process_Init)
                    InitApplicationProcessState(((Class_17)arg).Server_Logical_Devices, DLMS_Common.InitClientSAP());
                // Make Data For Logger
                if (_GET_Response != null)
                    _GET_Response(arg);
            }
            else if (classType == typeof(Class_15))        // OBIS Codes Received
            {
                if (_GET_Response != null)
                    _GET_Response(arg);
            }
            else
            {
                // Make Data For Logger
                if (_GET_Response != null)
                    _GET_Response.Invoke(arg);
            }
        }

        /// <summary>
        /// Finalize function claim all resource in hold
        /// </summary>
        ~DLMS_Application_Process()
        {
            try
            {
                Dispose();
            }
            catch
            {
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                DetachEventHandlers();
                #region Dispose Member Variables

                Client_Logical_Devices = null;
                Send_APPLICATION_ASSOCIATION = null;
                ASSOCIATION_RELEASE_to_AL = null;
                ASSOCIATION_RELEASE_from_AL = null;
                Proposed_Conformance = null;
                GET_to_AL = null;
                GET_from_AL = null;
                SET_to_AL = null;
                SET_from_AL = null;
                ACTION_to_AL = null;
                ACTION_from_AL = null;
                SetBuf = null;
                if (logger != null)
                {
                    logger.Dispose();
                }
                if (Application_Layer != null)
                {
                    Application_Layer.Dispose();
                }
                GetSAPEntryDelegate = null;

                #endregion
            }
            catch (Exception) { }
        }

        /// <summary>
        /// This helper function Detach All AL(Application_Layer) event handlers
        /// </summary>
        internal void DetachEventHandlers()
        {
            try
            {
                #region // Remove SAPAssociation Event Handlers

                Delegate[] Handlers = null;
                if (_SAPAssociation != null)
                {
                    Handlers = _SAPAssociation.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        SAPAssociation -= (flagStatus)item;
                    }
                }

                #endregion
                #region // Remove GET_Recieved_With_Block Event Handlers

                Handlers = null;
                if (_GET_Recieved_With_Block != null)
                {
                    Handlers = _GET_Recieved_With_Block.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        GET_Recieved_With_Block -= (Get_next_sender)item;
                    }
                }

                #endregion
                #region // Remove SET_Response_With_Block Event Handlers

                Handlers = null;
                if (_SET_Response_With_Block != null)
                {
                    Handlers = _SET_Response_With_Block.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        SET_Response_With_Block -= (SET_Next_Sender)item;
                    }
                }

                #endregion
                #region // Remove DecodingComplete Event Handlers

                Handlers = null;
                if (_DecodingComplete != null)
                {
                    Handlers = _DecodingComplete.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        DecodingComplete -= (DecodingCompleted)item;
                    }
                }

                #endregion
                #region // Remove GET_Response Event Handlers

                Handlers = null;
                if (_GET_Response != null)
                {
                    Handlers = _GET_Response.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        GET_Response -= (DecodingCompleted)item;
                    }
                }

                #endregion
                #region // Remove SET_Response Event Handlers

                Handlers = null;
                if (_SET_Response != null)
                {
                    Handlers = _SET_Response.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        SET_Response -= (SetResult)item;
                    }
                }

                #endregion
                #region // Remove Action_Response Event Handlers

                Handlers = null;
                if (_Action_Response != null)
                {
                    Handlers = _Action_Response.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        Action_Response -= (Action<Action_Result>)item;
                    }
                }

                #endregion

                // De-attach Application Layer Event Handlers
                if (Application_Layer != null)
                {
                    Application_Layer.AARE_Packet_Decoded -= AARE_is_Recieved;
                    Application_Layer.GET_Packet_Decoded -= GET_Decoded_Handler;
                    Application_Layer.SET_Packet_Decoded -= SET_Decoded_Handler;
                    Application_Layer.Action_Packet_Decoded -= Action_Decoded_Handler;
                    Application_Layer.Association_Release_Packet_Decoded
                                                            -= Association_Release_is_Recieved;
                    Application_Layer.DetachEventHandlers();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while executing DetachEventHandlers_DLMS_Application_Process", ex);
            }
            finally
            {
                isHandlerAttached = false;
            }
        }

        /// <summary>
        /// This helper function attach All AL(Application_Layer) event handlers
        /// </summary>
        internal void AttachEventHandlers()
        {
            try
            {
                if (isHandlerAttached)
                    return;
                DecodingComplete += new DecodingCompleted(DLMS_Application_Process_DecodingComplete);
                if (Application_Layer != null)
                {
                    Application_Layer.AARE_Packet_Decoded += new Event_Handler_AARE(AARE_is_Recieved);
                    Application_Layer.GET_Packet_Decoded += new Event_Handler_GET(GET_Decoded_Handler);
                    Application_Layer.SET_Packet_Decoded += new Event_Handler_SET(SET_Decoded_Handler);
                    Application_Layer.Action_Packet_Decoded += new Event_Handler_Action(Action_Decoded_Handler);
                    Application_Layer.Association_Release_Packet_Decoded
                        += new Event_Handler_Association_Release(Association_Release_is_Recieved);
                    //Attach Internal Event_Handlers
                    Application_Layer.AttachEventHandlers();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while executing AttachEventHandlers_DLMS_Application_Process", ex);
            }
            finally
            {
                isHandlerAttached = true;
            }
        }

        #endregion
    }
    // End class DLMS_Application_Process
}   // End Name-space