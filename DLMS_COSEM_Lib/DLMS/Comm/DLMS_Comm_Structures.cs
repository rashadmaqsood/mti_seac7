using System;
using System.Collections.Generic;
using System.Text;

namespace DLMS.Comm
{
    #region Application Association


    /// <summary>
    /// Member stAPPLICATION_ASSOCIATION Structure
    /// </summary>
    public class stProposed_xDLMS_Context : ICloneable
    {
        public byte[] Dedicated_Key;                        /// will define later.
        public byte Proposed_DLMS_Version_Number;
        public byte[] Proposed_DLMS_Conformance;           /// Size = 3 bytes, all flags
        public ushort Client_Max_Receive_PDU_Size;


        /// <summary>
        /// Constructor
        /// </summary>
        public stProposed_xDLMS_Context()
        {
            /// Initialize arrays
            Proposed_DLMS_Conformance = new byte[3];

            /// unknown length
            Dedicated_Key = new byte[5];        // will define later
        }

        #region ICloneable Members

        public object Clone()
        {
            stProposed_xDLMS_Context Cloned_Object = new stProposed_xDLMS_Context();

            Cloned_Object.Client_Max_Receive_PDU_Size = Client_Max_Receive_PDU_Size;
            Cloned_Object.Proposed_DLMS_Version_Number = Proposed_DLMS_Version_Number;
            Cloned_Object.Proposed_DLMS_Conformance = (byte[])Proposed_DLMS_Conformance.Clone();
            Cloned_Object.Dedicated_Key = (byte[])Dedicated_Key.Clone();

            return Cloned_Object;
        }

        #endregion
    } // End of Class

    /// <summary>
    /// Member stAPPLICATION_ASSOCIATION
    /// </summary>
    public class stNegotiated_xDLMS_Context : ICloneable
    {
        public byte Negotiated_DLMS_Version_Number;
        public byte[] Negotiated_DLMS_Conformance;         /// Size = 3 bytes, all flags
        public ushort Server_Max_Receive_PDU_Size;
        public ushort Server_Max_Send_PDU_Size;
        public ushort VAA_Name;

        /// <summary>
        /// Constructor
        /// </summary>
        public stNegotiated_xDLMS_Context()
        {
            Negotiated_DLMS_Conformance = new byte[3];
        }

        #region ICloneable Members

        public object Clone()
        {
            stNegotiated_xDLMS_Context Cloned_Object = new stNegotiated_xDLMS_Context();

            Cloned_Object.Negotiated_DLMS_Version_Number = Negotiated_DLMS_Version_Number;
            Cloned_Object.Server_Max_Receive_PDU_Size = Server_Max_Receive_PDU_Size;
            Cloned_Object.Server_Max_Send_PDU_Size = Server_Max_Send_PDU_Size;
            Cloned_Object.VAA_Name = VAA_Name;
            Cloned_Object.Negotiated_DLMS_Conformance = (byte[])Negotiated_DLMS_Conformance.Clone();

            return Cloned_Object;
        }

        #endregion
    } /// End of Class

    public class stAppTitle : ICloneable
    {
        public byte[] AP_Title;
        public uint UserId;

        public stAppTitle()
        {
            AP_Title = Encoding.ASCII.GetBytes("MTIsmart");      /// MTIsmart
            UserId = 0x01;
        }

        #region ICloneable Members

        public object Clone()
        {
            stAppTitle Cloned_Object = new stAppTitle();

            if (this.AP_Title != null)
                Cloned_Object.AP_Title = (byte[])this.AP_Title.Clone();

            Cloned_Object.UserId = UserId;

            return Cloned_Object;
        }

        #endregion
    }

    public struct stAuthentication_MechanismName : ICloneable
    {
        /// <summary>
        /// ctt-element: unsigned
        /// </summary>
        public byte ctt_element;
        /// <summary>
        /// country-element: unsigned
        /// </summary>
        public byte Country_element;
        /// <summary>
        /// country-name-element: long-unsigned
        /// </summary>
        public ushort Country_name_element;
        /// <summary>
        /// identified-organization-element: unsigned
        /// </summary>
        public byte Organization_element;
        /// <summary>
        /// DLMS-UA-element: unsigned
        /// </summary>
        public byte DLMS_UA_element;
        /// <summary>
        /// authentication-mechanism-name-element: unsigned
        /// </summary>
        public byte Authen_mechanism_name_element;
        /// <summary>
        /// mechanism-id-element: unsigned
        /// </summary>
        public byte mechanism_id_element;

        #region ICloneable Members

        public object Clone()
        {
            stAuthentication_MechanismName Cloned_Object = new stAuthentication_MechanismName();

            Cloned_Object.ctt_element = ctt_element;
            Cloned_Object.Country_element = Country_element;
            Cloned_Object.Country_name_element = Country_name_element;
            Cloned_Object.Organization_element = Organization_element;

