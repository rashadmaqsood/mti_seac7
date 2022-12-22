using System;
using System.Collections.Generic;
using System.Text;
using DLMS;
using comm;
using DLMS.Comm;


namespace comm
{
    public class BillingData
    {
        private List<BillingItem> billingItems;
        private DateTime timeStamp;
        private StDateTime timeStampRaw;
        private uint billingCounter;

        public StDateTime TimeStampRaw
        {
            get { return timeStampRaw; }
            set { timeStampRaw = value; }
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
