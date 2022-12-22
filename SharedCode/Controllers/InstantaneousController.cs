using comm.DataContainer;
using DLMS;
using DLMS.Comm;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.Param;
using SharedCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace SharedCode.Controllers
{
    public class InstantaneousController
    {
        #region Data_Members
        GenericProfileInfo instantaneousInfo;
        private ApplicationProcess_Controller _AP_Controller;
        private Configurator _Configurator;
        private ConnectionInfo connInfo;

        private byte _lastIOFailureCount = 0;

        public InstantaneousController()
        {
        }
        public ApplicationProcess_Controller AP_Controller
        {
            get { return _AP_Controller; }
            set { _AP_Controller = value; }
        }
        #endregion

        #region Properties
        public Configurator Configurator
        {
            get { return _Configurator; }
            set { _Configurator = value; }
        }

        public ConnectionInfo ConnectionInfo
        {
            get { return connInfo; }
            set { connInfo = value; }
        }

        public byte LastIOFailureCount
        {
            get { return _lastIOFailureCount; }
            set { _lastIOFailureCount = value; }
        }

        #endregion

        #region Old SEAC Code Added

        public List<CB_DayRecord> Format_CB_DAYData(List<ILValue[]> Raw_DayRecord_Data)
        {
            try
            {
                List<CB_DayRecord> List_items = new List<CB_DayRecord>();
                CB_DayRecord item = null;
                ILValue temp = new ILValue();
                ILValue[] value = new ILValue[1];

                for (int i = 0; i < Raw_DayRecord_Data.Count; i++)
                {
                    item = new CB_DayRecord();
                    value = Raw_DayRecord_Data[i];
                    temp = Array.Find(value, x => x.OBIS_Index == Get_Index.Dummy_CB_DAY_RECORD);
                    item.RAWBYTES = (byte[])(temp.Value);
                    List_items.Add(item);
                }

                return List_items;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CaptureObject> Initialize_CB_DayRecord_ObjectList()
        {
            List<CaptureObject> listToReturn = new List<CaptureObject>();
            CaptureObject Obj_1 = new CaptureObject();

            StOBISCode OBis = Get_Index.Dummy_CB_DAY_RECORD;
            Obj_1.ClassId = 1;
            Obj_1.AttributeIndex = 0x02;
            Obj_1.DataIndex = 0;
            Obj_1.OBISCode = OBis.OBISCode;
            listToReturn.Add(Obj_1);

            return listToReturn;
        }

        public List<CB_DayRecord> ReadCumulativeDayRecord()
        {
            Class_7 cbRecord = (Class_7)GetSAPEntry(Get_Index.CB_Day_Record);
            cbRecord.captureObjectsList = Initialize_CB_DayRecord_ObjectList();

            List<ILValue[]> Ins_Periods = ReadInstantaneousDataRaw(cbRecord);
            List<CB_DayRecord> dayData = Format_CB_DAYData(Ins_Periods);
            // return dayData;
            return dayData;
        }

        public List<TL_LoadProfile> Format_TLLoadProfileData(List<ILValue[]> Raw_DayRecord_Data)
        {
            try
            {
                List<TL_LoadProfile> List_items = new List<TL_LoadProfile>();
                TL_LoadProfile item = null;
                ILValue temp = new ILValue();
                ILValue[] value = new ILValue[1];

                for (int i = 0; i < Raw_DayRecord_Data.Count; i++)
                {
                    item = new TL_LoadProfile();
                    value = Raw_DayRecord_Data[i];
                    temp = Array.Find(value, x => x.OBIS_Index == Get_Index.Dummy_TL_Load_Profile);
                    item.RAWBYTES = (byte[])(temp.Value);
                    List_items.Add(item);
                }

                return List_items;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TL_LoadProfile> Read_TLLOADPROFILE_Record()
        {
            Class_7 TLRecord = (Class_7)GetSAPEntry(Get_Index.TL_Load_Profile);
            TLRecord.captureObjectsList = Initialize_TLLoadProfile_ObjectList();

            List<ILValue[]> Ins_Periods = ReadInstantaneousDataRaw(TLRecord);
            List<TL_LoadProfile> dayData = Format_TLLoadProfileData(Ins_Periods);
            return dayData;
        }

        public List<CaptureObject> Initialize_TLLoadProfile_ObjectList()
        {
            List<CaptureObject> listToReturn = new List<CaptureObject>();
            CaptureObject Obj_1 = new CaptureObject();

            StOBISCode OBis = Get_Index.Dummy_TL_Load_Profile;
            Obj_1.ClassId = 1;
            Obj_1.AttributeIndex = 0x02;
            Obj_1.DataIndex = 0;
            Obj_1.OBISCode = OBis.OBISCode;
            listToReturn.Add(Obj_1);

            return listToReturn;
        }

        public byte[] GET_Any_ByteArray(Get_Index arg, byte attribute, byte Class_ID)
        {
            try
            {
                Base_Class Comm_Obj = GET_Object(arg, attribute);
                Instantaneous_Class instant_Obj = new Instantaneous_Class();
                return instant_Obj.Decode_Any_ByteArray(Comm_Obj, Class_ID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public List<CaptureObject> InitializeDisplayWindowObjectList()
        {
            List<CaptureObject> listToReturn = new List<CaptureObject>();
            CaptureObject Obj_1 = new CaptureObject();

            StOBISCode OBis = Get_Index.Manufacturing_ID;
            Obj_1.ClassId = 1;
            Obj_1.AttributeIndex = 0x02;
            Obj_1.DataIndex = 0;
            Obj_1.OBISCode = OBis.OBISCode;
            listToReturn.Add(Obj_1);

            return listToReturn;
        }

        public string GetDisplayWindowData(byte i)
        {
            Class_7 Instantaneous_CommObj = null;
            switch (i)
            {
                case 0:
                    Instantaneous_CommObj = (Class_7)GetSAPEntry(Get_Index.DisplayWindows_NOR_7);
                    break;
                case 1:
                    Instantaneous_CommObj = (Class_7)GetSAPEntry(Get_Index.DisplayWindows_ALT_7);
                    break;
                case 2:
                    Instantaneous_CommObj = (Class_7)GetSAPEntry(Get_Index.Display_Windows_TestMode_Get);
                    break;
            }
            Instantaneous_CommObj.captureObjectsList = InitializeDisplayWindowObjectList();

            List<ILValue[]> Ins_Periods = ReadInstantaneousDataRaw(Instantaneous_CommObj);
            ILValue[] value = new ILValue[1];
            value = Ins_Periods[0];
            ILValue temp = Array.Find(value, x => x.OBIS_Index == Get_Index.Manufacturing_ID);
            string s = Encoding.ASCII.GetString((byte[])temp.Value);
            return s;
        }

        public InstantaneousObjClass saveToClass_o(InstantaneousData data, string MSN)
        {
            try
            {
                InstantaneousItem item = new InstantaneousItem();
                InstantaneousObjClass obj = new InstantaneousObjClass();
                obj.MSN = MSN;
                obj.dateTime = data.TimeStamp;

                #region Current
                item = data.InstantaneousItems.Find(x => x.Name == "Current");
                obj.current_PhaseA = item.Value.PhaseA;
                //  item = data.InstantaneousItems.Find(x => x.Name == (Get_Index.Current_Ph_B).ToString());
                obj.current_PhaseB = item.Value.PhaseB;
                // item = data.InstantaneousItems.Find(x => x.Name == (Get_Index.Current_Ph_C).ToString());
                obj.current_PhaseC = item.Value.PhaseC;
                obj.current_PhaseTL = obj.current_PhaseA + obj.current_PhaseB + obj.current_PhaseC;
                #endregion

                #region Voltage
                item = data.InstantaneousItems.Find(x => x.Name == "Voltage");
                obj.voltage_PhaseA = item.Value.PhaseA;
                //  item = data.InstantaneousItems.Find(x => x.Name == (Get_Index.Voltage_Ph_B).ToString());
                obj.voltage_PhaseB = item.Value.PhaseB;
                //  item = data.InstantaneousItems.Find(x => x.Name == (Get_Index.Voltage_Ph_C).ToString());
                obj.voltage_PhaseC = item.Value.PhaseC;
                obj.voltage_PhaseTL = (obj.voltage_PhaseA + obj.voltage_PhaseB + obj.voltage_PhaseC) / 3;
                #endregion

                #region Active Power Positive
                item = data.InstantaneousItems.Find(x => x.Name == "Active Power Postive");
                obj.active_powerPositive_PhaseA = item.Value.PhaseA;
                //  item = data.InstantaneousItems.Find(x => x.Name == (Get_Index.Active_Power_Ph_B_Pos).ToString());
                obj.active_powerPositive_PhaseB = item.Value.PhaseB;
                //  item = data.InstantaneousItems.Find(x => x.Name == (Get_Index.Active_Power_Ph_C_Pos).ToString());
                obj.active_powerPositive_PhaseC = item.Value.PhaseC;
                // obj.active_powerPositive_PhaseTL = (obj.active_powerPositive_PhaseA + obj.active_powerPositive_PhaseB + obj.active_powerPositive_PhaseC) ;
                obj.active_powerPositive_PhaseTL = item.Value.PhaseAvg_Total;
                #endregion

                #region Active Power Negative
                item = data.InstantaneousItems.Find(x => x.Name == "Active Power Negative");
                obj.active_powerNegative_PhaseA = item.Value.PhaseA;
                //     item = data.InstantaneousItems.Find(x => x.Name == (Get_Index.Active_Power_Ph_B_Neg).ToString());
                obj.active_powerNegative_PhaseB = item.Value.PhaseB;
                //    item = data.InstantaneousItems.Find(x => x.Name == (Get_Index.Active_Power_Ph_C_Neg).ToString());
                obj.active_powerNegative_PhaseC = item.Value.PhaseC;
                // obj.active_powerNegative_PhaseTL = (obj.active_powerNegative_PhaseA + obj.active_powerNegative_PhaseB + obj.active_powerNegative_PhaseC) ;
                obj.active_powerNegative_PhaseTL = item.Value.PhaseAvg_Total;
                #endregion

                #region Reactive Power Positive
                item = data.InstantaneousItems.Find(x => x.Name == "Reactive Power Postive");
                obj.reactive_powerPositive_PhaseA = item.Value.PhaseA;
                //   item = data.InstantaneousItems.Find(x => x.Name == (Get_Index.Reactive_Power_Ph_B_Pos).ToString());
                obj.reactive_powerPositive_PhaseB = item.Value.PhaseB;
                //   item = data.InstantaneousItems.Find(x => x.Name == (Get_Index.Reactive_Power_Ph_C_Pos).ToString());
                obj.reactive_powerPositive_PhaseC = item.Value.PhaseC;
                // obj.reactive_powerPositive_PhaseTL = (obj.reactive_powerPositive_PhaseA + obj.reactive_powerPositive_PhaseB + obj.reactive_powerPositive_PhaseC) ;
                obj.reactive_powerPositive_PhaseTL = item.Value.PhaseAvg_Total;
                #endregion

                #region Reactive Power Negative
                item = data.InstantaneousItems.Find(x => x.Name == "Reactive Power Negative");
                obj.reactive_powerNegative_PhaseA = item.Value.PhaseA;
                //     item = data.InstantaneousItems.Find(x => x.Name == (Get_Index.Reactive_Power_Ph_B_Neg).ToString());
                obj.reactive_powerNegative_PhaseB = item.Value.PhaseB;
                //     item = data.InstantaneousItems.Find(x => x.Name == (Get_Index.Reactive_Power_Ph_A_Neg).ToString());
                obj.reactive_powerNegative_PhaseC = item.Value.PhaseC;
                // obj.reactive_powerNegative_PhaseTL = (obj.reactive_powerNegative_PhaseA + obj.reactive_powerNegative_PhaseB + obj.reactive_powerNegative_PhaseC) ;
                obj.reactive_powerNegative_PhaseTL = item.Value.PhaseAvg_Total;
                #endregion

                obj.frequency = data.Frequency;

                return obj;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Instantaneous_SinglePhase saveToClass_SinglePhase(InstantaneousData data, string MSN)
        {
            try
            {
                InstantaneousItem item = new InstantaneousItem();
                Instantaneous_SinglePhase obj = new Instantaneous_SinglePhase();
                obj.MSN = MSN;
                obj.dateTime = data.TimeStamp;

                #region Current
                item = data.InstantaneousItems.Find(x => x.Name == "Instantaneous Current");
                obj.current = item.Value.PhaseA;

                #endregion

                #region Voltage
                item = data.InstantaneousItems.Find(x => x.Name == "Instantaneous Voltage");
                obj.voltage = item.Value.PhaseA;

                #endregion

                #region Active Power Positive
                item = data.InstantaneousItems.Find(x => x.Name == "Instantaneous Power Factor");
                obj.powerFactor = item.Value.PhaseA;

                #endregion

                #region Active Power Negative
                item = data.InstantaneousItems.Find(x => x.Name == "Instantaneous Active Power");
                obj.instantaneousActivePower = item.Value.PhaseA;

                #endregion

                obj.frequency = data.Frequency;

                return obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public InstantaneousData FormatInstantaneousData(List<ILValue[]> RawInstantaneousData, bool isSinglePhase)
        {
            if (!isSinglePhase)
                return FormatInstantaneousData(RawInstantaneousData);
            try
            {
                InstantaneousData list_to_return = new InstantaneousData();
                List<InstantaneousItem> List_items = new List<InstantaneousItem>();
                InstantaneousItem item = new InstantaneousItem();
                ILValue temp = new ILValue();
                ILValue[] value = new ILValue[1];

                value = RawInstantaneousData[0]; //Instantaneous data is received in one Packet

                #region MeterDateTime
                //Reading Date
                temp = Array.Find(value, x => x.OBIS_Index == Get_Index.Meter_Clock);
                list_to_return.TimeStamp = (StDateTime)(temp.Value);
                #endregion

                #region Current
                //Reading Current
                temp = Array.Find(value, x => x.OBIS_Index == Get_Index.Current_Ph_A);
                item.Name = "Instantaneous Current";
                item._Unit = Unit.A;
                item.Value.PhaseA = makeValue(temp, item.Multiplier);
                List_items.Add(item);
                #endregion

                #region Voltage
                //Reading Voltage
                item = new InstantaneousItem();
                temp = Array.Find(value, x => x.OBIS_Index == Get_Index.Voltage_Ph_A);
                item.Name = "Instantaneous Voltage";
                item._Unit = Unit.V;
                item.Value.PhaseA = makeValue(temp, item.Multiplier);
                List_items.Add(item);
                #endregion

                #region Power Factor

                //Reading ActivePower_Postive
                item = new InstantaneousItem();
                temp = Array.Find(value, x => x.OBIS_Index == Get_Index.Power_Factor_All);
                item.Name = "Instantaneous Power Factor";
                item._Unit = Unit.UnitLess;
                item.Value.PhaseA = (makeValue(temp, item.Multiplier));

                List_items.Add(item);
                #endregion

                #region Instantaneous Active Power

                //Reading Instantaneous ActivePower_Postive
                item = new InstantaneousItem();
                temp = Array.Find(value, x => x.OBIS_Index == Get_Index.Active_Power_Ph_A_Pos);
                //if (temp == null)
                //{
                //    throw new Exception("Instantaneous Active power not found");
                //}
                item.Name = "Instantaneous Active Power";
                item._Unit = Unit.kW;
                item.Value.PhaseA = (makeValue(temp, item.Multiplier)) / 1000;

                List_items.Add(item);

                #endregion

                list_to_return.InstantaneousItems = List_items;
                return list_to_return;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public InstantaneousData ReadInstantaneousData(bool isSinglePhase)
        {
            ///Initialize Instantaneous Object
            Class_7 temp = new Class_7(Get_Index.CurrentPowerQualityDataProfile);
            instantaneousInfo = GetInstantanouseInfo(temp);
            Class_7 Instantaneous_CommObj = (Class_7)GetSAPEntry(Get_Index.CurrentPowerQualityDataProfile);
            List<ILValue[]> Ins_Periods = ReadInstantaneousDataRaw(Instantaneous_CommObj);
            InstantaneousData InstantaneousData = FormatInstantaneousData(Ins_Periods, isSinglePhase);
            return InstantaneousData;
        }

        public byte[] GET_Byte_Array(ref Instantaneous_Class Instantaneous_Class_obj, Get_Index arg, byte attribute, byte Class_ID)
        {
            try
            {
                Base_Class Comm_Obj = GET_Object(arg, attribute);
                return Instantaneous_Class_obj.Decode_Any_ByteArray(Comm_Obj, Class_ID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public string GET_Any_string(ref Instantaneous_Class Instantaneous_Class_obj, Get_Index arg, byte attribute, byte Class_ID)
        {
            try
            {
                Base_Class Comm_Obj = GET_Object(arg, attribute);
                return Instantaneous_Class_obj.Decode_Any_string(Comm_Obj, Class_ID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public byte[] Get_Active_Season()
        {
            try
            {
                //get active season
                Instantaneous_Class obj_Instant = new Instantaneous_Class();
                string Dummy_String = GET_Any_string(ref obj_Instant, Get_Index.Active_Season, 2, 1);
                byte[] tempBytes = Encoding.ASCII.GetBytes(Dummy_String);
                return tempBytes;
            }
            catch (Exception ex) { throw new Exception("Error occurred while reading Current Active Season," + ex.Message, ex); }
        }

        public void Get_Active_Season(Instantaneous_Class Instantaneous_Class_obj)
        {
            byte[] tempBytes = this.Get_Active_Season();

            Instantaneous_Class_obj.Active_Day_Profile = tempBytes[0];
            Instantaneous_Class_obj.Active_Season = tempBytes[1];
            Instantaneous_Class_obj.Active_Tariff = tempBytes[2];
        }

        public double GET_Any(ref Instantaneous_Class Instantaneous_Class_obj, Get_Index arg, byte attribute, byte Class_ID)
        {
            try
            {
                Base_Class Comm_Obj = GET_Object(arg, attribute);
                return Instantaneous_Class_obj.Decode_Any(Comm_Obj, Class_ID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        #endregion

        public InstantaneousData ReadInstantaneousData(List<AccessRights> instRights)
        {
            // Initialize Instantaneous Object
            try
            {
                Class_7 temp = new Class_7(Get_Index.CurrentPowerQualityDataProfile);
                instantaneousInfo = GetInstantanouseInfo(temp);
                Class_7 Instantaneous_CommObj = (Class_7)GetSAPEntry(Get_Index.CurrentPowerQualityDataProfile);
                List<ILValue[]> Ins_Periods = ReadInstantaneousDataRaw(Instantaneous_CommObj);
                InstantaneousData InstantaneousData = FormatInstantaneousData(Ins_Periods, instRights);
                return InstantaneousData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ILValue[]> ReadInstantaneousDataRaw(Class_7 Instantaneous_CommObj)
        {
            try
            {
                Instantaneous_CommObj.DecodingAttribute = 0x02;
                GET_Object(Instantaneous_CommObj);

                if (Instantaneous_CommObj.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                {
                    InstantaneousDataGenerator InstantaneousDataGenerator = new InstantaneousDataGenerator();

                    List<ILValue[]> InsPeriods = InstantaneousDataGenerator.GetData(((Class_7)Instantaneous_CommObj).buffer);
                    return InsPeriods;
                }
                else
                    throw new Exception("Error occurred while reading instantaneous data");

            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred reading ins data (Error Code:{0})", (int)MDCErrors.App_Instantaneous_Data_Read), ex);
            }
        }
        private double makeValue(ILValue val, float multiplier)
        {
            try
            {
                if (val != null)
                {
                    double rawVal = Convert.ToDouble(val.Value);
                    if (multiplier < 0)
                    {
                        rawVal = rawVal / (Math.Pow(10, Math.Abs(multiplier)));
                    }
                    else if (multiplier > 0)
                    {
                        rawVal = rawVal * (Math.Pow(10, Math.Abs(multiplier)));
                    }
                    return rawVal;
                }
                else
                    return double.NaN;

            }
            catch (Exception ex)
            {
                return double.PositiveInfinity;
            }
        }

        public InstantaneousData FormatInstantaneousData(List<ILValue[]> RawInstantaneousData, List<AccessRights> instRights)
        {
            try
            {
                InstantaneousData list_to_return = new InstantaneousData();
                List<InstantaneousItem> List_items = new List<InstantaneousItem>();
                InstantaneousItem item = new InstantaneousItem();
                ILValue temp = new ILValue();
                ILValue[] value = new ILValue[1];

                value = RawInstantaneousData[0]; // Instantaneous data is received in one Packet

                StOBISCode OBIS_Code_Strt = Get_Index.Dummy;
                string kvar_import = "Reactive Power Import", kvar_export = "Reactive Power Export";

                #region MeterDateTime

                OBIS_Code_Strt = GetOBISCode(Get_Index.Meter_Clock);
                // Reading Date
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                list_to_return.TimeStamp = (StDateTime)temp.Value;

                #endregion

                #region Current
                if (instRights.Find(x => x.QuantityName.Equals(eInstantaneousData.NewInst_Current.ToString()) && x.Read) != null)
                {
                    OBIS_Code_Strt = GetOBISCode(Get_Index.Current_Ph_A);
                    // Reading Current
                    temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                    item.Name = "Current";
                    item._Unit = Unit.A;
                    item.Value.PhaseA = makeValue(temp, item.Multiplier);

                    OBIS_Code_Strt = GetOBISCode(Get_Index.Current_Ph_B);
                    temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                    item.Value.PhaseB = makeValue(temp, item.Multiplier);

                    OBIS_Code_Strt = GetOBISCode(Get_Index.Current_Ph_C);
                    temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                    item.Value.PhaseC = makeValue(temp, item.Multiplier);

                    item.Value.PhaseAvg_Total = (item.Value.PhaseA + item.Value.PhaseB + item.Value.PhaseC);
                    List_items.Add(item);
                }
                #endregion

                #region Voltage
                if (instRights.Find(x => x.QuantityName.Equals(eInstantaneousData.NewInst_Voltage.ToString()) && x.Read) != null)
                {

                    OBIS_Code_Strt = GetOBISCode(Get_Index.Voltage_Ph_A);
                    // Reading Voltage
                    item = new InstantaneousItem();
                    temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                    item.Name = "Voltage";
                    item._Unit = Unit.V;
                    item.Value.PhaseA = makeValue(temp, item.Multiplier);

                    OBIS_Code_Strt = GetOBISCode(Get_Index.Voltage_Ph_B);
                    temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                    item.Value.PhaseB = makeValue(temp, item.Multiplier);

                    OBIS_Code_Strt = GetOBISCode(Get_Index.Voltage_Ph_C);
                    temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                    item.Value.PhaseC = makeValue(temp, item.Multiplier);

                    item.Value.PhaseAvg_Total = (item.Value.PhaseA + item.Value.PhaseB + item.Value.PhaseC) / 3;
                    List_items.Add(item);
                }
                #endregion

                #region Active Power Positive
                if (instRights.Find(x => x.QuantityName.Equals(eInstantaneousData.NewInst_PowerkW_Import.ToString()) && x.Read) != null)
                {
                    OBIS_Code_Strt = GetOBISCode(Get_Index.Active_Power_Total_Pos);
                    // Reading ActivePower_Postive
                    item = new InstantaneousItem();
                    temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                    item.Name = "Active Power Import";
                    item._Unit = Unit.kW;
                    item.Value.PhaseAvg_Total = (makeValue(temp, item.Multiplier)) / 1000;

                    OBIS_Code_Strt = GetOBISCode(Get_Index.Active_Power_Ph_A_Pos);
                    temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                    item.Value.PhaseA = (makeValue(temp, item.Multiplier)) / 1000;

                    OBIS_Code_Strt = GetOBISCode(Get_Index.Active_Power_Ph_B_Pos);
                    temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                    item.Value.PhaseB = (makeValue(temp, item.Multiplier)) / 1000;

                    OBIS_Code_Strt = GetOBISCode(Get_Index.Active_Power_Ph_C_Pos);
                    temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                    item.Value.PhaseC = (makeValue(temp, item.Multiplier)) / 1000;
                    List_items.Add(item);
                }
                #endregion

                #region Active Power Negative

                if (instRights.Find(x => x.QuantityName.Equals(eInstantaneousData.NewInst_PowerkW_Export.ToString()) && x.Read) != null)
                {
                    OBIS_Code_Strt = GetOBISCode(Get_Index.Active_Power_Total_Neg);
                    // Reading ActivePower_Negative
                    item = new InstantaneousItem();
                    temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                    item.Name = "Active Power Export";
                    item._Unit = Unit.kW;
                    item.Value.PhaseAvg_Total = (makeValue(temp, item.Multiplier)) / 1000;

                    OBIS_Code_Strt = GetOBISCode(Get_Index.Active_Power_Ph_A_Neg);
                    temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                    item.Value.PhaseA = (makeValue(temp, item.Multiplier)) / 1000;

                    OBIS_Code_Strt = GetOBISCode(Get_Index.Active_Power_Ph_B_Neg);
                    temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                    item.Value.PhaseB = (makeValue(temp, item.Multiplier)) / 1000;

                    OBIS_Code_Strt = GetOBISCode(Get_Index.Active_Power_Ph_C_Neg);
                    temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                    item.Value.PhaseC = (makeValue(temp, item.Multiplier)) / 1000;
                    List_items.Add(item);
                }
                #endregion

                #region Reactive Power Positive

                if ((instRights.Find(x => x.QuantityName.Equals(eInstantaneousData.NewInst_Power_kvar.ToString()) && x.Read) != null) ||
                    instRights.Find(x => x.QuantityName.Equals(eInstantaneousData.NewInst_Powerkvar_Import.ToString()) && x.Read) != null)
                {
                    OBIS_Code_Strt = GetOBISCode(Get_Index.Reactive_Power_Total_Pos);
                    // Reading ReactivePower_Postive
                    item = new InstantaneousItem();
                    temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                    item.Name = kvar_import;
                    item._Unit = Unit.kVar;
                    item.Value.PhaseAvg_Total = (makeValue(temp, item.Multiplier)) / 1000;

                    OBIS_Code_Strt = GetOBISCode(Get_Index.Reactive_Power_Ph_A_Pos);
                    temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                    item.Value.PhaseA = (makeValue(temp, item.Multiplier)) / 1000;

                    OBIS_Code_Strt = GetOBISCode(Get_Index.Reactive_Power_Ph_B_Pos);
                    temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                    item.Value.PhaseB = (makeValue(temp, item.Multiplier)) / 1000;

                    OBIS_Code_Strt = GetOBISCode(Get_Index.Reactive_Power_Ph_C_Pos);
                    temp = Array.Find(value, x => x.OBIS_Index == Get_Index.Reactive_Power_Ph_C_Pos);
                    item.Value.PhaseC = (makeValue(temp, item.Multiplier)) / 1000;
                    List_items.Add(item);
                }
                #endregion

                #region Reactive Power Negative

                if ((instRights.Find(x => x.QuantityName.Equals(eInstantaneousData.NewInst_Powerkvar_Export.ToString()) && x.Read) != null) ||
                    (instRights.Find(x => x.QuantityName.Equals(eInstantaneousData.NewInst_Power_kvar.ToString()) && x.Read) != null))
                {
                    item = new InstantaneousItem();
                    item.Name = kvar_export;
                    item._Unit = Unit.kVar;

                    OBIS_Code_Strt = GetOBISCode(Get_Index.Reactive_Power_Total_Neg);
                    temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                    item.Value.PhaseAvg_Total = (makeValue(temp, item.Multiplier)) / 1000;

                    OBIS_Code_Strt = GetOBISCode(Get_Index.Reactive_Power_Ph_A_Neg);
                    temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                    item.Value.PhaseA = (makeValue(temp, item.Multiplier)) / 1000;

                    OBIS_Code_Strt = GetOBISCode(Get_Index.Reactive_Power_Ph_B_Neg);
                    temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                    item.Value.PhaseB = (makeValue(temp, item.Multiplier)) / 1000;

                    OBIS_Code_Strt = GetOBISCode(Get_Index.Reactive_Power_Ph_C_Neg);
                    temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                    item.Value.PhaseC = (makeValue(temp, item.Multiplier)) / 1000;
                    List_items.Add(item);
                }
                #endregion

                #region Active Power Total

                if (instRights.Find(x => x.QuantityName.Equals(eInstantaneousData.NewInst_Power_kW.ToString()) && x.Read) != null)
                {
                    item = new InstantaneousItem();
                    item.Name = "Active Power";
                    item._Unit = Unit.kW;

                    item.Value.PhaseAvg_Total = (GETDouble_Any(Get_Index.Absolute_Active_Power_Total, 2)) / 1000;
                    item.Value.PhaseA = (GETDouble_Any(Get_Index.Absolute_Active_Power_Phase_1, 2)) / 1000;
                    item.Value.PhaseB = (GETDouble_Any(Get_Index.Absolute_Active_Power_Phase_2, 2)) / 1000;
                    item.Value.PhaseC = (GETDouble_Any(Get_Index.Absolute_Active_Power_Phase_3, 2)) / 1000;

                    List_items.Add(item);
                    #endregion

                    #region Reactive Power Total
                    // TODO: Resolve Bug : NewInst_Power_kvar should not be nested in NewInst_Power_kW //Azeem
                    if (instRights.Find(x => x.QuantityName.Equals(eInstantaneousData.NewInst_Power_kvar.ToString()) && x.Read) != null)
                    {
                        item = new InstantaneousItem();
                        item.Name = "Reactive Power";
                        item._Unit = Unit.kVar;
                        Phase import = List_items.Find(x => x.Name.Equals(kvar_import)).Value;
                        Phase export = List_items.Find(x => x.Name.Equals(kvar_export)).Value;

                        item.Value.PhaseAvg_Total = import.PhaseAvg_Total + export.PhaseAvg_Total; ;
                        item.Value.PhaseA = import.PhaseA + export.PhaseA;
                        item.Value.PhaseB = import.PhaseB + export.PhaseB;
                        item.Value.PhaseC = import.PhaseC + export.PhaseC;

                        List_items.Add(item);

                        if (instRights.Find(x => x.QuantityName.Equals(eInstantaneousData.NewInst_Powerkvar_Import.ToString()) && x.Read) == null)
                            List_items.Remove(List_items.Find(x => x.Name.Equals(kvar_import)));
                        if (instRights.Find(x => x.QuantityName.Equals(eInstantaneousData.NewInst_Powerkvar_Export.ToString()) && x.Read) == null)
                            List_items.Remove(List_items.Find(x => x.Name.Equals(kvar_export)));
                    }
                }
                #endregion

                #region Supply Frequency

                if (instRights.Find(x => x.QuantityName.Equals(eInstantaneousData.NewInst_Frequency.ToString()) && x.Read) != null)
                {
                    OBIS_Code_Strt = GetOBISCode(Get_Index.Supply_Frequency);
                    item = new InstantaneousItem();
                    temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                    item.Name = "Power Supply Frequency";
                    item._Unit = Unit.Hz;
                    list_to_return.Frequency = makeValue(temp, item.Multiplier);
                }
                #endregion

                list_to_return.InstantaneousItems = List_items;
                return list_to_return;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public InstantaneousData FormatInstantaneousData(List<ILValue[]> RawInstantaneousData)
        {
            try
            {
                InstantaneousData list_to_return = new InstantaneousData();
                List<InstantaneousItem> List_items = new List<InstantaneousItem>();
                InstantaneousItem item = new InstantaneousItem();
                ILValue temp = new ILValue();
                ILValue[] value = new ILValue[1];

                value = RawInstantaneousData[0]; // Instantaneous data is received in one Packet

                StOBISCode OBIS_Code_Strt = Get_Index.Dummy;

                #region MeterDateTime

                OBIS_Code_Strt = GetOBISCode(Get_Index.Meter_Clock);
                // Reading Date
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                list_to_return.TimeStamp = (StDateTime)temp.Value;

                #endregion

                #region Current

                OBIS_Code_Strt = GetOBISCode(Get_Index.Current_Ph_A);
                // Reading Current
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                item.Name = "Current";
                item._Unit = Unit.A;
                item.Value.PhaseA = makeValue(temp, item.Multiplier);

                OBIS_Code_Strt = GetOBISCode(Get_Index.Current_Ph_B);
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                item.Value.PhaseB = makeValue(temp, item.Multiplier);

                OBIS_Code_Strt = GetOBISCode(Get_Index.Current_Ph_C);
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                item.Value.PhaseC = makeValue(temp, item.Multiplier);

                item.Value.PhaseAvg_Total = (item.Value.PhaseA + item.Value.PhaseB + item.Value.PhaseC);
                List_items.Add(item);

                #endregion

                #region Voltage

                OBIS_Code_Strt = GetOBISCode(Get_Index.Voltage_Ph_A);
                // Reading Voltage
                item = new InstantaneousItem();
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                item.Name = "Voltage";
                item._Unit = Unit.V;
                item.Value.PhaseA = makeValue(temp, item.Multiplier);

                OBIS_Code_Strt = GetOBISCode(Get_Index.Voltage_Ph_B);
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                item.Value.PhaseB = makeValue(temp, item.Multiplier);

                OBIS_Code_Strt = GetOBISCode(Get_Index.Voltage_Ph_C);
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                item.Value.PhaseC = makeValue(temp, item.Multiplier);

                item.Value.PhaseAvg_Total = (item.Value.PhaseA + item.Value.PhaseB + item.Value.PhaseC) / 3;
                List_items.Add(item);

                #endregion

                #region Active Power Positive

                OBIS_Code_Strt = GetOBISCode(Get_Index.Active_Power_Total_Pos);
                // Reading ActivePower_Postive
                item = new InstantaneousItem();
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                item.Name = "Active Power Positive";
                item._Unit = Unit.kW;
                item.Value.PhaseAvg_Total = (makeValue(temp, item.Multiplier)) / 1000;

                OBIS_Code_Strt = GetOBISCode(Get_Index.Active_Power_Ph_A_Pos);
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                item.Value.PhaseA = (makeValue(temp, item.Multiplier)) / 1000;

                OBIS_Code_Strt = GetOBISCode(Get_Index.Active_Power_Ph_B_Pos);
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                item.Value.PhaseB = (makeValue(temp, item.Multiplier)) / 1000;

                OBIS_Code_Strt = GetOBISCode(Get_Index.Active_Power_Ph_C_Pos);
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                item.Value.PhaseC = (makeValue(temp, item.Multiplier)) / 1000;
                List_items.Add(item);

                #endregion

                #region Active Power Negative

                OBIS_Code_Strt = GetOBISCode(Get_Index.Active_Power_Total_Neg);
                // Reading ActivePower_Negative
                item = new InstantaneousItem();
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                item.Name = "Active Power Negative";
                item._Unit = Unit.kW;
                item.Value.PhaseAvg_Total = (makeValue(temp, item.Multiplier)) / 1000;

                OBIS_Code_Strt = GetOBISCode(Get_Index.Active_Power_Ph_A_Neg);
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                item.Value.PhaseA = (makeValue(temp, item.Multiplier)) / 1000;

                OBIS_Code_Strt = GetOBISCode(Get_Index.Active_Power_Ph_B_Neg);
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                item.Value.PhaseB = (makeValue(temp, item.Multiplier)) / 1000;

                OBIS_Code_Strt = GetOBISCode(Get_Index.Active_Power_Ph_C_Neg);
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                item.Value.PhaseC = (makeValue(temp, item.Multiplier)) / 1000;
                List_items.Add(item);

                #endregion

                #region Reactive Power Positive

                OBIS_Code_Strt = GetOBISCode(Get_Index.Reactive_Power_Total_Pos);
                // Reading ReactivePower_Postive
                item = new InstantaneousItem();
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                item.Name = "Reactive Power Positive";
                item._Unit = Unit.kVar;
                item.Value.PhaseAvg_Total = (makeValue(temp, item.Multiplier)) / 1000;

                OBIS_Code_Strt = GetOBISCode(Get_Index.Reactive_Power_Ph_A_Pos);
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                item.Value.PhaseA = (makeValue(temp, item.Multiplier)) / 1000;

                OBIS_Code_Strt = GetOBISCode(Get_Index.Reactive_Power_Ph_B_Pos);
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                item.Value.PhaseB = (makeValue(temp, item.Multiplier)) / 1000;

                OBIS_Code_Strt = GetOBISCode(Get_Index.Reactive_Power_Ph_C_Pos);
                temp = Array.Find(value, x => x.OBIS_Index == Get_Index.Reactive_Power_Ph_C_Pos);
                item.Value.PhaseC = (makeValue(temp, item.Multiplier)) / 1000;
                List_items.Add(item);

                #endregion

                #region Reactive Power Negative

                OBIS_Code_Strt = GetOBISCode(Get_Index.Reactive_Power_Total_Neg);
                // Reading ReactivePower_Negative
                item = new InstantaneousItem();
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                item.Name = "Reactive Power Negative";
                item._Unit = Unit.kVar;
                item.Value.PhaseAvg_Total = (makeValue(temp, item.Multiplier)) / 1000;

                OBIS_Code_Strt = GetOBISCode(Get_Index.Reactive_Power_Ph_A_Neg);
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                item.Value.PhaseA = (makeValue(temp, item.Multiplier)) / 1000;

                OBIS_Code_Strt = GetOBISCode(Get_Index.Reactive_Power_Ph_B_Neg);
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                item.Value.PhaseB = (makeValue(temp, item.Multiplier)) / 1000;

                OBIS_Code_Strt = GetOBISCode(Get_Index.Reactive_Power_Ph_C_Neg);
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                item.Value.PhaseC = (makeValue(temp, item.Multiplier)) / 1000;
                List_items.Add(item);

                #endregion

                #region Supply Frequency

                OBIS_Code_Strt = GetOBISCode(Get_Index.Supply_Frequency);
                item = new InstantaneousItem();
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                item.Name = "Power Supply Frequency";
                item._Unit = Unit.Hz;
                list_to_return.Frequency = makeValue(temp, item.Multiplier);

                #endregion

                list_to_return.InstantaneousItems = List_items;
                return list_to_return;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Base_Class GET_Object(Base_Class ObjIdentifier)
        {
            Base_Class objReceived = null;
            try
            {
                objReceived = AP_Controller.GET(ObjIdentifier);
                return objReceived;
            }
            catch (DLMSDecodingException ex)    ///Error Type 1 (Decoding Type Errors)
            {
                objReceived = AP_Controller.PreviousRequestedObject;
                //return objReceived;
                throw ex;
            }
            catch (DLMSException ex)            ///Error Type 2 (DLMS Layer Errors)
            {
                objReceived = AP_Controller.PreviousRequestedObject;
                //return objReceived;
                throw ex;
            }
            catch (IOException ex)              ///Communication Data IO Errors
            {
                throw ex;
            }
            catch (Exception ex)               ///Other Reason Errors
            {
                throw ex;
            }
            finally
            { }
        }
        public InstantaneousData ReadInstantaneousData_SinglePhase(CancellationTokenSource TK = null)
        {
            // Initialize Instantaneous Object
            Instantaneous_Class Instantaneous_Class_obj = new Instantaneous_Class();
            Class_7 temp = new Class_7(Get_Index.CurrentPowerQualityDataProfile);
            instantaneousInfo = GetInstantanouseInfo(temp);
            Class_7 Instantaneous_CommObj = (Class_7)GetSAPEntry(Get_Index.CurrentPowerQualityDataProfile);
            List<ILValue[]> Ins_Periods = ReadInstantaneousDataRaw(Instantaneous_CommObj);
            InstantaneousData InstantaneousData = FormatInstantaneousData_SinglePhase(Ins_Periods);
            InstantaneousData.Frequency = GETDouble_Any(Instantaneous_Class_obj, Get_Index.Supply_Frequency, 2) / 100;

            return InstantaneousData;
        }

        public InstantaneousData FormatInstantaneousData_SinglePhase(List<ILValue[]> RawInstantaneousData)
        {
            try
            {
                InstantaneousData list_to_return = new InstantaneousData();
                List<InstantaneousItem> List_items = new List<InstantaneousItem>();
                InstantaneousItem item = new InstantaneousItem();
                ILValue temp = new ILValue();
                ILValue[] value = new ILValue[1];

                StOBISCode OBIS_Code_Strt = Get_Index.Dummy;

                value = RawInstantaneousData[0]; // Instantaneous data is received in one Packet

                #region MeterDateTime

                OBIS_Code_Strt = GetOBISCode(Get_Index.Meter_Clock);

                // Reading Date
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                list_to_return.TimeStamp = (StDateTime)(temp.Value);

                #endregion

                #region Current

                OBIS_Code_Strt = GetOBISCode(Get_Index.Current_Ph_A);

                // Reading Current
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                item.Name = "Instantaneous Current";
                item._Unit = Unit.A;
                item.Value.PhaseA = makeValue(temp, item.Multiplier);
                List_items.Add(item);

                #endregion

                #region Voltage

                OBIS_Code_Strt = GetOBISCode(Get_Index.Voltage_Ph_A);

                // Reading Voltage
                item = new InstantaneousItem();
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                item.Name = "Instantaneous Voltage";
                item._Unit = Unit.V;
                item.Value.PhaseA = makeValue(temp, item.Multiplier);
                List_items.Add(item);

                #endregion

                #region Power Factor

                OBIS_Code_Strt = GetOBISCode(Get_Index.Power_Factor_All);

                //Reading ActivePower_Postive
                item = new InstantaneousItem();
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);
                item.Name = "Instantaneous Power Factor";
                item._Unit = Unit.UnitLess;
                item.Value.PhaseA = (makeValue(temp, item.Multiplier));

                List_items.Add(item);

                #endregion

                #region Instantaneous Active Power

                OBIS_Code_Strt = GetOBISCode(Get_Index.Active_Power_Ph_A_Pos);

                // Reading Instantaneous ActivePower_Postive
                item = new InstantaneousItem();
                temp = Array.Find(value, x => x.OBIS_Index == OBIS_Code_Strt);

                // if (temp == null)
                // {
                //     throw new Exception("Instantaneous Active power not found");
                // }

                item.Name = "Instantaneous Active Power";
                item._Unit = Unit.kW;
                item.Value.PhaseA = (makeValue(temp, item.Multiplier)) / 1000;

                List_items.Add(item);

                #endregion

                list_to_return.InstantaneousItems = List_items;
                return list_to_return;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public Instantaneous_Class ReadInstantaneousDataDDS()
        {
            try
            {
                const int kWDivider = 1000;
                Instantaneous_Class InstantValues = new Instantaneous_Class();

                InstantValues.dateTime = new StDateTime();
                InstantValues.dateTime.SetDateTime(GET_MeterClock());

                TryGET_Any(InstantValues, Get_Index.Current_Ph_A, 2);
                InstantValues.current_PhaseA = TryGET_Any(InstantValues, Get_Index.Current_Ph_A, 3);
                TryGET_Any(InstantValues, Get_Index.Current_Ph_B, 2);
                InstantValues.current_PhaseB = TryGET_Any(InstantValues, Get_Index.Current_Ph_B, 3);
                TryGET_Any(InstantValues, Get_Index.Current_Ph_C, 2);
                InstantValues.current_PhaseC = TryGET_Any(InstantValues, Get_Index.Current_Ph_C, 3);

                TryGET_Any(InstantValues, Get_Index.Voltage_Ph_A, 2);
                InstantValues.voltage_PhaseA = TryGET_Any(InstantValues, Get_Index.Voltage_Ph_A, 3);
                TryGET_Any(InstantValues, Get_Index.Voltage_Ph_B, 2);
                InstantValues.voltage_PhaseB = TryGET_Any(InstantValues, Get_Index.Voltage_Ph_B, 3);
                TryGET_Any(InstantValues, Get_Index.Voltage_Ph_C, 2);
                InstantValues.voltage_PhaseC = TryGET_Any(InstantValues, Get_Index.Voltage_Ph_C, 3);

                TryGET_Any(InstantValues, Get_Index.Active_Power_Ph_A_Pos, 2);
                InstantValues.active_powerPositive_PhaseA = TryGET_Any(InstantValues, Get_Index.Active_Power_Ph_A_Pos, 3) / kWDivider;
                TryGET_Any(InstantValues, Get_Index.Active_Power_Ph_B_Pos, 2);
                InstantValues.active_powerPositive_PhaseB = TryGET_Any(InstantValues, Get_Index.Active_Power_Ph_B_Pos, 3) / kWDivider;
                TryGET_Any(InstantValues, Get_Index.Active_Power_Ph_C_Pos, 2);
                InstantValues.active_powerPositive_PhaseC = TryGET_Any(InstantValues, Get_Index.Active_Power_Ph_C_Pos, 3) / kWDivider;
                TryGET_Any(InstantValues, Get_Index.Active_Power_Total_Pos, 2);
                InstantValues.active_powerPositive_PhaseC = TryGET_Any(InstantValues, Get_Index.Active_Power_Total_Pos, 3) / kWDivider;
                TryGET_Any(InstantValues, Get_Index.Active_Power_Ph_A_Neg, 2);
                InstantValues.active_powerNegative_PhaseA = TryGET_Any(InstantValues, Get_Index.Active_Power_Ph_A_Neg, 3) / kWDivider;
                TryGET_Any(InstantValues, Get_Index.Active_Power_Ph_B_Neg, 2);
                InstantValues.active_powerNegative_PhaseB = TryGET_Any(InstantValues, Get_Index.Active_Power_Ph_B_Neg, 3) / kWDivider;
                TryGET_Any(InstantValues, Get_Index.Active_Power_Ph_C_Neg, 2);
                InstantValues.active_powerNegative_PhaseC = TryGET_Any(InstantValues, Get_Index.Active_Power_Ph_C_Neg, 3) / kWDivider;
                TryGET_Any(InstantValues, Get_Index.Active_Power_Total_Neg, 2);
                InstantValues.active_powerNegative_PhaseC = TryGET_Any(InstantValues, Get_Index.Active_Power_Total_Neg, 3) / kWDivider;

                TryGET_Any(InstantValues, Get_Index.Reactive_Power_Ph_A_Pos, 2);
                InstantValues.reactive_powerPositive_PhaseA = TryGET_Any(InstantValues, Get_Index.Reactive_Power_Ph_A_Pos, 3) / kWDivider;
                TryGET_Any(InstantValues, Get_Index.Reactive_Power_Ph_B_Pos, 2);
                InstantValues.reactive_powerPositive_PhaseB = TryGET_Any(InstantValues, Get_Index.Reactive_Power_Ph_B_Pos, 3) / kWDivider;
                TryGET_Any(InstantValues, Get_Index.Reactive_Power_Ph_C_Pos, 2);
                InstantValues.reactive_powerPositive_PhaseC = TryGET_Any(InstantValues, Get_Index.Reactive_Power_Ph_C_Pos, 3) / kWDivider;
                TryGET_Any(InstantValues, Get_Index.Reactive_Power_Total_Pos, 2);
                InstantValues.reactive_powerPositive_PhaseC = TryGET_Any(InstantValues, Get_Index.Reactive_Power_Total_Pos, 3) / kWDivider;
                TryGET_Any(InstantValues, Get_Index.Reactive_Power_Ph_A_Neg, 2);
                InstantValues.reactive_powerNegative_PhaseA = TryGET_Any(InstantValues, Get_Index.Reactive_Power_Ph_A_Neg, 3) / kWDivider;
                TryGET_Any(InstantValues, Get_Index.Reactive_Power_Ph_B_Neg, 2);
                InstantValues.reactive_powerNegative_PhaseB = TryGET_Any(InstantValues, Get_Index.Reactive_Power_Ph_B_Neg, 3) / kWDivider;
                TryGET_Any(InstantValues, Get_Index.Reactive_Power_Ph_C_Neg, 2);
                InstantValues.reactive_powerNegative_PhaseC = TryGET_Any(InstantValues, Get_Index.Reactive_Power_Ph_C_Neg, 3) / kWDivider;
                TryGET_Any(InstantValues, Get_Index.Reactive_Power_Total_Neg, 2);
                InstantValues.reactive_powerNegative_PhaseTL = TryGET_Any(InstantValues, Get_Index.Reactive_Power_Total_Neg, 3) / kWDivider;
                TryGET_Any(InstantValues, Get_Index.Supply_Frequency, 2);
                InstantValues.frequency = TryGET_Any(InstantValues, Get_Index.Supply_Frequency, 3);

                return InstantValues;
            }
            catch (Exception ex)
            { throw new Exception(string.Format("Error occurred reading Cumulative Billing data (Error Code:{0})", (MDCErrors.App_Cum_Billing_Read)), ex); }
        }

        public Instantaneous_Class ReadInstantaneousDataDDS_SinglePhase()
        {
            Param_Clock_Caliberation paramCC = new Param_Clock_Caliberation();
            Instantaneous_Class InstantValues = new Instantaneous_Class();
            InstantValues.dateTime = new StDateTime();
            InstantValues.dateTime.SetDateTime(GET_MeterClock());

            TryGET_Any(InstantValues, Get_Index.Current_Ph_A, 2);
            InstantValues.current_PhaseA = InstantValues.current_PhaseTL = TryGET_Any(InstantValues, Get_Index.Current_Ph_A, 3);
            InstantValues.current_PhaseB = InstantValues.current_PhaseC = Double.NaN;

            TryGET_Any(InstantValues, Get_Index.Voltage_Ph_A, 2);
            InstantValues.voltage_PhaseA = InstantValues.voltage_PhaseTL = TryGET_Any(InstantValues, Get_Index.Voltage_Ph_A, 3);
            InstantValues.voltage_PhaseB = InstantValues.voltage_PhaseC = Double.NaN;

            TryGET_Any(InstantValues, Get_Index.Power_Factor_All, 2);
            InstantValues.Power_Factor_Ph_A = InstantValues.Power_Factor_All = TryGET_Any(InstantValues, Get_Index.Power_Factor_All, 3);
            InstantValues.Power_Factor_Ph_B = InstantValues.Power_Factor_Ph_C = Double.NaN;

            TryGET_Any(InstantValues, Get_Index.Active_Power_Total_Pos, 2);
            InstantValues.active_powerPositive_PhaseA = InstantValues.active_powerPositive_PhaseTL = TryGET_Any(InstantValues, Get_Index.Active_Power_Total_Pos, 3);
            InstantValues.active_powerPositive_PhaseB = InstantValues.active_powerPositive_PhaseC = Double.NaN;


            TryGET_Any(InstantValues, Get_Index.Supply_Frequency, 2);
            InstantValues.frequency = TryGET_Any(InstantValues, Get_Index.Supply_Frequency, 3);

            return InstantValues;
        }

        public Instantaneous_Class ReadInstantaneousDataByOBISList()
        {
            Instantaneous_Class inst_Class = new Instantaneous_Class();
            inst_Class.dateTime = new StDateTime();
            Dictionary<string, double> ReadValues = new Dictionary<string, double>();
            List<CaptureObject> captureObjectsList = Configurator.GetProfileCaptureObjectList(ConnectionInfo, Get_Index.CurrentPowerQualityDataProfile);

            inst_Class.dateTime.SetDateTime(GET_MeterClock());

            foreach (var captureObj in captureObjectsList)
            {
                try
                {
                    if (captureObj.StOBISCode.OBISIndex.Equals(Get_Index.Meter_Clock)) continue;

                    if (string.IsNullOrEmpty(captureObj.DatabaseFieldName)) continue;

                    //GETDouble_Any(captureObj.StOBISCode, 2);

                    double val = GETDouble_Any(captureObj.StOBISCode, 0);
                    val = Commons.ApplyMultiplier(val, captureObj.Multiplier);

                    if (ReadValues.ContainsKey(captureObj.DatabaseFieldName))
                        ReadValues[captureObj.DatabaseFieldName] += val;
                    else
                        ReadValues.Add(captureObj.DatabaseFieldName, val);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error While Reading Ins data By OBIS Code", ex);
                }
            }

            if (ReadValues.Keys.Count > 0)
            {
                foreach (var key in ReadValues.Keys)
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        double val = ReadValues[key];
                        inst_Class.DBColumns.Append($"{key}, ");
                        inst_Class.DBValues.Append($"{val}, ");
                        inst_Class.DB_Columns_Values.Append($"{key} = {val}, ");
                    }
                }
            }

            return inst_Class;
        }

        public interface IDataGenerator
        {
            List<ILValue[]> GetData(List<DLMS.Base_Class> dataList);
        }

        /// <summary>
        /// Commented_Modified
        /// </summary>
        /// <param name="Instantaneous_CommObj"></param>
        /// <returns></returns>
        public GenericProfileInfo GetInstantanouseInfo(Class_7 Instantaneous_CommObj)
        {
            try
            {
                GenericProfileInfo InstantaneousInfo = new GenericProfileInfo();
                Class_7 instantaneous_CommObj = (Class_7)GetSAPEntry(Get_Index.CurrentPowerQualityDataProfile);

                #region // Initialize Power Quality Capture Buffer Info

                try
                {
                    StOBISCode OBIS_Index_PowerProfile = Get_Index.CurrentPowerQualityDataProfile;
                    instantaneous_CommObj.captureObjectsList = Configurator.GetProfileCaptureObjectList(ConnectionInfo, OBIS_Index_PowerProfile);
                    instantaneous_CommObj.BaseClassCaptureObjectsList = instantaneous_CommObj.InitializeCaptureBuffer(1);
                }
                catch (Exception)
                {
                    instantaneous_CommObj.captureObjectsList = new List<CaptureObject>();
                }

                #endregion

                if (instantaneous_CommObj.IsAttribReadable(0x03) && !instantaneous_CommObj.IsCaptureObjectListIntialized)
                {
                    ///Request Capture Object List
                    instantaneous_CommObj = (Class_7)GET_Object(Get_Index.CurrentPowerQualityDataProfile, (byte)3);
                    if (instantaneous_CommObj.GetAttributeDecodingResult(0x03) == DecodingResult.Ready
                        && instantaneous_CommObj.captureObjectsList != null)
                    {
                        if (!instantaneous_CommObj.IsCaptureObjectListIntialized)
                        {
                            instantaneous_CommObj.BaseClassCaptureObjectsList = instantaneous_CommObj.InitializeCaptureBuffer(1);
                        }
                        #region // Save Power Quality Capture Buffer Info
                        #region Debugging & Logging
#if Enable_DEBUG_RunMode

                        try
                        {
                            StOBISCode OBIS_Index_PowerProfile = Get_Index.CurrentPowerQualityDataProfile;
                            Configurator.SaveProfileCaptureObjectList(CurrentConnectionInfo, instantaneous_CommObj.captureObjectsList, OBIS_Index_PowerProfile, null);

                        }
                        catch (Exception ex)
                        {
                            //Commons.WriteError(String.Format("Error occurred while saving Instantaneous Capture List Info,Error Details {0}", ex.Message));
                        }

#endif
                        #endregion
                        #endregion
                    }
                }
                else if (instantaneous_CommObj.IsCaptureObjectListIntialized)
                {
                    ;
                    // instantaneous_CommObj.BaseClassCaptureObjectsList =  instantaneous_CommObj.InitializeCaptureBuffer(1);
                    // InstantaneousInfo.CaptureBufferInfo.AddRange(instantaneous_CommObj.BaseClassCaptureObjectsList);
                }
                else if (!instantaneous_CommObj.IsAttribReadable(0x03))
                {
                    throw new Exception("Error occurred reading Instantaneous Info,unable to read Capture ObjectInfo No Access Rights available");
                }
                else
                    throw new Exception("Error occurred reading Instantaneous Info,unable to read Capture ObjectInfo");
                return InstantaneousInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred reading Instantaneous data_GetInstantanouseInfo", ex);
            }
        }

        // private double makeValue(ILValue val, float multiplier)
        // {
        //     try
        //     {
        //         if (val != null)
        //         {
        //             double rawVal = Convert.ToDouble(val.Value);
        //             if (multiplier < 0)
        //             {
        //                 rawVal = rawVal / (Math.Pow(10, Math.Abs(multiplier)));
        //             }
        //             else if (multiplier > 0)
        //             {
        //                 rawVal = rawVal * (Math.Pow(10, Math.Abs(multiplier)));
        //             }
        //             return rawVal;
        //         }
        //         else
        //             return double.NaN;
        //
        //     }
        //     catch (Exception ex)
        //     {
        //         return double.PositiveInfinity;
        //     }
        // }

        public Base_Class GetSAPEntry(StOBISCode ObjIdentifier)
        {
            return this.AP_Controller.GetSAPEntry(ObjIdentifier);
        }
        public Base_Class GetSAPEntry(Get_Index ObjIdentifier)
        {
            return this.AP_Controller.GetSAPEntry(ObjIdentifier);
        }

        public Base_Class GET_Object(StOBISCode ObjIdentifier, byte attribArg)
        {
            Base_Class objReceived = null;
            try
            {
                objReceived = GetSAPEntry(ObjIdentifier);
                objReceived.DecodingAttribute = attribArg;

                // Reset To DecodingResult To DataNotPresent
                if (objReceived != null)
                {
                    objReceived.ResetAttributeDecodingResults(objReceived.DecodingAttribute);
                }
                GET_Object(objReceived);

                return objReceived;
            }
            catch (Exception ex)  // Other Reason Errors
            {
                throw ex;
            }
        }

        //public Base_Class GET_Object(Get_Index ObjIdentifier, byte attribArg)
        //{
        //    Base_Class objReceived = null;
        //    try
        //    {
        //        objReceived = AP_Controller.GET(ObjIdentifier, attribArg);
        //        return objReceived;
        //    }
        //    catch (DLMSDecodingException ex)    ///Error Type 1 (Decoding Type Errors)
        //    {
        //        objReceived = AP_Controller.PreviousRequestedObject;
        //        //return objReceived;
        //        throw ex;
        //    }
        //    catch (DLMSException ex)            ///Error Type 2 (DLMS Layer Errors)
        //    {
        //        objReceived = AP_Controller.PreviousRequestedObject;
        //        //return objReceived;
        //        throw ex;
        //    }
        //    catch (IOException ex)              ///Communication Data IO Errors
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)               ///Other Reason Errors
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //    }
        //}

        public Instantaneous_Class saveToClass(InstantaneousData data, string MSN)
        {
            try
            {
                InstantaneousItem item = new InstantaneousItem();
                Instantaneous_Class obj = new Instantaneous_Class();
                obj.MSN = MSN;
                obj.dateTime = data.TimeStamp;
                //obj.reference_no = reference_no;

                //foreach (InstantaneousItem i_item in data.InstantaneousItems)
                //{
                //    if (!string.IsNullOrEmpty(i_item.DBColumnsString))
                //    {
                //        obj.DBColumns.Append(i_item.DBColumnsString);
                //        obj.DBValues.Append(i_item.DBValuesString);
                //        obj.DB_Columns_Values.Append(i_item.DBUpdateString);
                //    }
                //}

                #region Current

                item = data.InstantaneousItems.Find(x => (x.Name == "Current" || x.Name == "Instantaneous Current"));
                if (item != null)
                {
                    if (!item.Value.PhaseAvg_Total.Equals(Double.NaN))
                    {
                        obj.current_PhaseA = item.Value.PhaseA;
                        obj.current_PhaseB = item.Value.PhaseB;
                        obj.current_PhaseC = item.Value.PhaseC;
                        obj.current_PhaseTL = obj.current_PhaseA + obj.current_PhaseB + obj.current_PhaseC;
                    }
                    else
                        obj.current_PhaseA = obj.current_PhaseTL = item.Value.PhaseA;

                }

                #endregion

                #region Voltage

                item = data.InstantaneousItems.Find(x => (x.Name == "Voltage" || x.Name == "Instantaneous Voltage"));
                if (item != null)
                {
                    if (!item.Value.PhaseAvg_Total.Equals(Double.NaN))
                    {
                        obj.voltage_PhaseA = item.Value.PhaseA;
                        obj.voltage_PhaseB = item.Value.PhaseB;
                        obj.voltage_PhaseC = item.Value.PhaseC;
                        obj.voltage_PhaseTL = (obj.voltage_PhaseA + obj.voltage_PhaseB + obj.voltage_PhaseC) / 3;
                    }
                    else
                        obj.voltage_PhaseA = obj.voltage_PhaseTL = item.Value.PhaseA;
                }


                #endregion

                #region Active Power Positive

                item = data.InstantaneousItems.Find(x => (x.Name == "Active Power Positive" || x.Name == "Instantaneous Active Power"));
                if (item != null)
                {
                    if (!item.Value.PhaseAvg_Total.Equals(Double.NaN))
                    {
                        obj.active_powerPositive_PhaseA = item.Value.PhaseA;
                        //  item = data.InstantaneousItems.Find(x => x.Name == (Get_Index.Active_Power_Ph_B_Pos).ToString());
                        obj.active_powerPositive_PhaseB = item.Value.PhaseB;
                        //  item = data.InstantaneousItems.Find(x => x.Name == (Get_Index.Active_Power_Ph_C_Pos).ToString());
                        obj.active_powerPositive_PhaseC = item.Value.PhaseC;
                        // obj.active_powerPositive_PhaseTL = (obj.active_powerPositive_PhaseA + obj.active_powerPositive_PhaseB + obj.active_powerPositive_PhaseC) ;
                        obj.active_powerPositive_PhaseTL = item.Value.PhaseAvg_Total;
                    }
                    else
                        obj.active_powerPositive_PhaseA = obj.active_powerPositive_PhaseTL = item.Value.PhaseA;
                }
                #endregion

                #region Active Power Negative

                item = data.InstantaneousItems.Find(x => x.Name == "Active Power Negative");
                if (item != null)
                {
                    if (!item.Value.PhaseAvg_Total.Equals(Double.NaN))
                    {
                        obj.active_powerNegative_PhaseA = item.Value.PhaseA;
                        //     item = data.InstantaneousItems.Find(x => x.Name == (Get_Index.Active_Power_Ph_B_Neg).ToString());
                        obj.active_powerNegative_PhaseB = item.Value.PhaseB;
                        //    item = data.InstantaneousItems.Find(x => x.Name == (Get_Index.Active_Power_Ph_C_Neg).ToString());
                        obj.active_powerNegative_PhaseC = item.Value.PhaseC;
                        // obj.active_powerNegative_PhaseTL = (obj.active_powerNegative_PhaseA + obj.active_powerNegative_PhaseB + obj.active_powerNegative_PhaseC) ;
                        obj.active_powerNegative_PhaseTL = item.Value.PhaseAvg_Total;
                    }
                    else
                        obj.active_powerNegative_PhaseA = obj.active_powerNegative_PhaseTL = item.Value.PhaseA;
                }

                #endregion

                #region Reactive Power Positive

                item = data.InstantaneousItems.Find(x => x.Name == "Reactive Power Positive");
                if (item != null)
                {
                    if (!item.Value.PhaseAvg_Total.Equals(Double.NaN))
                    {
                        obj.reactive_powerPositive_PhaseA = item.Value.PhaseA;
                        //   item = data.InstantaneousItems.Find(x => x.Name == (Get_Index.Reactive_Power_Ph_B_Pos).ToString());
                        obj.reactive_powerPositive_PhaseB = item.Value.PhaseB;
                        //   item = data.InstantaneousItems.Find(x => x.Name == (Get_Index.Reactive_Power_Ph_C_Pos).ToString());
                        obj.reactive_powerPositive_PhaseC = item.Value.PhaseC;
                        // obj.reactive_powerPositive_PhaseTL = (obj.reactive_powerPositive_PhaseA + obj.reactive_powerPositive_PhaseB + obj.reactive_powerPositive_PhaseC) ;
                        obj.reactive_powerPositive_PhaseTL = item.Value.PhaseAvg_Total;
                    }
                    else
                        obj.reactive_powerPositive_PhaseA = obj.reactive_powerPositive_PhaseTL = item.Value.PhaseA;
                }

                #endregion

                #region Reactive Power Negative

                item = data.InstantaneousItems.Find(x => x.Name == "Reactive Power Negative");
                if (item != null)
                {
                    if (!item.Value.PhaseAvg_Total.Equals(Double.NaN))
                    {
                        obj.reactive_powerNegative_PhaseA = item.Value.PhaseA;
                        obj.reactive_powerNegative_PhaseB = item.Value.PhaseB;
                        obj.reactive_powerNegative_PhaseC = item.Value.PhaseC;
                        obj.reactive_powerNegative_PhaseTL = item.Value.PhaseAvg_Total;
                    }
                    else
                        obj.reactive_powerNegative_PhaseA = obj.reactive_powerNegative_PhaseTL = item.Value.PhaseA;
                }
                #endregion

                obj.frequency = data.Frequency;

                return obj;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //public Instantaneous_Class saveToClass_SinglePhase(InstantaneousData data, string MSN)
        //{
        //    try
        //    {
        //        InstantaneousItem item = new InstantaneousItem();
        //        Instantaneous_Class obj = new Instantaneous_Class();

        //        obj.reactive_powerPositive_PhaseA=
        //        obj.reactive_powerPositive_PhaseB=
        //        obj.reactive_powerPositive_PhaseC=
        //        obj.reactive_powerPositive_PhaseTL=
        //        obj.reactive_powerNegative_PhaseA=
        //        obj.reactive_powerNegative_PhaseB=
        //        obj.reactive_powerNegative_PhaseC =
        //        obj.reactive_powerNegative_PhaseTL=Double.NaN;

        //        obj.MSN = MSN;
        //        //obj.reference_no = reference_no;
        //        obj.dateTime = data.TimeStamp;

        //        #region Current
        //        item = data.InstantaneousItems.Find(x => x.Name == "Instantaneous Current");
        //        obj.current_PhaseA = obj.current_PhaseTL = (item != null ? item.Value.PhaseA:Double.NaN);

        //        obj.current_PhaseB = obj.current_PhaseC = Double.NaN;

        //        #endregion

        //        #region Voltage
        //        item = data.InstantaneousItems.Find(x => x.Name == "Instantaneous Voltage");
        //        obj.voltage_PhaseTL = obj.voltage_PhaseA = (item != null ? item.Value.PhaseA:Double.NaN);


        //        obj.voltage_PhaseB = obj.voltage_PhaseC = Double.NaN;

        //        #endregion

        //        #region Active Power Positive
        //        item = data.InstantaneousItems.Find(x => x.Name == "Instantaneous Power Factor");
        //        obj.Power_Factor_All = obj.Power_Factor_Ph_A = (item != null ? item.Value.PhaseA:Double.NaN);

        //        obj.Power_Factor_Ph_B = obj.Power_Factor_Ph_C = Double.NaN;

        //        #endregion

        //        #region Active Power Negative
        //        item = data.InstantaneousItems.Find(x => x.Name == "Instantaneous Active Power");
        //        obj.active_powerPositive_PhaseA = obj.active_powerPositive_PhaseTL = (item != null ? item.Value.PhaseA : Double.NaN);

        //        obj.active_powerPositive_PhaseB = obj.active_powerPositive_PhaseC = Double.NaN;

        //        #endregion

        //        obj.frequency = data.Frequency;

        //        return obj;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        #region Save_Capture_List

        private List<CaptureObject> GetPowerQualityBufferInfo()
        {
            List<CaptureObject> Info = new List<CaptureObject>();
            //Meter_Clock               Class id 8   Attribut 02  Data Index 00
            CaptureObject Obj_1 = new CaptureObject();
            StOBISCode OBis = Get_Index.Meter_Clock;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x02;
            Obj_1.DataIndex = 0;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);
            //Current_Ph_A              Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Current_Ph_A;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);
            ///Current_Ph_B              Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Current_Ph_B;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            //Current_Ph_C             Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Current_Ph_C;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            //Voltage_Ph_A             Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Voltage_Ph_A;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            //Voltage_Ph_B             Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Voltage_Ph_B;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            //Voltage_Ph_C             Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Voltage_Ph_C;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            //Active_Power_Total_Pos             Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Active_Power_Total_Pos;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            //Active_Power_Ph_A_Pos             Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Active_Power_Ph_A_Pos;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            //Active_Power_Ph_B_Pos             Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Active_Power_Ph_B_Pos;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            //Active_Power_Ph_C_Pos             Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Active_Power_Ph_C_Pos;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            //Active_Power_Total_Neg             Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Active_Power_Total_Neg;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            //Active_Power_Ph_A_Neg             Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Active_Power_Ph_A_Neg;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            //Active_Power_Ph_B_Neg             Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Active_Power_Ph_B_Neg;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            //Active_Power_Ph_C_Neg             Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Active_Power_Ph_C_Neg;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            //Reactive_Power_Total_Pos             Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Reactive_Power_Total_Pos;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            //Reactive_Power_Ph_A_Pos             Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Reactive_Power_Ph_A_Pos;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            //Reactive_Power_Ph_B_Pos             Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Reactive_Power_Ph_B_Pos;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            //Reactive_Power_Ph_C_Pos             Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Reactive_Power_Ph_C_Pos;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            //Reactive_Power_Total_Neg             Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Reactive_Power_Total_Neg;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            //Reactive_Power_Ph_A_Neg             Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Reactive_Power_Ph_A_Neg;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            //Reactive_Power_Ph_B_Neg             Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Reactive_Power_Ph_B_Neg;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            //Reactive_Power_Ph_C_Neg             Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Reactive_Power_Ph_C_Neg;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            //Supply_Frequency             Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Supply_Frequency;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            return Info;
        }

        #endregion

        public StOBISCode GetOBISCode(Get_Index OBIS_Index)
        {
            StOBISCode obisCode = Get_Index.Dummy;

            try
            {
                if (_AP_Controller == null)
                    throw new NotImplementedException("OBISLabelLookup");

                obisCode = _AP_Controller.GetOBISCode(OBIS_Index);
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

        #region GetDateTime

        public DateTime GET_MeterClock()
        {
            try
            {
                // Get RTC T                
                Param_Clock_Caliberation MeterClockParam = new Param_Clock_Caliberation();
                Base_Class MeterClock_CommObj = GET_Object(Get_Index.Meter_Clock, 0x02);
                MeterClockParam.Decode_Date_Time(MeterClock_CommObj);
                return MeterClockParam.Set_Time;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting RTC Date Time", ex);
            }
        }

        #endregion

        #region GET_Any

        // public double GET_Any(ref  Instantaneous_Class Instantaneous_Class_obj, Get_Index arg, byte attribute, byte Class_ID)
        // {
        //     try
        //     {
        //         Base_Class Comm_Obj = GET_Object(arg, attribute);
        //         return Instantaneous_Class_obj.Decode_Any(Comm_Obj, Class_ID);
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
        //     }
        // }

        // public byte[] GET_Any_ByteArray(Get_Index arg, byte attribute, byte Class_ID)
        // {
        //     try
        //     {
        //         Instantaneous_Class Instantaneous_Class_obj = new Instantaneous_Class();
        //         Base_Class Comm_Obj = GET_Object(arg, attribute);
        //         return Instantaneous_Class_obj.Decode_Any_ByteArray(Comm_Obj, Class_ID);
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
        //     }
        // }

        public double GETDouble_Any(StOBISCode arg, byte attribute)
        {
            try
            {
                Base_Class Comm_Obj = GET_Object(arg, attribute);
                return DLMS_Common.Decode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
                // return DataContainer_Class_obj.Decode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }
        public double GETDouble_Any(Get_Index arg, byte attribute)
        {
            try
            {
                Base_Class Comm_Obj = GET_Object(arg, attribute);
                return DLMS_Common.Decode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
                // return DataContainer_Class_obj.Decode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public double GETDouble_Any(IDecodeAnyObject DataContainer_Class_obj, Get_Index arg, byte attribute)
        {
            try
            {
                Base_Class Comm_Obj = GET_Object(arg, attribute);
                return DataContainer_Class_obj.Decode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public byte[] GETArray_Any(IDecodeAnyObject DataContainer_Class_obj, Get_Index arg, byte attribute)
        {
            try
            {
                Base_Class Comm_Obj = GET_Object(arg, attribute);
                return DataContainer_Class_obj.Decode_Any_ByteArray(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public byte[] GETArray_Any(Get_Index arg, byte attribute)
        {
            try
            {
                Base_Class Comm_Obj = GET_Object(arg, attribute);
                return DLMS_Common.Decode_Any_ByteArray(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public string GETString_Any(IDecodeAnyObject DataContainer_Class_obj, Get_Index arg, byte attribute)
        {
            try
            {
                Base_Class Comm_Obj = GET_Object(arg, attribute);
                return DataContainer_Class_obj.Decode_Any_string(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public string GETString_Any(Get_Index arg, byte attribute)
        {
            try
            {
                Base_Class Comm_Obj = GET_Object(arg, attribute);
                return DLMS_Common.Decode_Any_string(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public double GET_Any(IDecodeAnyObject DataContainer_Class_obj, Get_Index arg, byte attribute)
        {
            try
            {
                Base_Class Comm_Obj = GET_Object(arg, attribute);
                return DataContainer_Class_obj.Decode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public void GET_Any(IDecodeAnyObject DataContainer_Class_obj, string Data_Property, Get_Index arg, byte attribute)
        {
            double value = double.NaN;
            try
            {
                if (string.IsNullOrEmpty(Data_Property))
                    throw new ArgumentNullException("Data_Property");
                if (DataContainer_Class_obj == null)
                    throw new ArgumentNullException("DecodeAnyObject");

                Base_Class Comm_Obj = GET_Object(arg, attribute);
                value = DataContainer_Class_obj.Decode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));

                // Data_Property
                DataContainer_Class_obj.setDataMemberByName(Data_Property, value);

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public double TryGET_Any(IDecodeAnyObject DataContainer_Class_obj, Get_Index arg, byte attribute)
        {
            double temp = double.NaN;

            try
            {
                Base_Class Comm_Obj = GET_Object(arg, attribute);
                temp = DataContainer_Class_obj.Decode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));

                ResetMaxIOFailure();
            }
            catch (Exception ex)
            {
                temp = double.NaN;
                //  throw new Exception("Error occurred while Getting " + arg.ToString(), ex);

                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;

                System.Diagnostics.Debug.WriteLineIf(true, string.Format("Error occurred while Getting {0} {1}"
                                                    , arg.ToString(), ex.ToString()));
            }

            return temp;
        }

        public bool TryGET_Any(IDecodeAnyObject DataContainer_Class_obj, string Data_Property, Get_Index arg, byte attribute)
        {
            double value = double.NaN;
            bool isSuccess = true;

            try
            {
                if (string.IsNullOrEmpty(Data_Property))
                    throw new ArgumentNullException("Data_Property");
                if (DataContainer_Class_obj == null)
                    throw new ArgumentNullException("DecodeAnyObject");

                Base_Class Comm_Obj = GET_Object(arg, attribute);
                value = DataContainer_Class_obj.Decode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));

                if (value == Double.NaN || Double.IsInfinity(value))
                    isSuccess = false;

                if (isSuccess)
                    ResetMaxIOFailure();
            }
            catch (Exception ex)
            {
                value = double.NaN;
                isSuccess = false;
                //  throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                System.Diagnostics.Debug.WriteLineIf(true, string.Format("Error occurred while Getting {0} {1}"
                                                    , arg.ToString(), ex.ToString()));
            }

            // Data_Property Write
            DataContainer_Class_obj.setDataMemberByName(Data_Property, value);

            return isSuccess;
        }

        public bool TryGET_Any(IDecodeAnyObject DataContainer_Class_obj, string Data_Property, string TimeStamp_DataProperty, Get_Index arg, byte attribute)
        {
            double value = double.NaN;
            bool isSuccess = true;
            StDateTime DataTimeObj = null;
            DateTime DataTimeStamp = DateTime.MinValue;

            try
            {
                if (string.IsNullOrEmpty(Data_Property))
                    throw new ArgumentNullException("Data_Property");
                if (string.IsNullOrEmpty(Data_Property))
                    throw new ArgumentNullException("TimeStamp_DataProperty");
                if (DataContainer_Class_obj == null)
                    throw new ArgumentNullException("DecodeAnyObject");

                Base_Class Comm_Obj = GET_Object(arg, attribute);
                value = DataContainer_Class_obj.Decode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID), ref DataTimeObj);

                if (DataTimeObj != null && DataTimeObj.IsDateTimeConvertible)
                {
                    DataTimeStamp = DataTimeObj.GetDateTime();
                }
                else if (DataTimeObj != null && DataTimeObj.IsDateConvertible)
                {
                    DataTimeStamp = DataTimeObj.GetDate();
                }
                else if (DataTimeObj != null && DataTimeObj.IsTimeConvertible)
                {
                    DataTimeStamp = DateTime.MinValue.Add(DataTimeObj.GetTime());
                }

                if (value == Double.NaN || Double.IsInfinity(value))
                    isSuccess = false;

                if (isSuccess)
                    ResetMaxIOFailure();
            }
            catch (Exception ex)
            {
                value = double.NaN;
                isSuccess = false;
                //  throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                System.Diagnostics.Debug.WriteLineIf(true, string.Format("Error occurred while Getting {0} {1}"
                                                    , arg.ToString(), ex.ToString()));
            }

            // Data_Property Write
            DataContainer_Class_obj.setDataMemberByName(Data_Property, value);
            DataContainer_Class_obj.setDataMemberByName(TimeStamp_DataProperty, DataTimeStamp);

            return isSuccess;
        }

        public double TryGET_Any(IDecodeAnyObject Instantaneous_Class_obj, Get_Index arg, byte attribute, ref DateTime DtTimeStamp)
        {
            double temp = double.NaN;
            DtTimeStamp = DateTime.MinValue;

            StDateTime dateTimeLocal = null;

            try
            {
                Base_Class Comm_Obj = GET_Object(arg, attribute);
                temp = Instantaneous_Class_obj.Decode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID), ref dateTimeLocal);

                // DateTime Value Info
                if (dateTimeLocal != null && dateTimeLocal.IsDateTimeConvertible)
                {
                    DtTimeStamp = dateTimeLocal.GetDateTime();
                }
                else if (dateTimeLocal != null && dateTimeLocal.IsDateConvertible)
                {
                    DtTimeStamp = dateTimeLocal.GetDate();
                }
                else if (dateTimeLocal != null && dateTimeLocal.IsTimeConvertible)
                {
                    DtTimeStamp = DateTime.MinValue.Add(dateTimeLocal.GetTime());
                }

                ResetMaxIOFailure();
            }
            catch (Exception ex)
            {
                temp = double.NaN;
                //  throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                System.Diagnostics.Debug.WriteLineIf(true, string.Format("Error occurred while Getting {0} {1}"
                                                    , arg.ToString(), ex.ToString()));
            }

            return temp;
        }

        public bool TryGETValue_Any(IDecodeAnyObject DataContainer_Class_obj, string Data_Property, Get_Index arg, byte attribute)
        {
            bool isSuccess = true;

            try
            {
                if (string.IsNullOrEmpty(Data_Property))
                    throw new ArgumentNullException("Data_Property");
                if (DataContainer_Class_obj == null)
                    throw new ArgumentNullException("DecodeAnyObject");

                Base_Class Comm_Obj = GET_Object(arg, attribute);
                isSuccess = DataContainer_Class_obj.TryDecode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID), DataContainer_Class_obj, Data_Property);

                // isSuccess = false;
                if (isSuccess)
                    ResetMaxIOFailure();
            }
            catch (Exception ex)
            {
                isSuccess = false;
                //  throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                System.Diagnostics.Debug.WriteLineIf(true, string.Format("Error occurred while Getting {0} {1}"
                                                    , arg.ToString(), ex.ToString()));
            }

            return isSuccess;
        }

        public bool TryGETValue_Any(IDecodeAnyObject DataContainer_Class_obj, string Data_Property, string TimeStamp_DataProperty, Get_Index arg, byte attribute)
        {
            bool isSuccess = true;

            try
            {
                if (string.IsNullOrEmpty(Data_Property))
                    throw new ArgumentNullException("Data_Property");
                if (DataContainer_Class_obj == null)
                    throw new ArgumentNullException("DecodeAnyObject");

                Base_Class Comm_Obj = GET_Object(arg, attribute);
                isSuccess = DataContainer_Class_obj.TryDecode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID), DataContainer_Class_obj, Data_Property, TimeStamp_DataProperty);

                // isSuccess = false;
                if (isSuccess)
                    ResetMaxIOFailure();
            }
            catch (Exception ex)
            {
                isSuccess = false;
                //  throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                System.Diagnostics.Debug.WriteLineIf(true, string.Format("Error occurred while Getting {0} {1}"
                                                    , arg.ToString(), ex.ToString()));
            }

            return isSuccess;
        }

        public double TryGET_Any(IDecodeAnyObject Instantaneous_Class_obj, StOBISCode arg, byte attribute, ref DateTime DtTimeStamp)
        {
            double temp = double.NaN;
            DtTimeStamp = DateTime.MinValue;

            StDateTime dateTimeLocal = null;

            try
            {
                Base_Class Comm_Obj = GET_Object(arg, attribute);
                temp = Instantaneous_Class_obj.Decode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID), ref dateTimeLocal);

                // DateTime Value Info
                if (dateTimeLocal != null && dateTimeLocal.IsDateTimeConvertible)
                {
                    DtTimeStamp = dateTimeLocal.GetDateTime();
                }
                else if (dateTimeLocal != null && dateTimeLocal.IsDateConvertible)
                {
                    DtTimeStamp = dateTimeLocal.GetDate();
                }
                else if (dateTimeLocal != null && dateTimeLocal.IsTimeConvertible)
                {
                    DtTimeStamp = DateTime.MinValue.Add(dateTimeLocal.GetTime());
                }

            }
            catch (Exception ex)
            {
                temp = double.NaN;
                //  throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
                System.Diagnostics.Debug.WriteLineIf(true, string.Format("Error occurred while Getting {0} {1}"
                                                    , arg.ToString(), ex.ToString()));
            }
            return temp;
        }

        #endregion


        #region Instantaneous Data

        public void GET_Instaneous_Voltage_Ph_A(Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                // Instantaneous_Class_obj.Voltage_Ph_A = GET_Any(Instantaneous_Class_obj, Get_Index.Voltage_Ph_A, 0);
                TryGETValue_Any(Instantaneous_Class_obj, "VoltagePhaseA", Get_Index.Voltage_Ph_A, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Voltage_Ph_A", ex);
            }
        }

        public void GET_Instaneous_Voltage_Ph_B(Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                // Instantaneous_Class_obj.Voltage_Ph_B = GET_Any(Instantaneous_Class_obj, Get_Index.Voltage_Ph_B, 0);
                TryGETValue_Any(Instantaneous_Class_obj, "VoltagePhaseB", Get_Index.Voltage_Ph_B, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Voltage_Ph_B", ex);
            }
        }

        public void GET_Instaneous_Voltage_Ph_C(Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                // Instantaneous_Class_obj.Voltage_Ph_C = GET_Any(Instantaneous_Class_obj, Get_Index.Voltage_Ph_C, 0);
                TryGETValue_Any(Instantaneous_Class_obj, "VoltagePhaseC", Get_Index.Voltage_Ph_C, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Voltage_Ph_C", ex);
            }
        }

        public void GET_Instaneous_Current_Phase_A(Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                Instantaneous_Class_obj.current_PhaseA = GET_Any(Instantaneous_Class_obj, Get_Index.Current_Ph_A, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Current_Phase_A", ex);
            }
        }

        public void GET_Instaneous_Current_Phase_B(Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                Instantaneous_Class_obj.current_PhaseB = GET_Any(Instantaneous_Class_obj, Get_Index.Current_Ph_B, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Current_Phase_B", ex);
            }
        }

        public void GET_Instaneous_Current_Phase_C(Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                Instantaneous_Class_obj.current_PhaseC = GET_Any(Instantaneous_Class_obj, Get_Index.Current_Ph_C, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Current_Phase_C", ex);
            }
        }

        public void GET_Instaneous_Active_Power_Pos_Phase_A(Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                Instantaneous_Class_obj.active_powerPositive_PhaseA = GET_Any(Instantaneous_Class_obj, Get_Index.Active_Power_Ph_A_Pos, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Decode_Active_Power_Pos_Phase_A", ex);
            }
        }

        public void GET_Instaneous_Active_Power_Pos_Phase_B(Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                Instantaneous_Class_obj.active_powerPositive_PhaseB = GET_Any(Instantaneous_Class_obj, Get_Index.Active_Power_Ph_B_Pos, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Decode_Active_Power_Pos_Phase_B", ex);
            }
        }

        public void GET_Instaneous_Active_Power_Pos_Phase_C(Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                Instantaneous_Class_obj.active_powerPositive_PhaseC = GET_Any(Instantaneous_Class_obj, Get_Index.Active_Power_Ph_C_Pos, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Decode_Active_Power_Pos_Phase_C", ex);
            }
        }

        public void GET_Instaneous_Active_Power_Neg_Phase_A(Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                Instantaneous_Class_obj.active_powerNegative_PhaseA = GET_Any(Instantaneous_Class_obj, Get_Index.Active_Power_Ph_A_Neg, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Decode_Active_Power_Neg_Phase_A", ex);
            }
        }

        public void GET_Instantaneous_Active_Power_Neg_Phase_B(Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                Instantaneous_Class_obj.active_powerNegative_PhaseB = GET_Any(Instantaneous_Class_obj, Get_Index.Active_Power_Ph_B_Neg, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Decode_Active_Power_Neg_Phase_B", ex);
            }
        }

        public void GET_Instaneous_Active_Power_Neg_Phase_C(Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                Instantaneous_Class_obj.active_powerNegative_PhaseC = GET_Any(Instantaneous_Class_obj, Get_Index.Active_Power_Ph_C_Neg, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Decode_Active_Power_Neg_Phase_C", ex);
            }
        }

        public void GET_Instaneous_Active_Power_Pos_Total(Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                Instantaneous_Class_obj.active_powerPositive_PhaseTL = GET_Any(Instantaneous_Class_obj, Get_Index.Active_Power_Total_Pos, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Active_Power_Pos_Total_", ex);
            }
        }

        public void GET_Instaneous_Active_Power_Neg_Total(Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                Instantaneous_Class_obj.active_powerNegative_PhaseTL = GET_Any(Instantaneous_Class_obj, Get_Index.Active_Power_Total_Neg, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Active_Power_Neg_Total_", ex);
            }
        }

        public void GET_Instaneous_Reactive_Power_Pos_Phase_A(Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                Instantaneous_Class_obj.reactive_powerPositive_PhaseA = GET_Any(Instantaneous_Class_obj, Get_Index.Reactive_Power_Ph_A_Pos, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Reactive_Power_Ph_A_Pos_Comm_Obj", ex);
            }
        }

        public void GET_Instaneous_Reactive_Power_Pos_Phase_B(Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                Instantaneous_Class_obj.reactive_powerPositive_PhaseB = GET_Any(Instantaneous_Class_obj, Get_Index.Reactive_Power_Ph_B_Pos, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Reactive_Power_Ph_B_Pos_Comm_Obj", ex);
            }
        }

        public void GET_Instaneous_Reactive_Power_Pos_Phase_C(Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                Instantaneous_Class_obj.reactive_powerPositive_PhaseC = GET_Any(Instantaneous_Class_obj, Get_Index.Reactive_Power_Ph_C_Pos, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Decode_Reactive_Power_Pos_Phase_C", ex);
            }
        }

        public void GET_Instaneous_Reactive_Power_Neg_Phase_A(Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                Instantaneous_Class_obj.reactive_powerNegative_PhaseA = GET_Any(Instantaneous_Class_obj, Get_Index.Reactive_Power_Ph_A_Neg, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Decode_Reactive_Power_Neg_Phase_A", ex);
            }
        }

        public void GET_Instaneous_Reactive_Power_Neg_Phase_B(Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                Instantaneous_Class_obj.reactive_powerNegative_PhaseB = GET_Any(Instantaneous_Class_obj, Get_Index.Reactive_Power_Ph_B_Neg, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Decode_Reactive_Power_Neg_Phase_B", ex);
            }
        }

        public void GET_Instaneous_Reactive_Power_Neg_Phase_C(Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                Instantaneous_Class_obj.reactive_powerNegative_PhaseC = GET_Any(Instantaneous_Class_obj, Get_Index.Reactive_Power_Ph_C_Neg, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Decode_Reactive_Power_Neg_Phase_C", ex);
            }
        }

        public void GET_Instaneous_Reactive_Power_Pos_Total(Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                Instantaneous_Class_obj.reactive_powerPositive_PhaseTL = GET_Any(Instantaneous_Class_obj, Get_Index.Reactive_Power_Total_Pos, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Reactive_Power_Pos_Total", ex);
            }
        }

        public void GET_Instaneous_Reactive_Power_Neg_Total(Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                Instantaneous_Class_obj.reactive_powerNegative_PhaseTL = GET_Any(Instantaneous_Class_obj, Get_Index.Reactive_Power_Total_Neg, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Reactive_Power_Neg_Total", ex);
            }
        }

        public void GET_Instaneous_Supply_Frequency(Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                Instantaneous_Class_obj.frequency = GET_Any(Instantaneous_Class_obj, Get_Index.Supply_Frequency, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Supply_Frequency", ex);
            }
        }

        public void GET_Instaneous_Power_Factor_A(Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                Instantaneous_Class_obj.Power_Factor_Ph_A = GET_Any(Instantaneous_Class_obj, Get_Index.Power_Factor_Ph_A, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Power_Factor_A", ex);
            }
        }

        public void GET_Instaneous_Power_Factor_B(ref Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                Instantaneous_Class_obj.Power_Factor_Ph_B = GET_Any(Instantaneous_Class_obj, Get_Index.Power_Factor_Ph_B, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Power_Factor_B", ex);
            }
        }

        public void GET_Instaneous_Power_Factor_C(ref Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                Instantaneous_Class_obj.Power_Factor_Ph_C = GET_Any(Instantaneous_Class_obj, Get_Index.Power_Factor_Ph_C, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Power_Factor_C", ex);
            }
        }

        public void GET_Instaneous_Power_Factor_All(ref Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                Instantaneous_Class_obj.Power_Factor_All = GET_Any(Instantaneous_Class_obj, Get_Index.Power_Factor_All, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Power_Factor_All", ex);
            }
        }

        public void GET_Instaneous_Battery_Volts(ref Instantaneous_Class Instantaneous_Class_obj)
        {
            try
            {
                Instantaneous_Class_obj.Battery_Volts = GET_Any(Instantaneous_Class_obj, Get_Index.Battery_Volts, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Supply_Frequency", ex);
            }
        }

        #endregion

        #region Support_Methods

        public void ResetMaxIOFailure()
        {
            LastIOFailureCount = 0;
        }

        public bool IsMaxIOFailureOccure(bool isFailure = true)
        {
            try
            {
                if (isFailure)
                    this.LastIOFailureCount++;
                if (LastIOFailureCount > ApplicationController.MaxRetryReadFailure)
                    return true;
                else
                    return false;
            }
            finally
            {
                // IO Operation Is Success
                if (!isFailure)
                    this.LastIOFailureCount = 0;
            }
        }

        #endregion

    }
}
