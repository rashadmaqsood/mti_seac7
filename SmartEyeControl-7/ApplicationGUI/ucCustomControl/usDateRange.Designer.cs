namespace SmartEyeControl_7.ApplicationGUI.ucCustomControl
{
    partial class usDateRange
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
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lbl_fromTxt = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lbl_ToTxt = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd/MMM/yyyy HH:mm:ss";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(49, 3);
            this.dtpFrom.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(149, 20);
            this.dtpFrom.TabIndex = 20;
            // 
            // lbl_fromTxt
            // 
            this.lbl_fromTxt.AutoSize = true;
            this.lbl_fromTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbl_fromTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_fromTxt.Location = new System.Drawing.Point(7, 10);
            this.lbl_fromTxt.Margin = new System.Windows.Forms.Padding(5, 5, 5, 3);
            this.lbl_fromTxt.Name = "lbl_fromTxt";
            this.lbl_fromTxt.Size = new System.Drawing.Size(34, 13);
            this.lbl_fromTxt.TabIndex = 22;
            this.lbl_fromTxt.Text = "From";
            // 
            // dtpTo
            // 
            this.dtpTo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpTo.CustomFormat = "dd/MMM/yyyy HH:mm:ss";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(48, 32);
            this.dtpTo.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(149, 20);
            this.dtpTo.TabIndex = 19;
            // 
            // lbl_ToTxt
            // 
            this.lbl_ToTxt.AutoSize = true;
            this.lbl_ToTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbl_ToTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_ToTxt.Location = new System.Drawing.Point(18, 39);
            this.lbl_ToTxt.Margin = new System.Windows.Forms.Padding(25, 5, 5, 3);
            this.lbl_ToTxt.Name = "lbl_ToTxt";
            this.lbl_ToTxt.Size = new System.Drawing.Size(22, 13);
            this.lbl_ToTxt.TabIndex = 21;
            this.lbl_ToTxt.Text = "To";
            // 
            // usDateRange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.lbl_fromTxt);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.lbl_ToTxt);
            this.Name = "usDateRange";
            this.Size = new System.Drawing.Size(204, 55);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_fromTxt;
        private System.Windows.Forms.Label lbl_ToTxt;
        public System.Windows.Forms.DateTimePicker dtpFrom;
        public System.Windows.Forms.DateTimePicker dtpTo;
    }
}
