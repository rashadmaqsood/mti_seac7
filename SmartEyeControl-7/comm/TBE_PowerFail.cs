using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS;
using System.Xml.Serialization;
using comm;
using DLMS.Comm;

namespace SmartEyeControl_7.comm
{
    [Serializable]
    [XmlInclude(typeof(TBE))]
    public class TBE_PowerFail : ICustomStructure, IParam, ICloneable
    {
        #region Data_Members

        [XmlElement("DisableEventAtPowerFailTBE1", typeof(byte))]
        public byte disableEventAtPowerFail_TBE1 = 0;
        [XmlElement("DisableEventAtPowerFailTBE2", typeof(byte))]
        public byte disableEventAtPowerFail_TBE2 = 0; 
        
        #endregion

        #region Encoders/Decoders

        public byte[] Encode_Data()
        {
            try
            {
                byte temp = 0;
                List<byte> encodeRaw = new List<byte>(50);
                encodeRaw.AddRange(new byte[] { (byte)DataTypes._A02_structure, 2 });

                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A03_boolean, this.disableEventAtPowerFail_TBE1));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A03_boolean, this.disableEventAtPowerFail_TBE2));

                return encodeRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSEncodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException("Error occurred while encoding ICustomStructure Param_EventsCaution", "Param_EventsCaution", ex);
            }
        }

        public void Decode_Data(byte[] Data)
        {
            int array_traverser = 0;
            Decode_Data(Data, ref array_traverser, Data.Length);
        }

        public void Decode_Data(byte[] Data, ref int array_traverse, int length)
        {
            try
            {
                byte currentByte = Data[array_traverse++];
                if (currentByte != (byte)DataTypes._A02_structure && Data[array_traverse] != 2)
                    throw new DLMSDecodingException("Invalid ICustomStructure TBE_PowerFail Structure received", "decode TBE_PowerFail");
                array_traverse++;

                this.disableEventAtPowerFail_TBE1 = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                this.disableEventAtPowerFail_TBE2 = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));


            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSDecodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException("Error occurred while decoding ICustomStructure TBE_PowerFail", "TBE_PowerFail", ex);
            }
        } 
        
        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }
}
