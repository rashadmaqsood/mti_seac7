using OptocomSoftware.Reporting;
using SmartEyeControl_7.Reporting;
using System;
using System.Windows.Forms;

namespace ucCustomControl
{
    partial class pnlDebugging
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
            if (Application_Controller != null)
            {
                Application_Controller.PropertyChanged -= Application_Controller_PropertyChanged;
            }
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
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pnlDebugging));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblConnectionStatus = new System.Windows.Forms.Label();
            this.lblCapConnectionStatus = new System.Windows.Forms.Label();
            this.lblHeading1 = new System.Windows.Forms.Label();
            this.timer_testing = new System.Windows.Forms.Timer(this.components);
            this.bgw_GETAlarmCounter = new System.ComponentModel.BackgroundWorker();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.txt_TAG = new System.Windows.Forms.TextBox();
            this.txt_Length = new System.Windows.Forms.TextBox();
            this.txt_BaseAddress = new System.Windows.Forms.TextBox();
            this.cmb_EMP_No = new System.Windows.Forms.ComboBox();
            this.btn_AddSelectedObject = new System.Windows.Forms.Button();
            this.btn_RemoveSelectedObject = new System.Windows.Forms.Button();
            this.btn_RemoveAllObject = new System.Windows.Forms.Button();
            this.btn_AddAllObject = new System.Windows.Forms.Button();
            this.bgw = new System.ComponentModel.BackgroundWorker();
            this.Raw_Data_Viewer = new System.Windows.Forms.TabPage();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.lblRemainingChunks = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lblCurrentChunk = new System.Windows.Forms.Label();
            this.lblEpNo = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblTotalChunks = new System.Windows.Forms.Label();
            this.btnSavePackets = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.chk_Mem_References = new System.Windows.Forms.CheckedListBox();
            this.btn_Export_File = new System.Windows.Forms.Button();
            this.btn_Import_File = new System.Windows.Forms.Button();
            this.btn_Read_Memory_MAP = new System.Windows.Forms.Button();
            this.btn_Clear_All = new System.Windows.Forms.Button();
            this.chk_ALL = new System.Windows.Forms.CheckBox();
            this.gp_Mem_Ref = new System.Windows.Forms.GroupBox();
            this.btn_Set_CurrentMemRef = new System.Windows.Forms.Button();
            this.btn_Add_MemRef = new System.Windows.Forms.Button();
            this.btn_GET_CurrentMemRef = new System.Windows.Forms.Button();
            this.btn_GET_RawData = new System.Windows.Forms.Button();
            this.lbl_TAG = new System.Windows.Forms.Label();
            this.lbl_Length = new System.Windows.Forms.Label();
            this.lbl_Address = new System.Windows.Forms.Label();
            this.lbl_Mem_Module = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.chk_EditorFormat = new System.Windows.Forms.CheckBox();
            this.rtb_RAW_Data = new System.Windows.Forms.RichTextBox();
            this.btn_Clear_Editor_ = new System.Windows.Forms.Button();
            this.btn_copy_Clip_Board = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.chkICCGate = new System.Windows.Forms.CheckBox();
            this.btnGetIccGate = new System.Windows.Forms.Button();
            this.btnSetIccGate = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.cmpProtocol = new System.Windows.Forms.ComboBox();
            this.btnGetHDLCAddress = new System.Windows.Forms.Button();
            this.btnSetHDLCAddress = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.cmbHour = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cmbMinute = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.chkEnableTimerReset = new System.Windows.Forms.CheckBox();
            this.btnGetTimerReset = new System.Windows.Forms.Button();
            this.btnSetTimerReset = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.chkPowerUpReset = new System.Windows.Forms.CheckBox();
            this.btnPowerUpResetGet = new System.Windows.Forms.Button();
            this.btnPowerUpResetSet = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkWatchDogReset = new System.Windows.Forms.CheckBox();
            this.btnGetWatchDogReset = new System.Windows.Forms.Button();
            this.btnSetWatchDog = new System.Windows.Forms.Button();
            this.gbCalibrationMode = new System.Windows.Forms.GroupBox();
            this.btnCalibrationModeDeactive = new System.Windows.Forms.Button();
            this.btnCalibrationModeSet = new System.Windows.Forms.Button();
            this.gpDoorOpen = new System.Windows.Forms.GroupBox();
            this.btn_DoorOpenSet = new System.Windows.Forms.Button();
            this.btnModemStatus = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.rbHEX = new System.Windows.Forms.RadioButton();
            this.rbASCII = new System.Windows.Forms.RadioButton();
            this.txt_firmareInfo = new System.Windows.Forms.RichTextBox();
            this.txt_debugString = new System.Windows.Forms.TextBox();
            this.btn_getFirmwareInfo = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txt_major_alarm_profile = new System.Windows.Forms.TextBox();
            this.btn_Get_major_alram_counter = new System.Windows.Forms.Button();
            this.btn_Set_major_alram_counter = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_DebugStr_lenght = new System.Windows.Forms.Label();
            this.btn_readCautions = new System.Windows.Forms.Button();
            this.btn_readErrors = new System.Windows.Forms.Button();
            this.btn_debugString = new System.Windows.Forms.Button();
            this.tab_debug = new System.Windows.Forms.TabPage();
            this.btn_SetParams = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgv_TestOutput = new System.Windows.Forms.DataGridView();
            this.dtTimeStamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stOBISCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ObjectLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBISLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AttributeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Attribute_Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testOutput = new System.Windows.Forms.DataGridViewLinkColumn();
            this.txt_getCount = new System.Windows.Forms.TextBox();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.btn_TL = new System.Windows.Forms.Button();
            this.btn_T4 = new System.Windows.Forms.Button();
            this.btn_T3 = new System.Windows.Forms.Button();
            this.btn_T2 = new System.Windows.Forms.Button();
            this.btn_T1 = new System.Windows.Forms.Button();
            this.pb1 = new System.Windows.Forms.ProgressBar();
            this.btn_ClearSelectedList = new System.Windows.Forms.Button();
            this.btn_ListAddAll = new System.Windows.Forms.Button();
            this.btn_ListSearch = new System.Windows.Forms.Button();
            this.list_Search = new System.Windows.Forms.ListBox();
            this.lbl_totalItemsSelected = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_StopProcess = new System.Windows.Forms.Button();
            this.btn_GetParams = new System.Windows.Forms.Button();
            this.list_Selected = new System.Windows.Forms.ListBox();
            this.list_possible = new System.Windows.Forms.ListBox();
            this.tpObisCodes = new System.Windows.Forms.TabPage();
            this.txtIDecodedOutput = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtIWroteResp = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnIWrtireAttrib = new System.Windows.Forms.Button();
            this.rdbAscii = new System.Windows.Forms.RadioButton();
            this.rdbHex = new System.Windows.Forms.RadioButton();
            this.rdbDecimal = new System.Windows.Forms.RadioButton();
            this.txtIValue = new System.Windows.Forms.TextBox();
            this.lblCapWriteResponse = new System.Windows.Forms.Label();
            this.lblCapValue = new System.Windows.Forms.Label();
            this.lclCapDatatype = new System.Windows.Forms.Label();
            this.cbxDataTypesWR = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkEnableSelectiveAccess = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.gpSelectiveAccess = new System.Windows.Forms.GroupBox();
            this.tcAccessSelector = new System.Windows.Forms.TabControl();
            this.tpEntry = new System.Windows.Forms.TabPage();
            this.lblToSelectedVal = new System.Windows.Forms.Label();
            this.lblFromEntry = new System.Windows.Forms.Label();
            this.txtToSelectedVal = new System.Windows.Forms.TextBox();
            this.txtFromEntry = new System.Windows.Forms.TextBox();
            this.lblFromSelectedVal = new System.Windows.Forms.Label();
            this.txtToEntry = new System.Windows.Forms.TextBox();
            this.txtFromSelectedVal = new System.Windows.Forms.TextBox();
            this.lblToEntry = new System.Windows.Forms.Label();
            this.tpRange = new System.Windows.Forms.TabPage();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lbl_fromTxt = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lbl_ToTxt = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtAttribute = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnIReadAttrib = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.txtClassId = new System.Windows.Forms.TextBox();
            this.btnIClrDecodedOP = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.btnC2CDecodedOP = new System.Windows.Forms.Button();
            this.txtC = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtA = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtB = new System.Windows.Forms.TextBox();
            this.txtF = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtD = new System.Windows.Forms.TextBox();
            this.txtE = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flp_DebugBtn = new System.Windows.Forms.FlowLayoutPanel();
            this.btnReadObis = new System.Windows.Forms.Button();
            this.btnObjectListoutput = new System.Windows.Forms.Button();
            this.trvIObisCodes = new System.Windows.Forms.TreeView();
            this.lbxObisCodes = new System.Windows.Forms.ListBox();
            this.tp1 = new System.Windows.Forms.TabPage();
            this.btnIClearcomm = new System.Windows.Forms.Button();
            this.btnIClearOutput = new System.Windows.Forms.Button();
            this.btnIC2CComm = new System.Windows.Forms.Button();
            this.btnIC2Coutput = new System.Windows.Forms.Button();
            this.txtICommViewer = new System.Windows.Forms.TextBox();
            this.txtOutPutWindow = new System.Windows.Forms.TextBox();
            this.lblCapResponse = new System.Windows.Forms.Label();
            this.lblCapOutput = new System.Windows.Forms.Label();
            this.tcontrolMain = new System.Windows.Forms.TabControl();
            this.tpIOStates = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.srno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pin_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.port = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.in_sys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.in_act = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.in_low = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.out_sys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.out_act = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.out_low = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dir_sys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dir_act = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dir_low = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sel_sys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sel_act = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sel_low = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ren = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnreadState = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.bgw_DebugTest = new System.ComponentModel.BackgroundWorker();
            this.ds_CB_Day_Record = new OptocomSoftware.Reporting.ds_CB_Day_Record();
            this.dsCBDayRecordBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.Raw_Data_Viewer.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.gp_Mem_Ref.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.gbCalibrationMode.SuspendLayout();
            this.gpDoorOpen.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tab_debug.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_TestOutput)).BeginInit();
            this.tpObisCodes.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gpSelectiveAccess.SuspendLayout();
            this.tcAccessSelector.SuspendLayout();
            this.tpEntry.SuspendLayout();
            this.tpRange.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flp_DebugBtn.SuspendLayout();
            this.tp1.SuspendLayout();
            this.tcontrolMain.SuspendLayout();
            this.tpIOStates.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ds_CB_Day_Record)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCBDayRecordBindingSource)).BeginInit();
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
            // lblHeading1
            // 
            this.lblHeading1.AutoSize = true;
            this.lblHeading1.Location = new System.Drawing.Point(58, 65);
            this.lblHeading1.Name = "lblHeading1";
            this.lblHeading1.Size = new System.Drawing.Size(215, 13);
            this.lblHeading1.TabIndex = 0;
            this.lblHeading1.Text = "Establish Connection with Meter to Continue";
            // 
            // timer_testing
            // 
            this.timer_testing.Interval = 1000;
            this.timer_testing.Tick += new System.EventHandler(this.timer_testing_Tick);
            // 
            // bgw_GETAlarmCounter
            // 
            this.bgw_GETAlarmCounter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_GETAlarmCounter_DoWork);
            this.bgw_GETAlarmCounter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_GETAlarmCounter_RunWorkerCompleted);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // txt_TAG
            // 
            this.txt_TAG.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_TAG.Location = new System.Drawing.Point(40, 66);
            this.txt_TAG.Name = "txt_TAG";
            this.txt_TAG.Size = new System.Drawing.Size(227, 20);
            this.txt_TAG.TabIndex = 14;
            this.toolTip.SetToolTip(this.txt_TAG, "Add Descriptive TAG here");
            this.txt_TAG.Visible = false;
            this.txt_TAG.TextChanged += new System.EventHandler(this.txt_TAG_TextChanged);
            // 
            // txt_Length
            // 
            this.txt_Length.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Length.Location = new System.Drawing.Point(199, 43);
            this.txt_Length.Name = "txt_Length";
            this.txt_Length.Size = new System.Drawing.Size(68, 20);
            this.txt_Length.TabIndex = 2;
            this.toolTip.SetToolTip(this.txt_Length, "1024 MAX ");
            this.txt_Length.TextChanged += new System.EventHandler(this.txt_Length_TextChanged);
            // 
            // txt_BaseAddress
            // 
            this.txt_BaseAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_BaseAddress.Location = new System.Drawing.Point(114, 43);
            this.txt_BaseAddress.Name = "txt_BaseAddress";
            this.txt_BaseAddress.Size = new System.Drawing.Size(79, 20);
            this.txt_BaseAddress.TabIndex = 1;
            this.toolTip.SetToolTip(this.txt_BaseAddress, "[0000]-[FFFF] Range");
            this.txt_BaseAddress.TextChanged += new System.EventHandler(this.txt_BaseAddress_TextChanged);
            // 
            // cmb_EMP_No
            // 
            this.cmb_EMP_No.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_EMP_No.FormattingEnabled = true;
            this.cmb_EMP_No.Items.AddRange(new object[] {
            "0: EPM1a        ",
            "1: EPM1b        ",
            "2: EPM1c         ",
            "3: EPM1d        ",
            "4: EPM2a        ",
            "5: EPM2b        ",
            "6: EPM2c         ",
            "7: EPM2d        ",
            "8: SFR_RAM ",
            "9: RAM            "});
            this.cmb_EMP_No.Location = new System.Drawing.Point(9, 43);
            this.cmb_EMP_No.Name = "cmb_EMP_No";
            this.cmb_EMP_No.Size = new System.Drawing.Size(99, 21);
            this.cmb_EMP_No.TabIndex = 0;
            this.toolTip.SetToolTip(this.cmb_EMP_No, "Select EEPROM Module No");
            this.cmb_EMP_No.SelectedValueChanged += new System.EventHandler(this.cmb_EMP_No_SelectedValueChanged);
            this.cmb_EMP_No.TextChanged += new System.EventHandler(this.cmb_EMP_No_TextChanged);
            // 
            // btn_AddSelectedObject
            // 
            this.btn_AddSelectedObject.AutoSize = true;
            this.btn_AddSelectedObject.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.btn_AddSelectedObject.ForeColor = System.Drawing.Color.Black;
            this.btn_AddSelectedObject.Image = ((System.Drawing.Image)(resources.GetObject("btn_AddSelectedObject.Image")));
            this.btn_AddSelectedObject.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_AddSelectedObject.Location = new System.Drawing.Point(305, 263);
            this.btn_AddSelectedObject.Name = "btn_AddSelectedObject";
            this.btn_AddSelectedObject.Size = new System.Drawing.Size(50, 30);
            this.btn_AddSelectedObject.TabIndex = 25;
            this.btn_AddSelectedObject.Tag = "Button";
            this.btn_AddSelectedObject.Text = ">>";
            this.toolTip.SetToolTip(this.btn_AddSelectedObject, "Add All Objects Available In Current Login");
            this.btn_AddSelectedObject.Click += new System.EventHandler(this.btn_AddSelectedObject_Click);
            // 
            // btn_RemoveSelectedObject
            // 
            this.btn_RemoveSelectedObject.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_RemoveSelectedObject.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RemoveSelectedObject.Image = ((System.Drawing.Image)(resources.GetObject("btn_RemoveSelectedObject.Image")));
            this.btn_RemoveSelectedObject.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_RemoveSelectedObject.Location = new System.Drawing.Point(305, 371);
            this.btn_RemoveSelectedObject.Name = "btn_RemoveSelectedObject";
            this.btn_RemoveSelectedObject.Size = new System.Drawing.Size(50, 30);
            this.btn_RemoveSelectedObject.TabIndex = 24;
            this.btn_RemoveSelectedObject.Tag = "Button";
            this.btn_RemoveSelectedObject.Text = "<<";
            this.toolTip.SetToolTip(this.btn_RemoveSelectedObject, "Remove All Objects Available In Current Login");
            this.btn_RemoveSelectedObject.Click += new System.EventHandler(this.btn_RemoveSelectedObject_Click);
            // 
            // btn_RemoveAllObject
            // 
            this.btn_RemoveAllObject.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_RemoveAllObject.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RemoveAllObject.Image = ((System.Drawing.Image)(resources.GetObject("btn_RemoveAllObject.Image")));
            this.btn_RemoveAllObject.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_RemoveAllObject.Location = new System.Drawing.Point(305, 335);
            this.btn_RemoveAllObject.Name = "btn_RemoveAllObject";
            this.btn_RemoveAllObject.Size = new System.Drawing.Size(50, 30);
            this.btn_RemoveAllObject.TabIndex = 22;
            this.btn_RemoveAllObject.Tag = "Button";
            this.btn_RemoveAllObject.Text = "<<<";
            this.toolTip.SetToolTip(this.btn_RemoveAllObject, "Remove All Objects Available in Current Device");
            this.btn_RemoveAllObject.Click += new System.EventHandler(this.btn_RemoveAllObject_Click);
            // 
            // btn_AddAllObject
            // 
            this.btn_AddAllObject.AutoSize = true;
            this.btn_AddAllObject.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.btn_AddAllObject.ForeColor = System.Drawing.Color.Black;
            this.btn_AddAllObject.Image = ((System.Drawing.Image)(resources.GetObject("btn_AddAllObject.Image")));
            this.btn_AddAllObject.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_AddAllObject.Location = new System.Drawing.Point(305, 299);
            this.btn_AddAllObject.Name = "btn_AddAllObject";
            this.btn_AddAllObject.Size = new System.Drawing.Size(50, 30);
            this.btn_AddAllObject.TabIndex = 21;
            this.btn_AddAllObject.Tag = "Button";
            this.btn_AddAllObject.Text = ">>>";
            this.toolTip.SetToolTip(this.btn_AddAllObject, "Add All Objects Available In Current Device");
            this.btn_AddAllObject.Click += new System.EventHandler(this.btn_AddAllObject_Click);
            // 
            // Raw_Data_Viewer
            // 
            this.Raw_Data_Viewer.Controls.Add(this.groupBox11);
            this.Raw_Data_Viewer.Controls.Add(this.btnSavePackets);
            this.Raw_Data_Viewer.Controls.Add(this.groupBox10);
            this.Raw_Data_Viewer.Controls.Add(this.gp_Mem_Ref);
            this.Raw_Data_Viewer.Controls.Add(this.progressBar);
            this.Raw_Data_Viewer.Controls.Add(this.chk_EditorFormat);
            this.Raw_Data_Viewer.Controls.Add(this.rtb_RAW_Data);
            this.Raw_Data_Viewer.Controls.Add(this.btn_Clear_Editor_);
            this.Raw_Data_Viewer.Controls.Add(this.btn_copy_Clip_Board);
            this.Raw_Data_Viewer.Location = new System.Drawing.Point(4, 22);
            this.Raw_Data_Viewer.Name = "Raw_Data_Viewer";
            this.Raw_Data_Viewer.Padding = new System.Windows.Forms.Padding(3);
            this.Raw_Data_Viewer.Size = new System.Drawing.Size(1130, 495);
            this.Raw_Data_Viewer.TabIndex = 8;
            this.Raw_Data_Viewer.Text = "Raw Data Viewer";
            this.Raw_Data_Viewer.UseVisualStyleBackColor = true;
            this.Raw_Data_Viewer.Enter += new System.EventHandler(this.Raw_Data_Viewer_Enter);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.label22);
            this.groupBox11.Controls.Add(this.lblRemainingChunks);
            this.groupBox11.Controls.Add(this.label18);
            this.groupBox11.Controls.Add(this.label24);
            this.groupBox11.Controls.Add(this.lblCurrentChunk);
            this.groupBox11.Controls.Add(this.lblEpNo);
            this.groupBox11.Controls.Add(this.label20);
            this.groupBox11.Controls.Add(this.lblTotalChunks);
            this.groupBox11.Location = new System.Drawing.Point(6, 392);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(404, 74);
            this.groupBox11.TabIndex = 44;
            this.groupBox11.TabStop = false;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(7, 47);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(86, 13);
            this.label22.TabIndex = 40;
            this.label22.Text = "Total Chunks:";
            // 
            // lblRemainingChunks
            // 
            this.lblRemainingChunks.AutoSize = true;
            this.lblRemainingChunks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemainingChunks.Location = new System.Drawing.Point(322, 47);
            this.lblRemainingChunks.Name = "lblRemainingChunks";
            this.lblRemainingChunks.Size = new System.Drawing.Size(15, 13);
            this.lblRemainingChunks.TabIndex = 43;
            this.lblRemainingChunks.Text = "--";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(200, 47);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(116, 13);
            this.label18.TabIndex = 36;
            this.label18.Text = "Remaining Chunks:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(7, 25);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(62, 13);
            this.label24.TabIndex = 42;
            this.label24.Text = "EPM_NO:";
            // 
            // lblCurrentChunk
            // 
            this.lblCurrentChunk.AutoSize = true;
            this.lblCurrentChunk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentChunk.Location = new System.Drawing.Point(301, 25);
            this.lblCurrentChunk.Name = "lblCurrentChunk";
            this.lblCurrentChunk.Size = new System.Drawing.Size(15, 13);
            this.lblCurrentChunk.TabIndex = 37;
            this.lblCurrentChunk.Text = "--";
            // 
            // lblEpNo
            // 
            this.lblEpNo.AutoSize = true;
            this.lblEpNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEpNo.Location = new System.Drawing.Point(69, 25);
            this.lblEpNo.Name = "lblEpNo";
            this.lblEpNo.Size = new System.Drawing.Size(15, 13);
            this.lblEpNo.TabIndex = 41;
            this.lblEpNo.Text = "--";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(200, 25);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(92, 13);
            this.label20.TabIndex = 38;
            this.label20.Text = "Current Chunk:";
            // 
            // lblTotalChunks
            // 
            this.lblTotalChunks.AutoSize = true;
            this.lblTotalChunks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalChunks.Location = new System.Drawing.Point(99, 47);
            this.lblTotalChunks.Name = "lblTotalChunks";
            this.lblTotalChunks.Size = new System.Drawing.Size(15, 13);
            this.lblTotalChunks.TabIndex = 39;
            this.lblTotalChunks.Text = "--";
            // 
            // btnSavePackets
            // 
            this.btnSavePackets.Location = new System.Drawing.Point(572, 434);
            this.btnSavePackets.Name = "btnSavePackets";
            this.btnSavePackets.Size = new System.Drawing.Size(150, 30);
            this.btnSavePackets.TabIndex = 25;
            this.btnSavePackets.Tag = "Button";
            this.btnSavePackets.Text = "Export Data";
            this.btnSavePackets.Click += new System.EventHandler(this.btnSavePackets_Click);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.chk_Mem_References);
            this.groupBox10.Controls.Add(this.btn_Export_File);
            this.groupBox10.Controls.Add(this.btn_Import_File);
            this.groupBox10.Controls.Add(this.btn_Read_Memory_MAP);
            this.groupBox10.Controls.Add(this.btn_Clear_All);
            this.groupBox10.Controls.Add(this.chk_ALL);
            this.groupBox10.Location = new System.Drawing.Point(6, 151);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(404, 237);
            this.groupBox10.TabIndex = 35;
            this.groupBox10.TabStop = false;
            // 
            // chk_Mem_References
            // 
            this.chk_Mem_References.FormattingEnabled = true;
            this.chk_Mem_References.Location = new System.Drawing.Point(9, 19);
            this.chk_Mem_References.Name = "chk_Mem_References";
            this.chk_Mem_References.Size = new System.Drawing.Size(253, 184);
            this.chk_Mem_References.Sorted = true;
            this.chk_Mem_References.TabIndex = 15;
            // 
            // btn_Export_File
            // 
            this.btn_Export_File.Location = new System.Drawing.Point(268, 148);
            this.btn_Export_File.Name = "btn_Export_File";
            this.btn_Export_File.Size = new System.Drawing.Size(130, 25);
            this.btn_Export_File.TabIndex = 19;
            this.btn_Export_File.Tag = "Button";
            this.btn_Export_File.Text = "Export";
            this.btn_Export_File.Click += new System.EventHandler(this.btn_Export_File_Click);
            // 
            // btn_Import_File
            // 
            this.btn_Import_File.Location = new System.Drawing.Point(268, 178);
            this.btn_Import_File.Name = "btn_Import_File";
            this.btn_Import_File.Size = new System.Drawing.Size(130, 25);
            this.btn_Import_File.TabIndex = 20;
            this.btn_Import_File.Tag = "Button";
            this.btn_Import_File.Text = "Import";
            this.btn_Import_File.Click += new System.EventHandler(this.btn_Import_File_Click);
            // 
            // btn_Read_Memory_MAP
            // 
            this.btn_Read_Memory_MAP.Location = new System.Drawing.Point(268, 19);
            this.btn_Read_Memory_MAP.Name = "btn_Read_Memory_MAP";
            this.btn_Read_Memory_MAP.Size = new System.Drawing.Size(130, 25);
            this.btn_Read_Memory_MAP.TabIndex = 23;
            this.btn_Read_Memory_MAP.Tag = "Button";
            this.btn_Read_Memory_MAP.Text = "Read Memory MAP";
            this.btn_Read_Memory_MAP.Click += new System.EventHandler(this.btn_Read_Memory_MAP_Click);
            // 
            // btn_Clear_All
            // 
            this.btn_Clear_All.Location = new System.Drawing.Point(268, 50);
            this.btn_Clear_All.Name = "btn_Clear_All";
            this.btn_Clear_All.Size = new System.Drawing.Size(130, 25);
            this.btn_Clear_All.TabIndex = 18;
            this.btn_Clear_All.Tag = "Button";
            this.btn_Clear_All.Text = "Clear";
            this.btn_Clear_All.Click += new System.EventHandler(this.btn_Clear_All_Click);
            // 
            // chk_ALL
            // 
            this.chk_ALL.AutoSize = true;
            this.chk_ALL.Location = new System.Drawing.Point(9, 209);
            this.chk_ALL.Name = "chk_ALL";
            this.chk_ALL.Size = new System.Drawing.Size(70, 17);
            this.chk_ALL.TabIndex = 24;
            this.chk_ALL.Text = "Select All";
            this.chk_ALL.UseVisualStyleBackColor = true;
            this.chk_ALL.CheckedChanged += new System.EventHandler(this.chk_ALL_CheckedChanged);
            // 
            // gp_Mem_Ref
            // 
            this.gp_Mem_Ref.Controls.Add(this.btn_Set_CurrentMemRef);
            this.gp_Mem_Ref.Controls.Add(this.btn_Add_MemRef);
            this.gp_Mem_Ref.Controls.Add(this.btn_GET_CurrentMemRef);
            this.gp_Mem_Ref.Controls.Add(this.btn_GET_RawData);
            this.gp_Mem_Ref.Controls.Add(this.txt_TAG);
            this.gp_Mem_Ref.Controls.Add(this.lbl_TAG);
            this.gp_Mem_Ref.Controls.Add(this.lbl_Length);
            this.gp_Mem_Ref.Controls.Add(this.lbl_Address);
            this.gp_Mem_Ref.Controls.Add(this.lbl_Mem_Module);
            this.gp_Mem_Ref.Controls.Add(this.txt_Length);
            this.gp_Mem_Ref.Controls.Add(this.txt_BaseAddress);
            this.gp_Mem_Ref.Controls.Add(this.cmb_EMP_No);
            this.gp_Mem_Ref.Location = new System.Drawing.Point(6, 8);
            this.gp_Mem_Ref.Name = "gp_Mem_Ref";
            this.gp_Mem_Ref.Size = new System.Drawing.Size(404, 137);
            this.gp_Mem_Ref.TabIndex = 0;
            this.gp_Mem_Ref.TabStop = false;
            this.gp_Mem_Ref.Text = "Memory References";
            // 
            // btn_Set_CurrentMemRef
            // 
            this.btn_Set_CurrentMemRef.Location = new System.Drawing.Point(129, 92);
            this.btn_Set_CurrentMemRef.Name = "btn_Set_CurrentMemRef";
            this.btn_Set_CurrentMemRef.Size = new System.Drawing.Size(133, 25);
            this.btn_Set_CurrentMemRef.TabIndex = 17;
            this.btn_Set_CurrentMemRef.Tag = "Button";
            this.btn_Set_CurrentMemRef.Text = "Write Mem Reference";
            this.btn_Set_CurrentMemRef.Click += new System.EventHandler(this.btn_Set_CurrentMemRef_Click);
            // 
            // btn_Add_MemRef
            // 
            this.btn_Add_MemRef.Location = new System.Drawing.Point(6, 92);
            this.btn_Add_MemRef.Name = "btn_Add_MemRef";
            this.btn_Add_MemRef.Size = new System.Drawing.Size(117, 25);
            this.btn_Add_MemRef.TabIndex = 16;
            this.btn_Add_MemRef.Tag = "Button";
            this.btn_Add_MemRef.Text = "Add Reference";
            this.btn_Add_MemRef.Click += new System.EventHandler(this.btn_Add_MemRef_Click);
            // 
            // btn_GET_CurrentMemRef
            // 
            this.btn_GET_CurrentMemRef.Location = new System.Drawing.Point(273, 92);
            this.btn_GET_CurrentMemRef.Name = "btn_GET_CurrentMemRef";
            this.btn_GET_CurrentMemRef.Size = new System.Drawing.Size(125, 25);
            this.btn_GET_CurrentMemRef.TabIndex = 21;
            this.btn_GET_CurrentMemRef.Tag = "Button";
            this.btn_GET_CurrentMemRef.Text = "Read Mem Reference";
            this.btn_GET_CurrentMemRef.Click += new System.EventHandler(this.btn_GET_CurrentMemRef_Click);
            // 
            // btn_GET_RawData
            // 
            this.btn_GET_RawData.Location = new System.Drawing.Point(273, 40);
            this.btn_GET_RawData.Name = "btn_GET_RawData";
            this.btn_GET_RawData.Size = new System.Drawing.Size(125, 25);
            this.btn_GET_RawData.TabIndex = 22;
            this.btn_GET_RawData.Tag = "Button";
            this.btn_GET_RawData.Text = "Read Data";
            this.btn_GET_RawData.Click += new System.EventHandler(this.btn_GET_RawData_Click);
            // 
            // lbl_TAG
            // 
            this.lbl_TAG.AutoSize = true;
            this.lbl_TAG.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TAG.Location = new System.Drawing.Point(6, 69);
            this.lbl_TAG.Name = "lbl_TAG";
            this.lbl_TAG.Size = new System.Drawing.Size(32, 13);
            this.lbl_TAG.TabIndex = 13;
            this.lbl_TAG.Text = "TAG";
            this.lbl_TAG.Visible = false;
            // 
            // lbl_Length
            // 
            this.lbl_Length.AutoSize = true;
            this.lbl_Length.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Length.Location = new System.Drawing.Point(196, 27);
            this.lbl_Length.Name = "lbl_Length";
            this.lbl_Length.Size = new System.Drawing.Size(46, 13);
            this.lbl_Length.TabIndex = 5;
            this.lbl_Length.Text = "Length";
            // 
            // lbl_Address
            // 
            this.lbl_Address.AutoSize = true;
            this.lbl_Address.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Address.Location = new System.Drawing.Point(111, 27);
            this.lbl_Address.Name = "lbl_Address";
            this.lbl_Address.Size = new System.Drawing.Size(84, 13);
            this.lbl_Address.TabIndex = 4;
            this.lbl_Address.Text = "Base Address";
            // 
            // lbl_Mem_Module
            // 
            this.lbl_Mem_Module.AutoSize = true;
            this.lbl_Mem_Module.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Mem_Module.Location = new System.Drawing.Point(16, 27);
            this.lbl_Mem_Module.Name = "lbl_Mem_Module";
            this.lbl_Mem_Module.Size = new System.Drawing.Size(58, 13);
            this.lbl_Mem_Module.TabIndex = 3;
            this.lbl_Mem_Module.Text = "EPM_NO";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(942, 434);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(182, 23);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 34;
            this.progressBar.Visible = false;
            // 
            // chk_EditorFormat
            // 
            this.chk_EditorFormat.AutoSize = true;
            this.chk_EditorFormat.Location = new System.Drawing.Point(809, 440);
            this.chk_EditorFormat.Name = "chk_EditorFormat";
            this.chk_EditorFormat.Size = new System.Drawing.Size(116, 17);
            this.chk_EditorFormat.TabIndex = 33;
            this.chk_EditorFormat.Text = "Word Wrap Format";
            this.chk_EditorFormat.UseVisualStyleBackColor = true;
            this.chk_EditorFormat.CheckedChanged += new System.EventHandler(this.chk_EditorFormat_CheckedChanged);
            // 
            // rtb_RAW_Data
            // 
            this.rtb_RAW_Data.AcceptsTab = true;
            this.rtb_RAW_Data.Font = new System.Drawing.Font("Courier New", 8.9F);
            this.rtb_RAW_Data.Location = new System.Drawing.Point(416, 12);
            this.rtb_RAW_Data.Name = "rtb_RAW_Data";
            this.rtb_RAW_Data.ReadOnly = true;
            this.rtb_RAW_Data.Size = new System.Drawing.Size(708, 416);
            this.rtb_RAW_Data.TabIndex = 12;
            this.rtb_RAW_Data.Text = "";
            // 
            // btn_Clear_Editor_
            // 
            this.btn_Clear_Editor_.Image = ((System.Drawing.Image)(resources.GetObject("btn_Clear_Editor_.Image")));
            this.btn_Clear_Editor_.Location = new System.Drawing.Point(728, 434);
            this.btn_Clear_Editor_.Name = "btn_Clear_Editor_";
            this.btn_Clear_Editor_.Size = new System.Drawing.Size(75, 30);
            this.btn_Clear_Editor_.TabIndex = 32;
            this.btn_Clear_Editor_.Tag = "Button";
            this.btn_Clear_Editor_.Text = "Clear";
            this.btn_Clear_Editor_.Click += new System.EventHandler(this.btn_Clear_Editor_Click);
            // 
            // btn_copy_Clip_Board
            // 
            this.btn_copy_Clip_Board.Image = ((System.Drawing.Image)(resources.GetObject("btn_copy_Clip_Board.Image")));
            this.btn_copy_Clip_Board.Location = new System.Drawing.Point(416, 434);
            this.btn_copy_Clip_Board.Name = "btn_copy_Clip_Board";
            this.btn_copy_Clip_Board.Size = new System.Drawing.Size(150, 30);
            this.btn_copy_Clip_Board.TabIndex = 30;
            this.btn_copy_Clip_Board.Tag = "Button";
            this.btn_copy_Clip_Board.Text = "Copy to Clipboard";
            this.btn_copy_Clip_Board.Click += new System.EventHandler(this.btn_copy_Clip_Board_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage4.Controls.Add(this.groupBox9);
            this.tabPage4.Controls.Add(this.groupBox8);
            this.tabPage4.Controls.Add(this.groupBox7);
            this.tabPage4.Controls.Add(this.groupBox6);
            this.tabPage4.Controls.Add(this.groupBox5);
            this.tabPage4.Controls.Add(this.gbCalibrationMode);
            this.tabPage4.Controls.Add(this.gpDoorOpen);
            this.tabPage4.Controls.Add(this.btnModemStatus);
            this.tabPage4.Controls.Add(this.btnClear);
            this.tabPage4.Controls.Add(this.rbHEX);
            this.tabPage4.Controls.Add(this.rbASCII);
            this.tabPage4.Controls.Add(this.txt_firmareInfo);
            this.tabPage4.Controls.Add(this.txt_debugString);
            this.tabPage4.Controls.Add(this.btn_getFirmwareInfo);
            this.tabPage4.Controls.Add(this.groupBox4);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.lbl_DebugStr_lenght);
            this.tabPage4.Controls.Add(this.btn_readCautions);
            this.tabPage4.Controls.Add(this.btn_readErrors);
            this.tabPage4.Controls.Add(this.btn_debugString);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1130, 495);
            this.tabPage4.TabIndex = 5;
            this.tabPage4.Text = "Debug Str";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.chkICCGate);
            this.groupBox9.Controls.Add(this.btnGetIccGate);
            this.groupBox9.Controls.Add(this.btnSetIccGate);
            this.groupBox9.Location = new System.Drawing.Point(813, 383);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(259, 54);
            this.groupBox9.TabIndex = 28;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "ICC Gate";
            // 
            // chkICCGate
            // 
            this.chkICCGate.AutoSize = true;
            this.chkICCGate.Location = new System.Drawing.Point(133, 20);
            this.chkICCGate.Name = "chkICCGate";
            this.chkICCGate.Size = new System.Drawing.Size(105, 17);
            this.chkICCGate.TabIndex = 27;
            this.chkICCGate.Text = "Enable ICC Gate";
            this.chkICCGate.UseVisualStyleBackColor = true;
            // 
            // btnGetIccGate
            // 
            this.btnGetIccGate.Location = new System.Drawing.Point(70, 16);
            this.btnGetIccGate.Name = "btnGetIccGate";
            this.btnGetIccGate.Size = new System.Drawing.Size(57, 24);
            this.btnGetIccGate.TabIndex = 21;
            this.btnGetIccGate.Tag = "Button";
            this.btnGetIccGate.Text = "Get";
            this.btnGetIccGate.Click += new System.EventHandler(this.btnGetIccGate_Click);
            // 
            // btnSetIccGate
            // 
            this.btnSetIccGate.Location = new System.Drawing.Point(7, 16);
            this.btnSetIccGate.Name = "btnSetIccGate";
            this.btnSetIccGate.Size = new System.Drawing.Size(57, 24);
            this.btnSetIccGate.TabIndex = 20;
            this.btnSetIccGate.Tag = "Button";
            this.btnSetIccGate.Text = "Set";
            this.btnSetIccGate.Click += new System.EventHandler(this.btnSetIccGate_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.cmpProtocol);
            this.groupBox8.Controls.Add(this.btnGetHDLCAddress);
            this.groupBox8.Controls.Add(this.btnSetHDLCAddress);
            this.groupBox8.Location = new System.Drawing.Point(813, 436);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(258, 43);
            this.groupBox8.TabIndex = 27;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Protocol Gate";
            // 
            // cmpProtocol
            // 
            this.cmpProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmpProtocol.FormattingEnabled = true;
            this.cmpProtocol.Location = new System.Drawing.Point(131, 12);
            this.cmpProtocol.Name = "cmpProtocol";
            this.cmpProtocol.Size = new System.Drawing.Size(121, 21);
            this.cmpProtocol.TabIndex = 22;
            // 
            // btnGetHDLCAddress
            // 
            this.btnGetHDLCAddress.Location = new System.Drawing.Point(70, 12);
            this.btnGetHDLCAddress.Name = "btnGetHDLCAddress";
            this.btnGetHDLCAddress.Size = new System.Drawing.Size(57, 24);
            this.btnGetHDLCAddress.TabIndex = 21;
            this.btnGetHDLCAddress.Tag = "Button";
            this.btnGetHDLCAddress.Text = "Get";
            this.btnGetHDLCAddress.Click += new System.EventHandler(this.btnGetProtocol_GateWay_Click);
            // 
            // btnSetHDLCAddress
            // 
            this.btnSetHDLCAddress.Location = new System.Drawing.Point(7, 12);
            this.btnSetHDLCAddress.Name = "btnSetHDLCAddress";
            this.btnSetHDLCAddress.Size = new System.Drawing.Size(57, 24);
            this.btnSetHDLCAddress.TabIndex = 20;
            this.btnSetHDLCAddress.Tag = "Button";
            this.btnSetHDLCAddress.Text = "Set";
            this.btnSetHDLCAddress.Click += new System.EventHandler(this.btnSetProtocol_GateWay_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.cmbHour);
            this.groupBox7.Controls.Add(this.label16);
            this.groupBox7.Controls.Add(this.cmbMinute);
            this.groupBox7.Controls.Add(this.label15);
            this.groupBox7.Controls.Add(this.chkEnableTimerReset);
            this.groupBox7.Controls.Add(this.btnGetTimerReset);
            this.groupBox7.Controls.Add(this.btnSetTimerReset);
            this.groupBox7.Location = new System.Drawing.Point(659, 383);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(148, 96);
            this.groupBox7.TabIndex = 22;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Timer Reset";
            // 
            // cmbHour
            // 
            this.cmbHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHour.FormattingEnabled = true;
            this.cmbHour.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23"});
            this.cmbHour.Location = new System.Drawing.Point(35, 45);
            this.cmbHour.Name = "cmbHour";
            this.cmbHour.Size = new System.Drawing.Size(49, 21);
            this.cmbHour.TabIndex = 26;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(4, 49);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(30, 13);
            this.label16.TabIndex = 25;
            this.label16.Text = "Hour";
            // 
            // cmbMinute
            // 
            this.cmbMinute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMinute.FormattingEnabled = true;
            this.cmbMinute.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59"});
            this.cmbMinute.Location = new System.Drawing.Point(35, 72);
            this.cmbMinute.Name = "cmbMinute";
            this.cmbMinute.Size = new System.Drawing.Size(49, 21);
            this.cmbMinute.TabIndex = 24;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(4, 76);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(24, 13);
            this.label15.TabIndex = 23;
            this.label15.Text = "Min";
            // 
            // chkEnableTimerReset
            // 
            this.chkEnableTimerReset.AutoSize = true;
            this.chkEnableTimerReset.Location = new System.Drawing.Point(90, 76);
            this.chkEnableTimerReset.Name = "chkEnableTimerReset";
            this.chkEnableTimerReset.Size = new System.Drawing.Size(59, 17);
            this.chkEnableTimerReset.TabIndex = 22;
            this.chkEnableTimerReset.Text = "Enable";
            this.chkEnableTimerReset.UseVisualStyleBackColor = true;
            // 
            // btnGetTimerReset
            // 
            this.btnGetTimerReset.Location = new System.Drawing.Point(70, 19);
            this.btnGetTimerReset.Name = "btnGetTimerReset";
            this.btnGetTimerReset.Size = new System.Drawing.Size(57, 24);
            this.btnGetTimerReset.TabIndex = 21;
            this.btnGetTimerReset.Tag = "Button";
            this.btnGetTimerReset.Text = "Get";
            this.btnGetTimerReset.Click += new System.EventHandler(this.btnGetTimerReset_Click);
            // 
            // btnSetTimerReset
            // 
            this.btnSetTimerReset.Location = new System.Drawing.Point(7, 19);
            this.btnSetTimerReset.Name = "btnSetTimerReset";
            this.btnSetTimerReset.Size = new System.Drawing.Size(57, 24);
            this.btnSetTimerReset.TabIndex = 20;
            this.btnSetTimerReset.Tag = "Button";
            this.btnSetTimerReset.Text = "Set";
            this.btnSetTimerReset.Click += new System.EventHandler(this.btnSetTimerReset_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.chkPowerUpReset);
            this.groupBox6.Controls.Add(this.btnPowerUpResetGet);
            this.groupBox6.Controls.Add(this.btnPowerUpResetSet);
            this.groupBox6.Location = new System.Drawing.Point(450, 383);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(203, 43);
            this.groupBox6.TabIndex = 21;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Power Up Reset";
            // 
            // chkPowerUpReset
            // 
            this.chkPowerUpReset.AutoSize = true;
            this.chkPowerUpReset.Location = new System.Drawing.Point(132, 19);
            this.chkPowerUpReset.Name = "chkPowerUpReset";
            this.chkPowerUpReset.Size = new System.Drawing.Size(59, 17);
            this.chkPowerUpReset.TabIndex = 22;
            this.chkPowerUpReset.Text = "Enable";
            this.chkPowerUpReset.UseVisualStyleBackColor = true;
            // 
            // btnPowerUpResetGet
            // 
            this.btnPowerUpResetGet.Location = new System.Drawing.Point(69, 13);
            this.btnPowerUpResetGet.Name = "btnPowerUpResetGet";
            this.btnPowerUpResetGet.Size = new System.Drawing.Size(57, 24);
            this.btnPowerUpResetGet.TabIndex = 21;
            this.btnPowerUpResetGet.Tag = "Button";
            this.btnPowerUpResetGet.Text = "Get";
            this.btnPowerUpResetGet.Click += new System.EventHandler(this.btnPowerUpResetGet_Click);
            // 
            // btnPowerUpResetSet
            // 
            this.btnPowerUpResetSet.Location = new System.Drawing.Point(6, 13);
            this.btnPowerUpResetSet.Name = "btnPowerUpResetSet";
            this.btnPowerUpResetSet.Size = new System.Drawing.Size(57, 24);
            this.btnPowerUpResetSet.TabIndex = 20;
            this.btnPowerUpResetSet.Tag = "Button";
            this.btnPowerUpResetSet.Text = "Set";
            this.btnPowerUpResetSet.Click += new System.EventHandler(this.btnPowerUpResetSet_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.chkWatchDogReset);
            this.groupBox5.Controls.Add(this.btnGetWatchDogReset);
            this.groupBox5.Controls.Add(this.btnSetWatchDog);
            this.groupBox5.Location = new System.Drawing.Point(450, 436);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(203, 43);
            this.groupBox5.TabIndex = 21;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Watch Dog Reset";
            // 
            // chkWatchDogReset
            // 
            this.chkWatchDogReset.AutoSize = true;
            this.chkWatchDogReset.Location = new System.Drawing.Point(132, 19);
            this.chkWatchDogReset.Name = "chkWatchDogReset";
            this.chkWatchDogReset.Size = new System.Drawing.Size(59, 17);
            this.chkWatchDogReset.TabIndex = 22;
            this.chkWatchDogReset.Text = "Enable";
            this.chkWatchDogReset.UseVisualStyleBackColor = true;
            // 
            // btnGetWatchDogReset
            // 
            this.btnGetWatchDogReset.Location = new System.Drawing.Point(69, 13);
            this.btnGetWatchDogReset.Name = "btnGetWatchDogReset";
            this.btnGetWatchDogReset.Size = new System.Drawing.Size(57, 24);
            this.btnGetWatchDogReset.TabIndex = 21;
            this.btnGetWatchDogReset.Tag = "Button";
            this.btnGetWatchDogReset.Text = "Get";
            this.btnGetWatchDogReset.Click += new System.EventHandler(this.btnGetWatchDogReset_Click);
            // 
            // btnSetWatchDog
            // 
            this.btnSetWatchDog.Location = new System.Drawing.Point(6, 13);
            this.btnSetWatchDog.Name = "btnSetWatchDog";
            this.btnSetWatchDog.Size = new System.Drawing.Size(57, 24);
            this.btnSetWatchDog.TabIndex = 20;
            this.btnSetWatchDog.Tag = "Button";
            this.btnSetWatchDog.Text = "Set";
            this.btnSetWatchDog.Click += new System.EventHandler(this.btnSetWatchDog_Click);
            // 
            // gbCalibrationMode
            // 
            this.gbCalibrationMode.Controls.Add(this.btnCalibrationModeDeactive);
            this.gbCalibrationMode.Controls.Add(this.btnCalibrationModeSet);
            this.gbCalibrationMode.Location = new System.Drawing.Point(16, 280);
            this.gbCalibrationMode.Name = "gbCalibrationMode";
            this.gbCalibrationMode.Size = new System.Drawing.Size(179, 97);
            this.gbCalibrationMode.TabIndex = 20;
            this.gbCalibrationMode.TabStop = false;
            this.gbCalibrationMode.Text = "Calibration Mode";
            // 
            // btnCalibrationModeDeactive
            // 
            this.btnCalibrationModeDeactive.Location = new System.Drawing.Point(14, 59);
            this.btnCalibrationModeDeactive.Name = "btnCalibrationModeDeactive";
            this.btnCalibrationModeDeactive.Size = new System.Drawing.Size(153, 24);
            this.btnCalibrationModeDeactive.TabIndex = 19;
            this.btnCalibrationModeDeactive.Tag = "Button";
            this.btnCalibrationModeDeactive.Text = "Calibration Mode Deactive";
            this.btnCalibrationModeDeactive.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnCalibrationModeSet
            // 
            this.btnCalibrationModeSet.Location = new System.Drawing.Point(14, 27);
            this.btnCalibrationModeSet.Name = "btnCalibrationModeSet";
            this.btnCalibrationModeSet.Size = new System.Drawing.Size(153, 24);
            this.btnCalibrationModeSet.TabIndex = 18;
            this.btnCalibrationModeSet.Tag = "Button";
            this.btnCalibrationModeSet.Text = "Calibration Mode Active";
            this.btnCalibrationModeSet.Click += new System.EventHandler(this.btnCalibrationModeSet_Click);
            // 
            // gpDoorOpen
            // 
            this.gpDoorOpen.Controls.Add(this.btn_DoorOpenSet);
            this.gpDoorOpen.Location = new System.Drawing.Point(16, 207);
            this.gpDoorOpen.Name = "gpDoorOpen";
            this.gpDoorOpen.Size = new System.Drawing.Size(179, 73);
            this.gpDoorOpen.TabIndex = 19;
            this.gpDoorOpen.TabStop = false;
            this.gpDoorOpen.Text = "Door Open";
            // 
            // btn_DoorOpenSet
            // 
            this.btn_DoorOpenSet.Location = new System.Drawing.Point(14, 30);
            this.btn_DoorOpenSet.Name = "btn_DoorOpenSet";
            this.btn_DoorOpenSet.Size = new System.Drawing.Size(136, 24);
            this.btn_DoorOpenSet.TabIndex = 18;
            this.btn_DoorOpenSet.Tag = "Button";
            this.btn_DoorOpenSet.Text = "Door Open Set";
            this.btn_DoorOpenSet.Click += new System.EventHandler(this.btnDoorOpenSet_Click);
            // 
            // btnModemStatus
            // 
            this.btnModemStatus.Location = new System.Drawing.Point(16, 444);
            this.btnModemStatus.Name = "btnModemStatus";
            this.btnModemStatus.Size = new System.Drawing.Size(134, 24);
            this.btnModemStatus.TabIndex = 17;
            this.btnModemStatus.Tag = "Button";
            this.btnModemStatus.Text = "Get Modem Status";
            this.btnModemStatus.Click += new System.EventHandler(this.button6_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(16, 414);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(134, 24);
            this.btnClear.TabIndex = 17;
            this.btnClear.Tag = "Button";
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // rbHEX
            // 
            this.rbHEX.AutoSize = true;
            this.rbHEX.Checked = true;
            this.rbHEX.Location = new System.Drawing.Point(995, 38);
            this.rbHEX.Name = "rbHEX";
            this.rbHEX.Size = new System.Drawing.Size(77, 17);
            this.rbHEX.TabIndex = 16;
            this.rbHEX.TabStop = true;
            this.rbHEX.Text = "HEX String";
            this.rbHEX.UseVisualStyleBackColor = true;
            this.rbHEX.CheckedChanged += new System.EventHandler(this.rbHEX_CheckedChanged);
            // 
            // rbASCII
            // 
            this.rbASCII.AutoSize = true;
            this.rbASCII.Location = new System.Drawing.Point(995, 16);
            this.rbASCII.Name = "rbASCII";
            this.rbASCII.Size = new System.Drawing.Size(82, 17);
            this.rbASCII.TabIndex = 16;
            this.rbASCII.Text = "ASCII String";
            this.rbASCII.UseVisualStyleBackColor = true;
            this.rbASCII.CheckedChanged += new System.EventHandler(this.rbASCII_CheckedChanged);
            // 
            // txt_firmareInfo
            // 
            this.txt_firmareInfo.Location = new System.Drawing.Point(158, 383);
            this.txt_firmareInfo.Name = "txt_firmareInfo";
            this.txt_firmareInfo.Size = new System.Drawing.Size(288, 96);
            this.txt_firmareInfo.TabIndex = 15;
            this.txt_firmareInfo.Text = "";
            // 
            // txt_debugString
            // 
            this.txt_debugString.Location = new System.Drawing.Point(243, 16);
            this.txt_debugString.Multiline = true;
            this.txt_debugString.Name = "txt_debugString";
            this.txt_debugString.ReadOnly = true;
            this.txt_debugString.Size = new System.Drawing.Size(745, 280);
            this.txt_debugString.TabIndex = 0;
            // 
            // btn_getFirmwareInfo
            // 
            this.btn_getFirmwareInfo.Location = new System.Drawing.Point(16, 383);
            this.btn_getFirmwareInfo.Name = "btn_getFirmwareInfo";
            this.btn_getFirmwareInfo.Size = new System.Drawing.Size(134, 25);
            this.btn_getFirmwareInfo.TabIndex = 14;
            this.btn_getFirmwareInfo.Text = "Get Firmware Info";
            this.btn_getFirmwareInfo.UseVisualStyleBackColor = true;
            this.btn_getFirmwareInfo.Click += new System.EventHandler(this.btn_getFirmwareInfo_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txt_major_alarm_profile);
            this.groupBox4.Controls.Add(this.btn_Get_major_alram_counter);
            this.groupBox4.Controls.Add(this.btn_Set_major_alram_counter);
            this.groupBox4.Location = new System.Drawing.Point(14, 113);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(181, 93);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Major Alarm Counter";
            // 
            // txt_major_alarm_profile
            // 
            this.txt_major_alarm_profile.Location = new System.Drawing.Point(16, 36);
            this.txt_major_alarm_profile.Name = "txt_major_alarm_profile";
            this.txt_major_alarm_profile.Size = new System.Drawing.Size(153, 20);
            this.txt_major_alarm_profile.TabIndex = 11;
            this.txt_major_alarm_profile.Text = "0";
            // 
            // btn_Get_major_alram_counter
            // 
            this.btn_Get_major_alram_counter.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Get_major_alram_counter.Image = ((System.Drawing.Image)(resources.GetObject("btn_Get_major_alram_counter.Image")));
            this.btn_Get_major_alram_counter.Location = new System.Drawing.Point(16, 62);
            this.btn_Get_major_alram_counter.Name = "btn_Get_major_alram_counter";
            this.btn_Get_major_alram_counter.Size = new System.Drawing.Size(75, 25);
            this.btn_Get_major_alram_counter.TabIndex = 5;
            this.btn_Get_major_alram_counter.Tag = "Button";
            this.btn_Get_major_alram_counter.Text = "GET";
            this.btn_Get_major_alram_counter.Click += new System.EventHandler(this.btn_Get_major_alram_counter_Click_1);
            // 
            // btn_Set_major_alram_counter
            // 
            this.btn_Set_major_alram_counter.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Set_major_alram_counter.Image = ((System.Drawing.Image)(resources.GetObject("btn_Set_major_alram_counter.Image")));
            this.btn_Set_major_alram_counter.Location = new System.Drawing.Point(94, 62);
            this.btn_Set_major_alram_counter.Name = "btn_Set_major_alram_counter";
            this.btn_Set_major_alram_counter.Size = new System.Drawing.Size(75, 25);
            this.btn_Set_major_alram_counter.TabIndex = 5;
            this.btn_Set_major_alram_counter.Tag = "Button";
            this.btn_Set_major_alram_counter.Text = "SET";
            this.btn_Set_major_alram_counter.Click += new System.EventHandler(this.btn_Set_major_alram_counter_Click_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(155, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Length";
            // 
            // lbl_DebugStr_lenght
            // 
            this.lbl_DebugStr_lenght.AutoSize = true;
            this.lbl_DebugStr_lenght.Location = new System.Drawing.Point(208, 29);
            this.lbl_DebugStr_lenght.Name = "lbl_DebugStr_lenght";
            this.lbl_DebugStr_lenght.Size = new System.Drawing.Size(16, 13);
            this.lbl_DebugStr_lenght.TabIndex = 2;
            this.lbl_DebugStr_lenght.Text = "---";
            // 
            // btn_readCautions
            // 
            this.btn_readCautions.Location = new System.Drawing.Point(14, 78);
            this.btn_readCautions.Name = "btn_readCautions";
            this.btn_readCautions.Size = new System.Drawing.Size(136, 24);
            this.btn_readCautions.TabIndex = 1;
            this.btn_readCautions.Tag = "Button";
            this.btn_readCautions.Text = "Debug Read Caution";
            this.btn_readCautions.Click += new System.EventHandler(this.btn_readCautions_Click);
            // 
            // btn_readErrors
            // 
            this.btn_readErrors.Location = new System.Drawing.Point(14, 48);
            this.btn_readErrors.Name = "btn_readErrors";
            this.btn_readErrors.Size = new System.Drawing.Size(136, 24);
            this.btn_readErrors.TabIndex = 1;
            this.btn_readErrors.Tag = "Button";
            this.btn_readErrors.Text = "Debug Read Errors";
            this.btn_readErrors.Click += new System.EventHandler(this.btn_readErrors_Click);
            // 
            // btn_debugString
            // 
            this.btn_debugString.Location = new System.Drawing.Point(14, 18);
            this.btn_debugString.Name = "btn_debugString";
            this.btn_debugString.Size = new System.Drawing.Size(136, 24);
            this.btn_debugString.TabIndex = 1;
            this.btn_debugString.Tag = "Button";
            this.btn_debugString.Text = "Get Debug String";
            this.btn_debugString.Click += new System.EventHandler(this.btn_debugString_Click);
            // 
            // tab_debug
            // 
            this.tab_debug.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tab_debug.Controls.Add(this.btn_AddSelectedObject);
            this.tab_debug.Controls.Add(this.btn_RemoveSelectedObject);
            this.tab_debug.Controls.Add(this.btn_RemoveAllObject);
            this.tab_debug.Controls.Add(this.btn_AddAllObject);
            this.tab_debug.Controls.Add(this.btn_SetParams);
            this.tab_debug.Controls.Add(this.label6);
            this.tab_debug.Controls.Add(this.label5);
            this.tab_debug.Controls.Add(this.dgv_TestOutput);
            this.tab_debug.Controls.Add(this.txt_getCount);
            this.tab_debug.Controls.Add(this.txt_search);
            this.tab_debug.Controls.Add(this.btn_TL);
            this.tab_debug.Controls.Add(this.btn_T4);
            this.tab_debug.Controls.Add(this.btn_T3);
            this.tab_debug.Controls.Add(this.btn_T2);
            this.tab_debug.Controls.Add(this.btn_T1);
            this.tab_debug.Controls.Add(this.pb1);
            this.tab_debug.Controls.Add(this.btn_ClearSelectedList);
            this.tab_debug.Controls.Add(this.btn_ListAddAll);
            this.tab_debug.Controls.Add(this.btn_ListSearch);
            this.tab_debug.Controls.Add(this.list_Search);
            this.tab_debug.Controls.Add(this.lbl_totalItemsSelected);
            this.tab_debug.Controls.Add(this.label3);
            this.tab_debug.Controls.Add(this.label2);
            this.tab_debug.Controls.Add(this.label1);
            this.tab_debug.Controls.Add(this.btn_StopProcess);
            this.tab_debug.Controls.Add(this.btn_GetParams);
            this.tab_debug.Controls.Add(this.list_Selected);
            this.tab_debug.Controls.Add(this.list_possible);
            this.tab_debug.Location = new System.Drawing.Point(4, 22);
            this.tab_debug.Name = "tab_debug";
            this.tab_debug.Size = new System.Drawing.Size(1130, 495);
            this.tab_debug.TabIndex = 2;
            this.tab_debug.Text = "Debug";
            this.tab_debug.UseVisualStyleBackColor = true;
            // 
            // btn_SetParams
            // 
            this.btn_SetParams.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SetParams.Image = ((System.Drawing.Image)(resources.GetObject("btn_SetParams.Image")));
            this.btn_SetParams.Location = new System.Drawing.Point(684, 292);
            this.btn_SetParams.Name = "btn_SetParams";
            this.btn_SetParams.Size = new System.Drawing.Size(79, 25);
            this.btn_SetParams.TabIndex = 20;
            this.btn_SetParams.Tag = "Button";
            this.btn_SetParams.Text = "SET";
            this.btn_SetParams.Click += new System.EventHandler(this.btn_SetParams_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(800, 217);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 20);
            this.label6.TabIndex = 19;
            this.label6.Text = "Search Items";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(801, 264);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(181, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Enter OBIS Code or Name to search.";
            // 
            // dgv_TestOutput
            // 
            this.dgv_TestOutput.AllowUserToAddRows = false;
            this.dgv_TestOutput.AllowUserToDeleteRows = false;
            this.dgv_TestOutput.AllowUserToOrderColumns = true;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgv_TestOutput.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle17;
            this.dgv_TestOutput.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgv_TestOutput.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dgv_TestOutput.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgv_TestOutput.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_TestOutput.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dgv_TestOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_TestOutput.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dtTimeStamp,
            this.stOBISCode,
            this.ObjectLabel,
            this.OBISLabel,
            this.AttributeId,
            this.testStatus,
            this.Attribute_Value,
            this.testOutput});
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_TestOutput.DefaultCellStyle = dataGridViewCellStyle19;
            this.dgv_TestOutput.Location = new System.Drawing.Point(17, 14);
            this.dgv_TestOutput.Name = "dgv_TestOutput";
            this.dgv_TestOutput.ReadOnly = true;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_TestOutput.RowHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.dgv_TestOutput.RowHeadersWidth = 50;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_TestOutput.RowsDefaultCellStyle = dataGridViewCellStyle21;
            this.dgv_TestOutput.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_TestOutput.Size = new System.Drawing.Size(1062, 173);
            this.dgv_TestOutput.TabIndex = 16;
            this.dgv_TestOutput.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_TestOutput_CellClick);
            // 
            // dtTimeStamp
            // 
            this.dtTimeStamp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dtTimeStamp.DataPropertyName = "TimeStamp";
            this.dtTimeStamp.FillWeight = 10F;
            this.dtTimeStamp.HeaderText = "Time Stamp";
            this.dtTimeStamp.Name = "dtTimeStamp";
            this.dtTimeStamp.ReadOnly = true;
            // 
            // stOBISCode
            // 
            this.stOBISCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.stOBISCode.DataPropertyName = "OBIS_Code";
            this.stOBISCode.FillWeight = 10F;
            this.stOBISCode.HeaderText = "OBIS Code";
            this.stOBISCode.Name = "stOBISCode";
            this.stOBISCode.ReadOnly = true;
            // 
            // ObjectLabel
            // 
            this.ObjectLabel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ObjectLabel.DataPropertyName = "ObjectLabel";
            this.ObjectLabel.FillWeight = 10F;
            this.ObjectLabel.HeaderText = "Label";
            this.ObjectLabel.Name = "ObjectLabel";
            this.ObjectLabel.ReadOnly = true;
            // 
            // OBISLabel
            // 
            this.OBISLabel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.OBISLabel.DataPropertyName = "Label";
            this.OBISLabel.FillWeight = 15F;
            this.OBISLabel.HeaderText = "Attribute Label";
            this.OBISLabel.Name = "OBISLabel";
            this.OBISLabel.ReadOnly = true;
            // 
            // AttributeId
            // 
            this.AttributeId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AttributeId.DataPropertyName = "AttributeId";
            this.AttributeId.FillWeight = 5F;
            this.AttributeId.HeaderText = "Attribute Id";
            this.AttributeId.Name = "AttributeId";
            this.AttributeId.ReadOnly = true;
            // 
            // testStatus
            // 
            this.testStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.testStatus.DataPropertyName = "TestStatusSTR";
            this.testStatus.FillWeight = 10F;
            this.testStatus.HeaderText = "Test Status";
            this.testStatus.Name = "testStatus";
            this.testStatus.ReadOnly = true;
            this.testStatus.Width = 102;
            // 
            // Attribute_Value
            // 
            this.Attribute_Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Attribute_Value.DataPropertyName = "Attribute_Value";
            this.Attribute_Value.FillWeight = 25F;
            this.Attribute_Value.HeaderText = "Value";
            this.Attribute_Value.Name = "Attribute_Value";
            this.Attribute_Value.ReadOnly = true;
            // 
            // testOutput
            // 
            this.testOutput.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.testOutput.FillWeight = 5F;
            this.testOutput.HeaderText = "Test Output";
            this.testOutput.Name = "testOutput";
            this.testOutput.ReadOnly = true;
            this.testOutput.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.testOutput.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.testOutput.Text = "View Raw Data";
            this.testOutput.UseColumnTextForLinkValue = true;
            this.testOutput.Width = 103;
            // 
            // txt_getCount
            // 
            this.txt_getCount.Location = new System.Drawing.Point(684, 240);
            this.txt_getCount.MaxLength = 3;
            this.txt_getCount.Name = "txt_getCount";
            this.txt_getCount.Size = new System.Drawing.Size(79, 20);
            this.txt_getCount.TabIndex = 11;
            this.txt_getCount.Text = "1";
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(804, 240);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(275, 20);
            this.txt_search.TabIndex = 7;
            this.txt_search.TextChanged += new System.EventHandler(this.txt_search_TextChanged);
            // 
            // btn_TL
            // 
            this.btn_TL.Location = new System.Drawing.Point(684, 444);
            this.btn_TL.Name = "btn_TL";
            this.btn_TL.Size = new System.Drawing.Size(30, 23);
            this.btn_TL.TabIndex = 15;
            this.btn_TL.Text = "TL";
            this.btn_TL.UseVisualStyleBackColor = true;
            this.btn_TL.Click += new System.EventHandler(this.btn_TL_Click);
            // 
            // btn_T4
            // 
            this.btn_T4.Location = new System.Drawing.Point(733, 415);
            this.btn_T4.Name = "btn_T4";
            this.btn_T4.Size = new System.Drawing.Size(30, 23);
            this.btn_T4.TabIndex = 15;
            this.btn_T4.Text = "T4";
            this.btn_T4.UseVisualStyleBackColor = true;
            this.btn_T4.Click += new System.EventHandler(this.btn_T4_Click);
            // 
            // btn_T3
            // 
            this.btn_T3.Location = new System.Drawing.Point(684, 415);
            this.btn_T3.Name = "btn_T3";
            this.btn_T3.Size = new System.Drawing.Size(30, 23);
            this.btn_T3.TabIndex = 15;
            this.btn_T3.Text = "T3";
            this.btn_T3.UseVisualStyleBackColor = true;
            this.btn_T3.Click += new System.EventHandler(this.btn_T3_Click);
            // 
            // btn_T2
            // 
            this.btn_T2.Location = new System.Drawing.Point(734, 386);
            this.btn_T2.Name = "btn_T2";
            this.btn_T2.Size = new System.Drawing.Size(30, 23);
            this.btn_T2.TabIndex = 15;
            this.btn_T2.Text = "T2";
            this.btn_T2.UseVisualStyleBackColor = true;
            this.btn_T2.Click += new System.EventHandler(this.btn_T2_Click);
            // 
            // btn_T1
            // 
            this.btn_T1.Location = new System.Drawing.Point(684, 386);
            this.btn_T1.Name = "btn_T1";
            this.btn_T1.Size = new System.Drawing.Size(30, 23);
            this.btn_T1.TabIndex = 15;
            this.btn_T1.Text = "T1";
            this.btn_T1.UseVisualStyleBackColor = true;
            this.btn_T1.Click += new System.EventHandler(this.btn_T1_Click);
            // 
            // pb1
            // 
            this.pb1.Location = new System.Drawing.Point(804, 192);
            this.pb1.Name = "pb1";
            this.pb1.Size = new System.Drawing.Size(275, 23);
            this.pb1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pb1.TabIndex = 14;
            this.pb1.Visible = false;
            // 
            // btn_ClearSelectedList
            // 
            this.btn_ClearSelectedList.Image = ((System.Drawing.Image)(resources.GetObject("btn_ClearSelectedList.Image")));
            this.btn_ClearSelectedList.Location = new System.Drawing.Point(684, 352);
            this.btn_ClearSelectedList.Name = "btn_ClearSelectedList";
            this.btn_ClearSelectedList.Size = new System.Drawing.Size(79, 25);
            this.btn_ClearSelectedList.TabIndex = 10;
            this.btn_ClearSelectedList.Tag = "Button";
            this.btn_ClearSelectedList.Text = "Clear";
            this.btn_ClearSelectedList.Click += new System.EventHandler(this.btn_ClearSelectedList_Click);
            // 
            // btn_ListAddAll
            // 
            this.btn_ListAddAll.Enabled = false;
            this.btn_ListAddAll.Image = ((System.Drawing.Image)(resources.GetObject("btn_ListAddAll.Image")));
            this.btn_ListAddAll.Location = new System.Drawing.Point(1085, 444);
            this.btn_ListAddAll.Name = "btn_ListAddAll";
            this.btn_ListAddAll.Size = new System.Drawing.Size(91, 25);
            this.btn_ListAddAll.TabIndex = 9;
            this.btn_ListAddAll.Tag = "Button";
            this.btn_ListAddAll.Text = "Add All";
            this.btn_ListAddAll.Visible = false;
            this.btn_ListAddAll.Click += new System.EventHandler(this.btn_ListAddAll_Click);
            // 
            // btn_ListSearch
            // 
            this.btn_ListSearch.Enabled = false;
            this.btn_ListSearch.Image = ((System.Drawing.Image)(resources.GetObject("btn_ListSearch.Image")));
            this.btn_ListSearch.Location = new System.Drawing.Point(1085, 414);
            this.btn_ListSearch.Name = "btn_ListSearch";
            this.btn_ListSearch.Size = new System.Drawing.Size(91, 25);
            this.btn_ListSearch.TabIndex = 8;
            this.btn_ListSearch.Tag = "Button";
            this.btn_ListSearch.Text = "Clear";
            this.btn_ListSearch.Visible = false;
            this.btn_ListSearch.Click += new System.EventHandler(this.btn_ListSearch_Click);
            // 
            // list_Search
            // 
            this.list_Search.FormattingEnabled = true;
            this.list_Search.Location = new System.Drawing.Point(804, 281);
            this.list_Search.Name = "list_Search";
            this.list_Search.Size = new System.Drawing.Size(275, 199);
            this.list_Search.TabIndex = 6;
            this.list_Search.DoubleClick += new System.EventHandler(this.list_Search_DoubleClick);
            // 
            // lbl_totalItemsSelected
            // 
            this.lbl_totalItemsSelected.AutoSize = true;
            this.lbl_totalItemsSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_totalItemsSelected.Location = new System.Drawing.Point(770, 214);
            this.lbl_totalItemsSelected.Name = "lbl_totalItemsSelected";
            this.lbl_totalItemsSelected.Size = new System.Drawing.Size(23, 16);
            this.lbl_totalItemsSelected.TabIndex = 4;
            this.lbl_totalItemsSelected.Text = "---";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(654, 217);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Total Items Selected: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(362, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Selected Items";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Available Items";
            // 
            // btn_StopProcess
            // 
            this.btn_StopProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_StopProcess.Image = ((System.Drawing.Image)(resources.GetObject("btn_StopProcess.Image")));
            this.btn_StopProcess.Location = new System.Drawing.Point(684, 322);
            this.btn_StopProcess.Name = "btn_StopProcess";
            this.btn_StopProcess.Size = new System.Drawing.Size(79, 25);
            this.btn_StopProcess.TabIndex = 5;
            this.btn_StopProcess.Tag = "Button";
            this.btn_StopProcess.Text = "STOP";
            this.btn_StopProcess.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btn_GetParams
            // 
            this.btn_GetParams.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_GetParams.Image = ((System.Drawing.Image)(resources.GetObject("btn_GetParams.Image")));
            this.btn_GetParams.Location = new System.Drawing.Point(684, 263);
            this.btn_GetParams.Name = "btn_GetParams";
            this.btn_GetParams.Size = new System.Drawing.Size(79, 25);
            this.btn_GetParams.TabIndex = 5;
            this.btn_GetParams.Tag = "Button";
            this.btn_GetParams.Text = "GET";
            this.btn_GetParams.Click += new System.EventHandler(this.btn_GetParams_Click);
            // 
            // list_Selected
            // 
            this.list_Selected.FormattingEnabled = true;
            this.list_Selected.Location = new System.Drawing.Point(366, 216);
            this.list_Selected.Name = "list_Selected";
            this.list_Selected.Size = new System.Drawing.Size(275, 264);
            this.list_Selected.Sorted = true;
            this.list_Selected.TabIndex = 1;
            this.list_Selected.DoubleClick += new System.EventHandler(this.list_Selected_DoubleClick);
            // 
            // list_possible
            // 
            this.list_possible.FormattingEnabled = true;
            this.list_possible.Location = new System.Drawing.Point(17, 213);
            this.list_possible.Name = "list_possible";
            this.list_possible.Size = new System.Drawing.Size(275, 264);
            this.list_possible.Sorted = true;
            this.list_possible.TabIndex = 0;
            this.list_possible.Click += new System.EventHandler(this.list_possible_Click);
            this.list_possible.DoubleClick += new System.EventHandler(this.list_possible_DoubleClick);
            // 
            // tpObisCodes
            // 
            this.tpObisCodes.BackColor = System.Drawing.Color.Transparent;
            this.tpObisCodes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tpObisCodes.Controls.Add(this.txtIDecodedOutput);
            this.tpObisCodes.Controls.Add(this.groupBox3);
            this.tpObisCodes.Controls.Add(this.groupBox2);
            this.tpObisCodes.Controls.Add(this.groupBox1);
            this.tpObisCodes.Controls.Add(this.lbxObisCodes);
            this.tpObisCodes.Location = new System.Drawing.Point(4, 22);
            this.tpObisCodes.Name = "tpObisCodes";
            this.tpObisCodes.Padding = new System.Windows.Forms.Padding(3);
            this.tpObisCodes.Size = new System.Drawing.Size(1130, 495);
            this.tpObisCodes.TabIndex = 1;
            this.tpObisCodes.Text = "Total Objects";
            this.tpObisCodes.UseVisualStyleBackColor = true;
            // 
            // txtIDecodedOutput
            // 
            this.txtIDecodedOutput.Location = new System.Drawing.Point(692, 28);
            this.txtIDecodedOutput.MaxLength = 0;
            this.txtIDecodedOutput.Multiline = true;
            this.txtIDecodedOutput.Name = "txtIDecodedOutput";
            this.txtIDecodedOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtIDecodedOutput.Size = new System.Drawing.Size(245, 416);
            this.txtIDecodedOutput.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtIWroteResp);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.btnIWrtireAttrib);
            this.groupBox3.Controls.Add(this.rdbAscii);
            this.groupBox3.Controls.Add(this.rdbHex);
            this.groupBox3.Controls.Add(this.rdbDecimal);
            this.groupBox3.Controls.Add(this.txtIValue);
            this.groupBox3.Controls.Add(this.lblCapWriteResponse);
            this.groupBox3.Controls.Add(this.lblCapValue);
            this.groupBox3.Controls.Add(this.lclCapDatatype);
            this.groupBox3.Controls.Add(this.cbxDataTypesWR);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(943, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(187, 423);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Write Objects";
            this.groupBox3.Visible = false;
            // 
            // txtIWroteResp
            // 
            this.txtIWroteResp.Location = new System.Drawing.Point(10, 247);
            this.txtIWroteResp.Multiline = true;
            this.txtIWroteResp.Name = "txtIWroteResp";
            this.txtIWroteResp.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtIWroteResp.Size = new System.Drawing.Size(163, 140);
            this.txtIWroteResp.TabIndex = 5;
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(111, 394);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(70, 25);
            this.button2.TabIndex = 1;
            this.button2.Tag = "Button";
            this.button2.Text = "Clear";
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(7, 394);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 25);
            this.button1.TabIndex = 1;
            this.button1.Tag = "Button";
            this.button1.Text = "Copy to Clipboard";
            // 
            // btnIWrtireAttrib
            // 
            this.btnIWrtireAttrib.Image = ((System.Drawing.Image)(resources.GetObject("btnIWrtireAttrib.Image")));
            this.btnIWrtireAttrib.Location = new System.Drawing.Point(27, 178);
            this.btnIWrtireAttrib.Name = "btnIWrtireAttrib";
            this.btnIWrtireAttrib.Size = new System.Drawing.Size(120, 25);
            this.btnIWrtireAttrib.TabIndex = 4;
            this.btnIWrtireAttrib.Tag = "Button";
            this.btnIWrtireAttrib.Text = "Write Attribute";
            // 
            // rdbAscii
            // 
            this.rdbAscii.AutoSize = true;
            this.rdbAscii.Location = new System.Drawing.Point(126, 149);
            this.rdbAscii.Name = "rdbAscii";
            this.rdbAscii.Size = new System.Drawing.Size(47, 17);
            this.rdbAscii.TabIndex = 3;
            this.rdbAscii.Text = "Ascii";
            this.rdbAscii.UseVisualStyleBackColor = true;
            // 
            // rdbHex
            // 
            this.rdbHex.AutoSize = true;
            this.rdbHex.Checked = true;
            this.rdbHex.Location = new System.Drawing.Point(76, 149);
            this.rdbHex.Name = "rdbHex";
            this.rdbHex.Size = new System.Drawing.Size(44, 17);
            this.rdbHex.TabIndex = 3;
            this.rdbHex.TabStop = true;
            this.rdbHex.Text = "Hex";
            this.rdbHex.UseVisualStyleBackColor = true;
            // 
            // rdbDecimal
            // 
            this.rdbDecimal.AutoSize = true;
            this.rdbDecimal.Location = new System.Drawing.Point(7, 149);
            this.rdbDecimal.Name = "rdbDecimal";
            this.rdbDecimal.Size = new System.Drawing.Size(63, 17);
            this.rdbDecimal.TabIndex = 3;
            this.rdbDecimal.Text = "Decimal";
            this.rdbDecimal.UseVisualStyleBackColor = true;
            // 
            // txtIValue
            // 
            this.txtIValue.Location = new System.Drawing.Point(7, 106);
            this.txtIValue.Name = "txtIValue";
            this.txtIValue.Size = new System.Drawing.Size(166, 20);
            this.txtIValue.TabIndex = 2;
            // 
            // lblCapWriteResponse
            // 
            this.lblCapWriteResponse.AutoSize = true;
            this.lblCapWriteResponse.Location = new System.Drawing.Point(7, 231);
            this.lblCapWriteResponse.Name = "lblCapWriteResponse";
            this.lblCapWriteResponse.Size = new System.Drawing.Size(83, 13);
            this.lblCapWriteResponse.TabIndex = 1;
            this.lblCapWriteResponse.Text = "Write Response";
            // 
            // lblCapValue
            // 
            this.lblCapValue.AutoSize = true;
            this.lblCapValue.Location = new System.Drawing.Point(4, 89);
            this.lblCapValue.Name = "lblCapValue";
            this.lblCapValue.Size = new System.Drawing.Size(34, 13);
            this.lblCapValue.TabIndex = 1;
            this.lblCapValue.Text = "Value";
            // 
            // lclCapDatatype
            // 
            this.lclCapDatatype.AutoSize = true;
            this.lclCapDatatype.Location = new System.Drawing.Point(7, 33);
            this.lclCapDatatype.Name = "lclCapDatatype";
            this.lclCapDatatype.Size = new System.Drawing.Size(57, 13);
            this.lclCapDatatype.TabIndex = 1;
            this.lclCapDatatype.Text = "Data Type";
            // 
            // cbxDataTypesWR
            // 
            this.cbxDataTypesWR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDataTypesWR.FormattingEnabled = true;
            this.cbxDataTypesWR.Location = new System.Drawing.Point(7, 52);
            this.cbxDataTypesWR.Name = "cbxDataTypesWR";
            this.cbxDataTypesWR.Size = new System.Drawing.Size(178, 21);
            this.cbxDataTypesWR.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkEnableSelectiveAccess);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.gpSelectiveAccess);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.txtAttribute);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.btnIReadAttrib);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtClassId);
            this.groupBox2.Controls.Add(this.btnIClrDecodedOP);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.btnC2CDecodedOP);
            this.groupBox2.Controls.Add(this.txtC);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtA);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtB);
            this.groupBox2.Controls.Add(this.txtF);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtD);
            this.groupBox2.Controls.Add(this.txtE);
            this.groupBox2.Location = new System.Drawing.Point(428, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(258, 423);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Read Objects";
            // 
            // chkEnableSelectiveAccess
            // 
            this.chkEnableSelectiveAccess.AutoSize = true;
            this.chkEnableSelectiveAccess.Location = new System.Drawing.Point(145, 206);
            this.chkEnableSelectiveAccess.Name = "chkEnableSelectiveAccess";
            this.chkEnableSelectiveAccess.Size = new System.Drawing.Size(110, 17);
            this.chkEnableSelectiveAccess.TabIndex = 0;
            this.chkEnableSelectiveAccess.Text = "Enable Descriptor";
            this.chkEnableSelectiveAccess.UseVisualStyleBackColor = true;
            this.chkEnableSelectiveAccess.CheckedChanged += new System.EventHandler(this.chkEnableSelectiveAccess_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Green;
            this.label14.Location = new System.Drawing.Point(20, 163);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(78, 20);
            this.label14.TabIndex = 32;
            this.label14.Text = "Attribute :";
            this.label14.Click += new System.EventHandler(this.label12_Click);
            // 
            // gpSelectiveAccess
            // 
            this.gpSelectiveAccess.Controls.Add(this.tcAccessSelector);
            this.gpSelectiveAccess.Location = new System.Drawing.Point(5, 251);
            this.gpSelectiveAccess.Name = "gpSelectiveAccess";
            this.gpSelectiveAccess.Size = new System.Drawing.Size(245, 153);
            this.gpSelectiveAccess.TabIndex = 3;
            this.gpSelectiveAccess.TabStop = false;
            this.gpSelectiveAccess.Text = "Selective Access";
            // 
            // tcAccessSelector
            // 
            this.tcAccessSelector.Controls.Add(this.tpEntry);
            this.tcAccessSelector.Controls.Add(this.tpRange);
            this.tcAccessSelector.Location = new System.Drawing.Point(6, 11);
            this.tcAccessSelector.Name = "tcAccessSelector";
            this.tcAccessSelector.SelectedIndex = 0;
            this.tcAccessSelector.Size = new System.Drawing.Size(239, 131);
            this.tcAccessSelector.TabIndex = 33;
            // 
            // tpEntry
            // 
            this.tpEntry.Controls.Add(this.lblToSelectedVal);
            this.tpEntry.Controls.Add(this.lblFromEntry);
            this.tpEntry.Controls.Add(this.txtToSelectedVal);
            this.tpEntry.Controls.Add(this.txtFromEntry);
            this.tpEntry.Controls.Add(this.lblFromSelectedVal);
            this.tpEntry.Controls.Add(this.txtToEntry);
            this.tpEntry.Controls.Add(this.txtFromSelectedVal);
            this.tpEntry.Controls.Add(this.lblToEntry);
            this.tpEntry.Location = new System.Drawing.Point(4, 22);
            this.tpEntry.Name = "tpEntry";
            this.tpEntry.Padding = new System.Windows.Forms.Padding(3);
            this.tpEntry.Size = new System.Drawing.Size(231, 105);
            this.tpEntry.TabIndex = 0;
            this.tpEntry.Text = "Entry Descriptor";
            this.tpEntry.UseVisualStyleBackColor = true;
            // 
            // lblToSelectedVal
            // 
            this.lblToSelectedVal.AutoSize = true;
            this.lblToSelectedVal.Location = new System.Drawing.Point(6, 84);
            this.lblToSelectedVal.Name = "lblToSelectedVal";
            this.lblToSelectedVal.Size = new System.Drawing.Size(83, 13);
            this.lblToSelectedVal.TabIndex = 4;
            this.lblToSelectedVal.Text = "To Selected Val";
            // 
            // lblFromEntry
            // 
            this.lblFromEntry.AutoSize = true;
            this.lblFromEntry.Location = new System.Drawing.Point(6, 9);
            this.lblFromEntry.Name = "lblFromEntry";
            this.lblFromEntry.Size = new System.Drawing.Size(57, 13);
            this.lblFromEntry.TabIndex = 2;
            this.lblFromEntry.Text = "From Entry";
            // 
            // txtToSelectedVal
            // 
            this.txtToSelectedVal.Location = new System.Drawing.Point(104, 81);
            this.txtToSelectedVal.Name = "txtToSelectedVal";
            this.txtToSelectedVal.Size = new System.Drawing.Size(74, 20);
            this.txtToSelectedVal.TabIndex = 3;
            // 
            // txtFromEntry
            // 
            this.txtFromEntry.Location = new System.Drawing.Point(104, 6);
            this.txtFromEntry.Name = "txtFromEntry";
            this.txtFromEntry.Size = new System.Drawing.Size(74, 20);
            this.txtFromEntry.TabIndex = 1;
            // 
            // lblFromSelectedVal
            // 
            this.lblFromSelectedVal.AutoSize = true;
            this.lblFromSelectedVal.Location = new System.Drawing.Point(6, 59);
            this.lblFromSelectedVal.Name = "lblFromSelectedVal";
            this.lblFromSelectedVal.Size = new System.Drawing.Size(93, 13);
            this.lblFromSelectedVal.TabIndex = 4;
            this.lblFromSelectedVal.Text = "From Selected Val";
            // 
            // txtToEntry
            // 
            this.txtToEntry.Location = new System.Drawing.Point(104, 32);
            this.txtToEntry.Name = "txtToEntry";
            this.txtToEntry.Size = new System.Drawing.Size(74, 20);
            this.txtToEntry.TabIndex = 1;
            // 
            // txtFromSelectedVal
            // 
            this.txtFromSelectedVal.Location = new System.Drawing.Point(104, 56);
            this.txtFromSelectedVal.Name = "txtFromSelectedVal";
            this.txtFromSelectedVal.Size = new System.Drawing.Size(74, 20);
            this.txtFromSelectedVal.TabIndex = 3;
            // 
            // lblToEntry
            // 
            this.lblToEntry.AutoSize = true;
            this.lblToEntry.Location = new System.Drawing.Point(6, 35);
            this.lblToEntry.Name = "lblToEntry";
            this.lblToEntry.Size = new System.Drawing.Size(47, 13);
            this.lblToEntry.TabIndex = 2;
            this.lblToEntry.Text = "To Entry";
            // 
            // tpRange
            // 
            this.tpRange.Controls.Add(this.dtpFrom);
            this.tpRange.Controls.Add(this.lbl_fromTxt);
            this.tpRange.Controls.Add(this.dtpTo);
            this.tpRange.Controls.Add(this.lbl_ToTxt);
            this.tpRange.Location = new System.Drawing.Point(4, 22);
            this.tpRange.Name = "tpRange";
            this.tpRange.Padding = new System.Windows.Forms.Padding(3);
            this.tpRange.Size = new System.Drawing.Size(231, 105);
            this.tpRange.TabIndex = 1;
            this.tpRange.Text = "RangeDescriptor";
            this.tpRange.UseVisualStyleBackColor = true;
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd/MMM/yyyy HH:mm:ss";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(47, 26);
            this.dtpFrom.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(179, 20);
            this.dtpFrom.TabIndex = 20;
            // 
            // lbl_fromTxt
            // 
            this.lbl_fromTxt.AutoSize = true;
            this.lbl_fromTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbl_fromTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_fromTxt.Location = new System.Drawing.Point(5, 29);
            this.lbl_fromTxt.Margin = new System.Windows.Forms.Padding(5, 5, 5, 3);
            this.lbl_fromTxt.Name = "lbl_fromTxt";
            this.lbl_fromTxt.Size = new System.Drawing.Size(34, 13);
            this.lbl_fromTxt.TabIndex = 22;
            this.lbl_fromTxt.Text = "From";
            // 
            // dtpTo
            // 
            this.dtpTo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpTo.CustomFormat = "dd/MMM/yyyy HH:mm:ss";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(47, 59);
            this.dtpTo.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(179, 20);
            this.dtpTo.TabIndex = 19;
            // 
            // lbl_ToTxt
            // 
            this.lbl_ToTxt.AutoSize = true;
            this.lbl_ToTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbl_ToTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_ToTxt.Location = new System.Drawing.Point(14, 63);
            this.lbl_ToTxt.Margin = new System.Windows.Forms.Padding(25, 5, 5, 3);
            this.lbl_ToTxt.Name = "lbl_ToTxt";
            this.lbl_ToTxt.Size = new System.Drawing.Size(22, 13);
            this.lbl_ToTxt.TabIndex = 21;
            this.lbl_ToTxt.Text = "To";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Green;
            this.label13.Location = new System.Drawing.Point(207, 63);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(19, 20);
            this.label13.TabIndex = 32;
            this.label13.Text = "F";
            this.label13.Click += new System.EventHandler(this.label12_Click);
            // 
            // txtAttribute
            // 
            this.txtAttribute.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAttribute.Location = new System.Drawing.Point(104, 160);
            this.txtAttribute.MaxLength = 2;
            this.txtAttribute.Name = "txtAttribute";
            this.txtAttribute.Size = new System.Drawing.Size(70, 29);
            this.txtAttribute.TabIndex = 31;
            this.txtAttribute.Text = "0";
            this.txtAttribute.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Green;
            this.label7.Location = new System.Drawing.Point(24, 131);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 20);
            this.label7.TabIndex = 5;
            this.label7.Text = "Class Id :";
            // 
            // btnIReadAttrib
            // 
            this.btnIReadAttrib.Image = ((System.Drawing.Image)(resources.GetObject("btnIReadAttrib.Image")));
            this.btnIReadAttrib.Location = new System.Drawing.Point(7, 198);
            this.btnIReadAttrib.Name = "btnIReadAttrib";
            this.btnIReadAttrib.Size = new System.Drawing.Size(130, 25);
            this.btnIReadAttrib.TabIndex = 2;
            this.btnIReadAttrib.Tag = "Button";
            this.btnIReadAttrib.Text = "Read Attribute";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Green;
            this.label12.Location = new System.Drawing.Point(131, 63);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(20, 20);
            this.label12.TabIndex = 32;
            this.label12.Text = "E";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // txtClassId
            // 
            this.txtClassId.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClassId.Location = new System.Drawing.Point(104, 125);
            this.txtClassId.MaxLength = 4;
            this.txtClassId.Name = "txtClassId";
            this.txtClassId.Size = new System.Drawing.Size(70, 29);
            this.txtClassId.TabIndex = 30;
            this.txtClassId.Text = "0001";
            this.txtClassId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnIClrDecodedOP
            // 
            this.btnIClrDecodedOP.Image = ((System.Drawing.Image)(resources.GetObject("btnIClrDecodedOP.Image")));
            this.btnIClrDecodedOP.Location = new System.Drawing.Point(145, 227);
            this.btnIClrDecodedOP.Name = "btnIClrDecodedOP";
            this.btnIClrDecodedOP.Size = new System.Drawing.Size(100, 25);
            this.btnIClrDecodedOP.TabIndex = 1;
            this.btnIClrDecodedOP.Tag = "Button";
            this.btnIClrDecodedOP.Text = "Clear";
            this.btnIClrDecodedOP.Click += new System.EventHandler(this.btnIClrDecodedOP_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Green;
            this.label11.Location = new System.Drawing.Point(50, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(21, 20);
            this.label11.TabIndex = 32;
            this.label11.Text = "D";
            // 
            // btnC2CDecodedOP
            // 
            this.btnC2CDecodedOP.Image = ((System.Drawing.Image)(resources.GetObject("btnC2CDecodedOP.Image")));
            this.btnC2CDecodedOP.Location = new System.Drawing.Point(7, 227);
            this.btnC2CDecodedOP.Name = "btnC2CDecodedOP";
            this.btnC2CDecodedOP.Size = new System.Drawing.Size(130, 25);
            this.btnC2CDecodedOP.TabIndex = 1;
            this.btnC2CDecodedOP.Tag = "Button";
            this.btnC2CDecodedOP.Text = "Copy to Clipboard";
            this.btnC2CDecodedOP.Click += new System.EventHandler(this.btnC2CDecodedOP_Click);
            // 
            // txtC
            // 
            this.txtC.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtC.Location = new System.Drawing.Point(180, 34);
            this.txtC.MaxLength = 2;
            this.txtC.Name = "txtC";
            this.txtC.Size = new System.Drawing.Size(70, 29);
            this.txtC.TabIndex = 28;
            this.txtC.Text = "0B";
            this.txtC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Green;
            this.label10.Location = new System.Drawing.Point(207, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(20, 20);
            this.label10.TabIndex = 32;
            this.label10.Text = "C";
            // 
            // txtA
            // 
            this.txtA.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtA.Location = new System.Drawing.Point(28, 34);
            this.txtA.MaxLength = 2;
            this.txtA.Name = "txtA";
            this.txtA.Size = new System.Drawing.Size(70, 29);
            this.txtA.TabIndex = 28;
            this.txtA.Text = "01";
            this.txtA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Green;
            this.label9.Location = new System.Drawing.Point(131, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 20);
            this.label9.TabIndex = 32;
            this.label9.Text = "B";
            // 
            // txtB
            // 
            this.txtB.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtB.Location = new System.Drawing.Point(104, 34);
            this.txtB.MaxLength = 2;
            this.txtB.Name = "txtB";
            this.txtB.Size = new System.Drawing.Size(70, 29);
            this.txtB.TabIndex = 28;
            this.txtB.Text = "11";
            this.txtB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtF
            // 
            this.txtF.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtF.Location = new System.Drawing.Point(180, 85);
            this.txtF.MaxLength = 2;
            this.txtF.Name = "txtF";
            this.txtF.Size = new System.Drawing.Size(70, 29);
            this.txtF.TabIndex = 28;
            this.txtF.Text = "FF";
            this.txtF.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Green;
            this.label8.Location = new System.Drawing.Point(50, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 20);
            this.label8.TabIndex = 5;
            this.label8.Text = "A";
            // 
            // txtD
            // 
            this.txtD.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtD.Location = new System.Drawing.Point(28, 85);
            this.txtD.MaxLength = 2;
            this.txtD.Name = "txtD";
            this.txtD.Size = new System.Drawing.Size(70, 29);
            this.txtD.TabIndex = 28;
            this.txtD.Text = "24";
            this.txtD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtE
            // 
            this.txtE.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtE.Location = new System.Drawing.Point(104, 85);
            this.txtE.MaxLength = 2;
            this.txtE.Name = "txtE";
            this.txtE.Size = new System.Drawing.Size(70, 29);
            this.txtE.TabIndex = 28;
            this.txtE.Text = "00";
            this.txtE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flp_DebugBtn);
            this.groupBox1.Controls.Add(this.trvIObisCodes);
            this.groupBox1.Location = new System.Drawing.Point(26, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(396, 423);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Obis Codes List:";
            // 
            // flp_DebugBtn
            // 
            this.flp_DebugBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flp_DebugBtn.Controls.Add(this.btnReadObis);
            this.flp_DebugBtn.Controls.Add(this.btnObjectListoutput);
            this.flp_DebugBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flp_DebugBtn.Location = new System.Drawing.Point(3, 390);
            this.flp_DebugBtn.Name = "flp_DebugBtn";
            this.flp_DebugBtn.Size = new System.Drawing.Size(390, 30);
            this.flp_DebugBtn.TabIndex = 30;
            // 
            // btnReadObis
            // 
            this.btnReadObis.Image = ((System.Drawing.Image)(resources.GetObject("btnReadObis.Image")));
            this.btnReadObis.Location = new System.Drawing.Point(3, 3);
            this.btnReadObis.Name = "btnReadObis";
            this.btnReadObis.Size = new System.Drawing.Size(100, 25);
            this.btnReadObis.TabIndex = 26;
            this.btnReadObis.Tag = "Button";
            this.btnReadObis.Text = "Read List";
            // 
            // btnObjectListoutput
            // 
            this.btnObjectListoutput.Image = ((System.Drawing.Image)(resources.GetObject("btnObjectListoutput.Image")));
            this.btnObjectListoutput.Location = new System.Drawing.Point(109, 3);
            this.btnObjectListoutput.Name = "btnObjectListoutput";
            this.btnObjectListoutput.Size = new System.Drawing.Size(135, 25);
            this.btnObjectListoutput.TabIndex = 30;
            this.btnObjectListoutput.Tag = "Button";
            this.btnObjectListoutput.Text = "Copy to Clipboard";
            // 
            // trvIObisCodes
            // 
            this.trvIObisCodes.Location = new System.Drawing.Point(6, 19);
            this.trvIObisCodes.Name = "trvIObisCodes";
            this.trvIObisCodes.Size = new System.Drawing.Size(376, 368);
            this.trvIObisCodes.TabIndex = 27;
            // 
            // lbxObisCodes
            // 
            this.lbxObisCodes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxObisCodes.FormattingEnabled = true;
            this.lbxObisCodes.Location = new System.Drawing.Point(131, 58);
            this.lbxObisCodes.Name = "lbxObisCodes";
            this.lbxObisCodes.ScrollAlwaysVisible = true;
            this.lbxObisCodes.Size = new System.Drawing.Size(279, 368);
            this.lbxObisCodes.TabIndex = 24;
            this.lbxObisCodes.Visible = false;
            this.lbxObisCodes.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbxObisCodes_MouseDoubleClick);
            // 
            // tp1
            // 
            this.tp1.BackColor = System.Drawing.Color.Transparent;
            this.tp1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tp1.Controls.Add(this.btnIClearcomm);
            this.tp1.Controls.Add(this.btnIClearOutput);
            this.tp1.Controls.Add(this.btnIC2CComm);
            this.tp1.Controls.Add(this.btnIC2Coutput);
            this.tp1.Controls.Add(this.txtICommViewer);
            this.tp1.Controls.Add(this.txtOutPutWindow);
            this.tp1.Controls.Add(this.lblCapResponse);
            this.tp1.Controls.Add(this.lblCapOutput);
            this.tp1.Location = new System.Drawing.Point(4, 22);
            this.tp1.Name = "tp1";
            this.tp1.Padding = new System.Windows.Forms.Padding(3);
            this.tp1.Size = new System.Drawing.Size(1130, 495);
            this.tp1.TabIndex = 0;
            this.tp1.Text = "Internal Process";
            this.tp1.UseVisualStyleBackColor = true;
            this.tp1.Click += new System.EventHandler(this.tp1_Click);
            // 
            // btnIClearcomm
            // 
            this.btnIClearcomm.Image = ((System.Drawing.Image)(resources.GetObject("btnIClearcomm.Image")));
            this.btnIClearcomm.Location = new System.Drawing.Point(633, 445);
            this.btnIClearcomm.Name = "btnIClearcomm";
            this.btnIClearcomm.Size = new System.Drawing.Size(88, 30);
            this.btnIClearcomm.TabIndex = 31;
            this.btnIClearcomm.Tag = "Button";
            this.btnIClearcomm.Text = "Clear";
            this.btnIClearcomm.Click += new System.EventHandler(this.btnIClearcomm_Click);
            // 
            // btnIClearOutput
            // 
            this.btnIClearOutput.Image = ((System.Drawing.Image)(resources.GetObject("btnIClearOutput.Image")));
            this.btnIClearOutput.Location = new System.Drawing.Point(166, 445);
            this.btnIClearOutput.Name = "btnIClearOutput";
            this.btnIClearOutput.Size = new System.Drawing.Size(75, 30);
            this.btnIClearOutput.TabIndex = 31;
            this.btnIClearOutput.Tag = "Button";
            this.btnIClearOutput.Text = "Clear";
            // 
            // btnIC2CComm
            // 
            this.btnIC2CComm.Image = ((System.Drawing.Image)(resources.GetObject("btnIC2CComm.Image")));
            this.btnIC2CComm.Location = new System.Drawing.Point(477, 445);
            this.btnIC2CComm.Name = "btnIC2CComm";
            this.btnIC2CComm.Size = new System.Drawing.Size(150, 30);
            this.btnIC2CComm.TabIndex = 29;
            this.btnIC2CComm.Tag = "Button";
            this.btnIC2CComm.Text = "Copy to Clipboard";
            // 
            // btnIC2Coutput
            // 
            this.btnIC2Coutput.Image = ((System.Drawing.Image)(resources.GetObject("btnIC2Coutput.Image")));
            this.btnIC2Coutput.Location = new System.Drawing.Point(10, 445);
            this.btnIC2Coutput.Name = "btnIC2Coutput";
            this.btnIC2Coutput.Size = new System.Drawing.Size(150, 30);
            this.btnIC2Coutput.TabIndex = 29;
            this.btnIC2Coutput.Tag = "Button";
            this.btnIC2Coutput.Text = "Copy to Clipboard";
            // 
            // txtICommViewer
            // 
            this.txtICommViewer.Location = new System.Drawing.Point(477, 50);
            this.txtICommViewer.Multiline = true;
            this.txtICommViewer.Name = "txtICommViewer";
            this.txtICommViewer.ReadOnly = true;
            this.txtICommViewer.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtICommViewer.Size = new System.Drawing.Size(478, 389);
            this.txtICommViewer.TabIndex = 28;
            // 
            // txtOutPutWindow
            // 
            this.txtOutPutWindow.Location = new System.Drawing.Point(6, 50);
            this.txtOutPutWindow.MaxLength = 0;
            this.txtOutPutWindow.Multiline = true;
            this.txtOutPutWindow.Name = "txtOutPutWindow";
            this.txtOutPutWindow.ReadOnly = true;
            this.txtOutPutWindow.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutPutWindow.Size = new System.Drawing.Size(464, 389);
            this.txtOutPutWindow.TabIndex = 0;
            // 
            // lblCapResponse
            // 
            this.lblCapResponse.AutoSize = true;
            this.lblCapResponse.Location = new System.Drawing.Point(474, 31);
            this.lblCapResponse.Name = "lblCapResponse";
            this.lblCapResponse.Size = new System.Drawing.Size(103, 13);
            this.lblCapResponse.TabIndex = 27;
            this.lblCapResponse.Text = "Communication Log:";
            // 
            // lblCapOutput
            // 
            this.lblCapOutput.AutoSize = true;
            this.lblCapOutput.Location = new System.Drawing.Point(7, 31);
            this.lblCapOutput.Name = "lblCapOutput";
            this.lblCapOutput.Size = new System.Drawing.Size(84, 13);
            this.lblCapOutput.TabIndex = 1;
            this.lblCapOutput.Text = "Output Window:";
            // 
            // tcontrolMain
            // 
            this.tcontrolMain.Controls.Add(this.tp1);
            this.tcontrolMain.Controls.Add(this.tpObisCodes);
            this.tcontrolMain.Controls.Add(this.tab_debug);
            this.tcontrolMain.Controls.Add(this.tabPage4);
            this.tcontrolMain.Controls.Add(this.Raw_Data_Viewer);
            this.tcontrolMain.Controls.Add(this.tpIOStates);
            this.tcontrolMain.Location = new System.Drawing.Point(3, 3);
            this.tcontrolMain.Name = "tcontrolMain";
            this.tcontrolMain.SelectedIndex = 0;
            this.tcontrolMain.Size = new System.Drawing.Size(1138, 521);
            this.tcontrolMain.TabIndex = 1;
            // 
            // tpIOStates
            // 
            this.tpIOStates.Controls.Add(this.panel2);
            this.tpIOStates.Controls.Add(this.panel1);
            this.tpIOStates.Location = new System.Drawing.Point(4, 22);
            this.tpIOStates.Name = "tpIOStates";
            this.tpIOStates.Padding = new System.Windows.Forms.Padding(3);
            this.tpIOStates.Size = new System.Drawing.Size(1130, 495);
            this.tpIOStates.TabIndex = 9;
            this.tpIOStates.Text = "IO States";
            this.tpIOStates.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 59);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1124, 433);
            this.panel2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle22;
            this.dataGridView1.ColumnHeadersHeight = 50;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.srno,
            this.name,
            this.pin_no,
            this.port,
            this.in_sys,
            this.in_act,
            this.in_low,
            this.out_sys,
            this.out_act,
            this.out_low,
            this.dir_sys,
            this.dir_act,
            this.dir_low,
            this.sel_sys,
            this.sel_act,
            this.sel_low,
            this.ren});
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle24;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1066, 433);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.Paint += new System.Windows.Forms.PaintEventHandler(this.dataGridView1_Paint);
            // 
            // srno
            // 
            this.srno.DataPropertyName = "SrNo";
            this.srno.HeaderText = "SR#";
            this.srno.Name = "srno";
            this.srno.Width = 50;
            // 
            // name
            // 
            this.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.name.DataPropertyName = "Name";
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.DefaultCellStyle = dataGridViewCellStyle23;
            this.name.HeaderText = "Name";
            this.name.Name = "name";
            // 
            // pin_no
            // 
            this.pin_no.DataPropertyName = "PIN_NO";
            this.pin_no.HeaderText = "PIN#";
            this.pin_no.Name = "pin_no";
            this.pin_no.Width = 50;
            // 
            // port
            // 
            this.port.DataPropertyName = "PORT";
            this.port.HeaderText = "PORT";
            this.port.Name = "port";
            this.port.Width = 50;
            // 
            // in_sys
            // 
            this.in_sys.DataPropertyName = "IN.SystemResetState";
            this.in_sys.HeaderText = "SYS";
            this.in_sys.Name = "in_sys";
            this.in_sys.Width = 50;
            // 
            // in_act
            // 
            this.in_act.DataPropertyName = "IN.ActivePowerMode";
            this.in_act.HeaderText = "ACT";
            this.in_act.Name = "in_act";
            this.in_act.Width = 50;
            // 
            // in_low
            // 
            this.in_low.DataPropertyName = "IN.LowPowerMode";
            this.in_low.HeaderText = "LOW";
            this.in_low.Name = "in_low";
            this.in_low.Width = 50;
            // 
            // out_sys
            // 
            this.out_sys.DataPropertyName = "OUT.SystemResetState";
            this.out_sys.HeaderText = "SYS";
            this.out_sys.Name = "out_sys";
            this.out_sys.Width = 50;
            // 
            // out_act
            // 
            this.out_act.DataPropertyName = "OUT.ActivePowerMode";
            this.out_act.HeaderText = "ACT";
            this.out_act.Name = "out_act";
            this.out_act.Width = 50;
            // 
            // out_low
            // 
            this.out_low.DataPropertyName = "OUT.LowPowerMode";
            this.out_low.HeaderText = "LOW";
            this.out_low.Name = "out_low";
            this.out_low.Width = 50;
            // 
            // dir_sys
            // 
            this.dir_sys.DataPropertyName = "Direction.SystemResetState";
            this.dir_sys.HeaderText = "SYS";
            this.dir_sys.Name = "dir_sys";
            this.dir_sys.Width = 70;
            // 
            // dir_act
            // 
            this.dir_act.DataPropertyName = "Direction.ActivePowerMode";
            this.dir_act.HeaderText = "ACT";
            this.dir_act.Name = "dir_act";
            this.dir_act.Width = 70;
            // 
            // dir_low
            // 
            this.dir_low.DataPropertyName = "Direction.LowPowerMode";
            this.dir_low.HeaderText = "LOW";
            this.dir_low.Name = "dir_low";
            this.dir_low.Width = 70;
            // 
            // sel_sys
            // 
            this.sel_sys.DataPropertyName = "Selection.SystemResetState";
            this.sel_sys.HeaderText = "SYS";
            this.sel_sys.Name = "sel_sys";
            this.sel_sys.Width = 50;
            // 
            // sel_act
            // 
            this.sel_act.DataPropertyName = "Selection.ActivePowerMode";
            this.sel_act.HeaderText = "ACT";
            this.sel_act.Name = "sel_act";
            this.sel_act.Width = 50;
            // 
            // sel_low
            // 
            this.sel_low.DataPropertyName = "Selection.LowPowerMode";
            this.sel_low.HeaderText = "LOW";
            this.sel_low.Name = "sel_low";
            this.sel_low.Width = 50;
            // 
            // ren
            // 
            this.ren.DataPropertyName = "ResisterEnable.SystemResetState";
            this.ren.HeaderText = "REN";
            this.ren.Name = "ren";
            this.ren.Width = 50;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnreadState);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1124, 56);
            this.panel1.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(955, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(111, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnreadState
            // 
            this.btnreadState.Location = new System.Drawing.Point(955, 27);
            this.btnreadState.Name = "btnreadState";
            this.btnreadState.Size = new System.Drawing.Size(111, 23);
            this.btnreadState.TabIndex = 1;
            this.btnreadState.Text = "Read IO State";
            this.btnreadState.UseVisualStyleBackColor = true;
            this.btnreadState.Click += new System.EventHandler(this.btnreadState_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(257, 1);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(435, 55);
            this.label17.TabIndex = 0;
            this.label17.Text = "FUSION IO STATE";
            // 
            // bgw_DebugTest
            // 
            this.bgw_DebugTest.WorkerReportsProgress = true;
            this.bgw_DebugTest.WorkerSupportsCancellation = true;
            this.bgw_DebugTest.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_DebugTest_DoWork);
            this.bgw_DebugTest.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgw_DebugTest_ProgressChanged);
            this.bgw_DebugTest.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_DebugTest_RunWorkerCompleted);
            // 
            // ds_CB_Day_Record
            // 
            this.ds_CB_Day_Record.DataSetName = "ds_CB_Day_Record";
            this.ds_CB_Day_Record.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dsCBDayRecordBindingSource
            // 
            this.dsCBDayRecordBindingSource.DataSource = this.ds_CB_Day_Record;
            this.dsCBDayRecordBindingSource.Position = 0;
            // 
            // pnlDebugging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tcontrolMain);
            this.Name = "pnlDebugging";
            this.Size = new System.Drawing.Size(1350, 650);
            this.Load += new System.EventHandler(this.pnlDebugging_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.Raw_Data_Viewer.ResumeLayout(false);
            this.Raw_Data_Viewer.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.gp_Mem_Ref.ResumeLayout(false);
            this.gp_Mem_Ref.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.gbCalibrationMode.ResumeLayout(false);
            this.gpDoorOpen.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tab_debug.ResumeLayout(false);
            this.tab_debug.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_TestOutput)).EndInit();
            this.tpObisCodes.ResumeLayout(false);
            this.tpObisCodes.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gpSelectiveAccess.ResumeLayout(false);
            this.tcAccessSelector.ResumeLayout(false);
            this.tpEntry.ResumeLayout(false);
            this.tpEntry.PerformLayout();
            this.tpRange.ResumeLayout(false);
            this.tpRange.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.flp_DebugBtn.ResumeLayout(false);
            this.tp1.ResumeLayout(false);
            this.tp1.PerformLayout();
            this.tcontrolMain.ResumeLayout(false);
            this.tpIOStates.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ds_CB_Day_Record)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCBDayRecordBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblConnectionStatus;
        private System.Windows.Forms.Label lblCapConnectionStatus;
        private System.Windows.Forms.Label lblHeading1;
        private System.Windows.Forms.Timer timer_testing;
        // private System.Windows.Forms.Button btn_debugString;
        // private System.Windows.Forms.Button btn_ClearGrid;

        //private System.Windows.Forms.Button btn_ClearSelectedList;
        //private System.Windows.Forms.Button btn_ListAddAll;
        //private System.Windows.Forms.Button btn_ListSearch;
        //    private System.Windows.Forms.Button button3;
        //private System.Windows.Forms.Button btn_GetParams;
        //private System.Windows.Forms.Button button2;
        //private System.Windows.Forms.Button button1;
        //public System.Windows.Forms.Button btnIWrtireAttrib;

        //public System.Windows.Forms.Button btnIReadAttrib;
        //private System.Windows.Forms.Button btnIClrDecodedOP;
        //private System.Windows.Forms.Button btnC2CDecodedOP;
        //  private System.Windows.Forms.Button btnReadObis;

        //private System.Windows.Forms.Button btnIClearcomm;
        //private System.Windows.Forms.Button btnIClearOutput;
        //private System.Windows.Forms.Button btnIC2CComm;
        //private System.Windows.Forms.Button btnIC2Coutput;
        private System.ComponentModel.BackgroundWorker bgw_GETAlarmCounter;
        private ErrorProvider errorProvider;
        private ToolTip toolTip;
        private System.ComponentModel.BackgroundWorker bgw;
        private TabControl tcontrolMain;
        private TabPage tp1;
        private Button btnIClearcomm;
        private Button btnIClearOutput;
        private Button btnIC2CComm;
        private Button btnIC2Coutput;
        private TextBox txtICommViewer;
        private TextBox txtOutPutWindow;
        private Label lblCapResponse;
        private Label lblCapOutput;
        private TabPage tpObisCodes;
        public TextBox txtIDecodedOutput;
        private GroupBox groupBox3;
        public TextBox txtIWroteResp;
        private Button button2;
        private Button button1;
        private Button btnIWrtireAttrib;
        public RadioButton rdbAscii;
        public RadioButton rdbHex;
        public RadioButton rdbDecimal;
        public TextBox txtIValue;
        private Label lblCapWriteResponse;
        private Label lblCapValue;
        private Label lclCapDatatype;
        public ComboBox cbxDataTypesWR;
        private GroupBox groupBox2;
        private Label label14;
        private GroupBox gpSelectiveAccess;
        private Label lblToSelectedVal;
        internal TextBox txtToSelectedVal;
        private Label lblFromSelectedVal;
        internal TextBox txtFromSelectedVal;
        private Label lblToEntry;
        private Label lblFromEntry;
        internal TextBox txtToEntry;
        internal TextBox txtFromEntry;
        private Label label13;
        internal TextBox txtAttribute;
        private Label label7;
        public Button btnIReadAttrib;
        private Label label12;
        internal TextBox txtClassId;
        private Button btnIClrDecodedOP;
        private Label label11;
        private Button btnC2CDecodedOP;
        internal TextBox txtC;
        private Label label10;
        internal TextBox txtA;
        private Label label9;
        internal TextBox txtB;
        internal TextBox txtF;
        private Label label8;
        internal TextBox txtD;
        internal TextBox txtE;
        private GroupBox groupBox1;
        private FlowLayoutPanel flp_DebugBtn;
        private Button btnReadObis;
        public Button btnObjectListoutput;
        private TreeView trvIObisCodes;
        private ListBox lbxObisCodes;
        private TabPage tab_debug;
        private Button btn_AddSelectedObject;
        private Button btn_RemoveSelectedObject;
        private Button btn_RemoveAllObject;
        private Button btn_AddAllObject;
        private Button btn_SetParams;
        private Label label6;
        private Label label5;
        private TextBox txt_getCount;
        private TextBox txt_search;
        private Button btn_TL;
        private Button btn_T4;
        private Button btn_T3;
        private Button btn_T2;
        private Button btn_T1;
        private ProgressBar pb1;
        private Button btn_ClearSelectedList;
        private Button btn_ListAddAll;
        private Button btn_ListSearch;
        private ListBox list_Search;
        private Label lbl_totalItemsSelected;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button btn_StopProcess;
        private Button btn_GetParams;
        private ListBox list_Selected;
        private ListBox list_possible;
        private TabPage tabPage4;
        private Button btnModemStatus;
        private Button btnClear;
        private RadioButton rbHEX;
        private RadioButton rbASCII;
        private RichTextBox txt_firmareInfo;
        private TextBox txt_debugString;
        private Button btn_getFirmwareInfo;
        private GroupBox groupBox4;
        private TextBox txt_major_alarm_profile;
        private Button btn_Get_major_alram_counter;
        private Button btn_Set_major_alram_counter;
        private Label label4;
        private Label lbl_DebugStr_lenght;
        private Button btn_readCautions;
        private Button btn_readErrors;
        private Button btn_debugString;
        private TabPage Raw_Data_Viewer;
        private ProgressBar progressBar;
        private CheckBox chk_EditorFormat;
        private RichTextBox rtb_RAW_Data;
        private Button btn_Clear_Editor_;
        private Button btn_copy_Clip_Board;
        private System.ComponentModel.BackgroundWorker bgw_DebugTest;
        private DataGridViewTextBoxColumn dtTimeStamp;
        private DataGridViewTextBoxColumn stOBISCode;
        private DataGridViewTextBoxColumn ObjectLabel;
        private DataGridViewTextBoxColumn OBISLabel;
        private DataGridViewTextBoxColumn AttributeId;
        private DataGridViewTextBoxColumn testStatus;
        private DataGridViewTextBoxColumn Attribute_Value;
        private DataGridViewLinkColumn testOutput;
        private Button btn_DoorOpenSet;
        private GroupBox gpDoorOpen;
        private GroupBox gbCalibrationMode;
        private Button btnCalibrationModeSet;
        private Button btnCalibrationModeDeactive;
        private GroupBox groupBox6;
        private CheckBox chkPowerUpReset;
        private Button btnPowerUpResetGet;
        private Button btnPowerUpResetSet;
        private GroupBox groupBox5;
        private CheckBox chkWatchDogReset;
        private Button btnGetWatchDogReset;
        private Button btnSetWatchDog;
        private GroupBox groupBox7;
        private Label label15;
        private CheckBox chkEnableTimerReset;
        private Button btnGetTimerReset;
        private Button btnSetTimerReset;
        private ComboBox cmbHour;
        private Label label16;
        private ComboBox cmbMinute;
        private TabPage tpIOStates;
        private Panel panel2;
        private DataGridView dataGridView1;
        private Panel panel1;
        private Label label17;
        private Button btnreadState;
        private ds_CB_Day_Record ds_CB_Day_Record;
        private BindingSource dsCBDayRecordBindingSource;
        private Button btnSave;
        private DataGridViewTextBoxColumn srno;
        private DataGridViewTextBoxColumn name;
        private DataGridViewTextBoxColumn pin_no;
        private DataGridViewTextBoxColumn port;
        private DataGridViewTextBoxColumn in_sys;
        private DataGridViewTextBoxColumn in_act;
        private DataGridViewTextBoxColumn in_low;
        private DataGridViewTextBoxColumn out_sys;
        private DataGridViewTextBoxColumn out_act;
        private DataGridViewTextBoxColumn out_low;
        private DataGridViewTextBoxColumn dir_sys;
        private DataGridViewTextBoxColumn dir_act;
        private DataGridViewTextBoxColumn dir_low;
        private DataGridViewTextBoxColumn sel_sys;
        private DataGridViewTextBoxColumn sel_act;
        private DataGridViewTextBoxColumn sel_low;
        private DataGridViewTextBoxColumn ren;
        private Label lbl_fromTxt;
        private Label lbl_ToTxt;
        public DateTimePicker dtpFrom;
        public DateTimePicker dtpTo;
        public TabControl tcAccessSelector;
        internal CheckBox chkEnableSelectiveAccess;
        public TabPage tpEntry;
        public TabPage tpRange;
        private GroupBox groupBox8;
        private Button btnGetHDLCAddress;
        private Button btnSetHDLCAddress;
        private ComboBox cmpProtocol;
        private GroupBox groupBox9;
        private CheckBox chkICCGate;
        private Button btnGetIccGate;
        private Button btnSetIccGate;
        private GroupBox gp_Mem_Ref;
        private TextBox txt_TAG;
        private Label lbl_TAG;
        private Label lbl_Length;
        private Label lbl_Address;
        private Label lbl_Mem_Module;
        private TextBox txt_Length;
        private TextBox txt_BaseAddress;
        private ComboBox cmb_EMP_No;
        private Button btn_Set_CurrentMemRef;
        private Button btn_Add_MemRef;
        private Button btn_Import_File;
        private CheckBox chk_ALL;
        private Button btn_Export_File;
        private Button btn_GET_CurrentMemRef;
        private Button btn_Clear_All;
        private CheckedListBox chk_Mem_References;
        private Button btn_Read_Memory_MAP;
        private Button btn_GET_RawData;
        private GroupBox groupBox10;
        private Button btnSavePackets;
        private Label label24;
        private Label lblEpNo;
        private Label label22;
        private Label lblTotalChunks;
        private Label label20;
        private Label lblCurrentChunk;
        private Label label18;
        private Label lblRemainingChunks;
        private GroupBox groupBox11;
        
    }
}
