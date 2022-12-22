using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.DataContainer;
using SharedCode.Common;
using SEAC.Common;
using SharedCode.Comm.Param;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucModemInitialize : UserControl
    {
        private Param_Modem_Initialize _Param_Modem_Initialize_Object = null;
        private Param_ModemBasics_NEW _Param_ModemBasics_NEW_object = null;
        private bool _Modem_Warnings_disable = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_Modem_Initialize Param_Modem_Initialize_Object
        {
            get { return _Param_Modem_Initialize_Object; }
            set { _Param_Modem_Initialize_Object = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_ModemBasics_NEW Param_ModemBasics_NEW_object
        {
            get { return _Param_ModemBasics_NEW_object; }
            set { _Param_ModemBasics_NEW_object = value; }
        }

        public bool Modem_Warnings_disable
        {
            get { return _Modem_Warnings_disable; }
            set { _Modem_Warnings_disable = value; }
        }

        private void ucModemInitialize_Load(object sender, EventArgs e)
        {
            if (_Param_Modem_Initialize_Object == null)
                _Param_Modem_Initialize_Object = new Param_Modem_Initialize();
            if (_Param_ModemBasics_NEW_object == null)
                _Param_ModemBasics_NEW_object = new Param_ModemBasics_NEW();
        }

        public ucModemInitialize()
        {
            InitializeComponent();
            if (errorProvider != null)
            {
                foreach (var itemCtr in gpModemInit.Controls)
                {
                    if (itemCtr.GetType() == typeof(TextBox))
                        errorProvider.SetIconAlignment((TextBox)itemCtr, ErrorIconAlignment.MiddleLeft);
                }
                errorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            }
        }

        #region Modem Initialize leave events

        private void txt_ModemInitialize_APNString_Leave(object sender, EventArgs e)
        {

            try
            {
                string txtModemInit_APNStr = txt_ModemInitialize_APNString.Text;
                //if (App_Validation.Validate_URIFormat(txtModemInit_APNStr))
                if (txt_ModemInitialize_APNString.Text.Length < 32)
                {
                    Param_Modem_Initialize_Object.APN = txtModemInit_APNStr;
                    App_Validation.Apply_ValidationResult(true, String.Empty, txt_ModemInitialize_APNString, ref errorProvider);
                    ///txt_ModemInitialize_APNString.ForeColor = Color.Black;
                }
                else
                {
                    String ErrorMessage = String.Format("Validation failed,Invalid Param_Modem_Initialize.APN {0}", Param_Modem_Initialize_Object.APN);
                    App_Validation.Apply_ValidationResult(false, ErrorMessage, txt_ModemInitialize_APNString, ref errorProvider);
                    ///txt_ModemInitialize_APNString.ForeColor = Color.Red;
                }
                ///Notification notifier = null;
                ///***Commented_Code_Section
                //string ErrorMessage = String.Empty;
                //if (!App_Validation.Validate_Param_Modem_Initialize(Param_Modem_Initialize_Object, ref ErrorMessage))
                //{
                //    notifier = new Notification("Error Validating Param_Modem_Initialize_Object", ErrorMessage);
                //}
            }
            catch { }
        }

        private void txt_ModemInitialize_UserName_Leave(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                App_Validation_Info ValidationInfo = LocalCommon.AppValidationInfo;
                ///Validate Username
                String UserName = txt_ModemInitialize_UserName.Text;
                bool isValidated = App_Validation.Validate_TxtLength_WithPaddingChar(ValidationInfo.ModemBasics_Min_PasswordLength1,
                    ValidationInfo.ModemBasics_Max_PasswordLength1, UserName);
                if (!isValidated && !string.IsNullOrEmpty(UserName))
                {
                    string ErrorMessage = String.Format("Validation failed,Invalid Param_ModemBasics_NEW.Username {0}", UserName);
                    App_Validation.Apply_ValidationResult(false, ErrorMessage, txt_ModemInitialize_UserName, ref errorProvider);
                }
                else
                {
                    Param_ModemBasics_NEW_object.UserName = UserName;
                    App_Validation.Apply_ValidationResult(true, String.Empty, txt_ModemInitialize_UserName, ref errorProvider);
                }
                ///***Modified
                //string ErrorMessage = String.Empty;
                //if (!App_Validation.Validate_Param_ModemBasic(Param_ModemBasics_NEW_object, ref ErrorMessage))
                //{
                //    notifier = new Notification("Error Validating Param_ModemBasics_NEW_object", ErrorMessage);
                //}
            }
            catch { }
        }

        private void check_ModemLimitsAndTime_DecrementEventCounter_CheckedChanged(object sender, EventArgs e)
        {
            if (check_ModemLimitsAndTime_DecrementEventCounter.Checked)
                Param_ModemBasics_NEW_object.Flag_DecrementCounter = 1;
            else
                Param_ModemBasics_NEW_object.Flag_DecrementCounter = 0;
        }

        private void check_ModemLimitsAndTime_RLRQ_FLAG_CheckedChanged(object sender, EventArgs e)
        {
            if (check_ModemLimitsAndTime_RLRQ_FLAG.Checked)
                Param_ModemBasics_NEW_object.Flag_RLRQ = 1;
            else
                Param_ModemBasics_NEW_object.Flag_RLRQ = 0;
        }

        private void check_Flag_FastDisconnect_CheckedChanged(object sender, EventArgs e)
        {
            if (check_Flag_FastDisconnect.Checked)
                Param_ModemBasics_NEW_object.Flag_FastDisconnect = 1;
            else
                Param_ModemBasics_NEW_object.Flag_FastDisconnect = 0;
        }

        private void txt_ModemInitialize_Password_Leave(object sender, EventArgs e)
        {
            App_Validation_Info ValidationInfo = LocalCommon.AppValidationInfo;
            ///Validate Password
            String Password = txt_ModemInitialize_Password.Text;
            bool isValidated = App_Validation.Validate_TxtLength_WithPaddingChar(ValidationInfo.ModemBasics_Min_PasswordLength1,
                ValidationInfo.ModemBasics_Max_PasswordLength1, Password);

            if (!isValidated && !string.IsNullOrEmpty(Password))
            {
                string ErrorMessage = String.Format("Validation failed,Invalid Param_ModemBasics_NEW.Password {0}", Password);
                App_Validation.Apply_ValidationResult(false, ErrorMessage, txt_ModemInitialize_Password, ref errorProvider);
            }
            else
            {
                Param_ModemBasics_NEW_object.Password = Password;
                App_Validation.Apply_ValidationResult(true, String.Empty, txt_ModemInitialize_Password, ref errorProvider);
            }
        }

        private void txt_ModemLimitsAndTime_Wakepassword_Leave(object sender, EventArgs e)
        {
            App_Validation_Info ValidationInfo = LocalCommon.AppValidationInfo;
            string wakeupPassword = txt_ModemLimitsAndTime_Wakepassword.Text;
            ///Validate WakeupPassword
            bool isValidated = App_Validation.Validate_TxtLength_WithPaddingChar(ValidationInfo.ModemBasics_Min_PasswordLength1,
                ValidationInfo.ModemBasics_Max_PasswordLength1, wakeupPassword);
            if (!isValidated)
            {
                string ErrorMessage = String.Format("Validation failed,Invalid Param_ModemBasics_NEW.WakeupPassword {0}", wakeupPassword);
                App_Validation.Apply_ValidationResult(false, ErrorMessage, txt_ModemLimitsAndTime_Wakepassword, ref errorProvider);
            }
            else
            {
                Param_ModemBasics_NEW_object.WakeupPassword = wakeupPassword;
                App_Validation.Apply_ValidationResult(true, String.Empty, txt_ModemLimitsAndTime_Wakepassword, ref errorProvider);
            }
        }

        private void txt_ModemInitialize_PinCode_Leave(object sender, EventArgs e)
        {
            String ErrorMessage = String.Empty;
            ushort pin_Code = 0;
            try
            {
                pin_Code = ushort.Parse(txt_ModemInitialize_PinCode.Text);
                if (!App_Validation.Validate_PINCode(pin_Code))
                {
                    ErrorMessage = String.Format("Validation failed,Invalid Param_Modem_Initialize.PIN_code {0}", pin_Code);
                    App_Validation.Apply_ValidationResult(false, ErrorMessage, txt_ModemInitialize_PinCode, ref errorProvider);
                }
                else
                {
                    Param_Modem_Initialize_Object.PIN_code = pin_Code;
                    App_Validation.Apply_ValidationResult(true, String.Empty, txt_ModemInitialize_PinCode, ref errorProvider);
                }
            }
            catch
            {
                ErrorMessage = String.Format("Validation failed,Invalid Param_Modem_Initialize.PIN_code {0}", txt_ModemInitialize_PinCode.Text);
                App_Validation.Apply_ValidationResult(false, ErrorMessage, txt_ModemInitialize_PinCode, ref errorProvider);
            }
        }

        #endregion

        public void showToGUI_ModemInitialize()
        {
            try
            {
                txt_ModemInitialize_APNString.Text = _Param_Modem_Initialize_Object.APN;
                txt_ModemInitialize_UserName.Text = _Param_ModemBasics_NEW_object.UserName.ToString();
                txt_ModemInitialize_Password.Text = _Param_ModemBasics_NEW_object.Password.ToString();
                txt_ModemInitialize_PinCode.Text = _Param_Modem_Initialize_Object.PIN_code.ToString("D4");
                check_ModemLimitsAndTime_RLRQ_FLAG.Checked = Convert.ToBoolean(_Param_ModemBasics_NEW_object.Flag_RLRQ);
                check_Flag_FastDisconnect.Checked = Convert.ToBoolean(_Param_ModemBasics_NEW_object.Flag_FastDisconnect);
                check_ModemLimitsAndTime_DecrementEventCounter.Checked = Convert.ToBoolean(_Param_ModemBasics_NEW_object.Flag_DecrementCounter);
                txt_ModemLimitsAndTime_Wakepassword.Text = _Param_ModemBasics_NEW_object.WakeupPassword.ToString();
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error show ParamModemInitialize", ex.Message, 5000);
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
                        _HelperAccessRights((ModemInitialize)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
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

        private void _HelperAccessRights(ModemInitialize qty, bool read, bool write)
        {
            switch (qty)
            {
                case ModemInitialize.APN:
                    txt_ModemInitialize_APNString.ReadOnly = !write;
                    fLP_APNString.Visible = (read || write);
                    break;
                case ModemInitialize.UserName:
                    txt_ModemInitialize_UserName.ReadOnly = !write;
                    fLP_UserName.Visible = (read || write);
                    break;
                case ModemInitialize.Password:
                    txt_ModemInitialize_Password.ReadOnly = !write;
                    fLP_Password.Visible = (read || write);
                    break;
                case ModemInitialize.WakeUpPassword:
                    txt_ModemLimitsAndTime_Wakepassword.ReadOnly = !write;
                    fLP_WakeupPassword.Visible = (read || write);
                    break;
                case ModemInitialize.PINCode:
                    txt_ModemInitialize_PinCode.ReadOnly = !write;
                    fLP_PINCode.Visible = read || write;
                    break;
                case ModemInitialize.ReleaseAssociationTCPDisconnectflag:
                    check_ModemLimitsAndTime_RLRQ_FLAG.Enabled = write;
                    check_ModemLimitsAndTime_RLRQ_FLAG.Visible = read || write;
                    break;
                case ModemInitialize.DecrementEventCounterflag:
                    check_ModemLimitsAndTime_DecrementEventCounter.Enabled = write;
                    check_ModemLimitsAndTime_DecrementEventCounter.Visible = read || write;
                    break;
                case ModemInitialize.FastDisconnectflag:
                    check_Flag_FastDisconnect.Enabled = write;
                    check_Flag_FastDisconnect.Visible = read || write;
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
