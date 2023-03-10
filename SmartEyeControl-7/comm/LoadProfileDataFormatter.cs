using System;
using System.Collections.Generic;
using System.Text;
using SmartEyeControl_7.comm;
using DLMS;
using AccurateOptocomSoftware.comm;
using Comm;
using DLMS.Comm;
using Comm.DataContainers;
using DatabaseConfiguration.DataSet;

namespace comm
{
    public interface ILoadProfileDataFormatter
    {
        StOBISCode GetOBISCode(Get_Index OBIS_Index);

        LoadProfileData MakeData(List<ILValue[]> CommObj, List<LoadProfileChannelInfo> LoadChannelInfo, List<LoadProfileChannelInfo> FixedChannelInfo);
        //LoadProfileData MakeData(List<ILValue[]> CommObj,List<LoadProfileChannelInfo> LoadChannelInfo);
        List<LoadProfileChannelInfo> GetLoadProfileItemsFormatX();
    }

    public class LoadProfileDataFormatter : ILoadProfileDataFormatter
    {
        public Func<Get_Index, StOBISCode> OBISLabelLookup;

        private Configs configurations;

        public Configs Configurations
        {
            get { return configurations; }
            set { configurations = value; }
        }
        #region ILoadProfileDataFormatter Members

        public StOBISCode GetOBISCode(Get_Index OBIS_Index)
        {
            StOBISCode obisCode = Get_Index.Dummy;

            try
            {
                if (OBISLabelLookup == null)
                    throw new NotImplementedException("OBISLabelLookup");

                obisCode = OBISLabelLookup.Invoke(OBIS_Index);
            }
            catch (Exception ex)
            {
#if DEBUG
                // if (logErr)
                System.Diagnostics.Debug.Write(string.Format("{0} OBIS Code Not Found {1}", OBIS_Index.ToString(), ex.Message));
#endif
                throw ex;
            }

            return obisCode;
        }

