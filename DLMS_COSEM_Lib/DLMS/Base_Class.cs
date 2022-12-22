using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS.LRUCache;
using DLMS.Comm;

namespace DLMS
{
    /// <summary>
    /// DLMS_COSEM template Class sample description
    /// Definition DLMS_COSEM Data MODEL:All COSEM_Instance available in metering Device with standard OBIS Code as per COSEM Specification
    /// Definition DLSM_COMSEM_Object Specification: StOBISCode,Class_Id
    /// Definition Attribute Specification:EncodingAttribute,DecodingAttribute,EncodingType,EncodingSubType,AccessSelector
    /// Definition Method/Action Specification:MethodInvokeId,Method_ParametersFlag(If Arguments exists):EncodingType,EncodingSubType
    /// Definition Access Rights Association:
    /// </summary>
    abstract public class Base_Class : ICloneable
    {
        #region DataMembers

        /// <summary>
        /// Identify Maximum Numbers Attribute supported by the DLMS_COSEM Object</summary>
        /// <remarks>
        /// template classes,later on this Attribute_No value information to validate EncodingAttribute,DecodingAttribute,DecodingResult and OBISCodeRights
        /// in specific DLMS_Instance interface objects </remarks>

        public readonly byte Attribs_No;
        /// <summary>
        /// Identify Maximum Methods/rountine Numbers supported by the DLMS_COSEM Object
        /// </summary>
        /// <remarks>
        /// this Methods_No value information to validate MethodInvokeId and OBISCodeRights 
        /// in specific DLMS_COSEM Instance interface objects
        /// </remarks>
        public readonly byte Methods_No;

        private readonly StOBISCode Index;
        private ushort? ownerId;
        private IAccessSelector accessSelector;
        protected OBISCodeRights rights;
        // Decoding Variables 
        protected byte _Attribute_to_decode;
        protected DecodingResult[] accessResult = null;
        // Encoders Variables
        protected byte _EncodingAttribute;
        protected DataTypes _EncodingType;
        protected DataTypes _EncodingSubType;
        // Action Parameters
        private byte methodInvokeId;
        private Data_Access_Result method_data_result;
        private bool method_ParametersFlag;
        internal List<byte> EncodedRaw;
        private byte _COSEM_EncodeDecode_OPT = 1;

        #endregion

        #region Properties

        /// <summary>Get/Set access rights for the current instance</summary>
        /// <remarks>
        /// Collection of Access rights for current DLMS_COSEM interface object, these rights 
        /// includes access rights for object attributes,methods and data selectors details.  
        /// The OBISCodeRights loaded from SAP_Table for Current association with meter, the OBISCodeRights is used to
        /// specify the access level for Attributes , for methods/Action and Selectors(for GET_Service) Of DLMS_COMSEM instance.
        /// <code>
        /// public enum Attrib_Access_Modes : byte
        /// {
        ///     No_Access = 0,
        ///     Read_Only,
        ///     Write_Only,
        ///     Read_Write,
        ///     Authenticated_Read_Only,
        ///     Authenticated_Write_Only,
        ///     Authenticated_Read_Write,
        /// }
        /// 
        ///  public enum Method_Access_Modes : byte
        /// {
        ///     No_Access = 0,
        ///     Access,
        ///     Authenticated_Access,
        /// }
        /// 
        ///  public enum SelectiveAccessType : byte
        /// {
        ///     Not_Applied = 0,
        ///     Range_Descripter = 1,
        ///     Entry_Descripter = 2,
        ///     Both_Types = 3
        /// }
        /// 
        /// </code>
        /// </remarks>
        public OBISCodeRights Rights
        {
            get { return rights; }
            set { rights = value; }
        }

        /// <summary>Get the Result(<see cref="DecodingResult"/>) after Validating access rights</summary>
        /// <remarks>
        /// AccessResults the success or error status codes retrieved while reading the DLMS_COSEM instance from the meter. 
        /// The Access Results information is collected during current object decoding process. These status codes are
        /// further utilize during the retrieval of attributes Value/Data of DLMS_COSEM object.
        /// </remarks>
        public DecodingResult[] AccessResults
        {
            get { return accessResult; }

        }

        /// <summary>
        /// Get the OBIS(<see cref="StOBISCode"/>) code for current instance
        /// </summary>
        /// <remarks>
        /// OBIS_CODE is readonly Public Property to define Logical_Name for specific DLMS_COSEM Instance interface objects. 
        /// The OBIS_CODE is 6 byte long logical Name in format[OBIS_CODE_A][OBIS_CODE_B][OBIS_CODE_C][OBIS_CODE_E][OBIS_CODE_F].
        /// </remarks>
        public byte[] OBIS_CODE
        {
            get
            {
                return OBISIndex.OBISCode;
            }
        }

        /// <summary>
        /// Get the class ID of the current instance</summary>
        /// <remarks>
        /// Class_ID is unique identifier for DLMS_COSEM Template interface Class. each class has its own unique class ID in DLMS/COSEM Context 
        /// This Class_ID is 2 byte ushort Number to identify the object of specific class.
        /// </remarks>
        public ObjectType ObjectType
        {
            get { return OBISIndex.ObjectType; }
        }

        /// <summary>
        /// Get the class ID of the current instance</summary>
        /// <remarks>
        /// Class_ID is unique identifier for DLMS_COSEM Template interface Class. each class has its own unique class ID in DLMS/COSEM Context 
        /// This Class_ID is 2 byte ushort Number to identify the object of specific class.
        /// </remarks>
        public UInt16 Class_ID
        {
            get { return OBISIndex.ClassId; }
        }

        /// <summary>Get the composite Code represent a single Data Quantity </summary>
        /// <remarks>
        /// INDEX is ,custom Composite storage structure, used in Library Code to define Logical_Name and COSEM Template Class_id: information
        /// for specific DLMS_COSEM Instance. The Index is also used as KEY for SAP_Table data structure which is used to retrieve access rights defined for DLMS_COSEM Instance interface object. 
        /// The INDEX structure take 8 byte memory as Unsigned Long Integer number,
        /// 6 byte for Logical Name(one byte for each OCTECT) and two bytes for Class_id.
        /// The INDEX format is [Class_id][OBIS_CODE_A][OBIS_CODE_B][OBIS_CODE_C][OBIS_CODE_E][OBIS_CODE_F].
        /// for DLMS Data MODEL Object_Specification 
        /// </remarks>
        public Get_Index INDEX
        {
            get
            {
                return OBISIndex.OBISIndex;
            }
        }

        /// <summary>
        /// OwnerId is nullable public property used as part of KeyIndexer composite Structure
        /// </summary>
        public ushort? OwnerId
        {
            get { return ownerId; }
            set { ownerId = value; }
        }

        /// <summary>Get <see cref="StOBISCode"/> structure </summary>
        /// <remarks>
        /// StOBISCode is ,custom Composite storage structure, used in Library Code to define Logical_Name and COSEM Template Class_id: information
        /// for specific DLMS_COSEM Instance interface objects.The StOBISCode structure take 8 byte memory as Unsigned Long Integer number,
        /// 6 byte for Logical Name(one byte for each OCTECT) and two bytes for Class_id.
        /// The StOBISCode format is [Class_id][OBIS_CODE_A][OBIS_CODE_B][OBIS_CODE_C][OBIS_CODE_E][OBIS_CODE_F].
        /// for DLMS Data MODEL Object_Specification 
        /// </remarks>
        public StOBISCode OBISIndex
        {
            get { return Index; }
        }

