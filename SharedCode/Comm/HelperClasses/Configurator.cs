using DatabaseConfiguration.DataBase;
using DatabaseConfiguration.DataSet;
using DLMS;
using DLMS.Comm;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.Param;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SharedCode.Comm.HelperClasses
{
    public class Configurator
    {
        #region Data_Members

        public static readonly string DefaultDevice = "Fusion";
        public static readonly string DefaultManufacturer = "MTI";
        public static readonly string DefaultMeterModel = "R326";

        private ConfigsHelper configurationHelper;
        private Configs loadedConfiguration;
        private ConcurrentDictionary<string, object> loadedConfigs;
        private TimeSpan lastConfigRefreshTime = TimeSpan.MinValue;
        private TimeSpan maxConfigRefreshTime = TimeSpan.MaxValue;
        public static readonly TimeSpan DefaultMaxConfigRefreshTime = TimeSpan.FromMinutes(05.0f);
        private Timer timerCheckIfRefreshRequired = new Timer();

        #endregion

        #region Properties
        public ConfigsHelper ConfigurationHelper
        {
            get { return configurationHelper; }
            set { configurationHelper = value; }
        }

        public Configs LoadedConfiguration
        {
            get { return loadedConfiguration; }
            set { loadedConfiguration = value; }
        }

        public ConcurrentDictionary<string, object> LoadedConfigs
        {
            get { return loadedConfigs; }
        }

        public TimeSpan LastConfigRefreshTime
        {
            get { return lastConfigRefreshTime; }
        }

        public TimeSpan MaxConfigRefreshTime
        {
            get { return maxConfigRefreshTime; }
            set
            {
                if (value <= DefaultMaxConfigRefreshTime)
                    maxConfigRefreshTime = DefaultMaxConfigRefreshTime;
                else
                    maxConfigRefreshTime = value;
            }
        }

        //public bool IsConfigRefreshRequired
        //{
        //    get
        //    {
        //        try
        //        {
        //            if (DateTime.Now.TimeOfDay.Subtract(LastConfigRefreshTime) > MaxConfigRefreshTime)
        //                return true;
        //            else
        //                return false;
        //        }
        //        catch (Exception)
        //        {
        //            return true;
        //        }
        //    }
        //}

        public bool IsConfigRefreshRequired
        {
            get;
            set;
        }
        #endregion

        #region Default Constructor

        public Configurator()
        {
            IsConfigRefreshRequired = true;
            loadedConfigs = new ConcurrentDictionary<string, object>();
            loadedConfiguration = new Configs();
            lastConfigRefreshTime = TimeSpan.MinValue;
            maxConfigRefreshTime = DefaultMaxConfigRefreshTime;
            timerCheckIfRefreshRequired.Interval = 5000;
            timerCheckIfRefreshRequired.Elapsed += new ElapsedEventHandler(timerCheckIfRefreshRequired_Elapsed);
            timerCheckIfRefreshRequired.Enabled = true;
        }

        #endregion

        #region Default Destructor

        ~Configurator()
        {
            timerCheckIfRefreshRequired.Enabled = false;
            timerCheckIfRefreshRequired.Stop();
            timerCheckIfRefreshRequired.Elapsed -= new ElapsedEventHandler(timerCheckIfRefreshRequired_Elapsed);

            configurationHelper = null;
            loadedConfiguration = null;
            loadedConfigs = null;
        }

        #endregion

        #region Member_Methods
        private void timerCheckIfRefreshRequired_Elapsed(object sender, ElapsedEventArgs args)
        {
            try
            {

                timerCheckIfRefreshRequired.Enabled = false;
                timerCheckIfRefreshRequired.Stop();
                if (!IsConfigRefreshRequired)
                    IsConfigRefreshRequired = true;//_dbController.IsConfigurationRefreshRequired();
            }
            catch (Exception)
            {

                //throw;
            }
            finally
            {

                if (!timerCheckIfRefreshRequired.Enabled)
                {
                    timerCheckIfRefreshRequired.Enabled = true;
                    timerCheckIfRefreshRequired.Start();
                }
            }
        }


        public void LoadConfiguration(Configs configDataSet)
        {
            try
            {
                this.ConfigurationHelper.DAL.Load_All_Configurations(configDataSet);
                // Find Default Meter & Default Configurations

                var Manufacturer_Row = (configDataSet.Manufacturer.Count > 0) ? configDataSet.Manufacturer[0] : null;
                Configs.DeviceRow Device_Row = null;
                Configs.Device_AssociationRow Association_Row = null;

                if (Manufacturer_Row != null)
                {
                    var devs = Manufacturer_Row.GetDeviceRows();
                    Device_Row = (devs != null && devs.Length > 0) ? devs[0] : null;
                }

                if (Device_Row != null)
                {
                    var assoc_rows = Device_Row.GetDevice_AssociationRows();
                    Association_Row = (assoc_rows != null && assoc_rows.Length > 0) ? assoc_rows[0] : null;
                }

                // Select Configuration Based On MeterInfo Id
                Configs.ConfigurationRow DefaultConfig = null;
                if (Association_Row != null)
                {
                    DefaultConfig = Association_Row.ConfigurationRow;
                }

                configDataSet.Configuration.CurrentConfiguration = DefaultConfig;
            }
            catch (Exception ex)
            {
                IsConfigRefreshRequired = false;
                throw ex;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ResetConfiguration()
        {
            // AHMED ASHRAF
            try
            {
                lock (this)
                {
                    loadedConfigs = new ConcurrentDictionary<string, object>();
                    loadedConfiguration = new Configs();
                    LoadConfiguration(loadedConfiguration);
                    lastConfigRefreshTime = DateTime.Now.TimeOfDay;
                    //  timerCheckIfRefreshRequired.Enabled=true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    IsConfigRefreshRequired = false;
                    //_dbController.UpdateConfigurationRefreshFlag(0);
                }
                catch (Exception)
                { }
            }
        }

        public static string GetSAPTableKey(ConnectionInfo ConnInfo)
        {
            string keySAP = "default_KEYSAP";
            int device = -1;
            string meterModel = DefaultMeterModel;
            string manufacturer = DefaultManufacturer;
            int OBISRightGroup = -1;

            try
            {
                if (ConnInfo.IsInitialized)
                {
                    device = ConnInfo.MeterInfo.Device.Id;
                    manufacturer = ConnInfo.MeterInfo.Device.ManufacturerId.ToString();
                    meterModel = ConnInfo.MeterInfo.MeterModel;
                    OBISRightGroup = ConnInfo.MeterInfo.Device_Association.RightGroupId;

                    keySAP = string.Format("SAPKEY_{0}{1}{2}{3}", manufacturer, device, meterModel, OBISRightGroup);
                }

                return keySAP;
            }
            catch (Exception ex)
            {
                throw new Exception("GetSAPTableKey," + ex.Message, ex);
            }
        }

        public String GetSAPTableKey(ConnectionInfo ConfInfo, int dummyParam = 1)
        {
            try
            {
                return Configurator.GetSAPTableKey(ConfInfo);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Meter_Configurations

        public void GetMeterConnectionInfo(IOConnection IOConnObj, MeterSerialNumber MSN, int deviceAssociationId)
        {
            try
            {
                //IOConnObj.MeterSerialNumberObj = MSN;

                MeterConfig meterConf = GetMeterConfiguration(deviceAssociationId);
                ConnectionInfo confInfo = new ConnectionInfo();

                // Initialize New Meter Con fig Container
                confInfo.MeterInfo = new MeterConfig(meterConf);
                IOConnObj.ConnectionInfo = confInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MeterConfig GetMeterConfiguration(int DeviceAssociationId)
        {
            try
            {
                string Key = "GetMeterConfig_" + DeviceAssociationId;

                MeterConfig Configs = null;
                try
                {
                    if (IsConfigRefreshRequired)
                    {
                        Configs = GetMeterConfigurationHelper(DeviceAssociationId);
                    }
                    else
                        Configs = (MeterConfig)LoadedConfigs[Key];
                }
                catch (Exception) { }
                finally
                {
                    if (Configs == null)
                        Configs = GetMeterConfigurationHelper(DeviceAssociationId);
                }
                return Configs;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while loading Meter Config", ex);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        internal MeterConfig GetMeterConfigurationHelper(int DeviceAssociationId)
        {
            try
            {
                string Key = "GetMeterConfig_" + DeviceAssociationId;
                object Meter_Info_Raw = null;
                MeterConfig Meter_Infos = null;

                if (IsConfigRefreshRequired)
                {
                    ResetConfiguration();
                }

                {
                    LoadedConfigs.TryGetValue(Key, out Meter_Info_Raw);

                    if (Meter_Info_Raw != null)
                        Meter_Infos = (MeterConfig)Meter_Info_Raw;
                    else
                    {
                        LoadedConfigs.TryRemove(Key, out Meter_Info_Raw);
                        Meter_Infos = ConfigurationHelper.GetMeterConfigurationByAssociationId(DeviceAssociationId);
                        LoadedConfigs.TryAdd(Key, Meter_Infos);
                    }
                }
                return Meter_Infos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while loading Meter Configure", ex);
            }
        }

        #endregion

        #region Meter Access Rights Configurations

        public SAPTable GetAccessRights(ConnectionInfo ConnectionInfo)
        {
            SAPTable SPTable = null;
            string keySAP = "Default_SAPTable";
            try
            {
                keySAP = GetSAPTableKey(ConnectionInfo);
                try
                {
                    if (IsConfigRefreshRequired)
                    {
                        SPTable = GetAccessRightsHelper(ConnectionInfo);
                    }
                    else
                        SPTable = (SAPTable)LoadedConfigs[keySAP];
                }
                catch (Exception) { }
                finally
                {
                    if (SPTable == null ||
                        (SPTable.OBISLabelLookup == null || SPTable.OBISLabelLookup.Count > 0) ||
                        (SPTable.SapTable == null || SPTable.SapTable.Count > 0))
                        SPTable = GetAccessRightsHelper(ConnectionInfo);
                }
                return SPTable;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while loading meter Access Rights Table", ex);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        internal SAPTable GetAccessRightsHelper(ConnectionInfo ConnectionInfo)
        {
            string keySAP = string.Empty;
            int deviceId = -1;
            int obisGroupId = -1;

            SAPTable CurrentSAPTable = null;
            Object SAP_Table_Obj = null;
            try
            {
                keySAP = "default_SAPKEY";
                keySAP = GetSAPTableKey(ConnectionInfo);

                if (ConnectionInfo.IsInitialized)
                {
                    deviceId = ConnectionInfo.MeterInfo.Device.Id;
                    obisGroupId = ConnectionInfo.MeterInfo.Device_Association.RightGroupId;
                }

                if (IsConfigRefreshRequired)
                {
                    ResetConfiguration();
                }

                {
                    CurrentSAPTable = null;
                    LoadedConfigs.TryGetValue(keySAP, out SAP_Table_Obj);

                    if (SAP_Table_Obj != null && ((SAPTable)SAP_Table_Obj).SapTable.Count > 0)
                        CurrentSAPTable = (SAPTable)SAP_Table_Obj;
                    else
                    {
                        LoadedConfigs.TryRemove(keySAP, out SAP_Table_Obj);

                        List<OBISCodeRights> rights = ConfigurationHelper.GetCurrentSAPOBISRights(obisGroupId);
                        // Update OBISLabel Lookup
                        List<KeyValuePair<StOBISCode, StOBISCode>> OBISLabelLookup = ConfigurationHelper.GetOBISCodesMapByDeviceId(deviceId);

                        CurrentSAPTable = new SAPTable();
                        // Populate OBIS Label Lookup
                        if (OBISLabelLookup != null && OBISLabelLookup.Count > 0)
                            CurrentSAPTable.AddRangeOBISCode(OBISLabelLookup);
                        // Populate OBIS Code Rights
                        if (rights != null && rights.Count > 0)
                            CurrentSAPTable.AddRangeOBISRights(rights);

                        LoadedConfigs.TryAdd(keySAP, CurrentSAPTable);
                    }
                }

                if (CurrentSAPTable == null)
                    throw new Exception("Invalid SAP Table Structure");
                return CurrentSAPTable;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while loading meter Access Rights Table _1", ex);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SaveOBISCodeRights(ConnectionInfo ConnectionInfo, List<OBISCodeRights> OBISCodeRights)
        {
            string keySAP = string.Empty;

            Object SAP_Table_Obj;
            try
            {
                keySAP = GetSAPTableKey(ConnectionInfo);
                int deviceId = -1;
                int obisGroupId = -1;

                if (ConnectionInfo.IsInitialized)
                {
                    deviceId = ConnectionInfo.MeterInfo.Device.Id;
                    obisGroupId = ConnectionInfo.MeterInfo.Device_Association.RightGroupId;
                }

                try
                {
                    ConfigurationHelper.SaveCurrentSAPOBISRights(obisGroupId, deviceId, OBISCodeRights);

                    // Save Read OBIS Rights To Backings Store
                    try
                    {
                        //ConfigurationDBAccessLayer ConfigDBAccessLayer = new ConfigurationDBAccessLayer();
                        //ConfigDBAccessLayer.Update_AccessRights(ConfigurationHelper.LoadedConfigurations);

                        // OBISRithtsDAO OBIS_RightDAO = new OBISRithtsDAO();
                        // RithtsDAO Rights_DAO = new RithtsDAO(OBIS_RightDAO.DataBaseConnection);
                        // OBISDetailsDAO OBISCodeMAPDAO = new OBISDetailsDAO(OBIS_RightDAO.DataBaseConnection);

                        // OBIS_RightDAO.AcceptChangesOBIS_Rights(ConfigurationHelper.LoadedConfigurations);
                        // Rights_DAO.AcceptChanges_Rights(ConfigurationHelper.LoadedConfigurations);
                        // OBISCodeMAPDAO.AcceptChangesObisDetails(ConfigurationHelper.LoadedConfigurations);

                        // ConfigurationHelper.LoadedConfigurations.OBIS_Rights.AcceptChanges();
                        // ConfigurationHelper.LoadedConfigurations.OBIS_Details.AcceptChanges();
                    }
                    catch (Exception ex)
                    {
                        // Discard Previous Modification
                        ConfigurationHelper.LoadedConfigurations.OBIS_Rights.RejectChanges();
                        ConfigurationHelper.LoadedConfigurations.Rights.RejectChanges();
                        ConfigurationHelper.LoadedConfigurations.OBIS_Details.RejectChanges();
                        throw ex;
                    }

                    LoadedConfigs.TryRemove(keySAP, out SAP_Table_Obj);
                    SAPTable CurrentSAPTable = new SAPTable();
                    CurrentSAPTable.AddRangeOBISRights(OBISCodeRights);
                    LoadedConfigs.TryAdd(keySAP, CurrentSAPTable);
                }
                catch (Exception ex) { throw ex; }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred saving OBIS Code Rights Objects"), ex);
            }
        }

        #endregion

        #region Load Profile Channels Configurations

        public List<LoadProfileChannelInfo> GetMeterLoadProfileChannels(ConnectionInfo ConnectionInfo)
        {
            List<LoadProfileChannelInfo> ChannelInfo = null;
            int configId = -1;
            int device = -1;
            string meterModel = DefaultMeterModel;
            string manufacturer = DefaultManufacturer;

            string keyLPChannels = "Default_LPChannels";
            try
            {
                if (ConnectionInfo.IsInitialized)
                {
                    manufacturer = ConnectionInfo.MeterInfo.Device.ManufacturerId.ToString();
                    device = ConnectionInfo.MeterInfo.Device.Id;
                    configId = ConnectionInfo.MeterInfo.Device_Configuration.Id;
                    meterModel = ConnectionInfo.MeterInfo.MeterModel;

                    keyLPChannels = string.Format("LP_Channels_{0}{1}{2}", manufacturer, device, configId);
                }

                try
                {
                    if (IsConfigRefreshRequired)
                    {
                        ChannelInfo = GetMeterLoadProfileChannelsHelper(ConnectionInfo);
                    }
                    else
                        ChannelInfo = (List<LoadProfileChannelInfo>)LoadedConfigs[keyLPChannels];
                }
                catch (Exception) { }
                finally
                {
                    if (ChannelInfo == null)
                        ChannelInfo = GetMeterLoadProfileChannelsHelper(ConnectionInfo);
                }

                return ChannelInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while loading meter load profile channels info", ex);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        internal List<LoadProfileChannelInfo> GetMeterLoadProfileChannelsHelper(ConnectionInfo ConnectionInfo)
        {
            List<LoadProfileChannelInfo> ChannelInfo = null;
            Object LoadProfile_Obj_Raw = null;
            string keyLPChannels = "Default_LPChannels";

            int configId = -1;
            int device = -1;
            string meterModel = DefaultMeterModel;
            string manufacturer = DefaultManufacturer;

            try
            {
                if (ConnectionInfo.IsInitialized)
                {
                    device = ConnectionInfo.MeterInfo.Device.Id;
                    manufacturer = ConnectionInfo.MeterInfo.Device.ManufacturerId.ToString();
                    meterModel = ConnectionInfo.MeterInfo.MeterModel;
                    configId = ConnectionInfo.MeterInfo.Device_Configuration.Id;

                    keyLPChannels = string.Format("LP_Channels_{0}{1}{2}", manufacturer, device, configId);
                }

                if (IsConfigRefreshRequired)
                {
                    ResetConfiguration();
                }

                {
                    LoadedConfigs.TryGetValue(keyLPChannels, out LoadProfile_Obj_Raw);
                    if (LoadProfile_Obj_Raw != null)
                    {
                        ChannelInfo = (List<LoadProfileChannelInfo>)LoadProfile_Obj_Raw;
                    }
                    else
                    {
                        ulong LPGroupId = Convert.ToUInt64(ConnectionInfo.MeterInfo.Device_Configuration.LoadProfileGroupId);

                        ChannelInfo = ConfigurationHelper.GetAllSelectableLoadProfileChannels(configId);
                        LoadedConfigs.TryRemove(keyLPChannels, out LoadProfile_Obj_Raw);
                        LoadedConfigs.TryAdd(keyLPChannels, ChannelInfo);
                    }
                }

                if (ChannelInfo == null || ChannelInfo.Count <= 0)
                    throw new Exception("Invalid Structure LoadProfile Channels");
                return ChannelInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while loading meter load profile channels info", ex);
            }
        }

        #endregion

        #region Load Profile Channel Group

        internal void GetMeterGroupByLoadProfileChannels(ConnectionInfo ConnectionInfo, List<LoadProfileChannelInfo> LP_Channel_Group, out uint? GroupId)
        {
            int device = -1;
            string meterModel = DefaultMeterModel;
            string manufacturer = DefaultManufacturer;

            string keyLPChannels = "Default_LPChannelsGroup";
            GroupId = null;
            try
            {
                if (ConnectionInfo.IsInitialized)
                {
                    device = ConnectionInfo.MeterInfo.Device.Id;
                    manufacturer = ConnectionInfo.MeterInfo.Device.ManufacturerId.ToString();
                    meterModel = ConnectionInfo.MeterInfo.MeterModel;

                    keyLPChannels = string.Format("LP_ChannelsGroup_{0}{1}{2}{3}", manufacturer, device, meterModel, GroupId);
                }

                if (LP_Channel_Group == null || LP_Channel_Group.Count <= 0)
                    GroupId = null;
                //try
                //{
                //    // Update Load Profile Group
                //    ConfigurationHelper.GetMeterLoadProfileChannelGroup(LP_Channel_Group, out GroupId);
                //}
                //catch (Exception)
                //{ }
            }
            catch (Exception) { }
        }

        //public List<LoadProfileChannelInfo> GetMeterLoadProfileChannelsByGroup(ConnectionInfo ConnectionInfo, long? GroupId = null)
        //{
        //    List<LoadProfileChannelInfo> ChannelInfo = null;

        //    int device = -1;
        //    string meterModel = DefaultMeterModel;
        //    string manufacturer = DefaultManufacturer;

        //    string keyLPChannels = "Default_LPChannelsGroup";
        //    try
        //    {
        //        if (ConnectionInfo.IsInitialized)
        //        {
        //            device = ConnectionInfo.MeterInfo.Device.Id;
        //            manufacturer = ConnectionInfo.MeterInfo.Device.ManufacturerId.ToString();
        //            meterModel = ConnectionInfo.MeterInfo.MeterModel;

        //            keyLPChannels = string.Format("LP_ChannelsGroup_{0}{1}{2}{3}", manufacturer, device, meterModel, GroupId);
        //        }
        //        try
        //        {
        //            if (IsConfigRefreshTimeExpired)
        //            {
        //                ChannelInfo = GetMeterLoadProfileChannelsHelper(ConnectionInfo);
        //            }
        //            else
        //                ChannelInfo = (List<LoadProfileChannelInfo>)loadedConfigs[keyLPChannels];
        //        }
        //        catch (Exception) { }
        //        finally
        //        {
        //            if (ChannelInfo == null)
        //                ChannelInfo = GetMeterLoadProfileChannelsByGroupHelper(ConnectionInfo, GroupId);
        //        }
        //        return ChannelInfo;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error occurred while loading meter load profile channels group info", ex);
        //    }
        //}

        //[MethodImpl(MethodImplOptions.Synchronized)]
        //internal List<LoadProfileChannelInfo> GetMeterLoadProfileChannelsByGroupHelper(ConnectionInfo ConnectionInfo, long? GroupId = null)
        //{
        //    List<LoadProfileChannelInfo> ChannelInfo = null;
        //    Object LoadProfile_Obj_Raw = null;

        //    int configId = -1;
        //    int device = -1;
        //    string meterModel = DefaultMeterModel;
        //    string manufacturer = DefaultManufacturer;

        //    string keyLPChannels = "Default_LPChannelsGroup";
        //    try
        //    {
        //        if (ConnectionInfo.IsInitialized)
        //        {
        //            device = ConnectionInfo.MeterInfo.Device.Id;
        //            manufacturer = ConnectionInfo.MeterInfo.Device.ManufacturerId.ToString();
        //            meterModel = ConnectionInfo.MeterInfo.MeterModel;
        //            configId = ConnectionInfo.MeterInfo.Device_Configuration.Id;

        //            keyLPChannels = string.Format("LP_ChannelsGroup_{0}{1}{2}{3}", manufacturer, device, meterModel, GroupId);
        //        }

        //        if (IsConfigRefreshTimeExpired)
        //        {
        //            ChannelInfo = GetMeterLoadProfileChannelsHelper(ConnectionInfo);
        //        }
        //        // else
        //        {
        //            LoadedConfigs.TryGetValue(keyLPChannels, out LoadProfile_Obj_Raw);
        //            if (LoadProfile_Obj_Raw != null)
        //            {
        //                ChannelInfo = (List<LoadProfileChannelInfo>)LoadProfile_Obj_Raw;
        //            }
        //            else
        //            {
        //                ChannelInfo = ConfigurationHelper.GetMeterProfileChannelsInfo((ulong)GroupId, configId);
        //                LoadedConfigs.TryRemove(keyLPChannels, out LoadProfile_Obj_Raw);
        //                LoadedConfigs.TryAdd(keyLPChannels, ChannelInfo);
        //            }
        //        }

        //        if (ChannelInfo == null || ChannelInfo.Count <= 0)
        //            throw new Exception("Invalid Structure LoadProfile Channels");
        //        return ChannelInfo;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error occurred while loading meter load profile channels group info", ex);
        //    }
        //}

        //[MethodImpl(MethodImplOptions.Synchronized)]
        //internal void InsertOrUpdateLoadProfileChannelGroupHelper(ConnectionInfo ConnectionInfo, List<LoadProfileChannelInfo> LP_Channel_Group, out uint? GroupId)
        //{
        //    string device = DefaultDevice;
        //    string meterModel = DefaultMeterModel;
        //    string manufacturer = DefaultManufacturer;

        //    string keyLPChannels = "Default_LPChannelsGroup";
        //    GroupId = null;

        //    try
        //    {
        //        if (ConnectionInfo.IsInitialized)
        //        {
        //            device = ConnectionInfo.MeterInfo.Device.DeviceName;
        //            manufacturer = ConnectionInfo.MeterInfo.Device.ManufacturerId.ToString();
        //            meterModel = ConnectionInfo.MeterInfo.MeterModel;

        //            keyLPChannels = string.Format("LP_ChannelsGroup_{0}{1}{2}{3}", manufacturer, device, meterModel, GroupId);
        //        }
        //        if (LP_Channel_Group == null || LP_Channel_Group.Count <= 0)
        //            throw new Exception("Invalid Structure LoadProfile Channels");
        //        try
        //        {
        //            //// Update Load Profile Group
        //            //ConfigurationHelper.GetMeterLoadProfileChannelGroup(LP_Channel_Group, out GroupId);
        //        }
        //        catch (Exception)
        //        { }
        //        if (GroupId == null)
        //        {
        //            ConfigurationHelper.InsertMeterLoadProfileChannelGroup(LP_Channel_Group, out GroupId);
        //            try
        //            {
        //                LoadProfileChannel_GroupDAO DAO = new LoadProfileChannel_GroupDAO();
        //                DAO.AcceptChanges_LoadProfileChannelGroup(ConfigurationHelper.LoadedConfigurations);
        //            }
        //            catch (Exception ex)
        //            {
        //                // Discard All Modification Made
        //                ConfigurationHelper.LoadedConfigurations.LoadProfileChannel_Group.RejectChanges();
        //                throw ex;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error occurred while updating meter load profile channels group info", ex);
        //    }
        //}

        #endregion

        #region Event Info Configurations

        public List<EventInfo> GetMeterEventInfo(ConnectionInfo ConnectionInfo)
        {
            List<EventInfo> EventInfo = null;

            int device = -1;
            string meterModel = DefaultMeterModel;
            string manufacturer = DefaultManufacturer;

            string keyEventInfo = "Default_EventInfo";
            try
            {
                if (ConnectionInfo.IsInitialized)
                {
                    device = ConnectionInfo.MeterInfo.Device.Id;
                    manufacturer = ConnectionInfo.MeterInfo.Device.ManufacturerId.ToString();
                    meterModel = ConnectionInfo.MeterInfo.MeterModel;

                    keyEventInfo = string.Format("EventInfo_{0}{1}{2}", manufacturer, device, meterModel);
                }
                try
                {
                    if (IsConfigRefreshRequired)// || !LoadedConfigs.ContainsKey(keyEventInfo)) //Azeem
                    {
                        EventInfo = GetMeterEventInfoHelper(ConnectionInfo);
                    }
                    else
                        EventInfo = (List<EventInfo>)LoadedConfigs[keyEventInfo];
                }
                catch (Exception ex) { }
                finally
                {
                    if (EventInfo == null)
                        EventInfo = GetMeterEventInfoHelper(ConnectionInfo);
                }
                return EventInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while loading meter event info", ex);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        internal List<EventInfo> GetMeterEventInfoHelper(ConnectionInfo ConnectionInfo)
        {
            List<EventInfo> EventInfo = null;
            Object EventInfo_RawObj = null;

            int device = -1;
            string meterModel = DefaultMeterModel;
            string manufacturer = DefaultManufacturer;

            string keyEventInfo = "Default_EventInfo";
            try
            {
                if (ConnectionInfo.IsInitialized)
                {
                    device = ConnectionInfo.MeterInfo.Device.Id;
                    manufacturer = ConnectionInfo.MeterInfo.Device.ManufacturerId.ToString();
                    meterModel = ConnectionInfo.MeterInfo.MeterModel;

                    keyEventInfo = string.Format("EventInfo_{0}{1}{2}", manufacturer, device, meterModel);
                }
                if (IsConfigRefreshRequired)
                {
                    ResetConfiguration();
                }
                // else
                {
                    LoadedConfigs.TryGetValue(keyEventInfo, out EventInfo_RawObj);
                    if (EventInfo_RawObj != null)
                        EventInfo = (List<EventInfo>)EventInfo_RawObj;
                    else
                    {
                        EventInfo = ConfigurationHelper.GetEventInfoItems(ConnectionInfo.MeterInfo.Device_Configuration.Id); // (meterModel, meterVersion);
                        LoadedConfigs.TryRemove(keyEventInfo, out EventInfo_RawObj);
                        LoadedConfigs.TryAdd(keyEventInfo, EventInfo);
                    }
                }
                if (EventInfo == null || EventInfo.Count <= 0)
                    throw new Exception("Invalid Structure EventInfo");
                return EventInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while loading meter event info", ex);
            }
        }

        #endregion

        #region Event Log Info Configurations

        public List<EventLogInfo> GetMeterEventLogInfo(ConnectionInfo ConnectionInfo)
        {
            List<EventLogInfo> EventInfo = null;

            string device = DefaultDevice;
            string meterModel = DefaultMeterModel;
            string manufacturer = DefaultManufacturer;

            string keyEventInfo = "Default_EventLogInfo";
            try
            {
                if (ConnectionInfo.IsInitialized)
                {
                    device = ConnectionInfo.MeterInfo.Device.DeviceName;
                    manufacturer = ConnectionInfo.MeterInfo.Device.ManufacturerId.ToString();
                    meterModel = ConnectionInfo.MeterInfo.MeterModel;

                    keyEventInfo = string.Format("EventLogInfo_{0}{1}{2}", manufacturer, device, meterModel);
                }
                try
                {
                    if (IsConfigRefreshRequired)// || !LoadedConfigs.ContainsKey(keyEventInfo))
                    {
                        EventInfo = GetMeterEventLogInfoHelper(ConnectionInfo);
                    }
                    else
                        EventInfo = (List<EventLogInfo>)LoadedConfigs[keyEventInfo];
                }
                catch (Exception) { }
                finally
                {
                    if (EventInfo == null)
                        EventInfo = GetMeterEventLogInfoHelper(ConnectionInfo);
                }
                return EventInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while loading meter event info", ex);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        internal List<EventLogInfo> GetMeterEventLogInfoHelper(ConnectionInfo ConnectionInfo)
        {
            List<EventLogInfo> EventInfo = null;
            Object EventLogInfo_Obj_Raw = null;

            int device = -1;
            string meterModel = DefaultMeterModel;
            string manufacturer = DefaultManufacturer;

            string keyEventInfo = "Default_EventLogInfo";
            try
            {
                if (ConnectionInfo.IsInitialized)
                {
                    device = ConnectionInfo.MeterInfo.Device.Id;
                    manufacturer = ConnectionInfo.MeterInfo.Device.ManufacturerId.ToString();
                    meterModel = ConnectionInfo.MeterInfo.MeterModel;

                    keyEventInfo = string.Format("EventLogInfo_{0}{1}{2}", manufacturer, device, meterModel);
                }
                if (IsConfigRefreshRequired)
                {
                    ResetConfiguration();
                }
                // else
                {
                    LoadedConfigs.TryGetValue(keyEventInfo, out EventLogInfo_Obj_Raw);

                    if (EventLogInfo_Obj_Raw != null)
                        EventInfo = (List<EventLogInfo>)(EventLogInfo_Obj_Raw);
                    else
                    {
                        EventInfo = ConfigurationHelper.GetEventLogInfoItems(ConnectionInfo.MeterInfo.Device_Configuration.Id);
                        // (meterModel, meterVersion);
                        LoadedConfigs.TryRemove(keyEventInfo, out EventLogInfo_Obj_Raw);
                        LoadedConfigs.TryAdd(keyEventInfo, EventInfo);
                    }
                }

                if (EventInfo == null || EventInfo.Count <= 0)
                    throw new Exception("Invalid Structure EventLogInfo");

                return EventInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while loading meter event log info", ex);
            }
        }

        #endregion

        #region Billings Configurations

        public List<BillingItem> GetBillingItemsFormat(ConnectionInfo ConnectionInfo)
        {
            List<BillingItem> BillingInfo = null;

            string device = DefaultDevice;
            string meterModel = DefaultMeterModel;
            string manufacturer = DefaultManufacturer;

            string keyBillInfo = "Default_BillingInfo";

            try
            {
                if (ConnectionInfo.IsInitialized)
                {
                    device = ConnectionInfo.MeterInfo.Device.DeviceName;
                    manufacturer = ConnectionInfo.MeterInfo.Device.ManufacturerId.ToString();
                    meterModel = ConnectionInfo.MeterInfo.MeterModel;

                    keyBillInfo = string.Format("BillingInfo_{0}{1}{2}", manufacturer, device, meterModel);
                }
                try
                {
                    if (IsConfigRefreshRequired)
                    {
                        BillingInfo = GetBillingItemsFormatHelper(ConnectionInfo);
                    }
                    else
                        BillingInfo = (List<BillingItem>)LoadedConfigs[keyBillInfo];
                }
                catch (Exception) { }
                finally
                {
                    if (BillingInfo == null)
                        BillingInfo = GetBillingItemsFormatHelper(ConnectionInfo);
                }
                return BillingInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while loading meter billing info", ex);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        internal List<BillingItem> GetBillingItemsFormatHelper(ConnectionInfo ConnectionInfo)
        {
            Object BillingInfo_Obj_Raw = null;
            List<BillingItem> BillingInfo = null;

            int device = -1;
            string meterModel = DefaultMeterModel;
            string manufacturer = DefaultManufacturer;

            string keyBillInfo = "Default_BillingInfo";
            try
            {
                if (ConnectionInfo.IsInitialized)
                {
                    device = ConnectionInfo.MeterInfo.Device.Id;
                    manufacturer = ConnectionInfo.MeterInfo.Device.ManufacturerId.ToString();
                    meterModel = ConnectionInfo.MeterInfo.MeterModel;

                    keyBillInfo = string.Format("BillingInfo_{0}{1}{2}", manufacturer, device, meterModel);
                }
                if (IsConfigRefreshRequired)
                {
                    ResetConfiguration();
                }
                // else
                {
                    LoadedConfigs.TryGetValue(keyBillInfo, out BillingInfo_Obj_Raw);

                    if (BillingInfo_Obj_Raw != null)
                        BillingInfo = (List<BillingItem>)BillingInfo_Obj_Raw;
                    else
                    {
                        BillingInfo = ConfigurationHelper.GetBillingItemsFormat(ConnectionInfo.MeterInfo.Device_Configuration.Id); //meterModel, meterVersion);
                        LoadedConfigs.TryRemove(keyBillInfo, out BillingInfo_Obj_Raw);
                        LoadedConfigs.TryAdd(keyBillInfo, BillingInfo);
                    }
                }

                if (BillingInfo == null || BillingInfo.Count <= 0)
                    throw new Exception("Invalid Billing Info Structure");
                return BillingInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while loading meter billing info", ex);
            }
        }

        #endregion

        #region Capture Object List Configurations

        public List<CaptureObject> GetProfileCaptureObjectList(ConnectionInfo ConnectionInfo, StOBISCode TargetProfile, long? GroupId = null)
        {
            List<CaptureObject> ProfileCaptureList = null;

            string device = DefaultDevice;
            string meterModel = DefaultMeterModel;
            string manufacturer = DefaultManufacturer;

            string keyCaptureInfo = "Default_ProfileCaputerList";
            try
            {
                if (ConnectionInfo.IsInitialized)
                {
                    device = ConnectionInfo.MeterInfo.Device.DeviceName;
                    manufacturer = ConnectionInfo.MeterInfo.Device.ManufacturerId.ToString();
                    meterModel = ConnectionInfo.MeterInfo.MeterModel;

                    keyCaptureInfo = string.Format("ProfileCaputerList_{0}{1}{2}{3}{4}", TargetProfile.ToString(),
                                                   manufacturer, device, meterModel, GroupId);
                }
                try
                {
                    if (IsConfigRefreshRequired)
                    {
                        ProfileCaptureList = GetProfileCaptureObjectListHelper(ConnectionInfo, TargetProfile, GroupId);
                    }
                    else
                        ProfileCaptureList = (List<CaptureObject>)LoadedConfigs[keyCaptureInfo];
                }
                catch (Exception) { }
                finally
                {
                    if (ProfileCaptureList == null)
                        ProfileCaptureList = GetProfileCaptureObjectListHelper(ConnectionInfo, TargetProfile, GroupId);
                }
                return ProfileCaptureList;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred reading Capture Objects List For Profile {0}", TargetProfile), ex);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        internal List<CaptureObject> GetProfileCaptureObjectListHelper(ConnectionInfo ConnectionInfo, StOBISCode TargetProfile, long? GroupId = null)
        {
            Object ProfileCaptureList_Obj_Raw = null;
            List<CaptureObject> ProfileCaptureList = null;

            int deviceId = -1;
            string device = DefaultDevice;
            string meterModel = DefaultMeterModel;
            string manufacturer = DefaultManufacturer;

            string keyCaptureInfo = "Default_ProfileCaputerList";
            try
            {
                if (ConnectionInfo.IsInitialized)
                {
                    deviceId = ConnectionInfo.MeterInfo.Device.Id;
                    device = ConnectionInfo.MeterInfo.Device.DeviceName;
                    manufacturer = ConnectionInfo.MeterInfo.Device.ManufacturerId.ToString();
                    meterModel = ConnectionInfo.MeterInfo.MeterModel;

                    keyCaptureInfo = string.Format("ProfileCaputerList_{0}{1}{2}{3}{4}", TargetProfile.ToString(),
                                                   manufacturer, device, meterModel, GroupId);
                }

                if (IsConfigRefreshRequired)
                {
                    ResetConfiguration();
                }
                // else
                {
                    LoadedConfigs.TryGetValue(keyCaptureInfo, out ProfileCaptureList_Obj_Raw);
                    if (ProfileCaptureList_Obj_Raw != null)
                        ProfileCaptureList = (List<CaptureObject>)ProfileCaptureList_Obj_Raw;
                    else
                    {
                        ProfileCaptureList = ConfigurationHelper.GetProfileCaptureObjectList(deviceId, TargetProfile, GroupId);
                        LoadedConfigs.TryRemove(keyCaptureInfo, out ProfileCaptureList_Obj_Raw);
                        LoadedConfigs.TryAdd(keyCaptureInfo, ProfileCaptureList);
                    }
                }

                if (ProfileCaptureList == null || ProfileCaptureList.Count <= 0)
                    throw new Exception("Invalid Structure Profile Capture");
                return ProfileCaptureList;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred reading Capture Objects List For Profile {0}", TargetProfile), ex);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SaveProfileCaptureObjectList(ConnectionInfo ConnectionInfo, List<CaptureObject> CaptureObjectList, StOBISCode TargetProfile, long? GroupId)
        {
            object ProfileCaptureList_Obj_Raw = null;

            int device = -1;
            string meterModel = DefaultMeterModel;
            string manufacturer = DefaultManufacturer;
            string keyCaptureInfo = "Default_ProfileCaputerList";

            try
            {
                if (ConnectionInfo != null)
                {
                    if (ConnectionInfo.IsInitialized)
                    {
                        device = ConnectionInfo.MeterInfo.Device.Id;
                        manufacturer = ConnectionInfo.MeterInfo.Device.ManufacturerId.ToString();
                        meterModel = ConnectionInfo.MeterInfo.MeterModel;

                        keyCaptureInfo = string.Format("ProfileCaputerList_{0}{1}{2}{3}{4}", TargetProfile.ToString(),
                                                       manufacturer, device, meterModel, GroupId);
                    }

                    LoadedConfigs.TryGetValue(keyCaptureInfo, out ProfileCaptureList_Obj_Raw);

                    // Save Profile Capture Object List If Not exists before
                    if (ProfileCaptureList_Obj_Raw == null)
                    {
                        LoadedConfigs.TryAdd(keyCaptureInfo, CaptureObjectList);
                        // Try Save Capture Object List Into Backing Store
                        //ConfigurationHelper.SaveProfileCaptureObjectList(device, CaptureObjectList, TargetProfile, GroupId);
                        //try
                        //{
                        //    CaptureObject_DAO DAO = new CaptureObject_DAO();
                        //    DAO.AcceptChanges_CaptureObject(ConfigurationHelper.LoadedConfigurations);
                        //    ConfigurationHelper.LoadedConfigurations.CaptureObjects.AcceptChanges();
                        //}
                        //catch (Exception ex)
                        //{
                        //    // Discard Capture Objects Modifications
                        //    ConfigurationHelper.LoadedConfigurations.CaptureObjects.RejectChanges();
                        //    throw ex;
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred saving Capture Objects List For Profile {0}", TargetProfile), ex);
            }
        }

        #endregion
    }
}
