using System.Windows.Forms;
namespace SmartEyeControl_7.ApplicationGUI.GUI
{
    partial class CustomSetGet
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.check_CTPT = new System.Windows.Forms.CheckBox();
            this.check_Clock = new System.Windows.Forms.CheckBox();
            this.check_Contactor = new System.Windows.Forms.CheckBox();
            this.check_customerReference = new System.Windows.Forms.CheckBox();
            this.check_Limits = new System.Windows.Forms.CheckBox();
            this.check_MonitoringTime = new System.Windows.Forms.CheckBox();
            this.check_DecimalPoint = new System.Windows.Forms.CheckBox();
            this.check_EnergyParams = new System.Windows.Forms.CheckBox();
            this.check_EventCaution = new System.Windows.Forms.CheckBox();
            this.check_IPV4 = new System.Windows.Forms.CheckBox();
            this.btn_CustomSelectAll = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_CustomDeselect = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_CustomOK = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_CustomCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.check_TCPUDP = new System.Windows.Forms.CheckBox();
            this.check_MDI_params = new System.Windows.Forms.CheckBox();
            this.check_DisplayWindows_Nor = new System.Windows.Forms.CheckBox();
            this.check_ActivityCalender = new System.Windows.Forms.CheckBox();
            this.check_DataProfilewithEvents = new System.Windows.Forms.CheckBox();
            this.check_MajorAlarmprofile = new System.Windows.Forms.CheckBox();
            this.gbMeterSetting = new System.Windows.Forms.GroupBox();
            this.fLP_MeterSettings = new System.Windows.Forms.FlowLayoutPanel();
            this.check_DisplayWindows_Alt = new System.Windows.Forms.CheckBox();
            this.check_DisplayWindows_test = new System.Windows.Forms.CheckBox();
            this.check_DisplayPowerDown = new System.Windows.Forms.CheckBox();
            this.chk_PQ_LoadProfileInterval = new System.Windows.Forms.CheckBox();
            this.chk_LoadProfile_2_Interval = new System.Windows.Forms.CheckBox();
            this.chk_LoadProfile_2 = new System.Windows.Forms.CheckBox();
            this.chk_LoadProfile_Interval = new System.Windows.Forms.CheckBox();
            this.chk_LoadProfile = new System.Windows.Forms.CheckBox();
            this.check_Password_Elec = new System.Windows.Forms.CheckBox();
            this.chbStatusWordMap1 = new System.Windows.Forms.CheckBox();
            this.chbStatusWordMap2 = new System.Windows.Forms.CheckBox();
            this.check_loadShedding = new System.Windows.Forms.CheckBox();
            this.check_GeneratorStart = new System.Windows.Forms.CheckBox();
            this.check_Time = new System.Windows.Forms.CheckBox();
            this.chk_DisplayPowerDown = new System.Windows.Forms.CheckBox();
            this.gbTBESetting = new System.Windows.Forms.GroupBox();
            this.flpTimeEvents = new System.Windows.Forms.FlowLayoutPanel();
            this.check_TBEs = new System.Windows.Forms.CheckBox();
            this.chk_GPP = new System.Windows.Forms.CheckBox();
            this.gbSecurityKeys = new System.Windows.Forms.GroupBox();
            this.flpSecurityKeys = new System.Windows.Forms.FlowLayoutPanel();
            this.check_WriteAuthenticationKey = new System.Windows.Forms.CheckBox();
            this.check_EncryptionKey = new System.Windows.Forms.CheckBox();
            this.check_SecurityPolicy = new System.Windows.Forms.CheckBox();
            this.gbStandardModem = new System.Windows.Forms.GroupBox();
            this.flpStandardModem = new System.Windows.Forms.FlowLayoutPanel();
            this.check_StandardModem_IP_Profile = new System.Windows.Forms.CheckBox();
            this.check_StandardModem_Number_Profile = new System.Windows.Forms.CheckBox();
            this.check_StandardModem_KeepAlive = new System.Windows.Forms.CheckBox();
            this.gbModemSetting = new System.Windows.Forms.GroupBox();
            this.flp_ModemSettings = new System.Windows.Forms.FlowLayoutPanel();
            this.check_IP_Profile = new System.Windows.Forms.CheckBox();
            this.chbWakeupProfile = new System.Windows.Forms.CheckBox();
            this.chbKeepAlive = new System.Windows.Forms.CheckBox();
            this.chbNumberProfile = new System.Windows.Forms.CheckBox();
            this.chbModemLimitsAndTime = new System.Windows.Forms.CheckBox();
            this.chbModemInitialize = new System.Windows.Forms.CheckBox();
            this.chbCommunicationProfile = new System.Windows.Forms.CheckBox();
            this.gbMeterSetting.SuspendLayout();
            this.fLP_MeterSettings.SuspendLayout();
            this.gbTBESetting.SuspendLayout();
            this.flpTimeEvents.SuspendLayout();
            this.gbSecurityKeys.SuspendLayout();
            this.flpSecurityKeys.SuspendLayout();
            this.gbStandardModem.SuspendLayout();
            this.flpStandardModem.SuspendLayout();
            this.gbModemSetting.SuspendLayout();
            this.flp_ModemSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // check_CTPT
            // 
            this.check_CTPT.AutoSize = true;
            this.check_CTPT.BackColor = System.Drawing.Color.Transparent;
            this.check_CTPT.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_CTPT.ForeColor = System.Drawing.Color.Black;
            this.check_CTPT.Location = new System.Drawing.Point(3, 28);
            this.check_CTPT.Name = "check_CTPT";
            this.check_CTPT.Size = new System.Drawing.Size(91, 19);
            this.check_CTPT.TabIndex = 0;
            this.check_CTPT.Text = "CT PT Ratios";
            this.check_CTPT.UseVisualStyleBackColor = false;
            // 
            // check_Clock
            // 
            this.check_Clock.AutoSize = true;
            this.check_Clock.BackColor = System.Drawing.Color.Transparent;
            this.check_Clock.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_Clock.ForeColor = System.Drawing.Color.Black;
            this.check_Clock.Location = new System.Drawing.Point(3, 53);
            this.check_Clock.Name = "check_Clock";
            this.check_Clock.Size = new System.Drawing.Size(123, 19);
            this.check_Clock.TabIndex = 1;
            this.check_Clock.Text = "Clock Caliberation";
            this.check_Clock.UseVisualStyleBackColor = false;
            // 
            // check_Contactor
            // 
            this.check_Contactor.AutoSize = true;
            this.check_Contactor.BackColor = System.Drawing.Color.Transparent;
            this.check_Contactor.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_Contactor.ForeColor = System.Drawing.Color.Black;
            this.check_Contactor.Location = new System.Drawing.Point(3, 78);
            this.check_Contactor.Name = "check_Contactor";
            this.check_Contactor.Size = new System.Drawing.Size(123, 19);
            this.check_Contactor.TabIndex = 2;
            this.check_Contactor.Text = "Contactor Params";
            this.check_Contactor.UseVisualStyleBackColor = false;
            // 
            // check_customerReference
            // 
            this.check_customerReference.AutoSize = true;
            this.check_customerReference.BackColor = System.Drawing.Color.Transparent;
            this.check_customerReference.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_customerReference.ForeColor = System.Drawing.Color.Black;
            this.check_customerReference.Location = new System.Drawing.Point(3, 103);
            this.check_customerReference.Name = "check_customerReference";
            this.check_customerReference.Size = new System.Drawing.Size(139, 19);
            this.check_customerReference.TabIndex = 4;
            this.check_customerReference.Text = "Customer Reference";
            this.check_customerReference.UseVisualStyleBackColor = false;
            // 
            // check_Limits
            // 
            this.check_Limits.AutoSize = true;
            this.check_Limits.BackColor = System.Drawing.Color.Transparent;
            this.check_Limits.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_Limits.ForeColor = System.Drawing.Color.Black;
            this.check_Limits.Location = new System.Drawing.Point(3, 278);
            this.check_Limits.Name = "check_Limits";
            this.check_Limits.Size = new System.Drawing.Size(59, 19);
            this.check_Limits.TabIndex = 12;
            this.check_Limits.Text = "Limits";
            this.check_Limits.UseVisualStyleBackColor = false;
            // 
            // check_MonitoringTime
            // 
            this.check_MonitoringTime.AutoSize = true;
            this.check_MonitoringTime.BackColor = System.Drawing.Color.Transparent;
            this.check_MonitoringTime.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_MonitoringTime.ForeColor = System.Drawing.Color.Black;
            this.check_MonitoringTime.Location = new System.Drawing.Point(191, 53);
            this.check_MonitoringTime.Name = "check_MonitoringTime";
            this.check_MonitoringTime.Size = new System.Drawing.Size(117, 19);
            this.check_MonitoringTime.TabIndex = 15;
            this.check_MonitoringTime.Text = "Monitoring Time";
            this.check_MonitoringTime.UseVisualStyleBackColor = false;
            // 
            // check_DecimalPoint
            // 
            this.check_DecimalPoint.AutoSize = true;
            this.check_DecimalPoint.BackColor = System.Drawing.Color.Transparent;
            this.check_DecimalPoint.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_DecimalPoint.ForeColor = System.Drawing.Color.Black;
            this.check_DecimalPoint.Location = new System.Drawing.Point(3, 128);
            this.check_DecimalPoint.Name = "check_DecimalPoint";
            this.check_DecimalPoint.Size = new System.Drawing.Size(101, 19);
            this.check_DecimalPoint.TabIndex = 5;
            this.check_DecimalPoint.Text = "Decimal Point";
            this.check_DecimalPoint.UseVisualStyleBackColor = false;
            // 
            // check_EnergyParams
            // 
            this.check_EnergyParams.AutoSize = true;
            this.check_EnergyParams.BackColor = System.Drawing.Color.Transparent;
            this.check_EnergyParams.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_EnergyParams.ForeColor = System.Drawing.Color.Black;
            this.check_EnergyParams.Location = new System.Drawing.Point(3, 253);
            this.check_EnergyParams.Name = "check_EnergyParams";
            this.check_EnergyParams.Size = new System.Drawing.Size(106, 19);
            this.check_EnergyParams.TabIndex = 6;
            this.check_EnergyParams.Text = "Energy Params";
            this.check_EnergyParams.UseVisualStyleBackColor = false;
            // 
            // check_EventCaution
            // 
            this.check_EventCaution.AutoSize = true;
            this.check_EventCaution.BackColor = System.Drawing.Color.Transparent;
            this.check_EventCaution.Enabled = false;
            this.check_EventCaution.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_EventCaution.ForeColor = System.Drawing.Color.Black;
            this.check_EventCaution.Location = new System.Drawing.Point(191, 153);
            this.check_EventCaution.Name = "check_EventCaution";
            this.check_EventCaution.Size = new System.Drawing.Size(102, 19);
            this.check_EventCaution.TabIndex = 7;
            this.check_EventCaution.Text = "Event Caution";
            this.check_EventCaution.UseVisualStyleBackColor = false;
            this.check_EventCaution.Visible = false;
            // 
            // check_IPV4
            // 
            this.check_IPV4.AutoSize = true;
            this.check_IPV4.BackColor = System.Drawing.Color.Transparent;
            this.check_IPV4.Enabled = false;
            this.check_IPV4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_IPV4.ForeColor = System.Drawing.Color.Black;
            this.check_IPV4.Location = new System.Drawing.Point(191, 228);
            this.check_IPV4.Name = "check_IPV4";
            this.check_IPV4.Size = new System.Drawing.Size(51, 19);
            this.check_IPV4.TabIndex = 9;
            this.check_IPV4.Text = "IPV4";
            this.check_IPV4.UseVisualStyleBackColor = false;
            this.check_IPV4.Visible = false;
            // 
            // btn_CustomSelectAll
            // 
            this.btn_CustomSelectAll.Location = new System.Drawing.Point(213, 455);
            this.btn_CustomSelectAll.Name = "btn_CustomSelectAll";
            this.btn_CustomSelectAll.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_CustomSelectAll.Size = new System.Drawing.Size(70, 24);
            this.btn_CustomSelectAll.TabIndex = 22;
            this.btn_CustomSelectAll.Values.Text = "Select All";
            this.btn_CustomSelectAll.Click += new System.EventHandler(this.btn_CustomSelectAll_Click);
            // 
            // btn_CustomDeselect
            // 
            this.btn_CustomDeselect.Location = new System.Drawing.Point(290, 455);
            this.btn_CustomDeselect.Name = "btn_CustomDeselect";
            this.btn_CustomDeselect.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_CustomDeselect.Size = new System.Drawing.Size(58, 24);
            this.btn_CustomDeselect.TabIndex = 1;
            this.btn_CustomDeselect.Values.Text = "Reset All";
            this.btn_CustomDeselect.Click += new System.EventHandler(this.btn_CustomDeselect_Click);
            // 
            // btn_CustomOK
            // 
            this.btn_CustomOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_CustomOK.Location = new System.Drawing.Point(8, 455);
            this.btn_CustomOK.Name = "btn_CustomOK";
            this.btn_CustomOK.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_CustomOK.Size = new System.Drawing.Size(58, 24);
            this.btn_CustomOK.TabIndex = 1;
            this.btn_CustomOK.Values.Text = "OK";
            // 
            // btn_CustomCancel
            // 
            this.btn_CustomCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_CustomCancel.Location = new System.Drawing.Point(78, 455);
            this.btn_CustomCancel.Name = "btn_CustomCancel";
            this.btn_CustomCancel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_CustomCancel.Size = new System.Drawing.Size(58, 24);
            this.btn_CustomCancel.TabIndex = 1;
            this.btn_CustomCancel.Values.Text = "Cancel";
            this.btn_CustomCancel.Click += new System.EventHandler(this.btn_CustomSelectAll_Click);
            // 
            // check_TCPUDP
            // 
            this.check_TCPUDP.AutoSize = true;
            this.check_TCPUDP.BackColor = System.Drawing.Color.Transparent;
            this.check_TCPUDP.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_TCPUDP.ForeColor = System.Drawing.Color.Black;
            this.check_TCPUDP.Location = new System.Drawing.Point(191, 178);
            this.check_TCPUDP.Name = "check_TCPUDP";
            this.check_TCPUDP.Size = new System.Drawing.Size(74, 19);
            this.check_TCPUDP.TabIndex = 8;
            this.check_TCPUDP.Text = "TCP/UDP";
            this.check_TCPUDP.UseVisualStyleBackColor = false;
            this.check_TCPUDP.Visible = false;
            // 
            // check_MDI_params
            // 
            this.check_MDI_params.AutoSize = true;
            this.check_MDI_params.BackColor = System.Drawing.Color.Transparent;
            this.check_MDI_params.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_MDI_params.ForeColor = System.Drawing.Color.Black;
            this.check_MDI_params.Location = new System.Drawing.Point(191, 28);
            this.check_MDI_params.Name = "check_MDI_params";
            this.check_MDI_params.Size = new System.Drawing.Size(133, 19);
            this.check_MDI_params.TabIndex = 13;
            this.check_MDI_params.Text = "MDI Params              ";
            this.check_MDI_params.UseVisualStyleBackColor = false;
            // 
            // check_DisplayWindows_Nor
            // 
            this.check_DisplayWindows_Nor.AutoSize = true;
            this.check_DisplayWindows_Nor.BackColor = System.Drawing.Color.Transparent;
            this.check_DisplayWindows_Nor.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_DisplayWindows_Nor.ForeColor = System.Drawing.Color.Black;
            this.check_DisplayWindows_Nor.Location = new System.Drawing.Point(3, 153);
            this.check_DisplayWindows_Nor.Name = "check_DisplayWindows_Nor";
            this.check_DisplayWindows_Nor.Size = new System.Drawing.Size(169, 19);
            this.check_DisplayWindows_Nor.TabIndex = 19;
            this.check_DisplayWindows_Nor.Text = "Display Windows - Normal";
            this.check_DisplayWindows_Nor.UseVisualStyleBackColor = false;
            // 
            // check_ActivityCalender
            // 
            this.check_ActivityCalender.AutoSize = true;
            this.check_ActivityCalender.BackColor = System.Drawing.Color.Transparent;
            this.check_ActivityCalender.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_ActivityCalender.ForeColor = System.Drawing.Color.Black;
            this.check_ActivityCalender.Location = new System.Drawing.Point(3, 3);
            this.check_ActivityCalender.Name = "check_ActivityCalender";
            this.check_ActivityCalender.Size = new System.Drawing.Size(119, 19);
            this.check_ActivityCalender.TabIndex = 9;
            this.check_ActivityCalender.Text = "Activity Calender";
            this.check_ActivityCalender.UseVisualStyleBackColor = false;
            // 
            // check_DataProfilewithEvents
            // 
            this.check_DataProfilewithEvents.AutoSize = true;
            this.check_DataProfilewithEvents.BackColor = System.Drawing.Color.Transparent;
            this.check_DataProfilewithEvents.Enabled = false;
            this.check_DataProfilewithEvents.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_DataProfilewithEvents.ForeColor = System.Drawing.Color.Black;
            this.check_DataProfilewithEvents.Location = new System.Drawing.Point(191, 203);
            this.check_DataProfilewithEvents.Name = "check_DataProfilewithEvents";
            this.check_DataProfilewithEvents.Size = new System.Drawing.Size(157, 19);
            this.check_DataProfilewithEvents.TabIndex = 19;
            this.check_DataProfilewithEvents.Text = "Data Profile with Events";
            this.check_DataProfilewithEvents.UseVisualStyleBackColor = false;
            this.check_DataProfilewithEvents.Visible = false;
            // 
            // check_MajorAlarmprofile
            // 
            this.check_MajorAlarmprofile.AutoSize = true;
            this.check_MajorAlarmprofile.BackColor = System.Drawing.Color.Transparent;
            this.check_MajorAlarmprofile.Enabled = false;
            this.check_MajorAlarmprofile.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_MajorAlarmprofile.ForeColor = System.Drawing.Color.Black;
            this.check_MajorAlarmprofile.Location = new System.Drawing.Point(191, 253);
            this.check_MajorAlarmprofile.Name = "check_MajorAlarmprofile";
            this.check_MajorAlarmprofile.Size = new System.Drawing.Size(133, 19);
            this.check_MajorAlarmprofile.TabIndex = 9;
            this.check_MajorAlarmprofile.Text = "Major Alarm profile";
            this.check_MajorAlarmprofile.UseVisualStyleBackColor = false;
            this.check_MajorAlarmprofile.Visible = false;
            // 
            // gbMeterSetting
            // 
            this.gbMeterSetting.BackColor = System.Drawing.Color.Transparent;
            this.gbMeterSetting.Controls.Add(this.fLP_MeterSettings);
            this.gbMeterSetting.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbMeterSetting.ForeColor = System.Drawing.Color.Maroon;
            this.gbMeterSetting.Location = new System.Drawing.Point(11, 5);
            this.gbMeterSetting.Name = "gbMeterSetting";
            this.gbMeterSetting.Size = new System.Drawing.Size(399, 444);
            this.gbMeterSetting.TabIndex = 25;
            this.gbMeterSetting.TabStop = false;
            this.gbMeterSetting.Text = "Meter Settings";
            // 
            // fLP_MeterSettings
            // 
            this.fLP_MeterSettings.Controls.Add(this.check_ActivityCalender);
            this.fLP_MeterSettings.Controls.Add(this.check_CTPT);
            this.fLP_MeterSettings.Controls.Add(this.check_Clock);
            this.fLP_MeterSettings.Controls.Add(this.check_Contactor);
            this.fLP_MeterSettings.Controls.Add(this.check_customerReference);
            this.fLP_MeterSettings.Controls.Add(this.check_DecimalPoint);
            this.fLP_MeterSettings.Controls.Add(this.check_DisplayWindows_Nor);
            this.fLP_MeterSettings.Controls.Add(this.check_DisplayWindows_Alt);
            this.fLP_MeterSettings.Controls.Add(this.check_DisplayWindows_test);
            this.fLP_MeterSettings.Controls.Add(this.check_DisplayPowerDown);
            this.fLP_MeterSettings.Controls.Add(this.check_EnergyParams);
            this.fLP_MeterSettings.Controls.Add(this.check_Limits);
            this.fLP_MeterSettings.Controls.Add(this.chk_PQ_LoadProfileInterval);
            this.fLP_MeterSettings.Controls.Add(this.chk_LoadProfile_2_Interval);
            this.fLP_MeterSettings.Controls.Add(this.chk_LoadProfile_2);
            this.fLP_MeterSettings.Controls.Add(this.chk_LoadProfile_Interval);
            this.fLP_MeterSettings.Controls.Add(this.chk_LoadProfile);
            this.fLP_MeterSettings.Controls.Add(this.check_MDI_params);
            this.fLP_MeterSettings.Controls.Add(this.check_MonitoringTime);
            this.fLP_MeterSettings.Controls.Add(this.check_Password_Elec);
            this.fLP_MeterSettings.Controls.Add(this.chbStatusWordMap1);
            this.fLP_MeterSettings.Controls.Add(this.chbStatusWordMap2);
            this.fLP_MeterSettings.Controls.Add(this.check_EventCaution);
            this.fLP_MeterSettings.Controls.Add(this.check_TCPUDP);
            this.fLP_MeterSettings.Controls.Add(this.check_DataProfilewithEvents);
            this.fLP_MeterSettings.Controls.Add(this.check_IPV4);
            this.fLP_MeterSettings.Controls.Add(this.check_MajorAlarmprofile);
            this.fLP_MeterSettings.Controls.Add(this.check_loadShedding);
            this.fLP_MeterSettings.Controls.Add(this.check_GeneratorStart);
            this.fLP_MeterSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fLP_MeterSettings.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLP_MeterSettings.ForeColor = System.Drawing.Color.Black;
            this.fLP_MeterSettings.Location = new System.Drawing.Point(3, 19);
            this.fLP_MeterSettings.Name = "fLP_MeterSettings";
            this.fLP_MeterSettings.Size = new System.Drawing.Size(393, 422);
            this.fLP_MeterSettings.TabIndex = 26;
            // 
            // check_DisplayWindows_Alt
            // 
            this.check_DisplayWindows_Alt.AutoSize = true;
            this.check_DisplayWindows_Alt.BackColor = System.Drawing.Color.Transparent;
            this.check_DisplayWindows_Alt.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_DisplayWindows_Alt.ForeColor = System.Drawing.Color.Black;
            this.check_DisplayWindows_Alt.Location = new System.Drawing.Point(3, 178);
            this.check_DisplayWindows_Alt.Name = "check_DisplayWindows_Alt";
            this.check_DisplayWindows_Alt.Size = new System.Drawing.Size(181, 19);
            this.check_DisplayWindows_Alt.TabIndex = 19;
            this.check_DisplayWindows_Alt.Text = "Display Windows - Alternate";
            this.check_DisplayWindows_Alt.UseVisualStyleBackColor = false;
            // 
            // check_DisplayWindows_test
            // 
            this.check_DisplayWindows_test.AutoSize = true;
            this.check_DisplayWindows_test.BackColor = System.Drawing.Color.Transparent;
            this.check_DisplayWindows_test.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_DisplayWindows_test.ForeColor = System.Drawing.Color.Black;
            this.check_DisplayWindows_test.Location = new System.Drawing.Point(3, 203);
            this.check_DisplayWindows_test.Name = "check_DisplayWindows_test";
            this.check_DisplayWindows_test.Size = new System.Drawing.Size(182, 19);
            this.check_DisplayWindows_test.TabIndex = 19;
            this.check_DisplayWindows_test.Text = "Display Windows - TestMode";
            this.check_DisplayWindows_test.UseVisualStyleBackColor = false;
            // 
            // check_DisplayPowerDown
            // 
            this.check_DisplayPowerDown.AutoSize = true;
            this.check_DisplayPowerDown.BackColor = System.Drawing.Color.Transparent;
            this.check_DisplayPowerDown.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_DisplayPowerDown.ForeColor = System.Drawing.Color.Black;
            this.check_DisplayPowerDown.Location = new System.Drawing.Point(3, 228);
            this.check_DisplayPowerDown.Name = "check_DisplayPowerDown";
            this.check_DisplayPowerDown.Size = new System.Drawing.Size(138, 19);
            this.check_DisplayPowerDown.TabIndex = 25;
            this.check_DisplayPowerDown.Text = "Display Power Down";
            this.check_DisplayPowerDown.UseVisualStyleBackColor = false;
            // 
            // chk_PQ_LoadProfileInterval
            // 
            this.chk_PQ_LoadProfileInterval.AutoSize = true;
            this.chk_PQ_LoadProfileInterval.BackColor = System.Drawing.Color.Transparent;
            this.chk_PQ_LoadProfileInterval.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_PQ_LoadProfileInterval.ForeColor = System.Drawing.Color.Black;
            this.chk_PQ_LoadProfileInterval.Location = new System.Drawing.Point(3, 303);
            this.chk_PQ_LoadProfileInterval.Name = "chk_PQ_LoadProfileInterval";
            this.chk_PQ_LoadProfileInterval.Size = new System.Drawing.Size(155, 19);
            this.chk_PQ_LoadProfileInterval.TabIndex = 37;
            this.chk_PQ_LoadProfileInterval.Text = "PQ Load Profile Interval";
            this.chk_PQ_LoadProfileInterval.UseVisualStyleBackColor = false;
            // 
            // chk_LoadProfile_2_Interval
            // 
            this.chk_LoadProfile_2_Interval.AutoSize = true;
            this.chk_LoadProfile_2_Interval.BackColor = System.Drawing.Color.Transparent;
            this.chk_LoadProfile_2_Interval.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_LoadProfile_2_Interval.ForeColor = System.Drawing.Color.Black;
            this.chk_LoadProfile_2_Interval.Location = new System.Drawing.Point(3, 328);
            this.chk_LoadProfile_2_Interval.Name = "chk_LoadProfile_2_Interval";
            this.chk_LoadProfile_2_Interval.Size = new System.Drawing.Size(149, 19);
            this.chk_LoadProfile_2_Interval.TabIndex = 36;
            this.chk_LoadProfile_2_Interval.Text = "Load Profile_2 Interval";
            this.chk_LoadProfile_2_Interval.UseVisualStyleBackColor = false;
            // 
            // chk_LoadProfile_2
            // 
            this.chk_LoadProfile_2.AutoSize = true;
            this.chk_LoadProfile_2.BackColor = System.Drawing.Color.Transparent;
            this.chk_LoadProfile_2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_LoadProfile_2.ForeColor = System.Drawing.Color.Black;
            this.chk_LoadProfile_2.Location = new System.Drawing.Point(3, 353);
            this.chk_LoadProfile_2.Name = "chk_LoadProfile_2";
            this.chk_LoadProfile_2.Size = new System.Drawing.Size(156, 19);
            this.chk_LoadProfile_2.TabIndex = 35;
            this.chk_LoadProfile_2.Text = "Load Profile_2 Channels";
            this.chk_LoadProfile_2.UseVisualStyleBackColor = false;
            // 
            // chk_LoadProfile_Interval
            // 
            this.chk_LoadProfile_Interval.AutoSize = true;
            this.chk_LoadProfile_Interval.BackColor = System.Drawing.Color.Transparent;
            this.chk_LoadProfile_Interval.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_LoadProfile_Interval.ForeColor = System.Drawing.Color.Black;
            this.chk_LoadProfile_Interval.Location = new System.Drawing.Point(3, 378);
            this.chk_LoadProfile_Interval.Name = "chk_LoadProfile_Interval";
            this.chk_LoadProfile_Interval.Size = new System.Drawing.Size(136, 19);
            this.chk_LoadProfile_Interval.TabIndex = 33;
            this.chk_LoadProfile_Interval.Text = "Load Profile Interval";
            this.chk_LoadProfile_Interval.UseVisualStyleBackColor = false;
            // 
            // chk_LoadProfile
            // 
            this.chk_LoadProfile.AutoSize = true;
            this.chk_LoadProfile.BackColor = System.Drawing.Color.Transparent;
            this.chk_LoadProfile.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_LoadProfile.ForeColor = System.Drawing.Color.Black;
            this.chk_LoadProfile.Location = new System.Drawing.Point(191, 3);
            this.chk_LoadProfile.Name = "chk_LoadProfile";
            this.chk_LoadProfile.Size = new System.Drawing.Size(143, 19);
            this.chk_LoadProfile.TabIndex = 34;
            this.chk_LoadProfile.Text = "Load Profile Channels";
            this.chk_LoadProfile.UseVisualStyleBackColor = false;
            // 
            // check_Password_Elec
            // 
            this.check_Password_Elec.AutoSize = true;
            this.check_Password_Elec.BackColor = System.Drawing.Color.Transparent;
            this.check_Password_Elec.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_Password_Elec.ForeColor = System.Drawing.Color.Black;
            this.check_Password_Elec.Location = new System.Drawing.Point(191, 78);
            this.check_Password_Elec.Name = "check_Password_Elec";
            this.check_Password_Elec.Size = new System.Drawing.Size(188, 19);
            this.check_Password_Elec.TabIndex = 24;
            this.check_Password_Elec.Text = "Current Association Password";
            this.check_Password_Elec.UseVisualStyleBackColor = false;
            // 
            // chbStatusWordMap1
            // 
            this.chbStatusWordMap1.AutoSize = true;
            this.chbStatusWordMap1.BackColor = System.Drawing.Color.Transparent;
            this.chbStatusWordMap1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbStatusWordMap1.ForeColor = System.Drawing.Color.Black;
            this.chbStatusWordMap1.Location = new System.Drawing.Point(191, 103);
            this.chbStatusWordMap1.Name = "chbStatusWordMap1";
            this.chbStatusWordMap1.Size = new System.Drawing.Size(130, 19);
            this.chbStatusWordMap1.TabIndex = 38;
            this.chbStatusWordMap1.Text = "Status Word Map 1";
            this.chbStatusWordMap1.UseVisualStyleBackColor = false;
            // 
            // chbStatusWordMap2
            // 
            this.chbStatusWordMap2.AutoSize = true;
            this.chbStatusWordMap2.BackColor = System.Drawing.Color.Transparent;
            this.chbStatusWordMap2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbStatusWordMap2.ForeColor = System.Drawing.Color.Black;
            this.chbStatusWordMap2.Location = new System.Drawing.Point(191, 128);
            this.chbStatusWordMap2.Name = "chbStatusWordMap2";
            this.chbStatusWordMap2.Size = new System.Drawing.Size(130, 19);
            this.chbStatusWordMap2.TabIndex = 39;
            this.chbStatusWordMap2.Text = "Status Word Map 2";
            this.chbStatusWordMap2.UseVisualStyleBackColor = false;
            // 
            // check_loadShedding
            // 
            this.check_loadShedding.AutoSize = true;
            this.check_loadShedding.BackColor = System.Drawing.Color.Transparent;
            this.check_loadShedding.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_loadShedding.ForeColor = System.Drawing.Color.Black;
            this.check_loadShedding.Location = new System.Drawing.Point(191, 278);
            this.check_loadShedding.Name = "check_loadShedding";
            this.check_loadShedding.Size = new System.Drawing.Size(105, 19);
            this.check_loadShedding.TabIndex = 9;
            this.check_loadShedding.Text = "Load Shedding";
            this.check_loadShedding.UseVisualStyleBackColor = false;
            // 
            // check_GeneratorStart
            // 
            this.check_GeneratorStart.AutoSize = true;
            this.check_GeneratorStart.BackColor = System.Drawing.Color.Transparent;
            this.check_GeneratorStart.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_GeneratorStart.ForeColor = System.Drawing.Color.Black;
            this.check_GeneratorStart.Location = new System.Drawing.Point(191, 303);
            this.check_GeneratorStart.Name = "check_GeneratorStart";
            this.check_GeneratorStart.Size = new System.Drawing.Size(113, 19);
            this.check_GeneratorStart.TabIndex = 9;
            this.check_GeneratorStart.Text = "Generator Start";
            this.check_GeneratorStart.UseVisualStyleBackColor = false;
            // 
            // check_Time
            // 
            this.check_Time.AutoSize = true;
            this.check_Time.BackColor = System.Drawing.Color.Transparent;
            this.check_Time.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_Time.ForeColor = System.Drawing.Color.Black;
            this.check_Time.Location = new System.Drawing.Point(69, 83);
            this.check_Time.Name = "check_Time";
            this.check_Time.Size = new System.Drawing.Size(53, 19);
            this.check_Time.TabIndex = 8;
            this.check_Time.Text = "Time";
            this.check_Time.UseVisualStyleBackColor = false;
            // 
            // chk_DisplayPowerDown
            // 
            this.chk_DisplayPowerDown.AutoSize = true;
            this.chk_DisplayPowerDown.BackColor = System.Drawing.Color.Transparent;
            this.chk_DisplayPowerDown.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_DisplayPowerDown.ForeColor = System.Drawing.Color.Black;
            this.chk_DisplayPowerDown.Location = new System.Drawing.Point(0, 0);
            this.chk_DisplayPowerDown.Name = "chk_DisplayPowerDown";
            this.chk_DisplayPowerDown.Size = new System.Drawing.Size(138, 19);
            this.chk_DisplayPowerDown.TabIndex = 25;
            this.chk_DisplayPowerDown.Text = "Display Power Down";
            this.chk_DisplayPowerDown.UseVisualStyleBackColor = false;
            // 
            // gbTBESetting
            // 
            this.gbTBESetting.BackColor = System.Drawing.Color.Transparent;
            this.gbTBESetting.Controls.Add(this.flpTimeEvents);
            this.gbTBESetting.Controls.Add(this.check_Time);
            this.gbTBESetting.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTBESetting.ForeColor = System.Drawing.Color.Maroon;
            this.gbTBESetting.Location = new System.Drawing.Point(416, 315);
            this.gbTBESetting.Name = "gbTBESetting";
            this.gbTBESetting.Size = new System.Drawing.Size(176, 59);
            this.gbTBESetting.TabIndex = 26;
            this.gbTBESetting.TabStop = false;
            this.gbTBESetting.Text = "Time Events";
            // 
            // flpTimeEvents
            // 
            this.flpTimeEvents.Controls.Add(this.check_TBEs);
            this.flpTimeEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTimeEvents.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpTimeEvents.Location = new System.Drawing.Point(3, 19);
            this.flpTimeEvents.Name = "flpTimeEvents";
            this.flpTimeEvents.Size = new System.Drawing.Size(170, 37);
            this.flpTimeEvents.TabIndex = 31;
            // 
            // check_TBEs
            // 
            this.check_TBEs.AutoSize = true;
            this.check_TBEs.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_TBEs.ForeColor = System.Drawing.Color.Black;
            this.check_TBEs.Location = new System.Drawing.Point(3, 3);
            this.check_TBEs.Name = "check_TBEs";
            this.check_TBEs.Size = new System.Drawing.Size(127, 19);
            this.check_TBEs.TabIndex = 7;
            this.check_TBEs.Text = "Time Based Events";
            this.check_TBEs.UseVisualStyleBackColor = true;
            // 
            // chk_GPP
            // 
            this.chk_GPP.AutoSize = true;
            this.chk_GPP.BackColor = System.Drawing.Color.Transparent;
            this.chk_GPP.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_GPP.ForeColor = System.Drawing.Color.Black;
            this.chk_GPP.Location = new System.Drawing.Point(0, 0);
            this.chk_GPP.Name = "chk_GPP";
            this.chk_GPP.Size = new System.Drawing.Size(46, 19);
            this.chk_GPP.TabIndex = 26;
            this.chk_GPP.Text = "SVS";
            this.chk_GPP.UseVisualStyleBackColor = false;
            // 
            // gbSecurityKeys
            // 
            this.gbSecurityKeys.BackColor = System.Drawing.Color.Transparent;
            this.gbSecurityKeys.Controls.Add(this.flpSecurityKeys);
            this.gbSecurityKeys.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSecurityKeys.ForeColor = System.Drawing.Color.Maroon;
            this.gbSecurityKeys.Location = new System.Drawing.Point(416, 374);
            this.gbSecurityKeys.Name = "gbSecurityKeys";
            this.gbSecurityKeys.Size = new System.Drawing.Size(176, 101);
            this.gbSecurityKeys.TabIndex = 28;
            this.gbSecurityKeys.TabStop = false;
            this.gbSecurityKeys.Text = "Security Keys";
            // 
            // flpSecurityKeys
            // 
            this.flpSecurityKeys.Controls.Add(this.check_WriteAuthenticationKey);
            this.flpSecurityKeys.Controls.Add(this.check_EncryptionKey);
            this.flpSecurityKeys.Controls.Add(this.check_SecurityPolicy);
            this.flpSecurityKeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpSecurityKeys.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpSecurityKeys.Location = new System.Drawing.Point(3, 19);
            this.flpSecurityKeys.Name = "flpSecurityKeys";
            this.flpSecurityKeys.Size = new System.Drawing.Size(170, 79);
            this.flpSecurityKeys.TabIndex = 31;
            // 
            // check_WriteAuthenticationKey
            // 
            this.check_WriteAuthenticationKey.AutoSize = true;
            this.check_WriteAuthenticationKey.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_WriteAuthenticationKey.ForeColor = System.Drawing.Color.Black;
            this.check_WriteAuthenticationKey.Location = new System.Drawing.Point(3, 3);
            this.check_WriteAuthenticationKey.Name = "check_WriteAuthenticationKey";
            this.check_WriteAuthenticationKey.Size = new System.Drawing.Size(131, 19);
            this.check_WriteAuthenticationKey.TabIndex = 7;
            this.check_WriteAuthenticationKey.Text = "Authentication Key";
            this.check_WriteAuthenticationKey.UseVisualStyleBackColor = true;
            // 
            // check_EncryptionKey
            // 
            this.check_EncryptionKey.AutoSize = true;
            this.check_EncryptionKey.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_EncryptionKey.ForeColor = System.Drawing.Color.Black;
            this.check_EncryptionKey.Location = new System.Drawing.Point(3, 28);
            this.check_EncryptionKey.Name = "check_EncryptionKey";
            this.check_EncryptionKey.Size = new System.Drawing.Size(107, 19);
            this.check_EncryptionKey.TabIndex = 7;
            this.check_EncryptionKey.Text = "Encryption Key";
            this.check_EncryptionKey.UseVisualStyleBackColor = true;
            // 
            // check_SecurityPolicy
            // 
            this.check_SecurityPolicy.AutoSize = true;
            this.check_SecurityPolicy.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_SecurityPolicy.ForeColor = System.Drawing.Color.Black;
            this.check_SecurityPolicy.Location = new System.Drawing.Point(3, 53);
            this.check_SecurityPolicy.Name = "check_SecurityPolicy";
            this.check_SecurityPolicy.Size = new System.Drawing.Size(104, 19);
            this.check_SecurityPolicy.TabIndex = 8;
            this.check_SecurityPolicy.Text = "Security Policy";
            this.check_SecurityPolicy.UseVisualStyleBackColor = true;
            // 
            // gbStandardModem
            // 
            this.gbStandardModem.BackColor = System.Drawing.Color.Transparent;
            this.gbStandardModem.Controls.Add(this.flpStandardModem);
            this.gbStandardModem.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbStandardModem.ForeColor = System.Drawing.Color.Maroon;
            this.gbStandardModem.Location = new System.Drawing.Point(416, 206);
            this.gbStandardModem.Name = "gbStandardModem";
            this.gbStandardModem.Size = new System.Drawing.Size(176, 103);
            this.gbStandardModem.TabIndex = 29;
            this.gbStandardModem.TabStop = false;
            this.gbStandardModem.Text = "Standard Modem Settings";
            // 
            // flpStandardModem
            // 
            this.flpStandardModem.Controls.Add(this.check_StandardModem_IP_Profile);
            this.flpStandardModem.Controls.Add(this.check_StandardModem_Number_Profile);
            this.flpStandardModem.Controls.Add(this.check_StandardModem_KeepAlive);
            this.flpStandardModem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpStandardModem.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpStandardModem.Location = new System.Drawing.Point(3, 19);
            this.flpStandardModem.Name = "flpStandardModem";
            this.flpStandardModem.Size = new System.Drawing.Size(170, 81);
            this.flpStandardModem.TabIndex = 31;
            // 
            // check_StandardModem_IP_Profile
            // 
            this.check_StandardModem_IP_Profile.AutoSize = true;
            this.check_StandardModem_IP_Profile.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_StandardModem_IP_Profile.ForeColor = System.Drawing.Color.Black;
            this.check_StandardModem_IP_Profile.Location = new System.Drawing.Point(3, 3);
            this.check_StandardModem_IP_Profile.Name = "check_StandardModem_IP_Profile";
            this.check_StandardModem_IP_Profile.Size = new System.Drawing.Size(75, 19);
            this.check_StandardModem_IP_Profile.TabIndex = 7;
            this.check_StandardModem_IP_Profile.Text = "IP Profile";
            this.check_StandardModem_IP_Profile.UseVisualStyleBackColor = true;
            // 
            // check_StandardModem_Number_Profile
            // 
            this.check_StandardModem_Number_Profile.AutoSize = true;
            this.check_StandardModem_Number_Profile.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_StandardModem_Number_Profile.ForeColor = System.Drawing.Color.Black;
            this.check_StandardModem_Number_Profile.Location = new System.Drawing.Point(3, 28);
            this.check_StandardModem_Number_Profile.Name = "check_StandardModem_Number_Profile";
            this.check_StandardModem_Number_Profile.Size = new System.Drawing.Size(111, 19);
            this.check_StandardModem_Number_Profile.TabIndex = 9;
            this.check_StandardModem_Number_Profile.Text = "Number Profile";
            this.check_StandardModem_Number_Profile.UseVisualStyleBackColor = true;
            // 
            // check_StandardModem_KeepAlive
            // 
            this.check_StandardModem_KeepAlive.AutoSize = true;
            this.check_StandardModem_KeepAlive.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_StandardModem_KeepAlive.ForeColor = System.Drawing.Color.Black;
            this.check_StandardModem_KeepAlive.Location = new System.Drawing.Point(3, 53);
            this.check_StandardModem_KeepAlive.Name = "check_StandardModem_KeepAlive";
            this.check_StandardModem_KeepAlive.Size = new System.Drawing.Size(84, 19);
            this.check_StandardModem_KeepAlive.TabIndex = 10;
            this.check_StandardModem_KeepAlive.Text = "Keep Alive";
            this.check_StandardModem_KeepAlive.UseVisualStyleBackColor = true;
            // 
            // gbModemSetting
            // 
            this.gbModemSetting.BackColor = System.Drawing.Color.Transparent;
            this.gbModemSetting.Controls.Add(this.flp_ModemSettings);
            this.gbModemSetting.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbModemSetting.ForeColor = System.Drawing.Color.Maroon;
            this.gbModemSetting.Location = new System.Drawing.Point(417, 5);
            this.gbModemSetting.Name = "gbModemSetting";
            this.gbModemSetting.Size = new System.Drawing.Size(175, 198);
            this.gbModemSetting.TabIndex = 30;
            this.gbModemSetting.TabStop = false;
            this.gbModemSetting.Text = "Modem Settings";
            // 
            // flp_ModemSettings
            // 
            this.flp_ModemSettings.Controls.Add(this.check_IP_Profile);
            this.flp_ModemSettings.Controls.Add(this.chbWakeupProfile);
            this.flp_ModemSettings.Controls.Add(this.chbKeepAlive);
            this.flp_ModemSettings.Controls.Add(this.chbNumberProfile);
            this.flp_ModemSettings.Controls.Add(this.chbModemLimitsAndTime);
            this.flp_ModemSettings.Controls.Add(this.chbModemInitialize);
            this.flp_ModemSettings.Controls.Add(this.chbCommunicationProfile);
            this.flp_ModemSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_ModemSettings.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flp_ModemSettings.Location = new System.Drawing.Point(3, 19);
            this.flp_ModemSettings.Name = "flp_ModemSettings";
            this.flp_ModemSettings.Size = new System.Drawing.Size(169, 176);
            this.flp_ModemSettings.TabIndex = 11;
            // 
            // check_IP_Profile
            // 
            this.check_IP_Profile.AutoSize = true;
            this.check_IP_Profile.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_IP_Profile.ForeColor = System.Drawing.Color.Black;
            this.check_IP_Profile.Location = new System.Drawing.Point(3, 3);
            this.check_IP_Profile.Name = "check_IP_Profile";
            this.check_IP_Profile.Size = new System.Drawing.Size(75, 19);
            this.check_IP_Profile.TabIndex = 7;
            this.check_IP_Profile.Text = "IP Profile";
            this.check_IP_Profile.UseVisualStyleBackColor = true;
            // 
            // chbWakeupProfile
            // 
            this.chbWakeupProfile.AutoSize = true;
            this.chbWakeupProfile.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbWakeupProfile.ForeColor = System.Drawing.Color.Black;
            this.chbWakeupProfile.Location = new System.Drawing.Point(3, 28);
            this.chbWakeupProfile.Name = "chbWakeupProfile";
            this.chbWakeupProfile.Size = new System.Drawing.Size(111, 19);
            this.chbWakeupProfile.TabIndex = 8;
            this.chbWakeupProfile.Text = "WakeUp Profile";
            this.chbWakeupProfile.UseVisualStyleBackColor = true;
            // 
            // chbKeepAlive
            // 
            this.chbKeepAlive.AutoSize = true;
            this.chbKeepAlive.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbKeepAlive.ForeColor = System.Drawing.Color.Black;
            this.chbKeepAlive.Location = new System.Drawing.Point(3, 53);
            this.chbKeepAlive.Name = "chbKeepAlive";
            this.chbKeepAlive.Size = new System.Drawing.Size(84, 19);
            this.chbKeepAlive.TabIndex = 10;
            this.chbKeepAlive.Text = "Keep Alive";
            this.chbKeepAlive.UseVisualStyleBackColor = true;
            // 
            // chbNumberProfile
            // 
            this.chbNumberProfile.AutoSize = true;
            this.chbNumberProfile.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbNumberProfile.ForeColor = System.Drawing.Color.Black;
            this.chbNumberProfile.Location = new System.Drawing.Point(3, 78);
            this.chbNumberProfile.Name = "chbNumberProfile";
            this.chbNumberProfile.Size = new System.Drawing.Size(111, 19);
            this.chbNumberProfile.TabIndex = 9;
            this.chbNumberProfile.Text = "Number Profile";
            this.chbNumberProfile.UseVisualStyleBackColor = true;
            // 
            // chbModemLimitsAndTime
            // 
            this.chbModemLimitsAndTime.AutoSize = true;
            this.chbModemLimitsAndTime.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbModemLimitsAndTime.ForeColor = System.Drawing.Color.Black;
            this.chbModemLimitsAndTime.Location = new System.Drawing.Point(3, 103);
            this.chbModemLimitsAndTime.Name = "chbModemLimitsAndTime";
            this.chbModemLimitsAndTime.Size = new System.Drawing.Size(158, 19);
            this.chbModemLimitsAndTime.TabIndex = 11;
            this.chbModemLimitsAndTime.Text = "Modem Limits and Time";
            this.chbModemLimitsAndTime.UseVisualStyleBackColor = true;
            // 
            // chbModemInitialize
            // 
            this.chbModemInitialize.AutoSize = true;
            this.chbModemInitialize.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbModemInitialize.ForeColor = System.Drawing.Color.Black;
            this.chbModemInitialize.Location = new System.Drawing.Point(3, 128);
            this.chbModemInitialize.Name = "chbModemInitialize";
            this.chbModemInitialize.Size = new System.Drawing.Size(117, 19);
            this.chbModemInitialize.TabIndex = 12;
            this.chbModemInitialize.Text = "Modem Initialize";
            this.chbModemInitialize.UseVisualStyleBackColor = true;
            // 
            // chbCommunicationProfile
            // 
            this.chbCommunicationProfile.AutoSize = true;
            this.chbCommunicationProfile.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbCommunicationProfile.ForeColor = System.Drawing.Color.Black;
            this.chbCommunicationProfile.Location = new System.Drawing.Point(3, 153);
            this.chbCommunicationProfile.Name = "chbCommunicationProfile";
            this.chbCommunicationProfile.Size = new System.Drawing.Size(151, 19);
            this.chbCommunicationProfile.TabIndex = 13;
            this.chbCommunicationProfile.Text = "Communication Profile";
            this.chbCommunicationProfile.UseVisualStyleBackColor = true;
            // 
            // CustomSetGet
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Dialog;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(599, 491);
            this.Controls.Add(this.gbModemSetting);
            this.Controls.Add(this.gbStandardModem);
            this.Controls.Add(this.gbSecurityKeys);
            this.Controls.Add(this.gbTBESetting);
            this.Controls.Add(this.gbMeterSetting);
            this.Controls.Add(this.btn_CustomDeselect);
            this.Controls.Add(this.btn_CustomCancel);
            this.Controls.Add(this.btn_CustomOK);
            this.Controls.Add(this.btn_CustomSelectAll);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomSetGet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CustomSetGet";
            this.Load += new System.EventHandler(this.CustomSetGet_Load);
            this.gbMeterSetting.ResumeLayout(false);
            this.fLP_MeterSettings.ResumeLayout(false);
            this.fLP_MeterSettings.PerformLayout();
            this.gbTBESetting.ResumeLayout(false);
            this.gbTBESetting.PerformLayout();
            this.flpTimeEvents.ResumeLayout(false);
            this.flpTimeEvents.PerformLayout();
            this.gbSecurityKeys.ResumeLayout(false);
            this.flpSecurityKeys.ResumeLayout(false);
            this.flpSecurityKeys.PerformLayout();
            this.gbStandardModem.ResumeLayout(false);
            this.flpStandardModem.ResumeLayout(false);
            this.flpStandardModem.PerformLayout();
            this.gbModemSetting.ResumeLayout(false);
            this.flp_ModemSettings.ResumeLayout(false);
            this.flp_ModemSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        //private System.Windows.Forms.Button btn_CustomSelectAll;
        //private System.Windows.Forms.Button btn_CustomDeselect;
        //private System.Windows.Forms.Button btn_CustomOK;
        //private System.Windows.Forms.Button btn_CustomCancel;

        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_CustomSelectAll;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_CustomDeselect;
        private ComponentFactory.Krypton.Toolkit.KryptonButton  btn_CustomOK;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_CustomCancel;

