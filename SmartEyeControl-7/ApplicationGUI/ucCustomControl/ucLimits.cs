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
    public partial class ucLimits : UserControl
    {
        #region Data_Members

        private LimitValues limits;
        private Param_Limits _Param_Limits_object;

        private Param_Limit_Demand_OverLoad _Param_Limit_Demand_OverLoad_T1;
        private Param_Limit_Demand_OverLoad _Param_Limit_Demand_OverLoad_T2;
        private Param_Limit_Demand_OverLoad _Param_Limit_Demand_OverLoad_T3;
        private Param_Limit_Demand_OverLoad _Param_Limit_Demand_OverLoad_T4;

        #endregion

        #region Property

        public bool IsValid = false;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Param_Limits Param_Limits_object
        {
            get { return _Param_Limits_object; }
            set { _Param_Limits_object = value; }
        }

        //[Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad_T1
        //{
        //    get { return _Param_Limit_Demand_OverLoad_T1; }
        //    set { _Param_Limit_Demand_OverLoad_T1 = value; }
        //}

        //[Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad_T2
        //{
        //    get { return _Param_Limit_Demand_OverLoad_T2; }
        //    set { _Param_Limit_Demand_OverLoad_T2 = value; }
        //}

        //[Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad_T3
        //{
        //    get { return _Param_Limit_Demand_OverLoad_T3; }
        //    set { _Param_Limit_Demand_OverLoad_T3 = value; }
        //}

        //[Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad_T4
        //{
        //    get { return _Param_Limit_Demand_OverLoad_T4; }
        //    set { _Param_Limit_Demand_OverLoad_T4 = value; }
        //}

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal LimitValues Parameterization_Limits
        {
            get { return limits; }
            set
            {
                limits = value;
                //Apply_LimitValues(limits);
            }
        }

        public bool IsValidated
        {
            get
            {
                if (errorProvider != null)
                {
                    String ErrorMessage = null;
                    foreach (Control itemCtr in gb_Limits.Controls)
                    {
                        if (itemCtr.GetType() == typeof(TextBox))
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

        public void ResetValidationError()
        {
            if (errorProvider != null)
            {
                errorProvider.Clear();
                String ErrorMessage = String.Empty;
                foreach (Control itemCtr in gb_Limits.Controls)
                {
                    if (itemCtr.GetType() == typeof(TextBox))
                    {
                        App_Validation.Apply_ValidationResult(true, String.Empty, itemCtr, errorProvider);
                    }
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<AccessRights> AccessRights { get; set; }

        #endregion

        public ucLimits()
        {
            InitializeComponent();
            pnlMeterOnLoad.Visible =
            pnlOverCurrentPhase.Visible =
            pnlPowerFactorChange.Visible =
            pnlCrestFactorLow.Visible =
            pnlCrestFactorHigh.Visible =
            pnlOverLoad_1p.Visible = !(Commons.IsQc);

            if (Commons.IsQc)
            {
                lbl_CT_Fail.Text = "C.T. Fail (A)";
                //lbl_PT_Fail_V.Text = "P.T. Fail (V)";
                lbl_PT_Fail.Text = "P.T. Fail (A)";
                lbl_limit_high_neutral.Text = "High Neutral Current";
            }
        }
        /// <summary>
        /// Take Access Rights as parameter and apply them over control according to the Rights
        /// </summary>
        /// <param name="Rights"></param>
        public ucLimits(List<AccessRights> Rights, bool IsSinglePhase)
        {
            InitializeComponent();
            AccessRights = Rights;
            ApplyAccessRights(AccessRights, IsSinglePhase);
        }
        #region Helper_Methods

        private void AttachValidation_EventHandlers()
        {
            try
            {
                if (txt_limit_over_volt.Visible || Commons.IsQc)
                {
                    txt_limit_over_volt.Leave += txt_limit_over_volt_Leave;
                    txt_limit_over_volt.TextChanged += txt_limit_over_volt_Leave;
                }
                if (txt_limit_under_volt.Visible || Commons.IsQc)
                {
                    txt_limit_under_volt.Leave += txt_limit_under_volt_Leave;
                    txt_limit_under_volt.TextChanged += txt_limit_under_volt_Leave;
                }
                if (txt_limit_imbalance_volt.Visible || Commons.IsQc)
                {
                    txt_limit_imbalance_volt.Leave += txt_limit_imbalance_volt_Leave;
                    txt_limit_imbalance_volt.TextChanged += txt_limit_imbalance_volt_Leave;
                }
                if (txt_limit_high_neutral_current.Visible || Commons.IsQc)
                {
                    txt_limit_high_neutral_current.Leave += txt_limit_high_neutral_current_Leave;
                    txt_limit_high_neutral_current.TextChanged += txt_limit_high_neutral_current_Leave;
                }
                if (txt_limit_CT_FailAmpLimit.Visible || Commons.IsQc)
                {
                    txt_limit_CT_FailAmpLimit.Leave += txt_limit_CT_FailAmpLimit_Leave;
                    txt_limit_CT_FailAmpLimit.TextChanged += txt_limit_CT_FailAmpLimit_Leave;
                }
                if (txt_limit_PT_FailVoltLimit.Visible || Commons.IsQc)
                {
                    txt_limit_PT_FailVoltLimit.Leave += txt_limit_PT_FailVoltLimit_Leave;
                    txt_limit_PT_FailVoltLimit.TextChanged += txt_limit_PT_FailVoltLimit_Leave;
                }
                if (txt_limit_PT_FailAmpLimit.Visible || Commons.IsQc)
                {
                    txt_limit_PT_FailAmpLimit.Leave += txt_limit_PT_FailAmpLimit_Leave;
                    txt_limit_PT_FailAmpLimit.TextChanged += txt_limit_PT_FailAmpLimit_Leave;
                }
                #region txt_limit_OverCurrentByPhase

                if (txt_limit_OverCurrentByPhase_T1.Visible || Commons.IsQc)
                {
                    txt_limit_OverCurrentByPhase_T1.Leave += txt_limit_OverCurrentByPhase_T1_Leave;
                    txt_limit_OverCurrentByPhase_T1.TextChanged += txt_limit_OverCurrentByPhase_T1_Leave;
                }
                if (txt_limit_OverCurrentByPhase_T2.Visible || Commons.IsQc)
                {
                    txt_limit_OverCurrentByPhase_T2.Leave += txt_limit_OverCurrentByPhase_T2_Leave;
                    txt_limit_OverCurrentByPhase_T2.TextChanged += txt_limit_OverCurrentByPhase_T2_Leave;
                }
                if (txt_limit_OverCurrentByPhase_T3.Visible || Commons.IsQc)
                {
                    txt_limit_OverCurrentByPhase_T3.Leave += txt_limit_OverCurrentByPhase_T3_Leave;
                    txt_limit_OverCurrentByPhase_T3.TextChanged += txt_limit_OverCurrentByPhase_T3_Leave;
                }
                if (txt_limit_OverCurrentByPhase_T4.Visible || Commons.IsQc)
                {
                    txt_limit_OverCurrentByPhase_T4.Leave += txt_limit_OverCurrentByPhase_T4_Leave;
                    txt_limit_OverCurrentByPhase_T4.TextChanged += txt_limit_OverCurrentByPhase_T4_Leave;
                }
                #endregion

                if (txt_limit_reverse_energy.Visible || Commons.IsQc)
                {
                    txt_limit_reverse_energy.Leave += txt_limit_reverse_energy_Leave;
                    txt_limit_reverse_energy.TextChanged += txt_limit_reverse_energy_Leave;
                }
                if (txt_limit_tamperEnergy.Visible || Commons.IsQc)
                {
                    txt_limit_tamperEnergy.Leave += txt_limit_tamperEnergy_Leave;
                    txt_limit_tamperEnergy.TextChanged += txt_limit_tamperEnergy_Leave;
                }

                #region txt_OverLoadByPhase_T
                if (txt_OverLoadByPhase_T1.Visible || Commons.IsQc)
                {
                    txt_OverLoadByPhase_T1.Leave += txt_OverLoadByPhase_T1_Leave;
                    txt_OverLoadByPhase_T1.TextChanged += txt_OverLoadByPhase_T1_Leave;
                }
                if (txt_OverLoadByPhase_T2.Visible || Commons.IsQc)
                {
                    txt_OverLoadByPhase_T2.Leave += txt_OverLoadByPhase_T2_Leave;
                    txt_OverLoadByPhase_T2.TextChanged += txt_OverLoadByPhase_T2_Leave;
                }
                if (txt_OverLoadByPhase_T3.Visible || Commons.IsQc)
                {
                    txt_OverLoadByPhase_T3.Leave += txt_OverLoadByPhase_T3_Leave;
                    txt_OverLoadByPhase_T3.TextChanged += txt_OverLoadByPhase_T3_Leave;
                }
                if (txt_OverLoadByPhase_T4.Visible || Commons.IsQc)
                {
                    txt_OverLoadByPhase_T4.Leave += txt_OverLoadByPhase_T4_Leave;
                    txt_OverLoadByPhase_T4.TextChanged += txt_OverLoadByPhase_T4_Leave;
                }
                #endregion

                #region txt_OverLoadTotal_T
                if (txt_OverLoadTotal_T1.Visible || Commons.IsQc)
                {
                    txt_OverLoadTotal_T1.Leave += txt_OverLoadTotal_T1_Leave;
                    txt_OverLoadTotal_T1.TextChanged += txt_OverLoadTotal_T1_Leave;
                }
                if (txt_OverLoadTotal_T2.Visible || Commons.IsQc)
                {
                    txt_OverLoadTotal_T2.Leave += txt_OverLoadTotal_T2_Leave;
                    txt_OverLoadTotal_T2.TextChanged += txt_OverLoadTotal_T2_Leave;
                }
                if (txt_OverLoadTotal_T3.Visible || Commons.IsQc)
                {
                    txt_OverLoadTotal_T3.Leave += txt_OverLoadTotal_T3_Leave;
                    txt_OverLoadTotal_T3.TextChanged += txt_OverLoadTotal_T3_Leave;
                }
                if (txt_OverLoadTotal_T4.Visible || Commons.IsQc)
                {
                    txt_OverLoadTotal_T4.Leave += txt_OverLoadTotal_T4_Leave;
                    txt_OverLoadTotal_T4.TextChanged += txt_OverLoadTotal_T4_Leave;
                }
                #endregion

                #region txt_DemandOverLoad_T
                if (txt_DemandOverLoad_T1.Visible || Commons.IsQc)
                {
                    txt_DemandOverLoad_T1.Leave += txt_DemandOverLoad_T1_Leave;
                    txt_DemandOverLoad_T1.TextChanged += txt_DemandOverLoad_T1_Leave;
                }
                if (txt_DemandOverLoad_T2.Visible || Commons.IsQc)
                {
                    txt_DemandOverLoad_T2.Leave += txt_DemandOverLoad_T2_Leave;
                    txt_DemandOverLoad_T2.TextChanged += txt_DemandOverLoad_T2_Leave;
                }
                if (txt_DemandOverLoad_T3.Visible || Commons.IsQc)
                {
                    txt_DemandOverLoad_T3.Leave += txt_DemandOverLoad_T3_Leave;
                    txt_DemandOverLoad_T3.TextChanged += txt_DemandOverLoad_T3_Leave;
                }
                if (txt_DemandOverLoad_T4.Visible || Commons.IsQc)
                {
                    txt_DemandOverLoad_T4.Leave += txt_DemandOverLoad_T4_Leave;
                    txt_DemandOverLoad_T4.TextChanged += txt_DemandOverLoad_T4_Leave;
                }
                #endregion


                if (tbOverCurrentPhase.Visible)
                {
                    tbOverCurrentPhase.Leave += tbOverCurrentPhase_Leave;
                    tbOverCurrentPhase.TextChanged += tbOverCurrentPhase_Leave;
                }

                if (tbMeterOnLoad.Visible)
                {
                    tbMeterOnLoad.Leave += tbMeterOnLoad_Leave;
                    tbMeterOnLoad.TextChanged += tbMeterOnLoad_Leave;
                }

                if (tbPowerFactorChange.Visible)
                {
                    tbPowerFactorChange.Leave += tbPowerFactorChange_Leave;
                    tbPowerFactorChange.TextChanged += tbPowerFactorChange_Leave;
                }

                if (tbCrestFactorLow.Visible)
                {
                    tbCrestFactorLow.Leave += tbCrestFactorLow_Leave;
                    tbCrestFactorLow.TextChanged += tbCrestFactorLow_Leave;
                }

                if (tbCrestFactorHigh.Visible )
                {
                    tbCrestFactorHigh.Leave += tbCrestFactorHigh_Leave;
                    tbCrestFactorHigh.TextChanged += tbCrestFactorHigh_Leave;
                }

                if (tbOverLoad_1P.Visible )
                {
                    tbOverLoad_1P.Leave += tbOverLoad_1P_Leave;
                    tbOverLoad_1P.TextChanged += tbOverLoad_1P_Leave;
                }
            }
            finally
            {

            }
        }

        private void tbOverLoad_1P_Leave(object sender, EventArgs e)
        {
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.OverPower, tbOverLoad_1P, errorProvider);
        }

        private void tbCrestFactorHigh_Leave(object sender, EventArgs e)
        {
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.CrestFactorHigh, tbCrestFactorHigh, errorProvider);
        }

        private void tbCrestFactorLow_Leave(object sender, EventArgs e)
        {
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.CrestFactorLow, tbCrestFactorLow, errorProvider);
        }

        private void tbPowerFactorChange_Leave(object sender, EventArgs e)
        {
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.PowerFactor_Change, tbPowerFactorChange, errorProvider);
        }

        private void tbMeterOnLoad_Leave(object sender, EventArgs e)
        {
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.Meter_ON_Load, tbMeterOnLoad, errorProvider);
        }

        private void tbOverCurrentPhase_Leave(object sender, EventArgs e)
        {
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.OverCurrent_Phase, tbOverCurrentPhase, errorProvider);
        }

        private void DeattachValidation_EventHandlers()
        {
            try
            {
                if (txt_limit_over_volt.Visible)
                {
                    txt_limit_over_volt.Leave -= txt_limit_over_volt_Leave;
                    txt_limit_over_volt.TextChanged -= txt_limit_over_volt_Leave;
                }
                if (txt_limit_under_volt.Visible)
                {
                    txt_limit_under_volt.Leave -= txt_limit_under_volt_Leave;
                    txt_limit_under_volt.TextChanged -= txt_limit_under_volt_Leave;
                }
                if (txt_limit_imbalance_volt.Visible)
                {
                    txt_limit_imbalance_volt.Leave -= txt_limit_imbalance_volt_Leave;
                    txt_limit_imbalance_volt.TextChanged -= txt_limit_imbalance_volt_Leave;
                }
                if (txt_limit_high_neutral_current.Visible)
                {
                    txt_limit_high_neutral_current.Leave -= txt_limit_high_neutral_current_Leave;
                    txt_limit_high_neutral_current.TextChanged -= txt_limit_high_neutral_current_Leave;
                }
                if (txt_limit_CT_FailAmpLimit.Visible)
                {
                    txt_limit_CT_FailAmpLimit.Leave -= txt_limit_CT_FailAmpLimit_Leave;
                    txt_limit_CT_FailAmpLimit.TextChanged -= txt_limit_CT_FailAmpLimit_Leave;
                }
                if (txt_limit_PT_FailVoltLimit.Visible)
                {
                    txt_limit_PT_FailVoltLimit.Leave -= txt_limit_PT_FailVoltLimit_Leave;
                    txt_limit_PT_FailVoltLimit.TextChanged -= txt_limit_PT_FailVoltLimit_Leave;
                }
                if (txt_limit_PT_FailAmpLimit.Visible)
                {
                    txt_limit_PT_FailAmpLimit.Leave -= txt_limit_PT_FailAmpLimit_Leave;
                    txt_limit_PT_FailAmpLimit.TextChanged -= txt_limit_PT_FailAmpLimit_Leave;
                }
                #region txt_limit_OverCurrentByPhase

                if (txt_limit_OverCurrentByPhase_T1.Visible)
                {
                    txt_limit_OverCurrentByPhase_T1.Leave -= txt_limit_OverCurrentByPhase_T1_Leave;
                    txt_limit_OverCurrentByPhase_T1.TextChanged -= txt_limit_OverCurrentByPhase_T1_Leave;
                }
                if (txt_limit_OverCurrentByPhase_T2.Visible)
                {
                    txt_limit_OverCurrentByPhase_T2.Leave -= txt_limit_OverCurrentByPhase_T2_Leave;
                    txt_limit_OverCurrentByPhase_T2.TextChanged -= txt_limit_OverCurrentByPhase_T2_Leave;
                }
                if (txt_limit_OverCurrentByPhase_T3.Visible)
                {
                    txt_limit_OverCurrentByPhase_T3.Leave -= txt_limit_OverCurrentByPhase_T3_Leave;
                    txt_limit_OverCurrentByPhase_T3.TextChanged -= txt_limit_OverCurrentByPhase_T3_Leave;
                }
                if (txt_limit_OverCurrentByPhase_T4.Visible)
                {
                    txt_limit_OverCurrentByPhase_T4.Leave -= txt_limit_OverCurrentByPhase_T4_Leave;
                    txt_limit_OverCurrentByPhase_T4.TextChanged -= txt_limit_OverCurrentByPhase_T4_Leave;
                }
                #endregion

                if (txt_limit_reverse_energy.Visible)
                {
                    txt_limit_reverse_energy.Leave -= txt_limit_reverse_energy_Leave;
                    txt_limit_reverse_energy.TextChanged -= txt_limit_reverse_energy_Leave;
                }
                if (txt_limit_tamperEnergy.Visible)
                {
                    txt_limit_tamperEnergy.Leave -= txt_limit_tamperEnergy_Leave;
                    txt_limit_tamperEnergy.TextChanged -= txt_limit_tamperEnergy_Leave;
                }

                #region txt_OverLoadByPhase_T
                if (txt_OverLoadByPhase_T1.Visible)
                {
                    txt_OverLoadByPhase_T1.Leave -= txt_OverLoadByPhase_T1_Leave;
                    txt_OverLoadByPhase_T1.TextChanged -= txt_OverLoadByPhase_T1_Leave;
                }
                if (txt_OverLoadByPhase_T2.Visible)
                {
                    txt_OverLoadByPhase_T2.Leave -= txt_OverLoadByPhase_T2_Leave;
                    txt_OverLoadByPhase_T2.TextChanged -= txt_OverLoadByPhase_T2_Leave;
                }
                if (txt_OverLoadByPhase_T3.Visible)
                {
                    txt_OverLoadByPhase_T3.Leave -= txt_OverLoadByPhase_T3_Leave;
                    txt_OverLoadByPhase_T3.TextChanged -= txt_OverLoadByPhase_T3_Leave;
                }
                if (txt_OverLoadByPhase_T4.Visible)
                {
                    txt_OverLoadByPhase_T4.Leave -= txt_OverLoadByPhase_T4_Leave;
                    txt_OverLoadByPhase_T4.TextChanged -= txt_OverLoadByPhase_T4_Leave;
                }
                #endregion

                #region txt_OverLoadTotal_T
                if (txt_OverLoadTotal_T1.Visible)
                {
                    txt_OverLoadTotal_T1.Leave -= txt_OverLoadTotal_T1_Leave;
                    txt_OverLoadTotal_T1.TextChanged -= txt_OverLoadTotal_T1_Leave;
                }
                if (txt_OverLoadTotal_T2.Visible)
                {
                    txt_OverLoadTotal_T2.Leave -= txt_OverLoadTotal_T2_Leave;
                    txt_OverLoadTotal_T2.TextChanged -= txt_OverLoadTotal_T2_Leave;
                }
                if (txt_OverLoadTotal_T3.Visible)
                {
                    txt_OverLoadTotal_T3.Leave -= txt_OverLoadTotal_T3_Leave;
                    txt_OverLoadTotal_T3.TextChanged -= txt_OverLoadTotal_T3_Leave;
                }
                if (txt_OverLoadTotal_T4.Visible)
                {
                    txt_OverLoadTotal_T4.Leave -= txt_OverLoadTotal_T4_Leave;
                    txt_OverLoadTotal_T4.TextChanged -= txt_OverLoadTotal_T4_Leave;
                }
                #endregion

                #region txt_DemandOverLoad_T
                if (txt_DemandOverLoad_T1.Visible)
                {
                    txt_DemandOverLoad_T1.Leave -= txt_DemandOverLoad_T1_Leave;
                    txt_DemandOverLoad_T1.TextChanged -= txt_DemandOverLoad_T1_Leave;
                }
                if (txt_DemandOverLoad_T2.Visible)
                {
                    txt_DemandOverLoad_T2.Leave -= txt_DemandOverLoad_T2_Leave;
                    txt_DemandOverLoad_T2.TextChanged -= txt_DemandOverLoad_T2_Leave;
                }
                if (txt_DemandOverLoad_T3.Visible)
                {
                    txt_DemandOverLoad_T3.Leave -= txt_DemandOverLoad_T3_Leave;
                    txt_DemandOverLoad_T3.TextChanged -= txt_DemandOverLoad_T3_Leave;
                }
                if (txt_DemandOverLoad_T4.Visible)
                {
                    txt_DemandOverLoad_T4.Leave -= txt_DemandOverLoad_T4_Leave;
                    txt_DemandOverLoad_T4.TextChanged -= txt_DemandOverLoad_T4_Leave;
                }
                #endregion

            }
            finally
            {

            }
        }

        private static bool Validate_Limit_Value(Param_Limits Param_Limits_object, ThreshouldItem MntrThItem,
                    Control Validating_Contrl, ErrorProvider errorProvider)
        {
            bool isValidated = false;
            double val = 0.0d;
            String ErrorMessage = String.Empty;
            try
            {
                val = Convert.ToDouble(Validating_Contrl.Text);
                ErrorMessage = String.Format("Error Range Validating {0},Value {1}", MntrThItem, val);
                Param_Limits_object.SetLimit(MntrThItem, val);
                isValidated = App_Validation.Validate_Param_Limits(Param_Limits_object, MntrThItem, ref ErrorMessage);
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

        //Not Used
        private static bool Validate_Limit_Value(Param_Limit_Demand_OverLoad paramLimitDemandOverLoad, ThreshouldItem MntrThItem,
            Control Validating_Contrl, ErrorProvider errorProvider)
        {
            bool isValidated = false;
            double val = 0.0d;
            String ErrorMessage = String.Empty;
            try
            {
                val = Convert.ToDouble(Validating_Contrl.Text);
                ErrorMessage = String.Format("Error Range Validating {0},Value {1}", MntrThItem, val);
                paramLimitDemandOverLoad.Threshold = val;
                isValidated = App_Validation.Validate_Param_Limits(paramLimitDemandOverLoad, MntrThItem, ref ErrorMessage);
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

        private bool Validate_ParamLimits()
        {
            bool isValidated = false;
            String errorMessage = String.Empty;
            try
            {
                //    ///Validate Param_Limit_Demand_OverLoad
                //    ///Param_Limit_Demand_OverLoad[] Param_Limit_Demand_OverLoads = new Param_Limit_Demand_OverLoad[] { Param_Limit_Demand_OverLoad_T1, 
                //    ///        Param_Limit_Demand_OverLoad_T2, 
                //    ///        Param_Limit_Demand_OverLoad_T3, 
                //    ///        Param_Limit_Demand_OverLoad_T4 };

                //    //Param_Limits_object.DemandOverLoadTotal_T1 = Param_Limit_Demand_OverLoad_T1.Threshold;
                //    //Param_Limits_object.DemandOverLoadTotal_T2 = Param_Limit_Demand_OverLoad_T2.Threshold;
                //    //Param_Limits_object.DemandOverLoadTotal_T3 = Param_Limit_Demand_OverLoad_T3.Threshold;
                //    //Param_Limits_object.DemandOverLoadTotal_T4 = Param_Limit_Demand_OverLoad_T4.Threshold;

                //    lbl_ErrorUCLimits.Visible = false;

                //Commented by Azeem
                isValidated = App_Validation.Validate_Param_Limits(Param_Limits_object, ref errorMessage, Parameterization_Limits);
                //isValidated = App_Validation.Validate_Param_Limits(Param_Limits_object, ref errorMessage, AccessRights, Parameterization_Limits);
                if (!isValidated)
                {
                    //lbl_ErrorUCLimits.Visible = true; //Commented by Azeem  (Accurate)
                    
                    App_Validation.Apply_ValidationResult(isValidated, errorMessage, lbl_ErrorUCLimits, errorProvider);
                }
                else
                    App_Validation.Apply_ValidationResult(isValidated, string.Empty, lbl_ErrorUCLimits, errorProvider);


                ///isValidated = App_Validation.Validate_Param_Limits(Param_Limit_Demand_OverLoads, ref errorMessage, Parameterization_Limits);
                ///if (!isValidated)
                ///{
                ///    lbl_ErrorUCLimits.Visible = true;
                ///    App_Validation.Apply_ValidationResult(isValidated, errorMessage, lbl_ErrorUCLimits, errorProvider);
                ///}
            }
            catch (Exception ex)
            {
                //lbl_ErrorUCLimits.Visible = true; ////Commented by Azeem for Jugaar (Accurate)
                App_Validation.Apply_ValidationResult(isValidated, errorMessage, lbl_ErrorUCLimits, errorProvider);
                Notification notifier = new Notification("Validation Error", ex.Message, 1000, Notification.Sounds.Beep);
            }
            return isValidated;
            //return true;
        }

        #endregion

        public void txt_limit_ContactorFailurePwr_Leave(object sender, EventArgs e)
        {
            if (LocalCommon.TextBox_validation(0, 100, txt_limit_ContactorFailure_Pwr))
            {
                Param_Limits_object.Contactor_Fail_Pwr_Limit = Convert.ToDouble(txt_limit_ContactorFailure_Pwr.Text);
                //Param_Limits_object.Contactor_Fail_Pwr_Limit *= 1000; //multiply by TEN THOUSAND
            }
        }

        private void txt_limit_ContactorFailure_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
               && !char.IsDigit(e.KeyChar)
             && e.KeyChar != '.')
            { e.Handled = true; }
            // only allow one decimal point     
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1
                ) { e.Handled = true; }

        }

        private void ucLimits_Load(object sender, EventArgs e)
        {
            try
            {
                DeattachValidation_EventHandlers();

                if (_Param_Limits_object == null)
                    _Param_Limits_object = new Param_Limits();

                if (_Param_Limit_Demand_OverLoad_T1 == null)
                    _Param_Limit_Demand_OverLoad_T1 = new Param_Limit_Demand_OverLoad();
                if (_Param_Limit_Demand_OverLoad_T2 == null)
                    _Param_Limit_Demand_OverLoad_T2 = new Param_Limit_Demand_OverLoad();
                if (_Param_Limit_Demand_OverLoad_T3 == null)
                    _Param_Limit_Demand_OverLoad_T3 = new Param_Limit_Demand_OverLoad();
                if (_Param_Limit_Demand_OverLoad_T4 == null)
                    _Param_Limit_Demand_OverLoad_T4 = new Param_Limit_Demand_OverLoad();

                if (limits == null)
                    limits = new LimitValues(Commons.Default_Meter);

                //Apply_LimitValues(limits);
                if (errorProvider != null)
                {
                    foreach (Control itemCtr in gb_Limits.Controls)
                    {
                        if (itemCtr.GetType() == typeof(TextBox) ||
                            itemCtr.GetType() == typeof(ComboBox))
                            errorProvider.SetIconAlignment(itemCtr, ErrorIconAlignment.MiddleRight);
                    }
                    errorProvider.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;
                }

            }
            finally
            {
                AttachValidation_EventHandlers();
            }
        }

        private void ucLimits_Leave(object sender, EventArgs e)
        {
            IsValid = Validate_ParamLimits();
        }

        private void txt_limit_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
              && e.KeyChar != '.')
            { e.Handled = true; }
            // only allow one decimal point     
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1
                ) { e.Handled = true; }
        }

        internal void Apply_LimitValues(LimitValues param_LimitValues)
        {
            try
            {
                String toolTipFormatStr = @"{0}_Limit:Range[{1}-{2}]";
                #region OverCurrentByPhase
                ///Apply ToolTip String
                if(txt_limit_OverCurrentByPhase_T1.Visible)
                {
                toolTip.SetToolTip(txt_limit_OverCurrentByPhase_T1,
                       String.Format(toolTipFormatStr, ThreshouldItem.OverCurrentByPhase_T1,
                       param_LimitValues.OverCurrentByPhase_T1_MIN,
                       param_LimitValues.OverCurrentByPhase_T1_MAX));
                }
                if(txt_limit_OverCurrentByPhase_T2.Visible)
                {
                ///Apply ToolTip String
                toolTip.SetToolTip(txt_limit_OverCurrentByPhase_T2,
                       String.Format(toolTipFormatStr, ThreshouldItem.OverCurrentByPhase_T2,
                       param_LimitValues.OverCurrentByPhase_T2_MIN,
                       param_LimitValues.OverCurrentByPhase_T2_MAX));
                }
                if(txt_limit_OverCurrentByPhase_T3.Visible)
                {
                ///Apply ToolTip String
                toolTip.SetToolTip(txt_limit_OverCurrentByPhase_T3,
                       String.Format(toolTipFormatStr, ThreshouldItem.OverCurrentByPhase_T3,
                       param_LimitValues.OverCurrentByPhase_T3_MIN,
                       param_LimitValues.OverCurrentByPhase_T3_MAX));
                }
                if(txt_limit_OverCurrentByPhase_T4.Visible)
                {
                ///Apply ToolTip String
                toolTip.SetToolTip(txt_limit_OverCurrentByPhase_T4,
                       String.Format(toolTipFormatStr, ThreshouldItem.OverCurrentByPhase_T4,
                       param_LimitValues.OverCurrentByPhase_T4_MIN,
                       param_LimitValues.OverCurrentByPhase_T4_MAX));
                #endregion
                }
                if(txt_limit_CT_FailAmpLimit.Visible)
                {
                ///Apply ToolTip String CTFail
                toolTip.SetToolTip(txt_limit_CT_FailAmpLimit,
                       String.Format(toolTipFormatStr, ThreshouldItem.CTFail,
                       param_LimitValues.CTFail_MIN,
                       param_LimitValues.CTFail_MAX));

                }
                if(txt_limit_high_neutral_current.Visible)
                {
                ///Apply ToolTip String HighNeutralCurrent
                toolTip.SetToolTip(txt_limit_high_neutral_current,
                       String.Format(toolTipFormatStr, ThreshouldItem.HighNeutralCurrent,
                       param_LimitValues.HighNeutralCurrent_MIN,
                       param_LimitValues.HighNeutralCurrent_MAX));

                }
                if(txt_limit_imbalance_volt.Visible)
                {
                ///Apply ToolTip String ImbalanceVolt
                toolTip.SetToolTip(txt_limit_imbalance_volt,
                       String.Format(toolTipFormatStr, ThreshouldItem.ImbalanceVolt,
                       param_LimitValues.ImbalanceVolt_MIN,
                       param_LimitValues.ImbalanceVolt_MAX));

                }
                if(txt_limit_over_volt.Visible)
                {
                ///Apply ToolTip String OverVolt
                toolTip.SetToolTip(txt_limit_over_volt,
                       String.Format(toolTipFormatStr, ThreshouldItem.OverVolt,
                       param_LimitValues.OverVolt_MIN,
                       param_LimitValues.OverVolt_MAX));

                }
                if(txt_limit_over_volt.Visible)
                {
                ///Apply ToolTip String OverVolt
                toolTip.SetToolTip(txt_limit_over_volt,
                       String.Format(toolTipFormatStr, ThreshouldItem.OverVolt,
                       param_LimitValues.OverVolt_MIN,
                       param_LimitValues.OverVolt_MAX));

                }
                if(txt_limit_PT_FailAmpLimit.Visible)
                {
                ///Apply ToolTip String PTFail_Amp
                toolTip.SetToolTip(txt_limit_PT_FailAmpLimit,
                       String.Format(toolTipFormatStr, ThreshouldItem.PTFail_Amp,
                       param_LimitValues.PTFail_AMP_MIN,
                       param_LimitValues.PTFail_AMP_MAX));

                }
                if(txt_limit_PT_FailVoltLimit.Visible)
                {
                ///Apply ToolTip String PTFail_Volt
                toolTip.SetToolTip(txt_limit_PT_FailVoltLimit,
                       String.Format(toolTipFormatStr, ThreshouldItem.PTFail_Volt,
                       param_LimitValues.PTFail_Volt_MIN,
                       param_LimitValues.PTFail_Volt_MAX));

                }
                if(txt_limit_reverse_energy.Visible)
                {
                ///Apply ToolTip String ReverseEnergy
                toolTip.SetToolTip(txt_limit_reverse_energy,
                       String.Format(toolTipFormatStr, ThreshouldItem.ReverseEnergy,
                       param_LimitValues.ReverseEnergy_MIN,
                       param_LimitValues.ReverseEnergy_MAX));

                }
                if(txt_limit_tamperEnergy.Visible)
                {
                ///Apply ToolTip String TamperEnergy
                toolTip.SetToolTip(txt_limit_tamperEnergy,
                       String.Format(toolTipFormatStr, ThreshouldItem.TamperEnergy,
                       param_LimitValues.TamperEnergy_MIN,
                       param_LimitValues.TamperEnergy_MAX));

                }
                if(txt_limit_under_volt.Visible)
                {
                ///Apply ToolTip String UnderVolt
                toolTip.SetToolTip(txt_limit_under_volt,
                       String.Format(toolTipFormatStr, ThreshouldItem.UnderVolt,
                       param_LimitValues.UnderVolt_MIN,
                       param_LimitValues.UnderVolt_MAX));

                #region ///Apply ToolTip String OverLoadByPhase

                }
                if(txt_OverLoadByPhase_T1.Visible)
                {
                ///Apply ToolTip String OverLoadByPhase_T1
                toolTip.SetToolTip(txt_OverLoadByPhase_T1,
                       String.Format(toolTipFormatStr, ThreshouldItem.OverLoadByPhase_T1,
                       param_LimitValues.OverLoadByPhase_T1_MIN,
                       param_LimitValues.OverLoadByPhase_T1_MAX));

                }
                if(txt_OverLoadByPhase_T2.Visible)
                {
                ///Apply ToolTip String OverLoadByPhase_T2
                toolTip.SetToolTip(txt_OverLoadByPhase_T2,
                       String.Format(toolTipFormatStr, ThreshouldItem.OverLoadByPhase_T2,
                       param_LimitValues.OverLoadByPhase_T2_MIN,
                       param_LimitValues.OverLoadByPhase_T2_MAX));

                }
                if(txt_OverLoadByPhase_T3.Visible)
                {
                ///Apply ToolTip String OverLoadByPhase_T3
                toolTip.SetToolTip(txt_OverLoadByPhase_T3,
                       String.Format(toolTipFormatStr, ThreshouldItem.OverLoadByPhase_T3,
                       param_LimitValues.OverLoadByPhase_T3_MIN,
                       param_LimitValues.OverLoadByPhase_T3_MAX));

                }
                if(txt_OverLoadByPhase_T4.Visible)
                {
                ///Apply ToolTip String OverLoadByPhase_T4
                toolTip.SetToolTip(txt_OverLoadByPhase_T4,
                       String.Format(toolTipFormatStr, ThreshouldItem.OverLoadByPhase_T4,
                       param_LimitValues.OverLoadByPhase_T4_MIN,
                       param_LimitValues.OverLoadByPhase_T4_MAX));

                #endregion

                #region ///Apply ToolTip String OverLoadTotal

                }
                if(txt_OverLoadTotal_T1.Visible)
                {
                ///Apply ToolTip String OverLoadTotal_T1
                toolTip.SetToolTip(txt_OverLoadTotal_T1,
                       String.Format(toolTipFormatStr, ThreshouldItem.OverLoadTotal_T1,
                       param_LimitValues.OverLoadTotal_T1_MIN,
                       param_LimitValues.OverLoadTotal_T1_MAX));

                }
                if(txt_OverLoadTotal_T2.Visible)
                {
                ///Apply ToolTip String OverLoadTotal_T2
                toolTip.SetToolTip(txt_OverLoadTotal_T2,
                       String.Format(toolTipFormatStr, ThreshouldItem.OverLoadTotal_T2,
                       param_LimitValues.OverLoadTotal_T2_MIN,
                       param_LimitValues.OverLoadTotal_T2_MAX));

                }
                if(txt_OverLoadTotal_T3.Visible)
                {
                ///Apply ToolTip String OverLoadTotal_T3
                toolTip.SetToolTip(txt_OverLoadTotal_T3,
                       String.Format(toolTipFormatStr, ThreshouldItem.OverLoadTotal_T3,
                       param_LimitValues.OverLoadTotal_T3_MIN,
                       param_LimitValues.OverLoadTotal_T3_MAX));

                }
                if(txt_OverLoadTotal_T4.Visible)
                {
                ///Apply ToolTip String OverLoadTotal_T4
                toolTip.SetToolTip(txt_OverLoadTotal_T4,
                       String.Format(toolTipFormatStr, ThreshouldItem.OverLoadTotal_T4,
                       param_LimitValues.OverLoadTotal_T4_MIN,
                       param_LimitValues.OverLoadTotal_T4_MAX));
                }
                #endregion

                #region ///Apply ToolTip String MDIExceed

                if(txt_DemandOverLoad_T1.Visible)
                {
                ///Apply ToolTip String MDIExceed_T1
                toolTip.SetToolTip(txt_DemandOverLoad_T1,
                       String.Format(toolTipFormatStr, ThreshouldItem.MDIExceed_T1,
                       param_LimitValues.MDIExceed_T1_MIN,
                       param_LimitValues.MDIExceed_T1_MAX));

                }
                if(txt_DemandOverLoad_T2.Visible)
                {
                ///Apply ToolTip String MDIExceed_T2
                toolTip.SetToolTip(txt_DemandOverLoad_T2,
                       String.Format(toolTipFormatStr, ThreshouldItem.MDIExceed_T2,
                       param_LimitValues.MDIExceed_T2_MIN,
                       param_LimitValues.MDIExceed_T2_MAX));

                }
                if(txt_DemandOverLoad_T3.Visible)
                {
                ///Apply ToolTip String MDIExceed_T3
                toolTip.SetToolTip(txt_DemandOverLoad_T3,
                       String.Format(toolTipFormatStr, ThreshouldItem.MDIExceed_T3,
                       param_LimitValues.MDIExceed_T3_MIN,
                       param_LimitValues.MDIExceed_T3_MAX));

                }
                if(txt_DemandOverLoad_T4.Visible)
                {
                ///Apply ToolTip String MDIExceed_T4
                toolTip.SetToolTip(txt_DemandOverLoad_T4,
                       String.Format(toolTipFormatStr, ThreshouldItem.MDIExceed_T4,
                       param_LimitValues.MDIExceed_T4_MIN,
                       param_LimitValues.MDIExceed_T4_MAX));
                }

                #endregion

            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Apply Param_LimitValues", ex.Message, 5000);
            }
        }

        #region Limits_leave_events

        public void txt_limit_over_volt_Leave(object sender, EventArgs e)
        {
            ///if (SmartEyeControl_7.Common.Commons.TextBox_validation(limits.OverVolt_MIN, limits.PTFail_Volt_MAX, txt_limit_over_volt))
            ///{
            ///    Param_Limits_object.OverVolt = Convert.ToDouble(txt_limit_over_volt.Text);
            ///}
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.OverVolt, txt_limit_over_volt, errorProvider);
            //Validate_ParamLimits();
        }

        public void txt_limit_under_volt_Leave(object sender, EventArgs e)
        {
            ///if (SmartEyeControl_7.Common.Commons.TextBox_validation(limits.UnderVolt_MIN, limits.UnderVolt_MAX, txt_limit_under_volt))
            ///{
            ///    Param_Limits_object.UnderVolt = Convert.ToDouble(txt_limit_under_volt.Text);
            ///}
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.UnderVolt, txt_limit_under_volt, errorProvider);
            //Validate_ParamLimits();
        }

        private void txt_limit_PT_FailVoltLimit_Leave(object sender, EventArgs e)
        {
            ///if (SmartEyeControl_7.Common.Commons.TextBox_validation(limits.PTFail_Volt_MIN, limits.PTFail_Volt_MAX, txt_limit_PT_FailVoltLimit))
            ///{
            ///    Param_Limits_object.PTFail_Volt = Convert.ToDouble(txt_limit_PT_FailVoltLimit.Text);
            ///}
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.PTFail_Volt, txt_limit_PT_FailVoltLimit, errorProvider);
            //Validate_ParamLimits();
        }

        public void txt_limit_imbalance_volt_Leave(object sender, EventArgs e)
        {
            //if (SmartEyeControl_7.Common.Commons.TextBox_validation(limits.ImbalanceVolt_MIN, limits.ImbalanceVolt_MAX, txt_limit_imbalance_volt))
            //{
            //    Param_Limits_object.ImbalanceVolt = Convert.ToDouble(txt_limit_imbalance_volt.Text);
            //}
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.ImbalanceVolt, txt_limit_imbalance_volt, errorProvider);
            //Validate_ParamLimits();
        }

        public void txt_limit_high_neutral_current_Leave(object sender, EventArgs e)
        {
            //if (SmartEyeControl_7.Common.Commons.TextBox_validation(limits.HighNeutralCurrent_MIN, limits.HighNeutralCurrent_MAX, txt_limit_high_neutral_current))
            //{
            //    Param_Limits_object.HighNeutralCurrent = Convert.ToDouble(txt_limit_high_neutral_current.Text);
            //}
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.HighNeutralCurrent, txt_limit_high_neutral_current, errorProvider);
            //Validate_ParamLimits();
        }

        public void txt_limit_CT_FailAmpLimit_Leave(object sender, EventArgs e)
        {
            //if (SmartEyeControl_7.Common.Commons.TextBox_validation(limits.CTFail_MIN, limits.CTFail_MAX, txt_limit_CT_FailAmpLimit))
            //{
            //    Param_Limits_object.CTFail_AMP = Convert.ToDouble(txt_limit_CT_FailAmpLimit.Text);
            //}
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.CTFail, txt_limit_CT_FailAmpLimit, errorProvider);
            //Validate_ParamLimits();
        }

        public void txt_limit_PT_FailAmpLimit_Leave(object sender, EventArgs e)
        {
            //if (SmartEyeControl_7.Common.Commons.TextBox_validation(limits.PTFail_AMP_MIN, limits.PTFail_AMP_MAX, txt_limit_PT_FailAmpLimit))
            //{
            //    Param_Limits_object.PTFail_AMP = Convert.ToDouble(txt_limit_PT_FailAmpLimit.Text);
            //}
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.PTFail_Amp, txt_limit_PT_FailAmpLimit, errorProvider);
            //Validate_ParamLimits();
        }

        public void txt_limit_OverCurrentByPhase_T1_Leave(object sender, EventArgs e)
        {
            //if (SmartEyeControl_7.Common.Commons.TextBox_validation(limits.OverCurrentByPhase_T1_MIN, limits.OverCurrentByPhase_T1_MAX, txt_limit_OverCurrentByPhase_T1))
            //{
            //    Param_Limits_object.OverCurrentByPhase_T1 = Convert.ToDouble(txt_limit_OverCurrentByPhase_T1.Text);
            //}
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.OverCurrentByPhase_T1, txt_limit_OverCurrentByPhase_T1, errorProvider);
            //Validate_ParamLimits();
        }

        public void txt_limit_OverCurrentByPhase_T2_Leave(object sender, EventArgs e)
        {
            //if (SmartEyeControl_7.Common.Commons.TextBox_validation(limits.OverCurrentByPhase_T2_MIN, limits.OverCurrentByPhase_T2_MAX, txt_limit_OverCurrentByPhase_T2))
            //{
            //    Param_Limits_object.OverCurrentByPhase_T2 = Convert.ToDouble(txt_limit_OverCurrentByPhase_T2.Text);
            //}
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.OverCurrentByPhase_T2, txt_limit_OverCurrentByPhase_T2, errorProvider);
            //Validate_ParamLimits();
        }

        public void txt_limit_OverCurrentByPhase_T3_Leave(object sender, EventArgs e)
        {
            //if (SmartEyeControl_7.Common.Commons.TextBox_validation(limits.OverCurrentByPhase_T3_MIN, limits.OverCurrentByPhase_T3_MAX, txt_limit_OverCurrentByPhase_T3))
            //{
            //    Param_Limits_object.OverCurrentByPhase_T3 = Convert.ToDouble(txt_limit_OverCurrentByPhase_T3.Text);
            //}
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.OverCurrentByPhase_T3, txt_limit_OverCurrentByPhase_T3, errorProvider);
            //Validate_ParamLimits();
        }

        public void txt_limit_OverCurrentByPhase_T4_Leave(object sender, EventArgs e)
        {
            //if (SmartEyeControl_7.Common.Commons.TextBox_validation(limits.OverCurrentByPhase_T4_MIN, limits.OverCurrentByPhase_T4_MAX, txt_limit_OverCurrentByPhase_T4))
            //{
            //    Param_Limits_object.OverCurrentByPhase_T4 = Convert.ToDouble(txt_limit_OverCurrentByPhase_T4.Text);
            //}
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.OverCurrentByPhase_T4, txt_limit_OverCurrentByPhase_T4, errorProvider);
            //Validate_ParamLimits();
        }

        public void txt_limit_reverse_energy_Leave(object sender, EventArgs e)
        {
            //if (SmartEyeControl_7.Common.Commons.TextBox_validation(limits.ReverseEnergy_MIN, limits.ReverseEnergy_MAX, txt_limit_reverse_energy))
            //{
            //    Param_Limits_object.ReverseEnergy = Convert.ToDouble(txt_limit_reverse_energy.Text);
            //    Param_Limits_object.ReverseEnergy *= 1000; //multiply by TEN THOUSAND
            //}
            ///bool isValidated = 
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.ReverseEnergy, txt_limit_reverse_energy, errorProvider);
            //Validate_ParamLimits();
            ///if (isValidated)
            /// Param_Limits_object.ReverseEnergy *= 1000; ///multiply by TEN THOUSAND

        }

        public void txt_limit_tamperEnergy_Leave(object sender, EventArgs e)
        {
            //if (SmartEyeControl_7.Common.Commons.TextBox_validation(limits.TamperEnergy_MIN, limits.TamperEnergy_MAX, txt_limit_tamperEnergy))
            //{
            //    Param_Limits_object.TamperEnergy = Convert.ToDouble(txt_limit_tamperEnergy.Text);
            //    Param_Limits_object.TamperEnergy *= 1000;
            //}
            //bool isValidated = 
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.TamperEnergy, txt_limit_tamperEnergy, errorProvider);
            //Validate_ParamLimits();
            //if (isValidated)
            //    Param_Limits_object.TamperEnergy *= 1000;
        }

        public void txt_OverLoadByPhase_T1_Leave(object sender, EventArgs e)
        {
            TextBox txt_OverLoadByPhase_local = (TextBox)sender;
            //if (SmartEyeControl_7.Common.Commons.TextBox_validation(limits.OverLoadByPhase_T1_MIN, limits.OverLoadByPhase_T1_MAX, txt_OverLoadByPhase_local))
            //{
            //    Param_Limits_object.OverLoadByPhase_T1 = Convert.ToDouble(txt_OverLoadByPhase_local.Text);
            //    Param_Limits_object.OverLoadByPhase_T1 *= 1000;
            //}
            ///bool isValidated = 
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.OverLoadByPhase_T1, txt_OverLoadByPhase_local, errorProvider);
            //Validate_ParamLimits();
            ///if (isValidated)
            ///    Param_Limits_object.OverLoadByPhase_T1 *= 1000;
        }

        public void txt_OverLoadByPhase_T2_Leave(object sender, EventArgs e)
        {
            TextBox txt_OverLoadByPhase_local = (TextBox)sender;
            //if (SmartEyeControl_7.Common.Commons.TextBox_validation(limits.OverLoadByPhase_T2_MIN, limits.OverLoadByPhase_T2_MAX, txt_OverLoadByPhase_local))
            //{
            //    Param_Limits_object.OverLoadByPhase_T2 = Convert.ToDouble(txt_OverLoadByPhase_local.Text);
            //    Param_Limits_object.OverLoadByPhase_T2 *= 1000;
            //}
            ///bool isValidated =
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.OverLoadByPhase_T2, txt_OverLoadByPhase_local, errorProvider);
           // Validate_ParamLimits();
            ///if (isValidated)
            ///    Param_Limits_object.OverLoadByPhase_T2 *= 1000;
        }

        public void txt_OverLoadByPhase_T3_Leave(object sender, EventArgs e)
        {
            TextBox txt_OverLoadByPhase_local = (TextBox)sender;
            //if (SmartEyeControl_7.Common.Commons.TextBox_validation(limits.OverLoadByPhase_T3_MIN, limits.OverLoadByPhase_T3_MAX, txt_OverLoadByPhase_local))
            //{
            //    Param_Limits_object.OverLoadByPhase_T3 = Convert.ToDouble(txt_OverLoadByPhase_local.Text);
            //    Param_Limits_object.OverLoadByPhase_T3 *= 1000;
            //}
            ///bool isValidated = 
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.OverLoadByPhase_T3, txt_OverLoadByPhase_local, errorProvider);
            //Validate_ParamLimits();
            ///if (isValidated)
            ///    Param_Limits_object.OverLoadByPhase_T3 *= 1000;
        }

        public void txt_OverLoadByPhase_T4_Leave(object sender, EventArgs e)
        {
            TextBox txt_OverLoadByPhase_local = (TextBox)sender;
            //if (SmartEyeControl_7.Common.Commons.TextBox_validation(limits.OverLoadByPhase_T4_MIN, limits.OverLoadByPhase_T4_MAX, txt_OverLoadByPhase_local))
            //{
            //    Param_Limits_object.OverLoadByPhase_T4 = Convert.ToDouble(txt_OverLoadByPhase_local.Text);
            //    Param_Limits_object.OverLoadByPhase_T4 *= 1000;
            //}
            ///bool isValidated = 
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.OverLoadByPhase_T4, txt_OverLoadByPhase_local, errorProvider);
            //Validate_ParamLimits();
            ///if (isValidated)
            ///    Param_Limits_object.OverLoadByPhase_T4 *= 1000;
        }

        public void txt_OverLoadTotal_T1_Leave(object sender, EventArgs e)
        {
            TextBox txt_OverLoadTotal_local = (TextBox)sender;
            //if (SmartEyeControl_7.Common.Commons.TextBox_validation(limits.OverLoadTotal_T1_MIN, limits.OverLoadTotal_T1_MAX, txt_OverLoadTotal_local))
            //{
            //    Param_Limits_object.OverLoadTotal_T1 = Convert.ToDouble(txt_OverLoadTotal_local.Text);
            //    Param_Limits_object.OverLoadTotal_T1 *= 1000;
            //}
            ///bool isValidated = 
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.OverLoadTotal_T1, txt_OverLoadTotal_local, errorProvider);
            //Validate_ParamLimits();
            //if (isValidated)
            //    Param_Limits_object.OverLoadTotal_T1 *= 1000;
        }

        public void txt_OverLoadTotal_T2_Leave(object sender, EventArgs e)
        {
            TextBox txt_OverLoadTotal_local = (TextBox)sender;
            //if (SmartEyeControl_7.Common.Commons.TextBox_validation(limits.OverLoadTotal_T2_MIN, limits.OverLoadTotal_T2_MAX, txt_OverLoadTotal_local))
            //{
            //    Param_Limits_object.OverLoadTotal_T2 = Convert.ToDouble(txt_OverLoadTotal_T2.Text);
            //    Param_Limits_object.OverLoadTotal_T2 *= 1000;
            //}

            ///bool isValidated = 
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.OverLoadTotal_T2, txt_OverLoadTotal_local, errorProvider);
            //Validate_ParamLimits();
            //if (isValidated)
            //    Param_Limits_object.OverLoadTotal_T2 *= 1000;
        }

        public void txt_OverLoadTotal_T3_Leave(object sender, EventArgs e)
        {
            TextBox txt_OverLoadTotal_local = (TextBox)sender;
            //if (SmartEyeControl_7.Common.Commons.TextBox_validation(limits.OverLoadTotal_T3_MIN, limits.OverLoadTotal_T3_MAX, txt_OverLoadTotal_local))
            //{
            //    Param_Limits_object.OverLoadTotal_T3 = Convert.ToDouble(txt_OverLoadTotal_local.Text);
            //    Param_Limits_object.OverLoadTotal_T3 *= 1000;
            //}
            ///bool isValidated = 
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.OverLoadTotal_T3, txt_OverLoadTotal_local, errorProvider);
            //Validate_ParamLimits();
            ///if (isValidated)
            ///    Param_Limits_object.OverLoadTotal_T3 *= 1000;
        }

        public void txt_OverLoadTotal_T4_Leave(object sender, EventArgs e)
        {
            TextBox txt_OverLoadTotal_local = (TextBox)sender;

            //if (SmartEyeControl_7.Common.Commons.TextBox_validation(limits.OverLoadTotal_T4_MIN, limits.OverLoadTotal_T4_MAX, txt_OverLoadTotal_local))
            //{
            //    Param_Limits_object.OverLoadTotal_T4 = Convert.ToDouble(txt_OverLoadTotal_local.Text);
            //    Param_Limits_object.OverLoadTotal_T4 *= 1000;
            //}
            ///bool isValidated = 
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.OverLoadTotal_T4, txt_OverLoadTotal_local, errorProvider);
           // Validate_ParamLimits();
            ///if (isValidated)
            ///    Param_Limits_object.OverLoadTotal_T4 *= 1000;
        }

        public void txt_DemandOverLoad_T1_Leave(object sender, EventArgs e)
        {
            TextBox txt_MDIExceed_local = (TextBox)sender;
            ///Validate_Limit_Value(Param_Limit_Demand_OverLoad_T1, ThreshouldItem.MDIExceed_T1, txt_MDIExceed_local, errorProvider);
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.MDIExceed_T1, txt_MDIExceed_local, errorProvider);
            //Validate_ParamLimits();
            //if (SmartEyeControl_7.Common.Commons.TextBox_validation(limits.MDIExceed_T1_MIN, limits.MDIExceed_T1_MAX, txt_DemandOverLoad_T1))
            //{
            //    decimal D = Convert.ToDecimal(txt_DemandOverLoad_T1.Text);
            //    D *= 1000;
            //    D = Math.Floor(D);
            //    Param_Limit_Demand_OverLoad_T1.Threshold = Convert.ToUInt32(D);
            //    txt_DemandOverLoad_T1.ForeColor = Color.Black;
            //}
        }

        public void txt_DemandOverLoad_T2_Leave(object sender, EventArgs e)
        {
            TextBox txt_MDIExceed_local = (TextBox)sender;
            ///Validate_Limit_Value(Param_Limit_Demand_OverLoad_T2, ThreshouldItem.MDIExceed_T2, txt_MDIExceed_local, errorProvider);
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.MDIExceed_T2, txt_MDIExceed_local, errorProvider);
            //Validate_ParamLimits();
            //if (SmartEyeControl_7.Common.Commons.TextBox_validation(limits.MDIExceed_T2_MIN, limits.MDIExceed_T2_MAX, txt_DemandOverLoad_T2))
            //{
            //    decimal D = Convert.ToDecimal(txt_DemandOverLoad_T2.Text);
            //    D *= 1000;
            //    D = Math.Floor(D);
            //    Param_Limit_Demand_OverLoad_T2.Threshold = Convert.ToUInt32(D);
            //    txt_DemandOverLoad_T2.ForeColor = Color.Black;
            //}
        }

        public void txt_DemandOverLoad_T3_Leave(object sender, EventArgs e)
        {
            TextBox txt_MDIExceed_local = (TextBox)sender;
            ///Validate_Limit_Value(Param_Limit_Demand_OverLoad_T3, ThreshouldItem.MDIExceed_T3, txt_MDIExceed_local, errorProvider);
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.MDIExceed_T3, txt_MDIExceed_local, errorProvider);
           // Validate_ParamLimits();
            //if (SmartEyeControl_7.Common.Commons.TextBox_validation(limits.MDIExceed_T3_MIN, limits.MDIExceed_T3_MAX, txt_DemandOverLoad_T3))
            //{
            //    decimal D = Convert.ToDecimal(txt_DemandOverLoad_T3.Text);
            //    D *= 1000;
            //    D = Math.Floor(D);
            //    Param_Limit_Demand_OverLoad_T3.Threshold = Convert.ToUInt32(D);
            //    txt_DemandOverLoad_T3.ForeColor = Color.Black;
            //}
        }

        public void txt_DemandOverLoad_T4_Leave(object sender, EventArgs e)
        {
            TextBox txt_MDIExceed_local = (TextBox)sender;
            ///Validate_Limit_Value(Param_Limit_Demand_OverLoad_T4, ThreshouldItem.MDIExceed_T4, txt_MDIExceed_local, errorProvider);
            Validate_Limit_Value(Param_Limits_object, ThreshouldItem.MDIExceed_T4, txt_MDIExceed_local, errorProvider);
            //Validate_ParamLimits();
            //if (SmartEyeControl_7.Common.Commons.TextBox_validation(limits.MDIExceed_T4_MIN, limits.MDIExceed_T4_MAX, txt_DemandOverLoad_T4))
            //{
            //    decimal D = Convert.ToDecimal(txt_DemandOverLoad_T4.Text);
            //    D *= 1000;
            //    D = Math.Floor(D);
            //    Param_Limit_Demand_OverLoad_T4.Threshold = Convert.ToUInt32(D);
            //    txt_DemandOverLoad_T4.ForeColor = Color.Black;
            //}
        }

        #endregion

        public void showToGUI_Limits()
        {
            try
            {
                DeattachValidation_EventHandlers();

                ResetValidationError();
                lbl_ErrorUCLimits.Visible = false;

                if (txt_limit_OverCurrentByPhase_T1.Visible || Commons.IsQc)
                {
                    txt_limit_OverCurrentByPhase_T1.Text = LocalCommon.value_to_string(Param_Limits_object.OverCurrentByPhase_T1);
                    txt_limit_OverCurrentByPhase_T1_Leave(txt_limit_OverCurrentByPhase_T1, new EventArgs());
                }
                if (txt_limit_OverCurrentByPhase_T2.Visible || Commons.IsQc)
                {
                    txt_limit_OverCurrentByPhase_T2.Text = LocalCommon.value_to_string(Param_Limits_object.OverCurrentByPhase_T2);
                    txt_limit_OverCurrentByPhase_T2_Leave(txt_limit_OverCurrentByPhase_T2, new EventArgs());
                }
                if (txt_limit_OverCurrentByPhase_T3.Visible || Commons.IsQc)
                {
                    txt_limit_OverCurrentByPhase_T3.Text = LocalCommon.value_to_string(Param_Limits_object.OverCurrentByPhase_T3);
                    txt_limit_OverCurrentByPhase_T3_Leave(txt_limit_OverCurrentByPhase_T3, new EventArgs());
                }
                if (txt_limit_OverCurrentByPhase_T4.Visible || Commons.IsQc)
                {
                    txt_limit_OverCurrentByPhase_T4.Text = LocalCommon.value_to_string(Param_Limits_object.OverCurrentByPhase_T4);
                    txt_limit_OverCurrentByPhase_T4_Leave(txt_limit_OverCurrentByPhase_T4, new EventArgs());
                }
                if (txt_limit_CT_FailAmpLimit.Visible || Commons.IsQc)
                {
                    txt_limit_CT_FailAmpLimit.Text = LocalCommon.value_to_string(Param_Limits_object.CTFail_AMP);
                    txt_limit_CT_FailAmpLimit_Leave(txt_limit_CT_FailAmpLimit, new EventArgs());
                }
                if (txt_limit_high_neutral_current.Visible || Commons.IsQc)
                {
                    txt_limit_high_neutral_current.Text = LocalCommon.value_to_string(Param_Limits_object.HighNeutralCurrent);
                    txt_limit_high_neutral_current_Leave(txt_limit_high_neutral_current, new EventArgs());
                }
                if (txt_limit_imbalance_volt.Visible || Commons.IsQc)
                {
                    txt_limit_imbalance_volt.Text = LocalCommon.value_to_string(Param_Limits_object.ImbalanceVolt);
                    txt_limit_imbalance_volt_Leave(txt_limit_imbalance_volt, new EventArgs());
                }
                if (txt_limit_over_volt.Visible || Commons.IsQc)
                {
                    txt_limit_over_volt.Text = LocalCommon.value_to_string(Param_Limits_object.OverVolt);
                    txt_limit_over_volt_Leave(txt_limit_over_volt, new EventArgs());
                }
                if (txt_limit_PT_FailAmpLimit.Visible || Commons.IsQc)
                {
                    txt_limit_PT_FailAmpLimit.Text = LocalCommon.value_to_string(Param_Limits_object.PTFail_AMP);
                    txt_limit_PT_FailAmpLimit_Leave(txt_limit_PT_FailAmpLimit, new EventArgs());
                }
                if (txt_limit_PT_FailVoltLimit.Visible || Commons.IsQc)
                {
                    txt_limit_PT_FailVoltLimit.Text = LocalCommon.value_to_string(Param_Limits_object.PTFail_Volt);
                    txt_limit_PT_FailVoltLimit_Leave(txt_limit_PT_FailVoltLimit, new EventArgs());
                }

                if (txt_limit_reverse_energy.Visible || Commons.IsQc)
                {
                    txt_limit_reverse_energy.Text = LocalCommon.value_to_string(Param_Limits_object.ReverseEnergy);/// 1000);
                    txt_limit_reverse_energy_Leave(txt_limit_reverse_energy, new EventArgs());
                }
                if (txt_limit_tamperEnergy.Visible || Commons.IsQc)
                {
                    txt_limit_tamperEnergy.Text = LocalCommon.value_to_string(Param_Limits_object.TamperEnergy);/// 1000);
                    txt_limit_tamperEnergy_Leave(txt_limit_tamperEnergy, new EventArgs());
                }
                if (txt_limit_under_volt.Visible || Commons.IsQc)
                {
                    txt_limit_under_volt.Text = LocalCommon.value_to_string(Param_Limits_object.UnderVolt);
                    txt_limit_under_volt_Leave(txt_limit_under_volt, new EventArgs());
                }
                //txt_DemandOverLoad_T1.Text = Commons.value_to_string(Param_Limits_object.DemandOverLoadTotal_T1);
                //txt_DemandOverLoad_T2.Text = Commons.value_to_string(Param_Limits_object.DemandOverLoadTotal_T2);
                //txt_DemandOverLoad_T3.Text = Commons.value_to_string(Param_Limits_object.DemandOverLoadTotal_T3);
                //txt_DemandOverLoad_T4.Text = Commons.value_to_string(Param_Limits_object.DemandOverLoadTotal_T4);

                if (txt_OverLoadByPhase_T1.Visible || Commons.IsQc)
                {
                    txt_OverLoadByPhase_T1.Text = LocalCommon.value_to_string(Param_Limits_object.OverLoadByPhase_T1);/// 1000);
                    txt_OverLoadByPhase_T1_Leave(txt_OverLoadByPhase_T1, new EventArgs());
                }
                if (txt_OverLoadByPhase_T2.Visible || Commons.IsQc)
                {
                    txt_OverLoadByPhase_T2.Text = LocalCommon.value_to_string(Param_Limits_object.OverLoadByPhase_T2);/// 1000);
                    txt_OverLoadByPhase_T2_Leave(txt_OverLoadByPhase_T2, new EventArgs());
                }
                if (txt_OverLoadByPhase_T3.Visible || Commons.IsQc)
                {
                    txt_OverLoadByPhase_T3.Text = LocalCommon.value_to_string(Param_Limits_object.OverLoadByPhase_T3);/// 1000);
                    txt_OverLoadByPhase_T3_Leave(txt_OverLoadByPhase_T3, new EventArgs());
                }
                if (txt_OverLoadByPhase_T4.Visible || Commons.IsQc)
                {
                    txt_OverLoadByPhase_T4.Text = LocalCommon.value_to_string(Param_Limits_object.OverLoadByPhase_T4);/// 1000);
                    txt_OverLoadByPhase_T4_Leave(txt_OverLoadByPhase_T4, new EventArgs());
                }
                if (txt_OverLoadTotal_T1.Visible || Commons.IsQc)
                {
                    txt_OverLoadTotal_T1.Text = LocalCommon.value_to_string(Param_Limits_object.OverLoadTotal_T1);/// 1000);
                    txt_OverLoadTotal_T1_Leave(txt_OverLoadTotal_T1, new EventArgs());
                }
                if (txt_OverLoadTotal_T2.Visible || Commons.IsQc)
                {
                    txt_OverLoadTotal_T2.Text = LocalCommon.value_to_string(Param_Limits_object.OverLoadTotal_T2);/// 1000);
                    txt_OverLoadTotal_T2_Leave(txt_OverLoadTotal_T2, new EventArgs());
                }
                if (txt_OverLoadTotal_T3.Visible || Commons.IsQc)
                {
                    txt_OverLoadTotal_T3.Text = LocalCommon.value_to_string(Param_Limits_object.OverLoadTotal_T3);/// 1000);
                    txt_OverLoadTotal_T3_Leave(txt_OverLoadTotal_T3, new EventArgs());
                }
                if (txt_OverLoadTotal_T4.Visible || Commons.IsQc)
                {
                    txt_OverLoadTotal_T4.Text = LocalCommon.value_to_string(Param_Limits_object.OverLoadTotal_T4);/// 1000);
                    txt_OverLoadTotal_T4_Leave(txt_OverLoadTotal_T4, new EventArgs());
                }

                if (txt_DemandOverLoad_T1.Visible || Commons.IsQc)
                {
                    txt_DemandOverLoad_T1.Text = LocalCommon.value_to_string(Param_Limits_object.DemandOverLoadTotal_T1);/// 1000);
                    txt_DemandOverLoad_T1_Leave(txt_DemandOverLoad_T1, new EventArgs());
                }
                if (txt_DemandOverLoad_T2.Visible || Commons.IsQc)
                {
                    txt_DemandOverLoad_T2.Text = LocalCommon.value_to_string(Param_Limits_object.DemandOverLoadTotal_T2);/// 1000);
                    txt_DemandOverLoad_T2_Leave(txt_DemandOverLoad_T2, new EventArgs());
                }
                if (txt_DemandOverLoad_T3.Visible || Commons.IsQc)
                {
                    txt_DemandOverLoad_T3.Text = LocalCommon.value_to_string(Param_Limits_object.DemandOverLoadTotal_T3);/// 1000);
                    txt_DemandOverLoad_T3_Leave(txt_DemandOverLoad_T3, new EventArgs());
                }
                if (txt_DemandOverLoad_T4.Visible || Commons.IsQc)
                {
                    txt_DemandOverLoad_T4.Text = LocalCommon.value_to_string(Param_Limits_object.DemandOverLoadTotal_T4);/// 1000);
                    txt_DemandOverLoad_T4_Leave(txt_DemandOverLoad_T4, new EventArgs());
                }
                if (txt_limit_ContactorFailure_Pwr.Visible || Commons.IsQc)
                {
                    txt_limit_ContactorFailure_Pwr.Text = LocalCommon.value_to_string(Param_Limits_object.Contactor_Fail_Pwr_Limit); // / 1000);
                    //txt_limit_ContactorFailure_Pwr_Leave(txt_limit_ContactorFailure_Pwr, new EventArgs());
                }
                if (tbMeterOnLoad.Visible)
                {
                    tbMeterOnLoad.Text = LocalCommon.value_to_string(Param_Limits_object.MeterOnLoad);
                    tbMeterOnLoad_Leave(tbMeterOnLoad, new EventArgs());
                }
                if (tbOverCurrentPhase.Visible)
                {
                    tbOverCurrentPhase.Text = LocalCommon.value_to_string(Param_Limits_object.OverCurrent_Phase);
                    tbOverCurrentPhase_Leave(tbOverCurrentPhase, new EventArgs());
                }
                if (tbPowerFactorChange.Visible)
                {
                    tbPowerFactorChange.Text = LocalCommon.value_to_string(Param_Limits_object.PowerFactor_Change);
                    tbPowerFactorChange_Leave(tbPowerFactorChange, new EventArgs());
                }
                if (tbCrestFactorHigh.Visible)
                {
                    tbCrestFactorHigh.Text = LocalCommon.value_to_string(Param_Limits_object.CrestFactorHigh);
                    tbCrestFactorHigh_Leave(tbCrestFactorHigh, new EventArgs());
                }
                if (tbCrestFactorLow.Visible)
                {
                    tbCrestFactorLow.Text = LocalCommon.value_to_string(Param_Limits_object.CrestFactorLow);
                    tbCrestFactorLow_Leave(tbCrestFactorLow, new EventArgs());
                }
                if (tbOverLoad_1P.Visible)
                {
                    tbOverLoad_1P.Text = LocalCommon.value_to_string(Param_Limits_object.OverPower);/// 1000);
                    tbOverLoad_1P_Leave(tbOverLoad_1P, new EventArgs());
                }

            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error show Param_Limits_object", ex.Message, 5000);
            }
            finally
            {
                IsValid = Validate_ParamLimits();
                AttachValidation_EventHandlers();
            }
        }

        #region AccessControlMethods

        public bool ApplyAccessRights(List<AccessRights> Rights, bool IsSinglePhase)
        {
            bool isSuccess = false;
            try
            {
                this.SuspendLayout();
                TBLPnl2.SuspendLayout();
                tabLayoutPnl.SuspendLayout();

                if (IsSinglePhase)
                {
                    lbl_CT_Fail.Text = "Unbalance Line Neutral Current";
                    //lbl_PT_Fail_V.Text = "Current in OneWire (A)";
                    lbl_PT_Fail.Text = "Over Laod (kW)";
                    lbl_limit_high_neutral.Text = "Over Current Neutral";
                }
                else
                {
                    lbl_CT_Fail.Text = "C.T. Fail (A)";
                    //lbl_PT_Fail_V.Text = "P.T. Fail (V)";
                    lbl_PT_Fail.Text = "P.T. Fail (A)";
                    lbl_limit_high_neutral.Text = "High Neutral Current";
                }

                #region Initialize Layout Code

                pnl_OverCurrent.Visible = pnl_OverLoadByPh.Visible = pnl_OverLoadTotal.Visible = pnl_MDIExceed.Visible = true;
                pnl_OverCurrent.Enabled = pnl_OverLoadByPh.Enabled = pnl_OverLoadTotal.Enabled = pnl_MDIExceed.Enabled = true;
                pnlOverCurrentPhase.Visible = pnlCrestFactorHigh.Visible = pnlCrestFactorLow.Visible = pnlMeterOnLoad.Visible = 
                    pnlPowerFactorChange.Visible = pnlOverLoad_1p.Visible = true;
                pnlOverCurrentPhase.Enabled = pnlCrestFactorHigh.Enabled = pnlCrestFactorLow.Enabled = pnlMeterOnLoad.Enabled = 
                    pnlPowerFactorChange.Enabled = pnlOverLoad_1p.Enabled = true;

                //Display Sequence Goes here
                //Panel[] Panel_Objects = new Panel[]{pnl_OverVolt,pnl_UnderVolt,pnl_ImbalanceVolt,pnl_highNeutral,pnl_Rev_Energy,
                //                                    pnl_TemperEnrgy,pnl_CTFail,pnl_PTFail_Amp,pnl_PTFail_Volt,pnlContactorFailure,
                //                                    pnlOverCurrentPhase, pnlCrestFactorHigh, pnlCrestFactorLow, pnlMeterOnLoad, pnlPowerFactorChange};
                //Display Sequence Goes here
                Panel[] Panel_Objects = new Panel[]{pnl_OverVolt,pnl_UnderVolt,pnl_ImbalanceVolt,pnlContactorFailure,pnl_Rev_Energy,pnl_highNeutral,
                                                    pnlOverCurrentPhase, pnlPowerFactorChange, pnl_TemperEnrgy,pnl_CTFail,pnl_PTFail_Amp,pnl_PTFail_Volt,
                                                    pnlMeterOnLoad, pnlCrestFactorHigh, pnlCrestFactorLow, pnlOverLoad_1p };
                foreach (var pnl_obj in Panel_Objects)
                {
                    pnl_obj.Enabled = true;
                    pnl_obj.Visible = true;
                }

                int pnl_Index = 0;
                int row_index = 0;
                int col_index = 0;

                #endregion
                if (Rights.Find(x => x.Read == true || x.Write == true) != null)
                {
                    foreach (var item in Rights)
                    {
                        _HelperAccessRights((Limits)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
                    }
                    isSuccess = true;
                }
                #region Apply_Layout Logic

                //Reset Table Layout
                tabLayoutPnl.Controls.Clear();
                for (col_index = 0; col_index < (tabLayoutPnl.ColumnCount - 1); col_index += 2)
                {
                    row_index = 0;
                    while (row_index < (tabLayoutPnl.RowCount - 1) &&
                        pnl_Index < Panel_Objects.Length)
                    {
                        Panel Limit_Obj = Panel_Objects[pnl_Index++];
                        if (Limit_Obj.Enabled || Limit_Obj.Visible)
                        {
                            tabLayoutPnl.Controls.Add(Limit_Obj, col_index, row_index);
                            //Limit_Obj.Visible = true;
                            row_index++;
                        }
                    }
                }

                #endregion
                if (pnl_OverCurrent.Visible || pnl_OverLoadByPh.Visible || pnl_OverLoadTotal.Visible || pnl_MDIExceed.Visible)
                {
                    lbl_HeadingT1.Visible = lbl_HeadingT2.Visible = lbl_HeadingT3.Visible = lbl_HeadingT4.Visible = true;
                }
                else
                    lbl_HeadingT1.Visible = lbl_HeadingT2.Visible = lbl_HeadingT3.Visible = lbl_HeadingT4.Visible = false;
            }
            finally
            {
                TBLPnl2.ResumeLayout();
                tabLayoutPnl.ResumeLayout();
                this.ResumeLayout();
            }
            return isSuccess;
        }

        private void _HelperAccessRights(Limits qty, bool read, bool write)
        {
            //read = true;
            //write = true;
            switch (qty)
            {

                case Limits.OverVolt:
                    //pnl_OverVolt.Enabled = write;
                    pnl_OverVolt.Visible = (read || write);

                    txt_limit_over_volt.Enabled = write;
                    txt_limit_over_volt.Visible = read;
                    break;
                case Limits.UnderVolt:
                    //pnl_UnderVolt.Enabled = write;
                    pnl_UnderVolt.Visible = (read || write);

                    txt_limit_under_volt.Enabled = write;
                    txt_limit_under_volt.Visible = read;
                    break;
                case Limits.ImbalanceVolt:
                    //pnl_ImbalanceVolt.Enabled = write;
                    pnl_ImbalanceVolt.Visible = (read || write);

                    txt_limit_imbalance_volt.Enabled = write;
                    txt_limit_imbalance_volt.Visible = read;
                    break;
                case Limits.HighNeturalCurrent:
                    //pnl_highNeutral.Enabled = write;
                    pnl_highNeutral.Visible = read;

                    txt_limit_high_neutral_current.Enabled = write;
                    txt_limit_high_neutral_current.Visible = read;

                    break;
                case Limits.ReverseEnergykWh:
                    //pnl_Rev_Energy.Enabled = write;
                    pnl_Rev_Energy.Visible = (read || write);

                    txt_limit_reverse_energy.Enabled = write;
                    txt_limit_reverse_energy.Visible = read;
                    break;
                case Limits.TemperEnergykWh:
                    //pnl_TemperEnrgy.Enabled = write;
                    pnl_TemperEnrgy.Visible = (read || write);

                    txt_limit_tamperEnergy.Enabled = write;
                    txt_limit_tamperEnergy.Visible = read;
                    break;
                case Limits.CTFailLimitAmp:
                    //pnl_CTFail.Enabled = write;
                    pnl_CTFail.Visible = (read || write);

                    txt_limit_CT_FailAmpLimit.Enabled = write;
                    txt_limit_CT_FailAmpLimit.Visible = read;
                    break;
                case Limits.PTFailLimitAmp:
                    //pnl_PTFail_Amp.Enabled = write;
                    pnl_PTFail_Amp.Visible = (read || write);

                    txt_limit_PT_FailAmpLimit.Enabled = write;
                    txt_limit_PT_FailAmpLimit.Visible = read;
                    break;
                case Limits.PTFailLimitV:
                    //pnl_PTFail_Volt.Enabled = write;
                    pnl_PTFail_Volt.Visible = (read || write);

                    txt_limit_PT_FailVoltLimit.Enabled = write;
                    txt_limit_PT_FailVoltLimit.Visible = read;
                    break;

                case Limits.OverCurrentByPhaseT1:
                    txt_limit_OverCurrentByPhase_T1.Enabled = write;
                    txt_limit_OverCurrentByPhase_T1.Visible = (read || write);
                    break;
                case Limits.OverCurrentByPhaseT2:
                    txt_limit_OverCurrentByPhase_T2.Enabled = write;
                    txt_limit_OverCurrentByPhase_T2.Visible = (read || write);
                    break;
                case Limits.OverCurrentByPhaseT3:
                    txt_limit_OverCurrentByPhase_T3.Enabled = write;
                    txt_limit_OverCurrentByPhase_T3.Visible = (read || write);
                    break;
                case Limits.OverCurrentByPhaseT4:
                    txt_limit_OverCurrentByPhase_T4.Enabled = write;
                    txt_limit_OverCurrentByPhase_T4.Visible = (read || write);
                    break;

                case Limits.OverLoadByPhaseT1:
                    txt_OverLoadByPhase_T1.Enabled = write;
                    txt_OverLoadByPhase_T1.Visible = (read || write);
                    break;
                case Limits.OverLoadByPhaseT2:
                    txt_OverLoadByPhase_T2.Enabled = write;
                    txt_OverLoadByPhase_T2.Visible = (read || write);
                    break;
                case Limits.OverLoadByPhaseT3:
                    txt_OverLoadByPhase_T3.Enabled = write;
                    txt_OverLoadByPhase_T3.Visible = (read || write);
                    break;
                case Limits.OverLoadByPhaseT4:
                    txt_OverLoadByPhase_T4.Enabled = write;
                    txt_OverLoadByPhase_T4.Visible = (read || write);
                    break;
                case Limits.OverLoadTotalT1:
                    txt_OverLoadTotal_T1.Enabled = write;
                    txt_OverLoadTotal_T1.Visible = (read || write);
                    break;
                case Limits.OverLoadTotalT2:
                    txt_OverLoadTotal_T2.Enabled = write;
                    txt_OverLoadTotal_T2.Visible = (read || write);
                    break;
                case Limits.OverLoadTotalT3:
                    txt_OverLoadTotal_T3.Enabled = write;
                    txt_OverLoadTotal_T3.Visible = (read || write);
                    break;
                case Limits.OverLoadTotalT4:
                    txt_OverLoadTotal_T4.Enabled = write;
                    txt_OverLoadTotal_T4.Visible = (read || write);
                    break;
                case Limits.MDIExceedT1:
                    txt_DemandOverLoad_T1.Enabled = write;
                    txt_DemandOverLoad_T1.Visible = (read || write);
                    break;
                case Limits.MDIExceedT2:
                    txt_DemandOverLoad_T2.Enabled = write;
                    txt_DemandOverLoad_T2.Visible = (read || write);
                    break;
                case Limits.MDIExceedT3:
                    txt_DemandOverLoad_T3.Enabled = write;
                    txt_DemandOverLoad_T3.Visible = (read || write);
                    break;
                case Limits.MDIExceedT4:
                    txt_DemandOverLoad_T4.Enabled = write;
                    txt_DemandOverLoad_T4.Visible = (read || write);
                    break;
                case Limits.Contactor_Failure_Pwr: //v5.3.12
                    pnlContactorFailure.Visible = (read || write);
                    txt_limit_ContactorFailure_Pwr.Enabled = write;
                    break;
                case Limits.Meter_ON_Load:
                    pnlMeterOnLoad.Visible = (read || write);
                    tbMeterOnLoad.Enabled = write;
                    break;
                case Limits.PowerFactor_Change:
                    pnlPowerFactorChange.Visible = (read || write);
                    tbPowerFactorChange.Enabled = write;
                    break;
                case Limits.CrestFactorLow:
                    pnlCrestFactorLow.Visible = (read || write);
                    tbCrestFactorLow.Enabled = write;
                    break;
                case Limits.CrestFactorHigh:
                    pnlCrestFactorHigh.Visible = (read || write);
                    tbCrestFactorHigh.Enabled = write;
                    break;
                case Limits.OverPower:
                    pnlOverLoad_1p.Visible = (read || write);
                    tbOverLoad_1P.Enabled = write;
                    break;
                default:
                    break;
            }
            #region OverCurrentByPhase

            if (qty == Limits.OverCurrentByPhaseT1 ||
               qty == Limits.OverCurrentByPhaseT2 ||
                qty == Limits.OverCurrentByPhaseT3 ||
                qty == Limits.OverCurrentByPhaseT4)
            {
                if (!txt_limit_OverCurrentByPhase_T1.Visible && !txt_limit_OverCurrentByPhase_T2.Visible &&
                    !txt_limit_OverCurrentByPhase_T3.Visible && !txt_limit_OverCurrentByPhase_T4.Visible)  //Bug Fixed by Azeem for Accurate version
                {
                    //pnl_OverCurrent.Enabled = false;
                    pnl_OverCurrent.Visible = false;
                }
            }

            #endregion
            #region OverLoadByPhase

            if (qty == Limits.OverLoadByPhaseT1 ||
                   qty == Limits.OverLoadByPhaseT2 ||
                    qty == Limits.OverLoadByPhaseT3 ||
                    qty == Limits.OverLoadByPhaseT4)
            {
                if (!txt_OverLoadByPhase_T1.Visible && !txt_OverLoadByPhase_T2.Visible &&
                    !txt_OverLoadByPhase_T3.Visible && !txt_OverLoadByPhase_T4.Visible) //Bug Fixed by Azeem for Accurate version
                {
                    //pnl_OverLoadByPh.Enabled = false;
                    pnl_OverLoadByPh.Visible = false;
                }
            }

            #endregion
            #region OverLoadTotal

            if (qty == Limits.OverLoadTotalT1 ||
                   qty == Limits.OverLoadTotalT2 ||
                    qty == Limits.OverLoadTotalT3 ||
                    qty == Limits.OverLoadTotalT4)
            {
                if (!txt_OverLoadTotal_T1.Visible && !txt_OverLoadTotal_T2.Visible &&
                    !txt_OverLoadTotal_T3.Visible && !txt_OverLoadTotal_T4.Visible)
                {
                    //pnl_OverLoadTotal.Enabled = false;
                    pnl_OverLoadTotal.Visible = false;
                }
            }

            #endregion
            #region MDIExceed

            if (qty == Limits.MDIExceedT1 ||
                   qty == Limits.MDIExceedT2 ||
                    qty == Limits.MDIExceedT3 ||
                    qty == Limits.MDIExceedT4)
            {
                if (!txt_DemandOverLoad_T1.Visible && !txt_DemandOverLoad_T2.Visible &&
                    !txt_DemandOverLoad_T3.Visible && !txt_DemandOverLoad_T4.Visible)
                {
                    //pnl_MDIExceed.Enabled = false;
                    pnl_MDIExceed.Visible = false;
                }
            }

            #endregion
        }

        #endregion

        private void lbl_limit_over_volt_Click(object sender, EventArgs e)
        {

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
