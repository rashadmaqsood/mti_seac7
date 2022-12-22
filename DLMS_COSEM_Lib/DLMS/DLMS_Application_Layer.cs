#define Enable_Log_Message

// Code Revised And Modified
// Revision # 1.1 Tuesday, 09 October 2012
// Revision # 1.2 Monday, 15 September 2014
// Revision # 1.3 Tuesday, 01 November 2016
// Revision # 1.4 Monday, 15 May 2017
//<Comments>
// Code Revision And Modification For Code Optimization
// Code Revision For XML Summary Comment
// Code Added For DLMS Event Notification Service
// </Comments>

using System;
using System.Linq;
using DLMS.Comm;
using Serenity.Crypto;
using System.Collections.Generic;

namespace DLMS
{
    #region Enumerated_Types

    /// <summary>
    /// AARQ(REF) Request Type sent from Client Application Layer to Server Application Layer of DLMS
    /// </summary>
    public enum AARQ_Packet_Type : byte
    {
        Lowest,
        Low,
        Highest,
    }

    /// <summary>
    /// GET_Service(REF) Response Packet Type sent from Application Layer Server to Application Layer Client of DLMS
    /// </summary>
    public enum GET_req_Packet_Type : byte
    {
        NORMAL,
        NEXT,
    }

    /// <summary>
    /// SET_Service(REF) Request Packet Type sent from Client Application Layer to Server Application Layer of DLMS
    /// </summary>
    public enum SET_req_Type : byte
    {
        NORMAL = 1,
        First_Data_BLOCK = 2,
        Non_First_Data_Block,
    }

    /// <summary>
    /// SET_Service(REF) Response Packet Type sent from Server Application Layer to Client Application Layer of DLMS
    /// </summary>
    public enum SET_Response_Type : byte
    {
        NORMAL = 1,
        Non_Last_Data_BLOCK = 2,
        Last_Data_Block,
    }

    #endregion

    #region Delegate_Decleration

    /// <summary>
    ///  Delegate for decoding functions used
    /// </summary>
    internal delegate void Decoding_Function();
    /// <summary>
    /// Delegate for creating event with reqd. parameters + return type
    /// </summary>
    /// <param name="structure"></param>
    internal delegate void Event_Handler_AARE(stAPPLICATION_ASSOCIATION structure);
    /// <summary>
    /// Delegate for creating event with reqd. parameters + return type
    /// </summary>
    /// <param name="str"></param>
    internal delegate void Event_Handler_Custom(byte[] str);
    /// <summary>
    /// // Delegate for creating event with reqd. parameters + return type
    /// </summary>
    /// <param name="structure"></param>
    internal delegate void Event_Handler_GET(stGET structure);
    /// <summary>
    /// Delegate Action for creating event with reqd. parameters + return type
    /// </summary>
    /// <param name="structure"></param>
    internal delegate void Event_Handler_SET(stSET structure);
    /// <summary>
    /// Event_Handler_Action,Delegate for creating event with reqd parameters + return type
    /// </summary>
    /// <param name="structure"></param>
    internal delegate void Event_Handler_Action(stACTION structure);

    /// <summary>
    /// Event_Handler_EventNotification,Delegate for creating event with reqd parameters + return type
    /// </summary>
    /// <param name="structure"></param>
    internal delegate void Event_Handler_EventNotification(stEVENT_NOTIFICATION structure);

    /// <summary>
    /// Event_Handler_EventNotify,Delegate for creating event with reqd parameters + return type
    /// </summary>
    /// <param name="structure"></param>
    public delegate void Event_Handler_EventNotify(St_EventNotify structure);


    /// <summary>
    /// Event_Handler_Association_Release,Delegate for creating event with reqd. parameters + return type
    /// </summary>
    /// <param name="structure"></param>
    internal delegate void Event_Handler_Association_Release(stASSOCIATION_RELEASE structure);

    #endregion

    /// <summary>
    /// The DLMS_Application_Layer is equivalent to (AL) Application_Layer(REF) in DLMS_COSEM protocol stack and architecture.
    /// </summary>
    /// <remarks>
    ///  It provides services to its service user, the COSEM Application Process(REF)<see cref="DLMS.DLMS_Application_Process"/>,
    ///  and uses services provided by the supporting lower layer.
    /// This covers the Communication Services and protocols for mapping the elements of the Data_Model(REF) 
    /// (Interface Layer to Metering Device Data And Functionality) to application protocol data units (APDU)
    /// The main component of the COSEM Application Layer(REF) is the COSEM Application Service Object. 
    /// The Communication Services are Association Control Service Element, ACSE(REF);AARQ_Service(REF),ARLRQ_Service(REF)
    /// the extended DLMS Application Service Element, GET_Service(REF),SET_Service(REF),Action_Service(REF),Event_Notification_Service(REF) 
    /// </remarks>
    public class DLMS_Application_Layer : IDisposable
    {
        #region little declaration

        private DLMSLogger dlmsLogger;

        /// <summary>
        /// For decoding purpose;Buffer Passed by the supporting lower layers;
        /// </summary>
        internal byte[] Recieved_Packet;

        /// Length of APDU
        private int Length_APDU;

        /// <summary>
        /// Flag Condition;that packet has been dropped
        /// </summary>
        private byte flg_Packet_Dropped;

        // global Delegate type variable to access from within functions
        //private Decoding_Function Next_decoder;

        /// <summary>
        /// Packet char index count used in decoding
        /// </summary>
        private int decode_counter = 0;

        // string of AARE packet to decode
        //public byte[] Response_Packet;  // It gives warning (never used) but it is reqd for

        private Security_Data _security_Data;
        private ICrypto _crypto;

        #region Declare REQUEST AND RESPONSE Structures

        /// <summary>
        /// Strut used by AARQ_Service(REF) Pass By Upper Layer For Encoding Support in AL(REF)
        /// </summary>
        stAPPLICATION_ASSOCIATION APPLICATION_ASSOCIATION_to_AP;

        /// <summary>
        /// Strut used by AARQ_Service(REF) Pass to Upper Layer For Decoding Support AP(REF)
        /// </summary>
        stAPPLICATION_ASSOCIATION APPLICATION_ASSOCIATION_from_AP;

        /// <summary>
        /// Strut used by GET_Service(REF) Pass By Upper Layer For Encoding Support in AL(REF)
        /// </summary>
        stGET GET_to_AP;

        /// <summary>
        /// Strut used by GET_Service(REF) Pass to Upper Layer For Decoding Support AP(REF)
        /// </summary>
        stGET GET_from_AP;

        /// <summary>
        /// Strut used by SET_Service(REF) Pass By Upper Layer For Encoding Support in AL(REF)
        /// </summary>
        stSET SET_to_AP;

        /// <summary>
        /// Strut used by SET_Service(REF) Pass to Upper Layer For Decoding Support AP(REF)
        /// </summary>
        stSET SET_from_AP;

        /// <summary>
        /// Strut used by ACTION_Service(REF) Pass By Upper Layer For Encoding Support in AL(REF)
        /// </summary>
        stACTION ACTION_to_AP;

        /// <summary>
        /// Strut used by ACTION_Service(REF) Pass to Upper Layer For Decoding Support AP(REF)
        /// </summary>
        stACTION ACTION_from_AP;

        /// <summary>
        /// Strut used by ARLRQ_Service(REF) Pass By Upper Layer For Encoding Support in AL(REF)
        /// </summary>
        stASSOCIATION_RELEASE ASSOCIATION_RELEASE_to_AP;

        /// <summary>
        /// Strut used by ARLRQ_Service(REF) Pass to Upper Layer For Decoding Support AP(REF)
        /// </summary>
        stASSOCIATION_RELEASE ASSOCIATION_RELEASE_from_AP;

        /// <summary>
        /// Strut used by Event_Invoke_Service(REF) Pass to Upper Layer For Decoding Support AP(REF)
        /// </summary>
        stEVENT_NOTIFICATION Event_Notification_to_AP;

        #endregion

        #region  Declare Fixed Headers Data

        static internal byte[] AARQ_header2;
        static internal byte[] AARQ_header6;

        /// <summary>
        /// Fixed_Header1 Initialize for AARQ_Lowest(REF)
        /// </summary>
        internal static byte[] AARQ_Lowest_header1;
        /// <summary>
        /// Fixed_Header2 Initialize for AARQ_Lowest(REF)
        /// </summary>
        internal static byte[] AARQ_Lowest_header2;
        /// <summary>
        /// Fixed_Header3 Initialize for AARQ_Lowest(REF)
        /// </summary>
        internal static byte[] AARQ_Lowest_header3;
        /// <summary>
        /// Fixed_Header4 Initialize for AARQ_Lowest(REF)
        /// </summary>
        internal static byte[] AARQ_Lowest_header4;

        /// <summary>
        /// Fixed_Header1 Initialize for AARQ_Low(REF)
        /// </summary>
        internal static byte[] AARQ_LOW_header1;
        /// <summary>
        /// Fixed_Header2 Initialize for AARQ_Low(REF)
        /// </summary>
        internal static byte[] AARQ_LOW_header2;
        /// <summary>
        /// Fixed_Header3 Initialize for AARQ_Low(REF)
        /// </summary>
        internal static byte[] AARQ_LOW_header3;
        /// <summary>
        /// Fixed_Header4 Initialize for AARQ_Low(REF)
        /// </summary>
        internal static byte[] AARQ_LOW_header4;
        /// <summary>
        /// Fixed_Header5 Initialize for AARQ_Low(REF)
        /// </summary>
        internal static byte[] AARQ_LOW_header5;
        /// <summary>
        /// Fixed_Header6 Initialize for AARQ_Low(REF)
        /// </summary>
        internal static byte[] AARQ_LOW_header6;
        /// <summary>
        /// Fixed_Header7 Initialize for AARQ_Low(REF)
        /// </summary>
        internal static byte[] AARQ_LOW_header7;

        /// <summary>
        /// Fixed_Header1 Initialize for AARQ_HLS(REF)
        /// </summary>
        internal static byte[] AARQ_HLS_header1;
        internal static byte[] AARQ_HLS_headerAE_Invoke;
        /// <summary>
        /// Fixed_Header2 Initialize for AARQ_HLS(REF)
        /// </summary>
        internal static byte[] AARQ_HLS_header2;
        /// <summary>
        /// Fixed_Header3 Initialize for AARQ_HLS(REF)
        /// </summary>
        internal static byte[] AARQ_HLS_header3;
        /// <summary>
        /// Fixed_Header4 Initialize for AARQ_HLS(REF)
        /// </summary>
        internal static byte[] AARQ_HLS_header4;
        /// <summary>
        /// Fixed_Header5 Initialize for AARQ_HLS(REF)
        /// </summary>
        internal static byte[] AARQ_HLS_header5;
        /// <summary>
        /// Fixed_Header6 Initialize for AARQ_HLS(REF)
        /// </summary>
        internal static byte[] AARQ_HLS_header6;
        /// <summary>
        /// Fixed_Header7 Initialize for AARQ_HLS(REF)
        /// </summary>
        internal static byte[] AARQ_HLS_header7;

        /// <summary>
        /// Fixed_Header For AARQ Parameter Security MechanismName
        /// </summary>
        readonly internal static byte[] SecMechanismName = { 0x60, 0x85, 0x74, 0x05, 0x08, 0x02, 0x01 };

        #endregion

        #region Identifier tags for encoding

        private static readonly byte AARQ_Identifier = 0x60;
        private static readonly byte GET_req_Identifier = 0xC0;
        private static readonly byte SET_req_Identifier = 0xC1;
        private static readonly byte ACTION_req_Identifier = 0xC3;
        private static readonly byte APPLICATION_ASSOCIATION_Identifier = 0x62;

        #endregion

        #region Identifier tags for decoding

        public static readonly byte AARE_Identifier = 0x61;
        public static readonly byte ConfirmedServiceError_Identifier = 0x0E;
        public static readonly byte ExceptionResponse_Identifier = 0xD8;
        public static readonly byte Event_Notify_req_Identifier = 0xC2;
        public static readonly byte Get_resp_Identifier = 0xC4;
        public static readonly byte Set_resp_Identifier = 0xC5;
        public static readonly byte Action_resp_Identifier = 0xC7;
        public static readonly byte Association_Release_resp_Identifier = 0x63;

        #endregion

        #endregion

        #region Event & Delegate Type Variables Declaration

        // TCP/IP data receive event
        //---------------------------------------------------
        // <summary>
        // str_Packet_Recvd event
        // </summary>
        //internal event Event_Handler_Custom str_Packet_Recvd = null;
        //-----------------------------------------------------------------------------
        private event Event_Handler_AARE _AARE_Packet_Decoded = null; // delegate { };
        private event Event_Handler_GET _GET_Packet_Decoded = null;
        private event Event_Handler_SET _SET_Packet_Decoded = null;
        private event Event_Handler_Action _Action_Packet_Decoded = null;
        private event Event_Handler_Association_Release _Association_Release_Packet_Decoded = null;
        private event Event_Handler_EventNotification _EventNotification_Packet_Decoded = null;
        private event Action<PacketType, String> _PacketDrop = null; // delegate { };
        //-------------------------------------------------------------------------------

        /// <summary>
        /// PacketDrop event notify that some error occurred while 
        /// decoding IOPacket dropped By DLMS_Application_Layer,
        /// upper layer should  respond to packet drop event appropriately 
        /// </summary>
        internal event Action<PacketType, String> PacketDrop
        {
            add
            {
                if (_PacketDrop != value)
                    _PacketDrop += value;
                else ; // Skip Already Event Handler Attached
            }
            remove
            {
                if (_PacketDrop != null)
                    _PacketDrop -= value;
            }
        }

        /// <summary>
        /// EventNotification_Packet_Decoded event notify that UnConfirmed Event Notification 
        /// Service Response Packet Decoding Completed By DLMS_Application_Layer
        /// </summary>
        internal event Event_Handler_EventNotification EventNotification_Packet_Decoded
        {
            add
            {
                // Avoid Duplicate EventHandler Attachment
                if (_EventNotification_Packet_Decoded != value)
                    _EventNotification_Packet_Decoded += value;
                else ;// Skip Already Event Handler Attached
            }
            remove
            {
                if (_EventNotification_Packet_Decoded != null)
                    _EventNotification_Packet_Decoded -= value;
            }
        }

        /// <summary>
        /// Event_Handler_Association_Release event notify that ARLRQ service response packet 
        /// Decoding Completed By DLMS_Application_Layer
        /// </summary>
        internal event Event_Handler_Association_Release Association_Release_Packet_Decoded
        {
            add
            {
                // Avoid Duplicate EventHandler Attachment
                if (_Association_Release_Packet_Decoded != value)
                    _Association_Release_Packet_Decoded += value;
                else ;// Skip Already Event Handler Attached
            }
            remove
            {
                if (_Association_Release_Packet_Decoded != null)
                    _Association_Release_Packet_Decoded -= value;
            }
        }

        /// <summary>
        /// Action_Packet_Decoded event notify that Action Service Response 
        /// Packet Decoding Completed By DLMS_Application_Layer
        /// </summary>
        internal event Event_Handler_Action Action_Packet_Decoded
        {
            add
            {
                // Avoid Duplicate EventHandler Attachment
                if (_Action_Packet_Decoded != value)
                    _Action_Packet_Decoded += value;
                else ; // Skip Already Event Handler Attached
            }
            remove
            {
                if (_Action_Packet_Decoded != null)
                    _Action_Packet_Decoded -= value;
            }
        }

        /// <summary>
        /// SET_Packet_Decoded event notify that SET Service 
        /// Response Packet Decoding Completed By DLMS_Application_Layer
        /// </summary>
        internal event Event_Handler_SET SET_Packet_Decoded
        {
            add
            {
                // Avoid Duplicate EventHandler Attachment
                if (_SET_Packet_Decoded != value)
                    _SET_Packet_Decoded += value;
                else ; // Skip Already Event Handler Attached
            }
            remove
            {
                if (_SET_Packet_Decoded != null)
                    _SET_Packet_Decoded -= value;
            }
        }

        /// <summary>
        /// GET_Packet_Decoded event notify that Get Service 
        /// Response Packet Decoding Completed By DLMS_Application_Layer
        /// </summary>
        internal event Event_Handler_GET GET_Packet_Decoded
        {
            add
            {
                // Avoid Duplicate EventHandler Attachment
                if (_GET_Packet_Decoded != value)
                    _GET_Packet_Decoded += value;
                else ; // Skip Already Event Handler Attached
            }
            remove
            {
                if (_GET_Packet_Decoded != null)
                    _GET_Packet_Decoded -= value;
            }
        }

