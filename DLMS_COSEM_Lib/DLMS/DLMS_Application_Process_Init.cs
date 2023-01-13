// Code Revised & Modified
// Revision # 1.1 Tuesday 09 October 2012
// <Comments>
//  Code Revision & Modification For Code Optimization
//  </Comments>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS.LRUCache;
using DLMS.Comm;

namespace DLMS
{
    #region delegate
    
    /// <summary>
    /// It will provide particular DLMS_COSEM Class Instance/Object fully initialized
    /// </summary>
    public delegate Base_Class GetSAPEntry(StOBISCode ObjIdentifier);
    /// <summary>
    /// It will provide KEY to DLMS_COSEM Instance/Object
    /// </summary>
    public delegate Base_Class GetSAPEntryKeyIndex(KeyIndexer ObjIdentifier);
    /// <summary>
    /// It will provide <see cref="OBISCodeRights"/> Access Rights in Current SAP Context for Particular DLMS_COSEM Instance/Object
    /// </summary>
    public delegate OBISCodeRights GetSAPRights(StOBISCode ObjIdentifier); 

    #endregion

    /// <summary>
    /// Class Contains Objects Definitions And Application Process Initialization Code
    /// </summary>
    public partial class DLMS_Application_Process
    {
        #region General Parameters

        private const byte Max_Associations = 0x64;
        private const ushort MaxSETHeader = 0x1E;
        private const ushort MaxActionHeader = 0x1E;
        private GetSAPEntry GetSAPEntryDelegate;
        private DLMSLogger logger;
        private uint user_Software_ID = 0x01020304;

        #endregion

        #region Properties
        
        /// <summary>
        /// Service Access Point(REF) for Client AP
        /// </summary>
        public SAP_Object CurrentClientSAP
        {
            get
            {
                return current_Client_SAP;
            }
            set
            {
                current_Client_SAP = value;

                // if (flg_Application_Process_Init)
                // {
                //     bool flagValExists = false;
                //     foreach (var item in Client_Logical_Devices)
                //     {
                //         if (item.Equals(value))
                //         {
                //             flagValExists = true;
                //             current_Client_SAP = item;
                //         }
                   
                //     }
                //     if (!flagValExists)
                //         throw new DLMSException("Unable to Set Current Client SAP,Application Process not initialized Properly");
                // }
                // else
                //     throw new DLMSException("Unable to Set Current Client SAP,Application Process not initialized");
            }
        }
        
        /// <summary>
        /// Service Access Point(REF) for Server AP
        /// </summary>
        public SAP_Object CurrentMeterSAP
        {
            get { return current_Meter_SAP; }
            set
            {
                current_Meter_SAP = value;

                // if (flg_Application_Process_Init)
                // {
                //     bool flagValExists = false;
                //     foreach (var item in Server_Logical_Devices)
                //     {
                //         if (item.Equals(value))
                //         {
                //             current_Meter_SAP = item;
                //             flagValExists = true;
                //         }
                   
                //     }
                //     if (!flagValExists)
                //         throw new DLMSException("Unable to Set Current Meter SAP,Application Process not initialized Properly");
                // }
                // else
                //     throw new DLMSException("Unable to Set Current Meter SAP,Application Process not initialized");

            }
        }

