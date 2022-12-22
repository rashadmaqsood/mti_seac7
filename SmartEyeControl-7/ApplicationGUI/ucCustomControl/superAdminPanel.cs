using AccurateOptocomSoftware.ApplicationGUI.GUI;
using AccurateOptocomSoftware.ApplicationGUI.ucCustomControl;
using SharedCode.Comm.HelperClasses;
using SharedCode.Common;
using SmartEyeControl_7.DB;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SmartEyeControl_7
{
    public partial class superAdminPanel : UserControl
    {
        DataBaseController dbController;
        User obj_user;
        private List<ApplicationRight_LT> dataAccessGroup = null;
        private Dictionary<string, int> MeterModelList = new Dictionary<string, int>();


        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public User Current_User { get; set; }

        public static String NewUser = "New User";
        public static String CreateUser = "Create User";

        public superAdminPanel()
        {
            InitializeComponent();
            dbController = new DataBaseController();
            obj_user = new User();
            Current_User = new User();
            //commented in v4.8.29
            //MeterModelList = dbController.LoadMeterModels(); //v4.8.15
        }

        public void showToGUI_user(User obj)
        {

            try
            {
                this.SuspendLayout();
                fLP_Main.SuspendLayout();
                pnl_Create_Edit.SuspendLayout();

                if (obj != null)
                {
                    #region //Apply Show & Hide Logic Based On User Privileges

                    Update_dtgAccessGroups();

                    if (obj.userTypeID == UserTypeID.SuperAdmin ||
                        obj.userTypeID == UserTypeID.Admin ||
                        obj.userTypeID == UserTypeID.Custom)
                    {
                        if (Current_User != null
                            && obj.userID != Current_User.userID &&
                            !(Current_User.userTypeID == UserTypeID.SuperAdmin 
                            || Current_User.userTypeID == UserTypeID.Admin))
                        {
                            gp_UserInfo.Visible = gp_UserInfo.Enabled = false;
                            //gpUserRights.Visible = gpUserRights.Enabled = false;
                            gp_AccessRights.Visible = gp_AccessRights.Enabled = false;
                            //gp_AssignRights.Visible = gp_AssignRights.Enabled = false;

                            btn_addUser.Visible = btn_addUser.Enabled = false;
                            btn_editUser.Visible = btn_editUser.Enabled = false;
                            btn_deleteUser.Visible = btn_deleteUser.Enabled = false;
                        }
                        //Settings For SuperAdmin View
                        else if (Current_User != null
                            && Current_User.userTypeID == UserTypeID.SuperAdmin)
                        {
                            gp_UserInfo.Visible = gp_UserInfo.Enabled = true;
                            //gpUserRights.Visible = gpUserRights.Enabled = true;
                            gp_AccessRights.Visible = gp_AccessRights.Enabled = true;
                            //gp_AssignRights.Visible = gp_AssignRights.Enabled = true;
                            
                            btn_addUser.Visible = btn_addUser.Enabled = true;
                            btn_editUser.Visible = btn_editUser.Enabled = true;
                            btn_deleteUser.Visible = btn_deleteUser.Enabled = true;
                        }
                        //Settings For Administrator View
                        else if (Current_User != null &&
                        Current_User.userTypeID == UserTypeID.Admin)
                        {
                            gp_UserInfo.Visible = gp_UserInfo.Enabled = true;
                            //gpUserRights.Visible = gpUserRights.Enabled = true;
                            gp_AccessRights.Visible = gp_AccessRights.Enabled = true;
                            //gp_AssignRights.Visible = gp_AssignRights.Enabled = false;

                            btn_addUser.Visible = btn_addUser.Enabled = false;
                            btn_editUser.Visible = btn_editUser.Enabled = false;
                            btn_deleteUser.Visible = btn_deleteUser.Enabled = false;
                        }
                    }
                    else if (obj.userTypeID == UserTypeID.Inspector ||
                        obj.userTypeID == UserTypeID.Reader ||
                        obj.userTypeID == UserTypeID.Custom)
                    {
                        if (Current_User != null &&
                           !(Current_User.userTypeID == UserTypeID.SuperAdmin ||
                             Current_User.userTypeID == UserTypeID.Admin))
                        {
                            gp_UserInfo.Visible = gp_UserInfo.Enabled = false;
                            //gpUserRights.Visible = gpUserRights.Enabled = false;
                            //gp_AssignRights.Visible = gp_AssignRights.Enabled = false;
                            gp_AccessRights.Visible = gp_AccessRights.Enabled = false;

                            btn_addUser.Visible = btn_addUser.Enabled = false;
                            btn_editUser.Visible = btn_editUser.Enabled = false;
                            btn_deleteUser.Visible = btn_deleteUser.Enabled = false;
                        }
                        //Settings For SuperAdmin UserType
                        else if (Current_User != null &&
                           Current_User.userTypeID == UserTypeID.SuperAdmin)
                        {
                            gp_UserInfo.Visible = gp_UserInfo.Enabled = true;
                            //gpUserRights.Visible = gpUserRights.Enabled = true;
                            //gp_AssignRights.Visible = gp_AssignRights.Enabled = true;
                            gp_AccessRights.Visible = gp_AccessRights.Enabled = true;

                            btn_addUser.Visible = btn_addUser.Enabled = true;
                            btn_editUser.Visible = btn_editUser.Enabled = true;
                            btn_deleteUser.Visible = btn_deleteUser.Enabled = true;
                        }
                        //Settings For Administrator UserType
                        else if (Current_User != null &&
                           Current_User.userTypeID == UserTypeID.Admin)
                        {
                            gp_UserInfo.Visible = gp_UserInfo.Enabled = true;
                            //gpUserRights.Visible = gpUserRights.Enabled = true;
                            gp_AccessRights.Visible = gp_AccessRights.Enabled = true;
                            //gp_AssignRights.Visible = gp_AssignRights.Enabled = false;

                            btn_addUser.Visible = btn_addUser.Enabled = false;
                            btn_editUser.Visible = btn_editUser.Enabled = true;
                            btn_deleteUser.Visible = btn_deleteUser.Enabled = false;
                        }
                    }

                    #endregion

                    txt_userName.Enabled = combo_userType.Enabled = txt_NID.Enabled = false;

                    chk_Activate.Checked = obj.isActive;
                    txt_userName.Text = obj.userName;
                    
                    //v4.8.15.
                    txt_userPassword.Text = User.GetDefaultUser().userPassword; // "AccAdmin";//obj.userPassword; default password
                    txt_userPasswordConfirm.Text = txt_userPassword.Text; //v4.8.15
                    txt_userPassword.Focus();

                    var UserTypeId = (UserTypeID)(byte)(obj.userTypeID);
                    combo_userType.SelectedItem = UserTypeId.ToString();
                    txt_fatherName.Text = obj.fatherName;
                    txt_address.Text = obj.address;
                    txt_employeeCode.Text = obj.employee_code;
                    txt_phone1.Text = obj.phone_1;
                    txt_phone2.Text = obj.phone_2;
                    txt_mobile.Text = obj.mobile_no;
                    txt_faxNumber.Text = obj.fax_no;
                    txt_NID.Text = obj.nid_no;

                    //check_billing.Checked = obj.rights.billingData;
                    //check_instantaneous.Checked = obj.rights.instantaneousData;
                    //check_loadProfile.Checked = obj.rights.loadProfileData;
                    //check_events.Checked = obj.rights.eventsData;
                    //check_Param.Checked = obj.rights.parameters;
                    //check_TcpAccess.Checked = obj.rights.tcp_access;
                    //check_HDLCAccess.Checked = obj.rights.hdlc_access;

                    Update_DataAccessGroups();
                    //Show Access Rights Assigned To Current User
                    //Select_UserAccessRightGroup(obj.RightsID_ACT34G, cmbAccessRightsAct34G);
                    //Select_UserAccessRightGroup(obj.RightsID_T421, cmbAccessRightsT421);
                    //Select_UserAccessRightGroup(obj.RightsID_R421, cmbAccessRightsR421);
                    //Select_UserAccessRightGroup(obj.RightsID_R411, cmbAccessRightsR411);
                    //Select_UserAccessRightGroup(obj.RightsID_R283, cmbAccessRightsR283);

                    foreach (ucMeterModelAccessRights cntrl in fLP_AccessRights_Main.Controls)
                    {

                        var RightsId = (from ApplicationRight abc in obj.AccessRights_Model
                                                              where cntrl.lblModel.Text == abc.MeterModel
                                                              select abc.RightsId).ToList<int>();

                        if (RightsId != null && RightsId.Count > 0)
                            Select_UserAccessRightGroup(RightsId[0], cntrl.cmbAccessRights);
                        else
                            cntrl.cmbAccessRights.SelectedIndex = 0;
                    }
                }
                else
                {
                    clearGUI();
                    MessageBox.Show("User does not exist!");
                }
            }
            catch (Exception ex)
            {
                Notification nt_Error = new Notification("Error Display User Details", "Unable to display User details "
                    + ex.Message, 5000);
            }
            finally
            {
                pnl_Create_Edit.ResumeLayout();
                fLP_Main.ResumeLayout();
                this.ResumeLayout();
            }
        }

        private void Select_UserAccessRightGroup(int RightsID, ComboBox cmbAccessRights)
        {
            try
            {
                if (dataAccessGroup == null || dataAccessGroup.Count <= 0)
                {
                    cmbAccessRights.SelectedItem = null;
                    return;
                }
                var results = (from ApplicationRight_LT myRow in dataAccessGroup
                               where myRow.ID == RightsID
                               select myRow).ToList<ApplicationRight_LT>();
                if (results != null && results.Count > 0)
                    cmbAccessRights.SelectedItem = results[0];
                else
                    cmbAccessRights.SelectedItem = null;
            }
            catch { }
        }

        private void clearGUI()
        {
            txt_userName.Text = string.Empty;
            txt_userPassword.Text = string.Empty;
            combo_userType.SelectedIndex = -1;
            //check_billing.Checked = false;
            //check_instantaneous.Checked = false;
            //check_loadProfile.Checked = false;
            //check_events.Checked = false;
            //check_Param.Checked = false;
            //check_TcpAccess.Checked = false;
            //check_HDLCAccess.Checked = false;
        }

        private void superAdminPanel_Load(object sender, EventArgs e)
        {
            try
            {
                if (dbController != null) // && dbController.IsConnectionOpen)
                {
                    MeterModelList = dbController.LoadMeterModels(); //v4.8.29
                    Load_AllUsers();
                    LoadMeterModels(); //v4.8.15
                    Update_dtgAccessGroups();
                    Update_DataAccessGroups(); 
                }
            }
            catch (Exception ex)
            {
                Notification nt_Error = new Notification("Error Loading/Show User Management", "Error Loading User Management "
                    + ex.Message, 5000);
            }
        }

        private void Update_dtgAccessGroups()
        {
            try
            {
                //Verify Either Current User Is Super Administrator
                if (Current_User != null && Current_User.userID != 0)
                {
                    if (Current_User.userTypeID == UserTypeID.SuperAdmin)
                    {
                        dtgAccessGroups.Visible = dtgAccessGroups.Enabled = true;

                        dtgAccessGroups.AutoGenerateColumns = false;
                        dtgAccessGroups.DataSource = null;
                        dtgAccessGroups.DataSource = dbController.SelectAccessRights();
                    }
                    else
                    {
                        dtgAccessGroups.Visible = dtgAccessGroups.Enabled = false;
                        dtgAccessGroups.DataSource = null;
                    }
                }
                else
                {
                    dtgAccessGroups.Visible = dtgAccessGroups.Enabled = false;

                    dtgAccessGroups.AutoGenerateColumns = false;
                    dtgAccessGroups.DataSource = null;
                    dtgAccessGroups.DataSource = dbController.SelectAccessRights();
                }
            }
            catch (Exception ex) { throw new Exception("Error occurred while loading Access Group Rights", ex); }
        }

        private bool LoadMeterModels()
        {
            //List<string> models = dbController.LoadMeterModels();
            try
            {
                for (int i = 0; i < MeterModelList.Count; i++)
                {
                    ucMeterModelAccessRights mmar = new ucMeterModelAccessRights();
                    mmar.lblModel.Text = MeterModelList.Keys.ElementAt(i);
                    fLP_AccessRights_Main.Controls.Add(mmar);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while loading meter model info", ex);
            }
        }

        private void Update_DataAccessGroups()
        {
            try
            {
                var dataAccessGroupDT = dbController.SelectAccessRights();

                dataAccessGroup = new List<ApplicationRight_LT>();

                ApplicationRight_LT Row = new ApplicationRight_LT();
                Row.ID = 0;
                Row.Identifier = "No Access";
                Row.Role = UserTypeID.Custom;
                dataAccessGroup.Add(Row);

                foreach (DataRow row_Dt in dataAccessGroupDT.Rows)
                {
                    Row = new ApplicationRight_LT();

                    Row.ID = Convert.ToInt32(row_Dt["id"]);
                    Row.Identifier = row_Dt["identifier"].ToString();
                    var UserRole = row_Dt["role"].ToString();

                    UserTypeID UserRoleType = UserTypeID.Custom;
                    Enum.TryParse<UserTypeID>(UserRole, out UserRoleType);
                    Row.Role = UserRoleType;

                    //Select Only Access Rights With Current Role
                    if (obj_user != null && obj_user.userTypeID == Row.Role)
                        dataAccessGroup.Add(Row);
                    else if (obj_user == null || obj_user.userID <= 0 || String.IsNullOrEmpty(obj_user.userName))
                        dataAccessGroup.Add(Row);
                }

                //var dtRow = dataAccessGroup.NewRow();
                //Add Manually Row For No Access
                //dtRow["id"] = 0;
                //dtRow["identifier"] = "No Access";
                //dtRow["role"] = UserTypeID.Custom.ToString();

                //ApplicationRight_LT Row = new ApplicationRight_LT();
                //dataAccessGroup.Rows.Add(dtRow);

                //cmbAccessRightsAct34G.DataSource = null;
                //cmbAccessRightsAct34G.DataSource = new List<ApplicationRight_LT>(dataAccessGroup);
                //cmbAccessRightsAct34G.DisplayMember = "identifier";

                //cmbAccessRightsT421.DataSource = null;
                //cmbAccessRightsT421.DataSource = new List<ApplicationRight_LT>(dataAccessGroup);
                //cmbAccessRightsT421.DisplayMember = "identifier";

                //cmbAccessRightsR421.DataSource = null;
                //cmbAccessRightsR421.DataSource = new List<ApplicationRight_LT>(dataAccessGroup);
                //cmbAccessRightsR421.DisplayMember = "identifier";

                //cmbAccessRightsR411.DataSource = null;
                //cmbAccessRightsR411.DataSource = new List<ApplicationRight_LT>(dataAccessGroup);
                //cmbAccessRightsR411.DisplayMember = "identifier";

                //cmbAccessRightsR283.DataSource = null;
                //cmbAccessRightsR283.DataSource = new List<ApplicationRight_LT>(dataAccessGroup);
                //cmbAccessRightsR283.DisplayMember = "identifier";

                foreach (ucMeterModelAccessRights mm in fLP_AccessRights_Main.Controls)
                {
                    mm.cmbAccessRights.DataSource = null;
                    mm.cmbAccessRights.DataSource = new List<ApplicationRight_LT>(dataAccessGroup);
                    mm.cmbAccessRights.DisplayMember = "identifier";
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred while Loading Data Access Group Details," + ex.Message, ex);
            }
            finally 
            {
                //Verify Either Current User Is Super Administrator or any other
                if (Current_User != null && Current_User.userID != 0)
                {
                    if (Current_User.userTypeID == UserTypeID.SuperAdmin)
                    {
                        //gp_AssignRights.Enabled = gp_AssignRights.Visible = true;
                    }
                    else
                    {
                        //gp_AssignRights.Enabled = gp_AssignRights.Visible = false;
                    }
                }
            }
        }

        private void Load_AllUsers()
        {
            var users = dbController.selectAllUsers();

            dataGridView_Users.AutoGenerateColumns = false;
            dataGridView_Users.DataSource = null;
            dataGridView_Users.DataSource = users;
        }

        public void Select_User(User Obj_User)
        {
            try
            {
                //Select Current User 
                if (Obj_User != null && Obj_User.userID != 0 &&
                    !String.IsNullOrEmpty(Obj_User.userName))
                {
                    //Find Obj_User Using User_ID
                    var Dt_users = (from row in dataGridView_Users.Rows.Cast<DataGridViewRow>()
                                    where row.DataBoundItem != null &&
                                          Convert.ToInt32(((DataRowView)row.DataBoundItem)["user_ID"]) == Obj_User.userID
                                    select row);

                    //UN-Select Previous
                    foreach (DataGridViewRow dtRow in dataGridView_Users.Rows)
                    {
                        dtRow.Selected = false;
                    }
                    foreach (var dtRow in Dt_users)
                    {
                        dtRow.Selected = true;
                        dtRow.Cells[0].Selected = true;
                        
                        //if (dtRow.DataBoundItem != null)
                        //{
                        //    var DtRow = ((DataRowView)dtRow.DataBoundItem);
                        //    if (DtRow.Row.RowState != DataRowState.Modified)
                        //        DtRow.Row.SetModified();
                        //}

                    }
                }
            }
            catch (Exception ex) { throw new Exception(String.Format("Error occurred while Select User {0}", Obj_User.userName), ex); }
        }

        private void Load_UserInfo(int userId, string UserName)
        {
            try
            {
                obj_user = dbController.getUser(UserName);
                //Reset Add New User
                btn_addUser.Text = NewUser;
                showToGUI_user(obj_user);
            }
            catch { throw; }
        }

        #region Add/Edit/Delete User

        private void btn_addUser_Click(object sender, EventArgs e)
        {
            try
            {
                string txt_Button = ((Button)sender).Text;
                //New User Button
                if (String.Equals(txt_Button, NewUser))
                {
                    obj_user = User.GetDefaultUser();

                    foreach (ucMeterModelAccessRights cntrl in fLP_AccessRights_Main.Controls)
                    {
                        cntrl.cmbAccessRights.SelectedIndex = 0;
                    }

                    showToGUI_user(obj_user);

                    txt_userPasswordConfirm.Text = obj_user.userPassword;

                    gp_UserInfo.Visible = true;
                    //gp_AssignRights.Visible = true;
                    //gpUserRights.Visible = true;

                    btn_editUser.Visible = btn_editUser.Enabled = true;
                    btn_deleteUser.Visible = btn_deleteUser.Enabled = true;

                    txt_userName.Enabled = combo_userType.Enabled = txt_NID.Enabled = true;
                    ((Button)sender).Text = CreateUser;

                    txt_userName.Focus();
                    txt_userPassword.Focus();
                    txt_userName.Focus();
                }
                else
                {
                    Type t = typeof(UserTypeID);

                    obj_user.userTypeID = (UserTypeID)(Enum.Parse(t, combo_userType.Text));
                    obj_user.isActive = chk_Activate.Checked;
                    obj_user.userName = txt_userName.Text;
                    obj_user.userPassword = txt_userPassword.Text;
                    obj_user.fatherName = txt_fatherName.Text;
                    obj_user.address = txt_address.Text;
                    obj_user.phone_1 = txt_phone1.Text;
                    obj_user.phone_2 = txt_phone2.Text;
                    obj_user.mobile_no = txt_mobile.Text;
                    obj_user.fax_no = txt_faxNumber.Text;
                    obj_user.nid_no = txt_NID.Text;
                    obj_user.employee_code = txt_employeeCode.Text;
                    obj_user.creation_date = DateTime.Now;

                    //obj_user.rights.hdlc_access = check_HDLCAccess.Checked;
                    //obj_user.rights.tcp_access = check_TcpAccess.Checked;
                    //obj_user.rights.parameters = check_Param.Checked;
                    //obj_user.rights.instantaneousData = check_instantaneous.Checked;
                    //obj_user.rights.loadProfileData = check_loadProfile.Checked;
                    //obj_user.rights.billingData = check_billing.Checked;
                    //obj_user.rights.eventsData = check_events.Checked;

                    //obj_user.RightsID_T421 = ((ApplicationRight_LT)cmbAccessRightsT421.SelectedItem).ID;
                    //obj_user.RightsID_R421 = ((ApplicationRight_LT)cmbAccessRightsR421.SelectedItem).ID;
                    //obj_user.RightsID_R411 = ((ApplicationRight_LT)cmbAccessRightsR411.SelectedItem).ID;
                    //obj_user.RightsID_ACT34G = ((ApplicationRight_LT)cmbAccessRightsAct34G.SelectedItem).ID;
                    //obj_user.RightsID_R283 = ((ApplicationRight_LT)cmbAccessRightsR283.SelectedItem).ID;

                    //if (!obj_user.rights.tcp_access && !obj_user.rights.hdlc_access)
                    //{
                    //    obj_user.rights.hdlc_access = true;
                    //}

                    foreach (ucMeterModelAccessRights ar in fLP_AccessRights_Main.Controls)
                    {
                        if (ar.cmbAccessRights.SelectedIndex > 0)
                        {
                            obj_user.RightsID_Model.Add(MeterModelList[ar.lblModel.Text], ((ApplicationRight_LT)ar.cmbAccessRights.SelectedItem).ID);
                        }
                    }

                    if (dbController.addNewUser(obj_user))
                    {
                        Load_AllUsers();
                        Select_User(obj_user);
                        Notification Notify_Msg = new Notification("Success", "New User Added Successfully", 5000);
                    }
                    else
                    {
                        Select_User(Current_User);
                        Notification nt_Error = new Notification("Error", "Unable to Add User", 5000);
                    }
                    ((Button)sender).Text = NewUser;
                }
            }
            catch (Exception ex)
            {
                Notification nt_Error = new Notification("Error Add New User", "Unable to Add new User" + ex.Message, 5000);
            }
        }

        private void btn_editUser_Click(object sender, EventArgs e)
        {
            try
            {
                Type t = typeof(UserTypeID);

                //obj_user.userTypeID = (UserTypeID)(Enum.Parse(t, combo_userType.Text));
                //obj_user.userName = txt_userName.Text;
                obj_user.isActive = chk_Activate.Checked;
                obj_user.userPassword = txt_userPassword.Text;
                obj_user.fatherName = txt_fatherName.Text;
                obj_user.address = txt_address.Text;
                obj_user.phone_1 = txt_phone1.Text;
                obj_user.phone_2 = txt_phone2.Text;
                obj_user.mobile_no = txt_mobile.Text;
                obj_user.fax_no = txt_faxNumber.Text;
                obj_user.nid_no = txt_NID.Text;//
                obj_user.employee_code = txt_employeeCode.Text;
                obj_user.creation_date = DateTime.Now;

                //obj_user.rights.hdlc_access = check_HDLCAccess.Checked;
                //obj_user.rights.tcp_access = check_TcpAccess.Checked;
                //obj_user.rights.parameters = check_Param.Checked;
                //obj_user.rights.instantaneousData = check_instantaneous.Checked;
                //obj_user.rights.loadProfileData = check_loadProfile.Checked;
                //obj_user.rights.billingData = check_billing.Checked;
                //obj_user.rights.eventsData = check_events.Checked;

                //obj_user.RightsID_T421 = ((ApplicationRight_LT)cmbAccessRightsT421.SelectedItem).ID;
                //obj_user.RightsID_R421 = ((ApplicationRight_LT)cmbAccessRightsR421.SelectedItem).ID;
                //obj_user.RightsID_R411 = ((ApplicationRight_LT)cmbAccessRightsR411.SelectedItem).ID;
                //obj_user.RightsID_ACT34G = ((ApplicationRight_LT)cmbAccessRightsAct34G.SelectedItem).ID;
                //obj_user.RightsID_R283 = ((ApplicationRight_LT)cmbAccessRightsR283.SelectedItem).ID;

                //if (!obj_user.rights.tcp_access && !obj_user.rights.hdlc_access)
                //{
                //    obj_user.rights.hdlc_access = true;
                //}
                obj_user.RightsID_Model.Clear();

                foreach (ucMeterModelAccessRights cntrl in fLP_AccessRights_Main.Controls)
                {
                    if (cntrl.cmbAccessRights.SelectedIndex > 0)
                    {
                        obj_user.RightsID_Model.Add(MeterModelList[cntrl.lblModel.Text], ((ApplicationRight_LT)cntrl.cmbAccessRights.SelectedItem).ID);
                    }
                    //else
                    //{
                    //    int meterModelId = MeterModelList[cntrl.lblModel.Text];
                    //    obj_user.RightsID_Model.Remove(meterModelId);
                    //}

                    //if (RightsId != null && RightsId.Count > 0)
                    //    Select_UserAccessRightGroup(RightsId[0], cntrl.cmbAccessRights);
                    //else
                    //    cntrl.cmbAccessRights.SelectedIndex = 0;
                }


                if (dbController.updateExistUser(obj_user))
                {
                    Notification Notify_Msg = new Notification("Success", "Selected User Updated Successfully", 5000);
                }
                else
                {
                    Notification nt_Error = new Notification("Error", "Unable to Update Existing User", 5000);
                }
            }
            catch (Exception ex)
            {
                Notification nt_Error = new Notification("Error Edit Existing User", "Unable to Update Existing User" + ex.Message, 5000);
            }
        }

        private void btn_deleteUser_Click(object sender, EventArgs e)
        {
            Notification nt_Error = null;
            Notification Notify_Msg = null;
            try
            {
                if (obj_user != null &&
                    obj_user.userID != 0 &&
                    !String.IsNullOrEmpty(obj_user.userName))
                {
                    if (dbController.deleteuser(obj_user))
                    {
                        Load_AllUsers();
                        Select_User(Current_User);
                        showToGUI_user(Current_User);
                        Notify_Msg = new Notification("Success", "Selected User Deleted Successfully", 5000);
                    }
                    else
                    {
                        nt_Error = new Notification("Error", "Unable to Delete Existing User", 5000);
                    }
                }
                else
                    nt_Error = new Notification("Select User", "Please Select an User to Delete", 5000);
            }
            catch (Exception ex)
            {
                nt_Error = new Notification("Error Edit Existing User", "Unable to Update Existing User" + ex.Message, 5000);
            }
        }

        #endregion

        #region Context Menu Strip For Access Right Grid

        private void dtgAccessGroups_CellContextMenuStripNeeded(object sender,
            DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex >= -0)
            {
                e.ContextMenuStrip = GetContextStripForAccessGrid(e.RowIndex);
            }
        }

        private ContextMenuStrip GetContextStripForAccessGrid(int row_Index)
        {
            try
            {
                ContextMenuStrip strip = new ContextMenuStrip();
                var Add = new ToolStripMenuItem();
                Add.Text = "Add New Rights";
                Add.Click += new EventHandler(Add_Click);
                //Add.Image = Image.FromFile(Directory.GetCurrentDirectory()+@"/images/icons/addStrip.png");
                Add.Tag = row_Index;
                strip.Items.Add(Add);

                var Edit = new ToolStripMenuItem();
                Edit.Text = "Edit Access Rights";
                Edit.Click += new EventHandler(Edit_Click);
                //Edit.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"/images/icons/EditStrip.png");
                Edit.Tag = row_Index;
                strip.Items.Add(Edit);

                var delete = new ToolStripMenuItem();
                delete.Text = "Delete Access rights";
                delete.Click += new EventHandler(delete_Click);
                //delete.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"/images/icons/deleteStrip.png");
                delete.Tag = row_Index;
                strip.Items.Add(delete);

                return strip;

            }
            catch (Exception)
            {
                throw;
            }
        }

        void delete_Click(object sender, EventArgs e)
        {
            try
            {
                var rowid = Convert.ToInt16((sender as ToolStripMenuItem).Tag);

                var DataRow = (DataRowView)dtgAccessGroups.Rows[rowid].DataBoundItem;
                var id = (int)(DataRow.Row["id"]);
                var ident = (DataRow.Row["identifier"]).ToString();

                if (MessageBox.Show("Are You Sure Want To Delete \"" + ident + "\" Rights?", "Delete Rights",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    if (dbController.DeleteAccessRights(id))
                    {
                        Update_dtgAccessGroups();
                        Update_DataAccessGroups();
                    }
                }
            }
            catch (Exception ex)
            {
                Notification nt_Error = new Notification("Error Delete Data Access Groups", "Unable to Delete Data Access Group" + ex.Message, 5000);
            }
        }

        void Edit_Click(object sender, EventArgs e)
        {
            try
            {
                var rowid = Convert.ToInt16((sender as ToolStripMenuItem).Tag);
                var DataRow = (DataRowView)dtgAccessGroups.Rows[rowid].DataBoundItem;
                var dataArray = (byte[])(DataRow.Row["rights"]);

                var pkg = ApplicationRight.GetObjectFromBytes(dataArray);
                var id = Convert.ToInt32(dtgAccessGroups["id", rowid].Value.ToString());
                var identifier = dtgAccessGroups["identifier", rowid].Value.ToString();
                var role = dtgAccessGroups["role", rowid].Value.ToString();

                var frm = new ApplicationUserRights(pkg);
                frm.Identifier = identifier;
                frm.Role = role;
                frm.StartPosition = FormStartPosition.CenterParent;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    dbController.UpdateAccessRights(id, frm.Identifier, frm.Role, frm.UserRigts);
                    Update_DataAccessGroups();
                }
            }
            catch (Exception ex)
            {
                Notification nt_Error = new Notification("Error Edit Data Access Groups", "Unable to Edit Data Access Group" + ex.Message, 5000);
            }
        }

        void Add_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new ApplicationUserRights(null);
                frm.StartPosition = FormStartPosition.CenterParent;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (dbController.CreateNewAccessRights(frm.Identifier, frm.Role, frm.UserRigts))
                    {
                        Update_dtgAccessGroups();
                        Update_DataAccessGroups();
                    }
                }
            }
            catch (Exception ex)
            {
                Notification nt_Error = new Notification("Error Add Data Access Groups", "Unable to Add Data Access Group" + ex.Message, 5000);
            }
        }

        private void dtgAccessGroups_Click(object sender, EventArgs e)
        {
            if (dtgAccessGroups.RowCount == 0)
            {
                dtgAccessGroups.ContextMenuStrip = initialMenuStrip;
            }
            else
            {
                dtgAccessGroups.ContextMenuStrip = null;
            }
        }

        #endregion

        private void dataGridView_Users_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            String UserType = "Invalid";
            DataGridViewRow row = null;
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex == 2)
                {
                    row = dataGridView_Users.Rows[e.RowIndex];
                    String user_type_idStr = null;
                    user_type_idStr = (string)((DataRowView)row.DataBoundItem)["user_type_id"];
                    var user_type_id = Convert.ToInt32(user_type_idStr);
                    UserType = ((UserTypeID)user_type_id).ToString();
                }
            }
            catch { }
            finally
            {
                try
                {
                    if (row != null)
                    {
                        e.Value = UserType.ToString();
                    }
                }
                catch
                { }
            }
        }

        private void dataGridView_Users_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = null;
            String user_idStr = null;
            try
            {
                row = dataGridView_Users.Rows[e.RowIndex];

                user_idStr = (String)((DataRowView)row.DataBoundItem)["user_ID"].ToString();
                var user_id = Convert.ToInt32(user_idStr);
                user_idStr = (string)((DataRowView)row.DataBoundItem)["user_name"];

                Load_UserInfo(user_id, user_idStr);
                Validate_UserInfo();
            }
            catch (Exception ex) { }
            finally
            {
            }
        }

        private void combo_userType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (CreateUser.Equals(btn_addUser.Text, StringComparison.OrdinalIgnoreCase))
                {
                    Type t = typeof(UserTypeID);
                    obj_user.userTypeID = (UserTypeID)(Enum.Parse(t, combo_userType.Text));
                    Update_DataAccessGroups();
                }
            }
            catch (Exception ex)
            {
                Notification nt_Error = new Notification("Error Loading Data AccessGroups", "Error Loading Data AccessGroups"
                    + ex.Message, 5000);
            }
        }

        #region Validate_User

        private bool Validate_UserName(User Current_User, Control Validating_Contrl, ErrorProvider errorProvider)
        {
            bool isValidated = false;
            String val = String.Empty;
            String ErrorMessage = String.Empty;
            try
            {
                //Clear Previous Error
                App_Validation.Apply_ValidationResult(true, String.Empty, Validating_Contrl, errorProvider);

                val = Validating_Contrl.Text;
                //Not empty null value
                isValidated = !String.IsNullOrEmpty(val);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Error Require User Name");
                }
                //User Already Exist
                if (isValidated)
                {
                    DataTable _DataTable = (DataTable)dataGridView_Users.DataSource;

                    var Record =
                    (from DataRow rw in _DataTable.Rows
                     where rw != null && String.Equals(val, rw["user_Name"].ToString())
                     select rw).Count<DataRow>();

                    isValidated = !(Record > 0);
                    if (!isValidated)
                    {
                        ErrorMessage = String.Format("Error User {0} already exist", val);
                    }
                }
                //User Name Length
                if (isValidated)
                {
                    isValidated = App_Validation.Validate_TxtLength_WithPaddingChar(05, 25, val, "  \r");
                    if (!isValidated)
                    {
                        ErrorMessage = String.Format("Error Range Validating User Name:{0}", val);
                    }
                }
                if (isValidated)
                {
                    App_Validation.Apply_ValidationResult(isValidated, String.Empty, Validating_Contrl, errorProvider);
                }
                else
                    App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, Validating_Contrl, errorProvider);
            }
            catch (Exception ex)
            {
                App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, Validating_Contrl, errorProvider);
                Notification notifier = new Notification("Validation Error", ex.Message, 1000, Notification.Sounds.Beep);
            }
            return isValidated;
        }

        private static bool Validate_Password(User Current_User, Control Validating_Contrl, ErrorProvider errorProvider)
        {
            bool isValidated = false;
            String val = String.Empty;
            String ErrorMessage = String.Empty;
            try
            {
                //Clear Previous Error
                App_Validation.Apply_ValidationResult(true, String.Empty, Validating_Contrl, errorProvider);

                val = Validating_Contrl.Text;
                //Non empty null value
                isValidated = !String.IsNullOrEmpty(val);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Error Require Password");
                }
                //Password
                if (isValidated)
                {
                    isValidated = App_Validation.Validate_TxtLength_WithPaddingChar(06, 25, val, "  \r");
                    if (!isValidated)
                    {
                        ErrorMessage = String.Format("Error Invalid Password Length [06-25]");
                    }
                }

                if (isValidated)
                {
                    App_Validation.Apply_ValidationResult(isValidated, String.Empty, Validating_Contrl, errorProvider);
                }
                else
                    App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, Validating_Contrl, errorProvider);
            }
            catch (Exception ex)
            {
                App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, Validating_Contrl, errorProvider);
                Notification notifier = new Notification("Validation Error", ex.Message, 1000, Notification.Sounds.Beep);
            }
            return isValidated;
        }

        private static bool Validate_FatherName(User Current_User, Control Validating_Contrl, ErrorProvider errorProvider)
        {
            bool isValidated = false;
            String val = String.Empty;
            String ErrorMessage = String.Empty;
            try
            {
                //Clear Previous Error
                App_Validation.Apply_ValidationResult(true, String.Empty, Validating_Contrl, errorProvider);

                val = Validating_Contrl.Text;
                //Non empty null value
                isValidated = String.IsNullOrEmpty(val);
                if (!String.IsNullOrEmpty(val))
                {
                    isValidated = App_Validation.Validate_TxtLength_WithPaddingChar(06, 25, val, "  \r");
                    if (!isValidated)
                    {
                        ErrorMessage = String.Format("Error Invalid Father Name Length [06-25]");
                    }
                }
                //Password
                if (isValidated)
                {
                    App_Validation.Apply_ValidationResult(isValidated, String.Empty, Validating_Contrl, errorProvider);
                }
                else
                    App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, Validating_Contrl, errorProvider);
            }
            catch (Exception ex)
            {
                App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, Validating_Contrl, errorProvider);
                Notification notifier = new Notification("Validation Error", ex.Message, 1000, Notification.Sounds.Beep);
            }
            return isValidated;
        }

        private static bool Validate_Address(User Current_User, Control Validating_Contrl, ErrorProvider errorProvider)
        {
            bool isValidated = false;
            String val = String.Empty;
            String ErrorMessage = String.Empty;
            try
            {
                //Clear Previous Error
                App_Validation.Apply_ValidationResult(true, String.Empty, Validating_Contrl, errorProvider);

                val = Validating_Contrl.Text;
                //Non empty null value
                isValidated = String.IsNullOrEmpty(val);
                if (!String.IsNullOrEmpty(val))
                {
                    isValidated = App_Validation.Validate_TxtLength_WithPaddingChar(06, 100, val, "  \r");
                    if (!isValidated)
                    {
                        ErrorMessage = String.Format("Error Invalid Father Name Length [06-100]");
                    }
                }
                //Address
                if (isValidated)
                {
                    App_Validation.Apply_ValidationResult(isValidated, String.Empty, Validating_Contrl, errorProvider);
                }
                else
                    App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, Validating_Contrl, errorProvider);
            }
            catch (Exception ex)
            {
                App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, Validating_Contrl, errorProvider);
                Notification notifier = new Notification("Validation Error", ex.Message, 1000, Notification.Sounds.Beep);
            }
            return isValidated;
        }

        private static bool Validate_EmployeeCode(User Current_User, Control Validating_Contrl, ErrorProvider errorProvider)
        {
            bool isValidated = false;
            String val = String.Empty;
            String ErrorMessage = String.Empty;
            try
            {
                //Clear Previous Error
                App_Validation.Apply_ValidationResult(true, String.Empty, Validating_Contrl, errorProvider);

                val = Validating_Contrl.Text;
                //Non empty null value
                isValidated = !String.IsNullOrEmpty(val);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Error Require Employee Code");
                }
                //Password
                if (isValidated)
                {
                    isValidated = App_Validation.Validate_TxtLength_WithPaddingChar(04, 25, val, "  \r");
                    if (!isValidated)
                    {
                        ErrorMessage = String.Format("Error Invalid Employee Code Length [04-25]");
                    }
                }
                if (isValidated)
                {
                    App_Validation.Apply_ValidationResult(isValidated, String.Empty, Validating_Contrl, errorProvider);
                }
                else
                    App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, Validating_Contrl, errorProvider);
            }
            catch (Exception ex)
            {
                App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, Validating_Contrl, errorProvider);
                Notification notifier = new Notification("Validation Error", ex.Message, 1000, Notification.Sounds.Beep);
            }
            return isValidated;
        }

        private static bool Validate_PhoneNumber(User Current_User, Control Validating_Contrl, ErrorProvider errorProvider)
        {
            bool isValidated = false;
            String val = String.Empty;
            String ErrorMessage = String.Empty;
            try
            {
                //Clear Previous Error
                App_Validation.Apply_ValidationResult(true, String.Empty, Validating_Contrl, errorProvider);

                val = Validating_Contrl.Text;
                //Non empty null value
                isValidated = String.IsNullOrEmpty(val);
                if (!isValidated)
                {
                    isValidated = App_Validation.Validate_IntlPhoneNumberFormat(val);
                    if (!isValidated)
                    {
                        ErrorMessage = String.Format("Error Invalid Phone Number Format ");
                    }
                }
                if (isValidated)
                {
                    App_Validation.Apply_ValidationResult(isValidated, String.Empty, Validating_Contrl, errorProvider);
                }
                else
                    App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, Validating_Contrl, errorProvider);
            }
            catch (Exception ex)
            {
                App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, Validating_Contrl, errorProvider);
                Notification notifier = new Notification("Validation Error", ex.Message, 1000, Notification.Sounds.Beep);
            }
            return isValidated;
        }

        private static bool Validate_NIDNumber(User Current_User, Control Validating_Contrl, ErrorProvider errorProvider)
        {
            bool isValidated = false;
            String val = String.Empty;
            String ErrorMessage = String.Empty;
            try
            {
                //Clear Previous Error
                App_Validation.Apply_ValidationResult(true, String.Empty, Validating_Contrl, errorProvider);

                val = Validating_Contrl.Text;
                //Non empty null value
                isValidated = !String.IsNullOrEmpty(val);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Error Require NID Number");
                }
                if (!isValidated)
                {
                    isValidated = App_Validation.Validate_NIDFormat(val);
                    if (!isValidated)
                    {
                        ErrorMessage = String.Format("Error Invalid NID Format ");
                    }
                }
                if (isValidated)
                {
                    App_Validation.Apply_ValidationResult(isValidated, String.Empty, Validating_Contrl, errorProvider);
                }
                else
                    App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, Validating_Contrl, errorProvider);
            }
            catch (Exception ex)
            {
                App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, Validating_Contrl, errorProvider);
                Notification notifier = new Notification("Validation Error", ex.Message, 1000, Notification.Sounds.Beep);
            }
            return isValidated;
        }

        private void Validate_UserInfo()
        {
            bool isValidated = false;
            try
            {
                //Clear Previous Error
                App_Validation.Apply_ValidationResult(true, String.Empty, txt_userName, errorProvider);

                if (btn_addUser.Text == CreateUser)
                {
                    isValidated = Validate_UserName(obj_user, txt_userName, errorProvider);

                    if (!isValidated)
                        return;
                }

                isValidated = Validate_Password(obj_user, txt_userPassword, errorProvider);

                if (!isValidated)
                    return;


                if (txt_userPassword.Text != txt_userPasswordConfirm.Text)
                {
                    isValidated = false;
                    App_Validation.Apply_ValidationResult(isValidated, "Password and Confirm password don't match", txt_userPasswordConfirm, errorProvider);
                    return;
                }
                else
                {
                    App_Validation.Apply_ValidationResult(isValidated, string.Empty, txt_userPasswordConfirm, errorProvider);
                }

                isValidated = Validate_FatherName(obj_user, txt_fatherName, errorProvider);
                if (!isValidated)
                    btn_addUser.Enabled = false;
                if (!isValidated)
                    return;

                isValidated = Validate_Address(obj_user, txt_address, errorProvider);

                if (!isValidated)
                    return;

                isValidated = Validate_EmployeeCode(obj_user, txt_employeeCode, errorProvider);

                if (!isValidated)
                    return;

                isValidated = Validate_PhoneNumber(obj_user, txt_phone1, errorProvider);

                if (!isValidated)
                    return;

                isValidated = Validate_PhoneNumber(obj_user, txt_phone2, errorProvider);

                if (!isValidated)
                    return;

                isValidated = Validate_PhoneNumber(obj_user, txt_mobile, errorProvider);

                if (!isValidated)
                    return;

                isValidated = Validate_PhoneNumber(obj_user, txt_faxNumber, errorProvider);

                if (isValidated)
                {
                    App_Validation.Apply_ValidationResult(isValidated, String.Empty, txt_faxNumber, errorProvider);
                }
                else
                    App_Validation.Apply_ValidationResult(isValidated, "Invalid Fax Number Format", txt_faxNumber, errorProvider);

                if (!isValidated)
                    return;
                //Clear Previous Error
                App_Validation.Apply_ValidationResult(true, String.Empty, txt_NID, errorProvider);

                if (btn_addUser.Text == CreateUser)
                {
                    isValidated = Validate_NIDNumber(obj_user, txt_NID, errorProvider);
                }
            }
            finally
            {
                if (!isValidated)
                {
                    if (btn_addUser.Text == CreateUser)
                        btn_addUser.Enabled = false;
                    else
                        btn_editUser.Enabled = false;
                }
                else
                {
                    if (!btn_addUser.Enabled)
                        btn_addUser.Enabled = true;
                    if (!btn_editUser.Enabled)
                        btn_editUser.Enabled = true;
                }
            }
        }

        #endregion

        #region User_Info_Leave_Envet

        #region //Validate User_Name

        private void txt_userName_Leave(object sender, EventArgs e)
        {
            bool isValidated = Validate_UserName(obj_user, txt_userName, errorProvider);
            if (!isValidated)
            {
                if (btn_addUser.Text == CreateUser)
                    btn_addUser.Enabled = false;
                else
                    btn_editUser.Enabled = false;
            }
        }

        private void txt_userName_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool isValidated = Validate_UserName(obj_user, txt_userName, errorProvider);
            if (!isValidated)
            {
                if (btn_addUser.Text == CreateUser)
                    btn_addUser.Enabled = false;
                else
                    btn_editUser.Enabled = false;
            }
        }

        #endregion

        private void txt_userPassword_Leave(object sender, EventArgs e)
        {
            bool isValidated = Validate_Password(obj_user, txt_userPassword, errorProvider);

            if (isValidated)
            {
                isValidated = Validate_Password(obj_user, txt_userPasswordConfirm, errorProvider);
                if (!isValidated)
                {
                    if (btn_addUser.Text == CreateUser)
                        btn_addUser.Enabled = false;
                    else
                        btn_editUser.Enabled = false;
                }
            }
            else
            {
                if (!isValidated)
                {
                    if (btn_addUser.Text == CreateUser)
                        btn_addUser.Enabled = false;
                    else
                        btn_editUser.Enabled = false;
                }
            }

            if (txt_userPassword.Text != txt_userPasswordConfirm.Text)
            {
                isValidated = false;

                if (btn_addUser.Text == CreateUser)
                    btn_addUser.Enabled = false;
                else
                    btn_editUser.Enabled = false;
                App_Validation.Apply_ValidationResult(isValidated, "Password and Confirm password don't match", txt_userPasswordConfirm, errorProvider);
            }
            else
            {
                App_Validation.Apply_ValidationResult(isValidated, string.Empty, txt_userPasswordConfirm, errorProvider);
            }
        }

        private void txt_fatherName_Leave(object sender, EventArgs e)
        {
            bool isValidated = Validate_FatherName(obj_user, txt_fatherName, errorProvider);
            if (!isValidated)
            {
                if (btn_addUser.Text == CreateUser)
                    btn_addUser.Enabled = false;
                else
                    btn_editUser.Enabled = false;
            }
        }

        private void txt_address_Leave(object sender, EventArgs e)
        {
            bool isValidated = Validate_Address(obj_user, txt_address, errorProvider);
            if (!isValidated)
            {
                if (btn_addUser.Text == CreateUser)
                    btn_addUser.Enabled = false;
                else
                    btn_editUser.Enabled = false;
            }
        }

        private void txt_employeeCode_Leave(object sender, EventArgs e)
        {
            bool isValidated = Validate_EmployeeCode(obj_user, txt_employeeCode, errorProvider);
            if (!isValidated)
            {
                if (btn_addUser.Text == CreateUser)
                    btn_addUser.Enabled = false;
                else
                    btn_editUser.Enabled = false;
            }
        }

        private void txt_phone1_Leave(object sender, EventArgs e)
        {
            bool isValidated = Validate_PhoneNumber(obj_user, txt_phone1, errorProvider);
            if (!isValidated)
            {
                if (btn_addUser.Text == CreateUser)
                    btn_addUser.Enabled = false;
                else
                    btn_editUser.Enabled = false;
            }
        }

        private void txt_phone2_Leave(object sender, EventArgs e)
        {
            bool isValidated = Validate_PhoneNumber(obj_user, txt_phone2, errorProvider);
            if (!isValidated)
            {
                if (btn_addUser.Text == CreateUser)
                    btn_addUser.Enabled = false;
                else
                    btn_editUser.Enabled = false;
            }
        }

        private void txt_mobile_Leave(object sender, EventArgs e)
        {
            bool isValidated = Validate_PhoneNumber(obj_user, txt_mobile, errorProvider);
            if (!isValidated)
            {
                if (btn_addUser.Text == CreateUser)
                    btn_addUser.Enabled = false;
                else
                    btn_editUser.Enabled = false;
            }
        }

        private void txt_faxNumber_Leave(object sender, EventArgs e)
        {
            bool isValidated = Validate_PhoneNumber(obj_user, txt_faxNumber, errorProvider);
            if (!isValidated)
            {
                if (btn_addUser.Text == CreateUser)
                    btn_addUser.Enabled = false;
                else
                    btn_editUser.Enabled = false;
            }

            if (isValidated)
            {
                App_Validation.Apply_ValidationResult(isValidated, String.Empty, txt_faxNumber, errorProvider);
            }
            else
                App_Validation.Apply_ValidationResult(isValidated, "Invalid Fax Number Format", txt_faxNumber, errorProvider);
        }

        private void txt_NID_Leave(object sender, EventArgs e)
        {
            bool isValidated = Validate_NIDNumber(obj_user, txt_NID, errorProvider);
            if (!isValidated)
            {
                if (btn_addUser.Text == CreateUser)
                    btn_addUser.Enabled = false;
                else
                    btn_editUser.Enabled = false;
            }
        }

        private void chk_Activate_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Activate.Checked)
                chk_Activate.Text = "Activate";
            else if (!chk_Activate.Checked)
                chk_Activate.Text = "Freeze";
        }

        private void gp_UserInfo_Leave(object sender, EventArgs e)
        {
            Validate_UserInfo();
        }

        #endregion

        private void label27_Click(object sender, EventArgs e)
        {

        }

        
    }
}