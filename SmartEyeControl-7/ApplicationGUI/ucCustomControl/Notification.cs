using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Media;

namespace SmartEyeControl_7.ApplicationGUI.ucCustomControl
{
    public partial class Notification : Form
    {
        private int delay = 3000;
        private Sounds AudioNotification;
        private IWin32Window _WinOwner = null;

        public IWin32Window WinOwner
        {
            get { return _WinOwner; }
            set { _WinOwner = value; }
        }

        public enum Sounds : byte
        {
            None,
            Asterisk,
            Beep,
            Exclamation,
            Hand,
            Question
        }

        public Notification(string Main, string SubMessage, int Delay_ms, Sounds AudioNotification = Sounds.Asterisk)
        {
            this.AudioNotification = AudioNotification;
            InitializeComponent();
            delay = Delay_ms;
            lbl_Main.Text = Main;
            lbl_sub.Text = SubMessage;
            #region Adjust Length (SubMessage)
            if (Main.Length > 15 || SubMessage.Length > 20)
            {
                this.Width = this.Width + 150;
                for (int count = 1; count <= SubMessage.Length / 25; count++)
                {
                    int strIndex = count * 25;
                    if (SubMessage.Length > strIndex)
                    {
                        SubMessage = SubMessage.Insert(strIndex, "\r\n");
                    }
                    else
                        break;
                }
            }
            #endregion
            Thread t = new Thread(new ThreadStart(ShowNotification));
            t.Start();
            Application.DoEvents();
        }

        public Notification(string Main, string SubMessage, Sounds AudioNotification = Sounds.Asterisk)
        {
            this.AudioNotification = AudioNotification;
            InitializeComponent();
            lbl_Main.Text = Main;
            lbl_sub.Text = SubMessage;
            Application.DoEvents();
            #region Adjust Length (SubMessage)
            if (Main.Length > 15 || SubMessage.Length > 20)
            {
                this.Width = this.Width + 150;
                for (int count = 1; count <= SubMessage.Length / 25; count++)
                {
                    int strIndex = count * 25;
                    if (SubMessage.Length > strIndex)
                    {
                        SubMessage = SubMessage.Insert(strIndex, "\r\n");
                    }
                    else
                        break;
                }
            }
            #endregion
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - 1);
            Thread t = new Thread(new ThreadStart(ShowNotification));
            t.Start();
            Application.DoEvents();
        }

        public void ShowNotification()
        {
            try
            {
                this.Show(_WinOwner);
                this.Opacity = 0;
                Application.DoEvents();
                int upward_init = this.Height / 16;
                while (this.Opacity < .85)
                {
                    this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, this.Location.Y - upward_init);
                    this.Opacity += .05;
                    Thread.Sleep(20);
                    Application.DoEvents();
                }
                #region Play_Sounds_here
                try
                {
                    switch (AudioNotification)
                    {
                        case Sounds.Asterisk:
                            SystemSounds.Asterisk.Play();
                            break;
                        case Sounds.Beep:
                            SystemSounds.Beep.Play();
                            break;
                        case Sounds.Exclamation:
                            SystemSounds.Exclamation.Play();
                            break;
                        case Sounds.Hand:
                            SystemSounds.Hand.Play();
                            break;
                        case Sounds.Question:
                            SystemSounds.Question.Play();
                            break;
                        default:
                            break;
                    }
                }
                catch
                { }
                #endregion
                Thread.Sleep(delay);
                while (this.Opacity > 0)
                {
                    this.Opacity -= .05;
                    this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, this.Location.Y + upward_init);
                    Thread.Sleep(20);
                    Application.DoEvents();

                }
                this.Close();
            }
            catch
            {
            }
        }
    }
}
