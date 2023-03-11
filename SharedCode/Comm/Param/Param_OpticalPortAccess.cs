using DLMS;
using DLMS.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedCode.Comm.Param
{
    public class Param_OpticalPortAccess : ICustomStructure, IParam
    {
        #region Properties
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        #endregion

        #region ICUstomeStructure
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void Decode_Data(byte[] Data)
        {
            int array_traverser = 0;
            Decode_Data(Data, ref array_traverser, Data.Length);
        }

        public void Decode_Data(byte[] Data, ref int array_traverse, int length)
        {
            byte[] temp = new byte[12];
            byte currentByte = Data[array_traverse++];
            if (currentByte != (byte)DataTypes._A02_structure && Data[array_traverse] != 11)
                throw new DLMSDecodingException("Invalid ICustomStructure Param_Port_Access Structure received", "Decode_Data_Param_Optical_Port_Access");
            array_traverse++;
            if (Data[array_traverse++] == (byte)DataTypes._A09_octet_string && Data[array_traverse++] == 12)
            {
                Array.Copy(Data, array_traverse, temp, 0, 12);
                this.StartTime = BasicEncodeDecode.Decode_DateTime(temp);
                array_traverse += 12;
            }

            if (Data[array_traverse++] == (byte)DataTypes._A09_octet_string && Data[array_traverse++] == 12)
            {
                Array.Copy(Data, array_traverse, temp, 0, 12);
                this.EndTime = BasicEncodeDecode.Decode_DateTime(temp);
                array_traverse += 12;
            }
        }

        public byte[] Encode_Data()
        {
            try
            {
                byte[] encodedDate=null;
                List<byte> encodeRaw = new List<byte>(50);
                encodeRaw.AddRange(new byte[] { (byte)DataTypes._A02_structure, 2 });
                BasicEncodeDecode.Encode_DateTime(this.StartTime, ref encodedDate);
                encodeRaw.Add((byte)DataTypes._A09_octet_string);
                encodeRaw.Add((byte)encodedDate.Length);
                encodeRaw.AddRange(encodedDate);
                BasicEncodeDecode.Encode_DateTime(this.EndTime, ref encodedDate);
                encodeRaw.Add((byte)DataTypes._A09_octet_string);
                encodeRaw.Add((byte)encodedDate.Length);
                encodeRaw.AddRange(encodedDate);
                return encodeRaw.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
