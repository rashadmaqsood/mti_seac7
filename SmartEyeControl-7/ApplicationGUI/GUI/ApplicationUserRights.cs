using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using SharedCode.Comm.HelperClasses;
using SmartEyeControl_7.DB;
using SEAC.Common;

namespace AccurateOptocomSoftware.ApplicationGUI.GUI
{
    public partial class ApplicationUserRights : Form
    {
        #region Properties

        public ApplicationRight UserRigts { get; set; }

        public DataBaseController dbController { get; set; }

        private ApplicationRight _backupRights;

        internal List<AccessRights> CurrentRights = null;
        internal List<sAccessRights> sCurrentRights = null;
        bool IsMsnRangeSelected = false;

        public String CurrentNode = null;

        public string Identifier { get { return txtIdentifier.Text; } set { txtIdentifier.Text = value; } }

        public string Role { get { return cmRole.Text; } set { cmRole.Text = value; } }

        public const String Row_Error_MSG1 = "Please Assign Read Access!!";
        public String Row_Error_MSG_MSN = "";

        #endregion

        public ApplicationUserRights(ApplicationRight rights)
        {
            UserRigts = rights;
            MakeDefaultOrBackupCopy();
            InitializeComponent();
            dbController = new DataBaseController();

            //Added by M.Azeem Inayat
            cmRole.DataSource = Enum.GetNames(typeof(UserTypeID)); //Issue resolved: User Group selection fail When opened for Edition
        }

        private void MakeDefaultOrBackupCopy()
        {
            if (UserRigts != null)
            {
                _backupRights = (ApplicationRight)UserRigts.Clone();
            }
            else
            {
                _backupRights = ApplicationRight.GetDefaultRights();
                UserRigts = ApplicationRight.GetDefaultRights();
            }
        }

        private void ApplicationUserRights_Load(object sender, EventArgs e)
        {
            PopulateTreeView(UserRigts);
            //cmRole.DataSource = Enum.GetNames(typeof(UserTypeID)); //Commented By Azeem Inayat
        }

