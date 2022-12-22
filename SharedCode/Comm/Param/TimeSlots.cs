using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SharedCode.Comm.Param
{
    [Serializable]
    [XmlInclude(typeof(TimeSlot))]
    public class TimeSlot:ISerializable,ICloneable
    {
        public const ushort MAX_TimeSlot = 6;
        public static readonly TimeSpan StartTime_Min = TimeSpan.FromSeconds(0d);
        public static readonly TimeSpan StartTime_Max = TimeSpan.FromHours(24.0d);
        
        private readonly ushort timeSlotId;
        internal TimeSpan start_Time;
        private Tarrif_ScriptSelector script_Selector;
        public event Action<TimeSlot> StartTime_Changed = delegate { };

        #region Constructurs
        
        public TimeSlot(ushort timeSlotId, TimeSpan startTime, Tarrif_ScriptSelector selector)
            : this(timeSlotId)
        {
            StartTime = startTime;
            ScriptSelector = selector;
        }

        public TimeSlot(ushort timeSlotId)
        {
            if (timeSlotId >= 1 && timeSlotId <= MAX_TimeSlot)
                this.timeSlotId = timeSlotId;
            else
                throw new Exception("Invalid TimeSlot ID selected");
            ScriptSelector = Tarrif_ScriptSelector.T1;         ///Default
            StartTime = new TimeSpan(0,0,0,0,0);               ///Default Time Zero Hour
        }

        public TimeSlot()
            : this(1)                                ///Default Constructur   
        { }
 
        #endregion
        
        public ushort TimeSlotId
        {
            get 
            { 
                return timeSlotId; 
            }
        }

        public TimeSpan StartTime
        {
            get { return start_Time; }
            set 
            {
                if (value.TotalHours <= 24.00d)
                {
                    start_Time = value;
                    StartTime_Changed.Invoke(this);
                }
                else
                    throw new Exception("TimeSlice Value should be less than 24 hours");
            }
        }
        
        public Tarrif_ScriptSelector ScriptSelector
        {
            get { return script_Selector; }
            set { script_Selector = value; }
        }

        #region ISerializable Members

        protected TimeSlot(SerializationInfo info, StreamingContext context)
        {   
            ///Getting TimeSlot ID Type
            timeSlotId = (ushort)info.GetValue("TimeSlotId", typeof(System.UInt16));
            ///Getting TimeSlot Start Time Type Long
            long TimeTicks = (long)info.GetValue("StartTime", typeof(System.Int64));
            this.StartTime = new TimeSpan(TimeTicks);
            ///Getting ScriptSelector Type uShort
            this.ScriptSelector = (Tarrif_ScriptSelector)info.GetValue("TariffScriptSelector", typeof(ushort));
        }

        [SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ///Adding TimeSlot ID Type UShort
            info.AddValue("TimeSlotId", this.TimeSlotId);
            ///Adding TimeSlot Start Time Type Long
            info.AddValue("StartTime", this.StartTime.Ticks);
            ///Adding ScriptSelector Type uShort
            info.AddValue("TariffScriptSelector", (ushort)this.ScriptSelector);
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
                throw new Exception("Error occured while Clone TimeSlot", ex);
            }
        }
    }

    public enum Tarrif_ScriptSelector : ushort
    {
        T1  = 0x01,
        T2,
        T3,
        T4
    }

}
