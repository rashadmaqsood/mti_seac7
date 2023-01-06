using DatabaseConfiguration.DataSet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Data.Linq;
using DLMS.Comm;
using DLMS;

namespace DatabaseConfiguration.DataBase
{

    public class SMT_DBAccessLayer : SMT_DAO
    {
        public SMT_DBAccessLayer(string connectionString):base(connectionString)
        {
        }

        #region Load All Data

        public void Load_All_Configurations(ref Configs AllDataSet)
        {
            try
            {
                // For Data Loading Set Configs DataSET 
                if (AllDataSet == null)
                    AllDataSet = new Configs();
                AllDataSet.Clear();

                UsersDAO UserDAO = new UsersDAO(this.DataBaseConnection);
                UserDAO.LoadUser(AllDataSet);

                // All Configuration Groups
                OBISRightsGroupSMT_DAO OBIS_RightsGroupDAO = new OBISRightsGroupSMT_DAO(this.DataBaseConnection);
                OBIS_RightsGroupDAO.LoadOBISRightsGroup(AllDataSet);

                EventsGroupSMT_DAO egroupDAO = new EventsGroupSMT_DAO(this.DataBaseConnection);
                egroupDAO.LoadEventsGroup(AllDataSet);

                BillingItemGroupSMT_DAO bgroupDAO = new BillingItemGroupSMT_DAO(this.DataBaseConnection);
                bgroupDAO.LoadBillingItemGroup(AllDataSet);

                DisplayWindowsGroupDAO dgroupDAO = new DisplayWindowsGroupDAO(this.DataBaseConnection);
                dgroupDAO.LoadDisplayWindowsGroup(AllDataSet);

                LoadProfileGroupSMT_DAO lgroupDAO = new LoadProfileGroupSMT_DAO(this.DataBaseConnection);
                lgroupDAO.LoadLoadProfileGroup(AllDataSet);

                StatusWordDAO statusWordDAO = new StatusWordDAO(this.DataBaseConnection);
                statusWordDAO.LoadStatusWord(AllDataSet);


                AllQuantitiesDAO allQuantities = new AllQuantitiesDAO(this.DataBaseConnection);
                allQuantities.Load_ObisLabels(AllDataSet);

                ManufacturerSMT_DAO manufacturerDAO = new ManufacturerSMT_DAO(this.DataBaseConnection);
                manufacturerDAO.LoadManufacturer(AllDataSet);

                AuthenticationTypeSMT_DAO authenticationTypeDAO = new AuthenticationTypeSMT_DAO(this.DataBaseConnection);
                authenticationTypeDAO.LoadAuthenticationType(AllDataSet);

                DeviceSMT_DAO deviceDAO = new DeviceSMT_DAO(this.DataBaseConnection);
                deviceDAO.LoadDevice(AllDataSet);

                DeviceAssociationSMT_DAO deviceAssociationDAO = new DeviceAssociationSMT_DAO(this.DataBaseConnection);
                deviceAssociationDAO.LoadDeviceAssociation(AllDataSet);

                OBISDetailsSMT_DAO ObisDetails = new OBISDetailsSMT_DAO(this.DataBaseConnection);
                ObisDetails.Load_ObisDetails(AllDataSet);

                // DataRelation []childRel = new DataRelation[ AllDataSet.OBIS_Rights.ChildRelations.Count];
                // AllDataSet.OBIS_Rights.ChildRelations.CopyTo(childRel, 0);
                // AllDataSet.OBIS_Rights.ChildRelations.Clear();

                // Dt = (Configs)AllDataSet.OBIS_Rights.DataSet.Copy();
                OBISRithtsSMT_DAO OBIS_RightsDAO = new OBISRithtsSMT_DAO(this.DataBaseConnection);
                OBIS_RightsDAO.Load_OBIS_Rights(AllDataSet);

                // Dt = (Configs)AllDataSet.Rights.DataSet.Copy();
                RithtsSMT_DAO RightsDAO = new RithtsSMT_DAO(this.DataBaseConnection);
                RightsDAO.Load_Rights(AllDataSet);

                ConfigurationSMT_DAO ConfigDAO = new ConfigurationSMT_DAO(this.DataBaseConnection);
                ConfigDAO.LoadMeterConfiguraion(AllDataSet);


                DisplayWindowDAO DispWinDAO = new DisplayWindowDAO(this.DataBaseConnection);
                DispWinDAO.Load_Display_Windows(AllDataSet);

                LoadProfileSMT_DAO LoadProfileDAO = new LoadProfileSMT_DAO(this.DataBaseConnection);
                LoadProfileDAO.Load_Profiles(AllDataSet);

                BillItemSMT_DAO BillDAO = new BillItemSMT_DAO(this.DataBaseConnection);
                BillDAO.Load_Billing_Items(AllDataSet);

                BillTariffSMT_DAO BillTariffDAO = new BillTariffSMT_DAO(this.DataBaseConnection);
                BillTariffDAO.Load_Billing_Tariff(AllDataSet);

                EventInfoSMT_DAO EventInfoDAO = new EventInfoSMT_DAO(this.DataBaseConnection);
                EventInfoDAO.Load_EventProfileInfo(AllDataSet);

                EventLogsSMT_DAO EventLogDAO = new EventLogsSMT_DAO(this.DataBaseConnection);
                EventLogDAO.Load_EventLogsInfo(AllDataSet);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading Meter Configurations", ex);
            }
            finally
            {
                AllDataSet.AcceptChanges();
                //AllDataSet.RejectChanges();
            }
        }

        #endregion

        #region Update Data

