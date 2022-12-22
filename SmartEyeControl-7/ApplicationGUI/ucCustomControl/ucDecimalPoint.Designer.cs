namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    partial class ucDecimalPoint
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
            this.gb_DecimalPoint = new System.Windows.Forms.GroupBox();
            this.lbl_DecimalPoint_InstantaneousPower = new System.Windows.Forms.Label();
            this.txt_DecimalPoint_InstantaneousCurrent = new System.Windows.Forms.TextBox();
            this.lbl_DecimalPoint_InstantaneousCurrent = new System.Windows.Forms.Label();
            this.txt_DecimalPoint_InstantaneousMDI = new System.Windows.Forms.TextBox();
            this.lbl_DecimalPoint_InstantaneousMDI = new System.Windows.Forms.Label();
            this.txt_DecimalPoint_InstantaneousPower = new System.Windows.Forms.TextBox();
            this.txt_DecimalPoint_InstantaneousVoltage = new System.Windows.Forms.TextBox();
            this.lbl_DecimalPoint_InstantaneousVoltage = new System.Windows.Forms.Label();
            this.txt_DecimalPoint_BillingKWHMDI = new System.Windows.Forms.TextBox();
            this.lbl_DecimalPoint_BillingKWMDI = new System.Windows.Forms.Label();
            this.txt_DecimalPoint_BillingKWH = new System.Windows.Forms.TextBox();
            this.lbl_DecimalPoint_BillingKWH = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.gb_DecimalPoint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // gb_DecimalPoint
            // 
            this.gb_DecimalPoint.AutoSize = true;
            this.gb_DecimalPoint.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gb_DecimalPoint.Controls.Add(this.lbl_DecimalPoint_InstantaneousPower);
            this.gb_DecimalPoint.Controls.Add(this.txt_DecimalPoint_InstantaneousCurrent);
            this.gb_DecimalPoint.Controls.Add(this.lbl_DecimalPoint_InstantaneousCurrent);
            this.gb_DecimalPoint.Controls.Add(this.txt_DecimalPoint_InstantaneousMDI);
            this.gb_DecimalPoint.Controls.Add(this.lbl_DecimalPoint_InstantaneousMDI);
            this.gb_DecimalPoint.Controls.Add(this.txt_DecimalPoint_InstantaneousPower);
            this.gb_DecimalPoint.Controls.Add(this.txt_DecimalPoint_InstantaneousVoltage);
            this.gb_DecimalPoint.Controls.Add(this.lbl_DecimalPoint_InstantaneousVoltage);
            this.gb_DecimalPoint.Controls.Add(this.txt_DecimalPoint_BillingKWHMDI);
            this.gb_DecimalPoint.Controls.Add(this.lbl_DecimalPoint_BillingKWMDI);
            this.gb_DecimalPoint.Controls.Add(this.txt_DecimalPoint_BillingKWH);
            this.gb_DecimalPoint.Controls.Add(this.lbl_DecimalPoint_BillingKWH);
            this.gb_DecimalPoint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_DecimalPoint.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.gb_DecimalPoint.ForeColor = System.Drawing.Color.Maroon;
            this.gb_DecimalPoint.Location = new System.Drawing.Point(0, 0);
            this.gb_DecimalPoint.Name = "gb_DecimalPoint";
            this.gb_DecimalPoint.Size = new System.Drawing.Size(280, 192);
            this.gb_DecimalPoint.TabIndex = 2;
            this.gb_DecimalPoint.TabStop = false;
            this.gb_DecimalPoint.Text = "Decimal point";
            // 
            // lbl_DecimalPoint_InstantaneousPower
            // 
            this.lbl_DecimalPoint_InstantaneousPower.AutoSize = true;
            this.lbl_DecimalPoint_InstantaneousPower.ForeColor = System.Drawing.Color.Navy;
            this.lbl_DecimalPoint_InstantaneousPower.Location = new System.Drawing.Point(6, 98);
            this.lbl_DecimalPoint_InstantaneousPower.Name = "lbl_DecimalPoint_InstantaneousPower";
            this.lbl_DecimalPoint_InstantaneousPower.Size = new System.Drawing.Size(123, 15);
            this.lbl_DecimalPoint_InstantaneousPower.TabIndex = 15;
            this.lbl_DecimalPoint_InstantaneousPower.Text = "Instantaneous Power";
            // 
            // txt_DecimalPoint_InstantaneousCurrent
            // 
            this.txt_DecimalPoint_InstantaneousCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_DecimalPoint_InstantaneousCurrent.Location = new System.Drawing.Point(153, 121);
            this.txt_DecimalPoint_InstantaneousCurrent.Name = "txt_DecimalPoint_InstantaneousCurrent";
            this.txt_DecimalPoint_InstantaneousCurrent.Size = new System.Drawing.Size(114, 23);
            this.txt_DecimalPoint_InstantaneousCurrent.TabIndex = 4;
            this.txt_DecimalPoint_InstantaneousCurrent.Text = "0000.0000";
            this.txt_DecimalPoint_InstantaneousCurrent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_DecimalPoint_KeyPress);
            this.txt_DecimalPoint_InstantaneousCurrent.Leave += new System.EventHandler(this.txt_DecimalPoint_InstantaneousCurrent_Leave);
            // 
            // lbl_DecimalPoint_InstantaneousCurrent
            // 
            this.lbl_DecimalPoint_InstantaneousCurrent.AutoSize = true;
            this.lbl_DecimalPoint_InstantaneousCurrent.ForeColor = System.Drawing.Color.Navy;
            this.lbl_DecimalPoint_InstantaneousCurrent.Location = new System.Drawing.Point(6, 124);
            this.lbl_DecimalPoint_InstantaneousCurrent.Name = "lbl_DecimalPoint_InstantaneousCurrent";
            this.lbl_DecimalPoint_InstantaneousCurrent.Size = new System.Drawing.Size(130, 15);
            this.lbl_DecimalPoint_InstantaneousCurrent.TabIndex = 17;
            this.lbl_DecimalPoint_InstantaneousCurrent.Text = "Instantaneous Current";
            // 
            // txt_DecimalPoint_InstantaneousMDI
            // 
            this.txt_DecimalPoint_InstantaneousMDI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_DecimalPoint_InstantaneousMDI.Location = new System.Drawing.Point(153, 147);
            this.txt_DecimalPoint_InstantaneousMDI.Name = "txt_DecimalPoint_InstantaneousMDI";
            this.txt_DecimalPoint_InstantaneousMDI.Size = new System.Drawing.Size(114, 23);
            this.txt_DecimalPoint_InstantaneousMDI.TabIndex = 5;
            this.txt_DecimalPoint_InstantaneousMDI.Text = "0000.0000";
            this.txt_DecimalPoint_InstantaneousMDI.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_DecimalPoint_KeyPress);
            this.txt_DecimalPoint_InstantaneousMDI.Leave += new System.EventHandler(this.txt_DecimalPoint_InstantaneousMDI_Leave);
            // 
            // lbl_DecimalPoint_InstantaneousMDI
            // 
            this.lbl_DecimalPoint_InstantaneousMDI.AutoSize = true;
            this.lbl_DecimalPoint_InstantaneousMDI.ForeColor = System.Drawing.Color.Navy;
            this.lbl_DecimalPoint_InstantaneousMDI.Location = new System.Drawing.Point(6, 150);
            this.lbl_DecimalPoint_InstantaneousMDI.Name = "lbl_DecimalPoint_InstantaneousMDI";
            this.lbl_DecimalPoint_InstantaneousMDI.Size = new System.Drawing.Size(109, 15);
            this.lbl_DecimalPoint_InstantaneousMDI.TabIndex = 17;
            this.lbl_DecimalPoint_InstantaneousMDI.Text = "Instantaneous MDI";
            // 
            // txt_DecimalPoint_InstantaneousPower
            // 
            this.txt_DecimalPoint_InstantaneousPower.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_DecimalPoint_InstantaneousPower.Location = new System.Drawing.Point(153, 95);
            this.txt_DecimalPoint_InstantaneousPower.Name = "txt_DecimalPoint_InstantaneousPower";
            this.txt_DecimalPoint_InstantaneousPower.Size = new System.Drawing.Size(114, 23);
            this.txt_DecimalPoint_InstantaneousPower.TabIndex = 3;
            this.txt_DecimalPoint_InstantaneousPower.Text = "0000.0000";
            this.txt_DecimalPoint_InstantaneousPower.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_DecimalPoint_KeyPress);
            this.txt_DecimalPoint_InstantaneousPower.Leave += new System.EventHandler(this.txt_DecimalPoint_InstantaneousPower_Leave);
            // 
            // txt_DecimalPoint_InstantaneousVoltage
            // 
            this.txt_DecimalPoint_InstantaneousVoltage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_DecimalPoint_InstantaneousVoltage.Location = new System.Drawing.Point(153, 68);
            this.txt_DecimalPoint_InstantaneousVoltage.Name = "txt_DecimalPoint_InstantaneousVoltage";
            this.txt_DecimalPoint_InstantaneousVoltage.Size = new System.Drawing.Size(114, 23);
            this.txt_DecimalPoint_InstantaneousVoltage.TabIndex = 2;
            this.txt_DecimalPoint_InstantaneousVoltage.Text = "0000.0000";
            this.txt_DecimalPoint_InstantaneousVoltage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_DecimalPoint_KeyPress);
            this.txt_DecimalPoint_InstantaneousVoltage.Leave += new System.EventHandler(this.txt_DecimalPoint_InstantaneousVoltage_Leave);
            // 
            // lbl_DecimalPoint_InstantaneousVoltage
            // 
            this.lbl_DecimalPoint_InstantaneousVoltage.AutoSize = true;
            this.lbl_DecimalPoint_InstantaneousVoltage.ForeColor = System.Drawing.Color.Navy;
            this.lbl_DecimalPoint_InstantaneousVoltage.Location = new System.Drawing.Point(6, 71);
            this.lbl_DecimalPoint_InstantaneousVoltage.Name = "lbl_DecimalPoint_InstantaneousVoltage";
            this.lbl_DecimalPoint_InstantaneousVoltage.Size = new System.Drawing.Size(128, 15);
            this.lbl_DecimalPoint_InstantaneousVoltage.TabIndex = 13;
            this.lbl_DecimalPoint_InstantaneousVoltage.Text = "Instantaneous Voltage";
            // 
            // txt_DecimalPoint_BillingKWHMDI
            // 
            this.txt_DecimalPoint_BillingKWHMDI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_DecimalPoint_BillingKWHMDI.Location = new System.Drawing.Point(153, 42);
            this.txt_DecimalPoint_BillingKWHMDI.Name = "txt_DecimalPoint_BillingKWHMDI";
            this.txt_DecimalPoint_BillingKWHMDI.Size = new System.Drawing.Size(114, 23);
            this.txt_DecimalPoint_BillingKWHMDI.TabIndex = 1;
            this.txt_DecimalPoint_BillingKWHMDI.Text = "0000.0000";
            this.txt_DecimalPoint_BillingKWHMDI.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_DecimalPoint_KeyPress);
            this.txt_DecimalPoint_BillingKWHMDI.Leave += new System.EventHandler(this.txt_DecimalPoint_BillingKWHMDI_Leave);
            // 
            // lbl_DecimalPoint_BillingKWMDI
            // 
            this.lbl_DecimalPoint_BillingKWMDI.AutoSize = true;
            this.lbl_DecimalPoint_BillingKWMDI.ForeColor = System.Drawing.Color.Navy;
            this.lbl_DecimalPoint_BillingKWMDI.Location = new System.Drawing.Point(6, 45);
            this.lbl_DecimalPoint_BillingKWMDI.Name = "lbl_DecimalPoint_BillingKWMDI";
            this.lbl_DecimalPoint_BillingKWMDI.Size = new System.Drawing.Size(64, 15);
            this.lbl_DecimalPoint_BillingKWMDI.TabIndex = 11;
            this.lbl_DecimalPoint_BillingKWMDI.Text = "Billing MDI";
            // 
            // txt_DecimalPoint_BillingKWH
            // 
            this.txt_DecimalPoint_BillingKWH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_DecimalPoint_BillingKWH.Location = new System.Drawing.Point(153, 15);
            this.txt_DecimalPoint_BillingKWH.Name = "txt_DecimalPoint_BillingKWH";
            this.txt_DecimalPoint_BillingKWH.Size = new System.Drawing.Size(114, 23);
            this.txt_DecimalPoint_BillingKWH.TabIndex = 0;
            this.txt_DecimalPoint_BillingKWH.Text = "00000000.000";
            this.txt_DecimalPoint_BillingKWH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_DecimalPoint_KeyPress);
            this.txt_DecimalPoint_BillingKWH.Leave += new System.EventHandler(this.txt_DecimalPoint_BillingKWH_Leave);
            // 
            // lbl_DecimalPoint_BillingKWH
            // 
            this.lbl_DecimalPoint_BillingKWH.AutoSize = true;
            this.lbl_DecimalPoint_BillingKWH.ForeColor = System.Drawing.Color.Navy;
            this.lbl_DecimalPoint_BillingKWH.Location = new System.Drawing.Point(6, 19);
            this.lbl_DecimalPoint_BillingKWH.Name = "lbl_DecimalPoint_BillingKWH";
            this.lbl_DecimalPoint_BillingKWH.Size = new System.Drawing.Size(79, 15);
            this.lbl_DecimalPoint_BillingKWH.TabIndex = 9;
            this.lbl_DecimalPoint_BillingKWH.Text = "Billing Energy";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ucDecimalPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.gb_DecimalPoint);
            this.DoubleBuffered = true;
            this.Name = "ucDecimalPoint";
            this.Size = new System.Drawing.Size(280, 192);
            this.Load += new System.EventHandler(this.ucDecimalPoint_Load);
            this.Leave += new System.EventHandler(this.ucDecimalPoint_Leave);
            this.gb_DecimalPoint.ResumeLayout(false);
            this.gb_DecimalPoint.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.GroupBox gb_DecimalPoint;
        public System.Windows.Forms.TextBox txt_DecimalPoint_InstantaneousCurrent;
        public System.Windows.Forms.Label lbl_DecimalPoint_InstantaneousCurrent;
        public System.Windows.Forms.TextBox txt_DecimalPoint_InstantaneousMDI;
        public System.Windows.Forms.Label lbl_DecimalPoint_InstantaneousMDI;
        public System.Windows.Forms.TextBox txt_DecimalPoint_InstantaneousPower;
        public System.Windows.Forms.Label lbl_DecimalPoint_InstantaneousPower;
        public System.Windows.Forms.TextBox txt_DecimalPoint_InstantaneousVoltage;
        public System.Windows.Forms.Label lbl_DecimalPoint_InstantaneousVoltage;
        public System.Windows.Forms.TextBox txt_DecimalPoint_BillingKWHMDI;
        public System.Windows.Forms.Label lbl_DecimalPoint_BillingKWMDI;
        public System.Windows.Forms.TextBox txt_DecimalPoint_BillingKWH;
        public System.Windows.Forms.Label lbl_DecimalPoint_BillingKWH;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