        private void PopulateTreeView(ApplicationRight userRight)
        {
            List<AccessRights> vals = null;
            TreeNode ParentNode = null;

            try
            {
                //Initialize ApplicationRight Object here
                GetApplicationRightWithDefaults(ref userRight);

                trvQuantities.Nodes.Clear();
                trvQuantities.ImageList = LoadTreeViewIcons();

                //commented in v4.8.15
                //PropertyInfo[] pinfo;
                //pinfo = ApplicationRight.Get_PropertyInfo().ToArray();

                //Assign Fix TreeNodes here
                #region //Billing Tree View Node

                ParentNode = new TreeNode();
                ParentNode.Text = ParentNode.Name = "Billing Rights";
                ParentNode.ImageKey = "billing";
                ParentNode.SelectedImageKey = "billing_active";
                //Context Menu Strip
                ParentNode.ContextMenuStrip = Context_Rights;
                vals = userRight.BillingRights;
                ParentNode.Tag = vals;
                Init_TreeView_ChildNodes(ParentNode, vals);
                trvQuantities.Nodes.Add(ParentNode);

                #endregion
                #region //LoadProfile Tree View Node

                ParentNode = new TreeNode();
                ParentNode.Text = ParentNode.Name = "LoadProfile Rights";
                ParentNode.ImageKey = "loadprofile";
                ParentNode.SelectedImageKey = "loadprofile_active";
                //Context Menu Strip
                ParentNode.ContextMenuStrip = Context_Rights;
                vals = userRight.LoadProfileRights;
                ParentNode.Tag = vals;
                Init_TreeView_ChildNodes(ParentNode, vals);
                trvQuantities.Nodes.Add(ParentNode);

                #endregion
                #region //Instantaneous Tree View Node

                ParentNode = new TreeNode();

                ParentNode.Text = ParentNode.Name = "Instantaneous Data Rights";
                ParentNode.ImageKey = "instantaneous";
                ParentNode.SelectedImageKey = "instantaneous_active";

                //Context Menu Strip
                ParentNode.ContextMenuStrip = Context_Rights;
                vals = userRight.InstantenousDataRights;
                ParentNode.Tag = vals;
                Init_TreeView_ChildNodes(ParentNode, vals);
                trvQuantities.Nodes.Add(ParentNode);

                #endregion
                #region //Events Tree View Node

                ParentNode = new TreeNode();
                ParentNode.Text = ParentNode.Name = "Events Data Rights";
                ParentNode.ImageKey = "events";
                ParentNode.SelectedImageKey = "events_active";

                //Context Menu Strip
                ParentNode.ContextMenuStrip = Context_Rights;
                vals = userRight.EventsRights;
                ParentNode.Tag = vals;
                Init_TreeView_ChildNodes(ParentNode, vals);
                trvQuantities.Nodes.Add(ParentNode);

                #endregion
                #region //Events Tree View Node

                ParentNode = new TreeNode();
                ParentNode.Text = ParentNode.Name = "Meter Events Rights";
                ParentNode.ImageKey = "events";
                ParentNode.SelectedImageKey = "events_active";

                //Context Menu Strip
                ParentNode.ContextMenuStrip = Context_Rights;
                vals = userRight.MeterEventsRights;
                ParentNode.Tag = vals;
                Init_TreeView_ChildNodes(ParentNode, vals);
                trvQuantities.Nodes.Add(ParentNode);

                #endregion
                #region //Settings Tree View Node

                ParentNode = new TreeNode();

                ParentNode.Text = ParentNode.Name = "Settings Rights";
                ParentNode.ImageKey = "settings";
                ParentNode.SelectedImageKey = "settings_active";

                //Context Menu Strip
                ParentNode.ContextMenuStrip = Context_Rights;
                vals = userRight.SettingsRights;
                ParentNode.Tag = vals;
                Init_TreeView_ChildNodes(ParentNode, vals);
                trvQuantities.Nodes.Add(ParentNode);

                #endregion
                #region //Debug Tree View Node

                ParentNode = new TreeNode();

                ParentNode.Text = ParentNode.Name = "Debug Rights";
                ParentNode.ImageKey = "debug";
                ParentNode.SelectedImageKey = "debug_active";

                //Context Menu Strip
                ParentNode.ContextMenuStrip = Context_Rights;
                vals = userRight.DebugRights;
                ParentNode.Tag = vals;
                Init_TreeView_ChildNodes(ParentNode, vals);
                trvQuantities.Nodes.Add(ParentNode);

                #endregion

                #region //Meter Tree View Node

                ParentNode = new TreeNode();
                ParentNode.Text = ParentNode.Name = "Meter Rights";
                ParentNode.ImageKey = "parameterization";
                ParentNode.SelectedImageKey = "parameterization_active";
                //Context Menu Strip
                ParentNode.ContextMenuStrip = Context_Rights;
                vals = userRight.MeterRights;
                ParentNode.Tag = vals;
                Init_TreeView_ChildNodes_CustomizedCaptions(ParentNode, vals, "Visibility", "Advance Report");
                trvQuantities.Nodes.Add(ParentNode);

                #endregion

                #region Meter Parameters Tree View Node

                List<PropertyInfo> MeterParameters = ApplicationRight.GetMeteringParameters_PropertyInfo();
                foreach (var item in MeterParameters)
                {
                    ParentNode = new TreeNode();
                    ParentNode.Text = ParentNode.Name = item.Name;

                    ParentNode.ImageKey = "parameterization";
                    ParentNode.SelectedImageKey = "parameterization_active";

                    //Context Menu Strip
                    ParentNode.ContextMenuStrip = Context_Rights;
                    vals = null;
                    if (item != null && item.PropertyType == typeof(List<AccessRights>))
                        vals = (List<AccessRights>)item.GetValue(userRight, null);
                    if (vals == null)
                        continue;
                    ParentNode.Tag = vals;

                    Init_TreeView_ChildNodes(ParentNode, vals);
                    trvQuantities.Nodes.Add(ParentNode);
                }

                #endregion

                #region Modem Parameters Tree View Node

                List<PropertyInfo> ModemParameters = ApplicationRight.GetModemParameters_PropertyInfo();
                foreach (var item in ModemParameters)
                {
                    ParentNode = new TreeNode();
                    ParentNode.Text = ParentNode.Name = item.Name;

                    ParentNode.ImageKey = "modem";
                    ParentNode.SelectedImageKey = "modem_active";

                    //Context Menu Strip
                    ParentNode.ContextMenuStrip = Context_Rights;
                    vals = null;
                    if (item != null && item.PropertyType == typeof(List<AccessRights>))
                        vals = (List<AccessRights>)item.GetValue(userRight, null);
                    if (vals == null)
                        continue;
                    ParentNode.Tag = vals;

                    Init_TreeView_ChildNodes(ParentNode, vals);
                    trvQuantities.Nodes.Add(ParentNode);
                }

                #endregion

                #region Standard Modem Parameters Tree View Node

                List<PropertyInfo> StandardModemParameters = ApplicationRight.GetStandardModemParameters_PropertyInfo();
                foreach (var item in StandardModemParameters)
                {
                    ParentNode = new TreeNode();
                    ParentNode.Text = ParentNode.Name = item.Name;

                    ParentNode.ImageKey = "modem";
                    ParentNode.SelectedImageKey = "modem_active";

                    //Context Menu Strip
                    ParentNode.ContextMenuStrip = Context_Rights;
                    vals = null;
                    if (item != null && item.PropertyType == typeof(List<AccessRights>))
                        vals = (List<AccessRights>)item.GetValue(userRight, null);
                    if (vals == null)
                        continue;
                    ParentNode.Tag = vals;

                    Init_TreeView_ChildNodes(ParentNode, vals);
                    trvQuantities.Nodes.Add(ParentNode);
                }

                #endregion

                #region //Energy Mizer Tree View Node

                ParentNode = new TreeNode();
                ParentNode.Text = ParentNode.Name = "EnergyMizer Rights";
                ParentNode.ImageKey = "EnergyMizer";
                ParentNode.SelectedImageKey = "EnergyMizer_active";
                //Context Menu Strip
                ParentNode.ContextMenuStrip = Context_Rights;
                vals = userRight.EnergyMizerRights;
                ParentNode.Tag = vals;
                Init_TreeView_ChildNodes(ParentNode, vals);
                trvQuantities.Nodes.Add(ParentNode);

                #endregion

                #region HDLC Setup Parameters Tree View Node

                List<PropertyInfo> HDLCSetupParameters = ApplicationRight.GetHDLCSetupParameters_PropertyInfo();
                foreach (var item in HDLCSetupParameters)
                {
                    ParentNode = new TreeNode();
                    ParentNode.Text = ParentNode.Name = item.Name;

                    ParentNode.ImageKey = "modem";
                    ParentNode.SelectedImageKey = "modem_active";

                    //Context Menu Strip
                    ParentNode.ContextMenuStrip = Context_Rights;
                    vals = null;
                    if (item != null && item.PropertyType == typeof(List<AccessRights>))
                        vals = (List<AccessRights>)item.GetValue(userRight, null);
                    if (vals == null)
                        continue;
                    ParentNode.Tag = vals;

                    Init_TreeView_ChildNodes(ParentNode, vals);
                    trvQuantities.Nodes.Add(ParentNode);
                }

                #endregion

                #region //Communication Tree View Node

                ParentNode = new TreeNode();
                ParentNode.Text = ParentNode.Name = "General Rights";
                ParentNode.ImageKey = "communication";
                ParentNode.SelectedImageKey = "communication_active";
                //Context Menu Strip
                ParentNode.ContextMenuStrip = Context_Rights;
                vals = userRight.GeneralRights;
                ParentNode.Tag = vals;
                Init_TreeView_ChildNodes_CustomizedCaptions(ParentNode, vals, "Visibility", "Advance Report");
                trvQuantities.Nodes.Add(ParentNode);

                #endregion

                #region //MSN Range
                List<sAccessRights> sVals = null;
                ParentNode = new TreeNode();
                ParentNode.Text = ParentNode.Name = "MSN Range";
                //ParentNode.ImageKey = "MSN Range";
                //ParentNode.SelectedImageKey = "events_active";

                //Context Menu Strip
                ParentNode.ContextMenuStrip = Context_Rights;
                sVals = userRight.MsnRange;
                ParentNode.Tag = sVals;
                Init_TreeView_ChildNodes_CustomizedCaptions(ParentNode, sVals, "Start", "END");
                trvQuantities.Nodes.Add(ParentNode);

                #endregion

                //GeneralRights

                #region //Other Rights
                //List<AccessRights> Vals = null;
                ParentNode = new TreeNode();
                ParentNode.Text = ParentNode.Name = "Other Rights";
                //ParentNode.ImageKey = "MSN Range";
                //ParentNode.SelectedImageKey = "events_active";

                //Context Menu Strip
                ParentNode.ContextMenuStrip = Context_Rights;
                vals = userRight.OtherRights;
                ParentNode.Tag = vals;
                Init_TreeView_ChildNodes_CustomizedCaptions(ParentNode, vals, "Setting", "Don't care");
                trvQuantities.Nodes.Add(ParentNode);

                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while Populate Application UserRight", "Error Show Application User Right");
            }

        }

