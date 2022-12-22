//#define ENABLE_LIC

using System;
using System.Windows.Forms;
using AccurateOptocomSoftware.ApplicationGUI.ucCustomControl;
using GUI;
using AccurateOptocomSoftware.ApplicationGUI.GUI;

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
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

                #region License Verification

#if ENABLE_LIC

                try
                {
                    if (ProductValidationEngine.Current.Verify())
                    {
#endif
                        FrmContainer mainAppForm = new FrmContainer();
                        Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(mainAppForm.Appliction_ThreadException);
                        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(mainAppForm.Application_UnhandledException);
                        Application.Run(mainAppForm);
#if ENABLE_LIC
                    }
                    else
                    {
                        MessageBox.Show("Application startup sequence failed.\r\n" + ProductValidationEngine.Current.LastError,
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(1);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }
#endif
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "  \r\n  " + ex.StackTrace);
                Environment.Exit(11);
            }
        }


        [STAThread]
        static void Mainaaa()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

                #region License Verification

#if ENABLE_LIC

                try
                {
                    if (ProductValidationEngine.Current.Verify())
                    {
#endif
                frmDummy mainAppForm = new frmDummy();
                //Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(mainAppForm.Appliction_ThreadException);
                //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(mainAppForm.Application_UnhandledException);
                Application.Run(mainAppForm);
#if ENABLE_LIC
                    }
                    else
                    {
                        MessageBox.Show("Application startup sequence failed.\r\n" + ProductValidationEngine.Current.LastError,
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(1);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }
#endif
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "  \r\n  " + ex.StackTrace);
                Environment.Exit(11);
            }
        }


    }
}
