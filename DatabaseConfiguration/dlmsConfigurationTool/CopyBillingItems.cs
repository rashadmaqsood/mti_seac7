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
    public partial class CopyBillingItems : Form
    {
        private Configs configurations;

        public string GroupName
        {
            get { return this.txtBillingGroupName.Text; }
        }
        public bool IsExistingGroup
        {
            get { return this.chbSelectGroup.Checked; }
        }
        public Configs.BillingItem_GroupRow CopyGroup
        {
            get { return (Configs.BillingItem_GroupRow)this.cmbBillingGroups.SelectedItem; }
        }
        public Configs.BillingItem_GroupRow ExistingGroup
        {
            get { return (Configs.BillingItem_GroupRow)this.cmbExistingBillingGroups.SelectedItem; }
        }
        public CopyBillingItems(Configs config)
        {
            InitializeComponent();
            this.chbSelectGroup.Checked = true;
            this.configurations = config;

            foreach (Configs.BillingItem_GroupRow row in this.configurations.BillingItem_Group)
            {
                this.cmbBillingGroups.Items.Add(row);
            }
            this.cmbBillingGroups.DisplayMember = "BillingItem_Group_Name";
            this.cmbBillingGroups.ValueMember = "id";
            this.cmbBillingGroups.SelectedIndex = 0;

            this.cmbExistingBillingGroups.DisplayMember = "BillingItem_Group_Name";
            this.cmbExistingBillingGroups.ValueMember = "id";
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(chbSelectGroup.Checked)
            {
                this.lblExistingGroups.Visible = true;
                this.cmbExistingBillingGroups.Visible = true;

                this.lblNewGroup.Visible = false;
                this.txtBillingGroupName.Visible = false;
            }
            else
            {
                this.lblExistingGroups.Visible = false;
                this.cmbExistingBillingGroups.Visible = false;

                this.lblNewGroup.Visible = true;
                this.txtBillingGroupName.Visible = true;
            }
        }

        private void cmbBillingGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbExistingBillingGroups.Items.Clear();
            foreach(Configs.BillingItem_GroupRow row in this.configurations.BillingItem_Group)
            {
                if(!row.BillingItem_Group_Name.Equals(((Configs.BillingItem_GroupRow)this.cmbBillingGroups.SelectedItem).BillingItem_Group_Name))
                {
                    this.cmbExistingBillingGroups.Items.Add(row);
                }
            }
            if (this.cmbExistingBillingGroups.Items.Count > 0)
                this.cmbExistingBillingGroups.SelectedIndex = 0;
        }
    }
}
