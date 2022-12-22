using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DLMS.Comm;

namespace SharedCode.Comm.DataContainer
{
    [Serializable]
    public class EventItem:ICloneable
    {
        #region DataMembers
        private StDateTime dateTimeStamp;
        private EventInfo eventInfo;
        private uint eventCounter;
        private byte[] eventDetailString;
        private string eventDetailStr;
        #endregion

        #region Property
        public StDateTime DateTimeStamp
        {
            get { return dateTimeStamp; }
            set { dateTimeStamp = value; }
        }

        public DateTime EventDateTimeStamp
        {
            get 
            {
                if (DateTimeStamp != null && DateTimeStamp.IsDateTimeConvertible)
                    return DateTimeStamp.GetDateTime();
                else if (DateTimeStamp != null && DateTimeStamp.IsDateConvertible)
                    return DateTimeStamp.GetDate();
                else if (DateTimeStamp != null && DateTimeStamp.IsTimeConvertible)
                    return new DateTime().Add(DateTimeStamp.GetTime());
                else
                    throw new Exception("Unable to obtain event datetime stamps");

            }
            set
            {
                if (DateTimeStamp == null)
                {
                    DateTimeStamp = new StDateTime();
                }
                DateTimeStamp.SetDateTime(value);
            }
        }

        public uint EventCounter
        {
            get { return eventCounter; }
            set { eventCounter = value; }
        }

        public byte[] EventDetailString
        {
            get { return eventDetailString; }
            set { eventDetailString = value; }
        }

        public string EventDetailStr
        {
            get { return eventDetailStr; }
            set { eventDetailStr = value; }
        }
        
        public EventInfo EventInfo
        {
            get { return eventInfo; }
            set { eventInfo = value; }
        }
        #endregion
        
        #region Constructor
        public EventItem()
        {
            ///EventDetailString = new byte[8];
            this.EventInfo = new EventInfo();
        }

        public EventItem(EventItem otherObj)
        {
            this.DateTimeStamp = new StDateTime(otherObj.DateTimeStamp);
            this.eventCounter = otherObj.eventCounter;
            this.EventDetailString = otherObj.EventDetailString;
            this.EventInfo = (otherObj.EventInfo != null)? (EventInfo)otherObj.EventInfo.Clone():null;
            //Array.Copy(otherObj.EventDetailString, this.EventDetailString,otherObj.EventDetailString.Length);
        }
        #endregion

        #region ICloneable Members
        public object Clone()
        {
            EventItem clonee = new EventItem(this);
            return clonee;
        }
        #endregion

        public override string ToString()
        {
            try
            {
                DateTime DateTimeStamp = DateTime.MinValue;
                try
                {
                    DateTimeStamp = this.EventDateTimeStamp;
                }
                catch (Exception)
                { }
                string eventItemStr = String.Format("{0} {1} {2} {3}", this.EventInfo,
                    this.EventCounter,
                    (this.EventDateTimeStamp == DateTime.MinValue)? (Object)this.DateTimeStamp:(Object)DateTimeStamp,
                    this.EventDetailString);
                return eventItemStr;
            }
            catch (Exception ex)
            {

                return base.ToString();
            }
            
        }
    }
    [Serializable]
    public class EventInfo:ICloneable
    {
        #region Data_Members
        private MeterEvent? eventId;
        private int eventCode;
        private string eventName;
        private int maxEventCount;
        public const int MaxEventCountNotApplied = int.MinValue;
        #endregion

        #region Properties
        
        public MeterEvent? EventId
        {
            get { return eventId; }
            set { eventId = value; }
        }

        public int _EventId
        {
            get 
            {
                if (eventId == null) return 0;
                return (int)eventId; 
            }
            set 
            {
                try
                {
                    eventId = (MeterEvent)value;
                    if (String.IsNullOrEmpty(Enum.GetName(typeof(MeterEvent), eventId)))
                        EventId = null;
                }
                catch (Exception)
                {
                    EventId = null;
                }
            }

        }

        public int EventCode
        {
            get { return eventCode; }
            set { eventCode = value; }
        }

        public string EventName
        {
            get { return eventName; }
            set { eventName = value; }
        }

        public int MaxEventCount
        {
            get { return maxEventCount; }
            set { maxEventCount = value; }
        }

        #endregion

        #region Constructors

        public EventInfo()
        {
            EventId = null;
            eventCode = 0;
            eventName = "Combine Events Log";
            maxEventCount = MaxEventCountNotApplied;
        }

        public EventInfo(int eventCode, string eventName, int maxEventCount):this()
        {
            this.EventCode = eventCode;
            this.eventName = eventName;
            this.maxEventCount = maxEventCount;
           
        }
        public EventInfo(int eventId, int eventCode, string eventName, int maxEventCount)
           : this()
        {
            _EventId = eventId;
            this.EventCode = eventCode;
            this.eventName = eventName;
            this.maxEventCount = maxEventCount;
        }
        public EventInfo(MeterEvent eventId,int eventCode, string eventName, int maxEventCount)
            : this()
        {
            EventId = eventId;
            this.EventCode = eventCode;
            this.eventName = eventName;
            this.maxEventCount = maxEventCount;
        }

        public EventInfo(EventInfo otherObj):this()
        {
            this.EventId = otherObj.EventId;
            this.EventCode      = otherObj.EventCode;    
            this.eventName      = otherObj.eventName ;
            this.maxEventCount = otherObj.maxEventCount;
        }
        #endregion

