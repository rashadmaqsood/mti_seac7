//using comm.Rights;
using DLMS.Comm;
using GUI;
using SharedCode.Comm.HelperClasses;
using SmartEyeControl_7.ApplicationGUI.GUI;
//using SmartEyeControl_7.comm;
using System;
using ucCustomControl;

namespace AccurateOptocomSoftware.comm
{
    public class ApplicationRight_Helper : IDisposable
    {
        /*
                public void updateGUI(FrmContainer objApplicationCS, User CurrentUser)
        {
            int currentLocation = 59;
            const int offset = 89;

            #region Application.cs

               //Commented in v4.8.15 by Azeem
            try
            {
                
                objApplicationCS.cmbIOConnections.SuspendLayout();

                objApplicationCS.cmbIOConnections.Items.Clear();

                if (CurrentUser.rights.hdlc_access)
                {
                    objApplicationCS.cmbIOConnections.Items.Add(IOConnectionType.HDLC_MODE_E);
                }
                if (CurrentUser.rights.tcp_access)
                {
                    objApplicationCS.cmbIOConnections.Items.Add(IOConnectionType.IP_Link);
                }
                // objApplicationCS.cmbIOConnections.Items.Add(IOConnectionType.Direct_HDLC);
                // objApplicationCS.cmbIOConnections.Items.Add(IOConnectionType.IP_Ov_Serial_Link);

                if (CurrentUser.rights.hdlc_access && CurrentUser.rights.tcp_access)
                {
                    objApplicationCS.cmbIOConnections.Enabled = true;
                }
                else
                {
                    objApplicationCS.cmbIOConnections.Enabled = false;
                }

                if (objApplicationCS != null &&
                    objApplicationCS.cmbIOConnections.Items.Contains(IOConnectionType.HDLC_MODE_E))
                {
                    objApplicationCS.cmbIOConnections.SelectedItem = IOConnectionType.HDLC_MODE_E;
                }

                //if (CurrentUser.rights.hdlc_access && !CurrentUser.rights.tcp_access)
                //{
                //    objApplicationCS.cmbIOConnections.SelectedIndex = 0;
                //}
                //else if (CurrentUser.rights.tcp_access && !CurrentUser.rights.hdlc_access)
                //{
                //    objApplicationCS.cmbIOConnections.SelectedIndex = 1;
                //}

            }
            catch
            {
                throw;
            }
            finally
            {
                objApplicationCS.cmbIOConnections.ResumeLayout();
            }
            
            #endregion

            objApplicationCS.lbl_loginName.Text = CurrentUser.userName;
            #region Parameters
                    //Commented in v4.8.15 by Azeem
            if (CurrentUser.rights.parameters)
            {
                objApplicationCS.pic_Parameterization.Visible = true;
                objApplicationCS.lbl_headparam.Visible = true;
                objApplicationCS.pic_Parameterization.Location = new System.Drawing.Point(currentLocation, 1);
                objApplicationCS.lbl_headparam.Location = new System.Drawing.Point(currentLocation, 58);
                currentLocation += offset;
                objApplicationCS.pic_Modem.Visible = true;
                objApplicationCS.pic_Modem.Location = new System.Drawing.Point(currentLocation, 1);
                objApplicationCS.lbl_headmodem.Visible = true;
                objApplicationCS.lbl_headmodem.Location = new System.Drawing.Point(currentLocation, 58);
                currentLocation += offset;
            }
            else
            {
                objApplicationCS.pic_Parameterization.Visible = false;
                objApplicationCS.pic_Modem.Visible = false;
                objApplicationCS.lbl_headparam.Visible = false;
                objApplicationCS.lbl_headmodem.Visible = false;
            }

            #endregion
            #region BIlling

            if (CurrentUser.rights.billingData)
            {
                objApplicationCS.pic_Billing.Visible = true;
                objApplicationCS.lbl_headBill.Visible = true;
                objApplicationCS.pic_Billing.Location = new System.Drawing.Point(currentLocation, 1);
                objApplicationCS.lbl_headBill.Location = new System.Drawing.Point(currentLocation, 58);
                currentLocation += offset;
            }
            else
            {
                objApplicationCS.pic_Billing.Visible = false;
                objApplicationCS.lbl_headBill.Visible = false;
            }

            #endregion
            #region Instantaneous

            if (CurrentUser.rights.instantaneousData)
            {
                objApplicationCS.pic_Instantaneous.Visible = true;
                objApplicationCS.lbl_headIns.Visible = true;
                objApplicationCS.pic_Instantaneous.Location = new System.Drawing.Point(currentLocation, 1);
                objApplicationCS.lbl_headIns.Location = new System.Drawing.Point(currentLocation, 58);
                currentLocation += offset;
            }
            else
            {
                objApplicationCS.pic_Instantaneous.Visible = false;
                objApplicationCS.lbl_headIns.Visible = false;

            }

            #endregion
            #region LoadProfileData

            if (CurrentUser.rights.loadProfileData)
            {
                objApplicationCS.pic_LoadProfile.Visible = true;
                objApplicationCS.lbl_headLp.Visible = true;
                objApplicationCS.pic_LoadProfile.Location = new System.Drawing.Point(currentLocation, 1);
                objApplicationCS.lbl_headLp.Location = new System.Drawing.Point(currentLocation, 58);
                currentLocation += offset;
            }
            else
            {
                objApplicationCS.pic_LoadProfile.Visible = false;
                objApplicationCS.lbl_headLp.Visible = false;

            }

            #endregion
            #region EventsData

            if (CurrentUser.rights.eventsData)
            {
                objApplicationCS.pic_Events.Visible = true;
                objApplicationCS.lbl_headevents.Visible = true;
                objApplicationCS.pic_Events.Location = new System.Drawing.Point(currentLocation, 1);
                objApplicationCS.lbl_headevents.Location = new System.Drawing.Point(currentLocation, 58);
                currentLocation += offset;
            }
            else
            {
                objApplicationCS.pic_Events.Visible = false;
                objApplicationCS.lbl_headevents.Visible = false;

            }

            #endregion
            #region SuperAdmin_Reserved
            objApplicationCS.menuStrip1.Items.Clear();

            if (CurrentUser.userTypeID == UserTypeID.SuperAdmin)
            {
                //v4.8.15 //by Azeem
                objApplicationCS.flpSettingIcon.Visible =
                objApplicationCS.flpDebugIcon.Visible=
                objApplicationCS.flpAdminIcon.Visible = true;

                     //Commented in v4.8.15 by Azeem
                objApplicationCS.pic_settings.Visible = true;
                objApplicationCS.lbl_headSettings.Visible = true;
                objApplicationCS.pic_settings.Location = new System.Drawing.Point(currentLocation, 1);
                objApplicationCS.lbl_headSettings.Location = new System.Drawing.Point(currentLocation, 58);
                currentLocation += offset;

                objApplicationCS.pic_Debug.Visible = true;
                objApplicationCS.lbl_headdebug.Visible = true;
                objApplicationCS.pic_Debug.Location = new System.Drawing.Point(currentLocation, 1);
                objApplicationCS.lbl_headdebug.Location = new System.Drawing.Point(currentLocation, 58);
                currentLocation += offset;

                objApplicationCS.pic_AboutMe.Visible = true;
                objApplicationCS.lbl_headhelp.Visible = true;
                objApplicationCS.pic_AboutMe.Location = new System.Drawing.Point(currentLocation, 1);
                objApplicationCS.lbl_headhelp.Location = new System.Drawing.Point(currentLocation, 58);
                currentLocation += offset;

                objApplicationCS.pic_admin.Visible = true;
                objApplicationCS.lbl_headAdmin.Visible = true;
                objApplicationCS.pic_admin.Location = new System.Drawing.Point(currentLocation, 1);
                objApplicationCS.lbl_headAdmin.Location = new System.Drawing.Point(currentLocation, 58);
                currentLocation += offset;

                objApplicationCS.pic_logoff.Visible = true;
                objApplicationCS.lbl_headLogOff.Visible = true;
                objApplicationCS.pic_logoff.Location = new System.Drawing.Point(currentLocation, 1);
                objApplicationCS.lbl_headLogOff.Location = new System.Drawing.Point(currentLocation, 58);
                currentLocation += offset;
                

                //Add Settings To Context Menu Strip
                objApplicationCS.menuStrip1.Items.Add(objApplicationCS.menuToolStripMenuItem);
                objApplicationCS.menuStrip1.Items.Add(objApplicationCS.configurationsToolStripMenuItem);
                objApplicationCS.menuStrip1.Items.Add(objApplicationCS.helpToolStripMenuItem);
            }
            else if (CurrentUser.userTypeID == UserTypeID.Admin)
            {
                //v4.8.15 //by Azeem
                objApplicationCS.flpSettingIcon.Visible =
                objApplicationCS.flpDebugIcon.Visible =
                objApplicationCS.flpAdminIcon.Visible = true;

                     //Commented in v4.8.15 by Azeem
                objApplicationCS.pic_AboutMe.Visible = true;
                objApplicationCS.lbl_headhelp.Visible = true;
                objApplicationCS.pic_AboutMe.Location = new System.Drawing.Point(currentLocation, 1);
                objApplicationCS.lbl_headhelp.Location = new System.Drawing.Point(currentLocation, 58);
                currentLocation += offset;

                //Changed by Azeem
                //v10.0.21 Visible True
                objApplicationCS.pic_settings.Visible = true;
                objApplicationCS.lbl_headSettings.Visible = true;
                objApplicationCS.pic_settings.Location = new System.Drawing.Point(currentLocation, 1);
                objApplicationCS.lbl_headSettings.Location = new System.Drawing.Point(currentLocation, 58);
                currentLocation += offset;

                objApplicationCS.pic_Debug.Visible = true;
                objApplicationCS.lbl_headdebug.Visible = true;
                objApplicationCS.pic_Debug.Location = new System.Drawing.Point(currentLocation, 1);
                objApplicationCS.lbl_headdebug.Location = new System.Drawing.Point(currentLocation, 58);
                currentLocation += offset;

                //Add Settings To Context Menu Strip
                objApplicationCS.menuStrip1.Items.Add(objApplicationCS.menuToolStripMenuItem);
                objApplicationCS.menuStrip1.Items.Add(objApplicationCS.configurationsToolStripMenuItem);
                objApplicationCS.menuStrip1.Items.Add(objApplicationCS.helpToolStripMenuItem);
                //End Change

                objApplicationCS.pic_admin.Visible = true;
                objApplicationCS.lbl_headAdmin.Visible = true;
                objApplicationCS.pic_admin.Location = new System.Drawing.Point(currentLocation, 1);
                objApplicationCS.lbl_headAdmin.Location = new System.Drawing.Point(currentLocation, 58);
                currentLocation += offset;

                //objApplicationCS.pic_settings.Visible = false;
                //objApplicationCS.pic_Debug.Visible = false;
                //objApplicationCS.lbl_headSettings.Visible = false;
                //objApplicationCS.lbl_headdebug.Visible = false;

                objApplicationCS.pic_logoff.Visible = true;
                objApplicationCS.lbl_headLogOff.Visible = true;
                objApplicationCS.pic_logoff.Location = new System.Drawing.Point(currentLocation, 1);
                objApplicationCS.lbl_headLogOff.Location = new System.Drawing.Point(currentLocation, 58);
                currentLocation += offset;

                //Add Settings To Context Menu Strip
                objApplicationCS.menuStrip1.Items.Add(objApplicationCS.menuToolStripMenuItem);
                objApplicationCS.menuStrip1.Items.Add(objApplicationCS.configurationsToolStripMenuItem);
                objApplicationCS.menuStrip1.Items.Add(objApplicationCS.helpToolStripMenuItem);

            }
            else
            {
                //v4.8.15 //by Azeem
                objApplicationCS.flpSettingIcon.Visible =
                objApplicationCS.flpDebugIcon.Visible =
                objApplicationCS.flpAdminIcon.Visible = false; ;

                    //Commented in v4.8.15 by Azeem
                objApplicationCS.pic_AboutMe.Visible = true;
                objApplicationCS.lbl_headhelp.Visible = true;
                objApplicationCS.pic_AboutMe.Location = new System.Drawing.Point(currentLocation, 1);
                objApplicationCS.lbl_headhelp.Location = new System.Drawing.Point(currentLocation, 58);
                currentLocation += offset;

                objApplicationCS.pic_logoff.Visible = true;
                objApplicationCS.lbl_headLogOff.Visible = true;
                objApplicationCS.pic_logoff.Location = new System.Drawing.Point(currentLocation, 1);
                objApplicationCS.lbl_headLogOff.Location = new System.Drawing.Point(currentLocation, 58);
                currentLocation += offset;

                objApplicationCS.pic_settings.Visible = false;
                objApplicationCS.pic_Debug.Visible = false;
                //objApplicationCS.pic_AboutMe.Visible = false;

                objApplicationCS.lbl_headSettings.Visible = false;
                objApplicationCS.lbl_headdebug.Visible = false;
                //objApplicationCS.lbl_headhelp.Visible = false;

                objApplicationCS.pic_admin.Visible = false;
                objApplicationCS.lbl_headAdmin.Visible = false;
                
            }

            #endregion
        }
         */


