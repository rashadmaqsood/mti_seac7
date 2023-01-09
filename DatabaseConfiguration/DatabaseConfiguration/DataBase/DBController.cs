using DatabaseConfiguration.CommonModels;
using DatabaseConfiguration.DataSet;
using DLMS;
using DLMS.Comm;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace DatabaseConfiguration.DataBase
{
    public class ConfigDBController : IDBAccessLayer
    {
        #region Fields

        DbConnection _connection;
        public ServicesModel _entityModel;

        public delegate void loadMethod(Configs con);
        public List<loadMethod> Load_All_Configs;

        public delegate void updateMethod(Configs con);
        public Dictionary<string, updateMethod> Update_All_Configs;

        #endregion

        #region Properties
        


        #endregion // Properties

        #region Constructors

        public ConfigDBController(DbConnection DbConn)
        {
            this._connection = DbConn;
            this._entityModel = new ServicesModel(this._connection);

            Load_All_Configs = new List<loadMethod>();
            this.Init_Load_All_Configs();

            Update_All_Configs = new Dictionary<string, updateMethod>();
            this.Init_Update_All_Configs();
        }

        public ConfigDBController(string connString, DataBaseTypes DbType)
        {
            try
            {
                this._connection = this.Make_DbConnection(connString, DbType);
                this._entityModel = new ServicesModel(this._connection);

                Load_All_Configs = new List<loadMethod>();
                this.Init_Load_All_Configs();

                Update_All_Configs = new Dictionary<string, updateMethod>();
                this.Init_Update_All_Configs();
            }
            catch (Exception ex)
            {
                this.LogException($"ConfigDBController()", ex);
            }
        }

        #endregion // Constructors

        #region Methods

        private DbConnection Make_DbConnection(string _connString, DataBaseTypes active_database)
        {
            DbConnection _conn = null;
            try
            {
                if (active_database == DataBaseTypes.MDC_DATABASE)
                {
                    _conn = new MySqlConnection
                    {
                        ConnectionString = _connString
                    };
                }
                else if (active_database == DataBaseTypes.SCT_DATABASE)
                {
                    _conn = new SqlConnection
                    {
                        ConnectionString = _connString
                    };
                }
                else if (active_database == DataBaseTypes.SEAC_DATABASE)
                {
                    _conn = new SQLiteConnection
                    {
                        ConnectionString = new SQLiteConnectionStringBuilder() { DataSource = _connString, ForeignKeys = true }.ConnectionString
                    };
                }
                else if (active_database == DataBaseTypes.MDC_DATABASE_With_ODBC)
                {
                    _conn = new OdbcConnection()
                    {
                        ConnectionString = _connString
                    };
                }
                else
                {
                    _conn = null;
                }

                // The constructor of this._entityModel i.e. DbContext(DbConnection existingConnection, bool contextOwnsConnection) opens connection automatically.
                // Basically it opens when a request is called and closes once the results have been disposed or consumed. You can manually open/close or use the same connection. [stackoverflow]
                // Manually opened connection can't be close auto.
                //_conn.Open(); 
            }
            catch (Exception ex)
            {
                this.LogException($"Make_DbConnection()", ex);
            }

            return _conn;
        }

        /// <summary>
        /// Usually EF opens and closes the connections automatically before/after an operation is completed. 
        /// However if you open a connection manually, EF will NOT close it for you after a database operation is completed.
        /// It is the best practice to close a connection manually, if you don't need it
        /// </summary>
        public void OpenConnection()
        {
            try
            {
                this._connection.Open();
            }
            catch (Exception ex)
            {
                this.LogException($"OpenConnection()", ex);
                throw ex;
            }
        }

        /// <summary>
        /// It is the best practice to close a connection manually, if you don't need it. Otherwise,
        /// the connection will be closed when your DbContext object is disposed (by the garbage collector for example)
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                if (this._connection == null) return;

                if (this._connection.State != ConnectionState.Closed)
                {
                    this._connection.Close();
                }
            }
            catch (Exception ex)
            {
                this.LogException($"OpenConnection()", ex);
                throw ex;
            }
        }

        #region Load_DB_Methods
        private void Init_Load_All_Configs()
        {
            try
            {
                Load_All_Configs.Add(Load_Manufac_Data);
                Load_All_Configs.Add(Load_Auth_Type);
                Load_All_Configs.Add(Load_Devices);
                Load_All_Configs.Add(Load_Configs);
                Load_All_Configs.Add(Load_OBIS_Right_Group);
                Load_All_Configs.Add(Load_Devices_Associations);
                Load_All_Configs.Add(Load_LP_Group_Data);
                Load_All_Configs.Add(Load_Event_Groups);
                Load_All_Configs.Add(Load_Bill_Item_Group);
                Load_All_Configs.Add(Load_All_Quan);
                Load_All_Configs.Add(Load_Obis_Details);
                Load_All_Configs.Add(Load_LP_Channels_Data);
                Load_All_Configs.Add(Load_Bill_Items);
                Load_All_Configs.Add(Load_Bill_Tariff_Quan);
                Load_All_Configs.Add(Load_Event_Info_Data);
                Load_All_Configs.Add(Load_Event_Logs_Data);
                Load_All_Configs.Add(Load_Capture_Obj);

                // Following tables not showing in DGVs yet
                Load_All_Configs.Add(Load_Display_Win_Group);
                Load_All_Configs.Add(Load_Display_Windows);
                Load_All_Configs.Add(Load_Obis_Rights);
                Load_All_Configs.Add(Load_Rights);
                Load_All_Configs.Add(Load_StatusWord);

                /*
                Load_All_Configs.Add(Load_All_Quan);
                Load_All_Configs.Add(Load_Auth_Type);
                Load_All_Configs.Add(Load_Bill_Item_Group);
                Load_All_Configs.Add(Load_Bill_Items);
                Load_All_Configs.Add(Load_Bill_Tariff_Quan);
                Load_All_Configs.Add(Load_Capture_Obj);
                Load_All_Configs.Add(Load_Configs);
                Load_All_Configs.Add(Load_Devices);
                Load_All_Configs.Add(Load_Devices_Associations);
                Load_All_Configs.Add(Load_Display_Windows);
                Load_All_Configs.Add(Load_Display_Win_Group);
                Load_All_Configs.Add(Load_Event_Info_Data);
                Load_All_Configs.Add(Load_Event_Logs_Data);
                Load_All_Configs.Add(Load_Event_Groups);
                Load_All_Configs.Add(Load_LP_Channels_Data);
                Load_All_Configs.Add(Load_LP_Group_Data);
                Load_All_Configs.Add(Load_Manufac_Data);
                Load_All_Configs.Add(Load_Obis_Details);
                Load_All_Configs.Add(Load_Obis_Rights);
                Load_All_Configs.Add(Load_OBIS_Right_Group);
                Load_All_Configs.Add(Load_Rights);
                Load_All_Configs.Add(Load_StatusWord);
                */
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load_OBIS_Right_Group(Configs AllDataSet)
        {
            try
            {
                var obis_right_group = _entityModel.Obis_Rights_Group_Data.ToList()
                                               .OrderBy(m => m.id);

                
                foreach (obis_rights_group obj in obis_right_group)
                {
                    Configs.Obis_Rights_GroupRow NEw = (Configs.Obis_Rights_GroupRow)AllDataSet.Obis_Rights_Group.NewRow();

                    NEw.id            = obj.id;
                    NEw.Group_Name    = obj.Group_Name;
                    NEw.Update_Rights = obj.Update_Rights;

                    AllDataSet.Obis_Rights_Group.AddObis_Rights_GroupRow(NEw);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load_LP_Group_Data(Configs AllDataSet)
        {
            try
            {
                var lp_group_data = _entityModel.LoadProfile_Group_Data.ToList()
                                              .OrderBy(m => m.id);

                foreach (loadprofile_group obj in lp_group_data)
                {
                    Configs.LoadProfile_GroupRow NEw = (Configs.LoadProfile_GroupRow)AllDataSet.LoadProfile_Group.NewRow();

                    NEw.id                     = obj.id;
                    NEw.LoadProfile_Group_Name = obj.LoadProfile_Group_Name;

                    AllDataSet.LoadProfile_Group.AddLoadProfile_GroupRow(NEw);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load_Event_Groups(Configs AllDataSet)
        {
            try
            {
                var ev_groups = _entityModel.Events_Group_Data.ToList()
                                              .OrderBy(m => m.id);

                foreach (events_group obj in ev_groups)
                {
                    Configs.Events_GroupRow NEw = (Configs.Events_GroupRow)AllDataSet.Events_Group.NewRow();

                    NEw.id                = obj.id;
                    NEw.Events_group_Name = obj.Events_Group_Name;

                    AllDataSet.Events_Group.AddEvents_GroupRow(NEw);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load_Bill_Item_Group(Configs AllDataSet)
        {
            try
            {
                var bill_item_group = _entityModel.BillingItem_Group_Data.ToList()
                                             .OrderBy(m => m.id);

                foreach (billingitem_group obj in bill_item_group)
                {
                    Configs.BillingItem_GroupRow NEw = (Configs.BillingItem_GroupRow)AllDataSet.BillingItem_Group.NewRow();

                    NEw.id                     = obj.id;
                    NEw.BillingItem_Group_Name = obj.BillingItem_Group_Name;

                    AllDataSet.BillingItem_Group.AddBillingItem_GroupRow(NEw);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load_Display_Win_Group(Configs AllDataSet)
        {
            try
            {
                var display_win_group = _entityModel.DisplayWindows_Group_Data.ToList()
                                            .OrderByDescending(m => m.Dw_Group_Name);

                foreach (displaywindows_group obj in display_win_group)
                {
                    Configs.DisplayWindows_GroupRow NEw = (Configs.DisplayWindows_GroupRow)AllDataSet.DisplayWindows_Group.NewRow();

                    NEw.id            = obj.id;
                    NEw.Dw_Group_Name = obj.Dw_Group_Name;

                    AllDataSet.DisplayWindows_Group.AddDisplayWindows_GroupRow(NEw);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load_Configs(Configs AllDataSet)
        {
            try
            {
                var configs = _entityModel.Configurations_Data.ToList()
                                            .OrderByDescending(m => m.Name);

                foreach (configuration_new obj in configs)
                {
                    Configs.ConfigurationRow NEw = (Configs.ConfigurationRow)AllDataSet.Configuration.NewRow();

                    NEw.id                   = obj.id;
                    NEw.Name                 = obj.Name;
                    NEw.lp_channels_group_id = obj.LP_Channels_Group_ID;
                    NEw.BillItemsGroupId     = obj.BillItemsGroupId;
                    NEw.DisplayWindowGroupId = obj.DisplayWindowGroupId ?? 0;
                    NEw.EventGroupId         = obj.EventGroupId;

                    AllDataSet.Configuration.AddConfigurationRow(NEw);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load_Manufac_Data(Configs AllDataSet)
        {
            try
            {
                //Thread.Sleep(5000);
                var manufac_data = _entityModel.Manufacturer_Data.ToList()
                                           .OrderByDescending(m => m.id);

                foreach (manufacturer obj in manufac_data)
                {
                    Configs.ManufacturerRow NEw = (Configs.ManufacturerRow)AllDataSet.Manufacturer.NewRow();

                    NEw.id                = obj.id;
                    NEw.Manufacturer_Name = obj.Manufacturer_Name;
                    NEw.Code              = obj.Code;

                    AllDataSet.Manufacturer.AddManufacturerRow(NEw);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load_Auth_Type(Configs AllDataSet)
        {
            try
            {
                var auth_type = _entityModel.Authentication_Type_Data.ToList()
                                          .OrderBy(m => m.Authentication_Type_Name);

                foreach (authentication_type obj in auth_type)
                {
                    Configs.Authentication_TypeRow NEw = (Configs.Authentication_TypeRow)AllDataSet.Authentication_Type.NewRow();

                    NEw.id                       = obj.Id;
                    NEw.Authentication_Type_Name = obj.Authentication_Type_Name;

                    AllDataSet.Authentication_Type.AddAuthentication_TypeRow(NEw);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load_Devices(Configs AllDataSet)
        {
            try
            {
                var devices = _entityModel.DevicesData.ToList()
                                           .OrderBy(m => m.Device_Name);

                foreach (device obj in devices)
                {
                    Configs.DeviceRow NEw = (Configs.DeviceRow)AllDataSet.Device.NewRow();

                    NEw.id              = obj.id;
                    NEw.Device_Name     = obj.Device_Name;
                    NEw.Model           = obj.Model;
                    NEw.Manufacturer_Id = obj.Manufacturer_Id;
                    NEw.IsSinglePhase   = obj.IsSinglePhase;
                    NEw.Accuracy        = obj.Accuracy;
                    NEw.Product         = obj.Product;

                    AllDataSet.Device.AddDeviceRow(NEw);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load_Devices_Associations(Configs AllDataSet)
        {
            try
            {
                var devices_associations = _entityModel.Device_Associations_Data.ToList()
                                           .OrderBy(m => m.Association_Name);

                foreach (device_association obj in devices_associations)
                {
                    Configs.Device_AssociationRow NEw = (Configs.Device_AssociationRow)AllDataSet.Device_Association.NewRow();

                    NEw.id                     = obj.id;
                    NEw.Association_Name       = obj.Association_Name;
                    NEw.Authentication_Type_Id = obj.Authentication_Type_Id;
                    NEw.Client_Sap             = obj.Client_Sap;
                    NEw.Meter_Sap              = obj.Meter_Sap;
                    NEw.Device_Id              = obj.Device_Id;
                    NEw.Configuration_Id       = obj.Configuration_Id;
                    NEw.ObisRightGroupId       = obj.ObisRightGroupId;
                    NEw.Reload_Config          = obj.Reload_Config ?? 0;
                    NEw.Association_Index      = obj.Association_Index;

                    AllDataSet.Device_Association.AddDevice_AssociationRow(NEw);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load_All_Quan(Configs AllDataSet)
        {
            try
            {
                var all_quan = _entityModel.AllQuantitiesData.ToList()
                                          .OrderBy(m => m.Label);

                foreach (all_quantities obj in all_quan)
                {
                    Configs.AllQuantitiesRow NEw = (Configs.AllQuantitiesRow)AllDataSet.AllQuantities.NewRow();
                    
                    NEw.id              = obj.id;
                    NEw.OBIS_Index      = obj.OBIS_Index;
                    NEw.Label           = obj.Label;
                    NEw.Dp_Name         = obj.DP_Name ?? string.Empty;
                    NEw.Unit            = obj.Unit ?? string.Empty;
                    NEw.Priority        = obj.Priority ?? 0;
                    StOBISCode OBISCode = (Get_Index)NEw.OBIS_Index;
                    NEw.Quantity_Code   = OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                    NEw.Quantity_Name   = OBISCode.OBISIndex.ToString();

                    AllDataSet.AllQuantities.AddAllQuantitiesRow(NEw);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Filter_All_Quan(string textToSearch, Configs AllDataSet)
        {
            try
            {
                var all_quan = _entityModel.AllQuantitiesData.ToList().Where(i => i.Label.Contains(textToSearch));

                if (all_quan != null)
                {
                    foreach (all_quantities obj in all_quan)
                    {
                        Configs.AllQuantitiesRow NEw = (Configs.AllQuantitiesRow)AllDataSet.AllQuantities.NewRow();
                        
                        NEw.OBIS_Index      = obj.OBIS_Index;
                        NEw.Label           = obj.Label;
                        NEw.Dp_Name         = obj.DP_Name ?? string.Empty;
                        NEw.Unit            = obj.Unit ?? string.Empty;
                        NEw.Priority        = obj.Priority ?? 0;
                        StOBISCode OBISCode = (Get_Index)NEw.OBIS_Index;
                        NEw.Quantity_Code   = OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                        NEw.Quantity_Name   = OBISCode.OBISIndex.ToString();

                        AllDataSet.AllQuantities.AddAllQuantitiesRow(NEw);
                    } 
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load_Obis_Details(Configs AllDataSet)
        {
            try
            {
                var obis_detail = _entityModel.ObisDetails_Data.ToList()
                                         .OrderBy(m => m.id);

                foreach (obis_details obj in obis_detail)
                {
                    Configs.OBIS_DetailsRow NEw = (Configs.OBIS_DetailsRow)AllDataSet.OBIS_Details.NewRow();

                    NEw.id                = obj.id;
                    NEw.Obis_Code         = obj.Obis_Code;
                    NEw.Default_OBIS_Code = obj.Default_OBIS_Code;
                    NEw.Device_Id         = obj.Device_Id;
                    StOBISCode OBISCode   = (Get_Index)NEw.Obis_Code;
                    NEw.OBIS_Quantity     = OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);

                    AllDataSet.OBIS_Details.AddOBIS_DetailsRow(NEw);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load_Capture_Obj(Configs AllDataSet)
        {
            try
            {
                 var capture_obj = _entityModel.Capture_Objects_Data.ToList()
                                       .OrderBy(m => m.id).ThenBy(x => x.Target_OBIS_Index);

                foreach (capture_objects obj in capture_obj)
                {

                    Configs.CaptureObjectsRow NEw = (Configs.CaptureObjectsRow)AllDataSet.CaptureObjects.NewRow();

                    NEw.id                = obj.id;
                    NEw.SequenceId        = obj.SequenceId;
                    NEw.OBIS_Index        = obj.OBIS_Index;
                    NEw.AttributeNo       = obj.AttributeNo;
                    NEw.DataIndex         = (ulong)obj.DataIndex;
                    NEw.ConfigId          = obj.ConfigId ?? 0;
                    NEw.GroupId           = obj.GroupId ?? 0;
                    NEw.Target_OBIS_Index = obj.Target_OBIS_Index;
                    NEw.DeviceId          = obj.DeviceId ?? 0;
                    NEw.databasefield     = obj.databasefield;
                    NEw.Multiplier        = Convert.ToInt16(obj.Multiplier ?? 0);

                    AllDataSet.CaptureObjects.AddCaptureObjectsRow(NEw);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load_LP_Channels_Data(Configs AllDataSet)
        {
            try
            {
                var lp_channels_data = _entityModel.LoadProfileChannels_Data.ToList()
                                     .OrderBy(m => m.id).ThenBy(x => x.QuantityIndex).ThenBy(x => x.SequenceId);

                foreach (load_profile_channels obj in lp_channels_data)
                {
                    Configs.LoadProfileChannelsRow NEw = (Configs.LoadProfileChannelsRow)AllDataSet.LoadProfileChannels.NewRow();

                    NEw.id                 = obj.id;
                    NEw.Label              = obj.Label;
                    NEw.QuantityIndex      = obj.QuantityIndex;
                    NEw.AttributeNo        = obj.AttributeNo ?? 0;
                    NEw.Multiplier         = Convert.ToInt16(obj.Multiplier ?? 0);
                    NEw.SequenceId         = Convert.ToInt16(obj.SequenceId ?? 0);
                    NEw.FormatSpecifier    = obj.FormatSpecifier;
                    NEw.Unit               = obj.Unit;
                    NEw.LoadProfileGroupId = obj.LoadProfileGroupId;

                    AllDataSet.LoadProfileChannels.AddLoadProfileChannelsRow(NEw);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load_Bill_Items(Configs AllDataSet)
        {
            try
            {
                var bill_items = _entityModel.BillingItems_Data.ToList()
                                   .OrderBy(m => m.id).ThenBy(x => x.BillItemGroupId);

                foreach (billing_items obj in bill_items)
                {
                    Configs.BillingItemsRow NEw = (Configs.BillingItemsRow)AllDataSet.BillingItems.NewRow();

                    NEw.id              = obj.id;
                    NEw.Label           = obj.Label;
                    NEw.FormatSpecifier = obj.FormatSpecifier;
                    NEw.Unit            = obj.Unit;
                    NEw.Multiplier      = Convert.ToInt16(obj.Multiplier ?? 0);
                    NEw.SequenceId      = Convert.ToInt16(obj.SequenceId ?? 0);
                    NEw.BillItemGroupId = obj.BillItemGroupId;
                    NEw.ConfigId        = obj.ConfigId ?? 0;

                    AllDataSet.BillingItems.AddBillingItemsRow(NEw);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load_Display_Windows(Configs AllDataSet)
        {
            try
            {
                var display_win = _entityModel.Display_Windows_Data.ToList()
                  .OrderBy(m => m.id).ThenBy(x => x.QuantityIndex).ThenBy(x => x.SequenceId);

                foreach (display_windows obj in display_win)
                {
                    Configs.DisplayWindowsRow NEw = (Configs.DisplayWindowsRow)AllDataSet.DisplayWindows.NewRow();

                    NEw.id                    = obj.id;
                    NEw.Category              = obj.Category;
                    NEw.Label                 = obj.Label;
                    NEw.AttributeNo           = obj.AttributeNo ?? 0;
                    NEw.WinNumberToDisplay    = Convert.ToInt16(obj.WinNumberToDisplay ?? 0);
                    NEw.QuantityIndex         = obj.QuantityIndex;
                    NEw.SequenceId            = Convert.ToInt16(obj.SequenceId ?? 0);
                    NEw.DisplayWindowsGroupId = obj.DisplayWindowsGroupId ?? 0;
                    NEw.ConfigId              = obj.ConfigId ?? 0;

                    AllDataSet.DisplayWindows.AddDisplayWindowsRow(NEw);
                }
            }
            catch (Exception ex)
            {
                //skipped because it has not been implemented in MDC
            }
        }

        public void Load_Bill_Tariff_Quan(Configs AllDataSet)
        {
            try
            {
                var bill_tariff_quan = _entityModel.BillTariffQuantity_Data.ToList()
                      .OrderBy(m => m.BillItemId);

                foreach (bill_tariff_quantity obj in bill_tariff_quan)
                {
                    Configs.BillTariffQuantityRow NEw = (Configs.BillTariffQuantityRow)AllDataSet.BillTariffQuantity.NewRow();

                    NEw.BillItemId    = obj.BillItemId;
                    NEw.OBIS_Index    = obj.OBIS_Index;
                    NEw.SequenceId    = Convert.ToInt16(obj.SequenceId ?? 0);
                    NEw.DatabaseField = obj.DatabaseField;

                    AllDataSet.BillTariffQuantity.AddBillTariffQuantityRow(NEw);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load_Event_Info_Data(Configs AllDataSet)
        {
            try
            {
                var ev_info_data = _entityModel.Events_Info_Data.ToList().OrderBy(m => m.id);
                //var ev_info_data = _entityModel.Events_Info_Data.ToList().OrderBy(m => m.id).ThenBy(x => x.EventCode).ThenBy(x => x.EventGroupId);

                foreach (event_info obj in ev_info_data)
                {
                    Configs.EventInfoRow NEw = (Configs.EventInfoRow)AllDataSet.EventInfo.NewRow();

                    NEw.id            = obj.id;
                    NEw.EventCode     = obj.EventCode;
                    NEw.Label         = obj.Label;
                    NEw.MaxEventCount = obj.MaxEventCount;
                    NEw.EventNo       = Convert.ToInt16(obj.EventNo ?? 0);
                    NEw.EventGroupId  = obj.EventGroupId;
                    //NEw.CautionNumber = obj.CautionNumber;
                    //NEw.FlashTime = obj.FlashTime;
                    NEw.ConfigId      = obj.ConfigId ?? 0;

                    AllDataSet.EventInfo.AddEventInfoRow(NEw);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load_Event_Logs_Data(Configs AllDataSet)
        {
            try
            {
                var ev_logs_data = _entityModel.Event_Logs_Data.ToList()
                     .OrderBy(m => m.id).ThenBy(x => x.EventLogIndex).ThenBy(x => x.EventGroupId);

                foreach (event_logs obj in ev_logs_data)
                {
                    Configs.EventLogsRow NEw = (Configs.EventLogsRow)AllDataSet.EventLogs.NewRow();
                    NEw.id_pk             = obj.id_pk;
                    NEw.id                = obj.id;
                    NEw.EventLogIndex     = Convert.ToDecimal(obj.EventLogIndex ?? 0);
                    NEw.EventCounterIndex = Convert.ToDecimal(obj.EventCounterIndex ?? 0);
                    NEw.EventGroupId      = obj.EventGroupId;
                    NEw.ConfigId          = obj.ConfigId ?? 0;

                    AllDataSet.EventLogs.AddEventLogsRow(NEw);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load_Obis_Rights(Configs AllDataSet)
        {
            try
            {
                var obis_rights = _entityModel.Obis_Rights_Data.ToList()
                     .OrderBy(m => m.id).ThenBy(x => x.OBIS_Index);

                foreach (obis_rights obj in obis_rights)
                {
                    Configs.OBIS_RightsRow NEw = (Configs.OBIS_RightsRow)AllDataSet.OBIS_Rights.NewRow();

                    NEw.id               = (int)obj.id;
                    NEw.OBIS_Index       = obj.OBIS_Index;
                    NEw.Version          = obj.Version ?? 0;
                    NEw.ObisRightGroupId = obj.ObisRightGroupId ?? 0;
                    NEw.ClientSAPId      = obj.ClientSapId ?? 0;
                    NEw.ServerSAPId      = obj.ServerSapId ?? 0;

                    AllDataSet.OBIS_Rights.AddOBIS_RightsRow(NEw);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load_Rights(Configs AllDataSet)
        {
            try
            {
                var rights = _entityModel.Rights_Data.ToList()
                     .OrderBy(m => m.id).ThenBy(x => x.OBIS_Right_Id);

                foreach (rights obj in rights)
                {
                    Configs.RightsRow NEw = (Configs.RightsRow)AllDataSet.Rights.NewRow();

                    NEw.id            = (int)obj.id;
                    NEw.type          = obj.type;
                    NEw.SubId         = obj.SubId;
                    NEw.value         = obj.value;
                    NEw.OBIS_Right_Id = (int)obj.OBIS_Right_Id;

                    AllDataSet.Rights.AddRightsRow(NEw);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load_StatusWord(Configs AllDataSet)
        {
            try
            {
                var statusWord = _entityModel.StatusWord_Data.ToList()
                               .OrderBy(m => m.Code);

                foreach (status_word obj in statusWord)
                {
                    Configs.Status_WordRow NEw = (Configs.Status_WordRow)AllDataSet.Status_Word.NewRow();

                    NEw.Code = obj.Code;
                    NEw.Name = obj.Name;

                    AllDataSet.Status_Word.AddStatus_WordRow(NEw);
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                //skipped because it has not been implemented in MDC
            }
        }
        
        public void Load_All_Configurations(Configs AllDataSet)
        {
            try
            {
                // For Data Loading Set Configs DataSET 
                if (AllDataSet == null)
                    AllDataSet = new Configs();
                AllDataSet.Clear();

                foreach (var tableLoadMethod in Load_All_Configs)
                    tableLoadMethod(AllDataSet);

                #region Commented Code
                /*
                var obis_right_group = _entityModel.Obis_Rights_Group_Data.ToList()
                               .OrderBy(m => m.id);

                foreach (obis_rights_group obj in obis_right_group)
                {
                    Configs.Obis_Rights_GroupRow NEw = (Configs.Obis_Rights_GroupRow)AllDataSet.Obis_Rights_Group.NewRow();
                    NEw.id = obj.id;
                    NEw.Group_Name = obj.Group_Name;
                    NEw.Update_Rights = obj.Update_Rights;
                    AllDataSet.Obis_Rights_Group.AddObis_Rights_GroupRow(NEw);
                }


                var lp_group_data = _entityModel.LoadProfile_Group_Data.ToList()
                                      .OrderBy(m => m.id);

                foreach (loadprofile_group obj in lp_group_data)
                {
                    Configs.LoadProfile_GroupRow NEw = (Configs.LoadProfile_GroupRow)AllDataSet.LoadProfile_Group.NewRow();
                    NEw.id = obj.id;
                    NEw.LoadProfile_Group_Name = obj.LoadProfile_Group_Name;
                    AllDataSet.LoadProfile_Group.AddLoadProfile_GroupRow(NEw);
                }



                var ev_groups = _entityModel.Events_Group_Data.ToList()
                                      .OrderBy(m => m.id);

                foreach (events_group obj in ev_groups)
                {
                    Configs.Events_GroupRow NEw = (Configs.Events_GroupRow)AllDataSet.Events_Group.NewRow();
                    NEw.id = obj.id;
                    NEw.Events_group_Name = obj.Events_Group_Name;
                    AllDataSet.Events_Group.AddEvents_GroupRow(NEw);
                }



                var bill_item_group = _entityModel.BillingItem_Group_Data.ToList()
                                     .OrderBy(m => m.id);

                foreach (billingitem_group obj in bill_item_group)
                {
                    Configs.BillingItem_GroupRow NEw = (Configs.BillingItem_GroupRow)AllDataSet.BillingItem_Group.NewRow();
                    NEw.id = obj.id;
                    NEw.BillingItem_Group_Name = obj.BillingItem_Group_Name;
                    AllDataSet.BillingItem_Group.AddBillingItem_GroupRow(NEw);
                }


                var display_win_group = _entityModel.DisplayWindows_Group_Data.ToList()
                                    .OrderByDescending(m => m.Dw_Group_Name);

                foreach (displaywindows_group obj in display_win_group)
                {
                    Configs.DisplayWindows_GroupRow NEw = (Configs.DisplayWindows_GroupRow)AllDataSet.DisplayWindows_Group.NewRow();
                    NEw.id = obj.id;
                    NEw.Dw_Group_Name = obj.Dw_Group_Name;
                    AllDataSet.DisplayWindows_Group.AddDisplayWindows_GroupRow(NEw);
                }


                var configs = _entityModel.Configurations_Data.ToList()
                                    .OrderByDescending(m => m.Name);

                foreach (configuration_new obj in configs)
                {
                    Configs.ConfigurationRow NEw = (Configs.ConfigurationRow)AllDataSet.Configuration.NewRow();
                    NEw.id = obj.id;
                    NEw.Name = obj.Name;
                    NEw.lp_channels_group_id = obj.LP_Channels_Group_ID;
                    NEw.BillItemsGroupId = obj.BillItemsGroupId;
                    NEw.EventGroupId = obj.EventGroupId;
                    NEw.DisplayWindowGroupId = obj.DisplayWindowGroupId ?? 0;
                    AllDataSet.Configuration.AddConfigurationRow(NEw);
                }


                var manufac_data = _entityModel.Manufacturer_Data.ToList()
                                   .OrderByDescending(m => m.id);

                foreach (manufacturer obj in manufac_data)
                {
                    Configs.ManufacturerRow NEw = (Configs.ManufacturerRow)AllDataSet.Manufacturer.NewRow();
                    NEw.id = obj.id;
                    NEw.Manufacturer_Name = obj.Manufacturer_Name;
                    NEw.Code = obj.Code;
                    AllDataSet.Manufacturer.AddManufacturerRow(NEw);
                }


                var auth_type = _entityModel.Authentication_Type_Data.ToList()
                                  .OrderBy(m => m.Authentication_Type_Name);

                foreach (authentication_type obj in auth_type)
                {
                    Configs.Authentication_TypeRow NEw = (Configs.Authentication_TypeRow)AllDataSet.Authentication_Type.NewRow();
                    NEw.id = obj.Id;
                    NEw.Authentication_Type_Name = obj.Authentication_Type_Name;
                    AllDataSet.Authentication_Type.AddAuthentication_TypeRow(NEw);
                }


                var devices = _entityModel.DevicesData.ToList()
                                   .OrderBy(m => m.Device_Name);

                foreach (device obj in devices)
                {
                    Configs.DeviceRow NEw = (Configs.DeviceRow)AllDataSet.Device.NewRow();
                    NEw.id = obj.id;
                    NEw.Device_Name = obj.Device_Name;
                    NEw.IsSinglePhase = obj.IsSinglePhase;
                    NEw.Model = obj.Model;
                    NEw.Manufacturer_Id = obj.Manufacturer_Id;
                    NEw.Accuracy = obj.Accuracy;
                    NEw.Product = obj.Product;
                    AllDataSet.Device.AddDeviceRow(NEw);
                }


                var devices_associations = _entityModel.Device_Associations_Data.ToList()
                                   .OrderBy(m => m.Association_Name);

                foreach (device_association obj in devices_associations)
                {
                    Configs.Device_AssociationRow NEw = (Configs.Device_AssociationRow)AllDataSet.Device_Association.NewRow();
                    NEw.id = obj.id;
                    NEw.Association_Name = obj.Association_Name;
                    NEw.Authentication_Type_Id = obj.Authentication_Type_Id;
                    NEw.Configuration_Id = obj.Configuration_Id;
                    NEw.Device_Id = obj.Device_Id;
                    NEw.ObisRightGroupId = obj.ObisRightGroupId;
                    NEw.Client_Sap = obj.Client_Sap;
                    NEw.Meter_Sap = obj.Meter_Sap;
                    NEw.Reload_Config = obj.Reload_Config ?? 0;
                    NEw.Association_Index = obj.Association_Index;
                    AllDataSet.Device_Association.AddDevice_AssociationRow(NEw);
                }


                var all_quan = _entityModel.AllQuantitiesData.ToList()
                                  .OrderBy(m => m.Label);

                foreach (all_quantities obj in all_quan)
                {
                    Configs.AllQuantitiesRow NEw = (Configs.AllQuantitiesRow)AllDataSet.AllQuantities.NewRow();
                    NEw.Label = obj.Label;
                    NEw.OBIS_Index = obj.OBIS_Index;
                    NEw.Dp_Name = obj.DP_Name;
                    NEw.Unit = obj.Unit;
                    NEw.Priority = obj.Priority ?? 0;
                    StOBISCode OBISCode = (Get_Index)NEw.OBIS_Index;
                    NEw.Quantity_Code = OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                    NEw.Quantity_Name = OBISCode.OBISIndex.ToString();
                    AllDataSet.AllQuantities.AddAllQuantitiesRow(NEw);
                }


                var obis_deta = _entityModel.ObisDetails_Data.ToList()
                                 .OrderBy(m => m.id);

                foreach (obis_details obj in obis_deta)
                {
                    Configs.OBIS_DetailsRow NEw = (Configs.OBIS_DetailsRow)AllDataSet.OBIS_Details.NewRow();
                    NEw.id = obj.id;
                    NEw.Obis_Code = obj.Obis_Code;
                    NEw.Default_OBIS_Code = obj.Default_OBIS_Code;
                    NEw.Device_Id = obj.Device_Id;
                    StOBISCode OBISCode = (Get_Index)NEw.Obis_Code;
                    NEw.OBIS_Quantity = OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                    AllDataSet.OBIS_Details.AddOBIS_DetailsRow(NEw);
                }
                //# SequenceId, OBIS_Index,          AttributeNo,   DataIndex,      ConfigId,       GroupId,    Target_OBIS_Index,      DeviceId
                //       1,      2251799830462719,       2,               0,         111,            201,        1971426009612543,        4

                // capture_obj.Count()
                var capture_obj = _entityModel.Capture_Objects_Data
                               .OrderBy(m => m.id).ThenBy(x => x.Target_OBIS_Index).Distinct().ToList();
                //.Where( x=> x.GroupId == 201 && x.DeviceId == 4 )
                AllDataSet.CaptureObjects.Clear();
                int index = 0;
                foreach (capture_objects obj in capture_obj)
                {

                    Configs.CaptureObjectsRow NEw = (Configs.CaptureObjectsRow)AllDataSet.CaptureObjects.NewRow();

                    NEw.SequenceId = obj.SequenceId;
                    NEw.OBIS_Index = obj.OBIS_Index;
                    NEw.Target_OBIS_Index = obj.Target_OBIS_Index;
                    NEw.AttributeNo = obj.AttributeNo;
                    NEw.DataIndex = (ulong)obj.DataIndex;
                    //NEw.DatabaseField = obj.DatabaseField ?? "";

                    NEw.DeviceId = obj.DeviceId ?? 0;
                    NEw.GroupId = obj.GroupId ?? 0;
                    NEw.ConfigId = obj.ConfigId ?? 0;
                    if (AllDataSet.CaptureObjects.Select($"GroupId={NEw.GroupId} and DeviceId={NEw.DeviceId} and Target_OBIS_Index={NEw.Target_OBIS_Index} and OBIS_Index={NEw.OBIS_Index}").Count() != 0)
                    {
                        Console.WriteLine($"GroupId={NEw.GroupId} and DeviceId={NEw.DeviceId} and Target_OBIS_Index={NEw.Target_OBIS_Index} and OBIS_Index={NEw.OBIS_Index}");
                    }
                    else
                        AllDataSet.CaptureObjects.AddCaptureObjectsRow(NEw);
                    index++;
                }


                var lp_channels_data = _entityModel.LoadProfileChannels_Data.ToList()
                             .OrderBy(m => m.id).ThenBy(x => x.QuantityIndex).ThenBy(x => x.SequenceId);

                foreach (load_profile_channels obj in lp_channels_data)
                {
                    Configs.LoadProfileChannelsRow NEw = (Configs.LoadProfileChannelsRow)AllDataSet.LoadProfileChannels.NewRow();
                    NEw.id = obj.id;
                    NEw.SequenceId = Convert.ToInt16(obj.SequenceId ?? 0);
                    NEw.QuantityIndex = obj.QuantityIndex;
                    NEw.LoadProfileGroupId = obj.LoadProfileGroupId;
                    NEw.AttributeNo = obj.AttributeNo ?? 0;
                    NEw.FormatSpecifier = obj.FormatSpecifier;
                    NEw.Multiplier = Convert.ToInt16(obj.Multiplier ?? 0);
                    NEw.Unit = obj.Unit;
                    AllDataSet.LoadProfileChannels.AddLoadProfileChannelsRow(NEw);
                }


                var bill_items = _entityModel.BillingItems_Data.ToList()
                           .OrderBy(m => m.id).ThenBy(x => x.BillItemGroupId);

                foreach (billing_items obj in bill_items)
                {
                    Configs.BillingItemsRow NEw = (Configs.BillingItemsRow)AllDataSet.BillingItems.NewRow();
                    NEw.id = obj.id;
                    NEw.SequenceId = Convert.ToInt16(obj.SequenceId ?? 0);
                    NEw.BillItemGroupId = obj.BillItemGroupId;
                    NEw.Label = obj.Label;
                    NEw.FormatSpecifier = obj.FormatSpecifier;
                    NEw.Multiplier = Convert.ToInt16(obj.Multiplier ?? 0);
                    NEw.Unit = obj.Unit;
                    NEw.ConfigId = obj.ConfigId ?? 0;
                    AllDataSet.BillingItems.AddBillingItemsRow(NEw);
                }


                try
                {
                    var display_win = _entityModel.Display_Windows_Data.ToList()
                      .OrderBy(m => m.id).ThenBy(x => x.QuantityIndex).ThenBy(x => x.SequenceId);

                    foreach (display_windows obj in display_win)
                    {
                        Configs.DisplayWindowsRow NEw = (Configs.DisplayWindowsRow)AllDataSet.DisplayWindows.NewRow();
                        NEw.id = obj.id;
                        NEw.SequenceId = Convert.ToInt16(obj.SequenceId ?? 0);
                        NEw.QuantityIndex = obj.QuantityIndex;
                        NEw.Category = obj.Category;
                        NEw.AttributeNo = obj.AttributeNo ?? 0;
                        NEw.WinNumberToDisplay = Convert.ToInt16(obj.WinNumberToDisplay ?? 0);
                        NEw.DisplayWindowsGroupId = obj.DisplayWindowsGroupId ?? 0;
                        NEw.ConfigId = obj.ConfigId ?? 0;
                        AllDataSet.DisplayWindows.AddDisplayWindowsRow(NEw);
                    }
                }
                catch (Exception ex)
                {
                    //skipped because it has not been implemented in MDC
                }


                var bill_tariff_quan = _entityModel.BillTariffQuantity_Data.ToList()
                          .OrderBy(m => m.BillItemId);

                foreach (bill_tariff_quantity obj in bill_tariff_quan)
                {
                    Configs.BillTariffQuantityRow NEw = (Configs.BillTariffQuantityRow)AllDataSet.BillTariffQuantity.NewRow();
                    NEw.BillItemId = obj.BillItemId;
                    NEw.SequenceId = Convert.ToInt16(obj.SequenceId ?? 0);
                    NEw.OBIS_Index = obj.OBIS_Index;
                    NEw.DatabaseField = obj.DatabaseField;
                    AllDataSet.BillTariffQuantity.AddBillTariffQuantityRow(NEw);
                }


                var ev_info_data = _entityModel.Events_Info_Data.ToList()
                          .OrderBy(m => m.id).ThenBy(x => x.EventCode).ThenBy(x => x.EventGroupId);

                foreach (event_info obj in ev_info_data)
                {
                    Configs.EventInfoRow NEw = (Configs.EventInfoRow)AllDataSet.EventInfo.NewRow();
                    NEw.id = obj.id;
                    NEw.EventCode = obj.EventCode;
                    NEw.EventNo = Convert.ToInt16(obj.EventNo ?? 0);
                    NEw.EventGroupId = obj.EventGroupId;
                    NEw.MaxEventCount = obj.MaxEventCount;
                    NEw.ConfigId = obj.ConfigId ?? 0;
                    NEw.Label = obj.Label;
                    AllDataSet.EventInfo.AddEventInfoRow(NEw);
                }


                var ev_logs_data = _entityModel.Event_Logs_Data.ToList()
                         .OrderBy(m => m.id).ThenBy(x => x.EventLogIndex).ThenBy(x => x.EventGroupId);

                foreach (event_logs obj in ev_logs_data)
                {
                    Configs.EventLogsRow NEw = (Configs.EventLogsRow)AllDataSet.EventLogs.NewRow();
                    NEw.id = obj.id;
                    NEw.EventLogIndex = obj.EventLogIndex;
                    NEw.EventCounterIndex = obj.EventCounterIndex;
                    NEw.EventGroupId = obj.EventGroupId;
                    NEw.ConfigId = obj.ConfigId ?? 0;
                    AllDataSet.EventLogs.AddEventLogsRow(NEw);
                }


                var obis_rights = _entityModel.Obis_Rights_Data.ToList()
                         .OrderBy(m => m.id).ThenBy(x => x.OBIS_Index);

                foreach (obis_rights obj in obis_rights)
                {
                    Configs.OBIS_RightsRow NEw = (Configs.OBIS_RightsRow)AllDataSet.OBIS_Rights.NewRow();
                    NEw.id = (int)obj.id;
                    NEw.OBIS_Index = obj.OBIS_Index;
                    NEw.ObisRightGroupId = obj.ObisRightGroupId ?? 0;
                    NEw.Version = obj.Version ?? 0;
                    NEw.ClientSAPId = obj.ClientSapId ?? 0;
                    NEw.ServerSAPId = obj.ServerSapId ?? 0;
                    AllDataSet.OBIS_Rights.AddOBIS_RightsRow(NEw);
                }


                var rights = _entityModel.Rights_Data.ToList()
                         .OrderBy(m => m.id).ThenBy(x => x.OBIS_Right_Id);

                foreach (rights obj in rights)
                {
                    Configs.RightsRow NEw = (Configs.RightsRow)AllDataSet.Rights.NewRow();
                    NEw.id = (int)obj.id;
                    NEw.type = obj.type;
                    NEw.SubId = obj.SubId;
                    NEw.value = obj.value;
                    NEw.OBIS_Right_Id = (int)obj.OBIS_Right_Id;
                    AllDataSet.Rights.AddRightsRow(NEw);
                }


                try
                {
                    var statusWord = _entityModel.StatusWord_Data.ToList()
                                   .OrderBy(m => m.Code);

                    foreach (status_word obj in statusWord)
                    {
                        Configs.Status_WordRow NEw = (Configs.Status_WordRow)AllDataSet.Status_Word.NewRow();
                        NEw.Code = obj.Code;
                        NEw.Name = obj.Name;
                        AllDataSet.Status_Word.AddStatus_WordRow(NEw);
                    }
                }
                catch (Exception ex)
                {
                    //skipped because it has not been implemented in MDC
                }


                //var lp_group_data = model.LoadProfile_Group_Data.ToList();
                //var ev_groups = model.Events_Group_Data.ToList();
                //var bill_item_group = model.BillingItem_Group_Data.ToList();
                //var display_win_group = model.DisplayWindows_Group_Data.ToList();
                //var configs = model.Configurations_Data.ToList();
                //var manufac_data = model.Manufacturer_Data.ToList();
                //var auth_type = model.Authentication_Type_Data.ToList();
                //var devices = model.DevicesData.ToList();
                //var devices_associations = model.Device_Associations_Data.ToList();
                //var all_quan = model.AllQuantitiesData.ToList();
                //var obis_deta = model.ObisDetails_Data.ToList();
                //var capture_obj = model.Capture_Objects_Data.ToList();
                //var lp_channels_data = model.LoadProfileChannels_Data.ToList();
                //var bill_items = model.BillingItems_Data.ToList();
                //var display_win = model.Display_Windows_Data.ToList();
                //var bill_tariff_quan = model.BillTariffQuantity_Data.ToList();
                //var ev_info_data = model.Events_Info_Data.ToList();
                //var ev_logs_data = model.Event_Logs_Data.ToList();
                //var obis_rights = model.Obis_Rights_Data.ToList();
                //var rights = model.Rights_Data.ToList();
                //var statusWord = model.StatusWord_Data.ToList(); 
                */
                #endregion
            }
            catch (Exception ex)
            {
                //throw new Exception("Error Loading Meter Configurations," + ex.Message, ex);
                this.LogException("Load_All_Configurations()", ex);
            }
            finally
            {
                AllDataSet.AcceptChanges();
            }
        }
        private void LogException(string method, Exception ex)
        {
            string fileName = "DBConfigExceptions.txt";

            string message = $"{DateTime.Now.ToString()} : {method} => Exception : {ex.GetBaseException().Message}{Environment.NewLine} => StackTrace : {ex.StackTrace}{Environment.NewLine}";
            string Line = $"============================================================={Environment.NewLine}";

            File.AppendAllText(fileName, message);
            File.AppendAllText(fileName, Line);

        }

        #endregion // Load_DB_Methods

        #region Update_DB_Methods
        private void Init_Update_All_Configs()
        {
            try
            {
                Configs con = new Configs();
                
                Update_All_Configs.Add(con.AllQuantities.TableName, this.AcceptChangesAllQuantities);
                Update_All_Configs.Add(con.Authentication_Type.TableName,	this.AcceptChangesAuthenticationType);
                Update_All_Configs.Add(con.BillingItem_Group.TableName,	this.AcceptChangesBillingGroup);
                Update_All_Configs.Add(con.BillingItems.TableName,	this.AcceptChangesBillingItems);
                Update_All_Configs.Add(con.BillTariffQuantity.TableName,	this.AcceptChangesBillTariffQuantity);
                Update_All_Configs.Add(con.Configuration.TableName,	this.AcceptChangesConfigurations);
                Update_All_Configs.Add(con.Device.TableName,	this.AcceptChangesDevice);
                Update_All_Configs.Add(con.Device_Association.TableName,	this.AcceptChangesDeviceAssociation);
                Update_All_Configs.Add(con.DisplayWindows.TableName,	this.AcceptChangesDisplayWindows);
                Update_All_Configs.Add(con.DisplayWindows_Group.TableName,	this.AcceptChangesDisplayWindowsGroup);
                Update_All_Configs.Add(con.EventInfo.TableName,	this.AcceptChangesEventsInfo);
                Update_All_Configs.Add(con.EventLogs.TableName,	this.AcceptChangesEventsLogs);
                Update_All_Configs.Add(con.Events_Group.TableName,	this.AcceptChangesEventsGroup);
                Update_All_Configs.Add(con.LoadProfileChannels.TableName,	this.AcceptChangesLoadProfileChannels);
                Update_All_Configs.Add(con.LoadProfile_Group.TableName,	this.AcceptChangesLoadProfileGroup);
                Update_All_Configs.Add(con.Manufacturer.TableName,	this.AcceptChangesManufacturer);
                Update_All_Configs.Add(con.OBIS_Details.TableName,	this.AcceptChangesObisDetails);
                Update_All_Configs.Add(con.OBIS_Rights.TableName,	this.AcceptChangesObisRights);
                Update_All_Configs.Add(con.Obis_Rights_Group.TableName,	this.AcceptChangesObisRightsGroup);
                Update_All_Configs.Add(con.Rights.TableName,	this.AcceptChangesRights);
                Update_All_Configs.Add(con.users.TableName,	this.AcceptChangesUsers);
                Update_All_Configs.Add(con.CaptureObjects.TableName,	this.AcceptChangesCaptureObjects);
                Update_All_Configs.Add(con.Status_Word.TableName,	this.AcceptChangesStatusWord);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save_N_Load_All_Configs(Configs AllDataSet)
        {
            try
            {
                // Save Configs to db
                this.Update_All_Configuration(AllDataSet);
                AllDataSet.Clear();
                
                this.Load_All_Configurations(AllDataSet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_All_Configuration(Configs AllDataSet)
        {
            // DataContext DtTransaction = null;
            try
            {
                Configs Dt = new Configs();
                AllDataSet.EnforceConstraints = false;
                Dt.EnforceConstraints = false;
                Dt = (Configs)AllDataSet.GetChanges();
                

                this.AcceptChangesEventsGroup(Dt);
                this.AcceptChangesBillingGroup(Dt);
                this.AcceptChangesLoadProfileGroup(Dt);
                this.AcceptChangesDisplayWindowsGroup(Dt);
                this.AcceptChangesObisRightsGroup(Dt);
                this.AcceptChangesConfigurations(Dt);
                this.AcceptChangesManufacturer(Dt);
                this.AcceptChangesAuthenticationType(Dt);
                this.AcceptChangesDevice(Dt);
                this.AcceptChangesDeviceAssociation(Dt);
                this.AcceptChangesAllQuantities(Dt);
                this.AcceptChangesObisDetails(Dt);
                this.AcceptChangesCaptureObjects(Dt);
                this.AcceptChangesObisRights(Dt);
                this.AcceptChangesRights(Dt);
                this.AcceptChangesLoadProfileChannels(Dt);
                this.AcceptChangesBillingItems(Dt);
                this.AcceptChangesBillTariffQuantity(Dt);
                this.AcceptChangesEventsInfo(Dt);
                this.AcceptChangesEventsLogs(Dt);
                this.AcceptChangesDisplayWindows(Dt);
                this.AcceptChangesStatusWord(Dt);

                this._entityModel.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Error Updating configurations,", ex);
            }
            finally
            {
            }
        }

        #region Events Group
        void AcceptChangesEventsGroup(Configs DataSet)
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

        void InsertEventsGroup(Configs.Events_GroupRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    events_group ev_group = new events_group();
                    ev_group.id = Row.id;
                    ev_group.Events_Group_Name = Row.Events_group_Name;
                    this._entityModel.Events_Group_Data.Add(ev_group);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving EventsGroup data in data source", ex);
            }
        }

        void UpdateEventsGroup(Configs.Events_GroupRow Row)
        {
            try
            {
                #region Update_Code
                if (Row.RowState == DataRowState.Modified)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    events_group ev_group = this._entityModel.Events_Group_Data.SingleOrDefault(x => x.id == id);
                    if (ev_group != null)
                    {
                        ev_group.Events_Group_Name = Row.Events_group_Name;
                        this._entityModel.Events_Group_Data.Attach(ev_group);
                        this._entityModel.Entry(ev_group).State = EntityState.Modified;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying EventsGroup data in data source", ex);
            }
        }

        void DeleteEventsGroup(Configs.Events_GroupRow Row)
        {
            try
            {
                #region Delete_Code

                if (Row.RowState == DataRowState.Deleted)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    events_group ev_group = this._entityModel.Events_Group_Data.SingleOrDefault(x => x.id == id);
                    if (ev_group != null)
                    {
                        this._entityModel.Events_Group_Data.Remove(ev_group);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting EventsGroup data in data source", ex);
            }
        }

        #endregion 

        #endregion

        #region Billing Group
        void AcceptChangesBillingGroup(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.BillingItem_GroupRow> Rows = DataSet.BillingItem_Group.ToList<Configs.BillingItem_GroupRow>();
                for (int index = 0; (Rows != null && index < Rows.Count); index++)
                {
                    Configs.BillingItem_GroupRow dtRow = Rows[index];
                    #region Insert_Code
                    // Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertBillingGroup(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateBillingGroup(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteBillingGroup(dtRow);
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

        void InsertBillingGroup(Configs.BillingItem_GroupRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    billingitem_group bill_group = new billingitem_group();
                    bill_group.id = Row.id;
                    bill_group.BillingItem_Group_Name = Row.BillingItem_Group_Name;
                    this._entityModel.BillingItem_Group_Data.Add(bill_group);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving BillingGroup data in data source", ex);
            }
        }

        void UpdateBillingGroup(Configs.BillingItem_GroupRow Row)
        {
            try
            {
                #region Update_Code
                if (Row.RowState == DataRowState.Modified)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    billingitem_group bill_group = this._entityModel.BillingItem_Group_Data.SingleOrDefault(x => x.id == id);
                    if (bill_group != null)
                    {
                        bill_group.BillingItem_Group_Name = Row.BillingItem_Group_Name;
                        this._entityModel.BillingItem_Group_Data.Attach(bill_group);
                        this._entityModel.Entry(bill_group).State = EntityState.Modified;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying BillingGroup data in data source", ex);
            }
        }

        void DeleteBillingGroup(Configs.BillingItem_GroupRow Row)
        {
            try
            {
                #region Delete_Code

                if (Row.RowState == DataRowState.Deleted)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    billingitem_group bill_group = this._entityModel.BillingItem_Group_Data.SingleOrDefault(x => x.id == id);
                    if (bill_group != null)
                    {
                        this._entityModel.BillingItem_Group_Data.Remove(bill_group);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting BillingGroup data in data source", ex);
            }
        }

        #endregion 

        #endregion

        #region LoadProfile Group
        void AcceptChangesLoadProfileGroup(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.LoadProfile_GroupRow> Rows = DataSet.LoadProfile_Group.ToList<Configs.LoadProfile_GroupRow>();
                for (int index = 0; (Rows != null && index < Rows.Count); index++)
                {
                    Configs.LoadProfile_GroupRow dtRow = Rows[index];
                    #region Insert_Code
                    // Exe Insert Query
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
                throw new Exception("Error Updating LoadProfileGroup", ex);
            }
        }

        #region CRUD_Methods

        void InsertLoadProfileGroup(Configs.LoadProfile_GroupRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    loadprofile_group lp_group = new loadprofile_group();
                    lp_group.id = Row.id;
                    lp_group.LoadProfile_Group_Name = Row.LoadProfile_Group_Name;
                    this._entityModel.LoadProfile_Group_Data.Add(lp_group);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving LoadProfileGroup data in data source", ex);
            }
        }

        void UpdateLoadProfileGroup(Configs.LoadProfile_GroupRow Row)
        {
            try
            {
                #region Update_Code
                if (Row.RowState == DataRowState.Modified)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    loadprofile_group lp_group = this._entityModel.LoadProfile_Group_Data.SingleOrDefault(x => x.id == id);
                    if (lp_group != null)
                    {
                        lp_group.LoadProfile_Group_Name = Row.LoadProfile_Group_Name;
                        this._entityModel.LoadProfile_Group_Data.Attach(lp_group);
                        this._entityModel.Entry(lp_group).State = EntityState.Modified;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying LoadProfileGroup data in data source", ex);
            }
        }

        void DeleteLoadProfileGroup(Configs.LoadProfile_GroupRow Row)
        {
            try
            {
                #region Delete_Code

                if (Row.RowState == DataRowState.Deleted)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    loadprofile_group lp_group = this._entityModel.LoadProfile_Group_Data.SingleOrDefault(x => x.id == id);
                    if (lp_group != null)
                    {
                        this._entityModel.LoadProfile_Group_Data.Remove(lp_group);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting LoadProfileGroup data in data source", ex);
            }
        }

        #endregion 

        #endregion

        #region DisplayWindows Group
        void AcceptChangesDisplayWindowsGroup(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.DisplayWindows_GroupRow> Rows = DataSet.DisplayWindows_Group.ToList<Configs.DisplayWindows_GroupRow>();
                for (int index = 0; (Rows != null && index < Rows.Count); index++)
                {
                    Configs.DisplayWindows_GroupRow dtRow = Rows[index];
                    #region Insert_Code
                    // Exe Insert Query
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
                throw new Exception("Error Updating DisplayWindowsGroup", ex);
            }
        }

        #region CRUD_Methods

        void InsertDisplayWindowsGroup(Configs.DisplayWindows_GroupRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    displaywindows_group dw_group = new displaywindows_group();
                    dw_group.id = Row.id;
                    dw_group.Dw_Group_Name = Row.Dw_Group_Name;
                    this._entityModel.DisplayWindows_Group_Data.Add(dw_group);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving DisplayWindowsGroup data in data source", ex);
            }
        }

        void UpdateDisplayWindowsGroup(Configs.DisplayWindows_GroupRow Row)
        {
            try
            {
                #region Update_Code
                if (Row.RowState == DataRowState.Modified)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    displaywindows_group dw_group = this._entityModel.DisplayWindows_Group_Data.SingleOrDefault(x => x.id == id);
                    if (dw_group != null)
                    {
                        dw_group.Dw_Group_Name = Row.Dw_Group_Name;
                        this._entityModel.DisplayWindows_Group_Data.Attach(dw_group);
                        this._entityModel.Entry(dw_group).State = EntityState.Modified;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying DisplayWindowsGroup data in data source", ex);
            }
        }

        void DeleteDisplayWindowsGroup(Configs.DisplayWindows_GroupRow Row)
        {
            try
            {
                #region Delete_Code

                if (Row.RowState == DataRowState.Deleted)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    displaywindows_group dw_group = this._entityModel.DisplayWindows_Group_Data.SingleOrDefault(x => x.id == id);
                    if (dw_group != null)
                    {
                        this._entityModel.DisplayWindows_Group_Data.Remove(dw_group);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting DisplayWindowsGroup data in data source", ex);
            }
        }

        #endregion 

        #endregion 

        #region ObisRights Group
        void AcceptChangesObisRightsGroup(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.Obis_Rights_GroupRow> Rows = DataSet.Obis_Rights_Group.ToList<Configs.Obis_Rights_GroupRow>();
                for (int index = 0; (Rows != null && index < Rows.Count); index++)
                {
                    Configs.Obis_Rights_GroupRow dtRow = Rows[index];
                    #region Insert_Code
                    // Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertObisRightsGroup(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateObisRightsGroup(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteObisRightsGroup(dtRow);
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating ObisRightsGroup", ex);
            }
        }

        #region CRUD_Methods

        void InsertObisRightsGroup(Configs.Obis_Rights_GroupRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    obis_rights_group ob_rights_group = new obis_rights_group();
                    ob_rights_group.id = Row.id;
                    ob_rights_group.Group_Name = Row.Group_Name;
                    ob_rights_group.Update_Rights = Row.Update_Rights;
                    this._entityModel.Obis_Rights_Group_Data.Add(ob_rights_group);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving ObisRightsGroup data in data source", ex);
            }
        }

        void UpdateObisRightsGroup(Configs.Obis_Rights_GroupRow Row)
        {
            try
            {
                #region Update_Code
                if (Row.RowState == DataRowState.Modified)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    obis_rights_group ob_rights_group = this._entityModel.Obis_Rights_Group_Data.SingleOrDefault(x => x.id == id);
                    if (ob_rights_group != null)
                    {
                        ob_rights_group.Group_Name = Row.Group_Name;
                        ob_rights_group.Update_Rights = Row.Update_Rights;
                        this._entityModel.Obis_Rights_Group_Data.Attach(ob_rights_group);
                        this._entityModel.Entry(ob_rights_group).State = EntityState.Modified;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying ObisRightsGroup data in data source", ex);
            }
        }

        void DeleteObisRightsGroup(Configs.Obis_Rights_GroupRow Row)
        {
            try
            {
                #region Delete_Code

                if (Row.RowState == DataRowState.Deleted)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    obis_rights_group ob_rights_group = this._entityModel.Obis_Rights_Group_Data.SingleOrDefault(x => x.id == id);
                    if (ob_rights_group != null)
                    {
                        this._entityModel.Obis_Rights_Group_Data.Remove(ob_rights_group);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting ObisRightsGroup data in data source", ex);
            }
        }

        #endregion 

        #endregion

        #region Configurations
        void AcceptChangesConfigurations(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.ConfigurationRow> Rows = DataSet.Configuration.ToList<Configs.ConfigurationRow>();
                for (int index = 0; (Rows != null && index < Rows.Count); index++)
                {
                    Configs.ConfigurationRow dtRow = Rows[index];
                    #region Insert_Code
                    // Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertConfigurations(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateConfigurations(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteConfigurations(dtRow);
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating Configurations", ex);
            }
        }

        #region CRUD_Methods

        void InsertConfigurations(Configs.ConfigurationRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    configuration_new configs = new configuration_new();
                    configs.id = Row.id;
                    configs.Name = Row.Name;
                    configs.LP_Channels_Group_ID = Row.lp_channels_group_id;
                    configs.BillItemsGroupId = Row.BillItemsGroupId;
                    configs.EventGroupId = Row.EventGroupId;
                    configs.DisplayWindowGroupId = Row.DisplayWindowGroupId;

                    this._entityModel.Configurations_Data.Add(configs);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving Configurations data in data source", ex);
            }
        }

        void UpdateConfigurations(Configs.ConfigurationRow Row)
        {
            try
            {
                #region Update_Code
                if (Row.RowState == DataRowState.Modified)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    configuration_new configs = this._entityModel.Configurations_Data.SingleOrDefault(x => x.id == id);
                    if (configs != null)
                    {
                        configs.Name = Row.Name;
                        configs.LP_Channels_Group_ID = Row.lp_channels_group_id;
                        configs.BillItemsGroupId = Row.BillItemsGroupId;
                        configs.EventGroupId = Row.EventGroupId;
                        configs.DisplayWindowGroupId = Row.DisplayWindowGroupId;

                        this._entityModel.Configurations_Data.Attach(configs);
                        this._entityModel.Entry(configs).State = EntityState.Modified;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying Configurations data in data source", ex);
            }
        }

        void DeleteConfigurations(Configs.ConfigurationRow Row)
        {
            try
            {
                #region Delete_Code

                if (Row.RowState == DataRowState.Deleted)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    configuration_new configs = this._entityModel.Configurations_Data.SingleOrDefault(x => x.id == id);
                    if (configs != null)
                    {
                        this._entityModel.Configurations_Data.Remove(configs);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting Configurations data in data source", ex);
            }
        }

        #endregion 

        #endregion

        #region Manufacturer
        void AcceptChangesManufacturer(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.ManufacturerRow> Rows = DataSet.Manufacturer.ToList<Configs.ManufacturerRow>();
                for (int index = 0; (Rows != null && index < Rows.Count); index++)
                {
                    Configs.ManufacturerRow dtRow = Rows[index];
                    #region Insert_Code
                    // Exe Insert Query
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

        void InsertManufacturer(Configs.ManufacturerRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    manufacturer manuf = new manufacturer();
                    manuf.id = Row.id;
                    manuf.Manufacturer_Name = Row.Manufacturer_Name;
                    manuf.Code = Row.Code;
                    this._entityModel.Manufacturer_Data.Add(manuf);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving Manufacturer data in data source", ex);
            }
        }

        void UpdateManufacturer(Configs.ManufacturerRow Row)
        {
            try
            {
                #region Update_Code
                if (Row.RowState == DataRowState.Modified)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    manufacturer manuf = this._entityModel.Manufacturer_Data.SingleOrDefault(x => x.id == id);
                    if (manuf != null)
                    {
                        manuf.Manufacturer_Name = Row.Manufacturer_Name;
                        manuf.Code = Row.Code;
                        this._entityModel.Manufacturer_Data.Attach(manuf);
                        this._entityModel.Entry(manuf).State = EntityState.Modified;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying Manufacturer data in data source", ex);
            }
        }

        void DeleteManufacturer(Configs.ManufacturerRow Row)
        {
            try
            {
                #region Delete_Code

                if (Row.RowState == DataRowState.Deleted)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    manufacturer manuf = this._entityModel.Manufacturer_Data.SingleOrDefault(x => x.id == id);
                    if (manuf != null)
                    {
                        this._entityModel.Manufacturer_Data.Remove(manuf);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting Manufacturer data in data source", ex);
            }
        }

        #endregion

        #endregion

        #region AuthenticationType
        void AcceptChangesAuthenticationType(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.Authentication_TypeRow> Rows = DataSet.Authentication_Type.ToList<Configs.Authentication_TypeRow>();
                for (int index = 0; (Rows != null && index < Rows.Count); index++)
                {
                    Configs.Authentication_TypeRow dtRow = Rows[index];
                    #region Insert_Code
                    // Exe Insert Query
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

        void InsertAuthenticationType(Configs.Authentication_TypeRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    authentication_type auth_type = new authentication_type();
                    auth_type.Id = Row.id;
                    auth_type.Authentication_Type_Name = Row.Authentication_Type_Name;
                    this._entityModel.Authentication_Type_Data.Add(auth_type);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving AuthenticationType data in data source", ex);
            }
        }

        void UpdateAuthenticationType(Configs.Authentication_TypeRow Row)
        {
            try
            {
                #region Update_Code
                if (Row.RowState == DataRowState.Modified)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    authentication_type auth_type = this._entityModel.Authentication_Type_Data.SingleOrDefault(x => x.Id == id);
                    if (auth_type != null)
                    {
                        auth_type.Authentication_Type_Name = Row.Authentication_Type_Name;
                        this._entityModel.Authentication_Type_Data.Attach(auth_type);
                        this._entityModel.Entry(auth_type).State = EntityState.Modified;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying AuthenticationType data in data source", ex);
            }
        }

        void DeleteAuthenticationType(Configs.Authentication_TypeRow Row)
        {
            try
            {
                #region Delete_Code

                if (Row.RowState == DataRowState.Deleted)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    authentication_type auth_type = this._entityModel.Authentication_Type_Data.SingleOrDefault(x => x.Id == id);
                    if (auth_type != null)
                    {
                        this._entityModel.Authentication_Type_Data.Remove(auth_type);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting AuthenticationType data in data source", ex);
            }
        }

        #endregion 

        #endregion

        #region Device
        void AcceptChangesDevice(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.DeviceRow> Rows = DataSet.Device.ToList<Configs.DeviceRow>();
                for (int index = 0; (Rows != null && index < Rows.Count); index++)
                {
                    Configs.DeviceRow dtRow = Rows[index];
                    #region Insert_Code
                    // Exe Insert Query
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

        void InsertDevice(Configs.DeviceRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    device devices = new device();

                    devices.id = Row.id;
                    devices.Device_Name = Row.Device_Name;
                    devices.IsSinglePhase = Row.IsSinglePhase;
                    devices.Manufacturer_Id = Row.Manufacturer_Id;
                    devices.Model = Row.Model;
                    devices.Accuracy = Row.Accuracy;
                    devices.Product = Row.Product;

                    this._entityModel.DevicesData.Add(devices);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving Device data in data source", ex);
            }
        }

        void UpdateDevice(Configs.DeviceRow Row)
        {
            try
            {
                #region Update_Code
                if (Row.RowState == DataRowState.Modified)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    device devices = this._entityModel.DevicesData.SingleOrDefault(x => x.id == id);
                    if (devices != null)
                    {
                        devices.Device_Name = Row.Device_Name;
                        devices.IsSinglePhase = Row.IsSinglePhase;
                        devices.Model = Row.Model;
                        devices.Accuracy = Row.Accuracy;
                        devices.Product = Row.Product;

                        this._entityModel.DevicesData.Attach(devices);
                        this._entityModel.Entry(devices).State = EntityState.Modified;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying Device data in data source", ex);
            }
        }

        void DeleteDevice(Configs.DeviceRow Row)
        {
            try
            {
                #region Delete_Code

                if (Row.RowState == DataRowState.Deleted)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    device devices = this._entityModel.DevicesData.SingleOrDefault(x => x.id == id);
                    if (devices != null)
                    {
                        this._entityModel.DevicesData.Remove(devices);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting Device data in data source", ex);
            }
        }

        #endregion 

        #endregion

        #region Device Association
        void AcceptChangesDeviceAssociation(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.Device_AssociationRow> Rows = DataSet.Device_Association.ToList<Configs.Device_AssociationRow>();
                for (int index = 0; (Rows != null && index < Rows.Count); index++)
                {
                    Configs.Device_AssociationRow dtRow = Rows[index];
                    #region Insert_Code
                    // Exe Insert Query
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

        void InsertDeviceAssociation(Configs.Device_AssociationRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    device_association device_association = new device_association();

                    device_association.id = Row.id;
                    device_association.Association_Name = Row.Association_Name;
                    device_association.Authentication_Type_Id = Row.Authentication_Type_Id;
                    device_association.Configuration_Id = Row.Configuration_Id;
                    device_association.Device_Id = Row.Device_Id;
                    device_association.ObisRightGroupId = Row.ObisRightGroupId;
                    device_association.Client_Sap = Row.Client_Sap;
                    device_association.Meter_Sap = Row.Meter_Sap;
                    device_association.Reload_Config = Row.Reload_Config;
                    device_association.Association_Index = Row.Association_Index;

                    this._entityModel.Device_Associations_Data.Add(device_association);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving DeviceAssociation data in data source", ex);
            }
        }

        void UpdateDeviceAssociation(Configs.Device_AssociationRow Row)
        {
            try
            {
                #region Update_Code
                if (Row.RowState == DataRowState.Modified)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    device_association device_association = this._entityModel.Device_Associations_Data.SingleOrDefault(x => x.id == id);
                    if (device_association != null)
                    {
                        device_association.Association_Name = Row.Association_Name;
                        device_association.Authentication_Type_Id = Row.Authentication_Type_Id;
                        device_association.Configuration_Id = Row.Configuration_Id;
                        device_association.Device_Id = Row.Device_Id;
                        device_association.ObisRightGroupId = Row.ObisRightGroupId;
                        device_association.Client_Sap = Row.Client_Sap;
                        device_association.Meter_Sap = Row.Meter_Sap;
                        device_association.Reload_Config = Row.Reload_Config;
                        device_association.Association_Index = Row.Association_Index;

                        this._entityModel.Device_Associations_Data.Attach(device_association);
                        this._entityModel.Entry(device_association).State = EntityState.Modified;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying DeviceAssociation data in data source", ex);
            }
        }

        void DeleteDeviceAssociation(Configs.Device_AssociationRow Row)
        {
            try
            {
                #region Delete_Code

                if (Row.RowState == DataRowState.Deleted)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    device_association device_association = this._entityModel.Device_Associations_Data.SingleOrDefault(x => x.id == id);
                    if (device_association != null)
                    {
                        this._entityModel.Device_Associations_Data.Remove(device_association);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting DeviceAssociation data in data source", ex);
            }
        }

        #endregion 

        #endregion

        #region All Quantities
        void AcceptChangesAllQuantities(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.AllQuantitiesRow> Rows = DataSet.AllQuantities.ToList<Configs.AllQuantitiesRow>();
                for (int index = 0; (Rows != null && index < Rows.Count); index++)
                {
                    Configs.AllQuantitiesRow dtRow = Rows[index];
                    #region Insert_Code
                    // Exe Insert Query
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
                throw new Exception("Error Updating AllQuantities", ex);
            }
        }

        #region CRUD_Methods

        void InsertAllQuantities(Configs.AllQuantitiesRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    all_quantities all_quan = new all_quantities();

                    all_quan.OBIS_Index = (long)Row.OBIS_Index;
                    all_quan.Label      = Row.Label;
                    all_quan.DP_Name    = Row.Dp_Name;
                    all_quan.Unit       = Row.Unit;
                    all_quan.Priority   = Row.Priority;
                    
                    this._entityModel.AllQuantitiesData.Add(all_quan);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving AllQuantities data in data source", ex);
            }
        }

        public void UpdateAllQuantities(Configs.AllQuantitiesRow Row)
        {
            try
            {
                #region Update_Code
                if (Row.RowState == DataRowState.Modified)
                {
                    decimal obis_index = Convert.ToDecimal(Row["OBIS_Index", DataRowVersion.Original]);
                    all_quantities all_quan = this._entityModel.AllQuantitiesData.SingleOrDefault(x => x.OBIS_Index == obis_index);
                    if (all_quan != null)
                    {
                        all_quan.OBIS_Index = (long)Row.OBIS_Index;
                        all_quan.Label      = Row.Label;
                        all_quan.DP_Name    = Row.Dp_Name;
                        all_quan.Unit       = Row.Unit;
                        all_quan.Priority   = Row.Priority;

                        this._entityModel.AllQuantitiesData.Attach(all_quan);
                        this._entityModel.Entry(all_quan).State = EntityState.Modified;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying AllQuantities data in data source", ex);
            }
        }

        void DeleteAllQuantities(Configs.AllQuantitiesRow Row)
        {
            try
            {
                #region Delete_Code

                if (Row.RowState == DataRowState.Deleted)
                {
                    decimal obis_index = Convert.ToDecimal(Row["OBIS_Index", DataRowVersion.Original]);
                    all_quantities all_quan = this._entityModel.AllQuantitiesData.SingleOrDefault(x => x.OBIS_Index == obis_index);
                    if (all_quan != null)
                    {
                        this._entityModel.AllQuantitiesData.Remove(all_quan);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting AllQuantities data in data source", ex);
            }
        }

        #endregion 

        #endregion

        #region Obis Details
        void AcceptChangesObisDetails(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.OBIS_DetailsRow> Rows = DataSet.OBIS_Details.ToList<Configs.OBIS_DetailsRow>();
                for (int index = 0; (Rows != null && index < Rows.Count); index++)
                {
                    Configs.OBIS_DetailsRow dtRow = Rows[index];
                    #region Insert_Code
                    // Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertObisDetails(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateObisDetails(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteObisDetails(dtRow);
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating obis_details", ex);
            }
        }

        #region CRUD_Methods

        void InsertObisDetails(Configs.OBIS_DetailsRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    obis_details obis_detail = new obis_details();

                    obis_detail.id                = Row.id;
                    obis_detail.Obis_Code         = (long)Row.Obis_Code;
                    obis_detail.Default_OBIS_Code = (long)Row.Default_OBIS_Code;
                    obis_detail.Device_Id         = Row.Device_Id;

                    this._entityModel.ObisDetails_Data.Add(obis_detail);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving obis_details data in data source", ex);
            }
        }

        void UpdateObisDetails(Configs.OBIS_DetailsRow Row)
        {
            try
            {
                #region Update_Code
                if (Row.RowState == DataRowState.Modified)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    obis_details obis_detail = this._entityModel.ObisDetails_Data.SingleOrDefault(x => x.id == id);
                    if (obis_detail != null)
                    {
                        //obis_detail.id                = Row.id;
                        obis_detail.Obis_Code         = (long)Row.Obis_Code;
                        obis_detail.Default_OBIS_Code = (long)Row.Default_OBIS_Code;
                        obis_detail.Device_Id         = Row.Device_Id;

                        this._entityModel.ObisDetails_Data.Attach(obis_detail);
                        this._entityModel.Entry(obis_detail).State = EntityState.Modified;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying obis_details data in data source", ex);
            }
        }

        void DeleteObisDetails(Configs.OBIS_DetailsRow Row)
        {
            try
            {
                #region Delete_Code

                if (Row.RowState == DataRowState.Deleted)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    obis_details obis_detail = this._entityModel.ObisDetails_Data.SingleOrDefault(x => x.id == id);
                    if (obis_detail != null)
                    {
                        this._entityModel.ObisDetails_Data.Remove(obis_detail);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting obis_details data in data source", ex);
            }
        }

        #endregion 

        #endregion
        
        #region users
        void AcceptChangesUsers(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.usersRow> Rows = DataSet.users.ToList<Configs.usersRow>();
                for (int index = 0; (Rows != null && index < Rows.Count); index++)
                {
                    Configs.usersRow dtRow = Rows[index];
                    #region Insert_Code
                    // Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertUsers(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateUsers(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteUsers(dtRow);
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating CaptureObjects", ex);
            }
        }

        #region CRUD_Methods

        void InsertUsers(Configs.usersRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    users user_obj = new users();

                    user_obj.user_name = Row.user_name;
                    user_obj.user_password = Row.user_password;
                    user_obj.user_type = Row.user_type;

                    this._entityModel.users.Add(user_obj);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving users data in data source", ex);
            }
        }

        void UpdateUsers(Configs.usersRow Row)
        {
            try
            {
                #region Update_Code
                if (Row.RowState == DataRowState.Modified)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);

                    users user_obj = this._entityModel.users.SingleOrDefault(x => x.id == id);
                    if (user_obj != null)
                    {
                        user_obj.user_name = Row.user_name;
                        user_obj.user_password = Row.user_password;
                        user_obj.user_type = (int)Row.user_type;

                        this._entityModel.users.Attach(user_obj);
                        this._entityModel.Entry(user_obj).State = EntityState.Modified;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying CaptureObjects data in data source", ex);
            }
        }

        void DeleteUsers(Configs.usersRow Row)
        {
            try
            {
                #region Delete_Code

                if (Row.RowState == DataRowState.Deleted)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);

                    users user_obj = this._entityModel.users.SingleOrDefault(x => x.id == id);

                    if (user_obj != null)
                    {
                        this._entityModel.users.Remove(user_obj);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting CaptureObjects data in data source", ex);
            }
        }

        #endregion 

        #endregion
        
        #region Capture Objects
        void AcceptChangesCaptureObjects(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.CaptureObjectsRow> Rows = DataSet.CaptureObjects.ToList<Configs.CaptureObjectsRow>();
                for (int index = 0; (Rows != null && index < Rows.Count); index++)
                {
                    Configs.CaptureObjectsRow dtRow = Rows[index];
                    #region Insert_Code
                    // Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertCaptureObjects(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateCaptureObjects(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteCaptureObjects(dtRow);
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Error Updating CaptureObjects", ex);
            }
        }

        #region CRUD_Methods

        void InsertCaptureObjects(Configs.CaptureObjectsRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    capture_objects cap_obj = new capture_objects();

                    cap_obj.SequenceId        = Row.SequenceId;
                    cap_obj.OBIS_Index        = (long)Row.OBIS_Index;
                    cap_obj.AttributeNo       = Row.AttributeNo;
                    cap_obj.DataIndex         = (int)Row.DataIndex;
                    cap_obj.ConfigId          = Row.ConfigId;
                    cap_obj.GroupId           = (int)Row.GroupId;
                    cap_obj.Target_OBIS_Index = (long)Row.Target_OBIS_Index;
                    cap_obj.DeviceId          = Row.DeviceId;
                    cap_obj.databasefield     = Row.databasefield;
                    cap_obj.Multiplier        = Row.Multiplier;

                    this._entityModel.Capture_Objects_Data.Add(cap_obj);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving CaptureObjects data in data source", ex);
            }
        }

        void UpdateCaptureObjects(Configs.CaptureObjectsRow Row)
        {
            try
            {
                #region Update_Code
                if (Row.RowState == DataRowState.Modified)
                {
                    long obis_index = Convert.ToInt64(Row["OBIS_Index", DataRowVersion.Original]);
                    int deviceId = Convert.ToInt32(Row["DeviceId", DataRowVersion.Original]);
                    long groupId = Convert.ToInt64(Row["GroupId", DataRowVersion.Original]);
                    long targetIndex = Convert.ToInt64(Row["Target_OBIS_Index", DataRowVersion.Original]);

                    capture_objects cap_obj = this._entityModel.Capture_Objects_Data.SingleOrDefault(x => x.OBIS_Index == obis_index 
                                              && (x.GroupId == groupId || x.GroupId == null) && x.Target_OBIS_Index == targetIndex && x.DeviceId == deviceId);
                    if (cap_obj != null)
                    {
                        cap_obj.SequenceId        = Row.SequenceId;
                        cap_obj.OBIS_Index        = (long)Row.OBIS_Index;
                        cap_obj.AttributeNo       = Row.AttributeNo;
                        cap_obj.DataIndex         = (int)Row.DataIndex;
                        cap_obj.ConfigId          = Row.ConfigId;
                        cap_obj.GroupId           = (int)Row.GroupId;
                        cap_obj.Target_OBIS_Index = (long)Row.Target_OBIS_Index;
                        cap_obj.DeviceId          = Row.DeviceId;
                        cap_obj.databasefield     = Row.databasefield;
                        cap_obj.Multiplier        = Row.Multiplier;

                        this._entityModel.Capture_Objects_Data.Attach(cap_obj);
                        this._entityModel.Entry(cap_obj).State = EntityState.Modified;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying CaptureObjects data in data source", ex);
            }
        }

        void DeleteCaptureObjects(Configs.CaptureObjectsRow Row)
        {
            try
            {
                #region Delete_Code

                if (Row.RowState == DataRowState.Deleted)
                {
                    long obis_index  = Convert.ToInt64(Row["OBIS_Index", DataRowVersion.Original]);
                    int deviceId     = Convert.ToInt32(Row["DeviceId", DataRowVersion.Original]);
                    long groupId     = Convert.ToInt64(Row["GroupId", DataRowVersion.Original]);
                    long targetIndex = Convert.ToInt64(Row["Target_OBIS_Index", DataRowVersion.Original]);

                    capture_objects cap_obj = this._entityModel.Capture_Objects_Data.SingleOrDefault(x => x.OBIS_Index == obis_index 
                                              && x.GroupId == groupId && x.Target_OBIS_Index == targetIndex && x.DeviceId == deviceId);
                    if (cap_obj != null)
                    {
                        this._entityModel.Capture_Objects_Data.Remove(cap_obj);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting CaptureObjects data in data source", ex);
            }
        }

        #endregion 

        #endregion

        #region Obis Rights
        void AcceptChangesObisRights(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.OBIS_RightsRow> Rows = DataSet.OBIS_Rights.ToList<Configs.OBIS_RightsRow>();
                for (int index = 0; (Rows != null && index < Rows.Count); index++)
                {
                    Configs.OBIS_RightsRow dtRow = Rows[index];
                    #region Insert_Code
                    // Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertObisRights(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateObisRights(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteObisRights(dtRow);
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating ObisRights", ex);
            }
        }

        #region CRUD_Methods

        void InsertObisRights(Configs.OBIS_RightsRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    obis_rights obis_rights = new obis_rights();

                    obis_rights.id               = Row.id;
                    obis_rights.OBIS_Index       = (long)Row.OBIS_Index;
                    obis_rights.ObisRightGroupId = Row.ObisRightGroupId;
                    obis_rights.Version          = Row.Version;
                    obis_rights.ClientSapId      = Row.ClientSAPId;
                    obis_rights.ServerSapId      = Row.ServerSAPId;

                    this._entityModel.Obis_Rights_Data.Add(obis_rights);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving ObisRights data in data source", ex);
            }
        }

        void UpdateObisRights(Configs.OBIS_RightsRow Row)
        {
            try
            {
                #region Update_Code
                if (Row.RowState == DataRowState.Modified)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    obis_rights obis_rights = this._entityModel.Obis_Rights_Data.SingleOrDefault(x => x.id == id);
                    if (obis_rights != null)
                    {
                        obis_rights.id               = Row.id;
                        obis_rights.OBIS_Index       = (long)Row.OBIS_Index;
                        obis_rights.ObisRightGroupId = Row.ObisRightGroupId;
                        obis_rights.Version          = Row.Version;
                        obis_rights.ClientSapId      = Row.ClientSAPId;
                        obis_rights.ServerSapId      = Row.ServerSAPId;

                        this._entityModel.Obis_Rights_Data.Attach(obis_rights);
                        this._entityModel.Entry(obis_rights).State = EntityState.Modified;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying ObisRights data in data source", ex);
            }
        }

        void DeleteObisRights(Configs.OBIS_RightsRow Row)
        {
            try
            {
                #region Delete_Code

                if (Row.RowState == DataRowState.Deleted)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    obis_rights obis_rights = this._entityModel.Obis_Rights_Data.SingleOrDefault(x => x.id == id);
                    if (obis_rights != null)
                    {
                        this._entityModel.Obis_Rights_Data.Remove(obis_rights);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting ObisRights data in data source", ex);
            }
        }

        #endregion 

        #endregion

        #region Rights
        void AcceptChangesRights(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.RightsRow> Rows = DataSet.Rights.ToList<Configs.RightsRow>();
                for (int index = 0; (Rows != null && index < Rows.Count); index++)
                {
                    Configs.RightsRow dtRow = Rows[index];
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
                throw new Exception("Error Updating Rights", ex);
            }
        }

        #region CRUD_Methods

        void InsertRights(Configs.RightsRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    rights rights = new rights();

                    rights.id = Row.id;
                    rights.type = Row.type;
                    rights.SubId = Row.SubId;
                    rights.value = Row.value;
                    rights.OBIS_Right_Id = Row.OBIS_Right_Id;

                    this._entityModel.Rights_Data.Add(rights);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving Rights data in data source", ex);
            }
        }

        void UpdateRights(Configs.RightsRow Row)
        {
            try
            {
                #region Update_Code
                if (Row.RowState == DataRowState.Modified)
                {
                    byte type = Convert.ToByte(Row["type", DataRowVersion.Original]);
                    byte subId = Convert.ToByte(Row["SubId", DataRowVersion.Original]);
                    decimal obis_right_id = Convert.ToDecimal(Row["OBIS_Right_Id", DataRowVersion.Original]);
                    rights rights = this._entityModel.Rights_Data.SingleOrDefault(x => x.type == type && x.SubId == subId
                                                                                  && x.OBIS_Right_Id == obis_right_id);
                    if (rights != null)
                    {
                        rights.id = Row.id;
                        rights.value = Row.value;

                        this._entityModel.Rights_Data.Attach(rights);
                        this._entityModel.Entry(rights).State = EntityState.Modified;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying Rights data in data source", ex);
            }
        }

        void DeleteRights(Configs.RightsRow Row)
        {
            try
            {
                #region Delete_Code

                if (Row.RowState == DataRowState.Deleted)
                {
                    byte type = Convert.ToByte(Row["type", DataRowVersion.Original]);
                    byte subId = Convert.ToByte(Row["SubId", DataRowVersion.Original]);
                    decimal obis_right_id = Convert.ToDecimal(Row["OBIS_Right_Id", DataRowVersion.Original]);
                    rights rights = this._entityModel.Rights_Data.SingleOrDefault(x => x.type == type && x.SubId == subId
                                                                                  && x.OBIS_Right_Id == obis_right_id);
                    if (rights != null)
                    {
                        this._entityModel.Rights_Data.Remove(rights);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting Rights data in data source", ex);
            }
        }

        #endregion

        #endregion

        #region Load Profile Channels
        void AcceptChangesLoadProfileChannels(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.LoadProfileChannelsRow> Rows = DataSet.LoadProfileChannels.ToList<Configs.LoadProfileChannelsRow>();
                for (int index = 0; (Rows != null && index < Rows.Count); index++)
                {
                    Configs.LoadProfileChannelsRow dtRow = Rows[index];
                    #region Insert_Code
                    // Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertLoadProfileChannels(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateLoadProfileChannels(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteLoadProfileChannels(dtRow);
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating LoadProfileChannels", ex);
            }
        }

        #region CRUD_Methods

        void InsertLoadProfileChannels(Configs.LoadProfileChannelsRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    load_profile_channels lp_channels = new load_profile_channels();

                    //lp_channels.id                 = Row.id;
                    lp_channels.Label              = Row.Label;
                    lp_channels.QuantityIndex      = (long)Row.QuantityIndex;
                    lp_channels.AttributeNo        = Row.AttributeNo;
                    lp_channels.Multiplier         = Row.Multiplier;
                    lp_channels.SequenceId         = Row.SequenceId;
                    lp_channels.FormatSpecifier    = Row.FormatSpecifier;
                    lp_channels.Unit               = Row.Unit;
                    lp_channels.LoadProfileGroupId = Row.LoadProfileGroupId;

                    this._entityModel.LoadProfileChannels_Data.Add(lp_channels);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving LoadProfileChannels data in data source", ex);
            }
        }

        void UpdateLoadProfileChannels(Configs.LoadProfileChannelsRow Row)
        {
            try
            {
                #region Update_Code
                if (Row.RowState == DataRowState.Modified)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    decimal quantity_index = Convert.ToDecimal(Row["QuantityIndex", DataRowVersion.Original]);
                    int groupId = Convert.ToInt32(Row["LoadProfileGroupId", DataRowVersion.Original]);

                    load_profile_channels lp_channels = this._entityModel.LoadProfileChannels_Data.SingleOrDefault(x => x.id == id
                                              && x.QuantityIndex == quantity_index && x.LoadProfileGroupId == groupId);
                    if (lp_channels != null)
                    {
                        lp_channels.Label              = Row.Label;
                        lp_channels.QuantityIndex      = (long)Row.QuantityIndex;
                        lp_channels.AttributeNo        = Row.AttributeNo;
                        lp_channels.Multiplier         = Row.Multiplier;
                        lp_channels.SequenceId         = Row.SequenceId;
                        lp_channels.FormatSpecifier    = Row.FormatSpecifier;
                        lp_channels.Unit               = Row.Unit;
                        lp_channels.LoadProfileGroupId = Row.LoadProfileGroupId;

                        this._entityModel.LoadProfileChannels_Data.Attach(lp_channels);
                        this._entityModel.Entry(lp_channels).State = EntityState.Modified;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying LoadProfileChannels data in data source", ex);
            }
        }

        void DeleteLoadProfileChannels(Configs.LoadProfileChannelsRow Row)
        {
            try
            {
                #region Delete_Code

                if (Row.RowState == DataRowState.Deleted)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    decimal quantity_index = Convert.ToDecimal(Row["QuantityIndex", DataRowVersion.Original]);
                    int groupId = Convert.ToInt32(Row["LoadProfileGroupId", DataRowVersion.Original]);

                    load_profile_channels lp_channels = this._entityModel.LoadProfileChannels_Data.SingleOrDefault(x => x.id == id
                                              && x.QuantityIndex == quantity_index && x.LoadProfileGroupId == groupId);
                    if (lp_channels != null)
                    {
                        this._entityModel.LoadProfileChannels_Data.Remove(lp_channels);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting LoadProfileChannels data in data source", ex);
            }
        }

        #endregion

        #endregion

        #region Billing Items
        void AcceptChangesBillingItems(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.BillingItemsRow> Rows = DataSet.BillingItems.ToList<Configs.BillingItemsRow>();
                for (int index = 0; (Rows != null && index < Rows.Count); index++)
                {
                    Configs.BillingItemsRow dtRow = Rows[index];
                    #region Insert_Code
                    // Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertBillingItems(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateBillingItems(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteBillingItems(dtRow);
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating BillingItems", ex);
            }
        }

        #region CRUD_Methods

        void InsertBillingItems(Configs.BillingItemsRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    billing_items bill_items = new billing_items();

                    //bill_items.id = Row.id;
                    bill_items.SequenceId      = Row.SequenceId;
                    bill_items.BillItemGroupId = Row.BillItemGroupId;
                    bill_items.Label           = Row.Label;
                    bill_items.FormatSpecifier = Row.FormatSpecifier;
                    bill_items.Multiplier      = Row.Multiplier;
                    bill_items.Unit            = Row.Unit;
                    bill_items.ConfigId        = Row.ConfigId;

                    this._entityModel.BillingItems_Data.Add(bill_items);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving BillingItems data in data source", ex);
            }
        }

        void UpdateBillingItems(Configs.BillingItemsRow Row)
        {
            try
            {
                #region Update_Code
                if (Row.RowState == DataRowState.Modified)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    billing_items bill_items = this._entityModel.BillingItems_Data.SingleOrDefault(x => x.id == id);
                    if (bill_items != null)
                    {
                        bill_items.SequenceId      = Row.SequenceId;
                        bill_items.BillItemGroupId = Row.BillItemGroupId;
                        bill_items.Label           = Row.Label;
                        bill_items.FormatSpecifier = Row.FormatSpecifier;
                        bill_items.Multiplier      = Row.Multiplier;
                        bill_items.Unit            = Row.Unit;
                        bill_items.ConfigId        = Row.ConfigId;

                        this._entityModel.BillingItems_Data.Attach(bill_items);
                        this._entityModel.Entry(bill_items).State = EntityState.Modified;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying BillingItems data in data source", ex);
            }
        }

        void DeleteBillingItems(Configs.BillingItemsRow Row)
        {
            try
            {
                #region Delete_Code

                if (Row.RowState == DataRowState.Deleted)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    billing_items bill_items = this._entityModel.BillingItems_Data.SingleOrDefault(x => x.id == id);
                    if (bill_items != null)
                    {
                        this._entityModel.BillingItems_Data.Remove(bill_items);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting BillingItems data in data source", ex);
            }
        }

        #endregion 

        #endregion

        #region Bill Tariff Quantity
        void AcceptChangesBillTariffQuantity(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.BillTariffQuantityRow> Rows = DataSet.BillTariffQuantity.ToList<Configs.BillTariffQuantityRow>();
                for (int index = 0; (Rows != null && index < Rows.Count); index++)
                {
                    Configs.BillTariffQuantityRow dtRow = Rows[index];
                    #region Insert_Code
                    // Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertBillTariffQuantity(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateBillTariffQuantity(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteBillTariffQuantity(dtRow);
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating BillTariffQuantity", ex);
            }
        }

        #region CRUD_Methods

        void InsertBillTariffQuantity(Configs.BillTariffQuantityRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    bill_tariff_quantity bill_tariff = new bill_tariff_quantity();

                    bill_tariff.BillItemId    = Row.BillItemId;
                    bill_tariff.SequenceId    = Row.SequenceId;
                    bill_tariff.OBIS_Index    = (long)Row.OBIS_Index;
                    bill_tariff.DatabaseField = Row.DatabaseField;

                    this._entityModel.BillTariffQuantity_Data.Add(bill_tariff);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving BillTariffQuantity data in data source", ex);
            }
        }

        void UpdateBillTariffQuantity(Configs.BillTariffQuantityRow Row)
        {
            try
            {
                #region Update_Code
                if (Row.RowState == DataRowState.Modified)
                {
                    int bill_item_id = Convert.ToInt32(Row["BillItemId", DataRowVersion.Original]);
                    decimal obis_index = Convert.ToDecimal(Row["OBIS_Index", DataRowVersion.Original]);
                    bill_tariff_quantity bill_tariff = this._entityModel.BillTariffQuantity_Data.SingleOrDefault(x => x.BillItemId == bill_item_id 
                                                                                                                 && x.OBIS_Index == obis_index);
                    if (bill_tariff != null)
                    {
                        bill_tariff.BillItemId    = Row.BillItemId;
                        bill_tariff.SequenceId    = Row.SequenceId;
                        bill_tariff.OBIS_Index    = (long)Row.OBIS_Index;
                        bill_tariff.DatabaseField = Row.DatabaseField;

                        this._entityModel.BillTariffQuantity_Data.Attach(bill_tariff);
                        this._entityModel.Entry(bill_tariff).State = EntityState.Modified;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying BillTariffQuantity data in data source", ex);
            }
        }

        void DeleteBillTariffQuantity(Configs.BillTariffQuantityRow Row)
        {
            try
            {
                #region Delete_Code

                if (Row.RowState == DataRowState.Deleted)
                {
                    int bill_item_id = Convert.ToInt32(Row["BillItemId", DataRowVersion.Original]);
                    decimal obis_index = Convert.ToDecimal(Row["OBIS_Index", DataRowVersion.Original]);
                    bill_tariff_quantity bill_tariff = this._entityModel.BillTariffQuantity_Data.SingleOrDefault(x => x.BillItemId == bill_item_id
                                                                                                                 && x.OBIS_Index == obis_index);
                    if (bill_tariff != null)
                    {
                        this._entityModel.BillTariffQuantity_Data.Remove(bill_tariff);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting BillTariffQuantity data in data source", ex);
            }
        }

        #endregion 

        #endregion

        #region Events Info
        void AcceptChangesEventsInfo(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.EventInfoRow> Rows = DataSet.EventInfo.ToList<Configs.EventInfoRow>();
                for (int index = 0; (Rows != null && index < Rows.Count); index++)
                {
                    Configs.EventInfoRow dtRow = Rows[index];
                    #region Insert_Code
                    // Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertEventsInfo(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateEventsInfo(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteEventsInfo(dtRow);
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating EventsInfo", ex);
            }
        }

        #region CRUD_Methods

        void InsertEventsInfo(Configs.EventInfoRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    event_info ev_info = new event_info();

                    ev_info.id            = Row.id;
                    ev_info.EventCode     = Row.EventCode;
                    ev_info.Label         = Row.Label;
                    ev_info.MaxEventCount = Row.MaxEventCount;
                    ev_info.EventNo       = Row.EventNo;
                    ev_info.EventGroupId  = Row.EventGroupId;
                    ev_info.ConfigId      = Row.ConfigId;

                    this._entityModel.Events_Info_Data.Add(ev_info);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving EventsInfo data in data source", ex);
            }
        }

        void UpdateEventsInfo(Configs.EventInfoRow Row)
        {
            try
            {
                #region Update_Code
                if (Row.RowState == DataRowState.Modified)
                {
                    int ev_code = Convert.ToInt32(Row["EventCode", DataRowVersion.Original]);
                    int ev_group = Convert.ToInt32(Row["EventGroupId", DataRowVersion.Original]);
                    event_info ev_info = this._entityModel.Events_Info_Data.SingleOrDefault(x => x.EventCode == ev_code && x.EventGroupId == ev_group);
                    if (ev_info != null)
                    {
                        ev_info.id            = Row.id;
                        ev_info.EventCode     = Row.EventCode;
                        ev_info.Label         = Row.Label;
                        ev_info.MaxEventCount = Row.MaxEventCount;
                        ev_info.EventNo       = Row.EventNo;
                        ev_info.EventGroupId  = Row.EventGroupId;
                        ev_info.ConfigId      = Row.ConfigId;

                        this._entityModel.Events_Info_Data.Attach(ev_info);
                        this._entityModel.Entry(ev_info).State = EntityState.Modified;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying EventsInfo data in data source", ex);
            }
        }

        void DeleteEventsInfo(Configs.EventInfoRow Row)
        {
            try
            {
                #region Delete_Code

                if (Row.RowState == DataRowState.Deleted)
                {
                    int ev_code = Convert.ToInt32(Row["EventCode", DataRowVersion.Original]);
                    int ev_group = Convert.ToInt32(Row["EventGroupId", DataRowVersion.Original]);
                    event_info ev_info = this._entityModel.Events_Info_Data.SingleOrDefault(x => x.EventCode == ev_code && x.EventGroupId == ev_group);
                    if (ev_info != null)
                    {
                        this._entityModel.Events_Info_Data.Remove(ev_info);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting EventsInfo data in data source", ex);
            }
        }

        #endregion 

        #endregion

        #region Events Logs
        void AcceptChangesEventsLogs(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.EventLogsRow> Rows = DataSet.EventLogs.ToList<Configs.EventLogsRow>();
                for (int index = 0; (Rows != null && index < Rows.Count); index++)
                {
                    Configs.EventLogsRow dtRow = Rows[index];
                    #region Insert_Code
                    // Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertEventsLogs(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateEventsLogs(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteEventsLogs(dtRow);
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating EventsLogs", ex);
            }
        }

        #region CRUD_Methods

        void InsertEventsLogs(Configs.EventLogsRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    event_logs ev_logs = new event_logs();

                    ev_logs.id                = Row.id;
                    ev_logs.EventLogIndex     = (long)Row.EventLogIndex;
                    ev_logs.EventCounterIndex = (long)Row.EventCounterIndex;
                    ev_logs.EventGroupId      = Row.EventGroupId;
                    ev_logs.ConfigId          = Row.ConfigId;

                    this._entityModel.Event_Logs_Data.Add(ev_logs);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving EventsLogs data in data source", ex);
            }
        }

        void UpdateEventsLogs(Configs.EventLogsRow Row)
        {
            try
            {
                #region Update_Code
                if (Row.RowState == DataRowState.Modified)
                {
                    int id_pk = Convert.ToInt32(Row["id_pk", DataRowVersion.Original]);
                    int ev_group = Convert.ToInt32(Row["EventGroupId", DataRowVersion.Original]);
                    event_logs ev_logs = this._entityModel.Event_Logs_Data.SingleOrDefault(x => x.id_pk == id_pk && x.EventGroupId == ev_group);
                    if (ev_logs != null)
                    {
                        ev_logs.id                = Row.id;
                        ev_logs.EventLogIndex     = (long)Row.EventLogIndex;
                        ev_logs.EventCounterIndex = (long)Row.EventCounterIndex;
                        ev_logs.EventGroupId      = Row.EventGroupId;
                        ev_logs.ConfigId          = Row.ConfigId;

                        this._entityModel.Event_Logs_Data.Attach(ev_logs);
                        this._entityModel.Entry(ev_logs).State = EntityState.Modified;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying EventsLogs data in data source", ex);
            }
        }

        void DeleteEventsLogs(Configs.EventLogsRow Row)
        {
            try
            {
                #region Delete_Code

                if (Row.RowState == DataRowState.Deleted)
                {
                    int id_pk = Convert.ToInt32(Row["id_pk", DataRowVersion.Original]);
                    int ev_group = Convert.ToInt32(Row["EventGroupId", DataRowVersion.Original]);
                    event_logs ev_logs = this._entityModel.Event_Logs_Data.SingleOrDefault(x => x.id_pk == id_pk && x.EventGroupId == ev_group);
                    if (ev_logs != null)
                    {
                        this._entityModel.Event_Logs_Data.Remove(ev_logs);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting EventsLogs data in data source", ex);
            }
        }

        #endregion 

        #endregion

        #region Display Windows
        void AcceptChangesDisplayWindows(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.DisplayWindowsRow> Rows = DataSet.DisplayWindows.ToList<Configs.DisplayWindowsRow>();
                for (int index = 0; (Rows != null && index < Rows.Count); index++)
                {
                    Configs.DisplayWindowsRow dtRow = Rows[index];
                    #region Insert_Code
                    // Exe Insert Query
                    if (dtRow.RowState == DataRowState.Added)
                    {
                        InsertDisplayWindows(dtRow);
                    }
                    #endregion
                    #region Update_Code
                    else if (dtRow.RowState == DataRowState.Modified)
                    {
                        UpdateDisplayWindows(dtRow);
                    }
                    #endregion
                    #region Delete_Code
                    else if (dtRow.RowState == DataRowState.Deleted)
                    {
                        DeleteDisplayWindows(dtRow);
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating DisplayWindows", ex);
            }
        }

        #region CRUD_Methods

        void InsertDisplayWindows(Configs.DisplayWindowsRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    display_windows displa_win = new display_windows();

                    //displa_win.id                    = Row.id;
                    displa_win.Category              = Row.Category;
                    displa_win.Label                 = Row.Label;
                    displa_win.AttributeNo           = Row.AttributeNo;
                    displa_win.WinNumberToDisplay    = Row.WinNumberToDisplay;
                    displa_win.QuantityIndex         = (long)Row.QuantityIndex;
                    displa_win.SequenceId            = Row.SequenceId;
                    displa_win.DisplayWindowsGroupId = Row.DisplayWindowsGroupId;
                    displa_win.ConfigId              = Row.ConfigId;

                    this._entityModel.Display_Windows_Data.Add(displa_win);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving DisplayWindows data in data source", ex);
            }
        }

        void UpdateDisplayWindows(Configs.DisplayWindowsRow Row)
        {
            try
            {
                #region Update_Code
                if (Row.RowState == DataRowState.Modified)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    display_windows displa_win = this._entityModel.Display_Windows_Data.SingleOrDefault(x => x.id == id);
                    if (displa_win != null)
                    {
                        displa_win.Category              = Row.Category;
                        displa_win.Label                 = Row.Label;
                        displa_win.AttributeNo           = Row.AttributeNo;
                        displa_win.WinNumberToDisplay    = Row.WinNumberToDisplay;
                        displa_win.QuantityIndex         = (long)Row.QuantityIndex;
                        displa_win.SequenceId            = Row.SequenceId;
                        displa_win.DisplayWindowsGroupId = Row.DisplayWindowsGroupId;
                        displa_win.ConfigId              = Row.ConfigId;

                        this._entityModel.Display_Windows_Data.Attach(displa_win);
                        this._entityModel.Entry(displa_win).State = EntityState.Modified;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying DisplayWindows data in data source", ex);
            }
        }

        void DeleteDisplayWindows(Configs.DisplayWindowsRow Row)
        {
            try
            {
                #region Delete_Code

                if (Row.RowState == DataRowState.Deleted)
                {
                    int id = Convert.ToInt32(Row["id", DataRowVersion.Original]);
                    display_windows displa_win = this._entityModel.Display_Windows_Data.SingleOrDefault(x => x.id == id);
                    if (displa_win != null)
                    {
                        this._entityModel.Display_Windows_Data.Remove(displa_win);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting DisplayWindows data in data source", ex);
            }
        }

        #endregion 

        #endregion

        #region Status Word
        void AcceptChangesStatusWord(Configs DataSet)
        {
            try
            {
                if (DataSet == null)
                    return;
                List<Configs.Status_WordRow> MeterUserRows = DataSet.Status_Word.ToList<Configs.Status_WordRow>();
                for (int index = 0; (MeterUserRows != null && index < MeterUserRows.Count); index++)
                {
                    Configs.Status_WordRow dtRow = MeterUserRows[index];
                    #region Insert_Code
                    // Exe Insert Query
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
                throw new Exception("Error Updating StatusWord", ex);
            }
        }

        #region CRUD_Methods

        void InsertStatusWord(Configs.Status_WordRow Row)
        {
            try
            {
                #region Insert_Code
                ///Exe Insert Query
                if (Row.RowState == DataRowState.Added)
                {
                    status_word st_word = new status_word();
                    st_word.Code = Row.Code;
                    st_word.Name = Row.Name;
                    this._entityModel.StatusWord_Data.Add(st_word);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving StatusWord data in data source", ex);
            }
        }

        void UpdateStatusWord(Configs.Status_WordRow Row)
        {
            try
            {
                #region Update_Code
                if (Row.RowState == DataRowState.Modified)
                {
                    int code = Convert.ToInt32(Row["Code", DataRowVersion.Original]);
                    status_word st_word = this._entityModel.StatusWord_Data.SingleOrDefault(x => x.Code == code);
                    if (st_word != null)
                    {
                        st_word.Name = Row.Name;
                        this._entityModel.StatusWord_Data.Attach(st_word);
                        this._entityModel.Entry(st_word).State = EntityState.Modified;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying StatusWord data in data source", ex);
            }
        }

        void DeleteStatusWord(Configs.Status_WordRow Row)
        {
            try
            {
                #region Delete_Code

                if (Row.RowState == DataRowState.Deleted)
                {
                    int code = Convert.ToInt32(Row["Code", DataRowVersion.Original]);
                    status_word st_word = this._entityModel.StatusWord_Data.SingleOrDefault(x => x.Code == code);
                    if (st_word != null)
                    {
                        this._entityModel.StatusWord_Data.Remove(st_word);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting StatusWord data in data source", ex);
            }
        }

        #endregion

        #endregion
        
        #endregion // Update_DB_Methods

        #endregion // Methods

    }
}
