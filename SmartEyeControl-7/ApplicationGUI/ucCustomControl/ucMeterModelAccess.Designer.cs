namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    partial class ucMeterModelAccessRights
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
            this.cmbAccessRights = new System.Windows.Forms.ComboBox();
            this.lblModel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbAccessRights
            // 
            this.cmbAccessRights.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccessRights.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAccessRights.FormattingEnabled = true;
            this.cmbAccessRights.Location = new System.Drawing.Point(89, 3);
            this.cmbAccessRights.Name = "cmbAccessRights";
            this.cmbAccessRights.Size = new System.Drawing.Size(183, 23);
            this.cmbAccessRights.TabIndex = 0;
            this.cmbAccessRights.SelectedIndexChanged += new System.EventHandler(this.cmbAccessRights_SelectedIndexChanged);
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.BackColor = System.Drawing.Color.Transparent;
            this.lblModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModel.Location = new System.Drawing.Point(3, 6);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(77, 15);
            this.lblModel.TabIndex = 1;
            this.lblModel.Text = "Meter Model";
            this.lblModel.Click += new System.EventHandler(this.lblModel_Click);
            // 
            // ucMeterModelAccessRights
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblModel);
            this.Controls.Add(this.cmbAccessRights);
            this.Name = "ucMeterModelAccessRights";
            this.Size = new System.Drawing.Size(276, 28);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox cmbAccessRights;
        public System.Windows.Forms.Label lblModel;
    }
}
