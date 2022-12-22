using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS.Comm;

namespace DLMS
{
    /// <summary>
    /// Association Logical Name (class_id: 15, version: 2) COSEM logical devices able to establish AAs (Application Association).
    /// Within COSEM context using LN(Logical Name) referencing, model the AAs (Application Association) 
    /// through instances of the Association LN Class.
    /// A COSEM logical device has one instance of this Class for each AA (Application Association) the device is able to support.
    /// </summary>
    public class Class_15 : Base_Class
    {
        public static readonly byte Reply_HLS_Authentication = 1;
        public static readonly byte Change_HLS_Secret = 2;

        public static readonly byte Add_Object = 3;
        public static readonly byte Remove_Object = 4;

        public static readonly byte Add_User = 5;
        public static readonly byte Remove_User = 6;

        #region Data_Members

        private List<OBISCodeRights> _OBISCodes;

        /// <summary>
        /// Associated_Partners
        /// </summary>
        public ushort ClientSAP_Id;
        public ushort ServerSAP_Id;

        public stApplication_Context applicationContext;
        public stNegotiated_xDLMS_Context negotiatedDLMSContext;
        public stAuthentication_MechanismName authenticationMechanismName;

        /// <summary>
        /// Password Required for authentication for AA (Application Association).
        /// </summary>
        public string Password;
        public AssociationStatus associationStatus;
        public StOBISCode SecurityObject_Reference;

        public List<stUser> usersList;
        public stUser currentUser;

