using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace SharedCode.Comm.DataContainer
{
    public class ScheduledRequest
    {
        //public Request Request_Obj;
        public string MSN;
        public Schedule_Struct schedule_Struct;
        private ushort _CNA_Count;
        public Process_Status ProcessStatus = Process_Status.Default;
        private Request_Status _Request_Status = Request_Status.Default;

        public Request_Status Request_Status
        {
            get { return GetRequestStatus(); }
            set { SetRequestStatus(value); }
        }

        //public TimeSpan VeryNextScheduleTime
        //{
        //    get
        //    {
        //        int SelectedIndex = 0;
        //        DateTime minSch = schedule_Struct[SelectedIndex].NextCallTime;
        //        int minInterval = schedule_Struct[SelectedIndex].interval;
        //        TimeSpan ToReturn = TimeSpan.FromMinutes(minInterval);
        //        try
        //        {
        //            for (int i = 1; i < schedule_Struct.Count; i++)
        //            {
        //                if (schedule_Struct[i].interval < minInterval)
        //                {
        //                    minSch = schedule_Struct[i].NextCallTime;
        //                    SelectedIndex = i;
        //                }
        //            }
        //            Schedule_Struct temp = schedule_Struct[SelectedIndex];
        //            temp.NextCallTime = GetNextCallTime(schedule_Struct[SelectedIndex].NextCallTime, schedule_Struct[SelectedIndex].interval);
        //            schedule_Struct[SelectedIndex] = temp;

        //            ToReturn = minSch.Subtract(DateTime.Now);
        //        }
        //        catch (Exception)
        //        {
        //            ToReturn = TimeSpan.FromMinutes(minInterval);
        //        }

        //        return ToReturn;
        //    }
        //}

        public DateTime GetNextCallTime(DateTime lastDueTime, long Interval)
        {
            DateTime nextCallDateTime = DateTime.MinValue;
            try
            {
                TimeSpan temp = DateTime.Now.Subtract(lastDueTime);
                int mod = (int)(temp.TotalMinutes % Interval);
                long qtnt = (long)(temp.TotalMinutes / Interval);
                nextCallDateTime = lastDueTime.AddMinutes((qtnt * Interval) + Interval);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return nextCallDateTime;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SetRequestStatus(Request_Status Status)
        {
            _Request_Status = Status;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Request_Status GetRequestStatus()
        {
            return _Request_Status;
        }

/*
        public ushort CNA_Count
        {
            get
            {
                return _CNA_Count;
            }
            set
            {
                _CNA_Count = value;
            }

        }

        public bool IsCNA_Max
        {
            get
            {
                return (CNA_Count >= Limits.Max_CNA_Count_Limit) ? true : false;
            }
        }

        public ScheduledRequest()
        {
            //Request_Obj = new Request();
            schedule_Struct = new Schedule_Struct();
            CNA_Count = 0;
            //isCNAUpdated = false;

        }
 * */
    }

    public class Quantities
    {
        #region DataMembers

        private int loadProfile;
        private int cummBilling;
        private int monthlyBilling;
        private int instantaneous;
        private int eventLog;

        #endregion

        public Quantities()
        {
            ///eventLog = new List<int>();
            eventLog = 0;
            loadProfile = 0;
            cummBilling = 0;
            monthlyBilling = 0;
            instantaneous = 0;
        }

        #region Properties
        public int LoadProfile
        {
            get { return loadProfile; }
            set { loadProfile = value; }
        }

        public int CommBilling
        {
            get { return cummBilling; }
            set { cummBilling = value; }
        }

        public int MonthlyBilling
        {
            get { return monthlyBilling; }
            set { monthlyBilling = value; }
        }

        public int Instantaneous
        {
            get { return instantaneous; }
            set { instantaneous = value; }
        }

        public int EventLog
        {
            get { return eventLog; }
            set { eventLog = value; }
        }
        #endregion
    }

    public class MDC_Status
    {
        #region Data_Members
        public static TimeSpan DefaultDurationPeriod = new TimeSpan(0, 30, 0);

        public DateTime SessionStart;
        public DateTime SessionStop;
        public TimeSpan _duration;
        public long KA_Expected_Transactions;
        public long KA_Successful_Transactions;
        public long KA_Connection_Count;
        public long KA_Pooling_Count;
        public DateTime KA_Last_Pooling_Time;
        public long NKA_Connection_Count;
        public long NKA_Expected_Transactions;
        public long NKA_Successful_Transactions;
        public long NKA_Pooling_Count;
        public DateTime NKA_Last_Pooling_Time;

        public bool mdc_status_sync = false;

        #endregion

        #region Property

        public TimeSpan Duration
        {
            get
            {
                var ab = DateTime.Now.Subtract(SessionStart).TotalMinutes;
                return _duration.Subtract(TimeSpan.FromMinutes(ab));
            }
            set
            {
                _duration = value;
            }
        }

        public bool IsSessionPeriodElapsed
        {
            get
            {
                try
                {
                    if (SessionStart.Add(this.Duration) <= DateTime.Now)
                        return true;
                    else
                        return false;

                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        #endregion

        #region Constructur

        /// <summary>
        /// Default Constructur
        /// </summary>
        public MDC_Status(TimeSpan duration)
        {
            Reset();
            //this.Duration = DefaultDurationPeriod;
            this.Duration = duration;

        }

        public void sync_mdcStatus()
        {
            int x = (int)SessionStart.TimeOfDay.TotalMinutes % (int)this.Duration.TotalMinutes;
            TimeSpan s = this.Duration.Add(TimeSpan.FromMinutes(-x));
            this.Duration = s;

        }
        /// <summary>
        /// Copy Constructur
        /// </summary>
        /// <param name="OtherOBj"></param>
        public MDC_Status(MDC_Status OtherOBj)
        {
            Reset();
            SessionStart = OtherOBj.SessionStart;
            SessionStop = OtherOBj.SessionStop;
            Duration = OtherOBj.Duration;
            KA_Expected_Transactions = OtherOBj.KA_Expected_Transactions;
            KA_Successful_Transactions = OtherOBj.KA_Successful_Transactions;
            KA_Connection_Count = OtherOBj.KA_Connection_Count;
            KA_Pooling_Count = OtherOBj.KA_Pooling_Count;
            KA_Last_Pooling_Time = OtherOBj.KA_Last_Pooling_Time;
            NKA_Connection_Count = OtherOBj.NKA_Connection_Count;
            NKA_Expected_Transactions = OtherOBj.NKA_Expected_Transactions;
            NKA_Successful_Transactions = OtherOBj.NKA_Successful_Transactions;
            NKA_Pooling_Count = OtherOBj.NKA_Pooling_Count;
            NKA_Last_Pooling_Time = OtherOBj.NKA_Last_Pooling_Time;
        }
        #endregion

        public void Reset()
        {
            SessionStart = DateTime.Now;
            SessionStop = DateTime.MinValue;
            KA_Connection_Count = 0;
            KA_Expected_Transactions = 0;
            KA_Last_Pooling_Time = DateTime.MinValue;
            KA_Pooling_Count = 0;
            KA_Successful_Transactions = 0;

            NKA_Connection_Count = 0;
            NKA_Expected_Transactions = 0;
            NKA_Last_Pooling_Time = DateTime.MinValue;
            NKA_Pooling_Count = 0;
            NKA_Successful_Transactions = 0;
        }

    }

    public class Stats
    {
        public int i = 0;
        public long MaxTrans;
        public double Max_duration;
        public double Min_duration;
        public double Avg_duration;
        public double STD_duration;
        public long Max_Meters_Allocated;
        public double Max_Meters_Duration;
    }

    public class MeterStatus
    {
        public int TBE1;
        public int TBE2;
        public List<int> OMA;
        public long TBE_GroupID;
        public bool IsTimeSync = true;
        public long ParamRequestID = 0;
        public long TBERequestID = 0;
        public MeterStatus()
        {
            TBE1 = 0;
            TBE2 = 0;
            OMA = new List<int>();
        }
    }

    public class LP_Params
    {
        public bool ChannelRequestLP1 = false;
        public bool ChangeIntervalRequestLP1 = false;

        public bool ChannelRequestLP2 = false;
        public bool ChangeIntervalRequestLP2 = false;

        public bool ChangeIntervalRequestLP3 = false;

        /// <summary>
        /// Test
        /// </summary>
        // public LPChannels()
        // {
        //     Channel_1 = 845524459061503;
        //     Channel_2 = 845524475838719;
        //     Channel_3 = 1408474412220671;
        //     Channel_4 = 845525952168191;
        //     LoadProfilePeriod = TimeSpan.FromMinutes(30);
        // }
    }


    public class MeterInfoUpdateFlags
    {
        public Process_Status ProcessStatus = Process_Status.Default;

        public bool UnsetMajorAlarms = false;
        public bool SetIpProfile = false;
        public bool SetKeepAlive = false;
        public bool SetModemInitializeBasics = false;
        public bool SetModemInitializeExtended = false;
        public bool TBE1WriteRequestID = false;
        public bool TBE2WriteRequestID = false;
        public bool MajorAlarmGroupID = false;
        public bool DW_NormalID = false;
        public bool DW_AlternateID = false;
        public bool LP1_ChannelsWriteRequest = false;
        public bool LP1_IntervalWriteRequest = false;

        public bool LP2_ChannelsWriteRequest = false;
        public bool LP2_IntervalWriteRequest = false;
        public bool LP3_IntervalWriteRequest = false;

        // public bool Read_PQ = false;
        // public bool Read_EV = false;
        // public bool Read_CB = false;
        // public bool Read_MB = false;
        // public bool Read_LP = false;
        // public bool Read_LP2 = false;
        // public bool Read_AR = false;
        // public bool Read_SS = false;

        public bool Schedule_PQ = false;
        // public bool Schedule_LP = false;
        // public bool Schedule_LP2 = false;
        public bool Schedule_EV = false;
        public bool Schedule_CB = false;
        public bool Schedule_MB = false;
        public bool Schedule_SS = false;
        public bool Schedule_CS = false;
        public bool Schedule_RG = false;
        public bool Schedule_CO = false;
        public bool SaveSchedule_PQ = false;

        public bool base_time_PQ = false;
        // public bool base_time_LP = false;
        // public bool base_time_LP2 = false;
        public bool base_time_EV = false;
        public bool base_time_CB = false;
        public bool base_time_MB = false;
        public bool base_time_SS = false;
        public bool base_time_CS = false;
        public bool base_time_RG = false;
        public bool base_time_CO = false;
        public bool base_time_Save_PQ = false;

        public bool KAS_DueTime = false;

        // public bool SuperImmediate_LP = false;
        // public bool SuperImmediate_LP2 = false;
        public bool SuperImmediate_EV = false;
        public bool SuperImmediate_CB = false;
        public bool SuperImmediate_MB = false;
        public bool SuperImmediate_SS = false;
        public bool SuperImmediate_CS = false;
        public bool SuperImmediate_PQ = false;
        public bool SuperImmediate_RG = false;
        public bool SuperImmediate_CO = false;
        public bool SuperImmediate_Save_PQ = false;

        public bool last_PQ_time = false;
        // public bool last_LP_time = false;
        // public bool last_LP2_time = false;
        public bool last_EV_time = false;
        public bool last_CB_time = false;
        public bool last_MB_time = false;
        public bool last_SS_time = false;
        public bool last_CS_time = false;
        public bool last_RG_time = false;
        public bool last_Save_PQ_time = false;

        public bool IsPasswordInvalid = false;//
        public bool IsPasswordTemporary = false;
        public bool IsDefaultPassWordActive = false;

        public bool AquireContactorLock = false;
        public bool IsContactorStatusUpdate = false;
        public bool NoContactorRequestPending = false;

        public bool IsMDIDateReset = false;
        public bool IsContactorParamsWrite = false;

        // Modem Limits Time
        public bool IsModemLimitsTimeWrite = false;

        // is customer reference no write
        public bool IsCReferenceWrite = false;
        // Is Number Profile Writes
        public bool IsNumberProFileWrite = false;
        public bool IsWakeUpProfileWrite = false;
        public bool IsDisplayPowerDownWrite = false;
        public bool ISDisableTBEOnPowerFailWrite = false;

        // public bool UpdateLoadProfileCounter = false;
        // public bool UpdateLoadProfile2Counter = false;
        public bool UpdateEventCounters = false;
        public bool UpdateMBCounter = false;

        // public bool IsDisableLP = false;
        // public bool IsDisableLP2 = false;
        public bool IsDisableEv = false;
        public bool IsDisableCS = false;
        public bool IsDisableMB = false;

        public bool IsLimitsWrite = false;
        public bool IsMonitoringTimeWrite = false;
        public bool IsCTPTWrite = false;
        public bool IsDecimalPointsWrite = false;
        public bool IsEnergyParamsWrite = false;
        public bool IsRemoteGridInputsWrite = false;
        public bool IsTotal_RetriesWrite = false;

        public bool IsLoadSheddingScheduleWrite = false;
        public bool IsConsumptionDataNowWrite = false;
        public bool IsConsumptionDataWeeklyWrite = false;
        public bool IsConsumptionDataMonthlyWrite = false;
        public bool IsOpticalPortAccessWrite = false;
    }
}
