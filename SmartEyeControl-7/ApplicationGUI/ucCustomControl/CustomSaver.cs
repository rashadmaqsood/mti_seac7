using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SharedCode.Comm.HelperClasses;

namespace SmartEyeControl_7.ApplicationGUI.GUI
{
    public partial class CustomSetGet : Form
    {
        public ApplicationRight AccessRights { get; set; }

        private bool isSinglePhase = false;

        private bool isAllChecked = false;

        //public bool IsReadMode = false;
        public int IsReadMode = -1; //0=Report, 1=Get, 2=Set

        //public bool IsAllChecked
        //{
        //    get
        //    {
        //        foreach (var item in MeterSetting.Controls)
        //        {
        //            if (item is CheckBox)
        //            {
        //                if (((CheckBox)item).Checked)
        //                {

        //                }
        //                else
        //                {
        //                    if (((CheckBox)item).Name == "check_Password_Elec")
        //                    {
        //                        continue;
        //                    }
        //                    else
        //                    {
        //                        return false;
        //                    }
        //                }

        //            }
        //        }
        //        foreach (var item in TBESetting.Controls)
        //        {
        //            if (item is CheckBox)
        //            {
        //                if (((CheckBox)item).Checked)
        //                {

        //                }
        //                else
        //                {
        //                    return false;
        //                }

        //            }
        //        }
        //        foreach (var item in ModemSetting.Controls)
        //        {
        //            if (item is CheckBox)
        //            {
        //                if (((CheckBox)item).Checked)
        //                {

        //                }
        //                else
        //                {
        //                    return false;
        //                }

        //            }
        //        }
        //        return true;
        //    }
        //}

        public CustomSetGet(string formName, bool isSingle)
        {
            InitializeComponent();
            this.isSinglePhase = isSingle;
            RESET_ALL();
            this.Text = formName;
        }

        public CustomSetGet(string formName, bool isSingle, ApplicationRight _AccessRights, int _isReadMode)
        {
            InitializeComponent();
            this.isSinglePhase = isSingle;
            RESET_ALL();

            AccessRights = _AccessRights;
            IsReadMode = _isReadMode;
            this.Text = formName;
            if (IsReadMode == 0) //Means Report View
            {
                SetAccessRightsForRead(AccessRights);
                //btn_CustomSelectAll_Click(btn_CustomSelectAll, new EventArgs());
                //this.DialogResult = DialogResult.OK;
            }
            else if (IsReadMode == 1) //Get
            {
                this.BackgroundImage = OptocomSoftware.Properties.Resources.Button_BG;
                SetAccessRightsForRead(AccessRights);
            }
            if (IsReadMode == 2) //Set
            {
                this.BackgroundImage = OptocomSoftware.Properties.Resources.Buttn_BG_on_focus;
                SetAccessRightsForWrite(AccessRights);

                if (AccessRights.DisplayWindowsRights.Find(x => x.QuantityName == "DisplayWindowsNormal" && x.Write) != null)
                {
                }
                else if (AccessRights.DisplayWindowsRights.Find(x => x.QuantityName == "ScrollTime" && x.Write) != null)
                {
                    check_DisplayWindows_Nor.Text = "Normal Window Scroll Time";
                }
            }
        }

        private void btn_CustomSave_Click(object sender, EventArgs e)
        {

        }

        private void btn_CustomSelectAll_Click(object sender, EventArgs e)
        {
            if (!isSinglePhase)
            {
                //RESET_ALL();
                foreach (var c in flpSecurityKeys.Controls)
                {
                    if (c.GetType() == typeof(CheckBox))
                    {
                        ((CheckBox)c).Checked = flpSecurityKeys.Visible ? true : false;
                        ((CheckBox)c).Enabled = flpSecurityKeys.Visible ? true : false;
                    }
                }
                foreach (var c in flpStandardModem.Controls)
                {
                    if (c.GetType() == typeof(CheckBox))
                    {
                        ((CheckBox)c).Checked = flpStandardModem.Visible ? true : false;
                        ((CheckBox)c).Enabled = flpStandardModem.Visible ? true : false;
                    }
                }

                foreach (var c in flpTimeEvents.Controls)
                {
                    if (c.GetType() == typeof(CheckBox))
                    {
                        ((CheckBox)c).Checked = flpTimeEvents.Visible ? true : false;
                        ((CheckBox)c).Enabled = flpTimeEvents.Visible ? true : false;
                    }
                }
                foreach (var c in fLP_MeterSettings.Controls)
                {
                    if (c.GetType() == typeof(CheckBox))
                    {
                        ((CheckBox)c).Checked = fLP_MeterSettings.Visible ? true : false;
                        ((CheckBox)c).Enabled = fLP_MeterSettings.Visible ? true : false;
                    }
                }
                foreach (var c in flp_ModemSettings.Controls)
                {
                    if (c.GetType() == typeof(CheckBox))
                    {
                        ((CheckBox)c).Checked = flp_ModemSettings.Visible ? true : false;
                        ((CheckBox)c).Enabled = flp_ModemSettings.Visible ? true : false;
                    }
                }

                //SET_CheckBox(check_ActivityCalender, true);
                //SET_CheckBox(check_Clock, true);
                //SET_CheckBox(check_IP_Profile, true);
                //SET_CheckBox(chbWakeupProfile, true);
                //SET_CheckBox(chbNumberProfile, true);
                //SET_CheckBox(chbKeepAlive, true);
                //SET_CheckBox(chbModemLimitsAndTime, true);
                //SET_CheckBox(chbModemInitialize, true);
                //SET_CheckBox(chbCommunicationProfile, true);
                //SET_CheckBox(check_Contactor, true);
                //SET_CheckBox(check_CTPT, true);
                //SET_CheckBox(check_customerReference, true);
                //SET_CheckBox(check_DataProfilewithEvents, true);
                //SET_CheckBox(check_DecimalPoint, true);
                //SET_CheckBox(check_DisplayWindows_Alt, true);
                //SET_CheckBox(check_DisplayWindows_Nor, true);
                //SET_CheckBox(check_DisplayWindows_test, true);
                //SET_CheckBox(check_DisplayPowerDown, true);
                //SET_CheckBox(check_EnergyParams, true);
                //SET_CheckBox(check_EventCaution, true);
                //SET_CheckBox(check_IPV4, true);
                //SET_CheckBox(check_Limits, true);
                //SET_CheckBox(chk_LoadProfile, true);
                //SET_CheckBox(chk_LoadProfile_Interval, true);
                //SET_CheckBox(chk_PQ_LoadProfileInterval, true);
                //SET_CheckBox(chk_LoadProfile_2, true);
                //SET_CheckBox(chk_LoadProfile_2_Interval, true);
                //SET_CheckBox(check_MajorAlarmprofile, true);
                //SET_CheckBox(check_MDI_params, true);
                //SET_CheckBox(check_MonitoringTime, true);
                //SET_CheckBox(check_TCPUDP, true);
                ////SET_CheckBox(check_Password, true);
                ////SET_CheckBox(check_Password_Elec, true);
                //SET_CheckBox(check_Time, true);
                //SET_CheckBox(check_TBEs, true);
                //SET_CheckBox(chk_GPP, true);
                //SET_CheckBox(check_DisplayPowerDown, true);
                //SET_CheckBox(chbStatusWordMap1, true);
                //SET_CheckBox(chbStatusWordMap2, true);
                //SET_CheckBox(check_StandardModem_IP_Profile, true);
                //SET_CheckBox(check_StandardModem_KeepAlive, true);
                //SET_CheckBox(check_StandardModem_Number_Profile, true);
                //SET_CheckBox(check_loadShedding, true);
                //SET_CheckBox(check_GeneratorStart, true);
            }
            else
            {
                SET_CheckBox(check_IP_Profile, true);
                SET_CheckBox(check_Password_Elec, true);
                SET_CheckBox(check_customerReference, true);
            }

            //if (IsReadMode == 0)
            //{
            //    SetAccessRightsForRead(AccessRights);
            //    //this.DialogResult = DialogResult.OK;
            //}
            //else if (IsReadMode == 1)
            //{
            //    SetAccessRightsForRead(AccessRights);
            //}
            //else if (IsReadMode == 2)
            //{
            //    SetAccessRightsForWrite(AccessRights);
            //}
            #region Access Control



            #endregion

        }