            Cloned_Object.DLMS_UA_element = DLMS_UA_element;
            Cloned_Object.Authen_mechanism_name_element = Authen_mechanism_name_element;
            Cloned_Object.mechanism_id_element = mechanism_id_element;

            return Cloned_Object;
        }

        #endregion
    }

    public struct stApplication_Context : ICloneable
    {
        /// <summary>
        /// ctt-element: unsigned
        /// </summary>
        public byte ctt_element;
        /// <summary>
        /// country-element: unsigned
        /// </summary>
        public byte Country_element;
        /// <summary>
        /// country-name-element: long-unsigned
        /// </summary>
        public ushort Country_name_element;
        /// <summary>
        /// identified-organization-element: unsigned
        /// </summary>
        public byte Organization_element;
        /// <summary>
        /// DLMS-UA-element: unsigned
        /// </summary>
        public byte DLMS_UA_element;
        /// <summary>
        /// application-context-element: unsigned
        /// </summary>
        public byte Application_Context_element;
        /// <summary>
        /// context-id-element: unsigned
        /// </summary>
        public byte Context_id_element;

        #region ICloneable Members

        public object Clone()
        {
            stApplication_Context Cloned_Object = new stApplication_Context();

            Cloned_Object.ctt_element = ctt_element;
            Cloned_Object.Country_element = Country_element;
            Cloned_Object.Country_name_element = Country_name_element;
            Cloned_Object.Organization_element = Organization_element;

            Cloned_Object.DLMS_UA_element = DLMS_UA_element;
            Cloned_Object.Application_Context_element = Application_Context_element;
            Cloned_Object.Context_id_element = Context_id_element;

            return Cloned_Object;
        }

        #endregion
    }

    public class stUser
    {
        public byte UserId;
        public string UserName = String.Empty;
    }

    /// <summary>
    /// Class stAPPLICATION_ASSOCIATION
    /// </summary>
    public class stAPPLICATION_ASSOCIATION : ICloneable
    {
        public UInt16 Protocol_Connect_Parameters;    /// will define later.
        public byte ACSE_Protocol_VERSION;
        public stApplication_Context Application_Context_Name;  /// Size = 8 including null

        public stAppTitle Called_AP_Title;            /// will define later.
        public byte[] Called_AE_Qualifier;            /// will define later.

        public byte Called_AP_Invocation_Identifier;
        public byte Called_AE_Invocation_Identifier;

        public stAppTitle Calling_AP_Title;           /// will define later.
        public byte[] Calling_AE_Qualifier;           /// will define later.

        public byte Calling_AP_Invocation_Identifier;
        public byte Calling_AE_Invocation_Identifier;
        public byte Local_Or_Remote;
        public byte Result;
        public Result_SourceDiagnostic Failure_Type;

        public stAppTitle Responding_AP_Title;        /// will define later.
        public byte[] Responding_AE_Qualifier;        /// will define later.

        public byte Responding_AP_Invocation_Identifier;
        public byte Responding_AE_Invocation_Identifier;

        public byte ACSE_Requirements;                /// will define later.

        public stAuthentication_MechanismName Security_Mechanism_Name;  /// Size = 8 including null

        public byte[] Calling_Authentication_Value;   /// Size = 8 including null
        public byte Responding_Authentication_Value;  /// will define later.
        public byte Implementation_Information;       /// will define later.

        public DLMSErrCodesException XDLMS_Initiate_Error = null;
        public byte User_Information;                 /// will define later.
        public byte Service_Class;                    /// will define later. 

        public byte Flg_Negotiated_xDLMS_Context;
        internal byte Flg_CompatibilityMode = 0;

        public stProposed_xDLMS_Context Proposed_xDLMS_Context;
        public stNegotiated_xDLMS_Context Negotiated_xDLMS_Context;


        #region Authentication_Mechanism

        public HLS_Mechanism Authentication_Mechanism
        {
            get
            {
                // Default Value
                HLS_Mechanism Sec_Mechanism_Name = HLS_Mechanism.LowSec;
                // Last Byte Security_Mechanism_Name
                Sec_Mechanism_Name = (HLS_Mechanism)Security_Mechanism_Name.mechanism_id_element;
                return Sec_Mechanism_Name;
            }
            set
            {
                // Last Byte Security_Mechanism_Name
                Security_Mechanism_Name.mechanism_id_element = Convert.ToByte(value);
            }
        }

        #endregion

        #region Application_Context_ID

        public Application_ContextName Application_Context_ID
        {
            get
            {
                // Default Application_Context_ID
                Application_ContextName _Context_ID = Application_ContextName.LN_Referencing_No_Ciphering;
                // Last Byte Application_Context_Name
                _Context_ID = (Application_ContextName)Application_Context_Name.Context_id_element;
                return _Context_ID;
            }
            set
            {
                // Last Byte Application_Context_ID
                Application_Context_Name.Context_id_element = Convert.ToByte(value);
            }
        }

        #endregion