        /// <summary>
        /// To get Max Number Of Possible Client-Server ACSE(REF) Association Count
        /// </summary>
        public List<uint> Total_Associations
        {
            get
            {
                if (Client_Logical_Devices != null && Server_Logical_Devices != null &&
                    Client_Logical_Devices.Count > 0 && Server_Logical_Devices.Count > 0)
                {
                    List<uint> totalAssociations = new List<uint>();
                    foreach (SAP_Object client_SAP in Client_Logical_Devices)
                        foreach (SAP_Object meter_SAP in Server_Logical_Devices)
                        {
                            totalAssociations.Add((UInt32)((client_SAP.SAP_Address << 16) | meter_SAP.SAP_Address));
                        }
                    return totalAssociations;
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// To get Max Number Of Possible Client-Server ACSE(REF) Association Count
        /// </summary>
        public ushort Total_associations_supported
        {
            get
            {
                if (Client_Logical_Devices != null && Server_Logical_Devices != null)
                    return (ushort)(Client_Logical_Devices.Count * Server_Logical_Devices.Count);
                else
                    return 0;
            }
        }
        /// <summary>
        /// To get Index for Current Association in Total_Associations Structure
        /// </summary>
        internal byte Current_Association_Index
        {
            get
            {
                return Get_Association_Index(CurrentMeterSAP, CurrentClientSAP);
            }
        }
        
        /// <summary>
        /// To get Index for Current Association in Total_Associations Structure
        /// </summary>
        internal byte Get_Association_Index(SAP_Object MeterSAP, SAP_Object ClientSAP)
        {
            try
            {
                uint AssociationVal = (UInt32)((ClientSAP._SAP_Address << 16) | MeterSAP._SAP_Address);
                if (Total_Associations != null)
                {
                    int index = Total_Associations.IndexOf(AssociationVal);
                    return (index != -1) ? (byte)index : (byte)0xFF;
                }
                else
                    return (byte)0xFF;
            }
            catch (Exception ex)
            {
                return (byte)0xFF;
            }
        }

        /// <summary>
        /// To get Index for Current Association in Total_Associations Structure
        /// </summary>
        internal uint Current_Association
        {
            get
            {
                return (UInt32)((CurrentClientSAP._SAP_Address << 16) | CurrentMeterSAP._SAP_Address);
            }
        }

        /// <summary>
        /// AL(Application_Layer) Class instance <see cref="DLMS_Application_Layer"/> that reveals set of 
        /// helper function to implement Service Supported by DLMS_COSEM
        /// </summary>
        public DLMS_Application_Layer ApplicationLayer
        {
            get { return Application_Layer; }

        }
        /// <summary>
        /// Flag Indicate Either Client AP(Application Process) IsConnected With Server AP(Application Process).
        /// The flag flg_Association_Developed is updated after AARQ response decoded by AP
        /// </summary>
        public bool Is_Association_Developed
        {
            get { return flg_Association_Developed; }
            set { flg_Association_Developed = value; }
        }
        /// <summary>
        /// To get instance of <see cref="DLMS.DLMSLogger"/>
        /// </summary>
        /// <remarks>
        /// DLMSLogger class Logs step by step CheckPoints in different functions,
        /// various error Exceptional cases and Log IODumps in Process
        /// </remarks>
        public DLMSLogger Logger
        {
            get
            {
                return logger;
            }
        }
        /// <summary>
        /// To get instance of <see cref="GetSAPEntry"/>
        /// </summary>
        public GetSAPEntry GetSAPTableEntryDelegate
        {
            get { return GetSAPEntryDelegate; }
            set { GetSAPEntryDelegate = value; }
        }
        /// <summary>
        /// To initialize the Software_User_ID
        /// </summary>
        /// <remarks>
        /// The User_Software_ID information is pass in AARQ_Service to
        /// identify the Client Software that login in Server
        /// </remarks>
        public uint User_Software_ID
        {
            get { return this.user_Software_ID; }
            set { this.user_Software_ID = value; }
        }

        #endregion

        /// <summary>
        /// To initialize Struts and state variable use in AL (Application_Layer) and Client AP (Application_Process) classes
        /// </summary>
        internal void InitializeVariables()
        {
            Client_Logical_Devices = DLMS_Common.InitClientSAP();
            // Initialize AARQ Strut
            Init_stApplication_Association();

            // Initialize Get Request Strut
            Init_stGet_Request();

            // Initialize Set Request Strut
            Init_stSet_Request();

            // Initialize Action Request Strut
            Init_stAction_Request();

            // Initialize Association Release Strut
            ASSOCIATION_RELEASE_to_AL.Reason = 0xff;       // No Reason
            // Send_ASSOCIATION_RELEASE.Reason = 0x12;     // could be any reason
        }
        
        //------------------------------------------------------------------------
        /// <summary>
        /// To initialize Strut <see cref="stAPPLICATION_ASSOCIATION"/>  to be use in Encoding AARQ_Service(REF) Request Processing
        /// </summary>
        private void Init_stApplication_Association()
        {
            Send_APPLICATION_ASSOCIATION.Application_Context_Name = new stApplication_Context()
            {
                ctt_element = 0x60,
                Country_element = 0x85,
                Country_name_element = 0x74,
                Organization_element = 0x05,

                DLMS_UA_element = 0x08,
                Application_Context_element = 0x01,
                Context_id_element = 0x01
            };

            if (Negotiated_Conformance != null)
                Negotiated_Conformance.Clear();

            Send_APPLICATION_ASSOCIATION.Service_Class = 0;
            Send_APPLICATION_ASSOCIATION.Proposed_xDLMS_Context.Proposed_DLMS_Version_Number = 0x06;

            // Proposed DLMS Conformance
            DLMS_Common.Byte_Array_Copier(Proposed_Conformance,
                                          ref Send_APPLICATION_ASSOCIATION.Proposed_xDLMS_Context.Proposed_DLMS_Conformance, 0);

            // AP APDU Size
            Send_APPLICATION_ASSOCIATION.Proposed_xDLMS_Context.Client_Max_Receive_PDU_Size = 0x04B0; //0xFFFF;

            // Initialize Security Mechanism Name
            Send_APPLICATION_ASSOCIATION.Security_Mechanism_Name = new stAuthentication_MechanismName()
            {
                ctt_element = DLMS_Application_Layer.SecMechanismName[0],
                Country_element = DLMS_Application_Layer.SecMechanismName[1],
                Country_name_element = Convert.ToUInt16(DLMS_Application_Layer.SecMechanismName[2]),
                Organization_element = DLMS_Application_Layer.SecMechanismName[3],
                DLMS_UA_element = DLMS_Application_Layer.SecMechanismName[4],
                Authen_mechanism_name_element = DLMS_Application_Layer.SecMechanismName[5],
                mechanism_id_element = DLMS_Application_Layer.SecMechanismName[6]
            };

            // Assign default value
            Send_APPLICATION_ASSOCIATION.Result = (byte)AssociationResult.RejectedPermanent;
            Send_APPLICATION_ASSOCIATION.XDLMS_Initiate_Error = null;

            Send_APPLICATION_ASSOCIATION.Flg_CompatibilityMode = 1;
        }

        /// <summary>
        /// To initialize Strut <see cref="stGET"/>  to be use in GET_Service(REF) Request/Response Processing
        /// </summary>
        private void Init_stGet_Request()
        {
            GET_to_AL = new stGET();
            GET_from_AL = new stGET();

            GET_to_AL.Request_Type = (byte)GET_Method.Normal;
            // GET_to_AL.Request_Type = (byte)GET_Method.With_Block;
            // Send_GET.Request_Type = (byte)GET_Method.WithList;

            GET_to_AL.Invoke_Id_Priority = 0x81;
            GET_to_AL.COSEM_Attribute_Descriptor.COSEM_Class_Id = 0x0011;

            byte[] DATA = { 0x00, 0x00, 0x29, 0x00, 0x00, 0xFF };
            DATA.CopyTo(GET_to_AL.COSEM_Attribute_Descriptor.COSEM_Object_Instance_Id, 0);

            GET_to_AL.COSEM_Attribute_Descriptor.COSEM_Object_Attribute_Id = 0x02;
            GET_to_AL.COSEM_Attribute_Descriptor.Access_Selection_Parameters = 0x00;
            GET_to_AL.Block_Number = 0x00;

            // Initialize Get_From_AL
            GET_from_AL.Request_Type = (byte)GET_Method.Normal;
            GET_from_AL.Result = (byte)Data_Access_Result.Object_Unavailable;
            GET_from_AL.Response_Type = (byte)GET_Method.Normal;
            GET_from_AL.Block_Number = 0x00;
        }

        /// <summary>
        /// To initialize Strut <see cref="stSET"/>  to use in SET_Service(REF) Request Processing
        /// </summary>
        private void Init_stSet_Request()
        {
            SET_to_AL.Request_Type = 0x01; // NORMAL
            SET_to_AL.Invoke_Id_Priority = 0xC1;

            SET_to_AL.COSEM_Attribute_Descriptor.COSEM_Class_Id = 0x0011;

            byte[] OBIS = { 0x00, 0x00, 0x29, 0x00, 0x00, 0xFF };
            OBIS.CopyTo(SET_to_AL.COSEM_Attribute_Descriptor.COSEM_Object_Instance_Id, 0);

            SET_to_AL.COSEM_Attribute_Descriptor.COSEM_Object_Attribute_Id = 0x02;
            SET_to_AL.COSEM_Attribute_Descriptor.Access_Selection_Parameters = 0x00;

            SET_to_AL.DataBlock_SA = new stDataBlock_SA();

            byte[] DATA1 = { 0x05, 0x05, 0x05, 0x05, 0x05 };
            SET_to_AL.Data = DATA1;

        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// To initialize Strut <see cref="stACTION"/>  to use in MethodInvoke_Service(REF) Request Processing
        /// </summary>
        private void Init_stAction_Request()
        {
            ACTION_to_AL.Request_Type = 0x01;    // NORMAL
            ACTION_to_AL.Invoke_Id_Priority = 0xC1;
            ACTION_to_AL.COSEM_Method_Descriptor.COSEM_Class_Id = 0x0011;

            //Send_ACTION.Flg_Method_Invocation_Parameters = 0x00;  // method invocation parameters not to be sent
            ACTION_to_AL.Flg_Method_Invocation_Parameters = 0x01;    // method invocation parameters not to be sent

            byte[] OBIS1 = { 0x00, 0x00, 0x29, 0x00, 0x00, 0xFF };
            OBIS1.CopyTo(ACTION_to_AL.COSEM_Method_Descriptor.COSEM_Object_Instance_Id, 0);

            ACTION_to_AL.COSEM_Method_Descriptor.COSEM_Object_Method_Id = 0x02;

            byte[] DATA2 = { 0x05, 0x05, 0x05, 0x05, 0x05 };
            if (ACTION_to_AL.Method_Invocation_Parameters == null || ACTION_to_AL.Method_Invocation_Parameters.Length != DATA2.Length)
                Array.Resize<byte>(ref ACTION_to_AL.Method_Invocation_Parameters, DATA2.Length);

            DATA2.CopyTo(ACTION_to_AL.Method_Invocation_Parameters, 0);
        }

        ///--------------------------------------------------------------------------------------
        private void Init_stEventNotification_Request(stEVENT_NOTIFICATION EventNotify_from_AL)
        {
            if (EventNotify_from_AL == null)
                EventNotify_from_AL = new stEVENT_NOTIFICATION();

            DLMS_Application_Layer.Init_stEventNotification_Request(EventNotify_from_AL);
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// To Initialize the <see cref="Client_Logical_Devices"/> and <see cref="Server_Logical_Devices"/> data-Struts
        /// to use in AARQ_Service processing,SAP Table Processing etc
        /// </summary>
        /// <param name="meterSAPList"></param>
        /// <param name="ClientSAPList"></param>
        public void InitApplicationProcessState(List<SAP_Object> meterSAPList, List<SAP_Object> ClientSAPList)
        {
            if (meterSAPList == null || meterSAPList.Count == 0 || ClientSAPList == null || ClientSAPList.Count == 0)
                throw new Exception("Invalid argument passed");
            Client_Logical_Devices = new List<SAP_Object>();
            Server_Logical_Devices = new List<SAP_Object>();
            
            flg_SAP_List_Recieved = true;
            foreach (SAP_Object sap in ClientSAPList)
            {
                Client_Logical_Devices.Add(sap);
            }
            foreach (SAP_Object sap in meterSAPList)
            {
                Server_Logical_Devices.Add(sap);
            }
            flg_Application_Process_Init = true;

            // if (!flg_Application_Process_Init)
            // {
            //     Define_Objcts();
               
            // }
            // Current_Association
            // CurrentClientSAP = Client_Logical_Devices[0];
            // CurrentMeterSAP = Server_Logical_Devices[0];
        }
    }

    #region DataTypesDefinition

    /// <summary>
    /// COSEM IC(Interface Classes)(REF) Valid Ids
    /// </summary>
    public enum Class_ID_No : byte
    {
        IC_1 = 1,
        IC_3 = 3,
        IC_4 = 4,
        IC_7 = 7,
        IC_15 = 15,
    }

    /// <summary>
    /// Data_Access_Result is OutCome Result for GET_Service and SET_Service Operation
    /// </summary>
    /// <remarks>
    /// The enumerated const value notify Success or error Codes for failure;Result for SET_Service Operation
    /// </remarks>
    public enum Data_Access_Result : byte
    {
        /// <summary>
        /// Connection is rejected.
        /// </summary>
        Rejected = 0xFF, 
        Success = 0x00,
        Hardware_Fault = 0x01,
        Temporary_Failure = 0x02,
        Read_Write_Denied = 0x03,
        Object_Undefined = 0x04,
        Object_Class_Inconsistent = 0x09,
        Object_Unavailable = 0x0B,
        Type_Mismatched = 0x0C,
        Scope_of_Access_Violated = 0x0D,
        Data_Block_Unavailable = 0x0E,
        Long_Get_Aborted = 0x0F,
        no_Long_Get_in_Progress = 0x10,
        Long_Set_Aborted = 0x11,
        no_Long_Set_in_Progress = 0x12,
        Data_Block_NumberInvalid = 0x13,
        Other_Reason = 0xFA,
    }
    
    /// <summary>
    /// Action_Result is OutCome Result for Invoke_Service Operation
    /// </summary>
    /// <remarks>
    /// The enumerated const value notify Success or error Codes for failure;Result for Invoke_Service Operation
    /// </remarks>
    public enum Action_Result : byte
    {
        Rejected = 0xFF, 
        Success = 0,
        Hardware_fault = 1,
        Temporary_failure = 2,
        Read_write_denied = 3,
        Object_undefined = 4,
        Object_class_inconsistent = 9,
        Object_unavailable = 11,
        Type_unmatched = 12,
        Scope_of_access_violated = 13,
        Data_block_unavailable = 14,
        Long_action_aborted = 15,
        No_long_action_in_progress = 16,
        other_reason = 250
    }

    /// <summary>
    /// To identify the Action_Service(REF) response Type received
    /// </summary>
    /// <remarks>
    /// The Actoin_res_Type value is present in <see cref="stACTION"/> strut
    /// </remarks>
    public enum Action_res_Type : byte
    {
        Normal = 1,
        Next_pblock,
        With_list,
        With_next_pblock,
    }

    /// <summary>
    /// To identify the Action_Service(REF) request Type
    /// </summary>
    /// <remarks>
    /// The ACTION_req_Type value is present in <see cref="stACTION"/> strut
    /// </remarks>
    public enum ACTION_req_Type : byte
    {
        Normal = 1,
        Next_pblock,
        With_list,
        With_first_pblock,
        With_list_and_first_pblock,
        With_pblock
    }

    /// <summary>
    /// To identify the SET_Service(REF) request Type
    /// </summary>
    /// <remarks>
    /// The Set_Req_Type value is present in <see cref="stSET"/> strut
    /// </remarks>
    public enum Set_Req_Type : byte
    {
        Normal = 0x01,
        FirstBlock = 0x02,
        NonFirstBlock = 0x03,
    }

    /// <summary>
    /// To identify the GET_Service(REF) response Type
    /// </summary>
    /// <remarks>
    /// The GET_Method value is present in <see cref="stGET"/> strut
    /// </remarks>
    public enum GET_Method : byte
    {
        Normal = 0x01,
        With_Block = 0x02,
        With_List = 0x03,
    }

    /// <summary>
    /// To identify the COSEM IC(REF) instance decoding function outcome, in GET_Service(REF) response decoding process
    /// </summary>
    /// <remarks>
    /// The DecodingResult enumerated const notify either data for Particular Attribute in COSEM IC Instance is Successfully decoded;
    /// DecodingError occurred, Current Not Present in response or not available due to insufficient AccessRights for Attribute.
    /// The DecodingResults are updated in function <see cref="Base_Class.Decode_Data(ref byte[],ref int,int)"/>
    /// for each attribute of COSEM IC(REF) instance.
    /// </remarks>
    public enum DecodingResult : byte
    {
        DataNotPresent = 0x0,
        NoAccess,
        NotInSAPList,
        DecodingError,
        DLMSError,
        Ready
    }

    /// <summary>
    /// To define the Clock_Base for RTC Programming in COSEM IC 8 <see cref="Class_8"/> process
    /// </summary>
    public enum Clock_Base : byte
    {
        Not_Defined = 0,
        Internal_Crystal,
        Mains_Freq_50_Hz,
        Mains_Freq_60_Hz,
        GPS_Clock,
        Radio_Controlled,
    }

    /// <summary>
    /// To define particular Attribute Access Rights for IC Instance in SAP table
    /// </summary>
    public enum Attrib_Access_Modes : byte
    {
        No_Access = 0,
        Read_Only,
        Write_Only,
        Read_Write,
        Authenticated_Read_Only,
        Authenticated_Write_Only,
        Authenticated_Read_Write,
    }

    /// <summary>
    /// To define particular Method Access Rights for COSEM IC Instance in SAP table
    /// </summary>
    public enum Method_Access_Modes : byte
    {
        No_Access = 0,
        Access,
        Authenticated_Access,
    }

    #endregion
}