        private void restore_enable_status()
        {
            if (!isSinglePhase)
            {
                SET_CheckBox(check_ActivityCalender, true);
                SET_CheckBox(check_Clock, true);
                SET_CheckBox(check_IP_Profile, true);
                SET_CheckBox(check_Contactor, true);
                SET_CheckBox(check_CTPT, true);
                SET_CheckBox(check_customerReference, true);
                SET_CheckBox(check_DataProfilewithEvents, true);
                SET_CheckBox(check_DecimalPoint, true);
                SET_CheckBox(check_DisplayWindows_Alt, true);
                SET_CheckBox(check_DisplayWindows_Nor, true);
                SET_CheckBox(check_DisplayWindows_test, true);
                SET_CheckBox(check_DisplayPowerDown, true);
                SET_CheckBox(check_EnergyParams, true);
                SET_CheckBox(check_EventCaution, true);
                SET_CheckBox(check_IPV4, true);
                SET_CheckBox(check_Limits, true);
                SET_CheckBox(chk_LoadProfile, true);
                SET_CheckBox(chk_LoadProfile_Interval, true);
                SET_CheckBox(chk_PQ_LoadProfileInterval, true);
                SET_CheckBox(chk_LoadProfile_2, true);
                SET_CheckBox(chk_LoadProfile_2_Interval, true);
                SET_CheckBox(check_MajorAlarmprofile, true);
                SET_CheckBox(check_MDI_params, true);
                SET_CheckBox(check_MonitoringTime, true);
                SET_CheckBox(check_TCPUDP, true);
                // SET_CheckBox(check_Passwords, true);
                SET_CheckBox(check_Time, true);
                SET_CheckBox(check_TBEs, true);
                SET_CheckBox(chk_GPP, true);
                SET_CheckBox(check_DisplayPowerDown, true);
                SET_CheckBox(chbStatusWordMap1, true);
                SET_CheckBox(chbStatusWordMap2, true);
                SET_CheckBox(check_StandardModem_IP_Profile, true);
                SET_CheckBox(check_StandardModem_KeepAlive, true);
                SET_CheckBox(check_StandardModem_Number_Profile, true);

                SET_CheckBox(chbWakeupProfile, true);
                SET_CheckBox(chbNumberProfile, true);
                SET_CheckBox(chbKeepAlive, true);
                SET_CheckBox(chbModemLimitsAndTime, true);
                SET_CheckBox(chbModemInitialize, true);
                SET_CheckBox(chbCommunicationProfile, true);
                SET_CheckBox(check_GeneratorStart, true);

            }
            else
            {
                SET_CheckBox(check_IP_Profile, true);
                SET_CheckBox(check_Password_Elec, true);
                SET_CheckBox(check_customerReference, true);
            }
        }

        public void setEnableStatus()
        {
            foreach (var item in this.Controls)
            {
                if (item is CheckBox)
                {
                    if (((CheckBox)item).Checked)
                    {
                        ((CheckBox)item).Enabled = true;
                    }
                    else
                    {
                        ((CheckBox)item).Enabled = false;
                    }
                }
                else
                {
                    foreach (var itemSubControl in ((Control)item).Controls)
                    {
                        if (itemSubControl is CheckBox)
                        {
                            if (((CheckBox)itemSubControl).Checked)
                            {
                                ((CheckBox)itemSubControl).Enabled = true;
                            }
                            else
                            {
                                ((CheckBox)itemSubControl).Enabled = false;
                            }
                        }
                    }
                }
            }
            //foreach (Control c in this.Controls)
            //{
            //    if (c is CheckBox)
            //    {
            //        CheckBox chk = (CheckBox)c; ;
            //        if (((CheckBox)c).Checked)
            //        {
            //            ((CheckBox)c).Enabled = true;
            //        }
            //        else
            //        {
            //            ((CheckBox)c).Enabled = false;
            //        }

            //    }
            //}

        }
        private void btn_CustomDeselect_Click(object sender, EventArgs e)
        {
            RESET_ALL();
        }

        private void btn_CustomGet_Click(object sender, EventArgs e)
        {

        }

