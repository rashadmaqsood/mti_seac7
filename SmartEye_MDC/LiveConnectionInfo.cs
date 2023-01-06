using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using comm;
using System.Text.RegularExpressions;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.DataContainer;
using SharedCode.Common;
using Communicator.comm;
using Communicator.MTI_MDC;

namespace Communicator
{
    public partial class LiveConnectionInfo : Form
    {
        #region Data Members

        Timer timer;
        private bool IsWindowRegister = false;

        #endregion

        #region Properties
        //================================================
        public IOConnection Connection { get; set; }
        //================================================
        public IOConnection TempConnection { get; set; }
        //================================================
        private bool IsRunLog { get; set; }
        //================================================
        public ConnectionManager ConManager { get; set; }
        //================================================
        public string MSN { get; set; }
        //================================================
        #endregion

        #region Constructor

        public LiveConnectionInfo(IOConnection con, ConnectionManager manager)
        {
            try
            {
                InitializeComponent();
                Connection = con;
                ConManager = manager;
                ConManager.NewConnectionAdded += new ConnectionCreated(ConManager_NewConnectionAdded);
                Connection.CurrentMeterLog += new OnChangeStatus(Connection_CurrentMeterLog);
                SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
                lstLog.Columns[0].Width = 165;
                lstLog.Columns[1].Width = lstLog.Width - 160;
                MSN = string.Empty;
                //lblDuration.Text = Connection.ConnectionDuration.ToString("c");
                //lblModel.Text = Connection.ConnectionInfo.MeterInfo.Model;
                //lblType.Text = Connection.ConnectionInfo.ConnectionType.ToString();
                ListViewHelper.EnableDoubleBuffer(lstLog);
                ListViewHelper.SetExtendedStyle(lstLog, ListViewExtendedStyles.DoubleBuffer);
                IsRunLog = true;
                timer = new Timer();
                timer.Interval = 300000;
                timer.Tick += timer_Tick;
            }
            catch (Exception ex)
            {

                LocalCommon.LogMDCExceptionIntoFile(ex);
            }

        }

        #endregion

        #region Events & Methods

