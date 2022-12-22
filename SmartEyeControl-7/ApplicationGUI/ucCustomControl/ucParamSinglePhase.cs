using SEAC.Common;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.Param;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using SmartEyeControl_7.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucParamSinglePhase : UserControl
    {
        #region Data_Members

        private LimitValues _limits = null;
        private Param_Limits _Param_Limits_object = null;
        private Param_Monitoring_Time _Param_Monitoring_time_object = null;
        private Param_MDI_parameters _Param_MDI_parameters_object = null;
        private Param_ErrorDetail _param_ErrorDetails = null;

        //internal static readonly StDateTime MDI_ResetTime;
        //internal static readonly StDateTime MDI_ResetDate;
        //StDateTime mdi_autoResetDate = new StDateTime();

        #endregion

        #region Properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_MDI_parameters Param_MDI_parameters_object
        {
            get { return _Param_MDI_parameters_object; }
            set { _Param_MDI_parameters_object = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_Monitoring_Time Param_Monitoring_time_object
        {
            get { return _Param_Monitoring_time_object; }
            set { _Param_Monitoring_time_object = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_Limits Param_Limits_object
        {
            get { return _Param_Limits_object; }
            set { _Param_Limits_object = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        internal LimitValues Limits
        {
            get { return _limits; }
            set { _limits = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_ErrorDetail Param_ErrorDetails
        {
            get { return _param_ErrorDetails; }
            set { _param_ErrorDetails = value; }
        }

        #endregion

        public ucParamSinglePhase()
        {
            InitializeComponent();
        }

        //static ucParamSinglePhase()
        //{
        //    MDI_ResetTime = new StDateTime();
        //    MDI_ResetTime.Kind = StDateTime.DateTimeType.Time;
        //    StDateTime.StDateTimeHelper.Save_WildCards(new List<StDateTimeWildCards>() 
        //    {StDateTimeWildCards.NullYear,StDateTimeWildCards.NullMonth,
        //     StDateTimeWildCards.NullDay,StDateTimeWildCards.NullDayOfWeek,StDateTimeWildCards.NullGMT},
        //     MDI_ResetTime);
        //    MDI_ResetDate = new StDateTime();
        //    MDI_ResetDate.Kind = StDateTime.DateTimeType.Date;
        //    StDateTime.StDateTimeHelper.Save_WildCards(new List<StDateTimeWildCards>() { StDateTimeWildCards.NullGMT }, MDI_ResetDate);
        //}

        private void gpParmSinglePhase_Enter(object sender, EventArgs e)
        { }

        #region Event_Handlers

        private void ucParamSinglePhase_Load(object sender, EventArgs e)
        {
            if (_Param_Limits_object == null)
                _Param_Limits_object = new Param_Limits();

            if (_limits == null)
                _limits = new LimitValues("R283");

            if (_Param_Monitoring_time_object == null)
                _Param_Monitoring_time_object = new Param_Monitoring_Time();

            if (_Param_MDI_parameters_object == null)
                _Param_MDI_parameters_object = new Param_MDI_parameters();

            if (_param_ErrorDetails == null)
                _param_ErrorDetails = new Param_ErrorDetail();

            //Attach Load MDI Reset EventHandlers
            //dtc_MDI_ResetDate_SinglePhase.Load += dtc_mdi_SinglePhase_Load;
            //dtc_MDI_ResetTime_SinglePhase.Load += dtc_mdi_SinglePhase_Load;
            //Attach Leave MDI Reset EventHandlers
            //dtc_MDI_ResetDate_SinglePhase.Leave += dtc_mdi_SinglePhase_Leave;
            //dtc_MDI_ResetTime_SinglePhase.Leave += dtc_mdi_SinglePhase_Leave;

            //Attach ucMDIAutoResetDateParam EventHandlers
            if (ucMDIAutoResetDateParam != null)
            {
                //Apply visual Settings here
                //ucMDIAutoResetDateParam.gpMDIResetParam.ForeColor = gpParmSinglePhase.ForeColor;
                ucMDIAutoResetDateParam.Load += new EventHandler(ucMDIAutoResetDateParam_Load);
                ucMDIAutoResetDateParam.Leave += new EventHandler(ucMDIAutoResetDateParam_Leave);
            }
        }

        private void txt_Limits_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
               && !char.IsDigit(e.KeyChar)
             && e.KeyChar != '.')
            { e.Handled = true; }
            // only allow one decimal point     
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            { e.Handled = true; }
        }

        public void txt_OverLoadTota_Leave(object sender, EventArgs e)
        {
            TextBox txt_OverLoadTotal_local = (TextBox)sender;

            if (LocalCommon.TextBox_validation(_limits.OverLoadTotal_T1_MIN,
                _limits.OverLoadTotal_T1_MAX, txt_OverLoadTotal_local))
            {
                _Param_Limits_object.OverLoadTotal_T1 = Convert.ToDouble(txt_OverLoadTotal_local.Text);
                _Param_Limits_object.OverLoadTotal_T1 *= 1000;
            }
        }

        private void txt_SP_overLoadTotal_TextChanged(object sender, EventArgs e)
        {
            txt_OverLoadTota_Leave(sender, e);
        }

        private void SP_DT_MT_PowerFail_Leave(object sender, EventArgs e)
        {
            _Param_Monitoring_time_object.PowerFail = SP_DT_MT_PowerFail.Value.TimeOfDay;
        }

        private void SP_DT_MT_Earth_Leave(object sender, EventArgs e)
        {
            _Param_Monitoring_time_object.PowerUpDelayEarth = SP_DT_MT_Earth.Value.TimeOfDay;
        }

        private void SP_DT_MT_ReverseEnergy_Leave(object sender, EventArgs e)
        {
            _Param_Monitoring_time_object.ReverseEnergy = SP_DT_MT_ReverseEnergy.Value.TimeOfDay;
        }

        //private void dtc_mdi_SinglePhase_Load(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ///Load Default Date_Time here
        //        dtc_MDI_ResetDate_SinglePhase.ContentControl.ValueCustom = MDI_ResetDate;
        //        dtc_MDI_ResetTime_SinglePhase.ContentControl.ValueCustom = MDI_ResetTime;
        //    }
        //    catch (Exception ex)
        //    {
        //        Notification n = new Notification("Invalid Date or Time for MDI AutoReset Date", ex.Message, 1500);
        //    }
        //}
        //private void dtc_mdi_SinglePhase_Leave(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Save_AutoResetDateTime();
        //        if (mdi_autoResetDate.IsValid)
        //            _Param_MDI_parameters_object.Auto_reset_date = mdi_autoResetDate;
        //        else
        //        {
        //            throw new Exception("Invalid Date or Time for MDI AutoReset");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Notification n = new Notification("Error Invalid Date or Time for MDI AutoReset Date", ex.Message, 1500);
        //    }
        //}

        void ucMDIAutoResetDateParam_Leave(object sender, EventArgs e)
        {
            try
            {
                var AutoResetDateTime = ucMDIAutoResetDateParam.StDateTime_Value;
                if (AutoResetDateTime.IsValid)
                {
                    _Param_MDI_parameters_object.Auto_reset_date = AutoResetDateTime;
                }
                else
                {
                    throw new Exception("Invalid Object, MDI AutoReset DateTime");
                }
            }
            catch (Exception ex)
            {
                Notification n = new Notification("Error Invalid Object, MDI AutoReset DateTime", ex.Message, 1500);
            }
        }

        void ucMDIAutoResetDateParam_Load(object sender, EventArgs e)
        {
            try
            {
                //Load Default Date_Time here
                //ucMDIAutoResetDateParam.MDI_AutoResetDateTime = MDI_ResetDate;
            }
            catch (Exception ex)
            {
                Notification n = new Notification("Invalid Object, MDI AutoReset DateTime", ex.Message, 1500);
            }
        }

        //private void Save_AutoResetDateTime()
        //{
        //    if (dtc_MDI_ResetDate_SinglePhase != null && dtc_MDI_ResetTime_SinglePhase != null)
        //    {
        //        mdi_autoResetDate = new StDateTime(dtc_MDI_ResetDate_SinglePhase.ContentControl.ValueCustom);
        //        mdi_autoResetDate.Kind = StDateTime.DateTimeType.DateTime;
        //        ///Init MDI AUTO RESET TIME
        //        mdi_autoResetDate.Hour = dtc_MDI_ResetTime_SinglePhase.ContentControl.ValueCustom.Hour;
        //        mdi_autoResetDate.Minute = dtc_MDI_ResetTime_SinglePhase.ContentControl.ValueCustom.Minute;
        //        mdi_autoResetDate.Second = dtc_MDI_ResetTime_SinglePhase.ContentControl.ValueCustom.Second;
        //        mdi_autoResetDate.Hundred = dtc_MDI_ResetTime_SinglePhase.ContentControl.ValueCustom.Hundred;
        //        mdi_autoResetDate.UTCOffset = dtc_MDI_ResetTime_SinglePhase.ContentControl.ValueCustom.UTCOffset;
        //    }
        //}

        #endregion

        public void showToGUI_Limits()
        {
            try
            {
                txt_SP_overLoadTotal.Text = LocalCommon.value_to_string(_Param_Limits_object.OverLoadTotal_T1 / 1000);
            }
            catch (Exception ex)
            {
                Notification n = new Notification("Error,Unable to Show Limits Parameters", ex.Message, 1500, Notification.Sounds.Hand);
            }
        }

        public void showToGUI_MonitoringTime()
        {
            try
            {
                SP_DT_MT_PowerFail.Text = Convert.ToString(Param_Monitoring_time_object.PowerFail);
                SP_DT_MT_Earth.Text = Convert.ToString(_Param_Monitoring_time_object.PowerUpDelayEarth);
                SP_DT_MT_ReverseEnergy.Text = Convert.ToString(_Param_Monitoring_time_object.ReverseEnergy);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error,Unable to Show Monitoring Time Parameters", ex.Message, 1500, Notification.Sounds.Hand);
            }
        }

        public void showToGUI_MDIParms()
        {
            try
            {
                //mdi_autoResetDate = Param_MDI_parameters_object.Auto_reset_date;
                //mdi_autoResetDate.Kind = StDateTime.DateTimeType.Date;
                //dtc_MDI_ResetDate_SinglePhase.ContentControl.ValueCustom = mdi_autoResetDate;
                //mdi_autoResetDate.Kind = StDateTime.DateTimeType.Time;
                //dtc_MDI_ResetTime_SinglePhase.ContentControl.ValueCustom = mdi_autoResetDate;
                //mdi_autoResetDate.Kind = StDateTime.DateTimeType.DateTime;
                ucMDIAutoResetDateParam.showToGUI_StDateTime(Param_MDI_parameters_object.Auto_reset_date);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error,Unable to Show MDI Parameters", ex.Message, 1500, Notification.Sounds.Hand);
            }
        }

        public void ShowToGUI_ErrorFlages()
        {
            try
            {
                if (!String.IsNullOrEmpty(Param_ErrorDetails.Text_EventDEtail))
                    txt_EventDEtail.Text = Param_ErrorDetails.Text_EventDEtail;
                this.check_BRE.Checked = Param_ErrorDetails.Check_BRE;
                this.check_CMMDI.Checked = Param_ErrorDetails.Check_CMMDI;

                this.check_RFU1.Checked = Param_ErrorDetails.Check_RFU1;
                this.check_RFU2.Checked = Param_ErrorDetails.Check_RFU2;
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error,Unable to Show Error Flags", ex.Message, 1500, Notification.Sounds.Hand);
            }
        }

        #region AccessControlMethods

        public bool ApplyAccessRights(List<AccessRights> Rights)
        {
            bool isAnyMntrChecked = false;
            try
            {
                this.SuspendLayout();
                this.fLP_Main_Mntr.SuspendLayout();
                this.fLP_Main.SuspendLayout();

                if (Rights.Find(x => x.Read == true || x.Write == true) != null)
                {
                    foreach (var item in Rights)
                    {
                        _HelperAccessRights((SinglePhase)Enum.Parse(item.QuantityType, item.QuantityName),
                            item.Read, item.Write);
                        if (!isAnyMntrChecked &&
                           (String.Equals(item.QuantityName, SinglePhase.MTEarth.ToString()) ||
                             String.Equals(item.QuantityName, SinglePhase.MTPhaseFail.ToString()) ||
                             String.Equals(item.QuantityName, SinglePhase.MTPhaseFail.ToString())))
                            isAnyMntrChecked = (item.Read || item.Write);
                    }
                    
                    if (isAnyMntrChecked)
                    {
                        gpMonitoring_Time.Visible = true;
                    }
                    else
                        gpMonitoring_Time.Visible = false;

                    return true;
                }
                return false;
            }
            finally
            {
                this.ResumeLayout();
                this.fLP_Main_Mntr.ResumeLayout();
                this.fLP_Main.ResumeLayout();
            }
        }

        private void _HelperAccessRights(SinglePhase qty, bool read, bool write)
        {
            switch (qty)
            {
                case SinglePhase.MTPhaseFail:
                    SP_DT_MT_PowerFail.Enabled = SP_btn_Set_MTPowerFail.Visible = SP_btn_Set_MTPowerFail.Enabled = write;
                    SP_DT_MT_PowerFail.Visible = SP_btn_Get_MTPowerFail.Visible = SP_btn_Set_MTPowerFail.Visible = read;

                    fLP_PowerFail.Visible = (write || read);
                    break;
                case SinglePhase.MTEarth:
                    SP_DT_MT_Earth.Enabled = SP_btn_Set_MTEarth.Visible = SP_btn_Set_MTEarth.Enabled = write;
                    SP_DT_MT_Earth.Visible = SP_btn_Get_MTEarth.Visible = SP_btn_Set_MTEarth.Visible = read;

                    fLP_Earth.Visible = (write || read);
                    break;
                case SinglePhase.MTReverseEnergy:
                    SP_DT_MT_ReverseEnergy.Enabled = SP_btn_Set_MTReverseEnergy.Visible = SP_btn_Set_MTReverseEnergy.Enabled = write;
                    SP_DT_MT_ReverseEnergy.Visible = SP_btn_Set_MTReverseEnergy.Visible = SP_btn_Get_MTReverseEnergy.Visible = read;

                    fLP_ReverseEnergy.Visible = (write || read);
                    break;
                case SinglePhase.LimitOverLoadTotal:
                    gpLimits.Enabled = write;
                    gpLimits.Visible = read;
                    if (read && !write)
                    {
                        gpLimits.Visible = true;
                        btn_SP_SetOverLoadTotal.Enabled = txt_SP_overLoadTotal.Enabled = false;
                    }
                    break;
                case SinglePhase.MDIAutoResetDateTime:
                    gpMDI.Enabled = write;
                    gpMDI.Visible = read;
                    if (read && !write)
                    {
                        gpMDI.Visible = true;
                        SP_btn_Set_MDI_AutoResetDate.Visible = ucMDIAutoResetDateParam.Visible = false;
                    }
                    break;
                case SinglePhase.ErrorFlags:
                    gpErrorFlages.Enabled = write;
                    gpErrorFlages.Visible = read;
                    if (read && !write)
                    {
                        gpErrorFlages.Visible = true;

                        //foreach (var item in gpErrorFlages.Controls)
                        //{
                        //    (item as Control).Enabled = false;
                        //}

                        SP_btn_getEventString.Enabled = true;
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion

        private void SP_btn_Get_MTPowerFail_Click(object sender, EventArgs e)
        {

        }
    }
}