        /// <summary>
        /// Constructor
        /// </summary>
        public stAPPLICATION_ASSOCIATION()
        {
            // Initialize Arrays
            Application_Context_Name = new stApplication_Context();
            Security_Mechanism_Name = new stAuthentication_MechanismName();
            Calling_Authentication_Value = new byte[8];

            Called_AP_Title = new stAppTitle();                // will define later.
            Called_AE_Qualifier = new byte[5];                 // will define later.
            Calling_AP_Title = new stAppTitle();               // will define later.
            Calling_AE_Qualifier = new byte[5];                // will define later.
            Responding_AP_Title = new stAppTitle();            // will define later.
            Responding_AE_Qualifier = new byte[5];             // will define later.

            // Initialize Structure
            Proposed_xDLMS_Context = new stProposed_xDLMS_Context();
            Negotiated_xDLMS_Context = new stNegotiated_xDLMS_Context();
        }

        #region ICloneable Members

        public object Clone()
        {
            stAPPLICATION_ASSOCIATION Cloned_Object = new stAPPLICATION_ASSOCIATION();

            Cloned_Object.Protocol_Connect_Parameters = Protocol_Connect_Parameters;
            Cloned_Object.ACSE_Protocol_VERSION = ACSE_Protocol_VERSION;
            Cloned_Object.Application_Context_Name = Application_Context_Name;    // Size = 8 including null

            Cloned_Object.Called_AP_Title = (stAppTitle)Called_AP_Title.Clone();
            Cloned_Object.Called_AE_Qualifier = (byte[])Called_AE_Qualifier.Clone();

            Cloned_Object.Called_AP_Invocation_Identifier = Called_AP_Invocation_Identifier;
            Cloned_Object.Called_AE_Invocation_Identifier = Called_AE_Invocation_Identifier;

            Cloned_Object.Calling_AP_Title = (stAppTitle)Calling_AP_Title.Clone();
            Cloned_Object.Calling_AE_Qualifier = (byte[])Calling_AE_Qualifier.Clone();

            Cloned_Object.Calling_AP_Invocation_Identifier = Calling_AP_Invocation_Identifier;
            Cloned_Object.Calling_AE_Invocation_Identifier = Calling_AE_Invocation_Identifier;
            Cloned_Object.Local_Or_Remote = Local_Or_Remote;
            Cloned_Object.Result = Result;
            Cloned_Object.Failure_Type = Failure_Type;


            Cloned_Object.Responding_AP_Title = (stAppTitle)Responding_AP_Title.Clone();
            Cloned_Object.Responding_AE_Qualifier = (byte[])Responding_AE_Qualifier.Clone();

            Cloned_Object.Responding_AP_Invocation_Identifier = Responding_AP_Invocation_Identifier;
            Cloned_Object.Responding_AE_Invocation_Identifier = Responding_AE_Invocation_Identifier;

            Cloned_Object.ACSE_Requirements = ACSE_Requirements;
            Cloned_Object.Security_Mechanism_Name = Security_Mechanism_Name;

            Cloned_Object.Calling_Authentication_Value = Calling_Authentication_Value;
            Cloned_Object.Responding_Authentication_Value = Responding_Authentication_Value;
            Cloned_Object.Implementation_Information = Implementation_Information;

            Cloned_Object.XDLMS_Initiate_Error = XDLMS_Initiate_Error;
            Cloned_Object.User_Information = User_Information;

            Cloned_Object.Service_Class = Service_Class;
            Cloned_Object.Flg_Negotiated_xDLMS_Context = Flg_Negotiated_xDLMS_Context;

            Cloned_Object.Proposed_xDLMS_Context = (stProposed_xDLMS_Context)Proposed_xDLMS_Context.Clone();
            Cloned_Object.Negotiated_xDLMS_Context = (stNegotiated_xDLMS_Context)Negotiated_xDLMS_Context.Clone();

            return Cloned_Object;
        }

        #endregion
    } // End of Class

    #endregion

    //----------------------------------------------------------------------
    #region Set

    /// <summary>
    /// A parameter of the xDLMS attribute-related GET and SET services, used with logical name (LN)
    ///referencing. An attribute is fully identified by the COSEM interface class identifier, the COSEM
    ///object instance identifier (logical name) and the attribute identifier within the given object. GET
    ///and SET services may access the whole attribute, or only a part of it (selective access). 
    ///A GET and SET service may refer to one attribute only, or several attributes. 
    /// </summary>
    public class stCOSEM_Attribute_DescriptorBase : ICloneable
    {
        /// <summary>
        /// COSEM Interface Class Id
        /// </summary>
        public ushort COSEM_Class_Id;
        /// <summary>
        /// COSEM Interface Class Instance Id OBIS(Object Identification System) Code
        /// </summary>
        public byte[] COSEM_Object_Instance_Id;	// size = 6
        /// <summary>
        /// Attribute Specification:Particular request attribute Id
        /// </summary>
        public byte COSEM_Object_Attribute_Id;


