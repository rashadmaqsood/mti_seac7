using SharedCode.Comm.Param;
using System;
using System.Linq;

namespace SharedCode.Comm.DataContainer
{
    public class MeterReliablityParams
    {
        #region Data Members

        #region Limits
        public double LM_temperEnergy_kW;
        public double LM_Ct_Fail_amp;
        public double LM_Pt_Fail_amp;
        public double LM_Pt_Fail_Volt;
        public double LM_over_current_phase_t1;
        public double LM_over_current_phase_t2;
        public double LM_over_current_phase_t3;
        public double LM_over_current_phase_t4;
        public double LM_mdi_exceed_t1;
        public double LM_mdi_exceed_t2;
        public double LM_mdi_exceed_t3;
        public double LM_mdi_exceed_t4;
        public double LM_Over_Load_phase_t1;
        public double LM_Over_Load_phase_t2;
        public double LM_Over_Load_phase_t3;
        public double LM_Over_Load_phase_t4;
        public double LM_Over_Load_Total_t1;
        public double LM_Over_Load_Total_t2;
        public double LM_Over_Load_Total_t3;
        public double LM_Over_Load_Total_t4;

        #endregion
        #region Monitoring Time
        public TimeSpan MT_Over_Current;
        public TimeSpan MT_Over_Load;

        #endregion
        #region MDI Parameters
        public bool MDI_Auto_Reset_Enable;
        public string MDI_Auto_Reset_Date;
        #endregion
        #region IP Profiles
        public string ip_port_1 = "0";
        public string ip_port_2 = "0";
        public string ip_port_3 = "0";
        public string ip_port_4 = "0";
        #endregion
        #region Number Profile
        public string number_profile_1="0";
        public string number_profile_2="0";
        public string number_profile_3="0";
        public string number_profile_4="0";
        #endregion
        #region Keep Alive
        public bool KA_enable;
        public byte KA_wakeup_id;
        public ushort KA_ping_time_sec;
        #endregion
        #region Modem limits And time
        public byte MTL_Retries_ip;
        public ushort MTL_tcp_Inactivity;
        public ushort MTL_tcp_sip_send;
        #endregion
        #region Modem Initialization
        public string MI_APN;
        public byte MT_Decrement_Counter;
        #endregion
        #region Time Base Event
        public string TBE1;
        public string TBE2;
        public byte TBE1_Disable;
        public byte TBE2_Disable;

        #endregion
        #region Contactor param
        public bool Turn_ContactorOff_OL_t1;
        public bool Turn_ContactorOff_OL_t2;
        public bool Turn_ContactorOff_OL_t3;
        public bool Turn_ContactorOff_OL_t4;
        public bool con_tariff_change;
        public bool retry_automatic;
        public uint retry_count;
        public uint retry_auto_interval;
        #endregion
        #region Debug Errors
        public string debug_error;
        #endregion
        #region Debug Cautions
        public string debug_cautions;
        #endregion
        #region Debug Contactor Status
        public string debug_contactor_status;
        #endregion
        #region ModemStatusInfo
        public Param_ModemStatus ModemStatusInfo;
        #endregion

        #endregion //Data Members

        #region Constructor

        #endregion //Constructor

        #region Indexer

        #endregion //Indexer

        #region Properties

        #endregion //Properties

