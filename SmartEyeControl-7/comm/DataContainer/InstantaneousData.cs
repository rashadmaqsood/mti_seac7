using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS;
using DLMS.Comm;

namespace comm
{
    public class InstantaneousData
    {
        #region DataMembers
        private List<InstantaneousItem> instantaneousItems;
        private StDateTime timeStamp;
        private double frequency;
        
        #endregion

        #region Properties
        public StDateTime TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }
        public double Frequency
        {
            get { return frequency; }
            set { frequency = value; }
        }
        public List<InstantaneousItem> InstantaneousItems
        {
            get { return instantaneousItems; }
            set { instantaneousItems = value; }
        } 
        #endregion

        public InstantaneousData()
        {
            instantaneousItems = new List<InstantaneousItem>();
            this.timeStamp = new StDateTime();
            this.Frequency = 0.0;
        }
    }
}
