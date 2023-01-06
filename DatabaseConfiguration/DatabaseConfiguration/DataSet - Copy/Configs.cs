using DatabaseConfiguration.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace DatabaseConfiguration.DataSet
{

    public partial class Configs
    {
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
                DBDAO.Load_All_Configurations(ref conf, connectionString);
                ///Find Default Meter & Default Configurations
                // this.Meter_Configuration.CurrentConfiguration = (this.Meter_Configuration.Count > 0) ? this.Meter_Configuration[0] : null;
                ///Select Configuration Based On MeterInfo Id
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
                DBDAO.Update_All_Configuration(this, connectionString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Commented Code Section

        /// <summary>
        /// Save previously loaded file
        /// </summary>
        //public void SaveConfigurations()
        //{
        //    try
        //    {
        //        FileInfo fileInfo = new FileInfo(FileSourceURL);
        //        if (!fileInfo.Exists)
        //            throw new Exception("Configuration file source not valid");

        //        fileInfo = null;
        //        SaveConfigurations(FileSourceURL);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void LoadConfigurations(string fileUrl)
        //{
        //    try
        //    {
        //        using (FileStream configFile = new FileStream(fileUrl, FileMode.Open, FileAccess.ReadWrite))
        //        {
        //            ///Clear Previous Data from Data SETs
        //            ///Clear Data From MeterTypeInfo Table will enforce to clear data from relevant
        //            ///data tables also,hence all data will be cleared
        //            DataRow[] dt = this.Meter_Configuration.Select();


        //            foreach (Configs.Meter_ConfigurationRow dtRow in dt)
        //            {
        //                this.Meter_Configuration.RemoveMeter_ConfigurationRow(dtRow);
        //            }
        //            foreach (DataTable dttable in this.Tables)
        //            {
        //                dttable.Clear();
        //            }
        //            ///Read Current Data From XML
        //            this.ReadXml(configFile);
        //            configFile.Close();
        //        }
        //        ///Find Default Meter & Default Configurations
        //        this.Meter_Configuration.CurrentConfiguration = (this.Meter_Configuration.Count > 0) ? this.Meter_Configuration[0] : null;
        //        ///Select Configuration Based On MeterInfo Id
        //        Configs.ConfigurationRow DefaultConfig = null;
        //        foreach (var item in Configuration)
        //        {
        //            if (DefaultConfig == null)
        //            {
        //                DefaultConfig = item;
        //                break;
        //            }
        //        }
        //        Configuration.CurrentConfiguration = DefaultConfig;
        //        this.fileURL = fileUrl;

        //        //Configs NewConfig = new Configs();
        //        //DateTime TimeStamp_0 = DateTime.Now;
        //        //ConfigurationDBAccessLayer DBDAO = new ConfigurationDBAccessLayer();
        //        //DBDAO.Load_All_Configurations(ref NewConfig);

        //        // DateTime TimeStamp_1 = DateTime.Now;
        //        // Console.Out.WriteLine("Time Duration 1::{0}", TimeStamp_1 - TimeStamp_0);
        //        // NewConfig.Configuration[0].LastModified = DateTime.Now;
        //        // DBDAO.Update_All_Configuration( NewConfig);
        //        // DateTime TimeStamp_2 = DateTime.Now;
        //        // Console.Out.WriteLine("Time Duration 2::{0}", TimeStamp_2 - TimeStamp_1);
        //        // DBDAO.UpdateMeterConfigurations( NewConfig);

        //        ///End Loading Rights
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void SaveConfigurations(string fileUrl)
        //{
        //    try
        //    {
        //        using (FileStream configFile = new FileStream(fileUrl, FileMode.Create, FileAccess.ReadWrite))
        //        {
        //            //if (this.Configuration.CurrentConfiguration != null)
        //            //    this.Configuration.CurrentConfiguration.LastModified = DateTime.Now;
        //            this.WriteXml(configFile, XmlWriteMode.IgnoreSchema);
        //            ///Find Default Meter & Default Configurations
        //            configFile.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}



        //partial class Meter_InfoDataTable
        //{
        //    private Configs.Meter_InfoRow currentMeterInfo;

        //    public Configs.Meter_InfoRow CurrentMeterInfo
        //    {
        //        get { return currentMeterInfo; }
        //        set { currentMeterInfo = value; }
        //    }

        //    public List<Configs.Meter_InfoRow> GetAllMeterConfigs()
        //    {
        //        DataRow[] Meters = this.Select("true", "id,Model_id desc");
        //        return new List<Configs.Meter_InfoRow>((Configs.Meter_InfoRow[])Meters);
        //    }

        //    public Configs.Meter_InfoRow GetMeter(int model_id)
        //    {
        //        Configs.Meter_InfoRow[] Meters = (Configs.Meter_InfoRow[])this.Select(
        //            String.Format("Model_id = '{0}'", model_id), "id,Model_id desc");
        //        if (Meters != null && Meters.Length > 0)
        //            return Meters[0];
        //        else
        //            return null;
        //    }

        //    public Configs.Meter_InfoRow GetMeter(int model_id, int manufecturerId)
        //    {
        //        Configs.Meter_InfoRow[] Meters = (Configs.Meter_InfoRow[])this.Select(
        //            String.Format("Model_id = '{0}' and Manufacturer_id = '{1}'", model_id, manufecturerId), "id,Model_id desc");
        //        if (Meters != null && Meters.Length > 0)
        //            return Meters[0];
        //        else
        //            return null;
        //    }
        //    public Configs.Meter_InfoRow GetMeterById(int id)
        //    {
        //        Configs.Meter_InfoRow[] Meters = (Configs.Meter_InfoRow[])this.Select(
        //            String.Format("id = '{0}'", id), "id,Model_id desc");
        //        if (Meters != null && Meters.Length > 0)
        //            return Meters[0];
        //        else
        //            return null;
        //    }
        //}

        //partial class Meter_ConfigurationDataTable
        //{
        //    private Configs.Meter_ConfigurationRow currentConfiguration;

        //    public Configs.Meter_ConfigurationRow CurrentConfiguration
        //    {

        //        get { return currentConfiguration; }
        //        set { currentConfiguration = value; }
        //    }


        //    public Configs.Meter_ConfigurationRow[] GetConfigurationsByIdOrFirmwareVersion(int id, string firmwareVersion)
        //    {
        //        Configs.Meter_ConfigurationRow[] AllConfigs = (Configs.Meter_ConfigurationRow[])this.Select(String.Format("Meter_Info_id = {0} and (Firmware_Version is null or Firmware_Version like '%{1}%' or Firmware_Version <= '{1}') ", id, firmwareVersion), "Firmware_Version desc");

        //        return AllConfigs;
        //    }


        //    public Configs.Meter_ConfigurationRow GetConfigurationById(int id, string version)
        //    {
        //        try
        //        {
        //            Configs.Meter_ConfigurationRow[] AllConfigs = GetConfigurationsByIdOrFirmwareVersion(id, version);
        //            Configs.Meter_ConfigurationRow Configuration = null;

        //            if (AllConfigs != null &&
        //                AllConfigs.Length > 0)
        //            {
        //                Configuration = AllConfigs[0];
        //                foreach (var config in AllConfigs)
        //                {
        //                    if (Configuration != config)
        //                    {
        //                        if (Configuration != null)
        //                        {
        //                            Configuration = config;
        //                            break;
        //                        }
        //                        else
        //                            continue;
        //                    }
        //                }
        //            }
        //            return Configuration;
        //        }
        //        catch (Exception)
        //        {
        //            return null;
        //        }
        //    }
        //}

        // partial class Sap_ConfigDataTable
        // {
        //     public List<Configs.Sap_ConfigRow> GetAllSAPInfoInfo()
        //     {
        //         try
        //         {
        //             List<Configs.Sap_ConfigRow> SelectedSAPs = new List<Configs.Sap_ConfigRow>();

        //             Configs.Sap_ConfigRow[] Rows = (Configs.Sap_ConfigRow[])this.Select("1=1");
        //             SelectedSAPs.AddRange(Rows);
        //             return SelectedSAPs;
        //         }
        //         catch (Exception ex)
        //         {
        //             return null;
        //         }
        //     }

        //     public List<Configs.Sap_ConfigRow> GetSAPInfoByMeterConfiguration(Configs.Meter_ConfigurationRow currentMeterInfo)
        //     {
        //         try
        //         {
        //             List<Configs.Sap_ConfigRow> SelectedSAPs = new List<Configs.Sap_ConfigRow>();

        //             Configs.Sap_ConfigRow[] Rows = (Configs.Sap_ConfigRow[])this.Select(String.Format("Meter_Config_id = {0}",
        //                 currentMeterInfo.id), "id asc");
        //             SelectedSAPs.AddRange(Rows);
        //             return SelectedSAPs;
        //         }
        //         catch (Exception ex)
        //         {
        //             return null;
        //         }
        //     }
        //     public Configs.Sap_ConfigRow GetSAPRowByAddress(uint meter_sap, uint client_sap)
        //     {
        //         try
        //         {

        //             Configs.Sap_ConfigRow[] Rows = (Configs.Sap_ConfigRow[])this.Select(String.Format("Meter_Sap = {0} and Client_Sap={1}",
        //                 meter_sap, client_sap), "id asc");
        //             if (Rows != null && Rows.Length > 0) return Rows[0];
        //             return null;
        //         }
        //         catch (Exception ex)
        //         {
        //             return null;
        //         }
        //     }
        // }

        #endregion

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
                    List<Configs.DisplayWindowsRow> SelectedWindows = new List<DisplayWindowsRow>();

                    foreach (var item in this.Rows)
                    {
                        if (((Configs.DisplayWindowsRow)item).DisplayWindowsGroupId == CurrentConfig.DisplayWindowGroupId)
                            SelectedWindows.Add(((Configs.DisplayWindowsRow)item));
                    }
                    /// Sort List Based On Sequence Id

                    /// SelectedWindows.Sort((x, y) => x.SequenceId.CompareTo(y.SequenceId));
                    /// SelectedWindows.Sort((x,y)=>  x.SequenceId.CompareTo(y.SequenceId));

                    /// Configs.DisplayWindowsRow[] Rows = (Configs.DisplayWindowsRow[])this.Select(String.Format("ConfigId = {0}", CurrentConfig.id));
                    /// SelectedWindows.AddRange(Rows);

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
        }

        partial class BillingItemsDataTable
        {
            public List<BillingItemsRow> GetBillingItemsByConfigurations(Configs.ConfigurationRow CurrentConfig)
            {
                try
                {
                    DataRow[] BillingItemsRaw = this.Select(String.Format("BillItemGroupId = {0}", CurrentConfig.BillItemsGroupId), "SequenceId asc");
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
                    foreach (BillingItemsRow row in BillingItems)
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

                    this.AddBillingItemsRow(id, BillingItem.Label, BillingItem.FormatSpecifier, BillingItem.Unit, BillingItem.Multiplier, BillingItem.SequenceId, groupRow);
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
                    DataRow[] EventItems = this.Select(String.Format("EventGroupId = {0}", CurrentConfig.EventGroupId), "eventCode asc");
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
                    foreach (EventInfoRow row in events)
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
                    this.AddEventInfoRow(id, eventRecord.EventCode, eventRecord.Label, eventRecord.MaxEventCount, eventRecord.EventNo, groupRow, eventRecord.CautionNumber, eventRecord.FlashTime);
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

                    //foreach (Configs.OBIS_RightsRow row in this)
                    //{
                    //    if (ids.Contains(row.Obis_Detail_Id))
                    //    {
                    //        OBIS_Rights.Add(row);
                    //    }
                    //}

                    EnumerableRowCollection<Configs.OBIS_RightsRow> Rows = this.Where(x => ObisRightGroupId == x.ObisRightGroupId);
                    OBIS_Rights.AddRange(Rows);
                    return OBIS_Rights;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            // public List<Configs.OBIS_RightsRow> GetOBIS_Rihgts(List<int> DetailIds)
            // {
            //     try
            //     {
            //         List<Configs.OBIS_RightsRow> OBIS_Rights = new List<Configs.OBIS_RightsRow>();
            //         Configs.OBIS_RightsRow[] Rows = this.Where(x => ids.Contains(x.Obis_Detail_Id));
            //         OBIS_Rights.AddRange(Rows);
            //         return OBIS_Rights;
            //     }
            //     catch (Exception ex)
            //     {
            //         return null;
            //     }
            // }
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
                    throw new Exception(String.Format("Unable to read OBIS Code Rights from {0}", OBISCode.OBIS_Index));
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
                    throw new Exception(String.Format("Unable to read OBIS Details From Rights Group Id {0}",DeviceId));
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
                EnumerableRowCollection<AllQuantitiesRow> row = this.Where(x => x.OBIS_Index == OBIS_Code);
                if (row == null) return null;
                List<AllQuantitiesRow> OBIS_Labels = new List<AllQuantitiesRow>();
                OBIS_Labels.AddRange(row);
                return OBIS_Labels[0];
            }
        }
    }

}