        /// <summary>
        /// Constructor
        /// </summary>
        public stCOSEM_Attribute_DescriptorBase()
        {
            COSEM_Object_Instance_Id = new byte[6];
        }

        #region IClonable Members

        public object Clone()
        {
            stCOSEM_Attribute_DescriptorBase Cloned_Object = new stCOSEM_Attribute_DescriptorBase();

            Cloned_Object.COSEM_Class_Id = COSEM_Class_Id;
            Cloned_Object.COSEM_Object_Attribute_Id = COSEM_Object_Attribute_Id;
            Cloned_Object.COSEM_Object_Instance_Id = (byte[])COSEM_Object_Instance_Id.Clone();

            return Cloned_Object;
        }

        #endregion
    }

    /// <summary>
    /// A parameter of the xDLMS attribute-related GET and SET services, used with logical name (LN)
    ///referencing. An attribute is fully identified by the COSEM interface class identifier, the COSEM
    ///object instance identifier (logical name) and the attribute identifier within the given object. GET
    ///and SET services may access the whole attribute, or only a part of it (selective access). 
    ///A GET and SET service may refer to one attribute only, or several attributes. 
    /// </summary>
    public class stCOSEM_Attribute_Descriptor : stCOSEM_Attribute_DescriptorBase, ICloneable
    {
        
        /// <summary>
        /// Flag Either Access_Selection_Parameters is Applied for Current Request
        /// </summary>
        public byte Access_Selection_Parameters;
        /// <summary>
        /// Access Selector Type
        /// </summary>
        public byte Access_Selector;
        /// <summary>
        /// Encoded Access Selector Data
        /// </summary>
        public byte[] Access_Parameters;		// Data of unknown length

        /// <summary>
        /// Constructor
        /// </summary>
        public stCOSEM_Attribute_Descriptor()
        {   
            // Don't know when to initialize Access_Parameters
            Access_Parameters = new byte[5];
        }

        #region IClonable Members
        public object Clone()
        {
            stCOSEM_Attribute_Descriptor Cloned_Object = new stCOSEM_Attribute_Descriptor();

            Cloned_Object.COSEM_Class_Id = COSEM_Class_Id;
            Cloned_Object.COSEM_Object_Attribute_Id = COSEM_Object_Attribute_Id;
            Cloned_Object.Access_Selection_Parameters = Access_Selection_Parameters;
            Cloned_Object.Access_Selector = Access_Selector;
            Cloned_Object.COSEM_Object_Instance_Id = (byte[])COSEM_Object_Instance_Id.Clone();
            Cloned_Object.Access_Parameters = (byte[])Access_Parameters.Clone();

            return Cloned_Object;
        }

        #endregion
    }

    /// <summary>
    /// The Strut stDateBlock_SA is member of <see cref="stSET"/>, 
    /// The Strut is useful in SET_With_Blocking_Service.
    /// </summary>
    public class stDataBlock_SA : ICloneable
    {
        /// <summary>
        /// Flag Last_Block indicate either is Last Block
        /// </summary>
        public byte Last_Block;
        /// <summary>
        /// The Unsigned long incremental block number for current data block
        /// </summary>
        public ulong Block_Number;
        /// <summary>
        /// The encoded APDU to transfer in current data block
        /// </summary>
        public byte[] Raw_Data;	// Data of unknown length

        /// <summary>
        /// Constructor
        /// </summary>
        public stDataBlock_SA()
        {
            // Don't known to init Data
            Raw_Data = new byte[5];
        }
        #region ICloneable Members

        public object Clone()
        {
            stDataBlock_SA Cloned_Object = new stDataBlock_SA();
            Cloned_Object.Last_Block = Last_Block;
            Cloned_Object.Block_Number = Block_Number;
            Cloned_Object.Raw_Data = (byte[])Raw_Data.Clone();

            return Cloned_Object;
        }

        #endregion
    }
    /// <summary>
    /// The stSET is helper strut in Encoding and Decoding Process of SET_Service and SET_WITH_BLOCK Service
    /// </summary>
    public class stSET : ICloneable
    {
        public byte Invoke_Id_Priority;
        public byte Service_Class;
        /// <summary>
        /// SET requestType <see cref="Set_Req_Type"/>
        /// </summary>
        public byte Request_Type;
        /// <summary>
        /// <see cref="stCOSEM_Attribute_Descriptor"/> Attribute Descriptor
        /// </summary>
        public stCOSEM_Attribute_Descriptor COSEM_Attribute_Descriptor;
        /// <summary>
        /// Enocoded Data for SET_Service
        /// </summary>
        public byte[] Data;		// Data of unknown length
        /// <summary>
        /// <see cref="stDataBlock_SA"/>
        /// </summary>
        public stDataBlock_SA DataBlock_SA;
        /// <summary>
        /// Current BlockNumber
        /// </summary>
        public uint Block_number;
        /// <summary>
        /// SET responseType
        /// </summary>
        public byte Response_Type;
        /// <summary>
        /// <see cref="DLMS.Data_Access_Result"/> Set_Service Exec Result
        /// </summary>
        public byte Result;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public stSET()
        {
            // Instantiate Structures
            COSEM_Attribute_Descriptor = new stCOSEM_Attribute_Descriptor();
            DataBlock_SA = new stDataBlock_SA();

            // data of unknwn length
            Data = new byte[5];
        }

