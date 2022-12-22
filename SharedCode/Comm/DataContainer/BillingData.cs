using System;
using System.Collections.Generic;
using System.Text;
using DLMS;
using DLMS.Comm;
using SharedCode.Comm.Param;

namespace SharedCode.Comm.DataContainer
{
    public class BillingData:IParam
    {
        private List<BillingItem> billingItems;
        private DateTime timeStamp;
        private StDateTime timeStampRaw;
        private uint billingCounter;
        private uint billingCounterDay;

        public StDateTime TimeStampRaw
        {
            get { return timeStampRaw; }
            set { timeStampRaw = value; }
        }
        private DateTime timeStampDay;
        public DateTime TimeStampDay
        {
            get { return timeStampDay; }
            set { timeStampDay = value; }
        }
        public DateTime TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }

        public uint BillingCounter
        {
            get { return billingCounter; }
            set { billingCounter = value; }
        }
        public uint BillingCounterDay
        {
            get { return billingCounterDay; }
            set { billingCounterDay = value; }
        }

        public List<BillingItem> BillingItems
        {
            get { return billingItems; }
            set { billingItems = value; }
        }

        public BillingData()
        {
            billingItems = new List<BillingItem>();
            billingCounter = 0;
        }
    }
}
