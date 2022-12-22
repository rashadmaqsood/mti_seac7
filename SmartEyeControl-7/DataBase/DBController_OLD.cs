using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartEyeControl_7.Properties;
using AccurateOptocomSoftware.Properties;

namespace SmartEyeControl_7.DataBase
{
   public class DBController_OLD
    {
        private string server;

        public string Server
        {
            get { return server; }
            set 
            { 
                server = value;
                Settings.Default["DBIP"] = server;
                
            }
        }
        private string database;

        public string Database
        {
            get { return database; }
            set 
            { 
                database = value;
                Settings.Default["Database"] = database;
                
            }
        }
        private string userID;

        public string UserID
        {
            get { return userID; }
            set 
            { 
                userID = value;
                Settings.Default["UserId"] = userID;
                
            }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set 
            { 
                password = value;
                Settings.Default["Password"] = password;
            }
        }

        #region default constructor

        public DBController_OLD()
        {
            //this.server = "192.168.30.213";
            //this.Database = "testDb";
            //this.UserID = "galaxy";
            //this.Password = "";
            try
            {
                ///Try to load application configurations from files
                Server = (String)Settings.Default["DBIP"];
                Database = (String)Settings.Default["Database"];
                UserID = (String)Settings.Default["UserId"];
                Password = (String)Settings.Default["Password"];
            }
            catch (Exception ex)
            { 
            
            }
            finally
            {

            }
        } 

        #endregion

       
    }
}