        #region ICloneable Members

        public object Clone()
        {
            stSET Cloned_Object = new stSET();

            Cloned_Object.Invoke_Id_Priority = Invoke_Id_Priority;
            Cloned_Object.Service_Class = Service_Class;
            Cloned_Object.Request_Type = Request_Type;
            Cloned_Object.Response_Type = Response_Type;
            Cloned_Object.Result = Result;
            Cloned_Object.Block_number = Block_number;

            Cloned_Object.COSEM_Attribute_Descriptor = (stCOSEM_Attribute_Descriptor)COSEM_Attribute_Descriptor.Clone();
            Cloned_Object.DataBlock_SA = (stDataBlock_SA)DataBlock_SA.Clone();

            Cloned_Object.Data = (byte[])Data.Clone();

            return Cloned_Object;
        }

        #endregion
    } // End class stSET

    #endregion
    //----------------------------------------------------------------------
    
    #region Get

    /// <summary>
    /// The Strut stDataBlock_G is member of <see cref="stGET"/>, 
    /// The Strut is useful in GET_With_Blocking_Service.
    /// The Strut is useful to accumulate all data in set of blocks received from server.
    /// </summary>
    public class stDataBlock_G : ICloneable
    {
        /// <summary>
        /// Flag Last_Block indicate either is Last Block
        /// </summary>
        public byte Last_Block;
        /// <summary>
        /// The Unsigned long incremental block number for current data block
        /// </summary>
        public UInt32 Block_Number;
        /// <summary>
        /// Flag Either data is present or null
        /// </summary>
        public byte Result;
        /// <summary>
        /// The encoded APDU in current data block
        /// </summary>
        public List<byte> Raw_Data;		// Data of unknown length
        /// <summary>
        /// The GET_Service outCome/result <see cref="DLMS.Data_Access_Result"/>
        /// </summary>
        public byte Data_Access_Result;

        /// <summary>
        /// Constructor
        /// </summary>
        public stDataBlock_G()
        {
            // data of unknown length
            Raw_Data = new List<byte>(05);
        }

        #region ICloneable Members

        public object Clone()
        {
            stDataBlock_G Cloned_Object = new stDataBlock_G();

            Cloned_Object.Last_Block = Last_Block;
            Cloned_Object.Block_Number = Block_Number;
            Cloned_Object.Result = Result;
            Cloned_Object.Data_Access_Result = Data_Access_Result;
            
            if (Raw_Data != null)
            {
                Cloned_Object.Raw_Data = new List<byte>(Raw_Data);
            }

            return Cloned_Object;
        }

        #endregion
    }

    /// <summary>
    /// The stGET is helper strut in Encoding and Decoding Process of GET_Service and GET_WITH_BLOCK Service
    /// </summary>
    public class stGET : ICloneable
    {
        public byte Invoke_Id_Priority;
        public byte Service_Class;
        /// <summary>
        /// GET Request Type <see cref="DLMS.GET_Method"/>
        /// </summary>
        public byte Request_Type;
        /// <summary>
        /// COSEM IC(REF) Interface Class Instance Attribute Descriptor <see cref="stCOSEM_Attribute_Descriptor"/>
        /// </summary>
        public stCOSEM_Attribute_Descriptor COSEM_Attribute_Descriptor;
        /// <summary>
        /// Current Block Number
        /// </summary>
        public UInt32 Block_Number;
        /// <summary>
        /// GET Response Type <see cref="DLMS.GET_Method"/>
        /// </summary>
        public byte Response_Type;
        /// <summary>
        /// Flag:Either Data field present or not
        /// </summary>
        public byte Result;
        /// <summary>
        /// <see cref="DLMS.Data_Access_Result"/>
        /// </summary>
        public byte Get_Data_Result;
        /// <summary>
        /// Encode RAW data
        /// </summary>
        public byte[] Data;		                  // Data of unknown length
        /// <summary>
        /// <see cref="DLMS.Data_Access_Result"/>
        /// </summary>
        public byte Data_Access_Result;
        /// <summary>
        /// <see cref="stDataBlock_G"/> helper strut for GET_WITH_BLOCK_Service
        /// </summary>
        public stDataBlock_G DataBlock_G;

        /// <summary>
        /// Constructor
        /// </summary>
        public stGET()
        {
            // Instantiate structures
            COSEM_Attribute_Descriptor = new stCOSEM_Attribute_Descriptor();
            DataBlock_G = new stDataBlock_G();
            // Instantiate byte arrays
            // data of unknown length
            Data = new byte[5];
        }
        #region ICloneable Members

