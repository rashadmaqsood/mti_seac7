using System;
using System.Collections.Generic;
using DLMS.Comm;
using DLMS;
using SharedCode.Comm.Param;
using System.Xml.Serialization;

namespace SharedCode.Comm.Param
{
    [Serializable]
    [XmlInclude(typeof(TBE_PowerFail))]
    public class TBE_PowerFail : ICustomStructure, ICloneable,IParam
    {
        #region Data_Members


        public byte disableEventAtPowerFail_TBE1 = 0;

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
