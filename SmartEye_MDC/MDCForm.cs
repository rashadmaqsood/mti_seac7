//Timer is disabled for license validation

using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using comm;
using Commuincator.MeterConnManager;
using DatabaseManager.Database;
using DLMS;
using System.IO;
using Communicator.MTI_MDC;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Communicator.Properties;
using System.Reflection;
using System.Threading.Tasks;
using LogSystem.Shared.Common.Enums;
using SharedCode.Comm.DataContainer;
using SharedCode.Common;

namespace Communicator
{
    public partial class MDCForm : Form
    {
        #region Members

        public static bool StartServer = true;
        private MDC mdcServer = null;
        DateTime startTime = DateTime.Now;
        DateTime stopTime;
        DatabaseController DBController;
        Stats stats_obj = null;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int SW_MINIMIZE = 6;
        private const int SW_MAXIMIZE = 3;
        private const int SW_RESTORE = 9;
        private AllMeterscs MeterList;

        #endregion

        #region Constructor

        public MDCForm()
        {
            try
            {
                InitializeComponent();
                this.Location = new Point(830, 175);
                // Object Initial Work
                mdcServer = new MDC();
                DBController = new DatabaseController();
            }
            catch (Exception ex)
            {
                LocalCommon.SaveApplicationException(new Exception("Main MDCForm"), 1);
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }

        #endregion

        #region Properties
        public MDC MDCServer
        {
            get { return mdcServer; }
            set { mdcServer = value; }
        }

        #endregion

        #region MDC_Status

        public void showStatistics()
        {
            try
            {
                //if (stats_obj != null)
                //{
                //    //Max transactions
                //    long x = MDCServer.MeterConnectionManager.ThreadAllocator.MDC_Status_Obj.KA_Expected_Transactions + MDCServer.MeterConnectionManager.ThreadAllocator.MDC_Status_Obj.NKA_Expected_Transactions;
                //    lbl_transactions.Text = x.ToString();

                //    //Max duration
                //    lbl_max_duration.Text = GetDurationInString(stats_obj.Max_duration);

                //    //Min duration
                //    lbl_min_duration.Text = GetDurationInString(stats_obj.Min_duration);

                //    //Average duration
                //    lbl_avg_duration.Text = GetDurationInString(stats_obj.Avg_duration);

                //    ///STD Duration
                //    lblStdDur.Text = GetDurationInString(stats_obj.Avg_duration + stats_obj.STD_duration);

                //    //Max Meters allocated
                //    lbl_max_meters_allocated.Text = stats_obj.Max_Meters_Allocated.ToString();


                //}
                //else
                //{
                //    lbl_avg_duration.Text = "0";
                //    lbl_max_duration.Text = "0";
                //    lbl_max_meters_allocated.Text = "0";
                //    lbl_min_duration.Text = "0";
                //    lbl_transactions.Text = "0";
                //    lblStdDur.Text = "0";
                //}
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string GetDurationInString(double Value)
        {
            string toReturn = null;
            int hour = ((int)Value) / 3600;
            Value = Value - (3600 * hour);
            int minute = ((int)Value) / 60;
            Value = Value - (60 * minute);
            toReturn = hour.ToString("D2") + ":" + minute.ToString("D2") + ":" + Value.ToString("F3");
            return toReturn;
        }

        public void Show_MDC_Status_to_GUI()
        {
            try
            {
                if (!MDCServer.IsServerRunning)
                    return;
                //lblkam_count.Text = MDCServer.MeterConnectionManager.ThreadAllocator.MDC_Status_Obj.KA_Connection_Count.ToString();
                //lblnkam_count.Text = MDCServer.MeterConnectionManager.ThreadAllocator.Nka_count.ToString();
                lblkam_exp_trans.Text = MDCServer.MeterConnectionManager.ThreadAllocator.MDC_Status_Obj.KA_Expected_Transactions.ToString();
                lblkam_succ_trans.Text = MDCServer.MeterConnectionManager.ThreadAllocator.MDC_Status_Obj.KA_Successful_Transactions.ToString();
                lblnkam_exp_trans.Text = MDCServer.MeterConnectionManager.ThreadAllocator.MDC_Status_Obj.NKA_Expected_Transactions.ToString();
                lblnkam_succ_trans.Text = MDCServer.MeterConnectionManager.ThreadAllocator.MDC_Status_Obj.NKA_Successful_Transactions.ToString();

                //if (MDCServer.MeterConnectionManager.ThreadAllocator.MDC_Status_Obj.KA_Expected_Transactions > 0)
                //{
                //  //  lbl_KA_success_per.Text = (100.0 * ((MDCServer.MeterConnectionManager.ThreadAllocator.MDC_Status_Obj.KA_Successful_Transactions * 1.0) / (MDCServer.MeterConnectionManager.ThreadAllocator.MDC_Status_Obj.KA_Expected_Transactions))).ToString("F2") + "%";

                //}
                //if (MDCServer.MeterConnectionManager.ThreadAllocator.MDC_Status_Obj.NKA_Expected_Transactions > 0)
                //{
                //  //  lbl_NKA_success_per.Text = (100.0 * ((MDCServer.MeterConnectionManager.ThreadAllocator.MDC_Status_Obj.NKA_Successful_Transactions * 1.0) / (MDCServer.MeterConnectionManager.ThreadAllocator.MDC_Status_Obj.NKA_Expected_Transactions))).ToString("F2") + "%";

                // }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update_MDC_Status()
        {
            //DBController.DBConnect.OpenConnection();//Open Connection

            ////Connections of Meters Count
            //long count = MDCServer.MeterConnectionManager.ThreadAllocator.MDC_Status_Obj.KA_Connection_Count;
            //long nkam_count = MDCServer.MeterConnectionManager.ThreadAllocator.MDC_Status_Obj.NKA_Connection_Count;
            //DBController.insert_con_count(startTime, count, nkam_count);

            ////Keep Alive Transactions status
            //long exceptedTrans = MDCServer.MeterConnectionManager.ThreadAllocator.MDC_Status_Obj.KA_Expected_Transactions;
            //long successfulTrans = MDCServer.MeterConnectionManager.ThreadAllocator.MDC_Status_Obj.KA_Successful_Transactions;
            //long kam_pooling_count = MDCServer.MeterConnectionManager.ThreadAllocator.MDC_Status_Obj.KA_Pooling_Count;
            //DBController.insert_Excepted_Trans(startTime, exceptedTrans, successfulTrans, kam_pooling_count);

            ////Non Keep Alive Transactions status
            //long nkamExpectedTrans = MDCServer.MeterConnectionManager.ThreadAllocator.MDC_Status_Obj.NKA_Expected_Transactions;
            //long nkamSuccessfulTrans = MDCServer.MeterConnectionManager.ThreadAllocator.MDC_Status_Obj.NKA_Successful_Transactions;
            //DBController.insert_Excepted_nkam_Trans(startTime, nkamExpectedTrans, nkamSuccessfulTrans);

            ////Save Last Pooling Time
            //DBController.SaveLastPollingTime(startTime, MDCServer.MeterConnectionManager.ThreadAllocator.MDC_Status_Obj.KA_Last_Pooling_Time);
            //DBController.DBConnect.CloseConnection();//Close Database Connection
        }

        private void backgroundWorker_Statistics_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                stats_obj = DBController.get_Statistics();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void backgroundWorker_Statistics_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error == null)
                    showStatistics();
            }
            catch (Exception)
            {

            }
        }

        #endregion

        #region Gets the build date and time (by reading the COFF header)

        // http://msdn.microsoft.com/en-us/library/ms680313
        struct _IMAGE_FILE_HEADER
        {
            public ushort Machine;
            public ushort NumberOfSections;
            public uint TimeDateStamp;
            public uint PointerToSymbolTable;
            public uint NumberOfSymbols;
            public ushort SizeOfOptionalHeader;
            public ushort Characteristics;
        };

        static DateTime GetBuildDateTime(Assembly assembly)
        {
            if (File.Exists(assembly.Location))
            {
                var buffer = new byte[Math.Max(Marshal.SizeOf(typeof(_IMAGE_FILE_HEADER)), 4)];
                using (var fileStream = new FileStream(assembly.Location, FileMode.Open, FileAccess.Read))
                {
                    fileStream.Position = 0x3C;
                    fileStream.Read(buffer, 0, 4);
                    fileStream.Position = BitConverter.ToUInt32(buffer, 0); // COFF header offset
                    fileStream.Read(buffer, 0, 4); // "PE\0\0"
                    fileStream.Read(buffer, 0, buffer.Length);
                }
                var pinnedBuffer = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                try
                {
                    var coffHeader = (_IMAGE_FILE_HEADER)Marshal.PtrToStructure(pinnedBuffer.AddrOfPinnedObject(), typeof(_IMAGE_FILE_HEADER));

                    return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1) + new TimeSpan(coffHeader.TimeDateStamp * TimeSpan.TicksPerSecond));
                }
                finally
                {
                    pinnedBuffer.Free();
                }
            }

            return new DateTime();
        }

