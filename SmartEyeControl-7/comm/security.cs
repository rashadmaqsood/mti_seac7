using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartEyeControl_7.comm
{
    public class security
    {
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
