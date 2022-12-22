//#define AppLoginSkip

using AccurateOptocomSoftware.comm;
using DatabaseConfiguration.DataSet;
using DLMS;
using DLMS.Comm;
using SharedCode.Comm.HelperClasses;
using SharedCode.Controllers;
using SmartEyeControl_7.DB;
using SharedCode.TCP_Communication;
using SmartEyeAdvanceControl;
using SmartEyeControl_7;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ucCustomControl;
using SEAC.Common;
using AccurateOptocomSoftware.Common;
using SharedCode.Common;
using DatabaseConfiguration.DataBase;
using SmartEyeControl_7.comm;
using SharedCode.Comm.DataContainer;
using ServerToolkit.BufferManagement;
using OptocomSoftware.Properties;

namespace GUI
{
    public delegate void MarkStatus(bool status);
    public delegate bool GetStatus();

    public partial class FrmContainer : Form
    {
        [DllImport("User32")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);
        /// Declare Classes
        #region Declare Classes

        DLMS_Application_Process Application_Process;
        TCP_Client Client = new TCP_Client();
        private ConnectionManager connectionManager;
        private ConnectionController ConnController;
        private ApplicationProcess_Controller AP_Controller;
        private ApplicationController Application_Controller;
        private BackgroundWorker BckWorkerThread;
        private ProgressDialog progressDialog;
        private Exception ex;
        private Thread th;
        private Thread thread_PassKey; //v4.8.12 Added by Azeem
        private SharedCode.Comm.HelperClasses.Debugger _debugger;

        #endregion
        private BindingList<IOConnection> MeterSerial;

        string ImageFolderPath = "";
        bool paramChecked, modemChecked, billingChecked, loadprofileChecked, instantaneousChecked, eventsChecked, settingsChecked, helpChecked, debugChecked, logOffChecked, adminChecked;
        private Size size_pnl_comm;
        List<string> Categories_List_Items = new List<string>();
        int iconheightNormal = 50;
        int iconWidthNormal = 75; //50;
        //BackImage Commented By Azeem For Accurate
        //System.Drawing.Image backImage = System.Drawing.Image.FromFile(Environment.CurrentDirectory + @"\images\backImage.jpg"); //Commented by Azeem due to Accurate Accurate
        int iconheightFocus = 57;
        int iconWidthFocus = 75;// 57;
        protected bool allowshowdisplay = true;

        Bitmap billing, parameterization, modem, loadprofile, instantaneous, events, settings, help, debug, logOff, admin;
        Bitmap billing_active, parameterization_active, modem_active, loadprofile_active, instantaneous_active, events_active, settings_active, help_active, debug_active, logOff_active, admin_active;
        //Bitmap backgroundGradient;

        DataBaseController dbController = new DataBaseController();
        DatabaseValidator dbValidator = null;
        //v4.8.12
        PassKeyManager pasKyMngr = null;
        PasswordKey ObjPassKey = null;
        //v4.8.12

        private BufferPool pool = null;
        private GetDataReaderBuffer DataBuffer = null;
        public static readonly long BufferPoolSlabSize = 1024 * 1024;  // 1_MB Pre-Reserved Slab Size
        public static readonly int BufferPoolSlabCount = 20;           // 20 Slab Count

        uCHeartBeat frm_HB;

        //private List<AccessRights> genRights = null; //v4.8.15
        private ConfigsHelper ConfigLoader;
        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(allowshowdisplay ? value : allowshowdisplay);
        }

        public void SplashLoad()
        {
            SmartEyeControl_7.Splash sp = null;
            sp = new SmartEyeControl_7.Splash();
            sp.Opacity = 0;
            sp.StartPosition = FormStartPosition.CenterScreen;
            sp.WindowState = FormWindowState.Normal;

            sp.Show();

            while (this.WindowState == FormWindowState.Minimized)
            {
                Application.DoEvents();
                if (sp.Opacity != 1)
                    sp.Opacity = sp.Opacity + .01;
            }
        }

        public IBuffer GetDataBuffer(int MaxBufferSize = Commons.IOBufferLength)
        {
            try
            {
                lock (pool)
                {
                    return pool.GetBuffer(MaxBufferSize);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while taking Data Reader Buffer ", ex);
            }
        }

        public FrmContainer()
        {
            bool flag = false;
            try
            {
                InitializeComponent();
                //SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
                this.WindowState = FormWindowState.Minimized;
                Commons.Application_Type = ApplicationType.SEAC;
                //Added by Azeem Inayat
                this.Text = Get_ApplicationTitle();
                ImageFolderPath = Environment.CurrentDirectory + @"\images\icons\";

                pool = new BufferPool(BufferPoolSlabSize, BufferPoolSlabCount, BufferPoolSlabCount / 2);
                DataBuffer = new GetDataReaderBuffer(GetDataBuffer);
                //#region DataBase Validation
                //if (!dbValidator.Validate_Database())
                //{
                //    MessageBox.Show("DB Error 0x1");
                //    Environment.Exit(11);
                //}
                //#endregion

                //Application Default Settings
                SmartEyeControl_7.Properties.Resources.Culture = new System.Globalization.CultureInfo("en-us");
                #region /// Instantiate Class

                // Instantiate Class

                Application_Controller = new ApplicationController();
                ConfigLoader = new ConfigsHelper(Application_Controller.Configurations);
                //ConfigLoader.DAL = new SEAC_DBAccessLayer(OptocomSoftware.Properties.Settings.Default.SEAC7_DBConnectionString);
                //ConfigLoader.DAL = new ConfigDBController(OptocomSoftware.Properties.Settings.Default.SEAC7_DBConnectionString, DatabaseConfiguration.DataBaseTypes.SEAC_DATABASE);
                ConfigLoader.DAL = new ConfigDBController(@"Application_Configs\SEAC.db", DatabaseConfiguration.DataBaseTypes.SEAC_DATABASE);
                Application_Controller.Configurator.ConfigurationHelper = ConfigLoader;
                Application_Controller.LoadProfile_Controller.Configurator.ConfigurationHelper = ConfigLoader;
                Application_Controller.EventController.Configurator.ConfigurationHelper = ConfigLoader;
                Application_Controller.Billing_Controller.Configurator.ConfigurationHelper = ConfigLoader;
                Application_Process = Application_Controller.Applicationprocess_Controller.ApplicationProcess;
                connectionManager = Application_Controller.ConnectionManager;
                AP_Controller = Application_Controller.Applicationprocess_Controller;
                ConnController = Application_Controller.ConnectionController;
                ConnController.ConnectionManager.CreateDataReaderBuffer = DataBuffer;
                _debugger = new SharedCode.Comm.HelperClasses.Debugger();

                // Load Application Configuration
                this.ConfigLoader.LoadConfiguration(OptocomSoftware.Properties.Settings.Default.SEAC7_DBConnectionString);
                Configs conf = Application_Controller.Configurations;

                #endregion

                // By Pass Access Rights Check
                //if (this.Application_Controller != null)
                ApplicationProcess_Controller.CheckAccessRights =
                      Convert.ToBoolean(OptocomSoftware.Properties.Settings.Default["CheckAccessRights"] ?? "False");

                if (connectionManager != null)
                {
                    this.connectionManager.MaxReadBuffer =
                        Convert.ToInt32(OptocomSoftware.Properties.Settings.Default["MaxReadBuffer"] ?? 1024);

                    this.connectionManager.MaxWriteBuffer =
                        Convert.ToInt32(OptocomSoftware.Properties.Settings.Default["MaxWriteBuffer"] ?? 1024);
                }

                #region LOGIN PAGE again:

                SmartEyeControl_7.Login login = new SmartEyeControl_7.Login();

                again:
                if (flag)
                {
                    login = new Login(true);
                    login.Visible = false;
                }
                bool ISlogin = login.handleLogin();
                if (!ISlogin)
                {
                    Application.ExitThread();
                    Environment.Exit(0);
                }
                else
                {
                    var usr = dbController.verifyUserAndGetDetails(login.txt_login.Text, login.txt_password.Text);
                    Application_Controller.CurrentUser = usr;
                    if (Application_Controller.CurrentUser == null)
                    {
                        flag = true;
                        goto again;
                    }

                    int hWnd;
                    Process[] processRunning = Process.GetProcesses();
                    foreach (Process pr in processRunning)
                    {
                        if (pr.ProcessName == "SmartEyeAdvanceControl-7")
                        {
                            hWnd = pr.MainWindowHandle.ToInt32();
                            ShowWindow(hWnd, 3); //3 is the enum for maximize
                        }
                    }
                }

                #endregion

                Thread t = new Thread(new ThreadStart(SplashLoad));
                t.Start();

                Application.DoEvents();
                this.WindowState = FormWindowState.Minimized;

                #region Controller Initialization
                if (pnlBilling1 != null)
                {
                    this.pnlBilling1.Application_Controller = Application_Controller;
                }
                if (Panel_Debugging != null)
                {
                    this.Panel_Debugging.Application_Controller = Application_Controller;
                }
                if (Panel_Events != null)
                {
                    this.Panel_Events.Application_Controller = Application_Controller;
                }
                if (pnlParameterization1 != null)
                {
                    this.pnlParameterization1.Application_Controller = Application_Controller;
                }
                if (pnlLoad_Profile != null)
                {
                    pnlLoad_Profile.Application_Controller = Application_Controller;
                }
                if (Panel_Instantaneous != null)
                {
                    Panel_Instantaneous.Application_Controller = Application_Controller;
                }

                #endregion

                try
                {
                    #region ICONS
                    //backgroundGradient = new Bitmap(ImageFolderPath + "bg.png");


                    billing = new Bitmap(ImageFolderPath + "billing.png");
                    billing_active = new Bitmap(ImageFolderPath + "billing_active.png");

                    loadprofile = new Bitmap(ImageFolderPath + "loadprofile.png");
                    loadprofile_active = new Bitmap(ImageFolderPath + "loadprofile_active.png");

                    debug = new Bitmap(ImageFolderPath + "debug.png");
                    debug_active = new Bitmap(ImageFolderPath + "debug_active.png");

                    events = new Bitmap(ImageFolderPath + "events.png");
                    events_active = new Bitmap(ImageFolderPath + "events_active.png");

                    instantaneous = new Bitmap(ImageFolderPath + "instantaneous.png");
                    instantaneous_active = new Bitmap(ImageFolderPath + "instantaneous_active.png");

                    loadprofile = new Bitmap(ImageFolderPath + "loadprofile.png");
                    loadprofile_active = new Bitmap(ImageFolderPath + "loadprofile_active.png");

                    help = new Bitmap(ImageFolderPath + "help.png");
                    help_active = new Bitmap(ImageFolderPath + "help_active.png");

                    parameterization = new Bitmap(ImageFolderPath + "parameterization.png");
                    parameterization_active = new Bitmap(ImageFolderPath + "parameterization_active.png");

                    modem = new Bitmap(ImageFolderPath + "modem.png");
                    modem_active = new Bitmap(ImageFolderPath + "modem_active.png");

                    settings = new Bitmap(ImageFolderPath + "settings.png");
                    settings_active = new Bitmap(ImageFolderPath + "settings_active.png");

                    logOff = new Bitmap(ImageFolderPath + "logOff.png");
                    logOff_active = new Bitmap(ImageFolderPath + "logOff_active.png");

                    admin = new Bitmap(ImageFolderPath + "admin.png");
                    admin_active = new Bitmap(ImageFolderPath + "admin_active.png");
                    #endregion
                    CheckForIllegalCrossThreadCalls = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error setting Theme.  " + ex.Message);
                }

                /// store previous size and then resize
                size_pnl_comm = pnlCommunication.Size;
                /// Initialize Categories List
                Populate_Categories_Items();
                /// assign to static objects
                MsgDisplay.MessageViewer = Panel_Debugging.txtMessageViewer;
                MsgDisplay.ObisCodesList = Panel_Debugging.lbxObisCodesViewer;
                MsgDisplay.ObisCodesTree = Panel_Debugging.trvObisCodesViewer;
                MsgDisplay.CommunicationViewer = Panel_Debugging.txtCommViewer;
                MsgDisplay.ReadResponseViewer = Panel_Debugging.txtIDecodedOutput;
                MsgDisplay.WriteResponseViewer = Panel_Debugging.txtIWroteResp;

                #region // assign event_handler

                Panel_Debugging.lbxObisCodesViewer.MouseDoubleClick += new
                           MouseEventHandler(lbxObisCodesViewer_MouseDoubleClick);
                Panel_Debugging.btnReadObisCodes.Click += new EventHandler(btnReadObisCodes_Click);
                Panel_Debugging.btnC2Coutput.Click += new EventHandler(btn_pnl_DebuggingC2Coutput_Click);
                Panel_Debugging.btnC2CResponse.Click += new EventHandler(btn_pnl_DebuggingC2CResponse_Click);
                Panel_Debugging.btnClearOutput.Click += new EventHandler(btn_pnl_Debugging_ClearOutput_Click);
                Panel_Debugging.btnIReadAttrib.Click += new EventHandler(btnIReadAttrib_Click);
                MsgDisplay.ObisCodesTree.AfterSelect += new TreeViewEventHandler(ObisCodesTree_AfterSelect);
                ///Setting Maximum Form Size
                Rectangle nativeScreen = Screen.PrimaryScreen.Bounds;
                this.MaximizedBounds = nativeScreen;
                this.Size = new Size(nativeScreen.Width, nativeScreen.Height);
                progressDialog = new ProgressDialog();
                progressDialog.okButton.Visible = false;
                ConnController.ProcessStatus += new Action<string>(progressDialog.ConnController_ProcessStatusHandler);
                this.pic_Parameterization.Click += new EventHandler(pic_Parameterization_Click);
                this.pic_Instantaneous.Click += new EventHandler(pic_Instantaneous_Click);
                this.pic_Billing.Click += new EventHandler(pic_Billing_Click);
                this.pic_Events.Click += new EventHandler(pic_Events_Click);
                this.pic_LoadProfile.Click += new EventHandler(pic_LoadProfile_Click);
                this.pic_settings.Click += new EventHandler(pic_settings_Click);
                this.pic_AboutMe.Click += new EventHandler(pic_AboutMe_Click);
                this.pic_Debug.Click += new EventHandler(pic_Debug_Click);

                #endregion
                //*****************************************
                MeterSerial = ConnController.MeterSerial;
                //*****************************************
                allowshowdisplay = true;
                cmbManufacturers.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        #region Icon Image Click
        void pic_Debug_Click(object sender, EventArgs e)
        {
            showDebug();
        }

        void pic_AboutMe_Click(object sender, EventArgs e)
        {
            aboutMeToolStripMenuItem_Click(this, new EventArgs());
            //Panel_Welcome.BringToFront();
            //unClickAll();
        }

        void pic_settings_Click(object sender, EventArgs e)
        {
            ShowSettings();
        }

        void pic_LoadProfile_Click(object sender, EventArgs e)
        {
            showLoadProfile();
        }

        void pic_Events_Click(object sender, EventArgs e)
        {
            showEvents();
        }

        void pic_Billing_Click(object sender, EventArgs e)
        {
            showBilling();
        }

        void pic_Instantaneous_Click(object sender, EventArgs e)
        {
            showInstantaneous();
        }

        void pic_Parameterization_Click(object sender, EventArgs e)
        {
            showParameterization(false);
        }
        #endregion

        private void txtAssociationPaswd_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(e.KeyChar) == 0x1B ||
                    Convert.ToInt32(e.KeyChar) == 0x7F)
                {
                    txtAssociationPaswd.Text = "mtiDLMScosem";
                }
            }
            catch (Exception)
            { }
        }

