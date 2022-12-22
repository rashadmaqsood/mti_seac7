using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SharedCode.Comm.DataContainer
{
    public struct MA_Status
    {
        private bool TBE1;
        private List<int> OMA;
        private bool TBE2;

        #region Properties
        public bool TBE1_Property
        {
            get { return TBE1; }
            set { TBE1 = value; }
        }

        public bool TBE2_Property
        {
            get { return TBE2; }
            set { TBE2 = value; }
        }

        public List<int> OMA_Property
        {
            get { return OMA; }
            set { OMA = value; }
        }
        #endregion

        public MA_Status(bool tbe1, bool tbe2, List<int> list)//Constructor
        {
            this.TBE1 = tbe1;
            this.TBE2 = tbe2;
            this.OMA = list;
        }
    }
    public struct Schedule_Struct
    {
        public ushort DataRequestID;
        public int interval;
        public long schedule_id;
        public DateTime LastScheduleTime;
        public DateTime NextCallTime;
    }

    public class Profile_Counter
    {
        public uint MaxDifferenceCheck;
        public uint MinDifferenceCheck;
        public uint Meter_Counter;
        public uint DB_Counter;
        public uint Max_Size;
        public uint ChunkSize;
        public TimeSpan Period;
        private DateTime lastReadTime;
        public uint GroupId;
        public ushort InvalidUpdate;
        public bool ReadInstant = false;
        public bool UpdateInstantFlag = false;
        public bool UpdateCounter = false;
        public uint CounterToUpdate = 0;
        public DateTime ToTime { get; set; }
        public DateTime LastReadTime 
        {
            get 
            {
                //if (lastReadTime < MaxEntriesTime)
                //    return MaxEntriesTime;
                //else 
                    return lastReadTime;
            } 
            set
            {
                lastReadTime = value;
            }
        }

        public DateTime MaxEntriesTime 
        {
            get
            {
                return DateTime.Now.AddMinutes(Period.TotalMinutes * Max_Size * -1);
            }
        }

        public DateTime InstantReadTime
        {
            get
            {
                return DateTime.Now.AddMinutes(Period.TotalMinutes * ChunkSize * -1);
            }
        }
        public Profile_Counter()
        {
            Meter_Counter = 0;
            DB_Counter = 0;
            Max_Size = 0;
            GroupId = 0;
            LastReadTime = DateTime.MinValue;
            Period = new TimeSpan();
            ToTime = DateTime.MinValue;
            ChunkSize = 0;
            MaxDifferenceCheck = MinDifferenceCheck = 0;
        }

        public bool IsCounterValid 
        {
            get
            {
                return !(DB_Counter > Meter_Counter || (Meter_Counter - DB_Counter) > MaxDifferenceCheck);
            }
        }

        public bool IsLowCounter
        {
            get
            {
                return DB_Counter > Meter_Counter;
            }
        }

        public bool IsHighCounter
        {
            get
            {
                return (Meter_Counter - DB_Counter) > MaxDifferenceCheck;
            }
        }
        public bool IsEqual
        {
            get
            {
                return Meter_Counter == DB_Counter;
            }
        }
        public int Difference
        {
            get
            {
                return (int)(Meter_Counter - DB_Counter);
            }
        }

        public bool IsUptoDate
        {
            get
            {
                return Difference == 0 && Meter_Counter>0;
            }
        }

        public bool IsReadable
        {
            get
            {
                return !IsUptoDate && Difference > 0;
            }
        }
        public bool IsLessThanMinDifference
        {
            get
            {
                return  Difference <MinDifferenceCheck;
            }
        }
    }

    public struct MDC_Log
    {
        public string msn;
        public DateTime session_dt;
        public string log_string;
        public bool IsException;
        public TimeSpan ConnectionLife;
        public long SentBytes;
        public long ReceiveBytes;
    }

}
