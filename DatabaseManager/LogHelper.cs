using Common;
using System;
using System.IO;

namespace comm
{
    public class LogHelper
    {
        public LogHelper()
        { 
        
        }

        public void LogConnection(String str)
        {
            try
            {
                String logFilePath = String.Format(@"{0}\ConnectionLog.txt",App_Common.GetApplicationConfigsDirectory());
                using(TextWriter FileWriter = new StreamWriter(logFilePath, true))
                {
                    String txt = String.Format("{0}___{1}",str, DateTime.Now);
                    FileWriter.WriteLine(txt);
                    FileWriter.Flush();
                }
            }
            catch (Exception ex)
            { 
                
            }
        
        }
    
    }
}
