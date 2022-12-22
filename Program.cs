using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GUI;

namespace SmartEyeControl_7
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            FrmContainer mainAppForm = new FrmContainer();
            Application.ThreadException +=new System.Threading.ThreadExceptionEventHandler(mainAppForm.Appliction_ThreadException);
            AppDomain.CurrentDomain.UnhandledException +=new UnhandledExceptionEventHandler(mainAppForm.Application_UnhandledException);
            
            Application.Run(mainAppForm);
        }
    }
}