        /// <summary>
        /// AARE_Packet_Decoded event notify that AARE Packet Decoding Completed By DLMS_Application_Layer
        /// </summary>
        internal event Event_Handler_AARE AARE_Packet_Decoded
        {
            add
            {
                // Avoid Duplicate EventHandler Attachment
                if (_AARE_Packet_Decoded != value)
                    _AARE_Packet_Decoded += value;
                else ; // Skip Already Event Handler Attached
            }
            remove
            {
                if (_AARE_Packet_Decoded != null)
                    _AARE_Packet_Decoded -= value;
            }
        }

        #endregion

        #region Properties

        public Security_Data Security_Data
        {
            set
            {
                _security_Data = value;
            }
            get
            {
                return _security_Data;
            }
        }
        public ICrypto Crypto
        {
            set
            {
                _crypto = value;
            }
        }

        /// <summary>
        /// Get/Set the instance of <see cref="DLMSLogger"/>
        /// </summary>
        /// <remarks>
        /// The DLMSLogger instance is responsible for Logging different logical Conditions,
        /// logical CheckPoints in Process flow, different error Conditions(Message Encode/Decode,General Process).
        /// </remarks>
        public DLMSLogger DLMSLogger
        {
            get { return dlmsLogger; }
            set { dlmsLogger = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public DLMS_Application_Layer()
        {
            AttachEventHandlers();
            // Event handler for TCP event
            //TCP_Client.TCP_Packet_Recieved += new TCP_Client.Packet_Handler(str_Packet_Recvd);
            InitializeVariables();
        }

        /// <summary>
        /// Static Constructor
        /// </summary>
        static DLMS_Application_Layer()
        {
            Init_AARQ_Headers();
        }

        #endregion


        /// <summary>
        /// Initialized AARQ Fixed Length Headers
        /// </summary>
        private static void Init_AARQ_Headers()
        {
            AARQ_header2 = new byte[] { 0xA2, 0x06, 0x13, 0x04 };
            AARQ_header6 = new byte[] { 0xA6, 0x0A, 0x04, 0x08 };

            AARQ_Lowest_header1 = new byte[] { 0xA1, 0x09, 0x06, 0x07 };
            AARQ_Lowest_header2 = new byte[] { 0xBE, 0x10, 0x04, 0x0E, 0x01, 0x00 };
            AARQ_Lowest_header3 = new byte[] { 0x00 };
            AARQ_Lowest_header4 = new byte[] { 0x5F, 0x1F, 0x4, 0x00 };

            AARQ_LOW_header1 = new byte[] { 0xA1, 0x09, 0x06, 0x07 };
            AARQ_LOW_header2 = new byte[] { 0x8A, 0x02, 0x07, 0x80, 0x8B, 0x07 };
            AARQ_LOW_header3 = new byte[] { 0xAC };
            AARQ_LOW_header4 = new byte[] { 0x80 };
            AARQ_LOW_header5 = new byte[] { 0xBE, 0x10, 0x04, 0x0E, 0x1, 0x00 };
            AARQ_LOW_header6 = new byte[] { 0x00 };
            AARQ_LOW_header7 = new byte[] { 0x5F, 0x1F, 0x04, 0x00 };

            AARQ_HLS_header1 = new byte[] { 0xA1, 0x09, 0x06, 0x07 };
            AARQ_HLS_headerAE_Invoke = new byte[] { 0xA9, 0x03, 0x02, 0x01 };
            AARQ_HLS_header2 = new byte[] { 0x8A, 0x02, 0x07, 0x80, 0x8B, 0x07 };
            AARQ_HLS_header3 = new byte[] { 0xAC };
            AARQ_HLS_header4 = new byte[] { 0x80 };
            AARQ_HLS_header5 = new byte[] { 0xBE, 0x10, 0x04, 0x0E, 0x1, 0x00 };
            AARQ_HLS_header6 = new byte[] { 0x00 };
            AARQ_HLS_header7 = new byte[] { 0x5F, 0x1F, 0x04, 0x00 };
        }

        /// <summary>
        /// Initialize the local Struts in ApplicationLayer
        /// </summary>
        internal void InitializeVariables()
        {
            try
            {
                #region Construct Structures

                APPLICATION_ASSOCIATION_to_AP = new stAPPLICATION_ASSOCIATION();       /// Structure to be sent to AL
                APPLICATION_ASSOCIATION_from_AP = new stAPPLICATION_ASSOCIATION();     /// Storage for Received structure from AL

                GET_to_AP = new stGET();             /// GET from AL
                GET_from_AP = new stGET();           /// GET for AL

                SET_to_AP = new stSET();
                SET_from_AP = new stSET();

                ACTION_to_AP = new stACTION();
                ACTION_from_AP = new stACTION();

                ASSOCIATION_RELEASE_to_AP = new stASSOCIATION_RELEASE();
                ASSOCIATION_RELEASE_from_AP = new stASSOCIATION_RELEASE();

                Event_Notification_to_AP = new stEVENT_NOTIFICATION();

                #endregion

                Recieved_Packet = null;
                // Response_Packet = null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Initializing Variables_ApplicationLayer->InitializeVariables()\r\nDetails" + ex.Message);
            }
        }

        public static void Init_stEventNotification_Request(stEVENT_NOTIFICATION EventNotifier)
        {
            // Instantiate Structures
            EventNotifier.COSEM_Attribute_Descriptor = new stCOSEM_Attribute_DescriptorBase();

            EventNotifier.Time = new byte[12];
            EventNotifier.Attribute_Value = new byte[06];  // unknown length
        }

        internal void Init_stEventNotification_Request()
        {
            if (this.Event_Notification_to_AP == null)
                this.Event_Notification_to_AP = new stEVENT_NOTIFICATION();

            DLMS_Application_Layer.Init_stEventNotification_Request(this.Event_Notification_to_AP);
        }

        //--------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------
        // Encoding 
        //--------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------


        //--------------------------------------------------------------------
        //--------------------------------------------------------------------
        // Application Association Request Encoding
        //--------------------------------------------------------------------
        //--------------------------------------------------------------------

        #region AARQ Encoding

        /// <summary>
        /// This function encodes the (AARQ)Application Association Request(REF)
        /// </summary>
        /// <remarks>
        /// (AARQ);An application protocol data unit (APDU) sent by the client application layer(AL) to the server application
        ///layer(AL), as a result of invoking the COSEM-OPEN.Request service(REF).
        ///The Strut <see cref="stAPPLICATION_ASSOCIATION"/> define <see cref="stProposed_xDLMS_Context"/>
        /// and <see cref="stNegotiated_xDLMS_Context"/> parameters for Application_Context(REF).
        /// </remarks>
        /// <param name="AARQ_st_To_Encode">The Parameters Specify Application_Context(REF)</param>
        /// <returns>Encode AARQ APDU Message</returns>
        public byte[] A_ASSOCIATE_req(stAPPLICATION_ASSOCIATION AARQ_st_To_Encode)
        {
            // Assign to local variable
            APPLICATION_ASSOCIATION_from_AP = (stAPPLICATION_ASSOCIATION)AARQ_st_To_Encode.Clone();
            APPLICATION_ASSOCIATION_to_AP = (stAPPLICATION_ASSOCIATION)AARQ_st_To_Encode.Clone();

            // Variable to Store Type of packet
            AARQ_Packet_Type AARQ_type = AARQ_Packet_Type.Lowest;

            if (APPLICATION_ASSOCIATION_from_AP.Authentication_Mechanism == HLS_Mechanism.LowestSec)
                AARQ_type = AARQ_Packet_Type.Lowest;
            else if (APPLICATION_ASSOCIATION_from_AP.Authentication_Mechanism == HLS_Mechanism.LowSec)
                AARQ_type = AARQ_Packet_Type.Low;
            else
            {
                AARQ_type = AARQ_Packet_Type.Highest;
            }

            // Tell AL to form AARQ Packet of specific type
            return Build_AARQ_APDU(AARQ_type);
        }

        /// <summary>
        /// This function encodes the (AARQ)Application Association Request(REF)
        /// </summary>
        /// <param name="AARQ_packet_type">Application Association Request Type<see cref="AARQ_Packet_Type"/></param>
        /// <returns>Encode AARQ_APDU</returns>
        private byte[] Build_AARQ_APDU(AARQ_Packet_Type AARQ_packet_type)
        {
            // Temp array to store Encoded Data
            byte[] temp_AARQ_APDU = null;
            // Final Array to be returned
            byte[] Encoded_AARQ_APDU;

            byte[] App_Title = new byte[AARQ_header2.Length];
            Buffer.BlockCopy(AARQ_header2, 0, App_Title, 0, AARQ_header2.Length);

            byte[] AE_Invoke = new byte[05];

            byte[] Application_Context_Name = new byte[07];

            // Encode Structure with headers into the packet
            Application_Context_Name[0] = APPLICATION_ASSOCIATION_from_AP.Application_Context_Name.ctt_element;
            Application_Context_Name[1] = APPLICATION_ASSOCIATION_from_AP.Application_Context_Name.Country_element;
            Application_Context_Name[2] = (byte)APPLICATION_ASSOCIATION_from_AP.Application_Context_Name.Country_name_element;
            Application_Context_Name[3] = APPLICATION_ASSOCIATION_from_AP.Application_Context_Name.Organization_element;

            Application_Context_Name[4] = APPLICATION_ASSOCIATION_from_AP.Application_Context_Name.DLMS_UA_element;
            Application_Context_Name[5] = APPLICATION_ASSOCIATION_from_AP.Application_Context_Name.Application_Context_element;
            Application_Context_Name[6] = APPLICATION_ASSOCIATION_from_AP.Application_Context_Name.Context_id_element;

            byte[] Security_Mechanism_Name = new byte[07];

            // Encode Structure with headers into the packet
            Security_Mechanism_Name[0] = APPLICATION_ASSOCIATION_from_AP.Security_Mechanism_Name.ctt_element;
            Security_Mechanism_Name[1] = APPLICATION_ASSOCIATION_from_AP.Security_Mechanism_Name.Country_element;
            Security_Mechanism_Name[2] = (byte)APPLICATION_ASSOCIATION_from_AP.Security_Mechanism_Name.Country_name_element;
            Security_Mechanism_Name[3] = APPLICATION_ASSOCIATION_from_AP.Security_Mechanism_Name.Organization_element;

            Security_Mechanism_Name[4] = APPLICATION_ASSOCIATION_from_AP.Security_Mechanism_Name.DLMS_UA_element;
            Security_Mechanism_Name[5] = APPLICATION_ASSOCIATION_from_AP.Security_Mechanism_Name.Authen_mechanism_name_element;
            Security_Mechanism_Name[6] = APPLICATION_ASSOCIATION_from_AP.Security_Mechanism_Name.mechanism_id_element;


            // Packet length + contents depend upon AARQ packet type
            switch (AARQ_packet_type)
            {
                #region AARQ_Packet_Type.Lowest

                case AARQ_Packet_Type.Lowest:
                    {
                        // byte Packet_Length;
                        temp_AARQ_APDU = DLMS_Common.Append_to_End(AARQ_Lowest_header1, Application_Context_Name);

                        // Called Software Application Title
                        if (!Convert.ToBoolean(APPLICATION_ASSOCIATION_from_AP.Flg_CompatibilityMode))
                        {
                            // Null APP Title
                            if (APPLICATION_ASSOCIATION_from_AP.Called_AP_Title != null &&
                               APPLICATION_ASSOCIATION_from_AP.Called_AP_Title.AP_Title != null &&
                               APPLICATION_ASSOCIATION_from_AP.Called_AP_Title.AP_Title.Length > 0)
                            {
                                byte App_Title_Length = 08;
                                // Called Software Application Title
                                App_Title_Length = Convert.ToByte(APPLICATION_ASSOCIATION_from_AP.Called_AP_Title.AP_Title.Length);

                                // Update Application Length
                                //App_Title[01] = Convert.ToByte(App_Title_Length + 2);
                                //App_Title[03] = App_Title_Length;

                                //temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU, App_Title);
                                //temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU, APPLICATION_ASSOCIATION_from_AP.Called_AP_Title.AP_Title);
                            }
                        }

                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU, AARQ_Lowest_header2);

                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU, APPLICATION_ASSOCIATION_from_AP.Service_Class);

                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU, AARQ_Lowest_header3);

                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU,
                            APPLICATION_ASSOCIATION_from_AP.Proposed_xDLMS_Context.Proposed_DLMS_Version_Number);

                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU, AARQ_Lowest_header4);

                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU,
                            APPLICATION_ASSOCIATION_from_AP.Proposed_xDLMS_Context.Proposed_DLMS_Conformance);

                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU,
                            APPLICATION_ASSOCIATION_from_AP.Proposed_xDLMS_Context.Client_Max_Receive_PDU_Size);


                        DLMSLogger.LogALMessage("AARQ Lowest Packet is encoded ", ALMessageType.Encode, PacketType.ARQ);
                        break;
                    } // End Case 

                #endregion
                #region AARQ_Packet_Type.Low

                case AARQ_Packet_Type.Low:
                    {
                        // byte Packet_Length;
                        temp_AARQ_APDU = DLMS_Common.Append_to_End(AARQ_LOW_header1, Application_Context_Name);

                        // Called Software Application Title
                        if (!Convert.ToBoolean(APPLICATION_ASSOCIATION_from_AP.Flg_CompatibilityMode))
                        {
                            // Null Ap Title
                            if (APPLICATION_ASSOCIATION_from_AP.Called_AP_Title != null &&
                               APPLICATION_ASSOCIATION_from_AP.Called_AP_Title.AP_Title != null &&
                               APPLICATION_ASSOCIATION_from_AP.Called_AP_Title.AP_Title.Length > 0)
                            {
                                byte App_Title_Length = 08;
                                // Called Software Application Title
                                App_Title_Length = Convert.ToByte(APPLICATION_ASSOCIATION_from_AP.Called_AP_Title.AP_Title.Length);

                                // Update Application Length
                                App_Title[01] = Convert.ToByte(App_Title_Length + 2);
                                App_Title[03] = App_Title_Length;

                                temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU, App_Title);
                                temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU, APPLICATION_ASSOCIATION_from_AP.Called_AP_Title.AP_Title);
                            }
                        }

                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU, AARQ_LOW_header2);

                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU, Security_Mechanism_Name);

                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU, AARQ_LOW_header3);


                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU,
                            (byte)(APPLICATION_ASSOCIATION_from_AP.Calling_Authentication_Value.Length + 2));

                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU, AARQ_LOW_header4);

                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU,
                            (byte)APPLICATION_ASSOCIATION_from_AP.Calling_Authentication_Value.Length);

                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU,
                            APPLICATION_ASSOCIATION_from_AP.Calling_Authentication_Value);


                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU, AARQ_LOW_header5);

                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU,
                            APPLICATION_ASSOCIATION_from_AP.Service_Class);

                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU, AARQ_LOW_header6);

                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU,
                            APPLICATION_ASSOCIATION_from_AP.Proposed_xDLMS_Context.Proposed_DLMS_Version_Number);

                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU, AARQ_LOW_header7);

                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU,
                            APPLICATION_ASSOCIATION_from_AP.Proposed_xDLMS_Context.Proposed_DLMS_Conformance);

                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU,
                            APPLICATION_ASSOCIATION_from_AP.Proposed_xDLMS_Context.Client_Max_Receive_PDU_Size);


                        DLMSLogger.LogALMessage("AARQ Low Packet is encoded ", ALMessageType.Encode, PacketType.ARQ);
                        break;
                    } // End Case

                #endregion
                #region AARQ_Packet_Type.Highest

                case AARQ_Packet_Type.Highest:
                    {
                        byte App_Title_Length = 08;

                        // byte Packet_Length;
                        temp_AARQ_APDU = DLMS_Common.Append_to_End(AARQ_HLS_header1, Application_Context_Name);

                        // Called Software Application Title
                        if (!Convert.ToBoolean(APPLICATION_ASSOCIATION_from_AP.Flg_CompatibilityMode))
                        {
                            // Null AP Title
                            if (APPLICATION_ASSOCIATION_from_AP.Called_AP_Title != null &&
                               APPLICATION_ASSOCIATION_from_AP.Called_AP_Title.AP_Title != null &&
                               APPLICATION_ASSOCIATION_from_AP.Called_AP_Title.AP_Title.Length > 0)
                            {
                                // Called Software Application Title
                                App_Title_Length = Convert.ToByte(APPLICATION_ASSOCIATION_from_AP.Called_AP_Title.AP_Title.Length);

                                // Update Application Length
                                App_Title[01] = Convert.ToByte(App_Title_Length + 2);
                                App_Title[03] = App_Title_Length;

                                temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU, App_Title);
                                temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU, APPLICATION_ASSOCIATION_from_AP.Called_AP_Title.AP_Title);
                            }
                        }

                        // Calling Software Application Title
                        // Null App Title
                        if (APPLICATION_ASSOCIATION_from_AP.Calling_AP_Title != null &&
                           APPLICATION_ASSOCIATION_from_AP.Calling_AP_Title.AP_Title != null &&
                           APPLICATION_ASSOCIATION_from_AP.Calling_AP_Title.AP_Title.Length > 0)
                        {
                            App_Title_Length = Convert.ToByte(APPLICATION_ASSOCIATION_from_AP.Calling_AP_Title.AP_Title.Length);

                            App_Title = new byte[AARQ_header6.Length];
                            Buffer.BlockCopy(AARQ_header6, 0, App_Title, 0, AARQ_header6.Length);

                            // Update Application Length
                            App_Title[01] = Convert.ToByte(App_Title_Length + 2);
                            App_Title[03] = App_Title_Length;

                            temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU, App_Title);
                            temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU, APPLICATION_ASSOCIATION_from_AP.Calling_AP_Title.AP_Title);
                        }


                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU, AARQ_HLS_header2);

                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU, Security_Mechanism_Name);

                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU, AARQ_HLS_header3);

                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU,
                            (byte)(APPLICATION_ASSOCIATION_from_AP.Calling_Authentication_Value.Length + 2));

                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU, AARQ_HLS_header4);

                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU,
                            (byte)APPLICATION_ASSOCIATION_from_AP.Calling_Authentication_Value.Length);

                        temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU,
                            APPLICATION_ASSOCIATION_from_AP.Calling_Authentication_Value);

                        // Process XDLMS User Information APDU
                        byte[] userInfo_APDU = AARQ_HLS_header5;

                        userInfo_APDU = DLMS_Common.Append_to_End(userInfo_APDU,
                                APPLICATION_ASSOCIATION_from_AP.Service_Class);

                        userInfo_APDU = DLMS_Common.Append_to_End(userInfo_APDU, AARQ_HLS_header6);

                        userInfo_APDU = DLMS_Common.Append_to_End(userInfo_APDU,
                            APPLICATION_ASSOCIATION_from_AP.Proposed_xDLMS_Context.Proposed_DLMS_Version_Number);

                        userInfo_APDU = DLMS_Common.Append_to_End(userInfo_APDU, AARQ_HLS_header7);

                        userInfo_APDU = DLMS_Common.Append_to_End(userInfo_APDU,
                            APPLICATION_ASSOCIATION_from_AP.Proposed_xDLMS_Context.Proposed_DLMS_Conformance);

                        userInfo_APDU = DLMS_Common.Append_to_End(userInfo_APDU,
                            APPLICATION_ASSOCIATION_from_AP.Proposed_xDLMS_Context.Client_Max_Receive_PDU_Size);

                        // Secure UserInfo Field
                        // LN_Referencing_With_Ciphering
                        if (APPLICATION_ASSOCIATION_from_AP.Application_Context_ID >= Application_ContextName.LN_Referencing_With_Ciphering &&
                            _security_Data.SecurityControl > SecurityControl.None)
                        {
                            byte[] cipheredText = Security_Context.Apply_AARQ_Encryption(_security_Data, _crypto,
                                                                     userInfo_APDU.GetSegment<byte>(4)).Array;

                            byte[] Local_AARQ_HLS_header5 = new byte[] { 0xBE, 0x10, 0x04, 0x0E };
                            Local_AARQ_HLS_header5[1] = (byte)(cipheredText.Length + 2);
                            Local_AARQ_HLS_header5[3] = (byte)(cipheredText.Length);

                            cipheredText = DLMS_Common.Append_to_Start(cipheredText, Local_AARQ_HLS_header5);

                            temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU, cipheredText);
                        }
                        else
                        {
                            temp_AARQ_APDU = DLMS_Common.Append_to_End(temp_AARQ_APDU, userInfo_APDU);
                        }

                        // Debugger & Logger
                        DLMSLogger.LogALMessage("AARQ Highest Packet is encoded ", ALMessageType.Encode, PacketType.ARQ);
                        break;
                    }

                #endregion   // End Case
                default:
                    {
                        Encoded_AARQ_APDU = new byte[1] { 5 };
                        break;
                    }   // End Default

            } // End Switch

            // Packet_Length = (byte)temp_AARQ_APDU.Length;
            // temp_AARQ_APDU = Common.Append_to_Start(temp_AARQ_APDU, Packet_Length);
            temp_AARQ_APDU = BasicEncodeDecode.Append_Length_to_Start(temp_AARQ_APDU);
            temp_AARQ_APDU = DLMS_Common.Append_to_Start(temp_AARQ_APDU, AARQ_Identifier);

            // Copy To final Array
            Encoded_AARQ_APDU = new byte[temp_AARQ_APDU.Length];
            temp_AARQ_APDU.CopyTo(Encoded_AARQ_APDU, 0);

            // Return Encoded array
            return Encoded_AARQ_APDU;
        } // end function Build_AARQ_APDU

        #endregion

        //------------------------------------- GET ENCODING ---------------->>
        //--------------------------------------------------------------------
        //--------------------------------------------------------------------
        // GET Request Encoding
        //--------------------------------------------------------------------
        //--------------------------------------------------------------------

        #region GET Encoding

        /// <summary>
        /// This function encodes the GET Request(REF),the GET service, used to read the value of one or more attributes of COSEM interface objects
        /// </summary>
        /// <remarks>
        ///The GET_Service takes <see cref="stGET"/>,<see cref="stCOSEM_Attribute_Descriptor"/>  as argument to retrieve attributes of COSEM interface objects.
        /// The <see cref="stCOSEM_Attribute_Descriptor"/> define Logical Name(LN),COSEM IC(REF) Class Id,Attribute No and Attribute Access Descriptor. 
        /// </remarks>
        /// <param name="GET_st_To_Encode">The Struct hold  <see cref="stGET"/> Parameters for <see cref="stCOSEM_Attribute_Descriptor"/>(REF)</param>
        /// <returns>Encoded GET APDU</returns>
        public byte[] GET_req(stGET GET_st_To_Encode)
        {
            // Assign to local variable
            GET_from_AP = (stGET)GET_st_To_Encode.Clone();
            // variable to store type of packet
            GET_req_Packet_Type GET_Type;
            // Check which type of packet is to be formed
            if (GET_from_AP.Request_Type == 1)
            {
                GET_Type = GET_req_Packet_Type.NORMAL;
            }
            else
            {
                GET_Type = GET_req_Packet_Type.NEXT;
            }
            // Tell AL to form Get Request Packet of specific type
            return Build_GET_REQ_APDU(GET_Type);
        } // End GET_req

        /// <summary>
        /// This function encodes the GET Request(REF) APDU
        /// </summary>
        /// <param name="Get_Type">Specify GET_Request_Type<see cref="GET_req_Packet_Type"/></param>
        /// <returns>Encoded GET REQ APDU</returns>
        private byte[] Build_GET_REQ_APDU(GET_req_Packet_Type Get_Type)
        {
            // Temp array to store Encoded Data
            byte[] temp_GET_REQ_APDU;
            // Final Array to be returned
            byte[] Encoded_GET_REQ_APDU;
            // Packet length + contents depend upon AARQ packet type
            switch (Get_Type)
            {
                case GET_req_Packet_Type.NORMAL:
                    {
                        // Encode Structure with headers into the packet
                        temp_GET_REQ_APDU = DLMS_Common.Append_to_End(GET_req_Identifier, GET_from_AP.Request_Type);
                        temp_GET_REQ_APDU = DLMS_Common.Append_to_End(temp_GET_REQ_APDU, GET_from_AP.Invoke_Id_Priority);
                        temp_GET_REQ_APDU = DLMS_Common.Append_to_End(temp_GET_REQ_APDU,
                            GET_from_AP.COSEM_Attribute_Descriptor.COSEM_Class_Id);

                        //<<>><<>><<>><<>><<>><<>><<>><<>><<>><<>><<>><<>><<>><<>><<>><<>><<>><<>> razaahsan modification
                        //if( ( GET_from_AP.COSEM_Attribute_Descriptor.COSEM_Object_Instance_Id[0] ==  1) &&
                        //    ( GET_from_AP.COSEM_Attribute_Descriptor.COSEM_Object_Instance_Id[1] ==  0) &&
                        //    ( GET_from_AP.COSEM_Attribute_Descriptor.COSEM_Object_Instance_Id[2] == 98) &&
                        //    ( GET_from_AP.COSEM_Attribute_Descriptor.COSEM_Object_Instance_Id[3] ==  1) &&
                        //    ( GET_from_AP.COSEM_Attribute_Descriptor.COSEM_Object_Instance_Id[4] ==  0) )
                        //    GET_from_AP.COSEM_Attribute_Descriptor.COSEM_Object_Instance_Id[5] = 124;
                        //<<>><<>><<>><<>><<>><<>><<>><<>><<>><<>><<>><<>><<>><<>><<>><<>><<>><<>> razaahsan modification
                        //<<>><<>><<>><<>><<>><<>><<>><<>><<>><<>><<>><<>><<>><<>><<>><<>><<>><<>> razaahsan modification

                        temp_GET_REQ_APDU = DLMS_Common.Append_to_End(temp_GET_REQ_APDU,
                            GET_from_AP.COSEM_Attribute_Descriptor.COSEM_Object_Instance_Id);
                        temp_GET_REQ_APDU = DLMS_Common.Append_to_End(temp_GET_REQ_APDU,
                            GET_from_AP.COSEM_Attribute_Descriptor.COSEM_Object_Attribute_Id);
                        //Access Selection Not Null,Append Access Selection Params
                        if (GET_from_AP.COSEM_Attribute_Descriptor.Access_Selection_Parameters != (byte)DataTypes._A00_Null)
                        {
                            //Encode Access Selection
                            temp_GET_REQ_APDU = DLMS_Common.Append_to_End(temp_GET_REQ_APDU,
                            new byte[] { 1, GET_from_AP.COSEM_Attribute_Descriptor.Access_Selection_Parameters });
                            if (GET_from_AP.COSEM_Attribute_Descriptor.Access_Parameters == null)
                                throw new DLMSEncodingException("Invalid Access Selection Parameters", "Build_GET_Req_ApplicationLayer");
                            temp_GET_REQ_APDU = DLMS_Common.Append_to_End(temp_GET_REQ_APDU,
                                GET_from_AP.COSEM_Attribute_Descriptor.Access_Parameters);
                        }
                        else
                            temp_GET_REQ_APDU = DLMS_Common.Append_to_End(temp_GET_REQ_APDU, (byte)DataTypes._A00_Null);
                        // Copy to final array
                        Encoded_GET_REQ_APDU = new byte[temp_GET_REQ_APDU.Length];
                        DLMS_Common.Byte_Array_Copier(temp_GET_REQ_APDU, ref Encoded_GET_REQ_APDU, 0);
                        #region Debugger & Logger
#if Enable_Log_Message
                        //***Debugger & Logger
                        DLMSLogger.LogALMessage("Normal Packet is encoded ", ALMessageType.Encode, PacketType.GET);
#endif
                        #endregion
                        break;
                    } // End Case

                case GET_req_Packet_Type.NEXT:
                    {
                        // Encode Structure with headers into the packet
                        temp_GET_REQ_APDU = DLMS_Common.Append_to_End(GET_req_Identifier, GET_from_AP.Request_Type);
                        temp_GET_REQ_APDU = DLMS_Common.Append_to_End(temp_GET_REQ_APDU, GET_from_AP.Invoke_Id_Priority);
                        temp_GET_REQ_APDU = DLMS_Common.Append_to_End(temp_GET_REQ_APDU,
                            GET_from_AP.Block_Number);

                        // Copy to final array
                        Encoded_GET_REQ_APDU = new byte[temp_GET_REQ_APDU.Length];
                        DLMS_Common.Byte_Array_Copier(temp_GET_REQ_APDU, ref Encoded_GET_REQ_APDU, 0);
                        #region Debugger & Logger
#if Enable_Log_Message
                        //***Debugger & Logger
                        DLMSLogger.LogALMessage("Next Packet encoded ", ALMessageType.Encode, PacketType.GET);
#endif
                        #endregion
                        break;

                    } // End Case
                default:
                    {
                        Encoded_GET_REQ_APDU = new byte[1] { 5 };
                        break;
                    } // End Default

            } //End Switch
            return Encoded_GET_REQ_APDU;
        } // end function Build_GET_REQ_APDU

        #endregion

        ///------------------------------------- SET ENCODING ----------------->>
        //--------------------------------------------------------------------
        //--------------------------------------------------------------------
        // SET Request Encoding
        //--------------------------------------------------------------------
        //--------------------------------------------------------------------

        #region SET Encoding

        /// <summary>
        /// This function encodes the SET Request(REF),the SET service, used to write the value of one or more attributes of COSEM interface objects
        /// </summary>
        /// <remarks>
        /// The SET_Service takes <see cref="stSET"/>,<see cref="stCOSEM_Attribute_Descriptor"/> as argument to write data for attributes of COSEM interface objects.
        /// The <see cref="stCOSEM_Attribute_Descriptor"/> define Logical Name(LN),COSEM IC(REF) Class Id,Attribute No and Attribute Access Descriptor. 
        /// </remarks>
        /// <param name="SET_st_To_Encode">The Struct hold Parameters for <see cref="stCOSEM_Attribute_Descriptor"/>(REF)</param>
        /// <returns>Encoded SET APDU</returns>
        public byte[] SET_req(stSET SET_st_To_Encode)
        {
            // Assign to local variable
            SET_from_AP = (stSET)SET_st_To_Encode.Clone();
            // Tell AL to form Set Request Packet of specific type
            return Build_SET_REQ_APDU((SET_req_Type)SET_from_AP.Request_Type);
        } // End SET_req

        /// <summary>
        /// This helper function SET_Request(REF) APDU
        /// </summary>
        /// <param name="Set_Type">Specify the type of <see cref="SET_req_Type"/></param>
        /// <returns>Encoded SET REQ APDU</returns>
        private byte[] Build_SET_REQ_APDU(SET_req_Type Set_Type)
        {
            // Temp array to store Encoded Data
            byte[] temp_SET_REQ_APDU;
            // Final Array to be returned
            byte[] Encoded_SET_REQ_APDU;

            // Packet length + contents depend upon AARQ packet type
            switch (Set_Type)
            {
                #region SET_req_Type.NORMAL

                case SET_req_Type.NORMAL:
                    {
                        // Encode Structure with headers into the packet
                        temp_SET_REQ_APDU = DLMS_Common.Append_to_End(SET_req_Identifier, SET_from_AP.Request_Type);       //Request Type 
                        temp_SET_REQ_APDU = DLMS_Common.Append_to_End(temp_SET_REQ_APDU, SET_from_AP.Invoke_Id_Priority);  //Invoke Proiority ID
                        temp_SET_REQ_APDU = DLMS_Common.Append_to_End(temp_SET_REQ_APDU,                                   //DLSM COSEM Attribute Descripter
                            SET_from_AP.COSEM_Attribute_Descriptor.COSEM_Class_Id);                                        //DLMS COSEM Class ID
                        temp_SET_REQ_APDU = DLMS_Common.Append_to_End(temp_SET_REQ_APDU,
                            SET_from_AP.COSEM_Attribute_Descriptor.COSEM_Object_Instance_Id);                              //Instance ID
                        temp_SET_REQ_APDU = DLMS_Common.Append_to_End(temp_SET_REQ_APDU,
                            SET_from_AP.COSEM_Attribute_Descriptor.COSEM_Object_Attribute_Id);                             //DLMS COSEM Attribute ID
                        temp_SET_REQ_APDU = DLMS_Common.Append_to_End(temp_SET_REQ_APDU,
                            SET_from_AP.COSEM_Attribute_Descriptor.Access_Selection_Parameters);                           //Access Selection Parameters
                        temp_SET_REQ_APDU = DLMS_Common.Append_to_End(temp_SET_REQ_APDU,                                   //RAW Data To BE SENT
                            SET_from_AP.Data);
                        // Copy to final array
                        Encoded_SET_REQ_APDU = new byte[temp_SET_REQ_APDU.Length];
                        DLMS_Common.Byte_Array_Copier(temp_SET_REQ_APDU, ref Encoded_SET_REQ_APDU, 0);
                        break;
                    } // End Case 
                #endregion
                #region SET_req_Type.First_Data_BLOCK
                case SET_req_Type.First_Data_BLOCK:
                    {
                        // Encode Structure with headers into the packet
                        temp_SET_REQ_APDU = DLMS_Common.Append_to_End(SET_req_Identifier, SET_from_AP.Request_Type);            //Request Type
                        temp_SET_REQ_APDU = DLMS_Common.Append_to_End(temp_SET_REQ_APDU, SET_from_AP.Invoke_Id_Priority);       //Invoke Proiority ID
                        temp_SET_REQ_APDU = DLMS_Common.Append_to_End(temp_SET_REQ_APDU,                                        //DLSM COSEM Attribute Descripter
                            SET_from_AP.COSEM_Attribute_Descriptor.COSEM_Class_Id);                                             //DLMS COSEM Class ID
                        temp_SET_REQ_APDU = DLMS_Common.Append_to_End(temp_SET_REQ_APDU,
                            SET_from_AP.COSEM_Attribute_Descriptor.COSEM_Object_Instance_Id);                                   //Instance ID
                        temp_SET_REQ_APDU = DLMS_Common.Append_to_End(temp_SET_REQ_APDU,
                            SET_from_AP.COSEM_Attribute_Descriptor.COSEM_Object_Attribute_Id);                                  //DLMS COSEM Attribute ID
                        temp_SET_REQ_APDU = DLMS_Common.Append_to_End(temp_SET_REQ_APDU,
                            SET_from_AP.COSEM_Attribute_Descriptor.Access_Selection_Parameters);                                //Data Block_SA
                        temp_SET_REQ_APDU = DLMS_Common.Append_to_End(temp_SET_REQ_APDU, SET_from_AP.DataBlock_SA.Last_Block);  //Last Block 
                        temp_SET_REQ_APDU = DLMS_Common.Append_to_End(temp_SET_REQ_APDU, SET_from_AP.DataBlock_SA.Block_Number);//Block Number
                        byte[] lengthArr = null;
                        BasicEncodeDecode.Encode_Length(ref lengthArr, (ushort)SET_from_AP.DataBlock_SA.Raw_Data.Length);     //Append RAW Length
                        temp_SET_REQ_APDU = DLMS_Common.Append_to_End(temp_SET_REQ_APDU, lengthArr);
                        temp_SET_REQ_APDU = DLMS_Common.Append_to_End(temp_SET_REQ_APDU, SET_from_AP.DataBlock_SA.Raw_Data);    //RAW Data
                        // Copy to final array
                        Encoded_SET_REQ_APDU = new byte[temp_SET_REQ_APDU.Length];
                        DLMS_Common.Byte_Array_Copier(temp_SET_REQ_APDU, ref Encoded_SET_REQ_APDU, 0);
                        break;
                    } // End Case 
                #endregion
                #region SET_req_Type.Non_First_Data_Block
                case SET_req_Type.Non_First_Data_Block:
                    {
                        // Encode Structure with headers into the packet
                        temp_SET_REQ_APDU = DLMS_Common.Append_to_End(SET_req_Identifier, SET_from_AP.Request_Type);            //Request Type
                        temp_SET_REQ_APDU = DLMS_Common.Append_to_End(temp_SET_REQ_APDU, SET_from_AP.Invoke_Id_Priority);       //Invoke Proiority ID
                        //Data Block_SA
                        temp_SET_REQ_APDU = DLMS_Common.Append_to_End(temp_SET_REQ_APDU, SET_from_AP.DataBlock_SA.Last_Block);  //Last Block 
                        temp_SET_REQ_APDU = DLMS_Common.Append_to_End(temp_SET_REQ_APDU, SET_from_AP.DataBlock_SA.Block_Number);//Block Number
                        byte[] lengthArr = null;
                        BasicEncodeDecode.Encode_Length(ref lengthArr, (ushort)SET_from_AP.DataBlock_SA.Raw_Data.Length);     //Append RAW Length
                        temp_SET_REQ_APDU = DLMS_Common.Append_to_End(temp_SET_REQ_APDU, lengthArr);
                        temp_SET_REQ_APDU = DLMS_Common.Append_to_End(temp_SET_REQ_APDU, SET_from_AP.DataBlock_SA.Raw_Data);    //RAW Data
                        // Copy to final array
                        Encoded_SET_REQ_APDU = new byte[temp_SET_REQ_APDU.Length];
                        DLMS_Common.Byte_Array_Copier(temp_SET_REQ_APDU, ref Encoded_SET_REQ_APDU, 0);
                        break;
                    } // End Case 
                #endregion
                default:
                    {
                        Encoded_SET_REQ_APDU = new byte[1] { 5 };
                        break;
                    } // End Default

            }
            // End Switch
            #region Debugger & Logger
#if Enable_Log_Message
            //***Debugger & Logger
            DLMSLogger.LogALMessage(String.Format("SET {0} Packet is encoded,", Set_Type), ALMessageType.Encode, PacketType.SET);
#endif
            #endregion
            return Encoded_SET_REQ_APDU;

        } // end function Build_SET_REQ_APDU

        #endregion

        //------------------------------------- ACTION ENCODING ------------->>
        //--------------------------------------------------------------------
        //--------------------------------------------------------------------
        // Action Request Encoding
        //--------------------------------------------------------------------
        //--------------------------------------------------------------------

        #region Action Encoding

        /// <summary>
        /// This function encodes the Action_Request(REF),the ACTION_Service(REF), used to invoke one or more methods of COSEM interface objects.
        /// </summary>
        /// <remarks>
        /// The ACTION_Service(REF) takes <see cref="stACTION"/>,<see cref="stCOSEM_Method_Descriptor"/>  as argument to 
        /// invoke particular method of COSEM interface objects.
        /// The <see cref="stCOSEM_Method_Descriptor"/> define Logical Name(LN),COSEM IC(REF) Class Id and Method Invoke Id.
        /// </remarks>
        /// <param name="ACTION_st_To_Encode">The Struct hold  <see cref="stACTION"/> Parameters for <see cref="stCOSEM_Method_Descriptor"/>(REF)</param>
        /// <returns>The Encoded Action_Request APDU</returns>
        public byte[] ACTION_req(stACTION ACTION_st_To_Encode)
        {
            // Assign to local variable
            ACTION_from_AP = (stACTION)ACTION_st_To_Encode.Clone();
            // variable to store type of packet
            ACTION_req_Type ACTION_Type;
            // Check which type of packet is to be formed
            if (ACTION_from_AP.Request_Type == (byte)ACTION_req_Type.Normal)
            {
                ACTION_Type = ACTION_req_Type.Normal;
            }
            else
            {
                ACTION_Type = (ACTION_req_Type)ACTION_from_AP.Request_Type;
            }
            // Tell AL to form ACTION Request Packet of specific type
            return Build_ACTION_REQ_APDU(ACTION_Type);
        } // End ACTION_req

        /// <summary>
        /// This function encodes the Action_Request(REF) APDU
        /// </summary>
        /// <param name="ACTION_Type"><see cref="ACTION_req_Type"/>Specify Request Type</param>
        /// <returns>The Encoded Action_Request APDU</returns>
        private byte[] Build_ACTION_REQ_APDU(ACTION_req_Type ACTION_Type)
        {
            // Temp array to store Encoded Data
            byte[] temp_ACTION_REQ_APDU;
            // Final Array to be returned
            byte[] Encoded_ACTION_REQ_APDU;

            // Packet_length + contents depend upon ACTION request packet type
            switch (ACTION_Type)
            {
                case ACTION_req_Type.Normal:
                    {
                        // Encode Structure with headers into the packet
                        temp_ACTION_REQ_APDU = DLMS_Common.Append_to_End(ACTION_req_Identifier, ACTION_from_AP.Request_Type);
                        temp_ACTION_REQ_APDU = DLMS_Common.Append_to_End(temp_ACTION_REQ_APDU, ACTION_from_AP.Invoke_Id_Priority);
                        temp_ACTION_REQ_APDU = DLMS_Common.Append_to_End(temp_ACTION_REQ_APDU,
                            ACTION_from_AP.COSEM_Method_Descriptor.COSEM_Class_Id);
                        temp_ACTION_REQ_APDU = DLMS_Common.Append_to_End(temp_ACTION_REQ_APDU,
                            ACTION_from_AP.COSEM_Method_Descriptor.COSEM_Object_Instance_Id);
                        temp_ACTION_REQ_APDU = DLMS_Common.Append_to_End(temp_ACTION_REQ_APDU,
                            ACTION_from_AP.COSEM_Method_Descriptor.COSEM_Object_Method_Id);
                        temp_ACTION_REQ_APDU = DLMS_Common.Append_to_End(temp_ACTION_REQ_APDU,
                            ACTION_from_AP.Flg_Method_Invocation_Parameters);
                        if (ACTION_from_AP.Flg_Method_Invocation_Parameters != 0)
                        {
                            temp_ACTION_REQ_APDU = DLMS_Common.Append_to_End(temp_ACTION_REQ_APDU,
                            ACTION_from_AP.Method_Invocation_Parameters);
                        }
                        // Copy to final array
                        Encoded_ACTION_REQ_APDU = new byte[temp_ACTION_REQ_APDU.Length];
                        DLMS_Common.Byte_Array_Copier(temp_ACTION_REQ_APDU, ref Encoded_ACTION_REQ_APDU, 0);
                        #region Debugger & Logger
#if Enable_Log_Message
                        //***Debugger & Logger
                        DLMSLogger.LogALMessage("Request Normal is Encoded ", ALMessageType.Encode, PacketType.Action);
#endif
                        #endregion
                        break;

                    } // End Case
                default:
                    {
                        Encoded_ACTION_REQ_APDU = new byte[1] { 5 };
                        #region Debugger & Logger
#if Enable_Log_Message
                        //***Debugger & Logger
                        DLMSLogger.LogALMessage(String.Format("Unable to Encode Action Request Type {0}_{1}", ACTION_Type,
                            "Build_ACTION_REQ_APDU_Application_Layer"), ALMessageType.Encode, PacketType.Action);
#endif
                        #endregion
                        throw new DLMSEncodingException(String.Format("Unable to Encode Action Request Type {0}", ACTION_Type),
                                                    "Build_ACTION_REQ_APDU_Application_Layer");
                        break;
                    } // End Default

            } // End Switch
            return Encoded_ACTION_REQ_APDU;
        } // end function Build_ACTION_REQ_APDU

        #endregion

        //------------------------------ ASSOCIATION RELEASE ENCODING ------->>
        //--------------------------------------------------------------------
        //--------------------------------------------------------------------
        // ASSOCIATION_RELEASE Encoding
        //--------------------------------------------------------------------
        //--------------------------------------------------------------------

        #region Association Release Encoding

        /// <summary>
        /// This function encodes the (ARLRQ) Application Association Release Request(REF) APDU
        /// </summary>
        /// <remarks>
        /// (ARLRQ);An application protocol data unit (APDU) sent by the client application layer(AL) to the server application
        ///layer(AL), as a result of invoking the COSEM-RELEASE.Request(REF) service.
        ///The Struct <see cref="stASSOCIATION_RELEASE"/> define <see cref="stProposed_xDLMS_Context"/>
        /// and <see cref="stNegotiated_xDLMS_Context"/> parameters for Application_Context(REF).
        /// </remarks>
        /// <param name="AASSOCIATION_RELEASE_st_To_Encode">The Parameters Specify Application_Context(REF)</param>
        /// <returns>Encoded ARLRQ(REF) APDU</returns>
        public byte[] ASSOCIATION_RELEASE(stASSOCIATION_RELEASE AASSOCIATION_RELEASE_st_To_Encode)
        {
            // Assign to local variable
            ASSOCIATION_RELEASE_from_AP = (stASSOCIATION_RELEASE)AASSOCIATION_RELEASE_st_To_Encode.Clone();
            // Tell AL to form ASSOCIATION_RELEASE Request Packet of specific type
            return Build_ASSOCIATION_RELEASE_APDU();
        } // End ASSOCIATION_RELEASE

        /// <summary>
        ///  This function encodes the (ARLRQ) Application Association Release Request(REF) APDU
        /// </summary>
        /// <returns>Encoded ARLRQ(REF) APDU</returns>
        private byte[] Build_ASSOCIATION_RELEASE_APDU()
        {

            // Temp array to store Encoded Data
            byte[] temp_ASSOCIATION_RELEASE_APDU;
            // Final Array to be returned
            byte[] Encoded_ASSOCIATION_RELEASE_APDU;

            if (ASSOCIATION_RELEASE_from_AP.Reason != 0xff)
            {
                temp_ASSOCIATION_RELEASE_APDU = DLMS_Common.Append_to_End(0x80, 0x01);   // Reason_available + length of reason
                temp_ASSOCIATION_RELEASE_APDU = DLMS_Common.Append_to_End(temp_ASSOCIATION_RELEASE_APDU,
                    ASSOCIATION_RELEASE_from_AP.Reason);
                temp_ASSOCIATION_RELEASE_APDU = DLMS_Common.Append_to_Start(temp_ASSOCIATION_RELEASE_APDU,
                    0x03);   // length of remaining APDU
                temp_ASSOCIATION_RELEASE_APDU = DLMS_Common.Append_to_Start(temp_ASSOCIATION_RELEASE_APDU,
                    APPLICATION_ASSOCIATION_Identifier);   // length of remaining APDU
            }
            else
            {
                temp_ASSOCIATION_RELEASE_APDU = DLMS_Common.Append_to_End(APPLICATION_ASSOCIATION_Identifier, 0x00);
            }
            Encoded_ASSOCIATION_RELEASE_APDU = new byte[temp_ASSOCIATION_RELEASE_APDU.Length];
            temp_ASSOCIATION_RELEASE_APDU.CopyTo(Encoded_ASSOCIATION_RELEASE_APDU, 0);
            #region Debugger & Logger
#if Enable_Log_Message

            //***Debugger & Logger
            DLMSLogger.LogALMessage("Application_Association_Release Packet encoded ", ALMessageType.Encode, PacketType.RLRQ);

#endif
            #endregion
            return Encoded_ASSOCIATION_RELEASE_APDU;

        } // end function Build_ASSOCIATION_RELEASE_APDU

        #endregion

        public void Raise_Packet_Recv_event()
        {
            // Raise event of packet received with AARE type fix coded above
            // str_Packet_Recvd(Response_Packet);
        }

        /// <summary>
        /// This function pass IO_Buffer received for DLMS_COSEM_Application_Layer(REF)
        /// </summary>
        /// <remarks>
        /// This function passed IO_Buffer received for DLMS_COSEM_Application_Layer(REF) and
        /// then perform series of Data Decoder functions to invoke Response on AL(REF) 
        /// Client Application Process(AP) <see cref="DLMS_Application_Process"/>  module.
        /// </remarks>
        /// <param name="dt">Encoded Buffer</param>
        public void packetReceived(byte[] dt)
        {
            //string s = Common.ArrayToHexString(dt);
            //MsgDisplay.AppendCommResp(s);
            //str_Packet_Recvd.Invoke(dt);
            Decode_APDU(dt);
        }

        /// <summary>
        /// This function pass IO_Buffer received for DLMS_COSEM_Application_Layer(REF)
        /// </summary>
        /// <remarks>
        /// This function passed IO_Buffer received for DLMS_COSEM_Application_Layer(REF) and
        /// then perform series of Data Decoder functions to invoke Response on AL(REF) 
        /// Client Application Process(AP) <see cref="DLMS_Application_Process"/>  module.
        /// </remarks>
        /// <param name="dt">Encoded Buffer</param>
        public void packetReceived(ArraySegment<byte> dt)
        {
            // string s = Common.ArrayToHexString(dt);
            // MsgDisplay.AppendCommResp(s);
            // str_Packet_Recvd.Invoke(dt);
            Decode_APDU(dt.Array, dt.Offset, dt.Count);
        }

        //--------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------
        // Decoding 
        //--------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------

        /// private void Decode_APDU(string str_Recvd_Packet)
        private void Decode_APDU(byte[] str_Recvd_Packet, int offsetLocal = 0, int length = -1)
        {
            //get hex_array
            //Recieved_Packet = Common.String_to_Hex_array(str_Recvd_Packet);
            Recieved_Packet = str_Recvd_Packet;
            flg_Packet_Dropped = 0;
            decode_counter = offsetLocal;

            if (offsetLocal < 0 || offsetLocal > str_Recvd_Packet.Length)
                throw new ArgumentException("Invalid OffsetLocal", "offsetLocal");

            if (length <= -1 && offsetLocal + length > str_Recvd_Packet.Length)
                throw new ArgumentException("Invalid Packet Length", "length");

            // Code Section Revised For Optimization
            // Remove Wrapper first and check if its valid  == Remove Wrapper Code For Separate Layer Work
            if (true)
            {
                // Storage for current char
                byte current_char = Recieved_Packet[decode_counter++];

                // scan identifier
                if (current_char == AARE_Identifier)
                {
                    // Verify Length
                    // Code Section Revised For Better Performance
                    Length_APDU = current_char = Recieved_Packet[decode_counter++];
                    if (!(DLMS_Common.Length_Verified(Recieved_Packet, decode_counter, Length_APDU, false)))
                    {
                        // Next_decoder = AARE_Drop_Packet;
                        AARE_Drop_Packet();
                    }
                    else
                    {
                        // Next_decoder = AARE_decode;
                        AARE_decode();
                    }
                }
                // xDLMS Conformed Service Error
                else if (current_char == ConfirmedServiceError_Identifier) ServiceErr_Decode();
                else if (current_char == ExceptionResponse_Identifier)
                {
                    var ExErrorCode = Decode_Exception_Response(Recieved_Packet, decode_counter);

                    if (ExErrorCode != null)
                        DLMSLogger.LogALMessage(String.Format("Error: {0}", ExErrorCode), ALMessageType.FatalError, PacketType.ExcpetionResponse);

                    throw ExErrorCode;

                }
                else if (current_char == Get_resp_Identifier) Get_resp_decode();       // Next_decoder = Get_resp_decode;
                else if (current_char == Set_resp_Identifier) Set_resp_decode();       // Next_decoder = Set_resp_decode;
                else if (current_char == Action_resp_Identifier) Action_resp_decode(); // Next_decoder = Action_resp_decode;
                else if (current_char == Event_Notify_req_Identifier) Event_Notify_decode(); // Next_decoder = Event_Notify_decode;
                else if (current_char == Association_Release_resp_Identifier) Association_Release_decode(); // Next_decoder = Association_Release_decode;
                else No_Case_Drop_Packet();    // Next_decoder = No_Case_Drop_Packet;
            }

            // else
            // {
            //     ///Next_decoder = No_Case_Drop_Packet;
            //     No_Case_Drop_Packet();
            // }
            //  Run decoding function
            // Next_decoder();
        }

        //-------------------Conformed Service Error DECODING ----------------
        //--------------------------------------------------------------------
        //--------------------------------------------------------------------
        // AARE Decoding
        //--------------------------------------------------------------------
        //--------------------------------------------------------------------
        // xDLMS Conformed Service Error {AARE Initiate Request}
        private void ServiceErr_Decode()
        {
            var Err = Decode_Service_Error(Recieved_Packet, decode_counter);
            if (Err != null)
                DLMSLogger.LogALMessage("Error: " + Err.ToString(), ALMessageType.Decode, PacketType.ConfirmedServiceError);

            // Will be done later
            // Check Logic:= If Last Command Sent Is AARQ
            if (APPLICATION_ASSOCIATION_to_AP != null)
            {
                // FIX Err Code For Indication
                Err.Result = AssociationResult.RejectedPermanent;
                APPLICATION_ASSOCIATION_to_AP.Flg_Negotiated_xDLMS_Context = 0x00;
                APPLICATION_ASSOCIATION_to_AP.XDLMS_Initiate_Error = Err;
            }
            AARE_decode_completed();
        }


        /// <summary>
        /// helper decoder to decode --> Exception Response Error
        /// </summary>
        /// <remarks>
        /// mentioned as above in the documentation
        /// to decode Confirmed Service Error LOWEST Packet
        /// </remarks>
        internal DLMSErrCodesException Decode_Exception_Response(byte[] Recieved_Packet, int decode_counter)
        {
            // default values
            byte StateError = 2;
            byte ServiceError = 2;

            DLMSErrCodesException ErrCodeException = null;
            Exception InnerEx = null;

            try
            {
                StateError = Recieved_Packet[decode_counter++];
                ServiceError = Recieved_Packet[decode_counter++];
            }
            catch (Exception ex)
            {
                InnerEx = ex;
            }
            finally
            {
                // Raise Exception Response
                ErrCodeException = new DLMSErrCodesException((ExceptionStateError)StateError,
                                                             (ExceptionServiceError)ServiceError, InnerEx);
            }

            return ErrCodeException;
        }

        //------------------------------------- AARE DECODING --------------->>
        //--------------------------Code modified for optimization----------->>
        //--------------------------------------------------------------------

        //--------------------------------------------------------------------
        // AARE Decoding
        //--------------------------------------------------------------------
        //--------------------------------------------------------------------
        #region AARE Decoding

        /// <summary>
        /// This decoder function performs AARE(REF) response APDU decoding Process;On Error invoke AARE_Drop_Packet helper function
        /// </summary>
        /// <remarks>
        /// The AARE;an application protocol data unit (APDU) which may be sent by the server application layer to
        /// the client application layer proposing the association, as a result of invoking the COSEMOPEN.response service(REF). 
        /// It holds the parameters of the Negotiated_Application_Context(REF) and xDLMS_Context, or in case of failure, diagnostic information.
        /// It is carried by the supporting layer.
        /// </remarks>
        private void AARE_decode()
        {
            //Code modified for optimization
            // Storage for current char
            byte current_char = Recieved_Packet[decode_counter++];

            if (current_char == 0x80)
                AARE_decode1_1();//Next_decoder = AARE_decode1_1;

            else if (current_char == 0xA1)
                AARE_decode1_2();//Next_decoder = AARE_decode1_2;
            else
                AARE_Drop_Packet();//Next_decoder = AARE_Drop_Packet;

            // Execute next function
            //Next_decoder();
        }

        /// <summary>
        /// helper decoder to decode 80
        /// </summary>
        private void AARE_decode1_1()
        {
            //Next_decoder = AARE_decode_completed;
            //Next_decoder();
            AARE_decode_completed();
        }

        /// <summary>
        /// helper decoder to decode (A1) 09 06 07
        /// </summary>
        /// <remarks>
        /// mentioned as above in the documentation
        /// AARE decode 01 (ABOVE) 
        /// </remarks>
        private void AARE_decode1_2()
        {

            byte[] AARE_Temp_Array1 = new byte[] { 9, 6, 7 };
            Buffer.BlockCopy(Recieved_Packet, decode_counter, AARE_Temp_Array1, 0, AARE_Temp_Array1.Length);
            decode_counter += 3;
            // temp arrays for compare function

            byte[] to_compare = new byte[] { 9, 6, 7 };

            // If not equal drop packet else store next 7 bytes
            if (!DLMS_Common.Compare_Array(AARE_Temp_Array1, to_compare))
                AARE_Drop_Packet();  //Next_decoder = AARE_Drop_Packet;
            // Save application context name
            else
            {
                // temp arrays for storing
                byte[] byte_AARE_Temp_Array1 = new byte[7];
                Buffer.BlockCopy(Recieved_Packet, decode_counter, byte_AARE_Temp_Array1, 0, byte_AARE_Temp_Array1.Length);
                decode_counter += 7;

                APPLICATION_ASSOCIATION_to_AP.Application_Context_Name = new stApplication_Context()
                {
                    ctt_element = byte_AARE_Temp_Array1[0],
                    Country_element = byte_AARE_Temp_Array1[1],
                    Country_name_element = byte_AARE_Temp_Array1[2],
                    Organization_element = byte_AARE_Temp_Array1[3],

                    DLMS_UA_element = byte_AARE_Temp_Array1[4],
                    Application_Context_element = byte_AARE_Temp_Array1[5],
                    Context_id_element = byte_AARE_Temp_Array1[6]
                };

                AARE_decode_2();
            }
        }

        /// <summary>
        /// helper decoder to decode A2 03 02 01
        /// </summary>
        /// <remarks>
        /// mentioned as above in the documentation
        /// AARE decode 02 (ABOVE) 
        /// </remarks>
        private void AARE_decode_2()
        {
            // temp arrays for storing
            byte[] byte_AARE_Temp_Array3 = new byte[4];

            Buffer.BlockCopy(Recieved_Packet, decode_counter, byte_AARE_Temp_Array3, 0, byte_AARE_Temp_Array3.Length);
            decode_counter += byte_AARE_Temp_Array3.Length;

            // temp arrays for compare function
            byte[] to_compare = new byte[] { 0xA2, 3, 2, 1 };

            // If not equal drop packet else store next 7 bytes
            if (!DLMS_Common.Compare_Array(byte_AARE_Temp_Array3, to_compare))
                AARE_Drop_Packet();     //Next_decoder = AARE_Drop_Packet;
            else
            {
                /// Store char
                APPLICATION_ASSOCIATION_to_AP.Result = Recieved_Packet[decode_counter++];
                AARE_decode_3();    //Next_decoder = AARE_decode_3;
            }
            // Execute next function
            //Next_decoder();
        }


        /// <summary>
        /// helper decoder to decode A3 05
        /// </summary>
        /// <remarks>
        /// mentioned as above in the documentation
        /// AARE decode 03 (ABOVE) 
        /// </remarks>
        private void AARE_decode_3()
        {
            // temp arrays for storing
            byte[] byte_AARE_Temp_Array1 = new byte[2];
            Buffer.BlockCopy(Recieved_Packet, decode_counter, byte_AARE_Temp_Array1, 0, byte_AARE_Temp_Array1.Length);
            decode_counter += byte_AARE_Temp_Array1.Length;


            // temp arrays for compare function
            byte[] to_compare = new byte[] { 0xA3, 5 };

            // If not equal drop packet else store next 7 bytes
            if (!DLMS_Common.Compare_Array(byte_AARE_Temp_Array1, to_compare))
                AARE_Drop_Packet();
            else
                AARE_decode_4();

        }

        /// <summary>
        /// helper decoder function to decode Local_Or_Remote flag
        /// </summary>
        /// <remarks>
        /// mentioned as above in the documentation
        /// AARE decode 4 (ABOVE) 
        /// </remarks>
        private void AARE_decode_4()
        {
            byte temp_var = Recieved_Packet[decode_counter++];

            if (temp_var == 0xA1 || temp_var == 0xA2)
                AARE_decode_5();
            else
                AARE_Drop_Packet();

            /// Save the char 
            /// Associate-source-diagnostic
            /// 0xA1 For acse-service-user
            /// 0xA2 For acse-service-provider
            APPLICATION_ASSOCIATION_to_AP.Local_Or_Remote = temp_var;
        }

        /// <summary>
        /// helper decoder to decode 03 02 01 Associate-Source-Diagnostic
        /// </summary>
        /// <remarks>
        /// mentioned as above in the documentation
        /// AARE decode 5 (ABOVE) 
        /// </remarks>
        private void AARE_decode_5()
        {
            // temp arrays for storing
            byte[] byte_AARE_Temp_Array3 = new byte[3];
            Buffer.BlockCopy(Recieved_Packet, decode_counter, byte_AARE_Temp_Array3, 0, byte_AARE_Temp_Array3.Length);
            decode_counter += byte_AARE_Temp_Array3.Length;

            // temp arrays for compare function
            byte[] to_compare = new byte[] { 3, 2, 1 };

            // If not equal drop packet else store next 7 bytes
            if (!DLMS_Common.Compare_Array(byte_AARE_Temp_Array3, to_compare))
                AARE_Drop_Packet();
            else
            {
                // save the character
                APPLICATION_ASSOCIATION_to_AP.Failure_Type = (Result_SourceDiagnostic)Recieved_Packet[decode_counter++];
                AARE_decode_above();
            }
        }

        /// <summary>
        /// helper decoder to decode AARE
        /// </summary>
        /// <remarks>
        /// mentioned as above in the documentation
        /// AARE decode 6 (ABOVE) 
        /// </remarks>
        private void AARE_decode_above()
        {
            // Storage for current char
            byte current_char = Recieved_Packet[decode_counter++];

            // Take decision according to current char
            if (current_char == 0xA4)
                AARE_decode_Responding_AP_Title();
            else if (current_char == 0xA5)
                AARE_Do_Nothing();
            else if (current_char == 0xA6)
                AARE_Do_Nothing();
            else if (current_char == 0xA7)
                AARE_Do_Nothing();
            else if (current_char == 0x88)
                AARE_decode_LOW();
            else if (current_char == 0xBE)
                AARE_decode_LOWEST();
            else
                AARE_Drop_Packet();
        }

        //--------------------------------------------------------------------
        //--------------------------------------------------------------------
        // AARE decode Responding AP Title
        //--------------------------------------------------------------------
        //--------------------------------------------------------------------
        // to decode Responding AP Title
        private void AARE_decode_Responding_AP_Title()
        {
            // Store as size
            byte AARE_Responding_AP_Title_length = Recieved_Packet[decode_counter++];

            // check if length is not verified
            if (!(DLMS_Common.Length_Verified(Recieved_Packet, decode_counter, AARE_Responding_AP_Title_length, false)))
                AARE_Drop_Packet(); /// Next_decoder = AARE_Drop_Packet;
            // if verified, match next character (0x04)
            else
            {
                if (Recieved_Packet[decode_counter++] == 0x04)
                {
                    AARE_Responding_AP_Title_length = Recieved_Packet[decode_counter++];

                    byte[] Responding_AP_title = new byte[AARE_Responding_AP_Title_length];
                    Buffer.BlockCopy(Recieved_Packet, decode_counter, Responding_AP_title, 0, AARE_Responding_AP_Title_length);

                    APPLICATION_ASSOCIATION_to_AP.Responding_AP_Title = new stAppTitle()
                    {
                        AP_Title = Responding_AP_title,
                        UserId = 0
                    };
                    if (Security_Data != null)
                        _security_Data.ServerSystemTitle = new List<byte>(Responding_AP_title);
                    decode_counter += AARE_Responding_AP_Title_length;
                }
                else
                    AARE_Drop_Packet();   /// Next_decoder = AARE_Drop_Packet;
            }
            // Execute next decode block
            AARE_decode_above();
        }


        /// <summary>
        /// helper decoder to decode AARE decode LOW packet
        /// </summary>
        /// <remarks>
        /// mentioned as above in the documentation
        /// to decode LOW section of AARE
        /// </remarks>
        private void AARE_decode_LOW()
        {
            // temp arrays for storing
            byte[] byte_AARE_Temp_Array7 = new byte[3];
            Buffer.BlockCopy(Recieved_Packet, decode_counter, byte_AARE_Temp_Array7, 0, byte_AARE_Temp_Array7.Length);
            decode_counter += byte_AARE_Temp_Array7.Length;


            // bit_string length for incrementing decode counter
            byte temp_bString_len = byte_AARE_Temp_Array7[0];

            // temp arrays for compare function
            byte[] to_compare = new byte[] { 0x02, 0x07, 0x80 };
            byte temp_ACSE_Req;

            // If not equal send 0 else send 1
            if (!DLMS_Common.Compare_Array(byte_AARE_Temp_Array7, to_compare)) temp_ACSE_Req = 0;
            else temp_ACSE_Req = 1;

            // store in structure
            APPLICATION_ASSOCIATION_to_AP.ACSE_Requirements = temp_ACSE_Req;

            #region // increment decoder counter in case length is greater than 2

            //for (byte inc = 0; inc < (temp_bString_len - 2); inc++)  decode_counter++;
            decode_counter += ((temp_bString_len - 2) > 0) ? (temp_bString_len - 2) : 0;

            #endregion

            // temp arrays for storing
            byte[] byte_AARE_Temp_ = new byte[2];
            Buffer.BlockCopy(Recieved_Packet, decode_counter, byte_AARE_Temp_, 0, byte_AARE_Temp_.Length);
            decode_counter += byte_AARE_Temp_.Length;


            // temp arrays for compare function
            byte[] to_compare_2 = new byte[] { 0x89, 0x07 };

            // If not equal drop packet 
            if (!DLMS_Common.Compare_Array(byte_AARE_Temp_, to_compare_2))
                AARE_Drop_Packet();
            else
            {
                // temp arrays for storing
                byte[] temp_SMN = new byte[7];

                // store Security mechanism name into temp array
                //for (byte i = 0; i < 7; i++) temp_SMN[i] = Recieved_Packet[decode_counter++];
                Buffer.BlockCopy(Recieved_Packet, decode_counter, temp_SMN, 0, temp_SMN.Length);
                decode_counter += temp_SMN.Length;

                // store to the structure
                APPLICATION_ASSOCIATION_to_AP.Security_Mechanism_Name = new stAuthentication_MechanismName()
                {
                    ctt_element = temp_SMN[0],
                    Country_element = temp_SMN[1],
                    Country_name_element = temp_SMN[2],
                    Organization_element = temp_SMN[3],

                    DLMS_UA_element = temp_SMN[4],
                    Authen_mechanism_name_element = temp_SMN[5],
                    mechanism_id_element = temp_SMN[6]
                };

                // check for low encoding
                byte temp_low_check = Recieved_Packet[decode_counter];

                if (temp_low_check == 0xAA)
                    AARE_decode_LOW_AA();
                else if (temp_low_check == 0xBE)
                {
                    decode_counter++;
                    AARE_decode_LOWEST();
                }
                else
                    AARE_Drop_Packet();
            }
            // execute next function
            // Next_decoder();
        }

        /// <summary>
        /// helper decoder to decode AARE decode LOW_AA packet
        /// </summary>
        /// <remarks>
        /// mentioned as above in the documentation
        /// to decode LOW_AA section of AARE
        /// </remarks>
        private void AARE_decode_LOW_AA()
        {
            /// temp arrays for storing
            byte temp = Recieved_Packet[decode_counter++];

            if (temp != 0xAA)
                AARE_Drop_Packet();
            else
                AARE_decode_LOW_Auth_val();

        }

        /// <summary>
        /// helper decoder to decode AARE decode LOW_Auth_val packet
        /// </summary>
        /// <remarks>
        /// mentioned as above in the documentation
        /// to store Authentication value in LOW_Auth_val section of AARE
        /// </remarks>
        private void AARE_decode_LOW_Auth_val()
        {
            // size to know number of next characters
            byte size = Recieved_Packet[decode_counter++];

            #region // temporary storage for authentication value

            byte[] temp_auth_val = new byte[size];
            // for (byte i = 0; i < size; i++) temp_auth_val[i] = Recieved_Packet[decode_counter++];
            Buffer.BlockCopy(Recieved_Packet, decode_counter, temp_auth_val, 0, temp_auth_val.Length);
            decode_counter += temp_auth_val.Length;

            #endregion

            byte next_type = temp_auth_val[0];

            if (next_type == 0x82)
                AARE_Do_Nothing();      //Next_decoder = AARE_Do_Nothing;
            else if (next_type == 0x81)
                AARE_decode_LOW_BS_1(); //Next_decoder = AARE_decode_LOW_BS_1;
            else if (next_type == 0x80)
            {
                // check for length of string as well and take decision according to that
                #region // Store to the Structure

                APPLICATION_ASSOCIATION_to_AP.Calling_Authentication_Value = new byte[size - 2];
                Buffer.BlockCopy(temp_auth_val, 2, APPLICATION_ASSOCIATION_to_AP.Calling_Authentication_Value, 0, size - 2);

                #endregion

                // check for low encoding
                byte temp_low_check = Recieved_Packet[decode_counter++];

                if (temp_low_check == 0xBE)
                    AARE_decode_LOWEST();
                else
                    AARE_Drop_Packet();
            }
            else
                AARE_Drop_Packet();
        }

        /// <summary>
        /// helper decoder to decode AARE decode Bit string 1 LOW packet
        /// </summary>
        /// <remarks>
        /// mentioned as above in the documentation
        /// to decode + store Bit string 1 in LOW section of AARE
        /// </remarks>
        private void AARE_decode_LOW_BS_1()
        {
            #region Debugger & Logger
#if Enable_Log_Message

            DLMSLogger.LogALMessage("Bit String not decoded:AARE_decode_LOW_BS_1", ALMessageType.Decode, PacketType.ARE);

#endif
            #endregion
            AARE_Drop_Packet();
        }

        ////--------------------------------------------------------------------
        ////--------------------------------------------------------------------
        //// AARE decode Graphic string LOW packet
        ////--------------------------------------------------------------------
        ////--------------------------------------------------------------------
        //// to decode + store Graphic string 1 in LOW section of AARE
        // private void AARE_decode_LOW_GS_1()
        // {
        //     // local storage for length
        //     byte GS_len = Recieved_Packet[decode_counter++];

        //     // temp array for auth_val

        //     for(byte cnt=0; cnt<GS_len; cnt++)

        //     // execute next function
        //     Next_decoder();
        // }

        //--------------------------------------------------------------------
        //--------------------------------------------------------------------
        // AARE decode LOWEST packet
        //--------------------------------------------------------------------
        //--------------------------------------------------------------------

        /// <summary>
        /// helper decoder to  AARE decode LOWEST packet
        /// </summary>
        /// <remarks>
        /// mentioned as above in the documentation
        /// to decode LOWEST Packer
        /// </remarks>
        private void AARE_decode_LOWEST()
        {
            // Storage for current char
            byte current_char = Recieved_Packet[decode_counter++];

            // store as size
            byte AARE_LOW_user_info_length = current_char;

            // Check if length is not verified
            if (!(DLMS_Common.Length_Verified(Recieved_Packet, decode_counter, AARE_LOW_user_info_length, false)))
                AARE_Drop_Packet();
            // if verified, match next character (0x04)
            else
            {
                if (Recieved_Packet[decode_counter++] == 0x04)
                    AARE_decode_LOWEST_1();
                else
                    AARE_Drop_Packet();
            }
        }

        /// <summary>
        /// helper decoder to  AARE decode LOWEST_1 packet
        /// </summary>
        /// <remarks>
        /// mentioned as above in the documentation
        /// to decode LOWEST_1 Packer
        /// </remarks>
        private void AARE_decode_LOWEST_1()
        {
            // Storage for current char
            byte current_char = Recieved_Packet[decode_counter++];

            // store as size
            byte AARE_LOW_1_size = current_char;

            // verify length
            if (!(DLMS_Common.Length_Verified(Recieved_Packet, decode_counter, AARE_LOW_1_size, false)))
                AARE_Drop_Packet();
            // if verified, match next character
            else
            {
                current_char = Recieved_Packet[decode_counter++];
                if (Security_Context.IsSecurityApplicable(current_char))
                {
                    bool isTagMatched = false;
                    ArraySegment<byte> decrypted = Security_Context.Process_HLS_APDU(Security_Data, _crypto,
                        Recieved_Packet.GetSegment<byte>(decode_counter - 1), out isTagMatched, false);

                    Array.Copy(decrypted.Array, 0, Recieved_Packet, decode_counter - 1, decrypted.Array.Length);
                    current_char = Recieved_Packet[decode_counter - 1];
                }

                if (current_char == 0x08)
                {
                    // Set flag of Negotiated_xDLMS_Context
                    APPLICATION_ASSOCIATION_to_AP.Flg_Negotiated_xDLMS_Context = 0x01;
                    AARE_decode_LOWEST_init_resp();
                }
                else if (current_char == 0x0E)
                {
                    // Set flag of Negotiated_xDLMS_Context
                    APPLICATION_ASSOCIATION_to_AP.Flg_Negotiated_xDLMS_Context = 0x00;
                    AARE_decode_LOWEST_service_error();
                }
                else
                    AARE_Drop_Packet();
            }
        }

        /// <summary>
        /// helper decoder to  decode AARE LOWEST --> Initiate response
        /// </summary>
        /// <remarks>
        /// mentioned as above in the documentation
        ///  to decode initial response in AARE LOWEST
        /// </remarks>
        private void AARE_decode_LOWEST_init_resp()
        {
            // Storage for current char
            byte current_char = Recieved_Packet[decode_counter++];

            // Set flag of Negotiated_xDLMS_Context
            APPLICATION_ASSOCIATION_to_AP.Flg_Negotiated_xDLMS_Context = 0x01;

            // store as size
            byte AARE_QoS = current_char;

            APPLICATION_ASSOCIATION_to_AP.Negotiated_xDLMS_Context.Negotiated_DLMS_Version_Number
                = Recieved_Packet[decode_counter++];

            #region // temp arrays for storing

            byte[] byte_AARE_Temp_Array4 = new byte[4];
            Buffer.BlockCopy(Recieved_Packet, decode_counter, byte_AARE_Temp_Array4, 0, byte_AARE_Temp_Array4.Length);
            decode_counter += byte_AARE_Temp_Array4.Length;

            #endregion

            // temp arrays for compare function
            byte[] to_compare = new byte[] { 0x5F, 0x1F, 0x04, 0x00 };

            // If not equal drop packet else store next 3 bytes
            if (!DLMS_Common.Compare_Array(byte_AARE_Temp_Array4, to_compare))
                AARE_Drop_Packet();
            else
            {
                #region // save as Negotiated_DLMS_Conformance

                byte[] temp_conformance = new byte[3];
                Buffer.BlockCopy(Recieved_Packet, decode_counter, temp_conformance, 0, temp_conformance.Length);
                decode_counter += temp_conformance.Length;
                APPLICATION_ASSOCIATION_to_AP.Negotiated_xDLMS_Context.
                    Negotiated_DLMS_Conformance = temp_conformance;

                #endregion

                UInt16 temp_PDU_size = (UInt16)(Recieved_Packet[decode_counter++] << 8);
                temp_PDU_size |= Recieved_Packet[decode_counter++];
                APPLICATION_ASSOCIATION_to_AP.Negotiated_xDLMS_Context.Server_Max_Receive_PDU_Size = temp_PDU_size;

                byte[] final_check = new byte[2];
                Buffer.BlockCopy(Recieved_Packet, decode_counter, final_check, 0, final_check.Length);
                decode_counter += final_check.Length;

                byte[] final_compare = new byte[] { 0x00, 0x07 };
                if (!DLMS_Common.Compare_Array(final_check, final_compare))
                    AARE_Drop_Packet();
                else
                    AARE_decode_completed();
            }
        }

        /// <summary>
        /// helper decoder to  decode AARE LOWEST --> Confirmed Service Error
        /// </summary>
        /// <remarks>
        /// mentioned as above in the documentation
        ///  to decode Confirmed Service Error LOWEST Packet
        /// </remarks>
        internal void AARE_decode_LOWEST_service_error()
        {
            // Reset flag of Negotiated_xDLMS_Context
            APPLICATION_ASSOCIATION_to_AP.Flg_Negotiated_xDLMS_Context = 0x00;

            byte current_byte = Recieved_Packet[decode_counter++];
            // ConfirmedServiceError
            if (current_byte != 0x01) AARE_Drop_Packet();

            // ServiceError
            current_byte = Recieved_Packet[decode_counter++];
            if (!(current_byte == 0x00 || current_byte == 0x06))
                AARE_Drop_Packet();
            else
            {
                // Storage for current char
                current_byte = Recieved_Packet[decode_counter++];

                // DLMSErrCodesException As XDLMS_Initiate_Error
                APPLICATION_ASSOCIATION_to_AP.XDLMS_Initiate_Error = new DLMSErrCodesException(ConfirmedServiceError.InitiateError, ServiceError.Initiate, current_byte);

                AARE_decode_completed();
            }
        }


        /// <summary>
        /// helper decoder to  decode --> Confirmed Service Error
        /// </summary>
        /// <remarks>
        /// mentioned as above in the documentation
        ///  to decode Confirmed Service Error LOWEST Packet
        /// </remarks>
        internal DLMSErrCodesException Decode_Service_Error(byte[] Recieved_Packet, int decode_counter)
        {
            // default values
            byte Conformed_ServiceErr = 1;
            byte ServiceErr = 6;
            byte XDLMS_Initiate_Error = 1;

            DLMSErrCodesException ErrCodeException = null;
            Exception InnerEx = null;

            try
            {
                Conformed_ServiceErr = Recieved_Packet[decode_counter++];
                ServiceErr = Recieved_Packet[decode_counter++];
                XDLMS_Initiate_Error = Recieved_Packet[decode_counter++];
            }
            catch (Exception ex)
            {
                InnerEx = ex;
            }
            finally
            {
                ErrCodeException = new DLMSErrCodesException((ConfirmedServiceError)Conformed_ServiceErr,
                                                             (ServiceError)ServiceErr, XDLMS_Initiate_Error, InnerEx);
            }

            return ErrCodeException;
        }


        //--------------------------------------------------------------------
        //--------------------------------------------------------------------
        // AARE decode Completed :D
        //--------------------------------------------------------------------
        //--------------------------------------------------------------------

        /// <summary>
        /// The helper decoder function to Invoke <see cref="AARE_Packet_Decoded"/> Event
        /// </summary>
        /// <remarks>
        /// The helper decoder function to Invoke <see cref="AARE_Packet_Decoded"/> or <see cref="PacketDrop"/> Event to notify
        ///  the AL(REF) Application Layer Client <see cref="DLMS_Application_Process"/> module
        /// </remarks>
        private void AARE_decode_completed()
        {

            decode_counter = 0;
            if (flg_Packet_Dropped == 1)
            {
                #region Debugger & Logger
                //***Debugger & Logger
#if Enable_Log_Message
                DLMSLogger.LogALMessage("Packet is dropped:AARE_decode_completed", ALMessageType.Decode, PacketType.ARE);
#endif
                #endregion
                flg_Packet_Dropped = 0;
                if (_PacketDrop != null)
                    _PacketDrop.Invoke(PacketType.ARE, "Packet is dropped:AARE_decode_completed");
            }
            else
            {
                #region Debugger & Logger

                //***  Debugger & Logger
#if Enable_Log_Message
                DLMSLogger.LogALMessage("Packet decoded. Method: AARE_decode_completed", ALMessageType.Decode, PacketType.ARE);
#endif
                #endregion
                // make application association to AP structure ready and raise event
                // Raise Packet decode event
                if (_AARE_Packet_Decoded != null)
                    _AARE_Packet_Decoded(APPLICATION_ASSOCIATION_to_AP);
            }
        }

        /// <summary>
        /// The AARE Packet drop routine;On Error Condition Set flg_Packet_Dropped
        /// </summary>
        private void AARE_Drop_Packet()
        {
            flg_Packet_Dropped = 1;
            AARE_decode_completed();
        }

        /// <summary>
        ///AARE Do Nothing 
        /// </summary>
        private void AARE_Do_Nothing()
        {
            #region Debugger & Logger

#if Enable_Log_Message

            DLMSLogger.LogALMessage("Nothing Done_AARE Do Nothing", ALMessageType.Decode, PacketType.ARE);

#endif

            #endregion
        }

        #endregion

        //------------------------------- ASSOCIATION RELEASE DECODING ------->>
        //-------------------------------Code Revised & Modified for Optimized>>
        #region Association Release Decoding

        /// <summary>
        /// Association Release Response Decoding
        /// </summary>
        private void Association_Release_decode()
        {
            // Storage for current char
            // byte current_char = Recieved_Packet[decode_counter];
            // byte current_char;

            int APDU_len = BasicEncodeDecode.Decode_Length(Recieved_Packet, ref decode_counter);
            /// wrong length
            if (!DLMS_Common.Length_Verified(Recieved_Packet, decode_counter, APDU_len, false))
            {
                Association_Release_Drop_Packet();              //Next_decoder = Association_Release_Drop_Packet;
            }
            /// if packet ends, current char should be zero
            else if (APDU_len == 0)
            {
                Association_Release_decode_completed();         //Next_decoder = Association_Release_decode_completed;
            }
            /// packet hasn't ended yet
            else
            {
                /// decode_counter++;
                Association_Release_Optional_Field_decode();    //Next_decoder = Association_Release_Optional_Field_decode;
            }
            /// Execute next function
            /// Next_decoder();
        }

        ///<summary>
        /// Association Release Optional Field Decoding,to decode 80 or BE
        ///</summary>
        private void Association_Release_Optional_Field_decode()
        {
            /// Storage for current char
            byte current_char = Recieved_Packet[decode_counter++];
            byte Field = current_char;

            /// with reason
            if (Field == 0x80)
            {
                current_char = Recieved_Packet[decode_counter++];
                /// next char should be 0x01
                if (current_char != 0x01)
                    Association_Release_Drop_Packet(); //Next_decoder = Association_Release_Drop_Packet;
                else
                {
                    /// save reason
                    current_char = Recieved_Packet[decode_counter++];
                    ASSOCIATION_RELEASE_to_AP.Reason = current_char;
                    Association_Release_decode_completed();     //Next_decoder = Association_Release_decode_completed;
                }
            }
            else if (Field == 0xBE)
            {
                /// scan length
                /// current_char = Recieved_Packet[decode_counter++];
                int temp_length = BasicEncodeDecode.Decode_Length(Recieved_Packet, ref decode_counter);
                /// length is correct

                if (DLMS_Common.Length_Verified(Recieved_Packet, decode_counter, temp_length, false))
                {
                    /// save nothing just indicate to AP that Association has been released
                    /// Next_decoder = Association_Release_decode_completed;
                    Association_Release_decode_completed();
                }
                else
                {
                    /// wrong length , drop packet
                    /// Next_decoder = Association_Release_Drop_Packet;
                    Association_Release_Drop_Packet();
                }
            }
            /// optional field isn't either of 0x80 and 0xBE
            else
            {
                /// Next_decoder = Association_Release_Drop_Packet;
                Association_Release_Drop_Packet();
            }
            /// Execute next function
            /// Next_decoder();
        }

        /// <summary>
        /// The Association Release decode Completed function to Invoke <see cref="Association_Release_Packet_Decoded"/> 
        /// or <see cref="PacketDrop"/> Event
        /// </summary>
        /// <remarks>
        /// The helper decoder function to Invoke<see cref="Association_Release_Packet_Decoded"/> 
        /// or <see cref="PacketDrop"/> Event to notify the AL(REF) Application_Layer_Client <see cref="DLMS_Application_Process"/> module
        /// </remarks>
        private void Association_Release_decode_completed()
        {
            decode_counter = 0;
            if (flg_Packet_Dropped == 1)
            {
                #region Debugger & Logger
#if Enable_Log_Message
                //**Debugger & Logger
                DLMSLogger.LogALMessage("Packet is dropped:Association_Release_decode_completed", ALMessageType.Decode, PacketType.RLRQ);
#endif
                #endregion
                flg_Packet_Dropped = 0;
                if (_PacketDrop != null)
                    _PacketDrop.Invoke(PacketType.RLRQ, "Packet is dropped:Association_Release_decode_completed");
            }
            else
            {
                // make "Association Release to AP" structure ready and raise event
                #region Debugger & Logger
#if Enable_Log_Message
                DLMSLogger.LogALMessage("Packet decoded:Association_Release_decode_completed", ALMessageType.Decode, PacketType.RLRQ);
#endif
                #endregion
                if (_Association_Release_Packet_Decoded != null)
                    _Association_Release_Packet_Decoded(ASSOCIATION_RELEASE_to_AP);
            }
        }

        /// <summary>
        /// Association Release Packet drop routine
        /// </summary>
        private void Association_Release_Drop_Packet()
        {
            flg_Packet_Dropped = 1;
            Association_Release_decode_completed();
        }

        /// <summary>
        /// Association Release Do Nothing
        /// </summary>
        private void Association_Release_Do_Nothing()
        {
            #region Debugger & Logger
#if Enable_Log_Message
            DLMSLogger.LogALMessage("Packet decoded:Association_Release_Do_Nothing", ALMessageType.Decode, PacketType.RLRQ);
#endif
            #endregion

        }

        #endregion


        //------------------------------------- GET DECODING ------------------>>
        //----------------------Get Decoding Functions Modified For Optimization

        #region Get Decoding Functions

        /// <summary>
        /// This decoder function decodes GET Service Response;On Error Invoke Get_Drop_Packet
        /// </summary>
        /// <remarks>
        /// the GET service, used to read the value of one or more attributes of COSEM interface objects
        /// </remarks>
        private void Get_resp_decode()
        {
            // Storage for current char
            byte current_char = Recieved_Packet[decode_counter++];
            // save as request type
            GET_to_AP.Response_Type = current_char;

            if (current_char == 0x01)
                Get_resp_NORMAL_decode();  //Next_decoder = Get_resp_NORMAL_decode;
            else if (current_char == 0x02)
                Get_resp_DATA_BLOCK_decode();  //Next_decoder = Get_resp_DATA_BLOCK_decode;
            else if (current_char == 0x03)
                Get_resp_WITH_LIST_decode();  //Next_decoder = Get_resp_WITH_LIST_decode;
            else
                Get_Drop_Packet(); //Next_decoder = Get_Drop_Packet;
            // Execute next function
            //Next_decoder();
        }

        /// <summary>
        /// Get Response NORMAL Decoding
        /// </summary>
        private void Get_resp_NORMAL_decode()
        {
            // Storage for current char
            byte current_char = Recieved_Packet[decode_counter++];

            // store in structure
            GET_to_AP.Invoke_Id_Priority = current_char;

            current_char = Recieved_Packet[decode_counter++];

            // store in structure
            GET_to_AP.Get_Data_Result = current_char;

            if (current_char == 0x00)
                Get_decode_save_data();   //Next_decoder = Get_decode_save_data;
            else if (current_char == 0x01)
            {
                current_char = Recieved_Packet[decode_counter++];

                if (DLMS_Common.Length_Verified(Recieved_Packet, decode_counter, 0, false))
                {

                    //store in structure
                    GET_to_AP.Data_Access_Result = current_char;

                    // Finished decoding
                    //Next_decoder = Get_decode_completed;
                    Get_decode_completed();
                }
                else
                    Get_Drop_Packet();  //Next_decoder = Get_Drop_Packet;
            }
            // Invalid character
            else
                Get_Drop_Packet();      //Next_decoder = Get_Drop_Packet;

            // Execute next function
            //Next_decoder();
        }

        /// <summary>
        /// Get Response NORMAL Decode and save "DATA"
        /// </summary>
        private void Get_decode_save_data()
        {
            DLMS_Common.Byte_Array_Copier(Recieved_Packet, ref GET_to_AP.Data, decode_counter);
            Get_decode_completed();
            // Finished decoding
            //Next_decoder = Get_decode_completed;

            //Execute next function
            //Next_decoder();
        }

        /// <summary>
        /// Get Response WITH Data Block Decoding
        /// </summary>
        private void Get_resp_DATA_BLOCK_decode()
        {
            // Storage for current char
            byte current_char = Recieved_Packet[decode_counter++];

            // store in sructure
            GET_to_AP.Invoke_Id_Priority = current_char;

            // store next char as last block
            current_char = Recieved_Packet[decode_counter++];
            GET_to_AP.DataBlock_G.Last_Block = current_char;

            // store next 4 chars as ulong
            UInt32 temp_data_block_no = (UInt32)(Recieved_Packet[decode_counter++] << 24) +
                (UInt32)(Recieved_Packet[decode_counter++] << 16) +
                (UInt32)(Recieved_Packet[decode_counter++] << 8) + (UInt32)(Recieved_Packet[decode_counter++]);
            GET_to_AP.Block_Number = temp_data_block_no;

            // scan for 00 or 01    
            current_char = Recieved_Packet[decode_counter++];
            // store in structure
            GET_to_AP.Get_Data_Result = current_char;

            if (current_char == 0x00)
                Get_decode_save_Raw_data(); //Next_decoder = Get_decode_save_Raw_data;
            else if (current_char == 0x01)
            {
                current_char = Recieved_Packet[decode_counter++];
                if (DLMS_Common.Length_Verified(Recieved_Packet, decode_counter, 0, false))
                {

                    //store in structure
                    GET_to_AP.Data_Access_Result = current_char;
                    // Finished decoding
                    //Next_decoder = Get_decode_completed;
                    Get_decode_completed();
                }
                else
                    Get_Drop_Packet();          // Next_decoder = Get_Drop_Packet;
            }
            // Invalid character
            else
                Get_Drop_Packet();             // Next_decoder = Get_Drop_Packet;

            // Execute next function
            //Next_decoder();
        }

        /// <summary>
        /// Get Response with block Decode and save RAW_DATA
        /// </summary>
        private void Get_decode_save_Raw_data()
        {
            // storage for length of octet string
            // byte temp_len = Recieved_Packet[decode_counter++];
            int temp_len = BasicEncodeDecode.Decode_Length(Recieved_Packet, ref decode_counter);
            int old_length; // of raw data array
            if (GET_to_AP.DataBlock_G.Raw_Data == null)
            {
                old_length = 0;
                GET_to_AP.DataBlock_G.Raw_Data = new List<byte>(05);
            }
            else
            {
                old_length = GET_to_AP.DataBlock_G.Raw_Data.Count;
            }

            var TArray = new byte[temp_len];
            // Array.Resize<byte>(ref GET_to_AP.DataBlock_G.Raw_Data, old_length + temp_len);

            if (DLMS_Common.Length_Verified(Recieved_Packet, decode_counter, temp_len, false))
            {
                // Common.Byte_Array_Copier(Recieved_Packet, ref GET_to_AP.DataBlock_G.Raw_Data, decode_counter);
                Array.Copy(Recieved_Packet, decode_counter, TArray, 0, temp_len);
                if (temp_len > 0)
                {
                    GET_to_AP.DataBlock_G.Raw_Data.AddRange(TArray);
                }
                else
                    Get_Drop_Packet();

                // Finished decoding
                // Next_decoder = Get_decode_completed;
                Get_decode_completed();
            }
            else
                Get_Drop_Packet();  // Next_decoder = Get_Drop_Packet;
            // Execute next function
            // Next_decoder();
        }

        /// <summary>
        /// Get Response WITH LIST Decoding
        /// </summary>
        private void Get_resp_WITH_LIST_decode()
        {
            //TODO:Implement Get_resp_With_List Logic here
        }

        //--------------------------------------------------------------------
        //--------------------------------------------------------------------
        // Get decode Completed :D
        //--------------------------------------------------------------------
        //--------------------------------------------------------------------

        /// <summary>
        /// The Get_decode_completed decoder function to Invoke <see cref="GET_Packet_Decoded"/> Event;
        /// On flg_Packet_Dropped Set Condition to Invoke <see cref="PacketDrop"/> Event;
        /// </summary>
        /// <remarks>
        /// The helper decoder function to Invoke <see cref="GET_Packet_Decoded"/> Event or <see cref="PacketDrop"/> Event to notify
        ///  AL(REF) client <see cref="DLMS_Application_Process"/> module
        /// </remarks>
        private void Get_decode_completed()
        {
            try
            {
                decode_counter = 0;
                if (flg_Packet_Dropped == 1)
                {
                    #region Debugger & Logger
#if Enable_Log_Message
                    //**Debugger & Logger
                    DLMSLogger.LogALMessage("Packet is dropped:Get_decode_completed", ALMessageType.Decode, PacketType.GET); //** Debugger & Logger

#endif
                    #endregion
                    flg_Packet_Dropped = 0;
                    // final
                    GET_to_AP.DataBlock_G.Raw_Data = new List<byte>(05);
                    GET_to_AP.DataBlock_G.Raw_Data = null;
                    if (_PacketDrop != null)
                        _PacketDrop.Invoke(PacketType.GET, "Packet is dropped:Get_decode_completed");
                }
                else
                {
                    // make Get_to_AP structure ready and raise event
                    #region Debugger & Logger
#if Enable_Log_Message
                    //**Debugger & Logger
                    DLMSLogger.LogALMessage("Packet decoded:Get_decode_completed", ALMessageType.Decode, PacketType.GET);
#endif
                    #endregion
                    // Raise Packet decode event
                    if (_GET_Packet_Decoded != null)
                        _GET_Packet_Decoded(GET_to_AP);
                    //GET Command Response Complete//Reset GET Structure
                }
            }
            finally
            {
                if (GET_to_AP.Response_Type == (byte)GET_Method.With_Block
                    && GET_to_AP.DataBlock_G.Last_Block == 0x01)
                {
                    GET_to_AP = new stGET();
                }
                else if (GET_to_AP.Response_Type == (byte)GET_Method.Normal)
                {
                    GET_to_AP = new stGET();
                }
            }
        }

        /// <summary>
        /// Get Packet drop routine;On Error Condition Set flg_Packet_Dropped = 1
        /// </summary>
        private void Get_Drop_Packet()
        {
            flg_Packet_Dropped = 1;
            Get_decode_completed();
        }

        /// <summary>
        /// Get Do Nothing
        /// </summary>
        private void Get_Do_Nothing()
        {
        }

        #endregion

        //------------------------------------- SET DECODING ----------------------------->>
        //----------------------Set Functions Modified for Optimization------------------->>

        #region Set Decoding Functions

        /// <summary>
        /// This Set_resp_decode function decodes SET Service Response;On Error Invoke Set_Drop_Packet
        /// </summary>
        /// <remarks>
        /// The SET service, used to write the value of one or more attributes of COSEM interface objects
        /// </remarks>
        private void Set_resp_decode()
        {
            // Storage for current char
            byte current_char = Recieved_Packet[decode_counter++];

            if (current_char == (byte)SET_Response_Type.NORMAL)                   //Response Type --Normal/Block
            {
                SET_to_AP.Response_Type = current_char;
                Set_resp_NORMAL_decode();       //Next_decoder = Set_resp_NORMAL_decode;
            }
            else if (current_char == (byte)SET_Response_Type.Non_Last_Data_BLOCK ||
                current_char == (byte)SET_Response_Type.Last_Data_Block)
            {
                SET_to_AP.Response_Type = current_char;
                Set_resp_WithBlock_decode();    //Next_decoder = Set_resp_WithBlock_decode;

            }
            else
                Set_Drop_Packet();              //Next_decoder = Set_Drop_Packet;
            // Execute next function
            //Next_decoder();
        }

        /// <summary>
        /// Set Response NORMAL Decoding
        /// </summary>
        private void Set_resp_NORMAL_decode()
        {
            // Storage for current char
            byte current_char = Recieved_Packet[decode_counter++];

            // store in structure
            SET_to_AP.Invoke_Id_Priority = current_char;

            // store next char as result
            current_char = Recieved_Packet[decode_counter++];

            if (DLMS_Common.Length_Verified(Recieved_Packet, decode_counter, 0, false))
            {
                SET_to_AP.Result = current_char;
                Set_decode_completed();         //Next_decoder = Set_decode_completed;
            }
            else
            {
                Set_Drop_Packet();              //Next_decoder = Set_Drop_Packet;
            }
            // Execute next function
            //Next_decoder();
        }

        /// <summary>
        /// Set Response Blocking Decoding
        /// </summary>
        private void Set_resp_WithBlock_decode()
        {
            // Storage for current char
            byte current_char = Recieved_Packet[decode_counter++];
            // store Invoke Priority ID in structure
            SET_to_AP.Invoke_Id_Priority = current_char;

            // Decode Response Result For Last DataBlock
            if (SET_to_AP.Response_Type == (byte)SET_Response_Type.Last_Data_Block)   // Decode Response Result
            {
                SET_to_AP.Result = Recieved_Packet[decode_counter++];
            }

            // store next 4 chars as ulong
            // Decoding Block Number 
            UInt32 temp_data_block_no = (UInt32)(Recieved_Packet[decode_counter++] << 24) +
                                        (UInt32)(Recieved_Packet[decode_counter++] << 16) +
                                        (UInt32)(Recieved_Packet[decode_counter++] << 8) +
                                        (UInt32)(Recieved_Packet[decode_counter++]);

            SET_to_AP.Block_number = temp_data_block_no;

            Set_decode_completed();
            // Next_decoder = Set_decode_completed;
            // Execute next function
            // Next_decoder();
        }

        /// <summary>
        ///  The Set decode Completed function to Invoke <see cref="SET_Packet_Decoded"/> Event
        /// </summary>
        /// <remarks>
        /// The helper decoder function to Invoke <see cref="SET_Packet_Decoded"/> or <see cref="PacketDrop"/>  Events to notify
        ///  the AL(REF) client <see cref="DLMS_Application_Process"/> module
        /// </remarks>
        private void Set_decode_completed()
        {
            decode_counter = 0;
            if (flg_Packet_Dropped == 1)
            {
                #region Debugger & Logger
#if Enable_Log_Message
                //***Debugger & Logger
                DLMSLogger.LogALMessage("Packet is dropped:Set_decode_completed", ALMessageType.Decode, PacketType.SET);
#endif
                #endregion
                flg_Packet_Dropped = 0;
                if (_PacketDrop != null)
                    _PacketDrop.Invoke(PacketType.SET, "Packet is dropped:Set_decode_completed");
            }
            else
            {
                // make Set_to_AP structure ready and raise event
                #region Debugger & Logger
#if Enable_Log_Message
                //***Debugger & Logger
                DLMSLogger.LogALMessage("Packet decoded:Set_decode_completed", ALMessageType.Decode, PacketType.SET);
#endif
                #endregion
                if (_SET_Packet_Decoded != null)
                    _SET_Packet_Decoded.Invoke(SET_to_AP);
            }
        }

        /// <summary>
        /// Set Packet drop routine;On Error Condition Set flg_Packet_Dropped flag
        /// </summary>
        private void Set_Drop_Packet()
        {
            flg_Packet_Dropped = 1;
            Set_decode_completed();
        }

        /// <summary>
        /// Set Do Nothing
        /// </summary>
        private void Set_Do_Nothing()
        {

        }

        #endregion

        //------------------------------------- ACTION DECODING ------------------------------->>
        //--------------------------------Code modified for optimization----------------------->>
        #region Action Decoding

        /// <summary>
        /// This decoder function performs Action Response APDU decoding Process;On Error invoke Action_Drop_Packet helper function
        /// </summary>
        /// <remarks>
        /// The ACTION service, used to invoke one or more methods of COSEM interface objects.
        /// Invoking methods may imply sending service parameters and returning data
        /// </remarks>
        private void Action_resp_decode()
        {
            // Storage for current char
            //ActionResponse Type (Normal ,With List etc)
            byte current_char = Recieved_Packet[decode_counter++];
            ACTION_to_AP.Response_Type = current_char;

            if (current_char == (byte)(Action_res_Type.Normal))
                Action_resp_NORMAL_decode();  //Next_decoder = Action_resp_NORMAL_decode;
            //else if (current_char == 0x03) Next_decoder = Action_resp_WITH_LIST_decode;
            else
                Action_Drop_Packet(); //Next_decoder = Action_Drop_Packet;

            // Execute next function
            //Next_decoder();
        }

        /// <summary>
        /// Action Response NORMAL Decoding
        /// </summary>
        private void Action_resp_NORMAL_decode()
        {
            // Store in structure
            // Invoke ID Priority
            byte current_char = Recieved_Packet[decode_counter++];
            ACTION_to_AP.Invoke_Id_Priority = current_char;

            /// Store in structure
            /// ActionResponse.Result
            current_char = Recieved_Packet[decode_counter++];
            ACTION_to_AP.Action_Response.Result = current_char;

            /// Action Response Flags
            current_char = Recieved_Packet[decode_counter++];
            if (current_char == 0x00)
            {
                // check for end of packet
                if (DLMS_Common.Length_Verified(Recieved_Packet, decode_counter, 0, false))
                {
                    ACTION_to_AP.Action_Response.Flg_Response_Parameters = current_char;

                    // completed
                    // Next_decoder = Action_decode_completed;
                    Action_decode_completed();
                }
                else
                {
                    Action_Drop_Packet();
                    //Next_decoder = Action_Drop_Packet;
                }
            }
            else
            {
                ACTION_to_AP.Action_Response.Flg_Response_Parameters = current_char;

                current_char = Recieved_Packet[decode_counter++];

                if (current_char == 0x00)
                {
                    //store to struct
                    ACTION_to_AP.Action_Response.Response_Parameters.Choice = current_char;

                    //save data
                    DLMS_Common.Byte_Array_Copier(Recieved_Packet,
                        ref ACTION_to_AP.Action_Response.Response_Parameters.Data, decode_counter);

                    // completed
                    //Next_decoder = Action_decode_completed;
                    Action_decode_completed();
                }
                else if (current_char == 0x01)
                {
                    ACTION_to_AP.Action_Response.Response_Parameters.Choice = current_char;

                    current_char = Recieved_Packet[decode_counter++];
                    // check for end of packet
                    if (DLMS_Common.Length_Verified(Recieved_Packet, decode_counter, 0, false))
                    {
                        /// store to struct
                        ACTION_to_AP.Action_Response.Response_Parameters.Data_Access_Result = current_char;

                        /// completed
                        /// Next_decoder = Action_decode_completed;
                        Action_decode_completed();
                    }
                    else
                        Action_Drop_Packet(); /// Next_decoder = Action_Drop_Packet;
                }
                else
                {
                    /// Next_decoder = Action_Drop_Packet;
                    Action_Drop_Packet();
                }
            }
            /// Execute next function
            /// Next_decoder();
        }

        /// <summary>
        /// Action Response WITH LIST Decoding
        /// </summary>
        private void Action_resp_WITH_LIST_decode()
        {
            //TODO:This function implement the Action Response With List Decode
            //Next_decoder = Action_Drop_Packet;
            //Next_decoder.Invoke();
            Action_Drop_Packet();
        }

        /// <summary>
        ///  The Action decode Completed decoder function to Invoke <see cref="Action_Packet_Decoded"/> Event
        /// </summary>
        /// <remarks>
        /// The helper decoder function to Invoke <see cref="Action_Packet_Decoded"/> or <see cref="PacketDrop"/> Events to notify
        ///  the AL(REF) Application Layer Client <see cref="DLMS_Application_Process"/> module
        /// </remarks>
        private void Action_decode_completed()
        {
            try
            {
                decode_counter = 0;
                if (flg_Packet_Dropped == 1)
                {
                    #region Debugger & Logger
#if Enable_Log_Message
                    //***Debugger & Logger
                    DLMSLogger.LogALMessage("Packet is dropped:Action_decode_completed", ALMessageType.Decode, PacketType.Action);
#endif
                    #endregion
                    flg_Packet_Dropped = 0;
                    if (_PacketDrop != null)
                        _PacketDrop.Invoke(PacketType.Action, "Packet is dropped:Action_decode_completed");
                }
                else
                {
                    #region Debugger & Logger
#if Enable_Log_Message
                    //***Debugger & Logger
                    DLMSLogger.LogALMessage("Packet decoded:Action_decode_completed", ALMessageType.Decode, PacketType.Action);
#endif
                    #endregion
                    // make Action_to_AP structure ready and raise event
                    if (_Action_Packet_Decoded != null)
                        _Action_Packet_Decoded.Invoke(ACTION_to_AP);
                }
            }
            finally
            {
                if (ACTION_to_AP.Response_Type == (byte)Action_res_Type.With_next_pblock
                    && ACTION_to_AP.DataBlock_SA.Last_Block == 0x01)
                {
                    ACTION_to_AP = new stACTION();
                }
                else if (ACTION_to_AP.Response_Type == (byte)Action_res_Type.Normal)
                {
                    ACTION_to_AP = new stACTION();
                }
            }
        }

        /// <summary>
        /// Action Packet drop routine;On Error Conidtion Set flg_Packet_Dropped
        /// </summary>
        private void Action_Drop_Packet()
        {
            flg_Packet_Dropped = 1;
            Action_decode_completed();
        }

        /// <summary>
        ///  Action Do Nothing
        /// </summary>
        private void Action_Do_Nothing()
        {
            #region Debugger & Logger
#if Enable_Log_Message
            //**Debugger & Logger
            DLMSLogger.LogALMessage("Do Nothing:Action_Do_Nothing", ALMessageType.Decode, PacketType.Action);
#endif
            #endregion
        }

        #endregion

        //------------------------------- EVENT NOTIFICATION DECODING -------------------------->>
        //-------------------------------Code Revised & Modified for Optimized------------------>>
        #region Event Notification Decoding

        /// <summary>
        /// This Event_Notify_decode function decodes Event Service Response;On Error Invoke Get_Drop_Packet
        /// </summary>
        /// <remarks>
        /// A non client/server unsolicited type data communication service used with logical name (LN) referencing.
        /// Using the EventNotification.request(REF) Service, the server application process is able to send an
        /// unsolicited notification of the occurrence of an event to the remote client application
        /// </remarks>
        private void Event_Notify_decode()
        {
            Init_stEventNotification_Request();

            // Storage for current char
            byte current_char = Recieved_Packet[decode_counter++];

            if (current_char == 0x00)
            {
                Event_Notification_to_AP.Flg_Time = 0;
                Event_decode_zero_zero();
            }
            else
            {
                Event_Notification_to_AP.Flg_Time = 1;
                Event_decode_save_time();
                Event_decode_zero_zero();
            }
        }

        /// <summary>
        /// Event Notification decode 00
        /// </summary>
        private void Event_decode_zero_zero()
        {
            // Storage for current char
            byte current_char = Recieved_Packet[decode_counter++];
            ushort char_2 = Recieved_Packet[decode_counter++];
            ushort temp_Class_id = Convert.ToUInt16((current_char << 8) + char_2);

            // Save to strut
            Event_Notification_to_AP.COSEM_Attribute_Descriptor.COSEM_Class_Id = temp_Class_id;
            #region // Save COSEM_Object_Instance_Id

            byte[] byte_AARE_Temp_Array1 = new byte[6];
            Buffer.BlockCopy(Recieved_Packet, decode_counter, byte_AARE_Temp_Array1, 0, byte_AARE_Temp_Array1.Length);
            decode_counter += byte_AARE_Temp_Array1.Length;
            // Store to strut
            Event_Notification_to_AP.COSEM_Attribute_Descriptor.COSEM_Object_Instance_Id = byte_AARE_Temp_Array1;

            // store to strut
            #endregion

            current_char = Recieved_Packet[decode_counter++];
            Event_Notification_to_AP.COSEM_Attribute_Descriptor.COSEM_Object_Attribute_Id = current_char;
            DLMS_Common.Byte_Array_Copier(Recieved_Packet, ref Event_Notification_to_AP.Attribute_Value, decode_counter);
            Event_Notify_decode_completed();
        }

        /// <summary>
        /// Event Notification save time
        /// </summary>
        private void Event_decode_save_time()
        {
            int DateTime_Length = 12;

            DLMS_Common.Byte_Array_Copier(Recieved_Packet, ref Event_Notification_to_AP.Time, decode_counter, DateTime_Length);
            decode_counter += DateTime_Length;
        }

        /// <summary>
        /// The Event Notification decode Completed function to Invoke <see cref="Action_Packet_Decoded"/> or <see cref="PacketDrop"/> Event
        /// </summary>
        /// <remarks>
        /// The helper decoder function to Invoke <see cref="Action_Packet_Decoded"/> or <see cref="PacketDrop"/> Event to notify
        ///  the AL(REF) Application Layer Client <see cref="DLMS_Application_Process"/> module
        /// </remarks>
        private void Event_Notify_decode_completed()
        {
            try
            {

                decode_counter = 0;
                if (flg_Packet_Dropped == 1)
                {
                    //*** Debugger & Logger
                    DLMSLogger.LogALMessage("Packet is dropped:Event_Notify_decode_completed", ALMessageType.Decode, PacketType.EventNotification);
                    flg_Packet_Dropped = 0;
                    if (_PacketDrop != null)
                        _PacketDrop.Invoke(PacketType.EventNotification, "Packet is dropped:Event_Notify_decode_completed");
                }
                else
                {
                    // make "EVENT NOTIFICATION to AP" structure ready and raise event
                    // Debugger & Logger
                    DLMSLogger.LogALMessage("Packet decoded:Event_Notify_decode_completed", ALMessageType.Decode, PacketType.EventNotification);
                    if (_EventNotification_Packet_Decoded != null)
                        _EventNotification_Packet_Decoded(Event_Notification_to_AP);
                }
            }
            finally
            {
                if (Event_Notification_to_AP != null)
                {
                    Event_Notification_to_AP.Attribute_Value = new byte[06];
                }
            }
        }

        /// <summary>
        /// Event Notification Packet drop routine;On Error Condition Set flg_Packet_Dropped
        /// </summary>
        private void Event_Notify_Drop_Packet()
        {
            flg_Packet_Dropped = 1;
            Event_Notify_decode_completed();
        }

        /// <summary>
        /// Event Notification Do Nothing
        /// </summary>
        private void Event_Notify_Do_Nothing()
        {
            DLMSLogger.LogALMessage("Do Nothing:Event_Notify_Do_Nothing", ALMessageType.Decode, PacketType.EventNotification);
        }


        #endregion

        /// <summary>
        ///  The Generic Packet drop routine to Invoke <see cref="PacketDrop"/> Event
        /// </summary>
        /// <remarks>
        /// The helper decoder function to Invoke <see cref="PacketDrop"/> Event to notify
        ///  the AL(REF) client <see cref="DLMS_Application_Process"/> module
        /// </remarks>
        private void No_Case_Drop_Packet()
        {
            decode_counter = 0;
            #region Debugger & Logger
#if Enable_Log_Message
            //**Debugger & Logger
            DLMSLogger.LogALMessage("Identifier not recognized or wrapper was wrong, Packet dropped:No_Case_Drop_Packet",
                ALMessageType.Decode, PacketType.UNKNOWN);
#endif
            #endregion
            if (_PacketDrop != null)
                _PacketDrop.Invoke(PacketType.UNKNOWN, "Identifier not recognized or wrapper was wrong, Packet dropped:No_Case_Drop_Packet");
        }

        /// <summary>
        /// Free Relevant Resource,Cal IDisposable Function
        /// </summary>
        ~DLMS_Application_Layer()
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
                #region Dispose Off Member Variables

                APPLICATION_ASSOCIATION_to_AP = null;         // Structure to be sent to AL
                APPLICATION_ASSOCIATION_from_AP = null;     // Storage for recieved structure from AL
                GET_to_AP = null;                           // GET from AL
                GET_from_AP = null;                         // GET for AL
                SET_to_AP = null;
                SET_from_AP = null;
                ACTION_to_AP = null;
                ACTION_from_AP = null;
                ASSOCIATION_RELEASE_to_AP = null;
                ASSOCIATION_RELEASE_from_AP = null;
                Event_Notification_to_AP = null;
                Recieved_Packet = null;
                // Response_Packet = null;

                /*
                 * AARQ_Lowest_header1 = null;
                
                 * AARQ_Lowest_header2 = null;
                
                 * AARQ_Lowest_header3 = null;
                
                 * AARQ_Lowest_header4 = null;

                
                 * AARQ_LOW_header1 = null;
                
                 * AARQ_LOW_header2 = null;
                
                 * AARQ_LOW_header3 = null;
                
                 * AARQ_LOW_header4 = null;
                
                 * AARQ_LOW_header5 = null;
                
                 * AARQ_LOW_header6 = null;
                
                 * AARQ_LOW_header7 = null; 
                 */

                #endregion
                DetachEventHandlers();
            }
            catch
            {
            }
        }

        #endregion

        /// <summary>
        /// This helper function remove all 
        /// </summary>
        internal void DetachEventHandlers()
        {
            try
            {
                #region // Remove AARE_Packet_Decoded Event Handlers

                Delegate[] Handlers = null;
                if (_AARE_Packet_Decoded != null)
                {
                    Handlers = _AARE_Packet_Decoded.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        AARE_Packet_Decoded -= (Event_Handler_AARE)item;
                    }
                }

                #endregion
                #region // Remove str_Packet_Recvd Event Handlers

                // Handlers = null;
                // if (str_Packet_Recvd != null)
                // {
                //     Handlers = str_Packet_Recvd.GetInvocationList();
                //     foreach (Delegate item in Handlers)
                //     {
                //         str_Packet_Recvd -= (Event_Handler_Custom)item;
                //     }
                // }

                #endregion
                #region // Remove GET_Packet_Decoded Event Handlers

                Handlers = null;
                if (_GET_Packet_Decoded != null)
                {
                    Handlers = _GET_Packet_Decoded.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        GET_Packet_Decoded -= (Event_Handler_GET)item;
                    }
                }

                #endregion
                #region // Remove SET_Packet_Decoded Event Handlers

                Handlers = null;
                if (_SET_Packet_Decoded != null)
                {
                    Handlers = _SET_Packet_Decoded.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        SET_Packet_Decoded -= (Event_Handler_SET)item;
                    }
                }

                #endregion
                #region // Remove Action_Packet_Decoded Event Handlers

                Handlers = null;
                if (_Action_Packet_Decoded != null)
                {
                    Handlers = _Action_Packet_Decoded.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        Action_Packet_Decoded -= (Event_Handler_Action)item;
                    }
                }

                #endregion
                #region // Remove EventNotification_Packet_Decoded Event Handlers

                Handlers = null;
                if (_EventNotification_Packet_Decoded != null)
                {
                    Handlers = _EventNotification_Packet_Decoded.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        EventNotification_Packet_Decoded -= (Event_Handler_EventNotification)item;
                    }
                }

                #endregion
                #region // Remove Association_Release_Packet_Decoded Event Handlers

                Handlers = null;
                if (_Association_Release_Packet_Decoded != null)
                {
                    Handlers = _Association_Release_Packet_Decoded.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        Association_Release_Packet_Decoded -= (Event_Handler_Association_Release)item;
                    }
                }

                #endregion
                #region // Remove PacketDrop Event Handlers

                Handlers = null;
                if (_PacketDrop != null)
                {
                    Handlers = _PacketDrop.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        PacketDrop -= (Action<PacketType, String>)item;
                    }
                }

                #endregion
            }
            catch { }
        }

        internal void AttachEventHandlers()
        {
            try
            {
                // Create Event Handler
                // str_Packet_Recvd += new Event_Handler_Custom(Decode_APDU);
            }
            catch { }
        }
    }
    // End Class DLMS_Application_Layer
}
