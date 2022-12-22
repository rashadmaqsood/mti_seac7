using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharedCode.Comm.HelperClasses;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucHDLCSetup : UserControl
    {
        public ucHDLCSetup()
        {
            InitializeComponent();
        }

        #region AccessControlMethods

        public bool ApplyAccessRights(List<AccessRights> Rights)
        {
            bool isSuccess = false;
            try
            {
                this.btnGetParameter.Visible = this.btnSetDeviceAddress.Visible =
                    this.btnSetInactivityTimeOut.Visible = false;
                this.SuspendLayout();
                if (Rights is null)
                    return false;

                if (Rights.Find(x => x.Read == true || x.Write == true) != null)
                {
                    foreach (var item in Rights)
                    {
                        _HelperAccessRights((Modem_HDLCSetup)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
                    }
                    isSuccess = true;
                }
            }
            finally
            {
                this.ResumeLayout();
            }
            return isSuccess;
        }

        private void _HelperAccessRights(Modem_HDLCSetup qty, bool read, bool write)
        {
            switch (qty)
            {
                case Modem_HDLCSetup.Get_All:
                    if(read)
                    {
                        this.btnGetParameter.Visible = true;
                    }
                    else
                    {
                        this.btnGetParameter.Visible = false;
                    }
                    break;
                case Modem_HDLCSetup.Set_Device_Address:
                    if(write)
                    {
                        this.btnSetDeviceAddress.Visible = true;
                    }
                    else
                    {
                        this.btnSetDeviceAddress.Visible = false;
                    }
                    break;
                case Modem_HDLCSetup.Set_Inactivity_Timeout:
                    if (write)
                    {
                        this.btnSetInactivityTimeOut.Visible = true;
                    }
                    else
                    {
                        this.btnSetInactivityTimeOut.Visible = false;
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
