using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using datetime;
using SharedCode.Comm.HelperClasses;
using SharedCode.Common;
using SharedCode.Comm.DataContainer;
using SEAC.Common;
using SharedCode.Comm.Param;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucClockCalib : UserControl
    {
        private Param_Clock_Caliberation _Param_clock_caliberation_object = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_Clock_Caliberation Param_clock_caliberation_object
        {
            get { return _Param_clock_caliberation_object; }
            set { _Param_clock_caliberation_object = value; }
        }

        public bool IsValidated
        {
            get
            {
                if (errorProvider != null)
                {
                    String ErrorMessage = null;
                    foreach (Control itemCtr in gb_Clock.Controls)
                    {
                        if (itemCtr.GetType() == typeof(TextBox) ||
                       itemCtr.GetType() == typeof(ComboBox) ||
                       itemCtr.GetType() == typeof(SharedCode.Comm.HelperClasses.DateTimeChooser))
                        {
                            ErrorMessage = errorProvider.GetError(itemCtr);
                            if (!String.IsNullOrEmpty(ErrorMessage) ||
                                !String.IsNullOrWhiteSpace(ErrorMessage))
                                return false;
                        }
                    }
                    ///validate setTime
                    ErrorMessage = errorProvider.GetError(txt_Clock_set_time);
                    if (!String.IsNullOrEmpty(ErrorMessage) ||
                        !String.IsNullOrWhiteSpace(ErrorMessage))
                        return false;
                    ErrorMessage = errorProvider.GetError(this);
                    if (!String.IsNullOrEmpty(ErrorMessage) ||
                        !String.IsNullOrWhiteSpace(ErrorMessage))
                        return false;
                }
                return true;
            }
        }

        public ucClockCalib()
        {
            InitializeComponent();
        }

        private void ucClockCalib_Load(object sender, EventArgs e)
        {
            txt_Clock_ClockCalibrationPPM.Text = "32.678000";
            if (_Param_clock_caliberation_object == null)
                _Param_clock_caliberation_object = new Param_Clock_Caliberation();
            if (errorProvider != null)
            {
                foreach (Control itemCtr in gb_Clock.Controls)
                {
                    if (itemCtr.GetType() == typeof(TextBox) ||
                        itemCtr.GetType() == typeof(ComboBox) ||
                        itemCtr.GetType() == typeof(SharedCode.Comm.HelperClasses.DateTimeChooser))
                        errorProvider.SetIconAlignment(itemCtr, ErrorIconAlignment.MiddleRight);
                }
                errorProvider.SetIconAlignment(txt_Clock_set_time, ErrorIconAlignment.MiddleRight);
                errorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            }
        }

        #region Clock_leave_events

        private void txt_Clock_BeginDate_ValueChanged(object sender, EventArgs e)
        {
            txt_Clock_BeginDate_Leave(sender, e);
        }

        private void txt_Clock_BeginDate_Leave(object sender, EventArgs e)
        {
            _Param_clock_caliberation_object.Begin_Date = txt_Clock_BeginDate.Value;
        }

        private void txt_Clock_EndDate_ValueChanged(object sender, EventArgs e)
        {
            txt_Clock_EndDate_Leave(sender, e);
        }

        private void txt_Clock_EndDate_Leave(object sender, EventArgs e)
        {
            /// DateTime Date = new DateTime(0, txt_Clock_BeginDate.Value.Month, txt_Clock_BeginDate.Value.Day);
            _Param_clock_caliberation_object.End_Date = txt_Clock_EndDate.Value;
        }

        private void checkbox_Clock_EnableCaliberation_CheckStateChanged(object sender, EventArgs e)
        {
            _Param_clock_caliberation_object.Enable_Caliberation_FLAG = checkbox_Clock_EnableCaliberation.Checked;
        }

        private void checkbox_Clock_PPMAdd_CheckStateChanged(object sender, EventArgs e)
        {
            _Param_clock_caliberation_object.PPM_Add_FLAG = checkbox_Clock_PPMAdd.Checked;
        }

        private void checkbox_Clock_EnableDayLightSaving_CheckStateChanged(object sender, EventArgs e)
        {
            _Param_clock_caliberation_object.Enable_Day_Light_Saving_FLAG = checkbox_Clock_EnableDayLightSaving.Checked;
        }

        private void txt_Clock_ClockCalibrationPPM_TextChanged(object sender, EventArgs e)
        {
            try
            {
                App_Validation_Info ValidationInfo = LocalCommon.AppValidationInfo;
                double Xtal_freq = Convert.ToDouble(txt_Clock_ClockCalibrationPPM.Text);
                bool isValidated = App_Validation.Validate_Range(ValidationInfo.Xtal_freq_min, ValidationInfo.Xtal_freq_max, Xtal_freq);
                if (isValidated)
                {
                    App_Validation.Apply_ValidationResult(true, String.Empty, txt_Clock_ClockCalibrationPPM, ref errorProvider);
                    bool IsNegative_PPM = false;
                    Param_clock_caliberation_object.Clock_Caliberation_PPM =
                        LocalCommon.Calculate_PPM(Xtal_freq, ref IsNegative_PPM);
                    if (IsNegative_PPM)
                        Param_clock_caliberation_object.PPM_Add_FLAG = true;
                    else
                        Param_clock_caliberation_object.PPM_Add_FLAG = false;
                }
                else
                    App_Validation.Apply_ValidationResult(false, String.Format("Validation Failed:Xtal_frequency out of range {0}",
                        Xtal_freq),
                        txt_Clock_ClockCalibrationPPM, ref errorProvider);
            }
            catch (Exception ex)
            {
                ///txt_Clock_ClockCalibrationPPM.ForeColor = Color.Red;
                App_Validation.Apply_ValidationResult(false, ex.Message, txt_Clock_ClockCalibrationPPM, ref errorProvider);
            }
        }

        private void txt_Clock_set_time_Leave(object sender, EventArgs e)
        {
            Param_clock_caliberation_object.Set_Time = txt_Clock_set_time.Value;
        }

        private void ucClockCalib_Leave(object sender, EventArgs e)
        {
            try
            {
                String ErrorMessage = String.Empty;
                bool isValidated = App_Validation.Validate_Param_clock_Calib(Param_clock_caliberation_object, ref ErrorMessage);
                if (isValidated)
                    App_Validation.Apply_ValidationResult(true, String.Empty, this, ref errorProvider);
                else
                    App_Validation.Apply_ValidationResult(false, ErrorMessage, this, ref errorProvider);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Validating Param_Clock_ClockCalibration", ex.Message);
                App_Validation.Apply_ValidationResult(false, ex.Message, this, ref errorProvider);
            }
        }

        #endregion

        private void radio_clock_auto_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_clock_auto.Checked == true)
            {
                rb_Debug_Now_Click(this, new EventArgs());
            }
            else
            {
                rd_Degub_Manual_Click(this, new EventArgs());
            }
            Application.DoEvents();
        }

        private void rb_Debug_Now_Click(object sender, EventArgs e)
        {
            Param_clock_caliberation_object.Set_Time = DateTime.Now;
            txt_Clock_set_time.Value = DateTime.Now;
            tmr_Debug_NowTime.Enabled = true;
            ///radio_clock_auto.Checked = true;
        }

        private void rd_Degub_Manual_Click(object sender, EventArgs e)
        {
            tmr_Debug_NowTime.Enabled = false;
            ///radio_clock_manual.Checked = true;
        }

        private void tmr_Debug_NowTime_Tick(object sender, EventArgs e)
        {
            tmr_Debug_NowTime.Stop();
            Param_clock_caliberation_object.Set_Time = DateTime.Now;
            txt_Clock_set_time.Value = DateTime.Now;
            ///txt_Clock_set_time_Leave(this, new EventArgs());
            tmr_Debug_NowTime.Start();
        }

        public void showToGUI_Clock()
        {
            try
            {
                txt_Clock_set_time.Value = Param_clock_caliberation_object.Set_Time;
                Application.DoEvents();
                bool flag_ppm_add = Param_clock_caliberation_object.PPM_Add_FLAG;
                double PPM = 0;
                double Xtal_freq;
                Xtal_freq = LocalCommon.Calculate_XTALFrequency(Param_clock_caliberation_object.Clock_Caliberation_PPM, !flag_ppm_add);
                //txt_Clock_ClockCalibrationPPM.Text = Xtal_freq.ToString("f6");
                txt_Clock_ClockCalibrationPPM.Text = LocalCommon.
                    notRoundingOff(Xtal_freq.ToString(), 7);
                #region Commented_CodeSection

                //if (flag_ppm_add)
                //{
                //    PPM = Param_clock_caliberation_object.Clock_Caliberation_PPM * -1;
                //}
                //Xtal_freq = (((PPM * 32.768) / 1000000 / 10) + 32.768);
                //if (Xtal_freq < 32.982745 && Xtal_freq > 32.553255)
                //{
                //    // txt_Clock_ClockCalibrationPPM.Text = Xtal_freq.ToString("f6");
                //    txt_Clock_ClockCalibrationPPM.Text = SmartEyeControl_7.Common.Commons.
                //        notRoundingOff(Xtal_freq.ToString(), 7);
                //}
                ////txt_Clock_ClockCalibrationPPM.Text = Xtal_freq.ToString("f6");
                //txt_Clock_ClockCalibrationPPM.Text = SmartEyeControl_7.Common.Commons.
                //    notRoundingOff(Xtal_freq.ToString(), 7);

                //txt_Clock_ClockCalibrationPPM.Text = Convert.ToString(Param_clock_caliberation_object.Clock_Caliberation_PPM);
                //checkbox_Clock_EnableCaliberation.Checked = Param_clock_caliberation_object.Enable_Caliberation_FLAG;
                //checkbox_Clock_PPMAdd.Checked = Param_clock_caliberation_object.PPM_Add_FLAG;
                //checkbox_Clock_EnableDayLightSaving.Checked = Param_clock_caliberation_object.Enable_Day_Light_Saving_FLAG;
                //try
                //{
                //    txt_Clock_BeginDate.Value = Param_clock_caliberation_object.Begin_Date;
                //    txt_Clock_EndDate.Value = Param_clock_caliberation_object.End_Date;
                //}
                //catch
                //{
                //    MessageBox.Show("Clock Caliberation not Loaded Properly, Check the saved profile");
                //} 

                #endregion
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error showToGUI_Clock", ex.Message);
                App_Validation.Apply_ValidationResult(false, ex.Message, txt_Clock_ClockCalibrationPPM, ref errorProvider);
            }
        }

        #region AccessControlMethods

        public bool ApplyAccessRights(List<AccessRights> Rights)
        {
            bool isSuccess = false;
            try
            {
                this.SuspendLayout();
                var AccessRight = Rights.Find((x) => String.Equals(x.QuantityName, Misc.Clock.ToString(),
                    StringComparison.OrdinalIgnoreCase));

                if (AccessRight != null && (AccessRight.Read == true || AccessRight.Write == true))
                {
                    foreach (var item in Rights)
                    {
                        _HelperAccessRights((Misc)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
                    }
                    isSuccess = true;
                }
                else
                    return false;

            }
            finally
            {
                this.ResumeLayout();
            }
            return isSuccess;
        }

        private void _HelperAccessRights(Misc qty, bool read, bool write)
        {
            if (qty == Misc.Clock)
            {
                txt_Clock_BeginDate.Visible = txt_Clock_EndDate.Visible = read || write;
                txt_Clock_BeginDate.Enabled = txt_Clock_EndDate.Enabled = write;

                checkbox_Clock_EnableDayLightSaving.Visible = checkbox_Clock_EnableCaliberation.Visible = checkbox_Clock_PPMAdd.Visible = read || write;
                checkbox_Clock_EnableDayLightSaving.Enabled = checkbox_Clock_EnableCaliberation.Enabled = checkbox_Clock_PPMAdd.Enabled = write;

                txt_Clock_ClockCalibrationPPM.Visible = read || write;
                txt_Clock_ClockCalibrationPPM.Enabled = write;

                txt_Clock_set_time.Visible = read || write;
                txt_Clock_set_time.Enabled = write;

                radio_clock_manual.Visible = radio_clock_auto.Visible = read || write;
                radio_clock_manual.Enabled = radio_clock_auto.Enabled = write;
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