using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmartEyeControl_7.Properties;

namespace SmartEyeControl_7.DataBase
{
    public partial class Database_Settings : Form
    {
        private string server = "";

        public string Server
        {
            get { return server; }
            set { server = value; }
        }
        private string database = "";

        public string Database
        {
            get { return database; }
            set { database = value; }
        }
        private string userID = "";

        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        private string password = "";

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public Database_Settings()
        {
            InitializeComponent();
        }

        private void btn_DBSettings_OK_Click(object sender, EventArgs e)
        {
            try
            {
                server = txt_server.Text;
                database = txt_Database.Text;
                userID = txt_UserID.Text;
                password = txt_Password.Text;
                ///try to load Settings From application Configuration files
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error","Error Saving Database Connections Settings", MessageBoxButtons.OK);
            }
        }

        private void Database_Settings_Load(object sender, EventArgs e)
        {
            try
            {
                ///Populate Settings On GUI
                txt_server.Text = Server;
                txt_Database.Text = Database;
                txt_UserID.Text = UserID;
                txt_Password.Text = Password;

            }
            catch (Exception ex)
            { 
            }
        }
    }
}
