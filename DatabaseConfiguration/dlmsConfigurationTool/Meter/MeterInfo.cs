using dlmsConfigurationTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comm.DataContainers
{
    public class MeterInformation : IDisposable
    {
        #region Data Members

        public char[] EventsToSavePQ;
        public string EventsHexString;
        public uint LogLevel = 10;
        public Process_Status ProcessStatus = Process_Status.Default;
        public bool ReadEventsForcibly = false;
        public bool SavePQForcibly = false;
        public bool MustLogoutMeter = false;
        public bool IsLiveUpdated = false;
        public bool IsMeterParameterized = false;
        public bool IsAnyAlarmTriggered = false;
        public bool IsTriggeredAnyOtherThanTBEs = false;
        public MeterType MeterType_OBJ;
        public string MSN;
        public string Password;
        public uint ModelID = 0;
        public bool Status = false;
        //  public bool save_packet_log = false;

        public bool Unset_MajorAlarms = false;
        public long SetIPProfile = 0;
        public long SetKeepAlive = 0;
        public long SetModemInitializeBasics = 0;
        public long SetModemInitializeExtended = 0;
        public long TBE1WriteRequestID = 0;
        public long TBE2WriteRequestID = 0;
        public long MajorAlarmGroupID = 0;
        public LPChannels LPChannelsWriteRequest;

        public char[] eventsForLiveUpdate_individual;
        public string eventsForLiveUpdate_individual_string;

        public char[] eventsForLiveUpdate_logbook;
        public string eventsForLiveUpdate_logbook_string;

        public char[] individual_events_array;
        public string individual_events_string_sch;
        public bool read_logbook;
        public bool read_individual_events_sch;

        public string individual_events_string_alarm;

        public uint DW_NormalID = 0;//Dispaly windows normal
        public uint DW_AlternateID = 0;//display windows alternate
        public uint DW_ScrollTime = 15;

        public uint DW_Normal_format = 1;  //1 Means Number format, 2 means OBIS format
        public uint DW_Alternate_format = 1;

        public int DetailedBillingID = 0;
        public bool EnableLiveUpdate = false;
        public bool ReadEventsOnMajorAlarms = false;
        public bool EnableEchoLog = true;
        public bool EnableSaveLog = true;
        public bool SaveLifeTime = false;
        public bool ReadMBOnMDIReset = false;
        public bool isPrepaid = false;
        public bool prepaid_request_exist = false;

        public bool Read_PQ = false;
        public bool Read_EV = false;
        public bool Read_CB = false;
        public bool Read_MB = false;
        public short Read_LP = 0;
        public short Read_LP2 = 0;
        public bool Read_LPPQ = false;
        public bool Read_AR = false;
        public bool Read_SS = false;

        public bool Save_PQ = false;
        public bool Save_EV = false;
        public bool Save_CB = false;
        public bool Save_MB = false;
        public bool Save_LP = false;
        public bool Save_LP2 = false;
        public bool Save_LPPQ = false;
        public bool Save_AR = false;
        public bool Save_ST = false;


        public uint lp_chunk_size = 0;

        public byte lp_invalid_update = 0;
        public byte ev_invalid_update = 0;
        public byte mb_invalid_update = 0;

        public bool logoutMeter = false;

        public TimeSpan DefaultResetSessionDuration;

        public Schedule Schedule_PQ;
        public Schedule Schedule_LP;
        public Schedule Schedule_LP2;
        public Schedule Schedule_LPPQ;
        public Schedule Schedule_EV;
        public Schedule Schedule_CB;
        public Schedule Schedule_MB;
        public Schedule Schedule_SS;
        public Schedule Schedule_CS;
        public Schedule SaveSchedule_PQ;

        public Counters Counter_Obj;
        public TimeSpan Kas_Interval;
        public DateTime Kas_DueTime;
        public DateTime Kas_NextCallTime;

        //-----------------------------------------------------------------------------//
        //                          Modification by Furqan                             //
        //                          Date: 12-November-2014                             //
        //                          Version: 3.0.0.143                                 //
        //-----------------------------------------------------------------------------//
        //============================================================================== 

        public string   Reference_no;
        public string   Meter_sim_no;
        public string   Wakeup_password;
        public string   Wakeup_no1;
        public string   Wakeup_no2;
        public string   Wakeup_no3;
        public string   Wakeup_no4;
        public long     Contactor_param_id = 0;
        public bool     Write_contactor_param = false;
        public string   New_meter_password;
        public bool     Write_password_flag = false;
        public DateTime New_password_activation_time;
        public int      Current_contactor_status = -1;
        //-----------------------------------------------------------------------------//
        //                          Modification by Furqan                             //
        //                          Date: 14-November-2014                             //
        //                          Version: 3.0.0.143                                 //
        //-----------------------------------------------------------------------------//
        //============================================================================== 
        // add new fields below
        //============================================================================== 
        public Int32 Min_cs_difference;
        public Int32 Max_cs_difference;
        public int      New_contactor_satate;
        public bool     Apply_new_contactor_state = false;
        public byte     Mdi_reset_date_time;
        public bool     Write_mdi_reset_date_time = false;

        public string   Default_Password;
        public DateTime Last_Password_update_time;

        //11-25-2014 Two new fields added by furqan
        public bool Write_modem_limits_time = false;
        public long Modem_limits_time_param_id = 0; 

        //11/26/2014 new field added by furqan 
        public bool Write_reference_no = false;
        //==============================================================================

        #endregion

        #region Properties

        public bool IsParamEmpty
        {
            get
            {
                // add write_password_flag , Write_mdi_reset_date_time , write_contactor_param, Write_modem_limits_time, write_reference_no
                return !LPChannelsWriteRequest.ChannelRequest && !Write_reference_no && !Write_modem_limits_time && !Write_contactor_param && !Write_mdi_reset_date_time && !Write_password_flag && !LPChannelsWriteRequest.ChangeIntervalRequest && !Schedule_CS.IsSuperImmediate && !Unset_MajorAlarms && (Schedule_CS.SchType == ScheduleType.Disabled || !IsScheduleReadyToBeProcess(Schedule_CS)) && TBE1WriteRequestID <= 0 && TBE2WriteRequestID <= 0 && SetIPProfile <= 0 && SetKeepAlive <= 0 && SetModemInitializeBasics <= 0 && SetModemInitializeExtended <= 0 && MajorAlarmGroupID <= 0 && DW_NormalID <= 0 && DW_AlternateID <= 0;
            }
        }

        public bool IsDataRequestEmpty
        {
            get
            {
                return Schedule_CB.SchType == ScheduleType.Disabled && Schedule_EV.SchType == ScheduleType.Disabled && Schedule_LP.SchType == ScheduleType.Disabled && Schedule_MB.SchType == ScheduleType.Disabled && Schedule_PQ.SchType == ScheduleType.Disabled && Schedule_SS.SchType == ScheduleType.Disabled && !Schedule_CB.IsSuperImmediate && !Schedule_EV.IsSuperImmediate && !Schedule_LP.IsSuperImmediate && !Schedule_MB.IsSuperImmediate && !Schedule_PQ.IsSuperImmediate && !Schedule_SS.IsSuperImmediate;
            }
        }

        #endregion

        public MeterInformation()
        {
            MSN = String.Empty;
            Counter_Obj = new Counters();
            LPChannelsWriteRequest = new LPChannels();
            Schedule_PQ = new Schedule();
            Schedule_LP = new Schedule();
            Schedule_LP2 = new Schedule();
            Schedule_LPPQ = new Schedule();
            Schedule_EV = new Schedule();
            Schedule_CB = new Schedule();
            Schedule_MB = new Schedule();
            Schedule_SS = new Schedule();
            Schedule_CS = new Schedule();
            SaveSchedule_PQ = new Schedule();

        }

        #region Member Functions
        
        public void PreUpdateSchedule(Schedule Param_Schedule, DateTime SessionDateTime)
        {
            Param_Schedule.LastReadTime = SessionDateTime;
            if (Param_Schedule.SchType == ScheduleType.Immediate || Param_Schedule.SchType == ScheduleType.SpecifiedDateTime)
                Param_Schedule.SchType = ScheduleType.Disabled;
            else if (Param_Schedule.SchType == ScheduleType.IntervalFixed)
                Param_Schedule.BaseDateTime = GetNextCallTimeForFixedInterval(Param_Schedule.BaseDateTime, Param_Schedule.Interval.TotalMinutes);
            else if (Param_Schedule.SchType == ScheduleType.IntervalRandom)
                Param_Schedule.BaseDateTime = SessionDateTime.Add(Param_Schedule.Interval);
        }

        public bool IsScheduleReadyToBeProcess(Schedule Param_Schedule)
        {
            MustLogoutMeter = true;
            if (Param_Schedule.SchType == ScheduleType.Immediate || Param_Schedule.SchType == ScheduleType.EveryTime)
                return true;
            else if (Param_Schedule.SchType == ScheduleType.IntervalFixed || Param_Schedule.SchType == ScheduleType.IntervalRandom || Param_Schedule.SchType == ScheduleType.SpecifiedDateTime)
            {
                if (Param_Schedule.BaseDateTime <= DateTime.Now)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public DateTime GetNextCallTimeForFixedInterval(DateTime BaseTime, double IntervalInMinutes)
        {
            DateTime nextCallDateTime = DateTime.MinValue;
            try
            {
                TimeSpan temp = DateTime.Now.Subtract(BaseTime);

                //long qtnt = (long)(temp.TotalMinutes / IntervalInMinutes);
                //nextCallDateTime = BaseTime.AddMinutes((qtnt * IntervalInMinutes) + IntervalInMinutes);

                double qtnt = Math.Floor(temp.TotalMinutes / IntervalInMinutes);


                nextCallDateTime = BaseTime.AddMinutes((qtnt + 1) * IntervalInMinutes);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return nextCallDateTime;
        }

        #endregion

        #region IDisposeable Member

        public void Dispose()
        {
            Schedule_PQ = null;
            Schedule_LP = null;
            Schedule_LP2 = null;
            Schedule_LPPQ = null;
            Schedule_EV = null;
            Schedule_CB = null;
            Schedule_MB = null;
            Schedule_SS = null;
            Schedule_CS = null;
            SaveSchedule_PQ = null;
        }

        #endregion

        ~MeterInformation()
        {
            try
            {
                Dispose();
            }
            catch
            { }
        }
    }

    public class MeterInformation_Sub
    {
        #region Data Members

        public MeterType MeterType_OBJ;

        public string MSN;
        
        public bool read_logbook;
        
        public short Read_LP = 0;
        
        public bool Save_LP = false;

        public uint lp_chunk_size = 0;

        public byte lp_invalid_update = 0;
        public byte ev_invalid_update = 0;
        public byte mb_invalid_update = 0;

        public Schedule Schedule_LP;

        public Counters Counter_Obj;
        
        #endregion

        #region Properties
        
        public bool IsDataRequestEmpty
        {
            get
            {
                return  Schedule_LP.SchType == ScheduleType.Disabled  && !Schedule_LP.IsSuperImmediate;
            }
        }

        #endregion

        public MeterInformation_Sub()
        {
            MSN = String.Empty;
            Counter_Obj = new Counters();
            Schedule_LP = new Schedule();
        }

        #region Member Functions

        public void PreUpdateSchedule(Schedule Param_Schedule, DateTime SessionDateTime)
        {
            Param_Schedule.LastReadTime = SessionDateTime;
            if (Param_Schedule.SchType == ScheduleType.Immediate || Param_Schedule.SchType == ScheduleType.SpecifiedDateTime)
                Param_Schedule.SchType = ScheduleType.Disabled;
            else if (Param_Schedule.SchType == ScheduleType.IntervalFixed)
                Param_Schedule.BaseDateTime = GetNextCallTimeForFixedInterval(Param_Schedule.BaseDateTime, Param_Schedule.Interval.TotalMinutes);
            else if (Param_Schedule.SchType == ScheduleType.IntervalRandom)
                Param_Schedule.BaseDateTime = SessionDateTime.Add(Param_Schedule.Interval);
        }

        public bool IsScheduleReadyToBeProcess(Schedule Param_Schedule)
        {
            if (Param_Schedule.SchType == ScheduleType.Immediate || Param_Schedule.SchType == ScheduleType.EveryTime)
                return true;
            else if (Param_Schedule.SchType == ScheduleType.IntervalFixed ||
                Param_Schedule.SchType == ScheduleType.IntervalRandom ||
                Param_Schedule.SchType == ScheduleType.SpecifiedDateTime)
            {
                if (Param_Schedule.BaseDateTime <= DateTime.Now)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public DateTime GetNextCallTimeForFixedInterval(DateTime BaseTime, double IntervalInMinutes)
        {
            DateTime nextCallDateTime = DateTime.MinValue;
            try
            {
                TimeSpan temp = DateTime.Now.Subtract(BaseTime);

                //long qtnt = (long)(temp.TotalMinutes / IntervalInMinutes);
                //nextCallDateTime = BaseTime.AddMinutes((qtnt * IntervalInMinutes) + IntervalInMinutes);

                double qtnt = Math.Floor(temp.TotalMinutes / IntervalInMinutes);

                nextCallDateTime = BaseTime.AddMinutes((qtnt + 1) * IntervalInMinutes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return nextCallDateTime;
        }

        #endregion
    }
    public enum MeterType
    {
        NonKeepAlive = 0,
        KeepAlive = 1,
        Intruder = 2
    };
}