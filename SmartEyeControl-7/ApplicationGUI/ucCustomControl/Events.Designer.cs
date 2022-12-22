using System.Windows.Forms;
namespace ucCustomControl
{
    partial class pnlEvents
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pnlEvents));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tb_Events = new System.Windows.Forms.TabControl();
            this.tab_LogBook = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkReadByDateTime = new System.Windows.Forms.CheckBox();
            this.btn_ReadLogBook = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.grid_LogBook = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.Individual_Events = new System.Windows.Forms.TabPage();
            this.Meter_Events = new System.Windows.Forms.TabPage();
            this.pnl_Event_Container = new System.Windows.Forms.Panel();
            this.grid_Events_Counters = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.EventsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventsCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventsCounter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grid_Events_Log = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.Date_Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventCounter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventDetails = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gp_EventData = new System.Windows.Forms.GroupBox();
            this.check_SelectAllEvents = new System.Windows.Forms.CheckBox();
            this.lblEventsFilter = new System.Windows.Forms.Label();
            this.radio_Events_CountersOnly = new System.Windows.Forms.RadioButton();
            this.list_Event_SelectableEvents = new System.Windows.Forms.CheckedListBox();
            this.radio_Events_CompleteLog = new System.Windows.Forms.RadioButton();
            this.combo_EventFilters = new System.Windows.Forms.ComboBox();
            this.check_E_addToDB = new System.Windows.Forms.CheckBox();
            this.lbl_Status = new System.Windows.Forms.Label();
            this.lbl_comboEvents = new System.Windows.Forms.Label();
            this.combo_Events_SelectedITems = new System.Windows.Forms.ComboBox();
            this.lbl_eventDetails = new System.Windows.Forms.Label();
            this.ProgramEvents = new System.Windows.Forms.TabPage();
            this.btn_SaveAlarm = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_LoadAlarm = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.check_resetAll_Alarms = new System.Windows.Forms.CheckBox();
            this.grid_Events = new System.Windows.Forms.DataGridView();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Progress_Bar = new System.Windows.Forms.ProgressBar();
            this.btn_GetMajorAlarms = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_SetMajorAlarm = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_GET_EventsCautions = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_SET_EventsCautions = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_Events_GET = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.bgw_TBE1_Set = new System.ComponentModel.BackgroundWorker();
            this.bgw_TBE1_Get = new System.ComponentModel.BackgroundWorker();
            this.bgw_TBE2_Set = new System.ComponentModel.BackgroundWorker();
            this.bgw_TBE2_Get = new System.ComponentModel.BackgroundWorker();
            this.btn_events_report = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_SetMajorStatus = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.bgw_SetMajorAlarms = new System.ComponentModel.BackgroundWorker();
            this.bgw_GetMajorAlarms = new System.ComponentModel.BackgroundWorker();
            this.bgw_SetAlarmStatus = new System.ComponentModel.BackgroundWorker();
            this.bgw_getEventCautions = new System.ComponentModel.BackgroundWorker();
            this.bgw_SetEventCautions = new System.ComponentModel.BackgroundWorker();
            this.btn_SecurityReport = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_ReadSecurityData = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.fLP_MainButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.flp_MajorAlarmButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.lblHeading = new System.Windows.Forms.Label();
            this.DateTimes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventNames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventCodes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventCounters = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usDateRangeEv = new SmartEyeControl_7.ApplicationGUI.ucCustomControl.usDateRange();
            this.Event_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Event_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Caution_Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Is_Enable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Is_LogBook_Event = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Read_caution = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Display_Caution = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isFlash = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Flash_Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsMajorAlarm = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsTriggered = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResetAlarmStatus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tb_Events.SuspendLayout();
            this.tab_LogBook.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_LogBook)).BeginInit();
            this.Meter_Events.SuspendLayout();
            this.pnl_Event_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_Events_Counters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_Events_Log)).BeginInit();
            this.gp_EventData.SuspendLayout();
            this.ProgramEvents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_Events)).BeginInit();
            this.fLP_MainButtons.SuspendLayout();
            this.flp_MajorAlarmButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_Events
            // 
            this.tb_Events.Controls.Add(this.tab_LogBook);
            this.tb_Events.Controls.Add(this.Individual_Events);
            this.tb_Events.Controls.Add(this.Meter_Events);
            this.tb_Events.Controls.Add(this.ProgramEvents);
            this.tb_Events.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Events.Location = new System.Drawing.Point(3, 72);
            this.tb_Events.Name = "tb_Events";
            this.tb_Events.SelectedIndex = 0;
            this.tb_Events.Size = new System.Drawing.Size(1166, 578);
            this.tb_Events.TabIndex = 4;
            this.tb_Events.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tab_LogBook
            // 
            this.tab_LogBook.BackColor = System.Drawing.Color.Transparent;
            this.tab_LogBook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tab_LogBook.Controls.Add(this.groupBox1);
            this.tab_LogBook.Controls.Add(this.btn_ReadLogBook);
            this.tab_LogBook.Controls.Add(this.grid_LogBook);
            this.tab_LogBook.Location = new System.Drawing.Point(4, 23);
            this.tab_LogBook.Name = "tab_LogBook";
            this.tab_LogBook.Size = new System.Drawing.Size(1158, 551);
            this.tab_LogBook.TabIndex = 2;
            this.tab_LogBook.Text = "Log Book";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.usDateRangeEv);
            this.groupBox1.Controls.Add(this.chkReadByDateTime);
            this.groupBox1.Location = new System.Drawing.Point(715, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 124);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Date Time Settings";
            // 
            // chkReadByDateTime
            // 
            this.chkReadByDateTime.AutoSize = true;
            this.chkReadByDateTime.Location = new System.Drawing.Point(28, 25);
            this.chkReadByDateTime.Name = "chkReadByDateTime";
            this.chkReadByDateTime.Size = new System.Drawing.Size(122, 18);
            this.chkReadByDateTime.TabIndex = 14;
            this.chkReadByDateTime.Text = "Read By Datetime";
            this.chkReadByDateTime.UseVisualStyleBackColor = true;
            // 
            // btn_ReadLogBook
            // 
            this.btn_ReadLogBook.Location = new System.Drawing.Point(15, 13);
            this.btn_ReadLogBook.Name = "btn_ReadLogBook";
            this.btn_ReadLogBook.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_ReadLogBook.Size = new System.Drawing.Size(140, 30);
            this.btn_ReadLogBook.TabIndex = 8;
            this.btn_ReadLogBook.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_ReadLogBook.Values.Image")));
            this.btn_ReadLogBook.Values.Text = "Read Log Book";
            this.btn_ReadLogBook.Click += new System.EventHandler(this.btn_ReadLogBook_Click);
            // 
            // grid_LogBook
            // 
            this.grid_LogBook.AllowUserToAddRows = false;
            this.grid_LogBook.AllowUserToDeleteRows = false;
            this.grid_LogBook.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid_LogBook.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DateTimes,
            this.EventNames,
            this.EventCodes,
            this.EventCounters});
            this.grid_LogBook.Location = new System.Drawing.Point(15, 49);
            this.grid_LogBook.Name = "grid_LogBook";
            this.grid_LogBook.RowHeadersWidth = 50;
            this.grid_LogBook.Size = new System.Drawing.Size(694, 442);
            this.grid_LogBook.TabIndex = 7;
            // 
            // Individual_Events
            // 
            this.Individual_Events.BackColor = System.Drawing.SystemColors.Control;
            this.Individual_Events.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Individual_Events.Location = new System.Drawing.Point(4, 23);
            this.Individual_Events.Name = "Individual_Events";
            this.Individual_Events.Size = new System.Drawing.Size(1158, 551);
            this.Individual_Events.TabIndex = 3;
            this.Individual_Events.Text = "Individual Events Log";
            // 
            // Meter_Events
            // 
            this.Meter_Events.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Meter_Events.Controls.Add(this.pnl_Event_Container);
            this.Meter_Events.Controls.Add(this.lbl_eventDetails);
            this.Meter_Events.Location = new System.Drawing.Point(4, 23);
            this.Meter_Events.Name = "Meter_Events";
            this.Meter_Events.Padding = new System.Windows.Forms.Padding(3);
            this.Meter_Events.Size = new System.Drawing.Size(1158, 551);
            this.Meter_Events.TabIndex = 1;
            this.Meter_Events.Text = "Meter Event Counters ";
            this.Meter_Events.UseVisualStyleBackColor = true;
            // 
            // pnl_Event_Container
            // 
            this.pnl_Event_Container.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnl_Event_Container.BackColor = System.Drawing.SystemColors.Control;
            this.pnl_Event_Container.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_Event_Container.Controls.Add(this.grid_Events_Counters);
            this.pnl_Event_Container.Controls.Add(this.grid_Events_Log);
            this.pnl_Event_Container.Controls.Add(this.gp_EventData);
            this.pnl_Event_Container.Controls.Add(this.check_E_addToDB);
            this.pnl_Event_Container.Controls.Add(this.lbl_Status);
            this.pnl_Event_Container.Controls.Add(this.lbl_comboEvents);
            this.pnl_Event_Container.Controls.Add(this.combo_Events_SelectedITems);
            this.pnl_Event_Container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Event_Container.Location = new System.Drawing.Point(3, 3);
            this.pnl_Event_Container.Name = "pnl_Event_Container";
            this.pnl_Event_Container.Size = new System.Drawing.Size(1152, 545);
            this.pnl_Event_Container.TabIndex = 15;
            // 
            // grid_Events_Counters
            // 
            this.grid_Events_Counters.AllowUserToAddRows = false;
            this.grid_Events_Counters.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            this.grid_Events_Counters.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grid_Events_Counters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grid_Events_Counters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EventsName,
            this.EventsCode,
            this.EventsCounter});
            this.grid_Events_Counters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.grid_Events_Counters.Location = new System.Drawing.Point(313, 52);
            this.grid_Events_Counters.Name = "grid_Events_Counters";
            this.grid_Events_Counters.RowHeadersWidth = 50;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grid_Events_Counters.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.grid_Events_Counters.Size = new System.Drawing.Size(564, 372);
            this.grid_Events_Counters.StateNormal.Background.Color1 = System.Drawing.SystemColors.Control;
            this.grid_Events_Counters.TabIndex = 16;
            // 
            // EventsName
            // 
            this.EventsName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EventsName.FillWeight = 50.43147F;
            this.EventsName.HeaderText = "EventName";
            this.EventsName.Name = "EventsName";
            this.EventsName.ReadOnly = true;
            // 
            // EventsCode
            // 
            this.EventsCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EventsCode.FillWeight = 25.28426F;
            this.EventsCode.HeaderText = "EventCode";
            this.EventsCode.Name = "EventsCode";
            this.EventsCode.ReadOnly = true;
            // 
            // EventsCounter
            // 
            this.EventsCounter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EventsCounter.FillWeight = 25.28426F;
            this.EventsCounter.HeaderText = "EventCounter";
            this.EventsCounter.Name = "EventsCounter";
            this.EventsCounter.ReadOnly = true;
            // 
            // grid_Events_Log
            // 
            this.grid_Events_Log.AllowUserToAddRows = false;
            this.grid_Events_Log.AllowUserToDeleteRows = false;
            this.grid_Events_Log.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid_Events_Log.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date_Time,
            this.EventName,
            this.EventCode,
            this.EventCounter,
            this.EventDetails});
            this.grid_Events_Log.Location = new System.Drawing.Point(312, 49);
            this.grid_Events_Log.Name = "grid_Events_Log";
            this.grid_Events_Log.RowHeadersWidth = 50;
            this.grid_Events_Log.Size = new System.Drawing.Size(745, 372);
            this.grid_Events_Log.StateNormal.Background.Color1 = System.Drawing.SystemColors.Control;
            this.grid_Events_Log.TabIndex = 15;
            this.grid_Events_Log.Visible = false;
            // 
            // Date_Time
            // 
            this.Date_Time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Date_Time.HeaderText = "Date Time";
            this.Date_Time.Name = "Date_Time";
            this.Date_Time.ReadOnly = true;
            this.Date_Time.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // EventName
            // 
            this.EventName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.EventName.FillWeight = 150F;
            this.EventName.HeaderText = "Event Name";
            this.EventName.Name = "EventName";
            this.EventName.ReadOnly = true;
            this.EventName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.EventName.Width = 150;
            // 
            // EventCode
            // 
            this.EventCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EventCode.HeaderText = "Event Code";
            this.EventCode.Name = "EventCode";
            this.EventCode.ReadOnly = true;
            this.EventCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // EventCounter
            // 
            this.EventCounter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EventCounter.HeaderText = "Event Counter";
            this.EventCounter.Name = "EventCounter";
            this.EventCounter.ReadOnly = true;
            this.EventCounter.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // EventDetails
            // 
            this.EventDetails.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.EventDetails.FillWeight = 200F;
            this.EventDetails.HeaderText = "Event Details";
            this.EventDetails.Name = "EventDetails";
            this.EventDetails.ReadOnly = true;
            this.EventDetails.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.EventDetails.Width = 200;
            // 
            // gp_EventData
            // 
            this.gp_EventData.BackColor = System.Drawing.Color.Transparent;
            this.gp_EventData.Controls.Add(this.check_SelectAllEvents);
            this.gp_EventData.Controls.Add(this.lblEventsFilter);
            this.gp_EventData.Controls.Add(this.radio_Events_CountersOnly);
            this.gp_EventData.Controls.Add(this.list_Event_SelectableEvents);
            this.gp_EventData.Controls.Add(this.radio_Events_CompleteLog);
            this.gp_EventData.Controls.Add(this.combo_EventFilters);
            this.gp_EventData.Location = new System.Drawing.Point(16, 17);
            this.gp_EventData.Name = "gp_EventData";
            this.gp_EventData.Size = new System.Drawing.Size(229, 407);
            this.gp_EventData.TabIndex = 13;
            this.gp_EventData.TabStop = false;
            this.gp_EventData.Text = "Read Event Data";
            // 
            // check_SelectAllEvents
            // 
            this.check_SelectAllEvents.AutoSize = true;
            this.check_SelectAllEvents.Location = new System.Drawing.Point(9, 18);
            this.check_SelectAllEvents.Name = "check_SelectAllEvents";
            this.check_SelectAllEvents.Size = new System.Drawing.Size(75, 18);
            this.check_SelectAllEvents.TabIndex = 10;
            this.check_SelectAllEvents.Text = "Check All";
            this.check_SelectAllEvents.UseVisualStyleBackColor = true;
            this.check_SelectAllEvents.CheckedChanged += new System.EventHandler(this.check_SelectAllEvents_CheckedChanged);
            // 
            // lblEventsFilter
            // 
            this.lblEventsFilter.AutoSize = true;
            this.lblEventsFilter.ForeColor = System.Drawing.Color.Navy;
            this.lblEventsFilter.Location = new System.Drawing.Point(6, 16);
            this.lblEventsFilter.Name = "lblEventsFilter";
            this.lblEventsFilter.Size = new System.Drawing.Size(91, 14);
            this.lblEventsFilter.TabIndex = 9;
            this.lblEventsFilter.Text = "Events Filtering";
            this.lblEventsFilter.Visible = false;
            // 
            // radio_Events_CountersOnly
            // 
            this.radio_Events_CountersOnly.AutoSize = true;
            this.radio_Events_CountersOnly.Checked = true;
            this.radio_Events_CountersOnly.ForeColor = System.Drawing.Color.Navy;
            this.radio_Events_CountersOnly.Location = new System.Drawing.Point(99, 48);
            this.radio_Events_CountersOnly.Name = "radio_Events_CountersOnly";
            this.radio_Events_CountersOnly.Size = new System.Drawing.Size(100, 18);
            this.radio_Events_CountersOnly.TabIndex = 6;
            this.radio_Events_CountersOnly.TabStop = true;
            this.radio_Events_CountersOnly.Text = "Counters Only";
            this.radio_Events_CountersOnly.UseVisualStyleBackColor = true;
            this.radio_Events_CountersOnly.Visible = false;
            this.radio_Events_CountersOnly.CheckedChanged += new System.EventHandler(this.radio_Events_CountersOnly_CheckedChanged);
            // 
            // list_Event_SelectableEvents
            // 
            this.list_Event_SelectableEvents.CheckOnClick = true;
            this.list_Event_SelectableEvents.FormattingEnabled = true;
            this.list_Event_SelectableEvents.Location = new System.Drawing.Point(9, 57);
            this.list_Event_SelectableEvents.Name = "list_Event_SelectableEvents";
            this.list_Event_SelectableEvents.Size = new System.Drawing.Size(210, 344);
            this.list_Event_SelectableEvents.Sorted = true;
            this.list_Event_SelectableEvents.TabIndex = 4;
            // 
            // radio_Events_CompleteLog
            // 
            this.radio_Events_CompleteLog.AutoSize = true;
            this.radio_Events_CompleteLog.ForeColor = System.Drawing.Color.Navy;
            this.radio_Events_CompleteLog.Location = new System.Drawing.Point(99, 32);
            this.radio_Events_CompleteLog.Name = "radio_Events_CompleteLog";
            this.radio_Events_CompleteLog.Size = new System.Drawing.Size(98, 18);
            this.radio_Events_CompleteLog.TabIndex = 6;
            this.radio_Events_CompleteLog.Text = "Complete Log";
            this.radio_Events_CompleteLog.UseVisualStyleBackColor = true;
            this.radio_Events_CompleteLog.Visible = false;
            this.radio_Events_CompleteLog.CheckedChanged += new System.EventHandler(this.radio_Events_CompleteLog_CheckedChanged);
            // 
            // combo_EventFilters
            // 
            this.combo_EventFilters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_EventFilters.FormattingEnabled = true;
            this.combo_EventFilters.Items.AddRange(new object[] {
            "Read All Events",
            "Read Last 10 Events ",
            "Read Last 20 Events",
            "Read Last 30 Events",
            "Read Last 40 Events",
            "Read Last 50 Events"});
            this.combo_EventFilters.Location = new System.Drawing.Point(98, 14);
            this.combo_EventFilters.Name = "combo_EventFilters";
            this.combo_EventFilters.Size = new System.Drawing.Size(125, 22);
            this.combo_EventFilters.TabIndex = 7;
            this.combo_EventFilters.Visible = false;
            // 
            // check_E_addToDB
            // 
            this.check_E_addToDB.AutoSize = true;
            this.check_E_addToDB.BackColor = System.Drawing.Color.Transparent;
            this.check_E_addToDB.Checked = true;
            this.check_E_addToDB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_E_addToDB.Location = new System.Drawing.Point(977, 9);
            this.check_E_addToDB.Name = "check_E_addToDB";
            this.check_E_addToDB.Size = new System.Drawing.Size(80, 18);
            this.check_E_addToDB.TabIndex = 12;
            this.check_E_addToDB.Text = "Add To DB";
            this.check_E_addToDB.UseVisualStyleBackColor = false;
            this.check_E_addToDB.Visible = false;
            this.check_E_addToDB.CheckedChanged += new System.EventHandler(this.check_E_addToDB_CheckedChanged);
            // 
            // lbl_Status
            // 
            this.lbl_Status.AutoSize = true;
            this.lbl_Status.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Status.Location = new System.Drawing.Point(22, 434);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(63, 13);
            this.lbl_Status.TabIndex = 14;
            this.lbl_Status.Text = "lbl_Status";
            this.lbl_Status.Visible = false;
            // 
            // lbl_comboEvents
            // 
            this.lbl_comboEvents.AutoSize = true;
            this.lbl_comboEvents.BackColor = System.Drawing.Color.Transparent;
            this.lbl_comboEvents.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_comboEvents.ForeColor = System.Drawing.Color.Navy;
            this.lbl_comboEvents.Location = new System.Drawing.Point(360, 12);
            this.lbl_comboEvents.Name = "lbl_comboEvents";
            this.lbl_comboEvents.Size = new System.Drawing.Size(146, 15);
            this.lbl_comboEvents.TabIndex = 9;
            this.lbl_comboEvents.Text = "Select Event to see Detail";
            this.lbl_comboEvents.Visible = false;
            // 
            // combo_Events_SelectedITems
            // 
            this.combo_Events_SelectedITems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_Events_SelectedITems.FormattingEnabled = true;
            this.combo_Events_SelectedITems.Location = new System.Drawing.Point(512, 10);
            this.combo_Events_SelectedITems.Name = "combo_Events_SelectedITems";
            this.combo_Events_SelectedITems.Size = new System.Drawing.Size(170, 22);
            this.combo_Events_SelectedITems.TabIndex = 8;
            this.combo_Events_SelectedITems.Visible = false;
            this.combo_Events_SelectedITems.SelectedIndexChanged += new System.EventHandler(this.combo_Events_SelectedITems_SelectedIndexChanged);
            // 
            // lbl_eventDetails
            // 
            this.lbl_eventDetails.AutoSize = true;
            this.lbl_eventDetails.Location = new System.Drawing.Point(700, 18);
            this.lbl_eventDetails.Name = "lbl_eventDetails";
            this.lbl_eventDetails.Size = new System.Drawing.Size(0, 14);
            this.lbl_eventDetails.TabIndex = 11;
            // 
            // ProgramEvents
            // 
            this.ProgramEvents.Controls.Add(this.btn_SaveAlarm);
            this.ProgramEvents.Controls.Add(this.btn_LoadAlarm);
            this.ProgramEvents.Controls.Add(this.check_resetAll_Alarms);
            this.ProgramEvents.Controls.Add(this.grid_Events);
            this.ProgramEvents.Location = new System.Drawing.Point(4, 23);
            this.ProgramEvents.Name = "ProgramEvents";
            this.ProgramEvents.Padding = new System.Windows.Forms.Padding(3);
            this.ProgramEvents.Size = new System.Drawing.Size(1158, 551);
            this.ProgramEvents.TabIndex = 0;
            this.ProgramEvents.Text = "Program Major Alarm";
            this.ProgramEvents.UseVisualStyleBackColor = true;
            // 
            // btn_SaveAlarm
            // 
            this.btn_SaveAlarm.Location = new System.Drawing.Point(3, 459);
            this.btn_SaveAlarm.Name = "btn_SaveAlarm";
            this.btn_SaveAlarm.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_SaveAlarm.Size = new System.Drawing.Size(136, 30);
            this.btn_SaveAlarm.TabIndex = 13;
            this.btn_SaveAlarm.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_SaveAlarm.Values.Image")));
            this.btn_SaveAlarm.Values.Text = "Save MajorAlarm";
            this.btn_SaveAlarm.Visible = false;
            this.btn_SaveAlarm.Click += new System.EventHandler(this.btn_SaveAlarm_Click);
            // 
            // btn_LoadAlarm
            // 
            this.btn_LoadAlarm.Location = new System.Drawing.Point(145, 459);
            this.btn_LoadAlarm.Name = "btn_LoadAlarm";
            this.btn_LoadAlarm.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_LoadAlarm.Size = new System.Drawing.Size(136, 30);
            this.btn_LoadAlarm.TabIndex = 12;
            this.btn_LoadAlarm.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_LoadAlarm.Values.Image")));
            this.btn_LoadAlarm.Values.Text = "Load MajorAlarm";
            this.btn_LoadAlarm.Visible = false;
            this.btn_LoadAlarm.Click += new System.EventHandler(this.btn_LoadAlarm_Click);
            // 
            // check_resetAll_Alarms
            // 
            this.check_resetAll_Alarms.AutoSize = true;
            this.check_resetAll_Alarms.Location = new System.Drawing.Point(861, 469);
            this.check_resetAll_Alarms.Name = "check_resetAll_Alarms";
            this.check_resetAll_Alarms.Size = new System.Drawing.Size(75, 18);
            this.check_resetAll_Alarms.TabIndex = 4;
            this.check_resetAll_Alarms.Text = "Reset All";
            this.check_resetAll_Alarms.UseVisualStyleBackColor = true;
            this.check_resetAll_Alarms.Click += new System.EventHandler(this.check_resetAll_Alarms_Click);
            // 
            // grid_Events
            // 
            this.grid_Events.AllowUserToAddRows = false;
            this.grid_Events.AllowUserToDeleteRows = false;
            this.grid_Events.AllowUserToOrderColumns = true;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.PowderBlue;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightBlue;
            this.grid_Events.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.grid_Events.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid_Events.BackgroundColor = System.Drawing.Color.DimGray;
            this.grid_Events.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_Events.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grid_Events.ColumnHeadersHeight = 50;
            this.grid_Events.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Event_Name,
            this.Event_Code,
            this.Caution_Number,
            this.Is_Enable,
            this.Is_LogBook_Event,
            this.Read_caution,
            this.Display_Caution,
            this.isFlash,
            this.Flash_Time,
            this.IsMajorAlarm,
            this.IsTriggered,
            this.ResetAlarmStatus});
            this.grid_Events.GridColor = System.Drawing.Color.SteelBlue;
            this.grid_Events.Location = new System.Drawing.Point(0, 0);
            this.grid_Events.Name = "grid_Events";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_Events.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.grid_Events.RowHeadersWidth = 10;
            this.grid_Events.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.grid_Events.Size = new System.Drawing.Size(1119, 450);
            this.grid_Events.TabIndex = 3;
            this.grid_Events.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_Events_CellValueChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Enabled = false;
            this.progressBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.progressBar1.Location = new System.Drawing.Point(1050, 41);
            this.progressBar1.MarqueeAnimationSpeed = 35;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.progressBar1.Size = new System.Drawing.Size(116, 24);
            this.progressBar1.Step = 30;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 7;
            this.progressBar1.Visible = false;
            // 
            // Progress_Bar
            // 
            this.Progress_Bar.Enabled = false;
            this.Progress_Bar.Location = new System.Drawing.Point(1050, 41);
            this.Progress_Bar.Name = "Progress_Bar";
            this.Progress_Bar.Size = new System.Drawing.Size(116, 24);
            this.Progress_Bar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.Progress_Bar.TabIndex = 15;
            this.Progress_Bar.Visible = false;
            // 
            // btn_GetMajorAlarms
            // 
            this.btn_GetMajorAlarms.Location = new System.Drawing.Point(149, 3);
            this.btn_GetMajorAlarms.Name = "btn_GetMajorAlarms";
            this.btn_GetMajorAlarms.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_GetMajorAlarms.Size = new System.Drawing.Size(140, 30);
            this.btn_GetMajorAlarms.TabIndex = 5;
            this.btn_GetMajorAlarms.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_GetMajorAlarms.Values.Image")));
            this.btn_GetMajorAlarms.Values.Text = "Get Major Alarms";
            this.btn_GetMajorAlarms.Click += new System.EventHandler(this.btn_GetMajorAlarm_Click);
            // 
            // btn_SetMajorAlarm
            // 
            this.btn_SetMajorAlarm.Location = new System.Drawing.Point(579, 3);
            this.btn_SetMajorAlarm.Name = "btn_SetMajorAlarm";
            this.btn_SetMajorAlarm.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_SetMajorAlarm.Size = new System.Drawing.Size(140, 30);
            this.btn_SetMajorAlarm.TabIndex = 4;
            this.btn_SetMajorAlarm.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_SetMajorAlarm.Values.Image")));
            this.btn_SetMajorAlarm.Values.Text = "Set Major Alarms";
            this.btn_SetMajorAlarm.Click += new System.EventHandler(this.btn_SetMajorAlarm_Click);
            // 
            // btn_GET_EventsCautions
            // 
            this.btn_GET_EventsCautions.Location = new System.Drawing.Point(3, 3);
            this.btn_GET_EventsCautions.Name = "btn_GET_EventsCautions";
            this.btn_GET_EventsCautions.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_GET_EventsCautions.Size = new System.Drawing.Size(140, 30);
            this.btn_GET_EventsCautions.TabIndex = 6;
            this.btn_GET_EventsCautions.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_GET_EventsCautions.Values.Image")));
            this.btn_GET_EventsCautions.Values.Text = "Get Event Cautions";
            this.btn_GET_EventsCautions.Click += new System.EventHandler(this.btn_GET_EventsCautions_Click);
            // 
            // btn_SET_EventsCautions
            // 
            this.btn_SET_EventsCautions.Location = new System.Drawing.Point(295, 3);
            this.btn_SET_EventsCautions.Name = "btn_SET_EventsCautions";
            this.btn_SET_EventsCautions.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_SET_EventsCautions.Size = new System.Drawing.Size(140, 30);
            this.btn_SET_EventsCautions.TabIndex = 6;
            this.btn_SET_EventsCautions.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_SET_EventsCautions.Values.Image")));
            this.btn_SET_EventsCautions.Values.Text = "Set Event Cautions";
            this.btn_SET_EventsCautions.Click += new System.EventHandler(this.btn_SET_Click);
            // 
            // btn_Events_GET
            // 
            this.btn_Events_GET.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Events_GET.Location = new System.Drawing.Point(3, 3);
            this.btn_Events_GET.Name = "btn_Events_GET";
            this.btn_Events_GET.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_Events_GET.Size = new System.Drawing.Size(115, 30);
            this.btn_Events_GET.TabIndex = 5;
            this.btn_Events_GET.Values.Text = "READ";
            this.btn_Events_GET.Click += new System.EventHandler(this.btn_Events_GET_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // btn_events_report
            // 
            this.btn_events_report.Location = new System.Drawing.Point(1126, 3);
            this.btn_events_report.Name = "btn_events_report";
            this.btn_events_report.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_events_report.Size = new System.Drawing.Size(131, 30);
            this.btn_events_report.TabIndex = 37;
            this.btn_events_report.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_events_report.Values.Image")));
            this.btn_events_report.Values.Text = "Generate Report";
            this.btn_events_report.Click += new System.EventHandler(this.btn_Reports_events_Click);
            // 
            // btn_SetMajorStatus
            // 
            this.btn_SetMajorStatus.ForeColor = System.Drawing.Color.Navy;
            this.btn_SetMajorStatus.Location = new System.Drawing.Point(441, 3);
            this.btn_SetMajorStatus.Name = "btn_SetMajorStatus";
            this.btn_SetMajorStatus.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_SetMajorStatus.Size = new System.Drawing.Size(132, 30);
            this.btn_SetMajorStatus.TabIndex = 36;
            this.btn_SetMajorStatus.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_SetMajorStatus.Values.Image")));
            this.btn_SetMajorStatus.Values.Text = "Set Alarm Status";
            this.btn_SetMajorStatus.Visible = false;
            this.btn_SetMajorStatus.Click += new System.EventHandler(this.btn_SetMajorStatus_Click);
            // 
            // bgw_SetMajorAlarms
            // 
            this.bgw_SetMajorAlarms.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_SetMajorAlarms_DoWork);
            this.bgw_SetMajorAlarms.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_SetMajorAlarms_RunWorkerCompleted);
            // 
            // bgw_GetMajorAlarms
            // 
            this.bgw_GetMajorAlarms.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_GetMajorAlarms_DoWork);
            this.bgw_GetMajorAlarms.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_GetMajorAlarms_RunWorkerCompleted);
            // 
            // bgw_SetAlarmStatus
            // 
            this.bgw_SetAlarmStatus.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_SetAlarmStatus_DoWork);
            this.bgw_SetAlarmStatus.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_SetAlarmStatus_RunWorkerCompleted);
            // 
            // bgw_getEventCautions
            // 
            this.bgw_getEventCautions.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_getEventCautions_DoWork);
            this.bgw_getEventCautions.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_getEventCautions_RunWorkerCompleted);
            // 
            // bgw_SetEventCautions
            // 
            this.bgw_SetEventCautions.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_SetEventCautions_DoWork);
            this.bgw_SetEventCautions.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_SetEventCautions_RunWorkerCompleted);
            // 
            // btn_SecurityReport
            // 
            this.btn_SecurityReport.Location = new System.Drawing.Point(989, 3);
            this.btn_SecurityReport.Name = "btn_SecurityReport";
            this.btn_SecurityReport.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_SecurityReport.Size = new System.Drawing.Size(131, 30);
            this.btn_SecurityReport.TabIndex = 37;
            this.btn_SecurityReport.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_SecurityReport.Values.Image")));
            this.btn_SecurityReport.Values.Text = "Security Report";
            this.btn_SecurityReport.Visible = false;
            this.btn_SecurityReport.Click += new System.EventHandler(this.btn_SecurityReport_Click);
            // 
            // btn_ReadSecurityData
            // 
            this.btn_ReadSecurityData.Location = new System.Drawing.Point(852, 3);
            this.btn_ReadSecurityData.Name = "btn_ReadSecurityData";
            this.btn_ReadSecurityData.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_ReadSecurityData.Size = new System.Drawing.Size(131, 30);
            this.btn_ReadSecurityData.TabIndex = 37;
            this.btn_ReadSecurityData.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_ReadSecurityData.Values.Image")));
            this.btn_ReadSecurityData.Values.Text = "Read Security Data";
            this.btn_ReadSecurityData.Click += new System.EventHandler(this.btn_ReadSecurityData_Click);
            // 
            // fLP_MainButtons
            // 
            this.fLP_MainButtons.AutoSize = true;
            this.fLP_MainButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_MainButtons.BackColor = System.Drawing.Color.Transparent;
            this.fLP_MainButtons.Controls.Add(this.btn_Events_GET);
            this.fLP_MainButtons.Controls.Add(this.flp_MajorAlarmButtons);
            this.fLP_MainButtons.Controls.Add(this.btn_ReadSecurityData);
            this.fLP_MainButtons.Controls.Add(this.btn_SecurityReport);
            this.fLP_MainButtons.Controls.Add(this.btn_events_report);
            this.fLP_MainButtons.Location = new System.Drawing.Point(0, 34);
            this.fLP_MainButtons.Name = "fLP_MainButtons";
            this.fLP_MainButtons.Size = new System.Drawing.Size(1260, 41);
            this.fLP_MainButtons.TabIndex = 38;
            // 
            // flp_MajorAlarmButtons
            // 
            this.flp_MajorAlarmButtons.BackColor = System.Drawing.Color.Transparent;
            this.flp_MajorAlarmButtons.Controls.Add(this.btn_GET_EventsCautions);
            this.flp_MajorAlarmButtons.Controls.Add(this.btn_GetMajorAlarms);
            this.flp_MajorAlarmButtons.Controls.Add(this.btn_SET_EventsCautions);
            this.flp_MajorAlarmButtons.Controls.Add(this.btn_SetMajorStatus);
            this.flp_MajorAlarmButtons.Controls.Add(this.btn_SetMajorAlarm);
            this.flp_MajorAlarmButtons.Location = new System.Drawing.Point(124, 3);
            this.flp_MajorAlarmButtons.Name = "flp_MajorAlarmButtons";
            this.flp_MajorAlarmButtons.Size = new System.Drawing.Size(722, 35);
            this.flp_MajorAlarmButtons.TabIndex = 38;
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.BackColor = System.Drawing.Color.Transparent;
            this.lblHeading.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.Black;
            this.lblHeading.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblHeading.Location = new System.Drawing.Point(416, 1);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(124, 33);
            this.lblHeading.TabIndex = 11;
            this.lblHeading.Text = "      Events";
            // 
            // DateTimes
            // 
            this.DateTimes.FillWeight = 40F;
            this.DateTimes.HeaderText = "Date Time";
            this.DateTimes.Name = "DateTimes";
            this.DateTimes.ReadOnly = true;
            this.DateTimes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // EventNames
            // 
            this.EventNames.FillWeight = 60F;
            this.EventNames.HeaderText = "Event Name";
            this.EventNames.Name = "EventNames";
            this.EventNames.ReadOnly = true;
            this.EventNames.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // EventCodes
            // 
            this.EventCodes.HeaderText = "Event Code";
            this.EventCodes.Name = "EventCodes";
            this.EventCodes.ReadOnly = true;
            this.EventCodes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // EventCounters
            // 
            this.EventCounters.HeaderText = "Event Counter";
            this.EventCounters.Name = "EventCounters";
            this.EventCounters.ReadOnly = true;
            this.EventCounters.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // usDateRangeEv
            // 
            this.usDateRangeEv.Location = new System.Drawing.Point(10, 49);
            this.usDateRangeEv.Name = "usDateRangeEv";
            this.usDateRangeEv.Size = new System.Drawing.Size(204, 55);
            this.usDateRangeEv.TabIndex = 9;
            // 
            // Event_Name
            // 
            this.Event_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Event_Name.FillWeight = 62.49745F;
            this.Event_Name.HeaderText = "Event Name";
            this.Event_Name.MaxInputLength = 1;
            this.Event_Name.Name = "Event_Name";
            this.Event_Name.ReadOnly = true;
            this.Event_Name.Width = 175;
            // 
            // Event_Code
            // 
            this.Event_Code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Event_Code.DefaultCellStyle = dataGridViewCellStyle5;
            this.Event_Code.FillWeight = 2.055837F;
            this.Event_Code.HeaderText = "Event Code";
            this.Event_Code.Name = "Event_Code";
            this.Event_Code.ReadOnly = true;
            this.Event_Code.Width = 84;
            // 
            // Caution_Number
            // 
            this.Caution_Number.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Caution_Number.DefaultCellStyle = dataGridViewCellStyle6;
            this.Caution_Number.FillWeight = 2.055837F;
            this.Caution_Number.HeaderText = "Caution Number";
            this.Caution_Number.Name = "Caution_Number";
            this.Caution_Number.Width = 109;
            // 
            // Is_Enable
            // 
            this.Is_Enable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Is_Enable.HeaderText = "Is Disable";
            this.Is_Enable.Name = "Is_Enable";
            this.Is_Enable.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Is_Enable.Width = 62;
            // 
            // Is_LogBook_Event
            // 
            this.Is_LogBook_Event.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Is_LogBook_Event.HeaderText = "Exclude from Logbook";
            this.Is_LogBook_Event.Name = "Is_LogBook_Event";
            this.Is_LogBook_Event.Width = 77;
            // 
            // Read_caution
            // 
            this.Read_caution.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Read_caution.FillWeight = 2.055837F;
            this.Read_caution.HeaderText = "Read Caution";
            this.Read_caution.Name = "Read_caution";
            this.Read_caution.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Read_caution.Width = 77;
            // 
            // Display_Caution
            // 
            this.Display_Caution.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Display_Caution.FillWeight = 2.055837F;
            this.Display_Caution.HeaderText = "Display Caution";
            this.Display_Caution.Name = "Display_Caution";
            this.Display_Caution.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Display_Caution.Width = 88;
            // 
            // isFlash
            // 
            this.isFlash.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.isFlash.FillWeight = 2.055837F;
            this.isFlash.HeaderText = "isFlash";
            this.isFlash.Name = "isFlash";
            this.isFlash.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.isFlash.Width = 53;
            // 
            // Flash_Time
            // 
            this.Flash_Time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Flash_Time.DefaultCellStyle = dataGridViewCellStyle7;
            this.Flash_Time.FillWeight = 2.055837F;
            this.Flash_Time.HeaderText = "Flash Time";
            this.Flash_Time.Name = "Flash_Time";
            this.Flash_Time.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // IsMajorAlarm
            // 
            this.IsMajorAlarm.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.IsMajorAlarm.FillWeight = 2.055837F;
            this.IsMajorAlarm.HeaderText = "Is Major Alarm";
            this.IsMajorAlarm.Name = "IsMajorAlarm";
            this.IsMajorAlarm.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsMajorAlarm.Width = 54;
            // 
            // IsTriggered
            // 
            this.IsTriggered.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.IsTriggered.FillWeight = 2.055837F;
            this.IsTriggered.HeaderText = "Alarm Status";
            this.IsTriggered.Name = "IsTriggered";
            this.IsTriggered.ReadOnly = true;
            this.IsTriggered.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.IsTriggered.Width = 74;
            // 
            // ResetAlarmStatus
            // 
            this.ResetAlarmStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ResetAlarmStatus.FillWeight = 2.055837F;
            this.ResetAlarmStatus.HeaderText = "Reset Alarm Status";
            this.ResetAlarmStatus.Name = "ResetAlarmStatus";
            this.ResetAlarmStatus.Width = 74;
            // 
            // pnlEvents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.Progress_Bar);
            this.Controls.Add(this.tb_Events);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.fLP_MainButtons);
            this.DoubleBuffered = true;
            this.Name = "pnlEvents";
            this.Size = new System.Drawing.Size(1350, 650);
            this.Load += new System.EventHandler(this.pnlEvents_Load);
            this.tb_Events.ResumeLayout(false);
            this.tab_LogBook.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_LogBook)).EndInit();
            this.Meter_Events.ResumeLayout(false);
            this.Meter_Events.PerformLayout();
            this.pnl_Event_Container.ResumeLayout(false);
            this.pnl_Event_Container.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_Events_Counters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_Events_Log)).EndInit();
            this.gp_EventData.ResumeLayout(false);
            this.gp_EventData.PerformLayout();
            this.ProgramEvents.ResumeLayout(false);
            this.ProgramEvents.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_Events)).EndInit();
            this.fLP_MainButtons.ResumeLayout(false);
            this.flp_MajorAlarmButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public ComponentFactory.Krypton.Toolkit.KryptonButton btn_SET_EventsCautions;
        public ComponentFactory.Krypton.Toolkit.KryptonButton btn_SetMajorAlarm;
        public ComponentFactory.Krypton.Toolkit.KryptonButton btn_events_report;
        public ComponentFactory.Krypton.Toolkit.KryptonButton btn_SetMajorStatus;
        public ComponentFactory.Krypton.Toolkit.KryptonButton btn_ReadSecurityData;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_ReadLogBook;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid_LogBook;

        private System.Windows.Forms.TabPage ProgramEvents;
        private System.Windows.Forms.TabPage Meter_Events;
        //   private System.Windows.Forms.DataGridView grid_Events_Counters;
        private System.Windows.Forms.CheckedListBox list_Event_SelectableEvents;
       // private System.Windows.Forms.Button btn_Events_GET;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_Events_GET;
        //   private System.Windows.Forms.DataGridView grid_Events_Log;
        private System.Windows.Forms.RadioButton radio_Events_CompleteLog;
        private System.Windows.Forms.RadioButton radio_Events_CountersOnly;
        private System.Windows.Forms.Label lbl_comboEvents;
        public System.Windows.Forms.ComboBox combo_Events_SelectedITems;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox check_SelectAllEvents;
        private System.Windows.Forms.ComboBox combo_EventFilters;
        private System.Windows.Forms.Label lblEventsFilter;
        private System.Windows.Forms.Label lbl_eventDetails;
        //private System.Windows.Forms.Button btn_SetMajorAlarm;
       // private ComponentFactory.Krypton.Toolkit.KryptonButton btn_SetMajorAlarm;
        //private System.Windows.Forms.Button btn_GetMajorAlarms;
        //private ComponentFactory.Krypton.Toolkit.KryptonButton btn_GetMajorAlarms;
        //private ComponentFactory.Krypton.Toolkit.KryptonButton btn_SET_EventsCautions;

        //private System.Windows.Forms.Button btn_SET_EventsCautions;
        //private System.Windows.Forms.Button btn_GET_EventsCautions;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_GET_EventsCautions;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_GetMajorAlarms;

        //private ComponentFactory.Krypton.Toolkit.KryptonButton btn_GET_EventsCautions;
        //private System.Windows.Forms.Button btn_GET_TBE2;
        // private System.Windows.Forms.Button btn_SET_TBE2;
        private System.Windows.Forms.CheckBox check_E_addToDB;
        //private System.Windows.Forms.Button btn_GET_TBE1;
        // private System.Windows.Forms.Button btn_SET_TBE1;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.GroupBox gp_EventData;
        private System.Windows.Forms.Label lbl_Status;
        private System.ComponentModel.BackgroundWorker bgw_TBE1_Set;
        private System.ComponentModel.BackgroundWorker bgw_TBE1_Get;
        private System.ComponentModel.BackgroundWorker bgw_TBE2_Set;
        private System.ComponentModel.BackgroundWorker bgw_TBE2_Get;
        private CheckBox check_resetAll_Alarms;
        private System.ComponentModel.BackgroundWorker bgw_SetMajorAlarms;
        private System.ComponentModel.BackgroundWorker bgw_GetMajorAlarms;
        private ProgressBar Progress_Bar;
        private System.ComponentModel.BackgroundWorker bgw_SetAlarmStatus;
        private System.ComponentModel.BackgroundWorker bgw_getEventCautions;
        private System.ComponentModel.BackgroundWorker bgw_SetEventCautions;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_SecurityReport;
        private DataGridView grid_Events;
        //public ComponentFactory.Krypton.Toolkit.KryptonButton btn_SET_EventsCautions;
        //public ComponentFactory.Krypton.Toolkit.KryptonButton btn_SetMajorAlarm;
        //public ComponentFactory.Krypton.Toolkit.KryptonButton btn_events_report;
        //public ComponentFactory.Krypton.Toolkit.KryptonButton btn_SetMajorStatus;
        //public ComponentFactory.Krypton.Toolkit.KryptonButton btn_ReadSecurityData;
        //private ComponentFactory.Krypton.Toolkit.KryptonButton btn_ReadLogBook;
        //private ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid_LogBook;
        public TabPage tab_LogBook;
        public TabControl tb_Events;
        private Panel pnl_Event_Container;
        private TabPage Individual_Events;
        private FlowLayoutPanel fLP_MainButtons;
        internal ComponentFactory.Krypton.Toolkit.KryptonButton btn_LoadAlarm;
        internal ComponentFactory.Krypton.Toolkit.KryptonButton btn_SaveAlarm;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid_Events_Log;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid_Events_Counters;
        private DataGridViewTextBoxColumn EventsName;
        private DataGridViewTextBoxColumn EventsCode;
        private DataGridViewTextBoxColumn EventsCounter;
        private FlowLayoutPanel flp_MajorAlarmButtons;
        private DataGridViewTextBoxColumn Date_Time;
        private DataGridViewTextBoxColumn EventName;
        private DataGridViewTextBoxColumn EventCode;
        private DataGridViewTextBoxColumn EventCounter;
        private DataGridViewTextBoxColumn EventDetails;
        private SmartEyeControl_7.ApplicationGUI.ucCustomControl.usDateRange usDateRangeEv;
        private CheckBox chkReadByDateTime;
        private GroupBox groupBox1;
        private DataGridViewTextBoxColumn DateTimes;
        private DataGridViewTextBoxColumn EventNames;
        private DataGridViewTextBoxColumn EventCodes;
        private DataGridViewTextBoxColumn EventCounters;
        private DataGridViewTextBoxColumn Event_Name;
        private DataGridViewTextBoxColumn Event_Code;
        private DataGridViewTextBoxColumn Caution_Number;
        private DataGridViewCheckBoxColumn Is_Enable;
        private DataGridViewCheckBoxColumn Is_LogBook_Event;
        private DataGridViewCheckBoxColumn Read_caution;
        private DataGridViewCheckBoxColumn Display_Caution;
        private DataGridViewCheckBoxColumn isFlash;
        private DataGridViewTextBoxColumn Flash_Time;
        private DataGridViewCheckBoxColumn IsMajorAlarm;
        private DataGridViewTextBoxColumn IsTriggered;
        private DataGridViewCheckBoxColumn ResetAlarmStatus;
    }
}
