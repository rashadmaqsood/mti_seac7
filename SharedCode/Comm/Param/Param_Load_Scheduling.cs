using DLMS;
using DLMS.Comm;
using SharedCode.Comm.HelperClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SharedCode.Comm.Param
{

    [Serializable]
    [XmlInclude(typeof(Param_Load_Scheduling))]
    public class Param_Load_Scheduling : ISerializable, IParam, ICloneable
    {
        private const int _MAX_ENTRIES = 12;
        private List<ScheduleEntry> _listOfEntries;
        private List<LoadScheduling> _listLoadScheduling;

        #region Properties
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<LoadScheduling> ListLoadScheduling
        {
            get
            {
                return _listLoadScheduling;
            }
        }
        public List<ScheduleEntry> ListOfEntries
        {
            get
            {
                _listOfEntries = new List<ScheduleEntry>();
                for (int i = 0; i < _listLoadScheduling.Count; i++)
                {
                    AddEntryWithSelector(false, _listLoadScheduling[i].StartTime, StartDate, EndDate);
                    AddEntryWithSelector(true, _listLoadScheduling[i].EndTime, StartDate, EndDate);
                }
                return _listOfEntries;
            }
            set
            {
                List<ScheduleEntry> localListOfEntries = value;
                if(localListOfEntries.Count>0)
                {
                    StartDate = localListOfEntries[0].BeginDate.GetDate();
                    EndDate = localListOfEntries[0].EndDate.GetDate();
                }
                if (localListOfEntries.Count % 2 == 0)
                {
                    _listLoadScheduling = new List<LoadScheduling>();
                    for (int i = 0; i < localListOfEntries.Count; i = i + 2)
                    {
                        ScheduleEntry startEntry = new ScheduleEntry(localListOfEntries[i]);
                        ScheduleEntry endEntry   = new ScheduleEntry(localListOfEntries[i + 1]);

                        if (startEntry.ScriptSelector == 0x01 && endEntry.ScriptSelector == 0x02)
                        {
                            TimeSpan time = new TimeSpan(startEntry.SwitchTime.Hour, startEntry.SwitchTime.Minute,0);
                            int interval = FindInterval(startEntry.SwitchTime, endEntry.SwitchTime);
                            _listLoadScheduling.Add(new LoadScheduling(time, interval));
                        }
                        else
                            throw new ArgumentException("List Entries Script SElector are Inconsistent. Param_Load_Scheduling");
                    }
                }
                else
                {
                    throw new ArgumentException("List of Entries is Inconsistent (i.e., Odd Numbers ). Param_Load_Scheduling");
                }
            }
        }
        #endregion

        #region Constuctor

        public Param_Load_Scheduling()
        {
            _listLoadScheduling = new List<LoadScheduling>();
            _listOfEntries = new List<ScheduleEntry>();

        }
        #endregion

        #region Member Methods
        public int Add(LoadScheduling newEntry)
        {
            int overlapIndex = IsOverlapTime(newEntry);

            if ( overlapIndex == -1) // No Overlap Occurs (Correct Entry)
            {
                if (_listLoadScheduling.Count < _MAX_ENTRIES)
                {
                    _listLoadScheduling.Add(newEntry);
                    _listLoadScheduling = _listLoadScheduling.OrderBy(o => o.StartTime).ToList();
                    return -1; // true;
                }
                else
                    throw new IndexOutOfRangeException( string.Format("Max '{0}' Entries Allowed!" , _MAX_ENTRIES));
            }
            else // Overlap Occures At
            {
                return overlapIndex;  // Overlapped
            }
            //return false;
        }

        public void Remove(int index)
        {
            _listLoadScheduling.RemoveAt(index);

            _listLoadScheduling = _listLoadScheduling.OrderBy(o => o.StartTime).ToList();
        }

        public void Clear()
        {
            _listLoadScheduling.Clear();
        }

        public int IsOverlapTime(LoadScheduling newEntry)
        {
            if (_listLoadScheduling.Count == 0)
            {
                return -1 ; //false   // no need for overlap's checks
            }
            else //if (dgView.Rows.Count > 0)
            {
                int index = 0;
                foreach (var currentEntry in _listLoadScheduling)
                {
                    if ((newEntry.StartTime < currentEntry.EndTime 
                        && newEntry.EndTime > currentEntry.StartTime) || (newEntry.StartTime == currentEntry.StartTime) )
                    {
                        return index; //true   // Overlapped
                    }
                    index++;
                }
                return -1; // false;
            }
        } // end isOverlapTime()
        
        private void AddEntryWithSelector(bool selector, TimeSpan switchTime, DateTime beginDate, DateTime endDate)
        {
            BitArray ExecWeekDays = new BitArray(ScheduleEntry.WeekDaysCount, true);
            BitArray ExecSpecialDays = new BitArray(0);

            var time = new StDateTime();
            time.SetTime(switchTime);

            var beginstDate = new StDateTime();
            beginstDate.SetDate(beginDate);

            var endstDate = new StDateTime();
            endstDate.SetDate(endDate);


            ScheduleEntry obj = new ScheduleEntry()
            {
                Index = ((ushort)(_listOfEntries.Count + 1)),
                LogicalName = Get_Index.Contactor_Script_Table,
                ExecWeekdays = ExecWeekDays,
                ExecSpecialDays = ExecSpecialDays,
                ScriptSelector = selector ? (byte)2 : (byte)1,
                SwitchTime = time,
                BeginDate = beginstDate,
                EndDate = endstDate
            };

            //TODO: "yahan NOT_SPECIFIED wala data set karna hy"
            //obj.BeginDate.Year = StDateTime.NullYear;
            //obj.BeginDate.DayOfWeek = StDateTime.Null;
            
            //obj.EndDate.Year = StDateTime.NullYear;
            //obj.EndDate.DayOfWeek = StDateTime.Null;

            this._listOfEntries.Add(obj);

        }

        //for timepicker interval
        private int FindInterval(StDateTime startTime, StDateTime endTime)
        {
            if (startTime != null && endTime != null)
            {
                TimeSpan start_time = new TimeSpan(startTime.Hour, startTime.Minute, 0);
                TimeSpan end_time   = new TimeSpan(endTime.Hour, endTime.Minute, 0);

                TimeSpan diff_time;

                if (end_time > start_time)  // normal Case
                {
                    diff_time = end_time.Subtract(start_time);
                }
                else // time upper 24 hours case
                {
                    int hours   = (23 - start_time.Hours)   + end_time.Hours;
                    int minutes = (60 - start_time.Minutes) + end_time.Minutes;

                    diff_time = new TimeSpan(hours, minutes, 0);
                }

                int real_Interval = Math.Abs(Convert.ToInt32(diff_time.TotalMinutes));

                return real_Interval;
            }

           //if (startTime != null && endTime != null)
           //{
           //    int startHour = (startTime.Hour == 0x00)? 24 : startTime.Hour,
           //        startMint = startTime.Minute;
           //    int endHour = (endTime.Hour == 0x00) ? 24 : endTime.Hour,
           //        endMint = endTime.Minute; 
           //    if(startHour != 24 || endHour != 24)
           //    {
           //        startHour = startTime.Hour;
           //        endHour   = endTime.Hour;
           //    } 
           //    int hourDiff = Math.Abs(endHour - startHour),  // make absolute Hours
           //        mintDiff = Math.Abs(endMint - startMint);  // make absolute Minutes 
           //    int real_Interval = 0; 
           //    if ((startHour == 24 && endHour == 24) && (endMint < startMint))
           //        real_Interval = (1440 - Math.Abs(startMint - endMint) );
           //    else
           //        real_Interval = (hourDiff * 60) + (mintDiff); 
           //    return real_Interval;
           //}
            
            return 0;
        }

        // for combobox interval
        //private int FindInterval(StDateTime startTime, StDateTime endTime)
        //{
        //    if (startTime != null && endTime != null)
        //    {
        //        int startHour = startTime.Hour,
        //            startMint = startTime.Minute;

        //        int endHour = endTime.Hour,
        //            endMint = endTime.Minute;

        //        int hourDiff = Math.Abs(endHour - startHour),  // make absolute Hours
        //            mintDiff = Math.Abs(endMint - startMint);  // make absolute Minutes

        //        int real_Interval = (hourDiff * 60) + (mintDiff);

        //        return real_Interval;
        //    }
        //    return 0;
        //}

        #endregion

        #region ISerializable interface Members


        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public Param_Load_Scheduling(SerializationInfo info, StreamingContext context)
        {
            _listLoadScheduling = (List<LoadScheduling>)info.GetValue("ListLoadScheduling", typeof(List<LoadScheduling>));
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            try
            {
                ///Adding StartDate Type DateTime
                info.AddValue("StartDate", (this.StartDate != null) ? this.StartDate : DateTime.Now , typeof(DateTime));
                ///Adding EndDate Type DateTime
                info.AddValue("EndDate", this.EndDate, typeof(DateTime));
                ///Adding ListLoadScheduling Type List<LoadScheduling>
                info.AddValue("ListLoadScheduling", this.ListLoadScheduling, typeof(List<LoadScheduling>));
                
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ICloneable interface Members
        public object Clone()
        {
            MemoryStream memStream = null;
            Object dp = null;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (memStream = new MemoryStream(50))
                {
                    formatter.Serialize(memStream, this);
                    memStream.Seek(0, SeekOrigin.Begin);
                    dp = formatter.Deserialize(memStream);
                }
                return dp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Clone Param_Load_Scheduling ", ex);
            }
        }
        #endregion
    }
}