        /// <summary>
        /// Get the Key to identify a particular instance of a particular class in DLMS/COSEM object dictionary
        /// </summary>
        /// <remarks>
        /// The KeyIndexer is readonly ,custom Composite Structure, Public Property to Index KEY the for specific DLMS_COSEM Instance interface objects
        /// for storage and retrieval from different INDEX based storage structures, KEY_VALUE Pair Data Dictionary, HASH Tables.   
        /// </remarks>
        public KeyIndexer KeyIndexer
        {
            get
            {
                try
                {
                    return new KeyIndexer(OBISIndex, OwnerId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Get/Set decoding attribute which have to be manipulate later in read or write process
        /// </summary>
        /// <remarks>
        /// The DecodingAttribute for particular attribute Identity of DLMS object retrieved during the last data read request.
        /// The DecodingAttribute information is between 0 and Max Attribs_No of DLMS_COSME template class, if read request is
        /// for all attribute of Object then this Property shall be 0. 
        /// </remarks>
        public byte DecodingAttribute
        {
            get { return _Attribute_to_decode; }
            set
            {
                if (value > Attribs_No && Attribs_No != 0)
                    throw new DLMSException(String.Format("Invalid Attribute To Decode {0:X2},In Class ID {1:X2} (Error Code:{3})", value,
                        this.Class_ID, (int)DLMSErrors.Invalid_DecodingAttribute));
                _Attribute_to_decode = value;
            }
        }

        /// <summary>
        /// Get/Set Access Selector define read or write Range <see cref="EntryDescripter"/>
        /// </summary>
        /// <remarks>
        /// The xDLMS services Read, Write, UnconfirmedWrite (used with SN referencing) and GET, SET (used with LN referencing) typically reference the entire attribute.
        /// However, for certain attributes selective access to just a part of the attribute may be provided. The part of the attribute is identified by specific selective access parameters.
        /// These are defined as part of the attribute specification.</remarks>
        public IAccessSelector AccessSelector
        {
            get { return accessSelector; }
            set { accessSelector = value; }
        }

        /// <summary>
        /// Get/Set DataTypes enum, the EncodingType(<see cref="DataTypes"/>) is used to define Choice(DataTypes enum) for attribute_Specfication.
        /// </summary>
        /// <remarks>
        ///  For attribute_Specfication EncodingType,EncodingSubType Properties should be initialized to work properly.
        /// </remarks>
        /// <example>
        /// <code>
        /// //To Set long64-unsigned data type using Class_id:01 the following Code Piece should be used to initialize DLSM_Intance
        /// 
        /// StOBISCode OBIS = GET_INDEX.Major_Alarm_Counter;
        /// Base_Class  Comm_Obj = GetSAPEntry(OBIS);
        /// Comm_Obj.EncodingAttribute = 0x02;
        /// Comm_Obj.EncodingType = DataTypes._A15_long_64_unsigned;
        /// 
        /// //Now use Initialized Base_Class Comm_Obj for SET Operation
        /// Data_Access_Result SET_OUTCome = ApplicationProcess_Controller.SET(Comm_Obj);
        /// //Now Verify SET_OUTCome variable for SET function success/failure 
        /// </code>
        /// </example>
        public DataTypes EncodingType
        {
            get { return _EncodingType; }
            set { _EncodingType = value; }
        }

        public DataTypes DecodingType
        {
            get { return _EncodingType; }
            set { _EncodingType = value; }
        }


        /// <summary>
        /// Get/Set DataTypes enum, the EncodingSubType (<see cref="DataTypes"/>) for iterative data structure
        /// </summary>
        /// <example>
        /// <code>
        /// EncodingSubType is Only initialized for attribute_Specfication when EncodingType is Iterative data structure
        /// To Set long64-unsigned data type using Class_id:01 the following Code Piece should be used to initialize DLSM_Intance
        /// 
        /// StOBISCode OBIS = GET_INDEX.Major_Alarm_Counter;
        /// Base_Class  Comm_Obj = GetSAPEntry(OBIS);
        /// Comm_Obj.EncodingAttribute = 0x02;
        /// Comm_Obj.EncodingType = DataTypes._A01_array;
        /// Comm_Obj.EncodingSubType = DataTypes._A15_long_64_unsigned;
        /// 
        /// //Now use Initialized Base_Class Comm_Obj for SET Operation
        /// Data_Access_Result SET_OUTCome = ApplicationProcess_Controller.SET(Comm_Obj);
        /// //Now Verify SET_OUTCome variable for SET function success/failure 
        /// </code>
        /// </example>
        public DataTypes EncodingSubType
        {
            get { return _EncodingSubType; }
            set { _EncodingSubType = value; }
        }

        public DataTypes DecodingSubType
        {
            get { return _EncodingSubType; }
            set { _EncodingSubType = value; }
        }


        /// <summary> Get/Set the encoding attribute (Attribute ID) </summary>
        /// <remarks>
        /// It is used to retrieve the particular attribute in DLMS_COSEM object for current data read request.
        /// The EncodingAttribute information is between 0 and Max Attribs_No of DLMS_COSME template class, if read request is
        /// for all attribute of Object then this Property shall be 0. 
        /// </remarks>
        public byte EncodingAttribute
        {
            get { return _EncodingAttribute; }
            set
            {
                if ((value > Attribs_No && Attribs_No != 0) || value == 0)
                    throw new DLMSException(String.Format("Invalid Attribute To Encode 0x{0:X2},In Class ID 0x{1:X2} (Error Code:{3})", value,
                                            this.Class_ID, (int)DLMSErrors.Invalid_EncodingAttribute));
                _EncodingAttribute = value;
            }
        }

        /// <summary>
        /// Get/Set Method invoke ID to encode a Invoking Request
        /// </summary>
        /// <remarks>
        /// Method/Action Id for DLMS_COSEM Interface Object which is used to invoke particular Method in DLMS_COSEM object for action request.
        /// The MethodInvokeId information is between 1 and Max Methods_No of DLMS_COSME object. 
        /// </remarks>
        public byte MethodInvokeId
        {
            get
            {
                return methodInvokeId;
            }
            set
            {
                if (value > Methods_No && value == 0)
                    throw new DLMSException(String.Format("Invalid Method To Invoke 0x{0:X2},In Class ID 0x{1:X2} (Error Code:{3})", value,
                        this.Class_ID, (int)DLMSErrors.Invalid_MethodInvokeId));
                methodInvokeId = value;
            }
        }

        /// <summary>
        ///  Get/Set the flag to identify Method is parameterized or non-parameterized which is being invoke.
        /// </summary>
        public bool Method_ParametersFlag
        {
            get { return method_ParametersFlag; }
            set { method_ParametersFlag = value; }
        }

        /// <summary>
        /// Specify outcome/result for the last action/Method request.
        /// </summary>
        /// <returns>
        /// Data_Access_Result enumerated number to represent the last action/Method outcome results. The Data_Access_Result
        ///  is used to notify the Successful Method execution or error status_codes being returned from DLMS_COSEM device.
        /// </returns>
        public Data_Access_Result Method_Data_Result
        {
            get { return method_data_result; }
            set { method_data_result = value; }
        }

        public bool EnableCOSEM_EncodeDecode_OPT
        {
            get { return Convert.ToBoolean(_COSEM_EncodeDecode_OPT); }
            set { _COSEM_EncodeDecode_OPT = Convert.ToByte(value); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        /// Obselete
        /// </remarks>
        /// <param name="class_id">To specify DLMS_COSEM template Class Id</param>
        /// <param name="attributes">To specify DLMS_COSEM template Class Max Number Of Attribute Supported</param>
        /// <param name="methods">To specify DLMS_COSEM template Class Max Number Of Methods Supported</param>
        /// <param name="In">GET_Index to specify Logical Name,Class_id for Current Object</param>
        /// <param name="Ob_Co">Ob_Co is 6 byte array form Logical Name Specification</param>
        /// <param name="Total_Associations">The Total_Association to specify Max_No of Association can be created 
        /// for SAP table structure specification</param>
        public Base_Class(UInt16 class_id, byte attributes, byte methods, Get_Index In, byte[] Ob_Co, UInt16 Total_Associations)
            : this(class_id, Ob_Co)
        {
            Attribs_No = attributes;
            Methods_No = methods;
            Index = In;
            accessResult = new DecodingResult[Attribs_No];
            Rights = new OBISCodeRights();
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        private Base_Class()
        {
            Index = Get_Index.Dummy;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        /// Obselete
        /// </remarks>
        /// <param name="class_id">To specify DLMS_COSEM template Class Id</param>
        /// <param name="Ob_Co">Ob_Co is 6 byte array form Logical Name Specification</param>
        public Base_Class(UInt16 class_id, byte[] Ob_Co)
            : this()
        {
            if (Ob_Co == null || Ob_Co.Length != 6)
                throw new DLMSException("Invalid OBIS Code To Initialize_Base_Class_Contructure");

            StOBISCode t = StOBISCode.ConvertFrom(Ob_Co);
            t.ClassId = class_id;
            Index = t;

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        /// Obselete
        /// </remarks>
        /// <param name="class_id">To specify DLMS_COSEM template Class Id</param>
        /// <param name="attributes">To specify DLMS_COSEM template Class Max Number Of Attribute Supported</param>
        /// <param name="methods">To specify DLMS_COSEM template Class Max Number Of Methods Supported</param>
        /// <param name="Ob_Co">Ob_Co is 6 byte array form Logical Name Specification</param>
        public Base_Class(UInt16 class_id, byte attributes, byte methods, byte[] Ob_Co)
            : this(class_id, Ob_Co)
        {
            Attribs_No = attributes;
            Methods_No = methods;

            accessResult = new DecodingResult[Attribs_No];
            for (int index = 0; index < accessResult.Length; index++)
            {
                accessResult[index] = DecodingResult.DataNotPresent;
            }
            Rights = new OBISCodeRights();
            Rights.OBISIndex = OBISIndex;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        /// Most Commonly used in Constructor in Derived Classes for Chaining
        /// </remarks>
        /// <param name="OBISCode">To specify DLMS_COSEM Logical Name, OBIS Code,Class_Id</param>
        /// <param name="attributes">To specify DLMS_COSEM template Class Max Number Of Attribute Supported</param>
        /// <param name="methods">To specify DLMS_COSEM template Class Max Number Of Methods Supported</param>
        public Base_Class(StOBISCode OBISCode, byte attributes, byte methods)
        {
            Attribs_No = attributes;
            Methods_No = methods;
            Index = OBISCode;
            accessResult = new DecodingResult[Attribs_No];
            Rights = new OBISCodeRights();
            Rights.OBISIndex = OBISCode;
        }

        /// <summary>
        /// Copy Constructor Base_Class
        /// </summary>
        /// <param name="obj">The Base_Class DLMS_COSEM Instance to be copied</param>
        public Base_Class(Base_Class obj)
            : this(obj.Class_ID, obj.Attribs_No, obj.Methods_No, obj.OBIS_CODE)
        {
            /// Copy Remaining DataItems
            Index = obj.OBISIndex;
            if (obj.AccessSelector != null)
                accessSelector = (IAccessSelector)obj.AccessSelector.Clone();
            if (obj.Rights != null)
                Rights = (OBISCodeRights)obj.Rights.Clone();
            _Attribute_to_decode = obj.DecodingAttribute;

            _EncodingAttribute = obj.EncodingAttribute;
            _EncodingType = obj.EncodingType;
            _EncodingSubType = obj.EncodingSubType;
        }

        #endregion

        #region Decoders/Encoders

        /// <summary>
        /// Over-ridable method to decode GET Command Attribute data
        /// </summary>
        /// <param name="Data">The byte[] Buffer to be decoded</param>
        /// <param name="array_traverse">array_traverse is indexer for current index</param>
        /// <param name="length">length argument specify the decoded-data length</param>
        public virtual void Decode_Data(ref byte[] Data, ref int array_traverse, int length)
        {
            //UInt16 array_traverse = 0;
            byte[] Obis_code_recieved = new byte[6];
            try
            {
                for (int index = 0; index < AccessResults.Length; index++)
                    AccessResults[index] = DecodingResult.DataNotPresent;
                //------------------------------------------------------
                DecoderAttribute_0(ref Data, ref array_traverse, "BaseClass_DataDecoder");
                DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, "BaseClass_DataDecoder");
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                {
                    throw new DLMSDecodingException("Error occurred while decoding data", "BaseClass_DataDecoder", ex);
                }
            }
        }

        /// <summary>
        /// Over-ridable method to decode GET Command Attribute data
        /// </summary>
        /// <param name="Data">The byte[] Buffer to be decoded</param>
        public virtual void Decode_Data(ref byte[] Data)
        {
            try
            {
                int array_traverse = 0;
                Decode_Data(ref Data, ref array_traverse, Data.Length);
                DLMS_Common.Data_Array_resizer(ref Data, array_traverse);
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                {
                    throw new DLMSDecodingException(String.Format("Error occurred while decoding data {0}_{1}", OBISIndex, OBISIndex.OBISIndex),
                                                    "BaseClass_DataDecoder", ex);
                }
            }
        }

        /// <summary>
        /// Over-ridable method to encode data for SET command
        /// </summary>
        /// <returns>
        /// byte[] encoded buffer for DLMS_COSEM instance
        /// </returns>
        public virtual byte[] Encode_Data()
        {
            EncodedRaw = new List<byte>();
            EncodedRaw.Add((byte)DataTypes._A00_Null);
            return EncodedRaw.ToArray<byte>();
        }

        /// <summary>
        ///  Over-ridable method to decode Method return result parameters
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="array_traverse"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public virtual int Decode_Parameters(ref byte[] Data, ref int array_traverse, int length)
        {
            //Do Nothing For Base_Class 
            return array_traverse;
        }

        /// <summary>
        /// Over-ridable method to decode Method return result parameters
        /// </summary>
        /// <param name="Data"></param>
        public virtual void Decode_Parameters(ref byte[] Data)
        {
            try
            {
                int array_traverse = 0;
                Decode_Parameters(ref Data, ref array_traverse, Data.Length);
                DLMS_Common.Data_Array_resizer(ref Data, array_traverse);
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                {
                    throw new DLMSDecodingException(String.Format("Error occurred while decoding data parameters {0}_{1}",
                        OBISIndex, OBISIndex.OBISIndex),
                        "BaseClass_DataDecoder", ex);
                }
            }
        }

        /// <summary>
        /// Over-ridable method to encode Action Parameter data if exists
        /// </summary>
        /// <returns></returns>
        public virtual byte[] Encode_Parameters()
        {
            return null;
        }

        /// <summary>
        /// DecoderAttribute_0 decodes bytes for Attribute zero of DLMS classes,method verifies the startup data type should be 
        /// structure and no of received attributes should be equal to Total Attributes in DLMS class
        /// </summary>
        /// <param name="Data">byte []Data,Binary Data Buffer</param>
        /// <param name="array_traverse">ushort array_traverse,Indexer to Binary Buffer</param>
        /// <param name="ExtDecoderName">String ExtDecoderName,Name of Decoder of Child Classes</param>
        protected byte DecoderAttribute_0(ref byte[] Data, ref int array_traverse, String ExtDecoderName)
        {
            byte Attribs_No_Recv = 0;
            if (DecodingAttribute == 0x00)
            {
                byte current_char = Data[array_traverse++];
                Attribs_No_Recv = Data[array_traverse++];

                // 1st element must be structure
                if (current_char != (byte)DataTypes._A02_structure)
                {
                    // Generate Error and return
                    SetAttributeDecodingResult(1, DecodingResult.DecodingError);
                    throw new DLMSDecodingException(String.Format("First element should be 02 (Structure) (Error Code:{0}) ",
                        (int)DLMSErrors.Invalid_Type_MisMatch), ExtDecoderName);
                }
                if (Attribs_No_Recv == 0 || !(Attribs_No_Recv <= Attribs_No))
                {
                    // Generate Error and return
                    SetAttributeDecodingResult(1, DecodingResult.DecodingError);
                    throw new DLMSDecodingException(String.Format("Wrong number of Attribute Received (Error Code:{0})",
                        (int)DLMSErrors.Invalid_DecodingAttribute), ExtDecoderName);
                }
            }
            return Attribs_No_Recv;
        }

        /// <summary>
        /// EncoderAttribute_0 encodes bytes for Attribute zero of DLMS classes,
        /// </summary>
        /// <remarks>
        /// EncoderAttribute_0 method verifies the startup data type should be structure and no of encoded attributes 
        /// should be equal to Total <see cref="Attribs_No"/> in DLMS_COSEM template class
        /// </remarks>
        protected void EncoderAttribute_0()
        {
            try
            {
                if (EncodingAttribute == 0)
                {
                    throw new DLMSEncodingException(String.Format("Error Encoding With Attribute 0 (Error Code:{0})",
                        (int)DLMSErrors.Invalid_EncodingAttribute), "EncodingAttribute_0");
                }
            }
            catch (Exception ex)
            {
                throw new DLMSEncodingException(String.Format("Error Encoding With Attribute 0 (Error Code:{0})",
                    (int)DLMSErrors.Invalid_EncodingAttribute), "EncodingAttribute_0");
            }
        }

        /// <summary>
        /// Decodes the OBIS Code of DLMS_COSEM instance
        /// </summary>
        /// <remarks>The DecoderLogicalName verifies the OBIS Code received already exists in current SAP Context ,
        /// also checks the decoded OBIS Code matches the DLMS_COSEM instance OBIS being decoded.
        /// </remarks>
        /// <param name="Data">byte []Data,Binary Data Buffer</param>
        /// <param name="array_traverse">ushort array_traverse,Indexer to Binary Buffer</param>
        /// <param name="Obis_code_received">byte []Obis_code_received,ref array that contains OBIS Code Decoded</param>
        /// <param name="ExtDecoderName">String ExtDecoderName,Name of Decoder of Child Classes</param>
        protected void DecoderLogicalName(ref byte[] Data, ref int array_traverse, ref byte[] Obis_code_received, String ExtDecoderName)
        {
            if (DecodingAttribute == 0x01 || DecodingAttribute == 0x00)
            {
                try
                {
                    if (Data[array_traverse++] == (byte)DataTypes._A00_Null)
                    {
                        if (!IsAttribReadable(0x01))
                        {
                            SetAttributeDecodingResult(0x01, DecodingResult.NoAccess);
                        }
                        else
                        {
                            SetAttributeDecodingResult(0x01, DecodingResult.DecodingError);
                        }
                        return;
                    }
                    if (Obis_code_received == null)              //Instantiate OBIS-Code Array
                        Obis_code_received = new byte[6];
                    array_traverse--;
                    Obis_code_received = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse, Data.Length);
                    StOBISCode CodeReceived = StOBISCode.ConvertFrom(Obis_code_received);
                    CodeReceived.ClassId = OBISIndex.ClassId;
                    if (!CodeReceived.Equals(OBISIndex))
                    {
                        SetAttributeDecodingResult(1, DecodingResult.DLMSError);
                        throw new DLMSDecodingException(String.Format("{0}_{1} Wrong OBIS Code (Error Code:{2})", CodeReceived,
                            OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_OBISCode),
                            ExtDecoderName)
                        { HelpLink = "Error:WR76" };
                    }
                    SetAttributeDecodingResult(1, DecodingResult.Ready);
                }
                catch (Exception ex)
                {
                    if (!IsAttribReadable(0x01))
                    {
                        SetAttributeDecodingResult(0x01, DecodingResult.NoAccess);
                    }
                    else
                    {
                        SetAttributeDecodingResult(0x01, DecodingResult.DecodingError);
                    }

                    if (ex is DLMSDecodingException)
                        throw ex;
                    else
                        throw new DLMSDecodingException(String.Format("{0}_{1} Wrong OBIS Code (Error Code:{2})", OBISIndex, OBISIndex.OBISIndex,
                             (int)DLMSErrors.Invalid_OBISCode), ExtDecoderName)
                        { HelpLink = "Error:WR76" };
                }
            }
        }

        /// <summary>
        /// Encodes the OBIS Code of DLMS_COSEM instance 
        /// </summary>
        protected void EncoderLogicalName(bool Nullable)
        {
            try
            {
                if (EncodingAttribute == 0 || EncodingAttribute == 1)
                {
                    if (Nullable)                                            //Send Nullable
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    else
                    {
                        EncodedRaw.Add((byte)DataTypes._A09_octet_string);
                        EncodedRaw.AddRange(OBIS_CODE);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DLMSEncodingException(String.Format("Error Encoding Logical Name (Error Code:{0})",
                    (int)DLMSErrors.Invalid_OBISCode), "EncodingLogicalName_Encoder");
            }
        }

        /// <summary>
        /// EncoderLogicalName encodes the OBIS Code of DLMS_COSEM instance 
        /// </summary>
        protected void EncoderLogicalName()
        {
            try
            {
                EncoderLogicalName(true);
            }
            catch (Exception ex)
            {
                throw new DLMSEncodingException(String.Format("Error Encoding Logical Name (Error Code:{0})",
                    (int)DLMSErrors.Invalid_OBISCode), "EncodingLogicalName_Encoder");
            }
        }

        #endregion

        #region Member Methods

        /// <summary>
        /// Helper Method to retrieve the DecodingResult value for particular attribute No
        /// </summary>
        /// <param name="attributeNo">The argument specify the attributeNo value,the attributeNo should be between 1 and Max attribute_No</param>
        /// <returns>
        /// DecodingResult the enumerated value for result
        /// <code>
        /// public enum DecodingResult : byte
        /// {
        ///    DataNotPresent = 0x0,
        ///    NoAccess,
        ///    NotInSAPList,
        ///    DecodingError,
        ///    DLMSError,
        ///    Ready
        /// }
        /// </code>
        /// </returns>

        public DecodingResult GetAttributeDecodingResult(int attributeNo)
        {
            if (attributeNo != 0)
            {
                if (attributeNo <= Attribs_No)    //Attribute Decoded Le
                {
                    return AccessResults[attributeNo - 1];
                }
                else
                    return DecodingResult.DataNotPresent;       //Default
            }
            else
            {
                // return DecodingResult.Ready;
                DecodingResult res = DecodingResult.DataNotPresent;
                foreach (var item in AccessResults)
                {
                    if (item == DecodingResult.Ready)
                        res = item;
                    else if (item == DecodingResult.NoAccess)
                        continue;
                    else
                    {
                        res = item;
                        break;
                    }
                }
                return res;
            }
        }

        /// <summary>
        /// Helper Method to set/assign the DecodingResult value for particular attributeNo
        /// </summary>
        /// <param name="attributeNo">The argument specify the attributeNo value,the attributeNo should be between 1 and Max attribute_No</param>
        /// <param name="res">DecodingResult the enumerated value for parameter res</param>
        protected void SetAttributeDecodingResult(int attributeNo, DecodingResult res)
        {
            try
            {
                if (attributeNo <= Attribs_No)    // Attribute Decoded Le
                {
                    AccessResults[attributeNo - 1] = res;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to set access rights", ex);
            }
        }

        /// <summary>
        /// Helper Method to Reset the DecodingResult Value Of All Attributes
        /// </summary>
        /// <param name="attributeNo"></param>
        public void ResetAttributeDecodingResults(int attributeNo = 0)
        {
            if (attributeNo == 0x00)
            {
                for (int index = 0; index < AccessResults.Length; index++)
                    AccessResults[index] = DecodingResult.DataNotPresent;
            }
            else if (AccessResults != null && DecodingAttribute <= AccessResults.Length)
            {
                SetAttributeDecodingResult(attributeNo, DecodingResult.DataNotPresent);
            }
        }

        /// <summary>
        /// IsReadSuccess Property is true only when Last data retrieval for Particular Attribute(DecodingAttribute) is <see cref="DecodingResult"/>.Ready
        /// </summary>
        /// <returns>
        /// True:Functions returns Success 
        /// False:Functions returns false if GetAttributeDecodingResult(DecodingAttribute) does not have outcome DecodingResult.Ready  
        /// </returns>
        public bool IsReadSuccess
        {
            get
            {
                bool IsdataRead = true;
                try
                {
                    if (DecodingAttribute != 0)
                    {
                        DecodingResult res = GetAttributeDecodingResult(DecodingAttribute);
                        if (res != DecodingResult.Ready)
                            IsdataRead = false;

                    }
                    else
                    {
                        for (int attributeNo = 1; attributeNo <= Attribs_No; attributeNo++)
                        {
                            DecodingResult res = GetAttributeDecodingResult(DecodingAttribute);
                            if (res != DecodingResult.Ready)
                            {
                                IsdataRead = false;
                                break;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    IsdataRead = false;
                }
                return IsdataRead;
            }
        }

        public Attrib_Access_Modes GetAttribRight(int attribId)
        {
            var AttribRight = Rights.GetAttribRight(attribId);
            return AttribRight;
        }

        public byte GetAccessSelectorRight(int attribId)
        {
            var selector = Rights.GetAccessSelectorRight(attribId);
            return selector;
        }

        public Method_Access_Modes GetMethodRight(int methodId)
        {
            var MethodRight = Rights.GetMethodRight(methodId);
            return MethodRight;
        }

        /// <summary>
        /// Helper Method to identify attribute whether IsReadable or not
        /// </summary>
        /// <remarks>
        /// helper function that computes result on the basis of OBIS Code Rights <see cref="OBISCodeRights"/>  in
        /// current attribute SAP context
        /// </remarks>
        /// <param name="AttribNo">The argument specify the attributeNo value</param>
        /// <returns>
        /// True,False
        /// </returns>
        public bool IsAttribReadable(byte AttribNo)
        {
            var isreadable = Rights.IsAttribReadable(AttribNo);
            return isreadable;
        }

        /// <summary>
        /// Helper Method to identify attribute whether IsWritable or not
        /// </summary>
        /// <remarks>
        /// IsAttribWritable is helper function that computes result on the basis of OBIS Code Rights <see cref="OBISCodeRights"/>  in
        /// current attribute SAP context
        /// </remarks>
        /// <param name="AttribNo">The argument specify the attributeNo value</param>
        /// <returns>
        /// True,False
        /// </returns>
        public bool IsAttribWritable(byte AttribNo)
        {
            var iswritable = Rights.IsAttribWritable(AttribNo);
            return iswritable;
        }

        /// <summary>
        /// Helper Method to identify attribute whether IsWritable or not
        /// </summary>
        /// <remarks>
        /// IsMethodInvokable is helper function that computes result on the basis of OBIS Code Rights <see cref="OBISCodeRights"/>  in
        /// current attribute SAP context
        /// </remarks>
        /// <param name="AttribNo">The argument specify the mehtodNo value</param>
        /// <returns>
        /// True , False
        /// </returns>
        public bool IsMethodInvokable(byte MethodNo)
        {
            var isInvokable = Rights.IsMethodInvokable(MethodNo);
            return isInvokable;
        }

        /// <summary>
        /// Helper Method to identify whether Selective Access is Applicable or not on particular Attribute 
        /// </summary>
        /// <remarks>
        /// Helper Method that computes result on the basis of OBIS Code <see cref="OBISCodeRights"/>  in
        /// current attribute SAP context. The Selective Access is Commonly applicable for "Generic Profile" (Class_id:7) "buffer" 
        /// attribute no 2.
        /// </remarks>
        /// <param name="AttribNo">The argument specify the attributeNo value</param>
        /// <returns>
        /// <see cref="SelectiveAccessType"/> enumerated value
        /// </returns>
        public SelectiveAccessType IsAccessSelecterApplied(byte AttribNo)
        {
            return Rights.IsAccessSelecterApplied(AttribNo);
        }

        public void Grant_Attribute_Rights(int attributeNo = 0, Attrib_Access_Modes AccessRight = Attrib_Access_Modes.Read_Write)
        {
            try
            {
                Rights.Grant_Attribute_Rights(Attribs_No, attributeNo, AccessRight);
            }
            catch { }
        }

        public void Grant_Method_Rights(int methodId = 0, Method_Access_Modes AccessRight = Method_Access_Modes.Access)
        {
            try
            {
                Rights.Grant_Method_Rights(this.Methods_No, methodId, AccessRight);
            }
            catch { }
        }

        /// <summary>
        /// ToString is override-able method that Returns the String representation of the DLMS.Base_Class object
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            ///Append Base Object Values using delemeter char(,)
            StringBuilder strVal = new StringBuilder();
            strVal.AppendFormat("OBIS Code Received:{0}:{1}", DLMS_Common.ArrayToHexString(OBIS_CODE), GetAttributeDecodingResult(1));
            strVal.AppendFormat(",Class Id:{0}", Class_ID);
            strVal.AppendFormat(",OBIS Index:{0}  {1}", INDEX, OBISIndex.ToString());
            strVal.AppendFormat(",Attribute Request:{0}", DecodingAttribute);
            return strVal.ToString();
        }

        #endregion

        #region ICloneable Members

        /// <summary>
        /// The method will Create Deep-Copy Cloned current instance 
        /// </summary>
        /// <returns></returns>
        public virtual object Clone()
        {
            Base_Class clonee = (Base_Class)this.MemberwiseClone();
            clonee.rights = (OBISCodeRights)this.rights.Clone();
            if (this.AccessSelector != null)
            {
                clonee.AccessSelector = (IAccessSelector)this.AccessSelector.Clone();
            }
            return clonee;
        }

        #endregion
    }
}





//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using DLMS.LRUCache;
//using DLMS.Comm;

//namespace DLMS
//{
//    /// <summary>
//    /// DLMS_COSEM template Class sample description
//    /// Definition DLMS_COSEM Data MODEL:All COSEM_Instance available in metering Device with standard OBIS Code as per COSEM Specification
//    /// Definition DLSM_COMSEM_Object Specification: StOBISCode,Class_Id
//    /// Definition Attribute Specification:EncodingAttribute,DecodingAttribute,EncodingType,EncodingSubType,AccessSelector
//    /// Definition Method/Action Specification:MethodInvokeId,Method_ParametersFlag(If Arguments exists):EncodingType,EncodingSubType
//    /// Definition Access Rights Association:
//    /// </summary>
//    abstract public class Base_Class : ICloneable
//    {
//        #region DataMembers

//        /// <summary>
//        /// Identify Maximum Numbers Attribute supported by the DLMS_COSEM Object</summary>
//        /// <remarks>
//        /// template classes,later on this Attribute_No value information to validate EncodingAttribute,DecodingAttribute,DecodingResult and OBISCodeRights
//        /// in specific DLMS_Instance interface objects </remarks>

//        public readonly byte Attribs_No;
//        /// <summary>
//        /// Identify Maximum Methods/rountine Numbers supported by the DLMS_COSEM Object
//        /// </summary>
//        /// <remarks>
//        /// this Methods_No value information to validate MethodInvokeId and OBISCodeRights 
//        /// in specific DLMS_COSEM Instance interface objects
//        /// </remarks>
//        public readonly byte Methods_No;

//        private readonly StOBISCode Index;
//        private ushort? ownerId;
//        private IAccessSelector accessSelector;
//        protected OBISCodeRights rights;
//        // Decoding Variables 
//        protected byte _Attribute_to_decode;
//        protected DecodingResult[] accessResult = null;
//        // Encoders Variables
//        protected byte _EncodingAttribute;
//        protected DataTypes _EncodingType;
//        protected DataTypes _EncodingSubType;
//        // Action Parameters
//        private byte methodInvokeId;
//        private Data_Access_Result method_data_result;
//        private bool method_ParametersFlag;
//        internal List<byte> EncodedRaw;
//        private byte _COSEM_EncodeDecode_OPT = 1;

//        #endregion

//        #region Properties

//        /// <summary>Get/Set access rights for the current instance</summary>
//        /// <remarks>
//        /// Collection of Access rights for current DLMS_COSEM interface object, these rights 
//        /// includes access rights for object attributes,methods and data selectors details.  
//        /// The OBISCodeRights loaded from SAP_Table for Current association with meter, the OBISCodeRights is used to
//        /// specify the access level for Attributes , for methods/Action and Selectors(for GET_Service) Of DLMS_COMSEM instance.
//        /// <code>
//        /// public enum Attrib_Access_Modes : byte
//        /// {
//        ///     No_Access = 0,
//        ///     Read_Only,
//        ///     Write_Only,
//        ///     Read_Write,
//        ///     Authenticated_Read_Only,
//        ///     Authenticated_Write_Only,
//        ///     Authenticated_Read_Write,
//        /// }
//        /// 
//        ///  public enum Method_Access_Modes : byte
//        /// {
//        ///     No_Access = 0,
//        ///     Access,
//        ///     Authenticated_Access,
//        /// }
//        /// 
//        ///  public enum SelectiveAccessType : byte
//        /// {
//        ///     Not_Applied = 0,
//        ///     Range_Descripter = 1,
//        ///     Entry_Descripter = 2,
//        ///     Both_Types = 3
//        /// }
//        /// 
//        /// </code>
//        /// </remarks>
//        public OBISCodeRights Rights
//        {
//            get { return rights; }
//            set { rights = value; }
//        }

//        /// <summary>Get the Result(<see cref="DecodingResult"/>) after Validating access rights</summary>
//        /// <remarks>
//        /// AccessResults the success or error status codes retrieved while reading the DLMS_COSEM instance from the meter. 
//        /// The Access Results information is collected during current object decoding process. These status codes are
//        /// further utilize during the retrieval of attributes Value/Data of DLMS_COSEM object.
//        /// </remarks>
//        public DecodingResult[] AccessResults
//        {
//            get { return accessResult; }

//        }

//        /// <summary>
//        /// Get the OBIS(<see cref="StOBISCode"/>) code for current instance
//        /// </summary>
//        /// <remarks>
//        /// OBIS_CODE is readonly Public Property to define Logical_Name for specific DLMS_COSEM Instance interface objects. 
//        /// The OBIS_CODE is 6 byte long logical Name in format[OBIS_CODE_A][OBIS_CODE_B][OBIS_CODE_C][OBIS_CODE_E][OBIS_CODE_F].
//        /// </remarks>
//        public byte[] OBIS_CODE
//        {
//            get
//            {
//                return OBISIndex.OBISCode;
//            }
//        }

//        /// <summary>
//        /// Get the class ID of the current instance</summary>
//        /// <remarks>
//        /// Class_ID is unique identifier for DLMS_COSEM Template interface Class. each class has its own unique class ID in DLMS/COSEM Context 
//        /// This Class_ID is 2 byte ushort Number to identify the object of specific class.
//        /// </remarks>
//        public ObjectType ObjectType
//        {
//            get { return OBISIndex.ObjectType; }
//        }

//        /// <summary>
//        /// Get the class ID of the current instance</summary>
//        /// <remarks>
//        /// Class_ID is unique identifier for DLMS_COSEM Template interface Class. each class has its own unique class ID in DLMS/COSEM Context 
//        /// This Class_ID is 2 byte ushort Number to identify the object of specific class.
//        /// </remarks>
//        public UInt16 Class_ID
//        {
//            get { return OBISIndex.ClassId; }
//        }

//        /// <summary>Get the composite Code represent a single Data Quantity </summary>
//        /// <remarks>
//        /// INDEX is ,custom Composite storage structure, used in Library Code to define Logical_Name and COSEM Template Class_id: information
//        /// for specific DLMS_COSEM Instance. The Index is also used as KEY for SAP_Table data structure which is used to retrieve access rights defined for DLMS_COSEM Instance interface object. 
//        /// The INDEX structure take 8 byte memory as Unsigned Long Integer number,
//        /// 6 byte for Logical Name(one byte for each OCTECT) and two bytes for Class_id.
//        /// The INDEX format is [Class_id][OBIS_CODE_A][OBIS_CODE_B][OBIS_CODE_C][OBIS_CODE_E][OBIS_CODE_F].
//        /// for DLMS Data MODEL Object_Specification 
//        /// </remarks>
//        public Get_Index INDEX
//        {
//            get
//            {
//                return OBISIndex.OBISIndex;
//            }
//        }

//        /// <summary>
//        /// OwnerId is nullable public property used as part of KeyIndexer composite Structure
//        /// </summary>
//        public ushort? OwnerId
//        {
//            get { return ownerId; }
//            set { ownerId = value; }
//        }

//        /// <summary>Get <see cref="StOBISCode"/> structure </summary>
//        /// <remarks>
//        /// StOBISCode is ,custom Composite storage structure, used in Library Code to define Logical_Name and COSEM Template Class_id: information
//        /// for specific DLMS_COSEM Instance interface objects.The StOBISCode structure take 8 byte memory as Unsigned Long Integer number,
//        /// 6 byte for Logical Name(one byte for each OCTECT) and two bytes for Class_id.
//        /// The StOBISCode format is [Class_id][OBIS_CODE_A][OBIS_CODE_B][OBIS_CODE_C][OBIS_CODE_E][OBIS_CODE_F].
//        /// for DLMS Data MODEL Object_Specification 
//        /// </remarks>
//        public StOBISCode OBISIndex
//        {
//            get { return Index; }
//        }

//        /// <summary>
//        /// Get the Key to identify a particular instance of a particular class in DLMS/COSEM object dictionary
//        /// </summary>
//        /// <remarks>
//        /// The KeyIndexer is readonly ,custom Composite Structure, Public Property to Index KEY the for specific DLMS_COSEM Instance interface objects
//        /// for storage and retrieval from different INDEX based storage structures, KEY_VALUE Pair Data Dictionary, HASH Tables.   
//        /// </remarks>
//        public KeyIndexer KeyIndexer
//        {
//            get
//            {
//                try
//                {
//                    return new KeyIndexer(OBISIndex, OwnerId);
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//            }
//        }

//        /// <summary>
//        /// Get/Set decoding attribute which have to be manipulate later in read or write process
//        /// </summary>
//        /// <remarks>
//        /// The DecodingAttribute for particular attribute Identity of DLMS object retrieved during the last data read request.
//        /// The DecodingAttribute information is between 0 and Max Attribs_No of DLMS_COSME template class, if read request is
//        /// for all attribute of Object then this Property shall be 0. 
//        /// </remarks>
//        public byte DecodingAttribute
//        {
//            get { return _Attribute_to_decode; }
//            set
//            {
//                if (value > Attribs_No && Attribs_No != 0)
//                    throw new DLMSException(String.Format("Invalid Attribute To Decode {0:X2},In Class ID {1:X2} (Error Code:{3})", value,
//                        this.Class_ID,(int)DLMSErrors.Invalid_DecodingAttribute));
//                _Attribute_to_decode = value;
//            }
//        }

//        /// <summary>
//        /// Get/Set Access Selector define read or write Range <see cref="EntryDescripter"/>
//        /// </summary>
//        /// <remarks>
//        /// The xDLMS services Read, Write, UnconfirmedWrite (used with SN referencing) and GET, SET (used with LN referencing) typically reference the entire attribute.
//        /// However, for certain attributes selective access to just a part of the attribute may be provided. The part of the attribute is identified by specific selective access parameters.
//        /// These are defined as part of the attribute specification.</remarks>
//        public IAccessSelector AccessSelector
//        {
//            get { return accessSelector; }
//            set { accessSelector = value; }
//        }

//        /// <summary>
//        /// Get/Set DataTypes enum, the EncodingType(<see cref="DataTypes"/>) is used to define Choice(DataTypes enum) for attribute_Specfication.
//        /// </summary>
//        /// <remarks>
//        ///  For attribute_Specfication EncodingType,EncodingSubType Properties should be initialized to work properly.
//        /// </remarks>
//        /// <example>
//        /// <code>
//        /// //To Set long64-unsigned data type using Class_id:01 the following Code Piece should be used to initialize DLSM_Intance
//        /// 
//        /// StOBISCode OBIS = GET_INDEX.Major_Alarm_Counter;
//        /// Base_Class  Comm_Obj = GetSAPEntry(OBIS);
//        /// Comm_Obj.EncodingAttribute = 0x02;
//        /// Comm_Obj.EncodingType = DataTypes._A15_long_64_unsigned;
//        /// 
//        /// //Now use Initialized Base_Class Comm_Obj for SET Operation
//        /// Data_Access_Result SET_OUTCome = ApplicationProcess_Controller.SET(Comm_Obj);
//        /// //Now Verify SET_OUTCome variable for SET function success/failure 
//        /// </code>
//        /// </example>
//        public DataTypes EncodingType
//        {
//            get { return _EncodingType; }
//            set { _EncodingType = value; }
//        }

//        public DataTypes DecodingType
//        {
//            get { return _EncodingType; }
//            set { _EncodingType = value; }
//        }


//        /// <summary>
//        /// Get/Set DataTypes enum, the EncodingSubType (<see cref="DataTypes"/>) for iterative data structure
//        /// </summary>
//        /// <example>
//        /// <code>
//        /// EncodingSubType is Only initialized for attribute_Specfication when EncodingType is Iterative data structure
//        /// To Set long64-unsigned data type using Class_id:01 the following Code Piece should be used to initialize DLSM_Intance
//        /// 
//        /// StOBISCode OBIS = GET_INDEX.Major_Alarm_Counter;
//        /// Base_Class  Comm_Obj = GetSAPEntry(OBIS);
//        /// Comm_Obj.EncodingAttribute = 0x02;
//        /// Comm_Obj.EncodingType = DataTypes._A01_array;
//        /// Comm_Obj.EncodingSubType = DataTypes._A15_long_64_unsigned;
//        /// 
//        /// //Now use Initialized Base_Class Comm_Obj for SET Operation
//        /// Data_Access_Result SET_OUTCome = ApplicationProcess_Controller.SET(Comm_Obj);
//        /// //Now Verify SET_OUTCome variable for SET function success/failure 
//        /// </code>
//        /// </example>
//        public DataTypes EncodingSubType
//        {
//            get { return _EncodingSubType; }
//            set { _EncodingSubType = value; }
//        }

//        public DataTypes DecodingSubType
//        {
//            get { return _EncodingSubType; }
//            set { _EncodingSubType = value; }
//        }


//        /// <summary> Get/Set the encoding attribute (Attribute ID) </summary>
//        /// <remarks>
//        /// It is used to retrieve the particular attribute in DLMS_COSEM object for current data read request.
//        /// The EncodingAttribute information is between 0 and Max Attribs_No of DLMS_COSME template class, if read request is
//        /// for all attribute of Object then this Property shall be 0. 
//        /// </remarks>
//        public byte EncodingAttribute
//        {
//            get { return _EncodingAttribute; }
//            set
//            {
//                if ((value > Attribs_No && Attribs_No != 0) || value == 0)
//                throw new DLMSException(String.Format("Invalid Attribute To Encode 0x{0:X2},In Class ID 0x{1:X2} (Error Code:{3})", value,
//                                        this.Class_ID, (int)DLMSErrors.Invalid_EncodingAttribute));
//                _EncodingAttribute = value;
//            }
//        }

//        /// <summary>
//        /// Get/Set Method invoke ID to encode a Invoking Request
//        /// </summary>
//        /// <remarks>
//        /// Method/Action Id for DLMS_COSEM Interface Object which is used to invoke particular Method in DLMS_COSEM object for action request.
//        /// The MethodInvokeId information is between 1 and Max Methods_No of DLMS_COSME object. 
//        /// </remarks>
//        public byte MethodInvokeId
//        {
//            get
//            {
//                return methodInvokeId;
//            }
//            set
//            {
//                if (value > Methods_No && value == 0)
//                    throw new DLMSException(String.Format("Invalid Method To Invoke 0x{0:X2},In Class ID 0x{1:X2} (Error Code:{3})", value,
//                        this.Class_ID, (int)DLMSErrors.Invalid_MethodInvokeId));
//                methodInvokeId = value;
//            }
//        }

//        /// <summary>
//        ///  Get/Set the flag to identify Method is parameterized or non-parameterized which is being invoke.
//        /// </summary>
//        public bool Method_ParametersFlag
//        {
//            get { return method_ParametersFlag; }
//            set { method_ParametersFlag = value; }
//        }

//        /// <summary>
//        /// Specify outcome/result for the last action/Method request.
//        /// </summary>
//        /// <returns>
//        /// Data_Access_Result enumerated number to represent the last action/Method outcome results. The Data_Access_Result
//        ///  is used to notify the Successful Method execution or error status_codes being returned from DLMS_COSEM device.
//        /// </returns>
//        public Data_Access_Result Method_Data_Result
//        {
//            get { return method_data_result; }
//            set { method_data_result = value; }
//        }

//        public bool EnableCOSEM_EncodeDecode_OPT
//        {
//            get { return Convert.ToBoolean(_COSEM_EncodeDecode_OPT); }
//            set { _COSEM_EncodeDecode_OPT = Convert.ToByte(value); }
//        }

//        #endregion

//        #region Constructor

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        /// <remarks>
//        /// Obselete
//        /// </remarks>
//        /// <param name="class_id">To specify DLMS_COSEM template Class Id</param>
//        /// <param name="attributes">To specify DLMS_COSEM template Class Max Number Of Attribute Supported</param>
//        /// <param name="methods">To specify DLMS_COSEM template Class Max Number Of Methods Supported</param>
//        /// <param name="In">GET_Index to specify Logical Name,Class_id for Current Object</param>
//        /// <param name="Ob_Co">Ob_Co is 6 byte array form Logical Name Specification</param>
//        /// <param name="Total_Associations">The Total_Association to specify Max_No of Association can be created 
//        /// for SAP table structure specification</param>
//        public Base_Class(UInt16 class_id, byte attributes, byte methods, Get_Index In, byte[] Ob_Co, UInt16 Total_Associations)
//            : this(class_id, Ob_Co)
//        {
//            Attribs_No = attributes;
//            Methods_No = methods;
//            Index = In;
//            accessResult = new DecodingResult[Attribs_No];
//            Rights = new OBISCodeRights();
//        }

//        /// <summary>
//        /// Default Constructor
//        /// </summary>
//        private Base_Class()
//        {
//            Index = Get_Index.Dummy;
//        }

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        /// <remarks>
//        /// Obselete
//        /// </remarks>
//        /// <param name="class_id">To specify DLMS_COSEM template Class Id</param>
//        /// <param name="Ob_Co">Ob_Co is 6 byte array form Logical Name Specification</param>
//        public Base_Class(UInt16 class_id, byte[] Ob_Co)
//            : this()
//        {
//            if (Ob_Co == null || Ob_Co.Length != 6)
//                throw new DLMSException("Invalid OBIS Code To Initialize_Base_Class_Contructure");

//            StOBISCode t = StOBISCode.ConvertFrom(Ob_Co);
//            t.ClassId = class_id;
//            Index = t;

//        }

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        /// <remarks>
//        /// Obselete
//        /// </remarks>
//        /// <param name="class_id">To specify DLMS_COSEM template Class Id</param>
//        /// <param name="attributes">To specify DLMS_COSEM template Class Max Number Of Attribute Supported</param>
//        /// <param name="methods">To specify DLMS_COSEM template Class Max Number Of Methods Supported</param>
//        /// <param name="Ob_Co">Ob_Co is 6 byte array form Logical Name Specification</param>
//        public Base_Class(UInt16 class_id, byte attributes, byte methods, byte[] Ob_Co)
//            : this(class_id, Ob_Co)
//        {
//            Attribs_No = attributes;
//            Methods_No = methods;

//            accessResult = new DecodingResult[Attribs_No];
//            for (int index = 0; index < accessResult.Length; index++)
//            {
//                accessResult[index] = DecodingResult.DataNotPresent;
//            }
//            Rights = new OBISCodeRights();
//            Rights.OBISIndex = OBISIndex;
//        }

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        /// <remarks>
//        /// Most Commonly used in Constructor in Derived Classes for Chaining
//        /// </remarks>
//        /// <param name="OBISCode">To specify DLMS_COSEM Logical Name, OBIS Code,Class_Id</param>
//        /// <param name="attributes">To specify DLMS_COSEM template Class Max Number Of Attribute Supported</param>
//        /// <param name="methods">To specify DLMS_COSEM template Class Max Number Of Methods Supported</param>
//        public Base_Class(StOBISCode OBISCode, byte attributes, byte methods)
//        {
//            Attribs_No = attributes;
//            Methods_No = methods;
//            Index = OBISCode;
//            accessResult = new DecodingResult[Attribs_No];
//            Rights = new OBISCodeRights();
//            Rights.OBISIndex = OBISCode;
//        }

//        /// <summary>
//        /// Copy Constructor Base_Class
//        /// </summary>
//        /// <param name="obj">The Base_Class DLMS_COSEM Instance to be copied</param>
//        public Base_Class(Base_Class obj)
//            : this(obj.Class_ID, obj.Attribs_No, obj.Methods_No, obj.OBIS_CODE)
//        {
//            /// Copy Remaining DataItems
//            Index = obj.OBISIndex;
//            if (obj.AccessSelector != null)
//                accessSelector = (IAccessSelector)obj.AccessSelector.Clone();
//            if (obj.Rights != null)
//                Rights = (OBISCodeRights)obj.Rights.Clone();
//            _Attribute_to_decode = obj.DecodingAttribute;

//            _EncodingAttribute = obj.EncodingAttribute;
//            _EncodingType = obj.EncodingType;
//            _EncodingSubType = obj.EncodingSubType;
//        }

//        #endregion

//        #region Decoders/Encoders

//        /// <summary>
//        /// Over-ridable method to decode GET Command Attribute data
//        /// </summary>
//        /// <param name="Data">The byte[] Buffer to be decoded</param>
//        /// <param name="array_traverse">array_traverse is indexer for current index</param>
//        /// <param name="length">length argument specify the decoded-data length</param>
//        public virtual void Decode_Data(ref byte[] Data, ref int array_traverse, int length)
//        {
//            //UInt16 array_traverse = 0;
//            byte[] Obis_code_recieved = new byte[6];
//            try
//            {
//                for (int index = 0; index < AccessResults.Length; index++)
//                    AccessResults[index] = DecodingResult.DataNotPresent;
//                //------------------------------------------------------
//                DecoderAttribute_0(ref Data, ref array_traverse, "BaseClass_DataDecoder");
//                DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, "BaseClass_DataDecoder");
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                {
//                    throw ex;
//                }
//                else
//                {
//                    throw new DLMSDecodingException("Error occurred while decoding data", "BaseClass_DataDecoder", ex);
//                }
//            }
//        }

//        /// <summary>
//        /// Over-ridable method to decode GET Command Attribute data
//        /// </summary>
//        /// <param name="Data">The byte[] Buffer to be decoded</param>
//        public virtual void Decode_Data(ref byte[] Data)
//        {
//            try
//            {
//                int array_traverse = 0;
//                Decode_Data(ref Data, ref array_traverse, Data.Length);
//                DLMS_Common.Data_Array_resizer(ref Data, array_traverse);
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                {
//                    throw ex;
//                }
//                else
//                {
//                    throw new DLMSDecodingException(String.Format("Error occurred while decoding data {0}_{1}", OBISIndex, OBISIndex.OBISIndex),
//                                                    "BaseClass_DataDecoder", ex);
//                }
//            }
//        }

//        /// <summary>
//        /// Over-ridable method to encode data for SET command
//        /// </summary>
//        /// <returns>
//        /// byte[] encoded buffer for DLMS_COSEM instance
//        /// </returns>
//        public virtual byte[] Encode_Data()
//        {
//            EncodedRaw = new List<byte>();
//            EncodedRaw.Add((byte)DataTypes._A00_Null);
//            return EncodedRaw.ToArray<byte>();
//        }

//        /// <summary>
//        ///  Over-ridable method to decode Method return result parameters
//        /// </summary>
//        /// <param name="Data"></param>
//        /// <param name="array_traverse"></param>
//        /// <param name="length"></param>
//        /// <returns></returns>
//        public virtual int Decode_Parameters(ref byte[] Data, ref int array_traverse, int length)
//        {
//            //Do Nothing For Base_Class 
//            return array_traverse;
//        }

//        /// <summary>
//        /// Over-ridable method to decode Method return result parameters
//        /// </summary>
//        /// <param name="Data"></param>
//        public virtual void Decode_Parameters(ref byte[] Data)
//        {
//            try
//            {
//                int array_traverse = 0;
//                Decode_Parameters(ref Data, ref array_traverse, Data.Length);
//                DLMS_Common.Data_Array_resizer(ref Data, array_traverse);
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                {
//                    throw ex;
//                }
//                else
//                {
//                    throw new DLMSDecodingException(String.Format("Error occurred while decoding data parameters {0}_{1}", 
//                        OBISIndex, OBISIndex.OBISIndex),
//                        "BaseClass_DataDecoder", ex);
//                }
//            }
//        }

//        /// <summary>
//        /// Over-ridable method to encode Action Parameter data if exists
//        /// </summary>
//        /// <returns></returns>
//        public virtual byte[] Encode_Parameters()
//        {
//            return null;
//        }

//        /// <summary>
//        /// DecoderAttribute_0 decodes bytes for Attribute zero of DLMS classes,method verifies the startup data type should be 
//        /// structure and no of received attributes should be equal to Total Attributes in DLMS class
//        /// </summary>
//        /// <param name="Data">byte []Data,Binary Data Buffer</param>
//        /// <param name="array_traverse">ushort array_traverse,Indexer to Binary Buffer</param>
//        /// <param name="ExtDecoderName">String ExtDecoderName,Name of Decoder of Child Classes</param>
//        protected byte DecoderAttribute_0(ref byte[] Data, ref int array_traverse, String ExtDecoderName)
//        {
//            byte Attribs_No_Recv = 0;
//            if (DecodingAttribute == 0x00)
//            {
//                byte current_char = Data[array_traverse++];
//                Attribs_No_Recv = Data[array_traverse++];

//                // 1st element must be structure
//                if (current_char != (byte)DataTypes._A02_structure)
//                {
//                    // Generate Error and return
//                    SetAttributeDecodingResult(1, DecodingResult.DecodingError);
//                    throw new DLMSDecodingException(String.Format("First element should be 02 (Structure) (Error Code:{0}) ",
//                        (int)DLMSErrors.Invalid_Type_MisMatch), ExtDecoderName);
//                }
//                if (Attribs_No_Recv == 0 || !(Attribs_No_Recv <= Attribs_No))
//                {
//                    // Generate Error and return
//                    SetAttributeDecodingResult(1, DecodingResult.DecodingError);
//                    throw new DLMSDecodingException(String.Format("Wrong number of Attribute Received (Error Code:{0})",
//                        (int)DLMSErrors.Invalid_DecodingAttribute), ExtDecoderName);
//                }
//            }
//            return Attribs_No_Recv;
//        }

//        /// <summary>
//        /// EncoderAttribute_0 encodes bytes for Attribute zero of DLMS classes,
//        /// </summary>
//        /// <remarks>
//        /// EncoderAttribute_0 method verifies the startup data type should be structure and no of encoded attributes 
//        /// should be equal to Total <see cref="Attribs_No"/> in DLMS_COSEM template class
//        /// </remarks>
//        protected void EncoderAttribute_0()
//        {
//            try
//            {
//                if (EncodingAttribute == 0)
//                {
//                    throw new DLMSEncodingException(String.Format("Error Encoding With Attribute 0 (Error Code:{0})",
//                        (int)DLMSErrors.Invalid_EncodingAttribute), "EncodingAttribute_0");
//                }
//            }
//            catch (Exception ex)
//            {
//                throw new DLMSEncodingException(String.Format("Error Encoding With Attribute 0 (Error Code:{0})",
//                    (int)DLMSErrors.Invalid_EncodingAttribute), "EncodingAttribute_0");
//            }
//        }

//        /// <summary>
//        /// Decodes the OBIS Code of DLMS_COSEM instance
//        /// </summary>
//        /// <remarks>The DecoderLogicalName verifies the OBIS Code received already exists in current SAP Context ,
//        /// also checks the decoded OBIS Code matches the DLMS_COSEM instance OBIS being decoded.
//        /// </remarks>
//        /// <param name="Data">byte []Data,Binary Data Buffer</param>
//        /// <param name="array_traverse">ushort array_traverse,Indexer to Binary Buffer</param>
//        /// <param name="Obis_code_received">byte []Obis_code_received,ref array that contains OBIS Code Decoded</param>
//        /// <param name="ExtDecoderName">String ExtDecoderName,Name of Decoder of Child Classes</param>
//        protected void DecoderLogicalName(ref byte[] Data, ref int array_traverse, ref byte[] Obis_code_received, String ExtDecoderName)
//        {
//            if (DecodingAttribute == 0x01 || DecodingAttribute == 0x00)
//            {
//                try
//                {
//                    if (Data[array_traverse++] == (byte)DataTypes._A00_Null)
//                    {
//                        if (!IsAttribReadable(0x01))
//                        {
//                            SetAttributeDecodingResult(0x01, DecodingResult.NoAccess);
//                        }
//                        else
//                        {
//                            SetAttributeDecodingResult(0x01, DecodingResult.DecodingError);
//                        }
//                        return;
//                    }
//                    if (Obis_code_received == null)              //Instantiate OBIS-Code Array
//                        Obis_code_received = new byte[6];
//                    array_traverse--;
//                    Obis_code_received = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse, Data.Length);
//                    StOBISCode CodeReceived = StOBISCode.ConvertFrom(Obis_code_received);
//                    CodeReceived.ClassId = OBISIndex.ClassId;
//                    if (!CodeReceived.Equals(OBISIndex))
//                    {
//                        SetAttributeDecodingResult(1, DecodingResult.DLMSError);
//                        throw new DLMSDecodingException(String.Format("{0}_{1} Wrong OBIS Code (Error Code:{2})", CodeReceived, 
//                            OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_OBISCode),
//                            ExtDecoderName) { HelpLink = "Error:WR76" };
//                    }
//                    SetAttributeDecodingResult(1, DecodingResult.Ready);
//                }
//                catch (Exception ex)
//                {
//                    if (!IsAttribReadable(0x01))
//                    {
//                        SetAttributeDecodingResult(0x01, DecodingResult.NoAccess);
//                    }
//                    else
//                    {
//                        SetAttributeDecodingResult(0x01, DecodingResult.DecodingError);
//                    }

//                    if (ex is DLMSDecodingException)
//                        throw ex;
//                    else
//                        throw new DLMSDecodingException(String.Format("{0}_{1} Wrong OBIS Code (Error Code:{2})", OBISIndex, OBISIndex.OBISIndex,
//                             (int)DLMSErrors.Invalid_OBISCode), ExtDecoderName) { HelpLink = "Error:WR76" };
//                }
//            }
//        }

//        /// <summary>
//        /// Encodes the OBIS Code of DLMS_COSEM instance 
//        /// </summary>
//        protected void EncoderLogicalName(bool Nullable)
//        {
//            try
//            {
//                if (EncodingAttribute == 0 || EncodingAttribute == 1)
//                {
//                    if (Nullable)                                            //Send Nullable
//                        EncodedRaw.Add((byte)DataTypes._A00_Null);
//                    else
//                    {
//                        EncodedRaw.Add((byte)DataTypes._A09_octet_string);
//                        EncodedRaw.AddRange(OBIS_CODE);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw new DLMSEncodingException(String.Format("Error Encoding Logical Name (Error Code:{0})",
//                    (int)DLMSErrors.Invalid_OBISCode), "EncodingLogicalName_Encoder");
//            }
//        }

//        /// <summary>
//        /// EncoderLogicalName encodes the OBIS Code of DLMS_COSEM instance 
//        /// </summary>
//        protected void EncoderLogicalName()
//        {
//            try
//            {
//                EncoderLogicalName(true);
//            }
//            catch (Exception ex)
//            {
//                throw new DLMSEncodingException(String.Format("Error Encoding Logical Name (Error Code:{0})",
//                    (int)DLMSErrors.Invalid_OBISCode), "EncodingLogicalName_Encoder");
//            }
//        }

//        #endregion

//        #region Member Methods

//        /// <summary>
//        /// Helper Method to retrieve the DecodingResult value for particular attribute No
//        /// </summary>
//        /// <param name="attributeNo">The argument specify the attributeNo value,the attributeNo should be between 1 and Max attribute_No</param>
//        /// <returns>
//        /// DecodingResult the enumerated value for result
//        /// <code>
//        /// public enum DecodingResult : byte
//        /// {
//        ///    DataNotPresent = 0x0,
//        ///    NoAccess,
//        ///    NotInSAPList,
//        ///    DecodingError,
//        ///    DLMSError,
//        ///    Ready
//        /// }
//        /// </code>
//        /// </returns>

//        public DecodingResult GetAttributeDecodingResult(int attributeNo)
//        {
//            if (attributeNo != 0)
//            {
//                if (attributeNo <= Attribs_No)    //Attribute Decoded Le
//                {
//                    return AccessResults[attributeNo - 1];
//                }
//                else
//                    return DecodingResult.DataNotPresent;       //Default
//            }
//            else
//            {
//                // return DecodingResult.Ready;
//                DecodingResult res = DecodingResult.DataNotPresent;
//                foreach (var item in AccessResults)
//                {
//                    if (item == DecodingResult.Ready)
//                        res = item;
//                    else if (item == DecodingResult.NoAccess)
//                        continue;
//                    else
//                    {
//                        res = item;
//                        break;
//                    }
//                }
//                return res;
//            }
//        }

//        /// <summary>
//        /// Helper Method to set/assign the DecodingResult value for particular attributeNo
//        /// </summary>
//        /// <param name="attributeNo">The argument specify the attributeNo value,the attributeNo should be between 1 and Max attribute_No</param>
//        /// <param name="res">DecodingResult the enumerated value for parameter res</param>
//        protected void SetAttributeDecodingResult(int attributeNo, DecodingResult res)
//        {
//            try
//            {
//                if (attributeNo <= Attribs_No)    // Attribute Decoded Le
//                {
//                    AccessResults[attributeNo - 1] = res;
//                }
//            }
//            catch (Exception ex)
//            {
//                throw new Exception("Unable to set access rights", ex);
//            }
//        }

//        /// <summary>
//        /// Helper Method to Reset the DecodingResult Value Of All Attributes
//        /// </summary>
//        /// <param name="attributeNo"></param>
//        public void ResetAttributeDecodingResults(int attributeNo = 0)
//        {
//            if (attributeNo == 0x00)
//            {
//                for (int index = 0; index < AccessResults.Length; index++)
//                    AccessResults[index] = DecodingResult.DataNotPresent;
//            }
//            else if (AccessResults != null && DecodingAttribute <= AccessResults.Length)
//            {
//                SetAttributeDecodingResult(attributeNo, DecodingResult.DataNotPresent);
//            }
//        }

//        /// <summary>
//        /// IsReadSuccess Property is true only when Last data retrieval for Particular Attribute(DecodingAttribute) is <see cref="DecodingResult"/>.Ready
//        /// </summary>
//        /// <returns>
//        /// True:Functions returns Success 
//        /// False:Functions returns false if GetAttributeDecodingResult(DecodingAttribute) does not have outcome DecodingResult.Ready  
//        /// </returns>
//        public bool IsReadSuccess
//        {
//            get
//            {
//                bool IsdataRead = true;
//                try
//                {
//                    if (DecodingAttribute != 0)
//                    {
//                        DecodingResult res = GetAttributeDecodingResult(DecodingAttribute);
//                        if (res != DecodingResult.Ready)
//                            IsdataRead = false;

//                    }
//                    else
//                    {
//                        for (int attributeNo = 1; attributeNo <= Attribs_No; attributeNo++)
//                        {
//                            DecodingResult res = GetAttributeDecodingResult(DecodingAttribute);
//                            if (res != DecodingResult.Ready)
//                            {
//                                IsdataRead = false;
//                                break;
//                            }
//                        }
//                    }
//                }
//                catch (Exception)
//                {
//                    IsdataRead = false;
//                }
//                return IsdataRead;
//            }
//        }

//        public Attrib_Access_Modes GetAttribRight(int attribId)
//        {
//            var AttribRight = Rights.GetAttribRight(attribId);
//            return AttribRight;
//        }

//        public byte GetAccessSelectorRight(int attribId)
//        {
//            var selector = Rights.GetAccessSelectorRight(attribId);
//            return selector;
//        }

//        public Method_Access_Modes GetMethodRight(int methodId)
//        {
//            var MethodRight = Rights.GetMethodRight(methodId);
//            return MethodRight;
//        }

//        /// <summary>
//        /// Helper Method to identify attribute whether IsReadable or not
//        /// </summary>
//        /// <remarks>
//        /// helper function that computes result on the basis of OBIS Code Rights <see cref="OBISCodeRights"/>  in
//        /// current attribute SAP context
//        /// </remarks>
//        /// <param name="AttribNo">The argument specify the attributeNo value</param>
//        /// <returns>
//        /// True,False
//        /// </returns>
//        public bool IsAttribReadable(byte AttribNo)
//        {
//            var isreadable = Rights.IsAttribReadable(AttribNo);
//            return isreadable;
//        }

//        /// <summary>
//        /// Helper Method to identify attribute whether IsWritable or not
//        /// </summary>
//        /// <remarks>
//        /// IsAttribWritable is helper function that computes result on the basis of OBIS Code Rights <see cref="OBISCodeRights"/>  in
//        /// current attribute SAP context
//        /// </remarks>
//        /// <param name="AttribNo">The argument specify the attributeNo value</param>
//        /// <returns>
//        /// True,False
//        /// </returns>
//        public bool IsAttribWritable(byte AttribNo)
//        {
//            var iswritable = Rights.IsAttribWritable(AttribNo);
//            return iswritable;
//        }

//        /// <summary>
//        /// Helper Method to identify attribute whether IsWritable or not
//        /// </summary>
//        /// <remarks>
//        /// IsMethodInvokable is helper function that computes result on the basis of OBIS Code Rights <see cref="OBISCodeRights"/>  in
//        /// current attribute SAP context
//        /// </remarks>
//        /// <param name="AttribNo">The argument specify the mehtodNo value</param>
//        /// <returns>
//        /// True , False
//        /// </returns>
//        public bool IsMethodInvokable(byte MethodNo)
//        {
//            var isInvokable = Rights.IsMethodInvokable(MethodNo);
//            return isInvokable;
//        }

//        /// <summary>
//        /// Helper Method to identify whether Selective Access is Applicable or not on particular Attribute 
//        /// </summary>
//        /// <remarks>
//        /// Helper Method that computes result on the basis of OBIS Code <see cref="OBISCodeRights"/>  in
//        /// current attribute SAP context. The Selective Access is Commonly applicable for "Generic Profile" (Class_id:7) "buffer" 
//        /// attribute no 2.
//        /// </remarks>
//        /// <param name="AttribNo">The argument specify the attributeNo value</param>
//        /// <returns>
//        /// <see cref="SelectiveAccessType"/> enumerated value
//        /// </returns>
//        public SelectiveAccessType IsAccessSelecterApplied(byte AttribNo)
//        {
//            return Rights.IsAccessSelecterApplied(AttribNo);
//        }

//        public void Grant_Attribute_Rights(int attributeNo = 0, Attrib_Access_Modes AccessRight = Attrib_Access_Modes.Read_Write)
//        {
//            try
//            {
//                Rights.Grant_Attribute_Rights(Attribs_No, attributeNo, AccessRight);
//            }
//            catch { }
//        }

//        public void Grant_Method_Rights(int methodId = 0, Method_Access_Modes AccessRight = Method_Access_Modes.Access)
//        {
//            try
//            {
//                Rights.Grant_Method_Rights(this.Methods_No, methodId, AccessRight);
//            }
//            catch { }
//        }

//        /// <summary>
//        /// ToString is override-able method that Returns the String representation of the DLMS.Base_Class object
//        /// </summary>
//        /// <returns>String</returns>
//        public override string ToString()
//        {
//            ///Append Base Object Values using delemeter char(,)
//            StringBuilder strVal = new StringBuilder();
//            strVal.AppendFormat("OBIS Code Received:{0}:{1}", DLMS_Common.ArrayToHexString(OBIS_CODE), GetAttributeDecodingResult(1));
//            strVal.AppendFormat(",Class Id:{0}", Class_ID);
//            strVal.AppendFormat(",OBIS Index:{0}  {1}", INDEX, OBISIndex.ToString());
//            strVal.AppendFormat(",Attribute Request:{0}", DecodingAttribute);
//            return strVal.ToString();
//        }

//        #endregion

//        #region ICloneable Members

//        /// <summary>
//        /// The method will Create Deep-Copy Cloned current instance 
//        /// </summary>
//        /// <returns></returns>
//        public virtual object Clone()
//        {
//            Base_Class clonee = (Base_Class)this.MemberwiseClone();
//            clonee.rights = (OBISCodeRights)this.rights.Clone();
//            if (this.AccessSelector != null)
//            {
//                clonee.AccessSelector = (IAccessSelector)this.AccessSelector.Clone();
//            }
//            return clonee;
//        }

//        #endregion
//    }
//}
