using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using SharedCode.Comm.DataContainer;

namespace SmartEyeControl_7.Reporting
{
    public partial class Billing_ReportSelection : Form
    {
        List<BillingItem> obj_ToShow;

        public List<BillingItem> Obj_ToShow
        {
            get { return obj_ToShow; }
            set { obj_ToShow = value; }
        }

       
       
        public Billing_ReportSelection()
        {
            InitializeComponent();
        }
       
        public Billing_ReportSelection(BillingData obj_BillingData)
        {
            InitializeComponent();
            obj_ToShow = new List<BillingItem>();

            foreach (BillingItem item in obj_BillingData.BillingItems)
            {
                if (item.Name == "Active Energy Total" || item.Name == "Reactive Energy Total" || item.Name == "Cumulative MDI") 
                {
                    list_BillingQuantities.Items.Add(item);
                    list_BillingQuantities.SetItemCheckState(list_BillingQuantities.Items.Count-1, CheckState.Checked);
                }
            }
            //Added by Azeem //to Hide selection form
            button1_Click(button1, new EventArgs());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (list_BillingQuantities.CheckedItems.Count == 0)
            {
                Notification n = new Notification("Error", "Please select atleast one quantity");
            }
            for (int i = 0; i < list_BillingQuantities.CheckedItems.Count; i++)
            {
                 obj_ToShow.Add((BillingItem)list_BillingQuantities.CheckedItems[i]);
            }
            this.DialogResult = DialogResult.OK;
        }

        private void checkAll_Billing_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAll_Billing.Checked)
            {
                for (int count = 0; count < list_BillingQuantities.Items.Count; count++)
                {
                    list_BillingQuantities.SetItemCheckState(count, CheckState.Checked);
                }
            }
            else if (!checkAll_Billing.Checked)
            {
                for (int count = 0; count < list_BillingQuantities.Items.Count; count++)
                {
                    list_BillingQuantities.SetItemCheckState(count, CheckState.Unchecked);
                }
            }

        }

        
        
    }
}
