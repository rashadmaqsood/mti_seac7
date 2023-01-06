using DatabaseConfiguration.DataBase;
using DatabaseConfiguration.DataSet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dlmsConfigurationTool
{
    public class ConfigUpdater
    {
        #region Delegates

        public delegate void update_configs(DataTable _dtable);

        #endregion // Delegates

        #region Fields

        private Configs                            config;
        private ConfigDBController                 dbController;
        public  update_configs                     Update_configs;
        public  Dictionary<string, update_configs> Update_configs_table;

        #endregion // Fields

        #region Constructors

        public ConfigUpdater(Configs _config, ConfigDBController _dbController)
        {
            config       = _config;
            dbController = _dbController;

            this.Update_configs_table = new Dictionary<string, update_configs>();
            this.Init_dicUpdateConfigs();

        }

        #endregion // Constructors

        #region Methods
        
        public void Init_dicUpdateConfigs()
        {
            Update_configs_table.Add("all_quantities"         , this.Update_configs_AllQuantities);
            Update_configs_table.Add("authentication_type"    , this.Update_configs_Authentication_Type);
            Update_configs_table.Add("bill_tariff_quantity"   , this.Update_configs_BillTariffQuantity);
            Update_configs_table.Add("billing_items"          , this.Update_configs_BillingItems);
            Update_configs_table.Add("billingitem_group"      , this.Update_configs_BillingItem_Group);
            Update_configs_table.Add("capture_objects"        , this.Update_configs_CaptureObjects);
            Update_configs_table.Add("configuration_new"      , this.Update_configs_Configuration);
            Update_configs_table.Add("device"                 , this.Update_configs_Device);
            Update_configs_table.Add("device_association"     , this.Update_configs_Device_Association);
            Update_configs_table.Add("display_windows"        , this.Update_configs_DisplayWindows);
            Update_configs_table.Add("displaywindows_group"   , this.Update_configs_DisplayWindows_Group);
            Update_configs_table.Add("event_info"             , this.Update_configs_EventInfo);
            Update_configs_table.Add("event_logs"             , this.Update_configs_EventLogs);
            Update_configs_table.Add("events_group"           , this.Update_configs_Events_Group);
            Update_configs_table.Add("load_profile_channels"  , this.Update_configs_LoadProfileChannels);
            Update_configs_table.Add("loadprofile_group"      , this.Update_configs_LoadProfile_Group);
            Update_configs_table.Add("manufacturer"           , this.Update_configs_Manufacturer);
            Update_configs_table.Add("obis_details"           , this.Update_configs_OBIS_Details);
            Update_configs_table.Add("obis_rights"            , this.Update_configs_OBIS_Rights);
            Update_configs_table.Add("obis_rights_group"      , this.Update_configs_Obis_Rights_Group);
            Update_configs_table.Add("rights"                 , this.Update_configs_Rights);
            Update_configs_table.Add("status_word"            , this.Update_configs_Status_Word);
            Update_configs_table.Add("users"                  , this.Update_configs_users);
        }
        public void Update_configs_AllQuantities(DataTable dt_Excel)
        {
            try
            {
                for (int i = 0; i < dt_Excel.Rows.Count; i++)
                {
                    Configs.AllQuantitiesRow newRow = (Configs.AllQuantitiesRow)config.AllQuantities.NewRow();

                    for (int j = 0; j < dt_Excel.Columns.Count; j++)
                    {
                        newRow[j] = string.IsNullOrEmpty(dt_Excel.Rows[i][j].ToString()) ? 0 : dt_Excel.Rows[i][j];
                    }
                    
                    Configs.AllQuantitiesRow existingRow = config.AllQuantities.FirstOrDefault(x => x.OBIS_Index == newRow.OBIS_Index);

                    /*
                    if (existingRow != null)
                        config.AllQuantities.RemoveAllQuantitiesRow(existingRow);
                    
                    config.AllQuantities.AddAllQuantitiesRow(newRow);
                    */
                    
                    if (existingRow == null)
                    {
                        config.AllQuantities.AddAllQuantitiesRow(newRow);
                    }
                    else
                    {
                        existingRow.Label = newRow.Label;
                        existingRow.Dp_Name = newRow.Dp_Name;
                        existingRow.Unit = newRow.Unit;
                        existingRow.Priority = newRow.Priority;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_configs_Authentication_Type(DataTable dt_Excel)
        {
            try
            {
                for (int i = 0; i < dt_Excel.Rows.Count; i++)
                {
                    Configs.Authentication_TypeRow newRow = (Configs.Authentication_TypeRow)config.Authentication_Type.NewRow();

                    for (int j = 0; j < dt_Excel.Columns.Count; j++)
                    {
                        newRow[j] = string.IsNullOrEmpty(dt_Excel.Rows[i][j].ToString()) ? 0 : dt_Excel.Rows[i][j];
                    }

                    Configs.Authentication_TypeRow existingRow = config.Authentication_Type.FindByid(newRow.id);
                    if (existingRow == null)
                    {
                        config.Authentication_Type.AddAuthentication_TypeRow(newRow);
                    }
                    else
                    {
                        existingRow.id = newRow.id;
                        existingRow.Authentication_Type_Name = newRow.Authentication_Type_Name;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_configs_BillingItem_Group(DataTable dt_Excel)
        {
            try
            {
                for (int i = 0; i < dt_Excel.Rows.Count; i++)
                {
                    Configs.BillingItem_GroupRow newRow = (Configs.BillingItem_GroupRow)config.BillingItem_Group.NewRow();

                    for (int j = 0; j < dt_Excel.Columns.Count; j++)
                    {
                        newRow[j] = string.IsNullOrEmpty(dt_Excel.Rows[i][j].ToString()) ? 0 : dt_Excel.Rows[i][j];
                    }

                    Configs.BillingItem_GroupRow existingRow = config.BillingItem_Group.FindByid(newRow.id);
                    if (existingRow == null)
                    {
                        config.BillingItem_Group.AddBillingItem_GroupRow(newRow);
                    }
                    else
                    {
                        existingRow.id = newRow.id;
                        existingRow.BillingItem_Group_Name = newRow.BillingItem_Group_Name;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_configs_BillingItems(DataTable dt_Excel)
        {
            try
            {
                for (int i = 0; i < dt_Excel.Rows.Count; i++)
                {
                    Configs.BillingItemsRow newRow = (Configs.BillingItemsRow)config.BillingItems.NewRow();

                    for (int j = 0; j < dt_Excel.Columns.Count; j++)
                    {
                        newRow[j] = string.IsNullOrEmpty(dt_Excel.Rows[i][j].ToString()) ? 0 : dt_Excel.Rows[i][j];
                    }

                    Configs.BillingItemsRow existingRow = config.BillingItems.FindByid(newRow.id);
                    if (existingRow == null)
                    {
                        config.BillingItems.AddBillingItemsRow(newRow);
                    }
                    else
                    {
                        existingRow.id = newRow.id;
                        existingRow.Label = newRow.Label;
                        existingRow.FormatSpecifier = newRow.FormatSpecifier;
                        existingRow.Unit = newRow.Unit;
                        existingRow.Multiplier = newRow.Multiplier;
                        existingRow.SequenceId = newRow.SequenceId;
                        existingRow.BillItemGroupId = newRow.BillItemGroupId;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_configs_BillTariffQuantity(DataTable dt_Excel)
        {
            try
            {
                for (int i = 0; i < dt_Excel.Rows.Count; i++)
                {
                    Configs.BillTariffQuantityRow newRow = (Configs.BillTariffQuantityRow)config.BillTariffQuantity.NewRow();

                    for (int j = 0; j < dt_Excel.Columns.Count; j++)
                    {
                        newRow[j] = string.IsNullOrEmpty(dt_Excel.Rows[i][j].ToString()) ? 0 : dt_Excel.Rows[i][j];
                    }

                    Configs.BillTariffQuantityRow existingRow = config.BillTariffQuantity.FirstOrDefault(x => x.BillItemId == newRow.BillItemId && x.OBIS_Index == newRow.OBIS_Index);
                    if (existingRow == null)
                    {
                        config.BillTariffQuantity.AddBillTariffQuantityRow(newRow);
                    }
                    else
                    {
                        existingRow.BillItemId = newRow.BillItemId;
                        existingRow.OBIS_Index = newRow.OBIS_Index;
                        existingRow.SequenceId = newRow.SequenceId;
                        existingRow.DatabaseField = newRow.DatabaseField;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_configs_Configuration(DataTable dt_Excel)
        {
            try
            {
                for (int i = 0; i < dt_Excel.Rows.Count; i++)
                {
                    Configs.ConfigurationRow newRow = (Configs.ConfigurationRow)config.Configuration.NewRow();

                    for (int j = 0; j < dt_Excel.Columns.Count; j++)
                    {
                        newRow[j] = string.IsNullOrEmpty(dt_Excel.Rows[i][j].ToString()) ? 0 : dt_Excel.Rows[i][j];
                    }

                    Configs.ConfigurationRow existingRow = config.Configuration.FindByid(newRow.id);
                    if (existingRow == null)
                    {
                        config.Configuration.AddConfigurationRow(newRow);
                    }
                    else
                    {
                        existingRow.id = newRow.id;
                        existingRow.Name = newRow.Name;
                        existingRow.lp_channels_group_id = newRow.lp_channels_group_id;
                        existingRow.BillItemsGroupId = newRow.BillItemsGroupId;
                        existingRow.EventGroupId = newRow.EventGroupId;
                        existingRow.DisplayWindowGroupId = newRow.DisplayWindowGroupId;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_configs_Device(DataTable dt_Excel)
        {
            try
            {
                for (int i = 0; i < dt_Excel.Rows.Count; i++)
                {
                    Configs.DeviceRow newRow = (Configs.DeviceRow)config.Device.NewRow();

                    for (int j = 0; j < dt_Excel.Columns.Count; j++)
                    {
                        newRow[j] = dt_Excel.Rows[i][j];
                    }

                    Configs.DeviceRow existingRow = config.Device.FindByid(newRow.id);
                    if (existingRow == null)
                    {
                        config.Device.AddDeviceRow(newRow);
                    }
                    else
                    {
                        existingRow.id = newRow.id;
                        existingRow.Device_Name = newRow.Device_Name;
                        existingRow.Model = newRow.Model;
                        existingRow.Manufacturer_Id = newRow.Manufacturer_Id;
                        existingRow.IsSinglePhase = newRow.IsSinglePhase;
                        existingRow.Accuracy = newRow.Accuracy;
                        existingRow.Product = newRow.Product;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_configs_Device_Association(DataTable dt_Excel)
        {
            try
            {
                for (int i = 0; i < dt_Excel.Rows.Count; i++)
                {
                    Configs.Device_AssociationRow newRow = (Configs.Device_AssociationRow)config.Device_Association.NewRow();

                    for (int j = 0; j < dt_Excel.Columns.Count; j++)
                    {
                        newRow[j] = string.IsNullOrEmpty(dt_Excel.Rows[i][j].ToString()) ? 0 : dt_Excel.Rows[i][j];
                    }

                    Configs.Device_AssociationRow existingRow = config.Device_Association.FindByid(newRow.id);
                    if (existingRow == null)
                    {
                        config.Device_Association.AddDevice_AssociationRow(newRow);
                    }
                    else
                    {
                        existingRow.id = newRow.id;
                        existingRow.Association_Name = newRow.Association_Name;
                        existingRow.Authentication_Type_Id = newRow.Authentication_Type_Id;
                        existingRow.Client_Sap = newRow.Client_Sap;
                        existingRow.Meter_Sap = newRow.Meter_Sap;
                        existingRow.Device_Id = newRow.Device_Id;
                        existingRow.Configuration_Id = newRow.Configuration_Id;
                        existingRow.ObisRightGroupId = newRow.ObisRightGroupId;
                        existingRow.Reload_Config = newRow.Reload_Config;
                        existingRow.Association_Index = newRow.Association_Index;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_configs_DisplayWindows(DataTable dt_Excel)
        {
            try
            {
                for (int i = 0; i < dt_Excel.Rows.Count; i++)
                {
                    Configs.DisplayWindowsRow newRow = (Configs.DisplayWindowsRow)config.DisplayWindows.NewRow();

                    for (int j = 0; j < dt_Excel.Columns.Count; j++)
                    {
                        newRow[j] = string.IsNullOrEmpty(dt_Excel.Rows[i][j].ToString()) ? 0 : dt_Excel.Rows[i][j];
                    }

                    Configs.DisplayWindowsRow existingRow = config.DisplayWindows.FindByid(newRow.id);
                    if (existingRow == null)
                    {
                        config.DisplayWindows.AddDisplayWindowsRow(newRow);
                    }
                    else
                    {
                        existingRow.id = newRow.id;
                        existingRow.AllQuantitiesRow = newRow.AllQuantitiesRow;
                        existingRow.Label = newRow.Label;
                        existingRow.AttributeNo = newRow.AttributeNo;
                        existingRow.ConfigId = newRow.ConfigId;
                        existingRow.Category = newRow.Category;
                        existingRow.ConfigId = newRow.ConfigId;
                        existingRow.DisplayWindowsGroupId = newRow.DisplayWindowsGroupId;
                        existingRow.DisplayWindows_GroupRow = newRow.DisplayWindows_GroupRow;
                        existingRow.QuantityIndex = newRow.QuantityIndex;
                        existingRow.SequenceId = newRow.SequenceId;
                        existingRow.WinNumberToDisplay = newRow.WinNumberToDisplay;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_configs_DisplayWindows_Group(DataTable dt_Excel)
        {
            try
            {
                for (int i = 0; i < dt_Excel.Rows.Count; i++)
                {
                    Configs.DisplayWindows_GroupRow newRow = (Configs.DisplayWindows_GroupRow)config.DisplayWindows_Group.NewRow();

                    for (int j = 0; j < dt_Excel.Columns.Count; j++)
                    {
                        newRow[j] = string.IsNullOrEmpty(dt_Excel.Rows[i][j].ToString()) ? 0 : dt_Excel.Rows[i][j];
                    }

                    Configs.DisplayWindows_GroupRow existingRow = config.DisplayWindows_Group.FindByid(newRow.id);
                    if (existingRow == null)
                    {
                        config.DisplayWindows_Group.AddDisplayWindows_GroupRow(newRow);
                    }
                    else
                    {
                        existingRow.id = newRow.id;
                        existingRow.Dw_Group_Name = newRow.Dw_Group_Name;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_configs_EventInfo(DataTable dt_Excel)
        {
            try
            {
                for (int i = 0; i < dt_Excel.Rows.Count; i++)
                {
                    Configs.EventInfoRow newRow = (Configs.EventInfoRow)config.EventInfo.NewRow();

                    for (int j = 0; j < dt_Excel.Columns.Count; j++)
                    {
                        newRow[j] = string.IsNullOrEmpty( dt_Excel.Rows[i][j] .ToString() ) ? 0 : dt_Excel.Rows[i][j];
                    }

                    Configs.EventInfoRow existingRow = config.EventInfo.FindByid(newRow.id);
                    if (existingRow == null)
                    {
                        config.EventInfo.AddEventInfoRow(newRow);
                    }
                    else
                    {
                        existingRow.id = newRow.id;
                        existingRow.EventCode = newRow.EventCode;
                        existingRow.Label = newRow.Label;
                        existingRow.MaxEventCount = newRow.MaxEventCount;
                        existingRow.ConfigId = newRow.ConfigId;
                        existingRow.EventNo = newRow.EventNo;
                        existingRow.EventGroupId = newRow.EventGroupId;
                        existingRow.CautionNumber = newRow.CautionNumber; //???
                        existingRow.FlashTime = newRow.FlashTime; //???
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_configs_EventLogs(DataTable dt_Excel)
        {
            try
            {
                for (int i = 0; i < dt_Excel.Rows.Count; i++)
                {
                    Configs.EventLogsRow newRow = (Configs.EventLogsRow)config.EventLogs.NewRow();

                    for (int j = 0; j < dt_Excel.Columns.Count; j++)
                    {
                        newRow[j] = string.IsNullOrEmpty(dt_Excel.Rows[i][j].ToString()) ? 0 : dt_Excel.Rows[i][j];
                    }

                    Configs.EventLogsRow existingRow = config.EventLogs.FirstOrDefault(x => x.id == newRow.id && x.EventGroupId == newRow.EventGroupId);
                    if (existingRow == null)
                    {
                        config.EventLogs.AddEventLogsRow(newRow);
                    }
                    else
                    {
                        existingRow.id                = newRow.id;
                        existingRow.EventLogIndex     = newRow.EventLogIndex;
                        existingRow.EventCounterIndex = newRow.EventCounterIndex;
                        existingRow.EventGroupId      = newRow.EventGroupId;
                        existingRow.ConfigId          = newRow.ConfigId;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_configs_Events_Group(DataTable dt_Excel)
        {
            try
            {
                for (int i = 0; i < dt_Excel.Rows.Count; i++)
                {
                    Configs.Events_GroupRow newRow = (Configs.Events_GroupRow)config.Events_Group.NewRow();

                    for (int j = 0; j < dt_Excel.Columns.Count; j++)
                    {
                        newRow[j] = string.IsNullOrEmpty(dt_Excel.Rows[i][j].ToString()) ? 0 : dt_Excel.Rows[i][j];
                    }

                    Configs.Events_GroupRow existingRow = config.Events_Group.FindByid(newRow.id);
                    if (existingRow == null)
                    {
                        config.Events_Group.AddEvents_GroupRow(newRow);
                    }
                    else
                    {
                        existingRow.id = newRow.id;
                        existingRow.Events_group_Name = newRow.Events_group_Name;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_configs_LoadProfileChannels(DataTable dt_Excel)
        {
            try
            {
                for (int i = 0; i < dt_Excel.Rows.Count; i++)
                {
                    Configs.LoadProfileChannelsRow newRow = (Configs.LoadProfileChannelsRow)config.LoadProfileChannels.NewRow();

                    for (int j = 0; j < dt_Excel.Columns.Count; j++)
                    {
                        newRow[j] = string.IsNullOrEmpty(dt_Excel.Rows[i][j].ToString()) ? 0 : dt_Excel.Rows[i][j];
                    }

                    Configs.LoadProfileChannelsRow existingRow = config.LoadProfileChannels.FindByid(newRow.id);
                    if (existingRow == null)
                    {
                        config.LoadProfileChannels.AddLoadProfileChannelsRow(newRow);
                    }
                    else
                    {
                        existingRow.id = newRow.id;
                        existingRow.Label = newRow.Label;
                        existingRow.QuantityIndex = newRow.QuantityIndex;
                        existingRow.AttributeNo = newRow.AttributeNo;
                        existingRow.Multiplier = newRow.Multiplier;
                        existingRow.SequenceId = newRow.SequenceId;
                        existingRow.Unit = newRow.Unit;
                        existingRow.FormatSpecifier = newRow.FormatSpecifier;
                        existingRow.LoadProfileGroupId = newRow.LoadProfileGroupId;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_configs_LoadProfile_Group(DataTable dt_Excel)
        {
            try
            {
                for (int i = 0; i < dt_Excel.Rows.Count; i++)
                {
                    Configs.LoadProfile_GroupRow newRow = (Configs.LoadProfile_GroupRow)config.LoadProfile_Group.NewRow();

                    for (int j = 0; j < dt_Excel.Columns.Count; j++)
                    {
                        newRow[j] = string.IsNullOrEmpty(dt_Excel.Rows[i][j].ToString()) ? 0 : dt_Excel.Rows[i][j];
                    }

                    Configs.LoadProfile_GroupRow existingRow = config.LoadProfile_Group.FindByid(newRow.id);
                    if (existingRow == null)
                    {
                        config.LoadProfile_Group.AddLoadProfile_GroupRow(newRow);
                    }
                    else
                    {
                        existingRow.id = newRow.id;
                        existingRow.LoadProfile_Group_Name = newRow.LoadProfile_Group_Name;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_configs_Manufacturer(DataTable dt_Excel)
        {
            try
            {
                for (int i = 0; i < dt_Excel.Rows.Count; i++)
                {
                    Configs.ManufacturerRow newRow = (Configs.ManufacturerRow)config.Manufacturer.NewRow();

                    for (int j = 0; j < dt_Excel.Columns.Count; j++)
                    {
                        newRow[j] = string.IsNullOrEmpty(dt_Excel.Rows[i][j].ToString()) ? 0 : dt_Excel.Rows[i][j];
                    }

                    Configs.ManufacturerRow existingRow = config.Manufacturer.FindByid(newRow.id);
                    if (existingRow == null)
                    {
                        config.Manufacturer.AddManufacturerRow(newRow);
                    }
                    else
                    {
                        existingRow.id = newRow.id;
                        existingRow.Manufacturer_Name = newRow.Manufacturer_Name;
                        existingRow.Code = newRow.Code;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_configs_OBIS_Details(DataTable dt_Excel)
        {
            try
            {
                for (int i = 0; i < dt_Excel.Rows.Count; i++)
                {
                    Configs.OBIS_DetailsRow newRow = (Configs.OBIS_DetailsRow)config.OBIS_Details.NewRow();

                    for (int j = 0; j < dt_Excel.Columns.Count; j++)
                    {
                        newRow[j] = string.IsNullOrEmpty(dt_Excel.Rows[i][j].ToString()) ? 0 : dt_Excel.Rows[i][j];
                    }

                    Configs.OBIS_DetailsRow existingRow = config.OBIS_Details.FirstOrDefault(x => x.Default_OBIS_Code == newRow.Default_OBIS_Code && x.Device_Id == newRow.Device_Id);
                    if (existingRow == null)
                    {
                        config.OBIS_Details.AddOBIS_DetailsRow(newRow);
                    }
                    else
                    {
                        existingRow.id = newRow.id;
                        existingRow.Obis_Code = newRow.Obis_Code;
                        existingRow.OBIS_Quantity = newRow.OBIS_Quantity;
                        existingRow.Default_OBIS_Code = newRow.Default_OBIS_Code;
                        existingRow.Device_Id = newRow.Device_Id;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_configs_OBIS_Rights(DataTable dt_Excel)
        {
            try
            {
                for (int i = 0; i < dt_Excel.Rows.Count; i++)
                {
                    Configs.OBIS_RightsRow newRow = (Configs.OBIS_RightsRow)config.OBIS_Rights.NewRow();

                    for (int j = 0; j < dt_Excel.Columns.Count; j++)
                    {
                        newRow[j] = string.IsNullOrEmpty(dt_Excel.Rows[i][j].ToString()) ? 0 : dt_Excel.Rows[i][j];
                    }

                    Configs.OBIS_RightsRow existingRow = config.OBIS_Rights.FindByid(newRow.id);
                    if (existingRow == null)
                    {
                        config.OBIS_Rights.AddOBIS_RightsRow(newRow);
                    }
                    else
                    {
                        existingRow.id = newRow.id;
                        existingRow.ClientSAPId = newRow.ClientSAPId;
                        existingRow.ObisRightGroupId = newRow.ObisRightGroupId;
                        existingRow.OBIS_Index = newRow.OBIS_Index;
                        existingRow.Obis_Rights_GroupRow = newRow.Obis_Rights_GroupRow;
                        existingRow.ServerSAPId = newRow.ServerSAPId;
                        existingRow.Version = newRow.Version;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_configs_Obis_Rights_Group(DataTable dt_Excel)
        {
            try
            {
                for (int i = 0; i < dt_Excel.Rows.Count; i++)
                {
                    Configs.Obis_Rights_GroupRow newRow = (Configs.Obis_Rights_GroupRow)config.Obis_Rights_Group.NewRow();

                    for (int j = 0; j < dt_Excel.Columns.Count; j++)
                    {
                        newRow[j] = string.IsNullOrEmpty(dt_Excel.Rows[i][j].ToString()) ? 0 : dt_Excel.Rows[i][j];
                    }

                    Configs.Obis_Rights_GroupRow existingRow = config.Obis_Rights_Group.FindByid(newRow.id);
                    if (existingRow == null)
                    {
                        config.Obis_Rights_Group.AddObis_Rights_GroupRow(newRow);
                    }
                    else
                    {
                        existingRow.id = newRow.id;
                        existingRow.Group_Name = newRow.Group_Name;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_configs_Rights(DataTable dt_Excel)
        {
            try
            {
                for (int i = 0; i < dt_Excel.Rows.Count; i++)
                {
                    Configs.RightsRow newRow = (Configs.RightsRow)config.Rights.NewRow();

                    for (int j = 0; j < dt_Excel.Columns.Count; j++)
                    {
                        newRow[j] = string.IsNullOrEmpty(dt_Excel.Rows[i][j].ToString()) ? 0 : dt_Excel.Rows[i][j];
                    }

                    Configs.RightsRow existingRow = config.Rights.FindByid(newRow.id);
                    if (existingRow == null)
                    {
                        config.Rights.AddRightsRow(newRow);
                    }
                    else
                    {
                        existingRow.id = newRow.id;
                        existingRow.OBIS_RightsRow = newRow.OBIS_RightsRow;
                        existingRow.OBIS_Right_Id = newRow.OBIS_Right_Id;
                        existingRow.SubId = newRow.SubId;
                        existingRow.type = newRow.type;
                        existingRow.value = newRow.value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_configs_users(DataTable dt_Excel)
        {
            try
            {
                for (int i = 0; i < dt_Excel.Rows.Count; i++)
                {
                    Configs.usersRow newRow = (Configs.usersRow)config.users.NewRow();

                    for (int j = 0; j < dt_Excel.Columns.Count; j++)
                    {
                        newRow[j] = string.IsNullOrEmpty(dt_Excel.Rows[i][j].ToString()) ? 0 : dt_Excel.Rows[i][j];
                    }

                    Configs.usersRow existingRow = config.users.FindByuser_name(newRow.user_name);
                    if (existingRow == null)
                    {
                        config.users.AddusersRow(newRow);
                    }
                    else
                    {
                        existingRow.user_name = newRow.user_name;
                        existingRow.user_password = newRow.user_password;
                        existingRow.user_type = newRow.user_type;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_configs_CaptureObjects(DataTable dt_Excel)
        {
            try
            {
                List<Configs.CaptureObjectsRow> existingRowsGroups  = new List<Configs.CaptureObjectsRow>();    // All Groups
                List<Configs.CaptureObjectsRow> importingRowsGroups = new List<Configs.CaptureObjectsRow>();    // All Groups
                List<Configs.CaptureObjectsRow> existingRowsGroup   = new List<Configs.CaptureObjectsRow>();    // One Group
                List<Configs.CaptureObjectsRow> importingRowsGroup  = new List<Configs.CaptureObjectsRow>();    // One Group

                for (int i = 0; i < dt_Excel.Rows.Count; i++)
                {
                    Configs.CaptureObjectsRow newRow = (Configs.CaptureObjectsRow)config.CaptureObjects.NewRow();

                    for (int j = 0; j < dt_Excel.Columns.Count; j++)
                    {
                        if (dt_Excel.Columns[j].Caption == "id") continue;  // "id" is auto increment primary key
                        newRow[j] = string.IsNullOrEmpty(dt_Excel.Rows[i][j].ToString()) ? 0 : dt_Excel.Rows[i][j];
                    }

                    // Add imported row in RowsGroups
                    importingRowsGroups.Add(newRow);

                    #region OldLogic
                    /*
                    //Configs.CaptureObjectsRow existingRow = config.CaptureObjects.FindByid(newRow.id);
                    Configs.CaptureObjectsRow existingRow = config.CaptureObjects.FirstOrDefault(x => //x.SequenceId        == newRow.SequenceId        &&
                                                                                                      x.OBIS_Index          == newRow.OBIS_Index        &&
                                                                                                      x.AttributeNo         == newRow.AttributeNo       &&
                                                                                                      //x.DataIndex         == newRow.DataIndex         &&
                                                                                                      //x.ConfigId          == newRow.ConfigId          && 
                                                                                                      x.GroupId             == newRow.GroupId           &&
                                                                                                      x.Target_OBIS_Index   == newRow.Target_OBIS_Index &&
                                                                                                      x.DeviceId            == newRow.DeviceId);//      &&
                                                                                                      //x.DatabaseField     == newRow.DatabaseField);
                                        
                    if (existingRow == null)
                    {
                        config.CaptureObjects.AddCaptureObjectsRow(newRow);
                    }
                    else
                    {
                        if (MessageBox.Show(existingRow.GroupId.ToString() + " is already exist. \r\nDo you really want to add it as duplicate", "Prompt", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            config.CaptureObjects.AddCaptureObjectsRow(newRow);
                        } 
                        else
                        {
                            //existingRow.id                = newRow.id;    // "id" is auto increment primary key
                            existingRow.SequenceId        = newRow.SequenceId;
                            existingRow.OBIS_Index        = newRow.OBIS_Index;
                            existingRow.AttributeNo       = newRow.AttributeNo;
                            existingRow.DataIndex         = newRow.DataIndex;
                            existingRow.DeviceId          = newRow.DeviceId;
                            existingRow.GroupId           = newRow.GroupId;
                            existingRow.Target_OBIS_Index = newRow.Target_OBIS_Index;
                            existingRow.DatabaseField     = newRow.DatabaseField;
                        }
                    }
                    */
                    #endregion
                }

                // Save imported rows (mis-matched with exiting rows) to db
                //dbController.Save_N_Load_All_Configs(config);


                // get total distinct GroupIds from excel
                var importingGroupIds  = importingRowsGroups.Select(x => x.GroupId).Distinct().ToList();

                // get total distinct DeviceIds from excel
                var importingDeviceIds = importingRowsGroups.Select(x => x.DeviceId).Distinct().ToList();

                // FIRST FILTER
                // Loop to fetch existing RowsGroups from configs.CaptureObjects w.r.t importingDeviceIds

                #region FIELDS USED IN FOLLOWING LOOPS

                DialogResult dialogResult;
                string       ExistingGroupIdText  = "";
                string       QuestionText         = "";

                #endregion

                for (int h = 0; h < importingDeviceIds.Count; h++)
                {
                    existingRowsGroups.Clear();
                    existingRowsGroups.AddRange(config.CaptureObjects.Where(x => x.DeviceId == importingDeviceIds[h]));

                    // SECOND FILTER
                    // Loop to fetch existing RowsGroups from configs.CaptureObjects w.r.t importingGroupIds
                    for (int i = 0; i < importingGroupIds.Count; i++)
                    {
                        importingRowsGroup = importingRowsGroups.Where(x => x.GroupId == importingGroupIds[i]).ToList();
                        existingRowsGroup  = existingRowsGroups. Where(x => x.GroupId == importingGroupIds[i]).ToList();

                        if (existingRowsGroup.Count > 0)
                        {
                            ExistingGroupIdText = "[GroupId: " + existingRowsGroup[0].GroupId.ToString() + "] already exists, with [" + existingRowsGroup.Count + " capture objects]:\r\n";
                            QuestionText        = "Do you want to REPLACE [it] with the [Importing GroupId: " + importingRowsGroup[0].GroupId.ToString() + "], with [" + importingRowsGroup.Count + " capture objects]:\r\n";

                            MessageForm messageForm = new MessageForm(ExistingGroupIdText, this.ListToString(existingRowsGroup), QuestionText, this.ListToString(importingRowsGroup));
                            dialogResult = messageForm.ShowDialog();

                            switch (dialogResult)
                            {
                                case DialogResult.Yes:
                                    //this.RemoveRowsGroup(existingRowsGroup);
                                    //this.AddRowsGroup_IfNotExist(importingRowsGroup);

                                    // Exit if() block and call "this.AddRowsGroup_IfNotExist(importingRowsGroup);"
                                    break;
                                case DialogResult.No:
                                    var newGroupId = config.CaptureObjects.Select(x => x.GroupId).Max() + 1;
                                    var newRowsGroup = this.Replace_GroupId_in_RowsGroup(newGroupId, importingRowsGroup);
                                    this.AddRowsGroup_IfNotExist(newRowsGroup);
                                    MessageBox.Show("The imported group is added with the new id: " + newGroupId);
                                    break;
                                case DialogResult.Cancel:
                                    return;
                                default:
                                    break;
                            }
                        }

                        this.AddRowsGroup_IfNotExist(importingRowsGroup);
                    } 
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region CRUD_Methods

        private List<Configs.CaptureObjectsRow> Replace_GroupId_in_RowsGroup(long newId, List<Configs.CaptureObjectsRow> _rowGroup)
        {
            List<Configs.CaptureObjectsRow> newRowGroup = new List<Configs.CaptureObjectsRow>();
            foreach (var item in _rowGroup)
            {
                item.GroupId = newId;
                newRowGroup.Add(item);
            }

            return newRowGroup;
        }

        #region RemoveRowsGroup() COMMENTED

        /*
        private void RemoveRowsGroup(List<Configs.CaptureObjectsRow> _rowGroup)
        {
            
            // REMOVE VS DELETE
            // Remove method deletes a DataRow from the DataRowCollection, the Delete method only marks the row for deletion.
             
            
            int RowIndex = 0;
            foreach (var item in _rowGroup)
            {
                // No changes received in dataset after datatable.Rows.Remove()
                //config.CaptureObjects.RemoveCaptureObjectsRow(item);          // RowState does not change to Deleted after Rows.Remove();

                // Changes received in dataset after datatable.Rows[Index_Of_Deleting_Row].delete()
                RowIndex = config.CaptureObjects.Rows.IndexOf(item);
                config.CaptureObjects.Rows[RowIndex].Delete();
            }

            // Save / Load changes to / from db
            dbController.Save_N_Load_All_Configs(config);
        }
        */

        #endregion

        private void AddRowsGroup_IfNotExist(List<Configs.CaptureObjectsRow> _rowGroup)
        {
            Configs.CaptureObjectsRow existingRow = null;

            foreach (var item in _rowGroup)
            {
                existingRow = config.CaptureObjects.FirstOrDefault(x => x.OBIS_Index        == item.OBIS_Index          &&
                                                                        x.AttributeNo       == item.AttributeNo         &&
                                                                        x.GroupId           == item.GroupId             &&
                                                                        x.Target_OBIS_Index == item.Target_OBIS_Index &&
                                                                        x.DeviceId          == item.DeviceId);

                if (existingRow == null)
                {
                    config.CaptureObjects.AddCaptureObjectsRow(item); 
                }
                //int RowIndex = config.CaptureObjects.Rows.IndexOf(item);
                //DataRowState dRS = config.CaptureObjects.Rows[RowIndex].RowState;
            }

            // Save / Load changes to / from db
            //dbController.Save_N_Load_All_Configs(config);
        }

        private string ListToString(List<Configs.CaptureObjectsRow> strList)
        {
            StringBuilder strBuilder = new StringBuilder();

            foreach (var item in strList)
            {
                strBuilder.Append(RowToString(item)).AppendLine();
            }

            return strBuilder.ToString();
        }

        private string RowToString(Configs.CaptureObjectsRow _row)
        {
            StringBuilder strBuilder = new StringBuilder();

            foreach (var item in _row.ItemArray)
            {
                strBuilder.Append(item).AppendFormat("          ");
            }

            return strBuilder.ToString();
        }
        #endregion

        public void Update_configs_Status_Word(DataTable dt_Excel)
        {
            try
            {
                for (int i = 0; i < dt_Excel.Rows.Count; i++)
                {
                    Configs.Status_WordRow newRow = (Configs.Status_WordRow)config.Status_Word.NewRow();

                    for (int j = 0; j < dt_Excel.Columns.Count; j++)
                    {
                        newRow[j] = string.IsNullOrEmpty(dt_Excel.Rows[i][j].ToString()) ? 0 : dt_Excel.Rows[i][j];
                    }

                    Configs.Status_WordRow existingRow = config.Status_Word.FirstOrDefault(x => x.Code == newRow.Code);
                    if (existingRow == null)
                    {
                        config.Status_Word.AddStatus_WordRow(newRow);
                    }
                    else
                    {
                        existingRow.Code = newRow.Code;
                        existingRow.Display_Code = newRow.Display_Code;
                        existingRow.Name = newRow.Name;
                        existingRow.P_Leval = newRow.P_Leval;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion // Methods
    }
}