        public LoadProfileData MakeData(List<ILValue[]> CommObj, List<LoadProfileChannelInfo> LoadProfileChannelInfo, List<LoadProfileChannelInfo> FixedChannelInfo)
        {
            try
            {
                if (CommObj == null || CommObj.Count <= 0)
                    throw new Exception("XYX");
                if (LoadProfileChannelInfo == null || LoadProfileChannelInfo.Count <= 0)
                    throw new Exception("YY");
                LoadProfileData LoadProfileData = new LoadProfileData();
                LoadProfileData.ChannelsInfo = LoadProfileChannelInfo;
                int fixedObjectCount = 0;

                var Meter_Clock = GetOBISCode(Get_Index.Meter_Clock);
                var Load_Profile_Counter = GetOBISCode(Get_Index.Load_Profile_Counter);
                var Load_Profile_Counter_2 = GetOBISCode(Get_Index.Load_Profile_Counter_2);
                var Load_Profile_Capture_Period = GetOBISCode(Get_Index.Load_Profile_Capture_Period);
                var Status_Word = GetOBISCode(Get_Index.Status_Word);

                List<LoadProfileItem> LoadProfileInstances = new List<LoadProfileItem>();
                uint count = 1;
                foreach (var LoadProfileCapture in CommObj)
                {
                    LoadProfileItem LoadProfileInstance = new LoadProfileItem();
                    LoadProfileInstance.Counter = count;

                    #region Store FIXED LP Objects

                    // Store LoadProfile Date&Time Stamp
                    ILValue TVal = Array.Find<ILValue>(LoadProfileCapture, (x) => x.OBIS_Index == Meter_Clock);
                    if (TVal.Value != null && TVal.Value.GetType() == typeof(StDateTime))
                    {
                        fixedObjectCount++;
                        StDateTime timeStamp = (StDateTime)TVal.Value;
                        if (timeStamp.IsDateTimeConvertible)
                            LoadProfileInstance.DateTimeStamp = timeStamp.GetDateTime();
                    }

                    // Store Load Profile Counter
                    TVal = Array.Find<ILValue>(LoadProfileCapture, (x) => x.OBIS_Index == Load_Profile_Counter ||
                                                                          x.OBIS_Index == Load_Profile_Counter_2);
                    if (TVal != null)
                    {
                        fixedObjectCount++;
                        LoadProfileInstance.Counter = Convert.ToUInt32(TVal.Value);
                    }

                    // Load Profile Capture Period
                    TVal = Array.Find<ILValue>(LoadProfileCapture, (x) => x.OBIS_Index == Load_Profile_Capture_Period);
                    if (TVal != null)
                    {
                        fixedObjectCount++;
                        LoadProfileInstance.Interval = Convert.ToUInt32(TVal.Value) / 60;
                    }

                    // Load Profile Status Word
                    TVal = Array.Find<ILValue>(LoadProfileCapture, (x) => x.OBIS_Index == Status_Word);
                    if (TVal != null)
                    {
                        fixedObjectCount++;
                        LoadProfileInstance.StatusWord.AddRange((byte[])TVal.Value);
                    }


                    #endregion
                    #region // Make Load Profile Instance Values

                    foreach (var item in LoadProfileChannelInfo)
                    {
                        List<ILValue> ilValues = new List<ILValue>();
                        if (item != null && FixedChannelInfo.FindAll(x => x.OBIS_Index == item.OBIS_Index).Count == 0)
                        {
                            var ItemActualCode = GetOBISCode(item.OBIS_Index);

                            ILValue val = Array.Find<ILValue>(LoadProfileCapture, (x) => x.OBIS_Index == ItemActualCode);
                            StOBISCode OBISCode = item.OBIS_Index;
                            if (val != null)
                            {
                                ilValues.Add(val);
                                double ChannelVal = double.PositiveInfinity;

                                if (item.SelectedAttribute == 0)
                                    ChannelVal = MakeChannelValue(val, item);
                                else
                                {
                                    //if (OBISCode.ClassId == 5)
                                    {
                                        // Lav
                                        if (item.SelectedAttribute == 0x03)
                                        {
                                            val.Value = val.GetDataItemValue(0x03);
                                            ChannelVal = MakeChannelValue(val, item);
                                        }
                                        // Cav
                                        else if (item.SelectedAttribute == 0x02)
                                        {
                                            val.Value = val.GetDataItemValue(0x02);
                                            ChannelVal = MakeChannelValue(val, item);
                                        }
                                        else
                                        {
                                            val.Value = val.GetDataItemValue(0x03);
                                            ChannelVal = MakeChannelValue(val, item);
                                        }
                                    }
                                }

                                LoadProfileInstance.LoadProfileInstance.Add(ChannelVal);
                                item.IsDataPresent = true;
                            }
                            else
                                item.IsDataPresent = false;
                        }
                    }

                    #endregion

                    LoadProfileInstances.Add(LoadProfileInstance);
                    count++;
                }
                LoadProfileData.ChannelsInstances = LoadProfileInstances;
                return LoadProfileData;

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred generating Load Profile data", ex);
            }
        }
        //public LoadProfileData MakeData(List<ILValue[]> CommObj, List<LoadProfileChannelInfo> LoadProfileChannelInfo)
        //{
        //    try
        //    {
        //        if (CommObj == null || CommObj.Count <= 0)
        //            throw new Exception("XYX");
        //        if(LoadProfileChannelInfo == null || LoadProfileChannelInfo.Count <= 0)
        //            throw new Exception("YY");
        //        LoadProfileData LoadProfileData = new LoadProfileData();
        //        LoadProfileData.ChannelsInfo = LoadProfileChannelInfo;
                
        //        int fixedObjectCount = 0;

        //        var Meter_Clock = GetOBISCode(Get_Index.Meter_Clock);
        //        var Load_Profile_Counter = GetOBISCode(Get_Index.Load_Profile_Counter);
        //        var Load_Profile_Counter_2 = GetOBISCode(Get_Index.Load_Profile_Counter_2);
        //        var Load_Profile_Capture_Period = GetOBISCode(Get_Index.Load_Profile_Capture_Period);
        //        var Status_Word = GetOBISCode(Get_Index.Status_Word); 