        private void Init_TreeView_ChildNodes(TreeNode ParentNode, List<AccessRights> vals)
        {
            foreach (var val in vals)
            {
                var childNode = new TreeNode();
                childNode.Tag = vals;
                childNode.Name = childNode.Text = val.QuantityName;
                childNode.Nodes.Add(string.Format("(R:{0}/W:{1})", val.Read, val.Write));

                childNode.ImageKey = ParentNode.ImageKey;
                childNode.SelectedImageKey = ParentNode.SelectedImageKey;

                ParentNode.Nodes.Add(childNode);
            }
        }
        //============================== v4.8.15 by Azeem =========================================
        private void Init_TreeView_ChildNodes_CustomizedCaptions(TreeNode ParentNode, List<AccessRights> vals, string caption1, string caption2)
        {
            foreach (var val in vals)
            {
                var childNode = new TreeNode();
                childNode.Tag = vals;
                childNode.Name = childNode.Text = val.QuantityName;
                //childNode.Nodes.Add(string.Format("(Visibility:{0}/Not Used:{1})", val.Read, val.Write));
                childNode.Nodes.Add(string.Format("({0}:{1}/{2}:{3})", caption1, val.Read, caption2, val.Write));

                childNode.ImageKey = ParentNode.ImageKey;
                childNode.SelectedImageKey = ParentNode.SelectedImageKey;

                ParentNode.Nodes.Add(childNode);
            }
        }
        //============================== v4.8.15 by Azeem =========================================
        private void Init_TreeView_ChildNodes_CustomizedCaptions(TreeNode ParentNode, List<sAccessRights> vals, string caption1, string caption2)
        {
            foreach (var val in vals)
            {
                var childNode = new TreeNode();
                childNode.Tag = vals;
                childNode.Name = childNode.Text = val.QuantityName;
                //childNode.Nodes.Add(string.Format("(Visibility:{0}/Not Used:{1})", val.Read, val.Write));
                childNode.Nodes.Add(string.Format("({0}:{1}/{2}:{3})", caption1, val.Start, caption2, val.End));

                childNode.ImageKey = ParentNode.ImageKey;
                childNode.SelectedImageKey = ParentNode.SelectedImageKey;

                ParentNode.Nodes.Add(childNode);
            }
        }

