using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS.Comm;

namespace DLMS
{
    /// <summary>
    /// Single action schedule (class_id: 22, version: 0) allows modeling the execution of periodic actions within a meter.
    /// modeling the execution of periodic actions within a meter. Such actions are not necessarily linked to tariffication.
    /// </summary>
    public class Class_22 : Base_Class
    {
        #region Data_Members

        public StOBISCode ExecutedScriptLogicalName;
        public UInt16 ExecutedScriptSelector;
        public SingleActionScheduleType Type;
        
        /// <summary>
        /// Specifies the time and the date when the script is executed.
        /// </summary>
        public List<StDateTime> executionTimeList;  // 0x04 

        #endregion

        #region Constructor

        public Class_22(Get_Index Index, byte[] Obis_Code, UInt16 No_of_Associations)
            : base(22, 4, 0, Index, Obis_Code, No_of_Associations)
        {
            // Dummy
            ExecutedScriptLogicalName = new StOBISCode();
            this.Type = SingleActionScheduleType.SingleActionScheduleType1;
            executionTimeList = new List<StDateTime>(1);
        }

        public Class_22(byte[] Obis_Code, byte Attribute_recieved)
            : base(22, 4, 0, Obis_Code)
        {
            // Dummy
            ExecutedScriptLogicalName = new StOBISCode();
            this.Type = SingleActionScheduleType.SingleActionScheduleType1;
            executionTimeList = new List<StDateTime>(1);

            DecodingAttribute = Attribute_recieved;
        }

        public Class_22(StOBISCode OBISCodeStruct)
            : base(OBISCodeStruct, 4, 0)
        {
            // Dummy
            ExecutedScriptLogicalName = new StOBISCode();
            this.Type = SingleActionScheduleType.SingleActionScheduleType1;
            executionTimeList = new List<StDateTime>(1);
        }

        /// <summary>
        /// Copy Constructor Class_22
        /// </summary>
        /// <param name="obj"></param>
        public Class_22(Class_22 obj)
            : base(obj)
        {
            this.ExecutedScriptLogicalName = new StOBISCode();

            if (obj.ExecutedScriptLogicalName != null)
                this.ExecutedScriptLogicalName.OBIS_Value = obj.ExecutedScriptLogicalName.OBIS_Value;

            this.ExecutedScriptSelector = obj.ExecutedScriptSelector;
            this.Type = obj.Type;

            this.executionTimeList = new List<StDateTime>(obj.executionTimeList);
        }

        #endregion

        #region Decoder/Encoders

        public override void Decode_Data(ref byte[] Data, ref int array_traverse, int length)
        {
            byte[] Obis_code_recieved = null;
            byte current_char;
            //------------------------------------------------------
            try
            {
                // SET All Attribute Access Status Results
                if (DecodingAttribute == 0x00)
                {
                    for (int index = 0; index < AccessResults.Length; index++)
                        AccessResults[index] = DecodingResult.DataNotPresent;
                }
                else if (AccessResults != null && DecodingAttribute <= AccessResults.Length)
                {
                    SetAttributeDecodingResult(DecodingAttribute, DecodingResult.DataNotPresent);
                }
                DecoderAttribute_0(ref Data, ref array_traverse, "Decode_Data_Action_Schedule_Class_22");
                DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, "Decode_Data_Action_Schedule_Class_22");

                #region Attribute 0x02 Execution_Script

                if (DecodingAttribute == 0x02 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        if (!IsAttribReadable(02))
                            SetAttributeDecodingResult(2, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(2, DecodingResult.DecodingError);
                    }
                    else
                    {
                        if (current_char != (byte)DataTypes._A02_structure ||
                            Data[array_traverse++] != 0x02)
                        {
                            throw new DLMSEncodingException(String.Format("Invalid Structure Received,ExecutedScript", EncodingType), "Decode_Data_Action_Schedule_Class_22");
                        }

                        var exeScript_OBIS = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse);
                        ExecutedScriptSelector = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                        // Set Script Logical Name
                        ExecutedScriptLogicalName = StOBISCode.ConvertFrom(exeScript_OBIS);

                        SetAttributeDecodingResult(2, DecodingResult.Ready);
                    }

                }

                #endregion
                #region Attribute 0x03 Execution_Time_Type

                if (DecodingAttribute == 0x03 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        if (!IsAttribReadable(0x03))
                            SetAttributeDecodingResult(0x03, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(0x03, DecodingResult.DecodingError);
                    }
                    else
                    {
                        array_traverse--;
                        var _Val = BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);
                        this.Type = (SingleActionScheduleType)_Val;

                        SetAttributeDecodingResult(0x03, DecodingResult.Ready);
                    }
                }

                #endregion
                #region Attribute 0x04 Execution_Time

