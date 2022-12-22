using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DLMS;
using DLMS.Comm;
using DatabaseConfiguration.DataSet;
using SharedCode.Comm.DataContainer;
using DatabaseConfiguration.DataBase;
using SharedCode.Comm.Param;
using DatabaseConfiguration;

namespace SharedCode.Comm.HelperClasses
{
    public class ConfigsHelper
    {
        private Configs loadedConfigurations;
        private IDBAccessLayer _dal;

        public IDBAccessLayer DAL
        {
            get { return _dal; }
            set { _dal = value; }
        }

        public Configs LoadedConfigurations
        {
            get { return loadedConfigurations; }
            set { loadedConfigurations = value; }
        }

        public ConfigsHelper(Configs localConfig)
        {
            LoadedConfigurations = localConfig;
        }

        public void SaveCurrentSAPOBISRights(int RightsGroupId, int deviceId, List<OBISCodeRights> OBISCodeRights, string connectionString)
        {
            try
            {
                // Configs.Sap_ConfigRow[] MeterSAPs = null;
                // Configs.Sap_ConfigRow CurrentSap = null;

                // MeterSAPs = (Configs.Sap_ConfigRow[])this.LoadedConfigurations.Sap_Config.Select(string.Format("id={0}", sap_config_id));
                // CurrentSap = MeterSAPs[0];

                if (OBISCodeRights.Count > 0)
                {
                    RemoveCurrentSAPOBISRights(RightsGroupId);
                }

                // if (CurrentMeterSAP == null)
                // {
                //     InsertNewSAP(ref CurrentMeterSAP,  (ushort)meterLogicalDevice._SAP_Address, true, MeterInfo.id);
                // }

                //CurrentClientSAP = MeterSAPs.Find((x) => (x.IsMeterSAP == false) &&
                //    (x.SAPAddress == ClientSAPObject.SAP_Address));
                // //if (CurrentClientSAP == null || CurrentMeterSAP == null)
                // //    throw new Exception("Meter Logical Devices or Client SAP does not exist");

                // if (CurrentClientSAP == null)
                // {
                //     InsertNewSAP(ref CurrentClientSAP, ClientSAPObject._SAP_Address, false, MeterInfo.id);
                // }


                ///Read OBIS Codes By Meter And Client SAPs
                ///Set OBIS_Code Rights Auto Id Seed
                long OBIS_Code_Rights_id_seed = 0, Rights_Id_Seed = 0;
                try
                {
                    OBISRithtsSEAC_DAO OBIS_DAO = new OBISRithtsSEAC_DAO(connectionString);
                    OBIS_Code_Rights_id_seed = OBIS_DAO.Load_MaxOBISRightId();

                    RithtsSEAC_DAO Rights_DAO = new RithtsSEAC_DAO(connectionString);
                    Rights_Id_Seed = Rights_DAO.Load_MaxOBISRightId();

                    LoadedConfigurations.OBIS_Rights.Columns["id"].AutoIncrementSeed = ++OBIS_Code_Rights_id_seed;
                    LoadedConfigurations.Rights.Columns["id"].AutoIncrementSeed = ++Rights_Id_Seed;

                }
                catch (Exception ex) { }

                foreach (var OBISCoderight in OBISCodeRights)
                {
                    try
                    {
                        #region Commented_Code_Section

                        // Configs.AllQuantitiesRow OBIS = LoadedConfigurations.
                        //                                 AllQuantities.Where(x => x.OBIS_Index == OBISCode.OBISIndex.OBIS_Value).FirstOrDefault();

                        //  if (OBIS == null)
                        //  {
                        //      OBIS = (Configs.AllQuantitiesRow)loadedConfigurations.AllQuantities.NewRow();
                        //      OBIS.OBIS_Index = OBISCode.OBISIndex.OBIS_Value;
                        //      OBIS.Label = "Auto Added";
                        //      LoadedConfigurations.AllQuantities.AddAllQuantitiesRow(OBIS);
                        //  } 

                        #endregion

                        Configs.OBIS_DetailsRow OBIS_Detail = loadedConfigurations.OBIS_Details.Where(x => x.Device_Id == deviceId &&
                                                                                                           x.Obis_Code == OBISCoderight.OBISIndex.OBIS_Value).FirstOrDefault();


                        Configs.OBIS_DetailsRow OBIS_Detail_By_Key = loadedConfigurations.OBIS_Details.Where(x => x.Device_Id == deviceId &&
                                                                                                           x.Default_OBIS_Code == OBISCoderight.OBISIndex.OBIS_Value).FirstOrDefault();

                        // Create New If Not Exist
                        if (OBIS_Detail == null)
                        {
                            OBIS_Detail = (Configs.OBIS_DetailsRow)loadedConfigurations.OBIS_Details.NewRow();

                            var Default_OBIS_Code = loadedConfigurations.AllQuantities.GetLabelIDByDefaultOBIS(OBISCoderight.OBISIndex.OBIS_Value);

                            // Ensure Unique OBIS_KEY Value
                            if (OBIS_Detail_By_Key == null || OBIS_Detail_By_Key.Default_OBIS_Code != Default_OBIS_Code)
                            {
                                OBIS_Detail.Default_OBIS_Code = loadedConfigurations.AllQuantities.GetLabelIDByDefaultOBIS(OBISCoderight.OBISIndex.OBIS_Value);
                            }
                            else
                                OBIS_Detail.Default_OBIS_Code = Convert.ToDecimal(Get_Index.Dummy);

                            OBIS_Detail.Obis_Code = OBISCoderight.OBISIndex.OBIS_Value;
                            OBIS_Detail.Device_Id = deviceId;
                            LoadedConfigurations.OBIS_Details.AddOBIS_DetailsRow(OBIS_Detail);
                        }

                        Configs.OBIS_RightsRow OBISRights = (Configs.OBIS_RightsRow)LoadedConfigurations.OBIS_Rights.NewRow();

                        OBISRights.OBIS_Index = OBISCoderight.OBISIndex.OBIS_Value;
                        OBISRights.Version = OBISCoderight.Version;
                        // Change for error removal by Sajid. See later when to use
                        OBISRights.ObisRightGroupId = RightsGroupId;

                        //try
                        //{
                        //    OBISRights.ServerSAPId = 0; // CurrentMeterSAP.id;
                        //    OBISRights.ClientSAPId = 0; // CurrentClientSAP.id;
                        //}
                        //catch (Exception)
                        //{
                        //}
                        // OBISRights.id =  loadedConfigurations.OBIS_Rights.Max(x => x.id) + 1;

                        /// OBISRights.id = 0;

                        LoadedConfigurations.OBIS_Rights.AddOBIS_RightsRow(OBISRights);

                        // Add OBIS Code Attribute Rights
                        foreach (var OBIS_Right in OBISCoderight.AttributeRights)
                        {
                            Configs.RightsRow Rights = (Configs.RightsRow)LoadedConfigurations.Rights.NewRow();
                            // Rights.id = 0;
                            Rights.type = (byte)OBISCodeRightType.Attribute;
                            Rights.SubId = OBIS_Right[0];
                            Rights.value = OBIS_Right[1];
                            Rights.OBIS_Right_Id = OBISRights.id;
                            LoadedConfigurations.Rights.AddRightsRow(Rights);
                        }

                        // Add OBIS Code Method Rights
                        foreach (var OBIS_Right in OBISCoderight.MethodRights)
                        {
                            Configs.RightsRow Rights = (Configs.RightsRow)LoadedConfigurations.Rights.NewRow();
                            Rights.type = (byte)OBISCodeRightType.Method;
                            Rights.SubId = OBIS_Right[0];
                            Rights.value = OBIS_Right[1];
                            Rights.OBIS_Right_Id = OBISRights.id;
                            LoadedConfigurations.Rights.AddRightsRow(Rights);
                        }

                        // Add OBIS Code Access Selectors Type
                        byte autoId = 1;
                        foreach (var AccessRight in OBISCoderight.AccessSelectors)
                        {
                            Configs.RightsRow Rights = (Configs.RightsRow)LoadedConfigurations.Rights.NewRow();
                            Rights.type = (byte)OBISCodeRightType.AccessSelector;
                            Rights.SubId = AccessRight[0];
                            Rights.value = AccessRight[1];
                            Rights.OBIS_Right_Id = OBISRights.id;
                            LoadedConfigurations.Rights.AddRightsRow(Rights);
                        }
                    }
                    catch (Exception ex)
                    {
                        // throw new Exception(String.Format("OBIS Code {0}", OBISCode.OBISIndex.
                        // ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)), ex);
                    }
                }

                // LoadedConfigurations.SaveConfigurations(Settings.Default.ConfigurationDBConnectionString);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred Saving OBIS Code Rights," + ex.Message, ex);
            }
        }

