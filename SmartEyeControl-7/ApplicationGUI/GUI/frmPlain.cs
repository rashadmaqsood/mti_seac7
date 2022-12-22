using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmPlain : Form
    {
        public frmPlain(FrmContainer parent)
        {
            InitializeComponent();

            MdiParent = parent;
        }

        private void frmPlain_Load(object sender, EventArgs e)
        {

        }
    }
}
