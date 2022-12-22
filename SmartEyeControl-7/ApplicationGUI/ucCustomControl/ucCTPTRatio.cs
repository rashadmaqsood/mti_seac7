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
    public partial class ucCTPTRatio : UserControl
    {
        #region Data_Members

        private Param_CTPT_Ratio _Param_CTPT_ratio_object = null;

        #endregion

        #region Properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_CTPT_Ratio Param_CTPT_ratio_object
        {
            get { return _Param_CTPT_ratio_object; }
            set { _Param_CTPT_ratio_object = value; }
        }

        public bool IsValidated
        {
            get
            {
                if (errorProvider != null)
                {
                    String ErrorMessage = null;
                    foreach (Control itemCtr in gb_CTPTRatio.Controls)
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

        public ucCTPTRatio()
        {
            InitializeComponent();
        }

        private void ucCTPTRatio_Load(object sender, EventArgs e)
        {
            if (_Param_CTPT_ratio_object == null)
                _Param_CTPT_ratio_object = new Param_CTPT_Ratio();
            if (errorProvider != null)
            {
                foreach (Control itemCtr in gb_CTPTRatio.Controls)
                {
                    if (itemCtr.GetType() == typeof(TextBox) ||
                        itemCtr.GetType() == typeof(ComboBox))
                        errorProvider.SetIconAlignment(itemCtr, ErrorIconAlignment.MiddleRight);
                }
                errorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            }
        }

        #region CTPT_ratio_leave_events

        private void txt_CTPT_Ratios_CTRatioNum_Leave(object sender, EventArgs e)
        {
            App_Validation_Info ValidationInfo = LocalCommon.AppValidationInfo;
            if (App_Validation.TextBox_RangeValidation((ushort)0, ValidationInfo.CTratio_Numerator_Max, txt_CTPT_Ratios_CTRatioNum, ref errorProvider))
            {
                Param_CTPT_ratio_object.CTratio_Numerator = Convert.ToUInt16(txt_CTPT_Ratios_CTRatioNum.Text);
            }

        }

        private void txt_CTPT_Ratios_CTRatioDenom_Leave(object sender, EventArgs e)
        {
            App_Validation_Info ValidationInfo = LocalCommon.AppValidationInfo;
            if (App_Validation.TextBox_RangeValidation((ushort)0, ValidationInfo.CTratio_Denominator_Max, txt_CTPT_Ratios_CTRatioDenom, ref errorProvider))
            {
                Param_CTPT_ratio_object.CTratio_Denominator = Convert.ToUInt16(txt_CTPT_Ratios_CTRatioDenom.Text);
            }
        }

        private void txt_CTPT_Ratios_PTRatio_Num_Leave(object sender, EventArgs e)
        {
            App_Validation_Info ValidationInfo = LocalCommon.AppValidationInfo;
            if (App_Validation.TextBox_RangeValidation((ushort)0, ValidationInfo.PTratio_Numerator_Max, txt_CTPT_Ratios_PTRatio_Num, ref errorProvider))
            {
                Param_CTPT_ratio_object.PTratio_Numerator = Convert.ToUInt16(txt_CTPT_Ratios_PTRatio_Num.Text);
            }
        }

        private void txt_CTPT_Ratios_PTRatioDenum_Leave(object sender, EventArgs e)
        {
            App_Validation_Info ValidationInfo = LocalCommon.AppValidationInfo;
            if (App_Validation.TextBox_RangeValidation((ushort)0, ValidationInfo.PTratio_Denominator_Max, txt_CTPT_Ratios_PTRatioDenum, ref errorProvider))
            {
                Param_CTPT_ratio_object.PTratio_Denominator = Convert.ToUInt16(txt_CTPT_Ratios_PTRatioDenum.Text);
            }
        }

        private void gb_CTPTRatio_Leave(object sender, EventArgs e)
        {
            try
            {
                #region Commented_Code_Section

                //if (txt_CTPT_Ratios_CTRatioDenom.ForeColor.Equals(Color.Black) &&
                //                        txt_CTPT_Ratios_CTRatioNum.ForeColor.Equals(Color.Black) &&
                //                        txt_CTPT_Ratios_PTRatio_Num.ForeColor.Equals(Color.Black) &&
                //                        txt_CTPT_Ratios_PTRatioDenum.ForeColor.Equals(Color.Black))
                //{
                //    check_param_CTPT.Enabled = true;

                //    Param_CTPT_ratio_object.CTratio_Numerator = Convert.ToUInt16(txt_CTPT_Ratios_CTRatioNum.Text);
                //    Param_CTPT_ratio_object.CTratio_Denominator = Convert.ToUInt16(txt_CTPT_Ratios_CTRatioDenom.Text);
                //    Param_CTPT_ratio_object.PTratio_Numerator = Convert.ToUInt16(txt_CTPT_Ratios_PTRatio_Num.Text);
                //    Param_CTPT_ratio_object.PTratio_Denominator = Convert.ToUInt16(txt_CTPT_Ratios_PTRatioDenum.Text);
                //}
                //else
                //{
                //    check_param_CTPT.Enabled = false;
                //    check_param_CTPT.Checked = false;
                //} 

                #endregion
                String ErrorMessage = String.Empty;
                if (App_Validation.Validate_Param_CTPT_Ratio(Param_CTPT_ratio_object, ref ErrorMessage))
                {
                    App_Validation.Apply_ValidationResult(true, String.Empty, this, ref errorProvider);
                    Param_CTPT_ratio_object.CTratio_Numerator = Convert.ToUInt16(txt_CTPT_Ratios_CTRatioNum.Text);
                    Param_CTPT_ratio_object.CTratio_Denominator = Convert.ToUInt16(txt_CTPT_Ratios_CTRatioDenom.Text);
                    Param_CTPT_ratio_object.PTratio_Numerator = Convert.ToUInt16(txt_CTPT_Ratios_PTRatio_Num.Text);
                    Param_CTPT_ratio_object.PTratio_Denominator = Convert.ToUInt16(txt_CTPT_Ratios_PTRatioDenum.Text);
                }
                else
                {
                    App_Validation.Apply_ValidationResult(false, ErrorMessage, this, ref errorProvider);
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Save CTPTS ", ex.Message);
            }
        }

        #endregion

        public void showToGUI_CTPTS(bool isSkipRights)
        {
            try
            {
                if (txt_CTPT_Ratios_CTRatioNum.Visible || isSkipRights)
                    txt_CTPT_Ratios_CTRatioNum.Text = LocalCommon.value_to_string(Param_CTPT_ratio_object.CTratio_Numerator);
                if (txt_CTPT_Ratios_CTRatioDenom.Visible || isSkipRights)
                    txt_CTPT_Ratios_CTRatioDenom.Text = LocalCommon.value_to_string(Param_CTPT_ratio_object.CTratio_Denominator);
                if (txt_CTPT_Ratios_PTRatio_Num.Visible || isSkipRights)
                    txt_CTPT_Ratios_PTRatio_Num.Text = LocalCommon.value_to_string(Param_CTPT_ratio_object.PTratio_Numerator);
                if (txt_CTPT_Ratios_PTRatioDenum.Visible || isSkipRights)
                    txt_CTPT_Ratios_PTRatioDenum.Text = LocalCommon.value_to_string(Param_CTPT_ratio_object.PTratio_Denominator);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error showToGUI_CTPTS", ex.Message);
            }
        }

        #region AccessControlMethods

        public bool ApplyAccessRights(List<AccessRights> Rights)
        {
            bool isSuccess = false;
            try
            {
                this.SuspendLayout();
                //var AccessRight = Rights.Find((x)=>  String.Equals(x.QuantityName,Misc.CTPTPatio.ToString(),StringComparison.OrdinalIgnoreCase));

                //if (AccessRight != null &&  (AccessRight.Read == true || AccessRight.Write == true))
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
                    bool isVisible = false;
                    foreach (var item in Rights)
                    {
                        isVisible = _HelperAccessRights((Misc)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);

                        if (!isSuccess && isVisible) isSuccess = true;
                    }
                    //isSuccess = true;
                }
            }
            finally
            {
                this.ResumeLayout();
            }
            return isSuccess;
        }

        private bool _HelperAccessRights(Misc qty, bool read, bool write)
        {
            //if (qty == Misc.CTPTPatio)
            //{
            //    txt_CTPT_Ratios_CTRatioNum.Visible = txt_CTPT_Ratios_CTRatioDenom.Visible = txt_CTPT_Ratios_PTRatio_Num.Visible = 
            //        txt_CTPT_Ratios_PTRatioDenum.Visible = (read || write);

            //    txt_CTPT_Ratios_CTRatioNum.Enabled = txt_CTPT_Ratios_CTRatioDenom.Enabled = txt_CTPT_Ratios_PTRatio_Num.Enabled =
            //        txt_CTPT_Ratios_PTRatioDenum.Enabled = write;
            //}
            bool isVisible = false;
            switch (qty)
            {
                case Misc.CT_Ratio:
                    lbl_CTPT_Ratios_CTRatio.Visible = label29.Visible =
                    txt_CTPT_Ratios_CTRatioNum.Visible = txt_CTPT_Ratios_CTRatioDenom.Visible = isVisible = (read || write);
                    txt_CTPT_Ratios_CTRatioNum.Enabled = txt_CTPT_Ratios_CTRatioDenom.Enabled = write;

                    break;
                case Misc.PT_Ratio:
                    lbl_CTPT_Ratios_PTRatio.Visible = label31.Visible =
                    txt_CTPT_Ratios_PTRatio_Num.Visible = txt_CTPT_Ratios_PTRatioDenum.Visible = isVisible = (read || write);
                    txt_CTPT_Ratios_PTRatio_Num.Enabled = txt_CTPT_Ratios_PTRatioDenum.Enabled = write;
                    break;
                default:
                    break;
            }
            return isVisible;
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
