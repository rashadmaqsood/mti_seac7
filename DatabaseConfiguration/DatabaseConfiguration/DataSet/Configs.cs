using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data;
using System.IO;
using DatabaseConfiguration.DataBase;
using DLMS;

namespace DatabaseConfiguration.DataSet
{

    public partial class Configs
    {
        partial class Device_AssociationDataTable
        {
        }
    
        partial class ManufacturerDataTable
        {
            public Configs.ManufacturerRow[] GetManufacturerByIdOrName(int id, string name)
            {
                Configs.ManufacturerRow[] AllManufacturers = (Configs.ManufacturerRow[])this.Select(String.Format("id = '{0}' OR Manufacturer_Name = '{1}'", id, name));

                return AllManufacturers;
            }

            public Configs.ManufacturerRow[] GetManufacturerByCodeOrName(string M_Code, string M_name)
            {
                Configs.ManufacturerRow[] AllManufacturers = (Configs.ManufacturerRow[])this.Select(String.Format("Code = '{0}' OR Manufacturer_Name = '{1}'", M_Code, M_name));

                return AllManufacturers;
            }
        }
    
        partial class DeviceDataTable
        {
            public Configs.DeviceRow[] GetDeviceByIdOrName(int id, string name)
            {
                Configs.DeviceRow[] AllManufacturers = (Configs.DeviceRow[])this.Select(String.Format("id = '{0}' OR Device_Name = '{1}'", id, name));

                return AllManufacturers;
            }

            public Configs.DeviceRow[] GetDeviceByNameOrModel(string name,string model,int manufacturerId)
            {
                var results = from myRow in this//[Rashad].AsEnumerable()
                              where (string.IsNullOrEmpty(name) || string.Equals(name, myRow.Device_Name)) &&
                                    (string.IsNullOrEmpty(model) || myRow.Device_Name.Contains(model)) &&
                                    (manufacturerId == -1 || myRow.Manufacturer_Id == manufacturerId)
                              select myRow;

                // Configs.DeviceRow[] AllManufacturers = (Configs.DeviceRow[])this.Select(String.Format(" Device_Name = '{0}' OR = Model like '{1}'", name));

                return results.ToArray <Configs.DeviceRow>();
            }
        }

        partial class Authentication_TypeDataTable
        {
        }

        partial class Obis_Rights_GroupDataTable
        {
        }

        partial class BillingItem_GroupDataTable
        {
        }