        //        List<LoadProfileItem> LoadProfileInstances = new List<LoadProfileItem>();
        //        uint count = 1;
        //        foreach (var LoadProfileCapture in CommObj)
        //        {
        //            LoadProfileItem LoadProfileInstance = new LoadProfileItem();
        //            LoadProfileInstance.Counter = count;
        //            ///Store  LoadProfile Date & Time Stamp
        //            ILValue TVal = Array.Find<ILValue>(LoadProfileCapture, (x) => x.OBIS_Index == Get_Index.Meter_Clock);
        //            if (TVal.Value != null && TVal.Value.GetType() == typeof(StDateTime))
        //            {
        //                fixedObjectCount++;
        //                StDateTime timeStamp = (StDateTime)TVal.Value;
        //                if (timeStamp.IsDateTimeConvertible)
        //                    LoadProfileInstance.DateTimeStamp = timeStamp.GetDateTime();
        //            }
        //            ///Store Billing Period Counter
        //            TVal = Array.Find<ILValue>(LoadProfileCapture, (x) => x.OBIS_Index == Get_Index.Load_Profile_Counter);
        //            if (TVal != null)
        //            {
        //                fixedObjectCount++;
        //                LoadProfileInstance.Counter = Convert.ToUInt32(TVal.Value);
        //            }
        //            ///razaahsan
        //            ///
        //            TVal = Array.Find<ILValue>(LoadProfileCapture, (x) => x.OBIS_Index == Get_Index.Load_Profile_Capture_Period);
        //            if (TVal != null)
        //            {
        //                fixedObjectCount++;
        //                LoadProfileInstance.Interval = Convert.ToUInt32(TVal.Value)/60;
        //            }

        //            // Load Profile Status Word
        //            TVal = Array.Find<ILValue>(LoadProfileCapture, (x) => x.OBIS_Index == Status_Word);
        //            if (TVal != null)
        //            {
        //                fixedObjectCount++;
        //                LoadProfileInstance.StatusWord.AddRange((byte[])TVal.Value);
        //            } 

        //            ///Make Load Profile Instance Values
        //            foreach (var item in LoadProfileChannelInfo)
        //            {
        //                List<ILValue> ilValues = new List<ILValue>();
        //                if (item != null)
        //                {
        //                    var ItemActualCode = GetOBISCode(item.OBIS_Index); 
        //                    ILValue val = Array.Find<ILValue>(LoadProfileCapture, (x) => x.OBIS_Index == ItemActualCode);
        //                    StOBISCode OBISCode = item.OBIS_Index;
        //                    if (val != null)
        //                    {
        //                        ilValues.Add(val);
        //                        double ChannelVal  = double.PositiveInfinity;
        //                        if (item.SelectedAttribute == 0)
        //                            ChannelVal = MakeChannelValue(val, item);
        //                        else
        //                        {
        //                            if (OBISCode.ClassId == 5)
        //                            {
        //                                ///Lav
        //                                if (item.SelectedAttribute == 0x03)
        //                                {
        //                                    val.Value = val.GetDataItemValue(0x03);
        //                                    ChannelVal = MakeChannelValue(val, item);
        //                                }
        //                                ///Cav
        //                                else if (item.SelectedAttribute == 0x02)
        //                                {
        //                                    val.Value = val.GetDataItemValue(0x02);
        //                                    ChannelVal = MakeChannelValue(val, item);
        //                                }
        //                                else
        //                                { 
        //                                     val.Value = val.GetDataItemValue(0x03);
        //                                    ChannelVal = MakeChannelValue(val, item);
        //                                }
        //                            }
        //                        }
        //                        LoadProfileInstance.LoadProfileInstance.Add(ChannelVal);
        //                        item.IsDataPresent = true;
        //                    }
        //                    else
        //                        item.IsDataPresent = false;
        //                }
        //            }

        //            LoadProfileInstances.Add(LoadProfileInstance);
        //            count++;
        //        }
        //        LoadProfileData.ChannelsInstances = LoadProfileInstances;
        //        return LoadProfileData;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error occurred generating Load Profile data", ex);
        //    }
        //}
        #endregion
        
