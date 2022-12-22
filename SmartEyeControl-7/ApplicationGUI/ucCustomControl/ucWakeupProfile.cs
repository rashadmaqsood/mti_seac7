using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using SmartEyeControl_7.Common;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.DataContainer;
using SharedCode.Common;
using SEAC.Common;
using SharedCode.Comm.Param;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucWakeupProfile : UserControl
    {
        #region Data_Members

        private Param_IP_ProfilesHelper _Param_IP_ProfilesHelper = null;
        private Param_WakeUp_Profile[] _Param_Wakeup_Profile_object = null;
        private Param_WakeUp_ProfileHelper _Param_WakeUp_ProfileHelperObj = null;
        private Param_Number_ProfileHelper _Param_Number_ProfileHelperObj = null;
        private Param_Communication_Profile _Param_Communication_Profile_object = null;
        private Param_Keep_Alive_IP _Param_Keep_Alive_IP_object = null;
        private bool _Modem_Warnings_disable = false;

        #endregion

        #region Properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_IP_ProfilesHelper Param_IP_ProfilesHelper
        {
            get { return _Param_IP_ProfilesHelper; }
            set
            {
                _Param_IP_ProfilesHelper = value;
                ///Register PropertyChannged Notification Event Handler
                _Param_IP_ProfilesHelper.PropertyChanged -= _Param_IP_ProfilesHelper_PropertyChanged;
                _Param_IP_ProfilesHelper.PropertyChanged += _Param_IP_ProfilesHelper_PropertyChanged;
            }

        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_WakeUp_Profile[] Param_Wakeup_Profile_object
        {
            get { return _Param_Wakeup_Profile_object; }
            set { _Param_Wakeup_Profile_object = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_WakeUp_ProfileHelper Param_WakeUp_ProfileHelperObj
        {
            get { return _Param_WakeUp_ProfileHelperObj; }
            set
            {
                _Param_WakeUp_ProfileHelperObj = value;
                ///Register PropertyChannged Notification Event Handler
                _Param_WakeUp_ProfileHelperObj.PropertyChanged -= _Param_IP_ProfilesHelper_PropertyChanged;
                _Param_WakeUp_ProfileHelperObj.PropertyChanged += _Param_IP_ProfilesHelper_PropertyChanged;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_Number_ProfileHelper Param_Number_ProfileHelperObj
        {
            get { return _Param_Number_ProfileHelperObj; }
            set
            {
                _Param_Number_ProfileHelperObj = value;
                ///Register PropertyChannged Notification Event Handler
                _Param_Number_ProfileHelperObj.PropertyChanged -= _Param_IP_ProfilesHelper_PropertyChanged;
                _Param_Number_ProfileHelperObj.PropertyChanged += _Param_IP_ProfilesHelper_PropertyChanged;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_Communication_Profile Param_Communication_Profile_object
        {
            get { return _Param_Communication_Profile_object; }
            set { _Param_Communication_Profile_object = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_Keep_Alive_IP Param_Keep_Alive_IP_object
        {
            get { return _Param_Keep_Alive_IP_object; }
            set { _Param_Keep_Alive_IP_object = value; }
        }

        public bool IsValidated
        {
            get
            {
                if (errorProvider != null)
                {
                    String ErrorMessage = null;
                    foreach (Control itemCtr in gpWakeUpProfile.Controls)
                    {
                        if (itemCtr.GetType() == typeof(TextBox) ||
                            itemCtr.GetType() == typeof(ComboBox))
                        {
                            ErrorMessage = errorProvider.GetError(itemCtr);
                            if (!String.IsNullOrEmpty(ErrorMessage) ||
                                !String.IsNullOrWhiteSpace(ErrorMessage))
                                return false;
                        }
                    }
                }
                return true;
            }
        }

        public bool Modem_Warnings_disable
        {
            get { return _Modem_Warnings_disable; }
            set { _Modem_Warnings_disable = value; }
        }

        #endregion

        public ucWakeupProfile()
        {
            InitializeComponent();
            if (txt_WakeUpProfile_TotalIDs.Items.Count == 0)
                txt_WakeUpProfile_TotalIDs.Items.AddRange(new object[] { 1, 2, 3, 4 });
            //Attach_Handlers();
        }

        private void ucWakeupProfile_Load(object sender, EventArgs e)
        {
            //Init Code
            if (_Param_IP_ProfilesHelper == null)
            {
                _Param_IP_ProfilesHelper = new Param_IP_ProfilesHelper();
            }
            if (_Param_Wakeup_Profile_object == null)
            {
                _Param_WakeUp_ProfileHelperObj = new Param_WakeUp_ProfileHelper();
                _Param_Wakeup_Profile_object = _Param_WakeUp_ProfileHelperObj.Param_WakeUp_Profile_object;
                _Param_WakeUp_ProfileHelperObj.Previous_Total_Wakeup_profiles = 1;
            }
            if (_Param_Number_ProfileHelperObj == null)
                _Param_Number_ProfileHelperObj = new Param_Number_ProfileHelper();
            if (_Param_Communication_Profile_object == null)
                _Param_Communication_Profile_object = new Param_Communication_Profile();
            if (_Param_Keep_Alive_IP_object == null)
                _Param_Keep_Alive_IP_object = new Param_Keep_Alive_IP();
            if (errorProvider != null)
            {
                foreach (Control itemCtr in gpWakeUpProfile.Controls)
                {
                    if (itemCtr.GetType() == typeof(TextBox) ||
                        itemCtr.GetType() == typeof(ComboBox))
                        errorProvider.SetIconAlignment(itemCtr, ErrorIconAlignment.MiddleRight);
                }
                errorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            }
        }

        private void _Param_IP_ProfilesHelper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                //Param_IP_Profiles_object
                if (e.PropertyName.Equals("Param_IP_Profiles_object"))
                {
                    int total_ParamIPProfileCount = Param_IP_ProfilesHelper.Total_IP_Profile;
                    int selected_Index = txt_WakeUpProfile_ProfileID.SelectedIndex;

                    //Define combo_IPProfileID_Priority1 Contents
                    combo_IPProfileID_Priority1.Items.Clear();
                    combo_IPProfileID_Priority2.Items.Clear();
                    combo_IPProfileID_Priority3.Items.Clear();
                    combo_IPProfileID_Priority4.Items.Clear();

                    combo_IPProfileID_Priority1.Items.Add("None");
                    combo_IPProfileID_Priority2.Items.Add("None");
                    combo_IPProfileID_Priority3.Items.Add("None");
                    combo_IPProfileID_Priority4.Items.Add("None");

                    for (int id = 1; id <= total_ParamIPProfileCount; id++)
                    {
                        combo_IPProfileID_Priority1.Items.Add(id);
                        combo_IPProfileID_Priority2.Items.Add(id);
                        combo_IPProfileID_Priority3.Items.Add(id);
                        combo_IPProfileID_Priority4.Items.Add(id);
                    }
                    int index = 0; //show first profile
                    try
                    {
                        txt_WakeUpProfile_ProfileID.SelectedIndex = Param_Wakeup_Profile_object[index].Wake_Up_Profile_ID - 1;
                        txt_WakeUpProfile_ProfileID_SelectedIndexChanged(txt_WakeUpProfile_ProfileID, new EventArgs());
                    }
                    catch
                    { }
                    //if (selected_Index != -1)
                    //    txt_WakeUpProfile_ProfileID.SelectedIndex = selected_Index;
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error show Wake Up Profile", ex.Message, 5000);
            }
        }

        #region Wakeup Profile

        private void txt_WakeUpProfile_TotalIDs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Modem_Warnings_disable && txt_WakeUpProfile_TotalIDs.SelectedIndex <
                    Param_WakeUp_ProfileHelperObj.Previous_Total_Wakeup_profiles)
                {
                    DialogResult DR = MessageBox.Show("Click OK to Continue, Cancel to Exit", "Warning", MessageBoxButtons.OKCancel);
                    if (DR == DialogResult.Cancel)
                    {
                        txt_WakeUpProfile_TotalIDs.SelectedIndex = Param_WakeUp_ProfileHelperObj.Previous_Total_Wakeup_profiles;
                        return;
                    }
                }

                int total_WakeupProfileDefined = 0;
                total_WakeupProfileDefined = Param_WakeUp_ProfileHelperObj.Total_Wakeup_Profile;
                int selectedIndex = txt_WakeUpProfile_TotalIDs.SelectedIndex;
                int delta = total_WakeupProfileDefined - Convert.ToInt32(txt_WakeUpProfile_TotalIDs.SelectedItem);

                int i;
                if (selectedIndex > -1)
                {
                    if (delta > 0)
                    {
                        ///Verify Number Profiles
                        foreach (var Param_Number_Profile_object in Param_Number_ProfileHelperObj.Param_Number_Profiles_object)
                        {
                            if ((Param_Number_Profile_object.Wake_Up_On_SMS > selectedIndex + 1 ||
                                Param_Number_Profile_object.Wake_Up_On_Voice_Call > selectedIndex + 1) && !Modem_Warnings_disable)
                            {
                                txt_WakeUpProfile_TotalIDs.SelectedIndex = Param_WakeUp_ProfileHelperObj.Previous_Total_Wakeup_profiles;
                                throw new Exception("Param WakeUp Profile being removed is already Assigned(Param Number Profile)");
                            }
                        }
                        ///Verify Param Keep Alive 
                        if (_Param_Keep_Alive_IP_object.Enabled && Param_Keep_Alive_IP_object.IP_Profile_ID > selectedIndex + 1 && !Modem_Warnings_disable)
                        {
                            txt_WakeUpProfile_TotalIDs.SelectedIndex = Param_WakeUp_ProfileHelperObj.Previous_Total_Wakeup_profiles;
                            throw new Exception("Param WakeUp Profile being removed is already Assigned(Param Keep Alive)");
                        }
                        ///Verify Param Communication Profiles 
                        if (Param_Communication_Profile_object.WakeUpProfileID > selectedIndex + 1 && !Modem_Warnings_disable)
                        {
                            txt_WakeUpProfile_TotalIDs.SelectedIndex = Param_WakeUp_ProfileHelperObj.Previous_Total_Wakeup_profiles;
                            throw new Exception("Param WakeUp Profile being removed is already Assigned(Param Communication Profiles)");
                        }

                    }
                    ///Process Param_WakeUp Total IDS
                    if (delta > 0)
                    {
                        i = Math.Abs(delta);
                        while (i > 0)
                        {
                            Param_WakeUp_ProfileHelperObj.RemoveParam_WakeUp_Profile();
                            i--;
                        }
                    }
                    ///Add Param IP Profiles here
                    else if (delta < 0)
                    {
                        i = Math.Abs(delta);
                        while (i > 0)
                        {
                            Param_WakeUp_ProfileHelperObj.AddParam_Wakeup_Profile();
                            i--;
                        }
                    }
                    txt_WakeUpProfile_ProfileID.Items.Clear();
                    int total = Convert.ToInt16(Param_WakeUp_ProfileHelperObj.Total_Wakeup_Profile);
                    for (i = 1; i <= total; i++)
                    {
                        txt_WakeUpProfile_ProfileID.Items.Add(i);
                    }
                    txt_WakeUpProfile_ProfileID.SelectedIndex = 0;
                    #region Commented_code_section

                    //Add Programmed Wakeup Profiles to Keep ALive Wakeup ID& Number Profile
                    ///***modified

                    //combo_KeepAlive_WakeUPProfileID.Items.Clear();
                    //combo_NumberProfile_VoiceCall.Items.Clear();
                    //combo_NumberProfile_SMS.Items.Clear();
                    //combo_CommProfile_ProfileID.Items.Clear();

                    //for (int j = 1; j <= total; j++)
                    //{
                    //    combo_KeepAlive_WakeUPProfileID.Items.Add(j);
                    //    combo_NumberProfile_VoiceCall.Items.Add(j);
                    //    combo_NumberProfile_SMS.Items.Add(j);
                    //    combo_CommProfile_ProfileID.Items.Add(j);
                    //}

                    #endregion
                }
                #region Commented_Code_Section_KeepAlive

                //if (combo_KeepAlive_WakeUPProfileID.Items.Count > 0 && Param_Keep_Alive_IP_object.Enabled
                //    && Param_Keep_Alive_IP_object.IP_Profile_ID > selectedIndex + 1)
                //    combo_KeepAlive_WakeUPProfileID.SelectedIndex = 0;
                //else
                //    combo_KeepAlive_WakeUPProfileID.SelectedIndex = Param_Keep_Alive_IP_object.IP_Profile_ID - 1;

                //if (Param_Communication_Profile_object.WakeUpProfileID > selectedIndex + 1)
                //    combo_CommProfile_ProfileID.SelectedIndex = 0;
                ////else combo_CommProfile_ProfileID.SelectedIndex = Param_Communication_Profile_object.WakeUpProfileID - 1;

                //if (previous_Total_WakeUp_profiles != selectedIndex)
                //    previous_Total_WakeUp_profiles = selectedIndex; 

                #endregion
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error show Wake Up Profile", ex.Message, 5000);
            }
            finally
            {
                if (Param_WakeUp_ProfileHelperObj != null && txt_WakeUpProfile_TotalIDs.SelectedIndex != -1)
                    Param_WakeUp_ProfileHelperObj.Previous_Total_Wakeup_profiles = txt_WakeUpProfile_TotalIDs.SelectedIndex;
            }
        }

        private void txt_WakeUpProfile_ProfileID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = txt_WakeUpProfile_ProfileID.SelectedIndex;
            #region ///Validate Unique ID

            App_Validation_Info ValidationInfo = LocalCommon.AppValidationInfo;
            bool isValidated = App_Validation.Validate_Range(ValidationInfo._Param_WakeUp_Profile_MinId,
                ValidationInfo._Param_WakeUp_Profile_MaxId,
                selectedIndex + 1);

            if (!isValidated)
                App_Validation.Apply_ValidationResult(false, String.Format("Incorrect Number Profile Unique Id:{0}", selectedIndex + 1),
                    txt_WakeUpProfile_ProfileID, ref errorProvider);
            else
                App_Validation.Apply_ValidationResult(true, string.Empty, txt_WakeUpProfile_ProfileID, ref errorProvider);

            #endregion
            Application.DoEvents();
            if (selectedIndex == -1) return;
            try
            {
                Application.DoEvents();
                txt_WakeUpProfile_ProfileID.SelectedIndex = Param_Wakeup_Profile_object[selectedIndex].Wake_Up_Profile_ID - 1;
                combo_IPProfileID_Priority1.SelectedIndex = Param_Wakeup_Profile_object[selectedIndex].IP_Profile_ID_1;
                combo_IPProfileID_Priority2.SelectedIndex = Param_Wakeup_Profile_object[selectedIndex].IP_Profile_ID_2;
                combo_IPProfileID_Priority3.SelectedIndex = Param_Wakeup_Profile_object[selectedIndex].IP_Profile_ID_3;
                combo_IPProfileID_Priority4.SelectedIndex = Param_Wakeup_Profile_object[selectedIndex].IP_Profile_ID_4;
            }
            catch
            {
                txt_WakeUpProfile_ProfileID.SelectedIndex = Param_Wakeup_Profile_object[selectedIndex].Wake_Up_Profile_ID - 1;

                combo_IPProfileID_Priority1.SelectedIndex = 0;
                combo_IPProfileID_Priority2.SelectedIndex = 0;
                combo_IPProfileID_Priority3.SelectedIndex = 0;
                combo_IPProfileID_Priority4.SelectedIndex = 0;

                Param_Wakeup_Profile_object[selectedIndex].IP_Profile_ID_1 = 1;
                Param_Wakeup_Profile_object[selectedIndex].IP_Profile_ID_2 = 1;
                Param_Wakeup_Profile_object[selectedIndex].IP_Profile_ID_3 = 1;
                Param_Wakeup_Profile_object[selectedIndex].IP_Profile_ID_4 = 1;
            }
            #region Commented_CodeSection

            ///Disabled_Flages
            //check_LocalControl_ID1.Checked = Param_Wakeup_Profile_object[index].Local_Control_FLAG_1;
            //check_LocalControl_ID2.Checked = Param_Wakeup_Profile_object[index].Local_Control_FLAG_2;
            //check_LocalControl_ID3.Checked = Param_Wakeup_Profile_object[index].Local_Control_FLAG_3;
            //check_LocalControl_ID4.Checked = Param_Wakeup_Profile_object[index].Local_Control_FLAG_4;

            //check_RemotelyControl_ID1.Checked = Param_Wakeup_Profile_object[index].Remotely_Control_FLAG_1;
            //check_RemotelyControl_ID2.Checked = Param_Wakeup_Profile_object[index].Remotely_Control_FLAG_2;
            //check_RemotelyControl_ID3.Checked = Param_Wakeup_Profile_object[index].Remotely_Control_FLAG_3;
            //check_RemotelyControl_ID4.Checked = Param_Wakeup_Profile_object[index].Remotely_Control_FLAG_4;

            //check_overVolt_ID1.Checked = Param_Wakeup_Profile_object[index].Over_Volt_FLAG_1;
            //check_overVolt_ID2.Checked = Param_Wakeup_Profile_object[index].Over_Volt_FLAG_2;
            //check_overVolt_ID3.Checked = Param_Wakeup_Profile_object[index].Over_Volt_FLAG_3;
            //check_overVolt_ID4.Checked = Param_Wakeup_Profile_object[index].Over_Volt_FLAG_4;

            //check_underVolt_ID1.Checked = Param_Wakeup_Profile_object[index].Under_Volt_FLAG_1;
            //check_underVolt_ID2.Checked = Param_Wakeup_Profile_object[index].Under_Volt_FLAG_2;
            //check_underVolt_ID3.Checked = Param_Wakeup_Profile_object[index].Under_Volt_FLAG_3;
            //check_underVolt_ID4.Checked = Param_Wakeup_Profile_object[index].Under_Volt_FLAG_4; 

            #endregion
        }

        private void combo_IPProfileID_Priority1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (combo_IPProfileID_Priority1.SelectedIndex == 0)
                    combo_IPProfileID_Priority1.SelectedIndex = 1;
                Application.DoEvents();
                int index_IP = combo_IPProfileID_Priority1.SelectedIndex;
                int index_wakeUp = txt_WakeUpProfile_ProfileID.SelectedIndex;
                if (index_IP == -1 || index_wakeUp == -1) return;
                bool isValidated = Validate_IPProfileId(Param_Wakeup_Profile_object[index_wakeUp], combo_IPProfileID_Priority1);
                if (index_IP == 0)
                {
                    Param_Wakeup_Profile_object[index_wakeUp].IP_Profile_ID_1 = 0;
                    return;
                }
                else if (isValidated)
                {
                    Param_Wakeup_Profile_object[index_wakeUp].IP_Profile_ID_1 = (byte)index_IP;
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error IPProfileID_Priority1", ex.Message, 1500);
            }
        }

        private void combo_IPProfileID_Priority2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index_IP = combo_IPProfileID_Priority2.SelectedIndex;
                int index_wakeUp = txt_WakeUpProfile_ProfileID.SelectedIndex;
                if (index_IP == -1 || index_wakeUp == -1) return;
                bool isValidated = Validate_IPProfileId(Param_Wakeup_Profile_object[index_wakeUp], combo_IPProfileID_Priority2);
                if (index_IP == 0)
                {
                    Param_Wakeup_Profile_object[index_wakeUp].IP_Profile_ID_2 = 0;
                    return;
                }
                else if (isValidated)
                {
                    Param_Wakeup_Profile_object[index_wakeUp].IP_Profile_ID_2 = (byte)index_IP;
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error IPProfileID_Priority2", ex.Message, 1500);
            }
        }

        private void combo_IPProfileID_Priority3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index_IP = combo_IPProfileID_Priority3.SelectedIndex;
                int index_wakeUp = txt_WakeUpProfile_ProfileID.SelectedIndex;
                if (index_IP == -1 || index_wakeUp == -1) return;
                bool isValidated = Validate_IPProfileId(Param_Wakeup_Profile_object[index_wakeUp], combo_IPProfileID_Priority3);
                if (index_IP == 0)
                {
                    Param_Wakeup_Profile_object[index_wakeUp].IP_Profile_ID_3 = 0;
                    return;
                }
                else if (isValidated)
                {
                    Param_Wakeup_Profile_object[index_wakeUp].IP_Profile_ID_3 = (byte)index_IP;
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error IPProfileID_Priority3", ex.Message, 1500);
            }
        }

        private void combo_IPProfileID_Priority4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index_IP = combo_IPProfileID_Priority4.SelectedIndex;
                int index_wakeUp = txt_WakeUpProfile_ProfileID.SelectedIndex;
                if (index_IP == -1 || index_wakeUp == -1) return;
                bool isValidated = Validate_IPProfileId(Param_Wakeup_Profile_object[index_wakeUp], combo_IPProfileID_Priority4);
                if (index_IP == 0)
                {
                    Param_Wakeup_Profile_object[index_wakeUp].IP_Profile_ID_4 = 0;
                    return;
                }
                else if (isValidated)
                {
                    Param_Wakeup_Profile_object[index_wakeUp].IP_Profile_ID_4 = (byte)index_IP;
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error IPProfileID_Priority4", ex.Message, 1500);
            }
        }

        private bool Validate_IPProfileId(Param_WakeUp_Profile Wk_Arg, ComboBox combo_IPProfileID_Priority)
        {
            bool isValidated = false;
            try
            {
                int index_IP = combo_IPProfileID_Priority1.SelectedIndex;
                FieldInfo fInfo = typeof(Param_IP_Profiles).GetField("Unique_ID");
                isValidated = App_Validation.Validate_LookupId(fInfo, Param_IP_ProfilesHelper.Param_IP_Profiles_object, (byte)index_IP);
                if (isValidated || index_IP == 0)
                    App_Validation.Apply_ValidationResult(true, String.Empty, combo_IPProfileID_Priority, ref errorProvider);
                else
                    App_Validation.Apply_ValidationResult(true, String.Format("Validation Failed,Incorrect Param_IP_Profile_Id:{0}", index_IP),
                        combo_IPProfileID_Priority, ref errorProvider);
                return isValidated;
            }
            catch
            { }
            return isValidated;
        }

        private void check_overVolt_ID1_CheckedChanged(object sender, EventArgs e)
        {
            int index = txt_WakeUpProfile_ProfileID.SelectedIndex;
            if (index == -1) return;
            CheckBox chk = (CheckBox)sender;
            if (chk == check_overVolt_ID1)
                Param_Wakeup_Profile_object[index].Over_Volt_FLAG_1 = chk.Checked;
            else if (chk == check_overVolt_ID2)
                Param_Wakeup_Profile_object[index].Over_Volt_FLAG_2 = chk.Checked;
            else if (chk == check_overVolt_ID3)
                Param_Wakeup_Profile_object[index].Over_Volt_FLAG_3 = chk.Checked;
            else if (chk == check_overVolt_ID4)
                Param_Wakeup_Profile_object[index].Over_Volt_FLAG_4 = chk.Checked;
        }

        private void check_underVolt_ID1_CheckedChanged(object sender, EventArgs e)
        {
            int index = txt_WakeUpProfile_ProfileID.SelectedIndex;
            if (index == -1) return;
            CheckBox chk = (CheckBox)sender;
            if (chk == check_underVolt_ID1)
                Param_Wakeup_Profile_object[index].Under_Volt_FLAG_1 = chk.Checked;
            else if (chk == check_underVolt_ID2)
                Param_Wakeup_Profile_object[index].Under_Volt_FLAG_2 = chk.Checked;
            else if (chk == check_underVolt_ID3)
                Param_Wakeup_Profile_object[index].Under_Volt_FLAG_3 = chk.Checked;
            else if (chk == check_underVolt_ID4)
                Param_Wakeup_Profile_object[index].Under_Volt_FLAG_4 = chk.Checked;
        }

        private void check_RemotelyControl_ID1_CheckedChanged(object sender, EventArgs e)
        {
            int index = txt_WakeUpProfile_ProfileID.SelectedIndex;
            if (index == -1) return;
            CheckBox chk = (CheckBox)sender;
            if (chk == check_RemotelyControl_ID1)
                Param_Wakeup_Profile_object[index].Remotely_Control_FLAG_1 = chk.Checked;
            else if (chk == check_RemotelyControl_ID2)
                Param_Wakeup_Profile_object[index].Remotely_Control_FLAG_2 = chk.Checked;
            else if (chk == check_RemotelyControl_ID3)
                Param_Wakeup_Profile_object[index].Remotely_Control_FLAG_3 = chk.Checked;
            else if (chk == check_RemotelyControl_ID4)
                Param_Wakeup_Profile_object[index].Remotely_Control_FLAG_4 = chk.Checked;
        }

        private void check_LocalControl_ID1_CheckedChanged(object sender, EventArgs e)
        {
            int index = txt_WakeUpProfile_ProfileID.SelectedIndex;
            if (index == -1) return;
            CheckBox chk = (CheckBox)sender;
            if (chk == check_LocalControl_ID1)
                Param_Wakeup_Profile_object[index].Local_Control_FLAG_1 = chk.Checked;
            else if (chk == check_LocalControl_ID2)
                Param_Wakeup_Profile_object[index].Local_Control_FLAG_2 = chk.Checked;
            else if (chk == check_LocalControl_ID3)
                Param_Wakeup_Profile_object[index].Local_Control_FLAG_3 = chk.Checked;
            else if (chk == check_LocalControl_ID4)
                Param_Wakeup_Profile_object[index].Local_Control_FLAG_4 = chk.Checked;
        }

        #endregion

        public void showToGUI_WakeUpProfile()
        {
            int i = 0;
            try
            {
                Dettach_Handlers();

                int Param_Wakeup_Profile_Count = Param_WakeUp_ProfileHelperObj.Total_Wakeup_Profile;
                if (Param_Wakeup_Profile_Count < 1)
                    throw new Exception("Zero Wakeup Profile Defined");
                txt_WakeUpProfile_TotalIDs.SelectedIndex = Param_Wakeup_Profile_Count - 1;

                #region //Update Wakeup Profile Unique ID

                if (txt_WakeUpProfile_ProfileID.Items.Count != Param_Wakeup_Profile_Count)
                {
                    txt_WakeUpProfile_ProfileID.Items.Clear();
                    for (i = 1; i <= Param_Wakeup_Profile_Count; i++)
                    {
                        txt_WakeUpProfile_ProfileID.Items.Add(i);
                    }
                }

                #endregion
                int total_ParamIPProfileCount = Param_IP_ProfilesHelper.Total_IP_Profile;
                #region //update combo_IPProfileID_Priority1

                if (combo_IPProfileID_Priority1.Items.Count != total_ParamIPProfileCount + 1)
                {
                    //Define combo_IPProfileID_Priority1 Contents
                    combo_IPProfileID_Priority1.Items.Clear();
                    combo_IPProfileID_Priority2.Items.Clear();
                    combo_IPProfileID_Priority3.Items.Clear();
                    combo_IPProfileID_Priority4.Items.Clear();

                    combo_IPProfileID_Priority1.Items.Add("None");
                    combo_IPProfileID_Priority2.Items.Add("None");
                    combo_IPProfileID_Priority3.Items.Add("None");
                    combo_IPProfileID_Priority4.Items.Add("None");

                    for (int id = 1; id <= total_ParamIPProfileCount; id++)
                    {
                        combo_IPProfileID_Priority1.Items.Add(id);
                        combo_IPProfileID_Priority2.Items.Add(id);
                        combo_IPProfileID_Priority3.Items.Add(id);
                        combo_IPProfileID_Priority4.Items.Add(id);
                    }
                }

                #endregion
                int index = 0; //show first profile
                try
                {
                    txt_WakeUpProfile_ProfileID.SelectedIndex = Param_Wakeup_Profile_object[index].Wake_Up_Profile_ID - 1;
                }
                catch
                { }
                if((int)(Param_Wakeup_Profile_object[index].IP_Profile_ID_1) < combo_IPProfileID_Priority1.Items.Count)
                    combo_IPProfileID_Priority1.SelectedIndex = (int)(Param_Wakeup_Profile_object[index].IP_Profile_ID_1);//+1 to deal with the "NONE" Label
                if ((int)(Param_Wakeup_Profile_object[index].IP_Profile_ID_2) < combo_IPProfileID_Priority2.Items.Count)
                    combo_IPProfileID_Priority2.SelectedIndex = (int)(Param_Wakeup_Profile_object[index].IP_Profile_ID_2);
                if ((int)(Param_Wakeup_Profile_object[index].IP_Profile_ID_3) < combo_IPProfileID_Priority3.Items.Count)
                    combo_IPProfileID_Priority3.SelectedIndex = (int)(Param_Wakeup_Profile_object[index].IP_Profile_ID_3);
                if ((int)(Param_Wakeup_Profile_object[index].IP_Profile_ID_4) < combo_IPProfileID_Priority4.Items.Count)
                    combo_IPProfileID_Priority4.SelectedIndex = (int)(Param_Wakeup_Profile_object[index].IP_Profile_ID_4);

                #region Commented_Code_Section
                //Disabled_Flages
                ///check_overVolt_ID1.Checked = Param_Wakeup_Profile_object[index].Over_Volt_FLAG_1;
                ///check_overVolt_ID2.Checked = Param_Wakeup_Profile_object[index].Over_Volt_FLAG_2;
                ///check_overVolt_ID3.Checked = Param_Wakeup_Profile_object[index].Over_Volt_FLAG_3;
                ///check_overVolt_ID4.Checked = Param_Wakeup_Profile_object[index].Over_Volt_FLAG_4;

                ///check_underVolt_ID1.Checked = Param_Wakeup_Profile_object[index].Under_Volt_FLAG_1;
                ///check_underVolt_ID2.Checked = Param_Wakeup_Profile_object[index].Under_Volt_FLAG_2;
                ///check_underVolt_ID3.Checked = Param_Wakeup_Profile_object[index].Under_Volt_FLAG_3;
                ///check_underVolt_ID4.Checked = Param_Wakeup_Profile_object[index].Under_Volt_FLAG_4;

                ///check_RemotelyControl_ID1.Checked = Param_Wakeup_Profile_object[index].Remotely_Control_FLAG_1;
                ///check_RemotelyControl_ID2.Checked = Param_Wakeup_Profile_object[index].Remotely_Control_FLAG_2;
                ///check_RemotelyControl_ID3.Checked = Param_Wakeup_Profile_object[index].Remotely_Control_FLAG_3;
                ///check_RemotelyControl_ID4.Checked = Param_Wakeup_Profile_object[index].Remotely_Control_FLAG_4;

                ///check_LocalControl_ID1.Checked = Param_Wakeup_Profile_object[index].Local_Control_FLAG_1;
                ///check_LocalControl_ID2.Checked = Param_Wakeup_Profile_object[index].Local_Control_FLAG_2;
                ///check_LocalControl_ID3.Checked = Param_Wakeup_Profile_object[index].Local_Control_FLAG_3;
                ///check_LocalControl_ID4.Checked = Param_Wakeup_Profile_object[index].Local_Control_FLAG_4; 
                #endregion
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error show Wake Up Profile", ex.Message, 5000);
            }
            finally
            {
                if (Param_WakeUp_ProfileHelperObj != null && txt_WakeUpProfile_TotalIDs.SelectedIndex != -1)
                    Param_WakeUp_ProfileHelperObj.Previous_Total_Wakeup_profiles = txt_WakeUpProfile_TotalIDs.SelectedIndex;
                Modem_Warnings_disable = false;
                Attach_Handlers();
            }
        }

        internal void Attach_Handlers()
        {
            try
            {
                txt_WakeUpProfile_TotalIDs.SelectedIndexChanged += txt_WakeUpProfile_TotalIDs_SelectedIndexChanged;
                txt_WakeUpProfile_ProfileID.SelectedIndexChanged += txt_WakeUpProfile_ProfileID_SelectedIndexChanged;

                combo_IPProfileID_Priority1.SelectedIndexChanged += combo_IPProfileID_Priority1_SelectedIndexChanged;
                combo_IPProfileID_Priority2.SelectedIndexChanged += combo_IPProfileID_Priority2_SelectedIndexChanged;
                combo_IPProfileID_Priority3.SelectedIndexChanged += combo_IPProfileID_Priority3_SelectedIndexChanged;
                combo_IPProfileID_Priority4.SelectedIndexChanged += combo_IPProfileID_Priority4_SelectedIndexChanged;

                check_overVolt_ID1.CheckedChanged += check_overVolt_ID1_CheckedChanged;
                check_underVolt_ID1.CheckedChanged += check_underVolt_ID1_CheckedChanged;
                check_RemotelyControl_ID1.CheckedChanged += check_RemotelyControl_ID1_CheckedChanged;
                check_LocalControl_ID1.CheckedChanged += check_LocalControl_ID1_CheckedChanged;
            }
            catch { throw; }
        }

        internal void Dettach_Handlers()
        {
            try
            {
                txt_WakeUpProfile_TotalIDs.SelectedIndexChanged -= txt_WakeUpProfile_TotalIDs_SelectedIndexChanged;
                txt_WakeUpProfile_ProfileID.SelectedIndexChanged -= txt_WakeUpProfile_ProfileID_SelectedIndexChanged;

                combo_IPProfileID_Priority1.SelectedIndexChanged -= combo_IPProfileID_Priority1_SelectedIndexChanged;
                combo_IPProfileID_Priority2.SelectedIndexChanged -= combo_IPProfileID_Priority2_SelectedIndexChanged;
                combo_IPProfileID_Priority3.SelectedIndexChanged -= combo_IPProfileID_Priority3_SelectedIndexChanged;
                combo_IPProfileID_Priority4.SelectedIndexChanged -= combo_IPProfileID_Priority4_SelectedIndexChanged;

                check_overVolt_ID1.CheckedChanged -= check_overVolt_ID1_CheckedChanged;
                check_underVolt_ID1.CheckedChanged -= check_underVolt_ID1_CheckedChanged;
                check_RemotelyControl_ID1.CheckedChanged -= check_RemotelyControl_ID1_CheckedChanged;
                check_LocalControl_ID1.CheckedChanged -= check_LocalControl_ID1_CheckedChanged;
            }
            catch { throw; }
        }

        #region AccessControlMethods

        public bool ApplyAccessRights(List<AccessRights> Rights)
        {
            bool isSuccess = false;
            try
            {
                this.SuspendLayout();
                this.fLP_Main.SuspendLayout();

                if (Rights.Find(x => x.Read == true || x.Write == true) != null)
                {
                    foreach (var item in Rights)
                    {
                        _HelperAccessRights((WakeUpProfiles)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
                    }
                    isSuccess = true;
                }
            }
            finally
            {
                this.ResumeLayout();
                this.fLP_Main.ResumeLayout();
            }
            return isSuccess;
        }

        private void _HelperAccessRights(WakeUpProfiles qty, bool read, bool write)
        {
            switch (qty)
            {
                case WakeUpProfiles.TotalWakeUPProfile:
                    txt_WakeUpProfile_TotalIDs.Enabled = write;
                    fLP_totalWakeupProfile.Visible = (read || write);
                    break;
                case WakeUpProfiles.ID:
                    fLP_ID.Visible = read;
                    break;
                case WakeUpProfiles.Priority1IPProfileID:
                    combo_IPProfileID_Priority1.Enabled = write;
                    fLP_1IPProf.Visible = (read || write);
                    break;
                case WakeUpProfiles.Priority2IPProfileID:
                    combo_IPProfileID_Priority2.Enabled = write;
                    fLP_IPProf2.Visible = (read || write);
                    break;
                case WakeUpProfiles.Priority3IPProfileID:
                    combo_IPProfileID_Priority3.Enabled = write;
                    fLP_IPProf3.Visible = (read || write);
                    break;
                case WakeUpProfiles.Priority4IPProfileID:
                    combo_IPProfileID_Priority4.Enabled = write;
                    fLP_IPProf4.Visible = (read || write);
                    break;
                default:
                    break;
            }
        }

        #endregion

        //Flickering Reduction
        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN
                return parms;
            }
        }
    }
}
