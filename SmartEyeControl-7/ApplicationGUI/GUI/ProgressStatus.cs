using System;
using System.Threading;
using System.Windows.Forms;

namespace GUI
{
    partial class ProgressDialog : Form
    {
        #region Data_Members
        private string dialogTitle;
        private string dialogStatus;
        private bool _UserInputEnable;
        private bool isAutoHideNow; 
        #endregion

        public ProgressDialog()
        {
            InitializeComponent();
            _UserInputEnable = true;
            EnableProgressBar = false;
            btnCancel.Enabled = false;
            
            IsAutoHideNow = false;
        }
        
        public string DialogTitle
        {
            get { return dialogTitle; }
            set { dialogTitle = value; }
        }
        
        public string DialogStatus
        {
            get { return dialogStatus; }
            set { dialogStatus = value; }
        }

        public bool UserInputEnable
        {
            get 
            {
                return _UserInputEnable;
            }
            set
            {
                _UserInputEnable = value;
                if (value)
                {
                    okButton.Enabled = true;
                    btnCancel.Enabled = true;
                }
                else
                {
                    okButton.Enabled = false;
                    btnCancel.Enabled = false;
                }
            }
        }

        public void UpdateDialogStatusHandler(String statusMsg)
        {
            if (this.IsHandleCreated && !this.IsDisposed)
            {
                if(txtStatus.Text.Length>0)
                    txtStatus.Text += "\r\n"+statusMsg;
                else
                   txtStatus.Text += statusMsg;
                
                //scroll to end
                txtStatus.SelectionStart = txtStatus.Text.Length;

                txtStatus.SelectionLength = 0;

                txtStatus.ScrollToCaret();

                Thread.Sleep(1);
                if (IsAutoHideNow)
                {
                    Application.DoEvents();
                    okButton.PerformClick();
                    Application.DoEvents();
                }
            }
            ///If Bool True Hide Dialog Now
        }

        public bool IsAutoHideNow
        {
            get { return isAutoHideNow; }
            set { isAutoHideNow = value; }
        }

        private void ProgressDialog_Load(object sender, EventArgs e)
        {
            lblTitle.Text = DialogTitle;
            txtStatus.Text = DialogStatus;
        }

        internal void ConnController_ProcessStatusHandler(string obj)
        {   
            Action<String> updater = UpdateDialogStatusHandler;
            try
            {
                if (txtStatus.IsHandleCreated && !txtStatus.IsDisposed)
                {
                    txtStatus.Invoke(updater, obj);
                    ///If Bool True Hide Dialog Now
                    if (IsAutoHideNow)
                    {
                        Application.DoEvents();
                        okButton.PerformClick();
                        Application.DoEvents();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Invoke(updater, "Error Displaying status message");
            }
        }

        private void ProgressDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_UserInputEnable)
            {
                MessageBox.Show(this, "Cann't Cancel,task is being executed", "Busy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        #region Properties
        public bool EnableProgressBar
        {
            get
            {
                return progressBar1.Enabled && progressBar1.Visible;
            }
            set 
            {
                if (value)
                {
                    this.progressBar1.Enabled = true;
                    this.progressBar1.Visible = true;
                }
                else
                { 
                    this.progressBar1.Enabled = false;
                    this.progressBar1.Visible = false;
                }
            }
        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Are you sure you want to abort the process?", "Process Abort", MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
            if (r == DialogResult.OK)
            {
            }
            else
            {
                return;
            }
            this.DialogTitle = "Process Aborted";
        }


        public void okButton_Click(object sender, EventArgs e)
        {
            //this.dialogTitle = "Complete";
            //this.Visible = false;
        }

    }
}
