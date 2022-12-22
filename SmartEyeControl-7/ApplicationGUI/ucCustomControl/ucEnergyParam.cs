using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.Param;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucEnergyParam : UserControl
    {
        private Param_Energy_Parameter _Param_energy_parameters_object = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_Energy_Parameter Param_energy_parameters_object
        {
            get { return _Param_energy_parameters_object; }
            set { _Param_energy_parameters_object = value; }
        }

        public ucEnergyParam()
        {
            InitializeComponent();
        }

        #region Energy_params_leave_events

        private void chk_EnergyParam_Quadrant1_CheckedChanged(object sender, EventArgs e)
        {
            _Param_energy_parameters_object.Quad1 = chk_EnergyParam_Quadrant1.Checked;
        }

        private void chk_EnergyParam_Quadrant2_CheckedChanged(object sender, EventArgs e)
        {
            _Param_energy_parameters_object.Quad2 = chk_EnergyParam_Quadrant2.Checked;
        }

        private void chk_EnergyParam_Quadrant3_CheckedChanged(object sender, EventArgs e)
        {
            _Param_energy_parameters_object.Quad3 = chk_EnergyParam_Quadrant3.Checked;
        }

        private void chk_EnergyParam_Quadrant4_CheckedChanged(object sender, EventArgs e)
        {
            _Param_energy_parameters_object.Quad4 = chk_EnergyParam_Quadrant4.Checked;
        }

        #endregion

        public void showToGUI_EnergyParam()
        {
            chk_EnergyParam_Quadrant1.Checked = Param_energy_parameters_object.Quad1;
            chk_EnergyParam_Quadrant2.Checked = Param_energy_parameters_object.Quad2;
            chk_EnergyParam_Quadrant3.Checked = Param_energy_parameters_object.Quad3;
            chk_EnergyParam_Quadrant4.Checked = Param_energy_parameters_object.Quad4;

        }

        #region AccessControlMethods

        public bool ApplyAccessRights(List<AccessRights> Rights)
        {
            bool isSuccess = false;
            try
            {
                this.SuspendLayout();
                var AccessRight = Rights.Find((x) => String.Equals(x.QuantityName, Misc.EnergyParam.ToString(),
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
            if (qty == Misc.EnergyParam)
            {
                chk_EnergyParam_Quadrant1.Visible = chk_EnergyParam_Quadrant2.Visible = chk_EnergyParam_Quadrant3.Visible =
                    chk_EnergyParam_Quadrant4.Visible = (read || write);

                chk_EnergyParam_Quadrant1.Enabled = chk_EnergyParam_Quadrant2.Enabled = chk_EnergyParam_Quadrant3.Enabled =
                    chk_EnergyParam_Quadrant4.Enabled = write;
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
