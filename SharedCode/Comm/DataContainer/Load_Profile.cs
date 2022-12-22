using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS;

namespace SharedCode.Comm.DataContainer
{
    public class Load_Profile
    {
        public string MSN;
        //public string reference_no;
        public List<string> channels ;
        public List<L_Data> loadData;

        public Load_Profile()
        {
            MSN = "";
            loadData = new List<L_Data>();
            channels = new List<string>();
        }
    }

    public class L_Data
    {
        public DateTime timeStamp;
        public uint interval;
        public uint counter;
        public List<double> value;
        public int channelID;
        public string statusWord;
        public L_Data()
        {
            this.value = new List<double>();
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