        //================================================================================
        void ConManager_NewConnectionAdded(IOConnection con)
        {
            try
            {
                if (IsWindowRegister && !Connection.IsConnected && MSN != string.Empty && this.IsHandleCreated)
                {
                    var obj_new_con = ConManager.IOConnectionsList[MSN];
                    if (obj_new_con != null && obj_new_con.MSN == MSN)
                    {
                        try
                        {
                            Connection.CurrentMeterLog -= Connection_CurrentMeterLog;
                            Connection = null;
                            Connection = obj_new_con;
                            Connection.CurrentMeterLog += new OnChangeStatus(Connection_CurrentMeterLog);
                            Connection.Tag = this;
                            
                            
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else
                    {
                        if (con == null) return;
                        TempConnection = con;
                        TempConnection.PropertyChanged += new PropertyChangedEventHandler(Value_PropertyChanged);
                    }
                }
            }
            catch (Exception ex)
            {

                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }
        //================================================================================
        void Value_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                if (e.PropertyName == "MeterSerialNumberObj" && this.IsHandleCreated)
                {
                    var tempCon = ConManager.IOConnectionsList.FirstOrDefault(x => x.Value.MSN == MSN);
                    if (tempCon.Value != null && Connection.MSN == MSN)
                    {
                        Connection.CurrentMeterLog -= Connection_CurrentMeterLog;
                        Connection = null;
                        Connection = tempCon.Value;
                        Connection.Tag = this;
                        Connection.CurrentMeterLog += new OnChangeStatus(Connection_CurrentMeterLog);
                        TempConnection.PropertyChanged -= Value_PropertyChanged;
                    }
                }
            }
            catch (Exception ex)
            {
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }
        //================================================================================
        void Connection_CurrentMeterLog(string message, MeterSerialNumber msn)
        {
            try
            {
                if (IsRunLog && Connection.IsChannelConnected && Connection.IsConnected && this.IsHandleCreated || Connection.CancelationRequest)
                {
                    this.Invoke(new Action<string, MeterSerialNumber>(Connection_CurrentMeterLog_helper), message, msn);
                }
            }
            catch (Exception e)
            {
                LocalCommon.LogMDCExceptionIntoFile(e);
            }
        }
        //================================================================================
        void Connection_CurrentMeterLog_helper(string message, MeterSerialNumber msn)
        {
            try
            {
                if (this.IsHandleCreated)
                {
                    
                    MSN = (msn != null) ? msn.ToString() : "-----------";
                    var duration = Connection.ConnectionDuration.ToString("c");
                    var model = Connection.ConnectionInfo.MeterInfo.MeterModel;
                    var type = Connection.ConnectionInfo.ConnectionType.ToString();
                    this.Text = "MSN: " + MSN + " | " + model + " | " + type + " | " + duration;
                    AddMessage(message);
                    SetTimer(0);
                    Application.DoEvents();
                }
            }
            catch (Exception e)
            {
                LocalCommon.LogMDCExceptionIntoFile(e);
            }
        }
        //================================================================================
        private void AddMessage(string message)
        {
            try
            {
                if (this.IsHandleCreated)
                {
                    if (lstLog.Items.Count > 50) lstLog.Items.Clear();
                    
                    var item1 = new ListViewItem();
                    if (Regex.IsMatch(message, @"(PVLO|PBLO)")) item1.ForeColor = Color.FromArgb(79, 167, 247);
                    if (Regex.IsMatch(message, @"(PVLI|PBLI)")) item1.ForeColor = Color.FromArgb(0, 215, 97);
                    if (Regex.IsMatch(message, @"(\sF\s)|(PF|FCL)")) item1.ForeColor = Color.Red;
                    if (message.ToUpper().Contains("CDD")) SetTimer(60000);

                    //item1.Font = new System.Drawing.Font("courier new", 7.5f);
                    item1.Text = DateTime.Now.ToString();
                    item1.SubItems.Add(message);

                    //item1.Focused = true;
                    lstLog.BeginUpdate();
                    lstLog.Items.Add(item1);
                    lstLog.EndUpdate();
                    lstLog.FocusedItem = item1;
                    lstLog.TopItem = item1;
                }
            }
            catch (Exception e)
            {
                LocalCommon.LogMDCExceptionIntoFile(e);
            }
        }
        //================================================================================
        private void LiveConnectionInfo_Load(object sender, EventArgs e)
        {
            try
            {
                timer.Start();
                MSN = Connection.MSN;
                AddMessage(Connection.MeterLiveLog);
            }
            catch (Exception ex)
            {
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }
        //================================================================================
        private void LiveConnectionInfo_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                lstLog.Columns[0].Width = 165;
                lstLog.Columns[1].Width = lstLog.Width - 160;
            }
            catch (Exception ex)
            {
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }
        //================================================================================
        private void btnPause_Click(object sender, EventArgs e)
        {
            try
            {
                btnPause.Text = (IsRunLog) ? "Play" : "Pause";
                IsRunLog = (IsRunLog) ? false : true;
            }
            catch (Exception ex)
            {
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }
        //================================================================================
        void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (IsRunLog && !IsWindowRegister)
                {
                    this.Close();
                }
                else
                {
                    ConManager_NewConnectionAdded(null);
                }
            }
            catch (Exception ex)
            {
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }
        //================================================================================
        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsWindowRegister)
                {
                    IsWindowRegister = true;
                    btnRegister.Text = "Un-register MSN";
                    SetTimer(10000);
                }
                else
                {
                    IsWindowRegister = false;
                    btnRegister.Text = "Register To MSN";
                    SetTimer(300000);

                }
            }
            catch (Exception ex)
            {
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }
        //================================================================================
        private void SetTimer(int interval)
        {
            timer.Stop();
            if (interval > 0) timer.Interval = interval;
            timer.Start();
        }
        //================================================================================
        private void btnKillConnection_Click(object sender, EventArgs e)
        {
            try
            {
                var rslt = MessageBox.Show("Are you sure to kill the connection", "Connection", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (rslt == System.Windows.Forms.DialogResult.Yes)
                {
                    AddMessage(string.Format("{0,-8}{1,-2}", "FCL", "R"));
                    if (Connection.TCPWrapperStream.CommunicationMode == SharedCode.TCP_Communication.CommunicationMode.IdleAliveMode)
                    {
                        Connection.Disconnect();
                    }
                    else
                    {
                        Connection.CancelationRequest = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }
        //================================================================================
        #endregion

        #region Destructor

        ~LiveConnectionInfo()
        {
            try
            {
                this.Dispose(false);
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

       
    }
}
