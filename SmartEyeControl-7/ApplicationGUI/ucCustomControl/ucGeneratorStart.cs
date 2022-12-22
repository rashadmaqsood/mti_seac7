using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharedCode.Comm.Param;
using SharedCode.Comm.HelperClasses;
using System.Globalization;
using SharedCode.Comm.DataContainer;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucGeneratorStart : UserControl
    {
        private const int MIN_TIME_VALUE = 0;
        private const int MAX_TIME_VAlUE = 5;
        private DateTime LIMIT_MT_Generator = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0,5,0);
        private Param_Generator_Start _paramGeneratorStart;

        #region Properties
        public Param_Generator_Start ParamGeneratorStart
        {
            get
            {
                return _paramGeneratorStart;
            }
        }

        #endregion

        #region Constructor
        public ucGeneratorStart()
        {
            InitializeComponent();

            _paramGeneratorStart = new Param_Generator_Start();
            this.Apply_toolTips();

            foreach (var item in Enum.GetNames(typeof(TariffEnum)))
                this.cmbTarrifOnGeneratorStart.Items.Add(item);

            if (this.cmbTarrifOnGeneratorStart.Items.Count > 0)
            {
                this.cmbTarrifOnGeneratorStart.SelectedIndex = 0;
            }
        }

        #endregion
        


        private void txt_MonitoringTime_GeneratorStart_Leave(object sender, EventArgs e)
        {
            if( (this.txt_MonitoringTime_GeneratorStart.Value.Minute > MAX_TIME_VAlUE) ||
                ((this.txt_MonitoringTime_GeneratorStart.Value.Minute == MAX_TIME_VAlUE) &&
                (this.txt_MonitoringTime_GeneratorStart.Value.Second > MIN_TIME_VALUE))
                
                )
            {
                this.txt_MonitoringTime_GeneratorStart.Value = LIMIT_MT_Generator;
            }

            _paramGeneratorStart.GeneratorStart_MonitoringTime = TimeSpan.ParseExact(this.txt_MonitoringTime_GeneratorStart.Text, ucMonitoringTime.format, CultureInfo.CurrentCulture);
        }

        private void Apply_toolTips()
        {
            try
            {
                toolTip.SetToolTip(txt_MonitoringTime_GeneratorStart, "Monitoring Time:Range[00:00--05:00]");
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Apply ToolTip for Generator_Start Elements", ex);
            }
        }

        internal void ShowToGUI(Param_Generator_Start paramGeneratorStart)
        {
            if(paramGeneratorStart != null)
            {
                this.txt_MonitoringTime_GeneratorStart.Value = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day, 
                    paramGeneratorStart.GeneratorStart_MonitoringTime.Hours, 
                    paramGeneratorStart.GeneratorStart_MonitoringTime.Minutes, 
                    paramGeneratorStart.GeneratorStart_MonitoringTime.Seconds);
                //this.txt_MonitoringTime_GeneratorStart.Value.Add(paramGeneratorStart.GeneratorStart_MonitoringTime);

                this.cmbTarrifOnGeneratorStart.SelectedIndex = (paramGeneratorStart.GeneratorStart_Tariff);
            }
        }

        private void cmbTarrifOnGeneratorStart_SelectedIndexChanged(object sender, EventArgs e)
        {
            _paramGeneratorStart.GeneratorStart_Tariff = (byte)(this.cmbTarrifOnGeneratorStart.SelectedIndex);
        }
    }
}
