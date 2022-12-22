using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.DataContainer;
using SharedCode.Common;
using SEAC.Common;
using SharedCode.Comm.Param;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucMDIParams : UserControl
    {
        private Param_MDI_parameters _Param_MDI_parameters_object;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Param_MDI_parameters Param_MDI_parameters_object
        {
            get { return _Param_MDI_parameters_object; }
            set { _Param_MDI_parameters_object = value; }
        }

        public bool IsValidated
        {
            get
            {
                if (errorProvider != null)
                {
                    //String ErrorMessage = null;
                    //Validation Error txt_MDIParams_minTime
                    if (!App_Validation.IsControlValidated(txt_MDIParams_minTime, errorProvider))
                        return false;
                    else if (!App_Validation.IsControlValidated(combo_MDIParams_MDIInterval, errorProvider))
                        return false;
                }
                if (!ucMDIAutoResetDateParam.IsValidated)
                    return false;
                return true;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<AccessRights> AccessRights { get; set; }

        public ucMDIParams()
        {
            InitializeComponent();
            //Attach_Handlers();

        }
        public ucMDIParams(List<AccessRights> rights)
            : this()
        {
            AccessRights = rights;
            ApplyAccessRights(rights);
        }

        #region Local_Event_Handlers

        private void check_MDIParams_ManualResetByRemote_CheckStateChanged(object sender, EventArgs e)
        {
            Param_MDI_parameters_object.FLAG_Manual_Reset_by_remote_command_2 = check_MDIParams_ManualResetByRemote.Checked;
        }

        private void check_MDIParams_Autoreset_CheckStateChanged(object sender, EventArgs e)
        {
            Param_MDI_parameters_object.FLAG_Auto_Reset_0 = check_MDIParams_Autoreset.Checked;
        }

        private void check_MDIParams_ManualResetByButton_CheckStateChanged(object sender, EventArgs e)
        {
            Param_MDI_parameters_object.FLAG_Manual_Reset_by_button_1 =
                check_MDIParams_ManualResetByButton.Checked;
        }

        private void check_MDIParams_Manual_Reset_powerDown_Mode_CheckStateChanged(object sender, EventArgs e)
        {
            Param_MDI_parameters_object.FLAG_Manual_Reset_in_PowerDown_Mode =
                check_MDIParams_Manual_Reset_powerDown_Mode.Checked;
        }

        private void check_MDIParams_Auto_Reset_powerDown_Mode_CheckedChanged(object sender, EventArgs e)
        {
            Param_MDI_parameters_object.FLAG_Auto_Reset_in_PowerDown_Mode = check_MDIParams_Auto_Reset_powerDown_Mode.Checked;
        }



        private void txt_MDIParams_minTime_TextChanged(object sender, EventArgs e)
        {
            txt_MDIParams_minTime_Leave(this, new EventArgs());
        }

        private void combo_MDIParams_MDIInterval_Leave(object sender, EventArgs e)
        {

        }

        private void combo_MDIParams_MDIInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            //combo_MDIParams_MDIInterval_Leave(sender, e);
            Param_MDI_parameters_object.MDI_Interval = Convert.ToUInt32(combo_MDIParams_MDIInterval.SelectedItem);
        }

        private void tb_MDIParams_SlidesCount_ValueChanged(object sender, EventArgs e)
        {
            Param_MDI_parameters_object.Roll_slide_count = Convert.ToUInt16(tb_MDIParams_SlidesCount.Value);
        }

        void ContentControl_ValueCustomChanged(object sender, EventArgs e)
        {
            ucMDIAutoResetDateParam_Leave(sender, e);
        }

        internal void ucMDIAutoResetDateParam_Leave(object sender, EventArgs e)
        {
            try
            {
                if (ucAutoResetDateParam != null && Param_MDI_parameters_object != null)
                {
                    Param_MDI_parameters_object.Auto_reset_date = ucAutoResetDateParam.ContentControl.ValueCustom;

                    //StDateTime new_val = new StDateTime();

                    ///Hard Code Fixed DateTime
                    //DLMS.Comm.StDateTime.StDateTimeHelper.SetTime(TimeSpan.Zero, new_val);

                    //new_val.Kind = StDateTime.DateTimeType.DateTime;

                    //new_val.Year = StDateTime.NullYear;
                    //new_val.Month = StDateTime.Null;
                    //new_val.DayOfWeek = StDateTime.Null;

                    //new_val.UTCOffset = StDateTime.NullUTCOffset;

                    //new_val.DayOfMonth = ucAutoResetDateParam.ContentControl.ValueCustom.DayOfMonth;
                    //Param_MDI_parameters_object.Auto_reset_date = new_val;


                    App_Validation.Apply_ValidationResult(true, String.Empty, ucAutoResetDateParam.ContentControl, errorProvider);
                }
                else
                {
                    Notification n = new Notification("Error", "Invalid Object for MDI AutoReset DateTime");

                    App_Validation.Apply_ValidationResult(false, "Error Occurred while validating MDI AutoReset DateTime",
                    ucMDIAutoResetDateParam.dtc_Date, errorProvider);
                }
            }
            catch
            {
                App_Validation.Apply_ValidationResult(false, "Error Occurred while validating MDI_AutoResetDateTime",
                   this, errorProvider);
            }
        }

        private void txt_MDIParams_minTime_Leave(object sender, EventArgs e)
        {
            try
            {
                //10.0.21  Minimum Time b/w Manual Resets SET problem Fixed
                App_Validation_Info ValidationInfo = LocalCommon.AppValidationInfo;
                ///Validate Param_MDI_Parameters.Min_Interval_ManualReset
                bool isValidated = App_Validation.TextBox_RangeValidation((byte)0,
                    ValidationInfo.Interval_ManualReset_Max, txt_MDIParams_minTime, ref errorProvider);
                if (isValidated)
                {
                    Param_MDI_parameters_object.
                    Minimum_Time_Interval_Between_Resets_In_case_of_Manual_Reset = Convert.ToByte(txt_MDIParams_minTime.Text);
                }
            }
            catch
            {
                App_Validation.Apply_ValidationResult(false, "Error Occurred while validating txt_MDIParams_minTime", txt_MDIParams_minTime, ref errorProvider);
            }

            //string ControlName = "Minimum Time Interval b/w Resets";
            //try
            //{
            //    if (!string.IsNullOrEmpty(txt_MDIParams_minTime.Text))
            //    {
            //        Int64 val = Convert.ToInt64(txt_MDIParams_minTime.Text);
            //        if (val >= byte.MinValue && val <= byte.MaxValue)
            //        {
            //            byte val_byte = Convert.ToByte(txt_MDIParams_minTime.Text);
            //            //Param_MDI_parameters_object.Auto_reset_date = ucMDIAutoResetDateParam.StDateTime_Value;
            //            Param_MDI_parameters_object.Minimum_Time_Interval_Between_Resets_In_case_of_Manual_Reset = val_byte;

            //            //App_Validation.Apply_ValidationResult(true, String.Empty, ucMDIAutoResetDateParam.dtc_Date, errorProvider);
            //            App_Validation.Apply_ValidationResult(true, String.Empty, txt_MDIParams_minTime, errorProvider);
            //        }
            //        else
            //        {
            //            Notification n = new Notification("Error", "Invalid Object for " + ControlName);

            //            //App_Validation.Apply_ValidationResult(false, "Error during validating Minimum Time Interval b/w Resets",
            //            //ucMDIAutoResetDateParam.dtc_Date, errorProvider);
            //            App_Validation.Apply_ValidationResult(false, "Error during validating " + ControlName,
            //            txt_MDIParams_minTime, errorProvider);
            //        }

            //    }
            //    else
            //    {
            //        Notification n = new Notification("Error", "Invalid Object for "+ControlName);

            //        //App_Validation.Apply_ValidationResult(false, "Error during validating Minimum Time Interval b/w Resets",
            //        //ucMDIAutoResetDateParam.dtc_Date, errorProvider);
            //        App_Validation.Apply_ValidationResult(false, "Error during validating "+ ControlName,
            //        txt_MDIParams_minTime, errorProvider);
            //    }
            //}
            //catch
            //{
            //    App_Validation.Apply_ValidationResult(false, "Error during validating " + ControlName,
            //       this, errorProvider);
            //}
        }


        #region radio_MDI_s_Click

        private void radio_MDI_s_Click(object sender, EventArgs e)
        {
            Param_MDI_parameters_object.Min_Time_Unit = 0;
        }
        private void radio_MDI_min_Click(object sender, EventArgs e)
        {
            Param_MDI_parameters_object.Min_Time_Unit = 1;
        }
        private void radio_MDI_h_Click(object sender, EventArgs e)
        {
            Param_MDI_parameters_object.Min_Time_Unit = 2;
        }
        private void radio_MDI_d_Click(object sender, EventArgs e)
        {
            Param_MDI_parameters_object.Min_Time_Unit = 3;
        }

        #endregion

        #endregion

        private void ucMDIParams_Load(object sender, EventArgs e)
        {
            if (_Param_MDI_parameters_object == null)
                _Param_MDI_parameters_object = new Param_MDI_parameters();
            //Initialize MDI Interval Manual

            if (combo_MDIParams_MDIInterval.SelectedIndex == -1)
                combo_MDIParams_MDIInterval.SelectedItem = "30";


            //Register Event Handlers here
            if (ucMDIAutoResetDateParam != null)
                ucMDIAutoResetDateParam.Leave += new EventHandler(ucMDIAutoResetDateParam_Leave);
            if (ucAutoResetDateParam != null)
            {
                ucAutoResetDateParam.Leave += new EventHandler(ucAutoResetDateParam_Leave);
                ucAutoResetDateParam.ContentControl.ValueCustomChanged += new Action<object, EventArgs>(ContentControl_ValueCustomChanged);
            }
            #region Set ErrorMessage

            if (errorProvider != null)
            {
                errorProvider.SetIconAlignment(combo_MDIParams_MDIInterval, ErrorIconAlignment.MiddleLeft);
                errorProvider.SetIconAlignment(txt_MDIParams_minTime, ErrorIconAlignment.MiddleLeft);
                errorProvider.SetIconAlignment(this, ErrorIconAlignment.MiddleLeft);
                errorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            }

            #endregion
        }



        void ucAutoResetDateParam_Leave(object sender, EventArgs e)
        {
            try
            {
                String ErrorMessage = String.Empty;
                bool isValiated = App_Validation.Validate_Param_MDI_Parameters(_Param_MDI_parameters_object, ref ErrorMessage);
                if (!isValiated)
                    errorProvider.SetError(this, ErrorMessage);
                else
                    errorProvider.SetError(this, String.Empty);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Save Param_MDI_parameters_object", ex.Message,
                    5000, Notification.Sounds.Hand);
                errorProvider.SetError(this, "Error Save Param_MDI_parameters_object");
            }
        }

        private void ucMDIParams_Leave(object sender, EventArgs e)
        {
            try
            {
                String ErrorMessage = String.Empty;
                bool isValiated = App_Validation.Validate_Param_MDI_Parameters(_Param_MDI_parameters_object, ref ErrorMessage);
                if (!isValiated)
                    errorProvider.SetError(this, ErrorMessage);
                else
                    errorProvider.SetError(this, String.Empty);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Save Param_MDI_parameters_object", ex.Message,
                    5000, Notification.Sounds.Hand);
                errorProvider.SetError(this, "Error Save Param_MDI_parameters_object");
            }
        }

        public void showToGUI_MDIParms()
        {
            try
            {
                Detach_Handlers();

                txt_MDIParams_minTime.Text = Param_MDI_parameters_object.Minimum_Time_Interval_Between_Resets_In_case_of_Manual_Reset.ToString();

                if (Param_MDI_parameters_object.Min_Time_Unit == 0) { radio_MDI_s.Checked = true; }
                if (Param_MDI_parameters_object.Min_Time_Unit == 1) { radio_MDI_min.Checked = true; }
                if (Param_MDI_parameters_object.Min_Time_Unit == 2) { radio_MDI_h.Checked = true; }
                if (Param_MDI_parameters_object.Min_Time_Unit == 3) { radio_MDI_d.Checked = true; }

                combo_MDIParams_MDIInterval.SelectedItem = ((Param_MDI_parameters_object.MDI_Interval)).ToString();
                if (Param_MDI_parameters_object.Roll_slide_count >= tb_MDIParams_SlidesCount.Minimum &&
                    Param_MDI_parameters_object.Roll_slide_count <= tb_MDIParams_SlidesCount.Maximum)
                    tb_MDIParams_SlidesCount.Value = Param_MDI_parameters_object.Roll_slide_count;

                ucMDIAutoResetDateParam.showToGUI_StDateTime(Param_MDI_parameters_object.Auto_reset_date);
                
                //Show Custom DateTime
                ucAutoResetDateParam.ContentControl.ValueCustom = Param_MDI_parameters_object.Auto_reset_date;

                check_MDIParams_Autoreset.Checked = Param_MDI_parameters_object.FLAG_Auto_Reset_0;
                check_MDIParams_ManualResetByButton.Checked = Param_MDI_parameters_object.FLAG_Manual_Reset_by_button_1;
                check_MDIParams_ManualResetByRemote.Checked = Param_MDI_parameters_object.FLAG_Manual_Reset_by_remote_command_2;
                check_MDIParams_Manual_Reset_powerDown_Mode.Checked = Param_MDI_parameters_object.FLAG_Manual_Reset_in_PowerDown_Mode;
                check_MDIParams_Auto_Reset_powerDown_Mode.Checked = Param_MDI_parameters_object.FLAG_Auto_Reset_in_PowerDown_Mode;
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error show Param_MDI_parameters_object", ex.Message,
                    5000, Notification.Sounds.Hand);
            }
            finally
            {
                Attach_Handlers();
            }
        }

        internal void Attach_Handlers()
        {
            try
            {
                check_MDIParams_ManualResetByRemote.CheckStateChanged += check_MDIParams_ManualResetByRemote_CheckStateChanged;
                check_MDIParams_Autoreset.CheckStateChanged += check_MDIParams_Autoreset_CheckStateChanged;
                check_MDIParams_ManualResetByButton.CheckStateChanged += check_MDIParams_ManualResetByButton_CheckStateChanged;
                check_MDIParams_Manual_Reset_powerDown_Mode.CheckStateChanged += check_MDIParams_Manual_Reset_powerDown_Mode_CheckStateChanged;
                check_MDIParams_Auto_Reset_powerDown_Mode.CheckedChanged += check_MDIParams_Auto_Reset_powerDown_Mode_CheckedChanged;

                txt_MDIParams_minTime.Leave += txt_MDIParams_minTime_Leave;
                txt_MDIParams_minTime.TextChanged += txt_MDIParams_minTime_TextChanged;
                combo_MDIParams_MDIInterval.Leave += combo_MDIParams_MDIInterval_Leave;
                combo_MDIParams_MDIInterval.SelectedIndexChanged += combo_MDIParams_MDIInterval_SelectedIndexChanged;
                tb_MDIParams_SlidesCount.ValueChanged += tb_MDIParams_SlidesCount_ValueChanged;

                radio_MDI_s.Click += radio_MDI_s_Click;
                radio_MDI_min.Click += radio_MDI_min_Click;
                radio_MDI_h.Click += radio_MDI_h_Click;
                radio_MDI_d.Click += radio_MDI_d_Click;

                ucMDIAutoResetDateParam.Leave += ucMDIAutoResetDateParam_Leave;

                ucAutoResetDateParam.Leave += ucAutoResetDateParam_Leave;
                ucAutoResetDateParam.ContentControl.ValueCustomChanged += ContentControl_ValueCustomChanged;
            }
            catch { throw; }
        }

        internal void Detach_Handlers()
        {
            try
            {
                check_MDIParams_ManualResetByRemote.CheckStateChanged -= check_MDIParams_ManualResetByRemote_CheckStateChanged;
                check_MDIParams_Autoreset.CheckStateChanged -= check_MDIParams_Autoreset_CheckStateChanged;
                check_MDIParams_ManualResetByButton.CheckStateChanged -= check_MDIParams_ManualResetByButton_CheckStateChanged;
                check_MDIParams_Manual_Reset_powerDown_Mode.CheckStateChanged -= check_MDIParams_Manual_Reset_powerDown_Mode_CheckStateChanged;
                check_MDIParams_Auto_Reset_powerDown_Mode.CheckedChanged -= check_MDIParams_Auto_Reset_powerDown_Mode_CheckedChanged;

                txt_MDIParams_minTime.Leave -= txt_MDIParams_minTime_Leave;
                txt_MDIParams_minTime.TextChanged -= txt_MDIParams_minTime_TextChanged;
                combo_MDIParams_MDIInterval.Leave -= combo_MDIParams_MDIInterval_Leave;
                combo_MDIParams_MDIInterval.SelectedIndexChanged -= combo_MDIParams_MDIInterval_SelectedIndexChanged;
                tb_MDIParams_SlidesCount.ValueChanged -= tb_MDIParams_SlidesCount_ValueChanged;

                radio_MDI_s.Click -= radio_MDI_s_Click;
                radio_MDI_min.Click -= radio_MDI_min_Click;
                radio_MDI_h.Click -= radio_MDI_h_Click;
                radio_MDI_d.Click -= radio_MDI_d_Click;

                ucMDIAutoResetDateParam.Leave -= ucMDIAutoResetDateParam_Leave;

                ucAutoResetDateParam.Leave -= ucAutoResetDateParam_Leave;
                ucAutoResetDateParam.ContentControl.ValueCustomChanged -= ContentControl_ValueCustomChanged;
            }
            catch { throw; }
        }

        #region AccessControlMethods

        public bool ApplyAccessRights(List<AccessRights> Rights)
        {
            bool AnyReadOrWrite = false;
            bool isAnyFlagchecked = false;
            try
            {
                this.SuspendLayout();

                if (Rights.Find(x => x.Read == true || x.Write == true) != null)
                {
                    foreach (var item in Rights)
                    {
                        _HelperAccessRights((MDIParameters)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
                        if (!AnyReadOrWrite)
                            AnyReadOrWrite = (item.Read || item.Write);
                        if (!isAnyFlagchecked && !String.IsNullOrEmpty(item.QuantityName) && item.QuantityName.EndsWith("flag", StringComparison.OrdinalIgnoreCase))
                        {
                            isAnyFlagchecked = (item.Read || item.Write);
                        }
                    }
                    if (!AnyReadOrWrite)
                        return AnyReadOrWrite;
                    #region Make Manual_Reset GP_Invisible here

                    var Right_IntervalManualReset = Rights.Find((x) => x.QuantityType == typeof(MDIParameters) &&
                                        String.Equals(x.QuantityName, MDIParameters.MinimumTimeIntervalBetweenManualReset.ToString()));
                    if (Right_IntervalManualReset != null &&
                       !(Right_IntervalManualReset.Read || Right_IntervalManualReset.Write) && !isAnyFlagchecked)
                    {
                        this.gpMDIManualReset.Visible = false;
                        this.gpMDIAutoReset.Margin = this.gpMDIManualReset.Margin;
                    }
                    else
                    {
                        this.gpMDIManualReset.Visible = true;
                    }

                    #endregion
                    #region Make Auto Reset GP_Invisible here

                    bool Auto_reset = false;
                    foreach (var item in Rights)
                    {
                        if (item == Right_IntervalManualReset || item.QuantityName.EndsWith("flag", StringComparison.OrdinalIgnoreCase))
                            continue;
                        else
                        {
                            if (!Auto_reset)
                                Auto_reset = (item.Read || item.Write);
                        }
                    }
                    if (!Auto_reset)
                        gpMDIAutoReset.Visible = false;
                    else
                        gpMDIAutoReset.Visible = true;

                    #endregion
                    return AnyReadOrWrite;
                }
                return false;
            }
            finally
            {
                this.ResumeLayout();
            }
        }

        private void _HelperAccessRights(MDIParameters qty, bool read, bool write)
        {
            switch (qty)
            {
                case MDIParameters.MinimumTimeIntervalBetweenManualReset:

                    txt_MDIParams_minTime.Enabled = write;

                    radio_MDI_d.Enabled = write;
                    radio_MDI_h.Enabled = write;
                    radio_MDI_min.Enabled = write;
                    radio_MDI_s.Enabled = write;
                    pnl_Manual_Reset.Visible = read;
                    break;

                case MDIParameters.ManualResetByButtonflag:
                    check_MDIParams_ManualResetByButton.Enabled = write;
                    check_MDIParams_ManualResetByButton.Visible = read;
                    break;
                case MDIParameters.ManualResetByRemoteCommandflag:
                    check_MDIParams_ManualResetByRemote.Enabled = write;
                    check_MDIParams_ManualResetByRemote.Visible = read;
                    break;
                case MDIParameters.ManualResetinPowerDownModeflag:
                    check_MDIParams_Manual_Reset_powerDown_Mode.Enabled = write;
                    check_MDIParams_Manual_Reset_powerDown_Mode.Visible = read;
                    break;
                case MDIParameters.DisableAutoResetinPowerDownModeflag:
                    check_MDIParams_Auto_Reset_powerDown_Mode.Enabled = write;
                    check_MDIParams_Auto_Reset_powerDown_Mode.Visible = read;
                    break;
                case MDIParameters.AutoResetEnableflag:
                    check_MDIParams_Autoreset.Enabled = write;
                    check_MDIParams_Autoreset.Visible = read;
                    break;

                case MDIParameters.MDIAutoResetDataTime:
                    pnl_AutoResetEnable.Enabled = write;
                    pnl_AutoResetEnable.Visible = read;

                    ucMDIAutoResetDateParam.Enabled = write;
                    ucMDIAutoResetDateParam.Visible = read;

                    ucAutoResetDateParam.Enabled = write;
                    ucAutoResetDateParam.Visible = read;

                    break;

                case MDIParameters.MDInterval:
                    combo_MDIParams_MDIInterval.Enabled = write;
                    lbl_MDIInterval.Visible = lblMDIInterval.Visible = combo_MDIParams_MDIInterval.Visible = read;
                    break;

                case MDIParameters.MDISlidesCount:
                    tb_MDIParams_SlidesCount.Enabled = write;
                    pnl_SlideCount_Container.Visible = tb_MDIParams_SlidesCount.Visible = read;
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
