using DatabaseManager.Properties;
using System.Data.Odbc;
using Serenity.Util;
using SharedCode.Common;
using System;
using System.Data;
using System.Data.Common;

namespace DatabaseManager.Database
{
    public class DBConnect : IDisposable
    {
        #region DataMembers
        private OdbcConnection connection;
        private static OdbcConnectionStringBuilder ConnectionString;
        private OdbcCommand cmd = null;
        #endregion

        #region Properties
        public virtual OdbcConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }
        public OdbcCommand Command
        {
            get { return cmd; }
            set { cmd = value; }
        }
        public Boolean IsConnectionOpen
        {
            get
            {
                if (connection == null || connection.State == ConnectionState.Broken || connection.State == ConnectionState.Closed)
                    return false;
                else if (Connection.State == ConnectionState.Open)
                    return true;
                else
                    return false;

            }

        }
        #endregion

        #region Constructor
        void BuildConnectionString()
        {
            ConnectionString = new OdbcConnectionStringBuilder(DatabaseManager.Properties.Settings.Default.MDC_DSN);
            //ConnectionString = new MySqlConnectionStringBuilder(Crypto.Decrypt(DatabaseManager.Properties.Settings.Default.MDC_DSN, Commons.Key_ConStr));
            ConnectionString["Pooling"] = true;
            ConnectionString["MinimumPoolSize"] = 20;
            ConnectionString["MaximumPoolsize"] = Convert.ToUInt32(Settings.Default.MaxPoolSize);
            ConnectionString["ConnectionReset"] = true;
            ConnectionString["ConnectionLifeTime"] = 300;
        }
        public DBConnect()
        {
            Initialize();

        }

        private void Initialize()
        {

        }
        #endregion

        #region Connection

        // Establishing Database Connection
        private void establishConnection()
        {
            //ConnectionString["DSN"] =  Settings.Default.MDC_DSN;
            //BuildConnectionString();
            //string ConnStr = Crypto.Decrypt(DatabaseManager.Properties.Settings.Default.MDC_DSN, _key);

            //Connection = new MySqlConnection(ConnectionString.ToString());
            string dsn = string.Format("Dsn={0}", DatabaseManager.Properties.Settings.Default.MDC_DSN);
            Connection = new OdbcConnection
            {
                ConnectionString = dsn
            };
            //Connection = new  OdbcConnection(ConnectionString.ToString());
        }

        //Open Database Connection
        public virtual bool OpenConnection()
        {
            try
            {
                if (Connection != null)
                    connection.Dispose();
                this.establishConnection();
                Connection.Open();
                return true;
            }
            catch (OdbcException ex)
            {
                //When handling errors, you can your application's response based on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.ErrorCode)
                {
                    case 0:
                        //MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        //MessageBox.Show("Invalid username/password, please try again");
                        break;
                    default:
                        //   MessageBox.Show(ex.Message);
                        break;
                }
                return false;
            }
        }

        //Close Database Connection
        public bool CloseConnection()
        {
            try
            {
                if (Connection != null)
                {
                    Connection.Close();
                    DisposeConnection();
                    Connection = null;
                }
                return true;
            }
            catch (OdbcException)
            {
                return false;
            }
        }

        //Dispose Database Connection
        public void DisposeConnection()
        {
            try
            {
                if (Connection != null)
                    Connection.Dispose();
            }
            catch (Exception) { }
        }

        #endregion

        #region IDisposeable Member

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Dispose();
                connection = null;
                // OdbcConnection.ReleaseObjectPool();
            }
            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }
            ConnectionString = null;
        }

        #endregion

        ~DBConnect()
        {
            try
            {
                Dispose();
            }
            catch (Exception)
            { }
        }
    }
}
