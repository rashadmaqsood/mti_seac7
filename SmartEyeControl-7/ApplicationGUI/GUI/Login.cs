//#define AppLoginSkip

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartEyeControl_7
{
    public partial class Login : Form
    {
        bool LoadMainForm = false;
        bool cancel = false;

        public bool _LoadMainForm
        {
            get { return LoadMainForm; }
            set { LoadMainForm = value; }
        }

        public Login()
        {

            InitializeComponent();
            this.Opacity = 1;
            
            ///***Commented By Irfan Sattar
            ///this.txt_login.Text = "";
            ///this.txt_password.Text = "";
            
            string strFilePath = @"c:\back_blue.swf";
            if (this.ShowDialog() == DialogResult.OK)
            {
                Application.DoEvents();
            }
            else
            {
                _LoadMainForm = false;
                cancel = true; ///return to Application.cs
            }
        }

        public Login(bool error)
        {

            InitializeComponent();
            this.Opacity = 1;

            ///***Commented By Irfan Sattar
            ///this.txt_login.Text = "";
            ///this.txt_password.Text = "";
            
            string strFilePath = @"c:\back_blue.swf";
            this.lbl_loginMsg.Visible = true;
            if (this.ShowDialog() == DialogResult.OK)
            {
                Application.DoEvents();
            }
            else
            {
                _LoadMainForm = false;
                cancel = true; //return to Application.cs
            }
        }

        public bool handleLogin()
        {
            while (!_LoadMainForm && !cancel)
            {
                Application.DoEvents();
            }
            return _LoadMainForm;
        }

        private void txt_Login_submit_Click(object sender, EventArgs e)
        {
            //assuming password to be correct
            this._LoadMainForm = true;
        }

        private void btn_login_Exit_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        #if AppLoginSkip
        bool FirsTime = true;
        #endif
        private void Login_Load(object sender, EventArgs e)
        {
#if AppLoginSkip
            //Added by Azeem to skip Login Process
            //txt_login.Text = "admin_aos";
            //txt_password.Text = "mti786";
            //txt_password.Text = "AccAdmin";
            //txt_login.Text = "accurate";
            //txt_password.Text = "accurate";

            txt_login.Text = "mazeem";
            txt_password.Text = "default";
            
            if (FirsTime)
            {
                FirsTime = false;
                txt_Login_submit_Click(this.btn_Login_submit, new EventArgs());
                this.DialogResult = DialogResult.OK;
            }
#endif
        }


    }
}

