using DLMS;
using GUI;
using SharedCode.Comm.HelperClasses;
using SharedCode.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SmartEyeControl_7.ApplicationGUI.ucCustomControl
{
    public partial class uCHeartBeat : Form
    {
        int RefreshTime = 0;
        int RefreshTimeLeft = 0;
        ApplicationController obj_ApplicationController;
        ConnectionController obj_ConnController;
        ConnectionsList objConnectionList;
        private ProgressDialog progressDialog;
        private Exception ex;
        private ApplicationProcess_Controller AP_Controller;
        private bool isReadWriteStart;
        public event Action ApplicationReadWrite = delegate { };
        Thread th;
        const byte _MSN = 0;
        const byte _IP = 1;
        const byte _PORT = 2;
        const byte _ConnectionTime = 3;
        const byte _LastHeartBeatTime = 4;
        const byte _Status = 5;
        private ComponentFactory.Krypton.Toolkit.KryptonButton ConnectButton_CLASS;
        private string Selected_IP_Port;

        public uCHeartBeat()
        {
            InitializeComponent();

        }

        public uCHeartBeat(ApplicationController Application_Controller, ConnectionController objConnController)
        {
            InitializeComponent();
            timer_updateButton.Enabled = true;
            obj_ApplicationController = Application_Controller;
            obj_ApplicationController.PropertyChanged += new PropertyChangedEventHandler(Application_Controller_PropertyChanged);
            obj_ConnController = objConnController;
            combo_HB_RefreshInterval.SelectedIndex = 0;
            progressDialog = new ProgressDialog();
            AP_Controller = obj_ApplicationController.Applicationprocess_Controller;
        }

        private void uCHeartBeat_Load(object sender, EventArgs e)
        {
            timer_refreshList.Start();
            timer1.Start();
            obj_ApplicationController.PropertyChanged += new PropertyChangedEventHandler(Application_Controller_PropertyChanged);
        }

        private void Application_Controller_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                ///Okay IsIOBusy Status
                if ("IsIOBusy".Equals(e.PropertyName) && sender is ApplicationController)
                {
                    ///
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void timer_refreshList_Tick(object sender, EventArgs e)
        {
            RefreshTimeLeft = RefreshTime / 1000;
            timer_refreshList.Enabled = false;
            timer_refreshList.Stop();
            showToGUI_HeartsBeats();
            timer_refreshList.Enabled = true;
            timer_refreshList.Start();

        }

        private void showToGUI_HeartsBeats()
        {
            try
            {
                IOConnection item = null;
                int MaxCount = 0;
                objConnectionList = obj_ApplicationController.ConnectionController.ConnectionManager.IOConnectionsList;
                Interlocked.Exchange(ref MaxCount, objConnectionList.Count);

                ConnectionsList localList = new ConnectionsList(objConnectionList);
                
                lbl_totalConnections.Text = MaxCount.ToString();
                clearGrid(grid_MeterHb);
                for (int k = 0; k < MaxCount; k++)
                {
                    item = localList[k];
                    grid_MeterHb.Rows.Add();
                    grid_MeterHb.Rows[grid_MeterHb.Rows.Count - 1].HeaderCell.Value = grid_MeterHb.Rows.Count.ToString();

                    if (item.ConnectionInfo.MSN != null)
                        grid_MeterHb[_MSN, grid_MeterHb.Rows.Count - 1].Value = item.ConnectionInfo.MSN;

                    string IP_PORT = item.IOStream.ToString();
                    int index = IP_PORT.IndexOf(':');
                    grid_MeterHb[_IP, grid_MeterHb.Rows.Count - 1].Value = IP_PORT.Substring(0, index);
                    grid_MeterHb[_PORT, grid_MeterHb.Rows.Count - 1].Value = IP_PORT.Substring(index + 1);
                    grid_MeterHb[_ConnectionTime, grid_MeterHb.Rows.Count - 1].Value = item.ConnectionInfo.LastActivity;

                    if (IP_PORT == Selected_IP_Port)
                    {
                        grid_MeterHb[0, grid_MeterHb.Rows.Count - 1].Selected = true;
                    }

                    if (item.HeartBeats.Count > 0)
                        grid_MeterHb[_LastHeartBeatTime, grid_MeterHb.Rows.Count - 1].Value = item.HeartBeats[item.HeartBeats.Count - 1].DateTimeStamp;

                }

                for (int i = 0; i < grid_MeterHb.Rows.Count; i++)
                {
                    string meterIP = grid_MeterHb[1, i].Value.ToString() + ":" + grid_MeterHb[2, i].Value.ToString();
                    IOConnection connection = getConnection(meterIP, true);
                    if (AP_Controller.IsConnected && AP_Controller.GetCommunicationObject == connection)
                    {
                        grid_MeterHb[5, i].Value = "Connected";
                        grid_MeterHb.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                    }
                    else
                    {
                        if (grid_MeterHb[0, i].Value == null)
                        {
                            grid_MeterHb[5, i].Value = "No HeartBeat";
                            grid_MeterHb.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (localList[i].HeartBeats.Count > 0)
                            {
                                grid_MeterHb[5, i].Value = "HeartBeat";
                                grid_MeterHb.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                            }
                            else
                            {
                                grid_MeterHb[5, i].Value = "Idle";
                                grid_MeterHb.Rows[i].DefaultCellStyle.ForeColor = Color.Brown;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error showing Heartbeats \r\n"+ex.Message);
            }
        }

        private void clearGrid(DataGridView grid)
        {
            for (int i = grid.Rows.Count - 1; i >= 0; i--)
            {
                grid.Rows.RemoveAt(i);
            }
        }

        private void GetMeterInfoMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (grid_MeterHb[0, grid_MeterHb.CurrentCell.RowIndex].Value == null)
                    return;
                string meterSerial = grid_MeterHb[0, grid_MeterHb.CurrentCell.RowIndex].Value.ToString();
                IOConnection selectedConn = getConnection(meterSerial);
                if (selectedConn == null)
                    return;
                string dialogMsg = null;
                if (selectedConn.ConnectionInfo == null)
                    dialogMsg = String.Format("Connected Connection Info {0}", selectedConn.IOStream);
                else
                {
                    string title = String.Format("Connected Connection Info {0}\r\n", selectedConn);
                    dialogMsg = title;
                    if (selectedConn.ConnectionInfo != null && selectedConn.ConnectionInfo.LastActivity != DateTime.MinValue)
                    {
                        dialogMsg = String.Format("Connection duration {0} \r\n", DateTime.Now.Subtract(selectedConn.ConnectionInfo.LastActivity));
                    }
                    if (selectedConn.ConnectionInfo != null && selectedConn.ConnectionInfo.MeterSerialNumberObj != null)
                    {
                        string MSN = String.Format("MSN {0}", selectedConn.ConnectionInfo.MSN);
                        dialogMsg = dialogMsg + MSN;
                    }
                    if (selectedConn.ConnectionInfo != null &&
                        selectedConn.ConnectionInfo.MeterInfo != null &&
                        !String.IsNullOrEmpty(selectedConn.ConnectionInfo.MeterInfo.MeterModel))
                    {
                        string model = String.Format("Model {0}", selectedConn.ConnectionInfo.MeterInfo.MeterModel);
                        dialogMsg = dialogMsg + "," + model;
                    }
                    if (selectedConn.ConnectionInfo != null &&
                        selectedConn.ConnectionInfo.MeterInfo != null &&
                        !String.IsNullOrEmpty(selectedConn.ConnectionInfo.MeterInfo.Version))
                    {
                        string firmwareVersion = String.Format("Firmware Version {0}", selectedConn.ConnectionInfo.MeterInfo.Version);
                        dialogMsg = dialogMsg + "," + firmwareVersion;
                    }
                    if (selectedConn.HeartBeats != null && selectedConn.HeartBeats.Count > 0)
                    {
                        HeartBeat hb = selectedConn.HeartBeats[selectedConn.HeartBeats.Count - 1];
                        dialogMsg = dialogMsg + String.Format("\r\nLast Heart Beat Duration {0}", DateTime.Now.Subtract(hb.DateTimeStamp));
                    }
                    if (selectedConn.ConnectionMonitor != null)
                    {
                        dialogMsg = dialogMsg + String.Format("\r\nTotal Received Bytes {0},Total Transmitted Bytes {1}",
                            selectedConn.ConnectionMonitor.InFlowTotalDataLength,
                            selectedConn.ConnectionMonitor.OutFlowTotalDataLength);
                    }
                }

                MessageBox.Show(this, dialogMsg, "Connection Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void readMeterInfoMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ///Abort Previous Executing Action


                int count = 0;
                while (BckWorkerThread != null && BckWorkerThread.IsBusy)
                {
                    BckWorkerThread.CancelAsync();
                    count++;
                }
                // Disable_IOActivity();
                BckWorkerThread = new BackgroundWorker();
                BckWorkerThread.WorkerSupportsCancellation = true;
                BckWorkerThread.DoWork += new DoWorkEventHandler(BckWorker_ReadMeterInfo_DoEventHandler);
                BckWorkerThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BckWorker_ReadMeterInfo_WorkCompleted);
                BckWorkerThread.RunWorkerAsync();
                obj_ApplicationController.IsIOBusy = true;
                progressDialog.DialogTitle = "Reading Meter Info";
                ///Configure Progress Dialog Box
                ///to Be Displayed
                progressDialog.DialogStatus = "";
                progressDialog.UserInputEnable = true;
                progressDialog.EnableProgressBar = true;
                progressDialog.IsAutoHideNow = false;
                //progressDialog.okButton.Visible = false;

                DialogResult result = progressDialog.ShowDialog();
                DialogResult res = DialogResult.OK;
                if (BckWorkerThread.IsBusy && (result == DialogResult.OK || result == DialogResult.Cancel))
                {
                    res = MessageBox.Show(this, "Are you sure want to cancel current porcess immediately? Otherwise wait till process completes."
                        , "Cancel Current Process", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                while (BckWorkerThread.IsBusy)
                {
                    if (res == DialogResult.OK && !BckWorkerThread.CancellationPending)  ///Okay Cancel Current Process
                        BckWorkerThread.CancelAsync();
                    Thread.Sleep(1);
                    Application.DoEvents();
                }


            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, "Error Reading Info," + ex.Message,"", MessageBoxButtons.OK);
                Notification errorNotifier = new Notification("Error Reading Meter Info", ex.Message);
            }
        }

        private void DropConnMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                obj_ConnController.Disconnect();
                obj_ApplicationController.ConnectToMeter = null;
                IpConContextMenu.Items[3].Enabled = true;
                Notification n = new Notification("Disconnected", "");
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                obj_ApplicationController.ConnectToMeter = null;
                throw ex;
            }
        }

        #region Read Meter Info

        private void BckWorker_ReadMeterInfo_DoEventHandler(Object sender, DoWorkEventArgs arg)
        {
            th = new Thread(BckWorker_ReadMeterInfo_DoWork_Helper);
            th.Priority = ThreadPriority.AboveNormal;
            try
            {
                th.Start();
                ex = null;
                Thread.Sleep(1);
                while (!(th.ThreadState == System.Threading.ThreadState.Aborted ||
                    th.ThreadState == System.Threading.ThreadState.Stopped))
                {
                    Application.DoEvents();
                    Thread.Sleep(1);
                    if (BckWorkerThread.CancellationPending)
                    {
                        th.Abort();
                        ///Set Application State
                        Thread.Sleep(1);
                    }
                }
                if (BckWorkerThread.CancellationPending)
                {
                    if (AP_Controller.IsConnected)
                    {
                        AP_Controller.ARLRQAsync().Wait(500);
                    }
                    obj_ConnController.AP_Controller.ApplicationProcess.Is_Association_Developed = false;
                    arg.Cancel = true;
                }
                if (ex != null)
                    throw ex;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);

                throw ex;
            }
            finally
            {
                if (th != null)
                    th.Abort();
            }
        }

        private void BckWorker_ReadMeterInfo_DoWork_Helper()
        {
            try
            {
                string meterIP = grid_MeterHb[1, grid_MeterHb.CurrentCell.RowIndex].Value.ToString() + ":" + grid_MeterHb[2, grid_MeterHb.CurrentCell.RowIndex].Value.ToString();
                IOConnection IOConn = getConnection(meterIP, true);
                if (!AP_Controller.IsConnected ||
                    (AP_Controller.IsConnected && AP_Controller.GetCommunicationObject == IOConn))
                {

                    int sapAddress = 01;
                    String txtPasswd = "microtek";

                    if (IOConn == null || !IOConn.IsConnected)
                    {
                        throw new Exception("Unable to read meter information,IP Link is disconnected");
                    }

                    #region /// Init Work Before Connection Works

                    if (IOConn.ConnectionInfo == null)
                    {
                        IOConn.ConnectionInfo = new ConnectionInfo();
                    }

                    MeterConfig conf = obj_ConnController.SelectedMeter;
                    if (IOConn.ConnectionInfo.MeterInfo == null)
                        IOConn.ConnectionInfo.MeterInfo = conf;

                    // SAPConfig CurrentSAP = conf.SapConfigs.Find((x) => x.MeterSap.SAP_Address == sapAddress);
                    // if (CurrentSAP != null)
                    // {
                    //     CurrentSAP = new SAPConfig(CurrentSAP);
                    // }

                    // CurrentSAP.Password = txtPasswd;
                    // if (CurrentSAP != null)
                    // {
                    //     IOConn.ConnectionInfo.CurrentSAP = CurrentSAP;
                    // }

                    #endregion

                    obj_ApplicationController.IsIOBusy = true;
                    obj_ConnController.GetMeterInfo(IOConn);
                }

                else
                {
                    throw new Exception("Application is already connected with another meter device,please try again lator");
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                this.ex = ex;
            }
            finally
            {
                Thread.CurrentThread.Abort();

            }
        }

        //private void BckWorker_ReadMeterInfo_DoWork_Helper()
        //{
        //    try
        //    {
        //        string meterIP = grid_MeterHb[1, grid_MeterHb.CurrentCell.RowIndex].Value.ToString() + ":" + grid_MeterHb[2, grid_MeterHb.CurrentCell.RowIndex].Value.ToString();
        //        IOConnection IOConn = getConnection(meterIP, true);
        //        if (!AP_Controller.IsConnected ||
        //            (AP_Controller.IsConnected && AP_Controller.GetCommunicationObject == IOConn))
        //        {

        //            String meterSAPName = "Management";
        //            String clientSAPName = "Management";
        //            String txtPasswd = "mtiDLMScosem";

        //            if (IOConn == null || !IOConn.IsConnected)
        //            {
        //                throw new Exception("Unable to read meter information,IP Link is disconnected");
        //            }
        //            #region ///Init Work Before Connection Works
        //            if (IOConn.ConnectionInfo == null)
        //            {
        //                IOConn.ConnectionInfo = new ConnectionInfo();

        //            }
        //            MeterConfig conf = obj_ConnController.MeterSAPConfig;
        //            if (IOConn.ConnectionInfo.MeterInfo == null ||
        //                IOConn.ConnectionInfo.MeterInfo.ServerSAP.Count <= 0 ||
        //                IOConn.ConnectionInfo.MeterInfo.ClientSAP.Count <= 0)
        //                IOConn.ConnectionInfo.MeterInfo = conf;

        //            SAPConfig CurrentMeterSAP = conf.ServerSAP.Find((x) => x.FaceName.Equals(meterSAPName, StringComparison.OrdinalIgnoreCase));
        //            SAP_Object CurrentClientSAP = conf.ClientSAP.Find((x) => x.SAP_Name.Equals(clientSAPName, StringComparison.OrdinalIgnoreCase));
        //            if (CurrentMeterSAP != null)
        //            {
        //                IOConn.ConnectionInfo.CurrentMeterSAP = CurrentMeterSAP;
        //                CurrentMeterSAP.DefaultPassword = txtPasswd;
        //            }
        //            if (CurrentClientSAP != null)
        //            {
        //                IOConn.ConnectionInfo.CurrentClientSAP = CurrentClientSAP;
        //            }
        //            #endregion
        //            obj_ApplicationController.IsIOBusy = true;
        //            obj_ConnController.GetMeterInfo(IOConn);
        //        }

        //        else
        //        {
        //            throw new Exception("Application is already connected with another meter device,please try again lator");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.Message);
        //        this.ex = ex;
        //    }
        //    finally
        //    {
        //        Thread.CurrentThread.Abort();

        //    }
        //}

        private void BckWorker_ReadMeterInfo_WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                    throw e.Error;
                ///Application Connected disable Controls,Update Controls On Connection Establishment
                progressDialog.IsAutoHideNow = true;
                progressDialog.UpdateDialogStatusHandler("Process Completed");

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Msg:" + ex.Message,"Error Connecting",MessageBoxButtons.OK,MessageBoxIcon.Error);
                String _txt = String.Format("Error occurred reading meter information:\r\n{0} {1}",
                    ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
                progressDialog.UpdateDialogStatusHandler(_txt);
                Notification errorNotifier = new Notification("Error Reading Meter Info", _txt);
            }
            finally
            {
                // UpdateConnectStatus(obj_ConnController.IsConnected);
                progressDialog.EnableProgressBar = false;
                progressDialog.okButton.Visible = true;
                progressDialog.btnCancel.Visible = true;
                obj_ApplicationController.IsIOBusy = false;
            }
        }


        #endregion

        private void addToFavourites_Click(object sender, EventArgs e)
        {
            try
            {
                updateFavouriteMeterList();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void updateFavouriteMeterList()
        {
            //Access Binding List and modify the collection
            if (grid_MeterHb.CurrentCell == null)
                return;

            int row = grid_MeterHb.CurrentCell.RowIndex;

            string IPPort = grid_MeterHb[_IP, row].Value.ToString() + ":" + grid_MeterHb[_PORT, row].Value.ToString();

            var keys = objConnectionList.Keys;
            var values = new List<IOConnection>();
            foreach (var key in keys)
            {
                IOConnection conn;
                if (objConnectionList.TryGetValue(key, out conn))
                {
                    values.Add(conn);
                }
            }

            foreach (var item in values)
            {
                if (item.IOStream.ToString() == IPPort)
                {
                    if (obj_ConnController.MeterSerial.Contains(item))
                    {
                        Notification n = new Notification("Error", "Meter already added to Favourite List");
                        return;
                    }
                    else
                    {
                        obj_ConnController.MeterSerial.Add(item);
                        obj_ConnController.MeterSerial.ResetBindings();
                    }

                    break;
                }
            }


        }

        private void combo_HB_RefreshInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTime = Convert.ToInt16(combo_HB_RefreshInterval.Text) * 1000;
            RefreshTimeLeft = RefreshTime / 1000;
            timer_refreshList.Interval = Convert.ToInt16(combo_HB_RefreshInterval.Text) * 1000;
        }

        private IOConnection getConnection(string MSN)
        {
            var keys = objConnectionList.Keys;
            var values = new List<IOConnection>();
            foreach (var key in keys)
            {
                IOConnection conn;
                if (objConnectionList.TryGetValue(key, out conn))
                {
                    values.Add(conn);
                }
            }
            foreach (IOConnection item in values)
            {
                if (item.ConnectionInfo.MSN == MSN)
                {
                    return item;
                }

            }
            return null;
        }

        private IOConnection getConnection(string IP, bool isTrue)
        {
            var keys = objConnectionList.Keys;
            var values = new List<IOConnection>();
            foreach (var key in keys)
            {
                IOConnection conn;
                if (objConnectionList.TryGetValue(key, out conn))
                {
                    values.Add(conn);
                }
            }

            foreach (IOConnection item in values)
            {
                string IOStream = item.IOStream.ToString();
                if (IOStream == IP)
                {
                    return item;
                }

            }
            return null;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            RefreshTimeLeft--;
            lbl_RefreshTimeLeft.Text = RefreshTimeLeft.ToString();
            lbl_1.Visible = true;
            lbl_2.Visible = true;
            Application.DoEvents();
            timer1.Start();
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            if (obj_ApplicationController.ConnectToMeter == null)
            {
                readMeterInfoMenuItem_Click(this, new EventArgs());
                string IPPort = grid_MeterHb[_IP, grid_MeterHb.CurrentCell.RowIndex].Value.ToString() + ":" + grid_MeterHb[_PORT, grid_MeterHb.CurrentCell.RowIndex].Value.ToString();
                IOConnection connection = getConnection(IPPort, true);
                obj_ApplicationController.ConnectToMeter = connection;
                IpConContextMenu.Items[3].Enabled = false;
            }
            else
            {
                Notification n = new Notification("Connection Failed", "Another meter is already connected");
            }
        }

        private IOConnection getConnectedMeter()
        {
            for (int i = 0; i < grid_MeterHb.Rows.Count; i++)
            {
                string IPPort = grid_MeterHb[_IP, i].Value.ToString() + ":" + grid_MeterHb[_PORT, i].Value.ToString();
                if (grid_MeterHb[_Status, i].Value.ToString() == "Connected")
                {
                    return getConnection(IPPort, true);
                }
            }
            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showToGUI_HeartsBeats();
        }

        private void grid_MeterHb_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (grid_MeterHb.CurrentCell != null)
                {
                    Selected_IP_Port = grid_MeterHb[_IP, grid_MeterHb.CurrentCell.RowIndex].Value.ToString();
                    Selected_IP_Port += ":" + grid_MeterHb[_PORT, grid_MeterHb.CurrentCell.RowIndex].Value.ToString();
                }
            }
            catch
            {
            }

        }

        private void uCHeartBeat_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                obj_ApplicationController.PropertyChanged -= new PropertyChangedEventHandler(Application_Controller_PropertyChanged);
            }
            catch (Exception)
            {
            }
        }

        private void btn_wakeup_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Environment.CurrentDirectory + @"\Wakeup Generator.exe");
        }


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
