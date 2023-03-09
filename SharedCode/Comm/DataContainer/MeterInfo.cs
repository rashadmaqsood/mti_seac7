using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Net;
using SharedCode.Comm.DataContainer;
using DLMS.Comm;
using DLMS;
using SharedCode.Common;

namespace SharedCode.Comm.DataContainer
{
    public enum READ_METHOD
    {
        Disabled = 0,
        ByCounter = 1,
        ByDateTime = 2
    }

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
        public DeviceType DeviceTypeVal = DeviceType.Not_Assigned;
        public LLCProtocolType LLC_Protocol_Type = LLCProtocolType.Not_Assigned;
        public Scheduler_Type Scheduler_Type = Scheduler_Type.Not_Assigned;

        public string MSN;
        public string GlobalDeviceId;
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
        public string MajorAlarmsString = string.Empty;
        public LP_Params LPParamRequest;

        public char[] eventsForLiveUpdate_individual;
        public string eventsForLiveUpdate_individual_string;

        public char[] eventsForLiveUpdate_logbook;
        public string eventsForLiveUpdate_logbook_string;

        public char[] individual_events_array;
        public string individual_events_string_sch;
        public bool read_logbook;
        public bool read_individual_events_sch;

        public string individual_events_string_alarm;

        public uint DW_NormalID = 0;    // Display windows normal
        public uint DW_AlternateID = 0; // display windows alternate
        public uint DW_ScrollTime = 15;

        public uint DW_Normal_format = 1;  // 1 Means Number format, 2 means OBIS format
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

        public bool Update_Alarm_Register_Live = false;
        public bool SendAlarmsResponse = false;

        public bool Read_PQ = false;
        public bool Read_EV = false;
        public READ_METHOD Read_CB = READ_METHOD.Disabled;
        public READ_METHOD Read_MB = READ_METHOD.Disabled;
        public READ_METHOD Read_LP = READ_METHOD.Disabled;
        public READ_METHOD Read_LP2 = READ_METHOD.Disabled;
        public READ_METHOD Read_LP3 = READ_METHOD.Disabled;
        bool _readAR = false;

        public bool Read_AR
        {
            get
            {
                return _readAR && !PrioritizeWakeup;
            }
            set
            {
                _readAR = value;
            }
        }

        public bool Read_SS = false;
        public bool Read_CS = false;
        public bool Read_RG = false;
        public bool Read_CO = false;
        // public bool Instant_LP_Read = false;
        // public bool Instant_LP2_Read = false;
        // public bool Instant_LP3_Read = false;
        public bool Save_PQ = false;
        public bool Save_EV = false;
        public bool Save_CB = false;
        public bool Save_MB = false;
        public bool Save_LP = false;
        public bool Save_LP2 = false;
        public bool Save_LP3 = false;
        public bool Save_AR = false;
        public bool Save_ST = false;
        public bool Save_Events_On_Major_Alarm = false;

        //public uint lp_chunk_size = 0;
        //public uint lp2_chunk_size = 0;

        public bool logoutMeter = false;

        public TimeSpan DefaultResetSessionDuration;

        public Schedule Schedule_PQ;
        public Schedule Schedule_LP;
        public Schedule Schedule_LP2;
        public Schedule Schedule_LP3;
        public Schedule Schedule_EV;
        public Schedule Schedule_CB;
        public Schedule Schedule_MB;
        public Schedule Schedule_SS;
        public Schedule Schedule_CS;
        public Schedule Schedule_RG;
        public Schedule SaveSchedule_PQ;
        public Schedule Schedule_CO;

        // public Counters Counter_Obj;
        public Profile_Counter LP_Counters;
        public Profile_Counter LP2_Counters;
        public Profile_Counter LP3_Counters;
        public Profile_Counter EV_Counters;
        public Profile_Counter MB_Counters;

        public TimeSpan Kas_Interval;
        public DateTime Kas_DueTime;
        public DateTime Kas_NextCallTime;

