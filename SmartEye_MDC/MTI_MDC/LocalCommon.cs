using Communicator.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Communicator.MTI_MDC
{
    public static class LocalCommon
    {
        public static readonly int ExceptionMaxLevel = 50;

        #region Default_Application_Directory

        public static string GetApplicationConfigsDirectory()
        {
            try
            {
                String fileDirectoryPath = (String)Settings.Default["ApplicationConfigsDirectory"];
                if (!Directory.Exists(fileDirectoryPath))
                {
                    fileDirectoryPath = Directory.GetCurrentDirectory() + @"\Application_Configs"; //Environment.CurrentDirectory + @"\Application_Configs";
                    if (!Directory.Exists(fileDirectoryPath))
                        Directory.CreateDirectory(fileDirectoryPath);
                    Settings.Default["ApplicationConfigsDirectory"] = fileDirectoryPath;
                    Settings.Default.Save();
                }
                return fileDirectoryPath;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static string GetApplicationLogsDirectory()
        {
            try
            {
                String fileDirectoryPath = (String)Settings.Default["ApplicationConfigsDirectory"];

                fileDirectoryPath = Directory.GetCurrentDirectory() + @"\Application_IOLogs";// Environment.CurrentDirectory + @"\Application_IOLogs";
                if (!Directory.Exists(fileDirectoryPath))
                    Directory.CreateDirectory(fileDirectoryPath);

                return fileDirectoryPath;
            }
            catch (Exception ex)
            {
                return Environment.CurrentDirectory;
            }
        }

        public static string GetApplicationErrorDirectory()
        {
            try
            {
                String fileDirectoryPath = (String)Settings.Default["ApplicationConfigsDirectory"];

                fileDirectoryPath = Directory.GetCurrentDirectory() + @"\Application_Errors";// Environment.CurrentDirectory + @"\Application_Errors";
                if (!Directory.Exists(fileDirectoryPath))
                    Directory.CreateDirectory(fileDirectoryPath);

                return fileDirectoryPath;
            }
            catch (Exception ex)
            {
                return Environment.CurrentDirectory;
            }
        }

        public static AppDomain GetCurrentAppDomain()
        {
            AppDomain domain = null;
            try
            {
                ///Load Current Application Domain
                AppDomain root = AppDomain.CurrentDomain;
                AppDomainSetup setup = new AppDomainSetup();
                domain = AppDomain.CreateDomain(GetApplicationConfigsDirectory(), null, setup);
            }
            catch
            {
                domain = AppDomain.CurrentDomain;
            }
            return domain;
        }
        #endregion

        public static void LogMDCExceptionIntoFile(Exception e)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(Directory.GetCurrentDirectory() + "\\MDC_Exception_Log.txt"))
                {
                    var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                    writer.Write("\r\nMTI_MDC Version {0} ", version);
                    writer.Write("\r\nLog Entry : ");
                    writer.WriteLine("{0}\t {1}", DateTime.Now.ToLongTimeString(),
                        DateTime.Now.ToLongDateString());
                    writer.WriteLine("Details:");
                    writer.WriteLine("Message:{0}", e.Message);
                    writer.WriteLine("<=+---------------------------------=============--------------------------------+=>");
                    writer.WriteLine("Stack Trace:{0}", e.StackTrace);
                    writer.WriteLine("<=+---------------------------------=============--------------------------------+=>");
                    writer.WriteLine();
                }
            }
            catch (Exception)
            {
            }
        }

        public static void SaveApplicationException(Exception ex, int curLevel = 5)
        {
            try
            {
                string fileUrl = String.Format(@"{0}\Exceptions\Exception_{1}.txt", GetApplicationErrorDirectory(), DateTime.Now.ToFileTime());
                SaveException(ex, fileUrl);
            }
            catch (Exception) { }
        }

        public static void SaveException(Exception ex, string fileURL, int curLevel = 14)
        {
            Exception _ex = ex;
            try
            {
                FileInfo FileINfo = new FileInfo(fileURL);
                string fileName = FileINfo.Name;
                string dirName = FileINfo.DirectoryName;

                // File Lenght Exceeds 20K 
                if (FileINfo.Exists && FileINfo.Length > 20480)
                    BackUpFileSameLoc(fileURL);

                using (StreamWriter wr = ExceptionsInit(dirName, fileName))
                {
                    StringBuilder builder = BuildExceptionLog(ex, curLevel);
                    wr.WriteLine(builder.ToString());
                }
            }
            catch (Exception) { }
        }

        public static void BackUpFileSameLoc(string fileUrl)
        {
            try
            {
                FileInfo FileINfo = new FileInfo(fileUrl);
                if (FileINfo.Exists)
                {
                    String NewFileName = FileINfo.Name.Replace(FileINfo.Extension, "");
                    NewFileName = String.Format("{0}_{1}_{2}", NewFileName, DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString());
                    ///Process New File Name
                    NewFileName = NewFileName.Replace(" ", "");
                    NewFileName = NewFileName.Replace(".", "");
                    NewFileName = NewFileName.Replace("/", "");
                    NewFileName = NewFileName.Replace(":", "");
                    NewFileName = String.Format(@"{0}\{1}{2}", FileINfo.Directory, NewFileName, FileINfo.Extension);
                    File.Move(fileUrl, NewFileName);
                    File.Delete(fileUrl);
                }
            }
            catch
            {
            }
        }
        public static StreamWriter ExceptionsInit(string relativeDirPath, string fileName)
        {
            try
            {
                FileStream fs = null;
                StreamWriter wr = null;

                string dir = relativeDirPath;
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                string path = dir + String.Format("\\{0}", fileName);
                if (!File.Exists(path))
                    fs = new FileStream(path, FileMode.CreateNew);
                else
                    fs = File.Open(path, FileMode.Append);
                wr = new StreamWriter(fs);
                return wr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static StringBuilder BuildExceptionLog(Exception ex, int curLevel = 14)
        {
            StringBuilder builder = new StringBuilder(512);

            builder.AppendFormat("Communicator Unhandled Error {0} {1},details\r\n", DateTime.Now, ex.Message);
            Exception _ex = ex;

            while (_ex != null && curLevel < ExceptionMaxLevel)
            {
                _ex = _ex.InnerException;
                if (_ex == null)
                    break;
                builder.AppendFormat("{0}\r\n", _ex.Message);
                builder.AppendFormat("{0}\r\n", ex.StackTrace);
                curLevel++;
            }
            if (ex != null)
                builder.AppendLine(ex.StackTrace);
            return builder;
        }

    }
}
