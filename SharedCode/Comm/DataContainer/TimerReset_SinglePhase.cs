using DLMS;
using DLMS.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedCode.Comm.DataContainer
{
    public class TimerReset_SinglePhase : ICustomStructure
    {

        #region Properties
        public bool Enable { get; set; }
        public byte Minute { get; set; }
        public byte Hour { get; set; }
        #endregion

        #region Encode Decode
        public byte[] Encode_Data()
        {
            List<byte> RawData = new List<byte>();
            RawData.Add((byte)DataTypes._A02_structure);
            RawData.Add((byte)3);
            RawData.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A0F_integer, Hour));
            RawData.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A0F_integer, Minute));
            RawData.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A03_boolean, Enable));
            return RawData.ToArray();
        }

        public void Decode_Data(byte[] Data)
        {
            int arrayTraverse = 0;
            Decode_Data(Data, ref arrayTraverse, Data.Length);
        }

        public void Decode_Data(byte[] Data, ref int array_traverse, int length)
        {
            try
            {
                byte currentChar = Data[array_traverse++];
                ///Validate Structure
                if (currentChar != (byte)DataTypes._A02_structure || Data[array_traverse++] != 3)
                    throw new DLMSDecodingException("Invalid St_Timer Reset Structure format", "Decode_Data_ICustomStructure");
                ///Decode Interval Data Type Integer
                Hour = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length));
                ///Decode Interval Data Type _Integer
                Minute = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length));
                ///Decode Enable/Disable Data Type _A03_Boolean
                Enable = Convert.ToBoolean(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length));
            }
            catch (Exception ex)
            {
                throw new DLMSDecodingException(String.Format("Error Decoding St_TimerReset Structure"), "Decode_Data", ex);
            }
        }
        #endregion

        #region Clone
        public object Clone()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