        private void GetApplicationRightWithDefaults(ref ApplicationRight userRight)
        {
            if (userRight == null)
            {
                userRight = ApplicationRight.GetDefaultRights();
                return;
            }
            PropertyInfo[] pinfo;
            List<AccessRights> vals = null;
            List<AccessRights> Default_vals = null;
            List<sAccessRights> svals = null;
            List<sAccessRights> sDefault_vals = null;
            pinfo = ApplicationRight.Get_PropertyInfo().ToArray();

            ApplicationRight DefaultUserRight = ApplicationRight.GetDefaultRights();
            //Initialize ApplicationRight Object here
            foreach (var item in pinfo)
            {
                vals = null; svals = null;

                if (item != null && item.PropertyType == typeof(List<AccessRights>))
                    vals = (List<AccessRights>)item.GetValue(userRight, null);
                else if (item != null && item.PropertyType == typeof(List<sAccessRights>))
                    svals = (List<sAccessRights>)item.GetValue(userRight, null);

                if (vals == null && item.PropertyType == typeof(List<AccessRights>))
                {
                    Default_vals = (List<AccessRights>)item.GetValue(DefaultUserRight, null);
                    item.SetValue(userRight, Default_vals, null);
                }
                else if (svals == null && item.PropertyType == typeof(List<sAccessRights>))
                {
                    sDefault_vals = (List<sAccessRights>)item.GetValue(DefaultUserRight, null);
                    item.SetValue(userRight, sDefault_vals, null);
                }


                ////////////////////////////////////////
                // Orignal  //By Furqan
                //if (item != null && item.PropertyType == typeof(List<AccessRights>))
                //    vals = (List<AccessRights>)item.GetValue(userRight, null);

                //if (vals == null)
                //{
                //    Default_vals = (List<AccessRights>)item.GetValue(DefaultUserRight, null);
                //    item.SetValue(userRight, Default_vals, null);
                //}
                //////////////////////////////////////
            }
        }

