using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace dlmsConfigurationTool
{
    public partial class ModelConfigurator : Form
    {
        public ModelConfigurator()
        {
            InitializeComponent();
            this.FrmApplicationConfiguration = new ApplicationConfiguration(this);
        }
        public ApplicationConfiguration FrmApplicationConfiguration { get; set; }

        private void ModelConfigurator_Load(object sender, EventArgs e)
        {
            this.Text = "dlms Configuration Tool [ v" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " ]";
            try
            {
                this.pnl_Main.Controls.Clear();

                this.Adjust_AppSize();

                this.pnl_Main.Controls.Add(this.FrmApplicationConfiguration);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ModelConfigurator_Resize(object sender, EventArgs e)
        {
            this.Adjust_AppSize();
        }

        private void Adjust_AppSize()
        {
            try
            {
                this.FrmApplicationConfiguration.Width = this.pnl_Main.Width;
                this.FrmApplicationConfiguration.Height = this.pnl_Main.Height;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
