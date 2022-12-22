using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SharedCode.Comm.Param
{
    [Serializable]
    [XmlInclude(typeof(Param_Load_Profile))]
    public class Param_Load_Profile
    {
        [XmlElement("TimingID", typeof(double))]
        public double TimingID;
        [XmlElement("Channel1ValueID", typeof(double))]
        public ushort Channel_1_Value_ID;
        [XmlElement("Channel2ValueID", typeof(double))]
        public ushort Channel_2_Value_ID;
        [XmlElement("Channel3ValueID", typeof(double))]
        public ushort Channel_3_Value_ID;
        [XmlElement("Channel4ValueID", typeof(double))]
        public ushort Channel_4_Value_ID;
    } 
}
