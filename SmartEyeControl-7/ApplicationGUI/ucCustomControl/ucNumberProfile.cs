using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using DLMS;
using System.Collections.Generic;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.DataContainer;
using SharedCode.Common;
using SEAC.Common;
using SharedCode.Comm.Param;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucNumberProfile : UserControl
    {
        #region Data_Members

        private Param_Number_Profile[] _Param_Number_Profile_object = null;
        private Param_Number_ProfileHelper _Param_Number_ProfileHelperObj = null;
        private Param_WakeUp_ProfileHelper _Param_WakeUp_ProfileHelperObj = null;
        private Param_Communication_Profile _Param_Communication_Profile_object = null;
        private bool _Modem_Warnings_disable = false;

        #endregion

        #region Properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_Number_Profile[] Param_Number_Profile_object
        {
            get { return _Param_Number_Profile_object; }
            set { _Param_Number_Profile_object = value; }
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
                _Param_WakeUp_ProfileHelperObj.PropertyChanged -= _Param_WakeUp_ProfilesHelper_PropertyChanged;
                _Param_WakeUp_ProfileHelperObj.PropertyChanged += _Param_WakeUp_ProfilesHelper_PropertyChanged;
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
                _Param_Number_ProfileHelperObj.PropertyChanged -= _Param_WakeUp_ProfilesHelper_PropertyChanged;
                _Param_Number_ProfileHelperObj.PropertyChanged += _Param_WakeUp_ProfilesHelper_PropertyChanged;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_Communication_Profile Param_Communication_Profile_object
        {
            get { return _Param_Communication_Profile_object; }
            set { _Param_Communication_Profile_object = value; }
        }

        public bool Modem_Warnings_disable
        {
            get { return _Modem_Warnings_disable; }
            set { _Modem_Warnings_disable = value; }
        }

        public bool IsValidated
        {
            get
            {
                if (errorProvider != null)
                {
                    String ErrorMessage = null;
                    foreach (Control itemCtr in gpNumberProfile.Controls)
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

        #endregion

        public ucNumberProfile()
        {
            InitializeComponent();
            //Attach_Handlers();
            if (combo_NumberProfile_TotalProfiles.Items.Count == 0)
                combo_NumberProfile_TotalProfiles.Items.AddRange(new object[] { 1, 2, 3, 4 });
        }

        private void ucNumberProfile_Load(object sender, EventArgs e)
        {
            #region ///Init Code

            if (_Param_WakeUp_ProfileHelperObj == null)
            {
                _Param_WakeUp_ProfileHelperObj = new Param_WakeUp_ProfileHelper();
                _Param_WakeUp_ProfileHelperObj.Previous_Total_Wakeup_profiles = 0;
            }
            if (_Param_Number_ProfileHelperObj == null)
                _Param_Number_ProfileHelperObj = new Param_Number_ProfileHelper();
            if (_Param_Communication_Profile_object == null)
                _Param_Communication_Profile_object = new Param_Communication_Profile();

            #endregion
            if (errorProvider != null)
            {
                foreach (Control itemCtr in gpNumberProfile.Controls)
                {
                    if (itemCtr.GetType() == typeof(TextBox) ||
                        itemCtr.GetType() == typeof(ComboBox))
                        errorProvider.SetIconAlignment(itemCtr, ErrorIconAlignment.MiddleRight);
                }
                errorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            }
        }

        private void _Param_WakeUp_ProfilesHelper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                //Param_IP_Profiles_object
                if (e.PropertyName.Equals("Param_Wakeup_Profile_object"))
                {
                    int total = Param_WakeUp_ProfileHelperObj.Total_Wakeup_Profile;

                    combo_NumberProfile_VoiceCall.Items.Clear();
                    combo_NumberProfile_SMS.Items.Clear();

                    for (int j = 1; j <= total; j++)
                    {
                        combo_NumberProfile_VoiceCall.Items.Add(j);
                        combo_NumberProfile_SMS.Items.Add(j);
                    }
                    //To Refresh GUI to Display ParamNumberProfile
                    try
                    {
                        combo_NumberProfile_UniqueID.SelectedIndex = 0;
                        combo_NumberProfile_UniqueID_SelectedIndexChanged(combo_NumberProfile_UniqueID, new EventArgs());
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error show Number Profile", ex.Message, 5000);
            }
        }

        #region Number_profile_leave_events

        private void combo_NumberProfile_TotalProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Modem_Warnings_disable && combo_NumberProfile_TotalProfiles.SelectedIndex <
                    Param_Number_ProfileHelperObj.Previous_Total_number_profiles)
                {
                    DialogResult DR = MessageBox.Show("Click OK to Continue, Cancel to Exit", "Warning", MessageBoxButtons.OKCancel);
                    if (DR == DialogResult.Cancel)
                    {
                        combo_NumberProfile_TotalProfiles.SelectedIndex = Param_Number_ProfileHelperObj.Previous_Total_number_profiles;
                        return;
                    }
                }
                int total_NumberProfileDefined = 0;
                total_NumberProfileDefined = Param_Number_ProfileHelperObj.Total_Number_Profile;
                int selectedIndex = combo_NumberProfile_TotalProfiles.SelectedIndex;
                int delta = total_NumberProfileDefined - Convert.ToInt32(combo_NumberProfile_TotalProfiles.SelectedItem);

                int i;
                if (selectedIndex > -1)
                {
                    if (delta > 0)
                        //Verify Either Param Communication Profile Already Assigned
                        if ((Param_Communication_Profile_object.NumberProfileID[0] > selectedIndex + 1 ||
                            Param_Communication_Profile_object.NumberProfileID[1] > selectedIndex + 1 ||
                            Param_Communication_Profile_object.NumberProfileID[2] > selectedIndex + 1 ||
                            Param_Communication_Profile_object.NumberProfileID[3] > selectedIndex + 1) && !Modem_Warnings_disable)
                        {
                            combo_NumberProfile_TotalProfiles.SelectedIndex =
                                Param_Number_ProfileHelperObj.Previous_Total_number_profiles;
                            throw new Exception("Number Profiles being removed are used");
                        }
                    //Process ParamNumberProfile Total IDS
                    if (delta > 0)
                    {
                        i = Math.Abs(delta);
                        while (i > 0)
                        {
                            Param_Number_ProfileHelperObj.RemoveParam_Number_Profile();
                            i--;
                        }
                    }
                    //Add ParamNumberProfile here
                    else if (delta < 0)
                    {
                        i = Math.Abs(delta);
                        while (i > 0)
                        {
                            Param_Number_ProfileHelperObj.AddParam_Number_Profile();
                            i--;
                        }
                    }
                    //Repopulate Number Profile Unique ID
                    combo_NumberProfile_UniqueID.Items.Clear();
                    int total = Convert.ToInt16(Param_Number_ProfileHelperObj.Total_Number_Profile);
                    for (i = 1; i <= total; i++)
                    {
                        combo_NumberProfile_UniqueID.Items.Add(i);
                    }
                    for (i = total; i < 5; i++)
                    {
                        Param_Number_Profile_object[i].Wake_Up_On_Voice_Call = 1;
                        Param_Number_Profile_object[i].Wake_Up_On_SMS = 1;
                    }
                    #region Commented_CodeSection

                    //combo_NumberProfile_UniqueID.SelectedIndex = 0;
                    //Add programmed number profiles to communicationProfile-> NUmberProfile ID

                    //Add total Number Profiles to Wakeup Profile combos
                    //First Add "None" to combos

                    //combo_commProfile_NumberProfileID_1.Items.Clear();
                    //combo_commProfile_NumberProfileID_2.Items.Clear();
                    //combo_commProfile_NumberProfileID_3.Items.Clear();
                    //combo_commProfile_NumberProfileID_4.Items.Clear();

                    //combo_commProfile_NumberProfileID_1.Items.Add("None");
                    //combo_commProfile_NumberProfileID_2.Items.Add("None");
                    //combo_commProfile_NumberProfileID_3.Items.Add("None");
                    //combo_commProfile_NumberProfileID_4.Items.Add("None");

                    //for (int j = 1; j <= total; j++)
                    //{
                    //    combo_commProfile_NumberProfileID_1.Items.Add(j);
                    //    combo_commProfile_NumberProfileID_2.Items.Add(j);
                    //    combo_commProfile_NumberProfileID_3.Items.Add(j);
                    //    combo_commProfile_NumberProfileID_4.Items.Add(j);
                    //}

                    //// if the Comm Profiles_NUM[IDS] are lower than number profile selected index then,, DONOT REST Comm Profile
                    //if (Param_Communication_Profile_object.NumberProfileID[0] > selectedIndex + 1 ||
                    //    Param_Communication_Profile_object.NumberProfileID[1] > selectedIndex + 1 ||
                    //    Param_Communication_Profile_object.NumberProfileID[2] > selectedIndex + 1 ||
                    //    Param_Communication_Profile_object.NumberProfileID[3] > selectedIndex + 1)
                    //{
                    //    combo_commProfile_NumberProfileID_1.SelectedIndex = 0;
                    //    combo_commProfile_NumberProfileID_2.SelectedIndex = 0;
                    //    combo_commProfile_NumberProfileID_3.SelectedIndex = 0;
                    //    combo_commProfile_NumberProfileID_4.SelectedIndex = 0;
                    //}
                    //if (previous_Total_Number_profiles != selectedIndex)
                    //    previous_Total_Number_profiles = selectedIndex;

                    #endregion
                }
                combo_NumberProfile_UniqueID.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error show  ParamNumberProfile", ex.Message, 5000);
            }
            finally
            {
                if (Param_Number_ProfileHelperObj != null && combo_NumberProfile_TotalProfiles.SelectedIndex != -1)
                    Param_Number_ProfileHelperObj.Previous_Total_number_profiles = combo_NumberProfile_TotalProfiles.SelectedIndex;
                Modem_Warnings_disable = false;
            }
        }

        private void combo_NumberProfile_UniqueID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = combo_NumberProfile_UniqueID.SelectedIndex;
            #region ///Validate Unique ID

            App_Validation_Info ValidationInfo = LocalCommon.AppValidationInfo;
            bool isValidated = App_Validation.Validate_Range(ValidationInfo._Param_Number_Profile_MinId,
                ValidationInfo._Param_Number_Profile_MaxId,
                selectedIndex + 1);

            if (!isValidated)
                App_Validation.Apply_ValidationResult(false, String.Format("Incorrect Number Profile Unique Id:{0}", selectedIndex + 1),
                    combo_NumberProfile_UniqueID, ref errorProvider);
            else
                App_Validation.Apply_ValidationResult(true, string.Empty, combo_NumberProfile_UniqueID, ref errorProvider);

            #endregion

            if (selectedIndex > -1)
            {
                if (selectedIndex == 4)
                {
                    txt_NumberProfile_Number.Enabled = false;
                }
                else
                {
                    txt_NumberProfile_Number.Enabled = true;
                }
                showToGUI_NumberProfile();
            }
        }

        private void combo_NumberProfile_VoiceCall_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (combo_NumberProfile_VoiceCall.SelectedIndex != -1)
                {
                    int a = combo_NumberProfile_UniqueID.SelectedIndex;

                    if (radio_modem_special.Checked)
                    {
                        a = 4;
                    }
                    Param_Number_Profile_object[a].Wake_Up_On_Voice_Call = (byte)(combo_NumberProfile_VoiceCall.SelectedIndex + 1);
                    Param_Number_Profile numProf = Param_Number_Profile_object[a];
                    #region Validate Wakeup_ProfileID Lookup
                    String ErrorMessage = String.Empty;
                    FieldInfo fInfo = typeof(Param_WakeUp_Profile).GetField("Wake_Up_Profile_ID");
                    ///Validate  numProf.Wake_Up_On_Voice_Call_ID Lookup
                    bool isValidated = App_Validation.Validate_LookupId(fInfo, Param_WakeUp_ProfileHelperObj.Param_WakeUp_Profile_object,
                        numProf.Wake_Up_On_Voice_Call);
                    if (!isValidated)
                    {
                        ErrorMessage = String.Format("Validation failed,InConsistent Number Profile.Wake_Up_On_Voice_Call ID {0}"
                            , numProf.Wake_Up_On_Voice_Call);
                        App_Validation.Apply_ValidationResult(false, ErrorMessage, combo_NumberProfile_VoiceCall, ref errorProvider);
                    }
                    else
                        App_Validation.Apply_ValidationResult(true, String.Empty, combo_NumberProfile_VoiceCall, ref errorProvider);

                    #endregion
                }
            }
            catch (Exception)
            {

            }
        }

        private void combo_NumberProfile_SMS_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (combo_NumberProfile_SMS.SelectedIndex != -1)
                {
                    int a = combo_NumberProfile_UniqueID.SelectedIndex;
                    if (radio_modem_special.Checked)
                    {
                        a = 4;
                    }
                    Param_Number_Profile_object[a].Wake_Up_On_SMS = (byte)(combo_NumberProfile_SMS.SelectedIndex + 1);
                    Param_Number_Profile numProf = Param_Number_Profile_object[a];
                    #region Validate Wakeup_ProfileID Lookup

                    String ErrorMessage = String.Empty;
                    FieldInfo fInfo = typeof(Param_WakeUp_Profile).GetField("Wake_Up_Profile_ID");
                    ///Validate  numProf.Wake_Up_On_Voice_Call_ID Lookup
                    bool isValidated = App_Validation.Validate_LookupId(fInfo, Param_WakeUp_ProfileHelperObj.Param_WakeUp_Profile_object,
                        numProf.Wake_Up_On_SMS);
                    if (!isValidated)
                    {
                        ErrorMessage = String.Format("Validation failed,InConsistent Number Profile.Wake_Up_On_SMS ID {0}"
                            , numProf.Wake_Up_On_SMS);
                        App_Validation.Apply_ValidationResult(false, ErrorMessage, combo_NumberProfile_SMS, ref errorProvider);
                    }
                    else
                        App_Validation.Apply_ValidationResult(true, String.Empty, combo_NumberProfile_SMS, ref errorProvider);

                    #endregion
                }
            }
            catch (Exception)
            {


            }
        }

        private void combo_NumberProfile_WakeupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int a = combo_NumberProfile_UniqueID.SelectedIndex;
            if (radio_modem_special.Checked)
            {
                a = 4;
            }
            if (combo_NumberProfile_WakeupType.SelectedIndex >= 0)
            {
                Param_Number_Profile_object[a].FLAG2 = (byte)combo_NumberProfile_WakeupType.SelectedIndex;
            }
        }

        private void txt_NumberProfile_Number_Leave(object sender, EventArgs e)
        {
            int a = combo_NumberProfile_UniqueID.SelectedIndex;
            if (a == -1)
            {
                combo_NumberProfile_UniqueID.SelectedIndex = 0;
            }
            a = combo_NumberProfile_UniqueID.SelectedIndex;
            ///***modification
            String Phn_Number_Txt = txt_NumberProfile_Number.Text;
            if (true || App_Validation.Validate_IntlPhoneNumberFormat(Phn_Number_Txt))
            ///if (Phn_Number_Txt.Length < 16)
            {
                ///Apply data Validation Results
                App_Validation.Apply_ValidationResult(true, String.Empty, txt_NumberProfile_Number, ref errorProvider);
                Param_Number_Profile_object[a].Number = LocalCommon.ConvertFromValidNumberString(Phn_Number_Txt);

                //byte[] byte_array = new byte[16] { 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63 };
                //Param_Number_Profile_object[a].Number = Encoding.ASCII.GetBytes(txt_NumberProfile_Number.Text);
                //byte_array = DLMS_Common.Append_to_End(Param_Number_Profile_object[a].Number, byte_array);
                //for (int i = 0; i < byte_array.Length; i++)
                //{
                //    byte_array[i] = (byte)(byte_array[i] - 48);
                //}
                //Array.Resize(ref byte_array, 16);
                //Param_Number_Profile_object[a].Number = byte_array;
                //Param_Number_Profile_object[4].Number = new byte[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            }
            else
            {
                String ErrorMessage = String.Format("Validation failed,Incorrect Number Format {0}", Phn_Number_Txt);
                ///Apply data Validation Results
                App_Validation.Apply_ValidationResult(false, ErrorMessage, txt_NumberProfile_Number, ref errorProvider);
            }
        }

        private void txt_NumberProfile_DataCallNumber_Leave(object sender, EventArgs e)
        {
            int a = combo_NumberProfile_UniqueID.SelectedIndex;
            if (a < 0)
                combo_NumberProfile_UniqueID.SelectedIndex = 0;
            a = combo_NumberProfile_UniqueID.SelectedIndex;

            String Phn_Number_Txt = txt_NumberProfile_DataCallNumber.Text;
            if (App_Validation.Validate_IntlPhoneNumberFormat(Phn_Number_Txt))
            ///if (Phn_Number_Txt.Length < 16)
            {
                ///Apply data Validation Results
                App_Validation.Apply_ValidationResult(true, String.Empty, txt_NumberProfile_DataCallNumber, ref errorProvider);

                byte[] byte_array = new byte[16] { 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63 };
                Param_Number_Profile_object[a].Datacall_Number = Encoding.ASCII.GetBytes(Phn_Number_Txt);
                byte_array = DLMS_Common.Append_to_End(Param_Number_Profile_object[a].Datacall_Number, byte_array);
                for (int i = 0; i < byte_array.Length; i++)
                {
                    byte_array[i] = (byte)(byte_array[i] - 48);
                }
                Array.Resize(ref byte_array, 16);
                Param_Number_Profile_object[a].Datacall_Number = byte_array;
            }
            else
            {
                String ErrorMessage = String.Format("Validation failed,Incorrect Number Format {0}", Phn_Number_Txt);
                ///Apply data Validation Results
                App_Validation.Apply_ValidationResult(false, ErrorMessage, txt_NumberProfile_DataCallNumber, ref errorProvider);
            }
        }

        private void check_NumberProfile_VerifyPassword_CheckedChanged(object sender, EventArgs e)
        {
            int a = combo_NumberProfile_UniqueID.SelectedIndex;
            if (radio_modem_special.Checked)
            {
                a = 4;
            }
            if (a < 0)
            {
                combo_NumberProfile_UniqueID.SelectedIndex = 0;
                a = 0;
            }
            Param_Number_Profile_object[a].Verify_Password_FLAG_0 = check_NumberProfile_VerifyPassword.Checked;

        }

        private void check_NumberProfile__AcceptParametersInWakeUpSMS_CheckedChanged(object sender, EventArgs e)
        {
            int a = combo_NumberProfile_UniqueID.SelectedIndex;
            if (radio_modem_special.Checked)
            {
                a = 4;
            }
            if (a < 0)
            {
                combo_NumberProfile_UniqueID.SelectedIndex = 0;
                a = 0;
            }
            Param_Number_Profile_object[a].Accept_Paramaeters_In_Wake_Up_SMS_FLAG_4 = check_NumberProfile_AcceptParametersInWakeUpSMS.Checked;

        }

        private void check_NumberProfile__RejectCall_CheckedChanged(object sender, EventArgs e)
        {
            int a = combo_NumberProfile_UniqueID.SelectedIndex;
            if (radio_modem_special.Checked)
            {
                a = 4;
            }
            if (a < 0)
            {
                combo_NumberProfile_UniqueID.SelectedIndex = 0;
                a = 0;
            }
            Param_Number_Profile_object[a].Reject_Call_FLAG_1 = check_NumberProfile_RejectCall.Checked;

        }

        private void check_NumberProfile__WakeupOnVoiceCall_CheckedChanged(object sender, EventArgs e)
        {
            int a = combo_NumberProfile_UniqueID.SelectedIndex;
            if (radio_modem_special.Checked)
            {
                a = 4;
            }
            if (a < 0)
            {
                combo_NumberProfile_UniqueID.SelectedIndex = 0;
                a = 0;
            }
            Param_Number_Profile_object[a].Wakup_On_Voice_Call_FLAG_5 = check_NumberProfile_WakeupOnVoiceCall.Checked;

        }

        private void check_NumberProfile__Rejectwithattend_CheckedChanged(object sender, EventArgs e)
        {
            int a = combo_NumberProfile_UniqueID.SelectedIndex;
            if (radio_modem_special.Checked)
            {
                a = 4;
            }
            if (a < 0)
            {
                combo_NumberProfile_UniqueID.SelectedIndex = 0;
                a = 0;
            }
            Param_Number_Profile_object[a].Reject_With_Attend_FLAG_2 = check_NumberProfile_Rejectwithattend.Checked;

        }

        private void check_NumberProfile__Allow2WaySMSCommunication_CheckedChanged(object sender, EventArgs e)
        {
            int a = combo_NumberProfile_UniqueID.SelectedIndex;
            if (radio_modem_special.Checked)
            {
                a = 4;
            }
            if (a < 0)
            {
                combo_NumberProfile_UniqueID.SelectedIndex = 0;
                a = 0;
            }
            Param_Number_Profile_object[a].Allow_2way_SMS_communication_FLAG_6 = check_NumberProfile_Allow2WaySMSCommunication.Checked;

        }

        private void check_NumberProfile__AcceptDataCall_CheckedChanged(object sender, EventArgs e)
        {
            int a = combo_NumberProfile_UniqueID.SelectedIndex;
            if (radio_modem_special.Checked)
            {
                a = 4;
            }
            if (a < 0)
            {
                combo_NumberProfile_UniqueID.SelectedIndex = 0;
                a = 0;
            }
            Param_Number_Profile_object[a].Accept_Data_Call_FLAG_7 = check_NumberProfile_AcceptDataCall.Checked;

        }

        private void check_NumberProfile_VerifyPassword_CheckedChanged_1(object sender, EventArgs e)
        {

            int a = combo_NumberProfile_UniqueID.SelectedIndex;
            if (radio_modem_special.Checked)
            {
                a = 4;
            }
            if (a < 0)
            {
                combo_NumberProfile_UniqueID.SelectedIndex = 0;
                a = 0;
            }
            Param_Number_Profile_object[a].Verify_Password_FLAG_0 = check_NumberProfile_VerifyPassword.Checked;

        }

        private void check_NumberProfile_WakeUpOnSMS_CheckedChanged(object sender, EventArgs e)
        {

            int a = combo_NumberProfile_UniqueID.SelectedIndex;
            if (radio_modem_special.Checked)
            {
                a = 4;
            }
            if (a < 0)
            {
                combo_NumberProfile_UniqueID.SelectedIndex = 0;
                a = 0;
            }
            Param_Number_Profile_object[a].Wakeup_On_SMS_FLAG_3 = check_NumberProfile_WakeUpOnSMS.Checked;


        }

        private void check_NumberProfile_AcceptParametersInWakeUpSMS_CheckedChanged(object sender, EventArgs e)
        {
            int a = combo_NumberProfile_UniqueID.SelectedIndex;
            if (radio_modem_special.Checked)
            {
                a = 4;
            }
            if (a < 0)
            {
                combo_NumberProfile_UniqueID.SelectedIndex = 0;
                a = 0;
            }
            Param_Number_Profile_object[a].Accept_Paramaeters_In_Wake_Up_SMS_FLAG_4 = check_NumberProfile_AcceptParametersInWakeUpSMS.Checked;

        }

        private void check_NumberProfile_WakeUpOnSMS_FLAG_CheckedChanged(object sender, EventArgs e)
        {
            int a = combo_NumberProfile_UniqueID.SelectedIndex;
            if (a < 0)
            {
                combo_NumberProfile_UniqueID.SelectedIndex = 0;
                a = 0;
            }
            //Param_Number_Profile_object[a].Wakeup_On_SMS_FLAG_3 = check_NumberProfile_WakeUpOnSMS_FLAG.Checked;
        }

        #endregion

        private void radio_modem_special_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radio_modem_special.Checked)
                {
                    try
                    {
                        txt_NumberProfile_Number.Enabled = false;
                        combo_NumberProfile_TotalProfiles.Enabled = false;
                        combo_NumberProfile_UniqueID.Enabled = false;
                        int ID = 4;
                        String DataCallNumber = LocalCommon.ConvertToValidString(Param_Number_Profile_object[ID].Datacall_Number);
                        String Number = LocalCommon.ConvertToValidString(Param_Number_Profile_object[ID].Number);

                        #region Mob_Number

                        if (App_Validation.Validate_IntlPhoneNumberFormat(Number))
                            txt_NumberProfile_Number.Text = Number;
                        else
                        {
                            txt_NumberProfile_Number.Text = Param_Number_ProfileHelper.DefaultMobileNumber;
                            Param_Number_Profile_object[ID].Number = LocalCommon.ConvertFromValidNumberString(Param_Number_ProfileHelper.DefaultMobileNumber);
                        }

                        #endregion
                        #region DataCallNumber

                        if (App_Validation.Validate_IntlPhoneNumberFormat(DataCallNumber))
                            txt_NumberProfile_DataCallNumber.Text = DataCallNumber;
                        else
                        {
                            txt_NumberProfile_DataCallNumber.Text = Param_Number_ProfileHelper.DefaultMobileNumber;
                            Param_Number_Profile_object[ID].Datacall_Number = LocalCommon.ConvertFromValidNumberString(Param_Number_ProfileHelper.DefaultMobileNumber);
                        }

                        #endregion

                        combo_NumberProfile_VoiceCall.SelectedIndex = Param_Number_Profile_object[ID].Wake_Up_On_Voice_Call - 1;
                        combo_NumberProfile_SMS.SelectedIndex = Param_Number_Profile_object[ID].Wake_Up_On_SMS - 1;

                        check_NumberProfile_AcceptDataCall.Checked = Param_Number_Profile_object[ID].Accept_Data_Call_FLAG_7;
                        check_NumberProfile_AcceptParametersInWakeUpSMS.Checked = Param_Number_Profile_object[ID].Accept_Paramaeters_In_Wake_Up_SMS_FLAG_4;
                        check_NumberProfile_Allow2WaySMSCommunication.Checked = Param_Number_Profile_object[ID].Allow_2way_SMS_communication_FLAG_6;
                        check_NumberProfile_RejectCall.Checked = Param_Number_Profile_object[ID].Reject_Call_FLAG_1;
                        check_NumberProfile_Rejectwithattend.Checked = Param_Number_Profile_object[ID].Reject_With_Attend_FLAG_2;
                        check_NumberProfile_VerifyPassword.Checked = Param_Number_Profile_object[ID].Verify_Password_FLAG_0;
                        check_NumberProfile_WakeUpOnSMS.Checked = Param_Number_Profile_object[ID].Wakeup_On_SMS_FLAG_3;
                        check_NumberProfile_WakeupOnVoiceCall.Checked = Param_Number_Profile_object[ID].Wakup_On_Voice_Call_FLAG_5;
                    }
                    catch (Exception)
                    {
                        Notification notifier = new Notification("Enter Number Profile Params again if required", String.Empty, 5000);
                    }
                }
                if (radio_modem_normal.Checked)
                {
                    combo_NumberProfile_TotalProfiles.Enabled = true;
                    combo_NumberProfile_UniqueID.Enabled = true;
                    txt_NumberProfile_Number.Enabled = true;
                    showToGUI_NumberProfile();
                }

                //String ErrorMessage = String.Empty;
                //bool IsValidated = false;
                //IsValidated = App_Validation.Validate_Param_Number_Profile(_Param_WakeUp_ProfileHelperObj.Param_WakeUp_Profile_object,
                //    _Param_Number_Profile_object, ref ErrorMessage);
                //if (!IsValidated)
                //{
                //    Notification Notifier = new Notification("Inconsistent Number Profile Object", ErrorMessage, 5000);
                //}

            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Inconsistent Number Profile Object", ex.Message, 5000);
            }
        }

        public void showToGUI_NumberProfile()
        {
            try
            {
                Deattach_Handlers();

                radio_modem_normal.Checked = true;
                int i = 0;
                int total_NumPro_Count = Param_Number_ProfileHelperObj.Total_Number_Profile;
                i = total_NumPro_Count;
                if (i < 1)
                    throw new Exception("Zero Number Profile Defined");
                combo_NumberProfile_TotalProfiles.SelectedIndex = i - 1;
                if (total_NumPro_Count != combo_NumberProfile_UniqueID.Items.Count)
                {
                    combo_NumberProfile_UniqueID.Items.Clear();
                    for (int Item = 1; (Item <= total_NumPro_Count &&
                                      Item <= Param_Number_ProfileHelper.Max_Number_Profile); Item++)
                    {
                        combo_NumberProfile_UniqueID.Items.Add(Item);
                    }
                }
                //Param_Number_ProfileHelperObj.Previous_Total_number_profiles = i - 1;
                int ID = combo_NumberProfile_UniqueID.SelectedIndex;
                //txt_WakeUpProfile_TotalIDs_SelectedIndexChanged(this, new EventArgs());
                if (ID == -1 || ID + 1 > total_NumPro_Count)
                {
                    ID = 0;
                    combo_NumberProfile_UniqueID.SelectedIndex = 0;
                }
                txt_NumberProfile_DataCallNumber.Text = LocalCommon.ConvertToValidString(Param_Number_Profile_object[ID].Datacall_Number);
                txt_NumberProfile_Number.Text = LocalCommon.ConvertToValidString(Param_Number_Profile_object[ID].Number);

                combo_NumberProfile_VoiceCall.SelectedIndex = Param_Number_Profile_object[ID].Wake_Up_On_Voice_Call - 1;
                combo_NumberProfile_SMS.SelectedIndex = Param_Number_Profile_object[ID].Wake_Up_On_SMS - 1;

                check_NumberProfile_AcceptDataCall.Checked = Param_Number_Profile_object[ID].Accept_Data_Call_FLAG_7;
                check_NumberProfile_AcceptParametersInWakeUpSMS.Checked = Param_Number_Profile_object[ID].Accept_Paramaeters_In_Wake_Up_SMS_FLAG_4;
                check_NumberProfile_Allow2WaySMSCommunication.Checked = Param_Number_Profile_object[ID].Allow_2way_SMS_communication_FLAG_6;
                check_NumberProfile_RejectCall.Checked = Param_Number_Profile_object[ID].Reject_Call_FLAG_1;
                check_NumberProfile_Rejectwithattend.Checked = Param_Number_Profile_object[ID].Reject_With_Attend_FLAG_2;
                check_NumberProfile_VerifyPassword.Checked = Param_Number_Profile_object[ID].Verify_Password_FLAG_0;
                check_NumberProfile_WakeUpOnSMS.Checked = Param_Number_Profile_object[ID].Wakeup_On_SMS_FLAG_3;
                check_NumberProfile_WakeupOnVoiceCall.Checked = Param_Number_Profile_object[ID].Wakup_On_Voice_Call_FLAG_5;

                if (Param_Number_Profile_object[ID].FLAG2 < 2)
                    combo_NumberProfile_WakeupType.SelectedIndex = Param_Number_Profile_object[ID].FLAG2;
                else
                    combo_NumberProfile_WakeupType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error show  ParamNumberProfile", ex.Message, 5000);
            }
            finally
            {
                if (Param_Number_ProfileHelperObj != null && combo_NumberProfile_TotalProfiles.SelectedIndex != -1)
                    Param_Number_ProfileHelperObj.Previous_Total_number_profiles = combo_NumberProfile_TotalProfiles.SelectedIndex;
                //Param_Number_ProfileHelperObj.Previous_Total_number_profiles = Param_Number_ProfileHelperObj.Total_Number_Profile;
                //Modem_Warnings_disable = false;
                Attach_Handlers();
            }
            //NOTE:defined as byte; but CHeckBOX on GUI..some flag conversion expected!!
        }

        internal void Attach_Handlers()
        {
            try
            {
                combo_NumberProfile_TotalProfiles.SelectedIndexChanged += combo_NumberProfile_TotalProfiles_SelectedIndexChanged;
                combo_NumberProfile_UniqueID.SelectedIndexChanged += combo_NumberProfile_UniqueID_SelectedIndexChanged;
                combo_NumberProfile_VoiceCall.SelectedIndexChanged += combo_NumberProfile_VoiceCall_SelectedIndexChanged;
                combo_NumberProfile_SMS.SelectedIndexChanged += combo_NumberProfile_SMS_SelectedIndexChanged;
                combo_NumberProfile_WakeupType.SelectedIndexChanged += combo_NumberProfile_WakeupType_SelectedIndexChanged;
                //Number Profile txt Leaver Events
                txt_NumberProfile_Number.Leave += txt_NumberProfile_Number_Leave;
                txt_NumberProfile_DataCallNumber.Leave += txt_NumberProfile_DataCallNumber_Leave;
                //Chk Changed Event Handlers
                check_NumberProfile_VerifyPassword.CheckedChanged += check_NumberProfile_VerifyPassword_CheckedChanged;
                check_NumberProfile_AcceptParametersInWakeUpSMS.CheckedChanged += check_NumberProfile__AcceptParametersInWakeUpSMS_CheckedChanged;
                check_NumberProfile_Rejectwithattend.CheckedChanged += check_NumberProfile__RejectCall_CheckedChanged;
                check_NumberProfile_WakeupOnVoiceCall.CheckedChanged += check_NumberProfile__WakeupOnVoiceCall_CheckedChanged;
                check_NumberProfile_Rejectwithattend.CheckedChanged += check_NumberProfile__Rejectwithattend_CheckedChanged;
                check_NumberProfile_Allow2WaySMSCommunication.CheckedChanged += check_NumberProfile__Allow2WaySMSCommunication_CheckedChanged;
                check_NumberProfile_AcceptDataCall.CheckedChanged += check_NumberProfile__AcceptDataCall_CheckedChanged;
                check_NumberProfile_VerifyPassword.CheckedChanged += check_NumberProfile_VerifyPassword_CheckedChanged_1;
                check_NumberProfile_WakeUpOnSMS.CheckedChanged += check_NumberProfile_WakeUpOnSMS_CheckedChanged;
                check_NumberProfile_AcceptParametersInWakeUpSMS.CheckedChanged += check_NumberProfile_AcceptParametersInWakeUpSMS_CheckedChanged;
                //check_NumberProfile_WakeUpOnSMS_FLAG.CheckedChanged += check_NumberProfile_WakeUpOnSMS_FLAG_CheckedChanged;
            }
            catch { throw; }
        }

        internal void Deattach_Handlers()
        {
            try
            {
                combo_NumberProfile_TotalProfiles.SelectedIndexChanged -= combo_NumberProfile_TotalProfiles_SelectedIndexChanged;
                combo_NumberProfile_UniqueID.SelectedIndexChanged -= combo_NumberProfile_UniqueID_SelectedIndexChanged;
                combo_NumberProfile_VoiceCall.SelectedIndexChanged -= combo_NumberProfile_VoiceCall_SelectedIndexChanged;
                combo_NumberProfile_SMS.SelectedIndexChanged -= combo_NumberProfile_SMS_SelectedIndexChanged;
                combo_NumberProfile_WakeupType.SelectedIndexChanged -= combo_NumberProfile_WakeupType_SelectedIndexChanged;
                //Number Profile txt Leaver Events
                txt_NumberProfile_Number.Leave -= txt_NumberProfile_Number_Leave;
                txt_NumberProfile_DataCallNumber.Leave -= txt_NumberProfile_DataCallNumber_Leave;
                //Chk Changed Event Handlers
                check_NumberProfile_VerifyPassword.CheckedChanged -= check_NumberProfile_VerifyPassword_CheckedChanged;
                check_NumberProfile_AcceptParametersInWakeUpSMS.CheckedChanged -= check_NumberProfile__AcceptParametersInWakeUpSMS_CheckedChanged;
                check_NumberProfile_Rejectwithattend.CheckedChanged -= check_NumberProfile__RejectCall_CheckedChanged;
                check_NumberProfile_WakeupOnVoiceCall.CheckedChanged -= check_NumberProfile__WakeupOnVoiceCall_CheckedChanged;
                check_NumberProfile_Rejectwithattend.CheckedChanged -= check_NumberProfile__Rejectwithattend_CheckedChanged;
                check_NumberProfile_Allow2WaySMSCommunication.CheckedChanged -= check_NumberProfile__Allow2WaySMSCommunication_CheckedChanged;
                check_NumberProfile_AcceptDataCall.CheckedChanged -= check_NumberProfile__AcceptDataCall_CheckedChanged;
                check_NumberProfile_VerifyPassword.CheckedChanged -= check_NumberProfile_VerifyPassword_CheckedChanged_1;
                check_NumberProfile_WakeUpOnSMS.CheckedChanged -= check_NumberProfile_WakeUpOnSMS_CheckedChanged;
                check_NumberProfile_AcceptParametersInWakeUpSMS.CheckedChanged -= check_NumberProfile_AcceptParametersInWakeUpSMS_CheckedChanged;
                //check_NumberProfile_WakeUpOnSMS_FLAG.CheckedChanged -= check_NumberProfile_WakeUpOnSMS_FLAG_CheckedChanged;
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
                        _HelperAccessRights((NumberProfiles)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
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

        private void _HelperAccessRights(NumberProfiles qty, bool read, bool write)
        {
            switch (qty)
            {
                case NumberProfiles.TotalNumberProfiles:
                    combo_NumberProfile_TotalProfiles.Enabled = write;
                    fLP_TotalNumProf.Visible = (read || write);
                    break;
                case NumberProfiles.ID:
                    fLP_UniqueID.Visible = read;
                    break;
                case NumberProfiles.WakeUpOnSMS:
                    combo_NumberProfile_SMS.Enabled = write;
                    fLP_WakeupSMS.Visible = (read || write);
                    break;
                case NumberProfiles.WakeUpOnVoiceCall:
                    combo_NumberProfile_VoiceCall.Enabled = write;
                    fLP_WK_VoiceCall.Visible = (read || write);
                    break;
                case NumberProfiles.WakeUpType:
                    combo_NumberProfile_WakeupType.Enabled = write;
                    fLP_WakeupType.Visible = read || write;
                    break;
                case NumberProfiles.Number:
                    txt_NumberProfile_Number.ReadOnly = !write;
                    fLP_NumberProfile.Visible = read || write;
                    break;
                case NumberProfiles.DataCallNumber:
                    txt_NumberProfile_DataCallNumber.ReadOnly = !write;
                    fLP_DataCallNumber.Visible = read || write;
                    break;
                //ucNumberProfile Special Flags
                case NumberProfiles.VerifyPasswordflag:
                    check_NumberProfile_VerifyPassword.Enabled = write;
                    check_NumberProfile_VerifyPassword.Visible = read || write;
                    break;
                case NumberProfiles.WakeUpOnSMSflag:
                    check_NumberProfile_WakeUpOnSMS.Enabled = write;
                    check_NumberProfile_WakeUpOnSMS.Visible = read || write;
                    break;
                case NumberProfiles.WakeUpOnVoiceCallflag:
                    check_NumberProfile_WakeupOnVoiceCall.Enabled = write;
                    check_NumberProfile_WakeupOnVoiceCall.Visible = read || write;
                    break;
                case NumberProfiles.AcceptParametersinSMsflag:
                    check_NumberProfile_AcceptParametersInWakeUpSMS.Enabled = write;
                    check_NumberProfile_AcceptParametersInWakeUpSMS.Visible = read || write;
                    break;
                case NumberProfiles.Allow2waySMSCommunicationflag:
                    check_NumberProfile_Allow2WaySMSCommunication.Enabled = write;
                    check_NumberProfile_Allow2WaySMSCommunication.Visible = read || write;
                    break;
                case NumberProfiles.Rejectwithattendflag:
                    check_NumberProfile_Rejectwithattend.Enabled = write;
                    check_NumberProfile_Rejectwithattend.Visible = read || write;
                    break;
                case NumberProfiles.RejectCallflag:
                    check_NumberProfile_RejectCall.Enabled = write;
                    check_NumberProfile_RejectCall.Visible = read || write;
                    break;
                case NumberProfiles.AcceptDataflag:
                    check_NumberProfile_AcceptDataCall.Enabled = write;
                    check_NumberProfile_AcceptDataCall.Visible = read || write;
                    break;
                case NumberProfiles.Regular_Anonymous:
                    pnl_Regular.Enabled = write;
                    pnl_Regular.Visible = read || write;
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