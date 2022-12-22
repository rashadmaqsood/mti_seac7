namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    partial class ucPasswords
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
            this.gpPasswords = new System.Windows.Forms.GroupBox();
            this.txt_Passwords_Electrical = new System.Windows.Forms.TextBox();
            this.txt_Passwords_Managerial = new System.Windows.Forms.TextBox();
            this.lbl_Passwords_ManagerialLogicalDevice = new System.Windows.Forms.Label();
            this.lbl_Passwords_ElectricalLogicalDevice = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.gpPasswords.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // gpPasswords
            // 
            this.gpPasswords.AutoSize = true;
            this.gpPasswords.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gpPasswords.Controls.Add(this.txt_Passwords_Electrical);
            this.gpPasswords.Controls.Add(this.txt_Passwords_Managerial);
            this.gpPasswords.Controls.Add(this.lbl_Passwords_ManagerialLogicalDevice);
            this.gpPasswords.Controls.Add(this.lbl_Passwords_ElectricalLogicalDevice);
            this.gpPasswords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpPasswords.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.gpPasswords.ForeColor = System.Drawing.Color.Maroon;
            this.gpPasswords.Location = new System.Drawing.Point(0, 0);
            this.gpPasswords.Name = "gpPasswords";
            this.gpPasswords.Size = new System.Drawing.Size(332, 83);
            this.gpPasswords.TabIndex = 12;
            this.gpPasswords.TabStop = false;
            this.gpPasswords.Text = "Passwords";
            // 
            // txt_Passwords_Electrical
            // 
            this.txt_Passwords_Electrical.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Passwords_Electrical.Location = new System.Drawing.Point(187, 52);
            this.txt_Passwords_Electrical.MaxLength = 20;
            this.txt_Passwords_Electrical.Name = "txt_Passwords_Electrical";
            this.txt_Passwords_Electrical.PasswordChar = '*';
            this.txt_Passwords_Electrical.Size = new System.Drawing.Size(137, 23);
            this.txt_Passwords_Electrical.TabIndex = 1;
            this.txt_Passwords_Electrical.Text = "microtek";
            this.txt_Passwords_Electrical.Visible = false;
            this.txt_Passwords_Electrical.Leave += new System.EventHandler(this.txt_Passwords_Managerial_Leave_1);
            // 
            // txt_Passwords_Managerial
            // 
            this.txt_Passwords_Managerial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Passwords_Managerial.Location = new System.Drawing.Point(215, 21);
            this.txt_Passwords_Managerial.MaxLength = 19;
            this.txt_Passwords_Managerial.Name = "txt_Passwords_Managerial";
            this.txt_Passwords_Managerial.PasswordChar = '*';
            this.txt_Passwords_Managerial.Size = new System.Drawing.Size(109, 23);
            this.txt_Passwords_Managerial.TabIndex = 0;
            this.txt_Passwords_Managerial.Text = "microtek";
            this.txt_Passwords_Managerial.Leave += new System.EventHandler(this.txt_Passwords_Managerial_Leave_1);
            // 
            // lbl_Passwords_ManagerialLogicalDevice
            // 
            this.lbl_Passwords_ManagerialLogicalDevice.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Passwords_ManagerialLogicalDevice.Location = new System.Drawing.Point(8, 24);
            this.lbl_Passwords_ManagerialLogicalDevice.Name = "lbl_Passwords_ManagerialLogicalDevice";
            this.lbl_Passwords_ManagerialLogicalDevice.Size = new System.Drawing.Size(169, 15);
            this.lbl_Passwords_ManagerialLogicalDevice.TabIndex = 36;
            this.lbl_Passwords_ManagerialLogicalDevice.Text = "Current Association Password";
            // 
            // lbl_Passwords_ElectricalLogicalDevice
            // 
            this.lbl_Passwords_ElectricalLogicalDevice.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Passwords_ElectricalLogicalDevice.Location = new System.Drawing.Point(8, 52);
            this.lbl_Passwords_ElectricalLogicalDevice.Name = "lbl_Passwords_ElectricalLogicalDevice";
            this.lbl_Passwords_ElectricalLogicalDevice.Size = new System.Drawing.Size(128, 15);
            this.lbl_Passwords_ElectricalLogicalDevice.TabIndex = 36;
            this.lbl_Passwords_ElectricalLogicalDevice.Text = "Elecrical Logical Device";
            this.lbl_Passwords_ElectricalLogicalDevice.Visible = false;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ucPasswords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.gpPasswords);
            this.Name = "ucPasswords";
            this.Size = new System.Drawing.Size(332, 83);
            this.Load += new System.EventHandler(this.ucPasswords_Load);
            this.Leave += new System.EventHandler(this.ucPasswords_Leave);
            this.gpPasswords.ResumeLayout(false);
            this.gpPasswords.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gpPasswords;
        private System.Windows.Forms.TextBox txt_Passwords_Managerial;
        private System.Windows.Forms.TextBox txt_Passwords_Electrical;
        private System.Windows.Forms.Label lbl_Passwords_ManagerialLogicalDevice;
        private System.Windows.Forms.Label lbl_Passwords_ElectricalLogicalDevice;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