        public List<LoadProfileChannelInfo> GetLoadProfileItemsFormatX()
        {
            List<LoadProfileChannelInfo> LoadProfile_SelectableList = new List<LoadProfileChannelInfo>();
            ///Make Billing Data Format Fix May Load Lator from file 
            
            #region Cumulative_Tariff1_KwhExport
            ///Cumulative_Tariff1_KwhImport
            LoadProfileChannelInfo Load_Profile_Selectable_entity = new LoadProfileChannelInfo();
            Load_Profile_Selectable_entity.Quantity_Name = "Active Energy Import Tariff 1";
            Load_Profile_Selectable_entity.Unit = Unit.kWh;
            Load_Profile_Selectable_entity.Format = "F3";
            Load_Profile_Selectable_entity.IsDataPresent = false;
            Load_Profile_Selectable_entity.OBIS_Index = Get_Index.Cumulative_Tariff1_KwhImport;
            Load_Profile_Selectable_entity.CapturePeriod = new TimeSpan(0);
            Load_Profile_Selectable_entity.Multiplier =  -3.0f;

            LoadProfile_SelectableList.Add(Load_Profile_Selectable_entity);


            ///Cumulative_Tariff2_KwhImport
            Load_Profile_Selectable_entity = new LoadProfileChannelInfo();
            Load_Profile_Selectable_entity.Quantity_Name = "Active Energy Import Tariff 2";
            Load_Profile_Selectable_entity.Unit = Unit.kWh;
            Load_Profile_Selectable_entity.Format = "F3";
            Load_Profile_Selectable_entity.IsDataPresent = false;
            Load_Profile_Selectable_entity.OBIS_Index = Get_Index.Cumulative_Tariff2_KwhImport;
            Load_Profile_Selectable_entity.CapturePeriod = new TimeSpan(0);
            Load_Profile_Selectable_entity.Multiplier = -3.0f;

            LoadProfile_SelectableList.Add(Load_Profile_Selectable_entity);



            ///Cumulative_Tariff3_KwhImport
            Load_Profile_Selectable_entity = new LoadProfileChannelInfo();
            Load_Profile_Selectable_entity.Quantity_Name = "Active Energy Import Tariff 3";
            Load_Profile_Selectable_entity.Unit = Unit.kWh;
            Load_Profile_Selectable_entity.Format = "F3";
            Load_Profile_Selectable_entity.IsDataPresent = false;
            Load_Profile_Selectable_entity.OBIS_Index = Get_Index.Cumulative_Tariff3_KwhImport;
            Load_Profile_Selectable_entity.CapturePeriod = new TimeSpan(0);
            Load_Profile_Selectable_entity.Multiplier = -3.0f;

            LoadProfile_SelectableList.Add(Load_Profile_Selectable_entity);


            ///Cumulative_Tariff4_KwhImport
            Load_Profile_Selectable_entity = new LoadProfileChannelInfo();
            Load_Profile_Selectable_entity.Quantity_Name = "Active Energy Import Tariff 4";
            Load_Profile_Selectable_entity.Unit = Unit.kWh;
            Load_Profile_Selectable_entity.Format = "F3";
            Load_Profile_Selectable_entity.IsDataPresent = false;
            Load_Profile_Selectable_entity.OBIS_Index = Get_Index.Cumulative_Tariff4_KwhImport;
            Load_Profile_Selectable_entity.CapturePeriod = new TimeSpan(0);
            Load_Profile_Selectable_entity.Multiplier = -3.0f;

            LoadProfile_SelectableList.Add(Load_Profile_Selectable_entity);


            ///Cumulative_TariffTL_KwhImport
            Load_Profile_Selectable_entity = new LoadProfileChannelInfo();
            Load_Profile_Selectable_entity.Quantity_Name = "Active Energy Import Total Tariff";
            Load_Profile_Selectable_entity.Unit = Unit.kWh;
            Load_Profile_Selectable_entity.Format = "F3";
            Load_Profile_Selectable_entity.IsDataPresent = false;
            Load_Profile_Selectable_entity.OBIS_Index = Get_Index.Cumulative_TariffTL_KwhImport;
            Load_Profile_Selectable_entity.CapturePeriod = new TimeSpan(0);
            Load_Profile_Selectable_entity.Multiplier = -3.0f;

            LoadProfile_SelectableList.Add(Load_Profile_Selectable_entity); 
            #endregion
            
            #region Cumulative_Tariff1_KwhExport
            ///Cumulative_Tariff1_KwhExport
            Load_Profile_Selectable_entity = new LoadProfileChannelInfo();
            Load_Profile_Selectable_entity.Quantity_Name = "Active Energy Export Tariff 1";
            Load_Profile_Selectable_entity.Unit = Unit.kWh;
            Load_Profile_Selectable_entity.Format = "F3";
            Load_Profile_Selectable_entity.IsDataPresent = false;
            Load_Profile_Selectable_entity.OBIS_Index = Get_Index.Cumulative_Tariff1_KwhExport;
            Load_Profile_Selectable_entity.CapturePeriod = new TimeSpan(0);
            Load_Profile_Selectable_entity.Multiplier = -3.0f;

            LoadProfile_SelectableList.Add(Load_Profile_Selectable_entity);


            ///Cumulative_Tariff2_KwhExport
            Load_Profile_Selectable_entity = new LoadProfileChannelInfo();
            Load_Profile_Selectable_entity.Quantity_Name = "Active Energy Export Tariff 2";
            Load_Profile_Selectable_entity.Unit = Unit.kWh;
            Load_Profile_Selectable_entity.Format = "F3";
            Load_Profile_Selectable_entity.IsDataPresent = false;
            Load_Profile_Selectable_entity.OBIS_Index = Get_Index.Cumulative_Tariff2_KwhExport;
            Load_Profile_Selectable_entity.CapturePeriod = new TimeSpan(0);
            Load_Profile_Selectable_entity.Multiplier = -3.0f;

            LoadProfile_SelectableList.Add(Load_Profile_Selectable_entity);



            ///Cumulative_Tariff3_KwhExport
            Load_Profile_Selectable_entity = new LoadProfileChannelInfo();
            Load_Profile_Selectable_entity.Quantity_Name = "Active Energy Export Tariff 3";
            Load_Profile_Selectable_entity.Unit = Unit.kWh;
            Load_Profile_Selectable_entity.Format = "F3";
            Load_Profile_Selectable_entity.IsDataPresent = false;
            Load_Profile_Selectable_entity.OBIS_Index = Get_Index.Cumulative_Tariff3_KwhExport;
            Load_Profile_Selectable_entity.CapturePeriod = new TimeSpan(0);
            Load_Profile_Selectable_entity.Multiplier = -3.0f;

            LoadProfile_SelectableList.Add(Load_Profile_Selectable_entity);


            ///Cumulative_Tariff4_KwhExport
            Load_Profile_Selectable_entity = new LoadProfileChannelInfo();
            Load_Profile_Selectable_entity.Quantity_Name = "Active Energy Export Tariff 4";
            Load_Profile_Selectable_entity.Unit = Unit.kWh;
            Load_Profile_Selectable_entity.Format = "F3";
            Load_Profile_Selectable_entity.IsDataPresent = false;
            Load_Profile_Selectable_entity.OBIS_Index = Get_Index.Cumulative_Tariff4_KwhExport;
            Load_Profile_Selectable_entity.CapturePeriod = new TimeSpan(0);
            Load_Profile_Selectable_entity.Multiplier = -3.0f;

            LoadProfile_SelectableList.Add(Load_Profile_Selectable_entity);


            ///Cumulative_TariffTL_KwhExport
            Load_Profile_Selectable_entity = new LoadProfileChannelInfo();
            Load_Profile_Selectable_entity.Quantity_Name = "Active Energy Export Total Tariff";
            Load_Profile_Selectable_entity.Unit = Unit.kWh;
            Load_Profile_Selectable_entity.Format = "F3";
            Load_Profile_Selectable_entity.IsDataPresent = false;
            Load_Profile_Selectable_entity.OBIS_Index = Get_Index.Cumulative_TariffTL_KwhExport;
            Load_Profile_Selectable_entity.CapturePeriod = new TimeSpan(0);
            Load_Profile_Selectable_entity.Multiplier = -3.0f;

            LoadProfile_SelectableList.Add(Load_Profile_Selectable_entity); 
            #endregion

            #region Cumulative_TariffTL_KwhAbsolute
            ///Cumulative_TariffT1_KwhAbsolute
            Load_Profile_Selectable_entity = new LoadProfileChannelInfo();
            Load_Profile_Selectable_entity.Quantity_Name = "Active Energy Absolute Tariff 1";
            Load_Profile_Selectable_entity.Unit = Unit.kWh;
            Load_Profile_Selectable_entity.Format = "F3";
            Load_Profile_Selectable_entity.IsDataPresent = false;
            Load_Profile_Selectable_entity.OBIS_Index = Get_Index.Cumulative_Tariff1_KwhAbsolute;
            Load_Profile_Selectable_entity.CapturePeriod = new TimeSpan(0);
            Load_Profile_Selectable_entity.Multiplier = -3.0f;

            LoadProfile_SelectableList.Add(Load_Profile_Selectable_entity);


            ///Cumulative_TariffT2_KwhAbsolute
            Load_Profile_Selectable_entity = new LoadProfileChannelInfo();
            Load_Profile_Selectable_entity.Quantity_Name = "Active Energy Absolute Tariff 2";
            Load_Profile_Selectable_entity.Unit = Unit.kWh;
            Load_Profile_Selectable_entity.Format = "F3";
            Load_Profile_Selectable_entity.IsDataPresent = false;
            Load_Profile_Selectable_entity.OBIS_Index = Get_Index.Cumulative_Tariff2_KwhAbsolute;
            Load_Profile_Selectable_entity.CapturePeriod = new TimeSpan(0);
            Load_Profile_Selectable_entity.Multiplier = -3.0f;

            LoadProfile_SelectableList.Add(Load_Profile_Selectable_entity);



            ///Cumulative_TariffT3_KwhAbsolute
            Load_Profile_Selectable_entity = new LoadProfileChannelInfo();
            Load_Profile_Selectable_entity.Quantity_Name = "Active Energy Absolute Tariff 3";
            Load_Profile_Selectable_entity.Unit = Unit.kWh;
            Load_Profile_Selectable_entity.Format = "F3";
            Load_Profile_Selectable_entity.IsDataPresent = false;
            Load_Profile_Selectable_entity.OBIS_Index = Get_Index.Cumulative_Tariff3_KwhAbsolute;
            Load_Profile_Selectable_entity.CapturePeriod = new TimeSpan(0);
            Load_Profile_Selectable_entity.Multiplier = -3.0f;

            LoadProfile_SelectableList.Add(Load_Profile_Selectable_entity);


            ///Cumulative_TariffT4_KwhAbsolute
            Load_Profile_Selectable_entity = new LoadProfileChannelInfo();
            Load_Profile_Selectable_entity.Quantity_Name = "Active Energy Absolute Tariff 4";
            Load_Profile_Selectable_entity.Unit = Unit.kWh;
            Load_Profile_Selectable_entity.Format = "F3";
            Load_Profile_Selectable_entity.IsDataPresent = false;
            Load_Profile_Selectable_entity.OBIS_Index = Get_Index.Cumulative_Tariff4_KwhAbsolute;
            Load_Profile_Selectable_entity.CapturePeriod = new TimeSpan(0);
            Load_Profile_Selectable_entity.Multiplier = -3.0f;

            LoadProfile_SelectableList.Add(Load_Profile_Selectable_entity);


            ///Cumulative_TariffTL_KwhAbsolute
            Load_Profile_Selectable_entity = new LoadProfileChannelInfo();
            Load_Profile_Selectable_entity.Quantity_Name = "Active Energy Absolute Total";
            Load_Profile_Selectable_entity.Unit = Unit.kWh;
            Load_Profile_Selectable_entity.Format = "F3";
            Load_Profile_Selectable_entity.IsDataPresent = false;
            Load_Profile_Selectable_entity.OBIS_Index = Get_Index.Cumulative_TariffTL_KwhAbsolute;
            Load_Profile_Selectable_entity.CapturePeriod = new TimeSpan(0);
            Load_Profile_Selectable_entity.Multiplier = -3.0f;

            LoadProfile_SelectableList.Add(Load_Profile_Selectable_entity);
            #endregion

            #region Active_Power_Ph_A_Pos
            ///Active_Power_Ph_A_Pos
            Load_Profile_Selectable_entity = new LoadProfileChannelInfo();
            Load_Profile_Selectable_entity.Quantity_Name = "Active Power Phase A Positive";
            Load_Profile_Selectable_entity.Unit = Unit.kW;
            Load_Profile_Selectable_entity.Format = "F3";
            Load_Profile_Selectable_entity.IsDataPresent = false;
            Load_Profile_Selectable_entity.OBIS_Index = Get_Index.Active_Power_Ph_A_Pos;
            Load_Profile_Selectable_entity.CapturePeriod = new TimeSpan(0);
            Load_Profile_Selectable_entity.Multiplier = -3.0f;

            LoadProfile_SelectableList.Add(Load_Profile_Selectable_entity);


            ///Active_Power_Ph_B_Pos
            Load_Profile_Selectable_entity = new LoadProfileChannelInfo();
            Load_Profile_Selectable_entity.Quantity_Name = "Active Power Phase B Positive";
            Load_Profile_Selectable_entity.Unit = Unit.kW;
            Load_Profile_Selectable_entity.Format = "F3";
            Load_Profile_Selectable_entity.IsDataPresent = false;
            Load_Profile_Selectable_entity.OBIS_Index = Get_Index.Active_Power_Ph_B_Pos;
            Load_Profile_Selectable_entity.CapturePeriod = new TimeSpan(0);
            Load_Profile_Selectable_entity.Multiplier = -3.0f;

            LoadProfile_SelectableList.Add(Load_Profile_Selectable_entity);



            ///Active_Power_Ph_C_Pos
            Load_Profile_Selectable_entity = new LoadProfileChannelInfo();
            Load_Profile_Selectable_entity.Quantity_Name = "Active Power Phase C Positive";
            Load_Profile_Selectable_entity.Unit = Unit.kW;
            Load_Profile_Selectable_entity.Format = "F3";
            Load_Profile_Selectable_entity.IsDataPresent = false;
            Load_Profile_Selectable_entity.OBIS_Index = Get_Index.Active_Power_Ph_C_Pos;
            Load_Profile_Selectable_entity.CapturePeriod = new TimeSpan(0);
            Load_Profile_Selectable_entity.Multiplier = -3.0f;

            LoadProfile_SelectableList.Add(Load_Profile_Selectable_entity);


            ///Active_Power_Ph_Total_Pos
            Load_Profile_Selectable_entity = new LoadProfileChannelInfo();
            Load_Profile_Selectable_entity.Quantity_Name = "Active Power Total Positive";
            Load_Profile_Selectable_entity.Unit = Unit.kW;
            Load_Profile_Selectable_entity.Format = "F3";
            Load_Profile_Selectable_entity.IsDataPresent = false;
            Load_Profile_Selectable_entity.OBIS_Index = Get_Index.Active_Power_Total_Pos;
            Load_Profile_Selectable_entity.CapturePeriod = new TimeSpan(0);
            Load_Profile_Selectable_entity.Multiplier = -3.0f;

            LoadProfile_SelectableList.Add(Load_Profile_Selectable_entity);

            #endregion

            #region Active_Power_Ph_A_Neg
            ///Active_Power_Ph_A_Neg
            Load_Profile_Selectable_entity = new LoadProfileChannelInfo();
            Load_Profile_Selectable_entity.Quantity_Name = "Active Power Phase A Negative";
            Load_Profile_Selectable_entity.Unit = Unit.kW;
            Load_Profile_Selectable_entity.Format = "F3";
            Load_Profile_Selectable_entity.IsDataPresent = false;
            Load_Profile_Selectable_entity.OBIS_Index = Get_Index.Active_Power_Ph_A_Neg;
            Load_Profile_Selectable_entity.CapturePeriod = new TimeSpan(0);
            Load_Profile_Selectable_entity.Multiplier = -3.0f;

            LoadProfile_SelectableList.Add(Load_Profile_Selectable_entity);


            ///Active_Power_Ph_B_Neg
            Load_Profile_Selectable_entity = new LoadProfileChannelInfo();
            Load_Profile_Selectable_entity.Quantity_Name = "Active Power Phase B Negative";
            Load_Profile_Selectable_entity.Unit = Unit.kW;
            Load_Profile_Selectable_entity.Format = "F3";
            Load_Profile_Selectable_entity.IsDataPresent = false;
            Load_Profile_Selectable_entity.OBIS_Index = Get_Index.Active_Power_Ph_B_Neg;
            Load_Profile_Selectable_entity.CapturePeriod = new TimeSpan(0);
            Load_Profile_Selectable_entity.Multiplier = -3.0f;

            LoadProfile_SelectableList.Add(Load_Profile_Selectable_entity);

            ///Active_Power_Ph_C_Neg
            Load_Profile_Selectable_entity = new LoadProfileChannelInfo();
            Load_Profile_Selectable_entity.Quantity_Name = "Active Power Phase C Negative";
            Load_Profile_Selectable_entity.Unit = Unit.kW;
            Load_Profile_Selectable_entity.Format = "F3";
            Load_Profile_Selectable_entity.IsDataPresent = false;
            Load_Profile_Selectable_entity.OBIS_Index = Get_Index.Active_Power_Ph_C_Neg;
            Load_Profile_Selectable_entity.CapturePeriod = new TimeSpan(0);
            Load_Profile_Selectable_entity.Multiplier = -3.0f;

            LoadProfile_SelectableList.Add(Load_Profile_Selectable_entity);


            ///Active_Power_Ph_Total_Neg
            Load_Profile_Selectable_entity = new LoadProfileChannelInfo();
            Load_Profile_Selectable_entity.Quantity_Name = "Active Power Total Negative";
            Load_Profile_Selectable_entity.Unit = Unit.kW;
            Load_Profile_Selectable_entity.Format = "F3";
            Load_Profile_Selectable_entity.IsDataPresent = false;
            Load_Profile_Selectable_entity.OBIS_Index = Get_Index.Active_Power_Total_Neg;
            Load_Profile_Selectable_entity.CapturePeriod = new TimeSpan(0);
            Load_Profile_Selectable_entity.Multiplier = -3.0f;

            LoadProfile_SelectableList.Add(Load_Profile_Selectable_entity);
            #endregion
            
            return LoadProfile_SelectableList;
        
        }

