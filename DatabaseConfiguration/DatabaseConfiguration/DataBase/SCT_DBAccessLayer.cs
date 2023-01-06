using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Odbc;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;
using DatabaseConfiguration.DataSet;

namespace DatabaseConfiguration.DataBase
{
    public class SCT_DBAccessLayer : SCT_DAO
    {
        public SCT_DBAccessLayer(string connectionString):base(connectionString)
        { }

        #region Load_All_Data

        public void Load_All_Configurations(ref Configs AllDataSet)
        {
            try
            {
                // For Data Loading Set Configs DataSET 
                if (AllDataSet == null)
                    AllDataSet = new Configs();
                AllDataSet.Clear();

                //UsersSCT_DAO UserSCT_DAO = new UsersSCT_DAO();
                //UserSCT_DAO.LoadUser(AllDataSet);

                // All Configuration Groups
                OBISRightsGroupSCT_DAO OBIS_RightsGroupSCT_DAO = new OBISRightsGroupSCT_DAO(this.DataBaseConnection);
                OBIS_RightsGroupSCT_DAO.LoadOBISRightsGroup(AllDataSet);

                EventsGroupSCT_DAO egroupSCT_DAO = new EventsGroupSCT_DAO(this.DataBaseConnection);
                egroupSCT_DAO.LoadEventsGroup(AllDataSet);

                BillingItemGroupSCT_DAO bgroupSCT_DAO = new BillingItemGroupSCT_DAO(this.DataBaseConnection);
                bgroupSCT_DAO.LoadBillingItemGroup(AllDataSet);

                DisplayWindowsGroupSCT_DAO dgroupSCT_DAO = new DisplayWindowsGroupSCT_DAO(this.DataBaseConnection);
                dgroupSCT_DAO.LoadDisplayWindowsGroup(AllDataSet);

                LoadProfileGroupSCT_DAO lgroupSCT_DAO = new LoadProfileGroupSCT_DAO(this.DataBaseConnection);
                lgroupSCT_DAO.LoadLoadProfileGroup(AllDataSet);

                StatusWordSCT_DAO statusWordSCT_DAO = new StatusWordSCT_DAO(this.DataBaseConnection);
                statusWordSCT_DAO.LoadStatusWord(AllDataSet);


                AllQuantitiesSCT_DAO allQuantities = new AllQuantitiesSCT_DAO(this.DataBaseConnection);
                allQuantities.Load_ObisLabels(AllDataSet);

                ManufacturerSCT_DAO manufacturerSCT_DAO = new ManufacturerSCT_DAO(this.DataBaseConnection);
                manufacturerSCT_DAO.LoadManufacturer(AllDataSet);

                AuthenticationTypeSCT_DAO authenticationTypeSCT_DAO = new AuthenticationTypeSCT_DAO(this.DataBaseConnection);
                authenticationTypeSCT_DAO.LoadAuthenticationType(AllDataSet);

                DeviceSCT_DAO deviceSCT_DAO = new DeviceSCT_DAO(this.DataBaseConnection);
                deviceSCT_DAO.LoadDevice(AllDataSet);

                DeviceAssociationSCT_DAO deviceAssociationSCT_DAO = new DeviceAssociationSCT_DAO(this.DataBaseConnection);
                deviceAssociationSCT_DAO.LoadDeviceAssociation(AllDataSet);

                OBISDetailsSCT_DAO ObisDetails = new OBISDetailsSCT_DAO(this.DataBaseConnection);
                ObisDetails.Load_ObisDetails(AllDataSet);

                // DataRelation []childRel = new DataRelation[ AllDataSet.OBIS_Rights.ChildRelations.Count];
                // AllDataSet.OBIS_Rights.ChildRelations.CopyTo(childRel, 0);
                // AllDataSet.OBIS_Rights.ChildRelations.Clear();

                // Dt = (Configs)AllDataSet.OBIS_Rights.DataSet.Copy();
                OBISRithtsSCT_DAO OBIS_RightsSCT_DAO = new OBISRithtsSCT_DAO(this.DataBaseConnection);
                OBIS_RightsSCT_DAO.Load_OBIS_Rights(AllDataSet);

                // Dt = (Configs)AllDataSet.Rights.DataSet.Copy();
                RithtsSCT_DAO RightsSCT_DAO = new RithtsSCT_DAO(this.DataBaseConnection);
                RightsSCT_DAO.Load_Rights(AllDataSet);

                ConfigurationSCT_DAO ConfigSCT_DAO = new ConfigurationSCT_DAO(this.DataBaseConnection);
                ConfigSCT_DAO.LoadMeterConfiguraion(AllDataSet);


                DisplayWindowSCT_DAO DispWinSCT_DAO = new DisplayWindowSCT_DAO(this.DataBaseConnection);
                DispWinSCT_DAO.Load_Display_Windows(AllDataSet);

                LoadProfileSCT_DAO LoadProfileSCT_DAO = new LoadProfileSCT_DAO(this.DataBaseConnection);
                LoadProfileSCT_DAO.Load_Profiles(AllDataSet);

                BillItemSCT_DAO BillSCT_DAO = new BillItemSCT_DAO(this.DataBaseConnection);
                BillSCT_DAO.Load_Billing_Items(AllDataSet);

                BillTariffSCT_DAO BillTariffSCT_DAO = new BillTariffSCT_DAO(this.DataBaseConnection);
                BillTariffSCT_DAO.Load_Billing_Tariff(AllDataSet);

                EventInfoSCT_DAO EventInfoSCT_DAO = new EventInfoSCT_DAO(this.DataBaseConnection);
                EventInfoSCT_DAO.Load_EventProfileInfo(AllDataSet);

                EventLogsSCT_DAO EventLogSCT_DAO = new EventLogsSCT_DAO(this.DataBaseConnection);
                EventLogSCT_DAO.Load_EventLogsInfo(AllDataSet);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading Meter Configurations", ex);
            }
        }

        #endregion

        #region Update Data

        public void Update_All_Configuration(Configs AllDataSet)
        {
            try
            {

                ///Update Data In DataSets
                using (IDbTransaction transection = this.DataBaseConnection.BeginTransaction(IsolationLevel.ReadCommitted))
                {

                    Configs Dt = new Configs();
                    AllDataSet.EnforceConstraints = false;
                    Dt.EnforceConstraints = false;
                    Dt = (Configs)AllDataSet.GetChanges();
                    // (Configs)AllDataSet.Meter_Info.DataSet.GetChanges();

                    EventsGroupSCT_DAO egroupSCT_DAO = new EventsGroupSCT_DAO(this.DataBaseConnection);
                    egroupSCT_DAO.Transaction = transection;
                    egroupSCT_DAO.AcceptChangesEventsGroup(Dt);

                    BillingItemGroupSCT_DAO bgroupSCT_DAO = new BillingItemGroupSCT_DAO(this.DataBaseConnection);
                    bgroupSCT_DAO.Transaction = transection;
                    bgroupSCT_DAO.AcceptChangesBillingItemGroup(Dt);

                    DisplayWindowsGroupSCT_DAO dgroupSCT_DAO = new DisplayWindowsGroupSCT_DAO(this.DataBaseConnection);
                    dgroupSCT_DAO.Transaction = transection;
                    dgroupSCT_DAO.AcceptChangesDisplayWindowsGroup(Dt);

                    LoadProfileGroupSCT_DAO lgroupSCT_DAO = new LoadProfileGroupSCT_DAO(this.DataBaseConnection);
                    lgroupSCT_DAO.Transaction = transection;
                    lgroupSCT_DAO.AcceptChangesLoadProfileGroup(Dt);

                    OBISRightsGroupSCT_DAO oBISRightsGroupSCT_DAO = new OBISRightsGroupSCT_DAO(this.DataBaseConnection);
                    oBISRightsGroupSCT_DAO.Transaction = transection;
                    oBISRightsGroupSCT_DAO.AcceptChangesOBISRightsGroup(Dt);


                    // UpdateMeterTypeInfo(Dt);
                    // Dt = (Configs)AllDataSet.Configuration.DataSet.GetChanges();
                    ConfigurationSCT_DAO ConfigSCT_DAO = new ConfigurationSCT_DAO(this.DataBaseConnection);
                    ConfigSCT_DAO.Transaction = transection;
                    ConfigSCT_DAO.AcceptChangesConfiguration(Dt);

                    StatusWordSCT_DAO statusWordSCT_DAO = new StatusWordSCT_DAO(this.DataBaseConnection);
                    statusWordSCT_DAO.Transaction = transection;
                    statusWordSCT_DAO.AcceptChangesStatusWord(Dt);

                    AllQuantitiesSCT_DAO AllQuantities = new AllQuantitiesSCT_DAO(this.DataBaseConnection);
                    AllQuantities.Transaction = transection;
                    AllQuantities.AcceptChangesAllQuantities(Dt);


                    ManufacturerSCT_DAO manufacturerSCT_DAO = new ManufacturerSCT_DAO(this.DataBaseConnection);
                    manufacturerSCT_DAO.Transaction = transection;
                    manufacturerSCT_DAO.AcceptChangesManufacturer(Dt);

                    AuthenticationTypeSCT_DAO authenticationTypeSCT_DAO = new AuthenticationTypeSCT_DAO(this.DataBaseConnection);
                    authenticationTypeSCT_DAO.Transaction = transection;
                    authenticationTypeSCT_DAO.AcceptChangesAuthenticationType(Dt);

                    DeviceSCT_DAO deviceSCT_DAO = new DeviceSCT_DAO(this.DataBaseConnection);
                    deviceSCT_DAO.Transaction = transection;
                    deviceSCT_DAO.AcceptChangesDevice(Dt);

                    DeviceAssociationSCT_DAO deviceAssociationSCT_DAO = new DeviceAssociationSCT_DAO(this.DataBaseConnection);
                    deviceAssociationSCT_DAO.Transaction = transection;
                    deviceAssociationSCT_DAO.AcceptChangesDeviceAssociation(Dt);

                    OBISDetailsSCT_DAO ObisDetails = new OBISDetailsSCT_DAO(this.DataBaseConnection);
                    ObisDetails.Transaction = transection;
                    ObisDetails.AcceptChangesObisDetails(Dt);


                    // DataRelation []childRel = new DataRelation[ AllDataSet.OBIS_Rights.ChildRelations.Count];
                    // AllDataSet.OBIS_Rights.ChildRelations.CopyTo(childRel, 0);
                    // AllDataSet.OBIS_Rights.ChildRelations.Clear();

                    // Dt = (Configs)AllDataSet.OBIS_Rights.DataSet.Copy();
                    OBISRithtsSCT_DAO OBIS_RightsSCT_DAO = new OBISRithtsSCT_DAO(this.DataBaseConnection);
                    OBIS_RightsSCT_DAO.Transaction = transection;
                    OBIS_RightsSCT_DAO.AcceptChangesOBIS_Rights(AllDataSet);

                    // Dt = (Configs)AllDataSet.Rights.DataSet.Copy();
                    RithtsSCT_DAO RightsSCT_DAO = new RithtsSCT_DAO(this.DataBaseConnection);
                    RightsSCT_DAO.Transaction = transection;
                    RightsSCT_DAO.AcceptChanges_Rights(Dt);

                    DisplayWindowSCT_DAO DispWinSCT_DAO = new DisplayWindowSCT_DAO(this.DataBaseConnection);
                    DispWinSCT_DAO.Transaction = transection;
                    DispWinSCT_DAO.AcceptChanges_Display_Windows(Dt);

                    LoadProfileSCT_DAO LoadProfileSCT_DAO = new LoadProfileSCT_DAO(this.DataBaseConnection);
                    LoadProfileSCT_DAO.Transaction = transection;
                    LoadProfileSCT_DAO.AcceptChanges_Load_Profiles(Dt);

                    BillItemSCT_DAO BillSCT_DAO = new BillItemSCT_DAO(this.DataBaseConnection);
                    BillSCT_DAO.Transaction = transection;
                    BillSCT_DAO.AcceptChanges_BillingItems(Dt);

                    BillTariffSCT_DAO BillTariffSCT_DAO = new BillTariffSCT_DAO(this.DataBaseConnection);
                    BillTariffSCT_DAO.Transaction = transection;
                    BillTariffSCT_DAO.AcceptChanges_BillingTariff(Dt);

                    EventInfoSCT_DAO EventInfoSCT_DAO = new EventInfoSCT_DAO(this.DataBaseConnection);
                    EventInfoSCT_DAO.Transaction = transection;
                    EventInfoSCT_DAO.AcceptChanges_EventProfileInfo(Dt);

                    EventLogsSCT_DAO EventLogSCT_DAO = new EventLogsSCT_DAO(this.DataBaseConnection);
                    EventLogSCT_DAO.Transaction = transection;
                    EventLogSCT_DAO.AcceptChanges_EventProfileInfo(Dt);


                    transection.Commit();
                    AllDataSet.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                if (AllDataSet != null)
                    AllDataSet.RejectChanges();

                throw new Exception("Error Updating configurations,", ex);
            }
            finally
            {
                if (AllDataSet != null)
                    AllDataSet.EnforceConstraints = true;
            }
        }

        #endregion
    }

    #region DAO_Base

    public class SCT_DAO : IDisposable
    {
        private IDbConnection DbConnection;

        private IDbTransaction transaction;

        public IDbTransaction Transaction
        {
            get { return transaction; }
            set { transaction = value; }
        }

        public IDbConnection DataBaseConnection
        {
            get { return DbConnection; }
            set { DbConnection = value; }
        }

        public string ConnectionString
        {
            set;
            get;
            //get
            //{
            //    try
            //    {
            //        string Connection = null;
            //        //Connection = ConfigurationManager.ConnectionStrings["Q_C_Check.Properties.Settings.DB_QC_CheckConnectionString"].ConnectionString;
            //        //Connection = ConfigurationManager.ConnectionStrings["Q_C_Check.Properties.Settings.DB_QC_CheckConnectionSmtPSeriesAll"].ConnectionString;
            //        //v3.2.27 Single Connection string

            //        Connection = SmartEyeAdvanceControl_7.Properties.Settings.Default.ConnectionString;

            //            //ConfigurationManager.ConnectionStrings["Q_C_Check.Properties.Settings.DB_QC_CheckConnectionString"].ConnectionString;

            //        //Connection =@"Data Source=192.168.30.181\; Initial Catalog=DB_QC_Check;Pwd= Microtech786; User id= qc_check";

            //        ///Hard Code
            //        //Connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Application_Configs\ConfigurationDB.accdb;Persist Security Info=True;Jet OLEDB:Database Password=Admin";
            //        //DBParams DB_Params = new DBParams();

            //        //MySqlConnectionStringBuilder _connectionString = new MySqlConnectionStringBuilder();
            //        //_connectionString.Server = (String)Settings.Default.Server;
            //        //_connectionString.Database = (String)Settings.Default.Database;
            //        //_connectionString.UserID = (String)Settings.Default.UserID;
            //        //_connectionString.Password = (String)Settings.Default.pswd;
            //        //_connectionString.MaximumPoolSize = Convert.ToUInt32(Settings.Default.MaxPoolSize);
            //        //_connectionString.ConnectionReset = true;
            //        //Connection =  _connectionString.ToString();


            //        //OdbcConnectionStringBuilder ConnectionString = new OdbcConnectionStringBuilder();
            //        //ConnectionString["DSN"] = "SCT7_DSN";
            //        // ConnectionString["DSN"] = Settings.Default.ConnectionString;
            //        //ConnectionString["Pooling"] = true;
            //        //ConnectionString["MinimumPoolSize"] = 50;
            //        //ConnectionString["MaximumPoolsize"] = Convert.ToUInt32(Settings.Default.MaxPoolSize);
            //        //ConnectionString["ConnectionReset"] = true;
            //        //ConnectionString["ConnectionLifeTime"] = Convert.ToUInt32(Settings.Default.ConnectionLifeTime);
            //        // Connection = ConnectionString.ConnectionString;

            //        //Local.ThreadPoolSize++;
            //        //Local.ConnectionResetTime++;
            //        ///Check Configurations Modified Here
            //        return Connection;

            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception("Error getting database connection string", ex);
            //    }
            //}
        }

        public SCT_DAO(string connectionString)
        {
            try
            {
                ConnectionString = connectionString;
                IDbConnection connection = new SqlConnection(ConnectionString);
                connection.Open();
                DbConnection = connection;
                //DbConnection = new SqlConnection(ConnectionString);
                //DbConnection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("Error building ConfigurationDB connection", ex);
            }
        }
        public SCT_DAO(IDbConnection Conn)
        {
            try
            {
                if (Conn != null && Conn.State != ConnectionState.Open)
                    Conn.Open();
                DataBaseConnection = Conn;
            }
            catch (Exception ex)
            {
                throw new Exception("Error building ConfigurationDB connection", ex);
            }
        }

