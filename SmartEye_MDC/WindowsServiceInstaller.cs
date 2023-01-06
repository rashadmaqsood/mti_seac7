using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ServiceProcess;
using System.Configuration.Install;
using Communicator.MTI_MDC;
using System.IO;

namespace Communicator
{
    [RunInstaller(true)]
    public class WindowsServiceInstaller : Installer
    {
        /// <summary>
        /// Public Constructor for WindowsServiceInstaller.
        /// - Put all of your Initialization code here.
        /// </summary>
        public WindowsServiceInstaller()
        {
            ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();
            ServiceInstaller serviceInstaller = new ServiceInstaller();

            //# Service Account Information
            serviceProcessInstaller.Account = ServiceAccount.NetworkService;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;


            string Server_Instance = "Default";
            try
            {
                // Set Current Directory Path
                string Exec_current_Working_Directory = typeof(MDC).Assembly.Location;
                FileInfo Exec_URL = new FileInfo(Exec_current_Working_Directory);
                var current_Working_Directory = Exec_URL.Directory;

                if (!string.IsNullOrEmpty(Exec_current_Working_Directory) &&
                    current_Working_Directory.Exists)
                    Directory.SetCurrentDirectory(current_Working_Directory.FullName);

                MDC.Init_Application_Config();

                if (!string.IsNullOrEmpty(Communicator.Properties.Settings.Default.Instance))
                    Server_Instance = Communicator.Properties.Settings.Default.Instance;
            }
            catch
            {
            }

            //# Service Information
            serviceInstaller.DisplayName = "[MDC]Smart Meter Data Collector";
            serviceInstaller.StartType = ServiceStartMode.Automatic;

            //# This must be identical to the WindowsService.ServiceBase name
            //# Set in the constructor of WindowsService.cs
            serviceInstaller.ServiceName = string.Format("MDC_{0}", Server_Instance);

            this.Installers.Add(serviceProcessInstaller);
            this.Installers.Add(serviceInstaller);
        }
    }

}
