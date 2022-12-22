using SharedCode.Comm.DataContainer;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.Param;
using SharedCode.Common;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucCommProfile : UserControl
    {
        #region Data_Members

        private Param_Number_ProfileHelper _Param_Number_ProfileHelperObj = null;
        private Param_WakeUp_ProfileHelper _Param_WakeUp_ProfileHelperObj = null;
        private Param_Communication_Profile _Param_Communication_Profile_object = null;
        private bool _Modem_Warnings_disable = false;

        #endregion

        #region Properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_WakeUp_ProfileHelper Param_WakeUp_ProfileHelperObj
        {
            get { return _Param_WakeUp_ProfileHelperObj; }
            set
            {
                _Param_WakeUp_ProfileHelperObj = value;
                ///Register PropertyChannged Notification Event Handler
                _Param_WakeUp_ProfileHelperObj.PropertyChanged -= _Param_Number_ProfileHelperObj_PropertyChanged;
                _Param_WakeUp_ProfileHelperObj.PropertyChanged += _Param_Number_ProfileHelperObj_PropertyChanged;
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
                _Param_Number_ProfileHelperObj.PropertyChanged -= _Param_Number_ProfileHelperObj_PropertyChanged;
                _Param_Number_ProfileHelperObj.PropertyChanged += _Param_Number_ProfileHelperObj_PropertyChanged;
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<AccessRights> AccessRights { get; set; }

        #region Enable/Disable Controls Logic

        private readonly Control[] NumProfile_WT_Controls = null;

        #endregion

        #endregion

        public ucCommProfile()
        {
            InitializeComponent();
            //Attach_Handlers();
            if (errorProvider != null)
            {
                //Set Alignment For Combo Number Profile
                foreach (Control itemCtr in gpNumberProfile.Controls)
                {
                    if (itemCtr.GetType() == typeof(ComboBox))
                        errorProvider.SetIconAlignment(itemCtr, ErrorIconAlignment.MiddleLeft);
                }
                //Set Alignment For Combo Wakeup Id,Protocol,Transport
                foreach (Control itemCtr in gpCommProfile.Controls)
                {
                    if (itemCtr.GetType() == typeof(ComboBox))
                        errorProvider.SetIconAlignment(itemCtr, ErrorIconAlignment.MiddleLeft);
                }
                errorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            }
            AccessRights = ApplicationRight.GetDefaultAccessRightByEnum(typeof(CommunicationProfile));
            NumProfile_WT_Controls = new Control[] { combo_commProfile_NumberProfileID_1, combo_commProfile_NumberProfileID_2,
                                                     combo_commProfile_NumberProfileID_3, combo_commProfile_NumberProfileID_4 };
        }

        void _Param_Number_ProfileHelperObj_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Param_Communication_Profile_object.NumberProfileID == null) //Azeem                
                Param_Communication_Profile_object.NumberProfileID = new byte[4];

            int num_0 = Param_Communication_Profile_object.NumberProfileID[0];
            int num_1 = Param_Communication_Profile_object.NumberProfileID[1];
            int num_2 = Param_Communication_Profile_object.NumberProfileID[2];
            int num_3 = Param_Communication_Profile_object.NumberProfileID[3];
            int wkUP = Param_Communication_Profile_object.WakeUpProfileID;

            ///Param_Wakeup_Profile_object
            if (e.PropertyName.Equals("Param_Wakeup_Profile_object"))
            {
                int total = Param_WakeUp_ProfileHelperObj.Total_Wakeup_Profile;
                ///Update 
                combo_CommProfile_ProfileID.Items.Clear();
                for (int j = 1; j <= total; j++)
                {
                    combo_CommProfile_ProfileID.Items.Add(j);
                }

                try
                {
                    if (Param_Communication_Profile_object.WakeUpProfileID > total)
                        combo_CommProfile_ProfileID.SelectedIndex = 0;
                    else combo_CommProfile_ProfileID.SelectedIndex = wkUP - 1;
                }
                catch (Exception)
                {
                }
            }
            else if (e.PropertyName.Equals("Param_Number_Profile"))
            {
                int total = Param_Number_ProfileHelperObj.Total_Number_Profile;

                //Add programmed number profiles to communicationProfile-> NUmberProfile ID

                //Add total Number Profiles to Wakeup Profile combos
                //First Add "None" to combos

                combo_commProfile_NumberProfileID_1.Items.Clear();
                combo_commProfile_NumberProfileID_2.Items.Clear();
                combo_commProfile_NumberProfileID_3.Items.Clear();
                combo_commProfile_NumberProfileID_4.Items.Clear();

                combo_commProfile_NumberProfileID_1.Items.Add("None");
                combo_commProfile_NumberProfileID_2.Items.Add("None");
                combo_commProfile_NumberProfileID_3.Items.Add("None");
                combo_commProfile_NumberProfileID_4.Items.Add("None");

                for (int j = 1; j <= total; j++)
                {
                    combo_commProfile_NumberProfileID_1.Items.Add(j);
                    combo_commProfile_NumberProfileID_2.Items.Add(j);
                    combo_commProfile_NumberProfileID_3.Items.Add(j);
                    combo_commProfile_NumberProfileID_4.Items.Add(j);
                }

                try
                {
                    //// if the Comm Profiles_NUM[IDS] are lower than number profile selected index then,, DONOT REST Comm Profile
                    if (Param_Communication_Profile_object.NumberProfileID[0] > total ||
                        Param_Communication_Profile_object.NumberProfileID[1] > total ||
                        Param_Communication_Profile_object.NumberProfileID[2] > total ||
                        Param_Communication_Profile_object.NumberProfileID[3] > total)
                    {
                        combo_commProfile_NumberProfileID_1.SelectedIndex = 0;
                        combo_commProfile_NumberProfileID_2.SelectedIndex = 0;
                        combo_commProfile_NumberProfileID_3.SelectedIndex = 0;
                        combo_commProfile_NumberProfileID_4.SelectedIndex = 0;
                    }
                    else
                    {
                        combo_commProfile_NumberProfileID_1.SelectedIndex = num_0;
                        combo_commProfile_NumberProfileID_2.SelectedIndex = num_1;
                        combo_commProfile_NumberProfileID_3.SelectedIndex = num_2;
                        combo_commProfile_NumberProfileID_4.SelectedIndex = num_3;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void ucCommProfile_Load(object sender, EventArgs e)
        {
            ///Init Code
            if (_Param_WakeUp_ProfileHelperObj == null)
            {
                _Param_WakeUp_ProfileHelperObj = new Param_WakeUp_ProfileHelper();
                _Param_WakeUp_ProfileHelperObj.Previous_Total_Wakeup_profiles = 0;
            }
            if (_Param_Number_ProfileHelperObj == null)
                _Param_Number_ProfileHelperObj = new Param_Number_ProfileHelper();
            if (_Param_Communication_Profile_object == null)
                _Param_Communication_Profile_object = new Param_Communication_Profile();
        }

        #region Check_Changed_EventHandlers

        private void check_CommProfile_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!((RadioButton)sender).Checked)
                    return;
                if (check_CommProfile_SMS.Checked)
                {
                    Param_Communication_Profile_object.SelectedMode = 0;
                    combo_commProfile_NumberProfileID_1.SelectedIndex = 0;
                    combo_commProfile_NumberProfileID_2.SelectedIndex = 0;
                    combo_commProfile_NumberProfileID_3.SelectedIndex = 0;
                    combo_commProfile_NumberProfileID_4.SelectedIndex = 0;

                    if (combo_CommProfile_ProfileID.SelectedIndex > -1)
                    {
                        Param_Communication_Profile_object.WakeUpProfileID = Convert.ToByte(combo_CommProfile_ProfileID.Text);
                    }
                    else
                    {
                        if (combo_CommProfile_ProfileID.Items.Count > 0)
                            combo_CommProfile_ProfileID.SelectedIndex = 0;
                        Param_Communication_Profile_object.WakeUpProfileID = Convert.ToByte(combo_CommProfile_ProfileID.Text);
                    }
                }
                else if (check_CommProfile_TCP.Checked)
                {
                    Param_Communication_Profile_object.SelectedMode = 1;

                    if (combo_CommProfile_ProfileID.SelectedIndex > -1)
                    {
                        Param_Communication_Profile_object.WakeUpProfileID = Convert.ToByte(combo_CommProfile_ProfileID.Text);
                    }
                    else
                    {
                        if (combo_CommProfile_ProfileID.Items.Count > 0)
                            combo_CommProfile_ProfileID.SelectedIndex = 0;
                        Param_Communication_Profile_object.WakeUpProfileID = Convert.ToByte(combo_CommProfile_ProfileID.Text);
                    }
                    Param_Communication_Profile_object.NumberProfileID[0] = 0;
                    Param_Communication_Profile_object.NumberProfileID[1] = 0;
                    Param_Communication_Profile_object.NumberProfileID[2] = 0;
                    Param_Communication_Profile_object.NumberProfileID[3] = 0;
                }
                else if (check_CommProfile_NO_event.Checked)
                {
                    Param_Communication_Profile_object.SelectedMode = 2;

                    Param_Communication_Profile_object.WakeUpProfileID = 0;
                    Param_Communication_Profile_object.NumberProfileID[0] = 0;
                    Param_Communication_Profile_object.NumberProfileID[1] = 0;
                    Param_Communication_Profile_object.NumberProfileID[2] = 0;
                    Param_Communication_Profile_object.NumberProfileID[3] = 0;
                }
                showToGUI_CommunicationProfile();
            }
            catch
            {
            }
        }


        private void check_CommProfile_SMS_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (check_CommProfile_SMS.Checked)
                {
                    Param_Communication_Profile_object.SelectedMode = 0;
                    combo_commProfile_NumberProfileID_1.SelectedIndex = 0;
                    combo_commProfile_NumberProfileID_2.SelectedIndex = 0;
                    combo_commProfile_NumberProfileID_3.SelectedIndex = 0;
                    combo_commProfile_NumberProfileID_4.SelectedIndex = 0;

                    combo_commProfile_NumberProfileID_1.Enabled = true;
                    combo_commProfile_NumberProfileID_2.Enabled = true;
                    combo_commProfile_NumberProfileID_3.Enabled = true;
                    combo_commProfile_NumberProfileID_4.Enabled = true;
                    combo_CommProfile_ProfileID.Enabled = false;
                    combo_CommProfile_Protocol.Enabled = false;
                    combo_CommProfile_Transport.Enabled = false;

                    Param_Communication_Profile_object.WakeUpProfileID = 0;
                }
                //***Commented Code Section
                //Param_Communication Profile Validation
                //Notification notifier = null;
                //String ErrorMessage = String.Empty;
                //bool isValidated = App_Validation.Validate_Communication_Profile(_Param_Communication_Profile_object,
                //    Param_Number_ProfileHelperObj.Param_Number_Profiles_object,
                //    Param_WakeUp_ProfileHelperObj.Param_WakeUp_Profile_object, ref ErrorMessage);
                //if (!isValidated)
                //    notifier = new Notification("Error Validating Param Comm Profile", ErrorMessage, 5000);
            }
            catch
            {
            }
        }

        private void check_CommProfile_TCP_CheckedChanged(object sender, EventArgs e)
        {
            if (check_CommProfile_TCP.Checked)
            {
                Param_Communication_Profile_object.SelectedMode = 1;
                combo_commProfile_NumberProfileID_1.Enabled = false;
                combo_commProfile_NumberProfileID_2.Enabled = false;
                combo_commProfile_NumberProfileID_3.Enabled = false;
                combo_commProfile_NumberProfileID_4.Enabled = false;
                combo_CommProfile_ProfileID.Enabled = true;

                //combo_CommProfile_Protocol.Enabled = true;
                //combo_CommProfile_Transport.Enabled = true;

                //if (Param_Communication_Profile_object.WakeUpProfileID > 0)
                //    combo_CommProfile_ProfileID.SelectedIndex = Param_Communication_Profile_object.WakeUpProfileID - 1;
                //combo_CommProfile_Protocol.SelectedIndex = Convert.ToByte(Param_Communication_Profile_object.Protocol_HDLC_TCP_Flag_0);
                //combo_CommProfile_Transport.SelectedIndex = Convert.ToByte(Param_Communication_Profile_object.Protocol_TCP_UDP_Flag_1);

                if (combo_CommProfile_ProfileID.SelectedIndex > -1)
                {
                    Param_Communication_Profile_object.WakeUpProfileID = Convert.ToByte(combo_CommProfile_ProfileID.Text);
                    combo_CommProfile_ProfileID.SelectedIndex = 0;
                    Application.DoEvents();
                }

                Param_Communication_Profile_object.NumberProfileID[0] = 0;
                Param_Communication_Profile_object.NumberProfileID[1] = 0;
                Param_Communication_Profile_object.NumberProfileID[2] = 0;
                Param_Communication_Profile_object.NumberProfileID[3] = 0;
            }
            else
            {
                Param_Communication_Profile_object.WakeUpProfileID = 0;
            }

            ///***Commented Code Section
            ///Param_Communication Profile Validation
            //Notification notifier = null;
            //String ErrorMessage = String.Empty;
            //bool isValidated = App_Validation.Validate_Communication_Profile(_Param_Communication_Profile_object,
            //    Param_Number_ProfileHelperObj.Param_Number_Profiles_object,
            //    Param_WakeUp_ProfileHelperObj.Param_WakeUp_Profile_object, ref ErrorMessage);
            //if (!isValidated)
            //    notifier = new Notification("Error Validating Param Comm Profile", ErrorMessage, 5000);

        }

        private void check_CommProfile_NO_event_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (check_CommProfile_NO_event.Checked)
                {
                    Param_Communication_Profile_object.WakeUpProfileID = 0;

                    Param_Communication_Profile_object.SelectedMode = 2;
                    combo_commProfile_NumberProfileID_1.Enabled = false;
                    combo_commProfile_NumberProfileID_2.Enabled = false;
                    combo_commProfile_NumberProfileID_3.Enabled = false;
                    combo_commProfile_NumberProfileID_4.Enabled = false;
                    combo_CommProfile_ProfileID.Enabled = false;
                    combo_CommProfile_Protocol.Enabled = false;
                    combo_CommProfile_Transport.Enabled = false;

                    Param_Communication_Profile_object.NumberProfileID[0] = 0;
                    Param_Communication_Profile_object.NumberProfileID[1] = 0;
                    Param_Communication_Profile_object.NumberProfileID[2] = 0;
                    Param_Communication_Profile_object.NumberProfileID[3] = 0;

                    combo_commProfile_NumberProfileID_1.SelectedIndex = 0;
                    combo_commProfile_NumberProfileID_2.SelectedIndex = 0;
                    combo_commProfile_NumberProfileID_3.SelectedIndex = 0;
                    combo_commProfile_NumberProfileID_4.SelectedIndex = 0;
                }

                combo_CommProfile_ProfileID.SelectedIndex = 0; //AHMED
                combo_CommProfile_Protocol.SelectedIndex = 1;
                combo_CommProfile_Transport.SelectedIndex = 1;

            }
            catch (Exception)
            {

            }
        }


        private void combo_commProfile_NumberProfileID_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (combo_commProfile_NumberProfileID_1.SelectedIndex != -1)
                {
                    Param_Communication_Profile_object.NumberProfileID[0] = (byte)combo_commProfile_NumberProfileID_1.SelectedIndex;
                }
                byte Num_Profile_Id = Param_Communication_Profile_object.NumberProfileID[0];
                Validate_Number_Profile(Num_Profile_Id, combo_commProfile_NumberProfileID_1);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Select Number Profile Id ParamCommunicationProfile", ex.Message, 5000);
            }
        }

        private void combo_commProfile_NumberProfileID_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (combo_commProfile_NumberProfileID_2.SelectedIndex != -1)
                {
                    Param_Communication_Profile_object.NumberProfileID[1] = (byte)combo_commProfile_NumberProfileID_2.SelectedIndex;
                }
                byte Num_Profile_Id = Param_Communication_Profile_object.NumberProfileID[1];
                Validate_Number_Profile(Num_Profile_Id, combo_commProfile_NumberProfileID_2);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Select Number Profile Id ParamCommunicationProfile", ex.Message, 5000);
            }
        }

        private void combo_commProfile_NumberProfileID_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (combo_commProfile_NumberProfileID_3.SelectedIndex != -1)
                {
                    Param_Communication_Profile_object.NumberProfileID[2] = (byte)combo_commProfile_NumberProfileID_3.SelectedIndex;
                }
                byte Num_Profile_Id = Param_Communication_Profile_object.NumberProfileID[2];
                Validate_Number_Profile(Num_Profile_Id, combo_commProfile_NumberProfileID_3);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Select Number Profile Id ParamCommunicationProfile", ex.Message, 5000);
            }
        }

        private void combo_commProfile_NumberProfileID_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (combo_commProfile_NumberProfileID_4.SelectedIndex != -1)
                {
                    Param_Communication_Profile_object.NumberProfileID[3] = (byte)combo_commProfile_NumberProfileID_4.SelectedIndex;
                }
                byte Num_Profile_Id = Param_Communication_Profile_object.NumberProfileID[3];
                Validate_Number_Profile(Num_Profile_Id, combo_commProfile_NumberProfileID_4);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Select Number Profile Id ParamCommunicationProfile", ex.Message, 5000);
            }
        }

        private void Validate_Number_Profile(byte Num_Profile_Id, ComboBox combo_commProfile_NumberProfileID)
        {
            FieldInfo fInfo = typeof(Param_Number_Profile).GetField("Unique_ID");
            bool isValidated = false;
            String ErrorMessage = String.Empty;
            ///Validate _Param_Communication_Profile_object.NumberProfileID Lookup
            isValidated = App_Validation.Validate_LookupId(fInfo,
                Param_Number_ProfileHelperObj.Param_Number_Profiles_object, Num_Profile_Id);
            if (!isValidated && Num_Profile_Id != 0)
            {
                ErrorMessage = String.Format("Validation failed,InConsistent NumberProfileID {0}", Num_Profile_Id);
                errorProvider.SetError(combo_commProfile_NumberProfileID, ErrorMessage);
            }
            else
                errorProvider.SetError(combo_commProfile_NumberProfileID, String.Empty);
        }

        private void combo_CommProfile_ProfileID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (combo_CommProfile_ProfileID.SelectedIndex != -1 && !check_CommProfile_NO_event.Checked)
                {
                    Param_Communication_Profile_object.WakeUpProfileID = (byte)Convert.ToInt16(combo_CommProfile_ProfileID.Text);
                }
                else
                {
                    Param_Communication_Profile_object.WakeUpProfileID = 0;
                }
                #region Validation Param_Communication_Profile_object.WakeUpProfileID

                String ErrorMessage = String.Empty;
                FieldInfo fInfo = typeof(Param_WakeUp_Profile).GetField("Wake_Up_Profile_ID");
                //Validate _Param_Communication_Profile_object.WakeUpProfileID Lookup
                bool isValidated = App_Validation.Validate_LookupId(fInfo,
                    Param_WakeUp_ProfileHelperObj.Param_WakeUp_Profile_object,
                     _Param_Communication_Profile_object.WakeUpProfileID);
                if (!isValidated ||
                    _Param_Communication_Profile_object.WakeUpProfileID == 0)
                {
                    ErrorMessage = String.Format("Validation failed,InConsistent WakeUpProfileID {0}",
                        _Param_Communication_Profile_object.WakeUpProfileID);
                    errorProvider.SetError(combo_CommProfile_ProfileID, ErrorMessage);
                }
                else
                    errorProvider.SetError(combo_CommProfile_ProfileID, String.Empty);

                #endregion
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Select Communication Wakeup Profile ParamCommunicationProfile", ex.Message, 5000);
            }
        }

        private void combo_CommProfile_Protocol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_CommProfile_Protocol.SelectedIndex != -1)
            {
                Param_Communication_Profile_object.Protocol_HDLC_TCP_Flag_0 = Convert.ToBoolean(combo_CommProfile_Protocol.SelectedIndex);
            }
            else
            {
                return;
            }
        }

        private void combo_CommProfile_Transport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_CommProfile_Transport.SelectedIndex != -1)
            {
                Param_Communication_Profile_object.Protocol_TCP_UDP_Flag_1 = Convert.ToBoolean(combo_CommProfile_Transport.SelectedIndex);
            }
            else
            {
                return;
            }
        }

        #endregion

        public void showToGUI_CommunicationProfile()
        {
            try
            {
                Detach_Handlers();

                this.SuspendLayout();
                this.fLP_Main.SuspendLayout();
                this.fLP_menu_choice.SuspendLayout();

                if (Param_Number_ProfileHelperObj.Total_Number_Profile <= 0)
                    return;

                Param_Number_ProfileHelperObj.OnPropertyChanged(Param_Number_ProfileHelper.PropertyName);
                Param_WakeUp_ProfileHelperObj.OnPropertyChanged(Param_WakeUp_ProfileHelper.PropertyName);

                //Select Param Communication profile Mode
                if (Param_Communication_Profile_object.SelectedMode == 0)
                {
                    check_CommProfile_SMS.Checked = true;
                }
                else if (Param_Communication_Profile_object.SelectedMode == 1)
                {
                    check_CommProfile_TCP.Checked = true;
                }
                else if (Param_Communication_Profile_object.SelectedMode == 2)
                {
                    check_CommProfile_NO_event.Checked = true;
                }
                else
                {
                    //By Default Choose None
                    combo_commProfile_NumberProfileID_1.SelectedIndex = 0;
                    combo_commProfile_NumberProfileID_2.SelectedIndex = 0;
                    combo_commProfile_NumberProfileID_3.SelectedIndex = 0;
                    combo_commProfile_NumberProfileID_4.SelectedIndex = 0;
                }

                //Set Check SMS,TCP_Link,No_Event_Notification Mode
                SetControlEnable(true, check_CommProfile_SMS);
                SetControlEnable(true, check_CommProfile_TCP);
                SetControlEnable(true, check_CommProfile_NO_event);

                SetControlVisible(true, check_CommProfile_SMS);
                SetControlVisible(true, check_CommProfile_TCP);
                SetControlVisible(true, check_CommProfile_NO_event);

                int num_0 = Param_Communication_Profile_object.NumberProfileID[0];
                int num_1 = Param_Communication_Profile_object.NumberProfileID[1];
                int num_2 = Param_Communication_Profile_object.NumberProfileID[2];
                int num_3 = Param_Communication_Profile_object.NumberProfileID[3];
                int wkUP = Param_Communication_Profile_object.WakeUpProfileID;

                //if (Param_Communication_Profile_object.NumberProfileID[0] == 0 &&
                //    Param_Communication_Profile_object.NumberProfileID[1] == 0 &&
                //    Param_Communication_Profile_object.NumberProfileID[2] == 0 &&
                //    Param_Communication_Profile_object.NumberProfileID[3] == 0 &&
                //   wkUP == 0)
                //{
                //    Param_Communication_Profile_object.SelectedMode = 2;
                //    check_CommProfile_NO_event.Checked = true;
                //}
                //else if (Param_Communication_Profile_object.WakeUpProfileID == 0)
                //{
                //    Param_Communication_Profile_object.SelectedMode = 0;
                //    check_CommProfile_SMS.Checked = true;
                //}
                //else
                //{
                //    Param_Communication_Profile_object.SelectedMode = 1;
                //    check_CommProfile_TCP.Checked = true;
                //}

                //Update GUI here
                try
                {
                    combo_commProfile_NumberProfileID_1.SelectedIndex = num_0;
                    combo_commProfile_NumberProfileID_2.SelectedIndex = num_1;
                    combo_commProfile_NumberProfileID_3.SelectedIndex = num_2;
                    combo_commProfile_NumberProfileID_4.SelectedIndex = num_3;

                    //Update Transport TCP_HDLC Protocol
                    if (Param_Communication_Profile_object.Protocol_HDLC_TCP_Flag_0 &&
                        combo_CommProfile_Protocol.Items.Count > 0)
                    {
                        combo_CommProfile_Protocol.SelectedIndex = 1;
                    }
                    else
                        combo_CommProfile_Protocol.SelectedIndex = 0;

                    //Update TCP_HDLC Protocol
                    if (Param_Communication_Profile_object.Protocol_TCP_UDP_Flag_1 &&
                        combo_CommProfile_Transport.Items.Count > 0)
                    {
                        combo_CommProfile_Transport.SelectedIndex = 1;
                    }
                    else
                        combo_CommProfile_Transport.SelectedIndex = 0;

                    //combo_CommProfile_ProfileID.SelectedItem = wkUP - 1; selected item not index
                    combo_CommProfile_ProfileID.SelectedItem = wkUP;
                    //Disable Combo All-Controls
                    combo_commProfile_NumberProfileID_1.Enabled = combo_commProfile_NumberProfileID_2.Enabled =
                    combo_commProfile_NumberProfileID_3.Enabled = combo_commProfile_NumberProfileID_4.Enabled = false;
                    combo_CommProfile_ProfileID.Enabled = false;

                    combo_CommProfile_Protocol.Enabled = false;
                    combo_CommProfile_Transport.Enabled = false;

                    gpNumberProfile.Visible = false;
                    flp_TCPLink.Visible = false;

                    //check_CommProfile_SMS.Checked
                    if (Param_Communication_Profile_object.SelectedMode == 0)
                    {
                        //combo_commProfile_NumberProfileID_1.Enabled = combo_commProfile_NumberProfileID_2.Enabled =
                        //combo_commProfile_NumberProfileID_3.Enabled = combo_commProfile_NumberProfileID_4.Enabled = true;

                        SetControlEnable(true, combo_commProfile_NumberProfileID_1);
                        SetControlEnable(true, combo_commProfile_NumberProfileID_2);
                        SetControlEnable(true, combo_commProfile_NumberProfileID_3);
                        SetControlEnable(true, combo_commProfile_NumberProfileID_4);

                        bool isReadable = IsControlRDEnable(CommunicationProfile.NumberProfileSelection);
                        gpNumberProfile.Visible = isReadable;
                    }
                    //check_CommProfile_TCP
                    else if (Param_Communication_Profile_object.SelectedMode == 1)
                    {
                        //combo_CommProfile_ProfileID.Enabled = true;
                        SetControlEnable(true, combo_CommProfile_ProfileID);
                        SetControlEnable(true, combo_CommProfile_Transport);
                        SetControlEnable(true, combo_CommProfile_Protocol);

                        flp_TCPLink.Visible = true;

                        SetControlVisible(true, combo_CommProfile_ProfileID);
                        SetControlVisible(true, combo_CommProfile_Transport);
                        SetControlVisible(true, combo_CommProfile_Protocol);
                    }
                    //check_CommProfile_NO_event
                    //else if (Param_Communication_Profile_object.SelectedMode == 2) 
                    //{
                    //}

                    if (Param_Communication_Profile_object.Network_Mode != null)
                    {
                        try
                        {
                            cbxNetworkModePriority1.SelectedIndex = Param_Communication_Profile_object.Network_Mode.Priority1;
                        }
                        catch (Exception)
                        {
                            cbxNetworkModePriority1.SelectedIndex = 0;
                        }
                        try
                        {
                            cbxNetworkModePriority2.SelectedIndex = Param_Communication_Profile_object.Network_Mode.Priority2;
                        }
                        catch (Exception)
                        {
                            cbxNetworkModePriority2.SelectedIndex = 0;
                        }
                        try
                        {
                            cbxNetworkModePriority3.SelectedIndex = Param_Communication_Profile_object.Network_Mode.Priority3;
                        }
                        catch (Exception)
                        {
                            cbxNetworkModePriority3.SelectedIndex = 0;
                        }
                        if (Param_Communication_Profile_object.Network_Mode.AutoSelection)
                            rbAutoNetworkMode.Checked = true;
                        else
                            rbManualNetworkMode.Checked = true;
                    }
                }
                catch { }

                Application.DoEvents();
            }
            catch (Exception Ex)
            {
                Notification notifier = new Notification("Error show  ParamCommunicationProfile", Ex.Message, 5000);
            }
            finally
            {
                Attach_Handlers();

                this.fLP_Main.ResumeLayout();
                this.fLP_menu_choice.ResumeLayout();
                this.ResumeLayout();
            }
        }

        internal void Attach_Handlers()
        {
            try
            {
                check_CommProfile_SMS.CheckedChanged += check_CommProfile_CheckedChanged;
                check_CommProfile_TCP.CheckedChanged += check_CommProfile_CheckedChanged;
                check_CommProfile_NO_event.CheckedChanged += check_CommProfile_CheckedChanged;

                //check_CommProfile_SMS.CheckedChanged += check_CommProfile_SMS_CheckedChanged;
                //check_CommProfile_TCP.CheckedChanged += check_CommProfile_TCP_CheckedChanged;
                //check_CommProfile_NO_event.CheckedChanged += check_CommProfile_NO_event_CheckedChanged;

                combo_commProfile_NumberProfileID_1.SelectedIndexChanged += combo_commProfile_NumberProfileID_1_SelectedIndexChanged;
                combo_commProfile_NumberProfileID_2.SelectedIndexChanged += combo_commProfile_NumberProfileID_2_SelectedIndexChanged;
                combo_commProfile_NumberProfileID_3.SelectedIndexChanged += combo_commProfile_NumberProfileID_3_SelectedIndexChanged;
                combo_commProfile_NumberProfileID_4.SelectedIndexChanged += combo_commProfile_NumberProfileID_4_SelectedIndexChanged;

                combo_CommProfile_ProfileID.SelectedIndexChanged += combo_CommProfile_ProfileID_SelectedIndexChanged;
                combo_CommProfile_Protocol.SelectedIndexChanged += combo_CommProfile_Protocol_SelectedIndexChanged;
                combo_CommProfile_Transport.SelectedIndexChanged += combo_CommProfile_Transport_SelectedIndexChanged;
            }
            catch { throw; }
        }

        internal void Detach_Handlers()
        {
            try
            {
                check_CommProfile_SMS.CheckedChanged -= check_CommProfile_CheckedChanged;
                check_CommProfile_TCP.CheckedChanged -= check_CommProfile_CheckedChanged;
                check_CommProfile_NO_event.CheckedChanged -= check_CommProfile_CheckedChanged;

                //check_CommProfile_SMS.CheckedChanged -= check_CommProfile_SMS_CheckedChanged;
                //check_CommProfile_TCP.CheckedChanged -= check_CommProfile_TCP_CheckedChanged;
                //check_CommProfile_NO_event.CheckedChanged -= check_CommProfile_NO_event_CheckedChanged;

                combo_commProfile_NumberProfileID_1.SelectedIndexChanged -= combo_commProfile_NumberProfileID_1_SelectedIndexChanged;
                combo_commProfile_NumberProfileID_2.SelectedIndexChanged -= combo_commProfile_NumberProfileID_2_SelectedIndexChanged;
                combo_commProfile_NumberProfileID_3.SelectedIndexChanged -= combo_commProfile_NumberProfileID_3_SelectedIndexChanged;
                combo_commProfile_NumberProfileID_4.SelectedIndexChanged -= combo_commProfile_NumberProfileID_4_SelectedIndexChanged;

                combo_CommProfile_ProfileID.SelectedIndexChanged -= combo_CommProfile_ProfileID_SelectedIndexChanged;
                combo_CommProfile_Protocol.SelectedIndexChanged -= combo_CommProfile_Protocol_SelectedIndexChanged;
                combo_CommProfile_Transport.SelectedIndexChanged -= combo_CommProfile_Transport_SelectedIndexChanged;
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
                this.fLP_menu_choice.SuspendLayout();

                if (Rights.Find(x => x.Read == true || x.Write == true) != null)
                {
                    this.AccessRights = Rights;
                    //foreach (var item in Rights)
                    //{
                    //    _HelperAccessRights((CommunicationProfile)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
                    //}
                    showToGUI_CommunicationProfile();
                    isSuccess = true;
                }
                else
                    return false;
            }
            finally
            {
                this.fLP_Main.ResumeLayout();
                this.fLP_menu_choice.ResumeLayout();
                this.ResumeLayout();
            }
            return isSuccess;
        }

        private void _HelperAccessRights(CommunicationProfile qty, bool read, bool write)
        {
            switch (qty)
            {
                case CommunicationProfile.SMS_Mode:
                    check_CommProfile_SMS.Enabled = write;
                    check_CommProfile_SMS.Visible = (read || write);
                    break;
                case CommunicationProfile.TCPLink_Mode:
                    check_CommProfile_TCP.Enabled = write;
                    check_CommProfile_TCP.Visible = (read || write);
                    break;
                case CommunicationProfile.NoEventNotification_Mode:
                    check_CommProfile_NO_event.Enabled = write;
                    check_CommProfile_NO_event.Visible = (read || write);
                    break;
                case CommunicationProfile.NumberProfileSelection:
                    SetControlEnable(write, combo_commProfile_NumberProfileID_1);
                    SetControlEnable(write, combo_commProfile_NumberProfileID_2);
                    SetControlEnable(write, combo_commProfile_NumberProfileID_3);
                    SetControlEnable(write, combo_commProfile_NumberProfileID_4);

                    gpNumberProfile.Visible = (read || write);
                    break;
                case CommunicationProfile.WakeUpID:
                    combo_CommProfile_ProfileID.Enabled = write;
                    fLP_WKID.Visible = (read || write);
                    break;
                case CommunicationProfile.Transport:
                    combo_CommProfile_Transport.Enabled = write;
                    fLP_Transport.Visible = (read || write);
                    break;
                case CommunicationProfile.Protocol:
                    combo_CommProfile_Protocol.Enabled = write;
                    fLP_Protocol.Visible = (read || write);
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Support_Fuction

        private void SetControlEnable(bool isEnable, Control Control)
        {
            if (!isEnable)
                Control.Enabled = isEnable;
            else
            {
                //Number Profile Page Control
                if (NumProfile_WT_Controls.Contains<Control>(Control))
                {
                    if (IsControlWTEnable(CommunicationProfile.NumberProfileSelection))
                        Control.Enabled = true;
                }
                //WakeUpProfile Id Control
                else if (combo_CommProfile_ProfileID == Control)
                {
                    if (IsControlWTEnable(CommunicationProfile.WakeUpID))
                        Control.Enabled = true;
                }
                //Transport Control
                else if (combo_CommProfile_Transport == Control)
                {
                    if (IsControlWTEnable(CommunicationProfile.Transport))
                        Control.Enabled = true;
                }
                //combo_CommProfile_Protocol Control
                else if (combo_CommProfile_Protocol == Control)
                {
                    if (IsControlWTEnable(CommunicationProfile.Protocol))
                        Control.Enabled = true;
                }
                //check_CommProfile_SMS Control
                else if (check_CommProfile_SMS == Control)
                {
                    if (IsControlWTEnable(CommunicationProfile.SMS_Mode))
                        Control.Enabled = true;
                }
                //check_CommProfile_NO_event Control
                else if (check_CommProfile_NO_event == Control)
                {
                    if (IsControlWTEnable(CommunicationProfile.NoEventNotification_Mode))
                        Control.Enabled = true;
                }
            }
        }

        private void SetControlVisible(bool isEnable, Control Control)
        {
            //Number Profile Page Control
            if (NumProfile_WT_Controls.Contains<Control>(Control))
            {
                if (IsControlRDEnable(CommunicationProfile.NumberProfileSelection) ||
                    IsControlWTEnable(CommunicationProfile.NumberProfileSelection))
                    Control.Parent.Visible = true;
                else
                    Control.Parent.Visible = false;
            }
            //WakeUpProfile Id Control
            else if (combo_CommProfile_ProfileID == Control)
            {
                if (IsControlRDEnable(CommunicationProfile.WakeUpID) ||
                    IsControlWTEnable(CommunicationProfile.WakeUpID))
                    Control.Parent.Visible = true;
                else
                    Control.Parent.Visible = false;
            }
            //Transport Control
            else if (combo_CommProfile_Transport == Control)
            {
                if (IsControlRDEnable(CommunicationProfile.Transport) ||
                    IsControlWTEnable(CommunicationProfile.Transport))
                    Control.Parent.Visible = true;
                else
                    Control.Parent.Visible = false;
            }
            //combo_CommProfile_Protocol Control
            else if (combo_CommProfile_Protocol == Control)
            {
                if (IsControlRDEnable(CommunicationProfile.Protocol) ||
                    IsControlWTEnable(CommunicationProfile.Protocol))
                    Control.Parent.Visible = true;
                else
                    Control.Parent.Visible = false;
            }
            //check_CommProfile_SMS Control
            else if (check_CommProfile_SMS == Control)
            {
                if (IsControlRDEnable(CommunicationProfile.SMS_Mode) ||
                    IsControlWTEnable(CommunicationProfile.SMS_Mode))
                    Control.Visible = true;
                else
                    Control.Visible = false;
            }
            //check_CommProfile_TCP Control
            else if (check_CommProfile_TCP == Control)
            {
                if (IsControlRDEnable(CommunicationProfile.TCPLink_Mode) ||
                    IsControlWTEnable(CommunicationProfile.TCPLink_Mode))
                    Control.Visible = true;
                else
                    Control.Visible = false;
            }
            //check_CommProfile_NO_event Control
            else if (check_CommProfile_NO_event == Control)
            {
                if (IsControlRDEnable(CommunicationProfile.NoEventNotification_Mode) ||
                    IsControlWTEnable(CommunicationProfile.NoEventNotification_Mode))
                    Control.Visible = true;
                else
                    Control.Visible = false;
            }
        }

        private bool IsControlWTEnable(CommunicationProfile type)
        {
            bool isEnable = false;
            try
            {
                isEnable = ApplicationRight.IsControlWTEnable(typeof(CommunicationProfile), type.ToString(), AccessRights);
            }
            catch { }
            return isEnable;
        }

        private bool IsControlRDEnable(CommunicationProfile type)
        {
            bool isEnable = false;
            try
            {
                isEnable = ApplicationRight.IsControlRDEnable(typeof(CommunicationProfile), type.ToString(), AccessRights);
            }
            catch { }
            return isEnable;
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

        private void rbAutoNetworkMode_CheckedChanged(object sender, EventArgs e)
        {
            Param_Communication_Profile_object.Network_Mode.AutoSelection = rbAutoNetworkMode.Checked;
            cbxNetworkModePriority1.Enabled =
    cbxNetworkModePriority2.Enabled =
    cbxNetworkModePriority3.Enabled = !rbAutoNetworkMode.Checked;
        }

        private void cbxNetworkModePriority1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Param_Communication_Profile_object.Network_Mode.Priority1 = (byte)cbxNetworkModePriority1.SelectedIndex;
        }
        private void cbxNetworkModePriority2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Param_Communication_Profile_object.Network_Mode.Priority2 = (byte)cbxNetworkModePriority2.SelectedIndex;
        }
        private void cbxNetworkModePriority3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Param_Communication_Profile_object.Network_Mode.Priority3 = (byte)cbxNetworkModePriority3.SelectedIndex;
        }

        private void rbManualNetworkMode_CheckedChanged(object sender, EventArgs e)
        {
            if (Param_Communication_Profile_object.Network_Mode == null)
                Param_Communication_Profile_object.Network_Mode = new NetworkModes();
            Param_Communication_Profile_object.Network_Mode.AutoSelection = rbAutoNetworkMode.Checked;
        }
    }
}
