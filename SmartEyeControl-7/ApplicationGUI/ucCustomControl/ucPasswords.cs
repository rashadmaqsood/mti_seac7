using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SmartEyeControl_7.Common;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.HelperClasses;
using SharedCode.Common;
using SEAC.Common;
using SharedCode.Comm.Param;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucPasswords : UserControl
    {
        private Param_Password _param_password_object = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_Password Param_password_object
        {
            get { return _param_password_object; }
            set { _param_password_object = value; }
        }

        public bool IsValidated
        {
            get
            {
                if (errorProvider != null)
                {
                    String ErrorMessage = null;
                    foreach (Control itemCtr in gpPasswords.Controls)
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

        public ucPasswords()
        {
            InitializeComponent();
        }

        #region Passwords

        private void txt_Passwords_Managerial_Leave(object sender, EventArgs e)
        {
            _param_password_object.Management_Device = txt_Passwords_Managerial.Text;

        }

        private void txt_Passwords_Electrical_Leave(object sender, EventArgs e)
        {
            _param_password_object.Electrical_Device = txt_Passwords_Electrical.Text;

        }

        #region Validation_Code_Password_Feilds

        private void txt_Passwords_Managerial_Leave_1(object sender, EventArgs e)
        {
            try
            {
                ValidateSave_Password((TextBox)sender);
                //_param_password_object.Electrical_Device = _param_password_object.Management_Device;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Password String " + ex.Message, "Error Password");
            }
        }

        private void txt_Passwords_Managerial_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateSave_Password((TextBox)sender);
            }
            catch (Exception ex)
            {

            }
        }

        private void ValidateSave_Password(TextBox txtPassword)
        {
            try
            {
                App_Validation_Info ValidationInfo = LocalCommon.AppValidationInfo;
                String ErrorMessage = String.Empty;

                if (txtPassword == txt_Passwords_Managerial)
                {
                    string txt = txt_Passwords_Managerial.Text;

                    //if (String.IsNullOrEmpty(txt) || txt.Length > 30)
                    //{
                    //    //txt_Passwords_Managerial.Focus();
                    //    txt_Passwords_Managerial.BackColor = Color.Red;
                    //    return;
                    //}
                    //else
                    //{
                    //    txt_Passwords_Managerial.BackColor = Color.White;
                    //    this._param_password_object.Management_Device = txt;
                    //}

                    #region //Validate _Management_Device

                    bool isValidated = App_Validation.Validate_TxtLength_WithPaddingChar(ValidationInfo.PasswordLength_Min,
                                    ValidationInfo.PasswordLength_Max, txt, "\r");
                    if (!isValidated)
                    {
                        ErrorMessage = String.Format("Validation failed,Invalid format:Param_Password");
                        ///Special Case For PasswordLength_Min == 0
                        if (ValidationInfo.PasswordLength_Min <= 0 &&
                            (String.IsNullOrEmpty(txt) ||
                            String.IsNullOrWhiteSpace(txt)))
                            isValidated = true;
                        else
                            isValidated = false;
                    }

                    #endregion

                    if (isValidated)
                    {
                        App_Validation.Apply_ValidationResult(isValidated, String.Empty, txt_Passwords_Managerial, ref errorProvider);
                        this._param_password_object.Management_Device = txt;
                    }
                    else
                        App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, txt_Passwords_Managerial, ref errorProvider);
                }
                else if (txtPassword == txt_Passwords_Electrical)
                {
                    string txt = txt_Passwords_Electrical.Text;

                    ///if (String.IsNullOrEmpty(txt) || txt.Length > 30)
                    ///{
                    ///    //txt_Passwords_Electrical.Focus();
                    ///    txt_Passwords_Electrical.BackColor = Color.Red;
                    ///    return;
                    ///}
                    ///else
                    ///{
                    ///    txt_Passwords_Electrical.BackColor = Color.White;
                    ///    this._param_password_object.Electrical_Device = txt;
                    ///}

                    #region ///Validate Electrical_Device

                    bool isValidated = App_Validation.Validate_TxtLength_WithPaddingChar(ValidationInfo.PasswordLength_Min,
                                    ValidationInfo.PasswordLength_Max, txt, "\r");
                    if (!isValidated)
                    {
                        ErrorMessage = String.Format("Validation failed,Invalid format:Param_Password");
                        ///Special Case For PasswordLength_Min == 0
                        if (ValidationInfo.PasswordLength_Min <= 0 &&
                            (String.IsNullOrEmpty(txt) ||
                            String.IsNullOrWhiteSpace(txt)))
                            isValidated = true;
                        else
                            isValidated = false;
                    }

                    #endregion
                    if (isValidated)
                    {
                        App_Validation.Apply_ValidationResult(isValidated, String.Empty, txt_Passwords_Managerial, ref errorProvider);
                        this._param_password_object._Electrical_Device = txt;
                    }
                    else
                        App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, txt_Passwords_Managerial, ref errorProvider);
                }
            }
            catch (Exception ex)
            {
                if (txtPassword != null)
                {
                    //txtPassword.Focus();
                    ///txtPassword.BackColor = Color.Red;
                    App_Validation.Apply_ValidationResult(false, ex.Message, txtPassword, ref errorProvider);
                }
                throw ex;
            }
        }

        #endregion

        #endregion

        private void ucPasswords_Load(object sender, EventArgs e)
        {
            if (_param_password_object == null)
                _param_password_object = new Param_Password();
            if (errorProvider != null)
            {
                foreach (Control itemCtr in gpPasswords.Controls)
                {
                    if (itemCtr.GetType() == typeof(TextBox) ||
                        itemCtr.GetType() == typeof(ComboBox))
                        errorProvider.SetIconAlignment(itemCtr, ErrorIconAlignment.MiddleLeft);
                }
                errorProvider.SetIconAlignment(this, ErrorIconAlignment.MiddleLeft);
                errorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            }
        }

        public void showToGUI()
        {
            try
            {
                //Modify by Azeem 15102015 // Visiblity Check Added
                if (txt_Passwords_Managerial.Visible)
                    txt_Passwords_Managerial.Text = _param_password_object.Management_Device;

                //modify furqan 22/10/2014
                if(txt_Passwords_Electrical.Visible)
                    txt_Passwords_Electrical.Text = _param_password_object.Electrical_Device;
            }
            catch (Exception ex)
            {
                Notification Notification = new Notification("Error ShowToGUI_ParamPassword", ex.Message);
            }
        }

        private void ucPasswords_Leave(object sender, EventArgs e)
        {
            try
            {
                String ErrorMessage = String.Empty;
                if (App_Validation.Validate_Param_Password(Param_password_object, ref ErrorMessage))
                    App_Validation.Apply_ValidationResult(true, String.Empty, this, ref errorProvider);
                else
                    App_Validation.Apply_ValidationResult(false, ErrorMessage, this, ref errorProvider);
            }
            catch (Exception ex)
            {
                App_Validation.Apply_ValidationResult(false, ex.Message, this, ref errorProvider);
            }
        }


        #region AccessControlMethods

        public bool ApplyAccessRights(List<AccessRights> Rights)
        {
            bool isSuccess = false;
            try
            {
                this.SuspendLayout();
                var AccessRight = Rights.Find((x) => String.Equals(x.QuantityName, Misc.AssociationPassword.ToString(), StringComparison.OrdinalIgnoreCase));

                if (AccessRight != null && (AccessRight.Read == true || AccessRight.Write == true))
                {
                    foreach (var item in Rights)
                    {
                        _HelperAccessRights((Misc)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
                    }
                    isSuccess = true;
                }
                else
                    return false;

            }
            finally
            {
                this.ResumeLayout();
            }
            return isSuccess;
        }

        private void _HelperAccessRights(Misc qty, bool read, bool write)
        {
            if (qty == Misc.AssociationPassword)
            {
                //pnl_PowerFail.Enabled = write;
                txt_Passwords_Managerial.Visible = (read || write); //txt_Passwords_Electrical.Visible = (read || write);
                txt_Passwords_Managerial.Enabled = txt_Passwords_Electrical.Enabled = write;
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