        private ImageList LoadTreeViewIcons()
        {
            FileInfo fileInfo = null;
            try
            {
                var ilist = new ImageList();
                // The default image size is 16 x 16, which sets up a larger 
                // image size. 
                ilist.ImageSize = new Size(25, 25);
                var files = LocalCommon.Directory_GetFiles(Directory.GetCurrentDirectory() + @"\images\icons",
                                          "*.png|*.ico|*.jpg|*.jpeg", SearchOption.TopDirectoryOnly);
                //var files = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\images\icons","*.png*.ico*.jpg*.jpeg");

                foreach (var fileURL in files)
                {
                    fileInfo = new FileInfo(fileURL);
                    string key = fileInfo.Name.Replace(fileInfo.Extension, string.Empty);
                    key.ToLower();
                    ilist.Images.Add(key, Image.FromFile(fileURL));
                }
                return ilist;
            }
            catch
            {
                throw;
            }
        }

        private void trvQuantities_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            if (e.Node.Name == "MSN Range" || (e.Node.Parent != null && e.Node.Parent.Name == "MSN Range"))
            {
                IsMsnRangeSelected = true;
                dtgRights.Columns.Clear();
                dtgRights.DataSource = null;
                dtgRights.Refresh();

                var slist = (List<sAccessRights>)e.Node.Tag;
                dtgRights.DataSource = slist;

                //Current Node Selected
                CurrentNode = e.Node.Name;
                sCurrentRights = slist;

                //Add Title here
                lbl_header.Text = CurrentNode;

            }
            else
            {
                IsMsnRangeSelected = false;
                dtgRights.Columns.Clear();
                dtgRights.DataSource = null;
                dtgRights.Refresh();

                var list = (List<AccessRights>)e.Node.Tag;
                dtgRights.DataSource = list;

                if (e.Node.Name == "Communication Rights" || (e.Node.Parent != null && e.Node.Parent.Name == "Communication Rights"))
                {
                    dtgRights.Columns[1].HeaderText = "Visible";
                    dtgRights.Columns[2].HeaderText = "Advance Report"; //Wapda Format will be displayed in default while Detail report will be displayed by this setting
                }
                else if (e.Node.Name == "Other Rights" || (e.Node.Parent != null && e.Node.Parent.Name == "Other Rights"))
                {
                    dtgRights.Columns[1].HeaderText = "Setting";
                    dtgRights.Columns[2].HeaderText = "Don't Care";
                }

                //Current Node Selected
                CurrentNode = e.Node.Name;
                CurrentRights = list;

                //Add Title here
                lbl_header.Text = CurrentNode;

            }
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (btnSaveChanges.Text == "Done")
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                if (!string.IsNullOrEmpty(txtIdentifier.Text) && !string.IsNullOrEmpty(cmRole.Text))
                {
                    PopulateTreeView(UserRigts);
                    _backupRights = (ApplicationRight)UserRigts.Clone();
                    btnSaveChanges.Text = "Done";
                }
                else
                {
                    MessageBox.Show("Identifier Or Role fields must not be empty!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {

            PopulateTreeView(_backupRights);
            UserRigts = _backupRights;
            btnSaveChanges.Text = "Save Changes";
        }

        private void dtgRights_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            btnSaveChanges.Text = "Save Changes";
        }

        #region Context_Menu Assign Access Rights

