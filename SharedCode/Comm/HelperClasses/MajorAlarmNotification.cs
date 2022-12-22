using SharedCode.Comm.EventDispatcher.Contracts;
using System;
using System.Collections.Generic;

namespace SharedCode.Comm.HelperClasses
{
    public class MajorAlarmNotification : IEvent
    {
        private int _eventCode;
        private int _hashCode;
        private List<object> _DataToSave = null;

        public MajorAlarmNotification()
        {
            _eventCode = -1;
            _hashCode = -1;

            OccurrenceTimeStamp = ReceptionTimeStamp = DateTime.MinValue;
        }

        public MajorAlarmNotification(int eventCode)
        {
            _eventCode = eventCode;
            _hashCode = GetHashCode();

            OccurrenceTimeStamp = DateTime.MinValue;
            ReceptionTimeStamp = DateTime.Now;
        }


        public List<object> DataToSave
        {
            get { return _DataToSave; }
            set { _DataToSave = value; }
        }

        public DateTime OccurrenceTimeStamp
        {
            get;
            set;
        }

        public DateTime ReceptionTimeStamp
        {
            get;
            set;
        }

        public int EventCode
        {
            get { return _eventCode; }
            set { _eventCode = value; }
        }

        public void Init()
        {
            _eventCode = -1;
            _hashCode = -1;

            OccurrenceTimeStamp =
            ReceptionTimeStamp = DateTime.MinValue;
        }

        public int HashCode
        {
            get
            {
                if (_hashCode <= 0)
                {
                    _hashCode = GetHashCode();
                }
                return _hashCode;
            }
        }

        private new int GetHashCode()
        {
            // Ref: http://stackoverflow.com/a/263416/254215
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + GetType().ToString().GetHashCode();
                hash = hash * 23 + _eventCode.GetHashCode();

                if (OccurrenceTimeStamp != null &&
                    OccurrenceTimeStamp != DateTime.MinValue)
                    hash = hash * 23 + OccurrenceTimeStamp.GetHashCode();

                if (ReceptionTimeStamp != null &&
                    ReceptionTimeStamp != DateTime.MinValue)
                    hash = hash * 23 + ReceptionTimeStamp.GetHashCode();

                return hash;
            }
        }

        public override string ToString()
        {
            String ToStringMsg = "Major Alarm Notify \r\n";

            try
            {
                ToStringMsg += string.Format("Major Alarm Code {0}\r\n Process Stamp {1}\r\n OccurrenceTimeStamp {2}\r\n",
                                             EventCode, ReceptionTimeStamp, OccurrenceTimeStamp);
            }
            catch
            {
                ToStringMsg += "Error";
            }

            return ToStringMsg;
        }

    }
}