        public void updateGUI(FrmContainer objApplicationCS, User CurrentUser)
        {

            try
            {
                objApplicationCS.lbl_loginName.Text = CurrentUser.userName;

                bool isSuccess = false;

                //this.SuspendLayout();
                objApplicationCS.pnlPicIcons.SuspendLayout();
                objApplicationCS.cmbIOConnections.SuspendLayout();

                objApplicationCS.cmbIOConnections.Items.Clear();


                try
                {
                    var AccessRight = CurrentUser.CurrentAccessRights.GeneralRights.Find((x) => x.QuantityType == typeof(GeneralRights) && (x.Read));
                    if (AccessRight != null && (AccessRight.Read == true))
                    {
                        objApplicationCS.cmbSecurity.Items.Clear();
                        foreach (var item in CurrentUser.CurrentAccessRights.GeneralRights)
                        {
                            _HelperAccessRights((GeneralRights)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write, ref objApplicationCS);
                        }
                        isSuccess = true;
                    }
                }
                catch (Exception)
                {
                    objApplicationCS.flpBillingIcon.Visible =
                        objApplicationCS.flpEventsIcon.Visible =
                        objApplicationCS.flpInstantaneousIcon.Visible =
                        objApplicationCS.flpLoadProfileIcon.Visible =
                        objApplicationCS.flpParameterIcon.Visible =
                        false;
                }


                #region SuperAdmin_Reserved
                objApplicationCS.menuStrip1.Items.Clear();

                if (CurrentUser.userTypeID == UserTypeID.SuperAdmin)
                {
                    //v4.8.15 //by Azeem
                    objApplicationCS.flpSettingIcon.Visible =
                    objApplicationCS.flpDebugIcon.Visible =
                    objApplicationCS.flpAdminIcon.Visible = true;

                    //Add Settings To Context Menu Strip
                    objApplicationCS.menuStrip1.Items.Add(objApplicationCS.menuToolStripMenuItem);
                    objApplicationCS.menuStrip1.Items.Add(objApplicationCS.configurationsToolStripMenuItem);
                    objApplicationCS.menuStrip1.Items.Add(objApplicationCS.helpToolStripMenuItem);
                }
                else if (CurrentUser.userTypeID == UserTypeID.Admin)
                {
                    //v4.8.15 //by Azeem
                    objApplicationCS.flpSettingIcon.Visible =
                    objApplicationCS.flpDebugIcon.Visible =
                    objApplicationCS.flpAdminIcon.Visible = false;

                    //Add Settings To Context Menu Strip
                    objApplicationCS.menuStrip1.Items.Add(objApplicationCS.menuToolStripMenuItem);
                    objApplicationCS.menuStrip1.Items.Add(objApplicationCS.configurationsToolStripMenuItem);
                    objApplicationCS.menuStrip1.Items.Add(objApplicationCS.helpToolStripMenuItem);

                }
                else
                {
                    //v4.8.15 //by Azeem
                    objApplicationCS.flpSettingIcon.Visible =
                    objApplicationCS.flpDebugIcon.Visible =
                    objApplicationCS.flpAdminIcon.Visible = false; ;
                }

                #endregion
            }
            finally
            {
                if (objApplicationCS.cmbIOConnections.Items.Count > 0)
                {
                    if (objApplicationCS.cmbIOConnections.Items.Contains(IOConnectionType.HDLC_MODE_E))
                    {
                        objApplicationCS.cmbIOConnections.SelectedItem = IOConnectionType.HDLC_MODE_E;
                    }
                    else
                    {
                        objApplicationCS.cmbIOConnections.SelectedIndex = 0;
                    }
                }

                objApplicationCS.cmbIOConnections.Enabled = (objApplicationCS.cmbIOConnections.Items.Count > 1) ? true : false;

                objApplicationCS.cmbIOConnections.ResumeLayout();
                objApplicationCS.pnlPicIcons.ResumeLayout();
            }
        }

