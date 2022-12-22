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
    public partial class ucDecimalPoint : UserControl
    {
        private Param_Decimal_Point _Param_decimal_point_object = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_Decimal_Point Param_decimal_point_object
        {
            get { return _Param_decimal_point_object; }
            set { _Param_decimal_point_object = value; }
        }

        public bool IsValidated
        {
            get
            {
                if (errorProvider != null)
                {
                    String ErrorMessage = null;
                    foreach (Control itemCtr in gb_DecimalPoint.Controls)
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

        public ucDecimalPoint()
        {
            InitializeComponent();
        }

        private void ucDecimalPoint_Load(object sender, EventArgs e)
        {
            if (_Param_decimal_point_object == null)
                _Param_decimal_point_object = new Param_Decimal_Point();
            if (errorProvider != null)
            {
                foreach (Control itemCtr in gb_DecimalPoint.Controls)
                {
                    if (itemCtr.GetType() == typeof(TextBox) ||
                        itemCtr.GetType() == typeof(ComboBox))
                        errorProvider.SetIconAlignment(itemCtr, ErrorIconAlignment.MiddleLeft);
                }
                errorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            }
        }

        private void txt_DecimalPoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                //&& !char.IsDigit(e.KeyChar)
              && e.KeyChar != '0'
              && e.KeyChar != '.')
            { e.Handled = true; }
            // only allow one decimal point     
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            { e.Handled = true; }
        }

        #region Decimal_point_leave_events

        private void txt_DecimalPoint_BillingKWH_Leave(object sender, EventArgs e)
        {
            try
            {
                App_Validation_Info ValidationInfo = LocalCommon.AppValidationInfo;
                byte returned = LocalCommon.
                    DecimalPoint_validation(txt_DecimalPoint_BillingKWH,
                    ValidationInfo.DecimalPoint_BillingKWH[0],
                    ValidationInfo.DecimalPoint_BillingKWH[1],
                    ValidationInfo.DecimalPoint_BillingKWH[2]);///4, 3, 7);
                if (returned != 0)
                {
                    Param_decimal_point_object.Billing_Energy = returned;
                    App_Validation.Apply_ValidationResult(true, String.Empty, txt_DecimalPoint_BillingKWH, ref errorProvider);
                }
                else
                    App_Validation.Apply_ValidationResult(false,
                        String.Format("Invalid DecimalPoint_BillingKWH Format:{0}", txt_DecimalPoint_BillingKWH.Text),
                        txt_DecimalPoint_BillingKWH, ref errorProvider);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error txt_DecimalPoint_BillingKWH_Leave", ex.Message);
            }
        }

        private void txt_DecimalPoint_BillingKWHMDI_Leave(object sender, EventArgs e)
        {
            try
            {
                App_Validation_Info ValidationInfo = LocalCommon.AppValidationInfo;
                byte returned = LocalCommon.
                    DecimalPoint_validation(txt_DecimalPoint_BillingKWHMDI,
                    ValidationInfo.DecimalPoint_BillingKWHMDI[0],
                    ValidationInfo.DecimalPoint_BillingKWHMDI[1],
                    ValidationInfo.DecimalPoint_BillingKWHMDI[2]);///4, 3, 7);
                if (returned != 0)
                {
                    Param_decimal_point_object.Billing_MDI = returned;
                    App_Validation.Apply_ValidationResult(true, String.Empty, txt_DecimalPoint_BillingKWHMDI, ref errorProvider);
                }
                else
                    App_Validation.Apply_ValidationResult(false,
                        String.Format("Invalid DecimalPoint_BillingKWHMDI Format:{0}", txt_DecimalPoint_BillingKWHMDI.Text),
                        txt_DecimalPoint_BillingKWHMDI, ref errorProvider);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error txt_DecimalPoint_BillingKWHMDI", ex.Message);
            }
        }

        private void txt_DecimalPoint_InstantaneousVoltage_Leave(object sender, EventArgs e)
        {
            try
            {
                App_Validation_Info ValidationInfo = LocalCommon.AppValidationInfo;
                byte returned = LocalCommon.
                    DecimalPoint_validation(txt_DecimalPoint_InstantaneousVoltage,
                    ValidationInfo.DecimalPoint_InstantaneousVoltage[0],
                    ValidationInfo.DecimalPoint_InstantaneousVoltage[1],
                    ValidationInfo.DecimalPoint_InstantaneousVoltage[2]);///3, 2, 5);
                if (returned != 0)
                {
                    Param_decimal_point_object.Instataneous_Voltage = returned;
                    App_Validation.Apply_ValidationResult(true, String.Empty, txt_DecimalPoint_InstantaneousVoltage, ref errorProvider);
                }
                else
                    App_Validation.Apply_ValidationResult(false,
                        String.Format("Invalid DecimalPoint_InstantaneousVoltage Format:{0}", txt_DecimalPoint_InstantaneousVoltage.Text),
                        txt_DecimalPoint_InstantaneousVoltage, ref errorProvider);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error txt_DecimalPoint_InstantaneousVoltage", ex.Message);
            }
        }

        private void txt_DecimalPoint_InstantaneousPower_Leave(object sender, EventArgs e)
        {
            try
            {
                App_Validation_Info ValidationInfo = LocalCommon.AppValidationInfo;
                byte returned = LocalCommon.
                    DecimalPoint_validation(txt_DecimalPoint_InstantaneousPower,
                    ValidationInfo.DecimalPoint_InstantaneousPower[0],
                    ValidationInfo.DecimalPoint_InstantaneousPower[1],
                    ValidationInfo.DecimalPoint_InstantaneousPower[2]);///2, 3, 5);
                if (returned != 0)
                {
                    Param_decimal_point_object.Instataneous_Power = returned;
                    App_Validation.Apply_ValidationResult(true, String.Empty, txt_DecimalPoint_InstantaneousPower, ref errorProvider);
                }
                else
                    App_Validation.Apply_ValidationResult(false,
                        String.Format("Invalid DecimalPoint_InstantaneousPower Format:{0}", txt_DecimalPoint_InstantaneousPower.Text),
                        txt_DecimalPoint_InstantaneousPower, ref errorProvider);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error txt_DecimalPoint_InstantaneousPower", ex.Message);
            }
        }

        private void txt_DecimalPoint_InstantaneousCurrent_Leave(object sender, EventArgs e)
        {
            try
            {
                App_Validation_Info ValidationInfo = LocalCommon.AppValidationInfo;
                byte returned = LocalCommon.
                    DecimalPoint_validation(txt_DecimalPoint_InstantaneousCurrent,
                    ValidationInfo.DecimalPoint_InstantaneousCurrent[0],
                    ValidationInfo.DecimalPoint_InstantaneousCurrent[1],
                    ValidationInfo.DecimalPoint_InstantaneousCurrent[2]);///3, 2, 5);
                if (returned != 0)
                {
                    Param_decimal_point_object.Instataneous_Current = returned;
                    App_Validation.Apply_ValidationResult(true, String.Empty, txt_DecimalPoint_InstantaneousCurrent, ref errorProvider);
                }
                else
                    App_Validation.Apply_ValidationResult(false,
                        String.Format("Invalid DecimalPoint_InstantaneousCurrent Format:{0}", txt_DecimalPoint_InstantaneousCurrent.Text),
                        txt_DecimalPoint_InstantaneousCurrent, ref errorProvider);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error txt_DecimalPoint_InstantaneousCurrent", ex.Message);
            }
        }

        private void txt_DecimalPoint_InstantaneousMDI_Leave(object sender, EventArgs e)
        {
            try
            {
                App_Validation_Info ValidationInfo = LocalCommon.AppValidationInfo;
                byte returned = LocalCommon.
                    DecimalPoint_validation(txt_DecimalPoint_InstantaneousMDI,
                    ValidationInfo.DecimalPoint_InstantaneousMDI[0],
                    ValidationInfo.DecimalPoint_InstantaneousMDI[1],
                    ValidationInfo.DecimalPoint_InstantaneousMDI[2]);/// 2, 3, 5);
                if (returned != 0)
                {
                    Param_decimal_point_object.Instataneous_MDI = returned;
                    App_Validation.Apply_ValidationResult(true, String.Empty, txt_DecimalPoint_InstantaneousMDI, ref errorProvider);
                }
                else
                    App_Validation.Apply_ValidationResult(false,
                        String.Format("Invalid DecimalPoint_InstantaneousMDI Format:{0}", txt_DecimalPoint_InstantaneousMDI.Text),
                        txt_DecimalPoint_InstantaneousMDI, ref errorProvider);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error txt_DecimalPoint_InstantaneousMDI", ex.Message);
            }
        }

        #endregion

        public void showToGUI_DecimalPoint()
        {
            try
            {
                txt_DecimalPoint_BillingKWH.Text = LocalCommon.
                        DecimalPoint_toGUI(Param_decimal_point_object.Billing_Energy);
                txt_DecimalPoint_BillingKWHMDI.Text = LocalCommon.
                    DecimalPoint_toGUI(Param_decimal_point_object.Billing_MDI);
                txt_DecimalPoint_InstantaneousMDI.Text = LocalCommon.
                    DecimalPoint_toGUI(Param_decimal_point_object.Instataneous_MDI);
                txt_DecimalPoint_InstantaneousPower.Text = LocalCommon.
                    DecimalPoint_toGUI(Param_decimal_point_object.Instataneous_Power);
                txt_DecimalPoint_InstantaneousVoltage.Text = LocalCommon.
                    DecimalPoint_toGUI(Param_decimal_point_object.Instataneous_Voltage);
                txt_DecimalPoint_InstantaneousCurrent.Text = LocalCommon.
                    DecimalPoint_toGUI(Param_decimal_point_object.Instataneous_Current);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error showToGUI_DecimalPoint", ex.Message);
            }
        }

        private void ucDecimalPoint_Leave(object sender, EventArgs e)
        {
            try
            {
                bool isValidated = false;
                String ErrorMessage = String.Empty;
                isValidated = App_Validation.Validate_Param_DecimalPoint(_Param_decimal_point_object, ref ErrorMessage);
                if (isValidated)
                    App_Validation.Apply_ValidationResult(true, String.Empty, this, ref errorProvider);
                else
                    App_Validation.Apply_ValidationResult(false, ErrorMessage, this, ref errorProvider);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error txt_DecimalPoint_InstantaneousMDI", ex.Message);
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
                //var AccessRight = Rights.Find((x) => String.Equals(x.QuantityName, Misc.DecimalPoint.ToString(),
                //    StringComparison.OrdinalIgnoreCase));

                //if (AccessRight != null && (AccessRight.Read == true || AccessRight.Write == true))
                //{
                //    foreach (var item in Rights)
                //    {
                //        _HelperAccessRights((Misc)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
                //    }
                //    isSuccess = true;
                //}
                //else
                //    return false;

                if (Rights.Find(x => x.Read == true || x.Write == true) != null)
                {
                    foreach (var item in Rights)
                    {
                        _HelperAccessRights((Misc)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
                    }
                    isSuccess = true;
                }
            }
            finally
            {
                this.ResumeLayout();
            }
            return isSuccess;
        }

        private void _HelperAccessRights(Misc qty, bool read, bool write)
        {
            //if (qty == Misc.DecimalPoint_BillEnergy)
            //{
            //    txt_DecimalPoint_BillingKWH.Visible = txt_DecimalPoint_BillingKWHMDI.Visible =
            //    txt_DecimalPoint_InstantaneousCurrent.Visible = txt_DecimalPoint_InstantaneousMDI.Visible =
            //    txt_DecimalPoint_InstantaneousVoltage.Visible = txt_DecimalPoint_InstantaneousPower.Visible = (read || write);

            //    txt_DecimalPoint_BillingKWH.Enabled = txt_DecimalPoint_BillingKWHMDI.Enabled =
            //    txt_DecimalPoint_InstantaneousCurrent.Enabled = txt_DecimalPoint_InstantaneousMDI.Enabled =
            //    txt_DecimalPoint_InstantaneousVoltage.Enabled = txt_DecimalPoint_InstantaneousPower.Enabled = (write);
            //}

            switch (qty)
            {
                case Misc.DecimalPoint_BillEnergy:
                    lbl_DecimalPoint_BillingKWH.Visible = 
                        txt_DecimalPoint_BillingKWH.Visible = (read || write);
                    txt_DecimalPoint_BillingKWH.Enabled = write;
                    break;
                case Misc.DecimalPoint_BillMdi:
                   lbl_DecimalPoint_BillingKWMDI.Visible = 
                       txt_DecimalPoint_BillingKWHMDI.Visible = (read || write);
                   txt_DecimalPoint_BillingKWHMDI.Enabled = write;
                    break;
                case Misc.DecimalPoint_InstCurrent:
                    lbl_DecimalPoint_InstantaneousCurrent.Visible = 
                        txt_DecimalPoint_InstantaneousCurrent.Visible = (read || write);
                    txt_DecimalPoint_InstantaneousCurrent.Enabled = write;
                    break;
                case Misc.DecimalPoint_InstMdi:
                    lbl_DecimalPoint_InstantaneousMDI.Visible = 
                        txt_DecimalPoint_InstantaneousMDI.Visible = (read || write);
                    txt_DecimalPoint_InstantaneousMDI.Enabled = write;
                    break;
                case Misc.DecimalPoint_InstPower:
                    lbl_DecimalPoint_InstantaneousPower.Visible = 
                        txt_DecimalPoint_InstantaneousPower.Visible = (read || write);
                    txt_DecimalPoint_InstantaneousPower.Enabled = write;
                    break;
                case Misc.DecimalPoint_InstVolt:
                    lbl_DecimalPoint_InstantaneousVoltage.Visible = 
                        txt_DecimalPoint_InstantaneousVoltage.Visible = (read || write);
                    txt_DecimalPoint_InstantaneousVoltage.Enabled = write;
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
