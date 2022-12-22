using System;

namespace SharedCode.Comm.HelperClasses
{
    public class security
    {
        private int eventCode;

        public int EventCode
        {
            get { return eventCode; }
            set { eventCode = value; }
        }

        private string eventName;
        private int maxCounter;

        public int MaxCounter
        {
            get { return maxCounter; }
            set { maxCounter = value; }
        }
        public string EventName
        {
            get { return eventName; }
            set { eventName = value; }
        }
        private uint eventCounter;

        public uint EventCounter
        {
            get { return eventCounter; }
            set { eventCounter = value; }
        }
        private DateTime eventLastOccuranceDate;

        public DateTime EventLastOccuranceDate
        {
            get { return eventLastOccuranceDate; }
            set { eventLastOccuranceDate = value; }
        }
        private byte[] eventDetail;
        public byte[] EventDetail
        {
            get { return eventDetail; }
            set { eventDetail = value; }
        }

    }
}