        public void updateGUI(pnlParameterization objParameterization, User CurrentUser)
        {
            if (CurrentUser.userTypeID == UserTypeID.Admin ||
                objParameterization.Application_Controller.CurrentUser.userTypeID == UserTypeID.SuperAdmin)
            {
                //SET PARAMETERS option available
                objParameterization.btn_SET_paramameters.Visible = objParameterization.btn_SET_paramameters.Enabled = true;

                objParameterization.btn_caliberation_loadall.Visible = objParameterization.btn_caliberation_loadall.Enabled = true;
                objParameterization.btn_Caliberation_Save.Visible = objParameterization.btn_Caliberation_Save.Enabled = true;

                objParameterization.btn_GenerateReport.Location = new System.Drawing.Point(582, 62);
            }
            else
            {
                objParameterization.btn_SET_paramameters.Visible = objParameterization.btn_SET_paramameters.Enabled = true; //Set True by Azeem for Accurate

                objParameterization.btn_caliberation_loadall.Visible = objParameterization.btn_caliberation_loadall.Enabled = false;
                objParameterization.btn_Caliberation_Save.Visible = objParameterization.btn_Caliberation_Save.Enabled = false;

                objParameterization.btn_GenerateReport.Visible = objParameterization.btn_GenerateReport.Enabled = true; //Set True by Azeem for Accurate
                objParameterization.btn_GenerateReport.Location = new System.Drawing.Point(440, 62); //commented by Azeem
            }
        }

