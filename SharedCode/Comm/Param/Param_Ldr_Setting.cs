using DLMS;
using DLMS.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SharedCode.Comm.Param
{
    [Serializable]
    [XmlInclude(typeof(Param_Ldr_Setting))]
    public class Param_Ldr_Setting : IParam, ICloneable
    {
        public bool IsEnabled;
        public sbyte Offset;
        public byte Divider;
        public byte Max;
        public byte Min;
        public byte RFU;

        #region Constructor
        public Param_Ldr_Setting()
        {
            IsEnabled = true;
            Offset = 0;
            Divider = 0;
            Max = 0;
            Min = 0;
            RFU = 0;
        }
        #endregion


        #region ICustomStructure Members

        public Base_Class Encode_Data(GetSAPEntry CommObjectGetter)
        {
            try
            {
                Class_1 param = (Class_1)CommObjectGetter.Invoke(Get_Index.Param_Ldr_Setting);
                param.EncodingAttribute = 2;
                param.EncodingType = DataTypes._A09_octet_string;

                byte[] bytearray = new byte[6];
                bytearray[0] = IsEnabled ? (byte)1 : (byte)0;
                bytearray[1] = (byte)Offset;
                bytearray[2] = Divider;
                bytearray[3] = Max;
                bytearray[4] = Min;
                bytearray[5] = RFU;
                param.Value_Array = bytearray;
                return param;
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSEncodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException("Error occurred while encoding Param_LDR", "Encode_Data_Param_Ldr_Setting", ex);

            }

        }

        //public byte[] Encode_Data()
        //{
        //    try
        //    {
        //        List<byte> encodeRaw = new List<byte>(8);
        //        encodeRaw.AddRange(new byte[] { (byte)DataTypes._A09_octet_string, (byte)6 });
        //        encodeRaw.Add(IsEnabled ? (byte)1 : (byte)0);
        //        encodeRaw.Add(Offset);
        //        encodeRaw.Add(Divider);
        //        encodeRaw.Add(Max);
        //        encodeRaw.Add(Min);
        //        encodeRaw.Add(RFU);
        //        return encodeRaw.ToArray();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.GetType() == typeof(DLMSEncodingException) || ex.GetType() == typeof(DLMSException))
        //        {
        //            throw ex;
        //        }
        //        else
        //            throw new DLMSEncodingException("Error occurred while encoding Param_LDR", "Encode_Data_Param_Ldr_Setting", ex);
        //    }
        //}

        //public void Decode_Data(byte[] Data)
        //{
        //    int array_traverse = 0;
        //    Decode_Data(Data, ref array_traverse, Data.Length);
        //}

        public void Decode_Data(Base_Class arg)
        {
            try
            {
                Class_1 param = (Class_1)arg;
                ///Verify data Receiced/OBIS/ETC
                if (param.GetAttributeDecodingResult(2) == DecodingResult.Ready)
                {
                    //if (currentByte != (byte)DataTypes._A09_octet_string && Data[array_traverse++] != 6)
                    //    throw new DLMSDecodingException("Invalid Param_LDR Structure received", "Decode_Data_Param_Ldr_Setting");

                    int array_traverse = 0;
                    byte[] Data = new byte[6];
                    Data = (byte[])param.Value_Array;
                    IsEnabled = Data[array_traverse++] == 1 ? true : false;
                    Offset = (sbyte)Data[array_traverse++];
                    Divider = Data[array_traverse++];
                    Max = Data[array_traverse++];
                    Min = Data[array_traverse++];
                    RFU = Data[array_traverse++];
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSDecodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException("Error occurred while decoding Param_Ldr_Setting", "Decode_Data_Param_Ldr_Setting", ex);
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
