using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SharedCode.Others
{
    public class FileHandler
    {
        string dateformate = "yyyy-MM-dd";
        string timeformat = "hh:MM:ss tt";
        string separator = "========================================================================";
        string errorSeparator = "================================================";
        string logPath = @"D:\mdc_log\DataProcess_";
        string errorPath = @"D:\Errors\Error_";

        private List<string> ExtractMSN(List<string> filesName)
        {
            List<string> msn = new List<string>();
            char[] delimiters = new char[] { '_', '.', '\n', };

            foreach (var name in filesName)
            {
                string[] tokens = name.Split(delimiters);
                msn.Add(tokens[1]);
            }
            return msn;

        }

        #region log
        public string ReadCompleteFile(String msn)
        {
            String fName = logPath + msn + ".txt";
            string log = "";
            string row = "";
            if (File.Exists(fName))
            {
                // string[] lines = File.ReadAllLines(fName);
                StreamReader stream = new StreamReader(fName);
                log = stream.ReadToEnd();
            }

            return log;
        }
        public string ReadLogFile_ByDate(String msn, string selectedDate)
        {

            string[] lines = null;
            string logString = "";
            string exractedDate = "";
            DateTime datetime = new DateTime();
            String fName = logPath + msn + ".txt";
            StringBuilder Logging = new StringBuilder(75);
            if (File.Exists(fName))
            {
                lines = File.ReadAllLines(fName);
            }
            if (lines.Length > 0)
            {
                for (int index = 0; index < lines.Length; index++)
                {
                    string line = lines[index];
                    string[] token = line.Split(':');
                    if (token[0].Equals("Incoming Connection "))// check for new log start
                    {
                        string[] tokens = line.Split('_');
                        datetime = DateTime.Parse(tokens[1]);//extract date
                        exractedDate = datetime.ToString(dateformate);
                        if (exractedDate.Equals(selectedDate))
                        {

                            while (line != separator)
                            {
                                line = lines[index++];
                                Logging.AppendLine(line.ToString());
                                if (index == lines.Length)
                                {
                                    break;
                                }
                            }
                            // Logging.Append(separator + "\n");
                        }

                    }
                    else
                    {
                    }
                }
            }
            else
            {
            }
            logString = Logging.ToString();
            return logString;
        }
        public string ReadLogFile_ByDateTime(String msn, string selectedDate, string timeString1, string timeString2)
        {

            string[] lines = null;
            string logString = "";
            string exractedDate = "";
            string extracedTime = "";
            DateTime datetime = new DateTime();
            String fName = logPath + msn + ".txt";
            StringBuilder Logging = new StringBuilder(75);
            if (File.Exists(fName))
            {
                lines = File.ReadAllLines(fName);
            }
            if (lines.Length > 0)
            {
                for (int index = 0; index < lines.Length; index++)
                {
                    string line = lines[index];
                    string[] token = line.Split(':');
                    if (token[0].Equals("Incoming Connection "))// check for new log start
                    {
                        string[] tokens = line.Split('_');
                        datetime = DateTime.Parse(tokens[1]);//extract date
                        exractedDate = datetime.ToString(dateformate);
                        if (exractedDate.Equals(selectedDate))
                        {
                            extracedTime = datetime.ToString(timeformat);
                            int result = String.Compare(extracedTime, timeString1);
                            int time1Result = extracedTime.CompareTo(timeString1);
                            int time2Result = extracedTime.CompareTo(timeString2);
                            if (extracedTime.CompareTo(timeString1) >= 0 && extracedTime.CompareTo(timeString2) <= 0)
                            {
                                while (line != separator)
                                {
                                    Logging.AppendLine(line.ToString());
                                    line = lines[index++];
                                    if (index == lines.Length)
                                    {
                                        break;
                                    }
                                }
                                Logging.Append(separator + "\n");
                            }

                        }

                    }
                    else
                    {
                    }
                }
            }
            else
            {
            }
            logString = Logging.ToString();
            return logString;
        }
        public string ReadLogFile_ByDateTime(String msn, string dateTimeString1, string dateTimeString2)
        {

            string[] lines = null;
            string logString = "";
            string exractedDate = "";
            string extracedTime = "";
            string extractedDateTime = "";
            DateTime datetime = new DateTime();
            String fName = logPath + msn + ".txt";
            StringBuilder Logging = new StringBuilder(75);
            if (File.Exists(fName))
            {
                lines = File.ReadAllLines(fName);
            }
            if (lines.Length > 0)
            {
                for (int index = 0; index < lines.Length; index++)
                {
                    string line = lines[index];
                    string[] token = line.Split(':');
                    if (token[0].Equals("Incoming Connection "))// check for new log start
                    {
                        string[] tokens = line.Split('_');
                        datetime = DateTime.Parse(tokens[1]);//extract date
                        extractedDateTime = datetime.ToString();
                        int CompDTResult1 = extractedDateTime.CompareTo(dateTimeString1);
                        int CompDTResult2 = extractedDateTime.CompareTo(dateTimeString2);
                        if (CompDTResult1 >= 0 && CompDTResult2 <= 0)
                        {
                            while (line != separator)
                            {
                                Logging.AppendLine(line.ToString());
                                line = lines[index++];
                                if (index == lines.Length)
                                {
                                    break;
                                }
                            }
                            Logging.Append(separator + "\n");
                        }

                    }
                    else
                    {
                    }
                }
            }
            else
            {
            }
            logString = Logging.ToString();
            return logString;
        }
        public List<string> GetMsnFromFile()
        {
            string logPath = @"D:\mdc_log";
            List<string> filesList = new List<string>();
            List<string> msn = new List<string>();
            try
            {
                DirectoryInfo di = new DirectoryInfo(logPath);
                var files = di.GetFiles();
                foreach (var file in files)
                {
                    filesList.Add(file.Name);
                }
                msn = ExtractMSN(filesList);
               // return msn;
            }
            catch (System.Exception ex)
            {
               // return null;
               // MessageBox.Show(ex.Message);
            }
            return msn;
        }

        public List<string> Get_DateListFromFile(String msn)
        {

            string[] lines = null;
            string exractedDate = "";
            DateTime datetime = new DateTime();
            List<string> dateList = new List<string>();
            StringBuilder Logging = new StringBuilder(75);
            String fName = logPath + msn + ".txt";
            if (File.Exists(fName))
            {
                lines = File.ReadAllLines(fName);
            }
            if (lines.Length > 0)
            {
                for (int index = 0; index < lines.Length; index++)
                {
                    string line = lines[index];
                    string[] token = line.Split(':');
                    if (token[0].Equals("Incoming Connection "))// check for new log start
                    {
                        string[] tokens = line.Split('_');
                        datetime = DateTime.Parse(tokens[1]);//extract date
                        exractedDate = datetime.ToString(dateformate);
                        dateList.Add(exractedDate);
                    }
                    else
                    {
                    }
                }
            }
            else
            {
            }
            List<string> distinctDates = dateList.Distinct().ToList();
            return distinctDates;
        }
        #endregion
        #region errors
        public string ReadError_CompleteFile(String msn)
        {
            String fName = errorPath + msn + ".txt";
            string log = "";
            string row = "";
            if (File.Exists(fName))
            {
                // string[] lines = File.ReadAllLines(fName);
                StreamReader stream = new StreamReader(fName);
                log = stream.ReadToEnd();
            }
            return log;
        }
        public string ReadErrorFile_ByDate(String msn, string selectedDate)
        {

            string[] lines = null;
            string logString = "";
            string exractedDate = "";
            DateTime datetime = new DateTime();
            String fName = errorPath + msn + ".txt";
            StringBuilder Logging = new StringBuilder(75);
            if (File.Exists(fName))
            {
                lines = File.ReadAllLines(fName);
            }
            if (lines.Length > 0)
            {
                for (int index = 0; index < lines.Length; index++)
                {
                    string line = lines[index];
                    string[] token = line.Split(':');
                    if (token[0].Equals("Error "))// check for new log start
                    {
                        string[] tokens = line.Split('_');
                        datetime = DateTime.Parse(tokens[1]);//extract date
                        exractedDate = datetime.ToString(dateformate);
                        if (exractedDate.Equals(selectedDate))
                        {

                            while (line != errorSeparator)
                            {
                                line = lines[index++];
                                Logging.AppendLine(line.ToString());
                                if (index == lines.Length)
                                {
                                    break;
                                }
                            }
                            // Logging.Append(errorSeparator + "\n");
                        }

                    }
                    else
                    {
                    }
                }
            }
            else
            {
            }
            logString = Logging.ToString();
            return logString;
        }
        public List<string> GetMsnFromErrorFile()
        {
            string logPath = @"D:\Errors";
            List<string> filesList = new List<string>();
            List<string> msn = new List<string>();
            try
            {
                DirectoryInfo di = new DirectoryInfo(logPath);
                var files = di.GetFiles();
                foreach (var file in files)
                {
                    filesList.Add(file.Name);
                }
                msn = ExtractMSN(filesList);
            }
            catch (System.Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
            return msn;
        }
        public List<string> Get_DateListFromErrorFile(String msn)
        {

            string[] lines = null;
            string exractedDate = "";
            DateTime datetime = new DateTime();
            List<string> dateList = new List<string>();
            StringBuilder Logging = new StringBuilder(75);
            String fName = errorPath + msn + ".txt";
            if (File.Exists(fName))
            {
                lines = File.ReadAllLines(fName);
            }
            if (lines.Length > 0)
            {
                for (int index = 0; index < lines.Length; index++)
                {
                    string line = lines[index];
                    string[] token = line.Split(':');
                    if (token[0].Equals("Error "))// check for new log start
                    {
                        string[] tokens = line.Split('_');
                        datetime = DateTime.Parse(tokens[1]);//extract date
                        exractedDate = datetime.ToString(dateformate);
                        dateList.Add(exractedDate);
                    }
                    else
                    {
                    }
                }
            }
            else
            {
            }
            List<string> distinctDates = dateList.Distinct().ToList();
            return distinctDates;
        }
        public string ReadErrorFile_ByDateTime(String msn, string dateTimeString1, string dateTimeString2)
        {

            string[] lines = null;
            string logString = "";
            string extractedDateTime = "";
            DateTime datetime = new DateTime();
            String fName = errorPath + msn + ".txt";
            StringBuilder Logging = new StringBuilder(75);
            if (File.Exists(fName))
            {
                lines = File.ReadAllLines(fName);
            }
            if (lines.Length > 0)
            {
                for (int index = 0; index < lines.Length; index++)
                {
                    string line = lines[index];
                    string[] token = line.Split(':');
                    if (token[0].Equals("Error "))// check for new log start
                    {
                        string[] tokens = line.Split('_');
                        datetime = DateTime.Parse(tokens[1]);//extract date
                        extractedDateTime = datetime.ToString();
                        int CompDTResult1 = extractedDateTime.CompareTo(dateTimeString1);
                        int CompDTResult2 = extractedDateTime.CompareTo(dateTimeString2);
                        if (CompDTResult1 >= 0 && CompDTResult2 <= 0)
                        {
                            while (line != errorSeparator)
                            {
                                line = lines[index++];
                                Logging.AppendLine(line.ToString());
                                if (index == lines.Length)
                                {
                                    break;
                                }
                            }
                            //Logging.Append(separator + "\n");
                        }

                    }
                    else
                    {
                    }
                }
            }
            else
            {
            }
            logString = Logging.ToString();
            return logString;
        }
        #endregion

    }


}