        public void updateGUI(pnlLoadProfile objLoadProfile, User CurrentUser)
        {

        }

        public void updateGUI(pnlEvents objEvents, User CurrentUser)
        {
            if (CurrentUser.userTypeID == UserTypeID.Admin ||
                CurrentUser.userTypeID == UserTypeID.SuperAdmin)
            {
                //SET PARAMETERS option available
                objEvents.btn_SET_EventsCautions.Visible = objEvents.btn_SET_EventsCautions.Enabled = true;
                objEvents.btn_SetMajorAlarm.Visible = objEvents.btn_SetMajorAlarm.Enabled = true;
                objEvents.btn_SetMajorStatus.Visible = objEvents.btn_SetMajorStatus.Enabled = true;

                objEvents.btn_SaveAlarm.Visible = objEvents.btn_SaveAlarm.Enabled = true;
                objEvents.btn_LoadAlarm.Visible = objEvents.btn_LoadAlarm.Enabled = true;

                objEvents.btn_events_report.Location = new System.Drawing.Point(722, 36);
                objEvents.btn_ReadSecurityData.Location = new System.Drawing.Point(859, 36);
            }
            else
            {
                objEvents.btn_SET_EventsCautions.Visible = objEvents.btn_SET_EventsCautions.Enabled = false;
                objEvents.btn_SetMajorAlarm.Visible = objEvents.btn_SetMajorAlarm.Enabled = false;
                objEvents.btn_SetMajorStatus.Visible = objEvents.btn_SetMajorStatus.Enabled = false;

                objEvents.btn_SaveAlarm.Visible = objEvents.btn_SaveAlarm.Enabled = false;
                objEvents.btn_LoadAlarm.Visible = objEvents.btn_LoadAlarm.Enabled = false;

                objEvents.btn_events_report.Location = new System.Drawing.Point(292, 36);
                objEvents.btn_ReadSecurityData.Location = new System.Drawing.Point(438, 36);
            }

            if (objEvents.tb_Events.TabPages.Count > 0)
                objEvents.tb_Events.SelectedIndex = 0;
            objEvents.combo_Events_SelectedITems.Items.Clear(); //v5.3.12
        }

