using comm;
using SharedCode.Comm.DataContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communicator.MeterConnManager
{
    /// <summary>
    /// Simple MeterInfoScheduler Only Schedule Meter reading based on the Meters Serial Numbers Info Only
    /// </summary>
    public class MeterInfoScheduler : MeterInfoBaseScheduler
    {
        #region Field_Members

        public const byte BasicOp_Value_Weight = 1;
        public const byte ImmediateFlag_Value_Weight = 1;
        public const byte PQ_CM_Value_Weight = 2;
        public const byte LP_Value_Weight = 5;
        public const byte Monthly_Value_Weight = 5;
        public const byte Events_Value_Weight = 5;
        public const byte Misc_Value_Weight = 2;
        public const byte Param_Value_Weight = 2;
        public const byte Param_MiscValue_Weight = 5;

        #endregion

        public MeterInfoScheduler()
            : base()
        {
        }

        #region Properties

        public Scheduler_Type Processing_Mode { get; set; }

        #endregion

        #region Member_Methods

        public override void ExecuteScheduler()
        {
            try
            {
                ScheduledMeterList = null;

                // Processing_Mode
                #region Not_Assigned

                if (Processing_Mode == Scheduler_Type.Not_Assigned)
                {
                    ScheduledMeterList = null;
                    // Do Nothing Business Logic
                    ScheduledMeterList = new List<MeterSerialNumber>(MeterList);
                    ScheduledMeterList.Sort(new Comparison<MeterSerialNumber>((x, y) => x.MSN.CompareTo(y.MSN)));
                }

                #endregion
                #region BatchProcessor

                else if (Processing_Mode == Scheduler_Type.BatchProcessor ||
                         Processing_Mode == Scheduler_Type.ShortestSchTasksProcessor)
                {
                    List<KeyValuePair<MeterSerialNumber, List<int>>> ProcessList = new List<KeyValuePair<MeterSerialNumber, List<int>>>(MeterList.Count);
                    KeyValuePair<MeterSerialNumber, List<int>> CurrentItem = new KeyValuePair<MeterSerialNumber, List<int>>();
                    MeterInformation meter_Info = null;
                    DateTime sch_dueTime = DateTime.MinValue;
                    int currnt_Weight = 0;

                    int Disable_Val = (Processing_Mode == Scheduler_Type.BatchProcessor) ? (int)int.MinValue : (int)int.MaxValue;
                    bool IsBatchProcess = (Processing_Mode == Scheduler_Type.BatchProcessor) ? true : false;
                    int Disable_SchVal = 0;

                    // Initialize Meter Serial Number {MSN}
                    foreach (var meterSrNum in MeterList)
                    {
                        CurrentItem = new KeyValuePair<MeterSerialNumber, List<int>>(meterSrNum, new List<int>(10));
                        ProcessList.Add(CurrentItem);
                    }

                    // Evaluate Each Meter Schedule
                    foreach (var CurrentListItem in ProcessList)
                    {
                        try
                        {
                            CurrentItem = CurrentListItem;
                            meter_Info = GetMeterInformation(CurrentItem.Key, true);

                            // null Meter_Info Obj
                            if (meter_Info == null ||
                                (meter_Info.IsDataRequestEmpty && meter_Info.IsParamEmpty))
                            {
                                // null val
                                CurrentItem.Value.Add(Disable_Val);
                                continue;
                            }

                            #region Data_Request_Schedule Processing

                            // PQ Schedule
                            currnt_Weight = (meter_Info.Read_PQ && meter_Info.IsScheduleToBeProcess(meter_Info.Schedule_PQ, out sch_dueTime)) ? PQ_CM_Value_Weight : Disable_SchVal;
                            currnt_Weight = (currnt_Weight == Disable_SchVal || meter_Info.Schedule_PQ.IsSuperImmediate) ? ImmediateFlag_Value_Weight : currnt_Weight;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);


                            // Cumulative Schedule
                            currnt_Weight = (meter_Info.Read_CB != READ_METHOD.Disabled && meter_Info.IsScheduleToBeProcess(meter_Info.Schedule_CB, out sch_dueTime)) ? PQ_CM_Value_Weight : Disable_SchVal;
                            currnt_Weight = (currnt_Weight == Disable_SchVal || meter_Info.Schedule_CB.IsSuperImmediate) ? ImmediateFlag_Value_Weight : currnt_Weight;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);


                            // Monthly Billing Schedule
                            currnt_Weight = (meter_Info.Read_MB != READ_METHOD.Disabled && meter_Info.IsScheduleToBeProcess(meter_Info.Schedule_MB, out sch_dueTime)) ? Monthly_Value_Weight : Disable_SchVal;
                            currnt_Weight = (currnt_Weight == Disable_SchVal || meter_Info.Schedule_MB.IsSuperImmediate) ? ImmediateFlag_Value_Weight : currnt_Weight;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);


                            // LP Schedule
                            currnt_Weight = (meter_Info.Read_LP != READ_METHOD.Disabled && meter_Info.IsScheduleToBeProcess(meter_Info.Schedule_LP, out sch_dueTime)) ? LP_Value_Weight : Disable_SchVal;
                            currnt_Weight = (currnt_Weight == Disable_SchVal || meter_Info.Schedule_LP.IsSuperImmediate) ? ImmediateFlag_Value_Weight : currnt_Weight;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);


                            // LP2 Schedule
                            currnt_Weight = (meter_Info.Read_LP2 != READ_METHOD.Disabled && meter_Info.IsScheduleToBeProcess(meter_Info.Schedule_LP2, out sch_dueTime)) ? LP_Value_Weight : Disable_SchVal;
                            currnt_Weight = (currnt_Weight == Disable_SchVal || meter_Info.Schedule_LP2.IsSuperImmediate) ? ImmediateFlag_Value_Weight : currnt_Weight;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);


                            // LP3 Schedule
                            currnt_Weight = (meter_Info.Read_LP3 != READ_METHOD.Disabled && meter_Info.IsScheduleToBeProcess(meter_Info.Schedule_LP3, out sch_dueTime)) ? LP_Value_Weight : Disable_SchVal;
                            currnt_Weight = (currnt_Weight == Disable_SchVal || meter_Info.Schedule_LP3.IsSuperImmediate) ? ImmediateFlag_Value_Weight : currnt_Weight;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);


                            // Event Schedule
                            currnt_Weight = ((meter_Info.read_logbook || meter_Info.Read_EV) && meter_Info.IsScheduleToBeProcess(meter_Info.Schedule_EV, out sch_dueTime)) ? Events_Value_Weight : Disable_SchVal;
                            currnt_Weight = (currnt_Weight == Disable_SchVal || meter_Info.Schedule_EV.IsSuperImmediate) ? ImmediateFlag_Value_Weight : currnt_Weight;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);


                            // Signal Strength Schedule
                            currnt_Weight = (meter_Info.Read_SS && meter_Info.IsScheduleToBeProcess(meter_Info.Schedule_SS, out sch_dueTime)) ? BasicOp_Value_Weight : Disable_SchVal;
                            currnt_Weight = (currnt_Weight == Disable_SchVal || meter_Info.Schedule_SS.IsSuperImmediate) ? ImmediateFlag_Value_Weight : currnt_Weight;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);


                            // Contactor Read Schedule
                            currnt_Weight = (meter_Info.Read_CO && meter_Info.IsScheduleToBeProcess(meter_Info.Schedule_CO, out sch_dueTime)) ? BasicOp_Value_Weight : Disable_SchVal;
                            currnt_Weight = (currnt_Weight == Disable_SchVal || meter_Info.Schedule_CO.IsSuperImmediate) ? ImmediateFlag_Value_Weight : currnt_Weight;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);


                            // Remote Grid Schedule
                            currnt_Weight = (meter_Info.Read_RG && meter_Info.IsScheduleToBeProcess(meter_Info.Schedule_RG, out sch_dueTime)) ? BasicOp_Value_Weight : Disable_SchVal;
                            currnt_Weight = (currnt_Weight == Disable_SchVal || meter_Info.Schedule_RG.IsSuperImmediate) ? ImmediateFlag_Value_Weight : currnt_Weight;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);
                            #endregion
                            #region Parameterization Schedule Processing

                            // Read Parameters Schedule
                            currnt_Weight = (meter_Info.ReadParams && meter_Info.ParamsToRead != null && meter_Info.ParamsToRead.Count > 0) ? (Misc_Value_Weight + (meter_Info.ParamsToRead.Count * BasicOp_Value_Weight)) : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);

                            // Write Parameters Schedule
                            // Write Param contactor_state
                            currnt_Weight = (meter_Info.IsContactorSupported && meter_Info.Apply_new_contactor_state) ? BasicOp_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);

                            // Write Write_contactor_param
                            currnt_Weight = (meter_Info.Write_contactor_param) ? BasicOp_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);

                            // Write Write_contactor_param
                            currnt_Weight = (meter_Info.Write_contactor_param) ? BasicOp_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);


                            // Write LPParamRequest.ChannelRequestLP1
                            currnt_Weight = (meter_Info.LPParamRequest.ChannelRequestLP1) ? Param_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);

                            // Write LPParamRequest.ChannelRequestLP2
                            currnt_Weight = (meter_Info.LPParamRequest.ChannelRequestLP2) ? Param_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);

                            // Write LPParamRequest.ChangeIntervalRequestLP1
                            currnt_Weight = (meter_Info.LPParamRequest.ChangeIntervalRequestLP1) ? BasicOp_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);

                            // Write LPParamRequest.ChangeIntervalRequestLP2
                            currnt_Weight = (meter_Info.LPParamRequest.ChangeIntervalRequestLP2) ? BasicOp_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);

                            // Write LPParamRequest.ChangeIntervalRequestLP3
                            currnt_Weight = (meter_Info.LPParamRequest.ChangeIntervalRequestLP3) ? BasicOp_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);



                            // Write TBE1WriteRequestID
                            currnt_Weight = (meter_Info.TBE1WriteRequestID > 0) ? Param_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);

                            // Write TBE2WriteRequestID
                            currnt_Weight = (meter_Info.TBE2WriteRequestID > 0) ? Param_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);

                            // Write TBEPowerFailParamOptions
                            currnt_Weight = (meter_Info.TBEPowerFailParamOptions > 0) ? Param_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);



                            // Write_reference_no
                            currnt_Weight = (meter_Info.Write_reference_no) ? BasicOp_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);

                            // Write_mdi_reset_date_time
                            currnt_Weight = (meter_Info.Write_mdi_reset_date_time) ? BasicOp_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);

                            // Write_password_flag
                            currnt_Weight = (meter_Info.Write_password_flag) ? Param_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);



                            // Write_Clock Syn Param
                            currnt_Weight = (meter_Info.IsScheduleToBeProcess(meter_Info.Schedule_CS, out sch_dueTime)) ? Param_Value_Weight : Disable_SchVal;
                            currnt_Weight = (currnt_Weight == Disable_SchVal || meter_Info.Schedule_CS.IsSuperImmediate) ? BasicOp_Value_Weight : currnt_Weight;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);

                            // Write_Unset_MajorAlarms
                            currnt_Weight = (meter_Info.Unset_MajorAlarms) ? Param_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);


                            // Write SetIPProfile
                            currnt_Weight = (meter_Info.SetIPProfile > 0) ? Param_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);

                            // Write SetKeepAlive
                            currnt_Weight = (meter_Info.SetKeepAlive > 0) ? Param_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);

                            // Write_Param WakeUp_Profile_Id
                            currnt_Weight = (meter_Info.WakeUp_Profile_Id > 0) ? Param_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);


                            // Write_Param SetModemInitializeBasics
                            currnt_Weight = (meter_Info.SetModemInitializeBasics > 0) ? Param_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);


                            // Write_Param SetModemInitializeExtended
                            currnt_Weight = (meter_Info.SetModemInitializeExtended > 0) ? Param_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);


                            // Write_Param  Write_modem_limits_time
                            currnt_Weight = (meter_Info.Write_modem_limits_time) ? Param_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);


                            // Write_Param  Write_Number_Profile
                            currnt_Weight = (meter_Info.Write_Number_Profile > 0) ? Param_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);


                            // Write_Param  MajorAlarmGroupID
                            currnt_Weight = (meter_Info.MajorAlarmGroupID > 0) ? Param_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);

                            // Write_Param  DW_NormalID
                            currnt_Weight = (meter_Info.DW_NormalID > 0) ? Param_MiscValue_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);


                            // Write_Param  DW_AlternateID
                            currnt_Weight = (meter_Info.DW_AlternateID > 0) ? Param_MiscValue_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);


                            // Write_Param  Write_Display_PowerDown_param
                            currnt_Weight = (meter_Info.Write_Display_PowerDown_param > 0) ? Param_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);


                            // Write_Param  Energy_Param_Id
                            currnt_Weight = (meter_Info.Energy_Param_Id > 0) ? Param_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);


                            // Write_Param  DecimalPoints_Param_Id
                            currnt_Weight = (meter_Info.DecimalPoints_Param_Id > 0) ? Param_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);


                            // Write_Param  CP_PT_Param_Id
                            currnt_Weight = (meter_Info.CP_PT_Param_Id > 0) ? Param_Value_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);


                            // Write_Param  MonitoringTime_Param_Id
                            currnt_Weight = (meter_Info.MonitoringTime_Param_Id > 0) ? Param_MiscValue_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);


                            // Write_Param  Limits_Param_Id
                            currnt_Weight = (meter_Info.Limits_Param_Id > 0) ? Param_MiscValue_Weight : Disable_SchVal;
                            // Add currnt_Weight
                            CurrentItem.Value.Add(currnt_Weight);

                            #endregion
                        }
                        catch
                        {
                            CurrentItem.Value.Clear();
                            // null val
                            CurrentItem.Value.Add(Disable_Val);

                            // Log Error
                            // throw;
                        }
                    }

                    // Schedule Sub-Meters Processing
                    ProcessList.Sort(new BatchProcessorComparer());

                    // Reverse Already Sorted Items
                    if (IsBatchProcess)
                        ProcessList.Reverse();

                    // Processing Final Results
                    base.ScheduledMeterList = new List<MeterSerialNumber>(MeterList.Count);
                    foreach (var CurrentListItem in ProcessList)
                    {
                        base.ScheduledMeterList.Add(CurrentListItem.Key);
                    }
                }

                #endregion
                #region Keep Alive Scheduler Due Time

                else if (Processing_Mode == Scheduler_Type.KasSchDueTime)
                {
                    List<KeyValuePair<MeterSerialNumber, DateTime[]>> ProcessList = new List<KeyValuePair<MeterSerialNumber, DateTime[]>>(MeterList.Count);
                    KeyValuePair<MeterSerialNumber, DateTime[]> CurrentItem = new KeyValuePair<MeterSerialNumber, DateTime[]>();
                    MeterInformation meter_Info = null;
                    DateTime sch_dueTime = DateTime.MinValue;
                    DateTime Disable_SchVal = DateTime.MaxValue;

                    // Initialize Meter Serial Number {MSN}
                    foreach (var meterSrNum in MeterList)
                    {
                        CurrentItem = new KeyValuePair<MeterSerialNumber, DateTime[]>(meterSrNum, new DateTime[1]);
                        ProcessList.Add(CurrentItem);
                    }

                    // Evaluate Each Meter Schedule
                    foreach (var CurrentListItem in ProcessList)
                    {
                        try
                        {
                            CurrentItem = CurrentListItem;
                            meter_Info = GetMeterInformation(CurrentItem.Key, false);

                            // null Meter_Info Obj
                            if (meter_Info == null ||
                                (meter_Info.IsDataRequestEmpty && meter_Info.IsParamEmpty))
                            {
                                // null val
                                CurrentItem.Value[0] = Disable_SchVal;
                                continue;
                            }

                            DateTime schedule_Time = meter_Info.Kas_DueTime;
                            CurrentItem.Value[0] = schedule_Time;
                        }
                        catch
                        {
                            CurrentItem.Value[0] = Disable_SchVal;
                            // null val
                            // Log Error
                            // throw;
                        }
                    }

                    // Schedule Sub-Meters Processing
                    ProcessList.Sort(new Comparison<KeyValuePair<MeterSerialNumber, DateTime[]>>((x, y) => x.Value[0].CompareTo(y.Value[0])));

                    // Processing Final Results
                    base.ScheduledMeterList = new List<MeterSerialNumber>(MeterList.Count);
                    foreach (var CurrentListItem in ProcessList)
                    {
                        base.ScheduledMeterList.Add(CurrentListItem.Key);
                    }
                }

                #endregion
                #region TasksSuccessRate

                else if (Processing_Mode == Scheduler_Type.TasksSuccessRate)
                {
                    List<KeyValuePair<MeterSerialNumber, float[]>> ProcessList = new List<KeyValuePair<MeterSerialNumber, float[]>>(MeterList.Count);
                    KeyValuePair<MeterSerialNumber, float[]> CurrentItem = new KeyValuePair<MeterSerialNumber, float[]>();
                    MeterInformation meter_Info = null;
                    DateTime sch_dueTime = DateTime.MinValue;
                    float Disable_SchVal = 0.0f;

                    // Initialize Meter Serial Number {MSN}
                    foreach (var meterSrNum in MeterList)
                    {
                        CurrentItem = new KeyValuePair<MeterSerialNumber, float[]>(meterSrNum, new float[1]);
                        ProcessList.Add(CurrentItem);
                    }

                    // Evaluate Each Meter Schedule
                    foreach (var CurrentListItem in ProcessList)
                    {
                        try
                        {
                            CurrentItem = CurrentListItem;
                            meter_Info = GetMeterInformation(CurrentItem.Key, false);

                            // null Meter_Info Obj
                            if (meter_Info == null ||
                                (meter_Info.IsDataRequestEmpty && meter_Info.IsParamEmpty))
                            {
                                // null val
                                CurrentItem.Value[0] = Disable_SchVal;
                                continue;
                            }

                            float success_Ratio = 0;
                            // Compute Success Failure Ratio
                            if (meter_Info.total_tries > 0 &&
                               meter_Info.total_tries < uint.MaxValue)
                            {
                                success_Ratio = (meter_Info.total_success / meter_Info.total_tries) * 1.0f;
                            }

                            CurrentItem.Value[0] = success_Ratio;
                        }
                        catch
                        {
                            CurrentItem.Value[0] = Disable_SchVal;
                            // null val
                            // Log Error
                            // throw;
                        }
                    }

                    // Schedule Sub-Meters Processing
                    ProcessList.Sort(new Comparison<KeyValuePair<MeterSerialNumber, float[]>>((x, y) => x.Value[0].CompareTo(y.Value[0])));
                    // Reverse Sort based on Success Ratio
                    ProcessList.Reverse();

                    // Processing Final Results
                    base.ScheduledMeterList = new List<MeterSerialNumber>(MeterList.Count);
                    foreach (var CurrentListItem in ProcessList)
                    {
                        base.ScheduledMeterList.Add(CurrentListItem.Key);
                    }
                }

                #endregion
                #region Scheduler Task Due Time

                else if (Processing_Mode == Scheduler_Type.SchTaskDueTime)
                {
                    List<KeyValuePair<MeterSerialNumber, List<DateTime>>> ProcessList = new List<KeyValuePair<MeterSerialNumber, List<DateTime>>>(MeterList.Count);
                    KeyValuePair<MeterSerialNumber, List<DateTime>> CurrentItem = new KeyValuePair<MeterSerialNumber, List<DateTime>>();
                    MeterInformation meter_Info = null;
                    DateTime sch_dueTime = DateTime.MaxValue;
                    DateTime Disable_ScheduleDateTime_Val = DateTime.MaxValue;
                    DateTime Scheduler_Exec_Time = DateTime.Now;
                    int currnt_Weight = 0;
                    int Max_Comparision_Count = 10;

                    int Disable_Val = (Processing_Mode == Scheduler_Type.SchTaskDueTime) ? (int)int.MinValue : (int)int.MaxValue;
                    int Disable_SchVal = Disable_Val;


                    // Initialize Meter Serial Number
                    foreach (var meterSrNum in MeterList)
                    {
                        CurrentItem = new KeyValuePair<MeterSerialNumber, List<DateTime>>(meterSrNum, new List<DateTime>(Max_Comparision_Count));

                        for (int index = 0; index < Max_Comparision_Count; index++)
                            CurrentItem.Value.Add(Disable_ScheduleDateTime_Val);

                        ProcessList.Add(CurrentItem);
                    }

                    int indexer = 0;

                    // Evaluate Each Meter Schedule
                    foreach (var CurrentListItem in ProcessList)
                    {
                        try
                        {
                            CurrentItem = CurrentListItem;
                            meter_Info = GetMeterInformation(CurrentItem.Key, false);

                            // null Meter_Info Obj
                            if (meter_Info == null ||
                                (meter_Info.IsDataRequestEmpty && meter_Info.IsParamEmpty))
                            {
                                // null val
                                // CurrentItem.Value.Add(Disable_Val);
                                continue;
                            }

                            indexer = 0;
                            #region Data_Request_Schedule Processing

                            // PQ Schedule
                            currnt_Weight = (meter_Info.Read_PQ && meter_Info.IsScheduleToBeProcess(meter_Info.Schedule_PQ, out sch_dueTime)) ? PQ_CM_Value_Weight : Disable_SchVal;

                            if (currnt_Weight == Disable_SchVal ||
                                meter_Info.Schedule_PQ.IsSuperImmediate)
                            {
                                currnt_Weight = ImmediateFlag_Value_Weight;
                                sch_dueTime = Scheduler_Exec_Time;
                            }

                            // Add sch_dueTime
                            if (currnt_Weight != Disable_SchVal)
                                CurrentItem.Value[indexer++] = sch_dueTime;
                            else
                                CurrentItem.Value[indexer++] = Disable_ScheduleDateTime_Val;


                            // Cumulative Schedule
                            currnt_Weight = (meter_Info.Read_CB!= READ_METHOD.Disabled && meter_Info.IsScheduleToBeProcess(meter_Info.Schedule_CB, out sch_dueTime)) ? PQ_CM_Value_Weight : Disable_SchVal;

                            if (currnt_Weight == Disable_SchVal ||
                                meter_Info.Schedule_CB.IsSuperImmediate)
                            {
                                currnt_Weight = ImmediateFlag_Value_Weight;
                                sch_dueTime = Scheduler_Exec_Time;
                            }

                            // Add sch_dueTime
                            if (currnt_Weight != Disable_SchVal)
                                CurrentItem.Value[indexer++] = sch_dueTime;
                            else
                                CurrentItem.Value[indexer++] = Disable_ScheduleDateTime_Val;


                            // Monthly Billing Schedule
                            currnt_Weight = (meter_Info.Read_MB != READ_METHOD.Disabled &&
                                             meter_Info.IsScheduleToBeProcess(meter_Info.Schedule_MB, out sch_dueTime)) ? Monthly_Value_Weight : Disable_SchVal;

                            if (currnt_Weight == Disable_SchVal ||
                                meter_Info.Schedule_MB.IsSuperImmediate)
                            {
                                currnt_Weight = ImmediateFlag_Value_Weight;
                                sch_dueTime = Scheduler_Exec_Time;
                            }

                            // Add sch_dueTime
                            if (currnt_Weight != Disable_SchVal)
                                CurrentItem.Value[indexer++] = sch_dueTime;
                            else
                                CurrentItem.Value[indexer++] = Disable_ScheduleDateTime_Val;


                            // LP Schedule
                            currnt_Weight = (meter_Info.Read_LP != READ_METHOD.Disabled && meter_Info.IsScheduleToBeProcess(meter_Info.Schedule_LP, out sch_dueTime)) ? LP_Value_Weight : Disable_SchVal;

                            if (currnt_Weight == Disable_SchVal ||
                                meter_Info.Schedule_LP.IsSuperImmediate)
                            {
                                currnt_Weight = ImmediateFlag_Value_Weight;
                                sch_dueTime = Scheduler_Exec_Time;
                            }

                            // Add sch_dueTime
                            if (currnt_Weight != Disable_SchVal)
                                CurrentItem.Value[indexer++] = sch_dueTime;
                            else
                                CurrentItem.Value[indexer++] = Disable_ScheduleDateTime_Val;

                            // LP2 Schedule
                            currnt_Weight = (meter_Info.Read_LP2 != READ_METHOD.Disabled && meter_Info.IsScheduleToBeProcess(meter_Info.Schedule_LP2, out sch_dueTime)) ? LP_Value_Weight : Disable_SchVal;

                            if (currnt_Weight == Disable_SchVal ||
                                meter_Info.Schedule_LP2.IsSuperImmediate)
                            {
                                currnt_Weight = ImmediateFlag_Value_Weight;
                                sch_dueTime = Scheduler_Exec_Time;
                            }

                            // Add sch_dueTime
                            if (currnt_Weight != Disable_SchVal)
                                CurrentItem.Value[indexer++] = sch_dueTime;
                            else
                                CurrentItem.Value[indexer++] = Disable_ScheduleDateTime_Val;


                            // LP3 Schedule
                            currnt_Weight = (meter_Info.Read_LP3 != READ_METHOD.Disabled && meter_Info.IsScheduleToBeProcess(meter_Info.Schedule_LP3, out sch_dueTime)) ? LP_Value_Weight : Disable_SchVal;

                            if (currnt_Weight == Disable_SchVal ||
                                meter_Info.Schedule_LP3.IsSuperImmediate)
                            {
                                currnt_Weight = ImmediateFlag_Value_Weight;
                                sch_dueTime = Scheduler_Exec_Time;
                            }

                            // Add sch_dueTime
                            if (currnt_Weight != Disable_SchVal)
                                CurrentItem.Value[indexer++] = sch_dueTime;
                            else
                                CurrentItem.Value[indexer++] = Disable_ScheduleDateTime_Val;


                            // Event Schedule
                            currnt_Weight = ((meter_Info.read_logbook || meter_Info.Read_EV) && meter_Info.IsScheduleToBeProcess(meter_Info.Schedule_EV, out sch_dueTime)) ? Events_Value_Weight : Disable_SchVal;

                            if (currnt_Weight == Disable_SchVal ||
                                meter_Info.Schedule_EV.IsSuperImmediate)
                            {
                                currnt_Weight = ImmediateFlag_Value_Weight;
                                sch_dueTime = Scheduler_Exec_Time;
                            }

                            // Add sch_dueTime
                            if (currnt_Weight != Disable_SchVal)
                                CurrentItem.Value[indexer++] = sch_dueTime;
                            else
                                CurrentItem.Value[indexer++] = Disable_ScheduleDateTime_Val;

                            // Signal Strength Schedule
                            currnt_Weight = (meter_Info.Read_SS && meter_Info.IsScheduleToBeProcess(meter_Info.Schedule_SS, out sch_dueTime)) ? BasicOp_Value_Weight : Disable_SchVal;

                            if (currnt_Weight == Disable_SchVal ||
                                meter_Info.Schedule_SS.IsSuperImmediate)
                            {
                                currnt_Weight = ImmediateFlag_Value_Weight;
                                sch_dueTime = Scheduler_Exec_Time;
                            }

                            // Add sch_dueTime
                            if (currnt_Weight != Disable_SchVal)
                                CurrentItem.Value[indexer++] = sch_dueTime;
                            else
                                CurrentItem.Value[indexer++] = Disable_ScheduleDateTime_Val;


                            // Contactor Read Schedule
                            currnt_Weight = (meter_Info.Read_CO && meter_Info.IsScheduleToBeProcess(meter_Info.Schedule_CO, out sch_dueTime)) ? BasicOp_Value_Weight : Disable_SchVal;

                            if (currnt_Weight == Disable_SchVal ||
                                meter_Info.Schedule_CO.IsSuperImmediate)
                            {
                                currnt_Weight = ImmediateFlag_Value_Weight;
                                sch_dueTime = Scheduler_Exec_Time;
                            }

                            // Add sch_dueTime
                            if (currnt_Weight != Disable_SchVal)
                                CurrentItem.Value[indexer++] = sch_dueTime;
                            else
                                CurrentItem.Value[indexer++] = Disable_ScheduleDateTime_Val;

                            #endregion

                        }
                        catch
                        {
                            CurrentItem.Value.Clear();
                            // null val
                            // set null val
                            // CurrentItem.Value.Add(Disable_Val);
                            for (int index = 0; index < Max_Comparision_Count; index++)
                                CurrentItem.Value.Add(Disable_ScheduleDateTime_Val);

                            // Log Error
                            // throw;
                        }
                    }

                    // Scheduler Sub-Meter Processing
                    ProcessList.Sort(new DateTimeValuesComparer());
                    // Reverse Process List
                    ProcessList.Reverse();

                    // Processing Final Results
                    base.ScheduledMeterList = new List<MeterSerialNumber>(MeterList.Count);
                    foreach (var CurrentListItem in ProcessList)
                    {
                        base.ScheduledMeterList.Add(CurrentListItem.Key);
                    }
                }

                #endregion
                #region Last Task Execution Time

                else if (Processing_Mode == Scheduler_Type.LastTaskExecutionTime)
                {
                    // Not Real Schedule Work
                    // Only Dummy Scheduler Type
                    ScheduledMeterList = null;
                    // Do Nothing Business Logic
                    ScheduledMeterList = new List<MeterSerialNumber>(MeterList);
                    ScheduledMeterList.Sort(new Comparison<MeterSerialNumber>((x, y) => x.MSN.CompareTo(y.MSN)));
                }

                #endregion
                else if( Processing_Mode == Scheduler_Type.OnDemandReading)
                {
                    MeterInformation meter_Info = null;
                    base.ScheduledMeterList = new List<MeterSerialNumber>(MeterList.Count);
                    foreach (var meterSrNum in MeterList)
                    {
                        meter_Info = GetMeterInformation(meterSrNum, true);
                        if (meter_Info != null && meter_Info.MeterType_OBJ != MeterType.Intruder && meter_Info.OnDemandRequest)
                            base.ScheduledMeterList.Add(meterSrNum);
                    }
                }

                else
                {
                    ScheduledMeterList = null;
                    // Do Nothing Business Logic
                    ScheduledMeterList = new List<MeterSerialNumber>(MeterList);
                }
            }
            catch { }
        }

        #endregion
    }

}
