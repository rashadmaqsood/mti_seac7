using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using SmartEyeControl_7.Properties;
//using AccurateOptocomSoftware.Properties;

namespace DataBase
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
                DatabaseManager.Properties.Settings.Default["DBIP"] = server;
                
            }
        }
        private string database;

        public string Database
        {
            get { return database; }
            set 
            { 
                database = value;
                DatabaseManager.Properties.Settings.Default["Database"] = database;
                
            }
        }
        private string userID;

        public string UserID
        {
            get { return userID; }
            set 
            { 
                userID = value;
                DatabaseManager.Properties.Settings.Default["UserId"] = userID;
                
            }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set 
            { 
                password = value;
                DatabaseManager.Properties.Settings.Default["Password"] = password;
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
                Server = (String)DatabaseManager.Properties.Settings.Default["DBIP"];
                Database = (String)DatabaseManager.Properties.Settings.Default["Database"];
                UserID = (String)DatabaseManager.Properties.Settings.Default["UserId"];
                Password = (String)DatabaseManager.Properties.Settings.Default["Password"];
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
