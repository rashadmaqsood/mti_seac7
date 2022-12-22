using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.Param;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucGeneralProcess : UserControl
    {
        public ucGeneralProcess()
        {
            InitializeComponent();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_Generel_Process Obj_General_Process { get; set; }

        private void chkSvSControl_CheckedChanged(object sender, EventArgs e)
        {
            if (Obj_General_Process != null)
            {
                Obj_General_Process.IsSVS = chkSvSControl.Checked;
            }
            else 
            {
                throw new Exception("Param General Process is Not Set");
            }
            
        }

        public void Show_To_GUI() 
        {
            if (Obj_General_Process != null)
            {
                chkSvSControl.Checked = Obj_General_Process.IsSVS;
            }
            else 
            {
                throw new Exception("Param General Process is Not Set");
            }
        }

        #region AccessControlMethods

        public bool ApplyAccessRights(List<AccessRights> Rights)
        {
            bool isSuccess = false;
            try
            {
                this.SuspendLayout();
                var AccessRight = Rights.Find((x)=>  String.Equals(x.QuantityName,Misc.GernalProcessParameter.ToString(),StringComparison.OrdinalIgnoreCase));

                if (AccessRight != null &&  (AccessRight.Read == true || AccessRight.Write == true))
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
            if (qty == Misc.GernalProcessParameter)
            {
                chkSvSControl.Visible = (read || write);
                chkSvSControl.Enabled = write;
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
