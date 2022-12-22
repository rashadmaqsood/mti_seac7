using System;
using System.Collections.Generic;
using DLMS;
using DLMS.Comm;

namespace SharedCode.Comm.Param
{
    public class Param_PresetTime:ICustomStructure
    {
        public StDateTime PresetTime;
        public StDateTime ValidityStartInterval;
        public StDateTime ValidityEndInterval;

        #region Constructor
        public Param_PresetTime()
        {
            PresetTime = new StDateTime();
            ValidityStartInterval = new StDateTime();
            ValidityEndInterval = new StDateTime();
        }
        #endregion


        #region ICustomStructure Members

        public byte[] Encode_Data()
        {
            try
            {
                List<byte> encodeRaw = new List<byte>(25);
                encodeRaw.AddRange(new byte[] { (byte)DataTypes._A02_structure, 3 });
                encodeRaw.AddRange(PresetTime.EncodeRawBytes(StDateTime.DateTimeType.DateTime));
                encodeRaw.AddRange(ValidityStartInterval.EncodeRawBytes(StDateTime.DateTimeType.DateTime));
                encodeRaw.AddRange(ValidityEndInterval.EncodeRawBytes(StDateTime.DateTimeType.DateTime));
                return encodeRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSEncodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException("Error occurred while encoding ICustomStructure Param_PresetTime", "Encode_Data_Param_PresetTime", ex);
            } 
        }

        public void Decode_Data(byte[] Data)
        {
            int array_traverse = 0;
            Decode_Data(Data,ref array_traverse,Data.Length);
        }

        public void Decode_Data(byte[] Data, ref int array_traverse,int length)
        {
            try
            {
                byte currentByte = Data[array_traverse++];
                if (currentByte != (byte)DataTypes._A02_structure && Data[array_traverse] != 3)
                    throw new DLMSDecodingException("Invalid ICustomStructure Param_PresetTime Structure received", "Decode_Data_Param_PresetTime");
                array_traverse++;
                PresetTime.DecodeRawBytes(Data, ref array_traverse);
                ValidityStartInterval.DecodeRawBytes(Data, ref array_traverse);
                ValidityEndInterval.DecodeRawBytes(Data, ref array_traverse);
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSDecodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException("Error occurred while decoding ICustomStructure Param_PresetTime", "Decode_Data_Param_PresetTime", ex);
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
