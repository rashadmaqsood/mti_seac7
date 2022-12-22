using DLMS.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DLMS
{
    /// <summary>
    /// Script table (class_id: 9, version: 0) Class allows modeling the triggering of a series of actions by executing scripts using the execute (data) method.
    /// Script table objects contain a table of script entries.
    /// Each entry consists of a script identifier and a series of action specifications. 
    /// An action specification activates a method or modifies an attribute of a COSEM object within the logical device.
    /// A certain script may be activated by other COSEM objects within the same logical device or from the outside.
    /// If two scripts have to be executed at the same time instance, then the one with the smaller index is executed first.
    /// </summary>
    public class Class_9 : Base_Class
    {
        public const byte Execute_Script = 0x01;

        [XmlIgnore()]
        public List<Script> Scripts;
        [XmlIgnore()]
        public ushort ExecutedScriptSelector;

        #region Constructor

        public Class_9(Get_Index Index, byte[] Obis_Code, UInt16 No_of_Associations)
            : base(9, 2, 1, Index, Obis_Code, No_of_Associations)
        {
            Scripts = new List<Script>();
        }

        public Class_9(byte[] Obis_Code, byte Attribute_recieved)
            : base(9, 2, 1, Obis_Code)
        {
            DecodingAttribute = Attribute_recieved;
            Scripts = new List<Script>();
        }

        public Class_9(StOBISCode OBISCodeStruct)
            : base(OBISCodeStruct, 2, 1)
        {
            Scripts = new List<Script>();
        }

        /// <summary>
        /// Copy Constructor Class_9
        /// </summary>
        /// <param name="obj"></param>
        public Class_9(Class_9 obj)
            : base(obj)
        {
            Scripts = new List<Script>(obj.Scripts.Count);
            // Script 
            foreach (var scpt in Scripts)
            {
                this.Scripts.Add(new Script(scpt));
            }
        }

        #endregion

        #region Member Methods

        /// <summary>
        /// Decode Data of this Class which is received
        /// in response of get data Request
        /// </summary>
        /// <param name="Data">Received data from Remote site</param>
        /// <param name="array_traverse">Off-Set</param>
        /// <param name="length">Length to decode</param>
        public override void Decode_Data(ref byte[] Data, ref int array_traverse, int length)
        {
            // UInt16 array_traverse = 0;
            byte[] Obis_code_recieved = null;
            byte current_char = 0;

            if (DecodingAttribute == 0x00)
            {
                for (int index = 0; index < AccessResults.Length; index++)
                    AccessResults[index] = DecodingResult.DataNotPresent;
            }
            else if (AccessResults != null && DecodingAttribute <= AccessResults.Length)
            {
                SetAttributeDecodingResult(DecodingAttribute, DecodingResult.DataNotPresent);
            }

            //------------------------------------------------------
            try
            {
                DecoderAttribute_0(ref Data, ref array_traverse, "Decode_Data_Class_9_Script_Table");
                DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, "Decode_Data_Class_9_Script_Table");
                #region Attribute_0x02

                if (DecodingAttribute == 0x02 ||
                    DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check Access Rights
                        if (!IsAttribReadable(0x02))
                            SetAttributeDecodingResult(0x02, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(0x02, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} Invalid Identifier of Value (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_9_Script_Table");
                        }

                        if (Scripts != null)
                            Scripts.Clear();
                    }
                    else
                    {
                        if (current_char != (byte)DataTypes._A01_array)
                        {
                            throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode ,_A01_Array Type Expected",
                                OBISIndex, OBISIndex.OBISIndex), "Decode_Data_Class_9_Script_Table");
                        }

                        int scripts_Length = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);

                        // Reset Prev Scripts
                        if (Scripts == null)
                            Scripts = new List<Script>();
                        else
                            Scripts.Clear();

                        Script Current_Script = null;
                        ScriptAction Current_Script_Action = null;

                        for (int indexer = 0; indexer < scripts_Length; indexer++)
                        {
                            if (current_char != (byte)DataTypes._A02_structure &&
                                Data[array_traverse++] != 0x02)
                                throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode Invalid Structure", OBISIndex, OBISIndex.OBISIndex),
                                                                "Decode_Data_Class_9_Script_Table");

                            Current_Script = new Script();
                            Scripts.Add(Current_Script);
                            // Script Id
                            Current_Script.Id = Convert.ToInt32(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));

                            // Action 
                            current_char = Data[array_traverse++];

                            // null Data
                            if (current_char == (byte)DataTypes._A00_Null) continue; // Skip Error
                            else if (current_char != (byte)DataTypes._A01_array)
                                throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode, _A01_Array Type Expected",
                                                                OBISIndex, OBISIndex.OBISIndex), "Decode_Data_Class_9_Script_Table");

                            int script_Act_Length = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);
                            for (int indexer_ActScript = 0;
                                 indexer_ActScript < script_Act_Length; indexer_ActScript++)
                            {
                                Current_Script_Action = new ScriptAction();
                                Current_Script.Actions.Add(Current_Script_Action);

                                // Action Script
                                current_char = Data[array_traverse++];
                                // null Data
                                if (current_char == (byte)DataTypes._A00_Null) continue; // Skip Error
                                else if (current_char != (byte)DataTypes._A02_structure ||
                                         Data[array_traverse++] != 0x05)
                                    throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode Invalid Structure",
                                        OBISIndex, OBISIndex.OBISIndex), "Decode_Data_Class_9_Script_Table");

                                // service_id: enum,
                                current_char = Data[array_traverse++];
                                if (current_char == (byte)DataTypes._A00_Null) ; // Skip Error
                                else
                                {
                                    if (current_char != (byte)DataTypes._A16_enum)
                                        throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode, _A16_enum Type Expected",
                                            OBISIndex, OBISIndex.OBISIndex), "Decode_Data_Class_9_Script_Table");

                                    Current_Script_Action.Type = (ScriptActionType)Data[array_traverse++];
                                }

                                ushort classId = 0;
                                byte[] logicalName = null;
                                Current_Script_Action.LogicalName = Get_Index.Dummy;

                                // Class_Id
                                current_char = Data[array_traverse++];
                                if (current_char == (byte)DataTypes._A00_Null) ; // Skip Error
                                else
                                {
                                    if (current_char != (byte)DataTypes._A12_long_unsigned)
                                        throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode, _A12_long_unsigned Type Expected",
                                                                        OBISIndex, OBISIndex.OBISIndex), "Decode_Data_Class_9_Script_Table");

                                    array_traverse--;
                                    classId = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                                }

                                // OBISCode Value
                                current_char = Data[array_traverse++];
                                if (current_char == (byte)DataTypes._A00_Null) logicalName = null; // Skip Error
                                else
                                {
                                    if (current_char != (byte)DataTypes._A09_octet_string)
                                        throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode, _A09_octet_string Type Expected",
                                                                        OBISIndex, OBISIndex.OBISIndex), "Decode_Data_Class_9_Script_Table");

                                    array_traverse--;
                                    logicalName = null;

                                    logicalName = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse);
                                    var _LogicalNameVar = StOBISCode.ConvertFrom(logicalName);
                                    _LogicalNameVar.ClassId = classId;

                                    Current_Script_Action.LogicalName = _LogicalNameVar;
                                }

                                // index: integer
                                current_char = Data[array_traverse++];
                                if (current_char == (byte)DataTypes._A00_Null) ; // Skip Error
                                else
                                {
                                    if (current_char != (byte)DataTypes._A0F_integer)
                                        throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode, _A0F_integer Type Expected",
                                                                        OBISIndex, OBISIndex.OBISIndex), "Decode_Data_Class_9_Script_Table");

                                    array_traverse--;
                                    Current_Script_Action.Index = Convert.ToInt32(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                                }

                                // parameter: DLMS Data Type Class_1 With Attribute 0x02
                                current_char = Data[array_traverse++];
                                if (current_char == (byte)DataTypes._A00_Null)  // Skip Error
                                {
                                    Current_Script_Action.Parameter = null;
                                    Current_Script_Action.ParameterDataType = DataTypes._A00_Null;
                                }
                                else
                                {
                                    array_traverse--;

                                    if (Current_Script_Action.Parameter == null)
                                        Current_Script_Action.Parameter = new Class_1(Get_Index.Dummy);

                                    Current_Script_Action.Parameter.DecodingAttribute = 0x02;
                                    Current_Script_Action.Parameter.Decode_Data(ref Data, ref array_traverse, Data.Length);

                                    Current_Script_Action.ParameterDataType = Current_Script_Action.Parameter.DecodingType;
                                }
                            }

                            SetAttributeDecodingResult(2, DecodingResult.Ready);
                        }

                    }

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
                    throw new DLMSException(String.Format("{0}_{1}_{2}_{3} (Error Code:{4})", "Error occurred while decoding data",
                        OBISIndex, OBISIndex.OBISIndex, "Decode_Data_Class_9_Script_Table", (int)DLMSErrors.ErrorDecoding_Type), ex);
                }
            }
        }

        public override byte[] Encode_Data()
        {
            try
            {
                EncodedRaw = new List<byte>(0x0A);
                //------------------------------------------------------
                EncoderAttribute_0();
                EncoderLogicalName();
                //------------------------------------------------------ 
                #region Attribute 0x02 Scripts

                if (EncodingAttribute == 0x02 ||
                    EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x02);
                    if (EncodingAttribute == 0x00 &&
                        !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to Encode Scripts,No Access", EncodingType),
                                                        "Encode_Data_Class_9_Script_Table");
                    }
                    // Encode Here Data
                    else if (EncodingAttribute == 0x02)
                    {
                        if (this.Scripts == null || this.Scripts.Count <= 0)
                            throw new DLMSEncodingException(String.Format("Unable to encode Scripts,Null Reference", EncodingType), "Encode_Data_Class_9_Script_Table");

                        byte[] tArray = null;
                        // Encode Scripts Array
                        // <DataType Array + Length>
                        EncodedRaw.Add((byte)DataTypes._A01_array);
                        BasicEncodeDecode.Encode_Length(ref tArray, (ushort)this.Scripts.Count);
                        EncodedRaw.AddRange(tArray);

                        foreach (var item in Scripts)
                        {
                            if (item == null)
                                throw new DLMSEncodingException(String.Format("Unable to encode Script,Null Reference", EncodingType), "Encode_Data_Class_9_Script_Table");

                            // Encode Script <DataType Structure,2>
                            tArray = new byte[] { (byte)DataTypes._A02_structure, 2 };
                            EncodedRaw.AddRange(tArray);

                            // Script Id
                            tArray = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, item.Id);
                            EncodedRaw.AddRange(tArray);

                            // Action 
                            if (item.Actions == null || item.Actions.Count <= 0)
                                EncodedRaw.Add((byte)DataTypes._A00_Null);
                            else
                            {
                                EncodedRaw.Add((byte)DataTypes._A01_array);
                                BasicEncodeDecode.Encode_Length(ref tArray, (ushort)item.Actions.Count);
                                EncodedRaw.AddRange(tArray);

                                foreach (var scptAct in item.Actions)
                                {
                                    // Encode Script <DataType Structure,0x02>
                                    tArray = new byte[] { (byte)DataTypes._A02_structure, 05 };
                                    EncodedRaw.AddRange(tArray);

                                    // null Action Script
                                    if (scptAct == null)
                                    {
                                        tArray = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00 };
                                        EncodedRaw.AddRange(tArray);
                                    }
                                    else
                                    {
                                        // Service Id
                                        EncodedRaw.Add((byte)DataTypes._A16_enum);
                                        EncodedRaw.Add((byte)scptAct.Type);

                                        if (scptAct.LogicalName == null ||
                                            scptAct.LogicalName == Get_Index.Dummy)
                                        {
                                            // null Class Id, null Logical Name
                                            tArray = new byte[] { 0x00, 0x00 };
                                            EncodedRaw.AddRange(tArray);
                                        }
                                        else
                                        {
                                            // Class Id
                                            tArray = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, scptAct.LogicalName.ClassId);
                                            EncodedRaw.AddRange(tArray);

                                            // Logical Name
                                            tArray = BasicEncodeDecode.Encode_OctetString(scptAct.LogicalName.OBISCode, DataTypes._A09_octet_string);
                                            EncodedRaw.AddRange(tArray);
                                        }

                                        // Index
                                        tArray = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A0F_integer, scptAct.Index);
                                        EncodedRaw.AddRange(tArray);

                                        // Parameter
                                        // null value
                                        if (scptAct.Parameter == null)
                                            EncodedRaw.Add((byte)DataTypes._A00_Null);
                                        else
                                        {
                                            // FIX DLMS Data Interface Class Encoder Parameter
                                            scptAct.Parameter.EncodingAttribute = 0x02;

                                            if (scptAct.Parameter.Value_Array != null &&
                                                scptAct.Parameter.Value_Array.Length > 0)
                                            {
                                                scptAct.Parameter.EncodingType = DataTypes._A01_array;
                                                scptAct.Parameter.DecodingSubType = scptAct.ParameterDataType;
                                            }
                                            else
                                                scptAct.Parameter.EncodingType = scptAct.ParameterDataType;

                                            tArray = scptAct.Parameter.Encode_Data();
                                            EncodedRaw.AddRange(tArray);
                                        }
                                    }
                                }
                            }

                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                byte[] dtBuf = EncodedRaw.ToArray<byte>();
                EncodedRaw = null;
                return dtBuf;
            }
            catch (Exception ex)
            {
                if (ex is DLMSEncodingException)
                {
                    throw ex;
                }
                else
                    throw new DLMSException(String.Format("{0}_{1}", "Error occurred while encoding data", "Encode_Data_Action_Schedule_Class_22"), ex);
            }
        }

        public override byte[] Encode_Parameters()
        {
            EncodedRaw = new List<byte>(0x0A);
            try
            {
                #region Execute_Script

                if (base.MethodInvokeId == Execute_Script)
                {
                    if (this.ExecutedScriptSelector <= 0 ||
                        this.ExecutedScriptSelector > ushort.MaxValue)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else   // Encode Data
                    {
                        EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, ExecutedScriptSelector));
                    }
                }

                #endregion

                return EncodedRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex is DLMSEncodingException)
                {
                    throw ex;
                }
                else
                {
                    throw new DLMSEncodingException(String.Format("Error occurred while Encode_Parameters (Error Code:{0})",
                                                   (int)DLMSErrors.ErrorEncoding_Type), "Encode_Parameters_Class_09");
                }
            }
        }

        #endregion

        #region Member Methods

        public override object Clone()
        {
            Class_9 cloned = new Class_9(this);
            return cloned;
        }

        #endregion
    }
}
