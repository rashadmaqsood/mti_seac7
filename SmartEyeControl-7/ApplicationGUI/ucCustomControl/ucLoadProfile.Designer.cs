namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    partial class ucLoadProfile
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.gpLoadProfile = new System.Windows.Forms.GroupBox();
            this.label186 = new System.Windows.Forms.Label();
            this.cmbLoadProfileScheme = new System.Windows.Forms.ComboBox();
            this.lblChannelCount = new System.Windows.Forms.Label();
            this.lblChannelsCount = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.cmb_LP_Period = new System.Windows.Forms.ComboBox();
            this.lblInterval = new System.Windows.Forms.Label();
            this.lstAvailableChannels = new System.Windows.Forms.ListBox();
            this.dgvLoadProfileChannels = new System.Windows.Forms.DataGridView();
            this.lblChannel_1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.gpLoadProfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoadProfileChannels)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // gpLoadProfile
            // 
            this.gpLoadProfile.Controls.Add(this.label186);
            this.gpLoadProfile.Controls.Add(this.cmbLoadProfileScheme);
            this.gpLoadProfile.Controls.Add(this.lblChannelCount);
            this.gpLoadProfile.Controls.Add(this.lblChannelsCount);
            this.gpLoadProfile.Controls.Add(this.btnClear);
            this.gpLoadProfile.Controls.Add(this.cmb_LP_Period);
            this.gpLoadProfile.Controls.Add(this.lblInterval);
            this.gpLoadProfile.Controls.Add(this.lstAvailableChannels);
            this.gpLoadProfile.Controls.Add(this.dgvLoadProfileChannels);
            this.gpLoadProfile.Controls.Add(this.lblChannel_1);
            this.gpLoadProfile.ForeColor = System.Drawing.Color.Maroon;
            this.gpLoadProfile.Location = new System.Drawing.Point(3, 2);
            this.gpLoadProfile.Name = "gpLoadProfile";
            this.gpLoadProfile.Size = new System.Drawing.Size(970, 437);
            this.gpLoadProfile.TabIndex = 1;
            this.gpLoadProfile.TabStop = false;
            this.gpLoadProfile.Text = "Load Profile";
            // 
            // label186
            // 
            this.label186.AutoSize = true;
            this.label186.Location = new System.Drawing.Point(351, 10);
            this.label186.Name = "label186";
            this.label186.Size = new System.Drawing.Size(105, 13);
            this.label186.TabIndex = 16;
            this.label186.Text = "Load Profile Scheme";
            // 
            // cmbLoadProfileScheme
            // 
            this.cmbLoadProfileScheme.BackColor = System.Drawing.Color.White;
            this.cmbLoadProfileScheme.FormattingEnabled = true;
            this.cmbLoadProfileScheme.Location = new System.Drawing.Point(354, 26);
            this.cmbLoadProfileScheme.MaxDropDownItems = 100;
            this.cmbLoadProfileScheme.Name = "cmbLoadProfileScheme";
            this.cmbLoadProfileScheme.Size = new System.Drawing.Size(205, 21);
            this.cmbLoadProfileScheme.TabIndex = 15;
            this.cmbLoadProfileScheme.SelectedIndexChanged += new System.EventHandler(this.cmbLoadProfileScheme_SelectedIndexChanged);
            // 
            // lblChannelCount
            // 
            this.lblChannelCount.AutoSize = true;
            this.lblChannelCount.Location = new System.Drawing.Point(727, 29);
            this.lblChannelCount.Name = "lblChannelCount";
            this.lblChannelCount.Size = new System.Drawing.Size(13, 13);
            this.lblChannelCount.TabIndex = 14;
            this.lblChannelCount.Text = "0";
            // 
            // lblChannelsCount
            // 
            this.lblChannelsCount.AutoSize = true;
            this.lblChannelsCount.Location = new System.Drawing.Point(576, 29);
            this.lblChannelsCount.Name = "lblChannelsCount";
            this.lblChannelsCount.Size = new System.Drawing.Size(130, 13);
            this.lblChannelsCount.TabIndex = 13;
            this.lblChannelsCount.Text = "Selected Channels Count:";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(910, 22);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(46, 24);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cmb_LP_Period
            // 
            this.cmb_LP_Period.BackColor = System.Drawing.Color.White;
            this.cmb_LP_Period.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_LP_Period.FormattingEnabled = true;
            this.cmb_LP_Period.Items.AddRange(new object[] {
            "01 mins",
            "05 mins",
            "15 mins",
            "30 mins",
            "1 hours",
            "2 hours",
            "3 hours",
            "4 hours",
            "6 hours",
            "8 hours",
            "12 hours",
            "24 hours"});
            this.cmb_LP_Period.Location = new System.Drawing.Point(806, 24);
            this.cmb_LP_Period.MaxDropDownItems = 100;
            this.cmb_LP_Period.Name = "cmb_LP_Period";
            this.cmb_LP_Period.Size = new System.Drawing.Size(98, 21);
            this.cmb_LP_Period.TabIndex = 10;
            this.cmb_LP_Period.SelectedIndexChanged += new System.EventHandler(this.txt_LoadProfile_Period_SelectedIndexChanged);
            // 
            // lblInterval
            // 
            this.lblInterval.AutoSize = true;
            this.lblInterval.Location = new System.Drawing.Point(758, 29);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(42, 13);
            this.lblInterval.TabIndex = 11;
            this.lblInterval.Text = "Interval";
            // 
            // lstAvailableChannels
            // 
            this.lstAvailableChannels.FormattingEnabled = true;
            this.lstAvailableChannels.Location = new System.Drawing.Point(9, 50);
            this.lstAvailableChannels.Name = "lstAvailableChannels";
            this.lstAvailableChannels.Size = new System.Drawing.Size(336, 381);
            this.lstAvailableChannels.TabIndex = 7;
            this.lstAvailableChannels.SelectedIndexChanged += new System.EventHandler(this.lstAvailableChannels_SelectedIndexChanged);
            this.lstAvailableChannels.DoubleClick += new System.EventHandler(this.btnAddChannel_Click);
            // 
            // dgvLoadProfileChannels
            // 
            this.dgvLoadProfileChannels.AllowUserToAddRows = false;
            this.dgvLoadProfileChannels.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLoadProfileChannels.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLoadProfileChannels.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLoadProfileChannels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoadProfileChannels.Location = new System.Drawing.Point(351, 50);
            this.dgvLoadProfileChannels.Name = "dgvLoadProfileChannels";
            this.dgvLoadProfileChannels.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLoadProfileChannels.Size = new System.Drawing.Size(612, 381);
            this.dgvLoadProfileChannels.TabIndex = 3;
            this.dgvLoadProfileChannels.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvLoadProfileChannels_KeyDown);
            // 
            // lblChannel_1
            // 
            this.lblChannel_1.AutoSize = true;
            this.lblChannel_1.Location = new System.Drawing.Point(6, 32);
            this.lblChannel_1.Name = "lblChannel_1";
            this.lblChannel_1.Size = new System.Drawing.Size(97, 13);
            this.lblChannel_1.TabIndex = 1;
            this.lblChannel_1.Text = "Available Channels";
            // 
            // ucLoadProfile
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.gpLoadProfile);
            this.DoubleBuffered = true;
            this.Name = "ucLoadProfile";
            this.Size = new System.Drawing.Size(976, 440);
            this.Load += new System.EventHandler(this.ucLoadProfile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.gpLoadProfile.ResumeLayout(false);
            this.gpLoadProfile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoadProfileChannels)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.GroupBox gpLoadProfile;
        private System.Windows.Forms.ListBox lstAvailableChannels;
        private System.Windows.Forms.DataGridView dgvLoadProfileChannels;
        private System.Windows.Forms.Label lblChannel_1;
        private System.Windows.Forms.Label label186;
        private System.Windows.Forms.ComboBox cmbLoadProfileScheme;
        private System.Windows.Forms.Label lblChannelCount;
        private System.Windows.Forms.Label lblChannelsCount;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ComboBox cmb_LP_Period;
        private System.Windows.Forms.Label lblInterval;
    }
}