        public void Update(DataTable AllDataSet, IDbCommand SelectSQLCmd)
        {
            try
            {
                using (DataContext dtContext = new DataContext(SelectSQLCmd.Connection))
                {

                    SqlDataAdapter MeterConfigurationDataAdapter =
                        new SqlDataAdapter((SqlCommand)SelectSQLCmd);
                    SqlCommandBuilder CommandBuilder = new SqlCommandBuilder(MeterConfigurationDataAdapter);
                    
                    //DataSet.MeterTypeInfo.Clear();
                    MeterConfigurationDataAdapter.UpdateCommand = (SqlCommand)CommandBuilder.GetUpdateCommand(true);
                    MeterConfigurationDataAdapter.DeleteCommand = (SqlCommand)CommandBuilder.GetDeleteCommand(true);
                    MeterConfigurationDataAdapter.InsertCommand = (SqlCommand)CommandBuilder.GetInsertCommand(true);

                    MeterConfigurationDataAdapter.Update(AllDataSet);
                    dtContext.SubmitChanges(ConflictMode.ContinueOnConflict);
                    MeterConfigurationDataAdapter.Dispose();
                    SelectSQLCmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating data", ex);
            }
        }

        public void Load(DataTable DataSet, IDbCommand SelectSQLCmd)
        {
            try
            {
                SqlDataAdapter DataAdapeter =
                    new SqlDataAdapter((SqlCommand)SelectSQLCmd);
                //DataSet.MeterTypeInfo.Clear();
                DataAdapeter.Fill(DataSet);
                DataAdapeter.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading data from data source", ex);
            }
        }

        public void LoadDataTable(DataTable dbTable, IDbCommand select)
        {
            try
            {
                select.Connection = DataBaseConnection;
                SqlDataAdapter ConfigsDataAdapeter =
                    new SqlDataAdapter((SqlCommand)select);
                //DataSet.MeterTypeInfo.Clear();
                ConfigsDataAdapeter.Fill(dbTable);
                ConfigsDataAdapeter.Dispose();
                select.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading data table", ex);
            }
        }

        //public IDbCommand GetCommand(string SqlQuery)
        //{
        //    try
        //    {
        //        OleDbCommand Command = new OleDbCommand(SqlQuery, DataBaseConnection);
        //        if (Transaction != null)
        //            Command.Transaction = Transaction;
        //        return Command;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public SqlCommand GetCommand(String SqlQuery)
        {
            SqlCommand Command = null;
            try
            {
                Command = new SqlCommand(SqlQuery, (SqlConnection)DataBaseConnection);
                if (Transaction != null)
                    Command.Transaction = (SqlTransaction)Transaction;
                return Command;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void UpdateDataTable(DataTable AllDataSet, IDbCommand SelectSQLCmd)
        {
            try
            {
                using (DataContext dtContext = new DataContext(SelectSQLCmd.Connection))
                {

                    SqlDataAdapter MeterConfigurationDataAdapter =
                        new SqlDataAdapter((SqlCommand)SelectSQLCmd);
                    SqlCommandBuilder CommandBuilder = new SqlCommandBuilder(MeterConfigurationDataAdapter);

                    //DataSet.MeterTypeInfo.Clear();
                    MeterConfigurationDataAdapter.UpdateCommand = (SqlCommand)CommandBuilder.GetUpdateCommand(true);
                    MeterConfigurationDataAdapter.DeleteCommand = (SqlCommand)CommandBuilder.GetDeleteCommand(true);
                    MeterConfigurationDataAdapter.InsertCommand = (SqlCommand)CommandBuilder.GetInsertCommand(true);
                    MeterConfigurationDataAdapter.Update(AllDataSet);
                    dtContext.SubmitChanges(ConflictMode.ContinueOnConflict);
                    MeterConfigurationDataAdapter.Dispose();
                    SelectSQLCmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating data", ex);
            }
        }

        public void close()
        {
            try
            {
                if (DataBaseConnection != null &&
                    DataBaseConnection.State == System.Data.ConnectionState.Open)
                    DataBaseConnection.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            try
            {
                close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }

    #endregion

    #region Manufacturer

    public class ManufacturerSCT_DAO : SCT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from Manufacturer order by id desc";
        public static readonly string Insert = "INSERT INTO Manufacturer (id, Manufacturer_Name, Code) VALUES (@id, @Manufacturer_Name,@Code)";
        public static readonly string Update = "UPDATE Manufacturer SET Manufacturer_Name = @Manufacturer_Name , Code = @Code "
            + "WHERE id = @id_";
        public static readonly string Delete = "DELETE FROM Manufacturer "
            + "WHERE id = @id_";

        public static readonly string IdParam = "@id";
        public static readonly string Manufacturer_Name = "@Manufacturer_Name";
        public static readonly string Code = "@Code";

        #endregion

        public ManufacturerSCT_DAO(string connectionString)
            : base(connectionString)
        { }
        public ManufacturerSCT_DAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadManufacturer(Configs DataSet)
        {
            try
            {
                LoadDataTable(DataSet.Manufacturer, GetCommand(Select));
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading Manufacturer", ex);
            }
        }

        public void UpdateManufacturer(Configs DataSet)
        {
            try
            {
                SqlCommand Select_Sql_Cmd = new SqlCommand(Select, (SqlConnection)DataBaseConnection);
                this.Update(DataSet.Manufacturer, Select_Sql_Cmd);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating Manufacturer", ex);
            }
        }

        public void AcceptChangesManufacturer(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.ManufacturerRow> ManufacturerRows = DataSet.Manufacturer.ToList<Configs.ManufacturerRow>();
                for (int index = 0; (ManufacturerRows != null && index < ManufacturerRows.Count); index++)
                {
                    Configs.ManufacturerRow dtRow = ManufacturerRows[index];
                    #region Insert_Code
                    ///Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertManufacturer(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateManufacturer(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteManufacturer(dtRow);
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating Manufacturer", ex);
            }
        }

        #region CRUD_Methods
        public void InsertManufacturer(Configs.ManufacturerRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    SqlCommand cmd = GetCommand(ManufacturerSCT_DAO.Insert);
                    cmd.Parameters.Add(new SqlParameter(ManufacturerSCT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new SqlParameter(ManufacturerSCT_DAO.Manufacturer_Name, Row.Manufacturer_Name));
                    cmd.Parameters.Add(new SqlParameter(ManufacturerSCT_DAO.Code, Row.Code));
                    //cmd.Parameters.Add(new OleDbParameter(MeterTypeInfoSCT_DAO.FirmWareVersionParam, Row.FirmWareVersion));
                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving Manufacturer data in data source", ex);
            }
        }

        public void UpdateManufacturer(Configs.ManufacturerRow Row)
        {
            try
            {
                #region Update_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    SqlCommand cmd = GetCommand(ManufacturerSCT_DAO.Update);
                    ///Reflected Changes Parameters
                    cmd.Parameters.Add(new SqlParameter(ManufacturerSCT_DAO.Manufacturer_Name, Row.Manufacturer_Name));
                    cmd.Parameters.Add(new SqlParameter(ManufacturerSCT_DAO.Code, Row.Code));

                    /// Parameters Before Modification
                    cmd.Parameters.Add(new SqlParameter(ManufacturerSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

                    int res = cmd.ExecuteNonQuery();
                    //if (res > 0)
                    //    Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying Manufacturer data in data source", ex);
            }
        }

        public void DeleteManufacturer(Configs.ManufacturerRow Row)
        {
            try
            {
                #region Delete_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Deleted)
                {
                    SqlCommand cmd = GetCommand(ManufacturerSCT_DAO.Delete);
                    cmd.Parameters.Add(new SqlParameter(ManufacturerSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting Manufacturer data in data source", ex);
            }
        }

        #endregion
    }

    #endregion

    #region Device

    public class DeviceSCT_DAO : SCT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from Device order by Device_Name asc";
        public static readonly string Insert = "INSERT INTO Device (id, Device_Name, Model, Manufacturer_Id, IsSinglePhase) VALUES (@id,@Device_Name,@Model, @Manufacturer_Id,@IsSinglePhase)";
        public static readonly string Update = "UPDATE Device SET Device_Name = @Device_Name , Model = @Model, Manufacturer_Id = @Manufacturer_Id, IsSinglePhase = @IsSinglePhase "
            + "WHERE id = @id_";
        public static readonly string Delete = "DELETE FROM Device "
            + "WHERE id = @id_";

        public static readonly string IdParam = "@id";
        public static readonly string Device_Name = "@Device_Name";
        public static readonly string Model = "@Model";
        public static readonly string Manufacturer_Id = "@Manufacturer_Id";
        public static readonly string IsSinglePhase = "@IsSinglePhase";

        #endregion

        public DeviceSCT_DAO(string connectionString)
            : base(connectionString)
        { }
        public DeviceSCT_DAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadDevice(Configs DataSet)
        {
            try
            {
                SqlDataAdapter ConfigsDataAdapeter =
                    new SqlDataAdapter(new SqlCommand(Select, (SqlConnection)DataBaseConnection));
                ConfigsDataAdapeter.Fill(DataSet.Device);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading Device", ex);
            }
        }

        public void UpdateDevice(Configs DataSet)
        {
            try
            {
                SqlCommand Select_Sql_Cmd = new SqlCommand(Select, (SqlConnection)DataBaseConnection);
                this.Update(DataSet.Device, Select_Sql_Cmd);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating Device", ex);
            }
        }

        public void AcceptChangesDevice(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.DeviceRow> DeviceRows = DataSet.Device.ToList<Configs.DeviceRow>();
                for (int index = 0; (DeviceRows != null && index < DeviceRows.Count); index++)
                {
                    Configs.DeviceRow dtRow = DeviceRows[index];
                    #region Insert_Code
                    ///Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertDevice(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateDevice(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteDevice(dtRow);
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating Device", ex);
            }
        }

        #region CRUD_Methods
        public void InsertDevice(Configs.DeviceRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    SqlCommand cmd = GetCommand(DeviceSCT_DAO.Insert);
                    cmd.Parameters.Add(new SqlParameter(DeviceSCT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new SqlParameter(DeviceSCT_DAO.Device_Name, Row.Device_Name));
                    cmd.Parameters.Add(new SqlParameter(DeviceSCT_DAO.Model, Row.Model));
                    cmd.Parameters.Add(new SqlParameter(DeviceSCT_DAO.Manufacturer_Id, Row.Manufacturer_Id));
                    cmd.Parameters.Add(new SqlParameter(DeviceSCT_DAO.IsSinglePhase, Row.IsSinglePhase));
                    //cmd.Parameters.Add(new OleDbParameter(MeterTypeInfoSCT_DAO.FirmWareVersionParam, Row.FirmWareVersion));
                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving Device data in data source", ex);
            }
        }

        public void UpdateDevice(Configs.DeviceRow Row)
        {
            try
            {
                #region Update_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    SqlCommand cmd = GetCommand(DeviceSCT_DAO.Update);
                    ///Reflected Changes Parameters
                    cmd.Parameters.Add(new SqlParameter(DeviceSCT_DAO.Device_Name, Row.Device_Name));
                    cmd.Parameters.Add(new SqlParameter(DeviceSCT_DAO.Model, Row.Model));
                    cmd.Parameters.Add(new SqlParameter(DeviceSCT_DAO.Manufacturer_Id, Row.Manufacturer_Id));
                    cmd.Parameters.Add(new SqlParameter(DeviceSCT_DAO.IsSinglePhase, Row.IsSinglePhase));
                    /// Parameters Before Modification
                    cmd.Parameters.Add(new SqlParameter(DeviceSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

                    int res = cmd.ExecuteNonQuery();
                    //if (res > 0)
                    //    Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying Device data in data source", ex);
            }
        }

        public void DeleteDevice(Configs.DeviceRow Row)
        {
            try
            {
                #region Delete_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Deleted)
                {
                    SqlCommand cmd = GetCommand(DeviceSCT_DAO.Delete);
                    /// Parameters Before Deletion
                    cmd.Parameters.Add(new SqlParameter(DeviceSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting Device data in data source", ex);
            }
        }

        #endregion
    }

    #endregion

    #region AuthenticationType

    public class AuthenticationTypeSCT_DAO : SCT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from Authentication_Type order by Authentication_Type_Name asc";
        public static readonly string Insert = "INSERT INTO Authentication_Type (id, Authentication_Type_Name) VALUES (@id, @Authentication_Type_Name)";
        public static readonly string Update = "UPDATE Authentication_Type SET Authentication_Type_Name = @Authentication_Type_Name "
            + "WHERE id = @id_";
        public static readonly string Delete = "DELETE FROM Authentication_Type "
            + "WHERE id = @id_";

        public static readonly string IdParam = "@id";
        public static readonly string Authentication_Type_Name = "@Authentication_Type_Name";

        #endregion

        public AuthenticationTypeSCT_DAO(string connectionString)
            : base(connectionString)
        { }
        public AuthenticationTypeSCT_DAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadAuthenticationType(Configs DataSet)
        {
            try
            {
                SqlDataAdapter ConfigsDataAdapeter =
                    new SqlDataAdapter(new SqlCommand(Select, (SqlConnection)DataBaseConnection));
                ConfigsDataAdapeter.Fill(DataSet.Authentication_Type);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading AuthenticationType", ex);
            }
        }

        public void UpdateAuthenticationType(Configs DataSet)
        {
            try
            {
                SqlCommand Select_Sql_Cmd = new SqlCommand(Select, (SqlConnection)DataBaseConnection);
                this.Update(DataSet.Authentication_Type, Select_Sql_Cmd);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating AuthenticationType", ex);
            }
        }

        public void AcceptChangesAuthenticationType(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.Authentication_TypeRow> Authentication_TypeRows = DataSet.Authentication_Type.ToList<Configs.Authentication_TypeRow>();
                for (int index = 0; (Authentication_TypeRows != null && index < Authentication_TypeRows.Count); index++)
                {
                    Configs.Authentication_TypeRow dtRow = Authentication_TypeRows[index];
                    #region Insert_Code
                    ///Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertAuthenticationType(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateAuthenticationType(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteAuthenticationType(dtRow);
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating AuthenticationType", ex);
            }
        }

        #region CRUD_Methods
        public void InsertAuthenticationType(Configs.Authentication_TypeRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    SqlCommand cmd = GetCommand(AuthenticationTypeSCT_DAO.Insert);
                    cmd.Parameters.Add(new SqlParameter(AuthenticationTypeSCT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new SqlParameter(AuthenticationTypeSCT_DAO.Authentication_Type_Name, Row.Authentication_Type_Name));
                    //cmd.Parameters.Add(new OleDbParameter(MeterTypeInfoSCT_DAO.FirmWareVersionParam, Row.FirmWareVersion));
                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving AuthenticationType data in data source", ex);
            }
        }

        public void UpdateAuthenticationType(Configs.Authentication_TypeRow Row)
        {
            try
            {
                #region Update_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    SqlCommand cmd = GetCommand(AuthenticationTypeSCT_DAO.Update);
                    ///Reflected Changes Parameters
                    cmd.Parameters.Add(new SqlParameter(AuthenticationTypeSCT_DAO.Authentication_Type_Name, Row.Authentication_Type_Name));
                    /// Parameters Before Updation
                    cmd.Parameters.Add(new SqlParameter(AuthenticationTypeSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    int res = cmd.ExecuteNonQuery();
                    //if (res > 0)
                    //    Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying AuthenticationType data in data source", ex);
            }
        }

        public void DeleteAuthenticationType(Configs.Authentication_TypeRow Row)
        {
            try
            {
                #region Delete_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Deleted)
                {
                    SqlCommand cmd = GetCommand(AuthenticationTypeSCT_DAO.Delete);
                    /// Parameters Before Deletion
                    cmd.Parameters.Add(new SqlParameter(AuthenticationTypeSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting AuthenticationType data in data source", ex);
            }
        }

        #endregion
    }

    #endregion

    #region DeviceAssociation

    public class DeviceAssociationSCT_DAO : SCT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from Device_Association order by Association_Name asc";
        public static readonly string Insert = "INSERT INTO Device_Association (id,Association_Name, Authentication_Type_Id, Client_Sap, Meter_Sap,Device_Id,Configuration_Id,ObisRightGroupId) VALUES (@id,@Association_Name,@Authentication_Type_Id,@Client_Sap, @Meter_Sap,@Device_Id,@Configuration_Id,@ObisRightGroupId)";
        public static readonly string Update = "UPDATE Device_Association SET Association_Name=@Association_Name, Authentication_Type_Id = @Authentication_Type_Id , Client_Sap = @Client_Sap, Meter_Sap = @Meter_Sap, Device_Id = @Device_Id, Configuration_Id = @Configuration_Id, ObisRightGroupId = @ObisRightGroupId "
                                                + "WHERE id = @id_";

        public static readonly string Delete = "DELETE FROM Device_Association "
                                                + "WHERE id = @id_";

        public static readonly string IdParam = "@id";
        public static readonly string Association_Name = "@Association_Name";
        public static readonly string Authentication_Type_Id = "@Authentication_Type_Id";
        public static readonly string Client_Sap = "@Client_Sap";
        public static readonly string Meter_Sap = "@Meter_Sap";
        public static readonly string Device_Id = "@Device_Id";
        public static readonly string Configuration_Id = "@Configuration_Id";
        public static readonly string ObisRightGroupId = "@ObisRightGroupId";

        #endregion

        public DeviceAssociationSCT_DAO(string connectionString)
            : base(connectionString)
        { }
        public DeviceAssociationSCT_DAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadDeviceAssociation(Configs DataSet)
        {
            try
            {
                SqlDataAdapter ConfigsDataAdapeter =
                    new SqlDataAdapter(new SqlCommand(Select, (SqlConnection)DataBaseConnection));
                ConfigsDataAdapeter.Fill(DataSet.Device_Association);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading DeviceAssociation", ex);
            }
        }

        public void UpdateDeviceAssociation(Configs DataSet)
        {
            try
            {
                SqlCommand Select_Sql_Cmd = new SqlCommand(Select, (SqlConnection)DataBaseConnection);
                this.Update(DataSet.Device_Association, Select_Sql_Cmd);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating DeviceAssociation", ex);
            }
        }

        public void AcceptChangesDeviceAssociation(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.Device_AssociationRow> DeviceAssociationRows = DataSet.Device_Association.ToList<Configs.Device_AssociationRow>();
                for (int index = 0; (DeviceAssociationRows != null && index < DeviceAssociationRows.Count); index++)
                {
                    Configs.Device_AssociationRow dtRow = DeviceAssociationRows[index];
                    #region Insert_Code
                    ///Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertDeviceAssociation(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateDeviceAssociation(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteDeviceAssociation(dtRow);
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating DeviceAssociation", ex);
            }
        }

        #region CRUD_Methods

        public void InsertDeviceAssociation(Configs.Device_AssociationRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    SqlCommand cmd = GetCommand(DeviceAssociationSCT_DAO.Insert);
                    cmd.Parameters.Add(new SqlParameter(DeviceAssociationSCT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new SqlParameter(DeviceAssociationSCT_DAO.Association_Name, Row.Association_Name));
                    cmd.Parameters.Add(new SqlParameter(DeviceAssociationSCT_DAO.Authentication_Type_Id, Row.Authentication_Type_Id));
                    cmd.Parameters.Add(new SqlParameter(DeviceAssociationSCT_DAO.Client_Sap, Row.Client_Sap));
                    cmd.Parameters.Add(new SqlParameter(DeviceAssociationSCT_DAO.Meter_Sap, Row.Meter_Sap));
                    cmd.Parameters.Add(new SqlParameter(DeviceAssociationSCT_DAO.Device_Id, Row.Device_Id));
                    cmd.Parameters.Add(new SqlParameter(DeviceAssociationSCT_DAO.Configuration_Id, Row.Configuration_Id));
                    cmd.Parameters.Add(new SqlParameter(DeviceAssociationSCT_DAO.ObisRightGroupId, Row.ObisRightGroupId));
                    //cmd.Parameters.Add(new OleDbParameter(MeterTypeInfoSCT_DAO.FirmWareVersionParam, Row.FirmWareVersion));
                    int res = cmd.ExecuteNonQuery();

                    //Row.AcceptChanges();
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving DeviceAssociationSCT_DAO data in data source", ex);
            }
        }

        public void UpdateDeviceAssociation(Configs.Device_AssociationRow Row)
        {
            try
            {
                #region Update_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    SqlCommand cmd = GetCommand(DeviceAssociationSCT_DAO.Update);
                    ///Reflected Changes Parameters
                    cmd.Parameters.Add(new SqlParameter(DeviceAssociationSCT_DAO.Association_Name, Row.Association_Name));
                    cmd.Parameters.Add(new SqlParameter(DeviceAssociationSCT_DAO.Authentication_Type_Id, Row.Authentication_Type_Id));
                    cmd.Parameters.Add(new SqlParameter(DeviceAssociationSCT_DAO.Client_Sap, Row.Client_Sap));
                    cmd.Parameters.Add(new SqlParameter(DeviceAssociationSCT_DAO.Meter_Sap, Row.Meter_Sap));
                    cmd.Parameters.Add(new SqlParameter(DeviceAssociationSCT_DAO.Device_Id, Row.Device_Id));
                    cmd.Parameters.Add(new SqlParameter(DeviceAssociationSCT_DAO.Configuration_Id, Row.Configuration_Id));
                    cmd.Parameters.Add(new SqlParameter(DeviceAssociationSCT_DAO.ObisRightGroupId, Row.ObisRightGroupId));

                    /// Parameters Before Updation
                    cmd.Parameters.Add(new SqlParameter(DeviceAssociationSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    int res = cmd.ExecuteNonQuery();
                    //if (res > 0)
                    //    Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying DeviceAssociation data in data source", ex);
            }
        }

        public void DeleteDeviceAssociation(Configs.Device_AssociationRow Row)
        {
            try
            {
                #region Delete_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Deleted)
                {
                    SqlCommand cmd = GetCommand(DeviceAssociationSCT_DAO.Delete);
                    /// Parameters Before Deletion
                    cmd.Parameters.Add(new SqlParameter(DeviceAssociationSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting DeviceAssociation data in data source", ex);
            }
        }

        #endregion
    }

    #endregion

    #region Configuration

    public class ConfigurationSCT_DAO : SCT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from Configuration_New order by Name desc";

        public static readonly string Insert = "INSERT INTO Configuration_New (id, Name, lp_channels_group_id, BillItemsGroupId, DisplayWindowGroupId, EventGroupId)" +
                                        "VALUES (@id, @name, @lp_channels_group_id, @BillItemsGroupId , @DisplayWindowGroupId, @EventGroupId)";

        public static readonly string Update = "UPDATE Configuration_New SET Name = @name, lp_channels_group_id = @lp_channels_group_id, " + " BillItemsGroupId = @BillItemsGroupId, DisplayWindowGroupId = @DisplayWindowGroupId, EventGroupId = @EventGroupId " +
                         "WHERE ((id = @id_) AND ((Name IS NULL) OR (Name = @name_)) AND ((lp_channels_group_id IS NULL) OR " +
                          "(lp_channels_group_id = @lp_channels_group_id_)) AND ((BillItemsGroupId IS NULL) OR (BillItemsGroupId = @BillItemsGroupId_)) AND " +
                          "((DisplayWindowGroupId IS NULL) OR (DisplayWindowGroupId = @DisplayWindowGroupId_)) AND ((EventGroupId IS NULL) OR (EventGroupId = @EventGroupId_)))";

        public static readonly string Delete = "DELETE FROM Configuration_New " +
                             "WHERE ((id = @id_) AND ((Name IS NULL) OR (Name = @name_)) AND ((lp_channels_group_id IS NULL) OR " +
                          "(lp_channels_group_id = @lp_channels_group_id_)) AND ((BillItemsGroupId IS NULL) OR (BillItemsGroupId = @BillItemsGroupId_)) AND " +
                          "((DisplayWindowGroupId IS NULL) OR (DisplayWindowGroupId = @DisplayWindowGroupId_)) AND ((EventGroupId IS NULL) OR (EventGroupId = @EventGroupId_)))";

        public static readonly string IdParam = "@id";
        public static readonly string NameParam = "@name";
        public static readonly string LpChannelsGroupId = "@lp_channels_group_id";
        public static readonly string BillItemsGroupId = "@BillItemsGroupId";
        public static readonly string DisplayWindowGroupId = "@DisplayWindowGroupId";
        public static readonly string EventGroupId = "@EventGroupId";

        #endregion

        public ConfigurationSCT_DAO(string connectionString)
            : base(connectionString)
        { }
        public ConfigurationSCT_DAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadMeterConfiguraion(Configs DataTable)
        {
            try
            {
                LoadDataTable(DataTable.Configuration, GetCommand(Select));
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading Meter Configuration", ex);
            }
        }

        public void UpdateMeterConfigurations(Configs AllDataSet)
        {
            try
            {
                this.Update(AllDataSet.Configuration, GetCommand(Select));
                //DataSet.MeterTypeInfo.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating Meter Configuration Info", ex);
            }
        }

        public void AcceptChangesConfiguration(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.ConfigurationRow> ConfigurationRow = DataSet.Configuration.ToList<Configs.ConfigurationRow>();
                for (int index = 0; (ConfigurationRow != null && index < ConfigurationRow.Count); index++)
                {
                    Configs.ConfigurationRow dtRow = ConfigurationRow[index];
                    #region Insert_Code
                    ///Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertConfiguration(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateConfiguration(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteConfiguration(dtRow);
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating Configuration", ex);
            }
        }

        #region CRUD_Method

        public void InsertConfiguration(Configs.ConfigurationRow Row)
        {
            IDbCommand cmd = null;
            try
            {
                #region Insert_Code
                /// Exec Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    cmd = GetCommand(ConfigurationSCT_DAO.Insert);
                    cmd.Parameters.Add(new SqlParameter(ConfigurationSCT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new SqlParameter(ConfigurationSCT_DAO.NameParam, Row.Name));

                    cmd.Parameters.Add(new SqlParameter(ConfigurationSCT_DAO.LpChannelsGroupId, SqlDbType.BigInt) { Value = (Row.Islp_channels_group_idNull()) ? DBNull.Value : (object)Row.lp_channels_group_id });

                    cmd.Parameters.Add(new SqlParameter(ConfigurationSCT_DAO.BillItemsGroupId, SqlDbType.BigInt) { Value = (Row.IsBillItemsGroupIdNull()) ? DBNull.Value : (object)Row.BillItemsGroupId });

                    cmd.Parameters.Add(new SqlParameter(ConfigurationSCT_DAO.DisplayWindowGroupId, SqlDbType.BigInt) { Value = (Row.IsDisplayWindowGroupIdNull()) ? DBNull.Value : (object)Row.DisplayWindowGroupId });

                    cmd.Parameters.Add(new SqlParameter(ConfigurationSCT_DAO.EventGroupId, SqlDbType.BigInt) { Value = (Row.IsEventGroupIdNull()) ? DBNull.Value : (object)Row.EventGroupId });


                    cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving Configurations data in data source", ex);
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
        }

        public void UpdateConfiguration(Configs.ConfigurationRow Row)
        {
            IDbCommand cmd = null;
            try
            {
                #region Update_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    cmd = GetCommand(ConfigurationSCT_DAO.Update);
                    ///Parameters Reflecting Changes

                    cmd.Parameters.Add(new SqlParameter(ConfigurationSCT_DAO.NameParam, Row.Name));

                    cmd.Parameters.Add(new SqlParameter(ConfigurationSCT_DAO.LpChannelsGroupId, SqlDbType.BigInt) { Value = (Row.Islp_channels_group_idNull()) ? DBNull.Value : (object)Row.lp_channels_group_id });

                    cmd.Parameters.Add(new SqlParameter(ConfigurationSCT_DAO.BillItemsGroupId, SqlDbType.BigInt) { Value = (Row.IsBillItemsGroupIdNull()) ? DBNull.Value : (object)Row.BillItemsGroupId });

                    cmd.Parameters.Add(new SqlParameter(ConfigurationSCT_DAO.DisplayWindowGroupId, SqlDbType.BigInt) { Value = (Row.IsDisplayWindowGroupIdNull()) ? DBNull.Value : (object)Row.DisplayWindowGroupId });


                    cmd.Parameters.Add(new SqlParameter(ConfigurationSCT_DAO.EventGroupId, SqlDbType.BigInt) { Value = (Row.IsEventGroupIdNull()) ? DBNull.Value : (object)Row.EventGroupId });


                    /// Parameters Before Modification
                    cmd.Parameters.Add(new SqlParameter(ConfigurationSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(ConfigurationSCT_DAO.NameParam + "_", Convert.ToString(Row["Name", DataRowVersion.Original])));


                    cmd.Parameters.Add(new SqlParameter(ConfigurationSCT_DAO.LpChannelsGroupId + "_", SqlDbType.BigInt)
                    {
                        Value = ((Row["lp_channels_group_id", DataRowVersion.Original]) == DBNull.Value) ? DBNull.Value :
                               (object)Convert.ToInt32(Row["lp_channels_group_id", DataRowVersion.Original])
                    });


                    cmd.Parameters.Add(new SqlParameter(ConfigurationSCT_DAO.BillItemsGroupId + "_", SqlDbType.BigInt)
                    {
                        Value = ((Row["BillItemsGroupId", DataRowVersion.Original]) == DBNull.Value) ? DBNull.Value :
                                 (object)Convert.ToInt32(Row["BillItemsGroupId", DataRowVersion.Original])
                    });

                    cmd.Parameters.Add(new SqlParameter(ConfigurationSCT_DAO.DisplayWindowGroupId + "_", SqlDbType.BigInt)
                    {
                        Value = ((Row["DisplayWindowGroupId", DataRowVersion.Original]) == DBNull.Value) ? DBNull.Value :
                                 (object)Convert.ToInt32(Row["DisplayWindowGroupId", DataRowVersion.Original])
                    });


                    cmd.Parameters.Add(new SqlParameter(ConfigurationSCT_DAO.EventGroupId + "_", SqlDbType.BigInt)
                    {
                        Value = ((Row["EventGroupId", DataRowVersion.Original]) == DBNull.Value) ? DBNull.Value :
                                 (object)Convert.ToInt32(Row["EventGroupId", DataRowVersion.Original])
                    });


                    int res = cmd.ExecuteNonQuery();
                    //if(res > 0)
                    //    Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating Configurations data in data source", ex);
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }

        }

        public void DeleteConfiguration(Configs.ConfigurationRow Row)
        {
            IDbCommand cmd = null;
            try
            {
                #region Delete_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Deleted)
                {
                    cmd = GetCommand(ConfigurationSCT_DAO.Delete);
                    /// Parameters Before Modification
                    cmd.Parameters.Add(new SqlParameter(ConfigurationSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(ConfigurationSCT_DAO.NameParam + "_", Convert.ToString(Row["Name", DataRowVersion.Original])));

                    cmd.Parameters.Add(new SqlParameter(ConfigurationSCT_DAO.LpChannelsGroupId + "_", SqlDbType.BigInt)
                    {
                        Value = ((Row["lp_channels_group_id", DataRowVersion.Original]) == DBNull.Value) ? DBNull.Value :
                               (object)Convert.ToInt32(Row["lp_channels_group_id", DataRowVersion.Original])
                    });


                    cmd.Parameters.Add(new SqlParameter(ConfigurationSCT_DAO.BillItemsGroupId + "_", SqlDbType.BigInt)
                    {
                        Value = ((Row["BillItemsGroupId", DataRowVersion.Original]) == DBNull.Value) ? DBNull.Value :
                                 (object)Convert.ToInt32(Row["BillItemsGroupId", DataRowVersion.Original])
                    });

                    cmd.Parameters.Add(new SqlParameter(ConfigurationSCT_DAO.DisplayWindowGroupId + "_", SqlDbType.BigInt)
                    {
                        Value = ((Row["DisplayWindowGroupId", DataRowVersion.Original]) == DBNull.Value) ? DBNull.Value :
                                 (object)Convert.ToInt32(Row["DisplayWindowGroupId", DataRowVersion.Original])
                    });


                    cmd.Parameters.Add(new SqlParameter(ConfigurationSCT_DAO.EventGroupId + "_", SqlDbType.BigInt)
                    {
                        Value = ((Row["EventGroupId", DataRowVersion.Original]) == DBNull.Value) ? DBNull.Value :
                                 (object)Convert.ToInt32(Row["EventGroupId", DataRowVersion.Original])
                    });


                    cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting Configuration data in data source", ex);
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }

        }

        #endregion
    }

    #endregion

    #region All Quantities

    public class AllQuantitiesSCT_DAO : SCT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from All_Quantities order by OBIS_Index";
        public static readonly string Insert = "INSERT INTO All_Quantities (OBIS_Index, Label) " +
                                      "VALUES (@OBIS_Index, @Label)";

        public static readonly string Update = "UPDATE All_Quantities SET OBIS_Index = @OBIS_Index, Label = @Label " +
             "WHERE OBIS_Index = @OBIS_Index_";

        public static readonly string Delete = "DELETE FROM All_Quantities " +
                                                "WHERE OBIS_Index = @OBIS_Index_";


        public static readonly string OBIS_Index = "@OBIS_Index";
        public static readonly string Label = "@Label";

        #endregion

        public AllQuantitiesSCT_DAO(string connectionString)
            : base(connectionString)
        {

        }
        public AllQuantitiesSCT_DAO(IDbConnection connection)
            : base(connection)
        {

        }

        public void Load_ObisLabels(Configs DataSet)
        {
            try
            {
                SqlDataAdapter OBIS_Rights_DataAdapeter =
                    new SqlDataAdapter(new SqlCommand(Select, (SqlConnection)DataBaseConnection));
                //DataSet.MeterTypeInfo.Clear();
                OBIS_Rights_DataAdapeter.Fill(DataSet.AllQuantities);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading Obis Labels info", ex);
            }
        }

        public void AcceptChangesAllQuantities(Configs DataSet)
        {
            Configs.AllQuantitiesRow dtRow = null;
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.AllQuantitiesRow> OBIS_Rights_List = DataSet.AllQuantities.ToList<Configs.AllQuantitiesRow>();
                for (int index = 0; (OBIS_Rights_List != null && index < OBIS_Rights_List.Count); index++)
                {
                    dtRow = OBIS_Rights_List[index];
                    #region Insert_Code

                    // Exec Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertAllQuantities(dtRow);
                    }

                    #endregion
                    #region Update_Code

                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateAllQuantities(dtRow);
                    }

                    #endregion
                    #region Delete_Code

                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteAllQuantities(dtRow);
                    }

                    #endregion
                }

            }
            catch (Exception ex)
            {
                //string msg = null;
                //if (dtRow != null)
                //{
                //    StOBISCode dt = (DLMS.Get_Index)dtRow.OBIS_Index;
                //    msg = dt.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                //}
                throw new Exception(String.Format("Error Updating OBIS Rights {0}", ex));
            }
        }

        #region CRUD_Method

        public void InsertAllQuantities(Configs.AllQuantitiesRow Row)
        {
            try
            {
                #region Insert_Code

                // Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    SqlCommand cmd = GetCommand(AllQuantitiesSCT_DAO.Insert);
                    cmd.Parameters.Add(new SqlParameter(AllQuantitiesSCT_DAO.OBIS_Index, Row.OBIS_Index));
                    cmd.Parameters.Add(new SqlParameter(AllQuantitiesSCT_DAO.Label, Row.Label));
                    cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();

                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving All Quantities data in data source", ex);
            }
        }

        public void UpdateAllQuantities(Configs.AllQuantitiesRow Row)
        {
            try
            {
                #region Update_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    SqlCommand cmd = GetCommand(AllQuantitiesSCT_DAO.Update);
                    // Parameters Reflecting Modification
                    cmd.Parameters.Add(new SqlParameter(AllQuantitiesSCT_DAO.OBIS_Index, Row.OBIS_Index));
                    cmd.Parameters.Add(new SqlParameter(AllQuantitiesSCT_DAO.Label, Row.Label));
                    // Parameters Before Modification
                    cmd.Parameters.Add(new SqlParameter(AllQuantitiesSCT_DAO.OBIS_Index + "_", Convert.ToUInt64(Row["OBIS_Index", DataRowVersion.Original])));
                    cmd.ExecuteNonQuery();
                    // Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating ObisDetails data in data source", ex);
            }
        }

        public void DeleteAllQuantities(Configs.AllQuantitiesRow Row)
        {
            try
            {
                #region Delete_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Deleted)
                {
                    SqlCommand cmd = GetCommand(AllQuantitiesSCT_DAO.Delete);
                    cmd.Parameters.Add(new SqlParameter(AllQuantitiesSCT_DAO.OBIS_Index + "_", Convert.ToUInt64(Row["OBIS_Index", DataRowVersion.Original])));

                    cmd.ExecuteNonQuery();
                    // Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting ObisDetails data in data source", ex);
            }
        }


        #endregion
    }

    #endregion

    #region OBIS Details

    public class OBISDetailsSCT_DAO : SCT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from OBIS_Details order by id";

        public static readonly string Insert = "INSERT INTO OBIS_Details (Obis_Code, Default_OBIS_Code, Device_Id) " +
                                      "VALUES (@Obis_Code, @Default_OBIS_Code, @Device_Id)";

        public static readonly string Update = "UPDATE OBIS_Details SET Obis_Code = @Obis_Code, Default_OBIS_Code = @Default_OBIS_Code, Device_Id = @Device_Id " +
             "WHERE id = @id_";
        public static readonly string Delete = "DELETE FROM OBIS_Details " +
               "WHERE id = @id_";

        public static readonly string IdParam = "@id";
        public static readonly string Obis_Code = "@Obis_Code";
        public static readonly string Default_OBIS_Code = "@Default_OBIS_Code";
        public static readonly string Device_Id = "@Device_Id";
        #endregion

        public OBISDetailsSCT_DAO(string connectionString)
            : base(connectionString)
        {

        }
        public OBISDetailsSCT_DAO(IDbConnection connection)
            : base(connection)
        {

        }

        public void Load_ObisDetails(Configs DataSet)
        {
            try
            {
                SqlDataAdapter OBIS_Rights_DataAdapeter =
                    new SqlDataAdapter(new SqlCommand(Select, (SqlConnection)DataBaseConnection));
                //DataSet.MeterTypeInfo.Clear();
                OBIS_Rights_DataAdapeter.Fill(DataSet.OBIS_Details);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading Obis Details info", ex);
            }
        }

        public void Update_ObisDetails(Configs DataSet)
        {
            try
            {
                SqlCommand Sel_qury = new SqlCommand(Select, (SqlConnection)DataBaseConnection);
                //DataSet.MeterTypeInfo.Clear();
                this.Update(DataSet.OBIS_Details, Sel_qury);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating ObisDetails info", ex);
            }
        }

        public void AcceptChangesObisDetails(Configs DataSet)
        {
            Configs.OBIS_DetailsRow dtRow = null;
            try
            {

                if (DataSet == null)
                    return;
                List<Configs.OBIS_DetailsRow> OBIS_Detail_List = DataSet.OBIS_Details.ToList<Configs.OBIS_DetailsRow>();
                for (int index = 0; (OBIS_Detail_List != null && index < OBIS_Detail_List.Count); index++)
                {
                    dtRow = OBIS_Detail_List[index];
                    #region Insert_Code
                    ///Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertOBISDetails(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateOBISDetails(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteOBISDetails(dtRow);
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error Updating ObisDetails {0}", ex));
            }
        }

        #region CRUD_Method

        public void InsertOBISDetails(Configs.OBIS_DetailsRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    SqlCommand cmd = GetCommand(OBISDetailsSCT_DAO.Insert);
                    //cmd.Parameters.Add(new SqlParameter(OBISDetailsSCT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new SqlParameter(OBISDetailsSCT_DAO.Obis_Code, Row.Obis_Code));
                    cmd.Parameters.Add(new SqlParameter(OBISDetailsSCT_DAO.Default_OBIS_Code, Row.Default_OBIS_Code));
                    cmd.Parameters.Add(new SqlParameter(OBISDetailsSCT_DAO.Device_Id, Row.Device_Id));
                    cmd.ExecuteNonQuery();
                    // Row.AcceptChanges();

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving ObisDetails data in data source", ex);
            }
        }

        public void UpdateOBISDetails(Configs.OBIS_DetailsRow Row)
        {
            try
            {
                #region Update_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    SqlCommand cmd = GetCommand(OBISDetailsSCT_DAO.Update);
                    ///Parameters Reflecting Modification
                    cmd.Parameters.Add(new SqlParameter(OBISDetailsSCT_DAO.Obis_Code, Row.Obis_Code));
                    cmd.Parameters.Add(new SqlParameter(OBISDetailsSCT_DAO.Default_OBIS_Code, Row.Default_OBIS_Code));
                    cmd.Parameters.Add(new SqlParameter(OBISDetailsSCT_DAO.Device_Id, Row.Device_Id));
                    ///Parameters Before Modification
                    cmd.Parameters.Add(new SqlParameter(OBISDetailsSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating ObisDetails data in data source", ex);
            }
        }

        public void DeleteOBISDetails(Configs.OBIS_DetailsRow Row)
        {
            try
            {
                #region Delete_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Deleted)
                {
                    SqlCommand cmd = GetCommand(OBISDetailsSCT_DAO.Delete);
                    cmd.Parameters.Add(new SqlParameter(OBISDetailsSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

                    cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting ObisDetails data in data source", ex);
            }
        }

        #endregion
    }

    #endregion

    #region OBISRightsGroup

    public class OBISRightsGroupSCT_DAO : SCT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from Obis_Rights_Group order by id desc";
        public static readonly string Insert = "INSERT INTO Obis_Rights_Group (id, Group_Name,Update_Rights) VALUES (@id, @Group_Name,@Update_Rights)";
        public static readonly string Update = "UPDATE Obis_Rights_Group SET Group_Name = @Group_Name, Update_Rights = @Update_Rights "
            + "WHERE id = @id_";
        public static readonly string Delete = "DELETE FROM Obis_Rights_Group "
            + "WHERE id = @id_";

        public static readonly string IdParam = "@id";
        public static readonly string Group_Name = "@Group_Name";
        public static readonly string Update_Rights = "@Update_Rights";

        #endregion

        public OBISRightsGroupSCT_DAO(string connectionString)
            : base(connectionString)
        { }
        public OBISRightsGroupSCT_DAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadOBISRightsGroup(Configs DataSet)
        {
            try
            {
                SqlDataAdapter ConfigsDataAdapeter =
                    new SqlDataAdapter(new SqlCommand(Select, (SqlConnection)DataBaseConnection));
                ConfigsDataAdapeter.Fill(DataSet.Obis_Rights_Group);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading OBISRightsGroup", ex);
            }
        }

        public void UpdateOBISRightsGroup(Configs DataSet)
        {
            try
            {
                SqlCommand Select_Sql_Cmd = new SqlCommand(Select, (SqlConnection)DataBaseConnection);
                this.Update(DataSet.Obis_Rights_Group, Select_Sql_Cmd);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating OBISRightsGroup", ex);
            }
        }

        public void AcceptChangesOBISRightsGroup(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.Obis_Rights_GroupRow> OBISRightsGroupRows = DataSet.Obis_Rights_Group.ToList<Configs.Obis_Rights_GroupRow>();
                for (int index = 0; (OBISRightsGroupRows != null && index < OBISRightsGroupRows.Count); index++)
                {
                    Configs.Obis_Rights_GroupRow dtRow = OBISRightsGroupRows[index];
                    #region Insert_Code
                    ///Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertOBISRightsGroup(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateOBISRightsGroup(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteOBISRightsGroup(dtRow);
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating OBISRightsGroup", ex);
            }
        }

        #region CRUD_Methods
        public void InsertOBISRightsGroup(Configs.Obis_Rights_GroupRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    SqlCommand cmd = GetCommand(OBISRightsGroupSCT_DAO.Insert);
                    cmd.Parameters.Add(new SqlParameter(OBISRightsGroupSCT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new SqlParameter(OBISRightsGroupSCT_DAO.Group_Name, Row.Group_Name));
                    cmd.Parameters.Add(new SqlParameter(OBISRightsGroupSCT_DAO.Update_Rights, Row.Update_Rights));
                    //cmd.Parameters.Add(new OleDbParameter(MeterTypeInfoSCT_DAO.FirmWareVersionParam, Row.FirmWareVersion));
                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving OBISRightsGroup data in data source", ex);
            }
        }

        public void UpdateOBISRightsGroup(Configs.Obis_Rights_GroupRow Row)
        {
            try
            {
                #region Update_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    SqlCommand cmd = GetCommand(OBISRightsGroupSCT_DAO.Update);
                    ///Reflected Changes Parameters
                    cmd.Parameters.Add(new SqlParameter(OBISRightsGroupSCT_DAO.Group_Name, Row.Group_Name));
                    cmd.Parameters.Add(new SqlParameter(OBISRightsGroupSCT_DAO.Update_Rights, Row.Update_Rights));

                    /// Parameters Before Modification
                    cmd.Parameters.Add(new SqlParameter(OBISRightsGroupSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

                    int res = cmd.ExecuteNonQuery();
                    //if (res > 0)
                    //    Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying OBISRightsGroup data in data source", ex);
            }
        }

        public void DeleteOBISRightsGroup(Configs.Obis_Rights_GroupRow Row)
        {
            try
            {
                #region Delete_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Deleted)
                {
                    SqlCommand cmd = GetCommand(OBISRightsGroupSCT_DAO.Delete);
                    cmd.Parameters.Add(new SqlParameter(OBISRightsGroupSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting OBISRightsGroup data in data source", ex);
            }
        }

        #endregion
    }

    #endregion

    #region OBIS_Rights

    public class OBISRithtsSCT_DAO : SCT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from OBIS_Rights order by id,OBIS_Index";

        public static readonly string SelectByGroupInfo = "Select * from OBIS_Rights WHERE (ObisRightGroupId = @ObisRightGroupId ) order by id,OBIS_Index";

        public static readonly string SelectMaxId = "Select Max(id) from OBIS_Rights";

        public static readonly string Insert = "INSERT INTO OBIS_Rights (id,ObisRightGroupId, OBIS_Index, Version) " +
                                      "VALUES (@id,@ObisRightGroupId, @OBIS_Index, @version)";

        public static readonly string Update = "UPDATE OBIS_Rights SET ObisRightGroupId = @ObisRightGroupId, OBIS_Index = @OBIS_Index, Version = @version " +
             "WHERE id = @id_ AND ObisRightGroupId = @ObisRightGroupId_";

        public static readonly string Delete = "DELETE FROM OBIS_Rights " +
             "WHERE id = @id_ AND ObisRightGroupId = @ObisRightGroupId_";

        public static readonly string IdParam = "@id";
        public static readonly string ObisRightGroupId = "@ObisRightGroupId";
        public static readonly string OBISIndexParam = "@OBIS_Index";
        public static readonly string VersionParam = "@version";
        #endregion

        public OBISRithtsSCT_DAO(string connectionString)
            : base(connectionString)
        {

        }
        public OBISRithtsSCT_DAO(IDbConnection connection)
            : base(connection)
        {

        }

        public void Load_OBIS_Rights(Configs DataSet)
        {
            try
            {
                SqlDataAdapter OBIS_Rights_DataAdapeter =
                    new SqlDataAdapter(new SqlCommand(Select, (SqlConnection)DataBaseConnection));
                // DataSet.MeterTypeInfo.Clear();
                OBIS_Rights_DataAdapeter.Fill(DataSet.OBIS_Rights);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading OBIS Rights info", ex);
            }
        }

        public void Load_OBISRightsByGroup(Configs DataSet, Configs.Obis_Rights_GroupRow OBISRightGroupRow)
        {
            try
            {
                if (DataSet == null)
                    return;
                #region // Clear Previously Loaded OBIS_Codes Of OBISRightGroup

                try
                {
                    // Query the database for the rows to be deleted
                    var deleteOBISRights =
                        from OBISRights in DataSet.OBIS_Rights.AsEnumerable<Configs.OBIS_RightsRow>()
                        where !OBISRights.IsObisRightGroupIdNull() &&
                               OBISRights.ObisRightGroupId == OBISRightGroupRow.id
                        select OBISRights;

                    try
                    {
                        DataSet.EnforceConstraints = false;
                        foreach (var row in deleteOBISRights)
                        {
                            if (row == null)
                                continue;

                            var rightsRows = row.GetRightsRows();
                            if (rightsRows != null && rightsRows.Length > 0)
                                foreach (var rightrow in rightsRows)
                                {
                                    if (rightrow == null)
                                        continue;
                                    rightrow.Delete();
                                }
                            row.Delete();
                            // DataSet.OBIS_Rights.DeleteOnSubmit(row);
                        }
                    }
                    finally
                    {
                        DataSet.EnforceConstraints = true;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error removing previouse loaded OBIS Code Rights Info", ex);
                }

                #endregion
                SqlCommand SelectByGroupInfo = GetCommand(OBISRithtsSCT_DAO.SelectByGroupInfo);
                SelectByGroupInfo.Parameters.Add(new SqlParameter(OBISRithtsSCT_DAO.ObisRightGroupId, OBISRightGroupRow.id));
                Load(DataSet.OBIS_Rights, SelectByGroupInfo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading OBIS Rights info", ex);
            }
        }

        public long Load_MaxOBISRightId()
        {
            try
            {
                SqlCommand Select_MaxOBISCodeId = GetCommand(OBISRithtsSCT_DAO.SelectMaxId);
                SqlDataReader reader = Select_MaxOBISCodeId.ExecuteReader();
                if (!reader.Read() || reader[0] == DBNull.Value) return 0;
                long val = Convert.ToInt64(reader[0]);
                return val;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading OBIS Rights Max Id info", ex);
            }
        }

        public void Update_OBISCodes(Configs DataSet)
        {
            try
            {
                SqlCommand Sel_qury = new SqlCommand(Select, (SqlConnection)DataBaseConnection);
                // DataSet.MeterTypeInfo.Clear();
                this.Update(DataSet.OBIS_Rights, Sel_qury);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating OBIS Code Labels info", ex);
            }
        }

        public void AcceptChangesOBIS_Rights(Configs DataSet)
        {
            Configs.OBIS_RightsRow dtRow = null;
            try
            {

                if (DataSet == null)
                    return;
                List<Configs.OBIS_RightsRow> OBIS_Rights_List = DataSet.OBIS_Rights.ToList<Configs.OBIS_RightsRow>();
                for (int index = 0; (OBIS_Rights_List != null && index < OBIS_Rights_List.Count); index++)
                {
                    dtRow = OBIS_Rights_List[index];
                    #region Insert_Code
                    ///Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertOBIS_Rights(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateOBIS_Rights(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteOBIS_Rights(dtRow);
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                //string msg = null;
                //if (dtRow != null)
                //{
                //    StOBISCode dt = (DLMS.Get_Index)dtRow.OBIS_Index;
                //    msg = dt.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                //}
                throw new Exception(String.Format("Error Updating OBIS Rights {0}", ex));
            }
        }

        #region CRUD_Method

        public void InsertOBIS_Rights(Configs.OBIS_RightsRow Row)
        {
            try
            {
                #region Insert_Code
                // Exec Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    SqlCommand cmd = GetCommand(OBISRithtsSCT_DAO.Insert);
                    cmd.Parameters.Add(new SqlParameter(OBISRithtsSCT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new SqlParameter(OBISRithtsSCT_DAO.ObisRightGroupId, Row.ObisRightGroupId));
                    cmd.Parameters.Add(new SqlParameter(OBISRithtsSCT_DAO.OBISIndexParam, Row.OBIS_Index));
                    cmd.Parameters.Add(new SqlParameter(OBISRithtsSCT_DAO.VersionParam, Row.Version));
                    cmd.ExecuteNonQuery();
                    // Row.AcceptChanges();

                }
                #endregion
            }
            catch (Exception ex)
            {
                //throw new Exception("Error saving OBIS_Rights data in data source", ex);
            }
        }

        public void UpdateOBIS_Rights(Configs.OBIS_RightsRow Row)
        {
            try
            {
                #region Update_Code
                // Exec Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    SqlCommand cmd = GetCommand(OBISRithtsSCT_DAO.Update);
                    ///Parameters Reflecting Modification
                    cmd.Parameters.Add(new SqlParameter(OBISRithtsSCT_DAO.ObisRightGroupId, Row.ObisRightGroupId));
                    cmd.Parameters.Add(new SqlParameter(OBISRithtsSCT_DAO.OBISIndexParam, Row.OBIS_Index));
                    cmd.Parameters.Add(new SqlParameter(OBISRithtsSCT_DAO.VersionParam, Row.Version));
                    ///Parameters Before Modification
                    cmd.Parameters.Add(new SqlParameter(OBISRithtsSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(OBISRithtsSCT_DAO.ObisRightGroupId + "_", Convert.ToInt32(Row["ObisRightGroupId", DataRowVersion.Original])));
                    cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating OBIS_Rights data in data source", ex);
            }
        }

        public void DeleteOBIS_Rights(Configs.OBIS_RightsRow Row)
        {
            try
            {
                #region Delete_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Deleted)
                {
                    SqlCommand cmd = GetCommand(OBISRithtsSCT_DAO.Delete);
                    cmd.Parameters.Add(new SqlParameter(OBISRithtsSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(OBISRithtsSCT_DAO.ObisRightGroupId + "_", Convert.ToInt32(Row["ObisRightGroupId", DataRowVersion.Original])));

                    cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting OBIS_Rights data in data source", ex);
            }
        }

        #endregion
    }

    #endregion

    #region Rights

    public class RithtsSCT_DAO : SCT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from Rights order by id,OBIS_Right_Id";

        public static readonly string SelectMaxId = "Select Max(id) from Rights";

        public static readonly string SelectByOBISRightId = "Select * from Rights WHERE OBIS_Right_Id = @OBIS_Rights_Id order by id,OBIS_Right_Id";

        public static readonly string Insert = "INSERT INTO Rights (id, type, SubId, value, OBIS_Right_Id) " +
                    " VALUES (@id, @type, @SubId, @value, @OBIS_Rights_Id)";

        public static readonly string Update = "UPDATE Rights SET type = @type, SubId = @SubId, value = @value,"
                    + "OBIS_Right_Id = @OBIS_Rights_Id " +
                   " WHERE id = @id_ AND SubId =  @SubId_"
                    + " AND value = @value_ AND OBIS_Right_Id = @OBIS_Right_Id_";


        public static readonly string Delete = "DELETE FROM [Rights] WHERE id = @id_ AND SubId =  @SubId_"
                    + " AND value = @value_ AND OBIS_Right_Id = @OBIS_Right_Id_";

        public static readonly string IdParam = "@id";
        public static readonly string TypeParam = "@type";
        public static readonly string SubIdParam = "@SubId";
        public static readonly string ValueParam = "@value";
        public static readonly string OBIS_RightsParam = "@OBIS_Rights_Id";
        #endregion

        public RithtsSCT_DAO(string connectionString)
            : base(connectionString) { }
        public RithtsSCT_DAO(IDbConnection connection)
            : base(connection) { }

        public void Load_Rights(Configs DataSet)
        {
            try
            {
                SqlDataAdapter OBIS_Rights_DataAdapeter =
                    new SqlDataAdapter(new SqlCommand(Select, (SqlConnection)DataBaseConnection));
                //DataSet.MeterTypeInfo.Clear();
                OBIS_Rights_DataAdapeter.Fill(DataSet.Rights);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading OBIS Rights info", ex);
            }
        }

        public void Load_RightsByOBISRightId(Configs DataSet, List<Configs.OBIS_RightsRow> OBISRights)
        {
            try
            {
                if (OBISRights == null || OBISRights.Count <= 0 || DataSet == null)
                    return;
                foreach (var OBISRight in OBISRights)
                {
                    SqlCommand SelectOBISRights = GetCommand(RithtsSCT_DAO.SelectByOBISRightId);
                    SelectOBISRights.Parameters.Add(new SqlParameter(RithtsSCT_DAO.OBIS_RightsParam, OBISRight.id));
                    Load(DataSet.Rights, SelectOBISRights);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading OBIS Rights info", ex);
            }
        }

        public long Load_MaxOBISRightId()
        {
            try
            {
                SqlCommand Select_MaxOBISCodeId = GetCommand(RithtsSCT_DAO.SelectMaxId);
                SqlDataReader reader = Select_MaxOBISCodeId.ExecuteReader(CommandBehavior.SingleResult);
                if (!reader.Read() || reader[0] == DBNull.Value) return 0;
                long val = Convert.ToInt64(reader[0]);
                return val;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading Rights Max Id info", ex);
            }
        }

        public void Update_Rights(Configs DataSet)
        {
            try
            {
                SqlCommand Sel_qury = new SqlCommand(Select, (SqlConnection)DataBaseConnection);
                //DataSet.MeterTypeInfo.Clear();
                this.Update(DataSet.Rights, Sel_qury);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating OBIS Rights info", ex);
            }
        }

        public void AcceptChanges_Rights(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.RightsRow> OBIS_Rights_List = DataSet.Rights.ToList<Configs.RightsRow>();
                for (int index = 0; (OBIS_Rights_List != null && index < OBIS_Rights_List.Count); index++)
                {
                    Configs.RightsRow dtRow = OBIS_Rights_List[index];
                    #region Insert_Code
                    ///Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertRights(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateRights(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteRights(dtRow);
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating OBIS Rights", ex);
            }
        }

        #region CRUD_Method

        public void InsertRights(Configs.RightsRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    SqlCommand cmd = GetCommand(RithtsSCT_DAO.Insert);
                    cmd.Parameters.Add(new SqlParameter(RithtsSCT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new SqlParameter(RithtsSCT_DAO.TypeParam, Row.type));
                    cmd.Parameters.Add(new SqlParameter(RithtsSCT_DAO.SubIdParam, Row.SubId));
                    cmd.Parameters.Add(new SqlParameter(RithtsSCT_DAO.ValueParam, Row.value));
                    cmd.Parameters.Add(new SqlParameter(RithtsSCT_DAO.OBIS_RightsParam, Row.OBIS_Right_Id));
                    cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                //throw new Exception("Error saving Rights data in data source", ex);
            }
        }

        public void UpdateRights(Configs.RightsRow Row)
        {
            try
            {
                #region Update_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    SqlCommand cmd = GetCommand(RithtsSCT_DAO.Update);
                    ///Parameters Reflecting Change
                    cmd.Parameters.Add(new SqlParameter(RithtsSCT_DAO.TypeParam, Row.type));
                    cmd.Parameters.Add(new SqlParameter(RithtsSCT_DAO.SubIdParam, Row.SubId));
                    cmd.Parameters.Add(new SqlParameter(RithtsSCT_DAO.ValueParam, Row.value));
                    cmd.Parameters.Add(new SqlParameter(RithtsSCT_DAO.OBIS_RightsParam, Row.OBIS_Right_Id));
                    ///Parameters Before Chagnge
                    cmd.Parameters.Add(new SqlParameter(RithtsSCT_DAO.IdParam + "-", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(RithtsSCT_DAO.TypeParam + "-", Convert.ToByte(Row["type", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(RithtsSCT_DAO.SubIdParam + "-", Convert.ToByte(Row["SubId", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(RithtsSCT_DAO.ValueParam + "-", Convert.ToByte(Row["value", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(RithtsSCT_DAO.OBIS_RightsParam + "-", Convert.ToInt64(Row["OBIS_Right_Id", DataRowVersion.Original])));

                    cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating Rights data in data source", ex);
            }
        }

        public void DeleteRights(Configs.RightsRow Row)
        {
            try
            {
                #region Delete_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Deleted)
                {
                    SqlCommand cmd = GetCommand(RithtsSCT_DAO.Delete);
                    cmd.Parameters.Add(new SqlParameter(RithtsSCT_DAO.IdParam, Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(RithtsSCT_DAO.TypeParam, Convert.ToByte(Row["type", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(RithtsSCT_DAO.SubIdParam, Convert.ToByte(Row["SubId", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(RithtsSCT_DAO.ValueParam, Convert.ToByte(Row["value", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(RithtsSCT_DAO.OBIS_RightsParam, Convert.ToInt64(Row["OBIS_Right_Id", DataRowVersion.Original])));

                    cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting Rights data in data source", ex);
            }
        }

        #endregion
    }
    #endregion

    #region Display_Windows

    public class DisplayWindowSCT_DAO : SCT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from Display_Windows order by id,QuantityIndex,SequenceId";

        public static readonly string Insert = "INSERT INTO Display_Windows (id, Category, Label, AttributeNo, WinNumberToDisplay, QuantityIndex, SequenceId, DisplayWindowsGroupId) " +
                                                "VALUES (@id,  @category,@label, @attributeNo, @winNumber, @QuantityIndex, @sequenceId, @DisplayWindowsGroupId)";

        public static readonly string Update = "UPDATE Display_Windows SET  Category = @category, Label = @label, AttributeNo = @attributeNo, WinNumberToDisplay = @winNumber, " +
                                                "QuantityIndex = @QuantityIndex, SequenceId = @sequenceId, DisplayWindowsGroupId = @DisplayWindowsGroupId " +
                                                 "WHERE ((id =  @id_) AND ((Category IS NULL) OR (Category = @category_))  AND ((AttributeNo IS NULL) OR " +
                                                "(AttributeNo = @attributeNo_)) AND ((WinNumberToDisplay IS NULL) OR (WinNumberToDisplay = @winNumber_)) AND (QuantityIndex = @QuantityIndex_) AND " +
                                                "((SequenceId IS NULL) OR (SequenceId = @sequenceId_)) AND (DisplayWindowsGroupId = @DisplayWindowsGroupId_))";


        public static readonly string Delete = "DELETE FROM Display_Windows " +
                                                "WHERE ((id =  @id_) AND ((Category IS NULL) OR (Category = @category_))  AND ((AttributeNo IS NULL) OR " +
                                                "(AttributeNo = @attributeNo_)) AND ((WinNumberToDisplay IS NULL) OR (WinNumberToDisplay = @winNumber_)) AND (QuantityIndex = @QuantityIndex_) AND " +
                                                "((SequenceId IS NULL) OR (SequenceId = @sequenceId_)) AND (DisplayWindowsGroupId = @DisplayWindowsGroupId_))";

        // Named Params
        public static readonly string IdParam = "@id";
        public static readonly string LabelParam = "@label";
        public static readonly string CategoryParam = "@category";
        public static readonly string AttributeNoParam = "@attributeNo";
        public static readonly string WindowNumberParam = "@winNumber";
        public static readonly string OBISIndexParam = "@QuantityIndex";
        public static readonly string SequenceIdParam = "@sequenceId";
        public static readonly string DisplayWindowsGroupIdParam = "@DisplayWindowsGroupId";

        #endregion

        public DisplayWindowSCT_DAO(string connectionString)
            : base(connectionString) { }
        public DisplayWindowSCT_DAO(IDbConnection connection)
            : base(connection) { }

        public void Load_Display_Windows(Configs DataSet)
        {
            try
            {
                SqlDataAdapter DisplayWindows_DataAdapeter =
                    new SqlDataAdapter(new SqlCommand(Select, (SqlConnection)DataBaseConnection));
                // DataSet.MeterTypeInfo.Clear();
                DisplayWindows_DataAdapeter.Fill(DataSet.DisplayWindows);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading Display Windows Info", ex);
            }
        }

        public void Update_Display_Windows(Configs DataSet)
        {
            try
            {
                SqlCommand OLE_DB_Cmd = new SqlCommand(Select, (SqlConnection)DataBaseConnection);
                // DataSet.MeterTypeInfo.Clear();
                this.Update(DataSet.DisplayWindows, OLE_DB_Cmd);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating Display Windows Info", ex);
            }
        }

        public void AcceptChanges_Display_Windows(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.DisplayWindowsRow> DisplayWindowRow = DataSet.DisplayWindows.ToList<Configs.DisplayWindowsRow>();
                for (int index = 0; (DisplayWindowRow != null && index < DisplayWindowRow.Count); index++)
                {
                    Configs.DisplayWindowsRow dtRow = DisplayWindowRow[index];
                    #region Insert_Code
                    ///Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        Insert_DisplayWindow(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        Update_DisplayWindow(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        Delete_DisplayWindow(dtRow);
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating SAP_Info", ex);
            }
        }

        #region CRUD_Method

        public void Insert_DisplayWindow(Configs.DisplayWindowsRow Row)
        {
            try
            {
                #region Insert_Code
                /// Exec Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    SqlCommand cmd = GetCommand(DisplayWindowSCT_DAO.Insert);
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.LabelParam, Row.Label));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.CategoryParam, Row.Category));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.AttributeNoParam, Row.AttributeNo));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.WindowNumberParam, Row.WinNumberToDisplay));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.OBISIndexParam, Row.QuantityIndex));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.SequenceIdParam, Row.SequenceId));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.DisplayWindowsGroupIdParam, Row.DisplayWindowsGroupId));
                    int res = cmd.ExecuteNonQuery();
                    // Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving Display Window data in data source", ex);
            }
        }

        public void Update_DisplayWindow(Configs.DisplayWindowsRow Row)
        {
            try
            {
                #region Update_Code

                // Exec Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    SqlCommand cmd = GetCommand(DisplayWindowSCT_DAO.Update);
                    // Parameters Reflecting Changes
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.CategoryParam, Row.Category));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.LabelParam, Row.Label));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.AttributeNoParam, Row.AttributeNo));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.WindowNumberParam, Row.WinNumberToDisplay));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.OBISIndexParam, Row.QuantityIndex));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.SequenceIdParam, Row.SequenceId));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.DisplayWindowsGroupIdParam, Row.DisplayWindowsGroupId));
                    // Parameters Before Changes
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    // cmd.Parameters.Add(new OleDbParameter(DisplayWindowSCT_DAO.LabelParam + "_", Convert.ToString(Row["Label", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.CategoryParam + "_", Convert.ToByte(Row["Category", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.AttributeNoParam + "_", Convert.ToByte(Row["AttributeNo", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.WindowNumberParam + "_", Convert.ToUInt32(Row["WinNumberToDisplay", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.OBISIndexParam + "_", Convert.ToUInt64(Row["QuantityIndex", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.SequenceIdParam + "_", Convert.ToUInt32(Row["SequenceId", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.DisplayWindowsGroupIdParam + "_", Convert.ToUInt32(Row["DisplayWindowsGroupId", DataRowVersion.Original])));

                    int res = cmd.ExecuteNonQuery();
                    // Row.AcceptChanges();
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating Display Window data in data source", ex);
            }
        }

        public void Delete_DisplayWindow(Configs.DisplayWindowsRow Row)
        {
            try
            {
                #region Delete_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Deleted)
                {
                    SqlCommand cmd = GetCommand(DisplayWindowSCT_DAO.Delete);
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.CategoryParam + "_", Convert.ToByte(Row["Category", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.AttributeNoParam + "_", Convert.ToByte(Row["AttributeNo", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.WindowNumberParam + "_", Convert.ToUInt32(Row["WinNumberToDisplay", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.OBISIndexParam + "_", Convert.ToUInt64(Row["QuantityIndex", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.SequenceIdParam + "_", Convert.ToUInt32(Row["SequenceId", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowSCT_DAO.DisplayWindowsGroupIdParam + "_", Convert.ToUInt32(Row["DisplayWindowsGroupId", DataRowVersion.Original])));
                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting Display Window data in data source", ex);
            }
        }
        #endregion
    }
    #endregion

    #region DisplayWindowsGroup

    public class DisplayWindowsGroupSCT_DAO : SCT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from DisplayWindows_Group order by Dw_Group_Name desc";
        public static readonly string Insert = "INSERT INTO DisplayWindows_Group (id, Dw_Group_Name) VALUES (@id, @Dw_Group_Name)";
        public static readonly string Update = "UPDATE DisplayWindows_Group SET Dw_Group_Name = @Dw_Group_Name "
            + "WHERE id = @id_";
        public static readonly string Delete = "DELETE FROM DisplayWindows_Group"
            + "WHERE id = @id_";

        public static readonly string IdParam = "@id";
        public static readonly string DWGroupName = "@Dw_Group_Name";

        #endregion

        public DisplayWindowsGroupSCT_DAO(string connectionString)
            : base(connectionString)
        { }
        public DisplayWindowsGroupSCT_DAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadDisplayWindowsGroup(Configs DataSet)
        {
            try
            {
                SqlDataAdapter ConfigsDataAdapeter =
                    new SqlDataAdapter(new SqlCommand(Select, (SqlConnection)DataBaseConnection));
                //DataSet.MeterTypeInfo.Clear();
                ConfigsDataAdapeter.Fill(DataSet.DisplayWindows_Group);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading DisplayWindows Group", ex);
            }
        }

        public void UpdateDisplayWindowsGroup(Configs DataSet)
        {
            try
            {
                SqlCommand Select_Sql_Cmd = new SqlCommand(Select, (SqlConnection)DataBaseConnection);
                this.Update(DataSet.DisplayWindows_Group, Select_Sql_Cmd);
                //DataSet.MeterTypeInfo.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating DisplayWindows Group", ex);
            }
        }

        public void AcceptChangesDisplayWindowsGroup(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.DisplayWindows_GroupRow> dataRows = DataSet.DisplayWindows_Group.ToList<Configs.DisplayWindows_GroupRow>();
                for (int index = 0; (dataRows != null && index < dataRows.Count); index++)
                {
                    Configs.DisplayWindows_GroupRow dtRow = dataRows[index];
                    #region Insert_Code
                    ///Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertDisplayWindowsGroup(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateDisplayWindowsGroup(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteDisplayWindowsGroup(dtRow);
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating DisplayWindows Group", ex);
            }
        }

        #region CRUD_Methods
        public void InsertDisplayWindowsGroup(Configs.DisplayWindows_GroupRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    SqlCommand cmd = GetCommand(DisplayWindowsGroupSCT_DAO.Insert);
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowsGroupSCT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowsGroupSCT_DAO.DWGroupName, Row.Dw_Group_Name));
                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving DisplayWindowsGroup data in data source", ex);
            }
        }

        public void UpdateDisplayWindowsGroup(Configs.DisplayWindows_GroupRow Row)
        {
            try
            {
                #region Update_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    SqlCommand cmd = GetCommand(DisplayWindowsGroupSCT_DAO.Update);
                    ///Reflected Changes Parameters
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowsGroupSCT_DAO.DWGroupName, Row.Dw_Group_Name));
                    ///Before Changes Parameters
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowsGroupSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

                    int res = cmd.ExecuteNonQuery();
                    //if (res > 0)
                    //    Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying DisplayWindowsGroup data in data source", ex);
            }
        }

        public void DeleteDisplayWindowsGroup(Configs.DisplayWindows_GroupRow Row)
        {
            try
            {
                #region Delete_Code

                /// Exec Insert Query
                if (Row.RowState == DataRowState.Deleted)
                {
                    SqlCommand cmd = GetCommand(DisplayWindowsGroupSCT_DAO.Delete);
                    cmd.Parameters.Add(new SqlParameter(DisplayWindowsGroupSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.ExecuteNonQuery();
                    /// Row.AcceptChanges();
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting DisplayWindowsGroup data in data source", ex);
            }
        }

        #endregion
    }
    #endregion

    #region Load_Profiles

    public class LoadProfileSCT_DAO : SCT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from Load_Profile_Channels order by id,QuantityIndex,SequenceId";

        public static readonly string Insert = "INSERT INTO Load_Profile_Channels (id,Label, QuantityIndex, AttributeNo, Multiplier, SequenceId, FormatSpecifier, Unit, LoadProfileGroupId) " +
                                               "VALUES (@id,@label, @quantityIndex, @attributeNo, @multiplier, @sequenceId, @formatSpecifier, @unit, @LoadProfileGroupId)";

        public static readonly string Update = "UPDATE Load_Profile_Channels SET Label= @label, QuantityIndex = @quantityIndex, AttributeNo = @attributeNo, Multiplier = @multiplier, " +
        "SequenceId = @sequenceId, FormatSpecifier = @formatSpecifier, Unit = @unit, LoadProfileGroupId = @LoadProfileGroupId " +
         "WHERE (((id IS NULL) OR (id = @id_)))";

        public static readonly string Delete = "DELETE FROM Load_Profile_Channels " +
        "WHERE (((id IS NULL) OR (id = @id_)))";

        // Named Parameters
        public static readonly string IdParam = "@id";
        public static readonly string LabelParam = "@label";
        public static readonly string OBISIndexParam = "@quantityIndex";
        public static readonly string AttributeNoParam = "@attributeNo";
        public static readonly string MultiplierParam = "@multiplier";
        public static readonly string SequenceIdParam = "@sequenceId";
        public static readonly string FormatSpecifierParam = "@formatSpecifier";
        public static readonly string UnitParam = "@unit";
        public static readonly string LoadProfileGroupIdParam = "@LoadProfileGroupId";

        #endregion

        public LoadProfileSCT_DAO(string connectionString)
            : base(connectionString) { }
        public LoadProfileSCT_DAO(IDbConnection connection)
            : base(connection) { }

        public void Load_Profiles(Configs DataSet)
        {
            try
            {
                SqlDataAdapter LoadProfile_DataAdapeter =
                    new SqlDataAdapter(new SqlCommand(Select, (SqlConnection)DataBaseConnection));
                //DataSet.MeterTypeInfo.Clear();
                LoadProfile_DataAdapeter.Fill(DataSet.LoadProfileChannels);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading Load Profiles Entries", ex);
            }
        }

        public void Update_Load_Profiles(Configs DataSet)
        {
            try
            {
                SqlCommand Sel_Load_Profile_Cmd = new SqlCommand(Select, (SqlConnection)DataBaseConnection);
                //DataSet.MeterTypeInfo.Clear();
                this.Update(DataSet.LoadProfileChannels, Sel_Load_Profile_Cmd);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating Load Profiles Entries", ex);
            }
        }

        public void AcceptChanges_Load_Profiles(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.LoadProfileChannelsRow> LoadProfileList = DataSet.LoadProfileChannels.ToList<Configs.LoadProfileChannelsRow>();
                for (int index = 0; (LoadProfileList != null && index < LoadProfileList.Count); index++)
                {
                    Configs.LoadProfileChannelsRow dtRow = LoadProfileList[index];
                    #region Insert_Code
                    ///Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        Insert_LoadProfiles(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        Update_LoadProfiles(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        Delete_LoadProfiles(dtRow);
                    }
                    #endregion
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating Load Profiles", ex);
            }
        }

        #region CRUD_Method

        public void Insert_LoadProfiles(Configs.LoadProfileChannelsRow Row)
        {
            try
            {
                #region Insert_Code

                /// Exec Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    SqlCommand cmd = GetCommand(LoadProfileSCT_DAO.Insert);
                    cmd.Parameters.Add(new SqlParameter(LoadProfileSCT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new SqlParameter(LoadProfileSCT_DAO.LabelParam, Row.Label));
                    cmd.Parameters.Add(new SqlParameter(LoadProfileSCT_DAO.OBISIndexParam, Row.QuantityIndex));
                    cmd.Parameters.Add(new SqlParameter(LoadProfileSCT_DAO.AttributeNoParam, Row.AttributeNo));
                    cmd.Parameters.Add(new SqlParameter(LoadProfileSCT_DAO.MultiplierParam, Row.Multiplier));
                    cmd.Parameters.Add(new SqlParameter(LoadProfileSCT_DAO.SequenceIdParam, Row.SequenceId));
                    cmd.Parameters.Add(new SqlParameter(LoadProfileSCT_DAO.FormatSpecifierParam, Row.FormatSpecifier));
                    cmd.Parameters.Add(new SqlParameter(LoadProfileSCT_DAO.UnitParam, Row.Unit));
                    cmd.Parameters.Add(new SqlParameter(LoadProfileSCT_DAO.LoadProfileGroupIdParam, Row.LoadProfileGroupId));
                    int res = cmd.ExecuteNonQuery();
                    // Row.AcceptChanges();
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving Load Profile data in data source", ex);
            }
        }

        public void Update_LoadProfiles(Configs.LoadProfileChannelsRow Row)
        {
            try
            {
                #region Update_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    SqlCommand cmd = GetCommand(LoadProfileSCT_DAO.Update);
                    ///Parameters Reflecting Changes
                    cmd.Parameters.Add(new SqlParameter(LoadProfileSCT_DAO.LabelParam, Row.Label));
                    cmd.Parameters.Add(new SqlParameter(LoadProfileSCT_DAO.OBISIndexParam, Row.QuantityIndex));
                    cmd.Parameters.Add(new SqlParameter(LoadProfileSCT_DAO.AttributeNoParam, Row.AttributeNo));
                    cmd.Parameters.Add(new SqlParameter(LoadProfileSCT_DAO.MultiplierParam, Row.Multiplier));
                    cmd.Parameters.Add(new SqlParameter(LoadProfileSCT_DAO.SequenceIdParam, Row.SequenceId));
                    cmd.Parameters.Add(new SqlParameter(LoadProfileSCT_DAO.FormatSpecifierParam, Row.FormatSpecifier));
                    cmd.Parameters.Add(new SqlParameter(LoadProfileSCT_DAO.UnitParam, Row.Unit));
                    cmd.Parameters.Add(new SqlParameter(LoadProfileSCT_DAO.LoadProfileGroupIdParam, Row.LoadProfileGroupId));
                    ///Parameters Before Changes
                    cmd.Parameters.Add(new SqlParameter(LoadProfileSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileSCT_DAO.MultiplierParam + "_", Convert.ToInt32(Row["AttributeNo", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileSCT_DAO.AttributeNoParam + "_", Convert.ToByte(Row["AttributeNo", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileSCT_DAO.OBISIndexParam + "_", Convert.ToUInt64(Row["QuantityIndex", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileSCT_DAO.SequenceIdParam + "_", Convert.ToUInt32(Row["SequenceId", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileSCT_DAO.FormatSpecifierParam + "_", Convert.ToString(Row["FormatSpecifier", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileSCT_DAO.UnitParam + "_", Convert.ToString(Row["Unit", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileSCT_DAO.LoadProfileGroupIdParam + "_", Convert.ToInt32(Row["LoadProfileGroupId", DataRowVersion.Original])));
                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating Load Profile data in data source", ex);
            }
        }

        public void Delete_LoadProfiles(Configs.LoadProfileChannelsRow Row)
        {
            try
            {
                #region Delete_Code
                if (Row.RowState == DataRowState.Deleted)
                {
                    SqlCommand cmd = GetCommand(LoadProfileSCT_DAO.Delete);
                    cmd.Parameters.Add(new SqlParameter(LoadProfileSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileSCT_DAO.MultiplierParam + "_", Convert.ToInt32(Row["AttributeNo", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileSCT_DAO.AttributeNoParam + "_", Convert.ToByte(Row["AttributeNo", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileSCT_DAO.OBISIndexParam + "_", Convert.ToUInt64(Row["QuantityIndex", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileSCT_DAO.SequenceIdParam  + "_", Convert.ToUInt32(Row["SequenceId", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileSCT_DAO.FormatSpecifierParam + "_", Convert.ToString(Row["FormatSpecifier", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileSCT_DAO.UnitParam + "_", Convert.ToString(Row["Unit", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileSCT_DAO.LoadProfileGroupIdParam + "_", Convert.ToInt32(Row["LoadProfileGroupId", DataRowVersion.Original])));
                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting Load Profile data in data source", ex);
            }
        }
        #endregion
    }

    #endregion

    #region LoadProfileGroup

    public class LoadProfileGroupSCT_DAO : SCT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from LoadProfile_Group order by LoadProfile_Group_Name desc";
        public static readonly string Insert = "INSERT INTO LoadProfile_Group (id, LoadProfile_Group_Name) VALUES (@id, @LoadProfile_Group_Name)";
        public static readonly string Update = "UPDATE LoadProfile_Group SET LoadProfile_Group_Name = @LoadProfile_Group_Name "
            + "WHERE id = @id_";
        public static readonly string Delete = "DELETE FROM LoadProfile_Group"
            + "WHERE id = @id_";

        public static readonly string IdParam = "@id";
        public static readonly string LPGroupName = "@LoadProfile_Group_Name";

        #endregion

        public LoadProfileGroupSCT_DAO(string connectionString)
            : base(connectionString)
        { }
        public LoadProfileGroupSCT_DAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadLoadProfileGroup(Configs DataSet)
        {
            try
            {
                SqlDataAdapter ConfigsDataAdapeter =
                    new SqlDataAdapter(new SqlCommand(Select, (SqlConnection)DataBaseConnection));
                //DataSet.MeterTypeInfo.Clear();
                ConfigsDataAdapeter.Fill(DataSet.LoadProfile_Group);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading Load Profile group", ex);
            }
        }

        public void UpdateLoadProfileGroup(Configs DataSet)
        {
            try
            {
                SqlCommand Select_Sql_Cmd = new SqlCommand(Select, (SqlConnection)DataBaseConnection);
                this.Update(DataSet.LoadProfile_Group, Select_Sql_Cmd);
                //DataSet.MeterTypeInfo.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating Load Profile group", ex);
            }
        }

        public void AcceptChangesLoadProfileGroup(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.LoadProfile_GroupRow> MeterUserRows = DataSet.LoadProfile_Group.ToList<Configs.LoadProfile_GroupRow>();
                for (int index = 0; (MeterUserRows != null && index < MeterUserRows.Count); index++)
                {
                    Configs.LoadProfile_GroupRow dtRow = MeterUserRows[index];
                    #region Insert_Code
                    ///Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertLoadProfileGroup(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateLoadProfileGroup(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteLoadProfileGroup(dtRow);
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating Meter User", ex);
            }
        }

        #region CRUD_Methods

        public void InsertLoadProfileGroup(Configs.LoadProfile_GroupRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    SqlCommand cmd = GetCommand(LoadProfileGroupSCT_DAO.Insert);
                    cmd.Parameters.Add(new SqlParameter(LoadProfileGroupSCT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new SqlParameter(LoadProfileGroupSCT_DAO.LPGroupName, Row.LoadProfile_Group_Name));
                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving LoadProfileGroup data in data source", ex);
            }
        }

        public void UpdateLoadProfileGroup(Configs.LoadProfile_GroupRow Row)
        {
            try
            {
                #region Update_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    SqlCommand cmd = GetCommand(LoadProfileGroupSCT_DAO.Update);
                    ///Reflected Changes Parameters
                    cmd.Parameters.Add(new SqlParameter(LoadProfileGroupSCT_DAO.LPGroupName, Row.LoadProfile_Group_Name));
                    ///Before Changes Parameters
                    cmd.Parameters.Add(new SqlParameter(LoadProfileGroupSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

                    int res = cmd.ExecuteNonQuery();
                    //if (res > 0)
                    //    Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying LoadProfileGroup data in data source", ex);
            }
        }

        public void DeleteLoadProfileGroup(Configs.LoadProfile_GroupRow Row)
        {
            try
            {
                #region Delete_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Deleted)
                {
                    SqlCommand cmd = GetCommand(LoadProfileGroupSCT_DAO.Delete);
                    cmd.Parameters.Add(new SqlParameter(LoadProfileGroupSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting LoadProfileGroup data in data source", ex);
            }
        }

        #endregion
    }
    #endregion

    #region Billing Item

    public class BillItemSCT_DAO : SCT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from Billing_Items order by id,BillItemGroupId";

        public static readonly string Insert = "INSERT INTO Billing_Items (id, Label, FormatSpecifier, Unit, Multiplier, SequenceId,BillItemGroupId) " +
                                               "VALUES (@id, @label, @formatSpecifier, @unit, @multiplier, @sequenceId, @BillItemGroupId)";

        public static readonly string Update = "UPDATE Billing_Items SET Label = @label, FormatSpecifier = @formatSpecifier, Unit = @unit," +
            "Multiplier = @multiplier, SequenceId = @sequenceId, BillItemGroupId = @BillItemGroupId " +
        "WHERE ((id = @id_) AND ((Label IS NULL) OR (Label = @label_)) AND ((FormatSpecifier IS NULL) OR (FormatSpecifier = @formatSpecifier_)) AND " +
        "((Unit IS NULL) OR (Unit = @unit_)) AND ((Multiplier IS NULL) OR (Multiplier = @multiplier)) AND ((SequenceId IS NULL) OR (SequenceId = @sequenceId)) AND " +
        "((BillItemGroupId IS NULL) OR (BillItemGroupId = @BillItemGroupId)))";

        public static readonly string Delete = "DELETE FROM Billing_Items " +
        "WHERE ((id = @id_) AND ((Label IS NULL) OR (Label = @label_)) AND ((FormatSpecifier IS NULL) OR (FormatSpecifier = @formatSpecifier_)) AND " +
        "((Unit IS NULL) OR (Unit = @unit_)) AND ((Multiplier IS NULL) OR (Multiplier = @multiplier)) AND ((SequenceId IS NULL) OR (SequenceId = @sequenceId)) AND " +
        "((BillItemGroupId IS NULL) OR (BillItemGroupId = @BillItemGroupId)))";

        // Named Parameters
        public static readonly string IdParam = "@id";
        public static readonly string LabelParam = "@label";
        public static readonly string FormatSpecifierParam = "@formatSpecifier";
        public static readonly string MultiplierParam = "@multiplier";
        public static readonly string SequenceIdParam = "@sequenceId";
        public static readonly string UnitParam = "@unit";
        public static readonly string BillItemGroupIdParam = "@BillItemGroupId";

        #endregion

        public BillItemSCT_DAO(string connectionString)
            : base(connectionString) { }
        public BillItemSCT_DAO(IDbConnection connection)
            : base(connection) { }

        public void Load_Billing_Items(Configs DataSet)
        {
            try
            {
                SqlDataAdapter Event_Profile_Info_DataAdapeter =
                    new SqlDataAdapter(new SqlCommand(Select, (SqlConnection)DataBaseConnection));

                // DataSet.MeterTypeInfo.Clear();
                Event_Profile_Info_DataAdapeter.Fill(DataSet.BillingItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading Billing Items Entries", ex);
            }
        }

        public void Update_Billing_Items(Configs DataSet)
        {
            try
            {
                SqlCommand Event_Profile_Info_Sel_Cmd = new SqlCommand(Select, (SqlConnection)DataBaseConnection);

                // DataSet.MeterTypeInfo.Clear();
                this.Update(DataSet.BillingItems, Event_Profile_Info_Sel_Cmd);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating Billing Items Entries", ex);
            }
        }

        public void AcceptChanges_BillingItems(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.BillingItemsRow> BillItemList = DataSet.BillingItems.ToList<Configs.BillingItemsRow>();
                for (int index = 0; (BillItemList != null && index < BillItemList.Count); index++)
                {
                    Configs.BillingItemsRow dtRow = BillItemList[index];
                    #region Insert_Code

                    /// Exec Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        Insert_BillItem(dtRow);
                    }

                    #endregion
                    #region Update_Code

                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        Update_BillItem(dtRow);
                    }

                    #endregion
                    #region Delete_Code

                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        Delete_BillItem(dtRow);
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating Billing Items", ex);
            }
        }

        #region CRUD_Method

        public void Insert_BillItem(Configs.BillingItemsRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    SqlCommand cmd = GetCommand(BillItemSCT_DAO.Insert);
                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.LabelParam, Row.Label));
                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.FormatSpecifierParam, Row.FormatSpecifier));
                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.UnitParam, Row.Unit));
                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.MultiplierParam, Row.Multiplier));
                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.SequenceIdParam, Row.SequenceId));
                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.BillItemGroupIdParam, Row.BillItemGroupId));
                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving Billing data in data source", ex);
            }
        }

        public void Update_BillItem(Configs.BillingItemsRow Row)
        {
            try
            {
                #region Update_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    SqlCommand cmd = GetCommand(BillItemSCT_DAO.Update);
                    ///Parameters Reflecting Modifications
                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.LabelParam, Row.Label));
                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.FormatSpecifierParam, Row.FormatSpecifier));
                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.UnitParam, Row.Unit));
                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.MultiplierParam, Row.Multiplier));
                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.SequenceIdParam, Row.SequenceId));
                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.BillItemGroupIdParam, Row.BillItemGroupId));
                    ///Parameters Before Modifications
                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.LabelParam + "_", Convert.ToString(Row["Label", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.FormatSpecifierParam + "_", Convert.ToString(Row["FormatSpecifier", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.UnitParam + "_", Convert.ToString(Row["Unit", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.MultiplierParam + "_", Convert.ToInt32(Row["Multiplier", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.SequenceIdParam + "_", Convert.ToInt32(Row["SequenceId", DataRowVersion.Original])));

                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.BillItemGroupIdParam + "_", Convert.ToUInt32(Row["BillItemGroupId", DataRowVersion.Original])));

                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating Billing data in data source", ex);
            }
        }

        public void Delete_BillItem(Configs.BillingItemsRow Row)
        {
            try
            {
                #region Delete_Code
                if (Row.RowState == DataRowState.Deleted)
                {
                    SqlCommand cmd = GetCommand(BillItemSCT_DAO.Delete);
                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.LabelParam + "_", Convert.ToString(Row["Label", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.FormatSpecifierParam + "_", Convert.ToString(Row["FormatSpecifier", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.UnitParam + "_", Convert.ToString(Row["Unit", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.MultiplierParam + "_", Convert.ToInt32(Row["Multiplier", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.SequenceIdParam + "_", Convert.ToInt32(Row["SequenceId", DataRowVersion.Original])));

                    cmd.Parameters.Add(new SqlParameter(BillItemSCT_DAO.BillItemGroupIdParam + "_", Convert.ToUInt32(Row["BillItemGroupId", DataRowVersion.Original])));

                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting Billing Items data in data source", ex);
            }
        }
        #endregion
    }

    #endregion

    #region BillingItemGroup

    public class BillingItemGroupSCT_DAO : SCT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from BillingItem_Group order by BillingItem_Group_Name desc";
        public static readonly string Insert = "INSERT INTO BillingItem_Group (id, BillingItem_Group_Name) VALUES (@id, @BillingItem_Group_Name)";
        public static readonly string Update = "UPDATE BillingItem_Group SET BillingItem_Group_Name = @BillingItem_Group_Name "
            + "WHERE id = @id_";
        public static readonly string Delete = "DELETE FROM BillingItem_Grou]"
            + "WHERE id = @id_";

        public static readonly string IdParam = "@id";
        public static readonly string BIGroupName = "@BillingItem_Group_Name";

        #endregion

        public BillingItemGroupSCT_DAO(string connectionString)
            : base(connectionString)
        { }
        public BillingItemGroupSCT_DAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadBillingItemGroup(Configs DataSet)
        {
            try
            {
                SqlDataAdapter ConfigsDataAdapeter =
                    new SqlDataAdapter(new SqlCommand(Select, (SqlConnection)DataBaseConnection));
                //DataSet.MeterTypeInfo.Clear();
                ConfigsDataAdapeter.Fill(DataSet.BillingItem_Group);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading Billing Item Group", ex);
            }
        }

        public void UpdateBillingItemGroup(Configs DataSet)
        {
            try
            {
                SqlCommand Select_Sql_Cmd = new SqlCommand(Select, (SqlConnection)DataBaseConnection);
                this.Update(DataSet.BillingItem_Group, Select_Sql_Cmd);
                //DataSet.MeterTypeInfo.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating Billing Item Group", ex);
            }
        }

        public void AcceptChangesBillingItemGroup(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.BillingItem_GroupRow> MeterUserRows = DataSet.BillingItem_Group.ToList<Configs.BillingItem_GroupRow>();
                for (int index = 0; (MeterUserRows != null && index < MeterUserRows.Count); index++)
                {
                    Configs.BillingItem_GroupRow dtRow = MeterUserRows[index];
                    #region Insert_Code
                    ///Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertBillingItemGroup(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateBillingItemGroup(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteBillingItemGroup(dtRow);
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating Billing Item Group", ex);
            }
        }

        #region CRUD_Methods
        public void InsertBillingItemGroup(Configs.BillingItem_GroupRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    SqlCommand cmd = GetCommand(BillingItemGroupSCT_DAO.Insert);
                    cmd.Parameters.Add(new SqlParameter(BillingItemGroupSCT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new SqlParameter(BillingItemGroupSCT_DAO.BIGroupName, Row.BillingItem_Group_Name));
                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving BillingItemGroup data in data source", ex);
            }
        }

        public void UpdateBillingItemGroup(Configs.BillingItem_GroupRow Row)
        {
            try
            {
                #region Update_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    SqlCommand cmd = GetCommand(BillingItemGroupSCT_DAO.Update);
                    ///Reflected Changes Parameters
                    cmd.Parameters.Add(new SqlParameter(BillingItemGroupSCT_DAO.BIGroupName, Row.BillingItem_Group_Name));
                    ///Before Changes Parameters
                    cmd.Parameters.Add(new SqlParameter(BillingItemGroupSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

                    int res = cmd.ExecuteNonQuery();
                    //if (res > 0)
                    //    Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying BillingItemGroup data in data source", ex);
            }
        }

        public void DeleteBillingItemGroup(Configs.BillingItem_GroupRow Row)
        {
            try
            {
                #region Delete_Code

                ///Exec Insert Query
                if (Row.RowState == DataRowState.Deleted)
                {
                    SqlCommand cmd = GetCommand(BillingItemGroupSCT_DAO.Delete);
                    cmd.Parameters.Add(new SqlParameter(BillingItemGroupSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.ExecuteNonQuery();
                    /// Row.AcceptChanges();
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting BillingItemGroup data in data source", ex);
            }
        }
        #endregion
    }
    #endregion

    #region Billing Tariff Quantity

    public class BillTariffSCT_DAO : SCT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from Bill_Tariff_Quantity order by BillItemID";

        public static readonly string Insert = "INSERT INTO Bill_Tariff_Quantity (BillItemId, OBIS_Index, SequenceId) " +
                                                "VALUES (@billItemId, @OBIS_Index, @sequenceId)";

        public static readonly string Update = "UPDATE Bill_Tariff_Quantity SET SequenceId = @sequenceId, OBIS_Index = @OBIS_Index  " +
               "WHERE ((BillItemId = @billItemId_) AND (OBIS_Index = @OBIS_Index_) AND ((SequenceId IS NULL) OR (SequenceId = @sequenceId_)))";

        public static readonly string Delete = "DELETE FROM Bill_Tariff_Quantity " +
                "WHERE ((BillItemId = @billItemId_) AND (OBIS_Index = @OBIS_Index_) AND ((SequenceId IS NULL) OR (SequenceId = @sequenceId_)))";

        //Named Parameters

        public static readonly string BillIdParam = "@billItemId";
        public static readonly string OBISIndexParam = "@OBIS_Index";
        public static readonly string SequenceIdParam = "@sequenceId";

        #endregion

        public BillTariffSCT_DAO(string connectionString)
            : base(connectionString) { }
        public BillTariffSCT_DAO(IDbConnection connection)
            : base(connection) { }

        public void Load_Billing_Tariff(Configs DataSet)
        {
            try
            {
                SqlDataAdapter Bill_Tariff_Info_DataAdapeter =
                    new SqlDataAdapter(new SqlCommand(Select, (SqlConnection)DataBaseConnection));
                //DataSet.MeterTypeInfo.Clear();
                Bill_Tariff_Info_DataAdapeter.Fill(DataSet.BillTariffQuantity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading Billing Tariff Quantity Info", ex);
            }
        }

        public void Update_Billing_Tariff(Configs DataSet)
        {
            try
            {
                SqlCommand Event_Profile_Info_Sel_Cmd = new SqlCommand(Select,
                    (SqlConnection)DataBaseConnection);
                //DataSet.MeterTypeInfo.Clear();
                this.Update(DataSet.BillTariffQuantity, Event_Profile_Info_Sel_Cmd);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating Billing Tariff Quantity Info", ex);
            }
        }

        public void AcceptChanges_BillingTariff(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.BillTariffQuantityRow> BillTariffList = DataSet.BillTariffQuantity.ToList<Configs.BillTariffQuantityRow>();
                for (int index = 0; (BillTariffList != null && index < BillTariffList.Count); index++)
                {
                    Configs.BillTariffQuantityRow dtRow = BillTariffList[index];
                    #region Insert_Code
                    ///Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        Insert_BillTariff(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        Update_BillTariff(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        Delete_BillTariff(dtRow);
                    }
                    #endregion
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating Billing Tariff Qunatity in DataStore", ex);
            }
        }

        #region CRUD_Method

        public void Insert_BillTariff(Configs.BillTariffQuantityRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    SqlCommand cmd = GetCommand(BillTariffSCT_DAO.Insert);
                    cmd.Parameters.Add(new SqlParameter(BillTariffSCT_DAO.BillIdParam, Row.BillItemId));
                    cmd.Parameters.Add(new SqlParameter(BillTariffSCT_DAO.OBISIndexParam, Row.OBIS_Index));
                    cmd.Parameters.Add(new SqlParameter(BillTariffSCT_DAO.SequenceIdParam, Row.SequenceId));
                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving Billing Tariff data in data source", ex);
            }
        }

        public void Update_BillTariff(Configs.BillTariffQuantityRow Row)
        {
            try
            {
                #region Update_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    SqlCommand cmd = GetCommand(BillTariffSCT_DAO.Update);
                    ///Parameters Reflecting Changes
                    cmd.Parameters.Add(new SqlParameter(BillTariffSCT_DAO.SequenceIdParam, Row.SequenceId));
                    cmd.Parameters.Add(new SqlParameter(BillTariffSCT_DAO.OBISIndexParam, Row.OBIS_Index));
                    ///Parameter Before Changes
                    cmd.Parameters.Add(new SqlParameter(BillTariffSCT_DAO.BillIdParam + "_", Convert.ToInt32(Row["BillItemId", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(BillTariffSCT_DAO.OBISIndexParam + "_", Convert.ToUInt64(Row["OBIS_Index", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(BillTariffSCT_DAO.SequenceIdParam + "_", Convert.ToInt32(Row["SequenceId", DataRowVersion.Original])));

                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating Billing Tariff data in data source", ex);
            }
        }

        public void Delete_BillTariff(Configs.BillTariffQuantityRow Row)
        {
            try
            {
                #region Delete_Code
                if (Row.RowState == DataRowState.Deleted)
                {
                    SqlCommand cmd = GetCommand(BillTariffSCT_DAO.Delete);
                    cmd.Parameters.Add(new SqlParameter(BillTariffSCT_DAO.BillIdParam + "_", Convert.ToInt32(Row["BillItemId", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(BillTariffSCT_DAO.OBISIndexParam + "_", Convert.ToUInt64(Row["OBIS_Index", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(BillTariffSCT_DAO.SequenceIdParam + "_", Convert.ToInt32(Row["SequenceId", DataRowVersion.Original])));

                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting Billing Tariff data in data source", ex);
            }
        }
        #endregion
    }
    #endregion

    #region EventInfo

    public class EventInfoSCT_DAO : SCT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from Event_Info order by id,EventCode";

        public static readonly string Insert = "INSERT INTO Event_Info (id, EventCode,EventNo, Label, MaxEventCount, EventGroupId,CautionNumber,FlashTime) " +
                                               "VALUES (@id, @eventCode,@eventNo, @label, @maxEventCount, @EventGroupId,@CautionNumber,@FlashTime)";

        public static readonly string Update = "UPDATE Event_Info SET EventNo = @eventNo,EventCode =@eventCode,  Label = @label, MaxEventCount = @maxEventCount, CautionNumber = @CautionNumber, FlashTime = @FlashTime " +
            "WHERE ((id IS NULL) OR (id = @id_)) And (EventCode = @eventCode_)";

        public static readonly string Delete = "DELETE FROM Event_Info " +
           "WHERE ((id IS NULL) OR (id = @id_)) And (EventCode = @eventCode_)";

        ///Named Parameters
        public static readonly string IdParam = "@id";
        public static readonly string EventCodeParam = "@eventCode";
        public static readonly string EventNoParam = "@eventNo";
        public static readonly string MaxEventCountParam = "@maxEventCount";
        public static readonly string LabelParam = "@label";
        public static readonly string ConfigParam = "@EventGroupId";
        public static readonly string CautionNumberParam = "@CautionNumber";
        public static readonly string FlashTimeParam = "@FlashTime";
        #endregion

        public EventInfoSCT_DAO(string connectionString)
            : base(connectionString) { }
        public EventInfoSCT_DAO(IDbConnection connection)
            : base(connection) { }

        public void Load_EventProfileInfo(Configs DataSet)
        {
            try
            {
                SqlDataAdapter Event_Profile_Info_DataAdapeter =
                    new SqlDataAdapter(new SqlCommand(Select, (SqlConnection)DataBaseConnection));
                //DataSet.MeterTypeInfo.Clear();
                Event_Profile_Info_DataAdapeter.Fill(DataSet.EventInfo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading Event Profile Entries", ex);
            }
        }

        public void Update_EventProfileInfo(Configs DataSet)
        {
            try
            {
                SqlCommand Db_Sel_Cmd = new SqlCommand(Select, (SqlConnection)DataBaseConnection);
                //DataSet.MeterTypeInfo.Clear();
                this.Update(DataSet.EventInfo, Db_Sel_Cmd);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating Event Profile Entries", ex);
            }
        }

        public void AcceptChanges_EventProfileInfo(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.EventInfoRow> EventProfileList = DataSet.EventInfo.ToList<Configs.EventInfoRow>();
                for (int index = 0; (EventProfileList != null && index < EventProfileList.Count); index++)
                {
                    Configs.EventInfoRow dtRow = EventProfileList[index];
                    #region Insert_Code
                    ///Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        Insert_EventProfileInfo(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        Update_EventProfileInfo(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        Delete_EventProfileInfo(dtRow);
                    }
                    #endregion
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating Event Profile Info in DataStore", ex);
            }
        }

        #region CRUD_Method

        public void Insert_EventProfileInfo(Configs.EventInfoRow Row)
        {
            try
            {
                #region Insert_Code

                /// Exec Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    SqlCommand cmd = GetCommand(EventInfoSCT_DAO.Insert);

                    cmd.Parameters.Add(new SqlParameter(EventInfoSCT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new SqlParameter(EventInfoSCT_DAO.EventCodeParam, Row.EventCode));
                    cmd.Parameters.Add(new SqlParameter(EventInfoSCT_DAO.EventNoParam, Row.EventNo));
                    cmd.Parameters.Add(new SqlParameter(EventInfoSCT_DAO.LabelParam, Row.Label));
                    cmd.Parameters.Add(new SqlParameter(EventInfoSCT_DAO.MaxEventCountParam, Row.MaxEventCount));
                    cmd.Parameters.Add(new SqlParameter(EventInfoSCT_DAO.ConfigParam, Row.EventGroupId));

                    cmd.Parameters.Add(new SqlParameter(EventInfoSCT_DAO.CautionNumberParam, Row.IsNull("CautionNumber") ?
                                      DBNull.Value : (object)Row.CautionNumber) { SqlDbType = SqlDbType.Int });
                    cmd.Parameters.Add(new SqlParameter(EventInfoSCT_DAO.FlashTimeParam, Row.IsNull("FlashTime") ?
                                      DBNull.Value : (object)Row.FlashTime) { SqlDbType = SqlDbType.Int });

                    int res = cmd.ExecuteNonQuery();
                    /// Row.AcceptChanges();
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving Event Profile Info data in data source", ex);
            }
        }

        public void Update_EventProfileInfo(Configs.EventInfoRow Row)
        {
            try
            {
                #region Update_Code

                // Exec Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    SqlCommand cmd = GetCommand(EventInfoSCT_DAO.Update);
                    // Parameters Reflecting Changes
                    cmd.Parameters.Add(new SqlParameter(EventInfoSCT_DAO.EventNoParam, Row.EventNo));
                    cmd.Parameters.Add(new SqlParameter(EventInfoSCT_DAO.EventCodeParam, Row.EventCode));
                    cmd.Parameters.Add(new SqlParameter(EventInfoSCT_DAO.LabelParam, Row.Label));
                    cmd.Parameters.Add(new SqlParameter(EventInfoSCT_DAO.MaxEventCountParam, Row.MaxEventCount));

                    cmd.Parameters.Add(new SqlParameter(EventInfoSCT_DAO.CautionNumberParam, Row.IsNull("CautionNumber") ?
                                      DBNull.Value : (object)Row.CautionNumber) { SqlDbType = SqlDbType.Int });
                    cmd.Parameters.Add(new SqlParameter(EventInfoSCT_DAO.FlashTimeParam, Row.IsNull("FlashTime") ?
                                      DBNull.Value : (object)Row.FlashTime) { SqlDbType = SqlDbType.Int });

                    // Parameters Before Changes
                    cmd.Parameters.Add(new SqlParameter(EventInfoSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(EventInfoSCT_DAO.EventCodeParam + "_", Convert.ToInt32(Row["EventCode", DataRowVersion.Original])));

                    int res = cmd.ExecuteNonQuery();
                    // Row.AcceptChanges();
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating Event Profile Info data in data source", ex);
            }
        }

        public void Delete_EventProfileInfo(Configs.EventInfoRow Row)
        {
            try
            {
                #region Delete_Code

                if (Row.RowState == DataRowState.Deleted)
                {
                    SqlCommand cmd = GetCommand(EventInfoSCT_DAO.Delete);

                    cmd.Parameters.Add(new SqlParameter(EventInfoSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new SqlParameter(EventInfoSCT_DAO.EventCodeParam + "_", Convert.ToInt32(Row["EventCode", DataRowVersion.Original])));

                    int res = cmd.ExecuteNonQuery();
                    // Row.AcceptChanges();
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting Event Profile Info data in data source", ex);
            }
        }

        #endregion
    }
    #endregion

    #region Event Logs

    public class EventLogsSCT_DAO : SCT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from Event_Logs order by id,EventLogIndex";

        public static readonly string Insert = "INSERT INTO Event_Logs (id, EventLogIndex, EventCounterIndex,EventGroupId) " +
                                                "VALUES (@id, @eventLogIndex, @eventCounterIndex,@eventGroupId)";

        public static readonly string Update = "UPDATE Event_Logs SET EventLogIndex = @eventLogIndex, EventCounterIndex = @eventCounterIndex,EventGroupId = @eventGroupId " +
                                                "WHERE ((id IS NULL) OR (id = @id_))";

        public static readonly string Delete = "DELETE FROM Event_Logs " +
                                                "WHERE ((id IS NULL) OR (id = @id_))";

        /// Named Parameters
        public static readonly string IdParam = "@id";
        public static readonly string EventLogOBISIndexParam = "@eventLogIndex";
        public static readonly string EventCounterOBISIndexParam = "@eventCounterIndex";
        public static readonly string ConfigParam = "@eventGroupId";

        #endregion

        public EventLogsSCT_DAO(string connectionString)
            : base(connectionString) { }
        public EventLogsSCT_DAO(IDbConnection connection)
            : base(connection) { }

        public void Load_EventLogsInfo(Configs DataSet)
        {
            try
            {
                SqlDataAdapter Event_Profile_Info_DataAdapeter =
                    new SqlDataAdapter(new SqlCommand(Select, (SqlConnection)DataBaseConnection));
                //DataSet.MeterTypeInfo.Clear();
                Event_Profile_Info_DataAdapeter.Fill(DataSet.EventLogs);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading Event Log Profile Entries", ex);
            }
        }

        public void Update_EventLogsInfo(Configs DataSet)
        {
            try
            {
                SqlCommand Db_Sel_Cmd = new SqlCommand(Select, (SqlConnection)DataBaseConnection);
                //DataSet.MeterTypeInfo.Clear();
                this.Update(DataSet.EventLogs, Db_Sel_Cmd);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating Event Profile Entries", ex);
            }
        }

        public void AcceptChanges_EventProfileInfo(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.EventLogsRow> EventsLogList = DataSet.EventLogs.ToList<Configs.EventLogsRow>();
                for (int index = 0; (EventsLogList != null && index < EventsLogList.Count); index++)
                {
                    Configs.EventLogsRow dtRow = EventsLogList[index];
                    #region Insert_Code
                    /// Exec Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        Insert_EventLog(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        Update_EventLog(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        Delete_EventLog(dtRow);
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating Event Profile Info in DataStore", ex);
            }
        }

        #region CRUD_Method

        public void Insert_EventLog(Configs.EventLogsRow Row)
        {
            try
            {
                #region Insert_Code

                /// Exec Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    SqlCommand cmd = GetCommand(EventLogsSCT_DAO.Insert);
                    cmd.Parameters.Add(new SqlParameter(EventLogsSCT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new SqlParameter(EventLogsSCT_DAO.EventLogOBISIndexParam, Row.EventLogIndex));
                    cmd.Parameters.Add(new SqlParameter(EventLogsSCT_DAO.EventCounterOBISIndexParam, Row.EventCounterIndex));
                    cmd.Parameters.Add(new SqlParameter(EventLogsSCT_DAO.ConfigParam, Row.EventGroupId));
                    int res = cmd.ExecuteNonQuery();
                    /// Row.AcceptChanges();
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving Event Logs data in data source", ex);
            }
        }

        public void Update_EventLog(Configs.EventLogsRow Row)
        {
            try
            {
                #region Update_Code

                ///Exec Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    SqlCommand cmd = GetCommand(EventLogsSCT_DAO.Update);
                    ///Parameters Reflecting Changes
                    cmd.Parameters.Add(new SqlParameter(EventLogsSCT_DAO.EventLogOBISIndexParam, Row.EventLogIndex));
                    cmd.Parameters.Add(new SqlParameter(EventLogsSCT_DAO.EventCounterOBISIndexParam, Row.EventCounterIndex));
                    cmd.Parameters.Add(new SqlParameter(EventLogsSCT_DAO.ConfigParam, Row.EventGroupId));

                    /// Parameters Before Changes
                    cmd.Parameters.Add(new SqlParameter(EventLogsSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

                    /// cmd.Parameters.Add(new OleDbParameter(EventLogsSCT_DAO.EventLogOBISIndexParam + "_", Convert.ToUInt64(Row["EventLogIndex", DataRowVersion.Original])));
                    /// cmd.Parameters.Add(new OleDbParameter(EventLogsSCT_DAO.EventCounterOBISIndexParam + "_", Convert.ToUInt64(Row["EventCounterIndex", DataRowVersion.Original])));
                    /// cmd.Parameters.Add(new OleDbParameter(EventLogsSCT_DAO.ConfigParam + "_", Convert.ToInt32(Row["ConfigId", DataRowVersion.Original])));

                    int res = cmd.ExecuteNonQuery();
                    /// Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating Event Logs data in data source", ex);
            }
        }

        public void Delete_EventLog(Configs.EventLogsRow Row)
        {
            try
            {
                #region Delete_Code

                if (Row.RowState == DataRowState.Deleted)
                {
                    SqlCommand cmd = GetCommand(EventLogsSCT_DAO.Delete);
                    cmd.Parameters.Add(new SqlParameter(EventLogsSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    int res = cmd.ExecuteNonQuery();
                    /// Row.AcceptChanges();
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting Event Logs data in data source", ex);
            }
        }

        #endregion
    }

    #endregion

    #region EventsGroup

    public class EventsGroupSCT_DAO : SCT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from Events_Group order by Events_group_Name desc";
        public static readonly string Insert = "INSERT INTO Events_Group (id, Events_group_Name) VALUES (@id, @Events_group_Name)";
        public static readonly string Update = "UPDATE Events_Group SET Events_group_Name = @Events_group_Name "
            + "WHERE id = @id_";
        public static readonly string Delete = "DELETE FROM Events_Group"
            + "WHERE id = @id_";

        public static readonly string IdParam = "@id";
        public static readonly string EVGroupName = "@Events_group_Name";

        #endregion

        public EventsGroupSCT_DAO(string connectionString)
            : base(connectionString)
        { }
        public EventsGroupSCT_DAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadEventsGroup(Configs DataSet)
        {
            try
            {
                SqlDataAdapter ConfigsDataAdapeter =
                    new SqlDataAdapter(new SqlCommand(Select, (SqlConnection)DataBaseConnection));
                //DataSet.MeterTypeInfo.Clear();
                ConfigsDataAdapeter.Fill(DataSet.Events_Group);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading Events group", ex);
            }
        }

        public void UpdateEventsGroup(Configs DataSet)
        {
            try
            {
                SqlCommand Select_Sql_Cmd = new SqlCommand(Select, (SqlConnection)DataBaseConnection);
                this.Update(DataSet.Events_Group, Select_Sql_Cmd);
                //DataSet.MeterTypeInfo.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating Events group", ex);
            }
        }

        public void AcceptChangesEventsGroup(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.Events_GroupRow> MeterUserRows = DataSet.Events_Group.ToList<Configs.Events_GroupRow>();
                for (int index = 0; (MeterUserRows != null && index < MeterUserRows.Count); index++)
                {
                    Configs.Events_GroupRow dtRow = MeterUserRows[index];
                    #region Insert_Code
                    ///Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertEventsGroup(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateEventsGroup(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteEventsGroup(dtRow);
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating Events Group", ex);
            }
        }

        #region CRUD_Methods

        public void InsertEventsGroup(Configs.Events_GroupRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    SqlCommand cmd = GetCommand(EventsGroupSCT_DAO.Insert);
                    cmd.Parameters.Add(new SqlParameter(EventsGroupSCT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new SqlParameter(EventsGroupSCT_DAO.EVGroupName, Row.Events_group_Name));
                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving EventsGroup data in data source", ex);
            }
        }

        public void UpdateEventsGroup(Configs.Events_GroupRow Row)
        {
            try
            {
                #region Update_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    SqlCommand cmd = GetCommand(EventsGroupSCT_DAO.Update);
                    /// Reflected Changes Parameters
                    cmd.Parameters.Add(new SqlParameter(EventsGroupSCT_DAO.EVGroupName, Row.Events_group_Name));
                    /// Before Changes Parameters
                    cmd.Parameters.Add(new SqlParameter(EventsGroupSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

                    int res = cmd.ExecuteNonQuery();

                    /// if (res > 0)
                    ///    Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying EventsGroup data in data source", ex);
            }
        }

        public void DeleteEventsGroup(Configs.Events_GroupRow Row)
        {
            try
            {
                #region Delete_Code

                /// Exec Insert Query
                if (Row.RowState == DataRowState.Deleted)
                {
                    SqlCommand cmd = GetCommand(EventsGroupSCT_DAO.Delete);
                    cmd.Parameters.Add(new SqlParameter(EventsGroupSCT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.ExecuteNonQuery();
                    /// Row.AcceptChanges();
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting EventsGroup data in data source", ex);
            }
        }

        #endregion
    }
    #endregion

    #region StatusWord

    public class StatusWordSCT_DAO : SCT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from Status_Word";
        public static readonly string Insert = "INSERT INTO Status_Word (Code, Name) VALUES (@Code, @Name)";
        public static readonly string Update = "UPDATE Status_Word SET Name = @Name "
            + "WHERE Code = @Code_";
        public static readonly string Delete = "DELETE FROM Status_Word "
            + "WHERE Code = @Code_";

        public static readonly string Code = "@Code";
        public static readonly string Name = "@Name";

        #endregion

        public StatusWordSCT_DAO(string connectionString)
            : base(connectionString)
        { }
        public StatusWordSCT_DAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadStatusWord(Configs DataSet)
        {
            try
            {
                SqlDataAdapter ConfigsDataAdapeter =
                    new SqlDataAdapter(new SqlCommand(Select, (SqlConnection)DataBaseConnection));
                //DataSet.MeterTypeInfo.Clear();
                ConfigsDataAdapeter.Fill(DataSet.Status_Word);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading Status Word", ex);
            }
        }

        public void AcceptChangesStatusWord(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.Status_WordRow> StatusWordRows = DataSet.Status_Word.ToList<Configs.Status_WordRow>();
                for (int index = 0; (StatusWordRows != null && index < StatusWordRows.Count); index++)
                {
                    Configs.Status_WordRow dtRow = StatusWordRows[index];
                    #region Insert_Code
                    ///Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertStatusWord(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateStatusWord(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteStatusWord(dtRow);
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating Events Group", ex);
            }
        }

        #region CRUD_Methods

        public void InsertStatusWord(Configs.Status_WordRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    SqlCommand cmd = GetCommand(StatusWordSCT_DAO.Insert);
                    cmd.Parameters.Add(new SqlParameter(StatusWordSCT_DAO.Code, Row.Code));
                    cmd.Parameters.Add(new SqlParameter(StatusWordSCT_DAO.Name, Row.Name));
                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving Status Word data in data source", ex);
            }
        }

        public void UpdateStatusWord(Configs.Status_WordRow Row)
        {
            try
            {
                #region Update_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    SqlCommand cmd = GetCommand(StatusWordSCT_DAO.Update);
                    /// Reflected Changes Parameters
                    cmd.Parameters.Add(new SqlParameter(StatusWordSCT_DAO.Name, Row.Name));
                    /// Before Changes Parameters
                    cmd.Parameters.Add(new SqlParameter(StatusWordSCT_DAO.Code + "_", Convert.ToInt32(Row["Code", DataRowVersion.Original])));

                    int res = cmd.ExecuteNonQuery();

                    /// if (res > 0)
                    ///    Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying Status Word data in data source", ex);
            }
        }

        public void DeleteStatusWord(Configs.Status_WordRow Row)
        {
            try
            {
                #region Delete_Code

                /// Exec Insert Query
                if (Row.RowState == DataRowState.Deleted)
                {
                    SqlCommand cmd = GetCommand(StatusWordSCT_DAO.Delete);
                    cmd.Parameters.Add(new SqlParameter(StatusWordSCT_DAO.Code + "_", Convert.ToInt32(Row["Code", DataRowVersion.Original])));
                    cmd.ExecuteNonQuery();
                    /// Row.AcceptChanges();
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting Status Word data in data source", ex);
            }
        }

        #endregion
    }
    #endregion

    #region CaptureObject_DAO

    //public class CaptureObjectSCT_DAO : SCT_DAO
    //{
    //    #region Data Members

    //    public static readonly string Select = "Select * from capture_objects order by SequenceId,Target_OBIS_Index";

    //    public static readonly string Insert = "INSERT INTO capture_objects (SequenceId,OBIS_Index,AttributeNo,DataIndex, ConfigId,GroupId,Target_OBIS_Index) "
    //        + "VALUES (@SequenceId,@OBIS_Index, @AttributeNo,@DataIndex,@groupId,@ConfigId,@Target_OBIS_Index)";

    //    //public static readonly string Update = "UPDATE capture_objects " +
    //    //    "SET OBIS_Index = @OBIS_Index, AttributeNo = @AttributeNo,DataIndex = @DataIndex,GroupId = @groupId,ConfigId =@ConfigId,Target_OBIS_Index = @Target_OBIS_Index  " +
    //    //    "WHERE ((OBIS_Index = @OBIS_Index_) AND ((@OBIS_Index_ = 1 AND Target_OBIS_Index IS NULL) OR (Target_OBIS_Index = @Target_OBIS_Index_)) AND ((GroupId IS NULL) OR (GroupId = @groupId_)))";

    //    public static readonly string Update = "UPDATE capture_objects " +
    //      " SET OBIS_Index = @OBIS_Index, AttributeNo = @AttributeNo,DataIndex = @DataIndex,GroupId = @groupId,ConfigId =@ConfigId,Target_OBIS_Index = @Target_OBIS_Index  " +
    //       " WHERE OBIS_Index =@OBIS_Index_ AND Target_OBIS_Index =  @Target_OBIS_Index_ AND GroupId = @groupId_ ";


    //    public static readonly string Delete = "DELETE FROM capture_objects " +
    //        " WHERE OBIS_Index =@OBIS_Index_ AND Target_OBIS_Index =  @Target_OBIS_Index_ AND GroupId = @groupId_ ";
        
    //    public static readonly string IdParam = "@id";
    //    public static readonly string OBISIndexParam = "@OBIS_Index";
    //    public static readonly string AttributeNoParam = "@AttributeNo";
    //    public static readonly string DataIndexParam = "@DataIndex";
    //    public static readonly string ConfigIdParam = "@ConfigId";
    //    public static readonly string GroupIdParam = "@groupId";
    //    public static readonly string TargetOBISIndexParam = "@Target_OBIS_Index";

    //    #endregion

    //    public CaptureObjectSCT_DAO(string connectionString) : base(connectionString) { }

    //    public CaptureObject_DAO(IDbConnection LocalConnection) : base(LocalConnection) { }

    //    public void Load_CaptureObject(Configs DataSet)
    //    {
    //        try
    //        {
    //            LoadDataTable(DataSet.CaptureObjects, GetCommand(Select));
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception("Error Loading CaptureBuffer data", ex);
    //        }
    //    }

    //    public void Update_CaptureObject(Configs DataSet)
    //    {
    //        try
    //        {
    //            UpdateDataTable(DataSet.CaptureObjects, GetCommand(Select));
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception("Error Updating CaptureBuffer data", ex);
    //        }
    //    }

    //    public void AcceptChanges_CaptureObject(Configs DataSet)
    //    {
    //        try
    //        {
    //            if (DataSet == null)
    //                return;
    //            List<Configs.CaptureObjectsRow> CaptureRightsList = DataSet.CaptureObjects.ToList<Configs.CaptureObjectsRow>();
    //            for (int index = 0; (CaptureRightsList != null && index < CaptureRightsList.Count); index++)
    //            {
    //                Configs.CaptureObjectsRow dtRow = CaptureRightsList[index];
    //                #region Insert_Code
    //                ///Exe Insert Query
    //                if (dtRow.RowState == DataRowState.Added)
    //                {
    //                    Insert_CaptureObjectRow(dtRow);
    //                }
    //                #endregion
    //                #region Update_Code
    //                else if (dtRow.RowState == DataRowState.Modified)
    //                {
    //                    Update_CaptureObjectRow(dtRow);
    //                }
    //                #endregion
    //                #region Delete_Code
    //                else if (dtRow.RowState == DataRowState.Deleted)
    //                {
    //                    Delete_CaptureObjectRow(dtRow);
    //                }
    //                #endregion
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception("Error Updating CaptureBuffer data", ex);
    //        }
    //    }

    //    #region CRUD_Method

    //    public void Insert_CaptureObjectRow(Configs.CaptureObjectsRow Row)
    //    {
    //        try
    //        {
    //            #region Insert_Code
    //            ///Exe Insert Query
    //            if (Row.RowState == DataRowState.Added)
    //            {
    //                using (SqlCommand cmd = (SqlCommand)GetCommand(CaptureObject_DAO.Insert))
    //                {
    //                    cmd.Parameters.Add(new SqlParameter(CaptureObject_DAO.IdParam, SqlDbType.Int)).Value = Row.SequenceId;
    //                    cmd.Parameters.Add(new SqlParameter(CaptureObject_DAO.OBISIndexParam, SqlDbType.BigInt)).Value = Row.OBIS_Index;
    //                    cmd.Parameters.Add(new SqlParameter(CaptureObject_DAO.AttributeNoParam, SqlDbType.TinyInt)).Value = Row.AttributeNo;
    //                    cmd.Parameters.Add(new SqlParameter(CaptureObject_DAO.DataIndexParam, SqlDbType.Int)).Value = Row.DataIndex;
    //                    cmd.Parameters.Add(new SqlParameter(CaptureObject_DAO.ConfigIdParam, SqlDbType.BigInt)).Value = Row.ConfigId;

    //                    if (Row.GroupId == 0)
    //                        cmd.Parameters.Add(new SqlParameter(CaptureObject_DAO.GroupIdParam, SqlDbType.BigInt)).Value = DBNull.Value;
    //                    else
    //                        cmd.Parameters.Add(new SqlParameter(CaptureObject_DAO.GroupIdParam, SqlDbType.BigInt)).Value = Row.GroupId;

    //                    cmd.Parameters.Add(new SqlParameter(CaptureObject_DAO.TargetOBISIndexParam, SqlDbType.BigInt)).Value = Row.Target_OBIS_Index;
    //                    int res = cmd.ExecuteNonQuery();
    //                }
    //                //Row.AcceptChanges();
    //            }
    //            #endregion
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception("Error saving CaptureBuffer data", ex);
    //        }
    //    }

    //    public void Update_CaptureObjectRow(Configs.CaptureObjectsRow Row)
    //    {
    //        try
    //        {
    //            #region Update_Code
    //            ///Exe Insert Query
    //            if (Row.RowState == DataRowState.Modified)
    //            {
    //                using (SqlCommand cmd = (SqlCommand)GetCommand(CaptureObject_DAO.Update))
    //                {
    //                    cmd.Parameters.Add(new SqlParameter(CaptureObject_DAO.IdParam, SqlDbType.Int)).Value = Row.SequenceId;
    //                    cmd.Parameters.Add(new SqlParameter(CaptureObject_DAO.OBISIndexParam, SqlDbType.Decimal)).Value = Row.OBIS_Index;
    //                    cmd.Parameters.Add(new SqlParameter(CaptureObject_DAO.AttributeNoParam, SqlDbType.TinyInt)).Value = Row.AttributeNo;
    //                    cmd.Parameters.Add(new SqlParameter(CaptureObject_DAO.DataIndexParam, SqlDbType.Int)).Value = Row.DataIndex;
    //                    cmd.Parameters.Add(new SqlParameter(CaptureObject_DAO.ConfigIdParam, SqlDbType.Int)).Value = Row.ConfigId;

    //                    if (Row.GroupId == 0)
    //                        cmd.Parameters.Add(new SqlParameter(CaptureObject_DAO.GroupIdParam, DBNull.Value) { SqlDbType = SqlDbType.Int });
    //                    else
    //                        cmd.Parameters.Add(new SqlParameter(CaptureObject_DAO.GroupIdParam, SqlDbType.Int)).Value = Row.GroupId;

    //                    cmd.Parameters.Add(new SqlParameter(CaptureObject_DAO.TargetOBISIndexParam, SqlDbType.Decimal)).Value = Row.Target_OBIS_Index;

    //                    ///Parameters Before Changes
    //                    cmd.Parameters.Add(new SqlParameter(CaptureObject_DAO.OBISIndexParam + "_", SqlDbType.Decimal)).Value =
    //                        Convert.ToUInt64(Row["OBIS_Index", DataRowVersion.Original]);
                        
    //                    cmd.Parameters.Add(new SqlParameter(CaptureObject_DAO.TargetOBISIndexParam + "_", SqlDbType.Decimal)).Value =
    //                        Convert.ToUInt64(Row["Target_OBIS_Index", DataRowVersion.Original]);
                        
    //                    cmd.Parameters.Add(new SqlParameter(CaptureObject_DAO.GroupIdParam + "_", SqlDbType.Int)).Value =
    //                        Convert.ToUInt64(Row["GroupId", DataRowVersion.Original]);
    //                    int res = cmd.ExecuteNonQuery();
    //                }
    //                //Row.AcceptChanges();
    //            }
    //            #endregion
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception("Error updating OBIS Codes data in data source", ex);
    //        }
    //    }

    //    public void Delete_CaptureObjectRow(Configs.CaptureObjectsRow Row)
    //    {
    //        try
    //        {
    //            #region Delete_Code
    //            ///Exe Insert Query
    //            if (Row.RowState == DataRowState.Deleted)
    //            {
    //                using (SqlCommand cmd = (SqlCommand)GetCommand(CaptureObject_DAO.Delete))
    //                {
    //                    cmd.Parameters.Add(new SqlParameter(CaptureObject_DAO.OBISIndexParam + "_", SqlDbType.Decimal)).Value =
    //                        Convert.ToUInt64(Row["OBIS_Index", DataRowVersion.Original]);
                        
    //                    cmd.Parameters.Add(new SqlParameter(CaptureObject_DAO.TargetOBISIndexParam + "_", SqlDbType.Decimal)).Value =
    //                        Convert.ToUInt64(Row["Target_OBIS_Index", DataRowVersion.Original]);
                        
    //                    cmd.Parameters.Add(new SqlParameter(CaptureObject_DAO.GroupIdParam + "_", SqlDbType.Int)).Value =
    //                        Convert.ToUInt64(Row["GroupId", DataRowVersion.Original]);
    //                    int res = cmd.ExecuteNonQuery();
    //                    //Row.AcceptChanges();
    //                }
    //            }
    //            #endregion
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception("Error deleting CaptureBuffer data", ex);
    //        }
    //    }

    //    #endregion
    //}

    #endregion

}
