//#define LCD_SEGMENT_TEST

using comm;
using DatabaseManager.Properties;
using DLMS;
using DLMS.Comm;
//using MySql.Data.MySqlClient;
using SharedCode;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.Param;
using SharedCode.Common;
using SharedCode.eGeniousDisplayUnit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace DatabaseManager.Database
{
    public delegate void DbExceptionOccur(Exception ex);
    public class DatabaseController : IDisposable
    {
        #region Mostly Used Functions


        public bool UpdateGatewayMetersProcessingStatus(long parentId)
        {
            try
            {
                DBConnect.OpenConnection();
                var QueryToUpdate = string.Format("update meter set processed_by_gateway=0 where parent_id={0} and processed_by_gateway=1", parentId);
                if (string.IsNullOrEmpty(QueryToUpdate)) return true;
                if (ExecuteQuery(QueryToUpdate))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;//report error
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public void insert_GateWayScanResults(GatewayScanResult dto)
        {
            try
            {
                string query = string.Empty;
                query = string.Format("INSERT INTO gateway_scan_result( gsn, scan_start_time,scan_end_time ,session_time,total_gateway_meters, scaned_meters,success,failure,max_scan_time,min_scan_time,avg_scan_time,total_scan_time,scan_termination_reason,readable_meters,schedule_type) VALUES({0},'{1}','{2}','{3}',{4},{5},{6},{7},'{8}','{9}','{10}','{11}','{12}','{13}','{14}')",
                    dto.GatewayNo, dto.ScanStartTime.ToString(DateFormat), dto.ScanEndTime.ToString(DateFormat), dto.SessionTime.ToString(DateFormat), dto.TotalGatewayMeters, dto.ScanedMeters, dto.Success, dto.Failure, dto.MaxScanTime.ToString(TimeFormat), dto.MinScanTime.ToString(TimeFormat), dto.AvgScanTime.ToString(TimeFormat), dto.TotalScanTime.ToString(TimeFormat), dto.ScanTerminationReason.ToString(), dto.TotalReadableMeters, dto.ScheduleType);
                ExecuteQuery(query);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void TryParseDateTime(string dateTimeStr, out DateTime dateTime)
        {
            try
            {
                dateTime = DateTime.Now;
                DateTime.TryParse(dateTimeStr, out dateTime);
            }
            catch (Exception)
            {
                dateTime = DateTime.Now;
            }
        }
        public MeterInformation GetMeterSettings(string MSN, bool chechForOnDemand = false, long parent_id = -1)
        {
            DBConnect.OpenConnection();
            MeterInformation tempMeterInfo = new MeterInformation();
            DataTable DT = null;
            OdbcCommand Command = null;
            string query = null;
            try
            {
                //, `save_life_time` has been removed,
                query = "SELECT `feeder_id`,`msn`,`parent_id`, `global_device_id` ,`password`,`type`,`network_ip`,`device_type`,`llc_protocol_type`,`model_id`, `status`, `unset_ma`,`hdlc_Address`,`schedule_type`, "
                    + "`tbe1_write_request_id`,`tbe2_write_request_id`, `enable_live_updation`, `read_events_on_major_alarms`, `log_echo_enable`,`dw_normal_mode_id`,`dw_alternate_mode_id`,`dw_normal_format`,`dw_alternate_format`,`scroll_time`, "
                    + "`log_save_enable`, `read_mb_on_mdi_reset`, `read_pq`, `read_ev`, `read_cb`, `read_mb`, `read_lp`, `read_ar`,`read_cs`, "
                    + "`read_ss`, `save_pq`, `save_ev`, `save_cb`, `save_mb`, `save_lp`, `save_ar`, `save_st`, "
                    + "`last_lp_time`, `last_ev_time`, `last_pq_time`, `last_cb_time`, `last_mb_time`,`last_ss_time`,`last_cs_time`, `save_last_pq_time`, "
                    + "`default_reset_duration`, `detailed_billing_id`, `max_lp_count_diff`, `min_lp_count_diff`, `max_ev_count_diff`, `min_ev_count_diff`, `set_ip_profiles`, `set_modem_initialize_basic`, `major_alarm_group_id`,`major_alarm_string`, "
                    + "`set_modem_initialize_extended`, `set_keepalive`, `sch_pq`, `base_time_pq`, `interval_pq`, `sch_cb`, `base_time_cb`, `interval_cb`, `sch_mb`, "
                    + "`base_time_mb`, `interval_mb`, `sch_ev`, `base_time_ev`, `interval_ev`, `sch_lp`, `base_time_lp`, `interval_lp`, `sch_ss`, `base_time_ss`, `interval_ss`, `max_load_profile_entries`, `max_events_entries`, "
                    + "`sch_cs`, `base_time_cs`, `interval_cs`, `save_sch_pq`, `save_base_time_pq`, `save_interval_pq`, "
                    + "`super_immediate_pq`, `super_immediate_cb`, `super_immediate_mb`, `super_immediate_ev`, `super_immediate_lp`, `super_immediate_ss`, `super_immediate_cs`, "
                    + "`monthly_billing_counter`,`events_liveUpdate_individual`,`events_liveUpdate_logbook`,`lp_chunk_size`,"
                    + "`read_individual_events_sch`,`read_logbook`,`individual_events_string_sch`,`individual_events_string_alarm`, "
                    + "`load_profile_counter`,`load_profile2_counter`,`load_profile3_counter`, `load_profile_group_id`, `load_profile2_group_id`, `load_profile3_group_id`, `event_counter`, `kas_interval`, `kas_due_time`, `events_to_save_pq`,"
                    + "`log_level`, `lp_write_channel_request`,`lp2_write_channel_request`,"
                    + "`lp_write_interval_request`, `lp_write_interval`,`lp2_write_interval_request`, `lp2_write_interval`,`lp3_write_interval_request`, `lp3_write_interval`,`is_prepaid`,`reference_no`,`meter_sim_no`,`wakeup_password`,`wakeup_no1`,`wakeup_no2`,"
                    + "`wakeup_no3`,`wakeup_no4`,`contactor_param_id`,`current_contactor_status`,`write_contactor_param`,`new_meter_password`,`write_password_flag`, "
                    + "`new_password_activation_time`, `max_cs_difference`,`apply_new_contactor_state`,`mdi_reset_date`,`write_mdi_reset_date`,`default_password`,`last_password_update_time`,"
                    + "`write_modem_limits_time`,`modem_limits_time_id`,`write_reference_no`,`number_profile_group_id`,`display_power_down_id`,`apply_disable_tbe_flag_on_powerfail`,`set_wakeup_profile_id`,"
                    + "`lp_invalid_update`,`ev_invalid_update`,`mb_invalid_update`,`read_debug_error`,`wakeup_request_id`,`ct_ratio_num`,`ct_ratio_denum`,`pt_ratio_num`,`pt_ratio_denum`,"
                    + "`limits_param_id`, `monitoring_time_param_id`, `ct_pt_param_id`, `decimal_points_param_id`, `energy_param_id`,`last_contactor_st_time`, `is_contactor`,`schedule_plan`,`read_params`,`read_param_string`,`contactor_lock`,`contactor_priority_seq`  "
                    + ",`authentication_key`,`encryption_key`,`master_key`,`security_policy`,`encryption_counter`,`read_rg`,`sch_rg`,`interval_rg`,`last_rgi_time`,`base_time_rg`,`super_immediate_rg`,`association_id`,`dds_compatible` "
                    + ",`read_lp2`,`save_lp2`,`last_lp2_time`,`sch_lp2`,`base_time_lp2`,`interval_lp2`,`max_load_profile2_entries`,`lp2_chunk_size`,`load_profile2_counter`,`max_lp2_count_diff`,`min_lp2_count_diff`,`super_immediate_lp2`,`lp2_invalid_update`"
                    + ",`read_lp3`,`save_lp3`,`last_lp3_time`,`sch_lp3`,`base_time_lp3`,`interval_lp3`,`max_load_profile3_entries`,`lp3_chunk_size`,`load_profile3_counter`,`max_lp3_count_diff`,`min_lp3_count_diff`,`super_immediate_lp3`,`lp3_invalid_update`"
                    + ",`mb_method`,`max_billing_months`,`cs_sync_method`,`read_instant_lp`,`read_instant_lp2`,`read_instant_lp3`"
                    + ",`save_events_on_alarm`,`read_co`,`sch_co`,`interval_co`,`base_time_co`,`last_co_time`,`super_immediate_co`,`standard_parameters`,`prioritize_wakeup`"
                    + ",`total_tries`,`total_success`,`update_ar_live`,`send_ar_response`,`write_load_shedding_schedule`,`load_shedding_schedule_id`"
                    + ",`write_consumption_data_now`,`write_consumption_data_weekly`,`write_consumption_data_monthly`"
                    + " FROM meter WHERE msn = " + MSN + (chechForOnDemand ? " or ( processed_by_gateway = 0 and parent_id = " + parent_id + " and (super_immediate_pq=1 or super_immediate_cb=1 or super_immediate_lp=1 or super_immediate_ev=1 or super_immediate_co=1 or super_immediate_rg=1))" : "")
                    + (chechForOnDemand ? " order by super_immediate_pq desc,super_immediate_cb desc,super_immediate_lp desc,super_immediate_ev desc,super_immediate_co desc,super_immediate_rg desc" : "") + " Limit 1 ";

                Command = new OdbcCommand(query, Connection);
                DT = GetTableFromDB(Command, DT);
                if (DT.Rows.Count > 0)
                {
                    tempMeterInfo.MSN = DT.Rows[0][DT.Columns["msn"]].ToString();
                    tempMeterInfo.GlobalDeviceId = (DT.Rows[0][DT.Columns["global_device_id"]] == DBNull.Value) ? "" : DT.Rows[0][DT.Columns["global_device_id"]].ToString();
                    tempMeterInfo.Password = DT.Rows[0][DT.Columns["password"]].ToString();
                    tempMeterInfo.StandardParameter = Convert.ToBoolean(DT.Rows[0]["standard_parameters"]);
                    tempMeterInfo.ModelID = Convert.ToUInt32(DT.Rows[0][DT.Columns["model_id"]]);
                    tempMeterInfo.ParentID = Convert.ToInt32((DT.Rows[0][DT.Columns["parent_id"]] == DBNull.Value) ? "-1" : DT.Rows[0][DT.Columns["parent_id"]]);
                    tempMeterInfo.MeterID = Convert.ToInt64(DT.Rows[0][DT.Columns["feeder_id"]]);
                    tempMeterInfo.EventsHexString = DT.Rows[0][DT.Columns["events_to_save_pq"]].ToString();
                    tempMeterInfo.LogLevel = Convert.ToUInt32(DT.Rows[0][DT.Columns["log_level"]]);
                    //  tempMeterInfo.save_packet_log = Convert.ToBoolean(DT.Rows[0]["save_packet_log"]);

                    //Parameterization
                    tempMeterInfo.Unset_MajorAlarms = Convert.ToBoolean(DT.Rows[0][DT.Columns["unset_ma"]]);
                    tempMeterInfo.TBE1WriteRequestID = Convert.ToInt64(DT.Rows[0][DT.Columns["tbe1_write_request_id"]]);
                    tempMeterInfo.TBE2WriteRequestID = Convert.ToInt64(DT.Rows[0][DT.Columns["tbe2_write_request_id"]]);
                    tempMeterInfo.SetIPProfile = Convert.ToInt64(DT.Rows[0][DT.Columns["set_ip_profiles"]]);
                    tempMeterInfo.SetKeepAlive = Convert.ToInt64(DT.Rows[0][DT.Columns["set_keepalive"]]);
                    tempMeterInfo.MajorAlarmGroupID = Convert.ToInt64(DT.Rows[0][DT.Columns["major_alarm_group_id"]]);
                    tempMeterInfo.MajorAlarmsString = DT.Rows[0][DT.Columns["major_alarm_string"]].ToString();
                    tempMeterInfo.SetModemInitializeBasics = Convert.ToInt64(DT.Rows[0][DT.Columns["set_modem_initialize_basic"]]);
                    tempMeterInfo.SetModemInitializeExtended = Convert.ToInt64(DT.Rows[0][DT.Columns["set_modem_initialize_extended"]]);
                    tempMeterInfo.Status = Convert.ToBoolean(DT.Rows[0][DT.Columns["status"]]);
                    tempMeterInfo.DW_NormalID = Convert.ToUInt32(DT.Rows[0][DT.Columns["dw_normal_mode_id"]]);
                    tempMeterInfo.DW_AlternateID = Convert.ToUInt32(DT.Rows[0][DT.Columns["dw_alternate_mode_id"]]);
                    tempMeterInfo.DW_ScrollTime = Convert.ToUInt32(DT.Rows[0][DT.Columns["scroll_time"]]);
                    tempMeterInfo.DW_Normal_format = Convert.ToUInt32(DT.Rows[0][DT.Columns["dw_normal_format"]]);
                    tempMeterInfo.DW_Alternate_format = Convert.ToUInt32(DT.Rows[0][DT.Columns["dw_alternate_format"]]);

                    //Misc Fields
                    //these flags will specify if duplicate entries is permissible in respective tables

                    //Counters


                    tempMeterInfo.EV_Counters.InvalidUpdate = Convert.ToUInt16(DT.Rows[0][DT.Columns["ev_invalid_update"]]);
                    //MOnthly Billing
                    tempMeterInfo.MB_Counters.DB_Counter = Convert.ToUInt32(DT.Rows[0][DT.Columns["monthly_billing_counter"]]);
                    tempMeterInfo.MB_Counters.InvalidUpdate = Convert.ToUInt16(DT.Rows[0][DT.Columns["mb_invalid_update"]]);
                    tempMeterInfo.BillingMethodId = Convert.ToByte(DT.Rows[0][DT.Columns["mb_method"]]);
                    tempMeterInfo.MB_Counters.Max_Size = Convert.ToByte(DT.Rows[0][DT.Columns["max_billing_months"]]);


                    //Events
                    tempMeterInfo.EV_Counters.MaxDifferenceCheck = Convert.ToUInt32(DT.Rows[0][DT.Columns["max_ev_count_diff"]]);
                    tempMeterInfo.EV_Counters.MinDifferenceCheck = Convert.ToUInt32(DT.Rows[0][DT.Columns["min_ev_count_diff"]]);
                    tempMeterInfo.EV_Counters.Max_Size = Convert.ToUInt32(DT.Rows[0][DT.Columns["max_events_entries"]]);
                    tempMeterInfo.EV_Counters.DB_Counter = Convert.ToUInt32(DT.Rows[0][DT.Columns["event_counter"]]);


                    tempMeterInfo.EnableLiveUpdate = Convert.ToBoolean(DT.Rows[0][DT.Columns["enable_live_updation"]]);
                    tempMeterInfo.ReadEventsOnMajorAlarms = Convert.ToBoolean(DT.Rows[0][DT.Columns["read_events_on_major_alarms"]]);
                    tempMeterInfo.EnableEchoLog = Convert.ToBoolean(DT.Rows[0][DT.Columns["log_echo_enable"]]);
                    tempMeterInfo.EnableSaveLog = Convert.ToBoolean(DT.Rows[0][DT.Columns["log_save_enable"]]);
                    //tempMeterInfo.SaveLifeTime = Convert.ToBoolean(DT.Rows[0][DT.Columns["save_life_time"]]);
                    tempMeterInfo.ReadMBOnMDIReset = Convert.ToBoolean(DT.Rows[0][DT.Columns["read_mb_on_mdi_reset"]]);
                    tempMeterInfo.isPrepaid = Convert.ToBoolean(DT.Rows[0][DT.Columns["is_prepaid"]]);


                    tempMeterInfo.eventsForLiveUpdate_individual_string = DT.Rows[0][DT.Columns["events_liveUpdate_individual"]].ToString();
                    tempMeterInfo.eventsForLiveUpdate_logbook_string = DT.Rows[0][DT.Columns["events_liveUpdate_logbook"]].ToString();


                    tempMeterInfo.read_logbook = Convert.ToBoolean(DT.Rows[0][DT.Columns["read_logbook"]]);

                    tempMeterInfo.read_individual_events_sch = Convert.ToBoolean(DT.Rows[0][DT.Columns["read_individual_events_sch"]]);

                    tempMeterInfo.individual_events_string_sch = DT.Rows[0][DT.Columns["individual_events_string_sch"]].ToString();

                    tempMeterInfo.individual_events_string_alarm = DT.Rows[0][DT.Columns["individual_events_string_alarm"]].ToString();


                    var ref_temp = DT.Rows[0][DT.Columns["reference_no"]].ToString();
                    tempMeterInfo.Reference_no = (string.IsNullOrEmpty(ref_temp)) ? "0000000000000000" : ref_temp;

                    tempMeterInfo.Meter_sim_no = DT.Rows[0][DT.Columns["meter_sim_no"]].ToString();
                    tempMeterInfo.Wakeup_password = DT.Rows[0][DT.Columns["wakeup_password"]].ToString();
                    tempMeterInfo.Wakeup_no1 = DT.Rows[0][DT.Columns["wakeup_no1"]].ToString();
                    tempMeterInfo.Wakeup_no2 = DT.Rows[0][DT.Columns["wakeup_no2"]].ToString();
                    tempMeterInfo.Wakeup_no3 = DT.Rows[0][DT.Columns["wakeup_no3"]].ToString();
                    tempMeterInfo.Wakeup_no4 = DT.Rows[0][DT.Columns["wakeup_no4"]].ToString();
                    tempMeterInfo.Contactor_param_id = Convert.ToInt64(DT.Rows[0][DT.Columns["contactor_param_id"]].ToString());
                    tempMeterInfo.Current_contactor_status = Convert.ToInt32(DT.Rows[0][DT.Columns["current_contactor_status"]]);
                    tempMeterInfo.Write_contactor_param = Convert.ToBoolean(DT.Rows[0][DT.Columns["write_contactor_param"]]);
                    tempMeterInfo.New_meter_password = DT.Rows[0][DT.Columns["new_meter_password"]].ToString();
                    tempMeterInfo.Write_password_flag = Convert.ToBoolean(DT.Rows[0][DT.Columns["write_password_flag"]]);
                    DateTime.TryParse(DT.Rows[0][DT.Columns["new_password_activation_time"]].ToString(), out tempMeterInfo.New_password_activation_time);

                    //=============== Modify on 18/11/2014 add new columns=========================//

                    //tempMeterInfo.Min_cs_difference = Int32.Parse(DT.Rows[0][DT.Columns["min_cs_difference"]].ToString());
                    tempMeterInfo.Max_cs_difference = Int32.Parse(DT.Rows[0][DT.Columns["max_cs_difference"]].ToString());
                    tempMeterInfo.ClockSyncronizationMethod = Byte.Parse(DT.Rows[0][DT.Columns["cs_sync_method"]].ToString());
                    //tempMeterInfo.Max_cs_difference = Int32.Parse(DT.Rows[0][DT.Columns["max_cs_difference"]].ToString());
                    //tempMeterInfo.Max_cs_difference = Int32.Parse(DT.Rows[0][DT.Columns["max_cs_difference"]].ToString());

                    tempMeterInfo.Mdi_reset_date_time = Convert.ToByte(DT.Rows[0][DT.Columns["mdi_reset_date"]]);
                    tempMeterInfo.Write_mdi_reset_date_time = Convert.ToBoolean(DT.Rows[0][DT.Columns["write_mdi_reset_date"]]);
                    tempMeterInfo.Apply_new_contactor_state = Convert.ToBoolean(DT.Rows[0][DT.Columns["apply_new_contactor_state"]]);
                    tempMeterInfo.Contactor_Priority_Sequence = DT.Rows[0][DT.Columns["contactor_priority_seq"]].ToString();
                    //tempMeterInfo.New_contactor_satate = Convert.ToInt32(DT.Rows[0][DT.Columns["new_contactor_state"]].ToString());
                    tempMeterInfo.Contactor_lock = Convert.ToInt32(DT.Rows[0][DT.Columns["contactor_lock"]].ToString());


                    DateTime.TryParse(DT.Rows[0][DT.Columns["last_password_update_time"]].ToString(), out tempMeterInfo.Last_Password_update_time);
                    tempMeterInfo.Default_Password = DT.Rows[0][DT.Columns["default_password"]].ToString();
                    //=============== Modified on 26/11/2014 add new columns=======================//

                    tempMeterInfo.Write_modem_limits_time = Convert.ToBoolean(DT.Rows[0][DT.Columns["write_modem_limits_time"]]);
                    tempMeterInfo.Modem_limits_time_param_id = Convert.ToInt64(DT.Rows[0][DT.Columns["modem_limits_time_id"]]);
                    tempMeterInfo.Write_reference_no = Convert.ToBoolean(DT.Rows[0][DT.Columns["write_reference_no"]]);

                    //=============== Modified on 03/12/2014 add new columns=======================//
                    tempMeterInfo.Write_Number_Profile = Convert.ToInt32(DT.Rows[0][DT.Columns["number_profile_group_id"]]);

                    tempMeterInfo.Write_Display_PowerDown_param = Convert.ToInt64(DT.Rows[0][DT.Columns["display_power_down_id"]]);

                    //tempMeterInfo.TBEPowerFailParamOptions = Convert.ToSByte(DT.Rows[0][DT.Columns["apply_disable_tbe_flag_on_powerfail"]]);
                    tempMeterInfo.TBEPowerFailParamOptions = Convert.ToByte(DT.Rows[0][DT.Columns["apply_disable_tbe_flag_on_powerfail"]].ToString(), 2);

                    tempMeterInfo.WakeUp_Profile_Id = Convert.ToInt32(DT.Rows[0][DT.Columns["set_wakeup_profile_id"]]);


                    tempMeterInfo.ReadDebugError = Convert.ToBoolean(DT.Rows[0][DT.Columns["read_debug_error"]]);
                    tempMeterInfo.WakeUp_Request_ID = Convert.ToInt64(DT.Rows[0][DT.Columns["wakeup_request_id"]]);
                    tempMeterInfo.PrioritizeWakeup = Convert.ToBoolean(DT.Rows[0][DT.Columns["prioritize_wakeup"]]);
                    //_prioritizeWakeUp
                    //=============================================================================// 

                    //Load Profile Channels Request
                    tempMeterInfo.LPParamRequest.ChannelRequestLP1 = Convert.ToBoolean(DT.Rows[0][DT.Columns["lp_write_channel_request"]]);
                    tempMeterInfo.LPParamRequest.ChannelRequestLP2 = Convert.ToBoolean(DT.Rows[0][DT.Columns["lp2_write_channel_request"]]);
                    //try
                    //{
                    //    tempMeterInfo.LPParamRequest.Channel_1 = Convert.ToInt64(DT.Rows[0][DT.Columns["lp_write_channel_1"]]);
                    //    tempMeterInfo.LPParamRequest.Channel_2 = Convert.ToInt64(DT.Rows[0][DT.Columns["lp_write_channel_2"]]);
                    //    tempMeterInfo.LPParamRequest.Channel_3 = Convert.ToInt64(DT.Rows[0][DT.Columns["lp_write_channel_3"]]);
                    //    tempMeterInfo.LPParamRequest.Channel_4 = Convert.ToInt64(DT.Rows[0][DT.Columns["lp_write_channel_4"]]);
                    //}
                    //catch
                    //{ }
                    tempMeterInfo.LPParamRequest.ChangeIntervalRequestLP1 = Convert.ToBoolean(DT.Rows[0][DT.Columns["lp_write_interval_request"]]);
                    tempMeterInfo.LPParamRequest.ChangeIntervalRequestLP2 = Convert.ToBoolean(DT.Rows[0][DT.Columns["lp2_write_interval_request"]]);
                    tempMeterInfo.LPParamRequest.ChangeIntervalRequestLP3 = Convert.ToBoolean(DT.Rows[0][DT.Columns["lp3_write_interval_request"]]);
                    //tempMeterInfo.LPParamRequest.LoadProfilePeriod = TimeSpan.FromMinutes(Convert.ToInt32(DT.Rows[0][DT.Columns["lp_write_interval"]]));


                    //billing detail
                    tempMeterInfo.DetailedBillingID = Convert.ToInt16(DT.Rows[0][DT.Columns["detailed_billing_id"]]);
                    //Read Quantity data is Enable/Disable
                    tempMeterInfo.Read_PQ = Convert.ToBoolean(DT.Rows[0][DT.Columns["read_pq"]]);
                    tempMeterInfo.Read_EV = Convert.ToBoolean(DT.Rows[0][DT.Columns["read_ev"]]);
                    tempMeterInfo.Read_CB = (READ_METHOD)Convert.ToInt16(DT.Rows[0][DT.Columns["read_cb"]]);
                    tempMeterInfo.Read_MB = (READ_METHOD)Convert.ToInt16(DT.Rows[0][DT.Columns["read_mb"]]);
                    tempMeterInfo.Read_LP = (READ_METHOD)Convert.ToInt16(DT.Rows[0][DT.Columns["read_lp"]]);
                    tempMeterInfo.Read_LP2 = (READ_METHOD)Convert.ToInt16(DT.Rows[0][DT.Columns["read_lp2"]]);
                    tempMeterInfo.Read_LP3 = (READ_METHOD)Convert.ToInt16(DT.Rows[0][DT.Columns["read_lp3"]]);

                    tempMeterInfo.Update_Alarm_Register_Live = Convert.ToBoolean(DT.Rows[0][DT.Columns["update_ar_live"]]);
                    tempMeterInfo.SendAlarmsResponse = Convert.ToBoolean(DT.Rows[0][DT.Columns["send_ar_response"]]);


                    tempMeterInfo.Save_Events_On_Major_Alarm = Convert.ToBoolean(DT.Rows[0][DT.Columns["save_events_on_alarm"]]);

                    tempMeterInfo.LP_Counters.ReadInstant = Convert.ToBoolean(DT.Rows[0][DT.Columns["read_instant_lp"]]);
                    tempMeterInfo.LP2_Counters.ReadInstant = Convert.ToBoolean(DT.Rows[0][DT.Columns["read_instant_lp2"]]);
                    tempMeterInfo.LP3_Counters.ReadInstant = Convert.ToBoolean(DT.Rows[0][DT.Columns["read_instant_lp3"]]);

                    tempMeterInfo.Read_AR = Convert.ToBoolean(DT.Rows[0][DT.Columns["read_ar"]]);
                    tempMeterInfo.Read_SS = Convert.ToBoolean(DT.Rows[0][DT.Columns["read_ss"]]);
                    tempMeterInfo.Read_CS = Convert.ToBoolean(DT.Rows[0][DT.Columns["read_cs"]]);
                    tempMeterInfo.Read_RG = Convert.ToBoolean(DT.Rows[0][DT.Columns["read_rg"]]);
                    tempMeterInfo.Read_CO = Convert.ToBoolean(DT.Rows[0][DT.Columns["read_co"]]);

                    //Save Quantity data is Enable/Disable
                    tempMeterInfo.Save_PQ = Convert.ToBoolean(DT.Rows[0][DT.Columns["save_pq"]]);
                    tempMeterInfo.Save_EV = Convert.ToBoolean(DT.Rows[0][DT.Columns["save_ev"]]);
                    tempMeterInfo.Save_CB = Convert.ToBoolean(DT.Rows[0][DT.Columns["save_cb"]]);
                    tempMeterInfo.Save_MB = Convert.ToBoolean(DT.Rows[0][DT.Columns["save_mb"]]);
                    tempMeterInfo.Save_LP = Convert.ToBoolean(DT.Rows[0][DT.Columns["save_lp"]]);
                    tempMeterInfo.Save_LP2 = Convert.ToBoolean(DT.Rows[0][DT.Columns["save_lp2"]]);
                    tempMeterInfo.Save_LP3 = Convert.ToBoolean(DT.Rows[0][DT.Columns["save_lp3"]]);
                    tempMeterInfo.Save_AR = Convert.ToBoolean(DT.Rows[0][DT.Columns["save_ar"]]);
                    tempMeterInfo.Save_ST = Convert.ToBoolean(DT.Rows[0][DT.Columns["save_st"]]);

                    //Super Immediate
                    tempMeterInfo.Schedule_PQ.IsSuperImmediate = Convert.ToBoolean(DT.Rows[0][DT.Columns["super_immediate_pq"]]);
                    tempMeterInfo.Schedule_CB.IsSuperImmediate = Convert.ToBoolean(DT.Rows[0][DT.Columns["super_immediate_cb"]]);
                    tempMeterInfo.Schedule_MB.IsSuperImmediate = Convert.ToBoolean(DT.Rows[0][DT.Columns["super_immediate_mb"]]);
                    tempMeterInfo.Schedule_EV.IsSuperImmediate = Convert.ToBoolean(DT.Rows[0][DT.Columns["super_immediate_ev"]]);
                    tempMeterInfo.Schedule_LP.IsSuperImmediate = Convert.ToBoolean(DT.Rows[0][DT.Columns["super_immediate_lp"]]);
                    tempMeterInfo.Schedule_LP2.IsSuperImmediate = Convert.ToBoolean(DT.Rows[0][DT.Columns["super_immediate_lp2"]]);
                    tempMeterInfo.Schedule_LP3.IsSuperImmediate = Convert.ToBoolean(DT.Rows[0][DT.Columns["super_immediate_lp3"]]);
                    tempMeterInfo.Schedule_SS.IsSuperImmediate = Convert.ToBoolean(DT.Rows[0][DT.Columns["super_immediate_ss"]]);
                    tempMeterInfo.Schedule_CS.IsSuperImmediate = Convert.ToBoolean(DT.Rows[0][DT.Columns["super_immediate_cs"]]);
                    tempMeterInfo.Schedule_RG.IsSuperImmediate = Convert.ToBoolean(DT.Rows[0][DT.Columns["super_immediate_rg"]]);
                    tempMeterInfo.Schedule_CO.IsSuperImmediate = Convert.ToBoolean(DT.Rows[0][DT.Columns["super_immediate_co"]]);

                    //Schedule Type
                    tempMeterInfo.Schedule_PQ.SchType = (ScheduleType)Convert.ToUInt32(DT.Rows[0][DT.Columns["sch_pq"]]);
                    tempMeterInfo.Schedule_EV.SchType = (ScheduleType)Convert.ToUInt32(DT.Rows[0][DT.Columns["sch_ev"]]);
                    tempMeterInfo.Schedule_LP.SchType = (ScheduleType)Convert.ToUInt32(DT.Rows[0][DT.Columns["sch_lp"]]);
                    tempMeterInfo.Schedule_LP2.SchType = (ScheduleType)Convert.ToUInt32(DT.Rows[0][DT.Columns["sch_lp2"]]);
                    tempMeterInfo.Schedule_LP3.SchType = (ScheduleType)Convert.ToUInt32(DT.Rows[0][DT.Columns["sch_lp3"]]);
                    tempMeterInfo.Schedule_CB.SchType = (ScheduleType)Convert.ToUInt32(DT.Rows[0][DT.Columns["sch_cb"]]);
                    tempMeterInfo.Schedule_MB.SchType = (ScheduleType)Convert.ToUInt32(DT.Rows[0][DT.Columns["sch_mb"]]);
                    tempMeterInfo.Schedule_SS.SchType = (ScheduleType)Convert.ToUInt32(DT.Rows[0][DT.Columns["sch_ss"]]);
                    tempMeterInfo.Schedule_CS.SchType = (ScheduleType)Convert.ToUInt32(DT.Rows[0][DT.Columns["sch_cs"]]);
                    tempMeterInfo.Schedule_RG.SchType = (ScheduleType)Convert.ToUInt32(DT.Rows[0][DT.Columns["sch_rg"]]);
                    tempMeterInfo.Schedule_CO.SchType = (ScheduleType)Convert.ToUInt32(DT.Rows[0][DT.Columns["sch_co"]]);
                    tempMeterInfo.SaveSchedule_PQ.SchType = (ScheduleType)Convert.ToUInt32(DT.Rows[0][DT.Columns["save_sch_pq"]]);

                    //Last Read time
                    DateTime.TryParse(DT.Rows[0][DT.Columns["last_lp_time"]].ToString(), out tempMeterInfo.Schedule_LP.LastReadTime);
                    DateTime.TryParse(DT.Rows[0][DT.Columns["last_lp2_time"]].ToString(), out tempMeterInfo.Schedule_LP2.LastReadTime);
                    DateTime.TryParse(DT.Rows[0][DT.Columns["last_lp3_time"]].ToString(), out tempMeterInfo.Schedule_LP3.LastReadTime);
                    DateTime.TryParse(DT.Rows[0][DT.Columns["last_ev_time"]].ToString(), out tempMeterInfo.Schedule_EV.LastReadTime);
                    DateTime.TryParse(DT.Rows[0][DT.Columns["last_pq_time"]].ToString(), out tempMeterInfo.Schedule_PQ.LastReadTime);
                    DateTime.TryParse(DT.Rows[0][DT.Columns["last_cb_time"]].ToString(), out tempMeterInfo.Schedule_CB.LastReadTime);
                    DateTime.TryParse(DT.Rows[0][DT.Columns["last_mb_time"]].ToString(), out tempMeterInfo.Schedule_MB.LastReadTime);
                    DateTime.TryParse(DT.Rows[0][DT.Columns["last_ss_time"]].ToString(), out tempMeterInfo.Schedule_SS.LastReadTime);
                    DateTime.TryParse(DT.Rows[0][DT.Columns["last_cs_time"]].ToString(), out tempMeterInfo.Schedule_CS.LastReadTime);
                    DateTime.TryParse(DT.Rows[0][DT.Columns["last_rgi_time"]].ToString(), out tempMeterInfo.Schedule_RG.LastReadTime);
                    DateTime.TryParse(DT.Rows[0][DT.Columns["last_co_time"]].ToString(), out tempMeterInfo.Schedule_CO.LastReadTime);
                    DateTime.TryParse(DT.Rows[0][DT.Columns["save_last_pq_time"]].ToString(), out tempMeterInfo.SaveSchedule_PQ.LastReadTime);

                    //Base Time
                    DateTime.TryParse(DT.Rows[0][DT.Columns["base_time_lp"]].ToString(), out tempMeterInfo.Schedule_LP.BaseDateTime);
                    DateTime.TryParse(DT.Rows[0][DT.Columns["base_time_lp2"]].ToString(), out tempMeterInfo.Schedule_LP2.BaseDateTime);
                    DateTime.TryParse(DT.Rows[0][DT.Columns["base_time_lp3"]].ToString(), out tempMeterInfo.Schedule_LP3.BaseDateTime);
                    DateTime.TryParse(DT.Rows[0][DT.Columns["base_time_ev"]].ToString(), out tempMeterInfo.Schedule_EV.BaseDateTime);
                    DateTime.TryParse(DT.Rows[0][DT.Columns["base_time_pq"]].ToString(), out tempMeterInfo.Schedule_PQ.BaseDateTime);
                    DateTime.TryParse(DT.Rows[0][DT.Columns["base_time_cb"]].ToString(), out tempMeterInfo.Schedule_CB.BaseDateTime);
                    DateTime.TryParse(DT.Rows[0][DT.Columns["base_time_mb"]].ToString(), out tempMeterInfo.Schedule_MB.BaseDateTime);
                    DateTime.TryParse(DT.Rows[0][DT.Columns["base_time_ss"]].ToString(), out tempMeterInfo.Schedule_SS.BaseDateTime);
                    DateTime.TryParse(DT.Rows[0][DT.Columns["base_time_cs"]].ToString(), out tempMeterInfo.Schedule_CS.BaseDateTime);
                    DateTime.TryParse(DT.Rows[0][DT.Columns["base_time_rg"]].ToString(), out tempMeterInfo.Schedule_RG.BaseDateTime);
                    DateTime.TryParse(DT.Rows[0][DT.Columns["base_time_co"]].ToString(), out tempMeterInfo.Schedule_CO.BaseDateTime);
                    DateTime.TryParse(DT.Rows[0][DT.Columns["save_base_time_pq"]].ToString(), out tempMeterInfo.SaveSchedule_PQ.BaseDateTime);

                    //Interval
                    TimeSpan.TryParse(DT.Rows[0][DT.Columns["interval_lp"]].ToString(), out tempMeterInfo.Schedule_LP.Interval);
                    TimeSpan.TryParse(DT.Rows[0][DT.Columns["interval_lp2"]].ToString(), out tempMeterInfo.Schedule_LP2.Interval);
                    TimeSpan.TryParse(DT.Rows[0][DT.Columns["interval_lp3"]].ToString(), out tempMeterInfo.Schedule_LP3.Interval);
                    TimeSpan.TryParse(DT.Rows[0][DT.Columns["interval_ev"]].ToString(), out tempMeterInfo.Schedule_EV.Interval);
                    TimeSpan.TryParse(DT.Rows[0][DT.Columns["interval_pq"]].ToString(), out tempMeterInfo.Schedule_PQ.Interval);
                    TimeSpan.TryParse(DT.Rows[0][DT.Columns["interval_cb"]].ToString(), out tempMeterInfo.Schedule_CB.Interval);
                    TimeSpan.TryParse(DT.Rows[0][DT.Columns["interval_mb"]].ToString(), out tempMeterInfo.Schedule_MB.Interval);
                    TimeSpan.TryParse(DT.Rows[0][DT.Columns["interval_ss"]].ToString(), out tempMeterInfo.Schedule_SS.Interval);
                    TimeSpan.TryParse(DT.Rows[0][DT.Columns["interval_cs"]].ToString(), out tempMeterInfo.Schedule_CS.Interval);
                    TimeSpan.TryParse(DT.Rows[0][DT.Columns["interval_rg"]].ToString(), out tempMeterInfo.Schedule_RG.Interval);
                    TimeSpan.TryParse(DT.Rows[0][DT.Columns["interval_co"]].ToString(), out tempMeterInfo.Schedule_CO.Interval);
                    TimeSpan.TryParse(DT.Rows[0][DT.Columns["save_interval_pq"]].ToString(), out tempMeterInfo.SaveSchedule_PQ.Interval);


                    //Load Profile Scheme 1
                    #region LOAD PROFILE 1
                    tempMeterInfo.LP_Counters.MaxDifferenceCheck = Convert.ToUInt32(DT.Rows[0][DT.Columns["max_lp_count_diff"]]);
                    tempMeterInfo.LP_Counters.MinDifferenceCheck = Convert.ToUInt32(DT.Rows[0][DT.Columns["min_lp_count_diff"]]);
                    tempMeterInfo.LP_Counters.Max_Size = Convert.ToUInt32(DT.Rows[0][DT.Columns["max_load_profile_entries"]]);
                    tempMeterInfo.LP_Counters.DB_Counter = Convert.ToUInt32(DT.Rows[0][DT.Columns["load_profile_counter"]]);
                    tempMeterInfo.LP_Counters.ChunkSize = Convert.ToUInt32(DT.Rows[0][DT.Columns["lp_chunk_size"]]);
                    tempMeterInfo.LP_Counters.Period = TimeSpan.FromMinutes(Convert.ToUInt32(DT.Rows[0][DT.Columns["lp_write_interval"]]));
                    tempMeterInfo.LP_Counters.LastReadTime = tempMeterInfo.Schedule_LP.LastReadTime;
                    tempMeterInfo.LP_Counters.InvalidUpdate = Convert.ToUInt16(DT.Rows[0][DT.Columns["lp_invalid_update"]]);
                    //tempMeterInfo.LP_Counters.Max_Size = Limits.Max_LoadProfile_Count_Limit; 
                    try
                    {
                        tempMeterInfo.LP_Counters.GroupId = Convert.ToUInt32(CheckDBNull(DT.Rows[0][DT.Columns["load_profile_group_id"]]));
                    }
                    catch
                    { }
                    #endregion
                    //Load_Profile Scheme 2
                    #region LOAD PROFILE 2
                    tempMeterInfo.LP2_Counters.MaxDifferenceCheck = Convert.ToUInt32(DT.Rows[0][DT.Columns["max_lp2_count_diff"]]);
                    tempMeterInfo.LP2_Counters.MinDifferenceCheck = Convert.ToUInt32(DT.Rows[0][DT.Columns["min_lp2_count_diff"]]);
                    tempMeterInfo.LP2_Counters.Max_Size = Convert.ToUInt32(DT.Rows[0][DT.Columns["max_load_profile2_entries"]]);
                    tempMeterInfo.LP2_Counters.DB_Counter = Convert.ToUInt32(DT.Rows[0][DT.Columns["load_profile2_counter"]]);
                    tempMeterInfo.LP2_Counters.ChunkSize = Convert.ToUInt32(DT.Rows[0][DT.Columns["lp2_chunk_size"]]);
                    tempMeterInfo.LP2_Counters.InvalidUpdate = Convert.ToUInt16(DT.Rows[0][DT.Columns["lp2_invalid_update"]]);
                    tempMeterInfo.LP2_Counters.Period = TimeSpan.FromMinutes(Convert.ToUInt32(DT.Rows[0][DT.Columns["lp2_write_interval"]]));
                    tempMeterInfo.LP2_Counters.LastReadTime = tempMeterInfo.Schedule_LP2.LastReadTime;
                    try
                    {
                        tempMeterInfo.LP2_Counters.GroupId = Convert.ToUInt32(CheckDBNull(DT.Rows[0][DT.Columns["load_profile2_group_id"]]));
                    }
                    catch
                    { }
                    #endregion
                    //PQ Load Profile
                    #region LOAD PROFILE 3
                    tempMeterInfo.LP3_Counters.MaxDifferenceCheck = Convert.ToUInt32(DT.Rows[0][DT.Columns["max_lp3_count_diff"]]);
                    tempMeterInfo.LP3_Counters.MinDifferenceCheck = Convert.ToUInt32(DT.Rows[0][DT.Columns["min_lp3_count_diff"]]);
                    tempMeterInfo.LP3_Counters.Max_Size = Convert.ToUInt32(DT.Rows[0][DT.Columns["max_load_profile3_entries"]]);
                    tempMeterInfo.LP3_Counters.DB_Counter = Convert.ToUInt32(DT.Rows[0][DT.Columns["load_profile3_counter"]]);
                    tempMeterInfo.LP3_Counters.ChunkSize = Convert.ToUInt32(DT.Rows[0][DT.Columns["lp3_chunk_size"]]);
                    tempMeterInfo.LP3_Counters.InvalidUpdate = Convert.ToUInt16(DT.Rows[0][DT.Columns["lp3_invalid_update"]]);

                    tempMeterInfo.LP3_Counters.Period = TimeSpan.FromMinutes(Convert.ToUInt32(DT.Rows[0][DT.Columns["lp3_write_interval"]]));
                    tempMeterInfo.LP3_Counters.LastReadTime = tempMeterInfo.Schedule_LP3.LastReadTime;
                    try
                    {
                        tempMeterInfo.LP3_Counters.GroupId = Convert.ToUInt32(CheckDBNull(DT.Rows[0][DT.Columns["load_profile3_group_id"]]));
                    }
                    catch
                    { }
                    #endregion

                    //MB Detail

                    //Schedule(used for only Keep Alive Meters)
                    TimeSpan.TryParse(DT.Rows[0][DT.Columns["default_reset_duration"]].ToString(), out tempMeterInfo.DefaultResetSessionDuration);
                    TimeSpan.TryParse(DT.Rows[0][DT.Columns["kas_interval"]].ToString(), out tempMeterInfo.Kas_Interval);
                    DateTime.TryParse(DT.Rows[0][DT.Columns["kas_due_time"]].ToString(), out tempMeterInfo.Kas_DueTime);

                    tempMeterInfo._ct_num = Convert.ToInt32(DT.Rows[0][DT.Columns["ct_ratio_num"]]);
                    tempMeterInfo._ct_denum = Convert.ToInt32(DT.Rows[0][DT.Columns["ct_ratio_denum"]]);
                    tempMeterInfo._pt_num = Convert.ToInt32(DT.Rows[0][DT.Columns["pt_ratio_num"]]);
                    tempMeterInfo._pt_denum = Convert.ToInt32(DT.Rows[0][DT.Columns["pt_ratio_denum"]]);

                    tempMeterInfo.Limits_Param_Id = Convert.ToInt64(DT.Rows[0][DT.Columns["limits_param_id"]]);
                    tempMeterInfo.MonitoringTime_Param_Id = Convert.ToInt64(DT.Rows[0][DT.Columns["monitoring_time_param_id"]]);
                    tempMeterInfo.CP_PT_Param_Id = Convert.ToInt64(DT.Rows[0][DT.Columns["ct_pt_param_id"]]);
                    tempMeterInfo.DecimalPoints_Param_Id = Convert.ToInt64(DT.Rows[0][DT.Columns["decimal_points_param_id"]]);
                    tempMeterInfo.Energy_Param_Id = Convert.ToInt64(DT.Rows[0][DT.Columns["energy_param_id"]]);
                    tempMeterInfo.IsContactorSupported = Convert.ToBoolean(DT.Rows[0][DT.Columns["is_contactor"]]);

                    // it will extract both ip and mask from the string and assign to both properties NetworkAdress and SubNetMask
                    tempMeterInfo.NetworkIPAndMAsk = Convert.ToString(DT.Rows[0][DT.Columns["network_ip"]]);
                    DateTime.TryParse(DT.Rows[0][DT.Columns["last_contactor_st_time"]].ToString(), out tempMeterInfo.Schedule_CO.LastReadTime);

                    tempMeterInfo.ReadParams = Convert.ToBoolean(DT.Rows[0][DT.Columns["read_params"]]);
                    tempMeterInfo.GetParamsToRead(DT.Rows[0][DT.Columns["read_param_string"]].ToString());

                    if (DT.Columns["type"] != null &&
                        Convert.ToBoolean(DT.Rows[0][DT.Columns["type"]]))
                        tempMeterInfo.MeterType_OBJ = MeterType.KeepAlive;
                    else
                        tempMeterInfo.MeterType_OBJ = MeterType.NonKeepAlive;

                    // Device Type
                    if (DT.Columns["device_type"] != null &&
                        DT.Rows[0][DT.Columns["device_type"]] != DBNull.Value)
                    {
                        byte val = Convert.ToByte(DT.Rows[0][DT.Columns["device_type"]]);
                        tempMeterInfo.DeviceTypeVal = (DeviceType)Enum.ToObject(typeof(DeviceType), val);
                    }
                    string schedule_plan = DT.Rows[0][DT.Columns["schedule_plan"]].ToString();
                    if (tempMeterInfo.DeviceTypeVal == DeviceType.eGenious)
                    {
                        schedule_plan = "2,7";
                    }

                    tempMeterInfo.DefineReadPlan(schedule_plan);


                    // Logical Link Layer Protocol Type
                    if (DT.Columns["llc_protocol_type"] != null &&
                        DT.Rows[0][DT.Columns["llc_protocol_type"]] != DBNull.Value)
                    {
                        byte val = Convert.ToByte(DT.Rows[0][DT.Columns["llc_protocol_type"]]);
                        tempMeterInfo.LLC_Protocol_Type = (LLCProtocolType)Enum.ToObject(typeof(LLCProtocolType), val);
                    }

                    // Gateway Device Scheduler For Sub-Meters Type
                    if (DT.Columns["schedule_type"] != null &&
                        DT.Rows[0][DT.Columns["schedule_type"]] != DBNull.Value)
                    {
                        byte val = Convert.ToByte(DT.Rows[0][DT.Columns["schedule_type"]]);
                        tempMeterInfo.Scheduler_Type = (Scheduler_Type)Enum.ToObject(typeof(Scheduler_Type), val);
                    }

                    // HDLC Protocol Address Field
                    if (DT.Columns["hdlc_Address"] != null &&
                        DT.Rows[0][DT.Columns["hdlc_Address"]] != DBNull.Value)
                    {
                        tempMeterInfo.HDLC_Address = Convert.ToUInt16(DT.Rows[0][DT.Columns["hdlc_Address"]]);
                    }

                    tempMeterInfo.DDS110_Compatible = Convert.ToBoolean(DT.Rows[0][DT.Columns["dds_compatible"]]); //.ToString() by Azeem //v6.0.2.0

                    // total Scheduler Tries Field
                    if (DT.Columns["total_tries"] != null &&
                        DT.Rows[0][DT.Columns["total_tries"]] != DBNull.Value)
                    {
                        tempMeterInfo.total_tries = Convert.ToUInt32(DT.Rows[0][DT.Columns["total_tries"]]);
                    }

                    // total Scheduler Success Field
                    if (DT.Columns["total_success"] != null &&
                        DT.Rows[0][DT.Columns["total_success"]] != DBNull.Value)
                    {
                        tempMeterInfo.total_success = Convert.ToUInt32(DT.Rows[0][DT.Columns["total_success"]]);
                    }


                    #region  Load Shedding Schedule 
                    //int Association_Id = -1;

                    if (DT.Rows[0]["load_shedding_schedule_id"] != DBNull.Value)
                    {
                        tempMeterInfo.Load_Shedding_Schedule_Id = Convert.ToUInt16(DT.Rows[0]["load_shedding_schedule_id"]);
                    }
                    tempMeterInfo.Write_Load_Shedding_Schedule = Convert.ToBoolean(DT.Rows[0][DT.Columns["write_load_shedding_schedule"]]);
                    #endregion


                    #region Consumption Data Flag
                    tempMeterInfo.Write_Consumption_Data_Now = Convert.ToBoolean(DT.Rows[0][DT.Columns["write_consumption_data_now"]]);
                    tempMeterInfo.Write_Consumption_Data_Weekly = Convert.ToBoolean(DT.Rows[0][DT.Columns["write_consumption_data_weekly"]]);
                    tempMeterInfo.Write_Consumption_Data_Monthly = Convert.ToBoolean(DT.Rows[0][DT.Columns["write_consumption_data_monthly"]]);
                    #endregion

                    #region HLS Information

                    #region association_id

                    int Association_Id = -1;

                    if (DT.Columns["association_id"] != null && DT.Rows[0]["association_id"] != DBNull.Value)
                    {
                        Association_Id = Convert.ToUInt16(DT.Rows[0]["association_id"]);
                    }

                    // Null or Default Association_Id Management R326
                    if (Association_Id <= 0)
                        Association_Id = 08;
                    tempMeterInfo.Association_Id = Association_Id;

                    #endregion


                    // if (tempMeterInfo.AuthenticationType >= DLMS.Comm.HLS_Mechanism.HLS_Manufac)
                    {
                        if (tempMeterInfo.ObjSecurityData == null)
                        {
                            tempMeterInfo.ObjSecurityData = new Security_Data();
                        }
                        var objSecurityData = tempMeterInfo.ObjSecurityData;

                        // Authentication KEY
                        if (DT.Columns["authentication_key"] != null && DT.Rows[0]["authentication_key"] != DBNull.Value)
                            objSecurityData.AuthenticationKey = new Key(DLMS_Common.String_to_Hex_array(DT.Rows[0]["authentication_key"].ToString().PadLeft(32, '0')),
                               DLMS.Comm.KEY_ID.AuthenticationKey);
                        else
                            objSecurityData.AuthenticationKey = null;

                        // Encryption KEY
                        if (DT.Columns["encryption_key"] != null && DT.Rows[0]["encryption_key"] != DBNull.Value)
                            objSecurityData.EncryptionKey = new Key(DLMS_Common.String_to_Hex_array(DT.Rows[0]["encryption_key"].ToString().PadLeft(32, '0')), Convert.ToUInt32(DT.Rows[0]["encryption_counter"].ToString()) + 1, /// Convert.ToUInt32(tbl.Rows[0]["encryption_counter"].ToString()), 
                                                                    0, DLMS.Comm.KEY_ID.GLOBAL_Unicast_EncryptionKey);
                        else
                            objSecurityData.EncryptionKey = null;

                        // Master Encryption KEY
                        if (DT.Columns["master_key"] != null && DT.Rows[0]["master_key"] != DBNull.Value)
                            objSecurityData.MasterEncryptionKey = new Key(DLMS_Common.String_to_Hex_array(DT.Rows[0]["master_key"].ToString().PadLeft(32, '0')),
                                DLMS.Comm.KEY_ID.MasterKey);
                        else
                            objSecurityData.MasterEncryptionKey = null;

                        objSecurityData.SecurityControl = (SecurityControl)(Convert.ToInt32(DT.Rows[0]["security_policy"].ToString()) * 0x10);

                        tempMeterInfo.ObjSecurityData = objSecurityData;
                    }

                    #endregion

                    #region Customer ID
                    long feederId = tempMeterInfo.MeterID;
                    if (tempMeterInfo.DeviceTypeVal == DeviceType.eGenious && tempMeterInfo.ParentID > 0)
                    {
                        feederId = tempMeterInfo.ParentID;
                    }
                    query = string.Format("select `customer_id`,`is_dlink`,`dlink_customer_id` from connection where feeder_id ={0}", feederId);
                    Command = new OdbcCommand(query, Connection);
                    DT = GetTableFromDB(Command, DT);
                    long CustomerID = 0;
                    if (DT.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(DT.Rows[0]["is_dlink"] ?? "0") != 0)
                            Int64.TryParse(DT.Rows[0]["dlink_customer_id"].ToString(), out CustomerID);
                        else
                            Int64.TryParse(DT.Rows[0]["customer_id"].ToString(), out CustomerID);
                    }

                    if (CustomerID == 0) tempMeterInfo.Customer_ID = "Null";
                    else tempMeterInfo.Customer_ID = CustomerID.ToString();
                    #endregion

                    #region Data From 'Consumers' Table
                    query = $"SELECT cost_per_kwh from consumers where consumer_id = '{CustomerID}'";
                    Command = new OdbcCommand(query, Connection);
                    DT = GetTableFromDB(Command, DT);
                    double costPerKwh = 0;
                    if (DT.Rows.Count > 0)
                    {
                        if (DT.Rows[0]["cost_per_kwh"] != DBNull.Value)
                            Double.TryParse(DT.Rows[0]["cost_per_kwh"].ToString(), out costPerKwh);
                        else
                            throw new Exception("Error in Conversion : [cost_per_kwh] is Null in Consumers Table.");
                    }
                    tempMeterInfo.CostPerKwh = costPerKwh;


                    #endregion

                }
                else
                {
                    tempMeterInfo.MeterType_OBJ = MeterType.Intruder;
                }

                return tempMeterInfo;
            }
            catch (Exception ex)
            {
                throw ex; // report error
            }
            finally
            {
                try
                {
                    _DBConnect.CloseConnection();
                    DT.Clear();
                    DT.Dispose();
                    Command.Dispose();
                    query = null;
                }
                catch
                { }
            }
        }

        public object CheckDBNull(object a)
        {
            try
            {
                if (!DBNull.Value.Equals(a))
                    return a;
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool UpdateMeterSettings(MeterInformation MI, MeterInfoUpdateFlags MIUF)
        {
            try
            {
                DBConnect.OpenConnection();
                var QueryToUpdate = MakeQuery(MI, MIUF);
                if (string.IsNullOrEmpty(QueryToUpdate)) return true;
                if (ExecuteQuery(QueryToUpdate))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;//report error
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public string MakeQuery(MeterInformation MI, MeterInfoUpdateFlags MIUF)
        {
            string tempQuery = "";

            try
            {
                string BaseTime_COValue = (MIUF.base_time_CO) ? String.Format(", base_time_co = '{0}'", MI.Schedule_CO.BaseDateTime.ToString(DateFormat)) : String.Empty;
                string BaseTime_RGValue = (MIUF.base_time_RG) ? String.Format(", base_time_rg = '{0}'", MI.Schedule_RG.BaseDateTime.ToString(DateFormat)) : String.Empty;
                string BaseTime_CBValue = (MIUF.base_time_CB) ? String.Format(", base_time_cb = '{0}'", MI.Schedule_CB.BaseDateTime.ToString(DateFormat)) : String.Empty;
                string BaseTime_CSValue = (MIUF.base_time_CS) ? String.Format(", base_time_cs = '{0}'", MI.Schedule_CS.BaseDateTime.ToString(DateFormat)) : String.Empty;
                string BaseTime_EVValue = (MIUF.base_time_EV) ? String.Format(", base_time_ev = '{0}'", MI.Schedule_EV.BaseDateTime.ToString(DateFormat)) : String.Empty;
                string BaseTime_LPValue = (MI.Schedule_LP.UpdateBaseTime) ? String.Format(", base_time_lp = '{0}'", MI.Schedule_LP.BaseDateTime.ToString(DateFormat)) : String.Empty;
                string BaseTime_LP2Value = (MI.Schedule_LP2.UpdateBaseTime) ? String.Format(", base_time_lp2 = '{0}'", MI.Schedule_LP2.BaseDateTime.ToString(DateFormat)) : String.Empty;
                string BaseTime_LP3Value = (MI.Schedule_LP3.UpdateBaseTime) ? String.Format(", base_time_lp3 = '{0}'", MI.Schedule_LP3.BaseDateTime.ToString(DateFormat)) : String.Empty;
                string BaseTime_MBValue = (MIUF.base_time_MB) ? String.Format(", base_time_mb = '{0}'", MI.Schedule_MB.BaseDateTime.ToString(DateFormat)) : String.Empty;
                string BaseTime_PQValue = (MIUF.base_time_PQ) ? String.Format(", base_time_pq = '{0}'", MI.Schedule_PQ.BaseDateTime.ToString(DateFormat)) : String.Empty;
                string BaseTime_SSValue = (MIUF.base_time_SS) ? String.Format(", base_time_ss = '{0}'", MI.Schedule_SS.BaseDateTime.ToString(DateFormat)) : String.Empty;
                string BaseTime_Save_PQValue = (MIUF.base_time_Save_PQ) ? String.Format(", save_base_time_pq = '{0}'", MI.SaveSchedule_PQ.BaseDateTime.ToString(DateFormat)) : String.Empty;

                string LastTime_RGValue = (MIUF.last_RG_time) ? String.Format(", last_rgi_time = '{0}'", MI.Schedule_RG.LastReadTime.ToString(DateFormat)) : String.Empty;
                string LastTime_CBValue = (MIUF.last_CB_time) ? String.Format(", last_cb_time = '{0}'", MI.Schedule_CB.LastReadTime.ToString(DateFormat)) : String.Empty;
                string LastTime_CSValue = (MIUF.last_CS_time) ? String.Format(", last_cs_time = '{0}'", MI.Schedule_CS.LastReadTime.ToString(DateFormat)) : String.Empty;
                string LastTime_EVValue = (MIUF.last_EV_time) ? String.Format(", last_ev_time = '{0}'", MI.Schedule_EV.LastReadTime.ToString(DateFormat)) : String.Empty;
                string LastTime_LPValue = (MI.Schedule_LP.UpdateLastReadTime) ? String.Format(", last_lp_time = '{0}'", MI.LP_Counters.LastReadTime.ToString(DateFormat)) : String.Empty;
                string LastTime_LP2Value = (MI.Schedule_LP2.UpdateLastReadTime) ? String.Format(", last_lp2_time = '{0}'", MI.LP2_Counters.LastReadTime.ToString(DateFormat)) : String.Empty;
                string LastTime_LP3Value = (MI.Schedule_LP3.UpdateLastReadTime) ? String.Format(", last_lp3_time = '{0}'", MI.LP3_Counters.LastReadTime.ToString(DateFormat)) : String.Empty;
                string LastTime_MBValue = (MIUF.last_MB_time) ? String.Format(", last_mb_time = '{0}'", MI.Schedule_MB.LastReadTime.ToString(DateFormat)) : String.Empty;
                string LastTime_PQValue = (MIUF.last_PQ_time) ? String.Format(", last_pq_time = '{0}'", MI.Schedule_PQ.LastReadTime.ToString(DateFormat)) : String.Empty;
                string LastTime_SSValue = (MIUF.last_SS_time) ? String.Format(", last_ss_time = '{0}'", MI.Schedule_SS.LastReadTime.ToString(DateFormat)) : String.Empty;
                string LastTime_Save_PQValue = (MIUF.last_Save_PQ_time) ? String.Format(", save_last_pq_time = '{0}'", MI.SaveSchedule_PQ.LastReadTime.ToString(DateFormat)) : String.Empty;

                string SchType_COValue = (MIUF.Schedule_CO) ? String.Format(", sch_co = '{0}'", (int)MI.Schedule_CO.SchType) : String.Empty;
                string SchType_RGValue = (MIUF.Schedule_RG) ? String.Format(", sch_rg = '{0}'", (int)MI.Schedule_RG.SchType) : String.Empty;
                string SchType_CBValue = (MIUF.Schedule_CB) ? String.Format(", sch_cb = '{0}'", (int)MI.Schedule_CB.SchType) : String.Empty;
                string SchType_CSValue = (MIUF.Schedule_CS) ? String.Format(", sch_cs = '{0}'", (int)MI.Schedule_CS.SchType) : String.Empty;
                string SchType_EVValue = (MIUF.Schedule_EV) ? String.Format(", sch_ev = '{0}'", (int)MI.Schedule_EV.SchType) : String.Empty;
                string SchType_LPValue = (MI.Schedule_LP.UpdateScheduleType) ? String.Format(", sch_lp = '{0}'", (int)MI.Schedule_LP.SchType) : String.Empty;
                string SchType_LP2Value = (MI.Schedule_LP2.UpdateScheduleType) ? String.Format(", sch_lp2 = '{0}'", (int)MI.Schedule_LP2.SchType) : String.Empty;
                string SchType_LP3Value = (MI.Schedule_LP3.UpdateScheduleType) ? String.Format(", sch_lp3 = '{0}'", (int)MI.Schedule_LP2.SchType) : String.Empty;
                string SchType_MBValue = (MIUF.Schedule_MB) ? String.Format(", sch_mb = '{0}'", (int)MI.Schedule_MB.SchType) : String.Empty;
                string SchType_PQValue = (MIUF.Schedule_PQ) ? String.Format(", sch_pq = '{0}'", (int)MI.Schedule_PQ.SchType) : String.Empty;
                string SchType_SSValue = (MIUF.Schedule_SS) ? String.Format(", sch_ss = '{0}'", (int)MI.Schedule_SS.SchType) : String.Empty;
                string SchType_Save_PQValue = (MIUF.SaveSchedule_PQ) ? String.Format(", save_sch_pq = '{0}'", (int)MI.SaveSchedule_PQ.SchType) : String.Empty;

                string ReadInstant_LP = (MI.LP_Counters.UpdateInstantFlag) ? String.Format(", read_instant_lp = '{0}'", Convert.ToByte(MI.LP_Counters.ReadInstant)) : String.Empty;
                string ReadInstant_LP2 = (MI.LP2_Counters.UpdateInstantFlag) ? String.Format(", read_instant_lp2 = '{0}'", Convert.ToByte(MI.LP2_Counters.ReadInstant)) : String.Empty;
                string ReadInstant_LP3 = (MI.LP3_Counters.UpdateInstantFlag) ? String.Format(", read_instant_lp3 = '{0}'", Convert.ToByte(MI.LP3_Counters.ReadInstant)) : String.Empty;


                string IsSuperImmediate_COValue = (MIUF.SuperImmediate_CO) ? String.Format(", super_immediate_CO = '{0}'", Convert.ToInt32(MI.Schedule_CO.IsSuperImmediate)) : String.Empty;
                string IsSuperImmediate_RGValue = (MIUF.SuperImmediate_RG) ? String.Format(", super_immediate_rg = '{0}'", Convert.ToInt32(MI.Schedule_RG.IsSuperImmediate)) : String.Empty;
                string IsSuperImmediate_CBValue = (MIUF.SuperImmediate_CB) ? String.Format(", super_immediate_cb = '{0}'", Convert.ToInt32(MI.Schedule_CB.IsSuperImmediate)) : String.Empty;
                string IsSuperImmediate_CSValue = (MIUF.SuperImmediate_CS) ? String.Format(", super_immediate_cs = '{0}'", Convert.ToInt32(MI.Schedule_CS.IsSuperImmediate)) : String.Empty;
                string IsSuperImmediate_EVValue = (MIUF.SuperImmediate_EV) ? String.Format(", super_immediate_ev = '{0}'", Convert.ToInt32(MI.Schedule_EV.IsSuperImmediate)) : String.Empty;
                string IsSuperImmediate_LPValue = (MI.Schedule_LP.UpdateIsSuperImmediate) ? String.Format(", super_immediate_lp = '{0}'", Convert.ToInt32(MI.Schedule_LP.IsSuperImmediate)) : String.Empty;
                string IsSuperImmediate_LP2Value = (MI.Schedule_LP2.UpdateIsSuperImmediate) ? String.Format(", super_immediate_lp2 = '{0}'", Convert.ToInt32(MI.Schedule_LP2.IsSuperImmediate)) : String.Empty;
                string IsSuperImmediate_LP3Value = (MI.Schedule_LP3.UpdateIsSuperImmediate) ? String.Format(", super_immediate_lp3 = '{0}'", Convert.ToInt32(MI.Schedule_LP3.IsSuperImmediate)) : String.Empty;
                string IsSuperImmediate_MBValue = (MIUF.SuperImmediate_MB) ? String.Format(", super_immediate_mb = '{0}'", Convert.ToInt32(MI.Schedule_MB.IsSuperImmediate)) : String.Empty;
                string IsSuperImmediate_PQValue = (MIUF.SuperImmediate_PQ) ? String.Format(", super_immediate_pq = '{0}'", Convert.ToInt32(MI.Schedule_PQ.IsSuperImmediate)) : String.Empty;
                string IsSuperImmediate_SSValue = (MIUF.SuperImmediate_SS) ? String.Format(", super_immediate_ss = '{0}'", Convert.ToInt32(MI.Schedule_SS.IsSuperImmediate)) : String.Empty;
                string IsSuperImmediate_Save_PQValue = (MIUF.SuperImmediate_Save_PQ) ? String.Format(", save_super_immediate_pq = '{0}'", Convert.ToInt32(MI.SaveSchedule_PQ.IsSuperImmediate)) : String.Empty;

                string KAS_DueTimeValue = string.Empty;

                if (MIUF.KAS_DueTime)
                {
                    KAS_DueTimeValue = String.Format(", `kas_due_time` = '{0}'", MI.Kas_DueTime.ToString(DateFormat));

                }
                string TotalRetries = MIUF.IsTotal_RetriesWrite ? String.Format(", `total_tries` ={0} , `total_success` ={1} ", MI.total_tries, MI.total_success) : string.Empty;

                string Unset_MajorAlarmValue = (MIUF.UnsetMajorAlarms) ? String.Format(", unset_ma = '{0}'", Convert.ToInt32(MI.Unset_MajorAlarms)) : String.Empty;
                string LPParamRequestValue = (MIUF.LP1_ChannelsWriteRequest) ? String.Format(", lp_write_channel_request = '{0}'", Convert.ToInt32(MI.LPParamRequest.ChannelRequestLP1)) : String.Empty;
                string LPIntervalChangeRequestValue = (MIUF.LP1_IntervalWriteRequest) ? String.Format(", lp_write_interval_request = '{0}'", Convert.ToInt32(MI.LPParamRequest.ChangeIntervalRequestLP1)) : String.Empty;

                string LP2_ParamRequestValue = (MIUF.LP2_ChannelsWriteRequest) ? String.Format(", lp2_write_channel_request = '{0}'", Convert.ToInt32(MI.LPParamRequest.ChannelRequestLP2)) : String.Empty;
                string LP2_IntervalChangeRequestValue = (MIUF.LP2_IntervalWriteRequest) ? String.Format(", lp2_write_interval_request = '{0}'", Convert.ToInt32(MI.LPParamRequest.ChangeIntervalRequestLP2)) : String.Empty;

                string LP3_IntervalChangeRequestValue = (MIUF.LP3_IntervalWriteRequest) ? String.Format(", lp3_write_interval_request = '{0}'", Convert.ToInt32(MI.LPParamRequest.ChangeIntervalRequestLP3)) : String.Empty;


                string SetKeepAliveValue = (MIUF.SetKeepAlive) ? String.Format(", set_keepalive = '{0}'", MI.SetKeepAlive) : String.Empty;
                string SetIPProfileValue = (MIUF.SetIpProfile) ? String.Format(", set_ip_profiles = '{0}'", MI.SetIPProfile) : String.Empty;

                string SetModemInitializeBasicValue = (MIUF.SetModemInitializeBasics) ? String.Format(", set_modem_initialize_basic = '{0}'", MI.SetModemInitializeBasics) : String.Empty;
                string SetModemInitializeExtendedValue = (MIUF.SetModemInitializeExtended) ? String.Format(", set_modem_initialize_extended = '{0}'", MI.SetModemInitializeExtended) : String.Empty;

                string SetMajorAlarmGroupIDValue = (MIUF.MajorAlarmGroupID) ? String.Format(", major_alarm_group_id = '{0}'", MI.MajorAlarmGroupID) : String.Empty;
                string TBE1WriteRequestValue = (MIUF.TBE1WriteRequestID) ? String.Format(", tbe1_write_request_id = '{0}'", MI.TBE1WriteRequestID) : String.Empty;
                string TBE2WriteRequestValue = (MIUF.TBE2WriteRequestID) ? String.Format(", tbe2_write_request_id = '{0}'", MI.TBE2WriteRequestID) : String.Empty;

                string dw_normal = (MIUF.DW_NormalID) ? String.Format(", dw_normal_mode_id = '{0}'", MI.DW_NormalID) : String.Empty;
                string dw_alternate = (MIUF.DW_AlternateID) ? String.Format(", dw_alternate_mode_id = '{0}'", MI.DW_AlternateID) : String.Empty;

                string current_pass = (MIUF.IsPasswordTemporary) ? String.Format(", password = '{0}'", MI.New_meter_password) : String.Empty;
                string write_password_flag = (MIUF.IsDefaultPassWordActive || MIUF.IsPasswordInvalid) ? String.Format(", write_password_flag = '{0}'", 0) : String.Empty;

                string updateWakeRequestID = (MI.WakeUp_Request_ID > 0) ? String.Format(", wakeup_request_id = '{0}'", 0) : String.Empty;
                string previous_pass;               //= (MIUF.IsDefaultPassWordActive) ? String.Format(", password = '{0}'", MI.Default_Password) : String.Empty;
                // = (MIUF.IsDefaultPassWordActive) ? String.Format(", last_password_update_time = '{0}'", DateTime.Now.ToString(DateFormat)) : String.Empty;
                string last_password_update_time = (MIUF.IsPasswordTemporary || MIUF.IsDefaultPassWordActive) ? String.Format(", last_password_update_time = '{0}'", DateTime.Now.ToString(DateFormat)) : string.Empty;

                if (MIUF.IsDefaultPassWordActive)
                {
                    previous_pass = String.Format(", password = '{0}'", MI.Default_Password);

                }
                else
                {
                    previous_pass = string.Empty;
                    last_password_update_time = string.Empty;
                }
                string mbCounter = (MIUF.UpdateMBCounter && !MIUF.IsDisableMB) ? string.Format(", monthly_billing_counter = '{0}'", MI.MonthlyBillingCounterToUpdate) : string.Empty;
                string eventCounter = (MIUF.UpdateEventCounters && !MIUF.IsDisableEv) ? string.Format(", event_counter= '{0}'", MI.EvCounterToUpdate) : string.Empty;
                string loadProfileCounter = (MI.LP_Counters.UpdateCounter && !MI.Schedule_LP.IsDisable) ? string.Format(", load_profile_counter = '{0}'", MI.LP_Counters.CounterToUpdate) : string.Empty;
                string loadProfile2Counter = (MI.LP2_Counters.UpdateCounter && !MI.Schedule_LP2.IsDisable) ? string.Format(", load_profile2_counter = '{0}'", MI.LP2_Counters.CounterToUpdate) : string.Empty;
                string loadProfile3Counter = (MI.LP3_Counters.UpdateCounter && !MI.Schedule_LP3.IsDisable) ? string.Format(", load_profile3_counter = '{0}'", MI.LP3_Counters.CounterToUpdate) : string.Empty;
                //string loadProfileCounter = (MIUF.UpdateLoadProfileCounter) ? string.Format(", load_profile_counter = '{0}'", MI.LPCounterToUpdate) : string.Empty;
                string mdi_reset_date = (MIUF.IsMDIDateReset) ? String.Format(", write_mdi_reset_date = '{0}'", 0) : String.Empty;

                string current_con_state = (MIUF.IsContactorStatusUpdate) ? String.Format(", current_contactor_status = '{0}', last_contactor_st_time='{1}'", MI.Current_contactor_status, MI.Schedule_CO.LastReadTime.ToString(DateFormat)) : String.Empty;
                string lock_contactor = (MIUF.AquireContactorLock) ? String.Format(", contactor_lock = '1'") : String.Empty;

                string NoContactorReqPending = (MIUF.NoContactorRequestPending) ? String.Format(", apply_new_contactor_state = 0") : String.Empty;
                string write_contactor_params = (MIUF.IsContactorParamsWrite) ? String.Format(", write_contactor_param = '{0}'", 0) : String.Empty;
                string write_modem_limits = (MIUF.IsModemLimitsTimeWrite) ? String.Format(", write_modem_limits_time = '{0}'", 0) : String.Empty;
                string write_reference_no = (MIUF.IsCReferenceWrite) ? String.Format(", write_reference_no = '{0}'", 0) : String.Empty;
                string Write_Number_Profile = (MIUF.IsNumberProFileWrite) ? String.Format(", number_profile_group_id = '{0}'", 0) : String.Empty;
                string Write_Display_Down_Params = (MIUF.IsDisplayPowerDownWrite) ? String.Format(", display_power_down_id = '{0}'", 0) : String.Empty;
                string Write_TBE_Disable_on_PowerFail = (MIUF.ISDisableTBEOnPowerFailWrite) ? String.Format(", apply_disable_tbe_flag_on_powerfail='0000'") : string.Empty;
                string Write_WakeUp_Profile = (MIUF.IsWakeUpProfileWrite) ? String.Format(", set_wakeup_profile_id = '{0}'", 0) : String.Empty;

                string DisableLB = (MI.Schedule_LP.IsDisable) ? String.Format(", read_lp = '{0}'", 0) : String.Empty;
                string DisableLP2 = (MI.Schedule_LP2.IsDisable) ? String.Format(", read_lp2 = '{0}'", 0) : String.Empty;
                string DisableLP3 = (MI.Schedule_LP3.IsDisable) ? String.Format(", read_lp3 = '{0}'", 0) : String.Empty;
                string DisableEV = (MIUF.IsDisableEv) ? String.Format(", read_logbook = '{0}'", 0) : String.Empty;
                string DisableCS = (MIUF.IsDisableCS) ? String.Format(", read_cs = '{0}'", 0) : String.Empty;
                string DisableMB = (MIUF.IsDisableMB) ? String.Format(", read_mb = '{0}'", 0) : String.Empty;
                string wakeupRequestID = (MI.WakeUp_Request_ID != 0) ? String.Format(", wakeup_request_id = 0") : String.Empty;

                string LimitsParams = (MIUF.IsLimitsWrite) ? String.Format(", limits_param_id = '{0}'", 0) : String.Empty;
                string MTParams = (MIUF.IsMonitoringTimeWrite) ? String.Format(", monitoring_time_param_id = '{0}'", 0) : String.Empty;
                string CTPTParams = (MIUF.IsCTPTWrite) ? String.Format(", ct_pt_param_id = '{0}'", 0) : String.Empty;
                string DecimalParams = (MIUF.IsDecimalPointsWrite) ? String.Format(", decimal_points_param_id = '{0}'", 0) : String.Empty;
                string EnergyParam = (MIUF.IsEnergyParamsWrite) ? String.Format(", energy_param_id = '{0}'", 0) : String.Empty;
                string ReadParam = (MI.ReadParams) ? String.Format(", `read_params` = '{0}'", 0) : String.Empty;
                string EncryptionCounter = (MI.ObjSecurityData.IsInitialized) ? String.Format(", `encryption_counter` = '{0}'", MI.ObjSecurityData.EncryptionKey.EncryptionCounter) : String.Empty;
                string ProcessedByGateway = MI.SubMeterProcessedByGateway ? string.Format(", `processed_by_gateway`=1") : string.Empty;
                string LoadSheddingSchedule = MIUF.IsLoadSheddingScheduleWrite ? string.Format(", `write_load_shedding_schedule`=0") : string.Empty;
                //string IsConsumptionDataNow = MIUF.IsConsumptionDataNowWrite ? string.Format(", `write_consumption_data_now`=0") : string.Empty;
                string IsConsumptionDataWeekly = MIUF.IsConsumptionDataWeeklyWrite ? string.Format(", `write_consumption_data_weekly`=0") : string.Empty;
                string IsConsumptionDataMonthly = MIUF.IsConsumptionDataMonthlyWrite ? string.Format(", `write_consumption_data_monthly`=0") : string.Empty;

                tempQuery = // String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}{37}{38}{39}{40}{41}{42}{43}{44}{45}{46}{47}{48}{49}{50}{51}{52}{53}{54}{55}{56}{57}{58}{59}{60}{61}{62}{63}{64}{65}{66}{67}{68}{69}{70}{71}{72}{73}{74}{75}{76}{77}{78}{79}{80}{81}{82}{83}{84}{85}{86}{87}{88}{89}{90}{91}{92}{93}{94}{95}",
                    BaseTime_COValue + BaseTime_RGValue + BaseTime_CBValue + BaseTime_CSValue + BaseTime_EVValue + BaseTime_LPValue + BaseTime_MBValue + BaseTime_PQValue + BaseTime_SSValue + BaseTime_Save_PQValue +
                    LastTime_RGValue + LastTime_CBValue + LastTime_CSValue + LastTime_EVValue + LastTime_LPValue + LastTime_MBValue + LastTime_PQValue + LastTime_SSValue + LastTime_Save_PQValue +
                    SchType_COValue + SchType_RGValue + SchType_CBValue + SchType_CSValue + SchType_EVValue + SchType_LPValue + SchType_MBValue + SchType_PQValue + SchType_SSValue + SchType_Save_PQValue +
                    IsSuperImmediate_COValue + IsSuperImmediate_RGValue + IsSuperImmediate_CBValue + IsSuperImmediate_CSValue + IsSuperImmediate_EVValue + IsSuperImmediate_LPValue + IsSuperImmediate_MBValue + IsSuperImmediate_PQValue + IsSuperImmediate_SSValue + IsSuperImmediate_Save_PQValue +
                    KAS_DueTimeValue + Unset_MajorAlarmValue + LPParamRequestValue + LPIntervalChangeRequestValue + LP2_ParamRequestValue + LP2_IntervalChangeRequestValue + LP3_IntervalChangeRequestValue + SetKeepAliveValue + SetIPProfileValue + SetModemInitializeBasicValue +
                    SetModemInitializeExtendedValue + SetMajorAlarmGroupIDValue + TBE1WriteRequestValue + TBE2WriteRequestValue + dw_normal + dw_alternate + current_pass + previous_pass + write_password_flag + last_password_update_time + mdi_reset_date +
                    current_con_state + write_contactor_params + write_modem_limits + write_reference_no + Write_Number_Profile + Write_Display_Down_Params + Write_TBE_Disable_on_PowerFail + loadProfileCounter + eventCounter + mbCounter +
                    Write_WakeUp_Profile + DisableLB + DisableEV + DisableCS + DisableMB + wakeupRequestID + LimitsParams + MTParams + CTPTParams + DecimalParams + EnergyParam + updateWakeRequestID + ReadParam + NoContactorReqPending + lock_contactor + EncryptionCounter +
                    BaseTime_LP2Value + LastTime_LP2Value + DisableLP2 + loadProfile2Counter + IsSuperImmediate_LP2Value + SchType_LP2Value + BaseTime_LP3Value + LastTime_LP3Value + DisableLP3 + loadProfile3Counter + IsSuperImmediate_LP3Value + SchType_LP3Value +
                    ReadInstant_LP + ReadInstant_LP2 + ReadInstant_LP3 + ProcessedByGateway + TotalRetries + LoadSheddingSchedule;// + IsConsumptionDataNow + IsConsumptionDataWeekly + IsConsumptionDataMonthly;


                if (!string.IsNullOrEmpty(tempQuery))
                {
                    var finalQuery = string.Format("UPDATE meter SET {0} WHERE feeder_id ='{1}'", tempQuery.Substring(1), MI.MeterID);
                    return finalQuery;
                }
                else
                {
                    return string.Empty;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        // public bool UpdateMeterSettings_Old(MeterInformation MI)
        // {

        //     if (!_DBConnect.IsConnectionOpen)
        //         _DBConnect.OpenConnection();
        //     try
        //     {
        //         string query = String.Format("UPDATE `meter` SET `tbe1_write_request_id`='{0}', `tbe2_write_request_id`='{1}',`last_pq_time`='{2}',`last_cb_time`='{3}',`last_mb_time`='{4}',`last_ev_time`='{5}', `last_lp_time`='{6}', `last_ss_time`='{7}', `last_cs_time`='{8}', `save_last_pq_time`='{9}', "
        //             + "`unset_ma`='{10}', `sch_pq`='{11}', `base_time_pq`='{12}', `sch_cb`='{13}', `base_time_cb`='{14}', `sch_mb`='{15}', `base_time_mb`='{16}', `sch_ev`='{17}',`base_time_ev`='{18}', `sch_lp`='{19}',`base_time_lp`='{20}', `sch_ss`='{21}',`base_time_ss`='{22}', `sch_cs`='{23}',`base_time_cs`='{24}', `save_sch_pq`='{25}', `save_base_time_pq`='{26}', "
        //             + "`super_immediate_pq`='{27}', `super_immediate_cb`='{28}', `super_immediate_mb`='{29}', `super_immediate_ev`='{30}', `super_immediate_lp`='{31}', `super_immediate_ss`='{32}', `super_immediate_cs`='{33}', "
        //             + "`set_ip_profiles`='{34}', `set_modem_initialize_basic`='{35}', `set_modem_initialize_extended`='{36}', `set_keepalive`='{37}', `major_alarm_group_id` = '{38}', `kas_due_time` = '{39}', lp_write_channel_request = '{40}', lp_write_interval_request = '{41}' WHERE msn = '{42}'"
        //             , MI.TBE1WriteRequestID
        //             , MI.TBE2WriteRequestID
        //             , MI.Schedule_PQ.LastReadTime.ToString(DateFormat)
        //             , MI.Schedule_CB.LastReadTime.ToString(DateFormat)
        //             , MI.Schedule_MB.LastReadTime.ToString(DateFormat)
        //             , MI.Schedule_EV.LastReadTime.ToString(DateFormat)
        //             , MI.Schedule_LP.LastReadTime.ToString(DateFormat)
        //             , MI.Schedule_SS.LastReadTime.ToString(DateFormat)
        //             , MI.Schedule_CS.LastReadTime.ToString(DateFormat)
        //             , MI.SaveSchedule_PQ.LastReadTime.ToString(DateFormat)
        //             , Convert.ToInt32(MI.Unset_MajorAlarms)
        //             , (int)MI.Schedule_PQ.SchType, MI.Schedule_PQ.BaseDateTime.ToString(DateFormat)
        //             , (int)MI.Schedule_CB.SchType, MI.Schedule_CB.BaseDateTime.ToString(DateFormat)
        //             , (int)MI.Schedule_MB.SchType, MI.Schedule_MB.BaseDateTime.ToString(DateFormat)
        //             , (int)MI.Schedule_EV.SchType, MI.Schedule_EV.BaseDateTime.ToString(DateFormat)
        //             , (int)MI.Schedule_LP.SchType, MI.Schedule_LP.BaseDateTime.ToString(DateFormat)
        //             , (int)MI.Schedule_SS.SchType, MI.Schedule_SS.BaseDateTime.ToString(DateFormat)
        //             , (int)MI.Schedule_CS.SchType, MI.Schedule_CS.BaseDateTime.ToString(DateFormat)
        //             , (int)MI.SaveSchedule_PQ.SchType, MI.SaveSchedule_PQ.BaseDateTime.ToString(DateFormat)
        //             , Convert.ToInt32(MI.Schedule_PQ.IsSuperImmediate)
        //             , Convert.ToInt32(MI.Schedule_CB.IsSuperImmediate)
        //             , Convert.ToInt32(MI.Schedule_MB.IsSuperImmediate)
        //             , Convert.ToInt32(MI.Schedule_EV.IsSuperImmediate)
        //             , Convert.ToInt32(MI.Schedule_LP.IsSuperImmediate)
        //             , Convert.ToInt32(MI.Schedule_SS.IsSuperImmediate)
        //             , Convert.ToInt32(MI.Schedule_CS.IsSuperImmediate)
        //             , MI.SetIPProfile
        //             , MI.SetModemInitializeBasics
        //             , MI.SetModemInitializeExtended
        //             , MI.SetKeepAlive
        //             , MI.MajorAlarmGroupID
        //             , MI.Kas_DueTime.ToString(DateFormat)
        //             , Convert.ToInt32(MI.LPParamRequest.ChannelRequest)
        //             , Convert.ToInt32(MI.LPParamRequest.ChangeIntervalRequest)
        //             , MI.MSN);
        //          OdbcCommand Command = new OdbcCommand(query, Connection);

        //         if (ExecuteCommandWithAlreadyOpenedConnection(Command))
        //             return true;
        //         else
        //             return false;
        //     }
        //     catch (Exception ex)
        //     {
        //         throw ex;//report error
        //     }
        //     finally
        //     {
        //         _DBConnect.CloseConnection();
        //     }
        // }

        public List<string> GetMSNListByGatewaySerialNumber(string feeder_id)
        {
            List<string> MSNs = null;
            DataTable DT = null;
             OdbcCommand Command = null;
            string query = null;

            try
            {
                DBConnect.OpenConnection();

                query = "SELECT distinct `msn` "
                        + " FROM meter WHERE `parent_id` = " + feeder_id;

                Command = new OdbcCommand(query, Connection);
                DT = GetTableFromDB(Command, DT);

                if (DT.Rows.Count > 0)
                {
                    MSNs = new List<string>();
                    string msnSTR = string.Empty;

                    foreach (DataRow row in DT.Rows)
                    {
                        if (row[DT.Columns["msn"]] != DBNull.Value)
                            msnSTR = row[DT.Columns["msn"]].ToString();

                        if (!string.IsNullOrEmpty(msnSTR) &&
                            !string.IsNullOrWhiteSpace(msnSTR))
                            MSNs.Add(msnSTR);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;//report error
            }
            finally
            {
                try
                {
                    DBConnect.CloseConnection();
                    DT.Clear();
                    DT.Dispose();
                    Command.Dispose();
                    query = null;
                }
                catch
                { }
            }

            return MSNs;

        }

        #endregion

        #region General

        public bool GeneralUpdate(string TableName, string[] Fields, string[] Values, string WhereField = "", string WhereCriteria = "", string WhereValue = "")
        {
            try
            {
                string query = String.Format("UPDATE {0} SET ", TableName);
                //Fields and values
                if (Fields.Length == Values.Length)
                {
                    for (int i = 0; i < Fields.Length; i++)
                    {
                        if (i > 0)
                            query = query + ", ";
                        query = String.Format("{0} {1} = {2}", query, Fields[i], Values[i]);
                    }
                }
                else
                    throw new Exception("Fields and Values count mismatch");
                //WHERE clause
                if (!(String.IsNullOrEmpty(WhereField) && String.IsNullOrEmpty(WhereValue) && String.IsNullOrEmpty(WhereCriteria)))
                {
                    query = String.Format("{0} WHERE {1} {2} {3}", query, WhereField, WhereCriteria, WhereValue);
                }
                if (ExecuteQuery(query))

                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }

        }

        public DataTable GeneralSelect(string TableName, string[] Fields, string[] Values, string WhereField = "", string WhereCriteria = "", string WhereValue = "")
        {
            DataTable DT = null;
            try
            {
                string query = "SELECT";
                //Fields and values
                if (Fields.Length == Values.Length)
                {
                    for (int i = 0; i < Fields.Length; i++)
                    {
                        if (i > 0)
                            query = query + ", ";
                        query = String.Format("{0} {1}", query, Fields[i]);
                    }
                }
                else
                    throw new Exception("Fields and Values count mismatch");
                query = String.Format("{0} FROM {1}", query, TableName);

                //WHERE clause
                if (!(String.IsNullOrEmpty(WhereField) && String.IsNullOrEmpty(WhereValue) && String.IsNullOrEmpty(WhereCriteria)))
                {
                    query = String.Format("{0} WHERE {1} {2} {3}", query, WhereField, WhereCriteria, WhereValue);
                }
                DT = GetTableFromDB(query);
            }
            catch
            {
                throw;
            }
            return DT;
        }

        private uint getConfigID(uint model)
        {
            try
            {
                switch (model)
                {
                    case 1: return 103;
                    case 2: return 111;
                    case 3: return 109;
                    default: throw new Exception("Undefined meter model");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Members

        private const string DateFormat = "yyyy-MM-dd HH:mm:ss";
        private const string DateOnlyFormat = "yyyy-MM-dd";
        private const string TimeFormat = @"hh\:mm\:ss";
        private const string Incoming_Live = "instantaneous_data_live";
        private DBConnect _DBConnect;
        private string DBNULLParam = "NULL";

        private bool InsertWeeklyLoadProfile = Settings.Default.EnableWeeklyLoadProfile;
        private bool InsertWeeklyInstantaneousData = Settings.Default.EnableWeeklyInstentanousData;

        #endregion

        #region Properties
        public DBConnect DBConnect
        {
            get
            {
                return _DBConnect;
            }
            set { _DBConnect = value; }
        }

        private OdbcCommand Command
        {
            get
            {
                return _DBConnect.Command;
            }
            set
            {
                _DBConnect.Command = value;
            }
        }
        private OdbcConnection Connection
        {
            get
            {
                return _DBConnect.Connection;
            }
        }
        private bool IsConnectionOpen
        {
            get
            {
                return _DBConnect.IsConnectionOpen;
            }
        }

        #endregion

        #region Constructor

        public DatabaseController()
        {
            try
            {
                _DBConnect = new DBConnect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }

        #endregion

        #region MDC SETTINGS
        public bool IsConfigurationRefreshRequired()
        {
            try
            {
                if (!_DBConnect.IsConnectionOpen)
                    _DBConnect.OpenConnection();

                OdbcCommand cmd = new OdbcCommand();
                cmd.CommandText = "select * from device_association where `reload_config` = 1";
                cmd.Connection = _DBConnect.Connection;
                var tbl = GetTableFromDB(cmd);
                return tbl.Rows.Count > 0;
            }
            catch (Exception)
            {
                //if (_newException != null) _newException(ex);
                return false;
            }
        }

        public bool UpdateConfigurationRefreshFlag(byte valueToUpdate)
        {
            try
            {
                if (!_DBConnect.IsConnectionOpen)
                    _DBConnect.OpenConnection();

                OdbcCommand cmd = new OdbcCommand();
                cmd.CommandText = string.Format("update device_association set `reload_config` = {0} where `reload_config` = 1", valueToUpdate);
                cmd.Connection = _DBConnect.Connection;
                ExecuteUpdateCommand(cmd);
                return true;
            }
            catch (Exception)
            {
                //if (_newException != null) _newException(ex);
                return false;
            }
        }

        #endregion

        #region MDC Save to DB
        public bool UpdateLastEvent_Live_individual(string msn, int event_code, DateTime timestamp)
        {
            try
            {
                DBConnect.OpenConnection();
                string query = String.Format("Update instantaneous_data_live set latest_eventcode_individual='{0}',latest_eventcode_individual_timestamp='{1}' where msn='{2}'", event_code, timestamp.ToString(DateFormat), msn);
                Command = new OdbcCommand(query, Connection);
                Command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }
        }
        public bool UpdateLastEvent_Live_logbook(string msn, int event_code, DateTime timestamp)
        {
            try
            {
                DBConnect.OpenConnection();
                string query = String.Format("Update instantaneous_data_live set latest_eventcode_logbook='{0}',latest_eventcode_logbook_timestamp='{1}' where msn='{2}'", event_code, timestamp.ToString(DateFormat), msn);
                Command = new OdbcCommand(query, Connection);
                Command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }
        }
        #region Signal Strength

        public bool UpdateSignalStrength_Live(double SignalStrength, string MSN, DateTime session_date_time)
        {
            try
            {
                DBConnect.OpenConnection();
                string query = String.Format("UPDATE instantaneous_data_live SET `signal_strength` = '{0}',`session_date_time`='{1}' WHERE msn = '{2}'", SignalStrength, session_date_time.ToString(DateFormat), MSN);
                OdbcCommand Command = new OdbcCommand(query, Connection);
                if (ExecuteUpdateCommand(Command))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Updating Signal Strength in Instantaneous Live", ex);
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        //public bool InsertSignalStrength_Live(double SignalStrength, string MSN, DateTime session_date_time)
        //{
        //    try
        //    {
        //        string query = String.Format("insert into instantaneous_data_live (`msn`,`reference_no`, `signal_strength`,`session_date_time`) values('{0}','{0}','{1}','{2}')",MSN, SignalStrength, session_date_time.ToString(DateFormat));
        //        OdbcCommand Command = new OdbcCommand(query, Connection);
        //        if (ExecuteUpdateCommand(Command))
        //            return true;
        //        else
        //            return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error While Updating Signal Strength in Instantaneous Live", ex);
        //    }
        //    finally
        //    {
        //    }
        //}

        public bool InsertSignalStrength_Live(double SignalStrength, string MSN, DateTime session_date_time)
        {
            try
            {
                DBConnect.OpenConnection();
                string query = String.Format("INSERT INTO instantaneous_data_live(`msn`,`session_date_time`,`signal_strength`) VALUES('{0}', '{1}','{2}')", MSN, session_date_time.ToString(DateFormat), SignalStrength);
                // string query = String.Format("Replace INTO instantaneous_data_live(msn, `signal_strength`) VALUES('{0}', '{1}')", MSN, SignalStrength);

                OdbcCommand Command = new OdbcCommand(query, Connection);
                if (ExecuteCommandWithAlreadyOpenedConnection(Command))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Inserting Signal Strength in Instantaneous Live", ex);
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        #endregion

        #region SaveInstantaneous


        #region Save / Update Instantaneous Read by OBIS List

        private void GetColumnsValuesString(Dictionary<string, double> Read_Values, out string columnsList, out string valuesList, out string columnsVaueslList)
        {
            columnsList = string.Empty;
            valuesList = string.Empty;
            columnsVaueslList = string.Empty;

            foreach (var item in Read_Values)
            {
                columnsList += ($"`{item.Key}`, "); //for Insert
                valuesList += ($"{item.Value}, "); //for Insert
                columnsVaueslList += ($"`{item.Key}` = {item.Value}, "); //for Update
            }
            columnsList = columnsList.Remove(columnsList.LastIndexOf(','), 1);
            valuesList = valuesList.Remove(valuesList.LastIndexOf(','), 1);
            columnsVaueslList = columnsVaueslList.Remove(columnsVaueslList.LastIndexOf(','), 1);
        }

        public bool InsertInstantaneous_byObisList(Instantaneous_Class inst_Class, DateTime SessionDateTime, MeterInformation MeterInfo)
        {
            try
            {
                string query = string.Empty;
                //GetColumnsValuesString(Read_Values, out string columnsList, out string valuesList, out string columnsValuesList);
                query = $"INSERT INTO instantaneous_data (`msn`,`global_device_id`, `mdc_read_datetime`, `db_datetime`, {inst_Class.DBColumns} `meter_datetime`) "
                      + $"VALUES ('{MeterInfo.MSN}','{MeterInfo.GlobalDeviceId}', '{SessionDateTime.ToString(DateFormat)}', now(), {inst_Class.DBValues} "
                      + $"'{inst_Class.dateTime.GetDateTime().ToString(DateFormat)}')";

                DBConnect.OpenConnection();
                OdbcCommand Command = new OdbcCommand(query, Connection);
                var rslt = ExecuteCommandWithAlreadyOpenedConnection(Command);
                return rslt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert_Update_Instantaneous_Live_byObisList(Instantaneous_Class inst_Class, DateTime SessionDateTime, DateTime ConnectionTime, MeterInformation MeterInfo)
        {
            try
            {
                //TODO: Read Properly from meter
                //inst_Class.dateTime = new StDateTime();
                string query = "";
                //GetColumnsValuesString(Read_Values, out string columnsList, out string valuesList, out string columnsValuesList);

                query = $"INSERT INTO instantaneous_data_live(`msn`, `session_date_time`, `connection_time`, {inst_Class.DBColumns} `meter_date_time`, "
                      + $" `ct`,`pt`,`customer_id`,`global_device_id`)"
                      + $" VALUES ('{MeterInfo.MSN}', '{SessionDateTime.ToString(DateFormat)}', '{ConnectionTime.ToString(DateFormat)}', "
                      + $" {inst_Class.DBValues} '{inst_Class.dateTime.GetDateTime().ToString(DateFormat)}', "
                      + $" {MeterInfo.CT},{MeterInfo.PT},{MeterInfo.Customer_ID},'{MeterInfo.GlobalDeviceId}')"
                      + $" ON DUPLICATE KEY UPDATE "
                      + $" `f_id` = {MeterInfo.MeterID}, `session_date_time`= '{SessionDateTime.ToString(DateFormat)}',{inst_Class.DB_Columns_Values}"
                      + $" `meter_date_time`='{inst_Class.dateTime.GetDateTime().ToString(DateFormat)}', `connection_time` = '{ConnectionTime.ToString(DateFormat)}',"
                      + $" `ct`={MeterInfo.CT},`pt`={MeterInfo.PT},`customer_id`={MeterInfo.Customer_ID},`global_device_id`='{MeterInfo.GlobalDeviceId}'";

                DBConnect.OpenConnection();
                OdbcCommand Command = new OdbcCommand(query, Connection);
                if (ExecuteCommandWithAlreadyOpenedConnection(Command))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw new Exception("Error while inserting Instantaneous Live Consumption Data", ex);
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        #endregion Save / Update Instantaneous Read by OBIS List

        public bool SaveInstantaneous(Instantaneous_Class Data, DateTime SessionDateTime, int SignalStrength, bool is_ss_read, MeterInformation MeterInfo)
        {
            try
            {
                string query = string.Empty;

                query = String.Format("INSERT  INTO instant_data (`msn`, `session_date_time`, `time`, `date`, `meter_date_time`, `current_phase_a`, `current_phase_b`, `current_phase_c`, `current_phase_total`, `voltage_phase_a`, `voltage_phase_b`, `voltage_phase_c`, `voltage_phase_total`, `active_pwr_pos_phase_a`, `active_pwr_pos_phase_b`, `active_pwr_pos_phase_c`, `active_pwr_pos_phase_total`, `active_pwr_neg_phase_a`, `active_pwr_neg_phase_b`, `active_pwr_neg_phase_c`, `active_pwr_neg_phase_total`, `reactive_pwr_pos_phase_a`, `reactive_pwr_pos_phase_b`, `reactive_pwr_pos_phase_c`, `reactive_pwr_pos_phase_total`, `reactive_pwr_neg_phase_a`, `reactive_pwr_neg_phase_b`, `reactive_pwr_neg_phase_c`, `reactive_pwr_neg_phase_total`, `frequency`, `signal_strength`,`ct`,`pt`,`customer_id`,`global_device_id`) "
                    + "VALUES('{0}', '{1}', CURTIME(), CURDATE(), '{2}', {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28},{29},{30},{31}, '{32}' )"

                , Data.MSN
                , SessionDateTime.ToString(DateFormat)
                , Data.dateTime.GetDateTime().ToString(DateFormat)
                , Commons.Validate_BillData(Data.current_PhaseA)
                , Commons.Validate_BillData(Data.current_PhaseB)
                , Commons.Validate_BillData(Data.current_PhaseC)
                , Commons.Validate_BillData(Data.current_PhaseTL)
                , Commons.Validate_BillData(Data.voltage_PhaseA)
                , Commons.Validate_BillData(Data.voltage_PhaseB)
                , Commons.Validate_BillData(Data.voltage_PhaseC)
                , Commons.Validate_BillData(Data.voltage_PhaseTL)

                , Commons.Validate_BillData(Data.active_powerPositive_PhaseA)
                , Commons.Validate_BillData(Data.active_powerPositive_PhaseB)
                , Commons.Validate_BillData(Data.active_powerPositive_PhaseC)
                , Commons.Validate_BillData(Data.active_powerPositive_PhaseTL)

                , Commons.Validate_BillData(Data.active_powerNegative_PhaseA)
                , Commons.Validate_BillData(Data.active_powerNegative_PhaseB)
                , Commons.Validate_BillData(Data.active_powerNegative_PhaseC)
                , Commons.Validate_BillData(Data.active_powerNegative_PhaseTL)

                , Commons.Validate_BillData(Data.reactive_powerPositive_PhaseA)
                , Commons.Validate_BillData(Data.reactive_powerPositive_PhaseB)
                , Commons.Validate_BillData(Data.reactive_powerPositive_PhaseC)
                , Commons.Validate_BillData(Data.reactive_powerPositive_PhaseTL)

                , Commons.Validate_BillData(Data.reactive_powerNegative_PhaseA)
                , Commons.Validate_BillData(Data.reactive_powerNegative_PhaseB)
                , Commons.Validate_BillData(Data.reactive_powerNegative_PhaseC)
                , Commons.Validate_BillData(Data.reactive_powerNegative_PhaseTL)
                , Commons.Validate_BillData(Data.frequency)
                , (is_ss_read) ? SignalStrength.ToString() : DBNULLParam
                // , SignalStrength
                , MeterInfo.CT
                , MeterInfo.PT
                , MeterInfo.Customer_ID
                , MeterInfo.GlobalDeviceId
                );


                //    , (SignalStrength == 0) ? "-116" : SignalStrength.ToString());

                // // , (SignalStrength == 0) ? DBNULLParam : SignalStrength.ToString());
                //// ,  SignalStrength.ToString());

                OdbcCommand Command = new OdbcCommand(query, Connection);
                var rslt = ExecuteCommandWithAlreadyOpenedConnection(Command);
                if (rslt && InsertWeeklyInstantaneousData)
                {
                    try
                    {
                        Command.CommandText = String.Format("INSERT  INTO weekly_instantaneous_data(`msn`, `session_date_time`, `time`, `date`, `meter_date_time`, `current_phase_a`, `current_phase_b`, `current_phase_c`, `current_phase_total`, `voltage_phase_a`, `voltage_phase_b`, `voltage_phase_c`, `voltage_phase_total`, `active_pwr_pos_phase_a`, `active_pwr_pos_phase_b`, `active_pwr_pos_phase_c`, `active_pwr_pos_phase_total`, `active_pwr_neg_phase_a`, `active_pwr_neg_phase_b`, `active_pwr_neg_phase_c`, `active_pwr_neg_phase_total`, `reactive_pwr_pos_phase_a`, `reactive_pwr_pos_phase_b`, `reactive_pwr_pos_phase_c`, `reactive_pwr_pos_phase_total`, `reactive_pwr_neg_phase_a`, `reactive_pwr_neg_phase_b`, `reactive_pwr_neg_phase_c`, `reactive_pwr_neg_phase_total`, `frequency`, `signal_strength`,`ct`,`pt`,`customer_id`) "
                        + "VALUES('{0}', '{1}', CURTIME(), CURDATE(), '{2}', {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28},{29}, {30},{31})"
                        , Data.MSN
                        , SessionDateTime.ToString(DateFormat)
                        , Data.dateTime.GetDateTime().ToString(DateFormat)
                        , Commons.Validate_BillData(Data.current_PhaseA)
                        , Commons.Validate_BillData(Data.current_PhaseB)
                        , Commons.Validate_BillData(Data.current_PhaseC)
                        , Commons.Validate_BillData(Data.current_PhaseTL)
                        , Commons.Validate_BillData(Data.voltage_PhaseA)
                        , Commons.Validate_BillData(Data.voltage_PhaseB)
                        , Commons.Validate_BillData(Data.voltage_PhaseC)
                        , Commons.Validate_BillData(Data.voltage_PhaseTL)

                        , Commons.Validate_BillData(Data.active_powerPositive_PhaseA)
                        , Commons.Validate_BillData(Data.active_powerPositive_PhaseB)
                        , Commons.Validate_BillData(Data.active_powerPositive_PhaseC)
                        , Commons.Validate_BillData(Data.active_powerPositive_PhaseTL)

                        , Commons.Validate_BillData(Data.active_powerNegative_PhaseA)
                        , Commons.Validate_BillData(Data.active_powerNegative_PhaseB)
                        , Commons.Validate_BillData(Data.active_powerNegative_PhaseC)
                        , Commons.Validate_BillData(Data.active_powerNegative_PhaseTL)

                        , Commons.Validate_BillData(Data.reactive_powerPositive_PhaseA)
                        , Commons.Validate_BillData(Data.reactive_powerPositive_PhaseB)
                        , Commons.Validate_BillData(Data.reactive_powerPositive_PhaseC)
                        , Commons.Validate_BillData(Data.reactive_powerPositive_PhaseTL)

                        , Commons.Validate_BillData(Data.reactive_powerNegative_PhaseA)
                        , Commons.Validate_BillData(Data.reactive_powerNegative_PhaseB)
                        , Commons.Validate_BillData(Data.reactive_powerNegative_PhaseC)
                        , Commons.Validate_BillData(Data.reactive_powerNegative_PhaseTL)

                        , Commons.Validate_BillData(Data.frequency)
                        , (is_ss_read) ? SignalStrength.ToString() : DBNULLParam
                        , MeterInfo.CT
                        , MeterInfo.PT
                        , MeterInfo.Customer_ID
                        );

                        //    , (SignalStrength == 0) ? DBNULLParam : SignalStrength.ToString());
                        // , SignalStrength.ToString());
                        // , SignalStrength);

                        ExecuteCommandWithAlreadyOpenedConnection(Command);
                    }
                    catch (Exception ex)
                    {
                        if (_newException != null) _newException(ex);
                    }
                    return true;
                }
                else
                    return rslt;
            }
            catch (Exception ex)
            {
                // throw ex;
                if (_newException != null) _newException(ex);
                return false;
            }
        }

        public bool UpdateInstantaneous_Live(Instantaneous_Class Data, DateTime SessionDateTime, DateTime ConnectionTime, MeterInformation MeterInfo)
        {
            try
            {
                string query = "";
                //Both Non Zero active power Values are available
                if (Data.active_powerPositive_PhaseTL > 0 && Data.active_powerNegative_PhaseTL > 0)
                {
                    query = $"UPDATE instantaneous_data_live SET " +
                        $"`session_date_time` =                     '{SessionDateTime.ToString(DateFormat)}'," +
                        $" `meter_date_time` =                      '{Data.dateTime.GetDateTime().ToString(DateFormat)}'," +
                        $" `active_pwr_pos_phase_total` =           '{Commons.Validate_BillData(Data.active_powerPositive_PhaseTL)}'," +
                        $" `active_pwr_neg_phase_total` =           '{Commons.Validate_BillData(Data.active_powerNegative_PhaseTL)}'," +
                        $" `non_zero_active_pwr_pos_phase_total` =  '{Commons.Validate_BillData(Data.active_powerPositive_PhaseTL)}'," +
                        $" `non_zero_active_pwr_neg_phase_total` =  '{Commons.Validate_BillData(Data.active_powerNegative_PhaseTL)}'," +
                        $" `reactive_pwr_pos_phase_total` =         '{Commons.Validate_BillData(Data.reactive_powerPositive_PhaseTL)}'," +
                        $" `reactive_pwr_neg_phase_total` =         '{Commons.Validate_BillData(Data.reactive_powerNegative_PhaseTL)}'," +
                        $" `voltage_phase_total` =                  '{Commons.Validate_BillData(Data.voltage_PhaseTL)}'," +
                        $" `current_phase_total` =                  '{Commons.Validate_BillData(Data.current_PhaseTL)}'," +
                        $" `frequency` =                            '{Commons.Validate_BillData(Data.frequency)}'," +
                        $" `connection_time` =                      '{ConnectionTime.ToString(DateFormat)}'," +
                        $" Type =                                   '{(int)MeterInfo.MeterType_OBJ}'," +
                        $"`session_time_active_power_positive_TL`=  '{SessionDateTime.ToString(DateFormat)}'," +
                        $"`session_time_active_power_negative_TL`=  '{SessionDateTime.ToString(DateFormat)}'," +
                        $"`ct`=                                     '{MeterInfo.CT}'," +
                        $"`pt`=                                     '{MeterInfo.PT}'," +
                        $"`f_id`=                                   '{MeterInfo.MeterID}'," + //TODO: 
                        $"current_phase_a='{Commons.Validate_BillData(Data.current_PhaseA)}'," +
                        $"current_phase_b='{Commons.Validate_BillData(Data.current_PhaseB)}'," +
                        $"current_phase_c='{Commons.Validate_BillData(Data.current_PhaseC)}'," +
                        $"voltage_phase_a='{Commons.Validate_BillData(Data.voltage_PhaseA)}'," +
                        $"voltage_phase_b='{Commons.Validate_BillData(Data.voltage_PhaseB)}'," +
                        $"voltage_phase_c='{Commons.Validate_BillData(Data.voltage_PhaseC)}'" +
                        $"WHERE msn = '{Data.MSN}'";

                }
                //Only non zero active power positive available
                else if (Data.active_powerPositive_PhaseTL > 0)
                {
                    query = $"UPDATE instantaneous_data_live SET " +
                        $"`session_date_time` =                    '{SessionDateTime.ToString(DateFormat)}'," +
                        $" `meter_date_time` =                     '{Data.dateTime.GetDateTime().ToString(DateFormat)}'," +
                        $" `active_pwr_pos_phase_total` =          '{Commons.Validate_BillData(Data.active_powerPositive_PhaseTL)}'," +
                        $" `active_pwr_neg_phase_total` =          '{Commons.Validate_BillData(Data.active_powerNegative_PhaseTL)}'," +
                        $" `non_zero_active_pwr_pos_phase_total` = '{Commons.Validate_BillData(Data.active_powerPositive_PhaseTL)}'," +
                        $" `reactive_pwr_pos_phase_total` =        '{Commons.Validate_BillData(Data.reactive_powerPositive_PhaseTL)}'," +
                        $" `reactive_pwr_neg_phase_total` =        '{Commons.Validate_BillData(Data.reactive_powerNegative_PhaseTL)}'," +
                        $" `voltage_phase_total` =                 '{Commons.Validate_BillData(Data.voltage_PhaseTL)}'," +
                        $" `current_phase_total` =                 '{Commons.Validate_BillData(Data.current_PhaseTL)}'," +
                        $" `frequency` =                           '{Commons.Validate_BillData(Data.frequency)}'," +
                        $" `connection_time` =                     '{ConnectionTime.ToString(DateFormat)}'," +
                        $" Type =                                  '{(int)MeterInfo.MeterType_OBJ}'," +
                        $"`session_time_active_power_positive_TL`= '{SessionDateTime.ToString(DateFormat)}'," +
                        $"`ct`=                                    '{MeterInfo.CT}'," +
                        $"`pt`=                                    '{MeterInfo.PT}'," +
                        $"`f_id`=                                  '{MeterInfo.MeterID}'," +
                        $"current_phase_a='{Commons.Validate_BillData(Data.current_PhaseA)}'," +
                        $"current_phase_b='{Commons.Validate_BillData(Data.current_PhaseB)}'," +
                        $"current_phase_c='{Commons.Validate_BillData(Data.current_PhaseC)}'," +
                        $"voltage_phase_a='{Commons.Validate_BillData(Data.voltage_PhaseA)}'," +
                        $"voltage_phase_b='{Commons.Validate_BillData(Data.voltage_PhaseB)}'," +
                        $"voltage_phase_c='{Commons.Validate_BillData(Data.voltage_PhaseC)}'" +
                        $"WHERE msn = '{Data.MSN}'";
                }
                //Only non zero active power negative available
                else if (Data.active_powerNegative_PhaseTL > 0)
                {
                    query = $"UPDATE instantaneous_data_live SET " +
                        $"`session_date_time` =                     '{SessionDateTime.ToString(DateFormat)}'," +
                        $" `meter_date_time` =                      '{Data.dateTime.GetDateTime().ToString(DateFormat)}'," +
                        $" `active_pwr_pos_phase_total` =           '{Commons.Validate_BillData(Data.active_powerPositive_PhaseTL)}'," +
                        $" `active_pwr_neg_phase_total` =           '{Commons.Validate_BillData(Data.active_powerNegative_PhaseTL)}'," +
                        $" `non_zero_active_pwr_neg_phase_total` =  '{Commons.Validate_BillData(Data.active_powerNegative_PhaseTL)}'," +
                        $" `reactive_pwr_pos_phase_total` =         '{Commons.Validate_BillData(Data.reactive_powerPositive_PhaseTL)}'," +
                        $" `reactive_pwr_neg_phase_total` =         '{Commons.Validate_BillData(Data.reactive_powerNegative_PhaseTL)}', " +
                        $" `voltage_phase_total` =                  '{Commons.Validate_BillData(Data.voltage_PhaseTL)}'," +
                        $" `current_phase_total` =                  '{Commons.Validate_BillData(Data.current_PhaseTL)}'," +
                        $" `frequency` =                            '{Commons.Validate_BillData(Data.frequency)}'," +
                        $" `connection_time` =                      '{ConnectionTime.ToString(DateFormat)}'," +
                        $" Type =                                   '{(int)MeterInfo.MeterType_OBJ}'," +
                        $"`session_time_active_power_negative_TL`=  '{SessionDateTime.ToString(DateFormat)}'," +
                        $"`ct`=                                     '{MeterInfo.CT}'," +
                        $"`pt`=                                     '{MeterInfo.PT}'," +
                        $"`f_id`=                                   '{MeterInfo.MeterID}', " +
                        $"current_phase_a='{Commons.Validate_BillData(Data.current_PhaseA)}'," +
                        $"current_phase_b='{Commons.Validate_BillData(Data.current_PhaseB)}'," +
                        $"current_phase_c='{Commons.Validate_BillData(Data.current_PhaseC)}'," +
                        $"voltage_phase_a='{Commons.Validate_BillData(Data.voltage_PhaseA)}'," +
                        $"voltage_phase_b='{Commons.Validate_BillData(Data.voltage_PhaseB)}'," +
                        $"voltage_phase_c='{Commons.Validate_BillData(Data.voltage_PhaseC)}'" +
                        $"WHERE msn = '{Data.MSN}'";
                }
                //Both Non Zero active power Values are not available
                else
                {
                    query = $"UPDATE instantaneous_data_live SET " +
                        $"`session_date_time` = '{SessionDateTime.ToString(DateFormat)}', " +
                        $"`meter_date_time` = '{Data.dateTime.GetDateTime().ToString(DateFormat)}', " +
                        $"`active_pwr_pos_phase_total` = '{Commons.Validate_BillData(Data.active_powerPositive_PhaseTL)}', " +
                        $"`active_pwr_neg_phase_total` = '{Commons.Validate_BillData(Data.active_powerNegative_PhaseTL)}', " +
                        $"`reactive_pwr_pos_phase_total` = '{Commons.Validate_BillData(Data.reactive_powerPositive_PhaseTL)}', " +
                        $"`reactive_pwr_neg_phase_total` = '{Commons.Validate_BillData(Data.reactive_powerNegative_PhaseTL)}' , " +
                        $"`voltage_phase_total` = '{Commons.Validate_BillData(Data.voltage_PhaseTL)}', " +
                        $"`current_phase_total` = '{Commons.Validate_BillData(Data.current_PhaseTL)}', " +
                        $"`frequency` = '{Commons.Validate_BillData(Data.frequency)}', " +
                        $"`connection_time` = '{ConnectionTime.ToString(DateFormat)}', " +
                        $"Type = '{(int)MeterInfo.MeterType_OBJ}'," +
                        $"`ct`='{MeterInfo.CT}'," +
                        $"`pt`='{MeterInfo.PT}'," +
                        $"`f_id`='{MeterInfo.MeterID}', " +

                        $"current_phase_a='{Commons.Validate_BillData(Data.current_PhaseA)}'," +
                        $"current_phase_b='{Commons.Validate_BillData(Data.current_PhaseB)}'," +
                        $"current_phase_c='{Commons.Validate_BillData(Data.current_PhaseC)}'," +
                        $"voltage_phase_a='{Commons.Validate_BillData(Data.voltage_PhaseA)}'," +
                        $"voltage_phase_b='{Commons.Validate_BillData(Data.voltage_PhaseB)}'," +
                        $"voltage_phase_c='{Commons.Validate_BillData(Data.voltage_PhaseC)}'" +
                        $"WHERE msn = '{Data.MSN}'";
                }
                DBConnect.OpenConnection();
                OdbcCommand Command = new OdbcCommand(query, Connection);
                if (ExecuteCommandWithAlreadyOpenedConnection(Command))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw new Exception("Error while updating Instantaneous Live Data", ex);
            }

        }

        public bool InsertInstantaneous_Live(Instantaneous_Class Data, DateTime SessionDateTime, DateTime ConnectionTime, MeterInformation MeterInfo)
        {
            try
            {
                DBConnect.OpenConnection();
                string query = "";
                //Both Non Zero active power Values are available
                if (Data.active_powerPositive_PhaseTL > 0 && Data.active_powerNegative_PhaseTL > 0)
                {
                    query = String.Format("INSERT INTO instantaneous_data_live(`f_id`,`msn`, `session_date_time`, `meter_date_time`, `active_pwr_pos_phase_total`, `active_pwr_neg_phase_total`, `non_zero_active_pwr_pos_phase_total`, `non_zero_active_pwr_neg_phase_total`, `reactive_pwr_pos_phase_total`, `reactive_pwr_neg_phase_total`, `voltage_phase_total`,`current_phase_total`, `frequency`, `connection_time`, `type`,`session_time_active_power_positive_TL`,`session_time_active_power_negative_TL`,`ct`,`pt`,current_phase_a,current_phase_b,current_phase_c,voltage_phase_a,voltage_phase_b,voltage_phase_c) VALUES('{18}','{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}','{14}','{15}','{16}','{17}','{19}','{20}', '{21}', '{22}','{23}','{24}')"
                        , Data.MSN
                        , SessionDateTime.ToString(DateFormat)
                        , Data.dateTime.GetDateTime().ToString(DateFormat)
                        , Commons.Validate_BillData(Data.active_powerPositive_PhaseTL)
                        , Commons.Validate_BillData(Data.active_powerNegative_PhaseTL)
                        , Commons.Validate_BillData(Data.active_powerPositive_PhaseTL)
                        , Commons.Validate_BillData(Data.active_powerNegative_PhaseTL)
                        , Commons.Validate_BillData(Data.reactive_powerPositive_PhaseTL)
                        , Commons.Validate_BillData(Data.reactive_powerNegative_PhaseTL)
                        , Commons.Validate_BillData(Data.voltage_PhaseTL)
                        , Commons.Validate_BillData(Data.current_PhaseTL)
                        , Commons.Validate_BillData(Data.frequency)
                        , ConnectionTime.ToString(DateFormat)
                        , (int)MeterInfo.MeterType_OBJ
                        , SessionDateTime.ToString(DateFormat)
                        , SessionDateTime.ToString(DateFormat)
                        , MeterInfo.CT
                        , MeterInfo.PT
                        , MeterInfo.MeterID
                        , Commons.Validate_BillData(Data.current_PhaseA)
                        , Commons.Validate_BillData(Data.current_PhaseB)
                        , Commons.Validate_BillData(Data.current_PhaseC)
                        , Commons.Validate_BillData(Data.voltage_PhaseA)
                        , Commons.Validate_BillData(Data.voltage_PhaseB)
                        , Commons.Validate_BillData(Data.voltage_PhaseC)
                        );
                }
                //Only non zero active power positive available
                else if (Data.active_powerPositive_PhaseTL > 0)
                {

                    query = String.Format("INSERT INTO instantaneous_data_live(`f_id`,`msn`, `session_date_time`, `meter_date_time`, `active_pwr_pos_phase_total`, `active_pwr_neg_phase_total`, `non_zero_active_pwr_pos_phase_total`, `reactive_pwr_pos_phase_total`, `reactive_pwr_neg_phase_total`, `voltage_phase_total`,`current_phase_total`, `frequency`, `connection_time`, `type`,`session_time_active_power_positive_TL`,`ct`,`pt`,current_phase_a,current_phase_b,current_phase_c,voltage_phase_a,voltage_phase_b,voltage_phase_c) VALUES('{16}','{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}','{13}','{14}','{15}','{17}','{18}','{19}','{20}', '{21}', '{22}')"
                        , Data.MSN
                        , SessionDateTime.ToString(DateFormat)
                        , Data.dateTime.GetDateTime().ToString(DateFormat)
                        , Commons.Validate_BillData(Data.active_powerPositive_PhaseTL)
                        , Commons.Validate_BillData(Data.active_powerNegative_PhaseTL)
                        , Commons.Validate_BillData(Data.active_powerPositive_PhaseTL)
                        , Commons.Validate_BillData(Data.reactive_powerPositive_PhaseTL)
                        , Commons.Validate_BillData(Data.reactive_powerNegative_PhaseTL)
                        , Commons.Validate_BillData(Data.voltage_PhaseTL)
                        , Commons.Validate_BillData(Data.current_PhaseTL)
                        , Commons.Validate_BillData(Data.frequency)
                        , ConnectionTime.ToString(DateFormat)
                        , (int)MeterInfo.MeterType_OBJ
                        , SessionDateTime.ToString(DateFormat)
                        , MeterInfo.CT
                        , MeterInfo.PT
                        , MeterInfo.MeterID
                        , Commons.Validate_BillData(Data.current_PhaseA)
                        , Commons.Validate_BillData(Data.current_PhaseB)
                        , Commons.Validate_BillData(Data.current_PhaseC)
                        , Commons.Validate_BillData(Data.voltage_PhaseA)
                        , Commons.Validate_BillData(Data.voltage_PhaseB)
                        , Commons.Validate_BillData(Data.voltage_PhaseC)
                        );
                }
                //Only non zero active power negative available
                else if (Data.active_powerNegative_PhaseTL > 0)
                {

                    query = String.Format("INSERT INTO instantaneous_data_live(`f_id`,`msn`, `session_date_time`, `meter_date_time`, `active_pwr_pos_phase_total`, `active_pwr_neg_phase_total`, `non_zero_active_pwr_neg_phase_total`, `reactive_pwr_pos_phase_total`, `reactive_pwr_neg_phase_total`, `voltage_phase_total`,`current_phase_total`, `frequency`, `connection_time`, `type`,`session_time_active_power_negative_TL`,`ct`,`pt`,current_phase_a,current_phase_b,current_phase_c,voltage_phase_a,voltage_phase_b,voltage_phase_c) VALUES('{16}','{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}','{13}','{14}','{15}','{17}','{18}','{19}','{20}', '{21}', '{22}')"
                        , Data.MSN
                        , SessionDateTime.ToString(DateFormat)
                        , Data.dateTime.GetDateTime().ToString(DateFormat)
                        , Commons.Validate_BillData(Data.active_powerPositive_PhaseTL)
                        , Commons.Validate_BillData(Data.active_powerNegative_PhaseTL)
                        , Commons.Validate_BillData(Data.active_powerNegative_PhaseTL)
                        , Commons.Validate_BillData(Data.reactive_powerPositive_PhaseTL)
                        , Commons.Validate_BillData(Data.reactive_powerNegative_PhaseTL)
                        , Commons.Validate_BillData(Data.voltage_PhaseTL)
                        , Commons.Validate_BillData(Data.current_PhaseTL)
                        , Commons.Validate_BillData(Data.frequency)
                        , ConnectionTime.ToString(DateFormat)
                        , (int)MeterInfo.MeterType_OBJ
                        , SessionDateTime.ToString(DateFormat)
                        , MeterInfo.CT
                        , MeterInfo.PT
                        , MeterInfo.MeterID
                        , Commons.Validate_BillData(Data.current_PhaseA)
                        , Commons.Validate_BillData(Data.current_PhaseB)
                        , Commons.Validate_BillData(Data.current_PhaseC)
                        , Commons.Validate_BillData(Data.voltage_PhaseA)
                        , Commons.Validate_BillData(Data.voltage_PhaseB)
                        , Commons.Validate_BillData(Data.voltage_PhaseC)
                        );
                }
                //Both Non Zero active power Values are not available
                else
                {
                    query = String.Format("INSERT INTO instantaneous_data_live(`f_id`,`msn`, `session_date_time`, `meter_date_time`, `active_pwr_pos_phase_total`, `active_pwr_neg_phase_total`, `reactive_pwr_pos_phase_total`, `reactive_pwr_neg_phase_total`, `voltage_phase_total`,`current_phase_total`, `frequency`, `connection_time`, `type`,`ct`,`pt`,current_phase_a,current_phase_b,current_phase_c,voltage_phase_a,voltage_phase_b,voltage_phase_c) VALUES('{14}','{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}','{12}','{13}','{14}', '{15}', '{16}', '{17}','{18}','{19}')"
                        , Data.MSN
                        , SessionDateTime.ToString(DateFormat)
                        , Data.dateTime.GetDateTime().ToString(DateFormat)
                        , Commons.Validate_BillData(Data.active_powerPositive_PhaseTL)
                        , Commons.Validate_BillData(Data.active_powerNegative_PhaseTL)
                        , Commons.Validate_BillData(Data.reactive_powerPositive_PhaseTL)
                        , Commons.Validate_BillData(Data.reactive_powerNegative_PhaseTL)
                        , Commons.Validate_BillData(Data.voltage_PhaseTL)
                        , Commons.Validate_BillData(Data.current_PhaseTL)
                        , Commons.Validate_BillData(Data.frequency)
                        , ConnectionTime.ToString(DateFormat)
                        , (int)MeterInfo.MeterType_OBJ
                        , MeterInfo.CT
                        , MeterInfo.PT
                        , MeterInfo.MeterID
                        , Commons.Validate_BillData(Data.current_PhaseA)
                        , Commons.Validate_BillData(Data.current_PhaseB)
                        , Commons.Validate_BillData(Data.current_PhaseC)
                        , Commons.Validate_BillData(Data.voltage_PhaseA)
                        , Commons.Validate_BillData(Data.voltage_PhaseB)
                        , Commons.Validate_BillData(Data.voltage_PhaseC)
                        );
                }

                OdbcCommand Command = new OdbcCommand(query, Connection);
                if (ExecuteCommandWithAlreadyOpenedConnection(Command))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw new Exception("Error while inserting Instantaneous Live Data", ex);
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public bool InsertInstantaneous_Live_SinglePhase(long feeder_id, Instantaneous_Class Data, DateTime SessionDateTime, DateTime ConnectionTime, int Type)
        {
            try
            {
                DBConnect.OpenConnection();
                string query = "";
                //Both Non Zero active power Values are available
                if (Data.active_powerPositive_PhaseA > 0)
                {

                    query = String.Format("INSERT INTO instantaneous_data_live(`f_id`,`msn`, `session_date_time`, `meter_date_time`, `active_pwr_pos_phase_total`, `non_zero_active_pwr_pos_phase_total`, `voltage_phase_total`,`current_phase_total`, `frequency`, `connection_time`, `type`,`session_time_active_power_positive_TL`) VALUES('{11}','{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}')"
                        , Data.MSN
                        , SessionDateTime.ToString(DateFormat)
                        , Data.dateTime.GetDateTime().ToString(DateFormat)
                        , Commons.Validate_BillData(Data.active_powerPositive_PhaseA)
                        , Commons.Validate_BillData(Data.active_powerPositive_PhaseA)
                        , Commons.Validate_BillData(Data.voltage_PhaseA)
                        , Commons.Validate_BillData(Data.current_PhaseA)
                        , Commons.Validate_BillData(Data.frequency)
                        , ConnectionTime.ToString(DateFormat)
                        , Type
                        , SessionDateTime.ToString(DateFormat)
                        , feeder_id
                        );
                }

                // active power Values are not available
                else
                {

                    query = String.Format("INSERT INTO instantaneous_data_live(`f_id`,`msn`, `session_date_time`, `meter_date_time`, `active_pwr_pos_phase_total`, `voltage_phase_total`,`current_phase_total`, `frequency`, `connection_time`, `type`) VALUES('{9}','{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')"
                        , Data.MSN
                        , SessionDateTime.ToString(DateFormat)
                        , Data.dateTime.GetDateTime().ToString(DateFormat)
                        , Commons.Validate_BillData(Data.active_powerPositive_PhaseA)
                        , Commons.Validate_BillData(Data.voltage_PhaseA)
                        , Commons.Validate_BillData(Data.current_PhaseA)
                        , Commons.Validate_BillData(Data.frequency)
                        , ConnectionTime.ToString(DateFormat)
                        , Type
                        , feeder_id
                        );
                }

                OdbcCommand Command = new OdbcCommand(query, Connection);
                if (ExecuteCommandWithAlreadyOpenedConnection(Command))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw new Exception("Error while inserting Instantaneous Live Data", ex);
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public bool UpdateInstantaneous_Live_SinglePhase(long feeder_id, Instantaneous_Class Data, DateTime SessionDateTime, DateTime ConnectionTime, int Type)
        {
            try
            {
                string query = "";
                //Both Non Zero active power Values are available
                if (Data.active_powerPositive_PhaseA > 0)
                {
                    query = String.Format("UPDATE instantaneous_data_live SET `session_date_time` = '{0}', `meter_date_time` = '{1}', `active_pwr_pos_phase_total` = '{2}', `non_zero_active_pwr_pos_phase_total` = '{3}', `voltage_phase_total` = '{4}', `current_phase_total` = '{5}', `frequency` = '{6}', `connection_time` = '{7}', Type = '{8}',`session_time_active_power_positive_TL`='{9}',`f_id`='{11}' WHERE msn = '{10}'"
                        , SessionDateTime.ToString(DateFormat)
                        , Data.dateTime.GetDateTime().ToString(DateFormat)
                        , Commons.Validate_BillData(Data.active_powerPositive_PhaseA)
                        , Commons.Validate_BillData(Data.active_powerPositive_PhaseA)
                        , Commons.Validate_BillData(Data.voltage_PhaseA)
                        , Commons.Validate_BillData(Data.current_PhaseA)
                        , Commons.Validate_BillData(Data.frequency)
                        , ConnectionTime.ToString(DateFormat)
                        , Type
                        , SessionDateTime.ToString(DateFormat)
                        , Data.MSN
                        , feeder_id);
                }

                //Both Non Zero active power Values are not available
                else
                {
                    query = String.Format("UPDATE instantaneous_data_live SET `session_date_time` = '{0}', `meter_date_time` = '{1}', `active_pwr_pos_phase_total` = '{2}',  `voltage_phase_total` = '{3}', `current_phase_total` = '{4}', `frequency` = '{5}', `connection_time` = '{6}', `Type` = '{7}',`f_id`='{9}' WHERE msn = '{8}'"
                        , SessionDateTime.ToString(DateFormat)
                        , Data.dateTime.GetDateTime().ToString(DateFormat)
                        , Commons.Validate_BillData(Data.active_powerPositive_PhaseA)
                        , Commons.Validate_BillData(Data.voltage_PhaseA)
                        , Commons.Validate_BillData(Data.current_PhaseA)
                        , Commons.Validate_BillData(Data.frequency)
                        , ConnectionTime.ToString(DateFormat)
                        , Type
                        , Data.MSN
                        , feeder_id);
                }

                OdbcCommand Command = new OdbcCommand(query, Connection);
                if (ExecuteCommandWithAlreadyOpenedConnection(Command))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw new Exception("Error while updating Instantaneous Live Data", ex);
            }

        }

        public bool InsertInstantaneous_Live_ConsumptionData(ConsumptionDataNow Data, DateTime SessionDateTime, long feederId, string msn)
        {
            try
            {
                DBConnect.OpenConnection();
                string query = "";

                query = $"INSERT INTO instantaneous_data_live(`f_id`,`msn`, `session_date_time`, " +
                        $"cost_per_kwh, active_pwr_pos_phase_total, today_kwh_consumption, this_month_kwh_consumption, consumption_modified_time) " +
                        $"VALUES({feederId}, {msn}, '{SessionDateTime.ToString(DateFormat)}', " +
                        $"{Data.consumptionDataNowArr[0].Price}, {Data.consumptionDataNowArr[0].Energy}, {Data.consumptionDataNowArr[1].Energy}, " +
                        $"{Data.consumptionDataNowArr[2].Energy}, '{Data.consumptionDataNowArr[1].DateTime.ToString(DateFormat)}'" +
                        $") ON DUPLICATE KEY UPDATE " +
                        $"`f_id` = {feederId}, `session_date_time`= '{SessionDateTime.ToString(DateFormat)}', " +
                        $"cost_per_kwh = {Data.consumptionDataNowArr[0].Price}, active_pwr_pos_phase_total = {Data.consumptionDataNowArr[0].Energy}, " +
                        $"today_kwh_consumption = {Data.consumptionDataNowArr[1].Energy}, this_month_kwh_consumption = {Data.consumptionDataNowArr[2].Energy}, " +
                        $"consumption_modified_time = '{Data.consumptionDataNowArr[1].DateTime.ToString(DateFormat)}'";

                OdbcCommand Command = new OdbcCommand(query, Connection);
                if (ExecuteCommandWithAlreadyOpenedConnection(Command))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw new Exception("Error while inserting Instantaneous Live Consumption Data", ex);
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public bool UpdateLoadProfile_Live(string MSN, LoadProfile_Live Data, DateTime capture_time)
        {
            try
            {
                DBConnect.OpenConnection();
                string query = String.Format("UPDATE instantaneous_data_live SET `lp_channel_1` = '{0}', `lp_channel_2` = '{1}', `lp_channel_3` = '{2}', `lp_channel_4` = '{3}',`last_lp_capture_time`='{4}' WHERE msn = '{5}'", Data.Channel_1, Data.Channel_2, Data.Channel_3, Data.Channel_4, capture_time.ToString(DateFormat), MSN);
                OdbcCommand Command = new OdbcCommand(query, Connection);
                //if (ExecuteCommandWithAlreadyOpenedConnection(Command))
                //  return true;
                Command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw new Exception("Error while updating LP Live: " + ex.Message);
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public bool InsertLoadProfile_Live(string MSN, LoadProfile_Live Data, DateTime SessionDateTime, DateTime capture_time)
        {
            try
            {
                DBConnect.OpenConnection();
                string query = String.Format("INSERT INTO instantaneous_data_live(`msn`, `session_date_time`, `lp_channel_1`, `lp_channel_2`, `lp_channel_3`, `lp_channel_4`,`last_lp_capture_time`) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}','{6}')", MSN, SessionDateTime.ToString(DateFormat), Data.Channel_1, Data.Channel_2, Data.Channel_3, Data.Channel_4, capture_time.ToString(DateFormat));
                OdbcCommand Command = new OdbcCommand(query, Connection);
                if (ExecuteCommandWithAlreadyOpenedConnection(Command))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw new Exception("Error while inserting LP Live: " + ex.Message);
            }

            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public bool UpdateCS_Live(string MSN, DateTime last_cs_time)
        {
            try
            {
                DBConnect.OpenConnection();
                string query = String.Format("UPDATE instantaneous_data_live SET `latest_cs_time`='{0}' WHERE msn = '{1}'", last_cs_time.ToString(DateFormat), MSN);
                OdbcCommand Command = new OdbcCommand(query, Connection);
                //if (ExecuteCommandWithAlreadyOpenedConnection(Command))
                //  return true;
                Command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw new Exception("Error while updating CS Live: " + ex.Message);
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }
        public bool InsertCS_Live(string MSN, DateTime last_cs_time)
        {
            try
            {
                DBConnect.OpenConnection();
                string query = String.Format("Insert into instantaneous_data_live(`msn`,`latest_cs_time`) values ('{0}','{1}')", MSN, last_cs_time.ToString(DateFormat));
                OdbcCommand Command = new OdbcCommand(query, Connection);
                //if (ExecuteCommandWithAlreadyOpenedConnection(Command))
                //  return true;
                Command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw new Exception("Error while updating CS LP Live: " + ex.Message);
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        #endregion

        #region CumulativeBilling
        public bool saveCumulativeBillingDataEx(Cumulative_billing_data Data, DateTime SessionDateTime, MeterInformation MeterInfo)
        {
            try
            {

                string query = String.Format("INSERT  INTO `cumm_billing_data` (`msn`, `session_date_time`, `time`, `date`, `meter_date_time`, {3} `ct`,`pt`,`customer_id`,`global_device_id`) VALUES('{0}', '{1}', CURTIME(), CURDATE(), '{2}', {4} '{5}', '{6}', {7}, '{8}' )"
                 , MeterInfo.MSN
                 , SessionDateTime.ToString(DateFormat)
                 , Data.date.ToString(DateFormat)
                 , Data.DBColumns
                 , Data.DBValues
                 , MeterInfo.CT
                 , MeterInfo.PT
                 , MeterInfo.Customer_ID
                 , MeterInfo.GlobalDeviceId
                 );

                OdbcCommand Command = new OdbcCommand(query, Connection);
                if (ExecuteCommandWithAlreadyOpenedConnection(Command))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw new Exception(String.Format("Error while saving Cumulative Billing Data (Error Code:{0})", (int)MDCErrors.App_Cum_Billing_Save), ex);
            }
        }

        //public bool saveCumulativeBillingData(Cumulative_billing_data Data, DateTime SessionDateTime, MeterInformation MeterInfo)
        //{
        //    try
        //    {
        //        string query = string.Empty;
        //        query = $"INSERT INTO `cumm_billing_data` (`customer_id`, `msn`, `global_device_id`, `session_date_time`, `time`, `date`,`meter_date_time`, {Data.DBColumns.ToString().TrimEnd(',')}) "
        //              + $" VALUES ({MeterInfo.Customer_ID}, '{MeterInfo.MSN}', '{MeterInfo.GlobalDeviceId}', '{SessionDateTime.ToString(DateFormat)}', CURTIME(), CURDATE(),"
        //              + $" '{Data.date.ToString(DateFormat)}', {Data.DBValues.ToString().TrimEnd(',')})";

        //        OdbcCommand Command = new OdbcCommand(query, Connection);
        //        var rslt = ExecuteCommandWithAlreadyOpenedConnection(Command);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (_newException != null) _newException(ex);
        //            throw new Exception(String.Format("Error while saving Cumulative Billing Data (Error Code:{0})", (int)MDCErrors.App_Cum_Billing_Save), ex);
        //    }
        //    //try
        //    //{

        //    //    string query = String.Format("INSERT  INTO `cumm_billing_data` (`msn`, `session_date_time`, `time`, `date`, `meter_date_time`, `active_energy_t1`, `active_energy_t2`, `active_energy_t3`, `active_energy_t4`, `active_energy_tl`, `reactive_energy_t1`, `reactive_energy_t2`, `reactive_energy_t3`, `reactive_energy_t4`, `reactive_energy_tl`, `active_mdi_t1`, `active_mdi_t2`, `active_mdi_t3`, `active_mdi_t4`, `active_mdi_tl`,`ct`,`pt`,`customer_id`,`global_device_id`) VALUES('{0}', '{1}', CURTIME(), CURDATE(), '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}','{18}','{19}',{20} , '{21}' )"
        //    //    , Data.msn
        //    //    , SessionDateTime.ToString(DateFormat)
        //    //    , Data.date.ToString(DateFormat)
        //    //    , Commons.Validate_BillData(Data.activeEnergy_T1)
        //    //    , Commons.Validate_BillData(Data.activeEnergy_T2)
        //    //    , Commons.Validate_BillData(Data.activeEnergy_T3)
        //    //    , Commons.Validate_BillData(Data.activeEnergy_T4)
        //    //    , Commons.Validate_BillData(Data.activeEnergy_TL)
        //    //    , Commons.Validate_BillData(Data.reactiveEnergy_T1)
        //    //    , Commons.Validate_BillData(Data.reactiveEnergy_T2)
        //    //    , Commons.Validate_BillData(Data.reactiveEnergy_T3)
        //    //    , Commons.Validate_BillData(Data.reactiveEnergy_T4)
        //    //    , Commons.Validate_BillData(Data.reactiveEnergy_TL)
        //    //    , Commons.Validate_BillData(Data.activeMDI_T1)
        //    //    , Commons.Validate_BillData(Data.activeMDI_T2)
        //    //    , Commons.Validate_BillData(Data.activeMDI_T3)
        //    //    , Commons.Validate_BillData(Data.activeMDI_T4)
        //    //    , Commons.Validate_BillData(Data.activeMDI_TL)
        //    //    , MeterInfo.CT
        //    //    , MeterInfo.PT
        //    //    , MeterInfo.Customer_ID
        //    //    , MeterInfo.GlobalDeviceId
        //    //    );

        //    //    OdbcCommand Command = new OdbcCommand(query, Connection);
        //    //    if (ExecuteCommandWithAlreadyOpenedConnection(Command))
        //    //        return true;
        //    //    else
        //    //        return false;
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    if (_newException != null) _newException(ex);
        //    //    throw new Exception(String.Format("Error while saving Cumulative Billing Data (Error Code:{0})", (int)MDCErrors.App_Cum_Billing_Save), ex);
        //    //}
        //}

        public bool saveCumulativeBillingForPrepaid(Cumulative_billing_data Data, DateTime SessionDateTime, MeterInformation MeterInfo)
        {
            try
            {
                //DBConnect.OpenConnection();
                string query = string.Empty;
                query = String.Format("INSERT INTO cumulative_billing_prepaid(`msn`, `reading_date_time`, `TL_kwh`, `T1_kwh`, `T2_kwh`, `T3_kwh`, `T4_kwh`, `TL_kvarh`, `T1_kvarh`, `T2_kvarh`, `T3_kvarh`, `T4_kvarh`, `TL_MDI_kw`, `T1_MDI_kw`, `T2_MDI_kw`, `T3_MDI_kw`, `T4_MDI_kw`) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}')",
                Data.msn,
                SessionDateTime.ToString(DateFormat),
                Commons.Validate_BillData(Data.activeEnergy_TL),
                Commons.Validate_BillData(Data.activeEnergy_T1),
                Commons.Validate_BillData(Data.activeEnergy_T2),
                Commons.Validate_BillData(Data.activeEnergy_T3),
                Commons.Validate_BillData(Data.activeEnergy_T4),
                Commons.Validate_BillData(Data.reactiveEnergy_TL),
                Commons.Validate_BillData(Data.reactiveEnergy_T1),
                Commons.Validate_BillData(Data.reactiveEnergy_T2),
                Commons.Validate_BillData(Data.reactiveEnergy_T3),
                Commons.Validate_BillData(Data.reactiveEnergy_T4),
                Commons.Validate_BillData(Data.activeMDI_TL),
                Commons.Validate_BillData(Data.activeMDI_T1),
                Commons.Validate_BillData(Data.activeMDI_T2),
                Commons.Validate_BillData(Data.activeMDI_T3),
                Commons.Validate_BillData(Data.activeMDI_T4)
                );

                OdbcCommand Command = new OdbcCommand(query, Connection);
                if (ExecuteCommandWithAlreadyOpenedConnection(Command))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw new Exception("Error while saving Cumulative Billing Data for Prepaid Meter: " + ex.Message);
            }
            finally
            {
                //DBConnect.CloseConnection();
            }
        }

        public bool saveCumulativeBillingForPrepaid_SinglePhase(Cumulative_billing_data Data, DateTime SessionDateTime, MeterInformation meterInfo)
        {
            try
            {
                DBConnect.OpenConnection();
                string query = string.Empty;

                query = String.Format("INSERT INTO cumulative_billing_prepaid(`msn`, `reading_date_time`, `TL_kwh`, `TL_MDI_kw`,`customer_id`) VALUES ('{0}','{1}','{2}','{3}' ,{4})",
                    Data.msn,
                    SessionDateTime.ToString(DateFormat),
                    Data.activeEnergy_T1,
                    Data.activeMDI_T1,
                    meterInfo.Customer_ID);

                OdbcCommand Command = new OdbcCommand(query, Connection);
                if (ExecuteCommandWithAlreadyOpenedConnection(Command))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw new Exception("Error while saving Cumulative Billing Data for Prepaid Meter: " + ex.Message);
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        //public bool UpdateCumulativeEnergy_Live(string msn, double activeEnergy_TL, double reactiveEnergy_TL, DateTime sessTimeEnergy)
        //{
        //    try
        //    {
        //        string query = string.Empty;

        //        query = String.Format("UPDATE instantaneous_data_live SET `session_time_energy` = '{0}', `active_energy_tl` = '{1}', `reactive_energy_tl` = '{2}' WHERE msn = '{3}'"
        //            , sessTimeEnergy.ToString(DateFormat)
        //            , Commons.Validate_BillData(activeEnergy_TL)
        //            , Commons.Validate_BillData(reactiveEnergy_TL)
        //            , msn);


        //        OdbcCommand Command = new OdbcCommand(query, Connection);
        //        if (ExecuteCommandWithAlreadyOpenedConnection(Command))
        //            return true;
        //        else
        //            return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error while updating Instantaneous Live Data", ex);
        //    }
        //}

        public bool UpdateCumulativeEnergy_Live(Cumulative_billing_data Data, DateTime SessionDateTime, MeterInformation MeterInfo)
        {
            try
            {
                //string query = String.Format(@"INSERT  INTO `cumm_billing_data_live` (
                //                                     `msn`, `session_date_time`, `time`, `date`, `meter_date_time`, 
                //                                     `active_energy_t1`, `active_energy_t2`, `active_energy_t3`, `active_energy_t4`, `active_energy_tl`, 
                //                                     `reactive_energy_t1`, `reactive_energy_t2`, `reactive_energy_t3`, `reactive_energy_t4`, `reactive_energy_tl`, 
                //                                     `active_mdi_t1`, `active_mdi_t2`, `active_mdi_t3`, `active_mdi_t4`, `active_mdi_tl`,`ct`,`pt`,`customer_id`)

                //                               VALUES ('{0}', '{1}', CURTIME(), CURDATE(), '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', 
                //                                      '{12}', '{13}', '{14}', '{15}', '{16}', '{17}','{18}','{19}',{20})

                //                               ON DUPLICATE KEY UPDATE
                //                                     `session_date_time` = VALUES(`session_date_time`), `time` = VALUES(`time`), `date` = VALUES(`date`), 
                //                                     `meter_date_time` = VALUES(`meter_date_time`), 
                //                                     `active_energy_t1` = VALUES(`active_energy_t1`), `active_energy_t2` = VALUES(`active_energy_t2`), 
                //                                     `active_energy_t3` = VALUES(`active_energy_t3`), `active_energy_t4` = VALUES(`active_energy_t4`), 
                //                                     `active_energy_tl` = VALUES(`active_energy_tl`), 
                //                                     `reactive_energy_t1` = VALUES(`reactive_energy_t1`), `reactive_energy_t2` = VALUES(`reactive_energy_t2`), 
                //                                     `reactive_energy_t3` = VALUES(`reactive_energy_t3`), `reactive_energy_t4` = VALUES(`reactive_energy_t4`), 
                //                                     `reactive_energy_tl` = VALUES(`reactive_energy_tl`), 
                //                                     `active_mdi_t1` = VALUES(`active_mdi_t1`), `active_mdi_t2` = VALUES(`active_mdi_t2`), 
                //                                     `active_mdi_t3` = VALUES(`active_mdi_t3`), `active_mdi_t4` = VALUES(`active_mdi_t4`), 
                //                                     `active_mdi_tl` = VALUES(`active_mdi_tl`),
                //                                     `ct` = VALUES(`ct`), `pt` = VALUES(`pt`), `customer_id` = VALUES(`customer_id`)
                //                                     "
                //                                    , Data.msn
                //                                    , SessionDateTime.ToString(DateFormat)
                //                                    , Data.date.ToString(DateFormat)
                //                                    , Commons.Validate_BillData(Data.activeEnergy_T1)
                //                                    , Commons.Validate_BillData(Data.activeEnergy_T2)
                //                                    , Commons.Validate_BillData(Data.activeEnergy_T3)
                //                                    , Commons.Validate_BillData(Data.activeEnergy_T4)
                //                                    , Commons.Validate_BillData(Data.activeEnergy_TL)
                //                                    , Commons.Validate_BillData(Data.reactiveEnergy_T1)
                //                                    , Commons.Validate_BillData(Data.reactiveEnergy_T2)
                //                                    , Commons.Validate_BillData(Data.reactiveEnergy_T3)
                //                                    , Commons.Validate_BillData(Data.reactiveEnergy_T4)
                //                                    , Commons.Validate_BillData(Data.reactiveEnergy_TL)
                //                                    , Commons.Validate_BillData(Data.activeMDI_T1)
                //                                    , Commons.Validate_BillData(Data.activeMDI_T2)
                //                                    , Commons.Validate_BillData(Data.activeMDI_T3)
                //                                    , Commons.Validate_BillData(Data.activeMDI_T4)
                //                                    , Commons.Validate_BillData(Data.activeMDI_TL)
                //                                    , MeterInfo.CT
                //                                    , MeterInfo.PT
                //                                    , MeterInfo.Customer_ID
                //                                    );

                string query = $"INSERT INTO `cumm_billing_data_live`(`customer_id`, `msn`, `global_device_id`, `session_date_time`, `time`, `date`,{Data.DBColumns} `meter_date_time` )"
                             + $" VALUES ({MeterInfo.Customer_ID}, '{MeterInfo.MSN}', '{MeterInfo.GlobalDeviceId}', '{SessionDateTime.ToString(DateFormat)}',"
                             + $" CURTIME(), CURDATE(),  {Data.DBValues} '{Data.date.ToString(DateFormat)}')"
                             + $" ON DUPLICATE KEY UPDATE"
                             + $" `session_date_time`= '{SessionDateTime.ToString(DateFormat)}', `time`=CURTIME(), `date`=CURDATE(),  {Data.DB_Columns_Values} `meter_date_time`='{Data.date.ToString(DateFormat)}'";

                OdbcCommand Command = new OdbcCommand(query, Connection);
                if (ExecuteCommandWithAlreadyOpenedConnection(Command))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw new Exception(String.Format("Error while saving live Cumulative Billing Data (Error Code:{0})", (int)MDCErrors.App_Cum_Billing_Save), ex);
            }
        }

        #region Save / Update Cumulative Read by OBIS List

        //public bool InsertCumlativeBilling_byObisList(Dictionary<string, double> Read_Values, DateTime SessionDateTime, MeterInformation MeterInfo, DateTime meterDateTime)
        //{
        //    try
        //    {
        //        string query = string.Empty;

        //        GetColumnsValuesString(Read_Values, out string columnsList, out string valuesList, out string columnsValuesList);

        //        query = $"INSERT INTO `cumm_billing_data` (`customer_id`, `msn`, `global_device_id`, `session_date_time`, `time`, `date`,`meter_date_time`, {columnsList}) "
        //              + $" VALUES ({MeterInfo.Customer_ID}, '{MeterInfo.MSN}', '{MeterInfo.GlobalDeviceId}', '{SessionDateTime.ToString(DateFormat)}', CURTIME(), CURDATE(),"
        //              + $" '{meterDateTime.ToString(DateFormat)}', {valuesList})";

        //        OdbcCommand Command = new OdbcCommand(query, Connection);
        //        var rslt = ExecuteCommandWithAlreadyOpenedConnection(Command);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public bool Insert_Update_CumlativeBilling_Live_byObisList(Dictionary<string, double> Read_Values, DateTime SessionDateTime, MeterInformation MeterInfo, DateTime meterDateTime)
        //{
        //    try
        //    {
        //        DBConnect.OpenConnection();
        //        string query = "";
        //        GetColumnsValuesString(Read_Values, out string columnsList, out string valuesList, out string columnsValuesList);

        //        query = $"INSERT INTO `cumm_billing_data_live`(`customer_id`, `msn`, `global_device_id`, `session_date_time`, `time`, `date`, `meter_date_time`, {columnsList})"
        //              + $" VALUES ({MeterInfo.Customer_ID}, '{MeterInfo.MSN}', '{MeterInfo.GlobalDeviceId}', '{SessionDateTime.ToString(DateFormat)}',"
        //              + $" CURTIME(), CURDATE(), '{meterDateTime.ToString(DateFormat)}', {valuesList})"
        //              + $" ON DUPLICATE KEY UPDATE"
        //              + $" `session_date_time`= '{SessionDateTime.ToString(DateFormat)}', `time`=CURTIME(), `date`=CURDATE(), `meter_date_time`='{meterDateTime.ToString(DateFormat)}', {columnsValuesList}";

        //        OdbcCommand Command = new OdbcCommand(query, Connection);
        //        if (ExecuteCommandWithAlreadyOpenedConnection(Command))
        //            return true;
        //        else
        //            return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (_newException != null) _newException(ex);
        //        throw new Exception("Error while inserting Instantaneous Live Consumption Data", ex);
        //    }
        //    finally
        //    {
        //        DBConnect.CloseConnection();
        //    }
        //}

        #endregion Save / Update Cumulative Read by OBIS List

        #endregion

        #region MonthlyBilling

        public CustomException SaveMonthlyBillingDataWithReplaceEx(Monthly_Billing_data MB_data, long meterCount, DateTime SessionDateTime, MeterInformation meterInfo, MeterInfoUpdateFlags flags)
        {
            var cException = new CustomException();
            long currentCounter = 0;
            OdbcTransaction transaction = null;
            try
            {
                DBConnect.OpenConnection();
                OdbcCommand Command = new OdbcCommand();
                StringBuilder mbQuery = new StringBuilder();
                int bulkInsert = 0;

                MB_data.monthly_billing_data.Sort((x, y) => x.Counter.CompareTo(y.Counter));

                transaction = (Connection).BeginTransaction();

                // Assign transaction object for a pending local transaction.
                Command.Connection = Connection;
                Command.Transaction = transaction;

                //Command.Connection = Connection;
                var prefix = "INSERT INTO";
                var postfix = " `monthly_billing` (`msn`, `session_date_time`, `time`, `date`, `meter_date_time`, `period_count`, {0} `ct`,`pt`,`customer_id`,`tariff` , `global_device_id`) VALUES";
                if (MB_data.monthly_billing_data.Count > 0)
                    postfix = string.Format(postfix, MB_data.monthly_billing_data[0].billData_obj.DBColumns);
                mbQuery.Append(postfix);
                for (int i = 0; i < MB_data.monthly_billing_data.Count; i++, bulkInsert++)
                {
                    #region Partial Insert
                    if (bulkInsert > 100)
                    {
                        switch (meterInfo.MB_Counters.InvalidUpdate)
                        {
                            case 0:
                                prefix = "INSERT INTO";
                                break;
                            case 1:
                                prefix = "INSERT IGNORE";
                                break;
                            default:
                                prefix = "REPLACE INTO";
                                break;
                        }
                        Command.CommandText = prefix;
                        Command.CommandText += mbQuery.ToString().Trim(',');
                        ExecuteCommandWithAlreadyOpenedConnection(Command);
                        mbQuery.Clear();
                        mbQuery.Append(postfix);
                        bulkInsert = 0;
                    }
                    #endregion

                    #region Making Entries
                    mbQuery.Append(String.Format("('{0}', '{1}', CURTIME(), CURDATE(), '{2}', '{3}', {4}, '{5}', '{6}', {7},'{8}' , '{9}' ),"
                                                            , meterInfo.MSN
                                                            , SessionDateTime.ToString(DateFormat)
                                                            , MB_data.monthly_billing_data[i].billData_obj.date.ToString(DateFormat)
                                                            , MB_data.monthly_billing_data[i].Counter
                                                            , MB_data.monthly_billing_data[i].billData_obj.DBValues.ToString().Trim(',')
                                                            , meterInfo.CT
                                                            , meterInfo.PT
                                                            , meterInfo.Customer_ID
                                                            , MB_data.MeterTariffCode
                                                            , meterInfo.GlobalDeviceId
                                      ));
                    // Counter Validation
                    currentCounter = MB_data.monthly_billing_data[i].Counter;
                    if (currentCounter > meterCount)
                    {
                        cException.SomeMessage = "Invalid Monthly Billing Counter Received in data";
                        cException.SomeNumber = currentCounter;
                        cException.isTrue = false;
                        throw new Exception(cException.SomeMessage);
                    }
                    #endregion
                }
                #region Main Execution

                if (bulkInsert > 0)
                {
                    switch (meterInfo.MB_Counters.InvalidUpdate)
                    {
                        case 0:
                            prefix = "INSERT INTO";
                            break;
                        case 1:
                            prefix = "INSERT IGNORE";
                            break;
                        default:
                            prefix = "REPLACE INTO";
                            break;
                    }
                    Command.CommandText = prefix;
                    Command.CommandText += mbQuery.ToString().Trim(',');
                    ExecuteCommandWithAlreadyOpenedConnection(Command);
                    mbQuery.Clear();
                    mbQuery.Append(postfix);
                    bulkInsert = 0;
                }
                #endregion
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (_newException != null) _newException(ex);
                #region Duplicate Entey Handling
                if (ex.Message.Contains("Duplicate entry"))
                {
                    cException.Ex = ex;
                    cException.isTrue = false;
                    cException.SomeMessage = string.Format("Error:{0} *", (int)MDCErrors.DB_Duplicate_Entery);
                }
                #endregion
                else
                {
                    cException.Ex = ex;
                    cException.isTrue = false;
                    cException.SomeMessage = ex.Message;
                }
            }
            finally
            {
                try
                {
                    if (transaction != null) transaction.Dispose();
                }
                catch
                {
                }
                DBConnect.CloseConnection();
                #region Counter Update
                //if (cException.isTrue && currentCounter > 0)
                //{
                //    meterInfo.MonthlyBillingCounterToUpdate = currentCounter;
                //    flags.UpdateMBCounter = true;
                //}
                #endregion
            }
            return cException;
        }
        public CustomException SaveMonthlyBillingDataWithReplace(Monthly_Billing_data MB_data, long meterCount, DateTime SessionDateTime, MeterInformation meterInfo, MeterInfoUpdateFlags flags)
        {
            var cException = new CustomException();
            long currentCounter = 0;
            OdbcTransaction transaction = null;
            try
            {
                DBConnect.OpenConnection();
                OdbcCommand Command = new OdbcCommand();
                StringBuilder mbQuery = new StringBuilder();
                int bulkInsert = 0;

                MB_data.monthly_billing_data.Sort((x, y) => x.Counter.CompareTo(y.Counter));

                transaction = (Connection).BeginTransaction();

                // Assign transaction object for a pending local transaction.
                Command.Connection = Connection;
                Command.Transaction = transaction;

                //Command.Connection = Connection;
                var prefix = "INSERT INTO";
                var postfix = " `monthly_billing` (`msn`, `session_date_time`, `time`, `date`, `meter_date_time`, `period_count`, `active_energy_t1`, `active_energy_t2`, `active_energy_t3`, `active_energy_t4`, `active_energy_tl`, `reactive_energy_t1`, `reactive_energy_t2`, `reactive_energy_t3`, `reactive_energy_t4`, `reactive_energy_tl`, `active_mdi_t1`, `active_mdi_t2`, `active_mdi_t3`, `active_mdi_t4`, `active_mdi_tl`, `ct`,`pt`,`customer_id`,`tariff`, `global_device_id`) VALUES";
                mbQuery.Append(postfix);
                for (int i = 0; i < MB_data.monthly_billing_data.Count; i++, bulkInsert++)
                {
                    #region Partial Insert
                    if (bulkInsert > 100)
                    {
                        switch (meterInfo.MB_Counters.InvalidUpdate)
                        {
                            case 0:
                                prefix = "INSERT INTO";
                                break;
                            case 1:
                                prefix = "INSERT IGNORE";
                                break;
                            default:
                                prefix = "REPLACE INTO";
                                break;
                        }
                        Command.CommandText = prefix;
                        Command.CommandText += mbQuery.ToString().Trim(',');
                        ExecuteCommandWithAlreadyOpenedConnection(Command);
                        mbQuery.Clear();
                        mbQuery.Append(postfix);
                        bulkInsert = 0;
                    }
                    #endregion

                    #region Making Entries
                    mbQuery.Append(String.Format("('{0}', '{1}', CURTIME(), CURDATE(), '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', {21},'{22}', '{23}' ),"
                                                            , meterInfo.MSN
                                                            , SessionDateTime.ToString(DateFormat)
                                                            , MB_data.monthly_billing_data[i].billData_obj.date.ToString(DateFormat)
                                                            , MB_data.monthly_billing_data[i].Counter
                                                            , MB_data.monthly_billing_data[i].billData_obj.activeEnergy_T1
                                                            , MB_data.monthly_billing_data[i].billData_obj.activeEnergy_T2
                                                            , MB_data.monthly_billing_data[i].billData_obj.activeEnergy_T3
                                                            , MB_data.monthly_billing_data[i].billData_obj.activeEnergy_T4
                                                            , MB_data.monthly_billing_data[i].billData_obj.activeEnergy_TL
                                                            , MB_data.monthly_billing_data[i].billData_obj.reactiveEnergy_T1
                                                            , MB_data.monthly_billing_data[i].billData_obj.reactiveEnergy_T2
                                                            , MB_data.monthly_billing_data[i].billData_obj.reactiveEnergy_T3
                                                            , MB_data.monthly_billing_data[i].billData_obj.reactiveEnergy_T4
                                                            , MB_data.monthly_billing_data[i].billData_obj.reactiveEnergy_TL
                                                            , MB_data.monthly_billing_data[i].billData_obj.activeMDI_T1
                                                            , MB_data.monthly_billing_data[i].billData_obj.activeMDI_T2
                                                            , MB_data.monthly_billing_data[i].billData_obj.activeMDI_T3
                                                            , MB_data.monthly_billing_data[i].billData_obj.activeMDI_T4
                                                            , MB_data.monthly_billing_data[i].billData_obj.activeMDI_TL
                                                            , meterInfo.CT
                                                            , meterInfo.PT
                                                            , meterInfo.Customer_ID
                                                            , MB_data.MeterTariffCode
                                                            , meterInfo.GlobalDeviceId
                                      ));
                    // Counter Validation
                    currentCounter = MB_data.monthly_billing_data[i].Counter;
                    if (currentCounter > meterCount)
                    {
                        cException.SomeMessage = "Invalid Monthly Billing Counter Received in data";
                        cException.SomeNumber = currentCounter;
                        cException.isTrue = false;
                        throw new Exception(cException.SomeMessage);
                    }
                    #endregion
                }
                #region Main Execution

                if (bulkInsert > 0)
                {
                    switch (meterInfo.MB_Counters.InvalidUpdate)
                    {
                        case 0:
                            prefix = "INSERT INTO";
                            break;
                        case 1:
                            prefix = "INSERT IGNORE";
                            break;
                        default:
                            prefix = "REPLACE INTO";
                            break;
                    }
                    Command.CommandText = prefix;
                    Command.CommandText += mbQuery.ToString().Trim(',');
                    ExecuteCommandWithAlreadyOpenedConnection(Command);
                    mbQuery.Clear();
                    mbQuery.Append(postfix);
                    bulkInsert = 0;
                }
                #endregion
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (_newException != null) _newException(ex);
                #region Duplicate Entey Handling
                if (ex.Message.Contains("Duplicate entry"))
                {
                    cException.Ex = ex;
                    cException.isTrue = false;
                    cException.SomeMessage = string.Format("Error:{0} *", (int)MDCErrors.DB_Duplicate_Entery);
                }
                #endregion
                else
                {
                    cException.Ex = ex;
                    cException.isTrue = false;
                    cException.SomeMessage = ex.Message;
                }
            }
            finally
            {
                try
                {
                    if (transaction != null) transaction.Dispose();
                }
                catch
                {
                }
                DBConnect.CloseConnection();
                #region Counter Update
                //if (cException.isTrue && currentCounter > 0)
                //{
                //    meterInfo.MonthlyBillingCounterToUpdate = currentCounter;
                //    flags.UpdateMBCounter = true;
                //}
                #endregion
            }
            return cException;
        }

        public void ReadMeterStatusInfo(Monthly_Billing_data _monthlyBillingData, string _consumerID)
        {
            try
            {
                DBConnect.OpenConnection();
                string query = string.Format("select tariff_code from consumers where consumer_id = '{0}'", _consumerID);
                var dbCommand = new OdbcCommand(query, Connection);
                using (var reader = dbCommand.ExecuteReader())
                {
                    DataTable tblComsumer = new DataTable();
                    tblComsumer.Load(reader);

                    if (tblComsumer.Rows.Count > 0 && tblComsumer.Rows[0]["tariff_code"] != DBNull.Value)
                    {
                        _monthlyBillingData.MeterTariffCode = Convert.ToInt32(tblComsumer.Rows[0]["tariff_code"]);
                        //_monthlyBillingData.CustomerStatusCode = Convert.ToInt32(tblComsumer.Rows[0]["consumer_status_code"]);
                        //DateTime.TryParse(tblComsumer.Rows[0]["consumer_status_date"].ToString(), out _monthlyBillingData.CustomerStatusChangeDate);
                    }
                    else _monthlyBillingData.MeterTariffCode = 0; 
                }
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw ex;
            }

        }
        public bool update_MonthlyBilling_Counter(string MSN, ulong Count)
        {
            try
            {
                DBConnect.OpenConnection();
                string query = String.Format("UPDATE meter SET monthly_billing_counter = '{0}' WHERE msn= '{1}'", Count, MSN);
                OdbcCommand Command = new OdbcCommand(query, Connection);
                if (ExecuteCommandWithAlreadyOpenedConnection(Command))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        #endregion

        #region LoadProfile

        //public CustomException saveLoadProfile(Load_Profile Data, DateTime SessionDateTime, string MSN, long ChannelGroupId, long CountComparer, long MeterCount, long DatabaseCount)
        //{
        //    CustomException cException = new CustomException();
        //    uint PreviousSavedDataCount = 0;
        //    uint CurrentDataCount = 0;
        //    OdbcTransaction transaction = null;
        //    OdbcCommand MyCommand = new OdbcCommand();
        //    MyCommand.Connection = Connection;
        //    try
        //    {

        //        DBConnect.OpenConnection();
        //        // Start a local transaction
        //        transaction = (Connection).BeginTransaction();

        //        // Assign transaction object for a pending local transaction.
        //        MyCommand.Connection = Connection;
        //        MyCommand.Transaction = transaction;

        //        for (int i = 0; i < Data.loadData.Count; i++)
        //        {
        //            #region Save
        //            try
        //            {
        //                MyCommand.CommandText = String.Format("INSERT DELAYED INTO load_profile_data (`msn`, `session_date_time`, `time`, `date`, `meter_date_time`, `load_profile_group_id`, `channel_1_val`, `channel_2_val`, `channel_3_val`, `channel_4_val`, `counter`, `lp_interval`) " +
        //                                                           "VALUES('{0}', '{1}', CURTIME(), CURDATE(), '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')"
        //                                                            , Data.MSN
        //                                                            , SessionDateTime.ToString(DateFormat)
        //                                                            , Data.loadData[i].timeStamp.ToString(DateFormat)
        //                                                            , ChannelGroupId
        //                                                            , Data.loadData[i].value[0]
        //                                                            , Data.loadData[i].value[1]
        //                                                            , Data.loadData[i].value[2]
        //                                                            , Data.loadData[i].value[3]
        //                                                            , Data.loadData[i].counter
        //                                                            , Data.loadData[i].interval);

        //                CurrentDataCount = Data.loadData[i].counter;
        //                if (CurrentDataCount > CountComparer)
        //                {
        //                    cException.isTrue = false;
        //                    cException.SomeMessage = "Data with Invalid Load Profile Counter save try... ";
        //                    break;
        //                }
        //                if (ExecuteCommandWithAlreadyOpenedConnection(MyCommand))
        //                {
        //                    PreviousSavedDataCount = Data.loadData[i].counter;
        //                    try
        //                    {
        //                        MyCommand.CommandText = String.Format("INSERT DELAYED INTO weekly_load_profile_data (`msn`, `session_date_time`, `time`, `date`, `meter_date_time`, `load_profile_group_id`, `channel_1_val`, `channel_2_val`, `channel_3_val`, `channel_4_val`, `counter`, `lp_interval`) " +
        //                            "VALUES('{0}', '{1}', CURTIME(), CURDATE(), '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')"
        //                            , Data.MSN
        //                            , SessionDateTime.ToString(DateFormat)
        //                            , Data.loadData[i].timeStamp.ToString(DateFormat)
        //                            , ChannelGroupId
        //                            , Data.loadData[i].value[0]
        //                            , Data.loadData[i].value[1]
        //                            , Data.loadData[i].value[2]
        //                            , Data.loadData[i].value[3]
        //                            , Data.loadData[i].counter
        //                            , Data.loadData[i].interval);
        //                        ExecuteCommandWithAlreadyOpenedConnection(MyCommand);
        //                    }
        //                    catch (Exception)
        //                    {
        //                    }
        //                    cException.isTrue = true;
        //                }
        //            }
        //            catch (OdbcException ex)
        //            {
        //                if (ex.Number == 1062)
        //                    continue;
        //                else
        //                    break;
        //            }
        //            catch (Exception)
        //            {
        //                cException.isTrue = false;
        //                break;
        //            }
        //            #endregion
        //        }
        //        if (!cException.isTrue)
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        cException.Ex = ex;

        //        cException.isTrue = false;
        //        cException.SomeMessage = ex.Message;
        //    }
        //    finally
        //    {
        //        cException.SomeNumber = (long)PreviousSavedDataCount;
        //        if (PreviousSavedDataCount > MeterCount + 1 || PreviousSavedDataCount < DatabaseCount)
        //        {
        //            cException.isTrue = false;
        //            cException.SomeMessage += "Invalid Load Profile Counter update try";
        //            transaction.Rollback();
        //        }
        //        else
        //        {
        //            update_LoadProfileCounter(MSN, (ulong)PreviousSavedDataCount, MyCommand, ChannelGroupId);//Update Counter to DB
        //            transaction.Commit();
        //        }
        //        DBConnect.CloseConnection();
        //    }

        //    return cException;
        //}

        public bool update_LoadProfileCounter(string MSN, ulong Count, OdbcCommand MyCommand, long? ChannelGroupId = null)
        {
            try
            {
                MyCommand.CommandText = String.Format("UPDATE meter SET load_profile_counter = '{0}', load_profile_group_id = '{1}' WHERE msn = '{2}'", Count, ChannelGroupId, MSN);
                if (ExecuteUpdateCommand(MyCommand))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }
        }
        public CustomException saveLoadProfileWithReplace(READ_METHOD LP_ReadMethod, Profile_Counter LP_Counter, Load_Profile Data, DateTime SessionDateTime, MeterInformation MeterInfo, LoadProfileScheme lpScheme)
        {
            CustomException cException = new CustomException();
            uint PreviousSavedDataCount = 0;
            uint CurrentDataCount = 0;
            OdbcCommand MyCommand = new OdbcCommand();
            OdbcTransaction transaction = null;
            try
            {
                MyCommand.Connection = Connection;
                DBConnect.OpenConnection();
                transaction = (Connection).BeginTransaction();
                MyCommand.Connection = Connection;
                MyCommand.Transaction = transaction;
                var lpInsert = new StringBuilder();
                //var weeklyUpdate = new StringBuilder();
                var channels_col = new StringBuilder();
                var channels_val = new StringBuilder();
                var lp_table_name = " load_profile";
                var weekly_lp_table_name = " weekly_load_profile_data";
                var lp_prefix = " (`msn`, `session_date_time`, `time`, `date`, `meter_date_time`, `load_profile_group_id`,{0} `counter`, `lp_interval`,`ct`,`pt`,`customer_id`,`load_profile_type`, `global_device_id`) VALUES";
                //var lp_weekly_prefix = " (`msn`, `session_date_time`, `time`, `date`, `meter_date_time`, `load_profile_group_id`,{0} `counter`, `lp_interval`,`ct`,`pt`,`customer_id`,`load_profile_type`) VALUES";
                var prfix = "Insert Into";
                int bulkCounter = 0;

                #region MakeChannelsColumns
                if (Data.loadData.Count > 0)
                {
                    L_Data lData = Data.loadData[0];
                    for (int i = 0; i < lData.value.Count; i++)
                    {
                        channels_col.Append(string.Format("`channel_{0}_val`,", i + 1));
                    }

                }
                lp_prefix = string.Format(lp_prefix, channels_col);
                //lp_weekly_prefix = string.Format(lp_weekly_prefix, channels_col);
                #endregion
                lpInsert.Append(lp_prefix);
                //weeklyUpdate.Append( lp_prefix);

                #region Make Load Profile Query
                for (int i = 0; i < Data.loadData.Count; i++, bulkCounter++)
                {
                    #region Bulk Insert Check
                    if (bulkCounter > 100)
                    {
                        if (LP_Counter.InvalidUpdate == 0)
                            prfix = "INSERT INTO ";
                        else if (LP_Counter.InvalidUpdate == 1)
                            prfix = "INSERT IGNORE ";
                        else
                            prfix = "REPLACE INTO ";
                        MyCommand.CommandText = prfix + lp_table_name;
                        MyCommand.CommandText += lpInsert.ToString().Trim(',');
                        if (ExecuteCommandWithAlreadyOpenedConnection(MyCommand) && InsertWeeklyLoadProfile)
                        {
                            MyCommand.CommandText = prfix + weekly_lp_table_name;
                            MyCommand.CommandText += lpInsert.ToString().Trim(',');
                            ExecuteCommandWithAlreadyOpenedConnection(MyCommand);
                        }
                        //weeklyUpdate.Clear();
                        lpInsert.Clear();
                        //weeklyUpdate.Append(string.Format(lp_weekly_prefix));
                        lpInsert.Append(string.Format(lp_prefix));
                        bulkCounter = 0;
                    }
                    #endregion

                    #region Save
                    try
                    {
                        channels_val.Clear();

                        for (int index = 0; index < Data.loadData[i].value.Count; index++)
                        {
                            channels_val.Append(string.Format("'{0}',", Data.loadData[i].value[index]));
                        }

                        lpInsert.Append(String.Format("('{0}', '{1}', CURTIME(), CURDATE(), '{2}', '{3}', {4} '{5}', '{6}', '{7}', '{8}', {9},'{10}', '{11}' ),"
                                                                    , Data.MSN
                                                                    , SessionDateTime.ToString(DateFormat)
                                                                    , Data.loadData[i].timeStamp.ToString(DateFormat)
                                                                    , LP_Counter.GroupId
                                                                    , channels_val
                                                                    , Data.loadData[i].counter
                                                                    , Data.loadData[i].interval
                                                                    , MeterInfo.CT
                                                                    , MeterInfo.PT
                                                                    , MeterInfo.Customer_ID
                                                                    , (byte)lpScheme
                                                                    , MeterInfo.GlobalDeviceId
                                                                    ));

                        CurrentDataCount = Data.loadData[i].counter;
                        if (LP_ReadMethod == READ_METHOD.ByCounter && CurrentDataCount > LP_Counter.Meter_Counter)
                        {
                            cException.isTrue = false;
                            cException.SomeMessage = "Data with Invalid Load Profile Counter save try... ";
                            break;
                        }

                        PreviousSavedDataCount = Data.loadData[i].counter;
                        cException.SomeNumber = PreviousSavedDataCount;

                        //if (InsertWeeklyLoadProfile)
                        //{
                        //    weeklyUpdate.Append(String.Format("('{0}', '{1}', CURTIME(), CURDATE(), '{2}', '{3}', {4} '{5}', '{6}', '{7}', '{8}', '{9}','{10}'),"
                        //                       , Data.MSN
                        //                       , SessionDateTime.ToString(DateFormat)
                        //                       , Data.loadData[i].timeStamp.ToString(DateFormat)
                        //                       , LP_Counter.GroupId
                        //                       , channels_val
                        //                       , Data.loadData[i].counter
                        //                       , Data.loadData[i].interval
                        //                       , MeterInfo.CT
                        //                       , MeterInfo.PT
                        //                       , MeterInfo.Customer_ID
                        //                       , (byte)lpScheme));
                        //}

                    }
                    catch (Exception ex)
                    {
                        cException.Ex = ex;
                        cException.isTrue = false;
                    }
                    #endregion
                }
                #endregion

                #region Main Query Executor
                if (bulkCounter > 0)
                {
                    if (LP_Counter.InvalidUpdate == 0)
                        prfix = "INSERT INTO ";
                    else if (LP_Counter.InvalidUpdate == 1)
                        prfix = "INSERT IGNORE ";
                    else
                        prfix = "REPLACE INTO ";
                    MyCommand.CommandText = prfix + lp_table_name;
                    MyCommand.CommandText += lpInsert.ToString().Trim(',');
                    if (ExecuteCommandWithAlreadyOpenedConnection(MyCommand) && InsertWeeklyLoadProfile)
                    {
                        MyCommand.CommandText = prfix + weekly_lp_table_name;
                        MyCommand.CommandText += lpInsert.ToString().Trim(',');
                        ExecuteCommandWithAlreadyOpenedConnection(MyCommand);
                    }
                    //weeklyUpdate.Clear();
                    lpInsert.Clear();
                    //weeklyUpdate.Append(string.Format(lp_weekly_prefix));
                    lpInsert.Append(string.Format(lp_prefix));
                }
                #endregion

                transaction.Commit();

            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                if (_newException != null) _newException(ex);
                #region Duplicate Entey Handling
                if (ex.Message.Contains("Duplicate entry"))
                {
                    cException.Ex = ex;
                    cException.isTrue = false;
                    cException.SomeMessage = string.Format("Error:{0} *", (int)MDCErrors.DB_Duplicate_Entery);
                }
                #endregion
                else
                {
                    cException.Ex = ex;
                    cException.isTrue = false;
                    cException.SomeMessage = ex.Message;
                }

            }
            finally
            {

                try
                {
                    if (LP_ReadMethod == READ_METHOD.ByCounter && (PreviousSavedDataCount > LP_Counter.Meter_Counter + 1 || PreviousSavedDataCount < LP_Counter.DB_Counter))
                    {
                        cException.isTrue = false;
                        cException.SomeMessage += "Invalid Load Profile Counter update try";
                        LP_Counter.UpdateCounter = false;

                    }
                    else
                    {
                        //if (PreviousSavedDataCount > 0)
                        //{
                        //update_LoadProfileCounter(MSN, (ulong)PreviousSavedDataCount, MyCommand, ChannelGroupId);//Update Counter to DB
                        //LP_Counter.GroupId = ChannelGroupId;
                        LP_Counter.CounterToUpdate = PreviousSavedDataCount;
                        cException.SomeNumber = PreviousSavedDataCount;
                        LP_Counter.UpdateCounter = true;
                        //}
                        //else
                        //{
                        //    //throw new Exception("Load Profile Counter (PreviousSavedDataCount) =0");
                        //}
                    }
                    if (transaction != null) transaction.Dispose();
                }
                catch (Exception)
                {
                }
                DBConnect.CloseConnection();
            }

            return cException;
        }

        #endregion

        #region LoadProfile3

        public CustomException savePQLoadProfileWithReplace(READ_METHOD ReadLPMethod, Profile_Counter LP_Counter, Load_Profile Data, DateTime SessionDateTime, MeterInformation MeterInfo)
        {
            CustomException cException = new CustomException();
            uint PreviousSavedDataCount = 0;
            uint CurrentDataCount = 0;
            OdbcCommand MyCommand = new OdbcCommand();
            OdbcTransaction transaction = null;
            try
            {
                CurrentDataCount = LP_Counter.DB_Counter;
                MyCommand.Connection = Connection;
                DBConnect.OpenConnection();
                transaction = (Connection).BeginTransaction();
                MyCommand.Connection = Connection;
                MyCommand.Transaction = transaction;
                var lpInsert = new StringBuilder();

                var lp_prefix = " instant_load_profile_data(`msn`, `session_date_time`, `time`, `date`, `meter_date_time`, `current_phase_a`, `current_phase_b`, `current_phase_c`, `current_phase_total`, `voltage_phase_a`, `voltage_phase_b`, `voltage_phase_c`, `voltage_phase_total`, `active_pwr_pos_phase_a`, `active_pwr_pos_phase_b`, `active_pwr_pos_phase_c`, `active_pwr_pos_phase_total`, `active_pwr_neg_phase_a`, `active_pwr_neg_phase_b`, `active_pwr_neg_phase_c`, `active_pwr_neg_phase_total`, `reactive_pwr_pos_phase_a`, `reactive_pwr_pos_phase_b`, `reactive_pwr_pos_phase_c`, `reactive_pwr_pos_phase_total`, `reactive_pwr_neg_phase_a`, `reactive_pwr_neg_phase_b`, `reactive_pwr_neg_phase_c`, `reactive_pwr_neg_phase_total`, `frequency`, `signal_strength`,`ct`,`pt`,`customer_id`) VALUES";
                var prfix = "Insert Into";
                lpInsert.Append(lp_prefix);
                int bulkCounter = 0;


                #region Make Load Profile Query
                for (int i = 0; i < Data.loadData.Count; i++, bulkCounter++)
                {
                    #region Bulk Insert Check
                    if (bulkCounter > 100)
                    {
                        if (MeterInfo.LP3_Counters.InvalidUpdate == 0)
                            prfix = "INSERT INTO ";
                        else if (MeterInfo.LP3_Counters.InvalidUpdate == 1)
                            prfix = "INSERT IGNORE ";
                        else
                            prfix = "REPLACE INTO ";
                        MyCommand.CommandText = prfix;
                        MyCommand.CommandText += lpInsert.ToString().Trim(',');
                        ExecuteCommandWithAlreadyOpenedConnection(MyCommand);
                        lpInsert.Clear();
                        lpInsert.Append(lp_prefix);
                        bulkCounter = 0;
                    }
                    #endregion

                    #region Save
                    try
                    {

                        lpInsert.Append(String.Format(@"('{0}', '{1}', CURTIME(), CURDATE(), '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}', '{22}', '{23}', '{24}', '{25}', '{26}', '{27}', '{28}','{29}','{30}',{31}),"
                                                                    , Data.MSN
                                                                    , SessionDateTime.ToString(DateFormat)
                                                                    , Data.loadData[i].timeStamp.ToString(DateFormat)
                                                                    , Commons.Validate_BillData(Data.loadData[i].value[0])
                                                                    , Commons.Validate_BillData(Data.loadData[i].value[1])
                                                                    , Commons.Validate_BillData(Data.loadData[i].value[2])
                                                                    , 0
                                                                    , Commons.Validate_BillData(Data.loadData[i].value[3])
                                                                    , Commons.Validate_BillData(Data.loadData[i].value[4])
                                                                    , Commons.Validate_BillData(Data.loadData[i].value[5])
                                                                    , 0
                                                                    , Commons.Validate_BillData(Data.loadData[i].value[6])
                                                                    , Commons.Validate_BillData(Data.loadData[i].value[7])
                                                                    , Commons.Validate_BillData(Data.loadData[i].value[8])
                                                                    , Commons.Validate_BillData(Data.loadData[i].value[9])

                                                                    , Commons.Validate_BillData(Data.loadData[i].value[10])
                                                                    , Commons.Validate_BillData(Data.loadData[i].value[11])
                                                                    , Commons.Validate_BillData(Data.loadData[i].value[12])
                                                                    , Commons.Validate_BillData(Data.loadData[i].value[13])

                                                                    , Commons.Validate_BillData(Data.loadData[i].value[14])
                                                                    , Commons.Validate_BillData(Data.loadData[i].value[15])
                                                                    , Commons.Validate_BillData(Data.loadData[i].value[16])
                                                                    , Commons.Validate_BillData(Data.loadData[i].value[17])

                                                                    , Commons.Validate_BillData(Data.loadData[i].value[18])
                                                                    , Commons.Validate_BillData(Data.loadData[i].value[19])
                                                                    , Commons.Validate_BillData(Data.loadData[i].value[20])
                                                                    , Commons.Validate_BillData(Data.loadData[i].value[21])

                                                                    , Commons.Validate_BillData(Data.loadData[i].value[22])
                                                                    , DBNULLParam
                                                                    , MeterInfo.CT
                                                                    , MeterInfo.PT
                                                                    , (MeterInfo.Customer_ID == null ? DBNULLParam : MeterInfo.Customer_ID)));

                        CurrentDataCount = ++CurrentDataCount;
                        if (ReadLPMethod == READ_METHOD.ByCounter && CurrentDataCount > LP_Counter.Meter_Counter)
                        {
                            cException.isTrue = false;
                            cException.SomeMessage = "Data with Invalid Load Profile2 Counter save try... ";
                            break;
                        }

                        PreviousSavedDataCount = CurrentDataCount;
                        cException.SomeNumber = PreviousSavedDataCount;
                    }
                    catch (Exception ex)
                    {
                        cException.Ex = ex;
                        cException.isTrue = false;
                    }
                    #endregion
                }
                #endregion

                #region Main Query Executor
                if (bulkCounter > 0)
                {
                    if (MeterInfo.LP3_Counters.InvalidUpdate == 0)
                        prfix = "INSERT INTO ";
                    else if (MeterInfo.LP3_Counters.InvalidUpdate == 1)
                        prfix = "INSERT IGNORE ";
                    else
                        prfix = "REPLACE INTO ";
                    MyCommand.CommandText = prfix;
                    MyCommand.CommandText += lpInsert.ToString().Trim(',');
                    ExecuteCommandWithAlreadyOpenedConnection(MyCommand);
                    lpInsert.Clear();
                    lpInsert.Append(lp_prefix);
                }
                #endregion

                transaction.Commit();

            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                if (_newException != null) _newException(ex);
                #region Duplicate Entey Handling
                if (ex.Message.Contains("Duplicate entry"))
                {
                    cException.Ex = ex;
                    cException.isTrue = false;
                    cException.SomeMessage = string.Format("Error:{0} *", (int)MDCErrors.DB_Duplicate_Entery);
                }
                #endregion
                else
                {
                    cException.Ex = ex;
                    cException.isTrue = false;
                    cException.SomeMessage = ex.Message;
                }

            }
            finally
            {

                try
                {
                    if (ReadLPMethod == READ_METHOD.ByCounter && (PreviousSavedDataCount > LP_Counter.Meter_Counter + 1 || PreviousSavedDataCount < LP_Counter.DB_Counter))
                    {
                        cException.isTrue = false;
                        cException.SomeMessage += "Invalid Load Profile Counter update try";
                        MeterInfo.LP3_Counters.UpdateCounter = false;

                    }
                    else
                    {
                        //if (PreviousSavedDataCount > 0)
                        //{
                        //update_LoadProfileCounter(MSN, (ulong)PreviousSavedDataCount, MyCommand, ChannelGroupId);//Update Counter to DB
                        MeterInfo.LP3_Counters.CounterToUpdate = PreviousSavedDataCount;
                        cException.SomeNumber = PreviousSavedDataCount;
                        MeterInfo.LP3_Counters.UpdateCounter = true;
                        //}
                        //else
                        //{
                        //    //throw new Exception("Load Profile Counter (PreviousSavedDataCount) =0");
                        //}
                    }
                    if (transaction != null) transaction.Dispose();
                }
                catch (Exception)
                {
                }
                DBConnect.CloseConnection();
            }

            return cException;
        }
        #endregion

        #region Events
        public CustomException saveEventsData(EventData Data, string msn, string reference_no, DateTime SessionDateTime, long CountComparer, long MeterCount, long DatabaseCount, MeterInformation MeterInfo, MeterInfoUpdateFlags flags)
        {
            CustomException cException = new CustomException();
            long PreviousSavedDataCount = 0;
            uint CurrentDataCount = 0;
            OdbcTransaction transaction = null;
            OdbcCommand MyCommand = new OdbcCommand();

            try
            {
                MyCommand.Connection = Connection;
                DBConnect.OpenConnection();
                // Start a local transaction
                transaction = (Connection).BeginTransaction();

                // Assign transaction object for a pending local transaction.
                MyCommand.Connection = Connection;
                MyCommand.Transaction = transaction;

                for (int i = 0; i < Data.EventRecords.Count; i++)
                {
                    try
                    {
                        MyCommand.CommandText = string.Format("INSERT INTO events_data(`session_datetime`,`arrival_time`,`msn`,`date`,`time`,`event_code`,`counter`,`description`,`is_individual`,`reference_no`,`customer_id`, `global_device_id`) VALUES" +
                                "('{0}', '{1}', '{2}', CURDATE(), CURTIME(), '{3}', '{4}', '{5}', '{6}','{7}',{8})" // , {9})"
                                        , SessionDateTime.ToString(DateFormat)
                                        , Data.EventRecords[i].EventDateTimeStamp.ToString(DateFormat)
                                        , msn
                                        , Data.EventRecords[i].EventInfo.EventCode
                                        , Data.EventRecords[i].EventCounter
                                        , Data.EventRecords[i].EventDetailStr
                                        , 0 // We are reading only combine events log
                                        , reference_no
                                        , MeterInfo.Customer_ID
                                        //, MeterInfo.GlobalDeviceId
                                        );

                        CurrentDataCount = Data.EventRecords[i].EventCounter;
                        if (CurrentDataCount > CountComparer)
                        {
                            cException.SomeMessage = "Data with Invalid Events Counter save try... ";
                            cException.isTrue = false;
                            break;
                        }
                        if (ExecuteCommandWithAlreadyOpenedConnection(MyCommand))
                            PreviousSavedDataCount = Data.EventRecords[i].EventCounter;
                    }
                    catch (OdbcException ex)
                    {
                        if (ex.ErrorCode == 1062)
                            continue;
                        else
                            throw ex;
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Duplicate entry"))
                        {
                            if (MeterInfo.EV_Counters.InvalidUpdate == 2)
                            {
                                #region Invalid Event Counter Updates
                                try
                                {
                                    MyCommand.CommandText = string.Format("update events_data set `session_datetime`='{0}',`arrival_time`='{1}',`date`='CURDATE()',"
                                                                + "`time`='CURTIME()',`description`='{2}',`is_individual`='{3}',`reference_no`='{4}' "
                                                                + "where msn='{5}' and `event_code`='{6}' and `counter`='{7}'"
                                                                    , SessionDateTime.ToString(DateFormat)
                                                                    , Data.EventRecords[i].EventDateTimeStamp.ToString(DateFormat)
                                                                    , Data.EventRecords[i].EventDetailStr
                                                                    , 0
                                                                    , reference_no
                                                                    , msn
                                                                    , Data.EventRecords[i].EventInfo.EventCode
                                                                    , Data.EventRecords[i].EventCounter
                                                                    );

                                    ExecuteCommandWithAlreadyOpenedConnection(MyCommand);
                                }
                                catch (Exception)
                                {
                                }
                                #endregion
                            }
                            else if (MeterInfo.EV_Counters.InvalidUpdate == 1)
                            {
                                goto Next;
                            }
                            else if (MeterInfo.EV_Counters.InvalidUpdate == 0)
                            {
                                if (_newException != null) _newException(ex);
                                cException.isTrue = false;
                                cException.SomeMessage = "Error:8686 *";
                                break;
                            }
                        }
                        else
                        {
                            cException.isTrue = false;
                            cException.SomeMessage = ex.Message;
                            break;
                        }
                    }
                    Next: continue;
                }
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                cException.Ex = ex;
                cException.isTrue = false;
                cException.SomeMessage = ex.Message;
            }
            finally
            {
                try
                {
                    cException.SomeNumber = (long)PreviousSavedDataCount;
                    if (PreviousSavedDataCount > MeterCount + 1 || PreviousSavedDataCount < DatabaseCount)
                    {
                        cException.SomeMessage += "Invalid Events Counter update try";
                        cException.isTrue = false;
                        transaction.Rollback();
                    }
                    else
                    {
                        // update_EventCounter(msn, PreviousSavedDataCount, MyCommand);//Update Counter to DB
                        MeterInfo.EvCounterToUpdate = PreviousSavedDataCount;
                        flags.UpdateEventCounters = true;
                        transaction.Commit();
                    }
                }
                catch (Exception)
                {
                }
                DBConnect.CloseConnection();
            }
            return cException;
        }

        public CustomException saveEventsDataWithReplace(EventData Data, string msn, DateTime SessionDateTime, long CountComparer, long MeterCount, long DatabaseCount, MeterInformation MeterInfo, MeterInfoUpdateFlags flags)
        {
            CustomException cException = new CustomException();
            long PreviousSavedDataCount = 0;
            uint CurrentDataCount = 0;
            OdbcCommand MyCommand = new OdbcCommand();
            OdbcTransaction transaction = null;
            bool isUpdate = false;
            try
            {
                DBConnect.OpenConnection();
                transaction = (Connection).BeginTransaction();
                MyCommand.Connection = Connection;
                MyCommand.Transaction = transaction;
                StringBuilder events = new StringBuilder();
                var bulkExecutor = 0;
                var prefix = "INSERT INTO";
                var event_query = " events_data(`session_datetime`,`arrival_time`,`msn`,`date`,`time`,`event_code`,`counter`,`description`,`is_individual`,`customer_id`, `global_device_id`) VALUES";
                #region Making Query

                events.Append(event_query);
                for (int i = 0; i < Data.EventRecords.Count; i++, bulkExecutor++)
                {
                    #region Bulk Handling check

                    if (bulkExecutor > 100)
                    {
                        #region Execute Command
                        if (MeterInfo.EV_Counters.InvalidUpdate == 0)
                            prefix = "INSERT INTO ";
                        else if (MeterInfo.EV_Counters.InvalidUpdate == 1)
                            prefix = "INSERT IGNORE ";
                        else
                            prefix = "REPLACE INTO ";

                        MyCommand.CommandText = prefix;
                        MyCommand.CommandText += events.ToString().Trim(',');
                        isUpdate = ExecuteCommandWithAlreadyOpenedConnection(MyCommand);
                        events.Clear();
                        events.Append(event_query);
                        bulkExecutor = 0;

                        #endregion
                    }

                    #endregion

                    #region Save
                    events.Append(string.Format("('{0}', '{1}', '{2}', CURDATE(), CURTIME(), '{3}', '{4}', '{5}', '{6}', {7}, '{8}' ),"
                                                , SessionDateTime.ToString(DateFormat)
                                                , Data.EventRecords[i].EventDateTimeStamp.ToString(DateFormat)
                                                , msn
                                                , Data.EventRecords[i].EventInfo.EventCode
                                                , Data.EventRecords[i].EventCounter
                                                , Data.EventRecords[i].EventDetailStr
                                                , 0 // We are reading only combine events log
                                                , MeterInfo.Customer_ID
                                                , MeterInfo.GlobalDeviceId
                                                ));

                    CurrentDataCount = Data.EventRecords[i].EventCounter;
                    if (CurrentDataCount > CountComparer)
                    {
                        cException.SomeMessage = "Data with Invalid Events Counter save try... ";
                        cException.isTrue = false;
                        break;
                    }
                    PreviousSavedDataCount = Data.EventRecords[i].EventCounter;

                    #endregion
                }

                #endregion

                #region Execute Command
                if (bulkExecutor > 0)
                {
                    if (MeterInfo.EV_Counters.InvalidUpdate == 0)
                        prefix = "INSERT INTO ";
                    else if (MeterInfo.EV_Counters.InvalidUpdate == 1)
                        prefix = "INSERT IGNORE ";
                    else
                        prefix = "REPLACE INTO ";
                    MyCommand.CommandText = prefix;
                    MyCommand.CommandText += events.ToString().Trim(',');
                    isUpdate = ExecuteCommandWithAlreadyOpenedConnection(MyCommand);
                    events.Clear();
                    events.Append(event_query);
                }
                #endregion

                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                cException.SomeMessage = ex.Message;
                if (ex.Message.Contains("Duplicate entry"))
                    cException.SomeMessage = string.Format("Error:{0} *", (int)MDCErrors.DB_Duplicate_Entery);
                if (_newException != null) _newException(ex);
                cException.Ex = ex;
                cException.isTrue = false;

            }
            finally
            {
                if (cException.isTrue)
                {
                    try
                    {
                        cException.SomeNumber = (long)PreviousSavedDataCount;
                        if (PreviousSavedDataCount > MeterCount + 1 || PreviousSavedDataCount < DatabaseCount)
                        {
                            cException.SomeMessage += "Invalid Events Counter update try";
                            cException.isTrue = false;

                        }
                        else
                        {
                            //update_EventCounter(msn, PreviousSavedDataCount, MyCommand);//Update Counter to DB
                            MeterInfo.EvCounterToUpdate = PreviousSavedDataCount;
                            cException.SomeNumber = PreviousSavedDataCount;
                            flags.UpdateEventCounters = true;

                        }
                        if (transaction != null) transaction.Dispose();
                    }
                    catch (Exception)
                    {
                    }
                }
                DBConnect.CloseConnection();
            }
            return cException;
        }

        public CustomException saveEventsData_Individual(List<EventData> events_Data, string msn, DateTime SessionDateTime, MeterInformation meterInfo)
        {
            CustomException cException = new CustomException();
            // OdbcTransaction transaction = null;
            OdbcCommand MyCommand = new OdbcCommand();
            EventData currentEventData = null;
            MyCommand.Connection = Connection;
            try
            {
                DBConnect.OpenConnection();
                // Start a local transaction
                // transaction = (Connection).BeginTransaction();

                // Assign transaction object for a pending local transaction.
                MyCommand.Connection = Connection;
                // MyCommand.Transaction = transaction;

                for (int k = 0; k < events_Data.Count; k++)
                {
                    currentEventData = events_Data[k];
                    for (int i = 0; i < currentEventData.EventRecords.Count; i++)
                    {
                        try
                        {

                            MyCommand.CommandText = string.Format("INSERT INTO events_data_individual(`session_datetime`,`arrival_time`,`msn`,`date`,`time`,`event_code`,`counter`,`description`,`is_individual`,`customer_id`) VALUES" +
                               "('{0}', '{1}', '{2}', CURDATE(), CURTIME(), '{3}', '{4}', '{5}', '{6}',{7})"
                                      , SessionDateTime.ToString(DateFormat)
                                      , currentEventData.EventRecords[i].EventDateTimeStamp.ToString(DateFormat)
                                      , msn
                                      , currentEventData.EventRecords[i].EventInfo.EventCode
                                      , currentEventData.EventRecords[i].EventCounter
                                      , currentEventData.EventRecords[i].EventDetailStr
                                      , 0
                                      , meterInfo.Customer_ID);

                            ExecuteCommandWithAlreadyOpenedConnection(MyCommand);

                        } // endTry
                        catch (OdbcException ex)
                        {
                            if (ex.ErrorCode == 1062)
                                continue;
                            else
                                throw ex;
                        }
                        catch (Exception)
                        {
                            throw;
                        }

                    } // endFor for single Event

                } // endFor for Entire List

                return cException;

            } // endTry
            catch (Exception ex)
            {
                if (_newException != null)
                    _newException(ex);
                cException.Ex = ex;
                cException.isTrue = false;
                cException.SomeMessage = ex.Message;
                return cException;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public CustomException saveEventsData_IndividualithReplace(List<EventData> events_Data, string msn, DateTime SessionDateTime, MeterInformation meterInfo)
        {
            CustomException cException = new CustomException();
            OdbcCommand MyCommand = new OdbcCommand();
            EventData currentEventData = null;
            MyCommand.Connection = Connection;
            try
            {
                DBConnect.OpenConnection();
                MyCommand.Connection = Connection;
                var events = new StringBuilder();
                var executor = 0;
                events.Append("REPLACE INTO events_data_individual(`session_datetime`,`arrival_time`,`msn`,`date`,`time`,`event_code`,`counter`,`description`,`is_individual`,`customer_id`) VALUES");

                for (int k = 0; k < events_Data.Count; k++)
                {
                    currentEventData = events_Data[k];
                    for (int i = 0; i < currentEventData.EventRecords.Count; i++, executor++)
                    {
                        if (executor > 100)
                        {
                            MyCommand.CommandText = events.ToString().Trim(',');
                            ExecuteCommandWithAlreadyOpenedConnection(MyCommand);
                            executor = 0;
                        }
                        events.Append(string.Format("('{0}', '{1}', '{2}',CURDATE(), CURTIME(),'{3}', '{4}', '{5}', '{6}',{7}),"
                                  , SessionDateTime.ToString(DateFormat)
                                  , currentEventData.EventRecords[i].EventDateTimeStamp.ToString(DateFormat)
                                  , msn
                                  , currentEventData.EventRecords[i].EventInfo.EventCode
                                  , currentEventData.EventRecords[i].EventCounter
                                  , currentEventData.EventRecords[i].EventDetailStr
                                  , 0
                                  , meterInfo.Customer_ID));
                    }

                    if (executor > 0)
                    {
                        MyCommand.CommandText = events.ToString().Trim(',');
                        ExecuteCommandWithAlreadyOpenedConnection(MyCommand);
                    }
                }

                return cException;


            } //endTry
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                cException.Ex = ex;
                cException.isTrue = false;
                cException.SomeMessage = ex.Message;
                return cException;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        /// <summary>
        /// To Save Major Alarm Events Data Individual
        /// </summary>
        /// <param name="SessionDateTime"></param>
        /// <param name="occurenceDateTime"></param>
        /// <param name="ReceptionDateTime"></param>
        /// <param name="msn"></param>
        /// <param name="event_code"></param>
        /// <param name="counter"></param>
        /// <param name="description"></param>
        /// <param name="is_individual"></param>
        /// <param name="reference_no"></param>
        /// <param name="customer_id"></param>
        /// <returns></returns>
        public CustomException saveMajorAlarmEventData_Individual(DateTime SessionDateTime, DateTime occurenceDateTime, DateTime ReceptionDateTime
                                                        , string msn, int event_code, int counter
                                                        , string description, bool is_individual
                                                        , string reference_no, long customer_id)
        {
            CustomException cException = new CustomException();
            // OdbcTransaction transaction = null;
            OdbcCommand MyCommand = new OdbcCommand();

            MyCommand.Connection = Connection;

            try
            {
                if (!DBConnect.IsConnectionOpen)
                    DBConnect.OpenConnection();

                // Assign transaction object for a pending local transaction.
                MyCommand.Connection = Connection;
                // MyCommand.Transaction = transaction;

                try
                {
                    MyCommand.CommandText = string.Format("INSERT INTO events_data_individual(`customer_id`,`session_datetime`,`arrival_time`,`msn`,`reference_no`,`date`,`time`, " +
                                                          " `event_code`,`counter`,`description`,`is_individual`) VALUES " +
                                                          " ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}','{9}','{10}')",
                         customer_id
                         , SessionDateTime.ToString(DateFormat)
                         , occurenceDateTime.ToString(DateFormat)
                         , msn
                         , reference_no
                         , ReceptionDateTime.ToString(DateOnlyFormat)
                         , ReceptionDateTime.ToString(TimeFormat)
                         , event_code
                         , counter
                         , description
                         , Convert.ToByte(is_individual));

                    ExecuteCommandWithAlreadyOpenedConnection(MyCommand);

                } // endTry
                catch (OdbcException ex)
                {
                    if (ex.ErrorCode == 1062)
                        ; // Not Raise Error
                    else
                        throw ex;
                }
                catch (Exception)
                {
                    throw;
                }

                return cException;

            } // endTry
            catch (Exception ex)
            {
                if (_newException != null)
                    _newException(ex);

                cException.Ex = ex;
                cException.isTrue = false;

                cException.SomeMessage = ex.Message;
                return cException;
            }
        }


        public CustomException saveMajorAlarmEventData_Individual(MeterData.events_data_individualDataTable events_Data)
        {
            CustomException cException = new CustomException();
            OdbcCommand MyCommand = new OdbcCommand();
            MeterData.events_data_individualRow currentEventData = null;
            MyCommand.Connection = Connection;
            try
            {
                DBConnect.OpenConnection();
                MyCommand.Connection = Connection;
                var events = new StringBuilder();
                var executor = 0;
                events.Append("REPLACE INTO events_data_individual(`session_datetime`,`arrival_time`,`msn`,`date`,`time`,`event_code`,`counter`,`description`,`is_individual`,`customer_id`) VALUES");

                string session_DT = string.Empty;
                string arrival_time_DT = string.Empty;
                string date_DT = string.Empty;
                string time_DT = string.Empty;

                for (int k = 0; k < events_Data.Count; k++, executor++)
                {
                    currentEventData = events_Data.Rows[k] as MeterData.events_data_individualRow;

                    if (executor > 100)
                    {
                        MyCommand.CommandText = events.ToString().Trim(',');
                        ExecuteCommandWithAlreadyOpenedConnection(MyCommand);
                        executor = 0;
                    }

                    session_DT = (currentEventData.session_datetime != null) ? currentEventData.session_datetime.ToString(DateFormat) : DBNull.Value.ToString();
                    arrival_time_DT = (currentEventData.arrival_time != null) ? currentEventData.arrival_time.ToString(DateFormat) : DBNull.Value.ToString();
                    date_DT = (currentEventData.date != null) ? currentEventData.date.ToString(DateOnlyFormat) : DBNull.Value.ToString();
                    time_DT = (currentEventData.time != null) ? currentEventData.time.ToString(TimeFormat) : DBNull.Value.ToString();

                    events.Append(string.Format("('{0}', '{1}', '{2}','{3}', '{4}', '{5}', '{6}','{7}','{8}','{9}'),"
                              , session_DT
                              , arrival_time_DT
                              , currentEventData.msn

                              , date_DT
                              , time_DT

                              , currentEventData.event_code
                              , currentEventData.counter
                              , currentEventData.description
                              , currentEventData.is_individual
                              //, currentEventData.reference_no
                              , currentEventData.customer_id));

                }


                if (executor > 0)
                {
                    MyCommand.CommandText = events.ToString().Trim(',');
                    ExecuteCommandWithAlreadyOpenedConnection(MyCommand);
                }

                return cException;


            } //endTry
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                cException.Ex = ex;
                cException.isTrue = false;
                cException.SomeMessage = ex.Message;
                return cException;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }



        // old
        public bool saveEventsData(EventData Data, string msn, string reference_no, DateTime SessionDateTime)
        {
            try
            {
                DBConnect.OpenConnection();
                OdbcCommand Command = new OdbcCommand();
                Command.Connection = Connection;
                for (int i = 0; i < Data.EventRecords.Count; i++)
                {
                    try
                    {
                        Command.CommandText = String.Format("INSERT  INTO events_data(`session_datetime`,`arrival_time`,`msn`,`reference_no`,`date`,`time`,`event_code`,`counter`,`description`,`is_individual`) VALUES ('{0}', '{1}', '{2}','{3}', CURDATE(), CURTIME(), '{4}', '{5}', '{6}', '{7}')"
                                        , SessionDateTime.ToString(DateFormat)
                                        , Data.EventRecords[i].EventDateTimeStamp.ToString(DateFormat)
                                        , msn
                                        , reference_no //added by furqan
                                        , Data.EventRecords[i].EventInfo.EventCode
                                        , Data.EventRecords[i].EventCounter
                                        , Data.EventRecords[i].EventDetailStr
                                        , 0 //We are reading only combine events log
                                        );

                        ExecuteCommandWithAlreadyOpenedConnection(Command);
                    }
                    catch (OdbcException ex)
                    {
                        if (ex.ErrorCode == 1062)
                            continue;
                        else
                            throw ex;
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }
        public bool update_EventCounter(string MSN, long Count, OdbcCommand MyCommand)
        {
            try
            {
                string query = String.Format("UPDATE meter SET event_counter= '{0}' WHERE msn = '{1}'", Count, MSN);
                MyCommand.CommandText = query;
                if (ExecuteUpdateCommand(MyCommand))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw;
            }

        }
        //public bool saveEventsData(List<EventData> data, string msn, DateTime SessionDateTime)
        //{

        //    try
        //    {
        //        foreach (EventData item in data)
        //        {
        //            string myInsertQuery = "INSERT DELAYED INTO events_data VALUES ";
        //            for (int i = 0; i < item.EventRecords.Count; i++)
        //            {
        //                int isIdnvidual = (item.EventInfo.EventCode.Equals(0)) ? 1 : 0;
        //                myInsertQuery += String.Format("('{0}','{1}','{2}',CURDATE(),CURTIME(), '{3}', '{4}', '{5}','{6}')",
        //                     SessionDateTime.ToString(DateFormat), item.EventRecords[i].EventDateTimeStamp.ToString(DateFormat), msn
        //                     , item.EventInfo.EventCode.ToString(), item.EventRecords[i].EventCounter, item.EventRecords[i].EventDetailStr, isIdnvidual);
        //                myInsertQuery += " , ";
        //            }
        //            myInsertQuery = myInsertQuery.Remove(myInsertQuery.Length - 2);

        //            ExecuteQuery(myInsertQuery);//Execute
        //        }

        //        return true;
        //    }
        //    catch (OdbcException ex)
        //    {
        //        return false;
        //    }
        //}
        #endregion

        #region Save RGCM Output Status
        public bool SaveRGCMStatus(DateTime SessionDateTime, List<GridStatusItem> RGCM_Status, MeterInformation meterInfo, bool statusChanged)
        {
            try
            {
                #region Populate RGCM Columns and Values
                StringBuilder columnNames = new StringBuilder(50);
                StringBuilder columnValues = new StringBuilder(50);
                if (RGCM_Status.Count == 0) return false;
                for (int i = 0; i < RGCM_Status.Count; i++)
                {
                    columnNames.Append(",input_" + (i + 1).ToString("00"));
                    columnNames.Append(",input_" + (i + 1).ToString("00") + "_time");
                    columnValues.Append("," + RGCM_Status[i].Status);
                    columnValues.Append(",'" + RGCM_Status[i].Time.ToString(DateFormat) + "'");
                }
                #endregion

                #region Header Strings
                string InsertHeader = "INSERT INTO grid_input_status";
                string UpdateHeader = "replace INTO grid_input_status_live";
                #endregion

                DBConnect.OpenConnection();
                string query = string.Format(@"(`customer_id`,`msn`,`session_date_time`, `date`, `time`{0}) 
                                            values({1},'{2}','{3}', CURDATE(), CURTIME(){4})",
                                            Convert.ToString(columnNames),
                                            meterInfo.Customer_ID,//Rashad== 0 ? "NULL" : meterInfo.Customer_ID.ToString(),
                                            meterInfo.MSN,
                                            SessionDateTime.ToString(DateFormat),
                                            columnValues.ToString());


                if (statusChanged)
                    ExecuteQuery(InsertHeader + query, true);
                ExecuteQuery(UpdateHeader + query, true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        #endregion

        #region OLD - User Alarms Response Methods
        //public string GetAlarmUserResponse(string MSN, Param_MajorAlarmProfile majorAlarms, byte status)
        //{
        //    StringBuilder RowIds = new StringBuilder(50);
        //    try
        //    {
        //        DBConnect.OpenConnection();
        //        Dictionary<string, short> eventIds = GetEventIdsForColumns();
        //        StringBuilder MyInsertQuery = new StringBuilder(50);
        //        //============================================================================== 
        //        MyInsertQuery.Append("select alarm_status_response_id"
        //            + ",`imbalance_volt`, `phase_sequence`"
        //            + ", `reverse_polarity`, `phase_fail`, `under_volt`, `over_volt`, `over_current`, `high_neutral_current`, `over_load`"
        //            + ", `reverse_energy`, `tamper_energy`, `ct_fail`, `pt_fail`, `opticalportLogin`, `power_fail`, `power_failEnd`"
        //            + ", `one_wire_tampering`, `meter-on_load`, `meter_on_load_end`, `under_volt_end`, `mdt_exceed`, `system_reset`"
        //            + ", `system_problems`, `mdi_reset`, `parameters`, `password_change`, `customer_code`, `time_change`, `window_sequense_change`"
        //            + ", `over_volt_end`, `bill_register_error`, `param_error`, `power_factor_change`, `battrey_low`, `door_open`, `short_time_power_fail`"
        //            + ", `record_recoverd`, `time_base_event_1`, `time_base_event_2`, `contactor_statusOn`, `contactor_status_off`, `short_time_power_fail_end`"
        //            + ", `reverse_energy_end`, `tamper_energy_end`, `over_load_end`, `reserved_46`, `reserved_47`, `reserved _48`"
        //           + ",`magnetic_feild_end`,`ct_fail_end` ,`pt_fail_end` ,`software_logout`,`reserved_07` ,`reserved_06` ,`reserved_05` ,`reserved_04` ,`reserved_03` ,`reserved_02` ,`reserved_01` ,`reserved_00`"
        //           + " from alarm_status_response where msn = " + MSN + " and status_type = " + status);
        //        //============================================================================== 
        //        OdbcCommand cmd = new OdbcCommand();
        //        cmd.CommandText = MyInsertQuery.ToString();
        //        cmd.Connection = _DBConnect.Connection;
        //        var tbl = GetTableFromDB(cmd);
        //        if (majorAlarms == null) RowIds.ToString();
        //        MajorAlarm userAlarmResponse = null;
        //        string delimiter = ",";
        //        foreach (DataRow row in tbl.Rows)
        //        {
        //            RowIds.Append(row["alarm_status_response_id"]);
        //            RowIds.Append(",");
        //            foreach (DataColumn column in tbl.Columns)
        //            {
        //                if (eventIds.ContainsKey(column.ColumnName) && Convert.ToBoolean(row[column.ColumnName]))
        //                {
        //                    userAlarmResponse = majorAlarms.AlarmItems.Find(x => x.Info._EventId == eventIds[column.ColumnName] + 1);
        //                    if (userAlarmResponse != null)
        //                        userAlarmResponse.IsResetUserStatus = (EnergyMizerAlarmStatus)status;
        //                }
        //            }
        //        }
        //        return RowIds.ToString().Trim(',');
        //    }
        //    catch (Exception)
        //    {
        //        RowIds.Clear();
        //        return RowIds.ToString().Trim(',');
        //    }
        //    finally
        //    {
        //        DBConnect.CloseConnection();
        //    }
        //}

        //public bool SaveUserAlarmResponseLogandDelete(string rowIds, DateTime sessionDateTime)
        //{
        //    try
        //    {
        //        if (!_DBConnect.IsConnectionOpen)
        //            DBConnect.OpenConnection();
        //        string request = string.Empty;
        //        if (string.IsNullOrEmpty(rowIds)) return true;
        //        string query = string.Format(@"INSERT INTO `alarm_status_response_log`(`customer_id`,`msn`,`response_date_time`,`session_date_time`,`imbalance_volt`,`phase_sequence`,`reverse_polarity`,`phase_fail`,`under_volt`,`over_volt`,`over_current`,`high_neutral_current`,`over_load`,`reverse_energy`,`tamper_energy`,`ct_fail`,`pt_fail`,`opticalportLogin`,`power_fail`,`power_failEnd`,`one_wire_tampering`,`meter-on_load`,`meter_on_load_end`,`under_volt_end`,`mdt_exceed`,`system_reset`,`system_problems`,`mdi_reset`,`parameters`,`password_change`,`customer_code`,`time_change`,`window_sequense_change`,`over_volt_end`,`bill_register_error`,`param_error`,`power_factor_change`,`battrey_low`,`door_open`,`short_time_power_fail`,`record_recoverd`,`time_base_event_1`,`time_base_event_2`,`contactor_statusOn`,`contactor_status_off`,`short_time_power_fail_end`,`reverse_energy_end`,`tamper_energy_end`,`over_load_end`,`reserved_46`,`reserved_47`,`reserved _48`,`magnetic_feild_end`,`ct_fail_end`,`pt_fail_end`,`software_logout`,`reserved_07`,`reserved_06`,`reserved_05`,`reserved_04`,`reserved_03`,`reserved_02`,`reserved_01`,`reserved_00`)
        //                                        select `customer_id`,`msn`,`response_date_time`,'{0}',`imbalance_volt`,`phase_sequence`,`reverse_polarity`,`phase_fail`,`under_volt`,`over_volt`,`over_current`,`high_neutral_current`,`over_load`,`reverse_energy`,`tamper_energy`,`ct_fail`,`pt_fail`,`opticalportLogin`,`power_fail`,`power_failEnd`,`one_wire_tampering`,`meter-on_load`,`meter_on_load_end`,`under_volt_end`,`mdt_exceed`,`system_reset`,`system_problems`,`mdi_reset`,`parameters`,`password_change`,`customer_code`,`time_change`,`window_sequense_change`,`over_volt_end`,`bill_register_error`,`param_error`,`power_factor_change`,`battrey_low`,`door_open`,`short_time_power_fail`,`record_recoverd`,`time_base_event_1`,`time_base_event_2`,`contactor_statusOn`,`contactor_status_off`,`short_time_power_fail_end`,`reverse_energy_end`,`tamper_energy_end`,`over_load_end`,`reserved_46`,`reserved_47`,`reserved _48`,`magnetic_feild_end`,`ct_fail_end`,`pt_fail_end`,`software_logout`,`reserved_07`,`reserved_06`,`reserved_05`,`reserved_04`,`reserved_03`,`reserved_02`,`reserved_01`,`reserved_00`
        //                                        from `alarm_status_response`
        //                                        where `alarm_status_response_id` in ({1})", sessionDateTime.ToString(DateFormat), rowIds);
        //        OdbcCommand Command = new OdbcCommand(query, Connection);
        //        Command.ExecuteNonQuery();
        //        Command.CommandText = string.Format("delete from alarm_status_response where alarm_status_response_id in ({0})", rowIds);
        //        Command.ExecuteNonQuery();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (_newException != null) _newException(ex);
        //        return false;
        //    }
        //    finally
        //    {
        //        DBConnect.CloseConnection();
        //    }
        //}

        #endregion

        #region New - User Alarms Response Methods

        public string GetAlarmUserResponse(string MSN, Param_MajorAlarmProfile majorAlarms, byte status)
        {
            StringBuilder RowIds = new StringBuilder(50);
            try
            {
                DBConnect.OpenConnection();
                Dictionary<string, short> eventIds = GetEventIdsForColumns();
                StringBuilder MyInsertQuery = new StringBuilder(50);
                //============================================================================== 
                MyInsertQuery.Append(@"select alarm_status_response_id,alarm_name
                     from alarm_status_response where msn = " + MSN + " and status_type = " + status);
                //============================================================================== 
                OdbcCommand cmd = new OdbcCommand();
                cmd.CommandText = MyInsertQuery.ToString();
                cmd.Connection = _DBConnect.Connection;
                var tbl = GetTableFromDB(cmd);
                if (majorAlarms == null) RowIds.ToString();
                MajorAlarm userAlarmResponse = null;
                //string delimiter = ",";
                foreach (DataRow row in tbl.Rows)
                {
                    RowIds.Append(row["alarm_status_response_id"]);
                    RowIds.Append(",");

                    userAlarmResponse = majorAlarms.AlarmItems.Find(x => x.Info._EventId == eventIds[row["alarm_name"].ToString()] + 1);
                    if (userAlarmResponse != null)
                        userAlarmResponse.IsResetUserStatus = (EnergyMizerAlarmStatus)status;

                    //foreach (DataColumn column in tbl.Columns)
                    //{
                    //    if (eventIds.ContainsKey(column.ColumnName) && Convert.ToBoolean(row[column.ColumnName]))
                    //    {
                    //        userAlarmResponse = majorAlarms.AlarmItems.Find(x => x.Info._EventId == eventIds[column.ColumnName] + 1);
                    //        if (userAlarmResponse != null)
                    //            userAlarmResponse.IsResetUserStatus = (EnergyMizerAlarmStatus)status;
                    //    }
                    //}
                }
                return RowIds.ToString().Trim(',');
            }
            catch (Exception)
            {
                RowIds.Clear();
                return RowIds.ToString().Trim(',');
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }


        public bool SaveUserAlarmResponseLogandDelete(string rowIds, DateTime sessionDateTime)
        {
            try
            {
                if (!_DBConnect.IsConnectionOpen)
                    DBConnect.OpenConnection();
                string request = string.Empty;
                if (string.IsNullOrEmpty(rowIds)) return true;
                string query = string.Format(@"INSERT INTO `alarm_status_response_log`(`customer_id`,`msn`,`response_date_time`,`session_date_time`,`alarm_name`,`status_type`)
                                                select `customer_id`,`msn`,`response_date_time`,'{0}',`alarm_name`,`status_type`
                                                from `alarm_status_response`
                                                where `alarm_status_response_id` in ({1})", sessionDateTime.ToString(DateFormat), rowIds);
                OdbcCommand Command = new OdbcCommand(query, Connection);
                Command.ExecuteNonQuery();
                Command.CommandText = string.Format("delete from alarm_status_response where alarm_status_response_id in ({0})", rowIds);
                Command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        #endregion


        #region Save Alarm Status
        public bool SaveAlarmStatus(string MSN, DateTime SessionDateTime, BitArray AlarmStatus, BitArray MDCAlarms, MeterInformation meterInfo)
        {
            try
            {
                DBConnect.OpenConnection();
                StringBuilder MyInsertQuery = new StringBuilder(50);
                //============================================================================== 
                MyInsertQuery.Append("INSERT INTO alarm_status"
                   + "( `customer_id`,`is_alarm_processed`,`msn`, `session_date_time`, `date`, `time`,  `imbalance_volt`, `phase_sequence`"
                    + ", `reverse_polarity`, `phase_fail`, `under_volt`, `over_volt`, `over_current`, `high_neutral_current`, `over_load`"
                    + ", `reverse_energy`, `tamper_energy`, `ct_fail`, `pt_fail`, `opticalportLogin`, `power_fail`, `power_failEnd`"
                    + ", `one_wire_tampering`, `meter-on_load`, `meter_on_load_end`, `under_volt_end`, `mdt_exceed`, `system_reset`"
                    + ", `system_problems`, `mdi_reset`, `parameters`, `password_change`, `customer_code`, `time_change`, `window_sequense_change`"
                    + ", `over_volt_end`, `bill_register_error`, `param_error`, `power_factor_change`, `battrey_low`, `door_open`, `short_time_power_fail`"
                    + ", `record_recoverd`, `time_base_event_1`, `time_base_event_2`, `contactor_statusOn`, `contactor_status_off`, `short_time_power_fail_end`"
                    + ", `reverse_energy_end`, `tamper_energy_end`, `over_load_end`, `reserved_46`, `reserved_47`, `reserved _48`"
                   + ",`magnetic_feild_end`,`ct_fail_end` ,`pt_fail_end` ,`software_logout`,`reserved_07` ,`reserved_06` ,`reserved_05` ,`reserved_04` ,`reserved_03` ,`reserved_02` ,`reserved_01` ,`reserved_00`"
                   + ", `high_ev_counter`,`low_ev_counter` ,`high_lp_counter` ,`low_lp_counter` ,`cs_valid_sync` ,`pwd_change`,`pwd_error`"
                   + ", `mdi_date_change`,`tbe1_change` ,`tbe2_change` ,`type_change` ,`param_m_limit_time`,`contactor_param` ,`lp_counter_mismatch`"
                   + ", `cs_invalid_sync`,`exception_occur`,`ev_counter_mismatch`,`mb_counter_mismatch`,`contactor_on`,`contactor_off`,`limits_change`,`monitoring_time_change`"
                   + ", `ct_pt_change`,`decimal_points_change`,`energy_param_change`,`rtc_failed_battery`,"
                   + " `Invalid_SecurityData`,`SecurityKey_Change`,`Invalid_AuthenticationTAG`,`high_lp2_counter`,`low_lp2_counter`,`lp2_counter_mismatch`,`major_alarm_string`,`mdc_alarm_string`"
                   + ") VALUES(" + meterInfo.Customer_ID + ", 0 ,'" + MSN + "','" + SessionDateTime.ToString(DateFormat) + "', CURDATE(), CURTIME()");
                //============================================================================== 

                #region Extracting values from both arrays
                var columnNames = Enum.GetNames(typeof(MeterEvent));
                for (int i = 0; i < columnNames.Length; i++)
                //foreach (var bit in AlarmStatus)
                {
                    var bit = (i >= AlarmStatus.Length ? false : AlarmStatus[i]);
                    MyInsertQuery.Append(", " + Convert.ToInt16(bit));
                }

                columnNames = Enum.GetNames(typeof(MDCEvents));
                for (int i = 0; i < columnNames.Length; i++)
                {
                    MyInsertQuery.Append(", " + Convert.ToInt16(MDCAlarms[i]));
                }

                #endregion

                #region Add Combine Event String

                var hexMeter = BitConverter.ToString(Commons.BitArrayToByteArray(AlarmStatus)).Replace("-", "");
                MyInsertQuery.Append(", " + "\'" + hexMeter + "\'");

                #endregion

                #region Add Combine MDC AlarmString

                var hexMDC = BitConverter.ToString(Commons.BitArrayToByteArray(MDCAlarms)).Replace("-", "");
                MyInsertQuery.Append(", " + "\'" + hexMDC + "\'");

                #endregion

                MyInsertQuery.Append(")");
                if (ExecuteQuery(MyInsertQuery.ToString()))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public Dictionary<short, string> GetColumnsForEventIds()
        {
            Dictionary<short, string> eventIds = new Dictionary<short, string>();
            StringBuilder MyInsertQuery = new StringBuilder(50);
            //============================================================================== 
            MyInsertQuery.Append("select `index`,status_column_name from events_label where status_column_name is not null");
            //============================================================================== 
            OdbcCommand cmd = new OdbcCommand();
            cmd.CommandText = MyInsertQuery.ToString();
            cmd.Connection = Connection;
            var tbl = GetTableFromDB(cmd);
            foreach (DataRow row in tbl.Rows)
            {
                short id = Convert.ToInt16(row["index"]);
                if (eventIds.ContainsKey(id))
                    eventIds[id] = row["status_column_name"].ToString();
                else
                    eventIds.Add(id, row["status_column_name"].ToString());
            }
            return eventIds;
        }

        public Dictionary<string, short> GetEventIdsForColumns()
        {
            Dictionary<string, short> eventIds = new Dictionary<string, short>();
            StringBuilder MyInsertQuery = new StringBuilder(50);
            //============================================================================== 
            MyInsertQuery.Append("select `index`,status_column_name from events_label where status_column_name is not null");
            //============================================================================== 
            OdbcCommand cmd = new OdbcCommand();
            cmd.CommandText = MyInsertQuery.ToString();
            cmd.Connection = Connection;
            var tbl = GetTableFromDB(cmd);
            foreach (DataRow row in tbl.Rows)
            {
                short id = Convert.ToInt16(row["index"]);
                string column = row["status_column_name"].ToString();

                if (eventIds.ContainsKey(column))
                    eventIds[column] = id;
                else
                    eventIds.Add(column, id);
            }
            return eventIds;
        }

        //public bool UpdateAlarmStatusLive(DateTime SessionDateTime, BitArray AlarmStatus, MeterInformation meterInfo)
        //{
        //    try
        //    {
        //        string MSN = meterInfo.MSN;
        //        DBConnect.OpenConnection();
        //        Dictionary<short, string> eventStatusColumns = GetColumnsForEventIds();
        //        StringBuilder MyInsertQuery = new StringBuilder(50);
        //        //============================================================================== 
        //        MyInsertQuery.Append(string.Format("update alarm_status_live set `customer_id` = {0} , `session_date_time` = '{1}',`msn` = '{2}', `date`=CURDATE(), `time`=CURTIME()", meterInfo.Customer_ID, SessionDateTime.ToString(DateFormat), MSN));
        //        //============================================================================== 

        //        #region Extracting values from both arrays
        //        var columnNames = Enum.GetValues(typeof(MeterEvent));
        //        for (short i = 0; i < columnNames.Length; i++)
        //        {
        //            var bit = (i >= AlarmStatus.Length ? false : AlarmStatus[i]);
        //            if (bit && eventStatusColumns.ContainsKey(i))
        //                MyInsertQuery.Append(", " + eventStatusColumns[i] + "=" + Convert.ToInt16(bit));
        //        }
        //        #endregion
        //        MyInsertQuery.Append(" where msn = " + MSN);
        //        if (ExecuteQuery(MyInsertQuery.ToString()))
        //            return true;
        //        else
        //            return InsertAlarmStatusLive(MSN, SessionDateTime, AlarmStatus, meterInfo);
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        DBConnect.CloseConnection();
        //    }
        //}

        public bool UpdateAlarmStatusLive(DateTime SessionDateTime, BitArray AlarmStatus, MeterInformation meterInfo)
        {
            try
            {
                string MSN = meterInfo.MSN;
                DBConnect.OpenConnection();
                Dictionary<short, string> eventStatusColumns = GetColumnsForEventIds();
                StringBuilder MyInsertQuery = new StringBuilder(50);
                //============================================================================== 
                MyInsertQuery.Append(string.Format("update alarm_status_live set `customer_id` = {0} , `session_date_time` = '{1}',`msn` = '{2}', `date`=CURDATE(), `time`=CURTIME()", meterInfo.Customer_ID, SessionDateTime.ToString(DateFormat), MSN));
                //============================================================================== 

                #region Extracting values from both arrays
                var columnNames = Enum.GetValues(typeof(MeterEvent));
                for (short i = 0; i < columnNames.Length; i++)
                {
                    var bit = (i >= AlarmStatus.Length ? false : AlarmStatus[i]);
                    if (bit && eventStatusColumns.ContainsKey(i))
                        MyInsertQuery.Append(String.Format(", {0} = {1},date_time_{0}='{2}'", eventStatusColumns[i], Convert.ToInt16(bit), SessionDateTime.ToString(DatabaseController.DateFormat)));
                }
                #endregion
                MyInsertQuery.Append(" where msn = " + MSN);
                if (ExecuteQuery(MyInsertQuery.ToString()))
                    return true;
                else
                    return InsertAlarmStatusLive(MSN, SessionDateTime, AlarmStatus, meterInfo);
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }


        public bool InsertAlarmStatusLive(string MSN, DateTime SessionDateTime, BitArray AlarmStatus, MeterInformation meterInfo)
        {
            try
            {
                //DBConnect.OpenConnection();
                StringBuilder MyInsertQuery = new StringBuilder(50);
                //============================================================================== 
                MyInsertQuery.Append("replace INTO alarm_status_live"
                   + "( `customer_id`,`msn`, `session_date_time`, `date`, `time`,  `imbalance_volt`, `phase_sequence`"
                    + ", `reverse_polarity`, `phase_fail`, `under_volt`, `over_volt`, `over_current`, `high_neutral_current`, `over_load`"
                    + ", `reverse_energy`, `tamper_energy`, `ct_fail`, `pt_fail`, `opticalportLogin`, `power_fail`, `power_failEnd`"
                    + ", `one_wire_tampering`, `meter-on_load`, `meter_on_load_end`, `under_volt_end`, `mdt_exceed`, `system_reset`"
                    + ", `system_problems`, `mdi_reset`, `parameters`, `password_change`, `customer_code`, `time_change`, `window_sequense_change`"
                    + ", `over_volt_end`, `bill_register_error`, `param_error`, `power_factor_change`, `battrey_low`, `door_open`, `short_time_power_fail`"
                    + ", `record_recoverd`, `time_base_event_1`, `time_base_event_2`, `contactor_statusOn`, `contactor_status_off`, `short_time_power_fail_end`"
                    + ", `reverse_energy_end`, `tamper_energy_end`, `over_load_end`, `reserved_46`, `reserved_47`, `reserved _48`"
                   + ",`magnetic_feild_end`,`ct_fail_end` ,`pt_fail_end` ,`software_logout`,`reserved_07` ,`reserved_06` ,`reserved_05` ,`reserved_04` ,`reserved_03` ,`reserved_02` ,`reserved_01` ,`reserved_00`"
                   + ") VALUES(" + meterInfo.Customer_ID + ",'" + MSN + "','" + SessionDateTime.ToString(DateFormat) + "', CURDATE(), CURTIME()");
                //============================================================================== 

                #region Extracting values from array
                var columnNames = Enum.GetNames(typeof(MeterEvent));
                for (int i = 0; i < columnNames.Length; i++)
                //foreach (var bit in AlarmStatus)
                {
                    var bit = (i >= AlarmStatus.Length ? false : AlarmStatus[i]);
                    MyInsertQuery.Append(", " + Convert.ToInt16(bit));
                }
                #endregion

                MyInsertQuery.Append(")");
                OdbcCommand cmd = new OdbcCommand(MyInsertQuery.ToString(), Connection);
                if (ExecuteCommandWithAlreadyOpenedConnection(cmd))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        #endregion

        #region Save Debug Error

        //public bool Save_DebugErrorCautions(string msn, string reference_no, DateTime sessionDate_time, string error_string, string cautionsString, string contactor_status)
        //{
        //    try
        //    {
        //        string query = String.Format("INSERT INTO mdc_internal(`msn`,`reference_no`,`session_date_time`,`error_string`,`caution_string`,`contactor_debug_string`)"
        //            + " VALUES('{0}', '{1}','{2}','{3}','{4}','{5}')", msn, reference_no, sessionDate_time.ToString(DateFormat), error_string, cautionsString, contactor_status);
        //        OdbcCommand Command = new OdbcCommand(query, Connection);
        //        return ExecuteQuery(Command);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (_newException != null) _newException(ex);
        //        return false;
        //    }
        //}

        #endregion

        #region Reliability
        public bool SaveReadParams_old(string msn, DateTime session_date_time, string reference_no, MeterReliablityParams obj_param)
        {
            try
            {
                var columns = "`msn` ,`reference_no` ,`session_date_time`,`lm_temperenergy_kw` ,`lm_ct_fail_amp` ,`lm_pt_fail_amp` ,`lm_pt_fail_volt` ,`lm_over_current_phase_t1`,`lm_over_current_phase_t2`"
                             + ",`lm_over_current_phase_t3` ,`lm_over_current_phase_t4`,`lm_mdi_exceed_t1`  ,`lm_mdi_exceed_t2` ,`lm_mdi_exceed_t3` ,`lm_mdi_exceed_t4` ,`lm_over_load_phase_t1`"
                             + ",`lm_over_load_phase_t2`,`lm_over_load_phase_t3`,`lm_over_load_phase_t4` ,`lm_over_load_total_t1`,`lm_over_load_total_t2`,`lm_over_load_total_t3`,`lm_over_load_total_t4`"
                             + ",`mt_over_current`,`mt_over_load`,`mdi_auto_reset_enable`,`mdi_auto_reset_date`,`ip_port_1`"
                             + ",`ip_port_2`,`ip_port_3`,`ip_port_4`,`number_profile_1`,`number_profile_2` ,`number_profile_3` ,`number_profile_4` ,`ka_enable` ,`ka_wakeup_id` ,`ka_ping_time_sec`"
                             + ",`mlt_retries_ip` ,`mlt_tcp_inactivity` ,`mlt_tcp_sip_send`,`mi_apn` ,`mi_decrement_counter`,`tbe1` ,`tbe2` ,`tbe1_disable` ,`tbe2_disable` ,`turn_contactoroff_ol_t1`"
                             + ",`turn_contactoroff_ol_t2` ,`turn_contactoroff_ol_t3`,`turn_contactoroff_ol_t4` ,`con_tariff_change` ,`retry_automatic` ,`retry_count` ,`retry_auto_interval`"
                             + ",`debug_error`,`debug_cautions` ,`debug_contactor_status`";

                string query = String.Format("INSERT INTO mdc_reliability({0}) VALUES("
                                             + "'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}'"
                                             + ",'{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}'"
                                             + ",'{46}','{47}','{48}','{49}','{50}','{51}','{52}','{53}','{54}','{55}','{56}','{57}','{58}')"
                                             , columns
                                             , msn
                                             , reference_no
                                             , session_date_time.ToString(DateFormat)
                                             , obj_param.LM_temperEnergy_kW
                                             , obj_param.LM_Ct_Fail_amp
                                             , obj_param.LM_Pt_Fail_amp
                                             , obj_param.LM_Pt_Fail_Volt
                                             , obj_param.LM_over_current_phase_t1
                                             , obj_param.LM_over_current_phase_t2
                                             , obj_param.LM_over_current_phase_t3
                                             , obj_param.LM_over_current_phase_t4
                                             , obj_param.LM_mdi_exceed_t1
                                             , obj_param.LM_mdi_exceed_t2
                                             , obj_param.LM_mdi_exceed_t3
                                             , obj_param.LM_mdi_exceed_t4
                                             , obj_param.LM_Over_Load_phase_t1
                                             , obj_param.LM_Over_Load_phase_t2
                                             , obj_param.LM_Over_Load_phase_t3
                                             , obj_param.LM_Over_Load_phase_t4
                                             , obj_param.LM_Over_Load_Total_t1
                                             , obj_param.LM_Over_Load_Total_t2
                                             , obj_param.LM_Over_Load_Total_t3
                                             , obj_param.LM_Over_Load_Total_t4
                                             , obj_param.MT_Over_Current
                                             , obj_param.MT_Over_Load
                                             , (obj_param.MDI_Auto_Reset_Enable) ? 1 : 0
                                             , obj_param.MDI_Auto_Reset_Date
                                             , obj_param.ip_port_1
                                             , obj_param.ip_port_2
                                             , obj_param.ip_port_3
                                             , obj_param.ip_port_4
                                             , obj_param.number_profile_1
                                             , obj_param.number_profile_2
                                             , obj_param.number_profile_3
                                             , obj_param.number_profile_4
                                             , (obj_param.KA_enable) ? 1 : 0
                                             , obj_param.KA_wakeup_id
                                             , obj_param.KA_ping_time_sec
                                             , obj_param.MTL_Retries_ip
                                             , obj_param.MTL_tcp_Inactivity
                                             , obj_param.MTL_tcp_sip_send
                                             , obj_param.MI_APN
                                             , obj_param.MT_Decrement_Counter
                                             , obj_param.TBE1
                                             , obj_param.TBE2
                                             , obj_param.TBE1_Disable
                                             , obj_param.TBE2_Disable
                                             , (obj_param.Turn_ContactorOff_OL_t1) ? 1 : 0
                                             , (obj_param.Turn_ContactorOff_OL_t2) ? 1 : 0
                                             , (obj_param.Turn_ContactorOff_OL_t3) ? 1 : 0
                                             , (obj_param.Turn_ContactorOff_OL_t4) ? 1 : 0
                                             , (obj_param.con_tariff_change) ? 1 : 0
                                             , (obj_param.retry_automatic) ? 1 : 0
                                             , obj_param.retry_count
                                             , obj_param.retry_auto_interval
                                             , obj_param.debug_error
                                             , obj_param.debug_cautions
                                             , obj_param.debug_contactor_status
                                             );

                OdbcCommand Command = new OdbcCommand(query, Connection);
                return ExecuteQuery(Command);
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }
        }

        public bool SaveReadParams(string msn, DateTime session_date_time, MeterReliablityParams obj_param)
        {
            try
            {
                var columns = "`msn` ,`session_date_time`,`lm_temperenergy_kw` ,`lm_ct_fail_amp` ,`lm_pt_fail_amp` ,`lm_pt_fail_volt` ,`lm_over_current_phase_t1`,`lm_over_current_phase_t2`"
                             + ",`lm_over_current_phase_t3` ,`lm_over_current_phase_t4`,`lm_mdi_exceed_t1`  ,`lm_mdi_exceed_t2` ,`lm_mdi_exceed_t3` ,`lm_mdi_exceed_t4` ,`lm_over_load_phase_t1`"
                             + ",`lm_over_load_phase_t2`,`lm_over_load_phase_t3`,`lm_over_load_phase_t4` ,`lm_over_load_total_t1`,`lm_over_load_total_t2`,`lm_over_load_total_t3`,`lm_over_load_total_t4`"
                             + ",`mt_over_current`,`mt_over_load`,`mdi_auto_reset_enable`,`mdi_auto_reset_date`,`ip_port_1`"
                             + ",`ip_port_2`,`ip_port_3`,`ip_port_4`,`number_profile_1`,`number_profile_2` ,`number_profile_3` ,`number_profile_4` ,`ka_enable` ,`ka_wakeup_id` ,`ka_ping_time_sec`"
                             + ",`mlt_retries_ip` ,`mlt_tcp_inactivity` ,`mlt_tcp_sip_send`,`mi_apn` ,`mi_decrement_counter`,`tbe1` ,`tbe2` ,`tbe1_disable` ,`tbe2_disable` ,`turn_contactoroff_ol_t1`"
                             + ",`turn_contactoroff_ol_t2` ,`turn_contactoroff_ol_t3`,`turn_contactoroff_ol_t4` ,`con_tariff_change` ,`retry_automatic` ,`retry_count` ,`retry_auto_interval`"
                             + ",`debug_error`,`debug_cautions` ,`debug_contactor_status`,`signal_strength`,`gsm_firmware_version`,`gsm_module_info`,`imei`,`imsi`,`network_code`,`connected_server_ip`,`connected_port`,`sim_local_ip`,`tower_id`,`location_area_code`";

                string query = String.Format("INSERT INTO mdc_reliability({0}) VALUES("
                                             + "'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}'"
                                             + ",'{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}'"
                                             + ",'{46}','{47}','{48}','{49}','{50}','{51}','{52}','{53}','{54}','{55}','{56}','{57}','{58}'"
                                             + ",'{59}','{60}','{61}','{62}',{63},'{64}','{65}','{66}','{67}','{68}')"
                                             , columns
                                             , msn
                                             , session_date_time.ToString(DateFormat)
                                             , obj_param.LM_temperEnergy_kW
                                             , obj_param.LM_Ct_Fail_amp
                                             , obj_param.LM_Pt_Fail_amp
                                             , obj_param.LM_Pt_Fail_Volt
                                             , obj_param.LM_over_current_phase_t1
                                             , obj_param.LM_over_current_phase_t2
                                             , obj_param.LM_over_current_phase_t3
                                             , obj_param.LM_over_current_phase_t4
                                             , obj_param.LM_mdi_exceed_t1
                                             , obj_param.LM_mdi_exceed_t2
                                             , obj_param.LM_mdi_exceed_t3
                                             , obj_param.LM_mdi_exceed_t4
                                             , obj_param.LM_Over_Load_phase_t1
                                             , obj_param.LM_Over_Load_phase_t2
                                             , obj_param.LM_Over_Load_phase_t3
                                             , obj_param.LM_Over_Load_phase_t4
                                             , obj_param.LM_Over_Load_Total_t1
                                             , obj_param.LM_Over_Load_Total_t2
                                             , obj_param.LM_Over_Load_Total_t3
                                             , obj_param.LM_Over_Load_Total_t4
                                             , obj_param.MT_Over_Current
                                             , obj_param.MT_Over_Load
                                             , (obj_param.MDI_Auto_Reset_Enable) ? 1 : 0
                                             , obj_param.MDI_Auto_Reset_Date
                                             , obj_param.ip_port_1
                                             , obj_param.ip_port_2
                                             , obj_param.ip_port_3
                                             , obj_param.ip_port_4
                                             , obj_param.number_profile_1
                                             , obj_param.number_profile_2
                                             , obj_param.number_profile_3
                                             , obj_param.number_profile_4
                                             , (obj_param.KA_enable) ? 1 : 0
                                             , obj_param.KA_wakeup_id
                                             , obj_param.KA_ping_time_sec
                                             , obj_param.MTL_Retries_ip
                                             , obj_param.MTL_tcp_Inactivity
                                             , obj_param.MTL_tcp_sip_send
                                             , obj_param.MI_APN
                                             , obj_param.MT_Decrement_Counter
                                             , obj_param.TBE1
                                             , obj_param.TBE2
                                             , obj_param.TBE1_Disable
                                             , obj_param.TBE2_Disable
                                             , (obj_param.Turn_ContactorOff_OL_t1) ? 1 : 0
                                             , (obj_param.Turn_ContactorOff_OL_t2) ? 1 : 0
                                             , (obj_param.Turn_ContactorOff_OL_t3) ? 1 : 0
                                             , (obj_param.Turn_ContactorOff_OL_t4) ? 1 : 0
                                             , (obj_param.con_tariff_change) ? 1 : 0
                                             , (obj_param.retry_automatic) ? 1 : 0
                                             , obj_param.retry_count
                                             , obj_param.retry_auto_interval
                                             , obj_param.debug_error
                                             , obj_param.debug_cautions
                                             , obj_param.debug_contactor_status
                                             , obj_param.ModemStatusInfo.SignalStrength
                                             , obj_param.ModemStatusInfo.FirmwareVersion
                                             , obj_param.ModemStatusInfo.MouduleInfo
                                             , obj_param.ModemStatusInfo.IMEI
                                             , obj_param.ModemStatusInfo.IMSI
                                             , string.IsNullOrEmpty(obj_param.ModemStatusInfo.NetworkCode) ? "0" : "NULL"
                                             , obj_param.ModemStatusInfo.ServerIP
                                             , obj_param.ModemStatusInfo.PortN
                                             , obj_param.ModemStatusInfo.AssingnedIP
                                             , obj_param.ModemStatusInfo.CellID
                                             , obj_param.ModemStatusInfo.LAC
                                             );

                OdbcCommand Command = new OdbcCommand(query, Connection);
                return ExecuteQuery(Command);
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }
        }

        #endregion

        #endregion

        #region Save Logging

        public bool save_packet_log(string msn, DateTime session, byte[] log)
        {
            try
            {
                string packet = string.Empty;
                for (int i = 0; i < log.Length; i++)
                {
                    packet += log[i].ToString("x2") + " ";
                }
                packet = packet.Replace("11 11 11 11 11 ", " Sent packet ");
                packet = packet.Replace("12 12 12 12 12", " Received packet ");

                string query = String.Format("Insert into packet_log VALUES('{0}','{1}','{2}')", msn, session.ToString(DateFormat), packet);
                if (ExecuteQuery(query))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void insert_comm_log(string MSN, Int64 Invoke_ID, Int64 Owner_ID, byte[] Log, int Status)
        {
            try
            {
                string tempLog = Encoding.ASCII.GetString(Log);
                string myInsertQuery = "INSERT INTO communication_log (msn, invoke_id, owner_id, log, status, date_time) VALUES('" + MSN + "','" + Invoke_ID + "','" + Owner_ID + "','" + Log + "','" + Status + "','" + DateTime.Now.ToString(DateFormat) + "')";
                ExecuteQuery(myInsertQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void insert_output_log(string MSN, Int64 Invoke_ID, Int64 Owner_ID, string Output_Log)
        {
            try
            {
                string myInsertQuery = "INSERT INTO output_log (msn, invoke_id, owner_id, log, date_time) VALUES('" + MSN + "','" + Invoke_ID + "','" + Owner_ID + "','" + Output_Log + "','" + DateTime.Now.ToString(DateFormat) + "')";
                ExecuteQuery(myInsertQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Insert_Mdc_Events_Log(string message, int eventCode, MeterInformation meterInfo, DateTime session_dateTime)
        {
            try
            {
                var eCode = eventCode + 501; //501 constant value to add in mdc_alarm value to get event code
                message = Regex.Replace(message, @"[']", "`", RegexOptions.None);
                var query = string.Format("insert into `mdc_alarms_log`(`msn`,`session_date_time`,`event_code`,`mdc_action`) Values('{0}','{1}','{2}','{3}')",
                    meterInfo.MSN,
                    session_dateTime.ToString(DateFormat),
                    eCode,
                    message
                    );

                //Execute Query
                ExecuteQuery(query, true);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Insert_Singlephase_Events_Log(List<MajorAlarm> TriggeredList, MeterInformation meterInfo, DateTime session_dateTime, bool emergancyAlaramStaus = false)
        {
            StringBuilder event_query = new StringBuilder("INSERT INTO events_data(`session_datetime`,`arrival_time`,`msn`,`date`,`time`,`event_code`,`counter`,`description`,`is_individual`,`customer_id`, `emergency_alarm_status_id`) VALUES");
            try
            {
                string alarm_status_val = emergancyAlaramStaus ? "1" : "null";
                foreach (MajorAlarm alarm in TriggeredList)
                {
                    event_query.Append(string.Format("('{0}','{0}', '{1}',  CURDATE(), CURTIME(),'{2}', '{3}', '{4}', '{5}', {6},{7}),"
                                 , session_dateTime.ToString(DateFormat)
                                 , meterInfo.MSN
                                 , alarm.Info.EventCode
                                 , 0
                                 , "--"
                                 , 0 //only combine events log
                                 , meterInfo.Customer_ID,
                                 alarm_status_val
                                 ));

                }
                //Execute Query
                ExecuteQuery(event_query.ToString().TrimEnd(','), true);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                event_query.Clear();
                event_query = null;
            }
        }

        public void insert_mdc_log(MDC_Log log)
        {
            try
            {
                var has_Except = Convert.ToInt16(log.IsException);
                log.log_string = Regex.Replace(log.log_string, @"[']", "`", RegexOptions.None);
                string myInsertQuery = string.Format("INSERT INTO mdc_log(`msn`, `session_date_time`, `date`, `time`, `log_string`, `has_exception`,`connection_life_time`,`sent_bytes`,`received_bytes`) VALUES('{0}','{1}',CURDATE(), CURTIME(),'{2}','{3}','{4}','{5}','{6}')", log.msn, log.session_dt.ToString(DateFormat), log.log_string, has_Except, log.ConnectionLife.ToString(), log.SentBytes, log.ReceiveBytes);

                System.Diagnostics.Debug.WriteLine(myInsertQuery);

                ExecuteQuery(myInsertQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void insert_mdc_log_live(MDC_Log log)
        {
            try
            {
                var has_Except = Convert.ToInt16(log.IsException);
                log.log_string = Regex.Replace(log.log_string, @"[']", "`", RegexOptions.None);
                string myInsertQuery = string.Format("replace INTO `mdc_log_live` (`msn`, `session_date_time`, `date`, `time`, `log_string`,`has_exception`,`connection_life_time`,`sent_bytes`,`received_bytes`) VALUES('{0}','{1}',CURDATE(), CURTIME(),'{2}','{3}','{4}','{5}','{6}')", log.msn, log.session_dt.ToString(DateFormat), log.log_string, has_Except, log.ConnectionLife.ToString(), log.SentBytes, log.ReceiveBytes);
                ExecuteQuery(myInsertQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void insert_mdc_Errorlog(MDC_Log errorlog)
        {
            try
            {
                errorlog.log_string = Regex.Replace(errorlog.log_string, @"[']", "`", RegexOptions.None);
                string myInsertQuery = "INSERT INTO mdc_errorlog(`msn`, `session_datetime`, `date`, `time`, `error_string`) VALUES('" + errorlog.msn + "','" + errorlog.session_dt.ToString(DateFormat) + "',CURDATE(), CURTIME(),'" + errorlog.log_string + "')";
                ExecuteQuery(myInsertQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<string> Get_MeterListFromLogs()
        {
            List<string> meterList = new List<string>();
            DataTable tempTable = new DataTable();
            try
            {
                _DBConnect.OpenConnection();
                string mySelectQuery = "SELECT DISTINCT `msn` FROM  `mdc_log` ";
                tempTable = GetTableFromDB(mySelectQuery);

                for (int i = 0; i < tempTable.Rows.Count; i++)
                {
                    meterList.Add(tempTable.Rows[i][0].ToString());
                }
                return meterList;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (_DBConnect.Connection != null)
                    _DBConnect.CloseConnection();
            }

        }
        public List<string> Get_MeterLogs_DateList(string msn)
        {
            List<string> dateList = new List<string>();
            DataTable tempTable = new DataTable();
            try
            {
                if (!String.IsNullOrEmpty(msn))
                {
                    _DBConnect.OpenConnection();
                    string mySelectQuery = "SELECT  DISTINCT`date` FROM  `mdc_log` WHERE msn = '" + msn + "'";
                    tempTable = GetTableFromDB(mySelectQuery);

                    for (int i = 0; i < tempTable.Rows.Count; i++)
                    {
                        dateList.Add(tempTable.Rows[i][0].ToString());
                    }
                    return dateList;
                }
                else
                    return null;

            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (_DBConnect.Connection != null)
                    _DBConnect.CloseConnection();
            }

        }
        public List<string> Get_MeterLogs_TimeList(string msn)
        {
            List<string> dateList = new List<string>();
            DataTable tempTable = new DataTable();
            try
            {
                if (!String.IsNullOrEmpty(msn))
                {
                    _DBConnect.OpenConnection();
                    string mySelectQuery = "SELECT  DISTINCT`time`FROM  `mdc_log` WHERE msn = '" + msn + "'";
                    tempTable = GetTableFromDB(mySelectQuery);

                    for (int i = 0; i < tempTable.Rows.Count; i++)
                    {
                        dateList.Add(tempTable.Rows[i][0].ToString());
                    }
                    return dateList;
                }
                else
                    return null;

            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (_DBConnect.Connection != null)
                    _DBConnect.CloseConnection();
            }

        }

        public string Get_MeterLogs(string msn)
        {
            string tempString = null;
            DataTable tempTable = new DataTable();
            try
            {
                _DBConnect.OpenConnection();
                string mySelectQuery = "SELECT  `log_string` FROM  `mdc_log` WHERE msn = '" + msn + "' ORDER BY date,time DESC ";
                tempTable = GetTableFromDB(mySelectQuery);

                for (int i = 0; i < tempTable.Rows.Count; i++)
                {
                    tempString = tempString + tempTable.Rows[i][0].ToString();
                }
                return tempString;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (_DBConnect.Connection != null)
                    _DBConnect.CloseConnection();
            }

        }
        public string Get_MeterLogs(string msn, string field, string value)
        {
            string tempString = null;
            DataTable tempTable = new DataTable();
            try
            {
                _DBConnect.OpenConnection();
                string mySelectQuery = "SELECT  `log_string` FROM  `mdc_log` WHERE msn = '" + msn + "' AND " + field + "=" + "'" + value + "' ORDER BY date,time DESC";
                tempTable = GetTableFromDB(mySelectQuery);

                for (int i = 0; i < tempTable.Rows.Count; i++)
                {
                    tempString = tempString + tempTable.Rows[i][0].ToString();
                }
                return tempString;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (_DBConnect.Connection != null)
                    _DBConnect.CloseConnection();
            }

        }
        public string Get_MeterLogs(string msn, string field, string timeString1, string timeString2)
        {
            string tempString = null;
            DataTable tempTable = new DataTable();
            try
            {
                _DBConnect.OpenConnection();
                string mySelectQuery = "SELECT  `log_string` FROM  `mdc_log` WHERE msn = '" + msn + "' AND ( time BETWEEN '" + timeString1 + "' AND '" + timeString2 + "')";
                tempTable = GetTableFromDB(mySelectQuery);

                for (int i = 0; i < tempTable.Rows.Count; i++)
                {
                    tempString = tempString + tempTable.Rows[i][0].ToString();
                }
                return tempString;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (_DBConnect.Connection != null)
                    _DBConnect.CloseConnection();
            }
        }
        public string Get_MeterLogs(string msn, string field1, string value1, string field2, string value2)
        {
            string tempString = null;
            DataTable tempTable = new DataTable();
            try
            {
                _DBConnect.OpenConnection();
                //SELECT * FROM `mdc_log` WHERE msn = '3696111111' and (date = '2012-12-15' AND time = '12:55:58')
                string mySelectQuery = "SELECT  `log_string` FROM  `mdc_log` WHERE msn = '" + msn + "' AND (" + field1 + " = " + "'" + value1 + "' AND " + field2 + " = " + "'" + value2 + "' )";
                tempTable = GetTableFromDB(mySelectQuery);

                for (int i = 0; i < tempTable.Rows.Count; i++)
                {
                    tempString = tempString + tempTable.Rows[i][0].ToString();
                }
                return tempString;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (_DBConnect.Connection != null)
                    _DBConnect.CloseConnection();
            }

        }
        public string Get_MeterLogs_ByDateTime(string msn, string date, string timeString1, string timeString2)
        {
            string tempString = null;
            DataTable tempTable = new DataTable();
            try
            {
                _DBConnect.OpenConnection();
                //SELECT * FROM `mdc_log` WHERE msn = '3696111111' and (date = '2012-12-15' AND time = '12:55:58')
                string mySelectQuery = "SELECT  `log_string` FROM  `mdc_log` WHERE msn = '" + msn + "' AND (date = '" + date + "' AND time BETWEEN '" + timeString1 + "' AND '" + timeString2 + "') ORDER BY date,time DESC";
                tempTable = GetTableFromDB(mySelectQuery);

                for (int i = 0; i < tempTable.Rows.Count; i++)
                {
                    tempString = tempString + tempTable.Rows[i][0].ToString();
                }
                return tempString;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (_DBConnect.Connection != null)
                    _DBConnect.CloseConnection();
            }
        }

        public List<string> Get_MeterListFromErrorLogs()
        {
            List<string> meterList = new List<string>();
            DataTable tempTable = new DataTable();
            try
            {
                _DBConnect.OpenConnection();
                string mySelectQuery = "SELECT DISTINCT `msn` FROM  `mdc_errorlog` ";
                tempTable = GetTableFromDB(mySelectQuery);

                for (int i = 0; i < tempTable.Rows.Count; i++)
                {
                    meterList.Add(tempTable.Rows[i][0].ToString());
                }
                return meterList;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (_DBConnect.Connection != null)
                    _DBConnect.CloseConnection();
            }

        }
        public List<string> Get_Error_DateList(string msn)
        {
            List<string> dateList = new List<string>();
            DataTable tempTable = new DataTable();
            try
            {
                if (msn != null && msn != string.Empty)
                {
                    _DBConnect.OpenConnection();
                    string mySelectQuery = "SELECT  DISTINCT`date` FROM  `mdc_errorlog` WHERE msn = '" + msn + "'";
                    tempTable = GetTableFromDB(mySelectQuery);

                    for (int i = 0; i < tempTable.Rows.Count; i++)
                    {
                        dateList.Add(tempTable.Rows[i][0].ToString());
                    }
                    return dateList;
                }
                else
                    return null;

            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (_DBConnect.Connection != null)
                    _DBConnect.CloseConnection();
            }

        }

        public string Get_MeterError(string msn)
        {
            string tempString = null;
            DataTable tempTable = new DataTable();
            try
            {
                _DBConnect.OpenConnection();
                string mySelectQuery = "SELECT  `error_string` FROM  `mdc_errorlog` WHERE msn = '" + msn + "' ORDER BY date,time DESC ";
                tempTable = GetTableFromDB(mySelectQuery);

                for (int i = 0; i < tempTable.Rows.Count; i++)
                {
                    tempString = tempString + tempTable.Rows[i][0].ToString();
                }
                return tempString;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                _DBConnect.CloseConnection();
            }

        }
        public string Get_MeterError(string msn, string field, string value)
        {
            string tempString = null;
            DataTable tempTable = new DataTable();
            try
            {
                _DBConnect.OpenConnection();
                string mySelectQuery = "SELECT  `error_string` FROM  `mdc_errorlog` WHERE msn = '" + msn + "' AND " + field + "=" + "'" + value + "' ORDER BY date,time DESC";
                tempTable = GetTableFromDB(mySelectQuery);

                for (int i = 0; i < tempTable.Rows.Count; i++)
                {
                    tempString = tempString + tempTable.Rows[i][0].ToString();
                }
                return tempString;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                _DBConnect.CloseConnection();
            }

        }
        public string Get_MeterError(string msn, string field, string timeString1, string timeString2)
        {
            string tempString = null;
            DataTable tempTable = new DataTable();
            try
            {
                _DBConnect.OpenConnection();
                string mySelectQuery = "SELECT  `error_string` FROM  `mdc_errorlog` WHERE msn = '" + msn + "' AND ( time BETWEEN '" + timeString1 + "' AND '" + timeString2 + "')";
                tempTable = GetTableFromDB(mySelectQuery);

                for (int i = 0; i < tempTable.Rows.Count; i++)
                {
                    tempString = tempString + tempTable.Rows[i][0].ToString();
                }
                return tempString;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                _DBConnect.CloseConnection();
            }
        }
        public string Get_MeterError_ByDateTime(string msn, string date, string timeString1, string timeString2)
        {
            string tempString = null;
            DataTable tempTable = new DataTable();
            try
            {
                _DBConnect.OpenConnection();
                //SELECT * FROM `mdc_log` WHERE msn = '3696111111' and (date = '2012-12-15' AND time = '12:55:58')
                string mySelectQuery = "SELECT  `error_string` FROM  `mdc_errorlog` WHERE msn = '" + msn + "' AND (date = '" + date + "' AND time BETWEEN '" + timeString1 + "' AND '" + timeString2 + "') ORDER BY date,time DESC";
                tempTable = GetTableFromDB(mySelectQuery);

                for (int i = 0; i < tempTable.Rows.Count; i++)
                {
                    tempString = tempString + tempTable.Rows[i][0].ToString();
                }
                return tempString;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                _DBConnect.CloseConnection();
            }
        }

        #endregion

        #region Statistics

        public bool insert_Statistics(string MSN, byte type, DateTime StartSession, long max_Allocated_Count, long max_Active_Session_Count, TimeSpan Duration, bool success)
        {
            try
            {
                string myInsertQuery = "INSERT INTO statistics (msn,type, max_allocated_meter_count, max_active_session_count, duration, datetime,success) VALUES('" + MSN + "','" + type + "','" + max_Allocated_Count + "','" + max_Active_Session_Count + "','" + Duration.ToString(TimeFormat) + "','" +
                StartSession.ToString(DateFormat) + "','" + (Convert.ToByte(success)) + "')";
                if (ExecuteQuery(myInsertQuery))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool InsertWarning(string MSN, DateTime SessionDateTime, DateTime MeterDateTime, string Warning)
        {
            try
            {
                //-----------------------------------------------------------------------------//
                //                          Modification by Rashad                             //
                //                          Date: 19-May-2017                             //
                //                          Version: 4.1.1.57                                 //
                //                          Query Update Details below:                        //
                //-----------------------------------------------------------------------------//
                //=============================================================================// 
                //  column removed   `reference_no`	                                           //
                //=============================================================================// 
                string query = String.Format("INSERT INTO warnings (msn, session_date_time, meter_date_time, warning_string) VALUES('{0}', '{1}', '{2}', '{3}')", MSN, SessionDateTime.ToString(DateFormat), MeterDateTime.ToString(DateFormat), Warning);
                OdbcCommand Command = new OdbcCommand(query, Connection);
                if (ExecuteQuery(Command))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while inserting Warning", ex);
            }
        }

        public Stats get_Statistics()
        {
            //try
            //{
            //    _DBConnect.OpenConnection();
            //    Stats tempStats = new Stats();
            //    string mySelectQuery = "SELECT  `max_allocated_meter_count` ,  `duration` FROM  `statistics` WHERE  `max_allocated_meter_count` = ( SELECT MAX(  `max_allocated_meter_count` ) FROM  `statistics` ORDER BY  `max_allocated_meter_count` )";
            //    DataTable DT = GetTableFromDB(mySelectQuery);

            //    if (DT.Rows.Count > 0)
            //    {
            //        tempStats.Max_Meters_Allocated = Convert.ToInt64(DT.Rows[0][0]);
            //        tempStats.Max_Meters_Duration = Convert.ToDouble(DT.Rows[0][1]);

            //        string mySelectQuery1 = "SELECT MAX(`max_allocated_meter_count`), MAX(duration) , MIN(duration) , AVG(CAST(duration AS TIME ) ) AS avg_field, " +
            //        "STDDEV_POP(CAST(duration AS TIME) ) AS std_field,  COUNT(duration) FROM  statistics";
            //        DT = GetTableFromDB(mySelectQuery1);

            //        tempStats.Max_duration = Convert.ToDouble(DT.Rows[0][DT.Columns["MAX(duration)"]]);
            //        tempStats.Min_duration = Convert.ToDouble(DT.Rows[0][DT.Columns["MIN(duration)"]]);
            //        tempStats.Avg_duration = Convert.ToDouble(DT.Rows[0][DT.Columns["avg_field"]]);
            //        tempStats.STD_duration = Convert.ToDouble(DT.Rows[0][DT.Columns["std_field"]]);
            //        tempStats.MaxTrans = Convert.ToInt64(DT.Rows[0][DT.Columns["COUNT(duration)"]]);

            //        return tempStats;
            //    }
            //    else
            //    {
            return null;
            //    }
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
            //finally
            //{
            //    _DBConnect.CloseConnection();
            //}
        }

        public void clearStatistics()
        {
            try
            {
                _DBConnect.OpenConnection();
                string query = "TRUNCATE `statistics`";
                ExecuteQuery(query);
            }
            catch (Exception error)
            {
                throw error;
            }
            finally
            {
                _DBConnect.CloseConnection();
            }
        }

        public bool insert_mdc_session(string version, DateTime start_time, DateTime stop_time)
        {
            try
            {
                _DBConnect.OpenConnection();
                #region SQL_QUERY_REGION
                string query = String.Format("INSERT INTO mdc_session(mdc_version,start_time, stop_time)" +
           "VALUES('{0}','{1}','{2}')", version, start_time.ToString(DateFormat), stop_time.ToString(DateFormat));
                #endregion

                if (ExecuteQuery(query))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }
        public bool update_mdc_session(DateTime stop_time, DateTime start_time)
        {
            try
            {
                _DBConnect.OpenConnection();
                #region SQL_QUERY_REGION
                string query = String.Format("Update mdc_session SET stop_time='{0}' where start_time='{1}'", stop_time.ToString(DateFormat), start_time.ToString(DateFormat));
                #endregion

                if (ExecuteQuery(query))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public bool UpdateWakeUpProcess(bool isConnectionTimeOrProcess, int status, long id)
        {
            try
            {
                _DBConnect.OpenConnection();
                if (id <= 0)
                    return false;
                #region SQL_QUERY_REGION

                string query;
                if (isConnectionTimeOrProcess)
                    query = string.Format("update `wakeup_status_log` set `conn_time` ='{0}', `conn_status`='{1}' where `wakeup_status_log_id`= '{2}'", DateTime.Now.ToString(DateFormat), status, id);
                else
                    query = string.Format("update `wakeup_status_log` set `completion_time` ='{0}', `completion_status` ='{1}' where `wakeup_status_log_id`= '{2}'", DateTime.Now.ToString(DateFormat), status, id);

                #endregion

                if (ExecuteQuery(query))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        #endregion

        #region Miscellaneous

        public DataTable GetTableFromDB(string Query)
        {
            try
            {
                _DBConnect.Command = new OdbcCommand(Query, ( OdbcConnection)_DBConnect.Connection);
                OdbcDataAdapter adapter = new OdbcDataAdapter();
                adapter.SelectCommand = (OdbcCommand)_DBConnect.Command;
                DataTable DT = new DataTable();
                adapter.Fill(DT);
                return DT;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetTableFromDB(OdbcCommand CommandToExecute)
        {
            OdbcDataAdapter adapter = null;
            DataTable DT = null;
            try
            {
                ///CommandToExecute.Connection = Connection;
                adapter = new OdbcDataAdapter(CommandToExecute);
                DT = new DataTable();
                adapter.Fill(DT);
                return DT;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                adapter.Dispose();
            }
        }

        public DataTable GetTableFromDB(OdbcCommand CommandToExecute, DataTable DT)
        {
            OdbcDataAdapter adapter = null;
            try
            {
                adapter = new OdbcDataAdapter(CommandToExecute);
                DT = new DataTable();
                adapter.Fill(DT);
                return DT;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //adapter.Dispose();
            }
        }

        public bool ExecuteQuery(string MyQuery, bool isThrow = false)
        {
            bool toReturn = false;
            try
            {
                DBConnect.OpenConnection();
                Command = new OdbcCommand(MyQuery, Connection);
                if (IsConnectionOpen)
                {
                    toReturn = Command.ExecuteNonQuery() > 0;
                    //toReturn = true;
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                if (isThrow) throw ex;
                if (_newException != null) _newException(ex);
                return false;
            }
        }

        public bool ExecuteQuery(OdbcCommand MyQuery)
        {
            try
            {
                if (!IsConnectionOpen)
                {
                    DBConnect.OpenConnection();
                }
                MyQuery.Connection = (OdbcConnection)DBConnect.Connection;
                MyQuery.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }
        }

        public bool ExecuteCommandWithAlreadyOpenedConnection(OdbcCommand MyCommand)
        {
            try
            {
                int Result = MyCommand.ExecuteNonQuery();
                if (Result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ExecuteUpdateCommand(OdbcCommand MyCommand)
        {
            try
            {
                int Result = MyCommand.ExecuteNonQuery();
                if (Result > 0)
                    return true;
                else return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw ex;
            }
        }

        public void InsertIntruders(string MSN, string ConnectionInfo, DateTime ConnectionDateTime)
        {
            try
            {
                DBConnect.OpenConnection();
                //if (!TryUpdateIntruderInformation(MSN, ConnectionInfo, ConnectionDateTime))
                // {
                // string query = "INSERT INTO intruders(`msn`, `connection_info`, `connection_date_time`) VALUES " + String.Format("('{0}', '{1}', '{2}')", MSN, ConnectionInfo, ConnectionDateTime.ToString(DateFormat));
                string query = "Replace INTO intruders(`msn`, `connection_info`, `connection_date_time`) VALUES " + String.Format("('{0}', '{1}', '{2}')", MSN, ConnectionInfo, ConnectionDateTime.ToString(DateFormat));
                OdbcCommand Command = new OdbcCommand(query, Connection);
                ExecuteCommandWithAlreadyOpenedConnection(Command);

                //Insert Into intruders Log As Well
                query = "Replace INTO intruders_log(`msn`, `connection_info`, `connection_date_time`) VALUES " + String.Format("('{0}', '{1}', '{2}')", MSN, ConnectionInfo, ConnectionDateTime.ToString(DateFormat));
                Command = new OdbcCommand(query, Connection);
                ExecuteCommandWithAlreadyOpenedConnection(Command);
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw ex;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public bool TryUpdateIntruderInformation(string MSN, string ConnectionInfo, DateTime ConnectionDateTime)
        {
            try
            {
                string query = String.Format("UPDATE intruders SET connection_info = '{0}', connection_date_time = '{1}' WHERE msn = '{2}'", ConnectionInfo, ConnectionDateTime.ToString(DateFormat), MSN);
                OdbcCommand Command = new OdbcCommand(query, Connection);
                if (ExecuteUpdateCommand(Command))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw ex;
            }
        }

        public void InsertMeterConnectionTime(string MSN, string ConnectionInfo, DateTime ConnectionDateTime)
        {
            try
            {
                DBConnect.OpenConnection();
                string query = "INSERT INTO meter_connection_life (msn, connection_info, connection_time) VALUES " + String.Format("('{0}', '{1}', '{2}')", MSN, ConnectionInfo, ConnectionDateTime.ToString(DateFormat));
                OdbcCommand Command = new OdbcCommand(query, Connection);
                ExecuteCommandWithAlreadyOpenedConnection(Command);
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw ex;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public void UpdateMeterDisconnectionTime(string MSN, DateTime ConnectionDateTime, DateTime DisconnectionDateTime)
        {
            try
            {
                DBConnect.OpenConnection();
                string query = String.Format("UPDATE meter_connection_life SET disconnection_time = '{0}' WHERE msn = '{1}' AND connection_time = '{2}'", DisconnectionDateTime.ToString(DateFormat), MSN, ConnectionDateTime.ToString(DateFormat));
                OdbcCommand Command = new OdbcCommand(query, Connection);
                ExecuteCommandWithAlreadyOpenedConnection(Command);
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw ex;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        // INSERT INTO t1 (a,b,c) VALUES (1,2,3)
        // ON DUPLICATE KEY UPDATE c=c+1;

        public void UpdateMeterConnectionTimeForLiveData(string MSN, DateTime ConnectionDateTime)
        {
            try
            {
                DBConnect.OpenConnection();

                string query = String.Format("UPDATE instantaneous_data_live SET connection_time = '{0}' WHERE msn = '{1}'", ConnectionDateTime.ToString(DateFormat), MSN);
                OdbcCommand Command = new OdbcCommand(query, Connection);
                if (ExecuteUpdateCommand(Command))
                    return;
                else
                    InsertMeterConnectionTimeForLiveData(MSN, ConnectionDateTime);

                //InsertUpdateMeterConnectionTimeForLiveData(MSN, ConnectionDateTime);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public void InsertMeterConnectionTimeForLiveData(string MSN, DateTime ConnectionDateTime)
        {
            try
            {
                DBConnect.OpenConnection();
                string query = String.Format("INSERT INTO instantaneous_data_live (msn, connection_time) VALUES ('{0}', '{1}')", MSN, ConnectionDateTime.ToString(DateFormat));
                OdbcCommand Command = new OdbcCommand(query, Connection);
                ExecuteCommandWithAlreadyOpenedConnection(Command);
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw ex;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public void InsertUpdateMeterConnectionTimeForLiveData(string MSN, DateTime ConnectionDateTime)
        {
            try
            {
                DBConnect.OpenConnection();
                string query = String.Format("INSERT INTO `instantaneous_data_live` (`msn`, `connection_time`) VALUES ('{0}', '{1}') ON DUPLICATE KEY UPDATE `connection_time`='{1}'", MSN, ConnectionDateTime.ToString(DateFormat));
                OdbcCommand Command = new OdbcCommand(query, Connection);
                ExecuteCommandWithAlreadyOpenedConnection(Command);
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw ex;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }


        public void DisableLoadProfile(string MSN)
        {
            try
            {
                string query = "UPDATE `meter` SET `read_lp` = 0 WHERE `msn` = '" + MSN + "'";
                OdbcCommand Command = new OdbcCommand(query, Connection);
                ExecuteCommandWithAlreadyOpenedConnection(Command);
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw ex;
            }
        }

        public void DisableQuantity(string MSN, string Quantity)
        {
            try
            {
                string query = "UPDATE `meter` SET `" + Quantity + "` = 0 WHERE `msn` = " + MSN + "";
                OdbcCommand Command = new OdbcCommand(query, Connection);
                ExecuteCommandWithAlreadyOpenedConnection(Command);
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw new Exception(String.Format("Error while Disabling {0}", Quantity), ex);
            }
        }

        public bool UpdateLoadProfileDefaultSettings(string MSN, LoadProfileScheme lpScheme)
        {
            try
            {
                DBConnect.OpenConnection();
                //int loadProfileGroupID = getLoadProfileID(channels, model);
                var Fields = new string[] { string.Format("load_profile{0}_counter", lpScheme == LoadProfileScheme.Scheme_1 ? string.Empty : ((byte)lpScheme).ToString()) };
                var Values = new string[] { "0" };
                if (GeneralUpdate("meter", Fields, Values, "msn", "=", MSN))
                    return true;
                return false;
            }
            catch
            {

                throw;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        // public int getLoadProfileID(LP_Params channels, uint meterModel)
        // {
        //     try
        //     {
        //         string query = String.Format("Select load_profile_group_id from load_profile_group where channel_1='{0}' AND channel_2='{1}' AND channel_3='{2}' AND channel_4='{3}'", channels.Channel_1, channels.Channel_2, channels.Channel_3, channels.Channel_4);
        //         OdbcCommand Command = new OdbcCommand(query, Connection);
        //         try
        //         {
        //             int id = Convert.ToInt16(Command.ExecuteScalar());
        //             if (id > 0) return id;
        //             else throw new Exception();
        //         }
        //         catch (Exception ex) //load profile group not found
        //         {
        //             uint configID = getConfigID(meterModel);
        //             string insertQuery = String.Format("Insert into load_profile_group (ConfigId,channel_1,channel_2,channel_3,channel_4) VALUES('{0}','{1}','{2}','{3}','{4}')", configID, channels.Channel_1, channels.Channel_2, channels.Channel_3, channels.Channel_4);
        //             Command = new OdbcCommand(insertQuery, Connection);
        //             if (Command.ExecuteNonQuery() > 0)
        //             {
        //                 string cmd = "Select last_insert_id() as last_id from load_profile_group";
        //                 Command = new OdbcCommand(cmd, Connection);
        //                 OdbcDataReader reader = Command.ExecuteReader();
        //                 reader.Read();
        //                 return (Convert.ToInt16(reader["last_id"]));
        //             }
        //             else
        //             {
        //                 return 0;
        //             }
        //         }
        //     }
        //     catch (Exception)
        //     {
        //         throw;
        //     }
        // }

        #endregion

        #region MDC Complete Statistics

        public bool insert_mdc_status(MDC_Status Status)
        {
            try
            {
                _DBConnect.OpenConnection();
                #region SQL_QUERY_REGION
                string query = "INSERT INTO mdc_status(start_time, stop_time, last_kas_poll_time, kam_count, ka_trans_count, ka_success_trans, kam_pool_count, nkam_count,                          nka_trans_count, nka_success_trans)" +
           "VALUES('" + Status.SessionStart.ToString(DateFormat) + "','" + Status.SessionStop.ToString(DateFormat) + "','" + Status.KA_Last_Pooling_Time.ToString(DateFormat) + "'," + Status.KA_Connection_Count + "," + Status.KA_Expected_Transactions + "," + Status.KA_Successful_Transactions + "," + Status.KA_Pooling_Count + "," + Status.NKA_Connection_Count + "," + Status.NKA_Expected_Transactions + "," + Status.NKA_Successful_Transactions + ")";
                #endregion

                if (ExecuteQuery(query))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        /// <summary>
        /// This Method is deprecated please use SaveAlarmStatus instead
        /// </summary>
        /// <param name="msn"></param>
        /// <param name="events"></param>
        /// <param name="alarms"></param>
        /// <param name="session_time"></param>
        public void InsertMDCEvents(string msn, string events, BitArray alarms, DateTime session_time)
        {
            try
            {
                //StringBuilder query = new StringBuilder();
                //var columnNames = Enum.GetNames(typeof(MDCEvents));
                //query.Append("update alarm_status set `is_alarm_processed` = 0 ,`mdc_alarm_string` ='" + events + "'");
                //for (int i = 0; i < columnNames.Length; i++) 
                //{
                //    var evnt= (MDCEvents)Enum.Parse(typeof(MDCEvents),columnNames[i]);
                //    query.Append(string.Format(",`{0}`={1}", columnNames[i],Convert.ToByte(alarms[(ushort)evnt])));
                //}
                ////query=query.Remove(query.Length - 1, 1);
                //query.Append(string.Format(" where `msn` = '{0}' and `session_date_time` = '{1}' ", msn, session_time.ToString(DateFormat)));

                //ExecuteQuery(query.ToString());

            }
            catch (Exception)
            {

            }
            finally
            {

            }


        }

        #endregion

        #region SinglePhase +KESC

        public bool saveCumulativeBillingData_KESC(BillingData Data, DateTime SessionDateTime, string MSN, MeterInformation MeterInfo)
        {
            OdbcCommand Command = null;
            try
            {
                //DBConnect.OpenConnection();
                BillingItem AEP = Data.BillingItems.Find(x => x.Name.Equals("Active Energy Positive"));
                BillingItem AEN = Data.BillingItems.Find(x => x.Name.Equals("Active Energy Negative"));
                BillingItem REP = Data.BillingItems.Find(x => x.Name.Equals("Reactive Energy Positive"));
                BillingItem REN = Data.BillingItems.Find(x => x.Name.Equals("Reactive Energy Negative"));
                BillingItem AMDI = Data.BillingItems.Find(x => x.Name.Equals("Active MDI"));
                BillingItem RMDI = Data.BillingItems.Find(x => x.Name.Equals("Reactive MDI"));
                BillingItem CMA = Data.BillingItems.Find(x => x.Name.Equals("Current Month Active MDI"));
                BillingItem CMR = Data.BillingItems.Find(x => x.Name.Equals("Current Month Reactive MDI"));


                string query = String.Format("INSERT  INTO cumm_billing_data_detailed " +
                 " (`msn`, `session_date_time`, `meter_date_time`, `active_energy_pos_t1`, `active_energy_pos_t2`, `active_energy_pos_t3`, `active_energy_pos_t4`, `active_energy_pos_tl`, `active_energy_neg_t1`, `active_energy_neg_t2`, `active_energy_neg_t3`, `active_energy_neg_t4`, `active_energy_neg_tl`, `reactive_energy_pos_t1`, `reactive_energy_pos_t2`, `reactive_energy_pos_t3`, `reactive_energy_pos_t4`, `reactive_energy_pos_tl`, `reactive_energy_neg_t1`, `reactive_energy_neg_t2`, `reactive_energy_neg_t3`, `reactive_energy_neg_t4`, `reactive_energy_neg_tl`, `active_mdi_t1`, `active_mdi_t2`, `active_mdi_t3`, `active_mdi_t4`, `active_mdi_tl`, `reactive_mdi_t1`, `reactive_mdi_t2`, `reactive_mdi_t3`, `reactive_mdi_t4`, `reactive_mdi_tl`," +
                  //`currentmonth_activemdi_t1`, `currentmonth_activemdi_t2`, `currentmonth_activemdi_t3`, `currentmonth_activemdi_t4`, `currentmonth_activemdi_tl`, `currentmonth_reactivemdi_t1`, `currentmonth_reactivemdi_t2`, `currentmonth_reactivemdi_t3`, `currentmonth_reactivemdi_t4`, `currentmonth_reactivemdi_tl`,
                  "`ct`,`pt`,`customer_id`)" +
                 "VALUES('{0}', '{1}', '{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}',{35})"//,'{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}','{46}')"
                , MSN
                , SessionDateTime.ToString(DateFormat)
                , Data.TimeStamp.ToString(DateFormat)

                , AEP.Value.T1
                , AEP.Value.T2
                , AEP.Value.T3
                , AEP.Value.T4
                , AEP.Value.TL

                , AEN.Value.T1
                , AEN.Value.T2
                , AEN.Value.T3
                , AEN.Value.T4
                , AEN.Value.TL


                , REP.Value.T1
                , REP.Value.T2
                , REP.Value.T3
                , REP.Value.T4
                , REP.Value.TL

                , REN.Value.T1
                , REN.Value.T2
                , REN.Value.T3
                , REN.Value.T4
                , REN.Value.TL

                , AMDI.Value.T1
                , AMDI.Value.T2
                , AMDI.Value.T3
                , AMDI.Value.T4
                , AMDI.Value.TL


                , RMDI.Value.T1
                , RMDI.Value.T2
                , RMDI.Value.T3
                , RMDI.Value.T4
                , RMDI.Value.TL
                /*
                , CMA.Value.T1
                , CMA.Value.T2
                , CMA.Value.T3
                , CMA.Value.T4
                , CMA.Value.TL

                , CMR.Value.T1
                , CMR.Value.T2
                , CMR.Value.T3
                , CMR.Value.T4
                , CMR.Value.TL
                 */
                //, reference_no
                , MeterInfo.CT
                , MeterInfo.PT
                , MeterInfo.Customer_ID);

                Command = new OdbcCommand(query, Connection);
                if (ExecuteCommandWithAlreadyOpenedConnection(Command))
                    return true;
                else
                    throw new Exception("");
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw new Exception("Error while saving Detailed Cumulative Billing Data", ex);
            }
            finally
            {
                try
                {
                    if (Command != null)
                    {
                        Command.Dispose();
                    }
                    //DBConnect.CloseConnection();
                }
                catch
                { }
            }

        }

        //public bool saveCumulativeBillingData_SinglePhase(cumulativeBilling_SinglePhase Data, DateTime SessionDateTime)
        //{
        //    OdbcCommand Command = null;
        //    try
        //    {
        //        DBConnect.OpenConnection();
        //        string query = String.Format("INSERT DELAYED INTO cumm_billing_data_singlephase VALUES('{0}', '{1}', '{2}', '{3}','{4}')"
        //        , Data.msn
        //        , SessionDateTime.ToString(DateFormat)
        //        , Data.date.ToString(DateFormat)
        //        , Data.activeEnergy
        //        , Data.activeMDI);

        //        Command = new OdbcCommand(query, Connection);
        //        if (ExecuteCommandWithAlreadyOpenedConnection(Command))
        //            return true;
        //        else
        //            return false;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            if (Command != null)
        //            {
        //                Command.Dispose();
        //            }
        //            DBConnect.CloseConnection();
        //        }
        //        catch (Exception)
        //        { }
        //    }

        //}

        public bool saveCumulativeBillingData_SinglePhase(cumulativeBilling_SinglePhase Data, DateTime SessionDateTime, MeterInformation meterInfo)
        {
            OdbcCommand Command = null;
            try
            {
                DBConnect.OpenConnection();
                string query = String.Format("INSERT INTO cumm_billing_data_singlephase (`msn`, `date_time`, `meter_date_time`, `active_energy`, `active_mdi`,`customer_id`) VALUES ('{0}','{1}', '{2}', '{3}', '{4}',{5})"

                , Data.msn
                , SessionDateTime.ToString(DateFormat)
                , Data.date.ToString(DateFormat)
                , Commons.Validate_BillData(Data.activeEnergy)
                , Commons.Validate_BillData(Data.activeMDI)
                , meterInfo.Customer_ID);

                Command = new OdbcCommand(query, Connection);
                if (ExecuteCommandWithAlreadyOpenedConnection(Command))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }
            finally
            {
                try
                {
                    if (Command != null)
                    {
                        Command.Dispose();
                    }
                    DBConnect.CloseConnection();
                }
                catch (Exception)
                { }
            }

        }

        public bool saveMonthlyBillingData_SinglePhase(Monthly_Billing_data_SinglePhase MB_data, DateTime SessionDateTime, MeterInformation meterInfo)
        {
            OdbcCommand Command = new OdbcCommand();
            try
            {
                DBConnect.OpenConnection();
                Command.Connection = Connection;
                foreach (var Data in MB_data.monthly_billing_data)
                {

                    Command.CommandText = String.Format("INSERT INTO monthly_billing_data_singlephase(`customer_id`,`msn`,`date_time` ,`meter_date_time`,`period_count`,`active_energy`,`active_mdi`) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}',{6})"
                    , meterInfo.Customer_ID
                    , Data.billData_obj.msn
                    , SessionDateTime.ToString(DateFormat)
                    , Data.billData_obj.date.ToString(DateFormat)
                    , Data.Counter
                    , Commons.Validate_BillData(Data.billData_obj.activeEnergy)
                    , Commons.Validate_BillData(Data.billData_obj.activeMDI)
                    );

                    ExecuteCommandWithAlreadyOpenedConnection(Command);
                }
                Command.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }
            finally
            {
                try
                {
                    if (Command != null)
                    {
                        Command.Dispose();
                    }
                    DBConnect.CloseConnection();
                }
                catch (Exception)
                { }
            }

        }

        public GetTariff getTariffDetails(int BillingDetailID)
        {
            GetTariff obj_tariff = new GetTariff();
            OdbcCommand Command = null;
            try
            {
                string query = "SELECT * FROM tariff_details WHERE billing_detail_id = '" + BillingDetailID + "'";

                Command = new OdbcCommand(query, Connection);

                DataTable table = GetTableFromDB(Command);
                obj_tariff.T1 = Convert.ToBoolean(table.Rows[0][1]);
                obj_tariff.T2 = Convert.ToBoolean(table.Rows[0][2]);
                obj_tariff.T3 = Convert.ToBoolean(table.Rows[0][3]);
                obj_tariff.T4 = Convert.ToBoolean(table.Rows[0][4]);
                obj_tariff.TL = Convert.ToBoolean(table.Rows[0][5]);

                return obj_tariff;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return null;
            }
        }

        public bool SaveInstantaneous_SinglePhase(Instantaneous_SinglePhase Data, DateTime SessionDateTime, double SignalStrength, bool is_ss_read, MeterInformation meterInfo)
        {
            if (!(SignalStrength > -116 && SignalStrength <= -52))
            {
                SignalStrength = -116;
            }
            OdbcCommand Command = null;
            try
            {
                DBConnect.OpenConnection();

                string query = String.Format("INSERT  INTO instantaneous_data_singlephase (`customer_id`,`msn` ,`date_time`,`meter_date_time`,`current`,`voltage`,`intantaneous_active_power`,`instantaneous_powerfactor`,`frequency`) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}',{8})"
                , meterInfo.Customer_ID
                , Data.MSN
                , SessionDateTime.ToString(DateFormat)
                , Data.dateTime.GetDateTime().ToString(DateFormat)
                , Commons.Validate_BillData(Data.current)
                , Commons.Validate_BillData(Data.voltage)
                , Commons.Validate_BillData(Data.instantaneousActivePower)
                , Commons.Validate_BillData(Data.powerFactor)
                , Commons.Validate_BillData(Data.frequency));

                Command = new OdbcCommand(query, Connection);
                if (ExecuteCommandWithAlreadyOpenedConnection(Command))
                //  return true;
                {

                    query = String.Format("INSERT  INTO `weekly_instantaneous_data` (`msn`, `session_date_time`, `time`, `date`,`meter_date_time`, `current_phase_total`,`voltage_phase_total`, `active_pwr_pos_phase_total`,  `frequency`, `signal_strength`,`customer_id`) VALUES ('{0}','{1}',CURTIME(),CURDATE(),'{2}','{3}','{4}','{5}','{6}','{7}',{8})"

                        , Data.MSN
                        , SessionDateTime.ToString(DateFormat)
                        , Data.dateTime.GetDateTime().ToString(DateFormat)
                        , Commons.Validate_BillData(Data.current)
                        , Commons.Validate_BillData(Data.voltage)
                        , Commons.Validate_BillData(Data.instantaneousActivePower)
                        , Commons.Validate_BillData(Data.frequency)
                        , (is_ss_read) ? SignalStrength.ToString() : DBNULLParam
                        , meterInfo.Customer_ID);

                    Command = new OdbcCommand(query, Connection);
                    ExecuteCommandWithAlreadyOpenedConnection(Command);

                    return true;

                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }
            finally
            {
                try
                {
                    if (Command != null)
                    {
                        Command.Dispose();
                    }
                }
                catch (Exception)
                { }
            }

        }

        #endregion

        #region Contactor

        //deprecated
        public bool getContactorRequest(MeterInformation MI, out ContactorControlData obj_req, bool OnDemandOff)
        {
            try
            {
                if (!_DBConnect.IsConnectionOpen)
                    DBConnect.OpenConnection();
                string request = string.Empty;
                string query = string.Format("SELECT contactor_id,msn,reference_number,customer_id, command, command_type,command_date_time,activation_time, expiry_time FROM "
                    + "contactor_control_data WHERE msn = '{0}'", MI.MSN);
                OdbcCommand Command = new OdbcCommand(query, Connection);

                using (var reader = Command.ExecuteReader())
                {
                    var dLits = Commons.MapReaderToObject<ContactorControlData>(reader);
                    return ContactorControlData.GetCommandToExecute(MI, dLits, out obj_req, OnDemandOff); 
                }
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw ex;
            }
        }
        public bool UpdateContactorLognData(string msn, ContactorControlData obj_req)
        {
            try
            {
                if (!_DBConnect.IsConnectionOpen)
                    DBConnect.OpenConnection();
                string request = string.Empty;
                string query = obj_req.GetContactorLogQuery(msn, obj_req.ActivationTime);
                OdbcCommand Command = new OdbcCommand(query, Connection);
                Command.ExecuteNonQuery();
                Command.CommandText = obj_req.GetContactorDeleteQuery(msn, obj_req.ActivationTime);
                Command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public bool UpdateContactorLogLive(string msn, ContactorControlData obj_req)
        {
            try
            {
                if (!_DBConnect.IsConnectionOpen)
                    DBConnect.OpenConnection();
                string request = string.Empty;
                string query = ContactorControlData.GetContactorLogLiveQuery(msn, obj_req);
                OdbcCommand Command = new OdbcCommand(query, Connection);
                var reader = Command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public bool UpdateContactorLiveData(string msn, long id, DateTime now, bool onOrOff)
        {
            string query = string.Empty;
            try
            {
                if (!_DBConnect.IsConnectionOpen)
                    DBConnect.OpenConnection();
                if (onOrOff)
                    query = string.Format("update instantaneous_data_live set last_contactor_on_req_time ='{0}' where msn ='{1}'", now.ToString(DateFormat), msn);
                else
                    query = string.Format("update instantaneous_data_live set last_contactor_off_req_time ='{0}' where msn ='{1}'", now.ToString(DateFormat), msn);
                OdbcCommand Command = new OdbcCommand(query, Connection);
                Command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                #region Insert New Record
                try
                {
                    if (onOrOff)
                        query = string.Format("insert into instantaneous_data_live(msn,last_contactor_on_req_time) values('{0}',{1})", msn, now.ToString(DateFormat));
                    else
                        query = string.Format("insert into instantaneous_data_live(msn,last_contactor_off_req_time) values('{0}',{1})", msn, now.ToString(DateFormat));
                    OdbcCommand Command = new OdbcCommand(query, Connection);
                    Command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    if (_newException != null) _newException(ex);
                }
                #endregion
                return false;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public bool updateContactorStatus(string MSN, string status)
        {
            try
            {
                if (!_DBConnect.IsConnectionOpen)
                    DBConnect.OpenConnection();

                //insert new record
                string query = String.Format("Replace into acknowledgement_data( `msn`, `ack_dateTime`, `log`) VALUES('{0}','{1}','{2}')", MSN, DateTime.Now.ToString(DateFormat), status);
                OdbcCommand Command = new OdbcCommand(query, Connection);

                if (ExecuteCommandWithAlreadyOpenedConnection(Command))
                    return true;
                else
                    return false;


            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }
        }
        public bool updateContactorACKstatus(string MSN, string refNumber, string status)
        {
            try
            {
                if (!_DBConnect.IsConnectionOpen)
                    DBConnect.OpenConnection();
                if (refNumber != string.Empty)
                {
                    string query = String.Format("Replace into acknowledgement_data SET log='{0}',ack_dateTime='{3}' where msn='{1}' AND reference_number='{2}'", status, MSN, refNumber, DateTime.Now.ToString(DateFormat));
                    OdbcCommand Command = new OdbcCommand(query, Connection);

                    if (ExecuteCommandWithAlreadyOpenedConnection(Command))
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }
        }
        public bool deleteContactorRequestEntry(string MSN)
        {
            try
            {
                DBConnect.OpenConnection();
                string query = String.Format("DELETE FROM contactor_control_data WHERE MSN ={0}", MSN);
                OdbcCommand Command = new OdbcCommand(query, Connection);

                if (ExecuteCommandWithAlreadyOpenedConnection(Command))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }
        public bool updateFINALContactorStatus(string MSN, string status, string final_status)
        {
            try
            {
                if (!_DBConnect.IsConnectionOpen)
                    DBConnect.OpenConnection();
                string query = String.Format("Update acknowledgement_data SET log='{0}',ack_dateTime='{2}',ack_status={3} where msn='{1}'", status, MSN, DateTime.Now.ToString(DateFormat), final_status);

                OdbcCommand Command = new OdbcCommand(query, Connection);

                if (ExecuteCommandWithAlreadyOpenedConnection(Command))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }
        }
        //Contactor parameters added by furqan 11-21-2014
        public bool GetContactorParamFromDB(long paramId, ref Param_ContactorExt obj_Param, ref Param_Limits obj_limit, ref Param_Monitoring_Time obj_MT)
        {
            try
            {
                if (!_DBConnect.IsConnectionOpen)
                    _DBConnect.OpenConnection();

                OdbcCommand cmd = new OdbcCommand();
                cmd.CommandText = "select *from contactor_params where `contactor_param_id` = ?";
                cmd.Parameters.Add(new OdbcParameter("@id", paramId));
                cmd.Connection = _DBConnect.Connection;
                var tbl = GetTableFromDB(cmd);
                if (obj_Param == null) return false;

                if (tbl.Rows.Count == 1)
                {
                    obj_Param.WriteContactorParam = Convert.ToBoolean(tbl.Rows[0]["write_contactor_param"]);
                    //============================================================================================
                    obj_Param.Contactor_OFF_Pulse_Time = Convert.ToUInt16(tbl.Rows[0]["contactor_off_pulse_time_ms"]);
                    //============================================================================================
                    obj_Param.Contactor_ON_Pulse_Time = Convert.ToUInt16(tbl.Rows[0]["contactor_on_pulse_time_ms"]);
                    //============================================================================================
                    obj_Param.Minimum_Interval_Bw_Contactor_State_Change = Convert.ToUInt16(tbl.Rows[0]["interval_btw_contactor_state_change_sec"]);
                    //============================================================================================
                    obj_Param.Power_Up_Delay_To_State_Change = Convert.ToUInt16(tbl.Rows[0]["power_up_delay_to_change_state_sec"]);
                    //============================================================================================
                    obj_Param.Interval_Contactor_Failure_Status = Convert.ToUInt16(tbl.Rows[0]["interval_to_contactor_failure_status_sec"]);
                    //============================================================================================
                    obj_Param.on_by_optically = Convert.ToBoolean(tbl.Rows[0]["optically_connect"]);
                    //============================================================================================
                    obj_Param.off_by_optically = Convert.ToBoolean(tbl.Rows[0]["optically_disconnect"]);
                    //============================================================================================
                    obj_Param.reconnect_by_tariff_change = Convert.ToBoolean(tbl.Rows[0]["tariff_change"]);
                    //============================================================================================
                    obj_Param.reconnect_automatic_or_switch = Convert.ToBoolean(tbl.Rows[0]["is_retry_automatic_or_switch"]);
                    //============================================================================================
                    obj_Param.RetryCount = Convert.ToByte(tbl.Rows[0]["retry_count"]);
                    //============================================================================================
                    obj_Param.Interval_Between_Retries = Convert.ToUInt32(tbl.Rows[0]["retry_auto_interval_in_sec"]);
                    //============================================================================================
                    obj_Param.Reconnect_By_Switch_on_Retries_Expire = Convert.ToBoolean(tbl.Rows[0]["reconnect_by_switch_on_expire"]);
                    //============================================================================================
                    obj_Param.Reconnect_Automatically_on_Retries_Expire = Convert.ToBoolean(tbl.Rows[0]["reconnect_automatic_on_expire"]);
                    //============================================================================================
                    obj_Param.Control_Mode = Convert.ToByte(tbl.Rows[0]["on_retry_expire_auto_interval_min"]);
                    //============================================================================================
                    obj_Param.Over_Load_T1_FLAG_0 = Convert.ToBoolean(tbl.Rows[0]["turn_contactor_off_overload_t1"]);
                    //============================================================================================
                    obj_Param.Over_Load_T2_FLAG_1 = Convert.ToBoolean(tbl.Rows[0]["tunr_contactor_off_overload_t2"]);
                    //============================================================================================
                    obj_Param.Over_Load_T3_FLAG_2 = Convert.ToBoolean(tbl.Rows[0]["turn_contactor_off_overload_t3"]);
                    //============================================================================================
                    obj_Param.Over_Load_T4_FLAG_3 = Convert.ToBoolean(tbl.Rows[0]["turn_contactor_off_overload_t4"]);
                    //============================================================================================ 
                    obj_limit.WriteOverLoadTotal_T1 = Convert.ToBoolean(tbl.Rows[0]["write_limit_over_load_total_kW_t1"]);
                    obj_limit.OverLoadTotal_T1 = Convert.ToDouble(tbl.Rows[0]["limit_over_load_total_kW_t1"]);
                    //============================================================================================ 
                    obj_limit.WriteOverLoadTotal_T2 = Convert.ToBoolean(tbl.Rows[0]["write_limit_over_load_total_kW_t2"]);
                    obj_limit.OverLoadTotal_T2 = Convert.ToDouble(tbl.Rows[0]["limit_over_load_total_kW_t2"]);
                    //============================================================================================ 
                    obj_limit.WriteOverLoadTotal_T3 = Convert.ToBoolean(tbl.Rows[0]["write_limit_over_load_total_kW_t3"]);
                    obj_limit.OverLoadTotal_T3 = Convert.ToDouble(tbl.Rows[0]["limit_over_load_total_kW_t3"]);
                    //============================================================================================ 
                    obj_limit.WriteOverLoadTotal_T4 = Convert.ToBoolean(tbl.Rows[0]["write_limit_over_load_total_kW_t4"]);
                    obj_limit.OverLoadTotal_T4 = Convert.ToDouble(tbl.Rows[0]["limit_over_load_total_kW_t4"]);
                    //============================================================================================ 
                    obj_MT.WriteOverLoad = Convert.ToBoolean(tbl.Rows[0]["write_monitoring_time"]);
                    obj_MT.OverLoad = new TimeSpan(0, 0, Int32.Parse(tbl.Rows[0]["monitering_time_over_load"].ToString()));
                    /*double obj_tp;
                    if (double.TryParse((tbl.Rows[0]["monitering_time_over_load"]).ToString(), out obj_tp))
                    {
                        obj_MT.OverLoad = new TimeSpan(0, 0, (int)obj_tp);
                    }
                    else
                    {
                        obj_MT.OverLoad = new TimeSpan(0, 3, 0);
                    }*/
                    //============================================================================================ 
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }
        }

        #endregion

        #region Modem

        public bool GetModemParamFromDB(long paramId, ref Param_ModemLimitsAndTime obj_Param)
        {
            try
            {
                if (!_DBConnect.IsConnectionOpen)
                    _DBConnect.OpenConnection();

                OdbcCommand cmd = new OdbcCommand();
                cmd.CommandText = "select * from modem_limits_time_param where `modem_param_id` = ?";
                cmd.Parameters.Add(new OdbcParameter("@id", paramId));
                cmd.Connection = _DBConnect.Connection;
                var tbl = GetTableFromDB(cmd);
                if (obj_Param == null) return false;

                if (tbl.Rows.Count == 1)
                {
                    //============================================================================================
                    obj_Param.Time_between_Retries_SMS = Convert.ToUInt16(tbl.Rows[0]["time_between_retries_sms_sec"]);
                    //============================================================================================
                    obj_Param.Time_between_Retries_IP_connection = Convert.ToUInt16(tbl.Rows[0]["time_between_retries_ip_con_sec"]);
                    //============================================================================================
                    obj_Param.Time_between_Retries_UDP = Convert.ToUInt16(tbl.Rows[0]["time_between_retries_udp_data_sec"]);
                    //============================================================================================
                    obj_Param.Time_between_Retries_TCP = Convert.ToUInt16(tbl.Rows[0]["time_between_retries_tcp_data_sec"]);
                    //============================================================================================
                    obj_Param.Time_between_Retries_Data_Call = Convert.ToUInt16(tbl.Rows[0]["time_between_retries_data_call_sec"]);
                    //============================================================================================
                    obj_Param.TimeRetriesAlwaysOnCycle = Convert.ToUInt16(tbl.Rows[0]["time_between_retries_always_on_cycles_sec"]);
                    //============================================================================================
                    obj_Param.TCP_Inactivity = Convert.ToUInt16(tbl.Rows[0]["tcp_inactivity_time_sec"]);
                    //============================================================================================
                    obj_Param.TimeOut_CipSend = Convert.ToUInt16(tbl.Rows[0]["timeout_sip_send"]);
                    //============================================================================================
                    obj_Param.Retry_IP_connection = Convert.ToByte(tbl.Rows[0]["retries_ip_connection"]);
                    //============================================================================================
                    obj_Param.Retry_SMS = Convert.ToByte(tbl.Rows[0]["retries_sms"]);
                    //============================================================================================
                    obj_Param.Retry_TCP = Convert.ToByte(tbl.Rows[0]["retries_tcp_data"]);
                    //============================================================================================
                    obj_Param.Retry_UDP = Convert.ToByte(tbl.Rows[0]["retries_udp_data"]);
                    //============================================================================================
                    obj_Param.Retry = Convert.ToByte(tbl.Rows[0]["retries_data_call"]);
                    //============================================================================================
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }

        }

        #endregion

        #region Number Profile

        /// <summary>
        /// Initialize NumberProfile Parameters From Database
        /// </summary>
        /// <param name="grpId">Number Profile Group</param>
        /// <param name="obj_numberProfile"></param>
        /// <returns></returns>
        public bool InitNumberProfilesParams(bool IsStandardParam, int grpId, out Param_Standard_Number_Profile[] obj_numberProfile)
        {
            try
            {
                if (!_DBConnect.IsConnectionOpen)
                    _DBConnect.OpenConnection();

                // OdbcConnection con = new OdbcConnection(_DBConnect.ConnectionString);
                OdbcCommand cmd = new OdbcCommand();
                cmd.Connection = _DBConnect.Connection;//_DBConnect.Connection;
                cmd.CommandText = "select * from `number_profile_params` where `number_profile_group_id` = ?";
                cmd.Parameters.Add(new OdbcParameter("@id", grpId));

                var tbl = GetTableFromDB(cmd);

                if (tbl.Rows.Count >= 1 && tbl.Rows.Count <= 5)
                {
                    int ArrayLength = IsStandardParam ? tbl.Rows.Count : 5;
                    obj_numberProfile = new Param_Standard_Number_Profile[ArrayLength];

                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        if (!IsStandardParam && Convert.ToBoolean(tbl.Rows[i]["is_aynonymous"]))
                        {
                            #region If Anonymous Number
                            var objNum = GetDefaultNumberProfileObject();

                            objNum.Accept_Data_Call_FLAG_7 = false;
                            //============================================================================================
                            objNum.Reject_Call_FLAG_1 = false;
                            //============================================================================================
                            objNum.Accept_Paramaeters_In_Wake_Up_SMS_FLAG_4 = Convert.ToBoolean(tbl.Rows[i]["accept_params_in_wakeup_sms"]);
                            //============================================================================================
                            objNum.Allow_2way_SMS_communication_FLAG_6 = Convert.ToBoolean(tbl.Rows[i]["allow_2way_sms_communication"]);
                            //============================================================================================
                            objNum.Reject_With_Attend_FLAG_2 = Convert.ToBoolean(tbl.Rows[i]["reject_with_attend"]);
                            //============================================================================================
                            objNum.Unique_ID = 5;
                            //============================================================================================
                            objNum.Wakeup_On_SMS_FLAG_3 = Convert.ToBoolean(tbl.Rows[i]["wakeup_on_sms"]);
                            //============================================================================================
                            objNum.Wakup_On_Voice_Call_FLAG_5 = Convert.ToBoolean(tbl.Rows[i]["wakeup_on_voice_call"]);
                            //============================================================================================
                            objNum.Verify_Password_FLAG_0 = Convert.ToBoolean(tbl.Rows[i]["verify_password"]);
                            //============================================================================================
                            objNum.Wake_Up_On_SMS = Convert.ToByte(tbl.Rows[i]["wakeup_on_sms_id"]);
                            //============================================================================================
                            objNum.Wake_Up_On_Voice_Call = Convert.ToByte(tbl.Rows[i]["wakeup_on_voice_call_id"]);
                            //============================================================================================
                            obj_numberProfile[4] = objNum;

                            #endregion
                        }
                        else if (!IsStandardParam)
                        {
                            #region If Not Anonymous

                            var objNum = GetDefaultNumberProfileObject();
                            //============================================================================================
                            objNum.Accept_Data_Call_FLAG_7 = false;
                            //============================================================================================
                            objNum.Reject_Call_FLAG_1 = false;
                            //============================================================================================
                            objNum.Accept_Paramaeters_In_Wake_Up_SMS_FLAG_4 = Convert.ToBoolean(tbl.Rows[i]["accept_params_in_wakeup_sms"]);
                            //============================================================================================
                            objNum.Allow_2way_SMS_communication_FLAG_6 = Convert.ToBoolean(tbl.Rows[i]["allow_2way_sms_communication"]);
                            //============================================================================================
                            objNum.Reject_With_Attend_FLAG_2 = Convert.ToBoolean(tbl.Rows[i]["reject_with_attend"]);
                            //============================================================================================
                            objNum.Unique_ID = Convert.ToByte(tbl.Rows[i]["unique_id"]);
                            //============================================================================================
                            objNum.Wakeup_On_SMS_FLAG_3 = Convert.ToBoolean(tbl.Rows[i]["wakeup_on_sms"]);
                            //============================================================================================
                            objNum.Wakup_On_Voice_Call_FLAG_5 = Convert.ToBoolean(tbl.Rows[i]["wakeup_on_voice_call"]);
                            //============================================================================================
                            objNum.Verify_Password_FLAG_0 = Convert.ToBoolean(tbl.Rows[i]["verify_password"]);
                            //============================================================================================
                            objNum.Wake_Up_On_SMS = Convert.ToByte(tbl.Rows[i]["wakeup_on_sms_id"]);
                            //============================================================================================
                            objNum.Wake_Up_On_Voice_Call = Convert.ToByte(tbl.Rows[i]["wakeup_on_voice_call_id"]);
                            //============================================================================================
                            objNum.Number = Param_Number_ProfileHelper.ConvertFromValidNumberString(tbl.Rows[i]["number"].ToString(), IsStandardParam);
                            //============================================================================================    
                            obj_numberProfile[objNum.Unique_ID - 1] = objNum;

                            #endregion
                        }
                        else
                        {
                            obj_numberProfile[i] = new Param_Standard_Number_Profile() { Number = Param_Number_ProfileHelper.ConvertFromValidNumberString(tbl.Rows[i]["number"].ToString(), IsStandardParam) };
                        }
                    }
                    #region If User Define Less than 5 profiles

                    if (!IsStandardParam && tbl.Rows.Count < 5)// if Profiles from Db is Less then 5 we have to fill to five including anonymous
                    {
                        var iterate = (obj_numberProfile[4] == null) ? 4 : 3;
                        for (int i = 0; i <= iterate; i++)
                        {
                            if (iterate == 4 && obj_numberProfile[4] == null)
                            {
                                var obj = GetDefaultNumberProfileObject();
                                obj.Unique_ID = 5;
                                obj_numberProfile[4] = obj;

                            }
                            else
                            {
                                if (obj_numberProfile[i] == null)
                                {
                                    var obj = GetDefaultNumberProfileObject();
                                    obj.Unique_ID = 0;
                                    obj.Number = Param_Number_ProfileHelper.ConvertFromValidNumberString("923430000000", IsStandardParam);
                                    obj_numberProfile[i] = obj;

                                }
                            }
                        }

                    }
                    #endregion
                    return true;
                }
                else
                {
                    obj_numberProfile = new Param_Number_Profile[1] { null };
                    return false;
                }
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw new Exception("Error invalid Number Profile Parameters");
            }
            finally
            {
                _DBConnect.CloseConnection();
            }



        }
        /// <summary>
        /// Get Default Instance Of Number Profile Class
        /// </summary>
        /// <returns>Param_Number_Profile</returns>
        private Param_Number_Profile GetDefaultNumberProfileObject()
        {

            var clne = new Param_Number_Profile();
            clne.Unique_ID = 0;
            clne.Accept_Data_Call_FLAG_7 = false;
            clne.Accept_Paramaeters_In_Wake_Up_SMS_FLAG_4 = false;
            clne.Allow_2way_SMS_communication_FLAG_6 = true;
            clne.Reject_Call_FLAG_1 = false;
            clne.Reject_With_Attend_FLAG_2 = true;
            clne.Verify_Password_FLAG_0 = false;
            clne.Wakeup_On_SMS_FLAG_3 = false;
            clne.Wakup_On_Voice_Call_FLAG_5 = false;
            clne.Wake_Up_On_SMS = 1;
            clne.Wake_Up_On_Voice_Call = 1;
            clne.Number = new byte[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            clne.Datacall_Number = new byte[16] { 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15 };
            return clne;
        }
        /// <summary>
        /// Convert MSN string into Bytes of MSN For Number Profile Parameters
        /// </summary>
        /// <param name="phoneTxt"></param>
        /// <returns></returns>

        #endregion

        #region Wakeup Profile

        public bool InitWakeUpProfilesParams(int grpId, out Param_WakeUp_Profile[] obj_WakeUpProfile)
        {
            try
            {
                if (!_DBConnect.IsConnectionOpen)
                    _DBConnect.OpenConnection();
                OdbcCommand cmd = new OdbcCommand();
                cmd.Connection = _DBConnect.Connection;
                cmd.CommandText = "select * from `param_wakeup_profile` where `wakeup_group_id` = ?";
                cmd.Parameters.Add(new OdbcParameter("@id", grpId));

                var tbl = GetTableFromDB(cmd);

                if (tbl.Rows.Count > 0)
                {
                    if (tbl.Rows.Count <= 4)
                    {
                        obj_WakeUpProfile = new Param_WakeUp_Profile[4];
                        int counter = 0;
                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            var obj_Wakeup = new Param_WakeUp_Profile();
                            obj_Wakeup.Wake_Up_Profile_ID = Convert.ToByte(tbl.Rows[i]["wakeup_profile_id"]);
                            obj_Wakeup.IP_Profile_ID_1 = Convert.ToByte(tbl.Rows[i]["ip_profile_id_1"]);
                            obj_Wakeup.IP_Profile_ID_2 = Convert.ToByte(tbl.Rows[i]["ip_profile_id_2"]);
                            obj_Wakeup.IP_Profile_ID_3 = Convert.ToByte(tbl.Rows[i]["ip_profile_id_3"]);
                            obj_Wakeup.IP_Profile_ID_4 = Convert.ToByte(tbl.Rows[i]["ip_profile_id_4"]);
                            obj_WakeUpProfile[i] = obj_Wakeup;
                            counter++;
                        }

                        for (int i = counter; i < 4; i++)
                        {
                            var obj_Wakeup = new Param_WakeUp_Profile();
                            obj_Wakeup.Wake_Up_Profile_ID = 0;
                            obj_Wakeup.IP_Profile_ID_1 = 0;
                            obj_Wakeup.IP_Profile_ID_2 = 0;
                            obj_Wakeup.IP_Profile_ID_3 = 0;
                            obj_Wakeup.IP_Profile_ID_4 = 0;
                            obj_WakeUpProfile[i] = obj_Wakeup;
                        }

                        return true;
                    }
                    else
                    {
                        obj_WakeUpProfile = new Param_WakeUp_Profile[1] { null };
                        return false;
                    }
                }
                else
                {
                    throw new Exception("Wakeup Profile Parameters are not Found With Provided ID");
                }
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw new Exception("Error invalid WakeUp Profile Parameters");
            }
            finally
            {
                _DBConnect.CloseConnection();
            }
        }

        #endregion

        #region Parameterization Request

        public DataTable GetIPProfileFromDatabase(long IPProfileID)
        {
            try
            {
                DBConnect.OpenConnection();
                ParameterizationRequest temp = new ParameterizationRequest();
                string query = String.Format("SELECT * FROM ip_profile WHERE ip_profile_id = '{0}'", IPProfileID);
                OdbcCommand Command = new OdbcCommand(query, Connection);
                return GetTableFromDB(Command);
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw ex;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public DataTable GetModemInitializeFromDatabase(long ModemInitializeID)
        {
            try
            {
                DBConnect.OpenConnection();
                ParameterizationRequest temp = new ParameterizationRequest();
                string query = String.Format("SELECT * FROM modem_initialize WHERE modem_initialize_id = '{0}'", ModemInitializeID);
                OdbcCommand Command = new OdbcCommand(query, Connection);
                return GetTableFromDB(Command);
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw ex;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public DataTable GetKeepAliveFromDatabase(long KeepAliveID)
        {
            try
            {
                DBConnect.OpenConnection();
                ParameterizationRequest temp = new ParameterizationRequest();
                string query = String.Format("SELECT * FROM keep_alive WHERE keep_alive_id = '{0}'", KeepAliveID);
                OdbcCommand Command = new OdbcCommand(query, Connection);
                return GetTableFromDB(Command);
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw ex;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public DataTable GetMajorAlarmFromDatabase(long MajorAlarmID)
        {
            try
            {
                DBConnect.OpenConnection();
                ParameterizationRequest temp = new ParameterizationRequest();
                string query = String.Format("SELECT * FROM major_alarm_group WHERE major_alarm_group_id = '{0}'", MajorAlarmID);
                OdbcCommand Command = new OdbcCommand(query, Connection);
                return GetTableFromDB(Command);
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw ex;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public DataTable GetDisplayWindowsNormal(long ID)
        {
            try
            {
                DBConnect.OpenConnection();
                string query = String.Format("SELECT quantity_index,window_number FROM display_windows_details WHERE id = '{0}'", ID);
                OdbcCommand Command = new OdbcCommand(query, Connection);
                return GetTableFromDB(Command);
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw ex;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public DataTable GetDisplayWindowsAlternate(long ID)
        {
            try
            {
                DBConnect.OpenConnection();
                string query = String.Format("SELECT quantity_index,window_number FROM display_windows_details WHERE id = '{0}'", ID);
                OdbcCommand Command = new OdbcCommand(query, Connection);
                return GetTableFromDB(Command);
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw ex;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        #endregion

        #region Timebase Events

        public DataTable GetTBERequestDetailFromDatabases(long TBEDetailID)
        {
            try
            {
                DBConnect.OpenConnection();
                ParameterizationRequest temp = new ParameterizationRequest();
                string query = String.Format("SELECT * FROM time_base_events_detail WHERE tbe_detail_id = '{0}'", TBEDetailID);
                OdbcCommand Command = new OdbcCommand(query, Connection);
                return GetTableFromDB(Command);
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw ex;
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        #endregion

        #region Password

        public void Update_Password_Log(string msn, string current_password, string previous_pass)
        {
            try
            {
                if (!_DBConnect.IsConnectionOpen)
                    DBConnect.OpenConnection();

                string query = string.Format("update `meter_password` set `previous_password` ='{0}' , `updated_password` = '{1}', `last_update_time`='{2}'  where `msn` = '{3}'", previous_pass, current_password, DateTime.Now.ToString(DateFormat), msn);

                OdbcCommand Command = new OdbcCommand(query, Connection);
                Command.ExecuteNonQuery();
                DBConnect.CloseConnection();
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw new Exception("Invalid Query Check database");
            }
        }




        public void Insert_Password_Log(string msn, string current_password, string previous_pass)
        {
            try
            {
                if (!_DBConnect.IsConnectionOpen)
                    DBConnect.OpenConnection();

                string query = string.Format("INSERT INTO `meter_pasword`(`msn`, `previous_password`, `updated_password`, `last_update_time`) VALUES ('{0}','{1}','{2}','{3}')", msn, previous_pass, current_password, DateTime.Now.ToString(DateFormat));

                OdbcCommand Command = new OdbcCommand(query, Connection);
                Command.ExecuteNonQuery();
                DBConnect.CloseConnection();
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw new Exception("Invalid Query Check database");
            }
        }


        #endregion

        #region Display Power Down

        public bool InitDisplayPowerDownParams(long pramId, out Param_Display_PowerDown obj_Disp)
        {
            if (!_DBConnect.IsConnectionOpen)
                _DBConnect.OpenConnection();
            obj_Disp = new Param_Display_PowerDown();
            try
            {
                OdbcCommand cmd = new OdbcCommand();
                cmd.CommandText = "select *from `display_power_down_params` where display_parm_id = ?";
                cmd.Parameters.Add(new OdbcParameter("@id", pramId));
                cmd.Connection = _DBConnect.Connection;

                var tbl = new DataTable();
                using (var reader = cmd.ExecuteReader())
                {
                    tbl.Load(reader);

                    #region Onject Initialization
                    if (tbl.Rows.Count == 1)
                    {
                        //==================================================================================================
                        obj_Disp.IsAlwaysOn = Convert.ToBoolean(tbl.Rows[0]["always_on"]);
                        //==================================================================================================
                        obj_Disp.IsImmidateOff = Convert.ToBoolean(tbl.Rows[0]["immediately_off"]);
                        //==================================================================================================
                        obj_Disp.IsDisplayOnByButton = Convert.ToBoolean(tbl.Rows[0]["display_on_by_button"]);
                        //==================================================================================================
                        obj_Disp.IsDisplayRepeat = Convert.ToBoolean(tbl.Rows[0]["display_repeat"]);
                        //==================================================================================================
                        obj_Disp.IsOnTimeCycleScroll = Convert.ToBoolean(tbl.Rows[0]["on_time_mode_scroll_cycle"]);
                        //==================================================================================================
                        obj_Disp.IsOffMinute_Sec = false;
                        //==================================================================================================
                        obj_Disp.IsOnMinute_Sec = false;
                        //==================================================================================================
                        obj_Disp.OffDelay = Convert.ToByte(tbl.Rows[0]["off_delay_sec"]);
                        //==================================================================================================
                        obj_Disp.OnTime = Convert.ToByte(tbl.Rows[0]["on_time_sec"]);
                        //==================================================================================================
                        obj_Disp.OffTime = Convert.ToByte(tbl.Rows[0]["off_time_sec"]);
                        //================================================================================================== 
                    }
                    return true;

                    #endregion 
                }
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                throw;
            }
        }

        #endregion

        #region Limits

        public bool GetLimitsParams(long paramId, ref Param_Limits obj_Param)
        {
            try
            {
                if (!_DBConnect.IsConnectionOpen)
                    _DBConnect.OpenConnection();

                OdbcCommand cmd = new OdbcCommand();
                cmd.CommandText = "select * from limits_params where `param_id` = ?";
                cmd.Parameters.Add(new OdbcParameter("@id", paramId));
                cmd.Connection = _DBConnect.Connection;
                var tbl = GetTableFromDB(cmd);
                if (obj_Param == null) obj_Param = new Param_Limits();
                if (tbl.Rows.Count == 1)
                {
                    //============================================================================================
                    obj_Param.WriteOverVolt = Convert.ToBoolean(tbl.Rows[0]["w_over_volt"]);
                    obj_Param.OverVolt = Convert.ToDouble(tbl.Rows[0]["over_volt"]);
                    //============================================================================================
                    obj_Param.WriteUnderVolt = Convert.ToBoolean(tbl.Rows[0]["w_under_volt"]);
                    obj_Param.UnderVolt = Convert.ToDouble(tbl.Rows[0]["under_volt"]);
                    //============================================================================================
                    obj_Param.WriteImbalanceVolt = Convert.ToBoolean(tbl.Rows[0]["w_imbalance_volt"]);
                    obj_Param.ImbalanceVolt = Convert.ToDouble(tbl.Rows[0]["imbalance_volt"]);
                    //============================================================================================
                    obj_Param.WriteHighNeutralCurrent = Convert.ToBoolean(tbl.Rows[0]["w_high_neatral_current"]);
                    obj_Param.HighNeutralCurrent = Convert.ToDouble(tbl.Rows[0]["high_neatral_current"]);
                    //============================================================================================
                    obj_Param.WriteReverseEnergy = Convert.ToBoolean(tbl.Rows[0]["w_reverse_energy"]);
                    obj_Param.ReverseEnergy = Convert.ToDouble(tbl.Rows[0]["reverse_energy"]);
                    //============================================================================================
                    obj_Param.WriteTamperEnergy = Convert.ToBoolean(tbl.Rows[0]["w_temper_energy"]);
                    obj_Param.TamperEnergy = Convert.ToDouble(tbl.Rows[0]["temper_energy"]);
                    //============================================================================================
                    obj_Param.WriteCTFail_AMP = Convert.ToBoolean(tbl.Rows[0]["w_ct_fail_amp"]);
                    obj_Param.CTFail_AMP = Convert.ToDouble(tbl.Rows[0]["ct_fail_amp"]);
                    //============================================================================================
                    obj_Param.WritePTFail_AMP = Convert.ToBoolean(tbl.Rows[0]["w_pt_fail_amp"]);
                    obj_Param.PTFail_AMP = Convert.ToDouble(tbl.Rows[0]["pt_fail_amp"]);
                    //============================================================================================
                    obj_Param.WritePTFail_Volt = Convert.ToBoolean(tbl.Rows[0]["w_pt_fail_volt"]);
                    obj_Param.PTFail_Volt = Convert.ToDouble(tbl.Rows[0]["pt_fail_volt"]);
                    //============================================================================================
                    obj_Param.WriteOverCurrentByPhase_T1 = Convert.ToBoolean(tbl.Rows[0]["w_over_current_phase_t1"]);
                    obj_Param.OverCurrentByPhase_T1 = Convert.ToDouble(tbl.Rows[0]["over_current_phase_t1"]);
                    //============================================================================================
                    obj_Param.WriteOverCurrentByPhase_T2 = Convert.ToBoolean(tbl.Rows[0]["w_over_current_phase_t2"]);
                    obj_Param.OverCurrentByPhase_T2 = Convert.ToDouble(tbl.Rows[0]["over_current_phase_t2"]);
                    //============================================================================================
                    obj_Param.WriteOverCurrentByPhase_T3 = Convert.ToBoolean(tbl.Rows[0]["w_over_current_phase_t3"]);
                    obj_Param.OverCurrentByPhase_T3 = Convert.ToDouble(tbl.Rows[0]["over_current_phase_t3"]);
                    //============================================================================================
                    obj_Param.WriteOverCurrentByPhase_T4 = Convert.ToBoolean(tbl.Rows[0]["w_over_current_phase_t4"]);
                    obj_Param.OverCurrentByPhase_T4 = Convert.ToDouble(tbl.Rows[0]["over_current_phase_t4"]);
                    //============================================================================================
                    obj_Param.WriteOverLoadByPhase_T1 = Convert.ToBoolean(tbl.Rows[0]["w_over_load_phase_t1"]);
                    obj_Param.OverLoadByPhase_T1 = Convert.ToDouble(tbl.Rows[0]["over_load_phase_t1"]);
                    //============================================================================================
                    obj_Param.WriteOverLoadByPhase_T2 = Convert.ToBoolean(tbl.Rows[0]["w_over_load_phase_t2"]);
                    obj_Param.OverLoadByPhase_T2 = Convert.ToDouble(tbl.Rows[0]["over_load_phase_t2"]);
                    //============================================================================================
                    obj_Param.WriteOverLoadByPhase_T3 = Convert.ToBoolean(tbl.Rows[0]["w_over_load_phase_t3"]);
                    obj_Param.OverLoadByPhase_T3 = Convert.ToDouble(tbl.Rows[0]["over_load_phase_t3"]);
                    //============================================================================================
                    obj_Param.WriteOverLoadByPhase_T4 = Convert.ToBoolean(tbl.Rows[0]["w_over_load_phase_t4"]);
                    obj_Param.OverLoadByPhase_T4 = Convert.ToDouble(tbl.Rows[0]["over_load_phase_t4"]);
                    //============================================================================================
                    obj_Param.WriteDemandOverLoadTotal_T1 = Convert.ToBoolean(tbl.Rows[0]["w_mdi_exceed_t1"]);
                    obj_Param.DemandOverLoadTotal_T1 = Convert.ToDouble(tbl.Rows[0]["mdi_exceed_t1"]) * 1000;
                    //============================================================================================
                    obj_Param.WriteDemandOverLoadTotal_T2 = Convert.ToBoolean(tbl.Rows[0]["w_mdi_exceed_t2"]);
                    obj_Param.DemandOverLoadTotal_T2 = Convert.ToDouble(tbl.Rows[0]["mdi_exceed_t2"]) * 1000;
                    //============================================================================================
                    obj_Param.WriteDemandOverLoadTotal_T3 = Convert.ToBoolean(tbl.Rows[0]["w_mdi_exceed_t3"]);
                    obj_Param.DemandOverLoadTotal_T3 = Convert.ToDouble(tbl.Rows[0]["mdi_exceed_t3"]) * 1000;
                    //============================================================================================
                    obj_Param.WriteDemandOverLoadTotal_T4 = Convert.ToBoolean(tbl.Rows[0]["w_mdi_exceed_t4"]);
                    obj_Param.DemandOverLoadTotal_T4 = Convert.ToDouble(tbl.Rows[0]["mdi_exceed_t4"]) * 1000;
                    //============================================================================================
                    obj_Param.WriteOverLoadTotal_T1 = Convert.ToBoolean(tbl.Rows[0]["w_over_load_total_t1"]);
                    obj_Param.OverLoadTotal_T1 = Convert.ToDouble(tbl.Rows[0]["over_load_total_t1"]);
                    //============================================================================================
                    obj_Param.WriteOverLoadTotal_T2 = Convert.ToBoolean(tbl.Rows[0]["w_over_load_total_t2"]);
                    obj_Param.OverLoadTotal_T2 = Convert.ToDouble(tbl.Rows[0]["over_load_total_t2"]);
                    //============================================================================================
                    obj_Param.WriteOverLoadTotal_T3 = Convert.ToBoolean(tbl.Rows[0]["w_over_load_total_t3"]);
                    obj_Param.OverLoadTotal_T3 = Convert.ToDouble(tbl.Rows[0]["over_load_total_t3"]);
                    //============================================================================================
                    obj_Param.WriteOverLoadTotal_T4 = Convert.ToBoolean(tbl.Rows[0]["w_over_load_total_t4"]);
                    obj_Param.OverLoadTotal_T4 = Convert.ToDouble(tbl.Rows[0]["over_load_total_t4"]);
                    //============================================================================================

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }

        }

        #endregion

        #region Monitoring Time

        public bool GetMonitoringTimeParams(long paramId, ref Param_Monitoring_Time obj_Param)
        {
            try
            {
                if (!_DBConnect.IsConnectionOpen)
                    _DBConnect.OpenConnection();

                OdbcCommand cmd = new OdbcCommand();
                cmd.CommandText = "select * from monitoring_time_param where `param_id` = ?";
                cmd.Parameters.Add(new OdbcParameter("@id", paramId));
                cmd.Connection = _DBConnect.Connection;
                var tbl = GetTableFromDB(cmd);
                if (obj_Param == null) obj_Param = new Param_Monitoring_Time();
                if (tbl.Rows.Count == 1)
                {
                    //============================================================================================
                    obj_Param.PowerFail = TimeSpan.Parse(tbl.Rows[0]["power_fail"].ToString());
                    obj_Param.WritePowerFail = Convert.ToBoolean(tbl.Rows[0]["w_power_fail"]);
                    //============================================================================================
                    obj_Param.PhaseFail = TimeSpan.Parse(tbl.Rows[0]["phase_fail"].ToString());
                    obj_Param.WritePhaseFail = Convert.ToBoolean(tbl.Rows[0]["w_phase_fail"]);
                    //============================================================================================
                    obj_Param.OverVolt = TimeSpan.Parse(tbl.Rows[0]["over_volt"].ToString());
                    obj_Param.WriteOverVolt = Convert.ToBoolean(tbl.Rows[0]["w_over_volt"]);
                    //============================================================================================
                    obj_Param.HighNeutralCurrent = TimeSpan.Parse(tbl.Rows[0]["high_neutral_current"].ToString());
                    obj_Param.WriteHighNeutralCurrent = Convert.ToBoolean(tbl.Rows[0]["w_high_neutral_current"]);
                    //============================================================================================
                    obj_Param.ImbalanceVolt = TimeSpan.Parse(tbl.Rows[0]["imbalance_volt"].ToString());
                    obj_Param.WriteImbalanceVolt = Convert.ToBoolean(tbl.Rows[0]["w_imbalance_volt"]);
                    //============================================================================================
                    obj_Param.OverCurrent = TimeSpan.Parse(tbl.Rows[0]["over_current"].ToString());
                    obj_Param.WriteOverCurrent = Convert.ToBoolean(tbl.Rows[0]["w_over_current"]);
                    //============================================================================================
                    obj_Param.ReversePolarity = TimeSpan.Parse(tbl.Rows[0]["reverse_polarity"].ToString());
                    obj_Param.WriteReversePolarity = Convert.ToBoolean(tbl.Rows[0]["w_reverse_polarity"]);
                    //============================================================================================
                    obj_Param.HALLSensor = TimeSpan.Parse(tbl.Rows[0]["hall_sensor"].ToString());
                    obj_Param.WriteHALLSensor = Convert.ToBoolean(tbl.Rows[0]["w_hall_sensor"]);
                    //============================================================================================
                    obj_Param.UnderVolt = TimeSpan.Parse(tbl.Rows[0]["under_volt"].ToString());
                    obj_Param.WriteUnderVolt = Convert.ToBoolean(tbl.Rows[0]["w_under_volt"]);
                    //============================================================================================
                    obj_Param.ReverseEnergy = TimeSpan.Parse(tbl.Rows[0]["reverse_energy"].ToString());
                    obj_Param.WriteReverseEnergy = Convert.ToBoolean(tbl.Rows[0]["w_reverse_energy"]);
                    //============================================================================================
                    obj_Param.TamperEnergy = TimeSpan.Parse(tbl.Rows[0]["temper_energy"].ToString());
                    obj_Param.WriteTamperEnergy = Convert.ToBoolean(tbl.Rows[0]["w_temper_energy"]);
                    //============================================================================================
                    obj_Param.CTFail = TimeSpan.Parse(tbl.Rows[0]["ct_fail"].ToString());
                    obj_Param.WriteCTFail = Convert.ToBoolean(tbl.Rows[0]["w_ct_fail"]);
                    //============================================================================================
                    obj_Param.PTFail = TimeSpan.Parse(tbl.Rows[0]["pt_fail"].ToString());
                    obj_Param.WritePTFail = Convert.ToBoolean(tbl.Rows[0]["w_pt_fail"]);
                    //============================================================================================
                    obj_Param.PowerUPDelay = TimeSpan.Parse(tbl.Rows[0]["pud_to_monitor"].ToString());
                    obj_Param.WritePowerUPDelay = Convert.ToBoolean(tbl.Rows[0]["w_pud_to_monitor"]);
                    //============================================================================================
                    obj_Param.PowerUpDelayEnergyRecording = TimeSpan.Parse(tbl.Rows[0]["pud_energy_recording"].ToString());
                    obj_Param.WritePowerUpDelayEnergyRecording = Convert.ToBoolean(tbl.Rows[0]["w_pud_energy_recording"]);
                    //============================================================================================
                    obj_Param.PhaseSequence = TimeSpan.Parse(tbl.Rows[0]["phase_sequence"].ToString());
                    obj_Param.WritePhaseSequence = Convert.ToBoolean(tbl.Rows[0]["w_phase_sequence"]);
                    //============================================================================================
                    obj_Param.OverLoad = TimeSpan.Parse(tbl.Rows[0]["over_load"].ToString());
                    obj_Param.WriteOverLoad = Convert.ToBoolean(tbl.Rows[0]["w_over_load"]);
                    //============================================================================================

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }

        }

        #endregion

        #region CT PT Params

        public bool GetCTPTParams(long paramId, ref Param_CTPT_Ratio obj_param)
        {
            try
            {
                if (!_DBConnect.IsConnectionOpen)
                    _DBConnect.OpenConnection();

                OdbcCommand cmd = new OdbcCommand();
                cmd.CommandText = "select * from ct_pt_param where `param_id` = ?";
                cmd.Parameters.Add(new OdbcParameter("@id", paramId));
                cmd.Connection = _DBConnect.Connection;
                var tbl = GetTableFromDB(cmd);
                if (obj_param == null) obj_param = new Param_CTPT_Ratio();

                if (tbl.Rows.Count == 1)
                {
                    //============================================================================================
                    obj_param.CTratio_Numerator = Convert.ToUInt16(tbl.Rows[0]["ct_num"]);
                    //============================================================================================
                    obj_param.CTratio_Denominator = Convert.ToUInt16(tbl.Rows[0]["ct_denum"]);
                    //============================================================================================
                    obj_param.PTratio_Numerator = Convert.ToUInt16(tbl.Rows[0]["pt_num"]);
                    //============================================================================================
                    obj_param.PTratio_Denominator = Convert.ToUInt16(tbl.Rows[0]["pt_denum"]);
                    //============================================================================================

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }

        }

        #endregion

        #region Decimal Points Params

        public bool GetDecimalPointsParams(long paramId, ref Param_Decimal_Point obj_param)
        {
            try
            {
                if (!_DBConnect.IsConnectionOpen)
                    _DBConnect.OpenConnection();

                OdbcCommand cmd = new OdbcCommand();
                cmd.CommandText = "select * from decimal_points_param where `param_id` = ?";
                cmd.Parameters.Add(new OdbcParameter("@id", paramId));
                cmd.Connection = _DBConnect.Connection;
                var tbl = GetTableFromDB(cmd);
                if (obj_param == null) obj_param = new Param_Decimal_Point();

                if (tbl.Rows.Count == 1)
                {
                    //============================================================================================
                    obj_param.Billing_Energy = Commons.DecimalPoint_validation(tbl.Rows[0]["billing_energy"].ToString(), 4, 3, 7);
                    //============================================================================================
                    obj_param.Billing_MDI = Commons.DecimalPoint_validation(tbl.Rows[0]["billing_mdi"].ToString(), 4, 3, 7);
                    //============================================================================================
                    obj_param.Instataneous_Voltage = Commons.DecimalPoint_validation(tbl.Rows[0]["instantaneous_voltage"].ToString(), 3, 2, 5);
                    //============================================================================================
                    obj_param.Instataneous_Power = Commons.DecimalPoint_validation(tbl.Rows[0]["instantaneous_pawor"].ToString(), 2, 3, 5);
                    //============================================================================================
                    obj_param.Instataneous_Current = Commons.DecimalPoint_validation(tbl.Rows[0]["instantaneous_current"].ToString(), 3, 2, 5);
                    //============================================================================================
                    obj_param.Instataneous_MDI = Commons.DecimalPoint_validation(tbl.Rows[0]["instantaneous_mdi"].ToString(), 2, 3, 5);
                    //============================================================================================

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }

        }

        #endregion

        #region Energy Params

        public bool GetEnergyParams(long paramId, ref Param_Energy_Parameter obj_param)
        {
            try
            {
                if (!_DBConnect.IsConnectionOpen)
                    _DBConnect.OpenConnection();

                OdbcCommand cmd = new OdbcCommand();
                cmd.CommandText = "select * from energy_param where `param_id` = ?";
                cmd.Parameters.Add(new OdbcParameter("@id", paramId));
                cmd.Connection = _DBConnect.Connection;
                var tbl = GetTableFromDB(cmd);
                if (obj_param == null) obj_param = new Param_Energy_Parameter();

                if (tbl.Rows.Count == 1)
                {
                    //============================================================================================
                    obj_param.Quad1 = Convert.ToBoolean(tbl.Rows[0]["quadrent_1"]);
                    //============================================================================================
                    obj_param.Quad2 = Convert.ToBoolean(tbl.Rows[0]["quadrent_2"]);
                    //============================================================================================
                    obj_param.Quad3 = Convert.ToBoolean(tbl.Rows[0]["quadrent_3"]);
                    //============================================================================================
                    obj_param.Quad4 = Convert.ToBoolean(tbl.Rows[0]["quadrent_4"]);
                    //============================================================================================


                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }

        }

        #endregion

        #region Exception Logging Event

        // define event that could help to log database Exception
        event DbExceptionOccur _newException;

        public event DbExceptionOccur NewDbException
        {
            add
            {
                lock (this)
                {
                    bool isAlreadyAdded = false;
                    if (_newException != null)
                    {
                        var subscriber = _newException.GetInvocationList();
                        foreach (var item in subscriber)
                        {
                            if (item.Target == value.Target)
                            {
                                isAlreadyAdded = true;
                                break;
                            }
                        }
                    }
                    if (!isAlreadyAdded) _newException += value;
                }

            }
            remove
            {
                lock (this)
                {
                    bool isAlreadyAdded = false;
                    if (_newException != null)
                    {
                        var subscriber = _newException.GetInvocationList();
                        foreach (var item in subscriber)
                        {
                            if (item.Target == value.Target)
                            {
                                isAlreadyAdded = true;
                                break;
                            }
                        }
                        if (isAlreadyAdded) _newException -= value;
                    }
                }
            }
        }

        private void InvokeException(Exception ex, string methodName)
        {
            methodName = "While Executing: *" + methodName + "* " + ex.Message;
            if (_newException != null) _newException(new Exception(methodName));
        }

        #endregion

        #region HLS Implementation

        // public bool GetMeterHLSSetting(long hlsId, ref Param_Energy_Parameter obj_param)
        // {
        //     try
        //     {
        //         if (!_DBConnect.IsConnectionOpen)
        //             _DBConnect.OpenConnection();

        //         OdbcCommand cmd = new OdbcCommand();
        //         cmd.CommandText = "select * from energy_param where `param_id` = ?";
        //         cmd.Parameters.Add(new OdbcParameter("@id", paramId));
        //         cmd.Connection = _DBConnect.Connection;
        //         var tbl = GetTableFromDB(cmd);
        //         if (obj_param == null) obj_param = new Param_Energy_Parameter();

        //         if (tbl.Rows.Count == 1)
        //         {
        //             //============================================================================================
        //             obj_param.Quad1 = Convert.ToBoolean(tbl.Rows[0]["quadrent_1"]);
        //             //============================================================================================
        //             obj_param.Quad2 = Convert.ToBoolean(tbl.Rows[0]["quadrent_2"]);
        //             //============================================================================================
        //             obj_param.Quad3 = Convert.ToBoolean(tbl.Rows[0]["quadrent_3"]);
        //             //============================================================================================
        //             obj_param.Quad4 = Convert.ToBoolean(tbl.Rows[0]["quadrent_4"]);
        //             //============================================================================================


        //             return true;
        //         }

        //         return false;
        //     }
        //     catch (Exception ex)
        //     {
        //         if (_newException != null) _newException(ex);
        //         return false;
        //     }

        // }

        #endregion

        #region Load Shedding

        // Param_Load_Scheduling

        public bool GetLoadSheddingParams(long scheduleId, ref Param_Load_Scheduling obj_param)
        {
            bool entryFound = false;
            try
            {
                if (!_DBConnect.IsConnectionOpen)
                    _DBConnect.OpenConnection();

                // load_shedding_schedule
                // load_shedding_detail

                OdbcCommand cmd = new OdbcCommand();
                cmd.CommandText = "SELECT activation_date, expiry_date, interval_id, start_time, end_time FROM load_shedding_schedule lss inner join load_shedding_detail lsd on lss.schedule_id = lsd.schedule_id where lss.schedule_id = ?";
                cmd.Parameters.Add(new OdbcParameter("@id", scheduleId));
                cmd.Connection = _DBConnect.Connection;
                var tbl = GetTableFromDB(cmd);

                if (obj_param == null)
                    obj_param = new Param_Load_Scheduling();

                if (tbl.Rows.Count > 0)
                {
                    entryFound = true;

                    obj_param.StartDate = Convert.ToDateTime(tbl.Rows[0]["activation_date"]);
                    obj_param.EndDate = Convert.ToDateTime(tbl.Rows[0]["expiry_date"]);

                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        LoadScheduling entry = new LoadScheduling();

                        //============================================================================================
                        entry.StartTime = TimeSpan.Parse(tbl.Rows[i]["start_time"].ToString());
                        //============================================================================================
                        entry.Interval = Convert.ToInt32((TimeSpan.Parse(tbl.Rows[i]["end_time"].ToString()).Subtract(entry.StartTime)).TotalMinutes);
                        //============================================================================================

                        int overlapAt = obj_param.Add(entry);     // also checks overlapping

                        if (overlapAt != -1) // overlapping with previous record
                        {
                            throw new InvalidConstraintException(string.Format("Inconsistent Load Shedding Entry with row No. {0}", overlapAt));
                        }
                        // else if (overlapAt == -1) then 'entry' added successfully...

                    }
                }

                return entryFound;
            }
            catch (Exception ex)
            {
                if (_newException != null) _newException(ex);
                return false;
            }

        }

        #endregion

        #region Consumption Data

        public bool GetConsumptionDataNow(int feederId, ref ConsumptionDataNow obj_param)
        {
            ConsumptionData consumptionData = null;

            try
            {
                if (!_DBConnect.IsConnectionOpen)
                    _DBConnect.OpenConnection();

                OdbcCommand cmd = new OdbcCommand();
                cmd.CommandText = $"SELECT today_kwh_consumption, this_month_kwh_consumption, " +
                    $"(active_pwr_pos_phase_total + active_pwr_neg_phase_total) as present_load,  " +
                    $"consumption_modified_time , session_date_time, c.cost_per_kwh " +
                    $"FROM instantaneous_data_live idl " +
                    $"INNER JOIN meter m ON idl.msn = m.msn " +
                    $"INNER JOIN connection con ON m.feeder_id = con.feeder_id " +
                    $"INNER JOIN consumers c ON con.customer_id = c.consumer_id " +
                    $"WHERE m.feeder_id = '{feederId}'";

                //cmd.Parameters.Add(new OdbcParameter("@msn", msn)ar });
                //cmd.Parameters.Add(new OdbcParameter("@consumer_id", consumerId)t });

                cmd.Connection = _DBConnect.Connection;
                var tbl = GetTableFromDB(cmd);

                if (obj_param == null)
                    obj_param = new ConsumptionDataNow();
                else
                    obj_param.ReInit();

                if (tbl.Rows.Count == 0)
                {
                    throw new Exception($"No Parent_id Found in meter table! for feeder_id = {feederId}");
                }

                consumptionData = new ConsumptionData();

                if (Commons.IsCellNull(tbl.Rows[0]["consumption_modified_time"]))
                {
                    throw new ArgumentNullException("Error in typecast 'consumption_modified_time' ");
                }
                consumptionData.DateTime = Convert.ToDateTime(tbl.Rows[0]["consumption_modified_time"]);

                double val = 0;
                if (Commons.IsCellNull(tbl.Rows[0]["today_kwh_consumption"]))
                {
                    throw new ArgumentNullException("Error in typecast 'today_kwh_consumption' ");
                }
                val = Convert.ToDouble(tbl.Rows[0]["today_kwh_consumption"]);

                if (Commons.IsCellNull(tbl.Rows[0]["cost_per_kwh"]))
                {
                    throw new ArgumentNullException("Error in typecast 'cost_per_kwh' ");
                }
                double costPerKwh = Convert.ToDouble(tbl.Rows[0]["cost_per_kwh"]);

                consumptionData.Energy = (int)Math.Round(val);
                consumptionData.Price = (int)Math.Round(consumptionData.Energy * costPerKwh);

#if LCD_SEGMENT_TEST
                //consumptionData.Energy = 12345;
                //consumptionData.Price = 123456789;
#endif

                //consumptionData.Energy = Convert.ToInt32(tbl.Rows[0]["today_kwh_consumption"]);
                //costPerKwh = Convert.ToDouble(tbl.Rows[0]["cost_per_kwh"]);
                bool isAdded = obj_param.AddInArray(consumptionData);
                if (!isAdded)
                {
                    throw new Exception("Error in Adding Object in Consumption Data Now Array");
                }

                if (Commons.IsCellNull(tbl.Rows[0]["present_load"]))
                {
                    throw new ArgumentNullException("Error in typecast 'present_load' ");
                }
                val = Convert.ToDouble(tbl.Rows[0]["present_load"]);

                if (Commons.IsCellNull(tbl.Rows[0]["session_date_time"]))
                {
                    throw new ArgumentNullException("Error in typecast 'session_date_time' ");
                }
                consumptionData.DateTime = Convert.ToDateTime(tbl.Rows[0]["session_date_time"]);
                consumptionData.Energy = (int)Math.Round(val);
                consumptionData.Price = (int)Math.Round(consumptionData.Energy * costPerKwh);

#if LCD_SEGMENT_TEST
                //consumptionData.Energy = 12345;
                //consumptionData.Price = 123456789;
#endif
                isAdded = obj_param.AddInArray(consumptionData);
                if (!isAdded)
                {
                    throw new Exception("Error in Adding Object in Consumption Data Now Array");
                }

                if (Commons.IsCellNull(tbl.Rows[0]["this_month_kwh_consumption"]))
                {
                    throw new ArgumentNullException("Error in typecast 'this_month_kwh_consumption' ");
                }
                val = Convert.ToDouble(tbl.Rows[0]["this_month_kwh_consumption"]);
                consumptionData.Energy = (int)Math.Round(val);
                consumptionData.Price = (int)Math.Round(consumptionData.Energy * costPerKwh);

#if LCD_SEGMENT_TEST
                //consumptionData.Energy = 12345;
                //consumptionData.Price = 123456789;
#endif
                //consumptionData.Energy = Convert.ToInt32(tbl.Rows[0]["this_month_kwh_consumption"]);
                isAdded = obj_param.AddInArray(consumptionData);
                if (!isAdded)
                {
                    throw new Exception("Error in Adding Object in Consumption Data Now Array");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //return false;
        }


        public bool SaveConsumptionDataSentNow_Log(ConsumptionDataNow obj_param, DateTime session_datetime, string customer_id, string msn)
        {
            try
            {
                if (!_DBConnect.IsConnectionOpen)
                    _DBConnect.OpenConnection();

                OdbcCommand cmd = new OdbcCommand();

                cmd.CommandText += $"INSERT INTO consumption_sent_log ( " +
                                   $"session_date_time, date, time, msn, customer_id, " +
                                   $"load_now, cost_now, datetime_now, " +
                                   $"today_load, today_cost, today_datetime, " +
                                   $"this_month_load, this_month_cost, this_month_datetime " +
                                   $") VALUES (" +
                                   $"'{session_datetime.ToString(DateFormat)}', CURRENT_DATE(), CURRENT_TIME(), '{msn}', '{customer_id}', " +
                                   $"{obj_param.consumptionDataNowArr[0].Energy}, {obj_param.consumptionDataNowArr[0].Price}, '{obj_param.consumptionDataNowArr[0].DateTime.ToString(DateFormat)}', " +
                                   $"{obj_param.consumptionDataNowArr[1].Energy}, {obj_param.consumptionDataNowArr[1].Price}, '{obj_param.consumptionDataNowArr[1].DateTime.ToString(DateFormat)}', " +
                                   $"{obj_param.consumptionDataNowArr[2].Energy}, {obj_param.consumptionDataNowArr[2].Price}, '{obj_param.consumptionDataNowArr[2].DateTime.ToString(DateFormat)}' " +
                                   $")";

                cmd.Connection = _DBConnect.Connection;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GetConsumptionDataWeekly(int feederId, ref ConsumptionDataWeekly obj_param)
        {
            Exception innerEx = null;

            try
            {
                DateTime date = DateTime.Now;
                int maxEntries = 7; // $maxEntries = 7;
                int i = 0;  //$i = 0;

                if (!_DBConnect.IsConnectionOpen)
                    _DBConnect.OpenConnection();

                OdbcCommand cmd = new OdbcCommand();
                cmd.CommandText = $"SELECT active_energy_tl, billing_date, (ct * pt) AS mf, c.cost_per_kwh " +
                                  $"FROM daily_billing_data_manual dbdm " +
                                  $"INNER JOIN meter m ON dbdm.msn = m.msn " +
                                  $"INNER JOIN connection con ON m.feeder_id = con.feeder_id " +
                                  $"INNER JOIN consumers c ON con.customer_id = c.consumer_id " +
                                  $"WHERE m.feeder_id = '{feederId}' " +
                                  $"AND billing_date >= now() - interval 8 day " +
                                  $"AND billing_date <= now() " +
                                  $"ORDER BY billing_date DESC LIMIT 8;";

                cmd.Connection = _DBConnect.Connection;
                var tbl = GetTableFromDB(cmd);

                if (obj_param == null)
                    obj_param = new ConsumptionDataWeekly();
                else
                    obj_param.ReInit();

                if (tbl.Rows.Count > 0)
                {
                    int entriesCount = 0;

                    if (Commons.IsCellNull(tbl.Rows[0]["cost_per_kwh"]))
                    {
                        throw new ArgumentNullException("Error in typecast 'cost_per_kwh' ");
                    }
                    double costPerKwh = Convert.ToDouble(tbl.Rows[i]["cost_per_kwh"]);

                    int RowCount = (tbl.Rows.Count - 1);
                    for (i = 0; i < RowCount; i++)
                    {
                        if (Commons.IsCellNull(tbl.Rows[0]["billing_date"]))
                        {
                            throw new ArgumentNullException("Error in typecast 'billing_date' ");
                        }
                        DateTime recordDate = Convert.ToDateTime(tbl.Rows[i]["billing_date"]);

                        if (Commons.IsCellNull(Convert.ToDouble(tbl.Rows[i]["active_energy_tl"])) || Commons.IsCellNull(tbl.Rows[i + 1]["active_energy_tl"]))
                        {
                            throw new ArgumentNullException("Error in typecast 'active_energy_tl' ");
                        }
                        double deltaValue = (Convert.ToDouble(tbl.Rows[i]["active_energy_tl"]) - Convert.ToDouble(tbl.Rows[i + 1]["active_energy_tl"]));

                        if (Commons.IsCellNull(tbl.Rows[i]["mf"]))
                        {
                            throw new ArgumentNullException("Error in typecast 'mf' ");
                        }
                        deltaValue = (deltaValue * Convert.ToDouble(tbl.Rows[i]["mf"]));

                        if (deltaValue < 0)
                            deltaValue = 0;

                        // while loop will add zero consumption for missing readings for any day
                        #region Add Missing Entries From the END
                        while (!(recordDate.Date.ToString() == date.Date.ToString()))
                        {
                            obj_param.consumptionDataWeeklyArr.Add(new ConsumptionData(date, 0, 0));
                            date = date.AddDays(-1);
                            entriesCount++; //$entriesCount++;
                            if (entriesCount >= maxEntries)
                                break;
                        }
                        #endregion

                        if (entriesCount >= maxEntries)
                            break;


                        obj_param.consumptionDataWeeklyArr.Add(new ConsumptionData(date, (int)deltaValue, (int)(deltaValue * costPerKwh))); // array_push(this->active_energy_weekly_history, deltaValue);

                        date = date.AddDays(-1);

                        entriesCount++;
                    }

                    //Added zeros to make full week other wise week disturb;
                    while (entriesCount < 7)
                    {
                        obj_param.consumptionDataWeeklyArr.Add(new ConsumptionData(date, 0, 0)); //array_push(this->active_energy_weekly_history, "0");
                        date = date.AddDays(-1);
                        entriesCount++;
                        if (entriesCount >= maxEntries)
                            break;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                if (innerEx != null)
                    throw innerEx;

                throw ex;
            }
            finally { }

            return false;

        }

        public bool SaveConsumptionDataWeekly_Log(ConsumptionDataWeekly obj_param, DateTime datetime, string msn, string customer_id)
        {
            try
            {
                if (!_DBConnect.IsConnectionOpen)
                    _DBConnect.OpenConnection();

                OdbcCommand cmd = new OdbcCommand();

                cmd.CommandText += $"INSERT INTO weekly_data_sent_log ( " +
                                   $"session_date_time, date, time, msn, customer_id, " +
                                   $"day1_date, day1_consumption, day1_cost, " +
                                   $"day2_date, day2_consumption, day2_cost, " +
                                   $"day3_date, day3_consumption, day3_cost, " +
                                   $"day4_date, day4_consumption, day4_cost, " +
                                   $"day5_date, day5_consumption, day5_cost, " +
                                   $"day6_date, day6_consumption, day6_cost, " +
                                   $"day7_date, day7_consumption, day7_cost " +
                                   $") VALUES (" +
                                   $"'{datetime.ToString(DateFormat)}', CURRENT_DATE(), CURRENT_TIME(), '{msn}', '{customer_id}', " +
                                   $"'{obj_param.consumptionDataWeeklyArr[0].DateTime.ToString(DateOnlyFormat)}', {obj_param.consumptionDataWeeklyArr[0].Energy}, {obj_param.consumptionDataWeeklyArr[0].Price}, " +
                                   $"'{obj_param.consumptionDataWeeklyArr[1].DateTime.ToString(DateOnlyFormat)}', {obj_param.consumptionDataWeeklyArr[1].Energy}, {obj_param.consumptionDataWeeklyArr[1].Price}, " +
                                   $"'{obj_param.consumptionDataWeeklyArr[2].DateTime.ToString(DateOnlyFormat)}', {obj_param.consumptionDataWeeklyArr[2].Energy}, {obj_param.consumptionDataWeeklyArr[2].Price}, " +
                                   $"'{obj_param.consumptionDataWeeklyArr[3].DateTime.ToString(DateOnlyFormat)}', {obj_param.consumptionDataWeeklyArr[3].Energy}, {obj_param.consumptionDataWeeklyArr[3].Price}, " +
                                   $"'{obj_param.consumptionDataWeeklyArr[4].DateTime.ToString(DateOnlyFormat)}', {obj_param.consumptionDataWeeklyArr[4].Energy}, {obj_param.consumptionDataWeeklyArr[4].Price}, " +
                                   $"'{obj_param.consumptionDataWeeklyArr[5].DateTime.ToString(DateOnlyFormat)}', {obj_param.consumptionDataWeeklyArr[5].Energy}, {obj_param.consumptionDataWeeklyArr[5].Price}, " +
                                   $"'{obj_param.consumptionDataWeeklyArr[6].DateTime.ToString(DateOnlyFormat)}', {obj_param.consumptionDataWeeklyArr[6].Energy}, {obj_param.consumptionDataWeeklyArr[6].Price} " +
                                   $")";

                cmd.Connection = _DBConnect.Connection;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GetConsumptionDataMonthly(int feederId, ref ConsumptionDataMonthly obj_param, string msn)
        {
            Exception innerEx = null;

            try
            {
                DateTime date = DateTime.Now;
                DateTime recordDate = date;
                double deltaValue = 0, mf = 0, costPerKwh = 0;
                int maxEntries = 6;
                //bool IsReadCurrentMonthFromDB = false;

                if (obj_param == null)
                    obj_param = new ConsumptionDataMonthly();
                else
                    obj_param.ReInit();

#if LCD_SEGMENT_TEST
                obj_param.consumptionDataMonthlyArr.Add(new ConsumptionData(recordDate, 123, 1230)); //01
                recordDate = recordDate.AddMonths(-1);
                obj_param.consumptionDataMonthlyArr.Add(new ConsumptionData(recordDate, 234, 2340)); //12
                recordDate = recordDate.AddMonths(-1);
                obj_param.consumptionDataMonthlyArr.Add(new ConsumptionData(recordDate, 345, 3450)); //11
                recordDate = recordDate.AddMonths(-1);
                obj_param.consumptionDataMonthlyArr.Add(new ConsumptionData(recordDate, 456, 4560)); //10
                recordDate = recordDate.AddMonths(-1);
                obj_param.consumptionDataMonthlyArr.Add(new ConsumptionData(recordDate, 567, 5670)); //09
                recordDate = recordDate.AddMonths(-1);
                obj_param.consumptionDataMonthlyArr.Add(new ConsumptionData(recordDate, 678, 6780)); //08
                return true;
#endif

                if (!_DBConnect.IsConnectionOpen)
                    _DBConnect.OpenConnection();


                OdbcCommand cmd = new OdbcCommand();
                cmd.CommandText = $"SELECT (active_energy_tl - previous_active_energy_tl) as active_energy_tl , billing_date, (ct * pt) AS mf, c.cost_per_kwh " +
                                  $"FROM monthly_billing_data_manual dbdm " +
                                  $"INNER JOIN meter m ON dbdm.msn = m.msn " +
                                  $"INNER JOIN connection con ON m.feeder_id = con.feeder_id " +
                                  $"INNER JOIN consumers c ON con.customer_id = c.consumer_id " +
                                  $"WHERE m.feeder_id = '{feederId}' and billing_date > c.initial_billing_date "  + //interval 24 hour " +
                                  $"ORDER BY billing_date DESC LIMIT 6;";

                DateTime ExpectedDate = date;
                DateTime minDate = date.AddMonths(-5);

                cmd.Connection = _DBConnect.Connection;
                var tbl = GetTableFromDB(cmd);

                if (tbl.Rows.Count > 0)
                {
                    int RowCount = (tbl.Rows.Count);
                    for (int i = 0; i < RowCount; i++)
                    {
                        if (Commons.IsCellNull(tbl.Rows[i]["mf"]))
                            throw new ArgumentNullException("Error in typecast 'mf' ");

                        if (Commons.IsCellNull(tbl.Rows[i]["active_energy_tl"]))
                            throw new ArgumentNullException("Error in typecast 'active_energy_tl' ");

                        if (Commons.IsCellNull(tbl.Rows[i]["billing_date"]))
                            throw new ArgumentNullException("Error in typecast 'billing_date' ");

                        if (Commons.IsCellNull(tbl.Rows[i]["cost_per_kwh"]))
                            throw new ArgumentNullException("Error in typecast 'cost_per_kwh' ");

                        mf = Convert.ToDouble(tbl.Rows[i]["mf"]);
                        deltaValue = Convert.ToDouble(tbl.Rows[i]["active_energy_tl"]) * mf;
                        recordDate = Convert.ToDateTime(tbl.Rows[i]["billing_date"]);
                        costPerKwh = Convert.ToDouble(tbl.Rows[i]["cost_per_kwh"]);

                        if (recordDate.Month == ExpectedDate.Month && recordDate.Year == ExpectedDate.Year)
                        {
                            obj_param.consumptionDataMonthlyArr.Add(new ConsumptionData(recordDate, (int)deltaValue, (int)(deltaValue * costPerKwh)));
                        }
                        else if (recordDate.Year >= minDate.Year &&
                            (recordDate.Month >= minDate.Month || ExpectedDate.Month > recordDate.Month))
                        {
                            AddMissingMonths(ref obj_param, ref ExpectedDate, ref date, ref recordDate);
                            obj_param.consumptionDataMonthlyArr.Add(new ConsumptionData(recordDate, (int)deltaValue, (int)(deltaValue * costPerKwh)));
                        }
                        else
                        {
                            AddMissingMonths(ref obj_param, ref ExpectedDate, ref date, ref recordDate);
                        }

                        ExpectedDate = ExpectedDate.AddMonths(-1);

                        if (obj_param.consumptionDataMonthlyArr.Count >= maxEntries)
                            break;
                    }

                    ConsumptionData cd = new ConsumptionData();
                    GetConsumptionDataMonthly_CurrentMonth(ref cd, date, msn, costPerKwh);
                    if (obj_param.consumptionDataMonthlyArr[0].DateTime.Year == date.Year && obj_param.consumptionDataMonthlyArr[0].DateTime.Month == date.Month)
                        obj_param.consumptionDataMonthlyArr[0] = cd;
                    else
                        obj_param.consumptionDataMonthlyArr.Insert(0, cd);

                    minDate = minDate.AddMonths(-1); //For Adding the most previous month if missing
                    AddMissingMonths(ref obj_param, ref ExpectedDate, ref date, ref minDate);
                    int totalRecords = obj_param.consumptionDataMonthlyArr.Count;

                    while (totalRecords > maxEntries)
                    {
                        obj_param.consumptionDataMonthlyArr.RemoveAt(totalRecords - 1);
                        totalRecords = obj_param.consumptionDataMonthlyArr.Count;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                if (innerEx != null)
                    throw innerEx;

                throw ex;
            }
            finally { }
        }

        private bool GetConsumptionDataMonthly_CurrentMonth(ref ConsumptionData consumptionData, DateTime currentDate, string msn, double cost_per_kwh)
        {
            DateTime recordDate = DateTime.MinValue;
            double deltaValue = 0, mf = 0;

            if (consumptionData == null)
                consumptionData = new ConsumptionData(currentDate, 0, 0);

            try
            {
                using (OdbcCommand cmd = new OdbcCommand())
                {
                    cmd.CommandText = $"SELECT this_month_kwh_consumption, consumption_modified_time, mf " +
                          $"FROM instantaneous_data_live " +
                          $"WHERE msn = {msn} " +
                          $"ORDER BY consumption_modified_time DESC;";

                    cmd.Connection = _DBConnect.Connection;
                    var tbl_Cur = GetTableFromDB(cmd);

                    if (tbl_Cur.Rows.Count > 0)
                    {
                        if (Commons.IsCellNull(tbl_Cur.Rows[0]["consumption_modified_time"]))
                            throw new ArgumentNullException("Error in typecast 'consumption_modified_time' ");

                        recordDate = Convert.ToDateTime(tbl_Cur.Rows[0]["consumption_modified_time"]);

                        if (recordDate.Month == currentDate.Month && recordDate.Year == currentDate.Year)
                        {
                            if (Commons.IsCellNull(tbl_Cur.Rows[0]["this_month_kwh_consumption"]))
                                throw new ArgumentNullException("Error in typecast 'this_month_kwh_consumption' ");

                            if (Commons.IsCellNull(tbl_Cur.Rows[0]["mf"]))
                                throw new ArgumentNullException("Error in typecast 'mf' ");

                            mf = (int)(Convert.ToDouble(tbl_Cur.Rows[0]["mf"]));
                            deltaValue = (int)(Convert.ToDouble(tbl_Cur.Rows[0]["this_month_kwh_consumption"]));
                            deltaValue = deltaValue * mf;
                            consumptionData = new ConsumptionData(recordDate, (int)deltaValue, (int)(deltaValue * cost_per_kwh));
                        }
                        else //Add 0 values in current month (dummy)
                        {
                            consumptionData = new ConsumptionData(currentDate, 0, 0);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        private bool AddMissingMonths(ref ConsumptionDataMonthly obj_param, ref DateTime ExpectedDate, ref DateTime date, ref DateTime recordDate)
        {
            do
            {
                obj_param.consumptionDataMonthlyArr.Add(new ConsumptionData(ExpectedDate, 0, 0));
                ExpectedDate = ExpectedDate.AddMonths(-1);
            }
            while (ExpectedDate.Month > recordDate.Month && ExpectedDate.Year >= recordDate.Year);

            return true;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                if (DBConnect != null)
                {
                    try
                    {
                        DBConnect.Dispose();
                        DBConnect = null;
                    }
                    catch (Exception)
                    { }
                }

            }
            catch (Exception) { }
        }

        #endregion

        ~DatabaseController()
        {
            try
            {
                Dispose();
            }
            catch (Exception)
            {
            }
        }
    }
}




