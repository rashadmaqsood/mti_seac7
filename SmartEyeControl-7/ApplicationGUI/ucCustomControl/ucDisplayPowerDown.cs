using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.Param;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucDisplayPowerDown : UserControl
    {
        private Param_Display_PowerDown _obj_Param_Display_PowerDown;

        public ucDisplayPowerDown()
        {
            InitializeComponent();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Param_Display_PowerDown Obj_Param_Display_PowerDown
        {
            get { return _obj_Param_Display_PowerDown; }
            set { _obj_Param_Display_PowerDown = value; }
        }

        private void ucDisplayPowerDown_Load(object sender, EventArgs e)
        {

        }

        private void ucDisplayPowerDown_Click(object sender, EventArgs e)
        {
            if (Obj_Param_Display_PowerDown != null)
            {
                Obj_Param_Display_PowerDown.IsOnMinute_Sec = rdbOnMin.Checked;
            }
            else
            {
                throw new Exception("Display Power Down Object is not Initialized");
            }
        }

        private void rdbOffMin_Click(object sender, EventArgs e)
        {
            if (Obj_Param_Display_PowerDown != null)
            {
                Obj_Param_Display_PowerDown.IsOffMinute_Sec = rdbOffMin.Checked;
            }
            else
            {
                throw new Exception("Display Power Down Object is not Initialized");
            }

        }

        private void chkOnTimeModeScrollCycle_CheckedChanged(object sender, EventArgs e)
        {
            if (Obj_Param_Display_PowerDown != null)
            {
                if (!chkAlwaysOn.Checked)
                {
                    chkOnTimeModeScrollCycle.Enabled = chkImmediatelyOff.Enabled = chkDisplayRepeat.Enabled = chkDisplayOnByButton.Enabled = true;
                    Obj_Param_Display_PowerDown.IsImmidateOff = chkImmediatelyOff.Checked;
                    Obj_Param_Display_PowerDown.IsDisplayOnByButton = chkDisplayOnByButton.Checked;
                    Obj_Param_Display_PowerDown.IsDisplayRepeat = chkDisplayRepeat.Checked;
                    Obj_Param_Display_PowerDown.IsOnTimeCycleScroll = chkOnTimeModeScrollCycle.Checked;
                    Obj_Param_Display_PowerDown.IsAlwaysOn = chkAlwaysOn.Checked;
                }
                else
                {
                    Obj_Param_Display_PowerDown.IsAlwaysOn = chkAlwaysOn.Checked;
                    chkImmediatelyOff.Enabled = Obj_Param_Display_PowerDown.IsImmidateOff = chkImmediatelyOff.Checked = false;
                    chkDisplayOnByButton.Enabled = Obj_Param_Display_PowerDown.IsDisplayOnByButton = chkDisplayOnByButton.Checked = false;
                    chkDisplayRepeat.Enabled = Obj_Param_Display_PowerDown.IsDisplayRepeat = chkDisplayRepeat.Checked = false;
                    chkOnTimeModeScrollCycle.Enabled = Obj_Param_Display_PowerDown.IsOnTimeCycleScroll = chkOnTimeModeScrollCycle.Checked = false;
                }
            }
            else
            {
                throw new Exception("Display Power Down Object is not Initialized");
            }
        }

        private void nudOffDelay_ValueChanged(object sender, EventArgs e)
        {
            if (Obj_Param_Display_PowerDown != null)
            {
                Obj_Param_Display_PowerDown.OffDelay = Convert.ToByte(nudOffDelay.Value);
                Obj_Param_Display_PowerDown.OffTime = Convert.ToByte(nudOffTime.Value);
                Obj_Param_Display_PowerDown.OnTime = Convert.ToByte(nudOnTime.Value);
            }
            else
            {
                throw new Exception("Display Power Down Object is not Initialized");
            }
        }

        public void Attach_Handlers()
        {
            //Re attach Events 
            nudOffDelay.ValueChanged += nudOffDelay_ValueChanged;
            nudOnTime.ValueChanged += nudOffDelay_ValueChanged;
            nudOffTime.ValueChanged += nudOffDelay_ValueChanged;
            chkAlwaysOn.CheckedChanged += chkOnTimeModeScrollCycle_CheckedChanged;
            chkDisplayOnByButton.CheckedChanged += chkOnTimeModeScrollCycle_CheckedChanged;
            chkDisplayRepeat.CheckedChanged += chkOnTimeModeScrollCycle_CheckedChanged;
            chkImmediatelyOff.CheckedChanged += chkOnTimeModeScrollCycle_CheckedChanged;
            chkOnTimeModeScrollCycle.CheckedChanged += chkOnTimeModeScrollCycle_CheckedChanged;
            rdbOffMin.Click += rdbOffMin_Click;
            rdbOnMin.Click += rdbOffMin_Click;
            rdbOnSec.Click += rdbOffMin_Click;
            rdbOffSec.Click += rdbOffMin_Click;
        }

        public void Deattach_Handlers()
        {
            //De Attach Events
            nudOffDelay.ValueChanged -= nudOffDelay_ValueChanged;
            nudOnTime.ValueChanged -= nudOffDelay_ValueChanged;
            nudOffTime.ValueChanged -= nudOffDelay_ValueChanged;
            chkAlwaysOn.CheckedChanged -= chkOnTimeModeScrollCycle_CheckedChanged;
            chkDisplayOnByButton.CheckedChanged -= chkOnTimeModeScrollCycle_CheckedChanged;
            chkDisplayRepeat.CheckedChanged -= chkOnTimeModeScrollCycle_CheckedChanged;
            chkImmediatelyOff.CheckedChanged -= chkOnTimeModeScrollCycle_CheckedChanged;
            chkOnTimeModeScrollCycle.CheckedChanged -= chkOnTimeModeScrollCycle_CheckedChanged;
            rdbOffMin.Click -= rdbOffMin_Click;
            rdbOnMin.Click -= rdbOffMin_Click;
            rdbOnSec.Click -= rdbOffMin_Click;
            rdbOffSec.Click -= rdbOffMin_Click;
        }

        public void Show_To_GUI()
        {
            try
            {
                Deattach_Handlers();
                // init Values
                nudOffDelay.Value = Obj_Param_Display_PowerDown.OffDelay;
                nudOffTime.Value = Obj_Param_Display_PowerDown.OffTime;
                nudOnTime.Value = Obj_Param_Display_PowerDown.OnTime;
                rdbOffMin.Checked = Obj_Param_Display_PowerDown.IsOffMinute_Sec;
                rdbOnMin.Checked = Obj_Param_Display_PowerDown.IsOnMinute_Sec;
                chkAlwaysOn.Checked = Obj_Param_Display_PowerDown.IsAlwaysOn;
                chkImmediatelyOff.Checked = Obj_Param_Display_PowerDown.IsImmidateOff;
                chkDisplayOnByButton.Checked = Obj_Param_Display_PowerDown.IsDisplayOnByButton;
                chkDisplayRepeat.Checked = Obj_Param_Display_PowerDown.IsDisplayRepeat;
                chkOnTimeModeScrollCycle.Checked = Obj_Param_Display_PowerDown.IsOnTimeCycleScroll;
            }
            catch (Exception)
            {
                throw new Exception("Error updating Display Power Down GUI ");
            }
            finally
            {
                Attach_Handlers();
            }
        }

        #region AccessControlMethods

        public bool ApplyAccessRights(List<AccessRights> Rights)
        {
            bool AnyReadOrWrite = false;
            try
            {
                this.SuspendLayout();

                if (Rights.Find(x => x.Read == true || x.Write == true) != null)
                {
                    foreach (var item in Rights)
                    {
                        _HelperAccessRights((DisplayPowerDownMode)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
                        if (!AnyReadOrWrite)
                            AnyReadOrWrite = (item.Read || item.Write);
                    }
                    return AnyReadOrWrite;
                }
                return AnyReadOrWrite;
            }
            finally
            {
                this.ResumeLayout();
            }
        }

        private void _HelperAccessRights(DisplayPowerDownMode qty, bool read, bool write)
        {
            switch (qty)
            {
                case DisplayPowerDownMode.OffDelay:

                    nudOffDelay.Enabled = write;
                    gp_OffDelay.Visible = read;

                    break;
                case DisplayPowerDownMode.OnTime:

                    nudOnTime.Enabled = write;
                    rdbOnSec.Enabled = write;
                    rdbOnMin.Enabled = write;

                    gp_OnTime.Visible = read;
                    
                    break;
                case DisplayPowerDownMode.OffTime:

                    nudOffTime.Enabled = write;
                    rdbOffSec.Enabled = write;
                    rdbOffMin.Enabled = write;

                    gp_OffTime.Visible = read;

                    break;
                case DisplayPowerDownMode.PowerDownDisplayFlags:

                    chkAlwaysOn.Enabled = write;
                    chkImmediatelyOff.Enabled = write;
                    chkDisplayOnByButton.Enabled = write;
                    chkOnTimeModeScrollCycle.Enabled = write;
                    chkDisplayRepeat.Enabled = write;

                    gp_PWD_Flags.Visible = read;

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