        public object Clone()
        {
            stGET Cloned_Object = new stGET();
            Cloned_Object.Invoke_Id_Priority = Invoke_Id_Priority;
            Cloned_Object.Service_Class = Service_Class;
            Cloned_Object.Request_Type = Request_Type;
            Cloned_Object.Block_Number = Block_Number;
            Cloned_Object.Response_Type = Response_Type;
            Cloned_Object.Result = Result;
            Cloned_Object.Get_Data_Result = Get_Data_Result;
            Cloned_Object.Data_Access_Result = Data_Access_Result;
            if (Cloned_Object.Data != null)
                Cloned_Object.Data = (byte[])Data.Clone();
            Cloned_Object.COSEM_Attribute_Descriptor = (stCOSEM_Attribute_Descriptor)COSEM_Attribute_Descriptor.Clone();
            Cloned_Object.DataBlock_G = (stDataBlock_G)DataBlock_G.Clone();


            Cloned_Object.Data = (byte[])Data.Clone();

            return Cloned_Object;
        }

        #endregion
    } // End class stGET

    #endregion
    //-------------------------------------------------------------------------
    #region Action

    ///<summary>
    ///Method descriptor:A parameter of the xDLMS method related ACTION service, used with logical name (LN) referencing.
    ///A method is fully identified by the COSEM interface class identifier, the COSEM object instance identifier (logical name) 
    ///and the method identifier within the given object. 
    /// </summary>
    public class stCOSEM_Method_Descriptor : ICloneable
    {
        /// <summary>
        /// COSEM Interface Class Id
        /// </summary>
        public ushort COSEM_Class_Id;
        /// <summary>
        /// COSEM Interface Class Instance Id OBIS(Object Identification System) Code
        /// </summary>
        public byte[] COSEM_Object_Instance_Id;	// size = 6
        /// <summary>
        /// Method Id Specification:Particular request attribute Id
        /// </summary>
        public byte COSEM_Object_Method_Id;

        /// <summary>
        /// Constructor
        /// </summary>
        public stCOSEM_Method_Descriptor()
        {
            // Instantiate byte array
            COSEM_Object_Instance_Id = new byte[6];
        }

        #region ICloneable Members

        public object Clone()
        {
            stCOSEM_Method_Descriptor Cloned_Object = new stCOSEM_Method_Descriptor();

            Cloned_Object.COSEM_Class_Id = COSEM_Class_Id;
            Cloned_Object.COSEM_Object_Method_Id = COSEM_Object_Method_Id;
            Cloned_Object.COSEM_Object_Instance_Id = (byte[])COSEM_Object_Instance_Id.Clone();

            return Cloned_Object;
        }

        #endregion
    } // End class stCOSEM_Method_Descriptor

    //--------------------------------------
    /// <summary>
    /// The stResponse_Parameters is helper strut to receive Action/Method Parameter in Decoding Process of Invoke_Service(REF)"/>
    /// </summary>
    public class stResponse_Parameters : ICloneable
    {
        public byte Choice;
        public byte[] Data;		// data of unknown length
        /// <summary>
        /// <see cref="Data_Access_Result"/>
        /// </summary>
        public byte Data_Access_Result;

        public stResponse_Parameters()
        {
            // data of unknwn length
            Data = new byte[5];
        }

        #region ICloneable Members

        public object Clone()
        {
            stResponse_Parameters Cloned_Object = new stResponse_Parameters();

            Cloned_Object.Choice = Choice;
            Cloned_Object.Data_Access_Result = Data_Access_Result;
            Cloned_Object.Data = (byte[])Data.Clone();

            return Cloned_Object;
        }

        #endregion
    }
    //-------------------------------------

    /// <summary>
    /// The stAction_Response is helper strut in Decoding Process of Invoke_Service(REF)"/>
    /// </summary>
    public class stAction_Response : ICloneable
    {
        /// <summary>
        /// Invoke_Service(REF) OutCome Result <see cref="Action_Result"/>
        /// </summary>
        public byte Result;
        /// <summary>
        /// Flag:Either Action Response Parameter/Data Present
        /// </summary>
        public byte Flg_Response_Parameters;
        /// <summary>
        /// <see cref="stResponse_Parameters"/>
        /// </summary>
        public stResponse_Parameters Response_Parameters;
        /// <summary>
        /// Default Constructor
        /// </summary>
        public stAction_Response()
        {
            Response_Parameters = new stResponse_Parameters();
        }

        #region ICloneable Members

        public object Clone()
        {
            stAction_Response Cloned_Object = new stAction_Response();

            Cloned_Object.Result = Result;
            Cloned_Object.Flg_Response_Parameters = Flg_Response_Parameters;
            Cloned_Object.Response_Parameters = (stResponse_Parameters)Response_Parameters.Clone();

            return Cloned_Object;
        }

        #endregion
    }

