using DatabaseConfiguration.DataSet;
using DLMS;
using DLMS.Comm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Odbc;
using System.Linq;

namespace DatabaseConfiguration.DataBase
{
    public class MDC_DBAccessLayer :MDC_DAO
    {
        public MDC_DBAccessLayer(string connectionString)
            : base(connectionString)
        {}

        #region Load All Data

        public void Load_All_Configurations(ref Configs AllDataSet)
        {
            try
            {
                // For Data Loading Set Configs DataSET 
                if (AllDataSet == null)
                    AllDataSet = new Configs();
                AllDataSet.Clear();

                OBISRightsGroupMDC_DAO OBIS_RightsGroupDAO = new OBISRightsGroupMDC_DAO(this.DataBaseConnection);
                OBIS_RightsGroupDAO.LoadOBISRightsGroup(AllDataSet);

                EventsGroupMDC_DAO egroupDAO = new EventsGroupMDC_DAO(this.DataBaseConnection);
                egroupDAO.LoadEventsGroup(AllDataSet);

                BillingItemGroupMDC_DAO bgroupDAO = new BillingItemGroupMDC_DAO(this.DataBaseConnection);
                bgroupDAO.LoadBillingItemGroup(AllDataSet);

                LoadProfileGroupMDC_DAO lgroupDAO = new LoadProfileGroupMDC_DAO(this.DataBaseConnection);
                lgroupDAO.LoadLoadProfileGroup(AllDataSet);

                ConfigurationMDC_DAO ConfigDAO = new ConfigurationMDC_DAO(this.DataBaseConnection);
                ConfigDAO.LoadMeterConfiguraion(AllDataSet);

                ManufacturerMDC_DAO manufacturerDAO = new ManufacturerMDC_DAO(this.DataBaseConnection);
                manufacturerDAO.LoadManufacturer(AllDataSet);

                AuthenticationTypeMDC_DAO authenticationTypeDAO = new AuthenticationTypeMDC_DAO(this.DataBaseConnection);
                authenticationTypeDAO.LoadAuthenticationType(AllDataSet);

                DeviceMDC_DAO deviceDAO = new DeviceMDC_DAO(this.DataBaseConnection);
                deviceDAO.LoadDevice(AllDataSet);

                DeviceAssociationMDC_DAO deviceAssociationDAO = new DeviceAssociationMDC_DAO(this.DataBaseConnection);
                deviceAssociationDAO.LoadDeviceAssociation(AllDataSet);

                OBISDetailsMDC_DAO ObisDetails = new OBISDetailsMDC_DAO(this.DataBaseConnection);
                ObisDetails.Load_ObisDetails(AllDataSet);

                OBIS_CodesMDC_DAO obisCodeDAO = new OBIS_CodesMDC_DAO(this.DataBaseConnection);
                obisCodeDAO.Load_OBISCodes(AllDataSet);

                //LoadProfileChannel_GroupDAO LoadProfile_ChannelDAO = new LoadProfileChannel_GroupDAO(OBIS_RightsGroupDAO.DataBaseConnection);
                //LoadProfile_ChannelDAO.Get_LoadProfileChannel_GroupDAO(AllDataSet);

                CaptureObjectMDC_DAO _CaptureObject_DAO = new CaptureObjectMDC_DAO(this.DataBaseConnection);
                _CaptureObject_DAO.Load_CaptureObject(AllDataSet);

                // Configurations
                // DisplayWindowDAO DispWinDAO = new DisplayWindowDAO(OBIS_RightsGroupDAO.DataBaseConnection);
                // DispWinDAO.Load_Display_Windows(AllDataSet);

                LoadProfileMDC_DAO LoadProfileDAO = new LoadProfileMDC_DAO(this.DataBaseConnection);
                LoadProfileDAO.Load_Profiles(AllDataSet);

                BillItemMDC_DAO BillDAO = new BillItemMDC_DAO(this.DataBaseConnection);
                BillDAO.Load_Billing_Items(AllDataSet);

                BillTariffMDC_DAO BillTariffDAO = new BillTariffMDC_DAO(this.DataBaseConnection);
                BillTariffDAO.Load_Billing_Tariff(AllDataSet);

                EventInfoMDC_DAO EventInfoDAO = new EventInfoMDC_DAO(this.DataBaseConnection);
                EventInfoDAO.Load_EventProfileInfo(AllDataSet);

                EventLogsMDC_DAO EventLogDAO = new EventLogsMDC_DAO(this.DataBaseConnection);
                EventLogDAO.Load_EventLogsInfo(AllDataSet);

                DataRelation[] childRel = new DataRelation[AllDataSet.OBIS_Rights.ChildRelations.Count];
                AllDataSet.OBIS_Rights.ChildRelations.CopyTo(childRel, 0);
                AllDataSet.OBIS_Rights.ChildRelations.Clear();

                OBISRithtsMDC_DAO OBIS_RightsDAO = new OBISRithtsMDC_DAO(this.DataBaseConnection);
                OBIS_RightsDAO.Load_OBIS_Rights(AllDataSet);

                RithtsMDC_DAO RightsDAO = new RithtsMDC_DAO(this.DataBaseConnection);
                RightsDAO.Load_Rights(AllDataSet);

                if (OBIS_RightsGroupDAO.DataBaseConnection != null)
                    OBIS_RightsGroupDAO.DataBaseConnection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading Meter Configurations," + ex.Message, ex);
            }
            finally
            {
                AllDataSet.AcceptChanges();
            }
        }

