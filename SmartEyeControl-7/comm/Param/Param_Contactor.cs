using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DLMS;
using DLMS.Comm;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace comm
{
    [Serializable]
    [XmlInclude(typeof(Param_Contactor))]
    public class Param_Contactor : ICustomStructure, IParam, ICloneable
    {
        #region Data Members

        [XmlElement("Contactor_ON_Pulse_Time", typeof(UInt16))]
        public UInt16 Contactor_ON_Pulse_Time;
        [XmlElement("Contactor_OFF_Pulse_Time", typeof(UInt16))]
        public UInt16 Contactor_OFF_Pulse_Time;
        [XmlElement("Minimum_Interval_Bw_Contactor_State_Change", typeof(UInt16))]
        public UInt16 Minimum_Interval_Bw_Contactor_State_Change;
        [XmlElement("Power_Up_Delay_To_State_Change", typeof(UInt16))]
        public UInt16 Power_Up_Delay_To_State_Change;
        [XmlElement("Interval_Between_Retries", typeof(UInt32))]
        public UInt32 Interval_Between_Retries;
        [XmlElement("Retry_Count", typeof(byte))]
        public byte RetryCount;
        [XmlElement("Control_Mode", typeof(byte))]
        public byte Control_Mode;

        #region Contactor Param Flags

        [XmlIgnore()]
        public bool Over_Current_By_Phase_T1_FLAG_0;
        [XmlIgnore()]
        public bool Over_Current_By_Phase_T2_FLAG_1;
        [XmlIgnore()]
        public bool Over_Current_By_Phase_T3_FLAG_2;
        [XmlIgnore()]
        public bool Over_Current_By_Phase_T4_FLAG_3;
        [XmlIgnore()]
        public bool Over_Load_By_Phase_T1_FLAG_4;
        [XmlIgnore()]
        public bool Over_Load_By_Phase_T2_FLAG_5;
        [XmlIgnore()]
        public bool Over_Load_By_Phase_T3_FLAG_6;
        [XmlIgnore()]
        public bool Over_Load_By_Phase_T4_FLAG_7;

        [XmlIgnore()]
        public bool Over_Load_T1_FLAG_0;
        [XmlIgnore()]
        public bool Over_Load_T2_FLAG_1;
        [XmlIgnore()]
        public bool Over_Load_T3_FLAG_2;
        [XmlIgnore()]
        public bool Over_Load_T4_FLAG_3;
        [XmlIgnore()]
        public bool Over_MDI_T1_FLAG_4;
        [XmlIgnore()]
        public bool Over_MDI_T2_FLAG_5;
        [XmlIgnore()]
        public bool Over_MDI_T3_FLAG_6;
        [XmlIgnore()]
        public bool Over_MDI_T4_FLAG_7;

        [XmlIgnore()]
        public bool Over_Volt_FLAG_0;
        [XmlIgnore()]
        public bool Under_Volt_FLAG_1;
        [XmlIgnore()]
        public bool b2_Reserved_Flag_2;
        [XmlIgnore()]
        public bool b2_Reserved_Flag_3;
        [XmlIgnore()]
        public bool b2_Reserved_Flag_4;
        [XmlIgnore()]
        public bool b2_Reserved_Flag_5;
        [XmlIgnore()]
        public bool b2_Reserved_Flag_6;
        [XmlIgnore()]
        public bool b2_Reserved_Flag_7;

        [XmlIgnore()]
        public bool off_by_optically;
        [XmlIgnore()]
        public bool on_by_optically;
        [XmlIgnore()]
        public bool reconnect_automatic_or_switch;
        [XmlIgnore()]
        public bool reconnect_by_tariff_change;
        [XmlIgnore()]
        public bool Reconnect_By_Switch_on_Retries_Expire;
        [XmlIgnore()]
        public bool Reconnect_Automatically_on_Retries_Expire;
        [XmlIgnore()]
        public bool b3_Reserved_Flag_5;
        [XmlIgnore()]
        public bool b3_Reserved_Flag_6;
        [XmlIgnore()]
        public bool b3_Reserved_Flag_7;

        [XmlIgnore()]
        public bool? contactor_read_Status = null;

        #endregion

        #endregion

        #region Properties

        [XmlElement("RAW_Flags_0", typeof(byte))]
        public byte RAW_Flags_0
        {
            get
            {
                byte Flags = 0x00;
                Flags |= (Over_Load_By_Phase_T4_FLAG_7) ? (byte)0x80 : (byte)0x00;
                Flags |= (Over_Load_By_Phase_T3_FLAG_6) ? (byte)0x40 : (byte)0x00;
                Flags |= (Over_Load_By_Phase_T2_FLAG_5) ? (byte)0x20 : (byte)0x00;
                Flags |= (Over_Load_By_Phase_T1_FLAG_4) ? (byte)0x10 : (byte)0x00;
                Flags |= (Over_Current_By_Phase_T4_FLAG_3) ? (byte)0x08 : (byte)0x00;
                Flags |= (Over_Current_By_Phase_T3_FLAG_2) ? (byte)0x04 : (byte)0x00;
                Flags |= (Over_Current_By_Phase_T2_FLAG_1) ? (byte)0x02 : (byte)0x00;
                Flags |= (Over_Current_By_Phase_T1_FLAG_0) ? (byte)0x01 : (byte)0x00;
                return Flags;
            }
            set
            {
                ///Decoding Flags Byte
                byte Flags = value;
                Over_Load_By_Phase_T4_FLAG_7 = ((Flags & 0x80) > 0) ? true : false;
                Over_Load_By_Phase_T3_FLAG_6 = ((Flags & 0x40) > 0) ? true : false;
                Over_Load_By_Phase_T2_FLAG_5 = ((Flags & 0x20) > 0) ? true : false;
                Over_Load_By_Phase_T1_FLAG_4 = ((Flags & 0x10) > 0) ? true : false;
                Over_Current_By_Phase_T4_FLAG_3 = ((Flags & 0x08) > 0) ? true : false;
                Over_Current_By_Phase_T3_FLAG_2 = ((Flags & 0x04) > 0) ? true : false;
                Over_Current_By_Phase_T2_FLAG_1 = ((Flags & 0x02) > 0) ? true : false;
                Over_Current_By_Phase_T1_FLAG_0 = ((Flags & 0x01) > 0) ? true : false;
            }
        }

        [XmlElement("RAW_Flags_1", typeof(byte))]
        public byte RAW_Flags_1
        {
            get
            {
                byte Flags = 0x00;
                Flags |= (Over_MDI_T4_FLAG_7) ? (byte)0x80 : (byte)0x00;
                Flags |= (Over_MDI_T3_FLAG_6) ? (byte)0x40 : (byte)0x00;
                Flags |= (Over_MDI_T2_FLAG_5) ? (byte)0x20 : (byte)0x00;
                Flags |= (Over_MDI_T1_FLAG_4) ? (byte)0x10 : (byte)0x00;
                Flags |= (Over_Load_T4_FLAG_3) ? (byte)0x08 : (byte)0x00;
                Flags |= (Over_Load_T3_FLAG_2) ? (byte)0x04 : (byte)0x00;
                Flags |= (Over_Load_T2_FLAG_1) ? (byte)0x02 : (byte)0x00;
                Flags |= (Over_Load_T1_FLAG_0) ? (byte)0x01 : (byte)0x00;
                return Flags;
            }
            set
            {
                ///Decoding Flags Byte
                byte Flags = value;
                Over_MDI_T4_FLAG_7 = ((Flags & 0x80) > 0) ? true : false;
                Over_MDI_T3_FLAG_6 = ((Flags & 0x40) > 0) ? true : false;
                Over_MDI_T2_FLAG_5 = ((Flags & 0x20) > 0) ? true : false;
                Over_MDI_T1_FLAG_4 = ((Flags & 0x10) > 0) ? true : false;
                Over_Load_T4_FLAG_3 = ((Flags & 0x08) > 0) ? true : false;
                Over_Load_T3_FLAG_2 = ((Flags & 0x04) > 0) ? true : false;
                Over_Load_T2_FLAG_1 = ((Flags & 0x02) > 0) ? true : false;
                Over_Load_T1_FLAG_0 = ((Flags & 0x01) > 0) ? true : false;
            }
        }

        [XmlElement("RAW_Flags_2", typeof(byte))]
        public byte RAW_Flags_2
        {
            get
            {
                byte Flags = 0x00;
                Flags |= (b2_Reserved_Flag_7) ? (byte)0x80 : (byte)0x00;
                Flags |= (b2_Reserved_Flag_6) ? (byte)0x40 : (byte)0x00;
                Flags |= (b2_Reserved_Flag_5) ? (byte)0x20 : (byte)0x00;
                Flags |= (b2_Reserved_Flag_4) ? (byte)0x10 : (byte)0x00;
                Flags |= (b2_Reserved_Flag_3) ? (byte)0x08 : (byte)0x00;
                Flags |= (b2_Reserved_Flag_2) ? (byte)0x04 : (byte)0x00;
                Flags |= (Under_Volt_FLAG_1) ? (byte)0x02 : (byte)0x00;
                Flags |= (Over_Volt_FLAG_0) ? (byte)0x01 : (byte)0x00;
                return Flags;
            }
            set
            {
                ///Decoding Flags Byte
                byte Flags = value;
                b2_Reserved_Flag_7 = ((Flags & 0x80) > 0) ? true : false;
                b2_Reserved_Flag_6 = ((Flags & 0x40) > 0) ? true : false;
                b2_Reserved_Flag_5 = ((Flags & 0x20) > 0) ? true : false;
                b2_Reserved_Flag_4 = ((Flags & 0x10) > 0) ? true : false;
                b2_Reserved_Flag_3 = ((Flags & 0x08) > 0) ? true : false;
                b2_Reserved_Flag_2 = ((Flags & 0x04) > 0) ? true : false;
                Under_Volt_FLAG_1 = ((Flags & 0x02) > 0) ? true : false;
                Over_Volt_FLAG_0 = ((Flags & 0x01) > 0) ? true : false;
            }
        }

        [XmlElement("RAW_Flags_3", typeof(byte))]
        public byte RAW_Flags_3
        {
            get
            {
                byte Flags = 0x00;
                Flags |= (b3_Reserved_Flag_7) ? (byte)0x80 : (byte)0x00;
                Flags |= (b3_Reserved_Flag_6) ? (byte)0x40 : (byte)0x00;
                Flags |= (Reconnect_Automatically_on_Retries_Expire) ? (byte)0x20 : (byte)0x00;
                Flags |= (Reconnect_By_Switch_on_Retries_Expire) ? (byte)0x10 : (byte)0x00;
                Flags |= (reconnect_by_tariff_change) ? (byte)0x08 : (byte)0x00;
                Flags |= (reconnect_automatic_or_switch) ? (byte)0x04 : (byte)0x00;
                Flags |= (on_by_optically) ? (byte)0x02 : (byte)0x00;
                Flags |= (off_by_optically) ? (byte)0x01 : (byte)0x00;
                return Flags;
            }
            set
            {
                ///Decoding Flags Byte
                byte Flags = value;
                b3_Reserved_Flag_7 = ((Flags & 0x80) > 0) ? true : false;
                b3_Reserved_Flag_6 = ((Flags & 0x40) > 0) ? true : false;
                Reconnect_Automatically_on_Retries_Expire = ((Flags & 0x20) > 0) ? true : false;
                Reconnect_By_Switch_on_Retries_Expire = ((Flags & 0x10) > 0) ? true : false;
                reconnect_by_tariff_change = ((Flags & 0x08) > 0) ? true : false;
                reconnect_automatic_or_switch = ((Flags & 0x04) > 0) ? true : false;
                on_by_optically = ((Flags & 0x02) > 0) ? true : false;
                off_by_optically = ((Flags & 0x01) > 0) ? true : false;
            }
        }

        #endregion

        public Param_Contactor() { }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="Param_Contactor"></param>
        public Param_Contactor(Param_Contactor Param_Contactor)
        {
            Contactor_ON_Pulse_Time = Param_Contactor.Contactor_ON_Pulse_Time;
            Contactor_OFF_Pulse_Time = Param_Contactor.Contactor_OFF_Pulse_Time;
            Minimum_Interval_Bw_Contactor_State_Change = Param_Contactor.Minimum_Interval_Bw_Contactor_State_Change;
            Power_Up_Delay_To_State_Change = Param_Contactor.Power_Up_Delay_To_State_Change;
            Interval_Between_Retries = Param_Contactor.Interval_Between_Retries;
            RetryCount = Param_Contactor.RetryCount;
            Control_Mode = Param_Contactor.Control_Mode;
            ///RAW_Flags
            RAW_Flags_0 = Param_Contactor.RAW_Flags_0;
            RAW_Flags_1 = Param_Contactor.RAW_Flags_1;
            RAW_Flags_2 = Param_Contactor.RAW_Flags_2;
            RAW_Flags_3 = Param_Contactor.RAW_Flags_3;
        }

        #region Commented_CodeSection
        //public Base_Class Encode_FLAGS(GetSAPEntry CommObjectGetter)
        //{
        //    Class_1 FLAGS_obj = (Class_1)CommObjectGetter.Invoke(Get_Index.ContactorParametres_Flag);
        //    FLAGS_obj.EncodingAttribute = 2;
        //    FLAGS_obj.EncodingType = DataTypes._A09_octet_string;
        //
        //
        //    byte[] bytearray = new byte[3];
        //    bytearray[2] = (byte)(Convert.ToInt32(Over_Load_By_Phase_T4_FLAG_7) * 128 + Convert.ToInt32(Over_Load_By_Phase_T3_FLAG_6) * 64 +
        //                    Convert.ToInt32(Over_Load_By_Phase_T2_FLAG_5) * 32 + Convert.ToInt32(Over_Load_By_Phase_T1_FLAG_4) * 16 +
        //                    Convert.ToInt32(Over_Current_By_Phase_T4_FLAG_3) * 8 + Convert.ToInt32(Over_Current_By_Phase_T3_FLAG_2) * 4 +
        //                    Convert.ToInt32(Over_Current_By_Phase_T2_FLAG_1) * 2 + Convert.ToInt32(Over_Current_By_Phase_T1_FLAG_0));
        //
        //    bytearray[1] = (byte)(Convert.ToInt32(Local_Control_FLAG_3) * 128 + Convert.ToInt32(Remotely_Control_FLAG_2) * 64 +
        //                                    Convert.ToInt32(Over_MDI_T2_FLAG_5) * 32 + Convert.ToInt32(Over_MDI_T1_FLAG_4) * 16 +
        //                                    Convert.ToInt32(Over_Load_T4_FLAG_3) * 8 + Convert.ToInt32(Over_Load_T3_FLAG_2) * 4 +
        //                                    Convert.ToInt32(Over_Load_T2_FLAG_1) * 2 + Convert.ToInt32(Over_Load_T1_FLAG_0));
        //
        //    bytearray[0] = (byte)(Convert.ToInt32(Over_Volt_FLAG_0) * 8 + Convert.ToInt32(Under_Volt_FLAG_1) * 4 +
        //                                                    Convert.ToInt32(Under_Volt_FLAG_1) * 2 + Convert.ToInt32(Over_Volt_FLAG_0));
        //
        //
        //    FLAGS_obj.Value_Array = bytearray;
        //    return FLAGS_obj;
        //}
        //
        //public void Decode_FLAGS(Base_Class arg)
        //{
        //    try
        //    {
        //        Class_1 Decode_FLAGS_obj = (Class_1)arg;
        //        ///Verify data Receiced/OBIS/ETC
        //        UInt32 flags = Convert.ToUInt32(Decode_FLAGS_obj.Value);
        //        Over_Current_By_Phase_T1_FLAG_0 = Convert.ToBoolean(flags % 2 ^ 0);
        //        Over_Current_By_Phase_T1_FLAG_0 = Convert.ToBoolean( flags % 2 ^ 1);
        //        Over_Current_By_Phase_T2_FLAG_1 = Convert.ToBoolean( flags % 2 ^ 2);
        //        Over_Current_By_Phase_T3_FLAG_2 = Convert.ToBoolean( flags % 2 ^ 3);
        //        Over_Current_By_Phase_T4_FLAG_3 = Convert.ToBoolean( flags % 2 ^ 4);
        //        Over_Load_By_Phase_T1_FLAG_4 = Convert.ToBoolean( flags % 2 ^ 5);
        //        Over_Load_By_Phase_T2_FLAG_5 = Convert.ToBoolean( flags % 2 ^ 6);
        //        Over_Load_By_Phase_T3_FLAG_6 = Convert.ToBoolean( flags % 2 ^ 7);
        //        Over_Load_By_Phase_T4_FLAG_7 = Convert.ToBoolean( flags % 2 ^ 8);
        //        Over_Load_T1_FLAG_0 = Convert.ToBoolean( flags % 2 ^ 9);
        //        Over_Load_T2_FLAG_1 = Convert.ToBoolean( flags % 2 ^ 10);
        //        Over_Load_T3_FLAG_2 = Convert.ToBoolean( flags % 2 ^ 11);
        //        Over_Load_T4_FLAG_3 = Convert.ToBoolean( flags % 2 ^ 12);
        //        Over_MDI_T1_FLAG_4 = Convert.ToBoolean( flags % 2 ^ 13);
        //        Over_MDI_T2_FLAG_5 = Convert.ToBoolean( flags % 2 ^ 14);
        //        Over_MDI_T3_FLAG_6 = Convert.ToBoolean( flags % 2 ^ 15);
        //        Over_MDI_T4_FLAG_7 = Convert.ToBoolean( flags % 2 ^ 16);
        //        Over_Volt_FLAG_0 = Convert.ToBoolean( flags % 2 ^ 17);
        //        Under_Volt_FLAG_1 = Convert.ToBoolean( flags % 2 ^ 18);
        //        Remotely_Control_FLAG_2 = Convert.ToBoolean( flags % 2 ^ 19);
        //        Local_Control_FLAG_3 = Convert.ToBoolean( flags % 2 ^ 20);
        //
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //} 
        #endregion

        public double Decode_Any(Base_Class arg, byte Class_ID)
        {
            try
            {
                if (Class_ID == 1)
                {
                    Class_1 temp_obj = (Class_1)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);
                    return temp;
                }
                if (Class_ID == 3)
                {
                    Class_3 temp_obj = (Class_3)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);
                    return temp;
                }
                if (Class_ID == 4)
                {
                    Class_4 temp_obj = (Class_4)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);
                    return temp;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region ICustomStructure Members

        public virtual byte[] Encode_Data()
        {
            try
            {
                List<byte> encodeRaw = new List<byte>(50);
                encodeRaw.AddRange(new byte[] { (byte)DataTypes._A02_structure, 11 });
                ///Contactor_ON_Pulse_Time <DataTypes._A12_long_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.Contactor_ON_Pulse_Time));
                ///Contactor_OFF_Pulse_Time <DataTypes._A12_long_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.Contactor_OFF_Pulse_Time));
                ///Minimum_Interval_Bw_Contactor_State_Change <DataTypes._A12_long_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.Minimum_Interval_Bw_Contactor_State_Change));
                ///Power_Up_Delay_To_State_Change <DataTypes._A12_long_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.Power_Up_Delay_To_State_Change));
                ///Interval_Between_Retries <DataTypes._A12_long_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A06_double_long_unsigned, this.Interval_Between_Retries));

                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.RetryCount));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.Control_Mode));
                ///RAW_Flags_0 <DataTypes._A11_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.RAW_Flags_0));
                ///RAW_Flags_1 <DataTypes._A11_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.RAW_Flags_1));
                ///RAW_Flags_2 <DataTypes._A11_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.RAW_Flags_2));
                ///RAW_Flags_3 <DataTypes._A11_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, RAW_Flags_3));

                return encodeRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSEncodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException("Error occurred while encoding ICustomStructure Param_Contactor", "Encode_Data_Param_Contactor", ex);
            }
        }

        public virtual void Decode_Data(byte[] Data)
        {
            int array_traverser = 0;
            Decode_Data(Data, ref array_traverser, Data.Length);
        }

        public virtual void Decode_Data(byte[] Data, ref int array_traverse, int length)
        {
            try
            {
                int temp = 0;
                byte currentByte = Data[array_traverse++];
                if (currentByte != (byte)DataTypes._A02_structure && Data[array_traverse] != 11)
                    throw new DLMSDecodingException("Invalid ICustomStructure Param_Contactor Structure received", "Decode_Data_Param_Contactor");

                array_traverse++;
                ///Contactor_ON_Pulse_Time <DataTypes._A12_long_unsigned>
                this.Contactor_ON_Pulse_Time = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));

                ///Contactor_OFF_Pulse_Time <DataTypes._A12_long_unsigned>
                this.Contactor_OFF_Pulse_Time = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                ///Minimum_Interval_Bw_Contactor_State_Change <DataTypes._A12_long_unsigned>
                this.Minimum_Interval_Bw_Contactor_State_Change =
                    Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));

                ///Power_Up_Delay_To_State_Change <DataTypes._A12_long_unsigned>
                this.Power_Up_Delay_To_State_Change = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                ///Interval_Between_Retries <DataTypes._A12_long_unsigned>
                this.Interval_Between_Retries = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));

                ///RetryCount <DataTypes._A11_unsigned>
                this.RetryCount = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));

                ///this.Control_Mode = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                this.Control_Mode = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));

                ///RAW_Flags_0 <DataTypes._A11_unsigned>
                this.RAW_Flags_0 = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                ///RAW_Flags_1 <DataTypes._A11_unsigned>
                this.RAW_Flags_1 = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                ///RAW_Flags_2 <DataTypes._A11_unsigned>
                this.RAW_Flags_2 = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                ///RAW_Flags_3 <DataTypes._A11_unsigned>
                this.RAW_Flags_3 = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));

            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSDecodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException("Error occurred while decoding ICustomStructure Param_Contactor", "Decode_Data_Param_Contactor", ex);
            }
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            MemoryStream memStream = null;
            Object dp = null;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (memStream = new MemoryStream(256))
                {
                    formatter.Serialize(memStream, this);
                    memStream.Seek(0, SeekOrigin.Begin);
                    dp = formatter.Deserialize(memStream);
                }
                return dp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Clone Object", ex);
            }
        }

        #endregion

    }

    [Serializable]
    [XmlInclude(typeof(Param_ContactorExt))]
    public class Param_ContactorExt : Param_Contactor
    {
        #region Data_Members

        [XmlElement("Interval_Contactor_Failure_Status", typeof(UInt16))]
        public ushort Interval_Contactor_Failure_Status;

        #endregion

        #region Constructor

        public Param_ContactorExt()
        {
        }

        public Param_ContactorExt(Param_Contactor Param_ContactorBase)
            : base(Param_ContactorBase)
        {
            if (Param_ContactorBase is Param_ContactorExt)
                Interval_Contactor_Failure_Status = ((Param_ContactorExt)Param_ContactorBase).
                    Interval_Contactor_Failure_Status;
            else
                Interval_Contactor_Failure_Status = 0;
        }

        #endregion

        #region ICustomStructure_Members

        public override byte[] Encode_Data()
        {
            try
            {
                List<byte> encodeRaw = new List<byte>(50);
                encodeRaw.AddRange(new byte[] { (byte)DataTypes._A02_structure, 12 });

                ///Contactor_ON_Pulse_Time <DataTypes._A12_long_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.Contactor_ON_Pulse_Time));
                ///Contactor_OFF_Pulse_Time <DataTypes._A12_long_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.Contactor_OFF_Pulse_Time));
                ///Minimum_Interval_Bw_Contactor_State_Change <DataTypes._A12_long_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.Minimum_Interval_Bw_Contactor_State_Change));
                ///Power_Up_Delay_To_State_Change <DataTypes._A12_long_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.Power_Up_Delay_To_State_Change));
                ///Interval_Between_Retries <DataTypes._A06_double_long_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A06_double_long_unsigned, this.Interval_Between_Retries));
                ///Interval_Contactor_Failure_Status <DataTypes._A12_long_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.Interval_Contactor_Failure_Status));

                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.RetryCount));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.Control_Mode));

                ///RAW_Flags_0 <DataTypes._A11_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.RAW_Flags_0));
                ///RAW_Flags_1 <DataTypes._A11_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.RAW_Flags_1));
                ///RAW_Flags_2 <DataTypes._A11_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.RAW_Flags_2));
                ///RAW_Flags_3 <DataTypes._A11_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, RAW_Flags_3));

                return encodeRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSEncodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException("Error occurred while encoding ICustomStructure Param_ContactorExt", "Encode_Data_Param_Contactor", ex);
            }
        }

        public override void Decode_Data(byte[] Data)
        {
            this.Decode_Data(Data);
        }

        public override void Decode_Data(byte[] Data, ref int array_traverse, int length)
        {
            try
            {
                byte currentByte = Data[array_traverse++];
                if (currentByte != (byte)DataTypes._A02_structure && Data[array_traverse] != 12)
                    throw new DLMSDecodingException("Invalid ICustomStructure Param_Contactor Structure received", "Decode_Data_Param_Contactor");
                array_traverse++;
                ///Contactor_ON_Pulse_Time <DataTypes._A12_long_unsigned>
                this.Contactor_ON_Pulse_Time = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                ///Contactor_OFF_Pulse_Time <DataTypes._A12_long_unsigned>
                this.Contactor_OFF_Pulse_Time = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                ///Minimum_Interval_Bw_Contactor_State_Change <DataTypes._A12_long_unsigned>
                this.Minimum_Interval_Bw_Contactor_State_Change =
                    Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                ///Power_Up_Delay_To_State_Change <DataTypes._A12_long_unsigned>
                this.Power_Up_Delay_To_State_Change = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                ///Interval_Between_Retries <DataTypes._A06_double_long_unsigned>
                this.Interval_Between_Retries = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                ///Interval_Contactor_Failure_Status <DataTypes._A12_long_unsigned>
                this.Interval_Contactor_Failure_Status = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));

                ///this.RetryCount
                this.RetryCount = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                ///this.Control_Mode = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                this.Control_Mode = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));

                ///RAW_Flags_0 <DataTypes._A11_unsigned>
                this.RAW_Flags_0 = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                ///RAW_Flags_1 <DataTypes._A11_unsigned>
                this.RAW_Flags_1 = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                ///RAW_Flags_2 <DataTypes._A11_unsigned>
                this.RAW_Flags_2 = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                ///RAW_Flags_2 <DataTypes._A11_unsigned>
                this.RAW_Flags_3 = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSDecodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException("Error occurred while decoding ICustomStructure Param_ContactorExt",
                        "Decode_Data_Param_Contactor", ex);
            }
        }

        #endregion

        #region ICloneable Members

        public new object Clone()
        {
            MemoryStream memStream = null;
            Object dp = null;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (memStream = new MemoryStream(256))
                {
                    formatter.Serialize(memStream, this);
                    memStream.Seek(0, SeekOrigin.Begin);
                    dp = formatter.Deserialize(memStream);
                }
                return dp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Clone Object", ex);
            }
        }

        #endregion
    }
}
