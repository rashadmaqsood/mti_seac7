namespace Communicator
{
    partial class AboutBox1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_CompanyName = new System.Windows.Forms.Label();
            this.lbl_productName = new System.Windows.Forms.Label();
            this.lbl_version = new System.Windows.Forms.Label();
            this.lbl_buildDate = new System.Windows.Forms.Label();
            this.lbl_copyRights = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Company Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Product Name";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(12, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Build Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(12, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Product Version";
            this.label4.Click += new System.EventHandler(this.label2_Click);
            // 
            // lbl_CompanyName
            // 
            this.lbl_CompanyName.AutoSize = true;
            this.lbl_CompanyName.BackColor = System.Drawing.Color.Transparent;
            this.lbl_CompanyName.Location = new System.Drawing.Point(128, 90);
            this.lbl_CompanyName.Name = "lbl_CompanyName";
            this.lbl_CompanyName.Size = new System.Drawing.Size(82, 13);
            this.lbl_CompanyName.TabIndex = 0;
            this.lbl_CompanyName.Text = "Company Name";
            // 
            // lbl_productName
            // 
            this.lbl_productName.AutoSize = true;
            this.lbl_productName.BackColor = System.Drawing.Color.Transparent;
            this.lbl_productName.Location = new System.Drawing.Point(128, 9);
            this.lbl_productName.Name = "lbl_productName";
            this.lbl_productName.Size = new System.Drawing.Size(75, 13);
            this.lbl_productName.TabIndex = 0;
            this.lbl_productName.Text = "Product Name";
            this.lbl_productName.Click += new System.EventHandler(this.label2_Click);
            // 
            // lbl_version
            // 
            this.lbl_version.AutoSize = true;
            this.lbl_version.BackColor = System.Drawing.Color.Transparent;
            this.lbl_version.Location = new System.Drawing.Point(128, 35);
            this.lbl_version.Name = "lbl_version";
            this.lbl_version.Size = new System.Drawing.Size(82, 13);
            this.lbl_version.TabIndex = 0;
            this.lbl_version.Text = "Product Version";
            this.lbl_version.Click += new System.EventHandler(this.label2_Click);
            // 
            // lbl_buildDate
            // 
            this.lbl_buildDate.AutoSize = true;
            this.lbl_buildDate.BackColor = System.Drawing.Color.Transparent;
            this.lbl_buildDate.Location = new System.Drawing.Point(128, 62);
            this.lbl_buildDate.Name = "lbl_buildDate";
            this.lbl_buildDate.Size = new System.Drawing.Size(56, 13);
            this.lbl_buildDate.TabIndex = 0;
            this.lbl_buildDate.Text = "Build Date";
            // 
            // lbl_copyRights
            // 
            this.lbl_copyRights.AutoSize = true;
            this.lbl_copyRights.BackColor = System.Drawing.Color.Transparent;
            this.lbl_copyRights.Location = new System.Drawing.Point(12, 132);
            this.lbl_copyRights.Name = "lbl_copyRights";
            this.lbl_copyRights.Size = new System.Drawing.Size(82, 13);
            this.lbl_copyRights.TabIndex = 0;
            this.lbl_copyRights.Text = "Company Name";
            // 
            // AboutBox1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(367, 155);
            this.Controls.Add(this.lbl_buildDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl_version);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbl_productName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_copyRights);
            this.Controls.Add(this.lbl_CompanyName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox1";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Smart Eye Control 7";
            this.Shown += new System.EventHandler(this.AboutBox1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_CompanyName;
        private System.Windows.Forms.Label lbl_productName;
        private System.Windows.Forms.Label lbl_version;
        private System.Windows.Forms.Label lbl_buildDate;
        private System.Windows.Forms.Label lbl_copyRights;


    }
}
