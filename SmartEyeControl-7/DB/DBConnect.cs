using SharedCode.Comm.DataContainer;
using SharedCode.Comm.Param;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SmartEyeControl_7.DB
{
    public enum Data_Type : byte
    {
        Boolean = 00,
        String = 01,
        Double = 02,
    }
    
    public class DBConnect
    {
        //structures used to save data items
        public struct Insert_Record
        {
            public uint session_id;
            public DateTime arrival_time;
            public string msn;
            public char cat_id;
            public long qty_id;
            public String qty_Label;
            public Object value;
            public sbyte scalar;
            public string unit;
            //public Data_Type DataType;
        }
        public struct Insert_Record_LoadProfile
        {
            public uint session_id;
            public DateTime arrival_time;
            public string msn;
            //public char cat_id;
            public long qty_id;
            public Object value;
            public sbyte scalar;
            public string unit;
            public int counter;
            public int interval;
            public int channel_id;
        }
        public struct Insert_Evnent_CL_Counter
        {
            public uint session_id;
            public DateTime arrival_time;
            public string msn;
            public UInt64 qty_id;
            public uint counter;
            public string description;
            public bool is_Indvidual;
        }

        private SQLiteConnection connection;

        private string server;
        private string database;
        private string uid;
        private string password;

        public static string ConnectionString 
        {
            get 
            {
                String ConnString = null;
                try
                {
                    ConnString = OptocomSoftware.Properties.Settings.Default.SEAC7_DBConnectionString;
                    //ConnString = @"Provider=Microsoft.ACE.SQLite.12.0;Data Source=Application_Configs\SEAC7_DB.mdb;Persist Security Info=True;Jet SQLite:Database Password=$v#1S0ot@5";
                }
                catch
                {
                    //ConnString = @"Provider=Microsoft.ACE.SQLite.12.0;Data Source=Application_Configs\SEAC7_DB.mdb;Persist Security Info=True;Jet SQLite:Database Password=$v#1S0ot@5";
                }
                return ConnString;
            }
        }

        private static string Session_ID = "";
        private static int max = 0;
        int unsuccessful_Count = 0;
        public Boolean ServerConnected = false;
        FileStream f;
        StreamWriter s;
        private Param_ActivityCalendar Calendar_Obj = new Param_ActivityCalendar();
        private static string meterSerialNumber = "";
        SQLiteTransaction tr = null;

        //Constructor DataBase Connection Open
        public DBConnect()
        {
            Initialize();
            Insert_Record[] List_Records = new Insert_Record[100];
        }

        private void Initialize()
        {

        }

        private void establishConnection()
        {
            //DBController_OLD DB_Controller = new DBController_OLD();

            //server = DB_Controller.Server;
            //database = DB_Controller.Database;
            //uid = DB_Controller.UserID;
            //password = DB_Controller.Password;
            //string connectionString;
            //connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new SQLiteConnection(ConnectionString);
        }

        //Close DataBase Connection O
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (SQLiteException ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Initialize values

        //open connection to database
        private int OpenConnection()
        {
            try
            {
                connection.Open();
                ServerConnected = true;
                return -1;
            }
            catch (SQLiteException ex)
            {
                return ex.ErrorCode;
                //When handling errors, you can your application's response based on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                //switch (ex.ErrorCode)
                //{
                //    case 0:
                //        MessageBox.Show("Cannot connect to server.  Contact administrator");
                //        break;

                //    case 1045:
                //        MessageBox.Show("Invalid username/password, please try again");
                //        break;
                //    default:
                //        MessageBox.Show(ex.Message);
                //        break;
                //}
                //return false;
            }
        }

        //saving queries if server is not connected
        public void SaveQueries(string queryName, string query)
        {
            s.WriteLine("{0}", queryName);
            s.WriteLine("{0}", query);
        }

        //Insert statement
        public void Insert()
        {
            Insert_Record obj = new Insert_Record();
            obj.arrival_time = DateTime.MinValue;
            obj.cat_id = 'I';
            obj.msn = "MTI000000127";
            obj.qty_id = 2;
            obj.scalar = 0;
            obj.session_id = 17;
            obj.unit = "KWH";
            obj.value = 123;
            Insert(obj, "");
        }

        //Update statement
        public void Update()
        {

        }

        //Delete statement
        public void Delete()
        {

        }

        //Select statement
        public List<string>[] Select()
        {
            string query = "SELECT * FROM meter_data";

            //Create a list to store the result
            List<string>[] list = new List<string>[3];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();


            //Open connection
            if (this.OpenConnection() == -1)
            {
                //Create Command
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                //Create a data reader and Execute the command
                SQLiteDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["msn"] + "");
                    list[1].Add(dataReader["arrival_time"] + "");
                    list[2].Add(dataReader["value"] + "");

                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        //Count statement
        public int Count()
        {
            string query = "SELECT Count(*) FROM meter_data";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == -1)
            {
                //Create Mysql Command
                SQLiteCommand cmd = new SQLiteCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        //Backup
        public bool Backup()
        {
            try
            {
                DateTime Time = DateTime.Now;
                int year = Time.Year;
                int month = Time.Month;
                int day = Time.Day;
                int hour = Time.Hour;
                int minute = Time.Minute;
                int second = Time.Second;
                int millisecond = Time.Millisecond;

                //Save file to C:\ with the current date as a filename
                string path;
                path = "C:\\" + year + "-" + month + "-" + day + "-" + hour + "-" + minute + "-" + second + "-" + millisecond + ".sql";
                StreamWriter file = new StreamWriter(path);


                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysqldump";
                psi.RedirectStandardInput = false;
                psi.RedirectStandardOutput = true;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", uid, password, server, database);
                psi.UseShellExecute = false;

                Process process = Process.Start(psi);

                string output;
                output = process.StandardOutput.ReadToEnd();
                file.WriteLine(output);
                process.WaitForExit();
                file.Close();
                process.Close();
            }
            catch (IOException ex)
            {
                //return ("Error , unable to backup!");
                return false;
            }
            return true;
        }

        //Restore
        public bool Restore()
        {
            try
            {
                //Read file from C:\
                string path;
                path = "C:\\MySqlBackup.sql";
                StreamReader file = new StreamReader(path);
                string input = file.ReadToEnd();
                file.Close();


                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysql";
                psi.RedirectStandardInput = true;
                psi.RedirectStandardOutput = false;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", uid, password, server, database);
                psi.UseShellExecute = false;


                Process process = Process.Start(psi);
                process.StandardInput.WriteLine(input);
                process.StandardInput.Close();
                process.WaitForExit();
                process.Close();
            }
            catch (IOException ex)
            {
                return false;
                //return ("Error , unable to Restore!");
            }
            return true;
        }

        //Billing and instantaneous
        public void Insert(Insert_Record Arg, string category)
        {
            try
            {
                #region Formating variable values
                string formate = "yyyy-MM-dd HH:mm:ss";
                string arivalTime = Arg.arrival_time.ToString(formate);
                string temp_ArrivalTime = "'" + arivalTime + "'";
                string temp_MSN = "'" + Arg.msn.ToString() + "'";
                string temp_QuantityID = "'" + Arg.qty_id.ToString() + "'";
                string temp_Scaler = "'" + Arg.scalar.ToString() + "'";
                string temp_SessionID = "'" + Session_ID + "'";
                string temp_Unit = "'" + Arg.unit.ToString() + "'";
                //string temp_Value = "'" + Arg.value.ToString() + "'";

                string temp_Value = "";
                try
                {
                    double test = Convert.ToDouble(Arg.value);
                    temp_Value = "'" + Arg.value.ToString() + "'";
                }
                catch (Exception)
                {
                    if (Arg.qty_Label.Contains("Relay") && Arg.value.ToString().Contains("Connected")) temp_Value = "'1'";
                    else if (Arg.qty_Label.Contains("Relay")) temp_Value = "'0'";
                }

                #endregion
                string query = "";
                if (category == "I")
                    query = "INSERT INTO [instantaneous_data_individualitem]([session_id], arrival_time, msn, qty_id,qty_label, [value], [scalar], [unit]) VALUES  (" + temp_SessionID + ", " + temp_ArrivalTime + "," + temp_MSN + ", " + temp_QuantityID + ",'" + Arg.qty_Label + "', " + temp_Value + ", " + temp_Scaler + "," + temp_Unit + ")";
                else if (category == "CB")
                    query = "INSERT INTO [billing_data] ([session_id], arrival_time, msn, qty_id, [value], [scalar], [unit]) VALUES  (" + temp_SessionID + ", " + temp_ArrivalTime + "," + temp_MSN + ", " + temp_QuantityID + ", " + temp_Value + ", " + temp_Scaler + "," + temp_Unit + ")";
                else if (category == "MB")
                    query = "INSERT INTO [monthly_billing_data] ([session_id], arrival_time, msn, qty_id, [value], [scalar], [unit]) VALUES  (" + temp_SessionID + ", " + temp_ArrivalTime + "," + temp_MSN + ", " + temp_QuantityID + ", " + temp_Value + ", " + temp_Scaler + "," + temp_Unit + ")";

                //create command and assign the query and connection from the constructor
                SQLiteCommand cmd = new SQLiteCommand(query, connection);

                if (ServerConnected == true)
                {
                    //Execute command
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        
                        throw;
                    }
                }
                else
                    SaveQueries("InstantaneousValue", query);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void callAllBillingFunctions(List<Insert_Record> List_Records, string category, uint max_Counter)
        {
            string MSN = List_Records[0].msn.ToString();
            //storing session ID for a specific meter
            //if (save_session_info(List_Records[0].msn.ToString(), category) == false)
            //{
            //    save_Info_To_meterList(MSN);
            //}
            //checking for session ID
            getLast_session_ID(MSN);
            #region inserting data according to category
            if (category == "I")
            {
                foreach (var item in List_Records)
                {
                    Insert(item, "I");
                }
            }
            else if (category == "CB")
            {
                foreach (var item in List_Records)
                {
                    Insert(item, "CB");
                }
            }
            else if (category == "MB")
            {
                foreach (var item in List_Records)
                {
                    Insert(item, category);
                    //update_maxCounter((int)max_Counter, category, MSN);
                }
            }
            #endregion
            
            update_session_info();
            CloseConnection();
        }
        
        public bool SaveToDataBase(List<Insert_Record> List_Records, string category, uint max_Counter)
        {
            try
            {
                establishConnection();
                if (this.OpenConnection() == -1)
                {
                    callAllBillingFunctions(List_Records, category, max_Counter);
                    return true;// "Success";
                }
                //if server is not connected function are called only for saving queries to a txt file
                else if (ServerConnected == false)
                {
                    f = new FileStream("Instataneous.txt", FileMode.Append);
                    s = new StreamWriter(f);
                    s.WriteLine("{0}", "*-*-*-*-");
                    callAllBillingFunctions(List_Records, category, max_Counter);
                    s.Close();
                    f.Close();
                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                //return (ex.Message);
                return false;
            }
        }

        public int get_Counter(string category, string MSN)
        {
            SQLiteDataReader reader_maxCounter = null;
            int max_Counter = -1;
            try
            {
                establishConnection();
                if (this.OpenConnection() == -1)
                {
                    string queryForMaxCounter = "SELECT max_count FROM counter_detector WHERE msn='" + MSN + "' AND cat_id='" + category + "'";
                    SQLiteCommand cmd = new SQLiteCommand(queryForMaxCounter, connection);
                    if (ServerConnected == true)
                    {
                        reader_maxCounter = cmd.ExecuteReader();

                        if (reader_maxCounter.Read())
                        {
                            max_Counter = Convert.ToInt32(reader_maxCounter["max_count"]);
                        }
                    }
                    else
                        SaveQueries("max_Counter", queryForMaxCounter);
                }
            }
            catch (Exception error)
            {
                //return (error.Message + "\n get max counter");
                return -1;
            }
            finally
            {
                if (ServerConnected == true)
                {
                    reader_maxCounter.Close();
                }
            }
            return max_Counter;
        }

        //Load Profile
        public bool Insert_LoadProfile(Insert_Record_LoadProfile Arg)
        {

            try
            {
                max = Arg.counter;
                #region  //changing formate for mysql
                string formate = "yyyy-MM-dd HH:mm:ss";
                string arivalTime = Arg.arrival_time.ToString(formate);
                string temp_ArrivalTime = "'" + arivalTime + "'";
                //string temp_CatID = "'" + Arg.cat_id.ToString() + "'";
                string temp_MSN = "'" + Arg.msn.ToString() + "'";
                string temp_QuantityID = "'" + Arg.qty_id.ToString() + "'";
                string temp_Scaler = "'" + Arg.scalar.ToString() + "'";
                string temp_SessionID = "'" + Session_ID + "'";
                string temp_Unit = "'" + Arg.unit.ToString() + "'";
                string temp_Value = "'" + Arg.value.ToString() + "'";
                string temp_counter = "'" + Arg.counter.ToString() + "'";
                string temp_interval = "'" + Arg.interval.ToString() + "'";
                string temp_channel = "'" + Arg.channel_id.ToString() + "'";
                #endregion

                //first enter data in session_info and check for the exception of foriegn key of msn
                string query = "INSERT INTO loadprofile_data VALUES  (" + temp_SessionID + "," + temp_ArrivalTime + "," + temp_MSN + ", " + temp_QuantityID + ", " + temp_Value + "," + temp_Scaler + "," + temp_Unit + "," + temp_counter + "," + temp_interval + "," + temp_channel + ")";

                //create command and assign the query and connection from the constructor
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                if (ServerConnected == true)
                {

                    //Execute command
                    cmd.ExecuteNonQuery();
                    return true;

                }
                else
                {
                    SaveQueries("LoadProfileValue", query);
                    return false;
                }
            }
            catch (SQLiteException ex)
            {
                return false;
                //MessageBox.Show("Error: " + ex.Message);
            }
        }
        public void callAllLoadProfileFunctions(List<Insert_Record_LoadProfile> List_Records,ref int unsuccess)
        {
            string MSN = List_Records[0].msn.ToString();
           
            try
            {
                tr = connection.BeginTransaction();
                //storing session ID for a specific meter
                if (save_session_info(List_Records[0].msn.ToString(), "L") == false)
                {
                    save_Info_To_meterList(MSN);
                }
                //checking for session ID
                getLast_session_ID(MSN);
                foreach (var item in List_Records)
                {
                    if (!Insert_LoadProfile(item))
                    {
                        unsuccessful_Count++;
                    }
                }

                if (max != 0)
                {
                    //update_maxCounter(max, "L", MSN);
                }
                update_session_info();
                tr.Commit();
                unsuccess=unsuccessful_Count;
            }
            catch (SQLiteException ex)
            {
                try
                {
                    //rolling back to its previous state of DB if all command not success
                    tr.Rollback();
                }
                catch (SQLiteException ex1)
                {
                  //  MessageBox.Show("Error:(Rollback) " + ex1.Message);
                }
               // MessageBox.Show("Error: " + ex.Message);
            }
            CloseConnection();
        }
        public bool SaveToDataBase_LoadProfile(List<Insert_Record_LoadProfile> List_Records,ref int unsuccess)
        {

            try
            {
                establishConnection();

                if (this.OpenConnection() == -1)
                {
                    callAllLoadProfileFunctions(List_Records,ref unsuccess);
                    return true;
                }
                else if (ServerConnected == false)
                {
                    f = new FileStream("LoadProfile.txt", FileMode.Append);
                    s = new StreamWriter(f);
                    callAllLoadProfileFunctions(List_Records,ref unsuccess);
                    s.WriteLine("{0}", "*-*-*-*-");
                    s.Close();
                    f.Close();
                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
             //   MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Events++++++
        public void Insert_Events(Insert_Evnent_CL_Counter Arg, string category)
        {
            try
            {
                string formate = "yyyy-MM-dd HH:mm:ss";
                string arivalTime = Arg.arrival_time.ToString(formate);

                string temp_ArrivalTime = "'" + arivalTime + "'";
                string temp_MSN = "'" + Arg.msn.ToString() + "'";
                string temp_QuantityID = "'" + Arg.qty_id.ToString() + "'";
                string temp_SessionID = "'" + Session_ID + "'";
                string temp_Counter = "'" + Arg.counter.ToString() + "'";
                string temp_Description = "'" + Arg.description + "'";
                bool temp_IsIndvidual = Arg.is_Indvidual;
                string query = "";
                if (category == "L")
                    query = "INSERT INTO events_data ([session_id], [arrival_time], msn, qty_id, [counter], [description], is_individual) VALUES  (" + temp_SessionID + ", " + temp_ArrivalTime + "," + temp_MSN + ", " + temp_QuantityID + ", " + temp_Counter + ", " + temp_Description + "," + temp_IsIndvidual + ")";

                //create command and assign the query and connection from the constructor
                SQLiteCommand cmd = new SQLiteCommand(query, connection);

                if (ServerConnected == true)
                {
                    //Execute command
                    cmd.ExecuteNonQuery();
                }
                else
                    SaveQueries("InstantaneousValue", query);
            }
            catch (Exception)
            {

                throw;
            }


        }
        public void callAllEventsFunctions(List<Insert_Evnent_CL_Counter> List_Records, string category)
        {
            string MSN_DBconnect = List_Records[0].msn.ToString();
            //storing session ID for a specific meter
            if (save_session_info(List_Records[0].msn.ToString(), "E") == false)
            {
                save_Info_To_meterList(MSN_DBconnect);
            }
            //checking for session ID
            getLast_session_ID(MSN_DBconnect);
            #region inserting data according to category
            if (category == "L")
            {
                foreach (var item in List_Records)
                {
                    Insert_Events(item, "L");
                }
            }
            else if (category == "C")
            {
                foreach (var item in List_Records)
                {
                    Insert_Events(item, "C");
                }
            }

            #endregion
            update_session_info();
            CloseConnection();
        }
        public bool SaveEventsToDataBase(List<Insert_Evnent_CL_Counter> List_Records, string category)
        {
            try
            {
                establishConnection();
                if (this.OpenConnection() == -1)
                {
                    //checkRemainingQueries();
                    callAllEventsFunctions(List_Records, category);
                    return true;
                }
                //if server is not connected function are called only for saving queries to a txt file
                else if (ServerConnected == false)
                {
                    f = new FileStream("Events.txt", FileMode.Append);
                    s = new StreamWriter(f);
                    s.WriteLine("{0}", "*-*-*-*-");
                    callAllEventsFunctions(List_Records, category);
                    s.Close();
                    f.Close();
                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Terrification+++++
        public bool save_TerrificationToDatabase(Param_ActivityCalendar Arg, string MSN)
        {
            meterSerialNumber = MSN;
            Calendar_Obj = Arg;
            try
            {
                establishConnection();
                if (this.OpenConnection() == -1)
                {
                    //checkRemainingQueries();
                    callAllTerrificationFunctions(Calendar_Obj, MSN);
                    return true;
                }
                //if server is not connected function are called only for saving queries to a txt file
                else if (ServerConnected == false)
                {
                    f = new FileStream("Terrification.txt", FileMode.Append);
                    s = new StreamWriter(f);
                    s.WriteLine("{0}", "*-*-*-*-");
                    //callAllTerrificationFunctions(Arg);
                    s.Close();
                    f.Close();
                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }

        }
        public void callAllTerrificationFunctions(Param_ActivityCalendar Arg, string MSN)
        {
            //calendar info
            saveCalendar(Calendar_Obj.CalendarName, SeasonProfile.MAX_Season_Profiles, TimeSlot.MAX_TimeSlot, MSN);
            //save day Profile
            saveDayProfile(Calendar_Obj.ParamDayProfile);
            //season profile
            saveSeasonProfile(Calendar_Obj.ParamSeasonProfile, MSN);
            //special days
            saveSpecialDays(Calendar_Obj.ParamSpecialDay, MSN);
            //update calander ID in meter table
            update_Meter_CalendarID(MSN);
        }
        public void saveCalendar(string calName, byte max_Season, ushort max_TimeSlots, string MSN)
        {
            try
            {
                //calName = "MTICALENDAR";
                string temp_Cal_Name = calName + "/" + MSN;
                string query;
                query = "INSERT INTO calendar_groups (cal_name, [max_seasons], [max_slots], cal_comment) VALUES  ('" + temp_Cal_Name + "', '" + max_Season + "','" + max_TimeSlots + "', 'No comment')";

                //create command and assign the query and connection from the constructor
                SQLiteCommand cmd = new SQLiteCommand(query, connection);

                if (ServerConnected == true)
                {
                    //Execute command
                    cmd.ExecuteNonQuery();
                }
                //else
                // SaveQueries("CalendarInfo", query);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void saveSeasonProfile(Param_SeasonProfile season, string MSN)
        {
            try
            {
                ServerConnected = true;
                foreach (var item in season)
                {

                    //item.Week_Profile.Day_Profile_FRI.Day_Profile_ID
                    byte[] name = Encoding.ASCII.GetBytes(item.Profile_Name);
                    string str = Convert.ToString(name[0]);
                    string temp_spName = "'Season " + str + "/" + MSN + "'";
                    string temp_Day = "'" + item.Start_Date.DayOfMonth.ToString() + "'";
                    string temp_Month = "'" + item.Start_Date.Month.ToString() + "'";
                    //Last entered calendar id reading

                    string temp_Cal_id = lastCalendarID();
                    string query = "";
                    query = "INSERT INTO season_profile ([cal_id], sp_name, [day], [month]) VALUES  ('" + temp_Cal_id + "'," + temp_spName + ", " + temp_Day + "," + temp_Month + ")";

                    //create command and assign the query and connection from the constructor
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);

                    if (ServerConnected == true)
                    {
                        //Execute command
                        cmd.ExecuteNonQuery();
                        //week profile
                        saveWeekProfile(item.Week_Profile, MSN);
                    }
                    //else
                    //    SaveQueries("SeasonProfile", query);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public string lastCalendarID()
        {
            SQLiteDataReader reader_lastCalendarID = null;
            string temp_C_id = "";
            try
            {
                string queryForCid = "SELECT MAX([cal_id]) FROM calendar_groups WHERE [cal_name] LIKE '%" + meterSerialNumber + "'";
                SQLiteCommand cmd = new SQLiteCommand(queryForCid, connection);
                if (ServerConnected == true)
                {
                    reader_lastCalendarID = cmd.ExecuteReader();

                    if (reader_lastCalendarID.Read())
                    {
                        temp_C_id = reader_lastCalendarID["MAX(cal_id)"].ToString();
                    }
                }
                //else
                //    SaveQueries("LastSeasonID", queryForSPid);
            }
            catch (Exception error)
            {
               // MessageBox.Show(error.Message + "\ngetSeasonID");
                reader_lastCalendarID.Close();
                return null;
            }
            reader_lastCalendarID.Close();
            return temp_C_id;
        }
        public void saveWeekProfile(WeekProfile item, string MSN)
        {
            try
            {
                byte[] name = Encoding.ASCII.GetBytes(item.Week_Profile_Name);
                string str = Convert.ToString(name[0]);

                string temp_wpName = "'Week " + str + "/" + MSN + "'";

                string temp_Mon = "'" + item.Day_Profile_MON.Day_Profile_ID.ToString() + "'";
                //saveDayProfile(item.Day_Profile_MON.Day_Profile_ID);

                string temp_Tue = "'" + item.Day_Profile_TUE.Day_Profile_ID.ToString() + "'";
                //saveDayProfile(item.Day_Profile_TUE.Day_Profile_ID);

                string temp_Wed = "'" + item.Day_Profile_WED.Day_Profile_ID.ToString() + "'";
                //saveDayProfile(item.Day_Profile_WED.Day_Profile_ID);

                string temp_Thur = "'" + item.Day_Profile_THRU.Day_Profile_ID.ToString() + "'";
                //saveDayProfile(item.Day_Profile_THRU.Day_Profile_ID);

                string temp_Fri = "'" + item.Day_Profile_FRI.Day_Profile_ID.ToString() + "'";
                //saveDayProfile(item.Day_Profile_FRI.Day_Profile_ID);

                string temp_Sat = "'" + item.Day_Profile_SAT.Day_Profile_ID.ToString() + "'";
                //saveDayProfile(item.Day_Profile_SAT.Day_Profile_ID);

                string temp_Sun = "'" + item.Day_Profile_SUN.Day_Profile_ID.ToString() + "'";
                //saveDayProfile(item.Day_Profile_SUN.Day_Profile_ID);

                //Last season profile id reading from season profile table
                string temp_Season_id = lastSeasonID();

                string query = "";
                query = "INSERT INTO week_profile ([sp_id], [wp_name], [monday], [tuesday], [wednesday], [thursday], [friday], [saturday], [sunday]) VALUES  ('" + temp_Season_id + "'," + temp_wpName + ", " + temp_Mon + "," + temp_Tue + ", " + temp_Wed + "," + temp_Thur + ", " + temp_Fri + "," + temp_Sat + "," + temp_Sun + ")";

                //create command and assign the query and connection from the constructor
                SQLiteCommand cmd = new SQLiteCommand(query, connection);

                if (ServerConnected == true)
                {
                    //Execute command
                    cmd.ExecuteNonQuery();
                }
                //else
                //    SaveQueries("WeekProfile", query);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string lastSeasonID()
        {
            SQLiteDataReader reader_lastSeasonID = null;
            string temp_sp_id = "";
            try
            {
                string queryForSPid = "SELECT MAX(sp_id) FROM season_profile WHERE sp_name LIKE '%" + meterSerialNumber + "'";
                SQLiteCommand cmd = new SQLiteCommand(queryForSPid, connection);
                if (ServerConnected == true)
                {
                    reader_lastSeasonID = cmd.ExecuteReader();

                    if (reader_lastSeasonID.Read())
                    {
                        temp_sp_id = reader_lastSeasonID["MAX(sp_id)"].ToString();
                    }
                }
                //else
                //    SaveQueries("LastSeasonID", queryForSPid);
            }
            catch (Exception error)
            {
                //MessageBox.Show(error.Message + "\ngetSeasonID");
                reader_lastSeasonID.Close();
                return null;
            }
            reader_lastSeasonID.Close();
            return temp_sp_id;
        }
        public void saveDayProfile(Param_DayProfile dayProfile)
        {

            //day profile table
            foreach (var dayItem in dayProfile)
            {

                string dp_Name = "DayProfile " + dayItem.Day_Profile_ID + "/" + meterSerialNumber;
                string query_DayProfile;
                query_DayProfile = "INSERT INTO day_profile (dp_name) VALUES  ('" + dp_Name + "' )";
                //create command and assign the query and connection from the constructor
                SQLiteCommand cmd = new SQLiteCommand(query_DayProfile, connection);

                if (ServerConnected == true)
                {
                    //Execute command
                    cmd.ExecuteNonQuery();
                }
                //else
                //    SaveQueries("DayProfile", query_DayProfile);

                //time slots
                //read last day profile id from database
                string temp_DP_id = getLast_DayProfileID();


                DayProfile dp = Calendar_Obj.ParamDayProfile.GetDayProfile(dayItem.Day_Profile_ID);
                int TimeSliceCount = 0;
                List<TimeSpan> startTimeOfSlots = new List<TimeSpan>();
                foreach (var sitem in dp)
                {
                    startTimeOfSlots.Add(sitem.StartTime);
                    TimeSliceCount++;
                }

                Tarrif_ScriptSelector tarriffSelector = new Tarrif_ScriptSelector();
                String tempTarriff = "";
                string colorOfTarriff = "";
                for (int i = 0; i < TimeSliceCount; i++)
                {

                    tarriffSelector = dp.GetDaySchedule((ushort)(i + 1)).ScriptSelector;
                    if (tarriffSelector == Tarrif_ScriptSelector.T1)
                    {
                        tempTarriff = "T1";
                        colorOfTarriff = "Red";
                    }
                    else if (tarriffSelector == Tarrif_ScriptSelector.T2)
                    {
                        tempTarriff = "T2";
                        colorOfTarriff = "Green";
                    }
                    else if (tarriffSelector == Tarrif_ScriptSelector.T3)
                    {
                        tempTarriff = "T3";
                        colorOfTarriff = "Blue";
                    }
                    else if (tarriffSelector == Tarrif_ScriptSelector.T4)
                    {
                        tempTarriff = "T4";
                        colorOfTarriff = "Yellow";
                    }

                    //query for DB
                    string query_Tariff = null;
                    query_Tariff = "INSERT INTO tariff (dp_id, tariff_name, tariff_color, starting_time) VALUES  ('" + temp_DP_id + "',' " + tempTarriff + "','" + colorOfTarriff + "','" + startTimeOfSlots[i] + "')";

                    //create command and assign the query and connection from the constructor
                    SQLiteCommand cmd1 = new SQLiteCommand(query_Tariff, connection);

                    if (ServerConnected == true)
                    {
                        //Execute command
                        cmd1.ExecuteNonQuery();
                    }
                    //else
                    //{
                    //    SaveQueries("TimeSlot", query_TimeSlots);
                    //    SaveQueries("Tariff", query_Tariff);
                    //}
                }
            }
        }
        public string getLast_DayProfileID()
        {
            SQLiteDataReader reader_last_DP_ID = null;
            string temp_DP_id = "";
            try
            {
                string queryForDPid = "SELECT MAX(dp_id) FROM day_profile WHERE dp_name LIKE '%" + meterSerialNumber + "'";
                SQLiteCommand cmd = new SQLiteCommand(queryForDPid, connection);
                if (ServerConnected == true)
                {
                    reader_last_DP_ID = cmd.ExecuteReader();

                    if (reader_last_DP_ID.Read())
                    {
                        temp_DP_id = reader_last_DP_ID["MAX(dp_id)"].ToString();
                    }
                }
                //else
                //    SaveQueries("LastDayProfileID", queryForDPid);
            }
            catch (Exception error)
            {
               // MessageBox.Show(error.Message + "\ngetDayProfileID");
                reader_last_DP_ID.Close();
                return null;
            }
            reader_last_DP_ID.Close();
            return temp_DP_id;
        }
        public void saveSpecialDays(Param_SpecialDay SD, string MSN)
        {

            foreach (var item in SD)
            {
                uint sdID = item.SpecialDayID;
                string sdName = "Special Day " + sdID + "/" + MSN;
                int dayProfileID = item.DayProfile.Day_Profile_ID;
                int year = item.StartDate.Year;
                int month = item.StartDate.Month;
                int day = item.StartDate.DayOfMonth;

                //query for DB
                string query_SP = null;
                query_SP = "INSERT INTO special_days (dp_id, sd_name, [day], [month], [year]) VALUES  ('" + dayProfileID + "', '" + sdName + "','" + day + "','" + month + "','" + year + "')";

                //create command and assign the query and connection from the constructor
                SQLiteCommand cmd = new SQLiteCommand(query_SP, connection);

                if (ServerConnected == true)
                {
                    //Execute command
                    cmd.ExecuteNonQuery();
                }
                //else
                //    SaveQueries("SpecialDay", query_SP);
            }
        }
        public void update_Meter_CalendarID(string MSN)
        {

            try
            {
                string temp_CalID = lastCalendarID();
                string queryForMeter_CalID_update = "UPDATE [meter] SET cal_id='" + temp_CalID + "' WHERE [msn]='" + MSN + "'";
                SQLiteCommand cmd = new SQLiteCommand(queryForMeter_CalID_update, connection);
                if (ServerConnected == true)
                {
                    //executing query
                    cmd.ExecuteNonQuery();
                }
                else
                    SaveQueries("updateMeterCalID", queryForMeter_CalID_update);


            }
            catch (Exception error)
            {
             //   MessageBox.Show(error.Message);
            }
        }
        //other common functions
        public bool save_session_info(string MSN, string category)
        {

            string temp_MSN = MSN;
            //current date time
            DateTime now = DateTime.Now;
            //changing formate for mysql
            string formate = "yyyy-MM-dd HH:mm:ss";
            string startTime = now.ToString(formate);
            try
            {
                //query
                string queryForSession = "INSERT INTO [session_inf]o ([msn], cat_id, [start_time]) VALUES('" + temp_MSN + "', '" + category + "', '" + startTime + "' )";
                //assigning command
                SQLiteCommand cmd = new SQLiteCommand(queryForSession, connection);
                if (ServerConnected == true)
                {
                    //executing query
                    cmd.ExecuteNonQuery();
                }
                else
                    SaveQueries("Session", queryForSession);
            }
            catch (SQLiteException ex)
            {
                return false;
                //When handling errors, you can your application's response based on the error number.
                //The two most common error numbers when connecting are as follows:
                //1452: Forieng key error(msn no exist in meter list).
                //switch (ex.ErrorCode)
                //{
                //    case 1452:
                //        MessageBox.Show("Meter serial number do not exist in database");
                //        break;
                //    default:
                //        MessageBox.Show(ex.Message);
                //        break;
                //}
                //return false;
            }
            //return 0;
            return true;
        }
        
        public void getLast_session_ID(string MSN)
        {
            SQLiteDataReader reader_loadProfile = null;
            try
            {
                string queryForSessionID = "SELECT MAX([id]) as pp FROM [session] WHERE msn = '" + MSN + "'";
                SQLiteCommand cmd = new SQLiteCommand(queryForSessionID, connection);
                if (ServerConnected == true)
                {
                    reader_loadProfile = cmd.ExecuteReader();

                    if (reader_loadProfile.Read())
                    {
                        Session_ID = reader_loadProfile["pp"].ToString();
                    }
                }
                else
                    SaveQueries("LastSessionID", queryForSessionID);
            }
            catch (Exception error)
            {
               // MessageBox.Show(error.Message + "\ngetSessionID");
            }
            finally
            {
                if (ServerConnected == true)
                    reader_loadProfile.Close();
            }
        }
        public void update_session_info()
        {
            try
            {
                //current date time
                DateTime now = DateTime.Now;
                //changing formate for mysql
                string formate = "yyyy-MM-dd HH:mm:ss";
                string endTime = now.ToString(formate);
                string queryForSession_info_update = "UPDATE [session_info] SET [end_time]='" + endTime + "',[status]='1' WHERE [session_id]='" + Session_ID + "'";
                SQLiteCommand cmd = new SQLiteCommand(queryForSession_info_update, connection);
                if (ServerConnected == true)
                {
                    //executing query
                    cmd.ExecuteNonQuery();
                }
                else
                    SaveQueries("updateSessionInfo", queryForSession_info_update);
            }
            catch (Exception error)
            {
                //MessageBox.Show(error.Message + "\ngetSessionID");
            }

        }
        public void save_Info_To_meterList(string MSN)
        {
            try
            {
                string queryForMeterList = "INSERT INTO meter (msn, ref_no) VALUES('" + MSN + "','" + MSN + "')";
                SQLiteCommand cmd = new SQLiteCommand(queryForMeterList, connection);
                if (ServerConnected == true)
                {
                    //executing query
                    cmd.ExecuteNonQuery();
                }
                else
                    SaveQueries("addMeter", queryForMeterList);
            }
            catch (Exception error)
            {
                //MessageBox.Show(error.Message + "\nSaving to Meter List");
            }
        }
        public void update_maxCounter(int MAX, string catagory, string MSN)
        {
            try
            {
                string queryForUpdateMaxCounter = "UPDATE counter_detector SET [max_count]='" + MAX + "' WHERE cat_id='" + catagory + "' AND msn='" + MSN + "'";
                SQLiteCommand cmd = new SQLiteCommand(queryForUpdateMaxCounter, connection);
                if (ServerConnected == true)
                {
                    //Execute command
                    cmd.ExecuteNonQuery();
                }
                else
                    SaveQueries("updateMaxCounter", queryForUpdateMaxCounter);
            }
            catch (Exception error)
            {
               // MessageBox.Show(error.Message + "\nUpdate Max counter");
            }
        }
    }
}