        public void Load_OBISCodes(Configs AllDataSet, Configs.Device_AssociationRow CurrentAssoc)
        {
            try
            {
                Configs.Obis_Rights_GroupRow OBIS_Rights_Group = null;

                if (CurrentAssoc == null || CurrentAssoc.IsObisRightGroupIdNull())
                    throw new ArgumentNullException("Current Device Association");

                OBIS_Rights_Group = CurrentAssoc.Obis_Rights_GroupRow;

                // Load OBIS_Rights
                OBISRithtsMDC_DAO OBIS_CodeRightsDAO = new OBISRithtsMDC_DAO(this.DataBaseConnection);
                OBIS_CodeRightsDAO.Load_OBISRightsByGroupInfo(AllDataSet, OBIS_Rights_Group.id);

                // Query the database for the rows to be deleted
                var OBISRightsByGroup =
                    from OBISRights in AllDataSet.OBIS_Rights//[Rashad].AsEnumerable<Configs.OBIS_RightsRow>()
                    where !OBISRights.IsObisRightGroupIdNull() &&
                           OBISRights.ObisRightGroupId == OBIS_Rights_Group.id
                    select OBISRights;

                // Load Rights
                RithtsMDC_DAO RightsDAO = new RithtsMDC_DAO(this.DataBaseConnection);
                RightsDAO.Load_RightsByOBISRightId(AllDataSet, OBISRightsByGroup.ToList<Configs.OBIS_RightsRow>());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Update Data

        public void Update_All_Configuration(Configs AllDataSet)
        {
            // DataContext DtTransaction = null;
            try
            {
                if (this.DataBaseConnection.State == ConnectionState.Closed)
                    this.DataBaseConnection.Open();
                //Update Data In DataSets
                using (IDbTransaction transection = this.DataBaseConnection.BeginTransaction(IsolationLevel.ReadCommitted))
                {

                    Configs Dt = new Configs();
                    AllDataSet.EnforceConstraints = false;
                    Dt.EnforceConstraints = false;
                    Dt = (Configs)AllDataSet.GetChanges();

                    EventsGroupMDC_DAO egroupDAO = new EventsGroupMDC_DAO(this.DataBaseConnection);
                    egroupDAO.Transaction = transection;
                    egroupDAO.AcceptChangesEventsGroup(Dt);

                    BillingItemGroupMDC_DAO bgroupDAO = new BillingItemGroupMDC_DAO(this.DataBaseConnection);
                    bgroupDAO.Transaction = transection;
                    bgroupDAO.AcceptChangesBillingItemGroup(Dt);

                    LoadProfileGroupMDC_DAO lgroupDAO = new LoadProfileGroupMDC_DAO(this.DataBaseConnection);
                    lgroupDAO.Transaction = transection;
                    lgroupDAO.AcceptChangesLoadProfileGroup(Dt);

                    OBISRightsGroupMDC_DAO oBISRightsGroupDAO = new OBISRightsGroupMDC_DAO(this.DataBaseConnection);
                    oBISRightsGroupDAO.Transaction = transection;
                    oBISRightsGroupDAO.AcceptChangesOBISRightsGroup(Dt);

                    ConfigurationMDC_DAO ConfigDAO = new ConfigurationMDC_DAO(this.DataBaseConnection);
                    ConfigDAO.Transaction = transection;
                    ConfigDAO.AcceptChangesConfiguration(Dt);

                    ManufacturerMDC_DAO manufacturerDAO = new ManufacturerMDC_DAO(this.DataBaseConnection);
                    manufacturerDAO.Transaction = transection;
                    manufacturerDAO.AcceptChangesManufacturer(Dt);

                    AuthenticationTypeMDC_DAO authenticationTypeDAO = new AuthenticationTypeMDC_DAO(this.DataBaseConnection);
                    authenticationTypeDAO.Transaction = transection;
                    authenticationTypeDAO.AcceptChangesAuthenticationType(Dt);

                    DeviceMDC_DAO deviceDAO = new DeviceMDC_DAO(this.DataBaseConnection);
                    deviceDAO.Transaction = transection;
                    deviceDAO.AcceptChangesDevice(Dt);

                    DeviceAssociationMDC_DAO deviceAssociationDAO = new DeviceAssociationMDC_DAO(this.DataBaseConnection);
                    deviceAssociationDAO.Transaction = transection;
                    deviceAssociationDAO.AcceptChangesDeviceAssociation(Dt);

                    OBISDetailsMDC_DAO ObisDetails = new OBISDetailsMDC_DAO(this.DataBaseConnection);
                    ObisDetails.Transaction = transection;
                    ObisDetails.AcceptChangesObisDetails(Dt);

                    OBIS_CodesMDC_DAO OBISCodeDAO = new OBIS_CodesMDC_DAO(this.DataBaseConnection);
                    OBISCodeDAO.Transaction = transection;
                    OBISCodeDAO.AcceptChanges_OBISCodes(Dt);

                    //LoadProfileDAO lpChannelsGroupDAO = new LoadProfileDAO(egroupDAO.DataBaseConnection);
                    //lpChannelsGroupDAO.DataBaseConnection = Conn;
                    //lpChannelsGroupDAO.Transaction = transection;
                    //lpChannelsGroupDAO.AcceptChanges_Load_Profiles(Dt);

                    CaptureObjectMDC_DAO _CaptureObject_DAO = new CaptureObjectMDC_DAO(this.DataBaseConnection);
                    _CaptureObject_DAO.Transaction = transection;
                    _CaptureObject_DAO.AcceptChanges_CaptureObject(Dt);

                    // DataRelation []childRel = new DataRelation[ AllDataSet.OBIS_Rights.ChildRelations.Count];
                    // AllDataSet.OBIS_Rights.ChildRelations.CopyTo(childRel, 0);
                    // AllDataSet.OBIS_Rights.ChildRelations.Clear();

                    // Dt = (Configs)AllDataSet.OBIS_Rights.DataSet.Copy();
                    OBISRithtsMDC_DAO OBIS_RightsDAO = new OBISRithtsMDC_DAO(this.DataBaseConnection);
                    OBIS_RightsDAO.Transaction = transection;
                    OBIS_RightsDAO.AcceptChangesOBIS_Rights(Dt);

                    // Dt = (Configs)AllDataSet.Rights.DataSet.Copy();
                    RithtsMDC_DAO RightsDAO = new RithtsMDC_DAO(this.DataBaseConnection);
                    RightsDAO.Transaction = transection;
                    RightsDAO.AcceptChanges_Rights(Dt);

                    // DisplayWindowDAO DispWinDAO = new DisplayWindowDAO(egroupDAO.DataBaseConnection);
                    // DispWinDAO.DataBaseConnection = Conn;
                    // DispWinDAO.Transaction = transection;
                    // DispWinDAO.AcceptChanges_Display_Windows(Dt);

                    LoadProfileMDC_DAO LoadProfileDAO = new LoadProfileMDC_DAO(this.DataBaseConnection);
                    LoadProfileDAO.Transaction = transection;
                    LoadProfileDAO.AcceptChanges_Load_Profiles(Dt);

                    BillItemMDC_DAO BillDAO = new BillItemMDC_DAO(this.DataBaseConnection);
                    BillDAO.Transaction = transection;
                    BillDAO.AcceptChanges_BillingItems(Dt);

                    BillTariffMDC_DAO BillTariffDAO = new BillTariffMDC_DAO(this.DataBaseConnection);
                    BillTariffDAO.Transaction = transection;
                    BillTariffDAO.AcceptChanges_BillingTariff(Dt);

                    EventInfoMDC_DAO EventInfoDAO = new EventInfoMDC_DAO(this.DataBaseConnection);
                    EventInfoDAO.Transaction = transection;
                    EventInfoDAO.AcceptChanges_EventProfileInfo(Dt);

                    EventLogsMDC_DAO EventLogDAO = new EventLogsMDC_DAO(this.DataBaseConnection);
                    EventLogDAO.Transaction = transection;
                    EventLogDAO.AcceptChanges_EventProfileInfo(Dt);

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

        public void Update_AccessRights(Configs AllDataSet, string connectionString)
        {
            // DataContext DtTransaction = null;
            try
            {

                // Update Data In DataSets
                OBISRightsGroupMDC_DAO oBISRightsGroupDAO = new OBISRightsGroupMDC_DAO(connectionString);
                using (IDbTransaction transection = oBISRightsGroupDAO.DataBaseConnection.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    oBISRightsGroupDAO.Transaction = transection;
                    oBISRightsGroupDAO.AcceptChangesOBISRightsGroup(AllDataSet);

                    OBISDetailsMDC_DAO ObisDetails = new OBISDetailsMDC_DAO(connectionString);
                    ObisDetails.Transaction = transection;
                    ObisDetails.AcceptChangesObisDetails(AllDataSet);

                    OBIS_CodesMDC_DAO OBISCodeDAO = new OBIS_CodesMDC_DAO(connectionString);
                    OBISCodeDAO.Transaction = transection;
                    OBISCodeDAO.AcceptChanges_OBISCodes(AllDataSet);

                    OBISRithtsMDC_DAO OBIS_RightsDAO = new OBISRithtsMDC_DAO(connectionString);
                    OBIS_RightsDAO.Transaction = transection;
                    OBIS_RightsDAO.AcceptChangesOBIS_Rights(AllDataSet);

                    RithtsMDC_DAO RightsDAO = new RithtsMDC_DAO(connectionString);
                    RightsDAO.Transaction = transection;
                    RightsDAO.AcceptChanges_Rights(AllDataSet);

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

    public class MDC_DAO : IDisposable
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
            //        // Connection = Settings.Default.ConfigurationDBConnectionString;

            //        OdbcConnectionStringBuilder ConnectionString = new OdbcConnectionStringBuilder();
            //        ConnectionString["DSN"] = Settings.Default.MDC_DSN;
            //        ConnectionString["Pooling"] = true;
            //        ConnectionString["MinimumPoolSize"] = 20;
            //        ConnectionString["MaximumPoolsize"] = Convert.ToUInt32(Settings.Default.MaxPoolSize);
            //        ConnectionString["ConnectionReset"] = true;
            //        ConnectionString["ConnectionLifeTime"] = Convert.ToUInt32(Settings.Default.ConnectionLifeTime);

            //        Connection = ConnectionString.ConnectionString;

            //        // Check Configurations Modified Here
            //        return Connection;
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception("Error getting database connection string", ex);
            //    }
            //}
        }

        public MDC_DAO(string connectionString)
        {
            try
            {
                ConnectionString = connectionString;
                IDbConnection connection = new OdbcConnection(ConnectionString);
                connection.Open();
                DataBaseConnection = connection;
            }
            catch (Exception ex)
            {
                throw new Exception("Error building ConfigurationDB connection" + ex.Message, ex);
            }
        }

        //public MDC_DAO(string dsn)
        //{
        //    try
        //    {
        //        IDbConnection connection = new OdbcConnection(dsn);
        //        connection.Open();
        //        DbConnection = connection;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error building ConfigurationDB connection" + ex.Message, ex);
        //    }
        //}

        public MDC_DAO(IDbConnection Conn)
        {
            try
            {
                if (Conn != null && Conn.State != ConnectionState.Open)
                    Conn.Open();
                DataBaseConnection = Conn;
                // DbConnection = new SqlConnection(ConnectionString);
                // DbConnection.Open();
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

                    OdbcDataAdapter MeterConfigurationDataAdapter =
                        new OdbcDataAdapter((OdbcCommand)SelectSQLCmd);
                    OdbcCommandBuilder CommandBuilder = new OdbcCommandBuilder(MeterConfigurationDataAdapter);
                    // DataSet.MeterTypeInfo.Clear();
                    MeterConfigurationDataAdapter.UpdateCommand = (OdbcCommand)CommandBuilder.GetUpdateCommand(true);
                    MeterConfigurationDataAdapter.DeleteCommand = (OdbcCommand)CommandBuilder.GetDeleteCommand(true);
                    MeterConfigurationDataAdapter.InsertCommand = (OdbcCommand)CommandBuilder.GetInsertCommand(true);
                    MeterConfigurationDataAdapter.Update(AllDataSet);
                    dtContext.SubmitChanges(); //[Rashad]ConflictMode.ContinueOnConflict);
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
                OdbcDataAdapter DataAdapeter =
                    new OdbcDataAdapter((OdbcCommand)SelectSQLCmd);
                // DataSet.MeterTypeInfo.Clear();
                DataAdapeter.Fill(DataSet);
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
                OdbcDataAdapter ConfigsDataAdapeter =
                    new OdbcDataAdapter((OdbcCommand)select);
                // OdbcCommand cmd = new OdbcCommand(select.CommandText, (OdbcConnection) select.Connection);
                // OdbcDataReader reader = cmd.ExecuteReader();
                // DataTable dt = new DataTable();

                // ConfigsDataAdapeter.Fill(dt);
                ConfigsDataAdapeter.Fill(dbTable);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading data table", ex);
            }
        }

        // public IDbCommand GetCommand(string SqlQuery)
        // {
        //     try
        //     {
        //         OdbcCommand Command = new OdbcCommand(SqlQuery, DataBaseConnection);
        //         if (Transaction != null)
        //             Command.Transaction = Transaction;
        //         return Command;
        //     }
        //     catch (Exception ex)
        //     {
        //         throw ex;
        //     }
        // }

        public IDbCommand GetCommand(String SqlQuery)
        {
            try
            {
                OdbcCommand Command = new OdbcCommand(SqlQuery, (OdbcConnection)DataBaseConnection);
                if (Transaction != null)
                    Command.Transaction = (OdbcTransaction)Transaction;
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

                    OdbcDataAdapter MeterConfigurationDataAdapter =
                        new OdbcDataAdapter((OdbcCommand)SelectSQLCmd);
                    OdbcCommandBuilder CommandBuilder = new OdbcCommandBuilder(MeterConfigurationDataAdapter);
                    // DataSet.MeterTypeInfo.Clear();
                    MeterConfigurationDataAdapter.UpdateCommand = (OdbcCommand)CommandBuilder.GetUpdateCommand(true);
                    MeterConfigurationDataAdapter.DeleteCommand = (OdbcCommand)CommandBuilder.GetDeleteCommand(true);
                    MeterConfigurationDataAdapter.InsertCommand = (OdbcCommand)CommandBuilder.GetInsertCommand(true);
                    MeterConfigurationDataAdapter.Update(AllDataSet);
                    dtContext.SubmitChanges();//[Rashad] ConflictMode.ContinueOnConflict);
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

    #region ManufacturerDAO

    public class ManufacturerMDC_DAO : MDC_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from manufacturer order by id desc";
        public static readonly string Insert = "INSERT INTO manufacturer (id, Manufacturer_Name, Code) VALUES (?, ?, ?)";
        public static readonly string Update = "UPDATE manufacturer SET Manufacturer_Name = ? , Code = ? "
            + "WHERE id = ?";
        public static readonly string Delete = "DELETE FROM manufacturer "
            + "WHERE id = ?";

        public static readonly string IdParam = "@id";
        public static readonly string Manufacturer_Name = "@Manufacturer_Name";
        public static readonly string Code = "@Code";

        #endregion

        public ManufacturerMDC_DAO(string connectionString)
            : base(connectionString) { }
        public ManufacturerMDC_DAO(IDbConnection connection)
            : base(connection) { }
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
                IDbCommand Select_Sql_Cmd = GetCommand(Select);
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
                /// Exec Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    IDbCommand cmd = GetCommand(ManufacturerMDC_DAO.Insert);
                    cmd.Parameters.Add(new OdbcParameter(ManufacturerMDC_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OdbcParameter(ManufacturerMDC_DAO.Manufacturer_Name, Row.Manufacturer_Name));
                    cmd.Parameters.Add(new OdbcParameter(ManufacturerMDC_DAO.Code, Row.Code));
                    //cmd.Parameters.Add(new OdbcParameter(MeterTypeInfoDAO.FirmWareVersionParam, Row.FirmWareVersion));
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
                /// Exec Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    IDbCommand cmd = GetCommand(ManufacturerMDC_DAO.Update);
                    ///Reflected Changes Parameters
                    cmd.Parameters.Add(new OdbcParameter(ManufacturerMDC_DAO.Manufacturer_Name, Row.Manufacturer_Name));
                    cmd.Parameters.Add(new OdbcParameter(ManufacturerMDC_DAO.Code, Row.Code));

                    /// Parameters Before Modification
                    cmd.Parameters.Add(new OdbcParameter(ManufacturerMDC_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

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
                /// Exec Insert Query
                if (Row.RowState == DataRowState.Deleted)
                {
                    IDbCommand cmd = GetCommand(ManufacturerMDC_DAO.Delete);
                    cmd.Parameters.Add(new OdbcParameter(ManufacturerMDC_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
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

    #region DeviceDAO
    public class DeviceMDC_DAO : MDC_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from device order by Device_Name asc";
        public static readonly string Insert = "INSERT INTO device (id, Device_Name, Model, Manufacturer_Id,IsSinglePhase) VALUES (?,?,?,?,?)";
        public static readonly string Update = "UPDATE device SET Device_Name = ? , Model = ?, Manufacturer_Id = ?, IsSinglePhase = ? "
            + "WHERE id = ?";
        public static readonly string Delete = "DELETE FROM device "
            + "WHERE id = ?";

        public static readonly string IdParam = "@id";
        public static readonly string Device_Name = "@Device_Name";
        public static readonly string Model = "@Model";
        public static readonly string Manufacturer_Id = "@Manufacturer_Id";
        public static readonly string IsSinglePhase = "@IsSinglePhase";

        #endregion

        public DeviceMDC_DAO(string connectionString)
            : base(connectionString)
        { }
        public DeviceMDC_DAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadDevice(Configs DataSet)
        {
            try
            {
                LoadDataTable(DataSet.Device, GetCommand(Select));
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
                IDbCommand Select_Sql_Cmd = GetCommand(Select);
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
                    IDbCommand cmd = GetCommand(DeviceMDC_DAO.Insert);
                    cmd.Parameters.Add(new OdbcParameter(DeviceMDC_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OdbcParameter(DeviceMDC_DAO.Device_Name, Row.Device_Name));
                    cmd.Parameters.Add(new OdbcParameter(DeviceMDC_DAO.Model, Row.Model));
                    cmd.Parameters.Add(new OdbcParameter(DeviceMDC_DAO.Manufacturer_Id, Row.Manufacturer_Id));
                    cmd.Parameters.Add(new OdbcParameter(DeviceMDC_DAO.IsSinglePhase, Row.IsSinglePhase));
                    //cmd.Parameters.Add(new OdbcParameter(MeterTypeInfoDAO.FirmWareVersionParam, Row.FirmWareVersion));
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
                    IDbCommand cmd = GetCommand(DeviceMDC_DAO.Update);
                    ///Reflected Changes Parameters
                    cmd.Parameters.Add(new OdbcParameter(DeviceMDC_DAO.Device_Name, Row.Device_Name));
                    cmd.Parameters.Add(new OdbcParameter(DeviceMDC_DAO.Model, Row.Model));
                    cmd.Parameters.Add(new OdbcParameter(DeviceMDC_DAO.Manufacturer_Id, Row.Manufacturer_Id));
                    cmd.Parameters.Add(new OdbcParameter(DeviceMDC_DAO.IsSinglePhase, Row.IsSinglePhase));

                    /// Parameters Before Modification
                    cmd.Parameters.Add(new OdbcParameter(DeviceMDC_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

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
                    IDbCommand cmd = GetCommand(DeviceMDC_DAO.Delete);
                    /// Parameters Before Deletion
                    cmd.Parameters.Add(new OdbcParameter(DeviceMDC_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
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

    #region AuthenticationTypeDAO

    public class AuthenticationTypeMDC_DAO : MDC_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from authentication_type order by Authentication_Type_Name asc";
        public static readonly string Insert = "INSERT INTO authentication_type (id, Authentication_Type_Name) VALUES (?,?)";
        public static readonly string Update = "UPDATE authentication_type SET Authentication_Type_Name = ? "
            + "WHERE id = ?";
        public static readonly string Delete = "DELETE FROM authentication_type "
            + "WHERE id = ?";

        public static readonly string IdParam = "@id";
        public static readonly string Authentication_Type_Name = "@Authentication_Type_Name";

        #endregion
        public AuthenticationTypeMDC_DAO(string connectionString)
            : base(connectionString)
        { }
        public AuthenticationTypeMDC_DAO(IDbConnection connection)
            : base(connection)
        { }
        public void LoadAuthenticationType(Configs DataSet)
        {
            try
            {
                LoadDataTable(DataSet.Authentication_Type, GetCommand(Select));
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
                IDbCommand Select_Sql_Cmd = GetCommand(Select);
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
                    IDbCommand cmd = GetCommand(AuthenticationTypeMDC_DAO.Insert);
                    cmd.Parameters.Add(new OdbcParameter(AuthenticationTypeMDC_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OdbcParameter(AuthenticationTypeMDC_DAO.Authentication_Type_Name, Row.Authentication_Type_Name));
                    //cmd.Parameters.Add(new OdbcParameter(MeterTypeInfoDAO.FirmWareVersionParam, Row.FirmWareVersion));
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
                    IDbCommand cmd = GetCommand(AuthenticationTypeMDC_DAO.Update);
                    ///Reflected Changes Parameters
                    cmd.Parameters.Add(new OdbcParameter(AuthenticationTypeMDC_DAO.Authentication_Type_Name, Row.Authentication_Type_Name));
                    /// Parameters Before Updation
                    cmd.Parameters.Add(new OdbcParameter(AuthenticationTypeMDC_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
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
                    IDbCommand cmd = GetCommand(AuthenticationTypeMDC_DAO.Delete);
                    /// Parameters Before Deletion
                    cmd.Parameters.Add(new OdbcParameter(AuthenticationTypeMDC_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
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

    #region DeviceAssociationDAO

    public class DeviceAssociationMDC_DAO : MDC_DAO
    {
        #region Data Members

        public static readonly string Select = "Select id,Association_Name, Authentication_Type_Id, Client_Sap, Meter_Sap,Device_Id,Configuration_Id,ObisRightGroupId,Reload_Config from device_association order by Association_Name asc";
        public static readonly string Insert = "INSERT INTO device_association (id,Association_Name, Authentication_Type_Id, Client_Sap, Meter_Sap,Device_Id,Configuration_Id,ObisRightGroupId) VALUES (?,?,?,?,?,?,?,?,?)";
        public static readonly string Update = "UPDATE device_association SET Association_Name= ?, Authentication_Type_Id = ? , Client_Sap = ?, Meter_Sap = ?, Device_Id = ?, Configuration_Id = ?, ObisRightGroupId = ?, "
                                                + " Reload_Config = ? WHERE id = ?";

        public static readonly string Delete = "DELETE FROM device_association "
                                                + "WHERE id = ?";

        public static readonly string IdParam = "@id";
        public static readonly string Association_Name = "@Association_Name";
        public static readonly string Authentication_Type_Id = "@Authentication_Type_Id";
        public static readonly string Client_Sap = "@Client_Sap";
        public static readonly string Meter_Sap = "@Meter_Sap";
        public static readonly string Device_Id = "@Device_Id";
        public static readonly string Configuration_Id = "@Configuration_Id";
        public static readonly string ObisRightGroupId = "@ObisRightGroupId";
        public static readonly string ReloadConfig = "@ReloadConfig";

        #endregion

        public DeviceAssociationMDC_DAO(string connectionString)
            : base(connectionString)
        { }
        public DeviceAssociationMDC_DAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadDeviceAssociation(Configs DataSet)
        {
            try
            {
                LoadDataTable(DataSet.Device_Association, GetCommand(Select));
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
                IDbCommand Select_Sql_Cmd = GetCommand(Select);
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
                    //Exe Insert Query
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
                    IDbCommand cmd = GetCommand(DeviceAssociationMDC_DAO.Insert);
                    cmd.Parameters.Add(new OdbcParameter(DeviceAssociationMDC_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OdbcParameter(DeviceAssociationMDC_DAO.Association_Name, Row.Association_Name));
                    cmd.Parameters.Add(new OdbcParameter(DeviceAssociationMDC_DAO.Authentication_Type_Id, Row.Authentication_Type_Id));
                    cmd.Parameters.Add(new OdbcParameter(DeviceAssociationMDC_DAO.Client_Sap, Row.Client_Sap));
                    cmd.Parameters.Add(new OdbcParameter(DeviceAssociationMDC_DAO.Meter_Sap, Row.Meter_Sap));
                    cmd.Parameters.Add(new OdbcParameter(DeviceAssociationMDC_DAO.Device_Id, Row.Device_Id));
                    cmd.Parameters.Add(new OdbcParameter(DeviceAssociationMDC_DAO.Configuration_Id, Row.Configuration_Id));
                    cmd.Parameters.Add(new OdbcParameter(DeviceAssociationMDC_DAO.ObisRightGroupId, Row.ObisRightGroupId));
                    cmd.Parameters.Add(new OdbcParameter(DeviceAssociationMDC_DAO.ReloadConfig, Row.Reload_Config));
                    int res = cmd.ExecuteNonQuery();

                    //Row.AcceptChanges();
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving DeviceAssociationDAO data in data source", ex);
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
                    IDbCommand cmd = GetCommand(DeviceAssociationMDC_DAO.Update);
                    ///Reflected Changes Parameters
                    cmd.Parameters.Add(new OdbcParameter(DeviceAssociationMDC_DAO.Association_Name, Row.Association_Name));
                    cmd.Parameters.Add(new OdbcParameter(DeviceAssociationMDC_DAO.Authentication_Type_Id, Row.Authentication_Type_Id));
                    cmd.Parameters.Add(new OdbcParameter(DeviceAssociationMDC_DAO.Client_Sap, Row.Client_Sap));
                    cmd.Parameters.Add(new OdbcParameter(DeviceAssociationMDC_DAO.Meter_Sap, Row.Meter_Sap));
                    cmd.Parameters.Add(new OdbcParameter(DeviceAssociationMDC_DAO.Device_Id, Row.Device_Id));
                    cmd.Parameters.Add(new OdbcParameter(DeviceAssociationMDC_DAO.Configuration_Id, Row.Configuration_Id));
                    cmd.Parameters.Add(new OdbcParameter(DeviceAssociationMDC_DAO.ObisRightGroupId, Row.ObisRightGroupId));
                    cmd.Parameters.Add(new OdbcParameter(DeviceAssociationMDC_DAO.ReloadConfig, Row.Reload_Config));

                    /// Parameters Before Updation
                    cmd.Parameters.Add(new OdbcParameter(DeviceAssociationMDC_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
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
                    IDbCommand cmd = GetCommand(DeviceAssociationMDC_DAO.Delete);
                    /// Parameters Before Deletion
                    cmd.Parameters.Add(new OdbcParameter(DeviceAssociationMDC_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
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

    public class ConfigurationMDC_DAO : MDC_DAO
    {
        #region Data Members

        public static readonly string Select = "Select id, Name, lp_channels_group_id, BillItemsGroupId, EventGroupId from configuration_new order by Name desc";

        public static readonly string Insert = "INSERT INTO configuration_new (id, Name, lp_channels_group_id, BillItemsGroupId, EventGroupId)" +
                                       "VALUES (?,?,?,?,?)";

        public static readonly string Update = "UPDATE configuration_new SET Name = ?, lp_channels_group_id = ?, " + " BillItemsGroupId = ?, EventGroupId = ? " +
                         "WHERE (id = ?)";

        public static readonly string Delete = "DELETE FROM configuration_new " +
                             "WHERE (id = ?)";

        public static readonly string IdParam = "@id";
        public static readonly string NameParam = "@name";
        public static readonly string LpChannelsGroupId = "@lp_channels_group_id";
        public static readonly string BillItemsGroupId = "@BillItemsGroupId";
        public static readonly string EventGroupId = "@EventGroupId";

        #endregion

        public ConfigurationMDC_DAO(string connectionString)
            : base(connectionString)
        { }
        public ConfigurationMDC_DAO(IDbConnection connection)
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
                IDbCommand Select_Sql_Cmd = GetCommand(Select);

                this.Update(AllDataSet.Configuration, Select_Sql_Cmd);
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
                    // Exec Insert Query
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
            try
            {
                #region Insert_Code
                /// Exec Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    IDbCommand cmd = GetCommand(ConfigurationMDC_DAO.Insert);
                    cmd.Parameters.Add(new OdbcParameter(ConfigurationMDC_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OdbcParameter(ConfigurationMDC_DAO.NameParam, Row.Name));
                    cmd.Parameters.Add(new OdbcParameter(ConfigurationMDC_DAO.LpChannelsGroupId, OdbcType.BigInt) { Value = (Row.Islp_channels_group_idNull()) ? DBNull.Value : (object)Row.lp_channels_group_id });
                    cmd.Parameters.Add(new OdbcParameter(ConfigurationMDC_DAO.BillItemsGroupId, OdbcType.BigInt) { Value = (Row.IsBillItemsGroupIdNull()) ? DBNull.Value : (object)Row.BillItemsGroupId });
                    cmd.Parameters.Add(new OdbcParameter(ConfigurationMDC_DAO.EventGroupId, OdbcType.BigInt) { Value = (Row.IsEventGroupIdNull()) ? DBNull.Value : (object)Row.EventGroupId });


                    cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving Configurations data in data source", ex);
            }
        }

        public void UpdateConfiguration(Configs.ConfigurationRow Row)
        {
            try
            {
                #region Update_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    IDbCommand cmd = GetCommand(ConfigurationMDC_DAO.Update);
                    ///Parameters Reflecting Changes

                    cmd.Parameters.Add(new OdbcParameter(ConfigurationMDC_DAO.NameParam, Row.Name));
                    cmd.Parameters.Add(new OdbcParameter(ConfigurationMDC_DAO.LpChannelsGroupId, OdbcType.BigInt) { Value = (Row.Islp_channels_group_idNull()) ? DBNull.Value : (object)Row.lp_channels_group_id });
                    cmd.Parameters.Add(new OdbcParameter(ConfigurationMDC_DAO.BillItemsGroupId, OdbcType.BigInt) { Value = (Row.IsBillItemsGroupIdNull()) ? DBNull.Value : (object)Row.BillItemsGroupId });
                    cmd.Parameters.Add(new OdbcParameter(ConfigurationMDC_DAO.EventGroupId, OdbcType.BigInt) { Value = (Row.IsEventGroupIdNull()) ? DBNull.Value : (object)Row.EventGroupId });

                    /// Parameters Before Modification
                    cmd.Parameters.Add(new OdbcParameter(ConfigurationMDC_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OdbcParameter(ConfigurationMDC_DAO.NameParam + "_", Convert.ToString(Row["Name", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OdbcParameter(ConfigurationMDC_DAO.LpChannelsGroupId + "_", OdbcType.BigInt)
                    {
                        Value = ((Row["lp_channels_group_id", DataRowVersion.Original]) == DBNull.Value) ? DBNull.Value :
                               (object)Convert.ToInt32(Row["lp_channels_group_id", DataRowVersion.Original])
                    });
                    cmd.Parameters.Add(new OdbcParameter(ConfigurationMDC_DAO.BillItemsGroupId + "_", OdbcType.BigInt)
                    {
                        Value = ((Row["BillItemsGroupId", DataRowVersion.Original]) == DBNull.Value) ? DBNull.Value :
                                 (object)Convert.ToInt32(Row["BillItemsGroupId", DataRowVersion.Original])
                    });

                    cmd.Parameters.Add(new OdbcParameter(ConfigurationMDC_DAO.EventGroupId + "_", OdbcType.BigInt)
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
        }

        public void DeleteConfiguration(Configs.ConfigurationRow Row)
        {
            try
            {
                #region Delete_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Deleted)
                {
                    IDbCommand cmd = GetCommand(ConfigurationMDC_DAO.Delete);
                    /// Parameters Before Modification
                    cmd.Parameters.Add(new OdbcParameter(ConfigurationMDC_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OdbcParameter(ConfigurationMDC_DAO.NameParam + "_", Convert.ToString(Row["Name", DataRowVersion.Original])));

                    cmd.Parameters.Add(new OdbcParameter(ConfigurationMDC_DAO.LpChannelsGroupId + "_", OdbcType.BigInt)
                    {
                        Value = ((Row["lp_channels_group_id", DataRowVersion.Original]) == DBNull.Value) ? DBNull.Value :
                               (object)Convert.ToInt32(Row["lp_channels_group_id", DataRowVersion.Original])
                    });


                    cmd.Parameters.Add(new OdbcParameter(ConfigurationMDC_DAO.BillItemsGroupId + "_", OdbcType.BigInt)
                    {
                        Value = ((Row["BillItemsGroupId", DataRowVersion.Original]) == DBNull.Value) ? DBNull.Value :
                                 (object)Convert.ToInt32(Row["BillItemsGroupId", DataRowVersion.Original])
                    });

                    cmd.Parameters.Add(new OdbcParameter(ConfigurationMDC_DAO.EventGroupId + "_", OdbcType.BigInt)
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
        }

        #endregion

    }

    #endregion

    #region OBIS Details

    public class OBISDetailsMDC_DAO : MDC_DAO
    {
        #region Data Members

        public static readonly string Select = "Select id, Obis_Code, Default_OBIS_Code, Device_Id from obis_details order by id";

        public static readonly string SelectByDeviceId = "Select id, Obis_Code, Default_OBIS_Code, Device_Id from obis_details order by id";

        public static readonly string Insert = "INSERT INTO obis_details (id, Obis_Code, Default_OBIS_Code, Device_Id) " +
                                      "VALUES (?,?,?,?)";

        public static readonly string Update = "UPDATE obis_details SET Obis_Code = ?, Default_OBIS_Code = ?, Device_Id = ? " +
             "WHERE id = ?";
        public static readonly string Delete = "DELETE FROM obis_details " +
               "WHERE id = ?";

        public static readonly string IdParam = "@id";
        public static readonly string Obis_Code = "@Obis_Code";
        public static readonly string Default_OBIS_Code = "@Default_OBIS_Code";
        public static readonly string Device_Id = "@Device_Id";

        #endregion

        public OBISDetailsMDC_DAO(string connectionString)
            : base(connectionString)
        {
        }
        public OBISDetailsMDC_DAO(IDbConnection connection)
            : base(connection)
        {
        }

        public void Load_OBISDetailsByDeviceId(Configs DataSet, int DeviceId)
        {
            try
            {
                if (DataSet == null)
                    return;

                #region // Clear Previously Loaded OBISDetails Of Current Device

                try
                {
                    // Query the database for the rows to be deleted
                    var deleteOBISDetail =
                        from OBISDetail in DataSet.OBIS_Details//[Rashad].AsEnumerable<Configs.OBIS_DetailsRow>()
                        where OBISDetail.Device_Id == DeviceId
                        select OBISDetail;

                    try
                    {
                        DataSet.EnforceConstraints = false;
                        foreach (var row in deleteOBISDetail)
                        {
                            if (row == null)
                                continue;
                            row.Delete();
                        }
                    }
                    finally
                    {
                        DataSet.EnforceConstraints = true;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error removing previous loaded OBIS Code Rights Info", ex);
                }

                #endregion

                IDbCommand SelectByDeviceId = GetCommand(OBISDetailsMDC_DAO.SelectByDeviceId);
                // Device Id
                SelectByDeviceId.Parameters.Add(new OdbcParameter(OBISDetailsMDC_DAO.Device_Id, DeviceId));
                LoadDataTable(DataSet.OBIS_Details, SelectByDeviceId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading OBIS Details Info", ex);
            }
        }

        public void Load_ObisDetails(Configs DataSet)
        {
            try
            {
                OdbcDataReader OBIS_Details_DataReader = new OdbcCommand(Select, (OdbcConnection)DataBaseConnection).ExecuteReader();

                DataSet.OBIS_Details.Rows.Clear();

                while (OBIS_Details_DataReader.Read())
                {
                    Configs.OBIS_DetailsRow row = DataSet.OBIS_Details.NewOBIS_DetailsRow();
                    row.id = Convert.ToInt32(OBIS_Details_DataReader.GetValue(0).ToString());
                    row.Obis_Code = Convert.ToDecimal(OBIS_Details_DataReader.GetValue(1).ToString());
                    row.Default_OBIS_Code = Convert.ToDecimal(OBIS_Details_DataReader.GetValue(2).ToString());
                    StOBISCode OBISCode = (Get_Index)row.Obis_Code;
                    row.OBIS_Quantity = OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                    row.Device_Id = Convert.ToInt32(OBIS_Details_DataReader.GetValue(3).ToString());
                    DataSet.OBIS_Details.Rows.Add(row);
                }
                OBIS_Details_DataReader.Close();

                //LoadDataTable(DataSet.OBIS_Details, GetCommand(Select));
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
                IDbCommand Sel_qury = GetCommand(Select);
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
                    // Exe Insert Query
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
                    IDbCommand cmd = GetCommand(OBISDetailsMDC_DAO.Insert);
                    cmd.Parameters.Add(new OdbcParameter(OBISDetailsMDC_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OdbcParameter(OBISDetailsMDC_DAO.Obis_Code, Row.Obis_Code));
                    cmd.Parameters.Add(new OdbcParameter(OBISDetailsMDC_DAO.Default_OBIS_Code, Row.Default_OBIS_Code));
                    cmd.Parameters.Add(new OdbcParameter(OBISDetailsMDC_DAO.Device_Id, Row.Device_Id));
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
                    IDbCommand cmd = GetCommand(OBISDetailsMDC_DAO.Update);
                    ///Parameters Reflecting Modification
                    cmd.Parameters.Add(new OdbcParameter(OBISDetailsMDC_DAO.Obis_Code, Row.Obis_Code));
                    cmd.Parameters.Add(new OdbcParameter(OBISDetailsMDC_DAO.Default_OBIS_Code, Row.Default_OBIS_Code));
                    cmd.Parameters.Add(new OdbcParameter(OBISDetailsMDC_DAO.Device_Id, Row.Device_Id));
                    ///Parameters Before Modification
                    cmd.Parameters.Add(new OdbcParameter(OBISDetailsMDC_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
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
                    IDbCommand cmd = GetCommand(OBISDetailsMDC_DAO.Delete);
                    cmd.Parameters.Add(new OdbcParameter(OBISDetailsMDC_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

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

    #region OBISRightsGroupDAO

    public class OBISRightsGroupMDC_DAO : MDC_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from obis_rights_group order by id asc";
        public static readonly string Insert = "INSERT INTO obis_rights_group (id, Group_Name,Update_Rights) VALUES (?,?,?)";
        public static readonly string Update = "UPDATE obis_rights_group SET Group_Name = ?, Update_Rights = ? "
            + "WHERE id = ?";
        public static readonly string Delete = "DELETE FROM obis_rights_group "
            + "WHERE id = ?";

        public static readonly string IdParam = "@id";
        public static readonly string Group_Name = "@Group_Name";
        public static readonly string Update_Rights = "@Update_Rights";

        #endregion

        public OBISRightsGroupMDC_DAO(string connectionString)
            : base(connectionString)
        { }
        public OBISRightsGroupMDC_DAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadOBISRightsGroup(Configs DataSet)
        {
            try
            {
                LoadDataTable(DataSet.Obis_Rights_Group, GetCommand(Select));
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
                IDbCommand Select_Sql_Cmd = GetCommand(Select);
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
                    // Exe Insert Query
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
                    IDbCommand cmd = GetCommand(OBISRightsGroupMDC_DAO.Insert);
                    cmd.Parameters.Add(new OdbcParameter(OBISRightsGroupMDC_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OdbcParameter(OBISRightsGroupMDC_DAO.Group_Name, Row.Group_Name));
                    cmd.Parameters.Add(new OdbcParameter(OBISRightsGroupMDC_DAO.Update_Rights, Row.Update_Rights));
                    //cmd.Parameters.Add(new OdbcParameter(MeterTypeInfoDAO.FirmWareVersionParam, Row.FirmWareVersion));
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
                    IDbCommand cmd = GetCommand(OBISRightsGroupMDC_DAO.Update);
                    ///Reflected Changes Parameters
                    cmd.Parameters.Add(new OdbcParameter(OBISRightsGroupMDC_DAO.Group_Name, Row.Group_Name));
                    cmd.Parameters.Add(new OdbcParameter(OBISRightsGroupMDC_DAO.Update_Rights, Row.Update_Rights));

                    /// Parameters Before Modification
                    cmd.Parameters.Add(new OdbcParameter(OBISRightsGroupMDC_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

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
                    IDbCommand cmd = GetCommand(OBISRightsGroupMDC_DAO.Delete);
                    cmd.Parameters.Add(new OdbcParameter(OBISRightsGroupMDC_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
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
    public class OBISRithtsMDC_DAO : MDC_DAO
    {
        #region Data Members

        public static readonly string Select = "Select id, OBIS_Index, Version,ObisRightGroupId from obis_rights order by id,OBIS_Index";

        public static readonly string SelectByGroupId = "Select id, OBIS_Index, Version,ObisRightGroupId from obis_rights WHERE (ObisRightGroupId = ?) order by id,OBIS_Index";

        public static readonly string SelectMaxId = "Select Max(id) from obis_rights";

        public static readonly string Insert = "INSERT INTO obis_rights (id, OBIS_Index, Version,ObisRightGroupId) " +
                                        "VALUES (?, ?, ?, ?)";

        public static readonly string Update = "UPDATE obis_rights SET id = ?, OBIS_Index = ?, Version = ?, ObisRightGroupId = ? " +
                                                "WHERE id = ?";

        public static readonly string Delete = "DELETE FROM obis_rights WHERE (id = ?)";


        public static readonly string IdParam = "@id";
        public static readonly string OBISIndexParam = "@oBIS_Index";
        public static readonly string VersionParam = "@version";
        public static readonly string ObisRightGroupId = "@ObisRightGroupId";

        #endregion

        public OBISRithtsMDC_DAO(string connectionString)
            : base(connectionString)
        {
        }
        public OBISRithtsMDC_DAO(IDbConnection connection)
            : base(connection)
        {
        }

        public void Load_OBIS_Rights(Configs DataSet)
        {
            try
            {
                LoadDataTable(DataSet.OBIS_Rights, GetCommand(Select));
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading OBIS Rights info", ex);
            }
        }

        public void Load_OBISRightsByGroupInfo(Configs DataSet, int LP_GroupInfo)
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
                        from OBISRights in DataSet.OBIS_Rights//[Rashad].AsEnumerable<Configs.OBIS_RightsRow>()
                        where !OBISRights.IsObisRightGroupIdNull() &&
                               OBISRights.ObisRightGroupId == LP_GroupInfo
                        select OBISRights;

                    try
                    {
                        DataSet.EnforceConstraints = false;
                        foreach (Configs.OBIS_RightsRow row in deleteOBISRights)
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
                    throw new Exception("Error removing previous loaded OBIS Code Rights Info", ex);
                }

                #endregion
                IDbCommand SelectBySAP = GetCommand(OBISRithtsMDC_DAO.SelectByGroupId);
                // Add Server SAP & Client SAP details
                SelectBySAP.Parameters.Add(new OdbcParameter(OBISRithtsMDC_DAO.ObisRightGroupId, LP_GroupInfo));
                LoadDataTable(DataSet.OBIS_Rights, SelectBySAP);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading OBIS Rights Info", ex);
            }
        }

        public long Load_MaxOBISRightId()
        {
            try
            {
                IDbCommand Select_MaxOBISCodeId = GetCommand(OBISRithtsMDC_DAO.SelectMaxId);
                IDataReader reader = Select_MaxOBISCodeId.ExecuteReader();
                bool isDataExists = reader.Read();
                long val = 0;
                if (isDataExists)
                {
                    val = Convert.ToInt64(reader[0]);
                }
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
                IDbCommand Sel_qury = GetCommand(Select);
                // DataSet.MeterTypeInfo.Clear();
                this.UpdateDataTable(DataSet.OBIS_Rights, Sel_qury);
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

                    // Exec Insert Query
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
                string msg = null;
                if (dtRow != null)
                {
                    StOBISCode dt = (DLMS.Get_Index)dtRow.OBIS_Index;
                    msg = dt.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                }
                throw new Exception(String.Format("Error Updating OBIS Rights {0}", msg), ex);
            }
        }

        #region CRUD_Method

        public void InsertOBIS_Rights(Configs.OBIS_RightsRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    OdbcCommand cmd = (OdbcCommand)GetCommand(OBISRithtsMDC_DAO.Insert);
                    cmd.Parameters.Add(OBISRithtsMDC_DAO.IdParam, OdbcType.BigInt).Value = Row.id;
                    cmd.Parameters.Add(OBISRithtsMDC_DAO.OBISIndexParam, OdbcType.BigInt, 20).Value = Row.OBIS_Index;
                    cmd.Parameters.Add(OBISRithtsMDC_DAO.VersionParam, OdbcType.TinyInt, 20).Value = Row.Version;
                    cmd.Parameters.Add(OBISRithtsMDC_DAO.ObisRightGroupId, OdbcType.BigInt).Value = Row.ObisRightGroupId;
                    cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving OBIS_Rights data in data source", ex);
            }
        }

        public void UpdateOBIS_Rights(Configs.OBIS_RightsRow Row)
        {
            try
            {
                #region Update_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    IDbCommand cmd = GetCommand(OBISRithtsMDC_DAO.Update);
                    ///Parameters Reflecting Modification
                    cmd.Parameters.Add(new OdbcParameter(OBISRithtsMDC_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OdbcParameter(OBISRithtsMDC_DAO.OBISIndexParam, Row.OBIS_Index));
                    cmd.Parameters.Add(new OdbcParameter(OBISRithtsMDC_DAO.VersionParam, Row.Version));
                    cmd.Parameters.Add(new OdbcParameter(OBISRithtsMDC_DAO.ObisRightGroupId, Row.ObisRightGroupId));
                    ///Parameters Before Modification
                    cmd.Parameters.Add(new OdbcParameter(OBISRithtsMDC_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

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

                // Exec Insert Query
                if (Row.RowState == DataRowState.Deleted)
                {
                    OdbcCommand cmd = (OdbcCommand)GetCommand(OBISRithtsMDC_DAO.Delete);
                    // cmd.Parameters.Add(new OdbcParameter(OBISRithtsDAO.IdParam, Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(OBISRithtsMDC_DAO.IdParam, OdbcType.BigInt).Value = Convert.ToInt32(Row["id", DataRowVersion.Original]);

                    cmd.ExecuteNonQuery();
                    // Row.AcceptChanges();
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

    public class RithtsMDC_DAO : MDC_DAO
    {
        #region Data Members

        public static readonly string Select = "Select id, type, SubId, value, OBIS_Right_Id from rights order by id,OBIS_Right_Id";
        public static readonly string SelectMaxId = "Select Max(id) from rights";
        public static readonly string SelectByOBISRightId = "Select id, type, SubId, value, OBIS_Right_Id from rights WHERE OBIS_Right_Id = ? order by id,OBIS_Right_Id ";
        public static readonly string Insert = "INSERT INTO rights (id, type, SubId, value, OBIS_Right_Id) " +
                                                " VALUES (?, ?, ?, ?, ?)";

        public static readonly string Update = "UPDATE rights SET id = ?, type = ?, SubId = ?, value = ?, "
                                               + "OBIS_Right_Id = ? " +
                                               "WHERE (id = ? AND OBIS_Right_Id = ?)";

        public static readonly string Delete = "DELETE FROM rights WHERE (id = ? AND OBIS_Right_Id = ?)";

        public static readonly string IdParam = "@id";
        public static readonly string TypeParam = "@type";
        public static readonly string SubIdParam = "@SubId";
        public static readonly string ValueParam = "@value";
        public static readonly string OBIS_RightsParam = "@OBIS_Rights_Id";

        #endregion

        public RithtsMDC_DAO(string connectionString)
            : base(connectionString) { }
        public RithtsMDC_DAO(IDbConnection connection)
            : base(connection) { }

        public void Load_Rights(Configs DataSet)
        {
            try
            {
                LoadDataTable(DataSet.Rights, GetCommand(Select));
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
                    IDbCommand SelectOBISRights = GetCommand(RithtsMDC_DAO.SelectByOBISRightId);
                    SelectOBISRights.Parameters.Add(new OdbcParameter(RithtsMDC_DAO.OBIS_RightsParam, OBISRight.id));
                    LoadDataTable(DataSet.Rights, SelectOBISRights);
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
                IDbCommand Select_MaxOBISCodeId = GetCommand(RithtsMDC_DAO.SelectMaxId);
                IDataReader reader = Select_MaxOBISCodeId.ExecuteReader(CommandBehavior.SingleResult);
                bool isDataExists = reader.Read();
                long val = 0;
                if (isDataExists)
                {
                    val = Convert.ToInt64(reader[0]);
                }
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
                IDbCommand Sel_qury = GetCommand(Select);
                // DataSet.MeterTypeInfo.Clear();
                this.UpdateDataTable(DataSet.Rights, Sel_qury);
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

                    // Exe Insert Query
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

                // Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    OdbcCommand cmd = (OdbcCommand)GetCommand(RithtsMDC_DAO.Insert);
                    cmd.Parameters.Add(RithtsMDC_DAO.IdParam, OdbcType.BigInt).Value = Row.id;
                    cmd.Parameters.Add(RithtsMDC_DAO.TypeParam, OdbcType.TinyInt).Value = Row.type;
                    cmd.Parameters.Add(RithtsMDC_DAO.SubIdParam, OdbcType.TinyInt).Value = Row.SubId;
                    cmd.Parameters.Add(RithtsMDC_DAO.ValueParam, OdbcType.TinyInt).Value = Row.value;
                    cmd.Parameters.Add(RithtsMDC_DAO.OBIS_RightsParam, OdbcType.BigInt).Value = Row.OBIS_Right_Id;
                    cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving Rights data in data source", ex);
            }
        }

        public void UpdateRights(Configs.RightsRow Row)
        {
            try
            {
                #region Update_Code
                // Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    IDbCommand cmd = GetCommand(RithtsMDC_DAO.Update);
                    ///Parameters Reflecting Change
                    cmd.Parameters.Add(new OdbcParameter(RithtsMDC_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OdbcParameter(RithtsMDC_DAO.TypeParam, Row.type));
                    cmd.Parameters.Add(new OdbcParameter(RithtsMDC_DAO.SubIdParam, Row.SubId));
                    cmd.Parameters.Add(new OdbcParameter(RithtsMDC_DAO.ValueParam, Row.value));
                    cmd.Parameters.Add(new OdbcParameter(RithtsMDC_DAO.OBIS_RightsParam, Row.OBIS_Right_Id));
                    ///Parameters Before Chagnge
                    cmd.Parameters.Add(new OdbcParameter(RithtsMDC_DAO.IdParam + "-", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OdbcParameter(RithtsMDC_DAO.OBIS_RightsParam + "-", Convert.ToInt64(Row["OBIS_Right_Id", DataRowVersion.Original])));

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
                // Exec Insert Query
                if (Row.RowState == DataRowState.Deleted)
                {
                    // Modified My_SQL Version
                    OdbcCommand cmd = (OdbcCommand)GetCommand(RithtsMDC_DAO.Delete);
                    cmd.Parameters.Add(RithtsMDC_DAO.IdParam, OdbcType.BigInt).Value = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    cmd.Parameters.Add(RithtsMDC_DAO.OBIS_RightsParam, OdbcType.BigInt).Value = Convert.ToInt64(Row["OBIS_Right_Id", DataRowVersion.Original]);
                    cmd.ExecuteNonQuery();
                    // Row.AcceptChanges();
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

    #region OBIS_Codes_DAO
    public class OBIS_CodesMDC_DAO : MDC_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from all_quantities order by Label asc";

        public static readonly string Insert = "INSERT INTO all_quantities (OBIS_Index, Label) VALUES (?,?)";

        public static readonly string Update = "UPDATE all_quantities " +
                                               "SET OBIS_Index = ?, Label = ? " +
                                               "WHERE (OBIS_Index = ?)";


        public static readonly string Delete = "DELETE FROM all_quantities " +
                                               "WHERE (OBIS_Index = ?)";

        public static readonly string OBISIndexParam = "@oBIS_Index";
        public static readonly string OBIS_LabelParam = "@obis_Label";

        #endregion

        public OBIS_CodesMDC_DAO(string connectionString)
            : base(connectionString) { }
        public OBIS_CodesMDC_DAO(IDbConnection connection)
            : base(connection) { }

        public void Load_OBISCodes(Configs DataSet)
        {
            try
            {
                 OdbcCommand cmd = new OdbcCommand(Select, (OdbcConnection) DataBaseConnection);
                 OdbcDataReader OBIS_Rights_DataReader = cmd.ExecuteReader();

                 DataSet.AllQuantities.Rows.Clear();

                 while (OBIS_Rights_DataReader.Read())
                 {
                     Configs.AllQuantitiesRow row = DataSet.AllQuantities.NewAllQuantitiesRow();
                     row.OBIS_Index = Convert.ToDecimal(OBIS_Rights_DataReader.GetValue(0).ToString());
                     row.Label = OBIS_Rights_DataReader.GetString(1);
                     StOBISCode OBISCode = (Get_Index)row.OBIS_Index;
                     row.Quantity_Code = OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                     row.Quantity_Name = OBISCode.OBISIndex.ToString();
                     DataSet.AllQuantities.Rows.Add(row);
                 }
                 OBIS_Rights_DataReader.Close();

                //LoadDataTable(DataSet.AllQuantities, GetCommand(Select));
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading OBIS Code Labels info", ex);
            }
        }

        public void Update_OBISCodes(Configs DataSet)
        {
            try
            {
                UpdateDataTable(DataSet.AllQuantities, GetCommand(Select));
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating OBIS Code Labels info", ex);
            }
        }

        public void AcceptChanges_OBISCodes(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.AllQuantitiesRow> OBIS_Rights_List = DataSet.AllQuantities.ToList<Configs.AllQuantitiesRow>();
                for (int index = 0; (OBIS_Rights_List != null && index < OBIS_Rights_List.Count); index++)
                {
                    Configs.AllQuantitiesRow dtRow = OBIS_Rights_List[index];
                    #region Insert_Code
                    ///Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        Insert_OBISCode(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        Update_OBISCode(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        Delete_OBISCode(dtRow);
                    }
                    #endregion
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating OBIS Codes", ex);
            }
        }

        #region CRUD_Method

        public void Insert_OBISCode(Configs.AllQuantitiesRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    IDbCommand cmd = GetCommand(OBIS_CodesMDC_DAO.Insert);
                    cmd.Parameters.Add(new OdbcParameter(OBIS_CodesMDC_DAO.OBISIndexParam, Row.OBIS_Index));
                    cmd.Parameters.Add(new OdbcParameter(OBIS_CodesMDC_DAO.OBIS_LabelParam, Row.Label));
                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving OBIS Codes data in data source", ex);
            }
        }

        public void Update_OBISCode(Configs.AllQuantitiesRow Row)
        {
            try
            {
                #region Update_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    IDbCommand cmd = GetCommand(OBIS_CodesMDC_DAO.Update);
                    ///Parameters Reflecting Changes
                    cmd.Parameters.Add(new OdbcParameter(OBIS_CodesMDC_DAO.OBISIndexParam, Row.OBIS_Index));
                    cmd.Parameters.Add(new OdbcParameter(OBIS_CodesMDC_DAO.OBIS_LabelParam, Row.Label));
                    ///Parameters Before Changes
                    cmd.Parameters.Add(new OdbcParameter(OBIS_CodesMDC_DAO.OBISIndexParam + "_", Convert.ToInt64(Row["OBIS_Index", DataRowVersion.Original])));
                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating OBIS Codes data in data source", ex);
            }
        }

        public void Delete_OBISCode(Configs.AllQuantitiesRow Row)
        {
            try
            {
                #region Delete_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Deleted)
                {
                    IDbCommand cmd = GetCommand(OBIS_CodesMDC_DAO.Delete);
                    cmd.Parameters.Add(new OdbcParameter(OBIS_CodesMDC_DAO.OBISIndexParam, Convert.ToInt64(Row["OBIS_Index", DataRowVersion.Original])));

                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting OBIS Code data in data source", ex);
            }
        }

        #endregion
    }
    #endregion

    #region CaptureObject_DAO

    public class CaptureObjectMDC_DAO : MDC_DAO
    {
        #region Data Members

        public static readonly string Select = "Select `SequenceId`,`OBIS_Index`,`AttributeNo`,`DataIndex`, `DeviceId`,`GroupId`,`Target_OBIS_Index` from `capture_objects` order by `SequenceId`,`Target_OBIS_Index`";

        public static readonly string Insert = "INSERT INTO `capture_objects` (`SequenceId`,`OBIS_Index`,`AttributeNo`,`DataIndex`, `DeviceId`,`GroupId`,`Target_OBIS_Index`) VALUES (?,?, ?,?,?,?,?)";

        public static readonly string Update = "UPDATE `capture_objects` " +
            "SET `SequenceId` = ?, `OBIS_Index` = ?, `AttributeNo` = ?,`DataIndex` = ?,`GroupId` = ?,`DeviceId` = ?,`Target_OBIS_Index` = ?  " +
           "WHERE (`OBIS_Index` = ? AND `DeviceId` = ? AND `GroupId` = ? AND `Target_OBIS_Index` = ?)";

        public static readonly string Delete = "DELETE FROM `capture_objects` " +
            "WHERE (`OBIS_Index` = ? AND `DeviceId` = ? AND `GroupId` = ? AND `Target_OBIS_Index` = ?)";

        public static readonly string IdParam = "@id";
        public static readonly string OBISIndexParam = "@OBIS_Index";
        public static readonly string AttributeNoParam = "@AttributeNo";
        public static readonly string DataIndexParam = "@DataIndex";
        public static readonly string DeviceIdParam = "@DeviceId";
        public static readonly string GroupIdParam = "@groupId";
        public static readonly string TargetOBISIndexParam = "@Target_OBIS_Index";
        //public static readonly string DatabaseFieldParam = "@DatabaseField";

        #endregion

        public CaptureObjectMDC_DAO(string connectionString)
            : base(connectionString) { }
        public CaptureObjectMDC_DAO(IDbConnection connection)
            : base(connection) { }

        public void Load_CaptureObject(Configs DataSet)
        {
            try
            {
                LoadDataTable(DataSet.CaptureObjects, GetCommand(Select));
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading CaptureBuffer data", ex);
            }
        }

        public void Update_CaptureObject(Configs DataSet)
        {
            try
            {
                UpdateDataTable(DataSet.CaptureObjects, GetCommand(Select));
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating CaptureBuffer data", ex);
            }
        }

        public void AcceptChanges_CaptureObject(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.CaptureObjectsRow> CaptureRightsList = DataSet.CaptureObjects.ToList<Configs.CaptureObjectsRow>();
                for (int index = 0; (CaptureRightsList != null &&
                                     index < CaptureRightsList.Count); index++)
                {
                    Configs.CaptureObjectsRow dtRow = CaptureRightsList[index];
                    #region Insert_Code

                    // Exec Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        Insert_CaptureObjectRow(dtRow);
                    }

                    #endregion
                    #region Update_Code

                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        Update_CaptureObjectRow(dtRow);
                    }

                    #endregion
                    #region Delete_Code

                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        Delete_CaptureObjectRow(dtRow);
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating CaptureBuffer data", ex);
            }
        }

        #region CRUD_Method

        public void Insert_CaptureObjectRow(Configs.CaptureObjectsRow Row)
        {
            try
            {
                #region Insert_Code

                // Exec Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    OdbcCommand cmd = (OdbcCommand)GetCommand(CaptureObjectMDC_DAO.Insert);
                    cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.IdParam, OdbcType.Int)).Value = Row.SequenceId;
                    cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.OBISIndexParam, OdbcType.BigInt)).Value = Row.OBIS_Index;
                    cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.AttributeNoParam, OdbcType.TinyInt)).Value = Row.AttributeNo;
                    cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.DataIndexParam, OdbcType.Int)).Value = Row.DataIndex;
                    cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.DeviceIdParam, OdbcType.BigInt)).Value = Row.DeviceId;

                    if (Row.IsGroupIdNull() || Row.GroupId == 0)
                        cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.GroupIdParam, OdbcType.BigInt)).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.GroupIdParam, OdbcType.BigInt)).Value = Row.GroupId;

                    cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.TargetOBISIndexParam, OdbcType.BigInt)).Value = Row.Target_OBIS_Index;
                    //cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.DatabaseFieldParam, Row.DatabaseField ?? string.Empty));
                    int res = cmd.ExecuteNonQuery();
                    // Row.AcceptChanges();
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving CaptureBuffer data", ex);
            }
        }

        public void Update_CaptureObjectRow(Configs.CaptureObjectsRow Row)
        {
            try
            {
                #region Update_Code

                // Exec Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    OdbcCommand cmd = (OdbcCommand)GetCommand(CaptureObjectMDC_DAO.Update);
                    cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.IdParam, OdbcType.Int)).Value = Row.SequenceId;
                    cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.OBISIndexParam, OdbcType.BigInt)).Value = Row.OBIS_Index;
                    cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.AttributeNoParam, OdbcType.TinyInt)).Value = Row.AttributeNo;
                    cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.DataIndexParam, OdbcType.Int)).Value = Row.DataIndex;

                    if (Row.IsGroupIdNull() || Row.GroupId == 0)
                        cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.GroupIdParam, DBNull.Value));
                    else
                        cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.GroupIdParam, OdbcType.BigInt)).Value = Row.GroupId;

                    cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.DeviceIdParam, OdbcType.BigInt)).Value = Row.DeviceId;
                    cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.TargetOBISIndexParam, OdbcType.BigInt)).Value = Row.Target_OBIS_Index;
                    //cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.DatabaseFieldParam, Row.DatabaseField));

                    // Parameters Before Changes
                    cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.OBISIndexParam + "_", OdbcType.BigInt)).Value =
                        Convert.ToInt64(Row["OBIS_Index", DataRowVersion.Original]);

                    cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.TargetOBISIndexParam + "_", OdbcType.BigInt)).Value =
                       Convert.ToInt32(Row["DeviceId", DataRowVersion.Original]);

                    cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.GroupIdParam + "_", OdbcType.BigInt)).Value =
                      Convert.ToInt64(Row["GroupId", DataRowVersion.Original]);
                    
                    cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.TargetOBISIndexParam + "_", OdbcType.BigInt)).Value =
                     Convert.ToInt64(Row["Target_OBIS_Index", DataRowVersion.Original]);

                    int res = cmd.ExecuteNonQuery();
                    // Row.AcceptChanges();
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating OBIS Codes data in data source", ex);
            }
        }

        public void Delete_CaptureObjectRow(Configs.CaptureObjectsRow Row)
        {
            try
            {
                #region Delete_Code

                // Exec Insert Query
                if (Row.RowState == DataRowState.Deleted)
                {
                    OdbcCommand cmd = (OdbcCommand)GetCommand(CaptureObjectMDC_DAO.Delete);

                    // Parameters Before Changes
                    cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.OBISIndexParam + "_", OdbcType.BigInt)).Value =
                        Convert.ToInt64(Row["OBIS_Index", DataRowVersion.Original]);

                    cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.TargetOBISIndexParam + "_", OdbcType.BigInt)).Value =
                       Convert.ToInt32(Row["DeviceId", DataRowVersion.Original]);

                    cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.GroupIdParam + "_", OdbcType.BigInt)).Value =
                        Convert.ToInt64(Row["GroupId", DataRowVersion.Original]);

                    cmd.Parameters.Add(new OdbcParameter(CaptureObjectMDC_DAO.TargetOBISIndexParam + "_", OdbcType.BigInt)).Value =
                    Convert.ToInt64(Row["Target_OBIS_Index", DataRowVersion.Original]);

                    int res = cmd.ExecuteNonQuery();
                    // Row.AcceptChanges();
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting CaptureBuffer data", ex);
            }
        }

        #endregion
    }

    #endregion

    #region Load_Profiles
    public class LoadProfileMDC_DAO : MDC_DAO
    {
        #region Data Members

        public static readonly string Select = "Select id, QuantityIndex, AttributeNo, Multiplier, SequenceId, FormatSpecifier, Unit, LoadProfileGroupId from load_profile_channels order by id,QuantityIndex,SequenceId";

        public static readonly string Insert = "INSERT INTO load_profile_channels (id, QuantityIndex, AttributeNo, Multiplier, SequenceId, FormatSpecifier, Unit, LoadProfileGroupId) " +
                                               "VALUES (?,?,?,?,?,?,?,?)";

        public static readonly string Update = "UPDATE load_profile_channels SET id = ?, QuantityIndex = ?, AttributeNo = ?, Multiplier = ?, " +
                                                "SequenceId = ?, FormatSpecifier = ?, Unit = ?, LoadProfileGroupId = ?  " +
                                                "WHERE ( QuantityIndex = ? AND LoadProfileGroupId = ? )";

        public static readonly string Delete = "DELETE FROM load_profile_channels " +
                                                 "WHERE ( QuantityIndex = ? AND LoadProfileGroupId = ? )";

        // Named Parameters
        public static readonly string IdParam = "@id";
        public static readonly string OBISIndexParam = "@quantityIndex";
        public static readonly string AttributeNoParam = "@attributeNo";
        public static readonly string MultiplierParam = "@multiplier";
        public static readonly string SequenceIdParam = "@sequenceId";
        public static readonly string FormatSpecifierParam = "@formatSpecifier";
        public static readonly string UnitParam = "@unit";
        public static readonly string LoadProfileGroupId = "@LoadProfileGroupId";

        #endregion

        public LoadProfileMDC_DAO(string connectionString)
            : base(connectionString) { }
        public LoadProfileMDC_DAO(IDbConnection connection)
            : base(connection) { }

        public void Load_Profiles(Configs DataSet)
        {
            try
            {
                LoadDataTable(DataSet.LoadProfileChannels, GetCommand(Select));
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
                UpdateDataTable(DataSet.LoadProfileChannels, GetCommand(Select));
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

                    // Exec Insert Query
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
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    IDbCommand cmd = GetCommand(LoadProfileMDC_DAO.Insert);
                    cmd.Parameters.Add(new OdbcParameter(LoadProfileMDC_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OdbcParameter(LoadProfileMDC_DAO.OBISIndexParam, Row.QuantityIndex));
                    cmd.Parameters.Add(new OdbcParameter(LoadProfileMDC_DAO.AttributeNoParam, Row.AttributeNo));
                    cmd.Parameters.Add(new OdbcParameter(LoadProfileMDC_DAO.MultiplierParam, Row.Multiplier));
                    cmd.Parameters.Add(new OdbcParameter(LoadProfileMDC_DAO.SequenceIdParam, Row.SequenceId));
                    cmd.Parameters.Add(new OdbcParameter(LoadProfileMDC_DAO.FormatSpecifierParam, Row.FormatSpecifier));
                    cmd.Parameters.Add(new OdbcParameter(LoadProfileMDC_DAO.UnitParam, Row.Unit));
                    cmd.Parameters.Add(new OdbcParameter(LoadProfileMDC_DAO.LoadProfileGroupId, Row.LoadProfileGroupId));
                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
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
                    IDbCommand cmd = GetCommand(LoadProfileMDC_DAO.Update);
                    ///Parameters Reflecting Changes
                    cmd.Parameters.Add(new OdbcParameter(LoadProfileMDC_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OdbcParameter(LoadProfileMDC_DAO.OBISIndexParam, Row.QuantityIndex));
                    cmd.Parameters.Add(new OdbcParameter(LoadProfileMDC_DAO.AttributeNoParam, Row.AttributeNo));
                    cmd.Parameters.Add(new OdbcParameter(LoadProfileMDC_DAO.MultiplierParam, Row.Multiplier));
                    cmd.Parameters.Add(new OdbcParameter(LoadProfileMDC_DAO.SequenceIdParam, Row.SequenceId));
                    cmd.Parameters.Add(new OdbcParameter(LoadProfileMDC_DAO.FormatSpecifierParam, Row.FormatSpecifier));
                    cmd.Parameters.Add(new OdbcParameter(LoadProfileMDC_DAO.UnitParam, Row.Unit));
                    cmd.Parameters.Add(new OdbcParameter(LoadProfileMDC_DAO.LoadProfileGroupId, Row.LoadProfileGroupId));
                    ///Parameters Before Changes
                    //cmd.Parameters.Add(new OdbcParameter(LoadProfileDAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OdbcParameter(LoadProfileMDC_DAO.OBISIndexParam + "_", Convert.ToInt64(Row["QuantityIndex", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OdbcParameter(LoadProfileMDC_DAO.LoadProfileGroupId + "_", Convert.ToInt32(Row["LoadProfileGroupId", DataRowVersion.Original])));

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
                    IDbCommand cmd = GetCommand(LoadProfileMDC_DAO.Delete);
                    //cmd.Parameters.Add(new OdbcParameter(LoadProfileDAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OdbcParameter(LoadProfileMDC_DAO.OBISIndexParam + "_", Convert.ToInt64(Row["QuantityIndex", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OdbcParameter(LoadProfileMDC_DAO.LoadProfileGroupId + "_", Convert.ToInt32(Row["LoadProfileGroupId", DataRowVersion.Original])));
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

    public class LoadProfileGroupMDC_DAO : MDC_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from loadprofile_group order by LoadProfile_Group_Name desc";
        public static readonly string Insert = "INSERT INTO loadprofile_group (id, LoadProfile_Group_Name) VALUES (?,?)";
        public static readonly string Update = "UPDATE loadprofile_group SET LoadProfile_Group_Name = ? "
            + "WHERE id = ?";
        public static readonly string Delete = "DELETE FROM loadprofile_group "
            + "WHERE id = ?";

        public static readonly string IdParam = "@id";
        public static readonly string LPGroupName = "@LoadProfile_Group_Name";

        #endregion
        public LoadProfileGroupMDC_DAO(string connectionString)
            : base(connectionString) { }
        public LoadProfileGroupMDC_DAO(IDbConnection connection)
            : base(connection) { }

        public void LoadLoadProfileGroup(Configs DataSet)
        {
            try
            {
                LoadDataTable(DataSet.LoadProfile_Group, GetCommand(Select));
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
                IDbCommand Select_Sql_Cmd = GetCommand(Select);
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
                    IDbCommand cmd = GetCommand(LoadProfileGroupMDC_DAO.Insert);
                    cmd.Parameters.Add(new OdbcParameter(LoadProfileGroupMDC_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OdbcParameter(LoadProfileGroupMDC_DAO.LPGroupName, Row.LoadProfile_Group_Name));
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
                    IDbCommand cmd = GetCommand(LoadProfileGroupMDC_DAO.Update);
                    ///Reflected Changes Parameters
                    cmd.Parameters.Add(new OdbcParameter(LoadProfileGroupMDC_DAO.LPGroupName, Row.LoadProfile_Group_Name));
                    ///Before Changes Parameters
                    cmd.Parameters.Add(new OdbcParameter(LoadProfileGroupMDC_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

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
                    IDbCommand cmd = GetCommand(LoadProfileGroupMDC_DAO.Delete);
                    cmd.Parameters.Add(new OdbcParameter(LoadProfileGroupMDC_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
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

    #region LoadProfileChannel_GroupDAO

    //public class LoadProfileChannel_GroupDAO : DAO
    //{
    //    #region Data Members

    //    public static readonly string Select = "Select `load_profile_group_id`, `channel_1`, `channel_2`, `channel_3`, `channel_4` from `load_profile_group` order by `load_profile_group_id`,`ConfigId`";

    //    public static readonly string Insert = "INSERT INTO `load_profile_group` (`load_profile_group_id`, `channel_1`, `channel_2`, `channel_3`, `channel_4`) " +
    //                                          "VALUES (?, ?, ?, ?, ?)";

    //    public static readonly string Update = "UPDATE load_profile_group SET `load_profile_group_id` = ?, `channel_1` = ?, " +
    //    "`channel_2` = ?, `channel_3` = ?, " +
    //    "`channel_4` = ? " +
    //     "WHERE (`load_profile_group` = ? )";

    //    public static readonly string Delete = "DELETE FROM `load_profile_group` " +
    //    "WHERE (`load_profile_group` = ? )";

    //    /// <summary>
    //    /// Named Parameters
    //    /// </summary>
    //    public static readonly string IdParam = "@load_profile_group_id";
    //    public static readonly string Channel_1_OBISIndexParam = "@channel_1";
    //    public static readonly string Channel_2_OBISIndexParam = "@channel_2";
    //    public static readonly string Channel_3_OBISIndexParam = "@channel_3";
    //    public static readonly string Channel_4_OBISIndexParam = "@channel_4";

    //    #endregion

    //    public LoadProfileChannel_GroupDAO() : base() { }

    //    public LoadProfileChannel_GroupDAO(IDbConnection LocalConnection) : base(LocalConnection) { }

    //    //public void Get_LoadProfileChannel_GroupDAO(Configs DataSet)
    //    //{
    //    //    try
    //    //    {
    //    //        LoadDataTable(DataSet.LoadProfileChannel_Group, GetCommand(Select));
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        throw new Exception("Error Loading Load Profile Channel Group Entries", ex);
    //    //    }
    //    //}

    //    //public void UpdateLoadProfileChannel_Group(Configs DataSet)
    //    //{
    //    //    try
    //    //    {
    //    //        UpdateDataTable(DataSet.LoadProfileChannel_Group, GetCommand(Update));
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        throw new Exception("Error Updating Load Profile Channel Group Entries", ex);
    //    //    }
    //    //}

    //    public void AcceptChanges_LoadProfileChannelGroup(Configs DataSet)
    //    {
    //        try
    //        {
    //            if (DataSet == null)
    //                return;
    //            List<Configs.LoadProfileChannel_GroupRow> LoadProfileList = DataSet.LoadProfileChannel_Group.ToList<Configs.LoadProfileChannel_GroupRow>();
    //            for (int index = 0; (LoadProfileList != null && index < LoadProfileList.Count); index++)
    //            {
    //                Configs.LoadProfileChannel_GroupRow dtRow = LoadProfileList[index];
    //                #region Insert_Code
    //                // Exe Insert Query
    //                if (dtRow.RowState == DataRowState.Added)
    //                {
    //                    Insert_LoadProfileChannelGroup(dtRow);
    //                }
    //                #endregion
    //                #region Update_Code
    //                else if (dtRow.RowState == DataRowState.Modified)
    //                {
    //                    Insert_LoadProfileChannelGroup(dtRow);
    //                }
    //                #endregion
    //                #region Delete_Code
    //                else if (dtRow.RowState == DataRowState.Deleted)
    //                {
    //                    Insert_LoadProfileChannelGroup(dtRow);
    //                }
    //                #endregion
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception("Error Updating Load Profiles", ex);
    //        }
    //    }

    //    #region CRUD_Method

    //    //public void Insert_LoadProfileChannelGroup(Configs.LoadProfileChannel_GroupRow Row)
    //    //{
    //    //    try
    //    //    {
    //    //        #region Insert_Code
    //    //        ///Exe Insert Query
    //    //        if (Row.RowState == DataRowState.Added)
    //    //        {
    //    //            OdbcCommand cmd = (OdbcCommand)GetCommand(LoadProfileChannel_GroupDAO.Insert);
    //    //            cmd.Parameters.Add(new OdbcParameter(LoadProfileChannel_GroupDAO.IdParam, OdbcType.BigInt)).Value = Row.load_profile_group_id;
    //    //            cmd.Parameters.Add(new OdbcParameter(LoadProfileChannel_GroupDAO.Channel_1_OBISIndexParam, OdbcType.BigInt)).Value = Row.Channel_1;
    //    //            cmd.Parameters.Add(new OdbcParameter(LoadProfileChannel_GroupDAO.Channel_2_OBISIndexParam, OdbcType.BigInt)).Value = Row.Channel_2;
    //    //            cmd.Parameters.Add(new OdbcParameter(LoadProfileChannel_GroupDAO.Channel_3_OBISIndexParam, OdbcType.BigInt)).Value = Row.Channel_3;
    //    //            cmd.Parameters.Add(new OdbcParameter(LoadProfileChannel_GroupDAO.Channel_4_OBISIndexParam, OdbcType.BigInt)).Value = Row.Channel_4;
    //    //            int res = cmd.ExecuteNonQuery();
    //    //            //Row.AcceptChanges();
    //    //        }
    //    //        #endregion
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        throw new Exception("Error saving Load Profile channel in data source", ex);
    //    //    }
    //    //}

    //    //public void Update_LoadProfileChannelGroup(Configs.LoadProfileChannel_GroupRow Row)
    //    //{
    //    //    try
    //    //    {
    //    //        #region Update_Code
    //    //        ///Exe Insert Query
    //    //        if (Row.RowState == DataRowState.Modified)
    //    //        {
    //    //            OdbcCommand cmd = (OdbcCommand)GetCommand(LoadProfileChannel_GroupDAO.Update);
    //    //            ///Parameters Reflecting Changes
    //    //            cmd.Parameters.Add(new OdbcParameter(LoadProfileChannel_GroupDAO.IdParam, OdbcType.BigInt)).Value = Row.load_profile_group_id;
    //    //            cmd.Parameters.Add(new OdbcParameter(LoadProfileChannel_GroupDAO.Channel_1_OBISIndexParam, OdbcType.BigInt)).Value = Row.Channel_1;
    //    //            cmd.Parameters.Add(new OdbcParameter(LoadProfileChannel_GroupDAO.Channel_2_OBISIndexParam, OdbcType.BigInt)).Value = Row.Channel_2;
    //    //            cmd.Parameters.Add(new OdbcParameter(LoadProfileChannel_GroupDAO.Channel_3_OBISIndexParam, OdbcType.BigInt)).Value = Row.Channel_3;
    //    //            cmd.Parameters.Add(new OdbcParameter(LoadProfileChannel_GroupDAO.Channel_4_OBISIndexParam, OdbcType.BigInt)).Value = Row.Channel_4;
    //    //            ///Parameters Before Changes
    //    //            cmd.Parameters.Add(new OdbcParameter(LoadProfileChannel_GroupDAO.IdParam + "_", Convert.ToInt32(Row["load_profile_group_id", DataRowVersion.Original])));
    //    //            int res = cmd.ExecuteNonQuery();
    //    //            //Row.AcceptChanges();
    //    //        }
    //    //        #endregion
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        throw new Exception("Error updating Load Profile data in data source", ex);
    //    //    }
    //    //}

    //    //public void Delete_LoadProfileChannelGroup(Configs.LoadProfileChannel_GroupRow Row)
    //    //{
    //    //    try
    //    //    {
    //    //        #region Delete_Code
    //    //        if (Row.RowState == DataRowState.Deleted)
    //    //        {
    //    //            OdbcCommand cmd = (OdbcCommand)GetCommand(LoadProfileChannel_GroupDAO.Update);
    //    //            cmd.Parameters.Add(new OdbcParameter(LoadProfileChannel_GroupDAO.IdParam + "_", Convert.ToInt32(Row["load_profile_group_id", DataRowVersion.Original])));
    //    //            int res = cmd.ExecuteNonQuery();
    //    //            //Row.AcceptChanges();
    //    //        }
    //    //        #endregion
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        throw new Exception("Error deleting Load Profile channel group data in data source", ex);
    //    //    }
    //    //}

    //    #endregion
    //}

    #endregion

    #region Billing Item
    public class BillItemMDC_DAO : MDC_DAO
    {
        #region Data Members

        public static readonly string Select = "Select id, Label, FormatSpecifier, Unit, Multiplier, SequenceId, BillItemGroupId from billing_items order by id,BillItemGroupId";

        public static readonly string Insert = "INSERT INTO billing_items (id, Label, FormatSpecifier, Unit, Multiplier, SequenceId, BillItemGroupId) " +
                                                "VALUES (?,?,?,?,?,?,?)";

        public static readonly string Update = "UPDATE billing_items SET id = ?, Label = ?, FormatSpecifier = ?, Unit = ?," +
                                               "Multiplier = ?, SequenceId = ?, BillItemGroupId = ? " +
                                               "WHERE (id = ? AND BillItemGroupId = ?)";

        public static readonly string Delete = "DELETE FROM billing_items " +
                                               "WHERE (id = ? AND BillItemGroupId = ?)";

        // Named Parameters
        public static readonly string IdParam = "@id";
        public static readonly string LabelParam = "@label";
        public static readonly string FormatSpecifierParam = "@formatSpecifier";
        public static readonly string MultiplierParam = "@multiplier";
        public static readonly string SequenceIdParam = "@sequenceId";
        public static readonly string UnitParam = "@unit";
        public static readonly string BillItemGroupId = "@BillItemGroupId";

        #endregion

        public BillItemMDC_DAO(string connectionString)
            : base(connectionString) { }
        public BillItemMDC_DAO(IDbConnection connection)
            : base(connection) { }

        public void Load_Billing_Items(Configs DataSet)
        {
            try
            {
                LoadDataTable(DataSet.BillingItems, GetCommand(Select));
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
                IDbCommand Event_Profile_Info_Sel_Cmd = GetCommand(Select);
                // DataSet.MeterTypeInfo.Clear();
                this.UpdateDataTable(DataSet.BillingItems, Event_Profile_Info_Sel_Cmd);
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
                    // Exe Insert Query
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
                    IDbCommand cmd = GetCommand(BillItemMDC_DAO.Insert);
                    cmd.Parameters.Add(new OdbcParameter(BillItemMDC_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OdbcParameter(BillItemMDC_DAO.LabelParam, Row.Label));
                    cmd.Parameters.Add(new OdbcParameter(BillItemMDC_DAO.FormatSpecifierParam, Row.FormatSpecifier));
                    cmd.Parameters.Add(new OdbcParameter(BillItemMDC_DAO.UnitParam, Row.Unit));
                    cmd.Parameters.Add(new OdbcParameter(BillItemMDC_DAO.MultiplierParam, Row.Multiplier));
                    cmd.Parameters.Add(new OdbcParameter(BillItemMDC_DAO.SequenceIdParam, Row.SequenceId));
                    cmd.Parameters.Add(new OdbcParameter(BillItemMDC_DAO.BillItemGroupId, Row.BillItemGroupId));
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
                    IDbCommand cmd = GetCommand(BillItemMDC_DAO.Update);
                    ///Parameters Reflecting Modifications
                    cmd.Parameters.Add(new OdbcParameter(BillItemMDC_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OdbcParameter(BillItemMDC_DAO.LabelParam, Row.Label));
                    cmd.Parameters.Add(new OdbcParameter(BillItemMDC_DAO.FormatSpecifierParam, Row.FormatSpecifier));
                    cmd.Parameters.Add(new OdbcParameter(BillItemMDC_DAO.UnitParam, Row.Unit));
                    cmd.Parameters.Add(new OdbcParameter(BillItemMDC_DAO.MultiplierParam, Row.Multiplier));
                    cmd.Parameters.Add(new OdbcParameter(BillItemMDC_DAO.SequenceIdParam, Row.SequenceId));
                    cmd.Parameters.Add(new OdbcParameter(BillItemMDC_DAO.BillItemGroupId, Row.BillItemGroupId));
                    ///Parameters Before Modifications
                    cmd.Parameters.Add(new OdbcParameter(BillItemMDC_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OdbcParameter(BillItemMDC_DAO.BillItemGroupId + "_", Convert.ToInt32(Row["BillItemGroupId", DataRowVersion.Original])));


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
                    IDbCommand cmd = GetCommand(BillItemMDC_DAO.Delete);
                    cmd.Parameters.Add(new OdbcParameter(BillItemMDC_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OdbcParameter(BillItemMDC_DAO.BillItemGroupId + "_", Convert.ToInt32(Row["BillItemGroupId", DataRowVersion.Original])));

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

    #region Billing Tariff Quantity
    public class BillTariffMDC_DAO : MDC_DAO
    {
        #region Data Members

        public static readonly string Select = "Select BillItemId, OBIS_Index, SequenceId,DatabaseField from bill_tariff_quantity";

        public static readonly string Insert = "INSERT INTO bill_tariff_quantity (BillItemId, OBIS_Index, SequenceId,DatabaseField) " +
                                               "VALUES (?,?,?,?)";

        public static readonly string Update = "UPDATE bill_tariff_quantity SET BillItemId = ?, OBIS_Index = ?, SequenceId = ?, DatabaseField = ? " +
                                               "WHERE (BillItemId = ? AND OBIS_Index = ? )";

        public static readonly string Delete = "DELETE FROM bill_tariff_quantity " +
                                               "WHERE (BillItemId = ? AND OBIS_Index = ? )";

        //Named Parameters

        public static readonly string BillIdParam = "@billItemId";
        public static readonly string OBISIndexParam = "@oBIS_Index";
        public static readonly string SequenceIdParam = "@sequenceId";
        public static readonly string DatabaseField = "@DatabaseField";
        #endregion

        public BillTariffMDC_DAO(string connectionString)
            : base(connectionString) { }
        public BillTariffMDC_DAO(IDbConnection connection)
            : base(connection) { }

        public void Load_Billing_Tariff(Configs DataSet)
        {
            try
            {
                LoadDataTable(DataSet.BillTariffQuantity, GetCommand(Select));
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
                IDbCommand Event_Profile_Info_Sel_Cmd = GetCommand(Select);
                //DataSet.MeterTypeInfo.Clear();
                this.UpdateDataTable(DataSet.BillTariffQuantity, Event_Profile_Info_Sel_Cmd);
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
                    // Exe Insert Query
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
                    IDbCommand cmd = GetCommand(BillTariffMDC_DAO.Insert);
                    cmd.Parameters.Add(new OdbcParameter(BillTariffMDC_DAO.BillIdParam, Row.BillItemId));
                    cmd.Parameters.Add(new OdbcParameter(BillTariffMDC_DAO.OBISIndexParam, Row.OBIS_Index));
                    cmd.Parameters.Add(new OdbcParameter(BillTariffMDC_DAO.SequenceIdParam, Row.SequenceId));
                    cmd.Parameters.Add(new OdbcParameter(BillTariffMDC_DAO.DatabaseField, Row.DatabaseField ?? string.Empty));
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
                    IDbCommand cmd = GetCommand(BillTariffMDC_DAO.Update);
                    ///Parameters Reflecting Changes
                    cmd.Parameters.Add(new OdbcParameter(BillTariffMDC_DAO.BillIdParam, Row.BillItemId));
                    cmd.Parameters.Add(new OdbcParameter(BillTariffMDC_DAO.OBISIndexParam, Row.OBIS_Index));
                    cmd.Parameters.Add(new OdbcParameter(BillTariffMDC_DAO.SequenceIdParam, Row.SequenceId));
                    cmd.Parameters.Add(new OdbcParameter(BillTariffMDC_DAO.DatabaseField, Row.DatabaseField));
                    ///Parameter Before Changes
                    cmd.Parameters.Add(new OdbcParameter(BillTariffMDC_DAO.BillIdParam + "_", Convert.ToInt32(Row["BillItemId", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OdbcParameter(BillTariffMDC_DAO.OBISIndexParam + "_", Convert.ToInt64(Row["OBIS_Index", DataRowVersion.Original])));

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
                    IDbCommand cmd = GetCommand(BillTariffMDC_DAO.Delete);
                    cmd.Parameters.Add(new OdbcParameter(BillTariffMDC_DAO.BillIdParam + "_", Convert.ToInt32(Row["BillItemId", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OdbcParameter(BillTariffMDC_DAO.OBISIndexParam + "_", Convert.ToInt64(Row["OBIS_Index", DataRowVersion.Original])));

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

    #region BillingItemGroup

    public class BillingItemGroupMDC_DAO : MDC_DAO
    {
        #region Data Members

        public static readonly string Select = "Select id, BillingItem_Group_Name from billingitem_group order by id";
        public static readonly string Insert = "INSERT INTO billingitem_group (id, BillingItem_Group_Name) VALUES (?,?)";
        public static readonly string Update = "UPDATE billingitem_group SET BillingItem_Group_Name = ? "
            + "WHERE id = ?";
        public static readonly string Delete = "DELETE FROM billingitem_group "
            + "WHERE id = ?";

        public static readonly string IdParam = "@id";
        public static readonly string BIGroupName = "@BillingItem_Group_Name";

        #endregion

        public BillingItemGroupMDC_DAO(string connectionString)
            : base(connectionString)
        { }
        public BillingItemGroupMDC_DAO(IDbConnection connection)
            : base(connection) { }
        public void LoadBillingItemGroup(Configs DataSet)
        {
            try
            {
                LoadDataTable(DataSet.BillingItem_Group, GetCommand(Select));
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
                IDbCommand Select_Sql_Cmd = GetCommand(Select);
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
                    // Exec Insert Query
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
                    IDbCommand cmd = GetCommand(BillingItemGroupMDC_DAO.Insert);
                    cmd.Parameters.Add(new OdbcParameter(BillingItemGroupMDC_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OdbcParameter(BillingItemGroupMDC_DAO.BIGroupName, Row.BillingItem_Group_Name));
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
                    IDbCommand cmd = GetCommand(BillingItemGroupMDC_DAO.Update);
                    ///Reflected Changes Parameters
                    cmd.Parameters.Add(new OdbcParameter(BillingItemGroupMDC_DAO.BIGroupName, Row.BillingItem_Group_Name));
                    ///Before Changes Parameters
                    cmd.Parameters.Add(new OdbcParameter(BillingItemGroupMDC_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

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
                    IDbCommand cmd = GetCommand(BillingItemGroupMDC_DAO.Delete);
                    cmd.Parameters.Add(new OdbcParameter(BillingItemGroupMDC_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
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

    #region Event Info

    public class EventInfoMDC_DAO : MDC_DAO
    {
        #region Data Members

        public static readonly string Select = "Select id, EventCode, Label, MaxEventCount, EventGroupId,EventNo from event_info order by id,EventCode,EventGroupId";

        public static readonly string Insert = "INSERT INTO event_info (id, EventCode, Label, MaxEventCount, EventGroupId,EventNo) " +
                                               "VALUES (?,?,?,?,?,?)";

        public static readonly string Update = "UPDATE event_info SET id = ?, EventCode = ?, Label = ?, MaxEventCount = ?, EventGroupId = ? ,EventNo = ? " +
                                               "WHERE (EventCode = ? AND EventGroupId = ?)";

        public static readonly string Delete = "DELETE FROM event_info " +
                                               "WHERE (EventCode = ? AND EventGroupId = ?)";

        // Named Parameters
        public static readonly string IdParam = "@id";
        public static readonly string EventCodeParam = "@eventCode";
        public static readonly string MaxEventCountParam = "@maxEventCode";
        public static readonly string LabelParam = "@label";
        public static readonly string EventGroupId = "@EventGroupId";
        public static readonly string EventNo = "@eventNo";

        #endregion

        public EventInfoMDC_DAO(string connectionString)
            : base(connectionString) { }
        public EventInfoMDC_DAO(IDbConnection connection)
            : base(connection) { }

        public void Load_EventProfileInfo(Configs DataSet)
        {
            try
            {
                LoadDataTable(DataSet.EventInfo, GetCommand(Select));
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
                UpdateDataTable(DataSet.EventInfo, GetCommand(Select));
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

                    // Exec Insert Query
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
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    IDbCommand cmd = GetCommand(EventInfoMDC_DAO.Insert);
                    cmd.Parameters.Add(new OdbcParameter(EventInfoMDC_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OdbcParameter(EventInfoMDC_DAO.EventCodeParam, Row.EventCode));
                    cmd.Parameters.Add(new OdbcParameter(EventInfoMDC_DAO.LabelParam, Row.Label));
                    cmd.Parameters.Add(new OdbcParameter(EventInfoMDC_DAO.MaxEventCountParam, Row.MaxEventCount));
                    cmd.Parameters.Add(new OdbcParameter(EventInfoMDC_DAO.EventGroupId, Row.EventGroupId));
                    cmd.Parameters.Add(new OdbcParameter(EventInfoMDC_DAO.EventNo, Row.EventNo));
                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
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
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    IDbCommand cmd = GetCommand(EventInfoMDC_DAO.Update);
                    ///Parameters Reflecting Changes
                    cmd.Parameters.Add(new OdbcParameter(EventInfoMDC_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OdbcParameter(EventInfoMDC_DAO.EventCodeParam, Row.EventCode));
                    cmd.Parameters.Add(new OdbcParameter(EventInfoMDC_DAO.LabelParam, Row.Label));
                    cmd.Parameters.Add(new OdbcParameter(EventInfoMDC_DAO.MaxEventCountParam, Row.MaxEventCount));
                    cmd.Parameters.Add(new OdbcParameter(EventInfoMDC_DAO.EventGroupId, Row.EventGroupId));
                    cmd.Parameters.Add(new OdbcParameter(EventInfoMDC_DAO.EventNo, Row.EventNo));
                    ///Parameters Before Changes
                    cmd.Parameters.Add(new OdbcParameter(EventInfoMDC_DAO.EventCodeParam + "_", Convert.ToInt32(Row["EventCode", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OdbcParameter(EventInfoMDC_DAO.EventGroupId + "_", Convert.ToInt32(Row["EventGroupId", DataRowVersion.Original])));
                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
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
                    IDbCommand cmd = GetCommand(EventInfoMDC_DAO.Delete);
                    cmd.Parameters.Add(new OdbcParameter(EventInfoMDC_DAO.EventCodeParam + "_", Convert.ToInt32(Row["EventCode", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OdbcParameter(EventInfoMDC_DAO.EventGroupId + "_", Convert.ToInt32(Row["EventGroupId", DataRowVersion.Original])));
                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
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

    public class EventLogsMDC_DAO : MDC_DAO
    {
        #region Data Members

        public static readonly string Select = "Select id, EventLogIndex, EventCounterIndex, EventGroupId from event_logs order by id,EventLogIndex,EventGroupId";

        public static readonly string Insert = "INSERT INTO event_logs (id, EventLogIndex, EventCounterIndex, EventGroupId) " +
                                                "VALUES (?,?,?,?)";

        public static readonly string Update = "UPDATE event_logs SET id = ?, EventLogIndex = ?, EventCounterIndex = ?, EventGroupId = ? " +
                                                "WHERE (id = ? AND EventGroupId = ?)";

        public static readonly string Delete = "DELETE FROM event_logs " +
                                                "WHERE (id = ? AND EventGroupId = ?)";

        //  Named Parameters
        public static readonly string IdParam = "@id";
        public static readonly string EventLogOBISIndexParam = "@eventLogIndex";
        public static readonly string EventCounterOBISIndexParam = "@eventCounterIndex";
        public static readonly string EventGroupId = "@EventGroupId";

        #endregion

        public EventLogsMDC_DAO(string connectionString)
            : base(connectionString) { }
        public EventLogsMDC_DAO(IDbConnection connection)
            : base(connection) { }

        public void Load_EventLogsInfo(Configs DataSet)
        {
            try
            {
                LoadDataTable(DataSet.EventLogs, GetCommand(Select));
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
                UpdateDataTable(DataSet.EventLogs, GetCommand(Select));
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
                    // Exe Insert Query
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
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    IDbCommand cmd = GetCommand(EventLogsMDC_DAO.Insert);
                    cmd.Parameters.Add(new OdbcParameter(EventLogsMDC_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OdbcParameter(EventLogsMDC_DAO.EventLogOBISIndexParam, Row.EventLogIndex));
                    cmd.Parameters.Add(new OdbcParameter(EventLogsMDC_DAO.EventCounterOBISIndexParam, Row.EventCounterIndex));
                    cmd.Parameters.Add(new OdbcParameter(EventLogsMDC_DAO.EventGroupId, Row.EventGroupId));
                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
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
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    IDbCommand cmd = GetCommand(EventLogsMDC_DAO.Update);
                    ///Parameters Reflecting Changes
                    cmd.Parameters.Add(new OdbcParameter(EventLogsMDC_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OdbcParameter(EventLogsMDC_DAO.EventLogOBISIndexParam, Row.EventLogIndex));
                    cmd.Parameters.Add(new OdbcParameter(EventLogsMDC_DAO.EventCounterOBISIndexParam, Row.EventCounterIndex));
                    cmd.Parameters.Add(new OdbcParameter(EventLogsMDC_DAO.EventGroupId, Row.EventGroupId));
                    ///Parameters Before Changes
                    cmd.Parameters.Add(new OdbcParameter(EventLogsMDC_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OdbcParameter(EventLogsMDC_DAO.EventGroupId + "_", Convert.ToInt32(Row["EventGroupId", DataRowVersion.Original])));

                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
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
                    IDbCommand cmd = GetCommand(EventLogsMDC_DAO.Delete);
                    cmd.Parameters.Add(new OdbcParameter(EventLogsMDC_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OdbcParameter(EventLogsMDC_DAO.EventGroupId + "_", Convert.ToInt32(Row["EventGroupId", DataRowVersion.Original])));
                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
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

    public class EventsGroupMDC_DAO : MDC_DAO
    {
        #region Data Members

        public static readonly string Select = "Select id, Events_group_Name from events_group order by id";
        public static readonly string Insert = "INSERT INTO events_group (id, Events_group_Name) VALUES (?,?)";
        public static readonly string Update = "UPDATE events_group SET Events_group_Name = ? "
            + "WHERE id = ?";
        public static readonly string Delete = "DELETE FROM events_group "
            + "WHERE id = ?";

        public static readonly string IdParam = "@id";
        public static readonly string EVGroupName = "@Events_group_Name";

        #endregion

        public EventsGroupMDC_DAO(string connectionString)
            : base(connectionString) { }
        public EventsGroupMDC_DAO(IDbConnection connection)
            : base(connection) { }

        public void LoadEventsGroup(Configs DataSet)
        {
            try
            {
                LoadDataTable(DataSet.Events_Group, GetCommand(Select));
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
                IDbCommand Select_Sql_Cmd = GetCommand(Select);
                this.Update(DataSet.Events_Group, Select_Sql_Cmd);
                // DataSet.MeterTypeInfo.Clear();
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
                    // Exe Insert Query
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
                    IDbCommand cmd = GetCommand(EventsGroupMDC_DAO.Insert);
                    cmd.Parameters.Add(new OdbcParameter(EventsGroupMDC_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OdbcParameter(EventsGroupMDC_DAO.EVGroupName, Row.Events_group_Name));
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
                    IDbCommand cmd = GetCommand(EventsGroupMDC_DAO.Update);
                    /// Reflected Changes Parameters
                    cmd.Parameters.Add(new OdbcParameter(EventsGroupMDC_DAO.EVGroupName, Row.Events_group_Name));
                    /// Before Changes Parameters
                    cmd.Parameters.Add(new OdbcParameter(EventsGroupMDC_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

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
                    IDbCommand cmd = GetCommand(EventsGroupMDC_DAO.Delete);
                    cmd.Parameters.Add(new OdbcParameter(EventsGroupMDC_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
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
}