        private void writeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String MSG = null;
            bool IsAnyWriteExist = false;
            bool IsAnyNotWriteExist = false;
            try
            {
                IsAnyWriteExist = CurrentRights.Exists((X) => X.Write);
                IsAnyNotWriteExist = CurrentRights.Exists((X) => !X.Write);
                dtgRights.SuspendLayout();

                foreach (var currentAccessRight in CurrentRights)
                {
                    if (currentAccessRight != null)
                    {
                        if (IsAnyNotWriteExist && IsAnyWriteExist)
                        {
                            currentAccessRight.Write = true;
                            MSG = String.Format("Write & Read Access Rights Assigned Some");
                        }
                        else if (IsAnyNotWriteExist && !IsAnyWriteExist)
                        {
                            currentAccessRight.Write = true;
                            MSG = String.Format("Write & Read Access Rights Assigned All");
                        }
                        else if (!IsAnyNotWriteExist && IsAnyWriteExist)
                        {
                            currentAccessRight.Write = false;
                            MSG = String.Format("Write Access Rights UN-Assigned All");
                        }
                        else
                        {
                            currentAccessRight.Write = false;
                            MSG = String.Format("Write Access Rights UN-Assigned All");
                        }
                    }
                }

                dtgRights.DataSource = null;
                dtgRights.DataSource = CurrentRights;
                MessageBox.Show(MSG, String.Format("{0}-Write Access Rights", CurrentNode));
            }
            catch
            {
                MessageBox.Show("Error occurred while Assign Write Access Rights", "Error");
            }
            finally
            {
                dtgRights.ResumeLayout();
            }
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String MSG = "Read & Write Access Rights UN-Assigned";
            try
            {
                dtgRights.SuspendLayout();

                foreach (var currentAccessRight in CurrentRights)
                {
                    if (currentAccessRight != null)
                    {
                        currentAccessRight.Write = false;
                        currentAccessRight.Read = false;
                    }
                }

                dtgRights.DataSource = null;
                dtgRights.DataSource = CurrentRights;
                MessageBox.Show(MSG, String.Format("{0}-Access Rights", CurrentNode));
            }
            catch
            {
                MessageBox.Show("Error occurred while UN-Assign Read & Write Access Rights", "Error");
            }
            finally
            {
                dtgRights.ResumeLayout();
            }
        }

