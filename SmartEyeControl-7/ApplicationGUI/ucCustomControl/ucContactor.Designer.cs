namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    partial class ucContactor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucContactor));
            this.gpContactor = new System.Windows.Forms.GroupBox();
            this.pb_Contactor = new System.Windows.Forms.ProgressBar();
            this.fLP_Buttons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnReadStatus = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_ConnectContactor = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_connectThroughSwitch = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_DisconnectContactor = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.fLP_Params = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_GetContactorParams = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_SetContactorParameters = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.fLP_Main = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.fLP_TopRow = new System.Windows.Forms.FlowLayoutPanel();
            this.fLP_txt_Box_Col = new System.Windows.Forms.FlowLayoutPanel();
            this.fLP_ContactorOn = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_Contactor_ContactorON_PulseTime = new System.Windows.Forms.Label();
            this.txt_Contactor_ContactorON_PulseTime = new System.Windows.Forms.TextBox();
            this.label137 = new System.Windows.Forms.Label();
            this.fLP_ContactorOff = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_Contactor_ContactorOFF_PulseTime = new System.Windows.Forms.Label();
            this.txt_Contactor_ContactorOFF_PulseTime = new System.Windows.Forms.TextBox();
            this.label138 = new System.Windows.Forms.Label();
            this.fLP_MinInterval_Stat = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_Contactor_MinIntervalBwContactorStateChange = new System.Windows.Forms.Label();
            this.txt_Contactor_MinIntervalBwContactorStateChange = new System.Windows.Forms.TextBox();
            this.label139 = new System.Windows.Forms.Label();
            this.fLP_PUPD_Stat = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_Contactor_PowerUpDelayToChangeState = new System.Windows.Forms.Label();
            this.txt_Contactor_PowerUpDelayToChangeState = new System.Windows.Forms.TextBox();
            this.label140 = new System.Windows.Forms.Label();
            this.fLP_Intr_ContFailureStat = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_Interval_Contactor_Failure_Status = new System.Windows.Forms.Label();
            this.txt_Contactor_Failure_Status = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_ContactorStatus = new System.Windows.Forms.Label();
            this.fLP_CheckBoxs = new System.Windows.Forms.FlowLayoutPanel();
            this.check_Contactor_offOptically = new System.Windows.Forms.CheckBox();
            this.check_Contactor_onOptically = new System.Windows.Forms.CheckBox();
            this.gp_Reconnect = new System.Windows.Forms.GroupBox();
            this.fLP_Reconnect = new System.Windows.Forms.FlowLayoutPanel();
            this.check_Contactor_ReconnectonTariffChange = new System.Windows.Forms.CheckBox();
            this.gp_Reconn_Retry = new System.Windows.Forms.GroupBox();
            this.fLP_Retry = new System.Windows.Forms.FlowLayoutPanel();
            this.fLP_Retry_Radio = new System.Windows.Forms.FlowLayoutPanel();
            this.radio_contactor_auto = new System.Windows.Forms.RadioButton();
            this.radio_contactor_switch = new System.Windows.Forms.RadioButton();
            this.fLP_Retry_AutoIntr = new System.Windows.Forms.FlowLayoutPanel();
            this.label175 = new System.Windows.Forms.Label();
            this.txt_Contactor_IntervalBWEntries = new System.Windows.Forms.TextBox();
            this.lbl_AutoInterval = new System.Windows.Forms.Label();
            this.fLP_Retry_Count = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_RetryCount = new System.Windows.Forms.Label();
            this.txt_Contactor_RetryCount = new System.Windows.Forms.TextBox();
            this.gp_RetryExpire = new System.Windows.Forms.GroupBox();
            this.fLP_RetryExpire = new System.Windows.Forms.FlowLayoutPanel();
            this.fLP_RtryExp = new System.Windows.Forms.FlowLayoutPanel();
            this.check_contactor_reconnectAuto = new System.Windows.Forms.CheckBox();
            this.check_contactor_reconnectSwitch = new System.Windows.Forms.CheckBox();
            this.fLP_RetryExpire_AutoInterval = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_OnRetry_AutoInterval = new System.Windows.Forms.Label();
            this.txt_Contactor_ControlMode = new System.Windows.Forms.ComboBox();
            this.label178 = new System.Windows.Forms.Label();
            this.flp_LastRow = new System.Windows.Forms.FlowLayoutPanel();
            this.gp_OverloadLimitControl = new System.Windows.Forms.GroupBox();
            this.gp_turn_off_con = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            this.flp_TCO_Limits = new System.Windows.Forms.FlowLayoutPanel();
            this.flp_TCO_OverLoadTotal = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_Cont_OverLoadTotal = new System.Windows.Forms.Label();
            this.check_Contactor_OverLoadTotal_T1 = new System.Windows.Forms.CheckBox();
            this.check_Contactor_OverLoadTotal_T2 = new System.Windows.Forms.CheckBox();
            this.check_Contactor_OverLoadTotal_T3 = new System.Windows.Forms.CheckBox();
            this.check_Contactor_OverLoadTotal_T4 = new System.Windows.Forms.CheckBox();
            this.flp_TCO_OverCurrentByPhase = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_TCO_OverCurrentByPhaseT1 = new System.Windows.Forms.CheckBox();
            this.cb_TCO_OverCurrentByPhaseT2 = new System.Windows.Forms.CheckBox();
            this.cb_TCO_OverCurrentByPhaseT3 = new System.Windows.Forms.CheckBox();
            this.cb_TCO_OverCurrentByPhaseT4 = new System.Windows.Forms.CheckBox();
            this.flp_TCO_OverLoadByPhase = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_TCO_OverLoadByPhaseT1 = new System.Windows.Forms.CheckBox();
            this.cb_TCO_OverLoadByPhaseT2 = new System.Windows.Forms.CheckBox();
            this.cb_TCO_OverLoadByPhaseT3 = new System.Windows.Forms.CheckBox();
            this.cb_TCO_OverLoadByPhaseT4 = new System.Windows.Forms.CheckBox();
            this.flp_TCO_MdiOverLoad = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_TCO_MdiOverLoadT1 = new System.Windows.Forms.CheckBox();
            this.cb_TCO_MdiOverLoadT2 = new System.Windows.Forms.CheckBox();
            this.cb_TCO_MdiOverLoadT3 = new System.Windows.Forms.CheckBox();
            this.cb_TCO_MdiOverLoadT4 = new System.Windows.Forms.CheckBox();
            this.ftp_TCO_Events = new System.Windows.Forms.FlowLayoutPanel();
            this.cb_TCO_OverVolt = new System.Windows.Forms.CheckBox();
            this.cb_TCO_UnderVolt = new System.Windows.Forms.CheckBox();
            this.cb_TCO_PhaseFail = new System.Windows.Forms.CheckBox();
            this.fLP_OverLoad = new System.Windows.Forms.FlowLayoutPanel();
            this.fLP_OverLoad_Mntr = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_MT_OverloaKW = new System.Windows.Forms.Label();
            this.txt_MonitoringTime_OverLoad_c = new System.Windows.Forms.DateTimePicker();
            this.pnl_OverLoad_Limit = new System.Windows.Forms.Panel();
            this.lbl_Limit_OverLoadTotal = new System.Windows.Forms.Label();
            this.txt_OverLoadTotal_c_T1 = new System.Windows.Forms.TextBox();
            this.txt_OverLoadTotal_c_T2 = new System.Windows.Forms.TextBox();
            this.label184 = new System.Windows.Forms.Label();
            this.label180 = new System.Windows.Forms.Label();
            this.label179 = new System.Windows.Forms.Label();
            this.label176 = new System.Windows.Forms.Label();
            this.txt_OverLoadTotal_c_T4 = new System.Windows.Forms.TextBox();
            this.txt_OverLoadTotal_c_T3 = new System.Windows.Forms.TextBox();
            this.gpContactorControl = new System.Windows.Forms.GroupBox();
            this.check_Status = new System.Windows.Forms.CheckBox();
            this.check_Contactor_LocalControl = new System.Windows.Forms.CheckBox();
            this.lbl_Contactor_OverLoadByPhase = new System.Windows.Forms.Label();
            this.lbl_Contactor_MDIOverLoad = new System.Windows.Forms.Label();
            this.lbl_Contactor_OverCurrentByPhase = new System.Windows.Forms.Label();
            this.check_Contactor_RemoteControl = new System.Windows.Forms.CheckBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.gpContactor.SuspendLayout();
            this.fLP_Buttons.SuspendLayout();
            this.fLP_Params.SuspendLayout();
            this.fLP_Main.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.fLP_TopRow.SuspendLayout();
            this.fLP_txt_Box_Col.SuspendLayout();
            this.fLP_ContactorOn.SuspendLayout();
            this.fLP_ContactorOff.SuspendLayout();
            this.fLP_MinInterval_Stat.SuspendLayout();
            this.fLP_PUPD_Stat.SuspendLayout();
            this.fLP_Intr_ContFailureStat.SuspendLayout();
            this.fLP_CheckBoxs.SuspendLayout();
            this.gp_Reconnect.SuspendLayout();
            this.fLP_Reconnect.SuspendLayout();
            this.gp_Reconn_Retry.SuspendLayout();
            this.fLP_Retry.SuspendLayout();
            this.fLP_Retry_Radio.SuspendLayout();
            this.fLP_Retry_AutoIntr.SuspendLayout();
            this.fLP_Retry_Count.SuspendLayout();
            this.gp_RetryExpire.SuspendLayout();
            this.fLP_RetryExpire.SuspendLayout();
            this.fLP_RtryExp.SuspendLayout();
            this.fLP_RetryExpire_AutoInterval.SuspendLayout();
            this.flp_LastRow.SuspendLayout();
            this.gp_OverloadLimitControl.SuspendLayout();
            this.gp_turn_off_con.SuspendLayout();
            this.flowLayoutPanel7.SuspendLayout();
            this.flp_TCO_Limits.SuspendLayout();
            this.flp_TCO_OverLoadTotal.SuspendLayout();
            this.flp_TCO_OverCurrentByPhase.SuspendLayout();
            this.flp_TCO_OverLoadByPhase.SuspendLayout();
            this.flp_TCO_MdiOverLoad.SuspendLayout();
            this.ftp_TCO_Events.SuspendLayout();
            this.fLP_OverLoad.SuspendLayout();
            this.fLP_OverLoad_Mntr.SuspendLayout();
            this.pnl_OverLoad_Limit.SuspendLayout();
            this.gpContactorControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // gpContactor
            // 
            this.gpContactor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gpContactor.Controls.Add(this.pb_Contactor);
            this.gpContactor.Controls.Add(this.fLP_Buttons);
            this.gpContactor.Controls.Add(this.fLP_Main);
            this.gpContactor.Controls.Add(this.gpContactorControl);
            this.gpContactor.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.gpContactor.ForeColor = System.Drawing.Color.Maroon;
            this.gpContactor.Location = new System.Drawing.Point(0, -2);
            this.gpContactor.Margin = new System.Windows.Forms.Padding(0);
            this.gpContactor.Name = "gpContactor";
            this.gpContactor.Size = new System.Drawing.Size(1088, 691);
            this.gpContactor.TabIndex = 48;
            this.gpContactor.TabStop = false;
            this.gpContactor.Text = "Contactor";
            // 
            // pb_Contactor
            // 
            this.pb_Contactor.Location = new System.Drawing.Point(560, -1);
            this.pb_Contactor.Name = "pb_Contactor";
            this.pb_Contactor.Size = new System.Drawing.Size(154, 23);
            this.pb_Contactor.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pb_Contactor.TabIndex = 61;
            this.pb_Contactor.Visible = false;
            // 
            // fLP_Buttons
            // 
            this.fLP_Buttons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_Buttons.Controls.Add(this.btnReadStatus);
            this.fLP_Buttons.Controls.Add(this.btn_ConnectContactor);
            this.fLP_Buttons.Controls.Add(this.btn_connectThroughSwitch);
            this.fLP_Buttons.Controls.Add(this.btn_DisconnectContactor);
            this.fLP_Buttons.Controls.Add(this.fLP_Params);
            this.fLP_Buttons.Location = new System.Drawing.Point(32, 17);
            this.fLP_Buttons.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.fLP_Buttons.Name = "fLP_Buttons";
            this.fLP_Buttons.Size = new System.Drawing.Size(853, 31);
            this.fLP_Buttons.TabIndex = 56;
            // 
            // btnReadStatus
            // 
            this.btnReadStatus.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnReadStatus.Location = new System.Drawing.Point(3, 3);
            this.btnReadStatus.Name = "btnReadStatus";
            this.btnReadStatus.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnReadStatus.Size = new System.Drawing.Size(100, 25);
            this.btnReadStatus.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.btnReadStatus.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Navy;
            this.btnReadStatus.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReadStatus.TabIndex = 70;
            this.btnReadStatus.Values.Image = ((System.Drawing.Image)(resources.GetObject("btnReadStatus.Values.Image")));
            this.btnReadStatus.Values.Text = "Read Status";
            this.btnReadStatus.Click += new System.EventHandler(this.btnReadStatus_Click);
            // 
            // btn_ConnectContactor
            // 
            this.btn_ConnectContactor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_ConnectContactor.Location = new System.Drawing.Point(109, 3);
            this.btn_ConnectContactor.Name = "btn_ConnectContactor";
            this.btn_ConnectContactor.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_ConnectContactor.Size = new System.Drawing.Size(125, 25);
            this.btn_ConnectContactor.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.btn_ConnectContactor.StateCommon.Content.LongText.Color1 = System.Drawing.Color.Maroon;
            this.btn_ConnectContactor.StateCommon.Content.LongText.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btn_ConnectContactor.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Navy;
            this.btn_ConnectContactor.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ConnectContactor.TabIndex = 69;
            this.btn_ConnectContactor.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_ConnectContactor.Values.Image")));
            this.btn_ConnectContactor.Values.Text = "Connect Contactor";
            this.btn_ConnectContactor.Click += new System.EventHandler(this.btn_ConnectContactor_Click);
            // 
            // btn_connectThroughSwitch
            // 
            this.btn_connectThroughSwitch.Location = new System.Drawing.Point(240, 3);
            this.btn_connectThroughSwitch.Name = "btn_connectThroughSwitch";
            this.btn_connectThroughSwitch.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_connectThroughSwitch.Size = new System.Drawing.Size(150, 25);
            this.btn_connectThroughSwitch.StateCommon.Content.LongText.Color1 = System.Drawing.Color.Maroon;
            this.btn_connectThroughSwitch.StateCommon.Content.LongText.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_connectThroughSwitch.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Navy;
            this.btn_connectThroughSwitch.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_connectThroughSwitch.TabIndex = 68;
            this.btn_connectThroughSwitch.Values.Text = "Connect through switch";
            // 
            // btn_DisconnectContactor
            // 
            this.btn_DisconnectContactor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_DisconnectContactor.Location = new System.Drawing.Point(396, 3);
            this.btn_DisconnectContactor.Name = "btn_DisconnectContactor";
            this.btn_DisconnectContactor.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_DisconnectContactor.Size = new System.Drawing.Size(150, 25);
            this.btn_DisconnectContactor.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.btn_DisconnectContactor.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Navy;
            this.btn_DisconnectContactor.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DisconnectContactor.TabIndex = 71;
            this.btn_DisconnectContactor.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_DisconnectContactor.Values.Image")));
            this.btn_DisconnectContactor.Values.Text = "Disconnect Contactor";
            // 
            // fLP_Params
            // 
            this.fLP_Params.AutoSize = true;
            this.fLP_Params.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_Params.Controls.Add(this.btn_GetContactorParams);
            this.fLP_Params.Controls.Add(this.btn_SetContactorParameters);
            this.fLP_Params.Location = new System.Drawing.Point(552, 3);
            this.fLP_Params.Name = "fLP_Params";
            this.fLP_Params.Size = new System.Drawing.Size(250, 31);
            this.fLP_Params.TabIndex = 49;
            // 
            // btn_GetContactorParams
            // 
            this.btn_GetContactorParams.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_GetContactorParams.Location = new System.Drawing.Point(3, 3);
            this.btn_GetContactorParams.Name = "btn_GetContactorParams";
            this.btn_GetContactorParams.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_GetContactorParams.Size = new System.Drawing.Size(119, 25);
            this.btn_GetContactorParams.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Navy;
            this.btn_GetContactorParams.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_GetContactorParams.TabIndex = 59;
            this.btn_GetContactorParams.Values.Text = "Get Parameters";
            // 
            // btn_SetContactorParameters
            // 
            this.btn_SetContactorParameters.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_SetContactorParameters.Location = new System.Drawing.Point(128, 3);
            this.btn_SetContactorParameters.Name = "btn_SetContactorParameters";
            this.btn_SetContactorParameters.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_SetContactorParameters.Size = new System.Drawing.Size(119, 25);
            this.btn_SetContactorParameters.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Navy;
            this.btn_SetContactorParameters.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SetContactorParameters.TabIndex = 60;
            this.btn_SetContactorParameters.Values.Text = "Set Parameters";
            // 
            // fLP_Main
            // 
            this.fLP_Main.AutoSize = true;
            this.fLP_Main.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_Main.Controls.Add(this.flowLayoutPanel1);
            this.fLP_Main.Controls.Add(this.flp_LastRow);
            this.fLP_Main.Location = new System.Drawing.Point(6, 49);
            this.fLP_Main.Name = "fLP_Main";
            this.fLP_Main.Padding = new System.Windows.Forms.Padding(3);
            this.fLP_Main.Size = new System.Drawing.Size(863, 496);
            this.fLP_Main.TabIndex = 49;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.fLP_TopRow);
            this.flowLayoutPanel1.Controls.Add(this.gp_Reconnect);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(6, 6);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(367, 484);
            this.flowLayoutPanel1.TabIndex = 68;
            // 
            // fLP_TopRow
            // 
            this.fLP_TopRow.AutoSize = true;
            this.fLP_TopRow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_TopRow.Controls.Add(this.fLP_txt_Box_Col);
            this.fLP_TopRow.Controls.Add(this.fLP_CheckBoxs);
            this.fLP_TopRow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLP_TopRow.Location = new System.Drawing.Point(3, 0);
            this.fLP_TopRow.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.fLP_TopRow.Name = "fLP_TopRow";
            this.fLP_TopRow.Size = new System.Drawing.Size(336, 177);
            this.fLP_TopRow.TabIndex = 67;
            // 
            // fLP_txt_Box_Col
            // 
            this.fLP_txt_Box_Col.AutoSize = true;
            this.fLP_txt_Box_Col.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_txt_Box_Col.Controls.Add(this.fLP_ContactorOn);
            this.fLP_txt_Box_Col.Controls.Add(this.fLP_ContactorOff);
            this.fLP_txt_Box_Col.Controls.Add(this.fLP_MinInterval_Stat);
            this.fLP_txt_Box_Col.Controls.Add(this.fLP_PUPD_Stat);
            this.fLP_txt_Box_Col.Controls.Add(this.fLP_Intr_ContFailureStat);
            this.fLP_txt_Box_Col.Controls.Add(this.lbl_ContactorStatus);
            this.fLP_txt_Box_Col.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLP_txt_Box_Col.Location = new System.Drawing.Point(3, 3);
            this.fLP_txt_Box_Col.Name = "fLP_txt_Box_Col";
            this.fLP_txt_Box_Col.Size = new System.Drawing.Size(330, 140);
            this.fLP_txt_Box_Col.TabIndex = 54;
            // 
            // fLP_ContactorOn
            // 
            this.fLP_ContactorOn.AutoSize = true;
            this.fLP_ContactorOn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_ContactorOn.Controls.Add(this.lbl_Contactor_ContactorON_PulseTime);
            this.fLP_ContactorOn.Controls.Add(this.txt_Contactor_ContactorON_PulseTime);
            this.fLP_ContactorOn.Controls.Add(this.label137);
            this.fLP_ContactorOn.Location = new System.Drawing.Point(3, 1);
            this.fLP_ContactorOn.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.fLP_ContactorOn.Name = "fLP_ContactorOn";
            this.fLP_ContactorOn.Size = new System.Drawing.Size(324, 23);
            this.fLP_ContactorOn.TabIndex = 49;
            // 
            // lbl_Contactor_ContactorON_PulseTime
            // 
            this.lbl_Contactor_ContactorON_PulseTime.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Contactor_ContactorON_PulseTime.Location = new System.Drawing.Point(3, 3);
            this.lbl_Contactor_ContactorON_PulseTime.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_Contactor_ContactorON_PulseTime.Name = "lbl_Contactor_ContactorON_PulseTime";
            this.lbl_Contactor_ContactorON_PulseTime.Size = new System.Drawing.Size(235, 15);
            this.lbl_Contactor_ContactorON_PulseTime.TabIndex = 0;
            this.lbl_Contactor_ContactorON_PulseTime.Text = "Contactor ON pulse time";
            // 
            // txt_Contactor_ContactorON_PulseTime
            // 
            this.txt_Contactor_ContactorON_PulseTime.Location = new System.Drawing.Point(244, 0);
            this.txt_Contactor_ContactorON_PulseTime.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.txt_Contactor_ContactorON_PulseTime.Name = "txt_Contactor_ContactorON_PulseTime";
            this.txt_Contactor_ContactorON_PulseTime.Size = new System.Drawing.Size(48, 23);
            this.txt_Contactor_ContactorON_PulseTime.TabIndex = 1;
            this.txt_Contactor_ContactorON_PulseTime.Text = "0";
            this.txt_Contactor_ContactorON_PulseTime.Leave += new System.EventHandler(this.txt_Contactor_ContactorON_PulseTime_Leave);
            // 
            // label137
            // 
            this.label137.AutoSize = true;
            this.label137.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label137.ForeColor = System.Drawing.Color.Navy;
            this.label137.Location = new System.Drawing.Point(298, 3);
            this.label137.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label137.Name = "label137";
            this.label137.Size = new System.Drawing.Size(23, 15);
            this.label137.TabIndex = 0;
            this.label137.Text = "ms";
            // 
            // fLP_ContactorOff
            // 
            this.fLP_ContactorOff.AutoSize = true;
            this.fLP_ContactorOff.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_ContactorOff.Controls.Add(this.lbl_Contactor_ContactorOFF_PulseTime);
            this.fLP_ContactorOff.Controls.Add(this.txt_Contactor_ContactorOFF_PulseTime);
            this.fLP_ContactorOff.Controls.Add(this.label138);
            this.fLP_ContactorOff.Location = new System.Drawing.Point(3, 26);
            this.fLP_ContactorOff.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.fLP_ContactorOff.Name = "fLP_ContactorOff";
            this.fLP_ContactorOff.Size = new System.Drawing.Size(324, 23);
            this.fLP_ContactorOff.TabIndex = 50;
            // 
            // lbl_Contactor_ContactorOFF_PulseTime
            // 
            this.lbl_Contactor_ContactorOFF_PulseTime.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Contactor_ContactorOFF_PulseTime.Location = new System.Drawing.Point(3, 3);
            this.lbl_Contactor_ContactorOFF_PulseTime.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_Contactor_ContactorOFF_PulseTime.Name = "lbl_Contactor_ContactorOFF_PulseTime";
            this.lbl_Contactor_ContactorOFF_PulseTime.Size = new System.Drawing.Size(235, 15);
            this.lbl_Contactor_ContactorOFF_PulseTime.TabIndex = 0;
            this.lbl_Contactor_ContactorOFF_PulseTime.Text = "Contactor OFF pulse time";
            // 
            // txt_Contactor_ContactorOFF_PulseTime
            // 
            this.txt_Contactor_ContactorOFF_PulseTime.Location = new System.Drawing.Point(244, 0);
            this.txt_Contactor_ContactorOFF_PulseTime.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.txt_Contactor_ContactorOFF_PulseTime.Name = "txt_Contactor_ContactorOFF_PulseTime";
            this.txt_Contactor_ContactorOFF_PulseTime.Size = new System.Drawing.Size(48, 23);
            this.txt_Contactor_ContactorOFF_PulseTime.TabIndex = 2;
            this.txt_Contactor_ContactorOFF_PulseTime.Text = "0";
            this.txt_Contactor_ContactorOFF_PulseTime.Leave += new System.EventHandler(this.txt_Contactor_ContactorOFF_PulseTime_Leave);
            // 
            // label138
            // 
            this.label138.AutoSize = true;
            this.label138.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label138.ForeColor = System.Drawing.Color.Navy;
            this.label138.Location = new System.Drawing.Point(298, 3);
            this.label138.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label138.Name = "label138";
            this.label138.Size = new System.Drawing.Size(23, 15);
            this.label138.TabIndex = 0;
            this.label138.Text = "ms";
            // 
            // fLP_MinInterval_Stat
            // 
            this.fLP_MinInterval_Stat.AutoSize = true;
            this.fLP_MinInterval_Stat.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_MinInterval_Stat.Controls.Add(this.lbl_Contactor_MinIntervalBwContactorStateChange);
            this.fLP_MinInterval_Stat.Controls.Add(this.txt_Contactor_MinIntervalBwContactorStateChange);
            this.fLP_MinInterval_Stat.Controls.Add(this.label139);
            this.fLP_MinInterval_Stat.Location = new System.Drawing.Point(3, 51);
            this.fLP_MinInterval_Stat.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.fLP_MinInterval_Stat.Name = "fLP_MinInterval_Stat";
            this.fLP_MinInterval_Stat.Size = new System.Drawing.Size(313, 23);
            this.fLP_MinInterval_Stat.TabIndex = 51;
            // 
            // lbl_Contactor_MinIntervalBwContactorStateChange
            // 
            this.lbl_Contactor_MinIntervalBwContactorStateChange.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Contactor_MinIntervalBwContactorStateChange.Location = new System.Drawing.Point(3, 3);
            this.lbl_Contactor_MinIntervalBwContactorStateChange.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_Contactor_MinIntervalBwContactorStateChange.Name = "lbl_Contactor_MinIntervalBwContactorStateChange";
            this.lbl_Contactor_MinIntervalBwContactorStateChange.Size = new System.Drawing.Size(235, 15);
            this.lbl_Contactor_MinIntervalBwContactorStateChange.TabIndex = 0;
            this.lbl_Contactor_MinIntervalBwContactorStateChange.Text = "Min Interval B/w Contactor state change";
            // 
            // txt_Contactor_MinIntervalBwContactorStateChange
            // 
            this.txt_Contactor_MinIntervalBwContactorStateChange.Location = new System.Drawing.Point(244, 0);
            this.txt_Contactor_MinIntervalBwContactorStateChange.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.txt_Contactor_MinIntervalBwContactorStateChange.Name = "txt_Contactor_MinIntervalBwContactorStateChange";
            this.txt_Contactor_MinIntervalBwContactorStateChange.Size = new System.Drawing.Size(48, 23);
            this.txt_Contactor_MinIntervalBwContactorStateChange.TabIndex = 3;
            this.txt_Contactor_MinIntervalBwContactorStateChange.Text = "0";
            this.txt_Contactor_MinIntervalBwContactorStateChange.Leave += new System.EventHandler(this.txt_Contactor_MinIntervalBwContactorStateChange_Leave);
            // 
            // label139
            // 
            this.label139.AutoSize = true;
            this.label139.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label139.ForeColor = System.Drawing.Color.Navy;
            this.label139.Location = new System.Drawing.Point(298, 3);
            this.label139.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label139.Name = "label139";
            this.label139.Size = new System.Drawing.Size(12, 15);
            this.label139.TabIndex = 0;
            this.label139.Text = "s";
            // 
            // fLP_PUPD_Stat
            // 
            this.fLP_PUPD_Stat.AutoSize = true;
            this.fLP_PUPD_Stat.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_PUPD_Stat.Controls.Add(this.lbl_Contactor_PowerUpDelayToChangeState);
            this.fLP_PUPD_Stat.Controls.Add(this.txt_Contactor_PowerUpDelayToChangeState);
            this.fLP_PUPD_Stat.Controls.Add(this.label140);
            this.fLP_PUPD_Stat.Location = new System.Drawing.Point(3, 76);
            this.fLP_PUPD_Stat.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.fLP_PUPD_Stat.Name = "fLP_PUPD_Stat";
            this.fLP_PUPD_Stat.Size = new System.Drawing.Size(313, 23);
            this.fLP_PUPD_Stat.TabIndex = 52;
            // 
            // lbl_Contactor_PowerUpDelayToChangeState
            // 
            this.lbl_Contactor_PowerUpDelayToChangeState.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Contactor_PowerUpDelayToChangeState.Location = new System.Drawing.Point(3, 3);
            this.lbl_Contactor_PowerUpDelayToChangeState.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_Contactor_PowerUpDelayToChangeState.Name = "lbl_Contactor_PowerUpDelayToChangeState";
            this.lbl_Contactor_PowerUpDelayToChangeState.Size = new System.Drawing.Size(235, 15);
            this.lbl_Contactor_PowerUpDelayToChangeState.TabIndex = 0;
            this.lbl_Contactor_PowerUpDelayToChangeState.Text = "Power up delay to change state";
            // 
            // txt_Contactor_PowerUpDelayToChangeState
            // 
            this.txt_Contactor_PowerUpDelayToChangeState.Location = new System.Drawing.Point(244, 0);
            this.txt_Contactor_PowerUpDelayToChangeState.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.txt_Contactor_PowerUpDelayToChangeState.Name = "txt_Contactor_PowerUpDelayToChangeState";
            this.txt_Contactor_PowerUpDelayToChangeState.Size = new System.Drawing.Size(48, 23);
            this.txt_Contactor_PowerUpDelayToChangeState.TabIndex = 4;
            this.txt_Contactor_PowerUpDelayToChangeState.Text = "0";
            this.txt_Contactor_PowerUpDelayToChangeState.Leave += new System.EventHandler(this.txt_Contactor_PowerUpDelayToChangeState_Leave);
            // 
            // label140
            // 
            this.label140.AutoSize = true;
            this.label140.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label140.ForeColor = System.Drawing.Color.Navy;
            this.label140.Location = new System.Drawing.Point(298, 3);
            this.label140.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label140.Name = "label140";
            this.label140.Size = new System.Drawing.Size(12, 15);
            this.label140.TabIndex = 0;
            this.label140.Text = "s";
            // 
            // fLP_Intr_ContFailureStat
            // 
            this.fLP_Intr_ContFailureStat.AutoSize = true;
            this.fLP_Intr_ContFailureStat.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_Intr_ContFailureStat.Controls.Add(this.lbl_Interval_Contactor_Failure_Status);
            this.fLP_Intr_ContFailureStat.Controls.Add(this.txt_Contactor_Failure_Status);
            this.fLP_Intr_ContFailureStat.Controls.Add(this.label1);
            this.fLP_Intr_ContFailureStat.Location = new System.Drawing.Point(3, 101);
            this.fLP_Intr_ContFailureStat.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.fLP_Intr_ContFailureStat.Name = "fLP_Intr_ContFailureStat";
            this.fLP_Intr_ContFailureStat.Size = new System.Drawing.Size(313, 23);
            this.fLP_Intr_ContFailureStat.TabIndex = 53;
            // 
            // lbl_Interval_Contactor_Failure_Status
            // 
            this.lbl_Interval_Contactor_Failure_Status.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Interval_Contactor_Failure_Status.Location = new System.Drawing.Point(3, 0);
            this.lbl_Interval_Contactor_Failure_Status.Name = "lbl_Interval_Contactor_Failure_Status";
            this.lbl_Interval_Contactor_Failure_Status.Size = new System.Drawing.Size(235, 15);
            this.lbl_Interval_Contactor_Failure_Status.TabIndex = 68;
            this.lbl_Interval_Contactor_Failure_Status.Text = "Interval to Contactor Failure Status";
            // 
            // txt_Contactor_Failure_Status
            // 
            this.txt_Contactor_Failure_Status.Location = new System.Drawing.Point(244, 0);
            this.txt_Contactor_Failure_Status.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.txt_Contactor_Failure_Status.Name = "txt_Contactor_Failure_Status";
            this.txt_Contactor_Failure_Status.Size = new System.Drawing.Size(48, 23);
            this.txt_Contactor_Failure_Status.TabIndex = 70;
            this.txt_Contactor_Failure_Status.Text = "0";
            this.txt_Contactor_Failure_Status.Leave += new System.EventHandler(this.txt_Contactor_Failure_Status_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(298, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 15);
            this.label1.TabIndex = 69;
            this.label1.Text = "s";
            // 
            // lbl_ContactorStatus
            // 
            this.lbl_ContactorStatus.AutoSize = true;
            this.lbl_ContactorStatus.BackColor = System.Drawing.Color.Transparent;
            this.lbl_ContactorStatus.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lbl_ContactorStatus.ForeColor = System.Drawing.Color.Navy;
            this.lbl_ContactorStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_ContactorStatus.Location = new System.Drawing.Point(6, 125);
            this.lbl_ContactorStatus.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.lbl_ContactorStatus.Name = "lbl_ContactorStatus";
            this.lbl_ContactorStatus.Size = new System.Drawing.Size(101, 15);
            this.lbl_ContactorStatus.TabIndex = 67;
            this.lbl_ContactorStatus.Text = "Status(Unkonwn)";
            this.lbl_ContactorStatus.Visible = false;
            // 
            // fLP_CheckBoxs
            // 
            this.fLP_CheckBoxs.AutoSize = true;
            this.fLP_CheckBoxs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_CheckBoxs.Controls.Add(this.check_Contactor_offOptically);
            this.fLP_CheckBoxs.Controls.Add(this.check_Contactor_onOptically);
            this.fLP_CheckBoxs.Location = new System.Drawing.Point(5, 149);
            this.fLP_CheckBoxs.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.fLP_CheckBoxs.Name = "fLP_CheckBoxs";
            this.fLP_CheckBoxs.Size = new System.Drawing.Size(268, 25);
            this.fLP_CheckBoxs.TabIndex = 55;
            // 
            // check_Contactor_offOptically
            // 
            this.check_Contactor_offOptically.AutoSize = true;
            this.check_Contactor_offOptically.ForeColor = System.Drawing.Color.Navy;
            this.check_Contactor_offOptically.Location = new System.Drawing.Point(3, 3);
            this.check_Contactor_offOptically.Name = "check_Contactor_offOptically";
            this.check_Contactor_offOptically.Size = new System.Drawing.Size(135, 19);
            this.check_Contactor_offOptically.TabIndex = 63;
            this.check_Contactor_offOptically.Text = "Optically Disconnect";
            this.check_Contactor_offOptically.UseVisualStyleBackColor = true;
            this.check_Contactor_offOptically.CheckedChanged += new System.EventHandler(this.check_Contactor_offOptically_CheckedChanged);
            // 
            // check_Contactor_onOptically
            // 
            this.check_Contactor_onOptically.ForeColor = System.Drawing.Color.Navy;
            this.check_Contactor_onOptically.Location = new System.Drawing.Point(144, 3);
            this.check_Contactor_onOptically.Name = "check_Contactor_onOptically";
            this.check_Contactor_onOptically.Size = new System.Drawing.Size(121, 19);
            this.check_Contactor_onOptically.TabIndex = 62;
            this.check_Contactor_onOptically.Text = "Optically Connect";
            this.check_Contactor_onOptically.UseVisualStyleBackColor = true;
            this.check_Contactor_onOptically.CheckedChanged += new System.EventHandler(this.check_Contactor_onOptically_CheckedChanged);
            // 
            // gp_Reconnect
            // 
            this.gp_Reconnect.AutoSize = true;
            this.gp_Reconnect.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gp_Reconnect.Controls.Add(this.fLP_Reconnect);
            this.gp_Reconnect.Location = new System.Drawing.Point(3, 177);
            this.gp_Reconnect.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.gp_Reconnect.Name = "gp_Reconnect";
            this.gp_Reconnect.Size = new System.Drawing.Size(233, 246);
            this.gp_Reconnect.TabIndex = 64;
            this.gp_Reconnect.TabStop = false;
            this.gp_Reconnect.Text = "Reconnect";
            // 
            // fLP_Reconnect
            // 
            this.fLP_Reconnect.AutoSize = true;
            this.fLP_Reconnect.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_Reconnect.Controls.Add(this.check_Contactor_ReconnectonTariffChange);
            this.fLP_Reconnect.Controls.Add(this.gp_Reconn_Retry);
            this.fLP_Reconnect.Controls.Add(this.gp_RetryExpire);
            this.fLP_Reconnect.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLP_Reconnect.Location = new System.Drawing.Point(3, 19);
            this.fLP_Reconnect.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.fLP_Reconnect.Name = "fLP_Reconnect";
            this.fLP_Reconnect.Size = new System.Drawing.Size(227, 208);
            this.fLP_Reconnect.TabIndex = 49;
            // 
            // check_Contactor_ReconnectonTariffChange
            // 
            this.check_Contactor_ReconnectonTariffChange.AutoSize = true;
            this.check_Contactor_ReconnectonTariffChange.ForeColor = System.Drawing.Color.Navy;
            this.check_Contactor_ReconnectonTariffChange.Location = new System.Drawing.Point(3, 3);
            this.check_Contactor_ReconnectonTariffChange.Name = "check_Contactor_ReconnectonTariffChange";
            this.check_Contactor_ReconnectonTariffChange.Size = new System.Drawing.Size(94, 19);
            this.check_Contactor_ReconnectonTariffChange.TabIndex = 46;
            this.check_Contactor_ReconnectonTariffChange.Text = "Tariff change";
            this.check_Contactor_ReconnectonTariffChange.UseVisualStyleBackColor = true;
            this.check_Contactor_ReconnectonTariffChange.CheckedChanged += new System.EventHandler(this.check_Contactor_ReconnectonTariffChange_CheckedChanged);
            // 
            // gp_Reconn_Retry
            // 
            this.gp_Reconn_Retry.AutoSize = true;
            this.gp_Reconn_Retry.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gp_Reconn_Retry.Controls.Add(this.fLP_Retry);
            this.gp_Reconn_Retry.Location = new System.Drawing.Point(3, 25);
            this.gp_Reconn_Retry.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.gp_Reconn_Retry.Name = "gp_Reconn_Retry";
            this.gp_Reconn_Retry.Size = new System.Drawing.Size(184, 90);
            this.gp_Reconn_Retry.TabIndex = 47;
            this.gp_Reconn_Retry.TabStop = false;
            this.gp_Reconn_Retry.Text = "Retry";
            // 
            // fLP_Retry
            // 
            this.fLP_Retry.AutoSize = true;
            this.fLP_Retry.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_Retry.Controls.Add(this.fLP_Retry_Radio);
            this.fLP_Retry.Controls.Add(this.fLP_Retry_AutoIntr);
            this.fLP_Retry.Controls.Add(this.fLP_Retry_Count);
            this.fLP_Retry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fLP_Retry.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLP_Retry.Location = new System.Drawing.Point(3, 19);
            this.fLP_Retry.Name = "fLP_Retry";
            this.fLP_Retry.Size = new System.Drawing.Size(178, 68);
            this.fLP_Retry.TabIndex = 52;
            // 
            // fLP_Retry_Radio
            // 
            this.fLP_Retry_Radio.AutoSize = true;
            this.fLP_Retry_Radio.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_Retry_Radio.Controls.Add(this.radio_contactor_auto);
            this.fLP_Retry_Radio.Controls.Add(this.radio_contactor_switch);
            this.fLP_Retry_Radio.Location = new System.Drawing.Point(3, 0);
            this.fLP_Retry_Radio.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.fLP_Retry_Radio.Name = "fLP_Retry_Radio";
            this.fLP_Retry_Radio.Size = new System.Drawing.Size(172, 19);
            this.fLP_Retry_Radio.TabIndex = 49;
            // 
            // radio_contactor_auto
            // 
            this.radio_contactor_auto.AutoSize = true;
            this.radio_contactor_auto.Checked = true;
            this.radio_contactor_auto.ForeColor = System.Drawing.Color.Navy;
            this.radio_contactor_auto.Location = new System.Drawing.Point(3, 0);
            this.radio_contactor_auto.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.radio_contactor_auto.Name = "radio_contactor_auto";
            this.radio_contactor_auto.Size = new System.Drawing.Size(82, 19);
            this.radio_contactor_auto.TabIndex = 0;
            this.radio_contactor_auto.TabStop = true;
            this.radio_contactor_auto.Text = "Automatic";
            this.radio_contactor_auto.UseVisualStyleBackColor = true;
            this.radio_contactor_auto.CheckedChanged += new System.EventHandler(this.radio_contactor_auto_CheckedChanged);
            // 
            // radio_contactor_switch
            // 
            this.radio_contactor_switch.AutoSize = true;
            this.radio_contactor_switch.ForeColor = System.Drawing.Color.Navy;
            this.radio_contactor_switch.Location = new System.Drawing.Point(108, 0);
            this.radio_contactor_switch.Margin = new System.Windows.Forms.Padding(20, 0, 3, 0);
            this.radio_contactor_switch.Name = "radio_contactor_switch";
            this.radio_contactor_switch.Size = new System.Drawing.Size(61, 19);
            this.radio_contactor_switch.TabIndex = 0;
            this.radio_contactor_switch.TabStop = true;
            this.radio_contactor_switch.Text = "Switch";
            this.radio_contactor_switch.UseVisualStyleBackColor = true;
            // 
            // fLP_Retry_AutoIntr
            // 
            this.fLP_Retry_AutoIntr.AutoSize = true;
            this.fLP_Retry_AutoIntr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_Retry_AutoIntr.Controls.Add(this.label175);
            this.fLP_Retry_AutoIntr.Controls.Add(this.txt_Contactor_IntervalBWEntries);
            this.fLP_Retry_AutoIntr.Controls.Add(this.lbl_AutoInterval);
            this.fLP_Retry_AutoIntr.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.fLP_Retry_AutoIntr.Location = new System.Drawing.Point(3, 19);
            this.fLP_Retry_AutoIntr.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.fLP_Retry_AutoIntr.Name = "fLP_Retry_AutoIntr";
            this.fLP_Retry_AutoIntr.Size = new System.Drawing.Size(168, 26);
            this.fLP_Retry_AutoIntr.TabIndex = 51;
            // 
            // label175
            // 
            this.label175.AutoSize = true;
            this.label175.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label175.ForeColor = System.Drawing.Color.Navy;
            this.label175.Location = new System.Drawing.Point(156, 0);
            this.label175.Margin = new System.Windows.Forms.Padding(0);
            this.label175.Name = "label175";
            this.label175.Size = new System.Drawing.Size(12, 15);
            this.label175.TabIndex = 0;
            this.label175.Text = "s";
            // 
            // txt_Contactor_IntervalBWEntries
            // 
            this.txt_Contactor_IntervalBWEntries.Location = new System.Drawing.Point(105, 0);
            this.txt_Contactor_IntervalBWEntries.Margin = new System.Windows.Forms.Padding(20, 0, 3, 3);
            this.txt_Contactor_IntervalBWEntries.Name = "txt_Contactor_IntervalBWEntries";
            this.txt_Contactor_IntervalBWEntries.Size = new System.Drawing.Size(48, 23);
            this.txt_Contactor_IntervalBWEntries.TabIndex = 5;
            this.txt_Contactor_IntervalBWEntries.Text = "0";
            this.txt_Contactor_IntervalBWEntries.Leave += new System.EventHandler(this.txt_Contactor_IntervalBWEntries_Leave);
            // 
            // lbl_AutoInterval
            // 
            this.lbl_AutoInterval.ForeColor = System.Drawing.Color.Navy;
            this.lbl_AutoInterval.Location = new System.Drawing.Point(3, 3);
            this.lbl_AutoInterval.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_AutoInterval.Name = "lbl_AutoInterval";
            this.lbl_AutoInterval.Size = new System.Drawing.Size(79, 15);
            this.lbl_AutoInterval.TabIndex = 0;
            this.lbl_AutoInterval.Text = "Auto Interval";
            // 
            // fLP_Retry_Count
            // 
            this.fLP_Retry_Count.AutoSize = true;
            this.fLP_Retry_Count.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_Retry_Count.Controls.Add(this.lbl_RetryCount);
            this.fLP_Retry_Count.Controls.Add(this.txt_Contactor_RetryCount);
            this.fLP_Retry_Count.Location = new System.Drawing.Point(3, 45);
            this.fLP_Retry_Count.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.fLP_Retry_Count.Name = "fLP_Retry_Count";
            this.fLP_Retry_Count.Size = new System.Drawing.Size(156, 23);
            this.fLP_Retry_Count.TabIndex = 50;
            // 
            // lbl_RetryCount
            // 
            this.lbl_RetryCount.ForeColor = System.Drawing.Color.Navy;
            this.lbl_RetryCount.Location = new System.Drawing.Point(3, 3);
            this.lbl_RetryCount.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_RetryCount.Name = "lbl_RetryCount";
            this.lbl_RetryCount.Size = new System.Drawing.Size(40, 15);
            this.lbl_RetryCount.TabIndex = 0;
            this.lbl_RetryCount.Text = "Count";
            // 
            // txt_Contactor_RetryCount
            // 
            this.txt_Contactor_RetryCount.Location = new System.Drawing.Point(105, 0);
            this.txt_Contactor_RetryCount.Margin = new System.Windows.Forms.Padding(59, 0, 3, 0);
            this.txt_Contactor_RetryCount.Name = "txt_Contactor_RetryCount";
            this.txt_Contactor_RetryCount.Size = new System.Drawing.Size(48, 23);
            this.txt_Contactor_RetryCount.TabIndex = 4;
            this.txt_Contactor_RetryCount.Text = "0";
            this.txt_Contactor_RetryCount.Leave += new System.EventHandler(this.txt_Contactor_RetryCount_Leave);
            // 
            // gp_RetryExpire
            // 
            this.gp_RetryExpire.AutoSize = true;
            this.gp_RetryExpire.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gp_RetryExpire.Controls.Add(this.fLP_RetryExpire);
            this.gp_RetryExpire.Location = new System.Drawing.Point(3, 115);
            this.gp_RetryExpire.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.gp_RetryExpire.Name = "gp_RetryExpire";
            this.gp_RetryExpire.Size = new System.Drawing.Size(224, 93);
            this.gp_RetryExpire.TabIndex = 47;
            this.gp_RetryExpire.TabStop = false;
            this.gp_RetryExpire.Text = "On Retry Expire";
            // 
            // fLP_RetryExpire
            // 
            this.fLP_RetryExpire.AutoSize = true;
            this.fLP_RetryExpire.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_RetryExpire.Controls.Add(this.fLP_RtryExp);
            this.fLP_RetryExpire.Controls.Add(this.fLP_RetryExpire_AutoInterval);
            this.fLP_RetryExpire.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fLP_RetryExpire.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLP_RetryExpire.Location = new System.Drawing.Point(3, 19);
            this.fLP_RetryExpire.Name = "fLP_RetryExpire";
            this.fLP_RetryExpire.Size = new System.Drawing.Size(218, 71);
            this.fLP_RetryExpire.TabIndex = 51;
            // 
            // fLP_RtryExp
            // 
            this.fLP_RtryExp.AutoSize = true;
            this.fLP_RtryExp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_RtryExp.Controls.Add(this.check_contactor_reconnectAuto);
            this.fLP_RtryExp.Controls.Add(this.check_contactor_reconnectSwitch);
            this.fLP_RtryExp.Location = new System.Drawing.Point(3, 0);
            this.fLP_RtryExp.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.fLP_RtryExp.Name = "fLP_RtryExp";
            this.fLP_RtryExp.Size = new System.Drawing.Size(174, 19);
            this.fLP_RtryExp.TabIndex = 49;
            // 
            // check_contactor_reconnectAuto
            // 
            this.check_contactor_reconnectAuto.AutoSize = true;
            this.check_contactor_reconnectAuto.Checked = true;
            this.check_contactor_reconnectAuto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_contactor_reconnectAuto.ForeColor = System.Drawing.Color.Navy;
            this.check_contactor_reconnectAuto.Location = new System.Drawing.Point(3, 0);
            this.check_contactor_reconnectAuto.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.check_contactor_reconnectAuto.Name = "check_contactor_reconnectAuto";
            this.check_contactor_reconnectAuto.Size = new System.Drawing.Size(83, 19);
            this.check_contactor_reconnectAuto.TabIndex = 46;
            this.check_contactor_reconnectAuto.Text = "Automatic";
            this.check_contactor_reconnectAuto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.check_contactor_reconnectAuto.UseVisualStyleBackColor = true;
            this.check_contactor_reconnectAuto.CheckedChanged += new System.EventHandler(this.check_contactor_reconnectAuto_CheckedChanged);
            // 
            // check_contactor_reconnectSwitch
            // 
            this.check_contactor_reconnectSwitch.AutoSize = true;
            this.check_contactor_reconnectSwitch.Checked = true;
            this.check_contactor_reconnectSwitch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_contactor_reconnectSwitch.ForeColor = System.Drawing.Color.Navy;
            this.check_contactor_reconnectSwitch.Location = new System.Drawing.Point(109, 0);
            this.check_contactor_reconnectSwitch.Margin = new System.Windows.Forms.Padding(20, 0, 3, 0);
            this.check_contactor_reconnectSwitch.Name = "check_contactor_reconnectSwitch";
            this.check_contactor_reconnectSwitch.Size = new System.Drawing.Size(62, 19);
            this.check_contactor_reconnectSwitch.TabIndex = 46;
            this.check_contactor_reconnectSwitch.Text = "Switch";
            this.check_contactor_reconnectSwitch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.check_contactor_reconnectSwitch.UseVisualStyleBackColor = true;
            this.check_contactor_reconnectSwitch.CheckedChanged += new System.EventHandler(this.check_contactor_reconnectSwitch_CheckedChanged);
            // 
            // fLP_RetryExpire_AutoInterval
            // 
            this.fLP_RetryExpire_AutoInterval.AutoSize = true;
            this.fLP_RetryExpire_AutoInterval.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_RetryExpire_AutoInterval.Controls.Add(this.lbl_OnRetry_AutoInterval);
            this.fLP_RetryExpire_AutoInterval.Controls.Add(this.txt_Contactor_ControlMode);
            this.fLP_RetryExpire_AutoInterval.Controls.Add(this.label178);
            this.fLP_RetryExpire_AutoInterval.Location = new System.Drawing.Point(3, 25);
            this.fLP_RetryExpire_AutoInterval.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.fLP_RetryExpire_AutoInterval.Name = "fLP_RetryExpire_AutoInterval";
            this.fLP_RetryExpire_AutoInterval.Size = new System.Drawing.Size(212, 26);
            this.fLP_RetryExpire_AutoInterval.TabIndex = 50;
            // 
            // lbl_OnRetry_AutoInterval
            // 
            this.lbl_OnRetry_AutoInterval.AutoSize = true;
            this.lbl_OnRetry_AutoInterval.ForeColor = System.Drawing.Color.Navy;
            this.lbl_OnRetry_AutoInterval.Location = new System.Drawing.Point(3, 3);
            this.lbl_OnRetry_AutoInterval.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_OnRetry_AutoInterval.Name = "lbl_OnRetry_AutoInterval";
            this.lbl_OnRetry_AutoInterval.Size = new System.Drawing.Size(79, 15);
            this.lbl_OnRetry_AutoInterval.TabIndex = 0;
            this.lbl_OnRetry_AutoInterval.Text = "Auto Interval";
            // 
            // txt_Contactor_ControlMode
            // 
            this.txt_Contactor_ControlMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txt_Contactor_ControlMode.FormattingEnabled = true;
            this.txt_Contactor_ControlMode.Items.AddRange(new object[] {
            "5",
            "5",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50",
            "55",
            "60",
            "65",
            "70",
            "75",
            "80",
            "85",
            "90",
            "95",
            "100",
            "105",
            "110",
            "115",
            "120",
            "125",
            "130",
            "135",
            "140",
            "145",
            "150",
            "155",
            "160",
            "165",
            "170",
            "175",
            "180",
            "185",
            "190",
            "195",
            "200",
            "205",
            "210",
            "215",
            "220",
            "225",
            "230",
            "235",
            "240",
            "245",
            "250",
            "255",
            "260",
            "265",
            "270",
            "275",
            "280",
            "285",
            "290",
            "295",
            "300",
            "305",
            "310",
            "315",
            "320",
            "325",
            "330",
            "335",
            "340",
            "345",
            "350",
            "355",
            "360",
            "365",
            "370",
            "375",
            "380",
            "385",
            "390",
            "395",
            "400",
            "405",
            "410",
            "415",
            "420",
            "425",
            "430",
            "435",
            "440",
            "445",
            "450",
            "455",
            "460",
            "465",
            "470",
            "475",
            "480",
            "485",
            "490",
            "495",
            "500",
            "505",
            "510",
            "515",
            "520",
            "525",
            "530",
            "535",
            "540",
            "545",
            "550",
            "555",
            "560",
            "565",
            "570",
            "575",
            "580",
            "585",
            "590",
            "595",
            "600",
            "605",
            "610",
            "615",
            "620",
            "625",
            "630",
            "635",
            "640",
            "645",
            "650",
            "655",
            "660",
            "665",
            "670",
            "675",
            "680",
            "685",
            "690",
            "695",
            "700",
            "705",
            "710",
            "715",
            "720",
            "725",
            "730",
            "735",
            "740",
            "745",
            "750",
            "755",
            "760",
            "765",
            "770",
            "775",
            "780",
            "785",
            "790",
            "795",
            "800",
            "805",
            "810",
            "815",
            "820",
            "825",
            "830",
            "835",
            "840",
            "845",
            "850",
            "855",
            "860",
            "865",
            "870",
            "875",
            "880",
            "885",
            "890",
            "895",
            "900",
            "905",
            "910",
            "915",
            "920",
            "925",
            "930",
            "935",
            "940",
            "945",
            "950",
            "955",
            "960",
            "965",
            "970",
            "975",
            "980",
            "985",
            "990",
            "995",
            "1000",
            "1005",
            "1010",
            "1015",
            "1020",
            "1025",
            "1030",
            "1035",
            "1040",
            "1045",
            "1050",
            "1055",
            "1060",
            "1065",
            "1070",
            "1075",
            "1080",
            "1085",
            "1090",
            "1095",
            "1100",
            "1105",
            "1110",
            "1115",
            "1120",
            "1125",
            "1130",
            "1135",
            "1140",
            "1145",
            "1150",
            "1155",
            "1160",
            "1165",
            "1170",
            "1175",
            "1180",
            "1185",
            "1190",
            "1195",
            "1200",
            "1205",
            "1210",
            "1215",
            "1220",
            "1225",
            "1230",
            "1235",
            "1240",
            "1245",
            "1250",
            "1255",
            "1260",
            "1265",
            "1270",
            "1275"});
            this.txt_Contactor_ControlMode.Location = new System.Drawing.Point(105, 0);
            this.txt_Contactor_ControlMode.Margin = new System.Windows.Forms.Padding(20, 0, 3, 3);
            this.txt_Contactor_ControlMode.Name = "txt_Contactor_ControlMode";
            this.txt_Contactor_ControlMode.Size = new System.Drawing.Size(76, 23);
            this.txt_Contactor_ControlMode.TabIndex = 48;
            this.txt_Contactor_ControlMode.SelectedIndexChanged += new System.EventHandler(this.txt_Contactor_ControlMode_SelectedIndexChanged);
            // 
            // label178
            // 
            this.label178.AutoSize = true;
            this.label178.ForeColor = System.Drawing.Color.Navy;
            this.label178.Location = new System.Drawing.Point(184, 0);
            this.label178.Margin = new System.Windows.Forms.Padding(0);
            this.label178.Name = "label178";
            this.label178.Size = new System.Drawing.Size(28, 15);
            this.label178.TabIndex = 47;
            this.label178.Text = "min";
            // 
            // flp_LastRow
            // 
            this.flp_LastRow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flp_LastRow.Controls.Add(this.gp_OverloadLimitControl);
            this.flp_LastRow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flp_LastRow.Location = new System.Drawing.Point(379, 3);
            this.flp_LastRow.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.flp_LastRow.Name = "flp_LastRow";
            this.flp_LastRow.Size = new System.Drawing.Size(478, 288);
            this.flp_LastRow.TabIndex = 49;
            // 
            // gp_OverloadLimitControl
            // 
            this.gp_OverloadLimitControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gp_OverloadLimitControl.Controls.Add(this.gp_turn_off_con);
            this.gp_OverloadLimitControl.Controls.Add(this.fLP_OverLoad);
            this.gp_OverloadLimitControl.Location = new System.Drawing.Point(3, 0);
            this.gp_OverloadLimitControl.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.gp_OverloadLimitControl.Name = "gp_OverloadLimitControl";
            this.gp_OverloadLimitControl.Size = new System.Drawing.Size(465, 284);
            this.gp_OverloadLimitControl.TabIndex = 65;
            this.gp_OverloadLimitControl.TabStop = false;
            this.gp_OverloadLimitControl.Text = "Limits overload";
            // 
            // gp_turn_off_con
            // 
            this.gp_turn_off_con.AutoSize = true;
            this.gp_turn_off_con.Controls.Add(this.flowLayoutPanel7);
            this.gp_turn_off_con.Location = new System.Drawing.Point(6, 100);
            this.gp_turn_off_con.Name = "gp_turn_off_con";
            this.gp_turn_off_con.Size = new System.Drawing.Size(447, 175);
            this.gp_turn_off_con.TabIndex = 53;
            this.gp_turn_off_con.TabStop = false;
            this.gp_turn_off_con.Text = "Turn Contactor Off in case (TCO)";
            // 
            // flowLayoutPanel7
            // 
            this.flowLayoutPanel7.AutoSize = true;
            this.flowLayoutPanel7.Controls.Add(this.flp_TCO_Limits);
            this.flowLayoutPanel7.Controls.Add(this.ftp_TCO_Events);
            this.flowLayoutPanel7.Location = new System.Drawing.Point(6, 22);
            this.flowLayoutPanel7.Name = "flowLayoutPanel7";
            this.flowLayoutPanel7.Size = new System.Drawing.Size(435, 131);
            this.flowLayoutPanel7.TabIndex = 52;
            // 
            // flp_TCO_Limits
            // 
            this.flp_TCO_Limits.AutoSize = true;
            this.flp_TCO_Limits.Controls.Add(this.flp_TCO_OverLoadTotal);
            this.flp_TCO_Limits.Controls.Add(this.flp_TCO_OverCurrentByPhase);
            this.flp_TCO_Limits.Controls.Add(this.flp_TCO_OverLoadByPhase);
            this.flp_TCO_Limits.Controls.Add(this.flp_TCO_MdiOverLoad);
            this.flp_TCO_Limits.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flp_TCO_Limits.Location = new System.Drawing.Point(3, 3);
            this.flp_TCO_Limits.Name = "flp_TCO_Limits";
            this.flp_TCO_Limits.Size = new System.Drawing.Size(332, 124);
            this.flp_TCO_Limits.TabIndex = 53;
            // 
            // flp_TCO_OverLoadTotal
            // 
            this.flp_TCO_OverLoadTotal.AutoSize = true;
            this.flp_TCO_OverLoadTotal.Controls.Add(this.lbl_Cont_OverLoadTotal);
            this.flp_TCO_OverLoadTotal.Controls.Add(this.check_Contactor_OverLoadTotal_T1);
            this.flp_TCO_OverLoadTotal.Controls.Add(this.check_Contactor_OverLoadTotal_T2);
            this.flp_TCO_OverLoadTotal.Controls.Add(this.check_Contactor_OverLoadTotal_T3);
            this.flp_TCO_OverLoadTotal.Controls.Add(this.check_Contactor_OverLoadTotal_T4);
            this.flp_TCO_OverLoadTotal.Location = new System.Drawing.Point(3, 3);
            this.flp_TCO_OverLoadTotal.Name = "flp_TCO_OverLoadTotal";
            this.flp_TCO_OverLoadTotal.Size = new System.Drawing.Size(326, 25);
            this.flp_TCO_OverLoadTotal.TabIndex = 50;
            // 
            // lbl_Cont_OverLoadTotal
            // 
            this.lbl_Cont_OverLoadTotal.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Cont_OverLoadTotal.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Cont_OverLoadTotal.Location = new System.Drawing.Point(3, 0);
            this.lbl_Cont_OverLoadTotal.Name = "lbl_Cont_OverLoadTotal";
            this.lbl_Cont_OverLoadTotal.Size = new System.Drawing.Size(140, 15);
            this.lbl_Cont_OverLoadTotal.TabIndex = 44;
            this.lbl_Cont_OverLoadTotal.Text = "Over Load Total";
            // 
            // check_Contactor_OverLoadTotal_T1
            // 
            this.check_Contactor_OverLoadTotal_T1.ForeColor = System.Drawing.Color.Navy;
            this.check_Contactor_OverLoadTotal_T1.Location = new System.Drawing.Point(149, 3);
            this.check_Contactor_OverLoadTotal_T1.Name = "check_Contactor_OverLoadTotal_T1";
            this.check_Contactor_OverLoadTotal_T1.Size = new System.Drawing.Size(39, 19);
            this.check_Contactor_OverLoadTotal_T1.TabIndex = 15;
            this.check_Contactor_OverLoadTotal_T1.Text = "T1";
            this.check_Contactor_OverLoadTotal_T1.UseVisualStyleBackColor = true;
            this.check_Contactor_OverLoadTotal_T1.CheckedChanged += new System.EventHandler(this.check_Contactor_OverLoadTotal_CheckedChanged);
            // 
            // check_Contactor_OverLoadTotal_T2
            // 
            this.check_Contactor_OverLoadTotal_T2.ForeColor = System.Drawing.Color.Navy;
            this.check_Contactor_OverLoadTotal_T2.Location = new System.Drawing.Point(194, 3);
            this.check_Contactor_OverLoadTotal_T2.Name = "check_Contactor_OverLoadTotal_T2";
            this.check_Contactor_OverLoadTotal_T2.Size = new System.Drawing.Size(39, 19);
            this.check_Contactor_OverLoadTotal_T2.TabIndex = 16;
            this.check_Contactor_OverLoadTotal_T2.Text = "T2";
            this.check_Contactor_OverLoadTotal_T2.UseVisualStyleBackColor = true;
            this.check_Contactor_OverLoadTotal_T2.CheckedChanged += new System.EventHandler(this.check_Contactor_OverLoadTotal_CheckedChanged);
            // 
            // check_Contactor_OverLoadTotal_T3
            // 
            this.check_Contactor_OverLoadTotal_T3.ForeColor = System.Drawing.Color.Navy;
            this.check_Contactor_OverLoadTotal_T3.Location = new System.Drawing.Point(239, 3);
            this.check_Contactor_OverLoadTotal_T3.Name = "check_Contactor_OverLoadTotal_T3";
            this.check_Contactor_OverLoadTotal_T3.Size = new System.Drawing.Size(39, 19);
            this.check_Contactor_OverLoadTotal_T3.TabIndex = 17;
            this.check_Contactor_OverLoadTotal_T3.Text = "T3";
            this.check_Contactor_OverLoadTotal_T3.UseVisualStyleBackColor = true;
            this.check_Contactor_OverLoadTotal_T3.CheckedChanged += new System.EventHandler(this.check_Contactor_OverLoadTotal_CheckedChanged);
            // 
            // check_Contactor_OverLoadTotal_T4
            // 
            this.check_Contactor_OverLoadTotal_T4.ForeColor = System.Drawing.Color.Navy;
            this.check_Contactor_OverLoadTotal_T4.Location = new System.Drawing.Point(284, 3);
            this.check_Contactor_OverLoadTotal_T4.Name = "check_Contactor_OverLoadTotal_T4";
            this.check_Contactor_OverLoadTotal_T4.Size = new System.Drawing.Size(39, 19);
            this.check_Contactor_OverLoadTotal_T4.TabIndex = 18;
            this.check_Contactor_OverLoadTotal_T4.Text = "T4";
            this.check_Contactor_OverLoadTotal_T4.UseVisualStyleBackColor = true;
            this.check_Contactor_OverLoadTotal_T4.CheckedChanged += new System.EventHandler(this.check_Contactor_OverLoadTotal_CheckedChanged);
            // 
            // flp_TCO_OverCurrentByPhase
            // 
            this.flp_TCO_OverCurrentByPhase.AutoSize = true;
            this.flp_TCO_OverCurrentByPhase.Controls.Add(this.label2);
            this.flp_TCO_OverCurrentByPhase.Controls.Add(this.cb_TCO_OverCurrentByPhaseT1);
            this.flp_TCO_OverCurrentByPhase.Controls.Add(this.cb_TCO_OverCurrentByPhaseT2);
            this.flp_TCO_OverCurrentByPhase.Controls.Add(this.cb_TCO_OverCurrentByPhaseT3);
            this.flp_TCO_OverCurrentByPhase.Controls.Add(this.cb_TCO_OverCurrentByPhaseT4);
            this.flp_TCO_OverCurrentByPhase.Location = new System.Drawing.Point(3, 34);
            this.flp_TCO_OverCurrentByPhase.Name = "flp_TCO_OverCurrentByPhase";
            this.flp_TCO_OverCurrentByPhase.Size = new System.Drawing.Size(326, 25);
            this.flp_TCO_OverCurrentByPhase.TabIndex = 51;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 15);
            this.label2.TabIndex = 42;
            this.label2.Text = "Over Current By Phase";
            // 
            // cb_TCO_OverCurrentByPhaseT1
            // 
            this.cb_TCO_OverCurrentByPhaseT1.ForeColor = System.Drawing.Color.Navy;
            this.cb_TCO_OverCurrentByPhaseT1.Location = new System.Drawing.Point(149, 3);
            this.cb_TCO_OverCurrentByPhaseT1.Name = "cb_TCO_OverCurrentByPhaseT1";
            this.cb_TCO_OverCurrentByPhaseT1.Size = new System.Drawing.Size(39, 19);
            this.cb_TCO_OverCurrentByPhaseT1.TabIndex = 6;
            this.cb_TCO_OverCurrentByPhaseT1.Text = "T1";
            this.cb_TCO_OverCurrentByPhaseT1.UseVisualStyleBackColor = true;
            this.cb_TCO_OverCurrentByPhaseT1.CheckedChanged += new System.EventHandler(this.cb_TCO_OverCurrentByPhaseT_CheckedChanged);
            // 
            // cb_TCO_OverCurrentByPhaseT2
            // 
            this.cb_TCO_OverCurrentByPhaseT2.ForeColor = System.Drawing.Color.Navy;
            this.cb_TCO_OverCurrentByPhaseT2.Location = new System.Drawing.Point(194, 3);
            this.cb_TCO_OverCurrentByPhaseT2.Name = "cb_TCO_OverCurrentByPhaseT2";
            this.cb_TCO_OverCurrentByPhaseT2.Size = new System.Drawing.Size(39, 19);
            this.cb_TCO_OverCurrentByPhaseT2.TabIndex = 7;
            this.cb_TCO_OverCurrentByPhaseT2.Text = "T2";
            this.cb_TCO_OverCurrentByPhaseT2.UseVisualStyleBackColor = true;
            this.cb_TCO_OverCurrentByPhaseT2.CheckedChanged += new System.EventHandler(this.cb_TCO_OverCurrentByPhaseT_CheckedChanged);
            // 
            // cb_TCO_OverCurrentByPhaseT3
            // 
            this.cb_TCO_OverCurrentByPhaseT3.ForeColor = System.Drawing.Color.Navy;
            this.cb_TCO_OverCurrentByPhaseT3.Location = new System.Drawing.Point(239, 3);
            this.cb_TCO_OverCurrentByPhaseT3.Name = "cb_TCO_OverCurrentByPhaseT3";
            this.cb_TCO_OverCurrentByPhaseT3.Size = new System.Drawing.Size(39, 19);
            this.cb_TCO_OverCurrentByPhaseT3.TabIndex = 8;
            this.cb_TCO_OverCurrentByPhaseT3.Text = "T3";
            this.cb_TCO_OverCurrentByPhaseT3.UseVisualStyleBackColor = true;
            this.cb_TCO_OverCurrentByPhaseT3.CheckedChanged += new System.EventHandler(this.cb_TCO_OverCurrentByPhaseT_CheckedChanged);
            // 
            // cb_TCO_OverCurrentByPhaseT4
            // 
            this.cb_TCO_OverCurrentByPhaseT4.ForeColor = System.Drawing.Color.Navy;
            this.cb_TCO_OverCurrentByPhaseT4.Location = new System.Drawing.Point(284, 3);
            this.cb_TCO_OverCurrentByPhaseT4.Name = "cb_TCO_OverCurrentByPhaseT4";
            this.cb_TCO_OverCurrentByPhaseT4.Size = new System.Drawing.Size(39, 19);
            this.cb_TCO_OverCurrentByPhaseT4.TabIndex = 9;
            this.cb_TCO_OverCurrentByPhaseT4.Text = "T4";
            this.cb_TCO_OverCurrentByPhaseT4.UseVisualStyleBackColor = true;
            this.cb_TCO_OverCurrentByPhaseT4.CheckedChanged += new System.EventHandler(this.cb_TCO_OverCurrentByPhaseT_CheckedChanged);
            // 
            // flp_TCO_OverLoadByPhase
            // 
            this.flp_TCO_OverLoadByPhase.AutoSize = true;
            this.flp_TCO_OverLoadByPhase.Controls.Add(this.label3);
            this.flp_TCO_OverLoadByPhase.Controls.Add(this.cb_TCO_OverLoadByPhaseT1);
            this.flp_TCO_OverLoadByPhase.Controls.Add(this.cb_TCO_OverLoadByPhaseT2);
            this.flp_TCO_OverLoadByPhase.Controls.Add(this.cb_TCO_OverLoadByPhaseT3);
            this.flp_TCO_OverLoadByPhase.Controls.Add(this.cb_TCO_OverLoadByPhaseT4);
            this.flp_TCO_OverLoadByPhase.Location = new System.Drawing.Point(3, 65);
            this.flp_TCO_OverLoadByPhase.Name = "flp_TCO_OverLoadByPhase";
            this.flp_TCO_OverLoadByPhase.Size = new System.Drawing.Size(326, 25);
            this.flp_TCO_OverLoadByPhase.TabIndex = 51;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 15);
            this.label3.TabIndex = 43;
            this.label3.Text = "Over Load By Phase";
            // 
            // cb_TCO_OverLoadByPhaseT1
            // 
            this.cb_TCO_OverLoadByPhaseT1.ForeColor = System.Drawing.Color.Navy;
            this.cb_TCO_OverLoadByPhaseT1.Location = new System.Drawing.Point(149, 3);
            this.cb_TCO_OverLoadByPhaseT1.Name = "cb_TCO_OverLoadByPhaseT1";
            this.cb_TCO_OverLoadByPhaseT1.Size = new System.Drawing.Size(39, 19);
            this.cb_TCO_OverLoadByPhaseT1.TabIndex = 10;
            this.cb_TCO_OverLoadByPhaseT1.Text = "T1";
            this.cb_TCO_OverLoadByPhaseT1.UseVisualStyleBackColor = true;
            this.cb_TCO_OverLoadByPhaseT1.CheckedChanged += new System.EventHandler(this.cb_TCO_OverLoadByPhase_CheckedChanged);
            // 
            // cb_TCO_OverLoadByPhaseT2
            // 
            this.cb_TCO_OverLoadByPhaseT2.ForeColor = System.Drawing.Color.Navy;
            this.cb_TCO_OverLoadByPhaseT2.Location = new System.Drawing.Point(194, 3);
            this.cb_TCO_OverLoadByPhaseT2.Name = "cb_TCO_OverLoadByPhaseT2";
            this.cb_TCO_OverLoadByPhaseT2.Size = new System.Drawing.Size(39, 19);
            this.cb_TCO_OverLoadByPhaseT2.TabIndex = 11;
            this.cb_TCO_OverLoadByPhaseT2.Text = "T2";
            this.cb_TCO_OverLoadByPhaseT2.UseVisualStyleBackColor = true;
            this.cb_TCO_OverLoadByPhaseT2.CheckedChanged += new System.EventHandler(this.cb_TCO_OverLoadByPhase_CheckedChanged);
            // 
            // cb_TCO_OverLoadByPhaseT3
            // 
            this.cb_TCO_OverLoadByPhaseT3.ForeColor = System.Drawing.Color.Navy;
            this.cb_TCO_OverLoadByPhaseT3.Location = new System.Drawing.Point(239, 3);
            this.cb_TCO_OverLoadByPhaseT3.Name = "cb_TCO_OverLoadByPhaseT3";
            this.cb_TCO_OverLoadByPhaseT3.Size = new System.Drawing.Size(39, 19);
            this.cb_TCO_OverLoadByPhaseT3.TabIndex = 12;
            this.cb_TCO_OverLoadByPhaseT3.Text = "T3";
            this.cb_TCO_OverLoadByPhaseT3.UseVisualStyleBackColor = true;
            this.cb_TCO_OverLoadByPhaseT3.CheckedChanged += new System.EventHandler(this.cb_TCO_OverLoadByPhase_CheckedChanged);
            // 
            // cb_TCO_OverLoadByPhaseT4
            // 
            this.cb_TCO_OverLoadByPhaseT4.ForeColor = System.Drawing.Color.Navy;
            this.cb_TCO_OverLoadByPhaseT4.Location = new System.Drawing.Point(284, 3);
            this.cb_TCO_OverLoadByPhaseT4.Name = "cb_TCO_OverLoadByPhaseT4";
            this.cb_TCO_OverLoadByPhaseT4.Size = new System.Drawing.Size(39, 19);
            this.cb_TCO_OverLoadByPhaseT4.TabIndex = 14;
            this.cb_TCO_OverLoadByPhaseT4.Text = "T4";
            this.cb_TCO_OverLoadByPhaseT4.UseVisualStyleBackColor = true;
            this.cb_TCO_OverLoadByPhaseT4.CheckedChanged += new System.EventHandler(this.cb_TCO_OverLoadByPhase_CheckedChanged);
            // 
            // flp_TCO_MdiOverLoad
            // 
            this.flp_TCO_MdiOverLoad.AutoSize = true;
            this.flp_TCO_MdiOverLoad.Controls.Add(this.label4);
            this.flp_TCO_MdiOverLoad.Controls.Add(this.cb_TCO_MdiOverLoadT1);
            this.flp_TCO_MdiOverLoad.Controls.Add(this.cb_TCO_MdiOverLoadT2);
            this.flp_TCO_MdiOverLoad.Controls.Add(this.cb_TCO_MdiOverLoadT3);
            this.flp_TCO_MdiOverLoad.Controls.Add(this.cb_TCO_MdiOverLoadT4);
            this.flp_TCO_MdiOverLoad.Location = new System.Drawing.Point(3, 96);
            this.flp_TCO_MdiOverLoad.Name = "flp_TCO_MdiOverLoad";
            this.flp_TCO_MdiOverLoad.Size = new System.Drawing.Size(326, 25);
            this.flp_TCO_MdiOverLoad.TabIndex = 51;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 15);
            this.label4.TabIndex = 45;
            this.label4.Text = "MDI Over Load";
            // 
            // cb_TCO_MdiOverLoadT1
            // 
            this.cb_TCO_MdiOverLoadT1.ForeColor = System.Drawing.Color.Navy;
            this.cb_TCO_MdiOverLoadT1.Location = new System.Drawing.Point(149, 3);
            this.cb_TCO_MdiOverLoadT1.Name = "cb_TCO_MdiOverLoadT1";
            this.cb_TCO_MdiOverLoadT1.Size = new System.Drawing.Size(39, 19);
            this.cb_TCO_MdiOverLoadT1.TabIndex = 19;
            this.cb_TCO_MdiOverLoadT1.Text = "T1";
            this.cb_TCO_MdiOverLoadT1.UseVisualStyleBackColor = true;
            this.cb_TCO_MdiOverLoadT1.CheckedChanged += new System.EventHandler(this.check_Contactor_MDIOverLoad_CheckedChanged);
            // 
            // cb_TCO_MdiOverLoadT2
            // 
            this.cb_TCO_MdiOverLoadT2.ForeColor = System.Drawing.Color.Navy;
            this.cb_TCO_MdiOverLoadT2.Location = new System.Drawing.Point(194, 3);
            this.cb_TCO_MdiOverLoadT2.Name = "cb_TCO_MdiOverLoadT2";
            this.cb_TCO_MdiOverLoadT2.Size = new System.Drawing.Size(39, 19);
            this.cb_TCO_MdiOverLoadT2.TabIndex = 20;
            this.cb_TCO_MdiOverLoadT2.Text = "T2";
            this.cb_TCO_MdiOverLoadT2.UseVisualStyleBackColor = true;
            this.cb_TCO_MdiOverLoadT2.CheckedChanged += new System.EventHandler(this.check_Contactor_MDIOverLoad_CheckedChanged);
            // 
            // cb_TCO_MdiOverLoadT3
            // 
            this.cb_TCO_MdiOverLoadT3.ForeColor = System.Drawing.Color.Navy;
            this.cb_TCO_MdiOverLoadT3.Location = new System.Drawing.Point(239, 3);
            this.cb_TCO_MdiOverLoadT3.Name = "cb_TCO_MdiOverLoadT3";
            this.cb_TCO_MdiOverLoadT3.Size = new System.Drawing.Size(39, 19);
            this.cb_TCO_MdiOverLoadT3.TabIndex = 21;
            this.cb_TCO_MdiOverLoadT3.Text = "T3";
            this.cb_TCO_MdiOverLoadT3.UseVisualStyleBackColor = true;
            this.cb_TCO_MdiOverLoadT3.CheckedChanged += new System.EventHandler(this.check_Contactor_MDIOverLoad_CheckedChanged);
            // 
            // cb_TCO_MdiOverLoadT4
            // 
            this.cb_TCO_MdiOverLoadT4.ForeColor = System.Drawing.Color.Navy;
            this.cb_TCO_MdiOverLoadT4.Location = new System.Drawing.Point(284, 3);
            this.cb_TCO_MdiOverLoadT4.Name = "cb_TCO_MdiOverLoadT4";
            this.cb_TCO_MdiOverLoadT4.Size = new System.Drawing.Size(39, 19);
            this.cb_TCO_MdiOverLoadT4.TabIndex = 22;
            this.cb_TCO_MdiOverLoadT4.Text = "T4";
            this.cb_TCO_MdiOverLoadT4.UseVisualStyleBackColor = true;
            this.cb_TCO_MdiOverLoadT4.CheckedChanged += new System.EventHandler(this.check_Contactor_MDIOverLoad_CheckedChanged);
            // 
            // ftp_TCO_Events
            // 
            this.ftp_TCO_Events.AutoSize = true;
            this.ftp_TCO_Events.Controls.Add(this.cb_TCO_OverVolt);
            this.ftp_TCO_Events.Controls.Add(this.cb_TCO_UnderVolt);
            this.ftp_TCO_Events.Controls.Add(this.cb_TCO_PhaseFail);
            this.ftp_TCO_Events.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ftp_TCO_Events.Location = new System.Drawing.Point(341, 3);
            this.ftp_TCO_Events.Name = "ftp_TCO_Events";
            this.ftp_TCO_Events.Size = new System.Drawing.Size(91, 75);
            this.ftp_TCO_Events.TabIndex = 52;
            // 
            // cb_TCO_OverVolt
            // 
            this.cb_TCO_OverVolt.AutoSize = true;
            this.cb_TCO_OverVolt.ForeColor = System.Drawing.Color.Navy;
            this.cb_TCO_OverVolt.Location = new System.Drawing.Point(3, 3);
            this.cb_TCO_OverVolt.Name = "cb_TCO_OverVolt";
            this.cb_TCO_OverVolt.Size = new System.Drawing.Size(78, 19);
            this.cb_TCO_OverVolt.TabIndex = 25;
            this.cb_TCO_OverVolt.Text = "Over Volt";
            this.cb_TCO_OverVolt.UseVisualStyleBackColor = true;
            this.cb_TCO_OverVolt.CheckedChanged += new System.EventHandler(this.cb_TCO_OverVolt_CheckedChanged);
            // 
            // cb_TCO_UnderVolt
            // 
            this.cb_TCO_UnderVolt.AutoSize = true;
            this.cb_TCO_UnderVolt.ForeColor = System.Drawing.Color.Navy;
            this.cb_TCO_UnderVolt.Location = new System.Drawing.Point(3, 28);
            this.cb_TCO_UnderVolt.Name = "cb_TCO_UnderVolt";
            this.cb_TCO_UnderVolt.Size = new System.Drawing.Size(85, 19);
            this.cb_TCO_UnderVolt.TabIndex = 26;
            this.cb_TCO_UnderVolt.Text = "Under Volt";
            this.cb_TCO_UnderVolt.UseVisualStyleBackColor = true;
            this.cb_TCO_UnderVolt.CheckedChanged += new System.EventHandler(this.cb_TCO_UnderVolt_CheckedChanged);
            // 
            // cb_TCO_PhaseFail
            // 
            this.cb_TCO_PhaseFail.AutoSize = true;
            this.cb_TCO_PhaseFail.ForeColor = System.Drawing.Color.Navy;
            this.cb_TCO_PhaseFail.Location = new System.Drawing.Point(3, 53);
            this.cb_TCO_PhaseFail.Name = "cb_TCO_PhaseFail";
            this.cb_TCO_PhaseFail.Size = new System.Drawing.Size(79, 19);
            this.cb_TCO_PhaseFail.TabIndex = 27;
            this.cb_TCO_PhaseFail.Text = "Phase Fail";
            this.cb_TCO_PhaseFail.UseVisualStyleBackColor = true;
            this.cb_TCO_PhaseFail.CheckedChanged += new System.EventHandler(this.cb_TCO_PhaseFail_CheckedChanged);
            // 
            // fLP_OverLoad
            // 
            this.fLP_OverLoad.AutoSize = true;
            this.fLP_OverLoad.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_OverLoad.Controls.Add(this.fLP_OverLoad_Mntr);
            this.fLP_OverLoad.Controls.Add(this.pnl_OverLoad_Limit);
            this.fLP_OverLoad.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLP_OverLoad.Location = new System.Drawing.Point(3, 19);
            this.fLP_OverLoad.Name = "fLP_OverLoad";
            this.fLP_OverLoad.Size = new System.Drawing.Size(427, 75);
            this.fLP_OverLoad.TabIndex = 52;
            // 
            // fLP_OverLoad_Mntr
            // 
            this.fLP_OverLoad_Mntr.AutoSize = true;
            this.fLP_OverLoad_Mntr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_OverLoad_Mntr.Controls.Add(this.lbl_MT_OverloaKW);
            this.fLP_OverLoad_Mntr.Controls.Add(this.txt_MonitoringTime_OverLoad_c);
            this.fLP_OverLoad_Mntr.Location = new System.Drawing.Point(3, 0);
            this.fLP_OverLoad_Mntr.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.fLP_OverLoad_Mntr.Name = "fLP_OverLoad_Mntr";
            this.fLP_OverLoad_Mntr.Size = new System.Drawing.Size(210, 23);
            this.fLP_OverLoad_Mntr.TabIndex = 51;
            // 
            // lbl_MT_OverloaKW
            // 
            this.lbl_MT_OverloaKW.BackColor = System.Drawing.Color.Transparent;
            this.lbl_MT_OverloaKW.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lbl_MT_OverloaKW.ForeColor = System.Drawing.Color.Navy;
            this.lbl_MT_OverloaKW.Location = new System.Drawing.Point(3, 3);
            this.lbl_MT_OverloaKW.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_MT_OverloaKW.Name = "lbl_MT_OverloaKW";
            this.lbl_MT_OverloaKW.Size = new System.Drawing.Size(97, 15);
            this.lbl_MT_OverloaKW.TabIndex = 56;
            this.lbl_MT_OverloaKW.Text = "Monitoring time";
            // 
            // txt_MonitoringTime_OverLoad_c
            // 
            this.txt_MonitoringTime_OverLoad_c.CustomFormat = "mm:ss";
            this.txt_MonitoringTime_OverLoad_c.Enabled = false;
            this.txt_MonitoringTime_OverLoad_c.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txt_MonitoringTime_OverLoad_c.Location = new System.Drawing.Point(148, 0);
            this.txt_MonitoringTime_OverLoad_c.Margin = new System.Windows.Forms.Padding(45, 0, 3, 0);
            this.txt_MonitoringTime_OverLoad_c.Name = "txt_MonitoringTime_OverLoad_c";
            this.txt_MonitoringTime_OverLoad_c.ShowUpDown = true;
            this.txt_MonitoringTime_OverLoad_c.Size = new System.Drawing.Size(59, 23);
            this.txt_MonitoringTime_OverLoad_c.TabIndex = 55;
            this.txt_MonitoringTime_OverLoad_c.ValueChanged += new System.EventHandler(this.txt_MonitoringTime_OverLoad_Leave);
            // 
            // pnl_OverLoad_Limit
            // 
            this.pnl_OverLoad_Limit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnl_OverLoad_Limit.Controls.Add(this.lbl_Limit_OverLoadTotal);
            this.pnl_OverLoad_Limit.Controls.Add(this.txt_OverLoadTotal_c_T1);
            this.pnl_OverLoad_Limit.Controls.Add(this.txt_OverLoadTotal_c_T2);
            this.pnl_OverLoad_Limit.Controls.Add(this.label184);
            this.pnl_OverLoad_Limit.Controls.Add(this.label180);
            this.pnl_OverLoad_Limit.Controls.Add(this.label179);
            this.pnl_OverLoad_Limit.Controls.Add(this.label176);
            this.pnl_OverLoad_Limit.Controls.Add(this.txt_OverLoadTotal_c_T4);
            this.pnl_OverLoad_Limit.Controls.Add(this.txt_OverLoadTotal_c_T3);
            this.pnl_OverLoad_Limit.Location = new System.Drawing.Point(3, 23);
            this.pnl_OverLoad_Limit.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnl_OverLoad_Limit.Name = "pnl_OverLoad_Limit";
            this.pnl_OverLoad_Limit.Size = new System.Drawing.Size(421, 52);
            this.pnl_OverLoad_Limit.TabIndex = 50;
            // 
            // lbl_Limit_OverLoadTotal
            // 
            this.lbl_Limit_OverLoadTotal.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Limit_OverLoadTotal.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lbl_Limit_OverLoadTotal.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Limit_OverLoadTotal.Location = new System.Drawing.Point(7, 19);
            this.lbl_Limit_OverLoadTotal.Name = "lbl_Limit_OverLoadTotal";
            this.lbl_Limit_OverLoadTotal.Size = new System.Drawing.Size(93, 15);
            this.lbl_Limit_OverLoadTotal.TabIndex = 50;
            this.lbl_Limit_OverLoadTotal.Text = "Over Load Total (KW)";
            // 
            // txt_OverLoadTotal_c_T1
            // 
            this.txt_OverLoadTotal_c_T1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_OverLoadTotal_c_T1.Location = new System.Drawing.Point(180, 17);
            this.txt_OverLoadTotal_c_T1.Name = "txt_OverLoadTotal_c_T1";
            this.txt_OverLoadTotal_c_T1.ReadOnly = true;
            this.txt_OverLoadTotal_c_T1.Size = new System.Drawing.Size(47, 23);
            this.txt_OverLoadTotal_c_T1.TabIndex = 51;
            this.txt_OverLoadTotal_c_T1.Text = "0";
            this.txt_OverLoadTotal_c_T1.Leave += new System.EventHandler(this.txt_OverLoadTotal_T1_Leave);
            // 
            // txt_OverLoadTotal_c_T2
            // 
            this.txt_OverLoadTotal_c_T2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_OverLoadTotal_c_T2.Location = new System.Drawing.Point(245, 18);
            this.txt_OverLoadTotal_c_T2.Name = "txt_OverLoadTotal_c_T2";
            this.txt_OverLoadTotal_c_T2.ReadOnly = true;
            this.txt_OverLoadTotal_c_T2.Size = new System.Drawing.Size(47, 23);
            this.txt_OverLoadTotal_c_T2.TabIndex = 52;
            this.txt_OverLoadTotal_c_T2.Text = "0";
            this.txt_OverLoadTotal_c_T2.Leave += new System.EventHandler(this.txt_OverLoadTotal_T2_Leave);
            // 
            // label184
            // 
            this.label184.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label184.AutoSize = true;
            this.label184.BackColor = System.Drawing.Color.Transparent;
            this.label184.ForeColor = System.Drawing.Color.Navy;
            this.label184.Location = new System.Drawing.Point(192, 1);
            this.label184.Name = "label184";
            this.label184.Size = new System.Drawing.Size(20, 15);
            this.label184.TabIndex = 46;
            this.label184.Text = "T1";
            // 
            // label180
            // 
            this.label180.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label180.AutoSize = true;
            this.label180.BackColor = System.Drawing.Color.Transparent;
            this.label180.ForeColor = System.Drawing.Color.Navy;
            this.label180.Location = new System.Drawing.Point(383, 1);
            this.label180.Name = "label180";
            this.label180.Size = new System.Drawing.Size(20, 15);
            this.label180.TabIndex = 47;
            this.label180.Text = "T4";
            // 
            // label179
            // 
            this.label179.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label179.AutoSize = true;
            this.label179.BackColor = System.Drawing.Color.Transparent;
            this.label179.ForeColor = System.Drawing.Color.Navy;
            this.label179.Location = new System.Drawing.Point(321, 1);
            this.label179.Name = "label179";
            this.label179.Size = new System.Drawing.Size(20, 15);
            this.label179.TabIndex = 48;
            this.label179.Text = "T3";
            // 
            // label176
            // 
            this.label176.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label176.AutoSize = true;
            this.label176.BackColor = System.Drawing.Color.Transparent;
            this.label176.ForeColor = System.Drawing.Color.Navy;
            this.label176.Location = new System.Drawing.Point(255, 1);
            this.label176.Name = "label176";
            this.label176.Size = new System.Drawing.Size(20, 15);
            this.label176.TabIndex = 49;
            this.label176.Text = "T2";
            // 
            // txt_OverLoadTotal_c_T4
            // 
            this.txt_OverLoadTotal_c_T4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_OverLoadTotal_c_T4.Location = new System.Drawing.Point(371, 17);
            this.txt_OverLoadTotal_c_T4.Name = "txt_OverLoadTotal_c_T4";
            this.txt_OverLoadTotal_c_T4.ReadOnly = true;
            this.txt_OverLoadTotal_c_T4.Size = new System.Drawing.Size(47, 23);
            this.txt_OverLoadTotal_c_T4.TabIndex = 54;
            this.txt_OverLoadTotal_c_T4.Text = "0";
            this.txt_OverLoadTotal_c_T4.Leave += new System.EventHandler(this.txt_OverLoadTotal_T4_Leave);
            // 
            // txt_OverLoadTotal_c_T3
            // 
            this.txt_OverLoadTotal_c_T3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_OverLoadTotal_c_T3.Location = new System.Drawing.Point(309, 18);
            this.txt_OverLoadTotal_c_T3.Name = "txt_OverLoadTotal_c_T3";
            this.txt_OverLoadTotal_c_T3.ReadOnly = true;
            this.txt_OverLoadTotal_c_T3.Size = new System.Drawing.Size(47, 23);
            this.txt_OverLoadTotal_c_T3.TabIndex = 53;
            this.txt_OverLoadTotal_c_T3.Text = "0";
            this.txt_OverLoadTotal_c_T3.Leave += new System.EventHandler(this.txt_OverLoadTotal_T3_Leave);
            // 
            // gpContactorControl
            // 
            this.gpContactorControl.Controls.Add(this.check_Status);
            this.gpContactorControl.Controls.Add(this.check_Contactor_LocalControl);
            this.gpContactorControl.Enabled = false;
            this.gpContactorControl.Location = new System.Drawing.Point(772, 37);
            this.gpContactorControl.Name = "gpContactorControl";
            this.gpContactorControl.Size = new System.Drawing.Size(0, 0);
            this.gpContactorControl.TabIndex = 66;
            this.gpContactorControl.TabStop = false;
            this.gpContactorControl.Text = "Contactor Control";
            this.gpContactorControl.Visible = false;
            // 
            // check_Status
            // 
            this.check_Status.AutoSize = true;
            this.check_Status.Enabled = false;
            this.check_Status.Location = new System.Drawing.Point(289, 13);
            this.check_Status.Name = "check_Status";
            this.check_Status.Size = new System.Drawing.Size(55, 19);
            this.check_Status.TabIndex = 28;
            this.check_Status.Text = "Relay";
            this.check_Status.UseVisualStyleBackColor = true;
            // 
            // check_Contactor_LocalControl
            // 
            this.check_Contactor_LocalControl.AutoSize = true;
            this.check_Contactor_LocalControl.Enabled = false;
            this.check_Contactor_LocalControl.ForeColor = System.Drawing.Color.Navy;
            this.check_Contactor_LocalControl.Location = new System.Drawing.Point(186, 13);
            this.check_Contactor_LocalControl.Name = "check_Contactor_LocalControl";
            this.check_Contactor_LocalControl.Size = new System.Drawing.Size(97, 19);
            this.check_Contactor_LocalControl.TabIndex = 24;
            this.check_Contactor_LocalControl.Text = "Local Control";
            this.check_Contactor_LocalControl.UseVisualStyleBackColor = true;
            // 
            // lbl_Contactor_OverLoadByPhase
            // 
            this.lbl_Contactor_OverLoadByPhase.AutoSize = true;
            this.lbl_Contactor_OverLoadByPhase.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Contactor_OverLoadByPhase.Enabled = false;
            this.lbl_Contactor_OverLoadByPhase.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Contactor_OverLoadByPhase.Location = new System.Drawing.Point(5, 74);
            this.lbl_Contactor_OverLoadByPhase.Name = "lbl_Contactor_OverLoadByPhase";
            this.lbl_Contactor_OverLoadByPhase.Size = new System.Drawing.Size(114, 15);
            this.lbl_Contactor_OverLoadByPhase.TabIndex = 43;
            this.lbl_Contactor_OverLoadByPhase.Text = "Over Load By Phase";
            // 
            // lbl_Contactor_MDIOverLoad
            // 
            this.lbl_Contactor_MDIOverLoad.AutoSize = true;
            this.lbl_Contactor_MDIOverLoad.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Contactor_MDIOverLoad.Enabled = false;
            this.lbl_Contactor_MDIOverLoad.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Contactor_MDIOverLoad.Location = new System.Drawing.Point(6, 111);
            this.lbl_Contactor_MDIOverLoad.Name = "lbl_Contactor_MDIOverLoad";
            this.lbl_Contactor_MDIOverLoad.Size = new System.Drawing.Size(88, 15);
            this.lbl_Contactor_MDIOverLoad.TabIndex = 45;
            this.lbl_Contactor_MDIOverLoad.Text = "MDI Over Load";
            // 
            // lbl_Contactor_OverCurrentByPhase
            // 
            this.lbl_Contactor_OverCurrentByPhase.AutoSize = true;
            this.lbl_Contactor_OverCurrentByPhase.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Contactor_OverCurrentByPhase.Enabled = false;
            this.lbl_Contactor_OverCurrentByPhase.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Contactor_OverCurrentByPhase.Location = new System.Drawing.Point(6, 57);
            this.lbl_Contactor_OverCurrentByPhase.Name = "lbl_Contactor_OverCurrentByPhase";
            this.lbl_Contactor_OverCurrentByPhase.Size = new System.Drawing.Size(131, 15);
            this.lbl_Contactor_OverCurrentByPhase.TabIndex = 42;
            this.lbl_Contactor_OverCurrentByPhase.Text = "Over Current By Phase";
            // 
            // check_Contactor_RemoteControl
            // 
            this.check_Contactor_RemoteControl.AutoSize = true;
            this.check_Contactor_RemoteControl.Enabled = false;
            this.check_Contactor_RemoteControl.ForeColor = System.Drawing.Color.Navy;
            this.check_Contactor_RemoteControl.Location = new System.Drawing.Point(6, 13);
            this.check_Contactor_RemoteControl.Name = "check_Contactor_RemoteControl";
            this.check_Contactor_RemoteControl.Size = new System.Drawing.Size(114, 19);
            this.check_Contactor_RemoteControl.TabIndex = 23;
            this.check_Contactor_RemoteControl.Text = "Remote Control";
            this.check_Contactor_RemoteControl.UseVisualStyleBackColor = true;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ucContactor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.gpContactor);
            this.DoubleBuffered = true;
            this.Name = "ucContactor";
            this.Size = new System.Drawing.Size(1088, 689);
            this.Load += new System.EventHandler(this.ucContactor_Load);
            this.Enter += new System.EventHandler(this.ucContactor_Enter);
            this.gpContactor.ResumeLayout(false);
            this.gpContactor.PerformLayout();
            this.fLP_Buttons.ResumeLayout(false);
            this.fLP_Buttons.PerformLayout();
            this.fLP_Params.ResumeLayout(false);
            this.fLP_Main.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.fLP_TopRow.ResumeLayout(false);
            this.fLP_TopRow.PerformLayout();
            this.fLP_txt_Box_Col.ResumeLayout(false);
            this.fLP_txt_Box_Col.PerformLayout();
            this.fLP_ContactorOn.ResumeLayout(false);
            this.fLP_ContactorOn.PerformLayout();
            this.fLP_ContactorOff.ResumeLayout(false);
            this.fLP_ContactorOff.PerformLayout();
            this.fLP_MinInterval_Stat.ResumeLayout(false);
            this.fLP_MinInterval_Stat.PerformLayout();
            this.fLP_PUPD_Stat.ResumeLayout(false);
            this.fLP_PUPD_Stat.PerformLayout();
            this.fLP_Intr_ContFailureStat.ResumeLayout(false);
            this.fLP_Intr_ContFailureStat.PerformLayout();
            this.fLP_CheckBoxs.ResumeLayout(false);
            this.fLP_CheckBoxs.PerformLayout();
            this.gp_Reconnect.ResumeLayout(false);
            this.gp_Reconnect.PerformLayout();
            this.fLP_Reconnect.ResumeLayout(false);
            this.fLP_Reconnect.PerformLayout();
            this.gp_Reconn_Retry.ResumeLayout(false);
            this.gp_Reconn_Retry.PerformLayout();
            this.fLP_Retry.ResumeLayout(false);
            this.fLP_Retry.PerformLayout();
            this.fLP_Retry_Radio.ResumeLayout(false);
            this.fLP_Retry_Radio.PerformLayout();
            this.fLP_Retry_AutoIntr.ResumeLayout(false);
            this.fLP_Retry_AutoIntr.PerformLayout();
            this.fLP_Retry_Count.ResumeLayout(false);
            this.fLP_Retry_Count.PerformLayout();
            this.gp_RetryExpire.ResumeLayout(false);
            this.gp_RetryExpire.PerformLayout();
            this.fLP_RetryExpire.ResumeLayout(false);
            this.fLP_RetryExpire.PerformLayout();
            this.fLP_RtryExp.ResumeLayout(false);
            this.fLP_RtryExp.PerformLayout();
            this.fLP_RetryExpire_AutoInterval.ResumeLayout(false);
            this.fLP_RetryExpire_AutoInterval.PerformLayout();
            this.flp_LastRow.ResumeLayout(false);
            this.gp_OverloadLimitControl.ResumeLayout(false);
            this.gp_OverloadLimitControl.PerformLayout();
            this.gp_turn_off_con.ResumeLayout(false);
            this.gp_turn_off_con.PerformLayout();
            this.flowLayoutPanel7.ResumeLayout(false);
            this.flowLayoutPanel7.PerformLayout();
            this.flp_TCO_Limits.ResumeLayout(false);
            this.flp_TCO_Limits.PerformLayout();
            this.flp_TCO_OverLoadTotal.ResumeLayout(false);
            this.flp_TCO_OverCurrentByPhase.ResumeLayout(false);
            this.flp_TCO_OverLoadByPhase.ResumeLayout(false);
            this.flp_TCO_MdiOverLoad.ResumeLayout(false);
            this.ftp_TCO_Events.ResumeLayout(false);
            this.ftp_TCO_Events.PerformLayout();
            this.fLP_OverLoad.ResumeLayout(false);
            this.fLP_OverLoad.PerformLayout();
            this.fLP_OverLoad_Mntr.ResumeLayout(false);
            this.pnl_OverLoad_Limit.ResumeLayout(false);
            this.pnl_OverLoad_Limit.PerformLayout();
            this.gpContactorControl.ResumeLayout(false);
            this.gpContactorControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox gpContactor;
        private System.Windows.Forms.GroupBox gp_OverloadLimitControl;
        public System.Windows.Forms.TextBox txt_OverLoadTotal_c_T1;
        public System.Windows.Forms.TextBox txt_OverLoadTotal_c_T2;
        public System.Windows.Forms.Label label176;
        private System.Windows.Forms.DateTimePicker txt_MonitoringTime_OverLoad_c;
        public System.Windows.Forms.Label label179;
        public System.Windows.Forms.Label lbl_MT_OverloaKW;
        public System.Windows.Forms.Label label180;
        public System.Windows.Forms.TextBox txt_OverLoadTotal_c_T4;
        public System.Windows.Forms.Label label184;
        public System.Windows.Forms.TextBox txt_OverLoadTotal_c_T3;
        private System.Windows.Forms.GroupBox gp_Reconnect;
        private System.Windows.Forms.GroupBox gp_RetryExpire;
        private System.Windows.Forms.ComboBox txt_Contactor_ControlMode;
        private System.Windows.Forms.Label label178;
        private System.Windows.Forms.CheckBox check_contactor_reconnectSwitch;
        public System.Windows.Forms.Label lbl_OnRetry_AutoInterval;
        private System.Windows.Forms.GroupBox gp_Reconn_Retry;
        private System.Windows.Forms.RadioButton radio_contactor_switch;
        private System.Windows.Forms.RadioButton radio_contactor_auto;
        public System.Windows.Forms.Label lbl_AutoInterval;
        public System.Windows.Forms.Label lbl_RetryCount;
        public System.Windows.Forms.Label label175;
        public System.Windows.Forms.TextBox txt_Contactor_RetryCount;
        public System.Windows.Forms.TextBox txt_Contactor_IntervalBWEntries;
        private System.Windows.Forms.CheckBox check_Contactor_ReconnectonTariffChange;
        private System.Windows.Forms.CheckBox check_Contactor_offOptically;
        private System.Windows.Forms.CheckBox check_Contactor_onOptically;
        public System.Windows.Forms.TextBox txt_Contactor_PowerUpDelayToChangeState;
        public System.Windows.Forms.TextBox txt_Contactor_MinIntervalBwContactorStateChange;
        public System.Windows.Forms.TextBox txt_Contactor_ContactorOFF_PulseTime;
        public System.Windows.Forms.TextBox txt_Contactor_ContactorON_PulseTime;
        public System.Windows.Forms.Label lbl_Contactor_MinIntervalBwContactorStateChange;
        public System.Windows.Forms.Label label138;
        public System.Windows.Forms.Label label137;
        public System.Windows.Forms.Label label140;
        public System.Windows.Forms.Label label139;
        public System.Windows.Forms.Label lbl_Contactor_PowerUpDelayToChangeState;
        public System.Windows.Forms.Label lbl_Contactor_ContactorOFF_PulseTime;
        public System.Windows.Forms.Label lbl_Contactor_ContactorON_PulseTime;
        private System.Windows.Forms.GroupBox gpContactorControl;
        private System.Windows.Forms.CheckBox check_Status;
        public System.Windows.Forms.Label lbl_Contactor_OverLoadByPhase;
        public System.Windows.Forms.Label lbl_Contactor_MDIOverLoad;
        public System.Windows.Forms.Label lbl_Contactor_OverCurrentByPhase;
        public System.Windows.Forms.CheckBox check_Contactor_RemoteControl;
        //public System.Windows.Forms.CheckBox check_Contactor_MDIOverLoad_T4;
        public System.Windows.Forms.CheckBox check_Contactor_LocalControl;
        //public System.Windows.Forms.CheckBox check_Contactor_OverCurrentByPhase_T3;
        //public System.Windows.Forms.CheckBox check_Contactor_OverCurrentByPhase_T2;
        //public System.Windows.Forms.CheckBox check_Contactor_MDIOverLoad_T1;
        //public System.Windows.Forms.CheckBox check_Contactor_OverCurrentByPhase_T1;
        //public System.Windows.Forms.CheckBox check_Contactor_MDIOverLoad_T3;
        //public System.Windows.Forms.CheckBox check_Contactor_OverLoadByPhase_T1;
        //public System.Windows.Forms.CheckBox check_Contactor_OverCurrentByPhase_T4;
        //public System.Windows.Forms.CheckBox check_Contactor_OverLoadByPhase_T3;
        //public System.Windows.Forms.CheckBox check_Contactor_OverLoadByPhase_T2;
        //public System.Windows.Forms.CheckBox check_Contactor_MDIOverLoad_T2;
        //public System.Windows.Forms.CheckBox check_Contactor_OverLoadByPhase_T4;
        internal ComponentFactory.Krypton.Toolkit.KryptonButton btn_SetContactorParameters;
        internal ComponentFactory.Krypton.Toolkit.KryptonButton btn_GetContactorParams;
        internal System.Windows.Forms.Label lbl_ContactorStatus;
        private System.Windows.Forms.ErrorProvider errorProvider;
        public System.Windows.Forms.TextBox txt_Contactor_Failure_Status;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lbl_Interval_Contactor_Failure_Status;
        private System.Windows.Forms.CheckBox check_contactor_reconnectAuto;
        internal ComponentFactory.Krypton.Toolkit.KryptonButton btn_ConnectContactor;
        internal ComponentFactory.Krypton.Toolkit.KryptonButton btnReadStatus;
        internal ComponentFactory.Krypton.Toolkit.KryptonButton btn_DisconnectContactor;
        internal ComponentFactory.Krypton.Toolkit.KryptonButton btn_connectThroughSwitch;
        private System.Windows.Forms.FlowLayoutPanel fLP_ContactorOn;
        private System.Windows.Forms.FlowLayoutPanel fLP_ContactorOff;
        private System.Windows.Forms.FlowLayoutPanel fLP_MinInterval_Stat;
        private System.Windows.Forms.FlowLayoutPanel fLP_PUPD_Stat;
        private System.Windows.Forms.FlowLayoutPanel fLP_Intr_ContFailureStat;
        private System.Windows.Forms.FlowLayoutPanel fLP_txt_Box_Col;
        private System.Windows.Forms.FlowLayoutPanel fLP_CheckBoxs;
        private System.Windows.Forms.FlowLayoutPanel fLP_Buttons;
        private System.Windows.Forms.FlowLayoutPanel fLP_TopRow;
        private System.Windows.Forms.FlowLayoutPanel fLP_Retry_Radio;
        private System.Windows.Forms.FlowLayoutPanel fLP_Retry_AutoIntr;
        private System.Windows.Forms.FlowLayoutPanel fLP_Retry_Count;
        private System.Windows.Forms.FlowLayoutPanel fLP_Retry;
        private System.Windows.Forms.FlowLayoutPanel fLP_Params;
        private System.Windows.Forms.FlowLayoutPanel fLP_RtryExp;
        private System.Windows.Forms.FlowLayoutPanel fLP_RetryExpire_AutoInterval;
        private System.Windows.Forms.FlowLayoutPanel fLP_RetryExpire;
        private System.Windows.Forms.FlowLayoutPanel fLP_Reconnect;
        public System.Windows.Forms.Label lbl_Limit_OverLoadTotal;
        private System.Windows.Forms.Panel pnl_OverLoad_Limit;
        private System.Windows.Forms.FlowLayoutPanel fLP_OverLoad_Mntr;
        private System.Windows.Forms.FlowLayoutPanel fLP_OverLoad;
        private System.Windows.Forms.FlowLayoutPanel flp_LastRow;
        internal System.Windows.Forms.ProgressBar pb_Contactor;
        private System.Windows.Forms.FlowLayoutPanel fLP_Main;
        private System.Windows.Forms.GroupBox gp_turn_off_con;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private System.Windows.Forms.FlowLayoutPanel flp_TCO_Limits;
        private System.Windows.Forms.FlowLayoutPanel flp_TCO_OverLoadTotal;
        public System.Windows.Forms.Label lbl_Cont_OverLoadTotal;
        public System.Windows.Forms.CheckBox check_Contactor_OverLoadTotal_T1;
        public System.Windows.Forms.CheckBox check_Contactor_OverLoadTotal_T2;
        public System.Windows.Forms.CheckBox check_Contactor_OverLoadTotal_T3;
        public System.Windows.Forms.CheckBox check_Contactor_OverLoadTotal_T4;
        private System.Windows.Forms.FlowLayoutPanel flp_TCO_OverCurrentByPhase;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.CheckBox cb_TCO_OverCurrentByPhaseT1;
        public System.Windows.Forms.CheckBox cb_TCO_OverCurrentByPhaseT2;
        public System.Windows.Forms.CheckBox cb_TCO_OverCurrentByPhaseT3;
        public System.Windows.Forms.CheckBox cb_TCO_OverCurrentByPhaseT4;
        private System.Windows.Forms.FlowLayoutPanel flp_TCO_OverLoadByPhase;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.CheckBox cb_TCO_OverLoadByPhaseT1;
        public System.Windows.Forms.CheckBox cb_TCO_OverLoadByPhaseT2;
        public System.Windows.Forms.CheckBox cb_TCO_OverLoadByPhaseT3;
        public System.Windows.Forms.CheckBox cb_TCO_OverLoadByPhaseT4;
        private System.Windows.Forms.FlowLayoutPanel flp_TCO_MdiOverLoad;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.CheckBox cb_TCO_MdiOverLoadT1;
        public System.Windows.Forms.CheckBox cb_TCO_MdiOverLoadT2;
        public System.Windows.Forms.CheckBox cb_TCO_MdiOverLoadT3;
        public System.Windows.Forms.CheckBox cb_TCO_MdiOverLoadT4;
        private System.Windows.Forms.FlowLayoutPanel ftp_TCO_Events;
        public System.Windows.Forms.CheckBox cb_TCO_OverVolt;
        public System.Windows.Forms.CheckBox cb_TCO_UnderVolt;
        public System.Windows.Forms.CheckBox cb_TCO_PhaseFail;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}
