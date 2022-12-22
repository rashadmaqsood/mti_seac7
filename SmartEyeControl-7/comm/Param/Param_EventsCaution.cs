using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DLMS;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DLMS.Comm;

namespace comm
{
    [Serializable]
    [XmlInclude(typeof(Param_EventsCaution))]
    public class Param_EventsCaution : ISerializable, IParam, ICustomStructure, ICloneable
    {
        #region Data_Members

        [XmlIgnore()]
        public string Event_Name;
        private MeterEvent? eventId;
        [XmlIgnore()]
        public int Event_Code;
        [XmlIgnore()]
        public byte CautionNumber;
        [XmlIgnore()]
        public byte FlashTime;
        [XmlIgnore()]
        public byte Flag;
        [XmlIgnore()]
        public bool to_Read;
        [XmlIgnore()]
        public double eventCounter;

        #endregion

        #region Properties

        [XmlIgnore()]
        public MeterEvent? EventId
        {
            get { return eventId; }
            set
            {
                eventId = value;
            }
        }

        [XmlIgnore()]
        public bool IsDisplayCaution
        {
            get
            {
                return ((Flag & 0x01) != 0);
            }
            set
            {
                if (value)
                    Flag = (byte)(Flag | 0x01);
                else
                    Flag = (byte)(Flag & 0xFE);
            }
        }

        [XmlIgnore()]
        public bool IsReadCaution
        {
            get
            {
                return (Flag & 0x02) != 0;
            }
            set
            {
                if (value)
                    Flag = (byte)(Flag | 0x02);
                else
                    Flag = (byte)(Flag & 0xFD);
            }
        }

        [XmlIgnore()]
        public bool IsFlashCaution
        {
            get
            {
                return (Flag & 0x04) != 0;
            }
            set
            {
                if (value)
                    Flag = (byte)(Flag | 0x04);
                else
                    Flag = (byte)(Flag & 0xFB);
            }
        }
        [XmlIgnore()]
        public bool IsDisableLog
        {
            get
            {
                return (Flag & 0x80) != 0;
            }
            set
            {
                if (value)
                    Flag = (byte)(Flag | 0x80);
                else
                    Flag = (byte)(Flag & 0x7F);
            }
        }
        [XmlIgnore()]
        public bool IsExcludeFromLogBook
        {
            get
            {
                return (Flag & 0x40) != 0;
            }
            set
            {
                if (value)
                    Flag = (byte)(Flag | 0x40);
                else
                    Flag = (byte)(Flag & 0xBF);
            }
        }

        #endregion

        public Param_EventsCaution()
        {
        }

        public static int Param_EventsCautionSort_Helper(Param_EventsCaution X, Param_EventsCaution y)
        {
            if (X == null && y != null)
                return -1;
            else if (y == null)
                return 1;
            else
                return X.EventId.Value.CompareTo(y.EventId.Value);
        }

        #region ICustomStructure Members

        public byte[] Encode_Data()
        {
            try
            {
                byte temp = 0;
                List<byte> encodeRaw = new List<byte>(50);
                encodeRaw.AddRange(new byte[] { (byte)DataTypes._A02_structure, 4 });

                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.Event_Code));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.CautionNumber));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.FlashTime));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.Flag));

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
                int temp = 0;
                byte currentByte = Data[array_traverse++];
                if (currentByte != (byte)DataTypes._A02_structure && Data[array_traverse] != 4)
                    throw new DLMSDecodingException("Invalid ICustomStructure Param_EventsCaution Structure received", "decode Param_EventsCaution");
                array_traverse++;

                this.Event_Code = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                this.CautionNumber = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                this.FlashTime = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                this.Flag = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
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

        #region ISerializable Members

        protected Param_EventsCaution(SerializationInfo info, StreamingContext context)
        {
            try
            {
                ///Get MeterEventId Type short
                eventId = (MeterEvent?)info.GetInt16("MeterEventNo");
                ///Get MeterEventName Type String
                Event_Name = info.GetString("MeterEventName");
                ///Get MeterEventCode Type int
                Event_Code = info.GetInt32("MeterEventCode");
                ///Get CautionNumber Type byte
                this.CautionNumber = info.GetByte("CautionNumber");
                ///Get FlashTime Type byte
                this.FlashTime = info.GetByte("FlashTime");
                ///Get Flag Type byte
                this.Flag = info.GetByte("Flag");
                ///Adding eventCounter Type int
                this.eventCounter = info.GetInt32("EventCounter");
            }
            catch
            {
                throw;
            }
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            try
            {
                ///Adding MeterEventId Type short
                info.AddValue("MeterEventNo", (this.EventId != null) ? (short)this.EventId : short.MaxValue, typeof(short));
                ///Adding MeterEventName Type String
                info.AddValue("MeterEventName", this.Event_Name, typeof(String));
                ///Adding MeterEventCode Type int
                info.AddValue("MeterEventCode", this.Event_Code, typeof(int));
                ///Adding CautionNumber Type byte
                info.AddValue("CautionNumber", this.CautionNumber, typeof(byte));
                ///Adding FlashTime Type byte
                info.AddValue("FlashTime", this.FlashTime, typeof(byte));
                ///Adding Flag Type byte
                info.AddValue("Flag", this.Flag, typeof(byte));
                ///Adding eventCounter Type int
                info.AddValue("EventCounter", this.eventCounter);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        public object Clone()
        {
            MemoryStream memStream = null;
            Object dp = null;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (memStream = new MemoryStream(50))
                {
                    formatter.Serialize(memStream, this);
                    memStream.Seek(0, SeekOrigin.Begin);
                    dp = formatter.Deserialize(memStream);
                }
                return dp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Clone Param_MajorAlarmProfile", ex);
            }
        }
    }

    public class Param_EventCautionHelper
    {
        public static List<Param_EventsCaution> Init_ParamEventCaution(List<Param_EventsCaution> AlarmsItems = null)
        {
            try
            {
                var val_Array = Enum.GetValues(typeof(MeterEvent));
                int total_Count_Enum = val_Array.Length;
                Param_EventsCaution alarm_Dummy = new Param_EventsCaution()
                {
                    EventId = MeterEvent.ImbalanceVolt,
                    Event_Name = "ImbalanceVolt",
                    Event_Code = 1
                };
                String key = String.Empty;
                if (AlarmsItems == null)
                    AlarmsItems = new List<Param_EventsCaution>();

                foreach (MeterEvent item in val_Array)
                {
                    key = String.Format("{0}_Caution", item);
                    Param_EventsCaution toSerialize = AlarmsItems.Find((x) => x != null && x.EventId == item);
                    if (toSerialize == null)
                    {
                        alarm_Dummy = new Param_EventsCaution()
                        {
                            EventId = item,
                            Event_Name = "" + item,
                            Event_Code = (int)item,
                            FlashTime = 0
                        };
                        toSerialize = alarm_Dummy;
                        AlarmsItems.Add(toSerialize);
                    }
                }
                AlarmsItems.Sort(Param_EventsCaution.Param_EventsCautionSort_Helper);
                return AlarmsItems;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
