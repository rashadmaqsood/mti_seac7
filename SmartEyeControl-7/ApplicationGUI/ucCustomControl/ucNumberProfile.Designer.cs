namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    partial class ucNumberProfile
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
            this.pnl_Regular = new System.Windows.Forms.Panel();
            this.radio_modem_special = new System.Windows.Forms.RadioButton();
            this.radio_modem_normal = new System.Windows.Forms.RadioButton();
            this.lbl_NumberProfile_WakeUpOnVoiceCall = new System.Windows.Forms.Label();
            this.lbl_NumberProfile_UniqueID = new System.Windows.Forms.Label();
            this.lbl_NumberProfile_WakeupOnSMS = new System.Windows.Forms.Label();
            this.check_NumberProfile_VerifyPassword = new System.Windows.Forms.CheckBox();
            this.check_NumberProfile_RejectCall = new System.Windows.Forms.CheckBox();
            this.check_NumberProfile_WakeupOnVoiceCall = new System.Windows.Forms.CheckBox();
            this.check_NumberProfile_Rejectwithattend = new System.Windows.Forms.CheckBox();
            this.check_NumberProfile_AcceptParametersInWakeUpSMS = new System.Windows.Forms.CheckBox();
            this.check_NumberProfile_WakeUpOnSMS = new System.Windows.Forms.CheckBox();
            this.check_NumberProfile_Allow2WaySMSCommunication = new System.Windows.Forms.CheckBox();
            this.lbl_WKType = new System.Windows.Forms.Label();
            this.check_NumberProfile_AcceptDataCall = new System.Windows.Forms.CheckBox();
            this.lbl_totalNumProfile = new System.Windows.Forms.Label();
            this.combo_NumberProfile_UniqueID = new System.Windows.Forms.ComboBox();
            this.combo_NumberProfile_WakeupType = new System.Windows.Forms.ComboBox();
            this.txt_NumberProfile_DataCallNumber = new System.Windows.Forms.TextBox();
            this.combo_NumberProfile_TotalProfiles = new System.Windows.Forms.ComboBox();
            this.lbl_NumberProfile_DataCallNumber = new System.Windows.Forms.Label();
            this.combo_NumberProfile_VoiceCall = new System.Windows.Forms.ComboBox();
            this.txt_NumberProfile_Number = new System.Windows.Forms.TextBox();
            this.lbl_NumberProfile_Number = new System.Windows.Forms.Label();
            this.combo_NumberProfile_SMS = new System.Windows.Forms.ComboBox();
            this.gpNumberProfile = new System.Windows.Forms.GroupBox();
            this.fLP_Main = new System.Windows.Forms.FlowLayoutPanel();
            this.fLP_NumProf_txt = new System.Windows.Forms.FlowLayoutPanel();
            this.fLP_TotalNumProf = new System.Windows.Forms.FlowLayoutPanel();
            this.fLP_UniqueID = new System.Windows.Forms.FlowLayoutPanel();
            this.fLP_WakeupSMS = new System.Windows.Forms.FlowLayoutPanel();
            this.fLP_WK_VoiceCall = new System.Windows.Forms.FlowLayoutPanel();
            this.fLP_WakeupType = new System.Windows.Forms.FlowLayoutPanel();
            this.fLP_NumberProfile = new System.Windows.Forms.FlowLayoutPanel();
            this.fLP_DataCallNumber = new System.Windows.Forms.FlowLayoutPanel();
            this.fLP_Flags = new System.Windows.Forms.FlowLayoutPanel();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnl_Regular.SuspendLayout();
            this.gpNumberProfile.SuspendLayout();
            this.fLP_Main.SuspendLayout();
            this.fLP_NumProf_txt.SuspendLayout();
            this.fLP_TotalNumProf.SuspendLayout();
            this.fLP_UniqueID.SuspendLayout();
            this.fLP_WakeupSMS.SuspendLayout();
            this.fLP_WK_VoiceCall.SuspendLayout();
            this.fLP_WakeupType.SuspendLayout();
            this.fLP_NumberProfile.SuspendLayout();
            this.fLP_DataCallNumber.SuspendLayout();
            this.fLP_Flags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_Regular
            // 
            this.pnl_Regular.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Regular.Controls.Add(this.radio_modem_special);
            this.pnl_Regular.Controls.Add(this.radio_modem_normal);
            this.pnl_Regular.Location = new System.Drawing.Point(453, 88);
            this.pnl_Regular.Name = "pnl_Regular";
            this.pnl_Regular.Size = new System.Drawing.Size(119, 73);
            this.pnl_Regular.TabIndex = 17;
            // 
            // radio_modem_special
            // 
            this.radio_modem_special.AutoSize = true;
            this.radio_modem_special.Location = new System.Drawing.Point(3, 41);
            this.radio_modem_special.Name = "radio_modem_special";
            this.radio_modem_special.Size = new System.Drawing.Size(80, 17);
            this.radio_modem_special.TabIndex = 0;
            this.radio_modem_special.TabStop = true;
            this.radio_modem_special.Text = "Anonymous";
            this.radio_modem_special.UseVisualStyleBackColor = true;
            // 
            // radio_modem_normal
            // 
            this.radio_modem_normal.AutoSize = true;
            this.radio_modem_normal.Checked = true;
            this.radio_modem_normal.Location = new System.Drawing.Point(3, 17);
            this.radio_modem_normal.Name = "radio_modem_normal";
            this.radio_modem_normal.Size = new System.Drawing.Size(62, 17);
            this.radio_modem_normal.TabIndex = 0;
            this.radio_modem_normal.TabStop = true;
            this.radio_modem_normal.Text = "Regular";
            this.radio_modem_normal.UseVisualStyleBackColor = true;
            this.radio_modem_normal.CheckedChanged += new System.EventHandler(this.radio_modem_special_CheckedChanged);
            // 
            // lbl_NumberProfile_WakeUpOnVoiceCall
            // 
            this.lbl_NumberProfile_WakeUpOnVoiceCall.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NumberProfile_WakeUpOnVoiceCall.ForeColor = System.Drawing.Color.Black;
            this.lbl_NumberProfile_WakeUpOnVoiceCall.Location = new System.Drawing.Point(3, 3);
            this.lbl_NumberProfile_WakeUpOnVoiceCall.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.lbl_NumberProfile_WakeUpOnVoiceCall.Name = "lbl_NumberProfile_WakeUpOnVoiceCall";
            this.lbl_NumberProfile_WakeUpOnVoiceCall.Size = new System.Drawing.Size(124, 15);
            this.lbl_NumberProfile_WakeUpOnVoiceCall.TabIndex = 46;
            this.lbl_NumberProfile_WakeUpOnVoiceCall.Text = "Wakeup on Voice Call";
            // 
            // lbl_NumberProfile_UniqueID
            // 
            this.lbl_NumberProfile_UniqueID.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NumberProfile_UniqueID.ForeColor = System.Drawing.Color.Black;
            this.lbl_NumberProfile_UniqueID.Location = new System.Drawing.Point(3, 3);
            this.lbl_NumberProfile_UniqueID.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.lbl_NumberProfile_UniqueID.Name = "lbl_NumberProfile_UniqueID";
            this.lbl_NumberProfile_UniqueID.Size = new System.Drawing.Size(60, 15);
            this.lbl_NumberProfile_UniqueID.TabIndex = 45;
            this.lbl_NumberProfile_UniqueID.Text = "Unique ID";
            // 
            // lbl_NumberProfile_WakeupOnSMS
            // 
            this.lbl_NumberProfile_WakeupOnSMS.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NumberProfile_WakeupOnSMS.ForeColor = System.Drawing.Color.Black;
            this.lbl_NumberProfile_WakeupOnSMS.Location = new System.Drawing.Point(3, 3);
            this.lbl_NumberProfile_WakeupOnSMS.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.lbl_NumberProfile_WakeupOnSMS.Name = "lbl_NumberProfile_WakeupOnSMS";
            this.lbl_NumberProfile_WakeupOnSMS.Size = new System.Drawing.Size(95, 15);
            this.lbl_NumberProfile_WakeupOnSMS.TabIndex = 43;
            this.lbl_NumberProfile_WakeupOnSMS.Text = "Wakeup on SMS";
            // 
            // check_NumberProfile_VerifyPassword
            // 
            this.check_NumberProfile_VerifyPassword.AutoSize = true;
            this.check_NumberProfile_VerifyPassword.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_NumberProfile_VerifyPassword.ForeColor = System.Drawing.Color.Black;
            this.check_NumberProfile_VerifyPassword.Location = new System.Drawing.Point(3, 1);
            this.check_NumberProfile_VerifyPassword.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.check_NumberProfile_VerifyPassword.Name = "check_NumberProfile_VerifyPassword";
            this.check_NumberProfile_VerifyPassword.Size = new System.Drawing.Size(113, 19);
            this.check_NumberProfile_VerifyPassword.TabIndex = 5;
            this.check_NumberProfile_VerifyPassword.Text = "Verify Password";
            this.check_NumberProfile_VerifyPassword.UseVisualStyleBackColor = true;
            this.check_NumberProfile_VerifyPassword.CheckedChanged += new System.EventHandler(this.check_NumberProfile_VerifyPassword_CheckedChanged);
            // 
            // check_NumberProfile_RejectCall
            // 
            this.check_NumberProfile_RejectCall.AutoSize = true;
            this.check_NumberProfile_RejectCall.Enabled = false;
            this.check_NumberProfile_RejectCall.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_NumberProfile_RejectCall.ForeColor = System.Drawing.Color.Black;
            this.check_NumberProfile_RejectCall.Location = new System.Drawing.Point(3, 127);
            this.check_NumberProfile_RejectCall.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.check_NumberProfile_RejectCall.Name = "check_NumberProfile_RejectCall";
            this.check_NumberProfile_RejectCall.Size = new System.Drawing.Size(82, 19);
            this.check_NumberProfile_RejectCall.TabIndex = 7;
            this.check_NumberProfile_RejectCall.Text = "Reject Call";
            this.check_NumberProfile_RejectCall.UseVisualStyleBackColor = true;
            this.check_NumberProfile_RejectCall.CheckedChanged += new System.EventHandler(this.check_NumberProfile__RejectCall_CheckedChanged);
            // 
            // check_NumberProfile_WakeupOnVoiceCall
            // 
            this.check_NumberProfile_WakeupOnVoiceCall.AutoSize = true;
            this.check_NumberProfile_WakeupOnVoiceCall.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_NumberProfile_WakeupOnVoiceCall.ForeColor = System.Drawing.Color.Black;
            this.check_NumberProfile_WakeupOnVoiceCall.Location = new System.Drawing.Point(3, 43);
            this.check_NumberProfile_WakeupOnVoiceCall.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.check_NumberProfile_WakeupOnVoiceCall.Name = "check_NumberProfile_WakeupOnVoiceCall";
            this.check_NumberProfile_WakeupOnVoiceCall.Size = new System.Drawing.Size(141, 19);
            this.check_NumberProfile_WakeupOnVoiceCall.TabIndex = 5;
            this.check_NumberProfile_WakeupOnVoiceCall.Text = "Wakeup On VoiceCall";
            this.check_NumberProfile_WakeupOnVoiceCall.UseVisualStyleBackColor = true;
            this.check_NumberProfile_WakeupOnVoiceCall.CheckedChanged += new System.EventHandler(this.check_NumberProfile__WakeupOnVoiceCall_CheckedChanged);
            // 
            // check_NumberProfile_Rejectwithattend
            // 
            this.check_NumberProfile_Rejectwithattend.AutoSize = true;
            this.check_NumberProfile_Rejectwithattend.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_NumberProfile_Rejectwithattend.ForeColor = System.Drawing.Color.Black;
            this.check_NumberProfile_Rejectwithattend.Location = new System.Drawing.Point(3, 106);
            this.check_NumberProfile_Rejectwithattend.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.check_NumberProfile_Rejectwithattend.Name = "check_NumberProfile_Rejectwithattend";
            this.check_NumberProfile_Rejectwithattend.Size = new System.Drawing.Size(127, 19);
            this.check_NumberProfile_Rejectwithattend.TabIndex = 9;
            this.check_NumberProfile_Rejectwithattend.Text = "Reject with attend";
            this.check_NumberProfile_Rejectwithattend.UseVisualStyleBackColor = true;
            this.check_NumberProfile_Rejectwithattend.CheckedChanged += new System.EventHandler(this.check_NumberProfile__Rejectwithattend_CheckedChanged);
            // 
            // check_NumberProfile_AcceptParametersInWakeUpSMS
            // 
            this.check_NumberProfile_AcceptParametersInWakeUpSMS.AutoSize = true;
            this.check_NumberProfile_AcceptParametersInWakeUpSMS.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_NumberProfile_AcceptParametersInWakeUpSMS.ForeColor = System.Drawing.Color.Black;
            this.check_NumberProfile_AcceptParametersInWakeUpSMS.Location = new System.Drawing.Point(3, 64);
            this.check_NumberProfile_AcceptParametersInWakeUpSMS.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.check_NumberProfile_AcceptParametersInWakeUpSMS.Name = "check_NumberProfile_AcceptParametersInWakeUpSMS";
            this.check_NumberProfile_AcceptParametersInWakeUpSMS.Size = new System.Drawing.Size(218, 19);
            this.check_NumberProfile_AcceptParametersInWakeUpSMS.TabIndex = 6;
            this.check_NumberProfile_AcceptParametersInWakeUpSMS.Text = "Accept parameters in wake up SMS";
            this.check_NumberProfile_AcceptParametersInWakeUpSMS.UseVisualStyleBackColor = true;
            this.check_NumberProfile_AcceptParametersInWakeUpSMS.CheckedChanged += new System.EventHandler(this.check_NumberProfile__AcceptParametersInWakeUpSMS_CheckedChanged);
            // 
            // check_NumberProfile_WakeUpOnSMS
            // 
            this.check_NumberProfile_WakeUpOnSMS.AutoSize = true;
            this.check_NumberProfile_WakeUpOnSMS.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_NumberProfile_WakeUpOnSMS.ForeColor = System.Drawing.Color.Black;
            this.check_NumberProfile_WakeUpOnSMS.Location = new System.Drawing.Point(3, 22);
            this.check_NumberProfile_WakeUpOnSMS.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.check_NumberProfile_WakeUpOnSMS.Name = "check_NumberProfile_WakeUpOnSMS";
            this.check_NumberProfile_WakeUpOnSMS.Size = new System.Drawing.Size(120, 19);
            this.check_NumberProfile_WakeUpOnSMS.TabIndex = 6;
            this.check_NumberProfile_WakeUpOnSMS.Text = "Wake Up On SMS";
            this.check_NumberProfile_WakeUpOnSMS.UseVisualStyleBackColor = true;
            this.check_NumberProfile_WakeUpOnSMS.CheckedChanged += new System.EventHandler(this.check_NumberProfile_WakeUpOnSMS_CheckedChanged);
            // 
            // check_NumberProfile_Allow2WaySMSCommunication
            // 
            this.check_NumberProfile_Allow2WaySMSCommunication.AutoSize = true;
            this.check_NumberProfile_Allow2WaySMSCommunication.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_NumberProfile_Allow2WaySMSCommunication.ForeColor = System.Drawing.Color.Black;
            this.check_NumberProfile_Allow2WaySMSCommunication.Location = new System.Drawing.Point(3, 85);
            this.check_NumberProfile_Allow2WaySMSCommunication.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.check_NumberProfile_Allow2WaySMSCommunication.Name = "check_NumberProfile_Allow2WaySMSCommunication";
            this.check_NumberProfile_Allow2WaySMSCommunication.Size = new System.Drawing.Size(207, 19);
            this.check_NumberProfile_Allow2WaySMSCommunication.TabIndex = 10;
            this.check_NumberProfile_Allow2WaySMSCommunication.Text = "Allow 2 way SMS Communication";
            this.check_NumberProfile_Allow2WaySMSCommunication.UseVisualStyleBackColor = true;
            this.check_NumberProfile_Allow2WaySMSCommunication.CheckedChanged += new System.EventHandler(this.check_NumberProfile__Allow2WaySMSCommunication_CheckedChanged);
            // 
            // lbl_WKType
            // 
            this.lbl_WKType.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_WKType.ForeColor = System.Drawing.Color.Black;
            this.lbl_WKType.Location = new System.Drawing.Point(3, 3);
            this.lbl_WKType.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.lbl_WKType.Name = "lbl_WKType";
            this.lbl_WKType.Size = new System.Drawing.Size(81, 15);
            this.lbl_WKType.TabIndex = 45;
            this.lbl_WKType.Text = "Wakeup Type";
            // 
            // check_NumberProfile_AcceptDataCall
            // 
            this.check_NumberProfile_AcceptDataCall.AutoSize = true;
            this.check_NumberProfile_AcceptDataCall.Enabled = false;
            this.check_NumberProfile_AcceptDataCall.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_NumberProfile_AcceptDataCall.ForeColor = System.Drawing.Color.Black;
            this.check_NumberProfile_AcceptDataCall.Location = new System.Drawing.Point(3, 148);
            this.check_NumberProfile_AcceptDataCall.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.check_NumberProfile_AcceptDataCall.Name = "check_NumberProfile_AcceptDataCall";
            this.check_NumberProfile_AcceptDataCall.Size = new System.Drawing.Size(113, 19);
            this.check_NumberProfile_AcceptDataCall.TabIndex = 12;
            this.check_NumberProfile_AcceptDataCall.Text = "Accept Data Call";
            this.check_NumberProfile_AcceptDataCall.UseVisualStyleBackColor = true;
            this.check_NumberProfile_AcceptDataCall.CheckedChanged += new System.EventHandler(this.check_NumberProfile__AcceptDataCall_CheckedChanged);
            // 
            // lbl_totalNumProfile
            // 
            this.lbl_totalNumProfile.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_totalNumProfile.ForeColor = System.Drawing.Color.Black;
            this.lbl_totalNumProfile.Location = new System.Drawing.Point(3, 3);
            this.lbl_totalNumProfile.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.lbl_totalNumProfile.Name = "lbl_totalNumProfile";
            this.lbl_totalNumProfile.Size = new System.Drawing.Size(127, 15);
            this.lbl_totalNumProfile.TabIndex = 50;
            this.lbl_totalNumProfile.Text = "Total Number Profiles";
            // 
            // combo_NumberProfile_UniqueID
            // 
            this.combo_NumberProfile_UniqueID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_NumberProfile_UniqueID.FormattingEnabled = true;
            this.combo_NumberProfile_UniqueID.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.combo_NumberProfile_UniqueID.Location = new System.Drawing.Point(147, 0);
            this.combo_NumberProfile_UniqueID.Margin = new System.Windows.Forms.Padding(84, 0, 0, 0);
            this.combo_NumberProfile_UniqueID.Name = "combo_NumberProfile_UniqueID";
            this.combo_NumberProfile_UniqueID.Size = new System.Drawing.Size(48, 22);
            this.combo_NumberProfile_UniqueID.TabIndex = 1;
            this.combo_NumberProfile_UniqueID.SelectedIndexChanged += new System.EventHandler(this.combo_NumberProfile_UniqueID_SelectedIndexChanged);
            // 
            // combo_NumberProfile_WakeupType
            // 
            this.combo_NumberProfile_WakeupType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_NumberProfile_WakeupType.Enabled = false;
            this.combo_NumberProfile_WakeupType.FormattingEnabled = true;
            this.combo_NumberProfile_WakeupType.Items.AddRange(new object[] {
            "TCP Connect",
            "Event Notification"});
            this.combo_NumberProfile_WakeupType.Location = new System.Drawing.Point(107, 0);
            this.combo_NumberProfile_WakeupType.Margin = new System.Windows.Forms.Padding(23, 0, 0, 0);
            this.combo_NumberProfile_WakeupType.Name = "combo_NumberProfile_WakeupType";
            this.combo_NumberProfile_WakeupType.Size = new System.Drawing.Size(88, 22);
            this.combo_NumberProfile_WakeupType.TabIndex = 1;
            this.combo_NumberProfile_WakeupType.SelectedIndexChanged += new System.EventHandler(this.combo_NumberProfile_WakeupType_SelectedIndexChanged);
            // 
            // txt_NumberProfile_DataCallNumber
            // 
            this.txt_NumberProfile_DataCallNumber.Enabled = false;
            this.txt_NumberProfile_DataCallNumber.Location = new System.Drawing.Point(106, 0);
            this.txt_NumberProfile_DataCallNumber.Margin = new System.Windows.Forms.Padding(0);
            this.txt_NumberProfile_DataCallNumber.MaxLength = 15;
            this.txt_NumberProfile_DataCallNumber.Name = "txt_NumberProfile_DataCallNumber";
            this.txt_NumberProfile_DataCallNumber.Size = new System.Drawing.Size(88, 22);
            this.txt_NumberProfile_DataCallNumber.TabIndex = 3;
            this.txt_NumberProfile_DataCallNumber.Leave += new System.EventHandler(this.txt_NumberProfile_DataCallNumber_Leave);
            // 
            // combo_NumberProfile_TotalProfiles
            // 
            this.combo_NumberProfile_TotalProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_NumberProfile_TotalProfiles.FormattingEnabled = true;
            this.combo_NumberProfile_TotalProfiles.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.combo_NumberProfile_TotalProfiles.Location = new System.Drawing.Point(147, 0);
            this.combo_NumberProfile_TotalProfiles.Margin = new System.Windows.Forms.Padding(17, 0, 0, 0);
            this.combo_NumberProfile_TotalProfiles.Name = "combo_NumberProfile_TotalProfiles";
            this.combo_NumberProfile_TotalProfiles.Size = new System.Drawing.Size(48, 22);
            this.combo_NumberProfile_TotalProfiles.Sorted = true;
            this.combo_NumberProfile_TotalProfiles.TabIndex = 49;
            this.combo_NumberProfile_TotalProfiles.SelectedIndexChanged += new System.EventHandler(this.combo_NumberProfile_TotalProfiles_SelectedIndexChanged);
            // 
            // lbl_NumberProfile_DataCallNumber
            // 
            this.lbl_NumberProfile_DataCallNumber.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NumberProfile_DataCallNumber.ForeColor = System.Drawing.Color.Black;
            this.lbl_NumberProfile_DataCallNumber.Location = new System.Drawing.Point(3, 3);
            this.lbl_NumberProfile_DataCallNumber.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.lbl_NumberProfile_DataCallNumber.Name = "lbl_NumberProfile_DataCallNumber";
            this.lbl_NumberProfile_DataCallNumber.Size = new System.Drawing.Size(103, 15);
            this.lbl_NumberProfile_DataCallNumber.TabIndex = 47;
            this.lbl_NumberProfile_DataCallNumber.Text = "Data Call Number";
            // 
            // combo_NumberProfile_VoiceCall
            // 
            this.combo_NumberProfile_VoiceCall.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_NumberProfile_VoiceCall.FormattingEnabled = true;
            this.combo_NumberProfile_VoiceCall.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.combo_NumberProfile_VoiceCall.Location = new System.Drawing.Point(147, 0);
            this.combo_NumberProfile_VoiceCall.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.combo_NumberProfile_VoiceCall.Name = "combo_NumberProfile_VoiceCall";
            this.combo_NumberProfile_VoiceCall.Size = new System.Drawing.Size(48, 22);
            this.combo_NumberProfile_VoiceCall.Sorted = true;
            this.combo_NumberProfile_VoiceCall.TabIndex = 49;
            this.combo_NumberProfile_VoiceCall.SelectedIndexChanged += new System.EventHandler(this.combo_NumberProfile_VoiceCall_SelectedIndexChanged);
            // 
            // txt_NumberProfile_Number
            // 
            this.txt_NumberProfile_Number.Location = new System.Drawing.Point(106, 0);
            this.txt_NumberProfile_Number.Margin = new System.Windows.Forms.Padding(50, 0, 0, 0);
            this.txt_NumberProfile_Number.MaxLength = 15;
            this.txt_NumberProfile_Number.Name = "txt_NumberProfile_Number";
            this.txt_NumberProfile_Number.Size = new System.Drawing.Size(88, 22);
            this.txt_NumberProfile_Number.TabIndex = 2;
            this.txt_NumberProfile_Number.Leave += new System.EventHandler(this.txt_NumberProfile_Number_Leave);
            // 
            // lbl_NumberProfile_Number
            // 
            this.lbl_NumberProfile_Number.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NumberProfile_Number.ForeColor = System.Drawing.Color.Black;
            this.lbl_NumberProfile_Number.Location = new System.Drawing.Point(3, 3);
            this.lbl_NumberProfile_Number.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.lbl_NumberProfile_Number.Name = "lbl_NumberProfile_Number";
            this.lbl_NumberProfile_Number.Size = new System.Drawing.Size(53, 15);
            this.lbl_NumberProfile_Number.TabIndex = 44;
            this.lbl_NumberProfile_Number.Text = "Number";
            // 
            // combo_NumberProfile_SMS
            // 
            this.combo_NumberProfile_SMS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_NumberProfile_SMS.FormattingEnabled = true;
            this.combo_NumberProfile_SMS.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.combo_NumberProfile_SMS.Location = new System.Drawing.Point(147, 0);
            this.combo_NumberProfile_SMS.Margin = new System.Windows.Forms.Padding(49, 0, 0, 0);
            this.combo_NumberProfile_SMS.Name = "combo_NumberProfile_SMS";
            this.combo_NumberProfile_SMS.Size = new System.Drawing.Size(48, 22);
            this.combo_NumberProfile_SMS.Sorted = true;
            this.combo_NumberProfile_SMS.TabIndex = 49;
            this.combo_NumberProfile_SMS.SelectedIndexChanged += new System.EventHandler(this.combo_NumberProfile_SMS_SelectedIndexChanged);
            // 
            // gpNumberProfile
            // 
            this.gpNumberProfile.AutoSize = true;
            this.gpNumberProfile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gpNumberProfile.BackColor = System.Drawing.Color.Transparent;
            this.gpNumberProfile.Controls.Add(this.fLP_Main);
            this.gpNumberProfile.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold);
            this.gpNumberProfile.ForeColor = System.Drawing.Color.Maroon;
            this.gpNumberProfile.Location = new System.Drawing.Point(0, 0);
            this.gpNumberProfile.Name = "gpNumberProfile";
            this.gpNumberProfile.Size = new System.Drawing.Size(450, 223);
            this.gpNumberProfile.TabIndex = 16;
            this.gpNumberProfile.TabStop = false;
            this.gpNumberProfile.Text = "Number Profile";
            // 
            // fLP_Main
            // 
            this.fLP_Main.AutoSize = true;
            this.fLP_Main.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_Main.Controls.Add(this.fLP_NumProf_txt);
            this.fLP_Main.Controls.Add(this.fLP_Flags);
            this.fLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fLP_Main.Location = new System.Drawing.Point(3, 18);
            this.fLP_Main.Name = "fLP_Main";
            this.fLP_Main.Size = new System.Drawing.Size(444, 202);
            this.fLP_Main.TabIndex = 18;
            // 
            // fLP_NumProf_txt
            // 
            this.fLP_NumProf_txt.AutoSize = true;
            this.fLP_NumProf_txt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_NumProf_txt.Controls.Add(this.fLP_TotalNumProf);
            this.fLP_NumProf_txt.Controls.Add(this.fLP_UniqueID);
            this.fLP_NumProf_txt.Controls.Add(this.fLP_WakeupSMS);
            this.fLP_NumProf_txt.Controls.Add(this.fLP_WK_VoiceCall);
            this.fLP_NumProf_txt.Controls.Add(this.fLP_WakeupType);
            this.fLP_NumProf_txt.Controls.Add(this.fLP_NumberProfile);
            this.fLP_NumProf_txt.Controls.Add(this.fLP_DataCallNumber);
            this.fLP_NumProf_txt.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLP_NumProf_txt.Location = new System.Drawing.Point(3, 3);
            this.fLP_NumProf_txt.Name = "fLP_NumProf_txt";
            this.fLP_NumProf_txt.Size = new System.Drawing.Size(201, 196);
            this.fLP_NumProf_txt.TabIndex = 25;
            // 
            // fLP_TotalNumProf
            // 
            this.fLP_TotalNumProf.AutoSize = true;
            this.fLP_TotalNumProf.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_TotalNumProf.Controls.Add(this.lbl_totalNumProfile);
            this.fLP_TotalNumProf.Controls.Add(this.combo_NumberProfile_TotalProfiles);
            this.fLP_TotalNumProf.Location = new System.Drawing.Point(3, 3);
            this.fLP_TotalNumProf.Name = "fLP_TotalNumProf";
            this.fLP_TotalNumProf.Size = new System.Drawing.Size(195, 22);
            this.fLP_TotalNumProf.TabIndex = 18;
            // 
            // fLP_UniqueID
            // 
            this.fLP_UniqueID.AutoSize = true;
            this.fLP_UniqueID.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_UniqueID.Controls.Add(this.lbl_NumberProfile_UniqueID);
            this.fLP_UniqueID.Controls.Add(this.combo_NumberProfile_UniqueID);
            this.fLP_UniqueID.Location = new System.Drawing.Point(3, 31);
            this.fLP_UniqueID.Name = "fLP_UniqueID";
            this.fLP_UniqueID.Size = new System.Drawing.Size(195, 22);
            this.fLP_UniqueID.TabIndex = 19;
            // 
            // fLP_WakeupSMS
            // 
            this.fLP_WakeupSMS.AutoSize = true;
            this.fLP_WakeupSMS.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_WakeupSMS.Controls.Add(this.lbl_NumberProfile_WakeupOnSMS);
            this.fLP_WakeupSMS.Controls.Add(this.combo_NumberProfile_SMS);
            this.fLP_WakeupSMS.Location = new System.Drawing.Point(3, 59);
            this.fLP_WakeupSMS.Name = "fLP_WakeupSMS";
            this.fLP_WakeupSMS.Size = new System.Drawing.Size(195, 22);
            this.fLP_WakeupSMS.TabIndex = 21;
            // 
            // fLP_WK_VoiceCall
            // 
            this.fLP_WK_VoiceCall.AutoSize = true;
            this.fLP_WK_VoiceCall.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_WK_VoiceCall.Controls.Add(this.lbl_NumberProfile_WakeUpOnVoiceCall);
            this.fLP_WK_VoiceCall.Controls.Add(this.combo_NumberProfile_VoiceCall);
            this.fLP_WK_VoiceCall.Location = new System.Drawing.Point(3, 87);
            this.fLP_WK_VoiceCall.Name = "fLP_WK_VoiceCall";
            this.fLP_WK_VoiceCall.Size = new System.Drawing.Size(195, 22);
            this.fLP_WK_VoiceCall.TabIndex = 20;
            // 
            // fLP_WakeupType
            // 
            this.fLP_WakeupType.AutoSize = true;
            this.fLP_WakeupType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_WakeupType.Controls.Add(this.lbl_WKType);
            this.fLP_WakeupType.Controls.Add(this.combo_NumberProfile_WakeupType);
            this.fLP_WakeupType.Location = new System.Drawing.Point(3, 115);
            this.fLP_WakeupType.Name = "fLP_WakeupType";
            this.fLP_WakeupType.Size = new System.Drawing.Size(195, 22);
            this.fLP_WakeupType.TabIndex = 22;
            // 
            // fLP_NumberProfile
            // 
            this.fLP_NumberProfile.AutoSize = true;
            this.fLP_NumberProfile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_NumberProfile.Controls.Add(this.lbl_NumberProfile_Number);
            this.fLP_NumberProfile.Controls.Add(this.txt_NumberProfile_Number);
            this.fLP_NumberProfile.Location = new System.Drawing.Point(3, 143);
            this.fLP_NumberProfile.Name = "fLP_NumberProfile";
            this.fLP_NumberProfile.Size = new System.Drawing.Size(194, 22);
            this.fLP_NumberProfile.TabIndex = 23;
            // 
            // fLP_DataCallNumber
            // 
            this.fLP_DataCallNumber.AutoSize = true;
            this.fLP_DataCallNumber.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_DataCallNumber.Controls.Add(this.lbl_NumberProfile_DataCallNumber);
            this.fLP_DataCallNumber.Controls.Add(this.txt_NumberProfile_DataCallNumber);
            this.fLP_DataCallNumber.Location = new System.Drawing.Point(3, 171);
            this.fLP_DataCallNumber.Name = "fLP_DataCallNumber";
            this.fLP_DataCallNumber.Size = new System.Drawing.Size(194, 22);
            this.fLP_DataCallNumber.TabIndex = 24;
            // 
            // fLP_Flags
            // 
            this.fLP_Flags.AutoSize = true;
            this.fLP_Flags.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_Flags.Controls.Add(this.check_NumberProfile_VerifyPassword);
            this.fLP_Flags.Controls.Add(this.check_NumberProfile_WakeUpOnSMS);
            this.fLP_Flags.Controls.Add(this.check_NumberProfile_WakeupOnVoiceCall);
            this.fLP_Flags.Controls.Add(this.check_NumberProfile_AcceptParametersInWakeUpSMS);
            this.fLP_Flags.Controls.Add(this.check_NumberProfile_Allow2WaySMSCommunication);
            this.fLP_Flags.Controls.Add(this.check_NumberProfile_Rejectwithattend);
            this.fLP_Flags.Controls.Add(this.check_NumberProfile_RejectCall);
            this.fLP_Flags.Controls.Add(this.check_NumberProfile_AcceptDataCall);
            this.fLP_Flags.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLP_Flags.Location = new System.Drawing.Point(217, 15);
            this.fLP_Flags.Margin = new System.Windows.Forms.Padding(10, 15, 3, 3);
            this.fLP_Flags.Name = "fLP_Flags";
            this.fLP_Flags.Size = new System.Drawing.Size(224, 168);
            this.fLP_Flags.TabIndex = 18;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ucNumberProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pnl_Regular);
            this.Controls.Add(this.gpNumberProfile);
            this.DoubleBuffered = true;
            this.Name = "ucNumberProfile";
            this.Size = new System.Drawing.Size(582, 226);
            this.Load += new System.EventHandler(this.ucNumberProfile_Load);
            this.pnl_Regular.ResumeLayout(false);
            this.pnl_Regular.PerformLayout();
            this.gpNumberProfile.ResumeLayout(false);
            this.gpNumberProfile.PerformLayout();
            this.fLP_Main.ResumeLayout(false);
            this.fLP_Main.PerformLayout();
            this.fLP_NumProf_txt.ResumeLayout(false);
            this.fLP_NumProf_txt.PerformLayout();
            this.fLP_TotalNumProf.ResumeLayout(false);
            this.fLP_UniqueID.ResumeLayout(false);
            this.fLP_WakeupSMS.ResumeLayout(false);
            this.fLP_WK_VoiceCall.ResumeLayout(false);
            this.fLP_WakeupType.ResumeLayout(false);
            this.fLP_NumberProfile.ResumeLayout(false);
            this.fLP_NumberProfile.PerformLayout();
            this.fLP_DataCallNumber.ResumeLayout(false);
            this.fLP_DataCallNumber.PerformLayout();
            this.fLP_Flags.ResumeLayout(false);
            this.fLP_Flags.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Regular;
        private System.Windows.Forms.RadioButton radio_modem_special;
        private System.Windows.Forms.RadioButton radio_modem_normal;
        public System.Windows.Forms.Label lbl_NumberProfile_WakeUpOnVoiceCall;
        public System.Windows.Forms.Label lbl_NumberProfile_UniqueID;
        public System.Windows.Forms.Label lbl_NumberProfile_WakeupOnSMS;
        public System.Windows.Forms.CheckBox check_NumberProfile_VerifyPassword;
        public System.Windows.Forms.CheckBox check_NumberProfile_RejectCall;
        public System.Windows.Forms.CheckBox check_NumberProfile_WakeupOnVoiceCall;
        public System.Windows.Forms.CheckBox check_NumberProfile_Rejectwithattend;
        public System.Windows.Forms.CheckBox check_NumberProfile_AcceptParametersInWakeUpSMS;
        public System.Windows.Forms.CheckBox check_NumberProfile_WakeUpOnSMS;
        public System.Windows.Forms.CheckBox check_NumberProfile_Allow2WaySMSCommunication;
        public System.Windows.Forms.Label lbl_WKType;
        public System.Windows.Forms.CheckBox check_NumberProfile_AcceptDataCall;
        private System.Windows.Forms.Label lbl_totalNumProfile;
        public System.Windows.Forms.ComboBox combo_NumberProfile_UniqueID;
        public System.Windows.Forms.ComboBox combo_NumberProfile_WakeupType;
        public System.Windows.Forms.TextBox txt_NumberProfile_DataCallNumber;
        private System.Windows.Forms.ComboBox combo_NumberProfile_TotalProfiles;
        public System.Windows.Forms.Label lbl_NumberProfile_DataCallNumber;
        private System.Windows.Forms.ComboBox combo_NumberProfile_VoiceCall;
        public System.Windows.Forms.TextBox txt_NumberProfile_Number;
        public System.Windows.Forms.Label lbl_NumberProfile_Number;
        private System.Windows.Forms.ComboBox combo_NumberProfile_SMS;
        public System.Windows.Forms.GroupBox gpNumberProfile;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.FlowLayoutPanel fLP_Flags;
        private System.Windows.Forms.FlowLayoutPanel fLP_TotalNumProf;
        private System.Windows.Forms.FlowLayoutPanel fLP_UniqueID;
        private System.Windows.Forms.FlowLayoutPanel fLP_WK_VoiceCall;
        private System.Windows.Forms.FlowLayoutPanel fLP_WakeupSMS;
        private System.Windows.Forms.FlowLayoutPanel fLP_WakeupType;
        private System.Windows.Forms.FlowLayoutPanel fLP_NumberProfile;
        private System.Windows.Forms.FlowLayoutPanel fLP_DataCallNumber;
        private System.Windows.Forms.FlowLayoutPanel fLP_NumProf_txt;
        private System.Windows.Forms.FlowLayoutPanel fLP_Main;
    }
}
