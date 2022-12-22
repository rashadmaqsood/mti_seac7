namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    partial class ucSPDayProfileDateTime
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
            this.rdb_Advance_View = new System.Windows.Forms.RadioButton();
            this.rdb_OnlyOnce = new System.Windows.Forms.RadioButton();
            this.rdb_Week = new System.Windows.Forms.RadioButton();
            this.rdb_Year = new System.Windows.Forms.RadioButton();
            this.rdb_Month = new System.Windows.Forms.RadioButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.lbl_Format_ResetDate = new System.Windows.Forms.Label();
            this.dtc_StDate = new ucDateTimeChooser.ucCustomDateTimePicker();
            this.dateTimeChooser = new datetime.DateTimeChooser();
            this.lbl_RawDate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // rdb_Advance_View
            // 
            this.rdb_Advance_View.AutoSize = true;
            this.rdb_Advance_View.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdb_Advance_View.ForeColor = System.Drawing.Color.Navy;
            this.rdb_Advance_View.Location = new System.Drawing.Point(3, 60);
            this.rdb_Advance_View.Name = "rdb_Advance_View";
            this.rdb_Advance_View.Size = new System.Drawing.Size(102, 19);
            this.rdb_Advance_View.TabIndex = 36;
            this.rdb_Advance_View.TabStop = true;
            this.rdb_Advance_View.Text = "Advance View";
            this.rdb_Advance_View.UseVisualStyleBackColor = true;
            this.rdb_Advance_View.CheckedChanged += new System.EventHandler(this.rdb_AutoResetDateView_CheckedChanged);
            // 
            // rdb_OnlyOnce
            // 
            this.rdb_OnlyOnce.AutoSize = true;
            this.rdb_OnlyOnce.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdb_OnlyOnce.ForeColor = System.Drawing.Color.Navy;
            this.rdb_OnlyOnce.Location = new System.Drawing.Point(3, 45);
            this.rdb_OnlyOnce.Name = "rdb_OnlyOnce";
            this.rdb_OnlyOnce.Size = new System.Drawing.Size(81, 19);
            this.rdb_OnlyOnce.TabIndex = 35;
            this.rdb_OnlyOnce.TabStop = true;
            this.rdb_OnlyOnce.Text = "Only Once";
            this.rdb_OnlyOnce.UseVisualStyleBackColor = true;
            this.rdb_OnlyOnce.CheckedChanged += new System.EventHandler(this.rdb_AutoResetDateView_CheckedChanged);
            // 
            // rdb_Week
            // 
            this.rdb_Week.AutoSize = true;
            this.rdb_Week.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdb_Week.ForeColor = System.Drawing.Color.Navy;
            this.rdb_Week.Location = new System.Drawing.Point(3, 30);
            this.rdb_Week.Name = "rdb_Week";
            this.rdb_Week.Size = new System.Drawing.Size(108, 19);
            this.rdb_Week.TabIndex = 33;
            this.rdb_Week.TabStop = true;
            this.rdb_Week.Text = "Repeat Weekly";
            this.rdb_Week.UseVisualStyleBackColor = true;
            this.rdb_Week.CheckedChanged += new System.EventHandler(this.rdb_AutoResetDateView_CheckedChanged);
            // 
            // rdb_Year
            // 
            this.rdb_Year.AutoSize = true;
            this.rdb_Year.Checked = true;
            this.rdb_Year.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdb_Year.ForeColor = System.Drawing.Color.Navy;
            this.rdb_Year.Location = new System.Drawing.Point(3, 0);
            this.rdb_Year.Name = "rdb_Year";
            this.rdb_Year.Size = new System.Drawing.Size(100, 19);
            this.rdb_Year.TabIndex = 31;
            this.rdb_Year.TabStop = true;
            this.rdb_Year.Text = "Repeat Yearly";
            this.rdb_Year.UseVisualStyleBackColor = true;
            this.rdb_Year.CheckedChanged += new System.EventHandler(this.rdb_AutoResetDateView_CheckedChanged);
            // 
            // rdb_Month
            // 
            this.rdb_Month.AutoSize = true;
            this.rdb_Month.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdb_Month.ForeColor = System.Drawing.Color.Navy;
            this.rdb_Month.Location = new System.Drawing.Point(3, 15);
            this.rdb_Month.Name = "rdb_Month";
            this.rdb_Month.Size = new System.Drawing.Size(113, 19);
            this.rdb_Month.TabIndex = 32;
            this.rdb_Month.Text = "Repeat Monthly";
            this.rdb_Month.UseVisualStyleBackColor = true;
            this.rdb_Month.CheckedChanged += new System.EventHandler(this.rdb_AutoResetDateView_CheckedChanged);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // lbl_Format_ResetDate
            // 
            this.lbl_Format_ResetDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_Format_ResetDate.AutoSize = true;
            this.lbl_Format_ResetDate.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbl_Format_ResetDate.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Format_ResetDate.Location = new System.Drawing.Point(43, -138);
            this.lbl_Format_ResetDate.Name = "lbl_Format_ResetDate";
            this.lbl_Format_ResetDate.Size = new System.Drawing.Size(220, 15);
            this.lbl_Format_ResetDate.TabIndex = 47;
            this.lbl_Format_ResetDate.Text = "(DayOfWeek,DayOfMonth Month Year)";
            // 
            // dtc_StDate
            // 
            this.dtc_StDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtc_StDate.BackColor = System.Drawing.SystemColors.Window;
            this.dtc_StDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtc_StDate.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtc_StDate.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dtc_StDate.FormatEx = DLMS.dtpCustomExtensions.dtpLongDateWildCard;
            this.dtc_StDate.Location = new System.Drawing.Point(140, 15);
            this.dtc_StDate.Name = "dtc_StDate";
            this.dtc_StDate.ShowButtons = true;
            this.dtc_StDate.ShowUpDownButton = false;
            this.dtc_StDate.ShowWildCardWinButton = true;
            this.dtc_StDate.Size = new System.Drawing.Size(187, 22);
            this.dtc_StDate.TabIndex = 55;
            // 
            // dateTimeChooser
            // 
            this.dateTimeChooser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dateTimeChooser.BackColor = System.Drawing.Color.Transparent;
            this.dateTimeChooser.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.dateTimeChooser.ForeColor = System.Drawing.Color.Navy;
            this.dateTimeChooser.Location = new System.Drawing.Point(128, 0);
            this.dateTimeChooser.Name = "dateTimeChooser";
            this.dateTimeChooser.showDate = true;
            this.dateTimeChooser.showDayOfWeek = true;
            this.dateTimeChooser.showHour = false;
            this.dateTimeChooser.showHundredth = false;
            this.dateTimeChooser.showMinute = false;
            this.dateTimeChooser.showMonth = true;
            this.dateTimeChooser.showSeconds = false;
            this.dateTimeChooser.showYear = true;
            this.dateTimeChooser.Size = new System.Drawing.Size(318, 45);
            this.dateTimeChooser.TabIndex = 56;
            this.dateTimeChooser.VisibleDate = true;
            this.dateTimeChooser.VisibleTime = false;
            // 
            // lbl_RawDate
            // 
            this.lbl_RawDate.AutoSize = true;
            this.lbl_RawDate.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbl_RawDate.ForeColor = System.Drawing.Color.Navy;
            this.lbl_RawDate.Location = new System.Drawing.Point(161, 43);
            this.lbl_RawDate.Name = "lbl_RawDate";
            this.lbl_RawDate.Size = new System.Drawing.Size(90, 15);
            this.lbl_RawDate.TabIndex = 54;
            this.lbl_RawDate.Text = "Raw_DateView";
            this.lbl_RawDate.Visible = false;
            // 
            // ucSPDayProfileDateTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.dtc_StDate);
            this.Controls.Add(this.dateTimeChooser);
            this.Controls.Add(this.lbl_RawDate);
            this.Controls.Add(this.lbl_Format_ResetDate);
            this.Controls.Add(this.rdb_Advance_View);
            this.Controls.Add(this.rdb_OnlyOnce);
            this.Controls.Add(this.rdb_Week);
            this.Controls.Add(this.rdb_Year);
            this.Controls.Add(this.rdb_Month);
            this.Name = "ucSPDayProfileDateTime";
            this.Size = new System.Drawing.Size(447, 84);
            this.Load += new System.EventHandler(this.ucSPDayProfileDateTime_Load);
            this.Leave += new System.EventHandler(this.ucSPDayProfileDateTime_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdb_Week;
        private System.Windows.Forms.ErrorProvider errorProvider;
        internal System.Windows.Forms.RadioButton rdb_Advance_View;
        internal System.Windows.Forms.RadioButton rdb_OnlyOnce;
        internal System.Windows.Forms.RadioButton rdb_Year;
        internal System.Windows.Forms.RadioButton rdb_Month;
        internal ucDateTimeChooser.ucCustomDateTimePicker dtc_StDate;
        internal datetime.DateTimeChooser dateTimeChooser;
        private System.Windows.Forms.Label lbl_RawDate;
        private System.Windows.Forms.Label lbl_Format_ResetDate;
    }
}
