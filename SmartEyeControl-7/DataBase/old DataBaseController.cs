using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using comm;
using Comm;
using DLMS;
using SmartEyeControl_7.comm;
using SmartEyeControl_7.Common;
using DLMS.Comm;
using DatabaseControl;
using AccurateOptocomSoftware.comm;

namespace SmartEyeControl_7.DataBase
{
    public class DataBaseController
    {
        OdbcConnection connection;
        OdbcCommand command;
        OdbcDataReader reader;
        string date_format = "yyyy-MM-dd HH:mm:ss";

        private int session_id = 0;

        public DataBaseController()
        {
            try
            {
                connection = new OdbcConnection();
                connection.ConnectionString = DBConnect.ConnectionString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User verifyUserAndGetDetails(string userName, string password)
        {
            User obj_user;
            try
            {
                OpenConnection();
                string hashPassword = string.Empty;
                using (SHA256 mySHA256 = SHA256Managed.Create())
                {
                    hashPassword = Common.App_Common.ArrayToHexString(mySHA256.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password)), true);
                }
                string cmd = String.Format("Select * from user where isActive <> 0 and user_name='{0}' and user_password='{1}'", userName, hashPassword);
                command = new OdbcCommand(cmd, connection);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    obj_user = new User();
                    obj_user.userName = (reader["user_name"]).ToString();
                    obj_user.userID = Convert.ToInt32(reader["user_ID"]);
                    obj_user.rights.rightsID = Convert.ToUInt16(reader["user_rights_id"]);
                    byte userType = Convert.ToByte(reader["user_type_id"]);
                    obj_user.userTypeID = (UserTypeID)(userType);

                    //obj_user.userTypeID = (userTypeID)(reader["user_type_id"]);
                    //obj_user.userID = Convert.ToInt32(reader["user_ID"]);

                    obj_user.RightsID_ACT34G = Convert.ToInt32(reader["rights_id_r326"]);
                    obj_user.RightsID_R411 = Convert.ToInt32(reader["rights_id_r411"]);
                    obj_user.RightsID_T421 = Convert.ToInt32(reader["rights_id_t421"]);
                    obj_user.RightsID_R421 = Convert.ToInt32(reader["rights_id_r421"]);
                    obj_user.RightsID_R283 = Convert.ToInt32(reader["rights_id_r283"]);

                    cmd = String.Format("Select * from user_rights where right_id={0}", obj_user.rights.rightsID);
                    command = new OdbcCommand(cmd, connection);
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        
                        obj_user.rights.tcp_access = Convert.ToBoolean(reader["tcp"]);
                        obj_user.rights.hdlc_access = Convert.ToBoolean(reader["hdlc"]);
                        obj_user.rights.billingData = Convert.ToBoolean(reader["billing"]);
                        obj_user.rights.instantaneousData = Convert.ToBoolean(reader["instantaneous"]);
                        obj_user.rights.loadProfileData = Convert.ToBoolean(reader["loadprofile"]);
                        obj_user.rights.eventsData = Convert.ToBoolean(reader["events"]);
                        obj_user.rights.parameters = Convert.ToBoolean(reader["parameters"]);
                    }
                    return obj_user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error exec verifyUserAndGetDetails " + ex.Message, ex);
            }
            finally
            {
                if (connection != null &&
                    connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }

        public bool addNewUser(User obj)
        {
            
            try
            {
                OpenConnection();
                uint rightsID;
                string cmd = string.Empty;
                try
                {

                    cmd = String.Format("Select right_id from user_rights where tcp={0} AND hdlc={1} AND billing={2} AND instantaneous={3} AND loadprofile={4} AND events={5} AND parameters={6}", obj.rights.tcp_access, obj.rights.hdlc_access, obj.rights.billingData, obj.rights.instantaneousData, obj.rights.loadProfileData, obj.rights.eventsData, obj.rights.parameters);

                    command = new OdbcCommand(cmd, connection);

                    reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        rightsID = Convert.ToUInt16(reader["right_id"]);
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error exec addNewUser " + ex.Message, ex);
                }

                // cmd = String.Format("Insert into user (`user_type_id`, `user_rights_id`, `user_name`, `user_password`) VALUES({0},{1},'{2}','{3}')", (byte)obj.userTypeID, rightsID, obj.userName, obj.userPassword);
                cmd = "INSERT INTO `user` (`isActive`,`user_type_id`, `user_rights_id`,`rights_id_r326`,`rights_id_r411`,`rights_id_t421`,`rights_id_r421`,`rights_id_r283`, `user_name`, `user_password`, `father_Name`, `address`, `phone_1`, `phone_2`, `mobile_No`, `fax_No`, `nID`, `employee_Code`, `creationDate`) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,NOW())";
                command = new OdbcCommand(cmd, connection);
                command.Parameters.AddWithValue("@isActive", Convert.ToByte(obj.isActive));
                command.Parameters.AddWithValue("@user_type_id", (int)(obj.userTypeID));
                command.Parameters.AddWithValue("@user_rights_id", rightsID.ToString());

                command.Parameters.AddWithValue("@rights_id_r326", obj.RightsID_ACT34G);
                command.Parameters.AddWithValue("@rights_id_r411", obj.RightsID_R411);
                command.Parameters.AddWithValue("@rights_id_t421", obj.RightsID_T421);
                command.Parameters.AddWithValue("@rights_id_r421", obj.RightsID_R421);
                command.Parameters.AddWithValue("@rights_id_r283", obj.RightsID_R283);

                command.Parameters.AddWithValue("@user_name", obj.userName);

                using (SHA256 mySHA256 = SHA256Managed.Create())
                {
                    obj.userPassword = Common.App_Common.ArrayToHexString(mySHA256.ComputeHash(ASCIIEncoding.ASCII.GetBytes(obj.userPassword)), true);
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
                //command.Parameters.AddWithValue("@creationDate", obj.creation_date.ToString("yyyy-MM-dd HH:mm:ss"));
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
            catch (Exception ex)
            {
                throw new Exception("Error exec addNewUser " + ex.Message, ex);
            }
            finally
            {
                if (connection != null &&
                    connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }

        public bool updateExistUser(User obj)
        {
            
            try
            {
                OpenConnection();
                uint rightsID;
                string cmd = string.Empty;
                try
                {

                    cmd = String.Format("Select right_id from user_rights where tcp={0} AND hdlc={1} AND billing={2} AND instantaneous={3} AND loadprofile = {4} AND events={5} AND parameters={6}", obj.rights.tcp_access, obj.rights.hdlc_access, obj.rights.billingData, obj.rights.instantaneousData, obj.rights.loadProfileData, obj.rights.eventsData, obj.rights.parameters);

                    command = new OdbcCommand(cmd, connection);

                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        rightsID = Convert.ToUInt16(reader["right_id"]);
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error exec addNewUser " + ex.Message, ex);
                }
                cmd = "UPDATE `user` SET `isActive` = ? ,`user_rights_id` = ?,`rights_id_r326` = ?, `rights_id_r411` = ?, `rights_id_t421` = ?,`rights_id_r421` = ?," +
                      "`rights_id_r283` = ?,`user_password` = ?,`father_Name` = ?,`address` = ?,`phone_1` = ?,`phone_2` = ?,`mobile_No` = ?, " +
                      "`fax_No` = ?,`nID` = ?,`employee_Code` = ? WHERE `user_ID` = ? and `user_name` = ?;";

                command = new OdbcCommand(cmd, connection);

                command.Parameters.AddWithValue("@isActive", Convert.ToByte(obj.isActive));
                command.Parameters.AddWithValue("@user_rights_id", rightsID.ToString());
                command.Parameters.AddWithValue("@rights_id_r326", obj.RightsID_ACT34G);
                command.Parameters.AddWithValue("@rights_id_r411", obj.RightsID_R411);
                command.Parameters.AddWithValue("@rights_id_t421", obj.RightsID_T421);
                command.Parameters.AddWithValue("@rights_id_r421", obj.RightsID_R421);
                command.Parameters.AddWithValue("@rights_id_r283", obj.RightsID_R283);
                using (SHA256 mySHA256 = SHA256Managed.Create())
                {
                    obj.userPassword = Common.App_Common.ArrayToHexString(mySHA256.ComputeHash(ASCIIEncoding.ASCII.GetBytes(obj.userPassword)), true);
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
                //Where Criteria
                command.Parameters.AddWithValue("@user_ID", (int)(obj.userID));
                command.Parameters.AddWithValue("@user_name", obj.userName);

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
            catch (Exception ex)
            {
                throw new Exception("Error exec updateExistUser " + ex.Message, ex);
            }
            finally
            {
                if (connection != null &&
                    connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }

        public List<string> getAllUsers()
        {
            try
            {
                List<string> result = new List<string>();
                OpenConnection();
                string cmd = "Select user_name from user";

                command = new OdbcCommand(cmd, connection);

                reader = command.ExecuteReader();

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
            catch (Exception ex)
            {
                throw new Exception("Error exec getAllUsers " + ex.Message, ex);
            }
            finally
            {
                if (connection != null &&
                    connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }

        public DataTable selectAllUsers()
        {
            try
            {
                //List<string> result = new List<string>();
                DataTable UsersInfo = new DataTable();

                OpenConnection();
                string cmd = "Select * from user";
                command = new OdbcCommand(cmd, connection);
                Load(UsersInfo, command);
                return UsersInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error exec getAllUsers " + ex.Message, ex);
            }
            finally
            {
                if (connection != null &&
                    connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }

        public bool deleteuser(string username)
        {
            try
            {
                OpenConnection();
                string cmd = String.Format("Delete from user where user_name='{0}'", username);
                command = new OdbcCommand(cmd, connection);

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
            catch (Exception ex)
            {
                throw new Exception("Error exec delete user " + ex.Message, ex);
            }
            finally
            {
                if (connection != null &&
                    connection.State != ConnectionState.Closed)
                    connection.Close();
            }

        }

        public void Load(DataTable DataSet, OdbcCommand SelectSQLCmd)
        {
            try
            {
                OdbcDataAdapter DataAdapeter =
                    new OdbcDataAdapter((OdbcCommand)SelectSQLCmd);
                //DataSet.MeterTypeInfo.Clear();
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
                OpenConnection();
                string cmd = String.Format("Select * from user where user_name='{0}'", userName);
                command = new OdbcCommand(cmd, connection);
                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    objToReturn = new User();
                    while (reader.Read())
                    {
                        byte userType = Convert.ToByte(reader["user_type_id"]);
                        objToReturn.userTypeID = (UserTypeID)(userType);

                        objToReturn.userID = Convert.ToInt32(reader["user_ID"]);
                        objToReturn.isActive = Convert.ToBoolean(reader["isActive"]);
                        objToReturn.userName = reader["user_name"].ToString();
                        objToReturn.userPassword = reader["user_password"].ToString();
                        objToReturn.rights.rightsID = Convert.ToUInt16(reader["user_rights_id"]);

                        objToReturn.RightsID_ACT34G = Convert.ToUInt16(reader["rights_id_r326"]);
                        objToReturn.RightsID_R411 = Convert.ToUInt16(reader["rights_id_r411"]);
                        objToReturn.RightsID_T421 = Convert.ToUInt16(reader["rights_id_t421"]);
                        objToReturn.RightsID_R421 = Convert.ToUInt16(reader["rights_id_r421"]);
                        objToReturn.RightsID_R283 = Convert.ToUInt16(reader["rights_id_r283"]);

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
                    }
                }
                else
                {
                    return null;
                }
                cmd = String.Format("Select * from user_rights where right_id= {0}", objToReturn.rights.rightsID);

                command = new OdbcCommand(cmd, connection);

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objToReturn.rights.tcp_access = Convert.ToBoolean(reader["tcp"]);
                        objToReturn.rights.hdlc_access = Convert.ToBoolean(reader["hdlc"]);
                        objToReturn.rights.billingData = Convert.ToBoolean(reader["billing"]);
                        objToReturn.rights.instantaneousData = Convert.ToBoolean(reader["instantaneous"]);
                        objToReturn.rights.loadProfileData = Convert.ToBoolean(reader["loadprofile"]);
                        objToReturn.rights.parameters = Convert.ToBoolean(reader["parameters"]);
                        objToReturn.rights.eventsData = Convert.ToBoolean(reader["events"]);
                    }
                }
                else
                {
                    return null;
                }
                return objToReturn;
            }
            catch (Exception ex)
            {
                throw new Exception("Error exec getUser " + ex.Message, ex);
            }
            finally
            {
                if (connection != null &&
                    connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }

        private bool OpenConnection()
        {
            try
            {
                if (connection == null)
                    return false;
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                return true; //already open
            }
        }

        public int createSession(string msn, string userID)
        {
            try
            {
                OpenConnection();
                string cmd = "INSERT INTO `session` (`msn`,`date_time`, `user_id`) VALUES (?,NOW(),?)";
                command = new OdbcCommand(cmd, connection);

                command.Parameters.AddWithValue("@msn", msn);
                command.Parameters.AddWithValue("@user_id", userID);

                command.ExecuteNonQuery();

                cmd = "Select last_insert_id() as last_id from session";
                command = new OdbcCommand(cmd, connection);
                reader = command.ExecuteReader();
                reader.Read();
                session_id = Convert.ToInt32(reader["last_id"]);
                return session_id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error exec createSession " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }
        }

        #region Access Rights

        public bool CreateNewAccessRights(string idendifier, string role, ApplicationRight rights)
        {
            try
            {

                OdbcCommand cmd = new OdbcCommand();
                cmd.CommandText = string.Format("insert into access_rights(`identifier`,`role`,`rights`) values(?,?,?)");
                cmd.Parameters.Add(new OdbcParameter("@identifier", idendifier) { OdbcType = OdbcType.VarChar });
                cmd.Parameters.Add(new OdbcParameter("@role", role) { OdbcType = OdbcType.VarChar });
                cmd.Parameters.Add(new OdbcParameter("@rights", ApplicationRight.GetBinaryObject(rights)) { OdbcType = OdbcType.Image });
                OpenConnection();
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (connection != null &&
                    connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }

        public bool UpdateAccessRights(int id, string idendifier, string role, ApplicationRight rights)
        {
            try
            {
                OdbcCommand cmd = new OdbcCommand();
                cmd.CommandText = string.Format("update access_rights set `identifier`='{0}', `role`= '{1}',`rights`=? where `id` = {2} ", idendifier, role, id);

                //cmd.Parameters.Add(new OdbcParameter("@identifier", idendifier) { OdbcType = OdbcType.VarChar });
                //cmd.Parameters.Add(new OdbcParameter("@role", role) { OdbcType = OdbcType.VarChar });

                cmd.Parameters.Add(new OdbcParameter("@rights", ApplicationRight.GetBinaryObject(rights)) { OdbcType = OdbcType.Image });
                //cmd.Parameters.Add(new OdbcParameter("@id", id) { OdbcType = OdbcType.Int });
                OpenConnection();
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (connection != null &&
                    connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }

        public bool DeleteAccessRights(int id)
        {
            try
            {

                OdbcCommand cmd = new OdbcCommand();
                cmd.CommandText = string.Format("delete from access_rights where `id`=?");
                cmd.Parameters.Add(new OdbcParameter("@id", id) { OdbcType = OdbcType.Int });
                OpenConnection();
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (connection != null &&
                    connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }

        public DataTable SelectAccessRights()
        {
            try
            {
                OdbcCommand cmd = new OdbcCommand();
                cmd.CommandText = string.Format("select * from access_rights");
                OpenConnection();
                cmd.Connection = connection;
                var tbl = new DataTable();
                tbl.Load(cmd.ExecuteReader());
                return tbl;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (connection != null && 
                    connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }

        public ApplicationRight SelectAccessRights(int id)
        {
            ApplicationRight RightObj = null;
            try
            {
                OdbcCommand cmd = new OdbcCommand();
                cmd.CommandText = string.Format("select * from access_rights where id = ?");
                cmd.Parameters.Add(new OdbcParameter("@id", id) { OdbcType = OdbcType.Int });
                OpenConnection();
                cmd.Connection = connection;
                var tbl = new DataTable();
                tbl.Load(cmd.ExecuteReader());
                var bin = (byte[])tbl.Rows[0]["rights"];
                RightObj = ApplicationRight.GetObjectFromBytes(bin);
                if (RightObj != null)
                    RightObj.RightsId = id;
                return RightObj;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (connection != null &&
                    connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }

        public void LoadApplicationAccessRights(User CurrentUser)
        {
            try
            {
                CurrentUser.AccessRights_R283 = SelectAccessRights(CurrentUser.RightsID_R283);
                CurrentUser.AccessRights_Act34G = SelectAccessRights(CurrentUser.RightsID_ACT34G);
                CurrentUser.AccessRights_R411 = SelectAccessRights(CurrentUser.RightsID_R411);
                CurrentUser.AccessRights_R421 = SelectAccessRights(CurrentUser.RightsID_R421);
                CurrentUser.AccessRights_T421 = SelectAccessRights(CurrentUser.RightsID_T421);
                //R326 Access Rights
                CurrentUser.CurrentAccessRights = CurrentUser.AccessRights_Act34G;
            }
            catch { }
        }

        #endregion

        #region BILLING

        public bool saveCumulativeBillingData(Cumulative_billing_data Data)
        {
            try
            {
                if (OpenConnection())
                {
                    OdbcParameter T = null;
                    string query = "INSERT DELAYED INTO cumm_billing_data VALUES(?,NOW(), ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                    command = new OdbcCommand(query, connection);
                    command.Prepare();
                    command.Parameters.Add(new OdbcParameter("@MSN", Data.msn));

                    T = new OdbcParameter("@MeterDateTime", Data.date);
                    T.DbType = DbType.DateTime;
                    command.Parameters.Add(T);

                    command.Parameters.Add(new OdbcParameter("@ActiveEnergy_T1", Data.activeEnergy_T1));
                    command.Parameters.Add(new OdbcParameter("@ActiveEnergy_T2", Data.activeEnergy_T2));
                    command.Parameters.Add(new OdbcParameter("@ActiveEnergy_T3", Data.activeEnergy_T3));
                    command.Parameters.Add(new OdbcParameter("@ActiveEnergy_T4", Data.activeEnergy_T4));
                    command.Parameters.Add(new OdbcParameter("@ActiveEnergy_TL", Data.activeEnergy_TL));

                    command.Parameters.Add(new OdbcParameter("@ReactiveEnergy_T1", Data.reactiveEnergy_T1));
                    command.Parameters.Add(new OdbcParameter("@ReactiveEnergy_T2", Data.reactiveEnergy_T2));
                    command.Parameters.Add(new OdbcParameter("@ReactiveEnergy_T3", Data.reactiveEnergy_T3));
                    command.Parameters.Add(new OdbcParameter("@ReactiveEnergy_T4", Data.reactiveEnergy_T4));
                    command.Parameters.Add(new OdbcParameter("@ReactiveEnergy_TL", Data.reactiveEnergy_TL));

                    command.Parameters.Add(new OdbcParameter("@ActiveMDI_T1", Data.activeMDI_T1));
                    command.Parameters.Add(new OdbcParameter("@ActiveMDI_T2", Data.activeMDI_T2));
                    command.Parameters.Add(new OdbcParameter("@ActiveMDI_T3", Data.activeMDI_T3));
                    command.Parameters.Add(new OdbcParameter("@ActiveMDI_T4", Data.activeMDI_T4));
                    command.Parameters.Add(new OdbcParameter("@ActiveMDI_TL", Data.activeMDI_TL));

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
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error exec saveCumulativeBillingData " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool saveMonthlyBillingData(Monthly_Billing_data MB_data)
        {
            try
            {
                string date_time = DateTime.Now.ToString(date_format);
                string query = "INSERT DELAYED INTO monthly_billing_data VALUES(?,?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                OpenConnection();

                command = new OdbcCommand(query, connection);
                command.Prepare();

                foreach (var Data in MB_data.monthly_billing_data)
                {
                    ///Clear Previous Parameters
                    command.Parameters.Clear();
                    OdbcParameter T = null;

                    command.Parameters.Add(new OdbcParameter("@MSN", Data.billData_obj.msn));
                    command.Parameters.Add(new OdbcParameter("@date_time", date_time));

                    T = new OdbcParameter("@MeterDateTime", Data.billData_obj.date);
                    T.DbType = DbType.DateTime;
                    command.Parameters.Add(T);

                    T = new OdbcParameter("@Counter", Data.Counter);
                    T.DbType = DbType.Int64;
                    command.Parameters.Add(T);

                    command.Parameters.Add(new OdbcParameter("@ActiveEnergy_T1", Data.billData_obj.activeEnergy_T1));
                    command.Parameters.Add(new OdbcParameter("@ActiveEnergy_T2", Data.billData_obj.activeEnergy_T2));
                    command.Parameters.Add(new OdbcParameter("@ActiveEnergy_T3", Data.billData_obj.activeEnergy_T3));
                    command.Parameters.Add(new OdbcParameter("@ActiveEnergy_T4", Data.billData_obj.activeEnergy_T4));
                    command.Parameters.Add(new OdbcParameter("@ActiveEnergy_TL", Data.billData_obj.activeEnergy_TL));

                    command.Parameters.Add(new OdbcParameter("@ReactiveEnergy_T1", Data.billData_obj.reactiveEnergy_T1));
                    command.Parameters.Add(new OdbcParameter("@ReactiveEnergy_T2", Data.billData_obj.reactiveEnergy_T2));
                    command.Parameters.Add(new OdbcParameter("@ReactiveEnergy_T3", Data.billData_obj.reactiveEnergy_T3));
                    command.Parameters.Add(new OdbcParameter("@ReactiveEnergy_T4", Data.billData_obj.reactiveEnergy_T4));
                    command.Parameters.Add(new OdbcParameter("@ReactiveEnergy_TL", Data.billData_obj.reactiveEnergy_TL));

                    command.Parameters.Add(new OdbcParameter("@ActiveMDI_T1", Data.billData_obj.activeMDI_T1));
                    command.Parameters.Add(new OdbcParameter("@ActiveMDI_T2", Data.billData_obj.activeMDI_T2));
                    command.Parameters.Add(new OdbcParameter("@ActiveMDI_T3", Data.billData_obj.activeMDI_T3));
                    command.Parameters.Add(new OdbcParameter("@ActiveMDI_T4", Data.billData_obj.activeMDI_T4));
                    command.Parameters.Add(new OdbcParameter("@ActiveMDI_TL", Data.billData_obj.activeMDI_TL));

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
                return true;
            }
            catch (Exception ex)
            {
                ///throw new Exception("Error exec saveMonthlyBillingData " + ex.Message, ex);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool saveCumulativeBillingData_SinglePhase(cumulativeBilling_SinglePhase Data)
        {
            try
            {
                connection.Open();
                string query = "INSERT DELAYED INTO cumm_billing_data_singlephase VALUES(?,NOW(), ?, ?, ?)";

                command = new OdbcCommand(query, connection);
                command.Prepare();
                command.Parameters.Add(new OdbcParameter("@MSN", Data.msn));
                command.Parameters.Add(new OdbcParameter("@MeterDateTime", OdbcType.DateTime)).Value = Data.date;

                command.Parameters.Add(new OdbcParameter("@ActiveEnergy", Data.activeEnergy));
                command.Parameters.Add(new OdbcParameter("@ActiveMDI", Data.activeMDI));

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
            catch (Exception ex)
            {
                ///throw new Exception("Error exec saveCumulativeBillingData_SinglePhase " + ex.Message, ex);
                return false;
            }
            finally
            {
                connection.Close();
            }

        }

        public bool saveMonthlyBillingData_SinglePhase(Monthly_Billing_data_SinglePhase MB_data)
        {
            try
            {
                string query = "INSERT DELAYED INTO monthly_billing_data_singlephase VALUES(?,NOW(), ?, ?, ?, ?)";
                connection.Open();

                command = new OdbcCommand(query, connection);
                command.Prepare();

                foreach (var Data in MB_data.monthly_billing_data)
                {
                    ///Clear Previous Parameters
                    command.Parameters.Clear();
                    command.Parameters.Add(new OdbcParameter("@MSN", Data.billData_obj.msn));
                    command.Parameters.Add(new OdbcParameter("@MeterDateTime", OdbcType.DateTime)).Value = Data.billData_obj.date;
                    command.Parameters.Add(new OdbcParameter("@Counter", OdbcType.BigInt)).Value = Data.Counter;

                    command.Parameters.Add(new OdbcParameter("@ActiveEnergy_T1", Data.billData_obj.activeEnergy));
                    command.Parameters.Add(new OdbcParameter("@ActiveMDI_T1", Data.billData_obj.activeMDI));


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
                command.Dispose();
                return true;
            }
            catch (Exception)
            {
                ///throw new Exception("Error exec saveMonthlyBillingData_SinglePhase " + ex.Message, ex);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion

        #region LOAD PROFILE

        public bool saveLoadProfile(Load_Profile Data, long loadProfile_GroupID)
        {
            connection.Open();
            try
            {
                string query = "INSERT DELAYED INTO load_profile_data (`msn`,`meter_date_time`, `load_profile_group_id`, `channel_1_val`, `channel_2_val`, `channel_3_val`, `channel_4_val`, `counter`, `lp_interval`) " +
                        "VALUES(?,?,?, ?, ?, ?, ?, ?, ?)";

                command = new OdbcCommand(query, connection);
                command.Prepare();
                for (int i = 0; i < Data.loadData.Count; i++)
                {
                    ///Clear Previouse Parameters
                    command.Parameters.Clear();

                    OdbcParameter T = null;

                    command.Parameters.Add(new OdbcParameter("@MSN", Data.MSN));

                    T = new OdbcParameter("@MeterDateTime", Data.loadData[i].timeStamp);
                    T.DbType = DbType.DateTime;
                    command.Parameters.Add(T);

                    T = new OdbcParameter("@GroupID", loadProfile_GroupID);
                    T.OdbcType = OdbcType.BigInt;
                    command.Parameters.Add(T);

                    command.Parameters.Add(new OdbcParameter("@Value_Channel_1", Data.loadData[i].value[0]));
                    command.Parameters.Add(new OdbcParameter("@Value_Channel_2", Data.loadData[i].value[1]));
                    command.Parameters.Add(new OdbcParameter("@Value_Channel_3", Data.loadData[i].value[2]));
                    command.Parameters.Add(new OdbcParameter("@Value_Channel_4", Data.loadData[i].value[3]));

                    T = new OdbcParameter("@Counter", Data.loadData[i].counter);
                    T.DbType = DbType.Int64;
                    command.Parameters.Add(T);

                    T = new OdbcParameter("@LP_Interval", Data.loadData[i].interval);
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
            catch (Exception ex)
            {
                ///throw new Exception("Error exec saveLoadProfile " + ex.Message, ex);
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        private int saveLoadProfileChannels(string msn, string c1, string c2, string c3, string c4)
        {
            try
            {
                OpenConnection();
                string cmd = String.Format("Insert into loadprofilegroup (msn,channel1,channel2,channel3,channel4) VALUES ('{0}','{1}','{2}','{3}','{4}')",
                    msn, c1, c2, c3, c4);
                command = new OdbcCommand(cmd, connection);
                command.ExecuteNonQuery();

                cmd = "Select last_insert_id() as last_id from loadprofilegroup";
                command = new OdbcCommand(cmd, connection);
                reader = command.ExecuteReader();
                reader.Read();
                return Convert.ToInt16(reader["last_id"]);
            }
            catch (Exception ex)
            {
                throw new Exception("Error exec saveLoadProfileChannels " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public int getLoadProfileGroupID(string c1, string c2, string c3, string c4, string msn)
        {
            try
            {
                connection.Open();
                string cmd = String.Format("Select id from loadprofilegroup where channel1='{0}' AND channel2='{1}' AND channel3='{2}' AND channel4='{3}' AND msn='{4}'",
                    c1, c2, c3, c4, msn);
                command = new OdbcCommand(cmd, connection);
                reader = command.ExecuteReader();
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
            catch (Exception ex)
            {
                throw new Exception("Error exec getLoadProfileGroupID " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion

        #region EVENTS

        public bool saveLogBook(EventData Data, string msn)
        {
            try
            {
                string query = "INSERT DELAYED INTO logbook VALUES (?, ?, ?, ?)";
                connection.Open();
                command = new OdbcCommand(query, connection);
                command.Prepare();

                OdbcParameter T = null;
                for (int i = 0; i < Data.EventRecords.Count; i++)
                {
                    ///Clear Parameters
                    command.Parameters.Clear();

                    T = new OdbcParameter("@ArrivalTime", Data.EventRecords[i].EventDateTimeStamp);
                    T.DbType = DbType.DateTime;
                    command.Parameters.Add(T);

                    command.Parameters.Add(new OdbcParameter("@MSN", msn));
                    command.Parameters.Add(new OdbcParameter("@Code", Data.EventRecords[i].EventInfo.EventCode));

                    //Command.Parameters.Add(new OdbcParameter("@Counter", Data.EventRecords[i].EventCounter));
                    T = new OdbcParameter("@Counter", Data.EventRecords[i].EventCounter);
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
            catch (Exception ex)
            {
                ///throw new Exception("Error exec saveLogBook " + ex.Message, ex);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool saveAlarmStatus(string MSN, BitArray AlarmStatus)
        {
            try
            {
                connection.Open();
                string date_time = DateTime.Now.ToString(date_format);

                string MyInsertQuery = string.Empty;
                MyInsertQuery += String.Format("INSERT INTO alarm_status VALUES('{0}', '{1}'", MSN, date_time);
                //foreach (var bit in AlarmStatus)
                //{
                //    MyInsertQuery += ", " + Convert.ToInt16(bit);
                //}
                for (int i = 1; i < AlarmStatus.Length; i++)
                {
                    MyInsertQuery += ", " + Convert.ToInt16(AlarmStatus[i]);
                }
                MyInsertQuery += ")";
                command = new OdbcCommand(MyInsertQuery, connection);
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
            catch (Exception ex)
            {
                ///throw new Exception("Error exec saveAlarmStatus " + ex.Message, ex);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool save_CautionsAndFlash(string MSN, List<Param_EventsCaution> data)
        {
            connection.Open();
            try
            {
                string date_time = DateTime.Now.ToString(date_format);
                string cmd = string.Empty;
                foreach (var item in data)
                {
                    command = new OdbcCommand();
                    command.Connection = connection;
                    command.Parameters.Clear();

                    cmd = "Insert into event_caution_flash VALUES ( ?,?,?,?,?,?,?,?)";
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
            catch (Exception ex)
            {
                throw new Exception("Error exec save_CautionsAndFlash " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }

        }

        public bool save_majorAlarm(string MSN, Param_MajorAlarmProfile data)
        {
            try
            {
                connection.Open();
                string date_time = DateTime.Now.ToString(date_format);
                string cmd = string.Empty;
                command = new OdbcCommand();

                foreach (var item in data.AlarmItems)
                {
                    command.Parameters.Clear();

                    cmd = "Insert into major_alarms VALUES( ? ,?, ?,?)";
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
            catch (Exception ex)
            {
                throw new Exception("Error exec save_majorAlarm " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool saveEventData(EventData Data, string msn)
        {
            try
            {
                string date_time = DateTime.Now.ToString(date_format);
                string query = "INSERT INTO event_data VALUES (?,?,?,?,?,?)";
                connection.Open();
                command = new OdbcCommand(query, connection);
                command.Prepare();

                OdbcParameter T = null;
                for (int i = 0; i < Data.EventRecords.Count; i++)
                {
                    ///Clear Parameters
                    command.Parameters.Clear();

                    command.Parameters.Add(new OdbcParameter("@MSN", msn));
                    command.Parameters.Add(new OdbcParameter("@date_time", date_time));


                    T = new OdbcParameter("@ArrivalTime", Data.EventRecords[i].EventDateTimeStamp);
                    T.DbType = DbType.DateTime;
                    command.Parameters.Add(T);
                    command.Parameters.Add(new OdbcParameter("@Code", Data.EventRecords[i].EventInfo.EventCode));

                    //Command.Parameters.Add(new OdbcParameter("@Counter", Data.EventRecords[i].EventCounter));
                    T = new OdbcParameter("@Counter", Data.EventRecords[i].EventCounter);
                    T.DbType = DbType.Int32;
                    command.Parameters.Add(T);

                    command.Parameters.Add(new OdbcParameter("@detailString", Data.EventRecords[i].EventDetailStr));


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
            catch (Exception ex)
            {
                ///throw new Exception("Error exec saveEventData " + ex.Message, ex);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion

        #region POWER QUALITY

        public bool SavePowerQuality_SinglePhase(Instantaneous_SinglePhase Data)
        {
            try
            {
                connection.Open();
                string query = "INSERT DELAYED INTO instantaneous_data_singlephase VALUES(?,NOW(), ?, ?, ?, ?, ?, ?)";

                command = new OdbcCommand(query, connection);
                command.Prepare();
                command.Parameters.Add(new OdbcParameter("@MSN", Data.MSN));
                command.Parameters.Add(new OdbcParameter("@MeterDateTime", OdbcType.DateTime)).Value = Data.dateTime.GetDateTime();

                command.Parameters.Add(new OdbcParameter("@Current", Data.current));
                command.Parameters.Add(new OdbcParameter("@Voltage", Data.voltage));
                command.Parameters.Add(new OdbcParameter("@InstantaneousActivePower", Data.instantaneousActivePower));
                command.Parameters.Add(new OdbcParameter("@InstantaneousPowerFactor", Data.powerFactor));
                command.Parameters.Add(new OdbcParameter("@Frequency", Data.frequency));

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
            catch (Exception ex)
            {
                ///throw new Exception("Error exec SavePowerQuality_SinglePhase " + ex.Message, ex);
                return false;
            }
            finally
            {
                connection.Close();
            }

        }

        public bool SavePowerQuality(InstantaneousObjClass Data)
        {
            try
            {
                connection.Open();
                OdbcParameter T = null;
                string query = "INSERT DELAYED INTO instantaneous_data VALUES(?,NOW(),?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                command = new OdbcCommand(query, connection);
                command.Prepare();
                command.Parameters.Add(new OdbcParameter("@MSN", Data.MSN));
                T = new OdbcParameter("@MeterDateTime", Data.dateTime.GetDateTime());
                T.DbType = DbType.DateTime;
                command.Parameters.Add(T);

                command.Parameters.Add(new OdbcParameter("@Current_PhaseA", Data.current_PhaseA));
                command.Parameters.Add(new OdbcParameter("@Current_PhaseB", Data.current_PhaseB));
                command.Parameters.Add(new OdbcParameter("@Current_PhaseC", Data.current_PhaseC));
                command.Parameters.Add(new OdbcParameter("@Current_PhaseTL", Data.current_PhaseTL));

                command.Parameters.Add(new OdbcParameter("@Voltage_PhaseA", Data.voltage_PhaseA));
                command.Parameters.Add(new OdbcParameter("@Voltage_PhaseB", Data.voltage_PhaseB));
                command.Parameters.Add(new OdbcParameter("@Voltage_PhaseC", Data.voltage_PhaseC));
                command.Parameters.Add(new OdbcParameter("@Voltage_PhaseTL", Data.voltage_PhaseTL));

                command.Parameters.Add(new OdbcParameter("@ActivePowerPositive_PhaseA", Data.active_powerPositive_PhaseA));
                command.Parameters.Add(new OdbcParameter("@ActivePowerPositive_PhaseB", Data.active_powerPositive_PhaseB));
                command.Parameters.Add(new OdbcParameter("@ActivePowerPositive_PhaseC", Data.active_powerPositive_PhaseC));
                command.Parameters.Add(new OdbcParameter("@ActivePowerPositive_PhaseTL", Data.active_powerPositive_PhaseTL));

                command.Parameters.Add(new OdbcParameter("@ActivePowerNegative_PhaseA", Data.active_powerNegative_PhaseA));
                command.Parameters.Add(new OdbcParameter("@ActivePowerNegative_PhaseB", Data.active_powerNegative_PhaseB));
                command.Parameters.Add(new OdbcParameter("@ActivePowerNegative_PhaseC", Data.active_powerNegative_PhaseC));
                command.Parameters.Add(new OdbcParameter("@ActivePowerNegative_PhaseTL", Data.active_powerNegative_PhaseTL));

                command.Parameters.Add(new OdbcParameter("@ReactivePowerPositive_PhaseA", Data.reactive_powerPositive_PhaseA));
                command.Parameters.Add(new OdbcParameter("@ReactivePowerPositive_PhaseB", Data.reactive_powerPositive_PhaseB));
                command.Parameters.Add(new OdbcParameter("@ReactivePowerPositive_PhaseC", Data.reactive_powerPositive_PhaseC));
                command.Parameters.Add(new OdbcParameter("@ReactivePowerPositive_PhaseTL", Data.reactive_powerPositive_PhaseTL));

                command.Parameters.Add(new OdbcParameter("@ReactivePowerNegative_PhaseA", Data.reactive_powerNegative_PhaseA));
                command.Parameters.Add(new OdbcParameter("@ReactivePowerNegative_PhaseB", Data.reactive_powerNegative_PhaseB));
                command.Parameters.Add(new OdbcParameter("@ReactivePowerNegative_PhaseC", Data.reactive_powerNegative_PhaseC));
                command.Parameters.Add(new OdbcParameter("@ReactivePowerNegative_PhaseTL", Data.reactive_powerNegative_PhaseTL));

                command.Parameters.Add(new OdbcParameter("@Frequency", Data.frequency));
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
            catch (Exception ex)
            {
                ///throw new Exception("Error exec SavePowerQuality " + ex.Message, ex);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion

        #region Parameterization

        #region Meter

        public bool saveLimits(Param_Limits obj)
        {
            try
            {
                OpenConnection();
                string cmd = "INSERT INTO `limits` (`session_id`,`over_volt`, `under_volt`, `imbalance_volt`, `high_neutral_current`, `reverse_energy`, `tamper_energy`, `ct_fail`, `pt_fail_amp`, `pt_fail_volt`, `overcurrentbyphase_T1`, `overcurrentbyphase_T2`, `overcurrentbyphase_T3`, `overcurrentbyphase_T4`, `overloadbyphase_T1`, `overloadbyphase_T2`, `overloadbyphase_T3`, `overloadbyphase_T4`, `overloadtotal_T1`, `overloadtotal_T2`, `overloadtotal_T3`, `overloadtotal_T4`, `mdiexceed_T1`, `mdiexceed_T2`, `mdiexceed_T3`, `mdiexceed_T4`)  VALUES( ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
                command = new OdbcCommand(cmd, connection);
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

                int x = command.ExecuteNonQuery();

                if (x == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save limits => " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool saveMonitoringTime(Param_Monitoring_time obj)
        {
            try
            {
                OpenConnection();

                string cmd = "INSERT INTO `monitoring_time` (`session_id`, `power_fail`, `phase_fail`, `over_volt`, `high_neutral_current`, `imbalance_volt`, `over_current`, `over_load`, `reverse_polarity`, `under_volt`, `reverse_energy`, `tamper_energy`, `ct_fail`, `pt_fail`, `pud_tomonitor`, `pud_for_energyrecord`, `phase_sequence`) VALUES (?,?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?,?,?)";
                command = new OdbcCommand(cmd, connection);

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

                int x = command.ExecuteNonQuery();
                if (x == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save Monitoring Time => " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool saveActivityCalendar(Param_ActivityCalendar calendar)
        {
            int cal_id = 0;
            int x = 0;
            try
            {
                #region SaveCalendarINFO
                OpenConnection();
                string cmd = "INSERT INTO `calendar` (`session_id`,`calendar_name`, `activation_date`, `day_profile_count`, `week_profile_count`, `season_profile_count`, `special_day_count`) VALUES ( ?,?, ?, ?, ?, ?, ?)";
                command = new OdbcCommand();
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

                command.ExecuteNonQuery();


                cmd = "Select last_insert_id() as last_id from calendar";
                command = new OdbcCommand(cmd, connection);
                reader = command.ExecuteReader();
                reader.Read();
                cal_id = Convert.ToInt16(reader["last_id"]);
                #endregion

                #region SAVE DAYPROFILE INFO
                foreach (DayProfile day_profile in calendar.ParamDayProfile.dayProfile_Table)
                {
                    foreach (TimeSlot slot in day_profile.dayProfile_Schedule)
                    {
                        command = new OdbcCommand();
                        command.Parameters.Clear();

                        cmd = "INSERT INTO `day_profile` (`cal_id`, `day_profile_id`, `slot_id`, `tariff`, `start_time`) VALUES (?,?,?,?,?)";
                        command.CommandText = cmd;
                        command.Connection = connection;

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
                    command = new OdbcCommand();
                    command.Parameters.Clear();

                    cmd = "INSERT INTO `week_profile` (`cal_id`, `week_profile_id`, `dp_monday`, `dp_tuesday`, `dp_wednesday`, `dp_thursday`, `dp_friday`, `dp_saturday`, `dp_sunday`) VALUES (?, ?,?, ?, ?, ?, ?, ?, ?)";
                    command.CommandText = cmd;
                    command.Connection = connection;

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
                    command = new OdbcCommand();
                    command.Parameters.Clear();

                    cmd = "INSERT INTO `season_profile` (`cal_id`, `season_profile_id`, `week_profile_id`, `startdate_month`, `startdate_date`) VALUES (?, ?, ?, ?, ?)";
                    command.CommandText = cmd;
                    command.Connection = connection;


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
                    command = new OdbcCommand();
                    command.Parameters.Clear();

                    //  cmd = "INSERT INTO `special_day` (`cal_id`, `special_day_id`, `day_profile_id`, `year`, `month`, `date`, `dayofweek`) VALUES (?, ?, ?, ?, ?, ?, ?)";
                    cmd = String.Format("INSERT INTO `special_day` (`cal_id`, `special_day_id`, `day_profile_id`, `year`, `month`, `date`, `dayofweek`) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", cal_id, special_day.SpecialDayID, special_day.DayProfile.Day_Profile_ID, special_day.StartDate.Year, special_day.StartDate.Month, special_day.StartDate.DayOfMonth, special_day.StartDate.DayOfWeek);
                    command.CommandText = cmd;
                    command.Connection = connection;


                    //command.Parameters.AddWithValue("@cal_id", cal_id);
                    //command.Parameters.AddWithValue("@special_day_id", special_day.SpecialDayID);
                    //command.Parameters.AddWithValue("@day_profile_id", special_day.DayProfile.Day_Profile_ID);
                    //command.Parameters.AddWithValue("@year", special_day.StartDate.Year);
                    //command.Parameters.AddWithValue("@month", special_day.StartDate.Month);
                    //command.Parameters.AddWithValue("@date", special_day.StartDate.DayOfMonth);
                    //command.Parameters.AddWithValue("@dayofweek", special_day.StartDate.DayOfWeek);

                    x = command.ExecuteNonQuery();

                    if (x == 0)
                    {
                        throw new Exception("Error saving Special Day Profile information");
                    }

                }

                #endregion

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save Calendar => " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool saveMDIparams(Param_MDI_parameters obj)
        {
            try
            {
                OpenConnection();
                string cmd = "INSERT INTO `mdi_params` (`session_id`,`min_time_bw_reset_manual`, `min_time_bw_reset_unit`, `manual_reset_button`, `manual_reset_remotecommand`, `manual_reset_powerdownmode`, `auto_reset_flag`, `auto_reset_date`, `mdi_interval_min`, `slides_count`) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                command = new OdbcCommand(cmd, connection);
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

                int x = command.ExecuteNonQuery();

                if (x == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save MDI => " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool saveLoadProfileChannels(List<LoadProfileChannelInfo> obj)
        {
            try
            {
                OpenConnection();
                string cmd = "INSERT INTO `param_loadprofilechannels` (`session_id`, `interval`, `channel1`, `channel2`, `channel3`, `channel4`) VALUES (?, ?, ?, ?, ?, ?)";
                command = new OdbcCommand(cmd, connection);

                int minutes = Convert.ToInt32(obj[0].CapturePeriod.TotalMinutes);

                command.Parameters.AddWithValue("@session_id", session_id);
                command.Parameters.AddWithValue("@interval", minutes);
                command.Parameters.AddWithValue("@channel1", obj[0].Quantity_Name);
                command.Parameters.AddWithValue("@channel2", obj[1].Quantity_Name);
                command.Parameters.AddWithValue("@channel3", obj[2].Quantity_Name);
                command.Parameters.AddWithValue("@channel4", obj[3].Quantity_Name);

                int x = command.ExecuteNonQuery();

                if (x == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save Load Profile Channels => " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }
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
            finally
            {
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
            finally
            {
            }
        }

        internal bool saveDisplayWindow(DisplayWindows alt, int windowtype)
        {
            OpenConnection();
            string cmd = string.Empty;
            DateTime datetime = DateTime.Now;
            try
            {
                command = new OdbcCommand(cmd, connection);
                cmd = "INSERT INTO `seac7`.`display_window`(`session_id`,`window_name`,`window_type`,`scroll_time`,`window_number`)VALUES(?,?,?,?,?)";
                command.CommandText = cmd;
                command.Prepare();

                foreach (DisplayWindowItem item in alt.Windows)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@session_id", session_id);
                    command.Parameters.AddWithValue("@window_name", item.Window_Name);
                    command.Parameters.AddWithValue("@windowtype", windowtype);
                    command.Parameters.AddWithValue("@scroll_time", Convert.ToInt32(alt.ScrollTime.Seconds));
                    ///command.Parameters.AddWithValue("@window_number",Convert.ToUInt32(item.WindowNumberToDisplay.DisplayWindowNumber));
                    OdbcParameter param = new OdbcParameter("@window_number", OdbcType.Int);
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
            catch (Exception ex)
            {
                throw new Exception("Can not save Display Windows " + ((windowtype == 0) ? "Normal" : "Alternate") + ex.Message, ex);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        internal bool saveDisplayPowerDown(Param_Display_PowerDown dispPowerDownParam)
        {
            OpenConnection();
            string cmd = string.Empty;
            DateTime datetime = DateTime.Now;
            try
            {
                command = new OdbcCommand(cmd, connection);
                cmd = "INSERT INTO `display_powerdown`(`session_id`,`Off_Delay`,`Off_Time`,`On_Time`,`Is_AlwaysOn`,`Is_ImmidateOff`,`Is_DisplayOnByButton`,`Is_DisplayRepeat`,`Is_OnTimeCycleScroll`)VALUES(?,?,?,?,?,?,?,?,?);";
                command.CommandText = cmd;
                command.Prepare();

                command.Parameters.Add("@session_id", OdbcType.Int).Value = session_id;
                command.Parameters.Add("@Off_Delay", OdbcType.TinyInt).Value = dispPowerDownParam.OffDelay;
                command.Parameters.AddWithValue("@Off_Time", OdbcType.TinyInt).Value = dispPowerDownParam.OffTime;
                command.Parameters.AddWithValue("@On_Time", OdbcType.TinyInt).Value = dispPowerDownParam.OnTime;
                command.Parameters.AddWithValue("@Is_AlwaysOn", OdbcType.Bit).Value = dispPowerDownParam.IsAlwaysOn;
                command.Parameters.AddWithValue("@Is_ImmidateOff", OdbcType.Bit).Value = dispPowerDownParam.IsImmidateOff;
                command.Parameters.AddWithValue("@Is_DisplayOnByButton", OdbcType.Bit).Value = dispPowerDownParam.IsDisplayOnByButton;
                command.Parameters.AddWithValue("@Is_DisplayRepeat", OdbcType.Bit).Value = dispPowerDownParam.IsDisplayRepeat;
                command.Parameters.AddWithValue("@Is_OnTimeCycleScroll", OdbcType.Bit).Value = dispPowerDownParam.IsOnTimeCycleScroll;

                int x = command.ExecuteNonQuery();
                if (x == 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save Display Power Down Mode Parameters" + ex.Message, ex);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool saveCTPT(Param_CTPT_ratio obj)
        {
            try
            {
                OpenConnection();
                string cmd = "INSERT INTO `ct_pt_ratio` (`session_id`, `ct_ratio_num`, `ct_ratio_denom`, `pt_ratio_num`, `pt_ratio_denom`) VALUES (?,?, ?, ?, ?)";
                command = new OdbcCommand(cmd, connection);

                command.Parameters.AddWithValue("@session_id", session_id);
                command.Parameters.AddWithValue("@ct_ratio_num", Convert.ToInt32(obj.CTratio_Numerator));
                command.Parameters.AddWithValue("@ct_ratio_denom", Convert.ToInt32(obj.CTratio_Denominator));
                command.Parameters.AddWithValue("@pt_ratio_num", Convert.ToInt32(obj.PTratio_Numerator));
                command.Parameters.AddWithValue("@pt_ratio_denom", Convert.ToInt32(obj.PTratio_Denominator));

                int x = command.ExecuteNonQuery();

                if (x == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save CT PT Ratio => " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool saveDecimalPoint(Param_decimal_point obj)
        {
            try
            {
                OpenConnection();
                string cmd = "INSERT INTO `decimal_point` (`session_id`, `billing_energy`, `billing_mdi`, `instantaneous_voltage`, `instantaneous_power`, `instantaneous_current`, `instantaneous_mdi`) VALUES (?, ?, ?, ?, ?, ?, ?)";
                command = new OdbcCommand(cmd, connection);

                command.Parameters.AddWithValue("@session_id", session_id);
                command.Parameters.AddWithValue("@billing_energy", DecimalPoint_toGUI(obj.Billing_Energy));
                command.Parameters.AddWithValue("@billing_mdi", DecimalPoint_toGUI(obj.Billing_MDI));
                command.Parameters.AddWithValue("@instantaneous_voltage", DecimalPoint_toGUI(obj.Instataneous_Voltage));
                command.Parameters.AddWithValue("@instantaneous_power", DecimalPoint_toGUI(obj.Instataneous_Power));
                command.Parameters.AddWithValue("@instantaneous_current", DecimalPoint_toGUI(obj.Instataneous_Current));
                command.Parameters.AddWithValue("@instantaneous_mdi", DecimalPoint_toGUI(obj.Instataneous_MDI));

                int x = command.ExecuteNonQuery();

                if (x == 0)
                {
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception("Can not save Decimal Point => " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool saveCustomerRef(Param_Customer_Code obj, string customerName, string customerAddress)
        {
            try
            {
                OpenConnection();
                string cmd = "INSERT INTO `customer_reference` (`session_id`, `customer_code`, `customer_name`, `customer_address`) VALUES (?,?, ?, ?)";
                command = new OdbcCommand(cmd, connection);

                command.Parameters.AddWithValue("@session_id", session_id);
                command.Parameters.AddWithValue("@customer_code", obj.Customer_Code_String);
                command.Parameters.AddWithValue("@customer_name", customerName);
                command.Parameters.AddWithValue("@customer_address", customerAddress);

                int x = command.ExecuteNonQuery();

                if (x == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save Customer Reference => " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool saveContactor(Param_Contactor obj)
        {
            try
            {
                OpenConnection();
                string cmd = "INSERT INTO `seac7`.`contactor`" +
"(`session_id`,`contactor_ON_pulsetime`,`contactor_OFF_pulsetime`,`min_time_bw_contactor_statechange`,`power_up_delay_statechange`,`interval_between_retries`,`retry_count`," +
"`control_mode`,`over_volt`,`under_volt`,`overcurrentbyphase_T1`,`overcurrentbyphase_T2`,`overcurrentbyphase_T3`,`overcurrentbyphase_T4`,`overloadbyphase_T1`,`overloadbyphase_T2`," + "`overloadbyphase_T3`,`overloadbyphase_T4`,`overloadtotal_T1`,`overloadtotal_T2`,`overloadtotal_T3`,`overloadtotal_T4`,`mdioverload_T1`,`mdioverload_T2`,`mdioverload_T3`," +
"`mdioverload_T4`)VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

                command = new OdbcCommand(cmd, connection);

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

                int x = command.ExecuteNonQuery();

                if (x == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save Contactor => " + ex.Message, ex);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool saveClock(Param_clock_caliberation obj)
        {
            try
            {
                OpenConnection();
                string cmd = "INSERT INTO `meter_datetime` (`session_id`, `meter_datetime`) VALUES (?, ?)";
                command = new OdbcCommand(cmd, connection);

                command.Parameters.AddWithValue("@session_id", session_id);
                command.Parameters.AddWithValue("@meter_datetime", obj.Set_Time);

                int x = command.ExecuteNonQuery();

                if (x == 0)
                {
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception("Can not save Clock => " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion

        #region Modem

        public int saveModem(string msn)
        {
            OpenConnection();
            try
            {
                string cmd = "INSERT INTO `modem` (`msn`,`session_id`, `date_time`) VALUES (?,?, NOW())";
                command = new OdbcCommand(cmd, connection);
                command.Parameters.AddWithValue("@msn", msn);
                command.Parameters.AddWithValue("@session_id", session_id);

                command.ExecuteNonQuery();
                cmd = "Select last_insert_id() as last_id from modem";
                command = new OdbcCommand(cmd, connection);
                reader = command.ExecuteReader();
                reader.Read();
                int modem_id = Convert.ToInt16(reader["last_id"]);
                return modem_id;
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save Modem => " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool saveIPProfile(int modem_config_id, Param_IP_Profiles[] list_obj_ip)
        {
            try
            {
                OpenConnection();
                string cmd = string.Empty;
                int x = 0;

                foreach (Param_IP_Profiles item in list_obj_ip)
                {
                    command.Parameters.Clear();

                    cmd = "INSERT INTO `modem_ip_profile` (`modem_configID`, `id`, `ip_address`, `wrapper_tcpport`, `wrapper_udpport`, `hdlc_tcpport`, `hdlc_udpport`) VALUES (?, ?, ?, ?, ?, ?, ?)";
                    command = new OdbcCommand(cmd, connection);
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
            catch (Exception ex)
            {
                throw new Exception("Can not save IP profiles => " + ex.Message, ex);
            }
        }

        public bool saveNumberProfile(int modem_config_id, Param_Number_Profile[] list_obj_numberProfile)
        {
            try
            {
                OpenConnection();
                string cmd = string.Empty;
                string number = string.Empty;
                string dataCallnumber = string.Empty;

                foreach (Param_Number_Profile item in list_obj_numberProfile)
                {
                    command.Parameters.Clear();

                    cmd = "INSERT INTO `modem_numberprofile` (`modem_configID`, `id`, `wakeup_on_voicecall`, `wakeup_on_sms`," +
                               "`wakeup_type`, `number`, `datacallnumber`, `flag_verify_password`, `flag_wakeup_on_sms`, `flag_wakeup_on_voicecall`, " +
                               "`flag_accept_parameters_sms`, `flag_allow_twoway_sms_comm`, `flag_reject_with_attend`, `flag_reject_call`, `flag_accept_datacall`) " +
                               "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?);";

                    command = new OdbcCommand(cmd, connection);

                    number = App_Common.ConvertToValidString(item.Number);
                    dataCallnumber = App_Common.ConvertToValidString(item.Datacall_Number);

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
            catch (Exception ex)
            {
                throw new Exception("Can not save Number Profile => " + ex.Message, ex);
            }
        }

        public bool saveWakeupProfile(int modem_config_id, Param_WakeUp_Profile[] list_wakeup)
        {
            try
            {
                OpenConnection();
                string cmd = string.Empty;
                foreach (Param_WakeUp_Profile item in list_wakeup)
                {
                    command.Parameters.Clear();
                    cmd = "INSERT INTO `modem_wakeupprofile` (`modem_configID`,`wakeup_profile_id`, `priroity1_ip`, `priroity2_ip`, `priroity3_ip`, `priroity4_ip`) VALUES (?, ?, ?, ?, ?,?)";
                    command = new OdbcCommand(cmd, connection);

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
            catch (Exception ex)
            {
                throw new Exception("Can not save Wakeup Profile => " + ex.Message, ex);
            }
        }

        public bool saveCommProfile(int modem_config_id, Param_Communication_Profile obj_comm)
        {
            try
            {
                OpenConnection();
                string cmd = string.Empty;
                cmd = "INSERT INTO `modem_comm_profile` (`modem_configID`, `selection_mode`, `wakeup_id`, `protocol_flag`, `transport_flag`, `number1`, `number2`, `number3`, `number4`) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";
                command = new OdbcCommand(cmd, connection);

                command.Parameters.AddWithValue("@modem_configID", modem_config_id);
                command.Parameters.AddWithValue("@selection_mode", obj_comm.SelectedMode);
                command.Parameters.AddWithValue("@wakeup_id", obj_comm.WakeUpProfileID);
                command.Parameters.AddWithValue("@protocol_flag", obj_comm.Protocol_HDLC_TCP_Flag_0);
                command.Parameters.AddWithValue("@transport_flag", obj_comm.Protocol_TCP_UDP_Flag_1);
                command.Parameters.AddWithValue("@number1", obj_comm.NumberProfileID[0]);
                command.Parameters.AddWithValue("@number2", obj_comm.NumberProfileID[1]);
                command.Parameters.AddWithValue("@number3", obj_comm.NumberProfileID[2]);
                command.Parameters.AddWithValue("@number4", obj_comm.NumberProfileID[3]);

                int x = command.ExecuteNonQuery();
                if (x == 0)
                {
                    throw new Exception("Error saving communication profile");
                }

                return true;
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
                OpenConnection();
                string cmd = "INSERT INTO `modem_keep_alive` (`modem_configID`, `always_on_flag`, `wakeup_id`, `ping_time`) VALUES (?, ?, ?, ?)";
                command = new OdbcCommand(cmd, connection);
                command.Parameters.AddWithValue("@modem_configID", modem_config_id);
                command.Parameters.AddWithValue("@always_on_flag", Convert.ToInt16(obj_keepAlive.Enabled));
                command.Parameters.AddWithValue("@wakeup_id", Convert.ToInt16(obj_keepAlive.IP_Profile_ID));
                command.Parameters.AddWithValue("@ping_time", obj_keepAlive.Ping_time.ToString());

                int x = command.ExecuteNonQuery();

                if (x == 0)
                {
                    throw new Exception("Error saving KeepAlive settings");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save KeepAlive => " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool saveModemLimitsAndTime(int modem_config_id, Param_ModemLimitsAndTime obj_modemLimits)
        {
            try
            {
                OpenConnection();
                string cmd = "INSERT INTO `modem_limits_and_time` (`modem_configID`, `rssi_tcpudp_connection`, `rssi_level_sms`, " +
                    "`rssi_level_datacall`, `retries_ip_connection`, `retries_sms`, `retries_tcp_data`, `retries_udp_data`, `retries_datacall`, " +
                    "`time_bw_retries_sms`, `time_bw_retries_ip_connection`, `time_bw_retries_udp data`, `time_bw_retries_tcpdata`, " +
                    "`time_bw_retries_datacall`, `time_bw_retries_alwaysoncycle`, `tcp_inactivity`, `timeout_cipsend`)" +
                    "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                command = new OdbcCommand(cmd, connection);

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

                int x = command.ExecuteNonQuery();

                if (x == 0)
                {
                    throw new Exception("Error saving modem limits and time");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save Modem Limits and Time => " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool saveModemInitialze(int modem_config_id, Param_Modem_Initialize obj_modemInitialize, Param_ModemBasics_NEW obj_newModem)
        {
            try
            {
                OpenConnection();
                string cmd = "INSERT INTO `modem_initialize` (`modem_configID`, `apn_string`, `user_name`, `password`," +
                    "`wakeup_password`, `pin_code`, `release_association_ontcpdisconnect`, `decremet_eventcounter`, `fast_disconnect`)" +
                    "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";

                command = new OdbcCommand(cmd, connection);
                command.Parameters.AddWithValue("@modem_configID", modem_config_id);
                command.Parameters.AddWithValue("@apn_string", obj_modemInitialize.APN);
                command.Parameters.AddWithValue("@user_name", obj_newModem.UserName);
                command.Parameters.AddWithValue("@password", obj_newModem.Password);
                command.Parameters.AddWithValue("@wakeup_password", obj_newModem.WakeupPassword);
                command.Parameters.AddWithValue("@pin_code", obj_modemInitialize.PIN_code.ToString());
                command.Parameters.AddWithValue("@release_association_ontcpdisconnect", Convert.ToInt16(obj_newModem.Flag_RLRQ));
                command.Parameters.AddWithValue("@decremet_eventcounter", Convert.ToInt16(obj_newModem.Flag_DecrementCounter));
                command.Parameters.AddWithValue("@fast_disconnect", Convert.ToInt16(obj_newModem.Flag_FastDisconnect));

                int x = command.ExecuteNonQuery();

                if (x == 0)
                {
                    throw new Exception("Error saving modem initialize settings");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save Modem Initialize => " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion

        #region Save TBEs

        public bool saveTBEs(TBE tbe, TBE_PowerFail tbe_pf)
        {
            try
            {
                OpenConnection();
                string cmd = "INSERT INTO `time_base_events` (`session_id`, `control`, `datetime_year`, `datetime_month`, `datetime_day_of_month`," +
                    "`datetime_day_of_week`, `datetime_hours`, `datetime_minutes`, `datetime_seconds`, `interval_timespan`, `flag_disable_on_powerfail`)" +
                    "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                try
                {
                    command = new OdbcCommand(cmd, connection);

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

                cmd = "INSERT INTO `time_base_events` (`session_id`, `control`, `datetime_year`, `datetime_month`, `datetime_day_of_month`," +
                    "`datetime_day_of_week`, `datetime_hours`, `datetime_minutes`, `datetime_seconds`, `interval_timespan`, `flag_disable_on_powerfail`)" +
                    "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                command = new OdbcCommand(cmd, connection);

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
            catch (Exception ex)
            {
                throw new Exception("Can not save TBEs => " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
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
    }
}
