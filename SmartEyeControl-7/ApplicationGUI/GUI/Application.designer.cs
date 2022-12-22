//using ucCustomControl;
using comm.DataContainer;
using SharedCode.Comm.DataContainer;
using System;
using System.Windows.Forms;
//using SmartEyeControl_7;
namespace GUI
{
    partial class FrmContainer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmContainer));
            this.stStatus = new System.Windows.Forms.StatusStrip();
            this.stStatus_lblConnnStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.stStatus_lblFirmwareVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.stStatus_lblSerialNo = new System.Windows.Forms.ToolStripStatusLabel();
            this.stStatus_stringEmpty = new System.Windows.Forms.ToolStripStatusLabel();
            this.stStatusIOActivity = new System.Windows.Forms.ToolStripStatusLabel();
            this.stlblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.lblMainHeading = new System.Windows.Forms.Label();
            this.IpConContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.GetMeterInfoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readMeterInfoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DropConnMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeListMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblPortStatus = new System.Windows.Forms.Label();
            this.lblcapStatus = new System.Windows.Forms.Label();
            this.btnOpenClose = new System.Windows.Forms.Button();
            this.comboPort = new System.Windows.Forms.ComboBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.communicationConfigDialogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlPicIcons = new System.Windows.Forms.Panel();
            this.llblReloadRights = new System.Windows.Forms.LinkLabel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.flpParameterIcon = new System.Windows.Forms.FlowLayoutPanel();
            this.pic_Parameterization = new System.Windows.Forms.PictureBox();
            this.lbl_headparam = new System.Windows.Forms.Label();
            this.flpBillingIcon = new System.Windows.Forms.FlowLayoutPanel();
            this.pic_Billing = new System.Windows.Forms.PictureBox();
            this.lbl_headBill = new System.Windows.Forms.Label();
            this.flpLoadProfileIcon = new System.Windows.Forms.FlowLayoutPanel();
            this.pic_LoadProfile = new System.Windows.Forms.PictureBox();
            this.lbl_headLp = new System.Windows.Forms.Label();
            this.flpEventsIcon = new System.Windows.Forms.FlowLayoutPanel();
            this.pic_Events = new System.Windows.Forms.PictureBox();
            this.lbl_headevents = new System.Windows.Forms.Label();
            this.flpInstantaneousIcon = new System.Windows.Forms.FlowLayoutPanel();
            this.pic_Instantaneous = new System.Windows.Forms.PictureBox();
            this.lbl_headIns = new System.Windows.Forms.Label();
            this.flpSettingIcon = new System.Windows.Forms.FlowLayoutPanel();
            this.pic_settings = new System.Windows.Forms.PictureBox();
            this.lbl_headSettings = new System.Windows.Forms.Label();
            this.flpDebugIcon = new System.Windows.Forms.FlowLayoutPanel();
            this.pic_Debug = new System.Windows.Forms.PictureBox();
            this.lbl_headdebug = new System.Windows.Forms.Label();
            this.flpAdminIcon = new System.Windows.Forms.FlowLayoutPanel();
            this.pic_admin = new System.Windows.Forms.PictureBox();
            this.lbl_headAdmin = new System.Windows.Forms.Label();
            this.flpHelpIcon = new System.Windows.Forms.FlowLayoutPanel();
            this.pic_AboutMe = new System.Windows.Forms.PictureBox();
            this.lbl_headhelp = new System.Windows.Forms.Label();
            this.flpLogOffIcon = new System.Windows.Forms.FlowLayoutPanel();
            this.pic_logoff = new System.Windows.Forms.PictureBox();
            this.lbl_headLogOff = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlKeys = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_loginName = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.Panel_Welcome = new ucCustomControl.Welcome();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlsuperAdminPanel1 = new SmartEyeControl_7.superAdminPanel();
            this.pnlBilling1 = new SmartEyeControl_7.ApplicationGUI.GUI.PnlBilling();
            this.Panel_Instantaneous = new ucCustomControl.Instantaneous();
            this.Panel_Events = new ucCustomControl.pnlEvents();
            this.Panel_Debugging = new ucCustomControl.pnlDebugging();
            this.pnlParameterization1 = new ucCustomControl.pnlParameterization();
            this.pnlLoad_Profile = new ucCustomControl.pnlLoadProfile();
            this.gp_IR_Port = new System.Windows.Forms.GroupBox();
            this.lblRefreshPorts = new System.Windows.Forms.Label();
            this.cmbIRPorts = new System.Windows.Forms.ComboBox();
            this.lblSelPort = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.cmb = new System.Windows.Forms.ComboBox();
            this.toolIPConn = new System.Windows.Forms.ToolTip(this.components);
            this.cmbMeterSerial = new System.Windows.Forms.ListBox();
            this.timer_Connect = new System.Windows.Forms.Timer(this.components);
            this.pnlCommunication = new System.Windows.Forms.Panel();
            this.tcSetting = new System.Windows.Forms.TabControl();
            this.tpConnection = new System.Windows.Forms.TabPage();
            this.gbHDLCAddress = new System.Windows.Forms.GroupBox();
            this.txtHDLCAddress = new System.Windows.Forms.TextBox();
            this.gpClientConfig = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_DefaultPwd = new System.Windows.Forms.Button();
            this.txtAssociationPaswd = new System.Windows.Forms.TextBox();
            this.cmbManufacturers = new System.Windows.Forms.ComboBox();
            this.lblManufacturer = new System.Windows.Forms.Label();
            this.cmbAssociations = new System.Windows.Forms.ComboBox();
            this.lblAssociation = new System.Windows.Forms.Label();
            this.lblDevices = new System.Windows.Forms.Label();
            this.cmbDevices = new System.Windows.Forms.ComboBox();
            this.gpPhysicalConnection = new System.Windows.Forms.GroupBox();
            this.gp_WakeUp_Paras = new System.Windows.Forms.GroupBox();
            this.lnkbtnRefreshServer = new System.Windows.Forms.LinkLabel();
            this.lnkbtnConfigDialog = new System.Windows.Forms.LinkLabel();
            this.cmbIOConnections = new System.Windows.Forms.ComboBox();
            this.btnConnectApplication = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.gpMtrSerial = new System.Windows.Forms.GroupBox();
            this.lnkHeartBeatList = new System.Windows.Forms.LinkLabel();
            this.lnkTCPStatus = new System.Windows.Forms.LinkLabel();
            this.lnk_Disconnect_forcedly = new System.Windows.Forms.LinkLabel();
            this.tpHLS = new System.Windows.Forms.TabPage();
            this.pnlAuthenticationKeys = new System.Windows.Forms.Panel();
            this.cmbSecurity = new System.Windows.Forms.ComboBox();
            this.lblInvocationCounter = new System.Windows.Forms.Label();
            this.lblEncryptionKey = new System.Windows.Forms.Label();
            this.lblAuthKey = new System.Windows.Forms.Label();
            this.tbInvocationCounter = new System.Windows.Forms.TextBox();
            this.tbAuthenticationKey = new System.Windows.Forms.TextBox();
            this.tbEncryptionKey = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.check_auto_hide = new System.Windows.Forms.CheckBox();
            this.timer_PassKey = new System.Windows.Forms.Timer(this.components);
            this.stStatus.SuspendLayout();
            this.IpConContextMenu.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlPicIcons.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flpParameterIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Parameterization)).BeginInit();
            this.flpBillingIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Billing)).BeginInit();
            this.flpLoadProfileIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_LoadProfile)).BeginInit();
            this.flpEventsIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Events)).BeginInit();
            this.flpInstantaneousIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Instantaneous)).BeginInit();
            this.flpSettingIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_settings)).BeginInit();
            this.flpDebugIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Debug)).BeginInit();
            this.flpAdminIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_admin)).BeginInit();
            this.flpHelpIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_AboutMe)).BeginInit();
            this.flpLogOffIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_logoff)).BeginInit();
            this.gp_IR_Port.SuspendLayout();
            this.pnlCommunication.SuspendLayout();
            this.tcSetting.SuspendLayout();
            this.tpConnection.SuspendLayout();
            this.gbHDLCAddress.SuspendLayout();
            this.gpClientConfig.SuspendLayout();
            this.gpPhysicalConnection.SuspendLayout();
            this.gp_WakeUp_Paras.SuspendLayout();
            this.gpMtrSerial.SuspendLayout();
            this.tpHLS.SuspendLayout();
            this.pnlAuthenticationKeys.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // stStatus
            // 
            this.stStatus.BackColor = System.Drawing.Color.Transparent;
            this.stStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.stStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stStatus_lblConnnStatus,
            this.stStatus_lblFirmwareVersion,
            this.stStatus_lblSerialNo,
            this.stStatus_stringEmpty,
            this.stStatusIOActivity,
            this.stlblStatus,
            this.tsProgressBar});
            this.stStatus.Location = new System.Drawing.Point(0, 710);
            this.stStatus.Name = "stStatus";
            this.stStatus.Size = new System.Drawing.Size(1356, 22);
            this.stStatus.TabIndex = 0;
            this.stStatus.Text = "Status Strip";
            // 
            // stStatus_lblConnnStatus
            // 
            this.stStatus_lblConnnStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stStatus_lblConnnStatus.Name = "stStatus_lblConnnStatus";
            this.stStatus_lblConnnStatus.Size = new System.Drawing.Size(72, 17);
            this.stStatus_lblConnnStatus.Text = "Connected";
            this.stStatus_lblConnnStatus.ToolTipText = "Application Connection Status";
            this.stStatus_lblConnnStatus.Visible = false;
            // 
            // stStatus_lblFirmwareVersion
            // 
            this.stStatus_lblFirmwareVersion.AutoSize = false;
            this.stStatus_lblFirmwareVersion.Name = "stStatus_lblFirmwareVersion";
            this.stStatus_lblFirmwareVersion.Size = new System.Drawing.Size(0, 17);
            this.stStatus_lblFirmwareVersion.Visible = false;
            // 
            // stStatus_lblSerialNo
            // 
            this.stStatus_lblSerialNo.Name = "stStatus_lblSerialNo";
            this.stStatus_lblSerialNo.Size = new System.Drawing.Size(0, 17);
            this.stStatus_lblSerialNo.Visible = false;
            // 
            // stStatus_stringEmpty
            // 
            this.stStatus_stringEmpty.Name = "stStatus_stringEmpty";
            this.stStatus_stringEmpty.Size = new System.Drawing.Size(1302, 17);
            this.stStatus_stringEmpty.Spring = true;
            // 
            // stStatusIOActivity
            // 
            this.stStatusIOActivity.Name = "stStatusIOActivity";
            this.stStatusIOActivity.Size = new System.Drawing.Size(0, 17);
            this.stStatusIOActivity.Visible = false;
            // 
            // stlblStatus
            // 
            this.stlblStatus.Name = "stlblStatus";
            this.stlblStatus.Size = new System.Drawing.Size(39, 17);
            this.stlblStatus.Text = "Status";
            // 
            // tsProgressBar
            // 
            this.tsProgressBar.AutoSize = false;
            this.tsProgressBar.Name = "tsProgressBar";
            this.tsProgressBar.Size = new System.Drawing.Size(190, 16);
            this.tsProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.tsProgressBar.Visible = false;
            // 
            // lblMainHeading
            // 
            this.lblMainHeading.AutoSize = true;
            this.lblMainHeading.Font = new System.Drawing.Font("Monotype Corsiva", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainHeading.Location = new System.Drawing.Point(381, 1);
            this.lblMainHeading.Name = "lblMainHeading";
            this.lblMainHeading.Size = new System.Drawing.Size(291, 36);
            this.lblMainHeading.TabIndex = 1;
            this.lblMainHeading.Text = "Smart Eye Control (SEC-7)";
            // 
            // IpConContextMenu
            // 
            this.IpConContextMenu.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.IpConContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GetMeterInfoMenuItem,
            this.readMeterInfoMenuItem,
            this.DropConnMenuItem,
            this.removeListMenuItem});
            this.IpConContextMenu.Name = "IpConContextMenu";
            this.IpConContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.IpConContextMenu.Size = new System.Drawing.Size(234, 92);
            this.IpConContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.IpConContextMenu_Opening);
            // 
            // GetMeterInfoMenuItem
            // 
            this.GetMeterInfoMenuItem.AutoToolTip = true;
            this.GetMeterInfoMenuItem.Name = "GetMeterInfoMenuItem";
            this.GetMeterInfoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.GetMeterInfoMenuItem.Size = new System.Drawing.Size(233, 22);
            this.GetMeterInfoMenuItem.Text = "Connection Info";
            this.GetMeterInfoMenuItem.ToolTipText = "Display connected meter infomation in details";
            // 
            // readMeterInfoMenuItem
            // 
            this.readMeterInfoMenuItem.Name = "readMeterInfoMenuItem";
            this.readMeterInfoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.readMeterInfoMenuItem.Size = new System.Drawing.Size(233, 22);
            this.readMeterInfoMenuItem.Text = "Read Meter Info";
            // 
            // DropConnMenuItem
            // 
            this.DropConnMenuItem.AutoToolTip = true;
            this.DropConnMenuItem.Name = "DropConnMenuItem";
            this.DropConnMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.DropConnMenuItem.Size = new System.Drawing.Size(233, 22);
            this.DropConnMenuItem.Text = "Disconnect Connection";
            this.DropConnMenuItem.ToolTipText = "Disconnect Physical Connection";
            // 
            // removeListMenuItem
            // 
            this.removeListMenuItem.Name = "removeListMenuItem";
            this.removeListMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.removeListMenuItem.Size = new System.Drawing.Size(233, 22);
            this.removeListMenuItem.Text = "Remove From List";
            // 
            // lblPortStatus
            // 
            this.lblPortStatus.AutoSize = true;
            this.lblPortStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPortStatus.Location = new System.Drawing.Point(90, 61);
            this.lblPortStatus.Name = "lblPortStatus";
            this.lblPortStatus.Size = new System.Drawing.Size(45, 13);
            this.lblPortStatus.TabIndex = 5;
            this.lblPortStatus.Text = "Closed";
            // 
            // lblcapStatus
            // 
            this.lblcapStatus.AutoSize = true;
            this.lblcapStatus.Location = new System.Drawing.Point(22, 61);
            this.lblcapStatus.Name = "lblcapStatus";
            this.lblcapStatus.Size = new System.Drawing.Size(62, 13);
            this.lblcapStatus.TabIndex = 5;
            this.lblcapStatus.Text = "Port Status:";
            // 
            // btnOpenClose
            // 
            this.btnOpenClose.Location = new System.Drawing.Point(94, 35);
            this.btnOpenClose.Name = "btnOpenClose";
            this.btnOpenClose.Size = new System.Drawing.Size(60, 23);
            this.btnOpenClose.TabIndex = 5;
            this.btnOpenClose.Text = "Open";
            this.btnOpenClose.UseVisualStyleBackColor = true;
            // 
            // comboPort
            // 
            this.comboPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPort.FormattingEnabled = true;
            this.comboPort.Location = new System.Drawing.Point(19, 37);
            this.comboPort.MaxDropDownItems = 5;
            this.comboPort.Name = "comboPort";
            this.comboPort.Size = new System.Drawing.Size(69, 21);
            this.comboPort.TabIndex = 4;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(19, 21);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(59, 13);
            this.lblPort.TabIndex = 4;
            this.lblPort.Text = "Select Port";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.configurationsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1356, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.menuToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // configurationsToolStripMenuItem
            // 
            this.configurationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.communicationConfigDialogToolStripMenuItem,
            this.applicationConfigToolStripMenuItem,
            this.databaseSettingsToolStripMenuItem});
            this.configurationsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.configurationsToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.configurationsToolStripMenuItem.Name = "configurationsToolStripMenuItem";
            this.configurationsToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.configurationsToolStripMenuItem.Text = "Configurations";
            // 
            // communicationConfigDialogToolStripMenuItem
            // 
            this.communicationConfigDialogToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.communicationConfigDialogToolStripMenuItem.Name = "communicationConfigDialogToolStripMenuItem";
            this.communicationConfigDialogToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.communicationConfigDialogToolStripMenuItem.Text = "Comm Config Dialog";
            this.communicationConfigDialogToolStripMenuItem.Click += new System.EventHandler(this.configurationToolStripMenuItem_Click);
            // 
            // applicationConfigToolStripMenuItem
            // 
            this.applicationConfigToolStripMenuItem.Name = "applicationConfigToolStripMenuItem";
            this.applicationConfigToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.applicationConfigToolStripMenuItem.Text = "Application &Config";
            this.applicationConfigToolStripMenuItem.Click += new System.EventHandler(this.applicationConfigToolStripMenuItem_Click);
            // 
            // databaseSettingsToolStripMenuItem
            // 
            this.databaseSettingsToolStripMenuItem.Name = "databaseSettingsToolStripMenuItem";
            this.databaseSettingsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.databaseSettingsToolStripMenuItem.Text = "Database Settings";
            this.databaseSettingsToolStripMenuItem.Click += new System.EventHandler(this.databaseSettingsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMeToolStripMenuItem});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutMeToolStripMenuItem
            // 
            this.aboutMeToolStripMenuItem.Name = "aboutMeToolStripMenuItem";
            this.aboutMeToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.aboutMeToolStripMenuItem.Text = "About Me";
            this.aboutMeToolStripMenuItem.Click += new System.EventHandler(this.aboutMeToolStripMenuItem_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.CadetBlue;
            this.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMain.Controls.Add(this.pnlPicIcons);
            this.pnlMain.Controls.Add(this.Panel_Welcome);
            this.pnlMain.Controls.Add(this.label4);
            this.pnlMain.Controls.Add(this.pnlsuperAdminPanel1);
            this.pnlMain.Controls.Add(this.pnlBilling1);
            this.pnlMain.Controls.Add(this.Panel_Instantaneous);
            this.pnlMain.Controls.Add(this.Panel_Events);
            this.pnlMain.Controls.Add(this.lblMainHeading);
            this.pnlMain.Controls.Add(this.Panel_Debugging);
            this.pnlMain.Controls.Add(this.pnlParameterization1);
            this.pnlMain.Controls.Add(this.pnlLoad_Profile);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1356, 686);
            this.pnlMain.TabIndex = 15;
            // 
            // pnlPicIcons
            // 
            this.pnlPicIcons.AutoScroll = true;
            this.pnlPicIcons.BackColor = System.Drawing.Color.Transparent;
            this.pnlPicIcons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPicIcons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPicIcons.Controls.Add(this.llblReloadRights);
            this.pnlPicIcons.Controls.Add(this.flowLayoutPanel3);
            this.pnlPicIcons.Controls.Add(this.label1);
            this.pnlPicIcons.Controls.Add(this.lbl_loginName);
            this.pnlPicIcons.Controls.Add(this.splitter1);
            this.pnlPicIcons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPicIcons.Location = new System.Drawing.Point(0, 0);
            this.pnlPicIcons.Name = "pnlPicIcons";
            this.pnlPicIcons.Size = new System.Drawing.Size(1356, 72);
            this.pnlPicIcons.TabIndex = 15;
            // 
            // llblReloadRights
            // 
            this.llblReloadRights.AutoSize = true;
            this.llblReloadRights.Location = new System.Drawing.Point(968, 26);
            this.llblReloadRights.Name = "llblReloadRights";
            this.llblReloadRights.Size = new System.Drawing.Size(29, 13);
            this.llblReloadRights.TabIndex = 9;
            this.llblReloadRights.TabStop = true;
            this.llblReloadRights.Text = "RLD";
            this.llblReloadRights.Visible = false;
            this.llblReloadRights.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblReloadRights_LinkClicked);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.flpParameterIcon);
            this.flowLayoutPanel3.Controls.Add(this.flpBillingIcon);
            this.flowLayoutPanel3.Controls.Add(this.flpLoadProfileIcon);
            this.flowLayoutPanel3.Controls.Add(this.flpEventsIcon);
            this.flowLayoutPanel3.Controls.Add(this.flpInstantaneousIcon);
            this.flowLayoutPanel3.Controls.Add(this.flpSettingIcon);
            this.flowLayoutPanel3.Controls.Add(this.flpDebugIcon);
            this.flowLayoutPanel3.Controls.Add(this.flpAdminIcon);
            this.flowLayoutPanel3.Controls.Add(this.flpHelpIcon);
            this.flowLayoutPanel3.Controls.Add(this.flpLogOffIcon);
            this.flowLayoutPanel3.Controls.Add(this.flowLayoutPanel1);
            this.flowLayoutPanel3.Controls.Add(this.pnlKeys);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(890, 72);
            this.flowLayoutPanel3.TabIndex = 8;
            // 
            // flpParameterIcon
            // 
            this.flpParameterIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flpParameterIcon.Controls.Add(this.pic_Parameterization);
            this.flpParameterIcon.Controls.Add(this.lbl_headparam);
            this.flpParameterIcon.Location = new System.Drawing.Point(0, 0);
            this.flpParameterIcon.Margin = new System.Windows.Forms.Padding(0);
            this.flpParameterIcon.Name = "flpParameterIcon";
            this.flpParameterIcon.Size = new System.Drawing.Size(75, 70);
            this.flpParameterIcon.TabIndex = 6;
            this.flpParameterIcon.Visible = false;
            // 
            // pic_Parameterization
            // 
            this.pic_Parameterization.BackColor = System.Drawing.Color.Transparent;
            this.pic_Parameterization.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_Parameterization.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_Parameterization.Image = ((System.Drawing.Image)(resources.GetObject("pic_Parameterization.Image")));
            this.pic_Parameterization.Location = new System.Drawing.Point(0, 0);
            this.pic_Parameterization.Margin = new System.Windows.Forms.Padding(0);
            this.pic_Parameterization.Name = "pic_Parameterization";
            this.pic_Parameterization.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.pic_Parameterization.Size = new System.Drawing.Size(75, 50);
            this.pic_Parameterization.TabIndex = 0;
            this.pic_Parameterization.TabStop = false;
            this.toolTip1.SetToolTip(this.pic_Parameterization, "Meter Parameterization");
            this.pic_Parameterization.Click += new System.EventHandler(this.pic_Parameterization_Click_2);
            this.pic_Parameterization.MouseEnter += new System.EventHandler(this.pic_Parameterization_MouseEnter);
            this.pic_Parameterization.MouseLeave += new System.EventHandler(this.pic_Parameterization_MouseLeave);
            // 
            // lbl_headparam
            // 
            this.lbl_headparam.BackColor = System.Drawing.Color.Transparent;
            this.lbl_headparam.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_headparam.ForeColor = System.Drawing.Color.White;
            this.lbl_headparam.Location = new System.Drawing.Point(0, 50);
            this.lbl_headparam.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_headparam.Name = "lbl_headparam";
            this.lbl_headparam.Size = new System.Drawing.Size(75, 22);
            this.lbl_headparam.TabIndex = 1;
            this.lbl_headparam.Text = "Parameters";
            this.lbl_headparam.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flpBillingIcon
            // 
            this.flpBillingIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flpBillingIcon.Controls.Add(this.pic_Billing);
            this.flpBillingIcon.Controls.Add(this.lbl_headBill);
            this.flpBillingIcon.Location = new System.Drawing.Point(75, 0);
            this.flpBillingIcon.Margin = new System.Windows.Forms.Padding(0);
            this.flpBillingIcon.Name = "flpBillingIcon";
            this.flpBillingIcon.Size = new System.Drawing.Size(75, 70);
            this.flpBillingIcon.TabIndex = 8;
            this.flpBillingIcon.Visible = false;
            // 
            // pic_Billing
            // 
            this.pic_Billing.BackColor = System.Drawing.Color.Transparent;
            this.pic_Billing.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_Billing.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_Billing.Image = ((System.Drawing.Image)(resources.GetObject("pic_Billing.Image")));
            this.pic_Billing.Location = new System.Drawing.Point(0, 0);
            this.pic_Billing.Margin = new System.Windows.Forms.Padding(0);
            this.pic_Billing.Name = "pic_Billing";
            this.pic_Billing.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.pic_Billing.Size = new System.Drawing.Size(75, 50);
            this.pic_Billing.TabIndex = 0;
            this.pic_Billing.TabStop = false;
            this.toolTip1.SetToolTip(this.pic_Billing, "Billing");
            this.pic_Billing.Click += new System.EventHandler(this.pic_Billing_Click_1);
            this.pic_Billing.MouseEnter += new System.EventHandler(this.pic_Billing_MouseEnter);
            this.pic_Billing.MouseLeave += new System.EventHandler(this.pic_Billing_MouseLeave);
            // 
            // lbl_headBill
            // 
            this.lbl_headBill.BackColor = System.Drawing.Color.Transparent;
            this.lbl_headBill.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_headBill.ForeColor = System.Drawing.Color.White;
            this.lbl_headBill.Location = new System.Drawing.Point(0, 50);
            this.lbl_headBill.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_headBill.Name = "lbl_headBill";
            this.lbl_headBill.Size = new System.Drawing.Size(75, 22);
            this.lbl_headBill.TabIndex = 1;
            this.lbl_headBill.Text = "Billing";
            this.lbl_headBill.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flpLoadProfileIcon
            // 
            this.flpLoadProfileIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flpLoadProfileIcon.Controls.Add(this.pic_LoadProfile);
            this.flpLoadProfileIcon.Controls.Add(this.lbl_headLp);
            this.flpLoadProfileIcon.Location = new System.Drawing.Point(150, 0);
            this.flpLoadProfileIcon.Margin = new System.Windows.Forms.Padding(0);
            this.flpLoadProfileIcon.Name = "flpLoadProfileIcon";
            this.flpLoadProfileIcon.Size = new System.Drawing.Size(75, 70);
            this.flpLoadProfileIcon.TabIndex = 8;
            this.flpLoadProfileIcon.Visible = false;
            // 
            // pic_LoadProfile
            // 
            this.pic_LoadProfile.BackColor = System.Drawing.Color.Transparent;
            this.pic_LoadProfile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_LoadProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_LoadProfile.Image = ((System.Drawing.Image)(resources.GetObject("pic_LoadProfile.Image")));
            this.pic_LoadProfile.Location = new System.Drawing.Point(0, 0);
            this.pic_LoadProfile.Margin = new System.Windows.Forms.Padding(0);
            this.pic_LoadProfile.Name = "pic_LoadProfile";
            this.pic_LoadProfile.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.pic_LoadProfile.Size = new System.Drawing.Size(75, 50);
            this.pic_LoadProfile.TabIndex = 0;
            this.pic_LoadProfile.TabStop = false;
            this.toolTip1.SetToolTip(this.pic_LoadProfile, "Load Profile");
            this.pic_LoadProfile.Click += new System.EventHandler(this.pic_LoadProfile_Click_1);
            this.pic_LoadProfile.MouseEnter += new System.EventHandler(this.pic_LoadProfile_MouseEnter);
            this.pic_LoadProfile.MouseLeave += new System.EventHandler(this.pic_LoadProfile_MouseLeave);
            // 
            // lbl_headLp
            // 
            this.lbl_headLp.BackColor = System.Drawing.Color.Transparent;
            this.lbl_headLp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_headLp.ForeColor = System.Drawing.Color.White;
            this.lbl_headLp.Location = new System.Drawing.Point(0, 50);
            this.lbl_headLp.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_headLp.Name = "lbl_headLp";
            this.lbl_headLp.Size = new System.Drawing.Size(75, 22);
            this.lbl_headLp.TabIndex = 1;
            this.lbl_headLp.Text = "Load Profile";
            this.lbl_headLp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flpEventsIcon
            // 
            this.flpEventsIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flpEventsIcon.Controls.Add(this.pic_Events);
            this.flpEventsIcon.Controls.Add(this.lbl_headevents);
            this.flpEventsIcon.Location = new System.Drawing.Point(225, 0);
            this.flpEventsIcon.Margin = new System.Windows.Forms.Padding(0);
            this.flpEventsIcon.Name = "flpEventsIcon";
            this.flpEventsIcon.Size = new System.Drawing.Size(75, 70);
            this.flpEventsIcon.TabIndex = 8;
            this.flpEventsIcon.Visible = false;
            // 
            // pic_Events
            // 
            this.pic_Events.BackColor = System.Drawing.Color.Transparent;
            this.pic_Events.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_Events.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_Events.Image = ((System.Drawing.Image)(resources.GetObject("pic_Events.Image")));
            this.pic_Events.Location = new System.Drawing.Point(0, 0);
            this.pic_Events.Margin = new System.Windows.Forms.Padding(0);
            this.pic_Events.Name = "pic_Events";
            this.pic_Events.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.pic_Events.Size = new System.Drawing.Size(75, 50);
            this.pic_Events.TabIndex = 0;
            this.pic_Events.TabStop = false;
            this.toolTip1.SetToolTip(this.pic_Events, "Events");
            this.pic_Events.Click += new System.EventHandler(this.pic_Events_Click_1);
            this.pic_Events.MouseEnter += new System.EventHandler(this.pic_Events_MouseEnter);
            this.pic_Events.MouseLeave += new System.EventHandler(this.pic_Events_MouseLeave);
            // 
            // lbl_headevents
            // 
            this.lbl_headevents.BackColor = System.Drawing.Color.Transparent;
            this.lbl_headevents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_headevents.ForeColor = System.Drawing.Color.White;
            this.lbl_headevents.Location = new System.Drawing.Point(0, 50);
            this.lbl_headevents.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_headevents.Name = "lbl_headevents";
            this.lbl_headevents.Size = new System.Drawing.Size(75, 22);
            this.lbl_headevents.TabIndex = 1;
            this.lbl_headevents.Text = "Events";
            this.lbl_headevents.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flpInstantaneousIcon
            // 
            this.flpInstantaneousIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flpInstantaneousIcon.Controls.Add(this.pic_Instantaneous);
            this.flpInstantaneousIcon.Controls.Add(this.lbl_headIns);
            this.flpInstantaneousIcon.Location = new System.Drawing.Point(300, 0);
            this.flpInstantaneousIcon.Margin = new System.Windows.Forms.Padding(0);
            this.flpInstantaneousIcon.Name = "flpInstantaneousIcon";
            this.flpInstantaneousIcon.Size = new System.Drawing.Size(75, 70);
            this.flpInstantaneousIcon.TabIndex = 9;
            this.flpInstantaneousIcon.Visible = false;
            // 
            // pic_Instantaneous
            // 
            this.pic_Instantaneous.BackColor = System.Drawing.Color.Transparent;
            this.pic_Instantaneous.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_Instantaneous.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_Instantaneous.Image = ((System.Drawing.Image)(resources.GetObject("pic_Instantaneous.Image")));
            this.pic_Instantaneous.Location = new System.Drawing.Point(0, 0);
            this.pic_Instantaneous.Margin = new System.Windows.Forms.Padding(0);
            this.pic_Instantaneous.Name = "pic_Instantaneous";
            this.pic_Instantaneous.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.pic_Instantaneous.Size = new System.Drawing.Size(75, 50);
            this.pic_Instantaneous.TabIndex = 0;
            this.pic_Instantaneous.TabStop = false;
            this.toolTip1.SetToolTip(this.pic_Instantaneous, "Instantaneous");
            this.pic_Instantaneous.Click += new System.EventHandler(this.pic_Instantaneous_Click_1);
            this.pic_Instantaneous.MouseEnter += new System.EventHandler(this.pic_Instantaneous_MouseEnter);
            this.pic_Instantaneous.MouseLeave += new System.EventHandler(this.pic_Instantaneous_MouseLeave);
            // 
            // lbl_headIns
            // 
            this.lbl_headIns.BackColor = System.Drawing.Color.Transparent;
            this.lbl_headIns.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_headIns.ForeColor = System.Drawing.Color.White;
            this.lbl_headIns.Location = new System.Drawing.Point(0, 50);
            this.lbl_headIns.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_headIns.Name = "lbl_headIns";
            this.lbl_headIns.Size = new System.Drawing.Size(75, 22);
            this.lbl_headIns.TabIndex = 1;
            this.lbl_headIns.Text = "Instantaneous";
            this.lbl_headIns.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flpSettingIcon
            // 
            this.flpSettingIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flpSettingIcon.Controls.Add(this.pic_settings);
            this.flpSettingIcon.Controls.Add(this.lbl_headSettings);
            this.flpSettingIcon.Location = new System.Drawing.Point(375, 0);
            this.flpSettingIcon.Margin = new System.Windows.Forms.Padding(0);
            this.flpSettingIcon.Name = "flpSettingIcon";
            this.flpSettingIcon.Size = new System.Drawing.Size(75, 70);
            this.flpSettingIcon.TabIndex = 10;
            this.flpSettingIcon.Visible = false;
            // 
            // pic_settings
            // 
            this.pic_settings.BackColor = System.Drawing.Color.Transparent;
            this.pic_settings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_settings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_settings.Image = ((System.Drawing.Image)(resources.GetObject("pic_settings.Image")));
            this.pic_settings.Location = new System.Drawing.Point(0, 0);
            this.pic_settings.Margin = new System.Windows.Forms.Padding(0);
            this.pic_settings.Name = "pic_settings";
            this.pic_settings.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.pic_settings.Size = new System.Drawing.Size(75, 50);
            this.pic_settings.TabIndex = 0;
            this.pic_settings.TabStop = false;
            this.toolTip1.SetToolTip(this.pic_settings, "Settings");
            this.pic_settings.Click += new System.EventHandler(this.pic_settings_Click_1);
            this.pic_settings.MouseEnter += new System.EventHandler(this.pic_settings_MouseEnter);
            this.pic_settings.MouseLeave += new System.EventHandler(this.pic_settings_MouseLeave);
            // 
            // lbl_headSettings
            // 
            this.lbl_headSettings.BackColor = System.Drawing.Color.Transparent;
            this.lbl_headSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_headSettings.ForeColor = System.Drawing.Color.White;
            this.lbl_headSettings.Location = new System.Drawing.Point(0, 50);
            this.lbl_headSettings.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_headSettings.Name = "lbl_headSettings";
            this.lbl_headSettings.Size = new System.Drawing.Size(75, 22);
            this.lbl_headSettings.TabIndex = 1;
            this.lbl_headSettings.Text = "Settings";
            this.lbl_headSettings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flpDebugIcon
            // 
            this.flpDebugIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flpDebugIcon.Controls.Add(this.pic_Debug);
            this.flpDebugIcon.Controls.Add(this.lbl_headdebug);
            this.flpDebugIcon.Location = new System.Drawing.Point(450, 0);
            this.flpDebugIcon.Margin = new System.Windows.Forms.Padding(0);
            this.flpDebugIcon.Name = "flpDebugIcon";
            this.flpDebugIcon.Size = new System.Drawing.Size(75, 70);
            this.flpDebugIcon.TabIndex = 11;
            this.flpDebugIcon.Visible = false;
            // 
            // pic_Debug
            // 
            this.pic_Debug.BackColor = System.Drawing.Color.Transparent;
            this.pic_Debug.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_Debug.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_Debug.Image = ((System.Drawing.Image)(resources.GetObject("pic_Debug.Image")));
            this.pic_Debug.Location = new System.Drawing.Point(0, 0);
            this.pic_Debug.Margin = new System.Windows.Forms.Padding(0);
            this.pic_Debug.Name = "pic_Debug";
            this.pic_Debug.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.pic_Debug.Size = new System.Drawing.Size(75, 50);
            this.pic_Debug.TabIndex = 0;
            this.pic_Debug.TabStop = false;
            this.toolTip1.SetToolTip(this.pic_Debug, "Debug");
            this.pic_Debug.Click += new System.EventHandler(this.pic_Debug_Click_1);
            this.pic_Debug.MouseEnter += new System.EventHandler(this.pic_Debug_MouseEnter);
            this.pic_Debug.MouseLeave += new System.EventHandler(this.pic_Debug_MouseLeave);
            // 
            // lbl_headdebug
            // 
            this.lbl_headdebug.BackColor = System.Drawing.Color.Transparent;
            this.lbl_headdebug.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_headdebug.ForeColor = System.Drawing.Color.White;
            this.lbl_headdebug.Location = new System.Drawing.Point(0, 50);
            this.lbl_headdebug.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_headdebug.Name = "lbl_headdebug";
            this.lbl_headdebug.Size = new System.Drawing.Size(75, 22);
            this.lbl_headdebug.TabIndex = 1;
            this.lbl_headdebug.Text = " Debug";
            this.lbl_headdebug.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flpAdminIcon
            // 
            this.flpAdminIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flpAdminIcon.Controls.Add(this.pic_admin);
            this.flpAdminIcon.Controls.Add(this.lbl_headAdmin);
            this.flpAdminIcon.Location = new System.Drawing.Point(525, 0);
            this.flpAdminIcon.Margin = new System.Windows.Forms.Padding(0);
            this.flpAdminIcon.Name = "flpAdminIcon";
            this.flpAdminIcon.Size = new System.Drawing.Size(75, 70);
            this.flpAdminIcon.TabIndex = 13;
            // 
            // pic_admin
            // 
            this.pic_admin.BackColor = System.Drawing.Color.Transparent;
            this.pic_admin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_admin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_admin.Image = ((System.Drawing.Image)(resources.GetObject("pic_admin.Image")));
            this.pic_admin.Location = new System.Drawing.Point(0, 0);
            this.pic_admin.Margin = new System.Windows.Forms.Padding(0);
            this.pic_admin.Name = "pic_admin";
            this.pic_admin.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.pic_admin.Size = new System.Drawing.Size(75, 50);
            this.pic_admin.TabIndex = 0;
            this.pic_admin.TabStop = false;
            this.pic_admin.Click += new System.EventHandler(this.AdminPanel_Click);
            this.pic_admin.MouseEnter += new System.EventHandler(this.pic_admin_MouseEnter);
            this.pic_admin.MouseLeave += new System.EventHandler(this.pic_admin_MouseLeave);
            // 
            // lbl_headAdmin
            // 
            this.lbl_headAdmin.BackColor = System.Drawing.Color.Transparent;
            this.lbl_headAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_headAdmin.ForeColor = System.Drawing.Color.White;
            this.lbl_headAdmin.Location = new System.Drawing.Point(0, 50);
            this.lbl_headAdmin.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_headAdmin.Name = "lbl_headAdmin";
            this.lbl_headAdmin.Size = new System.Drawing.Size(75, 22);
            this.lbl_headAdmin.TabIndex = 1;
            this.lbl_headAdmin.Text = "Admin";
            this.lbl_headAdmin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flpHelpIcon
            // 
            this.flpHelpIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flpHelpIcon.Controls.Add(this.pic_AboutMe);
            this.flpHelpIcon.Controls.Add(this.lbl_headhelp);
            this.flpHelpIcon.Location = new System.Drawing.Point(600, 0);
            this.flpHelpIcon.Margin = new System.Windows.Forms.Padding(0);
            this.flpHelpIcon.Name = "flpHelpIcon";
            this.flpHelpIcon.Size = new System.Drawing.Size(75, 70);
            this.flpHelpIcon.TabIndex = 12;
            // 
            // pic_AboutMe
            // 
            this.pic_AboutMe.BackColor = System.Drawing.Color.Transparent;
            this.pic_AboutMe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_AboutMe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_AboutMe.Image = ((System.Drawing.Image)(resources.GetObject("pic_AboutMe.Image")));
            this.pic_AboutMe.Location = new System.Drawing.Point(0, 0);
            this.pic_AboutMe.Margin = new System.Windows.Forms.Padding(0);
            this.pic_AboutMe.Name = "pic_AboutMe";
            this.pic_AboutMe.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.pic_AboutMe.Size = new System.Drawing.Size(75, 50);
            this.pic_AboutMe.TabIndex = 0;
            this.pic_AboutMe.TabStop = false;
            this.pic_AboutMe.Click += new System.EventHandler(this.pic_AboutMe_Click_1);
            this.pic_AboutMe.MouseEnter += new System.EventHandler(this.pic_AboutMe_MouseEnter);
            this.pic_AboutMe.MouseLeave += new System.EventHandler(this.pic_AboutMe_MouseLeave);
            // 
            // lbl_headhelp
            // 
            this.lbl_headhelp.BackColor = System.Drawing.Color.Transparent;
            this.lbl_headhelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_headhelp.ForeColor = System.Drawing.Color.White;
            this.lbl_headhelp.Location = new System.Drawing.Point(0, 50);
            this.lbl_headhelp.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_headhelp.Name = "lbl_headhelp";
            this.lbl_headhelp.Size = new System.Drawing.Size(75, 22);
            this.lbl_headhelp.TabIndex = 1;
            this.lbl_headhelp.Text = "  Help";
            this.lbl_headhelp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flpLogOffIcon
            // 
            this.flpLogOffIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flpLogOffIcon.Controls.Add(this.pic_logoff);
            this.flpLogOffIcon.Controls.Add(this.lbl_headLogOff);
            this.flpLogOffIcon.Location = new System.Drawing.Point(675, 0);
            this.flpLogOffIcon.Margin = new System.Windows.Forms.Padding(0);
            this.flpLogOffIcon.Name = "flpLogOffIcon";
            this.flpLogOffIcon.Size = new System.Drawing.Size(75, 70);
            this.flpLogOffIcon.TabIndex = 13;
            // 
            // pic_logoff
            // 
            this.pic_logoff.BackColor = System.Drawing.Color.Transparent;
            this.pic_logoff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_logoff.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_logoff.Image = ((System.Drawing.Image)(resources.GetObject("pic_logoff.Image")));
            this.pic_logoff.Location = new System.Drawing.Point(0, 0);
            this.pic_logoff.Margin = new System.Windows.Forms.Padding(0);
            this.pic_logoff.Name = "pic_logoff";
            this.pic_logoff.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.pic_logoff.Size = new System.Drawing.Size(75, 50);
            this.pic_logoff.TabIndex = 3;
            this.pic_logoff.TabStop = false;
            this.pic_logoff.Click += new System.EventHandler(this.UserLogin_Click);
            this.pic_logoff.MouseEnter += new System.EventHandler(this.pic_logoff_MouseEnter);
            this.pic_logoff.MouseLeave += new System.EventHandler(this.pic_logoff_MouseLeave);
            // 
            // lbl_headLogOff
            // 
            this.lbl_headLogOff.BackColor = System.Drawing.Color.Transparent;
            this.lbl_headLogOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_headLogOff.ForeColor = System.Drawing.Color.White;
            this.lbl_headLogOff.Location = new System.Drawing.Point(0, 50);
            this.lbl_headLogOff.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_headLogOff.Name = "lbl_headLogOff";
            this.lbl_headLogOff.Size = new System.Drawing.Size(75, 20);
            this.lbl_headLogOff.TabIndex = 1;
            this.lbl_headLogOff.Text = "Log Off";
            this.lbl_headLogOff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 73);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 100);
            this.flowLayoutPanel1.TabIndex = 14;
            // 
            // pnlKeys
            // 
            this.pnlKeys.Location = new System.Drawing.Point(209, 73);
            this.pnlKeys.Name = "pnlKeys";
            this.pnlKeys.Size = new System.Drawing.Size(211, 144);
            this.pnlKeys.TabIndex = 31;
            this.pnlKeys.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1017, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "User Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_loginName
            // 
            this.lbl_loginName.BackColor = System.Drawing.Color.Transparent;
            this.lbl_loginName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_loginName.ForeColor = System.Drawing.Color.White;
            this.lbl_loginName.Location = new System.Drawing.Point(1017, 38);
            this.lbl_loginName.Name = "lbl_loginName";
            this.lbl_loginName.Size = new System.Drawing.Size(124, 13);
            this.lbl_loginName.TabIndex = 4;
            this.lbl_loginName.Text = "Mr. User";
            this.lbl_loginName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 72);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // Panel_Welcome
            // 
            this.Panel_Welcome.AutoScroll = true;
            this.Panel_Welcome.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Welcome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel_Welcome.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_Welcome.Location = new System.Drawing.Point(0, 72);
            this.Panel_Welcome.Name = "Panel_Welcome";
            this.Panel_Welcome.Size = new System.Drawing.Size(1138, 614);
            this.Panel_Welcome.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(910, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Authentication Key";
            // 
            // pnlsuperAdminPanel1
            // 
            this.pnlsuperAdminPanel1.AutoSize = true;
            this.pnlsuperAdminPanel1.BackColor = System.Drawing.Color.Transparent;
            this.pnlsuperAdminPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlsuperAdminPanel1.Location = new System.Drawing.Point(0, 72);
            this.pnlsuperAdminPanel1.Name = "pnlsuperAdminPanel1";
            this.pnlsuperAdminPanel1.Size = new System.Drawing.Size(1253, 626);
            this.pnlsuperAdminPanel1.TabIndex = 16;
            // 
            // pnlBilling1
            // 
            this.pnlBilling1.Application_Controller = null;
            this.pnlBilling1.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.pnlBilling1.BackColor = System.Drawing.Color.Transparent;
            this.pnlBilling1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlBilling1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBilling1.Location = new System.Drawing.Point(0, 72);
            this.pnlBilling1.MaxCounter = -1;
            this.pnlBilling1.Name = "pnlBilling1";
            this.pnlBilling1.Size = new System.Drawing.Size(1266, 650);
            this.pnlBilling1.TabIndex = 13;
            // 
            // Panel_Instantaneous
            // 
            this.Panel_Instantaneous.Application_Controller = null;
            this.Panel_Instantaneous.AutoScroll = true;
            this.Panel_Instantaneous.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Instantaneous.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel_Instantaneous.Location = new System.Drawing.Point(0, 72);
            this.Panel_Instantaneous.Name = "Panel_Instantaneous";
            this.Panel_Instantaneous.Size = new System.Drawing.Size(1291, 650);
            this.Panel_Instantaneous.TabIndex = 12;
            // 
            // Panel_Events
            // 
            this.Panel_Events.Application_Controller = null;
            this.Panel_Events.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Events.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel_Events.Location = new System.Drawing.Point(0, 72);
            this.Panel_Events.Name = "Panel_Events";
            this.Panel_Events.Size = new System.Drawing.Size(1305, 650);
            this.Panel_Events.TabIndex = 14;
            // 
            // Panel_Debugging
            // 
            this.Panel_Debugging.Application_Controller = null;
            this.Panel_Debugging.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Debugging.IsMemoryReadCompleted = false;
            this.Panel_Debugging.Location = new System.Drawing.Point(0, 72);
            this.Panel_Debugging.Name = "Panel_Debugging";
            this.Panel_Debugging.Size = new System.Drawing.Size(1350, 650);
            this.Panel_Debugging.TabIndex = 11;
            // 
            // pnlParameterization1
            // 
            this.pnlParameterization1.Application_Controller = null;
            this.pnlParameterization1.BackColor = System.Drawing.Color.Transparent;
            this.pnlParameterization1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlParameterization1.Location = new System.Drawing.Point(0, 72);
            this.pnlParameterization1.Modem_Warnings_disable = true;
            this.pnlParameterization1.Name = "pnlParameterization1";
            this.pnlParameterization1.Size = new System.Drawing.Size(1350, 650);
            this.pnlParameterization1.TabIndex = 13;
            // 
            // pnlLoad_Profile
            // 
            this.pnlLoad_Profile.Application_Controller = null;
            this.pnlLoad_Profile.BackColor = System.Drawing.Color.Transparent;
            this.pnlLoad_Profile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlLoad_Profile.IsDateTimeWise = false;
            this.pnlLoad_Profile.List_AvailableStatausWord = null;
            this.pnlLoad_Profile.Location = new System.Drawing.Point(0, 72);
            this.pnlLoad_Profile.LP_Scheme = SharedCode.Comm.DataContainer.LoadProfileScheme.None;
            this.pnlLoad_Profile.MaxCounter = -1;
            this.pnlLoad_Profile.Name = "pnlLoad_Profile";
            this.pnlLoad_Profile.Size = new System.Drawing.Size(1350, 650);
            this.pnlLoad_Profile.TabIndex = 14;
            // 
            // gp_IR_Port
            // 
            this.gp_IR_Port.Controls.Add(this.lblRefreshPorts);
            this.gp_IR_Port.Controls.Add(this.cmbIRPorts);
            this.gp_IR_Port.Controls.Add(this.lblSelPort);
            this.gp_IR_Port.ForeColor = System.Drawing.Color.Black;
            this.gp_IR_Port.Location = new System.Drawing.Point(6, 45);
            this.gp_IR_Port.Name = "gp_IR_Port";
            this.gp_IR_Port.Size = new System.Drawing.Size(185, 46);
            this.gp_IR_Port.TabIndex = 4;
            this.gp_IR_Port.TabStop = false;
            this.gp_IR_Port.Text = "IR Port";
            // 
            // lblRefreshPorts
            // 
            this.lblRefreshPorts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblRefreshPorts.Location = new System.Drawing.Point(159, 16);
            this.lblRefreshPorts.Name = "lblRefreshPorts";
            this.lblRefreshPorts.Size = new System.Drawing.Size(14, 21);
            this.lblRefreshPorts.TabIndex = 8;
            this.lblRefreshPorts.Text = "...";
            this.toolIPConn.SetToolTip(this.lblRefreshPorts, "Click to Refresh COM Ports");
            this.lblRefreshPorts.Click += new System.EventHandler(this.lblRefreshPorts_Click);
            // 
            // cmbIRPorts
            // 
            this.cmbIRPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIRPorts.FormattingEnabled = true;
            this.cmbIRPorts.Location = new System.Drawing.Point(84, 16);
            this.cmbIRPorts.MaxDropDownItems = 5;
            this.cmbIRPorts.Name = "cmbIRPorts";
            this.cmbIRPorts.Size = new System.Drawing.Size(69, 21);
            this.cmbIRPorts.TabIndex = 4;
            this.cmbIRPorts.SelectedIndexChanged += new System.EventHandler(this.cmbIRPorts_SelectedIndexChanged);
            // 
            // lblSelPort
            // 
            this.lblSelPort.AutoSize = true;
            this.lblSelPort.Location = new System.Drawing.Point(20, 19);
            this.lblSelPort.Name = "lblSelPort";
            this.lblSelPort.Size = new System.Drawing.Size(53, 13);
            this.lblSelPort.TabIndex = 4;
            this.lblSelPort.Text = "COM Port";
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 750;
            this.toolTip1.BackColor = System.Drawing.Color.Transparent;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(15, 31);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(133, 21);
            this.comboBox1.Sorted = true;
            this.comboBox1.TabIndex = 5;
            // 
            // cmb
            // 
            this.cmb.FormattingEnabled = true;
            this.cmb.Location = new System.Drawing.Point(15, 31);
            this.cmb.Name = "cmb";
            this.cmb.Size = new System.Drawing.Size(133, 21);
            this.cmb.Sorted = true;
            this.cmb.TabIndex = 5;
            // 
            // toolIPConn
            // 
            this.toolIPConn.AutoPopDelay = 5000;
            this.toolIPConn.InitialDelay = 300;
            this.toolIPConn.ReshowDelay = 100;
            this.toolIPConn.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolIPConn.ToolTipTitle = "Connection Info";
            // 
            // cmbMeterSerial
            // 
            this.cmbMeterSerial.ContextMenuStrip = this.IpConContextMenu;
            this.cmbMeterSerial.FormattingEnabled = true;
            this.cmbMeterSerial.Location = new System.Drawing.Point(6, 20);
            this.cmbMeterSerial.Name = "cmbMeterSerial";
            this.cmbMeterSerial.Size = new System.Drawing.Size(176, 95);
            this.cmbMeterSerial.TabIndex = 16;
            this.toolIPConn.SetToolTip(this.cmbMeterSerial, "Displays selected meters connected over the IP Link(Meter Serial Number/IP)");
            this.cmbMeterSerial.SelectedIndexChanged += new System.EventHandler(this.cmbMeterSerial_SelectedIndexChanged);
            this.cmbMeterSerial.MouseEnter += new System.EventHandler(this.cmbMeterSerial_MouseEnter);
            // 
            // timer_Connect
            // 
            this.timer_Connect.Interval = 1000;
            this.timer_Connect.Tick += new System.EventHandler(this.timer_Connect_Tick);
            // 
            // pnlCommunication
            // 
            this.pnlCommunication.BackColor = System.Drawing.Color.CadetBlue;
            this.pnlCommunication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlCommunication.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlCommunication.ContextMenuStrip = this.IpConContextMenu;
            this.pnlCommunication.Controls.Add(this.tcSetting);
            this.pnlCommunication.Controls.Add(this.pictureBox1);
            this.pnlCommunication.Controls.Add(this.check_auto_hide);
            this.pnlCommunication.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlCommunication.Location = new System.Drawing.Point(1136, 24);
            this.pnlCommunication.Name = "pnlCommunication";
            this.pnlCommunication.Size = new System.Drawing.Size(220, 686);
            this.pnlCommunication.TabIndex = 8;
            this.pnlCommunication.MouseEnter += new System.EventHandler(this.pnlCommunication_MouseEnter);
            this.pnlCommunication.MouseLeave += new System.EventHandler(this.pnlCommunication_MouseLeave_1);
            // 
            // tcSetting
            // 
            this.tcSetting.Controls.Add(this.tpConnection);
            this.tcSetting.Controls.Add(this.tpHLS);
            this.tcSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSetting.Location = new System.Drawing.Point(0, 0);
            this.tcSetting.Name = "tcSetting";
            this.tcSetting.SelectedIndex = 0;
            this.tcSetting.Size = new System.Drawing.Size(216, 630);
            this.tcSetting.TabIndex = 25;
            // 
            // tpConnection
            // 
            this.tpConnection.BackColor = System.Drawing.Color.CadetBlue;
            this.tpConnection.Controls.Add(this.gbHDLCAddress);
            this.tpConnection.Controls.Add(this.gpClientConfig);
            this.tpConnection.Controls.Add(this.gpPhysicalConnection);
            this.tpConnection.Controls.Add(this.btnConnectApplication);
            this.tpConnection.Controls.Add(this.gpMtrSerial);
            this.tpConnection.Controls.Add(this.lnk_Disconnect_forcedly);
            this.tpConnection.Location = new System.Drawing.Point(4, 22);
            this.tpConnection.Name = "tpConnection";
            this.tpConnection.Padding = new System.Windows.Forms.Padding(3);
            this.tpConnection.Size = new System.Drawing.Size(208, 604);
            this.tpConnection.TabIndex = 0;
            this.tpConnection.Text = "Connection";
            // 
            // gbHDLCAddress
            // 
            this.gbHDLCAddress.Controls.Add(this.txtHDLCAddress);
            this.gbHDLCAddress.Location = new System.Drawing.Point(6, 341);
            this.gbHDLCAddress.Name = "gbHDLCAddress";
            this.gbHDLCAddress.Size = new System.Drawing.Size(192, 39);
            this.gbHDLCAddress.TabIndex = 19;
            this.gbHDLCAddress.TabStop = false;
            this.gbHDLCAddress.Text = "HDLC Address";
            this.gbHDLCAddress.Visible = false;
            // 
            // txtHDLCAddress
            // 
            this.txtHDLCAddress.Location = new System.Drawing.Point(20, 14);
            this.txtHDLCAddress.Name = "txtHDLCAddress";
            this.txtHDLCAddress.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtHDLCAddress.Size = new System.Drawing.Size(153, 20);
            this.txtHDLCAddress.TabIndex = 0;
            // 
            // gpClientConfig
            // 
            this.gpClientConfig.BackColor = System.Drawing.Color.Transparent;
            this.gpClientConfig.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gpClientConfig.Controls.Add(this.label2);
            this.gpClientConfig.Controls.Add(this.btn_DefaultPwd);
            this.gpClientConfig.Controls.Add(this.txtAssociationPaswd);
            this.gpClientConfig.Controls.Add(this.cmbManufacturers);
            this.gpClientConfig.Controls.Add(this.lblManufacturer);
            this.gpClientConfig.Controls.Add(this.cmbAssociations);
            this.gpClientConfig.Controls.Add(this.lblAssociation);
            this.gpClientConfig.Controls.Add(this.lblDevices);
            this.gpClientConfig.Controls.Add(this.cmbDevices);
            this.gpClientConfig.ForeColor = System.Drawing.Color.Black;
            this.gpClientConfig.Location = new System.Drawing.Point(3, 3);
            this.gpClientConfig.Name = "gpClientConfig";
            this.gpClientConfig.Size = new System.Drawing.Size(195, 174);
            this.gpClientConfig.TabIndex = 11;
            this.gpClientConfig.TabStop = false;
            this.gpClientConfig.Text = "Meter User Info";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Password";
            // 
            // btn_DefaultPwd
            // 
            this.btn_DefaultPwd.ForeColor = System.Drawing.Color.Black;
            this.btn_DefaultPwd.Location = new System.Drawing.Point(135, 147);
            this.btn_DefaultPwd.Name = "btn_DefaultPwd";
            this.btn_DefaultPwd.Size = new System.Drawing.Size(51, 21);
            this.btn_DefaultPwd.TabIndex = 19;
            this.btn_DefaultPwd.Text = "Default Password";
            this.btn_DefaultPwd.UseVisualStyleBackColor = true;
            this.btn_DefaultPwd.Click += new System.EventHandler(this.btn_DefaultPwd_Click);
            // 
            // txtAssociationPaswd
            // 
            this.txtAssociationPaswd.Location = new System.Drawing.Point(17, 147);
            this.txtAssociationPaswd.Name = "txtAssociationPaswd";
            this.txtAssociationPaswd.Size = new System.Drawing.Size(104, 20);
            this.txtAssociationPaswd.TabIndex = 12;
            this.txtAssociationPaswd.Text = "microtek";
            this.txtAssociationPaswd.UseSystemPasswordChar = true;
            this.txtAssociationPaswd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAssociationPaswd_KeyPress);
            // 
            // cmbManufacturers
            // 
            this.cmbManufacturers.DisplayMember = "Manufecturer_Name";
            this.cmbManufacturers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbManufacturers.FormattingEnabled = true;
            this.cmbManufacturers.Location = new System.Drawing.Point(19, 31);
            this.cmbManufacturers.Name = "cmbManufacturers";
            this.cmbManufacturers.Size = new System.Drawing.Size(165, 21);
            this.cmbManufacturers.TabIndex = 18;
            this.cmbManufacturers.ValueMember = "id";
            this.cmbManufacturers.SelectedIndexChanged += new System.EventHandler(this.cmbManufacturer_SelectedIndexChanged);
            // 
            // lblManufacturer
            // 
            this.lblManufacturer.AutoSize = true;
            this.lblManufacturer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblManufacturer.Location = new System.Drawing.Point(16, 15);
            this.lblManufacturer.Name = "lblManufacturer";
            this.lblManufacturer.Size = new System.Drawing.Size(70, 13);
            this.lblManufacturer.TabIndex = 17;
            this.lblManufacturer.Text = "Manufecturer";
            // 
            // cmbAssociations
            // 
            this.cmbAssociations.DisplayMember = "Association_Name";
            this.cmbAssociations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAssociations.FormattingEnabled = true;
            this.cmbAssociations.Location = new System.Drawing.Point(19, 109);
            this.cmbAssociations.Name = "cmbAssociations";
            this.cmbAssociations.Size = new System.Drawing.Size(167, 21);
            this.cmbAssociations.TabIndex = 16;
            this.cmbAssociations.ValueMember = "id";
            this.cmbAssociations.SelectedIndexChanged += new System.EventHandler(this.cmbAssociations_SelectedIndexChanged);
            // 
            // lblAssociation
            // 
            this.lblAssociation.AutoSize = true;
            this.lblAssociation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssociation.Location = new System.Drawing.Point(16, 93);
            this.lblAssociation.Name = "lblAssociation";
            this.lblAssociation.Size = new System.Drawing.Size(75, 13);
            this.lblAssociation.TabIndex = 15;
            this.lblAssociation.Text = "Authentication";
            // 
            // lblDevices
            // 
            this.lblDevices.AutoSize = true;
            this.lblDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDevices.Location = new System.Drawing.Point(16, 52);
            this.lblDevices.Name = "lblDevices";
            this.lblDevices.Size = new System.Drawing.Size(41, 13);
            this.lblDevices.TabIndex = 1;
            this.lblDevices.Text = "Device";
            // 
            // cmbDevices
            // 
            this.cmbDevices.DisplayMember = "Device_Name";
            this.cmbDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDevices.FormattingEnabled = true;
            this.cmbDevices.Location = new System.Drawing.Point(19, 68);
            this.cmbDevices.Name = "cmbDevices";
            this.cmbDevices.Size = new System.Drawing.Size(167, 21);
            this.cmbDevices.TabIndex = 0;
            this.cmbDevices.ValueMember = "id";
            this.cmbDevices.SelectedIndexChanged += new System.EventHandler(this.cmbDevices_SelectedIndexChanged);
            // 
            // gpPhysicalConnection
            // 
            this.gpPhysicalConnection.BackColor = System.Drawing.Color.Transparent;
            this.gpPhysicalConnection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gpPhysicalConnection.Controls.Add(this.gp_IR_Port);
            this.gpPhysicalConnection.Controls.Add(this.gp_WakeUp_Paras);
            this.gpPhysicalConnection.Controls.Add(this.cmbIOConnections);
            this.gpPhysicalConnection.ForeColor = System.Drawing.Color.Black;
            this.gpPhysicalConnection.Location = new System.Drawing.Point(6, 183);
            this.gpPhysicalConnection.Name = "gpPhysicalConnection";
            this.gpPhysicalConnection.Size = new System.Drawing.Size(195, 99);
            this.gpPhysicalConnection.TabIndex = 14;
            this.gpPhysicalConnection.TabStop = false;
            this.gpPhysicalConnection.Text = "IO Connections";
            // 
            // gp_WakeUp_Paras
            // 
            this.gp_WakeUp_Paras.Controls.Add(this.lnkbtnRefreshServer);
            this.gp_WakeUp_Paras.Controls.Add(this.lnkbtnConfigDialog);
            this.gp_WakeUp_Paras.ForeColor = System.Drawing.Color.Black;
            this.gp_WakeUp_Paras.Location = new System.Drawing.Point(6, 52);
            this.gp_WakeUp_Paras.Name = "gp_WakeUp_Paras";
            this.gp_WakeUp_Paras.Size = new System.Drawing.Size(183, 40);
            this.gp_WakeUp_Paras.TabIndex = 5;
            this.gp_WakeUp_Paras.TabStop = false;
            this.gp_WakeUp_Paras.Text = "IP Params";
            // 
            // lnkbtnRefreshServer
            // 
            this.lnkbtnRefreshServer.AutoSize = true;
            this.lnkbtnRefreshServer.LinkColor = System.Drawing.Color.Blue;
            this.lnkbtnRefreshServer.Location = new System.Drawing.Point(77, 16);
            this.lnkbtnRefreshServer.Name = "lnkbtnRefreshServer";
            this.lnkbtnRefreshServer.Size = new System.Drawing.Size(105, 13);
            this.lnkbtnRefreshServer.TabIndex = 3;
            this.lnkbtnRefreshServer.TabStop = true;
            this.lnkbtnRefreshServer.Text = "Restart TCP Listener";
            this.lnkbtnRefreshServer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkbtnRefreshServer_LinkClicked);
            this.lnkbtnRefreshServer.Click += new System.EventHandler(this.lnkbtnRefreshServer_Click);
            // 
            // lnkbtnConfigDialog
            // 
            this.lnkbtnConfigDialog.AutoSize = true;
            this.lnkbtnConfigDialog.LinkColor = System.Drawing.Color.Blue;
            this.lnkbtnConfigDialog.Location = new System.Drawing.Point(3, 16);
            this.lnkbtnConfigDialog.Name = "lnkbtnConfigDialog";
            this.lnkbtnConfigDialog.Size = new System.Drawing.Size(64, 13);
            this.lnkbtnConfigDialog.TabIndex = 2;
            this.lnkbtnConfigDialog.TabStop = true;
            this.lnkbtnConfigDialog.Text = "Edit Options";
            this.lnkbtnConfigDialog.Click += new System.EventHandler(this.configurationToolStripMenuItem_Click);
            // 
            // cmbIOConnections
            // 
            this.cmbIOConnections.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIOConnections.FormattingEnabled = true;
            this.cmbIOConnections.Location = new System.Drawing.Point(6, 18);
            this.cmbIOConnections.Name = "cmbIOConnections";
            this.cmbIOConnections.Size = new System.Drawing.Size(182, 21);
            this.cmbIOConnections.TabIndex = 0;
            this.cmbIOConnections.SelectedIndexChanged += new System.EventHandler(this.cmbIOConnections_SelectedIndexChanged);
            // 
            // btnConnectApplication
            // 
            this.btnConnectApplication.Location = new System.Drawing.Point(22, 289);
            this.btnConnectApplication.Name = "btnConnectApplication";
            this.btnConnectApplication.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnConnectApplication.Size = new System.Drawing.Size(165, 33);
            this.btnConnectApplication.TabIndex = 6;
            this.btnConnectApplication.Values.Image = ((System.Drawing.Image)(resources.GetObject("btnConnectApplication.Values.Image")));
            this.btnConnectApplication.Values.Text = "Connect";
            this.btnConnectApplication.Click += new System.EventHandler(this.btnConnectApplication_Click);
            // 
            // gpMtrSerial
            // 
            this.gpMtrSerial.BackColor = System.Drawing.Color.Transparent;
            this.gpMtrSerial.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gpMtrSerial.Controls.Add(this.lnkHeartBeatList);
            this.gpMtrSerial.Controls.Add(this.lnkTCPStatus);
            this.gpMtrSerial.Controls.Add(this.cmbMeterSerial);
            this.gpMtrSerial.ForeColor = System.Drawing.Color.Black;
            this.gpMtrSerial.Location = new System.Drawing.Point(7, 386);
            this.gpMtrSerial.Name = "gpMtrSerial";
            this.gpMtrSerial.Size = new System.Drawing.Size(195, 137);
            this.gpMtrSerial.TabIndex = 17;
            this.gpMtrSerial.TabStop = false;
            this.gpMtrSerial.Text = "Favourite Meters List";
            // 
            // lnkHeartBeatList
            // 
            this.lnkHeartBeatList.AutoSize = true;
            this.lnkHeartBeatList.ForeColor = System.Drawing.Color.Blue;
            this.lnkHeartBeatList.LinkColor = System.Drawing.Color.Blue;
            this.lnkHeartBeatList.Location = new System.Drawing.Point(3, 118);
            this.lnkHeartBeatList.Name = "lnkHeartBeatList";
            this.lnkHeartBeatList.Size = new System.Drawing.Size(85, 13);
            this.lnkHeartBeatList.TabIndex = 18;
            this.lnkHeartBeatList.TabStop = true;
            this.lnkHeartBeatList.Text = "Connections List";
            this.lnkHeartBeatList.Click += new System.EventHandler(this.lnkHeartBeatList_Click);
            // 
            // lnkTCPStatus
            // 
            this.lnkTCPStatus.AutoSize = true;
            this.lnkTCPStatus.ForeColor = System.Drawing.Color.Blue;
            this.lnkTCPStatus.LinkColor = System.Drawing.Color.Blue;
            this.lnkTCPStatus.Location = new System.Drawing.Point(101, 118);
            this.lnkTCPStatus.Name = "lnkTCPStatus";
            this.lnkTCPStatus.Size = new System.Drawing.Size(94, 13);
            this.lnkTCPStatus.TabIndex = 17;
            this.lnkTCPStatus.TabStop = true;
            this.lnkTCPStatus.Text = "Connection Status";
            this.lnkTCPStatus.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTCPStatus_LinkClicked);
            // 
            // lnk_Disconnect_forcedly
            // 
            this.lnk_Disconnect_forcedly.AutoSize = true;
            this.lnk_Disconnect_forcedly.BackColor = System.Drawing.Color.Transparent;
            this.lnk_Disconnect_forcedly.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnk_Disconnect_forcedly.ForeColor = System.Drawing.Color.Blue;
            this.lnk_Disconnect_forcedly.LinkColor = System.Drawing.Color.Blue;
            this.lnk_Disconnect_forcedly.Location = new System.Drawing.Point(38, 325);
            this.lnk_Disconnect_forcedly.Name = "lnk_Disconnect_forcedly";
            this.lnk_Disconnect_forcedly.Size = new System.Drawing.Size(130, 13);
            this.lnk_Disconnect_forcedly.TabIndex = 15;
            this.lnk_Disconnect_forcedly.TabStop = true;
            this.lnk_Disconnect_forcedly.Text = "Disconnect Forcefully";
            this.lnk_Disconnect_forcedly.Click += new System.EventHandler(this.btnDisconnectApplication_Click);
            // 
            // tpHLS
            // 
            this.tpHLS.BackColor = System.Drawing.Color.CadetBlue;
            this.tpHLS.Controls.Add(this.pnlAuthenticationKeys);
            this.tpHLS.Location = new System.Drawing.Point(4, 22);
            this.tpHLS.Name = "tpHLS";
            this.tpHLS.Padding = new System.Windows.Forms.Padding(3);
            this.tpHLS.Size = new System.Drawing.Size(208, 604);
            this.tpHLS.TabIndex = 1;
            this.tpHLS.Text = "HLS";
            // 
            // pnlAuthenticationKeys
            // 
            this.pnlAuthenticationKeys.Controls.Add(this.cmbSecurity);
            this.pnlAuthenticationKeys.Controls.Add(this.lblInvocationCounter);
            this.pnlAuthenticationKeys.Controls.Add(this.lblEncryptionKey);
            this.pnlAuthenticationKeys.Controls.Add(this.lblAuthKey);
            this.pnlAuthenticationKeys.Controls.Add(this.tbInvocationCounter);
            this.pnlAuthenticationKeys.Controls.Add(this.tbAuthenticationKey);
            this.pnlAuthenticationKeys.Controls.Add(this.tbEncryptionKey);
            this.pnlAuthenticationKeys.Location = new System.Drawing.Point(6, 16);
            this.pnlAuthenticationKeys.Name = "pnlAuthenticationKeys";
            this.pnlAuthenticationKeys.Size = new System.Drawing.Size(189, 207);
            this.pnlAuthenticationKeys.TabIndex = 29;
            this.pnlAuthenticationKeys.Visible = false;
            // 
            // cmbSecurity
            // 
            this.cmbSecurity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSecurity.FormattingEnabled = true;
            this.cmbSecurity.Location = new System.Drawing.Point(8, 19);
            this.cmbSecurity.Name = "cmbSecurity";
            this.cmbSecurity.Size = new System.Drawing.Size(177, 21);
            this.cmbSecurity.TabIndex = 28;
            this.cmbSecurity.SelectedIndexChanged += new System.EventHandler(this.cmbSecurity_SelectedIndexChanged);
            // 
            // lblInvocationCounter
            // 
            this.lblInvocationCounter.AutoSize = true;
            this.lblInvocationCounter.Location = new System.Drawing.Point(6, 155);
            this.lblInvocationCounter.Name = "lblInvocationCounter";
            this.lblInvocationCounter.Size = new System.Drawing.Size(97, 13);
            this.lblInvocationCounter.TabIndex = 25;
            this.lblInvocationCounter.Text = "Invocation Counter";
            // 
            // lblEncryptionKey
            // 
            this.lblEncryptionKey.AutoSize = true;
            this.lblEncryptionKey.Location = new System.Drawing.Point(6, 109);
            this.lblEncryptionKey.Name = "lblEncryptionKey";
            this.lblEncryptionKey.Size = new System.Drawing.Size(78, 13);
            this.lblEncryptionKey.TabIndex = 26;
            this.lblEncryptionKey.Text = "Encryption Key";
            // 
            // lblAuthKey
            // 
            this.lblAuthKey.AutoSize = true;
            this.lblAuthKey.Location = new System.Drawing.Point(6, 56);
            this.lblAuthKey.Name = "lblAuthKey";
            this.lblAuthKey.Size = new System.Drawing.Size(96, 13);
            this.lblAuthKey.TabIndex = 24;
            this.lblAuthKey.Text = "Authentication Key";
            // 
            // tbInvocationCounter
            // 
            this.tbInvocationCounter.BackColor = System.Drawing.SystemColors.Window;
            this.tbInvocationCounter.Location = new System.Drawing.Point(6, 173);
            this.tbInvocationCounter.MaxLength = 0;
            this.tbInvocationCounter.Name = "tbInvocationCounter";
            this.tbInvocationCounter.Size = new System.Drawing.Size(177, 20);
            this.tbInvocationCounter.TabIndex = 27;
            this.tbInvocationCounter.Text = "123456";
            // 
            // tbAuthenticationKey
            // 
            this.tbAuthenticationKey.BackColor = System.Drawing.SystemColors.Window;
            this.tbAuthenticationKey.Location = new System.Drawing.Point(6, 75);
            this.tbAuthenticationKey.MaxLength = 32;
            this.tbAuthenticationKey.Name = "tbAuthenticationKey";
            this.tbAuthenticationKey.Size = new System.Drawing.Size(177, 20);
            this.tbAuthenticationKey.TabIndex = 22;
            this.tbAuthenticationKey.Text = "D0D1D2D3D4D5D6D7D8D9DADBDCDDDEDF";
            // 
            // tbEncryptionKey
            // 
            this.tbEncryptionKey.BackColor = System.Drawing.SystemColors.Window;
            this.tbEncryptionKey.Location = new System.Drawing.Point(6, 127);
            this.tbEncryptionKey.MaxLength = 32;
            this.tbEncryptionKey.Name = "tbEncryptionKey";
            this.tbEncryptionKey.Size = new System.Drawing.Size(177, 20);
            this.tbEncryptionKey.TabIndex = 23;
            this.tbEncryptionKey.Text = "000102030405060708090A0B0C0D0E0F";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox1.Location = new System.Drawing.Point(0, 630);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(216, 52);
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // check_auto_hide
            // 
            this.check_auto_hide.AutoSize = true;
            this.check_auto_hide.BackColor = System.Drawing.Color.Transparent;
            this.check_auto_hide.ForeColor = System.Drawing.Color.Black;
            this.check_auto_hide.Location = new System.Drawing.Point(10, 1);
            this.check_auto_hide.Name = "check_auto_hide";
            this.check_auto_hide.Size = new System.Drawing.Size(73, 17);
            this.check_auto_hide.TabIndex = 18;
            this.check_auto_hide.Text = "Auto Hide";
            this.check_auto_hide.UseVisualStyleBackColor = false;
            // 
            // timer_PassKey
            // 
            this.timer_PassKey.Interval = 30000;
            this.timer_PassKey.Tick += new System.EventHandler(this.timer_PassKey_Tick);
            // 
            // FrmContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1356, 732);
            this.Controls.Add(this.pnlCommunication);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.stStatus);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmContainer";
            this.Text = "SEAC";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmContainer_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmContainer_FormClosed);
            this.Load += new System.EventHandler(this.FrmContainer_Load);
            this.stStatus.ResumeLayout(false);
            this.stStatus.PerformLayout();
            this.IpConContextMenu.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlPicIcons.ResumeLayout(false);
            this.pnlPicIcons.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flpParameterIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Parameterization)).EndInit();
            this.flpBillingIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Billing)).EndInit();
            this.flpLoadProfileIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_LoadProfile)).EndInit();
            this.flpEventsIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Events)).EndInit();
            this.flpInstantaneousIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Instantaneous)).EndInit();
            this.flpSettingIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_settings)).EndInit();
            this.flpDebugIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Debug)).EndInit();
            this.flpAdminIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_admin)).EndInit();
            this.flpHelpIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_AboutMe)).EndInit();
            this.flpLogOffIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_logoff)).EndInit();
            this.gp_IR_Port.ResumeLayout(false);
            this.gp_IR_Port.PerformLayout();
            this.pnlCommunication.ResumeLayout(false);
            this.pnlCommunication.PerformLayout();
            this.tcSetting.ResumeLayout(false);
            this.tpConnection.ResumeLayout(false);
            this.tpConnection.PerformLayout();
            this.gbHDLCAddress.ResumeLayout(false);
            this.gbHDLCAddress.PerformLayout();
            this.gpClientConfig.ResumeLayout(false);
            this.gpClientConfig.PerformLayout();
            this.gpPhysicalConnection.ResumeLayout(false);
            this.gp_WakeUp_Paras.ResumeLayout(false);
            this.gp_WakeUp_Paras.PerformLayout();
            this.gpMtrSerial.ResumeLayout(false);
            this.gpMtrSerial.PerformLayout();
            this.tpHLS.ResumeLayout(false);
            this.pnlAuthenticationKeys.ResumeLayout(false);
            this.pnlAuthenticationKeys.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip stStatus;
        private System.Windows.Forms.Label lblMainHeading;
        private System.Windows.Forms.Panel pnlCommunication;
        private ucCustomControl.Welcome Panel_Welcome;
        private System.Windows.Forms.Label lblPortStatus;
        private System.Windows.Forms.Label lblcapStatus;
        private System.Windows.Forms.Button btnOpenClose;
        private System.Windows.Forms.ComboBox comboPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.GroupBox gpClientConfig;
        public System.Windows.Forms.Label lblDevices;
        public System.Windows.Forms.ComboBox cmbDevices;
        public System.Windows.Forms.TextBox txtAssociationPaswd;
        internal System.Windows.Forms.MenuStrip menuStrip1;
        internal System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        public ucCustomControl.pnlDebugging Panel_Debugging;
        public ucCustomControl.Instantaneous Panel_Instantaneous = new ucCustomControl.Instantaneous();
        public ucCustomControl.pnlEvents Panel_Events;
        public ucCustomControl.pnlParameterization pnlParameterization1;
        //  private pnlLoadProfile pnlLoad_Profile;
        public ucCustomControl.pnlLoadProfile pnlLoad_Profile;

        public System.Windows.Forms.Label lblAssociation;
        public System.Windows.Forms.ComboBox cmbAssociations;
        private System.Windows.Forms.GroupBox gpPhysicalConnection;
        private System.Windows.Forms.GroupBox gp_IR_Port;
        private System.Windows.Forms.ComboBox cmbIRPorts;
        private System.Windows.Forms.Label lblSelPort;
        //  private System.Windows.Forms.Button btnConnectApplication;
        private System.Windows.Forms.ToolStripStatusLabel stStatus_lblConnnStatus;
        private System.Windows.Forms.GroupBox gp_WakeUp_Paras;
        private System.Windows.Forms.LinkLabel lnkbtnConfigDialog;
        private System.Windows.Forms.Panel pnlMain;
        internal System.Windows.Forms.ToolStripMenuItem configurationsToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem communicationConfigDialogToolStripMenuItem;
        public SmartEyeControl_7.ApplicationGUI.GUI.PnlBilling pnlBilling1;
        internal System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem aboutMeToolStripMenuItem;
        private System.Windows.Forms.LinkLabel lnkbtnRefreshServer;
        private System.Windows.Forms.LinkLabel lnk_Disconnect_forcedly;
        private System.Windows.Forms.ToolStripMenuItem applicationConfigToolStripMenuItem;
        public System.Windows.Forms.Panel pnlPicIcons;
        public System.Windows.Forms.PictureBox pic_settings;
        public System.Windows.Forms.PictureBox pic_Parameterization;
        public System.Windows.Forms.PictureBox pic_Instantaneous;
        public System.Windows.Forms.PictureBox pic_Events;
        public System.Windows.Forms.PictureBox pic_LoadProfile;
        public System.Windows.Forms.PictureBox pic_Billing;
        // private Welcome welcome1;
        public System.Windows.Forms.PictureBox pic_Debug;
        private System.Windows.Forms.ToolStripMenuItem databaseSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripStatusLabel stStatus_lblFirmwareVersion;
        private System.Windows.Forms.ToolStripStatusLabel stStatus_lblSerialNo;
        private System.Windows.Forms.ToolStripStatusLabel stStatus_stringEmpty;
        private System.Windows.Forms.ToolStripStatusLabel stStatusIOActivity;
        private System.Windows.Forms.Splitter splitter1;
        public PictureBox pic_AboutMe;
        public Label lblManufacturer;
        public ComboBox cmbManufacturers;
        private ComboBox comboBox1;
        private ComboBox cmb;
        public ListBox cmbMeterSerial;
        private ContextMenuStrip IpConContextMenu;
        private ToolTip toolIPConn;
        private ToolStripMenuItem GetMeterInfoMenuItem;
        private ToolStripMenuItem DropConnMenuItem;
        private ToolStripMenuItem readMeterInfoMenuItem;
        private ToolStripMenuItem removeListMenuItem;
        private GroupBox gpMtrSerial;
        private LinkLabel lnkTCPStatus;
        private LinkLabel lnkHeartBeatList;
        public ComponentFactory.Krypton.Toolkit.KryptonButton btnConnectApplication;
        private Timer timer_Connect;
        private Button btn_DefaultPwd;
        private CheckBox check_auto_hide;
        public Label lbl_headSettings;
        public Label lbl_headIns;
        public Label lbl_headevents;
        public Label lbl_headLp;
        public Label lbl_headhelp;
        public Label lbl_headparam;
        public Label lbl_headBill;
        public Label lbl_headdebug;
        public Label lbl_headAdmin;
        public PictureBox pic_admin;
        public SmartEyeControl_7.superAdminPanel pnlsuperAdminPanel1;
        public ComboBox cmbIOConnections;
        public Label lbl_headLogOff;
        public PictureBox pic_logoff;
        private Label label1;
        public Label lbl_loginName;
        private Timer timer_PassKey;
        private FlowLayoutPanel flowLayoutPanel3;
        public FlowLayoutPanel flpParameterIcon;
        public FlowLayoutPanel flpBillingIcon;
        public FlowLayoutPanel flpLoadProfileIcon;
        public FlowLayoutPanel flpEventsIcon;
        public FlowLayoutPanel flpInstantaneousIcon;
        public FlowLayoutPanel flpSettingIcon;
        public FlowLayoutPanel flpDebugIcon;
        private FlowLayoutPanel flpHelpIcon;
        public FlowLayoutPanel flpAdminIcon;
        private FlowLayoutPanel flpLogOffIcon;
        private FlowLayoutPanel flowLayoutPanel1;
        private PictureBox pictureBox1;
        private ToolStripProgressBar tsProgressBar;
        private ToolStripStatusLabel stlblStatus;
        private Panel pnlKeys;
        private Label label4;
        private TabControl tcSetting;
        private TabPage tpConnection;
        private TabPage tpHLS;
        private Panel pnlAuthenticationKeys;
        public ComboBox cmbSecurity;
        public Label lblInvocationCounter;
        public Label lblEncryptionKey;
        public Label lblAuthKey;
        public TextBox tbInvocationCounter;
        public TextBox tbAuthenticationKey;
        public TextBox tbEncryptionKey;
        public Label label2;
        private GroupBox gbHDLCAddress;
        private TextBox txtHDLCAddress;
        private Label lblRefreshPorts;
        private LinkLabel llblReloadRights;
    }
}

