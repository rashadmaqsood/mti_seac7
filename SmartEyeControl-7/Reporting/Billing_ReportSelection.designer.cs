namespace SmartEyeControl_7.Reporting
{
    partial class Billing_ReportSelection
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
            this.grpBx_Electrical_Quantities = new System.Windows.Forms.GroupBox();
            this.checkAll_Billing = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.list_BillingQuantities = new ComponentFactory.Krypton.Toolkit.KryptonCheckedListBox();
            this.grpBx_Electrical_Quantities.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBx_Electrical_Quantities
            // 
            this.grpBx_Electrical_Quantities.Controls.Add(this.checkAll_Billing);
            this.grpBx_Electrical_Quantities.Controls.Add(this.button1);
            this.grpBx_Electrical_Quantities.Controls.Add(this.list_BillingQuantities);
            this.grpBx_Electrical_Quantities.Location = new System.Drawing.Point(12, 12);
            this.grpBx_Electrical_Quantities.Name = "grpBx_Electrical_Quantities";
            this.grpBx_Electrical_Quantities.Size = new System.Drawing.Size(212, 378);
            this.grpBx_Electrical_Quantities.TabIndex = 5;
            this.grpBx_Electrical_Quantities.TabStop = false;
            // 
            // checkAll_Billing
            // 
            this.checkAll_Billing.AutoSize = true;
            this.checkAll_Billing.Checked = true;
            this.checkAll_Billing.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkAll_Billing.Location = new System.Drawing.Point(22, 20);
            this.checkAll_Billing.Name = "checkAll_Billing";
            this.checkAll_Billing.Size = new System.Drawing.Size(71, 17);
            this.checkAll_Billing.TabIndex = 7;
            this.checkAll_Billing.Text = "Check All";
            this.checkAll_Billing.UseVisualStyleBackColor = true;
            this.checkAll_Billing.CheckedChanged += new System.EventHandler(this.checkAll_Billing_CheckedChanged);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(129, 340);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(52, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // list_BillingQuantities
            // 
            this.list_BillingQuantities.CheckOnClick = true;
            this.list_BillingQuantities.Location = new System.Drawing.Point(17, 55);
            this.list_BillingQuantities.Name = "list_BillingQuantities";
            this.list_BillingQuantities.Size = new System.Drawing.Size(164, 270);
            this.list_BillingQuantities.TabIndex = 1;
            // 
            // Billing_ReportSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 402);
            this.ControlBox = false;
            this.Controls.Add(this.grpBx_Electrical_Quantities);
            this.Name = "Billing_ReportSelection";
            this.Text = "Billing_ReportSelection";
            this.grpBx_Electrical_Quantities.ResumeLayout(false);
            this.grpBx_Electrical_Quantities.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBx_Electrical_Quantities;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckedListBox list_BillingQuantities;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkAll_Billing;
    }
}