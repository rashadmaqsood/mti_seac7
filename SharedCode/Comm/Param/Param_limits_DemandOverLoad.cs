using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DLMS;
using DLMS.Comm;

namespace SharedCode.Comm.Param
{
    [Serializable]
    [XmlInclude(typeof(Param_Limit_Demand_OverLoad))]
    public class Param_Limit_Demand_OverLoad : ICustomStructure, IParam
    {
        #region Data_Members

        [XmlElement("threshold", Type = typeof(double))]
        public double Threshold;

        #endregion

        #region ICustomStructure Members

        public byte[] Encode_Data()
        {
            try
            {
                List<byte> encodeRaw = new List<byte>(1);
                double t_Val = Threshold;
                t_Val = Math.Floor(t_Val * 1000);   ///1000 Fixed Multiplier
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A06_double_long_unsigned, Convert.ToUInt32(t_Val)));
                return encodeRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSEncodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException("Error occurred while encoding ICustomStructure Param_IP_Profiles", "Encode_Data_Param_IP_Profiles", ex);
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
                //array_traverse++;
                ///Threshold <DataType DataTypes._A06_double_long_unsigned>
                this.Threshold = Convert.ToUInt32(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse,length));
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSDecodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException("Error occurred while decoding ICustomStructure Param_IP_Profiles", "Decode_Data_Param_IP_Profiles", ex);
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