        public void updateGUI(Instantaneous objInstantaneous, User CurrentUser)
        {
        }

        public void updateGUI(PnlBilling objBilling, User CurrentUser)
        {
            //objBilling.ApplyAccessRights(CurrentUser.CurrentAccessRights.BillingRights);
        }

        public void updateGUI_All(FrmContainer objApplicationCS, pnlParameterization objParameterization,
                                  pnlLoadProfile objLoadProfile, pnlEvents objEvents, Instantaneous objInstantaneous,
                                  PnlBilling objBilling, User CurrentUser)
        {
            updateGUI(objApplicationCS, CurrentUser);
            updateGUI(objParameterization, CurrentUser);
            updateGUI(objLoadProfile, CurrentUser);
            updateGUI(objEvents, CurrentUser);
            updateGUI(objInstantaneous, CurrentUser);
            updateGUI(objBilling, CurrentUser);
        }


        #region General Application Rights
        //private ApplicationRight RightsUser = null;
        //private List<AccessRights> generalRights = null;


        private void _HelperAccessRights(GeneralRights qty, bool read, bool write, ref FrmContainer mainForm)
        {
            switch (qty)
            {
                case GeneralRights.Billing:
                    mainForm.pnlBilling1.Visible = mainForm.flpBillingIcon.Visible = read;
                    break;
                case GeneralRights.Events:
                    mainForm.flpEventsIcon.Visible = mainForm.Panel_Events.Visible = read;
                    break;
                case GeneralRights.Instataneous:
                    mainForm.flpInstantaneousIcon.Visible = mainForm.Panel_Instantaneous.Visible = read;
                    break;
                case GeneralRights.LoadProfile:
                    mainForm.flpLoadProfileIcon.Visible =
                    mainForm.pnlLoad_Profile.Visible = read;
                    break;
                case GeneralRights.Parameter:
                    mainForm.flpParameterIcon.Visible =
                    mainForm.pnlParameterization1.Visible = read;
                    break;
                case GeneralRights.HDLC:
                    if (read)
                    {
                        if (!mainForm.cmbIOConnections.Items.Contains(IOConnectionType.HDLC_MODE_E))
                            mainForm.cmbIOConnections.Items.Add(IOConnectionType.HDLC_MODE_E);
                    }
                    else
                    {
                        if (mainForm.cmbIOConnections.Items.Contains(IOConnectionType.HDLC_MODE_E))
                            mainForm.cmbIOConnections.Items.Remove(IOConnectionType.HDLC_MODE_E);
                    }
                    break;
                case GeneralRights.TCP:
                    if (read)
                    {
                        if (!mainForm.cmbIOConnections.Items.Contains(IOConnectionType.IP_Link))
                            mainForm.cmbIOConnections.Items.Add(IOConnectionType.IP_Link);
                    }
                    else
                    {
                        if (mainForm.cmbIOConnections.Items.Contains(IOConnectionType.IP_Link))
                            mainForm.cmbIOConnections.Items.Remove(IOConnectionType.IP_Link);
                    }
                    break;
                case GeneralRights.Direct_HDLC:
                    if (read)
                    {
                        if (!mainForm.cmbIOConnections.Items.Contains(IOConnectionType.Direct_HDLC))
                            mainForm.cmbIOConnections.Items.Add(IOConnectionType.Direct_HDLC);
                    }
                    else
                    {
                        if (mainForm.cmbIOConnections.Items.Contains(IOConnectionType.Direct_HDLC))
                            mainForm.cmbIOConnections.Items.Remove(IOConnectionType.Direct_HDLC);
                    }
                    break;
                case GeneralRights.IP_Over_Serial:
                    if (read)
                    {
                        if (!mainForm.cmbIOConnections.Items.Contains(IOConnectionType.IP_Ov_Serial_Link))
                            mainForm.cmbIOConnections.Items.Add(IOConnectionType.IP_Ov_Serial_Link);
                    }
                    else
                    {
                        if (mainForm.cmbIOConnections.Items.Contains(IOConnectionType.IP_Ov_Serial_Link))
                            mainForm.cmbIOConnections.Items.Remove(IOConnectionType.IP_Ov_Serial_Link);
                    }
                    break;
                case GeneralRights.Menufacturer:
                    mainForm.lblManufacturer.Visible = mainForm.cmbManufacturers.Visible = read;
                    break;
                case GeneralRights.Device:
                    mainForm.lblDevices.Visible = mainForm.cmbDevices.Visible = read;
                    break;
                case GeneralRights.Authentication:
                    mainForm.lblAssociation.Visible = mainForm.cmbAssociations.Visible = read;
                    break;
                case GeneralRights.SecurityControl_AutherizeKey:
                    if (read)
                    {
                        mainForm.cmbSecurity.Items.Add(SecurityControl.AuthenticationOnly.ToString());
                        if (mainForm.cmbSecurity.Items.Count > 0)
                        {
                            mainForm.cmbSecurity.Visible = true;
                            //mainForm.cmbSecurity.SelectedIndex = 0;
                        }
                    }
                    break;
                case GeneralRights.SecurityControl_EncryptKey:
                    if (read)
                    {
                        mainForm.cmbSecurity.Items.Add(SecurityControl.EncryptionOnly.ToString());
                        if (mainForm.cmbSecurity.Items.Count > 0)
                        {
                            mainForm.cmbSecurity.Visible = true;
                            //mainForm.cmbSecurity.SelectedIndex = 0;
                        }
                    }
                    break;
                case GeneralRights.SecurityControl_AuthEncrpKey:
                    if (read)
                    {
                        mainForm.cmbSecurity.Items.Add(SecurityControl.AuthenticationAndEncryption.ToString());
                        if (mainForm.cmbSecurity.Items.Count > 0)
                        {
                            mainForm.cmbSecurity.Visible = true;
                            //mainForm.cmbSecurity.SelectedIndex = 0;
                        }
                    }
                    break;
                case GeneralRights.SecurityControl_NoSecurity:
                    if (read)
                    {
                        mainForm.cmbSecurity.Items.Add(SecurityControl.None.ToString());
                        if (mainForm.cmbSecurity.Items.Count > 0)
                        {
                            mainForm.cmbSecurity.Visible = true;
                            //mainForm.cmbSecurity.SelectedIndex = 0;
                        }
                    }
                    break;
                case GeneralRights.SecurityAuthKey:
                    mainForm.lblAuthKey.Visible = mainForm.tbAuthenticationKey.Visible = read;
                    break;
                case GeneralRights.SecurityEncrpKey:
                    mainForm.lblEncryptionKey.Visible = mainForm.tbEncryptionKey.Visible = read;
                    break;
                case GeneralRights.SecurityInvocationCounter:
                    mainForm.lblInvocationCounter.Visible = mainForm.tbInvocationCounter.Visible = read;
                    break;
            }

            //mainForm.cmbSecurity.SelectedIndex = 0;
        }
        #endregion

        public void Dispose()
        {
            try
            {
                //this.Dispose();
            }
            catch (Exception ex)
            { }
        }
    }
}
