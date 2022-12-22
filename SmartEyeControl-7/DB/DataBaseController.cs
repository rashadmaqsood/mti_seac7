using DLMS.Comm;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.Param;
using SharedCode.Others;
using SmartEyeControl_7.comm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace SmartEyeControl_7.DB
{
    public class DataBaseController
    {
        string date_format = "yyyy-MM-dd HH:mm:ss";
        string SqliteCurrentDateTime = "datetime('now')";
        //string AccessCurrentDateTime = "NOW()";
        string ConnectionString = $" Data Source = {DBConnect.ConnectionString}";
        private int session_id = 0;

        public DataBaseController()
        {
        }

        //v4.8.15
        public User verifyUserAndGetDetails(string userName, string password)
        {
            User obj_user;
            try
            {
                string hashPassword = string.Empty;
                using (SHA256 mySHA256 = SHA256Managed.Create())
                {
                    hashPassword = Commons_DB.ArrayToHexString(mySHA256.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password)), true);
                }
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    string cmd = String.Format("Select * from [user_new] where isActive <> 0 and user_name='{0}' and user_password='{1}'", userName, hashPassword);
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        connection.Open();
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                obj_user = new User();
                                obj_user.userName = (reader["user_name"]).ToString();
                                obj_user.userID = Convert.ToInt32(reader["user_ID"]);
                                //obj_user.rights.rightsID = Convert.ToUInt16(reader["user_rights_id"]);
                                byte userType = Convert.ToByte(reader["user_type_id"]);
                                obj_user.userTypeID = (UserTypeID)(userType);

                                cmd = String.Format("Select * from [user_rights_model] where user_id={0}", obj_user.userID);
                                using (SQLiteCommand command2 = new SQLiteCommand(cmd, connection))
                                {
                                    using (SQLiteDataReader reader2 = command2.ExecuteReader())
                                    {
                                        if (reader.HasRows)
                                        {
                                            while (reader2.Read())
                                            {
                                                int meter_type_infoId = Convert.ToInt32(reader2["meter_type_info_id"]);
                                                int accessRightsId = Convert.ToInt32(reader2["access_rights_id"]);
                                                obj_user.RightsID_Model.Add(meter_type_infoId, accessRightsId);

                                                ApplicationRight modelRight = SelectAccessRights(accessRightsId, meter_type_infoId, connection);
                                                obj_user.AccessRights_Model.Add(modelRight);
                                            }
                                        }
                                    }
                                }
                                //obj_user.CurrentAccessRights = new ApplicationRight();
                                return obj_user;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error exec verifyUserAndGetDetails " + ex.Message, ex);
            }
            finally
            {
            }
        }

        public bool addNewUser(User obj)
        {
            try
            {
                string cmd = string.Empty;

                cmd = $@"INSERT INTO [user_new] 
                (isActive, user_type_id, user_name, user_password, father_Name, address, phone_1, phone_2, mobile_No, fax_No, nID, employee_Code, creationDate) 
                VALUES (?,?,?,?,?,?,?,?,?,?,?,?,{SqliteCurrentDateTime})";
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.AddWithValue("@isActive", Convert.ToByte(obj.isActive));
                        command.Parameters.AddWithValue("@user_type_id", (int)(obj.userTypeID));
                        command.Parameters.AddWithValue("@user_name", obj.userName);

                        using (SHA256 mySHA256 = SHA256Managed.Create())
                        {
                            obj.userPassword = Commons_DB.ArrayToHexString(mySHA256.ComputeHash(ASCIIEncoding.ASCII.GetBytes(obj.userPassword)), true);
                            command.Parameters.AddWithValue("@user_password", obj.userPassword);
                        }
                        command.Parameters.AddWithValue("@father_Name", obj.fatherName);
                        command.Parameters.AddWithValue("@address", obj.address);
                        command.Parameters.AddWithValue("@phone_1", obj.phone_1);
                        command.Parameters.AddWithValue("@phone_2", obj.phone_2);
                        command.Parameters.AddWithValue("@mobile_No", obj.mobile_no);
                        command.Parameters.AddWithValue("@fax_No", obj.fax_no);
                        command.Parameters.AddWithValue("@nID", obj.nid_no);
                        command.Parameters.AddWithValue("@employee_Code", obj.employee_code);
                        connection.Open();

                        if (command.ExecuteNonQuery() > 0)
                        {
                            User addedUser = this.getUser(obj.userName);

                            if (addedUser != null)
                            {
                                foreach (var item in obj.RightsID_Model)
                                {
                                    command.Parameters.Clear();

                                    cmd = @"INSERT INTO [user_rights_model] (user_id, meter_type_info_id, access_rights_id)
                                             VALUES (?,?,?)";
                                    command.CommandText = cmd;
                                    command.Parameters.AddWithValue("@user_id", (int)(addedUser.userID));
                                    command.Parameters.AddWithValue("@meter_type_info_id", (int)(item.Key));
                                    command.Parameters.AddWithValue("@access_rights_id", (int)(item.Value));
                                    int y = command.ExecuteNonQuery();

                                    if (y > 0) { }
                                    else return false;
                                }
                            }
                            else
                                return false;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error exec addNewUser " + ex.Message, ex);
            }
        }

        public bool updateExistUser(User obj)
        {
            try
            {
                string cmd = string.Empty;

                using (SHA256 mySHA256 = SHA256Managed.Create())
                {
                    obj.userPassword = Commons_DB.ArrayToHexString(mySHA256.ComputeHash(ASCIIEncoding.ASCII.GetBytes(obj.userPassword)), true);
                }

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        cmd = string.Format("UPDATE [user_new] SET [isActive] = {0} ,[user_password] = '{1}',[father_Name] = '{2}',[address] = '{3}',[phone_1] = '{4}', " +
                   "[phone_2] = '{5}',[mobile_No] = '{6}', [fax_No] = '{7}',[nID] = '{8}',[employee_Code] = '{9}' WHERE [user_ID] = {10} and [user_name] = '{11}';",
                    obj.isActive, obj.userPassword, obj.fatherName, obj.address, obj.phone_1, obj.phone_2, obj.mobile_no, obj.fax_no,
                    obj.nid_no, obj.employee_code, obj.userID, obj.userName);
                        command.CommandText = cmd;

                        connection.Open();
                        if (command.ExecuteNonQuery() > 0)
                        {
                            command.Parameters.Clear();

                            cmd = @"DELETE FROM [user_rights_model] WHERE user_id=?";
                            command.CommandText = cmd;
                            command.Parameters.AddWithValue("@user_id", (int)(obj.userID));
                            int rowsEffected = command.ExecuteNonQuery();

                            foreach (var item in obj.RightsID_Model)
                            {
                                cmd = @"INSERT INTO [user_rights_model] (user_id, meter_type_info_id, access_rights_id)
                            VALUES (?,?,?)";
                                command.CommandText = cmd;
                                rowsEffected = 0;
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("@user_id", (int)(obj.userID));
                                command.Parameters.AddWithValue("@meter_type_info_id", (int)(item.Key));
                                command.Parameters.AddWithValue("@access_rights_id", (int)(item.Value));

                                rowsEffected = command.ExecuteNonQuery();

                                if (rowsEffected > 0) { }
                                else return false;

                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error exec updateExistUser " + ex.Message, ex);
            }
        }

        public List<string> getAllUsers()
        {
            try
            {
                List<string> result = new List<string>();
                string cmd = "Select user_name from [user_new]";

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        connection.Open();

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    result.Add(reader["user_name"].ToString());
                                }

                                return result;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error exec getAllUsers " + ex.Message, ex);
            }
        }

        public DataTable selectAllUsers()
        {
            try
            {
                DataTable UsersInfo = new DataTable();
                string cmd = "Select * from [user_new]";
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        connection.Open();

                        Load(UsersInfo, command);
                        return UsersInfo;
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error exec getAllUsers " + ex.Message, ex);
            }
        }

        public bool deleteuser(User objUser)
        {
            SQLiteTransaction transaction = null;
            try
            {
                bool result = false;
                string cmd = String.Format("Delete from [user_rights_model] where user_id={0}", objUser.userID);

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                        command.Transaction = transaction;
                        connection.Open();

                        int c = command.ExecuteNonQuery();

                        if (c > 0)
                        {
                            cmd = String.Format("Delete from [user_new] where user_name='{0}'", objUser.userName);
                            command.CommandText = cmd;
                            c = command.ExecuteNonQuery();

                            if (c > 0)
                            {
                                result = true;
                                transaction.Commit();
                            }
                            else
                            {
                                transaction.Rollback();
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error exec delete user " + ex.Message, ex);
            }
        }

        public void Load(DataTable DataSet, SQLiteCommand SelectSQLCmd)
        {
            try
            {
                SQLiteDataAdapter DataAdapeter =
                    new SQLiteDataAdapter((SQLiteCommand)SelectSQLCmd);
                DataAdapeter.Fill(DataSet);
                DataAdapeter.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading data from data source", ex);
            }
        }

        public User getUser(string userName)
        {
            User objToReturn = null;

            try
            {
                string cmd = String.Format("Select * from [user_new] where user_name='{0}'", userName);
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        connection.Open();

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                objToReturn = new User();
                                reader.Read();
                                byte userType = Convert.ToByte(reader["user_type_id"]);
                                objToReturn.userTypeID = (UserTypeID)(userType);

                                objToReturn.userID = Convert.ToInt32(reader["user_ID"]);
                                objToReturn.isActive = Convert.ToBoolean(reader["isActive"]);
                                objToReturn.userName = reader["user_name"].ToString();
                                objToReturn.userPassword = reader["user_password"].ToString();
                                objToReturn.fatherName = reader["father_Name"].ToString();
                                objToReturn.address = reader["address"].ToString();
                                objToReturn.phone_1 = reader["phone_1"].ToString();
                                objToReturn.phone_2 = reader["phone_2"].ToString();
                                objToReturn.mobile_no = reader["mobile_No"].ToString();
                                objToReturn.fax_no = reader["fax_No"].ToString();
                                objToReturn.nid_no = reader["nID"].ToString();
                                objToReturn.employee_code = reader["employee_Code"].ToString();
                                if (reader["creationDate"] != DBNull.Value)
                                    objToReturn.creation_date = Convert.ToDateTime(reader["creationDate"]);

                                cmd = String.Format("Select * from [user_rights_model] where user_id={0}", objToReturn.userID);
                                using (SQLiteCommand command2 = new SQLiteCommand(cmd, connection))
                                {
                                    using (SQLiteDataReader reader2 = command2.ExecuteReader())
                                    {
                                        if (reader2.HasRows)
                                        {
                                            while (reader2.Read())
                                            {
                                                int meter_type_infoId = Convert.ToInt32(reader2["meter_type_info_id"]);
                                                int accessRightsId = Convert.ToInt32(reader2["access_rights_id"]);
                                                objToReturn.RightsID_Model.Add(meter_type_infoId, accessRightsId);

                                                ApplicationRight modelRight = SelectAccessRights(accessRightsId, meter_type_infoId, connection);
                                                objToReturn.AccessRights_Model.Add(modelRight);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                return null;
                            }

                        }
                    }
                }
                return objToReturn;
            }
            catch (Exception ex)
            {
                throw new Exception("Error exec getUser " + ex.Message, ex);
            }
        }

        public int createSession(string msn, string userID)
        {
            try
            {
                string cmd = $"INSERT INTO [session] (msn,date_time, user_id) VALUES ('{msn}',{SqliteCurrentDateTime},'{userID}')";

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {

                        connection.Open();
                        command.ExecuteNonQuery();

                        command.CommandText = "Select last_insert_rowid()"; //@@Identity";
                        session_id = Convert.ToInt32(command.ExecuteScalar());
                        return session_id;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error exec createSession " + ex.Message, ex);
            }
        }

        #region Access Rights

        public bool CreateNewAccessRights(string idendifier, string role, ApplicationRight rights)
        {
            try
            {
                string cmd = string.Format("insert into [access_rights](identifier,role,rights) values(?,?,?)");
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.Add(new SQLiteParameter("@identifier", idendifier) { DbType = DbType.String });
                        command.Parameters.Add(new SQLiteParameter("@role", role) { DbType = DbType.String });
                        command.Parameters.Add(new SQLiteParameter("@rights", ApplicationRight.GetBinaryObject(rights)) { DbType = DbType.Binary });
                        connection.Open();

                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateAccessRights(int id, string idendifier, string role, ApplicationRight rights)
        {
            try
            {
                string cmd = string.Format("update [access_rights] set [identifier]='{0}', role= '{1}',[rights]=? where [id] = {2} ", idendifier, role, id);

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.Add(new SQLiteParameter("@rights", ApplicationRight.GetBinaryObject(rights)) { DbType = DbType.Binary });
                        connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteAccessRights(int id)
        {
            try
            {
                string cmd = string.Format("delete from [access_rights] where id=?");
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.Add(new SQLiteParameter("@id", id) { DbType = DbType.UInt64 });
                        connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataTable SelectAccessRights()
        {
            try
            {
                string cmd = string.Format("select * from [access_rights]");
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        connection.Open();
                        var tbl = new DataTable();
                        tbl.Load(command.ExecuteReader());
                        return tbl;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ApplicationRight SelectAccessRights(int id)
        {
            ApplicationRight RightObj = null;
            try
            {
                string cmd = string.Format("select * from [access_rights] where [id] = ?");
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.Add(new SQLiteParameter("@id", id) { DbType = DbType.UInt64 });
                        connection.Open();
                        var tbl = new DataTable();
                        tbl.Load(command.ExecuteReader());
                        var bin = (byte[])tbl.Rows[0]["rights"];
                        RightObj = ApplicationRight.GetObjectFromBytes(bin);
                        if (RightObj != null)
                            RightObj.RightsId = id;
                        return RightObj;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public ApplicationRight SelectAccessRights(int id, int meterInfoId, SQLiteConnection connection)
        {
            ApplicationRight RightObj = null;
            try
            {
                string cmd = string.Format("select * from [access_rights] where [id] = ?");
                using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                {
                    command.Parameters.Add(new SQLiteParameter("@id", id) { DbType = DbType.UInt64 });
                    //connection.Open();
                    var tbl = new DataTable();
                    tbl.Load(command.ExecuteReader());
                    var bin = (byte[])tbl.Rows[0]["rights"];
                    RightObj = ApplicationRight.GetObjectFromBytes(bin);
                    if (RightObj != null)
                        RightObj.RightsId = id;

                    command.Parameters.Clear();
                    command.CommandText = String.Format("Select [Model] from [meter_type_info] where [id]={0}", meterInfoId);
                    var tbl2 = new DataTable();
                    tbl2.Load(command.ExecuteReader());
                    RightObj.MeterModel = tbl2.Rows[0]["Model"].ToString();
                    RightObj.MeterModelId = meterInfoId;

                    return RightObj;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void LoadApplicationAccessRights(User CurrentUser)
        {
            try
            {
                //CurrentUser.AccessRights_R283 = SelectAccessRights(CurrentUser.RightsID_R283);
                //CurrentUser.AccessRights_Act34G = SelectAccessRights(CurrentUser.RightsID_ACT34G);
                //CurrentUser.AccessRights_R411 = SelectAccessRights(CurrentUser.RightsID_R411);
                //CurrentUser.AccessRights_R421 = SelectAccessRights(CurrentUser.RightsID_R421);
                //CurrentUser.AccessRights_T421 = SelectAccessRights(CurrentUser.RightsID_T421);
                ////R326 Access Rights
                //CurrentUser.CurrentAccessRights = CurrentUser.AccessRights_Act34G;
            }
            catch { }
        }

        #endregion

        #region BILLING

        public bool saveCumulativeBillingData(Cumulative_billing_data Data)
        {
            try
            {
                string cmd = $"INSERT INTO [cumm_billing_data] VALUES(?,{SqliteCurrentDateTime}, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        SQLiteParameter T = null;

                        //command.Prepare();
                        command.Parameters.Add(new SQLiteParameter("@MSN", Data.msn));

                        T = new SQLiteParameter("@MeterDateTime", Data.date);
                        T.DbType = DbType.DateTime;
                        command.Parameters.Add(T);

                        command.Parameters.Add(new SQLiteParameter("@ActiveEnergy_T1", Data.activeEnergy_T1));
                        command.Parameters.Add(new SQLiteParameter("@ActiveEnergy_T2", Data.activeEnergy_T2));
                        command.Parameters.Add(new SQLiteParameter("@ActiveEnergy_T3", Data.activeEnergy_T3));
                        command.Parameters.Add(new SQLiteParameter("@ActiveEnergy_T4", Data.activeEnergy_T4));
                        command.Parameters.Add(new SQLiteParameter("@ActiveEnergy_TL", Data.activeEnergy_TL));

                        command.Parameters.Add(new SQLiteParameter("@ReactiveEnergy_T1", Data.reactiveEnergy_T1));
                        command.Parameters.Add(new SQLiteParameter("@ReactiveEnergy_T2", Data.reactiveEnergy_T2));
                        command.Parameters.Add(new SQLiteParameter("@ReactiveEnergy_T3", Data.reactiveEnergy_T3));
                        command.Parameters.Add(new SQLiteParameter("@ReactiveEnergy_T4", Data.reactiveEnergy_T4));
                        command.Parameters.Add(new SQLiteParameter("@ReactiveEnergy_TL", Data.reactiveEnergy_TL));

                        command.Parameters.Add(new SQLiteParameter("@ActiveMDI_T1", Data.activeMDI_T1));
                        command.Parameters.Add(new SQLiteParameter("@ActiveMDI_T2", Data.activeMDI_T2));
                        command.Parameters.Add(new SQLiteParameter("@ActiveMDI_T3", Data.activeMDI_T3));
                        command.Parameters.Add(new SQLiteParameter("@ActiveMDI_T4", Data.activeMDI_T4));
                        command.Parameters.Add(new SQLiteParameter("@ActiveMDI_TL", Data.activeMDI_TL));
                        connection.Open();

                        int x = command.ExecuteNonQuery();
                        if (x > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error exec saveCumulativeBillingData " + ex.Message, ex);
            }
        }

        public bool saveMonthlyBillingData(Monthly_Billing_data MB_data)
        {
            try
            {
                string date_time = DateTime.Now.ToString(date_format);
                string cmd = "INSERT INTO [monthly_billing_data] VALUES(?,?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        connection.Open();
                        foreach (var Data in MB_data.monthly_billing_data)
                        {
                            ///Clear Previous Parameters
                            command.Parameters.Clear();
                            SQLiteParameter T = null;

                            command.Parameters.Add(new SQLiteParameter("@MSN", Data.billData_obj.msn));
                            command.Parameters.Add(new SQLiteParameter("@date_time", date_time));

                            T = new SQLiteParameter("@MeterDateTime", Data.billData_obj.date);
                            T.DbType = DbType.DateTime;
                            command.Parameters.Add(T);

                            T = new SQLiteParameter("@Counter", Data.Counter);
                            T.DbType = DbType.Int64;
                            command.Parameters.Add(T);

                            command.Parameters.Add(new SQLiteParameter("@ActiveEnergy_T1", Data.billData_obj.activeEnergy_T1));
                            command.Parameters.Add(new SQLiteParameter("@ActiveEnergy_T2", Data.billData_obj.activeEnergy_T2));
                            command.Parameters.Add(new SQLiteParameter("@ActiveEnergy_T3", Data.billData_obj.activeEnergy_T3));
                            command.Parameters.Add(new SQLiteParameter("@ActiveEnergy_T4", Data.billData_obj.activeEnergy_T4));
                            command.Parameters.Add(new SQLiteParameter("@ActiveEnergy_TL", Data.billData_obj.activeEnergy_TL));

                            command.Parameters.Add(new SQLiteParameter("@ReactiveEnergy_T1", Data.billData_obj.reactiveEnergy_T1));
                            command.Parameters.Add(new SQLiteParameter("@ReactiveEnergy_T2", Data.billData_obj.reactiveEnergy_T2));
                            command.Parameters.Add(new SQLiteParameter("@ReactiveEnergy_T3", Data.billData_obj.reactiveEnergy_T3));
                            command.Parameters.Add(new SQLiteParameter("@ReactiveEnergy_T4", Data.billData_obj.reactiveEnergy_T4));
                            command.Parameters.Add(new SQLiteParameter("@ReactiveEnergy_TL", Data.billData_obj.reactiveEnergy_TL));

                            command.Parameters.Add(new SQLiteParameter("@ActiveMDI_T1", Data.billData_obj.activeMDI_T1));
                            command.Parameters.Add(new SQLiteParameter("@ActiveMDI_T2", Data.billData_obj.activeMDI_T2));
                            command.Parameters.Add(new SQLiteParameter("@ActiveMDI_T3", Data.billData_obj.activeMDI_T3));
                            command.Parameters.Add(new SQLiteParameter("@ActiveMDI_T4", Data.billData_obj.activeMDI_T4));
                            command.Parameters.Add(new SQLiteParameter("@ActiveMDI_TL", Data.billData_obj.activeMDI_TL));

                            int x = command.ExecuteNonQuery();
                            if (x > 0)
                            {
                                // return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool saveCumulativeBillingData_SinglePhase(cumulativeBilling_SinglePhase Data)
        {
            try
            {
                string cmd = $"INSERT INTO [cumm_billing_data_singlephase] VALUES(?,{SqliteCurrentDateTime}, ?, ?, ?)";
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.Add(new SQLiteParameter("@MSN", Data.msn));
                        command.Parameters.Add(new SQLiteParameter("@MeterDateTime", DbType.DateTime).Value = Data.date);
                        command.Parameters.Add(new SQLiteParameter("@ActiveEnergy", Data.activeEnergy));
                        command.Parameters.Add(new SQLiteParameter("@ActiveMDI", Data.activeMDI));
                        connection.Open();

                        int c = command.ExecuteNonQuery();
                        if (c > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool saveMonthlyBillingData_SinglePhase(Monthly_Billing_data_SinglePhase MB_data)
        {
            try
            {
                string cmd = $"INSERT INTO [monthly_billing_data_singlephase] VALUES(?,{SqliteCurrentDateTime}, ?, ?, ?, ?)";
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        connection.Open();
                        foreach (var Data in MB_data.monthly_billing_data)
                        {
                            ///Clear Previous Parameters
                            command.Parameters.Clear();
                            command.Parameters.Add(new SQLiteParameter("@MSN", Data.billData_obj.msn));
                            command.Parameters.Add(new SQLiteParameter("@MeterDateTime", DbType.DateTime).Value = Data.billData_obj.date);
                            command.Parameters.Add(new SQLiteParameter("@Counter", DbType.UInt64).Value = Data.Counter);

                            command.Parameters.Add(new SQLiteParameter("@ActiveEnergy_T1", Data.billData_obj.activeEnergy));
                            command.Parameters.Add(new SQLiteParameter("@ActiveMDI_T1", Data.billData_obj.activeMDI));

                            int c = command.ExecuteNonQuery();
                            if (c > 0)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }

                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region LOAD PROFILE

        public bool saveLoadProfile(Load_Profile Data, long loadProfile_GroupID)
        {
            try
            {
                string cmd = "INSERT INTO [load_profile_data] (msn,meter_date_time, load_profile_group_id, channel_1_val, channel_2_val, channel_3_val, channel_4_val, counter, lp_interval) " +
                        "VALUES(?,?,?, ?, ?, ?, ?, ?, ?)";

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        connection.Open();

                        for (int i = 0; i < Data.loadData.Count; i++)
                        {
                            ///Clear Previouse Parameters
                            command.Parameters.Clear();

                            SQLiteParameter T = null;

                            command.Parameters.Add(new SQLiteParameter("@MSN", Data.MSN));

                            T = new SQLiteParameter("@MeterDateTime", Data.loadData[i].timeStamp);
                            T.DbType = DbType.DateTime;
                            command.Parameters.Add(T);

                            T = new SQLiteParameter("@GroupID", loadProfile_GroupID);
                            T.DbType = DbType.UInt64;
                            command.Parameters.Add(T);

                            command.Parameters.Add(new SQLiteParameter("@Value_Channel_1", Data.loadData[i].value[0]));
                            command.Parameters.Add(new SQLiteParameter("@Value_Channel_2", Data.loadData[i].value[1]));
                            command.Parameters.Add(new SQLiteParameter("@Value_Channel_3", Data.loadData[i].value[2]));
                            command.Parameters.Add(new SQLiteParameter("@Value_Channel_4", Data.loadData[i].value[3]));

                            T = new SQLiteParameter("@Counter", Data.loadData[i].counter);
                            T.DbType = DbType.Int64;
                            command.Parameters.Add(T);

                            T = new SQLiteParameter("@LP_Interval", Data.loadData[i].interval);
                            T.DbType = DbType.String;
                            command.Parameters.Add(T);

                            int x;
                            try
                            {
                                x = command.ExecuteNonQuery();
                            }
                            catch (Exception)
                            {

                                continue;//DUplicate Record expected
                            }
                            if (x > 0)
                            {

                            }
                            else { return false; }
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int saveLoadProfileChannels(string msn, string c1, string c2, string c3, string c4)
        {
            try
            {
                string cmd = String.Format("Insert into [loadprofilegroup] (msn,channel1,channel2,channel3,channel4) VALUES ('{0}','{1}','{2}','{3}','{4}')",
                    msn, c1, c2, c3, c4);
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        connection.Open();

                        command.ExecuteNonQuery();

                        cmd = "Select last_insert_rowid()"; //@@Identity";
                        command.CommandText = cmd; // new SQLiteCommand(cmd, connection);
                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error exec saveLoadProfileChannels " + ex.Message, ex);
            }
            finally
            {
                //connection.Close();
            }
        }

        public int getLoadProfileGroupID(string c1, string c2, string c3, string c4, string msn)
        {
            try
            {
                string cmd = String.Format("Select [id] from [loadprofilegroup] where channel1='{0}' AND channel2='{1}' AND channel3='{2}' AND channel4='{3}' AND msn='{4}'",
                    c1, c2, c3, c4, msn);

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        connection.Open();

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                return Convert.ToInt16(reader["id"]);
                            }
                            else
                            {
                                return saveLoadProfileChannels(msn, c1, c2, c3, c4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error exec getLoadProfileGroupID " + ex.Message, ex);
            }
        }

        #endregion

        #region EVENTS

        public EventData readSavedLogBook(string msn, uint startCounter, uint endCounter)
        {
            try
            {
                EventData Data = new EventData();

                string cmd = "select * from [logbook] where msn=@MSN and [counter] between @startCounter and @endCounter";

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.Clear();
                        command.Parameters.Add(new SQLiteParameter("@MSN", msn));
                        command.Parameters.Add(new SQLiteParameter("@startCounter", startCounter));
                        command.Parameters.Add(new SQLiteParameter("@endCounter", endCounter));
                        connection.Open();

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    EventItem item = new EventItem();
                                    EventInfo ei = new EventInfo(Convert.ToInt32(reader["event_code"]), string.Empty, Convert.ToInt32(reader["counter"]));
                                    item.EventInfo = ei;
                                    item.EventCounter = Convert.ToUInt32(reader["counter"]);
                                    item.EventDateTimeStamp = Convert.ToDateTime(reader["arrival_time"]);
                                    Data.EventRecords.Add(item);
                                }
                            }
                            return Data;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int readMaxLogBookCounter(string msn)
        {
            try
            {
                EventData Data = new EventData();
                string cmd = "select max([counter]) as max_count from [logbook] where msn=@MSN";

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        connection.Open();

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            ///Clear Parameters
                            command.Parameters.Clear();
                            command.Parameters.Add(new SQLiteParameter("@MSN", msn));

                            if (reader.HasRows)
                            {
                                reader.Read();
                                return Convert.ToInt32(reader["max_count"]);
                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public bool saveLogBook(EventData Data, string msn, bool IsEventTimeCompensationRequired)
        {
            try
            {
                int logBookCounter = -1;
                if (IsEventTimeCompensationRequired)
                {
                    logBookCounter = readMaxLogBookCounter(msn);
                }
                string cmd = "INSERT INTO [logbook] VALUES (?, ?, ?, ?)";
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        connection.Open();

                        SQLiteParameter T = null;
                        for (int i = 0; i < Data.EventRecords.Count; i++)
                        {
                            if (IsEventTimeCompensationRequired)
                            {
                                if (Data.EventRecords[i].EventCounter <= logBookCounter) continue;
                            }
                            ///Clear Parameters
                            command.Parameters.Clear();

                            T = new SQLiteParameter("@ArrivalTime", Data.EventRecords[i].EventDateTimeStamp);
                            T.DbType = DbType.DateTime;
                            command.Parameters.Add(T);

                            command.Parameters.Add(new SQLiteParameter("@MSN", msn));
                            command.Parameters.Add(new SQLiteParameter("@Code", Data.EventRecords[i].EventInfo.EventCode));

                            //Command.Parameters.Add(new SQLiteParameter("@Counter", Data.EventRecords[i].EventCounter));
                            T = new SQLiteParameter("@Counter", Data.EventRecords[i].EventCounter);
                            T.DbType = DbType.Int32;
                            command.Parameters.Add(T);


                            int c;
                            try
                            {
                                c = command.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {

                                throw ex; //DUPLICATION
                            }

                            if (c > 0)
                            {

                            }
                            else
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool saveAlarmStatus(string MSN, BitArray AlarmStatus)
        {
            try
            {
                string date_time = DateTime.Now.ToString(date_format);

                string cmd = string.Empty;
                cmd += String.Format("INSERT INTO [alarm_status] VALUES('{0}', '{1}'", MSN, date_time);

                for (int i = 0; i < AlarmStatus.Length; i++) //v4.8.29 i=0
                {
                    cmd += ", " + Convert.ToInt16(AlarmStatus[i]);
                }

                //v4.8.29
                int TotalEvent = Enum.GetNames(typeof(MeterEvent)).Length;
                if (AlarmStatus.Length < TotalEvent)
                {
                    for (int i = AlarmStatus.Length; i < TotalEvent; i++)
                    {
                        cmd += ", " + Convert.ToInt16("0");
                    }
                }

                cmd += ")";

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        connection.Open();
                        int c = command.ExecuteNonQuery();
                        if (c > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool save_CautionsAndFlash(string MSN, List<Param_EventsCaution> data)
        {
            try
            {
                string date_time = DateTime.Now.ToString(date_format);
                string cmd = string.Empty;

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        connection.Open();

                        foreach (var item in data)
                        {
                            command.Parameters.Clear();

                            cmd = "Insert into [event_caution_flash] VALUES ( ?,?,?,?,?,?,?,?)";
                            command.CommandText = cmd;

                            command.Parameters.AddWithValue("@msn", MSN);
                            command.Parameters.AddWithValue("@date_time", date_time);
                            command.Parameters.AddWithValue("@eventCode", item.Event_Code);
                            command.Parameters.AddWithValue("@cautionNumber", item.CautionNumber);
                            command.Parameters.AddWithValue("@flashTime", item.FlashTime);

                            if ((item.Flag >> 1) % 2 == 1)
                            {
                                command.Parameters.AddWithValue("@readCaution", 1);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@readCaution", 0);
                            }

                            if (item.Flag % 2 == 1)
                            {
                                command.Parameters.AddWithValue("@displayCaution", 1);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@displayCaution", 0);
                            }

                            if ((item.Flag >> 2) % 2 == 1)
                            {
                                command.Parameters.AddWithValue("@isFlash", 1);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@isFlash", 0);
                            }
                            //cmd = "Insert into event_caution_flash VALUES ( @msn,CURDATE(),CURTIME(),@eventCode,@cautionNumber,@readCaution,@displayCaution,@isFlash,@flashTime)";

                            int x = command.ExecuteNonQuery();

                            if (x == 0)
                            {
                                return false;
                            }

                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error exec save_CautionsAndFlash " + ex.Message, ex);
            }
        }

        public bool save_majorAlarm(string MSN, Param_MajorAlarmProfile data)
        {
            try
            {
                string date_time = DateTime.Now.ToString(date_format);
                string cmd = string.Empty;
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        connection.Open();
                        foreach (var item in data.AlarmItems)
                        {
                            command.Parameters.Clear();

                            cmd = "Insert into [major_alarms] VALUES( ? ,?, ?,?)";
                            command.CommandText = cmd;
                            command.Connection = connection;

                            command.Parameters.AddWithValue("@msn", MSN);
                            command.Parameters.AddWithValue("@date_time", date_time);
                            command.Parameters.AddWithValue("@eventCode", item.Info.EventCode);
                            command.Parameters.AddWithValue("@isMajorAlarm", item.IsMajorAlarm);

                            int c = command.ExecuteNonQuery();
                            if (c == 0) return false;
                        }

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error exec save_majorAlarm " + ex.Message, ex);
            }
        }


        public int readMaxEventCounter(string msn, int eventcode)
        {
            try
            {
                EventData Data = new EventData();
                string cmd = "select max([counter]) as max_count from [event_data] where msn=@MSN AND event_code=@EVENT_CODE";

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        ///Clear Parameters
                        command.Parameters.Clear();
                        command.Parameters.Add(new SQLiteParameter("@MSN", msn));
                        command.Parameters.Add(new SQLiteParameter("@EVENT_CODE", eventcode));
                        connection.Open();
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                return Convert.ToInt32(reader["max_count"]);
                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public EventData ReadSavedEventData(int eventCode, string msn, uint startCounter, uint endCounter)
        {
            try
            {
                EventData Data = new EventData();

                string cmd = "select * from [event_data] where msn=@MSN and event_code=@EVENT_CODE AND [counter] between @startCounter and @endCounter";
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.Clear();
                        command.Parameters.Add(new SQLiteParameter("@MSN", msn));
                        command.Parameters.Add(new SQLiteParameter("@EVENT_CODE", eventCode));
                        command.Parameters.Add(new SQLiteParameter("@startCounter", startCounter));
                        command.Parameters.Add(new SQLiteParameter("@endCounter", endCounter));
                        connection.Open();

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    EventItem item = new EventItem();
                                    EventInfo ei = new EventInfo(Convert.ToInt32(reader["event_code"]), string.Empty, Convert.ToInt32(reader["counter"]));
                                    item.EventInfo = ei;
                                    item.EventCounter = Convert.ToUInt32(reader["counter"]);
                                    item.EventDateTimeStamp = Convert.ToDateTime(reader["meter_datetime"]);
                                    Data.EventRecords.Add(item);
                                }
                            }
                            return Data;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool saveEventData(EventData Data, string msn, bool IsEventTimeCompensationRequired)
        {
            try
            {
                int eventCounter = -1;
                if (IsEventTimeCompensationRequired)
                {
                    eventCounter = readMaxEventCounter(msn, Data.EventInfo.EventCode);
                }
                string date_time = DateTime.Now.ToString(date_format);
                string cmd = "INSERT INTO [event_data] VALUES (?,?,?,?,?,?)";

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        connection.Open();

                        SQLiteParameter T = null;
                        for (int i = 0; i < Data.EventRecords.Count; i++)
                        {
                            if (IsEventTimeCompensationRequired)
                            {
                                if (Data.EventRecords[i].EventCounter <= eventCounter) continue;
                                ///Clear Parameters
                            }
                            command.Parameters.Clear();

                            command.Parameters.Add(new SQLiteParameter("@MSN", msn));
                            command.Parameters.Add(new SQLiteParameter("@date_time", date_time));


                            T = new SQLiteParameter("@ArrivalTime", Data.EventRecords[i].EventDateTimeStamp);
                            T.DbType = DbType.DateTime;
                            command.Parameters.Add(T);
                            command.Parameters.Add(new SQLiteParameter("@Code", Data.EventRecords[i].EventInfo.EventCode));

                            //Command.Parameters.Add(new SQLiteParameter("@Counter", Data.EventRecords[i].EventCounter));
                            T = new SQLiteParameter("@Counter", Data.EventRecords[i].EventCounter);
                            T.DbType = DbType.Int32;
                            command.Parameters.Add(T);

                            command.Parameters.Add(new SQLiteParameter("@detailString", Data.EventRecords[i].EventDetailStr));


                            int c;
                            try
                            {
                                c = command.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {

                                throw ex; //DUPLICATION
                            }

                            if (c == 0)
                            {
                                return false;
                            }

                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region POWER QUALITY

        public bool SavePowerQuality_SinglePhase(Instantaneous_SinglePhase Data)
        {
            try
            {
                string cmd = $"INSERT INTO [instantaneous_data_singlephase] VALUES(?,{SqliteCurrentDateTime}, ?, ?, ?, ?, ?, ?)";
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        //command.Prepare();
                        command.Parameters.Add(new SQLiteParameter("@MSN", Data.MSN));
                        command.Parameters.Add(new SQLiteParameter("@MeterDateTime", Data.dateTime.GetDateTime()) { DbType = DbType.DateTime });

                        command.Parameters.Add(new SQLiteParameter("@Current", Data.current));
                        command.Parameters.Add(new SQLiteParameter("@Voltage", Data.voltage));
                        command.Parameters.Add(new SQLiteParameter("@InstantaneousActivePower", Data.instantaneousActivePower));
                        command.Parameters.Add(new SQLiteParameter("@InstantaneousPowerFactor", Data.powerFactor));
                        command.Parameters.Add(new SQLiteParameter("@Frequency", Data.frequency));
                        connection.Open();

                        int c = command.ExecuteNonQuery();
                        if (c > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SavePowerQuality(InstantaneousObjClass Data)
        {
            try
            {
                string cmd = $"INSERT INTO [instantaneous_data] VALUES(?,{SqliteCurrentDateTime},?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        SQLiteParameter T = null;

                        //command.Prepare();
                        command.Parameters.Add(new SQLiteParameter("@MSN", Data.MSN));
                        T = new SQLiteParameter("@MeterDateTime", Data.dateTime.GetDateTime());
                        T.DbType = DbType.DateTime;
                        command.Parameters.Add(T);

                        command.Parameters.Add(new SQLiteParameter("@Current_PhaseA", Data.current_PhaseA));
                        command.Parameters.Add(new SQLiteParameter("@Current_PhaseB", Data.current_PhaseB));
                        command.Parameters.Add(new SQLiteParameter("@Current_PhaseC", Data.current_PhaseC));
                        command.Parameters.Add(new SQLiteParameter("@Current_PhaseTL", Data.current_PhaseTL));

                        command.Parameters.Add(new SQLiteParameter("@Voltage_PhaseA", Data.voltage_PhaseA));
                        command.Parameters.Add(new SQLiteParameter("@Voltage_PhaseB", Data.voltage_PhaseB));
                        command.Parameters.Add(new SQLiteParameter("@Voltage_PhaseC", Data.voltage_PhaseC));
                        command.Parameters.Add(new SQLiteParameter("@Voltage_PhaseTL", Data.voltage_PhaseTL));

                        command.Parameters.Add(new SQLiteParameter("@ActivePowerPositive_PhaseA", Data.active_powerPositive_PhaseA));
                        command.Parameters.Add(new SQLiteParameter("@ActivePowerPositive_PhaseB", Data.active_powerPositive_PhaseB));
                        command.Parameters.Add(new SQLiteParameter("@ActivePowerPositive_PhaseC", Data.active_powerPositive_PhaseC));
                        command.Parameters.Add(new SQLiteParameter("@ActivePowerPositive_PhaseTL", Data.active_powerPositive_PhaseTL));

                        command.Parameters.Add(new SQLiteParameter("@ActivePowerNegative_PhaseA", Data.active_powerNegative_PhaseA));
                        command.Parameters.Add(new SQLiteParameter("@ActivePowerNegative_PhaseB", Data.active_powerNegative_PhaseB));
                        command.Parameters.Add(new SQLiteParameter("@ActivePowerNegative_PhaseC", Data.active_powerNegative_PhaseC));
                        command.Parameters.Add(new SQLiteParameter("@ActivePowerNegative_PhaseTL", Data.active_powerNegative_PhaseTL));

                        command.Parameters.Add(new SQLiteParameter("@ReactivePowerPositive_PhaseA", Data.reactive_powerPositive_PhaseA));
                        command.Parameters.Add(new SQLiteParameter("@ReactivePowerPositive_PhaseB", Data.reactive_powerPositive_PhaseB));
                        command.Parameters.Add(new SQLiteParameter("@ReactivePowerPositive_PhaseC", Data.reactive_powerPositive_PhaseC));
                        command.Parameters.Add(new SQLiteParameter("@ReactivePowerPositive_PhaseTL", Data.reactive_powerPositive_PhaseTL));

                        command.Parameters.Add(new SQLiteParameter("@ReactivePowerNegative_PhaseA", Data.reactive_powerNegative_PhaseA));
                        command.Parameters.Add(new SQLiteParameter("@ReactivePowerNegative_PhaseB", Data.reactive_powerNegative_PhaseB));
                        command.Parameters.Add(new SQLiteParameter("@ReactivePowerNegative_PhaseC", Data.reactive_powerNegative_PhaseC));
                        command.Parameters.Add(new SQLiteParameter("@ReactivePowerNegative_PhaseTL", Data.reactive_powerNegative_PhaseTL));

                        command.Parameters.Add(new SQLiteParameter("@Frequency", Data.frequency));

                        connection.Open();
                        int c = command.ExecuteNonQuery();
                        if (c > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region Parameterization

        #region Meter

        public bool saveLimits(Param_Limits obj)
        {
            try
            {
                string cmd = "INSERT INTO [limits] (session_id,over_volt, under_volt, imbalance_volt, high_neutral_current, reverse_energy, tamper_energy, ct_fail, pt_fail_amp, pt_fail_volt, overcurrentbyphase_T1, overcurrentbyphase_T2, overcurrentbyphase_T3, overcurrentbyphase_T4, overloadbyphase_T1, overloadbyphase_T2, overloadbyphase_T3, overloadbyphase_T4, overloadtotal_T1, overloadtotal_T2, overloadtotal_T3, overloadtotal_T4, mdiexceed_T1, mdiexceed_T2, mdiexceed_T3, mdiexceed_T4)  VALUES( ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.AddWithValue("@session_id", session_id);
                        command.Parameters.AddWithValue("@overVolt", obj.OverVolt);
                        command.Parameters.AddWithValue("@underVolt", obj.UnderVolt);
                        command.Parameters.AddWithValue("@imbalanceVolt", obj.ImbalanceVolt);
                        command.Parameters.AddWithValue("@highNeutralCurrent", obj.HighNeutralCurrent);
                        command.Parameters.AddWithValue("@reverseEnergy", obj.ReverseEnergy);/// / 1000);///
                        command.Parameters.AddWithValue("@tamperEnergy", obj.TamperEnergy);/// / 1000);
                        command.Parameters.AddWithValue("@ctFail", obj.CTFail_AMP);
                        command.Parameters.AddWithValue("@ptFailAmp", obj.PTFail_AMP);
                        command.Parameters.AddWithValue("@ptFailVolt", obj.PTFail_Volt);
                        command.Parameters.AddWithValue("@overCurrentByPhaseT1", obj.OverCurrentByPhase_T1);
                        command.Parameters.AddWithValue("@overCurrentByPhaseT2", obj.OverCurrentByPhase_T2);
                        command.Parameters.AddWithValue("@overCurrentByPhaseT3", obj.OverCurrentByPhase_T3);
                        command.Parameters.AddWithValue("@overCurrentByPhaseT4", obj.OverCurrentByPhase_T4);
                        command.Parameters.AddWithValue("@overLoadByPhaseT1", obj.OverLoadByPhase_T1);/// / 1000);
                        command.Parameters.AddWithValue("@overLoadByPhaseT2", obj.OverLoadByPhase_T2);/// / 1000);
                        command.Parameters.AddWithValue("@overLoadByPhaseT3", obj.OverLoadByPhase_T3);/// / 1000);
                        command.Parameters.AddWithValue("@overLoadByPhaseT4", obj.OverLoadByPhase_T4);/// / 1000);
                        command.Parameters.AddWithValue("@overLoadTotalT1", obj.OverLoadTotal_T1);/// / 1000);
                        command.Parameters.AddWithValue("@overLoadTotalT2", obj.OverLoadTotal_T2);/// 1000);
                        command.Parameters.AddWithValue("@overLoadTotalT3", obj.OverLoadTotal_T3);/// 1000);
                        command.Parameters.AddWithValue("@overLoadTotalT4", obj.OverLoadTotal_T4);/// 1000);
                        command.Parameters.AddWithValue("@mdiExceedT1", obj.DemandOverLoadTotal_T1);/// 1000);
                        command.Parameters.AddWithValue("@mdiExceedT2", obj.DemandOverLoadTotal_T2);/// 1000);
                        command.Parameters.AddWithValue("@mdiExceedT3", obj.DemandOverLoadTotal_T3);/// 1000);
                        command.Parameters.AddWithValue("@mdiExceedT4", obj.DemandOverLoadTotal_T4);/// 1000);
                        connection.Open();

                        int x = command.ExecuteNonQuery();

                        if (x == 0)
                        {
                            return false;
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save limits => " + ex.Message, ex);
            }
        }

        public bool saveMonitoringTime(Param_Monitoring_Time obj)
        {
            try
            {
                string cmd = "INSERT INTO [monitoring_time] (session_id, power_fail, phase_fail, over_volt, high_neutral_current, imbalance_volt, over_current, over_load, reverse_polarity, under_volt, reverse_energy, tamper_energy, ct_fail, pt_fail, pud_tomonitor, pud_for_energyrecord, phase_sequence) VALUES (?,?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?,?,?)";

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.AddWithValue("@session_id", session_id);
                        command.Parameters.AddWithValue("@powerFail", Convert.ToInt32(obj.PowerFail.TotalSeconds));
                        command.Parameters.AddWithValue("@phaseFail", Convert.ToInt32(obj.PhaseFail.TotalSeconds));
                        command.Parameters.AddWithValue("@overVolt", Convert.ToInt32(obj.OverVolt.TotalSeconds));
                        command.Parameters.AddWithValue("@highNeutralCurrent", Convert.ToInt32(obj.HighNeutralCurrent.TotalSeconds));
                        command.Parameters.AddWithValue("@imbalanceVolt", Convert.ToInt32(obj.ImbalanceVolt.TotalSeconds));
                        command.Parameters.AddWithValue("@overCurrent", Convert.ToInt32(obj.OverCurrent.TotalSeconds));
                        command.Parameters.AddWithValue("@overLoad", Convert.ToInt32(obj.OverLoad.TotalSeconds));
                        command.Parameters.AddWithValue("@reversePolarity", Convert.ToInt32(obj.ReversePolarity.TotalSeconds));
                        command.Parameters.AddWithValue("@under_volt", Convert.ToInt32(obj.UnderVolt.TotalSeconds));
                        command.Parameters.AddWithValue("@reverseEnergy", Convert.ToInt32(obj.ReverseEnergy.TotalSeconds));
                        command.Parameters.AddWithValue("@tamperEnergy", Convert.ToInt32(obj.TamperEnergy.TotalSeconds));
                        command.Parameters.AddWithValue("@ct_fail", Convert.ToInt32(obj.CTFail.TotalSeconds));
                        command.Parameters.AddWithValue("@pt_fail", Convert.ToInt32(obj.PTFail.TotalSeconds));
                        command.Parameters.AddWithValue("@pudToMonitor", Convert.ToInt32(obj.PowerUPDelay.TotalSeconds));
                        command.Parameters.AddWithValue("@pudForEnergyRecording", Convert.ToInt32(obj.PowerUpDelayEnergyRecording.TotalSeconds));
                        command.Parameters.AddWithValue("@phaseSequence", Convert.ToInt32(obj.PhaseSequence.TotalSeconds));
                        connection.Open();

                        int x = command.ExecuteNonQuery();
                        if (x == 0)
                        {
                            return false;
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save Monitoring Time => " + ex.Message, ex);
            }
        }

        public bool saveActivityCalendar(Param_ActivityCalendar calendar)
        {
            int cal_id = 0;
            int x = 0;
            try
            {
                #region SaveCalendarINFO
                //OpenConnection();
                string cmd = "INSERT INTO [calendar] (session_id,calendar_name, activation_date, day_profile_count, week_profile_count, season_profile_count, special_day_count) VALUES ( ?,?, ?, ?, ?, ?, ?)";

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.Clear();
                        command.CommandText = cmd;
                        command.Connection = connection;

                        command.Parameters.AddWithValue("@session_id", session_id);
                        command.Parameters.AddWithValue("@calendar_name", calendar.CalendarName);
                        command.Parameters.AddWithValue("@activation_date", calendar.CalendarstartDate.GetDateTime());
                        command.Parameters.AddWithValue("@day_profile_count", calendar.ParamDayProfile.DayProfileCount);
                        command.Parameters.AddWithValue("@week_profile_count", calendar.ParamWeekProfile.weekProfile_Table.Count);
                        command.Parameters.AddWithValue("@season_profile_count", calendar.ParamSeasonProfile.seasonProfile_Table.Count);
                        command.Parameters.AddWithValue("@special_day_count", calendar.ParamSpecialDay.specialDay_Table.Count);
                        connection.Open();

                        command.ExecuteNonQuery();

                        command.CommandText = "Select last_insert_rowid()"; //@@Identity";
                        cal_id = Convert.ToInt32(command.ExecuteScalar());
                        #endregion

                        #region SAVE DAYPROFILE INFO
                        foreach (DayProfile day_profile in calendar.ParamDayProfile.dayProfile_Table)
                        {
                            foreach (TimeSlot slot in day_profile.dayProfile_Schedule)
                            {
                                command.Parameters.Clear();

                                command.CommandText = "INSERT INTO [day_profile] (cal_id, day_profile_id, slot_id, tariff, start_time) VALUES (?,?,?,?,?)";
                                command.Parameters.AddWithValue("@cal_id", cal_id);
                                command.Parameters.AddWithValue("@day_profile_id", day_profile.Day_Profile_ID);
                                command.Parameters.AddWithValue("@slot_id", Convert.ToInt32(slot.TimeSlotId));

                                command.Parameters.AddWithValue("@tariff", slot.ScriptSelector.ToString());
                                command.Parameters.AddWithValue("@start_time", slot.StartTime.ToString());

                                x = command.ExecuteNonQuery();
                                if (x == 0)
                                {
                                    throw new Exception("Error saving Day Profile information");
                                }
                            }
                        }
                        #endregion

                        #region SAVE WEEK PROFILE INFO
                        foreach (WeekProfile week_profile in calendar.ParamWeekProfile.weekProfile_Table)
                        {
                            command.Parameters.Clear();

                            cmd = "INSERT INTO [week_profile] (cal_id, week_profile_id, dp_monday, dp_tuesday, dp_wednesday, dp_thursday, dp_friday, dp_saturday, dp_sunday) VALUES (?, ?,?, ?, ?, ?, ?, ?, ?)";
                            command.CommandText = cmd;

                            command.Parameters.AddWithValue("@cal_id", cal_id);
                            command.Parameters.AddWithValue("@week_profile_id", week_profile.Profile_Name_Str);
                            command.Parameters.AddWithValue("@dp_monday", week_profile.Day_Profile_MON.Day_Profile_ID);
                            command.Parameters.AddWithValue("@dp_tuesday", week_profile.Day_Profile_TUE.Day_Profile_ID);
                            command.Parameters.AddWithValue("@dp_wednesday", week_profile.Day_Profile_WED.Day_Profile_ID);
                            command.Parameters.AddWithValue("@dp_thursday", week_profile.Day_Profile_THRU.Day_Profile_ID);
                            command.Parameters.AddWithValue("@dp_friday", week_profile.Day_Profile_FRI.Day_Profile_ID);
                            command.Parameters.AddWithValue("@dp_saturday", week_profile.Day_Profile_SAT.Day_Profile_ID);
                            command.Parameters.AddWithValue("@dp_sunday", week_profile.Day_Profile_SUN.Day_Profile_ID);

                            x = command.ExecuteNonQuery();

                            if (x == 0)
                            {
                                throw new Exception("Error saving Week Profile information");
                            }

                        }
                        #endregion

                        #region SAVE SEASON PROFILE
                        foreach (SeasonProfile season_profile in calendar.ParamSeasonProfile.seasonProfile_Table)
                        {
                            command.Parameters.Clear();

                            cmd = "INSERT INTO [season_profile] (cal_id, season_profile_id, week_profile_id, startdate_month, startdate_date) VALUES (?, ?, ?, ?, ?)";
                            command.CommandText = cmd;
                            command.Parameters.AddWithValue("@cal_id", cal_id);
                            command.Parameters.AddWithValue("@season_profile_id", season_profile.Profile_Name_Str);
                            //  command.Parameters.AddWithValue("@season_profile_name", season_profile.Profile_Name_Str);
                            command.Parameters.AddWithValue("@week_profile_id", season_profile.Week_Profile.Profile_Name_Str);
                            command.Parameters.AddWithValue("@startdate_month", season_profile.Start_Date.Month);
                            command.Parameters.AddWithValue("@startdate_date", season_profile.Start_Date.DayOfMonth);

                            x = command.ExecuteNonQuery();

                            if (x == 0)
                            {
                                throw new Exception("Error saving Season Profile information");
                            }
                        }
                        #endregion

                        #region SAVE SPECIAL DAYS INFO

                        foreach (SpecialDay special_day in calendar.ParamSpecialDay.specialDay_Table)
                        {
                            command.Parameters.Clear();

                            //  cmd = "INSERT INTO special_day (cal_id, special_day_id, day_profile_id, year, month, date, dayofweek) VALUES (?, ?, ?, ?, ?, ?, ?)";
                            cmd = String.Format("INSERT INTO [special_day] (cal_id, special_day_id, day_profile_id, [year], [month], [date], dayofweek) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", cal_id, special_day.SpecialDayID, special_day.DayProfile.Day_Profile_ID, special_day.StartDate.Year, special_day.StartDate.Month, special_day.StartDate.DayOfMonth, special_day.StartDate.DayOfWeek);
                            command.CommandText = cmd;
                            x = command.ExecuteNonQuery();

                            if (x == 0)
                            {
                                throw new Exception("Error saving Special Day Profile information");
                            }

                        }

                        #endregion

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Calendar=> " + ex.Message, ex);
            }
        }

        public bool saveMDIparams(Param_MDI_parameters obj)
        {
            try
            {
                string cmd = "INSERT INTO [mdi_params] (session_id,min_time_bw_reset_manual, min_time_bw_reset_unit, manual_reset_button, manual_reset_remotecommand, manual_reset_powerdownmode, auto_reset_flag, auto_reset_date, mdi_interval_min, slides_count) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.AddWithValue("@session_id", session_id);
                        command.Parameters.AddWithValue("@min_time_bw_reset_manual", obj.Minimum_Time_Interval_Between_Resets_In_case_of_Manual_Reset);
                        command.Parameters.AddWithValue("@min_time_bw_reset_unit", obj.Min_Time_Unit);
                        command.Parameters.AddWithValue("@manual_reset_button", Convert.ToInt16(obj.FLAG_Manual_Reset_by_button_1));
                        command.Parameters.AddWithValue("@manual_reset_remotecommand", Convert.ToInt16(obj.FLAG_Manual_Reset_by_remote_command_2));
                        command.Parameters.AddWithValue("@manual_reset_powerdownmode", Convert.ToInt16(obj.FLAG_Manual_Reset_in_PowerDown_Mode));
                        command.Parameters.AddWithValue("@auto_reset_flag", Convert.ToInt16(obj.FLAG_Auto_Reset_0));
                        command.Parameters.AddWithValue("@auto_reset_date", Convert.ToInt16(obj.Auto_reset_date.DayOfMonth));//Convert.ToInt16(obj.Auto_reset_date.GetDateTime().Day));
                        command.Parameters.AddWithValue("@mdi_interval_min", Convert.ToInt16(obj.MDI_Interval));
                        command.Parameters.AddWithValue("@slides_count", Convert.ToInt16(obj.Roll_slide_count));
                        connection.Open();
                        int x = command.ExecuteNonQuery();

                        if (x == 0)
                        {
                            return false;
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save MDI => " + ex.Message, ex);
            }
        }

        public bool saveLoadProfileChannels(List<LoadProfileChannelInfo> obj)
        {
            //try
            //{
            //    OpenConnection();
            //    string cmd = "INSERT INTO [param_loadprofilechannels] ([session_id], [interval], channel1, channel2, channel3, channel4) VALUES (?, ?, ?, ?, ?, ?)";
            //    command = new SQLiteCommand(cmd, connection);

            //    int minutes = Convert.ToInt32(obj[0].CapturePeriod.TotalMinutes);

            //    command.Parameters.AddWithValue("@session_id", session_id);
            //    command.Parameters.AddWithValue("@interval", minutes);
            //    command.Parameters.AddWithValue("@channel1", obj[0].Quantity_Name);
            //    command.Parameters.AddWithValue("@channel2", obj[1].Quantity_Name);
            //    command.Parameters.AddWithValue("@channel3", obj[2].Quantity_Name);
            //    command.Parameters.AddWithValue("@channel4", obj[3].Quantity_Name);

            //    int x = command.ExecuteNonQuery();

            //    if (x == 0)
            //    {
            //        return false;
            //    }
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("Can not save Load Profile Channels => " + ex.Message, ex);
            //}
            //finally
            //{
            //    connection.Close();
            //}
            return true;
        }

        public bool saveDisplayWindow_Normal(DisplayWindows normal)
        {
            try
            {
                saveDisplayWindow(normal, 0);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool saveDisplayWindow_Alternate(DisplayWindows alt)
        {
            try
            {
                saveDisplayWindow(alt, 1);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool saveDisplayWindow(DisplayWindows alt, int windowtype)
        {
            string cmd = string.Empty;
            DateTime datetime = DateTime.Now;
            try
            {
                cmd = "INSERT INTO [display_window]([session_id],window_name,window_type,scroll_time,window_number)VALUES(?,?,?,?,?)";

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        connection.Open();
                        foreach (DisplayWindowItem item in alt.Windows)
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@session_id", session_id);
                            command.Parameters.AddWithValue("@window_name", item.Window_Name);
                            command.Parameters.AddWithValue("@windowtype", windowtype);
                            command.Parameters.AddWithValue("@scroll_time", Convert.ToInt32(alt.ScrollTime.Seconds));
                            ///command.Parameters.AddWithValue("@window_number",Convert.ToUInt32(item.WindowNumberToDisplay.DisplayWindowNumber));
                            SQLiteParameter param = new SQLiteParameter("@window_number", DbType.UInt64);
                            param.Value = item.WindowNumberToDisplay.DisplayWindowNumber;
                            command.Parameters.Add(param);
                            int x = command.ExecuteNonQuery();
                            if (x == 0)
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save Display Windows " + ((windowtype == 0) ? "Normal" : "Alternate") + ex.Message, ex);
            }
        }

        public bool saveDisplayPowerDown(Param_Display_PowerDown dispPowerDownParam)
        {
            string cmd = string.Empty;
            DateTime datetime = DateTime.Now;
            try
            {
                cmd = "INSERT INTO [display_powerdown]([session_id],Off_Delay,Off_Time,On_Time,Is_AlwaysOn,Is_ImmidateOff,Is_DisplayOnByButton,Is_DisplayRepeat,Is_OnTimeCycleScroll)VALUES(?,?,?,?,?,?,?,?,?);";

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.Add("@session_id", DbType.UInt64).Value = session_id;
                        //v4.8.30 TinyInt to SmallInt
                        command.Parameters.Add("@Off_Delay", DbType.UInt16).Value = dispPowerDownParam.OffDelay;
                        command.Parameters.AddWithValue("@Off_Time", DbType.UInt16).Value = dispPowerDownParam.OffTime;
                        command.Parameters.AddWithValue("@On_Time", DbType.UInt16).Value = dispPowerDownParam.OnTime;
                        command.Parameters.AddWithValue("@Is_AlwaysOn", DbType.Boolean).Value = dispPowerDownParam.IsAlwaysOn;
                        command.Parameters.AddWithValue("@Is_ImmidateOff", DbType.Boolean).Value = dispPowerDownParam.IsImmidateOff;
                        command.Parameters.AddWithValue("@Is_DisplayOnByButton", DbType.Boolean).Value = dispPowerDownParam.IsDisplayOnByButton;
                        command.Parameters.AddWithValue("@Is_DisplayRepeat", DbType.Boolean).Value = dispPowerDownParam.IsDisplayRepeat;
                        command.Parameters.AddWithValue("@Is_OnTimeCycleScroll", DbType.Boolean).Value = dispPowerDownParam.IsOnTimeCycleScroll;
                        connection.Open();

                        int x = command.ExecuteNonQuery();
                        if (x == 0)
                        {
                            return false;
                        }

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save Display Power Down Mode Parameters" + ex.Message, ex);
            }
        }

        public bool saveCTPT(Param_CTPT_Ratio obj)
        {
            try
            {
                string cmd = "INSERT INTO [ct_pt_ratio] ([session_id], ct_ratio_num, ct_ratio_denom, pt_ratio_num, pt_ratio_denom) VALUES (?,?, ?, ?, ?)";

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.AddWithValue("@session_id", session_id);
                        command.Parameters.AddWithValue("@ct_ratio_num", Convert.ToInt32(obj.CTratio_Numerator));
                        command.Parameters.AddWithValue("@ct_ratio_denom", Convert.ToInt32(obj.CTratio_Denominator));
                        command.Parameters.AddWithValue("@pt_ratio_num", Convert.ToInt32(obj.PTratio_Numerator));
                        command.Parameters.AddWithValue("@pt_ratio_denom", Convert.ToInt32(obj.PTratio_Denominator));
                        connection.Open();
                        int x = command.ExecuteNonQuery();

                        if (x == 0)
                        {
                            return false;
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save CT PT Ratio => " + ex.Message, ex);
            }
        }

        public bool saveDecimalPoint(Param_Decimal_Point obj)
        {
            try
            {
                string cmd = "INSERT INTO [decimal_point] ([session_id], billing_energy, billing_mdi, instantaneous_voltage, instantaneous_power, instantaneous_current, instantaneous_mdi) VALUES (?, ?, ?, ?, ?, ?, ?)";

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.AddWithValue("@session_id", session_id);
                        command.Parameters.AddWithValue("@billing_energy", DecimalPoint_toGUI(obj.Billing_Energy));
                        command.Parameters.AddWithValue("@billing_mdi", DecimalPoint_toGUI(obj.Billing_MDI));
                        command.Parameters.AddWithValue("@instantaneous_voltage", DecimalPoint_toGUI(obj.Instataneous_Voltage));
                        command.Parameters.AddWithValue("@instantaneous_power", DecimalPoint_toGUI(obj.Instataneous_Power));
                        command.Parameters.AddWithValue("@instantaneous_current", DecimalPoint_toGUI(obj.Instataneous_Current));
                        command.Parameters.AddWithValue("@instantaneous_mdi", DecimalPoint_toGUI(obj.Instataneous_MDI));
                        connection.Open();
                        int x = command.ExecuteNonQuery();

                        if (x == 0)
                        {
                            return false;
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save Decimal Point => " + ex.Message, ex);
            }
        }

        public bool saveCustomerRef(Param_Customer_Code obj, string customerName, string customerAddress)
        {
            try
            {
                string cmd = "INSERT INTO [customer_reference] ([session_id], customer_code, customer_name, customer_address) VALUES (?,?, ?, ?)";

                //v4.8.14  start
                if (customerName == null) customerName = "";
                if (customerAddress == null) customerAddress = "";
                //v4.8.14 end
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.AddWithValue("@session_id", session_id);
                        command.Parameters.AddWithValue("@customer_code", obj.Customer_Code_String.Replace("\0", string.Empty));
                        command.Parameters.AddWithValue("@customer_name", customerName);
                        command.Parameters.AddWithValue("@customer_address", customerAddress);
                        connection.Open();
                        int x = command.ExecuteNonQuery();

                        if (x == 0)
                        {
                            return false;
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save Customer Reference => " + ex.Message, ex);
            }
        }

        public bool saveContactor(Param_Contactor obj)
        {
            try
            {
                string cmd = "INSERT INTO [contactor]" +
"([session_id],contactor_ON_pulsetime,contactor_OFF_pulsetime,min_time_bw_contactor_statechange,power_up_delay_statechange,interval_between_retries,retry_count," +
"control_mode,over_volt,under_volt,overcurrentbyphase_T1,overcurrentbyphase_T2,overcurrentbyphase_T3,overcurrentbyphase_T4,overloadbyphase_T1,overloadbyphase_T2," + "overloadbyphase_T3,overloadbyphase_T4,overloadtotal_T1,overloadtotal_T2,overloadtotal_T3,overloadtotal_T4,mdioverload_T1,mdioverload_T2,mdioverload_T3," +
"mdioverload_T4)VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.AddWithValue("@session_id", session_id);
                        command.Parameters.AddWithValue("@contactor_ON_pulsetime", Convert.ToInt16(obj.Contactor_ON_Pulse_Time));
                        command.Parameters.AddWithValue("@contactor_OFF_pulsetime", Convert.ToInt16(obj.Contactor_OFF_Pulse_Time));
                        command.Parameters.AddWithValue("@min_time_bw_contactor_statechange", Convert.ToInt16(obj.Minimum_Interval_Bw_Contactor_State_Change));
                        command.Parameters.AddWithValue("@power_up_delay_statechange", Convert.ToInt16(obj.Power_Up_Delay_To_State_Change));
                        command.Parameters.AddWithValue("@interval_between_retries", Convert.ToInt16(obj.Interval_Between_Retries));
                        command.Parameters.AddWithValue("@retry_count", Convert.ToInt16(obj.RetryCount));
                        command.Parameters.AddWithValue("@control_mode", Convert.ToInt16(obj.Control_Mode));

                        //command.Parameters.AddWithValue("@remote_control", Convert.ToInt16(obj.Remotely_Control_FLAG_2));
                        //command.Parameters.AddWithValue("@local_control", Convert.ToInt16(obj.Local_Control_FLAG_3));

                        command.Parameters.AddWithValue("@over_volt", Convert.ToInt16(obj.Over_Volt_FLAG_0));
                        command.Parameters.AddWithValue("@under_volt", Convert.ToInt16(obj.Under_Volt_FLAG_1));
                        command.Parameters.AddWithValue("@overcurrentbyphase_T1", Convert.ToInt16(obj.Over_Current_By_Phase_T1_FLAG_0));
                        command.Parameters.AddWithValue("@overcurrentbyphase_T2", Convert.ToInt16(obj.Over_Current_By_Phase_T2_FLAG_1));
                        command.Parameters.AddWithValue("@overcurrentbyphase_T3", Convert.ToInt16(obj.Over_Current_By_Phase_T3_FLAG_2));
                        command.Parameters.AddWithValue("@overcurrentbyphase_T4", Convert.ToInt16(obj.Over_Current_By_Phase_T4_FLAG_3));
                        command.Parameters.AddWithValue("@overloadbyphase_T1", Convert.ToInt16(obj.Over_Load_By_Phase_T1_FLAG_4));
                        command.Parameters.AddWithValue("@overloadbyphase_T2", Convert.ToInt16(obj.Over_Load_By_Phase_T2_FLAG_5));
                        command.Parameters.AddWithValue("@overloadbyphase_T3", Convert.ToInt16(obj.Over_Load_By_Phase_T3_FLAG_6));
                        command.Parameters.AddWithValue("@overloadbyphase_T4", Convert.ToInt16(obj.Over_Load_By_Phase_T4_FLAG_7));
                        command.Parameters.AddWithValue("@overloadtotal_T1", Convert.ToInt16(obj.Over_Load_T1_FLAG_0));
                        command.Parameters.AddWithValue("@overloadtotal_T2", Convert.ToInt16(obj.Over_Load_T2_FLAG_1));
                        command.Parameters.AddWithValue("@overloadtotal_T3", Convert.ToInt16(obj.Over_Load_T3_FLAG_2));
                        command.Parameters.AddWithValue("@overloadtotal_T4", Convert.ToInt16(obj.Over_Load_T4_FLAG_3));
                        command.Parameters.AddWithValue("@mdioverload_T1", Convert.ToInt16(obj.Over_MDI_T1_FLAG_4));
                        command.Parameters.AddWithValue("@mdioverload_T2", Convert.ToInt16(obj.Over_MDI_T2_FLAG_5));
                        command.Parameters.AddWithValue("@mdioverload_T3", Convert.ToInt16(obj.Over_MDI_T3_FLAG_6));
                        command.Parameters.AddWithValue("@mdioverload_T4", Convert.ToInt16(obj.Over_MDI_T4_FLAG_7));
                        connection.Open();
                        int x = command.ExecuteNonQuery();

                        if (x == 0)
                        {
                            return false;
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save Contactor => " + ex.Message, ex);
            }
        }

        public bool saveClock(Param_Clock_Caliberation obj)
        {
            try
            {
                string cmd = "INSERT INTO [meter_datetime] ([session_id], meter_datetime) VALUES (?, ?)";

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.AddWithValue("@session_id", session_id);
                        command.Parameters.AddWithValue("@meter_datetime", obj.Set_Time);
                        connection.Open();

                        int x = command.ExecuteNonQuery();

                        if (x == 0)
                        {
                            return false;
                        }
                        return true;

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save Clock => " + ex.Message, ex);
            }
        }

        #endregion

        #region Modem

        public int saveModem(string msn)
        {
            try
            {
                string cmd = $"INSERT INTO [modem] (msn,[session_id], [date_time]) VALUES (?,?, {SqliteCurrentDateTime})";
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.AddWithValue("@msn", msn);
                        command.Parameters.AddWithValue("@session_id", session_id);
                        connection.Open();

                        command.ExecuteNonQuery();
                        //cmd = "Select last_insert_id() as last_id from [modem]";
                        //command = new SQLiteCommand(cmd, connection);
                        //reader = command.ExecuteReader();
                        //reader.Read();
                        //int modem_id = Convert.ToInt16(reader["last_id"]);
                        cmd = "Select last_insert_rowid()"; //@@Identity";
                        command.CommandText = cmd;
                        int modem_id = Convert.ToInt32(command.ExecuteScalar());
                        return modem_id;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save Modem => " + ex.Message, ex);
            }
        }

        public bool saveIPProfile(int modem_config_id, Param_IP_Profiles[] list_obj_ip)
        {
            try
            {
                string cmd = string.Empty;
                int x = 0;
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        connection.Open();
                        foreach (Param_IP_Profiles item in list_obj_ip)
                        {
                            command.Parameters.Clear();

                            command.CommandText = "INSERT INTO [modem_ip_profile] (modem_configID, [id], ip_address, wrapper_tcpport, wrapper_udpport, hdlc_tcpport, hdlc_udpport) VALUES (?, ?, ?, ?, ?, ?, ?)";

                            command.Parameters.AddWithValue("@mmodem_configID", modem_config_id);
                            command.Parameters.AddWithValue("@id", item.Unique_ID.ToString());
                            IPAddress ip = IPAddress.Parse(item.IP.ToString());
                            command.Parameters.AddWithValue("@ip_address", ip.ToString());
                            command.Parameters.AddWithValue("@wrapper_tcpport", item.Wrapper_Over_TCP_port.ToString());
                            command.Parameters.AddWithValue("@wrapper_udpport", item.Wrapper_Over_UDP_port.ToString());
                            command.Parameters.AddWithValue("@hdlc_tcpport", item.HDLC_Over_TCP_Port.ToString());
                            command.Parameters.AddWithValue("@hdlc_udpport", item.HDLC_Over_UDP_Port.ToString());
                            x = command.ExecuteNonQuery();
                            if (x == 0)
                            {
                                throw new Exception("Error saving IP Profiles");
                            }
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save IP profiles => " + ex.Message, ex);
            }
        }

        public bool saveNumberProfile(int modem_config_id, Param_Number_Profile[] list_obj_numberProfile)
        {
            try
            {
                string cmd = string.Empty;
                string number = string.Empty;
                string dataCallnumber = string.Empty;
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        connection.Open();

                        foreach (Param_Number_Profile item in list_obj_numberProfile)
                        {
                            command.Parameters.Clear();

                            cmd = "INSERT INTO [modem_numberprofile] (modem_configID, [id], wakeup_on_voicecall, wakeup_on_sms," +
                                       "wakeup_type, [number], datacallnumber, flag_verify_password, flag_wakeup_on_sms, flag_wakeup_on_voicecall, " +
                                       "flag_accept_parameters_sms, flag_allow_twoway_sms_comm, flag_reject_with_attend, flag_reject_call, flag_accept_datacall) " +
                                       "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?);";

                            command.CommandText = cmd;

                            number = Commons_DB.ConvertToValidString(item.Number);
                            dataCallnumber = Commons_DB.ConvertToValidString(item.Datacall_Number);

                            command.Parameters.AddWithValue("@modem_configID", modem_config_id);
                            command.Parameters.AddWithValue("@id", Convert.ToInt16(item.Unique_ID));
                            command.Parameters.AddWithValue("@wakeup_on_voicecall", item.Wake_Up_On_Voice_Call.ToString());
                            command.Parameters.AddWithValue("@wakeup_on_sms", item.Wake_Up_On_SMS.ToString());
                            command.Parameters.AddWithValue("@wakeup_type", item.FLAG2.ToString());
                            command.Parameters.AddWithValue("@number", number);
                            command.Parameters.AddWithValue("@datacallnumber", dataCallnumber);
                            command.Parameters.AddWithValue("@flag_verify_password", Convert.ToInt16(item.Verify_Password_FLAG_0));
                            command.Parameters.AddWithValue("@flag_wakeup_on_sms", Convert.ToInt16(item.Wake_Up_On_SMS));
                            command.Parameters.AddWithValue("@flag_wakeup_on_voicecall", Convert.ToInt16(item.Wake_Up_On_Voice_Call));
                            command.Parameters.AddWithValue("@flag_accept_parameters_sms", Convert.ToInt16(item.Accept_Paramaeters_In_Wake_Up_SMS_FLAG_4));
                            command.Parameters.AddWithValue("@flag_allow_twoway_sms_comm", Convert.ToInt16(item.Allow_2way_SMS_communication_FLAG_6));
                            command.Parameters.AddWithValue("@flag_reject_with_attend", Convert.ToInt16(item.Reject_With_Attend_FLAG_2));
                            command.Parameters.AddWithValue("@flag_reject_call", Convert.ToInt16(item.Reject_Call_FLAG_1));
                            command.Parameters.AddWithValue("@flag_accept_datacall", Convert.ToInt16(item.Accept_Data_Call_FLAG_7));

                            int x = command.ExecuteNonQuery();
                            if (x == 0)
                            {
                                throw new Exception("Error saving Number Profile");
                            }
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save Number Profile => " + ex.Message, ex);
            }
        }

        public bool saveWakeupProfile(int modem_config_id, Param_WakeUp_Profile[] list_wakeup)
        {
            try
            {
                string cmd = string.Empty;
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        connection.Open();
                        foreach (Param_WakeUp_Profile item in list_wakeup)
                        {
                            command.Parameters.Clear();
                            cmd = "INSERT INTO [modem_wakeupprofile] (modem_configID,wakeup_profile_id, priroity1_ip, priroity2_ip, priroity3_ip, priroity4_ip) VALUES (?, ?, ?, ?, ?,?)";
                            command.CommandText = cmd;

                            command.Parameters.AddWithValue("@modem_configID", modem_config_id);
                            command.Parameters.AddWithValue("@wakeup_profile_id", item.Wake_Up_Profile_ID);
                            command.Parameters.AddWithValue("@priroity1_ip", item.IP_Profile_ID_1);
                            command.Parameters.AddWithValue("@priroity2_ip", item.IP_Profile_ID_2);
                            command.Parameters.AddWithValue("@priroity3_ip", item.IP_Profile_ID_3);
                            command.Parameters.AddWithValue("@priroity4_ip", item.IP_Profile_ID_4);
                            int x = command.ExecuteNonQuery();

                            if (x == 0)
                            {
                                throw new Exception("Error saving Wakeup Profile");
                            }
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save Wakeup Profile => " + ex.Message, ex);
            }
        }

        public bool saveCommProfile(int modem_config_id, Param_Communication_Profile obj_comm)
        {
            try
            {
                string cmd = string.Empty;
                cmd = "INSERT INTO [modem_comm_profile] (modem_configID, selection_mode, wakeup_id, protocol_flag, transport_flag, number1, number2, number3, number4) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.AddWithValue("@modem_configID", modem_config_id);
                        command.Parameters.AddWithValue("@selection_mode", obj_comm.SelectedMode);
                        command.Parameters.AddWithValue("@wakeup_id", obj_comm.WakeUpProfileID);
                        command.Parameters.AddWithValue("@protocol_flag", obj_comm.Protocol_HDLC_TCP_Flag_0);
                        command.Parameters.AddWithValue("@transport_flag", obj_comm.Protocol_TCP_UDP_Flag_1);
                        command.Parameters.AddWithValue("@number1", obj_comm.NumberProfileID[0]);
                        command.Parameters.AddWithValue("@number2", obj_comm.NumberProfileID[1]);
                        command.Parameters.AddWithValue("@number3", obj_comm.NumberProfileID[2]);
                        command.Parameters.AddWithValue("@number4", obj_comm.NumberProfileID[3]);
                        connection.Open();
                        int x = command.ExecuteNonQuery();
                        if (x == 0)
                        {
                            throw new Exception("Error saving communication profile");
                        }

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save Communication Profile => " + ex.Message, ex);
            }
        }

        public bool saveKeepAlive(int modem_config_id, Param_Keep_Alive_IP obj_keepAlive)
        {
            try
            {
                string cmd = "INSERT INTO [modem_keep_alive] (modem_configID, always_on_flag, wakeup_id, ping_time) VALUES (?, ?, ?, ?)";
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.AddWithValue("@modem_configID", modem_config_id);
                        command.Parameters.AddWithValue("@always_on_flag", Convert.ToInt16(obj_keepAlive.Enabled));
                        command.Parameters.AddWithValue("@wakeup_id", Convert.ToInt16(obj_keepAlive.IP_Profile_ID));
                        command.Parameters.AddWithValue("@ping_time", obj_keepAlive.Ping_time.ToString());
                        connection.Open();
                        int x = command.ExecuteNonQuery();

                        if (x == 0)
                        {
                            throw new Exception("Error saving KeepAlive settings");
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save KeepAlive => " + ex.Message, ex);
            }
        }

        public bool saveModemLimitsAndTime(int modem_config_id, Param_ModemLimitsAndTime obj_modemLimits)
        {
            try
            {
                string cmd = "INSERT INTO [modem_limits_and_time] ([modem_configID], [rssi_tcpudp_connection], [rssi_level_sms], " +
                    "[rssi_level_datacall], [retries_ip_connection], [retries_sms], [retries_tcp_data], [retries_udp_data], [retries_datacall], " +
                    "[time_bw_retries_sms], [time_bw_retries_ip_connection], [time_bw_retries_udp data], [time_bw_retries_tcpdata], " +
                    "[time_bw_retries_datacall], [time_bw_retries_alwaysoncycle], [tcp_inactivity], [timeout_cipsend]) " +
                    "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.AddWithValue("@modem_configID", modem_config_id);
                        command.Parameters.AddWithValue("@rssi_tcpudp_connection", obj_modemLimits.RSSI_LEVEL_TCP_UDP_Connection.ToString());
                        command.Parameters.AddWithValue("@rssi_level_sms", obj_modemLimits.RSSI_LEVEL_SMS.ToString());
                        command.Parameters.AddWithValue("@rssi_level_datacall", obj_modemLimits.RSSI_LEVEL_Data_Call.ToString());
                        command.Parameters.AddWithValue("@retries_ip_connection", obj_modemLimits.Retry_IP_connection.ToString());
                        command.Parameters.AddWithValue("@retries_sms", obj_modemLimits.Retry_SMS.ToString());
                        command.Parameters.AddWithValue("@retries_tcp_data", obj_modemLimits.Retry_TCP.ToString());
                        command.Parameters.AddWithValue("@retries_udp_data", obj_modemLimits.Retry_UDP.ToString());
                        command.Parameters.AddWithValue("@retries_datacall", obj_modemLimits.Retry.ToString());
                        command.Parameters.AddWithValue("@time_bw_retries_sms", obj_modemLimits.Time_between_Retries_SMS.ToString());
                        command.Parameters.AddWithValue("@time_bw_retries_ip_connection", obj_modemLimits.Time_between_Retries_IP_connection.ToString());
                        command.Parameters.AddWithValue("@time_bw_retries_udp", obj_modemLimits.Time_between_Retries_UDP.ToString());
                        command.Parameters.AddWithValue("@time_bw_retries_tcpdata", obj_modemLimits.Time_between_Retries_TCP.ToString());
                        command.Parameters.AddWithValue("@time_bw_retries_datacall", obj_modemLimits.Time_between_Retries_Data_Call.ToString());
                        command.Parameters.AddWithValue("@time_bw_retries_alwaysoncycle", obj_modemLimits.TimeRetriesAlwaysOnCycle.ToString());
                        command.Parameters.AddWithValue("@tcp_inactivity", obj_modemLimits.TCP_Inactivity.ToString());
                        command.Parameters.AddWithValue("@timeout_cipsend", obj_modemLimits.TimeOut_CipSend.ToString());
                        connection.Open();
                        int x = command.ExecuteNonQuery();

                        if (x == 0)
                        {
                            throw new Exception("Error saving modem limits and time");
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save Modem Limits and Time => " + ex.Message, ex);
            }
        }

        public bool saveModemInitialze(int modem_config_id, Param_Modem_Initialize obj_modemInitialize, Param_ModemBasics_NEW obj_newModem)
        {
            try
            {
                string cmd = "INSERT INTO [modem_initialize] (modem_configID, apn_string, user_name, [password]," +
                    "wakeup_password, pin_code, release_association_ontcpdisconnect, decremet_eventcounter, fast_disconnect)" +
                    "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.AddWithValue("@modem_configID", modem_config_id);
                        command.Parameters.AddWithValue("@apn_string", obj_modemInitialize.APN);
                        command.Parameters.AddWithValue("@user_name", obj_newModem.UserName);
                        command.Parameters.AddWithValue("@password", obj_newModem.Password);
                        command.Parameters.AddWithValue("@wakeup_password", obj_newModem.WakeupPassword);
                        command.Parameters.AddWithValue("@pin_code", obj_modemInitialize.PIN_code.ToString());
                        command.Parameters.AddWithValue("@release_association_ontcpdisconnect", Convert.ToInt16(obj_newModem.Flag_RLRQ));
                        command.Parameters.AddWithValue("@decremet_eventcounter", Convert.ToInt16(obj_newModem.Flag_DecrementCounter));
                        command.Parameters.AddWithValue("@fast_disconnect", Convert.ToInt16(obj_newModem.Flag_FastDisconnect));
                        connection.Open();
                        int x = command.ExecuteNonQuery();

                        if (x == 0)
                        {
                            throw new Exception("Error saving modem initialize settings");
                        }

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save Modem Initialize => " + ex.Message, ex);
            }
        }

        #endregion

        #region Save TBEs

        public bool saveTBEs(TBE tbe, TBE_PowerFail tbe_pf)
        {
            try
            {
                string cmd = "INSERT INTO [time_base_events] ([session_id], control, datetime_year, datetime_month, datetime_day_of_month," +
                    "datetime_day_of_week, datetime_hours, datetime_minutes, datetime_seconds, interval_timespan, flag_disable_on_powerfail)" +
                    "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        try
                        {
                            string monthString = string.Empty;
                            if (tbe.Tbe1_datetime.Month == StDateTime.LastDayOfMonth)
                            {
                                monthString = "Last Day of Month";
                            }
                            else if (tbe.Tbe1_datetime.Month == StDateTime.SecondLastDayOfMonth)
                            {
                                monthString = "Second Last Day of Month";
                            }
                            else if (tbe.Tbe1_datetime.Month == StDateTime.Null)
                            {
                                monthString = "Not Specified";
                            }
                            else
                            {
                                monthString = tbe.Tbe1_datetime.Month.ToString();
                            }

                            command.Parameters.AddWithValue("@session_id", session_id);
                            command.Parameters.AddWithValue("@control", tbe.controlString(1));
                            command.Parameters.AddWithValue("@datetime_year", (tbe.Tbe1_datetime.Year == StDateTime.NullYear) ? "Not Specified" : tbe.Tbe1_datetime.Year.ToString());
                            command.Parameters.AddWithValue("@datetime_month", monthString);
                            command.Parameters.AddWithValue("@datetime_day_of_month", (tbe.Tbe1_datetime.DayOfMonth == StDateTime.Null) ? "Not Specified" : tbe.Tbe1_datetime.DayOfMonth.ToString());
                            command.Parameters.AddWithValue("@datetime_day_of_week", (tbe.Tbe1_datetime.DayOfWeek == StDateTime.Null) ? "Not Specified" : tbe.Tbe1_datetime.DayOfWeek.ToString());

                            command.Parameters.AddWithValue("@datetime_hours", tbe.Tbe1_datetime.Hour.ToString());
                            command.Parameters.AddWithValue("@datetime_minutes", tbe.Tbe1_datetime.Minute.ToString());
                            command.Parameters.AddWithValue("@datetime_seconds", tbe.Tbe1_datetime.Second.ToString());
                            command.Parameters.AddWithValue("@interval_timespan", tbe.Tbe1_interval.ToString());
                            command.Parameters.AddWithValue("@flag_disable_on_powerfail", Convert.ToInt16(tbe_pf.disableEventAtPowerFail_TBE1));
                            connection.Open();
                            int x = command.ExecuteNonQuery();
                            if (x == 0)
                            {
                                throw new Exception("Error saving timebase events");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Can not save TBEs => " + ex.Message, ex);
                        }

                        cmd = "INSERT INTO [time_base_events] ([session_id], [control], datetime_year, datetime_month, datetime_day_of_month," +
                            "datetime_day_of_week, datetime_hours, datetime_minutes, datetime_seconds, interval_timespan, flag_disable_on_powerfail)" +
                            "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                        command.CommandText = cmd;

                        string monthString2 = string.Empty;
                        if (tbe.Tbe2_datetime.Month == StDateTime.LastDayOfMonth)
                        {
                            monthString2 = "Last Day of Month";
                        }
                        else if (tbe.Tbe2_datetime.Month == StDateTime.SecondLastDayOfMonth)
                        {
                            monthString2 = "Second Last Day of Month";
                        }
                        else if (tbe.Tbe2_datetime.Month == StDateTime.Null)
                        {
                            monthString2 = "Not Specified";
                        }
                        else
                        {
                            monthString2 = tbe.Tbe1_datetime.Month.ToString();
                        }
                        command.Parameters.AddWithValue("@session_id", session_id);
                        command.Parameters.AddWithValue("@control", tbe.controlString(2));
                        command.Parameters.AddWithValue("@datetime_year", (tbe.Tbe2_datetime.Year == StDateTime.NullYear) ? "Not Specified" : tbe.Tbe2_datetime.Year.ToString());
                        command.Parameters.AddWithValue("@datetime_month", monthString2);
                        command.Parameters.AddWithValue("@datetime_day_of_month", (tbe.Tbe2_datetime.DayOfMonth == StDateTime.Null) ? "Not Specified" : tbe.Tbe2_datetime.DayOfMonth.ToString());
                        command.Parameters.AddWithValue("@datetime_day_of_week", (tbe.Tbe2_datetime.DayOfWeek == StDateTime.Null) ? "Not Specified" : tbe.Tbe2_datetime.DayOfWeek.ToString());
                        command.Parameters.AddWithValue("@datetime_hours", tbe.Tbe2_datetime.Hour.ToString());
                        command.Parameters.AddWithValue("@datetime_minutes", tbe.Tbe2_datetime.Minute.ToString());
                        command.Parameters.AddWithValue("@datetime_seconds", tbe.Tbe2_datetime.Second.ToString());
                        command.Parameters.AddWithValue("@interval_timespan", tbe.Tbe2_interval.ToString());
                        command.Parameters.AddWithValue("@flag_disable_on_powerfail", Convert.ToInt16(tbe_pf.disableEventAtPowerFail_TBE2));

                        int y = command.ExecuteNonQuery();
                        if (y == 0)
                        {
                            throw new Exception("Error saving timebase events");
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save TBEs => " + ex.Message, ex);
            }
        }

        #endregion

        #endregion

        public static string DecimalPoint_toGUI(byte value)
        {
            string temp;
            int upper;
            int lower;
            upper = value / 16;
            lower = value % 16;
            temp = "";
            temp = temp.PadLeft(upper, '0') + "." + temp.PadRight(lower, '0');
            return temp;
        }


        #region PassKey
        //=========================================================================================
        public PasswordKey SelectPassKey(string passKey)
        {
            try
            {
                string cmd = "SELECT * FROM [PassKeys] WHERE [PassKey]=?";
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.AddWithValue("@PassKey", passKey);
                        connection.Open();
                        SQLiteDataReader dr = command.ExecuteReader();
                        PasswordKey pk = new PasswordKey();

                        if (dr.HasRows)
                        {
                            dr.Read();
                            pk.PassKey = (dr["PassKey"]).ToString();
                            pk.LoginCount = Convert.ToInt32(dr["LoginCount"]);
                            pk.LastUsedTime = Convert.ToDateTime(dr["LastUsedTime"]);
                            pk.RemainingDuration = new TimeSpan(0, 0, Convert.ToInt32(dr["RemainingDuration_Sec"]));
                            pk.ExpiryStatus = Convert.ToInt32(dr["ExpiryStatus"]);
                            return pk;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error PassKey slct => " + ex.Message, ex);
            }
        }
        //=========================================================================================
        public bool InsertPassKey(PasswordKey passKey)
        {
            try
            {
                string cmd = "INSERT INTO [PassKeys] ([PassKey], [LoginCount], [LastUsedTime], [RemainingDuration_Sec], [ExpiryStatus]) " +
                                "VALUES (?, ?, ?, ?, ?)";
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.AddWithValue("@PassKey", passKey.PassKey);
                        command.Parameters.AddWithValue("@LoginCount", passKey.LoginCount);
                        command.Parameters.AddWithValue("@LastUsedTime", passKey.LastUsedTime.ToString(date_format));
                        command.Parameters.AddWithValue("@RemainingDuration_Sec", (int)passKey.RemainingDuration.TotalSeconds);
                        command.Parameters.AddWithValue("@ExpiryStatus", passKey.ExpiryStatus);
                        connection.Open();
                        int x = command.ExecuteNonQuery();

                        if (x == 0)
                        {
                            throw new Exception("Error PassKey Insrt");
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error PassKey Insrt => " + ex.Message, ex);
            }
        }
        //=========================================================================================
        public bool UpdateRemaingTime(string passKey, TimeSpan remainingTime)
        {
            try
            {
                string cmd = "UPDATE [PassKeys] SET([RemainingDuration_Sec]=?) WHERE PassKey=?";

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.AddWithValue("@PassKey", passKey);
                        command.Parameters.AddWithValue("@RemainingDuration_Sec", (int)remainingTime.TotalSeconds);
                        connection.Open();
                        int x = command.ExecuteNonQuery();

                        if (x == 0)
                        {
                            throw new Exception("Error PassKey UpdateRemaingTime");
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error PassKey UpdateRemaingTime => " + ex.Message, ex);
            }
        }
        //=========================================================================================
        public bool UpdatePassKey(PasswordKey passKey)
        {
            try
            {
                string cmd = string.Format("UPDATE [PassKeys]  SET [LoginCount]={0}, [LastUsedTime]='{1}', [RemainingDuration_Sec]={2}, [ExpiryStatus]={3} WHERE [PassKey]='{4}'",
                    passKey.LoginCount,
                    passKey.LastUsedTime.ToString("dd/MM/yyyy HH:mm:ss"),
                    (int)passKey.RemainingDuration.TotalSeconds,
                    passKey.ExpiryStatus,
                    passKey.PassKey);

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        connection.Open();
                        int x = command.ExecuteNonQuery();

                        if (x == 0)
                        {
                            throw new Exception("Error PassKey updt");
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error PassKey updt => " + ex.Message, ex);
            }
        }
        //=========================================================================================
        #endregion
        //=========================================================================================

        #region PassKey_db
        public DbKey SelectPassKey_db()
        {
            try
            {
                string cmd = "SELECT * FROM [PassKey_db]";
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        connection.Open();
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            DbKey pk = new DbKey();

                            if (reader.HasRows)
                            {
                                reader.Read();
                                pk.SecurityKey = (reader["Comunication1"]).ToString();
                                pk.AppVersion = Convert.ToUInt32((reader["Comunication2"]).ToString());
                                return pk;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error PassKey_db slct => " + ex.Message, ex);
            }
        }
        //=========================================================================================
        public bool InsertPassKey_db(DbKey passKey)
        {
            try
            {
                string cmd = "INSERT INTO [PassKey_db] ([Comunication1], [Comunication2]) VALUES (?, ?)";
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.AddWithValue("@Comunication1", passKey.SecurityKey);
                        command.Parameters.AddWithValue("@Comunication2", passKey.AppVersion);
                        connection.Open();
                        if (command.ExecuteNonQuery() == 0)
                            throw new Exception("Error PassKey_db Insrt");
                        else
                            return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error PassKey_db Insrt => " + ex.Message, ex);
            }
        }
        //=========================================================================================
        public bool UpdatePassKey_db(DbKey passKey, uint LastSavedVersion)
        {
            try
            {
                string cmd = string.Format(@"UPDATE [PassKey_db] SET [Comunication1] = '{0}', [Comunication2] = {1} WHERE [Comunication2] = {2}",
                                            passKey.SecurityKey, passKey.AppVersion, LastSavedVersion);
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        connection.Open();
                        int rslt = command.ExecuteNonQuery();
                        if (rslt == 1)
                            return true;
                        else if (rslt > 1)
                            throw new Exception("Error more PK_db update " + rslt);
                        else
                            throw new Exception("Error PK_db update " + rslt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error PK_db => " + ex.Message, ex);
            }
        }
        #endregion

        public int SelectMeterInfoId(string meterModel)
        {
            try
            {
                string cmd = $"Select [id] FROM [meter_type_info] where Model = '{meterModel}'";
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        command.Parameters.AddWithValue("@Model", meterModel);
                        connection.Open();
                        using (SQLiteDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToInt32(dr["id"]);
                            }
                        }
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Meter id slct => " + ex.Message, ex);
            }
        }
        public Dictionary<string, int> LoadMeterModels()
        {
            try
            {
                Dictionary<string, int> meterModels = new Dictionary<string, int>();

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    string cmd = "Select * FROM [meter_type_info]";

                    using (SQLiteCommand command = new SQLiteCommand(cmd, connection))
                    {
                        connection.Open();
                        using (SQLiteDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    string model = dr["Model"].ToString();
                                    int modelId = Convert.ToInt32(dr["id"].ToString());
                                    meterModels.Add(model, modelId);
                                }
                            }
                        }
                    }
                }
                return meterModels;
            }
            catch (Exception ex)
            {
                throw new Exception("Models slct => " + ex.Message, ex);
            }
        }
    }
}