    /// <summary>
    /// The stACTION is helper strut in Encoding and Decoding Process of Invoke_Service(REF)"/>
    /// </summary>
    public class stACTION : ICloneable
    {
        public byte Invoke_Id_Priority;
        public byte Service_Class;
        /// <summary>
        /// Action/Method RequestType <see cref="ACTION_req_Type"/>
        /// </summary>
        public byte Request_Type;
        /// <summary>
        /// COSEM Method Descriptor(REF) Strut <see cref="stCOSEM_Method_Descriptor"/>
        /// </summary>
        public stCOSEM_Method_Descriptor COSEM_Method_Descriptor;
        /// <summary>
        /// Flag:Action Invoke Parameter Present
        /// </summary>
        public byte Flg_Method_Invocation_Parameters;
        /// <summary>
        /// Encoded Action/Method Invoke Parameters
        /// </summary>
        public byte[] Method_Invocation_Parameters;	// data of unknown length
        /// <summary>
        /// Action/Method ResponseType <see cref="Action_res_Type"/>  
        /// </summary>
        public byte Response_Type;
        /// <summary>
        /// <see cref="stAction_Response"/> helper strut to decode Method/Action Invoke Response
        /// </summary>
        public stAction_Response Action_Response;
        /// <summary>
        /// <see cref="stDataBlock_SA"/> helper Strut for Invoke_With_Block_Service
        /// </summary>
        public stDataBlock_SA DataBlock_SA;
        /// <summary>
        /// Current BlockNumber
        /// </summary>
        public ulong Block_number;

        /// <summary>
        /// Constructor
        /// </summary>
        public stACTION()
        {
            Method_Invocation_Parameters = new byte[] { 0x05, 0x05, 0x05, 0x05, 0x05 };
            COSEM_Method_Descriptor = new stCOSEM_Method_Descriptor();
            Action_Response = new stAction_Response();
            DataBlock_SA = new stDataBlock_SA();
        }

        #region ICloneable Members

        public object Clone()
        {
            stACTION Cloned_Object = new stACTION();

            Cloned_Object.Invoke_Id_Priority = Invoke_Id_Priority;
            Cloned_Object.Service_Class = Service_Class;
            Cloned_Object.Request_Type = Request_Type;
            Cloned_Object.Response_Type = Response_Type;
            Cloned_Object.Block_number = Block_number;
            Cloned_Object.Flg_Method_Invocation_Parameters = Flg_Method_Invocation_Parameters;

            Cloned_Object.COSEM_Method_Descriptor = (stCOSEM_Method_Descriptor)COSEM_Method_Descriptor.Clone();
            Cloned_Object.Action_Response = (stAction_Response)Action_Response.Clone();
            Cloned_Object.DataBlock_SA = (stDataBlock_SA)DataBlock_SA.Clone();

            Cloned_Object.Method_Invocation_Parameters = (byte[])Method_Invocation_Parameters.Clone();

            return Cloned_Object;
        } // End Clone

        #endregion
    } // End Class stACTION

    #endregion
    //-------------------------------------------------------------------------
    #region Association Release

    /// <summary>
    /// Class stASSOCIATION_RELEASE helper strut in Encoding and Decoding Process of ARLRQ_Service
    /// </summary>
    public class stASSOCIATION_RELEASE : ICloneable
    {
        //Allien_type:P Use_RLRQ_RLRE;	
        public byte Reason;

        public stProposed_xDLMS_Context Proposed_xDLMS_Context;
        public stNegotiated_xDLMS_Context Negotiated_xDLMS_Context;

        public byte Local_Or_Remote;
        public byte Result;
        public byte Failure_Type;
        public byte User_Information;

        public stASSOCIATION_RELEASE()
        {
            // Instantiate structures
            Proposed_xDLMS_Context = new stProposed_xDLMS_Context();
            Negotiated_xDLMS_Context = new stNegotiated_xDLMS_Context();
        }

        #region ICloneable Members

        public object Clone()
        {
            stASSOCIATION_RELEASE Cloned_Object = new stASSOCIATION_RELEASE();

            Cloned_Object.Reason = Reason;
            Cloned_Object.Result = Result;
            Cloned_Object.Failure_Type = Failure_Type;
            Cloned_Object.User_Information = User_Information;

            Cloned_Object.Proposed_xDLMS_Context = (stProposed_xDLMS_Context)Proposed_xDLMS_Context.Clone();
            Cloned_Object.Negotiated_xDLMS_Context = (stNegotiated_xDLMS_Context)Negotiated_xDLMS_Context.Clone();

            return Cloned_Object;
        } // End Clone

        #endregion
    } // End class stASSOCIATION_RELEASE

    #endregion
    ///-------------------------------------------------------------------------
    #region Event Notification
    
    /// <summary>
    /// The stEVENT_NOTIFICATION is helper strut in Encoding and Decoding Process of Event Notification DLMS Service
    /// </summary>
    public class stEVENT_NOTIFICATION : ICloneable
    {
        public byte[] Time;
        public byte Flg_Time;

