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
    public partial class ucModemLimitsAndTime : UserControl
    {
        private Param_ModemLimitsAndTime _Param_ModemLimitsAndTime_Object = null;
        private bool _Modem_Warnings_disable = false;

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
                    foreach (Control itemCtr in gb_ModemLimitsAndTime.Controls)
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_ModemLimitsAndTime Param_ModemLimitsAndTime_Object
        {
            get { return _Param_ModemLimitsAndTime_Object; }
            set { _Param_ModemLimitsAndTime_Object = value; }
        }

        private void ucModemLimitsAndTime_Load(object sender, EventArgs e)
        {
            if (_Param_ModemLimitsAndTime_Object == null)
                _Param_ModemLimitsAndTime_Object = new Param_ModemLimitsAndTime();
            if (errorProvider != null)
            {
                foreach (var itemCtr in gb_ModemLimitsAndTime.Controls)
                {
                    if (itemCtr.GetType() == typeof(TextBox))
                        errorProvider.SetIconAlignment((TextBox)itemCtr, ErrorIconAlignment.MiddleLeft);
                }
                errorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            }
        }

        public ucModemLimitsAndTime()
        {
            InitializeComponent();
        }

        #region Modem_limits_and_time_leave_events

        private void txt_ModemLimitsAndTime_RSSI_Level_TCPUDP_Leave(object sender, EventArgs e)
        {
            if (App_Validation.TextBox_RangeValidation((ushort)0, LocalCommon.AppValidationInfo.RSSI_LEVEL_TCP_UDP_Connection_Max,
                txt_ModemLimitsAndTime_RSSI_Level_TCPUDP, ref errorProvider))
            {
                _Param_ModemLimitsAndTime_Object.RSSI_LEVEL_TCP_UDP_Connection = Convert.
                    ToUInt16(txt_ModemLimitsAndTime_RSSI_Level_TCPUDP.Text);
            }
        }

        private void txt_ModemLimitsAndTime_RSSI_Level_SMS_Leave(object sender, EventArgs e)
        {
            if (App_Validation.TextBox_RangeValidation((ushort)0, LocalCommon.AppValidationInfo.RSSI_LEVEL_SMS_Max,
                txt_ModemLimitsAndTime_RSSI_Level_SMS, ref errorProvider))
            {
                _Param_ModemLimitsAndTime_Object.RSSI_LEVEL_SMS = Convert.
                    ToUInt16(txt_ModemLimitsAndTime_RSSI_Level_SMS.Text);
            }
        }

        private void txt_ModemLimitsAndTime_RSSI_Level_Data_Call_Leave(object sender, EventArgs e)
        {
            if (App_Validation.TextBox_RangeValidation((ushort)0, LocalCommon.AppValidationInfo.RSSI_LEVEL_Data_Call_Max,
                txt_ModemLimitsAndTime_RSSI_Level_Data_Call, ref errorProvider))
            {
                _Param_ModemLimitsAndTime_Object.RSSI_LEVEL_Data_Call = Convert.
                    ToUInt16(txt_ModemLimitsAndTime_RSSI_Level_Data_Call.Text);
            }
        }

        private void txt_ModemLimitsAndTime_RetrySMS_Leave(object sender, EventArgs e)
        {
            if (App_Validation.TextBox_RangeValidation((ushort)0, LocalCommon.AppValidationInfo.Retry_SMS_Max,
                txt_ModemLimitsAndTime_RetrySMS, ref errorProvider))
            {
                _Param_ModemLimitsAndTime_Object.Retry_SMS = Convert.ToByte(txt_ModemLimitsAndTime_RetrySMS.Text);
            }
        }

        private void txt_ModemLimitsAndTime_Retry_Leave(object sender, EventArgs e)
        {
            if (App_Validation.TextBox_RangeValidation((ushort)0, LocalCommon.AppValidationInfo.Retry_Max,
                txt_ModemLimitsAndTime_Retry, ref errorProvider))
            {
                _Param_ModemLimitsAndTime_Object.Retry = Convert.ToByte(txt_ModemLimitsAndTime_Retry.Text);
            }
        }

        private void txt_ModemLimitsAndTime_RetryTCP_Leave(object sender, EventArgs e)
        {
            if (App_Validation.TextBox_RangeValidation((ushort)0, LocalCommon.AppValidationInfo.Retry_TCP_Max,
                txt_ModemLimitsAndTime_RetryTCP, ref errorProvider))
            {
                _Param_ModemLimitsAndTime_Object.Retry_TCP = Convert.ToByte(txt_ModemLimitsAndTime_RetryTCP.Text);
            }
        }

        private void txt_ModemLimitsAndTime_RetryUDP_Leave(object sender, EventArgs e)
        {
            if (App_Validation.TextBox_RangeValidation((ushort)0, LocalCommon.AppValidationInfo.Retry_UDP_Max,
                txt_ModemLimitsAndTime_RetryUDP, ref errorProvider))
            {
                _Param_ModemLimitsAndTime_Object.Retry_UDP = Convert.ToByte(txt_ModemLimitsAndTime_RetryUDP.Text);
            }
        }

        private void txt_ModemLimitsAndTime_TimeBetweenRetriesSMS_Leave(object sender, EventArgs e)
        {
            if (App_Validation.TextBox_RangeValidation((ushort)0, LocalCommon.AppValidationInfo.Time_between_Retries_SMS_Max,
                txt_ModemLimitsAndTime_TimeBetweenRetriesSMS, ref errorProvider))
            {
                _Param_ModemLimitsAndTime_Object.Time_between_Retries_SMS = Convert.
                    ToUInt16(txt_ModemLimitsAndTime_TimeBetweenRetriesSMS.Text);
            }
        }

        private void txt_ModemLimitsAndTime_TimeBetweenRetriesIPConnection_Leave(object sender, EventArgs e)
        {
            try
            {
                if (App_Validation.TextBox_RangeValidation((ushort)0,
                    LocalCommon.AppValidationInfo.Time_between_Retries_IP_connection_Max,
                    txt_ModemLimitsAndTime_TimeBetweenRetriesIPConnection, ref errorProvider))
                {
                    _Param_ModemLimitsAndTime_Object.Time_between_Retries_IP_connection = Convert.
                        ToUInt16(txt_ModemLimitsAndTime_TimeBetweenRetriesIPConnection.Text);
                }
            }
            catch { }
        }

        private void txt_ModemLimitsAndTime_TimeBetweenRetriesUDP_Leave(object sender, EventArgs e)
        {
            if (App_Validation.TextBox_RangeValidation((ushort)0, LocalCommon.AppValidationInfo.Time_between_Retries_UDP_Max,
                                txt_ModemLimitsAndTime_TimeBetweenRetriesUDP, ref errorProvider))
            {
                _Param_ModemLimitsAndTime_Object.Time_between_Retries_UDP = Convert.
                    ToUInt16(txt_ModemLimitsAndTime_TimeBetweenRetriesUDP.Text);
            }
        }

        private void txt_ModemLimitsAndTime_TimeBetweenRetriesTCP_Leave(object sender, EventArgs e)
        {
            if (App_Validation.TextBox_RangeValidation((ushort)0, LocalCommon.AppValidationInfo.Time_between_Retries_TCP_Max,
                                txt_ModemLimitsAndTime_TimeBetweenRetriesTCP, ref errorProvider))
            {
                _Param_ModemLimitsAndTime_Object.Time_between_Retries_TCP = Convert.
                    ToUInt16(txt_ModemLimitsAndTime_TimeBetweenRetriesTCP.Text);
            }
        }

        private void txt_ModemLimitsAndTime_TimeBetweenRetriesDataCall_Leave(object sender, EventArgs e)
        {
            if (App_Validation.TextBox_RangeValidation((ushort)0, LocalCommon.AppValidationInfo.Time_between_Retries_Data_Call_Max,
                                txt_ModemLimitsAndTime_TimeBetweenRetriesDataCall, ref errorProvider))
            {
                _Param_ModemLimitsAndTime_Object.Time_between_Retries_Data_Call = Convert.
                    ToUInt16(txt_ModemLimitsAndTime_TimeBetweenRetriesDataCall.Text);
            }
        }

        private void txt_ModemLimitsAndTime_TimeBetweenRetriesAlwaysOnCycle_Leave(object sender, EventArgs e)
        {
            if (App_Validation.TextBox_RangeValidation(LocalCommon.AppValidationInfo.TimeRetriesAlwaysOnCycle_Min,
                LocalCommon.AppValidationInfo.TimeRetriesAlwaysOnCycle_Max,
                txt_ModemLimitsAndTime_TimeBetweenRetriesAlwaysOnCycle, ref errorProvider))
            {
                _Param_ModemLimitsAndTime_Object.TimeRetriesAlwaysOnCycle = Convert.
                    ToUInt16(txt_ModemLimitsAndTime_TimeBetweenRetriesAlwaysOnCycle.Text);
            }
        }

        private void txt_ModemLimitsAndTime_Retry_IP_Leave(object sender, EventArgs e)
        {
            if (App_Validation.TextBox_RangeValidation((ushort)0, LocalCommon.AppValidationInfo.Retry_IP_connection_Max,
                txt_ModemLimitsAndTime_Retry_IP, ref errorProvider))
            {
                _Param_ModemLimitsAndTime_Object.Retry_IP_connection = Convert.ToByte(txt_ModemLimitsAndTime_Retry_IP.Text);
            }
        }

        private void txt_TCPInactivty_Leave(object sender, EventArgs e)
        {
            if (App_Validation.TextBox_RangeValidation(LocalCommon.AppValidationInfo.TCP_Inactivity_Min,
                LocalCommon.AppValidationInfo.TCP_Inactivity_Max,
                txt_TCPInactivty, ref errorProvider))
            {
                _Param_ModemLimitsAndTime_Object.TCP_Inactivity = Convert.ToUInt16(txt_TCPInactivty.Text);
            }
        }

        private void txt_TimeOutCipSend_Leave(object sender, EventArgs e)
        {
            if (App_Validation.TextBox_RangeValidation(LocalCommon.AppValidationInfo.TimeOut_CipSend_Min,
                LocalCommon.AppValidationInfo.TimeOut_CipSend_Max,
                txt_TimeOutCipSend, ref errorProvider))
            {
                _Param_ModemLimitsAndTime_Object.TimeOut_CipSend = Convert.ToUInt16(txt_TimeOutCipSend.Text);
            }
        }

        #endregion

        public void showToGUI_ModemLimitsAndTime()
        {
            try
            {
                txt_ModemLimitsAndTime_Retry.Text = Convert.ToString(Param_ModemLimitsAndTime_Object.Retry);
                txt_ModemLimitsAndTime_RetrySMS.Text = Convert.ToString(Param_ModemLimitsAndTime_Object.Retry_SMS);
                txt_ModemLimitsAndTime_RetryTCP.Text = Convert.ToString(Param_ModemLimitsAndTime_Object.Retry_TCP);
                txt_ModemLimitsAndTime_RetryUDP.Text = Convert.ToString(Param_ModemLimitsAndTime_Object.Retry_UDP);
                txt_ModemLimitsAndTime_Retry_IP.Text = Convert.ToString(Param_ModemLimitsAndTime_Object.Retry_IP_connection);
                txt_ModemLimitsAndTime_RSSI_Level_Data_Call.Text = Convert.ToString(Param_ModemLimitsAndTime_Object.RSSI_LEVEL_Data_Call);
                txt_ModemLimitsAndTime_RSSI_Level_SMS.Text = Convert.ToString(Param_ModemLimitsAndTime_Object.RSSI_LEVEL_SMS);
                txt_ModemLimitsAndTime_RSSI_Level_TCPUDP.Text = Convert.ToString(Param_ModemLimitsAndTime_Object.RSSI_LEVEL_TCP_UDP_Connection);
                txt_ModemLimitsAndTime_TimeBetweenRetriesDataCall.Text = Convert.ToString(Param_ModemLimitsAndTime_Object.Time_between_Retries_Data_Call);
                txt_ModemLimitsAndTime_TimeBetweenRetriesIPConnection.Text = Convert.ToString(Param_ModemLimitsAndTime_Object.Time_between_Retries_IP_connection);
                txt_ModemLimitsAndTime_TimeBetweenRetriesSMS.Text = Convert.ToString(Param_ModemLimitsAndTime_Object.Time_between_Retries_SMS);
                txt_ModemLimitsAndTime_TimeBetweenRetriesTCP.Text = Convert.ToString(Param_ModemLimitsAndTime_Object.Time_between_Retries_TCP);
                txt_ModemLimitsAndTime_TimeBetweenRetriesUDP.Text = Convert.ToString(Param_ModemLimitsAndTime_Object.Time_between_Retries_UDP);
                txt_ModemLimitsAndTime_TimeBetweenRetriesAlwaysOnCycle.Text = Convert.ToString(Param_ModemLimitsAndTime_Object.TimeRetriesAlwaysOnCycle);
                txt_TCPInactivty.Text = Param_ModemLimitsAndTime_Object.TCP_Inactivity.ToString();
                txt_TimeOutCipSend.Text = Param_ModemLimitsAndTime_Object.TimeOut_CipSend.ToString();

                //combo_NumberProfile_TotalProfiles.SelectedIndex = 0;
                //combo_NumberProfile_UniqueID.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error show Param_ModemLimitsAndTime", ex.Message, 5000);
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
                        _HelperAccessRights((ModemLimit)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
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

        private void _HelperAccessRights(ModemLimit qty, bool read, bool write)
        {
            switch (qty)
            {
                case ModemLimit.RSSILevelConnection:
                    txt_ModemLimitsAndTime_RSSI_Level_TCPUDP.ReadOnly = !write;
                    fLP_RSSILevel_TCPUDP.Visible = (read || write);
                    break;
                case ModemLimit.RSSILevelSMS:
                    txt_ModemLimitsAndTime_RSSI_Level_SMS.ReadOnly = !write;
                    fLP_RSSILevel_SMS.Visible = (read || write);
                    break;
                case ModemLimit.RSSILevelDataCall:
                    txt_ModemLimitsAndTime_RSSI_Level_Data_Call.ReadOnly = !write;
                    fLP_RSSILevel_DataCall.Visible = (read || write);
                    break;
                case ModemLimit.RetriesIPConnection:
                    txt_ModemLimitsAndTime_Retry_IP.ReadOnly = !write;
                    fLP_RetryIPConn.Visible = (read || write);
                    break;
                case ModemLimit.RetriesSMS:
                    txt_ModemLimitsAndTime_RetrySMS.ReadOnly = !write;
                    fLP_RetrySMS.Visible = read || write;
                    break;
                case ModemLimit.RetriesTCPdata:
                    txt_ModemLimitsAndTime_RetryTCP.ReadOnly = !write;
                    fLP_RetryTCPData.Visible = read || write;
                    break;
                case ModemLimit.RetriesUDPdata:
                    txt_ModemLimitsAndTime_RetryUDP.ReadOnly = !write;
                    fLP_RetryUDPData.Visible = read || write;
                    break;
                case ModemLimit.RetriesDataCall:
                    txt_ModemLimitsAndTime_Retry.ReadOnly = !write;
                    fLP_RetryDataCall.Visible = read || write;
                    break;
                case ModemLimit.TimebetweenRetriesSMS:
                    txt_ModemLimitsAndTime_TimeBetweenRetriesSMS.ReadOnly = !write;
                    fLP_TimeBWRetrySMS.Visible = read || write;
                    break;
                case ModemLimit.TimebetweenIPConnection:
                    txt_ModemLimitsAndTime_TimeBetweenRetriesIPConnection.ReadOnly = !write;
                    fLP_TimeBWRetryIPConn.Visible = read || write;
                    break;
                case ModemLimit.TimebetweenRetriesUDPdata:
                    txt_ModemLimitsAndTime_TimeBetweenRetriesUDP.ReadOnly = !write;
                    fLP_TimeBWRetryUDPData.Visible = read || write;
                    break;
                case ModemLimit.TimebetweenRetriesTCPdata:
                    txt_ModemLimitsAndTime_TimeBetweenRetriesTCP.ReadOnly = !write;
                    fLP_TimeBWRetryTCPData.Visible = read || write;
                    break;
                case ModemLimit.TimebetweenRetriesDataCall:
                    txt_ModemLimitsAndTime_TimeBetweenRetriesDataCall.ReadOnly = !write;
                    fLP_TimeBWRetryDataCall.Visible = read || write;
                    break;
                case ModemLimit.TimebetweenRetryAlwaysOnCycle:
                    txt_ModemLimitsAndTime_TimeBetweenRetriesAlwaysOnCycle.ReadOnly = !write;
                    fLP_TimeBWRetryAlways.Visible = read || write;
                    break;
                case ModemLimit.TCPInactivity:
                    txt_TCPInactivty.ReadOnly = !write;
                    fLP_TCPInactivity.Visible = read || write;
                    break;
                case ModemLimit.TimeOutCipSend:
                    txt_TimeOutCipSend.ReadOnly = !write;
                    fLP_TimeOutCipSend.Visible = read || write;
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
