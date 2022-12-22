namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    partial class ucModemInitialize
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
            this.gpModemInit = new System.Windows.Forms.GroupBox();
            this.fLP_Main = new System.Windows.Forms.FlowLayoutPanel();
            this.fLP_APNString = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_ModemInitialize_APNString = new System.Windows.Forms.Label();
            this.txt_ModemInitialize_APNString = new System.Windows.Forms.TextBox();
            this.fLP_UserName = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_ModemInitialize_UserName = new System.Windows.Forms.Label();
            this.txt_ModemInitialize_UserName = new System.Windows.Forms.TextBox();
            this.fLP_Password = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_ModemInitialize_Password = new System.Windows.Forms.Label();
            this.txt_ModemInitialize_Password = new System.Windows.Forms.TextBox();
            this.fLP_WakeupPassword = new System.Windows.Forms.FlowLayoutPanel();
            this.label22 = new System.Windows.Forms.Label();
            this.txt_ModemLimitsAndTime_Wakepassword = new System.Windows.Forms.TextBox();
            this.fLP_PINCode = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_ModemInitialize_PinCode = new System.Windows.Forms.Label();
            this.txt_ModemInitialize_PinCode = new System.Windows.Forms.TextBox();
            this.check_ModemLimitsAndTime_RLRQ_FLAG = new System.Windows.Forms.CheckBox();
            this.check_ModemLimitsAndTime_DecrementEventCounter = new System.Windows.Forms.CheckBox();
            this.check_Flag_FastDisconnect = new System.Windows.Forms.CheckBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.gpModemInit.SuspendLayout();
            this.fLP_Main.SuspendLayout();
            this.fLP_APNString.SuspendLayout();
            this.fLP_UserName.SuspendLayout();
            this.fLP_Password.SuspendLayout();
            this.fLP_WakeupPassword.SuspendLayout();
            this.fLP_PINCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // gpModemInit
            // 
            this.gpModemInit.AutoSize = true;
            this.gpModemInit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gpModemInit.Controls.Add(this.fLP_Main);
            this.gpModemInit.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold);
            this.gpModemInit.ForeColor = System.Drawing.Color.Maroon;
            this.gpModemInit.Location = new System.Drawing.Point(0, 0);
            this.gpModemInit.Name = "gpModemInit";
            this.gpModemInit.Size = new System.Drawing.Size(328, 232);
            this.gpModemInit.TabIndex = 9;
            this.gpModemInit.TabStop = false;
            this.gpModemInit.Text = "Modem Initialize";
            // 
            // fLP_Main
            // 
            this.fLP_Main.AutoSize = true;
            this.fLP_Main.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_Main.Controls.Add(this.fLP_APNString);
            this.fLP_Main.Controls.Add(this.fLP_UserName);
            this.fLP_Main.Controls.Add(this.fLP_Password);
            this.fLP_Main.Controls.Add(this.fLP_WakeupPassword);
            this.fLP_Main.Controls.Add(this.fLP_PINCode);
            this.fLP_Main.Controls.Add(this.check_ModemLimitsAndTime_RLRQ_FLAG);
            this.fLP_Main.Controls.Add(this.check_ModemLimitsAndTime_DecrementEventCounter);
            this.fLP_Main.Controls.Add(this.check_Flag_FastDisconnect);
            this.fLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fLP_Main.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLP_Main.Location = new System.Drawing.Point(3, 18);
            this.fLP_Main.Name = "fLP_Main";
            this.fLP_Main.Size = new System.Drawing.Size(322, 211);
            this.fLP_Main.TabIndex = 10;
            // 
            // fLP_APNString
            // 
            this.fLP_APNString.AutoSize = true;
            this.fLP_APNString.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_APNString.Controls.Add(this.lbl_ModemInitialize_APNString);
            this.fLP_APNString.Controls.Add(this.txt_ModemInitialize_APNString);
            this.fLP_APNString.Location = new System.Drawing.Point(10, 10);
            this.fLP_APNString.Margin = new System.Windows.Forms.Padding(10, 10, 50, 3);
            this.fLP_APNString.Name = "fLP_APNString";
            this.fLP_APNString.Size = new System.Drawing.Size(262, 22);
            this.fLP_APNString.TabIndex = 10;
            // 
            // lbl_ModemInitialize_APNString
            // 
            this.lbl_ModemInitialize_APNString.ForeColor = System.Drawing.Color.Black;
            this.lbl_ModemInitialize_APNString.Location = new System.Drawing.Point(3, 3);
            this.lbl_ModemInitialize_APNString.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_ModemInitialize_APNString.Name = "lbl_ModemInitialize_APNString";
            this.lbl_ModemInitialize_APNString.Size = new System.Drawing.Size(59, 14);
            this.lbl_ModemInitialize_APNString.TabIndex = 17;
            this.lbl_ModemInitialize_APNString.Text = "APN string";
            // 
            // txt_ModemInitialize_APNString
            // 
            this.txt_ModemInitialize_APNString.Location = new System.Drawing.Point(120, 0);
            this.txt_ModemInitialize_APNString.Margin = new System.Windows.Forms.Padding(55, 0, 0, 0);
            this.txt_ModemInitialize_APNString.Name = "txt_ModemInitialize_APNString";
            this.txt_ModemInitialize_APNString.Size = new System.Drawing.Size(142, 22);
            this.txt_ModemInitialize_APNString.TabIndex = 1;
            this.txt_ModemInitialize_APNString.Leave += new System.EventHandler(this.txt_ModemInitialize_APNString_Leave);
            // 
            // fLP_UserName
            // 
            this.fLP_UserName.AutoSize = true;
            this.fLP_UserName.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_UserName.Controls.Add(this.lbl_ModemInitialize_UserName);
            this.fLP_UserName.Controls.Add(this.txt_ModemInitialize_UserName);
            this.fLP_UserName.Location = new System.Drawing.Point(10, 38);
            this.fLP_UserName.Margin = new System.Windows.Forms.Padding(10, 3, 50, 3);
            this.fLP_UserName.Name = "fLP_UserName";
            this.fLP_UserName.Size = new System.Drawing.Size(262, 22);
            this.fLP_UserName.TabIndex = 11;
            // 
            // lbl_ModemInitialize_UserName
            // 
            this.lbl_ModemInitialize_UserName.AutoSize = true;
            this.lbl_ModemInitialize_UserName.ForeColor = System.Drawing.Color.Black;
            this.lbl_ModemInitialize_UserName.Location = new System.Drawing.Point(3, 3);
            this.lbl_ModemInitialize_UserName.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_ModemInitialize_UserName.Name = "lbl_ModemInitialize_UserName";
            this.lbl_ModemInitialize_UserName.Size = new System.Drawing.Size(63, 14);
            this.lbl_ModemInitialize_UserName.TabIndex = 19;
            this.lbl_ModemInitialize_UserName.Text = "User Name";
            // 
            // txt_ModemInitialize_UserName
            // 
            this.txt_ModemInitialize_UserName.Location = new System.Drawing.Point(120, 0);
            this.txt_ModemInitialize_UserName.Margin = new System.Windows.Forms.Padding(51, 0, 0, 0);
            this.txt_ModemInitialize_UserName.MaxLength = 25;
            this.txt_ModemInitialize_UserName.Name = "txt_ModemInitialize_UserName";
            this.txt_ModemInitialize_UserName.Size = new System.Drawing.Size(142, 22);
            this.txt_ModemInitialize_UserName.TabIndex = 2;
            this.txt_ModemInitialize_UserName.Leave += new System.EventHandler(this.txt_ModemInitialize_UserName_Leave);
            // 
            // fLP_Password
            // 
            this.fLP_Password.AutoSize = true;
            this.fLP_Password.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_Password.Controls.Add(this.lbl_ModemInitialize_Password);
            this.fLP_Password.Controls.Add(this.txt_ModemInitialize_Password);
            this.fLP_Password.Location = new System.Drawing.Point(10, 66);
            this.fLP_Password.Margin = new System.Windows.Forms.Padding(10, 3, 50, 3);
            this.fLP_Password.Name = "fLP_Password";
            this.fLP_Password.Size = new System.Drawing.Size(262, 22);
            this.fLP_Password.TabIndex = 12;
            // 
            // lbl_ModemInitialize_Password
            // 
            this.lbl_ModemInitialize_Password.AutoSize = true;
            this.lbl_ModemInitialize_Password.ForeColor = System.Drawing.Color.Black;
            this.lbl_ModemInitialize_Password.Location = new System.Drawing.Point(3, 3);
            this.lbl_ModemInitialize_Password.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_ModemInitialize_Password.Name = "lbl_ModemInitialize_Password";
            this.lbl_ModemInitialize_Password.Size = new System.Drawing.Size(54, 14);
            this.lbl_ModemInitialize_Password.TabIndex = 21;
            this.lbl_ModemInitialize_Password.Text = "Password";
            // 
            // txt_ModemInitialize_Password
            // 
            this.txt_ModemInitialize_Password.Location = new System.Drawing.Point(120, 0);
            this.txt_ModemInitialize_Password.Margin = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.txt_ModemInitialize_Password.MaxLength = 15;
            this.txt_ModemInitialize_Password.Name = "txt_ModemInitialize_Password";
            this.txt_ModemInitialize_Password.Size = new System.Drawing.Size(142, 22);
            this.txt_ModemInitialize_Password.TabIndex = 3;
            this.txt_ModemInitialize_Password.Leave += new System.EventHandler(this.txt_ModemInitialize_Password_Leave);
            // 
            // fLP_WakeupPassword
            // 
            this.fLP_WakeupPassword.AutoSize = true;
            this.fLP_WakeupPassword.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_WakeupPassword.Controls.Add(this.label22);
            this.fLP_WakeupPassword.Controls.Add(this.txt_ModemLimitsAndTime_Wakepassword);
            this.fLP_WakeupPassword.Location = new System.Drawing.Point(10, 94);
            this.fLP_WakeupPassword.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.fLP_WakeupPassword.Name = "fLP_WakeupPassword";
            this.fLP_WakeupPassword.Size = new System.Drawing.Size(223, 22);
            this.fLP_WakeupPassword.TabIndex = 13;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.ForeColor = System.Drawing.Color.Black;
            this.label22.Location = new System.Drawing.Point(3, 3);
            this.label22.Margin = new System.Windows.Forms.Padding(3);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(98, 14);
            this.label22.TabIndex = 19;
            this.label22.Text = "Wakeup Password";
            // 
            // txt_ModemLimitsAndTime_Wakepassword
            // 
            this.txt_ModemLimitsAndTime_Wakepassword.Location = new System.Drawing.Point(120, 0);
            this.txt_ModemLimitsAndTime_Wakepassword.Margin = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.txt_ModemLimitsAndTime_Wakepassword.MaxLength = 20;
            this.txt_ModemLimitsAndTime_Wakepassword.Name = "txt_ModemLimitsAndTime_Wakepassword";
            this.txt_ModemLimitsAndTime_Wakepassword.Size = new System.Drawing.Size(103, 22);
            this.txt_ModemLimitsAndTime_Wakepassword.TabIndex = 3;
            this.txt_ModemLimitsAndTime_Wakepassword.Leave += new System.EventHandler(this.txt_ModemLimitsAndTime_Wakepassword_Leave);
            // 
            // fLP_PINCode
            // 
            this.fLP_PINCode.AutoSize = true;
            this.fLP_PINCode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_PINCode.Controls.Add(this.lbl_ModemInitialize_PinCode);
            this.fLP_PINCode.Controls.Add(this.txt_ModemInitialize_PinCode);
            this.fLP_PINCode.Location = new System.Drawing.Point(10, 122);
            this.fLP_PINCode.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.fLP_PINCode.Name = "fLP_PINCode";
            this.fLP_PINCode.Size = new System.Drawing.Size(187, 22);
            this.fLP_PINCode.TabIndex = 14;
            // 
            // lbl_ModemInitialize_PinCode
            // 
            this.lbl_ModemInitialize_PinCode.AutoSize = true;
            this.lbl_ModemInitialize_PinCode.ForeColor = System.Drawing.Color.Black;
            this.lbl_ModemInitialize_PinCode.Location = new System.Drawing.Point(3, 3);
            this.lbl_ModemInitialize_PinCode.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_ModemInitialize_PinCode.Name = "lbl_ModemInitialize_PinCode";
            this.lbl_ModemInitialize_PinCode.Size = new System.Drawing.Size(50, 14);
            this.lbl_ModemInitialize_PinCode.TabIndex = 23;
            this.lbl_ModemInitialize_PinCode.Text = "PIN code";
            // 
            // txt_ModemInitialize_PinCode
            // 
            this.txt_ModemInitialize_PinCode.Location = new System.Drawing.Point(120, 0);
            this.txt_ModemInitialize_PinCode.Margin = new System.Windows.Forms.Padding(64, 0, 0, 0);
            this.txt_ModemInitialize_PinCode.MaxLength = 4;
            this.txt_ModemInitialize_PinCode.Name = "txt_ModemInitialize_PinCode";
            this.txt_ModemInitialize_PinCode.Size = new System.Drawing.Size(67, 22);
            this.txt_ModemInitialize_PinCode.TabIndex = 24;
            this.txt_ModemInitialize_PinCode.Leave += new System.EventHandler(this.txt_ModemInitialize_PinCode_Leave);
            // 
            // check_ModemLimitsAndTime_RLRQ_FLAG
            // 
            this.check_ModemLimitsAndTime_RLRQ_FLAG.AutoSize = true;
            this.check_ModemLimitsAndTime_RLRQ_FLAG.ForeColor = System.Drawing.Color.Black;
            this.check_ModemLimitsAndTime_RLRQ_FLAG.Location = new System.Drawing.Point(10, 157);
            this.check_ModemLimitsAndTime_RLRQ_FLAG.Margin = new System.Windows.Forms.Padding(10, 10, 3, 0);
            this.check_ModemLimitsAndTime_RLRQ_FLAG.Name = "check_ModemLimitsAndTime_RLRQ_FLAG";
            this.check_ModemLimitsAndTime_RLRQ_FLAG.Size = new System.Drawing.Size(217, 18);
            this.check_ModemLimitsAndTime_RLRQ_FLAG.TabIndex = 8;
            this.check_ModemLimitsAndTime_RLRQ_FLAG.Text = "Release Association on TCP Disconnect";
            this.check_ModemLimitsAndTime_RLRQ_FLAG.UseVisualStyleBackColor = true;
            this.check_ModemLimitsAndTime_RLRQ_FLAG.CheckedChanged += new System.EventHandler(this.check_ModemLimitsAndTime_RLRQ_FLAG_CheckedChanged);
            // 
            // check_ModemLimitsAndTime_DecrementEventCounter
            // 
            this.check_ModemLimitsAndTime_DecrementEventCounter.AutoSize = true;
            this.check_ModemLimitsAndTime_DecrementEventCounter.ForeColor = System.Drawing.Color.Black;
            this.check_ModemLimitsAndTime_DecrementEventCounter.Location = new System.Drawing.Point(10, 175);
            this.check_ModemLimitsAndTime_DecrementEventCounter.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.check_ModemLimitsAndTime_DecrementEventCounter.Name = "check_ModemLimitsAndTime_DecrementEventCounter";
            this.check_ModemLimitsAndTime_DecrementEventCounter.Size = new System.Drawing.Size(153, 18);
            this.check_ModemLimitsAndTime_DecrementEventCounter.TabIndex = 9;
            this.check_ModemLimitsAndTime_DecrementEventCounter.Text = "Decrement Event Counter";
            this.check_ModemLimitsAndTime_DecrementEventCounter.UseVisualStyleBackColor = true;
            this.check_ModemLimitsAndTime_DecrementEventCounter.CheckedChanged += new System.EventHandler(this.check_ModemLimitsAndTime_DecrementEventCounter_CheckedChanged);
            // 
            // check_Flag_FastDisconnect
            // 
            this.check_Flag_FastDisconnect.AutoSize = true;
            this.check_Flag_FastDisconnect.ForeColor = System.Drawing.Color.Black;
            this.check_Flag_FastDisconnect.Location = new System.Drawing.Point(10, 193);
            this.check_Flag_FastDisconnect.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.check_Flag_FastDisconnect.Name = "check_Flag_FastDisconnect";
            this.check_Flag_FastDisconnect.Size = new System.Drawing.Size(104, 18);
            this.check_Flag_FastDisconnect.TabIndex = 9;
            this.check_Flag_FastDisconnect.Text = "Fast Disconnect";
            this.check_Flag_FastDisconnect.UseVisualStyleBackColor = true;
            this.check_Flag_FastDisconnect.CheckedChanged += new System.EventHandler(this.check_Flag_FastDisconnect_CheckedChanged);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ucModemInitialize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.gpModemInit);
            this.DoubleBuffered = true;
            this.Name = "ucModemInitialize";
            this.Size = new System.Drawing.Size(331, 235);
            this.Load += new System.EventHandler(this.ucModemInitialize_Load);
            this.gpModemInit.ResumeLayout(false);
            this.gpModemInit.PerformLayout();
            this.fLP_Main.ResumeLayout(false);
            this.fLP_Main.PerformLayout();
            this.fLP_APNString.ResumeLayout(false);
            this.fLP_APNString.PerformLayout();
            this.fLP_UserName.ResumeLayout(false);
            this.fLP_UserName.PerformLayout();
            this.fLP_Password.ResumeLayout(false);
            this.fLP_Password.PerformLayout();
            this.fLP_WakeupPassword.ResumeLayout(false);
            this.fLP_WakeupPassword.PerformLayout();
            this.fLP_PINCode.ResumeLayout(false);
            this.fLP_PINCode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.CheckBox check_Flag_FastDisconnect;
        public System.Windows.Forms.CheckBox check_ModemLimitsAndTime_DecrementEventCounter;
        public System.Windows.Forms.CheckBox check_ModemLimitsAndTime_RLRQ_FLAG;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.FlowLayoutPanel fLP_APNString;
        private System.Windows.Forms.FlowLayoutPanel fLP_UserName;
        private System.Windows.Forms.FlowLayoutPanel fLP_Password;
        private System.Windows.Forms.FlowLayoutPanel fLP_WakeupPassword;
        private System.Windows.Forms.FlowLayoutPanel fLP_PINCode;
        private System.Windows.Forms.FlowLayoutPanel fLP_Main;
        public System.Windows.Forms.GroupBox gpModemInit;
        public System.Windows.Forms.TextBox txt_ModemInitialize_PinCode;
        public System.Windows.Forms.Label lbl_ModemInitialize_PinCode;
        public System.Windows.Forms.TextBox txt_ModemLimitsAndTime_Wakepassword;
        public System.Windows.Forms.TextBox txt_ModemInitialize_Password;
        public System.Windows.Forms.Label lbl_ModemInitialize_Password;
        public System.Windows.Forms.TextBox txt_ModemInitialize_UserName;
        public System.Windows.Forms.Label label22;
        public System.Windows.Forms.Label lbl_ModemInitialize_UserName;
        public System.Windows.Forms.TextBox txt_ModemInitialize_APNString;
        public System.Windows.Forms.Label lbl_ModemInitialize_APNString;
    }
}
