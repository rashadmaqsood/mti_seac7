using System.Windows.Forms;
namespace SmartEyeControl_7
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.btn_Login_submit = new System.Windows.Forms.Button();
            this.txt_login = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.btn_login_Exit = new System.Windows.Forms.Button();
            this.lbl_loginMsg = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Login_submit
            // 
            this.btn_Login_submit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Login_submit.Image = ((System.Drawing.Image)(resources.GetObject("btn_Login_submit.Image")));
            this.btn_Login_submit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Login_submit.Location = new System.Drawing.Point(121, 148);
            this.btn_Login_submit.Name = "btn_Login_submit";
            this.btn_Login_submit.Size = new System.Drawing.Size(81, 37);
            this.btn_Login_submit.TabIndex = 3;
            this.btn_Login_submit.Text = "&Submit";
            this.btn_Login_submit.UseVisualStyleBackColor = true;
            this.btn_Login_submit.Click += new System.EventHandler(this.txt_Login_submit_Click);
            // 
            // txt_login
            // 
            this.txt_login.BackColor = System.Drawing.Color.White;
            this.txt_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_login.ForeColor = System.Drawing.Color.Black;
            this.txt_login.Location = new System.Drawing.Point(121, 44);
            this.txt_login.Name = "txt_login";
            this.txt_login.Size = new System.Drawing.Size(168, 29);
            this.txt_login.TabIndex = 1;
            this.txt_login.Text = "Administrator";
            // 
            // txt_password
            // 
            this.txt_password.BackColor = System.Drawing.Color.White;
            this.txt_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_password.ForeColor = System.Drawing.Color.Black;
            this.txt_password.Location = new System.Drawing.Point(121, 92);
            this.txt_password.Name = "txt_password";
            this.txt_password.PasswordChar = '*';
            this.txt_password.Size = new System.Drawing.Size(168, 31);
            this.txt_password.TabIndex = 2;
            this.txt_password.Text = "default";
            // 
            // btn_login_Exit
            // 
            this.btn_login_Exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_login_Exit.Image = ((System.Drawing.Image)(resources.GetObject("btn_login_Exit.Image")));
            this.btn_login_Exit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_login_Exit.Location = new System.Drawing.Point(208, 149);
            this.btn_login_Exit.Name = "btn_login_Exit";
            this.btn_login_Exit.Size = new System.Drawing.Size(81, 37);
            this.btn_login_Exit.TabIndex = 4;
            this.btn_login_Exit.Text = "&Exit";
            this.btn_login_Exit.UseVisualStyleBackColor = true;
            this.btn_login_Exit.Click += new System.EventHandler(this.btn_login_Exit_Click);
            // 
            // lbl_loginMsg
            // 
            this.lbl_loginMsg.AutoSize = true;
            this.lbl_loginMsg.BackColor = System.Drawing.Color.Transparent;
            this.lbl_loginMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_loginMsg.ForeColor = System.Drawing.Color.DarkRed;
            this.lbl_loginMsg.Location = new System.Drawing.Point(27, 201);
            this.lbl_loginMsg.Name = "lbl_loginMsg";
            this.lbl_loginMsg.Size = new System.Drawing.Size(228, 16);
            this.lbl_loginMsg.TabIndex = 5;
            this.lbl_loginMsg.Tag = "";
            this.lbl_loginMsg.Text = "Invalid Login name or Password";
            this.lbl_loginMsg.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txt_login);
            this.panel1.Controls.Add(this.txt_password);
            this.panel1.Controls.Add(this.lbl_loginMsg);
            this.panel1.Controls.Add(this.btn_login_Exit);
            this.panel1.Controls.Add(this.btn_Login_submit);
            this.panel1.Location = new System.Drawing.Point(636, 202);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(322, 243);
            this.panel1.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(30, 139);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(72, 47);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(27, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(27, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Login Id";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(251, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(478, 40);
            this.label1.TabIndex = 8;
            this.label1.Text = "Smart Eye Advance Control";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(3, 202);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(404, 243);
            this.panel2.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel3.Location = new System.Drawing.Point(3, 24);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 205);
            this.panel3.TabIndex = 0;
            // 
            // Login
            // 
            this.AcceptButton = this.btn_Login_submit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.btn_login_Exit;
            this.ClientSize = new System.Drawing.Size(1001, 601);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Login_submit;
        private System.Windows.Forms.Button btn_login_Exit;
        public TextBox txt_login;
        public TextBox txt_password;
        private Label lbl_loginMsg;
        private Panel panel1;
        private Label label1;
        private Label label3;
        private Label label2;
        private PictureBox pictureBox1;
        private Panel panel2;
        private Panel panel3;
    }
}