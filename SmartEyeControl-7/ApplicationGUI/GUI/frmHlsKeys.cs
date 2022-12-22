using SmartEyeControl_7.DB;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmHlsKeys : Form
    {
        HlsDbController DbController = null;
        public frmHlsKeys()
        {
            InitializeComponent();
            DbController = new HlsDbController();
        }

        private void SetReadOnlyPropertyOfControls(bool isReadOnly)
        {
            tbAuthentication_Key.ReadOnly = tbGLOBAL_Broadcast_EncryptionKey.ReadOnly =
                tbGLOBAL_Unicast_EncryptionKey.ReadOnly = tbMaster_Key.ReadOnly = tbSystemTitle.ReadOnly = isReadOnly;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnAddNew.Text == "Add New")
                {
                    SetReadOnlyPropertyOfControls(false);
                    lblId.Text = "";
                    lblActiveStatus.Text = "No";
                    btnAddNew.Text = "Save";
                    btnDelete.Text = "Cancel";
                    btnEdit.Visible = btnActivate.Visible = false;
                }
                else
                {
                    DbController.Insert_Update_HLS_Key(0, tbGLOBAL_Unicast_EncryptionKey.Text, tbGLOBAL_Broadcast_EncryptionKey.Text,
                                                        tbAuthentication_Key.Text, tbMaster_Key.Text, tbSystemTitle.Text, false, true);
                    Load_All_Keys();
                    SetReadOnlyPropertyOfControls(true);
                    btnAddNew.Text = "Add New";
                    ResetAllControls();
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.Message;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            try
            {
                if (btnEdit.Text == "Edit")
                {
                    SetReadOnlyPropertyOfControls(false);
                    btnEdit.Text = "Update";
                    btnDelete.Text = "Cancel";
                    btnAddNew.Visible = btnActivate.Visible = false;
                }
                else
                {
                    DbController.Insert_Update_HLS_Key(Convert.ToUInt32(lblId.Text), tbGLOBAL_Unicast_EncryptionKey.Text, tbGLOBAL_Broadcast_EncryptionKey.Text,
                        tbAuthentication_Key.Text, tbMaster_Key.Text, tbSystemTitle.Text, (lblActiveStatus.Text.Equals("true",StringComparison.OrdinalIgnoreCase))?true:false, false);
                    Load_All_Keys();
                    SetReadOnlyPropertyOfControls(true);
                    btnEdit.Text = "Edit";
                    ResetAllControls();
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.Message;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnDelete.Text == "Delete")
                {
                    DialogResult result = MessageBox.Show("Delete Confirmation", "Are you Sure to Delete this Key", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        DbController.Delete_HLS_Key(Convert.ToUInt32(lblId.Text));
                        Load_All_Keys();
                    }
                }
                else
                {
                    ResetAllControls();
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.Message;
            }
        }

        void ResetAllControls()
        {
            btnDelete.Text = "Delete";
            btnAddNew.Text = "Add New";
            btnEdit.Text = "Edit";
            SetReadOnlyPropertyOfControls(true);
            btnActivate.Visible = btnAddNew.Visible = btnEdit.Visible = true;
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Activate Confirmation", "Are you Sure to Activate this Key", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    DbController.Active_HLS_Key(Convert.ToUInt32(lblId.Text));
                    Load_All_Keys(); 
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.Message;
            }
        }

        private void frmHlsKeys_Load(object sender, EventArgs e)
        {
            Load_All_Keys();
        }

        private void dgvAvaliableKeys_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvAvaliableKeys.Rows[e.RowIndex];
            tbAuthentication_Key.Text               = row.Cells["AuthenticationKey"].Value.ToString();
            tbGLOBAL_Broadcast_EncryptionKey.Text   = row.Cells["GLOBAL_Broadcast_EncryptionKey"].Value.ToString();
            tbGLOBAL_Unicast_EncryptionKey.Text     = row.Cells["GLOBAL_Unicast_EncryptionKey"].Value.ToString();
            tbMaster_Key.Text                       = row.Cells["MasterKey"].Value.ToString();
            lblId.Text                              = row.Cells["id"].Value.ToString();
            lblActiveStatus.Text                    = row.Cells["IsActive"].Value.ToString();
            tbSystemTitle.Text                      = row.Cells["SystemTitle"].Value.ToString();
        }

        private void Load_All_Keys()
        {
            try
            {
                dgvAvaliableKeys.DataSource = DbController.LoadAll_HLS_Keys();
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.Message;
            }
        }
    }
}
