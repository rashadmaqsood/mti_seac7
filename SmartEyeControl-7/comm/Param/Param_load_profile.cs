using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SmartEyeControl_7.comm
{
    [Serializable]
    [XmlInclude(typeof(Param_load_profile))]
    public class Param_load_profile
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
