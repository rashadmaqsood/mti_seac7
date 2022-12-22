using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DLMS;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace comm
{
    [Serializable]
    [XmlInclude(typeof(Param_MajorAlarmProfile))]
    [XmlInclude(typeof(MajorAlarm))]
    public class Param_MajorAlarmProfile : ISerializable, ICloneable, IParam
    {
        #region Data_Members

        private List<MajorAlarm> alarmItems;
        [XmlIgnore()]
        public List<MajorAlarm> AlarmItems
        {
            get { return alarmItems; }
            set { alarmItems = value; }
        }

        #endregion

        #region Constructor

        public Param_MajorAlarmProfile()
        {
            ///Init Param_MajorAlarmProfiles
            alarmItems = new List<MajorAlarm>();
            foreach (MeterEvent eventItem in Enum.GetValues(typeof(MeterEvent)))
            {
                MajorAlarm m = new MajorAlarm(new EventInfo(eventItem, -1, Enum.GetName(typeof(MeterEvent), eventItem), -1), false, false, false);
                alarmItems.Add(m);
            }
        }

        public Param_MajorAlarmProfile(List<EventInfo> eventInfo)
        {
            AlarmItems = new List<MajorAlarm>();
            if (eventInfo == null || eventInfo.Count <= 0)
                return;
            foreach (var itemEevnt in eventInfo)
            {
                if (itemEevnt.EventId == null || itemEevnt.EventCode == 0)
                    continue;
                AlarmItems.Add(new MajorAlarm(itemEevnt, false, false, false));
            }
        }

        public Param_MajorAlarmProfile(Param_MajorAlarmProfile paramObj)
        {
            AlarmItems = new List<MajorAlarm>();
            if (paramObj != null)
            {
                this.AlarmItems = new List<MajorAlarm>(paramObj.AlarmItems);
            }
        }

        #endregion

        #region Properties

        #endregion

        #region Encoders & Decoders routines

        public byte[] Encode_EventFilter(ref int bitLength)
        {
            try
            {
                //v4.8.29
                //bitLength = 48;
                bitLength = AlarmItems.Count;
                
                int DataByteCount = Convert.ToInt32(Math.Ceiling((bitLength / 8.0)));
                BitArray TBit = new BitArray(bitLength, false);
                byte[] TArray = new byte[DataByteCount];
                ///Encode Each Alarm Filter Register
                foreach (var item in AlarmItems)
                {
                    if (item != null && item.Info != null && item.Info.EventId != null)
                    {
                        int bitNo = (int)item.Info.EventId;
                        if (item.IsMajorAlarm)
                            if (bitNo > 0 && bitNo <= TBit.Count)
                                TBit[bitNo - 1] = true;
                    }
                }
                TBit.CopyTo(TArray, 0);
                return TArray;
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSEncodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException("Error occurred while encoding Event Filter Param_MajorAlarmProfile", "Param_MajorAlarmProfile", ex);
            }
        }

        public byte[] Encode_AlarmStatus(ref int bitLength)
        {
            try
            {
                byte temp = 0;
                //v4.8.29
                //bitLength = 48;
                bitLength = AlarmItems.Count;
                int DataByteCount = Convert.ToInt32(Math.Ceiling((bitLength / 8.0)));
                //v4.8.29
                //BitArray TBit = new BitArray(48, false);
                BitArray TBit = new BitArray(bitLength, false);
                
                byte[] TArray = new byte[DataByteCount];
                ///Encode Each Alarm Filter Register
                foreach (var item in AlarmItems)
                {
                    if (item != null && item.Info != null && item.Info.EventId != null)
                    {
                        int bitNo = (int)item.Info.EventId;
                        if (item.IsReset)
                            TBit[bitNo - 1] = true;
                    }
                }
                TBit.CopyTo(TArray, 0);
                return TArray;
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSEncodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException("Error occurred while encoding Event Filter Param_MajorAlarmProfile", "Param_MajorAlarmProfile", ex);
            }
        }

        public void Decode_EventFilter(byte[] Data, int bitLength)
        {
            try
            {
                int DataByteCount = Convert.ToInt32(Math.Ceiling((bitLength / 8.0)));
                BitArray TBit = new BitArray(Data);
                ///Encode Each Alarm Filter Register
                foreach (var item in AlarmItems)
                {
                    if (item != null && item.Info != null && item.Info.EventId != null)
                    {
                        int bitNo = (int)item.Info.EventId;
                        if (bitNo > 0 && bitNo <= TBit.Count && TBit[bitNo - 1])
                        {
                            item.IsMajorAlarm = true;
                        }
                        else
                            item.IsMajorAlarm = false;
                    }
                }

            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSEncodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException("Error occurred while decoding Event Filter Param_MajorAlarmProfile", "Param_MajorAlarmProfile", ex);
            }
        }

        public void Decode_AlarmStatus(byte[] Data, int bitLength)
        {
            try
            {
                int DataByteCount = Convert.ToInt32(Math.Ceiling((bitLength / 8.0)));
                BitArray TBit = new BitArray(Data);
                ///Encode Each Alarm Filter Register
                foreach (var item in AlarmItems)
                {
                    if (item != null && item.Info != null && item.Info.EventId != null)
                    {
                        int bitNo = (int)item.Info.EventId;
                        if (bitNo > 0 && bitNo <= TBit.Count && TBit[bitNo - 1])
                        {
                            item.IsTriggered = true;
                        }
                        else
                            item.IsTriggered = false;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSEncodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException("Error occurred while decoding Major Alarm Status Param_MajorAlarmProfile", "Param_MajorAlarmProfile", ex);
            }
        }
        #endregion

        public static int MajorAlarmSort_Helper(MajorAlarm X, MajorAlarm y)
        {
            if (X == null && y != null)
                return -1;
            else if (y == null)
                return 1;
            else
                return X.Info._EventId.CompareTo(y.Info._EventId);
        }

        #region ISerializable Members

        protected Param_MajorAlarmProfile(SerializationInfo info, StreamingContext context)
        {
            try
            {
                List<MajorAlarm> T_AlarmList = new List<MajorAlarm>();
                var val_Array = Enum.GetValues(typeof(MeterEvent));
                int total_Count_Enum = val_Array.Length;
                MajorAlarm alarm_Dummy = new MajorAlarm()
                {
                    Info = new EventInfo(MeterEvent.ImbalanceVolt, 1, "ImbalanceVolt", 50),
                    IsMajorAlarm = true
                };
                String key = String.Empty;
                foreach (MeterEvent item in val_Array)
                {
                    key = String.Format("{0}_Alarm", item);
                    MajorAlarm toSerialize = null;

                    try
                    {
                        toSerialize = (MajorAlarm)info.GetValue(key, typeof(MajorAlarm));
                    }
                    catch (Exception ex)
                    {
                        if (toSerialize == null)
                        {
                            alarm_Dummy = new MajorAlarm()
                            {
                                Info = new EventInfo(item, (short)item, "" + item, 50),
                                IsMajorAlarm = true

                            };
                            toSerialize = alarm_Dummy;
                        }
                    }
                    T_AlarmList.Add(toSerialize);
                }
                T_AlarmList.Sort(MajorAlarmSort_Helper);
                if (T_AlarmList == null || T_AlarmList.Count != total_Count_Enum)
                    throw new InvalidOperationException(String.Format("Invalid Param_MajorAlarmProfile Structure Count {0}", T_AlarmList.Count));
                AlarmItems = T_AlarmList;
            }
            catch (Exception ex)
            {
                //throw;
            }
        }


        /* //Commented in v4.8.29
        protected Param_MajorAlarmProfile(SerializationInfo info, StreamingContext context)
        {
            try
            {
                List<MajorAlarm> T_AlarmList = new List<MajorAlarm>();
                var val_Array = Enum.GetValues(typeof(MeterEvent));
                int total_Count_Enum = val_Array.Length;
                MajorAlarm alarm_Dummy = new MajorAlarm()
                {
                    Info = new EventInfo(MeterEvent.ImbalanceVolt, 1, "ImbalanceVolt", 50),
                    IsMajorAlarm = true
                };
                String key = String.Empty;
                foreach (MeterEvent item in val_Array)
                {
                    key = String.Format("{0}_Alarm", item);
                    MajorAlarm toSerialize = null;
                    toSerialize = (MajorAlarm)info.GetValue(key, typeof(MajorAlarm));
                    if (toSerialize == null)
                    {
                        alarm_Dummy = new MajorAlarm()
                        {
                            Info = new EventInfo(item, (short)item, "" + item, 50),
                            IsMajorAlarm = true
                        };
                        toSerialize = alarm_Dummy;
                    }
                    T_AlarmList.Add(toSerialize);
                }
                T_AlarmList.Sort(MajorAlarmSort_Helper);
                if (T_AlarmList == null || T_AlarmList.Count != total_Count_Enum)
                    throw new InvalidOperationException(String.Format("Invalid Param_MajorAlarmProfile Structure Count {0}", T_AlarmList.Count));
                AlarmItems = T_AlarmList;
            }
            catch
            {
                throw;
            }
        }
        */
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            try
            {
                var val_Array = Enum.GetValues(typeof(MeterEvent));
                int total_Count_Enum = val_Array.Length;
                MajorAlarm alarm_Dummy = new MajorAlarm() { Info = new EventInfo(MeterEvent.ImbalanceVolt, 1, "ImbalanceVolt", 50), IsMajorAlarm = true };
                String key = String.Empty;
                List<MajorAlarm> AlarmsItems = new List<MajorAlarm>();
                foreach (MeterEvent item in val_Array)
                {
                    key = String.Format("{0}_Alarm", item);
                    MajorAlarm toSerialize = AlarmItems.Find((x) => x != null && x.Info != null && x.Info.EventId == item);
                    if (toSerialize == null)
                    {
                        alarm_Dummy = new MajorAlarm() { Info = new EventInfo(item, (short)item, "" + item, 50), IsMajorAlarm = true };
                        toSerialize = alarm_Dummy;
                    }
                    info.AddValue(key, toSerialize);
                }
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
                using (memStream = new MemoryStream(256))
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

    [Serializable]
    public class MajorAlarm : ISerializable, ICloneable
    {
        #region Data_Members

        private EventInfo info;
        private bool isMajorAlarm;
        private bool isTriggered;
        private bool isReset;

        #endregion

        #region Property

        [XmlIgnore]
        public EventInfo Info
        {
            get { return info; }
            set { info = value; }
        }

        [XmlIgnore]
        public bool IsMajorAlarm
        {
            get { return isMajorAlarm; }
            set { isMajorAlarm = value; }
        }

        [XmlIgnore]
        public bool IsTriggered
        {
            get { return isTriggered; }
            set { isTriggered = value; }
        }

        [XmlIgnore]
        public bool IsReset
        {
            get { return isReset; }
            set { isReset = value; }
        }

        #endregion

        #region Constructur

        public MajorAlarm()
        {
            info = null;
            isMajorAlarm = false;
            isTriggered = false;
            isReset = false;
        }

        public MajorAlarm(EventInfo eventInfo,
                    bool isMajorAlarm,
                    bool isTriggered,
                    bool isReset)
        {
            Info = eventInfo;
            IsMajorAlarm = isMajorAlarm;
            IsTriggered = isTriggered;
            IsReset = isReset;
        }

        /// <summary>
        /// Deep Copy Constructur
        /// </summary>
        /// <param name="otherObj"></param>
        public MajorAlarm(MajorAlarm otherObj)
        {
            if (otherObj == null)
            {
                return;
            }
            Info = new EventInfo(otherObj.Info);
            IsMajorAlarm = otherObj.IsMajorAlarm;
            IsTriggered = otherObj.IsTriggered;
            IsReset = otherObj.IsReset;
        }

        #endregion

        #region ISerializable Members

        protected MajorAlarm(SerializationInfo info, StreamingContext context)
        {
            short MeterEventId = 0;
            String EventName = String.Empty;
            ///Getting MeterEventId Type short
            MeterEventId = (short)info.GetValue("MeterEventNo", typeof(short));
            ///Getting MeterEventName
            EventName = info.GetString("MeterEventName");
            ///Getting MajorAlarm_Flag Type bool
            IsMajorAlarm = info.GetBoolean("MajorAlarm_Flag");
            ///Getting AlarmTriggered_Flag Type bool
            IsTriggered = info.GetBoolean("AlarmTriggered_Flag");
            if (String.IsNullOrEmpty(EventName))
                EventName = "NIL";
            ///Populate EventInfo Object
            this.info = new EventInfo(MeterEventId, EventName, 50) { _EventId = MeterEventId };
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ///Adding MeterEventId Type short
            info.AddValue("MeterEventNo", this.info._EventId, typeof(short));
            ///Adding MeterEventName Type String
            info.AddValue("MeterEventName", this.info.EventName, typeof(String));
            ///Adding MajorAlarm_Flag Type bool
            info.AddValue("MajorAlarm_Flag", this.IsMajorAlarm);
            ///Adding AlarmTriggered_Flag Type bool
            info.AddValue("AlarmTriggered_Flag", this.IsTriggered);
        }

        #endregion

        public object Clone()
        {
            MemoryStream memStream = null;
            Object dp = null;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (memStream = new MemoryStream(10))
                {
                    formatter.Serialize(memStream, this);
                    memStream.Seek(0, SeekOrigin.Begin);
                    dp = formatter.Deserialize(memStream);
                }
                return dp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Clone MajorAlarm", ex);
            }
        }
    }
}
