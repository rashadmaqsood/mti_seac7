using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using System.Reflection;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.DataContainer;
using SharedCode.Common;
using SEAC.Common;
using SharedCode.Comm.Param;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucKeepAlive : UserControl
    {
        #region Data_Members

        private Param_WakeUp_ProfileHelper _Param_WakeUp_ProfileHelperObj = null;
        private Param_Keep_Alive_IP _Param_Keep_Alive_IP_object = null;
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

                int total = Param_WakeUp_ProfileHelperObj.Total_Wakeup_Profile;

                try
                {
                    //Add Programmed Wakeup Profiles to Keep ALive Wakeup ID& Number Profile
                    combo_KeepAlive_WakeUPProfileID.Items.Clear();
                    for (int j = 1; j <= total; j++)
                    {
                        combo_KeepAlive_WakeUPProfileID.Items.Add(j);
                    }
                    if (combo_KeepAlive_WakeUPProfileID.Items.Count > 0 && Param_Keep_Alive_IP_object.Enabled
                        && Param_Keep_Alive_IP_object.IP_Profile_ID > total)
                        combo_KeepAlive_WakeUPProfileID.SelectedIndex = 0;
                    else
                        combo_KeepAlive_WakeUPProfileID.SelectedIndex = Param_Keep_Alive_IP_object.IP_Profile_ID - 1;
                }
                catch (Exception ex)
                {
                    Notification notifier = new Notification("Error Keep Alive Profile", ex.Message, 2000);
                }
                ///Register PropertyChannged Notification Event Handler
                //_Param_WakeUp_ProfileHelperObj.PropertyChanged -= _Param_WakeUp_ProfileHelperObj_PropertyChanged;
                //_Param_WakeUp_ProfileHelperObj.PropertyChanged += _Param_WakeUp_ProfileHelperObj_PropertyChanged;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_Keep_Alive_IP Param_Keep_Alive_IP_object
        {
            get { return _Param_Keep_Alive_IP_object; }
            set { _Param_Keep_Alive_IP_object = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<AccessRights> AccessRights { get; set; }

        public bool Modem_Warnings_disable
        {
            get { return _Modem_Warnings_disable; }
            set { _Modem_Warnings_disable = value; }
        }

        public bool IsValidated
        {
            get
            {
                if (errProvider != null && check_EnableKeepAlive.Enabled)
                {
                    String ErrorMessage = null;
                    ErrorMessage = errProvider.GetError(txt_KeepAlive_PingTimer);
                    if (!String.IsNullOrEmpty(ErrorMessage) ||
                                !String.IsNullOrWhiteSpace(ErrorMessage))
                        return false;
                    ErrorMessage = errProvider.GetError(combo_KeepAlive_WakeUPProfileID);
                    if (!String.IsNullOrEmpty(ErrorMessage) ||
                                !String.IsNullOrWhiteSpace(ErrorMessage))
                        return false;
                }
                return true;
            }
        }

        #endregion

        //public void _Param_WakeUp_ProfileHelperObj_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    try
        //    {
        //        ///Param_IP_Profiles_object
        //        if (e.PropertyName.Equals("Param_Wakeup_Profile_object"))
        //        {
        //            int total = Param_WakeUp_ProfileHelperObj.Total_Wakeup_Profile;

        //            //Add Programmed Wakeup Profiles to Keep ALive Wakeup ID& Number Profile
        //            combo_KeepAlive_WakeUPProfileID.Items.Clear();
        //            for (int j = 1; j <= total; j++)
        //            {
        //                combo_KeepAlive_WakeUPProfileID.Items.Add(j);
        //            }
        //            if (combo_KeepAlive_WakeUPProfileID.Items.Count > 0 && Param_Keep_Alive_IP_object.Enabled
        //                && Param_Keep_Alive_IP_object.IP_Profile_ID > total)
        //                combo_KeepAlive_WakeUPProfileID.SelectedIndex = 0;
        //            else
        //                combo_KeepAlive_WakeUPProfileID.SelectedIndex = Param_Keep_Alive_IP_object.IP_Profile_ID - 1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Notification notifier = new Notification("Error Keep Alive Profile", ex.Message, 2000);
        //    }
        //}

        private void ucKeepAlive_Load(object sender, EventArgs e)
        {
            ///Init Code
            if (_Param_WakeUp_ProfileHelperObj == null)
            {
                _Param_WakeUp_ProfileHelperObj = new Param_WakeUp_ProfileHelper();
                _Param_WakeUp_ProfileHelperObj.Previous_Total_Wakeup_profiles = 0;
            }
            if (_Param_Keep_Alive_IP_object == null)
                _Param_Keep_Alive_IP_object = new Param_Keep_Alive_IP();
        }

        public ucKeepAlive()
        {
            InitializeComponent();
            //Attach_Handlers();
            errProvider.SetIconAlignment(txt_KeepAlive_PingTimer, ErrorIconAlignment.MiddleLeft);
            errProvider.SetIconAlignment(combo_KeepAlive_WakeUPProfileID, ErrorIconAlignment.MiddleLeft);
           // _Param_WakeUp_ProfileHelperObj.PropertyChanged += _Param_WakeUp_ProfileHelperObj_PropertyChanged;
        }

        #region KeepAlive_LeaveEvents

        private void check_EnableKeepAlive_CheckedChanged(object sender, EventArgs e)
        {
            if (check_EnableKeepAlive.Checked)
            {
                check_HeartBeatOnConnection.Checked = Param_Keep_Alive_IP_object.
                                                                       GET_KeepAliveIP_Flag(Param_KeepAliveIPFlag.HeartBeatOnConnection);

                ///Param_Keep_Alive_IP_object.HeartBeatOnConnection;
                chk_enable_wakeup_KeepAlive.Checked = Param_Keep_Alive_IP_object.
                                                                       GET_KeepAliveIP_Flag(Param_KeepAliveIPFlag.EnableWakeupInKeepAliveMode);

                check_HeartBeatOnConnection.Enabled = true;
                chk_enable_wakeup_KeepAlive.Enabled = true;

                combo_KeepAlive_WakeUPProfileID.Enabled = true;
                txt_KeepAlive_PingTimer.Enabled = true;
                Param_Keep_Alive_IP_object.Enabled = true;
                if (Param_Keep_Alive_IP_object.IP_Profile_ID > 0)
                    combo_KeepAlive_WakeUPProfileID.SelectedIndex = Param_Keep_Alive_IP_object.IP_Profile_ID - 1;
                if (combo_KeepAlive_WakeUPProfileID.SelectedIndex == -1 && combo_KeepAlive_WakeUPProfileID.Items.Count > 0)
                    combo_KeepAlive_WakeUPProfileID.SelectedIndex = 0;
                txt_KeepAlive_PingTimer.Text = Param_Keep_Alive_IP_object.Ping_time.ToString();
            }
            else
            {
                Param_Keep_Alive_IP_object.Enabled = false;
                combo_KeepAlive_WakeUPProfileID.Enabled = false;
                txt_KeepAlive_PingTimer.Enabled = false;

                Param_Keep_Alive_IP_object.IP_Profile_ID = 0;
                combo_KeepAlive_WakeUPProfileID.SelectedIndex = -1;
                check_HeartBeatOnConnection.Enabled = false;
                chk_enable_wakeup_KeepAlive.Enabled = false;
            }
        }

        private void txt_KeepAlive_PingTimer_Leave(object sender, EventArgs e)
        {
            App_Validation_Info ValidationInfo = LocalCommon.AppValidationInfo;
            if (App_Validation.TextBox_RangeValidation(ValidationInfo.Param_KA_MinPingtime,
                ValidationInfo.Param_KA_MaxPingtime, txt_KeepAlive_PingTimer, ref errProvider))
            //if (SmartEyeControl_7.Common.Commons.TextBox_validation(30, 65535, txt_KeepAlive_PingTimer))
            {
                Param_Keep_Alive_IP_object.Ping_time = Convert.ToUInt16(txt_KeepAlive_PingTimer.Text);
            }
            //else
            //{
            //    txt_KeepAlive_PingTimer.Text = "30";
            //    Param_Keep_Alive_IP_object.Ping_time = Convert.ToUInt16(txt_KeepAlive_PingTimer.Text);
            //}
        }

        private void combo_KeepAlive_WakeUPProfileID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_KeepAlive_WakeUPProfileID.SelectedIndex != -1)
            {
                Param_Keep_Alive_IP_object.IP_Profile_ID = (byte)(combo_KeepAlive_WakeUPProfileID.SelectedIndex + 1);
            }
            else
            {
                Param_Keep_Alive_IP_object.IP_Profile_ID = 0;
                return;
            }
            #region Validate Param_Keep_Alive_IP_object.IP_Profile_ID

            bool isValidated = false;
            String ErrorMessage = String.Empty;
            Notification notifier = null;
            FieldInfo fInfo = typeof(Param_WakeUp_Profile).GetField("Wake_Up_Profile_ID");
            //Validate  Param_KeepAlive.IP_Profile_ID Lookup
            isValidated = App_Validation.Validate_LookupId(fInfo, _Param_WakeUp_ProfileHelperObj.Param_WakeUp_Profile_object,
                                                                  Param_Keep_Alive_IP_object.IP_Profile_ID);
            if (!isValidated)
            {
                ErrorMessage = String.Format("Validation failed,InConsistent Wake_Up_Profile_ID {0}", Param_Keep_Alive_IP_object.IP_Profile_ID);
            }
            if (!isValidated)
            {
                notifier = new Notification("InConsistent Param_Keep_Alive_IP_object", ErrorMessage, Notification.Sounds.Exclamation);
                errProvider.SetError(combo_KeepAlive_WakeUPProfileID, ErrorMessage);
            }
            else
                errProvider.SetError(combo_KeepAlive_WakeUPProfileID, string.Empty);

            #endregion
        }

        private void chk_enable_wakeup_KeepAlive_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_enable_wakeup_KeepAlive.Checked)
            {
                Param_Keep_Alive_IP_object.Param_KeepAliveIP_Flag |= Param_KeepAliveIPFlag.EnableWakeupInKeepAliveMode;
            }
            else
            {
                Param_Keep_Alive_IP_object.Param_KeepAliveIP_Flag |= Param_KeepAliveIPFlag.EnableWakeupInKeepAliveMode;
                Param_Keep_Alive_IP_object.Param_KeepAliveIP_Flag ^= Param_KeepAliveIPFlag.EnableWakeupInKeepAliveMode;
            }
        }

        private void check_HeartBeatOnConnection_CheckedChanged(object sender, EventArgs e)
        {
            if (check_HeartBeatOnConnection.Checked)
            {
                Param_Keep_Alive_IP_object.Param_KeepAliveIP_Flag |= Param_KeepAliveIPFlag.HeartBeatOnConnection;
            }
            else
            {
                Param_Keep_Alive_IP_object.Param_KeepAliveIP_Flag |= Param_KeepAliveIPFlag.HeartBeatOnConnection;
                Param_Keep_Alive_IP_object.Param_KeepAliveIP_Flag ^= Param_KeepAliveIPFlag.HeartBeatOnConnection;
            }
        }

        #endregion

        public void showToGUI_KeepAlive()
        {
            try
            {
                //Detach_Handlers();

                if (Param_Keep_Alive_IP_object.IP_Profile_ID > 0 || Param_Keep_Alive_IP_object.Enabled)
                {
                    check_EnableKeepAlive.Checked = true;
                }
                else
                {
                    check_EnableKeepAlive.Checked = false;
                    return;
                }

                txt_KeepAlive_PingTimer.Text = Convert.ToString(Param_Keep_Alive_IP_object.Ping_time);

                check_HeartBeatOnConnection.Checked = Param_Keep_Alive_IP_object.GET_KeepAliveIP_Flag(Param_KeepAliveIPFlag.HeartBeatOnConnection);
                chk_enable_wakeup_KeepAlive.Checked = Param_Keep_Alive_IP_object.GET_KeepAliveIP_Flag(Param_KeepAliveIPFlag.EnableWakeupInKeepAliveMode);

                if (Param_Keep_Alive_IP_object.IP_Profile_ID - 1 < 0)
                {
                    check_EnableKeepAlive.Checked = false;

                }
                else
                    combo_KeepAlive_WakeUPProfileID.SelectedIndex = Param_Keep_Alive_IP_object.IP_Profile_ID - 1;
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error show ucKeepAlive Profile", ex.Message, 5000);
            }
            finally
            {
                //Attach_Handlers();
            }
        }

        internal void Attach_Handlers()
        {
            try
            {
                check_EnableKeepAlive.CheckedChanged += check_EnableKeepAlive_CheckedChanged;
                txt_KeepAlive_PingTimer.Leave += txt_KeepAlive_PingTimer_Leave;
                combo_KeepAlive_WakeUPProfileID.SelectedIndexChanged += combo_KeepAlive_WakeUPProfileID_SelectedIndexChanged;
                check_HeartBeatOnConnection.CheckedChanged += check_HeartBeatOnConnection_CheckedChanged;
                chk_enable_wakeup_KeepAlive.CheckedChanged += chk_enable_wakeup_KeepAlive_CheckedChanged;
            }
            catch { throw; }
        }

        internal void Detach_Handlers()
        {
            try
            {
                check_EnableKeepAlive.CheckedChanged -= check_EnableKeepAlive_CheckedChanged;
                txt_KeepAlive_PingTimer.Leave -= txt_KeepAlive_PingTimer_Leave;
                combo_KeepAlive_WakeUPProfileID.SelectedIndexChanged -= combo_KeepAlive_WakeUPProfileID_SelectedIndexChanged;
                check_HeartBeatOnConnection.CheckedChanged -= check_HeartBeatOnConnection_CheckedChanged;
                chk_enable_wakeup_KeepAlive.CheckedChanged -= chk_enable_wakeup_KeepAlive_CheckedChanged;
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

                if (Rights.Find(x => x.Read == true || x.Write == true) != null)
                {
                    this.AccessRights = Rights;
                    foreach (var item in this.AccessRights)
                    {
                        _HelperAccessRights((KeepAlive)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
                    }
                    isSuccess = true;
                }
                else
                    return false;
            }
            finally
            {
                this.ResumeLayout();
                this.fLP_Main.ResumeLayout();
            }
            return isSuccess;
        }

        private void _HelperAccessRights(KeepAlive qty, bool read, bool write)
        {
            switch (qty)
            {
                case KeepAlive.Enable:
                    SetControlEnable(write, check_EnableKeepAlive);
                    check_EnableKeepAlive.Visible = (read || write);
                    break;
                case KeepAlive.WakeUpProfileID:
                    SetControlEnable(write, combo_KeepAlive_WakeUPProfileID);
                    fLP_WKID.Visible = (read || write);
                    break;
                case KeepAlive.PingTime:
                    txt_KeepAlive_PingTimer.ReadOnly = !write;
                    fLP_PingTime.Visible = (read || write);
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
                //KeepAlive.Enable
                if (check_EnableKeepAlive == Control)
                {
                    if (IsControlWTEnable(KeepAlive.Enable))
                        Control.Enabled = true;
                }
                //KeepAlive.WakeUpProfileID
                else if (combo_KeepAlive_WakeUPProfileID == Control)
                {
                    if (IsControlWTEnable(KeepAlive.WakeUpProfileID))
                        Control.Enabled = true;
                }
                //KeepAlive.PingTime
                else if (txt_KeepAlive_PingTimer == Control)
                {
                    if (IsControlWTEnable(KeepAlive.PingTime))
                        Control.Enabled = true;
                }
            }
        }

        private bool IsControlWTEnable(KeepAlive type)
        {
            bool isEnable = false;
            try
            {
                isEnable = ApplicationRight.IsControlWTEnable(typeof(KeepAlive), type.ToString(), AccessRights);
            }
            catch { }
            return isEnable;
        }

        private bool IsControlRDEnable(KeepAlive type)
        {
            bool isEnable = false;
            try
            {
                isEnable = ApplicationRight.IsControlRDEnable(typeof(KeepAlive), type.ToString(), AccessRights);
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
    }
}
