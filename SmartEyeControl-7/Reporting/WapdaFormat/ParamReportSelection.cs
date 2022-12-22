using SharedCode.Comm.HelperClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartEyeControl_7.Reporting.WapdaFormat
{
    public partial class ParamReportSelection : Form
    {
        public int bParamRptType = 0;
        public ParamReportSelection(List<AccessRights> right )
        {
            InitializeComponent();
            bParamRptType = 0;

            rdbNormalModeReport.Visible = (right.Find(x => x.QuantityName.Equals(DisplayWindowsParams.DisplayWindowsNormal.ToString()) && x.Read) != null);
            rdbAlternateModeReport.Visible = (right.Find(x => x.QuantityName.Equals(DisplayWindowsParams.DisplayWindowsAlternate.ToString()) && x.Read) != null);
            rdbTestModeReport.Visible = (right.Find(x => x.QuantityName.Equals(DisplayWindowsParams.DisplayWindowsTest.ToString()) && x.Read) != null);
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            if (rdbProgrammingReport.Checked)
                bParamRptType = 1;
            else if (rdbNormalModeReport.Checked)
                bParamRptType = 2;
            else if (rdbAlternateModeReport.Checked)
                bParamRptType = 3;
            else if (rdbTestModeReport.Checked)
                bParamRptType = 4;
            else
                bParamRptType = 0;

            this.DialogResult = DialogResult.OK;
        }
    }
}
