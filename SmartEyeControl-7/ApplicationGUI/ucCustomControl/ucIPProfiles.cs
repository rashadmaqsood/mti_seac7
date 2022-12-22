using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using DLMS;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.DataContainer;
using SharedCode.Common;
using SEAC.Common;
using SharedCode.Comm.Param;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucIPProfiles : UserControl
    {
        #region Data_Members

        private Param_IPV4 _Param_IPV4_object = null;
        private Param_IP_Profiles[] _Param_IP_Profiles_object = null;
        private Param_IP_ProfilesHelper _Param_IP_ProfilesHelper = null;
        private Param_WakeUp_ProfileHelper _Param_WakeUp_ProfileHelperObj = null;
        private bool _Modem_Warnings_disable = false;

        #endregion

        public bool Modem_Warnings_disable
        {
            get { return _Modem_Warnings_disable; }
            set { _Modem_Warnings_disable = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_WakeUp_ProfileHelper Param_WakeUp_ProfileHelperObj
        {
            get { return _Param_WakeUp_ProfileHelperObj; }
            set { _Param_WakeUp_ProfileHelperObj = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_IP_ProfilesHelper Param_IP_ProfilesHelper
        {
            get { return _Param_IP_ProfilesHelper; }
            set { _Param_IP_ProfilesHelper = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_IP_Profiles[] Param_IP_Profiles_object
        {
            get { return _Param_IP_Profiles_object; }
            set
            {
                _Param_IP_Profiles_object = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_IPV4 Param_IPV4_object
        {
            get { return _Param_IPV4_object; }
            set { _Param_IPV4_object = value; }
        }

        public bool IsValidated
        {
            get
            {
                if (errorProvider != null)
                {
                    String ErrorMessage = null;
                    foreach (Control itemCtr in gpIPProfile.Controls)
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

        public ucIPProfiles()
        {
            InitializeComponent();
            //Attach_Handlers();
        }

        private void ucIPProfiles_Load(object sender, EventArgs e)
        {
            ///Init Code
            if (_Param_IPV4_object == null)
                _Param_IPV4_object = new Param_IPV4();
            if (_Param_IP_ProfilesHelper == null)
            {
                _Param_IP_ProfilesHelper = new Param_IP_ProfilesHelper();
                _Param_IP_Profiles_object = _Param_IP_ProfilesHelper.Param_IP_Profiles_object;
            }
            if (_Param_WakeUp_ProfileHelperObj == null)
                _Param_WakeUp_ProfileHelperObj = new Param_WakeUp_ProfileHelper();
            #region Apply_ErrorProvider_Settings

            if (errorProvider != null)
            {
                foreach (Control itemCtr in gpIPProfile.Controls)
                {
                    if (itemCtr.GetType() == typeof(TextBox) ||
                        itemCtr.GetType() == typeof(ComboBox))
                        errorProvider.SetIconAlignment(itemCtr, ErrorIconAlignment.MiddleRight);
                }
                errorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            }

            #endregion
        }

        #region IPV4 leave events

        private void txt_IPV4_DLReference_Leave(object sender, EventArgs e)
        {
            _Param_IPV4_object.DL_reference = DLMS_Common.String_to_Hex_array(txt_IPV4_DLReference.Text);
        }

        private void txt_IPV4_IP_Leave(object sender, EventArgs e)
        {
            try
            {
                IPAddress IPV4_IP = IPAddress.Parse(txt_IPV4_IP.Text);
                _Param_IPV4_object.IP = DLMS_Common.IPAddressToLong(IPV4_IP);
                txt_IPV4_IP.ForeColor = Color.Black;
            }
            catch
            {
                txt_IPV4_IP.ForeColor = Color.Red;
            }
        }

        private void txt_IPV4_SubnetMask_Leave(object sender, EventArgs e)
        {
            try
            {
                IPAddress IPV4_SubnetMask = IPAddress.Parse(txt_IPV4_SubnetMask.Text);
                _Param_IPV4_object.Subnet_Mask = DLMS_Common.IPAddressToLong(IPV4_SubnetMask);
                txt_IPV4_SubnetMask.ForeColor = Color.Black;

            }
            catch
            {
                txt_IPV4_SubnetMask.ForeColor = Color.Red;
            }
        }

        private void txt_IPV4_GatewayIP_Leave(object sender, EventArgs e)
        {
            try
            {
                IPAddress IPV4_GatewayIP = IPAddress.Parse(txt_IPV4_GatewayIP.Text);
                _Param_IPV4_object.Gateway_IP = DLMS_Common.IPAddressToLong(IPV4_GatewayIP);
                txt_IPV4_GatewayIP.ForeColor = Color.Black;

            }
            catch
            {
                txt_IPV4_GatewayIP.ForeColor = Color.Red;
            }
        }

        private void txt_IPV4_PrimaryDNS_Leave(object sender, EventArgs e)
        {
            try
            {
                IPAddress IPV4_PrimaryDNS = IPAddress.Parse(txt_IPV4_PrimaryDNS.Text);
                _Param_IPV4_object.Primary_DNS = DLMS_Common.IPAddressToLong(IPV4_PrimaryDNS);
                txt_IPV4_PrimaryDNS.ForeColor = Color.Black;
            }
            catch
            {
                txt_IPV4_PrimaryDNS.ForeColor = Color.Red;
            }
        }

        private void txt_IPV4_SecondaryDNS_Leave(object sender, EventArgs e)
        {
            try
            {
                IPAddress IPV4_SecondaryDNS = IPAddress.Parse(txt_IPV4_SecondaryDNS.Text);
                _Param_IPV4_object.Secondary_DNS = DLMS_Common.IPAddressToLong(IPV4_SecondaryDNS);
                txt_IPV4_SecondaryDNS.ForeColor = Color.Black;

            }
            catch
            {
                txt_IPV4_IP.ForeColor = Color.Red;
            }
        }

        #endregion

        #region IP_profiles_leave_events

        private void txt_IPProfile_IP_Leave(object sender, EventArgs e)
        {
            try
            {
                byte a = Convert.ToByte(combo_IPProfile_UniqueID.SelectedIndex);
                #region ///Validate IP Profile
                String IPTxt = txt_IPProfile_IP.Text;
                bool isValidated = App_Validation.Validate_IPFormat(IPTxt);
                if (!isValidated)
                {
                    String ErrorMessage = String.Format("Validation failed,InConsistent IP Profile Str {0}"
                        , IPTxt);
                    App_Validation.Apply_ValidationResult(false, ErrorMessage, txt_IPProfile_IP, ref errorProvider);
                    return;
                }
                else
                    App_Validation.Apply_ValidationResult(true, String.Empty, txt_IPProfile_IP, ref errorProvider);

                #endregion
                IPAddress _IPaddress = IPAddress.Parse(txt_IPProfile_IP.Text);
                _Param_IP_Profiles_object[a].IP = DLMS.DLMS_Common.IPAddressToLong(IPAddress.Parse(txt_IPProfile_IP.Text));
            }
            catch
            {
            }
        }

        private void txt_IPProfile_WrapperOverUDP_Leave(object sender, EventArgs e)
        {
            try
            {
                int a = Convert.ToInt16(combo_IPProfile_UniqueID.SelectedIndex);
                ///Validate Wrapper_Over_TCP_port
                bool isValidated = App_Validation.TextBox_RangeValidation((ushort)0, ushort.MaxValue, txt_IPProfile_WrapperOverUDP, ref errorProvider);
                if (isValidated)
                {
                    ///SmartEyeControl_7.Common.Commons.TextBox_validation(0, 65536, txt_IPProfile_WrapperOverUDP);
                    _Param_IP_Profiles_object[a].Wrapper_Over_UDP_port = Convert.ToUInt16(txt_IPProfile_WrapperOverUDP.Text);
                }
            }
            catch
            {
                App_Validation.Apply_ValidationResult(false, "Error Validating", txt_IPProfile_WrapperOverUDP, ref errorProvider);
            }
        }

        private void txt_IPProfile_HDLCOverTCP_Leave(object sender, EventArgs e)
        {
            try
            {
                byte a = Convert.ToByte(combo_IPProfile_UniqueID.SelectedIndex);
                ///Validate HDLC_Over_TCP_Port
                bool isValidated = App_Validation.TextBox_RangeValidation((ushort)0,
                    LocalCommon.AppValidationInfo._Param_IP_Profile_MaxPortNumber, txt_IPProfile_HDLCOverTCP, ref errorProvider);
                if (isValidated)
                {
                    _Param_IP_Profiles_object[a].HDLC_Over_TCP_Port = Convert.ToUInt16(txt_IPProfile_HDLCOverTCP.Text);
                }
            }
            catch
            {
                App_Validation.Apply_ValidationResult(false, "Error Validating", txt_IPProfile_HDLCOverTCP, ref errorProvider);
            }
        }

        private void txt_IPProfile_HDLCOverUDP_Leave(object sender, EventArgs e)
        {
            try
            {
                byte a = Convert.ToByte(combo_IPProfile_UniqueID.SelectedIndex);
                ///Validate txt_IPProfile_HDLCOverUDP
                bool isValidated = App_Validation.TextBox_RangeValidation((ushort)0,
                    LocalCommon.AppValidationInfo._Param_IP_Profile_MaxPortNumber, txt_IPProfile_HDLCOverUDP, ref errorProvider);
                if (isValidated)
                {
                    _Param_IP_Profiles_object[a].HDLC_Over_UDP_Port = Convert.ToUInt16(txt_IPProfile_HDLCOverUDP.Text);
                }
            }
            catch
            {
                App_Validation.Apply_ValidationResult(false, "Error Validating", txt_IPProfile_HDLCOverUDP, ref errorProvider);
            }
        }

        private void txt_IPProfile_WrapperOverTCP_Leave(object sender, EventArgs e)
        {
            try
            {
                byte a = Convert.ToByte(combo_IPProfile_UniqueID.SelectedIndex);
                ///Validate txt_IPProfile_WrapperOverTCP
                bool isValidated = App_Validation.TextBox_RangeValidation((ushort)0,
                    LocalCommon.AppValidationInfo._Param_IP_Profile_MaxPortNumber, txt_IPProfile_WrapperOverTCP, ref errorProvider);
                if (isValidated)
                {
                    _Param_IP_Profiles_object[a].Wrapper_Over_TCP_port = Convert.ToUInt16(txt_IPProfile_WrapperOverTCP.Text);
                }
            }
            catch
            {
                App_Validation.Apply_ValidationResult(false, "Error Validating", txt_IPProfile_WrapperOverTCP, ref errorProvider);
            }
        }

        private void txt_IPProfile_Total_IP_Profiles_Enter(object sender, EventArgs e)
        {
        }

        private void txt_IPProfile_Total_IP_Profiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            //1-- Check the IP dependence
            //2-- Re-initialize the object incremental count
            //3-- Make unused Id = 0
            //4-- Do Cleanup
            try
            {
                if (!Modem_Warnings_disable && txt_IPProfile_Total_IP_Profiles.SelectedIndex != -1
                    && txt_IPProfile_Total_IP_Profiles.SelectedIndex < Param_IP_ProfilesHelper.Previous_Total_IP_profiles)
                {
                    DialogResult DR = MessageBox.Show(this, "Click OK to Continue, Cancel to Exit", "Warning", MessageBoxButtons.OKCancel);
                    if (DR == DialogResult.Cancel)
                    {
                        txt_IPProfile_Total_IP_Profiles.SelectedIndex = Param_IP_ProfilesHelper.Previous_Total_IP_profiles;
                        return;
                    }
                }
                int total_IPProfileDefined = 0;
                total_IPProfileDefined = Param_IP_ProfilesHelper.Total_IP_Profile;
                int selectedIndex = txt_IPProfile_Total_IP_Profiles.SelectedIndex;
                int delta = total_IPProfileDefined - Convert.ToInt32(txt_IPProfile_Total_IP_Profiles.SelectedItem);

                if (selectedIndex > -1)
                {

                    ///Check Either IP_Profiles being Already Assigned
                    int i;
                    if (delta > 0)
                    {
                        foreach (var Param_WakeUp_Profile_object in Param_WakeUp_ProfileHelperObj.Param_WakeUp_Profile_object)
                        {
                            if (Param_WakeUp_Profile_object.Wake_Up_Profile_ID != 0 &&
                                 (Param_WakeUp_Profile_object.IP_Profile_ID_1 > selectedIndex + 1 ||
                             Param_WakeUp_Profile_object.IP_Profile_ID_2 > selectedIndex + 1 ||
                             Param_WakeUp_Profile_object.IP_Profile_ID_3 > selectedIndex + 1 ||
                             Param_WakeUp_Profile_object.IP_Profile_ID_4 > selectedIndex + 1) &&
                             !Modem_Warnings_disable)
                            {
                                txt_IPProfile_Total_IP_Profiles.SelectedIndex = Param_IP_ProfilesHelper.Previous_Total_IP_profiles;
                                throw new Exception("Param IP Profile being removed is already Assigned");
                            }
                        }
                    }

                    //for (i = 0; i < _Param_IP_Profiles_object.Length; i++)
                    //{
                    //    _Param_IP_Profiles_object[i].Unique_ID = (byte)(i + 1);
                    //}
                    ///Remove IP Profiles here
                    if (delta > 0)
                    {
                        i = Math.Abs(delta);
                        while (i > 0)
                        {
                            Param_IP_ProfilesHelper.RemoveParam_IP_Profiles();
                            i--;
                        }
                    }
                    ///Add Param IP Profiles here
                    else if (delta < 0)
                    {
                        i = Math.Abs(delta);
                        while (i > 0)
                        {
                            Param_IP_ProfilesHelper.AddParam_IP_Profiles();
                            i--;
                        }
                    }
                    //Populate Combo IP Profile Unique Id
                    combo_IPProfile_UniqueID.Items.Clear();
                    int total = Convert.ToInt16(txt_IPProfile_Total_IP_Profiles.SelectedIndex + 1);
                    for (i = 1; i <= total; i++)
                    {
                        
                        combo_IPProfile_UniqueID.Items.Add(i);
                    }
                    combo_IPProfile_UniqueID.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Invalid IP Profiles", ex.Message, 10000);
            }
            finally
            {
                ///Record Previouse Selected Index
                if (txt_IPProfile_Total_IP_Profiles.SelectedIndex != -1)
                    Param_IP_ProfilesHelper.Previous_Total_IP_profiles = txt_IPProfile_Total_IP_Profiles.SelectedIndex;
            }
        }

        private void combo_IPProfile_UniqueID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SelectedIndex = combo_IPProfile_UniqueID.SelectedIndex;
            int total_IP_Profile_Count = _Param_IP_ProfilesHelper.Total_IP_Profile;
            int ID = combo_IPProfile_UniqueID.SelectedIndex;
            App_Validation_Info ValidationInfo = LocalCommon.AppValidationInfo;
            #region ///Validate Param_IP_Profile Object Id here

            bool isValidated = App_Validation.Validate_Range((ushort)ValidationInfo._Param_IP_Profile_MinID,
                    (ushort)ValidationInfo._Param_IP_Profile_MaxID, ID);
            if (isValidated || ID == 0 || ID == -1)
                App_Validation.Apply_ValidationResult(true, String.Empty, combo_IPProfile_UniqueID, ref errorProvider);
            else if (!isValidated)
            {
                String ErrorMessage = String.Format("Validation failed,InConsistent IP Profile Ids {0}", ID);
                App_Validation.Apply_ValidationResult(false, ErrorMessage, combo_IPProfile_UniqueID, ref errorProvider);
            }

            #endregion
            if (ID == -1)
            {
                ID = 0;
                combo_IPProfile_UniqueID.SelectedIndex = 0;
                return;
            }
            showToGUI_IPProfile(Param_IP_Profiles_object[ID]);
        }

        #endregion

        public void showToGUI_IPV4()
        {
            txt_IPV4_GatewayIP.Text = DLMS.DLMS_Common.LongToIPAddressString(Param_IPV4_object.Gateway_IP);
            txt_IPV4_PrimaryDNS.Text = DLMS.DLMS_Common.LongToIPAddressString(Param_IPV4_object.Primary_DNS);
            txt_IPV4_SecondaryDNS.Text = DLMS.DLMS_Common.LongToIPAddressString(Param_IPV4_object.Secondary_DNS);
            txt_IPV4_SubnetMask.Text = DLMS.DLMS_Common.LongToIPAddressString(Param_IPV4_object.Subnet_Mask);
            txt_IPV4_IP.Text = DLMS.DLMS_Common.LongToIPAddressString(Param_IPV4_object.IP);
            txt_IPV4_DLReference.Text = DLMS_Common.ArrayToHexString(Param_IPV4_object.DL_reference);
        }

        public void showToGUI_IPProfile()
        {
            try
            {
                Detach_Handlers();

                int i = 0;
                i = _Param_IP_ProfilesHelper.Total_IP_Profile;
                if (i < 1)
                    throw new Exception("Zero IP Profiles Defined");
                if (i != combo_IPProfile_UniqueID.Items.Count)
                {
                    //Populate Combo IP Profile Unique Id
                    combo_IPProfile_UniqueID.Items.Clear();
                    for (int j = 1; j <= i; j++)
                    {
                        combo_IPProfile_UniqueID.Items.Add(j);
                    }
                }
                txt_IPProfile_Total_IP_Profiles.SelectedIndex = i - 1;
                int ID = combo_IPProfile_UniqueID.SelectedIndex;
                if (ID == -1 || (ID + 1) > i)
                {
                    ID = 0; //combo_CommProfile_ProfileID.SelectedIndex = 0; 
                    combo_IPProfile_UniqueID.SelectedIndex = ID;
                }
                showToGUI_IPProfile(Param_IP_Profiles_object[ID]);
            }
            catch (Exception ex)
            {
                Notification nt_Error = new Notification("Error Display IP Profile", "Unable to display Param IPv4 Profile Table " +
                    ex.Message, 5000);
                //nt_Error.Dispose();
            }
            finally
            {
                //Record Previous Selected Index
                if (txt_IPProfile_Total_IP_Profiles.SelectedIndex != -1)
                    Param_IP_ProfilesHelper.Previous_Total_IP_profiles = txt_IPProfile_Total_IP_Profiles.SelectedIndex;

                Attach_Handlers();
            }
        }

        private void showToGUI_IPProfile(Param_IP_Profiles Param_IP_Profiles_object)
        {
            try
            {
                txt_IPProfile_HDLCOverTCP.Text = Convert.ToString(Param_IP_Profiles_object.HDLC_Over_TCP_Port);
                txt_IPProfile_HDLCOverUDP.Text = Convert.ToString(Param_IP_Profiles_object.HDLC_Over_UDP_Port);
                txt_IPProfile_IP.Text = DLMS.DLMS_Common.LongToIPAddressString(Param_IP_Profiles_object.IP);
                txt_IPProfile_WrapperOverTCP.Text = Convert.ToString(Param_IP_Profiles_object.Wrapper_Over_TCP_port);
                txt_IPProfile_WrapperOverUDP.Text = Convert.ToString(Param_IP_Profiles_object.Wrapper_Over_UDP_port);
            }
            catch (Exception ex)
            {
                Notification nt_Error = new Notification("Error Display IP Profile", "Unable to display Param IPv4 Profile Table " +
                    ex.Message, 5000);
                //nt_Error.Dispose();
            }
        }
        internal void Attach_Handlers()
        {
            try
            {
                txt_IPProfile_Total_IP_Profiles.SelectedIndexChanged += txt_IPProfile_Total_IP_Profiles_SelectedIndexChanged;
                combo_IPProfile_UniqueID.SelectedIndexChanged += combo_IPProfile_UniqueID_SelectedIndexChanged;
                combo_IPProfile_UniqueID.Leave += combo_IPProfile_UniqueID_SelectedIndexChanged;
                //Text Controls Leave Events
                txt_IPProfile_IP.Leave += txt_IPProfile_IP_Leave;
                txt_IPProfile_WrapperOverUDP.Leave += txt_IPProfile_WrapperOverUDP_Leave;
                txt_IPProfile_HDLCOverTCP.Leave += txt_IPProfile_HDLCOverTCP_Leave;
                txt_IPProfile_HDLCOverUDP.Leave += txt_IPProfile_HDLCOverUDP_Leave;
                txt_IPProfile_WrapperOverTCP.Leave += txt_IPProfile_WrapperOverTCP_Leave;
            }
            catch
            {
                throw;
            }
        }

        internal void Detach_Handlers()
        {
            try
            {
                txt_IPProfile_Total_IP_Profiles.SelectedIndexChanged -= txt_IPProfile_Total_IP_Profiles_SelectedIndexChanged;
                combo_IPProfile_UniqueID.SelectedIndexChanged -= combo_IPProfile_UniqueID_SelectedIndexChanged;
                combo_IPProfile_UniqueID.Leave -= combo_IPProfile_UniqueID_SelectedIndexChanged;
                //Text Controls Leave Events
                txt_IPProfile_IP.Leave -= txt_IPProfile_IP_Leave;
                txt_IPProfile_WrapperOverUDP.Leave -= txt_IPProfile_WrapperOverUDP_Leave;
                txt_IPProfile_HDLCOverTCP.Leave -= txt_IPProfile_HDLCOverTCP_Leave;
                txt_IPProfile_HDLCOverUDP.Leave -= txt_IPProfile_HDLCOverUDP_Leave;
                txt_IPProfile_WrapperOverTCP.Leave -= txt_IPProfile_WrapperOverTCP_Leave;
            }
            catch
            {
                throw;
            }
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
                        _HelperAccessRights((IPProfiles)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
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

        private void _HelperAccessRights(IPProfiles qty, bool read, bool write)
        {
            switch (qty)
            {
                case IPProfiles.TotalIPProfiles:
                    txt_IPProfile_Total_IP_Profiles.Enabled = write;
                    fLP_TotalIPProfile.Visible = (read || write);
                    break;
                case IPProfiles.ID:
                    fLP_UniqueId.Visible = read;
                    break;
                case IPProfiles.IP:
                    txt_IPProfile_IP.ReadOnly = !write;
                    fLP_IP.Visible = (read || write);
                    break;
                case IPProfiles.WrapperOverTCPPort:
                    txt_IPProfile_WrapperOverTCP.ReadOnly = !write;
                    fLP_TCPPort.Visible = read || write;
                    break;
                case IPProfiles.WrapperOverUDPPort:
                    txt_IPProfile_WrapperOverUDP.ReadOnly = !write;
                    fLP_UDPPort.Visible = read || write;
                    break;
                case IPProfiles.HDLCOverTCPPort:
                    txt_IPProfile_HDLCOverTCP.ReadOnly = !write;
                    fLP_HDLCTCPPort.Visible = read || write;
                    break;
                case IPProfiles.HDLCOverUDPPort:
                    txt_IPProfile_HDLCOverUDP.ReadOnly = !write;
                    fLP_HDLCUDPPort.Visible = read || write;
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
