namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    partial class ucMDIParams
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.gpMDIAutoReset = new System.Windows.Forms.GroupBox();
            this.fLP_AutoReset = new System.Windows.Forms.FlowLayoutPanel();
            this.pnl_AutoResetEnable = new System.Windows.Forms.Panel();
            this.check_MDIParams_Autoreset = new System.Windows.Forms.CheckBox();
            this.pnl_AutoResetDateTime = new System.Windows.Forms.Panel();
            this.lbl_AutoResetDateTime = new System.Windows.Forms.Label();
            this.ucAutoResetDateParam = new ucDateTimeChooser.ucCustomDateTimePicker();
            this.ucMDIAutoResetDateParam = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucCustomDateTime();
            this.fLP_MDI_Interval = new System.Windows.Forms.FlowLayoutPanel();
            this.lblMDIInterval = new System.Windows.Forms.Label();
            this.combo_MDIParams_MDIInterval = new System.Windows.Forms.ComboBox();
            this.lbl_MDIInterval = new System.Windows.Forms.Label();
            this.pnl_SlideCount_Container = new System.Windows.Forms.SplitContainer();
            this.tb_MDIParams_SlidesCount = new System.Windows.Forms.TrackBar();
            this.lbl_Val_5 = new System.Windows.Forms.Label();
            this.lbl_Val_4 = new System.Windows.Forms.Label();
            this.lbl_Val_3 = new System.Windows.Forms.Label();
            this.lbl_Val_2 = new System.Windows.Forms.Label();
            this.lbl_Val_Max = new System.Windows.Forms.Label();
            this.lbl_Val_Min = new System.Windows.Forms.Label();
            this.lbl_SlideCount = new System.Windows.Forms.Label();
            this.gpMDIManualReset = new System.Windows.Forms.GroupBox();
            this.fLP_ManualReset = new System.Windows.Forms.FlowLayoutPanel();
            this.fLP_manual_Checks = new System.Windows.Forms.FlowLayoutPanel();
            this.pnl_Manual_Reset = new System.Windows.Forms.Panel();
            this.lbl_ManualResets = new System.Windows.Forms.Label();
            this.lbl_ValidationRange = new System.Windows.Forms.Label();
            this.radio_MDI_h = new System.Windows.Forms.RadioButton();
            this.radio_MDI_d = new System.Windows.Forms.RadioButton();
            this.radio_MDI_min = new System.Windows.Forms.RadioButton();
            this.radio_MDI_s = new System.Windows.Forms.RadioButton();
            this.txt_MDIParams_minTime = new System.Windows.Forms.TextBox();
            this.check_MDIParams_ManualResetByButton = new System.Windows.Forms.CheckBox();
            this.check_MDIParams_ManualResetByRemote = new System.Windows.Forms.CheckBox();
            this.check_MDIParams_Manual_Reset_powerDown_Mode = new System.Windows.Forms.CheckBox();
            this.check_MDIParams_Auto_Reset_powerDown_Mode = new System.Windows.Forms.CheckBox();
            this.gpMDIResetParam = new System.Windows.Forms.GroupBox();
            this.fLP_Main = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.gpMDIAutoReset.SuspendLayout();
            this.fLP_AutoReset.SuspendLayout();
            this.pnl_AutoResetEnable.SuspendLayout();
            this.pnl_AutoResetDateTime.SuspendLayout();
            this.fLP_MDI_Interval.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_SlideCount_Container)).BeginInit();
            this.pnl_SlideCount_Container.Panel1.SuspendLayout();
            this.pnl_SlideCount_Container.Panel2.SuspendLayout();
            this.pnl_SlideCount_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_MDIParams_SlidesCount)).BeginInit();
            this.gpMDIManualReset.SuspendLayout();
            this.fLP_ManualReset.SuspendLayout();
            this.fLP_manual_Checks.SuspendLayout();
            this.pnl_Manual_Reset.SuspendLayout();
            this.gpMDIResetParam.SuspendLayout();
            this.fLP_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // gpMDIAutoReset
            // 
            this.gpMDIAutoReset.AutoSize = true;
            this.gpMDIAutoReset.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gpMDIAutoReset.Controls.Add(this.fLP_AutoReset);
            this.gpMDIAutoReset.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gpMDIAutoReset.Location = new System.Drawing.Point(3, 182);
            this.gpMDIAutoReset.Margin = new System.Windows.Forms.Padding(3, 15, 3, 0);
            this.gpMDIAutoReset.Name = "gpMDIAutoReset";
            this.gpMDIAutoReset.Size = new System.Drawing.Size(497, 145);
            this.gpMDIAutoReset.TabIndex = 12;
            this.gpMDIAutoReset.TabStop = false;
            this.gpMDIAutoReset.Text = "MDI Auto Reset";
            // 
            // fLP_AutoReset
            // 
            this.fLP_AutoReset.AutoSize = true;
            this.fLP_AutoReset.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_AutoReset.Controls.Add(this.pnl_AutoResetEnable);
            this.fLP_AutoReset.Controls.Add(this.pnl_AutoResetDateTime);
            this.fLP_AutoReset.Controls.Add(this.ucMDIAutoResetDateParam);
            this.fLP_AutoReset.Controls.Add(this.fLP_MDI_Interval);
            this.fLP_AutoReset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fLP_AutoReset.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLP_AutoReset.Location = new System.Drawing.Point(3, 19);
            this.fLP_AutoReset.Name = "fLP_AutoReset";
            this.fLP_AutoReset.Size = new System.Drawing.Size(491, 123);
            this.fLP_AutoReset.TabIndex = 0;
            // 
            // pnl_AutoResetEnable
            // 
            this.pnl_AutoResetEnable.AutoSize = true;
            this.pnl_AutoResetEnable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnl_AutoResetEnable.Controls.Add(this.check_MDIParams_Autoreset);
            this.pnl_AutoResetEnable.Location = new System.Drawing.Point(0, 0);
            this.pnl_AutoResetEnable.Margin = new System.Windows.Forms.Padding(0);
            this.pnl_AutoResetEnable.Name = "pnl_AutoResetEnable";
            this.pnl_AutoResetEnable.Size = new System.Drawing.Size(129, 19);
            this.pnl_AutoResetEnable.TabIndex = 13;
            // 
            // check_MDIParams_Autoreset
            // 
            this.check_MDIParams_Autoreset.AutoSize = true;
            this.check_MDIParams_Autoreset.ForeColor = System.Drawing.Color.Black;
            this.check_MDIParams_Autoreset.Location = new System.Drawing.Point(0, 0);
            this.check_MDIParams_Autoreset.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.check_MDIParams_Autoreset.Name = "check_MDIParams_Autoreset";
            this.check_MDIParams_Autoreset.Size = new System.Drawing.Size(126, 19);
            this.check_MDIParams_Autoreset.TabIndex = 9;
            this.check_MDIParams_Autoreset.Text = "Auto Reset Enable";
            this.check_MDIParams_Autoreset.UseVisualStyleBackColor = true;
            this.check_MDIParams_Autoreset.CheckStateChanged += new System.EventHandler(this.check_MDIParams_Autoreset_CheckStateChanged);
            // 
            // pnl_AutoResetDateTime
            // 
            this.pnl_AutoResetDateTime.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnl_AutoResetDateTime.Controls.Add(this.lbl_AutoResetDateTime);
            this.pnl_AutoResetDateTime.Controls.Add(this.ucAutoResetDateParam);
            this.pnl_AutoResetDateTime.Location = new System.Drawing.Point(3, 22);
            this.pnl_AutoResetDateTime.Name = "pnl_AutoResetDateTime";
            this.pnl_AutoResetDateTime.Size = new System.Drawing.Size(304, 27);
            this.pnl_AutoResetDateTime.TabIndex = 41;
            // 
            // lbl_AutoResetDateTime
            // 
            this.lbl_AutoResetDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_AutoResetDateTime.AutoSize = true;
            this.lbl_AutoResetDateTime.ForeColor = System.Drawing.Color.Black;
            this.lbl_AutoResetDateTime.Location = new System.Drawing.Point(0, 0);
            this.lbl_AutoResetDateTime.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lbl_AutoResetDateTime.Name = "lbl_AutoResetDateTime";
            this.lbl_AutoResetDateTime.Size = new System.Drawing.Size(150, 15);
            this.lbl_AutoResetDateTime.TabIndex = 21;
            this.lbl_AutoResetDateTime.Text = "MDI Auto-Reset DateTime";
            // 
            // ucAutoResetDateParam
            // 
            this.ucAutoResetDateParam.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ucAutoResetDateParam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucAutoResetDateParam.Dock = System.Windows.Forms.DockStyle.Right;
            this.ucAutoResetDateParam.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ucAutoResetDateParam.FormatEx = DLMS.dtpCustomExtensions.dtpDayOfMonth;
            this.ucAutoResetDateParam.Location = new System.Drawing.Point(167, 0);
            this.ucAutoResetDateParam.Name = "ucAutoResetDateParam";
            this.ucAutoResetDateParam.ShowButtons = true;
            this.ucAutoResetDateParam.ShowUpDownButton = true;
            this.ucAutoResetDateParam.ShowWildCardWinButton = false;
            this.ucAutoResetDateParam.Size = new System.Drawing.Size(137, 22);
            this.ucAutoResetDateParam.TabIndex = 40;
            // 
            // ucMDIAutoResetDateParam
            // 
            this.ucMDIAutoResetDateParam.BackColor = System.Drawing.Color.Transparent;
            this.ucMDIAutoResetDateParam.Enabled = false;
            this.ucMDIAutoResetDateParam.Location = new System.Drawing.Point(0, 52);
            this.ucMDIAutoResetDateParam.Margin = new System.Windows.Forms.Padding(0);
            this.ucMDIAutoResetDateParam.Name = "ucMDIAutoResetDateParam";
            this.ucMDIAutoResetDateParam.Size = new System.Drawing.Size(0, 0);
            this.ucMDIAutoResetDateParam.TabIndex = 20;
            this.ucMDIAutoResetDateParam.Visible = false;
            // 
            // fLP_MDI_Interval
            // 
            this.fLP_MDI_Interval.AutoSize = true;
            this.fLP_MDI_Interval.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_MDI_Interval.Controls.Add(this.lblMDIInterval);
            this.fLP_MDI_Interval.Controls.Add(this.combo_MDIParams_MDIInterval);
            this.fLP_MDI_Interval.Controls.Add(this.lbl_MDIInterval);
            this.fLP_MDI_Interval.Controls.Add(this.pnl_SlideCount_Container);
            this.fLP_MDI_Interval.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fLP_MDI_Interval.Location = new System.Drawing.Point(3, 55);
            this.fLP_MDI_Interval.Name = "fLP_MDI_Interval";
            this.fLP_MDI_Interval.Size = new System.Drawing.Size(431, 65);
            this.fLP_MDI_Interval.TabIndex = 21;
            // 
            // lblMDIInterval
            // 
            this.lblMDIInterval.AutoSize = true;
            this.lblMDIInterval.ForeColor = System.Drawing.Color.Black;
            this.lblMDIInterval.Location = new System.Drawing.Point(3, 25);
            this.lblMDIInterval.Margin = new System.Windows.Forms.Padding(3, 25, 3, 0);
            this.lblMDIInterval.Name = "lblMDIInterval";
            this.lblMDIInterval.Size = new System.Drawing.Size(74, 15);
            this.lblMDIInterval.TabIndex = 1;
            this.lblMDIInterval.Text = "MDI interval";
            // 
            // combo_MDIParams_MDIInterval
            // 
            this.combo_MDIParams_MDIInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_MDIParams_MDIInterval.FormattingEnabled = true;
            this.combo_MDIParams_MDIInterval.Items.AddRange(new object[] {
            "1",
            "5",
            "6",
            "10",
            "15",
            "30",
            "60"});
            this.combo_MDIParams_MDIInterval.Location = new System.Drawing.Point(110, 20);
            this.combo_MDIParams_MDIInterval.Margin = new System.Windows.Forms.Padding(30, 20, 3, 3);
            this.combo_MDIParams_MDIInterval.Name = "combo_MDIParams_MDIInterval";
            this.combo_MDIParams_MDIInterval.Size = new System.Drawing.Size(62, 23);
            this.combo_MDIParams_MDIInterval.TabIndex = 9;
            this.combo_MDIParams_MDIInterval.SelectedIndexChanged += new System.EventHandler(this.combo_MDIParams_MDIInterval_SelectedIndexChanged);
            this.combo_MDIParams_MDIInterval.Leave += new System.EventHandler(this.combo_MDIParams_MDIInterval_Leave);
            // 
            // lbl_MDIInterval
            // 
            this.lbl_MDIInterval.AutoSize = true;
            this.lbl_MDIInterval.ForeColor = System.Drawing.Color.Black;
            this.lbl_MDIInterval.Location = new System.Drawing.Point(175, 25);
            this.lbl_MDIInterval.Margin = new System.Windows.Forms.Padding(0, 25, 3, 0);
            this.lbl_MDIInterval.Name = "lbl_MDIInterval";
            this.lbl_MDIInterval.Size = new System.Drawing.Size(55, 15);
            this.lbl_MDIInterval.TabIndex = 0;
            this.lbl_MDIInterval.Text = "(Minute)";
            // 
            // pnl_SlideCount_Container
            // 
            this.pnl_SlideCount_Container.Location = new System.Drawing.Point(278, 0);
            this.pnl_SlideCount_Container.Margin = new System.Windows.Forms.Padding(45, 0, 3, 0);
            this.pnl_SlideCount_Container.Name = "pnl_SlideCount_Container";
            this.pnl_SlideCount_Container.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // pnl_SlideCount_Container.Panel1
            // 
            this.pnl_SlideCount_Container.Panel1.Controls.Add(this.tb_MDIParams_SlidesCount);
            // 
            // pnl_SlideCount_Container.Panel2
            // 
            this.pnl_SlideCount_Container.Panel2.Controls.Add(this.lbl_Val_5);
            this.pnl_SlideCount_Container.Panel2.Controls.Add(this.lbl_Val_4);
            this.pnl_SlideCount_Container.Panel2.Controls.Add(this.lbl_Val_3);
            this.pnl_SlideCount_Container.Panel2.Controls.Add(this.lbl_Val_2);
            this.pnl_SlideCount_Container.Panel2.Controls.Add(this.lbl_Val_Max);
            this.pnl_SlideCount_Container.Panel2.Controls.Add(this.lbl_Val_Min);
            this.pnl_SlideCount_Container.Panel2.Controls.Add(this.lbl_SlideCount);
            this.pnl_SlideCount_Container.Size = new System.Drawing.Size(150, 65);
            this.pnl_SlideCount_Container.SplitterDistance = 25;
            this.pnl_SlideCount_Container.TabIndex = 19;
            // 
            // tb_MDIParams_SlidesCount
            // 
            this.tb_MDIParams_SlidesCount.BackColor = System.Drawing.SystemColors.Window;
            this.tb_MDIParams_SlidesCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_MDIParams_SlidesCount.LargeChange = 2;
            this.tb_MDIParams_SlidesCount.Location = new System.Drawing.Point(0, 0);
            this.tb_MDIParams_SlidesCount.Maximum = 6;
            this.tb_MDIParams_SlidesCount.Minimum = 1;
            this.tb_MDIParams_SlidesCount.Name = "tb_MDIParams_SlidesCount";
            this.tb_MDIParams_SlidesCount.Size = new System.Drawing.Size(150, 25);
            this.tb_MDIParams_SlidesCount.TabIndex = 16;
            this.tb_MDIParams_SlidesCount.Value = 6;
            this.tb_MDIParams_SlidesCount.ValueChanged += new System.EventHandler(this.tb_MDIParams_SlidesCount_ValueChanged);
            // 
            // lbl_Val_5
            // 
            this.lbl_Val_5.AutoSize = true;
            this.lbl_Val_5.ForeColor = System.Drawing.Color.Black;
            this.lbl_Val_5.Location = new System.Drawing.Point(105, 1);
            this.lbl_Val_5.Name = "lbl_Val_5";
            this.lbl_Val_5.Size = new System.Drawing.Size(14, 15);
            this.lbl_Val_5.TabIndex = 22;
            this.lbl_Val_5.Text = "5";
            // 
            // lbl_Val_4
            // 
            this.lbl_Val_4.AutoSize = true;
            this.lbl_Val_4.ForeColor = System.Drawing.Color.Black;
            this.lbl_Val_4.Location = new System.Drawing.Point(81, 1);
            this.lbl_Val_4.Name = "lbl_Val_4";
            this.lbl_Val_4.Size = new System.Drawing.Size(14, 15);
            this.lbl_Val_4.TabIndex = 21;
            this.lbl_Val_4.Text = "4";
            // 
            // lbl_Val_3
            // 
            this.lbl_Val_3.AutoSize = true;
            this.lbl_Val_3.ForeColor = System.Drawing.Color.Black;
            this.lbl_Val_3.Location = new System.Drawing.Point(56, 1);
            this.lbl_Val_3.Name = "lbl_Val_3";
            this.lbl_Val_3.Size = new System.Drawing.Size(14, 15);
            this.lbl_Val_3.TabIndex = 20;
            this.lbl_Val_3.Text = "3";
            // 
            // lbl_Val_2
            // 
            this.lbl_Val_2.AutoSize = true;
            this.lbl_Val_2.ForeColor = System.Drawing.Color.Black;
            this.lbl_Val_2.Location = new System.Drawing.Point(32, 1);
            this.lbl_Val_2.Name = "lbl_Val_2";
            this.lbl_Val_2.Size = new System.Drawing.Size(14, 15);
            this.lbl_Val_2.TabIndex = 19;
            this.lbl_Val_2.Text = "2";
            // 
            // lbl_Val_Max
            // 
            this.lbl_Val_Max.AutoSize = true;
            this.lbl_Val_Max.ForeColor = System.Drawing.Color.Black;
            this.lbl_Val_Max.Location = new System.Drawing.Point(130, 1);
            this.lbl_Val_Max.Name = "lbl_Val_Max";
            this.lbl_Val_Max.Size = new System.Drawing.Size(14, 15);
            this.lbl_Val_Max.TabIndex = 18;
            this.lbl_Val_Max.Text = "6";
            // 
            // lbl_Val_Min
            // 
            this.lbl_Val_Min.AutoSize = true;
            this.lbl_Val_Min.ForeColor = System.Drawing.Color.Black;
            this.lbl_Val_Min.Location = new System.Drawing.Point(7, 1);
            this.lbl_Val_Min.Name = "lbl_Val_Min";
            this.lbl_Val_Min.Size = new System.Drawing.Size(14, 15);
            this.lbl_Val_Min.TabIndex = 17;
            this.lbl_Val_Min.Text = "1";
            // 
            // lbl_SlideCount
            // 
            this.lbl_SlideCount.AutoSize = true;
            this.lbl_SlideCount.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SlideCount.ForeColor = System.Drawing.Color.Black;
            this.lbl_SlideCount.Location = new System.Drawing.Point(32, 11);
            this.lbl_SlideCount.Name = "lbl_SlideCount";
            this.lbl_SlideCount.Size = new System.Drawing.Size(85, 18);
            this.lbl_SlideCount.TabIndex = 2;
            this.lbl_SlideCount.Text = "Slide Counts";
            // 
            // gpMDIManualReset
            // 
            this.gpMDIManualReset.AutoSize = true;
            this.gpMDIManualReset.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gpMDIManualReset.Controls.Add(this.fLP_ManualReset);
            this.gpMDIManualReset.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpMDIManualReset.Location = new System.Drawing.Point(3, 20);
            this.gpMDIManualReset.Margin = new System.Windows.Forms.Padding(3, 20, 3, 0);
            this.gpMDIManualReset.Name = "gpMDIManualReset";
            this.gpMDIManualReset.Padding = new System.Windows.Forms.Padding(0);
            this.gpMDIManualReset.Size = new System.Drawing.Size(497, 147);
            this.gpMDIManualReset.TabIndex = 12;
            this.gpMDIManualReset.TabStop = false;
            this.gpMDIManualReset.Text = "MDI Manual Reset";
            // 
            // fLP_ManualReset
            // 
            this.fLP_ManualReset.AutoSize = true;
            this.fLP_ManualReset.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_ManualReset.Controls.Add(this.fLP_manual_Checks);
            this.fLP_ManualReset.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLP_ManualReset.Location = new System.Drawing.Point(-1, 10);
            this.fLP_ManualReset.Name = "fLP_ManualReset";
            this.fLP_ManualReset.Size = new System.Drawing.Size(495, 118);
            this.fLP_ManualReset.TabIndex = 0;
            // 
            // fLP_manual_Checks
            // 
            this.fLP_manual_Checks.AutoSize = true;
            this.fLP_manual_Checks.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_manual_Checks.Controls.Add(this.pnl_Manual_Reset);
            this.fLP_manual_Checks.Controls.Add(this.check_MDIParams_ManualResetByButton);
            this.fLP_manual_Checks.Controls.Add(this.check_MDIParams_ManualResetByRemote);
            this.fLP_manual_Checks.Controls.Add(this.check_MDIParams_Manual_Reset_powerDown_Mode);
            this.fLP_manual_Checks.Controls.Add(this.check_MDIParams_Auto_Reset_powerDown_Mode);
            this.fLP_manual_Checks.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fLP_manual_Checks.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLP_manual_Checks.Location = new System.Drawing.Point(3, 0);
            this.fLP_manual_Checks.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.fLP_manual_Checks.Name = "fLP_manual_Checks";
            this.fLP_manual_Checks.Size = new System.Drawing.Size(489, 118);
            this.fLP_manual_Checks.TabIndex = 13;
            // 
            // pnl_Manual_Reset
            // 
            this.pnl_Manual_Reset.AutoSize = true;
            this.pnl_Manual_Reset.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnl_Manual_Reset.Controls.Add(this.lbl_ManualResets);
            this.pnl_Manual_Reset.Controls.Add(this.lbl_ValidationRange);
            this.pnl_Manual_Reset.Controls.Add(this.radio_MDI_h);
            this.pnl_Manual_Reset.Controls.Add(this.radio_MDI_d);
            this.pnl_Manual_Reset.Controls.Add(this.radio_MDI_min);
            this.pnl_Manual_Reset.Controls.Add(this.radio_MDI_s);
            this.pnl_Manual_Reset.Controls.Add(this.txt_MDIParams_minTime);
            this.pnl_Manual_Reset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Manual_Reset.Location = new System.Drawing.Point(3, 0);
            this.pnl_Manual_Reset.Margin = new System.Windows.Forms.Padding(3, 0, 25, 0);
            this.pnl_Manual_Reset.Name = "pnl_Manual_Reset";
            this.pnl_Manual_Reset.Size = new System.Drawing.Size(461, 42);
            this.pnl_Manual_Reset.TabIndex = 10;
            // 
            // lbl_ManualResets
            // 
            this.lbl_ManualResets.AutoSize = true;
            this.lbl_ManualResets.ForeColor = System.Drawing.Color.Black;
            this.lbl_ManualResets.Location = new System.Drawing.Point(0, 5);
            this.lbl_ManualResets.Name = "lbl_ManualResets";
            this.lbl_ManualResets.Size = new System.Drawing.Size(246, 15);
            this.lbl_ManualResets.TabIndex = 0;
            this.lbl_ManualResets.Text = "Minimum Time Interval B/w  Manual Resets";
            // 
            // lbl_ValidationRange
            // 
            this.lbl_ValidationRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_ValidationRange.AutoSize = true;
            this.lbl_ValidationRange.ForeColor = System.Drawing.Color.Black;
            this.lbl_ValidationRange.Location = new System.Drawing.Point(274, 23);
            this.lbl_ValidationRange.Name = "lbl_ValidationRange";
            this.lbl_ValidationRange.Size = new System.Drawing.Size(53, 15);
            this.lbl_ValidationRange.TabIndex = 8;
            this.lbl_ValidationRange.Text = "[*0-255]";
            // 
            // radio_MDI_h
            // 
            this.radio_MDI_h.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radio_MDI_h.AutoSize = true;
            this.radio_MDI_h.ForeColor = System.Drawing.Color.Black;
            this.radio_MDI_h.Location = new System.Drawing.Point(408, -2);
            this.radio_MDI_h.Name = "radio_MDI_h";
            this.radio_MDI_h.Size = new System.Drawing.Size(52, 19);
            this.radio_MDI_h.TabIndex = 2;
            this.radio_MDI_h.Text = "Hour";
            this.radio_MDI_h.UseVisualStyleBackColor = true;
            this.radio_MDI_h.Click += new System.EventHandler(this.radio_MDI_h_Click);
            // 
            // radio_MDI_d
            // 
            this.radio_MDI_d.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radio_MDI_d.AutoSize = true;
            this.radio_MDI_d.ForeColor = System.Drawing.Color.Black;
            this.radio_MDI_d.Location = new System.Drawing.Point(408, 20);
            this.radio_MDI_d.Name = "radio_MDI_d";
            this.radio_MDI_d.Size = new System.Drawing.Size(45, 19);
            this.radio_MDI_d.TabIndex = 4;
            this.radio_MDI_d.Text = "Day";
            this.radio_MDI_d.UseVisualStyleBackColor = true;
            this.radio_MDI_d.Click += new System.EventHandler(this.radio_MDI_d_Click);
            // 
            // radio_MDI_min
            // 
            this.radio_MDI_min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radio_MDI_min.AutoSize = true;
            this.radio_MDI_min.ForeColor = System.Drawing.Color.Black;
            this.radio_MDI_min.Location = new System.Drawing.Point(357, 19);
            this.radio_MDI_min.Name = "radio_MDI_min";
            this.radio_MDI_min.Size = new System.Drawing.Size(46, 19);
            this.radio_MDI_min.TabIndex = 3;
            this.radio_MDI_min.Text = "Min";
            this.radio_MDI_min.UseVisualStyleBackColor = true;
            this.radio_MDI_min.Click += new System.EventHandler(this.radio_MDI_min_Click);
            // 
            // radio_MDI_s
            // 
            this.radio_MDI_s.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radio_MDI_s.AutoSize = true;
            this.radio_MDI_s.Checked = true;
            this.radio_MDI_s.ForeColor = System.Drawing.Color.Black;
            this.radio_MDI_s.Location = new System.Drawing.Point(357, 1);
            this.radio_MDI_s.Name = "radio_MDI_s";
            this.radio_MDI_s.Size = new System.Drawing.Size(43, 19);
            this.radio_MDI_s.TabIndex = 1;
            this.radio_MDI_s.TabStop = true;
            this.radio_MDI_s.Text = "Sec";
            this.radio_MDI_s.UseVisualStyleBackColor = true;
            this.radio_MDI_s.Click += new System.EventHandler(this.radio_MDI_s_Click);
            // 
            // txt_MDIParams_minTime
            // 
            this.txt_MDIParams_minTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_MDIParams_minTime.Location = new System.Drawing.Point(279, 0);
            this.txt_MDIParams_minTime.MaxLength = 3;
            this.txt_MDIParams_minTime.Name = "txt_MDIParams_minTime";
            this.txt_MDIParams_minTime.Size = new System.Drawing.Size(68, 23);
            this.txt_MDIParams_minTime.TabIndex = 0;
            this.txt_MDIParams_minTime.TextChanged += new System.EventHandler(this.txt_MDIParams_minTime_TextChanged);
            this.txt_MDIParams_minTime.Leave += new System.EventHandler(this.txt_MDIParams_minTime_Leave);
            // 
            // check_MDIParams_ManualResetByButton
            // 
            this.check_MDIParams_ManualResetByButton.AutoSize = true;
            this.check_MDIParams_ManualResetByButton.ForeColor = System.Drawing.Color.Black;
            this.check_MDIParams_ManualResetByButton.Location = new System.Drawing.Point(3, 42);
            this.check_MDIParams_ManualResetByButton.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.check_MDIParams_ManualResetByButton.Name = "check_MDIParams_ManualResetByButton";
            this.check_MDIParams_ManualResetByButton.Size = new System.Drawing.Size(156, 19);
            this.check_MDIParams_ManualResetByButton.TabIndex = 5;
            this.check_MDIParams_ManualResetByButton.Text = "Manual Reset By Button";
            this.check_MDIParams_ManualResetByButton.UseVisualStyleBackColor = true;
            this.check_MDIParams_ManualResetByButton.CheckStateChanged += new System.EventHandler(this.check_MDIParams_ManualResetByButton_CheckStateChanged);
            // 
            // check_MDIParams_ManualResetByRemote
            // 
            this.check_MDIParams_ManualResetByRemote.AutoSize = true;
            this.check_MDIParams_ManualResetByRemote.ForeColor = System.Drawing.Color.Black;
            this.check_MDIParams_ManualResetByRemote.Location = new System.Drawing.Point(3, 61);
            this.check_MDIParams_ManualResetByRemote.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.check_MDIParams_ManualResetByRemote.Name = "check_MDIParams_ManualResetByRemote";
            this.check_MDIParams_ManualResetByRemote.Size = new System.Drawing.Size(222, 19);
            this.check_MDIParams_ManualResetByRemote.TabIndex = 6;
            this.check_MDIParams_ManualResetByRemote.Text = "Manual Reset By Remote Command";
            this.check_MDIParams_ManualResetByRemote.UseVisualStyleBackColor = true;
            this.check_MDIParams_ManualResetByRemote.CheckStateChanged += new System.EventHandler(this.check_MDIParams_ManualResetByRemote_CheckStateChanged);
            // 
            // check_MDIParams_Manual_Reset_powerDown_Mode
            // 
            this.check_MDIParams_Manual_Reset_powerDown_Mode.AutoSize = true;
            this.check_MDIParams_Manual_Reset_powerDown_Mode.ForeColor = System.Drawing.Color.Black;
            this.check_MDIParams_Manual_Reset_powerDown_Mode.Location = new System.Drawing.Point(3, 80);
            this.check_MDIParams_Manual_Reset_powerDown_Mode.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.check_MDIParams_Manual_Reset_powerDown_Mode.Name = "check_MDIParams_Manual_Reset_powerDown_Mode";
            this.check_MDIParams_Manual_Reset_powerDown_Mode.Size = new System.Drawing.Size(219, 19);
            this.check_MDIParams_Manual_Reset_powerDown_Mode.TabIndex = 7;
            this.check_MDIParams_Manual_Reset_powerDown_Mode.Text = "Manual Reset in PowerDown Mode";
            this.check_MDIParams_Manual_Reset_powerDown_Mode.UseVisualStyleBackColor = true;
            this.check_MDIParams_Manual_Reset_powerDown_Mode.CheckStateChanged += new System.EventHandler(this.check_MDIParams_Manual_Reset_powerDown_Mode_CheckStateChanged);
            // 
            // check_MDIParams_Auto_Reset_powerDown_Mode
            // 
            this.check_MDIParams_Auto_Reset_powerDown_Mode.AutoSize = true;
            this.check_MDIParams_Auto_Reset_powerDown_Mode.ForeColor = System.Drawing.Color.Black;
            this.check_MDIParams_Auto_Reset_powerDown_Mode.Location = new System.Drawing.Point(3, 99);
            this.check_MDIParams_Auto_Reset_powerDown_Mode.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.check_MDIParams_Auto_Reset_powerDown_Mode.Name = "check_MDIParams_Auto_Reset_powerDown_Mode";
            this.check_MDIParams_Auto_Reset_powerDown_Mode.Size = new System.Drawing.Size(248, 19);
            this.check_MDIParams_Auto_Reset_powerDown_Mode.TabIndex = 9;
            this.check_MDIParams_Auto_Reset_powerDown_Mode.Text = "Disable Auto Reset in PowerDown Mode";
            this.check_MDIParams_Auto_Reset_powerDown_Mode.UseVisualStyleBackColor = true;
            this.check_MDIParams_Auto_Reset_powerDown_Mode.CheckedChanged += new System.EventHandler(this.check_MDIParams_Auto_Reset_powerDown_Mode_CheckedChanged);
            // 
            // gpMDIResetParam
            // 
            this.gpMDIResetParam.AutoSize = true;
            this.gpMDIResetParam.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gpMDIResetParam.Controls.Add(this.fLP_Main);
            this.gpMDIResetParam.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.gpMDIResetParam.ForeColor = System.Drawing.Color.Maroon;
            this.gpMDIResetParam.Location = new System.Drawing.Point(0, 0);
            this.gpMDIResetParam.Margin = new System.Windows.Forms.Padding(0);
            this.gpMDIResetParam.Name = "gpMDIResetParam";
            this.gpMDIResetParam.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.gpMDIResetParam.Size = new System.Drawing.Size(503, 349);
            this.gpMDIResetParam.TabIndex = 11;
            this.gpMDIResetParam.TabStop = false;
            this.gpMDIResetParam.Text = "MDI Params";
            // 
            // fLP_Main
            // 
            this.fLP_Main.AutoSize = true;
            this.fLP_Main.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_Main.Controls.Add(this.gpMDIManualReset);
            this.fLP_Main.Controls.Add(this.gpMDIAutoReset);
            this.fLP_Main.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLP_Main.Location = new System.Drawing.Point(0, 3);
            this.fLP_Main.Margin = new System.Windows.Forms.Padding(0);
            this.fLP_Main.Name = "fLP_Main";
            this.fLP_Main.Size = new System.Drawing.Size(503, 327);
            this.fLP_Main.TabIndex = 0;
            // 
            // ucMDIParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.gpMDIResetParam);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucMDIParams";
            this.Size = new System.Drawing.Size(503, 349);
            this.Load += new System.EventHandler(this.ucMDIParams_Load);
            this.Leave += new System.EventHandler(this.ucMDIParams_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.gpMDIAutoReset.ResumeLayout(false);
            this.gpMDIAutoReset.PerformLayout();
            this.fLP_AutoReset.ResumeLayout(false);
            this.fLP_AutoReset.PerformLayout();
            this.pnl_AutoResetEnable.ResumeLayout(false);
            this.pnl_AutoResetEnable.PerformLayout();
            this.pnl_AutoResetDateTime.ResumeLayout(false);
            this.pnl_AutoResetDateTime.PerformLayout();
            this.fLP_MDI_Interval.ResumeLayout(false);
            this.fLP_MDI_Interval.PerformLayout();
            this.pnl_SlideCount_Container.Panel1.ResumeLayout(false);
            this.pnl_SlideCount_Container.Panel1.PerformLayout();
            this.pnl_SlideCount_Container.Panel2.ResumeLayout(false);
            this.pnl_SlideCount_Container.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_SlideCount_Container)).EndInit();
            this.pnl_SlideCount_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tb_MDIParams_SlidesCount)).EndInit();
            this.gpMDIManualReset.ResumeLayout(false);
            this.gpMDIManualReset.PerformLayout();
            this.fLP_ManualReset.ResumeLayout(false);
            this.fLP_ManualReset.PerformLayout();
            this.fLP_manual_Checks.ResumeLayout(false);
            this.fLP_manual_Checks.PerformLayout();
            this.pnl_Manual_Reset.ResumeLayout(false);
            this.pnl_Manual_Reset.PerformLayout();
            this.gpMDIResetParam.ResumeLayout(false);
            this.gpMDIResetParam.PerformLayout();
            this.fLP_Main.ResumeLayout(false);
            this.fLP_Main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.GroupBox gpMDIResetParam;
        private System.Windows.Forms.GroupBox gpMDIManualReset;
        private System.Windows.Forms.Label lbl_ValidationRange;
        private System.Windows.Forms.Label lbl_ManualResets;
        private System.Windows.Forms.CheckBox check_MDIParams_Manual_Reset_powerDown_Mode;
        private System.Windows.Forms.CheckBox check_MDIParams_ManualResetByRemote;
        private System.Windows.Forms.RadioButton radio_MDI_h;
        private System.Windows.Forms.CheckBox check_MDIParams_ManualResetByButton;
        private System.Windows.Forms.TextBox txt_MDIParams_minTime;
        private System.Windows.Forms.RadioButton radio_MDI_s;
        private System.Windows.Forms.RadioButton radio_MDI_min;
        private System.Windows.Forms.RadioButton radio_MDI_d;
        private System.Windows.Forms.GroupBox gpMDIAutoReset;
        private System.Windows.Forms.SplitContainer pnl_SlideCount_Container;
        private System.Windows.Forms.TrackBar tb_MDIParams_SlidesCount;
        private System.Windows.Forms.Label lbl_Val_5;
        private System.Windows.Forms.Label lbl_Val_4;
        private System.Windows.Forms.Label lbl_Val_3;
        private System.Windows.Forms.Label lbl_Val_2;
        private System.Windows.Forms.Label lbl_Val_Max;
        private System.Windows.Forms.Label lbl_Val_Min;
        private System.Windows.Forms.Label lbl_SlideCount;
        private System.Windows.Forms.CheckBox check_MDIParams_Autoreset;
        private System.Windows.Forms.Label lblMDIInterval;
        private System.Windows.Forms.Label lbl_MDIInterval;
        private System.Windows.Forms.ComboBox combo_MDIParams_MDIInterval;
        private ucCustomDateTime ucMDIAutoResetDateParam;
        private System.Windows.Forms.Label lbl_AutoResetDateTime;
        private System.Windows.Forms.CheckBox check_MDIParams_Auto_Reset_powerDown_Mode;
        private System.Windows.Forms.Panel pnl_Manual_Reset;
        private System.Windows.Forms.FlowLayoutPanel fLP_manual_Checks;
        private System.Windows.Forms.FlowLayoutPanel fLP_ManualReset;
        private System.Windows.Forms.Panel pnl_AutoResetEnable;
        private System.Windows.Forms.FlowLayoutPanel fLP_MDI_Interval;
        private System.Windows.Forms.FlowLayoutPanel fLP_AutoReset;
        private System.Windows.Forms.FlowLayoutPanel fLP_Main;
        private System.Windows.Forms.Panel pnl_AutoResetDateTime;
        private ucDateTimeChooser.ucCustomDateTimePicker ucAutoResetDateParam;
    }
}