        public void Update_All_Configuration(Configs AllDataSet)
        {
            try
            {
                
                using (IDbTransaction transection = this.DataBaseConnection.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    Configs Dt = new Configs();
                    AllDataSet.EnforceConstraints = false;
                    Dt.EnforceConstraints = false;
                    Dt = (Configs)AllDataSet.GetChanges();
                    // (Configs)AllDataSet.Meter_Info.DataSet.GetChanges();

                    UsersDAO UserDAO = new UsersDAO(this.DataBaseConnection);
                    UserDAO.Transaction = transection;
                    UserDAO.AcceptChangesUser(Dt);


                    EventsGroupSMT_DAO egroupDAO = new EventsGroupSMT_DAO(this.DataBaseConnection);
                    egroupDAO.Transaction = transection;
                    egroupDAO.AcceptChangesEventsGroup(Dt);

                    BillingItemGroupSMT_DAO bgroupDAO = new BillingItemGroupSMT_DAO(this.DataBaseConnection);
                    bgroupDAO.Transaction = transection;
                    bgroupDAO.AcceptChangesBillingItemGroup(Dt);

                    DisplayWindowsGroupDAO dgroupDAO = new DisplayWindowsGroupDAO(this.DataBaseConnection);
                    dgroupDAO.Transaction = transection;
                    dgroupDAO.AcceptChangesDisplayWindowsGroup(Dt);

                    LoadProfileGroupSMT_DAO lgroupDAO = new LoadProfileGroupSMT_DAO(this.DataBaseConnection);
                    lgroupDAO.Transaction = transection;
                    lgroupDAO.AcceptChangesLoadProfileGroup(Dt);

                    OBISRightsGroupSMT_DAO oBISRightsGroupDAO = new OBISRightsGroupSMT_DAO(this.DataBaseConnection);
                    oBISRightsGroupDAO.Transaction = transection;
                    oBISRightsGroupDAO.AcceptChangesOBISRightsGroup(Dt);


                    // UpdateMeterTypeInfo(Dt);
                    // Dt = (Configs)AllDataSet.Configuration.DataSet.GetChanges();
                    ConfigurationSMT_DAO ConfigDAO = new ConfigurationSMT_DAO(this.DataBaseConnection);
                    ConfigDAO.Transaction = transection;
                    ConfigDAO.AcceptChangesConfiguration(Dt);

                    StatusWordDAO statusWordDAO = new StatusWordDAO(this.DataBaseConnection);
                    statusWordDAO.Transaction = transection;
                    statusWordDAO.AcceptChangesStatusWord(Dt);

                    AllQuantitiesDAO AllQuantities = new AllQuantitiesDAO(this.DataBaseConnection);
                    AllQuantities.Transaction = transection;
                    AllQuantities.AcceptChangesAllQuantities(Dt);


                    ManufacturerSMT_DAO manufacturerDAO = new ManufacturerSMT_DAO(this.DataBaseConnection);
                    manufacturerDAO.Transaction = transection;
                    manufacturerDAO.AcceptChangesManufacturer(Dt);

                    AuthenticationTypeSMT_DAO authenticationTypeDAO = new AuthenticationTypeSMT_DAO(this.DataBaseConnection);
                    authenticationTypeDAO.Transaction = transection;
                    authenticationTypeDAO.AcceptChangesAuthenticationType(Dt);

                    DeviceSMT_DAO deviceDAO = new DeviceSMT_DAO(this.DataBaseConnection);
                    deviceDAO.Transaction = transection;
                    deviceDAO.AcceptChangesDevice(Dt);

                    DeviceAssociationSMT_DAO deviceAssociationDAO = new DeviceAssociationSMT_DAO(this.DataBaseConnection);
                    deviceAssociationDAO.Transaction = transection;
                    deviceAssociationDAO.AcceptChangesDeviceAssociation(Dt);

                    OBISDetailsSMT_DAO ObisDetails = new OBISDetailsSMT_DAO(this.DataBaseConnection);
                    ObisDetails.Transaction = transection;
                    ObisDetails.AcceptChangesObisDetails(Dt);


                    OBISRithtsSMT_DAO OBIS_RightsDAO = new OBISRithtsSMT_DAO(this.DataBaseConnection);
                    OBIS_RightsDAO.Transaction = transection;
                    OBIS_RightsDAO.AcceptChangesOBIS_Rights(AllDataSet);

                    RithtsSMT_DAO RightsDAO = new RithtsSMT_DAO(this.DataBaseConnection);
                    RightsDAO.Transaction = transection;
                    RightsDAO.AcceptChanges_Rights(Dt);

                    DisplayWindowDAO DispWinDAO = new DisplayWindowDAO(this.DataBaseConnection);
                    DispWinDAO.Transaction = transection;
                    DispWinDAO.AcceptChanges_Display_Windows(Dt);

                    LoadProfileSMT_DAO LoadProfileDAO = new LoadProfileSMT_DAO(this.DataBaseConnection);
                    LoadProfileDAO.Transaction = transection;
                    LoadProfileDAO.AcceptChanges_Load_Profiles(Dt);

                    BillItemSMT_DAO BillDAO = new BillItemSMT_DAO(this.DataBaseConnection);
                    BillDAO.Transaction = transection;
                    BillDAO.AcceptChanges_BillingItems(Dt);

                    BillTariffSMT_DAO BillTariffDAO = new BillTariffSMT_DAO(this.DataBaseConnection);
                    BillTariffDAO.Transaction = transection;
                    BillTariffDAO.AcceptChanges_BillingTariff(Dt);

                    EventInfoSMT_DAO EventInfoDAO = new EventInfoSMT_DAO(this.DataBaseConnection);
                    EventInfoDAO.Transaction = transection;
                    EventInfoDAO.AcceptChanges_EventProfileInfo(Dt);

                    EventLogsSMT_DAO EventLogDAO = new EventLogsSMT_DAO(this.DataBaseConnection);
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

        #endregion
    }

    #region SMT_DAO_Base

    public class SMT_DAO : IDisposable
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
            get;
            //{
            //    try
            //    {
            //        string Connection = null;
            //        Connection = Settings.Default.SMT_DBConnectionString;
            //        return Connection;
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception("Error getting database connection string", ex);
            //    }
            //}
            set;
            //{
            //    if (!String.IsNullOrEmpty(value))
            //    {
            //        Settings.Default["SMT_DBConnectionString"] = value;
            //        Settings.Default.Save();
            //    }
            //}
        }

        public SMT_DAO(string connectionString)
        {
            try
            {
                ConnectionString = connectionString;
                OleDbConnection connection = new OleDbConnection(ConnectionString);
                connection.Open();
                DbConnection = connection;
                // DbConnection = new SqlConnection(ConnectionString);
                // DbConnection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("Error building ConfigurationDB connection", ex);
            }
        }
         public SMT_DAO(IDbConnection Conn)
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
        public void Update(DataTable AllDataSet, OleDbCommand SelectSQLCmd)
        {
            try
            {
                using (DataContext dtContext = new DataContext(SelectSQLCmd.Connection))
                {

                    OleDbDataAdapter MeterConfigurationDataAdapter =
                        new OleDbDataAdapter(SelectSQLCmd);
                    OleDbCommandBuilder CommandBuilder = new OleDbCommandBuilder(MeterConfigurationDataAdapter);
                    // DataSet.MeterTypeInfo.Clear();
                    MeterConfigurationDataAdapter.UpdateCommand = CommandBuilder.GetUpdateCommand(true);
                    MeterConfigurationDataAdapter.DeleteCommand = CommandBuilder.GetDeleteCommand(true);
                    MeterConfigurationDataAdapter.InsertCommand = CommandBuilder.GetInsertCommand(true);
                    MeterConfigurationDataAdapter.Update(AllDataSet);
                    dtContext.SubmitChanges(ConflictMode.ContinueOnConflict);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating data", ex);
            }
        }

        public void Load(DataTable DataSet, OleDbCommand SelectSQLCmd)
        {
            try
            {
                OleDbDataAdapter DataAdapeter =
                    new OleDbDataAdapter(SelectSQLCmd);
                // DataSet.MeterTypeInfo.Clear();
                DataAdapeter.Fill(DataSet);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading data from data source", ex);
            }
        }

        public OleDbCommand GetCommand(string SqlQuery)
        {
            try
            {
                OleDbCommand Command = new OleDbCommand(SqlQuery, (OleDbConnection)DataBaseConnection);
                if (Transaction != null)
                    Command.Transaction = (OleDbTransaction)Transaction;
                return Command;
            }
            catch (Exception ex)
            {
                throw ex;
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

    public class ManufacturerSMT_DAO : SMT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from [Manufacturer] order by [id] desc";
        public static readonly string Insert = "INSERT INTO [Manufacturer] ([id], [Manufacturer_Name], [Code]) VALUES (@id, @Manufacturer_Name,@Code)";
        public static readonly string Update = "UPDATE [Manufacturer] SET [Manufacturer_Name] = @Manufacturer_Name , [Code] = @Code "
            + "WHERE [id] = @id_";
        public static readonly string Delete = "DELETE FROM [Manufacturer] "
            + "WHERE [id] = @id_";

        public static readonly string IdParam = "@id";
        public static readonly string Manufacturer_Name = "@Manufacturer_Name";
        public static readonly string Code = "@Code";

        #endregion

        public ManufacturerSMT_DAO(string connectionString)
            : base(connectionString)
        { }
        public ManufacturerSMT_DAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadManufacturer(Configs DataSet)
        {
            try
            {
                OleDbDataAdapter ConfigsDataAdapeter =
                    new OleDbDataAdapter(new OleDbCommand(Select, (OleDbConnection)DataBaseConnection));
                ConfigsDataAdapeter.Fill(DataSet.Manufacturer);
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
                OleDbCommand Select_Sql_Cmd = new OleDbCommand(Select, (OleDbConnection)DataBaseConnection);
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
                    OleDbCommand cmd = GetCommand(ManufacturerSMT_DAO.Insert);
                    cmd.Parameters.Add(new OleDbParameter(ManufacturerSMT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OleDbParameter(ManufacturerSMT_DAO.Manufacturer_Name, Row.Manufacturer_Name));
                    cmd.Parameters.Add(new OleDbParameter(ManufacturerSMT_DAO.Code, Row.Code));
                    //cmd.Parameters.Add(new OleDbParameter(MeterTypeInfoDAO.FirmWareVersionParam, Row.FirmWareVersion));
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
                    OleDbCommand cmd = GetCommand(ManufacturerSMT_DAO.Update);
                    ///Reflected Changes Parameters
                    cmd.Parameters.Add(new OleDbParameter(ManufacturerSMT_DAO.Manufacturer_Name, Row.Manufacturer_Name));
                    cmd.Parameters.Add(new OleDbParameter(ManufacturerSMT_DAO.Code, Row.Code));

                    /// Parameters Before Modification
                    cmd.Parameters.Add(new OleDbParameter(ManufacturerSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

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
                    OleDbCommand cmd = GetCommand(ManufacturerSMT_DAO.Delete);
                    cmd.Parameters.Add(new OleDbParameter(ManufacturerSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
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

    public class DeviceSMT_DAO : SMT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from [Device] order by [Device_Name] asc";
        public static readonly string Insert = "INSERT INTO [Device] ([id], [Device_Name], [Model], [Manufacturer_Id], [IsSinglePhase]) VALUES (@id,@Device_Name,@Model, @Manufacturer_Id,@IsSinglePhase)";
        public static readonly string Update = "UPDATE [Device] SET [Device_Name] = @Device_Name , [Model] = @Model, [Manufacturer_Id] = @Manufacturer_Id, [IsSinglePhase] = @IsSinglePhase "
            + "WHERE [id] = @id_";
        public static readonly string Delete = "DELETE FROM [Device] "
            + "WHERE [id] = @id_";

        public static readonly string IdParam = "@id";
        public static readonly string Device_Name = "@Device_Name";
        public static readonly string Model = "@Model";
        public static readonly string Manufacturer_Id = "@Manufacturer_Id";
        public static readonly string IsSinglePhase = "@IsSinglePhase";

        #endregion

        public DeviceSMT_DAO(string connectionString)
            : base(connectionString)
        { }
        public DeviceSMT_DAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadDevice(Configs DataSet)
        {
            try
            {
                OleDbDataAdapter ConfigsDataAdapeter =
                    new OleDbDataAdapter(new OleDbCommand(Select, (OleDbConnection)DataBaseConnection));
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
                OleDbCommand Select_Sql_Cmd = new OleDbCommand(Select, (OleDbConnection)DataBaseConnection);
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
                    OleDbCommand cmd = GetCommand(DeviceSMT_DAO.Insert);
                    cmd.Parameters.Add(new OleDbParameter(DeviceSMT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OleDbParameter(DeviceSMT_DAO.Device_Name, Row.Device_Name));
                    cmd.Parameters.Add(new OleDbParameter(DeviceSMT_DAO.Model, Row.Model));
                    cmd.Parameters.Add(new OleDbParameter(DeviceSMT_DAO.Manufacturer_Id, Row.Manufacturer_Id));
                    cmd.Parameters.Add(new OleDbParameter(DeviceSMT_DAO.IsSinglePhase, Row.IsSinglePhase));
                    //cmd.Parameters.Add(new OleDbParameter(MeterTypeInfoDAO.FirmWareVersionParam, Row.FirmWareVersion));
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
                    OleDbCommand cmd = GetCommand(DeviceSMT_DAO.Update);
                    ///Reflected Changes Parameters
                    cmd.Parameters.Add(new OleDbParameter(DeviceSMT_DAO.Device_Name, Row.Device_Name));
                    cmd.Parameters.Add(new OleDbParameter(DeviceSMT_DAO.Model, Row.Model));
                    cmd.Parameters.Add(new OleDbParameter(DeviceSMT_DAO.Manufacturer_Id, Row.Manufacturer_Id));
                    cmd.Parameters.Add(new OleDbParameter(DeviceSMT_DAO.IsSinglePhase, Row.IsSinglePhase));
                    /// Parameters Before Modification
                    cmd.Parameters.Add(new OleDbParameter(DeviceSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

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
                    OleDbCommand cmd = GetCommand(DeviceSMT_DAO.Delete);
                    /// Parameters Before Deletion
                    cmd.Parameters.Add(new OleDbParameter(DeviceSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
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

    public class AuthenticationTypeSMT_DAO : SMT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from [Authentication_Type] order by [Authentication_Type_Name] asc";
        public static readonly string Insert = "INSERT INTO [Authentication_Type] ([id], [Authentication_Type_Name]) VALUES (@id, @Authentication_Type_Name)";
        public static readonly string Update = "UPDATE [Authentication_Type] SET [Authentication_Type_Name] = @Authentication_Type_Name "
            + "WHERE [id] = @id_";
        public static readonly string Delete = "DELETE FROM [Authentication_Type] "
            + "WHERE [id] = @id_";

        public static readonly string IdParam = "@id";
        public static readonly string Authentication_Type_Name = "@Authentication_Type_Name";

        #endregion

        public AuthenticationTypeSMT_DAO(string connectionString)
            : base(connectionString)
        { }
        public AuthenticationTypeSMT_DAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadAuthenticationType(Configs DataSet)
        {
            try
            {
                OleDbDataAdapter ConfigsDataAdapeter =
                    new OleDbDataAdapter(new OleDbCommand(Select, (OleDbConnection)DataBaseConnection));
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
                OleDbCommand Select_Sql_Cmd = new OleDbCommand(Select, (OleDbConnection)DataBaseConnection);
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
                    OleDbCommand cmd = GetCommand(AuthenticationTypeSMT_DAO.Insert);
                    cmd.Parameters.Add(new OleDbParameter(AuthenticationTypeSMT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OleDbParameter(AuthenticationTypeSMT_DAO.Authentication_Type_Name, Row.Authentication_Type_Name));
                    //cmd.Parameters.Add(new OleDbParameter(MeterTypeInfoDAO.FirmWareVersionParam, Row.FirmWareVersion));
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
                    OleDbCommand cmd = GetCommand(AuthenticationTypeSMT_DAO.Update);
                    ///Reflected Changes Parameters
                    cmd.Parameters.Add(new OleDbParameter(AuthenticationTypeSMT_DAO.Authentication_Type_Name, Row.Authentication_Type_Name));
                    /// Parameters Before Updation
                    cmd.Parameters.Add(new OleDbParameter(AuthenticationTypeSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
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
                    OleDbCommand cmd = GetCommand(AuthenticationTypeSMT_DAO.Delete);
                    /// Parameters Before Deletion
                    cmd.Parameters.Add(new OleDbParameter(AuthenticationTypeSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
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

    public class DeviceAssociationSMT_DAO : SMT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from [Device_Association] order by [Association_Name] asc";
        public static readonly string Insert = "INSERT INTO [Device_Association] ([id],[Association_Name], [Authentication_Type_Id], [Client_Sap], [Meter_Sap],[Device_Id],[Configuration_Id],[ObisRightGroupId],[Reload_Config],[Association_Index]) VALUES (@id,@Association_Name,@Authentication_Type_Id,@Client_Sap, @Meter_Sap,@Device_Id,@Configuration_Id,@ObisRightGroupId,@Reload_Config,@Association_Index)";
        public static readonly string Update = "UPDATE [Device_Association] SET [Association_Name]=@Association_Name, [Authentication_Type_Id] = @Authentication_Type_Id , [Client_Sap] = @Client_Sap, [Meter_Sap] = @Meter_Sap, [Device_Id] = @Device_Id, [Configuration_Id] = @Configuration_Id, [ObisRightGroupId] = @ObisRightGroupId,[Reload_Config] = @Reload_Config, [Association_Index] = @Association_Index "
                                                + "WHERE [id] = @id_";

        public static readonly string Delete = "DELETE FROM [Device_Association] "
                                                + "WHERE [id] = @id_";

        public static readonly string IdParam = "@id";
        public static readonly string Association_Name = "@Association_Name";
        public static readonly string Authentication_Type_Id = "@Authentication_Type_Id";
        public static readonly string Client_Sap = "@Client_Sap";
        public static readonly string Meter_Sap = "@Meter_Sap";
        public static readonly string Device_Id = "@Device_Id";
        public static readonly string Configuration_Id = "@Configuration_Id";
        public static readonly string ObisRightGroupId = "@ObisRightGroupId";
        public static readonly string Reload_Config = "@Reload_Config";
        public static readonly string Association_Index = "@Association_Index";

        #endregion

        public DeviceAssociationSMT_DAO(string connectionString)
            : base(connectionString)
        { }
        public DeviceAssociationSMT_DAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadDeviceAssociation(Configs DataSet)
        {
            try
            {
                OleDbDataAdapter ConfigsDataAdapeter =
                    new OleDbDataAdapter(new OleDbCommand(Select, (OleDbConnection)DataBaseConnection));
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
                OleDbCommand Select_Sql_Cmd = new OleDbCommand(Select, (OleDbConnection)DataBaseConnection);
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
                    OleDbCommand cmd = GetCommand(DeviceAssociationSMT_DAO.Insert);
                    cmd.Parameters.Add(new OleDbParameter(DeviceAssociationSMT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OleDbParameter(DeviceAssociationSMT_DAO.Association_Name, Row.Association_Name));
                    cmd.Parameters.Add(new OleDbParameter(DeviceAssociationSMT_DAO.Authentication_Type_Id, Row.Authentication_Type_Id));
                    cmd.Parameters.Add(new OleDbParameter(DeviceAssociationSMT_DAO.Client_Sap, Row.Client_Sap));
                    cmd.Parameters.Add(new OleDbParameter(DeviceAssociationSMT_DAO.Meter_Sap, Row.Meter_Sap));
                    cmd.Parameters.Add(new OleDbParameter(DeviceAssociationSMT_DAO.Device_Id, Row.Device_Id));
                    cmd.Parameters.Add(new OleDbParameter(DeviceAssociationSMT_DAO.Configuration_Id, Row.Configuration_Id));
                    cmd.Parameters.Add(new OleDbParameter(DeviceAssociationSMT_DAO.ObisRightGroupId, Row.ObisRightGroupId));
                    cmd.Parameters.Add(new OleDbParameter(DeviceAssociationSMT_DAO.Reload_Config, Row.Reload_Config));
                    cmd.Parameters.Add(new OleDbParameter(DeviceAssociationSMT_DAO.Association_Index, Row.Association_Index));
                    //cmd.Parameters.Add(new OleDbParameter(MeterTypeInfoDAO.FirmWareVersionParam, Row.FirmWareVersion));
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
                    OleDbCommand cmd = GetCommand(DeviceAssociationSMT_DAO.Update);
                    ///Reflected Changes Parameters
                    cmd.Parameters.Add(new OleDbParameter(DeviceAssociationSMT_DAO.Association_Name, Row.Association_Name));
                    cmd.Parameters.Add(new OleDbParameter(DeviceAssociationSMT_DAO.Authentication_Type_Id, Row.Authentication_Type_Id));
                    cmd.Parameters.Add(new OleDbParameter(DeviceAssociationSMT_DAO.Client_Sap, Row.Client_Sap));
                    cmd.Parameters.Add(new OleDbParameter(DeviceAssociationSMT_DAO.Meter_Sap, Row.Meter_Sap));
                    cmd.Parameters.Add(new OleDbParameter(DeviceAssociationSMT_DAO.Device_Id, Row.Device_Id));
                    cmd.Parameters.Add(new OleDbParameter(DeviceAssociationSMT_DAO.Configuration_Id, Row.Configuration_Id));
                    cmd.Parameters.Add(new OleDbParameter(DeviceAssociationSMT_DAO.ObisRightGroupId, Row.ObisRightGroupId));
                    cmd.Parameters.Add(new OleDbParameter(DeviceAssociationSMT_DAO.Reload_Config, Row.Reload_Config));
                    cmd.Parameters.Add(new OleDbParameter(DeviceAssociationSMT_DAO.Association_Index, Row.Association_Index));

                    /// Parameters Before Updation
                    cmd.Parameters.Add(new OleDbParameter(DeviceAssociationSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
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
                    OleDbCommand cmd = GetCommand(DeviceAssociationSMT_DAO.Delete);
                    /// Parameters Before Deletion
                    cmd.Parameters.Add(new OleDbParameter(DeviceAssociationSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
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

    #region UsersDAO

    public class UsersDAO : SMT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from [users] order by [user_name] desc";
        public static readonly string Insert = "INSERT INTO [users] ([user_name],[user_password],[user_type]) VALUES (@user_name, @user_password,@user_type)";
        public static readonly string Update = "UPDATE [users] SET [user_name] = @user_name,[user_password] = @user_password,[user_type]=@user_type"
            + "WHERE [user_name] = @user_name_";
        public static readonly string Delete = "DELETE FROM [users]"
            + "WHERE [user_name] = @user_name_";


        public static readonly string UserName = "@user_name";
        public static readonly string UserPassword = "@user_password";
        public static readonly string UserType = "@user_type";

        #endregion

        public UsersDAO(string connectionString)
            : base(connectionString)
        { }
        public UsersDAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadUser(Configs DataSet)
        {
            try
            {
                OleDbDataAdapter ConfigsDataAdapeter =
                    new OleDbDataAdapter(new OleDbCommand(Select, (OleDbConnection)DataBaseConnection));
                //DataSet.MeterTypeInfo.Clear();
                ConfigsDataAdapeter.Fill(DataSet.users);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading Users", ex);
            }
        }

        public void UpdateUser(Configs DataSet)
        {
            try
            {
                OleDbCommand Select_Sql_Cmd = new OleDbCommand(Select, (OleDbConnection)DataBaseConnection);
                this.Update(DataSet.users, Select_Sql_Cmd);
                //DataSet.MeterTypeInfo.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating User", ex);
            }
        }

        public void AcceptChangesUser(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.usersRow> MeterUserRows = DataSet.users.ToList<Configs.usersRow>();
                for (int index = 0; (MeterUserRows != null && index < MeterUserRows.Count); index++)
                {
                    Configs.usersRow dtRow = MeterUserRows[index];
                    #region Insert_Code
                    ///Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertUser(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateUser(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteMeterUser(dtRow);
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

        public void InsertUser(Configs.usersRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    OleDbCommand cmd = GetCommand(UsersDAO.Insert);
                    cmd.Parameters.Add(new OleDbParameter(UsersDAO.UserName, Row.user_name));
                    cmd.Parameters.Add(new OleDbParameter(UsersDAO.UserPassword, Row.user_password));
                    cmd.Parameters.Add(new OleDbParameter(UsersDAO.UserType, Row.user_type));
                    int res = cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving MeterUser data in data source", ex);
            }
        }

        public void UpdateUser(Configs.usersRow Row)
        {
            try
            {
                #region Update_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    OleDbCommand cmd = GetCommand(UsersDAO.Update);
                    ///Reflected Changes Parameters
                    cmd.Parameters.Add(new OleDbParameter(UsersDAO.UserName, Row.user_name));
                    cmd.Parameters.Add(new OleDbParameter(UsersDAO.UserPassword, Row.user_password));
                    cmd.Parameters.Add(new OleDbParameter(UsersDAO.UserType, Row.user_type));
                    ///Before Changes Parameters
                    cmd.Parameters.Add(new OleDbParameter(UsersDAO.UserName + "_", Convert.ToString(Row["user_name", DataRowVersion.Original])));
                    int res = cmd.ExecuteNonQuery();
                    //if (res > 0)
                    //    Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying MeterUser data in data source", ex);
            }
        }

        public void DeleteMeterUser(Configs.usersRow Row)
        {
            try
            {
                #region Delete_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Deleted)
                {
                    OleDbCommand cmd = GetCommand(UsersDAO.Delete);
                    cmd.Parameters.Add(new OleDbParameter(UsersDAO.UserName + "_", Row.user_name));
                    cmd.ExecuteNonQuery();
                    //Row.AcceptChanges();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting User data in data source", ex);
            }
        }
        #endregion
    }

    #endregion

    #region Configuration

    public class ConfigurationSMT_DAO : SMT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from [Configuration] order by [Name] desc";

        public static readonly string Insert = "INSERT INTO [Configuration] ([id], [Name], [lp_channels_group_id], [BillItemsGroupId], [DisplayWindowGroupId], [EventGroupId])" +
                                        "VALUES (@id, @name, @lp_channels_group_id, @BillItemsGroupId , @DisplayWindowGroupId, @EventGroupId)";

        public static readonly string Update = "UPDATE [Configuration] SET [Name] = @name, [lp_channels_group_id] = @lp_channels_group_id, " + " [BillItemsGroupId] = @BillItemsGroupId, [DisplayWindowGroupId] = @DisplayWindowGroupId, [EventGroupId] = @EventGroupId " +
                         "WHERE (([id] = @id_) AND (([Name] IS NULL) OR ([Name] = @name_)) AND (([lp_channels_group_id] IS NULL) OR " +
                          "([lp_channels_group_id] = @lp_channels_group_id_)) AND (([BillItemsGroupId] IS NULL) OR ([BillItemsGroupId] = @BillItemsGroupId_)) AND " +
                          "(([DisplayWindowGroupId] IS NULL) OR ([DisplayWindowGroupId] = @DisplayWindowGroupId_)) AND (([EventGroupId] IS NULL) OR ([EventGroupId] = @EventGroupId_)))";

        public static readonly string Delete = "DELETE FROM [Configuration]" +
                            "WHERE (([id] = @id_) AND (([Name] IS NULL) OR ([Name] = @name_)) AND (([lp_channels_group_id] IS NULL) OR " +
                             "([lp_channels_group_id] = @lp_channels_group_id_)) AND (([BillItemsGroupId] IS NULL) OR ([BillItemsGroupId] = @BillItemsGroupId_)) AND " +
                             "(([DisplayWindowGroupId] IS NULL) OR ([DisplayWindowGroupId] = @DisplayWindowGroupId_)) AND (([EventGroupId] IS NULL) OR ([EventGroupId] = @EventGroupId_)))";

        public static readonly string IdParam = "@id";
        public static readonly string NameParam = "@name";
        public static readonly string LpChannelsGroupId = "@lp_channels_group_id";
        public static readonly string BillItemsGroupId = "@BillItemsGroupId";
        public static readonly string DisplayWindowGroupId = "@DisplayWindowGroupId";
        public static readonly string EventGroupId = "@EventGroupId";

        #endregion

        public ConfigurationSMT_DAO(string connectionString)
            : base(connectionString)
        { }
        public ConfigurationSMT_DAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadMeterConfiguraion(Configs DataTable)
        {
            try
            {
                OleDbDataAdapter MeterConfigurationDataAdapter =
                    new OleDbDataAdapter(new OleDbCommand(Select, (OleDbConnection)DataBaseConnection));
                //DataSet.MeterTypeInfo.Clear();
                MeterConfigurationDataAdapter.Fill(DataTable.Configuration);
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
                OleDbCommand Select_Sql_Cmd = new OleDbCommand(Select, (OleDbConnection)DataBaseConnection);

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
                    /// Exec Insert Query
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
                    OleDbCommand cmd = GetCommand(ConfigurationSMT_DAO.Insert);
                    cmd.Parameters.Add(new OleDbParameter(ConfigurationSMT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OleDbParameter(ConfigurationSMT_DAO.NameParam, Row.Name));

                    cmd.Parameters.Add(new OleDbParameter(ConfigurationSMT_DAO.LpChannelsGroupId, OleDbType.BigInt) { Value = (Row.Islp_channels_group_idNull()) ? DBNull.Value : (object)Row.lp_channels_group_id });

                    cmd.Parameters.Add(new OleDbParameter(ConfigurationSMT_DAO.BillItemsGroupId, OleDbType.BigInt) { Value = (Row.IsBillItemsGroupIdNull()) ? DBNull.Value : (object)Row.BillItemsGroupId });

                    cmd.Parameters.Add(new OleDbParameter(ConfigurationSMT_DAO.DisplayWindowGroupId, OleDbType.BigInt) { Value = (Row.IsDisplayWindowGroupIdNull()) ? DBNull.Value : (object)Row.DisplayWindowGroupId });


                    cmd.Parameters.Add(new OleDbParameter(ConfigurationSMT_DAO.EventGroupId, OleDbType.BigInt) { Value = (Row.IsEventGroupIdNull()) ? DBNull.Value : (object)Row.EventGroupId });


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
                    OleDbCommand cmd = GetCommand(ConfigurationSMT_DAO.Update);
                    ///Parameters Reflecting Changes

                    cmd.Parameters.Add(new OleDbParameter(ConfigurationSMT_DAO.NameParam, Row.Name));

                    cmd.Parameters.Add(new OleDbParameter(ConfigurationSMT_DAO.LpChannelsGroupId, OleDbType.BigInt) { Value = (Row.Islp_channels_group_idNull()) ? DBNull.Value : (object)Row.lp_channels_group_id });

                    cmd.Parameters.Add(new OleDbParameter(ConfigurationSMT_DAO.BillItemsGroupId, OleDbType.BigInt) { Value = (Row.IsBillItemsGroupIdNull()) ? DBNull.Value : (object)Row.BillItemsGroupId });

                    cmd.Parameters.Add(new OleDbParameter(ConfigurationSMT_DAO.DisplayWindowGroupId, OleDbType.BigInt) { Value = (Row.IsDisplayWindowGroupIdNull()) ? DBNull.Value : (object)Row.DisplayWindowGroupId });


                    cmd.Parameters.Add(new OleDbParameter(ConfigurationSMT_DAO.EventGroupId, OleDbType.BigInt) { Value = (Row.IsEventGroupIdNull()) ? DBNull.Value : (object)Row.EventGroupId });


                    /// Parameters Before Modification
                    cmd.Parameters.Add(new OleDbParameter(ConfigurationSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(ConfigurationSMT_DAO.NameParam + "_", Convert.ToString(Row["Name", DataRowVersion.Original])));


                    cmd.Parameters.Add(new OleDbParameter(ConfigurationSMT_DAO.LpChannelsGroupId + "_", OleDbType.BigInt)
                    {
                        Value = ((Row["lp_channels_group_id", DataRowVersion.Original]) == DBNull.Value) ? DBNull.Value :
                               (object)Convert.ToInt32(Row["lp_channels_group_id", DataRowVersion.Original])
                    });


                    cmd.Parameters.Add(new OleDbParameter(ConfigurationSMT_DAO.BillItemsGroupId + "_", OleDbType.BigInt)
                    {
                        Value = ((Row["BillItemsGroupId", DataRowVersion.Original]) == DBNull.Value) ? DBNull.Value :
                                 (object)Convert.ToInt32(Row["BillItemsGroupId", DataRowVersion.Original])
                    });

                    cmd.Parameters.Add(new OleDbParameter(ConfigurationSMT_DAO.DisplayWindowGroupId + "_", OleDbType.BigInt)
                    {
                        Value = ((Row["DisplayWindowGroupId", DataRowVersion.Original]) == DBNull.Value) ? DBNull.Value :
                                 (object)Convert.ToInt32(Row["DisplayWindowGroupId", DataRowVersion.Original])
                    });


                    cmd.Parameters.Add(new OleDbParameter(ConfigurationSMT_DAO.EventGroupId + "_", OleDbType.BigInt)
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
                    OleDbCommand cmd = GetCommand(ConfigurationSMT_DAO.Delete);
                    /// Parameters Before Modification
                    cmd.Parameters.Add(new OleDbParameter(ConfigurationSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(ConfigurationSMT_DAO.NameParam + "_", Convert.ToString(Row["Name", DataRowVersion.Original])));

                    cmd.Parameters.Add(new OleDbParameter(ConfigurationSMT_DAO.LpChannelsGroupId + "_", OleDbType.BigInt)
                    {
                        Value = ((Row["lp_channels_group_id", DataRowVersion.Original]) == DBNull.Value) ? DBNull.Value :
                               (object)Convert.ToInt32(Row["lp_channels_group_id", DataRowVersion.Original])
                    });


                    cmd.Parameters.Add(new OleDbParameter(ConfigurationSMT_DAO.BillItemsGroupId + "_", OleDbType.BigInt)
                    {
                        Value = ((Row["BillItemsGroupId", DataRowVersion.Original]) == DBNull.Value) ? DBNull.Value :
                                 (object)Convert.ToInt32(Row["BillItemsGroupId", DataRowVersion.Original])
                    });

                    cmd.Parameters.Add(new OleDbParameter(ConfigurationSMT_DAO.DisplayWindowGroupId + "_", OleDbType.BigInt)
                    {
                        Value = ((Row["DisplayWindowGroupId", DataRowVersion.Original]) == DBNull.Value) ? DBNull.Value :
                                 (object)Convert.ToInt32(Row["DisplayWindowGroupId", DataRowVersion.Original])
                    });


                    cmd.Parameters.Add(new OleDbParameter(ConfigurationSMT_DAO.EventGroupId + "_", OleDbType.BigInt)
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

    #region All Quantities

    public class AllQuantitiesDAO : SMT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from [AllQuantities] order by [Label] asc";
        public static readonly string Insert = "INSERT INTO [AllQuantities] ([OBIS_Index], [Label]) " +
                                      "VALUES (@OBIS_Index, @Label)";

        public static readonly string Update = "UPDATE [AllQuantities] SET [OBIS_Index] = @OBIS_Index, [Label] = @Label " +
             "WHERE [OBIS_Index] = @OBIS_Index_";

        public static readonly string Delete = "DELETE FROM [AllQuantities] " +
                                                "WHERE [OBIS_Index] = @OBIS_Index_";


        public static readonly string OBIS_Index = "@OBIS_Index";
        public static readonly string Label = "@Label";

        #endregion

        public AllQuantitiesDAO(string connectionString)
            : base(connectionString)
        {

        }
        public AllQuantitiesDAO(IDbConnection connection)
            : base(connection)
        {

        }

        public void Load_ObisLabels(Configs DataSet)
        {
            try
            {
                //OleDbDataAdapter OBIS_Rights_DataAdapeter =
                //    new OleDbDataAdapter(new OleDbCommand(Select, (OleDbConnection)DataBaseConnection));

                OleDbDataReader OBIS_Rights_DataReader = new OleDbCommand(Select, (OleDbConnection)DataBaseConnection).ExecuteReader();

                DataSet.AllQuantities.Rows.Clear();

                while (OBIS_Rights_DataReader.Read())
                {
                    Configs.AllQuantitiesRow row =  DataSet.AllQuantities.NewAllQuantitiesRow();
                    row.OBIS_Index = Convert.ToDecimal(OBIS_Rights_DataReader.GetValue(0).ToString());
                    row.Label = OBIS_Rights_DataReader.GetString(1);
                    StOBISCode OBISCode = (Get_Index)row.OBIS_Index;
                    row.Quantity_Code = OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                    row.Quantity_Name = OBISCode.OBISIndex.ToString();
                    DataSet.AllQuantities.Rows.Add(row);
                }
                OBIS_Rights_DataReader.Close();
                //OBIS_Rights_DataAdapeter.Fill(DataSet.AllQuantities);
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

        public void InsertAllQuantities(Configs.AllQuantitiesRow Row)
        {
            try
            {
                #region Insert_Code

                // Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    OleDbCommand cmd = GetCommand(AllQuantitiesDAO.Insert);
                    cmd.Parameters.Add(new OleDbParameter(AllQuantitiesDAO.OBIS_Index, Row.OBIS_Index));
                    cmd.Parameters.Add(new OleDbParameter(AllQuantitiesDAO.Label, Row.Label));
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
                    OleDbCommand cmd = GetCommand(AllQuantitiesDAO.Update);
                    // Parameters Reflecting Modification
                    cmd.Parameters.Add(new OleDbParameter(AllQuantitiesDAO.OBIS_Index, Row.OBIS_Index));
                    cmd.Parameters.Add(new OleDbParameter(AllQuantitiesDAO.Label, Row.Label));
                    // Parameters Before Modification
                    cmd.Parameters.Add(new OleDbParameter(AllQuantitiesDAO.OBIS_Index + "_", Convert.ToUInt64(Row["OBIS_Index", DataRowVersion.Original])));
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
                    OleDbCommand cmd = GetCommand(AllQuantitiesDAO.Delete);
                    cmd.Parameters.Add(new OleDbParameter(AllQuantitiesDAO.OBIS_Index + "_", Convert.ToUInt64(Row["OBIS_Index", DataRowVersion.Original])));

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

    public class OBISDetailsSMT_DAO : SMT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from [OBIS_Details] order by [id]";

        public static readonly string Insert = "INSERT INTO [OBIS_Details] ([id], [Obis_Code], [Default_OBIS_Code], [Device_Id]) " +
                                      "VALUES (@id, @Obis_Code, @Default_OBIS_Code, @Device_Id)";

        public static readonly string Update = "UPDATE [OBIS_Details] SET [Obis_Code] = @Obis_Code, [Default_OBIS_Code] = @Default_OBIS_Code, [Device_Id] = @Device_Id " +
             "WHERE [id] = @id_";
        public static readonly string Delete = "DELETE FROM [OBIS_Details] " +
               "WHERE [id] = @id_";

        public static readonly string IdParam = "@id";
        public static readonly string Obis_Code = "@Obis_Code";
        public static readonly string Default_OBIS_Code = "@Default_OBIS_Code";
        public static readonly string Device_Id = "@Device_Id";
        #endregion

        public OBISDetailsSMT_DAO(string connectionString)
            : base(connectionString)
        {

        }
        public OBISDetailsSMT_DAO(IDbConnection connection)
            : base(connection)
        {

        }

        public void Load_ObisDetails(Configs DataSet)
        {
            try
            {
                OleDbDataReader OBIS_Details_DataReader = new OleDbCommand(Select, (OleDbConnection)DataBaseConnection).ExecuteReader();

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

                //DataSet.MeterTypeInfo.Clear();
                //OBIS_Rights_DataAdapeter.Fill(DataSet.OBIS_Details);
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
                OleDbCommand Sel_qury = new OleDbCommand(Select, (OleDbConnection)DataBaseConnection);
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
                    OleDbCommand cmd = GetCommand(OBISDetailsSMT_DAO.Insert);
                    cmd.Parameters.Add(new OleDbParameter(OBISDetailsSMT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OleDbParameter(OBISDetailsSMT_DAO.Obis_Code, Row.Obis_Code));
                    cmd.Parameters.Add(new OleDbParameter(OBISDetailsSMT_DAO.Default_OBIS_Code, Row.Default_OBIS_Code));
                    cmd.Parameters.Add(new OleDbParameter(OBISDetailsSMT_DAO.Device_Id, Row.Device_Id));
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
                    OleDbCommand cmd = GetCommand(OBISDetailsSMT_DAO.Update);
                    ///Parameters Reflecting Modification
                    cmd.Parameters.Add(new OleDbParameter(OBISDetailsSMT_DAO.Obis_Code, Row.Obis_Code));
                    cmd.Parameters.Add(new OleDbParameter(OBISDetailsSMT_DAO.Default_OBIS_Code, Row.Default_OBIS_Code));
                    cmd.Parameters.Add(new OleDbParameter(OBISDetailsSMT_DAO.Device_Id, Row.Device_Id));
                    ///Parameters Before Modification
                    cmd.Parameters.Add(new OleDbParameter(OBISDetailsSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
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
                    OleDbCommand cmd = GetCommand(OBISDetailsSMT_DAO.Delete);
                    cmd.Parameters.Add(new OleDbParameter(OBISDetailsSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

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

    public class OBISRightsGroupSMT_DAO : SMT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from [Obis_Rights_Group] order by [id] desc";
        public static readonly string Insert = "INSERT INTO [Obis_Rights_Group] ([id], [Group_Name],[Update_Rights]) VALUES (@id, @Group_Name,@Update_Rights)";
        public static readonly string Update = "UPDATE [Obis_Rights_Group] SET [Group_Name] = @Group_Name, [Update_Rights] = @Update_Rights "
            + "WHERE [id] = @id_";
        public static readonly string Delete = "DELETE FROM [Obis_Rights_Group] "
            + "WHERE [id] = @id_";

        public static readonly string IdParam = "@id";
        public static readonly string Group_Name = "@Group_Name";
        public static readonly string Update_Rights = "@Update_Rights";

        #endregion

        public OBISRightsGroupSMT_DAO(string connectionString)
            : base(connectionString)
        { }
        public OBISRightsGroupSMT_DAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadOBISRightsGroup(Configs DataSet)
        {
            try
            {
                OleDbDataAdapter ConfigsDataAdapeter =
                    new OleDbDataAdapter(new OleDbCommand(Select,(OleDbConnection)DataBaseConnection));
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
                OleDbCommand Select_Sql_Cmd = new OleDbCommand(Select, (OleDbConnection)DataBaseConnection);
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
                    OleDbCommand cmd = GetCommand(OBISRightsGroupSMT_DAO.Insert);
                    cmd.Parameters.Add(new OleDbParameter(OBISRightsGroupSMT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OleDbParameter(OBISRightsGroupSMT_DAO.Group_Name, Row.Group_Name));
                    cmd.Parameters.Add(new OleDbParameter(OBISRightsGroupSMT_DAO.Update_Rights, Row.Update_Rights));
                    //cmd.Parameters.Add(new OleDbParameter(MeterTypeInfoDAO.FirmWareVersionParam, Row.FirmWareVersion));
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
                    OleDbCommand cmd = GetCommand(OBISRightsGroupSMT_DAO.Update);
                    ///Reflected Changes Parameters
                    cmd.Parameters.Add(new OleDbParameter(OBISRightsGroupSMT_DAO.Group_Name, Row.Group_Name));
                    cmd.Parameters.Add(new OleDbParameter(OBISRightsGroupSMT_DAO.Update_Rights, Row.Update_Rights));

                    /// Parameters Before Modification
                    cmd.Parameters.Add(new OleDbParameter(OBISRightsGroupSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

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
                    OleDbCommand cmd = GetCommand(OBISRightsGroupSMT_DAO.Delete);
                    cmd.Parameters.Add(new OleDbParameter(OBISRightsGroupSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
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

    public class OBISRithtsSMT_DAO : SMT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from [OBIS_Rights] order by [id],[OBIS_Index]";

        public static readonly string SelectByGroupInfo = "Select * from [OBIS_Rights] WHERE ([ObisRightGroupId] = @ObisRightGroupId ) order by [id],[OBIS_Index]";

        public static readonly string SelectMaxId = "Select Max([id]) from [OBIS_Rights]";

        public static readonly string Insert = "INSERT INTO [OBIS_Rights] ([id], [ObisRightGroupId], [OBIS_Index], [Version]) " +
                                      "VALUES (@id, @ObisRightGroupId, @OBIS_Index, @version)";

        public static readonly string Update = "UPDATE [OBIS_Rights] SET [ObisRightGroupId] = @ObisRightGroupId, [OBIS_Index] = @OBIS_Index, [Version] = @version" +
             "WHERE (([id] = @id_) AND ((@id_ = 1 AND [ObisRightGroupId] IS NULL) OR ([ObisRightGroupId] = @ObisRightGroupId)) AND  ((@id_ = 1 AND [OBIS_Index] IS NULL) OR ([OBIS_Index] = @OBIS_Index_)) AND ((@id_ = 1 AND [Version] IS NULL) OR ([Version] = @version_)))";

        public static readonly string Delete = "DELETE FROM [OBIS_Rights] " +
               "WHERE (([id] = @id_) AND ((@id_ = 1 AND [ObisRightGroupId] IS NULL) OR ([ObisRightGroupId] = @ObisRightGroupId)) AND  ((@id_ = 1 AND [OBIS_Index] IS NULL) OR ([OBIS_Index] = @OBIS_Index_)) AND ((@id_ = 1 AND [Version] IS NULL) OR ([Version] = @version_)))";

        public static readonly string IdParam = "@id";
        public static readonly string ObisRightGroupId = "@ObisRightGroupId";
        public static readonly string OBISIndexParam = "@OBIS_Index";
        public static readonly string VersionParam = "@version";
        #endregion

        public OBISRithtsSMT_DAO(string connectionString)
            : base(connectionString)
        {

        }
        public OBISRithtsSMT_DAO(IDbConnection connection)
            : base(connection)
        {

        }

        public void Load_OBIS_Rights(Configs DataSet)
        {
            try
            {
                OleDbDataAdapter OBIS_Rights_DataAdapeter =
                    new OleDbDataAdapter(new OleDbCommand(Select, (OleDbConnection)DataBaseConnection));
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
                OleDbCommand SelectByGroupInfo = GetCommand(OBISRithtsSMT_DAO.SelectByGroupInfo);
                SelectByGroupInfo.Parameters.Add(new OleDbParameter(OBISRithtsSMT_DAO.ObisRightGroupId, OBISRightGroupRow.id));
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
                OleDbCommand Select_MaxOBISCodeId = GetCommand(OBISRithtsSMT_DAO.SelectMaxId);
                OleDbDataReader reader = Select_MaxOBISCodeId.ExecuteReader();
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
                OleDbCommand Sel_qury = new OleDbCommand(Select, (OleDbConnection)DataBaseConnection);
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
                // Exec Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    OleDbCommand cmd = GetCommand(OBISRithtsSMT_DAO.Insert);
                    cmd.Parameters.Add(new OleDbParameter(OBISRithtsSMT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OleDbParameter(OBISRithtsSMT_DAO.ObisRightGroupId, Row.ObisRightGroupId));
                    cmd.Parameters.Add(new OleDbParameter(OBISRithtsSMT_DAO.OBISIndexParam, Row.OBIS_Index));
                    cmd.Parameters.Add(new OleDbParameter(OBISRithtsSMT_DAO.VersionParam, Row.Version));
                    cmd.ExecuteNonQuery();
                    // Row.AcceptChanges();

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
                // Exec Insert Query
                if (Row.RowState == DataRowState.Modified)
                {
                    OleDbCommand cmd = GetCommand(OBISRithtsSMT_DAO.Update);
                    ///Parameters Reflecting Modification
                    cmd.Parameters.Add(new OleDbParameter(OBISRithtsSMT_DAO.ObisRightGroupId, Row.ObisRightGroupId));
                    cmd.Parameters.Add(new OleDbParameter(OBISRithtsSMT_DAO.OBISIndexParam, Row.OBIS_Index));
                    cmd.Parameters.Add(new OleDbParameter(OBISRithtsSMT_DAO.VersionParam, Row.Version));
                    ///Parameters Before Modification
                    cmd.Parameters.Add(new OleDbParameter(OBISRithtsSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(OBISRithtsSMT_DAO.ObisRightGroupId + "_", Convert.ToInt32(Row["ObisRightGroupId", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(OBISRithtsSMT_DAO.OBISIndexParam + "_", Convert.ToDecimal(Row["OBIS_Index", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(OBISRithtsSMT_DAO.VersionParam + "_", Convert.ToByte(Row["Version", DataRowVersion.Original])));
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
                    OleDbCommand cmd = GetCommand(OBISRithtsSMT_DAO.Delete);
                    cmd.Parameters.Add(new OleDbParameter(OBISRithtsSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(OBISRithtsSMT_DAO.ObisRightGroupId + "_", Convert.ToInt32(Row["ObisRightGroupId", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(OBISRithtsSMT_DAO.OBISIndexParam + "_", Convert.ToDecimal(Row["OBIS_Index", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(OBISRithtsSMT_DAO.VersionParam + "_", Convert.ToByte(Row["Version", DataRowVersion.Original])));

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

    public class RithtsSMT_DAO : SMT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from [Rights] order by [id],[OBIS_Right_Id]";

        public static readonly string SelectMaxId = "Select Max([id]) from [Rights]";

        public static readonly string SelectByOBISRightId = "Select * from [Rights] WHERE [OBIS_Right_Id] = @OBIS_Rights_Id order by [id],[OBIS_Right_Id]";

        public static readonly string Insert = "INSERT INTO [Rights] ([id], [type], [SubId], [value], [OBIS_Right_Id]) " +
                    " VALUES (@id, @type, @SubId, @value, @OBIS_Rights_Id)";

        public static readonly string Update = "UPDATE [Rights] SET [type] = @type, [SubId] = @SubId, [value] = @value,"
                    + "[OBIS_Right_Id] = @OBIS_Rights_Id " +
                    "WHERE (((@id_ = 1 AND [id] IS NULL) OR ([id] = @id_)) AND ([type] =  @type_) AND ([SubId] =  @SubId_)"
                    + "AND ((@value_ = 1 AND [value] IS NULL) OR ([value] = @value_)) AND ([OBIS_Right_Id] = @OBIS_Right_Id_))";


        public static readonly string Delete = "DELETE FROM [Rights] WHERE (((@id = 1 AND [id] IS NULL) OR ([id] = @id)) AND ([type] = @type) " +
                    " AND ([SubId] = @SubId) AND ((@value = 1 AND [value] IS NULL) OR ([value] = @value)) AND ([OBIS_Rights_Id] = @OBIS_Rights_Id))";

        public static readonly string IdParam = "@id";
        public static readonly string TypeParam = "@type";
        public static readonly string SubIdParam = "@SubId";
        public static readonly string ValueParam = "@value";
        public static readonly string OBIS_RightsParam = "@OBIS_Rights_Id";
        #endregion

        public RithtsSMT_DAO(string connectionString) : base(connectionString) { }
        public RithtsSMT_DAO(IDbConnection connection) : base(connection) { }

        public void Load_Rights(Configs DataSet)
        {
            try
            {
                OleDbDataAdapter OBIS_Rights_DataAdapeter =
                    new OleDbDataAdapter(new OleDbCommand(Select, (OleDbConnection)DataBaseConnection));
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
                    OleDbCommand SelectOBISRights = GetCommand(RithtsSMT_DAO.SelectByOBISRightId);
                    SelectOBISRights.Parameters.Add(new OleDbParameter(RithtsSMT_DAO.OBIS_RightsParam, OBISRight.id));
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
                OleDbCommand Select_MaxOBISCodeId = GetCommand(RithtsSMT_DAO.SelectMaxId);
                OleDbDataReader reader = Select_MaxOBISCodeId.ExecuteReader(CommandBehavior.SingleResult);
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
                OleDbCommand Sel_qury = new OleDbCommand(Select, (OleDbConnection)DataBaseConnection);
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
                    OleDbCommand cmd = GetCommand(RithtsSMT_DAO.Insert);
                    cmd.Parameters.Add(new OleDbParameter(RithtsSMT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OleDbParameter(RithtsSMT_DAO.TypeParam, Row.type));
                    cmd.Parameters.Add(new OleDbParameter(RithtsSMT_DAO.SubIdParam, Row.SubId));
                    cmd.Parameters.Add(new OleDbParameter(RithtsSMT_DAO.ValueParam, Row.value));
                    cmd.Parameters.Add(new OleDbParameter(RithtsSMT_DAO.OBIS_RightsParam, Row.OBIS_Right_Id));
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
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    OleDbCommand cmd = GetCommand(RithtsSMT_DAO.Update);
                    ///Parameters Reflecting Change
                    cmd.Parameters.Add(new OleDbParameter(RithtsSMT_DAO.TypeParam, Row.type));
                    cmd.Parameters.Add(new OleDbParameter(RithtsSMT_DAO.SubIdParam, Row.SubId));
                    cmd.Parameters.Add(new OleDbParameter(RithtsSMT_DAO.ValueParam, Row.value));
                    cmd.Parameters.Add(new OleDbParameter(RithtsSMT_DAO.OBIS_RightsParam, Row.OBIS_Right_Id));
                    ///Parameters Before Chagnge
                    cmd.Parameters.Add(new OleDbParameter(RithtsSMT_DAO.IdParam + "-", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(RithtsSMT_DAO.TypeParam + "-", Convert.ToByte(Row["type", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(RithtsSMT_DAO.SubIdParam + "-", Convert.ToByte(Row["SubId", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(RithtsSMT_DAO.ValueParam + "-", Convert.ToByte(Row["value", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(RithtsSMT_DAO.OBIS_RightsParam + "-", Convert.ToInt64(Row["OBIS_Right_Id", DataRowVersion.Original])));

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
                    OleDbCommand cmd = GetCommand(RithtsSMT_DAO.Delete);
                    cmd.Parameters.Add(new OleDbParameter(RithtsSMT_DAO.IdParam, Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(RithtsSMT_DAO.TypeParam, Convert.ToByte(Row["type", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(RithtsSMT_DAO.SubIdParam, Convert.ToByte(Row["SubId", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(RithtsSMT_DAO.ValueParam, Convert.ToByte(Row["value", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(RithtsSMT_DAO.OBIS_RightsParam, Convert.ToInt64(Row["OBIS_Right_Id", DataRowVersion.Original])));

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

    public class DisplayWindowDAO : SMT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from [DisplayWindows] order by [id],[QuantityIndex],[SequenceId]";

        public static readonly string Insert = "INSERT INTO [DisplayWindows] ([id], [Label], [Category], [AttributeNo], [WinNumberToDisplay], [QuantityIndex], [SequenceId], [DisplayWindowsGroupId]) " +
                                                "VALUES (@id,@label,  @category, @attributeNo, @winNumber, @QuantityIndex, @sequenceId, @DisplayWindowsGroupId)";

        public static readonly string Update = "UPDATE [DisplayWindows] SET  [Category] = @category, [Label] = @label, [AttributeNo] = @attributeNo, [WinNumberToDisplay] = @winNumber, " +
                                                "[QuantityIndex] = @QuantityIndex, [SequenceId] = @sequenceId, [DisplayWindowsGroupId] = @DisplayWindowsGroupId " +
                                                 "WHERE (([id] =  @id_) AND (([Category] IS NULL) OR ([Category] = @category_))  AND (([AttributeNo] IS NULL) OR " +
                                                "([AttributeNo] = @attributeNo_)) AND (([WinNumberToDisplay] IS NULL) OR ([WinNumberToDisplay] = @winNumber_)) AND ([QuantityIndex] = @QuantityIndex_) AND " +
                                                "(([SequenceId] IS NULL) OR ([SequenceId] = @sequenceId_)) AND ([DisplayWindowsGroupId] = @DisplayWindowsGroupId_))";


        public static readonly string Delete = "DELETE FROM [DisplayWindows] " +
                                                "WHERE (([id] =  @id_) AND (([Category] IS NULL) OR ([Category] = @category_))  AND (([AttributeNo] IS NULL) OR " +
                                                "([AttributeNo] = @attributeNo_)) AND (([WinNumberToDisplay] IS NULL) OR ([WinNumberToDisplay] = @winNumber_)) AND ([QuantityIndex] = @QuantityIndex_) AND " +
                                                "(([SequenceId] IS NULL) OR ([SequenceId] = @sequenceId_)) AND ([DisplayWindowsGroupId] = @DisplayWindowsGroupId_))";

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

        public DisplayWindowDAO(string connectionString) : base(connectionString) { }
        public DisplayWindowDAO(IDbConnection connection) : base(connection) { }

        public void Load_Display_Windows(Configs DataSet)
        {
            try
            {
                OleDbDataAdapter DisplayWindows_DataAdapeter =
                    new OleDbDataAdapter(new OleDbCommand(Select, (OleDbConnection)DataBaseConnection));
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
                OleDbCommand OLE_DB_Cmd = new OleDbCommand(Select, (OleDbConnection)DataBaseConnection);
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
                    OleDbCommand cmd = GetCommand(DisplayWindowDAO.Insert);
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.LabelParam, Row.Label));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.CategoryParam, Row.Category));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.AttributeNoParam, Row.AttributeNo));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.WindowNumberParam, Row.WinNumberToDisplay));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.OBISIndexParam, Row.QuantityIndex));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.SequenceIdParam, Row.SequenceId));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.DisplayWindowsGroupIdParam, Row.DisplayWindowsGroupId));
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
                    OleDbCommand cmd = GetCommand(DisplayWindowDAO.Update);
                    // Parameters Reflecting Changes
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.CategoryParam, Row.Category));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.LabelParam, Row.Label));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.AttributeNoParam, Row.AttributeNo));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.WindowNumberParam, Row.WinNumberToDisplay));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.OBISIndexParam, Row.QuantityIndex));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.SequenceIdParam, Row.SequenceId));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.DisplayWindowsGroupIdParam, Row.DisplayWindowsGroupId));
                    // Parameters Before Changes
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    // cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.LabelParam + "_", Convert.ToString(Row["Label", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.CategoryParam + "_", Convert.ToByte(Row["Category", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.AttributeNoParam + "_", Convert.ToByte(Row["AttributeNo", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.WindowNumberParam + "_", Convert.ToUInt32(Row["WinNumberToDisplay", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.OBISIndexParam + "_", Convert.ToUInt64(Row["QuantityIndex", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.SequenceIdParam + "_", Convert.ToUInt32(Row["SequenceId", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.DisplayWindowsGroupIdParam + "_", Convert.ToUInt32(Row["DisplayWindowsGroupId", DataRowVersion.Original])));

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
                    OleDbCommand cmd = GetCommand(DisplayWindowDAO.Delete);
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.CategoryParam + "_", Convert.ToByte(Row["Category", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.AttributeNoParam + "_", Convert.ToByte(Row["AttributeNo", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.WindowNumberParam + "_", Convert.ToUInt32(Row["WinNumberToDisplay", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.OBISIndexParam + "_", Convert.ToUInt64(Row["QuantityIndex", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.SequenceIdParam + "_", Convert.ToUInt32(Row["SequenceId", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowDAO.DisplayWindowsGroupIdParam + "_", Convert.ToUInt32(Row["DisplayWindowsGroupId", DataRowVersion.Original])));
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

    public class DisplayWindowsGroupDAO : SMT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from [DisplayWindows_Group] order by [Dw-Group_Name] desc";
        public static readonly string Insert = "INSERT INTO [DisplayWindows_Group] ([id], [Dw-Group_Name]) VALUES (@id, @Dw_Group_Name)";
        public static readonly string Update = "UPDATE [DisplayWindows_Group] SET [Dw-Group_Name] = @Dw_Group_Name "
            + "WHERE [id] = @id_";
        public static readonly string Delete = "DELETE FROM [DisplayWindows_Group]"
            + "WHERE [id] = @id_";

        public static readonly string IdParam = "@id";
        public static readonly string DWGroupName = "@Dw_Group_Name";

        #endregion

        public DisplayWindowsGroupDAO(string connectionString)
            : base(connectionString)
        { }
        public DisplayWindowsGroupDAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadDisplayWindowsGroup(Configs DataSet)
        {
            try
            {
                OleDbDataAdapter ConfigsDataAdapeter =
                    new OleDbDataAdapter(new OleDbCommand(Select, (OleDbConnection)DataBaseConnection));
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
                OleDbCommand Select_Sql_Cmd = new OleDbCommand(Select, (OleDbConnection)DataBaseConnection);
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
                    OleDbCommand cmd = GetCommand(DisplayWindowsGroupDAO.Insert);
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowsGroupDAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowsGroupDAO.DWGroupName, Row.Dw_Group_Name));
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
                    OleDbCommand cmd = GetCommand(DisplayWindowsGroupDAO.Update);
                    ///Reflected Changes Parameters
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowsGroupDAO.DWGroupName, Row.Dw_Group_Name));
                    ///Before Changes Parameters
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowsGroupDAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

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
                    OleDbCommand cmd = GetCommand(DisplayWindowsGroupDAO.Delete);
                    cmd.Parameters.Add(new OleDbParameter(DisplayWindowsGroupDAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
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

    public class LoadProfileSMT_DAO : SMT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from [LoadProfileChannels] order by [id],[QuantityIndex],[SequenceId]";

        public static readonly string Insert = "INSERT INTO [LoadProfileChannels] ([id],[Label], [QuantityIndex], [AttributeNo], [Multiplier], [SequenceId], [FormatSpecifier], [Unit], [LoadProfileGroupId]) " +
                                               "VALUES (@id,@label, @quantityIndex, @attributeNo, @multiplier, @sequenceId, @formatSpecifier, @unit, @LoadProfileGroupId)";

        public static readonly string Update = "UPDATE [LoadProfileChannels] SET [Label] = @label, [QuantityIndex] = @quantityIndex, [AttributeNo] = @attributeNo, [Multiplier] = @multiplier, " +
        "[SequenceId] = @sequenceId, [FormatSpecifier] = @formatSpecifier, [Unit] = @unit, [LoadProfileGroupId] = @LoadProfileGroupId " +
         "WHERE ((([id] IS NULL) OR ([id] = @id_)))";

        public static readonly string Delete = "DELETE FROM [LoadProfileChannels] " +
        "WHERE ((([id] IS NULL) OR ([id] = @id_)))";

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

        public LoadProfileSMT_DAO(string connectionString) : base(connectionString) { }
        public LoadProfileSMT_DAO(IDbConnection connection) : base(connection) { }

        public void Load_Profiles(Configs DataSet)
        {
            try
            {
                OleDbDataAdapter LoadProfile_DataAdapeter =
                    new OleDbDataAdapter(new OleDbCommand(Select, (OleDbConnection)DataBaseConnection));
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
                OleDbCommand Sel_Load_Profile_Cmd = new OleDbCommand(Select, (OleDbConnection)DataBaseConnection);
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
                    OleDbCommand cmd = GetCommand(LoadProfileSMT_DAO.Insert);
                    cmd.Parameters.Add(new OleDbParameter(LoadProfileSMT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OleDbParameter(LoadProfileSMT_DAO.LabelParam, Row.Label));
                    cmd.Parameters.Add(new OleDbParameter(LoadProfileSMT_DAO.OBISIndexParam, Row.QuantityIndex));
                    cmd.Parameters.Add(new OleDbParameter(LoadProfileSMT_DAO.AttributeNoParam, Row.AttributeNo));
                    cmd.Parameters.Add(new OleDbParameter(LoadProfileSMT_DAO.MultiplierParam, Row.Multiplier));
                    cmd.Parameters.Add(new OleDbParameter(LoadProfileSMT_DAO.SequenceIdParam, Row.SequenceId));
                    cmd.Parameters.Add(new OleDbParameter(LoadProfileSMT_DAO.FormatSpecifierParam, Row.FormatSpecifier));
                    cmd.Parameters.Add(new OleDbParameter(LoadProfileSMT_DAO.UnitParam, Row.Unit));
                    cmd.Parameters.Add(new OleDbParameter(LoadProfileSMT_DAO.LoadProfileGroupIdParam, Row.LoadProfileGroupId));
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
                    OleDbCommand cmd = GetCommand(LoadProfileSMT_DAO.Update);
                    ///Parameters Reflecting Changes
                    cmd.Parameters.Add(new OleDbParameter(LoadProfileSMT_DAO.LabelParam, Row.Label));
                    cmd.Parameters.Add(new OleDbParameter(LoadProfileSMT_DAO.OBISIndexParam, Row.QuantityIndex));
                    cmd.Parameters.Add(new OleDbParameter(LoadProfileSMT_DAO.AttributeNoParam, Row.AttributeNo));
                    cmd.Parameters.Add(new OleDbParameter(LoadProfileSMT_DAO.MultiplierParam, Row.Multiplier));
                    cmd.Parameters.Add(new OleDbParameter(LoadProfileSMT_DAO.SequenceIdParam, Row.SequenceId));
                    cmd.Parameters.Add(new OleDbParameter(LoadProfileSMT_DAO.FormatSpecifierParam, Row.FormatSpecifier));
                    cmd.Parameters.Add(new OleDbParameter(LoadProfileSMT_DAO.UnitParam, Row.Unit));
                    cmd.Parameters.Add(new OleDbParameter(LoadProfileSMT_DAO.LoadProfileGroupIdParam, Row.LoadProfileGroupId));
                    ///Parameters Before Changes
                    cmd.Parameters.Add(new OleDbParameter(LoadProfileSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileDAO.MultiplierParam + "_", Convert.ToInt32(Row["AttributeNo", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileDAO.AttributeNoParam + "_", Convert.ToByte(Row["AttributeNo", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileDAO.OBISIndexParam + "_", Convert.ToUInt64(Row["QuantityIndex", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileDAO.SequenceIdParam + "_", Convert.ToUInt32(Row["SequenceId", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileDAO.FormatSpecifierParam + "_", Convert.ToString(Row["FormatSpecifier", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileDAO.UnitParam + "_", Convert.ToString(Row["Unit", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileDAO.LoadProfileGroupIdParam + "_", Convert.ToInt32(Row["LoadProfileGroupId", DataRowVersion.Original])));
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
                    OleDbCommand cmd = GetCommand(LoadProfileSMT_DAO.Delete);
                    cmd.Parameters.Add(new OleDbParameter(LoadProfileSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileDAO.MultiplierParam + "_", Convert.ToInt32(Row["AttributeNo", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileDAO.AttributeNoParam + "_", Convert.ToByte(Row["AttributeNo", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileDAO.OBISIndexParam + "_", Convert.ToUInt64(Row["QuantityIndex", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileDAO.SequenceIdParam  + "_", Convert.ToUInt32(Row["SequenceId", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileDAO.FormatSpecifierParam + "_", Convert.ToString(Row["FormatSpecifier", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileDAO.UnitParam + "_", Convert.ToString(Row["Unit", DataRowVersion.Original])));
                    //cmd.Parameters.Add(new OleDbParameter(LoadProfileDAO.LoadProfileGroupIdParam + "_", Convert.ToInt32(Row["LoadProfileGroupId", DataRowVersion.Original])));
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

    public class LoadProfileGroupSMT_DAO : SMT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from [LoadProfile_Group] order by [LoadProfile_Group_Name] desc";
        public static readonly string Insert = "INSERT INTO [LoadProfile_Group] ([id], [LoadProfile_Group_Name]) VALUES (@id, @LoadProfile_Group_Name)";
        public static readonly string Update = "UPDATE [LoadProfile_Group] SET [LoadProfile_Group_Name] = @LoadProfile_Group_Name "
            + "WHERE [id] = @id_";
        public static readonly string Delete = "DELETE FROM [LoadProfile_Group]"
            + "WHERE [id] = @id_";

        public static readonly string IdParam = "@id";
        public static readonly string LPGroupName = "@LoadProfile_Group_Name";

        #endregion

        public LoadProfileGroupSMT_DAO(string connectionString)
            : base(connectionString)
        { }
        public LoadProfileGroupSMT_DAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadLoadProfileGroup(Configs DataSet)
        {
            try
            {
                OleDbDataAdapter ConfigsDataAdapeter =
                    new OleDbDataAdapter(new OleDbCommand(Select, (OleDbConnection)DataBaseConnection));
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
                OleDbCommand Select_Sql_Cmd = new OleDbCommand(Select, (OleDbConnection)DataBaseConnection);
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
                    OleDbCommand cmd = GetCommand(LoadProfileGroupSMT_DAO.Insert);
                    cmd.Parameters.Add(new OleDbParameter(LoadProfileGroupSMT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OleDbParameter(LoadProfileGroupSMT_DAO.LPGroupName, Row.LoadProfile_Group_Name));
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
                    OleDbCommand cmd = GetCommand(LoadProfileGroupSMT_DAO.Update);
                    ///Reflected Changes Parameters
                    cmd.Parameters.Add(new OleDbParameter(LoadProfileGroupSMT_DAO.LPGroupName, Row.LoadProfile_Group_Name));
                    ///Before Changes Parameters
                    cmd.Parameters.Add(new OleDbParameter(LoadProfileGroupSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

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
                    OleDbCommand cmd = GetCommand(LoadProfileGroupSMT_DAO.Delete);
                    cmd.Parameters.Add(new OleDbParameter(LoadProfileGroupSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
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

    public class BillItemSMT_DAO : SMT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from [BillingItems] order by [id],[BillItemGroupId], [SequenceId]";

        public static readonly string Insert = "INSERT INTO [BillingItems] ([id], [Label], [FormatSpecifier], [Unit], [Multiplier], [SequenceId], [BillItemGroupId]) " +
                                               "VALUES (@id, @label, @formatSpecifier, @unit, @multiplier, @sequenceId, @BillItemGroupId)";

        public static readonly string Update = "UPDATE [BillingItems] SET [Label] = @label, [FormatSpecifier] = @formatSpecifier, [Unit] = @unit," +
            "[Multiplier] = @multiplier, [SequenceId] = @sequenceId, [BillItemGroupId] = @BillItemGroupId " +
        "WHERE (([id] = @id_) AND (([Label] IS NULL) OR ([Label] = @label_)) AND (([FormatSpecifier] IS NULL) OR ([FormatSpecifier] = @formatSpecifier_)) AND " +
        "(([Unit] IS NULL) OR ([Unit] = @unit_)) AND (([Multiplier] IS NULL) OR ([Multiplier] = @multiplier)) AND (([SequenceId] IS NULL) OR ([SequenceId] = @sequenceId)) AND " +
        "(([BillItemGroupId] IS NULL) OR ([BillItemGroupId] = @BillItemGroupId)))";

        public static readonly string Delete = "DELETE FROM [BillingItems] " +
        "WHERE (([id] = @id_) AND (([Label] IS NULL) OR ([Label] = @label_)) AND (([FormatSpecifier] IS NULL) OR ([FormatSpecifier] = @formatSpecifier_)) AND " +
        "(([Unit] IS NULL) OR ([Unit] = @unit_)) AND (([Multiplier] IS NULL) OR ([Multiplier] = @multiplier_)) AND (([SequenceId] IS NULL) OR ([SequenceId] = @sequenceId_)) AND " +
        "(([BillItemGroupId] IS NULL) OR ([BillItemGroupId] = @BillItemGroupId)))";

        // Named Parameters
        public static readonly string IdParam = "@id";
        public static readonly string LabelParam = "@label";
        public static readonly string FormatSpecifierParam = "@formatSpecifier";
        public static readonly string MultiplierParam = "@multiplier";
        public static readonly string SequenceIdParam = "@sequenceId";
        public static readonly string UnitParam = "@unit";
        public static readonly string BillItemGroupIdParam = "@BillItemGroupId";

        #endregion

        public BillItemSMT_DAO(string connectionString) : base(connectionString) { }
        public BillItemSMT_DAO(IDbConnection connection) : base(connection) { }

        public void Load_Billing_Items(Configs DataSet)
        {
            try
            {
                OleDbDataAdapter Event_Profile_Info_DataAdapeter =
                    new OleDbDataAdapter(new OleDbCommand(Select, (OleDbConnection)DataBaseConnection));

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
                OleDbCommand Event_Profile_Info_Sel_Cmd = new OleDbCommand(Select, (OleDbConnection)DataBaseConnection);

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
                    OleDbCommand cmd = GetCommand(BillItemSMT_DAO.Insert);
                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.LabelParam, Row.Label));
                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.FormatSpecifierParam, Row.FormatSpecifier));
                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.UnitParam, Row.Unit));
                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.MultiplierParam, Row.Multiplier));
                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.SequenceIdParam, Row.SequenceId));
                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.BillItemGroupIdParam, Row.BillItemGroupId));
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
                    OleDbCommand cmd = GetCommand(BillItemSMT_DAO.Update);
                    ///Parameters Reflecting Modifications
                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.LabelParam, Row.Label));
                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.FormatSpecifierParam, Row.FormatSpecifier));
                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.UnitParam, Row.Unit));
                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.MultiplierParam, Row.Multiplier));
                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.SequenceIdParam, Row.SequenceId));
                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.BillItemGroupIdParam, Row.BillItemGroupId));
                    ///Parameters Before Modifications
                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.LabelParam + "_", Convert.ToString(Row["Label", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.FormatSpecifierParam + "_", Convert.ToString(Row["FormatSpecifier", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.UnitParam + "_", Convert.ToString(Row["Unit", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.MultiplierParam + "_", Convert.ToInt32(Row["Multiplier", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.SequenceIdParam + "_", Convert.ToInt32(Row["SequenceId", DataRowVersion.Original])));

                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.BillItemGroupIdParam + "_", Convert.ToUInt32(Row["BillItemGroupId", DataRowVersion.Original])));

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
                    OleDbCommand cmd = GetCommand(BillItemSMT_DAO.Delete);
                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.LabelParam + "_", Convert.ToString(Row["Label", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.FormatSpecifierParam + "_", Convert.ToString(Row["FormatSpecifier", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.UnitParam + "_", Convert.ToString(Row["Unit", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.MultiplierParam + "_", Convert.ToInt32(Row["Multiplier", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.SequenceIdParam + "_", Convert.ToInt32(Row["SequenceId", DataRowVersion.Original])));

                    cmd.Parameters.Add(new OleDbParameter(BillItemSMT_DAO.BillItemGroupIdParam + "_", Convert.ToUInt32(Row["BillItemGroupId", DataRowVersion.Original])));

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

    public class BillingItemGroupSMT_DAO : SMT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from [BillingItem_Group] order by [BillingItem_Group_Name] desc";
        public static readonly string Insert = "INSERT INTO [BillingItem_Group] ([id], [BillingItem_Group_Name]) VALUES (@id, @BillingItem_Group_Name)";
        public static readonly string Update = "UPDATE [BillingItem_Group] SET [BillingItem_Group_Name] = @BillingItem_Group_Name "
            + "WHERE [id] = @id_";
        public static readonly string Delete = "DELETE FROM [BillingItem_Group]"
            + "WHERE [id] = @id_";

        public static readonly string IdParam = "@id";
        public static readonly string BIGroupName = "@BillingItem_Group_Name";

        #endregion

        public BillingItemGroupSMT_DAO(string connectionString)
            : base(connectionString)
        { }
        public BillingItemGroupSMT_DAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadBillingItemGroup(Configs DataSet)
        {
            try
            {
                OleDbDataAdapter ConfigsDataAdapeter =
                    new OleDbDataAdapter(new OleDbCommand(Select, (OleDbConnection)DataBaseConnection));
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
                OleDbCommand Select_Sql_Cmd = new OleDbCommand(Select, (OleDbConnection)DataBaseConnection);
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
                    OleDbCommand cmd = GetCommand(BillingItemGroupSMT_DAO.Insert);
                    cmd.Parameters.Add(new OleDbParameter(BillingItemGroupSMT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OleDbParameter(BillingItemGroupSMT_DAO.BIGroupName, Row.BillingItem_Group_Name));
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
                    OleDbCommand cmd = GetCommand(BillingItemGroupSMT_DAO.Update);
                    ///Reflected Changes Parameters
                    cmd.Parameters.Add(new OleDbParameter(BillingItemGroupSMT_DAO.BIGroupName, Row.BillingItem_Group_Name));
                    ///Before Changes Parameters
                    cmd.Parameters.Add(new OleDbParameter(BillingItemGroupSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

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
                    OleDbCommand cmd = GetCommand(BillingItemGroupSMT_DAO.Delete);
                    cmd.Parameters.Add(new OleDbParameter(BillingItemGroupSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
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

    public class BillTariffSMT_DAO : SMT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from [BillTariffQuantity] order by [BillItemID]";

        public static readonly string Insert = "INSERT INTO [BillTariffQuantity] ([BillItemId], [OBIS_Index], [SequenceId]) " +
                                                "VALUES (@billItemId, @OBIS_Index, @sequenceId)";

        public static readonly string Update = "UPDATE [BillTariffQuantity] SET [SequenceId] = @sequenceId, [OBIS_Index] = @OBIS_Index  " +
               "WHERE (([BillItemId] = @billItemId_) AND ([OBIS_Index] = @OBIS_Index_) AND (([SequenceId] IS NULL) OR ([SequenceId] = @sequenceId_)))";

        public static readonly string Delete = "DELETE FROM [BillTariffQuantity] " +
                "WHERE (([BillItemId] = @billItemId_) AND ([OBIS_Index] = @OBIS_Index_) AND (([SequenceId] IS NULL) OR ([SequenceId] = @sequenceId_)))";

        //Named Parameters

        public static readonly string BillIdParam = "@billItemId";
        public static readonly string OBISIndexParam = "@OBIS_Index";
        public static readonly string SequenceIdParam = "@sequenceId";

        #endregion

        public BillTariffSMT_DAO(string connectionString) : base(connectionString) { }
        public BillTariffSMT_DAO(IDbConnection connection) : base(connection) { }

        public void Load_Billing_Tariff(Configs DataSet)
        {
            try
            {
                OleDbDataAdapter Bill_Tariff_Info_DataAdapeter =
                    new OleDbDataAdapter(new OleDbCommand(Select,(OleDbConnection)DataBaseConnection));
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
                OleDbCommand Event_Profile_Info_Sel_Cmd = new OleDbCommand(Select,
                    (OleDbConnection)DataBaseConnection);
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
                    OleDbCommand cmd = GetCommand(BillTariffSMT_DAO.Insert);
                    cmd.Parameters.Add(new OleDbParameter(BillTariffSMT_DAO.BillIdParam, Row.BillItemId));
                    cmd.Parameters.Add(new OleDbParameter(BillTariffSMT_DAO.OBISIndexParam, Row.OBIS_Index));
                    cmd.Parameters.Add(new OleDbParameter(BillTariffSMT_DAO.SequenceIdParam, Row.SequenceId));
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
                    OleDbCommand cmd = GetCommand(BillTariffSMT_DAO.Update);
                    ///Parameters Reflecting Changes
                    cmd.Parameters.Add(new OleDbParameter(BillTariffSMT_DAO.SequenceIdParam, Row.SequenceId));
                    cmd.Parameters.Add(new OleDbParameter(BillTariffSMT_DAO.OBISIndexParam, Row.OBIS_Index));
                    ///Parameter Before Changes
                    cmd.Parameters.Add(new OleDbParameter(BillTariffSMT_DAO.BillIdParam + "_", Convert.ToInt32(Row["BillItemId", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(BillTariffSMT_DAO.OBISIndexParam + "_", Convert.ToUInt64(Row["OBIS_Index", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(BillTariffSMT_DAO.SequenceIdParam + "_", Convert.ToInt32(Row["SequenceId", DataRowVersion.Original])));

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
                    OleDbCommand cmd = GetCommand(BillTariffSMT_DAO.Delete);
                    cmd.Parameters.Add(new OleDbParameter(BillTariffSMT_DAO.BillIdParam + "_", Convert.ToInt32(Row["BillItemId", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(BillTariffSMT_DAO.OBISIndexParam + "_", Convert.ToUInt64(Row["OBIS_Index", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(BillTariffSMT_DAO.SequenceIdParam + "_", Convert.ToInt32(Row["SequenceId", DataRowVersion.Original])));

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

    public class EventInfoSMT_DAO : SMT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from [EventInfo] order by [id],[EventCode],[EventGroupId]";

        public static readonly string Insert = "INSERT INTO [EventInfo] ([id], [EventCode],[EventNo], [Label], [MaxEventCount], [EventGroupId],[CautionNumber],[FlashTime]) " +
                                               "VALUES (@id, @eventCode,@eventNo, @label, @maxEventCount, @EventGroupId,@CautionNumber,@FlashTime)";

        public static readonly string Update = "UPDATE [EventInfo] SET [EventNo] = @eventNo,[EventCode] =@eventCode,  [Label] = @label, [MaxEventCount] = @maxEventCount, [CautionNumber] = @CautionNumber, [FlashTime] = @FlashTime " +
            "WHERE (([id] IS NULL) OR ([id] = @id_)) And ([EventCode] = @eventCode_)";

        public static readonly string Delete = "DELETE FROM [EventInfo] " +
           "WHERE (([id] IS NULL) OR ([id] = @id_)) And ([EventCode] = @eventCode_)";

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

        public EventInfoSMT_DAO(string connectionString) : base(connectionString) { }
        public EventInfoSMT_DAO(IDbConnection connection) : base(connection) { }

        public void Load_EventProfileInfo(Configs DataSet)
        {
            try
            {
                OleDbDataAdapter Event_Profile_Info_DataAdapeter =
                    new OleDbDataAdapter(new OleDbCommand(Select, (OleDbConnection)DataBaseConnection));
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
                OleDbCommand Db_Sel_Cmd = new OleDbCommand(Select, (OleDbConnection)DataBaseConnection);
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
                    OleDbCommand cmd = GetCommand(EventInfoSMT_DAO.Insert);

                    cmd.Parameters.Add(new OleDbParameter(EventInfoSMT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OleDbParameter(EventInfoSMT_DAO.EventCodeParam, Row.EventCode));
                    cmd.Parameters.Add(new OleDbParameter(EventInfoSMT_DAO.EventNoParam, Row.EventNo));
                    cmd.Parameters.Add(new OleDbParameter(EventInfoSMT_DAO.LabelParam, Row.Label));
                    cmd.Parameters.Add(new OleDbParameter(EventInfoSMT_DAO.MaxEventCountParam, Row.MaxEventCount));
                    cmd.Parameters.Add(new OleDbParameter(EventInfoSMT_DAO.ConfigParam, Row.EventGroupId));

                    cmd.Parameters.Add(new OleDbParameter(EventInfoSMT_DAO.CautionNumberParam, Row.IsNull("CautionNumber") ?
                                      DBNull.Value : (object)Row.CautionNumber) { OleDbType = OleDbType.Integer });
                    cmd.Parameters.Add(new OleDbParameter(EventInfoSMT_DAO.FlashTimeParam, Row.IsNull("FlashTime") ?
                                      DBNull.Value : (object)Row.FlashTime) { OleDbType = OleDbType.Integer });

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
                    OleDbCommand cmd = GetCommand(EventInfoSMT_DAO.Update);
                    // Parameters Reflecting Changes
                    cmd.Parameters.Add(new OleDbParameter(EventInfoSMT_DAO.EventNoParam, Row.EventNo));
                    cmd.Parameters.Add(new OleDbParameter(EventInfoSMT_DAO.EventCodeParam, Row.EventCode));
                    cmd.Parameters.Add(new OleDbParameter(EventInfoSMT_DAO.LabelParam, Row.Label));
                    cmd.Parameters.Add(new OleDbParameter(EventInfoSMT_DAO.MaxEventCountParam, Row.MaxEventCount));

                    cmd.Parameters.Add(new OleDbParameter(EventInfoSMT_DAO.CautionNumberParam, Row.IsNull("CautionNumber") ?
                                      DBNull.Value : (object)Row.CautionNumber) { OleDbType = OleDbType.Integer });
                    cmd.Parameters.Add(new OleDbParameter(EventInfoSMT_DAO.FlashTimeParam, Row.IsNull("FlashTime") ?
                                      DBNull.Value : (object)Row.FlashTime) { OleDbType = OleDbType.Integer });

                    // Parameters Before Changes
                    cmd.Parameters.Add(new OleDbParameter(EventInfoSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(EventInfoSMT_DAO.EventCodeParam + "_", Convert.ToInt32(Row["EventCode", DataRowVersion.Original])));

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
                    OleDbCommand cmd = GetCommand(EventInfoSMT_DAO.Delete);

                    cmd.Parameters.Add(new OleDbParameter(EventInfoSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
                    cmd.Parameters.Add(new OleDbParameter(EventInfoSMT_DAO.EventCodeParam + "_", Convert.ToInt32(Row["EventCode", DataRowVersion.Original])));

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

    public class EventLogsSMT_DAO : SMT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from [EventLogs] order by [id],[EventLogIndex]";

        public static readonly string Insert = "INSERT INTO [EventLogs] ([id], [EventLogIndex], [EventCounterIndex],[EventGroupId]) " +
                                                "VALUES (@id, @eventLogIndex, @eventCounterIndex,@eventGroupId)";

        public static readonly string Update = "UPDATE [EventLogs] SET [EventLogIndex] = @eventLogIndex, [EventCounterIndex] = @eventCounterIndex,[EventGroupId] = @eventGroupId " +
                                                "WHERE (([id] IS NULL) OR ([id] = @id_))";

        public static readonly string Delete = "DELETE FROM [EventLogs] " +
                                                "WHERE (([id] IS NULL) OR ([id] = @id_))";

        /// Named Parameters
        public static readonly string IdParam = "@id";
        public static readonly string EventLogOBISIndexParam = "@eventLogIndex";
        public static readonly string EventCounterOBISIndexParam = "@eventCounterIndex";
        public static readonly string ConfigParam = "@eventGroupId";

        #endregion

        public EventLogsSMT_DAO(string connectionString) : base(connectionString) { }
        public EventLogsSMT_DAO(IDbConnection connection) : base(connection) { }

        public void Load_EventLogsInfo(Configs DataSet)
        {
            try
            {
                OleDbDataAdapter Event_Profile_Info_DataAdapeter =
                    new OleDbDataAdapter(new OleDbCommand(Select, (OleDbConnection)DataBaseConnection));
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
                OleDbCommand Db_Sel_Cmd = new OleDbCommand(Select, (OleDbConnection)DataBaseConnection);
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
                    OleDbCommand cmd = GetCommand(EventLogsSMT_DAO.Insert);
                    cmd.Parameters.Add(new OleDbParameter(EventLogsSMT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OleDbParameter(EventLogsSMT_DAO.EventLogOBISIndexParam, Row.EventLogIndex));
                    cmd.Parameters.Add(new OleDbParameter(EventLogsSMT_DAO.EventCounterOBISIndexParam, Row.EventCounterIndex));
                    cmd.Parameters.Add(new OleDbParameter(EventLogsSMT_DAO.ConfigParam, Row.EventGroupId));
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
                    OleDbCommand cmd = GetCommand(EventLogsSMT_DAO.Update);
                    ///Parameters Reflecting Changes
                    cmd.Parameters.Add(new OleDbParameter(EventLogsSMT_DAO.EventLogOBISIndexParam, Row.EventLogIndex));
                    cmd.Parameters.Add(new OleDbParameter(EventLogsSMT_DAO.EventCounterOBISIndexParam, Row.EventCounterIndex));
                    cmd.Parameters.Add(new OleDbParameter(EventLogsSMT_DAO.ConfigParam, Row.EventGroupId));

                    /// Parameters Before Changes
                    cmd.Parameters.Add(new OleDbParameter(EventLogsSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

                    /// cmd.Parameters.Add(new OleDbParameter(EventLogsDAO.EventLogOBISIndexParam + "_", Convert.ToUInt64(Row["EventLogIndex", DataRowVersion.Original])));
                    /// cmd.Parameters.Add(new OleDbParameter(EventLogsDAO.EventCounterOBISIndexParam + "_", Convert.ToUInt64(Row["EventCounterIndex", DataRowVersion.Original])));
                    /// cmd.Parameters.Add(new OleDbParameter(EventLogsDAO.ConfigParam + "_", Convert.ToInt32(Row["ConfigId", DataRowVersion.Original])));

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
                    OleDbCommand cmd = GetCommand(EventLogsSMT_DAO.Delete);
                    cmd.Parameters.Add(new OleDbParameter(EventLogsSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
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

    public class EventsGroupSMT_DAO : SMT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from [Events_Group] order by [Events_group_Name] desc";
        public static readonly string Insert = "INSERT INTO [Events_Group] ([id], [Events_group_Name]) VALUES (@id, @Events_group_Name)";
        public static readonly string Update = "UPDATE [Events_Group] SET [Events_group_Name] = @Events_group_Name "
            + "WHERE [id] = @id_";
        public static readonly string Delete = "DELETE FROM [Events_Group]"
            + "WHERE [id] = @id_";

        public static readonly string IdParam = "@id";
        public static readonly string EVGroupName = "@Events_group_Name";

        #endregion

        public EventsGroupSMT_DAO(string connectionString)
            : base(connectionString)
        { }
        public EventsGroupSMT_DAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadEventsGroup(Configs DataSet)
        {
            try
            {
                OleDbDataAdapter ConfigsDataAdapeter =
                    new OleDbDataAdapter(new OleDbCommand(Select, (OleDbConnection)DataBaseConnection));
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
                OleDbCommand Select_Sql_Cmd = new OleDbCommand(Select, (OleDbConnection)DataBaseConnection);
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
                    OleDbCommand cmd = GetCommand(EventsGroupSMT_DAO.Insert);
                    cmd.Parameters.Add(new OleDbParameter(EventsGroupSMT_DAO.IdParam, Row.id));
                    cmd.Parameters.Add(new OleDbParameter(EventsGroupSMT_DAO.EVGroupName, Row.Events_group_Name));
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
                    OleDbCommand cmd = GetCommand(EventsGroupSMT_DAO.Update);
                    /// Reflected Changes Parameters
                    cmd.Parameters.Add(new OleDbParameter(EventsGroupSMT_DAO.EVGroupName, Row.Events_group_Name));
                    /// Before Changes Parameters
                    cmd.Parameters.Add(new OleDbParameter(EventsGroupSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));

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
                    OleDbCommand cmd = GetCommand(EventsGroupSMT_DAO.Delete);
                    cmd.Parameters.Add(new OleDbParameter(EventsGroupSMT_DAO.IdParam + "_", Convert.ToInt32(Row["id", DataRowVersion.Original])));
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

    public class StatusWordDAO : SMT_DAO
    {
        #region Data Members

        public static readonly string Select = "Select * from [Status_Word]";
        public static readonly string Insert = "INSERT INTO [Status_Word] ([Code], [Name], [Display_Code],[P_Level]) VALUES (@Code, @Name,@Display_Code,@P_Leval)";
        public static readonly string Update = "UPDATE [Status_Word] SET [Name] = @Name, [Display_Code] = @Display_Code,[P_Level] =  @P_Leval "
            + "WHERE [Code] = @Code_";
        public static readonly string Delete = "DELETE FROM [Status_Word] "
            + "WHERE [Code] = @Code_";

        public static readonly string Code = "@Code";
        public static readonly string Name = "@Name";
        public static readonly string Display_Code = "@Display_Code";
        public static readonly string PLevel = "@P_Leval";

        #endregion

        public StatusWordDAO(string connectionString)
            : base(connectionString)
        { }
        public StatusWordDAO(IDbConnection connection)
            : base(connection)
        { }

        public void LoadStatusWord(Configs DataSet)
        {
            try
            {
                OleDbDataAdapter ConfigsDataAdapeter =
                    new OleDbDataAdapter(new OleDbCommand(Select, (OleDbConnection)DataBaseConnection));
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
                    OleDbCommand cmd = GetCommand(StatusWordDAO.Insert);
                    cmd.Parameters.Add(new OleDbParameter(StatusWordDAO.Code, Row.Code));
                    cmd.Parameters.Add(new OleDbParameter(StatusWordDAO.Name, Row.Name));
                    cmd.Parameters.Add(new OleDbParameter(StatusWordDAO.Display_Code, Row.Display_Code));
                    cmd.Parameters.Add(new OleDbParameter(StatusWordDAO.PLevel, Row.P_Leval));
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
                    OleDbCommand cmd = GetCommand(StatusWordDAO.Update);
                    /// Reflected Changes Parameters
                    cmd.Parameters.Add(new OleDbParameter(StatusWordDAO.Name, Row.Name));
                    cmd.Parameters.Add(new OleDbParameter(StatusWordDAO.Display_Code, Row.Display_Code));
                    cmd.Parameters.Add(new OleDbParameter(StatusWordDAO.PLevel, Row.P_Leval));
                    /// Before Changes Parameters
                    cmd.Parameters.Add(new OleDbParameter(StatusWordDAO.Code + "_", Convert.ToInt32(Row["Code", DataRowVersion.Original])));

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
                    OleDbCommand cmd = GetCommand(StatusWordDAO.Delete);
                    cmd.Parameters.Add(new OleDbParameter(StatusWordDAO.Code + "_", Convert.ToInt32(Row["Code", DataRowVersion.Original])));
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
}