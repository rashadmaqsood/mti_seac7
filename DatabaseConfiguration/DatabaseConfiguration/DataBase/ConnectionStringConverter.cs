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
            //Computer\HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\ODBC
            RegistryKey reg = (Registry.LocalMachine).OpenSubKey($@"Software\ODBC\ODBC.INI\{dsn}");
            if (reg == null)
            {
                reg = (Registry.CurrentUser).OpenSubKey($@"Software\ODBC\ODBC.INI\{dsn}");
            }
            if (reg == null)
            {
                reg = (Registry.LocalMachine).OpenSubKey($@"Software\WOW6432Node\ODBC\ODBC.INI\{dsn}");
            }
            //reg = reg.OpenSubKey(dsn);
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
    }
}
