using DLMS;
using DLMS.Comm;
using SharedCode.Comm.HelperClasses;
using SharedCode.Common;
using SharedCode.Controllers;
using System;
using System.ComponentModel;
using System.Configuration;
using System.IO.Ports;
using System.Windows.Forms;

namespace RelayTest
{
    public partial class frmMain : Form
    {
        #region Data Members

        string port;
        ApplicationController _appController;
        ConnectionController _connController;
        DeviceAssociation _association;
        BackgroundWorker _worker;
        Action_Result _actionResult;

        Timer _timer;
        bool _contactorOn;

        int _onTime;
        int _offTime;

        int _defaultOnTime;
        int _defaultOffTime;

        RequestType _requestType;
        int _meterType;

        DateTime _startTime;
        DateTime _stopTime;

        int _totalIterations;
        int _successIterations;
        int _failIterations;

        #endregion

        #region Constructor
        public frmMain()
        {
            InitializeComponent();
            this.cmbMeterType.SelectedIndex = 0;
            _appController = new ApplicationController();
            _connController = _appController.ConnectionController;
            _connController.SelectedMeter = Commons.GetDefaultMeterSetting();

            this._timer = new Timer();
            this._timer.Tick += _timer_Tick;

            this._defaultOnTime = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultONTime"].ToString());
            this._defaultOffTime = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultOFFTime"].ToString());

            this.dtpOnTime.Value = (DateTime.Now.Date).AddSeconds(this._defaultOnTime);
            this.dtpOffTime.Value = (DateTime.Now.Date).AddSeconds(this._defaultOffTime);

            this._onTime = 0;
            this._offTime = 0;

            this._totalIterations = 0;
            this._successIterations = 0;
            this._failIterations = 0;
        }

        #endregion

        #region Event Handlers

        private void _timer_Tick(object sender, EventArgs e)
        {
            _actionResult = Action_Result.Temporary_failure;
            this._timer.Enabled = false;
            ContactorOnOff();
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                this._startTime = DateTime.Now;
                if (this.btnConnect.Text.Equals("Connect"))
                {
                    this.UpdateLog("Send Meter Connect Request");
                    this._requestType = RequestType.Connect;
                }
                else
                {
                    this.UpdateLog("Send Meter Disconnect Request");
                    this._requestType = RequestType.Disconnect;
                }
                if (!this._worker.IsBusy)
                {
                    this._worker.RunWorkerAsync();
                }
                else
                {
                    this.UpdateLog("Worker is busy Please wait for a while");
                }
            }
            catch (Exception ex)
            {
                this.UpdateLog(ex.Message);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            string[] serialPorts = SerialPort.GetPortNames();

            cmbPorts.Items.Clear();
            try
            {
                foreach (var port in serialPorts)
                {
                    cmbPorts.Items.Add(port);
                }
                if (cmbPorts.Items.Count > 0)
                    cmbPorts.SelectedIndex = 0;

                this.InitWorkers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            port = cmbPorts.SelectedItem.ToString();
        }

        private void btnTimerStart_Click(object sender, EventArgs e)
        {
            if (this._connController.IsConnected)
            {
                if (this.btnTimerStart.Text.Equals("Start"))
                {
                    _onTime = Convert.ToInt32(this.dtpOnTime.Value.TimeOfDay.TotalMilliseconds);
                    _offTime = Convert.ToInt32(this.dtpOffTime.Value.TimeOfDay.TotalMilliseconds);

                    if ( _onTime > 0 && _offTime > 0 )
                    {
                        if (_contactorOn)
                        {
                            this._timer.Interval = _offTime;// * 1000;
                        }
                        else
                        {
                            this._timer.Interval = _onTime;// * 1000;
                        }
                        this._timer.Enabled = true;
                        this.btnTimerStart.Text = "Stop";
                        this.dtpOffTime.Enabled = false;
                        this.dtpOnTime.Enabled = false;

                        this._totalIterations = 0;
                        this._successIterations = 0;
                        this._failIterations = 0;

                        this.lblTotalItrations.Text = this._totalIterations.ToString();
                        this.lblSuccess.Text = this._successIterations.ToString();
                        this.lblFail.Text = this._failIterations.ToString();

                        this._contactorOn = this._appController.Param_Controller.GET_Relay_Status(); 
                        if(this._contactorOn)
                        {
                            this._requestType = RequestType.Contactor_OFF;
                        }
                        else
                        {
                            this._requestType = RequestType.Contactor_ON;
                        }

                        this.ContactorOnOff();

                    }
                    else
                    {
                        MessageBox.Show("Please Chose valid ON Time OR OFF Time Value");
                    }
                }
                else
                {
                    SaveConfigs();
                    EnableDisableGUI();
                }
            }
            else
            {
                MessageBox.Show("Please Connect Meter First");
            }
        }

        private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_requestType == RequestType.Connect)
            {
                this.ConnectComplete();
            }
            else if (_requestType == RequestType.Disconnect)
            {
                this.DisconnectComplete();
            }
            else if (_requestType == RequestType.Contactor_ON)
            {
                this.ContactorOnComplete();
            }
            else if (_requestType == RequestType.Contactor_OFF)
            {
                this.ContactorOFFComplete();
            }
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (_requestType == RequestType.Connect)
            {
                this.MeterConnect();
            }
            else if (_requestType == RequestType.Disconnect)
            {
                this.MeterDisconnect();
            }
            else if (_requestType == RequestType.Contactor_ON)
            {
                this.ContactorON();
            }
            else if (_requestType == RequestType.Contactor_OFF)
            {
                this.ContactorOFF();
            }
        }

