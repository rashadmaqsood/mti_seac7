using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DLMS;
using DLMS.Comm;

namespace SharedCode.Comm.Param
{
    [XmlInclude(typeof(Param_TimeBaseEvents))]
    [Serializable]
    public class Param_TimeBaseEvents : ICustomStructure,IParam
    {
        #region Data Members
        public byte Control_Enum;
        public ushort Interval;
        public StDateTime DateTime;

        [XmlIgnore()]
        public const int Tb_Disable = 0;
        [XmlIgnore()]
        public const int Tb_DateTime = 1;
        [XmlIgnore()]
        public const int Tb_Interval = 2;
        [XmlIgnore()]
        public const int Tb_IntervalTimeSink = 3;
        [XmlIgnore()]
        public const int Tb_Fixed = 4;


        #endregion

        #region Properties
        public TimeSpan IntervalSink
        {
            get 
            {
                ushort min, sec;
                sec = (ushort)(0x00FF & Interval);
                min = (ushort)(0x00FF & (Interval >> 8));
                return new TimeSpan(0, min, sec);
            }
        }

        #endregion

        public Param_TimeBaseEvents()
        {
            this.DateTime = new StDateTime();
            this.DateTime.Year = StDateTime.NullYear;
            this.DateTime.Month = StDateTime.Null;
            this.DateTime.DayOfMonth = StDateTime.Null;
            this.DateTime.DayOfWeek = StDateTime.Null;
            this.DateTime.Hour = StDateTime.Null;
            this.DateTime.Minute = StDateTime.Null;
            this.DateTime.Second = StDateTime.Null;
            this.DateTime.Hundred = StDateTime.Null;
        }

        #region ICustomStructure Members

        public byte[] Encode_Data()
        {
            try
            {
                byte temp = 0;
                List<byte> encodeRaw = new List<byte>(50);
                encodeRaw.AddRange(new byte[] { (byte)DataTypes._A02_structure, 4 });

                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A16_enum, this.Control_Enum));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.Interval));
                encodeRaw.AddRange(DateTime.EncodeRawBytes(StDateTime.DateTimeType.Time));
                encodeRaw.AddRange(DateTime.EncodeRawBytes(StDateTime.DateTimeType.Date));

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
            Decode_Data(Data, ref array_traverser,Data.Length);
        }

        public void Decode_Data(byte[] Data, ref int array_traverse,int length)
        {
            try
            {
                DateTime = new StDateTime();
                byte currentByte = Data[array_traverse++];
                if (currentByte != (byte)DataTypes._A02_structure && Data[array_traverse] != 4)
                    throw new DLMSDecodingException("Invalid ICustomStructure Param_EventsCaution Structure received", "decode Param_EventsCaution");
                array_traverse++;

                this.Control_Enum = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                this.Interval = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                DateTime.DecodeRawBytes(Data, ref array_traverse);
                DateTime.DecodeRawBytes(Data, ref array_traverse);

                
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSDecodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException("Error occurred while decoding ICustomStructure Param_EventsCaution", "Param_EventsCaution", ex);
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
