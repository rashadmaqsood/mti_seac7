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
    public partial class ucStandardModemParameters : UserControl
    {
        public ucStandardModemParameters()
        {
            InitializeComponent();
        }

        #region AccessControlMethods

        public bool ApplyAccessRights(List<AccessRights> Rights)
        {
            bool isSuccess = false;
            try
            {
                if(Rights is null)
                {
                    this.tbStandardModem.TabPages.Remove(tpIpProfile);
                    this.tbStandardModem.TabPages.Remove(tpNumberProfile);
                    this.tbStandardModem.TabPages.Remove(tpKeepAlive);
                    return false;
                }
                this.SuspendLayout();

                if (Rights.Find(x => x.Read == true || x.Write == true) != null)
                {
                    foreach (var item in Rights)
                    {
                        _HelperAccessRights((StandardModem)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
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

        private void _HelperAccessRights(StandardModem qty, bool read, bool write)
        {
            switch (qty)
            {
                case StandardModem.StandardIPProfile:
                    if (!(read || write))
                        this.tbStandardModem.TabPages.Remove(tpIpProfile);
                    else
                    {
                        if(!this.tbStandardModem.TabPages.Contains(tpIpProfile))
                            this.tbStandardModem.TabPages.Add(tpIpProfile);
                    }
                    break;
                case StandardModem.StandardNumberProfile:
                    if (!(read || write))
                        this.tbStandardModem.TabPages.Remove(tpNumberProfile);
                    else
                    {
                        if (!this.tbStandardModem.TabPages.Contains(tpNumberProfile))
                            this.tbStandardModem.TabPages.Add(tpNumberProfile);
                    }
                    break;
                case StandardModem.StandardKeepAlive:
                    if (!(read || write))
                        this.tbStandardModem.TabPages.Remove(tpKeepAlive);
                    else
                    {
                        if (!this.tbStandardModem.TabPages.Contains(tpKeepAlive))
                            this.tbStandardModem.TabPages.Add(tpKeepAlive);
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
