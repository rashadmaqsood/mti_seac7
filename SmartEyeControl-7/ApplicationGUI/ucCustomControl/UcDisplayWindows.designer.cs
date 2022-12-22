using System.Windows.Forms;
namespace ucCustomControl
{
    partial class ucDisplayWindows
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDisplayWindows));
            this.mainPanel = new System.Windows.Forms.Panel();
            this.gpDisplayWindows = new System.Windows.Forms.GroupBox();
            this.flp_Main_Panel = new System.Windows.Forms.FlowLayoutPanel();
            this.fLPanel_avail_Category = new System.Windows.Forms.FlowLayoutPanel();
            this.gp_Avail_Category = new System.Windows.Forms.GroupBox();
            this.list_AvailableWindowsCategory = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.gp_GeneralSettings = new System.Windows.Forms.GroupBox();
            this.fLPanel_GenSettings = new System.Windows.Forms.FlowLayoutPanel();
            this.radio_windows_Normal = new System.Windows.Forms.RadioButton();
            this.radio_windows_Alternate = new System.Windows.Forms.RadioButton();
            this.radio_windows_Test = new System.Windows.Forms.RadioButton();
            this.lbl_NumberFormat = new System.Windows.Forms.Label();
            this.combo_Windows_NumberFormat = new System.Windows.Forms.ComboBox();
            this.pnl_gen_Scroll_Time = new System.Windows.Forms.Panel();
            this.lblScrollTime = new System.Windows.Forms.Label();
            this.txt_Windows_ScrollTime_ = new System.Windows.Forms.DateTimePicker();
            this.lblUnit = new System.Windows.Forms.Label();
            this.lbl_ScrollTimeLimit = new System.Windows.Forms.Label();
            this.pnl_Gen_Chks = new System.Windows.Forms.Panel();
            this.check_DisplayOBIS_D = new System.Windows.Forms.CheckBox();
            this.check_DisplayOBIS_C = new System.Windows.Forms.CheckBox();
            this.check_DisplayOBIS_E = new System.Windows.Forms.CheckBox();
            this.check_DoubleDot = new System.Windows.Forms.CheckBox();
            this.check_SingleDot = new System.Windows.Forms.CheckBox();
            this.flp_Avail_Win = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.list_AvailableWindows = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.flowLayoutPanel_btns = new System.Windows.Forms.FlowLayoutPanel();
            this.gpWindowSerialNo = new System.Windows.Forms.GroupBox();
            this.txt_Windows_number = new System.Windows.Forms.TextBox();
            this.btn_EditWindow = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_GenerateReport = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_Windows_AddWindow = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_removeAllWindows = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_RemoveWindow = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_replaceWindow = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.flp_SelectedWin = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSelectedWindows = new System.Windows.Forms.Label();
            this.grid_SelectedWindows = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.col_Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.mainPanel.SuspendLayout();
            this.gpDisplayWindows.SuspendLayout();
            this.flp_Main_Panel.SuspendLayout();
            this.fLPanel_avail_Category.SuspendLayout();
            this.gp_Avail_Category.SuspendLayout();
            this.gp_GeneralSettings.SuspendLayout();
            this.fLPanel_GenSettings.SuspendLayout();
            this.pnl_gen_Scroll_Time.SuspendLayout();
            this.pnl_Gen_Chks.SuspendLayout();
            this.flp_Avail_Win.SuspendLayout();
            this.flowLayoutPanel_btns.SuspendLayout();
            this.gpWindowSerialNo.SuspendLayout();
            this.flp_SelectedWin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_SelectedWindows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.Transparent;
            this.mainPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mainPanel.Controls.Add(this.gpDisplayWindows);
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(957, 400);
            this.mainPanel.TabIndex = 0;
            // 
            // gpDisplayWindows
            // 
            this.gpDisplayWindows.AutoSize = true;
            this.gpDisplayWindows.BackColor = System.Drawing.Color.Transparent;
            this.gpDisplayWindows.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gpDisplayWindows.Controls.Add(this.flp_Main_Panel);
            this.gpDisplayWindows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpDisplayWindows.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.gpDisplayWindows.ForeColor = System.Drawing.Color.Maroon;
            this.gpDisplayWindows.Location = new System.Drawing.Point(0, 0);
            this.gpDisplayWindows.Name = "gpDisplayWindows";
            this.gpDisplayWindows.Size = new System.Drawing.Size(957, 400);
            this.gpDisplayWindows.TabIndex = 0;
            this.gpDisplayWindows.TabStop = false;
            this.gpDisplayWindows.Text = "Display Windows";
            // 
            // flp_Main_Panel
            // 
            this.flp_Main_Panel.AutoSize = true;
            this.flp_Main_Panel.Controls.Add(this.fLPanel_avail_Category);
            this.flp_Main_Panel.Controls.Add(this.flp_Avail_Win);
            this.flp_Main_Panel.Controls.Add(this.flowLayoutPanel_btns);
            this.flp_Main_Panel.Controls.Add(this.flp_SelectedWin);
            this.flp_Main_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_Main_Panel.Location = new System.Drawing.Point(3, 19);
            this.flp_Main_Panel.Name = "flp_Main_Panel";
            this.flp_Main_Panel.Size = new System.Drawing.Size(951, 378);
            this.flp_Main_Panel.TabIndex = 32;
            // 
            // fLPanel_avail_Category
            // 
            this.fLPanel_avail_Category.AutoSize = true;
            this.fLPanel_avail_Category.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLPanel_avail_Category.Controls.Add(this.gp_Avail_Category);
            this.fLPanel_avail_Category.Controls.Add(this.gp_GeneralSettings);
            this.fLPanel_avail_Category.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLPanel_avail_Category.Location = new System.Drawing.Point(3, 3);
            this.fLPanel_avail_Category.Name = "fLPanel_avail_Category";
            this.fLPanel_avail_Category.Size = new System.Drawing.Size(118, 365);
            this.fLPanel_avail_Category.TabIndex = 29;
            // 
            // gp_Avail_Category
            // 
            this.gp_Avail_Category.Controls.Add(this.list_AvailableWindowsCategory);
            this.gp_Avail_Category.ForeColor = System.Drawing.Color.Maroon;
            this.gp_Avail_Category.Location = new System.Drawing.Point(0, 0);
            this.gp_Avail_Category.Margin = new System.Windows.Forms.Padding(0);
            this.gp_Avail_Category.Name = "gp_Avail_Category";
            this.gp_Avail_Category.Size = new System.Drawing.Size(118, 120);
            this.gp_Avail_Category.TabIndex = 27;
            this.gp_Avail_Category.TabStop = false;
            this.gp_Avail_Category.Text = "Available Categories";
            // 
            // list_AvailableWindowsCategory
            // 
            this.list_AvailableWindowsCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list_AvailableWindowsCategory.FormattingEnabled = true;
            this.list_AvailableWindowsCategory.Location = new System.Drawing.Point(3, 19);
            this.list_AvailableWindowsCategory.Name = "list_AvailableWindowsCategory";
            this.list_AvailableWindowsCategory.Size = new System.Drawing.Size(112, 98);
            this.list_AvailableWindowsCategory.Sorted = true;
            this.list_AvailableWindowsCategory.TabIndex = 15;
            this.list_AvailableWindowsCategory.SelectedIndexChanged += new System.EventHandler(this.list_AvailableWindowsCategory_SelectedIndexChanged);
            // 
            // gp_GeneralSettings
            // 
            this.gp_GeneralSettings.Controls.Add(this.fLPanel_GenSettings);
            this.gp_GeneralSettings.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gp_GeneralSettings.ForeColor = System.Drawing.Color.Maroon;
            this.gp_GeneralSettings.Location = new System.Drawing.Point(0, 120);
            this.gp_GeneralSettings.Margin = new System.Windows.Forms.Padding(0);
            this.gp_GeneralSettings.Name = "gp_GeneralSettings";
            this.gp_GeneralSettings.Size = new System.Drawing.Size(118, 245);
            this.gp_GeneralSettings.TabIndex = 14;
            this.gp_GeneralSettings.TabStop = false;
            this.gp_GeneralSettings.Text = "General Settings";
            // 
            // fLPanel_GenSettings
            // 
            this.fLPanel_GenSettings.AutoSize = true;
            this.fLPanel_GenSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLPanel_GenSettings.Controls.Add(this.radio_windows_Normal);
            this.fLPanel_GenSettings.Controls.Add(this.radio_windows_Alternate);
            this.fLPanel_GenSettings.Controls.Add(this.radio_windows_Test);
            this.fLPanel_GenSettings.Controls.Add(this.lbl_NumberFormat);
            this.fLPanel_GenSettings.Controls.Add(this.combo_Windows_NumberFormat);
            this.fLPanel_GenSettings.Controls.Add(this.pnl_gen_Scroll_Time);
            this.fLPanel_GenSettings.Controls.Add(this.pnl_Gen_Chks);
            this.fLPanel_GenSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fLPanel_GenSettings.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLPanel_GenSettings.Location = new System.Drawing.Point(3, 19);
            this.fLPanel_GenSettings.Margin = new System.Windows.Forms.Padding(0);
            this.fLPanel_GenSettings.Name = "fLPanel_GenSettings";
            this.fLPanel_GenSettings.Size = new System.Drawing.Size(112, 223);
            this.fLPanel_GenSettings.TabIndex = 30;
            // 
            // radio_windows_Normal
            // 
            this.radio_windows_Normal.AutoSize = true;
            this.radio_windows_Normal.Checked = true;
            this.radio_windows_Normal.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radio_windows_Normal.ForeColor = System.Drawing.Color.Black;
            this.radio_windows_Normal.Location = new System.Drawing.Point(10, 0);
            this.radio_windows_Normal.Margin = new System.Windows.Forms.Padding(10, 0, 50, 0);
            this.radio_windows_Normal.Name = "radio_windows_Normal";
            this.radio_windows_Normal.Size = new System.Drawing.Size(66, 19);
            this.radio_windows_Normal.TabIndex = 5;
            this.radio_windows_Normal.TabStop = true;
            this.radio_windows_Normal.Text = "Normal";
            this.radio_windows_Normal.UseVisualStyleBackColor = true;
            this.radio_windows_Normal.CheckedChanged += new System.EventHandler(this.radio_windows_Normal_CheckedChanged);
            // 
            // radio_windows_Alternate
            // 
            this.radio_windows_Alternate.AutoSize = true;
            this.radio_windows_Alternate.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radio_windows_Alternate.ForeColor = System.Drawing.Color.Black;
            this.radio_windows_Alternate.Location = new System.Drawing.Point(10, 19);
            this.radio_windows_Alternate.Margin = new System.Windows.Forms.Padding(10, 0, 40, 0);
            this.radio_windows_Alternate.Name = "radio_windows_Alternate";
            this.radio_windows_Alternate.Size = new System.Drawing.Size(78, 19);
            this.radio_windows_Alternate.TabIndex = 5;
            this.radio_windows_Alternate.Text = "Alternate";
            this.radio_windows_Alternate.UseVisualStyleBackColor = true;
            this.radio_windows_Alternate.CheckedChanged += new System.EventHandler(this.radio_windows_Normal_CheckedChanged);
            // 
            // radio_windows_Test
            // 
            this.radio_windows_Test.AutoSize = true;
            this.radio_windows_Test.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radio_windows_Test.ForeColor = System.Drawing.Color.Black;
            this.radio_windows_Test.Location = new System.Drawing.Point(10, 38);
            this.radio_windows_Test.Margin = new System.Windows.Forms.Padding(10, 0, 60, 0);
            this.radio_windows_Test.Name = "radio_windows_Test";
            this.radio_windows_Test.Size = new System.Drawing.Size(47, 19);
            this.radio_windows_Test.TabIndex = 5;
            this.radio_windows_Test.Text = "Test";
            this.radio_windows_Test.UseVisualStyleBackColor = true;
            this.radio_windows_Test.CheckedChanged += new System.EventHandler(this.radio_windows_Normal_CheckedChanged);
            // 
            // lbl_NumberFormat
            // 
            this.lbl_NumberFormat.AutoSize = true;
            this.lbl_NumberFormat.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NumberFormat.ForeColor = System.Drawing.Color.Black;
            this.lbl_NumberFormat.Location = new System.Drawing.Point(3, 57);
            this.lbl_NumberFormat.Name = "lbl_NumberFormat";
            this.lbl_NumberFormat.Size = new System.Drawing.Size(96, 15);
            this.lbl_NumberFormat.TabIndex = 3;
            this.lbl_NumberFormat.Text = "Number Format";
            // 
            // combo_Windows_NumberFormat
            // 
            this.combo_Windows_NumberFormat.FormattingEnabled = true;
            this.combo_Windows_NumberFormat.Items.AddRange(new object[] {
            "OBIS Mode",
            "Number"});
            this.combo_Windows_NumberFormat.Location = new System.Drawing.Point(5, 75);
            this.combo_Windows_NumberFormat.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.combo_Windows_NumberFormat.Name = "combo_Windows_NumberFormat";
            this.combo_Windows_NumberFormat.Size = new System.Drawing.Size(81, 23);
            this.combo_Windows_NumberFormat.TabIndex = 6;
            this.combo_Windows_NumberFormat.SelectedIndexChanged += new System.EventHandler(this.combo_Windows_NumberFormat_SelectedIndexChanged);
            // 
            // pnl_gen_Scroll_Time
            // 
            this.pnl_gen_Scroll_Time.AutoSize = true;
            this.pnl_gen_Scroll_Time.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnl_gen_Scroll_Time.Controls.Add(this.lblScrollTime);
            this.pnl_gen_Scroll_Time.Controls.Add(this.txt_Windows_ScrollTime_);
            this.pnl_gen_Scroll_Time.Controls.Add(this.lblUnit);
            this.pnl_gen_Scroll_Time.Controls.Add(this.lbl_ScrollTimeLimit);
            this.pnl_gen_Scroll_Time.Location = new System.Drawing.Point(3, 104);
            this.pnl_gen_Scroll_Time.Name = "pnl_gen_Scroll_Time";
            this.pnl_gen_Scroll_Time.Size = new System.Drawing.Size(90, 59);
            this.pnl_gen_Scroll_Time.TabIndex = 30;
            // 
            // lblScrollTime
            // 
            this.lblScrollTime.AutoSize = true;
            this.lblScrollTime.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScrollTime.ForeColor = System.Drawing.Color.Black;
            this.lblScrollTime.Location = new System.Drawing.Point(3, 0);
            this.lblScrollTime.Name = "lblScrollTime";
            this.lblScrollTime.Size = new System.Drawing.Size(66, 15);
            this.lblScrollTime.TabIndex = 3;
            this.lblScrollTime.Text = "Scroll Time";
            // 
            // txt_Windows_ScrollTime_
            // 
            this.txt_Windows_ScrollTime_.CustomFormat = "mm:ss";
            this.txt_Windows_ScrollTime_.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txt_Windows_ScrollTime_.Location = new System.Drawing.Point(4, 23);
            this.txt_Windows_ScrollTime_.Name = "txt_Windows_ScrollTime_";
            this.txt_Windows_ScrollTime_.ShowUpDown = true;
            this.txt_Windows_ScrollTime_.Size = new System.Drawing.Size(79, 23);
            this.txt_Windows_ScrollTime_.TabIndex = 10;
            this.txt_Windows_ScrollTime_.ValueChanged += new System.EventHandler(this.txt_Windows_ScrollTime__ValueChanged);
            this.txt_Windows_ScrollTime_.Leave += new System.EventHandler(this.txt_Windows_ScrollTime_Leave);
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.ForeColor = System.Drawing.Color.Maroon;
            this.lblUnit.Location = new System.Drawing.Point(3, 9);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(43, 15);
            this.lblUnit.TabIndex = 9;
            this.lblUnit.Text = "mm:ss";
            // 
            // lbl_ScrollTimeLimit
            // 
            this.lbl_ScrollTimeLimit.AutoSize = true;
            this.lbl_ScrollTimeLimit.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ScrollTimeLimit.ForeColor = System.Drawing.Color.Black;
            this.lbl_ScrollTimeLimit.Location = new System.Drawing.Point(1, 44);
            this.lbl_ScrollTimeLimit.Name = "lbl_ScrollTimeLimit";
            this.lbl_ScrollTimeLimit.Size = new System.Drawing.Size(86, 15);
            this.lbl_ScrollTimeLimit.TabIndex = 11;
            this.lbl_ScrollTimeLimit.Text = "[00:05 -04:15]";
            // 
            // pnl_Gen_Chks
            // 
            this.pnl_Gen_Chks.Controls.Add(this.check_DisplayOBIS_D);
            this.pnl_Gen_Chks.Controls.Add(this.check_DisplayOBIS_C);
            this.pnl_Gen_Chks.Controls.Add(this.check_DisplayOBIS_E);
            this.pnl_Gen_Chks.Controls.Add(this.check_DoubleDot);
            this.pnl_Gen_Chks.Controls.Add(this.check_SingleDot);
            this.pnl_Gen_Chks.Location = new System.Drawing.Point(131, 3);
            this.pnl_Gen_Chks.Name = "pnl_Gen_Chks";
            this.pnl_Gen_Chks.Size = new System.Drawing.Size(103, 58);
            this.pnl_Gen_Chks.TabIndex = 31;
            this.pnl_Gen_Chks.Visible = false;
            // 
            // check_DisplayOBIS_D
            // 
            this.check_DisplayOBIS_D.AutoSize = true;
            this.check_DisplayOBIS_D.ForeColor = System.Drawing.Color.Navy;
            this.check_DisplayOBIS_D.Location = new System.Drawing.Point(3, 21);
            this.check_DisplayOBIS_D.Name = "check_DisplayOBIS_D";
            this.check_DisplayOBIS_D.Size = new System.Drawing.Size(75, 19);
            this.check_DisplayOBIS_D.TabIndex = 8;
            this.check_DisplayOBIS_D.Text = "Display D";
            this.check_DisplayOBIS_D.UseVisualStyleBackColor = true;
            this.check_DisplayOBIS_D.Visible = false;
            // 
            // check_DisplayOBIS_C
            // 
            this.check_DisplayOBIS_C.AutoSize = true;
            this.check_DisplayOBIS_C.ForeColor = System.Drawing.Color.Navy;
            this.check_DisplayOBIS_C.Location = new System.Drawing.Point(3, 3);
            this.check_DisplayOBIS_C.Name = "check_DisplayOBIS_C";
            this.check_DisplayOBIS_C.Size = new System.Drawing.Size(74, 19);
            this.check_DisplayOBIS_C.TabIndex = 8;
            this.check_DisplayOBIS_C.Text = "Display C";
            this.check_DisplayOBIS_C.UseVisualStyleBackColor = true;
            this.check_DisplayOBIS_C.Visible = false;
            this.check_DisplayOBIS_C.CheckedChanged += new System.EventHandler(this.check_DisplayOBIS_C_CheckedChanged);
            // 
            // check_DisplayOBIS_E
            // 
            this.check_DisplayOBIS_E.AutoSize = true;
            this.check_DisplayOBIS_E.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_DisplayOBIS_E.ForeColor = System.Drawing.Color.Navy;
            this.check_DisplayOBIS_E.Location = new System.Drawing.Point(3, 38);
            this.check_DisplayOBIS_E.Name = "check_DisplayOBIS_E";
            this.check_DisplayOBIS_E.Size = new System.Drawing.Size(73, 19);
            this.check_DisplayOBIS_E.TabIndex = 8;
            this.check_DisplayOBIS_E.Text = "Display E";
            this.check_DisplayOBIS_E.UseVisualStyleBackColor = true;
            this.check_DisplayOBIS_E.Visible = false;
            this.check_DisplayOBIS_E.CheckedChanged += new System.EventHandler(this.check_DisplayOBIS_E_CheckedChanged);
            // 
            // check_DoubleDot
            // 
            this.check_DoubleDot.AutoSize = true;
            this.check_DoubleDot.Enabled = false;
            this.check_DoubleDot.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_DoubleDot.ForeColor = System.Drawing.Color.Navy;
            this.check_DoubleDot.Location = new System.Drawing.Point(3, 3);
            this.check_DoubleDot.Name = "check_DoubleDot";
            this.check_DoubleDot.Size = new System.Drawing.Size(88, 19);
            this.check_DoubleDot.TabIndex = 8;
            this.check_DoubleDot.Text = "Double Dot";
            this.check_DoubleDot.UseVisualStyleBackColor = true;
            this.check_DoubleDot.Visible = false;
            this.check_DoubleDot.CheckedChanged += new System.EventHandler(this.check_DoubleDot_CheckedChanged);
            // 
            // check_SingleDot
            // 
            this.check_SingleDot.AutoSize = true;
            this.check_SingleDot.Enabled = false;
            this.check_SingleDot.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_SingleDot.ForeColor = System.Drawing.Color.Navy;
            this.check_SingleDot.Location = new System.Drawing.Point(3, 21);
            this.check_SingleDot.Name = "check_SingleDot";
            this.check_SingleDot.Size = new System.Drawing.Size(81, 19);
            this.check_SingleDot.TabIndex = 8;
            this.check_SingleDot.Text = "Single Dot";
            this.check_SingleDot.UseVisualStyleBackColor = true;
            this.check_SingleDot.Visible = false;
            // 
            // flp_Avail_Win
            // 
            this.flp_Avail_Win.AutoSize = true;
            this.flp_Avail_Win.Controls.Add(this.label1);
            this.flp_Avail_Win.Controls.Add(this.list_AvailableWindows);
            this.flp_Avail_Win.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flp_Avail_Win.Location = new System.Drawing.Point(127, 3);
            this.flp_Avail_Win.Name = "flp_Avail_Win";
            this.flp_Avail_Win.Size = new System.Drawing.Size(270, 371);
            this.flp_Avail_Win.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(10, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(10, 0, 150, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 15);
            this.label1.TabIndex = 18;
            this.label1.Text = "Available Windows";
            // 
            // list_AvailableWindows
            // 
            this.list_AvailableWindows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list_AvailableWindows.FormattingEnabled = true;
            this.list_AvailableWindows.Location = new System.Drawing.Point(3, 18);
            this.list_AvailableWindows.Name = "list_AvailableWindows";
            this.list_AvailableWindows.Size = new System.Drawing.Size(264, 350);
            this.list_AvailableWindows.TabIndex = 17;
            // 
            // flowLayoutPanel_btns
            // 
            this.flowLayoutPanel_btns.AutoSize = true;
            this.flowLayoutPanel_btns.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel_btns.Controls.Add(this.gpWindowSerialNo);
            this.flowLayoutPanel_btns.Controls.Add(this.btn_GenerateReport);
            this.flowLayoutPanel_btns.Controls.Add(this.btn_Windows_AddWindow);
            this.flowLayoutPanel_btns.Controls.Add(this.btn_removeAllWindows);
            this.flowLayoutPanel_btns.Controls.Add(this.btn_RemoveWindow);
            this.flowLayoutPanel_btns.Controls.Add(this.btn_replaceWindow);
            this.flowLayoutPanel_btns.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_btns.Location = new System.Drawing.Point(403, 40);
            this.flowLayoutPanel_btns.Margin = new System.Windows.Forms.Padding(3, 40, 3, 3);
            this.flowLayoutPanel_btns.Name = "flowLayoutPanel_btns";
            this.flowLayoutPanel_btns.Size = new System.Drawing.Size(155, 270);
            this.flowLayoutPanel_btns.TabIndex = 28;
            // 
            // gpWindowSerialNo
            // 
            this.gpWindowSerialNo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gpWindowSerialNo.Controls.Add(this.txt_Windows_number);
            this.gpWindowSerialNo.Controls.Add(this.btn_EditWindow);
            this.gpWindowSerialNo.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpWindowSerialNo.ForeColor = System.Drawing.Color.Maroon;
            this.gpWindowSerialNo.Location = new System.Drawing.Point(0, 0);
            this.gpWindowSerialNo.Margin = new System.Windows.Forms.Padding(0);
            this.gpWindowSerialNo.Name = "gpWindowSerialNo";
            this.gpWindowSerialNo.Size = new System.Drawing.Size(148, 82);
            this.gpWindowSerialNo.TabIndex = 23;
            this.gpWindowSerialNo.TabStop = false;
            this.gpWindowSerialNo.Text = "Window Serial Number";
            // 
            // txt_Windows_number
            // 
            this.txt_Windows_number.Location = new System.Drawing.Point(31, 15);
            this.txt_Windows_number.Margin = new System.Windows.Forms.Padding(35, 3, 35, 5);
            this.txt_Windows_number.MaxLength = 3;
            this.txt_Windows_number.Name = "txt_Windows_number";
            this.txt_Windows_number.Size = new System.Drawing.Size(94, 23);
            this.txt_Windows_number.TabIndex = 7;
            this.txt_Windows_number.TextChanged += new System.EventHandler(this.txt_Windows_number_TextChanged);
            this.txt_Windows_number.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Windows_number_KeyPress);
            // 
            // btn_EditWindow
            // 
            this.btn_EditWindow.Location = new System.Drawing.Point(32, 43);
            this.btn_EditWindow.Name = "btn_EditWindow";
            this.btn_EditWindow.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_EditWindow.Size = new System.Drawing.Size(85, 30);
            this.btn_EditWindow.TabIndex = 9;
            this.btn_EditWindow.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_EditWindow.Values.Image")));
            this.btn_EditWindow.Values.Text = "Edit";
            this.btn_EditWindow.Click += new System.EventHandler(this.btn_EditWindow_Click);
            // 
            // btn_GenerateReport
            // 
            this.btn_GenerateReport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_GenerateReport.Location = new System.Drawing.Point(20, 85);
            this.btn_GenerateReport.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.btn_GenerateReport.Name = "btn_GenerateReport";
            this.btn_GenerateReport.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_GenerateReport.Size = new System.Drawing.Size(117, 30);
            this.btn_GenerateReport.TabIndex = 24;
            this.btn_GenerateReport.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_GenerateReport.Values.Image")));
            this.btn_GenerateReport.Values.Text = "Generate Report";
            this.btn_GenerateReport.Visible = false;
            this.btn_GenerateReport.Click += new System.EventHandler(this.btn_GenerateReport_Click);
            // 
            // btn_Windows_AddWindow
            // 
            this.btn_Windows_AddWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Windows_AddWindow.ForeColor = System.Drawing.Color.Green;
            this.btn_Windows_AddWindow.Location = new System.Drawing.Point(35, 121);
            this.btn_Windows_AddWindow.Margin = new System.Windows.Forms.Padding(35, 3, 5, 5);
            this.btn_Windows_AddWindow.Name = "btn_Windows_AddWindow";
            this.btn_Windows_AddWindow.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_Windows_AddWindow.Size = new System.Drawing.Size(85, 30);
            this.btn_Windows_AddWindow.TabIndex = 19;
            this.btn_Windows_AddWindow.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_Windows_AddWindow.Values.Image")));
            this.btn_Windows_AddWindow.Values.Text = "ADD                     ";
            this.btn_Windows_AddWindow.Click += new System.EventHandler(this.btn_Windows_AddWindow_Click);
            // 
            // btn_removeAllWindows
            // 
            this.btn_removeAllWindows.AllowDrop = true;
            this.btn_removeAllWindows.Location = new System.Drawing.Point(35, 159);
            this.btn_removeAllWindows.Margin = new System.Windows.Forms.Padding(35, 3, 35, 5);
            this.btn_removeAllWindows.Name = "btn_removeAllWindows";
            this.btn_removeAllWindows.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_removeAllWindows.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_removeAllWindows.Size = new System.Drawing.Size(85, 30);
            this.btn_removeAllWindows.TabIndex = 22;
            this.btn_removeAllWindows.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_removeAllWindows.Values.Image")));
            this.btn_removeAllWindows.Values.Text = "Remove All";
            this.btn_removeAllWindows.Click += new System.EventHandler(this.btn_removeAllWindows_Click);
            // 
            // btn_RemoveWindow
            // 
            this.btn_RemoveWindow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_RemoveWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RemoveWindow.Location = new System.Drawing.Point(35, 197);
            this.btn_RemoveWindow.Margin = new System.Windows.Forms.Padding(35, 3, 35, 5);
            this.btn_RemoveWindow.Name = "btn_RemoveWindow";
            this.btn_RemoveWindow.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_RemoveWindow.Size = new System.Drawing.Size(85, 30);
            this.btn_RemoveWindow.TabIndex = 20;
            this.btn_RemoveWindow.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_RemoveWindow.Values.Image")));
            this.btn_RemoveWindow.Values.Text = "REMOVE";
            this.btn_RemoveWindow.Click += new System.EventHandler(this.btn_RemoveWindow_Click);
            // 
            // btn_replaceWindow
            // 
            this.btn_replaceWindow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_replaceWindow.Location = new System.Drawing.Point(35, 235);
            this.btn_replaceWindow.Margin = new System.Windows.Forms.Padding(35, 3, 35, 5);
            this.btn_replaceWindow.Name = "btn_replaceWindow";
            this.btn_replaceWindow.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_replaceWindow.Size = new System.Drawing.Size(85, 30);
            this.btn_replaceWindow.TabIndex = 21;
            this.btn_replaceWindow.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_replaceWindow.Values.Image")));
            this.btn_replaceWindow.Values.Text = "REPLACE";
            this.btn_replaceWindow.Click += new System.EventHandler(this.btn_replaceWindow_Click);
            // 
            // flp_SelectedWin
            // 
            this.flp_SelectedWin.AutoSize = true;
            this.flp_SelectedWin.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flp_SelectedWin.Controls.Add(this.lblSelectedWindows);
            this.flp_SelectedWin.Controls.Add(this.grid_SelectedWindows);
            this.flp_SelectedWin.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flp_SelectedWin.Location = new System.Drawing.Point(564, 3);
            this.flp_SelectedWin.Name = "flp_SelectedWin";
            this.flp_SelectedWin.Size = new System.Drawing.Size(348, 369);
            this.flp_SelectedWin.TabIndex = 31;
            // 
            // lblSelectedWindows
            // 
            this.lblSelectedWindows.AutoSize = true;
            this.lblSelectedWindows.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedWindows.ForeColor = System.Drawing.Color.Black;
            this.lblSelectedWindows.Location = new System.Drawing.Point(10, 0);
            this.lblSelectedWindows.Margin = new System.Windows.Forms.Padding(10, 0, 230, 0);
            this.lblSelectedWindows.Name = "lblSelectedWindows";
            this.lblSelectedWindows.Size = new System.Drawing.Size(108, 15);
            this.lblSelectedWindows.TabIndex = 26;
            this.lblSelectedWindows.Text = "Selected Windows";
            // 
            // grid_SelectedWindows
            // 
            this.grid_SelectedWindows.AllowUserToAddRows = false;
            this.grid_SelectedWindows.AllowUserToDeleteRows = false;
            this.grid_SelectedWindows.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grid_SelectedWindows.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.grid_SelectedWindows.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.grid_SelectedWindows.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_Number,
            this.col_Name});
            this.grid_SelectedWindows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid_SelectedWindows.Location = new System.Drawing.Point(3, 18);
            this.grid_SelectedWindows.MaximumSize = new System.Drawing.Size(450, 450);
            this.grid_SelectedWindows.Name = "grid_SelectedWindows";
            this.grid_SelectedWindows.ReadOnly = true;
            this.grid_SelectedWindows.RowHeadersWidth = 30;
            this.grid_SelectedWindows.Size = new System.Drawing.Size(342, 348);
            this.grid_SelectedWindows.StateCommon.Background.Color1 = System.Drawing.Color.Gainsboro;
            this.grid_SelectedWindows.StateCommon.Background.Color2 = System.Drawing.Color.LightGray;
            this.grid_SelectedWindows.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
            this.grid_SelectedWindows.StateNormal.Background.Color1 = System.Drawing.Color.WhiteSmoke;
            this.grid_SelectedWindows.StateNormal.Background.Color2 = System.Drawing.Color.Gainsboro;
            this.grid_SelectedWindows.TabIndex = 25;
            this.grid_SelectedWindows.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grid_SelectedWindows_RowHeaderMouseClick);
            // 
            // col_Number
            // 
            this.col_Number.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_Number.HeaderText = "Number";
            this.col_Number.MinimumWidth = 50;
            this.col_Number.Name = "col_Number";
            this.col_Number.ReadOnly = true;
            this.col_Number.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.col_Number.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // col_Name
            // 
            this.col_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_Name.FillWeight = 350F;
            this.col_Name.HeaderText = "Name";
            this.col_Name.MinimumWidth = 175;
            this.col_Name.Name = "col_Name";
            this.col_Name.ReadOnly = true;
            this.col_Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ucDisplayWindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.mainPanel);
            this.DoubleBuffered = true;
            this.Name = "ucDisplayWindows";
            this.Size = new System.Drawing.Size(960, 403);
            this.Load += new System.EventHandler(this.UcDisplayWindows_Load);
            this.Enter += new System.EventHandler(this.ucDisplayWindows_Enter);
            this.Leave += new System.EventHandler(this.ucDisplayWindows_Leave);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.gpDisplayWindows.ResumeLayout(false);
            this.gpDisplayWindows.PerformLayout();
            this.flp_Main_Panel.ResumeLayout(false);
            this.flp_Main_Panel.PerformLayout();
            this.fLPanel_avail_Category.ResumeLayout(false);
            this.gp_Avail_Category.ResumeLayout(false);
            this.gp_GeneralSettings.ResumeLayout(false);
            this.gp_GeneralSettings.PerformLayout();
            this.fLPanel_GenSettings.ResumeLayout(false);
            this.fLPanel_GenSettings.PerformLayout();
            this.pnl_gen_Scroll_Time.ResumeLayout(false);
            this.pnl_gen_Scroll_Time.PerformLayout();
            this.pnl_Gen_Chks.ResumeLayout(false);
            this.pnl_Gen_Chks.PerformLayout();
            this.flp_Avail_Win.ResumeLayout(false);
            this.flp_Avail_Win.PerformLayout();
            this.flowLayoutPanel_btns.ResumeLayout(false);
            this.gpWindowSerialNo.ResumeLayout(false);
            this.gpWindowSerialNo.PerformLayout();
            this.flp_SelectedWin.ResumeLayout(false);
            this.flp_SelectedWin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_SelectedWindows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox gp_GeneralSettings;
        private Label lblUnit;
        private CheckBox check_DoubleDot;
        private CheckBox check_DisplayOBIS_D;
        private CheckBox check_DisplayOBIS_E;
        private CheckBox check_DisplayOBIS_C;
        private CheckBox check_SingleDot;
        private RadioButton radio_windows_Test;
        private RadioButton radio_windows_Alternate;
        private RadioButton radio_windows_Normal;
        private Label lbl_NumberFormat;
        private ComboBox combo_Windows_NumberFormat;
        private Label lblScrollTime;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox list_AvailableWindowsCategory;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox list_AvailableWindows;
        private Label label1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_GenerateReport;
        private GroupBox gpWindowSerialNo;
        private TextBox txt_Windows_number;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_EditWindow;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_removeAllWindows;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_replaceWindow;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_RemoveWindow;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_Windows_AddWindow;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid_SelectedWindows;
        private Label lblSelectedWindows;
        private ErrorProvider errorProvider;
        private DateTimePicker txt_Windows_ScrollTime_;
        private Label lbl_ScrollTimeLimit;
        public GroupBox gpDisplayWindows;
        public Panel mainPanel;
        private GroupBox gp_Avail_Category;
        private FlowLayoutPanel flowLayoutPanel_btns;
        private FlowLayoutPanel fLPanel_avail_Category;
        private FlowLayoutPanel fLPanel_GenSettings;
        private Panel pnl_Gen_Chks;
        private Panel pnl_gen_Scroll_Time;
        private FlowLayoutPanel flp_Avail_Win;
        private FlowLayoutPanel flp_SelectedWin;
        private FlowLayoutPanel flp_Main_Panel;
        private DataGridViewTextBoxColumn col_Number;
        private DataGridViewTextBoxColumn col_Name;
        // private System.Windows.Forms.Button btn_Windows_AddWindow;
        // private System.Windows.Forms.Button btn_EditWindow;
        //     private System.Windows.Forms.Button btn_RemoveWindow;
        //      private System.Windows.Forms.Button btn_replaceWindow;
        //private ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid_SelectedWindows;
        
        //private System.Windows.Forms.DataGridView grid_SelectedWindows;
        // private Button btn_removeAllWindows;
        

    }
}