        #endregion

        #region Events

        private void btn_Start_Server_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MDCServer.IsServerRunning &&
                    btn_Start_Server.Text == "Start")
                {
                    MDCServer.Start();
                    startTime = startTime.Add(DateTime.Now.Subtract(stopTime));
                    if (Settings.Default.SaveMDCSessions)
                    {
                        DBController.insert_mdc_session(Application.ProductVersion, startTime, DateTime.MaxValue);
                    }

                    if (MDCServer.IsServerRunning)
                    {
                        if (MDCServer.Activity_DataLogger != null)
                        {
                            MDCServer.Activity_DataLogger.LogMessage(string.Format("{0} Server Started, Listening On TCP Port {1}",
                                                                               Settings.Default.Instance, Settings.Default.Port));
                        }
                        else
                        {
                            MDC.Default_DataLogger.WriteInformation(string.Format("{0} Server Started, Listening On TCP Port {1}",
                                                                          Settings.Default.Instance, Settings.Default.Port), LogDestinations.EventLog | LogDestinations.TextFile);
                        }
                    }

                }
                else
                {
                    DialogResult Dl_Result = MessageBox.Show("Are you sure you want to Stop the Server Forcefully?",
                                                "Confirm Action!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (Dl_Result.Equals(DialogResult.Yes))
                    {
                        StopServerForcefully();
                        // MDCServer.TCPConController.TCPServer.ServerSocket.Disconnect(false);
                    }
                    else
                        return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "TCP Listener not Started " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LocalCommon.LogMDCExceptionIntoFile(ex);
                // Log Error Into File
                if (this.MDCServer != null && this.MDCServer.Activity_DataLogger != null)
                {
                    this.MDCServer.Activity_DataLogger.LogMessage("TCP Listener not Started ", ex, 10);
                }
                else
                    DLMS_Common.LogMessage(MDC.Default_DataLogger, "TCP Listener not Started ", ex, 10);
            }
            finally
            {
                try
                {
                    System.Threading.Thread.Sleep(1000);
                    bool IsRunning = MDCServer.IsServerRunning;
                    if (IsRunning)
                    {
                        btn_Start_Server.Text = "Stop";
                        timer.Enabled = true;
                        timer_RefreshAll.Enabled = true;
                        btnShutDown.Enabled = true;
                        btnShutDown.Text = "Shut Down";
                    }
                    else if (!IsRunning)
                    {
                        btn_Start_Server.Text = "Start";
                        timer.Enabled = false;
                        timer_RefreshAll.Enabled = false;
                        btnShutDown.Enabled = false;
                        btnShutDown.Text = "Shut Down";
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        private void ShutDownGracefully()
        {

            if (MDCServer.TCPConController.TCPServer.ConnectionList.Count > 0)
            {
                MDCServer.TCPConController.TCPServer.Disconnect_Server();
                MDCServer.TCPConController.TCPServer.IsShutDownInitiated = true;
                SpinWait.SpinUntil(() => { return MDCServer.TCPConController.TCPServer.ConnectionList.Count == 0; });

            }
            StopServerForcefully();

        }

        private void StopServerForcefully()
        {
            MDCServer.Stop();

            if (Settings.Default.SaveMDCSessions)
                DBController.update_mdc_session(DateTime.Now, startTime);
            try
            {
                // Console.Clear();
                if (!MDCServer.IsServerRunning)
                {
                    if (MDCServer.Activity_DataLogger != null)
                    {
                        MDCServer.Activity_DataLogger.LogMessage(string.Format("{0} Server Stopped", Settings.Default.Instance));
                    }
                    else
                    {
                        MDC.Default_DataLogger.WriteInformation(string.Format("{0} Server Stopped", Settings.Default.Instance),
                                                                      LogDestinations.EventLog | LogDestinations.TextFile);
                    }

                    stopTime = DateTime.Now;
                }
            }
            catch (Exception)
            { }
        }
        private void btnShutDown_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Dl_Result = MessageBox.Show("Are you sure you want to Shut Down the Server?", "Confirm Action!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Dl_Result.Equals(DialogResult.Yes))
                {
                    btnShutDown.Enabled = false;
                    btnShutDown.Text = "Waiting...";
                    Task.Factory.StartNew(() =>
                    {
                        ShutDownGracefully();
                        this.Invoke((MethodInvoker)(() =>
                        {
                            btnShutDown.Enabled = true;
                            btnShutDown.Text = "Shut Down";
                            btn_Start_Server.Text = "Start";
                            timer.Enabled = false;
                            timer_RefreshAll.Enabled = false;
                        }));

                    });
                    //MDCServer.TCPConController.TCPServer.ServerSocket.Disconnect(false);

                }
                else
                    return;
            }
            catch (Exception ex)
            {

                LocalCommon.LogMDCExceptionIntoFile(ex);
                // Log Error Into File
                if (this.MDCServer != null && this.MDCServer.Activity_DataLogger != null)
                {
                    this.MDCServer.Activity_DataLogger.LogMessage("Server Not Shut Down Properly", ex, 10);
                }
                else
                    DLMS_Common.LogMessage(MDC.Default_DataLogger, "Server Not Shut Down Properly", ex, 10);
            }
            finally
            {

            }
        }

        private void linkConnections_Click(object sender, EventArgs e)
        {
            try
            {
                //uCHeartBeat frm_HB = new uCHeartBeat();
                //frm_HB.ObjConnectionList = MDCServer.ConnectionManager.IOConnectionsList;
                //frm_HB.Show();
                if (MeterList == null || MeterList.IsDisposed)
                {
                    MeterList = new AllMeterscs(MDCServer.ConnectionManager.IOConnectionsList);
                    MeterList.ConManager = MDCServer.ConnectionManager;
                    Application.DoEvents();
                }
                MeterList.Show();
                MeterList.BringToFront();

            }
            catch (Exception ex)
            {
                LocalCommon.LogMDCExceptionIntoFile(ex);
                MessageBox.Show(this, "Error Display Connections List", "Error Display", MessageBoxButtons.OK);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.AppName = Application.ProductName;
                Commons.AppVersion = Application.ProductVersion;
                Commons.AppLastBuild = GetBuildDateTime(Assembly.GetEntryAssembly());
                // this.Text = String.Format("{0} Version: {1}, Build Date: {2}", Application.ProductName, Application.ProductVersion, GetBuildDateTime(Assembly.GetEntryAssembly()));
                this.Text = String.Format("{0}", Application.ProductName);

                cb_Echo.Text = "Show Echo";
                // MDCServer.Init_MDC();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Initializing MDC Server", "Error Initial", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw ex;
            }
        }



        // private void timer_statistics_Tick(object sender, EventArgs e)
        // {
        //     try
        //     {
        //         timer_statistics.Enabled = false;
        //         Show_MDC_Status_to_GUI();
        //         Update_MDC_Status();
        //     }
        //     catch (Exception)
        //     {
        //         throw;
        //     }
        //     finally
        //     {
        //         timer_statistics.Interval = 120000; //every 2 Minutes
        //         timer_statistics.Enabled = true; 
        //     }
        // }

        public void clearLogs()
        {
            try
            {
                string dir = string.Format(@"{0}\Errors", LocalCommon.GetApplicationConfigsDirectory());
                if (Directory.Exists(dir))
                    Directory.Delete(dir, true);

                dir = string.Format(@"{0}\Logs", LocalCommon.GetApplicationConfigsDirectory());
                if (Directory.Exists(dir))
                    Directory.Delete(dir, true);
                dir = string.Format(@"{0}\Exceptions", LocalCommon.GetApplicationConfigsDirectory());
                if (Directory.Exists(dir))
                    Directory.Delete(dir, true);

                //string file = string.Format(@"{0}\ConnectionLog.txt", Commons.GetApplicationConfigsDirectory());
                //if (File.Exists(file))
                //    File.Delete(file);

                //file = string.Format(@"{0}\Schedule_Errors.txt", Commons.GetApplicationConfigsDirectory());
                //if (File.Exists(file))
                //    File.Delete(file);
            }
            catch (Exception)
            {
                throw new Exception("Error clearing log");
            }
        }

        private void clearLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearLogs();
        }

        private void clearStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DBController.clearStatistics();
        }

        private void resetAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                clearLogs();
                DBController.clearStatistics();
                ///DBController.Reset_KASchedule();
                Console.Clear();
            }
            catch (Exception)
            { }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            #region statistics
            //if (!backgroundWorker_Statistics.IsBusy)
            //    backgroundWorker_Statistics.RunWorkerAsync();
            #endregion
            Show_MDC_Status_to_GUI();
        }

        private void openErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = LocalCommon.GetApplicationConfigsDirectory() + @"\errors";
            if (Directory.Exists(path))
                Process.Start(path);
        }

        private void openLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = LocalCommon.GetApplicationConfigsDirectory() + @"\logs";
            if (Directory.Exists(path))
                Process.Start(path);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.ShowDialog(this);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (btn_Start_Server.Text.ToLower().Equals("stop"))
                {
                    MessageBox.Show("Stop the server first.", "Alert!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
                else
                {
                    DialogResult Dl_Result = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (Dl_Result.Equals(DialogResult.Yes))
                    {
                        e.Cancel = false;

                    }
                    else
                        e.Cancel = true;
                }
            }
            catch
            { }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (MDCServer != null)
                    MDCServer.Dispose();
                Application.Exit();
                Environment.Exit(0);
            }
            catch
            { }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            try
            {
                IntPtr winHandle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;

                if (WindowState == FormWindowState.Minimized)
                {
                    ShowWindow(winHandle, SW_MINIMIZE);
                }
                else
                {
                    ShowWindow(winHandle, SW_RESTORE);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void clearOutputScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.Clear();
        }

        #endregion
        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                timer.Enabled = false;

                int year = 0, month = 0, days = 0;
                if (!(DateTime.Now > startTime))
                {
                    startTime = DateTime.Now;
                }

                TimeSpan span = DateTime.Now.Subtract(startTime);
                days = span.Days;
                if (days > 365)
                {
                    year = days / 365;
                    days = days % 365;
                }
                if (days > 30)
                {
                    month = days / 30;
                    days = days % 30;
                }

                string temp = year.ToString() + " Years, " + month.ToString() + " Months, " + days.ToString() + " Days and Time " + span.Hours.ToString("D2") + ":" + span.Minutes.ToString("D2") + ":" + span.Seconds.ToString("D2");//span.ToString();
                lbl_datetime.Text = temp;//.Substring(0, temp.IndexOf('.'));

            }
            catch (Exception)
            {
                lbl_datetime.Text = "Error";
            }
            finally
            {
                timer.Enabled = true;
            }
        }
        private void timer_AllocatedCount_Tick(object sender, EventArgs e)
        {
            timer_RefreshAll.Enabled = false;

            try
            {
                if (MDCServer != null && MDCServer.ConnectionManager != null)
                {
                    long TcpClientStatus = MDCServer.ConnectionManager.IOConnectionsList.Count;

                    var kAcount = MDCServer.ConnectionManager.IOConnectionsList.KeepAliveCount;
                    lblKeepAliveCount.Text = (kAcount == -1) ? "" : kAcount.ToString();

                    var monkAcount = MDCServer.ConnectionManager.IOConnectionsList.NonKeepAliveCount;
                    lblNonKeepAliveCount.Text = (monkAcount == -1) ? "" : monkAcount.ToString();

                    if (TcpClientStatus == -1)
                        //lbl_ConnectedCount.Text = "No Meter Connection Currently Connected";
                        lbl_ConnectedCount.Text = "0";

                    else
                        lbl_ConnectedCount.Text = TcpClientStatus.ToString();// +" Meters are currently Connected";

                    #region Active Meter Connections
                    if(MDCServer.MeterConnectionManager == null)
                    {
                        throw new Exception("MeterConnectionManager is null");
                    }
                    if(MDCServer.MeterConnectionManager.ThreadAllocator == null)
                    {
                        throw new Exception("MeterConnectionManager.ThreadAllocator is null");
                    }
                    long countAllocated = MDCServer.MeterConnectionManager.ThreadAllocator.Allocated_Count;
                    if (countAllocated == -1)
                        lbl_AllocatedCount.Text = "";
                    else
                        lbl_AllocatedCount.Text = countAllocated.ToString();// +" Meters are currently allocated";
                    #endregion
                }
                else
                    //lbl_AllocatedCount.Text = String.Format("No TCP Client Connection exists");
                    lbl_AllocatedCount.Text = String.Format("");

                Show_MDC_Status_to_GUI();
            }
            catch (Exception ex)
            {
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }

            timer_RefreshAll.Enabled = true;
        }

        private void cb_Echo_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_Echo.Checked)
            {
                //Commons.EnableEcho = true;
                cb_Echo.Text = "Hide Echo";
                Commons.ShowConsoleWindow();
            }
            else
            {
                cb_Echo.Text = "Show Echo";
                //Commons.EnableEcho = false;
                Commons.HideConsoleWindow();
            }
        }

        private void timer_LiscenseValidation_Tick(object sender, EventArgs e)
        {
            try
            {
                //if (!LicenseValidationObject.IsValidated(Limits.MachineProfileID))
                //{
                //    MessageBox.Show("License Expired.", "MTI Smart Eye MDC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    Environment.Exit(1);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MTI Smart Eye MDC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }

        private void cb_GeneralLog_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_GeneralLog.Checked)
            {
                Settings.Default["EnableGeneralLog"] = true;
            }
            else
            {
                Settings.Default["EnableGeneralLog"] = false;
            }
        }

        private void linkTCPStatus_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                String TcpServerStatus = "";
                String TcpClientStatus = "";
                String heartBeatStatus = "";
                String MeterAllocatedCount = "";
                String MeterConnectedCount = "";

                if (!MDCServer.IsServerRunning)
                {
                    TcpServerStatus = "TCP Listener is not running";
                    //TcpClientStatus = "No TCP Client Connection exists";
                    //MessageBox.Show(this, String.Format("Smart Eye MDC Server Status\r\n{0}\r\n{1}", TcpServerStatus, TcpClientStatus), "Server Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    TcpServerStatus = String.Format("TCP Listener is listening {0}", MDCServer.TCPConController.TCPServer.LocalSocket.ToString());
                }
                if (MDCServer != null && MDCServer.ConnectionManager != null)
                {
                    if (MDCServer.ConnectionManager.IsConnected)
                        TcpClientStatus = String.Format("{0} TCP Clients are connected", MDCServer.ConnectionManager.IOConnectionsList.Count);

                    HeartBeat hbeat = MDCServer.ConnectionManager.LastHeartBeatReceived;
                    heartBeatStatus = (hbeat != null && hbeat.IsVerifited) ? hbeat.ToString() : "";
                    #region Active Meter Connections
                    long countAllocated = MDCServer.MeterConnectionManager.ThreadAllocator.Allocated_Count;
                    long meterConnectionCount = MDCServer.MeterConnectionManager.ThreadAllocator.Active_Connection_Count;

                    if (countAllocated == -1)
                        MeterAllocatedCount = "No Meter Connection Currently Allocated";
                    else
                        MeterAllocatedCount = String.Format("{0} Meter Connections Currently Allocated", countAllocated);

                    if (meterConnectionCount == -1)
                        MeterConnectedCount = "No Meter Currently have Active Data Session";
                    else
                        MeterConnectedCount = String.Format("{0} Meters Currently have Active Data Session", meterConnectionCount);
                    #endregion
                }
                else
                    TcpClientStatus = String.Format("No TCP Client Connection exists");
                string heartBeatMsg = String.Format("Last Heart Beat received {0}", heartBeatStatus);
                MessageBox.Show(this, String.Format("Smart Eye MDC Server Status\r\n{0}\r\n{1}\r\n{2}\r\n{3}", TcpServerStatus, TcpClientStatus, MeterAllocatedCount,
                    (String.IsNullOrEmpty(heartBeatStatus)) ? "" : heartBeatMsg), "TCP Connection Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error getting TCP Connection Status" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }

        private void timer_StartMDC_Tick(object sender, EventArgs e)
        {
            #region Start Server on running application
            try
            {
                if (StartServer)
                {
                    try
                    {

                        if (!MDCServer.IsServerRunning)
                        {
                            MDCServer.Start();

                        }
                        StartServer = false;
                    }
                    catch (Exception ex)
                    {
                        LocalCommon.SaveApplicationException(new Exception("Main timer_StartMDC_Tick inner"), 1);
                        LocalCommon.LogMDCExceptionIntoFile(ex);
                    }
                    finally
                    {
                        startTime = DateTime.Now;
                        if (Settings.Default.SaveMDCSessions)
                        {
                            DBController.insert_mdc_session(Application.ProductVersion, DateTime.Now, DateTime.MaxValue);
                        }

                        btn_Start_Server.Text = "Stop";
                        timer_RefreshAll.Enabled = true;
                        timer.Enabled = true;
                        Settings.Default["EnableGeneralLog"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LocalCommon.SaveApplicationException(new Exception("Main timer_StartMDC_Tick"), 1);
                MessageBox.Show(ex.Message);
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
            finally
            {
                timer_StartMDC.Enabled = false;
            }
            #endregion
        }

        private void linkConnections_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }


    }
}
