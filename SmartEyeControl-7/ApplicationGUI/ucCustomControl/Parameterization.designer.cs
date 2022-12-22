using SharedCode.Comm.Param;
using System.Windows.Forms;
namespace ucCustomControl
{
    partial class pnlParameterization
    {
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pnlParameterization));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblConnectionStatus = new System.Windows.Forms.Label();
            this.lblCapConnectionStatus = new System.Windows.Forms.Label();
            this.btn_caliberation_loadall = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_Caliberation_Save = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_GET_paramameters = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_SET_paramameters = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.timer_ClockFWD = new System.Windows.Forms.Timer(this.components);
            this.Parameterization_BckWorkerThread = new System.ComponentModel.BackgroundWorker();
            this.timer_Debug_Read_Log = new System.Windows.Forms.Timer(this.components);
            this.tmr_Debug_NowTime = new System.Windows.Forms.Timer(this.components);
            this.BW_Testing = new System.ComponentModel.BackgroundWorker();
            this.Tab_Main = new System.Windows.Forms.TabControl();
            this.Meter = new System.Windows.Forms.TabPage();
            this.tbcMeterParams = new System.Windows.Forms.TabControl();
            this.tbLimits = new System.Windows.Forms.TabPage();
            this.ucLimits = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucLimits();
            this.tbMonitoring = new System.Windows.Forms.TabPage();
            this.ucMonitoringTime = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucMonitoringTime();
            this.tbTarrification = new System.Windows.Forms.TabPage();
            this.ucActivityCalendar = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucActivityCalendar();
            this.tbMDI = new System.Windows.Forms.TabPage();
            this.ucMDIParams = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucMDIParams();
            this.tbLoad_profile = new System.Windows.Forms.TabPage();
            this.ucLoadProfile = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucLoadProfile();
            this.tbDisplayWindows = new System.Windows.Forms.TabPage();
            this.ucDisplayWindows1 = new ucCustomControl.ucDisplayWindows();
            this.tbDisplayPowerDown = new System.Windows.Forms.TabPage();
            this.ucDisplayPowerDown1 = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucDisplayPowerDown();
            this.tbContactor = new System.Windows.Forms.TabPage();
            this.ucContactor = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucContactor();
            this.tbPnlSinglePhase = new System.Windows.Forms.TabPage();
            this.ucParamSinglePhase = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucParamSinglePhase();
            this.tbCaliberation = new System.Windows.Forms.TabPage();
            this.fLP_Main = new System.Windows.Forms.FlowLayoutPanel();
            this.fLP_first_Col = new System.Windows.Forms.FlowLayoutPanel();
            this.ucCustomerReference = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucCustomerReference();
            this.ucDecimalPoint = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucDecimalPoint();
            this.ucGeneralProcess = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucGeneralProcess();
            this.ucEnergyParam = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucEnergyParam();
            this.ucCTPTRatio = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucCTPTRatio();
            this.ucPasswords = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucPasswords();
            this.gpSecurityData = new System.Windows.Forms.GroupBox();
            this.lblSecurityControl = new System.Windows.Forms.Label();
            this.cmbSecurityControl = new System.Windows.Forms.ComboBox();
            this.btnGenetrateEncryptionKey = new System.Windows.Forms.Button();
            this.btnGenerateAuthenticationKey = new System.Windows.Forms.Button();
            this.txtEncryptionKey = new System.Windows.Forms.TextBox();
            this.lblEncryptionKey = new System.Windows.Forms.Label();
            this.txtAuthenticationKey = new System.Windows.Forms.TextBox();
            this.lblAuthenticationKey = new System.Windows.Forms.Label();
            this.grpTariffOnStartGenerator = new System.Windows.Forms.GroupBox();
            this.lblTarrifOnGeneratorStart = new System.Windows.Forms.Label();
            this.cmbTarrifOnGeneratorStart = new System.Windows.Forms.ComboBox();
            this.ucClockCalib = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucClockCalib();
            this.tbRTC = new System.Windows.Forms.TabPage();
            this.ucDateTime = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucDateTime();
            this.tbTesting = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbWriteThenRead = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbReadFailCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbWriteFailCount = new System.Windows.Forms.TextBox();
            this.tbReadTestCount = new System.Windows.Forms.TextBox();
            this.tbWriteTestCount = new System.Windows.Forms.TextBox();
            this.cbIsReadParam = new System.Windows.Forms.CheckBox();
            this.btnStopParamTest = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnStartParamTest = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.cbIsWriteParam = new System.Windows.Forms.CheckBox();
            this.fLP_Main_Testing = new System.Windows.Forms.FlowLayoutPanel();
            this.FLP_ReadLog = new System.Windows.Forms.FlowLayoutPanel();
            this.gb_Debug_Read_LOG = new System.Windows.Forms.GroupBox();
            this.lbl_Slider_Start = new System.Windows.Forms.Label();
            this.lbl_Slider_StartCount = new System.Windows.Forms.Label();
            this.btn_Debug = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.txt_Debug_Log_rows_Count = new System.Windows.Forms.TextBox();
            this.hsb_Debug_RefreshRate = new System.Windows.Forms.HScrollBar();
            this.Grid_Debug = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DayProfile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gpClockUp = new System.Windows.Forms.GroupBox();
            this.btn_StopTariffTest = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnStartTimer = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.check_ClkAddMonth = new System.Windows.Forms.CheckBox();
            this.lbl_rtc_min = new System.Windows.Forms.Label();
            this.lbl_rtc_sec = new System.Windows.Forms.Label();
            this.lbl_TimeToAdd = new System.Windows.Forms.Label();
            this.lbl_ClkInterval = new System.Windows.Forms.Label();
            this.combo_ClkTimeToAdd = new System.Windows.Forms.ComboBox();
            this.combo_ClockInterval = new System.Windows.Forms.ComboBox();
            this.check_AddTime = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tbSchedule = new System.Windows.Forms.TabPage();
            this.ucScheduleTableEntry1 = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucScheduleTableEntry();
            this.tbGeneratorStart = new System.Windows.Forms.TabPage();
            this.pnlGeneratorMain = new System.Windows.Forms.Panel();
            this.ucGeneratorStart1 = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucGeneratorStart();
            this.Modem = new System.Windows.Forms.TabPage();
            this.chbWifiSettings = new System.Windows.Forms.CheckBox();
            this.tab_ModemParameters = new System.Windows.Forms.TabControl();
            this.IP_Profiles = new System.Windows.Forms.TabPage();
            this.ucIPProfiles = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucIPProfiles();
            this.WakeUp_profiles = new System.Windows.Forms.TabPage();
            this.ucWakeupProfile = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucWakeupProfile();
            this.Number_Profile = new System.Windows.Forms.TabPage();
            this.ucNumberProfile = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucNumberProfile();
            this.communication_profile = new System.Windows.Forms.TabPage();
            this.ucCommProfile = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucCommProfile();
            this.Keep_Alive = new System.Windows.Forms.TabPage();
            this.ucKeepAlive = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucKeepAlive();
            this.modem_limits = new System.Windows.Forms.TabPage();
            this.ucModemLimitsAndTime = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucModemLimitsAndTime();
            this.Modem_Initialize = new System.Windows.Forms.TabPage();
            this.ucModemInitialize = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucModemInitialize();
            this.tpHDLCSetup = new System.Windows.Forms.TabPage();
            this.ucHDLCSetup = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucHDLCSetup();
            this.tpTimeBasedEvents = new System.Windows.Forms.TabPage();
            this.ucTimeWindowParam2 = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucTimeWindowParam();
            this.ucTimeWindowParam1 = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucTimeWindowParam();
            this.fLP_Main_TBE = new System.Windows.Forms.FlowLayoutPanel();
            this.check_TBE2_PowerFail = new System.Windows.Forms.CheckBox();
            this.check_TBE1_PowerFail = new System.Windows.Forms.CheckBox();
            this.tpStatusWord = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucStatusWordMap1 = new ucCustomControl.ucStatusWordMap();
            this.tpStandardModem = new System.Windows.Forms.TabPage();
            this.ucStandardModem = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucStandardModemParameters();
            this.tpEnergyMizer = new System.Windows.Forms.TabPage();
            this.pnlEnergyMizer = new System.Windows.Forms.Panel();
            this.ucEnergyMizer1 = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucEnergyMizer();
            this.button1 = new System.Windows.Forms.Button();
            this.bhw_GetMT = new System.ComponentModel.BackgroundWorker();
            this.bgw_GetMDIAUto = new System.ComponentModel.BackgroundWorker();
            this.bgw_SetMDIAuto = new System.ComponentModel.BackgroundWorker();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButton2 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.bgw_Gettime = new System.ComponentModel.BackgroundWorker();
            this.bgw_SetTime = new System.ComponentModel.BackgroundWorker();
            this.bgw_Contactor = new System.ComponentModel.BackgroundWorker();
            this.bgw_contactor_disconnect = new System.ComponentModel.BackgroundWorker();
            this.bgw_contactor_status = new System.ComponentModel.BackgroundWorker();
            this.btn_GenerateReport = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.bgw_contactor_getParams = new System.ComponentModel.BackgroundWorker();
            this.bgw_contactor_setParams = new System.ComponentModel.BackgroundWorker();
            this.fLP_ParamButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.rdbNormal = new System.Windows.Forms.RadioButton();
            this.rdbAdvance = new System.Windows.Forms.RadioButton();
            this.lblHeading = new System.Windows.Forms.Label();
            this.kryptonButton3 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.tpStatusWordMap = new System.Windows.Forms.TabPage();
            this.Tab_Main.SuspendLayout();
            this.Meter.SuspendLayout();
            this.tbcMeterParams.SuspendLayout();
            this.tbLimits.SuspendLayout();
            this.tbMonitoring.SuspendLayout();
            this.tbTarrification.SuspendLayout();
            this.tbMDI.SuspendLayout();
            this.tbLoad_profile.SuspendLayout();
            this.tbDisplayWindows.SuspendLayout();
            this.tbDisplayPowerDown.SuspendLayout();
            this.tbContactor.SuspendLayout();
            this.tbPnlSinglePhase.SuspendLayout();
            this.tbCaliberation.SuspendLayout();
            this.fLP_Main.SuspendLayout();
            this.fLP_first_Col.SuspendLayout();
            this.gpSecurityData.SuspendLayout();
            this.grpTariffOnStartGenerator.SuspendLayout();
            this.tbRTC.SuspendLayout();
            this.tbTesting.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.fLP_Main_Testing.SuspendLayout();
            this.FLP_ReadLog.SuspendLayout();
            this.gb_Debug_Read_LOG.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Debug)).BeginInit();
            this.gpClockUp.SuspendLayout();
            this.tbSchedule.SuspendLayout();
            this.tbGeneratorStart.SuspendLayout();
            this.pnlGeneratorMain.SuspendLayout();
            this.Modem.SuspendLayout();
            this.tab_ModemParameters.SuspendLayout();
            this.IP_Profiles.SuspendLayout();
            this.WakeUp_profiles.SuspendLayout();
            this.Number_Profile.SuspendLayout();
            this.communication_profile.SuspendLayout();
            this.Keep_Alive.SuspendLayout();
            this.modem_limits.SuspendLayout();
            this.Modem_Initialize.SuspendLayout();
            this.tpHDLCSetup.SuspendLayout();
            this.tpTimeBasedEvents.SuspendLayout();
            this.tpStatusWord.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tpStandardModem.SuspendLayout();
            this.tpEnergyMizer.SuspendLayout();
            this.pnlEnergyMizer.SuspendLayout();
            this.fLP_ParamButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.AutoSize = true;
            this.lblConnectionStatus.Location = new System.Drawing.Point(164, 46);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(79, 13);
            this.lblConnectionStatus.TabIndex = 1;
            this.lblConnectionStatus.Text = "Not Connected";
            // 
            // lblCapConnectionStatus
            // 
            this.lblCapConnectionStatus.AutoSize = true;
            this.lblCapConnectionStatus.Location = new System.Drawing.Point(58, 46);
            this.lblCapConnectionStatus.Name = "lblCapConnectionStatus";
            this.lblCapConnectionStatus.Size = new System.Drawing.Size(100, 13);
            this.lblCapConnectionStatus.TabIndex = 1;
            this.lblCapConnectionStatus.Text = "Connection Status: ";
            // 
            // btn_caliberation_loadall
            // 
            this.btn_caliberation_loadall.Location = new System.Drawing.Point(145, 3);
            this.btn_caliberation_loadall.Name = "btn_caliberation_loadall";
            this.btn_caliberation_loadall.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_caliberation_loadall.Size = new System.Drawing.Size(136, 30);
            this.btn_caliberation_loadall.TabIndex = 11;
            this.btn_caliberation_loadall.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_caliberation_loadall.Values.Image")));
            this.btn_caliberation_loadall.Values.Text = "Load Parameters";
            this.btn_caliberation_loadall.Click += new System.EventHandler(this.btn_caliberation_loadall_Click);
            // 
            // btn_Caliberation_Save
            // 
            this.btn_Caliberation_Save.Location = new System.Drawing.Point(3, 3);
            this.btn_Caliberation_Save.Name = "btn_Caliberation_Save";
            this.btn_Caliberation_Save.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_Caliberation_Save.Size = new System.Drawing.Size(136, 30);
            this.btn_Caliberation_Save.TabIndex = 10;
            this.btn_Caliberation_Save.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_Caliberation_Save.Values.Image")));
            this.btn_Caliberation_Save.Values.Text = "Save Parameters";
            this.btn_Caliberation_Save.Click += new System.EventHandler(this.btn_Caliberation_Save_Click);
            // 
            // btn_GET_paramameters
            // 
            this.btn_GET_paramameters.Location = new System.Drawing.Point(287, 3);
            this.btn_GET_paramameters.Name = "btn_GET_paramameters";
            this.btn_GET_paramameters.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_GET_paramameters.Size = new System.Drawing.Size(136, 30);
            this.btn_GET_paramameters.TabIndex = 10;
            this.btn_GET_paramameters.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_GET_paramameters.Values.Image")));
            this.btn_GET_paramameters.Values.Text = "GET Parameters";
            this.btn_GET_paramameters.Click += new System.EventHandler(this.btn_GETAsync_Parameters_Click);
            // 
            // btn_SET_paramameters
            // 
            this.btn_SET_paramameters.Location = new System.Drawing.Point(429, 3);
            this.btn_SET_paramameters.Name = "btn_SET_paramameters";
            this.btn_SET_paramameters.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_SET_paramameters.Size = new System.Drawing.Size(136, 30);
            this.btn_SET_paramameters.TabIndex = 10;
            this.btn_SET_paramameters.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_SET_paramameters.Values.Image")));
            this.btn_SET_paramameters.Values.Text = "SET Parameters";
            this.btn_SET_paramameters.Click += new System.EventHandler(this.btn_SETAsync_Parameters_Click);
            // 
            // timer_ClockFWD
            // 
            this.timer_ClockFWD.Interval = 1000;
            this.timer_ClockFWD.Tick += new System.EventHandler(this.timer_ClockFWD_Tick);
            // 
            // Parameterization_BckWorkerThread
            // 
            this.Parameterization_BckWorkerThread.WorkerReportsProgress = true;
            this.Parameterization_BckWorkerThread.WorkerSupportsCancellation = true;
            // 
            // timer_Debug_Read_Log
            // 
            this.timer_Debug_Read_Log.Interval = 1000;
            this.timer_Debug_Read_Log.Tick += new System.EventHandler(this.timer_Debug_Read_Log_Tick);
            // 
            // BW_Testing
            // 
            this.BW_Testing.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BW_Testing_DoWork);
            this.BW_Testing.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BW_Testing_RunWorkerCompleted);
            // 
            // Tab_Main
            // 
            this.Tab_Main.Controls.Add(this.Meter);
            this.Tab_Main.Controls.Add(this.Modem);
            this.Tab_Main.Controls.Add(this.tpTimeBasedEvents);
            this.Tab_Main.Controls.Add(this.tpStatusWord);
            this.Tab_Main.Controls.Add(this.tpStandardModem);
            this.Tab_Main.Controls.Add(this.tpEnergyMizer);
            this.Tab_Main.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tab_Main.HotTrack = true;
            this.Tab_Main.Location = new System.Drawing.Point(16, 75);
            this.Tab_Main.Name = "Tab_Main";
            this.Tab_Main.SelectedIndex = 0;
            this.Tab_Main.Size = new System.Drawing.Size(1111, 558);
            this.Tab_Main.TabIndex = 12;
            // 
            // Meter
            // 
            this.Meter.BackColor = System.Drawing.Color.Transparent;
            this.Meter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Meter.Controls.Add(this.tbcMeterParams);
            this.Meter.Location = new System.Drawing.Point(4, 23);
            this.Meter.Name = "Meter";
            this.Meter.Size = new System.Drawing.Size(1103, 531);
            this.Meter.TabIndex = 8;
            this.Meter.Text = "Meter";
            // 
            // tbcMeterParams
            // 
            this.tbcMeterParams.Controls.Add(this.tbLimits);
            this.tbcMeterParams.Controls.Add(this.tbMonitoring);
            this.tbcMeterParams.Controls.Add(this.tbTarrification);
            this.tbcMeterParams.Controls.Add(this.tbMDI);
            this.tbcMeterParams.Controls.Add(this.tbLoad_profile);
            this.tbcMeterParams.Controls.Add(this.tbDisplayWindows);
            this.tbcMeterParams.Controls.Add(this.tbDisplayPowerDown);
            this.tbcMeterParams.Controls.Add(this.tbContactor);
            this.tbcMeterParams.Controls.Add(this.tbPnlSinglePhase);
            this.tbcMeterParams.Controls.Add(this.tbCaliberation);
            this.tbcMeterParams.Controls.Add(this.tbRTC);
            this.tbcMeterParams.Controls.Add(this.tbTesting);
            this.tbcMeterParams.Controls.Add(this.tbSchedule);
            this.tbcMeterParams.Controls.Add(this.tbGeneratorStart);
            this.tbcMeterParams.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.tbcMeterParams.HotTrack = true;
            this.tbcMeterParams.Location = new System.Drawing.Point(3, 11);
            this.tbcMeterParams.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.tbcMeterParams.Name = "tbcMeterParams";
            this.tbcMeterParams.Padding = new System.Drawing.Point(6, 0);
            this.tbcMeterParams.SelectedIndex = 0;
            this.tbcMeterParams.Size = new System.Drawing.Size(1077, 517);
            this.tbcMeterParams.TabIndex = 0;
            // 
            // tbLimits
            // 
            this.tbLimits.BackColor = System.Drawing.Color.Transparent;
            this.tbLimits.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbLimits.Controls.Add(this.ucLimits);
            this.tbLimits.Location = new System.Drawing.Point(4, 22);
            this.tbLimits.Name = "tbLimits";
            this.tbLimits.Size = new System.Drawing.Size(1069, 491);
            this.tbLimits.TabIndex = 17;
            this.tbLimits.Text = "Limits";
            // 
            // ucLimits
            // 
            this.ucLimits.BackColor = System.Drawing.Color.Transparent;
            this.ucLimits.Location = new System.Drawing.Point(29, 6);
            this.ucLimits.Name = "ucLimits";
            this.ucLimits.Size = new System.Drawing.Size(867, 478);
            this.ucLimits.TabIndex = 0;
            // 
            // tbMonitoring
            // 
            this.tbMonitoring.BackColor = System.Drawing.Color.Transparent;
            this.tbMonitoring.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbMonitoring.Controls.Add(this.ucMonitoringTime);
            this.tbMonitoring.Location = new System.Drawing.Point(4, 22);
            this.tbMonitoring.Name = "tbMonitoring";
            this.tbMonitoring.Size = new System.Drawing.Size(1069, 491);
            this.tbMonitoring.TabIndex = 9;
            this.tbMonitoring.Text = "Monitoring";
            // 
            // ucMonitoringTime
            // 
            this.ucMonitoringTime.BackColor = System.Drawing.Color.Transparent;
            this.ucMonitoringTime.Location = new System.Drawing.Point(29, 30);
            this.ucMonitoringTime.Name = "ucMonitoringTime";
            this.ucMonitoringTime.Size = new System.Drawing.Size(740, 460);
            this.ucMonitoringTime.TabIndex = 0;
            // 
            // tbTarrification
            // 
            this.tbTarrification.BackColor = System.Drawing.Color.Transparent;
            this.tbTarrification.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbTarrification.Controls.Add(this.ucActivityCalendar);
            this.tbTarrification.Location = new System.Drawing.Point(4, 22);
            this.tbTarrification.Margin = new System.Windows.Forms.Padding(10, 10, 3, 3);
            this.tbTarrification.Name = "tbTarrification";
            this.tbTarrification.Size = new System.Drawing.Size(1069, 491);
            this.tbTarrification.TabIndex = 7;
            this.tbTarrification.Text = "Activity Calendar";
            // 
            // ucActivityCalendar
            // 
            this.ucActivityCalendar.BackColor = System.Drawing.Color.Transparent;
            this.ucActivityCalendar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucActivityCalendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucActivityCalendar.Location = new System.Drawing.Point(0, 0);
            this.ucActivityCalendar.Name = "ucActivityCalendar";
            this.ucActivityCalendar.Size = new System.Drawing.Size(1069, 491);
            this.ucActivityCalendar.TabIndex = 0;
            // 
            // tbMDI
            // 
            this.tbMDI.BackColor = System.Drawing.Color.Transparent;
            this.tbMDI.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbMDI.Controls.Add(this.ucMDIParams);
            this.tbMDI.Location = new System.Drawing.Point(4, 22);
            this.tbMDI.Name = "tbMDI";
            this.tbMDI.Size = new System.Drawing.Size(1069, 491);
            this.tbMDI.TabIndex = 10;
            this.tbMDI.Text = "MDI";
            // 
            // ucMDIParams
            // 
            this.ucMDIParams.AutoSize = true;
            this.ucMDIParams.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ucMDIParams.BackColor = System.Drawing.Color.Transparent;
            this.ucMDIParams.Location = new System.Drawing.Point(57, 26);
            this.ucMDIParams.Margin = new System.Windows.Forms.Padding(0);
            this.ucMDIParams.Name = "ucMDIParams";
            this.ucMDIParams.Size = new System.Drawing.Size(592, 373);
            this.ucMDIParams.TabIndex = 0;
            // 
            // tbLoad_profile
            // 
            this.tbLoad_profile.BackColor = System.Drawing.Color.Transparent;
            this.tbLoad_profile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tbLoad_profile.Controls.Add(this.ucLoadProfile);
            this.tbLoad_profile.Location = new System.Drawing.Point(4, 22);
            this.tbLoad_profile.Name = "tbLoad_profile";
            this.tbLoad_profile.Size = new System.Drawing.Size(1069, 491);
            this.tbLoad_profile.TabIndex = 11;
            this.tbLoad_profile.Text = "Load Profiles";
            // 
            // ucLoadProfile
            // 
            this.ucLoadProfile.BackColor = System.Drawing.Color.Transparent;
            this.ucLoadProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLoadProfile.Location = new System.Drawing.Point(0, 0);
            this.ucLoadProfile.Name = "ucLoadProfile";
            this.ucLoadProfile.Size = new System.Drawing.Size(1069, 491);
            this.ucLoadProfile.TabIndex = 0;
            // 
            // tbDisplayWindows
            // 
            this.tbDisplayWindows.BackColor = System.Drawing.Color.Transparent;
            this.tbDisplayWindows.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbDisplayWindows.Controls.Add(this.ucDisplayWindows1);
            this.tbDisplayWindows.Location = new System.Drawing.Point(4, 22);
            this.tbDisplayWindows.Name = "tbDisplayWindows";
            this.tbDisplayWindows.Size = new System.Drawing.Size(1069, 491);
            this.tbDisplayWindows.TabIndex = 12;
            this.tbDisplayWindows.Text = "Display Windows";
            // 
            // ucDisplayWindows1
            // 
            this.ucDisplayWindows1.ActiveSeason = null;
            this.ucDisplayWindows1.AutoSize = true;
            this.ucDisplayWindows1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ucDisplayWindows1.BackColor = System.Drawing.Color.Transparent;
            this.ucDisplayWindows1.Customercode = null;
            this.ucDisplayWindows1.Location = new System.Drawing.Point(14, 14);
            this.ucDisplayWindows1.Name = "ucDisplayWindows1";
            this.ucDisplayWindows1.Size = new System.Drawing.Size(1119, 465);
            this.ucDisplayWindows1.TabIndex = 0;
            // 
            // tbDisplayPowerDown
            // 
            this.tbDisplayPowerDown.BackColor = System.Drawing.SystemColors.Control;
            this.tbDisplayPowerDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbDisplayPowerDown.Controls.Add(this.ucDisplayPowerDown1);
            this.tbDisplayPowerDown.Location = new System.Drawing.Point(4, 22);
            this.tbDisplayPowerDown.Name = "tbDisplayPowerDown";
            this.tbDisplayPowerDown.Size = new System.Drawing.Size(1069, 491);
            this.tbDisplayPowerDown.TabIndex = 18;
            this.tbDisplayPowerDown.Text = "Display Power Down";
            // 
            // ucDisplayPowerDown1
            // 
            this.ucDisplayPowerDown1.AutoSize = true;
            this.ucDisplayPowerDown1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ucDisplayPowerDown1.BackColor = System.Drawing.Color.Transparent;
            this.ucDisplayPowerDown1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucDisplayPowerDown1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDisplayPowerDown1.Location = new System.Drawing.Point(0, 0);
            this.ucDisplayPowerDown1.Name = "ucDisplayPowerDown1";
            this.ucDisplayPowerDown1.Size = new System.Drawing.Size(1069, 491);
            this.ucDisplayPowerDown1.TabIndex = 0;
            // 
            // tbContactor
            // 
            this.tbContactor.BackColor = System.Drawing.Color.Transparent;
            this.tbContactor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbContactor.Controls.Add(this.ucContactor);
            this.tbContactor.Location = new System.Drawing.Point(4, 22);
            this.tbContactor.Name = "tbContactor";
            this.tbContactor.Size = new System.Drawing.Size(1069, 491);
            this.tbContactor.TabIndex = 14;
            this.tbContactor.Text = "Contactor";
            // 
            // ucContactor
            // 
            this.ucContactor.AutoSize = true;
            this.ucContactor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ucContactor.BackColor = System.Drawing.Color.Transparent;
            this.ucContactor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucContactor.Location = new System.Drawing.Point(0, 0);
            this.ucContactor.Name = "ucContactor";
            this.ucContactor.Size = new System.Drawing.Size(1069, 491);
            this.ucContactor.TabIndex = 0;
            this.ucContactor.Load += new System.EventHandler(this.ucContactor_Load_1);
            // 
            // tbPnlSinglePhase
            // 
            this.tbPnlSinglePhase.BackColor = System.Drawing.Color.Transparent;
            this.tbPnlSinglePhase.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbPnlSinglePhase.Controls.Add(this.ucParamSinglePhase);
            this.tbPnlSinglePhase.Location = new System.Drawing.Point(4, 22);
            this.tbPnlSinglePhase.Name = "tbPnlSinglePhase";
            this.tbPnlSinglePhase.Size = new System.Drawing.Size(1069, 491);
            this.tbPnlSinglePhase.TabIndex = 15;
            this.tbPnlSinglePhase.Text = "Single Phase";
            // 
            // ucParamSinglePhase
            // 
            this.ucParamSinglePhase.AutoSize = true;
            this.ucParamSinglePhase.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ucParamSinglePhase.BackColor = System.Drawing.Color.Transparent;
            this.ucParamSinglePhase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucParamSinglePhase.Location = new System.Drawing.Point(0, 0);
            this.ucParamSinglePhase.Name = "ucParamSinglePhase";
            this.ucParamSinglePhase.Size = new System.Drawing.Size(1069, 491);
            this.ucParamSinglePhase.TabIndex = 0;
            // 
            // tbCaliberation
            // 
            this.tbCaliberation.BackColor = System.Drawing.Color.Transparent;
            this.tbCaliberation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbCaliberation.Controls.Add(this.fLP_Main);
            this.tbCaliberation.Location = new System.Drawing.Point(4, 22);
            this.tbCaliberation.Margin = new System.Windows.Forms.Padding(5, 10, 5, 10);
            this.tbCaliberation.Name = "tbCaliberation";
            this.tbCaliberation.Size = new System.Drawing.Size(1069, 491);
            this.tbCaliberation.TabIndex = 13;
            this.tbCaliberation.Text = "Misc";
            // 
            // fLP_Main
            // 
            this.fLP_Main.BackColor = System.Drawing.Color.Transparent;
            this.fLP_Main.Controls.Add(this.fLP_first_Col);
            this.fLP_Main.Controls.Add(this.ucClockCalib);
            this.fLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fLP_Main.Location = new System.Drawing.Point(0, 0);
            this.fLP_Main.Name = "fLP_Main";
            this.fLP_Main.Size = new System.Drawing.Size(1069, 491);
            this.fLP_Main.TabIndex = 8;
            // 
            // fLP_first_Col
            // 
            this.fLP_first_Col.AutoSize = true;
            this.fLP_first_Col.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_first_Col.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.fLP_first_Col.Controls.Add(this.ucCustomerReference);
            this.fLP_first_Col.Controls.Add(this.ucDecimalPoint);
            this.fLP_first_Col.Controls.Add(this.ucGeneralProcess);
            this.fLP_first_Col.Controls.Add(this.ucEnergyParam);
            this.fLP_first_Col.Controls.Add(this.ucCTPTRatio);
            this.fLP_first_Col.Controls.Add(this.ucPasswords);
            this.fLP_first_Col.Controls.Add(this.gpSecurityData);
            this.fLP_first_Col.Controls.Add(this.grpTariffOnStartGenerator);
            this.fLP_first_Col.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fLP_first_Col.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLP_first_Col.Location = new System.Drawing.Point(3, 3);
            this.fLP_first_Col.Name = "fLP_first_Col";
            this.fLP_first_Col.Size = new System.Drawing.Size(700, 475);
            this.fLP_first_Col.TabIndex = 7;
            // 
            // ucCustomerReference
            // 
            this.ucCustomerReference.AutoSize = true;
            this.ucCustomerReference.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ucCustomerReference.BackColor = System.Drawing.Color.Transparent;
            this.ucCustomerReference.Location = new System.Drawing.Point(3, 3);
            this.ucCustomerReference.Name = "ucCustomerReference";
            this.ucCustomerReference.Size = new System.Drawing.Size(344, 177);
            this.ucCustomerReference.TabIndex = 15;
            // 
            // ucDecimalPoint
            // 
            this.ucDecimalPoint.AutoSize = true;
            this.ucDecimalPoint.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ucDecimalPoint.BackColor = System.Drawing.Color.Transparent;
            this.ucDecimalPoint.Location = new System.Drawing.Point(3, 186);
            this.ucDecimalPoint.Name = "ucDecimalPoint";
            this.ucDecimalPoint.Size = new System.Drawing.Size(344, 215);
            this.ucDecimalPoint.TabIndex = 10;
            // 
            // ucGeneralProcess
            // 
            this.ucGeneralProcess.BackColor = System.Drawing.Color.Transparent;
            this.ucGeneralProcess.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucGeneralProcess.Location = new System.Drawing.Point(3, 407);
            this.ucGeneralProcess.Name = "ucGeneralProcess";
            this.ucGeneralProcess.Size = new System.Drawing.Size(344, 48);
            this.ucGeneralProcess.TabIndex = 13;
            // 
            // ucEnergyParam
            // 
            this.ucEnergyParam.AutoSize = true;
            this.ucEnergyParam.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ucEnergyParam.BackColor = System.Drawing.Color.Transparent;
            this.ucEnergyParam.Location = new System.Drawing.Point(353, 3);
            this.ucEnergyParam.Name = "ucEnergyParam";
            this.ucEnergyParam.Size = new System.Drawing.Size(344, 99);
            this.ucEnergyParam.TabIndex = 11;
            // 
            // ucCTPTRatio
            // 
            this.ucCTPTRatio.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ucCTPTRatio.BackColor = System.Drawing.Color.Transparent;
            this.ucCTPTRatio.Cursor = System.Windows.Forms.Cursors.Default;
            this.ucCTPTRatio.Location = new System.Drawing.Point(353, 108);
            this.ucCTPTRatio.Name = "ucCTPTRatio";
            this.ucCTPTRatio.Size = new System.Drawing.Size(331, 113);
            this.ucCTPTRatio.TabIndex = 9;
            // 
            // ucPasswords
            // 
            this.ucPasswords.BackColor = System.Drawing.Color.Transparent;
            this.ucPasswords.Location = new System.Drawing.Point(353, 227);
            this.ucPasswords.Name = "ucPasswords";
            this.ucPasswords.Size = new System.Drawing.Size(331, 61);
            this.ucPasswords.TabIndex = 12;
            // 
            // gpSecurityData
            // 
            this.gpSecurityData.Controls.Add(this.lblSecurityControl);
            this.gpSecurityData.Controls.Add(this.cmbSecurityControl);
            this.gpSecurityData.Controls.Add(this.btnGenetrateEncryptionKey);
            this.gpSecurityData.Controls.Add(this.btnGenerateAuthenticationKey);
            this.gpSecurityData.Controls.Add(this.txtEncryptionKey);
            this.gpSecurityData.Controls.Add(this.lblEncryptionKey);
            this.gpSecurityData.Controls.Add(this.txtAuthenticationKey);
            this.gpSecurityData.Controls.Add(this.lblAuthenticationKey);
            this.gpSecurityData.ForeColor = System.Drawing.Color.Maroon;
            this.gpSecurityData.Location = new System.Drawing.Point(353, 294);
            this.gpSecurityData.Name = "gpSecurityData";
            this.gpSecurityData.Size = new System.Drawing.Size(330, 97);
            this.gpSecurityData.TabIndex = 8;
            this.gpSecurityData.TabStop = false;
            this.gpSecurityData.Text = "Security Data";
            // 
            // lblSecurityControl
            // 
            this.lblSecurityControl.AutoSize = true;
            this.lblSecurityControl.ForeColor = System.Drawing.Color.Navy;
            this.lblSecurityControl.Location = new System.Drawing.Point(8, 74);
            this.lblSecurityControl.Name = "lblSecurityControl";
            this.lblSecurityControl.Size = new System.Drawing.Size(95, 15);
            this.lblSecurityControl.TabIndex = 44;
            this.lblSecurityControl.Text = "Security Control";
            // 
            // cmbSecurityControl
            // 
            this.cmbSecurityControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSecurityControl.FormattingEnabled = true;
            this.cmbSecurityControl.Location = new System.Drawing.Point(120, 70);
            this.cmbSecurityControl.Name = "cmbSecurityControl";
            this.cmbSecurityControl.Size = new System.Drawing.Size(202, 23);
            this.cmbSecurityControl.TabIndex = 43;
            // 
            // btnGenetrateEncryptionKey
            // 
            this.btnGenetrateEncryptionKey.Location = new System.Drawing.Point(246, 43);
            this.btnGenetrateEncryptionKey.Name = "btnGenetrateEncryptionKey";
            this.btnGenetrateEncryptionKey.Size = new System.Drawing.Size(76, 23);
            this.btnGenetrateEncryptionKey.TabIndex = 42;
            this.btnGenetrateEncryptionKey.Text = "Generate";
            this.btnGenetrateEncryptionKey.UseVisualStyleBackColor = true;
            this.btnGenetrateEncryptionKey.Click += new System.EventHandler(this.btnGenetrateEncryptionKey_Click);
            // 
            // btnGenerateAuthenticationKey
            // 
            this.btnGenerateAuthenticationKey.Location = new System.Drawing.Point(246, 14);
            this.btnGenerateAuthenticationKey.Name = "btnGenerateAuthenticationKey";
            this.btnGenerateAuthenticationKey.Size = new System.Drawing.Size(76, 23);
            this.btnGenerateAuthenticationKey.TabIndex = 42;
            this.btnGenerateAuthenticationKey.Text = "Generate";
            this.btnGenerateAuthenticationKey.UseVisualStyleBackColor = true;
            this.btnGenerateAuthenticationKey.Click += new System.EventHandler(this.btnGenerateAuthenticationKey_Click);
            // 
            // txtEncryptionKey
            // 
            this.txtEncryptionKey.Location = new System.Drawing.Point(121, 43);
            this.txtEncryptionKey.MaxLength = 32;
            this.txtEncryptionKey.Name = "txtEncryptionKey";
            this.txtEncryptionKey.ReadOnly = true;
            this.txtEncryptionKey.Size = new System.Drawing.Size(123, 23);
            this.txtEncryptionKey.TabIndex = 40;
            this.txtEncryptionKey.Text = "D0D1D2D3D4D5D6D7D8D9DADBDCDDDEDF";
            // 
            // lblEncryptionKey
            // 
            this.lblEncryptionKey.AutoSize = true;
            this.lblEncryptionKey.ForeColor = System.Drawing.Color.Navy;
            this.lblEncryptionKey.Location = new System.Drawing.Point(8, 47);
            this.lblEncryptionKey.Name = "lblEncryptionKey";
            this.lblEncryptionKey.Size = new System.Drawing.Size(88, 15);
            this.lblEncryptionKey.TabIndex = 41;
            this.lblEncryptionKey.Text = "Encryption Key";
            // 
            // txtAuthenticationKey
            // 
            this.txtAuthenticationKey.Location = new System.Drawing.Point(121, 14);
            this.txtAuthenticationKey.MaxLength = 32;
            this.txtAuthenticationKey.Name = "txtAuthenticationKey";
            this.txtAuthenticationKey.ReadOnly = true;
            this.txtAuthenticationKey.Size = new System.Drawing.Size(123, 23);
            this.txtAuthenticationKey.TabIndex = 40;
            this.txtAuthenticationKey.Text = "D0D1D2D3D4D5D6D7D8D9DADBDCDDDEDF";
            // 
            // lblAuthenticationKey
            // 
            this.lblAuthenticationKey.AutoSize = true;
            this.lblAuthenticationKey.ForeColor = System.Drawing.Color.Navy;
            this.lblAuthenticationKey.Location = new System.Drawing.Point(8, 18);
            this.lblAuthenticationKey.Name = "lblAuthenticationKey";
            this.lblAuthenticationKey.Size = new System.Drawing.Size(112, 15);
            this.lblAuthenticationKey.TabIndex = 41;
            this.lblAuthenticationKey.Text = "Authentication Key";
            // 
            // grpTariffOnStartGenerator
            // 
            this.grpTariffOnStartGenerator.Controls.Add(this.lblTarrifOnGeneratorStart);
            this.grpTariffOnStartGenerator.Controls.Add(this.cmbTarrifOnGeneratorStart);
            this.grpTariffOnStartGenerator.ForeColor = System.Drawing.Color.Maroon;
            this.grpTariffOnStartGenerator.Location = new System.Drawing.Point(353, 397);
            this.grpTariffOnStartGenerator.Name = "grpTariffOnStartGenerator";
            this.grpTariffOnStartGenerator.Size = new System.Drawing.Size(330, 49);
            this.grpTariffOnStartGenerator.TabIndex = 8;
            this.grpTariffOnStartGenerator.TabStop = false;
            this.grpTariffOnStartGenerator.Text = "Tariff On Generator Start";
            // 
            // lblTarrifOnGeneratorStart
            // 
            this.lblTarrifOnGeneratorStart.AutoSize = true;
            this.lblTarrifOnGeneratorStart.ForeColor = System.Drawing.Color.Navy;
            this.lblTarrifOnGeneratorStart.Location = new System.Drawing.Point(10, 26);
            this.lblTarrifOnGeneratorStart.Name = "lblTarrifOnGeneratorStart";
            this.lblTarrifOnGeneratorStart.Size = new System.Drawing.Size(35, 15);
            this.lblTarrifOnGeneratorStart.TabIndex = 44;
            this.lblTarrifOnGeneratorStart.Text = "Tarrif";
            // 
            // cmbTarrifOnGeneratorStart
            // 
            this.cmbTarrifOnGeneratorStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTarrifOnGeneratorStart.FormattingEnabled = true;
            this.cmbTarrifOnGeneratorStart.Items.AddRange(new object[] {
            "T1",
            "T2",
            "T3",
            "T4"});
            this.cmbTarrifOnGeneratorStart.Location = new System.Drawing.Point(122, 22);
            this.cmbTarrifOnGeneratorStart.Name = "cmbTarrifOnGeneratorStart";
            this.cmbTarrifOnGeneratorStart.Size = new System.Drawing.Size(202, 23);
            this.cmbTarrifOnGeneratorStart.TabIndex = 43;
            // 
            // ucClockCalib
            // 
            this.ucClockCalib.BackColor = System.Drawing.Color.Transparent;
            this.ucClockCalib.ForeColor = System.Drawing.Color.Black;
            this.ucClockCalib.Location = new System.Drawing.Point(709, 3);
            this.ucClockCalib.Name = "ucClockCalib";
            this.ucClockCalib.Size = new System.Drawing.Size(316, 475);
            this.ucClockCalib.TabIndex = 16;
            // 
            // tbRTC
            // 
            this.tbRTC.BackColor = System.Drawing.Color.Transparent;
            this.tbRTC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbRTC.Controls.Add(this.ucDateTime);
            this.tbRTC.Location = new System.Drawing.Point(4, 22);
            this.tbRTC.Name = "tbRTC";
            this.tbRTC.Size = new System.Drawing.Size(1069, 491);
            this.tbRTC.TabIndex = 16;
            this.tbRTC.Text = "DateTime";
            // 
            // ucDateTime
            // 
            this.ucDateTime.BackColor = System.Drawing.Color.Transparent;
            this.ucDateTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDateTime.Location = new System.Drawing.Point(0, 0);
            this.ucDateTime.Name = "ucDateTime";
            this.ucDateTime.Size = new System.Drawing.Size(1069, 491);
            this.ucDateTime.TabIndex = 0;
            this.ucDateTime.Load += new System.EventHandler(this.ucDateTime_Load);
            // 
            // tbTesting
            // 
            this.tbTesting.BackColor = System.Drawing.Color.Transparent;
            this.tbTesting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbTesting.Controls.Add(this.groupBox1);
            this.tbTesting.Controls.Add(this.fLP_Main_Testing);
            this.tbTesting.Controls.Add(this.progressBar1);
            this.tbTesting.Location = new System.Drawing.Point(4, 22);
            this.tbTesting.Name = "tbTesting";
            this.tbTesting.Size = new System.Drawing.Size(1069, 491);
            this.tbTesting.TabIndex = 6;
            this.tbTesting.Text = "Testing";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbWriteThenRead);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbReadFailCount);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbWriteFailCount);
            this.groupBox1.Controls.Add(this.tbReadTestCount);
            this.groupBox1.Controls.Add(this.tbWriteTestCount);
            this.groupBox1.Controls.Add(this.cbIsReadParam);
            this.groupBox1.Controls.Add(this.btnStopParamTest);
            this.groupBox1.Controls.Add(this.btnStartParamTest);
            this.groupBox1.Controls.Add(this.cbIsWriteParam);
            this.groupBox1.Location = new System.Drawing.Point(803, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(205, 201);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Param Read/Write";
            // 
            // cbWriteThenRead
            // 
            this.cbWriteThenRead.AutoSize = true;
            this.cbWriteThenRead.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbWriteThenRead.ForeColor = System.Drawing.Color.Navy;
            this.cbWriteThenRead.Location = new System.Drawing.Point(9, 124);
            this.cbWriteThenRead.Name = "cbWriteThenRead";
            this.cbWriteThenRead.Size = new System.Drawing.Size(144, 19);
            this.cbWriteThenRead.TabIndex = 53;
            this.cbWriteThenRead.Text = "Write First then Read";
            this.cbWriteThenRead.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(28, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 51;
            this.label2.Text = "Read Fail";
            // 
            // tbReadFailCount
            // 
            this.tbReadFailCount.BackColor = System.Drawing.Color.White;
            this.tbReadFailCount.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.tbReadFailCount.Location = new System.Drawing.Point(134, 90);
            this.tbReadFailCount.Name = "tbReadFailCount";
            this.tbReadFailCount.ReadOnly = true;
            this.tbReadFailCount.Size = new System.Drawing.Size(65, 21);
            this.tbReadFailCount.TabIndex = 52;
            this.tbReadFailCount.Text = "000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(28, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Write Fail";
            // 
            // tbWriteFailCount
            // 
            this.tbWriteFailCount.BackColor = System.Drawing.Color.White;
            this.tbWriteFailCount.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.tbWriteFailCount.Location = new System.Drawing.Point(134, 66);
            this.tbWriteFailCount.Name = "tbWriteFailCount";
            this.tbWriteFailCount.ReadOnly = true;
            this.tbWriteFailCount.Size = new System.Drawing.Size(65, 21);
            this.tbWriteFailCount.TabIndex = 50;
            this.tbWriteFailCount.Text = "000";
            // 
            // tbReadTestCount
            // 
            this.tbReadTestCount.BackColor = System.Drawing.Color.White;
            this.tbReadTestCount.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.tbReadTestCount.Location = new System.Drawing.Point(134, 42);
            this.tbReadTestCount.Name = "tbReadTestCount";
            this.tbReadTestCount.ReadOnly = true;
            this.tbReadTestCount.Size = new System.Drawing.Size(65, 21);
            this.tbReadTestCount.TabIndex = 49;
            this.tbReadTestCount.Text = "000";
            // 
            // tbWriteTestCount
            // 
            this.tbWriteTestCount.BackColor = System.Drawing.Color.White;
            this.tbWriteTestCount.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.tbWriteTestCount.Location = new System.Drawing.Point(134, 21);
            this.tbWriteTestCount.Name = "tbWriteTestCount";
            this.tbWriteTestCount.ReadOnly = true;
            this.tbWriteTestCount.Size = new System.Drawing.Size(65, 21);
            this.tbWriteTestCount.TabIndex = 48;
            this.tbWriteTestCount.Text = "000";
            // 
            // cbIsReadParam
            // 
            this.cbIsReadParam.AutoSize = true;
            this.cbIsReadParam.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIsReadParam.ForeColor = System.Drawing.Color.Navy;
            this.cbIsReadParam.Location = new System.Drawing.Point(9, 44);
            this.cbIsReadParam.Name = "cbIsReadParam";
            this.cbIsReadParam.Size = new System.Drawing.Size(120, 19);
            this.cbIsReadParam.TabIndex = 7;
            this.cbIsReadParam.Text = "Read Parameters";
            this.cbIsReadParam.UseVisualStyleBackColor = true;
            // 
            // btnStopParamTest
            // 
            this.btnStopParamTest.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStopParamTest.Location = new System.Drawing.Point(105, 149);
            this.btnStopParamTest.Name = "btnStopParamTest";
            this.btnStopParamTest.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnStopParamTest.Size = new System.Drawing.Size(94, 30);
            this.btnStopParamTest.TabIndex = 8;
            this.btnStopParamTest.Values.Image = ((System.Drawing.Image)(resources.GetObject("btnStopParamTest.Values.Image")));
            this.btnStopParamTest.Values.Text = "Stop";
            this.btnStopParamTest.Click += new System.EventHandler(this.kryptonButton3_Click);
            // 
            // btnStartParamTest
            // 
            this.btnStartParamTest.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartParamTest.Location = new System.Drawing.Point(8, 149);
            this.btnStartParamTest.Name = "btnStartParamTest";
            this.btnStartParamTest.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnStartParamTest.Size = new System.Drawing.Size(91, 30);
            this.btnStartParamTest.TabIndex = 7;
            this.btnStartParamTest.Values.Image = ((System.Drawing.Image)(resources.GetObject("btnStartParamTest.Values.Image")));
            this.btnStartParamTest.Values.Text = "Start";
            this.btnStartParamTest.Click += new System.EventHandler(this.btnStartParamTest_Click);
            // 
            // cbIsWriteParam
            // 
            this.cbIsWriteParam.AutoSize = true;
            this.cbIsWriteParam.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIsWriteParam.ForeColor = System.Drawing.Color.Navy;
            this.cbIsWriteParam.Location = new System.Drawing.Point(9, 23);
            this.cbIsWriteParam.Name = "cbIsWriteParam";
            this.cbIsWriteParam.Size = new System.Drawing.Size(125, 19);
            this.cbIsWriteParam.TabIndex = 46;
            this.cbIsWriteParam.Text = "Write Parameters";
            this.cbIsWriteParam.UseVisualStyleBackColor = true;
            // 
            // fLP_Main_Testing
            // 
            this.fLP_Main_Testing.AutoSize = true;
            this.fLP_Main_Testing.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_Main_Testing.BackColor = System.Drawing.Color.Transparent;
            this.fLP_Main_Testing.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.fLP_Main_Testing.Controls.Add(this.FLP_ReadLog);
            this.fLP_Main_Testing.Controls.Add(this.gpClockUp);
            this.fLP_Main_Testing.Location = new System.Drawing.Point(3, 3);
            this.fLP_Main_Testing.Name = "fLP_Main_Testing";
            this.fLP_Main_Testing.Size = new System.Drawing.Size(784, 396);
            this.fLP_Main_Testing.TabIndex = 45;
            // 
            // FLP_ReadLog
            // 
            this.FLP_ReadLog.AutoSize = true;
            this.FLP_ReadLog.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FLP_ReadLog.Controls.Add(this.gb_Debug_Read_LOG);
            this.FLP_ReadLog.Controls.Add(this.Grid_Debug);
            this.FLP_ReadLog.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.FLP_ReadLog.Location = new System.Drawing.Point(3, 3);
            this.FLP_ReadLog.Name = "FLP_ReadLog";
            this.FLP_ReadLog.Size = new System.Drawing.Size(561, 390);
            this.FLP_ReadLog.TabIndex = 44;
            // 
            // gb_Debug_Read_LOG
            // 
            this.gb_Debug_Read_LOG.BackColor = System.Drawing.Color.Transparent;
            this.gb_Debug_Read_LOG.Controls.Add(this.lbl_Slider_Start);
            this.gb_Debug_Read_LOG.Controls.Add(this.lbl_Slider_StartCount);
            this.gb_Debug_Read_LOG.Controls.Add(this.btn_Debug);
            this.gb_Debug_Read_LOG.Controls.Add(this.txt_Debug_Log_rows_Count);
            this.gb_Debug_Read_LOG.Controls.Add(this.hsb_Debug_RefreshRate);
            this.gb_Debug_Read_LOG.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_Debug_Read_LOG.ForeColor = System.Drawing.Color.Maroon;
            this.gb_Debug_Read_LOG.Location = new System.Drawing.Point(3, 3);
            this.gb_Debug_Read_LOG.Name = "gb_Debug_Read_LOG";
            this.gb_Debug_Read_LOG.Size = new System.Drawing.Size(555, 66);
            this.gb_Debug_Read_LOG.TabIndex = 42;
            this.gb_Debug_Read_LOG.TabStop = false;
            this.gb_Debug_Read_LOG.Text = "READ LOG";
            // 
            // lbl_Slider_Start
            // 
            this.lbl_Slider_Start.AutoSize = true;
            this.lbl_Slider_Start.Location = new System.Drawing.Point(305, 22);
            this.lbl_Slider_Start.Name = "lbl_Slider_Start";
            this.lbl_Slider_Start.Size = new System.Drawing.Size(19, 20);
            this.lbl_Slider_Start.TabIndex = 42;
            this.lbl_Slider_Start.Text = "1";
            // 
            // lbl_Slider_StartCount
            // 
            this.lbl_Slider_StartCount.AutoSize = true;
            this.lbl_Slider_StartCount.Location = new System.Drawing.Point(514, 22);
            this.lbl_Slider_StartCount.Name = "lbl_Slider_StartCount";
            this.lbl_Slider_StartCount.Size = new System.Drawing.Size(29, 20);
            this.lbl_Slider_StartCount.TabIndex = 42;
            this.lbl_Slider_StartCount.Text = "10";
            // 
            // btn_Debug
            // 
            this.btn_Debug.Location = new System.Drawing.Point(33, 22);
            this.btn_Debug.Name = "btn_Debug";
            this.btn_Debug.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_Debug.Size = new System.Drawing.Size(168, 30);
            this.btn_Debug.TabIndex = 3;
            this.btn_Debug.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_Debug.Values.Image")));
            this.btn_Debug.Values.Text = "START READ LOG";
            this.btn_Debug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // txt_Debug_Log_rows_Count
            // 
            this.txt_Debug_Log_rows_Count.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Debug_Log_rows_Count.Location = new System.Drawing.Point(219, 22);
            this.txt_Debug_Log_rows_Count.MaxLength = 4;
            this.txt_Debug_Log_rows_Count.Name = "txt_Debug_Log_rows_Count";
            this.txt_Debug_Log_rows_Count.Size = new System.Drawing.Size(76, 29);
            this.txt_Debug_Log_rows_Count.TabIndex = 40;
            this.txt_Debug_Log_rows_Count.Text = "1";
            this.txt_Debug_Log_rows_Count.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_Debug_Log_rows_Count.TextChanged += new System.EventHandler(this.txt_Debug_Log_rows_Count_TextChanged);
            // 
            // hsb_Debug_RefreshRate
            // 
            this.hsb_Debug_RefreshRate.LargeChange = 1000;
            this.hsb_Debug_RefreshRate.Location = new System.Drawing.Point(327, 20);
            this.hsb_Debug_RefreshRate.Maximum = 10000;
            this.hsb_Debug_RefreshRate.Minimum = 1000;
            this.hsb_Debug_RefreshRate.Name = "hsb_Debug_RefreshRate";
            this.hsb_Debug_RefreshRate.Size = new System.Drawing.Size(185, 27);
            this.hsb_Debug_RefreshRate.SmallChange = 100;
            this.hsb_Debug_RefreshRate.TabIndex = 41;
            this.hsb_Debug_RefreshRate.Value = 1000;
            this.hsb_Debug_RefreshRate.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hsb_Debug_RefreshRate_Scroll);
            // 
            // Grid_Debug
            // 
            this.Grid_Debug.AllowUserToAddRows = false;
            this.Grid_Debug.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Grid_Debug.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.Grid_Debug.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Grid_Debug.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.Grid_Debug.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column5,
            this.Column3,
            this.DayProfile,
            this.Column4});
            this.Grid_Debug.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Grid_Debug.Location = new System.Drawing.Point(3, 75);
            this.Grid_Debug.Name = "Grid_Debug";
            this.Grid_Debug.ReadOnly = true;
            this.Grid_Debug.RowHeadersWidth = 30;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Grid_Debug.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.Grid_Debug.Size = new System.Drawing.Size(555, 312);
            this.Grid_Debug.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.Format = "d";
            dataGridViewCellStyle6.NullValue = null;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column1.HeaderText = "Meter Date";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 94;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle7.Format = "t";
            this.Column2.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column2.HeaderText = "Meter Time";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.Width = 97;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column5.HeaderText = "System Date";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 101;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column3.HeaderText = "Season Profile";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 110;
            // 
            // DayProfile
            // 
            this.DayProfile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DayProfile.HeaderText = "Day Profile";
            this.DayProfile.Name = "DayProfile";
            this.DayProfile.ReadOnly = true;
            this.DayProfile.Width = 93;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column4.HeaderText = "Tariff";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 64;
            // 
            // gpClockUp
            // 
            this.gpClockUp.AutoSize = true;
            this.gpClockUp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gpClockUp.Controls.Add(this.btn_StopTariffTest);
            this.gpClockUp.Controls.Add(this.btnStartTimer);
            this.gpClockUp.Controls.Add(this.check_ClkAddMonth);
            this.gpClockUp.Controls.Add(this.lbl_rtc_min);
            this.gpClockUp.Controls.Add(this.lbl_rtc_sec);
            this.gpClockUp.Controls.Add(this.lbl_TimeToAdd);
            this.gpClockUp.Controls.Add(this.lbl_ClkInterval);
            this.gpClockUp.Controls.Add(this.combo_ClkTimeToAdd);
            this.gpClockUp.Controls.Add(this.combo_ClockInterval);
            this.gpClockUp.Controls.Add(this.check_AddTime);
            this.gpClockUp.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpClockUp.ForeColor = System.Drawing.Color.Maroon;
            this.gpClockUp.Location = new System.Drawing.Point(570, 70);
            this.gpClockUp.Margin = new System.Windows.Forms.Padding(3, 70, 3, 3);
            this.gpClockUp.Name = "gpClockUp";
            this.gpClockUp.Size = new System.Drawing.Size(211, 201);
            this.gpClockUp.TabIndex = 7;
            this.gpClockUp.TabStop = false;
            this.gpClockUp.Text = "RTC Update";
            // 
            // btn_StopTariffTest
            // 
            this.btn_StopTariffTest.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_StopTariffTest.Location = new System.Drawing.Point(111, 149);
            this.btn_StopTariffTest.Name = "btn_StopTariffTest";
            this.btn_StopTariffTest.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_StopTariffTest.Size = new System.Drawing.Size(94, 30);
            this.btn_StopTariffTest.TabIndex = 6;
            this.btn_StopTariffTest.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_StopTariffTest.Values.Image")));
            this.btn_StopTariffTest.Values.Text = "Stop";
            this.btn_StopTariffTest.Click += new System.EventHandler(this.btn_StopTariffTest_Click);
            // 
            // btnStartTimer
            // 
            this.btnStartTimer.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartTimer.Location = new System.Drawing.Point(17, 149);
            this.btnStartTimer.Name = "btnStartTimer";
            this.btnStartTimer.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnStartTimer.Size = new System.Drawing.Size(91, 30);
            this.btnStartTimer.TabIndex = 6;
            this.btnStartTimer.Values.Image = ((System.Drawing.Image)(resources.GetObject("btnStartTimer.Values.Image")));
            this.btnStartTimer.Values.Text = "Start";
            this.btnStartTimer.Click += new System.EventHandler(this.btnStartTimer_Click);
            // 
            // check_ClkAddMonth
            // 
            this.check_ClkAddMonth.AutoSize = true;
            this.check_ClkAddMonth.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_ClkAddMonth.ForeColor = System.Drawing.Color.Navy;
            this.check_ClkAddMonth.Location = new System.Drawing.Point(29, 124);
            this.check_ClkAddMonth.Name = "check_ClkAddMonth";
            this.check_ClkAddMonth.Size = new System.Drawing.Size(88, 19);
            this.check_ClkAddMonth.TabIndex = 5;
            this.check_ClkAddMonth.Text = "Add Month";
            this.check_ClkAddMonth.UseVisualStyleBackColor = true;
            // 
            // lbl_rtc_min
            // 
            this.lbl_rtc_min.AutoSize = true;
            this.lbl_rtc_min.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_rtc_min.ForeColor = System.Drawing.Color.Navy;
            this.lbl_rtc_min.Location = new System.Drawing.Point(165, 82);
            this.lbl_rtc_min.Name = "lbl_rtc_min";
            this.lbl_rtc_min.Size = new System.Drawing.Size(28, 15);
            this.lbl_rtc_min.TabIndex = 4;
            this.lbl_rtc_min.Text = "min";
            // 
            // lbl_rtc_sec
            // 
            this.lbl_rtc_sec.AutoSize = true;
            this.lbl_rtc_sec.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_rtc_sec.ForeColor = System.Drawing.Color.Navy;
            this.lbl_rtc_sec.Location = new System.Drawing.Point(164, 45);
            this.lbl_rtc_sec.Name = "lbl_rtc_sec";
            this.lbl_rtc_sec.Size = new System.Drawing.Size(12, 15);
            this.lbl_rtc_sec.TabIndex = 4;
            this.lbl_rtc_sec.Text = "s";
            // 
            // lbl_TimeToAdd
            // 
            this.lbl_TimeToAdd.AutoSize = true;
            this.lbl_TimeToAdd.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TimeToAdd.ForeColor = System.Drawing.Color.Navy;
            this.lbl_TimeToAdd.Location = new System.Drawing.Point(23, 86);
            this.lbl_TimeToAdd.Name = "lbl_TimeToAdd";
            this.lbl_TimeToAdd.Size = new System.Drawing.Size(74, 15);
            this.lbl_TimeToAdd.TabIndex = 3;
            this.lbl_TimeToAdd.Text = "Time to Add";
            // 
            // lbl_ClkInterval
            // 
            this.lbl_ClkInterval.AutoSize = true;
            this.lbl_ClkInterval.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ClkInterval.ForeColor = System.Drawing.Color.Navy;
            this.lbl_ClkInterval.Location = new System.Drawing.Point(23, 48);
            this.lbl_ClkInterval.Name = "lbl_ClkInterval";
            this.lbl_ClkInterval.Size = new System.Drawing.Size(49, 15);
            this.lbl_ClkInterval.TabIndex = 3;
            this.lbl_ClkInterval.Text = "Interval";
            // 
            // combo_ClkTimeToAdd
            // 
            this.combo_ClkTimeToAdd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_ClkTimeToAdd.FormattingEnabled = true;
            this.combo_ClkTimeToAdd.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "30",
            "60",
            "120",
            "180",
            "240",
            "300",
            "360",
            "720",
            "1440",
            "2880"});
            this.combo_ClkTimeToAdd.Location = new System.Drawing.Point(108, 78);
            this.combo_ClkTimeToAdd.Name = "combo_ClkTimeToAdd";
            this.combo_ClkTimeToAdd.Size = new System.Drawing.Size(51, 23);
            this.combo_ClkTimeToAdd.TabIndex = 1;
            // 
            // combo_ClockInterval
            // 
            this.combo_ClockInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_ClockInterval.FormattingEnabled = true;
            this.combo_ClockInterval.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50",
            "55",
            "60"});
            this.combo_ClockInterval.Location = new System.Drawing.Point(107, 44);
            this.combo_ClockInterval.Name = "combo_ClockInterval";
            this.combo_ClockInterval.Size = new System.Drawing.Size(51, 23);
            this.combo_ClockInterval.TabIndex = 1;
            // 
            // check_AddTime
            // 
            this.check_AddTime.AutoSize = true;
            this.check_AddTime.Checked = true;
            this.check_AddTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_AddTime.Enabled = false;
            this.check_AddTime.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_AddTime.ForeColor = System.Drawing.Color.Navy;
            this.check_AddTime.Location = new System.Drawing.Point(23, 19);
            this.check_AddTime.Name = "check_AddTime";
            this.check_AddTime.Size = new System.Drawing.Size(78, 19);
            this.check_AddTime.TabIndex = 0;
            this.check_AddTime.Text = "Add Time";
            this.check_AddTime.UseVisualStyleBackColor = true;
            this.check_AddTime.CheckedChanged += new System.EventHandler(this.check_AddTime_CheckedChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(3, 402);
            this.progressBar1.MarqueeAnimationSpeed = 35;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(564, 40);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 43;
            this.progressBar1.Visible = false;
            // 
            // tbSchedule
            // 
            this.tbSchedule.Controls.Add(this.ucScheduleTableEntry1);
            this.tbSchedule.Location = new System.Drawing.Point(4, 22);
            this.tbSchedule.Name = "tbSchedule";
            this.tbSchedule.Padding = new System.Windows.Forms.Padding(3);
            this.tbSchedule.Size = new System.Drawing.Size(1069, 491);
            this.tbSchedule.TabIndex = 19;
            this.tbSchedule.Text = "Schedule Entry";
            this.tbSchedule.UseVisualStyleBackColor = true;
            // 
            // ucScheduleTableEntry1
            // 
            this.ucScheduleTableEntry1.AutoScroll = true;
            this.ucScheduleTableEntry1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucScheduleTableEntry1.Location = new System.Drawing.Point(3, 3);
            this.ucScheduleTableEntry1.Name = "ucScheduleTableEntry1";
            this.ucScheduleTableEntry1.Size = new System.Drawing.Size(998, 609);
            this.ucScheduleTableEntry1.TabIndex = 0;
            // 
            // tbGeneratorStart
            // 
            this.tbGeneratorStart.Controls.Add(this.pnlGeneratorMain);
            this.tbGeneratorStart.Location = new System.Drawing.Point(4, 22);
            this.tbGeneratorStart.Name = "tbGeneratorStart";
            this.tbGeneratorStart.Padding = new System.Windows.Forms.Padding(3);
            this.tbGeneratorStart.Size = new System.Drawing.Size(1069, 491);
            this.tbGeneratorStart.TabIndex = 20;
            this.tbGeneratorStart.Text = "Generator Start";
            this.tbGeneratorStart.UseVisualStyleBackColor = true;
            // 
            // pnlGeneratorMain
            // 
            this.pnlGeneratorMain.AutoScroll = true;
            this.pnlGeneratorMain.Controls.Add(this.ucGeneratorStart1);
            this.pnlGeneratorMain.Location = new System.Drawing.Point(6, 6);
            this.pnlGeneratorMain.Name = "pnlGeneratorMain";
            this.pnlGeneratorMain.Size = new System.Drawing.Size(1046, 431);
            this.pnlGeneratorMain.TabIndex = 0;
            // 
            // ucGeneratorStart1
            // 
            this.ucGeneratorStart1.Location = new System.Drawing.Point(3, 3);
            this.ucGeneratorStart1.Name = "ucGeneratorStart1";
            this.ucGeneratorStart1.Size = new System.Drawing.Size(582, 178);
            this.ucGeneratorStart1.TabIndex = 0;
            // 
            // Modem
            // 
            this.Modem.BackColor = System.Drawing.Color.Transparent;
            this.Modem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Modem.Controls.Add(this.chbWifiSettings);
            this.Modem.Controls.Add(this.tab_ModemParameters);
            this.Modem.Location = new System.Drawing.Point(4, 23);
            this.Modem.Name = "Modem";
            this.Modem.Size = new System.Drawing.Size(1103, 531);
            this.Modem.TabIndex = 7;
            this.Modem.Text = "Modem";
            // 
            // chbWifiSettings
            // 
            this.chbWifiSettings.AutoSize = true;
            this.chbWifiSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbWifiSettings.ForeColor = System.Drawing.Color.Black;
            this.chbWifiSettings.Location = new System.Drawing.Point(5, 352);
            this.chbWifiSettings.Name = "chbWifiSettings";
            this.chbWifiSettings.Size = new System.Drawing.Size(100, 20);
            this.chbWifiSettings.TabIndex = 14;
            this.chbWifiSettings.Text = "Wifi Settings";
            this.chbWifiSettings.UseVisualStyleBackColor = true;
            this.chbWifiSettings.CheckedChanged += new System.EventHandler(this.chbWifiSettings_CheckedChanged);
            // 
            // tab_ModemParameters
            // 
            this.tab_ModemParameters.Controls.Add(this.IP_Profiles);
            this.tab_ModemParameters.Controls.Add(this.WakeUp_profiles);
            this.tab_ModemParameters.Controls.Add(this.Number_Profile);
            this.tab_ModemParameters.Controls.Add(this.communication_profile);
            this.tab_ModemParameters.Controls.Add(this.Keep_Alive);
            this.tab_ModemParameters.Controls.Add(this.modem_limits);
            this.tab_ModemParameters.Controls.Add(this.Modem_Initialize);
            this.tab_ModemParameters.Controls.Add(this.tpHDLCSetup);
            this.tab_ModemParameters.Location = new System.Drawing.Point(0, 0);
            this.tab_ModemParameters.Name = "tab_ModemParameters";
            this.tab_ModemParameters.SelectedIndex = 0;
            this.tab_ModemParameters.Size = new System.Drawing.Size(675, 351);
            this.tab_ModemParameters.TabIndex = 0;
            // 
            // IP_Profiles
            // 
            this.IP_Profiles.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.IP_Profiles.Controls.Add(this.ucIPProfiles);
            this.IP_Profiles.Location = new System.Drawing.Point(4, 23);
            this.IP_Profiles.Name = "IP_Profiles";
            this.IP_Profiles.Size = new System.Drawing.Size(667, 324);
            this.IP_Profiles.TabIndex = 0;
            this.IP_Profiles.Text = "IP Profile";
            this.IP_Profiles.UseVisualStyleBackColor = true;
            // 
            // ucIPProfiles
            // 
            this.ucIPProfiles.BackColor = System.Drawing.Color.Transparent;
            this.ucIPProfiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucIPProfiles.Location = new System.Drawing.Point(0, 0);
            this.ucIPProfiles.Modem_Warnings_disable = false;
            this.ucIPProfiles.Name = "ucIPProfiles";
            this.ucIPProfiles.Size = new System.Drawing.Size(667, 324);
            this.ucIPProfiles.TabIndex = 0;
            this.ucIPProfiles.Leave += new System.EventHandler(this.ucIPProfiles_Leave);
            // 
            // WakeUp_profiles
            // 
            this.WakeUp_profiles.BackColor = System.Drawing.Color.Transparent;
            this.WakeUp_profiles.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.WakeUp_profiles.Controls.Add(this.ucWakeupProfile);
            this.WakeUp_profiles.Location = new System.Drawing.Point(4, 23);
            this.WakeUp_profiles.Name = "WakeUp_profiles";
            this.WakeUp_profiles.Size = new System.Drawing.Size(667, 324);
            this.WakeUp_profiles.TabIndex = 1;
            this.WakeUp_profiles.Text = "WakeUp Profile";
            this.WakeUp_profiles.UseVisualStyleBackColor = true;
            // 
            // ucWakeupProfile
            // 
            this.ucWakeupProfile.AutoSize = true;
            this.ucWakeupProfile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ucWakeupProfile.BackColor = System.Drawing.Color.Transparent;
            this.ucWakeupProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucWakeupProfile.Location = new System.Drawing.Point(0, 0);
            this.ucWakeupProfile.Modem_Warnings_disable = false;
            this.ucWakeupProfile.Name = "ucWakeupProfile";
            this.ucWakeupProfile.Size = new System.Drawing.Size(667, 324);
            this.ucWakeupProfile.TabIndex = 0;
            // 
            // Number_Profile
            // 
            this.Number_Profile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Number_Profile.Controls.Add(this.ucNumberProfile);
            this.Number_Profile.Location = new System.Drawing.Point(4, 23);
            this.Number_Profile.Name = "Number_Profile";
            this.Number_Profile.Size = new System.Drawing.Size(667, 324);
            this.Number_Profile.TabIndex = 2;
            this.Number_Profile.Text = "Number Profile";
            this.Number_Profile.UseVisualStyleBackColor = true;
            // 
            // ucNumberProfile
            // 
            this.ucNumberProfile.AutoSize = true;
            this.ucNumberProfile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ucNumberProfile.BackColor = System.Drawing.Color.Transparent;
            this.ucNumberProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucNumberProfile.Location = new System.Drawing.Point(0, 0);
            this.ucNumberProfile.Modem_Warnings_disable = false;
            this.ucNumberProfile.Name = "ucNumberProfile";
            this.ucNumberProfile.Size = new System.Drawing.Size(667, 324);
            this.ucNumberProfile.TabIndex = 0;
            // 
            // communication_profile
            // 
            this.communication_profile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.communication_profile.Controls.Add(this.ucCommProfile);
            this.communication_profile.Location = new System.Drawing.Point(4, 23);
            this.communication_profile.Name = "communication_profile";
            this.communication_profile.Size = new System.Drawing.Size(667, 324);
            this.communication_profile.TabIndex = 3;
            this.communication_profile.Text = "Communication Profile";
            this.communication_profile.UseVisualStyleBackColor = true;
            // 
            // ucCommProfile
            // 
            this.ucCommProfile.AutoSize = true;
            this.ucCommProfile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ucCommProfile.BackColor = System.Drawing.Color.Transparent;
            this.ucCommProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCommProfile.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold);
            this.ucCommProfile.Location = new System.Drawing.Point(0, 0);
            this.ucCommProfile.Modem_Warnings_disable = false;
            this.ucCommProfile.Name = "ucCommProfile";
            this.ucCommProfile.Size = new System.Drawing.Size(667, 324);
            this.ucCommProfile.TabIndex = 0;
            // 
            // Keep_Alive
            // 
            this.Keep_Alive.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Keep_Alive.Controls.Add(this.ucKeepAlive);
            this.Keep_Alive.Location = new System.Drawing.Point(4, 23);
            this.Keep_Alive.Name = "Keep_Alive";
            this.Keep_Alive.Size = new System.Drawing.Size(667, 324);
            this.Keep_Alive.TabIndex = 4;
            this.Keep_Alive.Text = "Keep Alive";
            this.Keep_Alive.UseVisualStyleBackColor = true;
            // 
            // ucKeepAlive
            // 
            this.ucKeepAlive.BackColor = System.Drawing.Color.Transparent;
            this.ucKeepAlive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucKeepAlive.Location = new System.Drawing.Point(0, 0);
            this.ucKeepAlive.Modem_Warnings_disable = false;
            this.ucKeepAlive.Name = "ucKeepAlive";
            this.ucKeepAlive.Size = new System.Drawing.Size(667, 324);
            this.ucKeepAlive.TabIndex = 0;
            // 
            // modem_limits
            // 
            this.modem_limits.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.modem_limits.Controls.Add(this.ucModemLimitsAndTime);
            this.modem_limits.Location = new System.Drawing.Point(4, 23);
            this.modem_limits.Name = "modem_limits";
            this.modem_limits.Size = new System.Drawing.Size(667, 324);
            this.modem_limits.TabIndex = 5;
            this.modem_limits.Text = "Modem Limits";
            this.modem_limits.UseVisualStyleBackColor = true;
            // 
            // ucModemLimitsAndTime
            // 
            this.ucModemLimitsAndTime.BackColor = System.Drawing.Color.Transparent;
            this.ucModemLimitsAndTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucModemLimitsAndTime.Location = new System.Drawing.Point(0, 0);
            this.ucModemLimitsAndTime.Modem_Warnings_disable = false;
            this.ucModemLimitsAndTime.Name = "ucModemLimitsAndTime";
            this.ucModemLimitsAndTime.Size = new System.Drawing.Size(667, 324);
            this.ucModemLimitsAndTime.TabIndex = 0;
            // 
            // Modem_Initialize
            // 
            this.Modem_Initialize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Modem_Initialize.Controls.Add(this.ucModemInitialize);
            this.Modem_Initialize.Location = new System.Drawing.Point(4, 23);
            this.Modem_Initialize.Name = "Modem_Initialize";
            this.Modem_Initialize.Size = new System.Drawing.Size(667, 324);
            this.Modem_Initialize.TabIndex = 6;
            this.Modem_Initialize.Text = "Modem Initialize";
            this.Modem_Initialize.UseVisualStyleBackColor = true;
            // 
            // ucModemInitialize
            // 
            this.ucModemInitialize.AutoSize = true;
            this.ucModemInitialize.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ucModemInitialize.BackColor = System.Drawing.Color.Transparent;
            this.ucModemInitialize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucModemInitialize.Location = new System.Drawing.Point(0, 0);
            this.ucModemInitialize.Modem_Warnings_disable = false;
            this.ucModemInitialize.Name = "ucModemInitialize";
            this.ucModemInitialize.Size = new System.Drawing.Size(667, 324);
            this.ucModemInitialize.TabIndex = 0;
            // 
            // tpHDLCSetup
            // 
            this.tpHDLCSetup.Controls.Add(this.ucHDLCSetup);
            this.tpHDLCSetup.Location = new System.Drawing.Point(4, 23);
            this.tpHDLCSetup.Name = "tpHDLCSetup";
            this.tpHDLCSetup.Size = new System.Drawing.Size(667, 324);
            this.tpHDLCSetup.TabIndex = 7;
            this.tpHDLCSetup.Text = "HDLC Setup";
            this.tpHDLCSetup.UseVisualStyleBackColor = true;
            // 
            // ucHDLCSetup
            // 
            this.ucHDLCSetup.Location = new System.Drawing.Point(9, 1);
            this.ucHDLCSetup.Name = "ucHDLCSetup";
            this.ucHDLCSetup.Size = new System.Drawing.Size(410, 275);
            this.ucHDLCSetup.TabIndex = 0;
            this.ucHDLCSetup.Load += new System.EventHandler(this.ucHDLCSetup_Load);
            // 
            // tpTimeBasedEvents
            // 
            this.tpTimeBasedEvents.AutoScroll = true;
            this.tpTimeBasedEvents.BackColor = System.Drawing.Color.Transparent;
            this.tpTimeBasedEvents.Controls.Add(this.ucTimeWindowParam2);
            this.tpTimeBasedEvents.Controls.Add(this.ucTimeWindowParam1);
            this.tpTimeBasedEvents.Controls.Add(this.fLP_Main_TBE);
            this.tpTimeBasedEvents.Controls.Add(this.check_TBE2_PowerFail);
            this.tpTimeBasedEvents.Controls.Add(this.check_TBE1_PowerFail);
            this.tpTimeBasedEvents.Location = new System.Drawing.Point(4, 23);
            this.tpTimeBasedEvents.Name = "tpTimeBasedEvents";
            this.tpTimeBasedEvents.Size = new System.Drawing.Size(1103, 531);
            this.tpTimeBasedEvents.TabIndex = 9;
            this.tpTimeBasedEvents.Text = "Meter Scheduling";
            // 
            // ucTimeWindowParam2
            // 
            this.ucTimeWindowParam2.BackColor = System.Drawing.Color.Transparent;
            this.ucTimeWindowParam2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucTimeWindowParam2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.ucTimeWindowParam2.Location = new System.Drawing.Point(475, 14);
            this.ucTimeWindowParam2.Name = "ucTimeWindowParam2";
            this.ucTimeWindowParam2.Size = new System.Drawing.Size(450, 272);
            this.ucTimeWindowParam2.TabIndex = 49;
            this.ucTimeWindowParam2.TimeWindowTitle = "Time Window 1";
            // 
            // ucTimeWindowParam1
            // 
            this.ucTimeWindowParam1.BackColor = System.Drawing.Color.Transparent;
            this.ucTimeWindowParam1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucTimeWindowParam1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.ucTimeWindowParam1.Location = new System.Drawing.Point(19, 14);
            this.ucTimeWindowParam1.Name = "ucTimeWindowParam1";
            this.ucTimeWindowParam1.Size = new System.Drawing.Size(450, 268);
            this.ucTimeWindowParam1.TabIndex = 48;
            this.ucTimeWindowParam1.TimeWindowTitle = "Time Window 1";
            // 
            // fLP_Main_TBE
            // 
            this.fLP_Main_TBE.AutoSize = true;
            this.fLP_Main_TBE.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_Main_TBE.Location = new System.Drawing.Point(3, 3);
            this.fLP_Main_TBE.Name = "fLP_Main_TBE";
            this.fLP_Main_TBE.Size = new System.Drawing.Size(0, 0);
            this.fLP_Main_TBE.TabIndex = 47;
            // 
            // check_TBE2_PowerFail
            // 
            this.check_TBE2_PowerFail.AutoSize = true;
            this.check_TBE2_PowerFail.Location = new System.Drawing.Point(209, 286);
            this.check_TBE2_PowerFail.Name = "check_TBE2_PowerFail";
            this.check_TBE2_PowerFail.Size = new System.Drawing.Size(161, 18);
            this.check_TBE2_PowerFail.TabIndex = 44;
            this.check_TBE2_PowerFail.Text = "Disable on Power Fail TBE2";
            this.check_TBE2_PowerFail.UseVisualStyleBackColor = true;
            this.check_TBE2_PowerFail.CheckedChanged += new System.EventHandler(this.check_TBE2_PowerFail_CheckedChanged);
            // 
            // check_TBE1_PowerFail
            // 
            this.check_TBE1_PowerFail.AutoSize = true;
            this.check_TBE1_PowerFail.Location = new System.Drawing.Point(17, 286);
            this.check_TBE1_PowerFail.Name = "check_TBE1_PowerFail";
            this.check_TBE1_PowerFail.Size = new System.Drawing.Size(161, 18);
            this.check_TBE1_PowerFail.TabIndex = 43;
            this.check_TBE1_PowerFail.Text = "Disable on Power Fail TBE1";
            this.check_TBE1_PowerFail.UseVisualStyleBackColor = true;
            this.check_TBE1_PowerFail.CheckedChanged += new System.EventHandler(this.check_TBE1_PowerFail_CheckedChanged);
            // 
            // tpStatusWord
            // 
            this.tpStatusWord.Controls.Add(this.panel1);
            this.tpStatusWord.Location = new System.Drawing.Point(4, 23);
            this.tpStatusWord.Name = "tpStatusWord";
            this.tpStatusWord.Padding = new System.Windows.Forms.Padding(3);
            this.tpStatusWord.Size = new System.Drawing.Size(1103, 531);
            this.tpStatusWord.TabIndex = 10;
            this.tpStatusWord.Text = "Status Word Map";
            this.tpStatusWord.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ucStatusWordMap1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1097, 525);
            this.panel1.TabIndex = 0;
            // 
            // ucStatusWordMap1
            // 
            this.ucStatusWordMap1.Location = new System.Drawing.Point(3, 3);
            this.ucStatusWordMap1.Name = "ucStatusWordMap1";
            this.ucStatusWordMap1.Obj_ApplicationController = null;
            this.ucStatusWordMap1.Size = new System.Drawing.Size(945, 465);
            this.ucStatusWordMap1.StatusWordMap_Type = SharedCode.Comm.Param.StatusWordMapType.StatusWordMap_1;
            this.ucStatusWordMap1.TabIndex = 0;
            // 
            // tpStandardModem
            // 
            this.tpStandardModem.Controls.Add(this.ucStandardModem);
            this.tpStandardModem.Location = new System.Drawing.Point(4, 23);
            this.tpStandardModem.Name = "tpStandardModem";
            this.tpStandardModem.Padding = new System.Windows.Forms.Padding(3);
            this.tpStandardModem.Size = new System.Drawing.Size(1103, 531);
            this.tpStandardModem.TabIndex = 11;
            this.tpStandardModem.Text = "Standard Modem";
            this.tpStandardModem.UseVisualStyleBackColor = true;
            // 
            // ucStandardModem
            // 
            this.ucStandardModem.Location = new System.Drawing.Point(6, 6);
            this.ucStandardModem.Name = "ucStandardModem";
            this.ucStandardModem.Size = new System.Drawing.Size(750, 344);
            this.ucStandardModem.TabIndex = 0;
            this.ucStandardModem.Load += new System.EventHandler(this.ucStandardModem_Load);
            // 
            // tpEnergyMizer
            // 
            this.tpEnergyMizer.AutoScroll = true;
            this.tpEnergyMizer.Controls.Add(this.pnlEnergyMizer);
            this.tpEnergyMizer.Location = new System.Drawing.Point(4, 23);
            this.tpEnergyMizer.Name = "tpEnergyMizer";
            this.tpEnergyMizer.Padding = new System.Windows.Forms.Padding(3);
            this.tpEnergyMizer.Size = new System.Drawing.Size(1103, 531);
            this.tpEnergyMizer.TabIndex = 12;
            this.tpEnergyMizer.Text = "Energy Mizer";
            this.tpEnergyMizer.UseVisualStyleBackColor = true;
            // 
            // pnlEnergyMizer
            // 
            this.pnlEnergyMizer.AutoScroll = true;
            this.pnlEnergyMizer.Controls.Add(this.ucEnergyMizer1);
            this.pnlEnergyMizer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEnergyMizer.Location = new System.Drawing.Point(3, 3);
            this.pnlEnergyMizer.Name = "pnlEnergyMizer";
            this.pnlEnergyMizer.Size = new System.Drawing.Size(1097, 525);
            this.pnlEnergyMizer.TabIndex = 1;
            // 
            // ucEnergyMizer1
            // 
            this.ucEnergyMizer1.Application_Controller = null;
            this.ucEnergyMizer1.Location = new System.Drawing.Point(7, 7);
            this.ucEnergyMizer1.Name = "ucEnergyMizer1";
            this.ucEnergyMizer1.Size = new System.Drawing.Size(1095, 528);
            this.ucEnergyMizer1.TabIndex = 0;
            this.ucEnergyMizer1.Load += new System.EventHandler(this.ucEnergyMizer1_Load);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(530, 151);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // bhw_GetMT
            // 
            this.bhw_GetMT.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bhw_GetMT_DoWork);
            this.bhw_GetMT.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bhw_GetMT_RunWorkerCompleted);
            // 
            // bgw_GetMDIAUto
            // 
            this.bgw_GetMDIAUto.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_MDIAUto_DoWork);
            this.bgw_GetMDIAUto.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_MDIAUto_RunWorkerCompleted);
            // 
            // bgw_SetMDIAuto
            // 
            this.bgw_SetMDIAuto.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_SetMDIAuto_DoWork);
            this.bgw_SetMDIAuto.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_SetMDIAuto_RunWorkerCompleted);
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonButton1.Location = new System.Drawing.Point(17, 87);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.kryptonButton1.Size = new System.Drawing.Size(104, 41);
            this.kryptonButton1.TabIndex = 3;
            this.kryptonButton1.Values.Text = "Read Time";
            // 
            // kryptonButton2
            // 
            this.kryptonButton2.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonButton2.Location = new System.Drawing.Point(127, 87);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.kryptonButton2.Size = new System.Drawing.Size(110, 41);
            this.kryptonButton2.TabIndex = 3;
            this.kryptonButton2.Values.Text = "Write Time";
            // 
            // bgw_Gettime
            // 
            this.bgw_Gettime.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_time_DoWork);
            this.bgw_Gettime.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_time_RunWorkerCompleted);
            // 
            // bgw_SetTime
            // 
            this.bgw_SetTime.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_SetTime_DoWork);
            this.bgw_SetTime.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_SetTime_RunWorkerCompleted);
            // 
            // bgw_Contactor
            // 
            this.bgw_Contactor.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_Contactor_DoWork);
            this.bgw_Contactor.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_Contactor_RunWorkerCompleted);
            // 
            // bgw_contactor_disconnect
            // 
            this.bgw_contactor_disconnect.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_contactor_disconnect_DoWork);
            this.bgw_contactor_disconnect.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_contactor_disconnect_RunWorkerCompleted);
            // 
            // bgw_contactor_status
            // 
            this.bgw_contactor_status.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_contactor_status_DoWork);
            this.bgw_contactor_status.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_contactor_status_RunWorkerCompleted);
            // 
            // btn_GenerateReport
            // 
            this.btn_GenerateReport.Location = new System.Drawing.Point(571, 3);
            this.btn_GenerateReport.Name = "btn_GenerateReport";
            this.btn_GenerateReport.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_GenerateReport.Size = new System.Drawing.Size(136, 30);
            this.btn_GenerateReport.TabIndex = 10;
            this.btn_GenerateReport.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_GenerateReport.Values.Image")));
            this.btn_GenerateReport.Values.Text = "Generate Report";
            this.btn_GenerateReport.Visible = false;
            this.btn_GenerateReport.Click += new System.EventHandler(this.btn_GenerateReport_Click);
            // 
            // bgw_contactor_getParams
            // 
            this.bgw_contactor_getParams.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_contactor_getParams_DoWork);
            this.bgw_contactor_getParams.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_contactor_getParams_RunWorkerCompleted);
            // 
            // bgw_contactor_setParams
            // 
            this.bgw_contactor_setParams.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_contactor_setParams_DoWork);
            this.bgw_contactor_setParams.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_contactor_setParams_RunWorkerCompleted);
            // 
            // fLP_ParamButtons
            // 
            this.fLP_ParamButtons.AutoSize = true;
            this.fLP_ParamButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_ParamButtons.BackColor = System.Drawing.Color.Transparent;
            this.fLP_ParamButtons.Controls.Add(this.btn_Caliberation_Save);
            this.fLP_ParamButtons.Controls.Add(this.btn_caliberation_loadall);
            this.fLP_ParamButtons.Controls.Add(this.btn_GET_paramameters);
            this.fLP_ParamButtons.Controls.Add(this.btn_SET_paramameters);
            this.fLP_ParamButtons.Controls.Add(this.btn_GenerateReport);
            this.fLP_ParamButtons.Controls.Add(this.rdbNormal);
            this.fLP_ParamButtons.Controls.Add(this.rdbAdvance);
            this.fLP_ParamButtons.Location = new System.Drawing.Point(16, 33);
            this.fLP_ParamButtons.Name = "fLP_ParamButtons";
            this.fLP_ParamButtons.Size = new System.Drawing.Size(900, 36);
            this.fLP_ParamButtons.TabIndex = 14;
            // 
            // rdbNormal
            // 
            this.rdbNormal.AutoSize = true;
            this.rdbNormal.Checked = true;
            this.rdbNormal.Location = new System.Drawing.Point(713, 3);
            this.rdbNormal.Name = "rdbNormal";
            this.rdbNormal.Size = new System.Drawing.Size(84, 17);
            this.rdbNormal.TabIndex = 12;
            this.rdbNormal.TabStop = true;
            this.rdbNormal.Text = "Normal View";
            this.rdbNormal.UseVisualStyleBackColor = true;
            // 
            // rdbAdvance
            // 
            this.rdbAdvance.AutoSize = true;
            this.rdbAdvance.Location = new System.Drawing.Point(803, 3);
            this.rdbAdvance.Name = "rdbAdvance";
            this.rdbAdvance.Size = new System.Drawing.Size(94, 17);
            this.rdbAdvance.TabIndex = 13;
            this.rdbAdvance.Text = "Advance View";
            this.rdbAdvance.UseVisualStyleBackColor = true;
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.BackColor = System.Drawing.Color.Transparent;
            this.lblHeading.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.Black;
            this.lblHeading.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblHeading.Location = new System.Drawing.Point(400, 1);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(222, 29);
            this.lblHeading.TabIndex = 13;
            this.lblHeading.Text = "       Parameterization";
            // 
            // kryptonButton3
            // 
            this.kryptonButton3.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonButton3.Location = new System.Drawing.Point(105, 149);
            this.kryptonButton3.Name = "kryptonButton3";
            this.kryptonButton3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.kryptonButton3.Size = new System.Drawing.Size(94, 30);
            this.kryptonButton3.TabIndex = 8;
            this.kryptonButton3.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonButton3.Values.Image")));
            this.kryptonButton3.Values.Text = "Stop";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.ForeColor = System.Drawing.Color.Navy;
            this.checkBox1.Location = new System.Drawing.Point(8, 47);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(120, 19);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Read Parameters";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox3.ForeColor = System.Drawing.Color.Navy;
            this.checkBox3.Location = new System.Drawing.Point(8, 95);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(89, 19);
            this.checkBox3.TabIndex = 47;
            this.checkBox3.Text = "Update RTC";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // tpStatusWordMap
            // 
            this.tpStatusWordMap.Location = new System.Drawing.Point(4, 23);
            this.tpStatusWordMap.Name = "tpStatusWordMap";
            this.tpStatusWordMap.Padding = new System.Windows.Forms.Padding(3);
            this.tpStatusWordMap.Size = new System.Drawing.Size(1114, 540);
            this.tpStatusWordMap.TabIndex = 10;
            this.tpStatusWordMap.Text = "Status Word Map";
            this.tpStatusWordMap.UseVisualStyleBackColor = true;
            // 
            // pnlParameterization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.fLP_ParamButtons);
            this.Controls.Add(this.Tab_Main);
            this.Controls.Add(this.lblHeading);
            this.DoubleBuffered = true;
            this.Name = "pnlParameterization";
            this.Size = new System.Drawing.Size(1209, 636);
            this.Load += new System.EventHandler(this.pnlParameterization_Load);
            this.Tab_Main.ResumeLayout(false);
            this.Meter.ResumeLayout(false);
            this.tbcMeterParams.ResumeLayout(false);
            this.tbLimits.ResumeLayout(false);
            this.tbMonitoring.ResumeLayout(false);
            this.tbTarrification.ResumeLayout(false);
            this.tbMDI.ResumeLayout(false);
            this.tbMDI.PerformLayout();
            this.tbLoad_profile.ResumeLayout(false);
            this.tbDisplayWindows.ResumeLayout(false);
            this.tbDisplayWindows.PerformLayout();
            this.tbDisplayPowerDown.ResumeLayout(false);
            this.tbDisplayPowerDown.PerformLayout();
            this.tbContactor.ResumeLayout(false);
            this.tbContactor.PerformLayout();
            this.tbPnlSinglePhase.ResumeLayout(false);
            this.tbPnlSinglePhase.PerformLayout();
            this.tbCaliberation.ResumeLayout(false);
            this.fLP_Main.ResumeLayout(false);
            this.fLP_Main.PerformLayout();
            this.fLP_first_Col.ResumeLayout(false);
            this.fLP_first_Col.PerformLayout();
            this.gpSecurityData.ResumeLayout(false);
            this.gpSecurityData.PerformLayout();
            this.grpTariffOnStartGenerator.ResumeLayout(false);
            this.grpTariffOnStartGenerator.PerformLayout();
            this.tbRTC.ResumeLayout(false);
            this.tbTesting.ResumeLayout(false);
            this.tbTesting.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.fLP_Main_Testing.ResumeLayout(false);
            this.fLP_Main_Testing.PerformLayout();
            this.FLP_ReadLog.ResumeLayout(false);
            this.gb_Debug_Read_LOG.ResumeLayout(false);
            this.gb_Debug_Read_LOG.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Debug)).EndInit();
            this.gpClockUp.ResumeLayout(false);
            this.gpClockUp.PerformLayout();
            this.tbSchedule.ResumeLayout(false);
            this.tbGeneratorStart.ResumeLayout(false);
            this.pnlGeneratorMain.ResumeLayout(false);
            this.Modem.ResumeLayout(false);
            this.Modem.PerformLayout();
            this.tab_ModemParameters.ResumeLayout(false);
            this.IP_Profiles.ResumeLayout(false);
            this.WakeUp_profiles.ResumeLayout(false);
            this.WakeUp_profiles.PerformLayout();
            this.Number_Profile.ResumeLayout(false);
            this.Number_Profile.PerformLayout();
            this.communication_profile.ResumeLayout(false);
            this.communication_profile.PerformLayout();
            this.Keep_Alive.ResumeLayout(false);
            this.modem_limits.ResumeLayout(false);
            this.Modem_Initialize.ResumeLayout(false);
            this.Modem_Initialize.PerformLayout();
            this.tpHDLCSetup.ResumeLayout(false);
            this.tpTimeBasedEvents.ResumeLayout(false);
            this.tpTimeBasedEvents.PerformLayout();
            this.tpStatusWord.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tpStandardModem.ResumeLayout(false);
            this.tpEnergyMizer.ResumeLayout(false);
            this.pnlEnergyMizer.ResumeLayout(false);
            this.fLP_ParamButtons.ResumeLayout(false);
            this.fLP_ParamButtons.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblConnectionStatus;
        public System.Windows.Forms.Label lblCapConnectionStatus;
        public System.Windows.Forms.TabControl tbcMeterParams;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_GET_paramameters;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.GroupBox gpClockUp;
        private System.Windows.Forms.ComboBox combo_ClockInterval;
        private System.Windows.Forms.CheckBox check_AddTime;
        private System.Windows.Forms.ComboBox combo_ClkTimeToAdd;
        private System.Windows.Forms.Label lbl_ClkInterval;
        private System.Windows.Forms.Label lbl_TimeToAdd;
        private System.Windows.Forms.Label lbl_rtc_min;
        private System.Windows.Forms.Label lbl_rtc_sec;
        private System.Windows.Forms.Timer timer_ClockFWD;
        private System.Windows.Forms.CheckBox check_ClkAddMonth;
        internal System.ComponentModel.BackgroundWorker Parameterization_BckWorkerThread;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnStartTimer;
        private System.Windows.Forms.TabPage tbTesting;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_Debug;
        private System.Windows.Forms.HScrollBar hsb_Debug_RefreshRate;
        private System.Windows.Forms.TextBox txt_Debug_Log_rows_Count;
        private System.Windows.Forms.Timer timer_Debug_Read_Log;
        private System.Windows.Forms.GroupBox gb_Debug_Read_LOG;
        private System.Windows.Forms.Timer tmr_Debug_NowTime;
        private System.Windows.Forms.Label lbl_Slider_Start;
        private System.Windows.Forms.Label lbl_Slider_StartCount;
        
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn DayProfile;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_StopTariffTest;
        private System.ComponentModel.BackgroundWorker BW_Testing;
        private System.Windows.Forms.ProgressBar progressBar1;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView Grid_Debug;
        private System.Windows.Forms.TabPage Modem;
        private System.Windows.Forms.TabControl tab_ModemParameters;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.TabPage Meter;
        public System.Windows.Forms.TabControl Tab_Main;
        private System.ComponentModel.BackgroundWorker bhw_GetMT;
        private System.ComponentModel.BackgroundWorker bgw_GetMDIAUto;
        private System.ComponentModel.BackgroundWorker bgw_SetMDIAuto;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton2;
        private System.ComponentModel.BackgroundWorker bgw_Gettime;
        private System.ComponentModel.BackgroundWorker bgw_SetTime;
        private System.ComponentModel.BackgroundWorker bgw_Contactor;
        private System.ComponentModel.BackgroundWorker bgw_contactor_disconnect;
        private System.ComponentModel.BackgroundWorker bgw_contactor_status;
        public ComponentFactory.Krypton.Toolkit.KryptonButton btn_SET_paramameters;
        public ComponentFactory.Krypton.Toolkit.KryptonButton btn_GenerateReport;
        private System.ComponentModel.BackgroundWorker bgw_contactor_getParams;
        private System.ComponentModel.BackgroundWorker bgw_contactor_setParams;
        private TabPage IP_Profiles;
        private TabPage WakeUp_profiles;
        private TabPage Number_Profile;
        private TabPage communication_profile;
        private TabPage Keep_Alive;
        private TabPage modem_limits;
        private TabPage Modem_Initialize;
        /// Meter TabControl TabPages
        private TabPage tbTarrification;
        private TabPage tbMonitoring;
        private TabPage tbMDI;
        private TabPage tbDisplayWindows;
        private TabPage tbCaliberation;
        private TabPage tbContactor;
        private TabPage tbPnlSinglePhase;
        private TabPage tbRTC;
        private TabPage tbLimits;
        /// Param Modem UCCustomControls
        //private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucIPProfiles ucIPProfiles;
        //private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucWakeupProfile ucWakeupProfile;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucNumberProfile ucNumberProfile;
        //private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucCommProfile ucCommProfile;
        //private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucKeepAlive ucKeepAlive;
        //private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucModemLimitsAndTime ucModemLimitsAndTime;
        //private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucModemInitialize ucModemInitialize;
        ///TabControl Meter ucCustomControls
        //private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucActivityCalendar ucActivityCalendar;
        //private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucLimits ucLimits;
        //private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucMonitoringTime ucMonitoringTime;
        //private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucLoadProfile ucLoadProfile;
        //private ucDisplayWindows ucDisplayWindows1;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucCustomerReference ucCustomerReference;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucPasswords ucPasswords;
        //private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucCTPTRatio ucCTPTRatio;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucEnergyParam ucEnergyParam;
        //private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucDecimalPoint ucDecimalPoint;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucClockCalib ucClockCalib;
        //private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucContactor ucContactor;
        //private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucDateTime ucDateTime;
        //private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucParamSinglePhase ucParamSinglePhase;
        private TabPage tpTimeBasedEvents;
        private CheckBox check_TBE2_PowerFail;
        private CheckBox check_TBE1_PowerFail;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucTimeWindowParam ucTimeWindowParam1;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucTimeWindowParam ucTimeWindowParam2;
        //private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucMDIParams ucMDIParams;
        private TabPage tbDisplayPowerDown;
        //private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucDisplayPowerDown ucDisplayPowerDown;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucGeneralProcess ucGeneralProcess;
        private FlowLayoutPanel fLP_Main_TBE;
        private FlowLayoutPanel fLP_first_Col;
        private FlowLayoutPanel fLP_Main;
        private FlowLayoutPanel FLP_ReadLog;
        private FlowLayoutPanel fLP_Main_Testing;
        internal ComponentFactory.Krypton.Toolkit.KryptonButton btn_caliberation_loadall;
        internal ComponentFactory.Krypton.Toolkit.KryptonButton btn_Caliberation_Save;
        private FlowLayoutPanel fLP_ParamButtons;
        private Button button1;
        private GroupBox groupBox1;
        private CheckBox cbIsReadParam;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnStopParamTest;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnStartParamTest;
        private CheckBox cbIsWriteParam;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton3;
        private CheckBox checkBox1;
        private CheckBox checkBox3;
        private TextBox tbWriteTestCount;
        private TextBox tbReadTestCount;
        private CheckBox cbWriteThenRead;
        private Label label2;
        private TextBox tbReadFailCount;
        private Label label1;
        private TextBox tbWriteFailCount;
        private RadioButton rdbNormal;
        private RadioButton rdbAdvance;
        public GroupBox gpSecurityData;
        private Label lblSecurityControl;
        private ComboBox cmbSecurityControl;
        private Button btnGenetrateEncryptionKey;
        private Button btnGenerateAuthenticationKey;
        private TextBox txtEncryptionKey;
        private Label lblEncryptionKey;
        private TextBox txtAuthenticationKey;
        private Label lblAuthenticationKey;
        private TabPage tpStatusWord;
        private Panel panel1;
        private TabPage tpStatusWordMap;
        private ucStatusWordMap ucStatusWordMap1;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucLimits ucLimits;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucMonitoringTime ucMonitoringTime;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucIPProfiles ucIPProfiles;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucWakeupProfile ucWakeupProfile;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucActivityCalendar ucActivityCalendar;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucMDIParams ucMDIParams;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucDisplayPowerDown ucDisplayPowerDown1;
        private ucDisplayWindows ucDisplayWindows1;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucContactor ucContactor;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucParamSinglePhase ucParamSinglePhase;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucCTPTRatio ucCTPTRatio;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucDecimalPoint ucDecimalPoint;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucDateTime ucDateTime;
        //private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucClockCalib ucClockCalib1;
        //private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucCustomerReference ucCustomerReference1;
        //private TextBox textBox2;
        //private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucGeneralProcess ucGeneralProcess1;
        //private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucPasswords ucPasswords1;
        //private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucEnergyParam ucEnergyParam1;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucCommProfile ucCommProfile;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucKeepAlive ucKeepAlive;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucModemLimitsAndTime ucModemLimitsAndTime;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucModemInitialize ucModemInitialize;
        private TabPage tpStandardModem;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucStandardModemParameters ucStandardModem;
        private CheckBox chbWifiSettings;
        private TabPage tpHDLCSetup;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucHDLCSetup ucHDLCSetup;
        private TabPage tbSchedule;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucScheduleTableEntry ucScheduleTableEntry1;
        private TabPage tpEnergyMizer;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucEnergyMizer ucEnergyMizer1;
        private Panel pnlEnergyMizer;
        private TabPage tbLoad_profile;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucLoadProfile ucLoadProfile;
        public GroupBox grpTariffOnStartGenerator;
        private Label lblTarrifOnGeneratorStart;
        private ComboBox cmbTarrifOnGeneratorStart;
        private TabPage tbGeneratorStart;
        private Panel pnlGeneratorMain;
        private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucGeneratorStart ucGeneratorStart1;
        //private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucNumberProfile ucNumberProfile1;
        //private FlowLayoutPanel flowLayoutPanel1;
        //private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucTimeWindowParam ucTimeWindowParam4;
        //private AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucTimeWindowParam ucTimeWindowParam3;
    }
}
