


using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;

namespace dlmsConfigurationTool
{
    internal class Config
    {
        #region Constructor

        private Config()
        {
            try
            {
                //this.LoadConfig();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        #endregion // Constructor

        #region Methods

        public bool LoadConfig()
        {
            bool result = false;
            this._config = ConfigurationManager.OpenExeConfiguration(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DatabaseConfiguration.dll"));
            if (this._config == null) return result;

            result = true;

            return result;
        }

        #endregion // Methods

        #region Properties

        public static Config Current
        {
            get
            {
                return s_current ?? (s_current = new Config());
            }

        }

        #endregion // Properties

        #region Fields

        static Config s_current;
        Configuration _config;

        #endregion // Fields
    }
}
