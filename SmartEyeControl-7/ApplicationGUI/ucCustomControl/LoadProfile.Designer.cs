using System.Windows.Forms;
namespace ucCustomControl
{
    partial class pnlLoadProfile
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pnlLoadProfile));
            this.grid_LoadProfile = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.btn_GetLoadProfile = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_ClearGrid = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.loadProfileBgW = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblHeading = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Rpt_LoadProfile = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.bgw_GetEntries = new System.ComponentModel.BackgroundWorker();
            this.check_SaveToDB = new System.Windows.Forms.CheckBox();
            this.btnGenerateChart = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnGeneratePerDayReport = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbLoadProfileType = new System.Windows.Forms.ComboBox();
            this.rbDateWiseLP = new System.Windows.Forms.CheckBox();
            this.tbLoadProfile = new System.Windows.Forms.TabControl();
            this.tpRange = new System.Windows.Forms.TabPage();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lbl_fromTxt = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lbl_ToTxt = new System.Windows.Forms.Label();
            this.tpEntry = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.combo_ToEntry = new System.Windows.Forms.ComboBox();
            this.combo_FromEntry = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_getloadprofile_Entries = new System.Windows.Forms.Button();
            this.gpLPChannelFilter = new System.Windows.Forms.GroupBox();
            this.tb_LPFilterView = new System.Windows.Forms.TabControl();
            this.tbChannelCount = new System.Windows.Forms.TabPage();
            this.cmb_HorChCountFilter = new System.Windows.Forms.ComboBox();
            this.tbGroupSelection = new System.Windows.Forms.TabPage();
            this.tbChannels = new System.Windows.Forms.TabPage();
            this.chkLPChannels = new System.Windows.Forms.CheckedListBox();
            this.btnGetChannels = new System.Windows.Forms.Button();
            this.chk_channel_filter = new System.Windows.Forms.CheckBox();
            this.lblLPReadEntries = new System.Windows.Forms.Label();
            this.lblLPEtries = new System.Windows.Forms.Label();
            this.cmbPerDayDates = new System.Windows.Forms.ComboBox();
            this.cmbChannelNumber = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.grid_LoadProfile)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tbLoadProfile.SuspendLayout();
            this.tpRange.SuspendLayout();
            this.tpEntry.SuspendLayout();
            this.gpLPChannelFilter.SuspendLayout();
            this.tb_LPFilterView.SuspendLayout();
            this.tbChannelCount.SuspendLayout();
            this.tbChannels.SuspendLayout();
            this.SuspendLayout();
            // 
            // grid_LoadProfile
            // 
            this.grid_LoadProfile.AllowUserToAddRows = false;
            this.grid_LoadProfile.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grid_LoadProfile.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grid_LoadProfile.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grid_LoadProfile.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.grid_LoadProfile.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.grid_LoadProfile.ColumnHeadersHeight = 45;
            this.grid_LoadProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.grid_LoadProfile.Location = new System.Drawing.Point(0, 19);
            this.grid_LoadProfile.Name = "grid_LoadProfile";
            this.grid_LoadProfile.ReadOnly = true;
            this.grid_LoadProfile.RowHeadersWidth = 40;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grid_LoadProfile.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.grid_LoadProfile.Size = new System.Drawing.Size(824, 382);
            this.grid_LoadProfile.TabIndex = 1;
            this.grid_LoadProfile.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_LoadProfile_CellDoubleClick);
            // 
            // btn_GetLoadProfile
            // 
            this.btn_GetLoadProfile.AutoSize = true;
            this.btn_GetLoadProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_GetLoadProfile.Location = new System.Drawing.Point(164, 60);
            this.btn_GetLoadProfile.Name = "btn_GetLoadProfile";
            this.btn_GetLoadProfile.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_GetLoadProfile.Size = new System.Drawing.Size(120, 30);
            this.btn_GetLoadProfile.TabIndex = 2;
            this.btn_GetLoadProfile.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_GetLoadProfile.Values.Image")));
            this.btn_GetLoadProfile.Values.Text = "Get Load Profile";
            this.btn_GetLoadProfile.Click += new System.EventHandler(this.btn_GetLoadProfile_Click);
            // 
            // btn_ClearGrid
            // 
            this.btn_ClearGrid.AutoSize = true;
            this.btn_ClearGrid.Location = new System.Drawing.Point(18, 60);
            this.btn_ClearGrid.Name = "btn_ClearGrid";
            this.btn_ClearGrid.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_ClearGrid.Size = new System.Drawing.Size(120, 30);
            this.btn_ClearGrid.TabIndex = 3;
            this.btn_ClearGrid.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_ClearGrid.Values.Image")));
            this.btn_ClearGrid.Values.Text = "Clear Grid";
            this.btn_ClearGrid.Visible = false;
            this.btn_ClearGrid.Click += new System.EventHandler(this.btn_ClearGrid_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(18, 540);
            this.progressBar1.MarqueeAnimationSpeed = 35;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(719, 31);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 4;
            this.progressBar1.Visible = false;
            // 
            // lblHeading
            // 
            this.lblHeading.BackColor = System.Drawing.Color.Transparent;
            this.lblHeading.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.Black;
            this.lblHeading.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblHeading.Location = new System.Drawing.Point(22, 1);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(1107, 29);
            this.lblHeading.TabIndex = 13;
            this.lblHeading.Text = "Load Profile";
            this.lblHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.grid_LoadProfile);
            this.groupBox1.Location = new System.Drawing.Point(18, 114);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(830, 420);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Load Profile";
            // 
            // btn_Rpt_LoadProfile
            // 
            this.btn_Rpt_LoadProfile.Location = new System.Drawing.Point(301, 60);
            this.btn_Rpt_LoadProfile.Name = "btn_Rpt_LoadProfile";
            this.btn_Rpt_LoadProfile.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_Rpt_LoadProfile.Size = new System.Drawing.Size(120, 30);
            this.btn_Rpt_LoadProfile.TabIndex = 15;
            this.btn_Rpt_LoadProfile.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_Rpt_LoadProfile.Values.Image")));
            this.btn_Rpt_LoadProfile.Values.Text = "Generate Report";
            this.btn_Rpt_LoadProfile.Visible = false;
            this.btn_Rpt_LoadProfile.Click += new System.EventHandler(this.btn_Rpt_LoadProfile_Click);
            // 
            // bgw_GetEntries
            // 
            this.bgw_GetEntries.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_GetEntries_DoWork);
            this.bgw_GetEntries.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_GetEntries_RunWorkerCompleted);
            // 
            // check_SaveToDB
            // 
            this.check_SaveToDB.AutoSize = true;
            this.check_SaveToDB.BackColor = System.Drawing.Color.Transparent;
            this.check_SaveToDB.Location = new System.Drawing.Point(763, 554);
            this.check_SaveToDB.Name = "check_SaveToDB";
            this.check_SaveToDB.Size = new System.Drawing.Size(85, 17);
            this.check_SaveToDB.TabIndex = 21;
            this.check_SaveToDB.Text = "Save To DB";
            this.check_SaveToDB.UseVisualStyleBackColor = false;
            this.check_SaveToDB.Visible = false;
            // 
            // btnGenerateChart
            // 
            this.btnGenerateChart.Location = new System.Drawing.Point(832, 60);
            this.btnGenerateChart.Name = "btnGenerateChart";
            this.btnGenerateChart.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnGenerateChart.Size = new System.Drawing.Size(120, 30);
            this.btnGenerateChart.TabIndex = 22;
            this.btnGenerateChart.Values.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerateChart.Values.Image")));
            this.btnGenerateChart.Values.Text = "Graph Report";
            this.btnGenerateChart.Click += new System.EventHandler(this.btnGenerateChart_Click);
            // 
            // btnGeneratePerDayReport
            // 
            this.btnGeneratePerDayReport.Location = new System.Drawing.Point(579, 60);
            this.btnGeneratePerDayReport.Name = "btnGeneratePerDayReport";
            this.btnGeneratePerDayReport.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnGeneratePerDayReport.Size = new System.Drawing.Size(120, 30);
            this.btnGeneratePerDayReport.TabIndex = 23;
            this.btnGeneratePerDayReport.Values.Image = ((System.Drawing.Image)(resources.GetObject("btnGeneratePerDayReport.Values.Image")));
            this.btnGeneratePerDayReport.Values.Text = "Per Day Report";
            this.btnGeneratePerDayReport.Click += new System.EventHandler(this.btnGeneratePerDayReport_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.cmbLoadProfileType);
            this.groupBox2.Controls.Add(this.rbDateWiseLP);
            this.groupBox2.Location = new System.Drawing.Point(872, 119);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(253, 87);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Load Profile Settings";
            // 
            // cmbLoadProfileType
            // 
            this.cmbLoadProfileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoadProfileType.FormattingEnabled = true;
            this.cmbLoadProfileType.Location = new System.Drawing.Point(25, 21);
            this.cmbLoadProfileType.Name = "cmbLoadProfileType";
            this.cmbLoadProfileType.Size = new System.Drawing.Size(157, 21);
            this.cmbLoadProfileType.TabIndex = 5;
            this.cmbLoadProfileType.SelectedIndexChanged += new System.EventHandler(this.cmbLoadProfileType_SelectedIndexChanged);
            // 
            // rbDateWiseLP
            // 
            this.rbDateWiseLP.AutoSize = true;
            this.rbDateWiseLP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDateWiseLP.Location = new System.Drawing.Point(25, 51);
            this.rbDateWiseLP.Name = "rbDateWiseLP";
            this.rbDateWiseLP.Size = new System.Drawing.Size(157, 17);
            this.rbDateWiseLP.TabIndex = 4;
            this.rbDateWiseLP.Text = "Date Wise Load Profile";
            this.rbDateWiseLP.UseVisualStyleBackColor = true;
            this.rbDateWiseLP.CheckedChanged += new System.EventHandler(this.rbScheme1_CheckedChanged);
            // 
            // tbLoadProfile
            // 
            this.tbLoadProfile.Controls.Add(this.tpRange);
            this.tbLoadProfile.Controls.Add(this.tpEntry);
            this.tbLoadProfile.Location = new System.Drawing.Point(872, 234);
            this.tbLoadProfile.Name = "tbLoadProfile";
            this.tbLoadProfile.SelectedIndex = 0;
            this.tbLoadProfile.Size = new System.Drawing.Size(257, 100);
            this.tbLoadProfile.TabIndex = 26;
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
            this.tpRange.Size = new System.Drawing.Size(249, 74);
            this.tpRange.TabIndex = 0;
            this.tpRange.Text = "Range Filter";
            this.tpRange.UseVisualStyleBackColor = true;
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd/MMM/yyyy HH:mm:ss";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(51, 13);
            this.dtpFrom.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(179, 20);
            this.dtpFrom.TabIndex = 3;
            // 
            // lbl_fromTxt
            // 
            this.lbl_fromTxt.AutoSize = true;
            this.lbl_fromTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbl_fromTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_fromTxt.Location = new System.Drawing.Point(9, 16);
            this.lbl_fromTxt.Margin = new System.Windows.Forms.Padding(5, 5, 5, 3);
            this.lbl_fromTxt.Name = "lbl_fromTxt";
            this.lbl_fromTxt.Size = new System.Drawing.Size(34, 13);
            this.lbl_fromTxt.TabIndex = 18;
            this.lbl_fromTxt.Text = "From";
            // 
            // dtpTo
            // 
            this.dtpTo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpTo.CustomFormat = "dd/MMM/yyyy HH:mm:ss";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(51, 46);
            this.dtpTo.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(179, 20);
            this.dtpTo.TabIndex = 3;
            // 
            // lbl_ToTxt
            // 
            this.lbl_ToTxt.AutoSize = true;
            this.lbl_ToTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbl_ToTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_ToTxt.Location = new System.Drawing.Point(18, 50);
            this.lbl_ToTxt.Margin = new System.Windows.Forms.Padding(25, 5, 5, 3);
            this.lbl_ToTxt.Name = "lbl_ToTxt";
            this.lbl_ToTxt.Size = new System.Drawing.Size(22, 13);
            this.lbl_ToTxt.TabIndex = 18;
            this.lbl_ToTxt.Text = "To";
            // 
            // tpEntry
            // 
            this.tpEntry.Controls.Add(this.label3);
            this.tpEntry.Controls.Add(this.combo_ToEntry);
            this.tpEntry.Controls.Add(this.combo_FromEntry);
            this.tpEntry.Controls.Add(this.label4);
            this.tpEntry.Controls.Add(this.btn_getloadprofile_Entries);
            this.tpEntry.Location = new System.Drawing.Point(4, 22);
            this.tpEntry.Name = "tpEntry";
            this.tpEntry.Padding = new System.Windows.Forms.Padding(3);
            this.tpEntry.Size = new System.Drawing.Size(249, 74);
            this.tpEntry.TabIndex = 1;
            this.tpEntry.Text = "Entry Filter";
            this.tpEntry.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(17, 13);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 5, 5, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "From";
            // 
            // combo_ToEntry
            // 
            this.combo_ToEntry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_ToEntry.FormattingEnabled = true;
            this.combo_ToEntry.Location = new System.Drawing.Point(68, 44);
            this.combo_ToEntry.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.combo_ToEntry.Name = "combo_ToEntry";
            this.combo_ToEntry.Size = new System.Drawing.Size(75, 21);
            this.combo_ToEntry.TabIndex = 16;
            // 
            // combo_FromEntry
            // 
            this.combo_FromEntry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_FromEntry.FormattingEnabled = true;
            this.combo_FromEntry.Location = new System.Drawing.Point(68, 10);
            this.combo_FromEntry.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.combo_FromEntry.Name = "combo_FromEntry";
            this.combo_FromEntry.Size = new System.Drawing.Size(75, 21);
            this.combo_FromEntry.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(26, 47);
            this.label4.Margin = new System.Windows.Forms.Padding(25, 5, 5, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "To";
            // 
            // btn_getloadprofile_Entries
            // 
            this.btn_getloadprofile_Entries.Image = ((System.Drawing.Image)(resources.GetObject("btn_getloadprofile_Entries.Image")));
            this.btn_getloadprofile_Entries.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_getloadprofile_Entries.Location = new System.Drawing.Point(149, 43);
            this.btn_getloadprofile_Entries.Name = "btn_getloadprofile_Entries";
            this.btn_getloadprofile_Entries.Size = new System.Drawing.Size(94, 23);
            this.btn_getloadprofile_Entries.TabIndex = 17;
            this.btn_getloadprofile_Entries.Tag = "Button";
            this.btn_getloadprofile_Entries.Text = "Get Counter";
            this.btn_getloadprofile_Entries.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_getloadprofile_Entries.Click += new System.EventHandler(this.btn_getloadprofile_Entries_Click);
            // 
            // gpLPChannelFilter
            // 
            this.gpLPChannelFilter.AutoSize = true;
            this.gpLPChannelFilter.Controls.Add(this.tb_LPFilterView);
            this.gpLPChannelFilter.Controls.Add(this.chk_channel_filter);
            this.gpLPChannelFilter.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpLPChannelFilter.ForeColor = System.Drawing.Color.Maroon;
            this.gpLPChannelFilter.Location = new System.Drawing.Point(872, 341);
            this.gpLPChannelFilter.Name = "gpLPChannelFilter";
            this.gpLPChannelFilter.Size = new System.Drawing.Size(259, 149);
            this.gpLPChannelFilter.TabIndex = 27;
            this.gpLPChannelFilter.TabStop = false;
            this.gpLPChannelFilter.Text = "Load Profile Channel Filter";
            // 
            // tb_LPFilterView
            // 
            this.tb_LPFilterView.Controls.Add(this.tbChannelCount);
            this.tb_LPFilterView.Controls.Add(this.tbGroupSelection);
            this.tb_LPFilterView.Controls.Add(this.tbChannels);
            this.tb_LPFilterView.Dock = System.Windows.Forms.DockStyle.Top;
            this.tb_LPFilterView.Location = new System.Drawing.Point(3, 19);
            this.tb_LPFilterView.Name = "tb_LPFilterView";
            this.tb_LPFilterView.SelectedIndex = 0;
            this.tb_LPFilterView.Size = new System.Drawing.Size(253, 108);
            this.tb_LPFilterView.TabIndex = 3;
            // 
            // tbChannelCount
            // 
            this.tbChannelCount.BackColor = System.Drawing.Color.Transparent;
            this.tbChannelCount.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbChannelCount.Controls.Add(this.cmb_HorChCountFilter);
            this.tbChannelCount.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.tbChannelCount.ForeColor = System.Drawing.Color.Black;
            this.tbChannelCount.Location = new System.Drawing.Point(4, 24);
            this.tbChannelCount.Name = "tbChannelCount";
            this.tbChannelCount.Size = new System.Drawing.Size(245, 80);
            this.tbChannelCount.TabIndex = 2;
            this.tbChannelCount.Text = "Channel Count";
            this.tbChannelCount.UseVisualStyleBackColor = true;
            // 
            // cmb_HorChCountFilter
            // 
            this.cmb_HorChCountFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_HorChCountFilter.FormattingEnabled = true;
            this.cmb_HorChCountFilter.Location = new System.Drawing.Point(36, 18);
            this.cmb_HorChCountFilter.MaxDropDownItems = 3;
            this.cmb_HorChCountFilter.Name = "cmb_HorChCountFilter";
            this.cmb_HorChCountFilter.Size = new System.Drawing.Size(141, 23);
            this.cmb_HorChCountFilter.TabIndex = 2;
            // 
            // tbGroupSelection
            // 
            this.tbGroupSelection.ForeColor = System.Drawing.Color.Black;
            this.tbGroupSelection.Location = new System.Drawing.Point(4, 24);
            this.tbGroupSelection.Name = "tbGroupSelection";
            this.tbGroupSelection.Size = new System.Drawing.Size(245, 80);
            this.tbGroupSelection.TabIndex = 3;
            this.tbGroupSelection.Text = "Channel Groups";
            this.tbGroupSelection.UseVisualStyleBackColor = true;
            // 
            // tbChannels
            // 
            this.tbChannels.Controls.Add(this.chkLPChannels);
            this.tbChannels.Controls.Add(this.btnGetChannels);
            this.tbChannels.Location = new System.Drawing.Point(4, 24);
            this.tbChannels.Name = "tbChannels";
            this.tbChannels.Padding = new System.Windows.Forms.Padding(3);
            this.tbChannels.Size = new System.Drawing.Size(245, 80);
            this.tbChannels.TabIndex = 4;
            this.tbChannels.Text = "Channels";
            this.tbChannels.UseVisualStyleBackColor = true;
            // 
            // chkLPChannels
            // 
            this.chkLPChannels.FormattingEnabled = true;
            this.chkLPChannels.Location = new System.Drawing.Point(3, 3);
            this.chkLPChannels.Name = "chkLPChannels";
            this.chkLPChannels.Size = new System.Drawing.Size(174, 76);
            this.chkLPChannels.TabIndex = 24;
            // 
            // btnGetChannels
            // 
            this.btnGetChannels.Image = ((System.Drawing.Image)(resources.GetObject("btnGetChannels.Image")));
            this.btnGetChannels.Location = new System.Drawing.Point(183, 6);
            this.btnGetChannels.Name = "btnGetChannels";
            this.btnGetChannels.Size = new System.Drawing.Size(59, 71);
            this.btnGetChannels.TabIndex = 23;
            this.btnGetChannels.Tag = "Button";
            this.btnGetChannels.Text = "Read Channel";
            // 
            // chk_channel_filter
            // 
            this.chk_channel_filter.AutoSize = true;
            this.chk_channel_filter.Checked = true;
            this.chk_channel_filter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_channel_filter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chk_channel_filter.ForeColor = System.Drawing.Color.Navy;
            this.chk_channel_filter.Location = new System.Drawing.Point(3, 127);
            this.chk_channel_filter.Name = "chk_channel_filter";
            this.chk_channel_filter.Size = new System.Drawing.Size(253, 19);
            this.chk_channel_filter.TabIndex = 1;
            this.chk_channel_filter.Text = "All Channels";
            this.chk_channel_filter.UseVisualStyleBackColor = true;
            // 
            // lblLPReadEntries
            // 
            this.lblLPReadEntries.AutoSize = true;
            this.lblLPReadEntries.Location = new System.Drawing.Point(829, 99);
            this.lblLPReadEntries.Name = "lblLPReadEntries";
            this.lblLPReadEntries.Size = new System.Drawing.Size(13, 13);
            this.lblLPReadEntries.TabIndex = 29;
            this.lblLPReadEntries.Text = "0";
            // 
            // lblLPEtries
            // 
            this.lblLPEtries.AutoSize = true;
            this.lblLPEtries.Location = new System.Drawing.Point(737, 98);
            this.lblLPEtries.Name = "lblLPEtries";
            this.lblLPEtries.Size = new System.Drawing.Size(86, 13);
            this.lblLPEtries.TabIndex = 28;
            this.lblLPEtries.Text = "LP Entries Count";
            // 
            // cmbPerDayDates
            // 
            this.cmbPerDayDates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPerDayDates.FormattingEnabled = true;
            this.cmbPerDayDates.Location = new System.Drawing.Point(441, 60);
            this.cmbPerDayDates.Name = "cmbPerDayDates";
            this.cmbPerDayDates.Size = new System.Drawing.Size(132, 21);
            this.cmbPerDayDates.TabIndex = 30;
            // 
            // cmbChannelNumber
            // 
            this.cmbChannelNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChannelNumber.FormattingEnabled = true;
            this.cmbChannelNumber.Items.AddRange(new object[] {
            "Channel 1",
            "Channel 2",
            "Channel 3",
            "Channel 4"});
            this.cmbChannelNumber.Location = new System.Drawing.Point(717, 60);
            this.cmbChannelNumber.Name = "cmbChannelNumber";
            this.cmbChannelNumber.Size = new System.Drawing.Size(106, 21);
            this.cmbChannelNumber.TabIndex = 31;
            // 
            // pnlLoadProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.cmbChannelNumber);
            this.Controls.Add(this.cmbPerDayDates);
            this.Controls.Add(this.lblLPReadEntries);
            this.Controls.Add(this.lblLPEtries);
            this.Controls.Add(this.gpLPChannelFilter);
            this.Controls.Add(this.tbLoadProfile);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnGenerateChart);
            this.Controls.Add(this.btnGeneratePerDayReport);
            this.Controls.Add(this.check_SaveToDB);
            this.Controls.Add(this.btn_Rpt_LoadProfile);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btn_ClearGrid);
            this.Controls.Add(this.btn_GetLoadProfile);
            this.DoubleBuffered = true;
            this.Name = "pnlLoadProfile";
            this.Size = new System.Drawing.Size(1350, 650);
            this.Load += new System.EventHandler(this.pnlLoadProfile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid_LoadProfile)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tbLoadProfile.ResumeLayout(false);
            this.tpRange.ResumeLayout(false);
            this.tpRange.PerformLayout();
            this.tpEntry.ResumeLayout(false);
            this.tpEntry.PerformLayout();
            this.gpLPChannelFilter.ResumeLayout(false);
            this.gpLPChannelFilter.PerformLayout();
            this.tb_LPFilterView.ResumeLayout(false);
            this.tbChannelCount.ResumeLayout(false);
            this.tbChannels.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

       // private System.Windows.Forms.Button btn_GetLoadProfile;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_GetLoadProfile;
         private ComponentFactory.Krypton.Toolkit.KryptonButton btn_ClearGrid;
        //private System.Windows.Forms.Button btn_ClearGrid;
        private System.ComponentModel.BackgroundWorker loadProfileBgW;
        private System.Windows.Forms.ProgressBar progressBar1;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid_LoadProfile;
        private System.Windows.Forms.Label lblHeading;
        private GroupBox groupBox1;
      //  private Button btn_Rpt_LoadProfile;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_Rpt_LoadProfile;
        private System.ComponentModel.BackgroundWorker bgw_GetEntries;
        private CheckBox check_SaveToDB;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnGenerateChart;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnGeneratePerDayReport;
        private GroupBox groupBox2;
        private ComboBox cmbLoadProfileType;
        private CheckBox rbDateWiseLP;
        private TabControl tbLoadProfile;
        private TabPage tpRange;
        private DateTimePicker dtpFrom;
        private Label lbl_fromTxt;
        private DateTimePicker dtpTo;
        private Label lbl_ToTxt;
        private TabPage tpEntry;
        private Label label3;
        private ComboBox combo_ToEntry;
        private ComboBox combo_FromEntry;
        private Label label4;
        private Button btn_getloadprofile_Entries;
        private GroupBox gpLPChannelFilter;
        private TabControl tb_LPFilterView;
        private TabPage tbChannelCount;
        private ComboBox cmb_HorChCountFilter;
        private TabPage tbGroupSelection;
        private TabPage tbChannels;
        private CheckedListBox chkLPChannels;
        private Button btnGetChannels;
        private CheckBox chk_channel_filter;
        private Label lblLPReadEntries;
        private Label lblLPEtries;
        private ComboBox cmbPerDayDates;
        private ComboBox cmbChannelNumber;
    }
}
