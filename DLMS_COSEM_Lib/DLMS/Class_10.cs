using DLMS.Comm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DLMS
{
    /// <summary>
    /// Schedule: This Interface Class allows modeling the execution of periodic actions within a meter.
    /// This IC, together with the IC “Special days”, allows modeling time- and date-driven activities within a device.
    /// </summary>
    public class Class_10 : Base_Class
    {
        public const int Enable_Execute = 1;
        public const int Insert_Execute = 2;
        public const int Delete_Execute = 3;

        public const int Max_ScheduleEntryCount = 9999;

        #region Data_Members

        // Variables For Method Invoke
        public ScheduleEntry Current_Entry = null;
        public ScheduleEntryRange RangeA;
        public ScheduleEntryRange RangeB;

        /// <summary>
        /// Specifies the scripts to be executed at given times
        /// </summary>        
        [XmlIgnore()]
        public List<ScheduleEntry> Entries;

        #endregion

        #region Constructor

        public Class_10(Get_Index Index, byte[] Obis_Code, UInt16 No_of_Associations)
            : base(10, 02, 03, Index, Obis_Code, No_of_Associations)
        {
            Entries = new List<ScheduleEntry>(20);

            Current_Entry = null;
        }

        public Class_10(byte[] Obis_Code, byte Attribute_recieved)
            : base(10, 02, 03, Obis_Code)
        {
            Entries = new List<ScheduleEntry>(20);

            Current_Entry = null;
        }

        public Class_10(StOBISCode OBISCodeStruct)
            : base(OBISCodeStruct, 02, 03)
        {
            Entries = new List<ScheduleEntry>(20);
            Current_Entry = null;
        }

        /// <summary>
        /// Copy Constructor Class_10
        /// </summary>
        /// <param name="obj"></param>
        public Class_10(Class_10 OtherObj)
            : base(OtherObj)
        {
            Entries = new List<ScheduleEntry>(20);
            Current_Entry = null;
            // Deep Copy All ScheduleEntry Data
            if (OtherObj.Entries != null &&
                OtherObj.Entries.Count > 0)
            {
                foreach (var schEntry in Entries)
                {
                    OtherObj.Entries.Add(new ScheduleEntry(schEntry));
                }
            }
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
                DecoderAttribute_0(ref Data, ref array_traverse, "Decode_Data_Schedule_Class_10");
                DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, "Decode_Data_Schedule_Class_10");

                #region Attribute 0x02 Schedule Entries

                if (DecodingAttribute == 0x02 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        if (!IsAttribReadable(02))
                            SetAttributeDecodingResult(02, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(02, DecodingResult.DecodingError);
                    }
                    else
                    {
                        // Reset Entries
                        if (Entries == null)
                            Entries = new List<ScheduleEntry>(20);
                        else
                            Entries.Clear();

                        if (current_char != (byte)DataTypes._A01_array)
                        {
                            throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode ,_A01_Array Type Expected",
                                                            OBISIndex, OBISIndex.OBISIndex), "Decode_Data_Schedule_Class_10");
                        }

                        int schEntries_Count = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);
                        byte[] current_OctSTR = null;
                        StDateTime current_DateTime = null;
                        BitArray currentBits = null;

                        for (int index = 0; index < schEntries_Count; index++)
                        {
                            Current_Entry = null;
                            current_char = Data[array_traverse++];

                            if (current_char == (byte)DataTypes._A00_Null)
                            {
                                Entries.Add(null);
                                continue;
                            }
                            else if (current_char != (byte)DataTypes._A02_structure)
                                throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode Invalid Structure", OBISIndex, OBISIndex.OBISIndex),
                                                                "Decode_Data_Schedule_Class_10");
                            else if(Data[array_traverse++] != 0x0A)
                                throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode Invalid Structure Elements", OBISIndex, OBISIndex.OBISIndex),
                                                                "Decode_Data_Schedule_Class_10");

                            // Decode Schedule Entry
                            Current_Entry = new ScheduleEntry();
                            Entries.Add(Current_Entry);

                            // index: long-unsigned
                            Current_Entry.Index = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                            // enable: boolean
                            Current_Entry.Enable = Convert.ToBoolean(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));

                            // script_logical_name: octet-string,
                            StOBISCode Script_OBIS;
                            current_char = Data[array_traverse];

                            if (current_char == (byte)DataTypes._A00_Null)
                            {
                                Current_Entry.LogicalName = Get_Index.Dummy;
                                // Skip Logical Name Decoding
                            }
                            else
                            {
                                if (current_char != (byte)DataTypes._A09_octet_string)
                                    throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode ,_A04_bit_string Type Expected", OBISIndex, OBISIndex.OBISIndex),
                                                                    "Decode_Data_Schedule_Class_10");


                                current_OctSTR = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse);

                                Script_OBIS = StOBISCode.ConvertFrom(current_OctSTR);
                                // Script Class Id 0x09
                                Script_OBIS.ClassId = 0x09;
                                // Script Logical Name
                                Current_Entry.LogicalName = Script_OBIS;
                            }
                            // script_selector: long-unsigned,
                            Current_Entry.ScriptSelector = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));

                            // switch_time: octet-string,
                            current_char = Data[array_traverse];

                            if (current_char == (byte)DataTypes._A00_Null)
                            {
                                Current_Entry.SwitchTime = null;
                            }
                            else
                            {
                                if (current_char != (byte)DataTypes._A09_octet_string)
                                    throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode ,_A09_octect_string Type Expected", OBISIndex, OBISIndex.OBISIndex),
                                                                    "Decode_Data_Schedule_Class_10");

                                current_OctSTR = null;
                                current_OctSTR = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse);
                                current_DateTime = new StDateTime();
                                current_DateTime.DecodeTime(current_OctSTR);

                                Current_Entry.SwitchTime = current_DateTime;
                            }
                            // validity_window: long-unsigned,
                            Current_Entry.ValidityWindow = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));

                            int bitLenght = 0;
                            // exec_weekdays: bit-string,
                            if (Current_Entry.ExecWeekdays == null)
                                Current_Entry.ExecWeekdays = new BitArray(ScheduleEntry.WeekDaysCount);
                            // reset Each Bit
                            for (int indexer = 0; indexer < ScheduleEntry.WeekDaysCount; indexer++)
                            {
                                Current_Entry.ExecWeekdays[indexer] = false;
                            }

                            current_char = Data[array_traverse];

                            if (current_char == (byte)DataTypes._A00_Null)
                            {
                                // Skip exec_weekdays decoding
                            }
                            else
                            {
                                if (current_char != (byte)DataTypes._A04_bit_string)
                                    throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode ,_A04_bit_string Type Expected", OBISIndex, OBISIndex.OBISIndex),
                                                                    "Decode_Data_Schedule_Class_10");

                                current_OctSTR = null;
                                current_OctSTR = BasicEncodeDecode.Decode_BitString(Data, ref array_traverse, ref bitLenght);
                                // Init Bits Received
                                currentBits = new BitArray(current_OctSTR);

                                // set Each Bit
                                for (int indexer = 0; indexer < ScheduleEntry.WeekDaysCount && indexer < bitLenght; indexer++)
                                {
                                    Current_Entry.ExecWeekdays[indexer] = currentBits[indexer];
                                }

                                // release memory
                                currentBits = null;
                            }

                            // exec_specdays: bit-string,
                            if (Current_Entry.ExecSpecialDays == null)
                                Current_Entry.ExecSpecialDays = new BitArray(ScheduleEntry.MaxSpecialDaysCount);
                            // reset Each Bit
                            for (int indexer = 0; indexer < ScheduleEntry.MaxSpecialDaysCount; indexer++)
                            {
                                Current_Entry.ExecSpecialDays[indexer] = false;
                            }

                            current_char = Data[array_traverse];

                            if (current_char == (byte)DataTypes._A00_Null)
                            {
                                // reset Each Bit
                                // skip
                            }
                            else
                            {
                                if (current_char != (byte)DataTypes._A04_bit_string)
                                    throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode ,_A04_bit_string Type Expected", OBISIndex, OBISIndex.OBISIndex),
                                                                    "Decode_Data_Schedule_Class_10");

                                current_OctSTR = null;
                                current_OctSTR = BasicEncodeDecode.Decode_BitString(Data, ref array_traverse, ref bitLenght);
                                // Init Bits Received
                                currentBits = new BitArray(current_OctSTR);

                                // Compare ExecSpecialDays BitLength
                                if (Current_Entry.ExecSpecialDays == null || Current_Entry.ExecSpecialDays.Length < bitLenght)
                                    Current_Entry.ExecSpecialDays = new BitArray(bitLenght);
                                //Current_Entry.SpecialDaysCount = bitLenght;

                                // set Each Bit
                                for (int indexer = 0; indexer < ScheduleEntry.MaxSpecialDaysCount &&
                                                      indexer < bitLenght; indexer++)
                                {
                                    Current_Entry.ExecSpecialDays[indexer] = currentBits[indexer];
                                }

                                // release memory
                                current_OctSTR = null;
                                currentBits = null;
                            }

                            // begin_date: octet-string,
                            current_char = Data[array_traverse];

                            if (current_char == (byte)DataTypes._A00_Null)
                            {
                                Current_Entry.BeginDate = null;
                            }
                            else
                            {
                                if (current_char != (byte)DataTypes._A09_octet_string)
                                    throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode ,_A09_octet_string Type Expected", OBISIndex, OBISIndex.OBISIndex),
                                                                    "Decode_Data_Schedule_Class_10");

                                current_OctSTR = null;
                                current_OctSTR = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse);
                                current_DateTime = new StDateTime();
                                current_DateTime.DecodeDate(current_OctSTR);

                                Current_Entry.BeginDate = current_DateTime;
                            }

                            // end_date: octet-string,
                            current_char = Data[array_traverse];

                            if (current_char == (byte)DataTypes._A00_Null)
                            {
                                Current_Entry.EndDate = null;
                            }
                            else
                            {
                                if (current_char != (byte)DataTypes._A09_octet_string)
                                    throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode ,_A09_octet_string Type Expected", OBISIndex, OBISIndex.OBISIndex),
                                                                    "Decode_Data_Schedule_Class_10");

                                current_OctSTR = null;
                                current_OctSTR = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse);
                                current_DateTime = new StDateTime();
                                current_DateTime.DecodeDate(current_OctSTR);

                                Current_Entry.EndDate = current_DateTime;
                            }
                        }

                        SetAttributeDecodingResult(2, DecodingResult.Ready);
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
                    throw new DLMSDecodingException("Error occurred while decoding data", "Decode_Data_Schedule_Class_10", ex);
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
                        throw new DLMSEncodingException(String.Format("Unable to encode Schedule List,No Access", EncodingType), "Encode_Data_Schedule_Class_10");
                    }
                    // Encode Here Data
                    else if (EncodingAttribute == 0x02)
                    {
                        if (Entries == null ||
                            Entries.Count <= 0)
                        {
                            throw new ArgumentNullException("Schedule Entries");
                        }

                        byte[] current_ByteSTR = null;
                        BasicEncodeDecode.Encode_Length(ref current_ByteSTR, Convert.ToUInt16(Entries.Count));

                        EncodedRaw.Add((byte)DataTypes._A01_array);
                        EncodedRaw.AddRange(current_ByteSTR);

                        // Encode Each Scheduler Entry
                        foreach (var schEntry in Entries)
                        {
                            current_ByteSTR = Encode_ScheduleEntry(schEntry);
                            EncodedRaw.AddRange(current_ByteSTR);
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
                    throw new DLMSException(String.Format("{0}_{1}", "Error occurred while encoding data", "Encode_Data_Schedule_Class_10"), ex);
            }
        }

        public byte[] Encode_ScheduleEntry(ScheduleEntry schEntry)
        {
            List<byte> EncodedRaw = new List<byte>(0x0A);
            byte[] current_ByteSTR = null;

            // Null schedule Entry
            if (schEntry == null)
            {
                EncodedRaw.Add((byte)DataTypes._A00_Null);
                return EncodedRaw.ToArray();
            }

            // Encode Structure
            EncodedRaw.Add((byte)DataTypes._A02_structure);
            EncodedRaw.Add(0x0A);

            // index: long-unsigned
            current_ByteSTR = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, schEntry.Index);
            EncodedRaw.AddRange(current_ByteSTR);

            // enable: boolean
            current_ByteSTR = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A03_boolean, schEntry.Index);
            EncodedRaw.AddRange(current_ByteSTR);

            // script_logical_name: octet-string,
            if (schEntry.LogicalName == Get_Index.Dummy)
            {
                EncodedRaw.Add((byte)DataTypes._A00_Null);
            }
            else
            {
                current_ByteSTR = BasicEncodeDecode.Encode_OctetString(schEntry.LogicalName.OBISCode, DataTypes._A09_octet_string);
                EncodedRaw.AddRange(current_ByteSTR);
            }

            // script_selector: long-unsigned,
            current_ByteSTR = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, schEntry.ScriptSelector);
            EncodedRaw.AddRange(current_ByteSTR);

            // switch_time: octet-string,
            if (schEntry.SwitchTime == null)
            {
                EncodedRaw.Add((byte)DataTypes._A00_Null);
            }
            else
            {
                current_ByteSTR = schEntry.SwitchTime.EncodeTime();
                current_ByteSTR = BasicEncodeDecode.Encode_OctetString(current_ByteSTR, DataTypes._A09_octet_string);
                EncodedRaw.AddRange(current_ByteSTR);
            }

            // validity_window: long-unsigned,
            current_ByteSTR = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, schEntry.ValidityWindow);
            EncodedRaw.AddRange(current_ByteSTR);

            int bitLenght = 0x07;
            // exec_weekdays: bit-string,
            if (schEntry.ExecWeekdays == null ||
                schEntry.ExecWeekdays.Count != ScheduleEntry.WeekDaysCount)
                EncodedRaw.Add((byte)DataTypes._A00_Null);
            else
            {
                current_ByteSTR = DLMS_Common.BitArrayToByteArray(schEntry.ExecWeekdays);
                current_ByteSTR = BasicEncodeDecode.Encode_BitString(current_ByteSTR, bitLenght);
                EncodedRaw.AddRange(current_ByteSTR);
            }

            //bitLenght = ScheduleEntry.MaxSpecialDaysCount;
            //if (schEntry.SpecialDaysCount >= 1 &&
            //    schEntry.SpecialDaysCount <= ScheduleEntry.MaxSpecialDaysCount)
            //    bitLenght = schEntry.SpecialDaysCount;

            // exec_specdays: bit-string,
            if (schEntry.ExecSpecialDays == null ||
                schEntry.ExecSpecialDays.Count < 0 ||
                schEntry.SpecialDaysCount < 0)
                EncodedRaw.Add((byte)DataTypes._A00_Null);
            else
            {
                bitLenght = schEntry.SpecialDaysCount;
                current_ByteSTR = DLMS_Common.BitArrayToByteArray(schEntry.ExecSpecialDays);
                current_ByteSTR = BasicEncodeDecode.Encode_BitString(current_ByteSTR, bitLenght);
                EncodedRaw.AddRange(current_ByteSTR);
            }


            // begin_date: octet-string,
            if (schEntry.BeginDate == null)
            {
                EncodedRaw.Add((byte)DataTypes._A00_Null);
            }
            else
            {
                current_ByteSTR = schEntry.BeginDate.EncodeDate();
                current_ByteSTR = BasicEncodeDecode.Encode_OctetString(current_ByteSTR, DataTypes._A09_octet_string);
                EncodedRaw.AddRange(current_ByteSTR);
            }

            // end_date: octet-string,
            if (schEntry.EndDate == null)
            {
                EncodedRaw.Add((byte)DataTypes._A00_Null);
            }
            else
            {
                current_ByteSTR = schEntry.EndDate.EncodeDate();
                current_ByteSTR = BasicEncodeDecode.Encode_OctetString(current_ByteSTR, DataTypes._A09_octet_string);
                EncodedRaw.AddRange(current_ByteSTR);
            }

            return EncodedRaw.ToArray();
        }

        public override byte[] Encode_Parameters()
        {
            EncodedRaw = new List<byte>(0x0A);
            byte[] current_ByteSTR = null;

            try
            {
                #region Execute_Script

                // Disable\Enable
                if (base.MethodInvokeId == Enable_Execute)
                {
                    if (this.RangeA.FirstIndex == 0 &&
                        this.RangeA.LastIndex == 0 &&
                        this.RangeB.FirstIndex == 0 &&
                        this.RangeB.LastIndex == 0)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    // Encode Parameters Data
                    else
                    {
                        EncodedRaw.Add((byte)DataTypes._A02_structure);
                        EncodedRaw.Add(0x04);

                        // index: long-unsigned
                        current_ByteSTR = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, RangeA.FirstIndex);
                        EncodedRaw.AddRange(current_ByteSTR);
                        current_ByteSTR = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, RangeA.LastIndex);
                        EncodedRaw.AddRange(current_ByteSTR);

                        // 2nd Range
                        current_ByteSTR = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, RangeB.FirstIndex);
                        EncodedRaw.AddRange(current_ByteSTR);
                        current_ByteSTR = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, RangeB.LastIndex);
                        EncodedRaw.AddRange(current_ByteSTR);
                    }
                }
                // Insert New Load Profile Schedule Entry
                else if (base.MethodInvokeId == Insert_Execute)
                {
                    // null Entry
                    if (this.Current_Entry == null ||
                        this.Current_Entry.Index <= 0 ||
                        this.Current_Entry.Index > 9999)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else   // Encode Data
                    {
                        EncodedRaw.AddRange(Encode_ScheduleEntry(Current_Entry));
                    }
                }
                else if (base.MethodInvokeId == Delete_Execute)
                {
                    if (this.RangeA.FirstIndex == 0 &&
                        this.RangeA.LastIndex == 0)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    // Encode Parameters Data
                    else
                    {
                        EncodedRaw.Add((byte)DataTypes._A02_structure);
                        EncodedRaw.Add(0x02);

                        // index: long-unsigned
                        current_ByteSTR = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, RangeA.FirstIndex);
                        EncodedRaw.AddRange(current_ByteSTR);
                        current_ByteSTR = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, RangeA.LastIndex);
                        EncodedRaw.AddRange(current_ByteSTR);
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
            Class_10 cloned = new Class_10(this);
            return cloned;
        }

        #endregion
    }
}
