using SEAC.Common;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.Param;
using SharedCode.Common;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using SmartEyeControl_7.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucContactor : UserControl
    {
        #region Data_Members

        private Param_ContactorExt _Param_Contactor_object;
        private Param_Monitoring_Time _Param_Monitoring_time_object;
        private Param_Limits _Param_Limits_object;
        private LimitValues limits;

        internal System.ComponentModel.ComponentResourceManager resources =
            new System.ComponentModel.ComponentResourceManager(typeof(ucContactor));

        #endregion

        #region Properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_ContactorExt Param_Contactor_object
        {
            get { return _Param_Contactor_object; }
            set { _Param_Contactor_object = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_Monitoring_Time Param_Monitoring_time_object
        {
            get { return _Param_Monitoring_time_object; }
            set { _Param_Monitoring_time_object = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_Limits Param_Limits_object
        {
            get
            {
                return _Param_Limits_object;
            }
            set { _Param_Limits_object = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        internal LimitValues Parameterization_Limits
        {
            get { return limits; }
            set { limits = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<AccessRights> AccessRights { get; set; }

        #endregion

        public ucContactor()
        {
            InitializeComponent();
            fLP_Buttons.Visible = !Commons.IsQc;
        }

        public ucContactor(List<AccessRights> rights)
            : this()
        {
            AccessRights = ApplicationRight.GetDefaultAccessRightByEnum(typeof(ContactorParam));
            ApplyAccessRights(AccessRights);
        }

        private void ucContactor_Load(object sender, EventArgs e)
        {
            if (_Param_Contactor_object == null)
                _Param_Contactor_object = new Param_ContactorExt();

            if (_Param_Limits_object == null)
                _Param_Limits_object = new Param_Limits();

            if (limits == null)
                limits = new LimitValues(Commons.Default_Meter);

            if (_Param_Monitoring_time_object == null)
                _Param_Monitoring_time_object = new Param_Monitoring_Time();

            ftp_TCO_Events.Enabled = true;
        }

        #region Contactor_leave_events

        private void txt_Contactor_ContactorON_PulseTime_Leave(object sender, EventArgs e)
        {
            ///Validate Contactor_ON_Pulse_Time data range validation
            String ErrorMessage = String.Empty;
            bool isValidated = App_Validation.TextBox_RangeValidation((ushort)0, LocalCommon.AppValidationInfo.Contactor_ON_Max_Pulse_Time,
                txt_Contactor_ContactorON_PulseTime, ref errorProvider);
            if (isValidated)
            {
                _Param_Contactor_object.Contactor_ON_Pulse_Time = Convert.ToUInt16(txt_Contactor_ContactorON_PulseTime.Text);
            }

        }

        private void txt_Contactor_ContactorOFF_PulseTime_Leave(object sender, EventArgs e)
        {
            ///Validate Contactor_OFF_Max_Pulse_Time data range validation
            String ErrorMessage = String.Empty;
            bool isValidated = App_Validation.TextBox_RangeValidation((ushort)0, LocalCommon.AppValidationInfo.Contactor_OFF_Max_Pulse_Time,
                txt_Contactor_ContactorOFF_PulseTime, ref errorProvider);
            if (isValidated)
            {
                _Param_Contactor_object.Contactor_OFF_Pulse_Time = Convert.ToUInt16(txt_Contactor_ContactorOFF_PulseTime.Text);
            }

        }

        private void txt_Contactor_MinIntervalBwContactorStateChange_Leave(object sender, EventArgs e)
        {
            ///Validate txt_Contactor_MinIntervalBwContactorStateChange data range validation
            String ErrorMessage = String.Empty;
            bool isValidated = App_Validation.TextBox_RangeValidation((ushort)0, LocalCommon.AppValidationInfo.Minimum_Interval_Bw_Contactor_State_Change_Max,
                txt_Contactor_MinIntervalBwContactorStateChange, ref errorProvider);
            if (isValidated)
            {
                _Param_Contactor_object.Minimum_Interval_Bw_Contactor_State_Change = Convert.ToUInt16(txt_Contactor_MinIntervalBwContactorStateChange.Text);
            }
        }

        private void txt_Contactor_PowerUpDelayToChangeState_Leave(object sender, EventArgs e)
        {
            ///Validate Power_Up_Delay_To_State_Change data range validation
            String ErrorMessage = String.Empty;
            bool isValidated = App_Validation.TextBox_RangeValidation((ushort)0, LocalCommon.AppValidationInfo.Power_Up_Delay_To_State_Change_Max,
                txt_Contactor_PowerUpDelayToChangeState, ref errorProvider);
            if (isValidated)
            {
                _Param_Contactor_object.Power_Up_Delay_To_State_Change = Convert.ToUInt16(txt_Contactor_PowerUpDelayToChangeState.Text);
            }
        }

        private void txt_Contactor_Failure_Status_Leave(object sender, EventArgs e)
        {
            //Validate Interval_Contactor_Failure_Status data range validation
            String ErrorMessage = String.Empty;
            bool isValidated = App_Validation.TextBox_RangeValidation((ushort)0, (ushort)65535,//Commons.AppValidationInfo.Power_Up_Delay_To_State_Change_Max,
                txt_Contactor_Failure_Status, ref errorProvider);
            if (isValidated)
            {
                ((Param_ContactorExt)Param_Contactor_object).Interval_Contactor_Failure_Status =
                         Convert.ToUInt16(txt_Contactor_Failure_Status.Text);
            }
        }

        private void txt_Contactor_IntervalBWEntries_Leave(object sender, EventArgs e)
        {
            ///Validate Contactor_IntervalBWEntries data range validation
            String ErrorMessage = String.Empty;
            bool isValidated = App_Validation.TextBox_RangeValidation((ushort)0, LocalCommon.AppValidationInfo.Interval_Between_Retries_Max,
                txt_Contactor_IntervalBWEntries, ref errorProvider);
            if (isValidated)
            {
                _Param_Contactor_object.Interval_Between_Retries = Convert.ToUInt32(txt_Contactor_IntervalBWEntries.Text);
            }
        }

        private void txt_Contactor_RetryCount_Leave(object sender, EventArgs e)
        {
            ///Validate RetryCount data range validation
            String ErrorMessage = String.Empty;
            bool isValidated = App_Validation.TextBox_RangeValidation((byte)0, LocalCommon.AppValidationInfo.RetryCount_Max,
                txt_Contactor_RetryCount, ref errorProvider);
            if (isValidated)
            {
                _Param_Contactor_object.RetryCount = Convert.ToByte(txt_Contactor_RetryCount.Text);
            }
        }

        //private void check_Contactor_OverCurrentByPhase_T1_CheckedChanged(object sender, EventArgs e)
        //{
        //    _Param_Contactor_object.Over_Current_By_Phase_T1_FLAG_0 = check_Contactor_OverCurrentByPhase_T1.Checked;
        //}

        //private void check_Contactor_OverCurrentByPhase_T2_CheckedChanged(object sender, EventArgs e)
        //{
        //    _Param_Contactor_object.Over_Current_By_Phase_T2_FLAG_1 = check_Contactor_OverCurrentByPhase_T2.Checked;
        //}

        //private void check_Contactor_OverCurrentByPhase_T3_CheckedChanged(object sender, EventArgs e)
        //{
        //    _Param_Contactor_object.Over_Current_By_Phase_T3_FLAG_2 = check_Contactor_OverCurrentByPhase_T3.Checked;
        //}

        //private void check_Contactor_OverCurrentByPhase_T4_CheckedChanged(object sender, EventArgs e)
        //{
        //    _Param_Contactor_object.Over_Current_By_Phase_T4_FLAG_3 = check_Contactor_OverCurrentByPhase_T4.Checked;
        //}

        //private void check_Contactor_OverLoadByPhase_T1_CheckedChanged(object sender, EventArgs e)
        //{
        //    _Param_Contactor_object.Over_Load_By_Phase_T1_FLAG_4 = check_Contactor_OverLoadByPhase_T1.Checked;
        //}

        //private void check_Contactor_OverLoadByPhase_T2_CheckedChanged(object sender, EventArgs e)
        //{
        //    _Param_Contactor_object.Over_Load_By_Phase_T2_FLAG_5 = check_Contactor_OverLoadByPhase_T2.Checked;
        //}

        //private void check_Contactor_OverLoadByPhase_T3_CheckedChanged(object sender, EventArgs e)
        //{
        //    _Param_Contactor_object.Over_Load_By_Phase_T3_FLAG_6 = check_Contactor_OverLoadByPhase_T3.Checked;
        //}

        //private void check_Contactor_OverLoadByPhase_T4_CheckedChanged(object sender, EventArgs e)
        //{
        //    _Param_Contactor_object.Over_Load_By_Phase_T4_FLAG_7 = check_Contactor_OverLoadByPhase_T4.Checked;
        //}

        private void check_Contactor_OverLoadTotal_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox Sender_CheckBox = (CheckBox)sender;
            if (Sender_CheckBox == check_Contactor_OverLoadTotal_T1)
                _Param_Contactor_object.Over_Load_T1_FLAG_0 = check_Contactor_OverLoadTotal_T1.Checked;
            else if (Sender_CheckBox == check_Contactor_OverLoadTotal_T2)
                _Param_Contactor_object.Over_Load_T2_FLAG_1 = check_Contactor_OverLoadTotal_T2.Checked;
            else if (Sender_CheckBox == check_Contactor_OverLoadTotal_T3)
                _Param_Contactor_object.Over_Load_T3_FLAG_2 = check_Contactor_OverLoadTotal_T3.Checked;
            else if (Sender_CheckBox == check_Contactor_OverLoadTotal_T4)
                _Param_Contactor_object.Over_Load_T4_FLAG_3 = check_Contactor_OverLoadTotal_T4.Checked;
        }

        private void check_Contactor_MDIOverLoad_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox Sender_CheckBox = (CheckBox)sender;
            if (Sender_CheckBox == cb_TCO_MdiOverLoadT1)
                _Param_Contactor_object.Over_MDI_T1_FLAG_4 = cb_TCO_MdiOverLoadT1.Checked;
            else if (Sender_CheckBox == cb_TCO_MdiOverLoadT2)
                _Param_Contactor_object.Over_MDI_T2_FLAG_5 = cb_TCO_MdiOverLoadT2.Checked;
            else if (Sender_CheckBox == cb_TCO_MdiOverLoadT3)
                _Param_Contactor_object.Over_MDI_T3_FLAG_6 = cb_TCO_MdiOverLoadT3.Checked;
            else if (Sender_CheckBox == cb_TCO_MdiOverLoadT4)
                _Param_Contactor_object.Over_MDI_T4_FLAG_7 = cb_TCO_MdiOverLoadT4.Checked;
        }

        //private void check_Contactor_OverVolt_CheckedChanged(object sender, EventArgs e)
        //{
        //    _Param_Contactor_object.Over_Volt_FLAG_0 = check_Contactor_OverVolt.Checked;
        //}

        //private void check_Contactor_UnderVolt_CheckedChanged(object sender, EventArgs e)
        //{
        //    _Param_Contactor_object.Under_Volt_FLAG_1 = check_Contactor_UnderVolt.Checked;
        //}

        private void check_Contactor_onOptically_CheckedChanged(object sender, EventArgs e)
        {
            _Param_Contactor_object.on_by_optically = check_Contactor_onOptically.Checked;
        }

        private void check_Contactor_offOptically_CheckedChanged(object sender, EventArgs e)
        {
            _Param_Contactor_object.off_by_optically = check_Contactor_offOptically.Checked;
        }

        private void check_Contactor_ReconnectAutoOrSwitch_CheckedChanged(object sender, EventArgs e)
        {
            _Param_Contactor_object.reconnect_automatic_or_switch = check_contactor_reconnectAuto.Checked;
        }

        private void check_contactor_reconnectAuto_CheckedChanged(object sender, EventArgs e)
        {
            _Param_Contactor_object.Reconnect_Automatically_on_Retries_Expire = check_contactor_reconnectAuto.Checked;
        }

        private void check_contactor_reconnectSwitch_CheckedChanged(object sender, EventArgs e)
        {
            _Param_Contactor_object.Reconnect_By_Switch_on_Retries_Expire = check_contactor_reconnectSwitch.Checked;

        }

        private void check_Contactor_ReconnectonTariffChange_CheckedChanged(object sender, EventArgs e)
        {
            _Param_Contactor_object.reconnect_by_tariff_change = check_Contactor_ReconnectonTariffChange.Checked;
        }

        private void check_contactor_reconnectbySwitchOrRetries_CheckedChanged(object sender, EventArgs e)
        {
            _Param_Contactor_object.Reconnect_By_Switch_on_Retries_Expire = check_contactor_reconnectAuto.Checked;
        }

        private void txt_Contactor_ControlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txt_Contactor_ControlMode.SelectedIndex != -1)
            {
                _Param_Contactor_object.Control_Mode = Convert.ToByte(txt_Contactor_ControlMode.SelectedIndex);
                //Validate Control_Mode data range validation
                bool isValidated = App_Validation.Validate_Range((byte)0, LocalCommon.AppValidationInfo.Control_Mode_Max, _Param_Contactor_object.Control_Mode);
                if (!isValidated)
                {
                    String ErrorMessage = String.Format("Validation failed,Invalid Param_Contactor.Control_Mode {0}",
                        _Param_Contactor_object.Control_Mode);
                    App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, txt_Contactor_ControlMode, ref errorProvider);
                }
                else
                    App_Validation.Apply_ValidationResult(isValidated, String.Empty, txt_Contactor_ControlMode, ref errorProvider);
            }
        }

        private void radio_contactor_auto_CheckedChanged(object sender, EventArgs e)
        {
            Param_Contactor_object.reconnect_automatic_or_switch = radio_contactor_auto.Checked;
        }

        #endregion

        private void txt_MonitoringTime_OverLoad_Leave(object sender, EventArgs e)
        {
            DateTimePicker txt_MonitoringTime_OverLoadLocal = (DateTimePicker)sender;
            _Param_Monitoring_time_object.OverLoad = txt_MonitoringTime_OverLoadLocal.Value.TimeOfDay;
        }

        public void txt_OverLoadTotal_T1_Leave(object sender, EventArgs e)
        {
            TextBox txt_OverLoadTotal_local = (TextBox)sender;
            ///Validate Over_Load_Total_T1 data range validation
            String ErrorMessage = String.Empty;
            bool isValidated = App_Validation.TextBox_RangeValidation(limits.OverLoadTotal_T1_MIN, limits.OverLoadTotal_T1_MAX,
                txt_OverLoadTotal_local, ref errorProvider);
            if (isValidated)
            {
                _Param_Limits_object.OverLoadTotal_T1 = Convert.ToDouble(txt_OverLoadTotal_local.Text);
                ///_Param_Limits_object.OverLoadTotal_T1 *= 1000;
            }
        }

        public void txt_OverLoadTotal_T2_Leave(object sender, EventArgs e)
        {
            TextBox txt_OverLoadTotal_local = (TextBox)sender;
            ///Validate OverLoadTotal_T2_min data range validation
            String ErrorMessage = String.Empty;
            bool isValidated = App_Validation.TextBox_RangeValidation(limits.OverLoadTotal_T2_MIN, limits.OverLoadTotal_T2_MAX,
                txt_OverLoadTotal_local, ref errorProvider);
            if (isValidated)
            {
                _Param_Limits_object.OverLoadTotal_T2 = Convert.ToDouble(txt_OverLoadTotal_local.Text);
                ///_Param_Limits_object.OverLoadTotal_T2 *= 1000;
            }
        }

        public void txt_OverLoadTotal_T3_Leave(object sender, EventArgs e)
        {
            TextBox txt_OverLoadTotal_local = (TextBox)sender;
            ///Validate Over_Load_Total_T3 data range validation
            String ErrorMessage = String.Empty;
            bool isValidated = App_Validation.TextBox_RangeValidation(limits.OverLoadTotal_T3_MIN, limits.OverLoadTotal_T3_MAX,
                txt_OverLoadTotal_local, ref errorProvider);
            if (isValidated)
            {
                _Param_Limits_object.OverLoadTotal_T3 = Convert.ToDouble(txt_OverLoadTotal_local.Text);
                ///_Param_Limits_object.OverLoadTotal_T3 *= 1000;
            }
        }

        public void txt_OverLoadTotal_T4_Leave(object sender, EventArgs e)
        {
            TextBox txt_OverLoadTotal_local = (TextBox)sender;
            ///Validate Over_Load_Total_T4 data range validation
            String ErrorMessage = String.Empty;
            bool isValidated = App_Validation.TextBox_RangeValidation(limits.OverLoadTotal_T4_MIN, limits.OverLoadTotal_T4_MAX,
                txt_OverLoadTotal_local, ref errorProvider);
            if (isValidated)
            {
                _Param_Limits_object.OverLoadTotal_T4 = Convert.ToDouble(txt_OverLoadTotal_local.Text);
                ///_Param_Limits_object.OverLoadTotal_T4 *= 1000;
            }
        }

        #region Show_TO_GUI

        public void showToGUI_Limits()
        {
            try
            {
                txt_OverLoadTotal_c_T1.Text = LocalCommon.value_to_string(Param_Limits_object.OverLoadTotal_T1); /// 1000);
                txt_OverLoadTotal_c_T2.Text = LocalCommon.value_to_string(Param_Limits_object.OverLoadTotal_T2); /// 1000);
                txt_OverLoadTotal_c_T3.Text = LocalCommon.value_to_string(Param_Limits_object.OverLoadTotal_T3); /// 1000);
                txt_OverLoadTotal_c_T4.Text = LocalCommon.value_to_string(Param_Limits_object.OverLoadTotal_T4); /// 1000);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("ucContactor Limits not Loaded Properly", ex.Message, 1500);
            }

        }

        public void showToGUI_MonitoringTime()
        {
            try
            {
                ///text Contactor Monitoring Time Over Load
                txt_MonitoringTime_OverLoad_c.Text = Convert.ToString(Param_Monitoring_time_object.OverLoad);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Monitoring Times not Loaded Properly", ex.Message, 1500);
            }
        }

        public void showToGUI_Contactor()
        {
            try
            {
                Detach_Handlers();

                txt_Contactor_ContactorOFF_PulseTime.Text = Convert.ToString(Param_Contactor_object.Contactor_OFF_Pulse_Time);
                txt_Contactor_ContactorON_PulseTime.Text = Convert.ToString(Param_Contactor_object.Contactor_ON_Pulse_Time);
                txt_Contactor_IntervalBWEntries.Text = Convert.ToString(Param_Contactor_object.Interval_Between_Retries);
                txt_Contactor_MinIntervalBwContactorStateChange.Text = Convert.ToString(Param_Contactor_object.Minimum_Interval_Bw_Contactor_State_Change);
                txt_Contactor_PowerUpDelayToChangeState.Text = Convert.ToString(Param_Contactor_object.Power_Up_Delay_To_State_Change);
                //Contactor_Failure_Status
                txt_Contactor_Failure_Status.Text = Convert.ToString(Param_Contactor_object.Interval_Contactor_Failure_Status);

                //check_Contactor_LocalControl.Checked = Param_Contactor_object.Local_Control_FLAG_3;
                cb_TCO_OverCurrentByPhaseT1.Checked = Param_Contactor_object.Over_Current_By_Phase_T1_FLAG_0;
                cb_TCO_OverCurrentByPhaseT2.Checked = Param_Contactor_object.Over_Current_By_Phase_T2_FLAG_1;
                cb_TCO_OverCurrentByPhaseT3.Checked = Param_Contactor_object.Over_Current_By_Phase_T3_FLAG_2;
                cb_TCO_OverCurrentByPhaseT4.Checked = Param_Contactor_object.Over_Current_By_Phase_T4_FLAG_3;

                cb_TCO_OverLoadByPhaseT1.Checked = Param_Contactor_object.Over_Load_By_Phase_T1_FLAG_4;
                cb_TCO_OverLoadByPhaseT2.Checked = Param_Contactor_object.Over_Load_By_Phase_T2_FLAG_5;
                cb_TCO_OverLoadByPhaseT3.Checked = Param_Contactor_object.Over_Load_By_Phase_T3_FLAG_6;
                cb_TCO_OverLoadByPhaseT4.Checked = Param_Contactor_object.Over_Load_By_Phase_T4_FLAG_7;
                check_Contactor_OverLoadTotal_T1.Checked = Param_Contactor_object.Over_Load_T1_FLAG_0;
                check_Contactor_OverLoadTotal_T2.Checked = Param_Contactor_object.Over_Load_T2_FLAG_1;
                check_Contactor_OverLoadTotal_T3.Checked = Param_Contactor_object.Over_Load_T3_FLAG_2;
                check_Contactor_OverLoadTotal_T4.Checked = Param_Contactor_object.Over_Load_T4_FLAG_3;
                cb_TCO_MdiOverLoadT1.Checked = Param_Contactor_object.Over_MDI_T1_FLAG_4;
                cb_TCO_MdiOverLoadT2.Checked = Param_Contactor_object.Over_MDI_T2_FLAG_5;
                cb_TCO_MdiOverLoadT3.Checked = Param_Contactor_object.Over_MDI_T3_FLAG_6;
                cb_TCO_MdiOverLoadT4.Checked = Param_Contactor_object.Over_MDI_T4_FLAG_7;
                //check_Contactor_LocalControl.Checked = Param_Contactor_object.Local_Control_FLAG_3;
                cb_TCO_OverVolt.Checked = Param_Contactor_object.Over_Volt_FLAG_0;
                cb_TCO_UnderVolt.Checked = Param_Contactor_object.Under_Volt_FLAG_1;
                cb_TCO_PhaseFail.Checked = Param_Contactor_object.PhaseFail_Flag_2;
                //check_Contactor_RemoteControl.Checked = Param_Contactor_object.Remotely_Control_FLAG_2;
                check_contactor_reconnectAuto.Checked = _Param_Contactor_object.Reconnect_Automatically_on_Retries_Expire;
                check_contactor_reconnectSwitch.Checked = _Param_Contactor_object.Reconnect_By_Switch_on_Retries_Expire;
                check_Contactor_ReconnectonTariffChange.Checked = _Param_Contactor_object.reconnect_by_tariff_change;

                txt_Contactor_RetryCount.Text = Param_Contactor_object.RetryCount.ToString();
                radio_contactor_auto.Checked = Param_Contactor_object.reconnect_automatic_or_switch;
                txt_Contactor_ControlMode.SelectedIndex = _Param_Contactor_object.Control_Mode;

                check_Contactor_onOptically.Checked = Param_Contactor_object.on_by_optically;
                check_Contactor_offOptically.Checked = Param_Contactor_object.off_by_optically;

                update_Contactor_Status();
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error show ParamContactor", ex.Message, 1500);
            }
            finally
            {
                Attach_Handlers();
            }
        }

        #endregion

        public void update_Contactor_Status()
        {
            lbl_ContactorStatus.Text = "Contactor Status(Unknown)";
            lbl_ContactorStatus.Image = null;
            ///Contactor In Connected State
            if (Param_Contactor_object.contactor_read_Status == null)
            {
                lbl_ContactorStatus.Visible = false;
            }
            else if ((bool)Param_Contactor_object.contactor_read_Status)
            {
                lbl_ContactorStatus.Visible = true;
                lbl_ContactorStatus.Text = "Contactor Status(Connected)            ";
                lbl_ContactorStatus.Image = ((System.Drawing.Image)(resources.GetObject("btn_ConnectContactor.Values.Image")));
            }
            else
            {
                lbl_ContactorStatus.Visible = true;
                lbl_ContactorStatus.Text = "Contactor Status(Disconnected)          ";
                lbl_ContactorStatus.Image = ((System.Drawing.Image)(resources.GetObject("btn_DisconnectContactor.Values.Image")));
            }
        }

        internal void Attach_Handlers()
        {
            try
            {
                txt_Contactor_ContactorON_PulseTime.Leave += txt_Contactor_ContactorON_PulseTime_Leave;
                txt_Contactor_ContactorOFF_PulseTime.Leave += txt_Contactor_ContactorOFF_PulseTime_Leave;

                txt_Contactor_MinIntervalBwContactorStateChange.Leave += txt_Contactor_MinIntervalBwContactorStateChange_Leave;
                txt_Contactor_PowerUpDelayToChangeState.Leave += txt_Contactor_PowerUpDelayToChangeState_Leave;

                txt_Contactor_Failure_Status.Leave += txt_Contactor_Failure_Status_Leave;
                txt_Contactor_IntervalBWEntries.Leave += txt_Contactor_IntervalBWEntries_Leave;

                txt_Contactor_RetryCount.Leave += txt_Contactor_RetryCount_Leave;

                //check_Contactor_OverCurrentByPhase_T1.CheckedChanged += check_Contactor_OverCurrentByPhase_T1_CheckedChanged;
                //check_Contactor_OverCurrentByPhase_T2.CheckedChanged += check_Contactor_OverCurrentByPhase_T2_CheckedChanged;
                //check_Contactor_OverCurrentByPhase_T3.CheckedChanged += check_Contactor_OverCurrentByPhase_T3_CheckedChanged;
                //check_Contactor_OverCurrentByPhase_T4.CheckedChanged += check_Contactor_OverCurrentByPhase_T4_CheckedChanged;

                //check_Contactor_OverLoadByPhase_T1.CheckedChanged += check_Contactor_OverLoadByPhase_T1_CheckedChanged;
                //check_Contactor_OverLoadByPhase_T2.CheckedChanged += check_Contactor_OverLoadByPhase_T2_CheckedChanged;
                //check_Contactor_OverLoadByPhase_T3.CheckedChanged += check_Contactor_OverLoadByPhase_T3_CheckedChanged;
                //check_Contactor_OverLoadByPhase_T4.CheckedChanged += check_Contactor_OverLoadByPhase_T4_CheckedChanged;

                check_Contactor_OverLoadTotal_T1.CheckedChanged += check_Contactor_OverLoadTotal_CheckedChanged;
                check_Contactor_OverLoadTotal_T2.CheckedChanged += check_Contactor_OverLoadTotal_CheckedChanged;
                check_Contactor_OverLoadTotal_T3.CheckedChanged += check_Contactor_OverLoadTotal_CheckedChanged;
                check_Contactor_OverLoadTotal_T4.CheckedChanged += check_Contactor_OverLoadTotal_CheckedChanged;

                //check_Contactor_MDIOverLoad_T1.CheckedChanged += check_Contactor_MDIOverLoad_CheckedChanged;
                //check_Contactor_MDIOverLoad_T2.CheckedChanged += check_Contactor_MDIOverLoad_CheckedChanged;
                //check_Contactor_MDIOverLoad_T3.CheckedChanged += check_Contactor_MDIOverLoad_CheckedChanged;
                //check_Contactor_MDIOverLoad_T4.CheckedChanged += check_Contactor_MDIOverLoad_CheckedChanged;

                //check_Contactor_OverVolt.CheckedChanged += check_Contactor_OverVolt_CheckedChanged;
                //check_Contactor_UnderVolt.CheckedChanged += check_Contactor_UnderVolt_CheckedChanged;

                check_Contactor_onOptically.CheckedChanged += check_Contactor_onOptically_CheckedChanged;
                check_Contactor_offOptically.CheckedChanged += check_Contactor_offOptically_CheckedChanged;

                //check_contactor_reconnectAuto.CheckedChanged += check_Contactor_ReconnectAutoOrSwitch_CheckedChanged;

                check_contactor_reconnectAuto.CheckedChanged += check_contactor_reconnectAuto_CheckedChanged;
                check_contactor_reconnectSwitch.CheckedChanged += check_contactor_reconnectSwitch_CheckedChanged;
                check_Contactor_ReconnectonTariffChange.CheckedChanged += check_Contactor_ReconnectonTariffChange_CheckedChanged;
                //check_contactor_reconnectbySwitchOrRetries.CheckedChanged += check_contactor_reconnectbySwitchOrRetries_CheckedChanged;

                txt_Contactor_ControlMode.SelectedIndexChanged += txt_Contactor_ControlMode_SelectedIndexChanged;
                radio_contactor_auto.CheckedChanged += radio_contactor_auto_CheckedChanged;

                txt_MonitoringTime_OverLoad_c.Leave += txt_MonitoringTime_OverLoad_Leave;

                txt_OverLoadTotal_c_T1.Leave += txt_OverLoadTotal_T1_Leave;
                txt_OverLoadTotal_c_T2.Leave += txt_OverLoadTotal_T2_Leave;
                txt_OverLoadTotal_c_T3.Leave += txt_OverLoadTotal_T3_Leave;
                txt_OverLoadTotal_c_T4.Leave += txt_OverLoadTotal_T4_Leave;

            }
            catch { throw; }
        }

        internal void Detach_Handlers()
        {
            try
            {
                txt_Contactor_ContactorON_PulseTime.Leave -= txt_Contactor_ContactorON_PulseTime_Leave;
                txt_Contactor_ContactorOFF_PulseTime.Leave -= txt_Contactor_ContactorOFF_PulseTime_Leave;

                txt_Contactor_MinIntervalBwContactorStateChange.Leave -= txt_Contactor_MinIntervalBwContactorStateChange_Leave;
                txt_Contactor_PowerUpDelayToChangeState.Leave -= txt_Contactor_PowerUpDelayToChangeState_Leave;

                txt_Contactor_Failure_Status.Leave -= txt_Contactor_Failure_Status_Leave;
                txt_Contactor_IntervalBWEntries.Leave -= txt_Contactor_IntervalBWEntries_Leave;

                txt_Contactor_RetryCount.Leave -= txt_Contactor_RetryCount_Leave;

                //check_Contactor_OverCurrentByPhase_T1.CheckedChanged -= check_Contactor_OverCurrentByPhase_T1_CheckedChanged;
                //check_Contactor_OverCurrentByPhase_T2.CheckedChanged -= check_Contactor_OverCurrentByPhase_T2_CheckedChanged;
                //check_Contactor_OverCurrentByPhase_T3.CheckedChanged -= check_Contactor_OverCurrentByPhase_T3_CheckedChanged;
                //check_Contactor_OverCurrentByPhase_T4.CheckedChanged -= check_Contactor_OverCurrentByPhase_T4_CheckedChanged;

                //check_Contactor_OverLoadByPhase_T1.CheckedChanged -= check_Contactor_OverLoadByPhase_T1_CheckedChanged;
                //check_Contactor_OverLoadByPhase_T2.CheckedChanged -= check_Contactor_OverLoadByPhase_T2_CheckedChanged;
                //check_Contactor_OverLoadByPhase_T3.CheckedChanged -= check_Contactor_OverLoadByPhase_T3_CheckedChanged;
                //check_Contactor_OverLoadByPhase_T4.CheckedChanged -= check_Contactor_OverLoadByPhase_T4_CheckedChanged;

                check_Contactor_OverLoadTotal_T1.CheckedChanged -= check_Contactor_OverLoadTotal_CheckedChanged;
                check_Contactor_OverLoadTotal_T2.CheckedChanged -= check_Contactor_OverLoadTotal_CheckedChanged;
                check_Contactor_OverLoadTotal_T3.CheckedChanged -= check_Contactor_OverLoadTotal_CheckedChanged;
                check_Contactor_OverLoadTotal_T4.CheckedChanged -= check_Contactor_OverLoadTotal_CheckedChanged;

                //check_Contactor_MDIOverLoad_T1.CheckedChanged -= check_Contactor_MDIOverLoad_CheckedChanged;
                //check_Contactor_MDIOverLoad_T2.CheckedChanged -= check_Contactor_MDIOverLoad_CheckedChanged;
                //check_Contactor_MDIOverLoad_T3.CheckedChanged -= check_Contactor_MDIOverLoad_CheckedChanged;
                //check_Contactor_MDIOverLoad_T4.CheckedChanged -= check_Contactor_MDIOverLoad_CheckedChanged;

                //check_Contactor_OverVolt.CheckedChanged -= check_Contactor_OverVolt_CheckedChanged;
                //check_Contactor_UnderVolt.CheckedChanged -= check_Contactor_UnderVolt_CheckedChanged;

                check_Contactor_onOptically.CheckedChanged -= check_Contactor_onOptically_CheckedChanged;
                check_Contactor_offOptically.CheckedChanged -= check_Contactor_offOptically_CheckedChanged;

                //check_contactor_reconnectAuto.CheckedChanged -= check_Contactor_ReconnectAutoOrSwitch_CheckedChanged;

                check_contactor_reconnectAuto.CheckedChanged -= check_contactor_reconnectAuto_CheckedChanged;
                check_contactor_reconnectSwitch.CheckedChanged -= check_contactor_reconnectSwitch_CheckedChanged;
                check_Contactor_ReconnectonTariffChange.CheckedChanged -= check_Contactor_ReconnectonTariffChange_CheckedChanged;
                //check_contactor_reconnectbySwitchOrRetries.CheckedChanged -= check_contactor_reconnectbySwitchOrRetries_CheckedChanged;

                txt_Contactor_ControlMode.SelectedIndexChanged -= txt_Contactor_ControlMode_SelectedIndexChanged;
                radio_contactor_auto.CheckedChanged -= radio_contactor_auto_CheckedChanged;

                txt_MonitoringTime_OverLoad_c.Leave -= txt_MonitoringTime_OverLoad_Leave;

                txt_OverLoadTotal_c_T1.Leave -= txt_OverLoadTotal_T1_Leave;
                txt_OverLoadTotal_c_T2.Leave -= txt_OverLoadTotal_T2_Leave;
                txt_OverLoadTotal_c_T3.Leave -= txt_OverLoadTotal_T3_Leave;
                txt_OverLoadTotal_c_T4.Leave -= txt_OverLoadTotal_T4_Leave;
            }
            catch { throw; }
        }

        #region AccessControlMethods

        public bool ApplyAccessRights(List<AccessRights> Rights)
        {
            this.AccessRights = Rights;

            ContactorParam[] All_Controls = null;
            bool IsRetryEnable = false;
            bool IsRetryExpireEnable = false;
            bool IsOverLoadLimitsEnable = false;

            try
            {
                this.SuspendLayout();
                this.fLP_TopRow.SuspendLayout();
                this.flp_LastRow.SuspendLayout();

                if (Rights.Find(x => x.Read == true || x.Write == true) != null)
                {
                    foreach (var item in Rights)
                    {
                        _HelperAccessRights((ContactorParam)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
                    }
                    #region Initialize Logic Work here

                    All_Controls = new ContactorParam[] { ContactorParam.RetryAutoInterval, ContactorParam.RetryCount,
                                                          ContactorParam.RetryAutoflag,ContactorParam.RetrySwitchflag};
                    IsRetryEnable = IsAnyControlWTEnable(All_Controls) || IsAnyControlRDEnable(All_Controls);

                    All_Controls = new ContactorParam[] { ContactorParam.RetryExpireAutoInterval, ContactorParam.RetryExpireAutoflag,
                                                          ContactorParam.RetryExpireSwitchflag};
                    IsRetryExpireEnable = IsAnyControlWTEnable(All_Controls) || IsAnyControlRDEnable(All_Controls);

                    All_Controls = new ContactorParam[] { ContactorParam.OverloadMonitoringTime, ContactorParam.OverLoadLimit,
                                                          ContactorParam.TurnOffOnOverLoadflag};
                    IsOverLoadLimitsEnable = IsAnyControlWTEnable(All_Controls) || IsAnyControlRDEnable(All_Controls);

                    #endregion
                    gp_Reconn_Retry.Visible = IsRetryEnable;
                    gp_RetryExpire.Visible = IsRetryExpireEnable;
                    gp_Reconnect.Visible = IsRetryEnable || IsRetryExpireEnable || IsControlWTEnable(ContactorParam.ReconnectTariffChangeflag)
                        || IsControlRDEnable(ContactorParam.ReconnectTariffChangeflag);

                    gp_OverloadLimitControl.Visible = IsOverLoadLimitsEnable;

                    return true;
                }
                return false;
            }
            finally
            {
                ftp_TCO_Events.Enabled = true;
//flp_TCO_OverCurrentByPhase.Enabled =
//flp_TCO_OverLoadByPhase.Enabled =
//flp_TCO_MdiOverLoad.Enabled =
//true;  //temp Azeem

                this.fLP_TopRow.ResumeLayout();
                this.flp_LastRow.ResumeLayout();
                this.ResumeLayout();
            }
        }

        private void _HelperAccessRights(ContactorParam qty, bool read, bool write)
        {
            switch (qty)
            {
                case ContactorParam.OnPulseTime:
                    txt_Contactor_ContactorON_PulseTime.ReadOnly = !write;
                    fLP_ContactorOn.Visible = read || write;
                    break;
                case ContactorParam.OffPulseTime:
                    txt_Contactor_ContactorOFF_PulseTime.ReadOnly = !write;
                    fLP_ContactorOff.Visible = read || write;
                    break;
                case ContactorParam.MinIntervalStateChange:
                    txt_Contactor_MinIntervalBwContactorStateChange.ReadOnly = !write;
                    fLP_MinInterval_Stat.Visible = read || write;
                    break;
                case ContactorParam.PUDelayStateChange:
                    txt_Contactor_PowerUpDelayToChangeState.ReadOnly = !write;
                    fLP_PUPD_Stat.Visible = read || write;
                    break;
                case ContactorParam.IntervalFailureStatus:
                    txt_Contactor_Failure_Status.ReadOnly = !write;
                    fLP_Intr_ContFailureStat.Visible = read || write;
                    break;
                case ContactorParam.ReadStatus:
                    btnReadStatus.Enabled = btnReadStatus.Visible = (read);
                    //btnReadStatus.Visible = read;
                    break;
                case ContactorParam.ConnectDisconnectRelay:
                    btn_ConnectContactor.Enabled = btn_DisconnectContactor.Enabled =
                    btn_ConnectContactor.Visible = btn_DisconnectContactor.Visible = write;
                    break;
                case ContactorParam.ConnectContactorThroughSwitch:
                    btn_connectThroughSwitch.Enabled =
                    btn_connectThroughSwitch.Visible = write;
                    break;
                case ContactorParam.ContactorParam:
                    btn_GetContactorParams.Enabled = read;
                    btn_GetContactorParams.Visible = read;
                    btn_SetContactorParameters.Enabled = write;
                    btn_SetContactorParameters.Visible = write;
                    break;
                case ContactorParam.OpticallyDisconnectflag:
                    check_Contactor_offOptically.Enabled = write;
                    check_Contactor_offOptically.Visible = read;
                    break;
                case ContactorParam.OpticallyConnectflag:
                    check_Contactor_onOptically.Enabled = write;
                    check_Contactor_onOptically.Visible = read;
                    break;
                case ContactorParam.RetryAutoInterval:
                    txt_Contactor_IntervalBWEntries.ReadOnly = !write;
                    fLP_Retry_AutoIntr.Visible = read;
                    break;
                case ContactorParam.RetryCount:
                    txt_Contactor_RetryCount.ReadOnly = !write;
                    fLP_Retry_Count.Visible = read;
                    break;
                case ContactorParam.RetryAutoflag:
                    radio_contactor_auto.Enabled = write;
                    radio_contactor_auto.Visible = read;
                    break;
                case ContactorParam.RetrySwitchflag:
                    radio_contactor_switch.Enabled = write;
                    radio_contactor_switch.Visible = read;
                    break;
                case ContactorParam.RetryExpireAutoInterval:
                    txt_Contactor_ControlMode.Enabled = write;
                    fLP_RetryExpire_AutoInterval.Visible = read;
                    break;
                case ContactorParam.RetryExpireAutoflag:
                    check_contactor_reconnectAuto.Enabled = write;
                    check_contactor_reconnectAuto.Visible = read;
                    break;
                case ContactorParam.RetryExpireSwitchflag:
                    check_contactor_reconnectSwitch.Enabled = write;
                    check_contactor_reconnectSwitch.Visible = read;
                    break;
                case ContactorParam.ReconnectTariffChangeflag:
                    check_Contactor_ReconnectonTariffChange.Enabled = write;
                    check_Contactor_ReconnectonTariffChange.Visible = read;
                    break;
                case ContactorParam.OverloadMonitoringTime:
                    txt_MonitoringTime_OverLoad_c.Enabled = write;
                    fLP_OverLoad_Mntr.Visible = read;
                    break;
                case ContactorParam.OverLoadLimit:
                    txt_OverLoadTotal_c_T1.ReadOnly = txt_OverLoadTotal_c_T2.ReadOnly =
                    txt_OverLoadTotal_c_T3.ReadOnly = txt_OverLoadTotal_c_T4.ReadOnly = !write;

                    pnl_OverLoad_Limit.Visible = read;
                    break;
                case ContactorParam.TurnOffOnOverLoadflag:
                    check_Contactor_OverLoadTotal_T1.Enabled = check_Contactor_OverLoadTotal_T2.Enabled =
                        check_Contactor_OverLoadTotal_T3.Enabled = check_Contactor_OverLoadTotal_T4.Enabled =
                        cb_TCO_MdiOverLoadT1.Enabled = cb_TCO_OverCurrentByPhaseT2.Enabled =
                        cb_TCO_MdiOverLoadT3.Enabled = cb_TCO_OverCurrentByPhaseT4.Enabled =
                        cb_TCO_OverLoadByPhaseT1.Enabled = cb_TCO_OverLoadByPhaseT2.Enabled =
                        cb_TCO_OverLoadByPhaseT3.Enabled = cb_TCO_OverLoadByPhaseT4.Enabled =
                        cb_TCO_MdiOverLoadT1.Enabled = cb_TCO_MdiOverLoadT2.Enabled =
                        cb_TCO_MdiOverLoadT3.Enabled = cb_TCO_MdiOverLoadT4.Enabled =
                        write;

                    flp_TCO_OverLoadTotal.Visible =
                    flp_TCO_OverCurrentByPhase.Visible =
                    flp_TCO_OverLoadByPhase.Visible =
                    flp_TCO_MdiOverLoad.Visible =
                    read;
                    gp_turn_off_con.Visible = read;
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Support_Function

        private bool IsAnyControlWTEnable(ContactorParam[] type)
        {
            bool isEnable = false;
            try
            {
                foreach (var item in type)
                {
                    if (!isEnable)
                        isEnable = ApplicationRight.IsControlWTEnable(typeof(ContactorParam), item.ToString(), AccessRights);
                    else
                        break;
                }
            }
            catch { }
            return isEnable;
        }

        private bool IsAnyControlRDEnable(ContactorParam[] type)
        {
            bool isEnable = false;
            try
            {
                foreach (var item in type)
                {
                    if (!isEnable)
                        isEnable = ApplicationRight.IsControlRDEnable(typeof(ContactorParam), item.ToString(), AccessRights);
                    else
                        break;
                }
            }
            catch { }
            return isEnable;
        }

        private bool IsControlWTEnable(ContactorParam type)
        {
            bool isEnable = false;
            try
            {
                isEnable = ApplicationRight.IsControlWTEnable(typeof(ContactorParam), type.ToString(), AccessRights);
            }
            catch { }
            return isEnable;
        }

        private bool IsControlRDEnable(ContactorParam type)
        {
            bool isEnable = false;
            try
            {
                isEnable = ApplicationRight.IsControlRDEnable(typeof(ContactorParam), type.ToString(), AccessRights);
            }
            catch { }
            return isEnable;
        }

        #endregion

        private void btn_ConnectContactor_Click(object sender, EventArgs e)
        {

        }

        private void btnReadStatus_Click(object sender, EventArgs e)
        {

        }

        //Flickering Reduction
        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN
                return parms;
            }
        }

        private void cb_TCO_OverVolt_CheckedChanged(object sender, EventArgs e)
        {
            Param_Contactor_object.Over_Volt_FLAG_0 = cb_TCO_OverVolt.Checked;
        }

        private void cb_TCO_UnderVolt_CheckedChanged(object sender, EventArgs e)
        {
            Param_Contactor_object.Under_Volt_FLAG_1 = cb_TCO_UnderVolt.Checked;
        }

        private void cb_TCO_PhaseFail_CheckedChanged(object sender, EventArgs e)
        {
            Param_Contactor_object.PhaseFail_Flag_2 = cb_TCO_PhaseFail.Checked;
        }

        private void cb_TCO_OverLoadByPhase_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox Sender_CheckBox = (CheckBox)sender;
            if (Sender_CheckBox == cb_TCO_OverLoadByPhaseT1)
                Param_Contactor_object.Over_Load_By_Phase_T1_FLAG_4 = cb_TCO_OverLoadByPhaseT1.Checked;
            if (Sender_CheckBox == cb_TCO_OverLoadByPhaseT2)
                Param_Contactor_object.Over_Load_By_Phase_T2_FLAG_5 = cb_TCO_OverLoadByPhaseT2.Checked;
            if (Sender_CheckBox == cb_TCO_OverLoadByPhaseT3)
                Param_Contactor_object.Over_Load_By_Phase_T3_FLAG_6 = cb_TCO_OverLoadByPhaseT3.Checked;
            if (Sender_CheckBox == cb_TCO_OverLoadByPhaseT4)
                Param_Contactor_object.Over_Load_By_Phase_T4_FLAG_7 = cb_TCO_OverLoadByPhaseT4.Checked;
        }

        private void cb_TCO_OverCurrentByPhaseT_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox Sender_CheckBox = (CheckBox)sender;
            if (Sender_CheckBox == cb_TCO_OverCurrentByPhaseT1)
                Param_Contactor_object.Over_Current_By_Phase_T1_FLAG_0 = cb_TCO_OverCurrentByPhaseT1.Checked;
            if (Sender_CheckBox == cb_TCO_OverCurrentByPhaseT2)
                Param_Contactor_object.Over_Current_By_Phase_T2_FLAG_1 = cb_TCO_OverCurrentByPhaseT2.Checked;
            if (Sender_CheckBox == cb_TCO_OverCurrentByPhaseT3)
                Param_Contactor_object.Over_Current_By_Phase_T3_FLAG_2 = cb_TCO_OverCurrentByPhaseT3.Checked;
            if (Sender_CheckBox == cb_TCO_OverCurrentByPhaseT4)
                Param_Contactor_object.Over_Current_By_Phase_T4_FLAG_3 = cb_TCO_OverCurrentByPhaseT4.Checked;
        }

        private void ucContactor_Enter(object sender, EventArgs e)
        {
            showToGUI_MonitoringTime();
            showToGUI_Limits();
        }
    }
}