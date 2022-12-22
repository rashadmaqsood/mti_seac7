using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS;
using TCP_Communication;
using ucCustomControl;
using comm;
using System.Xml.Serialization;
using DLMS.Comm;

namespace comm
{
    [Serializable]
    [XmlInclude(typeof(Param_Keep_Alive_IP))]
    public class Param_Keep_Alive_IP : ICustomStructure, IParam
    {
        #region DataMembers

        [XmlElement("Enabled", Type = typeof(bool))]
        public bool Enabled;
        [XmlElement("IPProfileID", Type = typeof(byte))]
        public byte IP_Profile_ID;
        [XmlElement("PingTime", Type = typeof(ushort))]
        public ushort Ping_time;

        #endregion

        #region Decoder/Encoder Functions

        public OBIS_data_from_GUI encode_IP_Profile_ID()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            obj_OBIS_data_from_GUI.OBIS_code = Get_Index.KeepAliveIP_1_IP_Profile_ID;
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
        #endregion

        #region ICustomStructure Members

        public byte[] Encode_Data()
        {
            try
            {
                List<byte> encodeRaw = new List<byte>(25);
                encodeRaw.AddRange(new byte[] { (byte)DataTypes._A02_structure, 3 });
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.Ping_time));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.IP_Profile_ID));
                byte dummy = 0;
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, dummy));
                return encodeRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSEncodingException) || ex.GetType() == typeof(DLMSException))
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
                byte dummy = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSDecodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException("Error occurred while decoding ICustomStructure Param_Keep_Alive_IP", "Decode_Data_Param_Keep_Alive_IP", ex);
            }
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
