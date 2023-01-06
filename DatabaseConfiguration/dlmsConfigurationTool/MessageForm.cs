using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dlmsConfigurationTool
{
    public partial class MessageForm : Form
    {
        public MessageForm(string lblMessage1 = null, string tbMessage1 = null, string lblMessage2 = null, string tbMessage2 = null)
        {
            InitializeComponent();

            this.lbl_Message1.Text = lblMessage1;
            this.tb_Message1. Text = tbMessage1;
            this.lbl_Message2.Text = lblMessage2;
            this.tb_Message2. Text = tbMessage2;
        }

        private void btn_Yes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btn_No_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