                if (DecodingAttribute == 0x04 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    /// null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        if (!IsAttribReadable(4))
                            SetAttributeDecodingResult(4, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(4, DecodingResult.DecodingError);
                    }
                    else
                    {
                        if (this.executionTimeList == null)
                            this.executionTimeList = new List<StDateTime>(1);
                        else if (this.executionTimeList.Count > 0)
                            executionTimeList.Clear();
                        // Decode ExecutionTime Array
                        // <DataType DataTypes._A01_array + Length>
                        if (current_char != (byte)DataTypes._A01_array)
                            throw new DLMSDecodingException("Array type is expected,unable to decode", "Decode_Data_Class_22");
                        int Count = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);
                        for (int index = 0; index < Count; index++)
                        {
                            StDateTime item = new StDateTime();
                            // Decode ExecutionTime Structure <DataType Structure,2>
                            current_char = Data[array_traverse++];
                            if (current_char != (byte)DataTypes._A02_structure ||
                                Data[array_traverse++] != 2)
                                throw new DLMSDecodingException("Execution_Time structure invalid,unable to decode", "Decode_Data_Class_22");
                            // Decode Time Value
                            item.DecodeRawBytes(Data, ref array_traverse);
                            // Decode Date Value
                            item.DecodeRawBytes(Data, ref array_traverse);
                            executionTimeList.Add(item);
                        }
                    }
                    SetAttributeDecodingResult(4, DecodingResult.Ready);
                }

                #endregion
                // DLMS_Common.Data_Array_resizer(ref Data, array_traverse);
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException("Error occurred while decoding data", "Decode_Data_Action_Schedule_Class_22", ex);
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
                #region Attribute 0x02 Execution_Script

                if (EncodingAttribute == 0x02 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x02);
                    if (EncodingAttribute == 0x00 &&
                        !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode Execution_Script,No Access", EncodingType),
                            "Encode_Data_Action_Schedule_Class_22");
                    }
                    // Encode Here Data
                    else if (EncodingAttribute == 0x02)
                    {
                        EncodedRaw.Add((byte)DataTypes._A02_structure);
                        EncodedRaw.Add(0x02);

                        // Execute Script OBIS_CODE
                        var encode_OBIS = BasicEncodeDecode.Encode_OctetString(ExecutedScriptLogicalName.OBISCode, DataTypes._A09_octet_string);
                        EncodedRaw.AddRange(encode_OBIS);

                        // SCRIPT Selector
                        EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, ExecutedScriptSelector));
                    }
                }

                #endregion
                //------------------------------------------------------ 
                #region Attribute 0x03 Execution_Time_Type

                if (EncodingAttribute == 0x03 ||
                    EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x03);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode Execution_Time_Type,No Access", EncodingType), "Encode_Data_Action_Schedule_Class_22");
                    }
                    // Encode Here Data
                    else if (EncodingAttribute == 0x03)
                    {
                        EncodedRaw.Add((byte)DataTypes._A16_enum);
                        EncodedRaw.Add((byte)this.Type);
                    }
                }

                #endregion
                //------------------------------------------------------ 
                #region Attribute 0x04 Execution_Time

                if (EncodingAttribute == 0x04 ||
                    EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x04);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode Execution_Time,No Access", EncodingType),
                                                        "Encode_Data_Action_Schedule_Class_22");
                    }
                    // Encode Here Data
                    else if (EncodingAttribute == 0x04)
                    {
                        if (this.executionTimeList == null || this.executionTimeList.Count <= 0)
                            throw new DLMSEncodingException(String.Format("Unable to encode Execution_Time,Null Reference", EncodingType), "Encode_Data_Action_Schedule_Class_22");
                        byte[] tArray = null;
                        // Encode ExecutionTime Array
                        // <DataType Array + Length>
                        EncodedRaw.Add((byte)DataTypes._A01_array);
                        BasicEncodeDecode.Encode_Length(ref tArray, (ushort)this.executionTimeList.Count);
                        EncodedRaw.AddRange(tArray);
                        foreach (var item in executionTimeList)
                        {
                            if (item == null)
                                throw new DLMSEncodingException(String.Format("Unable to encode Execution_Time,Null Reference", EncodingType), "Encode_Data_Action_Schedule_Class_22");
                            ///Encode ExecutionTime <DataType Structure,2>
                            tArray = new byte[] { (byte)DataTypes._A02_structure, 2 };
                            EncodedRaw.AddRange(tArray);
                            tArray = item.EncodeRawBytes(StDateTime.DateTimeType.Time);
                            EncodedRaw.AddRange(tArray);
                            tArray = item.EncodeRawBytes(StDateTime.DateTimeType.Date);
                            EncodedRaw.AddRange(tArray);
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

        #endregion

        #region Member Methods

        public override object Clone()
        {
            Class_22 cloned = new Class_22(this);
            return cloned;
        }

        #endregion
    }
}