        public string Reference_no;
        public string Meter_sim_no;
        public string Wakeup_password;
        public string Wakeup_no1;
        public string Wakeup_no2;
        public string Wakeup_no3;
        public string Wakeup_no4;
        public long Contactor_param_id = 0;
        public bool Write_contactor_param = false;
        public string New_meter_password;
        public bool Write_password_flag = false;
        public DateTime New_password_activation_time;
        public int Current_contactor_status = -1;
        public Int32 Min_cs_difference;
        public Int32 Max_cs_difference;

        public int New_contactor_satate;
        public bool Apply_new_contactor_state = false;
        public int Contactor_lock = 0;

        public byte Mdi_reset_date_time;
        public TimeSpan Mdi_reset_time;
        public bool Write_mdi_reset_date_time = false;

        public string Default_Password;
        public DateTime Last_Password_update_time;

        //11-25-2014 Two new fields added by furqan
        public bool Write_modem_limits_time = false;
        public long Modem_limits_time_param_id = 0;

        //11/26/2014 new field added by furqan 
        public bool Write_reference_no = false;

        //12/02/2014 new Field Added for Number_profile_params if > 0 write params else no

        public Int32 Write_Number_Profile = 0;
        public long Write_Display_PowerDown_param = 0;

        public byte TBEPowerFailParamOptions;

        public int WakeUp_Profile_Id = 0;
        private bool _prioritizeWakeUp = false;

        public bool PrioritizeWakeup
        {
            get
            {
                return _prioritizeWakeUp && WakeUp_Request_ID != 0;
            }
            set
            {
                _prioritizeWakeUp = value;
            }
        }

        // public long LPCounterToUpdate = 0;
        // public long LP2CounterToUpdate = 0;

        public long EvCounterToUpdate = 0;
        public long MonthlyBillingCounterToUpdate = 0;

        // public int LPInValidUpdate = 0;
        // public int LP2InValidUpdate = 0;
        // public int EvInvalidUpdate = 0;
        // public int MBInvalidUpdate = 0;

        public bool ReadDebugError = false;
        public long MeterID = 0;
        public long WakeUp_Request_ID = 0;

        public int _ct_num = 0;
        public int _ct_denum = 0;
        public int _pt_num = 0;
        public int _pt_denum = 0;
        
        public long Limits_Param_Id = 0;
        public long MonitoringTime_Param_Id = 0;
        public long CP_PT_Param_Id = 0;
        public long DecimalPoints_Param_Id = 0;
        public long Energy_Param_Id = 0;

        public string Customer_ID = null;
        // public DateTime LastContactorStatusChangeTime;
        public bool IsContactorSupported = false;
        public List<Schedules> ReadPlan;
        public List<ParamList> ParamsToRead;
        public bool ReadParams = false;
        public string Contactor_Priority_Sequence = string.Empty;
        public int Association_Id = 0;

        public Security_Data ObjSecurityData = null;

        public bool DDS110_Compatible = false;
        public List<int> MajorAlarmsProcessedInSession = null;
        public byte CommunicationTypeID = 0;
        public byte BillingMethodId = 1;
        // public byte MaxBillingMonths = 24;
        public byte ClockSyncronizationMethod = 0;
        public bool StandardParameter = false;

        // MAX_SessionTime
        public static readonly TimeSpan MaxCurrentSessionTime = TimeSpan.FromMinutes(60d);
        //==============================================================================
        public static bool Validate_MSN = true;
        public ushort HDLC_Address = 17;

        public uint total_tries = 0;
        public uint total_success = 0;

        public bool SubMeterProcessedByGateway = false;

        public int Load_Shedding_Schedule_Id = -1 ;
        public bool Write_Load_Shedding_Schedule = false;

        public bool Write_Consumption_Data_Now = false;
        public bool Write_Consumption_Data_Weekly = false;
        public bool Write_Consumption_Data_Monthly = false;

