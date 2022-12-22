namespace SmartEyeControl_7
{
    partial class superAdminPanel
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
            this.initialMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewRightsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnl_Create_Edit = new System.Windows.Forms.FlowLayoutPanel();
            this.gp_UserInfo = new System.Windows.Forms.GroupBox();
            this.lbl_Active = new System.Windows.Forms.Label();
            this.chk_Activate = new System.Windows.Forms.CheckBox();
            this.lbl_UserType = new System.Windows.Forms.Label();
            this.combo_userType = new System.Windows.Forms.ComboBox();
            this.lbl_UserName = new System.Windows.Forms.Label();
            this.txt_userName = new System.Windows.Forms.TextBox();
            this.lbl_UserPassword = new System.Windows.Forms.Label();
            this.txt_userPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_userPasswordConfirm = new System.Windows.Forms.TextBox();
            this.lbl_Father_Name = new System.Windows.Forms.Label();
            this.txt_fatherName = new System.Windows.Forms.TextBox();
            this.lbl_Address = new System.Windows.Forms.Label();
            this.txt_address = new System.Windows.Forms.RichTextBox();
            this.lbl_EmpCode = new System.Windows.Forms.Label();
            this.txt_employeeCode = new System.Windows.Forms.TextBox();
            this.lbl_Phone1 = new System.Windows.Forms.Label();
            this.txt_phone1 = new System.Windows.Forms.TextBox();
            this.lbl_Phone2 = new System.Windows.Forms.Label();
            this.txt_phone2 = new System.Windows.Forms.TextBox();
            this.lbl_Mobile = new System.Windows.Forms.Label();
            this.txt_mobile = new System.Windows.Forms.TextBox();
            this.lbl_faxNumber = new System.Windows.Forms.Label();
            this.txt_faxNumber = new System.Windows.Forms.TextBox();
            this.lbl_NID = new System.Windows.Forms.Label();
            this.txt_NID = new System.Windows.Forms.TextBox();
            this.gp_AccessRights = new System.Windows.Forms.GroupBox();
            this.lbl_Spacer = new System.Windows.Forms.Label();
            this.fLP_AccessRights_Main = new System.Windows.Forms.FlowLayoutPanel();
            this.fLP_Buttons = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_addUser = new System.Windows.Forms.Button();
            this.btn_editUser = new System.Windows.Forms.Button();
            this.btn_deleteUser = new System.Windows.Forms.Button();
            this.gp_User = new System.Windows.Forms.GroupBox();
            this.dataGridView_Users = new System.Windows.Forms.DataGridView();
            this.UID = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.User_Name = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.User_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblHeading = new System.Windows.Forms.Label();
            this.fLP_Main = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtgAccessGroups = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Identifier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Role = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.initialMenuStrip.SuspendLayout();
            this.pnl_Create_Edit.SuspendLayout();
            this.gp_UserInfo.SuspendLayout();
            this.gp_AccessRights.SuspendLayout();
            this.fLP_Buttons.SuspendLayout();
            this.gp_User.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Users)).BeginInit();
            this.fLP_Main.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAccessGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // initialMenuStrip
            // 
            this.initialMenuStrip.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.initialMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewRightsToolStripMenuItem});
            this.initialMenuStrip.Name = "initialMenuStrip";
            this.initialMenuStrip.Size = new System.Drawing.Size(158, 26);
            // 
            // addNewRightsToolStripMenuItem
            // 
            this.addNewRightsToolStripMenuItem.Name = "addNewRightsToolStripMenuItem";
            this.addNewRightsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.addNewRightsToolStripMenuItem.Text = "Add New Rights";
            this.addNewRightsToolStripMenuItem.Click += new System.EventHandler(this.Add_Click);
            // 
            // pnl_Create_Edit
            // 
            this.pnl_Create_Edit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnl_Create_Edit.Controls.Add(this.gp_UserInfo);
            this.pnl_Create_Edit.Controls.Add(this.gp_AccessRights);
            this.pnl_Create_Edit.Location = new System.Drawing.Point(3, 269);
            this.pnl_Create_Edit.Name = "pnl_Create_Edit";
            this.pnl_Create_Edit.Size = new System.Drawing.Size(1060, 312);
            this.pnl_Create_Edit.TabIndex = 12;
            // 
            // gp_UserInfo
            // 
            this.gp_UserInfo.Controls.Add(this.lbl_Active);
            this.gp_UserInfo.Controls.Add(this.txt_NID);
            this.gp_UserInfo.Controls.Add(this.txt_faxNumber);
            this.gp_UserInfo.Controls.Add(this.lbl_NID);
            this.gp_UserInfo.Controls.Add(this.txt_mobile);
            this.gp_UserInfo.Controls.Add(this.lbl_faxNumber);
            this.gp_UserInfo.Controls.Add(this.txt_phone2);
            this.gp_UserInfo.Controls.Add(this.lbl_Mobile);
            this.gp_UserInfo.Controls.Add(this.lbl_Phone1);
            this.gp_UserInfo.Controls.Add(this.lbl_Phone2);
            this.gp_UserInfo.Controls.Add(this.txt_phone1);
            this.gp_UserInfo.Controls.Add(this.lbl_EmpCode);
            this.gp_UserInfo.Controls.Add(this.txt_employeeCode);
            this.gp_UserInfo.Controls.Add(this.txt_address);
            this.gp_UserInfo.Controls.Add(this.txt_fatherName);
            this.gp_UserInfo.Controls.Add(this.lbl_Address);
            this.gp_UserInfo.Controls.Add(this.txt_userPasswordConfirm);
            this.gp_UserInfo.Controls.Add(this.lbl_Father_Name);
            this.gp_UserInfo.Controls.Add(this.txt_userPassword);
            this.gp_UserInfo.Controls.Add(this.label1);
            this.gp_UserInfo.Controls.Add(this.txt_userName);
            this.gp_UserInfo.Controls.Add(this.lbl_UserPassword);
            this.gp_UserInfo.Controls.Add(this.lbl_UserName);
            this.gp_UserInfo.Controls.Add(this.combo_userType);
            this.gp_UserInfo.Controls.Add(this.lbl_UserType);
            this.gp_UserInfo.Controls.Add(this.chk_Activate);
            this.gp_UserInfo.Location = new System.Drawing.Point(3, 3);
            this.gp_UserInfo.Name = "gp_UserInfo";
            this.gp_UserInfo.Size = new System.Drawing.Size(410, 304);
            this.gp_UserInfo.TabIndex = 17;
            this.gp_UserInfo.TabStop = false;
            this.gp_UserInfo.Text = "User Info";
            this.gp_UserInfo.Leave += new System.EventHandler(this.gp_UserInfo_Leave);
            // 
            // lbl_Active
            // 
            this.lbl_Active.AutoSize = true;
            this.lbl_Active.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Active.Location = new System.Drawing.Point(71, 275);
            this.lbl_Active.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_Active.Name = "lbl_Active";
            this.lbl_Active.Size = new System.Drawing.Size(134, 16);
            this.lbl_Active.TabIndex = 19;
            this.lbl_Active.Text = "Activate/Freeze User";
            // 
            // chk_Activate
            // 
            this.chk_Activate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chk_Activate.AutoSize = true;
            this.chk_Activate.Location = new System.Drawing.Point(260, 277);
            this.chk_Activate.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.chk_Activate.Name = "chk_Activate";
            this.chk_Activate.Size = new System.Drawing.Size(15, 14);
            this.chk_Activate.TabIndex = 20;
            this.chk_Activate.UseVisualStyleBackColor = true;
            this.chk_Activate.CheckedChanged += new System.EventHandler(this.chk_Activate_CheckedChanged);
            // 
            // lbl_UserType
            // 
            this.lbl_UserType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_UserType.Location = new System.Drawing.Point(78, 19);
            this.lbl_UserType.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_UserType.Name = "lbl_UserType";
            this.lbl_UserType.Size = new System.Drawing.Size(63, 16);
            this.lbl_UserType.TabIndex = 0;
            this.lbl_UserType.Text = "User Type";
            // 
            // combo_userType
            // 
            this.combo_userType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_userType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combo_userType.FormattingEnabled = true;
            this.combo_userType.Items.AddRange(new object[] {
            "SuperAdmin",
            "Admin",
            "Inspector",
            "Reader",
            "Custom"});
            this.combo_userType.Location = new System.Drawing.Point(146, 14);
            this.combo_userType.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.combo_userType.Name = "combo_userType";
            this.combo_userType.Size = new System.Drawing.Size(151, 24);
            this.combo_userType.TabIndex = 0;
            this.combo_userType.SelectedIndexChanged += new System.EventHandler(this.combo_userType_SelectedIndexChanged);
            // 
            // lbl_UserName
            // 
            this.lbl_UserName.AutoSize = true;
            this.lbl_UserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_UserName.Location = new System.Drawing.Point(10, 52);
            this.lbl_UserName.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_UserName.Name = "lbl_UserName";
            this.lbl_UserName.Size = new System.Drawing.Size(64, 13);
            this.lbl_UserName.TabIndex = 0;
            this.lbl_UserName.Text = "User Name*";
            // 
            // txt_userName
            // 
            this.txt_userName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_userName.Location = new System.Drawing.Point(80, 49);
            this.txt_userName.Margin = new System.Windows.Forms.Padding(33, 0, 0, 0);
            this.txt_userName.Name = "txt_userName";
            this.txt_userName.Size = new System.Drawing.Size(105, 20);
            this.txt_userName.TabIndex = 1;
            this.toolTip.SetToolTip(this.txt_userName, "Require*,Length [6-25]");
            this.txt_userName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_userName_KeyPress);
            this.txt_userName.Leave += new System.EventHandler(this.txt_userName_Leave);
            // 
            // lbl_UserPassword
            // 
            this.lbl_UserPassword.AutoSize = true;
            this.lbl_UserPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_UserPassword.Location = new System.Drawing.Point(10, 80);
            this.lbl_UserPassword.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_UserPassword.Name = "lbl_UserPassword";
            this.lbl_UserPassword.Size = new System.Drawing.Size(57, 13);
            this.lbl_UserPassword.TabIndex = 0;
            this.lbl_UserPassword.Text = "Password*";
            // 
            // txt_userPassword
            // 
            this.txt_userPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_userPassword.Location = new System.Drawing.Point(80, 75);
            this.txt_userPassword.Margin = new System.Windows.Forms.Padding(38, 0, 0, 0);
            this.txt_userPassword.Name = "txt_userPassword";
            this.txt_userPassword.PasswordChar = '*';
            this.txt_userPassword.Size = new System.Drawing.Size(105, 20);
            this.txt_userPassword.TabIndex = 2;
            this.toolTip.SetToolTip(this.txt_userPassword, "Require*, Valid Length [6-25]");
            this.txt_userPassword.Leave += new System.EventHandler(this.txt_userPassword_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(193, 77);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Retype Password*";
            // 
            // txt_userPasswordConfirm
            // 
            this.txt_userPasswordConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_userPasswordConfirm.Location = new System.Drawing.Point(290, 73);
            this.txt_userPasswordConfirm.Margin = new System.Windows.Forms.Padding(0);
            this.txt_userPasswordConfirm.Name = "txt_userPasswordConfirm";
            this.txt_userPasswordConfirm.PasswordChar = '*';
            this.txt_userPasswordConfirm.Size = new System.Drawing.Size(114, 20);
            this.txt_userPasswordConfirm.TabIndex = 2;
            this.toolTip.SetToolTip(this.txt_userPasswordConfirm, "Require*, Valid Length [6-25]");
            this.txt_userPasswordConfirm.Leave += new System.EventHandler(this.txt_userPassword_Leave);
            // 
            // lbl_Father_Name
            // 
            this.lbl_Father_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Father_Name.Location = new System.Drawing.Point(192, 52);
            this.lbl_Father_Name.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_Father_Name.Name = "lbl_Father_Name";
            this.lbl_Father_Name.Size = new System.Drawing.Size(79, 16);
            this.lbl_Father_Name.TabIndex = 4;
            this.lbl_Father_Name.Text = "Father\'s Name";
            // 
            // txt_fatherName
            // 
            this.txt_fatherName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_fatherName.Location = new System.Drawing.Point(276, 49);
            this.txt_fatherName.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.txt_fatherName.Name = "txt_fatherName";
            this.txt_fatherName.Size = new System.Drawing.Size(126, 20);
            this.txt_fatherName.TabIndex = 1;
            this.toolTip.SetToolTip(this.txt_fatherName, "Valid Length [6-25]");
            this.txt_fatherName.Leave += new System.EventHandler(this.txt_fatherName_Leave);
            // 
            // lbl_Address
            // 
            this.lbl_Address.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Address.Location = new System.Drawing.Point(17, 144);
            this.lbl_Address.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_Address.Name = "lbl_Address";
            this.lbl_Address.Size = new System.Drawing.Size(59, 16);
            this.lbl_Address.TabIndex = 5;
            this.lbl_Address.Text = "Address";
            // 
            // txt_address
            // 
            this.txt_address.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_address.Location = new System.Drawing.Point(81, 144);
            this.txt_address.Margin = new System.Windows.Forms.Padding(37, 0, 0, 0);
            this.txt_address.Name = "txt_address";
            this.txt_address.Size = new System.Drawing.Size(216, 48);
            this.txt_address.TabIndex = 13;
            this.txt_address.Text = "";
            this.toolTip.SetToolTip(this.txt_address, "Valid Length [6-100]");
            this.txt_address.Leave += new System.EventHandler(this.txt_address_Leave);
            // 
            // lbl_EmpCode
            // 
            this.lbl_EmpCode.AutoSize = true;
            this.lbl_EmpCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_EmpCode.Location = new System.Drawing.Point(12, 110);
            this.lbl_EmpCode.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_EmpCode.Name = "lbl_EmpCode";
            this.lbl_EmpCode.Size = new System.Drawing.Size(85, 13);
            this.lbl_EmpCode.TabIndex = 11;
            this.lbl_EmpCode.Text = "Employee Code*";
            // 
            // txt_employeeCode
            // 
            this.txt_employeeCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_employeeCode.Location = new System.Drawing.Point(106, 107);
            this.txt_employeeCode.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.txt_employeeCode.Name = "txt_employeeCode";
            this.txt_employeeCode.Size = new System.Drawing.Size(99, 20);
            this.txt_employeeCode.TabIndex = 1;
            this.toolTip.SetToolTip(this.txt_employeeCode, "Require*, Valid Length [4-25]");
            this.txt_employeeCode.Leave += new System.EventHandler(this.txt_employeeCode_Leave);
            // 
            // lbl_Phone1
            // 
            this.lbl_Phone1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Phone1.Location = new System.Drawing.Point(12, 210);
            this.lbl_Phone1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_Phone1.Name = "lbl_Phone1";
            this.lbl_Phone1.Size = new System.Drawing.Size(67, 16);
            this.lbl_Phone1.TabIndex = 6;
            this.lbl_Phone1.Text = "Phone # 1";
            // 
            // txt_phone1
            // 
            this.txt_phone1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_phone1.Location = new System.Drawing.Point(76, 206);
            this.txt_phone1.Margin = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.txt_phone1.Name = "txt_phone1";
            this.txt_phone1.Size = new System.Drawing.Size(101, 20);
            this.txt_phone1.TabIndex = 1;
            this.toolTip.SetToolTip(this.txt_phone1, "Valid Format +92-42-xxxxxxxx");
            this.txt_phone1.Leave += new System.EventHandler(this.txt_phone1_Leave);
            // 
            // lbl_Phone2
            // 
            this.lbl_Phone2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Phone2.Location = new System.Drawing.Point(197, 209);
            this.lbl_Phone2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_Phone2.Name = "lbl_Phone2";
            this.lbl_Phone2.Size = new System.Drawing.Size(67, 16);
            this.lbl_Phone2.TabIndex = 7;
            this.lbl_Phone2.Text = "Phone # 2";
            // 
            // txt_phone2
            // 
            this.txt_phone2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_phone2.Location = new System.Drawing.Point(260, 205);
            this.txt_phone2.Margin = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.txt_phone2.Name = "txt_phone2";
            this.txt_phone2.Size = new System.Drawing.Size(101, 20);
            this.txt_phone2.TabIndex = 1;
            this.toolTip.SetToolTip(this.txt_phone2, "Valid Format +92-42-xxxxxxxx");
            this.txt_phone2.Leave += new System.EventHandler(this.txt_phone2_Leave);
            // 
            // lbl_Mobile
            // 
            this.lbl_Mobile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Mobile.Location = new System.Drawing.Point(12, 238);
            this.lbl_Mobile.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_Mobile.Name = "lbl_Mobile";
            this.lbl_Mobile.Size = new System.Drawing.Size(59, 16);
            this.lbl_Mobile.TabIndex = 8;
            this.lbl_Mobile.Text = "Mobile #";
            // 
            // txt_mobile
            // 
            this.txt_mobile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_mobile.Location = new System.Drawing.Point(76, 234);
            this.txt_mobile.Margin = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.txt_mobile.Name = "txt_mobile";
            this.txt_mobile.Size = new System.Drawing.Size(101, 20);
            this.txt_mobile.TabIndex = 1;
            this.toolTip.SetToolTip(this.txt_mobile, "Valid Format +92-42-xxxxxxxx");
            this.txt_mobile.Leave += new System.EventHandler(this.txt_mobile_Leave);
            // 
            // lbl_faxNumber
            // 
            this.lbl_faxNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_faxNumber.Location = new System.Drawing.Point(203, 238);
            this.lbl_faxNumber.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_faxNumber.Name = "lbl_faxNumber";
            this.lbl_faxNumber.Size = new System.Drawing.Size(40, 16);
            this.lbl_faxNumber.TabIndex = 9;
            this.lbl_faxNumber.Text = "Fax #";
            // 
            // txt_faxNumber
            // 
            this.txt_faxNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_faxNumber.Location = new System.Drawing.Point(260, 235);
            this.txt_faxNumber.Margin = new System.Windows.Forms.Padding(43, 0, 0, 0);
            this.txt_faxNumber.Name = "txt_faxNumber";
            this.txt_faxNumber.Size = new System.Drawing.Size(101, 20);
            this.txt_faxNumber.TabIndex = 1;
            this.toolTip.SetToolTip(this.txt_faxNumber, "Valid Format +92-42-xxxxxxxx");
            this.txt_faxNumber.Leave += new System.EventHandler(this.txt_faxNumber_Leave);
            // 
            // lbl_NID
            // 
            this.lbl_NID.AutoSize = true;
            this.lbl_NID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NID.Location = new System.Drawing.Point(226, 111);
            this.lbl_NID.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbl_NID.Name = "lbl_NID";
            this.lbl_NID.Size = new System.Drawing.Size(30, 13);
            this.lbl_NID.TabIndex = 10;
            this.lbl_NID.Text = "NID*";
            // 
            // txt_NID
            // 
            this.txt_NID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_NID.Location = new System.Drawing.Point(276, 108);
            this.txt_NID.Margin = new System.Windows.Forms.Padding(50, 0, 0, 0);
            this.txt_NID.Name = "txt_NID";
            this.txt_NID.Size = new System.Drawing.Size(110, 20);
            this.txt_NID.TabIndex = 1;
            this.toolTip.SetToolTip(this.txt_NID, "Require*, Valid Format 00000-0000000-0");
            this.txt_NID.Leave += new System.EventHandler(this.txt_NID_Leave);
            // 
            // gp_AccessRights
            // 
            this.gp_AccessRights.Controls.Add(this.lbl_Spacer);
            this.gp_AccessRights.Controls.Add(this.fLP_AccessRights_Main);
            this.gp_AccessRights.Location = new System.Drawing.Point(419, 3);
            this.gp_AccessRights.Name = "gp_AccessRights";
            this.gp_AccessRights.Size = new System.Drawing.Size(638, 304);
            this.gp_AccessRights.TabIndex = 15;
            this.gp_AccessRights.TabStop = false;
            this.gp_AccessRights.Text = "Access Rights";
            // 
            // lbl_Spacer
            // 
            this.lbl_Spacer.AutoSize = true;
            this.lbl_Spacer.Location = new System.Drawing.Point(11, 345);
            this.lbl_Spacer.Margin = new System.Windows.Forms.Padding(3, 0, 400, 0);
            this.lbl_Spacer.Name = "lbl_Spacer";
            this.lbl_Spacer.Size = new System.Drawing.Size(0, 13);
            this.lbl_Spacer.TabIndex = 16;
            // 
            // fLP_AccessRights_Main
            // 
            this.fLP_AccessRights_Main.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_AccessRights_Main.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLP_AccessRights_Main.Location = new System.Drawing.Point(6, 16);
            this.fLP_AccessRights_Main.Name = "fLP_AccessRights_Main";
            this.fLP_AccessRights_Main.Size = new System.Drawing.Size(641, 285);
            this.fLP_AccessRights_Main.TabIndex = 15;
            // 
            // fLP_Buttons
            // 
            this.fLP_Buttons.AutoSize = true;
            this.fLP_Buttons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_Buttons.Controls.Add(this.btn_addUser);
            this.fLP_Buttons.Controls.Add(this.btn_editUser);
            this.fLP_Buttons.Controls.Add(this.btn_deleteUser);
            this.fLP_Buttons.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLP_Buttons.Location = new System.Drawing.Point(419, 46);
            this.fLP_Buttons.Name = "fLP_Buttons";
            this.fLP_Buttons.Size = new System.Drawing.Size(78, 174);
            this.fLP_Buttons.TabIndex = 14;
            // 
            // btn_addUser
            // 
            this.btn_addUser.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_addUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_addUser.Location = new System.Drawing.Point(10, 3);
            this.btn_addUser.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btn_addUser.Name = "btn_addUser";
            this.btn_addUser.Size = new System.Drawing.Size(65, 49);
            this.btn_addUser.TabIndex = 3;
            this.btn_addUser.Text = "New User";
            this.btn_addUser.UseVisualStyleBackColor = true;
            this.btn_addUser.Click += new System.EventHandler(this.btn_addUser_Click);
            // 
            // btn_editUser
            // 
            this.btn_editUser.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_editUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.btn_editUser.Location = new System.Drawing.Point(10, 58);
            this.btn_editUser.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btn_editUser.Name = "btn_editUser";
            this.btn_editUser.Size = new System.Drawing.Size(65, 48);
            this.btn_editUser.TabIndex = 13;
            this.btn_editUser.Text = "Edit User";
            this.btn_editUser.UseVisualStyleBackColor = true;
            this.btn_editUser.Click += new System.EventHandler(this.btn_editUser_Click);
            // 
            // btn_deleteUser
            // 
            this.btn_deleteUser.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_deleteUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.btn_deleteUser.Location = new System.Drawing.Point(10, 112);
            this.btn_deleteUser.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btn_deleteUser.Name = "btn_deleteUser";
            this.btn_deleteUser.Size = new System.Drawing.Size(65, 59);
            this.btn_deleteUser.TabIndex = 2;
            this.btn_deleteUser.Text = "Delete User";
            this.btn_deleteUser.UseVisualStyleBackColor = true;
            this.btn_deleteUser.Click += new System.EventHandler(this.btn_deleteUser_Click);
            // 
            // gp_User
            // 
            this.gp_User.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gp_User.Controls.Add(this.dataGridView_Users);
            this.gp_User.Location = new System.Drawing.Point(9, 0);
            this.gp_User.Name = "gp_User";
            this.gp_User.Size = new System.Drawing.Size(408, 220);
            this.gp_User.TabIndex = 17;
            this.gp_User.TabStop = false;
            this.gp_User.Text = "Users";
            // 
            // dataGridView_Users
            // 
            this.dataGridView_Users.AllowUserToAddRows = false;
            this.dataGridView_Users.AllowUserToDeleteRows = false;
            this.dataGridView_Users.AllowUserToResizeColumns = false;
            this.dataGridView_Users.AllowUserToResizeRows = false;
            this.dataGridView_Users.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Users.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Users.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UID,
            this.User_Name,
            this.User_Type});
            this.dataGridView_Users.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Users.Location = new System.Drawing.Point(3, 16);
            this.dataGridView_Users.Name = "dataGridView_Users";
            this.dataGridView_Users.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_Users.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Users.Size = new System.Drawing.Size(402, 201);
            this.dataGridView_Users.TabIndex = 15;
            this.dataGridView_Users.VirtualMode = true;
            this.dataGridView_Users.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Users_RowEnter);
            this.dataGridView_Users.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dataGridView_Users_CellValueNeeded);
            this.dataGridView_Users.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Users_RowEnter);
            // 
            // UID
            // 
            this.UID.DataPropertyName = "user_ID";
            this.UID.FillWeight = 55.8137F;
            this.UID.HeaderText = "Id";
            this.UID.Name = "UID";
            this.UID.ReadOnly = true;
            this.UID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UID.Width = 67;
            // 
            // User_Name
            // 
            this.User_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.User_Name.DataPropertyName = "user_name";
            this.User_Name.FillWeight = 122.3589F;
            this.User_Name.HeaderText = "User Name";
            this.User_Name.Name = "User_Name";
            this.User_Name.ReadOnly = true;
            this.User_Name.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.User_Name.Width = 146;
            // 
            // User_Type
            // 
            this.User_Type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.User_Type.FillWeight = 121.8274F;
            this.User_Type.HeaderText = "User Type";
            this.User_Type.Name = "User_Type";
            this.User_Type.ReadOnly = true;
            this.User_Type.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.User_Type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lblHeading
            // 
            this.lblHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.Navy;
            this.lblHeading.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblHeading.Location = new System.Drawing.Point(350, 3);
            this.lblHeading.Margin = new System.Windows.Forms.Padding(350, 3, 3, 3);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(713, 33);
            this.lblHeading.TabIndex = 18;
            this.lblHeading.Text = "User Management";
            // 
            // fLP_Main
            // 
            this.fLP_Main.AutoSize = true;
            this.fLP_Main.BackColor = System.Drawing.Color.Transparent;
            this.fLP_Main.Controls.Add(this.lblHeading);
            this.fLP_Main.Controls.Add(this.groupBox1);
            this.fLP_Main.Controls.Add(this.pnl_Create_Edit);
            this.fLP_Main.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLP_Main.Location = new System.Drawing.Point(3, 3);
            this.fLP_Main.Name = "fLP_Main";
            this.fLP_Main.Size = new System.Drawing.Size(1066, 584);
            this.fLP_Main.TabIndex = 16;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtgAccessGroups);
            this.groupBox1.Controls.Add(this.gp_User);
            this.groupBox1.Controls.Add(this.fLP_Buttons);
            this.groupBox1.Location = new System.Drawing.Point(3, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1057, 221);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            // 
            // dtgAccessGroups
            // 
            this.dtgAccessGroups.AllowUserToAddRows = false;
            this.dtgAccessGroups.AllowUserToDeleteRows = false;
            this.dtgAccessGroups.AllowUserToResizeRows = false;
            this.dtgAccessGroups.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgAccessGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgAccessGroups.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Identifier,
            this.Role});
            this.dtgAccessGroups.Enabled = false;
            this.dtgAccessGroups.Location = new System.Drawing.Point(500, 16);
            this.dtgAccessGroups.Name = "dtgAccessGroups";
            this.dtgAccessGroups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgAccessGroups.Size = new System.Drawing.Size(551, 201);
            this.dtgAccessGroups.TabIndex = 19;
            this.dtgAccessGroups.Visible = false;
            this.dtgAccessGroups.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dtgAccessGroups_CellContextMenuStripNeeded);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "id";
            this.ID.FillWeight = 46.90797F;
            this.ID.HeaderText = "Id";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // Identifier
            // 
            this.Identifier.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Identifier.DataPropertyName = "identifier";
            this.Identifier.FillWeight = 146.493F;
            this.Identifier.HeaderText = "Access Right Name";
            this.Identifier.Name = "Identifier";
            this.Identifier.ReadOnly = true;
            // 
            // Role
            // 
            this.Role.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Role.DataPropertyName = "role";
            this.Role.FillWeight = 106.599F;
            this.Role.HeaderText = "User\'s Type";
            this.Role.Name = "Role";
            this.Role.ReadOnly = true;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            this.errorProvider.RightToLeft = true;
            // 
            // superAdminPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.fLP_Main);
            this.Name = "superAdminPanel";
            this.Size = new System.Drawing.Size(1072, 626);
            this.Load += new System.EventHandler(this.superAdminPanel_Load);
            this.initialMenuStrip.ResumeLayout(false);
            this.pnl_Create_Edit.ResumeLayout(false);
            this.gp_UserInfo.ResumeLayout(false);
            this.gp_UserInfo.PerformLayout();
            this.gp_AccessRights.ResumeLayout(false);
            this.gp_AccessRights.PerformLayout();
            this.fLP_Buttons.ResumeLayout(false);
            this.gp_User.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Users)).EndInit();
            this.fLP_Main.ResumeLayout(false);
            this.fLP_Main.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAccessGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip initialMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addNewRightsToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel pnl_Create_Edit;
        private System.Windows.Forms.GroupBox gp_UserInfo;
        private System.Windows.Forms.Label lbl_UserType;
        private System.Windows.Forms.ComboBox combo_userType;
        private System.Windows.Forms.Label lbl_UserName;
        private System.Windows.Forms.TextBox txt_userName;
        private System.Windows.Forms.Label lbl_UserPassword;
        private System.Windows.Forms.TextBox txt_userPassword;
        private System.Windows.Forms.Label lbl_Father_Name;
        private System.Windows.Forms.TextBox txt_fatherName;
        private System.Windows.Forms.Label lbl_Address;
        private System.Windows.Forms.RichTextBox txt_address;
        private System.Windows.Forms.Label lbl_EmpCode;
        private System.Windows.Forms.TextBox txt_employeeCode;
        private System.Windows.Forms.Label lbl_Phone1;
        private System.Windows.Forms.TextBox txt_phone1;
        private System.Windows.Forms.Label lbl_Phone2;
        private System.Windows.Forms.TextBox txt_phone2;
        private System.Windows.Forms.Label lbl_Mobile;
        private System.Windows.Forms.TextBox txt_mobile;
        private System.Windows.Forms.Label lbl_faxNumber;
        private System.Windows.Forms.TextBox txt_faxNumber;
        private System.Windows.Forms.Label lbl_NID;
        private System.Windows.Forms.TextBox txt_NID;
        private System.Windows.Forms.GroupBox gp_AccessRights;
        private System.Windows.Forms.FlowLayoutPanel fLP_AccessRights_Main;
        private System.Windows.Forms.FlowLayoutPanel fLP_Buttons;
        private System.Windows.Forms.Button btn_addUser;
        private System.Windows.Forms.Button btn_editUser;
        private System.Windows.Forms.Button btn_deleteUser;
        private System.Windows.Forms.GroupBox gp_User;
        private System.Windows.Forms.DataGridView dataGridView_Users;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn UID;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn User_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn User_Type;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.FlowLayoutPanel fLP_Main;
        private System.Windows.Forms.Label lbl_Spacer;
        internal System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label lbl_Active;
        private System.Windows.Forms.CheckBox chk_Activate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_userPasswordConfirm;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dtgAccessGroups;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Identifier;
        private System.Windows.Forms.DataGridViewTextBoxColumn Role;
    }
}
