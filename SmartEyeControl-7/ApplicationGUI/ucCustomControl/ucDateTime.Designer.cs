namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    partial class ucDateTime
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
            this.tmr_Debug_NowTime = new System.Windows.Forms.Timer(this.components);
            this.tbMain = new System.Windows.Forms.TabControl();
            this.tpSetTime = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rd_Degub_Manual = new System.Windows.Forms.RadioButton();
            this.rb_Debug_Now = new System.Windows.Forms.RadioButton();
            this.btn_GetTime = new System.Windows.Forms.Button();
            this.btn_SETtime = new System.Windows.Forms.Button();
            this.txt_Clock_set_time_debug = new System.Windows.Forms.DateTimePicker();
            this.tpTimeMethods = new System.Windows.Forms.TabPage();
            this.btnGetPresetAdjustTime = new System.Windows.Forms.Button();
            this.groupBox38 = new System.Windows.Forms.GroupBox();
            this.btnGetShiftRange = new System.Windows.Forms.Button();
            this.txtClockShiftLimit = new System.Windows.Forms.TextBox();
            this.lblTimeShiftRange = new System.Windows.Forms.Label();
            this.btnClockLimitSet = new System.Windows.Forms.Button();
            this.groupBox39 = new System.Windows.Forms.GroupBox();
            this.btnClockSyncMethodGet = new System.Windows.Forms.Button();
            this.btnClockSynchronizationMethodSet = new System.Windows.Forms.Button();
            this.cmbClockSynchronizationMethod = new System.Windows.Forms.ComboBox();
            this.lblClockSyncMethod = new System.Windows.Forms.Label();
            this.gbTimeMethods = new System.Windows.Forms.GroupBox();
            this.btnTimeMethodsInvoke = new System.Windows.Forms.Button();
            this.txtSecondsToShift = new System.Windows.Forms.TextBox();
            this.dtpValidityEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpValidityStart = new System.Windows.Forms.DateTimePicker();
            this.dtpPresetTime = new System.Windows.Forms.DateTimePicker();
            this.lblPresetTime = new System.Windows.Forms.Label();
            this.lblValidityIntervalStart = new System.Windows.Forms.Label();
            this.lblValidityIntervalEnd = new System.Windows.Forms.Label();
            this.label187 = new System.Windows.Forms.Label();
            this.cmbTimeMethods = new System.Windows.Forms.ComboBox();
            this.lblTimeMethod = new System.Windows.Forms.Label();
            this.tbMain.SuspendLayout();
            this.tpSetTime.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tpTimeMethods.SuspendLayout();
            this.groupBox38.SuspendLayout();
            this.groupBox39.SuspendLayout();
            this.gbTimeMethods.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmr_Debug_NowTime
            // 
            this.tmr_Debug_NowTime.Interval = 500;
            this.tmr_Debug_NowTime.Tick += new System.EventHandler(this.tmr_Debug_NowTime_Tick);
            // 
            // tbMain
            // 
            this.tbMain.Controls.Add(this.tpSetTime);
            this.tbMain.Controls.Add(this.tpTimeMethods);
            this.tbMain.Location = new System.Drawing.Point(3, 3);
            this.tbMain.Name = "tbMain";
            this.tbMain.SelectedIndex = 0;
            this.tbMain.Size = new System.Drawing.Size(935, 369);
            this.tbMain.TabIndex = 42;
            // 
            // tpSetTime
            // 
            this.tpSetTime.Controls.Add(this.groupBox4);
            this.tpSetTime.Location = new System.Drawing.Point(4, 22);
            this.tpSetTime.Name = "tpSetTime";
            this.tpSetTime.Padding = new System.Windows.Forms.Padding(3);
            this.tpSetTime.Size = new System.Drawing.Size(927, 343);
            this.tpSetTime.TabIndex = 0;
            this.tpSetTime.Text = "Set Time";
            this.tpSetTime.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rd_Degub_Manual);
            this.groupBox4.Controls.Add(this.rb_Debug_Now);
            this.groupBox4.Controls.Add(this.btn_GetTime);
            this.groupBox4.Controls.Add(this.btn_SETtime);
            this.groupBox4.Controls.Add(this.txt_Clock_set_time_debug);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox4.Location = new System.Drawing.Point(8, 2);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(150);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox4.Size = new System.Drawing.Size(676, 64);
            this.groupBox4.TabIndex = 40;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Set Time";
            // 
            // rd_Degub_Manual
            // 
            this.rd_Degub_Manual.AutoSize = true;
            this.rd_Degub_Manual.Checked = true;
            this.rd_Degub_Manual.ForeColor = System.Drawing.Color.Navy;
            this.rd_Degub_Manual.Location = new System.Drawing.Point(352, 26);
            this.rd_Degub_Manual.Name = "rd_Degub_Manual";
            this.rd_Degub_Manual.Size = new System.Drawing.Size(65, 19);
            this.rd_Degub_Manual.TabIndex = 40;
            this.rd_Degub_Manual.TabStop = true;
            this.rd_Degub_Manual.Text = "Manual";
            this.rd_Degub_Manual.UseVisualStyleBackColor = true;
            this.rd_Degub_Manual.CheckedChanged += new System.EventHandler(this.radio_clock_auto_CheckedChanged);
            // 
            // rb_Debug_Now
            // 
            this.rb_Debug_Now.AutoSize = true;
            this.rb_Debug_Now.ForeColor = System.Drawing.Color.Navy;
            this.rb_Debug_Now.Location = new System.Drawing.Point(292, 26);
            this.rb_Debug_Now.Name = "rb_Debug_Now";
            this.rb_Debug_Now.Size = new System.Drawing.Size(51, 19);
            this.rb_Debug_Now.TabIndex = 39;
            this.rb_Debug_Now.Text = "Now";
            this.rb_Debug_Now.UseVisualStyleBackColor = true;
            this.rb_Debug_Now.CheckedChanged += new System.EventHandler(this.radio_clock_auto_CheckedChanged);
            // 
            // btn_GetTime
            // 
            this.btn_GetTime.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_GetTime.Location = new System.Drawing.Point(439, 18);
            this.btn_GetTime.Name = "btn_GetTime";
            this.btn_GetTime.Size = new System.Drawing.Size(104, 30);
            this.btn_GetTime.TabIndex = 3;
            this.btn_GetTime.Tag = "Button";
            this.btn_GetTime.Text = "Read Time";
            // 
            // btn_SETtime
            // 
            this.btn_SETtime.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SETtime.Location = new System.Drawing.Point(549, 18);
            this.btn_SETtime.Name = "btn_SETtime";
            this.btn_SETtime.Size = new System.Drawing.Size(110, 30);
            this.btn_SETtime.TabIndex = 3;
            this.btn_SETtime.Tag = "Button";
            this.btn_SETtime.Text = "Write Time";
            // 
            // txt_Clock_set_time_debug
            // 
            this.txt_Clock_set_time_debug.CustomFormat = "dd-MM-yyyy   HH:mm:ss";
            this.txt_Clock_set_time_debug.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Clock_set_time_debug.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txt_Clock_set_time_debug.Location = new System.Drawing.Point(14, 22);
            this.txt_Clock_set_time_debug.Name = "txt_Clock_set_time_debug";
            this.txt_Clock_set_time_debug.Size = new System.Drawing.Size(265, 26);
            this.txt_Clock_set_time_debug.TabIndex = 38;
            this.txt_Clock_set_time_debug.Value = new System.DateTime(2004, 11, 29, 0, 0, 0, 0);
            // 
            // tpTimeMethods
            // 
            this.tpTimeMethods.Controls.Add(this.btnGetPresetAdjustTime);
            this.tpTimeMethods.Controls.Add(this.groupBox38);
            this.tpTimeMethods.Controls.Add(this.groupBox39);
            this.tpTimeMethods.Controls.Add(this.gbTimeMethods);
            this.tpTimeMethods.Location = new System.Drawing.Point(4, 22);
            this.tpTimeMethods.Name = "tpTimeMethods";
            this.tpTimeMethods.Padding = new System.Windows.Forms.Padding(3);
            this.tpTimeMethods.Size = new System.Drawing.Size(927, 343);
            this.tpTimeMethods.TabIndex = 1;
            this.tpTimeMethods.Text = "Clock Synchronization";
            // 
            // btnGetPresetAdjustTime
            // 
            this.btnGetPresetAdjustTime.Location = new System.Drawing.Point(306, 261);
            this.btnGetPresetAdjustTime.Name = "btnGetPresetAdjustTime";
            this.btnGetPresetAdjustTime.Size = new System.Drawing.Size(169, 23);
            this.btnGetPresetAdjustTime.TabIndex = 11;
            this.btnGetPresetAdjustTime.Text = "Get Preset Adjusting Time";
            this.btnGetPresetAdjustTime.UseVisualStyleBackColor = true;
            // 
            // groupBox38
            // 
            this.groupBox38.Controls.Add(this.btnGetShiftRange);
            this.groupBox38.Controls.Add(this.txtClockShiftLimit);
            this.groupBox38.Controls.Add(this.lblTimeShiftRange);
            this.groupBox38.Controls.Add(this.btnClockLimitSet);
            this.groupBox38.ForeColor = System.Drawing.Color.Black;
            this.groupBox38.Location = new System.Drawing.Point(507, 118);
            this.groupBox38.Name = "groupBox38";
            this.groupBox38.Size = new System.Drawing.Size(396, 87);
            this.groupBox38.TabIndex = 14;
            this.groupBox38.TabStop = false;
            this.groupBox38.Text = "Clock Time Shift Limit";
            // 
            // btnGetShiftRange
            // 
            this.btnGetShiftRange.Location = new System.Drawing.Point(330, 39);
            this.btnGetShiftRange.Name = "btnGetShiftRange";
            this.btnGetShiftRange.Size = new System.Drawing.Size(41, 23);
            this.btnGetShiftRange.TabIndex = 14;
            this.btnGetShiftRange.Text = "Get";
            this.btnGetShiftRange.UseVisualStyleBackColor = true;
            // 
            // txtClockShiftLimit
            // 
            this.txtClockShiftLimit.Location = new System.Drawing.Point(160, 39);
            this.txtClockShiftLimit.Name = "txtClockShiftLimit";
            this.txtClockShiftLimit.Size = new System.Drawing.Size(117, 20);
            this.txtClockShiftLimit.TabIndex = 12;
            this.txtClockShiftLimit.Text = "100";
            // 
            // lblTimeShiftRange
            // 
            this.lblTimeShiftRange.AutoSize = true;
            this.lblTimeShiftRange.Location = new System.Drawing.Point(22, 41);
            this.lblTimeShiftRange.Name = "lblTimeShiftRange";
            this.lblTimeShiftRange.Size = new System.Drawing.Size(123, 13);
            this.lblTimeShiftRange.TabIndex = 11;
            this.lblTimeShiftRange.Text = "Time Shift Range ( Sec )";
            // 
            // btnClockLimitSet
            // 
            this.btnClockLimitSet.Location = new System.Drawing.Point(283, 39);
            this.btnClockLimitSet.Name = "btnClockLimitSet";
            this.btnClockLimitSet.Size = new System.Drawing.Size(41, 23);
            this.btnClockLimitSet.TabIndex = 13;
            this.btnClockLimitSet.Text = "Set";
            this.btnClockLimitSet.UseVisualStyleBackColor = true;
            // 
            // groupBox39
            // 
            this.groupBox39.Controls.Add(this.btnClockSyncMethodGet);
            this.groupBox39.Controls.Add(this.btnClockSynchronizationMethodSet);
            this.groupBox39.Controls.Add(this.cmbClockSynchronizationMethod);
            this.groupBox39.Controls.Add(this.lblClockSyncMethod);
            this.groupBox39.ForeColor = System.Drawing.Color.Black;
            this.groupBox39.Location = new System.Drawing.Point(507, 5);
            this.groupBox39.Name = "groupBox39";
            this.groupBox39.Size = new System.Drawing.Size(396, 87);
            this.groupBox39.TabIndex = 1;
            this.groupBox39.TabStop = false;
            this.groupBox39.Text = "Clock Synchronization Method";
            // 
            // btnClockSyncMethodGet
            // 
            this.btnClockSyncMethodGet.Location = new System.Drawing.Point(330, 44);
            this.btnClockSyncMethodGet.Name = "btnClockSyncMethodGet";
            this.btnClockSyncMethodGet.Size = new System.Drawing.Size(41, 23);
            this.btnClockSyncMethodGet.TabIndex = 14;
            this.btnClockSyncMethodGet.Text = "Get";
            this.btnClockSyncMethodGet.UseVisualStyleBackColor = true;
            // 
            // btnClockSynchronizationMethodSet
            // 
            this.btnClockSynchronizationMethodSet.Location = new System.Drawing.Point(283, 44);
            this.btnClockSynchronizationMethodSet.Name = "btnClockSynchronizationMethodSet";
            this.btnClockSynchronizationMethodSet.Size = new System.Drawing.Size(41, 23);
            this.btnClockSynchronizationMethodSet.TabIndex = 13;
            this.btnClockSynchronizationMethodSet.Text = "Set";
            this.btnClockSynchronizationMethodSet.UseVisualStyleBackColor = true;
            // 
            // cmbClockSynchronizationMethod
            // 
            this.cmbClockSynchronizationMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClockSynchronizationMethod.FormattingEnabled = true;
            this.cmbClockSynchronizationMethod.Items.AddRange(new object[] {
            "No Synchronization",
            "Adjust To Quarter",
            "Adjust To Measuring Period",
            "Adjust To Minute",
            "Reserved",
            "Adjust To Preset Time",
            "Shift Time"});
            this.cmbClockSynchronizationMethod.Location = new System.Drawing.Point(24, 44);
            this.cmbClockSynchronizationMethod.Name = "cmbClockSynchronizationMethod";
            this.cmbClockSynchronizationMethod.Size = new System.Drawing.Size(253, 21);
            this.cmbClockSynchronizationMethod.TabIndex = 12;
            // 
            // lblClockSyncMethod
            // 
            this.lblClockSyncMethod.AutoSize = true;
            this.lblClockSyncMethod.Location = new System.Drawing.Point(21, 25);
            this.lblClockSyncMethod.Name = "lblClockSyncMethod";
            this.lblClockSyncMethod.Size = new System.Drawing.Size(100, 13);
            this.lblClockSyncMethod.TabIndex = 11;
            this.lblClockSyncMethod.Text = "Clock Sync Method";
            // 
            // gbTimeMethods
            // 
            this.gbTimeMethods.Controls.Add(this.btnTimeMethodsInvoke);
            this.gbTimeMethods.Controls.Add(this.txtSecondsToShift);
            this.gbTimeMethods.Controls.Add(this.dtpValidityEnd);
            this.gbTimeMethods.Controls.Add(this.dtpValidityStart);
            this.gbTimeMethods.Controls.Add(this.dtpPresetTime);
            this.gbTimeMethods.Controls.Add(this.lblPresetTime);
            this.gbTimeMethods.Controls.Add(this.lblValidityIntervalStart);
            this.gbTimeMethods.Controls.Add(this.lblValidityIntervalEnd);
            this.gbTimeMethods.Controls.Add(this.label187);
            this.gbTimeMethods.Controls.Add(this.cmbTimeMethods);
            this.gbTimeMethods.Controls.Add(this.lblTimeMethod);
            this.gbTimeMethods.Location = new System.Drawing.Point(38, 6);
            this.gbTimeMethods.Name = "gbTimeMethods";
            this.gbTimeMethods.Size = new System.Drawing.Size(437, 249);
            this.gbTimeMethods.TabIndex = 0;
            this.gbTimeMethods.TabStop = false;
            this.gbTimeMethods.Text = "Time Methods";
            // 
            // btnTimeMethodsInvoke
            // 
            this.btnTimeMethodsInvoke.Location = new System.Drawing.Point(341, 197);
            this.btnTimeMethodsInvoke.Name = "btnTimeMethodsInvoke";
            this.btnTimeMethodsInvoke.Size = new System.Drawing.Size(75, 23);
            this.btnTimeMethodsInvoke.TabIndex = 10;
            this.btnTimeMethodsInvoke.Text = "Invoke";
            this.btnTimeMethodsInvoke.UseVisualStyleBackColor = true;
            // 
            // txtSecondsToShift
            // 
            this.txtSecondsToShift.Enabled = false;
            this.txtSecondsToShift.Location = new System.Drawing.Point(163, 199);
            this.txtSecondsToShift.Name = "txtSecondsToShift";
            this.txtSecondsToShift.Size = new System.Drawing.Size(129, 20);
            this.txtSecondsToShift.TabIndex = 9;
            this.txtSecondsToShift.Text = "100";
            // 
            // dtpValidityEnd
            // 
            this.dtpValidityEnd.CustomFormat = "  HH:mm:ss    dd/MMM/yyyy";
            this.dtpValidityEnd.Enabled = false;
            this.dtpValidityEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpValidityEnd.Location = new System.Drawing.Point(163, 157);
            this.dtpValidityEnd.Name = "dtpValidityEnd";
            this.dtpValidityEnd.Size = new System.Drawing.Size(253, 20);
            this.dtpValidityEnd.TabIndex = 8;
            // 
            // dtpValidityStart
            // 
            this.dtpValidityStart.CustomFormat = "  HH:mm:ss    dd/MMM/yyyy";
            this.dtpValidityStart.Enabled = false;
            this.dtpValidityStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpValidityStart.Location = new System.Drawing.Point(163, 114);
            this.dtpValidityStart.Name = "dtpValidityStart";
            this.dtpValidityStart.Size = new System.Drawing.Size(253, 20);
            this.dtpValidityStart.TabIndex = 7;
            // 
            // dtpPresetTime
            // 
            this.dtpPresetTime.CustomFormat = "  HH:mm:ss    dd/MMM/yyyy";
            this.dtpPresetTime.Enabled = false;
            this.dtpPresetTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPresetTime.Location = new System.Drawing.Point(163, 76);
            this.dtpPresetTime.Name = "dtpPresetTime";
            this.dtpPresetTime.Size = new System.Drawing.Size(253, 20);
            this.dtpPresetTime.TabIndex = 6;
            // 
            // lblPresetTime
            // 
            this.lblPresetTime.AutoSize = true;
            this.lblPresetTime.Location = new System.Drawing.Point(57, 82);
            this.lblPresetTime.Name = "lblPresetTime";
            this.lblPresetTime.Size = new System.Drawing.Size(63, 13);
            this.lblPresetTime.TabIndex = 5;
            this.lblPresetTime.Text = "Preset Time";
            // 
            // lblValidityIntervalStart
            // 
            this.lblValidityIntervalStart.AutoSize = true;
            this.lblValidityIntervalStart.Location = new System.Drawing.Point(8, 120);
            this.lblValidityIntervalStart.Name = "lblValidityIntervalStart";
            this.lblValidityIntervalStart.Size = new System.Drawing.Size(103, 13);
            this.lblValidityIntervalStart.TabIndex = 4;
            this.lblValidityIntervalStart.Text = "Validity Interval Start";
            // 
            // lblValidityIntervalEnd
            // 
            this.lblValidityIntervalEnd.AutoSize = true;
            this.lblValidityIntervalEnd.Location = new System.Drawing.Point(15, 160);
            this.lblValidityIntervalEnd.Name = "lblValidityIntervalEnd";
            this.lblValidityIntervalEnd.Size = new System.Drawing.Size(100, 13);
            this.lblValidityIntervalEnd.TabIndex = 3;
            this.lblValidityIntervalEnd.Text = "Validity Interval End";
            // 
            // label187
            // 
            this.label187.AutoSize = true;
            this.label187.Location = new System.Drawing.Point(37, 201);
            this.label187.Name = "label187";
            this.label187.Size = new System.Drawing.Size(89, 13);
            this.label187.TabIndex = 2;
            this.label187.Text = "Seconds To Shift";
            // 
            // cmbTimeMethods
            // 
            this.cmbTimeMethods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimeMethods.FormattingEnabled = true;
            this.cmbTimeMethods.Items.AddRange(new object[] {
            "Adjust To Quarter",
            "Adjust To Measuring Period",
            "Adjust To Minute",
            "Adjust To Preset Time",
            "Preset Adjusting Time",
            "Shift Time"});
            this.cmbTimeMethods.Location = new System.Drawing.Point(163, 32);
            this.cmbTimeMethods.Name = "cmbTimeMethods";
            this.cmbTimeMethods.Size = new System.Drawing.Size(253, 21);
            this.cmbTimeMethods.TabIndex = 1;
            // 
            // lblTimeMethod
            // 
            this.lblTimeMethod.AutoSize = true;
            this.lblTimeMethod.Location = new System.Drawing.Point(72, 37);
            this.lblTimeMethod.Name = "lblTimeMethod";
            this.lblTimeMethod.Size = new System.Drawing.Size(43, 13);
            this.lblTimeMethod.TabIndex = 0;
            this.lblTimeMethod.Text = "Method";
            // 
            // ucDateTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tbMain);
            this.DoubleBuffered = true;
            this.Name = "ucDateTime";
            this.Size = new System.Drawing.Size(943, 376);
            this.Load += new System.EventHandler(this.ucDateTime_Load);
            this.tbMain.ResumeLayout(false);
            this.tpSetTime.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tpTimeMethods.ResumeLayout(false);
            this.groupBox38.ResumeLayout(false);
            this.groupBox38.PerformLayout();
            this.groupBox39.ResumeLayout(false);
            this.groupBox39.PerformLayout();
            this.gbTimeMethods.ResumeLayout(false);
            this.gbTimeMethods.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer tmr_Debug_NowTime;
        private System.Windows.Forms.TabControl tbMain;
        private System.Windows.Forms.TabPage tpSetTime;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rd_Degub_Manual;
        private System.Windows.Forms.RadioButton rb_Debug_Now;
        private System.Windows.Forms.TabPage tpTimeMethods;
        private System.Windows.Forms.GroupBox groupBox38;
        private System.Windows.Forms.Label lblTimeShiftRange;
        private System.Windows.Forms.GroupBox groupBox39;
        private System.Windows.Forms.Label lblClockSyncMethod;
        private System.Windows.Forms.GroupBox gbTimeMethods;
        private System.Windows.Forms.Label lblPresetTime;
        private System.Windows.Forms.Label lblValidityIntervalStart;
        private System.Windows.Forms.Label lblValidityIntervalEnd;
        private System.Windows.Forms.Label label187;
        private System.Windows.Forms.Label lblTimeMethod;
        public System.Windows.Forms.Button btn_GetTime;
        public System.Windows.Forms.Button btn_SETtime;
        public System.Windows.Forms.Button btnGetPresetAdjustTime;
        public System.Windows.Forms.Button btnGetShiftRange;
        public System.Windows.Forms.TextBox txtClockShiftLimit;
        public System.Windows.Forms.Button btnClockLimitSet;
        public System.Windows.Forms.Button btnClockSyncMethodGet;
        public System.Windows.Forms.Button btnClockSynchronizationMethodSet;
        public System.Windows.Forms.ComboBox cmbClockSynchronizationMethod;
        public System.Windows.Forms.Button btnTimeMethodsInvoke;
        public System.Windows.Forms.TextBox txtSecondsToShift;
        public System.Windows.Forms.DateTimePicker dtpValidityEnd;
        public System.Windows.Forms.DateTimePicker dtpValidityStart;
        public System.Windows.Forms.DateTimePicker dtpPresetTime;
        public System.Windows.Forms.ComboBox cmbTimeMethods;
        public System.Windows.Forms.DateTimePicker txt_Clock_set_time_debug;
    }
}
