using DatabaseConfiguration.DataSet;
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
    public partial class CopyEvents : Form
    {
        private Configs configurations;

        public string GroupName
        {
            get { return this.txtEventGroupName.Text; }
        }
        public bool IsExistingGroup
        {
            get { return this.chbSelectGroup.Checked; }
        }
        public Configs.Events_GroupRow CopyGroup
        {
            get { return (Configs.Events_GroupRow)this.cmbEvetGroups.SelectedItem; }
        }
        public Configs.Events_GroupRow ExistingGroup
        {
            get { return (Configs.Events_GroupRow)this.cmbExistingEventGroups.SelectedItem; }
        }
        public CopyEvents(Configs config)
        {
            InitializeComponent();
            this.chbSelectGroup.Checked = true;
            this.configurations = config;

            foreach (Configs.Events_GroupRow row in this.configurations.Events_Group)
            {
                this.cmbEvetGroups.Items.Add(row);
            }
            this.cmbEvetGroups.DisplayMember = "Events_group_Name";
            this.cmbEvetGroups.ValueMember = "id";
            this.cmbEvetGroups.SelectedIndex = 0;

            this.cmbExistingEventGroups.DisplayMember = "Events_group_Name";
            this.cmbExistingEventGroups.ValueMember = "id";
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(chbSelectGroup.Checked)
            {
                this.lblExistingGroups.Visible = true;
                this.cmbExistingEventGroups.Visible = true;

                this.lblNewGroup.Visible = false;
                this.txtEventGroupName.Visible = false;
            }
            else
            {
                this.lblExistingGroups.Visible = false;
                this.cmbExistingEventGroups.Visible = false;

                this.lblNewGroup.Visible = true;
                this.txtEventGroupName.Visible = true;
            }
        }

        private void cmbBillingGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbExistingEventGroups.Items.Clear();
            foreach(Configs.Events_GroupRow row in this.configurations.Events_Group)
            {
                if(!row.Events_group_Name.Equals(((Configs.Events_GroupRow)this.cmbEvetGroups.SelectedItem).Events_group_Name))
                {
                    this.cmbExistingEventGroups.Items.Add(row);
                }
            }
            if (this.cmbExistingEventGroups.Items.Count > 0)
                this.cmbExistingEventGroups.SelectedIndex = 0;
        }
    }
}
