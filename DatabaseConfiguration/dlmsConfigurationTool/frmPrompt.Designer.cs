namespace dlmsConfigurationTool
{
    partial class frmPrompt
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
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.chbx_All = new System.Windows.Forms.CheckBox();
            this.pnl_btns = new System.Windows.Forms.Panel();
            this.pnl_bottom = new System.Windows.Forms.Panel();
            this.pnl_chbxAll = new System.Windows.Forms.Panel();
            this.pnl_chkList = new System.Windows.Forms.Panel();
            this.pnl_btns.SuspendLayout();
            this.pnl_bottom.SuspendLayout();
            this.pnl_chbxAll.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.btn_OK.FlatAppearance.BorderSize = 0;
            this.btn_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_OK.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_OK.Location = new System.Drawing.Point(22, 5);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(60, 23);
            this.btn_OK.TabIndex = 3;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = false;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.FlatAppearance.BorderSize = 0;
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cancel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Cancel.Location = new System.Drawing.Point(88, 5);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(60, 23);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // chbx_All
            // 
            this.chbx_All.AutoSize = true;
            this.chbx_All.Location = new System.Drawing.Point(9, 9);
            this.chbx_All.Name = "chbx_All";
            this.chbx_All.Size = new System.Drawing.Size(126, 17);
            this.chbx_All.TabIndex = 5;
            this.chbx_All.Text = "Check / Uncheck All";
            this.chbx_All.UseVisualStyleBackColor = true;
            this.chbx_All.Click += new System.EventHandler(this.chbx_All_Click);
            // 
            // pnl_btns
            // 
            this.pnl_btns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_btns.Controls.Add(this.btn_OK);
            this.pnl_btns.Controls.Add(this.btn_Cancel);
            this.pnl_btns.Location = new System.Drawing.Point(327, 3);
            this.pnl_btns.Name = "pnl_btns";
            this.pnl_btns.Size = new System.Drawing.Size(154, 35);
            this.pnl_btns.TabIndex = 2;
            // 
            // pnl_bottom
            // 
            this.pnl_bottom.Controls.Add(this.pnl_btns);
            this.pnl_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_bottom.Location = new System.Drawing.Point(0, 261);
            this.pnl_bottom.Name = "pnl_bottom";
            this.pnl_bottom.Size = new System.Drawing.Size(484, 41);
            this.pnl_bottom.TabIndex = 1;
            // 
            // pnl_chbxAll
            // 
            this.pnl_chbxAll.Controls.Add(this.chbx_All);
            this.pnl_chbxAll.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_chbxAll.Location = new System.Drawing.Point(0, 231);
            this.pnl_chbxAll.Name = "pnl_chbxAll";
            this.pnl_chbxAll.Size = new System.Drawing.Size(484, 30);
            this.pnl_chbxAll.TabIndex = 7;
            // 
            // pnl_chkList
            // 
            this.pnl_chkList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_chkList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_chkList.Location = new System.Drawing.Point(0, 0);
            this.pnl_chkList.Name = "pnl_chkList";
            this.pnl_chkList.Size = new System.Drawing.Size(484, 231);
            this.pnl_chkList.TabIndex = 8;
            // 
            // frmPrompt
            // 
            this.AcceptButton = this.btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(484, 302);
            this.Controls.Add(this.pnl_chkList);
            this.Controls.Add(this.pnl_chbxAll);
            this.Controls.Add(this.pnl_bottom);
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrompt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Prompt";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPrompt_FormClosing);
            this.Load += new System.EventHandler(this.frmPrompt_Load);
            this.pnl_btns.ResumeLayout(false);
            this.pnl_bottom.ResumeLayout(false);
            this.pnl_chbxAll.ResumeLayout(false);
            this.pnl_chbxAll.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.CheckBox chbx_All;
        private System.Windows.Forms.Panel pnl_btns;
        private System.Windows.Forms.Panel pnl_bottom;
        private System.Windows.Forms.Panel pnl_chbxAll;
        private System.Windows.Forms.Panel pnl_chkList;
    }
}