        /// <summary>
        /// Get or Set the List of Rights Associated with an specific
        /// attribute / object as per DLMS/COSEM protocol <see cref="OBISCodeRights"/>
        /// </summary>
        public List<OBISCodeRights> OBISCodesReceived
        {
            get { return _OBISCodes; }
            set { _OBISCodes = value; }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Index">OBIS code for specific entity</param>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="No_of_Associations">maximum no of association</param>
        public Class_15(Get_Index Index, byte[] Obis_Code, UInt16 No_of_Associations)
            : base(15, 11, 06, Index, Obis_Code, No_of_Associations)
        {
            _OBISCodes = new List<OBISCodeRights>(1);
        }
        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="obj">Class_15 Object</param>
        public Class_15(Class_15 obj)
            : base(obj)
        {
            if (obj._OBISCodes != null && obj._OBISCodes.Count > 0)
                _OBISCodes = new List<OBISCodeRights>(obj._OBISCodes);
            else
                _OBISCodes = new List<OBISCodeRights>(1);
            
            if (obj.OBISCodesReceived != null && obj.OBISCodesReceived.Count > 0)
                this.OBISCodesReceived = new List<OBISCodeRights>(obj.OBISCodesReceived);

            ClientSAP_Id = obj.ClientSAP_Id;
            ServerSAP_Id = obj.ServerSAP_Id;

            // (obj.applicationContext != null) ? (stApplication_Context)obj.applicationContext.Clone() : null;
            applicationContext = (stApplication_Context)obj.applicationContext.Clone();
            authenticationMechanismName = (stAuthentication_MechanismName)obj.authenticationMechanismName.Clone();
            negotiatedDLMSContext = (obj.negotiatedDLMSContext != null) ? (stNegotiated_xDLMS_Context)obj.negotiatedDLMSContext.Clone() : null;
            
            Password = obj.Password;
            associationStatus = obj.associationStatus;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="OBISCodeStruct">OBIS Code for a specific Object</param>
        public Class_15(StOBISCode OBISCodeStruct)
            : base(OBISCodeStruct, 9, 4)
        {
            _OBISCodes = new List<OBISCodeRights>(1);
        }

        #endregion

        #region Decoders / Encoders

        /// <summary>
        /// Decode Data of this Class which is received in response of get data Request
        /// </summary>
        /// <param name="Data">Received data from Remote site</param>
        /// <param name="array_traverse">Off-Set</param>
        /// <param name="length">Length to decode</param>
        public override void Decode_Data(ref byte[] Data, ref int array_traverse, int length)
        {
            byte[] Obis_code_recieved = null;
            byte current_char;
            OBISCodeRights OBISCoderight = null;

            try
            {
                if (DecodingAttribute == 0x00)
                {
                    for (int index = 0; index < AccessResults.Length; index++)
                        AccessResults[index] = DecodingResult.DataNotPresent;
                }
                else if (AccessResults != null && DecodingAttribute <= AccessResults.Length)
                {
                    SetAttributeDecodingResult(DecodingAttribute, DecodingResult.DataNotPresent);
                }

                byte AttribCount = DecoderAttribute_0(ref Data, ref array_traverse, "Decode_Class15_Data_OBIS_Codes_List");
                DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, "Decode_Class15_Data_OBIS_Codes_List");

                // Version_1 Check
                if (DecodingAttribute == 0x00 && (AttribCount != 09 || AttribCount != base.Attribs_No))
                {
                    // Generate Error and Return
                    SetAttributeDecodingResult(1, DecodingResult.DecodingError);
                    throw new DLMSDecodingException("Wrong Number of Attributes Received", "Decode_Class15_Data_OBIS_Codes_List");
                }

                #region Attribute 0x02 OBIS_Code_List

                ///------------------------------------------------------
                if (DecodingAttribute == 0x02 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check Access Rights
                        if (!base.IsAttribReadable(0x02))
                            SetAttributeDecodingResult(0x02, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(0x02, DecodingResult.DecodingError);
                    }
                    else
                    {
                        if (current_char != (byte)DataTypes._A01_array)
                        {
                            // Generate Error and Return
                            SetAttributeDecodingResult(0x02, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("First element should be 01 (array),invalid identifier (Error Code:{0})"
                                , (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Class15_Data_OBIS_Codes_List");
                        }

                        // OBIS Codes Received Current Associations
                        int Obis_Codes_in_Curret_Association = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);
                        // Set OBIS Codes List
                        if (_OBISCodes == null)
                            _OBISCodes = new List<OBISCodeRights>();
                        _OBISCodes.Clear();

                        // Store information for each Obis Code
                        for (UInt16 Obis_Codes = 0; Obis_Codes < Obis_Codes_in_Curret_Association; Obis_Codes++)
                        {
                            byte[] temp_byte_1 = new byte[3];
                            temp_byte_1[0] = Data[array_traverse++];
                            temp_byte_1[1] = Data[array_traverse++];
                            temp_byte_1[2] = Data[array_traverse++];
                            OBISCoderight = new OBISCodeRights();

                            byte[] to_Compare = new byte[] { 0x2, 0x4, 0x12 };
                            if (!DLMS_Common.Compare_Array(temp_byte_1, to_Compare))
                            {
                                // show error msg and return
                                SetAttributeDecodingResult(0x02, DecodingResult.DecodingError);
                                throw new DLMSDecodingException(String.Format("Error in Start of structure of OBIS Code No,invalid identifier (Error Code:{0})"
                                    , (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Class15_Data_OBIS_Codes_List");
                            }
                            // Store class id in 2 bytes
                            UInt16 Class_ID = (UInt16)((UInt16)(Data[array_traverse]) << 8 |
                                Data[array_traverse + 1]);

                            // Manually increment
                            array_traverse += 2;

                            if (Data[array_traverse++] != (byte)DataTypes._A11_unsigned)
                            {
                                // Generate Error and return
                                SetAttributeDecodingResult(0x02, DecodingResult.DecodingError);
                                throw new DLMSDecodingException(String.Format("This element is not 11 (unsigned),invalid identifier (Error Code:{0})"
                                    , (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Class15_Data_OBIS_Codes_List");
                            }
                            byte version_no = Data[array_traverse++];
                            OBISCoderight.Version = version_no;
                            if (Data[array_traverse++] != (byte)DataTypes._A09_octet_string)
                            {
                                // Generate Error and return
                                SetAttributeDecodingResult(0x02, DecodingResult.DecodingError);
                                throw new DLMSDecodingException(String.Format("This element is not 09 (octet string),invalid identifier (Error Code:{0})",
                                    (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Class15_Data_OBIS_Codes_List");
                            }

                            // save obis code
                            array_traverse--; // for the function to follow it has to be done
                            // DLMS_Common.Save_Data_Subfunction(Data, ref array_traverse, ref Obis_code_recieved);
                            // Set OBIS Code & Class Id
                            Obis_code_recieved = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse, Data.Length);
                            OBISCoderight.OBISIndex = StOBISCode.ConvertFrom(Obis_code_recieved);
                            OBISCoderight.ClassId = Class_ID;

                            //ushort index_of_recvd_Code = DLMS_Common.IndexOf(ref Codes_Class.Obis_Codes, Obis_code_recieved);
                            ///if OBIS CODE NOT FOUND,Index is ushort.Max == 0xFFFF 

                            temp_byte_1[0] = Data[array_traverse++];
                            temp_byte_1[1] = Data[array_traverse++];
                            temp_byte_1[2] = Data[array_traverse++];

                            to_Compare = new byte[] { 0x2, 0x2, 0x1 };
                            if (DLMS_Common.Compare_Arrays(temp_byte_1, to_Compare) == 0)
                            {
                                // show error msg and return
                                SetAttributeDecodingResult(0x02, DecodingResult.DecodingError);
                                throw new DLMSDecodingException(String.Format("Error in start of access rights of Obis Code No,invalid identifier (Error Code:{0})"
                                    , (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Class15_Data_OBIS_Codes_List");
                            }

                            byte No_Of_Attribs = (byte)BasicEncodeDecode.Decode_Length(Data, ref array_traverse);

                            // OBISCode.TotalAttributes = No_Of_Attribs;
                            OBISCoderight.AccessSelectors.Clear();

                            // Save all access rights
                            for (byte _attribs_No = 1; _attribs_No <= No_Of_Attribs; _attribs_No++)
                            {
                                temp_byte_1[0] = Data[array_traverse++];
                                temp_byte_1[1] = Data[array_traverse++];
                                temp_byte_1[2] = Data[array_traverse++];

                                to_Compare = new byte[] { 0x2, 0x3, 0x0F };
                                if (!DLMS_Common.Compare_Array(temp_byte_1, to_Compare))
                                {
                                    // show error msg and return
                                    SetAttributeDecodingResult(0x02, DecodingResult.DecodingError);
                                    throw new DLMSDecodingException(String.Format("Error in structure of access rights of Obis Code No,invalid identifier (Error Code:{0})", (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Class15_Data_OBIS_Codes_List");
                                }

                                int attribId = Data[array_traverse++];

                                if (Data[array_traverse++] != (byte)DataTypes._A16_enum)
                                {
                                    // show error msg and return
                                    SetAttributeDecodingResult(0x02, DecodingResult.DecodingError);
                                    throw new DLMSDecodingException(String.Format("This element is not 16 (Enum) of Obis Code No,invalid identifier (Error Code:{0})",
                                        (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Class15_Data_OBIS_Codes_List");
                                }

                                byte temp_right = Data[array_traverse++];
                                // Add Access Right of current attribute to list
                                OBISCoderight.SetAttribRight(attribId, (Attrib_Access_Modes)temp_right);

                                current_char = Data[array_traverse++];

                                byte no_access_selections;

                                if (current_char == (byte)DataTypes._A00_Null)
                                {
                                    no_access_selections = 0;
                                }
                                else   // Access Selectors
                                {
                                    if (current_char != (byte)DataTypes._A01_array)
                                    {
                                        // show error msg and return
                                        SetAttributeDecodingResult(0x02, DecodingResult.DecodingError);
                                        throw new DLMSDecodingException(String.Format("Access Selector Starting TAG should be 01 (array) of Obis Code No,invalid identifier (Error Code:{0})"
                                            , (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Class15_Data_OBIS_Codes_List");
                                    }
                                    // save no of access selectors
                                    no_access_selections = (byte)BasicEncodeDecode.Decode_Length(Data, ref array_traverse);
                                    byte[] access_selection_types = new byte[no_access_selections];

                                    // save access selection types
                                    for (byte current_selection = 0; current_selection < no_access_selections; current_selection++)
                                    {
                                        if (Data[array_traverse++] != (byte)DataTypes._A0F_integer)
                                        {
                                            // show error msg and return
                                            SetAttributeDecodingResult(0x02, DecodingResult.DecodingError);
                                            throw new DLMSDecodingException(String.Format("Access Selector Starting TAG should be 01 (array) of Obis Code No,invalid identifier (Error Code:{0})"
                                             , (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Class15_Data_OBIS_Codes_List");
                                        }
                                        // save access selection types
                                        else
                                        {
                                            // save to local array
                                            access_selection_types[current_selection] = Data[array_traverse++];
                                        }
                                    }

                                    // end access selectors saving for loop       
                                    // Access Selector Per Attribute ==> Attribute Length | Access Selector Set Values
                                    // OBISCoderight.AccessSelectors.Add(no_access_selections);
                                    // OBISCoderight.AccessSelectors.AddRange(access_selection_types);
                                    SelectiveAccessType lasType = SelectiveAccessType.Not_Applied, CurType = SelectiveAccessType.Not_Applied,
                                        selectType = SelectiveAccessType.Not_Applied;
                                    foreach (var selectorType in access_selection_types)
                                    {
                                        lasType = CurType;
                                        CurType = (SelectiveAccessType)selectorType;
                                    }

                                    if (lasType == SelectiveAccessType.Not_Applied && CurType == SelectiveAccessType.Not_Applied)
                                        selectType = SelectiveAccessType.Not_Applied;
                                    else if (lasType == SelectiveAccessType.Not_Applied && CurType != SelectiveAccessType.Not_Applied)
                                    {
                                        selectType = CurType;
                                    }
                                    else
                                        selectType = SelectiveAccessType.Both_Types;
                                    OBISCoderight.SetAccessSelectorRight(attribId, (byte)selectType);
                                }

                            } // End For loop for saving Access rights
                            ///-----------------------Save Method rights---------------------------->
                            if (Data[array_traverse++] != 01)
                            {
                                // show error msg and return
                                SetAttributeDecodingResult(0x02, DecodingResult.DecodingError);
                                throw new DLMSDecodingException(String.Format("Method structure array tag should be 01 in Obis Code:{0} (Error Code:{1})"

                                    + (Obis_Codes + 1), (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Class15_Data_OBIS_Codes_List");
                            }
                            ///Method rights array is received so store no of methods whose rights will be received
                            byte Implemented_Methods = (byte)BasicEncodeDecode.Decode_Length(Data, ref array_traverse);
                            //OBISCode.TotalMethods = Implemented_Methods;
                            // Method access rights saving for loop
                            for (byte methods = 0; methods < Implemented_Methods; methods++)
                            {

                                byte[] temp_byte = new byte[3];
                                temp_byte_1[0] = Data[array_traverse++];
                                temp_byte_1[1] = Data[array_traverse++];
                                temp_byte_1[2] = Data[array_traverse++];

                                to_Compare = new byte[] { (byte)DataTypes._A02_structure, 0x2, (byte)DataTypes._A0F_integer };
                                if (!DLMS_Common.Compare_Array(temp_byte_1, to_Compare))
                                {
                                    // show error msg and return
                                    SetAttributeDecodingResult(0x02, DecodingResult.DecodingError);
                                    throw new DLMSDecodingException(String.Format("Error in Structure of Method access rights of OBIS Code No: {0}  Class_ID:={1} (Error Code:{2})",
                                         (Obis_Codes + 1), Class_ID, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Class15_Data_OBIS_Codes_List");
                                }

                                // store method_id
                                byte method_id = Data[array_traverse++];
                                current_char = Data[array_traverse++];
                                if (current_char != (byte)DataTypes._A03_boolean && false)  ///By Pass Type Checking Temporary
                                {
                                    // show error msg and return
                                    SetAttributeDecodingResult(0x02, DecodingResult.DecodingError);
                                    throw new DLMSDecodingException(String.Format("Access Mode TAG should be 16 in Method rights structure in Obis Code:{0} (Error Code:{1})",
                                         (Obis_Codes + 1), (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Class15_Data_OBIS_Codes_List");
                                }
                                else
                                {
                                    current_char = Data[array_traverse++];
                                    OBISCoderight.SetMethodRight(method_id, (Method_Access_Modes)current_char);
                                }

                            } // End for loop for method rights
                            _OBISCodes.Add(OBISCoderight);      ///Storing OBIS-Code
                        } // End main for loop per Obis Code
                        SetAttributeDecodingResult(2, DecodingResult.Ready);
                    }
                }
                #endregion
                #region Attribute 0x03 associated_partners_id

                ///------------------------------------------------------
                if (DecodingAttribute == 0x03 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        ///Check access rights
                        if (!base.IsAttribReadable(0x03))
                            SetAttributeDecodingResult(0x03, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(0x03, DecodingResult.DecodingError);

                    }
                    else
                    {
                        current_char = Data[array_traverse++];

                        if (current_char != (byte)DataTypes._A02_structure ||
                            Data[array_traverse++] != 0x02)
                        {
                            throw new DLMSDecodingException("Invalid Structure received ", "Decode_Class15_Data_Associated_Partners");
                        }

                        ClientSAP_Id = (ushort)BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);
                        ServerSAP_Id = (ushort)BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);

                        SetAttributeDecodingResult(0x03, DecodingResult.Ready);
                    }
                }

                #endregion
                #region Attribute 0x04 Application_Context_Name
                //------------------------------------------------------
                if (DecodingAttribute == 0x04 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check access rights
                        if (!base.IsAttribReadable(0x04))
                            SetAttributeDecodingResult(0x04, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(0x04, DecodingResult.DecodingError);

                    }
                    else
                    {
                        if (current_char == (byte)DataTypes._A09_octet_string)
                        {
                            array_traverse--;
                            var appContextData = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse);

                            applicationContext.ctt_element = appContextData[0];
                            applicationContext.Country_element = appContextData[1];
                            applicationContext.Country_name_element = Convert.ToUInt16(appContextData[2] + appContextData[3] << 8);
                            applicationContext.Organization_element = appContextData[4];
                            applicationContext.DLMS_UA_element = appContextData[5];
                            applicationContext.Application_Context_element = appContextData[6];
                            applicationContext.Context_id_element = appContextData[7];
                        }
                        else if (current_char != (byte)DataTypes._A02_structure ||
                                Data[array_traverse++] != 0x07)
                        {
                            throw new DLMSDecodingException("Invalid Structure Received", "Decode_Class15_Application_Context_Name");
                        }

                        // Initialize Application Context Structure
                        applicationContext = new stApplication_Context();

                        applicationContext.ctt_element = (byte)BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);
                        applicationContext.Country_element = (byte)BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);
                        applicationContext.Country_name_element = (ushort)BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);
                        applicationContext.Organization_element = (byte)BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);
                        applicationContext.DLMS_UA_element = (byte)BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);
                        applicationContext.Application_Context_element = (byte)BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);
                        applicationContext.Context_id_element = (byte)BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);

                        SetAttributeDecodingResult(0x04, DecodingResult.Ready);
                    }
                }
                #endregion
                #region Attribute 0x05 xDLMS_context_info

                ///------------------------------------------------------
                if (DecodingAttribute == 0x05 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check Access Rights
                        if (!base.IsAttribReadable(0x05))
                            SetAttributeDecodingResult(0x05, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(0x05, DecodingResult.DecodingError);
                    }
                    else
                    {
                        if (current_char != (byte)DataTypes._A02_structure ||
                            Data[array_traverse++] != 0x06)
                            throw new DLMSDecodingException("Invalid Structure Received", "Decode_Class15_xDLMS_Context_Info");

                        // Initialize Negotiated DLMS Context Structure
                        negotiatedDLMSContext = new stNegotiated_xDLMS_Context();

                        int bitLength = 24;
                        negotiatedDLMSContext.Negotiated_DLMS_Conformance = BasicEncodeDecode.Decode_BitString(Data, ref array_traverse, ref bitLength);
                        negotiatedDLMSContext.Server_Max_Receive_PDU_Size = (ushort)BasicEncodeDecode.Intelligent_Data_Decoder(ref Data,
                                                                                                                               ref array_traverse);
                        negotiatedDLMSContext.Server_Max_Send_PDU_Size = (ushort)BasicEncodeDecode.Intelligent_Data_Decoder(ref Data,
                                                                                                                            ref array_traverse);
                        negotiatedDLMSContext.Negotiated_DLMS_Version_Number = (byte)BasicEncodeDecode.Intelligent_Data_Decoder(ref Data,
                                                                                                                                ref array_traverse);
                        // Decode QOS
                        negotiatedDLMSContext.VAA_Name = (ushort)BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);
                        // Decode CyperInfo
                        BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse);

                        SetAttributeDecodingResult(0x05, DecodingResult.Ready);

                        // throw new DLMSDecodingException("Unable to decode,decoder not implemented yet", 
                        // "Decode_Class15_Data_OBIS_Codes_List");
                    }
                }

                #endregion
                #region Attribute 0x06 Authentication_Mechanism_Name

                //------------------------------------------------------
                if (DecodingAttribute == 0x06 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check Access Rights
                        if (!base.IsAttribReadable(0x06))
                            SetAttributeDecodingResult(0x06, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(0x06, DecodingResult.DecodingError);
                    }
                    else
                    {
                        if (current_char == (byte)DataTypes._A09_octet_string)
                        {
                            #region AuthenticationMechanismName

                            // Initialize Authentication Mechanism Name Structure
                            authenticationMechanismName = new stAuthentication_MechanismName();

                            array_traverse--;
                            var appAuthenticationMechName = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse);

                            authenticationMechanismName.ctt_element = appAuthenticationMechName[0];
                            authenticationMechanismName.Country_element = appAuthenticationMechName[1];
                            authenticationMechanismName.Country_name_element = appAuthenticationMechName[2];// Convert.ToUInt16(appAuthenticationMechName[2] +
                                                                               //                 appAuthenticationMechName[3] << 8);

                            authenticationMechanismName.Organization_element = appAuthenticationMechName[3];
                            authenticationMechanismName.DLMS_UA_element = appAuthenticationMechName[4];
                            authenticationMechanismName.Authen_mechanism_name_element = appAuthenticationMechName[5];
                            authenticationMechanismName.mechanism_id_element = appAuthenticationMechName[6];

                            #endregion
                        }
                        else if (current_char != (byte)DataTypes._A02_structure ||
                                Data[array_traverse++] != 0x07)
                        {
                            throw new DLMSDecodingException("Invalid Structure Received", "Decode_Class15_AuthenticationMechanismName");
                        }

                        #region AuthenticationMechanismName

                        // Initialize AuthenticationMechanismName Structure
                        authenticationMechanismName = new stAuthentication_MechanismName();

                        authenticationMechanismName.ctt_element = (byte)BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);
                        authenticationMechanismName.Country_element = (byte)BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);
                        authenticationMechanismName.Country_name_element = (ushort)BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);
                        authenticationMechanismName.Organization_element = (byte)BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);
                        authenticationMechanismName.DLMS_UA_element = (byte)BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);
                        authenticationMechanismName.Authen_mechanism_name_element = (byte)BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);
                        authenticationMechanismName.mechanism_id_element = (byte)BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);
                        #endregion
                        SetAttributeDecodingResult(0x06, DecodingResult.Ready);
                    }
                }

                #endregion
                #region Attribute 0x07 Secret

                if (DecodingAttribute == 0x07 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check Access Rights
                        if (!IsAttribReadable(0x07))
                            SetAttributeDecodingResult(0x07, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(0x07, DecodingResult.DecodingError);
                    }
                    else
                    {
                        var RawBytes = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse, Data.Length);
                        this.Password = DLMS_Common.ArrayToPrintableString(RawBytes);

                        SetAttributeDecodingResult(0x07, DecodingResult.Ready);
                    }
                }

                #endregion
                #region Attribute 0x08 Association_status

                //------------------------------------------------------
                if (DecodingAttribute == 0x08 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check Access Rights
                        if (!IsAttribReadable(0x08))
                            SetAttributeDecodingResult(0x08, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(0x08, DecodingResult.DecodingError);
                    }
                    else
                    {
                        associationStatus = (AssociationStatus)BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);
                        SetAttributeDecodingResult(0x08, DecodingResult.Ready);
                    }
                }

                #endregion
                #region Attribute 0x09 Security_Setup_Reference

                //------------------------------------------------------
                if (DecodingAttribute == 0x09 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check Access Rights
                        if (!IsAttribReadable(0x09))
                            SetAttributeDecodingResult(0x09, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(0x09, DecodingResult.DecodingError);
                    }
                    else
                    {
                        array_traverse--;
                        // Decode OBIS_Code For Security Setup Reference Object
                        var OBIS_Value = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse);

                        try
                        {
                            this.SecurityObject_Reference = new StOBISCode() { ClassId = 64 };

                            byte[] _StOBISCodeRaw = new byte[08];
                            _StOBISCodeRaw[01] = 64;
                            Buffer.BlockCopy(OBIS_Value, 0, _StOBISCodeRaw, 2, 6);
                            Array.Reverse(_StOBISCodeRaw);
                            ulong _StOBISCodeRawValue = BitConverter.ToUInt64(_StOBISCodeRaw, 0);
                            this.SecurityObject_Reference = (Get_Index)_StOBISCodeRawValue;
                        }
                        // Set Null Val As Dummy Object
                        catch
                        {
                            this.SecurityObject_Reference = Get_Index.Dummy;
                        }

                        SetAttributeDecodingResult(0x09, DecodingResult.Ready);
                    }
                }

                #endregion

                // Version 1 Compatibility Check
                if (DecodingAttribute == 0 && AttribCount <= 09)
                {
                    return;
                }

                #region Attribute 0x0A Users_List

                ///------------------------------------------------------
                if (DecodingAttribute == 0x0A || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check Access Rights
                        if (!base.IsAttribReadable(0x0A))
                            SetAttributeDecodingResult(0x0A, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(0x0A, DecodingResult.DecodingError);
                    }
                    else
                    {
                        if (current_char != (byte)DataTypes._A01_array)
                        {
                            // Generate Error and return
                            SetAttributeDecodingResult(0x0A, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("First element should be 01 (array),invalid identifier (Error Code:{0})"
                                , (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Class15_Data_OBIS_Codes_List");
                        }

                        // OBIS Codes Received in Current Associations
                        int user_list_Count = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);


                        // Set User List
                        if (usersList == null)
                            usersList = new List<stUser>();
                        usersList.Clear();

                        // Store information for each Obis Code
                        for (UInt16 User_Count = 0; User_Count < user_list_Count; User_Count++)
                        {
                            byte[] temp_byte_1 = new byte[3];
                            temp_byte_1[0] = Data[array_traverse++];
                            temp_byte_1[1] = Data[array_traverse++];
                            temp_byte_1[2] = Data[array_traverse++];
                            OBISCoderight = new OBISCodeRights();

                            byte[] to_Compare = new byte[] { 0x02, 0x02, (byte)DataTypes._A11_unsigned };

                            if (!DLMS_Common.Compare_Array(temp_byte_1, to_Compare))
                            {
                                // show error msg and return
                                SetAttributeDecodingResult(0x02, DecodingResult.DecodingError);
                                throw new DLMSDecodingException(String.Format("Error in Structure user list,invalid identifier (Error Code:{0})"
                                   , (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Class15_Data_OBIS_Codes_List");

                            }

                            stUser user = new stUser();

                            user.UserId = Data[array_traverse++];
                            var UserName = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse);
                            user.UserName = Encoding.ASCII.GetString(UserName);

                            usersList.Add(user);
                        }
                    }
                    SetAttributeDecodingResult(0x0A, DecodingResult.Ready);
                }

                #endregion
                #region Attribute 0x0B Current User

                ///------------------------------------------------------
                if (DecodingAttribute == 0x0B || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check Access Rights
                        if (!base.IsAttribReadable(0x0B))
                            SetAttributeDecodingResult(0x0B, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(0x0B, DecodingResult.DecodingError);
                    }
                    else
                    {
                        // Set Current User
                        byte[] temp_byte_1 = new byte[3];
                        temp_byte_1[0] = Data[array_traverse++];
                        temp_byte_1[1] = Data[array_traverse++];
                        temp_byte_1[2] = Data[array_traverse++];
                        OBISCoderight = new OBISCodeRights();

                        byte[] to_Compare = new byte[] { 0x02, 0x02, (byte)DataTypes._A11_unsigned };

                        if (!DLMS_Common.Compare_Array(temp_byte_1, to_Compare))
                        {
                            // show error msg and return
                            SetAttributeDecodingResult(0x02, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("Error in Structure Current User,invalid identifier (Error Code:{0})"
                                   , (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Class15_Data_OBIS_Codes_List");
                        }

                        stUser user = new stUser();

                        user.UserId = Data[array_traverse++];
                        var UserName = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse);
                        user.UserName = Encoding.ASCII.GetString(UserName);

                        currentUser = user;
                    }
                    SetAttributeDecodingResult(0x0B, DecodingResult.Ready);
                }

                #endregion
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                {
                    throw new DLMSDecodingException(String.Format("Error occurred while decoding (Error Code:{0})",
                            (int)DLMSErrors.ErrorDecoding_Type), "Decode_Class15_Data_OBIS_Codes_List");

                }
            }
        }

        /// <summary>
        /// Set the Request Encoder
        /// </summary>
        /// <returns>byte[]</returns>
        public override byte[] Encode_Data()
        {
            EncodedRaw = new List<byte>(0x0A);
            try
            {
                //------------------------------------------------------
                EncoderAttribute_0();
                EncoderLogicalName();
                //------------------------------------------------------
                #region Attribute 0x02

                if (EncodingAttribute == 0x02 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x02);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode object list,No Access (Error Code:{0})",
                           (int)DLMSErrors.Insufficient_Priviledge), "EncodeData_Class_15");
                    }
                    else             ///Encode Here Data
                    {
                        throw new DLMSDecodingException(String.Format("Error encoding object list,Encoder not implemented yet (Error Code:{0})",
                            (int)DLMSErrors.Invalid_EncoderNotIncluded), "Decode_Class15_Data_OBIS_Codes_List");
                    }

                }

                #endregion
                #region Secret

                if (EncodingAttribute == 0x07 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x07);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode secret,No Access (Error Code:{0})",
                            (int)DLMSErrors.Insufficient_Priviledge), "EncodeData_Class_1");
                    }
                    else             ///Encode Here Data
                    {
                        var rawBytes = DLMS_Common.PrintableStringToByteArray(this.Password);
                        EncodedRaw.AddRange(BasicEncodeDecode.Encode_OctetString(rawBytes, DataTypes._A09_octet_string));
                    }
                }

                #endregion
                return EncodedRaw.ToArray();
            }
            finally
            {
                ///Dispose Resource
                EncodedRaw = null;
            }
        }

        public override int Decode_Parameters(ref byte[] Data, ref int array_traverse, int length)
        {
            byte current_char = 0;

            try
            {
                #region Secret

                if (base.MethodInvokeId == Reply_HLS_Authentication ||
                    base.MethodInvokeId == Change_HLS_Secret)
                {
                    current_char = Data[array_traverse];

                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check Access Rights
                        throw new DLMSDecodingException(String.Format("Error occurred while decoding Challenge String StoC (Error Code:{0})",
                                                        (int)DLMSErrors.ErrorDecoding_Type), "Decode_Class15_Data_Reply_HLS_Authentication");
                    }
                    else
                    {
                        var RawBytes = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse, Data.Length);
                        this.Password = DLMS_Common.ArrayToHexString(RawBytes);

                        SetAttributeDecodingResult(0x07, DecodingResult.Ready);
                    }
                }

                return 1;
                #endregion
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                {
                    throw new DLMSDecodingException(String.Format("Error occurred while decoding (Error Code:{0})",
                                                   (int)DLMSErrors.ErrorDecoding_Type), "Decode_Class15_Data_Decode_Parameters");
                }
            }
        }

        public override byte[] Encode_Parameters()
        {
            EncodedRaw = new List<byte>(0x0A);
            try
            {
                #region Secret

                if (base.MethodInvokeId == Reply_HLS_Authentication ||
                    base.MethodInvokeId == Change_HLS_Secret)
                {
                    if (String.IsNullOrEmpty(Password) ||
                        String.IsNullOrWhiteSpace(Password))
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else   // Encode Here Data
                    {
                        var rawBytes = DLMS_Common.String_to_Hex_array(this.Password);
                        EncodedRaw.AddRange(BasicEncodeDecode.Encode_OctetString(rawBytes, DataTypes._A09_octet_string));
                    }
                }

                #endregion
                return EncodedRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                {
                    throw new DLMSEncodingException(String.Format("Error occurred while Encode_Parameters (Error Code:{0})",
                                                   (int)DLMSErrors.ErrorEncoding_Type), "Encode_Parameters_Class15");
                }
            }
        }

        #endregion

        #region Member Methods

        public override object Clone()
        {
            Class_15 cloned = new Class_15(this);

            if (OBISCodesReceived != null && OBISCodesReceived.Count > 0)
                cloned.OBISCodesReceived.Clear();

            if (_OBISCodes != null && _OBISCodes.Count > 0)
                cloned._OBISCodes.Clear();

            return cloned;
        }

        /// <summary>
        /// Returns the String representation of the Class_15 object
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            String baseStr = base.ToString();
            StringBuilder strVal = new StringBuilder();
            StringBuilder ObisCodes = new StringBuilder();
            int ObisCodeCount = 0;

            if (OBISCodesReceived != null && OBISCodesReceived.Count > 0 && GetAttributeDecodingResult(2) == DecodingResult.Ready)
            {
                ObisCodeCount = OBISCodesReceived.Count;
                foreach (OBISCodeRights ObisCode in OBISCodesReceived)///Append Codes in List & Code Name
                {
                    try
                    {
                        // int indexOf = DLMS_Common.IndexOf(ref DLMS.Codes_Class.Obis_Codes, ObisCode.OBISCode);
                        // OBISCodeName = (indexOf == ushort.MaxValue)? Get_Index.Dummy: (Get_Index)indexOf;

                        ObisCodes.AppendFormat(",[{0:X3}]:{1}", ObisCode.OBISIndex.ClassId, DLMS_Common.ArrayToHexString(ObisCode.OBISCode));
                        ObisCodes.AppendFormat(" {0}", (ObisCode.OBISIndex.IsValidate) ? ObisCode.OBISIndex.OBISIndex.ToString() : "UnKnown");
                    }
                    catch (Exception) { }

                }
            }

            strVal.AppendFormat(",Received OBIS Codes Count:{0}:{1}", ObisCodeCount, GetAttributeDecodingResult(2));
            strVal.Append(ObisCodes.ToString());
            return baseStr + strVal;
        }

        #endregion
    }
}
