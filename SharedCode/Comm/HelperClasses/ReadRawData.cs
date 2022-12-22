using System;
using DLMS;
using DLMS.Comm;

namespace SharedCode.Comm.HelperClasses
{
    public class ReadRawData
    {
        public ReadRawData()
        {
            initializeByteArray(this.rawBytes);
        }
        #region DataMembers
        private ushort address;
        private ushort length;
        private byte EPMx = 1;
        private byte ePM_Number;

        private string string_ReadRawData;

        public byte[] rawBytes = new byte[6]{0,0,0,0,0,0};
        #endregion

        #region Properties
        public ushort Address
        {
            get { return address; }
            set
            {
                if (value > 0 && value <= 65535)
                {
                    address = value;
                    makeOctetString();
                }
                else throw new Exception("Address value must be between 1 and 65535");
            }
        }
        public ushort Length
        {
            get { return length; }
            set
            {
                if (value > 0 && value < 1025)
                {
                    length = value;
                    makeOctetString();
                }
                else throw new Exception("Length value must be between 1 and 1024");
            }
        }
        public byte EPMx1
        {
            get { return EPMx; }
        }
        public byte EPM_Number
        {
            get { return ePM_Number; }
            set
            {
                if (value >= 0 && value < 4)
                {
                    ePM_Number = value;
                    makeOctetString();
                }
                else throw new Exception("EPM_Number value must be between 0 and 3");


            }
        }
        #endregion

        public Base_Class Encode_ReadRawData(GetSAPEntry CommObjectGetter)
        {
            Class_1 read_raw_data = (Class_1)CommObjectGetter.Invoke(Get_Index.Get_Set_Raw_Data_address);
            read_raw_data.EncodingAttribute = 0x02;
            read_raw_data.EncodingType = DataTypes._A09_octet_string;

         //  if (!String.IsNullOrEmpty(string_ReadRawData))
            {
               // byte[] custRefCode = Encoding.UTF8.GetBytes(string_ReadRawData);
               //  byte[] custRefCode = Encoding.ASCII.GetBytes(string_ReadRawData);

              //  read_raw_data.Value_Array = custRefCode;
                read_raw_data.Value_Array = rawBytes;
            }
           // else
              //  read_raw_data.Value_Array = null;
            return read_raw_data;
        }

        private void makeOctetString()
        {
            //string_ReadRawData = "";
            //string_ReadRawData += Address.ToString("0000");
            //string_ReadRawData += Length.ToString("0000");
            //string_ReadRawData += EPMx1.ToString("00");
            //string_ReadRawData += EPM_Number.ToString("00");
            rawBytes[0] = (byte)Address;
            rawBytes[1] = (byte)(Address >> 8);
            rawBytes[2] = (byte)(Length);
            rawBytes[3] = (byte)(Length>>8);
            rawBytes[4] = EPMx1;
            rawBytes[5] = EPM_Number;

        }

        private void initializeByteArray(byte[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 0;
            }
        }

    }
}
