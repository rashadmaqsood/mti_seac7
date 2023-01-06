using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace dlmsConfigurationTool.Commen
{
    public class Common
    {
        public const string Real_Number_Regex = @"([0-9]+)|([0-9]*[1-9]+[0-9]*\.[0-9]*)|([0-9]*\.[0-9]*[1-9]+[0-9]*)|(^[0-9]*[1-9]+[0-9]*)";

        public static long Parse_Long_Number(string Orignal_String, int search_index = 0)
        {
            // Matches the first number with or without leading minus.
            var matches = Regex.Matches(Orignal_String, Real_Number_Regex, RegexOptions.Compiled);

            foreach (Match match in matches)
            {
                if (match.Success && match.Index >= search_index)
                {
                    // No need to TryParse here, the match has to be at least
                    // a 1-digit number.
                    return long.Parse(match.Value);
                }
            }
            return -1; // Or any other default value.
        }

        public static void LogIntoFile(Exception e)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(Directory.GetCurrentDirectory() + "\\SmartMeterDataBaseLogs.txt"))
                {
                    writer.Write("\r\nLog Entry : ");
                    writer.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                        DateTime.Now.ToLongDateString());
                    writer.WriteLine("Details:");
                    writer.WriteLine("Message:{0}", e.Message);
                    writer.WriteLine("<(*)----------------=============---------------(*)>");
                    writer.WriteLine("Stack Trace:{0}", e.StackTrace);
                    writer.WriteLine("<(*)----------------=============---------------(*)>");
                    writer.WriteLine();
                }
            }
            catch (Exception)
            {


            }
        }
    }
}
