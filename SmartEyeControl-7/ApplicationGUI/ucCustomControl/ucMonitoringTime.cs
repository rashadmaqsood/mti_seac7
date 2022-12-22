using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.DataContainer;
using SharedCode.Common;
using SharedCode.Comm.Param;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucMonitoringTime : UserControl
    {
        #region Data_Members

        private Param_Monitoring_Time _Param_Monitoring_time_object = null;
        private MonitoringTimeValues _Param_MonitoringTimeValues = null;
        public static readonly String format24Hr = @"hh\:mm\:ss"; //24Hr clock time format
        public static readonly String format = @"mm\:ss";///@"minute:second time format  

        #endregion

        #region Properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_Monitoring_Time Param_Monitoring_time_object
        {
            get { return _Param_Monitoring_time_object; }
            set { _Param_Monitoring_time_object = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public MonitoringTimeValues Param_MonitoringTimeValues
        {
            get { return _Param_MonitoringTimeValues; }
            set { _Param_MonitoringTimeValues = value; }
        }

        public bool IsValidated
        {
            get
            {
                if (errorProvider != null)
                {
                    String ErrorMessage = null;
                    foreach (Control itemCtr in gb_MonitoringTime.Controls)
                    {
                        if (itemCtr.GetType() == typeof(DateTimePicker) ||
                       itemCtr.GetType() == typeof(Label))
                        {
                            ErrorMessage = errorProvider.GetError(itemCtr);
                            if (!String.IsNullOrEmpty(ErrorMessage) ||
                                !String.IsNullOrWhiteSpace(ErrorMessage))
                                return false;
                        }
                    }
                }
                return true;
            }
        }

        public void ResetValidationError()
        {
            if (errorProvider != null)
            {
                errorProvider.Clear();
                String ErrorMessage = String.Empty;
                foreach (Control itemCtr in gb_MonitoringTime.Controls)
                {
                    if (itemCtr.GetType() == typeof(DateTimePicker)) //||
                    //itemCtr.GetType() == typeof(Label))
                    {
                        App_Validation.Apply_ValidationResult(true, String.Empty, itemCtr, errorProvider);
                    }
                }
            }
        }
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<AccessRights> AccessRights { get; set; }

        #endregion

        public ucMonitoringTime()
        {
            InitializeComponent();
        }

        public ucMonitoringTime(List<AccessRights> rights)
        {
            InitializeComponent();
            AccessRights = rights;
            ApplyAccessRights(AccessRights);
        }

        private void ucMonitoringTime_Load(object sender, EventArgs e)
        {
            try
            {
                DeattachValidation_EventHandlers();

                if (_Param_Monitoring_time_object == null)
                    _Param_Monitoring_time_object = new Param_Monitoring_Time();
                //Init Default MonitoringTimeValues
                if (_Param_MonitoringTimeValues == null)
                    _Param_MonitoringTimeValues = new MonitoringTimeValues();
                //Apply MonitoringTime MIN & MAX
                Apply_MonitoringTimeValues(_Param_MonitoringTimeValues);
                foreach (Control itemCtr in gb_MonitoringTime.Controls)
                {
                    if (itemCtr.GetType() == typeof(DateTimePicker) ||
                        itemCtr.GetType() == typeof(Label))
                        errorProvider.SetIconAlignment(itemCtr, ErrorIconAlignment.MiddleRight);
                }
                errorProvider.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;
            }
            finally
            {
                AttachValidation_EventHandlers();
            }
        }

        #region Helper_Methods

        private static bool SaveValidate_Param_MonitoringTime(Param_Monitoring_Time Param_MonitoringTime, MonitoringTimeItem MntrThItem,
                    Control Validating_Contrl, ErrorProvider errorProvider)
        {
            bool isValidated = false;
            TimeSpan val = new TimeSpan();
            String ErrorMessage = String.Empty;
            try
            {
                val = TimeSpan.ParseExact(Validating_Contrl.Text, format, CultureInfo.CurrentCulture);
                ErrorMessage = String.Format("Error Range Validating {0},Value {1}", MntrThItem, val);
                Param_MonitoringTime.SetMonitorTime(MntrThItem, val);
                isValidated = App_Validation.Validate_Param_MonitoringTime(Param_MonitoringTime, MntrThItem, ref ErrorMessage);
                if (isValidated)
                {
                    App_Validation.Apply_ValidationResult(isValidated, String.Empty, Validating_Contrl, errorProvider);
                }
                else
                    App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, Validating_Contrl, errorProvider);
            }
            catch (Exception ex)
            {
                if (ErrorMessage == String.Empty)
                    ErrorMessage = String.Format("Error while exec Save//Validate {0}_MonitoringTime", MntrThItem);
                App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, Validating_Contrl, errorProvider);
                Notification notifier = new Notification("Validation Error", ex.Message, 1000, Notification.Sounds.Beep);
            }
            return isValidated;
        }

        private static bool UpdateValidate_Param_MonitoringTime(Param_Monitoring_Time Param_MonitoringTime, MonitoringTimeItem MntrThItem,
                    Control Validating_Contrl, ErrorProvider errorProvider)
        {
            bool isValidated = false;
            TimeSpan val = new TimeSpan();
            String ErrorMessage = String.Empty;
            try
            {
                val = Param_MonitoringTime.GetMonitorTime(MntrThItem);
                ErrorMessage = String.Format("Error Range Validating {0},Value {1}", MntrThItem, val);

                isValidated = App_Validation.Validate_Param_MonitoringTime(Param_MonitoringTime, MntrThItem, ref ErrorMessage);
                if (isValidated)
                {
                    ///Validating_Contrl.Text = val.ToString(format24Hr, CultureInfo.CurrentCulture);
                    ((DateTimePicker)Validating_Contrl).Value = MonitoringTimeValues.Default_DateTime + val;
                }
                else
                {
                    ///Validating_Contrl.Text = MonitoringTimeValues.Default_MinValue.ToString(format24Hr, CultureInfo.CurrentCulture);
                    ((DateTimePicker)Validating_Contrl).Value = MonitoringTimeValues.Default_DateTime + MonitoringTimeValues.Default_MinValue;
                    App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, Validating_Contrl, errorProvider);
                }
            }
            catch (Exception ex)
            {
                if (ErrorMessage == String.Empty)
                    ErrorMessage = String.Format("Error while exec Update//Validate {0}_MonitoringTime", MntrThItem);
                App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, Validating_Contrl, errorProvider);
                Notification notifier = new Notification("Validation Error", ex.Message, 1000, Notification.Sounds.Beep);
            }
            return isValidated;
        }

        private bool Validate_ParamMonitoringTime()
        {
            bool isValidated = false;
            String errorMessage = String.Empty;
            try
            {
                //Validate Param_Limit_Demand_OverLoad
                lbl_ErrorUCMonitoringTime.Visible = false;
                isValidated = App_Validation.Validate_Param_MonitoringTime(Param_Monitoring_time_object, ref errorMessage,
                    Param_MonitoringTimeValues);
                if (!isValidated)
                {
                    //lbl_ErrorUCMonitoringTime.Visible = true; //By Azeem //Hidden
                    App_Validation.Apply_ValidationResult(isValidated, errorMessage, lbl_ErrorUCMonitoringTime, errorProvider);
                }
                //isValidated = App_Validation.Validate_Param_Limits(Param_Limit_Demand_OverLoads, ref errorMessage, Parameterization_Limits);
                //if (!isValidated)
                //{
                //    lbl_ErrorUCLimits.Visible = true;
                //    App_Validation.Apply_ValidationResult(isValidated, errorMessage, lbl_ErrorUCLimits, errorProvider);
                //}
            }
            catch (Exception ex)
            {
                //lbl_ErrorUCMonitoringTime.Visible = true;
                App_Validation.Apply_ValidationResult(isValidated, errorMessage, lbl_ErrorUCMonitoringTime, errorProvider);
                Notification notifier = new Notification("Validation Error", ex.Message, 1000, Notification.Sounds.Beep);
            }
            return isValidated;
        }

        internal void Apply_MonitoringTimeValues(MonitoringTimeValues param_MonitoringTimeValues)
        {
            String toolTipFormatStr = @"{0}:Range[{1:mm\:ss}-{2:mm\:ss}]";
            try
            {
                txt_MonitoringTime_PowerUpDelayForEnergyRecording.MinDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.PowerUpDelayEnergyRecording_MIN;
                txt_MonitoringTime_PowerUpDelayForEnergyRecording.MaxDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.PowerUpDelayEnergyRecording_MAX;
                ///Apply ToolTip here
                toolTip.SetToolTip(txt_MonitoringTime_PowerUpDelayForEnergyRecording,
                    String.Format(toolTipFormatStr, MonitoringTimeItem.PowerUpDelayEnergyRecording,
                    param_MonitoringTimeValues.PowerUpDelayEnergyRecording_MIN,
                    param_MonitoringTimeValues.PowerUpDelayEnergyRecording_MAX));

                txt_MonitoringTime_PowerUpDelayToMonitor.MinDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.PowerUPDelay_MIN;
                txt_MonitoringTime_PowerUpDelayToMonitor.MaxDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.PowerUPDelay_MAX;
                ///Apply ToolTip here
                toolTip.SetToolTip(txt_MonitoringTime_PowerUpDelayToMonitor,
                    String.Format(toolTipFormatStr, MonitoringTimeItem.PowerUPDelay,
                    param_MonitoringTimeValues.PowerUPDelay_MIN,
                    param_MonitoringTimeValues.PowerUPDelay_MAX));

                txt_MonitoringTime_PowerFail.MinDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.PowerFail_MIN;
                txt_MonitoringTime_PowerFail.MaxDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.PowerFail_MAX;
                ///Apply ToolTip here
                toolTip.SetToolTip(txt_MonitoringTime_PowerFail,
                    String.Format(toolTipFormatStr, MonitoringTimeItem.PowerFail,
                    param_MonitoringTimeValues.PowerFail_MIN,
                    param_MonitoringTimeValues.PowerFail_MAX));

                txt_MonitoringTime_CT_Fail.MinDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.CTFail_MIN;
                txt_MonitoringTime_CT_Fail.MaxDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.CTFail_MAX;
                ///Apply ToolTip here
                toolTip.SetToolTip(txt_MonitoringTime_CT_Fail,
                    String.Format(toolTipFormatStr, MonitoringTimeItem.CTFail,
                    param_MonitoringTimeValues.CTFail_MIN,
                    param_MonitoringTimeValues.CTFail_MAX));

                txt_MonitoringTime_PT_Fail.MinDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.PTFail_MIN;
                txt_MonitoringTime_PT_Fail.MaxDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.PTFail_MAX;
                ///Apply ToolTip here
                toolTip.SetToolTip(txt_MonitoringTime_PT_Fail,
                    String.Format(toolTipFormatStr, MonitoringTimeItem.PTFail,
                    param_MonitoringTimeValues.PTFail_MIN,
                    param_MonitoringTimeValues.PTFail_MAX));

                txt_MonitoringTime_ReverseEnergy.MinDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.ReverseEnergy_MIN;
                txt_MonitoringTime_ReverseEnergy.MaxDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.ReverseEnergy_MAX;
                ///Apply ToolTip here
                toolTip.SetToolTip(txt_MonitoringTime_ReverseEnergy,
                    String.Format(toolTipFormatStr, MonitoringTimeItem.ReverseEnergy,
                    param_MonitoringTimeValues.ReverseEnergy_MIN,
                    param_MonitoringTimeValues.ReverseEnergy_MAX));

                txt_MonitoringTime_TamperEnergy.MinDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.TamperEnergy_MIN;
                txt_MonitoringTime_TamperEnergy.MaxDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.TamperEnergy_MAX;
                ///Apply ToolTip here
                toolTip.SetToolTip(txt_MonitoringTime_TamperEnergy,
                    String.Format(toolTipFormatStr, MonitoringTimeItem.TamperEnergy,
                    param_MonitoringTimeValues.TamperEnergy_MIN,
                    param_MonitoringTimeValues.TamperEnergy_MAX));

                txt_MonitoringTime_UnderVolt.MinDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.UnderVolt_MIN;
                txt_MonitoringTime_UnderVolt.MaxDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.UnderVolt_MAX;
                ///Apply ToolTip here
                toolTip.SetToolTip(txt_MonitoringTime_UnderVolt,
                    String.Format(toolTipFormatStr, MonitoringTimeItem.UnderVolt,
                    param_MonitoringTimeValues.UnderVolt_MIN,
                    param_MonitoringTimeValues.UnderVolt_MAX));

                txt_MonitoringTime_PhaseFail.MinDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.PhaseFail_MIN;
                txt_MonitoringTime_PhaseFail.MaxDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.PhaseFail_MAX;
                ///Apply ToolTip here
                toolTip.SetToolTip(txt_MonitoringTime_PhaseFail,
                    String.Format(toolTipFormatStr, MonitoringTimeItem.PhaseFail,
                    param_MonitoringTimeValues.PhaseFail_MIN,
                    param_MonitoringTimeValues.PhaseFail_MAX));

                txt_MonitoringTime_OverVolt.MinDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.OverVolt_MIN;
                txt_MonitoringTime_OverVolt.MaxDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.OverVolt_MAX;
                ///Apply ToolTip here
                toolTip.SetToolTip(txt_MonitoringTime_OverVolt,
                    String.Format(toolTipFormatStr, MonitoringTimeItem.OverVolt,
                    param_MonitoringTimeValues.OverVolt_MIN,
                    param_MonitoringTimeValues.OverVolt_MAX));

                txt_MonitoringTime_OverLoad.MinDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.OverLoad_MIN;
                txt_MonitoringTime_OverLoad.MaxDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.OverLoad_MAX;
                ///Apply ToolTip here
                toolTip.SetToolTip(txt_MonitoringTime_OverLoad,
                    String.Format(toolTipFormatStr, MonitoringTimeItem.OverLoad,
                    param_MonitoringTimeValues.OverLoad_MIN,
                    param_MonitoringTimeValues.OverVolt_MAX));

                txt_MonitoringTime_OverCurrent.MinDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.OverCurrent_MIN;
                txt_MonitoringTime_OverCurrent.MaxDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.OverCurrent_MAX;
                ///Apply ToolTip here
                toolTip.SetToolTip(txt_MonitoringTime_OverCurrent,
                    String.Format(toolTipFormatStr, MonitoringTimeItem.OverCurrent,
                    param_MonitoringTimeValues.OverCurrent_MIN,
                    param_MonitoringTimeValues.OverCurrent_MAX));

                txt_MonitoringTime_HighNeutralCurrent.MinDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.HighNeutralCurrent_MIN;
                txt_MonitoringTime_HighNeutralCurrent.MaxDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.HighNeutralCurrent_MAX;
                ///Apply ToolTip here
                toolTip.SetToolTip(txt_MonitoringTime_HighNeutralCurrent,
                    String.Format(toolTipFormatStr, MonitoringTimeItem.HighNeutralCurrent,
                    param_MonitoringTimeValues.HighNeutralCurrent_MIN,
                    param_MonitoringTimeValues.HighNeutralCurrent_MAX));

                txt_MonitoringTime_ImbalanceVolt.MinDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.ImbalanceVolt_MIN;
                txt_MonitoringTime_ImbalanceVolt.MaxDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.ImbalanceVolt_MAX;
                ///Apply ToolTip here
                toolTip.SetToolTip(txt_MonitoringTime_ImbalanceVolt,
                    String.Format(toolTipFormatStr, MonitoringTimeItem.ImbalanceVolt,
                    param_MonitoringTimeValues.ImbalanceVolt_MIN,
                    param_MonitoringTimeValues.ImbalanceVolt_MAX));

                txt_MonitoringTime_Reverse_Polarity.MinDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.ReversePolarity_MIN;
                txt_MonitoringTime_Reverse_Polarity.MaxDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.ReversePolarity_MAX;
                //Apply ToolTip here
                toolTip.SetToolTip(txt_MonitoringTime_Reverse_Polarity,
                    String.Format(toolTipFormatStr, MonitoringTimeItem.ReversePolarity,
                    param_MonitoringTimeValues.ReversePolarity_MIN,
                    param_MonitoringTimeValues.ReversePolarity_MAX));

                txt_MonitoringTime_Phase_Sequence.MinDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.PhaseSequence_MIN;
                txt_MonitoringTime_Phase_Sequence.MaxDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.PhaseSequence_MAX;
                //Apply ToolTip here
                toolTip.SetToolTip(txt_MonitoringTime_Phase_Sequence,
                    String.Format(toolTipFormatStr, MonitoringTimeItem.PhaseSequence,
                    param_MonitoringTimeValues.PhaseSequence_MIN,
                    param_MonitoringTimeValues.PhaseSequence_MAX));

                txt_MonitoringTime_ContactorPowerFail.MinDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.Contactor_Failure_MIN;
                txt_MonitoringTime_ContactorPowerFail.MaxDate = MonitoringTimeValues.Default_DateTime +
                    param_MonitoringTimeValues.Contactor_Failure_MAX;

                toolTip.SetToolTip(txt_MonitoringTime_ContactorPowerFail,
                    String.Format(toolTipFormatStr, MonitoringTimeItem.Contactor_failure,
                    param_MonitoringTimeValues.Contactor_Failure_MIN,
                    param_MonitoringTimeValues.Contactor_Failure_MAX));

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Apply_MonitoringTimeValues", ex);
            }
        }

        private void AttachValidation_EventHandlers()
        {
            try
            {
                txt_MonitoringTime_ContactorPowerFail.ValueChanged += Txt_MonitoringTime_ContactorPowerFail_ValueChanged;
                txt_MonitoringTime_PowerFail.ValueChanged += txt_MonitoringTime_PowerFail_Leave;

                txt_MonitoringTime_CT_Fail.ValueChanged += txt_MonitoringTime_CT_Fail_Leave;
                txt_MonitoringTime_PT_Fail.ValueChanged += txt_MonitoringTime_PT_Fail_Leave;

                txt_MonitoringTime_ReverseEnergy.ValueChanged += txt_MonitoringTime_ReverseEnergy_Leave;
                txt_MonitoringTime_TamperEnergy.ValueChanged += txt_MonitoringTime_TamperEnergy_Leave;

                txt_MonitoringTime_UnderVolt.ValueChanged += txt_MonitoringTime_UnderVolt_Leave;
                txt_MonitoringTime_OverVolt.ValueChanged += txt_MonitoringTime_OverVolt_Leave;

                txt_MonitoringTime_ImbalanceVolt.ValueChanged += txt_MonitoringTime_ImbalanceVolt_Leave;
                txt_MonitoringTime_PhaseFail.ValueChanged += txt_MonitoringTime_PhaseFail_Leave;

                txt_MonitoringTime_OverLoad.ValueChanged += txt_MonitoringTime_OverLoad_Leave;
                ///text Contactor Monitoring Time Over Load
                ///txt_MonitoringTime_OverLoad_c.Text = Convert.ToStrin
                txt_MonitoringTime_OverCurrent.ValueChanged += txt_MonitoringTime_OverCurrent_Leave;
                txt_MonitoringTime_HighNeutralCurrent.ValueChanged += txt_MonitoringTime_HighNeutralCurrent_Leave;

                txt_MonitoringTime_Reverse_Polarity.ValueChanged += txt_MonitoringTime_Reverse_Polarity_Leave;
                txt_MonitoringTime_Phase_Sequence.ValueChanged += txt_MonitoringTime_Phase_Sequence_Leave;

                txt_MonitoringTime_PowerUpDelayForEnergyRecording.ValueChanged += txt_MonitoringTime_PowerUpDelayForEnergyRecording_Leave;
                txt_MonitoringTime_PowerUpDelayToMonitor.ValueChanged += txt_MonitoringTime_PowerUpDelayToMonitor_Leave;
                txt_MonitoringTime_StartGenerator.ValueChanged += txt_MonitoringTime_StartGenerator_Leave;

                txt_MonitoringTime_HallSensor.ValueChanged += txt_MonitoringTime_HallSensor_ValueChanged;

                txt_MonitoringTime_StartGenerator.ValueChanged += txt_MonitoringTime_StartGenerator_ValueChanged;

                check_PowerupDelayToMonitor_PowerupDelayMonitorVolt.CheckedChanged +=
                    check_PowerupDelayToMonitor_PowerupDelayMonitorVolt_CheckedChanged;

                check_PowerupDelayToMonitor_DisablePulseOnPowerUp.CheckedChanged +=
                    check_PowerupDelayToMonitor_DisablePulseOnPowerUp_CheckedChanged;

                check_PowerupDelayToMonitor_DissableEnergyOnPowerUp.CheckedChanged +=
                    check_PowerupDelayToMonitor_DissableEnergyOnPowerUp_CheckedChanged;

                check_PowerupDelayToMonitor_PowerupDelayMonitorCurrent.CheckedChanged +=
                    check_PowerupDelayToMonitor_PowerupDelayMonitorCurrent_CheckedChanged;

                check_PowerupDelayToMonitor_PowerUpDelayMonitorLoad.CheckedChanged +=
                    check_PowerupDelayToMonitor_PowerUpDelayMonitorLoad_CheckedChanged;
            }
            finally
            {
            }
        }

        private void Txt_MonitoringTime_ContactorPowerFail_ValueChanged(object sender, EventArgs e)
        {
            //Param_Monitoring_time_object.Contactor_Failure = txt_MonitoringTime_ContactorPowerFail.Value;
            SaveValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.Contactor_failure, txt_MonitoringTime_ContactorPowerFail, errorProvider);
            Validate_ParamMonitoringTime();
        }

        private void DeattachValidation_EventHandlers()
        {
            try
            {
                txt_MonitoringTime_ContactorPowerFail.ValueChanged -= Txt_MonitoringTime_ContactorPowerFail_ValueChanged;
                txt_MonitoringTime_PowerFail.ValueChanged -= txt_MonitoringTime_PowerFail_Leave;

                txt_MonitoringTime_CT_Fail.ValueChanged -= txt_MonitoringTime_CT_Fail_Leave;
                txt_MonitoringTime_PT_Fail.ValueChanged -= txt_MonitoringTime_PT_Fail_Leave;

                txt_MonitoringTime_ReverseEnergy.ValueChanged -= txt_MonitoringTime_ReverseEnergy_Leave;
                txt_MonitoringTime_TamperEnergy.ValueChanged -= txt_MonitoringTime_TamperEnergy_Leave;

                txt_MonitoringTime_UnderVolt.ValueChanged -= txt_MonitoringTime_UnderVolt_Leave;
                txt_MonitoringTime_OverVolt.ValueChanged -= txt_MonitoringTime_OverVolt_Leave;

                txt_MonitoringTime_PhaseFail.ValueChanged -= txt_MonitoringTime_PhaseFail_Leave;

                txt_MonitoringTime_OverLoad.ValueChanged -= txt_MonitoringTime_OverLoad_Leave;
                ///text Contactor Monitoring Time Over Load
                ///txt_MonitoringTime_OverLoad_c.Text = Convert.ToStrin
                txt_MonitoringTime_OverCurrent.ValueChanged -= txt_MonitoringTime_OverCurrent_Leave;
                txt_MonitoringTime_HighNeutralCurrent.ValueChanged -= txt_MonitoringTime_HighNeutralCurrent_Leave;

                txt_MonitoringTime_ImbalanceVolt.ValueChanged -= txt_MonitoringTime_ImbalanceVolt_Leave;
                txt_MonitoringTime_Reverse_Polarity.ValueChanged -= txt_MonitoringTime_Reverse_Polarity_Leave;
                txt_MonitoringTime_Phase_Sequence.ValueChanged -= txt_MonitoringTime_Phase_Sequence_Leave;

                txt_MonitoringTime_PowerUpDelayForEnergyRecording.ValueChanged -= txt_MonitoringTime_PowerUpDelayForEnergyRecording_Leave;
                txt_MonitoringTime_PowerUpDelayToMonitor.ValueChanged -= txt_MonitoringTime_PowerUpDelayToMonitor_Leave;

                txt_MonitoringTime_HallSensor.ValueChanged -= txt_MonitoringTime_HallSensor_ValueChanged;

                txt_MonitoringTime_StartGenerator.ValueChanged -= txt_MonitoringTime_StartGenerator_ValueChanged ;

                check_PowerupDelayToMonitor_PowerupDelayMonitorVolt.CheckedChanged -=
                    check_PowerupDelayToMonitor_PowerupDelayMonitorVolt_CheckedChanged;

                check_PowerupDelayToMonitor_DisablePulseOnPowerUp.CheckedChanged -=
                    check_PowerupDelayToMonitor_DisablePulseOnPowerUp_CheckedChanged;

                check_PowerupDelayToMonitor_DissableEnergyOnPowerUp.CheckedChanged -=
                    check_PowerupDelayToMonitor_DissableEnergyOnPowerUp_CheckedChanged;

                check_PowerupDelayToMonitor_PowerupDelayMonitorCurrent.CheckedChanged -=
                    check_PowerupDelayToMonitor_PowerupDelayMonitorCurrent_CheckedChanged;

                check_PowerupDelayToMonitor_PowerUpDelayMonitorLoad.CheckedChanged -=
                    check_PowerupDelayToMonitor_PowerUpDelayMonitorLoad_CheckedChanged;
            }
            finally
            {

            }
        }

        #endregion

        #region Local_Event_Handlers

        private void txt_MonitoringTime_PowerFail_Leave(object sender, EventArgs e)
        {
            ///Param_Monitoring_time_object.PowerFail = txt_MonitoringTime_PowerFail.Value.TimeOfDay;
            SaveValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.PowerFail, txt_MonitoringTime_PowerFail, errorProvider);
            Validate_ParamMonitoringTime();
        }

        private void txt_MonitoringTime_PhaseFail_Leave(object sender, EventArgs e)
        {
            ///Param_Monitoring_time_object.PhaseFail = txt_MonitoringTime_PhaseFail.Value.TimeOfDay;
            SaveValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.PhaseFail,
                txt_MonitoringTime_PhaseFail, errorProvider);
            Validate_ParamMonitoringTime();
        }

        private void txt_MonitoringTime_OverVolt_Leave(object sender, EventArgs e)
        {
            ///Param_Monitoring_time_object.OverVolt = txt_MonitoringTime_OverVolt.Value.TimeOfDay;
            SaveValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.OverVolt,
                txt_MonitoringTime_OverVolt, errorProvider);
            Validate_ParamMonitoringTime();
        }

        private void txt_MonitoringTime_HighNeutralCurrent_Leave(object sender, EventArgs e)
        {
            ///Param_Monitoring_time_object.HighNeutralCurrent = txt_MonitoringTime_HighNeutralCurrent.Value.TimeOfDay;
            SaveValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.HighNeutralCurrent,
                txt_MonitoringTime_HighNeutralCurrent, errorProvider);
            Validate_ParamMonitoringTime();
        }

        private void txt_MonitoringTime_ImbalanceVolt_Leave(object sender, EventArgs e)
        {
            ///Param_Monitoring_time_object.ImbalanceVolt = txt_MonitoringTime_ImbalanceVolt.Value.TimeOfDay;
            SaveValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.ImbalanceVolt,
                txt_MonitoringTime_ImbalanceVolt, errorProvider);
            Validate_ParamMonitoringTime();
        }

        private void txt_MonitoringTime_OverCurrent_Leave(object sender, EventArgs e)
        {
            ///Param_Monitoring_time_object.OverCurrent = txt_MonitoringTime_OverCurrent.Value.TimeOfDay;
            SaveValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.OverCurrent,
                txt_MonitoringTime_OverCurrent, errorProvider);
            Validate_ParamMonitoringTime();
        }

        private void txt_MonitoringTime_OverLoad_Leave(object sender, EventArgs e)
        {
            DateTimePicker txt_MonitoringTime_OverLoadLocal = (DateTimePicker)sender;
            ///Param_Monitoring_time_object.OverLoad = txt_MonitoringTime_OverLoadLocal.Value.TimeOfDay;

            SaveValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.OverLoad,
                txt_MonitoringTime_OverLoadLocal, errorProvider);
            Validate_ParamMonitoringTime();
        }

        private void txt_MonitoringTime_UnderVolt_Leave(object sender, EventArgs e)
        {

            SaveValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.UnderVolt,
                txt_MonitoringTime_UnderVolt, errorProvider);
            Validate_ParamMonitoringTime();
        }

        private void txt_MonitoringTime_ReverseEnergy_Leave(object sender, EventArgs e)
        {
            ///Param_Monitoring_time_object.ReverseEnergy = txt_MonitoringTime_ReverseEnergy.Value.TimeOfDay;
            SaveValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.ReverseEnergy,
                txt_MonitoringTime_ReverseEnergy, errorProvider);
            Validate_ParamMonitoringTime();
        }

        private void txt_MonitoringTime_TamperEnergy_Leave(object sender, EventArgs e)
        {
            ///Param_Monitoring_time_object.TamperEnergy = txt_MonitoringTime_TamperEnergy.Value.TimeOfDay;
            SaveValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.TamperEnergy,
                txt_MonitoringTime_TamperEnergy, errorProvider);
            Validate_ParamMonitoringTime();
        }

        private void txt_MonitoringTime_CT_Fail_Leave(object sender, EventArgs e)
        {
            ///Param_Monitoring_time_object.CTFail = txt_MonitoringTime_CT_Fail.Value.TimeOfDay;
            SaveValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.CTFail,
                txt_MonitoringTime_CT_Fail, errorProvider);
            Validate_ParamMonitoringTime();
        }

        private void txt_MonitoringTime_PT_Fail_Leave(object sender, EventArgs e)
        {
            ///Param_Monitoring_time_object.PTFail = txt_MonitoringTime_PT_Fail.Value.TimeOfDay;
            SaveValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.PTFail,
                txt_MonitoringTime_PT_Fail, errorProvider);
            Validate_ParamMonitoringTime();
        }

        private void txt_MonitoringTime_PowerUpDelayToMonitor_Leave(object sender, EventArgs e)
        {
            TimeSpan tmp = txt_MonitoringTime_PowerUpDelayToMonitor.Value.TimeOfDay;

            SaveValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.PowerUPDelay,
                txt_MonitoringTime_PowerUpDelayToMonitor, errorProvider);
            Validate_ParamMonitoringTime();
        }

        private void txt_MonitoringTime_PowerUpDelayForEnergyRecording_Leave(object sender, EventArgs e)
        {
            //TimeSpan tmp = txt_MonitoringTime_PowerUpDelayForEnergyRecording.Value.TimeOfDay;

            SaveValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.PowerUpDelayEnergyRecording,
                txt_MonitoringTime_PowerUpDelayForEnergyRecording, errorProvider);
            Validate_ParamMonitoringTime();
        }

        private void txt_MonitoringTime_Reverse_Polarity_Leave(object sender, EventArgs e)
        {
            ///Param_Monitoring_time_object.ReversePolarity = txt_MonitoringTime_Reverse_Polarity.Value.TimeOfDay;
            SaveValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.ReversePolarity,
                txt_MonitoringTime_Reverse_Polarity, errorProvider);
            Validate_ParamMonitoringTime();
        }

        private void txt_MonitoringTime_Phase_Sequence_Leave(object sender, EventArgs e)
        {
            ///Param_Monitoring_time_object.PhaseSequence = txt_MonitoringTime_Phase_Sequence.Value.TimeOfDay;
            SaveValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.PhaseSequence,
                txt_MonitoringTime_Phase_Sequence, errorProvider);
            Validate_ParamMonitoringTime();
        }

        private void txt_MonitoringTime_StartGenerator_Leave(object sender, EventArgs e)
        {
            
        }

        void txt_MonitoringTime_HallSensor_ValueChanged(object sender, EventArgs e)
        {
            ///Param_Monitoring_time_object.PhaseSequence = txt_MonitoringTime_Phase_Sequence.Value.TimeOfDay;
            SaveValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.HALLSensor,
                txt_MonitoringTime_HallSensor, errorProvider);
            Validate_ParamMonitoringTime();
        }

        void txt_MonitoringTime_StartGenerator_ValueChanged(object sender, EventArgs e)
        {
            Validate_ParamMonitoringTime();
        }
        private void check_PowerupDelayToMonitor_PowerupDelayMonitorVolt_CheckedChanged(object sender, EventArgs e)
        {
            Param_Monitoring_time_object.IsPowerupDelayMonitorVolt_FLAG0 = check_PowerupDelayToMonitor_PowerupDelayMonitorVolt.Checked;
        }

        private void check_PowerupDelayToMonitor_PowerupDelayMonitorCurrent_CheckedChanged(object sender, EventArgs e)
        {
            Param_Monitoring_time_object.IsPowerupDelayMonitorCurrent_FLAG1 = check_PowerupDelayToMonitor_PowerupDelayMonitorVolt.Checked;
        }

        private void check_PowerupDelayToMonitor_PowerUpDelayMonitorLoad_CheckedChanged(object sender, EventArgs e)
        {
            Param_Monitoring_time_object.IsPowerupDelayMonitorLoad_FLAG2 = check_PowerupDelayToMonitor_PowerupDelayMonitorVolt.Checked;
        }

        private void check_PowerupDelayToMonitor_DisablePulseOnPowerUp_CheckedChanged(object sender, EventArgs e)
        {
            Param_Monitoring_time_object.IsDisablePulseOnPowerUp_FLAG3 = check_PowerupDelayToMonitor_PowerupDelayMonitorVolt.Checked;

        }

        private void check_PowerupDelayToMonitor_DissableEnergyOnPowerUp_CheckedChanged(object sender, EventArgs e)
        {
            Param_Monitoring_time_object.IsDisableEnergyOnPowerUp_FLAG4 = check_PowerupDelayToMonitor_PowerupDelayMonitorVolt.Checked;
        }

        #endregion

        public void showToGUI_MonitoringTime()
        {
            try
            {
                DeattachValidation_EventHandlers();
                ResetValidationError();

                UpdateValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.PowerFail, txt_MonitoringTime_PowerFail, errorProvider);
                UpdateValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.CTFail, txt_MonitoringTime_CT_Fail, errorProvider);

                UpdateValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.PTFail, txt_MonitoringTime_PT_Fail, errorProvider);
                UpdateValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.ReverseEnergy, txt_MonitoringTime_ReverseEnergy, errorProvider);

                UpdateValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.TamperEnergy, txt_MonitoringTime_TamperEnergy, errorProvider);
                UpdateValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.UnderVolt, txt_MonitoringTime_UnderVolt, errorProvider);

                UpdateValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.PhaseFail, txt_MonitoringTime_PhaseFail, errorProvider);
                UpdateValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.OverVolt, txt_MonitoringTime_OverVolt, errorProvider);
                UpdateValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.OverLoad, txt_MonitoringTime_OverLoad, errorProvider);

                //text Contactor Monitoring Time Over Load
                //txt_MonitoringTime_OverLoad_c.Text = Convert.ToString(Param_Monitoring_time_object.Over_Load.ToString(format));

                UpdateValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.OverCurrent, txt_MonitoringTime_OverCurrent, errorProvider);
                UpdateValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.HighNeutralCurrent, txt_MonitoringTime_HighNeutralCurrent, errorProvider);

                UpdateValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.ImbalanceVolt, txt_MonitoringTime_ImbalanceVolt, errorProvider);
                UpdateValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.ReversePolarity, txt_MonitoringTime_Reverse_Polarity, errorProvider);

                UpdateValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.PhaseSequence, txt_MonitoringTime_Phase_Sequence, errorProvider);
                UpdateValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.PowerUpDelayEnergyRecording,
                                                    txt_MonitoringTime_PowerUpDelayForEnergyRecording, errorProvider);

                UpdateValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.PowerUPDelay, txt_MonitoringTime_PowerUpDelayToMonitor, errorProvider);
                UpdateValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.HALLSensor, txt_MonitoringTime_HallSensor, errorProvider);
                
                check_PowerupDelayToMonitor_PowerupDelayMonitorVolt.Checked = Param_Monitoring_time_object.IsPowerupDelayMonitorVolt_FLAG0;
                check_PowerupDelayToMonitor_DisablePulseOnPowerUp.Checked = Param_Monitoring_time_object.IsDisablePulseOnPowerUp_FLAG3;
                check_PowerupDelayToMonitor_DissableEnergyOnPowerUp.Checked = Param_Monitoring_time_object.IsDisableEnergyOnPowerUp_FLAG4;
                check_PowerupDelayToMonitor_PowerupDelayMonitorCurrent.Checked = Param_Monitoring_time_object.IsPowerupDelayMonitorCurrent_FLAG1;
                check_PowerupDelayToMonitor_PowerUpDelayMonitorLoad.Checked = Param_Monitoring_time_object.IsPowerupDelayMonitorLoad_FLAG2;

                UpdateValidate_Param_MonitoringTime(_Param_Monitoring_time_object, MonitoringTimeItem.Contactor_failure, txt_MonitoringTime_ContactorPowerFail, errorProvider);

            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error show Param_Monitoring_time_object", ex.Message, 5000);
            }
            finally
            {
                Validate_ParamMonitoringTime();
                AttachValidation_EventHandlers();
            }
        }

        #region AccessControlMethods

        public bool ApplyAccessRights(List<AccessRights> Rights)
        {
            bool isSuccess = false;
            try
            {
                this.SuspendLayout();
                tableLayoutPanel.SuspendLayout();
                #region Initialize Layout Code
                
                int pnl_Index = 0;
                int row_index = 0;
                int col_index = 0;
                //Display Sequence Goes here
                Panel[] Panel_Objects = new Panel[] 
                   {
                    pnl_PowerFail,pnl_PhaseFail,pnl_OverVolt,pnl_HighNeutralCurrent,pnl_ImbalanceVolt,
                    pnl_OverCurrent,pnl_OverLoad,pnl_ReversePolarity,pnl_HallSensor,pnl_UnderVolt,pnl_ReverseEnergy,pnl_TamperEnergy,
                    pnl_CTFail,pnl_PTFail,pnl_PhaseSequence,pnl_PUD,pnl_PUD_EnergyRec,pnl_StartGenerator
                   };
                foreach (var pnl_obj in Panel_Objects)
                {
                    pnl_obj.Enabled = true;
                    pnl_obj.Visible = true;
                }
 
                #endregion
                if (Rights.Find(x => x.Read == true || x.Write == true) != null)
                {
                    foreach (var item in Rights)
                    {
                        _HelperAccessRights((MonitoringTime)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
                    }
                    isSuccess = true;
                }
                #region Apply_Layout Logic

                //Reset Table Layout
                tableLayoutPanel.Controls.Clear();
                for (col_index = 0; col_index < (tableLayoutPanel.ColumnCount - 1); col_index += 2)
                {
                    row_index = 0;
                    while (row_index < (tableLayoutPanel.RowCount - 1) && 
                           pnl_Index < Panel_Objects.Length)
                    {
                        Panel Mntr_Obj = Panel_Objects[pnl_Index++];
                        if (Mntr_Obj.Enabled || Mntr_Obj.Visible)
                        {
                            tableLayoutPanel.Controls.Add(Mntr_Obj, col_index, row_index);
                            //Limit_Obj.Visible = true;
                            row_index++;
                        }
                    }
                } 

                #endregion
            }
            finally
            {
                this.ResumeLayout();
                tableLayoutPanel.ResumeLayout();
            }
            return isSuccess;
        }

        private void _HelperAccessRights(MonitoringTime qty, bool read, bool write)
        {
            switch (qty)
            {
                case MonitoringTime.PowerFail:
                    //pnl_PowerFail.Enabled = write;
                    pnl_PowerFail.Visible = read || write;

                    txt_MonitoringTime_PowerFail.Enabled = write;
                    lbl_MonitoringTime_PowerFail.Visible = txt_MonitoringTime_PowerFail.Visible = read;
                    break;
                case MonitoringTime.PhaseFail:
                    //pnl_PhaseFail.Enabled = write;
                    pnl_PhaseFail.Visible = read || write;

                    txt_MonitoringTime_PhaseFail.Enabled = write;
                    lbl_MonitoringTime_PhaseFail.Visible = txt_MonitoringTime_PhaseFail.Visible = read;
                    break;
                case MonitoringTime.OverVolt:
                    //pnl_OverVolt.Enabled = write;
                    pnl_OverVolt.Visible = read || write;

                    txt_MonitoringTime_OverVolt.Enabled = write;
                    lbl_MonitoringTime_OverVolt.Visible = txt_MonitoringTime_OverVolt.Visible = read;
                    break;
                case MonitoringTime.HighNeturalCurrent:
                    //pnl_HighNeutralCurrent.Enabled = write;
                    pnl_HighNeutralCurrent.Visible = read || write;

                    txt_MonitoringTime_HighNeutralCurrent.Enabled = write;
                    lbl_MonitoringTime_HighNeutralCurrent.Visible = txt_MonitoringTime_HighNeutralCurrent.Visible = read;
                    break;
                case MonitoringTime.ImbalanceVolt:
                    //pnl_ImbalanceVolt.Enabled = write;
                    pnl_ImbalanceVolt.Visible = read || write;

                    txt_MonitoringTime_ImbalanceVolt.Enabled = write;
                    lbl_MonitoringTime_ImbalanceVolt.Visible = txt_MonitoringTime_ImbalanceVolt.Visible = read;
                    break;
                case MonitoringTime.OverCurrent:
                    //pnl_OverCurrent.Enabled = write;
                    pnl_OverCurrent.Visible = read || write;

                    txt_MonitoringTime_OverCurrent.Enabled = write;
                    lbl_MonitoringTime_OverCurrent.Visible = txt_MonitoringTime_OverCurrent.Visible = read;
                    break;
                case MonitoringTime.OverLoad:
                    //pnl_OverLoad.Enabled = write;
                    pnl_OverLoad.Visible = read || write;

                    txt_MonitoringTime_OverLoad.Enabled = write;
                    lbl_MonitoringTime_OverLoad.Visible = txt_MonitoringTime_OverLoad.Visible = read;
                    break;
                case MonitoringTime.ReversePolarity:
                    //pnl_ReversePolarity.Enabled = write;
                    pnl_ReversePolarity.Visible = read || write;

                    txt_MonitoringTime_Reverse_Polarity.Enabled = write;
                    lbl_MonitoringTime_Reverse_Polarity.Visible = txt_MonitoringTime_Reverse_Polarity.Visible = read;
                    break;
                case MonitoringTime.HallSensor:
                    //pnl_HallSensor.Enabled = write;
                    pnl_HallSensor.Visible = read || write;

                    txt_MonitoringTime_HallSensor.Enabled = write;
                    lbl_MonitoringTime_HALLSesnor.Visible = txt_MonitoringTime_HallSensor.Visible = read;
                    break;
                case MonitoringTime.UnderVolt:
                    //pnl_UnderVolt.Enabled = write;
                    pnl_UnderVolt.Visible = read || write;

                    txt_MonitoringTime_UnderVolt.Enabled = write;
                    lbl_MonitoringTime_UnderLoad.Visible = txt_MonitoringTime_UnderVolt.Visible = read;
                    break;
                case MonitoringTime.ReverseEnergy:
                    //pnl_ReverseEnergy.Enabled = write;
                    pnl_ReverseEnergy.Visible = read || write;

                    txt_MonitoringTime_ReverseEnergy.Enabled = write;
                    lbl_MonitoringTime_ReverseEnergy.Visible = txt_MonitoringTime_ReverseEnergy.Visible = read;
                    break;
                case MonitoringTime.TemperEnergy:
                    //pnl_TamperEnergy.Enabled = write;
                    pnl_TamperEnergy.Visible = read || write;

                    txt_MonitoringTime_TamperEnergy.Enabled = write;
                    lbl_MonitoringTime_TamperEnergy.Visible = txt_MonitoringTime_TamperEnergy.Visible = read;
                    break;
                case MonitoringTime.CTFail:
                    //pnl_CTFail.Enabled = write;
                    pnl_CTFail.Visible = read || write;

                    txt_MonitoringTime_CT_Fail.Enabled = write;
                    lbl_MonitoringTime_CT_Fail.Visible = txt_MonitoringTime_CT_Fail.Visible = read;
                    break;
                case MonitoringTime.PTFail:
                    //pnl_PTFail.Enabled = write;
                    pnl_PTFail.Visible = read || write;

                    txt_MonitoringTime_PT_Fail.Enabled = write;
                    lbl_MonitoringTime_PT_Fail.Visible = txt_MonitoringTime_PT_Fail.Visible = read;
                    break;
                case MonitoringTime.PUDToMoniter:
                    //pnl_PUD.Enabled = write;
                    pnl_PUD.Visible = read || write;

                    txt_MonitoringTime_PowerUpDelayToMonitor.Enabled = write;
                    lbl_MonitoringTime_PowerUpDelayToMonitor.Visible = txt_MonitoringTime_PowerUpDelayToMonitor.Visible = read;
                    break;
                case MonitoringTime.PUDForEnergyRecording:
                    //pnl_PUD_EnergyRec.Enabled = write;
                    pnl_PUD_EnergyRec.Visible = read || write;

                    txt_MonitoringTime_PowerUpDelayForEnergyRecording.Enabled = write;
                    lbl_MonitoringTime_PowerUpDelayForEnergyRecording.Visible = txt_MonitoringTime_PowerUpDelayForEnergyRecording.Visible = read;
                    break;
                case MonitoringTime.PhaseSequence:
                    //pnl_PhaseSequence.Enabled = write;
                    pnl_PhaseSequence.Visible = read || write;

                    txt_MonitoringTime_Phase_Sequence.Enabled = write;
                    lbl_MonitoringTime_Phase_Sequence.Visible = txt_MonitoringTime_Phase_Sequence.Visible = read;
                    break;
                case MonitoringTime.StartGenerator:
                    pnl_StartGenerator.Visible = false; // read || write;
                    //txt_MonitoringTime_StartGenerator.Enabled = write;
                    //lbl_MonitoringTime_StartGenerator.Visible = txt_MonitoringTime_StartGenerator.Visible = read;
                    break;
                case MonitoringTime.ContactorFailure:
                    //pnl_PowerFail.Enabled = write;
                    pnl_ContactorFail.Visible = read || write;

                    txt_MonitoringTime_ContactorPowerFail.Enabled = write;
                    lbl_MonitoringTime_ContactorPowerFail.Visible = txt_MonitoringTime_ContactorPowerFail.Visible = read;
                    break;

                default:
                    break;
            }
        }

        #endregion

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
    }
}