        public bool IsAnyChecked
        {
            get
            {
                try
                {

                    //if (isSinglePhase)
                    //{
                    //    bool isPasswordChecked = check_Password_Elec.Checked;
                    //    bool isCustomerRefChecked = check_customerReference.Checked;
                    //    //bool isModemParamChecked = check_ModemParams.Checked;
                    //    bool isTimeBasedEventsChecked = check_TBEs.Checked;
                    //    bool isContactorChecked = check_Contactor.Checked;
                    //    RESET_ALL();

                    //    if (isPasswordChecked)
                    //    {
                    //        SET_CheckBox(check_Password_Elec, true);
                    //    }
                    //    if (isCustomerRefChecked)
                    //    {
                    //        SET_CheckBox(check_customerReference, true);
                    //    }
                    //    if (isModemParamChecked)
                    //    {
                    //        SET_CheckBox(check_ModemParams, true);
                    //    }
                    //    if (isTimeBasedEventsChecked)
                    //    {
                    //        SET_CheckBox(check_TBEs, true);
                    //    }
                    //    if (isContactorChecked)
                    //    {
                    //        SET_CheckBox(check_Contactor, true);
                    //    }
                    //}

                    bool IsAnySingleChecked = false;
                    List<CheckBox> Checks = new List<CheckBox>();
                    Stack<Control> SubControls = new Stack<Control>(500);
                    Control Contr = null;

                    foreach (Control item in this.Controls)
                    {
                        SubControls.Push(item);
                    }

                    while (SubControls.Count > 0)
                    {
                        Contr = SubControls.Pop();
                        if (Contr is CheckBox && !Checks.Contains(Contr))
                        {
                            Checks.Add((CheckBox)Contr);

                            if (((CheckBox)Contr).Checked)
                            {
                                IsAnySingleChecked = true;
                                break;
                            }
                        }
                        else if (!(Contr is CheckBox))
                        {
                            foreach (var itemSubControl in ((Control)Contr).Controls)
                            {
                                if (!SubControls.Contains(itemSubControl))
                                    SubControls.Push((Control)itemSubControl);
                            }
                        }
                    }

                    if (!IsAnySingleChecked && Checks.Count > 0)
                    {
                        foreach (var item in Checks)
                        {
                            if (((CheckBox)Contr).Checked)
                            {
                                IsAnySingleChecked = true;
                                break;
                            }
                        }
                    }

                    return IsAnySingleChecked;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public void RESET_ALL()
        {

            check_ActivityCalender.Checked = false;
            check_Clock.Checked = false;
            check_IP_Profile.Checked = false;
            check_Contactor.Checked = false;
            check_CTPT.Checked = false;
            check_customerReference.Checked = false;
            check_DataProfilewithEvents.Checked = false;
            check_DecimalPoint.Checked = false;
            check_DisplayWindows_Nor.Checked = false;
            check_DisplayWindows_Alt.Checked = false;
            check_DisplayWindows_test.Checked = false;
            check_DisplayPowerDown.Checked = false;
            check_EnergyParams.Checked = false;
            check_EventCaution.Checked = false;
            check_IPV4.Checked = false;
            check_Limits.Checked = false;
            chk_LoadProfile.Checked = false;
            chk_LoadProfile_Interval.Checked = false;
            chk_PQ_LoadProfileInterval.Checked = false;
            chk_LoadProfile_2.Checked = false;
            chk_LoadProfile_2_Interval.Checked = false;
            check_MajorAlarmprofile.Checked = false;
            check_MDI_params.Checked = false;
            check_MonitoringTime.Checked = false;
            check_TCPUDP.Checked = false;
            check_Password_Elec.Checked = false;
            check_Time.Checked = false;
            check_TBEs.Checked = false;
            chk_GPP.Checked = false;
            chbStatusWordMap1.Checked = false;
            chbStatusWordMap2.Checked = false;

            chbWakeupProfile.Checked = false;
            chbNumberProfile.Checked = false;
            chbKeepAlive.Checked = false;
            chbModemLimitsAndTime.Checked = false;
            chbModemInitialize.Checked = false;
            chbCommunicationProfile.Checked = false;

            check_StandardModem_IP_Profile.Checked = false;
            check_StandardModem_KeepAlive.Checked = false;
            check_StandardModem_Number_Profile.Checked = false;

            check_loadShedding.Checked = false;

            check_GeneratorStart.Checked = false;
        }

        public void SET_CheckBox(CheckBox _CheckBox, bool _Checked)
        {
            if ((this.Controls.Contains(gbMeterSetting)     && fLP_MeterSettings.Controls.Contains(_CheckBox))  ||
                (this.Controls.Contains(gbModemSetting)     && flp_ModemSettings.Controls.Contains(_CheckBox))  ||
                (this.Controls.Contains(gbStandardModem)    && flpStandardModem.Controls.Contains(_CheckBox))   ||
                (this.Controls.Contains(gbTBESetting)       && flpTimeEvents.Controls.Contains(_CheckBox))      ||
                (this.Controls.Contains(gbSecurityKeys)     && flpSecurityKeys.Controls.Contains(_CheckBox))
                )
            {
                if (_CheckBox.Enabled && _CheckBox.Visible)
                {
                    _CheckBox.Checked = _Checked;
                }
                //else
                //{
                //    //if (fLP_MeterSettings.Controls.Contains(_CheckBox))
                //    //{
                //    //_CheckBox.Checked = false;
                //    //}
                //} 
            }
            else
            {
                _CheckBox.Checked = false;
            }
        }

        private void CustomSetGet_Load(object sender, EventArgs e)
        {
            //setEnableStatus();
            if (IsReadMode == 0)
            {
                SetAccessRightsForRead(AccessRights);
                //btn_CustomSelectAll_Click(btn_CustomSelectAll, new EventArgs());
                //this.DialogResult = DialogResult.OK;
            }
            else if (IsReadMode == 1)
            {
                SetAccessRightsForRead(AccessRights);
            }
            else if (IsReadMode == 2)
            {
                SetAccessRightsForWrite(AccessRights);

                if (AccessRights.DisplayWindowsRights.Find(x => x.QuantityName == "DisplayWindowsNormal" && x.Write) != null)
                {
                }
                else if (AccessRights.DisplayWindowsRights.Find(x => x.QuantityName == "ScrollTime" && x.Write) != null)
                {
                    check_DisplayWindows_Nor.Text = "Normal Window Scroll Time";
                }
            }

        }

        public void SetAccessRightsForRead(ApplicationRight Rights)
        {
            //_________________________________Activity Calender_________________________________
            if (Rights.MeterRights.Find(x => x.QuantityName == MeterRights.ActivityCalender.ToString()
                && x.Read == true) == null)
                fLP_MeterSettings.Controls.Remove(check_ActivityCalender);
            //____________________________MISC Parameters_________________________
            if ((Rights.MiscRights.Find(x => x.QuantityName == Misc.CT_Ratio.ToString() && x.Read == true) == null) &&
                    (Rights.MeterRights.Find(x => x.QuantityName == Misc.PT_Ratio.ToString() && x.Read == true) == null))
            {
                fLP_MeterSettings.Controls.Remove(check_CTPT);
            }
            if (Rights.MiscRights.Find(x => x.QuantityName == Misc.Clock.ToString()
                && x.Read == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(check_Clock);
            }
            if (Rights.MiscRights.Find(x => x.QuantityName == Misc.CustomerReference.ToString()
                && x.Read == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(check_customerReference);
            }

            if (Rights.MiscRights.Find(x => x.QuantityName == Misc.AssociationPassword.ToString()
                && x.Read == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(check_Password_Elec);
            }
            if (Rights.MiscRights.Find(x => x.QuantityName == Misc.EnergyParam.ToString()
                && x.Read == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(check_EnergyParams);
            }
            if (Rights.MiscRights.Find(x => x.QuantityName == Misc.GernalProcessParameter.ToString()
                && x.Read == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(chk_GPP);
            }

            //_________________________________Decimal Point_____________________________
            if (
                (Rights.MiscRights.Find(x => x.QuantityName == Misc.DecimalPoint_BillEnergy.ToString() && x.Read == true) == null) &&
                (Rights.MiscRights.Find(x => x.QuantityName == Misc.DecimalPoint_BillMdi.ToString() && x.Read == true) == null) &&
                (Rights.MiscRights.Find(x => x.QuantityName == Misc.DecimalPoint_InstCurrent.ToString() && x.Read == true) == null) &&
                (Rights.MiscRights.Find(x => x.QuantityName == Misc.DecimalPoint_InstMdi.ToString() && x.Read == true) == null) &&
                (Rights.MiscRights.Find(x => x.QuantityName == Misc.DecimalPoint_InstPower.ToString() && x.Read == true) == null) &&
                (Rights.MiscRights.Find(x => x.QuantityName == Misc.DecimalPoint_InstVolt.ToString() && x.Read == true) == null)
            )
            {
                fLP_MeterSettings.Controls.Remove(check_DecimalPoint);
            }
            //_________________________________Contactor_________________________________
            if (Rights.MeterRights.Find(x => x.QuantityName == MeterRights.Contactor.ToString()
                && x.Read == true) == null)
                fLP_MeterSettings.Controls.Remove(check_Contactor);

            //____________________________Display Windows_________________________
            //if (Rights.MeterRights.Find(x => x.QuantityName == MeterRights.DisplayWindows.ToString()
            //    && x.Read == true) == null)
            //{
            //    fLP_MeterSettings.Controls.Remove(check_DisplayWindows_Alt);
            //    fLP_MeterSettings.Controls.Remove(check_DisplayWindows_Nor);
            //    fLP_MeterSettings.Controls.Remove(check_DisplayWindows_test);
            //}
            //v5.3.12
            if (Rights.DisplayWindowsRights.Find(x => x.QuantityName == DisplayWindowsParams.DisplayWindowsNormal.ToString()
                && x.Read == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(check_DisplayWindows_Nor);
            }
            if (Rights.DisplayWindowsRights.Find(x => x.QuantityName == DisplayWindowsParams.DisplayWindowsAlternate.ToString()
                && x.Read == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(check_DisplayWindows_Alt);
            }
            if (Rights.DisplayWindowsRights.Find(x => x.QuantityName == DisplayWindowsParams.DisplayWindowsTest.ToString()
                && x.Read == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(check_DisplayWindows_test);
            }

            //_________________________________Display Power Down_________________________________
            if (Rights.MeterRights.Find(x => x.QuantityName == MeterRights.DisplayPowerDown.ToString()
                && x.Read == true) == null)
                fLP_MeterSettings.Controls.Remove(check_DisplayPowerDown);
            //_________________________________Limits_________________________________
            if (Rights.MeterRights.Find(x => x.QuantityName == MeterRights.Limits.ToString()
                && x.Read == true) == null)
                fLP_MeterSettings.Controls.Remove(check_Limits);

            //____________________________Load Profile_________________________

            if (Rights.LoadProfileParams.Find(x => x.QuantityName == LoadProfileParams.PQLoadProfile.ToString()
            && x.Read == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(chk_PQ_LoadProfileInterval);
            }
            if (Rights.LoadProfileParams.Find(x => x.QuantityName == LoadProfileParams.LoadProfileChannels.ToString()
           && x.Read == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(chk_LoadProfile);
            }
            if (Rights.LoadProfileParams.Find(x => x.QuantityName == LoadProfileParams.LoadProfileChannels2.ToString()
               && x.Read == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(chk_LoadProfile_2);
            }
            if (Rights.LoadProfileParams.Find(x => x.QuantityName == LoadProfileParams.LoadProfileInterval.ToString()
               && x.Read == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(chk_LoadProfile_Interval);
            }
            if (Rights.LoadProfileParams.Find(x => x.QuantityName == LoadProfileParams.LoadProfileInterval2.ToString()
               && x.Read == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(chk_LoadProfile_2_Interval);
            }

            //_________________________________MDI_________________________________
            if (Rights.MeterRights.Find(x => x.QuantityName == MeterRights.MDI.ToString()
                && x.Read == true) == null)
                fLP_MeterSettings.Controls.Remove(check_MDI_params);

            //_________________________________Monitoring Time_________________________________
            if (Rights.MeterRights.Find(x => x.QuantityName == MeterRights.MonitoringTime.ToString()
                && x.Read == true) == null)
                fLP_MeterSettings.Controls.Remove(check_MonitoringTime);

            if (Rights.StatusWordParamRights.Find(x => x.QuantityType == typeof(StatusWordParam) && x.QuantityName == StatusWordParam.StausWord1.ToString() && x.Read) == null)
                fLP_MeterSettings.Controls.Remove(chbStatusWordMap1);

            if (Rights.StatusWordParamRights.Find(x => x.QuantityType == typeof(StatusWordParam) && x.QuantityName == StatusWordParam.StatusWord2.ToString() && x.Read) == null)
                fLP_MeterSettings.Controls.Remove(chbStatusWordMap2);

            // TODO: Fahad 05   ** Access Writes ** Remove control (READ)
            if (Rights.MeterRights.Find(x => x.QuantityType == typeof(MeterRights) && x.QuantityName == MeterRights.Schedule_Entry.ToString() && x.Read) == null)
                fLP_MeterSettings.Controls.Remove(check_loadShedding);

            if (Rights.MeterRights.Find(x => x.QuantityType == typeof(MeterRights) && x.QuantityName == MeterRights.GeneratorStart.ToString() && x.Read) == null)
                fLP_MeterSettings.Controls.Remove(check_GeneratorStart);

            //Security Data
            bool Is_Hidden =
                        //(Rights.MiscRights.Find(x => x.QuantityType == typeof(Misc) && x.QuantityName == Misc.SecData_AuthenticationKey.ToString() && x.Read) == null) &&
                        //(Rights.MiscRights.Find(x => x.QuantityType == typeof(Misc) && x.QuantityName == Misc.SecData_EncrptiontionKey.ToString() && x.Read) == null) &&
                        (Rights.MiscRights.Find(x => x.QuantityType == typeof(Misc) && x.QuantityName == Misc.SecData_Control_NoSecurity.ToString() && x.Read) == null) &&
                        (Rights.MiscRights.Find(x => x.QuantityType == typeof(Misc) && x.QuantityName == Misc.SecData_Control_Authentication.ToString() && x.Read) == null) &&
                        (Rights.MiscRights.Find(x => x.QuantityType == typeof(Misc) && x.QuantityName == Misc.SecData_Control_Encrptiontion.ToString() && x.Read) == null) &&
                        (Rights.MiscRights.Find(x => x.QuantityType == typeof(Misc) && x.QuantityName == Misc.SecData_Control_Auth_Encrp.ToString() && x.Read) == null);

            if (Is_Hidden)
            {
                flpSecurityKeys.Visible = false;
                this.Controls.Remove(gbSecurityKeys);
            }
            else
            {
                check_WriteAuthenticationKey.Visible = false; // (Rights.MiscRights.Find(x => x.QuantityName == Misc.SecData_AuthenticationKey.ToString()).Read);
                check_EncryptionKey.Visible = false; // (Rights.MiscRights.Find(x => x.QuantityName == Misc.SecData_EncrptiontionKey.ToString()).Read);
                check_SecurityPolicy.Visible = (
                    (Rights.MiscRights.Find(x => x.QuantityName == Misc.SecData_Control_Auth_Encrp.ToString()).Read) ||
                    (Rights.MiscRights.Find(x => x.QuantityName == Misc.SecData_Control_Authentication.ToString()).Read) ||
                    (Rights.MiscRights.Find(x => x.QuantityName == Misc.SecData_Control_Encrptiontion.ToString()).Read) ||
                    (Rights.MiscRights.Find(x => x.QuantityName == Misc.SecData_Control_NoSecurity.ToString()).Read)
                    );
            }
            //____________________________________________________________________
            //Modem
            Is_Hidden =
                        (Rights.ModemRights.Find(x => x.QuantityType == typeof(Modem) && x.QuantityName == Modem.CommunicationProfile.ToString() && x.Read) == null) &&
                        (Rights.ModemRights.Find(x => x.QuantityType == typeof(Modem) && x.QuantityName == Modem.IPProfile.ToString() && x.Read) == null) &&
                        (Rights.ModemRights.Find(x => x.QuantityType == typeof(Modem) && x.QuantityName == Modem.KeepAlive.ToString() && x.Read) == null) &&
                        (Rights.ModemRights.Find(x => x.QuantityType == typeof(Modem) && x.QuantityName == Modem.ModemInitiallization.ToString() && x.Read) == null) &&
                        (Rights.ModemRights.Find(x => x.QuantityType == typeof(Modem) && x.QuantityName == Modem.ModemLimitsAndTime.ToString() && x.Read) == null) &&
                        (Rights.ModemRights.Find(x => x.QuantityType == typeof(Modem) && x.QuantityName == Modem.NumberProfile.ToString() && x.Read) == null) &&
                        (Rights.ModemRights.Find(x => x.QuantityType == typeof(Modem) && x.QuantityName == Modem.WakeUpProfile.ToString() && x.Read) == null);

            if (Is_Hidden)
            {
                flp_ModemSettings.Visible = false;
                this.Controls.Remove(gbModemSetting);
            }
            else
            {
                if (Rights.ModemRights.Find(x => x.QuantityName == Modem.IPProfile.ToString()).Read == false)
                    flp_ModemSettings.Controls.Remove(check_IP_Profile);
                if (Rights.ModemRights.Find(x => x.QuantityName == Modem.CommunicationProfile.ToString()).Read == false)
                    flp_ModemSettings.Controls.Remove(chbCommunicationProfile);
                if (Rights.ModemRights.Find(x => x.QuantityName == Modem.WakeUpProfile.ToString()).Read == false)
                    flp_ModemSettings.Controls.Remove(chbWakeupProfile);
                if (Rights.ModemRights.Find(x => x.QuantityName == Modem.KeepAlive.ToString()).Read == false)
                    flp_ModemSettings.Controls.Remove(chbKeepAlive);
                if (Rights.ModemRights.Find(x => x.QuantityName == Modem.NumberProfile.ToString()).Read == false)
                    flp_ModemSettings.Controls.Remove(chbNumberProfile);
                if (Rights.ModemRights.Find(x => x.QuantityName == Modem.ModemLimitsAndTime.ToString()).Read == false)
                    flp_ModemSettings.Controls.Remove(chbModemLimitsAndTime);
                if (Rights.ModemRights.Find(x => x.QuantityName == Modem.ModemInitiallization.ToString()).Read == false)
                    flp_ModemSettings.Controls.Remove(chbModemInitialize);
            }



            //____________________________________________________________________
            if (Rights.ClockRights.Find(x => x.Read) == null)
                fLP_MeterSettings.Controls.Remove(check_Time);

            //____________________________________________________________________
            //Standard Modem
            Is_Hidden =
                        (Rights.StandardModemRights.Find(x => x.QuantityType == typeof(StandardModem) && x.QuantityName == StandardModem.StandardIPProfile.ToString() && x.Read) == null) &&
                        (Rights.StandardModemRights.Find(x => x.QuantityType == typeof(StandardModem) && x.QuantityName == StandardModem.StandardKeepAlive.ToString() && x.Read) == null) &&
                        (Rights.StandardModemRights.Find(x => x.QuantityType == typeof(StandardModem) && x.QuantityName == StandardModem.StandardNumberProfile.ToString() && x.Read) == null);

            if (Is_Hidden)
            {
                flpStandardModem.Visible = false;
                this.Controls.Remove(gbStandardModem);
            }
            else
            {
                if (Rights.StandardModemRights.Find(x => x.QuantityName == StandardModem.StandardIPProfile.ToString()).Read == false)
                    flpStandardModem.Controls.Remove(check_StandardModem_IP_Profile);
                if (Rights.StandardModemRights.Find(x => x.QuantityName == StandardModem.StandardKeepAlive.ToString()).Read == false)
                    flpStandardModem.Controls.Remove(check_StandardModem_KeepAlive);
                if (Rights.StandardModemRights.Find(x => x.QuantityName == StandardModem.StandardNumberProfile.ToString()).Read == false)
                    flpStandardModem.Controls.Remove(check_StandardModem_Number_Profile);
            }

            //Time base events
            Is_Hidden = (Rights.MeterSchedulingRights.Find(x => x.QuantityType == typeof(MeterScheduling) && x.QuantityName == MeterScheduling.TimeBaseEvent1.ToString() && x.Read) == null) &&
                        (Rights.MeterSchedulingRights.Find(x => x.QuantityType == typeof(MeterScheduling) && x.QuantityName == MeterScheduling.TimeBaseEvent2.ToString() && x.Read) == null);
            if (Is_Hidden)
            {
                flpTimeEvents.Visible = false;
                this.Controls.Remove(gbTBESetting);
            }
            else
            {
                if (Rights.MeterSchedulingRights.Find(x => x.QuantityName == MeterScheduling.TimeBaseEvent1.ToString()).Read == false
                    && Rights.MeterSchedulingRights.Find(x => x.QuantityName == MeterScheduling.TimeBaseEvent2.ToString()).Read == false)
                    flpTimeEvents.Controls.Remove(check_TBEs);
            }
            if (IsReadMode == 0)
            {
                this.btn_CustomSelectAll_Click(this.btn_CustomSelectAll, new EventArgs());
                this.DialogResult = DialogResult.OK;
            }
        }

        public void SetAccessRightsForWrite(ApplicationRight Rights)
        {
            //_________________________________Activity Calender_________________________________
            if (Rights.MeterRights.Find(x => x.QuantityName == MeterRights.ActivityCalender.ToString()
                && x.Write == true) == null)
                fLP_MeterSettings.Controls.Remove(check_ActivityCalender);
            ////____________________________MISC Parameters_________________________
            //if (Rights.MeterRights.Find(x => x.QuantityName == MeterRights.Misc.ToString()
            //    && x.Write == true) == null)
            //{
            //    fLP_MeterSettings.Controls.Remove(check_CTPT);
            //    fLP_MeterSettings.Controls.Remove(check_Clock);
            //    fLP_MeterSettings.Controls.Remove(check_customerReference);
            //    fLP_MeterSettings.Controls.Remove(check_DecimalPoint);
            //    fLP_MeterSettings.Controls.Remove(check_Password_Elec);
            //    fLP_MeterSettings.Controls.Remove(check_EnergyParams);
            //    fLP_MeterSettings.Controls.Remove(chk_GPP);
            //}
            //____________________________MISC Parameters_________________________
            if ((Rights.MiscRights.Find(x => x.QuantityName == Misc.CT_Ratio.ToString() && x.Write) == null) &&
                    (Rights.MeterRights.Find(x => x.QuantityName == Misc.PT_Ratio.ToString() && x.Write) == null))
            {
                fLP_MeterSettings.Controls.Remove(check_CTPT);
            }
            if (Rights.MiscRights.Find(x => x.QuantityName == Misc.Clock.ToString()
                && x.Write == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(check_Clock);
            }
            if (Rights.MiscRights.Find(x => x.QuantityName == Misc.CustomerReference.ToString()
                && x.Write == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(check_customerReference);
            }

            if (Rights.MiscRights.Find(x => x.QuantityName == Misc.AssociationPassword.ToString()
                && x.Write == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(check_Password_Elec);
            }
            if (Rights.MiscRights.Find(x => x.QuantityName == Misc.EnergyParam.ToString()
                && x.Write == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(check_EnergyParams);
            }
            if (Rights.MiscRights.Find(x => x.QuantityName == Misc.GernalProcessParameter.ToString()
                && x.Write == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(chk_GPP);
            }

            //_________________________________Decimal Point_____________________________
            if (
                (Rights.MiscRights.Find(x => x.QuantityName == Misc.DecimalPoint_BillEnergy.ToString() && x.Write == true) == null) &&
                (Rights.MiscRights.Find(x => x.QuantityName == Misc.DecimalPoint_BillMdi.ToString() && x.Write == true) == null) &&
                (Rights.MiscRights.Find(x => x.QuantityName == Misc.DecimalPoint_InstCurrent.ToString() && x.Write == true) == null) &&
                (Rights.MiscRights.Find(x => x.QuantityName == Misc.DecimalPoint_InstMdi.ToString() && x.Write == true) == null) &&
                (Rights.MiscRights.Find(x => x.QuantityName == Misc.DecimalPoint_InstPower.ToString() && x.Write == true) == null) &&
                (Rights.MiscRights.Find(x => x.QuantityName == Misc.DecimalPoint_InstVolt.ToString() && x.Write == true) == null)
            )
            {
                fLP_MeterSettings.Controls.Remove(check_DecimalPoint);
            }
            //_________________________________Contactor_________________________________
            if (Rights.MeterRights.Find(x => x.QuantityName == MeterRights.Contactor.ToString()
                && x.Write == true) == null)
                fLP_MeterSettings.Controls.Remove(check_Contactor);

            //____________________________Display Windows_________________________
            //if (Rights.MeterRights.Find(x => x.QuantityName == MeterRights.DisplayWindows.ToString()
            //    && x.Write == true) == null)
            //{
            //    fLP_MeterSettings.Controls.Remove(check_DisplayWindows_Alt);
            //    fLP_MeterSettings.Controls.Remove(check_DisplayWindows_Nor);
            //    fLP_MeterSettings.Controls.Remove(check_DisplayWindows_test);
            //}
            //v5.3.12
            if (Rights.DisplayWindowsRights.Find(x => x.QuantityName == DisplayWindowsParams.DisplayWindowsNormal.ToString()
                && x.Write == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(check_DisplayWindows_Nor);
            }
            if (Rights.DisplayWindowsRights.Find(x => x.QuantityName == DisplayWindowsParams.DisplayWindowsAlternate.ToString()
                && x.Write == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(check_DisplayWindows_Alt);
            }
            if (Rights.DisplayWindowsRights.Find(x => x.QuantityName == DisplayWindowsParams.DisplayWindowsTest.ToString()
                && x.Write == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(check_DisplayWindows_test);
            }

            //_________________________________Display Power Down_________________________________
            if (Rights.MeterRights.Find(x => x.QuantityName == MeterRights.DisplayPowerDown.ToString()
                && x.Write == true) == null)
                fLP_MeterSettings.Controls.Remove(check_DisplayPowerDown);
            //_________________________________Limits_________________________________
            if (Rights.MeterRights.Find(x => x.QuantityName == MeterRights.Limits.ToString()
                && x.Write == true) == null)
                fLP_MeterSettings.Controls.Remove(check_Limits);

            //____________________________Load Profile_________________________
            //____________________________Load Profile_________________________

            if (Rights.LoadProfileParams.Find(x => x.QuantityName == LoadProfileParams.PQLoadProfile.ToString()
            && x.Write == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(chk_PQ_LoadProfileInterval);
            }
            if (Rights.LoadProfileParams.Find(x => x.QuantityName == LoadProfileParams.LoadProfileChannels.ToString()
           && x.Write == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(chk_LoadProfile);
            }
            if (Rights.LoadProfileParams.Find(x => x.QuantityName == LoadProfileParams.LoadProfileChannels2.ToString()
               && x.Write == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(chk_LoadProfile_2);
            }
            if (Rights.LoadProfileParams.Find(x => x.QuantityName == LoadProfileParams.LoadProfileInterval.ToString()
               && x.Write == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(chk_LoadProfile_Interval);
            }
            if (Rights.LoadProfileParams.Find(x => x.QuantityName == LoadProfileParams.LoadProfileInterval2.ToString()
               && x.Write == true) == null)
            {
                fLP_MeterSettings.Controls.Remove(chk_LoadProfile_2_Interval);
            }
            //_________________________________MDI_________________________________
            if (Rights.MeterRights.Find(x => x.QuantityName == MeterRights.MDI.ToString()
                && x.Write == true) == null)
                fLP_MeterSettings.Controls.Remove(check_MDI_params);

            //_________________________________Monitoring Time_________________________________
            if (Rights.MeterRights.Find(x => x.QuantityName == MeterRights.MonitoringTime.ToString()
                && x.Write == true) == null)
                fLP_MeterSettings.Controls.Remove(check_MonitoringTime);

            if (Rights.StatusWordParamRights.Find(x => x.QuantityType == typeof(StatusWordParam) && x.QuantityName == StatusWordParam.StausWord1.ToString() && x.Write) == null)
                fLP_MeterSettings.Controls.Remove(chbStatusWordMap1);

            if (Rights.StatusWordParamRights.Find(x => x.QuantityType == typeof(StatusWordParam) && x.QuantityName == StatusWordParam.StatusWord2.ToString() && x.Write) == null)
                fLP_MeterSettings.Controls.Remove(chbStatusWordMap2);

            // TODO: Fahad 04   ** Access Writes ** Remove Control (WRITE)
            if (Rights.MeterRights.Find(x => x.QuantityType == typeof(MeterRights) && x.QuantityName == MeterRights.Schedule_Entry.ToString() && x.Write) == null)
                fLP_MeterSettings.Controls.Remove(check_loadShedding);

            if (Rights.MeterRights.Find(x => x.QuantityType == typeof(MeterRights) && x.QuantityName == MeterRights.GeneratorStart.ToString() && x.Write) == null)
                fLP_MeterSettings.Controls.Remove(check_GeneratorStart);

            //Security Data
            bool Is_Hidden =
                        //(Rights.MiscRights.Find(x => x.QuantityType == typeof(Misc) && x.QuantityName == Misc.SecData_AuthenticationKey.ToString() && x.Read) == null) &&
                        //(Rights.MiscRights.Find(x => x.QuantityType == typeof(Misc) && x.QuantityName == Misc.SecData_EncrptiontionKey.ToString() && x.Read) == null) &&
                        (Rights.MiscRights.Find(x => x.QuantityType == typeof(Misc) && x.QuantityName == Misc.SecData_Control_NoSecurity.ToString() && x.Write) == null) &&
                        (Rights.MiscRights.Find(x => x.QuantityType == typeof(Misc) && x.QuantityName == Misc.SecData_Control_Authentication.ToString() && x.Write) == null) &&
                        (Rights.MiscRights.Find(x => x.QuantityType == typeof(Misc) && x.QuantityName == Misc.SecData_Control_Encrptiontion.ToString() && x.Write) == null) &&
                        (Rights.MiscRights.Find(x => x.QuantityType == typeof(Misc) && x.QuantityName == Misc.SecData_Control_Auth_Encrp.ToString() && x.Write) == null);

            if (Is_Hidden)
                this.Controls.Remove(gbSecurityKeys);
            else
            {
                check_WriteAuthenticationKey.Visible = false; // (Rights.MiscRights.Find(x => x.QuantityName == Misc.SecData_AuthenticationKey.ToString()).Read);
                check_EncryptionKey.Visible = false; // (Rights.MiscRights.Find(x => x.QuantityName == Misc.SecData_EncrptiontionKey.ToString()).Read);
                check_SecurityPolicy.Visible = (
                    (Rights.MiscRights.Find(x => x.QuantityName == Misc.SecData_Control_Auth_Encrp.ToString()).Write) ||
                    (Rights.MiscRights.Find(x => x.QuantityName == Misc.SecData_Control_Authentication.ToString()).Write) ||
                    (Rights.MiscRights.Find(x => x.QuantityName == Misc.SecData_Control_Encrptiontion.ToString()).Write) ||
                    (Rights.MiscRights.Find(x => x.QuantityName == Misc.SecData_Control_NoSecurity.ToString()).Write)
                    );
            }
            //____________________________________________________________________
            //Modem
            Is_Hidden =
                        (Rights.ModemRights.Find(x => x.QuantityType == typeof(Modem) && x.QuantityName == Modem.CommunicationProfile.ToString() && x.Write) == null) &&
                        (Rights.ModemRights.Find(x => x.QuantityType == typeof(Modem) && x.QuantityName == Modem.IPProfile.ToString() && x.Write) == null) &&
                        (Rights.ModemRights.Find(x => x.QuantityType == typeof(Modem) && x.QuantityName == Modem.KeepAlive.ToString() && x.Write) == null) &&
                        (Rights.ModemRights.Find(x => x.QuantityType == typeof(Modem) && x.QuantityName == Modem.ModemInitiallization.ToString() && x.Write) == null) &&
                        (Rights.ModemRights.Find(x => x.QuantityType == typeof(Modem) && x.QuantityName == Modem.ModemLimitsAndTime.ToString() && x.Write) == null) &&
                        (Rights.ModemRights.Find(x => x.QuantityType == typeof(Modem) && x.QuantityName == Modem.NumberProfile.ToString() && x.Write) == null) &&
                        (Rights.ModemRights.Find(x => x.QuantityType == typeof(Modem) && x.QuantityName == Modem.WakeUpProfile.ToString() && x.Write) == null);

            if (Is_Hidden)
                this.Controls.Remove(gbModemSetting);
            else
            {
                if (Rights.ModemRights.Find(x => x.QuantityName == Modem.IPProfile.ToString()).Write == false)
                    flp_ModemSettings.Controls.Remove(check_IP_Profile);
                if (Rights.ModemRights.Find(x => x.QuantityName == Modem.CommunicationProfile.ToString()).Write == false)
                    flp_ModemSettings.Controls.Remove(chbCommunicationProfile);
                if (Rights.ModemRights.Find(x => x.QuantityName == Modem.WakeUpProfile.ToString()).Write == false)
                    flp_ModemSettings.Controls.Remove(chbWakeupProfile);
                if (Rights.ModemRights.Find(x => x.QuantityName == Modem.KeepAlive.ToString()).Write == false)
                    flp_ModemSettings.Controls.Remove(chbKeepAlive);
                if (Rights.ModemRights.Find(x => x.QuantityName == Modem.NumberProfile.ToString()).Write == false)
                    flp_ModemSettings.Controls.Remove(chbNumberProfile);
                if (Rights.ModemRights.Find(x => x.QuantityName == Modem.ModemLimitsAndTime.ToString()).Write == false)
                    flp_ModemSettings.Controls.Remove(chbModemLimitsAndTime);
                if (Rights.ModemRights.Find(x => x.QuantityName == Modem.ModemInitiallization.ToString()).Write == false)
                    flp_ModemSettings.Controls.Remove(chbModemInitialize);
            }

            //____________________________________________________________________
            if (Rights.ClockRights.Find(x => x.Write) == null)
                fLP_MeterSettings.Controls.Remove(check_Time);

            //____________________________________________________________________
            //Standard Modem
            Is_Hidden =
                        (Rights.StandardModemRights.Find(x => x.QuantityType == typeof(StandardModem) && x.QuantityName == StandardModem.StandardIPProfile.ToString() && x.Write) == null) &&
                        (Rights.StandardModemRights.Find(x => x.QuantityType == typeof(StandardModem) && x.QuantityName == StandardModem.StandardKeepAlive.ToString() && x.Write) == null) &&
                        (Rights.StandardModemRights.Find(x => x.QuantityType == typeof(StandardModem) && x.QuantityName == StandardModem.StandardNumberProfile.ToString() && x.Write) == null);

            if (Is_Hidden)
                this.Controls.Remove(gbStandardModem);
            else
            {
                if (Rights.StandardModemRights.Find(x => x.QuantityName == StandardModem.StandardIPProfile.ToString()).Write == false)
                    flpStandardModem.Controls.Remove(check_StandardModem_IP_Profile);
                if (Rights.StandardModemRights.Find(x => x.QuantityName == StandardModem.StandardKeepAlive.ToString()).Write == false)
                    flpStandardModem.Controls.Remove(check_StandardModem_KeepAlive);
                if (Rights.StandardModemRights.Find(x => x.QuantityName == StandardModem.StandardNumberProfile.ToString()).Write == false)
                    flpStandardModem.Controls.Remove(check_StandardModem_Number_Profile);
            }

            //Time base events
            Is_Hidden = (Rights.MeterSchedulingRights.Find(x => x.QuantityType == typeof(MeterScheduling) && x.QuantityName == MeterScheduling.TimeBaseEvent1.ToString() && x.Write) == null) &&
                        (Rights.MeterSchedulingRights.Find(x => x.QuantityType == typeof(MeterScheduling) && x.QuantityName == MeterScheduling.TimeBaseEvent2.ToString() && x.Write) == null);
            if (Is_Hidden)
                this.Controls.Remove(gbTBESetting);
            else
            {
                if (Rights.MeterSchedulingRights.Find(x => x.QuantityName == MeterScheduling.TimeBaseEvent1.ToString()).Write == false
                    && Rights.MeterSchedulingRights.Find(x => x.QuantityName == MeterScheduling.TimeBaseEvent2.ToString()).Write == false)
                    flpTimeEvents.Controls.Remove(check_TBEs);
            }
        }
    }

}
