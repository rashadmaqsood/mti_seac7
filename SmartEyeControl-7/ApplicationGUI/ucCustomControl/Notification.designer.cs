namespace SmartEyeControl_7.ApplicationGUI.ucCustomControl
{
    partial class Notification
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
            this.lbl_Main = new System.Windows.Forms.Label();
            this.lbl_sub = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_Main
            // 
            this.lbl_Main.AutoSize = true;
            this.lbl_Main.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Main.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Main.Location = new System.Drawing.Point(12, 5);
            this.lbl_Main.Name = "lbl_Main";
            this.lbl_Main.Size = new System.Drawing.Size(32, 19);
            this.lbl_Main.TabIndex = 0;
            this.lbl_Main.Text = "Main";
            this.lbl_Main.UseCompatibleTextRendering = true;
            // 
            // lbl_sub
            // 
            this.lbl_sub.AutoSize = true;
            this.lbl_sub.BackColor = System.Drawing.Color.Transparent;
            this.lbl_sub.Location = new System.Drawing.Point(12, 26);
            this.lbl_sub.Name = "lbl_sub";
            this.lbl_sub.Size = new System.Drawing.Size(70, 17);
            this.lbl_sub.TabIndex = 0;
            this.lbl_sub.Text = "SubMessage";
            this.lbl_sub.UseCompatibleTextRendering = true;
            // 
            // Notification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RosyBrown;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(178, 80);
            this.Controls.Add(this.lbl_sub);
            this.Controls.Add(this.lbl_Main);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(1182, 659);
            this.Name = "Notification";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Notification";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Main;
        private System.Windows.Forms.Label lbl_sub;
    }
}