        public stCOSEM_Attribute_DescriptorBase COSEM_Attribute_Descriptor;
        public byte[] Attribute_Value;

        public stEVENT_NOTIFICATION()
        {
            // Instantiate Structures
            COSEM_Attribute_Descriptor = new stCOSEM_Attribute_DescriptorBase();

            Time = new byte[12];
            Attribute_Value = new byte[06];  // unknwn length
        }

        #region ICloneable Members

        public object Clone()
        {
            stEVENT_NOTIFICATION Cloned_Object = new stEVENT_NOTIFICATION();

            if (Time != null)
                Cloned_Object.Time = (byte[])Time.Clone();
            Cloned_Object.Flg_Time = Flg_Time;

            // Perform Deep Copy
            if (Attribute_Value != null)
                Cloned_Object.Attribute_Value = (byte[])Attribute_Value.Clone();

            if (COSEM_Attribute_Descriptor != null)
                Cloned_Object.COSEM_Attribute_Descriptor = (stCOSEM_Attribute_DescriptorBase)COSEM_Attribute_Descriptor.Clone();

            return Cloned_Object;
        } // End Clone

        #endregion
    }
    // End class stEVENT_NOTIFICATION

    public class St_EventNotify : ICloneable
    {
        #region Data Members

        private StDateTime _CaptureTime;
        private bool _Flag_Time;
        private StOBISCode _COSEM_Object_Instance_Id;
        private byte _COSEM_Object_Attribute_Id;
        private object _Attribute_Value;

        #endregion

        #region Constructors

        public St_EventNotify(StOBISCode _COSEM_Object_Instance_Id,
                              byte _COSEM_Object_Attribute_Id,
                              StDateTime _CaptureTime)
            : this()
        {
            this._COSEM_Object_Instance_Id = _COSEM_Object_Instance_Id;
            this._COSEM_Object_Attribute_Id = _COSEM_Object_Attribute_Id;
            this._CaptureTime = _CaptureTime;
        }

        public St_EventNotify()
        {
            this._COSEM_Object_Instance_Id = Get_Index.Dummy;
            this._COSEM_Object_Attribute_Id = 0;

            this._Flag_Time = false;
            this._CaptureTime = new StDateTime();

            this._Attribute_Value = null;
        }

        public St_EventNotify(St_EventNotify Notifier)
        {
            this._COSEM_Object_Instance_Id = Notifier._COSEM_Object_Instance_Id;
            this._COSEM_Object_Attribute_Id = Notifier._COSEM_Object_Attribute_Id;

            this._Flag_Time = Notifier._Flag_Time;
            if (Notifier._CaptureTime != null)
                this._CaptureTime = (StDateTime)Notifier._CaptureTime.Clone();

            this._Attribute_Value = Notifier._Attribute_Value;
        }

        #endregion

        #region Properties

        public StDateTime CaptureTime
        {
            get { return _CaptureTime; }
            set { _CaptureTime = value; }
        }

        public bool Flag_Time
        {
            get { return _Flag_Time; }
            set { _Flag_Time = value; }
        }

        public StOBISCode COSEM_Object_OBISCode
        {
            get { return _COSEM_Object_Instance_Id; }
            set { _COSEM_Object_Instance_Id = value; }
        }

        public byte COSEM_Object_Attribute_Id
        {
            get { return _COSEM_Object_Attribute_Id; }
            set { _COSEM_Object_Attribute_Id = value; }
        }

        public object Attribute_Value
        {
            get { return _Attribute_Value; }
            set { _Attribute_Value = value; }
        }

        #endregion

        #region ICloneable Members

        object ICloneable.Clone()
        {
            return new St_EventNotify(this);
        }

        #endregion

        public override string ToString()
        {
            // Append Base Object Values using deleter Char(,)
            StringBuilder strVal = new StringBuilder();
            strVal.AppendFormat("StEvent Notify");
            strVal.AppendFormat("OBIS Code Received:{0}:{1}", COSEM_Object_OBISCode);
            strVal.AppendFormat(",OBIS Index:{0}  {1}", COSEM_Object_OBISCode.OBISIndex);
            strVal.AppendFormat(",Attribute Request:{0}", COSEM_Object_Attribute_Id);
            strVal.AppendFormat(",Class Id:{0}", COSEM_Object_OBISCode.ClassId);
            strVal.AppendFormat(",Value:{0}", Attribute_Value);

            return strVal.ToString();
        }
    }

    #endregion

    /// <summary>
    /// Unit_Scaler Strut define the <see cref="units"/> and Scaler value for extended Registers IC_3 and IC_4 etc
    /// </summary>
    public struct Unit_Scaler
    {
        /// <summary>
        /// <see cref="units"/>
        /// </summary>
        public units Unit;
        public sbyte Scaler;
    }

} // End namespace DLMS_Structures