        public System.Windows.Forms.CheckBox check_CTPT;
        public System.Windows.Forms.CheckBox check_Clock;
        public System.Windows.Forms.CheckBox check_Contactor;
        public System.Windows.Forms.CheckBox check_customerReference;
        public System.Windows.Forms.CheckBox check_Limits;
        public System.Windows.Forms.CheckBox check_MonitoringTime;
        public System.Windows.Forms.CheckBox check_DecimalPoint;
        public System.Windows.Forms.CheckBox check_EnergyParams;
        public System.Windows.Forms.CheckBox check_EventCaution;
        public System.Windows.Forms.CheckBox check_IPV4;
        public System.Windows.Forms.CheckBox check_TCPUDP;
        public System.Windows.Forms.CheckBox check_MDI_params;
        public System.Windows.Forms.CheckBox check_DisplayWindows_Nor;
        public System.Windows.Forms.CheckBox check_ActivityCalender;
        public System.Windows.Forms.CheckBox check_DataProfilewithEvents;
        public System.Windows.Forms.CheckBox check_MajorAlarmprofile;
        private System.Windows.Forms.GroupBox gbMeterSetting;
        public System.Windows.Forms.CheckBox check_DisplayWindows_Alt;
        public CheckBox check_Time;
        public CheckBox check_Password_Elec;
        public CheckBox check_DisplayWindows_test;
        private GroupBox gbTBESetting;
        public CheckBox check_TBEs;
        public CheckBox chk_DisplayPowerDown;
        public CheckBox chk_GPP;
        public CheckBox check_DisplayPowerDown;
        private FlowLayoutPanel fLP_MeterSettings;
        public GroupBox gbSecurityKeys;
        public CheckBox check_SecurityPolicy;
        public CheckBox check_EncryptionKey;
        public CheckBox check_WriteAuthenticationKey;
        public CheckBox chk_PQ_LoadProfileInterval;
        public CheckBox chk_LoadProfile_2_Interval;
        public CheckBox chk_LoadProfile_2;
        public CheckBox chk_LoadProfile_Interval;
        public CheckBox chk_LoadProfile;
        public CheckBox chbStatusWordMap1;
        public CheckBox chbStatusWordMap2;
        private GroupBox gbStandardModem;
        public CheckBox check_StandardModem_KeepAlive;
        public CheckBox check_StandardModem_Number_Profile;
        public CheckBox check_StandardModem_IP_Profile;
        private GroupBox gbModemSetting;
        public CheckBox chbCommunicationProfile;
        public CheckBox chbModemInitialize;
        public CheckBox chbModemLimitsAndTime;
        public CheckBox chbKeepAlive;
        public CheckBox chbNumberProfile;
        public CheckBox chbWakeupProfile;
        public CheckBox check_IP_Profile;
        private FlowLayoutPanel flp_ModemSettings;
        private FlowLayoutPanel flpStandardModem;
        private FlowLayoutPanel flpTimeEvents;
        private FlowLayoutPanel flpSecurityKeys;
        public CheckBox check_loadShedding;
        public CheckBox check_GeneratorStart;
    }
}