        partial class Events_GroupDataTable
        {
            public int GetMaxId()
            {
                try
                {
                    return ((Events_GroupRow)this.Select("ID = MAX(ID)")[0]).id;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        partial class LoadProfile_GroupDataTable
        {
        }

        //partial class LoadProfileChannel_GroupDataTable
        //{
        //    public Configs.LoadProfileChannel_GroupRow GetLoadProfileChannelGroup(ulong? GroupId = null)
        //    {
        //        try
        //        {
        //            Configs.LoadProfileChannel_GroupRow[] Rows = (Configs.LoadProfileChannel_GroupRow[])this.Select(String.Format("load_profile_group_id = {0}", (GroupId != null) ? GroupId : 0), "load_profile_group_id asc");
        //            return Rows[0];
        //        }
        //        catch (Exception)
        //        {
        //            return null;
        //        }
        //    }

        //    public long? MaxLoadProfileChannelGroupId()
        //    {
        //        try
        //        {
        //            long? Rows = this.Max<Configs.LoadProfileChannel_GroupRow>((x) => x.load_profile_group_id);
        //            // this.AsEnumerable().Max(r => r.Field<long>("load_profile_group_id"));
        //            // this.Select(String.Format("GroupId = Max(GroupId)"), "id asc");
        //            return Rows;
        //        }
        //        catch (Exception)
        //        {
        //            return null;
        //        }
        //    }

        //    public void GetLoadProfileChannelGroupId(Configs.LoadProfileChannel_GroupRow Channel_Group, out uint? GroupId)
        //    {
        //        GroupId = null;
        //        try
        //        {
        //            Configs.LoadProfileChannel_GroupRow[] Rows = (Configs.LoadProfileChannel_GroupRow[])this.
        //                Select(String.Format("1 = 1"), "load_profile_group_id asc");
                    
        //            // Compare All Load Profile Load Profile Channels 
        //            foreach (var channelGroup in Rows)
        //            {
        //                if (channelGroup == null)
        //                    continue;
        //                else if (channelGroup.Channel_1 == Channel_Group.Channel_1 &&
        //                         channelGroup.Channel_2 == Channel_Group.Channel_2 &&
        //                         channelGroup.Channel_3 == Channel_Group.Channel_3 &&
        //                         channelGroup.Channel_4 == Channel_Group.Channel_4)
        //                {
        //                    // Update Matching Group Id
        //                    GroupId = (uint)channelGroup.load_profile_group_id;
        //                    break;
        //                }
        //            }

        //        }
        //        catch (Exception)
        //        {
        //            GroupId = null;
        //        }
        //    }
        //}

        private string fileURL = "";

        public string FileSourceURL
        {
            get { return fileURL; }
            set { fileURL = value; }
        }

        public void LoadConfigurations(string connectionString)
        {
            try
            {
                SMT_DBAccessLayer DBDAO = new SMT_DBAccessLayer(connectionString);
                Configs conf = this;
                DBDAO.Load_All_Configurations(ref conf);
                //Find Default Meter & Default Configurations
                // this.Meter_Configuration.CurrentConfiguration = (this.Meter_Configuration.Count > 0) ? this.Meter_Configuration[0] : null;
                //Select Configuration Based On MeterInfo Id
                Configs.ConfigurationRow DefaultConfig = null;
                foreach (var item in Configuration)
                {
                    if (DefaultConfig == null)
                    {
                        DefaultConfig = item;
                        break;
                    }
                }
                Configuration.CurrentConfiguration = DefaultConfig;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveConfigurationData(string connectionString)
        {
            try
            {
                SMT_DBAccessLayer DBDAO = new SMT_DBAccessLayer(connectionString);
                DBDAO.Update_All_Configuration(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        partial class ConfigurationDataTable
        {
            private Configs.ConfigurationRow currentConfiguration;

            public Configs.ConfigurationRow CurrentConfiguration
            {
                get { return currentConfiguration; }
                set { currentConfiguration = value; }
            }

            public Configs.ConfigurationRow[] GetConfigurationsByIdOrName(int id, string name)
            {
                Configs.ConfigurationRow[] AllConfigs = (Configs.ConfigurationRow[])this.Select(String.Format("id = '{0}' OR Name = '{1}'", id, name));

                return AllConfigs;
            }

            public Configs.ConfigurationRow GetConfigurationByNameAndId(int id)
            {
                try
                {
                    Configs.ConfigurationRow[] AllConfigs = GetConfigurationsByIdOrName(id, null);
                    Configs.ConfigurationRow Configuration = null;
                    if (AllConfigs != null && AllConfigs.Length > 0)
                    {
                        Configuration = AllConfigs[0];
                        foreach (var config in AllConfigs)
                        {
                            if (Configuration != config)
                            {
                                if (Configuration != null)
                                {
                                    Configuration = config;
                                    break;
                                }
                                else
                                    continue;
                            }
                        }
                    }
                    return Configuration;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        partial class DisplayWindowsDataTable
        {
            public List<Configs.DisplayWindowsRow> GetDisplayWindowsByConfiguration(Configs.ConfigurationRow CurrentConfig)
            {
                try
                {
                    if (CurrentConfig.IsDisplayWindowGroupIdNull())
                        throw new ArgumentNullException("DisplayWindowGroupId");

                    List<Configs.DisplayWindowsRow> SelectedWindows = new List<DisplayWindowsRow>();

                    foreach (var item in this.Rows)
                    {
                        if (((Configs.DisplayWindowsRow)item).DisplayWindowsGroupId == CurrentConfig.DisplayWindowGroupId)
                            SelectedWindows.Add(((Configs.DisplayWindowsRow)item));
                    }

                    // Configs.DisplayWindowsRow[] Rows = (Configs.DisplayWindowsRow[])this.Select(String.Format("ConfigId = {0}", CurrentConfig.id));
                    // SelectedWindows.AddRange(Rows);
                    SelectedWindows.Sort((x, y) => (x.SequenceId.CompareTo(y.SequenceId) != 0) ? (x.SequenceId.CompareTo(y.SequenceId)) : (x.QuantityIndex.CompareTo(y.QuantityIndex)));

                    return SelectedWindows;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            public List<Configs.DisplayWindowsRow> GetDisplayWindowsByGroup(Configs.DisplayWindows_GroupRow CurrentConfig)
            {
                try
                {
                    List<Configs.DisplayWindowsRow> SelectedWindows = new List<DisplayWindowsRow>();

                    foreach (var item in this.Rows)
                    {
                        if (((Configs.DisplayWindowsRow)item).DisplayWindowsGroupId == CurrentConfig.id)
                            SelectedWindows.Add(((Configs.DisplayWindowsRow)item));
                    }

                    SelectedWindows.Sort((x, y) => (x.SequenceId.CompareTo(y.SequenceId) != 0) ? (x.SequenceId.CompareTo(y.SequenceId)) : (x.QuantityIndex.CompareTo(y.QuantityIndex)));

                    return SelectedWindows;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        partial class LoadProfileChannelsDataTable
        {
            public List<Configs.LoadProfileChannelsRow> GetLoadProfileByConfiguration(Configs.ConfigurationRow CurrentConfig)
            {
                try
                {
                    if (CurrentConfig.Islp_channels_group_idNull())
                        throw new ArgumentNullException("LoadProfileGroupId");

                    List<Configs.LoadProfileChannelsRow> SelectedWindows = new List<LoadProfileChannelsRow>();

                    Configs.LoadProfileChannelsRow[] Rows = (Configs.LoadProfileChannelsRow[])this.Select(String.Format("LoadProfileGroupId = {0}", CurrentConfig.lp_channels_group_id), "id,SequenceId asc");
                    SelectedWindows.AddRange(Rows);

                    return SelectedWindows;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            public List<Configs.LoadProfileChannelsRow> GetLoadProfileByGroup(Configs.LoadProfile_GroupRow CurrentConfig)
            {
                try
                {
                    List<Configs.LoadProfileChannelsRow> SelectedWindows = new List<LoadProfileChannelsRow>();

                    Configs.LoadProfileChannelsRow[] Rows = (Configs.LoadProfileChannelsRow[])this.Select(String.Format("LoadProfileGroupId = {0}", CurrentConfig.id), "id,SequenceId asc");
                    SelectedWindows.AddRange(Rows);

                    return SelectedWindows;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            //public List<Configs.LoadProfileChannelsRow> GetLoadProfileChannelsByGroup(Configs.LoadProfileChannel_GroupRow CurrentLPGroup, Configs.LoadProfile_GroupRow CurrentConfig)
            //{
            //    try
            //    {
            //        List<Configs.LoadProfileChannelsRow> SelectedWindows = new List<LoadProfileChannelsRow>();

            //        Configs.LoadProfileChannelsRow T_Row = null;

            //        // Add Channel_1
            //        Configs.LoadProfileChannelsRow[] Rows = (Configs.LoadProfileChannelsRow[])this.Select(String.Format("QuantityIndex = {0} AND  LoadProfileGroupId = {1}", CurrentLPGroup.Channel_1, CurrentConfig.id));
            //        T_Row = (Rows == null || Rows.Length <= 0) ? null : Rows[0];
            //        SelectedWindows.Add(T_Row);

            //        // Add Channel_2
            //        Rows = (Configs.LoadProfileChannelsRow[])this.Select(String.Format("QuantityIndex = {0} AND  LoadProfileGroupId = {1}", CurrentLPGroup.Channel_2, CurrentConfig.id));
            //        T_Row = (Rows == null || Rows.Length <= 0) ? null : Rows[0];
            //        SelectedWindows.Add(T_Row);

            //        // Add Channel_3
            //        Rows = (Configs.LoadProfileChannelsRow[])this.Select(String.Format("QuantityIndex = {0} AND  LoadProfileGroupId = {1}", CurrentLPGroup.Channel_3, CurrentConfig.id));
            //        T_Row = (Rows == null || Rows.Length <= 0) ? null : Rows[0];
            //        SelectedWindows.Add(T_Row);

            //        // Add Channel_4
            //        Rows = (Configs.LoadProfileChannelsRow[])this.Select(String.Format("QuantityIndex = {0} AND  LoadProfileGroupId = {1}", CurrentLPGroup.Channel_4, CurrentConfig.id));
            //        T_Row = (Rows == null || Rows.Length <= 0) ? null : Rows[0];
            //        SelectedWindows.Add(T_Row);

            //        return SelectedWindows;
            //    }
            //    catch
            //    {
            //        return null;
            //    }
            //}
        }

        partial class BillingItemsDataTable
        {
            public List<BillingItemsRow> GetBillingItemsByConfigurations(Configs.ConfigurationRow CurrentConfig)
            {
                try
                {
                    if (CurrentConfig.IsBillItemsGroupIdNull())
                        throw new ArgumentNullException("Billing Item GroupId");

                    DataRow[] BillingItemsRaw = this.Select(String.Format("BillItemGroupId = {0}", CurrentConfig.BillItemsGroupId), "id,SequenceId asc");
                    List<BillingItemsRow> BillingItems = new List<BillingItemsRow>();
                    BillingItems.AddRange((BillingItemsRow[])BillingItemsRaw);

                    return BillingItems;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public List<BillingItemsRow> GetBillingItemsByGroup(Configs.BillingItem_GroupRow CurrentConfig)
            {
                try
                {
                    DataRow[] BillingItemsRaw = this.Select(String.Format("BillItemGroupId = {0}", CurrentConfig.id), "id,SequenceId asc");
                    List<BillingItemsRow> BillingItems = new List<BillingItemsRow>();
                    BillingItems.AddRange((BillingItemsRow[])BillingItemsRaw);

                    return BillingItems;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public void RemoveBillingItemsByGroup(List<BillingItemsRow> BillingItems)
            {
                try
                {
                    foreach(BillingItemsRow row in BillingItems )
                    {
                        row.Delete();
                        //this.RemoveBillingItemsRow(row);
                        //AcceptChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public void CopyBillingItemsByGroup(BillingItemsRow BillingItem, BillingItem_GroupRow groupRow, int id)
            {
                try
                {

                    this.AddBillingItemsRow(BillingItem.Label, BillingItem.FormatSpecifier, BillingItem.Unit, BillingItem.Multiplier, BillingItem.SequenceId, groupRow,BillingItem.ConfigId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public int GetMaxId()
            {
                try
                {
                    return ((BillingItemsRow)this.Select("ID = MAX(ID)")[0]).id;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        partial class BillTariffQuantityDataTable
        {
     
            public void GetBillingItemsByConfigurations(Configs.ConfigurationRow CurrentConfig,
                List<BillingItemsRow> BillingItems,
                List<List<Configs.BillTariffQuantityRow>> BillingQuantityItems)
            {
                try
                {
                    foreach (BillingItemsRow BillItem in BillingItems)
                    {
                        DataRow[] BillQuantities = this.Select(String.Format("BillItemId = {0}", BillItem.id), "SequenceId asc");
                        List<Configs.BillTariffQuantityRow> BillItemQuan = new List<BillTariffQuantityRow>();
                        BillItemQuan.AddRange((Configs.BillTariffQuantityRow[])BillQuantities);
                        BillingQuantityItems.Add(BillItemQuan);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public void GetBillingItemsByGroup(Configs.BillingItem_GroupRow CurrentConfig,
                List<BillingItemsRow> BillingItems,
                List<List<Configs.BillTariffQuantityRow>> BillingQuantityItems)
            {
                try
                {
                    foreach (BillingItemsRow BillItem in BillingItems)
                    {
                        DataRow[] BillQuantities = this.Select(String.Format("BillItemId = {0}", BillItem.id), "SequenceId asc");
                        List<Configs.BillTariffQuantityRow> BillItemQuan = new List<BillTariffQuantityRow>();
                        BillItemQuan.AddRange((Configs.BillTariffQuantityRow[])BillQuantities);
                        BillingQuantityItems.Add(BillItemQuan);
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            public void RemoveBillingItemsByGroup(List<BillingItemsRow> BillingItems)
            {
                try
                {
                    foreach (BillingItemsRow BillItem in BillingItems)
                    {
                        DataRow[] BillQuantities = this.Select(String.Format("BillItemId = {0}", BillItem.id), "SequenceId asc");
                        List<Configs.BillTariffQuantityRow> BillItemQuan = new List<BillTariffQuantityRow>();
                        BillItemQuan.AddRange((Configs.BillTariffQuantityRow[])BillQuantities);
                        foreach (BillTariffQuantityRow row in BillItemQuan)
                        {
                            row.Delete();
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            public List<Configs.BillTariffQuantityRow> GetBillingTariffQuanitityByBillingItem(BillingItemsRow BillingItem)
            {
                try
                {

                    DataRow[] BillQuantities = this.Select(String.Format("BillItemId = {0}", BillingItem.id), "SequenceId asc");
                    List<Configs.BillTariffQuantityRow> BillItemQuan = new List<BillTariffQuantityRow>();
                    BillItemQuan.AddRange((Configs.BillTariffQuantityRow[])BillQuantities);

                    return BillItemQuan;

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

      

        partial class EventInfoDataTable
        {
            public void GetEventInfoByConfigurations(Configs.ConfigurationRow CurrentConfig, List<Configs.EventInfoRow> EventInfoItems)
            {
                try
                {
                    if (CurrentConfig.IsEventGroupIdNull())
                        throw new ArgumentNullException("Event GroupId");

                    DataRow[] EventItems = this.Select(String.Format("EventGroupId = {0} and EventNo >= 0", CurrentConfig.EventGroupId), "eventCode asc");
                    if (EventInfoItems != null)
                    {
                        EventInfoItems.Clear();
                        EventInfoItems.AddRange((Configs.EventInfoRow[])EventItems);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public void GetEventInfoByGroup(Configs.Events_GroupRow CurrentConfig, List<Configs.EventInfoRow> EventInfoItems)
            {
                try
                {
                    DataRow[] EventItems = this.Select(String.Format("EventGroupId = {0}", CurrentConfig.id), "eventCode asc");
                    if (EventInfoItems != null)
                    {
                        EventInfoItems.Clear();
                        EventInfoItems.AddRange((Configs.EventInfoRow[])EventItems);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public List<EventInfoRow> GetEventsByGroup(Configs.Events_GroupRow CurrentConfig)
            {
                try
                {
                    DataRow[] evetRow = this.Select(String.Format("EventGroupId = {0}", CurrentConfig.id));
                    List<EventInfoRow> events = new List<EventInfoRow>();
                    events.AddRange((EventInfoRow[])evetRow);

                    return events;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public void RemoveEventsByGroup(List<EventInfoRow> events)
            {
                try
                {
                    foreach(EventInfoRow row in events )
                    {
                        row.Delete();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public void CopyEventsByGroup(EventInfoRow eventRecord, Events_GroupRow groupRow, int id)
            {
                try
                {
                    this.AddEventInfoRow(eventRecord);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public int GetMaxId()
            {
                try
                {
                    return ((EventInfoRow)this.Select("ID = MAX(ID)")[0]).id;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        partial class EventLogsDataTable
        {
            public void GetEventLogInfoByConfigurations(Configs.ConfigurationRow CurrentConfig, List<Configs.EventLogsRow> EventLogInfoItems)
            {
                try
                {
                    if (CurrentConfig.IsEventGroupIdNull())
                        throw new ArgumentNullException("EventLog GroupId");

                    DataRow[] EventItems = this.Select(String.Format("EventGroupId = {0}", CurrentConfig.EventGroupId));
                    if (EventLogInfoItems != null)
                    {
                        EventLogInfoItems.Clear();
                        EventLogInfoItems.AddRange((Configs.EventLogsRow[])EventItems);

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public void GetEventLogInfoByGroup(Configs.Events_GroupRow CurrentConfig, List<Configs.EventLogsRow> EventLogInfoItems)
            {
                try
                {
                    DataRow[] EventItems = this.Select(String.Format("EventGroupId = {0}", CurrentConfig.id));

                    if (EventLogInfoItems != null)
                    {
                        EventLogInfoItems.Clear();
                        EventLogInfoItems.AddRange((Configs.EventLogsRow[])EventItems);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public void RemoveEventLogsByGroup(List<EventInfoRow> events)
            {
                try
                {
                    foreach (EventInfoRow eventRecord in events)
                    {
                        DataRow[] eventLogs = this.Select(String.Format("id = {0}", eventRecord.id));
                        List<Configs.EventLogsRow> eventLogData = new List<EventLogsRow>();
                        eventLogData.AddRange((Configs.EventLogsRow[])eventLogs);
                        foreach (EventLogsRow row in eventLogData)
                        {
                            row.Delete();
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            public List<Configs.EventLogsRow> GetEventLogsByEventInfo(EventInfoRow eventInfo)
            {
                try
                {

                    DataRow[] eventLogs = this.Select(String.Format("id = {0}", eventInfo.id));
                    List<Configs.EventLogsRow> eventLogData = new List<EventLogsRow>();
                    eventLogData.AddRange((Configs.EventLogsRow[])eventLogs);

                    return eventLogData;

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }


        public partial class OBIS_RightsDataTable
        {
            public List<Configs.OBIS_RightsRow> GetOBIS_Rihgts(int ObisRightGroupId)
            {
                try
                {
                    List<Configs.OBIS_RightsRow> OBIS_Rights = new List<Configs.OBIS_RightsRow>();

                    //[Rashad]EnumerableRowCollection<Configs.OBIS_RightsRow> Rows = this.Where(x => !x.IsObisRightGroupIdNull() &&
                    //[Rashad] ObisRightGroupId == x.ObisRightGroupId);
                    List<Configs.OBIS_RightsRow> Rows = this.Where(x => !x.IsObisRightGroupIdNull() &&
                                                                                      ObisRightGroupId == x.ObisRightGroupId).ToList<Configs.OBIS_RightsRow>();
                    
                    OBIS_Rights.AddRange(Rows);
                    return OBIS_Rights;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

        }

        public partial class RightsDataTable
        {
            public List<Configs.RightsRow> GetOBISCodeRights(Configs.OBIS_RightsRow OBISCode)
            {
                try
                {
                    List<Configs.RightsRow> OBIS_Rights = new List<Configs.RightsRow>();
                    Configs.RightsRow[] Rows = (Configs.RightsRow[])this.Select(String.Format("OBIS_Right_Id = {0}",
                       OBISCode.id), "type,SubId asc");
                    OBIS_Rights.AddRange(Rows);
                    return OBIS_Rights;
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("Unable to read OBIS Code Rights from {0}", (Get_Index)OBISCode.OBIS_Index));
                }
            }

            public List<Configs.RightsRow> GetOBISCodeRights(List<Configs.OBIS_RightsRow> OBISCodes)
            {
                try
                {
                    List<Configs.RightsRow> OBISRights = new List<RightsRow>();
                    List<Configs.RightsRow> Rights = null;
                    foreach (var OBISCode in OBISCodes)
                    {
                        Rights = GetOBISCodeRights(OBISCode);
                        OBISRights.AddRange(Rights);
                    }
                    return OBISRights;
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("Unable to read OBIS Code Rights,{0}", ex.Message));
                }
            }
        }

        public partial class OBIS_DetailsDataTable
        {
            public List<Configs.OBIS_DetailsRow> GetOBISDetailIdsByDeviceId(int DeviceId)
            {
                try
                {
                    List<Configs.OBIS_DetailsRow> OBIS_Details = new List<Configs.OBIS_DetailsRow>();
                    Configs.OBIS_DetailsRow[] Rows = (Configs.OBIS_DetailsRow[])this.Select(String.Format("Device_Id = {0}",
                       DeviceId), "Obis_Code asc");

                    if (Rows != null && Rows.Length > 0)
                        OBIS_Details.AddRange(Rows);

                    return OBIS_Details;
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("Unable to read OBIS Details From Rights Group Id {0}", (Get_Index)DeviceId));
                }
            }
        }

        public partial class AllQuantitiesDataTable
        {
            public decimal GetLabelIDByDefaultOBIS(ulong OBIS_Code)
            {
                List<AllQuantitiesRow> row = new List<AllQuantitiesRow>(this.Where(x => x.OBIS_Index == OBIS_Code));
                if (row == null || row.Count == 0) return 0;
                return row[0].OBIS_Index;
            }

            public AllQuantitiesRow FindByDefault_OBIS_Code(decimal OBIS_Code)
            {
                //[Rashad]EnumerableRowCollection<AllQuantitiesRow> row = this.Where(x => x.OBIS_Index == OBIS_Code);
                List<AllQuantitiesRow> row = this.Where(x => x.OBIS_Index == OBIS_Code).ToList<AllQuantitiesRow>();
                if (row == null) return null;
                List<AllQuantitiesRow> OBIS_Labels = new List<AllQuantitiesRow>();
                OBIS_Labels.AddRange(row);
                return OBIS_Labels[0];
            }
        }

        partial class CaptureObjectsDataTable
        {
            public List<Configs.CaptureObjectsRow> GetCaptureObjectByDevice(int deviceId)
            {
                try
                {
                    List<Configs.CaptureObjectsRow> CaptureObjs = new List<CaptureObjectsRow>();
                    Configs.CaptureObjectsRow[] TList = (Configs.CaptureObjectsRow[])this.Select(String.Format("DeviceId = {0}",
                        deviceId), "SequenceId,Target_OBIS_Index  asc");
                    CaptureObjs.AddRange(TList);
                    return CaptureObjs;
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("Unable to read capture objects details by Device,{0}", ex.Message));
                }
            }

            public List<Configs.CaptureObjectsRow> GetCaptureObjectByProfile(int deviceId, ulong Target_OBIS_Index, long? GroupId = null)
            {
                try
                {
                    List<Configs.CaptureObjectsRow> CaptureObjs = new List<CaptureObjectsRow>();

                    Configs.CaptureObjectsRow[] TList = null;
                    if (GroupId == null || GroupId <= 0)
                    {
                        TList = (Configs.CaptureObjectsRow[])this.Select(String.Format("DeviceId = {0} And Target_OBIS_Index = {1}",
                                                         deviceId, Target_OBIS_Index), "SequenceId  asc");
                    }
                    else
                    {
                        TList = (Configs.CaptureObjectsRow[])this.Select(String.Format("DeviceId = {0} And Target_OBIS_Index = {1} And GroupId = {2}",
                                                         deviceId, Target_OBIS_Index, GroupId), "SequenceId  asc");
                    }
                    
                    
                    CaptureObjs.AddRange(TList);
                    return CaptureObjs;
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("Unable to read capture objects details by Load Profile Group,{0}", ex.Message));
                }
            }
        }
    }

}
