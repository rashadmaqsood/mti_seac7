namespace dlmsConfigurationTool
{
    partial class CopyEvents
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbEvetGroups = new System.Windows.Forms.ComboBox();
            this.cmbExistingEventGroups = new System.Windows.Forms.ComboBox();
            this.lblExistingGroups = new System.Windows.Forms.Label();
            this.chbSelectGroup = new System.Windows.Forms.CheckBox();
            this.btnEventsGroupCopy = new System.Windows.Forms.Button();
            this.txtEventGroupName = new System.Windows.Forms.TextBox();
            this.lblNewGroup = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Group";
            // 
            // cmbEvetGroups
            // 
            this.cmbEvetGroups.FormattingEnabled = true;
            this.cmbEvetGroups.Location = new System.Drawing.Point(133, 38);
            this.cmbEvetGroups.Name = "cmbEvetGroups";
            this.cmbEvetGroups.Size = new System.Drawing.Size(148, 21);
            this.cmbEvetGroups.TabIndex = 1;
            this.cmbEvetGroups.SelectedIndexChanged += new System.EventHandler(this.cmbBillingGroups_SelectedIndexChanged);
            // 
            // cmbExistingEventGroups
            // 
            this.cmbExistingEventGroups.FormattingEnabled = true;
            this.cmbExistingEventGroups.Location = new System.Drawing.Point(133, 109);
            this.cmbExistingEventGroups.Name = "cmbExistingEventGroups";
            this.cmbExistingEventGroups.Size = new System.Drawing.Size(148, 21);
            this.cmbExistingEventGroups.TabIndex = 3;
            this.cmbExistingEventGroups.Visible = false;
            // 
            // lblExistingGroups
            // 
            this.lblExistingGroups.AutoSize = true;
            this.lblExistingGroups.Location = new System.Drawing.Point(12, 112);
            this.lblExistingGroups.Name = "lblExistingGroups";
            this.lblExistingGroups.Size = new System.Drawing.Size(108, 13);
            this.lblExistingGroups.TabIndex = 2;
            this.lblExistingGroups.Text = "Select Existing Group";
            this.lblExistingGroups.Visible = false;
            // 
            // chbSelectGroup
            // 
            this.chbSelectGroup.AutoSize = true;
            this.chbSelectGroup.Location = new System.Drawing.Point(133, 77);
            this.chbSelectGroup.Name = "chbSelectGroup";
            this.chbSelectGroup.Size = new System.Drawing.Size(132, 17);
            this.chbSelectGroup.TabIndex = 4;
            this.chbSelectGroup.Text = "Copy in Existing Group";
            this.chbSelectGroup.UseVisualStyleBackColor = true;
            this.chbSelectGroup.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnEventsGroupCopy
            // 
            this.btnEventsGroupCopy.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnEventsGroupCopy.Location = new System.Drawing.Point(206, 146);
            this.btnEventsGroupCopy.Name = "btnEventsGroupCopy";
            this.btnEventsGroupCopy.Size = new System.Drawing.Size(75, 23);
            this.btnEventsGroupCopy.TabIndex = 5;
            this.btnEventsGroupCopy.Text = "Copy";
            this.btnEventsGroupCopy.UseVisualStyleBackColor = true;
            // 
            // txtEventGroupName
            // 
            this.txtEventGroupName.Location = new System.Drawing.Point(133, 110);
            this.txtEventGroupName.Name = "txtEventGroupName";
            this.txtEventGroupName.Size = new System.Drawing.Size(148, 20);
            this.txtEventGroupName.TabIndex = 6;
            this.txtEventGroupName.Visible = false;
            // 
            // lblNewGroup
            // 
            this.lblNewGroup.AutoSize = true;
            this.lblNewGroup.Location = new System.Drawing.Point(28, 113);
            this.lblNewGroup.Name = "lblNewGroup";
            this.lblNewGroup.Size = new System.Drawing.Size(92, 13);
            this.lblNewGroup.TabIndex = 7;
            this.lblNewGroup.Text = "New Group Name";
            this.lblNewGroup.Visible = false;
            // 
            // CopyEvents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 233);
            this.Controls.Add(this.lblNewGroup);
            this.Controls.Add(this.txtEventGroupName);
            this.Controls.Add(this.btnEventsGroupCopy);
            this.Controls.Add(this.chbSelectGroup);
            this.Controls.Add(this.cmbExistingEventGroups);
            this.Controls.Add(this.lblExistingGroups);
            this.Controls.Add(this.cmbEvetGroups);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CopyEvents";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Copy Events";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbEvetGroups;
        private System.Windows.Forms.ComboBox cmbExistingEventGroups;
        private System.Windows.Forms.Label lblExistingGroups;
        private System.Windows.Forms.CheckBox chbSelectGroup;
        private System.Windows.Forms.Button btnEventsGroupCopy;
        private System.Windows.Forms.TextBox txtEventGroupName;
        private System.Windows.Forms.Label lblNewGroup;
    }
}