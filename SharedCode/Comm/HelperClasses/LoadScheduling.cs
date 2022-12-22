using DLMS.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SharedCode.Comm.HelperClasses
{
    [Serializable]
    [XmlInclude(typeof(LoadScheduling))]
    public class LoadScheduling
    {
        #region Properties
        public TimeSpan StartTime { get; set; }

        public int Interval { get; set; }
        
        public TimeSpan EndTime
        {
            get
            {
                return StartTime.Add(new TimeSpan(0, Interval, 0));
                //if (StartTime != null)
                //{
                //    StDateTime endTime = new StDateTime(StartTime);

                //    int extraHours = Interval / 60;
                //    int extraMins  = Interval % 60;

                //    endTime.Hour   = (byte)(StartTime.Hour   + extraHours);
                //    endTime.Minute = (byte)(StartTime.Minute + extraMins);

                //    return endTime;
                //}
                //return null;
            }
        }
        #endregion

        #region Constructor
        public LoadScheduling()
        {
            StartTime = new TimeSpan();
            Interval = 0;
        }

        public LoadScheduling(TimeSpan startTime,int interval)
        {
            StartTime = startTime;
            Interval = interval;
        }
        #endregion

    }
}
