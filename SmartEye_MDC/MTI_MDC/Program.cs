//#define Enable_License
//#define Enable_Dongle
//#define Enable_Config

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Linq;
using Communicator.Properties;
using System.Xml;
using System.Collections;
using SharedCode.Common;

namespace Communicator.MTI_MDC
{
    public enum enmSecurityType
    {
        DateTime = 0,
        DateTimeWithMachineInfo
    };

    static class Program
    {
        #region Console Position
        const int SWP_NOSIZE = 0x0001;

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetConsoleWindow();
        #endregion
        #region Console Disable close control

        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        #endregion

        [STAThread]
        public static void Main()
        {
            try
            {
                try
                {
                    Console.Title = "Smart Eye MDC Server";
                    //Console.BackgroundColor = ConsoleColor.White;
                    //Console.ForegroundColor = ConsoleColor.Black;
                    //Console.Clear();

                }
                catch (Exception)
                { }
                Commons.HideConsoleWindow();

                #region Config File Verfication
#if Enable_Config
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(@"Application_Configs\Application_Common_Config.config");
                System.Xml.XmlNodeList aNodes = xmlDoc.SelectNodes("/configuration/secKey");

                if (aNodes.Count <= 0 || aNodes[0].InnerText != GetSecKey(xmlDoc.SelectNodes("/configuration/appSettings")[0].InnerXml))
                {
                    MessageBox.Show("Invalid Config file.");
                    Environment.Exit(1);
                }
#endif
                #endregion

                #region Dongle Verification

#if Enable_Dongle
                try
                {
                    ValidateDongle dongle = new ValidateDongle();
                    if (dongle.VerifyDongle())
                    {
                        dongle.DongleRemoved += OnDongleRemoved;
                    }
                    else
                    {
                        MessageBox.Show("Dongle Not Verified!");
                        Environment.Exit(1);
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in Dongle Verification: " + ex.Message );
                    Environment.Exit(1);
                }
#endif

                #endregion // End Dongle Verification

                #region License Verification

                try
                {

#if Enable_License
                    ProductValidationEngine.Current.LicenceExpired += OnLicenseExpired;
                    if (ProductValidationEngine.Current.Verify(enmSecurityType.DateTime))
                    {
#endif
                    DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_CLOSE, MF_BYCOMMAND);

#region Console Position and size

                        try
                        {
                            Commons.ProductName = Application.ProductName;
                            Commons.ProductVersion = Application.ProductVersion;
                        }
                        catch (Exception)
                        { }

#endregion

                        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                        Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new MDCForm());
#if Enable_License
                    }
                    else
                    {
                        MessageBox.Show("Application startup sequence failed.\r\n" + ProductValidationEngine.Current.LastError,
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(1);

                    }
#endif
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "MTI Smart Eye MDC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LocalCommon.SaveApplicationException(new Exception("Main MTI Smart Eye MDC"), 1);
                   // Environment.Exit(1);
                }

#endregion
                DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_CLOSE, MF_BYCOMMAND);
#region Console Position and size
                try
                {
                    Commons.ProductName = Application.ProductName;
                    Commons.ProductVersion = Application.ProductVersion;
                }
                catch (Exception)
                { }
#endregion
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MDCForm());
            }
            catch (Exception ex)
            {
                LocalCommon.SaveApplicationException(new Exception("Main MTI Smart Eye MDC catch"), 1);
                LocalCommon.SaveApplicationException(ex);
                //Environment.Exit(0);
            }
        }

#if Enable_License
        private static void OnLicenseExpired(object sender, EventArgs e)
        {
            //
            // stop proper application process here.
            //
            try
            {
                LocalCommon.SaveApplicationException(new Exception(ProductValidationEngine.Current.LastError));
            }
            catch (Exception)
            { }

            Environment.Exit(1);
        }

#endif

#if Enable_Dongle
        private static void OnDongleRemoved(object sender, EventArgs e)
        {
            //
            // stop proper application process here.
            //
            try
            {
                LocalCommon.SaveApplicationException(new Exception("Unexpected Shutdown: Dongle Not Found...!!"));
            }
            catch (Exception)
            { }

            Environment.Exit(1);
        }

#endif

        private static string GetSecKey(string strData)
        {
            string secKey = null;
            try
            {
                string[] salts = new string[] { "asfjasfasfdafdasfjasldfkjaslfjaslfjashfauyqw78ey591274urasefh98q7587ghzxjkdvhksryw89",
									   "sfiajsl;fjq90285jaoru9woasdflkj()UJOfjlkjjoaiejfoapf;jas;fuqw9urofjaOJOFJOSDFJasdlsa",
									   "sjaslfj9a87r9pqwrjejo;fqw90820rfjas;dfja90ru839jflas;fj90UO:JIOPfuyasfj897972al;u89a",
									   "jlasdfkjasljLJLFJEodiasjfl89(&)f8iasjdlJHUIY(*YKJnaskdfuasifyhaskldfjasluf9asldfjasu",
									   "alsdfj90*UOJIDFsl;afui90U*Odfjao;sd9f89Aujdcoasj;ga90fuJ(*U(Yfo;jas098HNO:I()78fasdj"};
                secKey = GetHash(strData);
                foreach (string salt in salts)
                {
                    secKey = GetHash(string.Format("{0}{1}{2}", salt, secKey, salt));
                }

            }
            catch (System.Exception)
            {
                throw;
            }

            return secKey.Trim('=');
        }

        private static string GetHash(string str)
        {
            var PhraseAsByte = System.Text.ASCIIEncoding.UTF8.GetBytes(str);
            Byte[] EncryptedBytes;
            using (System.Security.Cryptography.SHA512Managed hash = new System.Security.Cryptography.SHA512Managed())
            {
                EncryptedBytes = hash.ComputeHash(PhraseAsByte);
                hash.Clear();
            }

            return Convert.ToBase64String(EncryptedBytes);
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            try
            {
                LocalCommon.SaveApplicationException(new Exception("Main Application_ThreadException"), 1);
                if (e.Exception != null)
                    LocalCommon.SaveApplicationException(e.Exception);
                else
                    LocalCommon.SaveApplicationException(new Exception("Unknown Error occurred while executing application"));

                DialogResult res = MessageBox.Show("System Critical Error Occurred for details visit System Error Logs,Do you want to stop server?", "System Critical Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (res == DialogResult.OK)
                    LocalCommon.SaveApplicationException(new Exception("Main Application_ThreadException end"), 1);
                //Application.Exit();
            }
            catch { }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                LocalCommon.SaveApplicationException(new Exception("Main CurrentDomain_UnhandledException"), 1);
                if (e.ExceptionObject != null)
                    LocalCommon.SaveApplicationException((Exception)e.ExceptionObject);
                else
                    LocalCommon.SaveApplicationException(new Exception("Unknown Error occurred while executing application"), 1);
                // MessageBox.Show("System Critical Error Occurred for details visit System Error Logs.", "System Critical Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (e.IsTerminating)
                {
                    LocalCommon.SaveApplicationException(new Exception("Main CurrentDomain_UnhandledException e.IsTerminating =" + e.IsTerminating), 1);
                    //Application.Exit();
                }
            }
            catch { }
        }
    }
}
