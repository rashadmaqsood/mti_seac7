using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DLMS;
using DLMS.Comm;

namespace comm
{
    [Serializable]
    [XmlInclude(typeof(Param_Communication_Profile))]
    public class Param_Communication_Profile:ICustomStructure,IParam
    {
        #region Data_Members
        
        [XmlElement("selectedMode", typeof(byte))]
        public byte SelectedMode;
        [XmlElement("WakeUpProfileID", typeof(byte))]
        public byte WakeUpProfileID;
        [XmlElement("NumberProfileID", typeof(byte[]))]
        public byte[] NumberProfileID;
        [XmlIgnore()]
        public bool Protocol_HDLC_TCP_Flag_0;
        [XmlIgnore()]
        public bool Protocol_TCP_UDP_Flag_1;
         
        [XmlElement("RAW_FLAG_COMMUNICATION_PROFILE", typeof(byte))]
        public byte RAW_FLAG_COMMUNICATION_PROFILE
        {
            get
            {
                byte Flags = 0x00;
                Flags += (Protocol_HDLC_TCP_Flag_0) ? (byte)0x01 : (byte)0x00;
                Flags += (Protocol_TCP_UDP_Flag_1) ? (byte)0x02 : (byte)0x00;
                return Flags;
            }
            set
            {
                byte Flags = value;
                Protocol_TCP_UDP_Flag_1 = ((Flags & 0x02) > 0) ? true : false;
                Protocol_HDLC_TCP_Flag_0 = ((Flags & 0x01) > 0) ? true : false;
            }
        }
 
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
        
        #region ICustomStructure_Members

        public byte[] Encode_Data()
        {
            try
            {
                List<byte> encodeRaw = new List<byte>(50);
                encodeRaw.AddRange(new byte[] { (byte)DataTypes._A02_structure, 5});
                ///WakeUpProfileID <DataType DataTypes._A11_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.WakeUpProfileID));
                ///Profile_ID <DataType DataTypes._A11_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.RAW_FLAG_COMMUNICATION_PROFILE));
                
                encodeRaw.AddRange(BasicEncodeDecode.Encode_OctetString(this.NumberProfileID,DataTypes._A09_octet_string));
                ///RAW_FLAG_COMMUNICATION_PROFILE <DataType DataTypes._A11_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, 0));     ///Dummy
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, 0));     ///Dummy

                return encodeRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSEncodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException("Error occurred while encoding ICustomStructure Param_Communication_Profile",
                        "Encode_Data_Param_Communication_Profile", ex);
            } 
        }

        public void Decode_Data(byte[] Data)
        {
            int array_traverse = 0;
            Decode_Data(Data, ref array_traverse,Data.Length); 
        }

        public void Decode_Data(byte[] Data, ref int array_traverse,int length)
        {
            try
            {
                byte currentByte = Data[array_traverse++];
                if (currentByte != (byte)DataTypes._A02_structure && Data[array_traverse] !=5)
                    throw new DLMSDecodingException("Invalid ICustomStructure Param_Communication_Profile Structure received", "Decode_Data_Param_Communication_Profile");
                array_traverse++;
                ///WakeUpProfileID <DataType DataTypes._A11_unsigned>
                this.WakeUpProfileID = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                RAW_FLAG_COMMUNICATION_PROFILE = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                ///Profile_ID <DataType DataTypes._A11_unsigned>
                this.NumberProfileID = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse);
                ///RAW_FLAG_COMMUNICATION_PROFILE <DataTypes.unsigned>
                byte dummy_1 = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                byte dummy_2 = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSDecodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException("Error occurred while decoding ICustomStructure Param_Communication_Profile", "Param_Communication_Profile", ex);
            }      
        }

        #endregion

        #region ICloneable_Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