        #region ICloneable Members

        #region ICloneable Members

        object ICloneable.Clone()
        {
            object clonee = new EventInfo(this);
            return clonee;
        }

        public EventInfo Clone()
        {
            EventInfo clonee = new EventInfo(this);
            return clonee;
        }

        #endregion

        #endregion

        public override string ToString()
        {
            return (this.EventName != null) ? this.EventName : this.EventCode.ToString();
        }
    }
    [Serializable]
    public class EventLogInfo : EventInfo,ICloneable
    {
        #region Data_Members
        private Get_Index eventLogIndex;
        private Get_Index eventCounterIndex;
        private long previousEventCounter = long.MinValue;
        private long currentEventCounter = long.MinValue;
        public const long EventCounterNotApplied = long.MinValue;
        #endregion

        #region Constructors
        public EventLogInfo()
        {
            EventLogIndex = Get_Index.Event_Log;
            EventCounterIndex = Get_Index.Event_Count_Total;
        }

        public EventLogInfo(int eventCode, string eventName, Get_Index eventLogInfo, Get_Index eventCounterIndex)
            : base(eventCode, eventName, -1)
        {
            this.eventLogIndex = eventLogInfo;
            this.eventCounterIndex = eventCounterIndex;
        }

        public EventLogInfo(EventInfo InfoObj, Get_Index eventlogInfo, Get_Index eventCounterIndex)
            : base(InfoObj)
        {
            this.EventLogIndex = eventlogInfo;
            this.eventCounterIndex = eventCounterIndex;
        }
        public EventLogInfo(int eventId, int eventCode, string eventName, Get_Index eventLogInfo, Get_Index eventCounterIndex)
     : base(eventId, eventCode, eventName, -1)
        {
            this.eventLogIndex = eventLogInfo;
            this.eventCounterIndex = eventCounterIndex;
        }
        public EventLogInfo(MeterEvent eventId, int eventCode, string eventName, Get_Index eventLogInfo, Get_Index eventCounterIndex)
            : base(eventId, eventCode, eventName, -1)
        {
            this.eventLogIndex = eventLogInfo;
            this.eventCounterIndex = eventCounterIndex;
        }

        public EventLogInfo(EventLogInfo otherObj):base(otherObj)
        {
            this.eventLogIndex =    otherObj.eventLogIndex;
            this.eventCounterIndex = otherObj.eventCounterIndex;

            this.PreviousEventCounter = otherObj.PreviousEventCounter;
            this.CurrentEventCounter = otherObj.CurrentEventCounter;
        }
        #endregion

        #region Properties

        public Get_Index EventLogIndex
        {
            get { return eventLogIndex; }
            set { eventLogIndex = value; }
        }

        public Get_Index EventCounterIndex
        {
            get { return eventCounterIndex; }
            set { eventCounterIndex = value; }
        }

        public long PreviousEventCounter
        {
            get { return previousEventCounter; }
            set { previousEventCounter = value; }
        }

        public long CurrentEventCounter
        {
            get { return currentEventCounter; }
            set { currentEventCounter = value; }
        }
        #endregion

        #region ICloneable Members

        object ICloneable.Clone()
        {
            object clonee = new EventLogInfo(this);
            return clonee;
        }

        public new object Clone()
        {
            Object clonee = new EventLogInfo(this);
            return clonee;
        }

        #endregion

    }

    [Serializable]
    public class EventData //:ICloneable
    {
        #region DataMembers
        private List<EventItem> eventRecords;
        private EventInfo eventInfo;
        public uint MaxCounterRead;
        #endregion

        #region Constructor

        public EventData()
        {
            eventRecords = new List<EventItem>();
        }
        //public EventData(EventData obj)
        //{
        //    eventRecords = new List<EventItem>(obj.EventRecords);
        //    EventInfo = obj.EventInfo;
        //}
        
        #endregion

        #region Property

        public List<EventItem> EventRecords
        {
            get { return eventRecords; }
            set { eventRecords = value; }
        }

        public EventInfo EventInfo
        {
            get { return eventInfo; }
            set { eventInfo = value; }
        }

        public uint MaxEventCounter
        {
            get
            {
                uint t = 0;
                try
                {
                    foreach (var evRecord in EventRecords)
                    {
                        if (evRecord != null && evRecord.EventCounter > t)
                            t = evRecord.EventCounter;
                    }
                }
                catch (Exception ex) { t = 0; }
                return t;
            }
        }

        public uint MinEventCounter
        {
            get
            {
                uint t = uint.MaxValue;
                try
                {
                    foreach (var evRecord in EventRecords)
                    {
                        if (evRecord != null && evRecord.EventCounter < t)
                            t = evRecord.EventCounter;
                    }
                }
                catch (Exception ex) { t = uint.MaxValue; }
                return (t == uint.MaxValue) ? 0 : t;
            }
        }
        
        #endregion

        public override string ToString()
        {
            return this.EventInfo.EventName;
            //return base.ToString();
        }

        public EventData Clone()
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                BinaryFormatter bf = new BinaryFormatter();

                bf.Serialize(ms, this);

                ms.Position = 0;
                object obj = bf.Deserialize(ms);
                ms.Close();

                return obj as EventData;
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

    }

}
