using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS;

namespace Comm
{
    public class Load_Profile
    {
        public string MSN;
        public string[] channels = new string[4];
        public List<L_Data> loadData;

        public Load_Profile()
        {
            MSN = "";
            loadData = new List<L_Data>();
        }
    }

    public class L_Data
    {
        public DateTime timeStamp;
        public uint interval;
        public uint counter;
        public double[] value;
        public int channelID;

        public L_Data()
        {
            this.value = new double[4];
        }
    }

    public class LoadProfile_Live
    {
        public double Channel_1;
        public double Channel_2;
        public double Channel_3;
        public double Channel_4;
    }
}