        public DateTime OpticalPortStartTime = DateTime.MinValue;
        public DateTime OpticalPortEndTime = DateTime.MinValue;
        public bool UpdateOpticalPortAccess = false;
        #endregion

        #region Properties

        public bool OnDemandRequest 
        {
            get
            {
                return Schedule_CB.IsSuperImmediate ||
                    Schedule_CO.IsSuperImmediate ||
                    Schedule_CS.IsSuperImmediate ||
                    Schedule_EV.IsSuperImmediate ||
                    Schedule_LP.IsSuperImmediate ||
                    Schedule_LP2.IsSuperImmediate ||
                    Schedule_LP3.IsSuperImmediate ||
                    Schedule_MB.IsSuperImmediate ||
                    Schedule_PQ.IsSuperImmediate ||
                    Schedule_RG.IsSuperImmediate ||
                    Schedule_SS.IsSuperImmediate;
            }
        }

        public bool IsParamEmpty
        {
            get
            {
                // add write_password_flag , Write_mdi_reset_date_time , write_contactor_param, Write_modem_limits_time, write_reference_no ,Write_number_Profile,Write_Display_PowerDown_param//  (Schedule_CS.SchType == ScheduleType.Disabled || !IsScheduleReadyToBeProcess(Schedule_CS))
                return !
                    (
                     LPParamRequest.ChannelRequestLP1 ||
                     LPParamRequest.ChannelRequestLP2 ||
                     LPParamRequest.ChangeIntervalRequestLP1 ||
                     LPParamRequest.ChangeIntervalRequestLP2 ||
                     LPParamRequest.ChangeIntervalRequestLP3 ||

                    TBE1WriteRequestID > 0 ||
                    TBE2WriteRequestID > 0 ||
                    TBEPowerFailParamOptions > 0 ||

                    Write_contactor_param ||
                    Apply_new_contactor_state ||

                    Write_reference_no ||
                    Write_mdi_reset_date_time ||
                    Write_password_flag ||
                    Schedule_CS.IsSuperImmediate ||
                    Unset_MajorAlarms ||
                    Read_CS ||

                    SetIPProfile > 0 ||
                    SetKeepAlive > 0 ||
                    WakeUp_Profile_Id > 0 ||
                    SetModemInitializeBasics > 0 ||
                    SetModemInitializeExtended > 0 ||
                    Write_modem_limits_time ||
                    Write_Number_Profile > 0 ||

                    MajorAlarmGroupID > 0 ||
                    DW_NormalID > 0 ||
                    DW_AlternateID > 0 ||
                    Write_Display_PowerDown_param > 0 ||
                    Energy_Param_Id > 0 ||
                    DecimalPoints_Param_Id > 0 ||
                    CP_PT_Param_Id > 0 ||
                    MonitoringTime_Param_Id > 0 ||
                    Limits_Param_Id > 0 ||
                    Write_Load_Shedding_Schedule  ||
                    Write_Consumption_Data_Now ||
                    Write_Consumption_Data_Monthly ||
                    Write_Consumption_Data_Weekly 
                    );
            }
        }

