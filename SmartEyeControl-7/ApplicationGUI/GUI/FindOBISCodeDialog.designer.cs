namespace SmartEyeControl_7.ApplicationGUI.GUI
{
    partial class FindOBISCodeDialog
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
            this.btnFind = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblLabel = new System.Windows.Forms.Label();
            this.txtLabel = new System.Windows.Forms.TextBox();
            this.lblId = new System.Windows.Forms.Label();
            this.txtIndexId = new System.Windows.Forms.TextBox();
            this.lblGetIndex = new System.Windows.Forms.Label();
            this.txt_GetIndex = new System.Windows.Forms.TextBox();
            this.lblFindByOBISString = new System.Windows.Forms.Label();
            this.txtOBISCode = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnFind
            // 
            this.btnFind.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnFind.Location = new System.Drawing.Point(239, 232);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 23);
            this.btnFind.TabIndex = 0;
            this.btnFind.Text = "&Find";
            this.btnFind.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(320, 232);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblLabel
            // 
            this.lblLabel.AutoSize = true;
            this.lblLabel.Location = new System.Drawing.Point(22, 56);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(71, 13);
            this.lblLabel.TabIndex = 2;
            this.lblLabel.Text = "Find By Label";
            // 
            // txtLabel
            // 
            this.txtLabel.Location = new System.Drawing.Point(145, 53);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(198, 20);
            this.txtLabel.TabIndex = 3;
            this.txtLabel.TextChanged += new System.EventHandler(this.txtLabel_TextChanged);
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(22, 90);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(54, 13);
            this.lblId.TabIndex = 2;
            this.lblId.Text = "Find By Id";
            // 
            // txtIndexId
            // 
            this.txtIndexId.Location = new System.Drawing.Point(145, 87);
            this.txtIndexId.Name = "txtIndexId";
            this.txtIndexId.Size = new System.Drawing.Size(198, 20);
            this.txtIndexId.TabIndex = 3;
            this.txtIndexId.TextChanged += new System.EventHandler(this.txtIndexId_TextChanged);
            // 
            // lblGetIndex
            // 
            this.lblGetIndex.AutoSize = true;
            this.lblGetIndex.Location = new System.Drawing.Point(22, 131);
            this.lblGetIndex.Name = "lblGetIndex";
            this.lblGetIndex.Size = new System.Drawing.Size(115, 13);
            this.lblGetIndex.TabIndex = 2;
            this.lblGetIndex.Text = "Find By Quantity Name";
            // 
            // txt_GetIndex
            // 
            this.txt_GetIndex.Location = new System.Drawing.Point(145, 128);
            this.txt_GetIndex.Name = "txt_GetIndex";
            this.txt_GetIndex.Size = new System.Drawing.Size(198, 20);
            this.txt_GetIndex.TabIndex = 3;
            this.txt_GetIndex.TextChanged += new System.EventHandler(this.txt_GetIndex_TextChanged);
            // 
            // lblFindByOBISString
            // 
            this.lblFindByOBISString.AutoSize = true;
            this.lblFindByOBISString.Location = new System.Drawing.Point(22, 174);
            this.lblFindByOBISString.Name = "lblFindByOBISString";
            this.lblFindByOBISString.Size = new System.Drawing.Size(98, 13);
            this.lblFindByOBISString.TabIndex = 2;
            this.lblFindByOBISString.Text = "Find By OBIS Code";
            // 
            // txtOBISCode
            // 
            this.txtOBISCode.Location = new System.Drawing.Point(145, 171);
            this.txtOBISCode.Name = "txtOBISCode";
            this.txtOBISCode.Size = new System.Drawing.Size(198, 20);
            this.txtOBISCode.TabIndex = 3;
            this.txtOBISCode.TextChanged += new System.EventHandler(this.txtOBISCode_TextChanged);
            // 
            // FindOBISCodeDialog
            // 
            this.AcceptButton = this.btnFind;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(410, 276);
            this.Controls.Add(this.txtOBISCode);
            this.Controls.Add(this.txt_GetIndex);
            this.Controls.Add(this.txtIndexId);
            this.Controls.Add(this.txtLabel);
            this.Controls.Add(this.lblFindByOBISString);
            this.Controls.Add(this.lblGetIndex);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.lblLabel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFind);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FindOBISCodeDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find OBIS Code Dialog";
            this.Load += new System.EventHandler(this.FindOBISCodeDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label lblLabel;
        private System.Windows.Forms.TextBox txtLabel;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtIndexId;
        private System.Windows.Forms.Label lblGetIndex;
        private System.Windows.Forms.TextBox txt_GetIndex;
        private System.Windows.Forms.Label lblFindByOBISString;
        private System.Windows.Forms.TextBox txtOBISCode;
    }
}