        private void FrmContainer_Load(object sender, EventArgs e)
        {
            //v8.4.26
            clsStatus.evSetProgressBarVisibility += new clsStatus.EventDelegate(clsStatus_setProgressBarVisibility);
            clsStatus.evSetStatusMessage += new clsStatus.EventStatusMessageDelegate(clsStatus_evSetStatusMessage);

            try
            {
                #region ///Init Debugger & Logger

                _debugger.GetDLMSLoggersDlg = new Action<System.Collections.ArrayList>(Get_DLMSLogger);
                _debugger.IOLog += new Action<string, byte[], DataStatus, DateTime>(Debugger_IOLog);
                _debugger.Logger += new LogMessage(Debugger_Logger);

                try
                {
                    _debugger.EnableErrorLog = OptocomSoftware.Properties.Settings.Default.IsEnableErrorInfo;
                    _debugger.EnableIOLog = OptocomSoftware.Properties.Settings.Default.IsEnableIOLog;
                    _debugger.EnableProcessInfoLog = OptocomSoftware.Properties.Settings.Default.IsEnableMessageInfoLog;
                }
                catch (Exception)
                {
                    _debugger.EnableErrorLog = true;
                    _debugger.EnableIOLog = false;
                    _debugger.EnableProcessInfoLog = false;
                }
                if (string.IsNullOrEmpty(Application_Process.Logger.Identifier))
                    Application_Process.Logger.Identifier = "(No MSN)";
                _debugger.Register = Application_Process.Logger;
                _debugger.Reset_Debugger();

                #endregion
                Application_Controller.PropertyChanged += new PropertyChangedEventHandler(Application_Controller_PropertyChanged);
                connectionManager.IOChannelDisconnect += new Action<IOConnection, string>(connectionManager_IOChannelDisconnect);
                connectionManager.IOActivity += new Action<IOConnection, IOActivityType>(connectionManager_IOActivity);
                MeterSerial = ConnController.MeterSerial;
                //IpConnContextMenuItem event handler
                GetMeterInfoMenuItem.Click += new EventHandler(GetMeterInfoMenuItem_Click);
                DropConnMenuItem.Click += new EventHandler(DropConnMenuItem_Click);
                readMeterInfoMenuItem.Click += new EventHandler(readMeterInfoMenuItem_Click);
                removeListMenuItem.Click += new EventHandler(removeListMenuItem_Click);

                LoadMeter_Config();

                string[] serialPorts = SerialPort.GetPortNames();
                //try
                //{
                //    cmbSerialPorts.Items.Clear();
                //    foreach (String port in serialPorts)
                //    {
                //        cmbSerialPorts.Items.Add(port);
                //    }
                //}
                //catch (Exception)
                //{
                //    Notification notifier = new Notification("Serial Port Init Error", "Error in adding Serial Ports in List");
                //}
                if (serialPorts != null)
                    Array.Sort<String>(serialPorts);
                cmbIRPorts.Items.Clear();
                try
                {
                    foreach (var port in serialPorts)
                    {
                        cmbIRPorts.Items.Add(port);
                    }

                    if (!String.IsNullOrEmpty(ConnController.CommunicationParams.IRCOMPort) &&
                       Array.Exists<String>(serialPorts, (x) => x.Equals(ConnController.CommunicationParams.IRCOMPort)))    ///Check Port exist In local list
                    {
                        cmbIRPorts.SelectedItem = ConnController.CommunicationParams.IRCOMPort;
                    }
                    else
                        cmbIRPorts.SelectedIndex = 0;
                }
                catch (Exception)
                {
                    Notification notifier = new Notification("IR Port Error", "Error in adding IR Ports in List");
                }

                cmbIOConnections.Items.Clear();
                
                foreach (var item in Enum.GetNames(typeof(IOConnectionType)))
                        this.cmbIOConnections.Items.Add(Enum.Parse(typeof(IOConnectionType), item));

                Process_AfterLogin();
                if (cmbManufacturers.Items.Count > 0) cmbManufacturers.SelectedIndex = 0;

                if (cmbIOConnections.Items.Count > 0)
                {
                    bool isHdlcEnabled = false;
                    foreach (var item in cmbIOConnections.Items)
                    {
                        if (item.ToString() == Enum.GetName(typeof(IOConnectionType), IOConnectionType.HDLC_MODE_E))
                        {
                            cmbIOConnections.SelectedItem = item;
                            isHdlcEnabled = true;
                            break;
                        }
                    }
                    if (!isHdlcEnabled) cmbIOConnections.SelectedIndex = 0;
                }


#if AppLoginSkip
                btnConnectApplication_Click(btnConnectApplication, new EventArgs()); //for Testing
#endif
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ////////// Added by Azeem Inayat
                this.WindowState = FormWindowState.Maximized;
                this.BringToFront();
            }
        }

