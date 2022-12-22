using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using SharedCode.Comm.HelperClasses;
using SharedCode.Common;
using SharedCode.Comm.DataContainer;
using SEAC.Common;
using SharedCode.Comm.Param;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucCustomerReference : UserControl
    {
        private Param_Customer_Code _Param_customer_code_object = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_Customer_Code Param_customer_code_object
        {
            get { return _Param_customer_code_object; }
            set { _Param_customer_code_object = value; }
        }

        public bool IsValidated
        {
            get
            {
                if (errorProvider != null)
                {
                    String ErrorMessage = null;
                    foreach (Control itemCtr in gpCustomerReference.Controls)
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

        public ucCustomerReference()
        {
            InitializeComponent();
        }

        private void ucCustomerReference_Load(object sender, EventArgs e)
        {
            if (_Param_customer_code_object == null)
                _Param_customer_code_object = new Param_Customer_Code();
            if (errorProvider != null)
            {
                foreach (Control itemCtr in gpCustomerReference.Controls)
                {
                    if (itemCtr.GetType() == typeof(TextBox) ||
                        itemCtr.GetType() == typeof(ComboBox))
                        errorProvider.SetIconAlignment(itemCtr, ErrorIconAlignment.MiddleLeft);
                }
                errorProvider.SetIconAlignment(this, ErrorIconAlignment.MiddleRight);
                errorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            }
        }

        #region CustomerReference_LeaveEvent

        private void txt_CustomerRef_CustomerCode_Leave(object sender, EventArgs e)
        {
            /////verification and validation of customer reference
            //Param_customer_code_object.Customer_Code_String = txt_CustomerRef_CustomerCode.Text;
            //while (Param_customer_code_object.Customer_Code_String.Length < 16)
            //    Param_customer_code_object.Customer_Code_String += '\0';
            String ErrorMessage = String.Empty;
            bool isValidated = false;
            try
            {
                if (txt_CustomerRef_CustomerCode.Text == "")
                    txt_CustomerRef_CustomerCode.Text = "000000";
                String _Customer_Code_String = txt_CustomerRef_CustomerCode.Text;
                App_Validation_Info ValidationInfo = LocalCommon.AppValidationInfo;
                #region ///verification and validation of customer reference

                isValidated = App_Validation.Validate_TxtLength_WithPaddingChar(ValidationInfo.CustomerCodeLength_Min,
                            ValidationInfo.CustomerCodeLength_Max, _Customer_Code_String, "\0");
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid format:Param_CustomerCode");
                    ///Special Case For PasswordLength_Min == 0
                    if (ValidationInfo.CustomerCodeLength_Min <= 0 &&
                        (String.IsNullOrEmpty(_Customer_Code_String) ||
                        String.IsNullOrWhiteSpace(_Customer_Code_String)))
                        isValidated = true;
                    else
                        isValidated = false;
                }

                #endregion
                if (isValidated)
                {
                    App_Validation.Apply_ValidationResult(true, String.Empty, txt_CustomerRef_CustomerCode, ref errorProvider);
                    Param_customer_code_object.Customer_Code_String = _Customer_Code_String.PadRight(ValidationInfo.CustomerCodeLength_Max, '\0');
                    Param_customer_code_object.Customer_Name = txt_CustomerName.Text.PadRight(txt_CustomerName.MaxLength, '\0');
                    Param_customer_code_object.Customer_Address = txt_CustomerAddress.Text.PadRight(txt_CustomerAddress.MaxLength, '\0');
                }
                else
                {
                    App_Validation.Apply_ValidationResult(false, ErrorMessage, txt_CustomerRef_CustomerCode, ref errorProvider);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Error occured while Validate_Param_Password,{0}", ex.Message);
                App_Validation.Apply_ValidationResult(false, ErrorMessage, txt_CustomerRef_CustomerCode, ref errorProvider);
            }
        }

        private void gpCustomerReference_Leave(object sender, EventArgs e)
        {
            //_Param_customer_code_object.Customer_Name = txt_CustomerName.Text;
            //_Param_customer_code_object.Customer_Address = txt_CustomerAddress.Text;
            Param_customer_code_object.Customer_Name = txt_CustomerName.Text.PadRight(16, '\0');
            Param_customer_code_object.Customer_Address = txt_CustomerAddress.Text.PadRight(16, '\0');
        }

        #endregion

        public void showToGUI_CustomerReference()
        {
            try
            {
                if(Param_customer_code_object != null &&  Param_customer_code_object.Customer_Code_String != null )
                    txt_CustomerRef_CustomerCode.Text   = (Param_customer_code_object.Customer_Code_String).ToString();
                if (Param_customer_code_object != null && Param_customer_code_object.Customer_Name != null)
                    txt_CustomerName.Text               = (Param_customer_code_object.Customer_Name).ToString();
                if (Param_customer_code_object != null && Param_customer_code_object.Customer_Address != null)
                    txt_CustomerAddress.Text            = (Param_customer_code_object.Customer_Address).ToString();
                ///txt_CustomerRef_CustomerCode.Focus();
            }
            catch (Exception ex)
            {
                Notification Notification = new Notification("Error showToGUI_CustomerReference", ex.Message);
            }
        }

        private void ucCustomerReference_Leave(object sender, EventArgs e)
        {
            String ErrorMessage = String.Empty;
            bool IsValidated = false;
            try
            {
                IsValidated = App_Validation.Validate_Param_Customer_Code(Param_customer_code_object, ref ErrorMessage);
                if (IsValidated)
                {
                    App_Validation.Apply_ValidationResult(true, String.Empty, this, ref errorProvider);
                }
                else
                {
                    App_Validation.Apply_ValidationResult(false, ErrorMessage, this, ref errorProvider);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Error occurred while Validate_Param_Password,{0}", ex.Message);
                App_Validation.Apply_ValidationResult(false, ErrorMessage, this, ref errorProvider);
            }
        }

        #region AccessControlMethods

        public bool ApplyAccessRights(List<AccessRights> Rights)
        {
            bool isSuccess = false;
            try
            {
                this.SuspendLayout();
                var AccessRight = Rights.Find((x)=>  String.Equals(x.QuantityName,Misc.CustomerReference.ToString(),StringComparison.OrdinalIgnoreCase));


                if (AccessRight != null &&  (AccessRight.Read == true || AccessRight.Write == true))
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
            if(qty == Misc.CustomerReference)
            {
                    //pnl_PowerFail.Enabled = write;
                    txt_CustomerRef_CustomerCode.Visible /*= txt_CustomerName.Visible = txt_CustomerAddress.Visible*/ = (read || write);
                    txt_CustomerRef_CustomerCode.Enabled /*= txt_CustomerName.Enabled = txt_CustomerAddress.Enabled*/ = write;
            }
        }

        #endregion

        private void txt_CustomerRef_CustomerCode_TextChanged(object sender, EventArgs e)
        {
            lblCustCodeLength.Text = "("+txt_CustomerRef_CustomerCode.Text.Length.ToString()+")";
        }

        private void txt_CustomerName_TextChanged(object sender, EventArgs e)
        {
            lblCustNameLength.Text = "(" + txt_CustomerName.Text.Length.ToString() + ")";
        }

        private void txt_CustomerAddress_TextChanged(object sender, EventArgs e)
        {
            lblCustAddressLength.Text = "(" + txt_CustomerAddress.Text.Length.ToString() + ")";
        }

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
