namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    partial class ucIPProfiles
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
            this.gpIPProfile = new System.Windows.Forms.GroupBox();
            this.fLP_Main = new System.Windows.Forms.FlowLayoutPanel();
            this.fLP_TotalIPProfile = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTotalIP = new System.Windows.Forms.Label();
            this.txt_IPProfile_Total_IP_Profiles = new System.Windows.Forms.ComboBox();
            this.fLP_UniqueId = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_IPProfile_UniqueID = new System.Windows.Forms.Label();
            this.combo_IPProfile_UniqueID = new System.Windows.Forms.ComboBox();
            this.fLP_IP = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_IPProfile_IP = new System.Windows.Forms.Label();
            this.txt_IPProfile_IP = new System.Windows.Forms.MaskedTextBox();
            this.fLP_TCPPort = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_IPProfile_WrapperOverTCPPort = new System.Windows.Forms.Label();
            this.txt_IPProfile_WrapperOverTCP = new System.Windows.Forms.TextBox();
            this.fLP_UDPPort = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_IPProfile_WrapperOverUDPPort = new System.Windows.Forms.Label();
            this.txt_IPProfile_WrapperOverUDP = new System.Windows.Forms.TextBox();
            this.fLP_HDLCTCPPort = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_IPProfile_HDLCOverTCPPort = new System.Windows.Forms.Label();
            this.txt_IPProfile_HDLCOverTCP = new System.Windows.Forms.TextBox();
            this.fLP_HDLCUDPPort = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_IPProfile_HDLCOverUPDPort = new System.Windows.Forms.Label();
            this.txt_IPProfile_HDLCOverUDP = new System.Windows.Forms.TextBox();
            this.gpIPV4 = new System.Windows.Forms.GroupBox();
            this.txt_IPV4_SecondaryDNS = new System.Windows.Forms.MaskedTextBox();
            this.txt_IPV4_PrimaryDNS = new System.Windows.Forms.MaskedTextBox();
            this.txt_IPV4_GatewayIP = new System.Windows.Forms.MaskedTextBox();
            this.txt_IPV4_SubnetMask = new System.Windows.Forms.MaskedTextBox();
            this.txt_IPV4_IP = new System.Windows.Forms.MaskedTextBox();
            this.txt_IPV4_DLReference = new System.Windows.Forms.TextBox();
            this.check_IPV4_DHCP_Flag = new System.Windows.Forms.CheckBox();
            this.lbl_IPV4_SecondaryDNS = new System.Windows.Forms.Label();
            this.lbl_IPV4_PrimaryDNS = new System.Windows.Forms.Label();
            this.lbl_IPV4_GatewayIP = new System.Windows.Forms.Label();
            this.lbl_IPV4_SubnetMask = new System.Windows.Forms.Label();
            this.lbl_IPV4_IP = new System.Windows.Forms.Label();
            this.lbl_IPV4_DLReference = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.gpIPProfile.SuspendLayout();
            this.fLP_Main.SuspendLayout();
            this.fLP_TotalIPProfile.SuspendLayout();
            this.fLP_UniqueId.SuspendLayout();
            this.fLP_IP.SuspendLayout();
            this.fLP_TCPPort.SuspendLayout();
            this.fLP_UDPPort.SuspendLayout();
            this.fLP_HDLCTCPPort.SuspendLayout();
            this.fLP_HDLCUDPPort.SuspendLayout();
            this.gpIPV4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // gpIPProfile
            // 
            this.gpIPProfile.BackColor = System.Drawing.Color.Transparent;
            this.gpIPProfile.Controls.Add(this.fLP_Main);
            this.gpIPProfile.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpIPProfile.ForeColor = System.Drawing.Color.Maroon;
            this.gpIPProfile.Location = new System.Drawing.Point(0, 0);
            this.gpIPProfile.Name = "gpIPProfile";
            this.gpIPProfile.Size = new System.Drawing.Size(213, 221);
            this.gpIPProfile.TabIndex = 12;
            this.gpIPProfile.TabStop = false;
            this.gpIPProfile.Text = "IP Profile";
            // 
            // fLP_Main
            // 
            this.fLP_Main.Controls.Add(this.fLP_TotalIPProfile);
            this.fLP_Main.Controls.Add(this.fLP_UniqueId);
            this.fLP_Main.Controls.Add(this.fLP_IP);
            this.fLP_Main.Controls.Add(this.fLP_TCPPort);
            this.fLP_Main.Controls.Add(this.fLP_UDPPort);
            this.fLP_Main.Controls.Add(this.fLP_HDLCTCPPort);
            this.fLP_Main.Controls.Add(this.fLP_HDLCUDPPort);
            this.fLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fLP_Main.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLP_Main.Location = new System.Drawing.Point(3, 19);
            this.fLP_Main.Margin = new System.Windows.Forms.Padding(0);
            this.fLP_Main.Name = "fLP_Main";
            this.fLP_Main.Size = new System.Drawing.Size(207, 199);
            this.fLP_Main.TabIndex = 14;
            // 
            // fLP_TotalIPProfile
            // 
            this.fLP_TotalIPProfile.AutoSize = true;
            this.fLP_TotalIPProfile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_TotalIPProfile.Controls.Add(this.lblTotalIP);
            this.fLP_TotalIPProfile.Controls.Add(this.txt_IPProfile_Total_IP_Profiles);
            this.fLP_TotalIPProfile.Location = new System.Drawing.Point(3, 3);
            this.fLP_TotalIPProfile.Name = "fLP_TotalIPProfile";
            this.fLP_TotalIPProfile.Size = new System.Drawing.Size(187, 23);
            this.fLP_TotalIPProfile.TabIndex = 49;
            // 
            // lblTotalIP
            // 
            this.lblTotalIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalIP.ForeColor = System.Drawing.Color.Black;
            this.lblTotalIP.Location = new System.Drawing.Point(3, 3);
            this.lblTotalIP.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblTotalIP.Name = "lblTotalIP";
            this.lblTotalIP.Size = new System.Drawing.Size(98, 13);
            this.lblTotalIP.TabIndex = 45;
            this.lblTotalIP.Text = "Total IP Profiles";
            // 
            // txt_IPProfile_Total_IP_Profiles
            // 
            this.txt_IPProfile_Total_IP_Profiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_IPProfile_Total_IP_Profiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txt_IPProfile_Total_IP_Profiles.FormattingEnabled = true;
            this.txt_IPProfile_Total_IP_Profiles.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.txt_IPProfile_Total_IP_Profiles.Location = new System.Drawing.Point(139, 0);
            this.txt_IPProfile_Total_IP_Profiles.Margin = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.txt_IPProfile_Total_IP_Profiles.Name = "txt_IPProfile_Total_IP_Profiles";
            this.txt_IPProfile_Total_IP_Profiles.Size = new System.Drawing.Size(48, 23);
            this.txt_IPProfile_Total_IP_Profiles.Sorted = true;
            this.txt_IPProfile_Total_IP_Profiles.TabIndex = 1;
            this.txt_IPProfile_Total_IP_Profiles.SelectedIndexChanged += new System.EventHandler(this.txt_IPProfile_Total_IP_Profiles_SelectedIndexChanged);
            // 
            // fLP_UniqueId
            // 
            this.fLP_UniqueId.AutoSize = true;
            this.fLP_UniqueId.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_UniqueId.Controls.Add(this.lbl_IPProfile_UniqueID);
            this.fLP_UniqueId.Controls.Add(this.combo_IPProfile_UniqueID);
            this.fLP_UniqueId.Location = new System.Drawing.Point(3, 32);
            this.fLP_UniqueId.Name = "fLP_UniqueId";
            this.fLP_UniqueId.Size = new System.Drawing.Size(187, 23);
            this.fLP_UniqueId.TabIndex = 50;
            // 
            // lbl_IPProfile_UniqueID
            // 
            this.lbl_IPProfile_UniqueID.ForeColor = System.Drawing.Color.Black;
            this.lbl_IPProfile_UniqueID.Location = new System.Drawing.Point(3, 3);
            this.lbl_IPProfile_UniqueID.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_IPProfile_UniqueID.Name = "lbl_IPProfile_UniqueID";
            this.lbl_IPProfile_UniqueID.Size = new System.Drawing.Size(60, 15);
            this.lbl_IPProfile_UniqueID.TabIndex = 45;
            this.lbl_IPProfile_UniqueID.Text = "Unique ID";
            // 
            // combo_IPProfile_UniqueID
            // 
            this.combo_IPProfile_UniqueID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_IPProfile_UniqueID.FormattingEnabled = true;
            this.combo_IPProfile_UniqueID.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.combo_IPProfile_UniqueID.Location = new System.Drawing.Point(139, 0);
            this.combo_IPProfile_UniqueID.Margin = new System.Windows.Forms.Padding(73, 0, 0, 0);
            this.combo_IPProfile_UniqueID.Name = "combo_IPProfile_UniqueID";
            this.combo_IPProfile_UniqueID.Size = new System.Drawing.Size(48, 23);
            this.combo_IPProfile_UniqueID.Sorted = true;
            this.combo_IPProfile_UniqueID.TabIndex = 1;
            this.combo_IPProfile_UniqueID.SelectedIndexChanged += new System.EventHandler(this.combo_IPProfile_UniqueID_SelectedIndexChanged);
            this.combo_IPProfile_UniqueID.Leave += new System.EventHandler(this.combo_IPProfile_UniqueID_SelectedIndexChanged);
            // 
            // fLP_IP
            // 
            this.fLP_IP.AutoSize = true;
            this.fLP_IP.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_IP.Controls.Add(this.lbl_IPProfile_IP);
            this.fLP_IP.Controls.Add(this.txt_IPProfile_IP);
            this.fLP_IP.Location = new System.Drawing.Point(3, 61);
            this.fLP_IP.Name = "fLP_IP";
            this.fLP_IP.Size = new System.Drawing.Size(188, 23);
            this.fLP_IP.TabIndex = 51;
            // 
            // lbl_IPProfile_IP
            // 
            this.lbl_IPProfile_IP.ForeColor = System.Drawing.Color.Black;
            this.lbl_IPProfile_IP.Location = new System.Drawing.Point(3, 3);
            this.lbl_IPProfile_IP.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_IPProfile_IP.Name = "lbl_IPProfile_IP";
            this.lbl_IPProfile_IP.Size = new System.Drawing.Size(17, 15);
            this.lbl_IPProfile_IP.TabIndex = 44;
            this.lbl_IPProfile_IP.Text = "IP";
            // 
            // txt_IPProfile_IP
            // 
            this.txt_IPProfile_IP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_IPProfile_IP.Location = new System.Drawing.Point(93, 0);
            this.txt_IPProfile_IP.Margin = new System.Windows.Forms.Padding(70, 0, 0, 0);
            this.txt_IPProfile_IP.Name = "txt_IPProfile_IP";
            this.txt_IPProfile_IP.Size = new System.Drawing.Size(95, 23);
            this.txt_IPProfile_IP.TabIndex = 2;
            this.txt_IPProfile_IP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_IPProfile_IP.Leave += new System.EventHandler(this.txt_IPProfile_IP_Leave);
            // 
            // fLP_TCPPort
            // 
            this.fLP_TCPPort.AutoSize = true;
            this.fLP_TCPPort.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_TCPPort.Controls.Add(this.lbl_IPProfile_WrapperOverTCPPort);
            this.fLP_TCPPort.Controls.Add(this.txt_IPProfile_WrapperOverTCP);
            this.fLP_TCPPort.Location = new System.Drawing.Point(0, 88);
            this.fLP_TCPPort.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.fLP_TCPPort.Name = "fLP_TCPPort";
            this.fLP_TCPPort.Size = new System.Drawing.Size(191, 23);
            this.fLP_TCPPort.TabIndex = 14;
            // 
            // lbl_IPProfile_WrapperOverTCPPort
            // 
            this.lbl_IPProfile_WrapperOverTCPPort.ForeColor = System.Drawing.Color.Black;
            this.lbl_IPProfile_WrapperOverTCPPort.Location = new System.Drawing.Point(3, 3);
            this.lbl_IPProfile_WrapperOverTCPPort.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_IPProfile_WrapperOverTCPPort.Name = "lbl_IPProfile_WrapperOverTCPPort";
            this.lbl_IPProfile_WrapperOverTCPPort.Size = new System.Drawing.Size(134, 15);
            this.lbl_IPProfile_WrapperOverTCPPort.TabIndex = 47;
            this.lbl_IPProfile_WrapperOverTCPPort.Text = "Wrapper over TCP Port";
            // 
            // txt_IPProfile_WrapperOverTCP
            // 
            this.txt_IPProfile_WrapperOverTCP.Location = new System.Drawing.Point(143, 0);
            this.txt_IPProfile_WrapperOverTCP.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.txt_IPProfile_WrapperOverTCP.Name = "txt_IPProfile_WrapperOverTCP";
            this.txt_IPProfile_WrapperOverTCP.Size = new System.Drawing.Size(48, 23);
            this.txt_IPProfile_WrapperOverTCP.TabIndex = 3;
            this.txt_IPProfile_WrapperOverTCP.Leave += new System.EventHandler(this.txt_IPProfile_WrapperOverTCP_Leave);
            // 
            // fLP_UDPPort
            // 
            this.fLP_UDPPort.AutoSize = true;
            this.fLP_UDPPort.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_UDPPort.Controls.Add(this.lbl_IPProfile_WrapperOverUDPPort);
            this.fLP_UDPPort.Controls.Add(this.txt_IPProfile_WrapperOverUDP);
            this.fLP_UDPPort.Location = new System.Drawing.Point(0, 113);
            this.fLP_UDPPort.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.fLP_UDPPort.Name = "fLP_UDPPort";
            this.fLP_UDPPort.Size = new System.Drawing.Size(191, 23);
            this.fLP_UDPPort.TabIndex = 14;
            // 
            // lbl_IPProfile_WrapperOverUDPPort
            // 
            this.lbl_IPProfile_WrapperOverUDPPort.Enabled = false;
            this.lbl_IPProfile_WrapperOverUDPPort.ForeColor = System.Drawing.Color.Black;
            this.lbl_IPProfile_WrapperOverUDPPort.Location = new System.Drawing.Point(3, 3);
            this.lbl_IPProfile_WrapperOverUDPPort.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_IPProfile_WrapperOverUDPPort.Name = "lbl_IPProfile_WrapperOverUDPPort";
            this.lbl_IPProfile_WrapperOverUDPPort.Size = new System.Drawing.Size(137, 15);
            this.lbl_IPProfile_WrapperOverUDPPort.TabIndex = 43;
            this.lbl_IPProfile_WrapperOverUDPPort.Text = "Wrapper over UDP Port";
            // 
            // txt_IPProfile_WrapperOverUDP
            // 
            this.txt_IPProfile_WrapperOverUDP.Enabled = false;
            this.txt_IPProfile_WrapperOverUDP.Location = new System.Drawing.Point(143, 0);
            this.txt_IPProfile_WrapperOverUDP.Margin = new System.Windows.Forms.Padding(0);
            this.txt_IPProfile_WrapperOverUDP.Name = "txt_IPProfile_WrapperOverUDP";
            this.txt_IPProfile_WrapperOverUDP.Size = new System.Drawing.Size(48, 23);
            this.txt_IPProfile_WrapperOverUDP.TabIndex = 4;
            this.txt_IPProfile_WrapperOverUDP.Leave += new System.EventHandler(this.txt_IPProfile_WrapperOverUDP_Leave);
            // 
            // fLP_HDLCTCPPort
            // 
            this.fLP_HDLCTCPPort.AutoSize = true;
            this.fLP_HDLCTCPPort.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_HDLCTCPPort.Controls.Add(this.lbl_IPProfile_HDLCOverTCPPort);
            this.fLP_HDLCTCPPort.Controls.Add(this.txt_IPProfile_HDLCOverTCP);
            this.fLP_HDLCTCPPort.Location = new System.Drawing.Point(0, 138);
            this.fLP_HDLCTCPPort.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.fLP_HDLCTCPPort.Name = "fLP_HDLCTCPPort";
            this.fLP_HDLCTCPPort.Size = new System.Drawing.Size(191, 23);
            this.fLP_HDLCTCPPort.TabIndex = 14;
            // 
            // lbl_IPProfile_HDLCOverTCPPort
            // 
            this.lbl_IPProfile_HDLCOverTCPPort.Enabled = false;
            this.lbl_IPProfile_HDLCOverTCPPort.ForeColor = System.Drawing.Color.Black;
            this.lbl_IPProfile_HDLCOverTCPPort.Location = new System.Drawing.Point(3, 3);
            this.lbl_IPProfile_HDLCOverTCPPort.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_IPProfile_HDLCOverTCPPort.Name = "lbl_IPProfile_HDLCOverTCPPort";
            this.lbl_IPProfile_HDLCOverTCPPort.Size = new System.Drawing.Size(114, 15);
            this.lbl_IPProfile_HDLCOverTCPPort.TabIndex = 46;
            this.lbl_IPProfile_HDLCOverTCPPort.Text = "HDLC over TCP Port";
            // 
            // txt_IPProfile_HDLCOverTCP
            // 
            this.txt_IPProfile_HDLCOverTCP.Enabled = false;
            this.txt_IPProfile_HDLCOverTCP.Location = new System.Drawing.Point(143, 0);
            this.txt_IPProfile_HDLCOverTCP.Margin = new System.Windows.Forms.Padding(23, 0, 0, 0);
            this.txt_IPProfile_HDLCOverTCP.Name = "txt_IPProfile_HDLCOverTCP";
            this.txt_IPProfile_HDLCOverTCP.Size = new System.Drawing.Size(48, 23);
            this.txt_IPProfile_HDLCOverTCP.TabIndex = 5;
            this.txt_IPProfile_HDLCOverTCP.Leave += new System.EventHandler(this.txt_IPProfile_HDLCOverTCP_Leave);
            // 
            // fLP_HDLCUDPPort
            // 
            this.fLP_HDLCUDPPort.AutoSize = true;
            this.fLP_HDLCUDPPort.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_HDLCUDPPort.Controls.Add(this.lbl_IPProfile_HDLCOverUPDPort);
            this.fLP_HDLCUDPPort.Controls.Add(this.txt_IPProfile_HDLCOverUDP);
            this.fLP_HDLCUDPPort.Location = new System.Drawing.Point(0, 163);
            this.fLP_HDLCUDPPort.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.fLP_HDLCUDPPort.Name = "fLP_HDLCUDPPort";
            this.fLP_HDLCUDPPort.Size = new System.Drawing.Size(191, 23);
            this.fLP_HDLCUDPPort.TabIndex = 14;
            // 
            // lbl_IPProfile_HDLCOverUPDPort
            // 
            this.lbl_IPProfile_HDLCOverUPDPort.BackColor = System.Drawing.Color.Transparent;
            this.lbl_IPProfile_HDLCOverUPDPort.Enabled = false;
            this.lbl_IPProfile_HDLCOverUPDPort.ForeColor = System.Drawing.Color.Black;
            this.lbl_IPProfile_HDLCOverUPDPort.Location = new System.Drawing.Point(3, 3);
            this.lbl_IPProfile_HDLCOverUPDPort.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_IPProfile_HDLCOverUPDPort.Name = "lbl_IPProfile_HDLCOverUPDPort";
            this.lbl_IPProfile_HDLCOverUPDPort.Size = new System.Drawing.Size(117, 15);
            this.lbl_IPProfile_HDLCOverUPDPort.TabIndex = 48;
            this.lbl_IPProfile_HDLCOverUPDPort.Text = "HDLC over UDP Port";
            // 
            // txt_IPProfile_HDLCOverUDP
            // 
            this.txt_IPProfile_HDLCOverUDP.Enabled = false;
            this.txt_IPProfile_HDLCOverUDP.Location = new System.Drawing.Point(143, 0);
            this.txt_IPProfile_HDLCOverUDP.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.txt_IPProfile_HDLCOverUDP.Name = "txt_IPProfile_HDLCOverUDP";
            this.txt_IPProfile_HDLCOverUDP.Size = new System.Drawing.Size(48, 23);
            this.txt_IPProfile_HDLCOverUDP.TabIndex = 6;
            this.txt_IPProfile_HDLCOverUDP.Leave += new System.EventHandler(this.txt_IPProfile_HDLCOverUDP_Leave);
            // 
            // gpIPV4
            // 
            this.gpIPV4.Controls.Add(this.txt_IPV4_SecondaryDNS);
            this.gpIPV4.Controls.Add(this.txt_IPV4_PrimaryDNS);
            this.gpIPV4.Controls.Add(this.txt_IPV4_GatewayIP);
            this.gpIPV4.Controls.Add(this.txt_IPV4_SubnetMask);
            this.gpIPV4.Controls.Add(this.txt_IPV4_IP);
            this.gpIPV4.Controls.Add(this.txt_IPV4_DLReference);
            this.gpIPV4.Controls.Add(this.check_IPV4_DHCP_Flag);
            this.gpIPV4.Controls.Add(this.lbl_IPV4_SecondaryDNS);
            this.gpIPV4.Controls.Add(this.lbl_IPV4_PrimaryDNS);
            this.gpIPV4.Controls.Add(this.lbl_IPV4_GatewayIP);
            this.gpIPV4.Controls.Add(this.lbl_IPV4_SubnetMask);
            this.gpIPV4.Controls.Add(this.lbl_IPV4_IP);
            this.gpIPV4.Controls.Add(this.lbl_IPV4_DLReference);
            this.gpIPV4.Enabled = false;
            this.gpIPV4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.gpIPV4.ForeColor = System.Drawing.Color.Maroon;
            this.gpIPV4.Location = new System.Drawing.Point(219, 0);
            this.gpIPV4.Name = "gpIPV4";
            this.gpIPV4.Size = new System.Drawing.Size(247, 221);
            this.gpIPV4.TabIndex = 13;
            this.gpIPV4.TabStop = false;
            this.gpIPV4.Text = "IPV4";
            this.gpIPV4.Visible = false;
            // 
            // txt_IPV4_SecondaryDNS
            // 
            this.txt_IPV4_SecondaryDNS.Location = new System.Drawing.Point(124, 154);
            this.txt_IPV4_SecondaryDNS.Name = "txt_IPV4_SecondaryDNS";
            this.txt_IPV4_SecondaryDNS.Size = new System.Drawing.Size(114, 23);
            this.txt_IPV4_SecondaryDNS.TabIndex = 6;
            this.txt_IPV4_SecondaryDNS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_IPV4_PrimaryDNS
            // 
            this.txt_IPV4_PrimaryDNS.Location = new System.Drawing.Point(124, 128);
            this.txt_IPV4_PrimaryDNS.Name = "txt_IPV4_PrimaryDNS";
            this.txt_IPV4_PrimaryDNS.Size = new System.Drawing.Size(114, 23);
            this.txt_IPV4_PrimaryDNS.TabIndex = 5;
            this.txt_IPV4_PrimaryDNS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_IPV4_GatewayIP
            // 
            this.txt_IPV4_GatewayIP.Location = new System.Drawing.Point(124, 102);
            this.txt_IPV4_GatewayIP.Name = "txt_IPV4_GatewayIP";
            this.txt_IPV4_GatewayIP.Size = new System.Drawing.Size(114, 23);
            this.txt_IPV4_GatewayIP.TabIndex = 4;
            this.txt_IPV4_GatewayIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_IPV4_SubnetMask
            // 
            this.txt_IPV4_SubnetMask.Location = new System.Drawing.Point(124, 73);
            this.txt_IPV4_SubnetMask.Name = "txt_IPV4_SubnetMask";
            this.txt_IPV4_SubnetMask.Size = new System.Drawing.Size(114, 23);
            this.txt_IPV4_SubnetMask.TabIndex = 3;
            this.txt_IPV4_SubnetMask.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_IPV4_IP
            // 
            this.txt_IPV4_IP.Location = new System.Drawing.Point(124, 47);
            this.txt_IPV4_IP.Name = "txt_IPV4_IP";
            this.txt_IPV4_IP.Size = new System.Drawing.Size(114, 23);
            this.txt_IPV4_IP.TabIndex = 2;
            this.txt_IPV4_IP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_IPV4_DLReference
            // 
            this.txt_IPV4_DLReference.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_IPV4_DLReference.Location = new System.Drawing.Point(124, 19);
            this.txt_IPV4_DLReference.Name = "txt_IPV4_DLReference";
            this.txt_IPV4_DLReference.Size = new System.Drawing.Size(114, 20);
            this.txt_IPV4_DLReference.TabIndex = 0;
            // 
            // check_IPV4_DHCP_Flag
            // 
            this.check_IPV4_DHCP_Flag.AutoSize = true;
            this.check_IPV4_DHCP_Flag.Location = new System.Drawing.Point(92, 192);
            this.check_IPV4_DHCP_Flag.Name = "check_IPV4_DHCP_Flag";
            this.check_IPV4_DHCP_Flag.Size = new System.Drawing.Size(80, 19);
            this.check_IPV4_DHCP_Flag.TabIndex = 7;
            this.check_IPV4_DHCP_Flag.Text = "DHCP Flag";
            this.check_IPV4_DHCP_Flag.UseVisualStyleBackColor = true;
            // 
            // lbl_IPV4_SecondaryDNS
            // 
            this.lbl_IPV4_SecondaryDNS.AutoSize = true;
            this.lbl_IPV4_SecondaryDNS.ForeColor = System.Drawing.Color.Black;
            this.lbl_IPV4_SecondaryDNS.Location = new System.Drawing.Point(4, 153);
            this.lbl_IPV4_SecondaryDNS.Name = "lbl_IPV4_SecondaryDNS";
            this.lbl_IPV4_SecondaryDNS.Size = new System.Drawing.Size(89, 15);
            this.lbl_IPV4_SecondaryDNS.TabIndex = 23;
            this.lbl_IPV4_SecondaryDNS.Text = "Secondary DNS";
            // 
            // lbl_IPV4_PrimaryDNS
            // 
            this.lbl_IPV4_PrimaryDNS.AutoSize = true;
            this.lbl_IPV4_PrimaryDNS.ForeColor = System.Drawing.Color.Black;
            this.lbl_IPV4_PrimaryDNS.Location = new System.Drawing.Point(4, 127);
            this.lbl_IPV4_PrimaryDNS.Name = "lbl_IPV4_PrimaryDNS";
            this.lbl_IPV4_PrimaryDNS.Size = new System.Drawing.Size(76, 15);
            this.lbl_IPV4_PrimaryDNS.TabIndex = 22;
            this.lbl_IPV4_PrimaryDNS.Text = "Primary DNS";
            // 
            // lbl_IPV4_GatewayIP
            // 
            this.lbl_IPV4_GatewayIP.AutoSize = true;
            this.lbl_IPV4_GatewayIP.ForeColor = System.Drawing.Color.Black;
            this.lbl_IPV4_GatewayIP.Location = new System.Drawing.Point(6, 101);
            this.lbl_IPV4_GatewayIP.Name = "lbl_IPV4_GatewayIP";
            this.lbl_IPV4_GatewayIP.Size = new System.Drawing.Size(68, 15);
            this.lbl_IPV4_GatewayIP.TabIndex = 21;
            this.lbl_IPV4_GatewayIP.Text = "Gateway IP";
            // 
            // lbl_IPV4_SubnetMask
            // 
            this.lbl_IPV4_SubnetMask.AutoSize = true;
            this.lbl_IPV4_SubnetMask.ForeColor = System.Drawing.Color.Black;
            this.lbl_IPV4_SubnetMask.Location = new System.Drawing.Point(4, 77);
            this.lbl_IPV4_SubnetMask.Name = "lbl_IPV4_SubnetMask";
            this.lbl_IPV4_SubnetMask.Size = new System.Drawing.Size(77, 15);
            this.lbl_IPV4_SubnetMask.TabIndex = 20;
            this.lbl_IPV4_SubnetMask.Text = "Subnet Mask";
            // 
            // lbl_IPV4_IP
            // 
            this.lbl_IPV4_IP.AutoSize = true;
            this.lbl_IPV4_IP.ForeColor = System.Drawing.Color.Black;
            this.lbl_IPV4_IP.Location = new System.Drawing.Point(6, 50);
            this.lbl_IPV4_IP.Name = "lbl_IPV4_IP";
            this.lbl_IPV4_IP.Size = new System.Drawing.Size(17, 15);
            this.lbl_IPV4_IP.TabIndex = 19;
            this.lbl_IPV4_IP.Text = "IP";
            // 
            // lbl_IPV4_DLReference
            // 
            this.lbl_IPV4_DLReference.AutoSize = true;
            this.lbl_IPV4_DLReference.ForeColor = System.Drawing.Color.Black;
            this.lbl_IPV4_DLReference.Location = new System.Drawing.Point(6, 26);
            this.lbl_IPV4_DLReference.Name = "lbl_IPV4_DLReference";
            this.lbl_IPV4_DLReference.Size = new System.Drawing.Size(78, 15);
            this.lbl_IPV4_DLReference.TabIndex = 18;
            this.lbl_IPV4_DLReference.Text = "DL reference";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ucIPProfiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.gpIPV4);
            this.Controls.Add(this.gpIPProfile);
            this.DoubleBuffered = true;
            this.Name = "ucIPProfiles";
            this.Size = new System.Drawing.Size(469, 223);
            this.Load += new System.EventHandler(this.ucIPProfiles_Load);
            this.gpIPProfile.ResumeLayout(false);
            this.fLP_Main.ResumeLayout(false);
            this.fLP_Main.PerformLayout();
            this.fLP_TotalIPProfile.ResumeLayout(false);
            this.fLP_UniqueId.ResumeLayout(false);
            this.fLP_IP.ResumeLayout(false);
            this.fLP_IP.PerformLayout();
            this.fLP_TCPPort.ResumeLayout(false);
            this.fLP_TCPPort.PerformLayout();
            this.fLP_UDPPort.ResumeLayout(false);
            this.fLP_UDPPort.PerformLayout();
            this.fLP_HDLCTCPPort.ResumeLayout(false);
            this.fLP_HDLCTCPPort.PerformLayout();
            this.fLP_HDLCUDPPort.ResumeLayout(false);
            this.fLP_HDLCUDPPort.PerformLayout();
            this.gpIPV4.ResumeLayout(false);
            this.gpIPV4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpIPProfile;
        private System.Windows.Forms.TextBox txt_IPProfile_WrapperOverTCP;
        private System.Windows.Forms.ComboBox txt_IPProfile_Total_IP_Profiles;
        private System.Windows.Forms.ComboBox combo_IPProfile_UniqueID;
        private System.Windows.Forms.TextBox txt_IPProfile_HDLCOverUDP;
        private System.Windows.Forms.Label lbl_IPProfile_HDLCOverUPDPort;
        private System.Windows.Forms.TextBox txt_IPProfile_HDLCOverTCP;
        private System.Windows.Forms.TextBox txt_IPProfile_WrapperOverUDP;
        private System.Windows.Forms.Label lbl_IPProfile_WrapperOverTCPPort;
        private System.Windows.Forms.Label lbl_IPProfile_HDLCOverTCPPort;
        private System.Windows.Forms.Label lbl_IPProfile_WrapperOverUDPPort;
        private System.Windows.Forms.Label lbl_IPProfile_IP;
        private System.Windows.Forms.Label lblTotalIP;
        private System.Windows.Forms.Label lbl_IPProfile_UniqueID;
        private System.Windows.Forms.GroupBox gpIPV4;
        private System.Windows.Forms.MaskedTextBox txt_IPV4_SecondaryDNS;
        private System.Windows.Forms.MaskedTextBox txt_IPV4_PrimaryDNS;
        private System.Windows.Forms.MaskedTextBox txt_IPV4_GatewayIP;
        private System.Windows.Forms.MaskedTextBox txt_IPV4_SubnetMask;
        private System.Windows.Forms.MaskedTextBox txt_IPV4_IP;
        private System.Windows.Forms.TextBox txt_IPV4_DLReference;
        private System.Windows.Forms.CheckBox check_IPV4_DHCP_Flag;
        private System.Windows.Forms.Label lbl_IPV4_SecondaryDNS;
        private System.Windows.Forms.Label lbl_IPV4_PrimaryDNS;
        private System.Windows.Forms.Label lbl_IPV4_GatewayIP;
        private System.Windows.Forms.Label lbl_IPV4_SubnetMask;
        private System.Windows.Forms.Label lbl_IPV4_IP;
        private System.Windows.Forms.Label lbl_IPV4_DLReference;
        private System.Windows.Forms.MaskedTextBox txt_IPProfile_IP;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.FlowLayoutPanel fLP_TotalIPProfile;
        private System.Windows.Forms.FlowLayoutPanel fLP_UniqueId;
        private System.Windows.Forms.FlowLayoutPanel fLP_IP;
        private System.Windows.Forms.FlowLayoutPanel fLP_TCPPort;
        private System.Windows.Forms.FlowLayoutPanel fLP_UDPPort;
        private System.Windows.Forms.FlowLayoutPanel fLP_HDLCTCPPort;
        private System.Windows.Forms.FlowLayoutPanel fLP_HDLCUDPPort;
        private System.Windows.Forms.FlowLayoutPanel fLP_Main;
    }
}
