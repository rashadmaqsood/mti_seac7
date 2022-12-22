namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    partial class ucParamSinglePhase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucParamSinglePhase));
            this.gpParmSinglePhase = new System.Windows.Forms.GroupBox();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.fLP_Main = new System.Windows.Forms.FlowLayoutPanel();
            this.fLP_first = new System.Windows.Forms.FlowLayoutPanel();
            this.fLP_Mntr_Limit = new System.Windows.Forms.FlowLayoutPanel();
            this.gpMonitoring_Time = new System.Windows.Forms.GroupBox();
            this.fLP_Main_Mntr = new System.Windows.Forms.FlowLayoutPanel();
            this.fLP_PowerFail = new System.Windows.Forms.FlowLayoutPanel();
            this.lblPowerFail = new System.Windows.Forms.Label();
            this.SP_DT_MT_PowerFail = new System.Windows.Forms.DateTimePicker();
            this.SP_btn_Get_MTPowerFail = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.SP_btn_Set_MTPowerFail = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.fLP_Earth = new System.Windows.Forms.FlowLayoutPanel();
            this.lblEarth = new System.Windows.Forms.Label();
            this.SP_DT_MT_Earth = new System.Windows.Forms.DateTimePicker();
            this.SP_btn_Get_MTEarth = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.SP_btn_Set_MTEarth = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.fLP_ReverseEnergy = new System.Windows.Forms.FlowLayoutPanel();
            this.lblReverseEnergy = new System.Windows.Forms.Label();
            this.SP_DT_MT_ReverseEnergy = new System.Windows.Forms.DateTimePicker();
            this.SP_btn_Get_MTReverseEnergy = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.SP_btn_Set_MTReverseEnergy = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.gpLimits = new System.Windows.Forms.GroupBox();
            this.fLP_OverLoadTotal = new System.Windows.Forms.FlowLayoutPanel();
            this.lblOverLoad = new System.Windows.Forms.Label();
            this.txt_SP_overLoadTotal = new System.Windows.Forms.TextBox();
            this.btn_SP_GetOverLoadTotal = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_SP_SetOverLoadTotal = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.gpErrorFlages = new System.Windows.Forms.GroupBox();
            this.SP_btn_getEventString = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.check_RFU2 = new System.Windows.Forms.CheckBox();
            this.check_RFU1 = new System.Windows.Forms.CheckBox();
            this.check_CMMDI = new System.Windows.Forms.CheckBox();
            this.check_BRE = new System.Windows.Forms.CheckBox();
            this.txt_EventDEtail = new System.Windows.Forms.RichTextBox();
            this.gpMDI = new System.Windows.Forms.GroupBox();
            this.ucMDIAutoResetDateParam = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucCustomDateTime();
            this.SP_btn_Set_MDI_AutoResetDate = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.SP_btn_Get_MDI_AutoResetDate = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.gpParmSinglePhase.SuspendLayout();
            this.fLP_Main.SuspendLayout();
            this.fLP_first.SuspendLayout();
            this.fLP_Mntr_Limit.SuspendLayout();
            this.gpMonitoring_Time.SuspendLayout();
            this.fLP_Main_Mntr.SuspendLayout();
            this.fLP_PowerFail.SuspendLayout();
            this.fLP_Earth.SuspendLayout();
            this.fLP_ReverseEnergy.SuspendLayout();
            this.gpLimits.SuspendLayout();
            this.fLP_OverLoadTotal.SuspendLayout();
            this.gpErrorFlages.SuspendLayout();
            this.gpMDI.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpParmSinglePhase
            // 
            this.gpParmSinglePhase.AutoSize = true;
            this.gpParmSinglePhase.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gpParmSinglePhase.Controls.Add(this.progressBar2);
            this.gpParmSinglePhase.Controls.Add(this.fLP_Main);
            this.gpParmSinglePhase.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.gpParmSinglePhase.ForeColor = System.Drawing.Color.Maroon;
            this.gpParmSinglePhase.Location = new System.Drawing.Point(0, 0);
            this.gpParmSinglePhase.Name = "gpParmSinglePhase";
            this.gpParmSinglePhase.Size = new System.Drawing.Size(703, 364);
            this.gpParmSinglePhase.TabIndex = 27;
            this.gpParmSinglePhase.TabStop = false;
            this.gpParmSinglePhase.Text = "Parameter Single Phase";
            this.gpParmSinglePhase.Enter += new System.EventHandler(this.gpParmSinglePhase_Enter);
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(596, 9);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(100, 23);
            this.progressBar2.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar2.TabIndex = 21;
            this.progressBar2.Visible = false;
            // 
            // fLP_Main
            // 
            this.fLP_Main.Controls.Add(this.fLP_first);
            this.fLP_Main.Controls.Add(this.gpMDI);
            this.fLP_Main.Location = new System.Drawing.Point(6, 9);
            this.fLP_Main.Name = "fLP_Main";
            this.fLP_Main.Size = new System.Drawing.Size(691, 333);
            this.fLP_Main.TabIndex = 31;
            // 
            // fLP_first
            // 
            this.fLP_first.AutoSize = true;
            this.fLP_first.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_first.Controls.Add(this.fLP_Mntr_Limit);
            this.fLP_first.Controls.Add(this.gpErrorFlages);
            this.fLP_first.Location = new System.Drawing.Point(3, 3);
            this.fLP_first.Name = "fLP_first";
            this.fLP_first.Size = new System.Drawing.Size(669, 192);
            this.fLP_first.TabIndex = 30;
            // 
            // fLP_Mntr_Limit
            // 
            this.fLP_Mntr_Limit.AutoSize = true;
            this.fLP_Mntr_Limit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_Mntr_Limit.Controls.Add(this.gpMonitoring_Time);
            this.fLP_Mntr_Limit.Controls.Add(this.gpLimits);
            this.fLP_Mntr_Limit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fLP_Mntr_Limit.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLP_Mntr_Limit.Location = new System.Drawing.Point(0, 0);
            this.fLP_Mntr_Limit.Margin = new System.Windows.Forms.Padding(0);
            this.fLP_Mntr_Limit.Name = "fLP_Mntr_Limit";
            this.fLP_Mntr_Limit.Size = new System.Drawing.Size(375, 192);
            this.fLP_Mntr_Limit.TabIndex = 29;
            // 
            // gpMonitoring_Time
            // 
            this.gpMonitoring_Time.AutoSize = true;
            this.gpMonitoring_Time.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gpMonitoring_Time.Controls.Add(this.fLP_Main_Mntr);
            this.gpMonitoring_Time.ForeColor = System.Drawing.Color.Maroon;
            this.gpMonitoring_Time.Location = new System.Drawing.Point(3, 0);
            this.gpMonitoring_Time.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.gpMonitoring_Time.Name = "gpMonitoring_Time";
            this.gpMonitoring_Time.Size = new System.Drawing.Size(369, 125);
            this.gpMonitoring_Time.TabIndex = 23;
            this.gpMonitoring_Time.TabStop = false;
            this.gpMonitoring_Time.Text = "Monitoring Times";
            // 
            // fLP_Main_Mntr
            // 
            this.fLP_Main_Mntr.AutoSize = true;
            this.fLP_Main_Mntr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_Main_Mntr.Controls.Add(this.fLP_PowerFail);
            this.fLP_Main_Mntr.Controls.Add(this.fLP_Earth);
            this.fLP_Main_Mntr.Controls.Add(this.fLP_ReverseEnergy);
            this.fLP_Main_Mntr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fLP_Main_Mntr.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLP_Main_Mntr.Location = new System.Drawing.Point(3, 19);
            this.fLP_Main_Mntr.Name = "fLP_Main_Mntr";
            this.fLP_Main_Mntr.Size = new System.Drawing.Size(363, 103);
            this.fLP_Main_Mntr.TabIndex = 0;
            // 
            // fLP_PowerFail
            // 
            this.fLP_PowerFail.AutoSize = true;
            this.fLP_PowerFail.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_PowerFail.Controls.Add(this.lblPowerFail);
            this.fLP_PowerFail.Controls.Add(this.SP_DT_MT_PowerFail);
            this.fLP_PowerFail.Controls.Add(this.SP_btn_Get_MTPowerFail);
            this.fLP_PowerFail.Controls.Add(this.SP_btn_Set_MTPowerFail);
            this.fLP_PowerFail.Dock = System.Windows.Forms.DockStyle.Top;
            this.fLP_PowerFail.Location = new System.Drawing.Point(3, 3);
            this.fLP_PowerFail.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.fLP_PowerFail.Name = "fLP_PowerFail";
            this.fLP_PowerFail.Size = new System.Drawing.Size(357, 23);
            this.fLP_PowerFail.TabIndex = 28;
            // 
            // lblPowerFail
            // 
            this.lblPowerFail.BackColor = System.Drawing.Color.Transparent;
            this.lblPowerFail.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblPowerFail.ForeColor = System.Drawing.Color.Navy;
            this.lblPowerFail.Location = new System.Drawing.Point(3, 0);
            this.lblPowerFail.Name = "lblPowerFail";
            this.lblPowerFail.Size = new System.Drawing.Size(64, 15);
            this.lblPowerFail.TabIndex = 14;
            this.lblPowerFail.Text = "Power Fail";
            // 
            // SP_DT_MT_PowerFail
            // 
            this.SP_DT_MT_PowerFail.CustomFormat = "mm:ss";
            this.SP_DT_MT_PowerFail.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.SP_DT_MT_PowerFail.Location = new System.Drawing.Point(105, 0);
            this.SP_DT_MT_PowerFail.Margin = new System.Windows.Forms.Padding(35, 0, 3, 0);
            this.SP_DT_MT_PowerFail.Name = "SP_DT_MT_PowerFail";
            this.SP_DT_MT_PowerFail.ShowUpDown = true;
            this.SP_DT_MT_PowerFail.Size = new System.Drawing.Size(59, 23);
            this.SP_DT_MT_PowerFail.TabIndex = 13;
            this.SP_DT_MT_PowerFail.Value = new System.DateTime(2014, 5, 9, 0, 15, 0, 0);
            this.SP_DT_MT_PowerFail.Leave += new System.EventHandler(this.SP_DT_MT_PowerFail_Leave);
            // 
            // SP_btn_Get_MTPowerFail
            // 
            this.SP_btn_Get_MTPowerFail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SP_btn_Get_MTPowerFail.Location = new System.Drawing.Point(207, 0);
            this.SP_btn_Get_MTPowerFail.Margin = new System.Windows.Forms.Padding(40, 0, 3, 0);
            this.SP_btn_Get_MTPowerFail.Name = "SP_btn_Get_MTPowerFail";
            this.SP_btn_Get_MTPowerFail.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.SP_btn_Get_MTPowerFail.Size = new System.Drawing.Size(66, 23);
            this.SP_btn_Get_MTPowerFail.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.SP_btn_Get_MTPowerFail.StateCommon.Content.Image.ImageV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.SP_btn_Get_MTPowerFail.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Navy;
            this.SP_btn_Get_MTPowerFail.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SP_btn_Get_MTPowerFail.TabIndex = 20;
            this.SP_btn_Get_MTPowerFail.Values.Image = ((System.Drawing.Image)(resources.GetObject("SP_btn_Get_MTPowerFail.Values.Image")));
            this.SP_btn_Get_MTPowerFail.Values.Text = "Get";
            this.SP_btn_Get_MTPowerFail.Click += new System.EventHandler(this.SP_btn_Get_MTPowerFail_Click);
            // 
            // SP_btn_Set_MTPowerFail
            // 
            this.SP_btn_Set_MTPowerFail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SP_btn_Set_MTPowerFail.Location = new System.Drawing.Point(281, 0);
            this.SP_btn_Set_MTPowerFail.Margin = new System.Windows.Forms.Padding(5, 0, 10, 0);
            this.SP_btn_Set_MTPowerFail.Name = "SP_btn_Set_MTPowerFail";
            this.SP_btn_Set_MTPowerFail.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.SP_btn_Set_MTPowerFail.Size = new System.Drawing.Size(66, 23);
            this.SP_btn_Set_MTPowerFail.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.SP_btn_Set_MTPowerFail.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Navy;
            this.SP_btn_Set_MTPowerFail.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SP_btn_Set_MTPowerFail.TabIndex = 18;
            this.SP_btn_Set_MTPowerFail.Values.Image = ((System.Drawing.Image)(resources.GetObject("SP_btn_Set_MTPowerFail.Values.Image")));
            this.SP_btn_Set_MTPowerFail.Values.Text = "Set";
            // 
            // fLP_Earth
            // 
            this.fLP_Earth.AutoSize = true;
            this.fLP_Earth.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_Earth.Controls.Add(this.lblEarth);
            this.fLP_Earth.Controls.Add(this.SP_DT_MT_Earth);
            this.fLP_Earth.Controls.Add(this.SP_btn_Get_MTEarth);
            this.fLP_Earth.Controls.Add(this.SP_btn_Set_MTEarth);
            this.fLP_Earth.Dock = System.Windows.Forms.DockStyle.Top;
            this.fLP_Earth.Location = new System.Drawing.Point(3, 34);
            this.fLP_Earth.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.fLP_Earth.Name = "fLP_Earth";
            this.fLP_Earth.Size = new System.Drawing.Size(357, 23);
            this.fLP_Earth.TabIndex = 29;
            // 
            // lblEarth
            // 
            this.lblEarth.BackColor = System.Drawing.Color.Transparent;
            this.lblEarth.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblEarth.ForeColor = System.Drawing.Color.Navy;
            this.lblEarth.Location = new System.Drawing.Point(3, 0);
            this.lblEarth.Name = "lblEarth";
            this.lblEarth.Size = new System.Drawing.Size(39, 15);
            this.lblEarth.TabIndex = 14;
            this.lblEarth.Text = "Earth ";
            // 
            // SP_DT_MT_Earth
            // 
            this.SP_DT_MT_Earth.CustomFormat = "mm:ss";
            this.SP_DT_MT_Earth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.SP_DT_MT_Earth.Location = new System.Drawing.Point(105, 0);
            this.SP_DT_MT_Earth.Margin = new System.Windows.Forms.Padding(60, 0, 3, 0);
            this.SP_DT_MT_Earth.Name = "SP_DT_MT_Earth";
            this.SP_DT_MT_Earth.ShowUpDown = true;
            this.SP_DT_MT_Earth.Size = new System.Drawing.Size(59, 23);
            this.SP_DT_MT_Earth.TabIndex = 13;
            this.SP_DT_MT_Earth.Value = new System.DateTime(2014, 5, 9, 0, 15, 0, 0);
            this.SP_DT_MT_Earth.Leave += new System.EventHandler(this.SP_DT_MT_Earth_Leave);
            // 
            // SP_btn_Get_MTEarth
            // 
            this.SP_btn_Get_MTEarth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SP_btn_Get_MTEarth.Location = new System.Drawing.Point(207, 0);
            this.SP_btn_Get_MTEarth.Margin = new System.Windows.Forms.Padding(40, 0, 3, 0);
            this.SP_btn_Get_MTEarth.Name = "SP_btn_Get_MTEarth";
            this.SP_btn_Get_MTEarth.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.SP_btn_Get_MTEarth.Size = new System.Drawing.Size(66, 23);
            this.SP_btn_Get_MTEarth.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.SP_btn_Get_MTEarth.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Navy;
            this.SP_btn_Get_MTEarth.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SP_btn_Get_MTEarth.TabIndex = 20;
            this.SP_btn_Get_MTEarth.Values.Image = ((System.Drawing.Image)(resources.GetObject("SP_btn_Get_MTEarth.Values.Image")));
            this.SP_btn_Get_MTEarth.Values.Text = "Get";
            // 
            // SP_btn_Set_MTEarth
            // 
            this.SP_btn_Set_MTEarth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SP_btn_Set_MTEarth.Location = new System.Drawing.Point(281, 0);
            this.SP_btn_Set_MTEarth.Margin = new System.Windows.Forms.Padding(5, 0, 10, 0);
            this.SP_btn_Set_MTEarth.Name = "SP_btn_Set_MTEarth";
            this.SP_btn_Set_MTEarth.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.SP_btn_Set_MTEarth.Size = new System.Drawing.Size(66, 23);
            this.SP_btn_Set_MTEarth.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.SP_btn_Set_MTEarth.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Navy;
            this.SP_btn_Set_MTEarth.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SP_btn_Set_MTEarth.TabIndex = 18;
            this.SP_btn_Set_MTEarth.Values.Image = ((System.Drawing.Image)(resources.GetObject("SP_btn_Set_MTEarth.Values.Image")));
            this.SP_btn_Set_MTEarth.Values.Text = "Set";
            // 
            // fLP_ReverseEnergy
            // 
            this.fLP_ReverseEnergy.AutoSize = true;
            this.fLP_ReverseEnergy.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_ReverseEnergy.Controls.Add(this.lblReverseEnergy);
            this.fLP_ReverseEnergy.Controls.Add(this.SP_DT_MT_ReverseEnergy);
            this.fLP_ReverseEnergy.Controls.Add(this.SP_btn_Get_MTReverseEnergy);
            this.fLP_ReverseEnergy.Controls.Add(this.SP_btn_Set_MTReverseEnergy);
            this.fLP_ReverseEnergy.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fLP_ReverseEnergy.Location = new System.Drawing.Point(3, 65);
            this.fLP_ReverseEnergy.Margin = new System.Windows.Forms.Padding(3, 3, 3, 15);
            this.fLP_ReverseEnergy.Name = "fLP_ReverseEnergy";
            this.fLP_ReverseEnergy.Size = new System.Drawing.Size(357, 23);
            this.fLP_ReverseEnergy.TabIndex = 30;
            // 
            // lblReverseEnergy
            // 
            this.lblReverseEnergy.BackColor = System.Drawing.Color.Transparent;
            this.lblReverseEnergy.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblReverseEnergy.ForeColor = System.Drawing.Color.Navy;
            this.lblReverseEnergy.Location = new System.Drawing.Point(3, 0);
            this.lblReverseEnergy.Name = "lblReverseEnergy";
            this.lblReverseEnergy.Size = new System.Drawing.Size(94, 15);
            this.lblReverseEnergy.TabIndex = 14;
            this.lblReverseEnergy.Text = " Reverse Energy";
            // 
            // SP_DT_MT_ReverseEnergy
            // 
            this.SP_DT_MT_ReverseEnergy.CustomFormat = "mm:ss";
            this.SP_DT_MT_ReverseEnergy.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.SP_DT_MT_ReverseEnergy.Location = new System.Drawing.Point(105, 0);
            this.SP_DT_MT_ReverseEnergy.Margin = new System.Windows.Forms.Padding(5, 0, 3, 0);
            this.SP_DT_MT_ReverseEnergy.Name = "SP_DT_MT_ReverseEnergy";
            this.SP_DT_MT_ReverseEnergy.ShowUpDown = true;
            this.SP_DT_MT_ReverseEnergy.Size = new System.Drawing.Size(59, 23);
            this.SP_DT_MT_ReverseEnergy.TabIndex = 13;
            this.SP_DT_MT_ReverseEnergy.Value = new System.DateTime(2014, 5, 9, 0, 15, 0, 0);
            this.SP_DT_MT_ReverseEnergy.Leave += new System.EventHandler(this.SP_DT_MT_ReverseEnergy_Leave);
            // 
            // SP_btn_Get_MTReverseEnergy
            // 
            this.SP_btn_Get_MTReverseEnergy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SP_btn_Get_MTReverseEnergy.Location = new System.Drawing.Point(207, 0);
            this.SP_btn_Get_MTReverseEnergy.Margin = new System.Windows.Forms.Padding(40, 0, 3, 0);
            this.SP_btn_Get_MTReverseEnergy.Name = "SP_btn_Get_MTReverseEnergy";
            this.SP_btn_Get_MTReverseEnergy.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.SP_btn_Get_MTReverseEnergy.Size = new System.Drawing.Size(66, 23);
            this.SP_btn_Get_MTReverseEnergy.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.SP_btn_Get_MTReverseEnergy.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Navy;
            this.SP_btn_Get_MTReverseEnergy.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SP_btn_Get_MTReverseEnergy.TabIndex = 20;
            this.SP_btn_Get_MTReverseEnergy.Values.Image = ((System.Drawing.Image)(resources.GetObject("SP_btn_Get_MTReverseEnergy.Values.Image")));
            this.SP_btn_Get_MTReverseEnergy.Values.Text = "Get";
            // 
            // SP_btn_Set_MTReverseEnergy
            // 
            this.SP_btn_Set_MTReverseEnergy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SP_btn_Set_MTReverseEnergy.Location = new System.Drawing.Point(281, 0);
            this.SP_btn_Set_MTReverseEnergy.Margin = new System.Windows.Forms.Padding(5, 0, 10, 0);
            this.SP_btn_Set_MTReverseEnergy.Name = "SP_btn_Set_MTReverseEnergy";
            this.SP_btn_Set_MTReverseEnergy.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.SP_btn_Set_MTReverseEnergy.Size = new System.Drawing.Size(66, 23);
            this.SP_btn_Set_MTReverseEnergy.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.SP_btn_Set_MTReverseEnergy.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Navy;
            this.SP_btn_Set_MTReverseEnergy.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SP_btn_Set_MTReverseEnergy.TabIndex = 18;
            this.SP_btn_Set_MTReverseEnergy.Values.Image = ((System.Drawing.Image)(resources.GetObject("SP_btn_Set_MTReverseEnergy.Values.Image")));
            this.SP_btn_Set_MTReverseEnergy.Values.Text = "Set";
            // 
            // gpLimits
            // 
            this.gpLimits.AutoSize = true;
            this.gpLimits.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gpLimits.Controls.Add(this.fLP_OverLoadTotal);
            this.gpLimits.ForeColor = System.Drawing.Color.Maroon;
            this.gpLimits.Location = new System.Drawing.Point(3, 125);
            this.gpLimits.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.gpLimits.Name = "gpLimits";
            this.gpLimits.Size = new System.Drawing.Size(369, 67);
            this.gpLimits.TabIndex = 25;
            this.gpLimits.TabStop = false;
            this.gpLimits.Text = "Limits";
            // 
            // fLP_OverLoadTotal
            // 
            this.fLP_OverLoadTotal.AutoSize = true;
            this.fLP_OverLoadTotal.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_OverLoadTotal.Controls.Add(this.lblOverLoad);
            this.fLP_OverLoadTotal.Controls.Add(this.txt_SP_overLoadTotal);
            this.fLP_OverLoadTotal.Controls.Add(this.btn_SP_GetOverLoadTotal);
            this.fLP_OverLoadTotal.Controls.Add(this.btn_SP_SetOverLoadTotal);
            this.fLP_OverLoadTotal.Location = new System.Drawing.Point(6, 22);
            this.fLP_OverLoadTotal.Name = "fLP_OverLoadTotal";
            this.fLP_OverLoadTotal.Size = new System.Drawing.Size(357, 23);
            this.fLP_OverLoadTotal.TabIndex = 28;
            // 
            // lblOverLoad
            // 
            this.lblOverLoad.ForeColor = System.Drawing.Color.Navy;
            this.lblOverLoad.Location = new System.Drawing.Point(3, 0);
            this.lblOverLoad.Name = "lblOverLoad";
            this.lblOverLoad.Size = new System.Drawing.Size(96, 15);
            this.lblOverLoad.TabIndex = 0;
            this.lblOverLoad.Text = "Over Load Total ";
            // 
            // txt_SP_overLoadTotal
            // 
            this.txt_SP_overLoadTotal.Location = new System.Drawing.Point(117, 0);
            this.txt_SP_overLoadTotal.Margin = new System.Windows.Forms.Padding(15, 0, 3, 0);
            this.txt_SP_overLoadTotal.MaxLength = 5;
            this.txt_SP_overLoadTotal.Name = "txt_SP_overLoadTotal";
            this.txt_SP_overLoadTotal.Size = new System.Drawing.Size(72, 23);
            this.txt_SP_overLoadTotal.TabIndex = 1;
            this.txt_SP_overLoadTotal.Text = "00";
            this.txt_SP_overLoadTotal.TextChanged += new System.EventHandler(this.txt_SP_overLoadTotal_TextChanged);
            this.txt_SP_overLoadTotal.Leave += new System.EventHandler(this.txt_OverLoadTota_Leave);
            // 
            // btn_SP_GetOverLoadTotal
            // 
            this.btn_SP_GetOverLoadTotal.Location = new System.Drawing.Point(207, 0);
            this.btn_SP_GetOverLoadTotal.Margin = new System.Windows.Forms.Padding(15, 0, 3, 0);
            this.btn_SP_GetOverLoadTotal.Name = "btn_SP_GetOverLoadTotal";
            this.btn_SP_GetOverLoadTotal.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_SP_GetOverLoadTotal.Size = new System.Drawing.Size(66, 23);
            this.btn_SP_GetOverLoadTotal.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.btn_SP_GetOverLoadTotal.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Navy;
            this.btn_SP_GetOverLoadTotal.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SP_GetOverLoadTotal.TabIndex = 19;
            this.btn_SP_GetOverLoadTotal.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_SP_GetOverLoadTotal.Values.Image")));
            this.btn_SP_GetOverLoadTotal.Values.Text = "Get";
            // 
            // btn_SP_SetOverLoadTotal
            // 
            this.btn_SP_SetOverLoadTotal.Location = new System.Drawing.Point(281, 0);
            this.btn_SP_SetOverLoadTotal.Margin = new System.Windows.Forms.Padding(5, 0, 10, 0);
            this.btn_SP_SetOverLoadTotal.Name = "btn_SP_SetOverLoadTotal";
            this.btn_SP_SetOverLoadTotal.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_SP_SetOverLoadTotal.Size = new System.Drawing.Size(66, 23);
            this.btn_SP_SetOverLoadTotal.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.btn_SP_SetOverLoadTotal.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Navy;
            this.btn_SP_SetOverLoadTotal.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SP_SetOverLoadTotal.TabIndex = 17;
            this.btn_SP_SetOverLoadTotal.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_SP_SetOverLoadTotal.Values.Image")));
            this.btn_SP_SetOverLoadTotal.Values.Text = "Set";
            // 
            // gpErrorFlages
            // 
            this.gpErrorFlages.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gpErrorFlages.Controls.Add(this.SP_btn_getEventString);
            this.gpErrorFlages.Controls.Add(this.check_RFU2);
            this.gpErrorFlages.Controls.Add(this.check_RFU1);
            this.gpErrorFlages.Controls.Add(this.check_CMMDI);
            this.gpErrorFlages.Controls.Add(this.check_BRE);
            this.gpErrorFlages.Controls.Add(this.txt_EventDEtail);
            this.gpErrorFlages.ForeColor = System.Drawing.Color.Maroon;
            this.gpErrorFlages.Location = new System.Drawing.Point(378, 0);
            this.gpErrorFlages.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.gpErrorFlages.Name = "gpErrorFlages";
            this.gpErrorFlages.Size = new System.Drawing.Size(288, 126);
            this.gpErrorFlages.TabIndex = 26;
            this.gpErrorFlages.TabStop = false;
            this.gpErrorFlages.Text = "Error Flages";
            // 
            // SP_btn_getEventString
            // 
            this.SP_btn_getEventString.ForeColor = System.Drawing.Color.Navy;
            this.SP_btn_getEventString.Location = new System.Drawing.Point(167, 93);
            this.SP_btn_getEventString.Name = "SP_btn_getEventString";
            this.SP_btn_getEventString.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.SP_btn_getEventString.Size = new System.Drawing.Size(115, 26);
            this.SP_btn_getEventString.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.SP_btn_getEventString.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Navy;
            this.SP_btn_getEventString.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold);
            this.SP_btn_getEventString.TabIndex = 4;
            this.SP_btn_getEventString.Values.Image = ((System.Drawing.Image)(resources.GetObject("SP_btn_getEventString.Values.Image")));
            this.SP_btn_getEventString.Values.Text = "Get Event detail";
            // 
            // check_RFU2
            // 
            this.check_RFU2.AutoSize = true;
            this.check_RFU2.ForeColor = System.Drawing.Color.Navy;
            this.check_RFU2.Location = new System.Drawing.Point(126, 77);
            this.check_RFU2.Name = "check_RFU2";
            this.check_RFU2.Size = new System.Drawing.Size(54, 19);
            this.check_RFU2.TabIndex = 5;
            this.check_RFU2.Text = "RFU2";
            this.check_RFU2.UseVisualStyleBackColor = true;
            // 
            // check_RFU1
            // 
            this.check_RFU1.AutoSize = true;
            this.check_RFU1.ForeColor = System.Drawing.Color.Navy;
            this.check_RFU1.Location = new System.Drawing.Point(126, 58);
            this.check_RFU1.Name = "check_RFU1";
            this.check_RFU1.Size = new System.Drawing.Size(54, 19);
            this.check_RFU1.TabIndex = 7;
            this.check_RFU1.Text = "RFU1";
            this.check_RFU1.UseVisualStyleBackColor = true;
            // 
            // check_CMMDI
            // 
            this.check_CMMDI.AutoSize = true;
            this.check_CMMDI.ForeColor = System.Drawing.Color.Navy;
            this.check_CMMDI.Location = new System.Drawing.Point(126, 39);
            this.check_CMMDI.Name = "check_CMMDI";
            this.check_CMMDI.Size = new System.Drawing.Size(134, 19);
            this.check_CMMDI.TabIndex = 8;
            this.check_CMMDI.Text = "Current Month MDI";
            this.check_CMMDI.UseVisualStyleBackColor = true;
            // 
            // check_BRE
            // 
            this.check_BRE.AutoSize = true;
            this.check_BRE.ForeColor = System.Drawing.Color.Navy;
            this.check_BRE.Location = new System.Drawing.Point(126, 19);
            this.check_BRE.Name = "check_BRE";
            this.check_BRE.Size = new System.Drawing.Size(121, 19);
            this.check_BRE.TabIndex = 6;
            this.check_BRE.Text = "Bill Register Error";
            this.check_BRE.UseVisualStyleBackColor = true;
            // 
            // txt_EventDEtail
            // 
            this.txt_EventDEtail.Location = new System.Drawing.Point(6, 22);
            this.txt_EventDEtail.Name = "txt_EventDEtail";
            this.txt_EventDEtail.ReadOnly = true;
            this.txt_EventDEtail.Size = new System.Drawing.Size(110, 98);
            this.txt_EventDEtail.TabIndex = 3;
            this.txt_EventDEtail.Text = "";
            // 
            // gpMDI
            // 
            this.gpMDI.Controls.Add(this.ucMDIAutoResetDateParam);
            this.gpMDI.Controls.Add(this.SP_btn_Set_MDI_AutoResetDate);
            this.gpMDI.Controls.Add(this.SP_btn_Get_MDI_AutoResetDate);
            this.gpMDI.ForeColor = System.Drawing.Color.Maroon;
            this.gpMDI.Location = new System.Drawing.Point(3, 198);
            this.gpMDI.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.gpMDI.Name = "gpMDI";
            this.gpMDI.Size = new System.Drawing.Size(684, 124);
            this.gpMDI.TabIndex = 28;
            this.gpMDI.TabStop = false;
            this.gpMDI.Text = "MDI Auto Reset";
            // 
            // ucMDIAutoResetDateParam
            // 
            this.ucMDIAutoResetDateParam.BackColor = System.Drawing.Color.Transparent;
            this.ucMDIAutoResetDateParam.Location = new System.Drawing.Point(6, 22);
            this.ucMDIAutoResetDateParam.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.ucMDIAutoResetDateParam.Name = "ucMDIAutoResetDateParam";
            this.ucMDIAutoResetDateParam.Size = new System.Drawing.Size(461, 103);
            this.ucMDIAutoResetDateParam.TabIndex = 27;
            // 
            // SP_btn_Set_MDI_AutoResetDate
            // 
            this.SP_btn_Set_MDI_AutoResetDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SP_btn_Set_MDI_AutoResetDate.Location = new System.Drawing.Point(591, 46);
            this.SP_btn_Set_MDI_AutoResetDate.Name = "SP_btn_Set_MDI_AutoResetDate";
            this.SP_btn_Set_MDI_AutoResetDate.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.SP_btn_Set_MDI_AutoResetDate.Size = new System.Drawing.Size(66, 23);
            this.SP_btn_Set_MDI_AutoResetDate.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.SP_btn_Set_MDI_AutoResetDate.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Navy;
            this.SP_btn_Set_MDI_AutoResetDate.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SP_btn_Set_MDI_AutoResetDate.TabIndex = 17;
            this.SP_btn_Set_MDI_AutoResetDate.Values.Image = ((System.Drawing.Image)(resources.GetObject("SP_btn_Set_MDI_AutoResetDate.Values.Image")));
            this.SP_btn_Set_MDI_AutoResetDate.Values.Text = "Set";
            // 
            // SP_btn_Get_MDI_AutoResetDate
            // 
            this.SP_btn_Get_MDI_AutoResetDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SP_btn_Get_MDI_AutoResetDate.Location = new System.Drawing.Point(519, 46);
            this.SP_btn_Get_MDI_AutoResetDate.Name = "SP_btn_Get_MDI_AutoResetDate";
            this.SP_btn_Get_MDI_AutoResetDate.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.SP_btn_Get_MDI_AutoResetDate.Size = new System.Drawing.Size(66, 23);
            this.SP_btn_Get_MDI_AutoResetDate.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.SP_btn_Get_MDI_AutoResetDate.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Navy;
            this.SP_btn_Get_MDI_AutoResetDate.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SP_btn_Get_MDI_AutoResetDate.TabIndex = 19;
            this.SP_btn_Get_MDI_AutoResetDate.Values.Image = ((System.Drawing.Image)(resources.GetObject("SP_btn_Get_MDI_AutoResetDate.Values.Image")));
            this.SP_btn_Get_MDI_AutoResetDate.Values.Text = "Get";
            // 
            // ucParamSinglePhase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.gpParmSinglePhase);
            this.DoubleBuffered = true;
            this.Name = "ucParamSinglePhase";
            this.Size = new System.Drawing.Size(706, 367);
            this.Load += new System.EventHandler(this.ucParamSinglePhase_Load);
            this.gpParmSinglePhase.ResumeLayout(false);
            this.fLP_Main.ResumeLayout(false);
            this.fLP_Main.PerformLayout();
            this.fLP_first.ResumeLayout(false);
            this.fLP_first.PerformLayout();
            this.fLP_Mntr_Limit.ResumeLayout(false);
            this.fLP_Mntr_Limit.PerformLayout();
            this.gpMonitoring_Time.ResumeLayout(false);
            this.gpMonitoring_Time.PerformLayout();
            this.fLP_Main_Mntr.ResumeLayout(false);
            this.fLP_Main_Mntr.PerformLayout();
            this.fLP_PowerFail.ResumeLayout(false);
            this.fLP_Earth.ResumeLayout(false);
            this.fLP_ReverseEnergy.ResumeLayout(false);
            this.gpLimits.ResumeLayout(false);
            this.gpLimits.PerformLayout();
            this.fLP_OverLoadTotal.ResumeLayout(false);
            this.fLP_OverLoadTotal.PerformLayout();
            this.gpErrorFlages.ResumeLayout(false);
            this.gpErrorFlages.PerformLayout();
            this.gpMDI.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gpParmSinglePhase;
        private System.Windows.Forms.GroupBox gpErrorFlages;
        private System.Windows.Forms.GroupBox gpMonitoring_Time;
        public System.Windows.Forms.Label lblPowerFail;
        public System.Windows.Forms.Label lblEarth;
        public System.Windows.Forms.Label lblReverseEnergy;
        private System.Windows.Forms.DateTimePicker SP_DT_MT_PowerFail;
        private System.Windows.Forms.DateTimePicker SP_DT_MT_Earth;
        private System.Windows.Forms.DateTimePicker SP_DT_MT_ReverseEnergy;
        private System.Windows.Forms.GroupBox gpLimits;
        private System.Windows.Forms.TextBox txt_SP_overLoadTotal;

        internal ComponentFactory.Krypton.Toolkit.KryptonButton SP_btn_Set_MTReverseEnergy;
        internal ComponentFactory.Krypton.Toolkit.KryptonButton SP_btn_Set_MTEarth;
        internal ComponentFactory.Krypton.Toolkit.KryptonButton SP_btn_Set_MTPowerFail;
        internal ComponentFactory.Krypton.Toolkit.KryptonButton SP_btn_Get_MTPowerFail;
        internal ComponentFactory.Krypton.Toolkit.KryptonButton SP_btn_Get_MTEarth;
        internal ComponentFactory.Krypton.Toolkit.KryptonButton SP_btn_Get_MTReverseEnergy;
        internal System.Windows.Forms.ProgressBar progressBar2;
        internal ComponentFactory.Krypton.Toolkit.KryptonButton SP_btn_Set_MDI_AutoResetDate;
        internal ComponentFactory.Krypton.Toolkit.KryptonButton SP_btn_Get_MDI_AutoResetDate;
        internal ComponentFactory.Krypton.Toolkit.KryptonButton btn_SP_SetOverLoadTotal;
        internal ComponentFactory.Krypton.Toolkit.KryptonButton btn_SP_GetOverLoadTotal;
        private System.Windows.Forms.CheckBox check_RFU2;
        private System.Windows.Forms.CheckBox check_RFU1;
        private System.Windows.Forms.CheckBox check_CMMDI;
        private System.Windows.Forms.CheckBox check_BRE;
        private System.Windows.Forms.RichTextBox txt_EventDEtail;
        internal ComponentFactory.Krypton.Toolkit.KryptonButton SP_btn_getEventString;
        private ucCustomDateTime ucMDIAutoResetDateParam;
        private System.Windows.Forms.GroupBox gpMDI;
        private System.Windows.Forms.FlowLayoutPanel fLP_PowerFail;
        private System.Windows.Forms.FlowLayoutPanel fLP_Earth;
        private System.Windows.Forms.FlowLayoutPanel fLP_ReverseEnergy;
        private System.Windows.Forms.FlowLayoutPanel fLP_Main_Mntr;
        private System.Windows.Forms.FlowLayoutPanel fLP_OverLoadTotal;
        private System.Windows.Forms.Label lblOverLoad;
        private System.Windows.Forms.FlowLayoutPanel fLP_Mntr_Limit;
        private System.Windows.Forms.FlowLayoutPanel fLP_first;
        private System.Windows.Forms.FlowLayoutPanel fLP_Main;
    }
}
