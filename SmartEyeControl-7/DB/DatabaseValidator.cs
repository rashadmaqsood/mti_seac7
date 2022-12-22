using Microsoft.Win32;
using System;
using System.Reflection;

namespace SmartEyeControl_7.DB
{
    public class DatabaseValidator
    {
        DataBaseController dbController = new DataBaseController();
        DbKey paskey_db = new DbKey();
        private uint INITIAL_APP_VERSION = 480; //4.8.0
        private uint CURRENT_APP_VERSION = Convert.ToUInt32(Assembly.GetExecutingAssembly().GetName().Version.ToString().Replace(".", ""));

        public bool Update_DatabaseValidationKeys(uint LastSavedVersion)
        {
            paskey_db.SecurityKey = Guid.NewGuid().ToString();
            paskey_db.AppVersion = CURRENT_APP_VERSION;

            if (dbController.UpdatePassKey_db(paskey_db, LastSavedVersion))
            {
                RegistryKey rkey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Comunications");
                rkey.SetValue("Comunication1", paskey_db.SecurityKey);
                rkey.SetValue("Comunication2", paskey_db.AppVersion);
                rkey.Dispose();
                return true;
            }
            else
                return false;
        }

        public bool Validate_Database()
        {
            try
            {
                DbKey ky_db = dbController.SelectPassKey_db();
                DbKey ky_reg = new DbKey();
                RegistryKey rkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Comunications");
                if (rkey != null)
                {
                    ky_reg.SecurityKey = rkey.GetValue("Comunication1").ToString();
                    ky_reg.AppVersion = Convert.ToUInt32(rkey.GetValue("Comunication2").ToString());
                    //rkey.Close();
                }

                //DB //App //Reg
                if (ky_db.AppVersion == INITIAL_APP_VERSION && rkey == null)    //4.8.25 //4.8.25 //null
                {
                    return Update_DatabaseValidationKeys(INITIAL_APP_VERSION); //It means First Time application run //Create keys in Registry and update both values in DB
                }
                else if (ky_reg.SecurityKey == ky_db.SecurityKey && ky_reg.AppVersion == ky_db.AppVersion && ky_db.AppVersion == CURRENT_APP_VERSION)  //4.8.25 //4.8.25 //4.8.25
                {
                    rkey.Dispose();
                    return Update_DatabaseValidationKeys(CURRENT_APP_VERSION); //Update GUID in DB/Registry and Run application
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

            #region commented
            //else if (ky_db.AppVersion == INITIAL_APP_VERSION && ky_reg.AppVersion == CURRENT_APP_VERSION) //4.8.0 //4.8.25 //4.8.25
            //{
            //    //Old DB has been replaced after application running>=1 times
            //    return false;
            //}
            //else if (ky_db.AppVersion > INITIAL_APP_VERSION && ky_db.AppVersion < CURRENT_APP_VERSION && ky_reg.AppVersion == CURRENT_APP_VERSION) //4.8.25 //4.8.25 //4.8.25
            //{
            //    //New application version is installed and Old version DB has been pasted
            //    return false;
            //}
            //else if (ky_db.AppVersion > CURRENT_APP_VERSION && ky_reg.AppVersion == CURRENT_APP_VERSION) //4.8.26 //4.8.25 //4.8.25
            //{
            //    //Old application version is installed and New version DB has been pasted
            //    return false;
            //}
            //else if (ky_db.AppVersion > CURRENT_APP_VERSION && ky_reg.AppVersion > CURRENT_APP_VERSION && ky_db.AppVersion == ky_reg.AppVersion) //4.8.26 //4.8.25 //4.8.26
            //{
            //    //Old application version is installed and New version DB has been pasted
            //    return false;
            //}
            //else if (ky_db.AppVersion == ky_reg.AppVersion && ky_reg.AppVersion > CURRENT_APP_VERSION) //4.8.25 //4.8.26 //4.8.25
            //{
            //    //Old application version is installed and New version DB has been pasted
            //    return false;
            //}
            #endregion
        }
    }
    public class DbKey
    {
        string securityKey = Guid.NewGuid().ToString();
        uint appVersion = 0;

        public string SecurityKey
        {
            get { return securityKey; }
            set { securityKey = value; }
        }
        public uint AppVersion
        {
            get { return appVersion; }
            set { appVersion = value; }
        }
    }
}
