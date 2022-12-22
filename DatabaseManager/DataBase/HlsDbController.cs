//using DLMS;
//using DLMS.Comm;
using Database;
using DLMS;
using DLMS.Comm;
using System;
using System.Data;
using System.Data.OleDb;

namespace DataBase
{
    public class HlsDbController
    {
        string ConnectionString = "";

        public HlsDbController()
        {
            try
            {
                ConnectionString = DBConnect.ConnectionString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Security_Data GetMeterHLSSetting(uint msn)
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionString))
                {
                    Security_Data objSecurityData = new Security_Data();
                    Repeat_Query: string cmd = string.Format("select * from meter_hls_info where msn = '{0}'", msn);
                    OleDbCommand command = new OleDbCommand(cmd, con);
                    con.Open();
                    OleDbDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();

                        objSecurityData.AuthenticationKey = new Key(DLMS_Common.String_to_Hex_array(dr["authentication_key"].ToString().PadLeft(32, '0')), DLMS.Comm.KEY_ID.AuthenticationKey);
                        objSecurityData.EncryptionKey = new Key(DLMS_Common.String_to_Hex_array(dr["encryption_key"].ToString().PadLeft(32, '0')), DLMS.Comm.KEY_ID.GLOBAL_Unicast_EncryptionKey);
                        objSecurityData.MasterEncryptionKey = new Key(DLMS_Common.String_to_Hex_array(dr["master_key"].ToString().PadLeft(32, '0')), DLMS.Comm.KEY_ID.MasterKey);
                        objSecurityData.SecurityControl = (SecurityControl)(Convert.ToInt32(dr["security_policy"].ToString()) * 0x10);
                        return objSecurityData;
                    }
                    else
                    /// Insert Default Security_Keys 
                    {
                        cmd = string.Format("insert into meter_hls_info(msn) values('{0}')", msn);

                        command = new OleDbCommand(cmd, con);
                        command.ExecuteNonQuery();
                        goto Repeat_Query;
                    }
                }
                //return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Loading Security Information.", ex);
            }
        }

        public uint Select_AssociationCount_by_Msn(string _msn)
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionString))
                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "smt_AssociationCount_Select_byMsn";
                    cmd.Connection = con;
                    DateTime dt = DateTime.Now;
                    cmd.Parameters.Add("@Msn", DbType.String);
                    cmd.Parameters["@Msn"].Value = _msn;
                    con.Open();
                    var result = cmd.ExecuteScalar();

                    if (result == null) return 0;
                    else return Convert.ToUInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update_AssociationCount(uint _CurrentCount, string _msn)
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionString))
                {
                    OleDbCommand cmd = new OleDbCommand();
                    con.Open();
                    uint LastCount = Select_AssociationCount_by_Msn(_msn);
                    DateTime dt = DateTime.Now;
                    cmd.Parameters.Add("@Msn", DbType.String);
                    cmd.Parameters["@Msn"].Value = _msn;
                    cmd.Parameters.Add("@Count", DbType.Int64);
                    cmd.Parameters["@Count"].Value = Convert.ToInt64(_CurrentCount);
                    cmd.Parameters.Add("@LastUpdateTime", DbType.DateTime);
                    cmd.Parameters["@LastUpdateTime"].Value = DateTime.Now;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
                    {
                        con.Open();
                    }
                    //If there is no previous Record then Insert New Record
                    if (LastCount <= 0) // If no previous record then 0 will be returned by Select_AssociationCount_by_Msn(_msn)
                    {
                        cmd.CommandText = "smt_AssociationCount_Insert";
                        return cmd.ExecuteNonQuery();
                    }
                    else //If there exists previous Record then Update it
                    {
                        //if (LastCount > 1) LogDataBaseException(new Exception(_msn + ": Last AssociationCount(" + LastCount + ")"));

                        cmd.CommandText = "smt_AssociationCount_Update_byMsn";
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception s)
            {
                //LogDataBaseException(s);
                throw new Exception(s.Message);
            }
        }

        public int Insert_Update_HLS_Key(Int64 id, string GLOBAL_Unicast_EncryptionKey, string GLOBAL_Broadcast_EncryptionKey, string AuthenticationKey, string MasterKey, string SystemTitle, bool isActive, bool isInsert)
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionString))
                {
                    OleDbCommand cmd = new OleDbCommand();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Connection = con;
                    if (isInsert)
                    {
                        cmd.CommandText = "smt_HlsKeys_Insert";
                    }
                    else
                    {
                        cmd.CommandText = "smt_HlsKeys_Update";
                        cmd.Parameters.Add("@id", DbType.Int64);
                        cmd.Parameters["@id"].Value = id;
                    }
                    cmd.Parameters.Add("@GLOBAL_Unicast_EncryptionKey", DbType.String);
                    cmd.Parameters["@GLOBAL_Unicast_EncryptionKey"].Value = GLOBAL_Unicast_EncryptionKey;
                    cmd.Parameters.Add("@GLOBAL_Broadcast_EncryptionKey", DbType.String);
                    cmd.Parameters["@GLOBAL_Broadcast_EncryptionKey"].Value = GLOBAL_Broadcast_EncryptionKey;
                    cmd.Parameters.Add("@AuthenticationKey", DbType.String);
                    cmd.Parameters["@AuthenticationKey"].Value = AuthenticationKey;
                    cmd.Parameters.Add("@MasterKey", DbType.String);
                    cmd.Parameters["@MasterKey"].Value = MasterKey;
                    cmd.Parameters.Add("@SystemTitle", DbType.String);
                    cmd.Parameters["@SystemTitle"].Value = SystemTitle;
                    cmd.Parameters.Add("@IsActive", DbType.Boolean);
                    cmd.Parameters["@IsActive"].Value = isActive;
                    con.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception s)
            {
                //LogDataBaseException(s);
                throw new Exception(s.Message);
            }
        }

        public int Delete_HLS_Key(Int64 id)
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionString))
                {
                    OleDbCommand cmd = new OleDbCommand();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.CommandText = "smt_HlsKeys_Delete";
                    cmd.Parameters.Add("@id", DbType.Int64);
                    cmd.Parameters["@id"].Value = id;
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception s)
            {
                //LogDataBaseException(s);
                throw new Exception(s.Message);
            }
        }

        public int Active_HLS_Key(Int64 id)
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionString))
                {
                    OleDbCommand cmd = new OleDbCommand();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.CommandText = "smt_HlsKeys_Active";
                    cmd.Parameters.Add("@id", DbType.Int64);
                    cmd.Parameters["@id"].Value = id;
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception s)
            {
                //LogDataBaseException(s);
                throw new Exception(s.Message);
            }
        }

        public DataTable LoadAll_HLS_Keys()
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionString))
                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * from tblSmtHlsKeys";
                    con.Open();
                    DataTable dt = new DataTable();
                    OleDbDataAdapter dAdptr = new OleDbDataAdapter(cmd);
                    dAdptr.Fill(dt);
                    return dt;
                }
            }
            catch (Exception s)
            {
                //LogDataBaseException(s);
                throw new Exception(s.Message);
            }
        }

        public DataTable Load_Active_HLS_Keys()
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionString))
                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT Top 1 * from tblSmtHlsKeys where IsActive = 1";
                    con.Open();
                    DataTable dt = new DataTable();
                    OleDbDataAdapter dAdptr = new OleDbDataAdapter(cmd);
                    dAdptr.Fill(dt);
                    return dt;
                }
            }
            catch (Exception s)
            {
                //LogDataBaseException(s);
                throw new Exception(s.Message);
            }
        }
    }
}