        public void LoadConfiguration(Configs configDataSet, string connectionString)
        {
            try
            {
                DAL.Load_All_Configurations(configDataSet);
                // Find Default Meter & Default Configurations

                var Manufacturer_Row = (this.LoadedConfigurations.Manufacturer.Count > 0) ? this.LoadedConfigurations.Manufacturer[0] : null;
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

                this.LoadedConfigurations.Configuration.CurrentConfiguration = DefaultConfig;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadConfiguration(string connectionString)
        {
            try
            {
                LoadConfiguration(this.LoadedConfigurations, connectionString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveConfigurations(string connectionString)
        {
            try
            {
                if (DAL == null)
                    //DAL = new SEAC_DBAccessLayer(connectionString);
                DAL = new ConfigDBController(connectionString, DataBaseTypes.SCT_DATABASE);
                DAL.Update_All_Configuration(this.LoadedConfigurations);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OBISCodeRights> GetCurrentSAPOBISRightsIOP(int OBIS_RightsGroupID, int deviceId)
        {
            try
            {
                if (LoadedConfigurations == null) return null;
                List<OBISCodeRights> OBISCodeRights = new List<OBISCodeRights>();
                List<Configs.OBIS_RightsRow> OBISRightsList = null;
                List<Configs.RightsRow> RightsList = null;

                // Read OBIS Codes By Meter And Client SAPs
                List<Configs.OBIS_DetailsRow> OBIS_Details = GetOBISDetailsByDeviceId(deviceId);
                // this.LoadedConfigurations.OBIS_Rights.GetOBIS_RihgtsBYRightsGroupId(ids);

                OBISRightsList = GetOBIS_RightsByRightsGroup(OBIS_RightsGroupID);

                if (OBISRightsList == null) return null;
                foreach (var OBISCode in OBISRightsList)
                {
                    RightsList = LoadedConfigurations.Rights.GetOBISCodeRights(OBISCode);
                    OBISCodeRights NewOBISRight = new OBISCodeRights();

                    NewOBISRight.OBISIndex = StOBISCode.ConvertFrom(Convert.ToUInt64(OBISCode.OBIS_Index));

                    // StOBISCode.ConvertFrom((ulong)(OBIS_Details.FirstOrDefault(x => x.id == OBISCode.Obis_Detail_Id).Obis_Code));
                    var OBIS_DetailsRow = OBIS_Details.FirstOrDefault(x => x.Obis_Code == OBISCode.OBIS_Index);

                    //if (OBIS_DetailsRow != null && !OBIS_DetailsRow.IsDefault_OBIS_CodeNull())
                    //    NewOBISRight.OBISLabel = (Get_Index)Convert.ToUInt64(OBIS_DetailsRow.Default_OBIS_Code);

                    NewOBISRight.Version = (byte)OBISCode.Version;
                    NewOBISRight.AttributeRights = new List<byte[]>();
                    NewOBISRight.MethodRights = new List<byte[]>();
                    NewOBISRight.AccessSelectors = new List<byte[]>();

                    // Assign OBISCode Rights
                    foreach (var Right in RightsList)
                    {
                        if (Right.type == (byte)OBISCodeRightType.Attribute)
                        {
                            NewOBISRight.SetAttribRight(Right.SubId, (Attrib_Access_Modes)Right.value);
                        }
                        else if (Right.type == (byte)OBISCodeRightType.Method)
                        {
                            NewOBISRight.SetMethodRight(Right.SubId, (Method_Access_Modes)Right.value);
                        }
                        else if (Right.type == (byte)OBISCodeRightType.AccessSelector)
                        {
                            NewOBISRight.SetAccessSelectorRight(Right.SubId, Right.value);
                        }
                    }

                    OBISCodeRights.Add(NewOBISRight);

                }
                return OBISCodeRights;
            }
            catch (Exception ex)
            {
                return null;
                //throw new Exception("Error occurred reading OBIS Code Rights", ex);
            }
        }

        public List<Manufacturer> LoadAllManufacturers()
        {
            Configs.ManufacturerRow[] MeterManufacturers = null;
            MeterManufacturers = (Configs.ManufacturerRow[])this.LoadedConfigurations.Manufacturer.Select(string.Format("1=1"));
            List<Manufacturer> manufacturerList = new List<Manufacturer>();
            foreach (Configs.ManufacturerRow row in MeterManufacturers)
            {
                Manufacturer manufacturer = new Manufacturer();
                manufacturer.Id = row.id;
                manufacturer.ManufacturerName = row.Manufacturer_Name;
                manufacturer.Code = row.Code;
                manufacturerList.Add(manufacturer);
            }
            return manufacturerList;
        }

        public List<Device> LoadDevicesByManufacturers(int ManufacturerId)
        {
            Configs.DeviceRow[] ManufacturerDevices = null;
            ManufacturerDevices = (Configs.DeviceRow[])this.LoadedConfigurations.Device.Select(string.Format("Manufacturer_Id={0}", ManufacturerId));
            List<Device> deviceList = new List<Device>();
            foreach (Configs.DeviceRow row in ManufacturerDevices)
            {
                Device device = new Device();
                device.Id = row.id;
                device.DeviceName = row.Device_Name;
                device.Model = row.Model;
                device.ManufacturerId = row.Manufacturer_Id;
                device.IsSinglePhase = row.IsSinglePhase;
                deviceList.Add(device);
            }
            return deviceList;
        }
        public Manufacturer LoadManufacturerById(int manufacturerId)
        {
            Configs.ManufacturerRow[] MeterManufacturers = null;
            MeterManufacturers = (Configs.ManufacturerRow[])this.LoadedConfigurations.Manufacturer.Select($"id={manufacturerId}");

            Manufacturer manufacturer = new Manufacturer();
            manufacturer.Id = MeterManufacturers[0].id;
            manufacturer.ManufacturerName = MeterManufacturers[0].Manufacturer_Name;
            manufacturer.Code = MeterManufacturers[0].Code;
            return manufacturer;
        }

        public Device LoadDeviceById(int deviceId)
        {
            Configs.DeviceRow[] ManufacturerDevices = null;
            ManufacturerDevices = (Configs.DeviceRow[])this.LoadedConfigurations.Device.Select($"id={deviceId}");
            Device dev = new Device();
            dev.Id = ManufacturerDevices[0].id;
            dev.DeviceName = ManufacturerDevices[0].Device_Name;
            dev.Model = ManufacturerDevices[0].Model;
            dev.ManufacturerId = ManufacturerDevices[0].Manufacturer_Id;
            dev.IsSinglePhase = ManufacturerDevices[0].IsSinglePhase;
            return dev;
        }
        public List<AuthenticationType> LoadAllAuthenticationTypes()
        {
            Configs.Authentication_TypeRow[] AuthenticationTypes = null;
            AuthenticationTypes = (Configs.Authentication_TypeRow[])this.LoadedConfigurations.Authentication_Type.Select(string.Format("1=1"));
            List<AuthenticationType> authenticationTypesList = new List<AuthenticationType>();
            foreach (Configs.Authentication_TypeRow row in AuthenticationTypes)
            {
                AuthenticationType device = new AuthenticationType();
                device.Id = row.id;
                device.AuthenticationTypeName = row.Authentication_Type_Name;
                authenticationTypesList.Add(device);
            }
            return authenticationTypesList;
        }

        public List<DeviceAssociation> LoadDeviceAssociations(int deviceId)
        {
            Configs.Device_AssociationRow[] selectedDeviceAssociations = null;
            List<DeviceAssociation> LoadedAssociations = new List<DeviceAssociation>();
            selectedDeviceAssociations = (Configs.Device_AssociationRow[])(this.LoadedConfigurations.Device_Association.Select(string.Format("Device_Id = {0}", deviceId)));

            if (selectedDeviceAssociations == null) return null;
            foreach (Configs.Device_AssociationRow deviceAssociationRow in selectedDeviceAssociations)
            {
                DeviceAssociation deviceAssociation = new DeviceAssociation();
                deviceAssociation.Id = deviceAssociationRow.id;
                deviceAssociation.AssociationName = deviceAssociationRow.Association_Name;
                deviceAssociation.AuthenticationType = (HLS_Mechanism)deviceAssociationRow.Authentication_Type_Id;
                deviceAssociation.ClientSap = (ushort)deviceAssociationRow.Client_Sap;
                deviceAssociation.MeterSap = (ushort)deviceAssociationRow.Meter_Sap;
                deviceAssociation.DeviceId = deviceAssociationRow.Device_Id;

                deviceAssociation.ConfigurationId = deviceAssociationRow.Configuration_Id;
                // (Configs.ConfigurationRow)LoadedConfigurations.Configuration.Select(string.Format("id={0}", deviceAssociations.Configuration_Id))[0];// deviceAssociations.Configuration_Id;

                deviceAssociation.RightGroupId = deviceAssociationRow.ObisRightGroupId;
                LoadedAssociations.Add(deviceAssociation);
            }
            return LoadedAssociations;
        }

        public Configuration LoadConfigurationById(int configId)
        {
            var config = (Configs.ConfigurationRow)LoadedConfigurations.Configuration.Select(string.Format("id={0}", configId))[0]; // deviceAssociations.Configuration_Id;

            if (config == null || (config.IsBillItemsGroupIdNull() &&
                                  config.IsDisplayWindowGroupIdNull() &&
                                  config.IsEventGroupIdNull() &&
                                  config.Islp_channels_group_idNull()))
            {
                throw new ArgumentNullException("configId");
            }

            Configuration configuration = new Configuration
                                            (config.id, config.Name)
            {
                LoadProfileGroupId = (config.Islp_channels_group_idNull()) ? -1 : config.lp_channels_group_id,
                BillItemsGroupId = (config.IsBillItemsGroupIdNull()) ? -1 : config.BillItemsGroupId,
                DisplayWindowGroupId = (config.IsDisplayWindowGroupIdNull()) ? -1 : config.DisplayWindowGroupId,
                EventGroupId = (config.IsEventGroupIdNull()) ? -1 : config.EventGroupId
            };
            return configuration;
        }

        public MeterConfig GetMeterConfigurationByAssociationId(int AssociationId)
        {
            MeterConfig CurrentMeterConfig = null;
            try
            {
                if (LoadedConfigurations == null) return null;

                var deviceAssociationRow = LoadedConfigurations.Device_Association.FindByid(AssociationId);

                if (deviceAssociationRow == null)
                    throw new ArgumentException("Association Id");

                var DeviceRow = LoadedConfigurations.Device.FindByid(deviceAssociationRow.Device_Id);
                var manufacturerRow = LoadedConfigurations.Manufacturer.FindByid(DeviceRow.Manufacturer_Id);

                // var ConfigurationRow = LoadedConfigurations.Configuration. GetConfigurationsByIdOrName(deviceAssociationRow.Configuration_Id, string.Empty);

                CurrentMeterConfig = new MeterConfig();
                // Device Association
                CurrentMeterConfig.Device_Association = new DeviceAssociation()
                {
                    Id = deviceAssociationRow.id,
                    AssociationName = deviceAssociationRow.Association_Name,
                    AuthenticationType = (HLS_Mechanism)deviceAssociationRow.Authentication_Type_Id,
                    ClientSap = Convert.ToUInt16(deviceAssociationRow.Client_Sap),
                    MeterSap = Convert.ToUInt16(deviceAssociationRow.Meter_Sap),
                    DeviceId = deviceAssociationRow.Device_Id,
                    ConfigurationId = deviceAssociationRow.Configuration_Id,
                    RightGroupId = deviceAssociationRow.ObisRightGroupId
                };
                // Device
                CurrentMeterConfig.Device = new Device()
                {
                    Id = DeviceRow.id,
                    DeviceName = DeviceRow.Device_Name,
                    ManufacturerId = DeviceRow.Manufacturer_Id,
                    Model = DeviceRow.Model,
                    IsSinglePhase = DeviceRow.IsSinglePhase,

                    SkipSoftwareUserID = false//
                };

                // Manufacturer
                CurrentMeterConfig.Manufacturer = new Manufacturer()
                {
                    Id = manufacturerRow.id,
                    Code = manufacturerRow.Code,
                    ManufacturerName = manufacturerRow.Manufacturer_Name
                };

                // Device Configuration
                CurrentMeterConfig.Device_Configuration = LoadConfigurationById(deviceAssociationRow.Configuration_Id);

                return CurrentMeterConfig;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MeterConfig> GetAllMeterConfigurations()
        {
            MeterConfig CurrentMeterConfig = null;
            List<MeterConfig> AllMeterConfigs = new List<MeterConfig>();
            List<int> deviceAssocIds = new List<int>();

            try
            {
                if (LoadedConfigurations == null) return null;

                foreach (var dvAssoRow in LoadedConfigurations.Device_Association.Rows)
                {
                    if (dvAssoRow == null || deviceAssocIds.Contains((dvAssoRow as Configs.Device_AssociationRow).id))
                        continue;
                    else
                        deviceAssocIds.Add((dvAssoRow as Configs.Device_AssociationRow).id);
                }

                deviceAssocIds.Sort();
                // Down load All Meter Configurations
                foreach (var devAssocId in deviceAssocIds)
                {
                    CurrentMeterConfig = GetMeterConfigurationByAssociationId(devAssocId);
                    if (CurrentMeterConfig == null || AllMeterConfigs.Contains(CurrentMeterConfig))
                        continue;
                    else
                        AllMeterConfigs.Add(CurrentMeterConfig);
                }

                return AllMeterConfigs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region OBIS Code Rights

        public Configs.Obis_Rights_GroupRow GetObisRightGroup(int rightGroupId)
        {
            return (Configs.Obis_Rights_GroupRow)this.LoadedConfigurations.Obis_Rights_Group.Select(string.Format("id={0}", rightGroupId))[0];
        }

        public List<OBISCodeRights> GetCurrentSAPOBISRights(int rights_group_id)
        {
            try
            {
                if (LoadedConfigurations == null) return null;
                // Configs.Sap_ConfigRow[] MeterSAPs = null;
                List<OBISCodeRights> OBISCodeRights = new List<OBISCodeRights>();
                // Configs.Sap_ConfigRow CurrentSAP = null;
                List<Configs.OBIS_RightsRow> OBISRightsList = null;
                List<Configs.RightsRow> RightsList = null;

                // MeterSAPs = (Configs.Sap_ConfigRow[])this.LoadedConfigurations.Sap_Config.Select(string.Format("id={0}", sap_Config_id));
                // CurrentSAP = MeterSAPs[0];

                // Read OBIS Codes By Meter And Client SAPs
                OBISRightsList = GetOBIS_RightsByRightsGroup(rights_group_id);  // this.LoadedConfigurations.OBIS_Rights.GetOBIS_Rihgts(rights_group_id);

                if (OBISRightsList == null) return null;

                foreach (var OBISright in OBISRightsList)
                {
                    RightsList = LoadedConfigurations.Rights.GetOBISCodeRights(OBISright);
                    OBISCodeRights NewOBISRight = new OBISCodeRights();
                    NewOBISRight.OBISIndex = StOBISCode.ConvertFrom((ulong)(OBISright.OBIS_Index));
                    NewOBISRight.Version = (byte)OBISright.Version;

                    NewOBISRight.AttributeRights = new List<byte[]>();
                    NewOBISRight.MethodRights = new List<byte[]>();
                    NewOBISRight.AccessSelectors = new List<byte[]>();
                    // Assign OBIS Code Rights
                    foreach (var Right in RightsList)
                    {
                        if (Right.type == (byte)OBISCodeRightType.Attribute)
                        {
                            NewOBISRight.SetAttribRight(Right.SubId, (Attrib_Access_Modes)Right.value);
                        }
                        else if (Right.type == (byte)OBISCodeRightType.Method)
                        {
                            NewOBISRight.SetMethodRight(Right.SubId, (Method_Access_Modes)Right.value);
                        }
                        else if (Right.type == (byte)OBISCodeRightType.AccessSelector)
                        {
                            // NewOBISRight.SetAccessSelectorRight(Right.SubId, (SelectiveAccessType)Right.value);
                            NewOBISRight.SetAccessSelectorRight(Right.SubId, Right.value);
                        }
                    }

                    OBISCodeRights.Add(NewOBISRight);

                }
                return OBISCodeRights;
            }
            catch (Exception ex)
            {
                return null;
                // throw new Exception("Error occurred reading OBIS Code Rights", ex);
            }
        }

        public void SaveCurrentSAPOBISRights(int RightsGroupId, int deviceId, List<OBISCodeRights> OBISCodeRights)
        {
            try
            {
                if (OBISCodeRights.Count > 0)
                {
                    RemoveCurrentSAPOBISRights(RightsGroupId);
                }

                // Read OBIS Codes By Meter And Client SAPs
                // Set OBIS_Code Rights Auto Id Seed
                long OBIS_Code_Rights_id_seed = 0, Rights_Id_Seed = 0;
                try
                {
                    OBISRithtsSEAC_DAO OBIS_DAO = new OBISRithtsSEAC_DAO("");
                    OBIS_Code_Rights_id_seed = OBIS_DAO.Load_MaxOBISRightId();

                    RithtsSEAC_DAO Rights_DAO = new RithtsSEAC_DAO("");
                    Rights_Id_Seed = Rights_DAO.Load_MaxOBISRightId();

                    LoadedConfigurations.OBIS_Rights.Columns["id"].AutoIncrementSeed = ++OBIS_Code_Rights_id_seed;
                    LoadedConfigurations.Rights.Columns["id"].AutoIncrementSeed = ++Rights_Id_Seed;

                }
                catch (Exception ex) { }

                foreach (var OBISCoderight in OBISCodeRights)
                {
                    try
                    {
                        Configs.OBIS_DetailsRow OBIS_Detail = loadedConfigurations.OBIS_Details.Where(x => x.Device_Id == deviceId &&
                                                                                                           x.Obis_Code == OBISCoderight.OBISIndex.OBIS_Value).FirstOrDefault();

                        Configs.OBIS_DetailsRow OBIS_Detail_By_Key = loadedConfigurations.OBIS_Details.Where(x => x.Device_Id == deviceId &&
                                                                                                           x.Default_OBIS_Code == OBISCoderight.OBISIndex.OBIS_Value).FirstOrDefault();

                        // Create New If Not Exist
                        if (OBIS_Detail == null)
                        {
                            OBIS_Detail = (Configs.OBIS_DetailsRow)loadedConfigurations.OBIS_Details.NewRow();

                            var Default_OBIS_Code = loadedConfigurations.AllQuantities.GetLabelIDByDefaultOBIS(OBISCoderight.OBISIndex.OBIS_Value);

                            // Ensure Unique OBIS_KEY Value
                            if (OBIS_Detail_By_Key == null || OBIS_Detail_By_Key.Default_OBIS_Code != Default_OBIS_Code)
                            {
                                OBIS_Detail.Default_OBIS_Code = loadedConfigurations.AllQuantities.GetLabelIDByDefaultOBIS(OBISCoderight.OBISIndex.OBIS_Value);
                            }
                            else
                                OBIS_Detail.Default_OBIS_Code = Convert.ToDecimal(Get_Index.Dummy);

                            OBIS_Detail.Obis_Code = OBISCoderight.OBISIndex.OBIS_Value;
                            OBIS_Detail.Device_Id = deviceId;
                            LoadedConfigurations.OBIS_Details.AddOBIS_DetailsRow(OBIS_Detail);
                        }

                        Configs.OBIS_RightsRow OBISRights = (Configs.OBIS_RightsRow)LoadedConfigurations.OBIS_Rights.NewRow();

                        OBISRights.OBIS_Index = OBISCoderight.OBISIndex.OBIS_Value;
                        OBISRights.Version = OBISCoderight.Version;
                        // Change for error removal by Sajid. See later when to use
                        OBISRights.ObisRightGroupId = RightsGroupId;

                        // OBISRights.id = 0;
                        LoadedConfigurations.OBIS_Rights.AddOBIS_RightsRow(OBISRights);

                        // Add OBIS Code Attribute Rights
                        foreach (var OBIS_Right in OBISCoderight.AttributeRights)
                        {
                            Configs.RightsRow Rights = (Configs.RightsRow)LoadedConfigurations.Rights.NewRow();
                            // Rights.id = 0;
                            Rights.type = (byte)OBISCodeRightType.Attribute;
                            Rights.SubId = OBIS_Right[0];
                            Rights.value = OBIS_Right[1];
                            Rights.OBIS_Right_Id = OBISRights.id;
                            LoadedConfigurations.Rights.AddRightsRow(Rights);
                        }

                        // Add OBIS Code Method Rights
                        foreach (var OBIS_Right in OBISCoderight.MethodRights)
                        {
                            Configs.RightsRow Rights = (Configs.RightsRow)LoadedConfigurations.Rights.NewRow();
                            Rights.type = (byte)OBISCodeRightType.Method;
                            Rights.SubId = OBIS_Right[0];
                            Rights.value = OBIS_Right[1];
                            Rights.OBIS_Right_Id = OBISRights.id;
                            LoadedConfigurations.Rights.AddRightsRow(Rights);
                        }

                        // Add OBIS Code Access Selectors Type
                        byte autoId = 1;
                        foreach (var AccessRight in OBISCoderight.AccessSelectors)
                        {
                            Configs.RightsRow Rights = (Configs.RightsRow)LoadedConfigurations.Rights.NewRow();
                            Rights.type = (byte)OBISCodeRightType.AccessSelector;
                            Rights.SubId = AccessRight[0];
                            Rights.value = AccessRight[1];
                            Rights.OBIS_Right_Id = OBISRights.id;
                            LoadedConfigurations.Rights.AddRightsRow(Rights);
                        }
                    }
                    catch (Exception ex)
                    {
                        // throw new Exception(String.Format("OBIS Code {0}", OBISCode.OBISIndex.
                        // ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)), ex);
                    }
                }

                // LoadedConfigurations.SaveConfigurations(Settings.Default.ConfigurationDBConnectionString);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred Saving OBIS Code Rights," + ex.Message, ex);
            }
        }

        public void RemoveCurrentSAPOBISRights(int rights_group_id)
        {
            try
            {
                // Query the database for the rows to be deleted
                var deleteOBISRights =
                    from OBISRights in loadedConfigurations.OBIS_Rights.AsEnumerable<Configs.OBIS_RightsRow>()
                    where !OBISRights.IsObisRightGroupIdNull() &&
                           OBISRights.ObisRightGroupId == rights_group_id
                    select OBISRights;

                try
                {
                    loadedConfigurations.EnforceConstraints = false;
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
                    loadedConfigurations.EnforceConstraints = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error removing OBIS Code Rights", ex);
            }
        }

        public List<Configs.OBIS_RightsRow> GetOBIS_RightsByRightsGroup(int RightsGroupId)
        {
            try
            {
                // List<Configs.OBIS_DetailsRow> OBIS_Details = GetOBISDetailsByRightsGroupId(RightsGroupId);
                // if (OBIS_Details == null || OBIS_Details.Count == 0) return null;
                // List<int> ids = (from ob in OBIS_Details select ob.id).ToList<int>();
                return loadedConfigurations.OBIS_Rights.GetOBIS_Rihgts(RightsGroupId);
            }
            catch (Exception ex)
            {

                throw new Exception("Error while Loading OBIS Rights", ex);
            }
        }

        #endregion

        #region OBIS_Detail

        public List<Configs.OBIS_DetailsRow> GetOBISDetailsByDeviceId(int DeviceId)
        {
            return loadedConfigurations.OBIS_Details.GetOBISDetailIdsByDeviceId(DeviceId);
        }

        public List<KeyValuePair<StOBISCode, StOBISCode>> GetOBISCodesMapByDeviceId(int DeviceId)
        {
            List<Configs.OBIS_DetailsRow> OBISDetailRows = null;
            List<KeyValuePair<StOBISCode, StOBISCode>> OBISKEYMap = new List<KeyValuePair<StOBISCode, StOBISCode>>(450);

            try
            {
                OBISDetailRows = loadedConfigurations.OBIS_Details.GetOBISDetailIdsByDeviceId(DeviceId);
                StOBISCode OBIS_key = Get_Index.Dummy;
                StOBISCode OBISCode;
                KeyValuePair<StOBISCode, StOBISCode> valPair;

                // Iterate OBISDetailRows
                foreach (var OBISDetailRow in OBISDetailRows)
                {
                    if (OBISDetailRow == null)
                        continue;
                    OBIS_key.OBIS_Value = ulong.MaxValue;
                    OBISCode = new StOBISCode();

                    //if (!OBISDetailRow.IsDefault_OBIS_CodeNull())
                        OBIS_key.OBIS_Value = Convert.ToUInt64(OBISDetailRow.Default_OBIS_Code);

                    if (!OBISDetailRow.IsObis_CodeNull())
                        OBISCode.OBIS_Value = Convert.ToUInt64(OBISDetailRow.Obis_Code);

                    // Default OBISCode Is DB Null
                    if (OBIS_key.OBIS_Value != ulong.MaxValue)
                    {
                        valPair = new KeyValuePair<StOBISCode, StOBISCode>(OBIS_key, OBISCode);

                        OBISKEYMap.Add(valPair);
                    }
                }
            }
            catch
            {
                throw;
            }

            return OBISKEYMap;
        }

        #endregion

        #region GetAllSelectableWindows

        public List<DisplayWindowItem> GetAllSelectableWindows()
        {
            Configs.ConfigurationRow CurrentConfiguration = null;
            List<DisplayWindowItem> Windows = null;

            try
            {
                CurrentConfiguration = LoadedConfigurations.Configuration.CurrentConfiguration;
                Windows = GetAllSelectableWindows(CurrentConfiguration);
            }
            catch
            {
                throw;
            }

            return Windows;
        }

        public List<DisplayWindowItem> GetAllSelectableWindows(int ConfigurationId)
        {
            Configs.ConfigurationRow CurrentConfiguration = null;
            List<DisplayWindowItem> Windows = null;

            try
            {
                CurrentConfiguration = LoadedConfigurations.Configuration.FindByid(ConfigurationId);
                Windows = GetAllSelectableWindows(CurrentConfiguration);
            }
            catch
            {
                throw;
            }

            return Windows;
        }

        /// <summary>
        /// Factory Method that used to build All Selectable Display Windows
        /// </summary>
        /// <returns></returns>
        public List<DisplayWindowItem> GetAllSelectableWindows(Configs.ConfigurationRow CurrentConfiguration)
        {
            List<DisplayWindowItem> Windows = new List<DisplayWindowItem>();
            List<Configs.DisplayWindowsRow> loadedList = null;
            try
            {
                if (CurrentConfiguration == null)
                    throw new ArgumentNullException("ConfigurationRow");

                if (LoadedConfigurations != null && LoadedConfigurations.DisplayWindows != null)
                {
                    loadedList = LoadedConfigurations.DisplayWindows.GetDisplayWindowsByConfiguration(CurrentConfiguration);
                }

                if (loadedList == null || loadedList.Count <= 0)
                    throw new Exception("Unable to load all selectable Windows list form configuration Data Source");

                foreach (var item in loadedList)
                {
                    DisplayWindowItem WindowItem = Windows.Find((x) => x.Obis_Index == (Get_Index)item.QuantityIndex);
                    if (WindowItem == null)
                    {
                        WindowItem = new DisplayWindowItem();
                        Windows.Add(WindowItem);
                    }

                    WindowItem.Obis_Index = (Get_Index)item.QuantityIndex;

                    string WinName = string.Empty;
                    if (!item.IsLabelNull())
                        WinName = item.Label;

                    if (String.IsNullOrEmpty(WinName))
                        WinName = LoadedConfigurations.AllQuantities.FindByDefault_OBIS_Code(item.QuantityIndex).Label;
                    WindowItem.Window_Name = WinName;

                    WindowItem.AttributeSelected = item.AttributeNo;
                    WindowItem.Category.Add((DisplayWindowsCategory)item.Category);
                }

                return Windows;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GetAllSelectableLoadProfileChannels

        //public List<LoadProfileChannelInfo> GetMeterAllSelectableLoadProfileChannels(string devName, string devModel, string Manufacturer_Code)
        //{
        //    try
        //    {
        //        Configs.ConfigurationRow MeterConfigEntry = GetMeterConfiguration(devName, devModel, Manufacturer_Code, null);

        //        if (MeterConfigEntry == null)
        //            throw new Exception("Unable to load all configuration");

        //        return GetAllSelectableLoadProfileChannels(MeterConfigEntry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<LoadProfileChannelInfo> GetMeterProfileChannelsInfo(ulong GroupId, int ConfigurationId)
        //{
        //    try
        //    {
        //        List<LoadProfileChannelInfo> LPChannels = new List<LoadProfileChannelInfo>();
        //        List<Configs.LoadProfileChannelsRow> loadedList = null;
        //        Configs.LoadProfileChannel_GroupRow ChannelGroup = null;
        //        Configs.ConfigurationRow CurrentConfiguration = null;

        //        if (LoadedConfigurations != null && LoadedConfigurations.LoadProfileChannel_Group != null)
        //        {
        //            ChannelGroup = LoadedConfigurations.LoadProfileChannel_Group.GetLoadProfileChannelGroup(GroupId);
        //            CurrentConfiguration = LoadedConfigurations.Configuration.FindByid(ConfigurationId);
        //        }
        //        if (ChannelGroup == null)
        //            throw new Exception("Unable to load profile channel group from configuration Data Source");

        //        if (LoadedConfigurations != null &&
        //            LoadedConfigurations.LoadProfileChannels != null)
        //        {
        //            loadedList = LoadedConfigurations.LoadProfileChannels.GetLoadProfileChannelsByGroup(ChannelGroup, CurrentConfiguration.LoadProfile_GroupRow);
        //        }

        //        if (loadedList == null || loadedList.Count <= 0)
        //            throw new Exception("Unable to load profile channels from configuration Data Source");

        //        foreach (Configs.LoadProfileChannelsRow item in loadedList)
        //        {
        //            LoadProfileChannelInfo LPChannelInfo = LPChannels.Find((x) => x.OBIS_Index == (Get_Index)item.QuantityIndex);
        //            if (LPChannelInfo == null)
        //            {
        //                LPChannelInfo = new LoadProfileChannelInfo();
        //                LPChannels.Add(LPChannelInfo);
        //            }
        //            LPChannelInfo.OBIS_Index = (Get_Index)item.QuantityIndex;
        //            LPChannelInfo.Quantity_Name = LoadedConfigurations.AllQuantities.FindByOBIS_Index(item.QuantityIndex).Label;
        //            LPChannelInfo.SelectedAttribute = item.AttributeNo;
        //            LPChannelInfo.CapturePeriod = new TimeSpan();
        //            LPChannelInfo.Channel_id = 1;
        //            LPChannelInfo.IsDataPresent = false;
        //            LPChannelInfo.Multiplier = item.Multiplier;

        //            String[] UnitValues = (String[])Enum.GetNames(typeof(Unit));
        //            bool ISMatched = false;
        //            foreach (var UnitItem in UnitValues)
        //            {
        //                if (UnitItem.Equals(item.Unit, StringComparison.OrdinalIgnoreCase))
        //                {
        //                    ISMatched = true;
        //                    item.Unit = UnitItem;
        //                    break;
        //                }
        //            }
        //            LPChannelInfo.Unit = (Unit)((ISMatched) ? Enum.Parse(typeof(Unit), item.Unit) : Unit.UnitLess);
        //        }
        //        return LPChannels;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<LoadProfileChannelInfo> GetAllSelectableLoadProfileChannels()
        {
            Configs.ConfigurationRow CurrentConfiguration = null;
            List<LoadProfileChannelInfo> LPChannels = null;

            try
            {
                CurrentConfiguration = LoadedConfigurations.Configuration.CurrentConfiguration;
                LPChannels = GetAllSelectableLoadProfileChannels(CurrentConfiguration);
            }
            catch
            {
                throw;
            }

            return LPChannels;
        }

        public List<LoadProfileChannelInfo> GetAllSelectableLoadProfileChannels(int ConfigurationId)
        {
            Configs.ConfigurationRow CurrentConfiguration = null;
            List<LoadProfileChannelInfo> LPChannels = null;

            try
            {
                CurrentConfiguration = LoadedConfigurations.Configuration.FindByid(ConfigurationId);
                LPChannels = GetAllSelectableLoadProfileChannels(CurrentConfiguration);
            }
            catch
            {
                throw;
            }

            return LPChannels;
        }

        public List<LoadProfileChannelInfo> GetAllSelectableLoadProfileChannels(Configs.ConfigurationRow CurrentConfiguration)
        {
            List<LoadProfileChannelInfo> LPChannels = new List<LoadProfileChannelInfo>();
            List<Configs.LoadProfileChannelsRow> loadedList = null;

            try
            {
                if (CurrentConfiguration == null)
                    throw new ArgumentNullException("ConfigurationRow");

                if (LoadedConfigurations != null && LoadedConfigurations.LoadProfileChannels != null)
                {
                    loadedList = LoadedConfigurations.LoadProfileChannels.GetLoadProfileByConfiguration(CurrentConfiguration);
                }
                if (loadedList == null || loadedList.Count <= 0)
                    throw new Exception("Unable to load all selectable Load Profile Channels from configuration Data Source");
                int chId = 1;
                foreach (Configs.LoadProfileChannelsRow item in loadedList)
                {
                    LoadProfileChannelInfo LPChannelInfo = LPChannels.Find((x) => x.OBIS_Index == (Get_Index)item.QuantityIndex);
                    if (LPChannelInfo == null)
                    {
                        LPChannelInfo = new LoadProfileChannelInfo();
                        LPChannels.Add(LPChannelInfo);
                    }
                    LPChannelInfo.OBIS_Index = (Get_Index)item.QuantityIndex;

                    string Quantity_Name = string.Empty;
                    if (!item.IsLabelNull())
                        Quantity_Name = item.Label;

                    if (String.IsNullOrEmpty(Quantity_Name))
                        Quantity_Name = LoadedConfigurations.AllQuantities.FindByDefault_OBIS_Code(item.QuantityIndex).Label;
                    LPChannelInfo.Quantity_Name = Quantity_Name;

                    // LPChannelInfo.Quantity_Name = LoadedConfigurations.AllQuantities.FindByDefault_OBIS_Code(item.QuantityIndex).Label;
                    LPChannelInfo.SelectedAttribute = item.AttributeNo;
                    LPChannelInfo.CapturePeriod = new TimeSpan();
                    LPChannelInfo.Channel_id = chId++; //1; //Azeem
                    LPChannelInfo.IsDataPresent = false;
                    LPChannelInfo.Multiplier = item.Multiplier;

                    String[] UnitValues = (String[])Enum.GetNames(typeof(Unit));
                    bool ISMatched = false;
                    foreach (var UnitItem in UnitValues)
                    {
                        if (UnitItem.Equals(item.Unit, StringComparison.OrdinalIgnoreCase))
                        {
                            ISMatched = true;
                            item.Unit = UnitItem;
                            break;
                        }
                    }
                    LPChannelInfo.Unit = (Unit)((ISMatched) ? Enum.Parse(typeof(Unit), item.Unit) : Unit.UnitLess);
                }
                return LPChannels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GetBillingItemsFormat

        public List<BillingItem> GetBillingItemsFormat()
        {
            Configs.ConfigurationRow CurrentConfiguration = null;
            List<BillingItem> BillItemsList = null;

            try
            {
                CurrentConfiguration = LoadedConfigurations.Configuration.CurrentConfiguration;
                BillItemsList = GetBillingItemsFormat(CurrentConfiguration);
            }
            catch
            {
                throw;
            }

            return BillItemsList;
        }

        public List<BillingItem> GetBillingItemsFormat(int ConfigurationId)
        {
            Configs.ConfigurationRow CurrentConfiguration = null;
            List<BillingItem> BillItemsList = null;

            try
            {
                CurrentConfiguration = LoadedConfigurations.Configuration.FindByid(ConfigurationId);
                BillItemsList = GetBillingItemsFormat(CurrentConfiguration);
            }
            catch
            {
                throw;
            }

            return BillItemsList;
        }

        public List<BillingItem> GetBillingItemsFormat(Configs.ConfigurationRow CurrentConfiguration)
        {
            List<BillingItem> BillItemsList = new List<BillingItem>();
            List<Configs.BillingItemsRow> BillingItems = new List<Configs.BillingItemsRow>();
            List<List<Configs.BillTariffQuantityRow>> BillItemQuantities = new List<List<Configs.BillTariffQuantityRow>>();

            try
            {
                Configs Configurator = LoadedConfigurations;

                if (CurrentConfiguration == null)
                    throw new ArgumentNullException("CurrentConfiguration");

                BillingItems = Configurator.BillingItems.GetBillingItemsByConfigurations(CurrentConfiguration);
                Configurator.BillTariffQuantity.GetBillingItemsByConfigurations(CurrentConfiguration, BillingItems, BillItemQuantities);

                // Build BillItem Object
                for (int index = 0; index < BillingItems.Count; index++)
                {
                    BillingItem NewItem = new BillingItem();
                    Configs.BillingItemsRow BillItem = BillingItems[index];
                    List<Configs.BillTariffQuantityRow> BillItemSubList = BillItemQuantities[index];

                    // Init Billing Items
                    NewItem.Name = BillItem.Label;
                    NewItem.Format = (String.IsNullOrEmpty(BillItem.FormatSpecifier) ? "f3" : BillItem.FormatSpecifier);
                    NewItem.Multiplier = BillItem.Multiplier;
                    //NewItem.DB_Field = BillingItems
                    String[] UnitValues = (String[])Enum.GetNames(typeof(Unit));

                    bool ISMatched = false;
                    foreach (var item in UnitValues)
                    {
                        if (item.Equals(BillItem.Unit, StringComparison.OrdinalIgnoreCase))
                        {
                            ISMatched = true;
                            BillItem.Unit = item;
                            break;
                        }
                    }

                    NewItem.Unit = (Unit)((ISMatched) ? Enum.Parse(typeof(Unit), BillItem.Unit) : Unit.UnitLess);
                    NewItem.ValueInfo = new List<Get_Index>();
                    foreach (Configs.BillTariffQuantityRow BillItemSub in BillItemSubList)
                    {
                        NewItem.ValueInfo.Add((Get_Index)BillItemSub.OBIS_Index);
                        NewItem.DBInfo.Add(BillItemSub.DatabaseField);
                    }
                    BillItemsList.Add(NewItem);
                }
                return BillItemsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Event Info & Event Logs

        public List<EventInfo> GetEventInfoItems()
        {
            Configs.ConfigurationRow CurrentConfiguration = null;
            List<EventInfo> EventInfoItems = null;

            try
            {
                CurrentConfiguration = LoadedConfigurations.Configuration.CurrentConfiguration;
                EventInfoItems = GetEventInfoItems(CurrentConfiguration);
            }
            catch
            {
                throw;
            }

            return EventInfoItems;
        }

        public List<EventInfo> GetEventInfoItems(int ConfigurationId)
        {
            Configs.ConfigurationRow CurrentConfiguration = null;
            List<EventInfo> EventInfoItems = null;

            try
            {
                CurrentConfiguration = LoadedConfigurations.Configuration.FindByid(ConfigurationId);
                EventInfoItems = GetEventInfoItems(CurrentConfiguration);
            }
            catch
            {
                throw;
            }

            return EventInfoItems;
        }

        public List<EventInfo> GetEventInfoItems(Configs.ConfigurationRow CurrentConfiguration)
        {
            try
            {
                List<EventInfo> EventInfoItems = new List<EventInfo>();
                List<Configs.EventInfoRow> eventInfoLoadList = new List<Configs.EventInfoRow>();

                if (CurrentConfiguration == null)
                    throw new ArgumentNullException("ConfigurationRow");

                if (LoadedConfigurations != null && LoadedConfigurations.EventInfo != null)
                {
                    LoadedConfigurations.EventInfo.GetEventInfoByConfigurations(CurrentConfiguration, eventInfoLoadList);
                }
                if (eventInfoLoadList == null || eventInfoLoadList.Count <= 0)
                    throw new Exception("Unable to load event info form configuration Data Source");
                foreach (var item in eventInfoLoadList)
                {
                    EventInfo EventInfoItem = new EventInfo(item.EventNo,item.EventCode, item.Label, item.MaxEventCount);
                   //if (!item.IsEventNoNull())
                   //         EventInfoItem._EventId = item.EventNo;
                    EventInfoItems.Add(EventInfoItem);
                }
                EventInfoItems.Sort((x, y) => x.EventCode.CompareTo(y.EventCode));

                return EventInfoItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EventLogInfo> GetEventLogInfoItems()
        {
            List<EventLogInfo> EventLogInfoItems = null;

            try
            {
                EventLogInfoItems = GetEventLogInfoItems(LoadedConfigurations.Configuration.CurrentConfiguration);
            }
            catch
            {
                throw;
            }

            return EventLogInfoItems;
        }

        public List<EventLogInfo> GetEventLogInfoItems(int ConfigurationId)
        {
            Configs.ConfigurationRow CurrentConfiguration = null;
            List<EventLogInfo> EventLogInfoItems = null;

            try
            {
                CurrentConfiguration = LoadedConfigurations.Configuration.FindByid(ConfigurationId);
                EventLogInfoItems = GetEventLogInfoItems(CurrentConfiguration);
            }
            catch
            {
                throw;
            }

            return EventLogInfoItems;
        }

        public List<EventLogInfo> GetEventLogInfoItems(Configs.ConfigurationRow CurrentConfiguration)
        {
            try
            {
                List<EventLogInfo> EventLogInfoItems = new List<EventLogInfo>();
                List<Configs.EventInfoRow> eventInfoLoadList = new List<Configs.EventInfoRow>();
                List<Configs.EventLogsRow> eventLogInfoLoadList = new List<Configs.EventLogsRow>();

                if (CurrentConfiguration == null)
                    throw new ArgumentNullException("ConfigurationRow");

                if (LoadedConfigurations != null && LoadedConfigurations.EventInfo != null && LoadedConfigurations.EventLogs != null)
                {
                    LoadedConfigurations.EventInfo.
                        GetEventInfoByConfigurations(CurrentConfiguration, eventInfoLoadList);
                    LoadedConfigurations.EventLogs.
                        GetEventLogInfoByConfigurations(CurrentConfiguration, eventLogInfoLoadList);
                }

                if ((eventInfoLoadList == null || eventInfoLoadList.Count <= 0) || (eventLogInfoLoadList == null || eventLogInfoLoadList.Count <= 0))
                    throw new Exception("Unable to load event log info form configuration Data Source");

                foreach (var eventLogItem in eventLogInfoLoadList)
                {
                    Configs.EventInfoRow eventInfoRow = eventInfoLoadList.Find((x) => x.id == eventLogItem.id);
                    if (eventInfoRow == null)
                        continue;

                    EventLogInfo eventLogItemNew = new EventLogInfo(eventInfoRow.EventNo, eventInfoRow.EventCode, eventInfoRow.Label,
                        (Get_Index)eventLogItem.EventLogIndex, (Get_Index)eventLogItem.EventCounterIndex);
                    eventLogItemNew.MaxEventCount = eventInfoRow.MaxEventCount;
                    EventLogInfoItems.Add(eventLogItemNew);

                }
                // Ahmed  EventLogInfoItems.Sort((x, y) => x.EventCode.CompareTo(y.EventCode));
                EventLogInfoItems.Sort((x, y) => x.EventLogIndex.CompareTo(y.EventLogIndex));
                return EventLogInfoItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        /// <summary>
        /// Select meter default current configurations
        /// </summary>
        /// <param name="firmwareVersion"></param>
        /// <param name="meterModel"></param>
        public Configs.ConfigurationRow SelectDefaultConfiguration(int ConfigurationId, int? AssociationId = null)
        {
            Configs.Device_AssociationRow DvAssocRow = null;
            Configs.ConfigurationRow CurrentConfigs = null;

            try
            {
                if (AssociationId != null && AssociationId > 0)
                {
                    DvAssocRow = LoadedConfigurations.Device_Association.FindByid(Convert.ToInt32(AssociationId));
                    CurrentConfigs = LoadedConfigurations.Configuration.GetConfigurationByNameAndId(DvAssocRow.Configuration_Id);
                }
                else
                {
                    CurrentConfigs = LoadedConfigurations.Configuration.GetConfigurationByNameAndId(ConfigurationId);
                }

                if (CurrentConfigs == null)
                {
                    throw new ArgumentNullException("ConfigurationRow");

                    CurrentConfigs = (Configs.ConfigurationRow)LoadedConfigurations.Configuration.NewRow();
                    CurrentConfigs.BillItemsGroupId = 1;
                    CurrentConfigs.DisplayWindowGroupId = 1;
                    CurrentConfigs.EventGroupId = 1;
                    CurrentConfigs.lp_channels_group_id = 1;
                    CurrentConfigs.id = loadedConfigurations.Configuration.Max(x => x.id) + 1;
                    LoadedConfigurations.Configuration.AddConfigurationRow(CurrentConfigs);
                }

                if (CurrentConfigs != null)
                {
                    // LoadedConfigurations.Meter_Info.CurrentMeterInfo = MeterInfoRow;
                    // LoadedConfigurations.Meter_Configuration.CurrentConfiguration = MeterConfigurationRow;
                    LoadedConfigurations.Configuration.CurrentConfiguration = CurrentConfigs;
                }
                return CurrentConfigs;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to load default configurations", ex);
            }
        }

        /// public void CopyMeter(string sourceDlmsVersion, string destDlmsVersion, string sourceModel, string destModel)
        //public void CopyMeter(Configs.Meter_ConfigurationRow sourceMeterInfo, Configs.Meter_ConfigurationRow targetMeterInfo)
        //{
        //    try
        //    {
        //        String ComparisionDlmsWare = String.Format("[Firmware_Version] =  '{0}'", sourceMeterInfo.Firmware_Version);
        //        if (String.IsNullOrEmpty(sourceMeterInfo.Firmware_Version))
        //            ComparisionDlmsWare = "true";
        //        Configs.Meter_ConfigurationRow MeterInfoRowDest = null;
        //        Configs.Meter_ConfigurationRow[] MeterInfoRows = (Configs.Meter_ConfigurationRow[])LoadedConfigurations.
        //            Meter_Configuration.Select(String.Format("{0}", ComparisionDlmsWare));
        //        if (MeterInfoRows == null || MeterInfoRows.Length <= 0)
        //        {
        //            Array.Sort(MeterInfoRows, (x, y) => x.Firmware_Version.CompareTo(y.Firmware_Version));
        //            Configs.Meter_ConfigurationRow MeterInfoRowSource = MeterInfoRows[MeterInfoRows.Length - 1];
        //            // LoadedConfigurations.MeterTypeInfo.GetMeter(sourceMeterInfo.Dlms_version);
        //            Configs.Meter_ConfigurationRow NewMtrRow = LoadedConfigurations.Meter_Configuration.NewMeter_ConfigurationRow();

        //            NewMtrRow.Firmware_Version = targetMeterInfo.Firmware_Version;
        //            NewMtrRow.Config_id = targetMeterInfo.Config_id;
        //            NewMtrRow.Meter_Info_id = targetMeterInfo.Meter_Info_id;

        //            LoadedConfigurations.Meter_Configuration.AddMeter_ConfigurationRow(NewMtrRow);
        //            MeterInfoRowDest = NewMtrRow;

        //            CopyMeterSAP(MeterInfoRowSource, MeterInfoRowDest);
        //            //throw new Exception("Error occurred while copy meter details");
        //        }
        //        else MeterInfoRowDest = MeterInfoRows[MeterInfoRows.Length - 1];

        //        /// if (MeterInfoRowSource == MeterInfoRowDest)
        //        ///     throw new Exception("Unable to copy from source meter to destination meter");
        //        /// Create New Meter 
        //        /// else if (MeterInfoRowDest == null)
        //        /// {
        //        /// }

        //        Configs.ConfigurationRow NewSourceConfig = LoadedConfigurations.Configuration.GetConfigurationsByIdOrName(sourceMeterInfo.Config_id, null)[0];
        //        Configs.ConfigurationRow NewDestinationConfig = null;// LoadedConfigurations.Configuration.GetConfigurationByMeterId(targetMeterInfo);

        //        /// if(NewSourceConfig == null)
        //        /// {
        //        ///     throw new Exception("Unable to copy,Meter Source not exists");
        //        /// }

        //        if (NewSourceConfig == null)
        //        {
        //            NewDestinationConfig = LoadedConfigurations.Configuration.NewConfigurationRow();
        //            NewDestinationConfig.Name = String.Format("{0}", NewSourceConfig.Name);
        //            NewDestinationConfig.BillItemsGroupId = NewSourceConfig.BillItemsGroupId;
        //            NewDestinationConfig.DisplayWindowGroupId = NewSourceConfig.DisplayWindowGroupId;
        //            NewDestinationConfig.EventGroupId = NewSourceConfig.EventGroupId;
        //            NewDestinationConfig.lp_channels_group_id = NewSourceConfig.lp_channels_group_id;
        //            LoadedConfigurations.Configuration.AddConfigurationRow(NewDestinationConfig);
        //        }

        //        /// Copy Meter Source SAPs to Meter Destination SAPs
        //        /// Copy Source to Destination Configuration
        //        /// CopyMeterConfiguration(NewSourceConfig, NewDestinationConfig);


        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error occurred while copy meter", ex);
        //    }
        //}


        /// <summary>
        /// To Delete All Meter & Its Configurations from the Data Sources
        /// </summary>
        /// <param name="MeterInfo"></param>
        //public void DeleteMeter(int meterInfoId, string version)
        //{
        //    try
        //    {
        //        Configs.Meter_ConfigurationRow MeterInfo = LoadedConfigurations.Meter_Configuration.GetConfigurationsByIdOrFirmwareVersion(meterInfoId, version)[0];
        //        if (MeterInfo != null)
        //        {
        //            ///Clear Data From MeterTypeInfo Table will enforce to clear data from relevant
        //            ///data tables also,hence all data will be cleared for specific meter
        //            MeterInfo.Delete();
        //            LoadedConfigurations.Meter_Configuration.RemoveMeter_ConfigurationRow(MeterInfo);
        //        }
        //        else
        //            throw new Exception("Unable to delete meter info details");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Unable to delete meter details", ex);
        //    }
        //}

        /// <summary>
        /// Try to copy all meter default configurations from source settings to destination settings
        /// </summary>
        /// <param name="sourcefirmwareVersion"></param>
        /// <param name="sourceMeterModel"></param>
        /// <param name="destfirmwareVersion"></param>
        /// <param name="destMeterModel"></param>
        //public void CopyMeter_Configuration(Configs.Meter_ConfigurationRow sourceMeterInfo, out Configs.Meter_ConfigurationRow targetMeterInfo)
        //{
        //    try
        //    {
        //        /// Select Source Meter Default Configuration
        //        Configs.Meter_ConfigurationRow SourceConfig = LoadedConfigurations.Meter_Configuration.GetConfigurationsByIdOrFirmwareVersion(sourceMeterInfo.id, sourceMeterInfo.Firmware_Version)[0];

        //        if (SourceConfig == null)
        //            throw new Exception("Unable to copy meter configurations,meter not exists");

        //        /// Try to Populate and Add New Configuration
        //        Configs.Meter_ConfigurationRow DestConfig = CreateMeter_ConfigurationRow(SourceConfig);

        //        targetMeterInfo = DestConfig;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error occurred while copying  meter configurations", ex);
        //    }
        //}

        public void CopyMeterConfiguration(Configs.ConfigurationRow SourceConfigId, Configs.ConfigurationRow DestinationConfigId)
        {
            try
            {
                /// Remove Configuration Rows, Delete All Configuration Data 
                /// If Previously that data exists
                Configs.ConfigurationRow TRow = CreateConfigurationRow(SourceConfigId);
                DestinationConfigId.Delete();
                LoadedConfigurations.Configuration.RemoveConfigurationRow(DestinationConfigId);
                LoadedConfigurations.Configuration.AddConfigurationRow(TRow);
                DestinationConfigId = TRow;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error copying meter configuration {0} {1}", SourceConfigId.Name, DestinationConfigId.Name), ex);
            }
        }

        public void CopyMeterConfiguration(Configs.ConfigurationRow SourceConfigId, out Configs.ConfigurationRow DestinationConfigId, int deepCopyRows)
        {
            try
            {
                /// Remove Configuration Rows, Delete All Configuration Data 
                /// If Previously that data exists
                Configs.ConfigurationRow TRow = CreateConfigurationRow(SourceConfigId);
                /// LoadedConfigurations.Configuration.RemoveConfigurationRow(DestinationConfigId);
                /// LoadedConfigurations.Configuration.AddConfigurationRow(TRow);
                DestinationConfigId = TRow;

                Configs.LoadProfile_GroupRow LPGP = null, LPGP_Out = null;
                Configs.DisplayWindows_GroupRow DisplayWin_GP = null, DisplayWin_GP_Out = null;
                Configs.BillingItem_GroupRow BillItem_GP = null, BillItem_GP_Out = null;
                Configs.Events_GroupRow EventItem_GP = null, EventItem_GP_Out = null;

                LPGP = SourceConfigId.LoadProfile_GroupRow;
                DisplayWin_GP = SourceConfigId.DisplayWindows_GroupRow;
                BillItem_GP = SourceConfigId.BillingItem_GroupRow;
                EventItem_GP = SourceConfigId.Events_GroupRow;

                /// Copy LoadProfile Group
                CopyLoadProfileGroup_Configuration(LPGP, out LPGP_Out);
                /// Copy Display WindowGroup
                CopyDisplayWindowGroup_Configuration(DisplayWin_GP, out DisplayWin_GP_Out);
                /// Copy BillingItems Group
                CopyBillingItemsGroup_Configuration(BillItem_GP, out BillItem_GP_Out);
                /// Copy EventInfo Group
                CopyEventInfoGroup_Configuration(EventItem_GP, out EventItem_GP_Out);

                /// Update Destination Configuration Row
                DestinationConfigId.lp_channels_group_id = LPGP_Out.id;
                DestinationConfigId.DisplayWindowGroupId = DisplayWin_GP_Out.id;
                DestinationConfigId.BillItemsGroupId = BillItem_GP_Out.id;
                DestinationConfigId.EventGroupId = EventItem_GP_Out.id;

                LoadedConfigurations.Configuration.AddConfigurationRow(TRow);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error copying Configuration {0}", SourceConfigId.Name), ex);
            }
        }

        public void CopyLoadProfileGroup_Configuration(Configs.LoadProfile_GroupRow SourceConfigId,
                                                       out Configs.LoadProfile_GroupRow DestinationConfigId)
        {
            try
            {
                Configs.LoadProfile_GroupRow TRow = CreateLoadProfileGroupRow(SourceConfigId);

                LoadedConfigurations.LoadProfile_Group.AddLoadProfile_GroupRow(TRow);
                DestinationConfigId = TRow;

                #region /// Copy Load Profile Channels Items

                try
                {
                    List<Configs.LoadProfileChannelsRow> LoadProfileItems = LoadedConfigurations.LoadProfileChannels.GetLoadProfileByGroup(SourceConfigId);
                    foreach (var LPItem in LoadProfileItems)
                    {
                        Configs.LoadProfileChannelsRow NewRow = CreateLoadProfileItem(LPItem);
                        NewRow.LoadProfileGroupId = DestinationConfigId.id;

                        LoadedConfigurations.LoadProfileChannels.AddLoadProfileChannelsRow(NewRow);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error copying load profile items", ex);
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error Copying LoadProfile Group Configurations {0}", SourceConfigId.LoadProfile_Group_Name), ex);
            }
        }

        public void CopyDisplayWindowGroup_Configuration(Configs.DisplayWindows_GroupRow SourceConfigId,
                                                   out Configs.DisplayWindows_GroupRow DestinationConfigId)
        {
            try
            {
                Configs.DisplayWindows_GroupRow TRow = CreateDisplayWindowGroupRow(SourceConfigId);

                LoadedConfigurations.DisplayWindows_Group.AddDisplayWindows_GroupRow(TRow);
                DestinationConfigId = TRow;

                #region // Copy Display Windows Items

                try
                {
                    List<Configs.DisplayWindowsRow> DespWinRow = LoadedConfigurations.DisplayWindows.GetDisplayWindowsByGroup(SourceConfigId);

                    foreach (var DispWin in DespWinRow)
                    {
                        Configs.DisplayWindowsRow NewRow = CreateDisplayWindow(DispWin);
                        NewRow.DisplayWindowsGroupId = DestinationConfigId.id;
                        LoadedConfigurations.DisplayWindows.AddDisplayWindowsRow(NewRow);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error copying display window items", ex);
                }

                #endregion

            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error Copying DisplayWindows Group Configurations {0}", SourceConfigId.Dw_Group_Name), ex);
            }
        }


        public void CopyBillingItemsGroup_Configuration(Configs.BillingItem_GroupRow SourceConfigId,
                                                    out Configs.BillingItem_GroupRow DestinationConfigId)
        {
            try
            {
                Configs.BillingItem_GroupRow TRow = CreateBillingItemGroupRow(SourceConfigId);

                LoadedConfigurations.BillingItem_Group.AddBillingItem_GroupRow(TRow);
                DestinationConfigId = TRow;

                #region // Copy Billing Items

                try
                {
                    List<Configs.BillingItemsRow> BillingItems = LoadedConfigurations.BillingItems.GetBillingItemsByGroup(SourceConfigId);
                    List<List<Configs.BillTariffQuantityRow>> TariffRows = new List<List<Configs.BillTariffQuantityRow>>();

                    LoadedConfigurations.BillTariffQuantity.GetBillingItemsByGroup(SourceConfigId, BillingItems, TariffRows);

                    for (int index = 0; BillingItems != null && index < BillingItems.Count; index++)
                    {
                        try
                        {
                            Configs.BillingItemsRow BillItem = BillingItems[index];
                            Configs.BillingItemsRow NewBillItemRow = CreateBillingItem(BillItem);
                            NewBillItemRow.BillItemGroupId = DestinationConfigId.id;

                            // Copy Billing Items Tariff Quantity
                            List<Configs.BillTariffQuantityRow> TariffQuantities = TariffRows.Find((x) => x[0].BillItemId == BillItem.id);
                            if (TariffQuantities == null)
                                continue;

                            LoadedConfigurations.BillingItems.AddBillingItemsRow(NewBillItemRow);
                            foreach (var TariffQuantity in TariffQuantities)
                            {
                                Configs.BillTariffQuantityRow NewRow = CreateBillTariffQuantity(TariffQuantity);
                                NewRow.BillItemId = NewBillItemRow.id;
                                LoadedConfigurations.BillTariffQuantity.AddBillTariffQuantityRow(NewRow);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error copying billing items", ex);
                }

                #endregion

                #region Commented_Code

                // #region ///Copy Display Windows Items
                // try
                // {
                //     List<Configs.DisplayWindowsRow> DespWinRow = LoadedConfigurations.DisplayWindows.GetDisplayWindowsByConfiguration(SourceConfigId);
                //     foreach (var DispWin in DespWinRow)
                //     {
                //         Configs.DisplayWindowsRow NewRow = CreateDisplayWindow(DispWin);
                //         NewRow.ConfigId = DestinationConfigId.id;
                //         LoadedConfigurations.DisplayWindows.AddDisplayWindowsRow(NewRow);
                //     }
                // }
                // catch (Exception ex)
                // {
                //     throw new Exception("Error copying display window items", ex);
                // }
                // #endregion
                //#region ///Copy Load Profile Channels Items
                //try
                //{
                //    List<Configs.LoadProfileChannelsRow> LoadProfileItems = LoadedConfigurations.LoadProfileChannels.GetLoadProfileByConfiguration(SourceConfigId);
                //    foreach (var LPItem in LoadProfileItems)
                //    {
                //        Configs.LoadProfileChannelsRow NewRow = CreateLoadProfileItem(LPItem);
                //        NewRow.ConfigId = DestinationConfigId.id;
                //        LoadedConfigurations.LoadProfileChannels.AddLoadProfileChannelsRow(NewRow);
                //    }
                //}
                //catch (Exception ex)
                //{
                //    throw new Exception("Error copying load profile items", ex);
                //}
                //#endregion
                //#region ///Copy Billing Items
                //try
                //{
                //    List<Configs.BillingItemsRow> BillingItems = LoadedConfigurations.BillingItems.GetBillingItemsByConfigurations(SourceConfigId);
                //    List<List<Configs.BillTariffQuantityRow>> TariffRows = new List<List<Configs.BillTariffQuantityRow>>();
                //    LoadedConfigurations.BillTariffQuantity.GetBillingItemsByConfigurations(SourceConfigId, BillingItems, TariffRows);

                //    for (int index = 0; BillingItems != null && index < BillingItems.Count; index++)
                //    {
                //        try
                //        {
                //            Configs.BillingItemsRow BillItem = BillingItems[index];
                //            Configs.BillingItemsRow NewBillItemRow = CreateBillingItem(BillItem);
                //            NewBillItemRow.ConfigId = DestinationConfigId.id;
                //            ///Copy Billing Items Tariff Quantitiy
                //            List<Configs.BillTariffQuantityRow> TariffQuantities = TariffRows.Find((x) => x[0].BillItemId == BillItem.id);
                //            if (TariffQuantities == null)
                //                continue;
                //            LoadedConfigurations.BillingItems.AddBillingItemsRow(NewBillItemRow);
                //            foreach (var TariffQuantity in TariffQuantities)
                //            {
                //                Configs.BillTariffQuantityRow NewRow = CreateBillTariffQuantity(TariffQuantity);
                //                NewRow.BillItemId = NewBillItemRow.id;
                //                LoadedConfigurations.BillTariffQuantity.AddBillTariffQuantityRow(NewRow);
                //            }

                //        }
                //        catch (Exception ex)
                //        {

                //            throw ex;
                //        }
                //    }

                //}
                //catch (Exception ex)
                //{

                //    throw new Exception("Error copying billing items", ex);
                //}
                //#endregion
                //#region ///Copy EventInfo & EventLogInfo
                //try
                //{
                //    List<Configs.EventInfoRow> EventInfoItems = new List<Configs.EventInfoRow>();
                //    List<Configs.EventLogsRow> EventLogInfoItems = new List<Configs.EventLogsRow>();
                //    LoadedConfigurations.EventInfo.GetEventInfoByConfigurations(SourceConfigId, EventInfoItems);
                //    LoadedConfigurations.EventLogs.GetEventLogInfoByConfigurations(SourceConfigId, EventLogInfoItems);

                //    for (int index = 0;
                //        (EventInfoItems != null && index < EventInfoItems.Count) ||
                //        (EventLogInfoItems != null && index < EventLogInfoItems.Count)
                //        ; index++)
                //    {
                //        try
                //        {
                //            Configs.EventInfoRow EventInfoItem = EventInfoItems[index];
                //            Configs.EventInfoRow NewEventInfoItem = CreateEventInfoItem(EventInfoItem);
                //            NewEventInfoItem.ConfigId = DestinationConfigId.id;
                //            ///Copy EventInfo & EventLogInfo
                //            LoadedConfigurations.EventInfo.AddEventInfoRow(NewEventInfoItem);
                //            Configs.EventLogsRow evenLogInfoItem = EventLogInfoItems.Find((x) => x.id == EventInfoItem.id);
                //            if (evenLogInfoItem == null)
                //                continue;
                //            Configs.EventLogsRow NewRow = CreateEventLogInfoItem(evenLogInfoItem);
                //            NewRow.id = NewEventInfoItem.id;
                //            NewRow.ConfigId = NewEventInfoItem.ConfigId;
                //            LoadedConfigurations.EventLogs.AddEventLogsRow(NewRow);
                //        }
                //        catch (Exception ex)
                //        {
                //            throw ex;
                //        }
                //    }

                //}
                //catch (Exception ex)
                //{

                //    throw new Exception("Error copying Event Info logs items", ex);
                //}
                //#endregion

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error Copying BillingItems Group Configurations {0}", SourceConfigId.BillingItem_Group_Name), ex);
            }
        }


        public void CopyEventInfoGroup_Configuration(Configs.Events_GroupRow SourceConfigId,
                                                     out Configs.Events_GroupRow DestinationConfigId)
        {
            try
            {
                Configs.Events_GroupRow TRow = CreateEventInfoGroupRow(SourceConfigId);

                LoadedConfigurations.Events_Group.AddEvents_GroupRow(TRow);
                DestinationConfigId = TRow;

                #region /// Copy EventInfo & EventLogInfo

                try
                {
                    List<Configs.EventInfoRow> EventInfoItems = new List<Configs.EventInfoRow>();
                    List<Configs.EventLogsRow> EventLogInfoItems = new List<Configs.EventLogsRow>();

                    LoadedConfigurations.EventInfo.GetEventInfoByGroup(SourceConfigId, EventInfoItems);
                    LoadedConfigurations.EventLogs.GetEventLogInfoByGroup(SourceConfigId, EventLogInfoItems);

                    for (int index = 0;
                         (EventInfoItems != null && index < EventInfoItems.Count) ||
                         (EventLogInfoItems != null && index < EventLogInfoItems.Count); index++)
                    {
                        try
                        {
                            Configs.EventInfoRow EventInfoItem = EventInfoItems[index];
                            Configs.EventInfoRow NewEventInfoItem = CreateEventInfoItem(EventInfoItem);
                            NewEventInfoItem.EventGroupId = DestinationConfigId.id;

                            /// Copy EventInfo & EventLogInfo
                            LoadedConfigurations.EventInfo.AddEventInfoRow(NewEventInfoItem);
                            Configs.EventLogsRow evenLogInfoItem = EventLogInfoItems.Find((x) => x.id == EventInfoItem.id);
                            if (evenLogInfoItem == null)
                                continue;

                            Configs.EventLogsRow NewRow = CreateEventLogInfoItem(evenLogInfoItem);

                            NewRow.id = NewEventInfoItem.id;
                            NewRow.EventInfoRow = NewEventInfoItem;
                            NewRow.EventGroupId = NewEventInfoItem.EventGroupId;
                            LoadedConfigurations.EventLogs.AddEventLogsRow(NewRow);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw new Exception("Error Copying Event Info Logs items", ex);
                }

                #endregion

            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error Copying EventInfo Group Configurations {0}", SourceConfigId.Events_group_Name), ex);
            }
        }

        //public Configs.ConfigurationRow GetMeterConfiguration(String devName, string devModel, string Manufacturer_Code, string Manufacturer_name)
        //{
        //    Configs.ConfigurationRow ConfigRow = null;
        //    int manuf_Id = -1;
        //    try
        //    {
        //        var manufacturers = LoadedConfigurations.Manufacturer.GetManufacturerByCodeOrName(Manufacturer_Code, Manufacturer_name);

        //        if (manufacturers != null && manufacturers.Length > 0)
        //            manuf_Id = manufacturers[0].id;

        //        var devices = LoadedConfigurations.Device.GetDeviceByNameOrModel(devName, devModel, manuf_Id);

        //        if (devices == null || devices.Length <= 0)
        //            return null;

        //        foreach (var dev in devices)
        //        {
        //            var dev_Assoc = dev.GetDevice_AssociationRows();

        //            if (dev_Assoc == null || dev_Assoc.Length <= 0)
        //                continue;
        //            else
        //            {
        //                foreach (var devAssc in dev_Assoc)
        //                {
        //                    if (devAssc == null || devAssc.IsConfiguration_IdNull())
        //                        continue;
        //                    else
        //                    {
        //                        ConfigRow = devAssc.ConfigurationRow;
        //                        if (ConfigRow != null)
        //                            break;
        //                    }
        //                }
        //            }

        //            if (ConfigRow != null)
        //                break;
        //        }

        //        return ConfigRow;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Unable to load meter configurations", ex);
        //    }
        //}

        public List<CaptureObject> GetProfileCaptureObjectList(int deviceId, StOBISCode Target_OBIS_Index, long? GroupId = null)
        {
            try
            {
                List<Configs.CaptureObjectsRow> CaptureObjectList = null;
                List<CaptureObject> CaptureObjectsList = null;

                CaptureObjectList = this.LoadedConfigurations.
                    CaptureObjects.GetCaptureObjectByProfile(deviceId, Target_OBIS_Index.OBIS_Value, GroupId);
                if (CaptureObjectList == null || CaptureObjectList.Count <= 0)
                    throw new Exception("No Capture objects Exits");
                CaptureObjectsList = new List<CaptureObject>();
                foreach (var captureObject in CaptureObjectList)
                {
                    if (captureObject == null)
                        throw new Exception("null capture object received");

                    CaptureObject Obj = new CaptureObject();
                    StOBISCode OBIS_Index = (Get_Index)captureObject.OBIS_Index;
                    Obj.OBISCode = OBIS_Index.OBISCode;
                    Obj.ClassId = OBIS_Index.ClassId;
                    Obj.AttributeIndex = captureObject.AttributeNo;
                    Obj.DataIndex = captureObject.DataIndex;
                    Obj.DatabaseFieldName = 
                        (string.IsNullOrEmpty(captureObject.databasefield)) ? string.Empty : captureObject.databasefield;
                    Obj.Multiplier = captureObject.Multiplier;
                    CaptureObjectsList.Add(Obj);
                }
                return CaptureObjectsList;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred reading Capture Objects List For Profile {0}", Target_OBIS_Index), ex);
            }
        }

        public void SaveProfileCaptureObjectList(int deviceId, List<CaptureObject> CaptureObjectList, StOBISCode Target_OBIS_Index, long? GroupId)
        {
            try
            {
                if (CaptureObjectList == null || CaptureObjectList.Count <= 0)
                    throw new Exception("No Capture objects Exits");
                int SeqId = 1;
                foreach (var captureObject in CaptureObjectList)
                {
                    if (captureObject == null)
                        throw new Exception("null capture object received");
                    Configs.CaptureObjectsRow CaptureObjRow = LoadedConfigurations.CaptureObjects.NewCaptureObjectsRow();
                    StOBISCode OBIS_Index = captureObject.StOBISCode;// StOBISCode.ConvertFrom(captureObject.OBISCode);
                    // OBIS_Index.ClassId = captureObject.ClassId;
                    ///Insert Values Into CaptureObjectsRow
                    CaptureObjRow.SequenceId = SeqId++;
                    CaptureObjRow.OBIS_Index = OBIS_Index.OBIS_Value;
                    CaptureObjRow.AttributeNo = captureObject.AttributeIndex;
                    CaptureObjRow.DataIndex = captureObject.DataIndex;
                    CaptureObjRow.Target_OBIS_Index = Target_OBIS_Index.OBIS_Value;
                    CaptureObjRow.DeviceId = deviceId;

                    if (GroupId != null)
                        CaptureObjRow.GroupId = (long)GroupId;
                    else
                        CaptureObjRow.SetGroupIdNull();

                    LoadedConfigurations.CaptureObjects.AddCaptureObjectsRow(CaptureObjRow);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred reading Capture Objects List For Profile {0}", Target_OBIS_Index), ex);
            }
        }

        //public void GetMeterLoadProfileChannelGroup(List<LoadProfileChannelInfo> LP_ChannelInfo, out uint? GroupId)
        //{
        //    try
        //    {
        //        // Populate Load Profile Channel Group Instance
        //        Configs.LoadProfileChannel_GroupRow GP_Row = LoadedConfigurations.LoadProfileChannel_Group.NewLoadProfileChannel_GroupRow();
        //        // GP_Row.ConfigId = MeterConfigEntry.id;

        //        //Channel_1_Value
        //        if (LP_ChannelInfo.Count > 0 && LP_ChannelInfo[0] != null)
        //        {
        //            StOBISCode OBIS_Channel_1 = LP_ChannelInfo[0].OBIS_Index;
        //            GP_Row.Channel_1 = OBIS_Channel_1.OBIS_Value;
        //        }
        //        else
        //            GP_Row.Channel_1 = 0;

        //        //Channel_2_Value
        //        if (LP_ChannelInfo.Count > 1 && LP_ChannelInfo[1] != null)
        //        {
        //            StOBISCode OBIS_Channel_2 = LP_ChannelInfo[1].OBIS_Index;
        //            GP_Row.Channel_2 = OBIS_Channel_2.OBIS_Value;
        //        }
        //        else
        //            GP_Row.Channel_2 = 0;

        //        //Channel_3_Value
        //        if (LP_ChannelInfo.Count > 2 && LP_ChannelInfo[2] != null)
        //        {
        //            StOBISCode OBIS_Channel_3 = LP_ChannelInfo[2].OBIS_Index;
        //            GP_Row.Channel_3 = OBIS_Channel_3.OBIS_Value;
        //        }
        //        else
        //            GP_Row.Channel_3 = 0;

        //        //Channel_4_Value
        //        if (LP_ChannelInfo.Count > 3 && LP_ChannelInfo[3] != null)
        //        {
        //            StOBISCode OBIS_Channel_4 = LP_ChannelInfo[3].OBIS_Index;
        //            GP_Row.Channel_4 = OBIS_Channel_4.OBIS_Value;
        //        }
        //        else
        //            GP_Row.Channel_4 = 0;

        //        LoadedConfigurations.LoadProfileChannel_Group.GetLoadProfileChannelGroupId(GP_Row, out GroupId);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void InsertMeterLoadProfileChannelGroup(List<LoadProfileChannelInfo> LP_ChannelInfo, out uint? GroupId)
        //{
        //    try
        //    {
        //        long GroupId_Seed = 0;
        //        try
        //        {
        //            long? MaxGroupId = LoadedConfigurations.LoadProfileChannel_Group.MaxLoadProfileChannelGroupId();
        //            if (MaxGroupId != null && MaxGroupId > 0)
        //                GroupId_Seed = (long)MaxGroupId + 1;

        //            LoadedConfigurations.LoadProfileChannel_Group.Columns["load_profile_group_id"].AutoIncrementSeed = GroupId_Seed;
        //        }
        //        catch { }
        //        // Populate Load Profile Channel Group Instance
        //        Configs.LoadProfileChannel_GroupRow GP_Row = LoadedConfigurations.LoadProfileChannel_Group.NewLoadProfileChannel_GroupRow();

        //        #region LP_Channel_Groups

        //        ///Channel_1_Value
        //        if (LP_ChannelInfo.Count > 0 && LP_ChannelInfo[0] != null)
        //        {
        //            StOBISCode OBIS_Channel_1 = LP_ChannelInfo[0].OBIS_Index;
        //            GP_Row.Channel_1 = OBIS_Channel_1.OBIS_Value;
        //        }
        //        else
        //            GP_Row.Channel_1 = 0;
        //        ///Channel_2_Value
        //        if (LP_ChannelInfo.Count > 1 && LP_ChannelInfo[1] != null)
        //        {
        //            StOBISCode OBIS_Channel_2 = LP_ChannelInfo[1].OBIS_Index;
        //            GP_Row.Channel_2 = OBIS_Channel_2.OBIS_Value;
        //        }
        //        else
        //            GP_Row.Channel_2 = 0;
        //        ///Channel_3_Value
        //        if (LP_ChannelInfo.Count > 2 && LP_ChannelInfo[2] != null)
        //        {
        //            StOBISCode OBIS_Channel_3 = LP_ChannelInfo[2].OBIS_Index;
        //            GP_Row.Channel_3 = OBIS_Channel_3.OBIS_Value;
        //        }
        //        else
        //            GP_Row.Channel_3 = 0;
        //        ///Channel_4_Value
        //        if (LP_ChannelInfo.Count > 3 && LP_ChannelInfo[3] != null)
        //        {
        //            StOBISCode OBIS_Channel_4 = LP_ChannelInfo[3].OBIS_Index;
        //            GP_Row.Channel_4 = OBIS_Channel_4.OBIS_Value;
        //        }
        //        else
        //            GP_Row.Channel_4 = 0;

        //        #endregion
        //        ///Insert New Load Profile Channels Group
        //        LoadedConfigurations.LoadProfileChannel_Group.AddLoadProfileChannel_GroupRow(GP_Row);
        //        GroupId = (uint)GP_Row.load_profile_group_id;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error occurred while saving load profile channel group", ex);
        //    }
        //}

        #region Copy DataTable Rows

        private Configs.ConfigurationRow CreateConfigurationRow(Configs.ConfigurationRow OtherObj)
        {
            try
            {
                Configs.ConfigurationRow NewRow = LoadedConfigurations.Configuration.NewConfigurationRow();

                NewRow.Name = String.Format("{0}", OtherObj.Name);
                NewRow.BillItemsGroupId = OtherObj.BillItemsGroupId;
                NewRow.DisplayWindowGroupId = OtherObj.DisplayWindowGroupId;
                NewRow.EventGroupId = OtherObj.EventGroupId;
                NewRow.lp_channels_group_id = OtherObj.lp_channels_group_id;

                return NewRow;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error copying configuration row"), ex);
            }
        }


        private Configs.LoadProfile_GroupRow CreateLoadProfileGroupRow(Configs.LoadProfile_GroupRow OtherObj)
        {
            try
            {
                Configs.LoadProfile_GroupRow NewRow = LoadedConfigurations.LoadProfile_Group.NewLoadProfile_GroupRow();
                NewRow.LoadProfile_Group_Name = OtherObj.LoadProfile_Group_Name;

                return NewRow;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error Copying LoadProfileGroup Row"), ex);
            }
        }

        private Configs.DisplayWindows_GroupRow CreateDisplayWindowGroupRow(Configs.DisplayWindows_GroupRow OtherObj)
        {
            try
            {
                Configs.DisplayWindows_GroupRow NewRow = LoadedConfigurations.DisplayWindows_Group.NewDisplayWindows_GroupRow();
                NewRow.Dw_Group_Name = OtherObj.Dw_Group_Name;

                return NewRow;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error Copying DisplayWindowGroup Row"), ex);
            }
        }

        private Configs.BillingItem_GroupRow CreateBillingItemGroupRow(Configs.BillingItem_GroupRow OtherObj)
        {
            try
            {
                Configs.BillingItem_GroupRow NewRow = LoadedConfigurations.BillingItem_Group.NewBillingItem_GroupRow();
                NewRow.BillingItem_Group_Name = OtherObj.BillingItem_Group_Name;

                return NewRow;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error Copying BillItemGroup Row"), ex);
            }
        }

        private Configs.Events_GroupRow CreateEventInfoGroupRow(Configs.Events_GroupRow OtherObj)
        {
            try
            {
                Configs.Events_GroupRow NewRow = LoadedConfigurations.Events_Group.NewEvents_GroupRow();
                NewRow.Events_group_Name = OtherObj.Events_group_Name;

                return NewRow;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error Copying EventGroup Row"), ex);
            }
        }


        private Configs.DisplayWindowsRow CreateDisplayWindow(Configs.DisplayWindowsRow OtherObj)
        {
            try
            {
                Configs.DisplayWindowsRow NewRow = LoadedConfigurations.DisplayWindows.NewDisplayWindowsRow();

                NewRow.AttributeNo = OtherObj.AttributeNo;
                NewRow.Category = OtherObj.Category;
                NewRow.QuantityIndex = OtherObj.QuantityIndex;
                NewRow.SequenceId = OtherObj.SequenceId;
                NewRow.DisplayWindowsGroupId = OtherObj.DisplayWindowsGroupId;
                NewRow.WinNumberToDisplay = OtherObj.WinNumberToDisplay;

                return NewRow;

            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error copying Display Window row"), ex);
            }
        }

        private Configs.LoadProfileChannelsRow CreateLoadProfileItem(Configs.LoadProfileChannelsRow OtherObj)
        {
            try
            {
                Configs.LoadProfileChannelsRow NewRow = LoadedConfigurations.LoadProfileChannels.NewLoadProfileChannelsRow();

                NewRow.AttributeNo = OtherObj.AttributeNo;
                NewRow.Multiplier = OtherObj.Multiplier;
                NewRow.QuantityIndex = OtherObj.QuantityIndex;
                NewRow.SequenceId = OtherObj.SequenceId;
                NewRow.FormatSpecifier = OtherObj.FormatSpecifier;
                NewRow.Unit = OtherObj.Unit;
                NewRow.LoadProfileGroupId = OtherObj.LoadProfileGroupId;

                return NewRow;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error copying load profile item"), ex);
            }
        }

        private Configs.BillingItemsRow CreateBillingItem(Configs.BillingItemsRow OtherObj)
        {
            try
            {
                Configs.BillingItemsRow NewRow = LoadedConfigurations.BillingItems.NewBillingItemsRow();
                NewRow.BillItemGroupId = OtherObj.BillItemGroupId;
                NewRow.FormatSpecifier = OtherObj.FormatSpecifier;
                NewRow.Label = OtherObj.Label;
                NewRow.SequenceId = OtherObj.SequenceId;
                // NewRow.id = OtherObj.id;
                NewRow.Unit = OtherObj.Unit;
                NewRow.Multiplier = OtherObj.Multiplier;
                return NewRow;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error copying billing item"), ex);
            }
        }

        private Configs.BillTariffQuantityRow CreateBillTariffQuantity(Configs.BillTariffQuantityRow OtherObj)
        {
            try
            {
                Configs.BillTariffQuantityRow NewRow = LoadedConfigurations.BillTariffQuantity.NewBillTariffQuantityRow();

                NewRow.BillItemId = OtherObj.BillItemId;
                NewRow.OBIS_Index = OtherObj.OBIS_Index;
                NewRow.SequenceId = OtherObj.SequenceId;

                return NewRow;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error copying billing item tariff quantity"), ex);
            }
        }

        private Configs.EventInfoRow CreateEventInfoItem(Configs.EventInfoRow OtherObj)
        {
            try
            {
                Configs.EventInfoRow NewRow = LoadedConfigurations.EventInfo.NewEventInfoRow();

                NewRow.EventGroupId = OtherObj.EventGroupId;
                NewRow.EventCode = OtherObj.EventCode;
                NewRow.EventNo = OtherObj.EventNo;
                NewRow.Label = OtherObj.Label;
                NewRow.MaxEventCount = OtherObj.MaxEventCount;
                // NewRow.id = OtherObj.id;

                return NewRow;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error copying event info object"), ex);
            }
        }

        private Configs.EventLogsRow CreateEventLogInfoItem(Configs.EventLogsRow OtherObj)
        {
            try
            {
                Configs.EventLogsRow NewRow = LoadedConfigurations.EventLogs.NewEventLogsRow();

                NewRow.EventInfoRow = OtherObj.EventInfoRow;
                NewRow.EventCounterIndex = OtherObj.EventCounterIndex;
                NewRow.EventLogIndex = OtherObj.EventLogIndex;
                NewRow.id = OtherObj.id;

                return NewRow;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error copying event Log info object"), ex);
            }
        }

        // private Configs.Sap_ConfigRow CreateMeterSAPItem(Configs.Sap_ConfigRow OtherObj)
        // {
        //     try
        //     {
        //         Configs.Sap_ConfigRow NewRow = LoadedConfigurations.Sap_Config.NewSap_ConfigRow();

        //         NewRow.Meter_User_id = OtherObj.Meter_User_id;
        //         NewRow.Client_Sap = OtherObj.Client_Sap;
        //         NewRow.Meter_Sap = OtherObj.Meter_Sap;
        //         NewRow.Meter_Config_id = OtherObj.Meter_Config_id;

        //         return NewRow;
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception(String.Format("Error copying SAP object"), ex);
        //     }
        // }

        #endregion

        #region New Configuration Methods


        /// <summary>
        /// Returns 0 for admin 1 for reader and 2 if user does not exist
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public byte getUserType(string username, string password)
        {
            try
            {
                Configs.usersRow[] Users = null;
                Users = (Configs.usersRow[])this.LoadedConfigurations.users.Select(string.Format("user_name ='{0}' AND user_password='{1}'", username, password));
                if (Users.Length > 0)
                {
                    if (Users[0].user_type == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    return 2;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion
    }
}
