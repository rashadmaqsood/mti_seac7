namespace dlmsConfigurationTool
{
    partial class MessageForm
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
            this.lbl_Message2 = new System.Windows.Forms.Label();
            this.pnl_bottom = new System.Windows.Forms.Panel();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_No = new System.Windows.Forms.Button();
            this.btn_Yes = new System.Windows.Forms.Button();
            this.pnl_Main = new System.Windows.Forms.Panel();
            this.tb_Message2 = new System.Windows.Forms.RichTextBox();
            this.tb_Message1 = new System.Windows.Forms.RichTextBox();
            this.lbl_Message1 = new System.Windows.Forms.Label();
            this.pnl_bottom.SuspendLayout();
            this.pnl_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Message2
            // 
            this.lbl_Message2.AutoSize = true;
            this.lbl_Message2.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_Message2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_Message2.Location = new System.Drawing.Point(12, 176);
            this.lbl_Message2.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_Message2.Name = "lbl_Message2";
            this.lbl_Message2.Padding = new System.Windows.Forms.Padding(0, 9, 0, 3);
            this.lbl_Message2.Size = new System.Drawing.Size(56, 25);
            this.lbl_Message2.TabIndex = 7;
            this.lbl_Message2.Text = "Message2";
            // 
            // pnl_bottom
            // 
            this.pnl_bottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_bottom.Controls.Add(this.btn_Cancel);
            this.pnl_bottom.Controls.Add(this.btn_No);
            this.pnl_bottom.Controls.Add(this.btn_Yes);
            this.pnl_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_bottom.Location = new System.Drawing.Point(0, 361);
            this.pnl_bottom.Name = "pnl_bottom";
            this.pnl_bottom.Size = new System.Drawing.Size(634, 43);
            this.pnl_bottom.TabIndex = 4;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.FlatAppearance.BorderSize = 0;
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cancel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Cancel.Location = new System.Drawing.Point(546, 9);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 5;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_No
            // 
            this.btn_No.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.btn_No.FlatAppearance.BorderSize = 0;
            this.btn_No.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_No.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_No.Location = new System.Drawing.Point(465, 9);
            this.btn_No.Name = "btn_No";
            this.btn_No.Size = new System.Drawing.Size(75, 23);
            this.btn_No.TabIndex = 4;
            this.btn_No.Text = "No";
            this.btn_No.UseVisualStyleBackColor = false;
            this.btn_No.Click += new System.EventHandler(this.btn_No_Click);
            // 
            // btn_Yes
            // 
            this.btn_Yes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.btn_Yes.FlatAppearance.BorderSize = 0;
            this.btn_Yes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Yes.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Yes.Location = new System.Drawing.Point(384, 9);
            this.btn_Yes.Name = "btn_Yes";
            this.btn_Yes.Size = new System.Drawing.Size(75, 23);
            this.btn_Yes.TabIndex = 3;
            this.btn_Yes.Text = "Yes";
            this.btn_Yes.UseVisualStyleBackColor = false;
            this.btn_Yes.Click += new System.EventHandler(this.btn_Yes_Click);
            // 
            // pnl_Main
            // 
            this.pnl_Main.AutoScroll = true;
            this.pnl_Main.Controls.Add(this.tb_Message2);
            this.pnl_Main.Controls.Add(this.lbl_Message2);
            this.pnl_Main.Controls.Add(this.tb_Message1);
            this.pnl_Main.Controls.Add(this.lbl_Message1);
            this.pnl_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Main.Location = new System.Drawing.Point(0, 0);
            this.pnl_Main.Name = "pnl_Main";
            this.pnl_Main.Padding = new System.Windows.Forms.Padding(12);
            this.pnl_Main.Size = new System.Drawing.Size(634, 361);
            this.pnl_Main.TabIndex = 5;
            // 
            // tb_Message2
            // 
            this.tb_Message2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_Message2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tb_Message2.Location = new System.Drawing.Point(12, 201);
            this.tb_Message2.Name = "tb_Message2";
            this.tb_Message2.Size = new System.Drawing.Size(610, 148);
            this.tb_Message2.TabIndex = 9;
            this.tb_Message2.Text = "";
            // 
            // tb_Message1
            // 
            this.tb_Message1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_Message1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tb_Message1.Location = new System.Drawing.Point(12, 28);
            this.tb_Message1.Name = "tb_Message1";
            this.tb_Message1.Size = new System.Drawing.Size(610, 148);
            this.tb_Message1.TabIndex = 8;
            this.tb_Message1.Text = "";
            // 
            // lbl_Message1
            // 
            this.lbl_Message1.AutoSize = true;
            this.lbl_Message1.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_Message1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_Message1.Location = new System.Drawing.Point(12, 12);
            this.lbl_Message1.Name = "lbl_Message1";
            this.lbl_Message1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.lbl_Message1.Size = new System.Drawing.Size(56, 16);
            this.lbl_Message1.TabIndex = 6;
            this.lbl_Message1.Text = "Message1";
            // 
            // MessageForm
            // 
            this.AcceptButton = this.btn_Yes;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(634, 404);
            this.Controls.Add(this.pnl_Main);
            this.Controls.Add(this.pnl_bottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Message";
            this.pnl_bottom.ResumeLayout(false);
            this.pnl_Main.ResumeLayout(false);
            this.pnl_Main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_Message2;
        private System.Windows.Forms.Panel pnl_bottom;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_No;
        private System.Windows.Forms.Button btn_Yes;
        private System.Windows.Forms.Panel pnl_Main;
        private System.Windows.Forms.Label lbl_Message1;
        private System.Windows.Forms.RichTextBox tb_Message2;
        private System.Windows.Forms.RichTextBox tb_Message1;
    }
}