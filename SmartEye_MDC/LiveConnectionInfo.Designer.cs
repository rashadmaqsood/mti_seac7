namespace Communicator
{
    partial class LiveConnectionInfo
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
            this.lstLog = new System.Windows.Forms.ListView();
            this.clmTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnPause = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnKillConnection = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstLog
            // 
            this.lstLog.AllowColumnReorder = true;
            this.lstLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLog.BackColor = System.Drawing.Color.Black;
            this.lstLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmTime,
            this.clmMessage});
            this.lstLog.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstLog.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstLog.Location = new System.Drawing.Point(-1, 0);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(577, 363);
            this.lstLog.TabIndex = 0;
            this.lstLog.UseCompatibleStateImageBehavior = false;
            this.lstLog.View = System.Windows.Forms.View.Details;
            // 
            // clmTime
            // 
            this.clmTime.Text = "Time";
            this.clmTime.Width = 141;
            // 
            // clmMessage
            // 
            this.clmMessage.Text = "Log";
            this.clmMessage.Width = 361;
            // 
            // btnPause
            // 
            this.btnPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPause.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.Location = new System.Drawing.Point(499, 367);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 9;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Location = new System.Drawing.Point(393, 367);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(101, 23);
            this.btnRegister.TabIndex = 10;
            this.btnRegister.Text = "Register To MSN";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnKillConnection
            // 
            this.btnKillConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKillConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKillConnection.Location = new System.Drawing.Point(3, 367);
            this.btnKillConnection.Name = "btnKillConnection";
            this.btnKillConnection.Size = new System.Drawing.Size(101, 23);
            this.btnKillConnection.TabIndex = 11;
            this.btnKillConnection.Text = "Close Connection";
            this.btnKillConnection.UseVisualStyleBackColor = true;
            this.btnKillConnection.Click += new System.EventHandler(this.btnKillConnection_Click);
            // 
            // LiveConnectionInfo
            // 
            this.AcceptButton = this.btnPause;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 393);
            this.Controls.Add(this.btnKillConnection);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.lstLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "LiveConnectionInfo";
            this.Text = "Live Meter Data";
            this.Load += new System.EventHandler(this.LiveConnectionInfo_Load);
            this.SizeChanged += new System.EventHandler(this.LiveConnectionInfo_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstLog;
        private System.Windows.Forms.ColumnHeader clmTime;
        private System.Windows.Forms.ColumnHeader clmMessage;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnKillConnection;
    }
}