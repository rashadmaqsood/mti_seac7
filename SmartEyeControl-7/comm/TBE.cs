using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS;
using System.Xml.Serialization;
using comm;
using DLMS.Comm;

namespace SmartEyeControl_7.comm
{
    [Serializable]
    [XmlInclude(typeof(TBE))]
    public class TBE:IParam
    {
        #region Data_Member

        private ushort tbe1_interval;
        private StDateTime tbe1_datetime;
        private byte controlEnum_Tbe1;
        private byte controlEnum_Tbe2;
        private ushort tbe2_interval;
        private StDateTime tbe2_datetime; 

        #endregion

        #region Properties
        
        [XmlElement("Tbe1Interval", typeof(ushort))]
        public ushort Tbe1_interval
        {
            get { return tbe1_interval; }
            set { tbe1_interval = value; }
        }

        [XmlElement("Tbe1Datetime", typeof(StDateTime))]
        public StDateTime Tbe1_datetime
        {
            get { return tbe1_datetime; }
            set { tbe1_datetime = value; }
        }

        [XmlElement("ControlEnumTbe1", typeof(byte))]
        public byte ControlEnum_Tbe1
        {
            get { return controlEnum_Tbe1; }
            set { controlEnum_Tbe1 = value; }
        }


        [XmlElement("Tbe2Interval", typeof(ushort))]
        public ushort Tbe2_interval
        {
            get { return tbe2_interval; }
            set { tbe2_interval = value; }
        }

        [XmlElement("Tbe2Datetime", typeof(StDateTime))]
        public StDateTime Tbe2_datetime
        {
            get { return tbe2_datetime; }
            set { tbe2_datetime = value; }
        }

        [XmlElement("ControlEnumTbe2", typeof(byte))]
        public byte ControlEnum_Tbe2
        {
            get { return controlEnum_Tbe2; }
            set { controlEnum_Tbe2 = value; }
        }
 
        #endregion

        public TBE()
        {
            this.Tbe1_datetime = new StDateTime();
            this.Tbe1_datetime.Year = StDateTime.NullYear;
            this.Tbe1_datetime.Month = StDateTime.Null;
            this.Tbe1_datetime.DayOfMonth = StDateTime.Null;
            this.Tbe1_datetime.DayOfWeek = StDateTime.Null;
            this.Tbe1_datetime.Hour = StDateTime.Null;
            this.Tbe1_datetime.Minute = StDateTime.Null;
            this.Tbe1_datetime.Second = StDateTime.Null;
            this.Tbe1_datetime.Hundred = StDateTime.Null;

            this.Tbe2_datetime = new StDateTime();
            this.Tbe2_datetime.Year = StDateTime.NullYear;
            this.Tbe2_datetime.Month = StDateTime.Null;
            this.Tbe2_datetime.DayOfMonth = StDateTime.Null;
            this.Tbe2_datetime.DayOfWeek = StDateTime.Null;
            this.Tbe2_datetime.Hour = StDateTime.Null;
            this.Tbe2_datetime.Minute = StDateTime.Null;
            this.Tbe2_datetime.Second = StDateTime.Null;
            this.Tbe2_datetime.Hundred = StDateTime.Null;
        }

        public string controlString(int a) //a=1;return TBE1 control enum else return TBE2 control enum
        {
            if (a == 1)
            {
                switch (this.ControlEnum_Tbe1)
                {
                    case 0:
                        return "Disabled";
                    case 1:
                        return "DateTime";
                    case 2:
                        return "Interval";
                    case 3:
                        return "Interval Time Sync";
                    case 4:
                        return "Fixed";
                    default:
                        return "Disabled";
                }
            }
            else
            {
                switch (this.ControlEnum_Tbe2)
                {
                    case 0:
                        return "Disabled";
                    case 1:
                        return "DateTime";
                    case 2:
                        return "Interval";
                    case 3:
                        return "Interval Time Sync";
                    case 4:
                        return "Fixed";
                    default:
                        return "Disabled";
                }
            }
        }
    }
}
