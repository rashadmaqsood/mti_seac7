using SharedCode.Comm.Param;
using System;
using System.Collections.Generic;
using System.Text;
namespace SharedCode.Comm.DataContainer
{
    public enum LoadProfileScheme:byte
    {
        None = 0, //by Azeem
        Load_Profile = 1,
        Load_Profile_Channel_2 = 2,
        Daily_Load_Profile = 4,
        Daily_Load_Profile_Channel2 = 5
    }
    public class LoadProfileData
    {
        private List<LoadProfileChannelInfo> channelsInfo;
        public uint ReadCount;

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
        private long channelGroupId;

        public int FixedObjectCount
        {
            get { return fixedObjCount; }
            set { fixedObjCount = value; }
        }

        public long ChannelGroupId
        {
            get { return channelGroupId; }
            set { channelGroupId = value; }
        }

        public bool CounterAvailable { get; set; }
        public bool IntervalAvailable { get; set; }
        public bool ClockAvailable { get; set; }
        public bool StatusWordAvailable { get; set; }

        public uint MaxCounter
        {
            get
            {
                uint maxLPCounter = 0;
                try
                {
                    foreach (var item in ChannelsInstances)
                    {
                        if (item != null && item.Counter > maxLPCounter)
                            maxLPCounter = item.Counter;
                    }
                }
                catch (Exception)
                {
                    maxLPCounter = 0;
                }
                return maxLPCounter;
            }
        }

        public uint MinCounter
        {
            get
            {
                uint t = uint.MaxValue;
                try
                {
                    foreach (var lpRecord in channelsInstances)
                    {
                        if (lpRecord != null && lpRecord.Counter < t)
                            t = lpRecord.Counter;
                    }
                }
                catch (Exception) { t = uint.MaxValue; }
                return (t == uint.MaxValue) ? 0 : t;
            }
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

        public void Add(LoadProfileData otherData)
        {
            if (otherData!=null)
            {
                this.ChannelsInstances.Add(otherData.ChannelsInstances[0]);
                this.CounterAvailable = otherData.CounterAvailable;
                this.IntervalAvailable = otherData.IntervalAvailable;
                this.ClockAvailable = otherData.ClockAvailable;
                this.StatusWordAvailable = otherData.StatusWordAvailable; 
            }

        }
    }

    public class LoadProfileItem
    {
        private List<double> loadProfileInstance;
        private DateTime date_timeStamp;
        private uint counter;
        private uint index;
        private uint interval;
        private string statusWord;

        public List<double> LoadProfileInstance
        {
            get { return loadProfileInstance; }
            set { loadProfileInstance = value; }
        }

        public DateTime DateTimeStamp
        {
            get { return date_timeStamp; }
            set { date_timeStamp = value; }
        }

        public uint Index
        {
            get { return index; }
            set { index = value; }
        }

        public uint Counter
        {
            get { return counter; }
            set { counter = value; }
        }

        public uint Interval
        {
            get { return interval; }
            set { interval = value; }
        }

        public string StatusWord
        {
            get { return statusWord; }
            set { statusWord = value; }
        }

        public LoadProfileItem()
        {
            loadProfileInstance = new List<double>();
            date_timeStamp = new DateTime();
            Counter = 0;
            StatusWord = string.Empty;
        }
    }
}