        public bool IsDataRequestEmpty
        {
            get
            {
                // return Schedule_CB.SchType == ScheduleType.Disabled && Schedule_EV.SchType == ScheduleType.Disabled && Schedule_LP.SchType == ScheduleType.Disabled && Schedule_MB.SchType == ScheduleType.Disabled && Schedule_PQ.SchType == ScheduleType.Disabled && Schedule_SS.SchType == ScheduleType.Disabled && !Schedule_CB.IsSuperImmediate && !Schedule_EV.IsSuperImmediate && !Schedule_LP.IsSuperImmediate && !Schedule_MB.IsSuperImmediate && !Schedule_PQ.IsSuperImmediate && !Schedule_SS.IsSuperImmediate;
                return !((Read_CB != READ_METHOD.Disabled && IsScheduleReadyToBeProcess(Schedule_CB)) || Schedule_CB.IsSuperImmediate ||
                         (read_logbook && Read_EV && IsScheduleReadyToBeProcess(Schedule_EV)) || Schedule_EV.IsSuperImmediate ||
                    (Read_LP != READ_METHOD.Disabled && IsScheduleReadyToBeProcess(Schedule_LP)) || Schedule_LP.IsSuperImmediate ||
                    (Read_LP2 != READ_METHOD.Disabled && IsScheduleReadyToBeProcess(Schedule_LP2)) || Schedule_LP2.IsSuperImmediate ||
                    (Read_LP3 != READ_METHOD.Disabled && IsScheduleReadyToBeProcess(Schedule_LP3)) || Schedule_LP3.IsSuperImmediate ||
                    (Read_MB != READ_METHOD.Disabled && IsScheduleReadyToBeProcess(Schedule_MB)) || Schedule_MB.IsSuperImmediate ||
                    (Read_PQ && IsScheduleReadyToBeProcess(Schedule_PQ)) || Schedule_PQ.IsSuperImmediate ||
                    (Read_SS && IsScheduleReadyToBeProcess(Schedule_SS)) || Schedule_SS.IsSuperImmediate ||
                    (Read_RG && IsScheduleReadyToBeProcess(Schedule_RG)) || Schedule_RG.IsSuperImmediate ||
                    (Read_CO && IsScheduleReadyToBeProcess(Schedule_CO)) || Schedule_CO.IsSuperImmediate ||
                    TBE1WriteRequestID > 0 || TBE2WriteRequestID > 0 || 
                    ReadParams);
            }
        }

        public decimal CT
        {
            get
            {
                decimal rslt = 0;
                try
                {
                    rslt = (decimal)_ct_num / _ct_denum;
                    return rslt > 0 ? rslt : 1;
                }
                catch (Exception)
                {
                    return 1;
                }

            }

        }

        public decimal PT
        {
            get
            {
                decimal rslt = 0;
                try
                {
                    rslt = (decimal)_pt_num / _pt_denum;
                    return rslt > 0 ? rslt : 1;
                }
                catch (Exception)
                {
                    return 1;
                }
            }

        }

        public double CostPerKwh { get; set; }

        public int ParentID { get; set; }

        public IPAddress NetworkAdress;

        public UInt32 SubNetMask;

        //split the ip and subnet mask and assign net mask to
        public string NetworkIPAndMAsk
        {
            set
            {

                if (string.IsNullOrEmpty(value))
                {
                    NetworkAdress = IPAddress.None;
                    SubNetMask = 0;
                }
                else
                {
                    var ip = value.Split('/', ':');
                    if (!IPAddress.TryParse(ip[0], out NetworkAdress))
                        throw new Exception("Invalid Network IP Provide Valid IP");
                    UInt32 length;
                    if (!UInt32.TryParse(ip[1], out length))
                        throw new Exception("Invalid Network Mask Length");
                    SubNetMask = length;

                }
            }
        }

        public bool IsAnyMajorAlarmTriger
        {
            get
            {
                return MajorAlarmsProcessedInSession != null &&
                       MajorAlarmsProcessedInSession.Count > 0;
            }
        }

        #endregion

