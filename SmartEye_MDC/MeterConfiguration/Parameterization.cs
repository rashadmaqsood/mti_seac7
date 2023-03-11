using comm;
using Communicator.MTI_MDC;
using DatabaseManager.Database;
using DLMS;
using DLMS.Comm;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.Param;
using SharedCode.Common;
using SharedCode.Controllers;
using SharedCode.eGeniousDisplayUnit;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Communicator.MeterConfiguration
{
    public class Parameterization
    {
        #region Data Member
        private TimeSpan MinScrollTime = TimeSpan.FromSeconds(5);
        private TimeSpan MaxScrollTime = TimeSpan.FromSeconds(3600);
        private ParameterController param_Controller;
        private DatabaseController _DBController = null;
        private LoadProfileController loadProfile_Controller;

        private Param_Standard_IP_Profile[] param_IP_Profiles_object = new Param_Standard_IP_Profile[4];
        private Param_IP_Profiles default_Param_IP_Profile = null;

        private Param_Modem_Initialize param_Modem_Initialize_Object = null;
        private Param_ModemBasics_NEW param_ModemBasics_NEW_object = null;
        private Param_Keep_Alive_IP param_Keep_Alive_IP_object = null;
        private Param_MajorAlarmProfile param_MajorAlarmProfile_Object = null;

        //public uint[] MajorAlarmsToSET = new uint[60];
        private List<LoadProfileChannelInfo> AllSelectableChannels = null;
        private List<LoadProfileChannelInfo> LoadProfileChannelsInfo = null;
        private TimeSpan LoadProfileInterval;

        private DisplayWindows Obj_displayWindows_normal;
        private DisplayWindows Obj_displayWindows_alternate;

        private Param_Limits obj_Param_Limits = null;
        private Param_Monitoring_Time obj_Param_MonitoringTime = null;
        private Param_CTPT_Ratio obj_Param_CTPT = null;
        private Param_Decimal_Point obj_Param_Decimal = null;
        private Param_Energy_Parameter obj_Param_Energy = null;
        private Param_Load_Scheduling obj_Param_Load_Scheduling = null;

        //private ConsumptionDataNow     obj_Consumption_Data_Now     = null;
        //private ConsumptionDataWeekly  obj_Consumption_Data_Weekly  = null;
        //private ConsumptionDataMonthly obj_Consumption_Data_Monthly = null;

        #endregion

        #region Properties
        private DisplayWindows Param_DisplayWindowsNormal
        {
            get
            {
                return Obj_displayWindows_normal;
            }
            set
            {
                Obj_displayWindows_normal = value;
            }
        }

        private DisplayWindows Param_DisplayWindowsAlternate
        {
            get
            {
                return Obj_displayWindows_alternate;

            }
            set
            {
                Obj_displayWindows_alternate = value;

            }
        }
        public Param_MajorAlarmProfile Param_MajorAlarmProfile_Object
        {
            get { return param_MajorAlarmProfile_Object; }
            set { param_MajorAlarmProfile_Object = value; }
        }

        public Param_IP_Profiles Default_Param_IP_Profile
        {
            get
            {
                return default_Param_IP_Profile;
            }
            set
            {
                default_Param_IP_Profile = value;
            }
        }
        public ParameterController Param_Controller
        {
            get { return param_Controller; }
            set { param_Controller = value; }
        }
        public Param_Keep_Alive_IP Param_Keep_Alive_IP_object
        {
            get { return param_Keep_Alive_IP_object; }
            set { param_Keep_Alive_IP_object = value; }
        }
        public Param_ModemBasics_NEW Param_ModemBasics_NEW_object
        {
            get { return param_ModemBasics_NEW_object; }
            set { param_ModemBasics_NEW_object = value; }
        }
        public Param_Modem_Initialize Param_Modem_Initialize_Object
        {
            get { return param_Modem_Initialize_Object; }
            set { param_Modem_Initialize_Object = value; }
        }
        public Param_Standard_IP_Profile[] Param_IP_Profiles_object
        {
            get { return param_IP_Profiles_object; }
            set { param_IP_Profiles_object = value; }
        }
        public DatabaseController DBController
        {
            get { return _DBController; }
            set { _DBController = value; }
        }
        public LoadProfileController LoadProfile_Controller
        {
            get { return loadProfile_Controller; }
            set { loadProfile_Controller = value; }
        }
        public Param_Load_Scheduling ParamLoadScheduling
        {
            get { return obj_Param_Load_Scheduling; }
            set { obj_Param_Load_Scheduling = value; }
        }
        //public ConsumptionDataNow ParamConsumptionDataNow
        //{
        //    get { return obj_Consumption_Data_Now; }
        //    set { obj_Consumption_Data_Now = value; }
        //}
        //public ConsumptionDataWeekly ParamConsumptionDataWeekly
        //{
        //    get { return obj_Consumption_Data_Weekly; }
        //    set { obj_Consumption_Data_Weekly = value; }
        //}
        //public ConsumptionDataMonthly ParamConsumptionDataMonthly
        //{
        //    get { return obj_Consumption_Data_Monthly; }
        //    set { obj_Consumption_Data_Monthly = value; }
        //}
        #endregion

        #region Constructor
        public Parameterization()
        {
            Initialization();
            InitializeIPProfiles(false);
            Obj_displayWindows_normal = new DisplayWindows();
            Obj_displayWindows_alternate = new DisplayWindows();
        }
        #endregion

        #region Member Functions

        #region SET
        public bool SET_DisplayWindows_Nor(uint ID, TimeSpan ScrollTime, uint format) //format=1 Number mode, 2=Obis Mode
        {
            try
            {
                ReadDisplayWindowsNormal(ID, format);//Read windows from database
                if (ScrollTime >= MinScrollTime && ScrollTime <= MaxScrollTime)
                    Param_DisplayWindowsNormal.ScrollTime = ScrollTime;
                else
                    Param_DisplayWindowsNormal.ScrollTime = MinScrollTime;

                if (Param_DisplayWindowsNormal.Windows.Count <= 50)
                {
                    if (Param_Controller.Set_DisplayWindow_Normal(Param_DisplayWindowsNormal) == Data_Access_Result.Success)
                        return true;
                }
                else
                {
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0},{1}", (ex != null) ? ex.Message : "", (ex != null && ex.InnerException != null) ? ex.InnerException.Message : ""));
            }
        }

        public bool SET_DisplayWindows_Alt(uint ID, TimeSpan ScrollTime, uint format)
        {
            try
            {
                ReadDisplayWindowsAlternate(ID, format);//Read Windows From Database
                if (ScrollTime > MinScrollTime && ScrollTime < MaxScrollTime)
                    Param_DisplayWindowsAlternate.ScrollTime = ScrollTime;
                else
                    Param_DisplayWindowsAlternate.ScrollTime = MinScrollTime;

                if (Param_DisplayWindowsAlternate.Windows.Count <= 50)
                {
                    if (Param_Controller.Set_DisplayWindow_Alternate(Param_DisplayWindowsAlternate) == Data_Access_Result.Success)
                        return true;
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0},{1}", (ex != null) ? ex.Message : "", (ex != null && ex.InnerException != null) ? ex.InnerException.Message : ""));

            }
        }

        public bool SET_IPProfiles(long IPProfileID, bool IsStandardParam)
        {
            bool result = ReadIPProfile(IPProfileID, IsStandardParam);
            if (!result)
                return result;

            Data_Access_Result dataAccessResult;
            if (IsStandardParam)
            {
                dataAccessResult = Param_Controller.SET_Standard_IP_Profiles(Param_IP_Profiles_object);
            }
            else
            {
                Param_IP_Profiles[] tempIPProfiles = new Param_IP_Profiles[Param_IP_Profiles_object.Length];
                int indexer = 0;
                for (byte i = 0; i < Param_IP_Profiles_object.Length; i++)
                {
                    tempIPProfiles[indexer++] = (Param_IP_Profiles)Param_IP_Profiles_object[i];
                }
                dataAccessResult = Param_Controller.SET_IP_Profiles(tempIPProfiles);
            }

            if (dataAccessResult == Data_Access_Result.Success)
                return true;
            else
                return false;
        }

        public void SET_ModemInitializeBasic(long ModemInitializeID)
        {
            ReadModemInitialize(ModemInitializeID);
            Param_Controller.SET_ModemBasics(Param_Modem_Initialize_Object);
        }

        public void SET_ModemInitializeExtended(long ModemInitializeID)
        {
            ReadModemInitialize(ModemInitializeID);
            Param_Controller.SET_ModemBasicsNew(Param_ModemBasics_NEW_object);
        }

        public void SET_ModemInitializeComplete(long ModemInitializeID)
        {
            ReadModemInitialize(ModemInitializeID);
            Param_Controller.SET_ModemBasics(Param_Modem_Initialize_Object);
            Param_Controller.SET_ModemBasicsNew(Param_ModemBasics_NEW_object);
        }

        public void SET_KeepAlive(long KeepAliveID)
        {
            ReadKeepAlive(KeepAliveID);
            Param_Controller.SET_Keep_Alive_IP(Param_Keep_Alive_IP_object);
        }

        public bool SET_LoadProfileChannels(long LPChannelsGrouId, LoadProfileScheme lpScheme)
        {

            try
            {
                Data_Access_Result result = this.LoadProfile_Controller.Set_LoadProfileChannels(LPChannelsGrouId, lpScheme);
                if (result == Data_Access_Result.Success)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }

        public bool SET_LoadProfileInterval(TimeSpan LoadProfilePeriod, LoadProfileScheme lpScheme)
        {
            try
            {
                Data_Access_Result result = this.LoadProfile_Controller.Set_LoadProfileInterval(LoadProfilePeriod, lpScheme);

                if (result == Data_Access_Result.Success)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }


        public bool SET_Current_Association_Password(string password)
        {
            var result = Param_Controller.Set_CurrentAssociationPassword(password);

            if (result == Data_Access_Result.Success)
            {
                return true;
            }
            return false;

        }

        public bool SET_MDI_AUTO_REST_DATE_TIME(StDateTime date)
        {
            var result = Param_Controller.Set_MDI_Date_Time(date);

            if (result == Data_Access_Result.Success)
            {
                return true;
            }
            return false;

        }

        public bool SET_Limits(long param_Id)
        {
            try
            {
                if (_DBController.GetLimitsParams(param_Id, ref obj_Param_Limits))
                {
                    return Param_Controller.SET_Limits_All(obj_Param_Limits);
                }
                else
                {
                    throw new Exception("Error Loading Limits Parameters From DB");
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool SET_MonitoringTime(long param_Id)
        {
            try
            {
                if (_DBController.GetMonitoringTimeParams(param_Id, ref obj_Param_MonitoringTime))
                {
                    return Param_Controller.SET_MonitoringTimeAll(obj_Param_MonitoringTime);
                }
                else
                {
                    throw new Exception("Error Loading Monitoring Time Parameters From DB");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool SET_CTPT_Ratio(long param_Id)
        {
            try
            {
                if (DBController.GetCTPTParams(param_Id, ref obj_Param_CTPT))
                {
                    return Param_Controller.SET_CTPT_Param(obj_Param_CTPT);
                }
                else
                {
                    throw new Exception("Error Loading CT PT Parameters From DB");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool SET_DecimalPoints(long param_Id)
        {
            try
            {
                if (DBController.GetDecimalPointsParams(param_Id, ref obj_Param_Decimal))
                {
                    return (Param_Controller.SET_Decimal_Point(obj_Param_Decimal) == Data_Access_Result.Success);
                }
                else
                {
                    throw new Exception("Error Loading Decimal Points Parameters From DB");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool SET_EnergyParams(long param_Id)
        {
            try
            {
                if (DBController.GetEnergyParams(param_Id, ref obj_Param_Energy))
                {
                    return (Param_Controller.SET_EnergyParams(obj_Param_Energy) == Data_Access_Result.Success);
                }
                else
                {
                    throw new Exception("Error Loading Energy Parameters From DB");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool SET_SchedulerTable(long scheduleId)
        {
            try
            {
                if (DBController.GetLoadSheddingParams(scheduleId, ref obj_Param_Load_Scheduling))
                {
                    StOBISCode obis_schedule = Get_Index.SCHEDULE_TABLE;

                    return (Param_Controller.SET_SchedulerTable(obis_schedule, obj_Param_Load_Scheduling.ListOfEntries) == Data_Access_Result.Success);
                }
                else
                {
                    throw new Exception("Error Loading Schedule Table Entries From DB");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ConsumptionDataNow SET_ConsumptionDataNow(int feederId)
        {
            try
            {
                ConsumptionDataNow obj_Consumption_Data_Now = null;
                if (!DBController.GetConsumptionDataNow(feederId, ref obj_Consumption_Data_Now))
                {
                    throw new Exception("Error Loading Consumption_Data_Now From DB");
                }

                StOBISCode obis = Get_Index.CONSUMPTION_DATA_NOW;
                Data_Access_Result result = Param_Controller.SET_ConsumptionDataNow(obis, obj_Consumption_Data_Now);
                if (result == Data_Access_Result.Success)
                    return obj_Consumption_Data_Now;
                else
                    throw new Exception("Error Writing Consumption Data: " + result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ConsumptionDataWeekly SET_ConsumptionDataWeekly(int feederId)
        {
            try
            {
                ConsumptionDataWeekly obj_Consumption_Data_Weekly = null;
                if (!DBController.GetConsumptionDataWeekly(feederId, ref obj_Consumption_Data_Weekly))
                    throw new Exception("Error Loading ConsumptionDataWeekly From DB");

                StOBISCode obis = Get_Index.CONSUMPTION_DATA_WEEKLY;

                Data_Access_Result result = Param_Controller.SET_ConsumptionDataWeekly(obis, obj_Consumption_Data_Weekly);

                if (result == Data_Access_Result.Success)
                    return obj_Consumption_Data_Weekly;
                else
                    throw new Exception("Error Writing Consumption Data: " + result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ConsumptionDataMonthly SET_ConsumptionDataMonthly(int feederId, string msn)
        {
            try
            {
                ConsumptionDataMonthly obj_Consumption_Data = null;
                if (!DBController.GetConsumptionDataMonthly(feederId, ref obj_Consumption_Data, msn))
                    throw new Exception("Error Loading ConsumptionDataMonthly From DB");

                StOBISCode obis = Get_Index.CONSUMPTION_DATA_MONTHLY;

                Data_Access_Result result = Param_Controller.SET_ConsumptionDataMonthly(obis, obj_Consumption_Data);

                if (result == Data_Access_Result.Success)
                    return obj_Consumption_Data;
                else
                    throw new Exception("Error Writing Consumption Data: " + result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Read

        public void ReadDisplayWindowsNormal(uint ID, uint format)
        {

            try
            {
                WindowNumber window_number = new WindowNumber();
                if (format == 2)
                {
                    window_number.OBIS_Code_Display_Mode = true;
                    window_number.Display_OBIS_Field_C = true;
                    window_number.Display_OBIS_Field_D = true;
                    window_number.Display_OBIS_Field_E = true;
                }
                else
                {
                    window_number.OBIS_Code_Display_Mode = false;
                    window_number.Display_OBIS_Field_C = false;
                    window_number.Display_OBIS_Field_D = false;
                    window_number.Display_OBIS_Field_E = false;
                }
                DataTable DT = DBController.GetDisplayWindowsNormal(ID);
                Param_DisplayWindowsNormal.Windows.Clear();
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    DisplayWindowItem Item = new DisplayWindowItem();
                    Item.AttributeSelected = 0; //Hard Coded
                    Item.Obis_Index = (Get_Index)(Convert.ToInt64(DT.Rows[i]["quantity_index"]));
                    window_number.DisplayWindowNumber = Convert.ToUInt16(DT.Rows[i]["window_number"]);
                    Item.WindowNumberToDisplay = window_number;
                    Param_DisplayWindowsNormal.Windows.Add(Item);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ReadDisplayWindowsAlternate(uint ID, uint format)
        {
            WindowNumber window_number = new WindowNumber();
            if (format == 2)
            {
                window_number.OBIS_Code_Display_Mode = true;
                window_number.Display_OBIS_Field_C = true;
                window_number.Display_OBIS_Field_D = true;
                window_number.Display_OBIS_Field_E = true;
            }
            else
            {
                window_number.OBIS_Code_Display_Mode = false;
                window_number.Display_OBIS_Field_C = false;
                window_number.Display_OBIS_Field_D = false;
                window_number.Display_OBIS_Field_E = false;
            }
            Param_DisplayWindowsAlternate.Windows.Clear();

            DataTable DT = DBController.GetDisplayWindowsAlternate(ID);
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                DisplayWindowItem Item = new DisplayWindowItem();
                Item.AttributeSelected = 0; //Hard Coded
                Item.Obis_Index = (Get_Index)(Convert.ToInt64(DT.Rows[i]["quantity_index"]));
                window_number.DisplayWindowNumber = Convert.ToUInt16(DT.Rows[i]["window_number"]);
                Item.WindowNumberToDisplay = window_number;
                Param_DisplayWindowsAlternate.Windows.Add(Item);
            }
        }
        //public void ReadMajorAlarm(long MajorAlarmID)
        //{
        //    DataTable DT = DBController.GetMajorAlarmFromDatabase(MajorAlarmID);
        //    for (int i = 1, j = 0; i < DT.Columns.Count; i++)
        //    {
        //        MajorAlarmsToSET[j++] = Convert.ToUInt16(DT.Rows[0][i]);
        //    }
        //}

        public void ReadKeepAlive(long KeepAliveID)
        {
            DataTable DT = DBController.GetKeepAliveFromDatabase(KeepAliveID);
            Param_Keep_Alive_IP_object.Enabled = Convert.ToBoolean(DT.Rows[0][DT.Columns["enable_always_on"]]);
            Param_Keep_Alive_IP_object.IP_Profile_ID = (!Param_Keep_Alive_IP_object.Enabled) ? (byte)0 : Convert.ToByte(DT.Rows[0][DT.Columns["wakeup_profile_id"]]);
            Param_Keep_Alive_IP_object.Ping_time = Convert.ToUInt16(DT.Rows[0][DT.Columns["ping_time"]]);
        }

        public bool ReadIPProfile(long IPProfileID, bool isStandardParam)
        {
            int Counter = 0;
            DataTable DT = DBController.GetIPProfileFromDatabase(IPProfileID);
            InitializeIPProfiles(isStandardParam);
            //IPAddress _IPaddress = IPAddress.Parse(DT.Rows[0][DT.Columns["ip_1"]].ToString());
            try
            {
                Param_IP_Profiles_object[0].IP = DLMS.DLMS_Common.IPAddressToLong(IPAddress.Parse(DT.Rows[0][DT.Columns["ip_1"]].ToString().Replace(" ", "")));
                Param_IP_Profiles_object[0].Wrapper_Over_TCP_port = Convert.ToUInt16(DT.Rows[0][DT.Columns["w_tcp_port_1"]]);
                if (!isStandardParam)
                    ((Param_IP_Profiles)param_IP_Profiles_object[0]).Unique_ID = 1;
                Counter++;
            }
            catch (Exception)
            {
                return false;
            }
            //Param_IP_Profiles_object[1].IP = Convert.ToUInt32(DT.Rows[0][DT.Columns["ip_2"]]);
            try
            {
                Param_IP_Profiles_object[1].IP = DLMS.DLMS_Common.IPAddressToLong(IPAddress.Parse(DT.Rows[0][DT.Columns["ip_2"]].ToString().Replace(" ", "")));
                Param_IP_Profiles_object[1].Wrapper_Over_TCP_port = Convert.ToUInt16(DT.Rows[0][DT.Columns["w_tcp_port_2"]]);
                if (!isStandardParam)
                    ((Param_IP_Profiles)param_IP_Profiles_object[1]).Unique_ID = 2;
                Counter++;
            }
            catch (Exception)
            {
                ;
            }
            try
            {
                //Param_IP_Profiles_object[2].IP = Convert.ToUInt32(DT.Rows[0][DT.Columns["ip_3"]]);
                Param_IP_Profiles_object[2].IP = DLMS.DLMS_Common.IPAddressToLong(IPAddress.Parse(DT.Rows[0][DT.Columns["ip_3"]].ToString().Replace(" ", "")));
                Param_IP_Profiles_object[2].Wrapper_Over_TCP_port = Convert.ToUInt16(DT.Rows[0][DT.Columns["w_tcp_port_3"]]);
                if (!isStandardParam)
                    ((Param_IP_Profiles)param_IP_Profiles_object[2]).Unique_ID = 3;
                Counter++;
            }
            catch (Exception)
            {
                ;
            }
            try
            {
                //Param_IP_Profiles_object[3].IP = Convert.ToUInt32(DT.Rows[0][DT.Columns["ip_4"]]);
                Param_IP_Profiles_object[3].IP = DLMS.DLMS_Common.IPAddressToLong(IPAddress.Parse(DT.Rows[0][DT.Columns["ip_4"]].ToString().Replace(" ", "")));
                Param_IP_Profiles_object[3].Wrapper_Over_TCP_port = Convert.ToUInt16(DT.Rows[0][DT.Columns["w_tcp_port_4"]]);
                if (!isStandardParam)
                    ((Param_IP_Profiles)param_IP_Profiles_object[3]).Unique_ID = 4;
                Counter++;
            }
            catch (Exception)
            {
                ;
            }
            if (isStandardParam) return true;
            for (int i = 0; i < Counter; i++)
            {
                //Param_IP_Profiles_object[i].Unique_ID = (byte)(i+1);
                ((Param_IP_Profiles)Param_IP_Profiles_object[i]).Wrapper_Over_UDP_port = Convert.ToUInt16(DT.Rows[0][DT.Columns["w_udp_port"]]);
                ((Param_IP_Profiles)Param_IP_Profiles_object[i]).HDLC_Over_TCP_Port = Convert.ToUInt16(DT.Rows[0][DT.Columns["h_tcp_port"]]);
                ((Param_IP_Profiles)Param_IP_Profiles_object[i]).HDLC_Over_UDP_Port = Convert.ToUInt16(DT.Rows[0][DT.Columns["h_udp_port"]]);
            }

            return true;
        }

        public void ReadModemInitialize(long ModemInitializeID)
        {
            DataTable DT = DBController.GetModemInitializeFromDatabase(ModemInitializeID);
            Param_Modem_Initialize_Object.APN = DT.Rows[0][DT.Columns["apn_string"]].ToString();
            Param_Modem_Initialize_Object.Username = DT.Rows[0][DT.Columns["username"]].ToString();
            Param_Modem_Initialize_Object.Password = DT.Rows[0][DT.Columns["password"]].ToString();
            Param_Modem_Initialize_Object.PIN_code = Convert.ToUInt16(DT.Rows[0][DT.Columns["pincode"]]);

            Param_ModemBasics_NEW_object.UserName = DT.Rows[0][DT.Columns["username"]].ToString();
            Param_ModemBasics_NEW_object.Password = DT.Rows[0][DT.Columns["password"]].ToString();
            Param_ModemBasics_NEW_object.WakeupPassword = DT.Rows[0][DT.Columns["wakeup_password"]].ToString();
            Param_ModemBasics_NEW_object.Flag_RLRQ = Convert.ToByte(DT.Rows[0][DT.Columns["release_association_on_tcp_disconnect"]]);
            Param_ModemBasics_NEW_object.Flag_DecrementCounter = Convert.ToByte(DT.Rows[0][DT.Columns["decrement_event_counter"]]);
        }
        #endregion

        #region Optical Port Access
        public bool SetOpticalPortAccess(DateTime startTime,DateTime endTime)
        {
            Param_OpticalPortAccess portAccess = new Param_OpticalPortAccess()
            {
                StartTime = startTime,
                EndTime = endTime
            };
            var writeResult = param_Controller.SET_OpticalPortAccess(portAccess);
            return writeResult == Data_Access_Result.Success;
        }
        #endregion
        #endregion

        #region Support Functions
        public void Initialization()
        {
            default_Param_IP_Profile = new Param_IP_Profiles();
            param_Keep_Alive_IP_object = new Param_Keep_Alive_IP();
            param_Modem_Initialize_Object = new Param_Modem_Initialize();
            param_ModemBasics_NEW_object = new Param_ModemBasics_NEW();
            param_MajorAlarmProfile_Object = new Param_MajorAlarmProfile();
            LoadProfileChannelsInfo = new List<LoadProfileChannelInfo>();
            obj_Param_Load_Scheduling = new Param_Load_Scheduling();
            ///Param_Controller = new ParameterController();
        }

        public void InitializeIPProfiles(bool Standard)
        {
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    Param_IP_Profiles_object[i] = Standard ? new Param_Standard_IP_Profile() : new Param_IP_Profiles();
                }
            }
            catch (Exception)
            {
                ;
            }
        }

        void load_IP_profile(string Dir)
        {
            try
            {
                //Dir += "IPProfile\\";
                if (Directory.Exists(Dir))
                {
                    string FILE = "\\IPProfile.xml"; //Default
                    System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(Param_IP_Profiles_object[0].GetType());
                    FileStream new_File = new FileStream(Dir + FILE, FileMode.OpenOrCreate);
                    Param_IP_Profiles_object[0] = (Param_IP_Profiles)x.Deserialize(new_File);
                    new_File.Close();
                }
                else
                {
                    Commons.WriteError(MDC.Default_DataLogger, "The following folder  does not exist :  " + Dir);
                }
            }
            catch (Exception Ex)
            {

                Commons.WriteError(MDC.Default_DataLogger, "Error Loading IP Profile" + Ex.Message);
            }

        }

        public void Init_LoadProfilesChannelsQuantities()
        {
            try
            {
                this.AllSelectableChannels = LoadProfile_Controller.Get_SelectableLoadProfileChannels();

            }
            catch
            {
                throw;
            }
        }

        private TimeSpan SaveLoadProfileInterval()
        {
            int loadProfileTimeInSec = 15;
            switch (loadProfileTimeInSec)
            {
                case 0:
                    loadProfileTimeInSec = 15 * 60;
                    break;
                case 1:
                    loadProfileTimeInSec = 30 * 60;
                    break;
                case 2:
                    loadProfileTimeInSec = 1 * (60 * 60);
                    break;
                case 3:
                    loadProfileTimeInSec = 2 * (60 * 60);
                    break;
                case 4:
                    loadProfileTimeInSec = 3 * (60 * 60);
                    break;
                case 5:
                    loadProfileTimeInSec = 4 * (60 * 60);
                    break;
                case 6:
                    loadProfileTimeInSec = 6 * (60 * 60);
                    break;
                case 7:
                    loadProfileTimeInSec = 8 * (60 * 60);
                    break;
                case 8:
                    loadProfileTimeInSec = 12 * (60 * 60);
                    break;
                case 9:
                    loadProfileTimeInSec = 24 * (60 * 60);
                    break;
                default:
                    throw new Exception("Invalid Load Profile Interval Value Selected");
            }
            TimeSpan LoadProfilePeriod = new TimeSpan(0, 0, loadProfileTimeInSec);
            return LoadProfilePeriod;
        }

        #endregion
    }
}
