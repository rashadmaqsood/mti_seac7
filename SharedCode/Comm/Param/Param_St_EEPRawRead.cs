using DLMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SharedCode.Comm.Param
{
    [XmlInclude(typeof(Param_St_EEPRawRead))]
    [Serializable]
    public class Param_St_EEPRawRead : ICloneable, IEquatable<Param_St_EEPRawRead>, IComparer<Param_St_EEPRawRead>
    {
        #region Data_Members

        /// <summary>
        /// 2-byte unsigned short Address of EEPROM,
        /// Address Validation Value 0x0000 to 0xFFFF
        /// </summary>
        public ushort Address { get; set; }
        /// <summary>
        /// 2-byte unsigned short RAW Data block length,
        /// Length Validation Value 0x0000 to 0x0400(1024)
        /// </summary>
        public ushort Length { get; set; }
        /// <summary>
        /// 1-byte unsigned RAW Don't Care Value,
        /// </summary>
        public byte EPMx;
        /// <summary>
        /// 1-byte unsigned EPM_Number Module Number,
        /// Length Validation Value 0 to 3
        /// </summary>
        public byte EPM_Number { get; set; }
        /// <summary>
        /// The TAG is description about memory reference(Location Description,short notes)
        /// </summary>
        public string TAG { get; set; }

        public const string HEX_Str_Format = "[{0:X1}][{1:X4}]--[{2}]";

        #endregion

        /// <summary>
        /// Copy Constructor
        /// </summary>
        public Param_St_EEPRawRead(Param_St_EEPRawRead obj)
        {

            Address = obj.Address;
            Length = obj.Length;
            EPMx = obj.EPMx;
            EPM_Number = obj.EPM_Number;
            this.TAG = obj.TAG;
        }

        public Param_St_EEPRawRead()
        {

            Address = 0x0000;
            Length = 1024;
            EPMx = 0;
            EPM_Number = 0;
            TAG = string.Empty;

        }

        #region ICustomStructure Members

        public byte[] Encode_Data()
        {
            try
            {
                string NullString = Convert.ToChar(00).ToString();

                List<byte> encodeRaw = new List<byte>(50);
                //encodeRaw.AddRange(new byte[] { (byte)DataTypes._A02_structure, 04 });
                //Address
                encodeRaw.Add((byte)(this.Address & 0xFF));
                encodeRaw.Add((byte)((this.Address & 0xFF00) >> 8));
                ///Length 
                encodeRaw.Add((byte)(this.Length & 0xFF));
                encodeRaw.Add((byte)((this.Length & 0xFF00) >> 8));
                ///EPMx
                encodeRaw.Add(this.EPMx);
                ///EPM_Number
                encodeRaw.Add(this.EPM_Number);

                return encodeRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex is DLMSEncodingException || ex is DLMSException)
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException("Error occurred while encoding ICustomStructure Param_EEPRawRead_Address", "Encode_Data_Param_EEPRawRead_Address", ex);
            }
        }

        public void Decode_Data(byte[] Data)
        {
            int array_traverse = 0;
            this.Decode_Data(Data, ref array_traverse, Data.Length);
        }

        public void Decode_Data(byte[] Data, ref int array_traverse, int length)
        {
            try
            {
                //byte currentByte = Data[array_traverse++];
                //if (currentByte != (byte)DataTypes._A02_structure && Data[array_traverse] != 04)
                //    throw new DLMSDecodingException("Invalid ICustomStructure Param_EEPRawRead_Address Structure received", "Decode_Data_Param_EEPRawRead_Address");
                //array_traverse++;

                this.Address = (ushort)(Data[array_traverse] + Data[array_traverse + 1] << 8);
                array_traverse += 2;

                this.Length = (ushort)(Data[array_traverse] + Data[array_traverse + 1] << 8);
                array_traverse += 2;

                this.EPMx = (byte)(Data[array_traverse++]);
                this.EPM_Number = (byte)(Data[array_traverse]);

            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException || ex is DLMSException)
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException("Error occurred while decoding ICustomStructure Param_EEPRawRead_Address", "Decode_Data_Param_EEPRawRead_Address", ex);
            }
        }

        #endregion

        #region ICloneable Members

        object ICloneable.Clone()
        {
            return new Param_St_EEPRawRead(this);
        }

        #endregion


        public bool Equals(Param_St_EEPRawRead other)
        {
            bool isEqual = this == other ||
                   (this.EPM_Number == other.EPM_Number &&
                   this.Address == other.Address &&
                   this.Length == other.Length);

            //Debug.Assert(isEqual, "Error Not Equal");

            return isEqual;
        }


        public int Compare(Param_St_EEPRawRead x, Param_St_EEPRawRead y)
        {
            int Cmp_OutCome = -1;
            try
            {
                int x_Res = 0, y_Res = 0;

                if (x == null && y == null)
                    return 0;
                else if (x == null)
                    return -1;
                else if (y == null)
                    return 1;
                else
                {
                    x_Res = (x.EPM_Number << 16) + x.Address;
                    y_Res = (y.EPM_Number << 16) + y.Address;

                    Cmp_OutCome = x_Res.CompareTo(y_Res);
                }
            }
            catch { }
            return Cmp_OutCome;
        }

        public override string ToString()
        {
            string formated_Str = String.Empty;
            try
            {
                formated_Str = String.Format(HEX_Str_Format, EPM_Number, Address, Length);
                if (!String.IsNullOrEmpty(TAG))
                    formated_Str = string.Format("{0} {1}", formated_Str, TAG);
            }
            catch { }
            return formated_Str;
        }

        public string ToString(int ShortFormat)
        {
            string formated_Str = String.Empty;
            try
            {
                formated_Str = String.Format(HEX_Str_Format, EPM_Number, Address, Length);
                //if (!String.IsNullOrEmpty(TAG))
                //    formated_Str = string.Format("{0} {1}", formated_Str, TAG);
            }
            catch { }
            return formated_Str;
        }
    }

    public class EEPRawReadData
    {
        public Param_St_EEPRawRead Param_St_EEPRawRead;

        public DateTime Capture_Stamp = DateTime.MinValue;
        public byte[] RawData;
    }
}
