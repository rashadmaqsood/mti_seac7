using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.Param;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucDateTime : UserControl
    {
        private Param_Clock_Caliberation _Param_clock_caliberation_object = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_Clock_Caliberation Param_clock_caliberation_object
        {
            get { return _Param_clock_caliberation_object; }
            set { _Param_clock_caliberation_object = value; }
        }

        private void ucDateTime_Load(object sender, EventArgs e)
        {
            if (_Param_clock_caliberation_object == null)
                _Param_clock_caliberation_object = new Param_Clock_Caliberation();
        }

        public ucDateTime()
        {
            InitializeComponent();

            this.cmbTimeMethods.SelectedIndex = 0;
            this.cmbClockSynchronizationMethod.SelectedIndex = 0;
        }

        private void txt_Clock_set_time_Leave(object sender, EventArgs e)
        {
            Param_clock_caliberation_object.Set_Time = txt_Clock_set_time_debug.Value;
        }

        private void radio_clock_auto_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_Debug_Now.Checked == true)
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
            tmr_Debug_NowTime.Enabled = true;
            //tmr_Debug_NowTime.Start();
            ///rb_Debug_Now.Checked = true;
        }

        private void rd_Degub_Manual_Click(object sender, EventArgs e)
        {
            tmr_Debug_NowTime.Enabled = false;
            ///rd_Degub_Manual.Checked = true;
        }

        private void tmr_Debug_NowTime_Tick(object sender, EventArgs e)
        {
            tmr_Debug_NowTime.Stop();
            Param_clock_caliberation_object.Set_Time = DateTime.Now;
            txt_Clock_set_time_debug.Value = Param_clock_caliberation_object.Set_Time;
            ///txt_Clock_set_time_Leave(this, new EventArgs());
            tmr_Debug_NowTime.Start();
            Application.DoEvents();
        }

        public void showToGUI_Clock()
        {
            txt_Clock_set_time_debug.Value = Param_clock_caliberation_object.Set_Time;
            Application.DoEvents();
        }

        #region AccessControlMethods

        public bool ApplyAccessRights(List<AccessRights> Rights)
        {
            try
            {
                this.SuspendLayout();
                this.tbMain.TabPages.Clear();
                if (Rights.Find(x => x.Read == true || x.Write == true) != null)
                {
                    foreach (var item in Rights)
                    {
                        _HelperAccessRights((Clock)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
                    }
                    return true;
                }
                return false;
            }
            finally
            {
                this.ResumeLayout();
            }
        }

        private void _HelperAccessRights(Clock qty, bool read, bool write)
        {
            switch (qty)
            {
                case Clock.RTC:
                    if (read == false && write == false)
                        this.tbMain.TabPages.Remove(tpSetTime);
                    else
                    {
                        if (!this.tbMain.TabPages.Contains(tpSetTime))
                            this.tbMain.TabPages.Add(tpSetTime);
                    }

                    btn_SETtime.Visible=btn_SETtime.Enabled = write;
                    rb_Debug_Now.Enabled = rd_Degub_Manual.Enabled = write;

                    btn_GetTime.Visible = read;
                    break;
                case Clock.ClockSyncMethods:

                    if (read == false && write == false)
                        this.tbMain.TabPages.Remove(tpTimeMethods);
                    else
                    {
                        if (!this.tbMain.TabPages.Contains(tpTimeMethods))
                            this.tbMain.TabPages.Add(tpTimeMethods);
                    }
                    btnTimeMethodsInvoke.Visible = btnTimeMethodsInvoke.Enabled = write;
                    btnGetPresetAdjustTime.Visible = btnGetPresetAdjustTime.Enabled = write;
                    btnClockSynchronizationMethodSet.Visible = btnClockSynchronizationMethodSet.Enabled = write;
                    btnClockLimitSet.Visible = btnClockLimitSet.Enabled = write;
                    cmbTimeMethods.Visible = cmbTimeMethods.Enabled = write;
                    lblTimeMethod.Enabled = lblTimeMethod.Visible = write;

                    btnClockSyncMethodGet.Visible = btnClockSyncMethodGet.Enabled = read;
                    btnGetShiftRange.Visible = btnGetShiftRange.Enabled = read;
                    cmbClockSynchronizationMethod.Enabled = cmbClockSynchronizationMethod.Visible = read;
                    lblClockSyncMethod.Enabled = lblClockSyncMethod.Visible = read;
                    txtClockShiftLimit.Enabled = txtClockShiftLimit.Visible = read;
                    lblTimeShiftRange.Enabled = lblTimeShiftRange.Visible = read;

                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
