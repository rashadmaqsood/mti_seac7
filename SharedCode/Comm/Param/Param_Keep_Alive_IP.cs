using System;
using System.Collections.Generic;
using DLMS;
using DLMS.Comm;
using System.Xml.Serialization;

namespace SharedCode.Comm.Param
{
    [XmlInclude(typeof(Param_Keep_Alive_IP))]
    [Serializable]
    public class Param_Keep_Alive_IP:ICustomStructure, IParam
    {
        public bool Enabled;
        public byte IP_Profile_ID;
        public ushort Ping_time;
        public byte Param_KeepAlive_Flags;

        [XmlIgnore()]
        public Param_KeepAliveIPFlag Param_KeepAliveIP_Flag
        {
            get { return (Param_KeepAliveIPFlag)Param_KeepAlive_Flags; }
            set { Param_KeepAlive_Flags = (byte)value; }
        }

        public OBIS_data_from_GUI encode_IP_Profile_ID()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            obj_OBIS_data_from_GUI.OBIS_code = Get_Index.KEEPALIVE_PARAMS;
            obj_OBIS_data_from_GUI.attribute = 2;
            obj_OBIS_data_from_GUI.Type = DataTypes._A11_unsigned;
            obj_OBIS_data_from_GUI.Data = IP_Profile_ID;

            return obj_OBIS_data_from_GUI;
        }

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

        public byte[] Encode_Data()
        {
            try
            {
                List<byte> encodeRaw = new List<byte>(25);
                encodeRaw.AddRange(new byte[] { (byte)DataTypes._A02_structure, 3 });
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.Ping_time));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.IP_Profile_ID));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.Param_KeepAlive_Flags));
                //byte dummy = 0;
                //encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, dummy));
                return encodeRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex is DLMSEncodingException || ex is DLMSException)
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException("Error occurred while encoding ICustomStructure Param_Keep_Alive_IP", "Encode_Data_Param_Keep_Alive_IP", ex);
            }
        }

        public void Decode_Data(byte[] Data)
        {
            int array_traverse = 0;
            Decode_Data(Data, ref array_traverse, Data.Length);
        }

        public void Decode_Data(byte[] Data, ref int array_traverse, int length)
        {
            try
            {
                byte currentByte = Data[array_traverse++];
                if (currentByte != (byte)DataTypes._A02_structure && Data[array_traverse] != 3)
                    throw new DLMSDecodingException("Invalid ICustomStructure Param_Keep_Alive_IP Structure received", "Decode_Data_Param_Keep_Alive_IP");
                array_traverse++;
                ///IP_Profiles <DataType DataTypes._A11_unsigned>
                //Ping_Time <DataTypes._A06_long_unsigned>
                this.Ping_time = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                this.IP_Profile_ID = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                //byte dummy = Convert.ToByte(BasicEncodeDecode.Intelligent_Date_Decoder(ref Data, ref array_traverse));
                this.Param_KeepAlive_Flags = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException || ex is DLMSException)
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException("Error occurred while decoding ICustomStructure Param_Keep_Alive_IP", "Decode_Data_Param_Keep_Alive_IP", ex);
            }
        }

        #endregion

        #region Support Flag Operations

        public void SET_KeepAliveIP_Flag(Param_KeepAliveIPFlag keepAliveFlag, bool value)
        {
            SET_KeepAliveIP_Flag(this, keepAliveFlag, value);
        }

        public static void SET_KeepAliveIP_Flag(Param_Keep_Alive_IP param_Keep_Alive_IP, Param_KeepAliveIPFlag keepAliveFlag, bool value)
        {
            try
            {
                Param_KeepAliveIPFlag Flags = param_Keep_Alive_IP.Param_KeepAliveIP_Flag;
                if (value)
                {
                    Flags |= keepAliveFlag;
                }
                else
                {
                    ///Reset Particular Flag
                    Flags |= keepAliveFlag;
                    Flags ^= keepAliveFlag;
                }
                param_Keep_Alive_IP.Param_KeepAliveIP_Flag = Flags;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred while Assign {0}", keepAliveFlag), ex);
            }
        }

        public static void GET_KeepAliveIP_Flag(Param_Keep_Alive_IP param_Keep_Alive_IP, Param_KeepAliveIPFlag keepAliveFlag, out bool value)
        {
            try
            {
                var Flags = param_Keep_Alive_IP.Param_KeepAliveIP_Flag;
                if ((Flags & keepAliveFlag) == keepAliveFlag)
                    value = true;
                else
                    value = false;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred reading  {0}", keepAliveFlag), ex);
            }
        }

        public bool GET_KeepAliveIP_Flag(Param_KeepAliveIPFlag keepAliveFlag)
        {
            bool flag_Val = false;
            GET_KeepAliveIP_Flag(this, keepAliveFlag, out flag_Val);
            return flag_Val;
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }

    [Flags]
    public enum Param_KeepAliveIPFlag : byte
    {
        None = 0,
        ///Send Immediate HeartBeat On TCP Connection
        HeartBeatOnConnection = 1,
        ///Enable Wakeup In KeepAlive Mode
        EnableWakeupInKeepAliveMode = 2
    }
}
