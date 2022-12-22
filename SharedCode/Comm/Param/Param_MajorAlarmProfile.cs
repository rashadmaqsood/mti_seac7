using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DLMS;
using System.Collections;
using SharedCode.Comm.DataContainer;
using System.Text;

namespace SharedCode.Comm.Param
{
    [Serializable]
    [XmlInclude(typeof(Param_MajorAlarmProfile))]
    public class Param_MajorAlarmProfile:IParam,IDisposable, ICloneable
    {
        #region Data_Members
        private List<MajorAlarm> alarmItems;
        public BitArray MA_Status_Array;
        private int _BitLength = 60;
        #endregion


        #region Constructor
        public Param_MajorAlarmProfile()
        {
            MA_Status_Array = new BitArray(_BitLength);
            ///Init Param_MajorAlarmProfiles
            alarmItems = new List<MajorAlarm>();
            foreach (MeterEvent eventItem in Enum.GetValues(typeof(MeterEvent)))
            {
                MajorAlarm m = new MajorAlarm(new EventInfo(eventItem, -1, Enum.GetName(typeof(MeterEvent), eventItem), -1), false, false, false);
                alarmItems.Add(m);
            }
        }

        public Param_MajorAlarmProfile(List<EventInfo> eventInfo, int bitLength)
        {
            _BitLength = bitLength;
            MA_Status_Array = new BitArray(_BitLength);
            AlarmItems = new List<MajorAlarm>();
            if (eventInfo == null && eventInfo.Count > 0)
                return;
            foreach (var itemEevnt in eventInfo)
            {
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
        public List<MajorAlarm> AlarmItems
        {
            get { return alarmItems; }
            set { alarmItems = value; }
        }

        #endregion

        #region Encoders & Decoders routines
        public byte[] Encode_EventFilter(ref int bitLength)
        {
            try
            {
                //bitLength = 48;
                bitLength = _BitLength;
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
                //bitLength = 48;
                bitLength = _BitLength;
                int DataByteCount = Convert.ToInt32(Math.Ceiling((bitLength / 8.0)));
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
                _BitLength = bitLength;
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
                _BitLength = bitLength;
                int DataByteCount = Convert.ToInt32(Math.Ceiling((bitLength / 8.0)));
                BitArray TBit = new BitArray(Data);
                MA_Status_Array = TBit;
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

        public byte[] Encode_AlarmUserStatus(ref int bitLength, EnergyMizerAlarmStatus status)
        {
            try
            {
                byte temp = 0;
                //bitLength = 48;
                bitLength = _BitLength;
                int DataByteCount = Convert.ToInt32(Math.Ceiling((bitLength / 8.0)));
                BitArray TBit = new BitArray(bitLength, false);
                byte[] TArray = new byte[DataByteCount];
                ///Encode Each Alarm Filter Register
                foreach (var item in AlarmItems)
                {
                    if (item != null && item.Info != null && item.Info.EventId != null)
                    {
                        int bitNo = (int)item.Info.EventId;
                        if (item.IsResetUserStatus == status)
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

        public void Decode_AlarmUserStatus(byte[] Data, int bitLength)
        {
            try
            {
                _BitLength = bitLength;
                int DataByteCount = Convert.ToInt32(Math.Ceiling((bitLength / 8.0)));
                BitArray TBit = new BitArray(Data);
                MA_Status_Array = TBit;
                ///Encode Each Alarm Filter Register
                foreach (var item in AlarmItems)
                {
                    if (item != null && item.Info != null && item.Info.EventId != null)
                    {
                        int bitNo = (int)item.Info.EventId;
                        //if (bitNo > 0 && bitNo <= TBit.Count && TBit[bitNo - 1])
                        //{
                        //    item.IsResetUserStatus = true;
                        //}
                        //else
                        //    item.IsResetUserStatus = false;
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

        #region ICloneable Members
        public object Clone()
        {
            Param_MajorAlarmProfile obj = new Param_MajorAlarmProfile();

            if (this.MA_Status_Array == null) //Azeem
            {
                _BitLength = this._BitLength;
                this.MA_Status_Array = new BitArray(_BitLength);
            }
            obj.MA_Status_Array = new BitArray(this.MA_Status_Array);
            obj._BitLength = this._BitLength;
            obj.AlarmItems = new List<MajorAlarm>();
            foreach (MajorAlarm item in this.AlarmItems)
            {
                MajorAlarm mAlarm = new MajorAlarm();
                mAlarm.Info = item.Info.Clone();
                mAlarm.IsMajorAlarm = item.IsMajorAlarm;
                mAlarm.IsReset = item.IsReset;
                mAlarm.IsResetUserStatus = item.IsResetUserStatus;
                mAlarm.IsTriggered = item.IsTriggered;
                obj.AlarmItems.Add(mAlarm);
            }
            return obj;
        }
        #endregion
        public void Dispose()
        {
            if (alarmItems != null)
                alarmItems.Clear();
            alarmItems = null;
            MA_Status_Array = null;
        }
    }
    [Serializable]
    [XmlInclude(typeof(MajorAlarm))]
    public class MajorAlarm
    {
        #region Data_Members
        private EventInfo info;
        private bool isMajorAlarm;
        private bool isTriggered;
        private bool isReset;
        private EnergyMizerAlarmStatus resetUserStatus;
        #endregion

        #region Property
        public EventInfo Info
        {
            get { return info; }
            set { info = value; }
        }

        public bool IsMajorAlarm
        {
            get { return isMajorAlarm; }
            set { isMajorAlarm = value; }
        }

        public bool IsTriggered
        {
            get { return isTriggered; }
            set { isTriggered = value; }
        }

        public bool IsReset
        {
            get { return isReset; }
            set { isReset = value; }
        }
        public EnergyMizerAlarmStatus IsResetUserStatus
        {
            get { return resetUserStatus; }
            set { resetUserStatus = value; }
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

    }

    public class SaveAlarmStatus
    {
        StringBuilder AlarmStatus;

        public SaveAlarmStatus()
        {
            AlarmStatus = new StringBuilder(48);
        }
        public string SaveToClass(List<MajorAlarm> AlarmItems)
        {
            try
            {
                AlarmStatus.Clear();
                foreach (var item in AlarmItems)
                {
                    if (item.IsTriggered)
                        AlarmStatus.Append(1);
                    else
                        AlarmStatus.Append(0);
                }
                return AlarmStatus.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
