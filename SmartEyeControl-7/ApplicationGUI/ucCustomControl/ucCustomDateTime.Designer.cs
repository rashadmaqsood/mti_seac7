namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    partial class ucCustomDateTime
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
            this.lbl_RawDate = new System.Windows.Forms.Label();
            this.rdb_Advance_View = new System.Windows.Forms.RadioButton();
            this.rdb_Daily = new System.Windows.Forms.RadioButton();
            this.rdb_OnlyOnce = new System.Windows.Forms.RadioButton();
            this.rdb_Week = new System.Windows.Forms.RadioButton();
            this.rdb_Year = new System.Windows.Forms.RadioButton();
            this.rdb_Month = new System.Windows.Forms.RadioButton();
            this.lbl_Format_ResetDate = new System.Windows.Forms.Label();
            this.lbl_Format_ResetTime = new System.Windows.Forms.Label();
            this.dtc_Date = new ucDateTimeChooser.ucCustomDateTimePicker();
            this.dtc_Time = new ucDateTimeChooser.ucCustomDateTimePicker();
            this.dateTimeChooser = new datetime.DateTimeChooser();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // lbl_RawDate
            // 
            this.lbl_RawDate.AutoSize = true;
            this.lbl_RawDate.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbl_RawDate.ForeColor = System.Drawing.Color.Navy;
            this.lbl_RawDate.Location = new System.Drawing.Point(100, 78);
            this.lbl_RawDate.Name = "lbl_RawDate";
            this.lbl_RawDate.Size = new System.Drawing.Size(90, 15);
            this.lbl_RawDate.TabIndex = 27;
            this.lbl_RawDate.Text = "Raw_DateView";
            this.lbl_RawDate.Visible = false;
            // 
            // rdb_Advance_View
            // 
            this.rdb_Advance_View.AutoSize = true;
            this.rdb_Advance_View.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdb_Advance_View.ForeColor = System.Drawing.Color.Navy;
            this.rdb_Advance_View.Location = new System.Drawing.Point(0, 69);
            this.rdb_Advance_View.Name = "rdb_Advance_View";
            this.rdb_Advance_View.Size = new System.Drawing.Size(102, 19);
            this.rdb_Advance_View.TabIndex = 27;
            this.rdb_Advance_View.TabStop = true;
            this.rdb_Advance_View.Text = "Advance View";
            this.rdb_Advance_View.UseVisualStyleBackColor = true;
            this.rdb_Advance_View.CheckedChanged += new System.EventHandler(this.rdb_DateView_CheckedChanged);
            // 
            // rdb_Daily
            // 
            this.rdb_Daily.AutoSize = true;
            this.rdb_Daily.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdb_Daily.ForeColor = System.Drawing.Color.Navy;
            this.rdb_Daily.Location = new System.Drawing.Point(0, 41);
            this.rdb_Daily.Name = "rdb_Daily";
            this.rdb_Daily.Size = new System.Drawing.Size(93, 19);
            this.rdb_Daily.TabIndex = 25;
            this.rdb_Daily.TabStop = true;
            this.rdb_Daily.Text = "Repeat Daily";
            this.rdb_Daily.UseVisualStyleBackColor = true;
            this.rdb_Daily.CheckedChanged += new System.EventHandler(this.rdb_DateView_CheckedChanged);
            // 
            // rdb_OnlyOnce
            // 
            this.rdb_OnlyOnce.AutoSize = true;
            this.rdb_OnlyOnce.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdb_OnlyOnce.ForeColor = System.Drawing.Color.Navy;
            this.rdb_OnlyOnce.Location = new System.Drawing.Point(0, 55);
            this.rdb_OnlyOnce.Name = "rdb_OnlyOnce";
            this.rdb_OnlyOnce.Size = new System.Drawing.Size(81, 19);
            this.rdb_OnlyOnce.TabIndex = 26;
            this.rdb_OnlyOnce.TabStop = true;
            this.rdb_OnlyOnce.Text = "Only Once";
            this.rdb_OnlyOnce.UseVisualStyleBackColor = true;
            this.rdb_OnlyOnce.CheckedChanged += new System.EventHandler(this.rdb_DateView_CheckedChanged);
            // 
            // rdb_Week
            // 
            this.rdb_Week.AutoSize = true;
            this.rdb_Week.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdb_Week.ForeColor = System.Drawing.Color.Navy;
            this.rdb_Week.Location = new System.Drawing.Point(0, 28);
            this.rdb_Week.Name = "rdb_Week";
            this.rdb_Week.Size = new System.Drawing.Size(108, 19);
            this.rdb_Week.TabIndex = 24;
            this.rdb_Week.TabStop = true;
            this.rdb_Week.Text = "Repeat Weekly";
            this.rdb_Week.UseVisualStyleBackColor = true;
            this.rdb_Week.CheckedChanged += new System.EventHandler(this.rdb_DateView_CheckedChanged);
            // 
            // rdb_Year
            // 
            this.rdb_Year.AutoSize = true;
            this.rdb_Year.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdb_Year.ForeColor = System.Drawing.Color.Navy;
            this.rdb_Year.Location = new System.Drawing.Point(0, 0);
            this.rdb_Year.Name = "rdb_Year";
            this.rdb_Year.Size = new System.Drawing.Size(100, 19);
            this.rdb_Year.TabIndex = 22;
            this.rdb_Year.Text = "Repeat Yearly";
            this.rdb_Year.UseVisualStyleBackColor = true;
            this.rdb_Year.CheckedChanged += new System.EventHandler(this.rdb_DateView_CheckedChanged);
            // 
            // rdb_Month
            // 
            this.rdb_Month.AutoSize = true;
            this.rdb_Month.Checked = true;
            this.rdb_Month.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdb_Month.ForeColor = System.Drawing.Color.Navy;
            this.rdb_Month.Location = new System.Drawing.Point(0, 14);
            this.rdb_Month.Name = "rdb_Month";
            this.rdb_Month.Size = new System.Drawing.Size(113, 19);
            this.rdb_Month.TabIndex = 23;
            this.rdb_Month.TabStop = true;
            this.rdb_Month.Text = "Repeat Monthly";
            this.rdb_Month.UseVisualStyleBackColor = true;
            this.rdb_Month.CheckedChanged += new System.EventHandler(this.rdb_DateView_CheckedChanged);
            // 
            // lbl_Format_ResetDate
            // 
            this.lbl_Format_ResetDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_Format_ResetDate.AutoSize = true;
            this.lbl_Format_ResetDate.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbl_Format_ResetDate.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Format_ResetDate.Location = new System.Drawing.Point(96, 0);
            this.lbl_Format_ResetDate.Name = "lbl_Format_ResetDate";
            this.lbl_Format_ResetDate.Size = new System.Drawing.Size(220, 15);
            this.lbl_Format_ResetDate.TabIndex = 29;
            this.lbl_Format_ResetDate.Text = "(DayOfWeek,DayOfMonth Month Year)";
            // 
            // lbl_Format_ResetTime
            // 
            this.lbl_Format_ResetTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_Format_ResetTime.AutoSize = true;
            this.lbl_Format_ResetTime.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbl_Format_ResetTime.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Format_ResetTime.Location = new System.Drawing.Point(286, 0);
            this.lbl_Format_ResetTime.Margin = new System.Windows.Forms.Padding(3, 0, 30, 0);
            this.lbl_Format_ResetTime.Name = "lbl_Format_ResetTime";
            this.lbl_Format_ResetTime.Size = new System.Drawing.Size(108, 15);
            this.lbl_Format_ResetTime.TabIndex = 30;
            this.lbl_Format_ResetTime.Text = "(Hour:Minute:Sec)";
            this.lbl_Format_ResetTime.Visible = false;
            // 
            // dtc_Date
            // 
            this.dtc_Date.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtc_Date.BackColor = System.Drawing.SystemColors.Window;
            this.dtc_Date.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtc_Date.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dtc_Date.FormatEx = DLMS.dtpCustomExtensions.dtpLongDateWildCard;
            this.dtc_Date.Location = new System.Drawing.Point(99, 14);
            this.dtc_Date.Name = "dtc_Date";
            this.dtc_Date.ShowButtons = true;
            this.dtc_Date.ShowUpDownButton = false;
            this.dtc_Date.ShowWildCardWinButton = true;
            this.dtc_Date.Size = new System.Drawing.Size(190, 22);
            this.dtc_Date.TabIndex = 31;
            // 
            // dtc_Time
            // 
            this.dtc_Time.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtc_Time.BackColor = System.Drawing.SystemColors.Window;
            this.dtc_Time.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtc_Time.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dtc_Time.FormatEx = DLMS.dtpCustomExtensions.dtpLongTime;
            this.dtc_Time.Location = new System.Drawing.Point(289, 14);
            this.dtc_Time.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.dtc_Time.Name = "dtc_Time";
            this.dtc_Time.ShowButtons = true;
            this.dtc_Time.ShowUpDownButton = true;
            this.dtc_Time.ShowWildCardWinButton = false;
            this.dtc_Time.Size = new System.Drawing.Size(80, 22);
            this.dtc_Time.TabIndex = 32;
            this.dtc_Time.Visible = false;
            // 
            // dateTimeChooser
            // 
            this.dateTimeChooser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dateTimeChooser.BackColor = System.Drawing.Color.Transparent;
            this.dateTimeChooser.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.dateTimeChooser.ForeColor = System.Drawing.Color.Navy;
            this.dateTimeChooser.Location = new System.Drawing.Point(99, 0);
            this.dateTimeChooser.Name = "dateTimeChooser";
            this.dateTimeChooser.showDate = true;
            this.dateTimeChooser.showDayOfWeek = true;
            this.dateTimeChooser.showHour = true;
            this.dateTimeChooser.showHundredth = false;
            this.dateTimeChooser.showMinute = true;
            this.dateTimeChooser.showMonth = true;
            this.dateTimeChooser.showSeconds = true;
            this.dateTimeChooser.showYear = true;
            this.dateTimeChooser.Size = new System.Drawing.Size(295, 75);
            this.dateTimeChooser.TabIndex = 33;
            this.dateTimeChooser.VisibleDate = true;
            this.dateTimeChooser.VisibleTime = true;
            // 
            // ucCustomDateTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.rdb_OnlyOnce);
            this.Controls.Add(this.rdb_Week);
            this.Controls.Add(this.rdb_Year);
            this.Controls.Add(this.rdb_Month);
            this.Controls.Add(this.rdb_Advance_View);
            this.Controls.Add(this.rdb_Daily);
            this.Controls.Add(this.lbl_Format_ResetDate);
            this.Controls.Add(this.lbl_Format_ResetTime);
            this.Controls.Add(this.dtc_Date);
            this.Controls.Add(this.dtc_Time);
            this.Controls.Add(this.dateTimeChooser);
            this.Controls.Add(this.lbl_RawDate);
            this.DoubleBuffered = true;
            this.Name = "ucCustomDateTime";
            this.Size = new System.Drawing.Size(419, 91);
            this.Load += new System.EventHandler(this.ucCustomDateTime_Load);
            this.Leave += new System.EventHandler(this.ucCustomDateTime_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label lbl_RawDate;
        private System.Windows.Forms.Label lbl_Format_ResetDate;
        private System.Windows.Forms.Label lbl_Format_ResetTime;
        internal ucDateTimeChooser.ucCustomDateTimePicker dtc_Date;
        internal ucDateTimeChooser.ucCustomDateTimePicker dtc_Time;
        internal datetime.DateTimeChooser dateTimeChooser;
        private System.Windows.Forms.RadioButton rdb_Advance_View;
        private System.Windows.Forms.RadioButton rdb_Daily;
        private System.Windows.Forms.RadioButton rdb_OnlyOnce;
        private System.Windows.Forms.RadioButton rdb_Week;
        private System.Windows.Forms.RadioButton rdb_Year;
        private System.Windows.Forms.RadioButton rdb_Month;
    }
}