        private void rtbLog_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            rtbLog.SelectionStart = rtbLog.Text.Length;
            // scroll it automatically
            rtbLog.ScrollToCaret();
        }

        private void cmbMeterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._meterType = this.cmbMeterType.SelectedIndex;
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            this.rtbLog.Text = "";
        }

        #endregion

        #region Private Methods

        void SaveConfigs()
        {
            this._defaultOnTime = (int)this.dtpOnTime.Value.TimeOfDay.TotalSeconds;
            this._defaultOffTime = (int)this.dtpOffTime.Value.TimeOfDay.TotalSeconds;

            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings["DefaultONTime"].Value = this._defaultOnTime.ToString();
            config.AppSettings.Settings["DefaultOFFTime"].Value = this._defaultOffTime.ToString();
            config.Save(ConfigurationSaveMode.Full,true);
        }

        void ContactorOnOff()
        {
            if (this._connController.IsConnected)
            {
                this._startTime = DateTime.Now;
                this._totalIterations++;
                this.lblTotalItrations.Text = this._totalIterations.ToString();
                if (!this._contactorOn)
                {

                    this.UpdateLog("Contactor On Request");
                    this._requestType = RequestType.Contactor_ON;
                }
                else if (this._contactorOn)
                {
                    this.UpdateLog("Contactor OFF Request");
                    this._requestType = RequestType.Contactor_OFF;
                }
                this._worker.RunWorkerAsync(); 
            }
            else
            {
                this.UpdateLog("Meter is dosconnected");
            }
        }

        void InitWorkers()
        {
            this._worker = new BackgroundWorker();
            this._worker.DoWork += _worker_DoWork; ;
            this._worker.RunWorkerCompleted += _worker_RunWorkerCompleted;
        }

        void UpdateLog(string message)
        {
            this.rtbLog.AppendText(string.Format("{0} : {1} \n",DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss.fff"),message));
        }

        void DisconnectComplete()
        {
            if (!this._connController.IsConnected)
            {
                this.btnConnect.Text = "Connect";
                this._stopTime = DateTime.Now;
                TimeSpan span = this._stopTime - this._startTime;
                int ms = (int)span.TotalMilliseconds;
                this.UpdateLog(string.Format("Meter Disconnect Successfully!  Total Time : {0} miliseconds",ms));
                //this.UpdateLog($"Meter Disconnect Successfully!  Total Time : {ms} miliseconds");
                this.EnableDisableGUI();
            }
        }

        void MeterDisconnect()
        {
            try
            {
                _connController.Disconnect();
            }
            catch (Exception ex)
            {
                this.UpdateLog(ex.Message);
            }
        }

        void ConnectComplete()
        {
            if (this._connController.IsConnected)
            {
                this.btnConnect.Text = "Disconnect";
                this._stopTime = DateTime.Now;
                TimeSpan span = this._stopTime - this._startTime;
                int ms = (int)span.TotalMilliseconds;
                this.UpdateLog(string.Format("Meter Connect Successfully!  Total Time : {0} miliseconds",ms));
                //this.UpdateLog($"Meter Connect Successfully!  Total Time : {ms} miliseconds");
            }
        }

       void MeterConnect()
        {
            try
            {
                if (this._meterType == 0)
                {
                    _association = new DeviceAssociation()
                    {
                        AuthenticationType = HLS_Mechanism.HLS_GMAC,
                        ClientSap = 1,
                        MeterSap = 1
                    };
                    _connController.SecurityData = Commons.DefaultSecurityData();
                    _connController.SelectedMeter.Device_Association = _association;
                   // _connController.Connect_IRLinkSimple("", port, _association);

                }
                else if (this._meterType == 1)
                {
                    _association = new DeviceAssociation()
                    {
                        AuthenticationType = HLS_Mechanism.LowSec,
                        ClientSap = 1,
                        MeterSap = 1
                    };
                    _connController.SelectedMeter.Device_Association = _association;
                    //_connController.Connect_IRLinkSimple("microtek", port, _association);

                }
            }
            catch (Exception ex)
            {
                this.UpdateLog(ex.Message);
            }
        }

        void ContactorON()
        {
            try
            {
                _actionResult = _appController.Param_Controller.RelayConnectRequest();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void ContactorOnComplete()
        {
            if (_actionResult != Action_Result.Success)
            {
                this._failIterations++;
                this.lblFail.Text = this._failIterations.ToString();
            }
            else
            {
                this._contactorOn = true;
                this._timer.Interval = this._onTime;
                this._stopTime = DateTime.Now;
                TimeSpan span = this._stopTime - this._startTime;
                int ms = (int)span.TotalMilliseconds;
                this.UpdateLog(string.Format("Contactor On Successfully!  Total Time : {0} miliseconds",ms));
                //this.UpdateLog($"Contactor On Successfully!  Total Time : {ms} miliseconds");
                this._successIterations++;
                this.lblSuccess.Text = this._successIterations.ToString();
            }
            this._timer.Enabled = true;
        }

        void ContactorOFF()
        {
            try
            {
                _actionResult = _appController.Param_Controller.RelayDisConnectRequest();
            }
            catch (Exception ex)
            {
                this.UpdateLog(ex.Message);
            }
        }

        void EnableDisableGUI()
        {
            this.dtpOnTime.Enabled = true;
            this.dtpOffTime.Enabled = true;
            this.btnTimerStart.Text = "Start";
            this._timer.Enabled = false;
        }
        void ContactorOFFComplete()
        {
            if (_actionResult != Action_Result.Success)
            {
                this._failIterations++;
                this.lblFail.Text = this._failIterations.ToString();
                this.UpdateLog("Contactor Failure.");
            }
            else
            {
                this._contactorOn = false;
                this._timer.Interval = this._offTime;
                this._stopTime = DateTime.Now;
                TimeSpan span = this._stopTime - this._startTime;
                int ms = (int)span.TotalMilliseconds;
                this.UpdateLog(string.Format("Contactor OFF Successfully!  Total Time : {0} miliseconds", ms));
                //this.UpdateLog($"Contactor OFF Successfully!  Total Time : {ms} miliseconds");
                this._successIterations++;
                this.lblSuccess.Text = this._successIterations.ToString();
            }
            this._timer.Enabled = true;
        }
        #endregion
    }
}
