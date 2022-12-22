namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    partial class ucStandardModemParameters
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
            this.tbStandardModem = new System.Windows.Forms.TabControl();
            this.tpIpProfile = new System.Windows.Forms.TabPage();
            this.label200 = new System.Windows.Forms.Label();
            this.cmb_StandardIPProfileList = new System.Windows.Forms.ComboBox();
            this.groupBox48 = new System.Windows.Forms.GroupBox();
            this.txtSecondaryIp = new System.Windows.Forms.MaskedTextBox();
            this.txtSecodaryTcpPort = new System.Windows.Forms.TextBox();
            this.label188 = new System.Windows.Forms.Label();
            this.label192 = new System.Windows.Forms.Label();
            this.groupBox49 = new System.Windows.Forms.GroupBox();
            this.txt3rdIp = new System.Windows.Forms.MaskedTextBox();
            this.txt3rdTcpPortIpProfile = new System.Windows.Forms.TextBox();
            this.label193 = new System.Windows.Forms.Label();
            this.label195 = new System.Windows.Forms.Label();
            this.groupBox50 = new System.Windows.Forms.GroupBox();
            this.txt4thIpProfile = new System.Windows.Forms.MaskedTextBox();
            this.txt4thTcpPort = new System.Windows.Forms.TextBox();
            this.label196 = new System.Windows.Forms.Label();
            this.label263 = new System.Windows.Forms.Label();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.txtPrimaryIp = new System.Windows.Forms.MaskedTextBox();
            this.txtPrimaryTcpPort = new System.Windows.Forms.TextBox();
            this.label191 = new System.Windows.Forms.Label();
            this.label194 = new System.Windows.Forms.Label();
            this.tpNumberProfile = new System.Windows.Forms.TabPage();
            this.label201 = new System.Windows.Forms.Label();
            this.cmbStandardNumberProfile = new System.Windows.Forms.ComboBox();
            this.groupBox40 = new System.Windows.Forms.GroupBox();
            this.label197 = new System.Windows.Forms.Label();
            this.txtMdf = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox41 = new System.Windows.Forms.GroupBox();
            this.label198 = new System.Windows.Forms.Label();
            this.txtNumberProfile3 = new System.Windows.Forms.TextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.groupBox43 = new System.Windows.Forms.GroupBox();
            this.label199 = new System.Windows.Forms.Label();
            this.txtNumberProfile4 = new System.Windows.Forms.TextBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.groupBox42 = new System.Windows.Forms.GroupBox();
            this.label216 = new System.Windows.Forms.Label();
            this.txtStandardModemDataCallNo = new System.Windows.Forms.TextBox();
            this.checkBox35 = new System.Windows.Forms.CheckBox();
            this.tpKeepAlive = new System.Windows.Forms.TabPage();
            this.groupBox45 = new System.Windows.Forms.GroupBox();
            this.chkKeepALiveStandard = new System.Windows.Forms.CheckBox();
            this.tbStandardModem.SuspendLayout();
            this.tpIpProfile.SuspendLayout();
            this.groupBox48.SuspendLayout();
            this.groupBox49.SuspendLayout();
            this.groupBox50.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.tpNumberProfile.SuspendLayout();
            this.groupBox40.SuspendLayout();
            this.groupBox41.SuspendLayout();
            this.groupBox43.SuspendLayout();
            this.groupBox42.SuspendLayout();
            this.tpKeepAlive.SuspendLayout();
            this.groupBox45.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbStandardModem
            // 
            this.tbStandardModem.Controls.Add(this.tpIpProfile);
            this.tbStandardModem.Controls.Add(this.tpNumberProfile);
            this.tbStandardModem.Controls.Add(this.tpKeepAlive);
            this.tbStandardModem.Location = new System.Drawing.Point(3, 3);
            this.tbStandardModem.Name = "tbStandardModem";
            this.tbStandardModem.SelectedIndex = 0;
            this.tbStandardModem.Size = new System.Drawing.Size(745, 311);
            this.tbStandardModem.TabIndex = 2;
            // 
            // tpIpProfile
            // 
            this.tpIpProfile.BackColor = System.Drawing.Color.Transparent;
            this.tpIpProfile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tpIpProfile.Controls.Add(this.label200);
            this.tpIpProfile.Controls.Add(this.cmb_StandardIPProfileList);
            this.tpIpProfile.Controls.Add(this.groupBox48);
            this.tpIpProfile.Controls.Add(this.groupBox49);
            this.tpIpProfile.Controls.Add(this.groupBox50);
            this.tpIpProfile.Controls.Add(this.groupBox18);
            this.tpIpProfile.Location = new System.Drawing.Point(4, 22);
            this.tpIpProfile.Name = "tpIpProfile";
            this.tpIpProfile.Padding = new System.Windows.Forms.Padding(3);
            this.tpIpProfile.Size = new System.Drawing.Size(737, 285);
            this.tpIpProfile.TabIndex = 0;
            this.tpIpProfile.Text = "IP Profiles";
            this.tpIpProfile.UseVisualStyleBackColor = true;
            // 
            // label200
            // 
            this.label200.AutoSize = true;
            this.label200.Location = new System.Drawing.Point(11, 32);
            this.label200.Name = "label200";
            this.label200.Size = new System.Drawing.Size(87, 13);
            this.label200.TabIndex = 50;
            this.label200.Text = "Select IP Profiles";
            // 
            // cmb_StandardIPProfileList
            // 
            this.cmb_StandardIPProfileList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_StandardIPProfileList.FormattingEnabled = true;
            this.cmb_StandardIPProfileList.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cmb_StandardIPProfileList.Location = new System.Drawing.Point(123, 29);
            this.cmb_StandardIPProfileList.Name = "cmb_StandardIPProfileList";
            this.cmb_StandardIPProfileList.Size = new System.Drawing.Size(155, 21);
            this.cmb_StandardIPProfileList.TabIndex = 49;
            // 
            // groupBox48
            // 
            this.groupBox48.BackColor = System.Drawing.Color.Transparent;
            this.groupBox48.Controls.Add(this.txtSecondaryIp);
            this.groupBox48.Controls.Add(this.txtSecodaryTcpPort);
            this.groupBox48.Controls.Add(this.label188);
            this.groupBox48.Controls.Add(this.label192);
            this.groupBox48.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox48.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox48.Location = new System.Drawing.Point(192, 64);
            this.groupBox48.Name = "groupBox48";
            this.groupBox48.Size = new System.Drawing.Size(172, 156);
            this.groupBox48.TabIndex = 48;
            this.groupBox48.TabStop = false;
            this.groupBox48.Text = "secondry";
            this.groupBox48.Visible = false;
            // 
            // txtSecondaryIp
            // 
            this.txtSecondaryIp.Location = new System.Drawing.Point(28, 51);
            this.txtSecondaryIp.Name = "txtSecondaryIp";
            this.txtSecondaryIp.Size = new System.Drawing.Size(131, 23);
            this.txtSecondaryIp.TabIndex = 3;
            this.txtSecondaryIp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtSecodaryTcpPort
            // 
            this.txtSecodaryTcpPort.Location = new System.Drawing.Point(28, 102);
            this.txtSecodaryTcpPort.Name = "txtSecodaryTcpPort";
            this.txtSecodaryTcpPort.Size = new System.Drawing.Size(78, 23);
            this.txtSecodaryTcpPort.TabIndex = 7;
            // 
            // label188
            // 
            this.label188.AutoSize = true;
            this.label188.ForeColor = System.Drawing.Color.Navy;
            this.label188.Location = new System.Drawing.Point(25, 80);
            this.label188.Name = "label188";
            this.label188.Size = new System.Drawing.Size(134, 15);
            this.label188.TabIndex = 47;
            this.label188.Text = "Wrapper over TCP Port";
            // 
            // label192
            // 
            this.label192.AutoSize = true;
            this.label192.ForeColor = System.Drawing.Color.Navy;
            this.label192.Location = new System.Drawing.Point(25, 29);
            this.label192.Name = "label192";
            this.label192.Size = new System.Drawing.Size(17, 15);
            this.label192.TabIndex = 44;
            this.label192.Text = "IP";
            // 
            // groupBox49
            // 
            this.groupBox49.BackColor = System.Drawing.Color.Transparent;
            this.groupBox49.Controls.Add(this.txt3rdIp);
            this.groupBox49.Controls.Add(this.txt3rdTcpPortIpProfile);
            this.groupBox49.Controls.Add(this.label193);
            this.groupBox49.Controls.Add(this.label195);
            this.groupBox49.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox49.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox49.Location = new System.Drawing.Point(372, 64);
            this.groupBox49.Name = "groupBox49";
            this.groupBox49.Size = new System.Drawing.Size(172, 156);
            this.groupBox49.TabIndex = 48;
            this.groupBox49.TabStop = false;
            this.groupBox49.Text = "3rd";
            this.groupBox49.Visible = false;
            // 
            // txt3rdIp
            // 
            this.txt3rdIp.Location = new System.Drawing.Point(28, 51);
            this.txt3rdIp.Name = "txt3rdIp";
            this.txt3rdIp.Size = new System.Drawing.Size(131, 23);
            this.txt3rdIp.TabIndex = 4;
            this.txt3rdIp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt3rdTcpPortIpProfile
            // 
            this.txt3rdTcpPortIpProfile.Location = new System.Drawing.Point(28, 102);
            this.txt3rdTcpPortIpProfile.Name = "txt3rdTcpPortIpProfile";
            this.txt3rdTcpPortIpProfile.Size = new System.Drawing.Size(78, 23);
            this.txt3rdTcpPortIpProfile.TabIndex = 8;
            // 
            // label193
            // 
            this.label193.AutoSize = true;
            this.label193.ForeColor = System.Drawing.Color.Navy;
            this.label193.Location = new System.Drawing.Point(25, 80);
            this.label193.Name = "label193";
            this.label193.Size = new System.Drawing.Size(134, 15);
            this.label193.TabIndex = 47;
            this.label193.Text = "Wrapper over TCP Port";
            // 
            // label195
            // 
            this.label195.AutoSize = true;
            this.label195.ForeColor = System.Drawing.Color.Navy;
            this.label195.Location = new System.Drawing.Point(25, 29);
            this.label195.Name = "label195";
            this.label195.Size = new System.Drawing.Size(17, 15);
            this.label195.TabIndex = 44;
            this.label195.Text = "IP";
            // 
            // groupBox50
            // 
            this.groupBox50.BackColor = System.Drawing.Color.Transparent;
            this.groupBox50.Controls.Add(this.txt4thIpProfile);
            this.groupBox50.Controls.Add(this.txt4thTcpPort);
            this.groupBox50.Controls.Add(this.label196);
            this.groupBox50.Controls.Add(this.label263);
            this.groupBox50.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox50.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox50.Location = new System.Drawing.Point(550, 64);
            this.groupBox50.Name = "groupBox50";
            this.groupBox50.Size = new System.Drawing.Size(172, 156);
            this.groupBox50.TabIndex = 48;
            this.groupBox50.TabStop = false;
            this.groupBox50.Text = "4th";
            this.groupBox50.Visible = false;
            // 
            // txt4thIpProfile
            // 
            this.txt4thIpProfile.Location = new System.Drawing.Point(28, 51);
            this.txt4thIpProfile.Name = "txt4thIpProfile";
            this.txt4thIpProfile.Size = new System.Drawing.Size(131, 23);
            this.txt4thIpProfile.TabIndex = 5;
            this.txt4thIpProfile.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt4thTcpPort
            // 
            this.txt4thTcpPort.Location = new System.Drawing.Point(28, 102);
            this.txt4thTcpPort.Name = "txt4thTcpPort";
            this.txt4thTcpPort.Size = new System.Drawing.Size(78, 23);
            this.txt4thTcpPort.TabIndex = 9;
            // 
            // label196
            // 
            this.label196.AutoSize = true;
            this.label196.ForeColor = System.Drawing.Color.Navy;
            this.label196.Location = new System.Drawing.Point(25, 80);
            this.label196.Name = "label196";
            this.label196.Size = new System.Drawing.Size(134, 15);
            this.label196.TabIndex = 47;
            this.label196.Text = "Wrapper over TCP Port";
            // 
            // label263
            // 
            this.label263.AutoSize = true;
            this.label263.ForeColor = System.Drawing.Color.Navy;
            this.label263.Location = new System.Drawing.Point(25, 29);
            this.label263.Name = "label263";
            this.label263.Size = new System.Drawing.Size(17, 15);
            this.label263.TabIndex = 44;
            this.label263.Text = "IP";
            // 
            // groupBox18
            // 
            this.groupBox18.BackColor = System.Drawing.Color.Transparent;
            this.groupBox18.Controls.Add(this.txtPrimaryIp);
            this.groupBox18.Controls.Add(this.txtPrimaryTcpPort);
            this.groupBox18.Controls.Add(this.label191);
            this.groupBox18.Controls.Add(this.label194);
            this.groupBox18.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox18.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox18.Location = new System.Drawing.Point(14, 64);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(172, 156);
            this.groupBox18.TabIndex = 11;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "primary";
            this.groupBox18.Visible = false;
            // 
            // txtPrimaryIp
            // 
            this.txtPrimaryIp.Location = new System.Drawing.Point(28, 51);
            this.txtPrimaryIp.Name = "txtPrimaryIp";
            this.txtPrimaryIp.Size = new System.Drawing.Size(131, 23);
            this.txtPrimaryIp.TabIndex = 2;
            this.txtPrimaryIp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPrimaryTcpPort
            // 
            this.txtPrimaryTcpPort.Location = new System.Drawing.Point(28, 102);
            this.txtPrimaryTcpPort.Name = "txtPrimaryTcpPort";
            this.txtPrimaryTcpPort.Size = new System.Drawing.Size(78, 23);
            this.txtPrimaryTcpPort.TabIndex = 6;
            // 
            // label191
            // 
            this.label191.AutoSize = true;
            this.label191.ForeColor = System.Drawing.Color.Navy;
            this.label191.Location = new System.Drawing.Point(25, 80);
            this.label191.Name = "label191";
            this.label191.Size = new System.Drawing.Size(134, 15);
            this.label191.TabIndex = 47;
            this.label191.Text = "Wrapper over TCP Port";
            // 
            // label194
            // 
            this.label194.AutoSize = true;
            this.label194.ForeColor = System.Drawing.Color.Navy;
            this.label194.Location = new System.Drawing.Point(25, 29);
            this.label194.Name = "label194";
            this.label194.Size = new System.Drawing.Size(17, 15);
            this.label194.TabIndex = 44;
            this.label194.Text = "IP";
            // 
            // tpNumberProfile
            // 
            this.tpNumberProfile.BackColor = System.Drawing.Color.Transparent;
            this.tpNumberProfile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tpNumberProfile.Controls.Add(this.label201);
            this.tpNumberProfile.Controls.Add(this.cmbStandardNumberProfile);
            this.tpNumberProfile.Controls.Add(this.groupBox40);
            this.tpNumberProfile.Controls.Add(this.groupBox41);
            this.tpNumberProfile.Controls.Add(this.groupBox43);
            this.tpNumberProfile.Controls.Add(this.groupBox42);
            this.tpNumberProfile.Location = new System.Drawing.Point(4, 22);
            this.tpNumberProfile.Name = "tpNumberProfile";
            this.tpNumberProfile.Size = new System.Drawing.Size(737, 285);
            this.tpNumberProfile.TabIndex = 2;
            this.tpNumberProfile.Text = "Number Profile";
            this.tpNumberProfile.UseVisualStyleBackColor = true;
            // 
            // label201
            // 
            this.label201.AutoSize = true;
            this.label201.Location = new System.Drawing.Point(11, 42);
            this.label201.Name = "label201";
            this.label201.Size = new System.Drawing.Size(114, 13);
            this.label201.TabIndex = 52;
            this.label201.Text = "Select Number Profiles";
            // 
            // cmbStandardNumberProfile
            // 
            this.cmbStandardNumberProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStandardNumberProfile.FormattingEnabled = true;
            this.cmbStandardNumberProfile.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cmbStandardNumberProfile.Location = new System.Drawing.Point(137, 39);
            this.cmbStandardNumberProfile.Name = "cmbStandardNumberProfile";
            this.cmbStandardNumberProfile.Size = new System.Drawing.Size(155, 21);
            this.cmbStandardNumberProfile.TabIndex = 51;
            // 
            // groupBox40
            // 
            this.groupBox40.BackColor = System.Drawing.Color.Transparent;
            this.groupBox40.Controls.Add(this.label197);
            this.groupBox40.Controls.Add(this.txtMdf);
            this.groupBox40.Controls.Add(this.checkBox1);
            this.groupBox40.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox40.Location = new System.Drawing.Point(193, 82);
            this.groupBox40.Name = "groupBox40";
            this.groupBox40.Size = new System.Drawing.Size(171, 111);
            this.groupBox40.TabIndex = 48;
            this.groupBox40.TabStop = false;
            this.groupBox40.Text = "Number 2";
            this.groupBox40.Visible = false;
            // 
            // label197
            // 
            this.label197.AutoSize = true;
            this.label197.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label197.ForeColor = System.Drawing.Color.Navy;
            this.label197.Location = new System.Drawing.Point(8, 30);
            this.label197.Name = "label197";
            this.label197.Size = new System.Drawing.Size(101, 15);
            this.label197.TabIndex = 47;
            this.label197.Text = "Wakeup Number";
            // 
            // txtMdf
            // 
            this.txtMdf.Location = new System.Drawing.Point(11, 52);
            this.txtMdf.MaxLength = 15;
            this.txtMdf.Name = "txtMdf";
            this.txtMdf.Size = new System.Drawing.Size(148, 20);
            this.txtMdf.TabIndex = 4;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(11, 361);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(108, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Wakeup on SMS";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // groupBox41
            // 
            this.groupBox41.BackColor = System.Drawing.Color.Transparent;
            this.groupBox41.Controls.Add(this.label198);
            this.groupBox41.Controls.Add(this.txtNumberProfile3);
            this.groupBox41.Controls.Add(this.checkBox2);
            this.groupBox41.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox41.Location = new System.Drawing.Point(370, 82);
            this.groupBox41.Name = "groupBox41";
            this.groupBox41.Size = new System.Drawing.Size(174, 108);
            this.groupBox41.TabIndex = 48;
            this.groupBox41.TabStop = false;
            this.groupBox41.Text = "Number 3";
            this.groupBox41.Visible = false;
            // 
            // label198
            // 
            this.label198.AutoSize = true;
            this.label198.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label198.ForeColor = System.Drawing.Color.Navy;
            this.label198.Location = new System.Drawing.Point(15, 30);
            this.label198.Name = "label198";
            this.label198.Size = new System.Drawing.Size(101, 15);
            this.label198.TabIndex = 47;
            this.label198.Text = "Wakeup Number";
            // 
            // txtNumberProfile3
            // 
            this.txtNumberProfile3.Location = new System.Drawing.Point(18, 52);
            this.txtNumberProfile3.MaxLength = 15;
            this.txtNumberProfile3.Name = "txtNumberProfile3";
            this.txtNumberProfile3.Size = new System.Drawing.Size(148, 20);
            this.txtNumberProfile3.TabIndex = 5;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(11, 361);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(108, 17);
            this.checkBox2.TabIndex = 7;
            this.checkBox2.Text = "Wakeup on SMS";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // groupBox43
            // 
            this.groupBox43.BackColor = System.Drawing.Color.Transparent;
            this.groupBox43.Controls.Add(this.label199);
            this.groupBox43.Controls.Add(this.txtNumberProfile4);
            this.groupBox43.Controls.Add(this.checkBox3);
            this.groupBox43.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox43.Location = new System.Drawing.Point(550, 81);
            this.groupBox43.Name = "groupBox43";
            this.groupBox43.Size = new System.Drawing.Size(174, 108);
            this.groupBox43.TabIndex = 48;
            this.groupBox43.TabStop = false;
            this.groupBox43.Text = "Number 4";
            this.groupBox43.Visible = false;
            // 
            // label199
            // 
            this.label199.AutoSize = true;
            this.label199.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label199.ForeColor = System.Drawing.Color.Navy;
            this.label199.Location = new System.Drawing.Point(8, 30);
            this.label199.Name = "label199";
            this.label199.Size = new System.Drawing.Size(101, 15);
            this.label199.TabIndex = 47;
            this.label199.Text = "Wakeup Number";
            // 
            // txtNumberProfile4
            // 
            this.txtNumberProfile4.Location = new System.Drawing.Point(11, 52);
            this.txtNumberProfile4.MaxLength = 15;
            this.txtNumberProfile4.Name = "txtNumberProfile4";
            this.txtNumberProfile4.Size = new System.Drawing.Size(148, 20);
            this.txtNumberProfile4.TabIndex = 6;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(11, 361);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(108, 17);
            this.checkBox3.TabIndex = 7;
            this.checkBox3.Text = "Wakeup on SMS";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // groupBox42
            // 
            this.groupBox42.BackColor = System.Drawing.Color.Transparent;
            this.groupBox42.Controls.Add(this.label216);
            this.groupBox42.Controls.Add(this.txtStandardModemDataCallNo);
            this.groupBox42.Controls.Add(this.checkBox35);
            this.groupBox42.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox42.Location = new System.Drawing.Point(14, 82);
            this.groupBox42.Name = "groupBox42";
            this.groupBox42.Size = new System.Drawing.Size(173, 106);
            this.groupBox42.TabIndex = 14;
            this.groupBox42.TabStop = false;
            this.groupBox42.Text = "Number 1";
            // 
            // label216
            // 
            this.label216.AutoSize = true;
            this.label216.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label216.ForeColor = System.Drawing.Color.Navy;
            this.label216.Location = new System.Drawing.Point(8, 30);
            this.label216.Name = "label216";
            this.label216.Size = new System.Drawing.Size(101, 15);
            this.label216.TabIndex = 47;
            this.label216.Text = "Wakeup Number";
            // 
            // txtStandardModemDataCallNo
            // 
            this.txtStandardModemDataCallNo.Location = new System.Drawing.Point(11, 52);
            this.txtStandardModemDataCallNo.MaxLength = 15;
            this.txtStandardModemDataCallNo.Name = "txtStandardModemDataCallNo";
            this.txtStandardModemDataCallNo.Size = new System.Drawing.Size(148, 20);
            this.txtStandardModemDataCallNo.TabIndex = 3;
            // 
            // checkBox35
            // 
            this.checkBox35.AutoSize = true;
            this.checkBox35.Location = new System.Drawing.Point(11, 361);
            this.checkBox35.Name = "checkBox35";
            this.checkBox35.Size = new System.Drawing.Size(108, 17);
            this.checkBox35.TabIndex = 7;
            this.checkBox35.Text = "Wakeup on SMS";
            this.checkBox35.UseVisualStyleBackColor = true;
            // 
            // tpKeepAlive
            // 
            this.tpKeepAlive.BackColor = System.Drawing.Color.Transparent;
            this.tpKeepAlive.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tpKeepAlive.Controls.Add(this.groupBox45);
            this.tpKeepAlive.Location = new System.Drawing.Point(4, 22);
            this.tpKeepAlive.Name = "tpKeepAlive";
            this.tpKeepAlive.Size = new System.Drawing.Size(737, 285);
            this.tpKeepAlive.TabIndex = 4;
            this.tpKeepAlive.Text = "Keep Alive";
            this.tpKeepAlive.UseVisualStyleBackColor = true;
            // 
            // groupBox45
            // 
            this.groupBox45.Controls.Add(this.chkKeepALiveStandard);
            this.groupBox45.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox45.Location = new System.Drawing.Point(43, 33);
            this.groupBox45.Name = "groupBox45";
            this.groupBox45.Size = new System.Drawing.Size(194, 89);
            this.groupBox45.TabIndex = 13;
            this.groupBox45.TabStop = false;
            this.groupBox45.Text = "Keep Alive";
            // 
            // chkKeepALiveStandard
            // 
            this.chkKeepALiveStandard.AutoSize = true;
            this.chkKeepALiveStandard.Checked = true;
            this.chkKeepALiveStandard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKeepALiveStandard.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkKeepALiveStandard.ForeColor = System.Drawing.Color.Navy;
            this.chkKeepALiveStandard.Location = new System.Drawing.Point(17, 38);
            this.chkKeepALiveStandard.Name = "chkKeepALiveStandard";
            this.chkKeepALiveStandard.Size = new System.Drawing.Size(124, 19);
            this.chkKeepALiveStandard.TabIndex = 37;
            this.chkKeepALiveStandard.Text = "Enable Always ON";
            this.chkKeepALiveStandard.UseVisualStyleBackColor = true;
            // 
            // ucStandardModemParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbStandardModem);
            this.Name = "ucStandardModemParameters";
            this.Size = new System.Drawing.Size(750, 319);
            this.tbStandardModem.ResumeLayout(false);
            this.tpIpProfile.ResumeLayout(false);
            this.tpIpProfile.PerformLayout();
            this.groupBox48.ResumeLayout(false);
            this.groupBox48.PerformLayout();
            this.groupBox49.ResumeLayout(false);
            this.groupBox49.PerformLayout();
            this.groupBox50.ResumeLayout(false);
            this.groupBox50.PerformLayout();
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            this.tpNumberProfile.ResumeLayout(false);
            this.tpNumberProfile.PerformLayout();
            this.groupBox40.ResumeLayout(false);
            this.groupBox40.PerformLayout();
            this.groupBox41.ResumeLayout(false);
            this.groupBox41.PerformLayout();
            this.groupBox43.ResumeLayout(false);
            this.groupBox43.PerformLayout();
            this.groupBox42.ResumeLayout(false);
            this.groupBox42.PerformLayout();
            this.tpKeepAlive.ResumeLayout(false);
            this.groupBox45.ResumeLayout(false);
            this.groupBox45.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbStandardModem;
        private System.Windows.Forms.TabPage tpIpProfile;
        private System.Windows.Forms.Label label200;
        private System.Windows.Forms.Label label188;
        private System.Windows.Forms.Label label192;
        private System.Windows.Forms.Label label193;
        private System.Windows.Forms.Label label195;
        private System.Windows.Forms.Label label196;
        private System.Windows.Forms.Label label263;
        private System.Windows.Forms.Label label191;
        private System.Windows.Forms.Label label194;
        private System.Windows.Forms.TabPage tpNumberProfile;
        private System.Windows.Forms.Label label201;
        public System.Windows.Forms.GroupBox groupBox40;
        public System.Windows.Forms.Label label197;
        public System.Windows.Forms.TextBox txtMdf;
        public System.Windows.Forms.CheckBox checkBox1;
        public System.Windows.Forms.GroupBox groupBox41;
        public System.Windows.Forms.Label label198;
        public System.Windows.Forms.TextBox txtNumberProfile3;
        public System.Windows.Forms.CheckBox checkBox2;
        public System.Windows.Forms.GroupBox groupBox43;
        public System.Windows.Forms.Label label199;
        public System.Windows.Forms.TextBox txtNumberProfile4;
        public System.Windows.Forms.CheckBox checkBox3;
        public System.Windows.Forms.GroupBox groupBox42;
        public System.Windows.Forms.Label label216;
        public System.Windows.Forms.TextBox txtStandardModemDataCallNo;
        public System.Windows.Forms.CheckBox checkBox35;
        private System.Windows.Forms.TabPage tpKeepAlive;
        private System.Windows.Forms.GroupBox groupBox45;
        public System.Windows.Forms.ComboBox cmb_StandardIPProfileList;
        public System.Windows.Forms.MaskedTextBox txtSecondaryIp;
        public System.Windows.Forms.TextBox txtSecodaryTcpPort;
        public System.Windows.Forms.MaskedTextBox txt3rdIp;
        public System.Windows.Forms.TextBox txt3rdTcpPortIpProfile;
        public System.Windows.Forms.MaskedTextBox txt4thIpProfile;
        public System.Windows.Forms.TextBox txt4thTcpPort;
        public System.Windows.Forms.MaskedTextBox txtPrimaryIp;
        public System.Windows.Forms.TextBox txtPrimaryTcpPort;
        public System.Windows.Forms.ComboBox cmbStandardNumberProfile;
        public System.Windows.Forms.CheckBox chkKeepALiveStandard;
        public System.Windows.Forms.GroupBox groupBox48;
        public System.Windows.Forms.GroupBox groupBox49;
        public System.Windows.Forms.GroupBox groupBox50;
        public System.Windows.Forms.GroupBox groupBox18;
    }
}