        #region Progress Bar and Status Message
        //=====================================================================
        private void clsStatus_setProgressBarVisibility(bool visibility)
        {
            //progressBarMain.Visible = visibility;
            tsProgressBar.Visible = visibility;

            //picPakistan.Visible = picPakistan.Enabled = false;
            //Thread th = new Thread(new ThreadStart(() =>
            //{
            //    picPakistan.Image = AccurateOptocomSoftware.Properties.Resources._120px_Nuvola_Pakistani_flag_svg;
            //picPakistan.Visible = picPakistan.Enabled = true;
            //    picPakistan.Refresh();
            //}));
            //th.Start();
        }
        //=====================================================================
        private void clsStatus_evSetStatusMessage(string msg, Color textColor)
        {
            //lblStatus.ForeColor = textColor;
            //lblStatus.Text = msg;
            stlblStatus.ForeColor = textColor;
            stlblStatus.Text = msg;
        }
        //=====================================================================
        #endregion
        void ObisCodesTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            byte Attribute = 0;
            StOBISCode OBIS_Code = DecodeObisCodeFromListViewer(ref Attribute);
            Panel_Debugging.txtClassId.Text = OBIS_Code.ClassId.ToString("X2");
            Panel_Debugging.txtA.Text = OBIS_Code.OBISCode_Feild_A.ToString("X2");
            Panel_Debugging.txtB.Text = OBIS_Code.OBISCode_Feild_B.ToString("X2");
            Panel_Debugging.txtC.Text = OBIS_Code.OBISCode_Feild_C.ToString("X2");
            Panel_Debugging.txtD.Text = OBIS_Code.OBISCode_Feild_D.ToString("X2");
            Panel_Debugging.txtE.Text = OBIS_Code.OBISCode_Feild_E.ToString("X2");
            Panel_Debugging.txtF.Text = OBIS_Code.OBISCode_Feild_F.ToString("X2");
            Panel_Debugging.txtAttribute.Text = Attribute.ToString();
        }

        private Get_Index DecodeObisCodeFromListViewer(ref byte Selected_Attribute)
        {
            if (MsgDisplay.ObisCodesTree.SelectedNode == null)
            {
                MsgDisplay.ReadResponseViewer.Text = "Select an Object First";
                return Get_Index.Dummy;
            }
            UInt16 Selected_Node_Index;
            Get_Index OBIS_Code = Get_Index.Dummy;

            Selected_Node_Index = LocalCommon.IndexofMainNode(MsgDisplay.ObisCodesTree.SelectedNode);
            string sample = MsgDisplay.ObisCodesTree.Nodes[(UInt16)Selected_Node_Index].ToString();
            sample = sample.Substring(10, sample.Length - 10);

            // get number of attribute to be requested
            Selected_Attribute = (byte)(LocalCommon.FindSelectedChildNode(MsgDisplay.ObisCodesTree.SelectedNode) + 1);
            try
            {
                OBIS_Code = (Get_Index)Enum.Parse(typeof(Get_Index), sample);
            }
            // Selected object is not present in our enum list
            catch (Exception)
            {
                OBIS_Code = Get_Index.Dummy;
            }
            return OBIS_Code;
        }
        //public void Init_MeterModels(bool validate = false)
        //{
        //    try
        //    {
        //        cmbManufacturers.Items.Clear();
        //        //cmbMeterModel.SelectedIndex = -1;

        //        for (int i = 0; i < Application_Controller.CurrentUser.AccessRights_Model.Count; i++)
        //        {
        //            cmbManufacturers.Items.Add(Application_Controller.CurrentUser.AccessRights_Model[i].MeterModel);
        //        }

        //        if (cmbManufacturers.Items.Count > 0) cmbManufacturers.SelectedIndex = 0;
        //        //Application_Controller.CurrentMeterName = cmbMeterModel.SelectedItem.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    finally
        //    {
        //        if (cmbManufacturers.Items.Count <= 0)
        //        {
        //            //Disable Select Meter Model Box
        //            gpClientConfig.Enabled = false;
        //            cmbAssociations.Items.Clear();
        //            cmbDevices.Items.Clear();
        //        }
        //        else
        //        {
        //            gpClientConfig.Enabled = true;
        //        }
        //    }
        //}

        private void LoadMeter_Config()
        {
            try
            {
                // ConfigsHelper ConfigLoader = new ConfigsHelper(ConnController.Configurations);
                // MeterSAPConfiguration Configs = ConfigLoader.GetAllMeterSAPConfiguration();

                MeterConfig model_Selected = null;

                // List<MeterUser> usersList = ConfigLoader.LoadAllUsers();

                List<Manufacturer> Manufacturers = ConfigLoader.LoadAllManufacturers();
                if (Manufacturers == null) return;
                cmbManufacturers.Items.Clear();
                foreach (var item in Manufacturers)
                {
                    cmbManufacturers.Items.Add(item);
                }

                // if (Manufacturers.Count > 0)
                // {
                //     LoadDevice(Manufacturers[0]);
                // }


                // if (Manufacturers.Count > 0)
                // {
                //     if (cmbManufacturers.Enabled)
                //     {
                //         cmbManufacturers.SelectedIndex = 0;
                //     }
                // }
                // else
                // {
                //     cmbManufacturers.Enabled = false;
                // }

                #region Load Authentication Types

                // List<AuthenticationType> AuhtenticationTypes = ConfigLoader.LoadAllAuthenticationTypes();
                // if (AuhtenticationTypes == null) return;
                // cmbAssociations.Items.Clear();
                // foreach (var item in AuhtenticationTypes)
                // {
                //     cmbAssociations.Items.Add(item);
                // }
                // if (AuhtenticationTypes.Count > 0)
                // {
                //     if (cmbAssociations.Enabled)
                //     {
                //         cmbAssociations.SelectedIndex = 0;
                //     }
                // }
                // else
                // {
                //     cmbAssociations.Enabled = false;
                // }


                #endregion // Load Authentication Types

                #region Default_Select_Meter

                //model_Selected = Configs.SAPConfig.Find((x) => x.MeterModel.ToLower().Contains("r326"));

                //if (model_Selected == null ||
                //    model_Selected.SapConfigs.Count < 0)
                //{
                //    model_Selected = Configs.SAPConfig.Find((x) => x.MeterModel.ToLower().Contains("t421"));
                //}
                //else if (model_Selected == null ||
                //    model_Selected.SapConfigs.Count < 0)
                //{
                //    model_Selected = Configs.SAPConfig.Find((x) => x.MeterModel.ToLower().Contains("r421"));

                //}
                //else if (model_Selected == null ||
                //    model_Selected.SapConfigs.Count < 0)
                //{
                //    model_Selected = Configs.SAPConfig.Find((x) => x.MeterModel.ToLower().Contains("r411"));

                //}
                //else if (model_Selected == null ||
                //    model_Selected.SapConfigs.Count < 0)
                //{
                //    model_Selected = Configs.SAPConfig.Find((x) => x.MeterModel.ToLower().Contains("r288"));

                //}

                //List<SAP_Object> MeterSAPs = null;
                //List<SAP_Object> ClientSAPs = null;
                //ConnController.GetSAP(out MeterSAPs, out ClientSAPs, model_Selected);
                // Application_Controller.CurrentMeterName = model_Selected.MeterModel;

                #endregion

            }
            catch // (Exception ex)
            {
                //  cmbMeterUsers.Enabled = false;
            }
        }

        private void LoadDevice(Manufacturer _manufacturer)
        {
            // ConfigsHelper configHelper = new ConfigsHelper(ConnController.Configurations);
            List<Device> Devices = ConfigLoader.LoadDevicesByManufacturers(_manufacturer.Id);

            cmbDevices.Items.Clear();
            foreach (var item in Devices)
            {
                cmbDevices.Items.Add(item);
            }

        }

        private void LoadDeviceAssociation(Device device)
        {
            try
            {
                int deviceId = ((Device)this.cmbDevices.SelectedItem).Id;
                // ConfigsHelper configHelper = new ConfigsHelper( ConnController.Configurations );

                List<DeviceAssociation> Associations = ConfigLoader.LoadDeviceAssociations(device.Id);
                cmbAssociations.Items.Clear();
                foreach (var item in Associations)
                {
                    cmbAssociations.Items.Add(item);
                }


            }
            catch (Exception)
            {
            }
        }

        public String Get_ApplicationTitle()
        {
            String Soft_Title = "OptoCom Software";
            try
            {
                AboutBox1 About_Soft = new AboutBox1(0);

                int dotIndex = -1;
                int dotCount = 0;
                String Trim_Version = About_Soft.AssemblyVersion;

                //#region Trim_Version

                //while (dotCount < 3) //3 instead of 2 // By Azeem //v10.0.21
                //{
                //    dotIndex = Trim_Version.IndexOf('.', dotIndex + 1);
                //    if (dotIndex != -1)
                //    {
                //        dotCount++;
                //    }
                //    else
                //        break;
                //}
                //if (dotIndex != -1)
                //{
                //    Trim_Version = Trim_Version.Substring(0, dotIndex);
                //}

                //#endregion

                Soft_Title = String.Format("{0} v{1}", About_Soft.AssemblyTitle, Trim_Version);
                return Soft_Title;
            }
            catch
            {
            }
            return Soft_Title;
        }

        //private void LoadMeterSAPConfig(String mtrModel)
        //{
        //    try
        //    {
        //        ConfigsHelper ConfigLoader = new ConfigsHelper(ConnController.Configurations);
        //        MeterSAPConfiguration Configs = ConfigLoader.GetAllMeterSAPConfiguration();
        //        MeterConfig MeterConf = null;
        //        foreach (var conf in Configs.SAPConfig)
        //        {
        //            if (conf.Model.Equals(mtrModel, StringComparison.OrdinalIgnoreCase))
        //            {
        //                MeterConf = conf;
        //                break;
        //            }
        //        }
        //        List<String> meterSAPS, clientSap;
        //        ConnController.GetSAP(out meterSAPS, out clientSap, MeterConf);
        //        cmbMeterSAP.Items.Clear();
        //        cbxClientSap.Items.Clear();
        //        foreach (var item in meterSAPS)
        //        {
        //            cmbMeterSAP.Items.Add(item);
        //        }
        //        foreach (var item in clientSap)
        //        {
        //            cbxClientSap.Items.Add(item);
        //        }
        //        if (meterSAPS.Count > 0 && clientSap.Count > 0)
        //        {
        //            cmbMeterSAP.Enabled = true;
        //            cbxClientSap.Enabled = true;
        //            if (cmbMeterSAP.Enabled || ConnController.ConnectionManager == null ||
        //                ConnController.ConnectionManager.ConnectionInfo == null)
        //            {
        //                cmbMeterSAP.SelectedIndex = 0;
        //                cbxClientSap.SelectedIndex = 0;
        //            }
        //            else
        //            {
        //                cmbMeterSAP.Text = ConnController.ConnectionManager.ConnectionInfo.CurrentMeterSAP.ToString();
        //                cbxClientSap.Text = ConnController.ConnectionManager.ConnectionInfo.CurrentClientSAP.ToString();
        //            }
        //        }
        //        else
        //        {
        //            cmbMeterSAP.Enabled = false;
        //            cbxClientSap.Enabled = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        cmbMeterSAP.Enabled = false;
        //        cbxClientSap.Enabled = false;
        //    }
        //}

        //private List<String> LoadMeterModels()
        //{
        //    try
        //    {
        //        List<String> models = new List<string>();
        //        ConfigsHelper ConfigLoader = new ConfigsHelper(ConnController.Configurations);
        //        MeterSAPConfiguration Configs = ConfigLoader.GetAllMeterSAPConfiguration();
        //        foreach (var conf in Configs.SAPConfig)
        //        {
        //            if (!models.Exists((x) => conf.Model.Equals(x)))
        //                models.Add(conf.Model);
        //        }
        //        return models;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error occurred while loading meter model info", ex);
        //    }
        //}

        private void cmbMeterModel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Function used to display value received for debug panel
        /// </summary>
        /// <param name="arg"></param>
        ///@@@
        private void Application_Process_ReceiveData(Base_Class arg)
        {
            #region Class_15 Code
            if (arg.GetType() == typeof(Class_15))       ///OBIS Codes List Received
            {
                try
                {
                    //Clear Obis codes list box
                    MsgDisplay.ObisCodesList.Items.Clear();
                    //Tree
                    //MsgDisplay.ObisCodesTree.BeginUpdate();
                    MsgDisplay.ObisCodesTree.Nodes.Clear();

                    List<OBISCodeRights> obisCodes = ((Class_15)arg).OBISCodesReceived;
                    OBISCodeRights rights;
                    List<TreeNode> treeNodes = new List<TreeNode>();
                    for (int index = 0; index < obisCodes.Count; index++)
                    {
                        try
                        {
                            rights = obisCodes[index];
                            ///Foreach OBIS Code Received Upda'e
                            if (!rights.OBISIndex.IsValidate)
                            {
                                continue;
                            }
                            TreeNode mainNode = new TreeNode(rights.OBISIndex.OBISIndex.ToString());
                            ///MsgDisplay.AddObisCodetoList(((DLMS.Get_Index)indexOfOBISCode).ToString());
                            List<String> Atrribute_Names = Class_ID_Structures.Class_Attribute_Names(rights.ClassId);
                            ///For Each Attribute
                            for (int indexAttrib = 0; indexAttrib < rights.TotalAttributes; indexAttrib++)
                            {
                                TreeNode _t = new TreeNode(Atrribute_Names[indexAttrib] + ": " + ((Attrib_Access_Modes)rights.GetAttribRight(indexAttrib + 1)));
                                mainNode.Nodes.Add(_t);

                            }
                            treeNodes.Add(mainNode);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    AddObjects addNodeDelg = new AddObjects(MsgDisplay.AddNodesInTree);
                    Object[] objs = new object[1];
                    objs[0] = treeNodes.ToArray();
                    MsgDisplay.ObisCodesTree.Invoke(addNodeDelg, objs);
                }
                catch (Exception ex)
                {
                    MsgDisplay.AppendMsg("<<Error>>displaying decoded information");
                    MessageBox.Show("Error Updating OBIS Codes Tree.Msg:" + ex.Message);
                }
            }
            #endregion
            #region Class 17 Code
            else if (arg.GetType() == typeof(Class_17))
            {
                Use_SAP_List(((Class_17)arg).Server_Logical_Devices);
            }
            #endregion
            #region Class 7 & Billing Object
            #endregion
            #region Else
            else
            {
                try
                {
                    String StrVal = arg.ToString();
                    String[] Splits = StrVal.Split(",".ToCharArray());
                    MsgDisplay.ReadResponseViewer.Text = "";
                    StringBuilder _txt = new StringBuilder();
                    foreach (string val in Splits)
                        _txt.AppendLine(val);
                    MsgDisplay.ReadResponseViewer.Text += _txt.ToString();
                }
                catch (Exception ex)
                {
                    MsgDisplay.AppendMsg("<<Error>>displaying decoded information");
                }
            }
            #endregion
        }

        /// event Handler for AP Event Sap_List_Recieved
        #region Debugger_Loggers

        void Use_SAP_List(List<SAP_Object> SAPs)
        {
            //lbxMeterSAPs.Items.Clear();
            //foreach (SAP_Object Meter_SAP in SAPs)
            //{
            //    lbxMeterSAPs.Items.Add(Meter_SAP.SAP_Name);
            //}
        }

        void Debugger_Logger(string identifier, string msg, byte[] IODump, DateTime dtTimeStamp, LogType LogMessageType)
        {
            try
            {
                string hexDt = "";
                string msgtxt = "";
                if (IODump != null)
                {
                    hexDt = LocalCommon.ArrayToHexString(IODump);
                    msgtxt = String.Format("{0} {1:dd/MM/yyyy HH:mm:ss.ff}  {2}\r\nIODump:{3}", identifier, dtTimeStamp, msg, hexDt);
                }
                else
                {
                    msgtxt = String.Format("{0} {1:dd/MM/yyyy HH:mm:ss.ff}  {2}", identifier, dtTimeStamp, msg);
                }

                MsgDisplay.AppendMsg(msgtxt);

                string dir = string.Format(@"{0}\Logs\", LocalCommon.GetApplicationConfigsDirectory());
                DirectoryInfo dirInfo = new DirectoryInfo(dir);
                if (!dirInfo.Exists)
                    dirInfo.Create();
                string filename = null;
                if (LogMessageType == LogType.ErrorLog)
                    filename = string.Format("{0}{1}_Error.txt", dir, identifier);
                else
                    filename = string.Format("{0}{1}.txt", dir, identifier);
                Write_IOLog(filename, msgtxt);
            }
            catch (Exception ex)
            { }
        }

        void Debugger_IOLog(string identifier, byte[] IODump, DataStatus ReadStatus, DateTime dtTimeStamp)
        {
            try
            {
                string hexDt = "";
                if (IODump != null)
                    hexDt = LocalCommon.ArrayToHexString(IODump);
                if (ReadStatus == DataStatus.Write)
                    MsgDisplay.AppendCommReq(identifier, hexDt, dtTimeStamp);
                else if (ReadStatus == DataStatus.Read)
                    MsgDisplay.AppendCommResp(identifier, hexDt, dtTimeStamp);
                string msg = String.Format("{0} {1:dd/MM/yyyy HH:mm:ss.ff} {2}>>{3},", identifier, dtTimeStamp, ReadStatus, hexDt);
                string dir = string.Format(@"{0}\Logs\", LocalCommon.GetApplicationConfigsDirectory());
                DirectoryInfo dirInfo = new DirectoryInfo(dir);
                if (!dirInfo.Exists)
                    dirInfo.Create();
                string filename = string.Format("{0}{1}_IO.txt", dir, identifier);
                Write_IOLog(filename, msg);
            }
            catch (Exception ex) { }
        }

        private void Write_IOLog(string fileName, string IOLog)
        {
            try
            {
                using (StreamWriter wt = new StreamWriter(new FileStream(fileName, FileMode.Append, FileAccess.Write)))
                {
                    wt.WriteLine(IOLog);
                    wt.WriteLine();
                }
            }
            catch (Exception ex) { }
        }

        #endregion

        private void Populate_Categories_Items()
        {
            Categories_List_Items.Add("WelCome");
            Categories_List_Items.Add("Instantaneous");
            Categories_List_Items.Add("Parameterization");
            Categories_List_Items.Add("Load Profile");
            Categories_List_Items.Add("Billing Data");
            Categories_List_Items.Add("Events");
            Categories_List_Items.Add("Debugging");
        }
        private void btnHide_Click(object sender, EventArgs e)
        {
        }

        #region @@@ Commented Files

        #endregion

        /// <summary>
        /// Event Handlers used to take request from Debug panel to read physical quantity 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// @@@
        private async void lbxObisCodesViewer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                ListBox lbxObisCodes = (ListBox)sender;
                if (lbxObisCodes.Items.Count == 0)
                {
                    return;
                }
                Get_Index OBIS_Index = (Get_Index)Enum.Parse(typeof(Get_Index), (string)MsgDisplay.ObisCodesList.SelectedItem);

                if (AP_Controller.ApplicationProcess.Is_Association_Developed)
                {
                    Base_Class ObjRequest = AP_Controller.GetSAPEntry(OBIS_Index);
                    ObjRequest.DecodingAttribute = 0x00;
                    if (Application_Controller != null)
                        Application_Controller.IsIOBusy = true;
                    /// AP_Controller.GET(ObjRequest, this.Application_Process_ReceiveData);
                    await AP_Controller.GETAsync(ObjRequest);
                    // tk.Wait();
                    this.Application_Process_ReceiveData(ObjRequest);
                }
                else
                    throw new Exception("Application disconnected,Please connect application first");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (Application_Controller != null)
                    Application_Controller.IsIOBusy = false;
            }
        }

        void btn_pnl_Debugging_ClearOutput_Click(object sender, EventArgs e)
        {
            MsgDisplay.MessageViewer.Text = "";
        }

        void btn_pnl_DebuggingC2CResponse_Click(object sender, EventArgs e)
        {
            LocalCommon.TextCopy2Clipboard(Panel_Debugging.txtCommViewer);
            Panel_Debugging.txtCommViewer.Select(Panel_Debugging.txtCommViewer.Text.Length - 1, 0);
        }

        void btn_pnl_DebuggingC2Coutput_Click(object sender, EventArgs e)
        {
            LocalCommon.TextCopy2Clipboard(Panel_Debugging.txtMessageViewer);
            Panel_Debugging.txtMessageViewer.Select(0, 0);
        }

        /// Event Handler to read Obis codes in panel_debugging
        private async void btnReadObisCodes_Click(object sender, EventArgs e)
        {
            try
            {
                /// Async OBIS Code Read Call
                Base_Class base_arg = AP_Controller.GetSAPEntry(Get_Index.Current_Association);
                Application_Controller.IsIOBusy = true;

                base_arg.DecodingAttribute = 02;
                await AP_Controller.GETAsync(base_arg); ///.Wait();
                Application_Process_ReceiveData(base_arg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Unable To Read OBIS Code List" + ex.Message, "Error Reading OBIS Code", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (Application_Controller != null)
                    Application_Controller.IsIOBusy = false;
            }
        }

        /// @@@
        /// Event handler to read selected attrib of object from debugging panel
        void btnIReadAttrib_Click(object sender, EventArgs e)
        {
            AutoResetEvent waitHandler = new AutoResetEvent(false);
            BackgroundWorker bgw_local = null;

            try
            {
                Get_Index OBIS_Code = Get_Index.Dummy;
                StOBISCode OBISCodeT = Get_Index.Dummy;
                byte Selected_Attribute = 0;

                /*  if (!Panel_Debugging.chkReadManual.Checked)
                  {
                      OBIS_Code = DecodeObisCodeFromListViewer(ref Selected_Attribute);
                      OBISCodeT = OBIS_Code;
                  }
                  else*/
                {
                    OBISCodeT = StOBISCode.ConvertFrom(Panel_Debugging.txtClassId.Text.PadLeft(4, '0') + "." + Panel_Debugging.txtA.Text.PadLeft(2, '0') + "." + Panel_Debugging.txtB.Text.PadLeft(2, '0') + "." + Panel_Debugging.txtC.Text.PadLeft(2, '0') + "." + Panel_Debugging.txtD.Text.PadLeft(2, '0') + "." + Panel_Debugging.txtE.Text.PadLeft(2, '0') + "." + Panel_Debugging.txtF.Text.PadLeft(2, '0'));
                    OBIS_Code = OBISCodeT.OBISIndex;
                }
                if (OBIS_Code != Get_Index.Dummy)
                {
                    // Sync GET Method Call
                    // ASync Method Call
                    // Test Code To Exe
                    IAccessSelector descriptor = null;
                    try
                    {
                        Selected_Attribute = Convert.ToByte(Panel_Debugging.txtAttribute.Text);
                        if (Panel_Debugging != null && Panel_Debugging.chkEnableSelectiveAccess.Checked)
                        {
                            if (Panel_Debugging.tcAccessSelector.SelectedTab == Panel_Debugging.tpEntry)
                            {
                                EntryDescripter des = new EntryDescripter();
                                des.FromEntry = uint.Parse(Panel_Debugging.txtFromEntry.Text);
                                des.ToEntry = uint.Parse(Panel_Debugging.txtToEntry.Text);
                                des.FromSelectedValue = ushort.Parse(Panel_Debugging.txtFromSelectedVal.Text);
                                des.ToSelectedValue = ushort.Parse(Panel_Debugging.txtToSelectedVal.Text);
                                if (!des.IsValid())
                                    throw new Exception("Invalid Access Selection Range");
                                descriptor = des;
                            }
                            else if (Panel_Debugging.tcAccessSelector.SelectedTab == Panel_Debugging.tpRange)
                            {
                                RangeDescripter rDes = new RangeDescripter();
                                rDes.FromEntry = Panel_Debugging.dtpFrom.Value;
                                rDes.ToEntry = Panel_Debugging.dtpTo.Value;
                                rDes.EncodingDataType = DataTypes._A19_datetime;
                                descriptor = rDes;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid Access Selection Value," + ex.Message, "Invalid Access Selection");
                        return;
                    }

                    Base_Class ObisCode = AP_Controller.GET_Internal_Object(OBIS_Code, Selected_Attribute);
                    if (descriptor != null)
                    {
                        ObisCode.AccessSelector = descriptor;
                    }
                    else
                        ObisCode.AccessSelector = null;
                    ObisCode.DecodingAttribute = Selected_Attribute;

                    #region ASync Data Read Op

                    bgw_local = new BackgroundWorker();

                    bgw_local.DoWork += new DoWorkEventHandler(
                        (arg, bgwEvArg) =>
                        {
                            Application_Controller.IsIOBusy = true;
                            AP_Controller.GET(ObisCode);///, Application_Process_ReceiveData);
                        });
                    bgw_local.RunWorkerCompleted += new RunWorkerCompletedEventHandler((arg, bgwEvArg) =>
                    {
                        Application_Controller.IsIOBusy = false;
                        // if (bgwEvArg.Error != null)
                        // {
                        //     throw bgwEvArg.Error;
                        // }
                        waitHandler.Set();

                    });

                    // Run bgw Worker Thread
                    bgw_local.RunWorkerAsync(ObisCode);

                    do
                    {
                        waitHandler.WaitOne(500);
                        Application.DoEvents();
                    }
                    while (bgw_local.IsBusy);

                    #endregion

                    if (ObisCode.GetAttributeDecodingResult(Selected_Attribute) == DecodingResult.Ready)
                        Application_Process_ReceiveData(ObisCode);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Unable To Read Object" + ex.Message, "Error Reading Object", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region UnHandled Exception Handlers

        public void Application_UnhandledException(object sender, UnhandledExceptionEventArgs ex)
        {
            Application.DoEvents();
            //Exception _ex = (Exception)ex.ExceptionObject;
        }

        public void Appliction_ThreadException(Object Sender, ThreadExceptionEventArgs e)
        {
            Application.DoEvents();
            Exception _ex = e.Exception;
        }

        #endregion

        #region Background Worker Connect

        private void btnConnectApplication_Click(object sender, EventArgs e)
        {
            try
            {
                /// InitSecurityData();
                /// if (cbxClientSap.SelectedIndex > 1) 
                // else AP_Controller.Security_Data = null;
                unClickAll();
                showWelComePanel();
                ///Abort Previous Executing Action
                int count = 0;
                while (BckWorkerThread != null && BckWorkerThread.IsBusy)
                {
                    BckWorkerThread.CancelAsync();
                    count++;
                }
                Disable_IOActivity();

                BckWorkerThread = new BackgroundWorker();
                BckWorkerThread.WorkerSupportsCancellation = true;
                BckWorkerThread.DoWork += new DoWorkEventHandler(BckWorker_ConnectApplication_DoEventHandler);
                BckWorkerThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BckWorker_ConnectApplication_WorkCompleted);
                BckWorkerThread.RunWorkerAsync();
                Application_Controller.IsIOBusy = true;
                if (!ConnController.IsConnected)
                {
                    progressDialog.DialogTitle = "Connecting";
                }
                else
                {
                    progressDialog.DialogTitle = "Disconnect";
                }
                ///Configure Progress Dialog Box
                ///to Be Displayed
                progressDialog.DialogStatus = "";
                progressDialog.UserInputEnable = true;
                progressDialog.EnableProgressBar = true;
                progressDialog.IsAutoHideNow = false;

                DialogResult result = progressDialog.ShowDialog();
                DialogResult res = DialogResult.OK;
                if (BckWorkerThread.IsBusy && (result == DialogResult.OK || result == DialogResult.Cancel))
                {
                    res = MessageBox.Show(this, "Are you sure want to cancel current process immediately? Otherwise wait till process completes."
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
                MessageBox.Show("Msg:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (Application_Controller.CurrentUser != null &&
                   Application_Controller.CurrentUser.CurrentAccessRights != null && Application_Controller.CurrentUser.IsCurrentAccessRightsChanged)
                {
                    //new ApplicationRight_Helper().updateGUI(this, Application_Controller.CurrentUser);
                    //pnlParameterization1.ApplyAccessRights(Application_Controller.CurrentUser.CurrentAccessRights, Application_Controller.CurrentUser.userTypeID);
                    if (this.cmbSecurity.Items.Count > 0)
                    {
                        DeviceAssociation CurrentDeviceAssociation = (DeviceAssociation)cmbAssociations.SelectedItem;
                        if (CurrentDeviceAssociation.AuthenticationType >= HLS_Mechanism.HLS_Manufac)
                        {
                            this.cmbSecurity.SelectedIndex = 3;
                        }
                    }
                }
                UpdateConnectStatus(ConnController.IsConnected);
                //btnConnect.Enabled = true;
                if (Application_Controller.ConnectionManager.ConnectionInfo != null &&
                     Application_Controller.ConnectionManager.ConnectionInfo.MeterInfo != null &&
                     !string.IsNullOrEmpty(Application_Controller.ConnectionManager.ConnectionInfo.MeterInfo.MeterModel))
                {
                    cmbManufacturers.Text = Application_Controller.ConnectionManager.ConnectionInfo.MeterInfo.MeterModel;
                    //if (Application_Controller.ConnectionManager.ConnectionInfo.MeterInfo.MeterModel.Equals("R283"))
                    //{
                    //    Application_Controller.isSinglePhase = true;
                    //}
                    //else
                    //{
                    //    Application_Controller.isSinglePhase = false;
                    //}
                    Application_Controller.isSinglePhase = Application_Controller.ConnectionManager.ConnectionInfo.MeterInfo.Device.IsSinglePhase;
                }
            }
        }

        private void BckWorker_ConnectApplication_DoEventHandler(Object sender, DoWorkEventArgs arg)
        {
            th = new Thread(BckWorker_ConnectApplication_DoWork_Helper);
            th.Priority = ThreadPriority.AboveNormal;
            try
            {
                th.Start();
                ex = null;
                Thread.Sleep(1);
                while (!(th.ThreadState == System.Threading.ThreadState.Aborted || th.ThreadState == System.Threading.ThreadState.Stopped))
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
                    ConnController.ConnectionManager.OpticalPortConnection.DisConnectHdlc();
                    // Disconnect Application Physical Connection Here
                    try
                    {
                        if (AP_Controller.IsConnected)
                        {
                            bool flag = false;
                            // Async ARLR
                            AP_Controller.ARLRQAsync().Wait(500);
                            AP_Controller.GetCommunicationObject.Disconnect();
                        }
                    }
                    catch (Exception) { }
                    arg.Cancel = true;
                }
                if (ex != null)
                    throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (th != null)
                    th.Abort();
            }
        }

        public void ARLQCalBck(bool calBck)
        {
            try
            {
                Console.Out.WriteLine("ARLQ Status " + calBck);
            }
            catch (Exception ex) { }
        }

        private void BckWorker_ConnectApplication_DoWork_Helper()
        {
            // this.Reset_SoftwareState();
            try
            {

                if (!this.ConnController.IsConnected)
                {
                    //if (cmbAssociations.SelectedIndex < 0 || cmbDevices.SelectedIndex < 0 || cmbManufacturers.SelectedIndex < 0)
                    //{
                    //    progressDialog.UpdateDialogStatusHandler("Select Valid Configuration for Association");
                    //    return;
                    //}

                    // int DeviceId = ((Device)this.cmbDevices.SelectedItem).Id;
                    // int AuthenticationTypeId = ((AuthenticationType)this.cmbAssociations.SelectedItem).Id;

                    Device Dev = null;
                    DeviceAssociation Association = null;
                    progressDialog.okButton.Visible = false;
                    var MeterConfig = ConnController.SelectedMeter;

                    if (MeterConfig == null || MeterConfig.Device == null || MeterConfig.Device_Association == null)
                        throw new Exception("Unable to connect application,Meter Configuration Not Update");

                    Dev = MeterConfig.Device;
                    Association = MeterConfig.Device_Association;
                    ushort hdlcAddress = Convert.ToUInt16(txtHDLCAddress.Text);

                    switch (ConnController.ConnectionType)
                    {
                        case IOConnectionType.HDLC_MODE_E:
                            {
                                if (connectionManager.ConnectionInfo == null) connectionManager.ConnectionInfo = new ConnectionInfo();
                                String Port = cmbIRPorts.SelectedItem.ToString();
                                String meterSAPName = string.Empty;//cmbManufacturers.SelectedItem.ToString();

                                // String clientSAPName = cbxClientSap.SelectedItem.ToString();
                                // ConnController.Connect_IRLink(txtAssociationPaswd.Text, Port);

                                ConnController.Connect_IRLinkWithRights(txtAssociationPaswd.Text, Port, Association,
                                    OptocomSoftware.Properties.Settings.Default.SEAC7_DBConnectionString, hdlcAddress);
                                break;
                            }
                        case IOConnectionType.Direct_HDLC:
                            {
                                String Port = cmbIRPorts.SelectedItem.ToString();

                                ConnController.Connect_IRLink(txtAssociationPaswd.Text, Port,
                                    OptocomSoftware.Properties.Settings.Default.SEAC7_DBConnectionString, hdlcAddress);
                                break;
                            }
                        case IOConnectionType.IP_Link:
                            {

                                String txtPasswd = txtAssociationPaswd.Text;
                                IOConnection Conn = (IOConnection)cmbMeterSerial.SelectedItem;
                                if (Conn == null || !Conn.IsConnected)
                                {
                                    throw new Exception("Unable to connect application,IP Link is disconnected");
                                }
                                #region // Init Work Before Connection Works

                                if (Conn.ConnectionInfo == null)
                                {
                                    Conn.ConnectionInfo = new ConnectionInfo();

                                }

                                if (Conn.ConnectionInfo.MeterInfo == null)
                                {
                                    Conn.ConnectionInfo.MeterInfo = new MeterConfig(MeterConfig);
                                    // Conn.ConnectionInfo.MeterInfo.Version = null;
                                }

                                #endregion

                                ConnController.Connect_IPLink(Conn, txtPasswd, Association);
                                // ConnController.Connect_IPLink(Conn, txtPasswd);
                                // else ConnController.Connect_IPLink(Conn, true);

                                Application_Controller.ConnectToMeter = Conn;
                                break;
                            }
                        case IOConnectionType.IP_Ov_Serial_Link:
                            {

                                String Port = cmbIRPorts.SelectedItem.ToString();

                                // String meterSAPName = cmbMeterUsers.SelectedItem.ToString();
                                // String clientSAPName = cbxClientSap.SelectedItem.ToString();

                                String txtPasswd = txtAssociationPaswd.Text;
                                if (AP_Controller.IsConnected)
                                {
                                    throw new Exception("Unable to connect application,application is alreadyconnected");
                                }
                                ConnController.Connect_IRLink(txtPasswd, Port, OptocomSoftware.Properties.Settings.Default.SEAC7_DBConnectionString, hdlcAddress);
                                break;
                            }
                    }

                    if (MeterConfig != null && string.IsNullOrEmpty(MeterConfig.MeterModel))
                        Application_Controller.ConnectionManager.ConnectionInfo.MeterInfo = MeterConfig;

                    // Application Connected disable Controls
                    // Init Application Controllers Work
                    this.Application_Controller.Billing_Controller.CurrentConnectionInfo =
                         this.Application_Controller.ConnectionManager.ConnectionInfo;
                    this.Application_Controller.EventController.CurrentConnectionInfo =
                        this.Application_Controller.ConnectionManager.ConnectionInfo;
                    this.Application_Controller.Param_Controller.CurrentConnectionInfo =
                        this.Application_Controller.ConnectionManager.ConnectionInfo;
                    this.Application_Controller.LoadProfile_Controller.CurrentConnectionInfo =
                        this.Application_Controller.ConnectionManager.ConnectionInfo;

                    if (Application_Controller.ConnectionManager.ConnectionInfo != null)
                    {
                        // cmbMeterModel.Text = Application_Controller.ConnectionManager.
                        // ConnectionInfo.MeterInfo.Model.ToString();

                        if (Application_Controller.ConnectionManager.
                            ConnectionInfo.MeterInfo.MeterModel.ToString().Equals("R283"))
                        {
                            Application_Controller.isSinglePhase = true;
                        }
                        else
                        {
                            Application_Controller.isSinglePhase = false;
                        }
                    }
                }
                else
                {
                    ConnController.Disconnect_Old();
                    Application_Controller.ConnectToMeter = null;
                    Notification n = new Notification("Disconnected", "");
                    Application.DoEvents();
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

        //private void BckWorker_ConnectApplication_DoWork_Helper()
        //{
        //    //ahmed// this.Reset_SoftwareState();
        //    try
        //    {
        //        ConnController.ConnectionType = (IOConnectionType)cmbIOConnections.SelectedItem;

        //        if (!this.ConnController.IsConnected)
        //        {
        //            if (cmbMeterModel.SelectedItem == null || cmbMeterSAP.SelectedItem == null || cbxClientSap.SelectedItem == null)
        //                throw new Exception("Please Select Correct Meter Model");

        //            //v4.8.12 change start
        //            #region Password Key Implementation
        //            if (rdbPass_Key.Checked)
        //            {
        //                string PassKey = tbPasskey.Text.Trim().Replace("-", "");
        //                if (PassKey.Length > 8)
        //                {
        //                    if (!dbValidator.Validate_Database())   //v4.8.26
        //                    {
        //                        throw new Exception("DB Error 0x1");
        //                    }
        //                    txtAssociationPaswd.Text = "";
        //                    ObjPassKey = new PasswordKey();
        //                    ObjPassKey = pasKyMngr.Decrypt(PassKey, (uint)Application_Controller.Applicationprocess_Controller.SoftWareId);
        //                    txtAssociationPaswd.Text = ObjPassKey.Password;
        //                }
        //                else if (PassKey.Length > 0 && PassKey.Length <= 8)
        //                {
        //                    throw new Exception("Invalid PassKey");
        //                }
        //            }
        //            #endregion

        //            String meterSAPName = cmbMeterSAP.SelectedItem.ToString();
        //            String clientSAPName = cbxClientSap.SelectedItem.ToString();
        //            string txtPasswd = txtAssociationPaswd.Text;

        //            //v4.8.12 change end

        //            switch ((IOConnectionType)cmbIOConnections.SelectedItem)
        //            {
        //                case IOConnectionType.HDLC_MODE_E:
        //                    {
        //                        String Port = cmbIRPorts.SelectedItem.ToString();
        //                        ConnController.Connect_IRLink(meterSAPName, clientSAPName, txtPasswd, Port);
        //                        break;
        //                    }
        //                case IOConnectionType.Direct_HDLC:
        //                    {
        //                        String Port = cmbIRPorts.SelectedItem.ToString();
        //                        ConnController.Connect_IRLink(meterSAPName, clientSAPName, txtPasswd, Port);
        //                        break;
        //                    }
        //                case IOConnectionType.IP_Ov_Serial_Link:
        //                    {

        //                        String Port = cmbIRPorts.SelectedItem.ToString();
        //                        if (AP_Controller.IsConnected)
        //                        {
        //                            throw new Exception("Unable to connect Application,Application is already Connected");
        //                        }
        //                        ConnController.Connect_IRLink(meterSAPName, clientSAPName, txtPasswd, Port);
        //                        break;
        //                    }
        //                case IOConnectionType.IP_Link:
        //                    {
        //                        IOConnection Conn = (IOConnection)cmbMeterSerial.SelectedItem;
        //                        if (Conn == null || !Conn.IsConnected)
        //                        {
        //                            throw new Exception("Unable to connect application,IP Link is disconnected");
        //                        }
        //                        #region ///Init Work Before Connection Works
        //                        if (Conn.ConnectionInfo == null)
        //                        {
        //                            Conn.ConnectionInfo = new ConnectionInfo();

        //                        }
        //                        MeterConfig conf = ConnController.MeterSAPConfig;
        //                        if (Conn.ConnectionInfo.MeterInfo == null ||
        //                            Conn.ConnectionInfo.MeterInfo.ServerSAP.Count <= 0 ||
        //                            Conn.ConnectionInfo.MeterInfo.ClientSAP.Count <= 0)
        //                        {
        //                            Conn.ConnectionInfo.MeterInfo = new MeterConfig(conf);
        //                            Conn.ConnectionInfo.MeterInfo.Version = null;
        //                        }
        //                        SAPConfig CurrentMeterSAP = conf.ServerSAP.Find((x) => x.FaceName.Equals(meterSAPName, StringComparison.OrdinalIgnoreCase));
        //                        SAP_Object CurrentClientSAP = conf.ClientSAP.Find((x) => x.SAP_Name.Equals(clientSAPName, StringComparison.OrdinalIgnoreCase));
        //                        if (CurrentMeterSAP != null)
        //                        {
        //                            Conn.ConnectionInfo.CurrentMeterSAP = CurrentMeterSAP;
        //                            CurrentMeterSAP.DefaultPassword = txtPasswd;
        //                        }
        //                        if (CurrentClientSAP != null)
        //                        {
        //                            Conn.ConnectionInfo.CurrentClientSAP = CurrentClientSAP;
        //                        }
        //                        #endregion
        //                        ConnController.Connect_IPLink(Conn, true);
        //                        Application_Controller.ConnectToMeter = Conn;
        //                        break;
        //                    }

        //            }

        //            /// Application Connected disable Controls
        //            /// Init Application Controllers Work
        //            this.Application_Controller.Billing_Controller.CurrentConnectionInfo =
        //                           this.Application_Controller.ConnectionManager.ConnectionInfo;
        //            this.Application_Controller.EventController.CurrentConnectionInfo =
        //                this.Application_Controller.ConnectionManager.ConnectionInfo;
        //            this.Application_Controller.Param_Controller.CurrentConnectionInfo =
        //                this.Application_Controller.ConnectionManager.ConnectionInfo;
        //            this.Application_Controller.LoadProfile_Controller.CurrentConnectionInfo =
        //                this.Application_Controller.ConnectionManager.ConnectionInfo;

        //            if (Application_Controller.ConnectionManager.ConnectionInfo != null)
        //            {
        //                //cmbMeterModel.Text = Application_Controller.ConnectionManager.ConnectionInfo.MeterInfo.Model.ToString();
        //                foreach (string s in cmbMeterModel.Items)
        //                {
        //                    if (Application_Controller.ConnectionManager.OpticalPortConnection.ModeEController.DeviceAddress.Contains(s))
        //                        cmbMeterModel.Text = s;
        //                }
        //                if (Application_Controller.ConnectionManager.ConnectionInfo.MeterInfo.Model.ToString().Equals("R283"))
        //                {
        //                    Application_Controller.isSinglePhase = true;
        //                }
        //                else
        //                {
        //                    Application_Controller.isSinglePhase = false;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            ConnController.Disconnect();
        //            Application_Controller.ConnectToMeter = null;
        //            Notification n = new Notification("Disconnected", "");
        //            Application.DoEvents();
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

        private void BckWorker_ConnectApplication_WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                    throw e.Error;
                ///Application Connected disable Controls,Update Controls On Connection Establishment
                if (connectionManager.ConnectionInfo.IsConnected)
                {
                    ///Reload Application Configurations
                    if (ConnController.IsConfigurationUpated)
                    {
                        progressDialog.EnableProgressBar = false;
                        progressDialog.UpdateDialogStatusHandler("Loading Current Meter Configurations");
                        Application.DoEvents();
                        RefreshApplicationConfigurations();
                    }
                    Debug_LoadRightsList();
                    progressDialog.IsAutoHideNow = true;
                    //progressDialog.okButton.PerformClick();
                    progressDialog.Visible = false;
                }
                ///Reset Application State On Application ReConnection
                if (ConnController != null && ConnController.ResetState && ConnController.IsConnected)
                {
                    //Reset_SoftwareState(); //ahmed
                }

                //v4.8.12
                if (ConnController != null && ConnController.IsConnected)
                {
                    #region Pass-Key Login Info Update in DB

                    bool pkUpdated = false;
                    if (pasKyMngr.UpdateStatus_TimeConsumption(ObjPassKey, true))
                    {
                        ObjPassKey = pasKyMngr.SelectPasswordKey(ObjPassKey.PassKey); //Re-Updating object in Memory
                        pkUpdated = true;
                        timer_PassKey.Start();
                        if (pkUpdated)
                        {
                            Notification ConnctionNotfier = new Notification("OK", "ALL OK");
                        }
                        else
                        {
                            Notification ConnctionNotfier = new Notification("Wrong", "Fail pk");
                        }
                    }
                    #endregion
                }
                progressDialog.UpdateDialogStatusHandler("Process Completed");

            }
            catch (Exception ex)
            {
                String _txt = String.Format("Error occurred while connecting,Details:\r\n{0} {1}",
                    ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
                progressDialog.UpdateDialogStatusHandler(_txt);
            }
            finally
            {
                //UpdateConnectStatus(ConnController.IsConnected, "Connection completed");  //commented by Azeem
                progressDialog.EnableProgressBar = false;
                //progressDialog.okButton.Visible = true;
                //progressDialog.btnCancel.Visible = true;
                Application_Controller.IsIOBusy = false;
            }
        }

        public void Reset_SoftwareState()
        {
            try
            {
                progressDialog.UpdateDialogStatusHandler("Resetting previous software application state");
                ///Reset Application State
                this.Panel_Debugging.Reset_State();
                this.pnlBilling1.Reset_State();
                this.Panel_Events.Reset_State();
                this.Panel_Instantaneous.Reset_State(); //Enabled by Azeem Inayat
                this.pnlLoad_Profile.Reset_State();
                this.pnlParameterization1.Reset_State();
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Refresh GUI", ex.Message, 1500);
            }
        }

        public void Debug_LoadRightsList()
        {
            ///Upload Application Association 
            try
            {
                DeviceAssociation Association = (DeviceAssociation)cmbAssociations.SelectedItem;

                List<OBISCodeRights> OBISCodeRightsList = ConnController.GetCurrentOBISCodeRights(Association.RightGroupId, Association.DeviceId);
                if (OBISCodeRightsList != null)
                {
                    Base_Class OBIS_Rights = AP_Controller.GetSAPEntry(Get_Index.Current_Association);
                    if (OBIS_Rights is Class_15)
                    {
                        ((Class_15)OBIS_Rights).OBISCodesReceived = OBISCodeRightsList;
                        Application_Process_ReceiveData(OBIS_Rights);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void UpdateConnectStatus(bool status)
        {
            try
            {
                //cmbSecurity.Enabled = txtAK.Enabled = txtEK.Enabled = txtIC.Enabled = !status;
                if (status)
                {
                    cmbIRPorts.Enabled = false;
                    cmbAssociations.Enabled = cmbDevices.Enabled = cmbManufacturers.Enabled = false;
                    cmbIOConnections.Enabled = false;
                    //cmbMeterModel.Enabled = false;
                    txtAssociationPaswd.Enabled = false;
                    btnConnectApplication.Enabled = false;
                    ///lnk_Disconnect_forcedly.Enabled = false;
                    gpPhysicalConnection.Enabled = false;
                    ///Status Label
                    stStatus_lblConnnStatus.Visible = true;
                    stStatus_lblFirmwareVersion.Visible = true;
                    stStatus_lblSerialNo.Visible = true;

                    if (connectionManager.ConnectionInfo != null && connectionManager.ConnectionInfo.MeterInfo != null)
                    {
                        Notification ConnctionNotfier = new Notification("Connected", String.Format("MSN {0}\r\nMeter Model {1} ",
                        connectionManager.ConnectionInfo.MSN,
                        connectionManager.ConnectionInfo.MeterInfo.MeterModelSTR));
                        stStatus_lblFirmwareVersion.Text = String.Format("Version:{0}", connectionManager.ConnectionInfo.MeterInfo.Version);

                    }
                    else
                        stStatus_lblFirmwareVersion.Text = String.Format("Version:{0}", "---");

                    if (connectionManager.ConnectionInfo != null)
                    {
                        string model = null;

                        if (connectionManager.ConnectionInfo.MeterSerialNumberObj != null)
                        {
                            if (!string.IsNullOrEmpty(connectionManager.ConnectionInfo.MeterInfo.MeterModelSTR))
                            {
                                model = connectionManager.ConnectionInfo.MeterInfo.MeterModelSTR;
                            }
                            else
                                model = connectionManager.ConnectionInfo.MeterInfo.MeterModel;
                            //connectionManager.ConnectionInfo.MeterSerialNumberObj.GetMSNPrefix();   //v 318
                        }

                        string txt_label = null;
                        if (!String.IsNullOrEmpty(connectionManager.ConnectionInfo.MSN) && !String.IsNullOrEmpty(model))
                            txt_label = String.Format("Serial#{0},Info:{1}", connectionManager.ConnectionInfo.MSN, model);
                        else if (!String.IsNullOrEmpty(connectionManager.ConnectionInfo.MSN))
                            txt_label = String.Format("Serial#{0}", connectionManager.ConnectionInfo.MSN);
                        else
                            txt_label = String.Format("Serial#{0}", "###");
                        stStatus_lblSerialNo.Text = txt_label;
                    }
                    else
                        stStatus_lblSerialNo.Text = String.Format("Serial#{0}", "###");

                    btnConnectApplication.Text = "Disconnect";
                }
                else
                {
                    cmbIRPorts.Enabled = true;
                    cmbAssociations.Enabled = cmbDevices.Enabled = cmbManufacturers.Enabled = true;
                    // cmbMeterModel.Enabled = true;
                    cmbIOConnections.Enabled = true;

                    ///Status Label
                    stStatus_lblConnnStatus.Visible = false;
                    stStatus_lblFirmwareVersion.Visible = false;
                    stStatus_lblSerialNo.Visible = false;
                    stStatusIOActivity.Visible = false;
                    btnConnectApplication.Text = "Connect";


                    btnConnectApplication.Enabled = true;
                    gpPhysicalConnection.Enabled = true;
                    txtAssociationPaswd.Enabled = true;
                    //btnConnect.Enabled = true;
                }
                btnConnectApplication.Enabled = true;
                lnk_Disconnect_forcedly.Enabled = true;

            }
            catch (Exception ex)
            {

            }
        }

        private void cmbIOConnections_SelectedIndexChanged(object sender, EventArgs e)
        {
            ///Make IR Port Selection Option Visible
            if (ConnController != null)
                ConnController.ConnectionType = (IOConnectionType)cmbIOConnections.SelectedItem;

            gbHDLCAddress.Visible = !((IOConnectionType)cmbIOConnections.SelectedItem == IOConnectionType.IP_Link);
            txtHDLCAddress.Text = ConnController.GetDirectHDLCAddress((IOConnectionType)cmbIOConnections.SelectedItem).ToString();

            if ((IOConnectionType)cmbIOConnections.SelectedItem == IOConnectionType.HDLC_MODE_E
                || (IOConnectionType)cmbIOConnections.SelectedItem == IOConnectionType.Direct_HDLC
                || (IOConnectionType)cmbIOConnections.SelectedItem == IOConnectionType.IP_Ov_Serial_Link
                )
            {
                string[] serialPorts = SerialPort.GetPortNames();
                if (serialPorts != null)
                    Array.Sort<String>(serialPorts);
                cmbIRPorts.Items.Clear();
                //cmbBaudRate.SelectedItem = "4800";
                foreach (var port in serialPorts)
                {
                    cmbIRPorts.Items.Add(port);
                }
                try
                {
                    cmbIRPorts.SelectedItem = ConnController.CommunicationParams.IRCOMPort;     ///Load Saved COM Port

                }
                catch (Exception)
                {
                }

                ///Selected Port
                gp_IR_Port.Enabled = true;
                gp_IR_Port.Visible = true;
            }
            else
            {
                gp_IR_Port.Enabled = false;
                gp_IR_Port.Visible = false;
            }

            if ((IOConnectionType)cmbIOConnections.SelectedItem == IOConnectionType.IP_Link
                )
            {
                gp_WakeUp_Paras.Enabled = true;
                gp_WakeUp_Paras.Visible = true;



                gpMtrSerial.Visible = true;
                cmbMeterSerial.Visible = true;
                gpMtrSerial.Visible = true;
            }
            else
            {
                gp_WakeUp_Paras.Enabled = false;
                gp_WakeUp_Paras.Visible = false;

                gpMtrSerial.Visible = false;
                cmbMeterSerial.Visible = false;
            }
        }

        #endregion

        #region Background Worker DisConnect Forcedly

        private void btnDisconnectApplication_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult result_Last = MessageBox.Show("Msg:Are you sure,want to cancel current application process?", "Abort Current Application Process", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result_Last != DialogResult.OK)
                {

                    return;
                }
                if (BckWorkerThread != null && BckWorkerThread.IsBusy)
                {

                    ///Stop Current Work
                    BckWorkerThread.CancelAsync();
                }
                Application_Controller.IsIOBusy = true;
                lnk_Disconnect_forcedly.Enabled = false;

                MeterSerial.Clear(); //Remove all connections from List

                Disable_IOActivity();
                ConnController.Disconnect(true);
            }
            catch (Exception ex)
            {
                String _txt = String.Format("Error occurred while disconnecting,Details:\r\n{0} {1}",
                    ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
                progressDialog.UpdateDialogStatusHandler(_txt);
                progressDialog.ShowDialog(this);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
                UpdateConnectStatus(ConnController.IsConnected);
            }
        }

        private void BckWorker_DisConnectApplication_DoEventHandler(Object sender, DoWorkEventArgs arg)
        {
            try
            {
                ConnController.Disconnect(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BckWorker_DisConnectApplication_WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                    throw e.Error;
                ///Application Connected disable Controls,Update Controls On Connection Establishment
                if (!connectionManager.IsConnected)
                {
                    progressDialog.IsAutoHideNow = true;
                    progressDialog.Visible = false;
                }

                progressDialog.UpdateDialogStatusHandler("Process Completed");

            }
            catch (Exception ex)
            {
                String _txt = String.Format("Error occurred while connecting,Details:\r\n{0} {1}",
                    ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
                progressDialog.UpdateDialogStatusHandler(_txt);
            }
            finally
            {
                progressDialog.EnableProgressBar = false;
                progressDialog.UserInputEnable = true;
                progressDialog.EnableProgressBar = false;
                ///SetReadWriteStatus(false);
                Application_Controller.IsIOBusy = false;
                UpdateConnectStatus(ConnController.IsConnected);
            }
        }

        #endregion

        private void Disable_IOActivity()
        {
            try
            {
                cmbIRPorts.Enabled = false;
                cmbAssociations.Enabled = false;
                cmbDevices.Enabled = false;
                cmbIOConnections.Enabled = false;
                txtAssociationPaswd.Enabled = false;
                btnConnectApplication.Enabled = false;
                cmbManufacturers.Enabled = false;
                tbAuthenticationKey.Enabled = false;
                tbEncryptionKey.Enabled = false;
                tbInvocationCounter.Enabled = false;
                cmbSecurity.Enabled = false;
                btn_DefaultPwd.Enabled = false;

                ///lnk_Disconnect_forcedly.Enabled = false;
                gpPhysicalConnection.Enabled = false;
                ///Status Label
            }
            catch (Exception ex)
            { }
        }

        private void lnkbtnRefreshServer_Click(object sender, EventArgs e)
        {
            try
            {
                timer_Connect.Enabled = true;
                DialogResult result_Last = MessageBox.Show("Msg:Are you sure,want to restart TCP Listener?", "Restart TCP Listener", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (result_Last != DialogResult.OK)
                    return;
                if (ConnController.IsConnected)
                {
                    MessageBox.Show(this, "Application is connection,please disconnect first", "Disconnect Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                ///SetReadWriteStatus(false);
                Application_Controller.IsIOBusy = false;
                ConnController.ConnectionManager.TCP_CONController.TCPServer.LocalSocket = new IPEndPoint(ConnController.TCPParams.ServerIP, ConnController.TCPParams.ServerPort);
                ConnController.ConnectionManager.TCP_CONController.TCPTimeOut = ConnController.TCPParams.TCPIPTimeOut;
                ConnController.ConnectionManager.TCP_CONController.IsTCPIpTimeOut = ConnController.TCPParams.IsTCPTimeOut;
                ConnController.ConnectionManager.TCP_CONController.RestartServer();
                ConnController.ConnectionManager.ResetConnectionStatusRunner();
                MessageBox.Show(this, "TCP Listener Restarted", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "TCP Listener not Restarted " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void lnkTCPStatus_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                String TcpServerStatus = "";
                String TcpClientStatus = "";
                String heartBeatStatus = "";
                if (!ConnController.ConnectionManager.TCP_CONController.IsServerListening)
                {
                    TcpServerStatus = "TCP Listener is not running";
                    TcpClientStatus = "No TCP Client Connection exists";
                }
                else
                {
                    TcpServerStatus = String.Format("TCP Listener is listening {0}", ConnController.ConnectionManager.TCP_CONController.TCPServer.LocalSocket.ToString());
                    if (ConnController.ConnectionManager.IsConnected)
                        TcpClientStatus = String.Format("{0} TCP Clients are connected", ConnController.ConnectionManager.IOConnectionsList.Count);
                    else
                        TcpClientStatus = String.Format("No TCP Client Connection exists");
                    HeartBeat hbeat = ConnController.ConnectionManager.LastHeartBeatReceived;
                    heartBeatStatus = (hbeat != null && hbeat.IsVerifited) ? hbeat.ToString() : "";
                }
                MessageBox.Show(this, String.Format("TCP Server Client Connection Status\r\n{0}\r\n{1}\r\nLast Heart Beat received {2}", TcpServerStatus, TcpClientStatus, heartBeatStatus), "TCP Connection Status", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error getting TCP Connection Status" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Configuration Menu Item Clicks

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration_parameters paramsDialog = new Configuration_parameters();
            paramsDialog.ShowDialog();
            cmbIOConnections_SelectedIndexChanged(cmbIOConnections, new EventArgs());
        }

        private void hDLCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration_parameters paramsDialog = new Configuration_parameters();
            paramsDialog.ShowDialog();
        }

        #endregion

        private void FrmContainer_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool isCancel = e.Cancel;
            if (Application_Controller != null)
                Application_Controller.PropertyChanged -= Application_Controller_PropertyChanged;
            DisposeOffApplication(ref isCancel);

        }
        private void FrmContainer_FormClosed(object sender, FormClosedEventArgs e)
        {
            bool isCancel = false;
            DisposeOffApplication(ref isCancel);

        }
        private void DisposeOffApplication(ref bool isCancel)
        {
            ///May Cancel Application Closeing Event Here
            if (!isCancel)
            {
                if (Application_Controller != null)
                    Application_Controller.Dispose();
                Application_Controller = null;
                if (_debugger != null)
                    _debugger.Dispose();
                if (th != null)
                {
                    th.Abort();
                }
                this.Dispose();
                Application_Process = null;
                Client = null;
                if (connectionManager != null)
                {
                    connectionManager.Dispose();
                }
                connectionManager = null;
                ConnController = null;
                AP_Controller = null;
                Application_Controller = null;
                BckWorkerThread = null;
                progressDialog = null;
                Application.Exit();
                Environment.Exit(1);
            }

        }
        private void aboutMeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 AboutBox = new AboutBox1(Application_Controller.Applicationprocess_Controller.SoftWareId);
            AboutBox.ShowDialog(this);
        }

        private void applicationConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ShowSettings();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void ShowSettings()
        {
            //this.Panel_Welcome.BringToFront();
            try
            {
                SmartEyeControl_7.ApplicationGUI.GUI.ApplicationConfiguration ApplicationConfig = new SmartEyeControl_7.ApplicationGUI.GUI.ApplicationConfiguration();
                String path = String.Format(@"{0}\Configs.dat", LocalCommon.GetApplicationConfigsDirectory());
                ApplicationConfig.DefaultURLPath = path;
                DialogResult result = ApplicationConfig.ShowDialog(this);
                this.ConfigLoader.LoadConfiguration(OptocomSoftware.Properties.Settings.Default.SEAC7_DBConnectionString);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

        }
        private void showParameterization(bool ISmodem)
        {
            //this.Panel_Welcome.BringToFront();
            if (pnlParameterization1 != null && !pnlParameterization1.Visible)
            {
                pnlParameterization1.Visible = true;
            }


            this.pnlParameterization1.BringToFront();
            this.pnlParameterization1.SHOWModem(ISmodem);
        }
        private void showLoadProfile()
        {
            //SmartEyeControl_7.ApplicationGUI.ucCustomControl.ScreenWitch SW = new ScreenWitch();
            //Thread.Sleep(2000);
            //this.Panel_Welcome.BringToFront();
            if (this.pnlLoad_Profile != null && !this.pnlLoad_Profile.Visible)
                this.pnlLoad_Profile.Visible = true;

            this.pnlLoad_Profile.BringToFront();
        }
        private void showBilling()
        {
            //this.Panel_Welcome.BringToFront();
            if (this.pnlBilling1 != null && !pnlBilling1.Visible)
                pnlBilling1.Visible = true;
            this.pnlBilling1.BringToFront();
        }
        private void showEvents()
        {
            //this.Panel_Welcome.BringToFront();
            if (this.Panel_Events != null && !this.Panel_Events.Visible)
                this.Panel_Events.Visible = true;
            this.Panel_Events.BringToFront();
        }
        private void showInstantaneous()
        {
            //this.Panel_Welcome.BringToFront();
            if (this.Panel_Instantaneous != null && !this.Panel_Instantaneous.Visible)
                this.Panel_Instantaneous.Visible = true;
            this.Panel_Instantaneous.BringToFront();
        }
        private void showWelComePanel()
        {
            //this.Panel_Welcome.BringToFront();
            if (this.Panel_Welcome != null && !this.Panel_Welcome.Visible)
                this.Panel_Welcome.Visible = true;
            this.Panel_Welcome.BringToFront();
        }
        private void showDebug()
        {
            //this.Panel_Welcome.BringToFront();
            if (this.Panel_Debugging != null && !this.Panel_Debugging.Visible)
                this.Panel_Debugging.Visible = true;
            this.Panel_Debugging.BringToFront();

        }

        #region IO Activity

        public void connectionManager_IOActivity(IOConnection conn, IOActivityType obj)
        {
            try
            {

                String IOActivityStr = String.Format("[Data Flow(Byte),Out:{0:D4},In:{1:D4},Avg Speed:{2:f4} {3}]",
                    conn.ConnectionMonitor.OutFlowTotalDataLength,
                    conn.ConnectionMonitor.InFlowTotalDataLength,
                    conn.ConnectionMonitor.AverageTransmissionSpeed,
                    conn.ConnectionMonitor.SpeedUnit
                    );
                stStatusIOActivity.Visible = true;
                stStatusIOActivity.Text = IOActivityStr;
            }
            catch (Exception ex)
            {

            }
        }

        private void connectionManager_IOChannelDisconnect(IOConnection conn, string obj)
        {
            try
            {
                ///Reset Connect 
                if (conn == AP_Controller.GetCommunicationObject)
                {
                    //ahmed
                    UpdateConnectStatus(false);
                    if (Application_Controller != null)
                        Application_Controller.ConnectToMeter = null;
                }
                if (conn.CurrentConnection != PhysicalConnectionType.OpticalPort)
                {
                    ///RefreshIOConnections(ConnController.ConnectionManager.IOConnectionsList, cmbMeterSerial, 20);
                    if (ConnController.MeterSerial != null &&
                        ConnController.MeterSerial.Count > 0)
                    {
                        ConnController.MeterSerial.Remove(conn);
                        ResetCmbMeterSerial();
                    }
                }
                ///Notification newNofier = new Notification(connectionManager.ConnectionInfo.MSN, "Disconnected");
                ///MessageBox.Show(this, String.Format("Application is disconnected\r\nDetails:{0}", obj), "Disconnected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

            }
        }

        ///// <summary>
        ///// Refresh IOConnections Only Keeps Latest Connection Received or favorite kept by user by maxConnections Limit
        ///// </summary>
        ///// <param name="ConnList"></param>
        ///// <param name="cmbMeterSerial"></param>
        ///// <param name="maxConn"></param>
        //private void RefreshIOConnections(ConnectionsList ConnList, ListBox cmbMeterSerial, int maxConn)
        //{
        //    try
        //    {
        //        ///cmbMeterSerial.Items.Clear(); 
        //        ///Remove All Disconnected Connections
        //        MeterSerial = ConnController.MeterSerial;
        //        IOConnection[] tList = new IOConnection[MeterSerial.Count];
        //        MeterSerial.CopyTo(tList, 0);
        //        foreach (var conn in tList)
        //        {
        //            if (!conn.IsConnected)
        //                MeterSerial.Remove(conn);
        //        }
        //        tList = new IOConnection[MeterSerial.Count];
        //        MeterSerial.CopyTo(tList, 0);
        //        int connListSize = 0;
        //        Interlocked.Exchange(ref connListSize, ConnList.Count);
        //        ///Update Current List
        //        if (ConnList != null && MeterSerial != null && connListSize > 0 && MeterSerial.Count < maxConn)
        //        {
        //            IOConnection[] arr = ConnList.ToArray();
        //            Array.Sort<IOConnection>(arr, (x, y) => (x != null && y != null && x.ConnectionInfo != null && y.ConnectionInfo != null) ? y.ConnectionInfo.LastActivity.CompareTo(x.ConnectionInfo.LastActivity) : -1);
        //            for (int index = 0; index < arr.Length; index++)
        //            {
        //                if (arr[index] != null &&
        //                    arr[index].CurrentConnection != PhysicalConnectionType.OpticalPort &&
        //                    MeterSerial.Count <= maxConn)
        //                {
        //                    IOConnection conT = arr[index];
        //                    ///Check Either Connection already exists before adding
        //                    bool isExists = false;
        //                    foreach (var conn in tList)
        //                    {
        //                        if (conn == conT)
        //                        {
        //                            isExists = true;
        //                            break;
        //                        }
        //                    }
        //                    if (!isExists)
        //                        MeterSerial.Add(conT);
        //                }
        //                else if (MeterSerial.Count <= maxConn)
        //                    continue;
        //                else
        //                    break;
        //            }
        //            ResetCmbMeterSerial();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        #endregion
        private void databaseSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Database_Settings dbSettings = new Database_Settings();
            //dbSettings.Server = Application_Controller.DB_Controller.Server;
            //dbSettings.Database = Application_Controller.DB_Controller.Database;
            //dbSettings.UserID = Application_Controller.DB_Controller.UserID;
            //dbSettings.Password = Application_Controller.DB_Controller.Password;
            //if (dbSettings.ShowDialog() == DialogResult.OK)
            //{
            //    Application_Controller.DB_Controller.Server = dbSettings.Server;
            //    Application_Controller.DB_Controller.Database = dbSettings.Database;
            //    Application_Controller.DB_Controller.UserID = dbSettings.UserID;
            //    Application_Controller.DB_Controller.Password = dbSettings.Password;
            //}
        }

        #region icons

        #region Parameterization
        private void pic_Parameterization_MouseEnter(object sender, EventArgs e)
        {
            if (!paramChecked)
            {
                //pic_Parameterization.Size = new System.Drawing.Size(iconWidthFocus, iconheightFocus);
                //pic_Parameterization.Image = parameterization_active;
            }
        }
        private void pic_Parameterization_MouseLeave(object sender, EventArgs e)
        {
            if (!paramChecked)
            {
                //pic_Parameterization.Size = new System.Drawing.Size(iconWidthNormal, iconheightNormal);
                //pic_Parameterization.Image = parameterization;
            }
        }
        private void pic_Parameterization_Click_2(object sender, EventArgs e)
        {
            unClickAll();
            //pic_Parameterization.Size = new System.Drawing.Size(iconWidthFocus, iconheightFocus);
            //pic_Parameterization.Image = parameterization_active;
            //backgroundGradient = new Bitmap(ImageFolderPath + "bg.png");
            //pic_Parameterization.BackgroundImage = backgroundGradient;
            //flpParameterIcon.BackgroundImage = backgroundGradient;
            paramChecked = true;
        }
        #endregion

        #region Billing
        private void pic_Billing_MouseEnter(object sender, EventArgs e)
        {
            if (!billingChecked)
            {
                //pic_Billing.Size = new System.Drawing.Size(iconWidthFocus, iconheightFocus);
                //pic_Billing.Image = billing_active;
            }
        }
        private void pic_Billing_MouseLeave(object sender, EventArgs e)
        {
            if (!billingChecked)
            {
                //pic_Billing.Size = new System.Drawing.Size(iconWidthNormal, iconheightNormal);
                //pic_Billing.Image = billing;
            }
        }
        private void pic_Billing_Click_1(object sender, EventArgs e)
        {
            unClickAll();
            //pic_Billing.Size = new System.Drawing.Size(iconWidthFocus, iconheightFocus);
            //pic_Billing.Image = billing_active;
            //backgroundGradient = new Bitmap(ImageFolderPath + "bg.png");
            //flpBillingIcon.BackgroundImage = backgroundGradient;
            billingChecked = true;
        }
        #endregion

        #region LoadProfile
        private void pic_LoadProfile_MouseEnter(object sender, EventArgs e)
        {
            if (!loadprofileChecked)
            {
                //pic_LoadProfile.Size = new System.Drawing.Size(iconWidthFocus, iconheightFocus);
                //pic_LoadProfile.Image = loadprofile_active;
            }
        }
        private void pic_LoadProfile_MouseLeave(object sender, EventArgs e)
        {
            if (!loadprofileChecked)
            {
                //pic_LoadProfile.Size = new System.Drawing.Size(iconWidthNormal, iconheightNormal);
                //pic_LoadProfile.Image = loadprofile;
            }
        }
        private void pic_LoadProfile_Click_1(object sender, EventArgs e)
        {
            unClickAll();
            //pic_LoadProfile.Size = new System.Drawing.Size(iconWidthFocus, iconheightFocus);
            //pic_LoadProfile.Image = loadprofile_active;
            //backgroundGradient = new Bitmap(ImageFolderPath + "bg.png");
            //flpLoadProfileIcon.BackgroundImage = backgroundGradient;
            loadprofileChecked = true;
        }
        #endregion

        #region Events
        private void pic_Events_MouseEnter(object sender, EventArgs e)
        {
            if (!eventsChecked)
            {
                //pic_Events.Size = new System.Drawing.Size(iconWidthFocus, iconheightFocus);
                //pic_Events.Image = events_active;
            }
        }
        private void pic_Events_MouseLeave(object sender, EventArgs e)
        {
            if (!eventsChecked)
            {
                //pic_Events.Size = new System.Drawing.Size(iconWidthNormal, iconheightNormal);
                //pic_Events.Image = events;
            }
        }
        private void pic_Events_Click_1(object sender, EventArgs e)
        {
            unClickAll();
            //pic_Events.Size = new System.Drawing.Size(iconWidthFocus, iconheightFocus);
            //pic_Events.Image = events_active;
            //backgroundGradient = new Bitmap(ImageFolderPath + "bg.png");
            //flpEventsIcon.BackgroundImage = backgroundGradient;
            eventsChecked = true;
        }

        #endregion

        #region Instantaneous
        private void pic_Instantaneous_MouseEnter(object sender, EventArgs e)
        {
            if (!instantaneousChecked)
            {
                //pic_Instantaneous.Size = new System.Drawing.Size(iconWidthFocus, iconheightFocus);
                //pic_Instantaneous.Image = instantaneous_active;
                //pic_Instantaneous.BackgroundImage = null;
            }
        }
        private void pic_Instantaneous_MouseLeave(object sender, EventArgs e)
        {
            if (!instantaneousChecked)
            {
                //pic_Instantaneous.Size = new System.Drawing.Size(iconWidthNormal, iconheightNormal);
                //pic_Instantaneous.Image = instantaneous;
                //pic_Instantaneous.BackgroundImage = null;
            }
        }
        private void pic_Instantaneous_Click_1(object sender, EventArgs e)
        {
            unClickAll();
            //pic_Instantaneous.Size = new System.Drawing.Size(iconWidthFocus, iconheightFocus);
            //pic_Instantaneous.Image = instantaneous_active;
            //backgroundGradient = new Bitmap(ImageFolderPath + "bg.png");
            //flpInstantaneousIcon.BackgroundImage = backgroundGradient;
            instantaneousChecked = true;
        }
        #endregion

        #region Settings

        private void pic_settings_MouseEnter(object sender, EventArgs e)
        {
            if (!settingsChecked)
            {
                //pic_settings.Size = new System.Drawing.Size(iconWidthFocus, iconheightFocus);
                //pic_settings.Image = settings_active;
            }
        }
        private void pic_settings_MouseLeave(object sender, EventArgs e)
        {
            if (!settingsChecked)
            {
                //pic_settings.Size = new System.Drawing.Size(iconWidthNormal, iconheightNormal);
                //pic_settings.Image = settings;
            }
        }
        private void pic_settings_Click_1(object sender, EventArgs e)
        {
            unClickAll();
            //pic_settings.Size = new System.Drawing.Size(iconWidthFocus, iconheightFocus);
            //pic_settings.Image = settings_active;
            //backgroundGradient = new Bitmap(ImageFolderPath + "bg.png");
            //flpSettingIcon.BackgroundImage = backgroundGradient;
            settingsChecked = true;
            //Panel_Welcome.BringToFront();
            unClickAll();
        }
        #endregion

        #region Debug
        private void pic_Debug_MouseEnter(object sender, EventArgs e)
        {
            if (!debugChecked)
            {
                //pic_Debug.Size = new System.Drawing.Size(iconWidthFocus, iconheightFocus);
                //pic_Debug.Image = debug_active;
            }
        }
        private void pic_Debug_MouseLeave(object sender, EventArgs e)
        {
            if (!debugChecked)
            {
                //pic_Debug.Size = new System.Drawing.Size(iconWidthNormal, iconheightNormal);
                //pic_Debug.Image = debug;
            }
        }
        private void pic_Debug_Click_1(object sender, EventArgs e)
        {
            unClickAll();
            //pic_Debug.Size = new System.Drawing.Size(iconWidthFocus, iconheightFocus);
            //pic_Debug.Image = debug_active;
            //backgroundGradient = new Bitmap(ImageFolderPath + "bg.png");
            //flpDebugIcon.BackgroundImage = backgroundGradient;
            debugChecked = true;
        }

        #endregion

        #region AboutME

        private void pic_AboutMe_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            if (!helpChecked)
            {
                //pic_AboutMe.Size = new System.Drawing.Size(iconWidthFocus, iconheightFocus);
                //pic_AboutMe.Image = help_active;
            }
        }
        private void pic_AboutMe_MouseLeave(object sender, EventArgs e)
        {
            if (!helpChecked)
            {
                //pic_AboutMe.Size = new System.Drawing.Size(iconWidthNormal, iconheightNormal);
                //pic_AboutMe.Image = help;
            }

        }

        private void pic_AboutMe_Click_1(object sender, EventArgs e)
        {
            unClickAll();
            //pic_AboutMe.Size = new System.Drawing.Size(iconWidthFocus, iconheightFocus);
            //pic_AboutMe.Image = help_active;
            //backgroundGradient = new Bitmap(ImageFolderPath + "bg.png");
            //flpHelpIcon.BackgroundImage = backgroundGradient;
            helpChecked = true;
        }

        #endregion

        #region Modem

        private void pic_Modem_MouseEnter(object sender, EventArgs e)
        {
            if (!modemChecked)
            {
                //pic_Modem.Size = new System.Drawing.Size(iconWidthFocus, iconheightFocus);
                //pic_Modem.Image = modem_active;
            }
        }

        private void pic_Modem_MouseLeave(object sender, EventArgs e)
        {
            if (!modemChecked)
            {
                //pic_Modem.Size = new System.Drawing.Size(iconWidthNormal, iconheightNormal);
                //pic_Modem.Image = modem;
            }
        }

        private void pic_Modem_Click(object sender, EventArgs e)
        {
            unClickAll();
            //pic_Modem.Size = new System.Drawing.Size(iconWidthFocus, iconheightFocus);
            //pic_Modem.Image = modem_active;
            //backgroundGradient = new Bitmap(ImageFolderPath + "bg.png");
            //flpModemParamIcon.BackgroundImage = backgroundGradient;
            modemChecked = true;
            showParameterization(true);
        }

        #endregion

        #endregion

        private void welcome1_Load(object sender, EventArgs e)
        {
            this.pic_Parameterization.Image = parameterization;
            this.pic_LoadProfile.Image = loadprofile;
            this.pic_Billing.Image = billing;
            this.pic_Instantaneous.Image = instantaneous;
            this.pic_Events.Image = events;
            this.pic_Debug.Image = debug;
            this.pic_settings.Image = settings;
            this.pic_AboutMe.Image = help;
            this.pic_logoff.Image = logOff;
            this.pic_admin.Image = admin;

            paramChecked = false; modemChecked = false; loadprofileChecked = false; billingChecked = false; instantaneousChecked = false; eventsChecked = false;
            debugChecked = false; settingsChecked = false; helpChecked = false; adminChecked = false;


        }

        private void unClickAll()
        {
            pic_Instantaneous.Size = new System.Drawing.Size(iconWidthNormal, iconheightNormal);
            pic_Parameterization.Size = new System.Drawing.Size(iconWidthNormal, iconheightNormal);
            pic_LoadProfile.Size = new System.Drawing.Size(iconWidthNormal, iconheightNormal);
            pic_Billing.Size = new System.Drawing.Size(iconWidthNormal, iconheightNormal);
            pic_Instantaneous.Size = new System.Drawing.Size(iconWidthNormal, iconheightNormal);
            pic_Events.Size = new System.Drawing.Size(iconWidthNormal, iconheightNormal);
            pic_Debug.Size = new System.Drawing.Size(iconWidthNormal, iconheightNormal);
            pic_settings.Size = new System.Drawing.Size(iconWidthNormal, iconheightNormal);
            pic_Debug.Size = new System.Drawing.Size(iconWidthNormal, iconheightNormal);
            pic_logoff.Size = new System.Drawing.Size(iconWidthNormal, iconheightNormal);
            pic_admin.Size = new System.Drawing.Size(iconWidthNormal, iconheightNormal);
            pic_AboutMe.Size = new System.Drawing.Size(iconWidthNormal, iconheightNormal);

            this.pic_Parameterization.Image = parameterization;
            this.flpParameterIcon.BackgroundImage = null;

            this.pic_LoadProfile.Image = loadprofile;
            this.flpLoadProfileIcon.BackgroundImage = null;

            this.pic_Billing.Image = billing;
            this.flpBillingIcon.BackgroundImage = null;

            this.pic_Instantaneous.Image = instantaneous;
            this.flpInstantaneousIcon.BackgroundImage = null;

            this.pic_Events.Image = events;
            this.flpEventsIcon.BackgroundImage = null;

            this.pic_Debug.Image = debug;
            this.flpDebugIcon.BackgroundImage = null;

            this.pic_settings.Image = settings;
            this.flpSettingIcon.BackgroundImage = null;

            this.pic_AboutMe.Image = help;
            this.flpHelpIcon.BackgroundImage = null;

            this.pic_logoff.Image = logOff;
            this.flpLogOffIcon.BackgroundImage = null;

            this.pic_admin.Image = admin;
            this.flpAdminIcon.BackgroundImage = null;

            paramChecked = false; modemChecked = false; loadprofileChecked = false; billingChecked = false; instantaneousChecked = false;
            eventsChecked = false; debugChecked = false; settingsChecked = false; helpChecked = false; adminChecked = false;
            //Panel_Welcome.BringToFront();
        }

        private void RefreshApplicationConfigurations()
        {
            try
            {
                if (pnlParameterization1 != null)
                {
                    this.pnlParameterization1.RefreshParameterizationConfig();
                    Application_Controller.CurrentMeterName = cmbManufacturers.Text;
                }
            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Error Loading", "Error loading application configurations");
            }

            try
            {
                if (Panel_Events != null)
                    this.Panel_Events.LoadAlarm(null, cmbDevices.Text); //v3.3.7 QC
            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Error Loading", "Error loading application configurations");
            }

        }

        private void Get_DLMSLogger(ArrayList Loggers)
        {
            try
            {
                if (Loggers != null)
                {
                    Loggers.Clear();
                    Loggers.Add(AP_Controller.ApplicationProcess.Logger);
                }
            }
            catch (Exception ex) { }
        }

        private void Application_Controller_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                ///Okay IsIOBusy Status
                if ("IsIOBusy".Equals(e.PropertyName) && sender is ApplicationController)
                {
                    UpdateReadWriteStatus(Application_Controller.IsIOBusy || Application_Controller.ConnectionManager.IsConnected);
                }
                else if ("ConnectToMeter".Equals(e.PropertyName) && sender is ApplicationController)
                {
                    IOConnection ConInfo = Application_Controller.ConnectToMeter;
                    if (ConInfo != null && ConInfo != AP_Controller.GetCommunicationObject)
                        Connect_Application(ConInfo);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void UpdateReadWriteStatus(bool IsReadWriteBusy)
        {
            #region Method_Body
            try
            {
                ///Disable IO Read Write btns 
                if (IsReadWriteBusy)
                {
                    cmbIRPorts.Enabled = false;
                    cmbAssociations.Enabled = false;
                    cmbDevices.Enabled = false;
                    cmbIOConnections.Enabled = false;
                    txtAssociationPaswd.Enabled = false;
                    //  btnConnectApplication.Enabled = false;
                    ///lnk_Disconnect_forcedly.Enabled = false;
                    gpPhysicalConnection.Enabled = false;

                    cmbManufacturers.Enabled = false;
                    tbAuthenticationKey.Enabled = false;
                    tbEncryptionKey.Enabled = false;
                    tbInvocationCounter.Enabled = false;
                    cmbSecurity.Enabled = false;
                    btn_DefaultPwd.Enabled = false;
                }
                ///Enable Read Write btns
                else
                {
                    cmbIRPorts.Enabled = true;
                    cmbAssociations.Enabled = true;
                    cmbDevices.Enabled = true;
                    cmbIOConnections.Enabled = true; //ahmed
                    txtAssociationPaswd.Enabled = true;
                    btnConnectApplication.Enabled = true;
                    //lnk_Disconnect_forcedly.Enabled = false;
                    gpPhysicalConnection.Enabled = true;

                    cmbManufacturers.Enabled = true;
                    tbAuthenticationKey.Enabled = true;
                    tbEncryptionKey.Enabled = true;
                    tbInvocationCounter.Enabled = true;
                    cmbSecurity.Enabled = true;
                    btn_DefaultPwd.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                cmbIRPorts.Enabled = true;
                cmbAssociations.Enabled = true;
                cmbDevices.Enabled = true;
                cmbIOConnections.Enabled = true;
                txtAssociationPaswd.Enabled = true;
                btnConnectApplication.Enabled = true;
                ///lnk_Disconnect_forcedly.Enabled = false;
                gpPhysicalConnection.Enabled = true;

                cmbManufacturers.Enabled = true;
                tbAuthenticationKey.Enabled = true;
                tbEncryptionKey.Enabled = true;
                tbInvocationCounter.Enabled = true;
                cmbSecurity.Enabled = true;
                btn_DefaultPwd.Enabled = true;
            }
            #endregion
        }

        private void cmbUserSoftId_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #region ComboBox_Meter_Serial

        private void cmbMeterSerial_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ResetCmbMeterSerial();
            }
            catch (Exception ex)
            {

            }
        }

        private void cmbMeterSerial_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                ResetCmbMeterSerial();
            }
            catch (Exception ex)
            {

            }
        }

        private void ResetCmbMeterSerial()
        {
            try
            {
                cmbMeterSerial.SelectedIndexChanged -= new EventHandler(cmbMeterSerial_SelectedIndexChanged);
                if (cmbMeterSerial.IsHandleCreated && cmbMeterSerial.InvokeRequired)
                {
                    ///Delegate dlg = new Action<BindingList<IOConnection>>((x) => cmbMeterSerial.DataSource = x);
                    Delegate dlg_1 = new Action<BindingList<IOConnection>>((x) => cmbMeterSerial.DataSource = x);
                    Delegate dlg = new Action(() => MeterSerial.ResetBindings());
                    cmbMeterSerial.Invoke(dlg_1, new object[1]);
                    cmbMeterSerial.Invoke(dlg_1, MeterSerial);
                    ///cmbMeterSerial.Invoke(dlg);
                }
                else
                {
                    cmbMeterSerial.DataSource = null;
                    cmbMeterSerial.DataSource = MeterSerial;
                    ///MeterSerial.ResetBindings();
                }
            }
            catch (Exception ex)
            { }
            finally
            {
                cmbMeterSerial.SelectedIndexChanged += new EventHandler(cmbMeterSerial_SelectedIndexChanged);
            }
        }

        #endregion

        #region IP Connections Context Menu

        public void IpConContextMenu_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                IOConnection selectedConn = (IOConnection)cmbMeterSerial.SelectedItem;
                GetMeterInfoMenuItem.Enabled = true;
                DropConnMenuItem.Enabled = true;
                readMeterInfoMenuItem.Enabled = true;
                removeListMenuItem.Enabled = true;
                ///Disable Menu Items
                if (selectedConn == null)
                {
                    GetMeterInfoMenuItem.Enabled = false;
                    DropConnMenuItem.Enabled = false;
                    readMeterInfoMenuItem.Enabled = false;
                    removeListMenuItem.Enabled = false;
                }
                if (AP_Controller.IsConnected && AP_Controller.GetCommunicationObject == selectedConn)
                {
                    DropConnMenuItem.Enabled = false;
                    readMeterInfoMenuItem.Enabled = false;
                    removeListMenuItem.Enabled = false;
                }
                else if (AP_Controller.IsConnected)
                {
                    readMeterInfoMenuItem.Enabled = false;
                }

            }
            catch (Exception ex)
            { }
        }

        public void DropConnMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                IOConnection selectedConn = (IOConnection)cmbMeterSerial.SelectedItem;
                if (selectedConn == null)
                    return;
                string dialogMsg = String.Format("Are you sure want to disconnect/drop meter?\r\n{0}", selectedConn);
                DialogResult res = MessageBox.Show(this, dialogMsg, "Disconnect Connection", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (res == DialogResult.OK)
                {
                    selectedConn.Disconnect();
                    MeterSerial.Remove(selectedConn);
                    ResetCmbMeterSerial();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LoadComPorts()
        {
            if (cmbIRPorts.Enabled)
            {

                string[] serialPorts = SerialPort.GetPortNames();
                //cmbSerialPorts.Items.Clear();
                //foreach (String port in serialPorts)
                //{
                //    cmbSerialPorts.Items.Add(port);
                //}
                if (serialPorts != null)
                    Array.Sort<String>(serialPorts);
                cmbIRPorts.Items.Clear();
                foreach (var port in serialPorts)
                {
                    cmbIRPorts.Items.Add(port);
                }
                if (cmbIRPorts.Items.Count > 0) cmbIRPorts.SelectedIndex = 0;
                // cmbIRPorts.Items.Clear();
                // cmbIRPorts.Items.AddRange(SerialPort.GetPortNames());
            }
        }
        private void lblRefreshPorts_Click(object sender, EventArgs e)
        {
            LoadComPorts();
        }

        private void llblReloadRights_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Device CurrentDevice = null;
            try
            {
                CurrentDevice = ((Device)this.cmbDevices.SelectedItem);
                if (ConnController != null && CurrentDevice != null)
                {
                    ConnController.SelectedMeter.Device = CurrentDevice;
                    UpdateUserRights(CurrentDevice);
                }
            }
            catch
            {

            }
        }

        private void lnkbtnRefreshServer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        public void GetMeterInfoMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                IOConnection selectedConn = (IOConnection)cmbMeterSerial.SelectedItem;
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
                unClickAll();
                showWelComePanel();
                ///Abort Previous Executing Action
                int count = 0;
                while (BckWorkerThread != null && BckWorkerThread.IsBusy)
                {
                    BckWorkerThread.CancelAsync();
                    Thread.Sleep(1);
                    count++;
                }
                Disable_IOActivity();
                BckWorkerThread = new BackgroundWorker();
                BckWorkerThread.WorkerSupportsCancellation = true;
                BckWorkerThread.DoWork += new DoWorkEventHandler(BckWorker_ReadMeterInfo_DoEventHandler);
                BckWorkerThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BckWorker_ReadMeterInfo_WorkCompleted);
                BckWorkerThread.RunWorkerAsync();
                ///SetReadWriteStatus(true);
                Application_Controller.IsIOBusy = true;
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
                    res = MessageBox.Show(this, "Are you sure want to cancel current porcess immediately,Press Ok? Otherwise wait till process completes,press Cancel."
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

        public void removeListMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                IOConnection Conn = (IOConnection)cmbMeterSerial.SelectedItem;
                string dialogMsg = String.Format("Are you sure want to remove meter from favourite Meter list?\r\n{0}", Conn);
                DialogResult res = MessageBox.Show(this, dialogMsg, "Remove Connection", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (res == DialogResult.OK)
                {
                    MeterSerial.Remove(Conn);
                    MeterSerial.ResetBindings();
                }
            }
            catch (Exception ex)
            { }
        }

        #endregion

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
                        //AP_Controller.ARLRQ(new flagStatus(ARLQCalBck));
                        //Thread.Sleep(100);

                        AP_Controller.ARLRQAsync().Wait(500);
                        AP_Controller.ApplicationProcess.Is_Association_Developed = false;
                        AP_Controller.GetCommunicationObject.Disconnect();
                    }
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
                IOConnection IOConn = (IOConnection)cmbMeterSerial.SelectedItem;
                if (!AP_Controller.IsConnected ||
                    (AP_Controller.IsConnected && AP_Controller.GetCommunicationObject == IOConn))
                {
                    // String meterSAPName = cmbMeterSAP.SelectedItem.ToString();
                    // String clientSAPName = cbxClientSap.SelectedItem.ToString();

                    String txtPasswd = txtAssociationPaswd.Text;

                    if (IOConn == null || !IOConn.IsChannelConnected)
                    {
                        throw new Exception("Unable to read meter information,IP Link is disconnected");
                    }
                    #region // Init Work Before Connection Works

                    if (IOConn.ConnectionInfo == null)
                    {
                        IOConn.ConnectionInfo = new ConnectionInfo();

                    }

                    MeterConfig conf = new MeterConfig(ConnController.SelectedMeter);
                    conf.Version = null;
                    if (IOConn.ConnectionInfo.MeterInfo == null ||
                        IOConn.ConnectionInfo.MeterInfo.Device_Association == null)
                        IOConn.ConnectionInfo.MeterInfo = conf;

                    // SAPConfig CurrentMeterSAP = conf.ServerSAP.Find((x) => x.FaceName.Equals(meterSAPName, StringComparison.OrdinalIgnoreCase));
                    // SAP_Object CurrentClientSAP = conf.ClientSAP.Find((x) => x.SAP_Name.Equals(clientSAPName, StringComparison.OrdinalIgnoreCase));
                    // if (CurrentMeterSAP != null)
                    // {
                    //     IOConn.ConnectionInfo.CurrentSAP = CurrentMeterSAP;
                    //     CurrentMeterSAP.Password = txtPasswd;
                    // }
                    // if (CurrentClientSAP != null)
                    // {
                    //     IOConn.ConnectionInfo.CurrentClientSAP = CurrentClientSAP;
                    // }

                    #endregion
                    /// SetReadWriteStatus(true);
                    Application_Controller.IsIOBusy = true;
                    ConnController.GetMeterInfo(IOConn);
                }

                else
                {
                    throw new Exception("Application is already connected with another meter device,please try again lator");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                this.ex = ex;
            }
            finally
            {
                Thread.CurrentThread.Abort();

            }
        }

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
                UpdateConnectStatus(ConnController.IsConnected);
                progressDialog.EnableProgressBar = false;
                progressDialog.okButton.Visible = true;
                progressDialog.btnCancel.Visible = true;
                ///SetReadWriteStatus(false);
                Application_Controller.IsIOBusy = false;
            }
        }

        #endregion

        private void lnkHeartBeatList_Click(object sender, EventArgs e)
        {
            try
            {
                if (frm_HB != null && frm_HB.Enabled)
                {
                    frm_HB.Close();
                    frm_HB.Dispose();
                    frm_HB = null;
                }
                frm_HB = new uCHeartBeat(Application_Controller, ConnController);
                frm_HB.Show();
            }
            catch (Exception)
            {
                MessageBox.Show(this, "Error Display Connections List", "Error Display", MessageBoxButtons.OK);
            }
        }

        private void Connect_Application(IOConnection ConnInfo)
        {
            try
            {
                if (!AP_Controller.IsConnected)
                {

                    //modify this line to handle more than 20 meters
                    if (MeterSerial.Contains(ConnInfo))
                    {
                        //donothing 
                        cmbMeterSerial.SelectedItem = ConnInfo;
                    }
                    else
                    {
                        MeterSerial.Add(ConnInfo);
                        cmbMeterSerial.SelectedItem = ConnInfo;
                    }
                    //cmbMeterSerial.SelectedItem = Application_Controller.ConnectToMeter;

                    btnConnectApplication_Click(this, new EventArgs());
                }
                else
                    throw new Exception("Application is already connected with another application");

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error Connecting");
            }
        }

        private void timer_Connect_Tick(object sender, EventArgs e)
        {
            if ((IOConnectionType)cmbIOConnections.SelectedItem == IOConnectionType.IP_Link)
            {
                timer_Connect.Stop();
                if (Application_Controller.ConnectToMeter != null && !AP_Controller.IsConnected)
                {
                    //modify this line to handle more than 20 meters
                    if (MeterSerial.Contains(Application_Controller.ConnectToMeter))
                    {
                        ;//meter is already in the list
                    }
                    else
                    {
                        MeterSerial.Add(Application_Controller.ConnectToMeter);
                    }
                    cmbMeterSerial.SelectedItem = Application_Controller.ConnectToMeter;
                    btnConnectApplication_Click(this, new EventArgs());
                    Application_Controller.ConnectToMeter = null;
                }
                else if (!AP_Controller.IsConnected)
                {
                    UpdateReadWriteStatus(false);
                    btnConnectApplication.Text = "Connect";
                    //MeterSerial.Remove((IOConnection)cmbMeterSerial.SelectedItem);
                }
                timer_Connect.Start();
            }
        }

        private void btn_DefaultPwd_Click(object sender, EventArgs e)
        {
            txtAssociationPaswd.Text = "microtek";
        }

        #region Communication_Panel

        private void pnlCommunication_MouseLeave_1(object sender, EventArgs e)
        {
            if (check_auto_hide.Checked && Cursor.Position.X < this.Width - 195 && pnlCommunication.Width != 20)
            {
                pnlCommunication.Size = new Size(20, size_pnl_comm.Height);
            }
        }

        private void pnlCommunication_MouseEnter(object sender, EventArgs e)
        {
            if (check_auto_hide.Checked && pnlCommunication.Width == 20 && Cursor.Position.X >= this.Width - 35)
            {
                pnlCommunication.Size = new Size(200, size_pnl_comm.Height);
            }
        }

        #endregion

        private void AdminPanel_Click(object sender, EventArgs e)
        {
            try
            {
                unClickAll();

                pnlsuperAdminPanel1.Current_User = Application_Controller.CurrentUser;
                pnlsuperAdminPanel1.Visible = true;
                pnlsuperAdminPanel1.BringToFront();

                pnlsuperAdminPanel1.Select_User(Application_Controller.CurrentUser);
                pnlsuperAdminPanel1.showToGUI_user(Application_Controller.CurrentUser);

                //pic_admin.Size = new System.Drawing.Size(iconWidthFocus, iconheightFocus);
                //pic_admin.Image = admin_active;
                //backgroundGradient = new Bitmap(ImageFolderPath + "bg.png");
                //flpAdminIcon.BackgroundImage = backgroundGradient;
                adminChecked = true;
            }
            catch { }
        }

        private void UserLogin_Click(object sender, EventArgs e)
        {
            bool flag = false;
            pnlCommunication.Visible = false;
            unClickAll();
            //v4.8.23
            //v4.8.29 commented
            //flpAdminIcon.Visible = flpBillingIcon.Visible = flpDebugIcon.Visible = flpEventsIcon.Visible = flpInstantaneousIcon.Visible = flpLoadProfileIcon.Visible =
            //    flpModemParamIcon.Visible = flpParameterIcon.Visible = flpSettingIcon.Visible = false;

            //v4.8.29
            Panel_Debugging.Visible = Panel_Events.Visible = Panel_Instantaneous.Visible = pnlsuperAdminPanel1.Visible =
                pnlParameterization1.Visible = pnlPicIcons.Visible = pnlLoad_Profile.Visible = pnlBilling1.Visible =
                false;

            //backgroundGradient = null;

            this.Hide();
            //To Disconnect Already Connected Meter
            if (Application_Controller.Applicationprocess_Controller.IsConnected)
            {
                this.btnConnectApplication_Click(this, new EventArgs());
            }
            Login login = new Login();
            retry:
            if (flag)
            {
                login = new Login(true);
            }
            Application_Controller.CurrentUser = dbController.verifyUserAndGetDetails(login.txt_login.Text, login.txt_password.Text);
            if (Application_Controller.CurrentUser == null)
            {
                flag = true;
                goto retry;
            }
            new ApplicationRight_Helper().updateGUI_All(this, pnlParameterization1, pnlLoad_Profile, Panel_Events,
                                                        Panel_Instantaneous, pnlBilling1, Application_Controller.CurrentUser);
            //Application_Controller.CurrentUser.updateGUI_All(this, pnlParameterization1, pnlLoad_Profile, Panel_Events, Panel_Instantaneous, pnlBilling1);
            Process_AfterLogin();
        }

        private void Process_AfterLogin()
        {
            if (Application_Controller.CurrentUser != null)
            {
                this.Show();
                pnlPicIcons.Visible = true;
                pnlCommunication.Visible = true;
                Application_Controller.Applicationprocess_Controller.UserId = Convert.ToUInt16(Application_Controller.CurrentUser.userID);
                //Init_MeterModels(true);
                pnlsuperAdminPanel1.showToGUI_user(Application_Controller.CurrentUser); //v4.8.29

                if (cmbManufacturers.Items.Count > 0)
                {
                    try
                    {
                        cmbManufacturers.SelectedIndex = -1;
                    }
                    catch (Exception) { }
                    cmbManufacturers.SelectedIndex = 0;
                }
            }
        }

        private void AttachAccessRights(User usr)
        {
            dbController.LoadApplicationAccessRights(usr);
        }

        private void pic_logoff_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;

            //pic_logoff.Size = new System.Drawing.Size(iconWidthFocus, iconheightFocus);
            //pic_logoff.Image = logOff_active;
            //flpLogOffIcon.BackgroundImageLayout = ImageLayout.Zoom;
        }

        private void pic_logoff_MouseLeave(object sender, EventArgs e)
        {
            //pic_logoff.Size = new System.Drawing.Size(iconWidthNormal, iconheightNormal);
            //pic_logoff.Image = logOff;
            //pic_logoff.BackgroundImageLayout = ImageLayout.Zoom;
        }

        //private void btnDecrypt_Click(object sender, EventArgs e)
        //{
        //    #region Password Key Implementation
        //    txtAssociationPaswd.Text = "";
        //    string PassKey = tbPasskey.Text.Trim().Replace("-", "");
        //    if (PassKey.Length > 8)
        //    {
        //        ObjPassKey = new PasswordKey();
        //        ObjPassKey = pasKyMngr.Decrypt(PassKey, (uint)Application_Controller.Applicationprocess_Controller.SoftWareId);
        //        txtAssociationPaswd.Text = ObjPassKey.Password;
        //    }
        //    else if (PassKey.Length > 0 && PassKey.Length <= 8)
        //    {
        //        throw new Exception("Invalid PassKey");
        //    }
        //    #endregion
        //}

        //private void btnClearPassKey_Click(object sender, EventArgs e)
        //{
        //    tbPasskey.Text = "";
        //}

        //private void rdbPass_Key_CheckedChanged(object sender, EventArgs e)
        //{
        //    txtAssociationPaswd.Text = tbPasskey.Text = "";

        //    if (rdbPassword.Checked)
        //    {
        //        txtAssociationPaswd.Enabled = true;
        //        tbPasskey.Visible = false;
        //    }
        //    else
        //    {
        //        if (dbValidator == null) dbValidator = new DatabaseValidator();
        //        if (pasKyMngr == null) pasKyMngr = new PassKeyManager();
        //        //PasswordKey ObjPassKey = null;
        //        txtAssociationPaswd.Enabled = false;
        //        tbPasskey.Visible = true;
        //    }
        //}

        //private void rdbPassword_EnabledChanged(object sender, EventArgs e)
        //{
        //    if (rdbPassword.Checked)
        //    {
        //        txtAssociationPaswd.Enabled = rdbPassword.Enabled;
        //        //tbPasskey.Enabled = !(rdbPassword.Enabled);
        //    }
        //    else
        //    {
        //        //txtAssociationPaswd.Enabled = false;
        //        tbPasskey.Enabled = rdbPass_Key.Enabled;
        //    }

        //    btn_DefaultPwd.Enabled = btnClearPassKey.Enabled = rdbPassword.Enabled;
        //}

        private void timer_PassKey_Tick(object sender, EventArgs e)
        {
            thread_PassKey = new Thread(Update_PassKey_TimeConsumption);
            thread_PassKey.Start();
        }
        private void Update_PassKey_TimeConsumption()
        {
            try
            {
                if (!dbValidator.Validate_Database())
                {
                    Notification notifier = new Notification("Error validation kyDb", ".^.");
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error validation kyDb", ex.Message);
            }

            try
            {
                if (pasKyMngr.UpdateStatus_TimeConsumption(ObjPassKey, false))
                {
                    ObjPassKey = pasKyMngr.SelectPasswordKey(ObjPassKey.PassKey);
                }
                else
                {
                    throw new Exception("Fail to Update pk");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to Update pk: " + ex.Message);
            }
            finally
            {
                thread_PassKey.Abort();
            }
        }

        private void pic_admin_MouseEnter(object sender, EventArgs e)
        {
            if (!adminChecked)
            {
                this.Cursor = Cursors.Hand;

                //pic_admin.Size = new System.Drawing.Size(iconWidthFocus, iconheightFocus);
                //pic_admin.Image = admin_active;
            }
        }

        private void pic_admin_MouseLeave(object sender, EventArgs e)
        {
            if (!adminChecked)
            {
                //pic_admin.Size = new System.Drawing.Size(iconWidthNormal, iconheightNormal);
                //pic_admin.Image = admin;
            }
        }

        #region Security Data

        //private void InitSecurityData()
        //{

        //    try
        //    {
        //        Security_Data _Security_Data = new Security_Data();

        //        _Security_Data.SecurityControl = (SecurityControl)0x30; /// (SecurityControl)(cmbSecurity.SelectedIndex * 0x10);
        //        _Security_Data.SystemTitle = new List<byte>(DLMS_Common.PrintableStringToByteArray
        //                                                   (OptocomSoftware.Properties.Settings.Default.SystemTitle));
        //        _Security_Data.ServerSystemTitle = _Security_Data.SystemTitle;


        //        string key = "C1CA4472EFE30A2668CC10A64DCCCED7";      /// txtAK.Text.PadLeft(32, '0');

        //        Key Authen_KEY = new Key(DLMS_Common.String_to_Hex_array(key), 1, 1,
        //                                 KEY_ID.AuthenticationKey);
        //        key = "F2CE6D0BC7E53DA0B23FCCEE9736D617";            /// txtEK.Text.PadLeft(32, '0');
        //        uint counter = 0;                                    /// Convert.ToUInt32(txtIC.Text);
        //        Key Encrypt_KEY = new Key(DLMS_Common.String_to_Hex_array(key), counter, 0,
        //                                             KEY_ID.GLOBAL_Unicast_EncryptionKey);

        //        Key KEK_KEY = new Key(new byte[]{0xAA, 0x55, 0x33, 0xCC,0xAA, 0x55, 0x33, 0xCC, 
        //                              0xAA, 0x55, 0x33, 0xCC, 0xAA, 0x55, 0x33, 0xCC}, 02, 02,
        //                              KEY_ID.MasterKey);

        //        _Security_Data.AuthenticationKey = Authen_KEY;
        //        _Security_Data.EncryptionKey = Encrypt_KEY;
        //        _Security_Data.MasterEncryptionKey = KEK_KEY;

        //        AP_Controller.Security_Data = _Security_Data;
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("Error While Initializing Security Data");
        //    }
        //}

        #endregion

        private void cmbIRPorts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        ////Flickering Reduction
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
        //        return cp;
        //    }
        //} 

        private void cmbManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Manufacturer CurrentManufacturer = null;

            try
            {
                CurrentManufacturer = ((Manufacturer)this.cmbManufacturers.SelectedItem);

                if (ConnController != null)
                {
                    ConnController.SelectedMeter.Manufacturer = CurrentManufacturer;
                }

                LoadDevice(CurrentManufacturer);

                if (cmbDevices.Items.Count > 0)
                {
                    // if (cmbManufacturers.Enabled)
                    {
                        cmbDevices.Enabled = true;
                        cmbDevices.SelectedIndex = 0;
                    }
                }
                else
                {
                    cmbDevices.Enabled = false;
                    cmbAssociations.Items.Clear();
                    cmbAssociations.Enabled = false;
                }


            }
            catch
            {

            }
        }

        private void cmbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {

            Device CurrentDevice = null;

            try
            {
                CurrentDevice = ((Device)this.cmbDevices.SelectedItem);
                // UpdateUserRights(CurrentDevice.Model);
                if (ConnController != null && CurrentDevice != null)
                {
                    ConnController.SelectedMeter.Device = CurrentDevice;
                    UpdateUserRights(CurrentDevice);
                }

                LoadDeviceAssociation(CurrentDevice);

                if (cmbAssociations.Items.Count > 0)
                {
                    // if (cmbAssociations.Enabled)
                    {
                        cmbAssociations.Enabled = true;
                        cmbAssociations.SelectedIndex = 0;
                    }
                }
                else
                {
                    cmbAssociations.Enabled = false;
                }

                DeviceAssociation devAssoc = (DeviceAssociation)(cmbAssociations.SelectedItem);
                ConnectionInfo conInfo = new ConnectionInfo()
                {
                    IsConnected = true,
                    
                    MeterInfo = new MeterConfig()
                    {
                        Device = CurrentDevice,
                        Device_Association = devAssoc,
                        Device_Configuration = ConfigLoader.LoadConfigurationById(devAssoc.ConfigurationId)
                    }
                };

                pnlParameterization1.RefreshParameterizationConfig(conInfo);
            }
            catch
            {

            }
        }

        private Security_Data InitSecurityData()
        {
            try
            {
                Security_Data _Security_Data = new Security_Data();

                _Security_Data.SecurityControl = (SecurityControl)(cmbSecurity.SelectedIndex * 0x10);
                _Security_Data.SystemTitle = new List<byte>(DLMS_Common.PrintableStringToByteArray(Settings.Default.SystemTitle));
                _Security_Data.ServerSystemTitle = _Security_Data.SystemTitle;

                string key = tbAuthenticationKey.Text.PadLeft(32, '0');

                Key Authen_KEY = new Key(DLMS_Common.String_to_Hex_array(key), 1, 1,
                                                    KEY_ID.AuthenticationKey);
                key = tbEncryptionKey.Text.PadLeft(32, '0');
                uint counter = Convert.ToUInt32(tbInvocationCounter.Text);
                Key Encrypt_KEY = new Key(DLMS_Common.String_to_Hex_array(key), counter, 0,
                                                     KEY_ID.GLOBAL_Unicast_EncryptionKey);

                Key KEK_KEY = new Key(new byte[]{0xAA, 0x55, 0x33, 0xCC,0xAA, 0x55, 0x33, 0xCC,
                                                 0xAA, 0x55, 0x33, 0xCC,0xAA, 0x55, 0x33, 0xCC}, 02, 02,
                                                 KEY_ID.MasterKey);

                _Security_Data.AuthenticationKey = Authen_KEY;
                _Security_Data.EncryptionKey = Encrypt_KEY;
                _Security_Data.MasterEncryptionKey = KEK_KEY;

                return _Security_Data;
            }
            catch (Exception)
            {
                throw new Exception("Error While Initializing Security Data.");
            }
        }

        private void cmbAssociations_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeviceAssociation CurrentDeviceAssociation = null;
            try
            {
                CurrentDeviceAssociation = (DeviceAssociation)cmbAssociations.SelectedItem;
                if (CurrentDeviceAssociation.AuthenticationType >= HLS_Mechanism.HLS_Manufac)
                {
                    this.pnlAuthenticationKeys.Visible = true;
                    this.cmbSecurity.SelectedIndex = 3;
                }
                else
                {
                    this.pnlAuthenticationKeys.Visible = false;
                    this.cmbSecurity.SelectedIndex = 0;
                }

                if (ConnController != null)
                {
                    ConnController.SelectedMeter.Device_Association = CurrentDeviceAssociation;
                    // Update Device Configuration
                    ConnController.SelectedMeter.Device_Configuration = null;
                    ConnController.SelectedMeter.Device_Configuration = ConfigLoader.LoadConfigurationById(CurrentDeviceAssociation.ConfigurationId);

                    //ConnController.IsConfigurationUpated = true;
                }
            }
            catch
            {

            }


        }
        #region User FUcntions

        private void UpdateUserRights(Device meter)
        {
            try
            {
                //String t = cmbManufacturers.Text;
                //LoadMeterSAPConfig(t);

                Application_Controller.CurrentMeterName = meter.Model;
                pnlParameterization1.Application_Controller = Application_Controller;
                pnlParameterization1.myTempFunction(cmbManufacturers.Text);
                if (Application_Controller.CurrentUser != null)
                {
                    int modelId = dbController.SelectMeterInfoId(Application_Controller.CurrentMeterName);
                    Application_Controller.CurrentUser.SetCurrentAccessRights(Application_Controller.CurrentMeterName, modelId);
                    //Application_Controller.CurrentUser.SetCurrentAccessRights(Application_Controller);
                }
                //test Case Goes here
                if (/*either_Changed &&*/
                   Application_Controller.CurrentUser != null &&
                   Application_Controller.CurrentUser.CurrentAccessRights != null &&
                   Application_Controller.CurrentUser.IsCurrentAccessRightsChanged)
                {
                    //Reset Parameters,Clear All data From Panels
                    Reset_SoftwareState();
                    //pnlParameterization1.ApplyAccessRights(Application_Controller.CurrentUser.CurrentAccessRights, Application_Controller.CurrentUser.userTypeID);
                    try
                    {
                        using (ApplicationRight_Helper ar = new ApplicationRight_Helper())
                        {
                            //ar.updateGUI(this, Application_Controller.CurrentUser);
                            ar.updateGUI_All(this, pnlParameterization1, pnlLoad_Profile, Panel_Events, Panel_Instantaneous, pnlBilling1, Application_Controller.CurrentUser);
                        }
                    }
                    catch (Exception) { }

                    //v4.8.23 by Azeem

                    //if (flpBillingIcon.Visible)
                    //    .ApplyAccessRights(Application_Controller.CurrentUser.CurrentAccessRights, Application_Controller.CurrentUser.userTypeID);
                    try
                    {
                        if (flpParameterIcon.Visible)
                            pnlParameterization1.ApplyAccessRights(Application_Controller.CurrentUser.CurrentAccessRights, Application_Controller.CurrentUser.userTypeID, meter.IsSinglePhase);
                    }
                    catch (Exception) { }

                    try
                    {
                        if (flpEventsIcon.Visible)
                            Panel_Events.ApplyAccessRights(Application_Controller.CurrentUser.CurrentAccessRights);
                    }
                    catch (Exception) { }

                    try
                    {
                        if (flpInstantaneousIcon.Visible)
                            Panel_Instantaneous.ApplyAccessRights(Application_Controller.CurrentUser.CurrentAccessRights);
                    }
                    catch (Exception) { }

                    try
                    {
                        if (flpBillingIcon.Visible)
                            pnlBilling1.ApplyAccessRights(Application_Controller.CurrentUser.CurrentAccessRights);
                    }
                    catch (Exception) { }

                    try
                    {
                        if (flpLoadProfileIcon.Visible)
                            pnlLoad_Profile.ApplyAccessRights(Application_Controller.CurrentUser.CurrentAccessRights);
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception ex)
            { }
        }

        #endregion

        private void cmbSecurity_SelectedIndexChanged(object sender, EventArgs e)
        {
            SecurityControl sec_val = (SecurityControl)(cmbSecurity.SelectedIndex * 0x10);

            if (ConnController != null &&
               (ConnController.SecurityData == null ||
               ConnController.SecurityData.SecurityControl != sec_val))
            {
                ConnController.SecurityData = InitSecurityData();
            }
        }

        private Security_Data InitSecurityData(string msn)
        {
            try
            {
                HlsDbController dbControl_hls = new HlsDbController();
                Security_Data _Security_Data = null;

                /// Load Security Data Configuration
                _Security_Data = dbControl_hls.GetMeterHLSSetting(Convert.ToUInt32(msn));

                _Security_Data.SystemTitle = new List<byte>(DLMS_Common.PrintableStringToByteArray(OptocomSoftware.Properties.Settings.Default.SystemTitle));
                _Security_Data.ServerSystemTitle = _Security_Data.SystemTitle;

                //Random rand_Nm = new Random();
                //uint EK_Counter = 0;
                //EK_Counter = (uint)rand_Nm.Next((int)OptocomSoftware.Properties.Settings.Default.MIN_EK_Counter,
                //                                (int)OptocomSoftware.Properties.Settings.Default.MAX_EK_Counter);

                _Security_Data.EncryptionKey.EncryptionCounter = 0; // EK_Counter;
                _Security_Data.EncryptionKey.DecryptionCounter = 0; //EK_Counter;

                return _Security_Data;
            }
            catch (Exception)
            {
                throw new Exception("Error While Initializing Security Data.");
            }
        }
    }

    // User FUcntions
}