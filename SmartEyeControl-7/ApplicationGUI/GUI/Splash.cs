using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using GUI;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;

namespace SmartEyeControl_7
{
    public partial class Splash : Form
    {
        [DllImport("User32")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);
        //public bool login_complete = false;
        public Splash()
        {
            InitializeComponent();
            this.Show();
            Application.DoEvents();
        }

        public void Splash_Load(object sender, EventArgs e)
        {
            this.Location = new System.Drawing.Point((Screen.PrimaryScreen.WorkingArea.Width / 2) - (Width / 2), ((Screen.PrimaryScreen.WorkingArea.Height / 2) - (Height / 2)));
            pictureBox1.Show();
            //Application.DoEvents();
        }

        public void hideSplash()
        {
            this.Hide();
            Application.DoEvents();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            //string path = Environment.CurrentDirectory + @"\\smarteye-screen.jpg";
            ////this.BackgroundImage = this.BackgroundImage = new Bitmap(@"c:\Temp\image.bmp");
            //this.BackgroundImage = this.BackgroundImage = new Bitmap(path);

            timer1.Enabled = false;
            timer1.Stop();
            while (this.Opacity != 0)
            {
                this.Opacity = this.Opacity - .20;
                Thread.Sleep(50);
                Application.DoEvents();
            }
            this.Visible = false;

            //again:
            //            SmartEyeControl_7.Login login = new SmartEyeControl_7.Login();
            //            //login.Opacity = 1;
            //            //while (login.Opacity != 0)
            //            //{
            //            //    login.Opacity = login.Opacity - .05;
            //            //    Thread.Sleep(10);
            //            //    Application.DoEvents();
            //            //}
            //            login.Visible = false;
            //            bool ISlogin = login.handleLogin();
            //            if (!ISlogin )
            //            {
            //                Application.ExitThread();
            //                Environment.Exit(0);
            //            }
            //            else if (login.txt_login.Text == "test" && login.txt_password.Text == "testing")
            //            {
            
//            }
//            else
//            {
//                MessageBox.Show("Incorrect Username or Password");
//                goto again;
//            }
            int hWnd;
            Process[] processRunning = Process.GetProcesses();
            foreach (Process pr in processRunning)
            {
                if (pr.ProcessName == "SmartEyeControl-7")
                {
                    hWnd = pr.MainWindowHandle.ToInt32();
                    ShowWindow(hWnd, 3); //3 is the enum for maximize
                }
            }
        }


        private void Main_appliationStart()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Environment.Exit(0);
            }
        }


    }
}