        private void readToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String MSG = null;
            bool IsAnyReadExist = false;
            bool IsAnyNotReadExist = false;
            try
            {
                dtgRights.SuspendLayout();

                IsAnyReadExist = CurrentRights.Exists((X) => X.Read);
                IsAnyNotReadExist = CurrentRights.Exists((X) => !X.Read);

                foreach (var currentAccessRight in CurrentRights)
                {
                    if (currentAccessRight != null)
                    {
                        if (IsAnyNotReadExist && IsAnyReadExist)
                        {
                            currentAccessRight.Read = true;
                            MSG = String.Format("Read Access Rights Assigned Some");
                        }
                        else if (IsAnyNotReadExist && !IsAnyReadExist)
                        {
                            currentAccessRight.Read = true;
                            MSG = String.Format("Read Access Rights Assigned All");
                        }
                        else if (!IsAnyNotReadExist && IsAnyReadExist)
                        {
                            currentAccessRight.Read = false;
                            MSG = String.Format("Read Access Rights UN-Assigned All");
                        }
                        else
                        {
                            currentAccessRight.Read = false;
                            MSG = String.Format("Read Access Rights UN-Assigned All");
                        }
                    }
                }
                dtgRights.DataSource = null;
                dtgRights.DataSource = CurrentRights;
                MessageBox.Show(MSG, String.Format("{0}-Read Access Rights", CurrentNode));
            }
            catch
            {
                MessageBox.Show("Error occurred while Assign Write Access Rights", "Error");
            }
            finally
            {
                dtgRights.ResumeLayout();
            }
        }

        #endregion

        private void dtgRights_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            try
            {
                if (IsMsnRangeSelected)
                {
                    bool IsMsnValid = true;
                    sAccessRights sCurret_Right = null;
                    if (sCurrentRights != null)
                        sCurret_Right = sCurrentRights[e.RowIndex];

                    if (sCurret_Right != null)
                    {
                        if (sCurret_Right.QuantityName.Contains(MsnRange.CompanyCode.ToString()) || sCurret_Right.QuantityName.Contains(MsnRange.MeterType.ToString()))
                        {
                            try
                            {
                                byte start = Convert.ToByte(sCurret_Right.Start.Trim());
                            }
                            catch (Exception)
                            {
                                Row_Error_MSG_MSN = "START: Range 0 to 41";
                                IsMsnValid = false;
                            }
                            try
                            {
                                byte end = Convert.ToByte(sCurret_Right.End.Trim());
                            }
                            catch (Exception)
                            {
                                Row_Error_MSG_MSN = "END: Range 0 to 41";
                                IsMsnValid = false;
                            }
                        }
                        else if (sCurret_Right.QuantityName.Contains(MsnRange.MeterSerial.ToString()))
                        {
                            try
                            {
                                //uint startTest = 999999;
                                if (!((Convert.ToUInt32(sCurret_Right.Start)) <= 999999))
                                {
                                    Row_Error_MSG_MSN = "START serial: Length should be 6  (i.e: 123456)";
                                    IsMsnValid = false;
                                }
                            }
                            catch
                            {
                                Row_Error_MSG_MSN = "START serial: Value should be Numeric (Range: 000000 - 999999)";
                                IsMsnValid = false;
                            }
                            try
                            {
                                //uint endTest = 0;
                                if (!((Convert.ToUInt32(sCurret_Right.End)) <= 999999))
                                {
                                    Row_Error_MSG_MSN = "END serial: Length should be 6  (i.e: 123456)";
                                    IsMsnValid = false;
                                }
                            }
                            catch
                            {
                                Row_Error_MSG_MSN = "END serial: Value should be Numeric (Range: 000000 - 999999)";
                                IsMsnValid = false;
                            }

                        }

                        if (!IsMsnValid)
                        {
                            btnSaveChanges.Enabled = false;
                            dtgRights.Rows[e.RowIndex].ErrorText = Row_Error_MSG_MSN;
                        }
                        else
                        {
                            btnSaveChanges.Enabled = true;
                            dtgRights.Rows[e.RowIndex].ErrorText = String.Empty;
                        }
                    }
                    else
                    {
                        dtgRights.Rows[e.RowIndex].ErrorText = String.Empty;
                    }
                }
                else
                {
                    AccessRights Curret_Right = null;
                    if (CurrentRights != null)
                        Curret_Right = CurrentRights[e.RowIndex];

                    if (Curret_Right != null && Curret_Right.Write && !Curret_Right.Read)
                    {
                        dtgRights.Rows[e.RowIndex].ErrorText = Row_Error_MSG1;
                    }
                    else
                    {
                        dtgRights.Rows[e.RowIndex].ErrorText = String.Empty;
                    }
                }
            }
            catch { }



            ////////////////////////////////////////////////
            // Furqan Orignal Code
            //AccessRights Curret_Right = null;
            //try
            //{
            //    if (CurrentRights != null)
            //        Curret_Right = CurrentRights[e.RowIndex];
            //    if (Curret_Right != null &&
            //        Curret_Right.Write && !Curret_Right.Read)
            //    {
            //        dtgRights.Rows[e.RowIndex].ErrorText = Row_Error_MSG1;
            //    }
            //    else
            //        dtgRights.Rows[e.RowIndex].ErrorText = String.Empty;
            //}
            //catch { }
            ////////////////////////////////////////////////////
        }

        private void ApplicationUserRights_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                trvQuantities.ImageList.Dispose();
                trvQuantities.Dispose();

                dtgRights.Dispose();
            }
            catch
            {

            }
        }

        private void MeterDevice_CheckedChanged(object sender, EventArgs e)
        {
                flpAssociationSettingFusion.Visible = rbFusion.Checked;
                flpAssociationSettingRfp.Visible = rbRFP135.Checked;
        }

        private void ApplicationUserRights_Paint(object sender, PaintEventArgs e)
        {
            // Getting the graphics object
            Graphics g = e.Graphics;
            // Creating the rectangle for the gradient
            Rectangle rBackground = new Rectangle(0, 0, this.Width, this.Height);

            // Creating the lineargradient
            System.Drawing.Drawing2D.LinearGradientBrush bBackground
                = new System.Drawing.Drawing2D.LinearGradientBrush(rBackground, LocalCommon.BgColor1, LocalCommon.BgColor2, LocalCommon.BgColorAngle);
            // Draw the gradient onto the form
            g.FillRectangle(bBackground, rBackground);

            // Disposing of the resources held by the brush
            bBackground.Dispose();
        }
    }
}
