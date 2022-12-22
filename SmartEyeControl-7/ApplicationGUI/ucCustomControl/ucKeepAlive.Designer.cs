namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    partial class ucKeepAlive
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
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.fLP_Main = new System.Windows.Forms.FlowLayoutPanel();
            this.check_EnableKeepAlive = new System.Windows.Forms.CheckBox();
            this.fLP_WKID = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_KeepAlive_IPProfileID = new System.Windows.Forms.Label();
            this.combo_KeepAlive_WakeUPProfileID = new System.Windows.Forms.ComboBox();
            this.fLP_PingTime = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_KeepAlive_PingTime = new System.Windows.Forms.Label();
            this.txt_KeepAlive_PingTimer = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label186 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label187 = new System.Windows.Forms.Label();
            this.label188 = new System.Windows.Forms.Label();
            this.label189 = new System.Windows.Forms.Label();
            this.label190 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.errProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.chk_enable_wakeup_KeepAlive = new System.Windows.Forms.CheckBox();
            this.check_HeartBeatOnConnection = new System.Windows.Forms.CheckBox();
            this.groupBox14.SuspendLayout();
            this.fLP_Main.SuspendLayout();
            this.fLP_WKID.SuspendLayout();
            this.fLP_PingTime.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.fLP_Main);
            this.groupBox14.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox14.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox14.Location = new System.Drawing.Point(0, 0);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(220, 166);
            this.groupBox14.TabIndex = 14;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Keep Alive";
            // 
            // fLP_Main
            // 
            this.fLP_Main.AutoSize = true;
            this.fLP_Main.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_Main.Controls.Add(this.check_EnableKeepAlive);
            this.fLP_Main.Controls.Add(this.chk_enable_wakeup_KeepAlive);
            this.fLP_Main.Controls.Add(this.check_HeartBeatOnConnection);
            this.fLP_Main.Controls.Add(this.fLP_WKID);
            this.fLP_Main.Controls.Add(this.fLP_PingTime);
            this.fLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fLP_Main.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLP_Main.Location = new System.Drawing.Point(3, 18);
            this.fLP_Main.Margin = new System.Windows.Forms.Padding(0);
            this.fLP_Main.Name = "fLP_Main";
            this.fLP_Main.Size = new System.Drawing.Size(214, 145);
            this.fLP_Main.TabIndex = 54;
            // 
            // check_EnableKeepAlive
            // 
            this.check_EnableKeepAlive.AutoSize = true;
            this.check_EnableKeepAlive.Checked = true;
            this.check_EnableKeepAlive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_EnableKeepAlive.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_EnableKeepAlive.ForeColor = System.Drawing.Color.Black;
            this.check_EnableKeepAlive.Location = new System.Drawing.Point(10, 3);
            this.check_EnableKeepAlive.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.check_EnableKeepAlive.Name = "check_EnableKeepAlive";
            this.check_EnableKeepAlive.Size = new System.Drawing.Size(124, 19);
            this.check_EnableKeepAlive.TabIndex = 37;
            this.check_EnableKeepAlive.Text = "Enable Always ON";
            this.check_EnableKeepAlive.UseVisualStyleBackColor = true;
            this.check_EnableKeepAlive.CheckedChanged += new System.EventHandler(this.check_EnableKeepAlive_CheckedChanged);
            // 
            // fLP_WKID
            // 
            this.fLP_WKID.AutoSize = true;
            this.fLP_WKID.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_WKID.Controls.Add(this.lbl_KeepAlive_IPProfileID);
            this.fLP_WKID.Controls.Add(this.combo_KeepAlive_WakeUPProfileID);
            this.fLP_WKID.Location = new System.Drawing.Point(10, 78);
            this.fLP_WKID.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.fLP_WKID.Name = "fLP_WKID";
            this.fLP_WKID.Size = new System.Drawing.Size(168, 22);
            this.fLP_WKID.TabIndex = 52;
            // 
            // lbl_KeepAlive_IPProfileID
            // 
            this.lbl_KeepAlive_IPProfileID.AutoSize = true;
            this.lbl_KeepAlive_IPProfileID.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_KeepAlive_IPProfileID.ForeColor = System.Drawing.Color.Black;
            this.lbl_KeepAlive_IPProfileID.Location = new System.Drawing.Point(3, 3);
            this.lbl_KeepAlive_IPProfileID.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_KeepAlive_IPProfileID.Name = "lbl_KeepAlive_IPProfileID";
            this.lbl_KeepAlive_IPProfileID.Size = new System.Drawing.Size(105, 15);
            this.lbl_KeepAlive_IPProfileID.TabIndex = 34;
            this.lbl_KeepAlive_IPProfileID.Text = "Wakeup Profile ID";
            // 
            // combo_KeepAlive_WakeUPProfileID
            // 
            this.combo_KeepAlive_WakeUPProfileID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_KeepAlive_WakeUPProfileID.FormattingEnabled = true;
            this.combo_KeepAlive_WakeUPProfileID.Location = new System.Drawing.Point(121, 0);
            this.combo_KeepAlive_WakeUPProfileID.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.combo_KeepAlive_WakeUPProfileID.Name = "combo_KeepAlive_WakeUPProfileID";
            this.combo_KeepAlive_WakeUPProfileID.Size = new System.Drawing.Size(47, 22);
            this.combo_KeepAlive_WakeUPProfileID.TabIndex = 14;
            this.combo_KeepAlive_WakeUPProfileID.SelectedIndexChanged += new System.EventHandler(this.combo_KeepAlive_WakeUPProfileID_SelectedIndexChanged);
            // 
            // fLP_PingTime
            // 
            this.fLP_PingTime.AutoSize = true;
            this.fLP_PingTime.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_PingTime.Controls.Add(this.lbl_KeepAlive_PingTime);
            this.fLP_PingTime.Controls.Add(this.txt_KeepAlive_PingTimer);
            this.fLP_PingTime.Location = new System.Drawing.Point(10, 106);
            this.fLP_PingTime.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.fLP_PingTime.Name = "fLP_PingTime";
            this.fLP_PingTime.Size = new System.Drawing.Size(195, 22);
            this.fLP_PingTime.TabIndex = 53;
            // 
            // lbl_KeepAlive_PingTime
            // 
            this.lbl_KeepAlive_PingTime.AutoSize = true;
            this.lbl_KeepAlive_PingTime.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_KeepAlive_PingTime.ForeColor = System.Drawing.Color.Black;
            this.lbl_KeepAlive_PingTime.Location = new System.Drawing.Point(3, 3);
            this.lbl_KeepAlive_PingTime.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_KeepAlive_PingTime.Name = "lbl_KeepAlive_PingTime";
            this.lbl_KeepAlive_PingTime.Size = new System.Drawing.Size(59, 15);
            this.lbl_KeepAlive_PingTime.TabIndex = 36;
            this.lbl_KeepAlive_PingTime.Text = "Ping time";
            // 
            // txt_KeepAlive_PingTimer
            // 
            this.txt_KeepAlive_PingTimer.Location = new System.Drawing.Point(121, 0);
            this.txt_KeepAlive_PingTimer.Margin = new System.Windows.Forms.Padding(56, 0, 0, 0);
            this.txt_KeepAlive_PingTimer.Name = "txt_KeepAlive_PingTimer";
            this.txt_KeepAlive_PingTimer.Size = new System.Drawing.Size(74, 22);
            this.txt_KeepAlive_PingTimer.TabIndex = 2;
            this.txt_KeepAlive_PingTimer.Leave += new System.EventHandler(this.txt_KeepAlive_PingTimer_Leave);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label186);
            this.groupBox8.Controls.Add(this.textBox2);
            this.groupBox8.Controls.Add(this.textBox3);
            this.groupBox8.Controls.Add(this.label187);
            this.groupBox8.Controls.Add(this.label188);
            this.groupBox8.Controls.Add(this.label189);
            this.groupBox8.Controls.Add(this.label190);
            this.groupBox8.Controls.Add(this.textBox4);
            this.groupBox8.Controls.Add(this.textBox9);
            this.groupBox8.Controls.Add(this.textBox10);
            this.groupBox8.Enabled = false;
            this.groupBox8.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox8.Location = new System.Drawing.Point(226, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(197, 169);
            this.groupBox8.TabIndex = 51;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "TCP_UDP";
            this.groupBox8.Visible = false;
            // 
            // label186
            // 
            this.label186.AutoSize = true;
            this.label186.Location = new System.Drawing.Point(6, 93);
            this.label186.Name = "label186";
            this.label186.Size = new System.Drawing.Size(66, 14);
            this.label186.TabIndex = 8;
            this.label186.Text = "IP reference";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(133, 91);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(50, 22);
            this.textBox2.TabIndex = 9;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(133, 22);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(50, 22);
            this.textBox3.TabIndex = 5;
            // 
            // label187
            // 
            this.label187.AutoSize = true;
            this.label187.Location = new System.Drawing.Point(6, 26);
            this.label187.Name = "label187";
            this.label187.Size = new System.Drawing.Size(39, 14);
            this.label187.TabIndex = 0;
            this.label187.Text = "IP Port";
            // 
            // label188
            // 
            this.label188.Location = new System.Drawing.Point(6, 116);
            this.label188.Name = "label188";
            this.label188.Size = new System.Drawing.Size(118, 45);
            this.label188.TabIndex = 2;
            this.label188.Text = "Max no of  simulaneous connections";
            // 
            // label189
            // 
            this.label189.AutoSize = true;
            this.label189.Location = new System.Drawing.Point(6, 48);
            this.label189.Name = "label189";
            this.label189.Size = new System.Drawing.Size(124, 14);
            this.label189.TabIndex = 0;
            this.label189.Text = "Max Segmentation Size";
            // 
            // label190
            // 
            this.label190.AutoSize = true;
            this.label190.Location = new System.Drawing.Point(6, 70);
            this.label190.Name = "label190";
            this.label190.Size = new System.Drawing.Size(102, 14);
            this.label190.TabIndex = 1;
            this.label190.Text = "Inactivity Time Out";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(133, 45);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(50, 22);
            this.textBox4.TabIndex = 5;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(133, 68);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(50, 22);
            this.textBox9.TabIndex = 6;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(133, 114);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(50, 22);
            this.textBox10.TabIndex = 7;
            // 
            // errProvider
            // 
            this.errProvider.ContainerControl = this;
            // 
            // chk_enable_wakeup_KeepAlive
            // 
            this.chk_enable_wakeup_KeepAlive.AutoSize = true;
            this.chk_enable_wakeup_KeepAlive.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_enable_wakeup_KeepAlive.ForeColor = System.Drawing.Color.Black;
            this.chk_enable_wakeup_KeepAlive.Location = new System.Drawing.Point(10, 28);
            this.chk_enable_wakeup_KeepAlive.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.chk_enable_wakeup_KeepAlive.Name = "chk_enable_wakeup_KeepAlive";
            this.chk_enable_wakeup_KeepAlive.Size = new System.Drawing.Size(111, 19);
            this.chk_enable_wakeup_KeepAlive.TabIndex = 55;
            this.chk_enable_wakeup_KeepAlive.Text = "Enable WakeUp";
            this.chk_enable_wakeup_KeepAlive.UseVisualStyleBackColor = true;
            this.chk_enable_wakeup_KeepAlive.CheckedChanged += new System.EventHandler(this.chk_enable_wakeup_KeepAlive_CheckedChanged);
            // 
            // check_HeartBeatOnConnection
            // 
            this.check_HeartBeatOnConnection.AutoSize = true;
            this.check_HeartBeatOnConnection.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_HeartBeatOnConnection.ForeColor = System.Drawing.Color.Black;
            this.check_HeartBeatOnConnection.Location = new System.Drawing.Point(10, 53);
            this.check_HeartBeatOnConnection.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.check_HeartBeatOnConnection.Name = "check_HeartBeatOnConnection";
            this.check_HeartBeatOnConnection.Size = new System.Drawing.Size(166, 19);
            this.check_HeartBeatOnConnection.TabIndex = 54;
            this.check_HeartBeatOnConnection.Text = "Heartbeat On Connection";
            this.check_HeartBeatOnConnection.UseVisualStyleBackColor = true;
            this.check_HeartBeatOnConnection.CheckedChanged += new System.EventHandler(this.check_HeartBeatOnConnection_CheckedChanged);
            // 
            // ucKeepAlive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox14);
            this.DoubleBuffered = true;
            this.Name = "ucKeepAlive";
            this.Size = new System.Drawing.Size(225, 170);
            this.Load += new System.EventHandler(this.ucKeepAlive_Load);
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.fLP_Main.ResumeLayout(false);
            this.fLP_Main.PerformLayout();
            this.fLP_WKID.ResumeLayout(false);
            this.fLP_WKID.PerformLayout();
            this.fLP_PingTime.ResumeLayout(false);
            this.fLP_PingTime.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.ComboBox combo_KeepAlive_WakeUPProfileID;
        private System.Windows.Forms.TextBox txt_KeepAlive_PingTimer;
        private System.Windows.Forms.Label lbl_KeepAlive_PingTime;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label186;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label187;
        private System.Windows.Forms.Label label188;
        private System.Windows.Forms.Label label189;
        private System.Windows.Forms.Label label190;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.ErrorProvider errProvider;
        private System.Windows.Forms.Label lbl_KeepAlive_IPProfileID;
        private System.Windows.Forms.FlowLayoutPanel fLP_WKID;
        private System.Windows.Forms.FlowLayoutPanel fLP_PingTime;
        private System.Windows.Forms.FlowLayoutPanel fLP_Main;
        public System.Windows.Forms.CheckBox check_EnableKeepAlive;
        public System.Windows.Forms.CheckBox chk_enable_wakeup_KeepAlive;
        public System.Windows.Forms.CheckBox check_HeartBeatOnConnection;
    }
}
