namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    partial class ucClockCalib
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
            this.gb_Clock = new System.Windows.Forms.GroupBox();
            this.txt_Clock_ClockCalibrationPPM = new System.Windows.Forms.MaskedTextBox();
            this.pnlClock = new System.Windows.Forms.Panel();
            this.radio_clock_auto = new System.Windows.Forms.RadioButton();
            this.radio_clock_manual = new System.Windows.Forms.RadioButton();
            this.label37 = new System.Windows.Forms.Label();
            this.txt_Clock_set_time = new System.Windows.Forms.DateTimePicker();
            this.txt_Clock_EndDate = new System.Windows.Forms.DateTimePicker();
            this.txt_Clock_BeginDate = new System.Windows.Forms.DateTimePicker();
            this.checkbox_Clock_PPMAdd = new System.Windows.Forms.CheckBox();
            this.checkbox_Clock_EnableDayLightSaving = new System.Windows.Forms.CheckBox();
            this.checkbox_Clock_EnableCaliberation = new System.Windows.Forms.CheckBox();
            this.label156 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.lbl_Clock_EndDate = new System.Windows.Forms.Label();
            this.lbl_Clock_BeginDate = new System.Windows.Forms.Label();
            this.lbl_Clock_ClockCalibrationPPM = new System.Windows.Forms.Label();
            this.tmr_Debug_NowTime = new System.Windows.Forms.Timer(this.components);
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.gb_Clock.SuspendLayout();
            this.pnlClock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // gb_Clock
            // 
            this.gb_Clock.Controls.Add(this.txt_Clock_ClockCalibrationPPM);
            this.gb_Clock.Controls.Add(this.pnlClock);
            this.gb_Clock.Controls.Add(this.txt_Clock_EndDate);
            this.gb_Clock.Controls.Add(this.txt_Clock_BeginDate);
            this.gb_Clock.Controls.Add(this.checkbox_Clock_PPMAdd);
            this.gb_Clock.Controls.Add(this.checkbox_Clock_EnableDayLightSaving);
            this.gb_Clock.Controls.Add(this.checkbox_Clock_EnableCaliberation);
            this.gb_Clock.Controls.Add(this.label156);
            this.gb_Clock.Controls.Add(this.label51);
            this.gb_Clock.Controls.Add(this.lbl_Clock_EndDate);
            this.gb_Clock.Controls.Add(this.lbl_Clock_BeginDate);
            this.gb_Clock.Controls.Add(this.lbl_Clock_ClockCalibrationPPM);
            this.gb_Clock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_Clock.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.gb_Clock.ForeColor = System.Drawing.Color.Maroon;
            this.gb_Clock.Location = new System.Drawing.Point(0, 0);
            this.gb_Clock.Name = "gb_Clock";
            this.gb_Clock.Size = new System.Drawing.Size(271, 340);
            this.gb_Clock.TabIndex = 6;
            this.gb_Clock.TabStop = false;
            this.gb_Clock.Text = "Clock";
            // 
            // txt_Clock_ClockCalibrationPPM
            // 
            this.txt_Clock_ClockCalibrationPPM.Location = new System.Drawing.Point(155, 176);
            this.txt_Clock_ClockCalibrationPPM.Mask = "00.000000";
            this.txt_Clock_ClockCalibrationPPM.Name = "txt_Clock_ClockCalibrationPPM";
            this.txt_Clock_ClockCalibrationPPM.Size = new System.Drawing.Size(103, 23);
            this.txt_Clock_ClockCalibrationPPM.TabIndex = 3;
            this.txt_Clock_ClockCalibrationPPM.Text = "32678000";
            this.txt_Clock_ClockCalibrationPPM.TextChanged += new System.EventHandler(this.txt_Clock_ClockCalibrationPPM_TextChanged);
            // 
            // pnlClock
            // 
            this.pnlClock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlClock.Controls.Add(this.radio_clock_auto);
            this.pnlClock.Controls.Add(this.radio_clock_manual);
            this.pnlClock.Controls.Add(this.label37);
            this.pnlClock.Controls.Add(this.txt_Clock_set_time);
            this.pnlClock.Location = new System.Drawing.Point(14, 234);
            this.pnlClock.Name = "pnlClock";
            this.pnlClock.Size = new System.Drawing.Size(220, 95);
            this.pnlClock.TabIndex = 37;
            // 
            // radio_clock_auto
            // 
            this.radio_clock_auto.AutoSize = true;
            this.radio_clock_auto.ForeColor = System.Drawing.Color.Navy;
            this.radio_clock_auto.Location = new System.Drawing.Point(128, 71);
            this.radio_clock_auto.Name = "radio_clock_auto";
            this.radio_clock_auto.Size = new System.Drawing.Size(52, 19);
            this.radio_clock_auto.TabIndex = 37;
            this.radio_clock_auto.Text = "Auto";
            this.radio_clock_auto.UseVisualStyleBackColor = true;
            this.radio_clock_auto.CheckedChanged += new System.EventHandler(this.rb_Debug_Now_Click);
            // 
            // radio_clock_manual
            // 
            this.radio_clock_manual.AutoSize = true;
            this.radio_clock_manual.Checked = true;
            this.radio_clock_manual.ForeColor = System.Drawing.Color.Navy;
            this.radio_clock_manual.Location = new System.Drawing.Point(41, 71);
            this.radio_clock_manual.Name = "radio_clock_manual";
            this.radio_clock_manual.Size = new System.Drawing.Size(65, 19);
            this.radio_clock_manual.TabIndex = 37;
            this.radio_clock_manual.TabStop = true;
            this.radio_clock_manual.Text = "Manual";
            this.radio_clock_manual.UseVisualStyleBackColor = true;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.ForeColor = System.Drawing.Color.Navy;
            this.label37.Location = new System.Drawing.Point(26, 13);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(136, 18);
            this.label37.TabIndex = 9;
            this.label37.Text = "Set Meter Date Time";
            // 
            // txt_Clock_set_time
            // 
            this.txt_Clock_set_time.CustomFormat = "dd-MM-yyyy   HH:mm:ss";
            this.txt_Clock_set_time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txt_Clock_set_time.Location = new System.Drawing.Point(29, 44);
            this.txt_Clock_set_time.MaxDate = new System.DateTime(2551, 1, 1, 0, 0, 0, 0);
            this.txt_Clock_set_time.MinDate = new System.DateTime(1950, 1, 1, 0, 0, 0, 0);
            this.txt_Clock_set_time.Name = "txt_Clock_set_time";
            this.txt_Clock_set_time.Size = new System.Drawing.Size(161, 23);
            this.txt_Clock_set_time.TabIndex = 0;
            this.txt_Clock_set_time.Value = new System.DateTime(2004, 11, 29, 0, 0, 0, 0);
            this.txt_Clock_set_time.Leave += new System.EventHandler(this.txt_Clock_set_time_Leave);
            // 
            // txt_Clock_EndDate
            // 
            this.txt_Clock_EndDate.CustomFormat = "MMM:dd";
            this.txt_Clock_EndDate.Enabled = false;
            this.txt_Clock_EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txt_Clock_EndDate.Location = new System.Drawing.Point(103, 68);
            this.txt_Clock_EndDate.MaxDate = new System.DateTime(2551, 1, 1, 0, 0, 0, 0);
            this.txt_Clock_EndDate.MinDate = new System.DateTime(1950, 1, 1, 0, 0, 0, 0);
            this.txt_Clock_EndDate.Name = "txt_Clock_EndDate";
            this.txt_Clock_EndDate.ShowUpDown = true;
            this.txt_Clock_EndDate.Size = new System.Drawing.Size(77, 23);
            this.txt_Clock_EndDate.TabIndex = 1;
            this.txt_Clock_EndDate.ValueChanged += new System.EventHandler(this.txt_Clock_EndDate_ValueChanged);
            this.txt_Clock_EndDate.Leave += new System.EventHandler(this.txt_Clock_EndDate_Leave);
            // 
            // txt_Clock_BeginDate
            // 
            this.txt_Clock_BeginDate.CustomFormat = "MMM:dd";
            this.txt_Clock_BeginDate.Enabled = false;
            this.txt_Clock_BeginDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txt_Clock_BeginDate.Location = new System.Drawing.Point(103, 42);
            this.txt_Clock_BeginDate.MaxDate = new System.DateTime(2551, 1, 1, 0, 0, 0, 0);
            this.txt_Clock_BeginDate.MinDate = new System.DateTime(1950, 1, 1, 0, 0, 0, 0);
            this.txt_Clock_BeginDate.Name = "txt_Clock_BeginDate";
            this.txt_Clock_BeginDate.ShowUpDown = true;
            this.txt_Clock_BeginDate.Size = new System.Drawing.Size(77, 23);
            this.txt_Clock_BeginDate.TabIndex = 0;
            this.txt_Clock_BeginDate.ValueChanged += new System.EventHandler(this.txt_Clock_BeginDate_ValueChanged);
            this.txt_Clock_BeginDate.Leave += new System.EventHandler(this.txt_Clock_BeginDate_Leave);
            // 
            // checkbox_Clock_PPMAdd
            // 
            this.checkbox_Clock_PPMAdd.AutoSize = true;
            this.checkbox_Clock_PPMAdd.Enabled = false;
            this.checkbox_Clock_PPMAdd.ForeColor = System.Drawing.Color.Navy;
            this.checkbox_Clock_PPMAdd.Location = new System.Drawing.Point(155, 128);
            this.checkbox_Clock_PPMAdd.Name = "checkbox_Clock_PPMAdd";
            this.checkbox_Clock_PPMAdd.Size = new System.Drawing.Size(76, 19);
            this.checkbox_Clock_PPMAdd.TabIndex = 2;
            this.checkbox_Clock_PPMAdd.Text = "PPM Add";
            this.checkbox_Clock_PPMAdd.UseVisualStyleBackColor = true;
            this.checkbox_Clock_PPMAdd.Visible = false;
            this.checkbox_Clock_PPMAdd.CheckedChanged += new System.EventHandler(this.checkbox_Clock_PPMAdd_CheckStateChanged);
            // 
            // checkbox_Clock_EnableDayLightSaving
            // 
            this.checkbox_Clock_EnableDayLightSaving.AutoSize = true;
            this.checkbox_Clock_EnableDayLightSaving.Enabled = false;
            this.checkbox_Clock_EnableDayLightSaving.ForeColor = System.Drawing.Color.Navy;
            this.checkbox_Clock_EnableDayLightSaving.Location = new System.Drawing.Point(14, 100);
            this.checkbox_Clock_EnableDayLightSaving.Name = "checkbox_Clock_EnableDayLightSaving";
            this.checkbox_Clock_EnableDayLightSaving.Size = new System.Drawing.Size(149, 19);
            this.checkbox_Clock_EnableDayLightSaving.TabIndex = 6;
            this.checkbox_Clock_EnableDayLightSaving.Text = "Enable Day light Saving";
            this.checkbox_Clock_EnableDayLightSaving.UseVisualStyleBackColor = true;
            this.checkbox_Clock_EnableDayLightSaving.CheckedChanged += new System.EventHandler(this.checkbox_Clock_EnableDayLightSaving_CheckStateChanged);
            // 
            // checkbox_Clock_EnableCaliberation
            // 
            this.checkbox_Clock_EnableCaliberation.AutoSize = true;
            this.checkbox_Clock_EnableCaliberation.Checked = true;
            this.checkbox_Clock_EnableCaliberation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkbox_Clock_EnableCaliberation.Enabled = false;
            this.checkbox_Clock_EnableCaliberation.ForeColor = System.Drawing.Color.Navy;
            this.checkbox_Clock_EnableCaliberation.Location = new System.Drawing.Point(14, 131);
            this.checkbox_Clock_EnableCaliberation.Name = "checkbox_Clock_EnableCaliberation";
            this.checkbox_Clock_EnableCaliberation.Size = new System.Drawing.Size(134, 19);
            this.checkbox_Clock_EnableCaliberation.TabIndex = 4;
            this.checkbox_Clock_EnableCaliberation.Text = "Enable Caliberation ";
            this.checkbox_Clock_EnableCaliberation.UseVisualStyleBackColor = true;
            this.checkbox_Clock_EnableCaliberation.CheckedChanged += new System.EventHandler(this.checkbox_Clock_EnableCaliberation_CheckStateChanged);
            // 
            // label156
            // 
            this.label156.AutoSize = true;
            this.label156.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label156.ForeColor = System.Drawing.Color.Navy;
            this.label156.Location = new System.Drawing.Point(60, 201);
            this.label156.Name = "label156";
            this.label156.Size = new System.Drawing.Size(169, 15);
            this.label156.TabIndex = 11;
            this.label156.Text = "(32.553256 - 32.982744 ) KHz";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.ForeColor = System.Drawing.Color.Navy;
            this.label51.Location = new System.Drawing.Point(60, 19);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(120, 15);
            this.label51.TabIndex = 11;
            this.label51.Text = "Day light Saving time";
            // 
            // lbl_Clock_EndDate
            // 
            this.lbl_Clock_EndDate.AutoSize = true;
            this.lbl_Clock_EndDate.Enabled = false;
            this.lbl_Clock_EndDate.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Clock_EndDate.Location = new System.Drawing.Point(12, 71);
            this.lbl_Clock_EndDate.Name = "lbl_Clock_EndDate";
            this.lbl_Clock_EndDate.Size = new System.Drawing.Size(56, 15);
            this.lbl_Clock_EndDate.TabIndex = 10;
            this.lbl_Clock_EndDate.Text = "End Date";
            // 
            // lbl_Clock_BeginDate
            // 
            this.lbl_Clock_BeginDate.AutoSize = true;
            this.lbl_Clock_BeginDate.Enabled = false;
            this.lbl_Clock_BeginDate.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Clock_BeginDate.Location = new System.Drawing.Point(12, 46);
            this.lbl_Clock_BeginDate.Name = "lbl_Clock_BeginDate";
            this.lbl_Clock_BeginDate.Size = new System.Drawing.Size(66, 15);
            this.lbl_Clock_BeginDate.TabIndex = 9;
            this.lbl_Clock_BeginDate.Text = "Begin Date";
            // 
            // lbl_Clock_ClockCalibrationPPM
            // 
            this.lbl_Clock_ClockCalibrationPPM.AutoSize = true;
            this.lbl_Clock_ClockCalibrationPPM.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Clock_ClockCalibrationPPM.Location = new System.Drawing.Point(14, 179);
            this.lbl_Clock_ClockCalibrationPPM.Name = "lbl_Clock_ClockCalibrationPPM";
            this.lbl_Clock_ClockCalibrationPPM.Size = new System.Drawing.Size(134, 15);
            this.lbl_Clock_ClockCalibrationPPM.TabIndex = 7;
            this.lbl_Clock_ClockCalibrationPPM.Text = "Crystal Frequency (kHz)";
            // 
            // tmr_Debug_NowTime
            // 
            this.tmr_Debug_NowTime.Interval = 1000;
            this.tmr_Debug_NowTime.Tick += new System.EventHandler(this.tmr_Debug_NowTime_Tick);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ucClockCalib
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.gb_Clock);
            this.DoubleBuffered = true;
            this.Name = "ucClockCalib";
            this.Size = new System.Drawing.Size(271, 340);
            this.Load += new System.EventHandler(this.ucClockCalib_Load);
            this.Leave += new System.EventHandler(this.ucClockCalib_Leave);
            this.gb_Clock.ResumeLayout(false);
            this.gb_Clock.PerformLayout();
            this.pnlClock.ResumeLayout(false);
            this.pnlClock.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox gb_Clock;
        private System.Windows.Forms.MaskedTextBox txt_Clock_ClockCalibrationPPM;
        private System.Windows.Forms.Panel pnlClock;
        private System.Windows.Forms.RadioButton radio_clock_auto;
        private System.Windows.Forms.RadioButton radio_clock_manual;
        public System.Windows.Forms.Label label37;
        private System.Windows.Forms.DateTimePicker txt_Clock_set_time;
        private System.Windows.Forms.DateTimePicker txt_Clock_EndDate;
        private System.Windows.Forms.DateTimePicker txt_Clock_BeginDate;
        public System.Windows.Forms.CheckBox checkbox_Clock_PPMAdd;
        public System.Windows.Forms.CheckBox checkbox_Clock_EnableDayLightSaving;
        public System.Windows.Forms.CheckBox checkbox_Clock_EnableCaliberation;
        public System.Windows.Forms.Label label156;
        public System.Windows.Forms.Label label51;
        public System.Windows.Forms.Label lbl_Clock_EndDate;
        public System.Windows.Forms.Label lbl_Clock_BeginDate;
        public System.Windows.Forms.Label lbl_Clock_ClockCalibrationPPM;
        private System.Windows.Forms.Timer tmr_Debug_NowTime;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