        public List<LoadProfileChannelInfo> GetLoadProfileItemsFormatX(Configs ApplicationConfigurator)
        {
            try
            {
                ConfigsHelper Configurator = new ConfigsHelper(ApplicationConfigurator);
                List<LoadProfileChannelInfo> AllSelectableChannels = Configurator.GetAllSelectableLoadProfileChannels();
                return AllSelectableChannels;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to convert all load profile channel values from data soucrce", ex);
            }
        }

        public List<LoadProfileChannelInfo> GetLoadProfileItemsFormatX(int dummy)
        {
            try
            {
                ConfigsHelper Configurator = new ConfigsHelper(Configurations);
                List<LoadProfileChannelInfo> AllSelectableChannels = Configurator.GetAllSelectableLoadProfileChannels();
                return AllSelectableChannels;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to convert all load profile channel values from data soucrce", ex);
            }
        }

        private double MakeChannelValue(ILValue val, LoadProfileChannelInfo ChannelInfo)
        { 
            try
            {
                if (val == null || val.Value == null || !(val.Value is ValueType))
                    return double.NaN;
                double rawVal = Convert.ToDouble(val.Value);
                if(ChannelInfo.Multiplier < 0)
                {
                     rawVal = rawVal / (Math.Pow(10,Math.Abs(ChannelInfo.Multiplier)));
                }
                else if(ChannelInfo.Multiplier > 0)
                {
                    rawVal = rawVal * (Math.Pow(10,Math.Abs(ChannelInfo.Multiplier)));
                }
                return rawVal;
            }
            catch (Exception ex)
            {
                return double.PositiveInfinity;
            }
        }
    }
}
