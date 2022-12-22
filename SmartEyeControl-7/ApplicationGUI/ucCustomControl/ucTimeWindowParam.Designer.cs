namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    partial class ucTimeWindowParam
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
            this.gpTimeWindow = new System.Windows.Forms.GroupBox();
            this.fLP_Main = new System.Windows.Forms.FlowLayoutPanel();
            this.fLP_ControlMode = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_ControlMode = new System.Windows.Forms.Label();
            this.combo_control_TBE = new System.Windows.Forms.ListBox();
            this.lbl_heading = new System.Windows.Forms.Label();
            this.fLP_DTC_TBE = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_Format = new System.Windows.Forms.Label();
            this.lbl_Access_Error = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.DTC_TBE = new ucDateTimeChooser.ucCustomDateTimePicker();
            this.ucCustomDateTime = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucCustomDateTime();
            this.gpTimeWindow.SuspendLayout();
            this.fLP_Main.SuspendLayout();
            this.fLP_ControlMode.SuspendLayout();
            this.fLP_DTC_TBE.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // gpTimeWindow
            // 
            this.gpTimeWindow.Controls.Add(this.fLP_Main);
            this.gpTimeWindow.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpTimeWindow.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpTimeWindow.ForeColor = System.Drawing.Color.Maroon;
            this.gpTimeWindow.Location = new System.Drawing.Point(0, 0);
            this.gpTimeWindow.Name = "gpTimeWindow";
            this.gpTimeWindow.Size = new System.Drawing.Size(450, 266);
            this.gpTimeWindow.TabIndex = 2;
            this.gpTimeWindow.TabStop = false;
            this.gpTimeWindow.Text = "Time Window 1";
            // 
            // fLP_Main
            // 
            this.fLP_Main.AutoSize = true;
            this.fLP_Main.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_Main.Controls.Add(this.fLP_ControlMode);
            this.fLP_Main.Controls.Add(this.fLP_DTC_TBE);
            this.fLP_Main.Controls.Add(this.lbl_heading);
            this.fLP_Main.Controls.Add(this.lbl_Access_Error);
            this.fLP_Main.Controls.Add(this.ucCustomDateTime);
            this.fLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fLP_Main.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLP_Main.Location = new System.Drawing.Point(3, 19);
            this.fLP_Main.Name = "fLP_Main";
            this.fLP_Main.Size = new System.Drawing.Size(444, 244);
            this.fLP_Main.TabIndex = 3;
            // 
            // fLP_ControlMode
            // 
            this.fLP_ControlMode.AutoSize = true;
            this.fLP_ControlMode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_ControlMode.Controls.Add(this.lbl_ControlMode);
            this.fLP_ControlMode.Controls.Add(this.combo_control_TBE);
            this.fLP_ControlMode.Location = new System.Drawing.Point(3, 3);
            this.fLP_ControlMode.Name = "fLP_ControlMode";
            this.fLP_ControlMode.Size = new System.Drawing.Size(291, 55);
            this.fLP_ControlMode.TabIndex = 3;
            // 
            // lbl_ControlMode
            // 
            this.lbl_ControlMode.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ControlMode.ForeColor = System.Drawing.Color.Navy;
            this.lbl_ControlMode.Location = new System.Drawing.Point(3, 0);
            this.lbl_ControlMode.Name = "lbl_ControlMode";
            this.lbl_ControlMode.Size = new System.Drawing.Size(83, 15);
            this.lbl_ControlMode.TabIndex = 2;
            this.lbl_ControlMode.Text = "Control Mode";
            // 
            // combo_control_TBE
            // 
            this.combo_control_TBE.FormattingEnabled = true;
            this.combo_control_TBE.ItemHeight = 15;
            this.combo_control_TBE.Items.AddRange(new object[] {
            "Disable",
            "DateTime",
            "Time Interval",
            "Time Interval[Sink]",
            "Time Interval[Fixed]"});
            this.combo_control_TBE.Location = new System.Drawing.Point(144, 3);
            this.combo_control_TBE.Margin = new System.Windows.Forms.Padding(55, 3, 3, 3);
            this.combo_control_TBE.Name = "combo_control_TBE";
            this.combo_control_TBE.Size = new System.Drawing.Size(144, 49);
            this.combo_control_TBE.TabIndex = 38;
            this.combo_control_TBE.SelectedIndexChanged += new System.EventHandler(this.combo_control_TBE1_SelectedIndexChanged);
            // 
            // lbl_heading
            // 
            this.lbl_heading.AutoSize = true;
            this.lbl_heading.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_heading.ForeColor = System.Drawing.Color.Navy;
            this.lbl_heading.Location = new System.Drawing.Point(3, 119);
            this.lbl_heading.Name = "lbl_heading";
            this.lbl_heading.Size = new System.Drawing.Size(90, 15);
            this.lbl_heading.TabIndex = 2;
            this.lbl_heading.Text = "Start DateTime";
            // 
            // fLP_DTC_TBE
            // 
            this.fLP_DTC_TBE.AutoSize = true;
            this.fLP_DTC_TBE.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_DTC_TBE.Controls.Add(this.lbl_Format);
            this.fLP_DTC_TBE.Controls.Add(this.DTC_TBE);
            this.fLP_DTC_TBE.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLP_DTC_TBE.Location = new System.Drawing.Point(170, 61);
            this.fLP_DTC_TBE.Margin = new System.Windows.Forms.Padding(170, 0, 3, 0);
            this.fLP_DTC_TBE.Name = "fLP_DTC_TBE";
            this.fLP_DTC_TBE.Size = new System.Drawing.Size(276, 58);
            this.fLP_DTC_TBE.TabIndex = 41;
            // 
            // lbl_Format
            // 
            this.lbl_Format.AutoSize = true;
            this.lbl_Format.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Format.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Format.Location = new System.Drawing.Point(3, 0);
            this.lbl_Format.Name = "lbl_Format";
            this.lbl_Format.Size = new System.Drawing.Size(222, 30);
            this.lbl_Format.TabIndex = 2;
            this.lbl_Format.Text = "(DayOfWeek, DayOfMonth Month Year HH:MM:SS)";
            // 
            // lbl_Access_Error
            // 
            this.lbl_Access_Error.AutoSize = true;
            this.lbl_Access_Error.ForeColor = System.Drawing.Color.Red;
            this.lbl_Access_Error.Location = new System.Drawing.Point(3, 134);
            this.lbl_Access_Error.Name = "lbl_Access_Error";
            this.lbl_Access_Error.Size = new System.Drawing.Size(203, 30);
            this.lbl_Access_Error.TabIndex = 42;
            this.lbl_Access_Error.Text = "Control Mode Time Interval\nInsufficient Privilege To View Details";
            this.lbl_Access_Error.Visible = false;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // DTC_TBE
            // 
            this.DTC_TBE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DTC_TBE.ForeColor = System.Drawing.SystemColors.WindowText;
            this.DTC_TBE.FormatEx = DLMS.dtpCustomExtensions.dtpLongDateTimeWildCard;
            this.DTC_TBE.Location = new System.Drawing.Point(3, 33);
            this.DTC_TBE.Name = "DTC_TBE";
            this.DTC_TBE.ShowButtons = true;
            this.DTC_TBE.ShowUpDownButton = false;
            this.DTC_TBE.ShowWildCardWinButton = true;
            this.DTC_TBE.Size = new System.Drawing.Size(270, 22);
            this.DTC_TBE.TabIndex = 39;
            // 
            // ucCustomDateTime
            // 
            this.ucCustomDateTime.BackColor = System.Drawing.Color.Transparent;
            this.ucCustomDateTime.Enabled = false;
            this.ucCustomDateTime.Location = new System.Drawing.Point(3, 164);
            this.ucCustomDateTime.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.ucCustomDateTime.Name = "ucCustomDateTime";
            this.ucCustomDateTime.Size = new System.Drawing.Size(0, 0);
            this.ucCustomDateTime.TabIndex = 40;
            this.ucCustomDateTime.Visible = false;
            // 
            // ucTimeWindowParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.gpTimeWindow);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.Name = "ucTimeWindowParam";
            this.Size = new System.Drawing.Size(450, 270);
            this.Load += new System.EventHandler(this.ucTimeWindowParam_Load);
            this.Leave += new System.EventHandler(this.ucTimeWindowParam_Leave);
            this.gpTimeWindow.ResumeLayout(false);
            this.gpTimeWindow.PerformLayout();
            this.fLP_Main.ResumeLayout(false);
            this.fLP_Main.PerformLayout();
            this.fLP_ControlMode.ResumeLayout(false);
            this.fLP_DTC_TBE.ResumeLayout(false);
            this.fLP_DTC_TBE.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpTimeWindow;
        private System.Windows.Forms.Label lbl_Format;
        private System.Windows.Forms.Label lbl_heading;
        private System.Windows.Forms.Label lbl_ControlMode;
        private System.Windows.Forms.ListBox combo_control_TBE;
        private ucDateTimeChooser.ucCustomDateTimePicker DTC_TBE;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private ucCustomDateTime ucCustomDateTime;
        private System.Windows.Forms.FlowLayoutPanel fLP_ControlMode;
        private System.Windows.Forms.FlowLayoutPanel fLP_DTC_TBE;
        private System.Windows.Forms.FlowLayoutPanel fLP_Main;
        private System.Windows.Forms.Label lbl_Access_Error;
    }
}
