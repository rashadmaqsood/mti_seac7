using DatabaseConfiguration.CommonModels;
using DatabaseConfiguration.DataSet;
using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Windows.Forms;

namespace dlmsConfigurationTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ModelConfigurator());

            //try
            //{
            //    MySqlConnection mySql_conn = new MySqlConnection
            //    {
            //        ConnectionString = "User ID=root;Password=786;Host=192.168.100.115;Port=3306;Database=mdc_rfp135;Protocol = TCP; Compress = false; Pooling = true; Min Pool Size = 0; Max Pool Size = 100; Connection Lifetime = 0; Convert Zero Datetime = True;SSL Mode=None"
            //    };
            //    SQLiteConnection sqlite_conn = new SQLiteConnection
            //    {
            //        ConnectionString = new SQLiteConnectionStringBuilder() { DataSource = "Application_Configs/SEAC.db", ForeignKeys = true }.ConnectionString
            //    };
            //    SqlConnection sql_conn = new SqlConnection
            //    {
            //        ConnectionString = @"Data Source=Dell-20\SQLEXPRESS;Initial Catalog=DB_QC_Check;Integrated Security = true;"
            //    };
            //    DBControllerImpl db = new DBControllerImpl();
            //    Configs newConfig = new Configs();
            //    //db.Load_All_Configurations(sqlite_conn, ref newConfig);
            //    //db.Load_All_Configurations(mySql_conn, ref newConfig);
            //    db.Load_All_Configurations(sql_conn, ref newConfig);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
    }
}