        #region Methods
        public bool InitParameterSet(ParametersSet prm,bool standardModemParam)
        {
            try
            {
                #region Limits
                LM_temperEnergy_kW = prm.ParamLimits.TamperEnergy;
                LM_Ct_Fail_amp = prm.ParamLimits.CTFail_AMP;
                LM_Pt_Fail_amp = prm.ParamLimits.PTFail_AMP;
                LM_Pt_Fail_Volt = prm.ParamLimits.PTFail_Volt;
                LM_over_current_phase_t1 = prm.ParamLimits.OverCurrentByPhase_T1;
                LM_over_current_phase_t2 = prm.ParamLimits.OverCurrentByPhase_T2;
                LM_over_current_phase_t3 = prm.ParamLimits.OverCurrentByPhase_T3;
                LM_over_current_phase_t4 = prm.ParamLimits.OverCurrentByPhase_T4;
                LM_mdi_exceed_t1 = prm.ParamLimits.DemandOverLoadTotal_T1;
                LM_mdi_exceed_t2 = prm.ParamLimits.DemandOverLoadTotal_T2;
                LM_mdi_exceed_t3 = prm.ParamLimits.DemandOverLoadTotal_T3;
                LM_mdi_exceed_t4 = prm.ParamLimits.DemandOverLoadTotal_T4;
                LM_Over_Load_phase_t1 = prm.ParamLimits.OverLoadByPhase_T1;
                LM_Over_Load_phase_t2 = prm.ParamLimits.OverLoadByPhase_T2;
                LM_Over_Load_phase_t3 = prm.ParamLimits.OverLoadByPhase_T3;
                LM_Over_Load_phase_t4 = prm.ParamLimits.OverLoadByPhase_T4;
                LM_Over_Load_Total_t1 = prm.ParamLimits.OverLoadTotal_T1;
                LM_Over_Load_Total_t2 = prm.ParamLimits.OverLoadTotal_T2;
                LM_Over_Load_Total_t3 = prm.ParamLimits.OverLoadTotal_T3;
                LM_Over_Load_Total_t4 = prm.ParamLimits.OverLoadTotal_T4;
                #endregion
                #region Monitoring Time
                MT_Over_Current = prm.ParamMonitoringTime.OverCurrent;
                MT_Over_Load = prm.ParamMonitoringTime.OverLoad;
                #endregion
                #region MDI Parameters
                try
                {
                    MDI_Auto_Reset_Enable = prm.ParamMDI.FLAG_Auto_Reset_0;
                    MDI_Auto_Reset_Date = string.Format("{0},{1},{2},{3} {4}:{5}:{6}"
                        , prm.ParamMDI.Auto_reset_date.Year
                        , prm.ParamMDI.Auto_reset_date.Month
                        , prm.ParamMDI.Auto_reset_date.DayOfMonth
                        , prm.ParamMDI.Auto_reset_date.DayOfWeek
                        , prm.ParamMDI.Auto_reset_date.Hour
                        , prm.ParamMDI.Auto_reset_date.Minute
                        , prm.ParamMDI.Auto_reset_date.Second);
                }
                catch
                {
                }
                #endregion
                #region IP Profiles
                try
                {
                    if (standardModemParam)
                    {
                        ip_port_1 = string.Format("{0}:{1}", GetIP(prm.ParamStandardIPProfiles[0].IP), prm.ParamStandardIPProfiles[0].Wrapper_Over_TCP_port);
                        ip_port_2 = string.Format("{0}:{1}", GetIP(prm.ParamStandardIPProfiles[1].IP), prm.ParamStandardIPProfiles[1].Wrapper_Over_TCP_port);
                        ip_port_3 = string.Format("{0}:{1}", GetIP(prm.ParamStandardIPProfiles[2].IP), prm.ParamStandardIPProfiles[2].Wrapper_Over_TCP_port);
                        ip_port_4 = string.Format("{0}:{1}", GetIP(prm.ParamStandardIPProfiles[3].IP), prm.ParamStandardIPProfiles[3].Wrapper_Over_TCP_port);
                    }
                    else
                    {
                        ip_port_1 = string.Format("{0}:{1}", GetIP(prm.ParamIPProfiles[0].IP), prm.ParamIPProfiles[0].Wrapper_Over_TCP_port);
                        ip_port_2 = string.Format("{0}:{1}", GetIP(prm.ParamIPProfiles[1].IP), prm.ParamIPProfiles[1].Wrapper_Over_TCP_port);
                        ip_port_3 = string.Format("{0}:{1}", GetIP(prm.ParamIPProfiles[2].IP), prm.ParamIPProfiles[2].Wrapper_Over_TCP_port);
                        ip_port_4 = string.Format("{0}:{1}", GetIP(prm.ParamIPProfiles[3].IP), prm.ParamIPProfiles[3].Wrapper_Over_TCP_port);
                    }
                }
                catch (Exception)
                {
                }
                #endregion
                #region Number Profile
                try
                {
                    if(standardModemParam)
                    {
                        number_profile_1 = ConvertToValidString(prm.ParamStandardNumberProfile[0].Number);
                        number_profile_2 = ConvertToValidString(prm.ParamStandardNumberProfile[1].Number);
                        number_profile_3 = ConvertToValidString(prm.ParamStandardNumberProfile[2].Number);
                        number_profile_4 = ConvertToValidString(prm.ParamStandardNumberProfile[3].Number);
                    }
                    else
                    {
                        number_profile_1 = ConvertToValidString(prm.ParamNumberProfile[0].Number);
                        number_profile_2 = ConvertToValidString(prm.ParamNumberProfile[1].Number);
                        number_profile_3 = ConvertToValidString(prm.ParamNumberProfile[2].Number);
                        number_profile_4 = ConvertToValidString(prm.ParamNumberProfile[3].Number);
                    }
                }
                catch 
                {
                }
                #endregion
                #region Keep Alive
                KA_enable = (prm.ParamKeepAliveIP.IP_Profile_ID>0);
                KA_wakeup_id = prm.ParamKeepAliveIP.IP_Profile_ID;
                KA_ping_time_sec = prm.ParamKeepAliveIP.Ping_time;
                #endregion
                #region Modem limits And time
                MTL_Retries_ip = prm.ParamModemLimitsAndTime.Retry_IP_connection;
                MTL_tcp_Inactivity = prm.ParamModemLimitsAndTime.TCP_Inactivity;
                MTL_tcp_sip_send = prm.ParamModemLimitsAndTime.TimeOut_CipSend;
                #endregion
                #region Modem Initialization
                MI_APN = prm.ParamModemInitialize.APN;
                MT_Decrement_Counter = prm.ParamModemBasicsNEW.Flag_DecrementCounter;
                #endregion
                #region Time Base Event
                try
                {
                    TBE1 = string.Format("{0}:{1},{2},{3},{4} {5}:{6}:{7} {8}"
                                     , prm.ParamTimeBaseEvent_1.Control_Enum
                                     , prm.ParamTimeBaseEvent_1.DateTime.Year
                                     , prm.ParamTimeBaseEvent_1.DateTime.Month
                                     , prm.ParamTimeBaseEvent_1.DateTime.DayOfMonth
                                     , prm.ParamTimeBaseEvent_1.DateTime.DayOfWeek
                                     , prm.ParamTimeBaseEvent_1.DateTime.Hour
                                     , prm.ParamTimeBaseEvent_1.DateTime.Minute
                                     , prm.ParamTimeBaseEvent_1.DateTime.Second
                                     , prm.ParamTimeBaseEvent_1.Control_Enum == 3 ? prm.ParamTimeBaseEvent_1.IntervalSink.ToString() : new TimeSpan(00, 00, prm.ParamTimeBaseEvent_1.Interval).ToString()
                                     );
                }
                catch
                {
                }
                try
                {
                    TBE2 = string.Format("{0}:{1},{2},{3},{4} {5}:{6}:{7} {8}"
                             , prm.ParamTimeBaseEvent_2.Control_Enum
                             , prm.ParamTimeBaseEvent_2.DateTime.Year
                             , prm.ParamTimeBaseEvent_2.DateTime.Month
                             , prm.ParamTimeBaseEvent_2.DateTime.DayOfMonth
                             , prm.ParamTimeBaseEvent_2.DateTime.DayOfWeek
                             , prm.ParamTimeBaseEvent_2.DateTime.Hour
                             , prm.ParamTimeBaseEvent_2.DateTime.Minute
                             , prm.ParamTimeBaseEvent_2.DateTime.Second
                             , prm.ParamTimeBaseEvent_2.Control_Enum == 3 ? prm.ParamTimeBaseEvent_2.IntervalSink.ToString() : new TimeSpan(00, 00, prm.ParamTimeBaseEvent_2.Interval).ToString());
                }
                catch
                {
                }
                TBE1_Disable = prm.ParamTBPowerFail.disableEventAtPowerFail_TBE1;
                TBE2_Disable = prm.ParamTBPowerFail.disableEventAtPowerFail_TBE2;

                #endregion //Methods
                #region Contactor param
                Turn_ContactorOff_OL_t1 = prm.ParamContactor.Over_Load_T1_FLAG_0;
                Turn_ContactorOff_OL_t2 = prm.ParamContactor.Over_Load_T2_FLAG_1;
                Turn_ContactorOff_OL_t3 = prm.ParamContactor.Over_Load_T3_FLAG_2;
                Turn_ContactorOff_OL_t4 = prm.ParamContactor.Over_Load_T4_FLAG_3;
                con_tariff_change = prm.ParamContactor.reconnect_by_tariff_change;
                retry_automatic = prm.ParamContactor.reconnect_automatic_or_switch;
                retry_count = prm.ParamContactor.RetryCount;
                retry_auto_interval = prm.ParamContactor.Interval_Between_Retries;
                #endregion
                #region Debug Errors
                debug_error = prm.Debug_Error;
                #endregion
                #region Debug Cautions
                debug_cautions = prm.Debug_Cautions;
                #endregion
                #region Debug Contactor Status
                debug_contactor_status = prm.Debug_Contactor_Status;
                #endregion
                #region ModemStatus Info
                ModemStatusInfo = prm.ParamModemInfo;
                #endregion
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        private string GetIP(UInt32 val) 
        {
            var bytes = BitConverter.GetBytes(val);
            return string.Join(".",bytes.Reverse());
        }

        public static string ConvertToValidString(byte[] array)
        {
            try
            {
                string _toReturn = "";

                byte current_Char = 0;
                if (array != null)
                {
                    current_Char = array[0];

                    for (int i = 0; i < array.Length; i++)
                    {
                        current_Char = array[i];
                        if (current_Char < (byte)10 && current_Char >= (byte)0)
                        {
                            _toReturn += current_Char.ToString();
                        }
                        else
                            //_toReturn += Char.ConvertFromUtf32(current_Char + 48);
                            _toReturn += Char.ConvertFromUtf32(current_Char);
                    }

                }
                if (!String.IsNullOrEmpty(_toReturn))
                    _toReturn = _toReturn.TrimEnd(("? " + Convert.ToChar(0x0F)).ToCharArray());
                return _toReturn;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion //Methods
    }
}

