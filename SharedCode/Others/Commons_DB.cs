using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SharedCode.Others
{
    public class Commons_DB
    {
        public static FileStream fs = null;
        public static StreamWriter wr = null;
        public static BufferedStream _ConsoleStream = null;
        public static readonly int ExceptionMaxLevel = 50;
        public static readonly string LogSourceName = "MySource";
        #region Default_Application_Directory

        public static string GetApplicationConfigsDirectory()
        {
            try
            {
                String fileDirectoryPath = (String)SharedCode.Properties.Settings.Default["ApplicationConfigsDirectory"];
                if (!Directory.Exists(fileDirectoryPath))
                {
                    fileDirectoryPath = Environment.CurrentDirectory + @"\Application_Configs";
                    if (!Directory.Exists(fileDirectoryPath))
                        Directory.CreateDirectory(fileDirectoryPath);
                    SharedCode.Properties.Settings.Default["ApplicationConfigsDirectory"] = fileDirectoryPath;
                    SharedCode.Properties.Settings.Default.Save();
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
                String fileDirectoryPath = (String)SharedCode.Properties.Settings.Default["ApplicationConfigsDirectory"];

                fileDirectoryPath = Environment.CurrentDirectory + @"\Application_IOLogs";
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
                String fileDirectoryPath = (String)SharedCode.Properties.Settings.Default["ApplicationConfigsDirectory"];

                fileDirectoryPath = Environment.CurrentDirectory + @"\Application_Errors";
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

        public static String GetAccessRightsFilePath(string associationName)
        {
            try
            {
                if (String.IsNullOrEmpty(associationName))
                    throw new Exception("Invalid Association Name");
                String fileName = String.Format(@"{0}\{1}.xml", GetApplicationConfigsDirectory(), associationName);
                return fileName;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //Added By Azeem
        ////////////////////////////////////////////////////////////////////////////
        //public static String GetAccessRightsFilePath(string associationName)
        //{
        //    try
        //    {
        //        if (String.IsNullOrEmpty(associationName))
        //            throw new Exception("Invalid Association Name");
        //        String fileName = String.Format(@"{0}\{1}.xml", Commons_DB.GetApplicationConfigsDirectory(), associationName);
        //        return fileName;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        public static string[] Directory_GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            string[] searchPatterns = searchPattern.Split('|');
            List<string> files = new List<string>();
            foreach (string sp in searchPatterns)
                files.AddRange(System.IO.Directory.GetFiles(path, sp, searchOption));
            files.Sort();
            return files.ToArray();
        }
        public static void SaveApplicationException(Exception ex, int curLevel = 5)
        {
            try
            {
                string fileUrl = String.Format(@"{0}\Exceptions\Exception_{1}.txt", Commons_DB.GetApplicationErrorDirectory(), DateTime.Now.ToFileTime());
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

                ///File Lenght Exceeds 20K 
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

            builder.AppendFormat("Unhandled Error{0} {1},details\r\n", DateTime.Now, ex.Message);
            Exception _ex = ex;

            while (_ex != null && curLevel < ExceptionMaxLevel)
            {
                _ex = _ex.InnerException;
                if (_ex == null)
                    break;
                builder.AppendFormat("{0}\r\n", _ex.Message);
                curLevel++;
            }
            if (ex != null)
                builder.AppendLine(ex.StackTrace);
            return builder;
        }
        public static string ConvertToValidString(byte[] array)
        {
            try
            {
                string _toReturn = "";

                byte current_Char = 0;
                if (array != null)
                {
                    current_Char = array[0];
                    //Process First Char
                    //if (current_Char >= 0 && current_Char <= 9)
                    //    _toReturn += current_Char.ToString();
                    //else
                    //    _toReturn += Char.ConvertFromUtf32(current_Char + 48);

                    for (int i = 0; i < array.Length; i++)
                    {
                        current_Char = array[i];
                        if (current_Char < (byte)10 && current_Char >= (byte)0)
                        {
                            _toReturn += current_Char.ToString();
                        }
                        else
                            //_toReturn += Char.ConvertFromUtf32(current_Char + 48);
                            _toReturn += Char.ConvertFromUtf32(current_Char);
                    }

                }
                if (!String.IsNullOrEmpty(_toReturn))
                    _toReturn = _toReturn.TrimEnd(("? " + Convert.ToChar(0x0F)).ToCharArray());
                return _toReturn;
            }
            catch (Exception)
            {
                throw;
            }
        }        /// <summary>
                 /// Converts byte array to string in hex format
                 /// </summary>
                 /// <param name="Hex_Array"></param>
                 /// <returns></returns>
        static public string ArrayToHexString(byte[] Hex_Array)
        {
            string Return_String = "";
            foreach (byte Value in Hex_Array) Return_String += (Value.ToString("X2") + " ");
            return Return_String;
        }
        static public string ArrayToHexString(byte[] Hex_Array, bool h)
        {
            string Return_String = "";
            foreach (byte Value in Hex_Array) Return_String += (Value.ToString("X2"));
            return Return_String;
        }
    }
}
