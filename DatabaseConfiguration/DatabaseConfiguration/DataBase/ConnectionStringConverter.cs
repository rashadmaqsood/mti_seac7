using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConfiguration.DataBase
{
    internal class ConnectionStringConverter
    {
        public static string ODBCtoMySqlConnectionString(string dsn)
        {
            RegistryKey reg = GetRegistry(Registry.CurrentUser, dsn);
            //reg = reg.OpenSubKey("ODBC");
            //reg = reg.OpenSubKey("ODBC.INI");
            // reg = reg.OpenSubKey("ODBC Data Sources");
            //reg = reg.OpenSubKey(dsn);
            if(reg == null)
            {
                reg = GetRegistry(Registry.LocalMachine,dsn);
            }
            if (reg != null)
            {
                var server = reg.GetValue("SERVER");
                var db = reg.GetValue("DATABASE");
                var pwd = reg.GetValue("PWD");
                var user = reg.GetValue("UID");
                if (server != null && db != null && user != null && pwd != null)
                {
                    return $"Server={server};Database={db};Uid={user};Pwd={pwd}";
                }
            }
            return null;
        }
        public static RegistryKey GetRegistry(RegistryKey registry,string dsn)
        {
            RegistryKey reg = (registry).OpenSubKey($"Software");
            reg = reg?.OpenSubKey("ODBC");
            reg = reg?.OpenSubKey("ODBC.INI");
            reg = reg?.OpenSubKey(dsn);
            return reg;
        }
    }
}