        public MeterInformation()
        {
            MSN = String.Empty;
            GlobalDeviceId = null;
            // Counter_Obj = new Counters();
            MajorAlarmsProcessedInSession = new List<int>(25);
            LP_Counters = new Profile_Counter();
            EV_Counters = new Profile_Counter();
            MB_Counters = new Profile_Counter();
            LP2_Counters = new Profile_Counter();
            LP3_Counters = new Profile_Counter();

            LPParamRequest = new LP_Params();
            Schedule_PQ = new Schedule();
            Schedule_LP = new Schedule();
            Schedule_LP2 = new Schedule();
            Schedule_LP3 = new Schedule();
            Schedule_EV = new Schedule();
            Schedule_CB = new Schedule();
            Schedule_MB = new Schedule();
            Schedule_SS = new Schedule();
            Schedule_CS = new Schedule();
            Schedule_RG = new Schedule();
            Schedule_CO = new Schedule();
            SaveSchedule_PQ = new Schedule();
            ReadPlan = new List<Schedules>();
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
            // MustLogoutMeter = true;
            if (Param_Schedule.SchType == ScheduleType.Immediate ||
                Param_Schedule.SchType == ScheduleType.EveryTime)
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

        public bool IsScheduleToBeProcess(Schedule Param_Schedule, out DateTime ProcessTime)
        {
            ProcessTime = DateTime.MinValue;

            // MustLogOutMeter = true;
            if (Param_Schedule.SchType == ScheduleType.Immediate ||
                Param_Schedule.SchType == ScheduleType.EveryTime)
            {
                ProcessTime = DateTime.Now;
                return true;
            }
            else if (Param_Schedule.SchType == ScheduleType.IntervalFixed ||
                     Param_Schedule.SchType == ScheduleType.IntervalRandom ||
                     Param_Schedule.SchType == ScheduleType.SpecifiedDateTime)
            {
                if (Param_Schedule.BaseDateTime <= DateTime.Now ||
                    (Param_Schedule.BaseDateTime - MaxCurrentSessionTime) <= DateTime.Now) // To Process In Current IO Session Slot
                {
                    ProcessTime = Param_Schedule.BaseDateTime;
                    return true;
                }
                else
                {
                    return false;
                }
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

                // long qtnt = (long)(temp.TotalMinutes / IntervalInMinutes);
                // nextCallDateTime = BaseTime.AddMinutes((qtnt * IntervalInMinutes) + IntervalInMinutes);

                double qtnt = Math.Floor(temp.TotalMinutes / IntervalInMinutes);
                nextCallDateTime = BaseTime.AddMinutes((qtnt + 1) * IntervalInMinutes);
            }
            catch // (Exception ex)
            {
                // Do Not Raise Error
                // throw ex;
                nextCallDateTime = DateTime.MinValue;
            }

            return nextCallDateTime;
        }

        public void DefineReadPlan(string planString)
        {
            try
            {
                if (!string.IsNullOrEmpty(planString.Replace(" ", "")))
                {
                    var schs = planString.Replace(" ", "").Split(',', '|');
                    ReadPlan.Clear();
                    foreach (var sch in schs)
                    {
                        var value = (Schedules)Enum.Parse(typeof(Schedules), sch.ToString());
                        ReadPlan.Add(value);
                    }
                    ReadPlan = ReadPlan.Distinct().ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid Schedule Strings Validation Failed", ex);
            }
        }

        public void GetParamsToRead(string subscription)
        {

            if (subscription.Length != 16)
                subscription = subscription.PadRight(16,'0');
            var Subs = Commons.HexStringToBinary(subscription,Enum.GetNames(typeof(ParamList)).Length);// new BitArray(Enumerable.Range(0, subscription.Length).Where(x => x % 2 == 0).
                                    //Select(x => Convert.ToByte(subscription.Substring(x, 2), 16)).ToArray());

            // char[] decodedParamRights = individual_events_string_alarm
            ParamsToRead = new List<ParamList>();
            ParamsToRead.Clear();
            for (int i = 0; i < Subs.Length; i++)
            {
                if (Subs[i] == '1')
                    ParamsToRead.Add((ParamList)i);
            }
        }

        #endregion

        #region IDisposeable Member

        public void Dispose()
        {
            Schedule_PQ = null;
            Schedule_LP = null;
            Schedule_EV = null;
            Schedule_CB = null;
            Schedule_MB = null;
            Schedule_SS = null;
            Schedule_CS = null;
            SaveSchedule_PQ = null;
            MajorAlarmsProcessedInSession = null;
            LP_Counters = null;
            EV_Counters = null;
            MB_Counters = null;
            LP2_Counters = null;
            LP3_Counters = null;
            Schedule_CO = null;
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

}