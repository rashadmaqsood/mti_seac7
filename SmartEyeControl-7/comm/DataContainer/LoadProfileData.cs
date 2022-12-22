using System;
using System.Collections.Generic;
using System.Text;

namespace comm
{
    public enum LoadProfileScheme
    {
        None=0, //by Azeem
        Scheme_1 = 1,
        Scheme_2 = 2,
        PQ_LoadProfile = 3,
    }
    public class LoadProfileData
    {
        private List<LoadProfileChannelInfo> channelsInfo;

        public List<LoadProfileChannelInfo> ChannelsInfo
        {
            get { return channelsInfo; }
            set { channelsInfo = value; }
        }
        private List<LoadProfileItem> channelsInstances;

        public List<LoadProfileItem> ChannelsInstances
        {
            get { return channelsInstances; }
            set { channelsInstances = value; }
        }

        // FIXED Object_Count
        private int fixedObjCount = 0;

        public int FixedObjectCount
        {
            get { return fixedObjCount; }
            set { fixedObjCount = value; }
        }

        public int MaxDynamicChannelCount
        {
            get
            {
                int MaxDynamicChannelCount = -1;

                if (channelsInfo != null && channelsInfo.Count > 0)
                {
                    MaxDynamicChannelCount = channelsInfo.Count;
                }

                return MaxDynamicChannelCount;
            }
        }

        public int DynamicChannelCount
        {
            get
            {
                int DynamicChannelCount = -1;

                if (ChannelsInstances != null &&
                    ChannelsInstances.Count > 0)
                {
                    DynamicChannelCount = ChannelsInstances[0].LoadProfileInstance.Count;
                }

                return DynamicChannelCount;
            }
        }
    }

    public class LoadProfileItem
    {
        private List<double> loadProfileInstance;
        private List<byte> statusWord;

        public List<double> LoadProfileInstance
        {
            get { return loadProfileInstance; }
            set { loadProfileInstance = value; }
        }
        private DateTime date_timeStamp;

        public DateTime DateTimeStamp
        {
            get { return date_timeStamp; }
            set { date_timeStamp = value; }
        }

        private uint counter;

        public uint Counter
        {
            get { return counter; }
            set { counter = value; }
        }

        private uint interval;

        public uint Interval
        {
            get { return interval; }
            set { interval = value; }
        }

        public List<byte> StatusWord
        {
            get { return statusWord; }
            set { statusWord = value; }
        }

        public LoadProfileItem()
        {
            loadProfileInstance = new List<double>();
            date_timeStamp = new DateTime();
            Counter = 0;
            StatusWord = new List<byte>();
        }
    }
}
