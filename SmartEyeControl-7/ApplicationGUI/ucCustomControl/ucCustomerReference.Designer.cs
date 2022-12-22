namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    partial class ucCustomerReference
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
            this.gpCustomerReference = new System.Windows.Forms.GroupBox();
            this.lblCustNameLength = new System.Windows.Forms.Label();
            this.lblCustAddressLength = new System.Windows.Forms.Label();
            this.lblCustCodeLength = new System.Windows.Forms.Label();
            this.txt_CustomerAddress = new System.Windows.Forms.RichTextBox();
            this.txt_CustomerRef_CustomerCode = new System.Windows.Forms.TextBox();
            this.txt_CustomerName = new System.Windows.Forms.TextBox();
            this.lbl_CustomerReference_CustomerCode = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.lblCustAddress = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.gpCustomerReference.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // gpCustomerReference
            // 
            this.gpCustomerReference.AutoSize = true;
            this.gpCustomerReference.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gpCustomerReference.Controls.Add(this.lblCustNameLength);
            this.gpCustomerReference.Controls.Add(this.lblCustAddressLength);
            this.gpCustomerReference.Controls.Add(this.lblCustCodeLength);
            this.gpCustomerReference.Controls.Add(this.txt_CustomerAddress);
            this.gpCustomerReference.Controls.Add(this.txt_CustomerRef_CustomerCode);
            this.gpCustomerReference.Controls.Add(this.txt_CustomerName);
            this.gpCustomerReference.Controls.Add(this.lbl_CustomerReference_CustomerCode);
            this.gpCustomerReference.Controls.Add(this.lblCustomerName);
            this.gpCustomerReference.Controls.Add(this.lblCustAddress);
            this.gpCustomerReference.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpCustomerReference.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.gpCustomerReference.ForeColor = System.Drawing.Color.Maroon;
            this.gpCustomerReference.Location = new System.Drawing.Point(0, 0);
            this.gpCustomerReference.Name = "gpCustomerReference";
            this.gpCustomerReference.Size = new System.Drawing.Size(300, 157);
            this.gpCustomerReference.TabIndex = 18;
            this.gpCustomerReference.TabStop = false;
            this.gpCustomerReference.Text = "Customer Reference";
            this.gpCustomerReference.Leave += new System.EventHandler(this.gpCustomerReference_Leave);
            // 
            // lblCustNameLength
            // 
            this.lblCustNameLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCustNameLength.AutoSize = true;
            this.lblCustNameLength.Location = new System.Drawing.Point(266, 60);
            this.lblCustNameLength.Name = "lblCustNameLength";
            this.lblCustNameLength.Size = new System.Drawing.Size(22, 15);
            this.lblCustNameLength.TabIndex = 44;
            this.lblCustNameLength.Text = "(0)";
            // 
            // lblCustAddressLength
            // 
            this.lblCustAddressLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCustAddressLength.AutoSize = true;
            this.lblCustAddressLength.Location = new System.Drawing.Point(266, 94);
            this.lblCustAddressLength.Name = "lblCustAddressLength";
            this.lblCustAddressLength.Size = new System.Drawing.Size(22, 15);
            this.lblCustAddressLength.TabIndex = 43;
            this.lblCustAddressLength.Text = "(0)";
            // 
            // lblCustCodeLength
            // 
            this.lblCustCodeLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCustCodeLength.AutoSize = true;
            this.lblCustCodeLength.Location = new System.Drawing.Point(266, 22);
            this.lblCustCodeLength.Name = "lblCustCodeLength";
            this.lblCustCodeLength.Size = new System.Drawing.Size(22, 15);
            this.lblCustCodeLength.TabIndex = 42;
            this.lblCustCodeLength.Text = "(0)";
            // 
            // txt_CustomerAddress
            // 
            this.txt_CustomerAddress.Location = new System.Drawing.Point(121, 89);
            this.txt_CustomerAddress.MaxLength = 30;
            this.txt_CustomerAddress.Name = "txt_CustomerAddress";
            this.txt_CustomerAddress.Size = new System.Drawing.Size(134, 46);
            this.txt_CustomerAddress.TabIndex = 41;
            this.txt_CustomerAddress.Text = "";
            this.txt_CustomerAddress.TextChanged += new System.EventHandler(this.txt_CustomerAddress_TextChanged);
            // 
            // txt_CustomerRef_CustomerCode
            // 
            this.txt_CustomerRef_CustomerCode.Location = new System.Drawing.Point(121, 19);
            this.txt_CustomerRef_CustomerCode.MaxLength = 15;
            this.txt_CustomerRef_CustomerCode.Name = "txt_CustomerRef_CustomerCode";
            this.txt_CustomerRef_CustomerCode.Size = new System.Drawing.Size(134, 23);
            this.txt_CustomerRef_CustomerCode.TabIndex = 0;
            this.txt_CustomerRef_CustomerCode.TextChanged += new System.EventHandler(this.txt_CustomerRef_CustomerCode_TextChanged);
            this.txt_CustomerRef_CustomerCode.Leave += new System.EventHandler(this.txt_CustomerRef_CustomerCode_Leave);
            // 
            // txt_CustomerName
            // 
            this.txt_CustomerName.Location = new System.Drawing.Point(121, 54);
            this.txt_CustomerName.MaxLength = 30;
            this.txt_CustomerName.Name = "txt_CustomerName";
            this.txt_CustomerName.Size = new System.Drawing.Size(134, 23);
            this.txt_CustomerName.TabIndex = 38;
            this.txt_CustomerName.TextChanged += new System.EventHandler(this.txt_CustomerName_TextChanged);
            // 
            // lbl_CustomerReference_CustomerCode
            // 
            this.lbl_CustomerReference_CustomerCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_CustomerReference_CustomerCode.AutoSize = true;
            this.lbl_CustomerReference_CustomerCode.ForeColor = System.Drawing.Color.Navy;
            this.lbl_CustomerReference_CustomerCode.Location = new System.Drawing.Point(6, 17);
            this.lbl_CustomerReference_CustomerCode.Name = "lbl_CustomerReference_CustomerCode";
            this.lbl_CustomerReference_CustomerCode.Size = new System.Drawing.Size(92, 15);
            this.lbl_CustomerReference_CustomerCode.TabIndex = 36;
            this.lbl_CustomerReference_CustomerCode.Text = "Customer Code";
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.ForeColor = System.Drawing.Color.Navy;
            this.lblCustomerName.Location = new System.Drawing.Point(6, 52);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(97, 15);
            this.lblCustomerName.TabIndex = 40;
            this.lblCustomerName.Text = "Customer Name";
            // 
            // lblCustAddress
            // 
            this.lblCustAddress.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCustAddress.AutoSize = true;
            this.lblCustAddress.ForeColor = System.Drawing.Color.Navy;
            this.lblCustAddress.Location = new System.Drawing.Point(6, 85);
            this.lblCustAddress.Name = "lblCustAddress";
            this.lblCustAddress.Size = new System.Drawing.Size(108, 15);
            this.lblCustAddress.TabIndex = 39;
            this.lblCustAddress.Text = "Customer Address";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ucCustomerReference
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.gpCustomerReference);
            this.DoubleBuffered = true;
            this.Name = "ucCustomerReference";
            this.Size = new System.Drawing.Size(300, 157);
            this.Load += new System.EventHandler(this.ucCustomerReference_Load);
            this.Leave += new System.EventHandler(this.ucCustomerReference_Leave);
            this.gpCustomerReference.ResumeLayout(false);
            this.gpCustomerReference.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gpCustomerReference;
        private System.Windows.Forms.RichTextBox txt_CustomerAddress;
        private System.Windows.Forms.TextBox txt_CustomerRef_CustomerCode;
        private System.Windows.Forms.TextBox txt_CustomerName;
        private System.Windows.Forms.Label lbl_CustomerReference_CustomerCode;
        private System.Windows.Forms.Label lblCustAddress;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label lblCustNameLength;
        private System.Windows.Forms.Label lblCustAddressLength;
        private System.Windows.Forms.Label lblCustCodeLength;
    }
}
