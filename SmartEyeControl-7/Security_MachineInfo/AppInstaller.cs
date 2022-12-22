// Copyright (C) 2014 MicroTech Industries
// All rights reserved.
// CODED by Muhammad Abubakar.
// Last Modified: 03/08/2015

using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using Microsoft.Win32;
using System.IO;
using System.Threading;
using System.Security.Principal;
using System.Security.AccessControl;

namespace System
{
    [RunInstaller(true)]
    public class AppInstaller : Installer
    {
        #region Constructor

        public AppInstaller()
            : base()
        {
            InitializeComponent();
            this.Committing += new InstallEventHandler(MyInstaller_Committing);
            this.Committed += new InstallEventHandler(MyInstaller_Committed);
            this.BeforeInstall += new InstallEventHandler(MyInstaller_BeforeInstall);
            this.AfterInstall += new InstallEventHandler(MyInstaller_AfterInstall);
            this.BeforeRollback += new InstallEventHandler(MyInstaller_BeforeRollback);
            this.AfterRollback += new InstallEventHandler(MyInstaller_AfterRollback);
            this.BeforeUninstall += new InstallEventHandler(MyInstaller_BeforeUninstall);
            this.AfterUninstall += new InstallEventHandler(MyInstaller_AfterUninstall);

            //_logFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Context.Parameters["ProductName"].Trim() + ".log");

        }

        #endregion

        #region Public Installer Methods

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Commit(IDictionary stateSaver)
        {
            if (stateSaver == null)
                return;
            base.Commit(stateSaver);
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Install(IDictionary stateSaver)
        {
            try
            {
                SecurityIdentifier sid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                NTAccount account = sid.Translate(typeof(NTAccount)) as NTAccount;

				using (RegistryKey key = Registry.LocalMachine.CreateSubKey(this.m_mainRegPath + this.m_regKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree))
                using (RegistryKey keySec = Registry.LocalMachine.CreateSubKey(this.m_mainRegPathSecLoc + this.m_regKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree))
                using (RegistryKey secKey = Registry.Users.CreateSubKey(this.m_secRegPath + this.m_regKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    if (key != null && keySec != null && secKey != null)
                    {
                        RegistrySecurity rsKey = key.GetAccessControl();
						RegistrySecurity rsKeySec = key.GetAccessControl();
                        RegistrySecurity rsSecKey = secKey.GetAccessControl();

                        if (account != null)
                        {
                            RegistryAccessRule rar = new RegistryAccessRule(
                                account.ToString(),
                                RegistryRights.FullControl,
                                InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                                PropagationFlags.None,
                                AccessControlType.Allow);

                            rsKey.AddAccessRule(rar);
                            rsSecKey.AddAccessRule(rar);

                        }

                        key.SetAccessControl(rsKey);
						key.SetAccessControl(rsKeySec);
                        secKey.SetAccessControl(rsSecKey);

                        if (secKey.GetValue(null) != null && secKey.GetValue(null).ToString() != String.Empty)
                        {
                            key.SetValue(this.m_regKeyName, secKey.GetValue(null).ToString());
							keySec.SetValue(this.m_regKeyName, secKey.GetValue(null).ToString());
                        }
                        else
                        {
                            key.SetValue(this.m_regKeyName, this.m_markerKey);
							keySec.SetValue(this.m_regKeyName, this.m_markerKey);
                            secKey.SetValue( null, this.m_markerKey );
                        }

                        secKey.Flush();
						secKey.Close();
                        key.Flush();
                        key.Close();
                        keySec.Flush();
                        keySec.Close();
                    }
                }

                base.Install(stateSaver);

            }
            catch (Exception) { throw new InstallException("Error 5000 occurred during configuration \r\n Please contact MicroTech industries."); }

        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Uninstall(IDictionary savedState)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(m_mainRegPath + m_regKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree);
			RegistryKey keySec = Registry.CurrentUser.OpenSubKey(m_mainRegPathSecLoc + m_regKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree);
            try
            {
                if (key != null)
                    key.DeleteSubKey(m_regKeyName);
				
				if (keySec != null)
                    keySec.DeleteSubKey(m_regKeyName);
            }
            catch { }

            base.Uninstall(savedState);
        }

        #endregion

        #region Protected Methods

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Private Methods

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        private void Log(string entry)
        {
            mutex.WaitOne();

            if (!String.IsNullOrEmpty(_logFileName))
            {
                using (StreamWriter output = new StreamWriter(_logFileName, true))
                {
                    string msg = string.Format("{0} |> {1}", DateTime.Now, entry);
                    output.WriteLine(msg);
                    output.Flush();
                    output.Close();
                }
            }

            mutex.ReleaseMutex();
        }

        #endregion

        #region MyInstaller EventHandlers

        private void MyInstaller_AfterUninstall(object sender, InstallEventArgs e)
        {
            Log("Application un-installed.");
        }

        private void MyInstaller_BeforeUninstall(object sender, InstallEventArgs e)
        {
            Log("un-installing started");
        }

        private void MyInstaller_AfterRollback(object sender, InstallEventArgs e)
        {
            Log("Rollbacked");
        }

        private void MyInstaller_BeforeRollback(object sender, InstallEventArgs e)
        {
            Log("Rollback started");
        }

        void MyInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            Log("Application installed.");
        }

        private void MyInstaller_Committing(object sender, InstallEventArgs e)
        {
            Log("Started saving installation settings.");
        }

        private void MyInstaller_Committed(object sender, InstallEventArgs e)
        {
            Log("Installation committed.");
        }

        private void MyInstaller_BeforeInstall(object sender, InstallEventArgs e)
        {
            Log("Started installing application.");
        }

        #endregion

        #region Fields

        private System.ComponentModel.IContainer components = null;

        private static Mutex mutex = new Mutex();
        private StreamWriter output;
        private string _logFileName;

        private string m_mainRegPath = @"Software\Microsoft\Windows\CurrentVersion\";
        private string m_secRegPath = @".DEFAULT\Control Panel\Accessibility\";
		private string m_mainRegPathSecLoc = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\";
        private string m_regKeyName = "{682c1542-2f2a-4d68-ae68-6df60bba6aee}";
        private string m_markerKey = "DU2MDNjM2MwMDAwY0MDAMDAw";

        #endregion

    }
}
