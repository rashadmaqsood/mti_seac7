namespace SmartEyeControl_7.ApplicationGUI.GUI
{
    partial class OBISCodeDialog
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
            this.btnOkay = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblLabel = new System.Windows.Forms.Label();
            this.txtLabel = new System.Windows.Forms.TextBox();
            this.lblFindByOBISString = new System.Windows.Forms.Label();
            this.txtOBISCode = new System.Windows.Forms.TextBox();
            this.rdbIndex = new System.Windows.Forms.RadioButton();
            this.gpOBISMode = new System.Windows.Forms.GroupBox();
            this.rdbOBISLabel = new System.Windows.Forms.RadioButton();
            this.rdbOBISCode = new System.Windows.Forms.RadioButton();
            this.gpOBISMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOkay
            // 
            this.btnOkay.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOkay.Location = new System.Drawing.Point(174, 138);
            this.btnOkay.Name = "btnOkay";
            this.btnOkay.Size = new System.Drawing.Size(75, 23);
            this.btnOkay.TabIndex = 0;
            this.btnOkay.Text = "&Ok";
            this.btnOkay.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(255, 138);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // lblLabel
            // 
            this.lblLabel.AutoSize = true;
            this.lblLabel.Location = new System.Drawing.Point(19, 106);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(75, 13);
            this.lblLabel.TabIndex = 2;
            this.lblLabel.Text = "Quantity Label";
            // 
            // txtLabel
            // 
            this.txtLabel.Location = new System.Drawing.Point(110, 103);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(198, 20);
            this.txtLabel.TabIndex = 3;
            this.txtLabel.TextChanged += new System.EventHandler(this.txtLabel_TextChanged);
            // 
            // lblFindByOBISString
            // 
            this.lblFindByOBISString.AutoSize = true;
            this.lblFindByOBISString.Location = new System.Drawing.Point(19, 70);
            this.lblFindByOBISString.Name = "lblFindByOBISString";
            this.lblFindByOBISString.Size = new System.Drawing.Size(60, 13);
            this.lblFindByOBISString.TabIndex = 2;
            this.lblFindByOBISString.Text = "OBIS Code";
            // 
            // txtOBISCode
            // 
            this.txtOBISCode.Location = new System.Drawing.Point(110, 67);
            this.txtOBISCode.Name = "txtOBISCode";
            this.txtOBISCode.Size = new System.Drawing.Size(198, 20);
            this.txtOBISCode.TabIndex = 3;
            this.txtOBISCode.TextChanged += new System.EventHandler(this.txtOBISCode_TextChanged);
            // 
            // rdbIndex
            // 
            this.rdbIndex.AutoSize = true;
            this.rdbIndex.Location = new System.Drawing.Point(93, 16);
            this.rdbIndex.Name = "rdbIndex";
            this.rdbIndex.Size = new System.Drawing.Size(93, 17);
            this.rdbIndex.TabIndex = 4;
            this.rdbIndex.Text = "Quantity Index";
            this.rdbIndex.UseVisualStyleBackColor = true;
            // 
            // gpOBISMode
            // 
            this.gpOBISMode.Controls.Add(this.rdbOBISLabel);
            this.gpOBISMode.Controls.Add(this.rdbOBISCode);
            this.gpOBISMode.Controls.Add(this.rdbIndex);
            this.gpOBISMode.Location = new System.Drawing.Point(7, 10);
            this.gpOBISMode.Name = "gpOBISMode";
            this.gpOBISMode.Size = new System.Drawing.Size(338, 43);
            this.gpOBISMode.TabIndex = 5;
            this.gpOBISMode.TabStop = false;
            this.gpOBISMode.Text = "OBIS Code Mode";
            // 
            // rdbOBISLabel
            // 
            this.rdbOBISLabel.AutoSize = true;
            this.rdbOBISLabel.Location = new System.Drawing.Point(192, 16);
            this.rdbOBISLabel.Name = "rdbOBISLabel";
            this.rdbOBISLabel.Size = new System.Drawing.Size(79, 17);
            this.rdbOBISLabel.TabIndex = 6;
            this.rdbOBISLabel.Text = "OBIS Label";
            this.rdbOBISLabel.UseVisualStyleBackColor = true;
            this.rdbOBISLabel.CheckedChanged += new System.EventHandler(this.rdbOBISCode_CheckedChanged);
            // 
            // rdbOBISCode
            // 
            this.rdbOBISCode.AutoSize = true;
            this.rdbOBISCode.Checked = true;
            this.rdbOBISCode.Location = new System.Drawing.Point(9, 16);
            this.rdbOBISCode.Name = "rdbOBISCode";
            this.rdbOBISCode.Size = new System.Drawing.Size(78, 17);
            this.rdbOBISCode.TabIndex = 5;
            this.rdbOBISCode.TabStop = true;
            this.rdbOBISCode.Text = "OBIS Code";
            this.rdbOBISCode.UseVisualStyleBackColor = true;
            this.rdbOBISCode.CheckedChanged += new System.EventHandler(this.rdbOBISCode_CheckedChanged);
            // 
            // OBISCodeDialog
            // 
            this.AcceptButton = this.btnOkay;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(351, 171);
            this.Controls.Add(this.gpOBISMode);
            this.Controls.Add(this.txtOBISCode);
            this.Controls.Add(this.txtLabel);
            this.Controls.Add(this.lblFindByOBISString);
            this.Controls.Add(this.lblLabel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOkay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "OBISCodeDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "OBIS Code Dialog";
            this.Load += new System.EventHandler(this.OBISCodeDialog_Load);
            this.gpOBISMode.ResumeLayout(false);
            this.gpOBISMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Button btnOkay;
        private System.Windows.Forms.Label lblLabel;
        private System.Windows.Forms.TextBox txtLabel;
        private System.Windows.Forms.Label lblFindByOBISString;
        private System.Windows.Forms.TextBox txtOBISCode;
        private System.Windows.Forms.RadioButton rdbIndex;
        private System.Windows.Forms.GroupBox gpOBISMode;
        private System.Windows.Forms.RadioButton rdbOBISLabel;
        private System.Windows.Forms.RadioButton rdbOBISCode;
    }
}